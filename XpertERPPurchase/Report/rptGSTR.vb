Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices
Imports System.Threading
'' RICHA AGARWAL BHA/25/09/18-000567
Public Class rptGSTR
    Dim excelApp As Excel.Application = Nothing
    Dim workbook As Excel.Workbook = Nothing
    Dim sheet As Excel.Worksheet = Nothing
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim variable1 As String = Nothing
    Dim exportGSTR As Boolean = False
    Dim strQryGSTR As String = Nothing
    Dim isGVDoubleClick As Boolean = False
    Dim B2CDocumentAmountRangeSameState As Decimal = 0
    Dim B2CDocumentAmountRangeOtherState As Decimal = 0

    Dim chkWithoutGrid As Boolean = False

    Const colDummyInvoiceAmt As String = "colDummyInvoiceAmt"
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
            qry += " where 2=2 and Seg_No = '7' AND GIT='N'"

            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Try
            Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
            txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Code", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendGroup._My_Click
        Try
            Dim qry As String = " select Ven_Group_Code as Code,Group_desc as Name from TSPL_VENDOR_group"
            txtVendGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGroupMulSel", qry, "Code", "Code", txtVendGroup.arrValueMember, txtVendGroup.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rptAPReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("CODE", "varchar(20) NOT NULL PRIMARY KEY ")
            coll.Add("FileName", "Varchar(50) not null")
            coll.Add("FileData", "VarBinary(Max) null ")
            coll.Add("COMMENTS", "VARCHAR(500) NULL")
            coll.Add("Created_By", "varchar(12) NOT NULL")
            coll.Add("Created_Date", "Datetime NOT NULL")
            coll.Add("Modified_By", "varchar(12) NOT NULL")
            coll.Add("Modified_Date", "Datetime NOT NULL")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_GSTR_BLANK_SHEET", coll, "", False, False)
            Reset()
            B2CDocumentAmountRangeSameState = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.B2CDocumentAmountRange, clsFixedParameterCode.B2CDocumentAmountRange, Nothing))
            B2CDocumentAmountRangeOtherState = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.B2CDocumentAmountRangeOtherState, clsFixedParameterCode.B2CDocumentAmountRangeOtherState, Nothing))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
        btnUpldBlnkGSTRExl.Visible = False
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If btnGSTR1.IsChecked Then
            VarID += "_A"
            gv1.VarID = VarID
        ElseIf btnGSTR2.IsChecked Then
            VarID += "_B"
            gv2.VarID = VarID
        End If
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myCDate(txtFromDate.Value, "dd/MM/yyyy") > clsCommon.myCDate(txtToDate.Value, "dd/MM/yyyy") Then
                Throw New Exception("From Date can't be greater than To Date !")
            End If
            GetReportGridID()
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
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
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
                If btnGSTR1.IsChecked Then
                    For i As Integer = 0 To gv2.Rows.Count - 1
                        If clsCommon.CompairString(gv2.Rows(i).Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(gv2.Rows(i).Cells("Name").Value, "Reverse charge supplies") = CompairStringResult.Equal Then
                            gv2.Rows(i).Cells("SNo").Value = ""
                        End If
                    Next
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            'ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function GSTR1DataHeader() As String
        Dim qryheader As String = String.Empty
        Dim qryfooter As String = String.Empty
        Dim dt As New DataTable
        Dim Wrcls As String = String.Empty
        Try
            Wrcls += "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & txtToDate.Value & "',103) "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" & clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) & ")"
            End If


            ''for header grid data
            '-- ar invocie all transactions
            qryheader = " select 'Total number of vouchers for the period' as Name,count(*) from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 " & Wrcls & "  " & Environment.NewLine &
            " ------------------------ ar invoice Against-----------------" & Environment.NewLine &
            " union all " & Environment.NewLine &
            " select 'Included in returns' as Name,count (*) from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & "  and Order_No<>'C' " & Environment.NewLine &
            "------------------------------- ar invoice Against HSN/SAC Information (to be provided)------------------------- " & Environment.NewLine &
            " union all " & Environment.NewLine &
            " select 'Included in HSN/SAC Summary' as Name,count (*) from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 and (isnull(AgainstScrapReturn,'')<>'' or ISNULL (Against_Sale_Return_No,'')<>'' or ISNULL (Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & "  and Order_No<>'C'  " & Environment.NewLine &
            " -------------- Incomplete HSN/SAC information (to be provided)------------------------ " & Environment.NewLine &
            " union all " & Environment.NewLine &
            " select 'Incomplete HSN/SAC information (to be provided)' as Name,0 " & Environment.NewLine &
            " ------------------------- ar invoice direct -------------------------- " & Environment.NewLine &
            " union all  " & Environment.NewLine &
            " select 'Not relevant for returns' as Name, sum(AllInvoices)-sum(InvoicesAgainst) as 'DirectInvoices' from ( " & Environment.NewLine &
            " select count(*) as 'AllInvoices',0 as 'InvoicesAgainst' from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 " & Wrcls & " and Order_No='C' " & Environment.NewLine &
            " union " & Environment.NewLine &
            " select 0 as 'AllInvoices',count(*) as 'InvoicesAgainst' from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 and (isnull(AgainstScrapReturn,'')<>'' or ISNULL (Against_Sale_Return_No,'')<>'' or ISNULL (Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " and Order_No='C' " & Environment.NewLine &
            " )z " & Environment.NewLine &
            "-------------------------- Incomplete/Mismatch in information (to be resolved)----------------" & Environment.NewLine &
            " union all " & Environment.NewLine &
            " select 'Incomplete/Mismatch in information (to be resolved)' as Name,0 " & Environment.NewLine
            ''====================
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return qryheader
    End Function

    Function GSTR1DataFooter() As String

        Dim qryfooter As String = String.Empty
        Dim dt As New DataTable
        Dim Wrcls As String = String.Empty
        Try
            Wrcls += "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & txtToDate.Value & "',103) "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" & clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) & ")"
            End If

            ''for footer grid data
            qryfooter = " -------------------------------------------------------registered customer against------------------------------------------------" & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue)+SUM(IsNUll(MandiAmount,0))+SUM(IsNull(KKFAmount,0)) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount1]  from ( " & Environment.NewLine &
            " select '1' as SNo,'B2B Invoices - 4A, 4B, 4C, 6B, 6C' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, 
            CASE WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as TCSAmount,	  
 CASE WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge   from TSPL_Customer_Invoice_Head " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine &
            " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine &
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine &
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Wrcls & " And Tax_Master.Is_Tax_Exempted<>1 And Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='Y' " & Environment.NewLine &
                        " UNION " & Environment.NewLine &
            " select '1' as SNo, 'B2B Invoices - 4A, 4B, 4C, 6B, 6C' as Name, '',0,0,0,0,0,0,0,0) z  group by Name " & Environment.NewLine &
            " union all " & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue)+SUM(IsNUll(MandiAmount,0))+SUM(IsNull(KKFAmount,0)) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount] ,0 as [Voucher Count1] ,0 as [Taxable Value1],0 as [Taxable Amount1],0 as [RoundOffAmount1],0 as [Invoice Amount1] from ( " & Environment.NewLine &
            " select '1a' as SNo,'Taxable Sales' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount,
            CASE WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as TCSAmount,	  
 CASE WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge  from TSPL_Customer_Invoice_Head " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
             " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine &
             " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine &
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine &
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Wrcls & "  And Tax_Master.Is_Tax_Exempted<>1 And Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='Y' " & Environment.NewLine &
                        " UNION " & Environment.NewLine &
            " select '1a' as SNo, 'Taxable Sales' as Name, '',0,0,0,0,0,0,0,0) z  group by Name " & Environment.NewLine &
            "union all " & Environment.NewLine &
            " select '1b' as SNo,'Reverse charge supplies' as Name, 0,0,0,0,0,0,0,0,0,0  " & Environment.NewLine &
            " union all " & Environment.NewLine &
            " ----------------------------  -------------------------unregistered customer against-------------------------------------------------------------------------------- " & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue)+SUM(IsNUll(MandiAmount,0))+SUM(IsNull(KKFAmount,0)) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount1]  from ( " & Environment.NewLine &
            " select '2' as SNo,'B2C(Large) Invoices - 5A, 5B' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,
            CASE WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as TCSAmount,	  
 CASE WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge
            from TSPL_Customer_Invoice_Head  " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine &
            " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            "  Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' " & Environment.NewLine &
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 " & Environment.NewLine &
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine &
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Environment.NewLine &
            " And IsNull(TSPL_COMPANY_MASTER.State,'')<>'' And ISNULL(TSPL_CUSTOMER_MASTER.State,'')<>'' and  TSPL_Customer_Invoice_Head.Document_Total>(Case  When TSPL_COMPANY_MASTER.state = tspl_customer_master.state Then " + clsCommon.myCstr(B2CDocumentAmountRangeSameState) + " Else " + clsCommon.myCstr(B2CDocumentAmountRangeOtherState) + " End) " & Wrcls & " and TSPL_Customer_Invoice_Head.Document_Total>0 And Tax_Master.Is_Tax_Exempted<>1 And Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='Y'   " & Environment.NewLine &
            " UNION " & Environment.NewLine &
            " select '2' as SNo, 'B2C(Large) Invoices - 5A, 5B' as Name, '', 0,0,0,0,0,0,0,0) z group by Name " & Environment.NewLine &
            " union all " & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue)+SUM(IsNUll(MandiAmount,0))+SUM(IsNull(KKFAmount,0)) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount1]  from ( " & Environment.NewLine &
            " select '3' as SNo,'B2C(Small) Invoices - 7' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,
            CASE WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as TCSAmount,	  
 CASE WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge
            from TSPL_Customer_Invoice_Head  " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
            " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            "  Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' " & Environment.NewLine &
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 " & Environment.NewLine &
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine &
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " & Environment.NewLine &
            " And IsNull(TSPL_COMPANY_MASTER.State,'')<>'' And ISNULL(TSPL_CUSTOMER_MASTER.State,'')<>'' and  TSPL_Customer_Invoice_Head.Document_Total<=(Case  When TSPL_COMPANY_MASTER.state = tspl_customer_master.state Then " & clsCommon.myCstr(B2CDocumentAmountRangeSameState) & " Else " & clsCommon.myCstr(B2CDocumentAmountRangeOtherState) & " End) " & Wrcls & "  and TSPL_Customer_Invoice_Head.Document_Total>0 And Tax_Master.Is_Tax_Exempted<>1 And Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='Y' " & Environment.NewLine &
            " UNION " & Environment.NewLine &
            " select '3' as SNo, 'B2C(Small) Invoices - 7' as Name, '',0,0,0,0,0,0,0,0) z  group by Name " & Environment.NewLine &
            " union all " & Environment.NewLine &
            " -------------------------------------------------------registered customer direct------------------------------------------------ " & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue)+SUM(IsNUll(MandiAmount,0))+SUM(IsNull(KKFAmount,0)) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount1]  from ( " & Environment.NewLine &
            " select '4' as SNo,'Credit/Debit Notes(Registered) - 9B' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,
            CASE WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as TCSAmount,	  
 CASE WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge
            from TSPL_Customer_Invoice_Head  " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
            " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') " & Environment.NewLine &
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine &
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' or  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='') " & Wrcls & " And Tax_Master.Is_Tax_Exempted<>1 And Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='Y' " & Environment.NewLine &
            " UNION " & Environment.NewLine &
            " select '4' as SNo, 'Credit/Debit Notes(Registered) - 9B' as Name, '',0,0,0,0,0,0,0,0) z group by Name " & Environment.NewLine &
            " union all " & Environment.NewLine &
            " -------------------------------------------------------unregistered customer direct------------------------------------------------ " & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue)+SUM(IsNUll(MandiAmount,0))+SUM(IsNull(KKFAmount,0)) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount1]  from ( " & Environment.NewLine &
            " select '5' as SNo,'Credit/Debit Notes(Unregistered) - 9B' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,
            CASE WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as TCSAmount,	  
 CASE WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge
            from TSPL_Customer_Invoice_Head  " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code  " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
            " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            " where 1=1 and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D')" & Environment.NewLine &
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine &
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' or  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='') " & Wrcls & " And Tax_Master.Is_Tax_Exempted<>1 And Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='Y'  " & Environment.NewLine &
            " UNION " & Environment.NewLine &
            " select '5' as SNo, 'Credit/Debit Notes(Unregistered) - 9B' as Name, '',0,0,0,0,0,0,0,0) z  group by Name " & Environment.NewLine &
            " union all" & Environment.NewLine &
            " -------------------------------------------------------registered/unregistered customer against export and Merchant------------------------------------------------ " & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue)+SUM(IsNUll(MandiAmount,0))+SUM(IsNull(KKFAmount,0)) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount1]  from ( " & Environment.NewLine &
            " select '6' as SNo,'Exports Invoices - 6A' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,
            CASE WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as TCSAmount,	  
 CASE WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge
            from TSPL_Customer_Invoice_Head " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
            " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            " where 1=1 AND TSPL_Customer_Invoice_Head.Against_Sale_No  IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Wrcls & " And Tax_Master.Is_Tax_Exempted<>1 And Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='Y' " & Environment.NewLine &
            " UNION " & Environment.NewLine &
            " select '6' as SNo, 'Exports Invoices - 6A' as Name, '',0,0,0,0,0,0,0,0) z group by Name " & Environment.NewLine &
            " union all  " & Environment.NewLine &
            " select '7' as SNo, 'Tax Liability(Advances received) - 11A(1), 11A(2)' as Name, 0,0, 0, 0,0,0, 0, 0,0,0 " & Environment.NewLine &
            " union all " & Environment.NewLine &
            " select '8' as SNo,'Adjustment of Advances - 11B(1), 11B(2)' as Name, 0,0, 0, 0,0,0, 0, 0,0,0   " & Environment.NewLine &
            " union all" & Environment.NewLine &
            " -------------------------------------------------------registered/unregistered customer Nil Rated Invoices - 8A, 8B, 8C, 8D------------------------------------------------" & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(InvoiceAmount-Total_Add_Charge) - Sum(Total_Tax) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount-Total_Add_Charge) as [Invoice Amount1]  from ( " & Environment.NewLine &
            " select '9' as SNo,'Nil Rated Invoices - 8A, 8B, 8C, 8D' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,TSPL_Customer_Invoice_Head.Total_Tax, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  
           CASE WHEN Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
                when Tax_Master.Type ='O' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) END  as TaxableAmount,
 CASE WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Is_TCS ='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END  as TCSAmount,	  
 CASE WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type IN ('M') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type  IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type IN ('K') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount,
            case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge  from TSPL_Customer_Invoice_Head  " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
            " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            " where  isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' And  TSPL_Customer_Invoice_Head.Against_Sale_No not IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Wrcls & " and Tax_Master.Is_Tax_Exempted=1 and Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='N' " & Environment.NewLine &
            " UNION " & Environment.NewLine &
            " select '9' as SNo, 'Nil Rated Invoices - 8A, 8B, 8C, 8D' as Name, '',0,0,0,0,0,0,0,0,0) z  group by Name " & Environment.NewLine &
              " union all" & Environment.NewLine &
            " -------------------------------------------------------Taxable Invoice (Direct)------------------------------------------------ " & Environment.NewLine &
            " select max(SNo) as SNo,max(Name) as Name,count(VoucherCount)-1 as [Voucher Count] ,sum(TaxableValue)+SUM(IsNUll(MandiAmount,0))+SUM(IsNull(KKFAmount,0)) as [Taxable Value],sum(TaxableAmount)-Sum(TCSAmount)-SUM(IsNUll(MandiAmount,0))-SUM(IsNull(KKFAmount,0)) as [Taxable Amount],sum(RoundOffAmount) as [RoundOffAmount],sum(InvoiceAmount) as [Invoice Amount],count(VoucherCount)-1 as [Voucher Count1] ,sum(TaxableValue) as [Taxable Value1],sum(TaxableAmount) as [Taxable Amount1],sum(RoundOffAmount) as [RoundOffAmount1],sum(InvoiceAmount) as [Invoice Amount1]  from ( " & Environment.NewLine &
            " select '10' as SNo,'Taxable Invoice (Direct)' AS Name,TSPL_Customer_Invoice_Head.Document_No as VoucherCount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))  as TaxableValue,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)   as TaxableAmount, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as InvoiceAmount,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as RoundOffAmount,
            CASE WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.is_TCS='Y' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as TCSAmount,	  
 CASE WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='M' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as MandiAmount,
 CASE WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN Tax_Master.Type='K' THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      END   as KKFAmount,TSPL_Customer_Invoice_Head.Total_Add_Charge
            from TSPL_Customer_Invoice_Head  " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
            " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=TSPL_Customer_Invoice_Head.Tax_Group " & Environment.NewLine &
            " where 1=1  and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 AND TSPL_Customer_Invoice_Head.Document_Type ='I'" & Environment.NewLine &
            " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) " & Environment.NewLine &
            " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='')  " & Wrcls & "  And Tax_Master.Is_Tax_Exempted<>1 And Tax_Master.Tax_Group_Type='S' And Tax_Master.Is_TCS='Y' " & Environment.NewLine &
            " UNION" & Environment.NewLine &
            " select '10' as SNo, 'Taxable Invoice (Direct)' as Name, '',0,0,0,0,0,0,0,0) z group by Name " & Environment.NewLine

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
            TotalClosing1.FormatString = "{0:N2}"
            TotalClosing1.Name = "Taxable Amount"
            TotalClosing1.AggregateExpression = "sum([Taxable Amount1])"
            summaryRowItem.Add(TotalClosing1)

            Dim TotalClosing2 As New GridViewSummaryItem()
            TotalClosing2.FormatString = "{0:N2}"
            TotalClosing2.Name = "Taxable Value"
            TotalClosing2.AggregateExpression = "sum([Taxable Value1])"
            summaryRowItem.Add(TotalClosing2)

            Dim TotalClosing5 As New GridViewSummaryItem()
            TotalClosing5.FormatString = "{0:N2}"
            TotalClosing5.Name = "RoundOffAmount"
            TotalClosing5.AggregateExpression = "sum(RoundOffAmount1)"
            summaryRowItem.Add(TotalClosing5)

            Dim TotalClosing4 As New GridViewSummaryItem()
            TotalClosing4.FormatString = "{0:N2}"
            TotalClosing4.Name = "Invoice Amount"
            ' TotalClosing4.AggregateExpression = "sum([Invoice Amount1])"
            TotalClosing4.AggregateExpression = "sum([Invoice Amount1])"
            summaryRowItem.Add(TotalClosing4)

        Else

            gv2.Columns("Name").Width = 350
            gv2.Columns("Name").HeaderText = "Particulars"

            gv2.Columns("TaxableValue").Width = 100
            gv2.Columns("TaxableValue").HeaderText = "Taxable Value"

            gv2.Columns("TaxableAmount").Width = 100
            gv2.Columns("TaxableAmount").HeaderText = "Tax Amount"

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

        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv2.MasterTemplate.ShowTotals = True
    End Sub

    Sub gv3BlankGridColumns()
        gv3.DataSource = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        gv3.Refresh()
        gv3.MasterTemplate.Refresh()

        Dim repoDummyInvoiceAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDummyInvoiceAmt.FormatString = ""
        repoDummyInvoiceAmt.HeaderText = "Invoice Amt"
        repoDummyInvoiceAmt.Name = colDummyInvoiceAmt
        repoDummyInvoiceAmt.Width = 100
        repoDummyInvoiceAmt.ReadOnly = False
        repoDummyInvoiceAmt.IsVisible = True
        gv3.MasterTemplate.Columns.Add(repoDummyInvoiceAmt)
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

            gv3.Columns("Taxable Value").Width = 110
            gv3.Columns("Taxable Value").HeaderText = "Taxable Value"

            gv3.Columns("Taxable Amount").Width = 110
            gv3.Columns("Taxable Amount").HeaderText = "Tax Amount"

            gv3.Columns("Invoice Amount").Width = 110
            gv3.Columns("Invoice Amount").HeaderText = "Invoice Amount"

            'gv3.Columns("Invoice Type").IsVisible = False
            'gv3.Columns("Invoice Type").HeaderText = "Invoice Type"

            'Dim item20 As New GridViewSummaryItem(colDummyInvoiceAmt, "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item20)

            'Dim summaryItem1 As New GridViewSummaryItem()
            'summaryItem1.FormatString = "{0:F2}"
            'summaryItem1.Name = "Invoice Amount"
            'summaryItem1.AggregateExpression = "sum([InvoiceAmountDisc])"
            'summaryRowItem.Add(summaryItem1)

            Dim item3 As New GridViewSummaryItem("Taxable Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)

            Dim item4 As New GridViewSummaryItem("Taxable Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)


            Dim chkPrevDoc As String = Nothing

            Dim Value As Decimal = 0
            'For Each gvRow As GridViewRowInfo In gv3.Rows
            '    'If clsCommon.CompairString(gvRow.Cells("Particulars").Value, "B2B Invoices - 4A, 4B, 4C, 6B, 6C") = CompairStringResult.Equal Then
            '    If chkPrevDoc IsNot Nothing AndAlso clsCommon.myLen(chkPrevDoc) > 0 Then
            '        If Not clsCommon.CompairString(chkPrevDoc, clsCommon.myCstr(gvRow.Cells("Document No").Value)) = CompairStringResult.Equal Then
            '            'Value += clsCommon.myCDecimal(gvRow.Cells("Invoice Amount").Value)
            '            item5 = New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
            '        End If
            '    Else
            '        'Value += clsCommon.myCDecimal(gvRow.Cells("Invoice Amount").Value)
            '        item5 = New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
            '    End If
            '    chkPrevDoc = clsCommon.myCstr(gvRow.Cells("Document No").Value)
            '    'End If
            'Next
            'summaryRowItem.Add(item5)

            'Dim item5 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item5)


            Dim item6 As New GridViewSummaryItem("Round Off Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
        ElseIf btnGSTR1.IsChecked = True And chkItemWise.Checked = True Then
            If isGVDoubleClick Then
                gv3.Columns("Taxable Value").Width = 110
                gv3.Columns("Taxable Value").HeaderText = "Taxable Value"
                gv3.Columns("Taxable Value").FormatString = "{0:F2}"

                gv3.Columns("Taxable Amount").Width = 110
                gv3.Columns("Taxable Amount").HeaderText = "Taxable Amount"
                gv3.Columns("Taxable Amount").FormatString = "{0:F2}"

                gv3.Columns("Invoice Amount").Width = 110
                gv3.Columns("Invoice Amount").HeaderText = "Invoice Amount"
                gv3.Columns("Invoice Amount").FormatString = "{0:F2}"

                gv3.Columns("Round Off Amount").Width = 110
                gv3.Columns("Round Off Amount").HeaderText = "Round Off Amount"
                gv3.Columns("Round Off Amount").FormatString = "{0:F2}"

                Dim item11 As New GridViewSummaryItem("Taxable Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item11)

                Dim item12 As New GridViewSummaryItem("Taxable Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item12)

                Dim item13 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item13)

                Dim item14 As New GridViewSummaryItem("Round Off Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item14)
            Else
                'richa GKD/05/02/19-000174 6 Feb,2019
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
        End If

        gv3.ShowGroupPanel = False
        gv3.MasterTemplate.AutoExpandGroups = True
        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv3.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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
        Basequery = " select TSPL_VENDOR_INVOICE_HEAD.Document_No as Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.GSTFinalNo ,TSPL_STATE_MASTER.STATE_CODE ,TSPL_STATE_MASTER.STATE_NAME ,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,tspl_vendor_master.Vendor_group_code," &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice' else " &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then 'Purchase Return' else" &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then 'Bulk Milk Purchase Invoice' else" &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then 'Milk Purchase Invoice'else" &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then 'VCGL'else" &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then 'Acquisition Entry'else" &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then 'Asset Work'else" &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then 'VSP Item Issue' else" &
           " case when (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'') then 'Bulk Milk Purchase Return' else 'Direct AP'" &
            " end end end  end end end end end end as Trans_Type," &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  else " &
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  else" &
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No else" &
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No else" &
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL else" &
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition else" &
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work else" &
             " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then Against_VSPItemIssue_No else case when (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'') then TSPL_VENDOR_INVOICE_HEAD.RefDocNo else  TSPL_VENDOR_INVOICE_HEAD.Document_No " &
              " end end end end end end end end end as AgainstDocNo," &
               " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *  isnull(TSPL_VENDOR_INVOICE_detail.Amount_less_Discount,0)  as TaxableValue," &
" case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " &
                "   +case when  Tax2_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " &
                 "  +case when  Tax3_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " &
                 " +case when  Tax4_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " &
                 "   +case when  Tax5_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " &
                 "  ) as Exempt_Amt " &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " &
                 "  +case when  Tax2_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " &
                 "  +case when  Tax3_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " &
                 " +case when  Tax4_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " &
                  "  +case when  Tax5_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " &
                  " ) as CGST_Amt" &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " &
                  " +case when  Tax2_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " &
                  " +case when  Tax3_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " &
                  " +case when  Tax4_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " &
                   " +case when  Tax5_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " &
                  " ) as SGST_Amt" &
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * (case when  Tax1_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " &
                 "  +case when  Tax2_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " &
                  " +case when  Tax3_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " &
                 " +case when  Tax4_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " &
                 "   +case when  Tax5_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " &
                  " ) as UGST_Amt" &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax1_Amt,0)else 0 end " &
                 "  +case when  Tax2_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_amt,0)else 0 end " &
                  " +case when  Tax3_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax3_amt,0)else 0 end " &
                 " +case when  Tax4_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_amt,0)else 0 end " &
                  "  +case when  Tax5_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax5_amt,0)else 0 end " &
                  " ) as IGST_Amt" &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " &
                 "  +case when  Tax2_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_Rate,0)else 0 end " &
                  " +case when  Tax3_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate,0)else 0 end " &
                 " +case when  Tax4_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_Rate,0)else 0 end " &
                  "  +case when  Tax5_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate,0)else 0 end " &
                  " ) as Exempt_Rate " &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " &
                 "  +case when  Tax2_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_Rate,0)else 0 end " &
                 "  +case when  Tax3_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate,0)else 0 end " &
                 " +case when  Tax4_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_Rate,0)else 0 end " &
                  "  +case when  Tax5_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate,0)else 0 end " &
                  " ) as CGST_Rate" &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " &
                  " +case when  Tax2_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_Rate,0)else 0 end " &
                  " +case when  Tax3_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate,0)else 0 end " &
                 " +case when  Tax4_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_Rate,0)else 0 end " &
                  "  +case when  Tax5_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate,0)else 0 end " &
                  " ) as SGST_Rate" &
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * (case when  Tax1_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " &
                 "  +case when  Tax2_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax2_Rate,0)else 0 end " &
                  " +case when  Tax3_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate,0)else 0 end " &
                  " +case when  Tax4_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.tax4_Rate,0)else 0 end " &
                  "  +case when  Tax5_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate,0)else 0 end " &
                  " ) as UGST_Rate" &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX1_Rate,0)else 0 end " &
                  " +case when  Tax2_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX2_Rate,0)else 0 end " &
                  " +case when  Tax3_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX3_Rate ,0)else 0 end " &
                 " +case when  Tax4_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX4_Rate,0)else 0 end " &
                 "   +case when  Tax5_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_detail.TAX5_Rate ,0)else 0 end " &
                  " ) as IGST_Rate," &
 " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0)   as TaxableAmount, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0)  as InvoiceAmount," &
            " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount" &
            " ,Against_POInvoice_No,Against_PurchaseReturn_No,Against_BulkMillkPurchaseInvoice_No,Against_MillkPurchaseInvoice_No,RefDocType,RefDocNo,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_MASTER.GSTRegistered as GSTRegistered,TSPL_PI_HEAD.PI_Type,TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition,TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No" &
             " from TSPL_VENDOR_INVOICE_HEAD " &
                " left join TSPL_VENDOR_INVOICE_detail on TSPL_VENDOR_INVOICE_detail.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no" &
                " left join tspl_item_master on tspl_item_master.item_code=TSPL_VENDOR_INVOICE_detail.item_code " &
                " left join tspl_tax_master as Tax1_Code on  Tax1_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax1 " &
                " left join tspl_tax_master as Tax2_Code on  Tax2_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax2" &
                " left join tspl_tax_master as Tax3_Code on  Tax3_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax3" &
                " left join tspl_tax_master as Tax4_Code on  Tax4_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax4" &
                " left join tspl_tax_master as Tax5_Code on  Tax5_Code.tax_code=TSPL_VENDOR_INVOICE_detail.tax5" &
                " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code " &
                " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code " &
                " left join TSPL_STATE_MASTER on  TSPL_STATE_MASTER.STATE_CODE =tspl_vendor_master.state_code" &
                " left join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_group_code " &
                " left join TSPL_PI_HEAD on TSPL_PI_HEAD.pi_no=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No"

        '" ) as pp group by Document_no "
        Basequery = " select * from (select Document_No ,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(Vendor_Code) as Vendor_Code,max(Vendor_Name) as Vendor_Name," &
" max(GSTFinalNo) as GSTFinalNo, max(STATE_CODE ) as STATE_CODE,max(STATE_NAME) as STATE_NAME," &
 " max(Loc_Code) as Loc_Code,max(Vendor_group_code) as Vendor_group_code,max(Trans_Type) as Trans_Type,max(AgainstDocNo) as AgainstDocNo,sum(TaxableValue ) as TaxableValue,sum(Exempt_Amt+CGST_Amt+SGST_Amt+UGST_Amt +IGST_Amt ) as TaxableAmount,sum(Exempt_Rate +CGST_Rate +SGST_Rate +UGST_Rate  +IGST_Rate ) as TaxableRate,sum(TaxableValue+Exempt_Amt+CGST_Amt+SGST_Amt+UGST_Amt +IGST_Amt) as InvoiceAmount,max(RoundOffAmount) as RoundOffAmount," &
" max(Against_POInvoice_No) as Against_POInvoice_No,max(Against_PurchaseReturn_No) as Against_PurchaseReturn_No,max(Against_BulkMillkPurchaseInvoice_No) as Against_BulkMillkPurchaseInvoice_No,max(Against_MillkPurchaseInvoice_No) as Against_MillkPurchaseInvoice_No,max(RefDocType) as RefDocType,max(RefDocNo) as RefDocNo,max(Document_Type) as Document_Type,max(GSTRegistered) as GSTRegistered,max(PI_Type) as PI_Type,max(Against_VCGL) as Against_VCGL,max(Against_Acquisition) as Against_Acquisition,max(Against_Asset_Work) as Against_Asset_Work,max(Against_VSPItemIssue_No)  as Against_VSPItemIssue_No" &
 " from (" &
        " " & Basequery & " " &
        " ) as Detail group by document_no  " &
       " ) as qq "

        Return Basequery

    End Function
    Private Function UnionBaseQry() As String
        Dim UnionQry As String = Nothing

        UnionQry = " select '' as Document_no,'" & txtFromDate.Value & "' as Invoice_Entry_date,'' as Vendor_code,'' as Vendor_Name,'' as STATE_CODE,'' as STATE_NAME,'' as GSTFinalNo,'' as Loc_Code,'' as Vendor_Group_Code,'' as Trans_type,'' as AgainstDocNo,0 as TaxableValue,0 as TaxableRate,0 as TaxableAmount,0 as InvoiceAmount,0 as RoundOFFAMount" &
            " ,'' as Against_POInvoice_No,'' as Against_PurchaseReturn_No,'' as Against_BulkMillkPurchaseInvoice_No,'' as Against_MillkPurchaseInvoice_No,'' as RefDocType,'' as RefDocNo,'' as Document_Type ,0 as GSTRegistered,'' as PI_Type,'' as Against_VCGL,'' as Against_Acquisition,'' as Against_Asset_Work,'' as Against_VSPItemIssue_No "

        Return UnionQry
    End Function

    Private Function Numberofvouchersfortheperiod() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and  convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103)  "

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If

        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 0 as count, 'Number of vouchers for the period' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" &
            "" & strBaseQuery & "" &
             " " & Wrcls & " " &
            " union all" &
               "" & strUnionBaseQuery & "" &
                ") as xx"

        Return Qry
    End Function
    Private Function Includedinreturns() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing

        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103)  " &
                " and (isnull(qq.Against_POInvoice_No,'')<>''" &
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>''" &
                " or isnull(qq.Against_BulkMillkPurchaseInvoice_No  ,'')<>''" &
                 " or isnull(qq.Against_MillkPurchaseInvoice_No  ,'')<>''" &
                 " or (isnull(qq.RefDocType ,'')='BM-PR' and isnull(qq.RefDocNo ,'')<>'')  )"


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If


        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()
        Qry = "select 1 as count, 'Included in returns' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" &
            "" & strBaseQuery & "" &
              " " & Wrcls & " " &
            " union all" &
               "" & strUnionBaseQuery & "" &
           ") as xx"
        Return Qry
    End Function
    Private Function Invoicesreadyforreturns() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing

        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) " &
                " and (isnull(qq.Against_POInvoice_No,'')<>''" &
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>''" &
                " or isnull(qq.Against_BulkMillkPurchaseInvoice_No  ,'')<>''" &
                 " or isnull(qq.Against_MillkPurchaseInvoice_No  ,'')<>''" &
         " or (isnull(qq.RefDocType ,'')='BM-PR' and isnull(qq.RefDocNo ,'')<>'')  )"

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If


        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 2 as count,'Invoices ready for returns' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" &
            "" & strBaseQuery & "" &
             " " & Wrcls & " " &
            " union all" &
               "" & strUnionBaseQuery & "" &
            ") as xx"
        Return Qry

    End Function
    Private Function Invoiceswithmismatchininformation()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 3 as count, 'Invoices with mismatch in information' as Type,0 as DocCount,* from ( " &
            "" & strUnionBaseQuery & "" &
        " ) as XX "
        Return qry
    End Function
    Private Function Notincludedinreturnsduetoincompleteinformation()
        Dim qry As String = Nothing
        Try
            Dim strUnionBaseQuery As String = Nothing
            strUnionBaseQuery = UnionBaseQry()
            qry = "select 4 as count, 'Not included in returns due to incomplete information' as Type,0 as DocCount,* from ( " &
                "" & strUnionBaseQuery & "" &
            " ) as XX "
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return qry
    End Function
    Private Function Notrelevantforreturns()
        Dim qry As String = Nothing
        Try
            Dim strNumberofvouchersfortheperiod As String = Nothing
            strNumberofvouchersfortheperiod = Numberofvouchersfortheperiod()
            Dim strIncludedinreturns As String = Nothing
            strIncludedinreturns = Includedinreturns()


            qry = " select 5 as count,'Not relevant for returns' as Type,*" &
                " from ( select max(DocCount) as DocCount,Document_no as Document_no,max(Trans_type) as Trans_type,max(AgainstDocNo ) as AgainstDocNo," &
              " max(Invoice_Entry_date) as Invoice_Entry_date ,max(Vendor_code) as Vendor_code,  max(Loc_Code) as Loc_Code,max(Vendor_Group_Code) as Vendor_Group_Code," &
              " sum(TaxableValue) as TaxableValue,sum(TaxableAmount) as TaxableAmount,sum(TaxableRate ) as TaxableRate, max(InvoiceAmount) as InvoiceAmount,max(RoundOFFAMount) as RoundOFFAMount," &
               " max(Against_POInvoice_No) as Against_POInvoice_No, " &
                     " max(Against_PurchaseReturn_No) as Against_PurchaseReturn_No,max(Against_BulkMillkPurchaseInvoice_No) as Against_BulkMillkPurchaseInvoice_No, " &
       " max(Against_MillkPurchaseInvoice_No) as Against_MillkPurchaseInvoice_No,max(RefDocType) as RefDocType,max(RefDocNo) as RefDocNo,max(Document_Type) as Document_Type, " &
       " max(GSTRegistered) as GSTRegistered,max(PI_Type) as PI_Type,max(Against_VCGL) as Against_VCGL,max(Against_Acquisition) as Against_Acquisition, " &
       " max(Against_Asset_Work) as Against_Asset_Work,max(Against_VSPItemIssue_No) as Against_VSPItemIssue_No " &
        " from ( " &
                " select bb.*,1 as RI from ( " &
            "" & strNumberofvouchersfortheperiod & "" & Environment.NewLine &
            ") as bb" &
            " Union all" & Environment.NewLine &
             " select YY.*,-1 as RI from ( " &
            "" & strIncludedinreturns & "" &
            ")  as YY " &
            ") as aa" &
            " group by Document_no  having sum(RI)>0 and isnull(Document_no,'')<>''" &
            " ) as xx "
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return qry
    End Function
    Private Function IncompleteHSNSACinformation()
        Dim qry As String = Nothing
        Try
            Dim strUnionBaseQuery As String = Nothing
            strUnionBaseQuery = UnionBaseQry()
            qry = "select 6 as count, 'Incomplete HSN/SAC information (to be provided)' as Type,0 as DocCount,* from ( " &
                "" & strUnionBaseQuery & "" &
            " ) as XX "
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return qry
    End Function
    Private Function GSTQryforHeader() As String
        Dim qry As String
        Try
            Dim StrNumberOfVouchersForthePeriod As String = Numberofvouchersfortheperiod()
            Dim StrIncludedinreturns As String = Includedinreturns()
            Dim StrInvoicesreadyforreturns As String = Invoicesreadyforreturns()
            Dim strInvoiceswithmismatchininformation As String = Invoiceswithmismatchininformation()
            Dim strNotincludedinreturnsduetoincompleteinformation As String = Notincludedinreturnsduetoincompleteinformation()
            Dim strNotrelevantforreturns As String = Notrelevantforreturns()
            Dim strIncompleteHSNSACinformation As String = IncompleteHSNSACinformation()

            qry = " select type as Name, sum(DocCount)-1 as Column1 " &
            " from ( " &
            " " & StrNumberOfVouchersForthePeriod & " " & Environment.NewLine &
            " ) as aa  group by type,count" &
            " Union all " & Environment.NewLine &
            " select type as Name, sum(DocCount)-1 as Column1 " &
            " from ( " &
             " " & StrIncludedinreturns & " " & Environment.NewLine &
              " ) as aa  group by type,count" &
                 " Union all " & Environment.NewLine &
                 " select type as Name, sum(DocCount)-1 as Column1 " &
            " from ( " &
             " " & StrInvoicesreadyforreturns & " " & Environment.NewLine &
              " ) as aa  group by type,count" &
              " Union all " & Environment.NewLine &
              " select type as Name, sum(DocCount) as Column1 " &
            " from ( " &
             " " & strInvoiceswithmismatchininformation & " " & Environment.NewLine &
              " ) as aa  group by type,count" &
              " Union all " & Environment.NewLine &
              " select type as Name, sum(DocCount) as Column1 " &
            " from ( " &
             " " & strNotincludedinreturnsduetoincompleteinformation & " " & Environment.NewLine &
              " ) as aa  group by type,count" &
              " Union all " & Environment.NewLine &
              " select type as Name, sum(DocCount) as Column1 " &
            " from ( " &
             " " & strNotrelevantforreturns & " " & Environment.NewLine &
              " ) as aa  group by type,count" &
               " Union all " & Environment.NewLine &
               " select type as Name, sum(DocCount) as Column1 " &
            " from ( " &
             " " & strIncompleteHSNSACinformation & " " & Environment.NewLine &
              " ) as aa  group by type,count"
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return qry
    End Function
    Sub drilldownHeaderforgv1()
        Dim qry As String = Nothing
        Try
            Dim dt As DataTable = Nothing
            If btnGSTR2.IsChecked Then
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
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub drilldownHeaderforgv3()
        Try
            Dim qry As String = Nothing
            Dim dt As DataTable = Nothing
            Dim strDoc As String = Nothing
            If btnGSTR2.IsChecked AndAlso Not chkItemWise.Checked Then
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
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub fromatGridDrillDownHeader()
        Try
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
            gv3.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Function B2BInvoices34A() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim WrclsDirectAP As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing

        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and (qq.GSTRegistered=1)" &
                " and (isnull(qq.Against_POInvoice_No,'')<>''" &
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>'' ) and (isnull(qq.TaxableAmount,0) <>0 ) "

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If
        WrclsDirectAP = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and (qq.GSTRegistered=1 ) and (isnull(qq.TaxableAmount,0) <>0 )  and (qq.Document_Type in ('I') )" &
                 " and isnull(qq.Against_POInvoice_No ,'')='' and " &
                 "isnull(qq.Against_PurchaseReturn_No ,'')='' and " &
                " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" &
                 " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" &
                 " isnull(qq.Against_VCGL ,'')='' and" &
                 " isnull(qq.Against_Acquisition ,'')='' and " &
                " isnull(qq.Against_Asset_Work ,'')='' and " &
                 " isnull(qq.Against_VSPItemIssue_No ,'')='' " &
                 "  and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end)  "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If

        strBaseQuery = BaseQry()

        strUnionBaseQuery = UnionBaseQry()


        Qry = "select 1 as count, 'To be reconciled with the GST portal' as grp, 'B2B Invoices - 3, 4A' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,final.* from (" &
            " select * from ( " &
            "" & strBaseQuery & "" &
            " " & Wrcls & " " &
             " ) as xx" &
            " Union All" &
             " select * from ( " &
           "" & strBaseQuery & "" &
              "" & WrclsDirectAP & "" &
            " ) as xx" &
             " union all" &
               "" & strUnionBaseQuery & "" &
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
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and (qq.GSTRegistered=1 )" &
                " and (isnull(qq.Against_POInvoice_No,'')<>''" &
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>'' ) and (isnull(qq.TaxableAmount,0) <>0) "

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If
        WrclsDirectAP = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and (qq.GSTRegistered=1 ) and (isnull(qq.TaxableAmount,0) <>0 )  and (qq.Document_Type in ('I') )" &
                 " and isnull(qq.Against_POInvoice_No ,'')='' and " &
                 "isnull(qq.Against_PurchaseReturn_No ,'')='' and " &
                " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" &
                 " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" &
                 " isnull(qq.Against_VCGL ,'')='' and" &
                 " isnull(qq.Against_Acquisition ,'')='' and " &
                " isnull(qq.Against_Asset_Work ,'')='' and " &
                 " isnull(qq.Against_VSPItemIssue_No ,'')='' " &
                 "  and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end)  "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If

        strBaseQuery = BaseQry()
        strCreditDebitNotesRegular6C = CreditDebitNotesRegular6C()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 2 as count, 'To be reconciled with the GST portal' as grp, 'Taxable Purchases' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,final.* from (" &
         " select * from ( " &
         "" & strBaseQuery & "" &
          " " & Wrcls & " " &
          " ) as xx" &
         " Union All" &
          " select * from ( " &
        "" & strBaseQuery & "" &
          "" & WrclsDirectAP & "" &
         " ) as xx" &
          " union all" &
               "" & strUnionBaseQuery & "" &
         " ) as final"
        Return Qry
    End Function


    Private Function Reversechargesupplies()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()

        qry = "select 3 as count,'To be reconciled with the GST portal' as grp, 'Reverse charge supplies' as Type,0 as DocCount,* from ( " &
            "" & strUnionBaseQuery & "" &
        ") as xx"
        Return qry
    End Function
    Private Function CreditDebitNotesRegular6C()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and (qq.GSTRegistered=1 ) and  ( qq.Document_Type in ('C','D') ) and (isnull(qq.TaxableAmount,0) <>0 )" &
                " and isnull(qq.Against_POInvoice_No ,'')='' and" &
                " isnull(qq.Against_PurchaseReturn_No ,'')='' and " &
                " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" &
                 " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" &
                 " isnull(qq.Against_VCGL ,'')='' and" &
                 " isnull(qq.Against_Acquisition ,'')='' and " &
                 " isnull(qq.Against_Asset_Work ,'')='' and " &
                 " isnull(qq.Against_VSPItemIssue_No ,'')='' " &
                 "   and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end) "



        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If


        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 4 as count, 'To be reconciled with the GST portal' as grp, 'Credit/Debit Notes Regular - 6C' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" &
            "" & strBaseQuery & "" &
             " " & Wrcls & " " &
             " union all" &
               "" & strUnionBaseQuery & "" &
            ") as xx"

        Return Qry
    End Function


    Private Function B2BURInvoices4B() As String
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Dim WrclsDirectAP As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and (qq.GSTRegistered=0 )and (isnull(qq.TaxableAmount,0) <>0 ) " &
                " and (isnull(qq.Against_POInvoice_No,'')<>''" &
                " or isnull(qq.Against_PurchaseReturn_No  ,'')<>'') and (qq.PI_Type<>'I' )"



        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If


        WrclsDirectAP = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and (qq.GSTRegistered=0 ) and (isnull(qq.TaxableAmount,0) <>0 )  and (qq.Document_Type in ('I') )" &
                " and isnull(qq.Against_POInvoice_No ,'')='' and " &
                "isnull(qq.Against_PurchaseReturn_No ,'')='' and " &
               " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" &
                " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" &
                " isnull(qq.Against_VCGL ,'')='' and" &
                " isnull(qq.Against_Acquisition ,'')='' and " &
               " isnull(qq.Against_Asset_Work ,'')='' and " &
                " isnull(qq.Against_VSPItemIssue_No ,'')='' " &
                "   and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end) "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            WrclsDirectAP += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If

        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()


        Qry = "select 5 as count, 'To be uploaded on the GST portal' as grp, 'B2BUR Invoices - 4B' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,final.* from (" &
           " select * from ( " &
           "" & strBaseQuery & "" &
            " " & Wrcls & " " &
            " ) as xx" &
             " Union All" &
            " select * from ( " &
          "" & strBaseQuery & "" &
           "" & WrclsDirectAP & "" &
            " union all" &
               "" & strUnionBaseQuery & "" &
           " ) as xx" &
             " ) as final"

        Return Qry
    End Function

    Private Function ImportofServices4C()
        Dim qry As String = Nothing
        qry = "select 6 as count,'To be uploaded on the GST portal' as Grp, 'Import of Services - 4C' as Type,0 as DocCount,'' as Document_no,null as Invoice_Entry_date,'' as Vendor_code,'' as Loc_Code,'' as Vendor_Group_Code,'' as Trans_type,'' as AgainstDoNo,0 as TaxableValue,0 as TaxableAmount,0 as InvoiceAmount,0 as RoundOFFAMount " &
                "  ,'' as Against_POInvoice_No,'' as Against_PurchaseReturn_No,'' as Against_BulkMillkPurchaseInvoice_No,'' as Against_MillkPurchaseInvoice_No,'' as RefDocType,'' as RefDocNo,'' as Document_Type,0 as GSTRegistered ,'' as PI_Type,'' as Against_VCGL,'' as Against_Acquisition,'' as Against_Asset_Work,'' as Against_VSPItemIssue_No"
        Return qry
    End Function
    Private Function ImportofGoods5()

        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and ( isnull(qq.TaxableAmount,0) <>0 ) and (qq.PI_Type='I' )" &
                " and (isnull(qq.Against_POInvoice_No,'')<>'' )"



        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If


        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 7 as count, 'To be uploaded on the GST portal' as grp, 'Import of Goods - 5' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" &
            "" & strBaseQuery & "" &
             " " & Wrcls & " " &
              " union all" &
               "" & strUnionBaseQuery & "" &
            ") as xx"

        Return Qry

    End Function

    Private Function CreditDebitNotesUnregistered6C()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103) and (qq.GSTRegistered=0 ) and (isnull(qq.TaxableAmount,0) <>0 )  and (qq.Document_Type in ('C','D') )" &
                 " and isnull(qq.Against_POInvoice_No ,'')='' and " &
                 "isnull(qq.Against_PurchaseReturn_No ,'')='' and " &
                " isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')='' and" &
                 " isnull(qq.Against_MillkPurchaseInvoice_No ,'')='' and" &
                 " isnull(qq.Against_VCGL ,'')='' and" &
                 " isnull(qq.Against_Acquisition ,'')='' and " &
                " isnull(qq.Against_Asset_Work ,'')='' and " &
                 " isnull(qq.Against_VSPItemIssue_No ,'')='' " &
                 " and 2=(case when isnull(qq.RefDocNo,'' )='BM-PR' then 3 else 2 end)  "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If

        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()

        Qry = "select 8 as count, 'To be uploaded on the GST portal' as grp, 'Credit/Debit Notes Unregistered - 6C' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" &
            "" & strBaseQuery & "" &
             " " & Wrcls & " " &
               " union all" &
               "" & strUnionBaseQuery & "" &
            " ) as xx"


        Return Qry
    End Function


    Private Function NilRatedInvoices7Summary()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103)  and (isnull(qq.TaxableAmount,0) =0 ) "


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If
        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()
        Qry = "select 9 as count, 'To be uploaded on the GST portal' as grp, 'Nil Rated Invoices - 7 - (Summary)' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" &
          "" & strBaseQuery & "" &
           " " & Wrcls & " " &
               " union all" &
               "" & strUnionBaseQuery & "" &
          ") as xx"



        Return Qry
    End Function

    Private Function Composition()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 10 as count,'To be uploaded on the GST portal' as Grp, 'Composition' as Type,0 as DocCount,* from ( " &
            "" & strUnionBaseQuery & "" &
        ") as xx"
        Return qry
    End Function
    Private Function Exempt()
        Dim Qry As String = Nothing
        Dim Wrcls As String = Nothing
        Dim strBaseQuery As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        Wrcls = " WHERE 2=2 and convert(date,qq.Invoice_Entry_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,qq.Invoice_Entry_Date,103)<=convert(date,'" & txtToDate.Value & "',103)  and (isnull(qq.TaxableAmount,0) =0 ) " &
                 " and ( isnull(qq.Against_MillkPurchaseInvoice_No ,'')<>'' or isnull(qq.Against_BulkMillkPurchaseInvoice_No ,'')<>'' or (isnull(qq.RefDocType ,'')='BM-PR' and isnull(qq.RefDocNo ,'')<>'') )"


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Wrcls += " and qq.vendor_code in (" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")"
        End If
        If txtVendGroup.arrValueMember IsNot Nothing AndAlso txtVendGroup.arrValueMember.Count > 0 Then
            Wrcls += " and qq.Vendor_group_code in (" & clsCommon.GetMulcallString(txtVendGroup.arrValueMember) & ")"
        End If
        strBaseQuery = BaseQry()
        strUnionBaseQuery = UnionBaseQry()
        Qry = "select 11 as count, 'To be uploaded on the GST portal' as grp, 'Exempt' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount ,xx.* from (" &
          "" & strBaseQuery & "" &
            " " & Wrcls & " " &
             " union all" &
               "" & strUnionBaseQuery & "" &
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

        Qry = " select 12 as count, 'To be uploaded on the GST portal' as grp, 'Nil Rated' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount, " &
   " Document_no as Document_no,max(Trans_type) as Trans_type,max(AgainstDocNo ) as AgainstDocNo,max(Invoice_Entry_date) as Invoice_Entry_date ,max(Vendor_code) as Vendor_code," &
   " max(Loc_Code) as Loc_Code,max(Vendor_Group_Code) as Vendor_Group_Code,sum(TaxableValue) as TaxableValue,sum(TaxableAmount) as TaxableAmount,sum(TaxableRate ) as TaxableRate," &
   " max(InvoiceAmount) as InvoiceAmount,max(RoundOFFAMount) as RoundOFFAMount,max(Against_POInvoice_No) as Against_POInvoice_No," &
   " max(Against_PurchaseReturn_No) as Against_PurchaseReturn_No,max(Against_BulkMillkPurchaseInvoice_No) as Against_BulkMillkPurchaseInvoice_No," &
  "  max(Against_MillkPurchaseInvoice_No) as Against_MillkPurchaseInvoice_No,max(RefDocType) as RefDocType,max(RefDocNo) as RefDocNo,max(Document_Type) as Document_Type," &
   " max(GSTRegistered) as GSTRegistered,max(PI_Type) as PI_Type,max(Against_VCGL) as Against_VCGL,max(Against_Acquisition) as Against_Acquisition," &
   " max(Against_Asset_Work) as Against_Asset_Work,max(Against_VSPItemIssue_No) as Against_VSPItemIssue_No  from (" &
    " select *,1 as RI from (" &
        "" & strNilRatedInvoices7Summary & "" &
        ") as aa" &
        " union all" &
        " select *,-1 As RI from (" &
         "" & Exempt() & "" &
              ") as bb" &
              " ) as final group by Document_no having sum(RI)>0 or isnull(max(Document_no),'') =''"


        Return Qry

    End Function

    Private Function NonGST()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 13 as count, 'To be uploaded on the GST portal' as Grp,'Non GST' as Type,0 as DocCount,* from ( " &
            "" & strUnionBaseQuery & "" &
        ") as xx"
        Return qry
    End Function
    Private Function AdvancePaid10ASummary()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 14 as count,'To be uploaded on the GST portal' as Grp, 'Advance Paid -10A - (Summary)' as Type,0 as DocCount,* from ( " &
            "" & strUnionBaseQuery & "" &
        ") as xx"
        Return qry
    End Function
    Private Function AdjustmentofAdvance10BSummary()
        Dim qry As String = Nothing
        Dim strUnionBaseQuery As String = Nothing
        strUnionBaseQuery = UnionBaseQry()
        qry = "select 15 as count,'To be uploaded on the GST portal' as Grp, 'Adjustment of Advance - 10B - (Summary)' as Type,0 as DocCount,* from ( " &
            "" & strUnionBaseQuery & "" &
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

        qry = " select count,type as Name,max(grp) as grp, sum(TaxableValue) as TaxableValue,sum(TaxableAmount) as TaxableAmount,sum(InvoiceAmount) as InvoiceAmount, sum(TaxableValue1) as TaxableValue1,sum(TaxableAmount1) as TaxableAmount1,sum(InvoiceAmount1) as InvoiceAmount1 " &
        " from ( " &
        " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & StrB2BInvoices34A & ") as aa " & Environment.NewLine &
        " Union all " & Environment.NewLine &
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,0 as TaxableValue1,0 as TaxableAmount1,0 as InvoiceAmount1 from (" & StrtaxablePurchase & ") as aa " & Environment.NewLine &
             " Union all " & Environment.NewLine &
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & StrReversechargesupplies & ") as aa " & Environment.NewLine &
          " Union all " & Environment.NewLine &
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & strCreditDebitNotesRegular6C & ") as aa " & Environment.NewLine &
          " Union all " & Environment.NewLine &
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strB2BURInvoices4B & " ) as aa" & Environment.NewLine &
          " Union all " & Environment.NewLine &
         " select  count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strImportofServices4C & " ) as aa" & Environment.NewLine &
           " Union all " & Environment.NewLine &
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & strImportofGoods5 & ") as aa " & Environment.NewLine &
           " Union all " & Environment.NewLine &
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & strCreditDebitNotesUnregistered6C & ") as aa " & Environment.NewLine &
           " Union all " & Environment.NewLine &
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strNilRatedInvoices7Summary & ") as aa " & Environment.NewLine &
           " Union all " & Environment.NewLine &
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strComposition & ") as aa " & Environment.NewLine &
           " Union all " & Environment.NewLine &
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,0 as TaxableValue1,0 as TaxableAmount1,0 as InvoiceAmount1 from ( " & strExempt & ") as aa " & Environment.NewLine &
           " Union all " & Environment.NewLine &
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,0 as TaxableValue1,0 as TaxableAmount1,0 as InvoiceAmount1 from (" & strNilRated & ") as aa " & Environment.NewLine &
                " Union all " & Environment.NewLine &
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strNonGST & ") as aa " & Environment.NewLine &
             " Union all " & Environment.NewLine &
         " select count,type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from (" & strAdvancePaid10ASummary & ") as aa " & Environment.NewLine &
            " Union all " & Environment.NewLine &
         " select count, type,grp,TaxableValue,TaxableAmount,InvoiceAmount,TaxableValue as TaxableValue1,TaxableAmount as TaxableAmount1,InvoiceAmount as InvoiceAmount1 from ( " & strAdjustmentofAdvance10BSummary & ") as aa " & Environment.NewLine &
        " ) as Header  group by type,count order by count "

        Return qry
    End Function


    Sub drilldownDetailforgv2()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If btnGSTR2.IsChecked Then
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
            isGVDoubleClick = True
            If btnGSTR1.IsChecked = True Then
                DrillDownHeaderForGSTR1()
            Else
                drilldownHeaderforgv1()
            End If
            isGVDoubleClick = False
        Catch ex As Exception
            isGVDoubleClick = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv3_DoubleClick(sender As Object, e As EventArgs) Handles gv3.DoubleClick
        drilldownHeaderforgv3()
    End Sub

    Private Sub gv2_DoubleClick(sender As Object, e As EventArgs) Handles gv2.DoubleClick
        Try
            If btnGSTR1.IsChecked AndAlso chkItemWise.Checked Then
                DrillDownDetailForGSTR1_ItemWise()
            ElseIf btnGSTR1.IsChecked AndAlso Not chkItemWise.Checked Then
                DrillDownDetailForGSTR1()
            ElseIf btnGSTR2.IsChecked Then
                drilldownDetailforgv2()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''richa agarwal 29 Nov,2018 ALF/26/11/18-000089
    Function getBaseQueryForItemWise_GSTR1(ByVal strWhrCls As String, ByVal trantype As String) As String
        Dim BaseQry As String = String.Empty
        Try
            ''richa GKD/05/02/19-000174 6 Feb,2019
            ''BaseQry = " select FinalQuery .Item_Code,max(tspl_item_master.item_Desc) as Description,max(tspl_item_master.HSN_Code) as [HSN Code] ,FinalQuery.UOM ,SUM(FinalQuery.TotalQty) as TotalQty,sum(FinalQuery.TotalAmount) as TotalAmount,sum(FinalQuery.TotalTaxAmount) as TotalTaxAmount,sum(FinalQuery.CGSTAmount) as CGSTAmount,sum(FinalQuery.SGSTAmount) as SGSTAmount,sum(FinalQuery.IGSTAmount) as IGSTAmount,sum(FinalQuery.UGSTAmount) as UGSTAmount " & Environment.NewLine & _

            BaseQry = "  select FinalQuery .Item_Code,max(tspl_item_master.item_Desc) as Description,max(tspl_item_master.HSN_Code) as [HSN Code] ,Max(TSPL_UNIT_MAster.GST_UNIT_CODE+'-'+TSPL_EINVOICE_UOM.Description) As [UOM] ,SUM(FinalQuery.TotalQty) as TotalQty,(sum(FinalQuery.TotalAmount)-sum(FinalQuery.TotalTaxAmount)) as TotalTaxableValue,"
            If clsCommon.CompairString(trantype, "Credit/Debit Notes(Unregistered) - 9B") <> CompairStringResult.Equal And clsCommon.CompairString(trantype, "Credit/Debit Notes(Registered) - 9B") <> CompairStringResult.Equal Then
                BaseQry += " Max(Tax_Rate)Tax_Rate, "
            End If
            BaseQry += " sum(FinalQuery.TotalTaxAmount) as TotalTaxAmount,sum(FinalQuery.CGSTAmount) as CGSTAmount,sum(FinalQuery.SGSTAmount) as SGSTAmount,sum(FinalQuery.IGSTAmount) as IGSTAmount,sum(FinalQuery.UGSTAmount) as UGSTAmount ,sum(FinalQuery.TotalAmount) as TotalAmount " & Environment.NewLine &
               " from ( " & Environment.NewLine
            If clsCommon.CompairString(trantype, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Or clsCommon.CompairString(trantype, "Credit/Debit Notes(Unregistered) - 9B") = CompairStringResult.Equal Or clsCommon.CompairString(trantype, "Credit/Debit Notes(Registered) - 9B") = CompairStringResult.Equal Then
                BaseQry += "---------------- Direct AR Invoice-------------------" & Environment.NewLine &
                " select DirectARInvoice.Item_Code ,DirectARInvoice.UOM ,SUM(DirectARInvoice.Qty) as TotalQty,sum(DirectARInvoice.Item_Net_Amt) as TotalAmount,sum(DirectARInvoice.Total_Tax_Amt) as TotalTaxAmount,sum(DirectARInvoice.CGSTAmount) as CGSTAmount,sum(DirectARInvoice.SGSTAmount) as SGSTAmount,sum(DirectARInvoice.IGSTAmount) as IGSTAmount,sum(DirectARInvoice.UGSTAmount) as UGSTAmount,SUM(Tax_Rate) As Tax_Rate  from (" & Environment.NewLine &
                " select TSPL_Customer_Invoice_Detail.Document_No,'' AS Item_Code ,0 AS Qty ,'' as UOM,TSPL_Customer_Invoice_Head.Document_Type," & Environment.NewLine &
                " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Amount ,0))" & Environment.NewLine &
                " as Item_Net_Amt ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Tax  ,0))" & Environment.NewLine &
                " as Total_Tax_Amt,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as CGSTAmount," & Environment.NewLine &
                " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as SGSTAmount ," & Environment.NewLine &
                " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as IGSTAmount ," & Environment.NewLine &
                " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine &
                " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as UGSTAmount," & Environment.NewLine &
                "  case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX1_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX2_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX3_Rate else 0 end  " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX4_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX5_Rate else 0 end + " & Environment.NewLine &
" case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX1_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX2_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX3_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX4_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX5_Rate else 0 end + " & Environment.NewLine &
" case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX1_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX2_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX3_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX4_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX5_Rate else 0 end + " & Environment.NewLine &
" case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX1_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX2_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX3_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX4_Rate else 0 end " & Environment.NewLine &
" + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX5_Rate else 0 end as Tax_Rate " & Environment.NewLine &
                " from TSPL_Customer_Invoice_Detail" & Environment.NewLine &
                " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Document_No =TSPL_Customer_Invoice_Detail.Document_No " & Environment.NewLine &
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine &
                " where (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) and TSPL_Customer_INVOICE_HEAD.Status =1 " & strWhrCls & "" & Environment.NewLine &
                " ) DirectARInvoice " & Environment.NewLine &
                " group by  DirectARInvoice.Item_Code ,DirectARInvoice.UOM,DirectARInvoice.Tax_Rate " & Environment.NewLine
                If clsCommon.CompairString(trantype, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Then
                    BaseQry += "   union all  " & Environment.NewLine
                End If
            End If
            If clsCommon.CompairString(trantype, "Credit/Debit Notes(Unregistered) - 9B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(trantype, "Credit/Debit Notes(Registered) - 9B") <> CompairStringResult.Equal Then
                BaseQry += " ---------------- Sale Invoice ,BULK SALE,CAN SALE--------------------- " & Environment.NewLine &
                   " select saleInvoice.Item_Code ,saleInvoice.UOM ,SUM(saleInvoice.Qty) as TotalQty,sum(saleInvoice.Item_Net_Amt) as TotalAmount,--sum(saleInvoice.Total_Tax_Amt) as TotalTaxAmount," & Environment.NewLine &
                   "  sum(saleInvoice.CGSTAmount + saleInvoice.SGSTAmount + saleInvoice.IGSTAmount + saleInvoice.UGSTAmount) as TotalTaxAmount,sum(saleInvoice.CGSTAmount) as CGSTAmount,sum(saleInvoice.SGSTAmount) as SGSTAmount,sum(saleInvoice.IGSTAmount) as IGSTAmount,sum(saleInvoice.UGSTAmount) as UGSTAmount,Max(Tax_Rate)Tax_Rate  from (select TSPL_SD_SALE_INVOICE_DETAIL.Document_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt, case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end  " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt else 0 end as UGSTAmount, " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate else 0 end  
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate else 0 end as Tax_Rate " & Environment.NewLine &
                   " from TSPL_SD_SALE_INVOICE_DETAIL " & Environment.NewLine &
                   " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.Document_Code  " & Environment.NewLine &
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
                   " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER .Location_Code  " & Environment.NewLine &
                   " where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' " & strWhrCls & ") and TSPL_SD_SALE_INVOICE_HEAD.Status =1 " & Environment.NewLine &
                   " union all  " & Environment.NewLine &
                   " select TSPL_CANSALE_INVOICE_detail.Document_No,TSPL_CANSALE_INVOICE_detail.ItemCode ,TSPL_CANSALE_INVOICE_detail.Qty ,TSPL_CANSALE_INVOICE_detail.UOM ,TSPL_CANSALE_INVOICE_detail.TotalAmount ,0 as Total_Tax_Amt,0 AS CGSTAmount,0 AS SGSTAmount ,0 AS IGSTAmount,0 AS UGSTAmount,0 As Tax_Rate from TSPL_CANSALE_INVOICE_detail " & Environment.NewLine &
                   " left outer join TSPL_CANSALE_INVOICE_HEAD on TSPL_CANSALE_INVOICE_HEAD .Document_no =TSPL_CANSALE_INVOICE_detail.Document_No " & Environment.NewLine &
                   " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_CANSALE_INVOICE_HEAD.Document_No " & Environment.NewLine &
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CANSALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
                   " where TSPL_CANSALE_INVOICE_HEAD.Posted =1 and TSPL_CANSALE_INVOICE_HEAD.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' " & strWhrCls & ") " & Environment.NewLine &
                   " union all " & Environment.NewLine &
                   " select TSPL_INVOICE_DETAIL_BULKSALE.Document_No,TSPL_INVOICE_DETAIL_BULKSALE.item_code,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Qty,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code as UOM,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount ,0 as Total_Tax_Amt,0 AS CGSTAmount,0 AS SGSTAmount ,0 AS IGSTAmount,0 AS UGSTAmount,0 As Tax_Rate  from TSPL_INVOICE_DETAIL_BULKSALE " & Environment.NewLine &
                   " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE .Document_no =TSPL_INVOICE_DETAIL_BULKSALE.Document_No  " & Environment.NewLine &
                   " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No " & Environment.NewLine &
                   " where TSPL_INVOICE_MASTER_BULKSALE.Posted =1 and TSPL_INVOICE_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' " & strWhrCls & ") " & Environment.NewLine &
                   " ) saleInvoice  " & Environment.NewLine &
                   " group by  saleInvoice.Item_Code ,saleInvoice.UOM,saleInvoice.Tax_Rate " & Environment.NewLine &
                   "   union all  " & Environment.NewLine &
                   " ---------------- Sale RETURN ,CAN SALE RETURN----------------------- " & Environment.NewLine &
                   " select saleRETURN.Item_Code ,saleRETURN.UOM ,SUM(saleRETURN.Qty) * -1  as TotalQty,sum(saleRETURN.Item_Net_Amt) * -1  as TotalAmount,sum(saleRETURN.Total_Tax_Amt) * -1  as TotalTaxAmount ,sum(saleRETURN.CGSTAmount) * -1  as CGSTAmount,sum(saleRETURN.SGSTAmount)  * -1  as SGSTAmount,sum(saleRETURN.IGSTAmount) * -1  as IGSTAmount,sum(saleRETURN.UGSTAmount)  * -1  as UGSTAmount,Max(Tax_Rate)*-1 As Tax_Rate from ( " & Environment.NewLine &
                   " select  " & Environment.NewLine &
                   " TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,TSPL_SD_SALE_RETURN_DETAIL.Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine &
                   "case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as UGSTAmount, " & Environment.NewLine &
                " case when isnull(TSPL_SD_SALE_Return_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_Return_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_Return_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_Return_DETAIL.TAX3_Rate else 0 end  
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_Return_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_Return_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_Return_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_Return_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_Return_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_Return_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_Return_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_Return_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_Return_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_Return_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_Return_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_Return_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_Return_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_Return_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_Return_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_Return_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_Return_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_Return_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_Return_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_Return_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_Return_DETAIL.TAX5_Rate else 0 end as Tax_Rate " & Environment.NewLine &
                " from TSPL_SD_SALE_RETURN_DETAIL " & Environment.NewLine &
                   " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  " & Environment.NewLine &
                   " where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''  " & strWhrCls & ") and TSPL_SD_SALE_RETURN_HEAD.Status =1 " & Environment.NewLine &
                   " union all " & Environment.NewLine &
                   " select TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No,TSPL_SALE_RETURN_DETAIL_BULKSALE.item_code,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as Qty,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code as UOM,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount ,0 as Total_Tax_Amt,0 AS CGSTAmount,0 AS SGSTAmount ,0 AS IGSTAmount,0 AS UGSTAmount,0 As Tax_Rate  from TSPL_SALE_RETURN_DETAIL_BULKSALE " & Environment.NewLine &
                   " left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE .Document_no =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No  " & Environment.NewLine &
                   " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_Return_No =TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No " & Environment.NewLine &
                   " where TSPL_SALE_RETURN_MASTER_BULKSALE.Posted =1 AND TSPL_SALE_RETURN_MASTER_BULKSALE.AGAINST='Bulk Invoice' " & Environment.NewLine &
                   " and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''  " & strWhrCls & ") " & Environment.NewLine &
                   " ) saleRETURN " & Environment.NewLine &
                   " group by  saleRETURN.Item_Code ,saleRETURN.UOM ,saleReturn.Tax_Rate" & Environment.NewLine &
                   " union all " & Environment.NewLine &
                   "---------------- Scrap Sale Return---------------------- " & Environment.NewLine &
                   " select ScrapsaleReturN.Item_Code ,ScrapsaleReturN.UOM ,SUM(ScrapsaleReturN.Qty) * -1 as TotalQty,sum(ScrapsaleReturN.Item_Net_Amt) * -1 as TotalAmount,sum(ScrapsaleReturN.Total_Tax_Amt) * -1 as TotalTaxAmount,sum(ScrapsaleReturN.CGSTAmount) * -1 as CGSTAmount,sum(ScrapsaleReturN.SGSTAmount) * -1 as SGSTAmount,sum(ScrapsaleReturN.IGSTAmount) * -1 as IGSTAmount,sum(ScrapsaleReturN.UGSTAmount) * -1 as UGSTAmount,Max(Tax_Rate)*-1 As Tax_Rate  from (select TSPL_SCRAPSALE_DETAIL_RETURN.Document_No,TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code ,TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty AS Qty ,TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code as UOM,TSPL_SCRAPSALE_DETAIL_RETURN.TotalAmt as Item_Net_Amt ,TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt as Total_Tax_Amt, case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine &
                   " case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt else 0 end  " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt else 0 end as UGSTAmount, " & Environment.NewLine &
                " case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Rate else 0 end  
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='CGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='SGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='IGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX3,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,'')='UGST' then TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Rate else 0 end as Tax_Rate " & Environment.NewLine &
                " from TSPL_SCRAPSALE_DETAIL_RETURN " & Environment.NewLine &
                   " left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN .Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No  " & Environment.NewLine &
                   " where TSPL_SCRAPSALE_DETAIL_RETURN.Document_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>''  " & strWhrCls & ") and TSPL_SCRAPSALE_HEAD_RETURN.ispost =1  " & Environment.NewLine &
                   " ) ScrapsaleReturN  " & Environment.NewLine &
                   " group by  ScrapsaleReturN.Item_Code ,ScrapsaleReturN.UOM,ScrapsaleReturN.Tax_Rate " & Environment.NewLine &
                   " union all  " & Environment.NewLine &
                   " ---------------- Scrap Sale ----------------- " & Environment.NewLine &
                   " select Scrapsale.Item_Code ,Scrapsale.UOM ,SUM(Scrapsale.Qty) as TotalQty,sum(Scrapsale.Item_Net_Amt) as TotalAmount,sum(Scrapsale.Total_Tax_Amt) as TotalTaxAmount,sum(Scrapsale.CGSTAmount) as CGSTAmount,sum(Scrapsale.SGSTAmount) as SGSTAmount,sum(Scrapsale.IGSTAmount) as IGSTAmount,sum(Scrapsale.UGSTAmount) as UGSTAmount,Max(Tax_Rate) As Tax_Rate  from ( " & Environment.NewLine &
                   " select TSPL_SCRAPINVOICE_DETAIL.invoice_No AS Document_No " & Environment.NewLine &
                   " ,TSPL_SCRAPINVOICE_DETAIL.Item_Code ,TSPL_SCRAPINVOICE_DETAIL.shipped_Qty AS Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code as UOM,TSPL_SCRAPINVOICE_DETAIL.TotalAmt as Item_Net_Amt ,TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt as Total_Tax_Amt, case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine &
                   " case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt else 0 end as UGSTAmount, " & Environment.NewLine &
                   " case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate else 0 end  
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX3,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX4 ,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SCRAPINVOICE_DETAIL.TAX5 ,'')='UGST' then TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate else 0 end as Tax_Rate " & Environment.NewLine &
                   " from TSPL_SCRAPINVOICE_DETAIL " & Environment.NewLine &
                   " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No  " & Environment.NewLine &
                   " where TSPL_SCRAPINVOICE_DETAIL.invoice_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrap  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')<>''  " & strWhrCls & ") and TSPL_SCRAPINVOICE_HEAD.ispost =1  " & Environment.NewLine &
                   " ) Scrapsale" & Environment.NewLine &
                   " group by  Scrapsale.Item_Code ,Scrapsale.UOM,Scrapsale.Tax_Rate " & Environment.NewLine &
                   " union all " & Environment.NewLine &
                   " ---------------- mcc Material Sale RETURN-------------------------------- " & Environment.NewLine &
                   " select MCCMaterialsaleRETURN.Item_Code ,MCCMaterialsaleRETURN.UOM ,SUM(MCCMaterialsaleRETURN.Qty) * -1  as TotalQty,sum(MCCMaterialsaleRETURN.Item_Net_Amt) * -1  as TotalAmount,sum(MCCMaterialsaleRETURN.Total_Tax_Amt) * -1  as TotalTaxAmount ,sum(MCCMaterialsaleRETURN.CGSTAmount) * -1  as CGSTAmount,sum(MCCMaterialsaleRETURN.SGSTAmount)  * -1  as SGSTAmount,sum(MCCMaterialsaleRETURN.IGSTAmount) * -1  as IGSTAmount,sum(MCCMaterialsaleRETURN.UGSTAmount)  * -1  as UGSTAmount,Max(Tax_Rate)*-1 As Tax_Rate from ( " & Environment.NewLine &
                   " select  " & Environment.NewLine &
                   " TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,TSPL_SD_SALE_RETURN_DETAIL.Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as CGSTAmount, " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as SGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as IGSTAmount , " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt else 0 end as UGSTAmount, " & Environment.NewLine &
                   " case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate else 0 end  
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='CGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='SGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate else 0 end + 
 case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate else 0 end 
 + case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,'')='UGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate else 0 end as Tax_Rate " & Environment.NewLine &
                   " from TSPL_SD_SALE_RETURN_DETAIL " & Environment.NewLine &
                   " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  " & Environment.NewLine &
                   " where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return ,'')<>''  " & strWhrCls & ") and TSPL_SD_SALE_RETURN_HEAD.Status =1 AND TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' " & Environment.NewLine &
                   " ) MCCMaterialsaleRETURN " & Environment.NewLine &
                   " group by  MCCMaterialsaleRETURN.Item_Code ,MCCMaterialsaleRETURN.UOM,MCCMaterialsaleRETURN.Tax_Rate " & Environment.NewLine &
                   " union all " & Environment.NewLine &
                   " ---------------- VCGL---------------------------- " & Environment.NewLine &
                    " select VCGL.Item_Code ,VCGL.UOM ,SUM(VCGL.Qty) as TotalQty,sum(VCGL.Item_Net_Amt)   as TotalAmount,sum(VCGL.Total_Tax_Amt)  as TotalTaxAmount ,sum(VCGL.CGSTAmount)  as CGSTAmount,sum(VCGL.SGSTAmount)   as SGSTAmount,sum(VCGL.IGSTAmount)  as IGSTAmount,sum(VCGL.UGSTAmount)   as UGSTAmount,Max(Tax_Rate) As Tax_Rate from (  " & Environment.NewLine &
                    " select TSPL_VCGL_Detail.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Detail.Row_Type ='Customer' then TSPL_VCGL_Detail.Dr_Amount-TSPL_VCGL_Detail.Cr_Amount else  " & Environment.NewLine &
                    " 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, 0 as CGSTAmount,  " & Environment.NewLine &
                    " 0 as SGSTAmount ,0 as IGSTAmount ,0 as UGSTAmount,0 As Tax_Rate  " & Environment.NewLine &
                    " from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  " & Environment.NewLine &
                    " where TSPL_VCGL_Detail.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''   " & strWhrCls & ") and TSPL_VCGL_Head.Status =1  " & Environment.NewLine &
                    " union all " & Environment.NewLine &
                    " select   " & Environment.NewLine &
                    " TSPL_VCGL_Head.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Head.Document_Type  ='C' then TSPL_VCGL_Head.Tot_Cr_Amount-TSPL_VCGL_Head.Tot_Dr_Amount else " & Environment.NewLine &
                    " 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, 0 as CGSTAmount,  " & Environment.NewLine &
                    " 0 as SGSTAmount ,0 as IGSTAmount ,0 as UGSTAmount,0 As Tax_Rate  " & Environment.NewLine &
                    " from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  " & Environment.NewLine &
                    " where TSPL_VCGL_Head.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''   " & strWhrCls & ") and TSPL_VCGL_Head.Status =1  " & Environment.NewLine &
                    " ) VCGL  " & Environment.NewLine &
                    " group by  VCGL.Item_Code ,VCGL.UOM ,VCGL.Tax_Rate" & Environment.NewLine &
                   "  union all " & Environment.NewLine &
                   " ---------------- Security Receipt--------------------------" & Environment.NewLine &
                   " select SecurityReceipt.Item_Code ,SecurityReceipt.UOM ,SUM(SecurityReceipt.Qty) as TotalQty,sum(SecurityReceipt.Item_Net_Amt) as TotalAmount,sum(SecurityReceipt.Total_Tax_Amt) as TotalTaxAmount,sum(SecurityReceipt.CGSTAmount) as CGSTAmount,sum(SecurityReceipt.SGSTAmount) as SGSTAmount,sum(SecurityReceipt.IGSTAmount) as IGSTAmount,sum(SecurityReceipt.UGSTAmount) as UGSTAmount,Max(Tax_Rate) as Tax_Rate  from ( " & Environment.NewLine &
                   " select TSPL_Customer_Invoice_Head.Against_Security_Receipt_No as Document_No,'' AS Item_Code ,0 AS Qty ,'' as UOM,TSPL_Customer_Invoice_Head.Document_Type," & Environment.NewLine &
                   " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Amount ,0))" & Environment.NewLine &
                   " as Item_Net_Amt ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Tax  ,0))" & Environment.NewLine &
                   " as Total_Tax_Amt,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as CGSTAmount, " & Environment.NewLine &
                   " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as SGSTAmount ," & Environment.NewLine &
                   " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end " & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as IGSTAmount ," & Environment.NewLine &
                   " case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * (case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX1_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX2_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX3_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX4_Amt else 0 end" & Environment.NewLine &
                   " + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX5_Amt else 0 end) as UGSTAmount," & Environment.NewLine &
                " case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX1_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX2_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX3_Rate else 0 end  
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX4_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='CGST' then TSPL_Customer_Invoice_Detail.TAX5_Rate else 0 end + 
 case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX1_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX2_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX3_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX4_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='SGST' then TSPL_Customer_Invoice_Detail.TAX5_Rate else 0 end + 
 case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX1_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX2_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX3_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX4_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='IGST' then TSPL_Customer_Invoice_Detail.TAX5_Rate else 0 end + 
 case when isnull(TSPL_Customer_Invoice_Detail.tax1,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX1_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.tax2,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX2_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX3,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX3_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX4 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX4_Rate else 0 end 
 + case when isnull(TSPL_Customer_Invoice_Detail.TAX5 ,'')='UGST' then TSPL_Customer_Invoice_Detail.TAX5_Rate else 0 end as Tax_Rate " & Environment.NewLine &
                " from TSPL_Customer_Invoice_Detail" & Environment.NewLine &
                   " left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Document_No =TSPL_Customer_Invoice_Detail.Document_No " & Environment.NewLine &
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine &
                   " where isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')<>''  and TSPL_Customer_INVOICE_HEAD.Status =1  " & strWhrCls & " " & Environment.NewLine &
                   " ) SecurityReceipt " & Environment.NewLine &
                   " group by  SecurityReceipt.Item_Code ,SecurityReceipt.UOM,SecurityReceipt.Tax_Rate" & Environment.NewLine
            End If

            BaseQry += " ) FinalQuery left outer join tspl_item_master on TSPL_ITEM_MASTER.Item_Code = FinalQuery.Item_Code Left Outer Join TSPL_UNIT_MAster On TSPL_UNIT_MAster.unit_code=finalQuery.UOM  Left Outer Join TSPL_EINVOICE_UOM On TSPL_EINVOICE_UOM.Code=TSPL_UNIT_MAster.GST_UNIT_CODE
 Where IsNull(FinalQuery.Item_Code,'')<>''  group by  FinalQuery.Item_Code ,FinalQuery.UOM,FinalQuery.Tax_Rate" & Environment.NewLine
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return BaseQry
    End Function
    Function getBaseQueryforDetail_GSTR1(ByVal strWhrCls As String, ByVal strType As String, Optional ByVal isExportGSTR As Boolean = False) As String
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
            " --(case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then 0 else TSPL_SD_SALE_RETURN_HEAD.Tax1_Rate end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>'' then 0 when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return,'')<>'' then TSPL_MCC_Material_sale_Return.Tax1_Rate when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No,'')<>'' then TSPL_Customer_INVOICE_HEAD.Tax1_Rate when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')='' ) then (TSPL_Customer_INVOICE_HEAD.TAX1_Rate+TSPL_Customer_INVOICE_HEAD.TAX2_Rate+TSPL_Customer_INVOICE_HEAD.TAX3_Rate+TSPL_Customer_INVOICE_HEAD.TAX4_Rate+TSPL_Customer_INVOICE_HEAD.TAX5_Rate+TSPL_Customer_INVOICE_HEAD.TAX6_Rate+TSPL_Customer_INVOICE_HEAD.TAX7_Rate+TSPL_Customer_INVOICE_HEAD.TAX8_Rate+TSPL_Customer_INVOICE_HEAD.TAX9_Rate+TSPL_Customer_INVOICE_HEAD.TAX10_Rate) end) as TaxRate, " & Environment.NewLine &
            " TSPL_Customer_Invoice_Detail.Tax_Rate as TaxRate," & Environment.NewLine &
"(Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX1_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX2_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX3_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX4_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX5_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX6_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX7_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX8_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX9_Amt,0) Else 0 End +
Case When TSPL_Customer_Invoice_Detail.Is_TCS = 'Y' Then Isnull(TSPL_Customer_INVOICE_HEAD.TAX10_Amt,0) Else 0  End) as TCSAmt," & Environment.NewLine &
            " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date else TSPL_SD_SALE_RETURN_HEAD.Document_Date end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.shipment_Date when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VCGL_HEAD.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then TSPL_MCC_Material_sale_Return.Document_Date when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then TSPL_RECEIPT_HEADER. Receipt_Date when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then null  end) as [Sale Invoice Date], " & Environment.NewLine &
            " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then case when isnull(TSPL_SD_SALE_RETURN_HEAD.Document_Code,'')='' then TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code else case when isnull(TSPL_SD_SALE_RETURN_HEAD.Ship_To_Location ,'')='' then NULL else TSPL_SD_SALE_RETURN_HEAD.Ship_To_Location end end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then case when isnull(TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,'')='' then NULL else TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location end when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then TSPL_SCRAPINVOICE_HEAD.loc_code when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then TSPL_SCRAPSALE_HEAD_RETURN.loc_code when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VCGL_HEAD.Location_Segment when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then case when isnull(TSPL_MCC_Material_sale_Return.Ship_To_Location ,'')='' then NULL else TSPL_MCC_Material_sale_Return.Ship_To_Location end when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then TSPL_RECEIPT_HEADER. Location_GL_Code when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Location Code], "


            BaseQry = " select SNo,Particulars,"
            If isExportGSTR Then
                BaseQry += "Case When TSPL_COMPANY_MASTER.State=tspl_state_master.STATE_CODE Then 
 Case When tspl_customer_master.GST_Registered=1 And ISNULL(tspl_customer_master.GSTNO,'')<>'' Then 'Intra-State supplies to registered persons' 
 Else 'Intra-State supplies to unregistered persons' End
 else
 Case When tspl_customer_master.GST_Registered=1 And ISNULL(tspl_customer_master.GSTNO,'')<>'' Then 'Inter-State supplies to registered persons' 
 Else 'Inter-State supplies to unregistered persons' End
 End As Description,"
            End If
            BaseQry += " [Document No],[Document Date], [Trans Type],[Sale Invoice No],[Sale Invoice Date],CASE WHEN ISNULL(TSPL_SHIP_TO_LOCATION.Ship_To_Code,'')<>'' THEN TSPL_SHIP_TO_LOCATION_sTATE.GST_STATE_Code +'- ' +TSPL_SHIP_TO_LOCATION_sTATE.STATE_NAME ELSE tspl_state_master.GST_STATE_Code +'-' +TSPL_state_master.state_name END AS [Place of Supply], z.[Customer Code] ,z.[Customer Name] ,z.[GST No],z.[Document Type] ,z.[Taxable Value] ,z.TaxRate,z.[Taxable Amount]-z.TCSAmt As [Taxable Amount] ,z.[Round Off Amount] ,z.[Invoice Amount] "
            If Not (clsCommon.CompairString(strType, "B2C(Large) Invoices - 5A, 5B") = CompairStringResult.Equal OrElse clsCommon.CompairString(strType, "B2C(Small) Invoices - 7") = CompairStringResult.Equal) Then
                BaseQry += " ,'Regular B2B' As [Invoice Type] "
            End If
            BaseQry += " ,z.[Reverse Charges]  from ( " & Environment.NewLine &
            " select ROW_NUMBER() OVER(ORDER BY TSPL_Customer_Invoice_Head.Document_No ASC) as  SNo,'" & strType & "' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date]," & strtranstypeandsaleinvoiceno & " " & Environment.NewLine &
            " TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.GSTNO as [GST No],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],
              TSPL_Customer_Invoice_Detail.Total_Amount-TSPL_Customer_Invoice_Detail.total_Tax as [Taxable Value]," & Environment.NewLine &
            " TSPL_Customer_Invoice_Detail.Tax_Amt AS [Taxable Amount],
              case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount], 
              (TSPL_Customer_Invoice_Head.Document_Total-TSPL_Customer_Invoice_Head.Total_Add_Charge)  as [Invoice Amount],'N' As [Reverse Charges] " & Environment.NewLine &
            " from (Select Document_No,Sum(Tax_Rate)Tax_Rate,Sum(Tax_Amt)Tax_Amt,MAX(Amount)Amount,MAX(Total_Tax)Total_Tax,Max(Total_Amount)Total_Amount,Max(Tax_Code)Tax_Code,Max(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,Max(Is_Tax_Exempted)Is_Tax_Exempted,Max(Is_TCS)Is_TCS from (Select Document_No,Tax,Tax_Rate,Sum(Tax_Amt)Tax_Amt,Sum(Amount)Amount,SUM(Total_Tax)Total_Tax,SUM(Total_Amount)Total_Amount,Max(Tax_Master.Tax_Code)Tax_Code,Max(Tax_Master.Tax_Group_Type)Tax_Group_Type,MAX(Tax_Master.Type)Type,Max(Tax_Master.Is_Tax_Exempted)Is_Tax_Exempted,Max(Tax_Master.Is_TCS)Is_TCS from ( "
#Disable Warning
            For i As Integer = 1 To 10
                If i <> 1 Then
                    BaseQry += Environment.NewLine & " Union All " & Environment.NewLine
                End If
                BaseQry += " Select TSPL_Customer_Invoice_Head.Document_No,TSPL_Customer_Invoice_Head.Tax_Group, TSPL_Customer_Invoice_Head.Tax" & clsCommon.myCstr(i) & " As Tax,IsNull(TSPL_CUSTOMER_INVOICE_DETAIL.Tax" & clsCommon.myCstr(i) & "_Rate,0) As Tax_Rate, TSPL_CUSTOMER_INVOICE_DETAIL.Tax" & clsCommon.myCstr(i) & "_Amt As Tax_Amt,
TSPL_CUSTOMER_INVOICE_DETAIL.TAX" & clsCommon.myCstr(i) & "_Base_Amt As Amount,TSPL_CUSTOMER_INVOICE_DETAIL.Total_Tax,TSPL_CUSTOMER_INVOICE_DETAIL.Total_Amount from TSPL_CUSTOMER_INVOICE_DETAIL
Inner Join TSPL_Customer_Invoice_Head On TSPL_Customer_Invoice_Head.Document_No=TSPL_CUSTOMER_INVOICE_DETAIL.Document_No
where convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & txtToDate.Value & "',103) "
            Next
#Enable Warning
            BaseQry += " )xyzTaxRate "
            BaseQry += " Left Outer Join ( Select Tax_Group_Code,Tax_Code,MAX(Tax_Code_Desc)Tax_Code_Desc,MAX(Tax_Group_Type)Tax_Group_Type,MAX(Type)Type,MAX(Is_Tax_Exempted)Is_Tax_Exempted,MAX(Is_TCS)Is_TCS from (Select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Type,TSPL_TAX_MASTER.Type,TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,TSPL_TAX_MASTER.Is_TCS from TSPL_TAX_GROUP_MASTER
Left Outer Join TSPL_TAX_GROUP_DETAILS On TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code
Left Outer Join TSPL_TAX_MASTER On TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code
) xyz Group By Tax_Group_Code,Tax_Code) As Tax_Master On Tax_Master.Tax_Group_Code=xyzTaxRate.Tax_Group And Tax_Master.Tax_Code=xyzTaxRate.Tax  "
            BaseQry += " Where 2=2"
            If clsCommon.CompairString(strType, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Then
                BaseQry += " and Tax_Master.Is_Tax_Exempted=1 And Tax_Master.Tax_Group_Type='S' and Tax_Master.Type='O' "
            Else
                BaseQry += " And Tax_Rate>0 And Tax_Amt>0 "
            End If
            BaseQry += " Group By Document_No,Tax_Group,Tax, Tax_Rate)xyzTax Group By Document_No) As TSPL_Customer_Invoice_Detail
 " & Environment.NewLine &
            " left outer join TSPL_Customer_Invoice_Head On TSPL_Customer_Invoice_Head.Document_No= TSPL_Customer_Invoice_Detail.Document_No " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code  " & Environment.NewLine &
            " left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN.Document_No =TSPL_Customer_Invoice_Head.AgainstScrapReturn " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_Return_No " & Environment.NewLine &
            " left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_Return_No AND TSPL_SALE_RETURN_MASTER_BULKSALE.AGAINST='Bulk Invoice' " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_RETURN_HEAD TSPL_MCC_Material_sale_Return on TSPL_MCC_Material_sale_Return.Document_Code =TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return AND TSPL_MCC_Material_sale_Return.Trans_Type='MCC' " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine &
            " left outer join TSPL_CANSALE_INVOICE_HEAD on TSPL_CANSALE_INVOICE_HEAD.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine &
            " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_No " & Environment.NewLine &
            " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_Customer_Invoice_Head.AgainstScrap " & Environment.NewLine &
            " left outer join TSPL_VCGL_HEAD  on TSPL_VCGL_HEAD.Document_No  =TSPL_Customer_Invoice_Head.Against_VCGL  " & Environment.NewLine &
            " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No  =TSPL_Customer_Invoice_Head.Against_Security_Receipt_No  and TSPL_RECEIPT_HEADER.Receipt_Type = 'P' " & Environment.NewLine &
            " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' " & Environment.NewLine &
            " where 1=1  " & strWhrCls & " " & Environment.NewLine &
            " ) z " & Environment.NewLine &
            " left outer join tspl_customer_master on tspl_customer_master.Cust_Code  =z.[Customer Code] " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =z.[Location Code] " & Environment.NewLine &
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =z.[Location Code] " & Environment.NewLine &
            " left outer join tspl_state_master on tspl_state_master.STATE_CODE =tspl_customer_master.State" & Environment.NewLine &
            " left outer join tspl_state_master TSPL_SHIP_TO_LOCATION_sTATE on TSPL_SHIP_TO_LOCATION_sTATE.STATE_CODE =TSPL_SHIP_TO_LOCATION.State" & Environment.NewLine &
            " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' " & Environment.NewLine
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return BaseQry
    End Function
    Sub DrillDownDetailForGSTR1_ItemWise(Optional sheetName As String = Nothing)
        Try
            If clsCommon.myLen(gv2.CurrentRow.Cells("Name").Value) > 0 Then
                Dim qry As String = String.Empty
                qry = "select 1 where 1=2 "
                Dim dt As DataTable = Nothing
                Dim Wrcls As String = "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & txtToDate.Value & "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" & clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) & ")"
                End If
                'Dim strtranstypeandsaleinvoiceno As String = " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then 'Sale Return' when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then 'Sale Invoice'  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then 'Scrap' when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then 'Scrap Return'   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then 'MCC Material Sale Return'  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then 'Security Receipt'  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then 'Direct AR Invoice'  end) as [Trans Type],  (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then Against_Sale_Return_No when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then Against_Sale_No  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then AgainstScrap when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then AgainstScrapReturn   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then Against_VCGL when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then Against_Security_Receipt_No  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Sale Invoice No],"
                If IIf(exportGSTR, (clsCommon.CompairString(sheetName, "hsn(b2b)") = CompairStringResult.Equal OrElse clsCommon.CompairString(sheetName, "hsn(b2c)") = CompairStringResult.Equal), clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2B Invoices - 4A, 4B, 4C, 6B, 6C") = CompairStringResult.Equal) Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    If Not exportGSTR Then
                        Wrcls += " and IsNull(TSPL_CUSTOMER_MASTER.GST_Registered,0) =1 And isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' "
                        Wrcls += " and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 "
                    End If
                    If exportGSTR AndAlso clsCommon.CompairString(sheetName, "hsn(b2b)") = CompairStringResult.Equal Then
                        Wrcls += " and IsNull(TSPL_CUSTOMER_MASTER.GST_Registered,0) =1 And isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' "
                    End If
                    If exportGSTR AndAlso clsCommon.CompairString(sheetName, "hsn(b2c)") = CompairStringResult.Equal Then
                        Wrcls += " and IsNull(TSPL_CUSTOMER_MASTER.GST_Registered,0) =0 And isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' "
                    End If
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "B2B Invoices - 4A, 4B, 4C, 6B, 6C")
                ElseIf Not exportGSTR AndAlso clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal Then
                    Wrcls += "  And TSPL_Customer_Invoice_Head.Against_Sale_No Not IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Taxable Sales")
                ElseIf Not exportGSTR AndAlso clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Large) Invoices - 5A, 5B") = CompairStringResult.Equal Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 and TSPL_Customer_Invoice_Head.Document_Total>'" & clsCommon.myCstr(B2CDocumentAmountRangeSameState) & "'"
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "B2C(Large) Invoices - 5A, 5B")
                ElseIf Not exportGSTR AndAlso clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Small) Invoices - 7") = CompairStringResult.Equal Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 and TSPL_Customer_Invoice_Head.Document_Total<= '" & clsCommon.myCstr(B2CDocumentAmountRangeSameState) & "' "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "B2C(Small) Invoices - 7")
                ElseIf Not exportGSTR AndAlso clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Registered) - 9B") = CompairStringResult.Equal Then
                    Wrcls += "  and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Credit/Debit Notes(Registered) - 9B")
                ElseIf Not exportGSTR AndAlso clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Unregistered) - 9B") = CompairStringResult.Equal Then
                    Wrcls += "  and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Credit/Debit Notes(Unregistered) - 9B")
                ElseIf Not exportGSTR AndAlso clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Exports Invoices - 6A") = CompairStringResult.Equal Then
                    Wrcls += "  AND TSPL_Customer_Invoice_Head.Against_Sale_No  IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Exports Invoices - 6A")
                ElseIf Not exportGSTR AndAlso clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal Then
                    Wrcls += "  AND isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) =0  AND TSPL_Customer_Invoice_Head.Against_Sale_No not IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    qry = getBaseQueryForItemWise_GSTR1(Wrcls, "Nil Rated Invoices - 8A, 8B, 8C, 8D")
                End If
                Dim strQry As String = ";With CTE As (" & qry & ")"
                If Not exportGSTR Then
                    dt = clsDBFuncationality.GetDataTable(strQry & "Select * from CTE")
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
                Else
                    strQryGSTR = strQry
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''richa 30 Nov,2018 ALF/26/11/18-000089,GKD/29/01/19-000173
    Sub DrillDownDetailForGSTR1(Optional ByVal sheetName As String = Nothing)
        Try
            If clsCommon.myLen(gv2.CurrentRow.Cells("Name").Value) > 0 Then
                Dim qry As String = String.Empty
                qry = "select 1 where 1=2 "
                Dim dt As DataTable = Nothing
                Dim Wrcls As String = "  and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & txtFromDate.Value & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & txtToDate.Value & "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" & clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) & ")"
                End If

                If IIf(exportGSTR, clsCommon.CompairString(sheetName, "b2b,sez,de") = CompairStringResult.Equal, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2B Invoices - 4A, 4B, 4C, 6B, 6C") = CompairStringResult.Equal) Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 " & Environment.NewLine &
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine &
                    " and (isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' )  and TSPL_Customer_Invoice_Detail.Is_Tax_Exempted<>1 and TSPL_Customer_Invoice_Detail.Tax_Group_Type='S' and TSPL_Customer_Invoice_Detail.Type<>'O' "
                    '" and ((isnull(TSPL_Customer_Invoice_Head.TAX1,'')='IGST'OR isnull(TSPL_Customer_Invoice_Head.tax2,'')='IGST' 
                    '             Or isnull(TSPL_Customer_Invoice_Head.tax3,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax4,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax5,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.TAX6,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax7,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax8,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax9,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.TAX10,'')='IGST' )  OR ((ISNULL(TSPL_Customer_Invoice_Head.tax1,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax2,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax3,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax4,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax5,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax6,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax7,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax8,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax9,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax10,'')='CGST' )  OR  ( ISNULL(TSPL_Customer_Invoice_Head.tax1,'')='SGST'
                    '              Or ISNULL(TSPL_Customer_Invoice_Head.tax2,'')='SGST'
                    '             Or  ISNULL(TSPL_Customer_Invoice_Head.tax3,'')='SGST'
                    '             Or ISNULL(TSPL_Customer_Invoice_Head.tax4,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax5,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax6,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax7,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax8,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax9,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax10,'')='SGST'))) "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "B2B Invoices - 4A, 4B, 4C, 6B, 6C", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))))

                ElseIf IIf(exportGSTR, sheetName = Nothing, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal) Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 " & Environment.NewLine &
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine &
                    " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) and TSPL_Customer_Invoice_Detail.Is_Tax_Exempted<>1 and TSPL_Customer_Invoice_Detail.Tax_Group_Type='S' and TSPL_Customer_Invoice_Detail.Type<>'O' "
                    '" and ((isnull(TSPL_Customer_Invoice_Head.TAX1,'')='IGST'OR isnull(TSPL_Customer_Invoice_Head.tax2,'')='IGST' 
                    '             Or isnull(TSPL_Customer_Invoice_Head.tax3,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax4,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax5,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.TAX6,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax7,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax8,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.tax9,'')='IGST' OR isnull(TSPL_Customer_Invoice_Head.TAX10,'')='IGST' )  OR ((ISNULL(TSPL_Customer_Invoice_Head.tax1,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax2,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax3,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax4,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax5,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax6,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax7,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax8,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax9,'')='CGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax10,'')='CGST' )  OR  ( ISNULL(TSPL_Customer_Invoice_Head.tax1,'')='SGST'
                    '              Or ISNULL(TSPL_Customer_Invoice_Head.tax2,'')='SGST'
                    '             Or  ISNULL(TSPL_Customer_Invoice_Head.tax3,'')='SGST'
                    '             Or ISNULL(TSPL_Customer_Invoice_Head.tax4,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax5,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax6,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax7,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax8,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax9,'')='SGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax10,'')='SGST'))) "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "Taxable Sales", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))))

                ElseIf IIf(exportGSTR, clsCommon.CompairString(sheetName, "b2cl") = CompairStringResult.Equal, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Large) Invoices - 5A, 5B") = CompairStringResult.Equal) Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =0 AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' 
  or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' 
  or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' 
  or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' 
  or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  
  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' )  " & Environment.NewLine &
                    "  And IsNull(TSPL_COMPANY_MASTER.state,'')<>'' and IsNull(tspl_customer_master.state,'')<>'' And TSPL_Customer_Invoice_Head.Document_Total>(Case  When TSPL_COMPANY_MASTER.state = tspl_customer_master.state Then " + clsCommon.myCstr(B2CDocumentAmountRangeSameState) + " Else " + clsCommon.myCstr(B2CDocumentAmountRangeOtherState) + " End) and TSPL_Customer_Invoice_Detail.Is_Tax_Exempted<>1 and TSPL_Customer_Invoice_Detail.Tax_Group_Type='S' and TSPL_Customer_Invoice_Detail.Type<>'O' "
                    '                   "And (((ISNULL(TSPL_Customer_Invoice_Head.tax1,'')='IGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax2,'')='IGST' 
                    '   Or  ISNULL(TSPL_Customer_Invoice_Head.tax3,'')='IGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax4,'')='IGST' 
                    'Or  ISNULL(TSPL_Customer_Invoice_Head.tax5,'')='IGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax6,'')='IGST' 
                    'Or  ISNULL(TSPL_Customer_Invoice_Head.tax7,'')='IGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax8,'')='IGST' 
                    'Or  ISNULL(TSPL_Customer_Invoice_Head.tax9,'')='IGST' OR ISNULL(TSPL_Customer_Invoice_Head.TAX10,'')='IGST')) 
                    'Or ((isnull(TSPL_Customer_Invoice_Head.tax1,'')='CGST' OR isnull(TSPL_Customer_Invoice_Head.tax2,'')='CGST' 
                    '  Or isnull(TSPL_Customer_Invoice_Head.tax3,'')='CGST' OR isnull(TSPL_Customer_Invoice_Head.tax4,'')='CGST'  
                    '  Or isnull(TSPL_Customer_Invoice_Head.tax5,'')='CGST' OR isnull(TSPL_Customer_Invoice_Head.tax6,'')='CGST'
                    '  Or isnull(TSPL_Customer_Invoice_Head.tax7,'')='CGST' OR isnull(TSPL_Customer_Invoice_Head.tax8,'')='CGST' 
                    '  Or isnull(TSPL_Customer_Invoice_Head.tax9,'')='CGST' OR isnull(TSPL_Customer_Invoice_Head.tax10,'')='CGST')  
                    'And (ISNULL(TSPL_Customer_Invoice_Head.tax1,'')='SGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax2,'')='SGST'
                    ' OR  ISNULL(TSPL_Customer_Invoice_Head.tax3,'')='SGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax4,'')='SGST'
                    ' OR ISNULL(TSPL_Customer_Invoice_Head.tax5,'')='SGST'  OR ISNULL(TSPL_Customer_Invoice_Head.tax6,'')='SGST'
                    ' OR ISNULL(TSPL_Customer_Invoice_Head.tax7,'')='SGST'  OR ISNULL(TSPL_Customer_Invoice_Head.tax8,'')='SGST'
                    ' OR ISNULL(TSPL_Customer_Invoice_Head.tax9,'')='SGST'  OR ISNULL(TSPL_Customer_Invoice_Head.tax10,'')='SGST')
                    'and TSPL_Customer_Invoice_Head.Document_Total>0)   )"

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "B2C(Large) Invoices - 5A, 5B", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))))

                ElseIf IIf(exportGSTR, clsCommon.CompairString(sheetName, "b2cs") = CompairStringResult.Equal, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "B2C(Small) Invoices - 7") = CompairStringResult.Equal) Then
                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0" & Environment.NewLine &
                   " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine &
                   " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')<>'' or isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')<>''  or ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')<>'' ) " &
                   " And IsNull(TSPL_COMPANY_MASTER.state,'')<>'' and IsNull(tspl_customer_master.state,'')<>'' And TSPL_Customer_Invoice_Head.Document_Total<=(Case  When TSPL_COMPANY_MASTER.state = tspl_customer_master.state Then " + clsCommon.myCstr(B2CDocumentAmountRangeSameState) + " Else " + clsCommon.myCstr(B2CDocumentAmountRangeOtherState) + " End) and TSPL_Customer_Invoice_Detail.Is_Tax_Exempted<>1 and TSPL_Customer_Invoice_Detail.Tax_Group_Type='S' and TSPL_Customer_Invoice_Detail.Type<>'O' "
                    '       "And (((ISNULL(TSPL_Customer_Invoice_Head.tax1,'')='IGST' OR ISNULL(TSPL_Customer_Invoice_Head.tax2,'')='IGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax3,'')='IGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax4,'')='IGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax5,'')='IGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax6,'')='IGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax7,'')='IGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax8,'')='IGST' OR  ISNULL(TSPL_Customer_Invoice_Head.tax9,'')='IGST' OR  ISNULL(TSPL_Customer_Invoice_Head.TAX10,'')='IGST')) or((isnull(TSPL_Customer_Invoice_Head.tax1,'')='CGST' or isnull(TSPL_Customer_Invoice_Head.tax2,'')='CGST' OR isnull(TSPL_Customer_Invoice_Head.tax3,'')='CGST' OR isnull(TSPL_Customer_Invoice_Head.tax4,'')='CGST'  OR isnull(TSPL_Customer_Invoice_Head.tax5,'')='CGST'OR  isnull(TSPL_Customer_Invoice_Head.tax6,'')='CGST'OR  isnull(TSPL_Customer_Invoice_Head.tax7,'')='CGST' OR isnull(TSPL_Customer_Invoice_Head.tax8,'')='CGST' OR  isnull(TSPL_Customer_Invoice_Head.tax9,'')='CGST'OR  isnull(TSPL_Customer_Invoice_Head.tax10,'')='CGST'  ) AND (ISNULL(TSPL_Customer_Invoice_Head.tax1,'')='SGST'
                    ' Or ISNULL(TSPL_Customer_Invoice_Head.tax2,'')='SGST'
                    'Or  ISNULL(TSPL_Customer_Invoice_Head.tax3,'')='SGST'
                    'Or ISNULL(TSPL_Customer_Invoice_Head.tax4,'')='SGST'
                    ' Or ISNULL(TSPL_Customer_Invoice_Head.tax5,'')='SGST'
                    '  Or ISNULL(TSPL_Customer_Invoice_Head.tax6,'')='SGST'
                    '   Or ISNULL(TSPL_Customer_Invoice_Head.tax7,'')='SGST'
                    ' Or ISNULL(TSPL_Customer_Invoice_Head.tax8,'')='SGST'
                    '  Or ISNULL(TSPL_Customer_Invoice_Head.tax9,'')='SGST'
                    '   Or ISNULL(TSPL_Customer_Invoice_Head.tax10,'')='SGST')
                    ' And TSPL_Customer_Invoice_Head.Document_Total>0)   ) "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "B2C(Small) Invoices - 7", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))))

                ElseIf IIf(exportGSTR, clsCommon.CompairString(sheetName, "cdnr") = CompairStringResult.Equal, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Registered) - 9B") = CompairStringResult.Equal) Then

                    Wrcls += " And TSPL_CUSTOMER_MASTER.GST_Registered =1 And isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') " & Environment.NewLine &
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine &
                     " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' OR  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' OR isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' OR isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='')  "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "Credit/Debit Notes(Registered) - 9B", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))))
                ElseIf IIf(exportGSTR, clsCommon.CompairString(sheetName, "cdnur") = CompairStringResult.Equal, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Credit/Debit Notes(Unregistered) - 9B") = CompairStringResult.Equal) Then

                    Wrcls += " and TSPL_CUSTOMER_MASTER.GST_Registered =0 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')='' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  AND TSPL_Customer_Invoice_Head.Document_Type IN ('C','D') " & Environment.NewLine &
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine &
                     " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' OR  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' OR isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' OR isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' OR ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='') "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "Credit/Debit Notes(Unregistered) - 9B", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))))

                ElseIf IIf(exportGSTR, sheetName = Nothing, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Exports Invoices - 6A") = CompairStringResult.Equal) Then
                    Wrcls += " AND TSPL_Customer_Invoice_Head.Against_Sale_No  IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "Exports Invoices - 6A", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))))

                ElseIf IIf(exportGSTR, clsCommon.CompairString(sheetName, "exemp") = CompairStringResult.Equal, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Nil Rated Invoices - 8A, 8B, 8C, 8D") = CompairStringResult.Equal) Then
                    Wrcls += " And isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''  AND TSPL_Customer_Invoice_Head.Against_Sale_No not IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) "
                    '       Wrcls += " And (ISNULL(TSPL_Customer_INVOICE_HEAD.TAX1,'')='EXEMPT' 
                    'Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX2,'')='EXEMPT'
                    ' Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX3,'')='EXEMPT'
                    '  Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX4,'')='EXEMPT'
                    '   Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX5,'')='EXEMPT'
                    ' Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX6,'')='EXEMPT'
                    '  Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX7,'')='EXEMPT'

                    '   Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX8,'')='EXEMPT'
                    '   Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX9,'')='EXEMPT'
                    '   Or ISNULL(TSPL_Customer_INVOICE_HEAD.TAX10,'')='EXEMPT'
                    ') "
                    Wrcls += " and TSPL_Customer_Invoice_Detail.Is_Tax_Exempted=1 and TSPL_Customer_Invoice_Detail.Tax_Group_Type='S' and TSPL_Customer_Invoice_Detail.Type='O'  "
                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "Nil Rated Invoices - 8A, 8B, 8C, 8D", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))), True)
                ElseIf IIf(exportGSTR, sheetName = Nothing, clsCommon.CompairString(gv2.CurrentRow.Cells("Name").Value, "Taxable Invoice (Direct)") = CompairStringResult.Equal) Then

                    Wrcls += " And isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0  And TSPL_Customer_Invoice_Head.Document_Type='I' " & Environment.NewLine &
                    " AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))" & Environment.NewLine &
                     " and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and  ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,'')='' and isnull(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_Subsidy_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_VCGL,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrap,'')='')  "

                    qry = getBaseQueryforDetail_GSTR1(Wrcls, clsCommon.myCstr(IIf(exportGSTR = True, "Taxable Invoice (Direct)", clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value))))
                End If

                Dim strQry As String = ";With CTE As (" & qry & ")"
                If Not exportGSTR Then
                    dt = clsDBFuncationality.GetDataTable(strQry & "Select * from CTE")

                    'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    '    dt.Columns.Add("InvoiceAmountDisc", GetType(Decimal))
                    '    Dim arrInvNo As New ArrayList
                    '    For ii As Integer = 0 To dt.Rows.Count - 1
                    '        If Not arrInvNo.Contains(dt.Rows(ii)("Sale Invoice No")) Then
                    '            dt.Rows(ii)("InvoiceAmountDisc") = clsCommon.myCDecimal(dt.Rows(ii)("Invoice Amount"))
                    '            arrInvNo.Add(dt.Rows(ii)("Sale Invoice No"))
                    '        End If
                    '    Next
                    '    dt.AcceptChanges()
                    'Else
                    '    Throw New Exception("No Data found")
                    'End If

                    gv3.DataSource = Nothing
                    gv3.Rows.Clear()
                    gv3.Columns.Clear()
                    gv3.GroupDescriptors.Clear()
                    gv3.MasterTemplate.SummaryRowsBottom.Clear()
                    gv3.MasterView.Refresh()

                    If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                        'gv3BlankGridColumns()
                        gv3.DataSource = dt

                        'If btnGSTR1.IsChecked = True And chkItemWise.Checked = False Then
                        '    Dim chkPrevDoc As String = Nothing
                        '    Dim ii As Integer = 0
                        '    For Each gvRows As GridViewRowInfo In gv3.Rows
                        '        If chkPrevDoc IsNot Nothing AndAlso clsCommon.myLen(chkPrevDoc) > 0 Then
                        '            If Not clsCommon.CompairString(chkPrevDoc, clsCommon.myCstr(gvRows.Cells("Document No").Value)) = CompairStringResult.Equal Then
                        '                gv3.Rows(ii).Cells(colDummyInvoiceAmt).Value = clsCommon.myCDecimal(gvRows.Cells("Invoice Amount").Value)
                        '            Else
                        '                gv3.Rows(ii).Cells(colDummyInvoiceAmt).Value = 0
                        '            End If
                        '        Else
                        '            gv3.Rows(ii).Cells(colDummyInvoiceAmt).Value = clsCommon.myCDecimal(gvRows.Cells("Invoice Amount").Value)
                        '        End If
                        '        chkPrevDoc = clsCommon.myCstr(gvRows.Cells("Document No").Value)
                        '        ii += 1
                        '    Next
                        'End If


                        RadPageView1.SelectedPage = RadPageViewPage3
                        gv3.BestFitColumns()
                        gv3.EnableFiltering = True
                        FormatGridFooterDetail()
                        RadPageViewPage3.Text = clsCommon.myCstr(gv2.CurrentRow.Cells("Name").Value)
                    Else
                        RadPageViewPage3.Text = "Detail data"
                        Throw New Exception("No Data Found to Display")
                    End If
                Else
                    strQryGSTR = Nothing
                    strQryGSTR = strQry
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
                    qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Total number of vouchers for the period' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date],TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)- CASE 
            WHEN TSPL_Customer_Invoice_Head.TAX1   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX2  IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX3   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX4   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX5   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX6   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX7   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX8   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX9   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX10  IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
             END      as [Taxable Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine &
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 " & Wrcls & " " & Environment.NewLine

                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Included in returns") = CompairStringResult.Equal Then
                    qry = " select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Included in returns' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date],TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)- CASE 
            WHEN TSPL_Customer_Invoice_Head.TAX1   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX2  IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX3   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX4   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX5   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX6   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX7   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX8   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX9   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
            WHEN TSPL_Customer_Invoice_Head.TAX10  IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
             END      as [Taxable Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount]   from TSPL_Customer_Invoice_Head " & Environment.NewLine &
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine &
                    " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
                    " where 1=1 and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine

                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Included in HSN/SAC Summary") = CompairStringResult.Equal Then
                    qry = "  select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Included in HSN/SAC Summary' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date],TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Customer_Invoice_Head.Document_Type as [Document Type], 
 case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0))
     + CASE WHEN TSPL_Customer_Invoice_Head.TAX1   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX2   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX3   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX4   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX5   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX6   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX7   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX8   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX9   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX10  IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      Else 0 End +
	  CASE 
      WHEN TSPL_Customer_Invoice_Head.TAX1   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX2   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX3   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX4   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX5   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX6   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX7   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX8   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX9   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX10  IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      Else 0 End as [Taxable Value],
 case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0)
  -    CASE 
      WHEN TSPL_Customer_Invoice_Head.TAX1   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX2   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX3   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX4   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX5   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX6   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX7   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX8   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX9   IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX10  IN ('TCS') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      Else 0 End   - 
	   CASE 
      WHEN TSPL_Customer_Invoice_Head.TAX1   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX2   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX3   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX4   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX5   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX6   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX7   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX8   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX9   IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX10  IN ('KKF') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      Else 0 End -
	  CASE 
      WHEN TSPL_Customer_Invoice_Head.TAX1   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX1_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX2   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX2_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX3   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX3_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX4   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX4_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX5   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX5_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX6   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX6_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX7   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX7_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX8   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX8_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX9   IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX9_Amt,0) 
      WHEN TSPL_Customer_Invoice_Head.TAX10  IN ('MNDTAX') THEN ISNULL(TSPL_Customer_Invoice_Head.TAX10_Amt,0) 
      Else 0 End
	  as [Taxable Amount], 
