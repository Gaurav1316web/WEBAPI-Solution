Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'' RICHA AGARWAL KDI/02/05/18-000289,KDI/28/05/18-000333
Public Class rptAPReport
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim variable1 As String = Nothing
    Dim StrPermission As String
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        '  txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " Select xxx.Code,xxx.Name From (" &
        " Select 'DAP' As Code,'Direct AP Invoice' As Name" &
        " Union  Select 'PI' As Code,'Purchase Invoice' As Name " &
        " Union  Select 'BMPI' As Code,'Bulk Milk Purchase Invoice' As Name " &
        " Union  Select 'MPI' As Code,'Milk Purchase Invoice' As Name " &
        " Union  Select 'VCGL' As Code,'VCGL' As Name " &
        " Union  Select 'PR' As Code,'Purchase Return' As Name " &
        " Union  Select 'VSR' As Code,'Vendor Service Charge' As Name " &
        " Union  Select 'BMPR' As Code,'Bulk Milk Purchase Return' As Name " &
        " ) xxx"
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulPur", qry, "Code", "Code", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click

        Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
        qry += " where 2=2 and Seg_No = '7' AND GIT='N'"
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        'End If
        qry += " and Segment_code in (" & StrPermission & ")"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Code", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)

    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub
    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_desc as Name from TSPL_VENDOR_group"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGroupMulSel", qry, "Code", "Code", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)

    End Sub

    Private Sub rptAPReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationSegment()
        Reset()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        ' txtUOM.Value = ""
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        btnSummary.IsChecked = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If btnSummary.IsChecked = True Then
            VarID += "_S"
        Else
            btnDetail.IsChecked = True
            VarID += "_D"
        End If
        gv1.VarID = VarID
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            GetReportGridID()
            PageSetupReport_ID = MyBase.Form_ID + IIf(btnSummary.IsChecked, "S", "D")
            TemplateGridview = gv1
            '=================Update by preeti Gupta Against Ticket No[KDI/07/06/18-000349,BHA/18/12/18-000758]
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            Dim headervalue As String = ""
            Dim headervalue1Rate As String = ""
            Dim variable2Rate As String = Nothing
            'Dim variable1 As String = Nothing

            Dim SumvariableTaxRate As String = Nothing
            Dim sumvariableTaxamt As String = Nothing

            Dim Wrcls As String = Nothing
            variable1 = Nothing
            '  variable1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+Tax_Code_Desc+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DETAIL.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y'  ) XXX For XML Path('')),1,1,'')"))
            ' variable2Rate = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+Tax_Code_Desc+' Rate'+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DETAIL.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' ) XXX For XML Path('')),1,1,'')"))
            ' Dim headervalue As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',isnull(['+Tax_Code_Desc+'],0) as ['+Tax_Code_Desc+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DETAIL.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y'  ) XXX For XML Path('')))"))
            '  Dim headervalue1Rate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',convert(decimal(18,2),isnull(['+Tax_Code_Desc+' Rate'+'],0)) as ['+Tax_Code_Desc+' Rate'+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' ) XXX For XML Path('')))"))

            '  dt1 = clsDBFuncationality.GetDataTable(" Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DETAIL.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y'")
            Dim strSDTaxRateBlankColumn As String = ""
            Dim strMCCMaterial As String = ""
            Dim strPivotForInnerQueryNoTax As String = Nothing
            Dim strDoublePivotForInnerQueryNoTax As String = Nothing
            Dim strTaxColumns As String = ""
            Dim dtTax As New DataTable

            dtTax = clsDBFuncationality.GetDataTable(" Select Distinct TSPL_TAX_MASTER.Tax_Code,TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_TAX_GROUP_DETAILS LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' and Tax_Group_Type ='P'")
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
                        '  headervalue += ","
                        ' headervalue1Rate += ","
                    End If
                    variable1 += "[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "]"
                    variable2Rate += "[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + " Rate]"
                    headervalue += ",isnull([" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "],0) as [" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "]"
                    headervalue1Rate += ",convert(decimal(18,2),isnull([" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + " Rate],0)) as [" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + " Rate]"
                    sumvariableTaxamt += ",sum(isnull(ZZ.[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "],0)) as [" & clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() & "]"
                    SumvariableTaxRate += ",max(isnull(ZZ.[" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + " Rate],0)) as [" & clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() & " Rate]"
                    'strPivotForOuterQuery = strPivotForOuterQuery & "sum(isnull(final.[" & clsCommon.myCstr(dr.Item("TAX1")) & "],0)) as [" & clsCommon.myCstr(dr.Item("TAX1")) & "]"

                    strPivotForInnerQueryNoTax += " , Null as [" + clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() + "]  "
                    strDoublePivotForInnerQueryNoTax += " , Null  as [" & clsCommon.myCstr(dt1.Rows(ii)("Tax_Code_Desc")).Trim() & " Rate] "
                Next

            End If
            ''richa BHA/17/09/18-000552
            Wrcls = " WHERE 2=2 and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            Else
                Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + StrPermission + ")"
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If
            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                Wrcls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
            End If
            '' KDI/22/05/18-000328 ADD VENDOR SERVICE CHARGE FILTER
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                Wrcls += "  AND (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'PI'  when (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'') then 'BMPR' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'VSR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'PR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'BMPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'MPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'DAP'  end) IN (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
            End If

            If clsCommon.CompairString(btnSummary.IsChecked, "True") <> CompairStringResult.Equal Then

                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    Wrcls += " and TSPL_VENDOR_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
            End If

            ''RICHA BHA/01/10/18-000584
            'Else
            Dim stCSACommonQuery As String = " select CASE WHEN isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Percentage,0)<>0 THEN TSPL_VENDOR_INVOICE_HEAD.TDS_Percentage ELSE TSPL_VENDOR_INVOICE_DETAIL.TDS_Per END AS TDS_Percentage,TSPL_VENDOR_INVOICE_HEAD.TDS_Calculated_Amount,(case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'Vendor Service Charge' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'Purchase Return' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'Bulk Milk Purchase Invoice' when (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'') then 'Bulk Milk Purchase Return' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'Milk Purchase Invoice' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL'  when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'Direct AP Invoice'  end) as [Trans Type]," &
