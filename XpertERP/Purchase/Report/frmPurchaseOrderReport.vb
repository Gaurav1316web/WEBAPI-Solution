Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmPurchaseOrderReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub txtfndfrom_po_num__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        'Dim qry As String = "select purchaseorder_no as Code,description as [Description] from TSPL_PURCHASE_ORDER_HEAD "
        ' txtfndfrom_po_num.Value = clsCommon.ShowSelectForm("FrmPurchaseOrderReport", qry, "Code", "", txtfndfrom_po_num.Value, "Code", isButtonClicked)
    End Sub

    Private Sub txtfndto_docno__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        'Dim qry As String = "select purchaseorder_no as Code,description as [Description] from TSPL_PURCHASE_ORDER_HEAD "
        'txtfndto_docno.Value = clsCommon.ShowSelectForm("FrmPurchaseOrderReport", qry, "Code", "", txtfndto_docno.Value, "Code", isButtonClicked)
    End Sub

    Sub LoadDocuemntNo()
        Dim qry As String = "select purchaseorder_no as Code,description as [Description] from TSPL_PURCHASE_ORDER_HEAD"
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"
        'cbgDocument.CheckedValue

    End Sub

    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub

    Private Sub btnclose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPurchaseOrderReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnprint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmPurchaseOrderReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadVendor()
        chkdocAll.IsChecked = True
        chkVendor_all.IsChecked = True
        chkLocAll.IsChecked = True
        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        chkLocAll.IsChecked = True
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Public Sub LoadLocation()
        'Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"

    End Sub
    'Private Sub btnprint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
    '    If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
    '        Return
    '    End If
    '    If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
    '        Return
    '    End If
    '    Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
    '    Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")

    '    PrintData(fromdate, todate, chkDoc_select.IsChecked, cbgDocument.CheckedValue, chkVendor_select.IsChecked, cbgVendor.CheckedValue, False, "")
    'End Sub
    Public Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isForAbad As Boolean, ByVal abandomentNo As String, ByVal i As Integer, ByVal ReqNo As String, ByVal POType As String, ByVal AdditonalCharges As String)
        Dim locationArr As ArrayList = cbgLocation.CheckedValue
        Dim DocCodeFilter As String = ""
        Dim VendorCodeFilter As String = ""
        Dim LocCodeFilter As String

        If isDocSelect AndAlso ArrDoc.Count > 0 Then
            DocCodeFilter = clsCommon.GetMulcallString(cbgDocument.CheckedValue)
            DocCodeFilter = DocCodeFilter.Replace("'", "")
        End If
        If isVendorSelect AndAlso ArrVendor.Count > 0 Then
            VendorCodeFilter = clsCommon.GetMulcallString(cbgVendor.CheckedValue)
            VendorCodeFilter = VendorCodeFilter.Replace("'", "")
        End If



        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select one location ", Me.Text)
            Return
        Else
            LocCodeFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            LocCodeFilter = LocCodeFilter.Replace("'", "")
        End If
        If isDocSelect AndAlso ArrDoc.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Document", Me.Text)
            Return

        ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
            Return

        End If
        Dim IsAbedMent As Boolean = True
        Dim strQuery As String = ""
        Dim strOrederCls As String = ""
        If Not isForAbad Then
            IsAbedMent = True
            strQuery = "select '" + FromDate + "' as FromDate,'" + ToDate + "' as Todate,'" + DocCodeFilter + "' as DocFilter,'" + VendorCodeFilter + "' as VendorCodeFilter,'" + LocCodeFilter + "' as LocCodeFilter,  TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as address, TSPL_PURCHASE_ORDER_HEAD.Dept_Desc ,TSPL_PURCHASE_ORDER_HEAD.Delivery_date ,TSPL_PURCHASE_ORDER_HEAD.Remarks ,TSPL_PURCHASE_ORDER_HEAD.Terms_Code,TSPL_PURCHASE_ORDER_HEAD.Mode_Of_Transport as  ModeofTransport,TSPL_PURCHASE_ORDER_DETAIL .Specification as  specification,TSPL_PURCHASE_ORDER_HEAD.Abandonment_No,(select max(TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_Date) from TSPL_PURCHASE_ORDER_HEAD_Hist where TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_No=TSPL_PURCHASE_ORDER_HEAD.Abandonment_No and PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No) as Abandonment_Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as purchase_no ,TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date as po_date ,case TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Type when 'L'then 'Local' when 'I' then 'Import' when 'J' then 'Job Work' when 'O' then 'Open' when 'S' then 'Specific'else 'Null' end as po_type ,tspl_purchase_order_head.vendor_name,TSPL_PURCHASE_ORDER_HEAD .Vendor_Code as vendor_type,TSPL_PURCHASE_ORDER_HEAD .Terms_Code as termscode ,TSPL_PURCHASE_ORDER_HEAD .Ref_No as ref_no ,TSPL_PURCHASE_ORDER_HEAD .Comments as comments ,TSPL_PURCHASE_ORDER_HEAD .Discount_Amt as dis_amt,TSPL_PURCHASE_ORDER_DETAIL .Disc_Amt  as dis_amt1,TSPL_PURCHASE_ORDER_HEAD.Amount_Less_Discount  as aftrdiscount ,TSPL_PURCHASE_ORDER_HEAD .PO_Total_Amt as Total_amount,TSPL_PURCHASE_ORDER_HEAD.Discount_Base as bfrdisc_amount,tax1.Tax_Code_Desc as tax1name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax8_amt,0) as txt8amt,isnull (TSPL_PURCHASE_ORDER_HEAD.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax10_amt,0) as txt10amt,isnull(TSPL_PURCHASE_ORDER_HEAD .Total_Tax_Amt,0) as total_tax_amt,   " & _
            "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name1 as Add1Desc, " & _
            "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt1,0) as Add1, " & _
            "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name2 as Add2Desc, " & _
             "   isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt2,0) as Add2, " & _
             "   TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name3 as Add3Desc, " & _
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt3,0) as Add3, " & _
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name4 as Add4Desc, " & _
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt4,0) as Add4, " & _
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name5 as Add5Desc, " & _
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt5,0) as Add5, " & _
             "   TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name6 as Add6Desc, " & _
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt6,0) as Add6, " & _
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name7 as Add7Desc, " & _
             "   isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt7,0) as Add7, " & _
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name8 as Add8Desc, " & _
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt8,0) as Add8, " & _
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name9 as Add9Desc, " & _
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt9,0) as Add9, " & _
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name10 as Add10Desc, " & _
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt10,0) as Add10, " & _
             " TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt as DocAmt, " & _
             " TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as address1,TSPL_PURCHASE_ORDER_DETAIL.item_code as item_code,((TSPL_PURCHASE_ORDER_DETAIL.item_desc) +(case when TSPL_PURCHASE_ORDER_DETAIL.Specification='' then '' else ' . ' end) +TSPL_PURCHASE_ORDER_DETAIL.Specification+ (case when TSPL_PURCHASE_ORDER_DETAIL.Remarks='' then '' else ' / ' end) + TSPL_PURCHASE_ORDER_DETAIL.Remarks )  as itemdesc,TSPL_TERMS_MASTER.Terms_Desc  as termsdesc,TSPL_PURCHASE_ORDER_DETAIL.Row_Type,TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty as qty,TSPL_PURCHASE_ORDER_DETAIL.unit_code as uom,TSPL_PURCHASE_ORDER_DETAIL.item_cost as itemcost,TSPL_PURCHASE_ORDER_DETAIL.amount as amount,TSPL_PURCHASE_ORDER_HEAD.TAX1_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX2_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX3_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX4_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX5_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX6_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX7_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX8_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX9_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX10_Rate ,TSPL_PURCHASE_ORDER_DETAIL.Disc_Per as 'dis_per',TSPL_COMPANY_MASTER .CST_LST ,TSPL_COMPANY_MASTER .Tin_No,TSPL_COMPANY_MASTER .Ecc_No as ExciseNo ,TSPL_COMPANY_MASTER .CE_Range as Range,TSPL_COMPANY_MASTER .CE_Commissionerate as DivisionCommission, ( case when TSPL_VENDOR_MASTER.Phone2 >0 then TSPL_VENDOR_MASTER.Phone1 +','+TSPL_VENDOR_MASTER.Phone2 else TSPL_VENDOR_MASTER.Phone1 end) as VenPhone1 , TSPL_VENDOR_MASTER.Fax as VenFax, ( case when TSPL_COMPANY_MASTER.Phone2 >0 then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Phn,TSPL_COMPANY_MASTER.Fax as faxno,TSPL_COMPANY_MASTER.Email as EmailId, TSPL_PURCHASE_ORDER_DETAIL.Unit_code,Circle_No as APGST from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_PURCHASE_ORDER_HEAD.tax2 left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_PURCHASE_ORDER_HEAD .TAX3 left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_PURCHASE_ORDER_HEAD .tax4 left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_PURCHASE_ORDER_HEAD .tax5 left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX6 left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX7 left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX8 left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX9 left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX10    left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PURCHASE_ORDER_HEAD.Vendor_Code" & _
             " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location left outer join TSPL_TERMS_MASTER on TSPL_PURCHASE_ORDER_HEAD.Terms_Code =TSPL_TERMS_MASTER.Terms_Code where 2=2"
            '" TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1,TSPL_PURCHASE_ORDER_DETAIL.item_code as item_code,TSPL_PURCHASE_ORDER_DETAIL.item_desc as itemdesc,TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty as qty,TSPL_PURCHASE_ORDER_DETAIL.unit_code as uom,TSPL_PURCHASE_ORDER_DETAIL.item_cost as itemcost,TSPL_PURCHASE_ORDER_DETAIL.amount as amount from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_PURCHASE_ORDER_HEAD.tax2 left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_PURCHASE_ORDER_HEAD .TAX3 left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_PURCHASE_ORDER_HEAD .tax4 left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_PURCHASE_ORDER_HEAD .tax5 left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX6 left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX7 left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX8 left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX9 left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX10    left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PURCHASE_ORDER_HEAD.Vendor_Code  where 2=2"
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select one location ", Me.Text)
                    Return
                End If
                strQuery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If

            If isDocSelect Then ''For Original PO
                strQuery += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            ElseIf isVendorSelect Then
                strQuery += " and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
            Else
                strQuery += " and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<= convert(date,'" + ToDate + "',103)"
            End If
            strOrederCls = "order by TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No ,TSPL_PURCHASE_ORDER_DETAIL .Line_No"
        Else ''For PO History Data Display
            IsAbedMent = False
            strQuery = "select '" + FromDate + "' as FromDate,'" + ToDate + "' as Todate,'" + DocCodeFilter + "' as DocFilter,'" + VendorCodeFilter + "' as VendorCodeFilter,'" + LocCodeFilter + "' as LocCodeFilter, TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_No,TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_Date,TSPL_PURCHASE_ORDER_HEAD_Hist .PurchaseOrder_No as purchase_no ,TSPL_PURCHASE_ORDER_HEAD_Hist .PurchaseOrder_Date as po_date ,case TSPL_PURCHASE_ORDER_HEAD_Hist .PurchaseOrder_Type when 'L'then 'Local' when 'I' then 'Import' when 'J' then 'Job Work' when 'O' then 'Open' when 'S' then 'Specific'else 'Null' end as po_type ,TSPL_PURCHASE_ORDER_HEAD_Hist.vendor_name,TSPL_PURCHASE_ORDER_HEAD_Hist .Vendor_Code as vendor_type,TSPL_PURCHASE_ORDER_HEAD_Hist .Terms_Code as termscode ,TSPL_PURCHASE_ORDER_HEAD_Hist .Ref_No as ref_no ,TSPL_PURCHASE_ORDER_HEAD_Hist .Comments as comments ,TSPL_PURCHASE_ORDER_HEAD_Hist .Discount_Amt as dis_amt,TSPL_PURCHASE_ORDER_HEAD_Hist.Amount_Less_Discount  as aftrdiscount ,TSPL_PURCHASE_ORDER_HEAD_Hist .PO_Total_Amt as Total_amount,TSPL_PURCHASE_ORDER_HEAD_Hist.Discount_Base as bfrdisc_amount,tax1.Tax_Code_Desc as tax1name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax8_amt,0) as txt8amt,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (TSPL_PURCHASE_ORDER_HEAD_Hist.tax10_amt,0) as txt10amt,isnull(TSPL_PURCHASE_ORDER_HEAD_Hist .Total_Tax_Amt,0) as total_tax_amt, TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1,TSPL_PURCHASE_ORDER_DETAIL_Hist.item_code as item_code,TSPL_PURCHASE_ORDER_DETAIL_Hist.item_desc as itemdesc,TSPL_PURCHASE_ORDER_DETAIL_Hist.purchaseorder_qty as qty,TSPL_PURCHASE_ORDER_DETAIL_Hist.unit_code as uom,TSPL_PURCHASE_ORDER_DETAIL_Hist.item_cost as itemcost,TSPL_PURCHASE_ORDER_DETAIL_Hist.amount as amount from TSPL_PURCHASE_ORDER_DETAIL_Hist left outer join TSPL_PURCHASE_ORDER_HEAD_Hist  on TSPL_PURCHASE_ORDER_HEAD_Hist.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL_Hist.PurchaseOrder_No and TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_No =TSPL_PURCHASE_ORDER_DETAIL_Hist.Abandonment_No left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_PURCHASE_ORDER_HEAD_Hist.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_PURCHASE_ORDER_HEAD_Hist.tax2 left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_PURCHASE_ORDER_HEAD_Hist .TAX3 left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_PURCHASE_ORDER_HEAD_Hist .tax4 left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_PURCHASE_ORDER_HEAD_Hist .tax5 left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_PURCHASE_ORDER_HEAD_Hist .TAX6 left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_PURCHASE_ORDER_HEAD_Hist .TAX7 left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_PURCHASE_ORDER_HEAD_Hist .TAX8 left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_PURCHASE_ORDER_HEAD_Hist .TAX9 left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_PURCHASE_ORDER_HEAD_Hist .TAX10 left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD_Hist.comp_code " & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_PURCHASE_ORDER_DETAIL_Hist.Location where 2=2"
            strQuery += " and TSPL_PURCHASE_ORDER_HEAD_Hist.PurchaseOrder_No='" + clsCommon.myCstr(ArrDoc(0)) + "' and TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_No='" + abandomentNo + "'"
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select one location ", Me.Text)
                    Return
                End If
                strQuery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If
            strOrederCls = " order by TSPL_PURCHASE_ORDER_HEAD_Hist .PurchaseOrder_No ,TSPL_PURCHASE_ORDER_DETAIL_Hist .Line_No"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery + strOrederCls)
        dt.Columns.Add("Isabedment", Type.GetType("System.String"))
        dt.Columns.Add("ReqNo", Type.GetType("System.String"))
        dt.Columns.Add("ReqDt", Type.GetType("System.String"))
        dt.Columns.Add("total_add_charges", Type.GetType("System.String"))

        ' Dim i1 As Integer
        For Each dr As DataRow In dt.Rows
            If IsAbedMent Then
                dr("Isabedment") = True
            Else
                dr("Isabedment") = False
            End If
            dr("total_add_charges") = AdditonalCharges
            dr("ReqNo") = ReqNo
            dr("ReqDt") = clsDBFuncationality.getSingleValue("select Require_Date  from TSPL_REQUISITION_HEAD where Requisition_Id='" + ReqNo + "'")
        Next




        ' PurchaseOrderViewer.funreport(dt, "FrmPurchaseOrderReport", "Purchase Order Report")
        Dim frmCRV As New frmCrystalReportViewer()
        If clsCommon.myCstr(POType) = "Job Work" Then
            dt.Columns.Add("OrderWise", Type.GetType("System.String"))
            dt.Columns.Add("Note", Type.GetType("System.String"))

            For Each dr As DataRow In dt.Rows
                dr("OrderWise") = "WORK ORDER"
                dr("Note") = "THE ABOVE RATES ARE INCLUSIVE OF TAX"
            Next
            If objCommonVar.CurrentCompanyCode = "GUNTUR" Then
                If i = 1 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "FrmPurchaseWorkOrderReport-G", "Purchase Order Report")
                ElseIf i = 2 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.PaperSize10x12, "FrmPurchaseOrderReport PrePrintedFormat", "Purchase Order Report")
                End If
            ElseIf objCommonVar.CurrentCompanyCode = "VIZAG" Then

                If i = 1 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "FrmPurchaseOrderReport-V", "Purchase Order Report")
                ElseIf i = 2 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.PaperSize10x12, "FrmPurchaseOrderReport PrePrintedFormat", "Purchase Order Report")
                End If
            Else
                If i = 1 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "FrmPurchaseOrderReport", "Purchase Order Report")
                ElseIf i = 2 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.PaperSize10x12, "FrmPurchaseOrderReport PrePrintedFormat", "Purchase Order Report")
                End If
            End If
        ElseIf clsCommon.myCstr(POType) = "A" Then

            If i = 1 Then
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "FrmPurchaseOrderReport", "Purchase Order Report")
            ElseIf i = 2 Then
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.PaperSize10x12, "FrmPurchaseOrderReport PrePrintedFormat", "Purchase Order Report")
            End If
        Else
            dt.Columns.Add("OrderWise", Type.GetType("System.String"))
            dt.Columns.Add("Note", Type.GetType("System.String"))

            For Each dr As DataRow In dt.Rows
                dr("OrderWise") = "PURCHASE ORDER"
                dr("Note") = "6 MONTHS FROM THE DATE OF SUPPLY FOR ANY TYPE OF MANUFACTURING DEFECT ONLY."
            Next
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "VIZAG") = CompairStringResult.Equal Then
                If i = 1 Then
                    If (objCommonVar.CurrentCompanyCode = "VIZAG") Then
                        dt.Columns.Add("CreatedBy", Type.GetType("System.String"))
                        For Each dr As DataRow In dt.Rows
                            dr("Range") = "BHIMILI"
                            dr("DivisionCommission") = "||| Vishakapatnam"
                            dr("CreatedBy") = objCommonVar.CurrentUser
                        Next
                    End If
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "FrmPurchaseOrderReport-G", "Purchase Order Report")
                ElseIf i = 2 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.PaperSize10x12, "FrmPurchaseOrderReport PrePrintedFormat", "Purchase Order Report")
                End If
               
            Else
                If i = 1 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "FrmPurchaseOrderReport", "Purchase Order Report")
                ElseIf i = 2 Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.PaperSize10x12, "FrmPurchaseOrderReport PrePrintedFormat", "Purchase Order Report")
                End If

            End If
        End If
        frmCRV = Nothing
        IsAbedMent = True
    End Sub

    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll.ToggleStateChanged, chkDoc_select.ToggleStateChanged
        cbgDocument.Enabled = Not chkdocAll.IsChecked
        If chkDoc_select.IsChecked Then
            chkVendor_all.IsChecked = chkDoc_select.IsChecked
        End If
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendor_all.ToggleStateChanged, chkVendor_select.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendor_all.IsChecked
        If chkVendor_select.IsChecked Then
            chkdocAll.IsChecked = chkVendor_select.IsChecked
        End If
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        chkdocAll.IsChecked = True
        chkLocAll.IsChecked = True
        chkVendor_all.IsChecked = True
        chkLocAll.IsChecked = True
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PO-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub btnPrintAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintAmendment.Click
        If Not chkDoc_select.IsChecked OrElse cbgDocument.CheckedValue.Count <> 1 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select only One Amendment No for Print", Me.Text)
            Exit Sub
        End If
        Dim strDocNo As String = clsCommon.myCstr(cbgDocument.CheckedValue(0))
        PrintAbandoment(strDocNo)

    End Sub
    Dim i As Integer
    Public Sub PrintAbandoment(ByVal strDocNo As String)
        Dim qry As String = "select Abandonment_No as AmendementNo,Abandonment_By as [Amendement By],Abandonment_Date as [Amendement Date] from TSPL_PURCHASE_ORDER_HEAD_Hist "
        Dim strAmandomentNo As String = clsCommon.ShowSelectForm("POABDFilter", qry, "AmendementNo", "PurchaseOrder_No='" + strDocNo + "'", "xxx", "AmendementNo", True)
        If clsCommon.myLen(strAmandomentNo) > 0 Then
            Dim arr As New ArrayList
            arr.Add(strDocNo)
            i = 1
            PrintData("", "", True, arr, False, Nothing, True, strAmandomentNo, i, "", "A", "")
            i = 0
        End If
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintData()
    End Sub
    Sub PrintData()
        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Documnet Number", Me.Text)
            Return
        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Vendor", Me.Text)
            Return
        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")
        Dim arr As ArrayList = cbgDocument.CheckedValue
        Dim arrVendor As ArrayList = cbgVendor.CheckedValue
        Dim i As Integer = 1
        PrintData(fromdate, todate, chkDoc_select.IsChecked, arr, chkVendor_select.IsChecked, arrVendor, False, "", i, "", "A", "")
    End Sub

    Private Sub FrmPurchaseOrderReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If

    End Sub
    Private Sub btnPrint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint1.Click
        PrintData()
    End Sub

    Private Sub btnPrePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrePrint.Click
        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Documnet Number", Me.Text)
            Return
        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Vendor", Me.Text)
            Return
        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")
        Dim arr As ArrayList = cbgDocument.CheckedValue
        Dim arrVendor As ArrayList = cbgVendor.CheckedValue
        Dim i As Integer = 2
        PrintData(fromdate, todate, chkDoc_select.IsChecked, arr, chkVendor_select.IsChecked, arrVendor, False, "", i, "", "A", "")

    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked

    End Sub

End Class