case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount],
case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0)  as [Round Off Amount]     from TSPL_Customer_Invoice_Head " & Environment.NewLine &
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code" & Environment.NewLine &
                    " left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code " & Environment.NewLine &
                     " where 1=1 And case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)>0 and (isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')<>'' or ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " " & Environment.NewLine
                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Not relevant for returns") = CompairStringResult.Equal Or clsCommon.CompairString(gv1.CurrentRow.Cells("Name").Value, "Taxable Sales") = CompairStringResult.Equal Then
                    qry = " Select ROW_NUMBER() OVER(ORDER BY Document_No ASC) as  SNo,'Not relevant for returns' as Particulars,TSPL_Customer_Invoice_Head.Document_No as [Document No],convert(varchar,TSPL_Customer_Invoice_Head.Document_Date ,103) as [Document Date],TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Customer_Invoice_Head.Document_Type as [Document Type],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Head.Discount_Base,0)-isnull(TSPL_Customer_Invoice_Head.Discount_Amount ,0)) as [Taxable Value],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) as [Taxable Amount], case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.Document_Total,0)  as [Invoice Amount],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end * isnull(TSPL_Customer_Invoice_Head.RoundOffAmount,0) as [Round Off Amount]  from (" & Environment.NewLine &
                    " select TSPL_Customer_Invoice_Head.Document_No as [Document No]   from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 " & Wrcls & " and Order_No='C' " & Environment.NewLine &
                    " except " & Environment.NewLine &
                    " select TSPL_Customer_Invoice_Head.Document_No as [Document No]   from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_Customer_Invoice_Head .Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_LOCATION_MASTER on TSPL_Customer_Invoice_Head.Loc_Code =TSPL_LOCATION_MASTER .Location_Code where 1=1 and (isnull(AgainstScrapReturn,'')<>'' or ISNULL (Against_Sale_Return_No,'')<>'' or ISNULL (Against_MCC_Material_Sale_Return,'')<>'' or isnull(Against_Asset_Disposal,'')<>'' or ISNULL (Against_Sale_No,'')<>'' or ISNULL (Against_Security_Receipt_No,'')<>'' or isnull(Against_Service_Visit_Code,'')<>'' or ISNULL (Against_Subsidy_No,'')<>'' or ISNULL (Against_VCGL,'')<>''  or ISNULL (AgainstScrap,'')<>'' ) " & Wrcls & " ) z left outer join  TSPL_Customer_Invoice_Head on z.[Document No] =TSPL_Customer_Invoice_Head.Document_No " & Wrcls & " " & Environment.NewLine &
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
                    frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, dtHeader, "rptGSTCompuatation - Sale", "GST Compuatation", clsCommon.myCDate(txtFromDate.Value), "SubReportOfGSTComHeaderPart.rpt", "SubReportOfGSTComdetailPart-Sale.rpt", dtdetail)
                Else
                    frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, dtHeader, "rptGSTCompuatation", "GST Compuatation", clsCommon.myCDate(txtFromDate.Value), "SubReportOfGSTComHeaderPart.rpt", "SubReportOfGSTComdetailPart.rpt", dtdetail)
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data found to print", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        Qry = "select 'Purchase Invoice' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No as AgainstDocNo" &
                            " ,TSPL_PI_DETAIL.item_code,TSPL_PI_DETAIL.Unit_code as UOM,TSPL_PI_DETAIL.Item_cost,TSPL_PI_DETAIL.PI_Qty as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_PI_DETAIL.Amt_Less_Discount+isnull(tspl_pi_detail.Rejected_Amount,0)) as Amt_Less_Discount," &
                            " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" &
  " case when  PI_Tax1_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end  " &
   "  +case when  PI_Tax2_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end   " &
    " +case when  PI_Tax3_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " &
    " +case when  PI_Tax4_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end   " &
     " +case when  PI_Tax5_Code.type='O' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end " &
     " ) as ItemWise_Exempt_Amt" &
    " , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" &
     " case when  PI_Tax1_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end   " &
     " +case when  PI_Tax2_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end  " &
      " +case when  PI_Tax3_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " &
      " +case when  PI_Tax4_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end  " &
       " +case when  PI_Tax5_Code.type='CGST' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end) as CGST_Amt, " &
        " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" &
        " case when  PI_Tax1_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end  " &
        " +case when  PI_Tax2_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end " &
        " +case when  PI_Tax3_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " &
        " +case when  PI_Tax4_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end  " &
        " +case when  PI_Tax5_Code.type='SGST' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end ) as SGST_Amt" &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * (" &
        " case when  PI_Tax1_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end  " &
         " +case when  PI_Tax2_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end  " &
         " +case when  PI_Tax3_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " &
          " +case when  PI_Tax4_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end   " &
          " +case when  PI_Tax5_Code.type='UGST' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end ) as UGST_Amt" &
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" &
          " case when  PI_Tax1_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax1_Amt,0)else 0 end  " &
            " +case when  PI_Tax2_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax2_amt,0)else 0 end  " &
            " +case when  PI_Tax3_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax3_amt,0)else 0 end  " &
            " +case when  PI_Tax4_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax4_amt,0)else 0 end  " &
            " +case when  PI_Tax5_Code.type='IGST' then ISNULL(TSPL_PI_DETAIL.tax5_amt,0)else 0 end ) as IGST_Amt" &
",TSPL_VENDOR_INVOICE_HEAD.Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,TSPL_PI_HEAD.PI_Type" &
       " from TSPL_VENDOR_INVOICE_HEAD" &
" left join TSPL_PI_HEAD on TSPL_PI_HEAD.pi_no=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No " &
" left join TSPL_PI_DETAIL on TSPL_PI_DETAIL.pi_no=TSPL_PI_HEAD.pi_no" &
" left join tspl_tax_master as PI_Tax1_Code on  PI_Tax1_Code.tax_code=TSPL_PI_DETAIL.tax1 " &
    " left join tspl_tax_master as PI_Tax2_Code on  PI_Tax2_Code.tax_code=TSPL_PI_DETAIL.tax2" &
   " left join tspl_tax_master as PI_Tax3_Code on  PI_Tax3_Code.tax_code=TSPL_PI_DETAIL.tax3 " &
  " left join tspl_tax_master as PI_Tax4_Code on  PI_Tax4_Code.tax_code=TSPL_PI_DETAIL.tax4 " &
  " left join tspl_tax_master as PI_Tax5_Code on  PI_Tax5_Code.tax_code=TSPL_PI_DETAIL.tax5 " &
" left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" &
" where isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' " &
        "" & Whrcls & ""

        Return Qry
    End Function
    Private Function ItemWIsePurchaseReturn() As String
        Dim Qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        Qry = "select 'Purchase Return' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No as AgainstDocNo" &
            " ,TSPL_PR_DETAIL.item_code,TSPL_PR_DETAIL.Unit_code as Uom,TSPL_PR_DETAIL.Item_cost,TSPL_PR_DETAIL.PR_Qty as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_PR_DETAIL.Amt_Less_Discount) as Amt_Less_Discount," &
" case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" &
" case when  PR_Tax1_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax1_Amt,0)else 0 end  " &
     " +case when  PR_Tax2_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " &
    " +case when  PR_Tax3_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " &
    " +case when  PR_Tax4_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " &
     " +case when  PR_Tax5_Code.type='O' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end" &
" ) as ItemWise_Exempt_Amt" &
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(" &
" case when  PR_Tax1_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax1_Amt,0)else 0 end  " &
    " +case when  PR_Tax2_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " &
    " +case when  PR_Tax3_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " &
    " +case when  PR_Tax4_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " &
     " +case when  PR_Tax5_Code.type='CGST' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end" &
" ) as CGST_Amt" &
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  PR_Tax2_Code.type='SGST' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " &
     " +case when  PR_Tax3_Code.type='SGST' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " &
    " +case when  PR_Tax4_Code.type='SGST' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " &
     " +case when  PR_Tax5_Code.type='SGST' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end) as SGST_Amt" &
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(  case when  PR_Tax1_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax1_Amt,0)else 0 end  " &
    " +case when  PR_Tax2_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " &
    " +case when  PR_Tax3_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " &
    " +case when  PR_Tax4_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " &
     " +case when  PR_Tax5_Code.type='UGST' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end) as UGST_Amt" &
" ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(  case when  PR_Tax1_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax1_Amt,0)else 0 end  " &
    " +case when  PR_Tax2_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax2_amt,0)else 0 end   " &
    " +case when  PR_Tax3_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax3_amt,0)else 0 end  " &
    " +case when  PR_Tax4_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax4_amt,0)else 0 end   " &
     " +case when  PR_Tax5_Code.type='IGST' then ISNULL(TSPL_PR_DETAIL.tax5_amt,0)else 0 end) as IGST_Amt" &
      ",TSPL_VENDOR_INVOICE_HEAD.Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" &
       " from TSPL_VENDOR_INVOICE_HEAD" &
" left join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No" &
" left join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No" &
" left join tspl_tax_master as PR_Tax1_Code on  PR_Tax1_Code.tax_code=TSPL_PR_DETAIL.tax1 " &
  " left join tspl_tax_master as PR_Tax2_Code on  PR_Tax2_Code.tax_code=TSPL_PR_DETAIL.tax2" &
  " left join tspl_tax_master as PR_Tax3_Code on  PR_Tax3_Code.tax_code=TSPL_PR_DETAIL.tax3 " &
   " left join tspl_tax_master as PR_Tax4_Code on  PR_Tax4_Code.tax_code=TSPL_PR_DETAIL.tax4 " &
   " left join tspl_tax_master as PR_Tax5_Code on  PR_Tax5_Code.tax_code=TSPL_PR_DETAIL.tax5" &
   " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" &
 " where isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>''" &
        "" & Whrcls & ""

        Return Qry
    End Function
    Private Function ItemWiseDirectAP() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = "select 'Direct AP' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No as AgainstDocNo" &
" ,'' as item_code,''  Uom,0 as Item_cost,0 as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount) as Amt_Less_Discount," &
" case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " &
                 "  +case when  Tax2_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " &
                  " +case when  Tax3_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " &
                  " +case when  Tax4_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " &
                   " +case when  Tax5_Code.type='O' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " &
                   " ) as ItemWise_Exempt_Amt  " &
" , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " &
                  " +case when  Tax2_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " &
                  " +case when  Tax3_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " &
                 " +case when  Tax4_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " &
                  "  +case when  Tax5_Code.type='CGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " &
                  " ) as CGST_Amt " &
 ", case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " &
                  " +case when  Tax2_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " &
                  " +case when  Tax3_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " &
                  " +case when  Tax4_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " &
                   " +case when  Tax5_Code.type='SGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " &
                   " ) as SGST_Amt " &
 " ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * (case when  Tax1_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " &
                  " +case when  Tax2_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " &
                  " +case when  Tax3_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " &
                 " +case when  Tax4_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " &
                   " +case when  Tax5_Code.type='UGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " &
                   " ) as UGST_Amt " &
 " , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(case when  Tax1_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax1_Amt,0)else 0 end  " &
                 "  +case when  Tax2_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax2_amt,0)else 0 end  " &
                  " +case when  Tax3_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax3_amt,0)else 0 end  " &
                 " +case when  Tax4_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax4_amt,0)else 0 end  " &
                   " +case when  Tax5_Code.type='IGST' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.tax5_amt,0)else 0 end  " &
                  " ) as IGST_Amt ,TSPL_VENDOR_INVOICE_HEAD.Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" &
       " from TSPL_VENDOR_INVOICE_HEAD" &
" left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" &
" left join tspl_tax_master as Tax1_Code on  Tax1_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax1  " &
                " left join tspl_tax_master as Tax2_Code on  Tax2_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax2 " &
                " left join tspl_tax_master as Tax3_Code on  Tax3_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax3 " &
                " left join tspl_tax_master as Tax4_Code on  Tax4_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax4 " &
                " left join tspl_tax_master as Tax5_Code on  Tax5_Code.tax_code=TSPL_VENDOR_INVOICE_HEAD.tax5 " &
       " where 2 = 2" &
  " and isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' and  " &
                " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' and  " &
                " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' and " &
                 " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' and " &
                 " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' and " &
                 " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')='' and  " &
                " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')='' and  " &
                "  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')=''  " &
                 "  and 2=(case when isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo,'' )='BM-PR' then 3 else 2 end)  " &
        "" & Whrcls & ""
        Return qry
    End Function

    Private Function ItemWiseBulkMilkPurchaseInvoice() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = " select 'Bulk Milk Purchase Invoice' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No as AgainstDocNo" &
    " ,TSPL_BULK_MILK_PURCHASE_INVOICE_detail.item_code,TSPL_BULK_MILK_PURCHASE_INVOICE_detail.UOM,0 as Item_cost,TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Invoice_Qty as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Actual_Amount) as Amt_Less_Discount,0 as ItemWise_Exempt_Amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" &
   " from TSPL_VENDOR_INVOICE_HEAD" &
" left join TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD on TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD.Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No" &
" left join TSPL_BULK_MILK_PURCHASE_INVOICE_detail on TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Doc_No=TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD.Doc_No" &
" left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" &
" where isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>''" &
        "" & Whrcls & ""
        Return qry
    End Function
    Private Function ItemWiseBulkMilkPurchaseReturn() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = "select 'Bulk Milk Purchase Return' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.RefDocNo as AgainstDocNo" &
" ,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.item_code,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.UOM,0 as Item_cost,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Invoice_Qty as Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Actual_Amount) as Amt_Less_Discount,0 as ItemWise_Exempt_Amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" &
       " from TSPL_VENDOR_INVOICE_HEAD" &
" left join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD on TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' " &
    " left join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL on TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No" &
    " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" &
 " where (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'')" &
        "" & Whrcls & ""
        Return qry
    End Function
    Private Function ItemWiseMilkPurchaseInvoice() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = " select 'Milk Purchase Invoice' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No as AgainstDocNo" &
                ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.item_code,'' as UOM,0 as Item_cost,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Net_Amount) as Amt_Less_Discount," &
            " 0 as ItemWise_Exempt_amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type" &
        " from TSPL_VENDOR_INVOICE_HEAD" &
" left join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_code=TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No" &
" left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.doc_code=TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_code" &
" left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" &
" where  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>''" &
        "" & Whrcls & ""
        Return qry
    End Function
    Private Function itemWiseVCGL() As String
        Dim qry As String = Nothing
        Dim Whrcls As String = Nothing
        Whrcls = WhrClsForAllTran()
        qry = " select 'VCGL' as Trans_Type,TSPL_VENDOR_INVOICE_HEAD.document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_code,TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No as AgainstDocNo,'' as item_code,'' as UOM,0 as Item_cost,0 as Qty," &
                " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end *(TSPL_VENDOR_INVOICE_HEAD.Document_Total ) as Amt_Less_Discount" &
                " , 0 as ItemWise_Exempt_amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,TSPL_VENDOR_MASTER.GSTRegistered,'' as PI_Type " &
                "  from TSPL_VENDOR_INVOICE_HEAD" &
                " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code" &
                " where  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>''" &
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

        strBaseQuery = "select 'B2B Invoices - 3, 4A' as DocType,XX.* from (" &
            "" & strItemWisePurchaseInvoice & "" &
            " " & WhrItemWisePurchaseInvoiceAndReturn & "" &
            " Union all " &
        "" & strItemWisePurchaseReturn & "" &
         " " & WhrItemWisePurchaseInvoiceAndReturn & "" &
        " Union all " &
        "" & strItemWiseDirectAP & "" &
         " " & WhrItemWiseDirectAP & "" &
         ") as XX"

        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" &
        "" & strBaseQuery & "" &
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

        strBaseQuery = "select 'Taxable Purchases' as DocType,XX.* from (" &
            "" & strItemWisePurchaseInvoice & "" &
            " " & WhrItemWisePurchaseInvoiceAndReturn & "" &
            " Union all " &
        "" & strItemWisePurchaseReturn & "" &
         " " & WhrItemWisePurchaseInvoiceAndReturn & "" &
        " Union all " &
        "" & strItemWiseDirectAP & "" &
         " " & WhrItemWiseDirectAP & "" &
         ") as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" &
        "" & strBaseQuery & "" &
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

        strBaseQuery = "select 'B2BUR Invoices - 4B' as DocType, XX.* from( " &
 "" & strItemWisePurchaseInvoice & "" &
            " " & WhrItemWiseMilkPurchaseInvoice & "" &
            " Union all " &
        "" & strItemWisePurchaseReturn & "" &
         " " & WhrItemWiseMilkPurchasedReturn & "" &
        " Union all " &
        "" & strItemWiseDirectAP & "" &
         " " & WhrItemWiseDirectAP & "" &
         ") as XX"

        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" &
     "" & strBaseQuery & "" &
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"

        Return strBaseQuery
    End Function
    Private Function ItemWiseCreditDebitNotesRegular6C()
        Dim strBaseQuery As String = Nothing
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()
        Dim WhrItemWiseDirectAP As String = " and  TSPL_VENDOR_MASTER.GSTRegistered=1  and   TSPL_VENDOR_INVOICE_HEAD.Document_Type in ('C','D')  and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0  "
        strBaseQuery = "select 'Credit/Debit Notes Regular - 6C' as DocType,xx.* from (" &
            "" & strItemWiseDirectAP & "" &
            " " & WhrItemWiseDirectAP & "" &
            ") as XX"
        Return strBaseQuery
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" &
     "" & strBaseQuery & "" &
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
    End Function
    Private Function ItemWiseImportofGoods5()
        Dim strBaseQuery As String = Nothing
        Dim strItemWisePurchaseInvoice As String = ItemWIsePurchaseInvoice()
        Dim strItemWisePurchaseReturn As String = ItemWIsePurchaseReturn()
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()

        Dim WhrItemWisePurchaseInvoiceAndReturn As String = " and  isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0 and TSPL_PI_HEAD.PI_Type='I' "

        strBaseQuery = "select 'Import of Goods - 5' as DocType,XX.* from (" &
            "" & strItemWisePurchaseInvoice & "" &
            " " & WhrItemWisePurchaseInvoiceAndReturn & "" &
         ") as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" &
     "" & strBaseQuery & "" &
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
        Return strBaseQuery

    End Function
    Private Function ItemWiseCreditDebitNotesUnregistered6C()
        Dim strBaseQuery As String = Nothing
        Dim strItemWiseDirectAP As String = ItemWiseDirectAP()
        Dim WhrItemWiseDirectAP As String = " and  TSPL_VENDOR_MASTER.GSTRegistered=0  and   TSPL_VENDOR_INVOICE_HEAD.Document_Type in ('C','D')  and isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0) <>0  "

        strBaseQuery = "select 'Credit/Debit Notes Unregistered - 6C' as DocType,xx.* from (" &
            "" & strItemWiseDirectAP & "" &
            " " & WhrItemWiseDirectAP & "" &
            " ) as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" &
     "" & strBaseQuery & "" &
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


        strBaseQuery = "select 'Nil Rated Invoices - 7 - (Summary)' as DocType,XX.* from ( " &
        "" & strItemWisePurchaseInvoice & "" &
           " " & WhrCls & "" &
           " Union all " &
       "" & strItemWisePurchaseReturn & "" &
        " " & WhrCls & "" &
       " Union all " &
       "" & strItemWiseDirectAP & "" &
        " " & WhrCls & "" &
        " Union all " &
         "" & strItemWiseBulkMilkPurchaseInvoice & "" &
        " " & WhrCls & "" &
        " Union all " &
         "" & strItemWiseBulkMilkPurchaseReturn & "" &
        " " & WhrCls & "" &
        " Union all " &
         "" & strItemWiseMilkPurchaseInvoice & "" &
        " " & WhrCls & "" &
         " Union all " &
         "" & strItemWiseVCGL & "" &
        " " & WhrCls & "" &
        ") as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" &
     "" & strBaseQuery & "" &
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
        Return strBaseQuery
    End Function
    Private Function ItemWiseExempt()
        Dim strBaseQuery As String = Nothing
        Dim strItemWiseMilkPurchaseInvoice As String = ItemWiseMilkPurchaseInvoice()
        Dim strItemWiseBulkMilkPurchaseInvoice As String = ItemWiseBulkMilkPurchaseInvoice()
        Dim strItemWiseItemWiseBulkMilkPurchaseReturn As String = ItemWiseBulkMilkPurchaseReturn()


        Dim WhrCls As String = " and  isnull(TSPL_VENDOR_INVOICE_HEAD.Total_Tax,0)=0 "
        strBaseQuery = "select 'Exempt' as DocType, XX.* from (" &
            "" & strItemWiseMilkPurchaseInvoice & "" &
           " " & WhrCls & "" &
           " Union all " &
       "" & strItemWiseBulkMilkPurchaseInvoice & "" &
        " " & WhrCls & "" &
       " Union all " &
       "" & strItemWiseItemWiseBulkMilkPurchaseReturn & "" &
        " " & WhrCls & "" &
        ") as XX"
        strBaseQuery = "select final.Item_code as [Item Code],max(tspl_item_master.item_desc) as [Item Description],max(tspl_item_master.hsn_Code) as [HSN Code],max(final.UOM) as [UOM],sum(final.Qty) as Qty,sum(final.Amt_Less_Discount) as [Taxable Value],sum(final.ItemWise_Exempt_Amt) as [CESS Amount],sum(final.CGST_Amt) as [CGST Amount],sum(final.SGST_Amt) as [SGST Amount],sum(final.IGST_Amt) as [IGST Amount],sum(final.UGST_Amt) as [UGST Amt],sum(final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Tax],sum(final.Amt_Less_Discount+final.ItemWise_Exempt_Amt+final.CGST_Amt+final.SGST_Amt+final.IGST_Amt+final.UGST_Amt) as [Total Value] from (" &
     "" & strBaseQuery & "" &
     ") as final  left join tspl_item_master on tspl_item_master.item_code= final.item_Code group by final.Item_code"
        Return strBaseQuery
    End Function
    Private Function ItemWiseNillRated()
        Dim strBaseQuery As String = Nothing
        Dim strNilRatedInvoices7Summary As String = ItemWiseNilRatedInvoices7Summary()
        Dim strExempt As String = ItemWiseExempt()

        strBaseQuery = "select pp.[Item code] as [Item Code],max([Item Description]) as [Item Description],max([HSN Code]) as [HSN Code],max(UOM) as [UOM],sum(Qty) as Qty,sum([Taxable Value]) as [Taxable Value],sum([CESS Amount]) as [CESS Amount],sum([CGST Amount]) as [CGST Amount],sum([SGST Amount]) as [SGST Amount],sum([IGST Amount]) as [IGST Amount],sum([UGST Amt]) as [UGST Amt],sum([CGST Amount]+[SGST Amount]+[IGST Amount]+[UGST Amt]) as [Total Tax],sum([Taxable Value]+[CGST Amount]+[SGST Amount]+[IGST Amount]+[UGST Amt]) as [Total Value] from (" &
            " select *,1 as RI from (" &
       "" & strNilRatedInvoices7Summary & "" &
       ") as aa" &
       " union all" &
       " select *,-1 As RI from (" &
        "" & strExempt & "" &
             ") as bb" &
              " ) as pp group by [item code] having sum(RI)>0  or isnull(max([item code]),'')=''"


        Return strBaseQuery
    End Function
    Private Function ItemWiseReversechargesupplies()
        Dim qry As String = Nothing
        qry = "select * from (" &
            " select '' as Trans_Type,'' as document_No,'' as Document_Type,'' as Vendor_code,'' as AgainstDocNo ,'' as item_code,'' as UOM,0 as Item_cost,0 as Qty,0 as Amt_Less_Discount,0 as ItemWise_Exempt_Amt,0 as CGST_Amt,0 as SGST_Amt,0 as UGST_Amt,0 as IGST_AMt,0 as Total_Tax,'' as GSTRegistered,'' as PI_Type from TSPL_VENDOR_INVOICE_HEAD " &
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
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rptGSTR_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.F11 Then
                btnUpldBlnkGSTRExl.Visible = True
            Else
                btnUpldBlnkGSTRExl.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUpldBlnkGSTRExl_Click(sender As Object, e As EventArgs) Handles btnUpldBlnkGSTRExl.Click
        Dim trans As SqlTransaction = Nothing
        Try
            Dim Qry As String = Nothing
            Dim sourceFilePath As String = Nothing
            Dim fileName As String = Nothing
            Dim Code As String = Nothing
            Dim issaved As Boolean = True

            ' Open File Dialog to Select an Excel File
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls"
            openFileDialog.Title = "Select an Excel File"


            If openFileDialog.ShowDialog() = DialogResult.OK Then
                'sourceFilePath = openFileDialog.FileName
                '' Get File Name and Destination Path
                'fileName = Path.GetFileName(sourceFilePath)
                ''Dim destinationFilePath As String = Path.Combine(uploadFolder, fileName)
                sourceFilePath = openFileDialog.FileName
                fileName = Path.GetFileName(sourceFilePath)

                ' Remove Read-Only attribute if set
                Dim fileAttributes As FileAttributes = File.GetAttributes(sourceFilePath)
                If (fileAttributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                    File.SetAttributes(sourceFilePath, fileAttributes And Not FileAttributes.ReadOnly)
                End If
            Else
                Throw New Exception("No file selected. Select a file to upload.")
            End If

            'trans = clsDBFuncationality.GetTransactin()
            Qry = " select MAX(CODE) from TSPL_GSTR_BLANK_SHEET "
            Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
            If clsCommon.myLen(Code) <= 0 Then
                Code = "GSTR0001"
            Else
                Code = clsCommon.incval(Code)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "FileName", fileName)
            'clsCommon.AddColumnsForChange(coll, "COMMENTS", COMMENTS)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            Qry = ""
            Qry = "SELECT Count(*) FROM TSPL_GSTR_BLANK_SHEET where CODE = '" + Code + "' "
            Dim check As Integer = clsDBFuncationality.getSingleValue(Qry, trans)
            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "CODE", Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GSTR_BLANK_SHEET", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GSTR_BLANK_SHEET", OMInsertOrUpdate.Update, " CODE = '" + Code + "'  ", trans)
            End If

            If clsCommon.myLen(sourceFilePath) > 0 Then
                Dim str As String = Nothing
                Dim bData As Byte()
                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(sourceFilePath)))
                bData = br.ReadBytes(br.BaseStream.Length)
                str = " UPDATE TSPL_GSTR_BLANK_SHEET set FileData = @BLOBData where CODE='" + Code + "'"
                Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                Dim prm As New SqlParameter("@BLOBData", bData)
                'cmd.Transaction = trans
                cmd.Parameters.Add(prm)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    issaved = True
                Else
                    issaved = False
                End If
                br.Close() ' done by stuti reagrding memory leakage
            End If

            If issaved Then
                clsCommon.MyMessageBoxShow(Me, "File Uploaded Successfully.", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "File Upload Failed.", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnExportGSTR1_Click(sender As Object, e As EventArgs) Handles btnExportGSTR1.Click
        Try
            Dim forlderName As String = clsCommon.myCstr(objCommonVar.ImportExportDrive) + ":\ERPTempFolder" + "\" + objCommonVar.CurrDatabase + "\" + objCommonVar.CurrentUserCode + "\Downloads\"

            Dim IsExists As Boolean = System.IO.Directory.Exists(forlderName)
            If IsExists = False Then
                System.IO.Directory.CreateDirectory(forlderName)
            End If
            Dim outputFilePath As String = forlderName
            Dim strQry As String = "SELECT TOP 1 * FROM TSPL_GSTR_BLANK_SHEET ORDER BY Created_Date DESC"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                outputFilePath += clsCommon.myCstr(DateTime.Now.ToString("yyyyMMddHHmmss")) + clsCommon.myCstr(dt.Rows(0)("FileName"))

                If File.Exists(outputFilePath) Then
                    File.Delete(outputFilePath)
                End If

                If Not File.Exists(outputFilePath) Then
                    'If clsCommon.MyMessageBoxShow(Me, "File does not exist at " & outputFilePath & Environment.NewLine & "Are you sure you want to download the GSTR blank sheet?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Dim fileData As Byte() = TryCast(dt.Rows(0)("FileData"), Byte())
                    If fileData IsNot Nothing AndAlso fileData.Length > 0 Then
                        Try
                            ' Write file
                            File.WriteAllBytes(outputFilePath, fileData)

                            ' Ensure file is writable
                            File.SetAttributes(outputFilePath, FileAttributes.Normal)

                            ' Notify user
                            'clsCommon.MyMessageBoxShow(Me, "File downloaded successfully at " & outputFilePath, Me.Text)
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try
                    Else
                        Throw New Exception("File data is empty.")
                    End If
                    'End If
                End If
            Else
                Throw New Exception("GSTR blank sheet not found!")
            End If


            exportGSTR = True
            Try
                clsCommon.ProgressBarPercentShow()
                exportExcel(outputFilePath)
                clsCommon.ProgressBarPercentHide()
                exportGSTR = False
            Catch ex As Exception
                exportGSTR = False
                clsCommon.ProgressBarPercentHide()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            exportGSTR = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function GetDataWithoutGrid() As String
        Dim Qry As String = "WITH InvoiceBase AS ( 
    SELECT 
        (CASE 
            WHEN ISNULL(Against_Sale_Return_No, '') <> '' THEN Against_Sale_Return_No 
            WHEN ISNULL(Against_Sale_No, '') <> '' THEN Against_Sale_No 
            WHEN ISNULL(AgainstScrap, '') <> '' THEN AgainstScrap 
            WHEN ISNULL(AgainstScrapReturn, '') <> '' THEN AgainstScrapReturn   
            WHEN ISNULL(Against_VCGL, '') <> '' THEN Against_VCGL 
            WHEN ISNULL(Against_MCC_Material_Sale_Return, '') <> '' THEN Against_MCC_Material_Sale_Return  
            WHEN ISNULL(Against_Security_Receipt_No, '') <> '' THEN Against_Security_Receipt_No  
            ELSE '' 
        END) AS InvoiceNumber,
        Document_Date,
        0 AS IsCancelled
    FROM TSPL_Customer_INVOICE_HEAD
    WHERE ISNULL(Against_VCGL, '') = '' And CONVERT(date, Document_Date, 103) >= CONVERT(date, '" + txtFromDate.Value + "', 103) AND CONVERT(date, Document_Date, 103) <= CONVERT(date, '" + txtToDate.Value + "', 103)

    UNION ALL

    SELECT 
        (CASE 
            WHEN ISNULL(Against_Sale_Return_No, '') <> '' THEN Against_Sale_Return_No 
            WHEN ISNULL(Against_Sale_No, '') <> '' THEN Against_Sale_No 
            WHEN ISNULL(AgainstScrap, '') <> '' THEN AgainstScrap 
            WHEN ISNULL(AgainstScrapReturn, '') <> '' THEN AgainstScrapReturn   
            WHEN ISNULL(Against_VCGL, '') <> '' THEN Against_VCGL 
            WHEN ISNULL(Against_MCC_Material_Sale_Return, '') <> '' THEN Against_MCC_Material_Sale_Return  
            WHEN ISNULL(Against_Security_Receipt_No, '') <> '' THEN Against_Security_Receipt_No  
            ELSE '' 
        END) AS InvoiceNumber,
        Document_Date,
        1 AS IsCancelled
    FROM TSPL_CUSTOMER_INVOICE_HEAD_Cancel_Data
    WHERE ISNULL(Against_VCGL, '') = '' And CONVERT(date, Document_Date, 103) >= CONVERT(date, '" + txtFromDate.Value + "', 103) AND CONVERT(date, Document_Date, 103) <= CONVERT(date, '" + txtToDate.Value + "', 103)
),

