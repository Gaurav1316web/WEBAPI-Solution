Imports common
Imports System.IO
Public Class FrmGRNReport
    Inherits FrmMainTranScreen




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnGRNReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub


    Private Sub FrmfrmGRNReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'LoadInvoiceType()
        'LoadGRNNo()
        'LoadVendor()
        'LoadLocation()
        chkGRNNoAll.IsChecked = True
        chkVendorAll.IsChecked = True
        chkLocationAll.IsChecked = True

        'If objCommonVar.CurrentUser <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Sub LoadInvoiceType()
        'Dim dt As New DataTable
        'dt.Columns.Add("Code", GetType(String))
        'dt.Columns.Add("Name", GetType(String))
        'Dim dr As DataRow = dt.NewRow()
        'dr("Code") = ""
        'dr("Name") = "All"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "I"
        'dr("Name") = "Invoice"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "D"
        'dr("Name") = "Debit Note"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "C"
        'dr("Name") = "Credit Note"
        'dt.Rows.Add(dr)

        'cboDocType.DataSource = dt
        'cboDocType.ValueMember = "Code"
        'cboDocType.DisplayMember = "Name"

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()

        Try


            Dim FromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
            Dim ToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            PrintData(FromDate, ToDate, chkGRNNoSelect.IsChecked, cbgGRNNo.CheckedValue, chkVendorSelect.IsChecked, cbgVendor.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)

            'frmInventoryReportViewer.proShowReport("Transfer Report", clsCommon.GetPrintDate(txtFromDate.Value, "yyyy-MM-dd"), clsCommon.GetPrintDate(txtToDate.Value, "yyyy-MM-dd"), txtFromTransferNo.Value, txtToTransferNo.Value, strType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'chkGRNNoAll.IsChecked = True
        'chkVendorAll.IsChecked = True
        'chkLocationAll.IsChecked = True
    End Sub

    Private Sub chkGRNNoAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGRNNoAll.ToggleStateChanged, chkGRNNoSelect.ToggleStateChanged
        cbgGRNNo.Enabled = Not chkGRNNoAll.IsChecked
        If chkGRNNoSelect.IsChecked Then
            chkVendorAll.IsChecked = chkGRNNoSelect.IsChecked
        End If
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged, chkVendorSelect.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
        If chkVendorSelect.IsChecked Then
            chkGRNNoAll.IsChecked = chkVendorSelect.IsChecked
        End If
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
    Sub GridData(ByVal FromDate As String, ByVal ToDate As String, ByVal isGRNNoSelect As Boolean, ByVal ArrGRNNo As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isLocationSelect As Boolean, ByVal ArrLocation As ArrayList)
        Try
            Dim qry As String = ""
            If isGRNNoSelect AndAlso ArrGRNNo.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Document")
                Return
            ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
                Return
            ElseIf isLocationSelect AndAlso ArrLocation.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location For Print")
                Return
            End If

            ' Dim qry As String = "select [PO No],[PO Date],[PO Quantity],[GRN No],[GRN Date],[Vendor Code],[Vendor Name],[Comment],[Item Code],Descripton,Quantity from ( " & _
            ' "select detail.PO_Id as [PO No],convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as [PO Date],TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as [PO Quantity],head.GRN_No as 'GRN No', convert(varchar,head.GRN_Date,103) as 'GRN Date', head.Vendor_Code as 'Vendor Code', head.Vendor_Name as 'Vendor Name', " & _
            ' "head.Amount_Less_Discount as 'Amount After Discount',head.Comments as 'Comment', detail.Item_Code as 'Item Code', detail.Item_Desc as 'Descripton', " & _
            ' "detail.GRN_Qty as 'Quantity', detail.Item_Cost as 'Item Cost', detail.Disc_Amt as 'Discount', detail.Amount as 'Amount', HEAD.Discount_Base as 'Total Amount', " & _
            ' "HEAD.Discount_Amt as 'Discount Amount', HEAD.GRN_Total_Amt as 'Net Amount',head.Bill_To_Location as 'Location', tax1.Tax_Code_Desc as tax1name, " & _
            ' "isnull (HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (HEAD.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (HEAD.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (HEAD.tax10_amt,0) as txt10amt,isnull(HEAD .Total_Tax_Amt,0) as total_tax_amt  from TSPL_GRN_HEAD as head " & _
            ' " left outer join TSPL_GRN_DETAIL as detail on head.GRN_No=detail.grn_no " & _
            ' " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =HEAD.tax1 " & _
            ' " left outer join tspl_tax_master as tax2 on tax2.tax_code = HEAD.tax2 " & _
            ' " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=HEAD .TAX3" & _
            ' " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= HEAD .tax4" & _
            ' " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=HEAD .tax5 " & _
            ' " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =HEAD .TAX6 " & _
            ' " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =HEAD .TAX7 " & _
            ' " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =HEAD .TAX8 " & _
            ' " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =HEAD .TAX9" & _
            ' " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =HEAD .TAX10  " & _
            ' " left outer join TSPL_PURCHASE_ORDER_HEAD on head.Against_PO  =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
            '" left outer join (select PurchaseOrder_No,Item_Code,Unit_code,sum(PurchaseOrder_Qty) as PurchaseOrder_Qty from TSPL_PURCHASE_ORDER_DETAIL group by PurchaseOrder_No,Item_Code,Unit_code) as TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
            ' " and detail.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code  " & _
            ' " where 2=2 and detail.Row_Type='Item' "
            If chkTrackingReport.Checked = True Then
                qry = "select ROW_NUMBER() OVER(ORDER BY convert(varchar, TSPL_GRN_HEAD.GRN_Date,103),TSPL_GRN_HEAD.GRN_NO ASC) as SNo
                ,convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as [GRN Date],TSPL_GRN_HEAD.GRN_No as [GRN No]
                ,TSPL_GRN_HEAD.VehicleNo as [Vehicle No],TSPL_GRN_HEAD.Ref_No as [Tender No]
                ,TSPL_GRN_HEAD.bill_to_location as [Location Code],tspl_location_master.Location_Desc AS [Location Name]
                ,TSPL_GRN_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name]
                ,TSPL_GRN_DETAIL.Item_Code  as [Item Code],tspl_item_master.Item_Desc as [Item Name]
                ,Cast(TSPL_GRN_DETAIL.GRN_Qty as decimal(18,2)) AS [GRN Qty]
                ,(case when TSPL_GRN_HEAD.Status=1 then 'Posted' else 'UnPosted' end) as [GRN Status] 
                ,convert(varchar, TSPL_GRN_HEAD.VisualQCUpdatedDate,103) as [Visual QC Date]
                ,(case when TSPL_GRN_HEAD.VisualQCStatus=5 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatus=1 then 'Ok' 
				when TSPL_GRN_HEAD.VisualQCStatus='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatus='3' then 'Partial Ok'  
				when TSPL_GRN_HEAD.VisualQCStatus='4' then 'On Hold' else 'Pending' end) as [Visual QC Status]
                ,TSPL_GRN_HEAD.VisualQCRemarks as [Visual QC Remarks]
                ,convert(varchar, TSPL_GRN_HEAD.VisualQCUpdatedDateSecond,103) as [Visual QC Date Second]
                ,case when TSPL_GRN_HEAD.VisualQCUpdatedDateSecond is not null then (case when TSPL_GRN_HEAD.VisualQCStatusSecond=5 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatusSecond=1 then 'Ok' 
                when TSPL_GRN_HEAD.VisualQCStatusSecond='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='3' then 'Partial Ok'  
                when TSPL_GRN_HEAD.VisualQCStatusSecond='4' then 'On Hold' else 'Pending' end) else '' end as [Visual QC Status Second]
                ,VisualQCRemarksSecond as [Visual QC Remarks Second]
                ,convert(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as [PO Date],TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as [PO No]
                ,convert(varchar, TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as [Weighment Date],TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No]
                ,TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight as [Weighment Gross Weight],TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight as [Weighment Tare Weight]
                ,TSPL_PO_WEIGHTMENT_DETAIL.Extra_Weight as [Gunny Bag Weight],TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as [Weighment Net Weight]
                ,TSPL_PO_WEIGHTMENT_HEAD.Type as [Weighment Type]
				,(case when TSPL_PO_WEIGHTMENT_HEAD.Status=1 then 'Posted' when TSPL_PO_WEIGHTMENT_HEAD.Status=0 then 'UnPosted' end) as [Weighment Status]
                ,convert(varchar, TSPL_MRN_HEAD.MRN_Date,103) as [MRN Date],TSPL_MRN_HEAD.MRN_No as [MRN No]
                ,convert(varchar, TSPL_QC_CHECK_HEAD.Document_Date,103) as [Chemical QC Date],TSPL_QC_CHECK_HEAD.Document_Code as [Chemical QC No]
                ,TSPL_QC_CHECK_HEAD.QC_Status AS [Chemical QC Result]
                ,(case when TSPL_QC_CHECK_HEAD.Posted=1 then 'Posted' when TSPL_QC_CHECK_HEAD.Posted=0 then 'UnPosted' end) as [Chemical QC Status]
                ,(case when TSPL_QC_CHECK_HEAD.Approved_For_SRN=1 then 'Approved' when TSPL_QC_CHECK_HEAD.Approved_For_SRN=0 then 'UnApproved' end) as [Chemical QC Approved Status]
                ,convert(varchar, TSPL_SRN_HEAD.SRN_Date,103) as [SRN Date],TSPL_SRN_HEAD.SRN_No as [SRN No]
                ,TSPL_SRN_DETAIL.Item_Cost AS [SRN Rate]
                ,TSPL_SRN_DETAIL.MRN_Qty AS [SRN Received Qty]
                ,Cast(TSPL_SRN_DETAIL.Rejected_Qty as decimal(18,2)) as [SRN Rejected Qty]
                ,Cast(TSPL_SRN_DETAIL.SRN_Qty as decimal(18,2))  as [SRN Accepted Qty]
                ,TSPL_SRN_DETAIL.Amount as [SRN Amount]
                ,TSPL_SRN_DETAIL.Total_Tax_Amt as [SRN Tax Amount]
                ,TSPL_SRN_DETAIL.Item_Net_Amt as [SRN Net Amount]
                ,(case when TSPL_SRN_HEAD.Status=1 then 'Posted' when TSPL_SRN_HEAD.Status=0 then 'UnPosted' end) as [SRN Status] 
                ,convert(varchar, TSPL_PI_HEAD.PI_Date,103) as [Purchase Invoice Date]
                ,TSPL_PI_HEAD.PI_No as [Purchase Invoice No]
                ,(case when TSPL_PI_HEAD.Status=1 then 'Posted' when TSPL_PI_HEAD.Status=0 then 'UnPosted' end) as [Purchase Invoice Status]
                from TSPL_GRN_DETAIL
                  left outer join TSPL_GRN_HEAD ON TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                  left outer join TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
                left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                and TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_GRN_DETAIL.item_code
                left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No and TSPL_SRN_HEAD.Against_MRN=TSPL_MRN_HEAD.MRN_No
     			left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_SRN_HEAD.SRN_No
				and TSPL_PI_DETAIL.Item_Code=TSPL_GRN_DETAIL.item_code
				and TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_PI_DETAIL.item_code
                and TSPL_PI_DETAIL.GRN_ID=TSPL_SRN_HEAD.Against_GRN
				and TSPL_PI_DETAIL.MRN_ID=TSPL_SRN_HEAD.Against_MRN            
                left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No
                left outer join tspl_location_master on tspl_location_master.location_code=TSPL_GRN_HEAD.bill_to_location
                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_GRN_HEAD.Vendor_Code
                left outer join TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.SRN_NO =TSPL_SRN_HEAD.SRN_NO
                AND TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
                left outer join tspl_item_master ON tspl_item_master.item_code=TSPL_GRN_DETAIL.ITEM_CODE
                where 1=1 "
                If txtPoNo.arrValueMember IsNot Nothing AndAlso txtPoNo.arrValueMember.Count > 0 Then
                    qry += " and TSPL_GRN_HEAD.GRN_No in (" + clsCommon.GetMulcallString(txtPoNo.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    qry += "  and TSPL_GRN_HEAD.Vendor_Code   in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
                End If
                qry += " and TSPL_GRN_HEAD.IsCancel=0 and convert(date,TSPL_GRN_HEAD.GRN_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_GRN_HEAD.GRN_Date,103)<= convert(date,'" + ToDate + "',103)"
                qry += " order by convert(varchar, TSPL_GRN_HEAD.GRN_Date,103),TSPL_GRN_HEAD.GRN_NO"
            Else
                qry = "select [PO No],[PO Date],[PO Quantity],[GRN No],[GRN Date],[Vendor Code],[Vendor Name],[Comment],[Item Code],Descripton,Quantity from ( " &
            " select detail.PO_Id as [PO No],convert(varchar,TSPL_PURCHASE_ORDER.PurchaseOrder_Date,103) as [PO Date],TSPL_PURCHASE_ORDER.PurchaseOrder_Qty as [PO Quantity],head.GRN_No as 'GRN No', convert(varchar,head.GRN_Date,103) as 'GRN Date', head.Vendor_Code as 'Vendor Code', head.Vendor_Name as 'Vendor Name', head.Amount_Less_Discount as 'Amount After Discount',head.Comments as 'Comment', detail.Item_Code as 'Item Code', detail.Item_Desc as 'Descripton'" &
            ", detail.GRN_Qty as 'Quantity'" &
            ", HEAD.Discount_Base as 'Total Amount', HEAD.Discount_Amt as 'Discount Amount', HEAD.GRN_Total_Amt as 'Net Amount',head.Bill_To_Location as 'Location'" &
            ", tax1.Tax_Code_Desc as tax1name,isnull (HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (HEAD.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (HEAD.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (HEAD.tax10_amt,0) as txt10amt,isnull(HEAD .Total_Tax_Amt,0) as total_tax_amt  from TSPL_GRN_HEAD as head " &
            " left outer join (select grn_no,PO_Id,Item_Code,Item_Desc,Row_Type,sum(GRN_Qty) as GRN_Qty from TSPL_GRN_DETAIL group by grn_no,PO_Id,Item_Code,Item_Desc,Row_Type) as detail on head.GRN_No=detail.grn_no " &
            " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =HEAD.tax1 " &
            " left outer join tspl_tax_master as tax2 on tax2.tax_code = HEAD.tax2 " &
            " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=HEAD .TAX3" &
            " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= HEAD .tax4" &
            " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=HEAD .tax5 " &
            " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =HEAD .TAX6 " &
            " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =HEAD .TAX7 " &
            " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =HEAD .TAX8 " &
            " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =HEAD .TAX9" &
            " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =HEAD .TAX10  " &
            " left outer join (select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,Item_Code,Unit_code,sum(PurchaseOrder_Qty) as PurchaseOrder_Qty from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No group by TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,Item_Code,Unit_code,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ) as TSPL_PURCHASE_ORDER on " &
            " detail.Item_Code=TSPL_PURCHASE_ORDER.Item_Code" &
            " and  detail.PO_Id  =TSPL_PURCHASE_ORDER.PurchaseOrder_No" &
            " where 2=2 and detail.Row_Type='Item' "


                'If isGRNNoSelect Then
                '    qry += " and HEAD.GRN_No in (" + clsCommon.GetMulcallString(ArrGRNNo) + ") "
                'End If
                'If isVendorSelect Then
                '    qry += " and HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
                'End If
                'If isLocationSelect Then
                '    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
                '    qry += " and head.Bill_To_Location in  (" + clsCommon.GetMulcallString(ArrLocation) + ")"
                'End If

                'End If
                '====added by shivani
                If txtPoNo.arrValueMember IsNot Nothing AndAlso txtPoNo.arrValueMember.Count > 0 Then
                    qry += " and HEAD.GRN_No in (" + clsCommon.GetMulcallString(txtPoNo.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and head.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    qry += "  and HEAD.Vendor_Code   in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
                End If
                '===================
                qry += " and head.IsCancel=0 and convert(date,HEAD.GRN_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,HEAD.GRN_Date,103)<= convert(date,'" + ToDate + "',103)"
                qry += " ) aa order by [GRN Date]"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.MasterTemplate.BestFitColumns()
                Gv1.ReadOnly = True
                ReStoreGridLayout()
            End If

            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found")
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
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
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.mbtnGRNReport & "'"))
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtPoNo.arrDispalyMember IsNot Nothing AndAlso txtPoNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("GRN No : " + clsCommon.GetMulcallStringWithComma(txtPoNo.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            'If chkGRNNoSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgGRNNo.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("GRN NO : " + strtemp)
            'End If

            'If chkVendorSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgVendor.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Vendor : " + strtemp)
            'End If


            'If chkLocationSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Location : " + strtemp)
            'End If
            ''

            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Gate Entry Register ", Gv1, arrHeader, "Gate Entry Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isGRNNoSelect As Boolean, ByVal ArrGRNNo As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isLocationSelect As Boolean, ByVal ArrLocation As ArrayList)

        'If isGRNNoSelect AndAlso ArrGRNNo.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Document")
        '    Return
        'ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
        '    Return
        'ElseIf isLocationSelect AndAlso ArrLocation.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Location For Print")
        '    Return
        'End If

        Dim qry As String = "select head.GRN_No as 'GRN No', head.GRN_Date as 'GRN Date', head.Vendor_Code as 'Vendor Code', head.Vendor_Name as 'Vendor Name',head.Amount_Less_Discount as 'Amount After Discount',head.Comments as 'Comment', detail.Item_Code as 'Item Code', detail.Item_Desc as 'Descripton',detail.GRN_Qty as 'Quantity', detail.Item_Cost as 'Item Cost', detail.Disc_Amt as 'Discount', detail.Amount as 'Amount', HEAD.Discount_Base as 'Total Amount',HEAD.Discount_Amt as 'Discount Amount', HEAD.GRN_Total_Amt as 'Net Amount',head.Bill_To_Location as 'Location', tax1.Tax_Code_Desc as tax1name,isnull (HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (HEAD.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (HEAD.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (HEAD.tax10_amt,0) as txt10amt,isnull(HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1  from TSPL_GRN_HEAD as head " & _
" left outer join TSPL_GRN_DETAIL as detail on head.GRN_No=detail.grn_no " & _
" left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =HEAD.tax1 " & _
" left outer join tspl_tax_master as tax2 on tax2.tax_code = HEAD.tax2 " & _
" left outer join tspl_tax_master as tax3 on tax3.Tax_Code=HEAD .TAX3" & _
" left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= HEAD .tax4" & _
" left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=HEAD .tax5 " & _
" left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =HEAD .TAX6 " & _
" left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =HEAD .TAX7 " & _
" left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =HEAD .TAX8 " & _
" left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =HEAD .TAX9" & _
" left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =HEAD .TAX10 " & _
" left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = HEAD.comp_code where 2=2 "

        'For ii As Integer = 1 To 10
        '    Dim strii As String = clsCommon.myCstr(ii)
        '    qry += " union all" & _
        '        " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else 0 end as DrAmt,case when Document_Type in('D') then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else 0 end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')"
        'Next
        'qry += " union all" & _
        '" select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('D') then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else 0 end as DrAmt,case when Document_Type in('I','C') then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else 0 end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC where TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')" & _
        '" union all " & _
        '" select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else 0 end as DrAmt,case when Document_Type in ('D') then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else 0 end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC where TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')" & _
        '" union all " & _
        '" select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_REMITTANCE.Branch_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') then TSPL_REMITTANCE.Actual_Total_TDS else 0 end as DrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') then TSPL_REMITTANCE.Actual_Total_TDS else 0 end as CrAmt from TSPL_REMITTANCE left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE.Branch_GL_AC left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_REMITTANCE.Document_No where TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')" & _
        '" )Final left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=final.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code left outer join TSPL_USER_MASTER as CreatedBy on CreatedBy.User_Code=TSPL_VENDOR_INVOICE_HEAD.Created_By" & _
        '" left outer join TSPL_USER_MASTER as AuthorisedBy on AuthorisedBy .User_Code=TSPL_VENDOR_INVOICE_HEAD.Modify_By where 2=2 "

        'If isGRNNoSelect Then
        '    qry += " and HEAD.GRN_No in (" + clsCommon.GetMulcallString(ArrGRNNo) + ") "
        'End If
        'If isVendorSelect Then
        '    qry += " and HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        'End If
        'If isLocationSelect Then
        '    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
        '    qry += " and head.Bill_To_Location in  (" + clsCommon.GetMulcallString(ArrLocation) + ")"
        'End If
        '====added by shivani
        If txtPoNo.arrValueMember IsNot Nothing AndAlso txtPoNo.arrValueMember.Count > 0 Then
            qry += " and HEAD.GRN_No in (" + clsCommon.GetMulcallString(txtPoNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and head.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += "  and HEAD.Vendor_Code   in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        '===================
        qry += " and head.IsCancel=0 and convert(date,HEAD.GRN_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,HEAD.GRN_Date,103)<= convert(date,'" + ToDate + "',103)"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptGRNReport", "GRN Report")
        frmCRV = Nothing
    End Sub

  
    Private Sub FrmGRNReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P AndAlso MyBase.isPrintFlag Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub

    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(chkTrackingReport.Checked = True, "Tracking", "")
            TemplateGridview = Gv1
            Dim FromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
            Dim ToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            GridData(FromDate, ToDate, chkGRNNoSelect.IsChecked, cbgGRNNo.CheckedValue, chkVendorSelect.IsChecked, cbgVendor.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export.Click
        If Gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If Gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Sub LoadGRNNo()
        Dim qry As String = "select GRN_No,GRN_Date from TSPL_GRN_HEAD"
        cbgGRNNo.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgGRNNo.ValueMember = "GRN_No"
        cbgGRNNo.DisplayMember = "GRN_Date"
        'cbgGRNNo.CheckedValue()

    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  WHERE  Status='N'   order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub
    Public Sub LoadLocation()
        'Commment remove by abhishek kumar as on 19/06/2012
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub
    Private Sub txtPoNo__My_Click(sender As Object, e As EventArgs) Handles txtPoNo._My_Click
        Dim qry As String = "select GRN_No as Code,GRN_Date as Date from TSPL_GRN_HEAD"
        txtPoNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Date", txtPoNo.arrValueMember, txtPoNo.arrDispalyMember)

    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub Gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If Gv1.Rows.Count > 0 Then
            Dim strDoc
            If Gv1.CurrentColumn Is Gv1.Columns("GRN No") Then
                strDoc = Gv1.CurrentRow.Cells("GRN No").Value
                MDI.ShowForm(clsUserMgtCode.mbtnGRN, "", True, strDoc)
            ElseIf Gv1.CurrentColumn Is Gv1.Columns("PO No") Then
                strDoc = Gv1.CurrentRow.Cells("PO No").Value
                MDI.ShowForm(clsUserMgtCode.mbtnPurchaseOrder, "", True, strDoc)
            End If
        End If

    End Sub

 
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