" TSPL_VENDOR_INVOICE_HEAD.Loc_Code as [Location Code] ,TSPL_GL_SEGMENT_CODE.Description as [Location Name] ,TSPL_STATE_MASTER.GST_STATE_Code as [Location State GST]," &
 " TSPL_STATE_MASTER.STATE_CODE as [Location State Code], TSPL_STATE_MASTER.STATE_NAME as [Location State Name],TSPL_LOCATION_MASTER.City_Code as [Place of Supply Name]," &
 " TSPL_LOCATION_MASTER.GSTNO as [Location GSTIN] ,TSPL_VENDOR_INVOICE_HEAD.Document_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as [Document Date]   ," &
 " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0 then Against_POInvoice_No else case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo,''))>0 and  isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' then TSPL_VENDOR_INVOICE_HEAD.RefDocNo else case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.against_purchaseReturn_No,''))>0 then against_purchaseReturn_No  else case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.against_acquisition,''))>0 then against_acquisition  else case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,''))>0 then Against_MillkPurchaseInvoice_No  else case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No,''))>0 then Against_BulkMillkPurchaseInvoice_No  else case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No,''))>0 then Against_VSPItemIssue_No else case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,''))>0 then Against_Asset_Work else case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,''))>0 then Against_VCGL end end  end  end end end end end end as [Purchase Invoice no]," &
" TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Invoice No],convert(varchar,TSPL_VENDOR_INVOICE_HEAD.vendor_invoice_date,103) as [Vendor Invoice Date]   ," &
            " '' as [Way Bill No],'' as [GR No],'' as [LR No]  ,'' as [Vehicle Code],'' as [Vehicle Name] ,TSPL_VENDOR_INVOICE_HEAD.vendor_code as [Vendor Code]," &
" TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as [Vendor Name] ,TSPL_VENDOR_MASTER.Add1 as [Vendor Address],Vendor_State.STATE_NAME as [Vendor State Name] ," &
" TSPL_VENDOR_MASTER.GSTFinalNo as [GSTIN],Vendor_State.GST_STATE_Code as [GST State Code],TSPL_VENDOR_MASTER.GST_Composition_scheme as [GST Composition] ," &
" TSPL_VENDOR_MASTER.GSTRegistered,TSPL_VENDOR_MASTER.incentive as [Incentive],TSPL_VENDOR_MASTER.Tin_No as [Tin No],'' as EMP ,'' as [Transporter Code]," &
           " '' as [Transporter Name],TSPL_VENDOR_GROUP.Ven_Group_Code as [Vendor Group Code],TSPL_VENDOR_GROUP.Description as [Vendor Group Name] ," &
           " '' as [Parent Vendor Code],'' as [Parent Vendor Name] ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as [Vendor Control Account] ," &