SplitInvoice AS (
    SELECT 
        InvoiceNumber,
        Document_Date,
        x.value('/x[1]', 'VARCHAR(50)') AS Part1,
        x.value('/x[2]', 'VARCHAR(50)') AS Part2,
        x.value('/x[3]', 'VARCHAR(50)') AS Part3,IsCancelled
		
    FROM (
        SELECT 
            InvoiceNumber,
            Document_Date,
            CAST('<x>' + 
                REPLACE(
                    REPLACE(
                        REPLACE(
                            REPLACE(InvoiceNumber, '/', '</x><x>'),
                        '-', '</x><x>'),
                    ':', '</x><x>'),
                '_', '</x><x>') 
            + '</x>' AS XML) AS x,IsCancelled
        FROM InvoiceBase
    ) AS SplitXML
)
SELECT 
	'Invoices for outward supply' As [Nature of Document],
    MIN(Cast(InvoiceNumber As Varchar)) AS [Sr. No. From],
    MAX(Cast(InvoiceNumber As Varchar)) AS [Sr. No. To],	
	Count(Part3) As [Total Number],	
	Sum(Case When IsCancelled=1 Then 1 Else 0 End) As [Cancelled]
FROM SplitInvoice
GROUP BY  Part1 
having Count(Part3)>0 "
        Return Qry
    End Function


    'Sub exportExcel(ByVal dt As DataTable, ByVal filePath As String, ByVal i As Integer)
    Sub exportExcel(ByVal filePath As String)
        Try
            'If i = 1 Then
            excelApp = New Excel.Application()
            workbook = excelApp.Workbooks.Open(filePath)
            'End If

            ' Get all sheet names dynamically
            Dim sheetNames As New List(Of String)()
            For Each tempSheet As Excel.Worksheet In workbook.Sheets
                sheetNames.Add(tempSheet.Name)
                Marshal.ReleaseComObject(tempSheet) ' Release tempSheet object
            Next
            Dim i As Integer = 1
            ' Loop through each existing sheet and write data
            For Each sheetName As String In sheetNames
                Dim dt As DataTable = Nothing
                clsCommon.ProgressBarPercentUpdate((i / (gv2.Rows.Count + 1)) * 100, "Processing...")
                If Not clsCommon.CompairString(sheetName, "Help Instruction") = CompairStringResult.Equal Then
                    sheet = CType(workbook.Sheets(sheetName), Excel.Worksheet)
                    DrillDownDetailForGSTR1(sheetName)
                    If clsCommon.CompairString(sheetName, "b2b,sez,de") = CompairStringResult.Equal Then 'AndAlso i = 1 Then
                        dt = clsDBFuncationality.GetDataTable(strQryGSTR & "Select [GST No],[Customer Name],[Sale Invoice No],Format([Sale Invoice Date],'dd-MMM-yyyy')[Sale Invoice Date],[Invoice Amount],[Place of Supply],[Reverse Charges],'' As [Applicable of Tax Rate],[Invoice Type],'' As [E-Commerce GSTIN],TaxRate,[Taxable Value],'' As [Cess Amount] from CTE Order By SNo")
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                    ElseIf clsCommon.CompairString(sheetName, "b2cl") = CompairStringResult.Equal Then ' AndAlso i = 4 Then
                        dt = clsDBFuncationality.GetDataTable(strQryGSTR & "Select [Sale Invoice No],Format([Sale Invoice Date],'dd-MMM-yyyy')[Sale Invoice Date],[Invoice Amount],[Place of Supply],'' As [Applicable of Tax Rate],TaxRate,[Taxable Value],'' As [Cess Amount],'' As [E-Commerce GSTIN] from CTE")
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                    ElseIf clsCommon.CompairString(sheetName, "b2cs") = CompairStringResult.Equal Then 'AndAlso i = 5 Then
                        dt = clsDBFuncationality.GetDataTable(strQryGSTR & "Select 'OE' As [Type],[Place of Supply],'' As [Applicable of Tax Rate],TaxRate,Sum([Taxable Value])[Taxable Value],'' As [Cess Amount],'' As [E-Commerce GSTIN] from CTE Group By [Place of Supply],TaxRate Order By TaxRate")
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                    ElseIf clsCommon.CompairString(sheetName, "cdnr") = CompairStringResult.Equal Then 'AndAlso i = 6 Then
                        dt = clsDBFuncationality.GetDataTable(strQryGSTR & "Select [GST No] As [GSTIN/UIN of Recipient],[Customer Name] As [Receiver Name],[Document No] As [Note Number],[Document Date] As [Note Date],[Document Type] As [Note Type],[Place of Supply],[Reverse Charges],[Invoice Type] As [Note Supply Type],[Invoice Amount] As [Note Value],'' [Applicable % of Tax Rate],TaxRate As Rate,[Taxable Value],'' As [Cess Amount] from CTE")
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                    ElseIf clsCommon.CompairString(sheetName, "cdnur") = CompairStringResult.Equal Then 'AndAlso i = 7 Then
                        dt = clsDBFuncationality.GetDataTable(strQryGSTR & "Select '' As [UR Type],[Document No] As [Note Number],[Document Date] As [Note Date],[Document Type] As [Note Type],[Place of Supply],[Invoice Amount] As [Note Value],'' As [Applicable % of Tax Rate],TaxRate As [Rate],[Taxable Value],'' As [Cess Amount] from CTE")
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                    ElseIf clsCommon.CompairString(sheetName, "exemp") = CompairStringResult.Equal Then 'AndAlso i = 11 Then
                        Dim Qry As String = strQryGSTR & "Select Description ,Sum([Nil Rated Supplies])[Nil Rated Supplies] ,Sum([Exempted(other than nil rated/non GST supply)])[Exempted(other than nil rated/non GST supply)], Sum([Non-GST Supplies])[Non-GST Supplies] 
