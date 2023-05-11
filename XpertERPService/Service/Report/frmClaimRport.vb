'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource

Public Class FrmClaimRport
    Inherits FrmMainTranScreen

    Dim qry As String
    Dim dt As DataTable

    Private Sub FrmClaimRport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmClaimReport)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        ' '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        'btnExport.Visible = MyBase.isExport
    End Sub

    Sub reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadCustomer()
        LoadVendor()
        chkCustomerAll.IsChecked = True
        chkVendorAll.IsChecked = True
        chkMRP.IsChecked = True
        GV1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub LoadLocation()
        qry = "select Location_Code AS Code ,Location_Desc as Description FROM TSPL_LOCATION_MASTER WHERE 2=2 "
        cbgLoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLoc.ValueMember = "Code"
        cbgLoc.DisplayMember = "Description"
    End Sub

    Sub LoadCustomer()
        qry = "Select Cust_Code as Code, Customer_Name As Name from TSPL_CUSTOMER_MASTER Where Status='N'"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub
    Sub LoadVendor()
        qry = "select Vendor_Code as Code,Vendor_Name as Name  from TSPL_VENDOR_MASTER  Where Status='N'"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
    End Sub

    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            If chkLocationSelect.IsChecked = True AndAlso cbgLoc.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            ElseIf chkCustomerSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Customer or select ALL")
                Return
            ElseIf chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Vendor or select ALL")
                Return
            End If
            If radioSummary.IsChecked Then
                If cmbMonth.Text = "" Then
                    clsCommon.MyMessageBoxShow("Please Select Month ")
                    Return
                ElseIf cmbYear.Text = "" Then
                    clsCommon.MyMessageBoxShow("Please Select Year ")
                    Return
                End If
            Else
                If fromDate.Value > ToDate.Value Then
                    clsCommon.MyMessageBoxShow("' From Date ' Can Not Be Lager Then ' To Date ' ")
                    Return
                End If
            End If
            GV1.EnableFiltering = True
            Dim dt As DataTable
            Dim strFromDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
            If chkLandingCost.IsChecked Then
                If radioCustomerWise.IsChecked Or radioDocumentWise.IsChecked Then
                    qry = " select comp_code,Comp_Name,Add1,Add2,Add3,Telephone,Fax, DATEname(MONTH,Document_Date) as Month_Name,YEAR(Document_Date )as Document_Year,Document_Code,Document_Date,Customer_Code,Customer_Name,Customer_Code+'-'+Customer_Name as Customer,Item_Code,Item_Desc,isnull(Landing_Cost,0) as Landing_Cost,isnull(MRP,0) as MRP,isnull(Margin_Percent,0)as Margin_Percent,(isnull(Margin_Percent,0)*isnull(Landing_Cost,0))/100 as Margin_Amount,isnull(Qty,0) as Qty,isnull(Billing_price,0) as Billing_price,isnull(Billing_Amount,0)as Billing_Amount ,isnull(Selling_Price,0) as Selling_Price,isnull(Selling_Amount,0) as Selling_Amount,isnull(case when (Billing_Amount-Selling_Amount)>0 then (Billing_Amount-Selling_Amount) else 0 end,0) as  Claim_Amount ,isnull(PrincipleCode,'') as PrincipleCode ,isnull(PrincipleDesc,'') as PrincipleDesc  from (select TSPL_SD_SALE_INVOICE_HEAD.Comp_Code as comp_code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Phone1 as Telephone,TSPL_COMPANY_MASTER.Fax ,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_SD_SALE_INVOICE_DETAIL.Landing_Cost,TSPL_SD_SALE_INVOICE_DETAIL.MRP,TSPL_SD_SALE_INVOICE_DETAIL.Markup_Percent as Margin_Percent,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost as Billing_price,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*TSPL_SD_SALE_INVOICE_DETAIL.Qty as Billing_Amount,xx.Rate as Selling_Price,xx.Rate*TSPL_SD_SALE_INVOICE_DETAIL.Qty as Selling_Amount,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleCode,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleDesc from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join TSPL_COMPANY_MASTER  on TSPL_SD_SALE_INVOICE_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  left outer join (select TSPL_STANDARD_RATE_ITEM.StdRateCode,TSPL_STANDARD_RATE_ITEM.FomeDate,TSPL_STANDARD_RATE_ITEM.Valied_Date,TSPL_STANDARD_RATE_ITEM.Cust_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Item_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Rate  from TSPL_STANDARD_RATE_ITEM left outer join TSPL_STANDARD_RATE_ITEM_DETAIL on TSPL_STANDARD_RATE_ITEM_DETAIL.StdRateCode=TSPL_STANDARD_RATE_ITEM.StdRateCode where IsCustomer='Y' and IsValied_Date=1 union all select TSPL_STANDARD_RATE_ITEM.StdRateCode,TSPL_STANDARD_RATE_ITEM.FomeDate,getdate() as Valied_Date,TSPL_STANDARD_RATE_ITEM.Cust_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Item_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Rate  from TSPL_STANDARD_RATE_ITEM  left outer join TSPL_STANDARD_RATE_ITEM_DETAIL on TSPL_STANDARD_RATE_ITEM_DETAIL.StdRateCode=TSPL_STANDARD_RATE_ITEM.StdRateCode   where IsCustomer='Y' and IsValied_Date=0 ) as xx on xx.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and xx.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code and TSPL_SD_SALE_INVOICE_HEAD.Document_Date >= xx.FomeDate and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<=xx.Valied_Date )as zzz where 1=1   "
                    qry += " and Document_Date>='" + strFromDate + "' and Document_Date<='" + strToDate + "' "
                    If chkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                    End If
                    If chkLocationSelect.IsChecked And cbgLoc.CheckedValue.Count > 0 Then
                        qry += " AND Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")"
                    End If
                    If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                        qry += " AND PrincipleCode in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
                    End If
                Else
                    qry = "select max(comp_code) as comp_code,max(Comp_Name) as Comp_Name,max(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3,max(Telephone) as Telephone,max(Fax)as Fax ,max(Month_Name) as Month_Name,max(Document_Year) as Document_Year,max(PrincipleCode) as PrincipleCode,max(PrincipleDesc) as PrincipleDesc,max(PrincipleCode)+'-'+max(PrincipleDesc) as Principle,max(vadd1) as vAdd1,max(vadd2) as vAdd2,max(vadd3) as vAdd3,max(vCity) as vCity ,Customer_Code ,max(Customer_Name) as Customer_Name,max(Customer) as Customer,SUM(Claim_Amount) as Claim_Amount  from (select comp_code,Comp_Name,Add1,Add2,Add3,Telephone,Fax, DATEname(MONTH,Document_Date) as Month_Name,YEAR(Document_Date )as Document_Year,Document_Code,Document_Date,Customer_Code,Customer_Name,Customer_Code+'-'+Customer_Name as Customer,Item_Code,Item_Desc,isnull(Landing_Cost,0) as Landing_Cost,isnull(MRP,0) as MRP,isnull(Margin_Percent,0)as Margin_Percent,(isnull(Margin_Percent,0)*isnull(Landing_Cost,0))/100 as Margin_Amount,isnull(Qty,0) as Qty,isnull(Billing_price,0) as Billing_price,isnull(Billing_Amount,0)as Billing_Amount ,isnull(Selling_Price,0) as Selling_Price,isnull(Selling_Amount,0) as Selling_Amount,isnull(case when (Billing_Amount-Selling_Amount)>0 then (Billing_Amount-Selling_Amount) else 0 end,0) as  Claim_Amount ,isnull(PrincipleCode,'') as PrincipleCode ,isnull(PrincipleDesc,'') as PrincipleDesc,ISNULL(vadd1,'')  as vadd1,ISNULL(vadd2,'')  as vadd2,ISNULL(vadd3,'')  as vadd3,ISNULL(vCity ,'')  as vCity  from (select TSPL_SD_SALE_INVOICE_HEAD.Comp_Code as comp_code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Phone1 as Telephone,TSPL_COMPANY_MASTER.Fax ,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_SD_SALE_INVOICE_DETAIL.Landing_Cost,TSPL_SD_SALE_INVOICE_DETAIL.MRP,TSPL_SD_SALE_INVOICE_DETAIL.Markup_Percent as Margin_Percent,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost as Billing_price,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*TSPL_SD_SALE_INVOICE_DETAIL.Qty as Billing_Amount,xx.Rate as Selling_Price,xx.Rate*TSPL_SD_SALE_INVOICE_DETAIL.Qty as Selling_Amount,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleCode,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleDesc,TSPL_VENDOR_MASTER.Add1 as vAdd1,TSPL_VENDOR_MASTER.Add2 as vAdd2,TSPL_VENDOR_MASTER.Add3 as vAdd3,TSPL_VENDOR_MASTER.City_Code_Desc as vCity from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join TSPL_COMPANY_MASTER  on TSPL_SD_SALE_INVOICE_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SD_SALE_INVOICE_DETAIL.PrincipleCode left outer join (select TSPL_STANDARD_RATE_ITEM.StdRateCode,TSPL_STANDARD_RATE_ITEM.FomeDate,TSPL_STANDARD_RATE_ITEM.Valied_Date,TSPL_STANDARD_RATE_ITEM.Cust_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Item_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Rate  from TSPL_STANDARD_RATE_ITEM left outer join TSPL_STANDARD_RATE_ITEM_DETAIL on TSPL_STANDARD_RATE_ITEM_DETAIL.StdRateCode=TSPL_STANDARD_RATE_ITEM.StdRateCode where IsCustomer='Y' and IsValied_Date=1 union all select TSPL_STANDARD_RATE_ITEM.StdRateCode,TSPL_STANDARD_RATE_ITEM.FomeDate,getdate() as Valied_Date,TSPL_STANDARD_RATE_ITEM.Cust_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Item_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Rate  from TSPL_STANDARD_RATE_ITEM  left outer join TSPL_STANDARD_RATE_ITEM_DETAIL on TSPL_STANDARD_RATE_ITEM_DETAIL.StdRateCode=TSPL_STANDARD_RATE_ITEM.StdRateCode   where IsCustomer='Y' and IsValied_Date=0 ) as xx on xx.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and xx.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code and TSPL_SD_SALE_INVOICE_HEAD.Document_Date >= xx.FomeDate and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<=xx.Valied_Date  )  as zzz    where 1=1  "
                    qry += " and datename(month,document_date)='" + clsCommon.myCstr(cmbMonth.Text) + "' and year(document_date)='" + clsCommon.myCstr(cmbYear.Text) + "' "
                    If chkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                    End If
                    If chkLocationSelect.IsChecked And cbgLoc.CheckedValue.Count > 0 Then
                        qry += " AND Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")"
                    End If
                    If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                        qry += " AND PrincipleCode in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
                    End If
                    qry += " )   summary group by Customer_Code   "

                End If
            Else
                If radioCustomerWise.IsChecked Or radioDocumentWise.IsChecked Then
                    qry = " select comp_code,Comp_Name,Add1,Add2,Add3,Telephone,Fax, DATEname(MONTH,Document_Date) as Month_Name,YEAR(Document_Date )as Document_Year,Document_Code,Document_Date,Customer_Code,Customer_Name,Customer_Code+'-'+Customer_Name as Customer,Item_Code,Item_Desc,isnull(Landing_Cost,0) as Landing_Cost,isnull(MRP,0) as MRP,isnull(Margin_Percent,0)as Margin_Percent,(isnull(Margin_Percent,0)*isnull(MRP,0))/100 as Margin_Amount,isnull(Qty,0) as Qty,isnull(Billing_price,0) as Billing_price,isnull(Billing_Amount,0)as Billing_Amount ,isnull(Selling_Price,0) as Selling_Price,isnull(Selling_Amount,0) as Selling_Amount,isnull(case when (Billing_Amount-Selling_Amount)>0 then (Billing_Amount-Selling_Amount) else 0 end,0) as  Claim_Amount ,isnull(PrincipleCode,'') as PrincipleCode ,isnull(PrincipleDesc,'') as PrincipleDesc  from (select TSPL_SD_SALE_INVOICE_HEAD.Comp_Code as comp_code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Phone1 as Telephone,TSPL_COMPANY_MASTER.Fax ,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_SD_SALE_INVOICE_DETAIL.Landing_Cost,TSPL_SD_SALE_INVOICE_DETAIL.MRP,TSPL_SD_SALE_INVOICE_DETAIL.Markup_Percent as Margin_Percent,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost as Billing_price,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*TSPL_SD_SALE_INVOICE_DETAIL.Qty as Billing_Amount,xx.Rate as Selling_Price,xx.Rate*TSPL_SD_SALE_INVOICE_DETAIL.Qty as Selling_Amount,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleCode,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleDesc from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join TSPL_COMPANY_MASTER  on TSPL_SD_SALE_INVOICE_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  left outer join (select TSPL_STANDARD_RATE_ITEM.StdRateCode,TSPL_STANDARD_RATE_ITEM.FomeDate,TSPL_STANDARD_RATE_ITEM.Valied_Date,TSPL_STANDARD_RATE_ITEM.Cust_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Item_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Rate  from TSPL_STANDARD_RATE_ITEM left outer join TSPL_STANDARD_RATE_ITEM_DETAIL on TSPL_STANDARD_RATE_ITEM_DETAIL.StdRateCode=TSPL_STANDARD_RATE_ITEM.StdRateCode where IsCustomer='Y' and IsValied_Date=1 union all select TSPL_STANDARD_RATE_ITEM.StdRateCode,TSPL_STANDARD_RATE_ITEM.FomeDate,getdate() as Valied_Date,TSPL_STANDARD_RATE_ITEM.Cust_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Item_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Rate  from TSPL_STANDARD_RATE_ITEM  left outer join TSPL_STANDARD_RATE_ITEM_DETAIL on TSPL_STANDARD_RATE_ITEM_DETAIL.StdRateCode=TSPL_STANDARD_RATE_ITEM.StdRateCode   where IsCustomer='Y' and IsValied_Date=0 ) as xx on xx.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and xx.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code and TSPL_SD_SALE_INVOICE_HEAD.Document_Date >= xx.FomeDate and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<=xx.Valied_Date )as zzz where 1=1  "
                    qry += " and Document_Date>='" + strFromDate + "' and Document_Date<='" + strToDate + "' "
                    If chkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                    End If
                    If chkLocationSelect.IsChecked And cbgLoc.CheckedValue.Count > 0 Then
                        qry += " AND Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")"
                    End If
                    If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                        qry += " AND PrincipleCode in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
                    End If
                Else
                    qry = "select max(comp_code) as comp_code,max(Comp_Name) as Comp_Name,max(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3,max(Telephone) as Telephone,max(Fax)as Fax ,max(Month_Name) as Month_Name,max(Document_Year) as Document_Year,max(PrincipleCode) as PrincipleCode,max(PrincipleDesc) as PrincipleDesc,max(PrincipleCode)+'-'+max(PrincipleDesc) as Principle,max(vadd1) as vAdd1,max(vadd2) as vAdd2,max(vadd3) as vAdd3,max(vCity) as vCity ,Customer_Code ,max(Customer_Name) as Customer_Name,max(Customer) as Customer,SUM(Claim_Amount) as Claim_Amount  from (select comp_code,Comp_Name,Add1,Add2,Add3,Telephone,Fax, DATEname(MONTH,Document_Date) as Month_Name,YEAR(Document_Date )as Document_Year,Document_Code,Document_Date,Customer_Code,Customer_Name,Customer_Code+'-'+Customer_Name as Customer,Item_Code,Item_Desc,isnull(Landing_Cost,0) as Landing_Cost,isnull(MRP,0) as MRP,isnull(Margin_Percent,0)as Margin_Percent,(isnull(Margin_Percent,0)*isnull(Landing_Cost,0))/100 as Margin_Amount,isnull(Qty,0) as Qty,isnull(Billing_price,0) as Billing_price,isnull(Billing_Amount,0)as Billing_Amount ,isnull(Selling_Price,0) as Selling_Price,isnull(Selling_Amount,0) as Selling_Amount,isnull(case when (Billing_Amount-Selling_Amount)>0 then (Billing_Amount-Selling_Amount) else 0 end,0) as  Claim_Amount ,isnull(PrincipleCode,'') as PrincipleCode ,isnull(PrincipleDesc,'') as PrincipleDesc,ISNULL(vadd1,'')  as vadd1,ISNULL(vadd2,'')  as vadd2,ISNULL(vadd3,'')  as vadd3,ISNULL(vCity ,'')  as vCity  from (select TSPL_SD_SALE_INVOICE_HEAD.Comp_Code as comp_code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Phone1 as Telephone,TSPL_COMPANY_MASTER.Fax ,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_SD_SALE_INVOICE_DETAIL.Landing_Cost,TSPL_SD_SALE_INVOICE_DETAIL.MRP,TSPL_SD_SALE_INVOICE_DETAIL.Markup_Percent as Margin_Percent,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost as Billing_price,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*TSPL_SD_SALE_INVOICE_DETAIL.Qty as Billing_Amount,xx.Rate as Selling_Price,xx.Rate*TSPL_SD_SALE_INVOICE_DETAIL.Qty as Selling_Amount,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleCode,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleDesc,TSPL_VENDOR_MASTER.Add1 as vAdd1,TSPL_VENDOR_MASTER.Add2 as vAdd2,TSPL_VENDOR_MASTER.Add3 as vAdd3,TSPL_VENDOR_MASTER.City_Code_Desc as vCity from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join TSPL_COMPANY_MASTER  on TSPL_SD_SALE_INVOICE_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SD_SALE_INVOICE_DETAIL.PrincipleCode left outer join (select TSPL_STANDARD_RATE_ITEM.StdRateCode,TSPL_STANDARD_RATE_ITEM.FomeDate,TSPL_STANDARD_RATE_ITEM.Valied_Date,TSPL_STANDARD_RATE_ITEM.Cust_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Item_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Rate  from TSPL_STANDARD_RATE_ITEM left outer join TSPL_STANDARD_RATE_ITEM_DETAIL on TSPL_STANDARD_RATE_ITEM_DETAIL.StdRateCode=TSPL_STANDARD_RATE_ITEM.StdRateCode where IsCustomer='Y' and IsValied_Date=1 union all select TSPL_STANDARD_RATE_ITEM.StdRateCode,TSPL_STANDARD_RATE_ITEM.FomeDate,getdate() as Valied_Date,TSPL_STANDARD_RATE_ITEM.Cust_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Item_Code,TSPL_STANDARD_RATE_ITEM_DETAIL.Rate  from TSPL_STANDARD_RATE_ITEM  left outer join TSPL_STANDARD_RATE_ITEM_DETAIL on TSPL_STANDARD_RATE_ITEM_DETAIL.StdRateCode=TSPL_STANDARD_RATE_ITEM.StdRateCode   where IsCustomer='Y' and IsValied_Date=0 ) as xx on xx.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and xx.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code and TSPL_SD_SALE_INVOICE_HEAD.Document_Date >= xx.FomeDate and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<=xx.Valied_Date  )  as zzz    where 1=1  "
                    qry += " and datename(month,document_date)='" + clsCommon.myCstr(cmbMonth.Text) + "' and year(document_date)='" + clsCommon.myCstr(cmbYear.Text) + "' "
                    If chkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                    End If
                    If chkLocationSelect.IsChecked And cbgLoc.CheckedValue.Count > 0 Then
                        qry += " AND Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ") "
                    End If
                    If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                        qry += " AND PrincipleCode in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
                    End If
                    qry += " )   summary group by Customer_Code   "
                End If
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            GV1.DataSource = Nothing
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                RadMessageBox.Show("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                GV1.DataSource = dt
                FormatGrid()
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(clsCommon.myCstr(dt.Rows(0)("Comp_Name")))
            arrHeader.Add(clsCommon.myCstr(dt.Rows(0)("Add1")))
            arrHeader.Add(clsCommon.myCstr(dt.Rows(0)("Add2")))
            arrHeader.Add(clsCommon.myCstr(dt.Rows(0)("Add3")))
            arrHeader.Add("TEL : " + clsCommon.myCstr(dt.Rows(0)("Telephone")))
            arrHeader.Add("FAX : " + clsCommon.myCstr(dt.Rows(0)("Fax")))
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Claim Report " + IIf(chkLandingCost.IsChecked, "Based On Landing Cost", "Based On MRP") + "", GV1, arrHeader, "Claim Report")
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Claim Report " + IIf(chkLandingCost.IsChecked, "Based On Landing Cost", "Based On MRP") + "", GV1, arrHeader, "Claim Report", True)
            ElseIf exporter = EnumExportTo.Print Then
                Dim frmCrystalReportViewer As New frmCrystalReportViewer
                frmCrystalReportViewer.funreport(CrystalReportFolder.ServiceReport, dt, "rptClaimSummary", "Claim Summary")

            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGrid()
        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next
        If radioCustomerWise.IsChecked Then
            GV1.Columns("Document_Code").Width = 200
            GV1.Columns("Document_Code").IsVisible = True
            GV1.Columns("Document_Code").HeaderText = "Document"

            GV1.Columns("Customer").Width = 200
            GV1.Columns("Customer").IsVisible = True
            GV1.Columns("Customer").HeaderText = "Customer"

            GV1.Columns("Item_Desc").Width = 200
            GV1.Columns("Item_Desc").IsVisible = True
            GV1.Columns("Item_Desc").HeaderText = "Item"

            GV1.Columns("Landing_Cost").Width = 100
            GV1.Columns("Landing_Cost").IsVisible = True
            GV1.Columns("Landing_Cost").HeaderText = "Landing Cost"

            GV1.Columns("MRP").Width = 100
            GV1.Columns("MRP").IsVisible = True
            GV1.Columns("MRP").HeaderText = "MRP"

            GV1.Columns("Margin_Percent").Width = 100
            GV1.Columns("Margin_Percent").IsVisible = True
            GV1.Columns("Margin_Percent").HeaderText = "Margin (%)"

            GV1.Columns("Margin_Amount").Width = 100
            GV1.Columns("Margin_Amount").IsVisible = True
            GV1.Columns("Margin_Amount").HeaderText = "Margin Amount (Rs.)"

            GV1.Columns("Qty").Width = 100
            GV1.Columns("Qty").IsVisible = True
            GV1.Columns("Qty").HeaderText = "Qty."

            GV1.Columns("Billing_price").Width = 100
            GV1.Columns("Billing_price").IsVisible = True
            GV1.Columns("Billing_price").HeaderText = "Billing Price (Rs.)"

            GV1.Columns("Billing_Amount").Width = 100
            GV1.Columns("Billing_Amount").IsVisible = True
            GV1.Columns("Billing_Amount").HeaderText = "Billing Amount (Rs.)"

            GV1.Columns("Selling_Price").Width = 100
            GV1.Columns("Selling_Price").IsVisible = True
            GV1.Columns("Selling_Price").HeaderText = "Selling Price (Rs.)"

            GV1.Columns("Selling_Amount").Width = 100
            GV1.Columns("Selling_Amount").IsVisible = True
            GV1.Columns("Selling_Amount").HeaderText = "Selling Amount (Rs.)"


            GV1.Columns("Claim_Amount").Width = 80
            GV1.Columns("Claim_Amount").IsVisible = True
            GV1.Columns("Claim_Amount").HeaderText = "Claim Amount (Rs.)"
            GV1.GroupDescriptors.Add(New GridGroupByExpression("Customer as Customer  format ""{0}: {1}"" group by Customer"))
            GV1.MasterTemplate.ExpandAllGroups()

            GV1.ShowGroupPanel = False
            GV1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Claim_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            Dim item3 As New GridViewSummaryItem("Selling_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Billing_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf radioDocumentWise.IsChecked Then
            GV1.Columns("Document_Code").Width = 200
            GV1.Columns("Document_Code").IsVisible = False
            GV1.Columns("Document_Code").HeaderText = "Document"

            GV1.Columns("Customer").Width = 200
            GV1.Columns("Customer").IsVisible = True
            GV1.Columns("Customer").HeaderText = "Customer"

            GV1.Columns("Item_Desc").Width = 200
            GV1.Columns("Item_Desc").IsVisible = True
            GV1.Columns("Item_Desc").HeaderText = "Item"

            GV1.Columns("Landing_Cost").Width = 100
            GV1.Columns("Landing_Cost").IsVisible = True
            GV1.Columns("Landing_Cost").HeaderText = "Landing Cost"

            GV1.Columns("MRP").Width = 100
            GV1.Columns("MRP").IsVisible = True
            GV1.Columns("MRP").HeaderText = "MRP"

            GV1.Columns("Margin_Percent").Width = 100
            GV1.Columns("Margin_Percent").IsVisible = True
            GV1.Columns("Margin_Percent").HeaderText = "Margin (%)"

            GV1.Columns("Margin_Amount").Width = 100
            GV1.Columns("Margin_Amount").IsVisible = True
            GV1.Columns("Margin_Amount").HeaderText = "Margin Amount (Rs.)"

            GV1.Columns("Qty").Width = 100
            GV1.Columns("Qty").IsVisible = True
            GV1.Columns("Qty").HeaderText = "Qty."

            GV1.Columns("Billing_price").Width = 100
            GV1.Columns("Billing_price").IsVisible = True
            GV1.Columns("Billing_price").HeaderText = "Billing Price (Rs.)"

            GV1.Columns("Billing_Amount").Width = 100
            GV1.Columns("Billing_Amount").IsVisible = True
            GV1.Columns("Billing_Amount").HeaderText = "Billing Amount (Rs.)"

            GV1.Columns("Selling_Price").Width = 100
            GV1.Columns("Selling_Price").IsVisible = True
            GV1.Columns("Selling_Price").HeaderText = "Selling Price (Rs.)"

            GV1.Columns("Selling_Amount").Width = 100
            GV1.Columns("Selling_Amount").IsVisible = True
            GV1.Columns("Selling_Amount").HeaderText = "Selling Amount (Rs.)"


            GV1.Columns("Claim_Amount").Width = 80
            GV1.Columns("Claim_Amount").IsVisible = True
            GV1.Columns("Claim_Amount").HeaderText = "Claim Amount (Rs.)"
            GV1.GroupDescriptors.Add(New GridGroupByExpression("Customer as Customer  format ""{0}: {1}"" group by Customer"))
            GV1.GroupDescriptors.Add(New GridGroupByExpression("Document_Code as Document  format ""{0}: {1}"" group by Document_Code"))
            GV1.MasterTemplate.ExpandAllGroups()

            GV1.ShowGroupPanel = False
            GV1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Claim_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            Dim item3 As New GridViewSummaryItem("Selling_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Billing_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else

            GV1.Columns("PrincipleCode").Width = 200
            GV1.Columns("PrincipleCode").IsVisible = False
            GV1.Columns("PrincipleCode").HeaderText = "Vendor Code"


            GV1.Columns("Principle").Width = 200
            GV1.Columns("Principle").IsVisible = False
            GV1.Columns("Principle").HeaderText = "Vendor"


            GV1.Columns("Customer").Width = 200
            GV1.Columns("Customer").IsVisible = True
            GV1.Columns("Customer").HeaderText = "Customer"


            GV1.Columns("Claim_Amount").Width = 80
            GV1.Columns("Claim_Amount").IsVisible = True
            GV1.Columns("Claim_Amount").HeaderText = "Claim Amount (Rs.)"
            GV1.GroupDescriptors.Add(New GridGroupByExpression("Principle as Vendor  format ""{0}: {1}"" group by Principle"))

            GV1.MasterTemplate.ExpandAllGroups()

            GV1.ShowGroupPanel = False
            GV1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Claim_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If

        GV1.BestFitColumns()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
        Refresh = 2
        Print = 3
    End Enum

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        reset()
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLoc.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkVendorAll__ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Print(EnumExportTo.Refresh)
    End Sub

    Private Sub rmiExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiPDF.Click
        Print(EnumExportTo.PDF)
    End Sub

    Private Sub radioSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles radioSummary.ToggleStateChanged
        If radioSummary.IsChecked Then
            GroupBox2.Visible = False
            GroupBox3.Visible = True
            GroupBox3.Left = GroupBox2.Left
            GroupBox3.Top = GroupBox2.Top
            btnPrint.Visible = True
        Else
            GroupBox2.Visible = True
            GroupBox3.Visible = False
            btnPrint.Visible = False
        End If
    End Sub

    Private Sub GroupBox3_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox3.VisibleChanged
        Try
            If GroupBox3.Visible Then
                cmbMonth.DataSource = clsDBFuncationality.GetDataTable("select distinct DATENAME (month,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ) as MonthName,month(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) from TSPL_SD_SALE_INVOICE_HEAD order by month(TSPL_SD_SALE_INVOICE_HEAD.Document_Date)")
                cmbMonth.DisplayMember = "MonthName"
                cmbMonth.ValueMember = "MonthName"
                cmbYear.DataSource = clsDBFuncationality.GetDataTable("select distinct Year(TSPL_SD_SALE_INVOICE_HEAD.Document_Date ) as DocYear from TSPL_SD_SALE_INVOICE_HEAD")
                cmbYear.DisplayMember = "DocYear"
                cmbYear.ValueMember = "DocYear"
                cmbMonth.SelectedIndex = 0
                cmbYear.SelectedIndex = 0
            Else
                cmbMonth.DataSource = Nothing
                cmbYear.DataSource = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print(EnumExportTo.Print)
    End Sub

    Private Sub GV1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GV1.CellDoubleClick
        If (radioCustomerWise.IsChecked) Or (radioDocumentWise.IsChecked) Then
            If clsCommon.myLen(GV1.CurrentRow.Cells("Document_Code").Value) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, GV1.CurrentRow.Cells("Document_Code").Value)

            End If
        End If
    End Sub
End Class
