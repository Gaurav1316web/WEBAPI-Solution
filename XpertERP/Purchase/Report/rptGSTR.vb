Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'' RICHA AGARWAL BHA/25/09/18-000567
Public Class rptGSTR
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim variable1 As String = Nothing
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click

        Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
        qry += " where 2=2 and Seg_No = '7' AND GIT='N'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Code", txtVendor.arrValueMember, txtVendor.arrDispalyMember)

    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_desc as Name from TSPL_VENDOR_group"
        txtVendGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGroupMulSel", qry, "Code", "Code", txtVendGroup.arrValueMember, txtVendGroup.arrDispalyMember)

    End Sub

    Private Sub rptAPReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtLocation.arrValueMember = Nothing
        'txtTransaction.arrValueMember = Nothing
        'txtItem.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing
        txtVendGroup.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustomerGroup.arrValueMember = Nothing
        btnGSTR1.IsChecked = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        gv3.DataSource = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage3.Text = "Detail data"

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            Dim qryheader As String = String.Empty
            Dim qryfooter As String = String.Empty
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            Dim Wrcls As String = String.Empty
            If btnGSTR1.IsChecked Then
                qryheader = GSTR1DataHeader()
                qryfooter = GSTR1DataFooter()
            Else
                qryheader = GSTQryforHeader()
                qryfooter = GSTQryforDetail()
            End If

            dt = clsDBFuncationality.GetDataTable(qryheader)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
                FormatGridHeader()
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            End If

            dt1 = clsDBFuncationality.GetDataTable(qryfooter)
            gv2.DataSource = Nothing
            gv2.Rows.Clear()
            gv2.Columns.Clear()
            gv2.GroupDescriptors.Clear()
            gv2.MasterTemplate.SummaryRowsBottom.Clear()
            gv2.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count > 0 Then
                gv2.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                gv2.BestFitColumns()
                gv2.EnableFiltering = True
                FormatGridFooter()
                If btnGSTR1.IsChecked = True Then
                    For i As Integer = 0 To gv2.Rows.Count - 1
                        If clsCommon.CompairString(gv2.Rows(i).Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal Or clsCommon.CompairString(gv2.Rows(i).Cells("Name").Value, "Reverse charge supplies") = CompairStringResult.Equal Then
                            gv2.Rows(i).Cells("SNo").Value = ""
                        End If
                    Next
                End If
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            End If

            'ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Function GSTR1DataHeader() As String
        Dim qryheader As String = String.Empty
        Dim qryfooter As String = String.Empty
        Dim dt As New DataTable
        Dim Wrcls As String = String.Empty
        Try
            Wrcls += "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")"
            End If


            ''for header grid data
            '-- ar invocie all transactions
            qryheader = " select 'Total number of vouchers for the period' as Name,count(*) from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 " & Wrcls & " " & Environment.NewLine & _
            " ------------------------ ar invoice Against-----------------" & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select 'Included in returns' as Name,count (*) from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine & _
            "------------------------------- ar invoice Against HSN/SAC Information (to be provided)------------------------- " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select 'Included in HSN/SAC Summary' as Name,count (*) from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 and (isnull(AgainstScrapReturn,'')<>'' or ISNULL (Against_Sale_Return_No,'')<>'' or ISNULL (Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine & _
            " -------------- Incomplete HSN/SAC information (to be provided)------------------------ " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select 'Incomplete HSN/SAC information (to be provided)' as Name,0 " & Environment.NewLine & _
            " ------------------------- ar invoice direct -------------------------- " & Environment.NewLine & _
            " union all  " & Environment.NewLine & _
            " select 'Not relevant for returns' as Name, sum(AllInvoices)-sum(InvoicesAgainst) as 'DirectInvoices' from ( " & Environment.NewLine & _
            " select count(*) as 'AllInvoices',0 as 'InvoicesAgainst' from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 " & Wrcls & " " & Environment.NewLine & _
            " union " & Environment.NewLine & _
            " select 0 as 'AllInvoices',count(*) as 'InvoicesAgainst' from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 and (isnull(AgainstScrapReturn,'')<>'' or ISNULL (Against_Sale_Return_No,'')<>'' or ISNULL (Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine & _
            " )z " & Environment.NewLine & _
            "-------------------------- Incomplete/Mismatch in information (to be resolved)----------------" & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select 'Incomplete/Mismatch in information (to be resolved)' as Name,0 " & Environment.NewLine
            ''====================
        Catch ex As Exception

        End Try
        Return qryheader
    End Function
    
    Function GSTR1DataFooter() As String

        Dim qryfooter As String = String.Empty
        Dim dt As New DataTable
        Dim Wrcls As String = String.Empty
        Try
            Wrcls += "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")"
            End If

            ''for footer grid data
            qryfooter = " -------------------------------------------------------registered customer against------------------------------------------------" & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine & _
            " select '1' as SNo,'B2B Invoices - 4A, 4B, 4C, 6B, 6C' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine & _
            " UNION " & Environment.NewLine & _
            " select '1' as SNo, 'B2B Invoices - 4A, 4B, 4C, 6B, 6C' as Name, '',0, 0, 0,0 ) z  group by Name " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount] ,0 as [Voucher Count1] ,0 as [Taxable Value1],0 as [Taxable Amount1],0 as [RoundOffAmount1],0 as [Invoice Amount1] from ( " & Environment.NewLine & _
            " select '1a' as SNo,'Taxable Sales' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount  from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine & _
            " UNION " & Environment.NewLine & _
            " select '1a' as SNo, 'Taxable Sales' as Name, '',0, 0, 0,0 ) z  group by Name " & Environment.NewLine & _
            "union all " & Environment.NewLine & _
            " select '1b' as SNo,'Reverse charge supplies' as Name, 0,0, 0, 0,0,0,0,0,0,0  " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " -----------------------------------------------------unregistered customer against-------------------------------------------------------------------------------- " & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine & _
            " select '2' as SNo,'B2C(Large) Invoices - 5A, 5B' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount  from TSPL_Customer_Invoice_Head  " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 " & Environment.NewLine & _
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine & _
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Environment.NewLine & _
            " and TSPL_Customer_Invoice_Head.Document_Total>250000 " & Wrcls & " " & Environment.NewLine & _
            " UNION " & Environment.NewLine & _
            " select '2' as SNo, 'B2C(Large) Invoices - 5A, 5B' as Name, '',0, 0, 0,0 ) z group by Name " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine & _
            " select '3' as SNo,'B2C(Small) Invoices - 7' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount from TSPL_Customer_Invoice_Head  " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 " & Environment.NewLine & _
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine & _
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Environment.NewLine & _
            " and TSPL_Customer_Invoice_Head.Document_Total<=250000 " & Wrcls & "  " & Environment.NewLine & _
            " UNION " & Environment.NewLine & _
            " select '3' as SNo, 'B2C(Small) Invoices - 7' as Name, '',0, 0, 0,0 ) z  group by Name " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " -------------------------------------------------------registered customer direct------------------------------------------------ " & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine & _
            " select '4' as SNo,'Credit/Debit Notes(Registered) - 9B' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount  from TSPL_Customer_Invoice_Head  " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') " & Environment.NewLine & _
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine & _
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='') " & Wrcls & "  " & Environment.NewLine & _
            " UNION " & Environment.NewLine & _
            " select '4' as SNo, 'Credit/Debit Notes(Registered) - 9B' as Name, '',0, 0, 0,0 ) z group by Name " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " -------------------------------------------------------unregistered customer direct------------------------------------------------ " & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine & _
            " select '5' as SNo,'Credit/Debit Notes(Unregistered) - 9B' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount  from TSPL_Customer_Invoice_Head  " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code  " & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D')" & Environment.NewLine & _
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine & _
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='') " & Wrcls & "  " & Environment.NewLine & _
            " UNION " & Environment.NewLine & _
            " select '5' as SNo, 'Credit/Debit Notes(Unregistered) - 9B' as Name, '',0, 0, 0,0 ) z  group by Name " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " -------------------------------------------------------registered/unregistered customer against export and Merchant------------------------------------------------ " & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine & _
            " select '6' as SNo,'Exports Invoices - 6A' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where 1=1 AND TSPL_Customer_Invoice_Head.Against_Sale_No  IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Wrcls & "  " & Environment.NewLine & _
            " UNION " & Environment.NewLine & _
            " select '6' as SNo, 'Exports Invoices - 6A' as Name, '',0, 0, 0,0 ) z group by Name " & Environment.NewLine & _
            " union all  " & Environment.NewLine & _
            " select '7' as SNo, 'Tax Liability(Advances received) - 11A(1), 11A(2)' as Name, 0,0, 0, 0,0,0, 0, 0,0,0  " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select '8' as SNo,'Adjustment of Advances - 11B(1), 11B(2)' as Name, 0,0, 0, 0,0,0, 0, 0,0,0  " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " -------------------------------------------------------registered/unregistered customer Nil Rated Invoices - 8A, 8B, 8C, 8D------------------------------------------------" & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine & _
            " select '9' as SNo,'Nil Rated Invoices - 8A, 8B, 8C, 8D' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount  from TSPL_Customer_Invoice_Head  " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where  isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) =0  AND TSPL_Customer_Invoice_Head.Against_Sale_No not IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Wrcls & "  " & Environment.NewLine & _
            " UNION " & Environment.NewLine & _
            " select '9' as SNo, 'Nil Rated Invoices - 8A, 8B, 8C, 8D' as Name, '',0, 0, 0,0 ) z  group by Name " & Environment.NewLine & _
              " union all" & Environment.NewLine & _
            " -------------------------------------------------------Taxable Invoice (Direct)------------------------------------------------ " & Environment.NewLine & _
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue) as [Taxable Value],sum(TaxableAmount) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine & _
            " select '10' as SNo,'Taxable Invoice (Direct)' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount  from TSPL_Customer_Invoice_Head  " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
            " where 1=1  and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 AND TSPL_Customer_Invoice_Head.Document_Type ='I'" & Environment.NewLine & _
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine & _
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='')  " & Wrcls & "   " & Environment.NewLine & _
            " UNION" & Environment.NewLine & _
            " select '10' as SNo, 'Taxable Invoice (Direct)' as Name, '',0, 0, 0,0 ) z group by Name " & Environment.NewLine

                        ''====================
        Catch ex As Exception

        End Try
        Return qryfooter
    End Function
    Sub FormatGridHeader()

        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next


        gv1.Columns("Name").IsVisible = True
        gv1.Columns("Name").Width = 450
        gv1.Columns("Name").HeaderText = "Returns Summary"


        gv1.Columns("Column1").IsVisible = True
        gv1.Columns("Column1").Width = 580
        gv1.Columns("Column1").HeaderText = " "





        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Sub FormatGridFooter()

        gv2.TableElement.TableHeaderHeight = 25
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        If btnGSTR1.IsChecked = True Then
            gv2.Columns("SNo").Width = 50
            gv2.Columns("SNo").HeaderText = "SI No."
            'gv2.Columns("SNo").FormatString = "{0:F0}"

            gv2.Columns("Name").Width = 350
            gv2.Columns("Name").HeaderText = "Particulars"
            'gv2.Columns("RoundOffAmount").HeaderText = "Round Off Amount"

            gv2.Columns("RoundOffAmount1").IsVisible = False
            ''richa BHA/26/10/18-000643
            gv2.Columns("Voucher Count1").IsVisible = False
            gv2.Columns("Taxable Value1").IsVisible = False
            gv2.Columns("Taxable Amount1").IsVisible = False
            gv2.Columns("Invoice Amount1").IsVisible = False

            Dim TotalClosing As New GridViewSummaryItem()
            TotalClosing.FormatString = "{0:F0}"
            TotalClosing.Name = "Voucher Count"
            TotalClosing.AggregateExpression = "sum([Voucher Count1])"
            summaryRowItem.Add(TotalClosing)

            Dim TotalClosing1 As New GridViewSummaryItem()
            TotalClosing1.FormatString = "{0:F2}"
            TotalClosing1.Name = "Taxable Amount"
            TotalClosing1.AggregateExpression = "sum([Taxable Amount1])"
            summaryRowItem.Add(TotalClosing1)

            Dim TotalClosing2 As New GridViewSummaryItem()
            TotalClosing2.FormatString = "{0:F2}"
            TotalClosing2.Name = "Taxable Value"
            TotalClosing2.AggregateExpression = "sum([Taxable Value1])"
            summaryRowItem.Add(TotalClosing2)

            Dim TotalClosing5 As New GridViewSummaryItem()
            TotalClosing5.FormatString = "{0:F2}"
            TotalClosing5.Name = "RoundOffAmount"
            TotalClosing5.AggregateExpression = "sum(RoundOffAmount1)"
            summaryRowItem.Add(TotalClosing5)

            Dim TotalClosing4 As New GridViewSummaryItem()
            TotalClosing4.FormatString = "{0:F2}"
            TotalClosing4.Name = "Invoice Amount"
            ' TotalClosing4.AggregateExpression = "sum([Invoice Amount1])"
            TotalClosing4.AggregateExpression = "sum([Invoice Amount1])-sum(RoundOffAmount1)"
            summaryRowItem.Add(TotalClosing4)

        Else

            gv2.Columns("Name").Width = 350
            gv2.Columns("Name").HeaderText = "Particulars"

            gv2.Columns("TaxableValue").Width = 100
            gv2.Columns("TaxableValue").HeaderText = "Taxable Value"

            gv2.Columns("TaxableAmount").Width = 100
            gv2.Columns("TaxableAmount").HeaderText = "Taxable Amount"

            gv2.Columns("InvoiceAmount").Width = 100
            gv2.Columns("InvoiceAmount").HeaderText = "Invoice Amount"

            gv2.Columns("TaxableValue1").Width = 100
            gv2.Columns("TaxableValue1").HeaderText = "Taxable Value1"
            gv2.Columns("TaxableValue1").IsVisible = False

            gv2.Columns("TaxableAmount1").Width = 100
            gv2.Columns("TaxableAmount1").HeaderText = "Taxable Amount1"
            gv2.Columns("TaxableAmount1").IsVisible = False

            gv2.Columns("InvoiceAmount1").Width = 100
            gv2.Columns("InvoiceAmount1").HeaderText = "Invoice Amount1"
            gv2.Columns("InvoiceAmount1").IsVisible = False

            gv2.Columns("Count").IsVisible = False
            gv2.Columns("grp").IsVisible = False
            'gv2.GroupDescriptors.Add(New GridGroupByExpression("grp as Item format ""{0}: {1}"" Group By grp"))

            Dim TotalClosing1 As New GridViewSummaryItem()
            TotalClosing1.FormatString = "{0:F2}"
            TotalClosing1.Name = "TaxableValue"
            TotalClosing1.AggregateExpression = "sum(TaxableValue1)"
            summaryRowItem.Add(TotalClosing1)

            Dim TotalClosing2 As New GridViewSummaryItem()
            TotalClosing2.FormatString = "{0:F2}"
            TotalClosing2.Name = "TaxableAmount"
            TotalClosing2.AggregateExpression = "sum(TaxableAmount1)"
            summaryRowItem.Add(TotalClosing2)

            Dim TotalClosing4 As New GridViewSummaryItem()
            TotalClosing4.FormatString = "{0:F2}"
            TotalClosing4.Name = "InvoiceAmount"
            TotalClosing4.AggregateExpression = "sum(InvoiceAmount1)"
            summaryRowItem.Add(TotalClosing4)

            Dim item3 As New GridViewSummaryItem("TaxableValue1", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("TaxableAmount1", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("InvoiceAmount1", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
        End If

        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv2.MasterTemplate.ShowTotals = True
    End Sub

    Sub FormatGridFooterDetail()

        gv3.TableElement.TableHeaderHeight = 25
        gv3.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv3.Columns.Count - 1
            gv3.Columns(ii).ReadOnly = True
            gv3.Columns(ii).IsVisible = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        If btnGSTR1.IsChecked = True And chkItemWise.Checked = False Then
            gv3.Columns("SNo").Width = 50
            gv3.Columns("SNo").HeaderText = "SI No."

            gv3.Columns("Particulars").Width = 350

            gv3.Columns("Taxable Amount").Width = 110
            gv3.Columns("Taxable Amount").HeaderText = "Tax Amount"


            Dim item3 As New GridViewSummaryItem("Taxable Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Taxable Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Round Off Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
        ElseIf btnGSTR1.IsChecked = True And chkItemWise.Checked = True Then
            ''richa GKD/05/02/19-000174 6 Feb,2019
            gv3.Columns("TotalTaxableValue").Width = 135
            gv3.Columns("TotalTaxableValue").HeaderText = "Total Taxable Amount"
            gv3.Columns("TotalTaxableValue").FormatString = "{0:F2}"

            gv3.Columns("TotalAmount").Width = 110
            gv3.Columns("TotalAmount").HeaderText = "Total Amount"
            gv3.Columns("TotalAmount").FormatString = "{0:F2}"

            gv3.Columns("TotalTaxAmount").Width = 110
            gv3.Columns("TotalTaxAmount").HeaderText = "Total Tax Amount"
            gv3.Columns("TotalTaxAmount").FormatString = "{0:F2}"

            gv3.Columns("CGSTAmount").Width = 110
            gv3.Columns("CGSTAmount").HeaderText = "CGST Amount"
            gv3.Columns("CGSTAmount").FormatString = "{0:F2}"

            gv3.Columns("SGSTAmount").Width = 110
            gv3.Columns("SGSTAmount").HeaderText = "SGST Amount"
            gv3.Columns("SGSTAmount").FormatString = "{0:F2}"

            gv3.Columns("IGSTAmount").Width = 110
            gv3.Columns("IGSTAmount").HeaderText = "IGST Amount"
            gv3.Columns("IGSTAmount").FormatString = "{0:F2}"

            gv3.Columns("UGSTAmount").Width = 110
            gv3.Columns("UGSTAmount").HeaderText = "UGST Amount"
            gv3.Columns("UGSTAmount").FormatString = "{0:F2}"

            Dim item3 As New GridViewSummaryItem("TotalAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("TotalTaxAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("CGSTAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("SGSTAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("IGSTAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("UGSTAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("TotalQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("TotalTaxableValue", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
        End If

        gv3.ShowGroupPanel = False
        gv3.MasterTemplate.AutoExpandGroups = True
        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub



    '========================Added by preeti Gupta =====================================
    Private Function BaseQry() As String
        Dim Basequery As String = Nothing

        'Basequery = "select Document_no as Document_no,max(Trans_type) as Trans_type,max(AgainstDocNo) as AgainstDoNo,max(Invoice_Entry_date) as Invoice_Entry_date ,max(Vendor_code) as Vendor_code,max(Loc_Code) as Loc_Code,max(Vendor_Group_Code) as Vendor_Group_Code,sum(TaxableValue) as TaxableValue,sum(TaxableAmount) as TaxableAmount,max(InvoiceAmount) as InvoiceAmount,max(RoundOFFAMount) as RoundOFFAMount,max(Against_POInvoice_No) as Against_POInvoice_No,max(Against_PurchaseReturn_No) as Against_PurchaseReturn_No,max(Against_BulkMillkPurchaseInvoice_No) as Against_BulkMillkPurchaseInvoice_No,max(Against_MillkPurchaseInvoice_No) as Against_MillkPurchaseInvoice_No,max(RefDocType) as RefDocType,max(RefDocNo) as RefDocNo,max(Document_Type) as Document_Type,max(GSTRegistered) as GSTRegistered,max(PI_Type) as PI_Type,max(Against_VCGL) as Against_VCGL,max(Against_Acquisition) as Against_Acquisition,max(Against_Asset_Work) as Against_Asset_Work,max(Against_VSPItemIssue_No) as Against_VSPItemIssue_No  from (" & _
        Basequery = " select TSPL_VENDOR_INVOICE_HEAD.Document_No as Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.GSTFinalNo ,TSPL_STATE_MASTER.STATE_CODE ,TSPL_STATE_MASTER.STATE_NAME ,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,tspl_vendor_master.Vendor_group_code," & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice' else " & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then 'Purchase Return' else" & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then 'Bulk Milk Purchase Invoice' else" & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then 'Milk Purchase Invoice'else" & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then 'VCGL'else" & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then 'Acquisition Entry'else" & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then 'Asset Work'else" & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then 'VSP Item Issue' else" & _
           " case when (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'') then 'Bulk Milk Purchase Return' else 'Direct AP'" & _
            " end end end  end end end end end end as Trans_Type," & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  else " & _
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  else" & _
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No else" & _
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No else" & _
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL else" & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition else" & _
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work else" & _
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then Against_VSPItemIssue_No else case when (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'') then TSPL_VENDOR_INVOICE_HEAD.RefDocNo else  TSPL_VENDOR_INVOICE_HEAD.Document_No " & _
              " end end end end end end end end end as AgainstDocNo," & _
               " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *  isnull(TSPL_VENDOR_INVOICE_detail.Amount_less_Discount,0)  as TaxableValue," & _
" case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " & _
                "   +case when  Tax2_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " & _
                 "  +case when  Tax3_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " & _
                 " +case when  Tax4_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " & _
                 "   +case when  Tax5_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " & _
                 "  ) as Exempt_Amt " & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " & _
                 "  +case when  Tax2_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " & _
                 "  +case when  Tax3_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " & _
                 " +case when  Tax4_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " & _
                  "  +case when  Tax5_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " & _
                  " ) as CGST_Amt" & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " & _
                  " +case when  Tax2_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " & _
                  " +case when  Tax3_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " & _
                  " +case when  Tax4_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " & _
                   " +case when  Tax5_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " & _
                  " ) as SGST_Amt" & _
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * (case when  Tax1_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " & _
                 "  +case when  Tax2_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " & _
                  " +case when  Tax3_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " & _
                 " +case when  Tax4_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " & _
                 "   +case when  Tax5_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " & _
                  " ) as UGST_Amt" & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " & _
                 "  +case when  Tax2_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " & _
                  " +case when  Tax3_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " & _
                 " +case when  Tax4_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " & _
                  "  +case when  Tax5_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " & _
                  " ) as IGST_Amt" & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " & _
                 "  +case when  Tax2_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_Rate,0)else 0 end " & _
                  " +case when  Tax3_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate,0)else 0 end " & _
                 " +case when  Tax4_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_Rate,0)else 0 end " & _
                  "  +case when  Tax5_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate,0)else 0 end " & _
                  " ) as Exempt_Rate " & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " & _
                 "  +case when  Tax2_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_Rate,0)else 0 end " & _
                 "  +case when  Tax3_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate,0)else 0 end " & _
                 " +case when  Tax4_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_Rate,0)else 0 end " & _
                  "  +case when  Tax5_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate,0)else 0 end " & _
                  " ) as CGST_Rate" & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " & _
                  " +case when  Tax2_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_Rate,0)else 0 end " & _
                  " +case when  Tax3_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate,0)else 0 end " & _
                 " +case when  Tax4_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_Rate,0)else 0 end " & _
                  "  +case when  Tax5_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate,0)else 0 end " & _
                  " ) as SGST_Rate" & _
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * (case when  Tax1_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " & _
                 "  +case when  Tax2_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_Rate,0)else 0 end " & _
                  " +case when  Tax3_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate,0)else 0 end " & _
                  " +case when  Tax4_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_Rate,0)else 0 end " & _
                  "  +case when  Tax5_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate,0)else 0 end " & _
                  " ) as UGST_Rate" & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " & _
                  " +case when  Tax2_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX2_Rate,0)else 0 end " & _
                  " +case when  Tax3_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate ,0)else 0 end " & _
                 " +case when  Tax4_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX4_Rate,0)else 0 end " & _
                 "   +case when  Tax5_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate ,0)else 0 end " & _
                  " ) as IGST_Rate," & _
 " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0)   as TaxableAmount, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0)  as InvoiceAmount," & _
            " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount" & _
            " ,Against_POInvoice_No,Against_PurchaseReturn_No,Against_BulkMillkPurchaseInvoice_No,Against_MillkPurchaseInvoice_No,RefDocType,RefDocNo,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_MASTER.GSTRegistered as GSTRegistered,TSPL_PI_HEAD.PI_Type,TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition,TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No" & _
             " from TSPL_VENDOR_INVOICE_HEAD " & _
                " left join TSPL_VENDOR_INVOICE_detail on TSPL_VENDOR_INVOICE_detail.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no" & _
                " left join tspl_item_master on tspl_item_master.item_code=TSPL_VENDOR_INVOICE_detail.item_code " & _
                " left join tspl_tax_master as Tax1_Code on  Tax1_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax1 " & _
                " left join tspl_tax_master as Tax2_Code on  Tax2_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax2" & _
                " left join tspl_tax_master as Tax3_Code on  Tax3_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax3" & _
                " left join tspl_tax_master as Tax4_Code on  Tax4_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax4" & _
                " left join tspl_tax_master as Tax5_Code on  Tax5_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax5" & _
                " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code " & _
                " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code " & _
                " left join TSPL_STATE_MASTER on  TSPL_STATE_MASTER.STATE_CODE =tspl_vendor_master.state_code" & _
                " left join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_group_code " & _
                " left join TSPL_PI_HEAD on TSPL_PI_HEAD.pi_no=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No"

        '" ) as pp group by Document_no "
        Basequery = " select * from (select Document_No ,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(Vendor_Code) as Vendor_Code,max(Vendor_Name) as Vendor_Name," & _
" max(GSTFinalNo) as GSTFinalNo, max(STATE_CODE ) as STATE_CODE,max(STATE_NAME) as STATE_NAME," & _
 " max(Loc_Code) as Loc_Code,max(Vendor_group_code) as Vendor_group_code,max(Trans_Type) as Trans_Type,max(AgainstDocNo) as AgainstDocNo,sum(TaxableValue ) as TaxableValue,sum(Exempt_Amt+CGST_Amt+SGST_Amt+UGST_Amt +IGST_Amt ) as TaxableAmount,sum(Exempt_Rate +CGST_Rate +SGST_Rate +UGST_Rate  +IGST_Rate ) as TaxableRate,sum(TaxableValue+Exempt_Amt+CGST_Amt+SGST_Amt+UGST_Amt +IGST_Amt) as InvoiceAmount,max(RoundOffAmount) as RoundOffAmount," & _
" max(Against_POInvoice_No) as Against_POInvoice_No,max(Against_PurchaseReturn_No) as Against_PurchaseReturn_No,max(Against_BulkMillkPurchaseInvoice_No) as Against_BulkMillkPurchaseInvoice_No,max(Against_MillkPurchaseInvoice_No) as Against_MillkPurchaseInvoice_No,max(RefDocType) as RefDocType,max(RefDocNo) as RefDocNo,max(Document_Type) as Document_Type,max(GSTRegistered) as GSTRegistered,max(PI_Type) as PI_Type,max(Against_VCGL) as Against_VCGL,max(Against_Acquisition) as Against_Acquisition,max(Against_Asset_Work) as Against_Asset_Work,max(Against_VSPItemIssue_No)  as Against_VSPItemIssue_No" & _
 " from (" & _
        " " & Basequery & " " & _
        " ) as Detail group by document_no  " & _
       " ) as qq "

        Return Basequery

    End Function
    Private Function UnionBaseQry() As String
        Dim UnionQry As String = Nothing

        UnionQry = " select '' as Document_no,'" & txtFromDate.Value & "' as Invoice_Entry_date,'' as Vendor_code,'' as Vendor_Name,'' as STATE_CODE,'' as STATE_NAME,'' as GSTFinalNo,'' as Loc_Code,'' as Vendor_Group_Code,'' as Trans_type,'' as AgainstDocNo,0 as TaxableValue,0 as TaxableRate,0 as TaxableAmount,0 as InvoiceAmount,0 as RoundOFFAMount" & _
            " ,'' as Against_POInvoice_No,'' as Against_PurchaseReturn_No,'' as Against_BulkMillkPurchaseInvoice_No,'' as Against_MillkPurchaseInvoice_No,'' as RefDocType,'' as RefDocNo,'' as Document_Type ,0 as GSTRegistered,'' as PI_Type,'' as Against_VCGL,'' as Against_Acquisition,'' as Against_Asset_Work,'' as Against_VSPItemIssue_No "

        Return UnionQry
    End Function

    Private Function Numberofvouchersfortheperiod() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and  convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103)  "

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If

        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 0 as count, 'Number of vouchers for the period' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" & _
            "" & strBaseQuery & "" & _
             " " & Wrcls & " " & _
            " union all" & _
               "" & strUnionBaseQuery & "" & _
                ") as xx"

        Return Qry
    End Function
    Private Function Includedinreturns() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing

        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103)  " & _
                " and (isnull(qq.Against_POInvoice_No,'')<>''" & _
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>''" & _
                " or isnull(qq.Against_BulkMillkPurchaseInvoice_No  ,'')<>''" & _
                 " or isnull(qq.Against_MillkPurchaseInvoice_No  ,'')<>''" & _
                 " or (isnull(qq.RefDocType ,'')='BM-PR' and isnull(qq.RefDocNo ,'')<>'')  )"


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If


        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()
        Qry = "select 1 as count, 'Included in returns' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" & _
            "" & strBaseQuery & "" & _
              " " & Wrcls & " " & _
            " union all" & _
               "" & strUnionBaseQuery & "" & _
           ") as xx"
        Return Qry
    End Function
    Private Function Invoicesreadyforreturns() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing

        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " & _
                " and (isnull(qq.Against_POInvoice_No,'')<>''" & _
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>''" & _
                " or isnull(qq.Against_BulkMillkPurchaseInvoice_No  ,'')<>''" & _
                 " or isnull(qq.Against_MillkPurchaseInvoice_No  ,'')<>''" & _
         " or (isnull(qq.RefDocType ,'')='BM-PR' and isnull(qq.RefDocNo ,'')<>'')  )"

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If


        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 2 as count,'Invoices ready for returns' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" & _
            "" & strBaseQuery & "" & _
             " " & Wrcls & " " & _
            " union all" & _
               "" & strUnionBaseQuery & "" & _
            ") as xx"
        Return Qry

    End Function
    Private Function Invoiceswithmismatchininformation()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 3 as count, 'Invoices with mismatch in information' as Type,0 as DocCount,* from ( " & _
            "" & strUnionBaseQuery & "" & _
        " ) as XX "
        Return qry
    End Function
    Private Function Notincludedinreturnsduetoincompleteinformation()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 4 as count, 'Not included in returns due to incomplete information' as Type,0 as DocCount,* from ( " & _
            "" & strUnionBaseQuery & "" & _
        " ) as XX "
        Return qry
    End Function
    Private Function Notrelevantforreturns()
        Dim qry As String = Nothing
        Dim strNumberofvouchersfortheperiod As String = Nothing
        strNumberofvouchersfortheperiod = Numberofvouchersfortheperiod()
        Dim strIncludedinreturns As String = Nothing
        strIncludedinreturns = Includedinreturns()


        qry = " select 5 as count,'Not relevant for returns' as Type,*" & _
            " from ( select max(DocCount) as DocCount,Document_no as Document_no,max(Trans_type) as Trans_type,max(AgainstDocNo ) as AgainstDocNo," & _
          " max(Invoice_Entry_date) as Invoice_Entry_date ,max(Vendor_code) as Vendor_code,  max(Loc_Code) as Loc_Code,max(Vendor_Group_Code) as Vendor_Group_Code," & _
          " sum(TaxableValue) as TaxableValue,sum(TaxableAmount) as TaxableAmount,sum(TaxableRate ) as TaxableRate, max(InvoiceAmount) as InvoiceAmount,max(RoundOFFAMount) as RoundOFFAMount," & _
           " max(Against_POInvoice_No) as Against_POInvoice_No, " & _
                 " max(Against_PurchaseReturn_No) as Against_PurchaseReturn_No,max(Against_BulkMillkPurchaseInvoice_No) as Against_BulkMillkPurchaseInvoice_No, " & _
   " max(Against_MillkPurchaseInvoice_No) as Against_MillkPurchaseInvoice_No,max(RefDocType) as RefDocType,max(RefDocNo) as RefDocNo,max(Document_Type) as Document_Type, " & _
   " max(GSTRegistered) as GSTRegistered,max(PI_Type) as PI_Type,max(Against_VCGL) as Against_VCGL,max(Against_Acquisition) as Against_Acquisition, " & _
   " max(Against_Asset_Work) as Against_Asset_Work,max(Against_VSPItemIssue_No) as Against_VSPItemIssue_No " & _
    " from ( " & _
            " select bb.*,1 as RI from ( " & _
        "" & strNumberofvouchersfortheperiod & "" & Environment.NewLine & _
        ") as bb" & _
        " Union all" & Environment.NewLine & _
         " select YY.*,-1 as RI from ( " & _
        "" & strIncludedinreturns & "" & _
        ")  as YY " & _
        ") as aa" & _
        " group by Document_no  having sum(RI)>0 and isnull(Document_no,'')<>''" & _
        " ) as xx "

        Return qry

    End Function
    Private Function IncompleteHSNSACinformation()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 6 as count, 'Incomplete HSN/SAC information (to be provided)' as Type,0 as DocCount,* from ( " & _
            "" & strUnionBaseQuery & "" & _
        " ) as XX "
        Return qry
    End Function
    Private Function GSTQryforHeader() As String
        Dim qry As String
        Dim StrNumberOfVouchersForthePeriod As String = Numberofvouchersfortheperiod()
        Dim StrIncludedinreturns As String = Includedinreturns()
        Dim StrInvoicesreadyforreturns As String = Invoicesreadyforreturns()
        Dim strInvoiceswithmismatchininformation As String = Invoiceswithmismatchininformation()
        Dim strNotincludedinreturnsduetoincompleteinformation As String = Notincludedinreturnsduetoincompleteinformation()
        Dim strNotrelevantforreturns As String = Notrelevantforreturns()
        Dim strIncompleteHSNSACinformation As String = IncompleteHSNSACinformation()

        qry = " select type as Name, sum(DocCount)-1 as Column1 " & _
        " from ( " & _
        " " & StrNumberOfVouchersForthePeriod & " " & Environment.NewLine & _
        " ) as aa  group by type,count" & _
        " Union all " & Environment.NewLine & _
        " select type as Name, sum(DocCount)-1 as Column1 " & _
        " from ( " & _
         " " & StrIncludedinreturns & " " & Environment.NewLine & _
          " ) as aa  group by type,count" & _
             " Union all " & Environment.NewLine & _
             " select type as Name, sum(DocCount)-1 as Column1 " & _
        " from ( " & _
         " " & StrInvoicesreadyforreturns & " " & Environment.NewLine & _
          " ) as aa  group by type,count" & _
          " Union all " & Environment.NewLine & _
          " select type as Name, sum(DocCount) as Column1 " & _
        " from ( " & _
         " " & strInvoiceswithmismatchininformation & " " & Environment.NewLine & _
          " ) as aa  group by type,count" & _
          " Union all " & Environment.NewLine & _
          " select type as Name, sum(DocCount) as Column1 " & _
        " from ( " & _
         " " & strNotincludedinreturnsduetoincompleteinformation & " " & Environment.NewLine & _
          " ) as aa  group by type,count" & _
          " Union all " & Environment.NewLine & _
          " select type as Name, sum(DocCount) as Column1 " & _
        " from ( " & _
         " " & strNotrelevantforreturns & " " & Environment.NewLine & _
          " ) as aa  group by type,count" & _
           " Union all " & Environment.NewLine & _
           " select type as Name, sum(DocCount) as Column1 " & _
        " from ( " & _
         " " & strIncompleteHSNSACinformation & " " & Environment.NewLine & _
          " ) as aa  group by type,count"


        Return qry
    End Function
    Sub drilldownHeaderforgv1()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If btnGSTR2.IsChecked = True Then
            If gv1.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Name").Value)
                Dim StrNumberOfVouchersForthePeriod As String = Nothing
                Dim StrIncludedinreturns As String = Nothing
                Dim StrInvoicesreadyforreturns As String = Nothing
                Dim strInvoiceswithmismatchininformation As String = Nothing
                Dim strNotincludedinreturnsduetoincompleteinformation As String = Nothing
                Dim strNotrelevantforreturns As String = Nothing
                Dim strIncompleteHSNSACinformation As String = Nothing

                StrNumberOfVouchersForthePeriod = Numberofvouchersfortheperiod()
                StrNumberOfVouchersForthePeriod = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & StrNumberOfVouchersForthePeriod & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                StrIncludedinreturns = Includedinreturns()
                StrIncludedinreturns = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & StrIncludedinreturns & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                StrInvoicesreadyforreturns = Invoicesreadyforreturns()
                StrInvoicesreadyforreturns = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & StrInvoicesreadyforreturns & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                strInvoiceswithmismatchininformation = Invoiceswithmismatchininformation()
                strInvoiceswithmismatchininformation = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strInvoiceswithmismatchininformation & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                strNotincludedinreturnsduetoincompleteinformation = Notincludedinreturnsduetoincompleteinformation()
                strNotincludedinreturnsduetoincompleteinformation = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strNotincludedinreturnsduetoincompleteinformation & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                strNotrelevantforreturns = Notrelevantforreturns()
                strNotrelevantforreturns = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strNotrelevantforreturns & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                strIncompleteHSNSACinformation = IncompleteHSNSACinformation()
                strIncompleteHSNSACinformation = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strIncompleteHSNSACinformation & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                Select Case strTransType
                    Case "Number of vouchers for the period"
                        qry = "" & StrNumberOfVouchersForthePeriod & ""
                    Case "Included in returns"
                        qry = "" & StrIncludedinreturns & ""
                    Case "Invoices ready for returns"
                        qry = "" & StrInvoicesreadyforreturns & ""
                    Case "Invoices with mismatch in information"
                        qry = "" & strInvoiceswithmismatchininformation & ""
                    Case "Not included in returns due to incomplete information"
                        qry = "" & strNotincludedinreturnsduetoincompleteinformation & ""
                    Case "Not relevant for returns"
                        qry = "" & strNotrelevantforreturns & ""
                    Case "Incomplete HSN/SAC information (to be provided)"
                        qry = "" & strIncompleteHSNSACinformation & ""
                End Select
                dt = clsDBFuncationality.GetDataTable(qry)
                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    gv3.DataSource = dt
                    RadPageView1.SelectedPage = RadPageViewPage3
                    gv3.BestFitColumns()
                    gv3.MasterTemplate.SummaryRowsBottom.Clear()
                    fromatGridDrillDownHeader()
                    RadPageViewPage3.Text = clsCommon.myCstr(gv1.CurrentRow.Cells("Name").Value)
                Else
                    RadPageViewPage3.Text = "Detail data"
                    Throw New Exception("No Data Found to Display")
                End If
                'gv3.DataSource = Nothing
                'gv3.Rows.Clear()
                'gv3.Columns.Clear()
                'gv3.DataSource = dt
                'gv3.GroupDescriptors.Clear()
                'gv3.MasterTemplate.SummaryRowsBottom.Clear()
                'RadPageView1.SelectedPage = RadPageViewPage3
                'RadPageViewPage3.Text = clsCommon.myCstr(gv1.CurrentRow.Cells("Name").Value)

                'gv3.BestFitColumns()
                'fromatGridDrillDownHeader()
            End If
        End If
    End Sub
    Sub drilldownHeaderforgv3()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        Dim strDoc As String = Nothing
        If btnGSTR2.IsChecked = True And chkItemWise.Checked = False Then
            If gv3.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv3.CurrentRow.Cells("Trans_Type").Value)
                If clsCommon.myLen(gv3.CurrentRow.Cells("AgainstDocNo").Value) > 0 Then
                    strDoc = clsCommon.myCstr(gv3.CurrentRow.Cells("AgainstDocNo").Value)
                End If


                Select Case strTransType
                    Case "Purchase Invoice"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strDoc)
                    Case "Purchase Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strDoc)
                    Case "Bulk Milk Purchase Invoice"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkPurchaseInvoice, strDoc)
                    Case "Milk Purchase Invoice"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, strDoc)
                    Case "VCGL"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, strDoc)
                    Case "Acquisition Entry"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FAAcquisitionEntry, strDoc)
                    Case "Asset Work"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FAAssetWork, strDoc)
                    Case "VSP Item Issue"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, strDoc)
                    Case "Bulk Milk Purchase Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, strDoc)
                    Case "Direct AP"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strDoc)
                End Select

            End If
        End If
    End Sub
    Sub fromatGridDrillDownHeader()
        gv3.TableElement.TableHeaderHeight = 25
        gv3.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv3.Columns.Count - 1
            gv3.Columns(ii).ReadOnly = True
            gv3.Columns(ii).IsVisible = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        If btnGSTR2.IsChecked = True AndAlso chkItemWise.Checked = False Then

            gv3.Columns("count").IsVisible = False
            gv3.Columns("DocCount").IsVisible = False
            gv3.Columns("Loc_Code").IsVisible = False
            'gv3.Columns("RoundOFFAMount").IsVisible = False

            gv3.Columns("Vendor_Group_Code").IsVisible = False
            gv3.Columns("Against_POInvoice_No").IsVisible = False
            gv3.Columns("Against_PurchaseReturn_No").IsVisible = False
            gv3.Columns("Against_BulkMillkPurchaseInvoice_No").IsVisible = False
            gv3.Columns("RefDocNo").IsVisible = False
            gv3.Columns("RefDocType").IsVisible = False
            gv3.Columns("Against_MillkPurchaseInvoice_No").IsVisible = False

            gv3.Columns("GSTRegistered").IsVisible = False
            gv3.Columns("PI_Type").IsVisible = False
            gv3.Columns("Against_VCGL").IsVisible = False
            gv3.Columns("Against_Acquisition").IsVisible = False
            gv3.Columns("Against_Asset_Work").IsVisible = False
            gv3.Columns("Against_VSPItemIssue_No").IsVisible = False


            gv3.Columns("Document_No").HeaderText = "Doc No"
            gv3.Columns("Trans_Type").HeaderText = "Against Doc Type"
            gv3.Columns("AgainstDocNo").HeaderText = "Against Doc No"
            gv3.Columns("Invoice_Entry_Date").HeaderText = "Doc Date"
            gv3.Columns("Vendor_Code").HeaderText = "Vendor Code"
            gv3.Columns("TaxableValue").HeaderText = "Taxable Value"
            gv3.Columns("TaxableAmount").HeaderText = "Total Tax"
            gv3.Columns("TaxableRate").HeaderText = "Total Rate"
            gv3.Columns("InvoiceAmount").HeaderText = "Document Total"

            Dim item2 As New GridViewSummaryItem("TaxableValue", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("TaxableAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("InvoiceAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("RoundOffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
        Else
            Dim item11 As New GridViewSummaryItem("Taxable Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item12 As New GridViewSummaryItem("CESS Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            Dim item13 As New GridViewSummaryItem("CGST Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item14 As New GridViewSummaryItem("SGST Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)
            Dim item15 As New GridViewSummaryItem("IGST Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)
            Dim item16 As New GridViewSummaryItem("UGST Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item16)
            Dim item17 As New GridViewSummaryItem("Total Tax", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item17)
            Dim item18 As New GridViewSummaryItem("Total Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item18)

        End If

        gv3.ShowGroupPanel = False
        gv3.MasterTemplate.AutoExpandGroups = True
        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub


    Private Function B2BInvoices34A() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim WrclsDirectAP As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing

        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and (qq.GSTRegistered=1)" & _
                " and (isnull(qq.Against_POInvoice_No,'')<>''" & _
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>'' ) and (isnull(qq.TaxableAmount,0) <>0 ) "

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If
        WrclsDirectAP = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and (qq.GSTRegistered=1 ) and (isnull(qq.TaxableAmount,0) <>0 )  and (qq.Document_Type in ('I') )" & _
                 " and isnull(qq.Against_POInvoice_No ,'')='' and " & _
                 "isnull(qq.Against_PurchaseReturn_No ,'')='' and " & _
                " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" & _
                 " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" & _
                 " isnull(qq.Against_VCGL ,'')='' and" & _
                 " isnull(qq.Against_Acquisition ,'')='' and " & _
                " isnull(qq.Against_Asset_Work ,'')='' and " & _
                 " isnull(qq.Against_VSPItemIssue_No ,'')='' " & _
                 "  and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end)  "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If

        strBaseQuery = BaseQry()

        strUnionBaseQuery = UnionBaseQry()


        Qry = "select 1 as count, 'To be reconciled with the GST portal' as grp, 'B2B Invoices - 3, 4A' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,final.* from (" & _
            " select * from ( " & _
            "" & strBaseQuery & "" & _
            " " & Wrcls & " " & _
             " ) as xx" & _
            " Union All" & _
             " select * from ( " & _
           "" & strBaseQuery & "" & _
              "" & WrclsDirectAP & "" & _
            " ) as xx" & _
             " union all" & _
               "" & strUnionBaseQuery & "" & _
            " ) as final"



        Return Qry
    End Function
    Private Function TaxablePurchases() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim WrclsDirectAP As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Dim strCreditDebitNotesRegular6C As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and (qq.GSTRegistered=1 )" & _
                " and (isnull(qq.Against_POInvoice_No,'')<>''" & _
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>'' ) and (isnull(qq.TaxableAmount,0) <>0) "

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If
        WrclsDirectAP = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and (qq.GSTRegistered=1 ) and (isnull(qq.TaxableAmount,0) <>0 )  and (qq.Document_Type in ('I') )" & _
                 " and isnull(qq.Against_POInvoice_No ,'')='' and " & _
                 "isnull(qq.Against_PurchaseReturn_No ,'')='' and " & _
                " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" & _
                 " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" & _
                 " isnull(qq.Against_VCGL ,'')='' and" & _
                 " isnull(qq.Against_Acquisition ,'')='' and " & _
                " isnull(qq.Against_Asset_Work ,'')='' and " & _
                 " isnull(qq.Against_VSPItemIssue_No ,'')='' " & _
                 "  and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end)  "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If

        strBaseQuery = BaseQry()
        strCreditDebitNotesRegular6C = CreditDebitNotesRegular6C()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 2 as count, 'To be reconciled with the GST portal' as grp, 'Taxable Purchases' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,final.* from (" & _
         " select * from ( " & _
         "" & strBaseQuery & "" & _
          " " & Wrcls & " " & _
          " ) as xx" & _
         " Union All" & _
          " select * from ( " & _
        "" & strBaseQuery & "" & _
          "" & WrclsDirectAP & "" & _
         " ) as xx" & _
          " union all" & _
               "" & strUnionBaseQuery & "" & _
         " ) as final"
        Return Qry
    End Function


    Private Function Reversechargesupplies()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()

        qry = "select 3 as count,'To be reconciled with the GST portal' as grp, 'Reverse charge supplies' as Type,0 as DocCount,* from ( " & _
            "" & strUnionBaseQuery & "" & _
        ") as xx"
        Return qry
    End Function
    Private Function CreditDebitNotesRegular6C()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and (qq.GSTRegistered=1 ) and  ( qq.Document_Type in ('C','D') ) and (isnull(qq.TaxableAmount,0) <>0 )" & _
                " and isnull(qq.Against_POInvoice_No ,'')='' and" & _
                " isnull(qq.Against_PurchaseReturn_No ,'')='' and " & _
                " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" & _
                 " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" & _
                 " isnull(qq.Against_VCGL ,'')='' and" & _
                 " isnull(qq.Against_Acquisition ,'')='' and " & _
                 " isnull(qq.Against_Asset_Work ,'')='' and " & _
                 " isnull(qq.Against_VSPItemIssue_No ,'')='' " & _
                 "   and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end) "



        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If


        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 4 as count, 'To be reconciled with the GST portal' as grp, 'Credit/Debit Notes Regular - 6C' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" & _
            "" & strBaseQuery & "" & _
             " " & Wrcls & " " & _
             " union all" & _
               "" & strUnionBaseQuery & "" & _
            ") as xx"

        Return Qry
    End Function


    Private Function B2BURInvoices4B() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Dim WrclsDirectAP As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and (qq.GSTRegistered=0 )and (isnull(qq.TaxableAmount,0) <>0 ) " & _
                " and (isnull(qq.Against_POInvoice_No,'')<>''" & _
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>'') and (qq.PI_Type<>'I' )"



        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If


        WrclsDirectAP = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and (qq.GSTRegistered=0 ) and (isnull(qq.TaxableAmount,0) <>0 )  and (qq.Document_Type in ('I') )" & _
                " and isnull(qq.Against_POInvoice_No ,'')='' and " & _
                "isnull(qq.Against_PurchaseReturn_No ,'')='' and " & _
               " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" & _
                " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" & _
                " isnull(qq.Against_VCGL ,'')='' and" & _
                " isnull(qq.Against_Acquisition ,'')='' and " & _
               " isnull(qq.Against_Asset_Work ,'')='' and " & _
                " isnull(qq.Against_VSPItemIssue_No ,'')='' " & _
                "   and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end) "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If

        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()


        Qry = "select 5 as count, 'To be uploaded on the GST portal' as grp, 'B2BUR Invoices - 4B' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,final.* from (" & _
           " select * from ( " & _
           "" & strBaseQuery & "" & _
            " " & Wrcls & " " & _
            " ) as xx" & _
             " Union All" & _
            " select * from ( " & _
          "" & strBaseQuery & "" & _
           "" & WrclsDirectAP & "" & _
            " union all" & _
               "" & strUnionBaseQuery & "" & _
           " ) as xx" & _
             " ) as final"

        Return Qry
    End Function

    Private Function ImportofServices4C()
        Dim qry As String = Nothing
        qry = "select 6 as count,'To be uploaded on the GST portal' as Grp, 'Import of Services - 4C' as Type,0 as DocCount,'' as Document_no,null as Invoice_Entry_date,'' as Vendor_code,'' as Loc_Code,'' as Vendor_Group_Code,'' as Trans_type,'' as AgainstDoNo,0 as TaxableValue,0 as TaxableAmount,0 as InvoiceAmount,0 as RoundOFFAMount " & _
                "  ,'' as Against_POInvoice_No,'' as Against_PurchaseReturn_No,'' as Against_BulkMillkPurchaseInvoice_No,'' as Against_MillkPurchaseInvoice_No,'' as RefDocType,'' as RefDocNo,'' as Document_Type,0 as GSTRegistered ,'' as PI_Type,'' as Against_VCGL,'' as Against_Acquisition,'' as Against_Asset_Work,'' as Against_VSPItemIssue_No"
        Return qry
    End Function
    Private Function ImportofGoods5()

        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and ( isnull(qq.TaxableAmount,0) <>0 ) and (qq.PI_Type='I' )" & _
                " and (isnull(qq.Against_POInvoice_No,'')<>'' )"



        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If


        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 7 as count, 'To be uploaded on the GST portal' as grp, 'Import of Goods - 5' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" & _
            "" & strBaseQuery & "" & _
             " " & Wrcls & " " & _
              " union all" & _
               "" & strUnionBaseQuery & "" & _
            ") as xx"

        Return Qry

    End Function

    Private Function CreditDebitNotesUnregistered6C()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and (qq.GSTRegistered=0 ) and (isnull(qq.TaxableAmount,0) <>0 )  and (qq.Document_Type in ('C','D') )" & _
                 " and isnull(qq.Against_POInvoice_No ,'')='' and " & _
                 "isnull(qq.Against_PurchaseReturn_No ,'')='' and " & _
                " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" & _
                 " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" & _
                 " isnull(qq.Against_VCGL ,'')='' and" & _
                 " isnull(qq.Against_Acquisition ,'')='' and " & _
                " isnull(qq.Against_Asset_Work ,'')='' and " & _
                 " isnull(qq.Against_VSPItemIssue_No ,'')='' " & _
                 " and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end)  "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If

        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 8 as count, 'To be uploaded on the GST portal' as grp, 'Credit/Debit Notes Unregistered - 6C' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" & _
            "" & strBaseQuery & "" & _
             " " & Wrcls & " " & _
               " union all" & _
               "" & strUnionBaseQuery & "" & _
            " ) as xx"


        Return Qry
    End Function


    Private Function NilRatedInvoices7Summary()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103)  and (isnull(qq.TaxableAmount,0) =0 ) "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If
        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()
        Qry = "select 9 as count, 'To be uploaded on the GST portal' as grp, 'Nil Rated Invoices - 7 - (Summary)' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" & _
          "" & strBaseQuery & "" & _
           " " & Wrcls & " " & _
               " union all" & _
               "" & strUnionBaseQuery & "" & _
          ") as xx"



        Return Qry
    End Function

    Private Function Composition()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 10 as count,'To be uploaded on the GST portal' as Grp, 'Composition' as Type,0 as DocCount,* from ( " & _
            "" & strUnionBaseQuery & "" & _
        ") as xx"
        Return qry
    End Function
    Private Function Exempt()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103)  and (isnull(qq.TaxableAmount,0) =0 ) " & _
                 " and ( isnull(qq.Against_MillkPurchaseInvoice_No ,'')<>'' or isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')<>'' or (isnull(qq.RefDocType ,'')='BM-PR' and isnull(qq.RefDocNo ,'')<>'') )"


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If
        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()
        Qry = "select 11 as count, 'To be uploaded on the GST portal' as grp, 'Exempt' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" & _
          "" & strBaseQuery & "" & _
            " " & Wrcls & " " & _
             " union all" & _
               "" & strUnionBaseQuery & "" & _
          ") as xx"



        Return Qry
    End Function

    Private Function NilRated()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strNilRatedInvoices7Summary As String = NilRatedInvoices7Summary()
        Dim strExempt As String = Exempt()

        'Wrcls = " WHERE 2=2 and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103)  and (isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) =0 ) "




        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        'End If
        'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
        '    Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        'End If
        'If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
        '    Wrcls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        'End If
        strBaseQuery = BaseQry()

        Qry = " select 12 as count, 'To be uploaded on the GST portal' as grp, 'Nil Rated' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount, " & _
   " Document_no as Document_no,max(Trans_type) as Trans_type,max(AgainstDocNo ) as AgainstDocNo,max(Invoice_Entry_date) as Invoice_Entry_date ,max(Vendor_code) as Vendor_code," & _
   " max(Loc_Code) as Loc_Code,max(Vendor_Group_Code) as Vendor_Group_Code,sum(TaxableValue) as TaxableValue,sum(TaxableAmount) as TaxableAmount,sum(TaxableRate ) as TaxableRate," & _
   " max(InvoiceAmount) as InvoiceAmount,max(RoundOFFAMount) as RoundOFFAMount,max(Against_POInvoice_No) as Against_POInvoice_No," & _
   " max(Against_PurchaseReturn_No) as Against_PurchaseReturn_No,max(Against_BulkMillkPurchaseInvoice_No) as Against_BulkMillkPurchaseInvoice_No," & _
  "  max(Against_MillkPurchaseInvoice_No) as Against_MillkPurchaseInvoice_No,max(RefDocType) as RefDocType,max(RefDocNo) as RefDocNo,max(Document_Type) as Document_Type," & _
   " max(GSTRegistered) as GSTRegistered,max(PI_Type) as PI_Type,max(Against_VCGL) as Against_VCGL,max(Against_Acquisition) as Against_Acquisition," & _
   " max(Against_Asset_Work) as Against_Asset_Work,max(Against_VSPItemIssue_No) as Against_VSPItemIssue_No  from (" & _
    " select *,1 as RI from (" & _
        "" & strNilRatedInvoices7Summary & "" & _
        ") as aa" & _
        " union all" & _
        " select *,-1 As RI from (" & _
         "" & Exempt() & "" & _
              ") as bb" & _
              " ) as final group by Document_no having sum(RI)>0 or isnull(max(Document_no),'') =''"


        Return Qry

    End Function

    Private Function NonGST()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 13 as count, 'To be uploaded on the GST portal' as Grp,'Non GST' as Type,0 as DocCount,* from ( " & _
            "" & strUnionBaseQuery & "" & _
        ") as xx"
        Return qry
    End Function
    Private Function AdvancePaid10ASummary()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 14 as count,'To be uploaded on the GST portal' as Grp, 'Advance Paid -10A - (Summary)' as Type,0 as DocCount,* from ( " & _
            "" & strUnionBaseQuery & "" & _
        ") as xx"
        Return qry
    End Function
    Private Function AdjustmentofAdvance10BSummary()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 15 as count,'To be uploaded on the GST portal' as Grp, 'Adjustment of Advance - 10B - (Summary)' as Type,0 as DocCount,* from ( " & _
            "" & strUnionBaseQuery & "" & _
        ") as xx"
        Return qry
    End Function

    Private Function GSTQryforDetail() As String
        Dim qry As String
        Dim StrB2BInvoices34A As String = B2BInvoices34A()
        Dim StrtaxablePurchase As String = TaxablePurchases()
        Dim StrReversechargesupplies As String = Reversechargesupplies()
        Dim strCreditDebitNotesRegular6C As String = CreditDebitNotesRegular6C()
        Dim strB2BURInvoices4B As String = B2BURInvoices4B()
        Dim strImportofServices4C As String = ImportofServices4C()
        Dim strImportofGoods5 As String = ImportofGoods5()
        Dim strCreditDebitNotesUnregistered6C As String = CreditDebitNotesUnregistered6C()
        Dim strNilRatedInvoices7Summary As String = NilRatedInvoices7Summary()
        Dim strComposition As String = Composition()
        Dim strExempt As String = Exempt()
        Dim strNilRated As String = NilRated()
        Dim strNonGST As String = NonGST()
        Dim strAdvancePaid10ASummary As String = AdvancePaid10ASummary()
        Dim strAdjustmentofAdvance10BSummary As String = AdjustmentofAdvance10BSummary()

        qry = " select count,type as Name,max(grp) as grp, sum(TaxableValue) as TaxableValue,sum(TaxableAmount) as TaxableAmount,sum(InvoiceAmount) as InvoiceAmount, sum(TaxableValue1) as TaxableValue1,sum(TaxableAmount1) as TaxableAmount1,sum(InvoiceAmount1) as InvoiceAmount1 " & _
        " from ( " & _
        " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & StrB2BInvoices34A & ") as aa " & Environment.NewLine & _
        " Union all " & Environment.NewLine & _
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,0 as TaxableValue1,0 as TaxableAmount1,0 as InvoiceAmount1 from (" & StrtaxablePurchase & ") as aa " & Environment.NewLine & _
             " Union all " & Environment.NewLine & _
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & StrReversechargesupplies & ") as aa " & Environment.NewLine & _
          " Union all " & Environment.NewLine & _
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & strCreditDebitNotesRegular6C & ") as aa " & Environment.NewLine & _
          " Union all " & Environment.NewLine & _
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strB2BURInvoices4B & " ) as aa" & Environment.NewLine & _
          " Union all " & Environment.NewLine & _
         " select  count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strImportofServices4C & " ) as aa" & Environment.NewLine & _
           " Union all " & Environment.NewLine & _
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & strImportofGoods5 & ") as aa " & Environment.NewLine & _
           " Union all " & Environment.NewLine & _
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & strCreditDebitNotesUnregistered6C & ") as aa " & Environment.NewLine & _
           " Union all " & Environment.NewLine & _
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strNilRatedInvoices7Summary & ") as aa " & Environment.NewLine & _
           " Union all " & Environment.NewLine & _
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strComposition & ") as aa " & Environment.NewLine & _
           " Union all " & Environment.NewLine & _
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,0 as TaxableValue1,0 as TaxableAmount1,0 as InvoiceAmount1 from ( " & strExempt & ") as aa " & Environment.NewLine & _
           " Union all " & Environment.NewLine & _
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,0 as TaxableValue1,0 as TaxableAmount1,0 as InvoiceAmount1 from (" & strNilRated & ") as aa " & Environment.NewLine & _
                " Union all " & Environment.NewLine & _
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strNonGST & ") as aa " & Environment.NewLine & _
             " Union all " & Environment.NewLine & _
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strAdvancePaid10ASummary & ") as aa " & Environment.NewLine & _
            " Union all " & Environment.NewLine & _
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & strAdjustmentofAdvance10BSummary & ") as aa " & Environment.NewLine & _
        " ) as Header  group by type,count order by count "

        Return qry
    End Function


    Sub drilldownDetailforgv2()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If btnGSTR2.IsChecked = True Then
            If gv2.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value)
                '========================
                Dim StrB2BInvoices34A As String = Nothing
                Dim StrtaxablePurchase As String = Nothing
                Dim StrReversechargesupplies As String = Nothing
                Dim strCreditDebitNotesRegular6C As String = Nothing
                Dim strB2BURInvoices4B As String = Nothing
                Dim strImportofServices4C As String = Nothing
                Dim strImportofGoods5 As String = Nothing
                Dim strCreditDebitNotesUnregistered6C As String = Nothing
                Dim strNilRatedInvoices7Summary As String = Nothing
                Dim strComposition As String = Nothing
                Dim strExempt As String = Nothing
                Dim strNilRated As String = Nothing
                Dim strNonGST As String = Nothing
                Dim strAdvancePaid10ASummary As String = Nothing
                Dim strAdjustmentofAdvance10BSummary As String = Nothing

                If chkItemWise.Checked = False Then
                    StrB2BInvoices34A = B2BInvoices34A()
                    StrB2BInvoices34A = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & StrB2BInvoices34A & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    StrtaxablePurchase = TaxablePurchases()
                    StrtaxablePurchase = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & StrtaxablePurchase & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    StrReversechargesupplies = Reversechargesupplies()
                    StrReversechargesupplies = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & StrReversechargesupplies & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strCreditDebitNotesRegular6C = CreditDebitNotesRegular6C()
                    strCreditDebitNotesRegular6C = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strCreditDebitNotesRegular6C & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strB2BURInvoices4B = B2BURInvoices4B()
                    strB2BURInvoices4B = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strB2BURInvoices4B & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strImportofServices4C = ImportofServices4C()
                    strImportofServices4C = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strImportofServices4C & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strImportofGoods5 = ImportofGoods5()
                    strImportofGoods5 = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strImportofGoods5 & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strCreditDebitNotesUnregistered6C = CreditDebitNotesUnregistered6C()
                    strCreditDebitNotesUnregistered6C = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strCreditDebitNotesUnregistered6C & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strNilRatedInvoices7Summary = NilRatedInvoices7Summary()
                    strNilRatedInvoices7Summary = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strNilRatedInvoices7Summary & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strComposition = Composition()
                    strComposition = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strComposition & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strExempt = Exempt()
                    strExempt = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strExempt & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strNilRated = NilRated()
                    strNilRated = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strNilRated & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strNonGST = NonGST()
                    strNonGST = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strNonGST & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strAdvancePaid10ASummary = AdvancePaid10ASummary()
                    strAdvancePaid10ASummary = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strAdvancePaid10ASummary & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                    strAdjustmentofAdvance10BSummary = AdjustmentofAdvance10BSummary()
                    strAdjustmentofAdvance10BSummary = "select row_number() over (order by Invoice_Entry_Date ,Document_no ) as SNO,* from (" & strAdjustmentofAdvance10BSummary & ") as xx where isnull(Document_no,'')<>'' order by Invoice_Entry_Date "
                Else
                    StrB2BInvoices34A = ItemWiseB2BInvoices34A()
                    StrtaxablePurchase = ItemWiseTaxablePurchases()
                    StrReversechargesupplies = ItemWiseReversechargesupplies() 'No
                    strCreditDebitNotesRegular6C = ItemWiseCreditDebitNotesRegular6C()
                    strB2BURInvoices4B = ItemWiseB2BURInvoices4B()
                    strImportofServices4C = ItemWiseReversechargesupplies() 'no
                    strImportofGoods5 = ItemWiseImportofGoods5()
                    strCreditDebitNotesUnregistered6C = ItemWiseCreditDebitNotesUnregistered6C()
                    strNilRatedInvoices7Summary = ItemWiseNilRatedInvoices7Summary()
                    strExempt = ItemWiseExempt()
                    strNilRated = ItemWiseNillRated()

                    strComposition = ItemWiseReversechargesupplies() 'no
                    strNonGST = ItemWiseReversechargesupplies()
                    strAdvancePaid10ASummary = ItemWiseReversechargesupplies()
                    strAdjustmentofAdvance10BSummary = ItemWiseReversechargesupplies()
                End If

                Select Case strTransType
                    Case "B2B Invoices - 3, 4A"
                        qry = "" & StrB2BInvoices34A & ""
                    Case "Taxable Purchases"
                        qry = "" & StrtaxablePurchase & ""
                    Case "Reverse charge supplies"
                        qry = "" & StrReversechargesupplies & ""
                    Case "Credit/Debit Notes Regular - 6C"
                        qry = "" & strCreditDebitNotesRegular6C & ""
                    Case "B2BUR Invoices - 4B"
                        qry = "" & strB2BURInvoices4B & ""
                    Case "Import of Services - 4C"
                        qry = "" & strImportofServices4C & ""
                    Case "Import of Goods - 5"
                        qry = "" & strImportofGoods5 & ""
                    Case "Credit/Debit Notes Unregistered - 6C"
                        qry = "" & strCreditDebitNotesUnregistered6C & ""
                    Case "Nil Rated Invoices - 7 - (Summary)"
                        qry = "" & strNilRatedInvoices7Summary & ""
                    Case "Composition"
                        qry = "" & strComposition & ""
                    Case "Exempt"
                        qry = "" & strExempt & ""
                    Case "Nil Rated"
                        qry = "" & strNilRated & ""
                    Case "Exempt"
                        qry = "" & strExempt & ""
                    Case "Non GST"
                        qry = "" & strNonGST & ""
                    Case "Advance Paid -10A - (Summary)"
                        qry = "" & strAdvancePaid10ASummary & ""
                    Case "Adjustment of Advance - 10B - (Summary)"
                        qry = "" & strAdjustmentofAdvance10BSummary & ""
                End Select
                dt = clsDBFuncationality.GetDataTable(qry)

                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.SummaryRowsBottom.Clear()
                gv3.MasterView.Refresh()

                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    gv3.DataSource = dt
                    RadPageView1.SelectedPage = RadPageViewPage3
                    gv3.BestFitColumns()
                    gv3.EnableFiltering = True
                    fromatGridDrillDownHeader()

                    RadPageViewPage3.Text = clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value)
                Else
                    RadPageViewPage3.Text = "Detail data"
                    Throw New Exception("No Data Found to Display")
                End If
            End If
        End If
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        funPrint()

    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        Try
            If btnGSTR1.IsChecked = True Then
                DrillDownHeaderForGSTR1()
            Else
                drilldownHeaderforgv1()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv3_DoubleClick(sender As Object, e As EventArgs) Handles gv3.DoubleClick
        drilldownHeaderforgv3()
    End Sub

    Private Sub gv2_DoubleClick(sender As Object, e As EventArgs) Handles gv2.DoubleClick
        Try
            If btnGSTR1.IsChecked = True And chkItemWise.Checked = True Then
                DrillDownDetailForGSTR1_ItemWise()
            ElseIf btnGSTR1.IsChecked = True And chkItemWise.Checked = False Then
                DrillDownDetailForGSTR1()
            ElseIf btnGSTR2.IsChecked = True Then
                drilldownDetailforgv2()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    ''richa agarwal 29 Nov,2018 ALF/26/11/18-000089
    Function getBaseQueryForItemWise_GSTR1(ByVal strWhrCls As String, ByVal trantype As String) As String
        Dim BaseQry As String = String.Empty
        Try
            ''richa GKD/05/02/19-000174 6 Feb,2019
            ''BaseQry = " select FinalQuery .Item_Code,max(tspl_item_master.item_Desc) as Description,max(tspl_item_master.HSN_Code) as [HSN Code] ,FinalQuery.UOM ,SUM(FinalQuery.TotalQty) as TotalQty,sum(FinalQuery.TotalAmount) as TotalAmount,sum(FinalQuery.TotalTaxAmount) as TotalTaxAmount,sum(FinalQuery.CGSTAmount) as CGSTAmount,sum(FinalQuery.SGSTAmount) as SGSTAmount,sum(FinalQuery.IGSTAmount) as IGSTAmount,sum(FinalQuery.UGSTAmount) as UGSTAmount " & Environment.NewLine & _

            BaseQry = "  select FinalQuery .Item_Code,max(tspl_item_master.item_Desc) as Description,max(tspl_item_master.HSN_Code) as [HSN Code] ,FinalQuery.UOM ,SUM(FinalQuery.TotalQty) as TotalQty,(sum(FinalQuery.TotalAmount)-sum(FinalQuery.TotalTaxAmount)) as TotalTaxableValue,sum(FinalQuery.TotalTaxAmount) as TotalTaxAmount,sum(FinalQuery.CGSTAmount) as CGSTAmount,sum(FinalQuery.SGSTAmount) as SGSTAmount,sum(FinalQuery.IGSTAmount) as IGSTAmount,sum(FinalQuery.UGSTAmount) as UGSTAmount ,sum(FinalQuery.TotalAmount) as TotalAmount " & Environment.NewLine & _
               " from ( " & Environment.NewLine
            If clsCommon.CompairString(trantype, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Or clsCommon.CompairString(trantype, "Credit/Debit Notes(Unregistered) - 9B") = CompairStringResult.Equal Or clsCommon.CompairString(trantype, "Credit/Debit Notes(Registered) - 9B") = CompairStringResult.Equal Then
                BaseQry += "---------------- Direct AR Invoice-------------------" & Environment.NewLine & _
                " select DirectARInvoice.Item_Code ,DirectARInvoice.UOM ,SUM(DirectARInvoice.Qty) as TotalQty,sum(DirectARInvoice.Item_Net_Amt) as TotalAmount,sum(DirectARInvoice.Total_Tax_Amt) as TotalTaxAmount,sum(DirectARInvoice.CGSTAmount) as CGSTAmount,sum(DirectARInvoice.SGSTAmount) as SGSTAmount,sum(DirectARInvoice.IGSTAmount) as IGSTAmount,sum(DirectARInvoice.UGSTAmount) as UGSTAmount  from (" & Environment.NewLine & _
                " select TSPL_Customer_Invoice_Detail.Document_No,'' AS Item_Code ,0 AS Qty ,'' as UOM,TSPL_Customer_Invoice_Head.Document_Type," & Environment.NewLine & _
                " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Amount ,0))" & Environment.NewLine & _
                " as Item_Net_Amt ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Tax  ,0))" & Environment.NewLine & _
                " as Total_Tax_Amt,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as CGSTAmount," & Environment.NewLine & _
                " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as SGSTAmount ," & Environment.NewLine & _
                " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as IGSTAmount ," & Environment.NewLine & _
                " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine & _
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as UGSTAmount from TSPL_Customer_Invoice_Detail" & Environment.NewLine & _
                " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Document_No =TSPL_Customer_Invoice_Detail.Document_No " & Environment.NewLine & _
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine & _
                " where (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) and TSPL_Customer_INVOICE_HEAD.Status =1 " & strWhrCls & "" & Environment.NewLine & _
                " ) DirectARInvoice " & Environment.NewLine & _
                " group by  DirectARInvoice.Item_Code ,DirectARInvoice.UOM " & Environment.NewLine
                If clsCommon.CompairString(trantype, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Then
                    BaseQry += "   union all  " & Environment.NewLine
                End If
            End If
            If clsCommon.CompairString(trantype, "Credit/Debit Notes(Unregistered) - 9B") <> CompairStringResult.Equal And clsCommon.CompairString(trantype, "Credit/Debit Notes(Registered) - 9B") <> CompairStringResult.Equal Then
                BaseQry += " ---------------- Sale Invoice ,BULK SALE,CAN SALE--------------------- " & Environment.NewLine & _
                   " select saleInvoice.Item_Code ,saleInvoice.UOM ,SUM(saleInvoice.Qty) as TotalQty,sum(saleInvoice.Item_Net_Amt) as TotalAmount,sum(saleInvoice.Total_Tax_Amt) as TotalTaxAmount,sum(saleInvoice.CGSTAmount) as CGSTAmount,sum(saleInvoice.SGSTAmount) as SGSTAmount,sum(saleInvoice.IGSTAmount) as IGSTAmount,sum(saleInvoice.UGSTAmount) as UGSTAmount  from (select TSPL_SD_SALE_INVOICE_DETAIL.Document_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt, case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end  " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine & _
                   " case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end as UGSTAmount from TSPL_SD_SALE_INVOICE_DETAIL " & Environment.NewLine & _
                   " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.Document_Code  " & Environment.NewLine & _
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine & _
                   " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER .Location_Code  " & Environment.NewLine & _
                   " where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' " & strWhrCls & ") and TSPL_SD_SALE_INVOICE_HEAD.Status =1 " & Environment.NewLine & _
                   " union all  " & Environment.NewLine & _
                   " select TSPL_CANSALE_INVOICE_detail.Document_No,TSPL_CANSALE_INVOICE_detail.ItemCode ,TSPL_CANSALE_INVOICE_detail.Qty ,TSPL_CANSALE_INVOICE_detail.UOM ,TSPL_CANSALE_INVOICE_detail.TotalAmount ,0 as Total_Tax_Amt,0 AS CGSTAmount,0 AS SGSTAmount ,0 AS IGSTAmount,0 AS UGSTAmount from TSPL_CANSALE_INVOICE_detail " & Environment.NewLine & _
                   " left outer join TSPL_CANSALE_INVOICE_HEAD on TSPL_CANSALE_INVOICE_HEAD .Document_no =TSPL_CANSALE_INVOICE_detail.Document_No " & Environment.NewLine & _
                   " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_CANSALE_INVOICE_HEAD.Document_No " & Environment.NewLine & _
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CANSALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine & _
                   " where TSPL_CANSALE_INVOICE_HEAD.Posted =1 and TSPL_CANSALE_INVOICE_HEAD.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' " & strWhrCls & ") " & Environment.NewLine & _
                   " union all " & Environment.NewLine & _
                   " select TSPL_INVOICE_DETAIL_BULKSALE.Document_No,TSPL_INVOICE_DETAIL_BULKSALE.item_code,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Qty,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code as UOM,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount ,0 as Total_Tax_Amt,0 AS CGSTAmount,0 AS SGSTAmount ,0 AS IGSTAmount,0 AS UGSTAmount  from TSPL_INVOICE_DETAIL_BULKSALE " & Environment.NewLine & _
                   " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE .Document_no =TSPL_INVOICE_DETAIL_BULKSALE.Document_No  " & Environment.NewLine & _
                   " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No " & Environment.NewLine & _
                   " where TSPL_INVOICE_MASTER_BULKSALE.Posted =1 and TSPL_INVOICE_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' " & strWhrCls & ") " & Environment.NewLine & _
                   " ) saleInvoice  " & Environment.NewLine & _
                   " group by  saleInvoice.Item_Code ,saleInvoice.UOM " & Environment.NewLine & _
                   "   union all  " & Environment.NewLine & _
                   " ---------------- Sale RETURN ,CAN SALE RETURN----------------------- " & Environment.NewLine & _
                   " select saleRETURN.Item_Code ,saleRETURN.UOM ,SUM(saleRETURN.Qty) * -1  as TotalQty,sum(saleRETURN.Item_Net_Amt) * -1  as TotalAmount,sum(saleRETURN.Total_Tax_Amt) * -1  as TotalTaxAmount ,sum(saleRETURN.CGSTAmount) * -1  as CGSTAmount,sum(saleRETURN.SGSTAmount)  * -1  as SGSTAmount,sum(saleRETURN.IGSTAmount) * -1  as IGSTAmount,sum(saleRETURN.UGSTAmount)  * -1  as UGSTAmount from ( " & Environment.NewLine & _
                   " select  " & Environment.NewLine & _
                   " TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,TSPL_SD_SALE_RETURN_DETAIL.Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine & _
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine & _
                   "case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as UGSTAmount " & Environment.NewLine & _
                   " from TSPL_SD_SALE_RETURN_DETAIL " & Environment.NewLine & _
                   " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  " & Environment.NewLine & _
                   " where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''  " & strWhrCls & ") and TSPL_SD_SALE_RETURN_HEAD.Status =1 " & Environment.NewLine & _
                   " union all " & Environment.NewLine & _
                   " select TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No,TSPL_SALE_RETURN_DETAIL_BULKSALE.item_code,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as Qty,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code as UOM,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount ,0 as Total_Tax_Amt,0 AS CGSTAmount,0 AS SGSTAmount ,0 AS IGSTAmount,0 AS UGSTAmount  from TSPL_SALE_RETURN_DETAIL_BULKSALE " & Environment.NewLine & _
                   " left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE .Document_no =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No  " & Environment.NewLine & _
                   " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_Return_No =TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No " & Environment.NewLine & _
                   " where TSPL_SALE_RETURN_MASTER_BULKSALE.Posted =1 AND TSPL_SALE_RETURN_MASTER_BULKSALE.AGAINST='Bulk Invoice' " & Environment.NewLine & _
                   " and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''  " & strWhrCls & ") " & Environment.NewLine & _
                   " ) saleRETURN " & Environment.NewLine & _
                   " group by  saleRETURN.Item_Code ,saleRETURN.UOM " & Environment.NewLine & _
                   " union all " & Environment.NewLine & _
                   "---------------- Scrap Sale Return---------------------- " & Environment.NewLine & _
                   " select ScrapsaleReturN.Item_Code ,ScrapsaleReturN.UOM ,SUM(ScrapsaleReturN.Qty) * -1 as TotalQty,sum(ScrapsaleReturN.Item_Net_Amt) * -1 as TotalAmount,sum(ScrapsaleReturN.Total_Tax_Amt) * -1 as TotalTaxAmount,sum(ScrapsaleReturN.CGSTAmount) * -1 as CGSTAmount,sum(ScrapsaleReturN.SGSTAmount) * -1 as SGSTAmount,sum(ScrapsaleReturN.IGSTAmount) * -1 as IGSTAmount,sum(ScrapsaleReturN.UGSTAmount) * -1 as UGSTAmount  from (select TSPL_SCRAPSALE_DETAIL_RETURN.Document_No,TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code ,TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty AS Qty ,TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code as UOM,TSPL_SCRAPSALE_DETAIL_RETURN.TotalAmt as Item_Net_Amt ,TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt as Total_Tax_Amt, case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine & _
                   " case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt else 0 end  " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt else 0 end as UGSTAmount from TSPL_SCRAPSALE_DETAIL_RETURN " & Environment.NewLine & _
                   " left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN .Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No  " & Environment.NewLine & _
                   " where TSPL_SCRAPSALE_DETAIL_RETURN.Document_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>''  " & strWhrCls & ") and TSPL_SCRAPSALE_HEAD_RETURN.ispost =1  " & Environment.NewLine & _
                   " ) ScrapsaleReturN  " & Environment.NewLine & _
                   " group by  ScrapsaleReturN.Item_Code ,ScrapsaleReturN.UOM " & Environment.NewLine & _
                   " union all  " & Environment.NewLine & _
                   " ---------------- Scrap Sale ----------------- " & Environment.NewLine & _
                   " select Scrapsale.Item_Code ,Scrapsale.UOM ,SUM(Scrapsale.Qty) as TotalQty,sum(Scrapsale.Item_Net_Amt) as TotalAmount,sum(Scrapsale.Total_Tax_Amt) as TotalTaxAmount,sum(Scrapsale.CGSTAmount) as CGSTAmount,sum(Scrapsale.SGSTAmount) as SGSTAmount,sum(Scrapsale.IGSTAmount) as IGSTAmount,sum(Scrapsale.UGSTAmount) as UGSTAmount  from ( " & Environment.NewLine & _
                   " select TSPL_SCRAPINVOICE_DETAIL.invoice_No AS Document_No " & Environment.NewLine & _
                   " ,TSPL_SCRAPINVOICE_DETAIL.Item_Code ,TSPL_SCRAPINVOICE_DETAIL.shipped_Qty AS Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code as UOM,TSPL_SCRAPINVOICE_DETAIL.TotalAmt as Item_Net_Amt ,TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt as Total_Tax_Amt, case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine & _
                   " case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt else 0 end as UGSTAmount " & Environment.NewLine & _
                   " from TSPL_SCRAPINVOICE_DETAIL " & Environment.NewLine & _
                   " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No  " & Environment.NewLine & _
                   " where TSPL_SCRAPINVOICE_DETAIL.invoice_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrap  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')<>''  " & strWhrCls & ") and TSPL_SCRAPINVOICE_HEAD.ispost =1  " & Environment.NewLine & _
                   " ) Scrapsale" & Environment.NewLine & _
                   " group by  Scrapsale.Item_Code ,Scrapsale.UOM " & Environment.NewLine & _
                   " union all " & Environment.NewLine & _
                   " ---------------- mcc Material Sale RETURN-------------------------------- " & Environment.NewLine & _
                   " select MCCMaterialsaleRETURN.Item_Code ,MCCMaterialsaleRETURN.UOM ,SUM(MCCMaterialsaleRETURN.Qty) * -1  as TotalQty,sum(MCCMaterialsaleRETURN.Item_Net_Amt) * -1  as TotalAmount,sum(MCCMaterialsaleRETURN.Total_Tax_Amt) * -1  as TotalTaxAmount ,sum(MCCMaterialsaleRETURN.CGSTAmount) * -1  as CGSTAmount,sum(MCCMaterialsaleRETURN.SGSTAmount)  * -1  as SGSTAmount,sum(MCCMaterialsaleRETURN.IGSTAmount) * -1  as IGSTAmount,sum(MCCMaterialsaleRETURN.UGSTAmount)  * -1  as UGSTAmount from ( " & Environment.NewLine & _
                   " select  " & Environment.NewLine & _
                   " TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,TSPL_SD_SALE_RETURN_DETAIL.Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine & _
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine & _
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as UGSTAmount " & Environment.NewLine & _
                   " from TSPL_SD_SALE_RETURN_DETAIL " & Environment.NewLine & _
                   " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  " & Environment.NewLine & _
                   " where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return ,'')<>''  " & strWhrCls & ") and TSPL_SD_SALE_RETURN_HEAD.Status =1 AND TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' " & Environment.NewLine & _
                   " ) MCCMaterialsaleRETURN " & Environment.NewLine & _
                   " group by  MCCMaterialsaleRETURN.Item_Code ,MCCMaterialsaleRETURN.UOM " & Environment.NewLine & _
                   " union all " & Environment.NewLine & _
                   " ---------------- VCGL---------------------------- " & Environment.NewLine & _
                    " select VCGL.Item_Code ,VCGL.UOM ,SUM(VCGL.Qty) as TotalQty,sum(VCGL.Item_Net_Amt)   as TotalAmount,sum(VCGL.Total_Tax_Amt)  as TotalTaxAmount ,sum(VCGL.CGSTAmount)  as CGSTAmount,sum(VCGL.SGSTAmount)   as SGSTAmount,sum(VCGL.IGSTAmount)  as IGSTAmount,sum(VCGL.UGSTAmount)   as UGSTAmount from (  " & Environment.NewLine & _
                    " select TSPL_VCGL_Detail.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Detail.Row_Type ='Customer' then TSPL_VCGL_Detail.Dr_Amount-TSPL_VCGL_Detail.Cr_Amount else  " & Environment.NewLine & _
                    " 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, 0 as CGSTAmount,  " & Environment.NewLine & _
                    " 0 as SGSTAmount ,0 as IGSTAmount ,0 as UGSTAmount  " & Environment.NewLine & _
                    " from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  " & Environment.NewLine & _
                    " where TSPL_VCGL_Detail.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''   " & strWhrCls & ") and TSPL_VCGL_Head.Status =1  " & Environment.NewLine & _
                    " union all " & Environment.NewLine & _
                    " select   " & Environment.NewLine & _
                    " TSPL_VCGL_Head.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Head.Document_Type  ='C' then TSPL_VCGL_Head.Tot_Cr_Amount-TSPL_VCGL_Head.Tot_Dr_Amount else " & Environment.NewLine & _
                    " 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, 0 as CGSTAmount,  " & Environment.NewLine & _
                    " 0 as SGSTAmount ,0 as IGSTAmount ,0 as UGSTAmount  " & Environment.NewLine & _
                    " from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  " & Environment.NewLine & _
                    " where TSPL_VCGL_Head.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''   " & strWhrCls & ") and TSPL_VCGL_Head.Status =1  " & Environment.NewLine & _
                    " ) VCGL  " & Environment.NewLine & _
                    " group by  VCGL.Item_Code ,VCGL.UOM " & Environment.NewLine & _
                   "  union all " & Environment.NewLine & _
                   " ---------------- Security Receipt--------------------------" & Environment.NewLine & _
                   " select SecurityReceipt.Item_Code ,SecurityReceipt.UOM ,SUM(SecurityReceipt.Qty) as TotalQty,sum(SecurityReceipt.Item_Net_Amt) as TotalAmount,sum(SecurityReceipt.Total_Tax_Amt) as TotalTaxAmount,sum(SecurityReceipt.CGSTAmount) as CGSTAmount,sum(SecurityReceipt.SGSTAmount) as SGSTAmount,sum(SecurityReceipt.IGSTAmount) as IGSTAmount,sum(SecurityReceipt.UGSTAmount) as UGSTAmount  from ( " & Environment.NewLine & _
                   " select TSPL_Customer_Invoice_Head.Against_Security_Receipt_No as Document_No,'' AS Item_Code ,0 AS Qty ,'' as UOM,TSPL_Customer_Invoice_Head.Document_Type," & Environment.NewLine & _
                   " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Amount ,0))" & Environment.NewLine & _
                   " as Item_Net_Amt ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Tax  ,0))" & Environment.NewLine & _
                   " as Total_Tax_Amt,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as CGSTAmount, " & Environment.NewLine & _
                   " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as SGSTAmount ," & Environment.NewLine & _
                   " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end " & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as IGSTAmount ," & Environment.NewLine & _
                   " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine & _
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as UGSTAmount from TSPL_Customer_Invoice_Detail" & Environment.NewLine & _
                   " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Document_No =TSPL_Customer_Invoice_Detail.Document_No " & Environment.NewLine & _
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
                   " where isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')<>''  and TSPL_Customer_INVOICE_HEAD.Status =1  " & strWhrCls & " " & Environment.NewLine & _
                   " ) SecurityReceipt " & Environment.NewLine & _
                   " group by  SecurityReceipt.Item_Code ,SecurityReceipt.UOM" & Environment.NewLine
            End If

            BaseQry += " ) FinalQuery left outer join tspl_item_master on TSPL_ITEM_MASTER.Item_Code = FinalQuery.Item_Code group by  FinalQuery.Item_Code ,FinalQuery.UOM" & Environment.NewLine
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return BaseQry
    End Function
    Function getBaseQueryforDetail_GSTR1(ByVal strWhrCls As String, ByVal strType As String) As String
        Dim BaseQry As String = String.Empty
        Try
            'Dim strtranstypeandsaleinvoiceno As String = " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then 'Sale Return' when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then 'Sale Invoice'  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then 'Scrap' when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then 'Scrap Return'   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then 'MCC Material Sale Return'  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then 'Security Receipt'  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then 'Direct AR Invoice'  end) as [Trans Type], " & Environment.NewLine & _
            '" (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then Against_Sale_Return_No when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then Against_Sale_No  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then AgainstScrap when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then AgainstScrapReturn   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then Against_VCGL when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then Against_Security_Receipt_No  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Sale Invoice No]," & Environment.NewLine & _
            '" (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then 0 else TSPL_SD_SALE_RETURN_HEAD.Tax1_Rate end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>'' then 0 when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return,'')<>'' then TSPL_MCC_Material_sale_Return.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No,'')<>'' then TSPL_Customer_INVOICE_HEAD.Tax1_Rate when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')='' ) then 0 end) as TaxRate, " & Environment.NewLine & _
            '" (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date else TSPL_SD_SALE_RETURN_HEAD.Document_Date end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.shipment_Date when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VCGL_HEAD.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then TSPL_MCC_Material_sale_Return.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then TSPL_RECEIPT_HEADER. Receipt_Date when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then null  end) as [Sale Invoice Date], " & Environment.NewLine & _
            '" (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code else case when isnull(TSPL_SD_SALE_RETURN_HEAD.Ship_To_Location ,'')='' then TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location else TSPL_SD_SALE_RETURN_HEAD.Ship_To_Location end end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then case when isnull(TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,'')='' then TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location else TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location end when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.loc_code when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.loc_code when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VCGL_HEAD.Location_Segment when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then case when isnull(TSPL_MCC_Material_sale_Return.Ship_To_Location ,'')='' then TSPL_MCC_Material_sale_Return.Bill_To_Location else TSPL_MCC_Material_sale_Return.Ship_To_Location end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then TSPL_RECEIPT_HEADER. Location_GL_Code when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Location Code], "
            ''richa ALF/03/12/18-000091
            Dim strtranstypeandsaleinvoiceno As String = " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then 'Sale Return' when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then 'Sale Invoice'  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then 'Scrap' when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then 'Scrap Return'   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then 'MCC Material Sale Return'  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then 'Security Receipt'  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then 'Direct AR Invoice'  end) as [Trans Type], " & Environment.NewLine &
            " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then Against_Sale_Return_No when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then Against_Sale_No  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then AgainstScrap when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then AgainstScrapReturn   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then Against_VCGL when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then Against_Security_Receipt_No  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Sale Invoice No]," & Environment.NewLine &
            " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then 0 else TSPL_SD_SALE_RETURN_HEAD.Tax1_Rate end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>'' then 0 when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return,'')<>'' then TSPL_MCC_Material_sale_Return.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No,'')<>'' then TSPL_Customer_INVOICE_HEAD.Tax1_Rate when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')='' ) then (TSPL_Customer_INVOICE_HEAD.TAX1_Rate+TSPL_Customer_INVOICE_HEAD.TAX2_Rate+TSPL_Customer_INVOICE_HEAD.TAX3_Rate+TSPL_Customer_INVOICE_HEAD.TAX4_Rate+TSPL_Customer_INVOICE_HEAD.TAX5_Rate+TSPL_Customer_INVOICE_HEAD.TAX6_Rate+TSPL_Customer_INVOICE_HEAD.TAX7_Rate+TSPL_Customer_INVOICE_HEAD.TAX8_Rate+TSPL_Customer_INVOICE_HEAD.TAX9_Rate+TSPL_Customer_INVOICE_HEAD.TAX10_Rate) end) as TaxRate, " & Environment.NewLine &
            " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date else TSPL_SD_SALE_RETURN_HEAD.Document_Date end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.shipment_Date when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VCGL_HEAD.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then TSPL_MCC_Material_sale_Return.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then TSPL_RECEIPT_HEADER. Receipt_Date when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then null  end) as [Sale Invoice Date], " & Environment.NewLine &
            " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code else case when isnull(TSPL_SD_SALE_RETURN_HEAD.Ship_To_Location ,'')='' then NULL else TSPL_SD_SALE_RETURN_HEAD.Ship_To_Location end end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then case when isnull(TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,'')='' then NULL else TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location end when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.loc_code when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.loc_code when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VCGL_HEAD.Location_Segment when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then case when isnull(TSPL_MCC_Material_sale_Return.Ship_To_Location ,'')='' then NULL else TSPL_MCC_Material_sale_Return.Ship_To_Location end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then TSPL_RECEIPT_HEADER. Location_GL_Code when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Location Code], "


            BaseQry = " select SNo,Particulars,[Document No],[Document Date], [Trans Type],[Sale Invoice No],[Sale Invoice Date],CASE WHEN ISNULL(TSPL_SHIP_TO_LOCATION.Ship_To_Code,'')<>'' THEN TSPL_SHIP_TO_LOCATION_sTATE.GST_STATE_Code +'- ' +TSPL_SHIP_TO_LOCATION_sTATE.STATE_NAME ELSE tspl_state_master.GST_STATE_Code +' - ' +TSPL_state_master.state_name END AS [Place of Supply], z.[Customer Code] ,z.[Customer Name] ,z.[GST No],z.[Document Type] ,z.[Taxable Value] ,z.TaxRate,z.[Taxable Amount] ,z.[Round Off Amount] ,z.[Invoice Amount] from ( " & Environment.NewLine & _
            " select ROW_NUMBER() OVER(ORDER BY TSPL_Customer_Invoice_Head.Document_No ASC) as  SNo,'" & strType & "' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " " & Environment.NewLine & _
            " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value]," & Environment.NewLine & _
            " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount] " & Environment.NewLine & _
            " from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code  " & Environment.NewLine & _
            " left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN.Document_No =TSPL_Customer_Invoice_Head.AgainstScrapReturn " & Environment.NewLine & _
            " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_Return_No " & Environment.NewLine & _
            " left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_Return_No AND TSPL_SALE_RETURN_MASTER_BULKSALE.AGAINST='Bulk Invoice' " & Environment.NewLine & _
            " left outer join TSPL_SD_SALE_RETURN_HEAD TSPL_MCC_Material_sale_Return on TSPL_MCC_Material_sale_Return.Document_Code =TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return AND TSPL_MCC_Material_sale_Return.Trans_Type='MCC' " & Environment.NewLine & _
            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine & _
            " left outer join TSPL_CANSALE_INVOICE_HEAD on TSPL_CANSALE_INVOICE_HEAD.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine & _
            " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine & _
            " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_Customer_Invoice_Head.AgainstScrap " & Environment.NewLine & _
            " left outer join TSPL_VCGL_HEAD  on TSPL_VCGL_HEAD.Document_No  =TSPL_Customer_Invoice_Head.Against_VCGL  " & Environment.NewLine & _
            " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No  =TSPL_Customer_Invoice_Head.Against_Security_Receipt_No  and TSPL_RECEIPT_HEADER.Receipt_Type = 'P' " & Environment.NewLine & _
            " where 1=1 " & strWhrCls & " " & Environment.NewLine & _
            " ) z " & Environment.NewLine & _
            " left outer join tspl_customer_master on tspl_customer_master.Cust_Code  =z.[Customer Code] " & Environment.NewLine & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =z.[Location Code] " & Environment.NewLine & _
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =z.[Location Code] " & Environment.NewLine & _
            " left outer join tspl_state_master on tspl_state_master.STATE_CODE =tspl_customer_master.State" & Environment.NewLine & _
            " left outer join tspl_state_master TSPL_SHIP_TO_LOCATION_sTATE on TSPL_SHIP_TO_LOCATION_sTATE.STATE_CODE =TSPL_SHIP_TO_LOCATION.State" & Environment.NewLine

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return BaseQry
    End Function
    Sub DrillDownDetailForGSTR1_ItemWise()
        Try
            If clsCommon.myLen(gv2.CurrentRow.Cells("Name").Value) > 0 Then
                Dim qry As String = String.Empty
                qry = "select 1 where 1=2 "
                Dim dt As DataTable = Nothing
                Dim Wrcls As String = "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")"
                End If
                'Dim strtranstypeandsaleinvoiceno As String = " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then 'Sale Return' when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then 'Sale Invoice'  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then 'Scrap' when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then 'Scrap Return'   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then 'MCC Material Sale Return'  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then 'Security Receipt'  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then 'Direct AR Invoice'  end) as [Trans Type],  (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then Against_Sale_Return_No when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then Against_Sale_No  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then AgainstScrap when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then AgainstScrapReturn   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then Against_VCGL when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then Against_Security_Receipt_No  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Sale Invoice No],"

                If clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2B Invoices - 4A, 4B, 4C, 6B, 6C") = CompairStringResult.Equal Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "B2B Invoices - 4A, 4B, 4C, 6B, 6C")
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Taxable Sales")
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Large) Invoices - 5A, 5B") = CompairStringResult.Equal Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 and TSPL_Customer_Invoice_Head.Document_Total>250000 "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "B2C(Large) Invoices - 5A, 5B")
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Small) Invoices - 7") = CompairStringResult.Equal Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 and TSPL_Customer_Invoice_Head.Document_Total<= 250000 "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "B2C(Small) Invoices - 7")
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Registered) - 9B") = CompairStringResult.Equal Then
                    Wrcls += "  and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Credit/Debit Notes(Registered) - 9B")
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Unregistered) - 9B") = CompairStringResult.Equal Then
                    Wrcls += "  and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Credit/Debit Notes(Unregistered) - 9B")
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Exports Invoices - 6A") = CompairStringResult.Equal Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No  IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Exports Invoices - 6A")
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Then
                    Wrcls += "  AND isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) =0  AND TSPL_Customer_Invoice_Head.Against_Sale_No not IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Nil Rated Invoices - 8A, 8B, 8C, 8D")
                End If

                dt = clsDBFuncationality.GetDataTable(qry)

                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.SummaryRowsBottom.Clear()
                gv3.MasterView.Refresh()

                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    gv3.DataSource = dt
                    RadPageView1.SelectedPage = RadPageViewPage3
                    gv3.BestFitColumns()
                    gv3.EnableFiltering = True
                    FormatGridFooterDetail()
                    RadPageViewPage3.Text = clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value)
                Else
                    RadPageViewPage3.Text = "Detail data"
                    Throw New Exception("No Data Found to Display")
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''richa 30 Nov,2018 ALF/26/11/18-000089,GKD/29/01/19-000173
    Sub DrillDownDetailForGSTR1()
        Try
            If clsCommon.myLen(gv2.CurrentRow.Cells("Name").Value) > 0 Then
                Dim qry As String = String.Empty
                qry = "select 1 where 1=2 "
                Dim dt As DataTable = Nothing
                Dim Wrcls As String = "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")"
                End If

                If clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2B Invoices - 4A, 4B, 4C, 6B, 6C") = CompairStringResult.Equal Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
                    " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))

                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
                    " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))

                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Large) Invoices - 5A, 5B") = CompairStringResult.Equal Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
                    " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & _
                    " and TSPL_Customer_Invoice_Head.Document_Total>250000 "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))

                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Small) Invoices - 7") = CompairStringResult.Equal Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
                   " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
                   " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & _
                   " and TSPL_Customer_Invoice_Head.Document_Total<=250000 "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))

                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Registered) - 9B") = CompairStringResult.Equal Then

                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') " & Environment.NewLine & _
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
                     " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='')  "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Unregistered) - 9B") = CompairStringResult.Equal Then

                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') " & Environment.NewLine & _
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
                     " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='') "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))

                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Exports Invoices - 6A") = CompairStringResult.Equal Then
                    Wrcls += " AND TSPL_Customer_Invoice_Head.Against_Sale_No  IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))

                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Then
                    Wrcls += " AND isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) =0  AND TSPL_Customer_Invoice_Head.Against_Sale_No not IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))
                ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Taxable Invoice (Direct)") = CompairStringResult.Equal Then

                    Wrcls += " and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type='I' " & Environment.NewLine & _
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
                     " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='')  "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))
                End If

                dt = clsDBFuncationality.GetDataTable(qry)

                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.SummaryRowsBottom.Clear()
                gv3.MasterView.Refresh()

                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    gv3.DataSource = dt
                    RadPageView1.SelectedPage = RadPageViewPage3
                    gv3.BestFitColumns()
                    gv3.EnableFiltering = True
                    FormatGridFooterDetail()
                    RadPageViewPage3.Text = clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value)
                Else
                    RadPageViewPage3.Text = "Detail data"
                    Throw New Exception("No Data Found to Display")
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''=------------------
    'Sub DrillDownDetailForGSTR1()
    '    Try
    '        If clsCommon.myLen(gv2.CurrentRow.Cells("Name").Value) > 0 Then
    '            Dim qry As String = String.Empty
    '            qry = "select 1 where 1=2 "
    '            Dim dt As DataTable = Nothing
    '            Dim Wrcls As String = "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

    '            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
    '            End If
    '            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
    '                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
    '            End If
    '            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
    '                Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")"
    '            End If
    '            ''richa ALF/20/11/18-000087
    '            Dim strtranstypeandsaleinvoiceno As String = " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then 'Sale Return' when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then 'Sale Invoice'  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then 'Scrap' when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then 'Scrap Return'   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then 'MCC Material Sale Return'  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then 'Security Receipt'  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then 'Direct AR Invoice'  end) as [Trans Type],  (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then Against_Sale_Return_No when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then Against_Sale_No  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then AgainstScrap when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then AgainstScrapReturn   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then Against_VCGL when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then Against_Security_Receipt_No  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Sale Invoice No],"
    '            ''richa BHA/26/10/18-000643
    '            If clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2B Invoices - 4A, 4B, 4C, 6B, 6C") = CompairStringResult.Equal Then
    '                qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'B2B Invoices - 4A, 4B, 4C, 6B, 6C' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
    '                " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
    '                " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
    '                " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
    '                " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Wrcls & " "
    '            ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal Then
    '                qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Taxable Sales' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
    '                " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
    '                " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
    '                " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
    '                " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Wrcls & " "

    '            ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Large) Invoices - 5A, 5B") = CompairStringResult.Equal Then
    '                qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'B2C(Large) Invoices - 5A, 5B' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount] , case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount]  from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
    '                " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
    '                " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
    '                " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
    '                " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & _
    '                " and TSPL_Customer_Invoice_Head.Document_Total>250000 " & Wrcls & " " & Environment.NewLine

    '            ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Small) Invoices - 7") = CompairStringResult.Equal Then
    '                qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'B2C(Small) Invoices - 7' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount] , case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount]  from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
    '                " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
    '                " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine & _
    '                " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
    '                " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & _
    '                " and TSPL_Customer_Invoice_Head.Document_Total<=250000 " & Wrcls & " " & Environment.NewLine
    '            ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Registered) - 9B") = CompairStringResult.Equal Then
    '                qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Credit/Debit Notes(Registered) - 9B' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
    '                " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
    '                " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') " & Environment.NewLine & _
    '                " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
    '                 " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='') " & Wrcls & "  " & Environment.NewLine
    '            ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Unregistered) - 9B") = CompairStringResult.Equal Then
    '                qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Credit/Debit Notes(Unregistered) - 9B' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
    '                " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
    '                " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') " & Environment.NewLine & _
    '                " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine & _
    '                 " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='') " & Wrcls & "  " & Environment.NewLine
    '            ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Exports Invoices - 6A") = CompairStringResult.Equal Then
    '                qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Exports Invoices - 6A' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount] , case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount]  from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
    '                " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
    '                 " where 1=1 AND TSPL_Customer_Invoice_Head.Against_Sale_No  IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Wrcls & "  "
    '            ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Then
    '                qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Nil Rated Invoices - 8A, 8B, 8C, 8D' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
    '                " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
    '                  " where  isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) =0  AND TSPL_Customer_Invoice_Head.Against_Sale_No not IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Wrcls & "  "
    '            End If

    '            dt = clsDBFuncationality.GetDataTable(qry)

    '            gv3.DataSource = Nothing
    '            gv3.Rows.Clear()
    '            gv3.Columns.Clear()
    '            gv3.GroupDescriptors.Clear()
    '            gv3.MasterTemplate.SummaryRowsBottom.Clear()
    '            gv3.MasterView.Refresh()

    '            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
    '                gv3.DataSource = dt
    '                RadPageView1.SelectedPage = RadPageViewPage3
    '                gv3.BestFitColumns()
    '                gv3.EnableFiltering = True
    '                FormatGridFooterDetail()
    '                RadPageViewPage3.Text = clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value)
    '            Else
    '                RadPageViewPage3.Text = "Detail data"
    '                Throw New Exception("No Data Found to Display")
    '            End If

    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub
    Sub DrillDownHeaderForGSTR1()
        Try
            If clsCommon.myLen(gv1.CurrentRow.Cells("Name").Value) > 0 Then
                Dim qry As String = String.Empty
                qry = "select 1 where 1=2 "
                Dim dt As DataTable = Nothing
                Dim Wrcls As String = "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")"
                End If

                If clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Total number of vouchers for the period") = CompairStringResult.Equal Or clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal Then
                    qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Total number of vouchers for the period' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date],TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 " & Wrcls & " " & Environment.NewLine

                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Included in returns") = CompairStringResult.Equal Then
                    qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Included in returns' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date],TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
                    " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
                    " where 1=1 and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine

                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Included in HSN/SAC Summary") = CompairStringResult.Equal Then
                    qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Included in HSN/SAC Summary' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date],TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine & _
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine & _
                    " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine & _
                     " where 1=1 and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine
                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Not relevant for returns") = CompairStringResult.Equal Or clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal Then
                    qry = " Select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Not relevant for returns' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date],TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount]  from (" & Environment.NewLine & _
                    " select TSPL_Customer_Invoice_Head.Document_No as [Document No]   from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 " & Wrcls & " " & Environment.NewLine & _
                    " except " & Environment.NewLine & _
                    " select TSPL_Customer_Invoice_Head.Document_No as [Document No]   from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 and (isnull(AgainstScrapReturn,'')<>'' or ISNULL (Against_Sale_Return_No,'')<>'' or ISNULL (Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " ) z left outer join  TSPL_Customer_Invoice_Head on z.[Document No] =TSPL_Customer_Invoice_Head.Document_No " & Wrcls & " " & Environment.NewLine & _
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code "
                End If

                dt = clsDBFuncationality.GetDataTable(qry)

                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.SummaryRowsBottom.Clear()
                gv3.MasterView.Refresh()

                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    gv3.DataSource = dt
                    RadPageView1.SelectedPage = RadPageViewPage3
                    gv3.BestFitColumns()
                    gv3.EnableFiltering = True
                    FormatGridFooterDetail()
                    RadPageViewPage3.Text = clsCommon.myCstr(gv1.CurrentRow.Cells("Name").Value)
                Else
                    RadPageViewPage3.Text = "Detail data"
                    Throw New Exception("No Data Found to Display")
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        Dim strQry As String = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER where 1=1"
        Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

        If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 Then
            strQry += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "')"
        End If
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        LoadCustomerNew()
    End Sub
    Sub LoadCustomerNew()
        Dim strQry As String = "select CM1.cust_code as Code, CM1.Customer_Name as Name, Case When ISNULL(CM2.Cust_Code,'')<>'' Then ISNULL(CM2.Cust_Code,'')+' - '+ISNULL(CM2.Customer_Name,'') Else '' End as [ParentCustomer]  from tspl_customer_master CM1 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM2 ON CM2.Cust_Code=CM1.Parent_Customer_No  where 1=1"

        '' for active status of customer 
        strQry += " and CM1.Status='N'"
        Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

        If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 Then
            strQry += " AND CM1.cust_code in (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                strQry += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ") ) "
            Else
                strQry += " ) "
            End If
        End If

        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub btnGSTR1_CheckStateChanged(sender As Object, e As EventArgs) Handles btnGSTR1.CheckStateChanged
        ''richa UDL/16/11/18-000241
        If btnGSTR1.IsChecked Then
            txtCustomer.Enabled = True
            txtCustomerGroup.Enabled = True

            txtVendGroup.Enabled = False
            txtVendor.Enabled = False
            txtVendor.arrValueMember = Nothing
            txtVendGroup.arrValueMember = Nothing
        Else
            txtCustomer.Enabled = False
            txtCustomerGroup.Enabled = False

            txtVendGroup.Enabled = True
            txtVendor.Enabled = True
            txtCustomer.arrValueMember = Nothing
            txtCustomerGroup.arrValueMember = Nothing
        End If
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        gv3.DataSource = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage3.Text = "Detail data"
    End Sub
    Public Sub funPrint()
        Try
            Dim StrQuery As String = Nothing
            Dim StrHeaderQry As String = Nothing
            Dim StrDetailQry As String = Nothing
            Dim dtHeader As DataTable = Nothing
            Dim dtdetail As DataTable = Nothing
            Dim dt As DataTable = Nothing
            Dim fromdate As String = Nothing
            Dim ToDate As String = Nothing
            fromdate = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            ToDate = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            StrQuery = "select '" & fromdate & "' as FromDate,'" & ToDate & "' as ToDate, comp_code,comp_Name,Add1 ,Add2 ,Add3 ,GSTReg_No  from tspl_company_master"

            If btnGSTR1.IsChecked Then
                StrHeaderQry = GSTR1DataHeader()
                StrDetailQry = GSTR1DataFooter()
            Else
                StrHeaderQry = GSTQryforHeader()
                StrDetailQry = GSTQryforDetail()
            End If

            dt = clsDBFuncationality.GetDataTable(StrQuery)
            dtHeader = clsDBFuncationality.GetDataTable(StrHeaderQry)
            dtdetail = clsDBFuncationality.GetDataTable(StrDetailQry)

            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If btnGSTR1.IsChecked Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dtHeader, "rptGSTCompuatation - Sale", "GST Compuatation", clsCommon.myCDate(txtFromDate.Value), "SubReportOfGSTComHeaderPart.rpt", "SubReportOfGSTComdetailPart-Sale.rpt", dtdetail)
                Else
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dtHeader, "rptGSTCompuatation", "GST Compuatation", clsCommon.myCDate(txtFromDate.Value), "SubReportOfGSTComHeaderPart.rpt", "SubReportOfGSTComdetailPart.rpt", dtdetail)
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data found to print")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    '==================Added by preeti Gupta AGAINST TICKET NO[]
    Private Function WhrClsForAllTran() As String
        Dim WhrCls As String = Nothing
        WhrCls = "  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103)"

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            WhrCls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            WhrCls += " and tspl_vendor_master.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            WhrCls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtVendGroup.arrValueMember) + ")"
        End If
        Return WhrCls
    End Function
    Private Function ItemWIsePurchaseInvoice() As String
        Dim Qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        Qry = "select 'Purchase Invoice' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No as AgainstDocNo" & _
                            " ,TSPL_PI_DETAIL.item_code,TSPL_PI_DETAIL.Unit_code as UOM,TSPL_PI_DETAIL.Item_cost,TSPL_PI_DETAIL.PI_Qty as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_PI_DETAIL.Amt_Less_Discount+isnull(tspl_pi_detail.Rejected_Amount,0)) as Amt_Less_Discount," & _
                            " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" & _
  " case when  PI_Tax1_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end  " & _
   "  +case when  PI_Tax2_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end   " & _
    " +case when  PI_Tax3_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " & _
    " +case when  PI_Tax4_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end   " & _
     " +case when  PI_Tax5_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end " & _
     " ) as ItemWise_Exempt_Amt" & _
    " , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" & _
     " case when  PI_Tax1_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end   " & _
     " +case when  PI_Tax2_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end  " & _
      " +case when  PI_Tax3_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " & _
      " +case when  PI_Tax4_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end  " & _
       " +case when  PI_Tax5_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end) as CGST_Amt, " & _
        " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" & _
        " case when  PI_Tax1_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end  " & _
        " +case when  PI_Tax2_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end " & _
        " +case when  PI_Tax3_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " & _
        " +case when  PI_Tax4_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end  " & _
        " +case when  PI_Tax5_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end ) as SGST_Amt" & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * (" & _
        " case when  PI_Tax1_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end  " & _
         " +case when  PI_Tax2_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end  " & _
         " +case when  PI_Tax3_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " & _
          " +case when  PI_Tax4_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end   " & _
          " +case when  PI_Tax5_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end ) as UGST_Amt" & _
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" & _
          " case when  PI_Tax1_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end  " & _
            " +case when  PI_Tax2_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end  " & _
            " +case when  PI_Tax3_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " & _
            " +case when  PI_Tax4_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end  " & _
            " +case when  PI_Tax5_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end ) as IGST_Amt" & _
",TSPL_VENDOR_INVOICE_HEAD.Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,TSPL_PI_HEAD.PI_Type" & _
       " from TSPL_VENDOR_INVOICE_HEAD" & _
" left join TSPL_PI_HEAD on TSPL_PI_HEAD.pi_no=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No " & _
" left join TSPL_PI_DETAIL on TSPL_PI_DETAIL.pi_no=TSPL_PI_HEAD.pi_no" & _
" left join tspl_tax_master as PI_Tax1_Code on  PI_Tax1_Code.tax_code=TSPL_PI_DETAIL.tax1 " & _
    " left join tspl_tax_master as PI_Tax2_Code on  PI_Tax2_Code.tax_code=TSPL_PI_DETAIL.tax2" & _
   " left join tspl_tax_master as PI_Tax3_Code on  PI_Tax3_Code.tax_code=TSPL_PI_DETAIL.tax3 " & _
  " left join tspl_tax_master as PI_Tax4_Code on  PI_Tax4_Code.tax_code=TSPL_PI_DETAIL.tax4 " & _
  " left join tspl_tax_master as PI_Tax5_Code on  PI_Tax5_Code.tax_code=TSPL_PI_DETAIL.tax5 " & _
" left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" & _
" where isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' " & _
        "" & Whrcls & ""

        Return Qry
    End Function
    Private Function ItemWIsePurchaseReturn() As String
        Dim Qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        Qry = "select 'Purchase Return' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No as AgainstDocNo" & _
            " ,TSPL_PR_DETAIL.item_code,TSPL_PR_DETAIL.Unit_code as Uom,TSPL_PR_DETAIL.Item_cost,TSPL_PR_DETAIL.PR_Qty as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_PR_DETAIL.Amt_Less_Discount) as Amt_Less_Discount," & _
" case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" & _
" case when  PR_Tax1_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax1_Amt,0)else 0 end  " & _
     " +case when  PR_Tax2_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " & _
    " +case when  PR_Tax3_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " & _
    " +case when  PR_Tax4_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " & _
     " +case when  PR_Tax5_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end" & _
" ) as ItemWise_Exempt_Amt" & _
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" & _
" case when  PR_Tax1_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax1_Amt,0)else 0 end  " & _
    " +case when  PR_Tax2_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " & _
    " +case when  PR_Tax3_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " & _
    " +case when  PR_Tax4_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " & _
     " +case when  PR_Tax5_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end" & _
" ) as CGST_Amt" & _
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  PR_Tax2_Code.type='SGST' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " & _
     " +case when  PR_Tax3_Code.type='SGST' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " & _
    " +case when  PR_Tax4_Code.type='SGST' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " & _
     " +case when  PR_Tax5_Code.type='SGST' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end) as SGST_Amt" & _
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(  case when  PR_Tax1_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax1_Amt,0)else 0 end  " & _
    " +case when  PR_Tax2_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " & _
    " +case when  PR_Tax3_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " & _
    " +case when  PR_Tax4_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " & _
     " +case when  PR_Tax5_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end) as UGST_Amt" & _
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(  case when  PR_Tax1_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax1_Amt,0)else 0 end  " & _
    " +case when  PR_Tax2_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " & _
    " +case when  PR_Tax3_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " & _
    " +case when  PR_Tax4_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " & _
     " +case when  PR_Tax5_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end) as IGST_Amt" & _
      ",TSPL_VENDOR_INVOICE_HEAD.Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" & _
       " from TSPL_VENDOR_INVOICE_HEAD" & _
" left join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No" & _
" left join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No" & _
" left join tspl_tax_master as PR_Tax1_Code on  PR_Tax1_Code.tax_code=TSPL_PR_DETAIL.tax1 " & _
  " left join tspl_tax_master as PR_Tax2_Code on  PR_Tax2_Code.tax_code=TSPL_PR_DETAIL.tax2" & _
  " left join tspl_tax_master as PR_Tax3_Code on  PR_Tax3_Code.tax_code=TSPL_PR_DETAIL.tax3 " & _
   " left join tspl_tax_master as PR_Tax4_Code on  PR_Tax4_Code.tax_code=TSPL_PR_DETAIL.tax4 " & _
   " left join tspl_tax_master as PR_Tax5_Code on  PR_Tax5_Code.tax_code=TSPL_PR_DETAIL.tax5" & _
   " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" & _
 " where isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>''" & _
        "" & Whrcls & ""

        Return Qry
    End Function
    Private Function ItemWiseDirectAP() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = "select 'Direct AP' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No as AgainstDocNo" & _
" ,'' as item_code,''  Uom,0 as Item_cost,0 as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount) as Amt_Less_Discount," & _
" case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " & _
                 "  +case when  Tax2_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " & _
                  " +case when  Tax3_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " & _
                  " +case when  Tax4_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " & _
                   " +case when  Tax5_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " & _
                   " ) as ItemWise_Exempt_Amt  " & _
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " & _
                  " +case when  Tax2_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " & _
                  " +case when  Tax3_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " & _
                 " +case when  Tax4_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " & _
                  "  +case when  Tax5_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " & _
                  " ) as CGST_Amt " & _
 ", case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " & _
                  " +case when  Tax2_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " & _
                  " +case when  Tax3_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " & _
                  " +case when  Tax4_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " & _
                   " +case when  Tax5_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " & _
                   " ) as SGST_Amt " & _
 " ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * (case when  Tax1_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " & _
                  " +case when  Tax2_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " & _
                  " +case when  Tax3_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " & _
                 " +case when  Tax4_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " & _
                   " +case when  Tax5_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " & _
                   " ) as UGST_Amt " & _
 " , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " & _
                 "  +case when  Tax2_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " & _
                  " +case when  Tax3_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " & _
                 " +case when  Tax4_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " & _
                   " +case when  Tax5_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " & _
                  " ) as IGST_Amt ,TSPL_VENDOR_INVOICE_HEAD.Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" & _
       " from TSPL_VENDOR_INVOICE_HEAD" & _
" left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" & _
" left join tspl_tax_master as Tax1_Code on  Tax1_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax1  " & _
                " left join tspl_tax_master as Tax2_Code on  Tax2_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax2 " & _
                " left join tspl_tax_master as Tax3_Code on  Tax3_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax3 " & _
                " left join tspl_tax_master as Tax4_Code on  Tax4_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax4 " & _
                " left join tspl_tax_master as Tax5_Code on  Tax5_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax5 " & _
       " where 2 = 2" & _
  " and isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' and  " & _
                " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' and  " & _
                " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' and " & _
                 " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' and " & _
                 " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' and " & _
                 " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')='' and  " & _
                " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')='' and  " & _
                "  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')=''  " & _
                 "  and 2=(case when isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo,'' )='BM-PR' then 3 else 2 end)  " & _
        "" & Whrcls & ""
        Return qry
    End Function

    Private Function ItemWiseBulkMilkPurchaseInvoice() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = " select 'Bulk Milk Purchase Invoice' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No as AgainstDocNo" & _
    " ,TSPL_BULK_MILK_PURCHASE_INVOICE_detail.item_code,TSPL_BULK_MILK_PURCHASE_INVOICE_detail.UOM,0 as Item_cost,TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Invoice_Qty as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Actual_Amount) as Amt_Less_Discount,0 as ItemWise_Exempt_Amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" & _
   " from TSPL_VENDOR_INVOICE_HEAD" & _
" left join TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD on TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD.Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No" & _
" left join TSPL_BULK_MILK_PURCHASE_INVOICE_detail on TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Doc_No=TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD.Doc_No" & _
" left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" & _
" where isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>''" & _
        "" & Whrcls & ""
        Return qry
    End Function
    Private Function ItemWiseBulkMilkPurchaseReturn() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = "select 'Bulk Milk Purchase Return' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.RefDocNo as AgainstDocNo" & _
" ,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.item_code,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.UOM,0 as Item_cost,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Invoice_Qty as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Actual_Amount) as Amt_Less_Discount,0 as ItemWise_Exempt_Amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" & _
       " from TSPL_VENDOR_INVOICE_HEAD" & _
" left join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD on TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' " & _
    " left join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL on TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No" & _
    " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" & _
 " where (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'')" & _
        "" & Whrcls & ""
        Return qry
    End Function
    Private Function ItemWiseMilkPurchaseInvoice() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = " select 'Milk Purchase Invoice' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No as AgainstDocNo" & _
                ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.item_code,'' as UOM,0 as Item_cost,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Net_Amount) as Amt_Less_Discount," & _
            " 0 as ItemWise_Exempt_amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" & _
        " from TSPL_VENDOR_INVOICE_HEAD" & _