from (Select 'Intra-State supplies to registered persons' As Description,0 As [Nil Rated Supplies],0 As [Exempted(other than nil rated/non GST supply)],0 As [Non-GST Supplies],'' As [Place of Supply]
Union All
Select 'Intra-State supplies to unregistered persons' As Description,0 As [Nil Rated Supplies],0 As [Exempted(other than nil rated/non GST supply)],0 As [Non-GST Supplies],'' As [Place of Supply]
Union All
Select 'Inter-State supplies to unregistered persons' As Description,0 As [Nil Rated Supplies],0 As [Exempted(other than nil rated/non GST supply)],0 As [Non-GST Supplies],'' As [Place of Supply]
Union All
Select 'Inter-State supplies to registered persons' As Description,0 As [Nil Rated Supplies],0 As [Exempted(other than nil rated/non GST supply)],0 As [Non-GST Supplies],'' As [Place of Supply]
Union All "
                        Qry += " Select Description,0 As [Nil Rated Supplies],Sum([Invoice Amount])[Exempted(other than nil rated/non GST supply)],0 As [Non-GST Supplies],[Place of Supply] from CTE Group By Description,[Place of Supply]"
                        Qry += ")abc Group By Description"
                        dt = clsDBFuncationality.GetDataTable(Qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                    ElseIf clsCommon.CompairString(sheetName, "hsn(b2b)") = CompairStringResult.Equal Then 'AndAlso i = 13 Then
                        DrillDownDetailForGSTR1_ItemWise(sheetName)
                        dt = clsDBFuncationality.GetDataTable(strQryGSTR & "Select [HSN Code],Max(Description)Description,Max(UOM)UOM,Sum(TotalQty)TotalQty,Sum(TotalAmount)TotalAmount,Max(Tax_Rate)Tax_Rate,Sum(TotalTaxableValue)TotalTaxableValue,Sum(IGSTAmount)IGSTAmount,Sum(CGSTAmount)CGSTAmount,Sum(SGSTAmount)SGSTAmount,'' As [Cess Amount]  from CTE Where TotalQty<>0 Group By [HSN Code]")
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                    ElseIf clsCommon.CompairString(sheetName, "hsn(b2c)") = CompairStringResult.Equal Then 'AndAlso i = 13 Then
                        DrillDownDetailForGSTR1_ItemWise(sheetName)
                        dt = clsDBFuncationality.GetDataTable(strQryGSTR & "Select [HSN Code],Max(Description)Description,Max(UOM)UOM,Sum(TotalQty)TotalQty,Sum(TotalAmount)TotalAmount,Max(Tax_Rate)Tax_Rate,Sum(TotalTaxableValue)TotalTaxableValue,Sum(IGSTAmount)IGSTAmount,Sum(CGSTAmount)CGSTAmount,Sum(SGSTAmount)SGSTAmount,'' As [Cess Amount]  from CTE  Where TotalQty<>0 Group By [HSN Code]")
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                    ElseIf clsCommon.CompairString(sheetName, "docs") = CompairStringResult.Equal Then
                        'If Not chkWithoutGrid Then
                        dt = clsDBFuncationality.GetDataTable(GetDataWithoutGrid())
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            WriteDataTableToSheet(sheet, dt, 3) ' Writing data from row 3
                        End If
                        '    chkWithoutGrid = True
                        'End If
                    End If
                    Marshal.ReleaseComObject(sheet) ' Release sheet object
                    sheet = Nothing
                End If
                i += 1
            Next


            '' Check if file is read only
            Dim fileInfo As New IO.FileInfo(filePath)
            If fileInfo.IsReadOnly Then
                fileInfo.IsReadOnly = False
            End If

            ' Save and close workbook
            workbook.Save()
            'If i = (gv2.Rows.Count + 1) Then
            workbook.Close(False)
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "File saved successfully at " & filePath, Me.Text)
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If i = (gv2.Rows.Count + 1) Then
            ' Release COM objects
            If workbook IsNot Nothing Then Marshal.ReleaseComObject(workbook)
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                Marshal.ReleaseComObject(excelApp)
            End If

            ' Force Garbage Collection
            workbook = Nothing
            excelApp = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            Process.Start(filePath)
            'End If
        End Try
    End Sub


    Sub WriteDataTableToSheet(sheet As Excel.Worksheet, dt As DataTable, startRow As Integer)
        Try
            Dim rowCount As Integer = dt.Rows.Count
            Dim colCount As Integer = dt.Columns.Count
            Dim dataArray(rowCount, colCount - 1) As Object

            '' Fill header row
            'For colIndex As Integer = 0 To colCount - 1
            '    sheet.Cells(startRow + 1, colIndex + 1).Value2 = dt.Columns(colIndex).ColumnName
            'Next

            ' Fill data into array
            For rowIndex As Integer = 0 To rowCount - 1
                For colIndex As Integer = 0 To colCount - 1
                    dataArray(rowIndex, colIndex) = dt.Rows(rowIndex)(colIndex)
                Next
            Next

            ' Set range in Excel to update all cells at once
            Dim startCell As Excel.Range = sheet.Cells(startRow + 2, 1)
            Dim endCell As Excel.Range = sheet.Cells(startRow + 1 + rowCount, colCount)
            Dim writeRange As Excel.Range = sheet.Range(startCell, endCell)

            writeRange.Value2 = dataArray ' Write all data in one go
            ' Force Excel to update
            sheet.Application.ScreenUpdating = True
            sheet.Application.Calculate()
        Catch ex As Exception
            Throw New Exception("Error writing data to Excel: " & ex.Message)
        End Try
    End Sub

    'Sub WriteDataTableToSheet(sheet As Excel.Worksheet, dt As DataTable, startRow As Integer)
    '    Try
    '        Dim rowIndex As Integer = startRow + 1 ' Excel rows start from 1

    '        ' Write column headers
    '        For colIndex As Integer = 0 To dt.Columns.Count - 1
    '            sheet.Cells(rowIndex, colIndex + 1).Value = dt.Columns(colIndex).ColumnName
    '        Next
    '        rowIndex += 1 ' Move to next row for data

    '        ' Write data
    '        For Each row As DataRow In dt.Rows
    '            For colIndex As Integer = 0 To dt.Columns.Count - 1
    '                sheet.Cells(rowIndex, colIndex + 1).Value = row(colIndex)
    '            Next
    '            rowIndex += 1
    '        Next

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub



End Class