" TSPL_VENDOR_INVOICE_HEAD.Document_Type  ,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code as [GL Account Code],TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Desc as [GL Account Desc]," &
" case when TSPL_VENDOR_INVOICE_HEAD .Document_Type ='D' then (-1*(TSPL_VENDOR_INVOICE_DETAIL.Amount)) else (TSPL_VENDOR_INVOICE_DETAIL.Amount) end AS Amount," &
" TSPL_VENDOR_INVOICE_DETAIL.Discount_Per as [Discount %],TSPL_VENDOR_INVOICE_DETAIL.Discount , case when TSPL_VENDOR_INVOICE_HEAD .Document_Type ='D' then (-1*(TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount)) else (TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount) end AS [Amount Less Discount], " &
" case when TSPL_VENDOR_INVOICE_HEAD .Document_Type ='D' then (-1*(TSPL_VENDOR_INVOICE_DETAIL.Total_Tax)) else (TSPL_VENDOR_INVOICE_DETAIL.Total_Tax) end AS  [Total Tax]," &
" case when TSPL_VENDOR_INVOICE_HEAD .Document_Type ='D' then (-1*(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount)) else (TSPL_VENDOR_INVOICE_DETAIL.Total_Amount) end AS [Total Amount]," &
" TSPL_VENDOR_INVOICE_DETAIL.Remarks,TSPL_VENDOR_INVOICE_DETAIL.Comments,TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode ,TSPL_VENDOR_INVOICE_DETAIL.AddChargeDesc," &
" TSPL_VENDOR_INVOICE_DETAIL.is_Unclaimed_Tax,case when TSPL_VENDOR_INVOICE_HEAD .Document_Type ='D' then (-1*(TSPL_VENDOR_INVOICE_DETAIL.Landed_Amount)) else (TSPL_VENDOR_INVOICE_DETAIL.Landed_Amount) end AS [Landed Amount]," &
" TSPL_VENDOR_INVOICE_DETAIL.Deduction_Code as [Deduction Code],TSPL_VENDOR_INVOICE_DETAIL.item_code as [Item Code],TSPL_VENDOR_INVOICE_DETAIL.item_desc as [Item Desc]," &
" TSPL_VENDOR_INVOICE_DETAIL.charge_cat_code as [Charge Category Code],TSPL_VENDOR_INVOICE_DETAIL.charge_cat_desc as [Charge Category Desc]," &
" TSPL_VENDOR_INVOICE_DETAIL.charge_cat_charges,TSPL_VENDOR_INVOICE_DETAIL.Item_Rate as [Item Rate],TSPL_VENDOR_INVOICE_DETAIL.Abatement_Per," &
" TSPL_VENDOR_INVOICE_DETAIL.Abatement_Amt,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc," &
" TSPL_VENDOR_INVOICE_DETAIL.Reverse_Charge_Per,TSPL_VENDOR_INVOICE_DETAIL.Reverse_Charge_Amount,TSPL_VENDOR_INVOICE_DETAIL.AgainstPayment_No,  " &
" TSPL_VENDOR_INVOICE_DETAIL.Payment_Amount,TSPL_VENDOR_INVOICE_DETAIL.TDS_Per,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code as [Hirerachy Code]," &
" TSPL_VENDOR_INVOICE_DETAIL.Cost_Centre_Code as [Cost Centre Code],  TSPL_VENDOR_INVOICE_DETAIL.Against_Milk_SRN_Commission_No," &
" TSPL_VENDOR_INVOICE_DETAIL.Asset_Code as [Asset Code],TSPL_VENDOR_INVOICE_DETAIL.Item_Type as [Item Type],TSPL_VENDOR_INVOICE_DETAIL.SAC_Code as [SAC Code]," &
" TSPL_VENDOR_INVOICE_DETAIL.Taxable_Amount as [Taxable Amount] ,'' as [Import Type],'' as [Import Bill of Entry No],'' as [Import Bill of Entry Date]," &
           " '' as [Import Bill of Entry Amount] ,'' as [Original Invoice No],'' as [Original Invoice Date],case when ITC_Elibible = 0 then 'No' else 'Yes' end as [ITC Eligible], case when ITC_TYPE =0 and ITC_Elibible =1 then 'No' when ITC_TYPE =1 and ITC_Elibible =1 then 'No' else '' end as [ITC Status], ITC_TYPE_Category as [ITC Details],TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No "


            Dim strLeftJoinQry As String = "  from  TSPL_VENDOR_INVOICE_HEAD " &