" left join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_code=TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No" & _
" left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.doc_code=TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_code" & _
" left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" & _
" where  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>''" & _
        "" & Whrcls & ""
        Return qry
    End Function
    Private Function itemWiseVCGL() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = " select 'VCGL' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No as AgainstDocNo,'' as item_code,'' as UOM,0 as Item_cost,0 as Qty," & _
                " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_VENDOR_INVOICE_HEAD.Document_Total ) as Amt_Less_Discount" & _
                " , 0 as ItemWise_Exempt_amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type " & _
                "  from TSPL_VENDOR_INVOICE_HEAD" & _
                " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" & _
                " where  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>''" & _
               "" & Whrcls & ""
        Return qry
    End Function

    Private Function ItemWiseB2BInvoices34A() As String
        Dim strBaseQuery As String = Nothing
        Dim strItemWisePurchaseInvoice As String = ItemWIsePurchaseInvoice()
        Dim strItemWisePurchaseReturn As String = ItemWIsePurchaseReturn()
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()

        Dim WhrItemWisePurchaseInvoiceAndReturn As String = " and TSPL_VENDOR_MASTER.GSTRegistered=1 and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0"

        Dim WhrItemWiseDirectAP As String = "  and TSPL_VENDOR_INVOICE_HEAD.Document_Type in ('I')  and tspl_vendor_master.GSTRegistered=1 and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax ,0) <>0 "

        strBaseQuery = "select 'B2B Invoices - 3, 4A' as DocType,XX.* from (" & _
            "" & strItemWisePurchaseInvoice & "" & _
            " " & WhrItemWisePurchaseInvoiceAndReturn & "" & _
            " Union all " & _
        "" & strItemWisePurchaseReturn & "" & _
         " " & WhrItemWisePurchaseInvoiceAndReturn & "" & _
        " Union all " & _
        "" & strItemWiseDirectAP & "" & _
         " " & WhrItemWiseDirectAP & "" & _
         ") as XX"

        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" & _
        "" & strBaseQuery & "" & _
        ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"

        Return strBaseQuery
    End Function
    Private Function ItemWiseTaxablePurchases() As String
        Dim strBaseQuery As String = Nothing
        Dim strItemWisePurchaseInvoice As String = ItemWIsePurchaseInvoice()
        Dim strItemWisePurchaseReturn As String = ItemWIsePurchaseReturn()
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()

        Dim WhrItemWisePurchaseInvoiceAndReturn As String = " and TSPL_VENDOR_MASTER.GSTRegistered=1 and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0"

        Dim WhrItemWiseDirectAP As String = "  and TSPL_VENDOR_INVOICE_HEAD.Document_Type in ('I')  and tspl_vendor_master.GSTRegistered=1 and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax ,0) <>0 "

        strBaseQuery = "select 'Taxable Purchases' as DocType,XX.* from (" & _
            "" & strItemWisePurchaseInvoice & "" & _
            " " & WhrItemWisePurchaseInvoiceAndReturn & "" & _
            " Union all " & _
        "" & strItemWisePurchaseReturn & "" & _
         " " & WhrItemWisePurchaseInvoiceAndReturn & "" & _
        " Union all " & _
        "" & strItemWiseDirectAP & "" & _
         " " & WhrItemWiseDirectAP & "" & _
         ") as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" & _
        "" & strBaseQuery & "" & _
        ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
        Return strBaseQuery
    End Function
    Private Function ItemWiseB2BURInvoices4B() As String
        Dim strBaseQuery As String = Nothing
        Dim strItemWisePurchaseInvoice As String = ItemWIsePurchaseInvoice()
        Dim strItemWisePurchaseReturn As String = ItemWIsePurchaseReturn()
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()

        Dim WhrItemWiseMilkPurchaseInvoice As String = " and TSPL_VENDOR_MASTER.GSTRegistered=0 and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0 and PI_Type<>'I'"
        Dim WhrItemWiseMilkPurchasedReturn As String = " and TSPL_VENDOR_MASTER.GSTRegistered=0 and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0 "

        Dim WhrItemWiseDirectAP As String = " and TSPL_VENDOR_MASTER.GSTRegistered=0 and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0 and  TSPL_VENDOR_INVOICE_HEAD.Document_Type in ('I') "

        strBaseQuery = "select 'B2BUR Invoices - 4B' as DocType, XX.* from( " & _
 "" & strItemWisePurchaseInvoice & "" & _
            " " & WhrItemWiseMilkPurchaseInvoice & "" & _
            " Union all " & _
        "" & strItemWisePurchaseReturn & "" & _
         " " & WhrItemWiseMilkPurchasedReturn & "" & _
        " Union all " & _
        "" & strItemWiseDirectAP & "" & _
         " " & WhrItemWiseDirectAP & "" & _
         ") as XX"

        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" & _
     "" & strBaseQuery & "" & _
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"

        Return strBaseQuery
    End Function
    Private Function ItemWiseCreditDebitNotesRegular6C()
        Dim strBaseQuery As String = Nothing
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()
        Dim WhrItemWiseDirectAP As String = " and  TSPL_VENDOR_MASTER.GSTRegistered=1  and   TSPL_VENDOR_INVOICE_HEAD.Document_Type in ('C','D')  and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0  "
        strBaseQuery = "select 'Credit/Debit Notes Regular - 6C' as DocType,xx.* from (" & _
            "" & strItemWiseDirectAP & "" & _
            " " & WhrItemWiseDirectAP & "" & _
            ") as XX"
        Return strBaseQuery
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" & _
     "" & strBaseQuery & "" & _
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
    End Function
    Private Function ItemWiseImportofGoods5()
        Dim strBaseQuery As String = Nothing
        Dim strItemWisePurchaseInvoice As String = ItemWIsePurchaseInvoice()
        Dim strItemWisePurchaseReturn As String = ItemWIsePurchaseReturn()
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()

        Dim WhrItemWisePurchaseInvoiceAndReturn As String = " and  isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0 and TSPL_PI_HEAD.PI_Type='I' "

        strBaseQuery = "select 'Import of Goods - 5' as DocType,XX.* from (" & _
            "" & strItemWisePurchaseInvoice & "" & _
            " " & WhrItemWisePurchaseInvoiceAndReturn & "" & _
         ") as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" & _
     "" & strBaseQuery & "" & _
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
        Return strBaseQuery

    End Function
    Private Function ItemWiseCreditDebitNotesUnregistered6C()
        Dim strBaseQuery As String = Nothing
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()
        Dim WhrItemWiseDirectAP As String = " and  TSPL_VENDOR_MASTER.GSTRegistered=0  and   TSPL_VENDOR_INVOICE_HEAD.Document_Type in ('C','D')  and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0  "

        strBaseQuery = "select 'Credit/Debit Notes Unregistered - 6C' as DocType,xx.* from (" & _
            "" & strItemWiseDirectAP & "" & _
            " " & WhrItemWiseDirectAP & "" & _
            " ) as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" & _
     "" & strBaseQuery & "" & _
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
        Return strBaseQuery
    End Function
    Private Function ItemWiseNilRatedInvoices7Summary()
        Dim strBaseQuery As String = Nothing
        Dim strItemWisePurchaseInvoice As String = ItemWIsePurchaseInvoice()
        Dim strItemWisePurchaseReturn As String = ItemWIsePurchaseReturn()
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()
        Dim strItemWiseBulkMilkPurchaseInvoice As String = ItemWiseBulkMilkPurchaseInvoice()
        Dim strItemWiseBulkMilkPurchaseReturn As String = ItemWiseBulkMilkPurchaseReturn()
        Dim strItemWiseMilkPurchaseInvoice As String = ItemWiseMilkPurchaseInvoice()
        Dim strItemWiseVCGL As String = itemWiseVCGL()

        Dim WhrCls As String = " and  (isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0)=0 ) "


        strBaseQuery = "select 'Nil Rated Invoices - 7 - (Summary)' as DocType,XX.* from ( " & _
        "" & strItemWisePurchaseInvoice & "" & _
           " " & WhrCls & "" & _
           " Union all " & _
       "" & strItemWisePurchaseReturn & "" & _
        " " & WhrCls & "" & _
       " Union all " & _
       "" & strItemWiseDirectAP & "" & _
        " " & WhrCls & "" & _
        " Union all " & _
         "" & strItemWiseBulkMilkPurchaseInvoice & "" & _
        " " & WhrCls & "" & _
        " Union all " & _
         "" & strItemWiseBulkMilkPurchaseReturn & "" & _
        " " & WhrCls & "" & _
        " Union all " & _
         "" & strItemWiseMilkPurchaseInvoice & "" & _
        " " & WhrCls & "" & _
         " Union all " & _
         "" & strItemWiseVCGL & "" & _
        " " & WhrCls & "" & _
        ") as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" & _
     "" & strBaseQuery & "" & _
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
        Return strBaseQuery
    End Function
    Private Function ItemWiseExempt()
        Dim strBaseQuery As String = Nothing
        Dim strItemWiseMilkPurchaseInvoice As String = ItemWiseMilkPurchaseInvoice()
        Dim strItemWiseBulkMilkPurchaseInvoice As String = ItemWiseBulkMilkPurchaseInvoice()
        Dim strItemWiseItemWiseBulkMilkPurchaseReturn As String = ItemWiseBulkMilkPurchaseReturn()


        Dim WhrCls As String = " and  isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0)=0 "
        strBaseQuery = "select 'Exempt' as DocType, XX.* from (" & _
            "" & strItemWiseMilkPurchaseInvoice & "" & _
           " " & WhrCls & "" & _
           " Union all " & _
       "" & strItemWiseBulkMilkPurchaseInvoice & "" & _
        " " & WhrCls & "" & _
       " Union all " & _
       "" & strItemWiseItemWiseBulkMilkPurchaseReturn & "" & _
        " " & WhrCls & "" & _
        ") as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" & _
     "" & strBaseQuery & "" & _
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
        Return strBaseQuery
    End Function
    Private Function ItemWiseNillRated()
        Dim strBaseQuery As String = Nothing
        Dim strNilRatedInvoices7Summary As String = ItemWiseNilRatedInvoices7Summary()
        Dim strExempt As String = ItemWiseExempt()

        strBaseQuery = "select pp.[Item code] as [Item Code],max([Item Description]) as [Item Description],max([HSN Code]) as [HSN Code],max(UOM) as [UOM],sum(Qty) as Qty,sum([Taxable Value]) as [Taxable Value],sum([CESS Amount]) as [CESS Amount],sum([CGST Amount]) as [CGST Amount],sum([SGST Amount]) as [SGST Amount],sum([IGST Amount]) as [IGST Amount],sum([UGST Amt]) as [UGST Amt],sum([CGST Amount]+[SGST Amount]+[IGST Amount]+[UGST Amt]) as [Total Tax],sum([Taxable Value]+[CGST Amount]+[SGST Amount]+[IGST Amount]+[UGST Amt]) as [Total Value] from (" & _
            " select *,1 as RI from (" & _
       "" & strNilRatedInvoices7Summary & "" & _
       ") as aa" & _
       " union all" & _
       " select *,-1 As RI from (" & _
        "" & strExempt & "" & _
             ") as bb" & _
              " ) as pp group by [item code] having sum(RI)>0  or isnull(max([item code]),'')=''"


        Return strBaseQuery
    End Function
    Private Function ItemWiseReversechargesupplies()
        Dim qry As String = Nothing
        qry = "select * from (" & _
            " select '' as Trans_Type,'' as document_No,'' as Document_Type,'' as Vendor_code,'' as AgainstDocNo ,'' as item_code,'' as UOM,0 as Item_cost,0 as Qty,0 as Amt_Less_Discount,0 as ItemWise_Exempt_Amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,'' as GSTRegistered,'' as PI_Type from TSPL_VENDOR_INVOICE_HEAD " & _
            ")  as xx where document_No <>'' "

        Return qry
    End Function

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & IIf(btnGSTR1.IsChecked = True, "GSTR1 Report of -- " & RadPageViewPage3.Text, "GSTR2 Report of -- " & RadPageViewPage3.Text) & "")


            If btnGSTR1.IsChecked = True Then
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember) + " "))
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember) + " "))
                End If
            Else
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
                End If
                If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVendGroup.arrDispalyMember) + " "))
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember) + " "))
                End If
            End If



            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            If gv3.Rows.Count > 0 Then
                'transportSql.exportdataChilRows(gv3, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
                transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow("No Data found")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & IIf(btnGSTR1.IsChecked = True, "GSTR1 Report of -- " & RadPageViewPage3.Text, "GSTR2 Report of -- " & RadPageViewPage3.Text) & "")


            If btnGSTR1.IsChecked = True Then
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember) + " "))
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember) + " "))
                End If
            Else
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
                End If
                If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVendGroup.arrDispalyMember) + " "))
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember) + " "))
                End If
            End If

            If gv3.Rows.Count > 0 Then
                clsCommon.MyExportToPDF(Me.Text, gv3, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data found")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
