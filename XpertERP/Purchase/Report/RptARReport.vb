Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RptARReport
    ''Checkin richa 19/06/2020

    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim variable1 As String = Nothing
    Dim Mainqry As String = String.Empty
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
    End Sub
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " Select xxx.Code,xxx.Name From (" & _
        " Select 'DAR' As Code,'Direct AR Invoice' As Name" & _
        " Union  Select 'SI' As Code,'Sale Invoice' As Name " & _
        " Union  Select 'SR' As Code,'Sale Return' As Name " & _
        " Union  Select 'SC' As Code,'Scrap' As Name " & _
        " Union  Select 'SCR' As Code,'Scrap Return' As Name " & _
        " Union  Select 'VCGL' As Code,'VCGL' As Name " & _
        " Union  Select 'MCC-MSR' As Code,'MCC Material Sale Return' As Name " & _
        " Union  Select 'SE-RN' As Code,'Security Receipt' As Name " & _
        " ) xxx"
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulPur", qry, "Code", "Code", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click

        Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
        qry += " where 2=2 and Seg_No = '7' AND GIT='N'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER where Status ='N' AND OnHold='N'  "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            qry += " and TSPL_CUSTOMER_MASTER.cust_code in (" + objCommonVar.strCurrUserCustomers + ") "
        End If
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CusMulSel", qry, "Code", "Code", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)

    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs)
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        '  txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub
    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_desc as Name from tspl_customer_group_master"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Code", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)

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
        txtTransaction.arrValueMember = Nothing
        TxtMultiCustomerCategory.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        btnSummary.IsChecked = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub loaddata()
        Try
            ''richa BHA/28/09/18-000576 ,BHA/28/09/18-000578,BHA/25/10/18-000641

            Dim dt As New DataTable
            Dim dt1 As New DataTable
            Dim headervalue As String = ""
            Dim headervalue1Rate As String = ""
            Dim variable2Rate As String = Nothing
            Dim SumvariableTaxRate As String = Nothing
            Dim sumvariableTaxamt As String = Nothing
            Dim Wrcls As String = Nothing
            variable1 = Nothing
            Dim strSDTaxRateBlankColumn As String = ""
            Dim strMCCMaterial As String = ""
            Dim strPivotForInnerQueryNoTax As String = Nothing
            Dim strDoublePivotForInnerQueryNoTax As String = Nothing
            Dim strTaxColumns As String = ""
            Dim strSumTaxColumns As String = Nothing
            Dim dtTax As New DataTable
            dtTax = clsDBFuncationality.GetDataTable(" Select Distinct TSPL_TAX_MASTER.Tax_Code,TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_TAX_GROUP_DETAILS LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE   Tax_Group_Type ='S'")
            If TxtTax.arrValueMember IsNot Nothing AndAlso TxtTax.arrValueMember.Count > 0 Then
                Dim dr As DataRow() = dtTax.Select(" Tax_Code in (" + clsCommon.GetMulcallString(TxtTax.arrValueMember) + " ) ")
                dt1 = dr.CopyToDataTable()
            Else
                dt1 = dtTax.Copy()
            End If

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For ii As Integer = 0 To dt1.Rows.Count - 1
                    If ii <> 0 Then
                        variable1 += ","
                        variable2Rate += ","
                    End If
                    strSumTaxColumns += ",sum(final.[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "]) as [" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "]"
                    variable1 += "[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "]"
                    variable2Rate += "[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + " Rate]"
                    headervalue += ",isnull([" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "],0) as [" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "]"
                    headervalue1Rate += ",convert(decimal(18,2),isnull([" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + " Rate],0)) as [" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + " Rate]"
                    'sumvariableTaxamt += ",sum(isnull(ZZ.[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "],0)) as [" & clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() & "]"
                    sumvariableTaxamt += ", case when [Document Type] ='C' then sum(isnull(ZZ.[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "],0))*-1 else sum(isnull(ZZ.[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "],0)) end  as [" & clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() & "]"
                    SumvariableTaxRate += ",max(isnull(ZZ.[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + " Rate],0)) as [" & clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() & " Rate]"
                    strPivotForInnerQueryNoTax += " , Null as [" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "]  "
                    strDoublePivotForInnerQueryNoTax += " , Null  as [" & clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() & " Rate] "
                Next
            End If

            Wrcls += " where 2=2 and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                    Wrcls += " and TSPL_Customer_INVOICE_HEAD.Loc_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")  "
                End If
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_Customer_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If
            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                Wrcls += " and tspl_Customer_master.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If
            Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
            If chkCustCategoryMappInUserMaster = True Then
                Wrcls += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
            End If
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                Wrcls += "  AND (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then 'SR' when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then 'SI' " & _
                " when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then 'SC' when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then 'SCR'  " & _
                " when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then 'MCC-MSR' " & _
                " when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then 'SE-RN' when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then 'DAR'  end) IN (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
            End If
            ''RICHA BHA/01/10/18-000584
            'Sanjay [Trans Type]-Jobwork
            Dim stCSACommonQuery As String = " select (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then 'Sale Return' when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then 'Sale Invoice' when ISNULL(TSPL_JOBWORK_BILLING_HEAD.DOCUMENT_CODE,'')<>'' then 'JobWork' when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then 'Scrap' when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then 'Scrap Return'   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then 'MCC Material Sale Return'  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then 'Security Receipt' " &
                    " when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then 'Direct AR Invoice'  end) as [Trans Type], " &
                    " (case when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>'' then Against_Sale_Return_No when isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>'' then Against_Sale_No  when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap,'')<>'' then AgainstScrap when isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  ,'')<>'' then AgainstScrapReturn   when isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL   ,'')<>'' then Against_VCGL when isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')<>'' then Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')<>'' then Against_Security_Receipt_No  when (isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')='' AND isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return    ,'')='' and isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No     ,'')='' ) then ''  end) as [Sale Invoice No]," &
                    " TSPL_Customer_Invoice_Head.Loc_Code as [Location Code],TSPL_GL_SEGMENT_CODE.Description as [Location Name]" &
                    " ,TSPL_LOCATION_MASTER.GSTNO as [Location GSTIN],TSPL_STATE_MASTER.GST_STATE_Code as [Location State GST], TSPL_STATE_MASTER.STATE_CODE as [Location State Code], TSPL_STATE_MASTER.STATE_NAME as [Location State Name],TSPL_LOCATION_MASTER.City_Code as [Place of Supply Code], TSPL_LOCATION_MASTER.City_Code as [Place of Supply Name]" &
                    " ,TSPL_Customer_Invoice_Detail.Document_No as [Document No] ,convert(varchar,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date]  ,'' as [Narration],'' as [Way Bill No],'' as [GR No],'' as [LR No]" &
                    " ,'' as [Vehicle Code],'' as [Vehicle Name]" &
                    " ,TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_Customer_Invoice_Head.Customer_Name as [Customer Name]" &
                    " ,TSPL_CUSTOMER_MASTER.Add1 as [Customer Address],'' as [Struct Code],TSPL_CUSTOMER_MASTER.Gstno as [GSTIN],TSPL_CUSTOMER_MASTER.State as [Customer State Code],State_Customer.STATE_NAME as [Customer State Name],State_Customer.GST_STATE_Code as [GST State Code],State_Customer.STATE_NAME as [GST State Name],TSPL_CUSTOMER_MASTER.GST_Registered as [GST Register],TSPL_CUSTOMER_MASTER.GST_COmposition as [GST Composition]" &
                    " ,TSPL_CITY_MASTER.City_Name as [Place of Supply],'' as [Transporter Code],'' as [Transporter Name],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code as [Customer Group Code],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Customer Group Name]" &
                    " ,'' as [Parent Customer Code],'' as [Parent Customer Name]" &
                    " ,TSPL_Customer_Invoice_Head.Customer_Control_AC as [Customer Control Account],TSPL_CUSTOMER_MASTER.Tin_No as [Tin No] ,TSPL_Customer_Invoice_Head.Document_Type as [Document Type]   ,TSPL_Customer_Invoice_Detail.GL_Account_Code as [GL Account Code],TSPL_Customer_Invoice_Detail.GL_Account_Desc as [GL Account Desc],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then  TSPL_Customer_Invoice_Detail.Amount *-1 else  TSPL_Customer_Invoice_Detail.Amount end as Amount,TSPL_Customer_Invoice_Detail.Discount_Per as [Discount %],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then  TSPL_Customer_Invoice_Detail.Discount *-1 else  TSPL_Customer_Invoice_Detail.Discount end as  Discount ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then  TSPL_Customer_Invoice_Detail.Amount_less_Discount *-1 else  TSPL_Customer_Invoice_Detail.Amount_less_Discount end as  [Amount Less Discount]" &
                     " ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then   TSPL_Customer_Invoice_Detail.Total_Tax *-1 else   TSPL_Customer_Invoice_Detail.Total_Tax end as [Total Tax],TSPL_Customer_Invoice_Detail.Remarks,TSPL_Customer_Invoice_Detail.Comments,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then  TSPL_Customer_Invoice_Detail.Total_Amount *-1 else  TSPL_Customer_Invoice_Detail.Total_Amount end as  [Total Amount] " &
                     " ,'' as [Import Type],'' as [Import Bill of Entry No],'' as [Import Bill of Entry Date],'' as [Import Bill of Entry Amount]" &
                     " ,'' as [Original Invoice No],'' as [Original Invoice Date],TSPL_Customer_Invoice_Detail.SNo "

            Dim strLeftJoinQry As String = " from  TSPL_Customer_Invoice_Head  left outer join TSPL_Customer_Invoice_Detail on TSPL_Customer_Invoice_Detail.Document_No=TSPL_Customer_Invoice_Head.Document_No " &
                     " left outer join TSPL_JOBWORK_BILLING_HEAD ON TSPL_JOBWORK_BILLING_HEAD.DOCUMENT_CODE=TSPL_Customer_INVOICE_HEAD.AgainstScrap " &
                     " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code " &
                     "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Customer_Invoice_Head.Loc_Code" &
                     " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State" &
                     "   left outer join TSPL_STATE_MASTER as State_Customer on State_Customer.STATE_CODE=TSPL_CUSTOMER_MASTER.State" &
                     " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code" &
                     " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_Customer_Invoice_Head.Loc_Code  " &
                     " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code " &
                     " left outer join tspl_tax_master as tspl_tax_master1  on tspl_tax_master1.tax_code=TSPL_Customer_Invoice_Detail.tax1" &
                     " left outer join tspl_tax_master as tspl_tax_master2  on tspl_tax_master2.tax_code=TSPL_Customer_Invoice_Detail.tax2" &
                     " left outer join tspl_tax_master as tspl_tax_master3  on tspl_tax_master3.tax_code=TSPL_Customer_Invoice_Detail.tax3" &
                     " left outer join tspl_tax_master as tspl_tax_master4  on tspl_tax_master4.tax_code=TSPL_Customer_Invoice_Detail.tax4" &
                     " left outer join tspl_tax_master as tspl_tax_master5  on tspl_tax_master5.tax_code=TSPL_Customer_Invoice_Detail.tax5" &
                     " left outer join tspl_tax_master as tspl_tax_master6  on tspl_tax_master6.tax_code=TSPL_Customer_Invoice_Detail.tax6" &
                     " left outer join tspl_tax_master as tspl_tax_master7  on tspl_tax_master7.tax_code=TSPL_Customer_Invoice_Detail.tax7" &
                     " left outer join tspl_tax_master as tspl_tax_master8  on tspl_tax_master8.tax_code=TSPL_Customer_Invoice_Detail.tax8" &
                     " left outer join tspl_tax_master as tspl_tax_master9 on tspl_tax_master9.tax_code=TSPL_Customer_Invoice_Detail.tax9" &
                     " left outer join tspl_tax_master as tspl_tax_master10  on tspl_tax_master10.tax_code=TSPL_Customer_Invoice_Detail.tax10"
            strTaxColumns = strPivotForInnerQueryNoTax & strDoublePivotForInnerQueryNoTax

            strMCCMaterial = " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & " and (coalesce(TSPL_Customer_Invoice_Detail.tax1,'')='' and  " & _
              " coalesce(TSPL_Customer_Invoice_Detail.tax2,'')=''  and coalesce(TSPL_Customer_Invoice_Detail.tax3,'')='' and coalesce(TSPL_Customer_Invoice_Detail.tax4,'')='' and  " & _
              " coalesce(TSPL_Customer_Invoice_Detail.tax5,'')='' and coalesce(TSPL_Customer_Invoice_Detail.tax6,'')='' and  coalesce(TSPL_Customer_Invoice_Detail.tax7,'')='' and " & _
              " coalesce(TSPL_Customer_Invoice_Detail.tax8,'')='' and  coalesce(TSPL_Customer_Invoice_Detail.tax9,'')='' and coalesce(TSPL_Customer_Invoice_Detail.tax10,'')='') ) T"
            strMCCMaterial += "   union all"
            strTaxColumns = " , tspl_tax_master1.Tax_Code_Desc as TAX1 ,TSPL_Customer_Invoice_Detail.TAX1_Amt as TAX1_Amt, TSPL_Customer_Invoice_Detail.TAX1_Rate,tspl_tax_master1.Tax_Code_Desc+' Rate' as Tax1Rate"

            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + variable1 + "))t pivot (min(tax1_rate) for Tax1Rate in (" + variable2Rate + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master2.Tax_Code_Desc as TAX2 ,TSPL_Customer_Invoice_Detail.TAX2_Amt as TAX2_Amt, TSPL_Customer_Invoice_Detail.TAX2_Rate,tspl_tax_master2.Tax_Code_Desc+' Rate' as Tax2Rate"

            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + variable1 + "))t pivot (min(tax2_rate) for Tax2Rate in (" + variable2Rate + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master3.Tax_Code_Desc as TAX3 ,TSPL_Customer_Invoice_Detail.TAX3_Amt as TAX3_Amt, TSPL_Customer_Invoice_Detail.TAX3_Rate,tspl_tax_master3.Tax_Code_Desc+' Rate' as Tax3Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + variable1 + "))t pivot (min(tax3_rate) for Tax3Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master4.Tax_Code_Desc as TAX4 ,TSPL_Customer_Invoice_Detail.TAX4_Amt as TAX4_Amt, TSPL_Customer_Invoice_Detail.TAX4_Rate,tspl_tax_master4.Tax_Code_Desc+' Rate' as Tax4Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + variable1 + "))t pivot (min(tax4_rate) for Tax4Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master5.Tax_Code_Desc as TAX5 ,TSPL_Customer_Invoice_Detail.TAX5_Amt as TAX5_Amt, TSPL_Customer_Invoice_Detail.TAX5_Rate,tspl_tax_master5.Tax_Code_Desc+' Rate' as Tax5Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + variable1 + "))t pivot (min(tax5_rate) for Tax5Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master6.Tax_Code_Desc as TAX6 ,TSPL_Customer_Invoice_Detail.TAX6_Amt as TAX6_Amt, TSPL_Customer_Invoice_Detail.TAX6_Rate,tspl_tax_master6.Tax_Code_Desc+' Rate' as Tax6Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax6<>'' )s pivot (sum(tax6_amt) for tax6 in (" + variable1 + "))t pivot (min(tax6_rate) for Tax6Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master7.Tax_Code_Desc as TAX7 ,TSPL_Customer_Invoice_Detail.TAX7_Amt as TAX7_Amt, TSPL_Customer_Invoice_Detail.TAX7_Rate,tspl_tax_master7.Tax_Code_Desc+' Rate' as Tax7Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + variable1 + "))t pivot (min(tax7_rate) for Tax7Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master8.Tax_Code_Desc as TAX8 ,TSPL_Customer_Invoice_Detail.TAX8_Amt as TAX8_Amt, TSPL_Customer_Invoice_Detail.TAX8_Rate,tspl_tax_master8.Tax_Code_Desc+' Rate' as Tax8Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + variable1 + "))t pivot (min(tax8_rate) for Tax8Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master9.Tax_Code_Desc as TAX9 ,TSPL_Customer_Invoice_Detail.TAX9_Amt as TAX9_Amt, TSPL_Customer_Invoice_Detail.TAX9_Rate,tspl_tax_master9.Tax_Code_Desc+' Rate' as Tax9Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax9<>'' )s pivot (sum(tax9_amt) for tax9 in (" + variable1 + "))t pivot (min(tax9_rate) for Tax9Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master10.Tax_Code_Desc as TAX10 ,TSPL_Customer_Invoice_Detail.TAX10_Amt as TAX10_Amt, TSPL_Customer_Invoice_Detail.TAX10_Rate,tspl_tax_master10.Tax_Code_Desc+' Rate' as Tax10Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_Customer_Invoice_Detail.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + variable1 + "))t pivot (min(tax10_rate) for Tax10Rate in (" + variable2Rate + "))t"

            Mainqry = " SELECT [Trans Type],[Sale Invoice No],[Location Code],MAX([Location Name]) AS [Location Name],MAX([Location GSTIN]) AS [Location GSTIN]," & _
                " MAX( [Location State GST])AS  [Location State GST],MAX([Location State Code]) AS [Location State Code],MAX([Location State Name]) AS [Location State Name]," & _
                " MAX([Place of Supply Code]) AS [Place of Supply Code], MAX([Place of Supply Name]) AS [Place of Supply Name],[Document No],MAX([Document Date]) as [Document Date]," & _
                " MAX([Narration]) AS [Narration],MAX([Way Bill No]) AS [Way Bill No],MAX([GR No]) AS [GR No],MAX([LR No]) AS [LR No],MAX([Vehicle Code]) AS [Vehicle Code]," & _
                " MAX([Vehicle Name]) AS [Vehicle Name],[Customer Code],MAX([Customer Name]) AS [Customer Name],max([Customer Address]) as [Customer Address],max([Struct Code]) as [Struct Code]," & _
                " max([GSTIN]) as [GSTIN],max([Customer State Code]) as [Customer State Code],max([Customer State Name]) as [Customer State Name],max([GST State Code]) as [GST State Code]," & _
                " max([GST State Name]) as [GST State Name],max([GST Register]) as [GST Register],max([GST Composition]) as [GST Composition],max([Place of Supply]) as [Place of Supply]," & _
                " max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],[Customer Group Code],max([Customer Group Name]) as [Customer Group Name]," & _
                " max([Parent Customer Code]) as [Parent Customer Code],max([Parent Customer Name]) as [Parent Customer Name],[Customer Control Account],max([Tin No]) as [Tin No]," & _
                " [Document Type],[GL Account Code],max([GL Account Desc]) as [GL Account Desc],Amount,[Discount %],Discount,[Amount Less Discount],[Total Tax]," & _
                " max(Remarks) as Remarks,max(Comments) as Comments,max([Import Type]) as [Import Type],max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date]," & _
                " max([Import Bill of Entry Amount]) as [Import Bill of Entry Amount],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date]" & sumvariableTaxamt & "  " & SumvariableTaxRate & " ,[Total Amount]" & _
                " FROM (" & _
             " " & strMCCMaterial & "" & _
                " ) AS zz GROUP BY [Trans Type],[Sale Invoice No],[Location Code],[Document No],[Customer Code],[Customer Group Code],[Customer Control Account],[Document Type],[GL Account Code]," & _
            "Amount,[Discount %],Discount,[Amount Less Discount],[Total Tax],[Total Amount],SNo "


            If clsCommon.CompairString(btnSummary.IsChecked, "True") = CompairStringResult.Equal Then
                ' qry = "select max([Customer Code]) as [Customer Code], max([Customer Name]) as [Customer Name], [Document No] ,max([Trans Type]) as [Trans Type],max([Sale Invoice No]) as [Sale Invoice No], " & _
                '         " max([Document Date]) as [Document Date], max([Location Code]) as [Location Code],max(TSPL_GL_SEGMENT_CODE.Description) as [Location Name] ," & _
                '          " max([Customer Control Account]) as  [Customer Control Account] , max([Document Type]) as [Document Type] ," & _
                '             "   case when max ([Document Type]) ='C' then sum([Total Amount])   *-1 else sum([Total Amount])  end as [Total Amount]   from (" & qry & ") as final left outer join tspl_Customer_master on tspl_Customer_master.Cust_Code=final.[Customer Code] " & _
                '" left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =final.[Location Code]  group by  [Document No],[Document Date] order by convert(date,[Document Date],103) asc,[Document No] "

                '  Mainqry = "select max([Customer Code]) as [Customer Code], max([Customer Name]) as [Customer Name], [Document No] ,max([Trans Type]) as [Trans Type],max([Sale Invoice No]) as [Sale Invoice No], " & _
                '         " max([Document Date]) as [Document Date], max([Location Code]) as [Location Code],max(TSPL_GL_SEGMENT_CODE.Description) as [Location Name] ," & _
                '          " max([Customer Control Account]) as  [Customer Control Account] , max([Document Type]) as [Document Type] ," & _
                '             "  sum([Total Amount]) as [Total Amount]   from (" & Mainqry & ") as final left outer join tspl_Customer_master on tspl_Customer_master.Cust_Code=final.[Customer Code] " & _
                '" left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =final.[Location Code]  group by  [Document No],[Document Date] order by convert(date,[Document Date],103) asc,[Document No] "

                Mainqry = "select max([Customer Code]) as [Customer Code], max([Customer Name]) as [Customer Name], [Document No] ,max([Trans Type]) as [Trans Type],max([Sale Invoice No]) as [Sale Invoice No], " &
                       " max([Document Date]) as [Document Date], max([Location Code]) as [Location Code],max(TSPL_GL_SEGMENT_CODE.Description) as [Location Name] ," &
                        " max([Customer Control Account]) as  [Customer Control Account] , max([Document Type]) as [Document Type] ," &
                        "sum([Amount Less Discount]) as [Invoice Amount before Discount],max(TSPL_Customer_Invoice_Head.Discount_Amount) as [Discount Amount],sum([Amount Less Discount])-max(TSPL_Customer_Invoice_Head.Discount_Amount) as [Net Amount after Discount],sum([Total Tax]) as [Total Tax]" + strSumTaxColumns + "" &
                        "  ,sum([Total Amount]) as [Total Amount],max(case when TSPL_Customer_Invoice_Head.Document_Type ='C' then  TSPL_Customer_INVOICE_HEAD.Total_Add_Charge *-1 else  TSPL_Customer_INVOICE_HEAD.Total_Add_Charge end) as [Total Additional Charges],max(case when TSPL_Customer_Invoice_Head.Document_Type ='C' then  TSPL_Customer_INVOICE_HEAD.RoundOffAmount *-1 else  TSPL_Customer_INVOICE_HEAD.RoundOffAmount end) as [Round Off Amount],max(case when TSPL_Customer_Invoice_Head.Document_Type ='C' then  TSPL_Customer_Invoice_Head.DOCUMENT_TOTAL *-1 else  TSPL_Customer_Invoice_Head.DOCUMENT_TOTAL end) as [Document Amount]   from (" & Mainqry & ") as final left outer join tspl_Customer_master on tspl_Customer_master.Cust_Code=final.[Customer Code] " &
              " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =final.[Location Code] left outer join TSPL_Customer_Invoice_Head  on TSPL_Customer_Invoice_Head.Document_No=final.[Document No] group by  [Document No],[Document Date] order by convert(date,[Document Date],103) asc,[Document No] "
            Else
                Mainqry = " select * from (" & Mainqry & ") as pp order by convert(date,[Document Date],103) asc,[Document No]"
            End If

            dt = clsDBFuncationality.GetDataTable(Mainqry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
                FormatGrid()
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub FormatGrid()
        'Added by preeti agsinst ticket no[]

        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        For Each col As GridViewColumn In gv1.Columns
            If clsCommon.CompairString(col.Name, "Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Discount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Amount Less Discount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total Tax") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Landed Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Abatement_Amt") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Reverse_Charge_Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Payment_Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Taxable Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Document_Total") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Round Off Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total Additional Charges") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Document Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Discount Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Net Amount") = CompairStringResult.Equal Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            ElseIf variable1.Contains(col.Name) = True Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                gv1.Columns(col.Name).ReadOnly = True
            End If
        Next



        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(btnSummary.IsChecked, "S", "D")
        TemplateGridview = gv1
        loaddata()
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        'Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
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
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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


    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If gv1.Rows.Count > 0 Then
                If gv1.CurrentColumn Is gv1.Columns("Document No") Then
                    Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("Document No").Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, DocNo)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptARReport & "'"))


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If
            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                arrHeader.Add(("Cust Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember) + " "))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember) + " "))
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrValueMember) + " "))
            End If
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                arrHeader.Add(("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember) + " "))
            End If
            If TxtTax.arrValueMember IsNot Nothing AndAlso TxtTax.arrValueMember.Count > 0 Then
                arrHeader.Add(("Tax : " + clsCommon.GetMulcallStringWithComma(TxtTax.arrDispalyMember) + " "))
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
            If gv1.Rows.Count > 0 Then
                ''richa done on AR report 19 June 2020
                transportSql.applyExportTemplate(gv1, Form_ID, IIf(btnSummary.IsChecked = True, "Summary", "Detail"))
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                ''richa ERO/17/03/20-001204
                ' transportSql.BulkExport("AR Report", Mainqry, "order by convert(date,[Document Date],103) asc,[Document No]", "xls")
                Exit Sub
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptARReport & "'"))


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If
            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                arrHeader.Add(("Cust Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember) + " "))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember) + " "))
            End If
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                arrHeader.Add(("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember) + " "))
            End If
            If TxtTax.arrValueMember IsNot Nothing AndAlso TxtTax.arrValueMember.Count > 0 Then
                arrHeader.Add(("Tax : " + clsCommon.GetMulcallStringWithComma(TxtTax.arrDispalyMember) + " "))
            End If
            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("AR Report" + IIf(btnSummary.IsChecked, "Summary", "Detail"), gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data found")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub TxtTax__My_Click(sender As Object, e As EventArgs) Handles TxtTax._My_Click
        Dim qry As String = " Select Distinct TSPL_TAX_MASTER.Tax_Code as Code,TSPL_TAX_MASTER.Tax_Code_Desc as Name from TSPL_TAX_GROUP_DETAILS LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Group_Type ='S'"

        TxtTax.arrValueMember = clsCommon.ShowMultipleSelectForm("T@MulSel", qry, "Code", "Code", TxtTax.arrValueMember, TxtTax.arrDispalyMember)
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCatMSel3", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub
End Class