" left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No " &
" left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code  " &
" left join TSPL_STATE_MASTER as Vendor_State on Vendor_State.STATE_CODE = tspl_vendor_master.State_Code " &
" left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code  " &
" left join  ( select TSPL_LOCATION_MASTER.Loc_Segment_Code ,max(TSPL_LOCATION_MASTER.GSTNO ) as GSTNO,max(State) as State,max(add1) as add1 ,max(City_Code) as City_Code from TSPL_LOCATION_MASTER group by TSPL_LOCATION_MASTER.Loc_Segment_Code ) as TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code " &
" left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_GL_SEGMENT_CODE.State_code  " &
" left outer join TSPL_STATE_MASTER as State_Customer on State_Customer.STATE_NAME=tspl_vendor_master.State  " &
" left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_Group_Code " &
     " left outer join tspl_tax_master as tspl_tax_master1  on tspl_tax_master1.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax1" &
        " left outer join tspl_tax_master as tspl_tax_master2  on tspl_tax_master2.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax2" &
        " left outer join tspl_tax_master as tspl_tax_master3  on tspl_tax_master3.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax3" &
        " left outer join tspl_tax_master as tspl_tax_master4  on tspl_tax_master4.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax4" &
        " left outer join tspl_tax_master as tspl_tax_master5  on tspl_tax_master5.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax5" &
        " left outer join tspl_tax_master as tspl_tax_master6  on tspl_tax_master6.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax6" &
        " left outer join tspl_tax_master as tspl_tax_master7  on tspl_tax_master7.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax7" &
        " left outer join tspl_tax_master as tspl_tax_master8  on tspl_tax_master8.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax8" &
        " left outer join tspl_tax_master as tspl_tax_master9 on tspl_tax_master9.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax9" &
        " left outer join tspl_tax_master as tspl_tax_master10  on tspl_tax_master10.tax_code=TSPL_VENDOR_INVOICE_DETAIL.tax10"

            Dim WhrCls As String = "" & Wrcls & ""

            strTaxColumns = strPivotForInnerQueryNoTax & strDoublePivotForInnerQueryNoTax


            strMCCMaterial = " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & " and (coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax1,'')='' and  " &
           " coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax2,'')=''  and coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax3,'')='' and coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax4,'')='' and  " &
           " coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax5,'')='' and coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax6,'')='' and  coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax7,'')='' and " &
           " coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax8,'')='' and  coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax9,'')='' and coalesce(TSPL_VENDOR_INVOICE_DETAIL.tax10,'')='') ) T"
            strMCCMaterial += "   union all"
            strTaxColumns = " , tspl_tax_master1.Tax_Code_Desc as TAX1,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX1_Amt,0) as TAX1_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX1_Rate,tspl_tax_master1.Tax_Code_Desc+' Rate' as Tax1Rate"

            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + variable1 + "))t pivot (min(tax1_rate) for Tax1Rate in (" + variable2Rate + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master2.Tax_Code_Desc as TAX2 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX2_Amt,0) as TAX2_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX2_Rate,tspl_tax_master2.Tax_Code_Desc+' Rate' as Tax2Rate"

            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + variable1 + "))t pivot (min(tax2_rate) for Tax2Rate in (" + variable2Rate + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master3.Tax_Code_Desc as TAX3 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX3_Amt,0) as TAX3_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX3_Rate,tspl_tax_master3.Tax_Code_Desc+' Rate' as Tax3Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + variable1 + "))t pivot (min(tax3_rate) for Tax3Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master4.Tax_Code_Desc as TAX4 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX4_Amt,0) as TAX4_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX4_Rate,tspl_tax_master4.Tax_Code_Desc+' Rate' as Tax4Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + variable1 + "))t pivot (min(tax4_rate) for Tax4Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master5.Tax_Code_Desc as TAX5 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX5_Amt,0) as TAX5_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX5_Rate,tspl_tax_master5.Tax_Code_Desc+' Rate' as Tax5Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + variable1 + "))t pivot (min(tax5_rate) for Tax5Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master6.Tax_Code_Desc as TAX6 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX6_Amt,0) as TAX6_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX6_Rate,tspl_tax_master6.Tax_Code_Desc+' Rate' as Tax6Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax6<>'' )s pivot (sum(tax6_amt) for tax6 in (" + variable1 + "))t pivot (min(tax6_rate) for Tax6Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master7.Tax_Code_Desc as TAX7 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX7_Amt,0) as TAX7_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX7_Rate,tspl_tax_master7.Tax_Code_Desc+' Rate' as Tax7Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + variable1 + "))t pivot (min(tax7_rate) for Tax7Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master8.Tax_Code_Desc as TAX8 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX8_Amt,0) as TAX8_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX8_Rate,tspl_tax_master8.Tax_Code_Desc+' Rate' as Tax8Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + variable1 + "))t pivot (min(tax8_rate) for Tax8Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master9.Tax_Code_Desc as TAX9 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX9_Amt,0) as TAX9_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX9_Rate,tspl_tax_master9.Tax_Code_Desc+' Rate' as Tax9Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax9<>'' )s pivot (sum(tax9_amt) for tax9 in (" + variable1 + "))t pivot (min(tax9_rate) for Tax9Rate in (" + variable2Rate + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " ,tspl_tax_master10.Tax_Code_Desc as TAX10 ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then -1 else 1 end * isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX10_Amt,0) as TAX10_Amt, TSPL_VENDOR_INVOICE_DETAIL.TAX10_Rate,tspl_tax_master10.Tax_Code_Desc+' Rate' as Tax10Rate"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strLeftJoinQry & Wrcls & "  and TSPL_VENDOR_INVOICE_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + variable1 + "))t pivot (min(tax10_rate) for Tax10Rate in (" + variable2Rate + "))t"

            ' change by priti for vendor name from vendor master KDI/05/07/18-000390

            qry = " select [Trans Type] as [Trans Type] ,[Location Code] as [Location Code] ,max([Location Name]) as [Location Name] ,[Vendor Code],max(tspl_vendor_master.vendor_name) as [Vendor Name]," &
                " max([Location State GST]) as [Location State GST] ,max([Location State Code]) as [Location State Code] ," &
                " max([Location State Name]) as [finlLocation State Name] ,max([Place of Supply Name]) as [Place of Supply Name] ," &
                " max([Location GSTIN]) as [Location GSTIN] ,[Purchase Invoice no]  as [Purchase Invoice no] ,Document_No  as Document_No," &
                " max([Document Date]) as [Document Date],[Vendor Invoice No],max([Vendor Invoice Date]) as [Vendor Invoice Date],max([Way Bill No]) as [Way Bill No]," &
                " [GR No],[LR No],[Vehicle Code] ,max([Vehicle Name]) as [Vehicle Name],max([Vendor Address]) as [Vendor Address]," &
                " max([Vendor State Name]) as [Vendor State Name], GSTIN,[GST State Code],zz.GSTRegistered,zz.Incentive,[Tin No],EMP ," &
                " [Transporter Code] ,max([Transporter Name] ) as [Transporter Name],max([Vendor Group Code] ) as [Vendor Group Code],max([Vendor Group Name]) as [Vendor Group Name]," &
                " max([Parent Vendor Code] )as [Parent Vendor Code],max([Vendor Control Account]) as [Vendor Control Account],max(Document_Type ) as Document_Type," &
               " [GL Account Code],max([GL Account Desc]) as [GL Account Desc],Amount,[Discount %] ,Discount,[total tax] ,[Amount Less Discount],max(Remarks ) as Remarks," &
               " max(Comments) as Comments,AddChargeCode ,max(AddChargeDesc ) as AddChargeDesc,is_Unclaimed_Tax ,[Landed Amount] ," &
                  "  [Item Code],max([Item Desc]) as [Item Desc] ,[Charge Category Code],max([Charge Category Desc] ) as [Charge Category Desc]," &
                   " charge_cat_charges,[Item Rate],Abatement_Per,Abatement_Amt,[Deduction Code] ,max(Deduction_Desc) as Deduction_Desc,Reverse_Charge_Amount ," &
                   " AgainstPayment_No,Payment_Amount,TDS_Per,[Hirerachy Code],[Cost Centre Code],Against_Milk_SRN_Commission_No,[Asset Code],[Item Type]," &
                  "  [SAC Code],[Taxable Amount],[Import Type],[Import Bill of Entry No],max([Import Bill of Entry Date] ) as [Import Bill of Entry Date]," &
                  "  [Import Bill of Entry Amount],[Original Invoice No] ,max([Original Invoice Date]) as [Original Invoice Date],MAX([ITC Eligible]) as [ITC Eligible] ,MAX([ITC Status]) as [ITC Status],MAX([ITC Details]) as [ITC Details]" & sumvariableTaxamt & "  " & SumvariableTaxRate & " ,[Total Amount],MAX(TDS_Percentage) AS [TDS Percentage],MAX(TDS_Calculated_Amount) AS [TDS Amount] from ( " &
            " " & strMCCMaterial & "" &
            ") as zz  left join  tspl_vendor_master on zz.[vendor code]= tspl_vendor_master.vendor_code group by  [Trans Type] , [Location Code] ,[Vendor Code] , [Location State GST] , [Location State Code] ,  [Purchase Invoice no] ,Document_No," &
        " [Vendor Invoice No],[GR No],[LR No],[Vehicle Code],GSTIN,[GST State Code],zz.GSTRegistered,zz.Incentive,[Tin No],EMP ,[Transporter Code], Amount,[Discount %] ,Discount ,[Total Amount]," &
        " [Amount Less Discount],[total tax],AddChargeCode,is_Unclaimed_Tax ,[Landed Amount] ,[Item Code] ,[Charge Category Code], [Item Rate],Abatement_Per,Abatement_Amt," &
         " [Deduction Code] ,Reverse_Charge_Amount ,AgainstPayment_No,Payment_Amount,TDS_Per,[Hirerachy Code],[Cost Centre Code],Against_Milk_SRN_Commission_No,[Asset Code]," &
         "  [Item Type],[SAC Code],[Taxable Amount],[Import Type],[Import Bill of Entry No],[Import Bill of Entry Amount],[Original Invoice No] ,[GL Account Code],charge_cat_charges,Detail_Line_No "

            ''RICHA BHA/28/09/18-000578
            '==update by preeti gupta Against ticket no[BHA/15/11/18-000683]
            If clsCommon.CompairString(btnSummary.IsChecked, "True") = CompairStringResult.Equal Then
                qry = "select Document_No, max([Vendor Code]) as [Vendor Code],max([Vendor Name]) as [Vendor Name],max([Trans Type]) as [Trans Type],max([Purchase Invoice no]) as [Purchase Invoice no]," &
                        " max([Vendor Invoice No]) as [Vendor Invoice No],max([Vendor Invoice Date]) as [Vendor Invoice Date] ,max([Location Code]) as [Location Code]," &
                        " max([Location Name]) as [Location Name],max([Vendor Control Account]) as [Vendor Control Account],max(Document_Type ) as Document_Type,max(GSTRegistered) as GSTRegistered,max(GSTIN) as GSTIN,sum([Amount Less Discount]) as [Amount Less Discount],sum([Total Tax]) as [Total Tax] ,sum([Total Amount]) as [Total Amount],max([TDS Percentage]) AS [TDS Percentage],max([TDS Amount]) AS [TDS Amount], MAX([ITC Eligible]) as [ITC Eligible] ,MAX([ITC Status]) as [ITC Status],MAX([ITC Details]) as [ITC Details]  from (" & qry & ") as final  group by  Document_No,convert(date,[Vendor Invoice Date],103)  order by convert(date,[Vendor Invoice Date],103) asc,Document_No "
            Else
                qry = " select * from (" & qry & ") as pp order by convert(date,[Vendor Invoice Date],103) asc,Document_No"
            End If





            dt = clsDBFuncationality.GetDataTable(qry)
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
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

        'If clsCommon.CompairString(btnSummary.IsChecked, "True") = CompairStringResult.Equal Then
        '    Dim item1 As New GridViewSummaryItem("Document_Total", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item1)
        'Else
        '    Dim item2 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item2)
        '    Dim item3 As New GridViewSummaryItem("Discount", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item3)
        '    Dim item4 As New GridViewSummaryItem("Amount Less Discount", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item4)
        '    Dim item5 As New GridViewSummaryItem("Total Tax", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item5)
        '    Dim item6 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item6)
        '    Dim item7 As New GridViewSummaryItem("Landed Amount", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item7)
        '    Dim item8 As New GridViewSummaryItem("Abatement_Amt", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item8)
        '    Dim item9 As New GridViewSummaryItem("Reverse_Charge_Amount", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item9)
        '    Dim item10 As New GridViewSummaryItem("Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item10)
        '    Dim item11 As New GridViewSummaryItem("Taxable Amount", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item11)
        'End If


        For Each col As GridViewColumn In gv1.Columns
            If clsCommon.CompairString(col.Name, "Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Discount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Amount Less Discount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total Tax") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Landed Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Abatement_Amt") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Reverse_Charge_Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Payment_Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Taxable Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Document_Total") = CompairStringResult.Equal Then
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
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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
                If gv1.CurrentColumn Is gv1.Columns("Document_No") Then
                    Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("Document_No").Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, DocNo)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Function Numberofvouchersfortheperiod() As String
        Dim Qry As String
        Dim Wrcls As String
        Wrcls = " WHERE 2=2 and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        Else
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + StrPermission + ")"
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
        End If
        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            Wrcls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
        End If
        'If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
        '    Wrcls += "  AND (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'PI' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'VSR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'PR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'BMPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'MPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'DAP'  end) IN (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
        'End If


        Qry = "select 0 as count, 'Number of vouchers for the period' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount , TSPL_VENDOR_INVOICE_HEAD.Document_No as Document_No," &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice' else " &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then 'Purchase Invoice' else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then 'Purchase Invoice'else 'Direct AP'" &
                " end end end end end end end end as Trans_Type," &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  else " &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then 'Purchase Invoice'else TSPL_VENDOR_INVOICE_HEAD.Document_No " &
                  " end end end end end end end end as AgainstDocNo," &
                   " TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, Amount_Less_Discount, Total_Tax, Document_Total" &
                   " from TSPL_VENDOR_INVOICE_HEAD left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code " &
                    " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code " &
                    " left join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_group_code " &
                            " " & Wrcls & " "
        Return Qry
    End Function

    Private Function Includedinreturns() As String
        Dim Qry As String
        Dim Wrcls As String
        Wrcls = " WHERE 2=2 and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " &
                " and (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>''" &
                " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>''" &
                " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>''" &
                 " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'')"

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        Else
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + StrPermission + ")"
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
        End If
        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            Wrcls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            Wrcls += "  AND (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'PI' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'VSR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'PR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'BMPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'MPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'DAP'  end) IN (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
        End If


        Qry = "select 1 as count, 'Included in returns' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount , TSPL_VENDOR_INVOICE_HEAD.Document_No as Document_No," &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice' else " &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then 'Purchase Invoice' else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then 'Purchase Invoice'else 'Direct AP'" &
                " end end end end end end end end as Trans_Type," &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  else " &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then 'Purchase Invoice'else TSPL_VENDOR_INVOICE_HEAD.Document_No " &
                  " end end end end end end end end as AgainstDocNo," &
                   " TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, Amount_Less_Discount, Total_Tax, Document_Total" &
                   " from TSPL_VENDOR_INVOICE_HEAD left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code " &
                    " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code " &
                    " left join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_group_code " &
                            " " & Wrcls & " "



        Return Qry
    End Function
    Private Function Invoicesreadyforreturns() As String
        Dim Qry As String
        Dim Wrcls As String
        Wrcls = " WHERE 2=2 and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " &
                " and (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>''" &
                " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>''" &
                " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>''" &
                 " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'')"

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        Else
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + StrPermission + ")"
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
        End If
        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            Wrcls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            Wrcls += "  AND (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'PI' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'VSR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'PR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'BMPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'MPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'DAP'  end) IN (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
        End If


        Qry = " select 2 as count,'Invoices ready for returns' as Type,row_number() over (partition by Document_No order by Document_No ) as DocCount , TSPL_VENDOR_INVOICE_HEAD.Document_No as Document_No," &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice' else " &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then 'Purchase Invoice' else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then 'Purchase Invoice'else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then 'Purchase Invoice'else 'Direct AP'" &
                " end end end end end end end end as Trans_Type," &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  else " &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL else" &
                " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work else" &
                 " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No ,'')<>'' then 'Purchase Invoice'else TSPL_VENDOR_INVOICE_HEAD.Document_No " &
                  " end end end end end end end end as AgainstDocNo," &
                   " TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, Amount_Less_Discount, Total_Tax, Document_Total" &
                   " from TSPL_VENDOR_INVOICE_HEAD left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code " &
                    " left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code " &
                    " left join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_group_code " &
                            " " & Wrcls & " "

        Return Qry

    End Function
    Private Function Invoiceswithmismatchininformation()
        Dim qry As String = "select 3 as count, 'Invoices with mismatch in information' as Type,0 as CountDoc,'' as Document_no,'' as Trans_type,'' as AgainstDocNo,Null as Invoice_Entry_date,'' as Vendor_code,0 as Amount_Less_Discount,0 as Total_Tax,0 as Document_Total"
        Return qry
    End Function
    Private Function Notincludedinreturnsduetoincompleteinformation()
        Dim qry As String = "select 4 as count, 'Not included in returns due to incomplete information' as Type,0 as CountDoc,'' as Document_no,'' as Trans_type,'' as AgainstDocNo,Null as Invoice_Entry_date,'' as Vendor_code,0 as Amount_Less_Discount,0 as Total_Tax,0 as Document_Total"
        Return qry
    End Function

    Private Function TaxablePurchasewithRegisteredVendor() As String
        Dim Qry As String
        Dim Wrcls As String
        Wrcls = " WHERE 2=2 and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and TSPL_VENDOR_INVOICE_HEAD.GSTRegistered=1 " &
                " and (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>''" &
                " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>''" &
                " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>''" &
                 " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'')"

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        Else
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + StrPermission + ")"
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
        End If
        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            Wrcls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            Wrcls += "  AND (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'PI' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'VSR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'PR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'BMPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'MPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'DAP'  end) IN (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
        End If


        Qry = "select '' as Number_of_Voucher_for_the_Period,(case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice'  when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'Purchase Return' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'Bulk Milk Purchase Invoice' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'Milk Purchase Invoice'  end) as trans_Type, (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No   end) as IncludeInReturn" &
                 ", case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Amount_less_discount*(-1) else TSPL_VENDOR_INVOICE_HEAD.Amount_less_discount end as RegAmount_less_discount,  case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Total_Tax *(-1) else TSPL_VENDOR_INVOICE_HEAD.Total_Tax end as RegTotal_Tax, case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Document_Total *(-1) else TSPL_VENDOR_INVOICE_HEAD.Document_Total end as RegDocument_Total" &
                  ", 0 as UnRegAmount_less_discount,  0 as UnRegTotal_Tax, 0 as UnRegDocument_Total" &
                " from TSPL_VENDOR_INVOICE_HEAD left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code left join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_group_code " &
                " " & Wrcls & " "

        Return Qry
    End Function
    Private Function TaxablePurchasewithoutRegisteredVendor() As String
        Dim Qry As String
        Dim Wrcls As String
        Wrcls = " WHERE 2=2 and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and TSPL_VENDOR_INVOICE_HEAD.GSTRegistered=0 " &
                " and (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>''" &
                " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>''" &
                " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>''" &
                 " or isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'')"

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        Else
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + StrPermission + ")"
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
        End If
        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            Wrcls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            Wrcls += "  AND (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'PI' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'VSR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'PR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'BMPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'MPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'DAP'  end) IN (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
        End If


        Qry = "select '' as Number_of_Voucher_for_the_Period,(case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice'  when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'Purchase Return' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'Bulk Milk Purchase Invoice' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'Milk Purchase Invoice'  end) as trans_Type, (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No   end) as IncludeInReturn" &
                 ",0 as RegAmount_less_discount,  0  as Regtotal_Tax, 0  as RegDocument_Total" &
                  ", case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Amount_less_discount*(-1) else TSPL_VENDOR_INVOICE_HEAD.Amount_less_discount end as UnRegAmount_less_discount,  case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Total_Tax *(-1) else TSPL_VENDOR_INVOICE_HEAD.Total_Tax end as UnRegTotal_Tax, case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Document_Total *(-1) else TSPL_VENDOR_INVOICE_HEAD.Document_Total end as UnRegDocument_Total" &
                " from TSPL_VENDOR_INVOICE_HEAD left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code left join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_group_code " &
                " " & Wrcls & " "

        Return Qry
    End Function
    Private Function TaxablePurchaseDirectAp() As String
        Dim Qry As String
        Dim Wrcls As String
        Wrcls = " WHERE 2=2 and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) and TSPL_VENDOR_INVOICE_HEAD.GSTRegistered=0 " &
               " isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' " &
                " AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' " &
                " AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' " &
                " AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' " &
                " AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')=''" &
                " AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition  ,'')=''" &
                " AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work   ,'')=''" &
                " AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No  ,'')=''"

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        Else
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + StrPermission + ")"
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            Wrcls += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
        End If
        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            Wrcls += " and tspl_vendor_master.Vendor_group_code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
        End If
        'If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
        '    Wrcls += "  AND (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'PI' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'VSR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'PR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'BMPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'MPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'DAP'  end) IN (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
        'End If


        Qry = "select '' as Number_of_Voucher_for_the_Period,(case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'Purchase Invoice'  when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'Purchase Return' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'Bulk Milk Purchase Invoice' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'Milk Purchase Invoice'  end) as trans_Type, (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No   end) as IncludeInReturn" &
                 ",0 as RegAmount_less_discount,  0  as Regtotal_Tax, 0  as RegDocument_Total" &
                  ", case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Amount_less_discount*(-1) else TSPL_VENDOR_INVOICE_HEAD.Amount_less_discount end as UnRegAmount_less_discount,  case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Total_Tax *(-1) else TSPL_VENDOR_INVOICE_HEAD.Total_Tax end as UnRegTotal_Tax, case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Document_Total *(-1) else TSPL_VENDOR_INVOICE_HEAD.Document_Total end as UnRegDocument_Total" &
                " from TSPL_VENDOR_INVOICE_HEAD left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code left join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code left join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=tspl_vendor_master.Vendor_group_code " &
                " " & Wrcls & " "

        Return Qry
    End Function
    Private Function GSTQry() As String
        Dim qry As String
        Dim StrNumberOfVouchersForthePeriod As String = Numberofvouchersfortheperiod()
        Dim StrIncludedinreturns As String = Includedinreturns()
        Dim StrTaxablePurchasewithRegisteredVendor As String = TaxablePurchasewithRegisteredVendor()
        Dim strTaxablePurchasewithoutRegisteredVendor As String = TaxablePurchasewithoutRegisteredVendor()
        qry = " select max(count) as Count,type, sum(DocCount) as DocCount " &
        " from ( " &
        " " & StrNumberOfVouchersForthePeriod & " " &
        " Union all " &
         " " & StrIncludedinreturns & " " &
           " Union all " &
         " " & StrTaxablePurchasewithRegisteredVendor & " " &
             " Union all " &
         " " & strTaxablePurchasewithoutRegisteredVendor & " " &
        " ) as Header "

        Return qry
    End Function
    Public Sub funPrint()
        Try
            Dim StrFinalQuryForGST = GSTQry()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrFinalQuryForGST)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = dt
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage2
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptGSTCompuatation", "Dispatch Product Sale", clsCommon.myCDate(txtFromDate.Value))


                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data found to print", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGST_Click(sender As Object, e As EventArgs) Handles btnGST.Click
        funPrint()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptAPReport & "'"))


            'If rbtnMCCRouteVLCCSelect.IsChecked Then


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If
            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember) + " "))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember) + " "))
            End If
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                arrHeader.Add(("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember) + " "))
            End If
            If TxtTax.arrValueMember IsNot Nothing AndAlso TxtTax.arrValueMember.Count > 0 Then
                arrHeader.Add(("Tax : " + clsCommon.GetMulcallStringWithComma(TxtTax.arrDispalyMember) + " "))
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                arrHeader.Add(("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember) + " "))
            End If

            'End If


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
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptAPReport & "'"))


            'If rbtnMCCRouteVLCCSelect.IsChecked Then


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If
            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember) + " "))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember) + " "))
            End If
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                arrHeader.Add(("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember) + " "))
            End If
            If TxtTax.arrValueMember IsNot Nothing AndAlso TxtTax.arrValueMember.Count > 0 Then
                arrHeader.Add(("Tax : " + clsCommon.GetMulcallStringWithComma(TxtTax.arrDispalyMember) + " "))
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                arrHeader.Add(("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember) + " "))
            End If

            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("AP Invoice Report" + IIf(btnSummary.IsChecked, "Summary", "Detail"), gv1, arrHeader, "AP Invoice Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub TxtTax__My_Click(sender As Object, e As EventArgs) Handles TxtTax._My_Click
        Dim qry As String = " Select Distinct TSPL_TAX_MASTER.Tax_Code as Code,TSPL_TAX_MASTER.Tax_Code_Desc as Name from TSPL_TAX_GROUP_DETAILS LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' and Tax_Group_Type ='P'"

        TxtTax.arrValueMember = clsCommon.ShowMultipleSelectForm("TaxMulSel", qry, "Code", "Code", TxtTax.arrValueMember, TxtTax.arrDispalyMember)
    End Sub
End Class
