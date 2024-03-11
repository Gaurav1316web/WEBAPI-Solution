'--Updation By--[Pankaj Kumar Chaudhary]---Against Ticket No---[BM00000000816, BM00000002999 28/06/2014]
'' Anubhooti(1-July-2014) Added Export Permission Against BM00000003016 ''''''''  BM00000003289  
'' Priti for ticket no '''' BM00000003329
Imports common
Imports System.IO
Public Class FrmTaxTracking
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim ShowVatSeriesNo As Double

    Private Sub FrmTaxTracking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'loadState()
        'loadlocation()

        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        reset()
        SetUserMgmtNew()

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        '===Sanjeet(UDL)17/11/2016===
        ShowVatSeriesNo = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVatSeriesNoSeprately, clsFixedParameterCode.ShowVatSeriesNoSeprately, Nothing))
        '==========================
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TaxTracking)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(1-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

        'btnSave.Visible = MyBase.isModifyFlag
        ' btnPrint.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub reset()
        rdoPur.IsChecked = True
        ChkVendorInvoice.Visible = True
        chkARinvoice.Visible = False
        'ChkSaleReturn.Visible = False
        'ChkSRInter.Visible = False
        'chkSaleInvoice.Checked = False
        ChkVendorInvoice.Checked = True
        chkARinvoice.Checked = False
        'ChkSaleReturn.Checked = False
        'ChkSRInter.Checked = False
        'chkSaleInvoice.Checked = False
        LoadTaxCode()
        LoadTaxRate()
        loadState()
        chkStateAll.IsChecked = True
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkTaxall.IsChecked = True
        chkrateall.IsChecked = True
        chkLocAll.IsChecked = True
        Chksummary.Checked = False
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
    End Sub

    Sub LoadTaxCode()
        Dim strquery As String = "select Tax_Code,Tax_Code_Desc from TSPL_TAX_MASTER "
        cgvTaxCode.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cgvTaxCode.ValueMember = "Tax_Code"
        cgvTaxCode.DisplayMember = "Tax_Code_Desc"
    End Sub

    Sub LoadTaxRate()
        'Dim strquery As String = "select distinct Tax_Rate from TSPL_TAX_RATES  "

        Dim strquery As String = "  select distinct  cast ( Tax_Rate as varchar(10)) as Rate  from TSPL_TAX_RATES order by Rate "
        cgvRate.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cgvRate.ValueMember = "Rate"
        cgvRate.DisplayMember = "Rate"
    End Sub
    ''RICHA AGARWAL TICKET NO BM00000008797
    Sub loadState()
        Dim strquery As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER "
        cgvState.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cgvState.ValueMember = "Code"
        cgvState.DisplayMember = "Name"
    End Sub
    Private Sub chkTaxall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTaxall.ToggleStateChanged
        cgvTaxCode.Enabled = Not chkTaxall.IsChecked
    End Sub

    Private Sub chkrateall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkrateall.ToggleStateChanged
        cgvRate.Enabled = Not chkrateall.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PageSetupReport_ID = GetReportId()
        TemplateGridview = gv
        PrintData()

    End Sub

    Sub PrintData()
        Try
            gv.DataSource = Nothing
            Dim loccode As String = ""
            Dim taxratefilter As String = ""
            Dim tacxodefilter As String = ""

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                loccode = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                loccode = loccode.Replace("'", "")
            End If
            If chkRateselect.IsChecked AndAlso cgvRate.CheckedValue.Count > 0 Then
                taxratefilter = clsCommon.GetMulcallString(cgvRate.CheckedValue)
                taxratefilter = taxratefilter.Replace("'", "")
            End If
            If chkSelectTax.IsChecked AndAlso cgvTaxCode.CheckedValue.Count > 0 Then
                tacxodefilter = clsCommon.GetMulcallString(cgvTaxCode.CheckedValue)
                tacxodefilter = tacxodefilter.Replace("'", "")
            End If
            If chkRateselect.IsChecked AndAlso cgvRate.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select at least one Rate.", Me.Text)
                Return
            End If
            If chkSelectTax.IsChecked AndAlso cgvTaxCode.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select at least one Tax.", Me.Text)
                Return
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select at least one Location.", Me.Text)
                Return
            End If
            If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select at least one State.", Me.Text)
                Return
            End If

            Dim qry As String = ""
            If (rdoPur.IsChecked) Then
                If (ChkAll.Checked = False AndAlso ChkVendorInvoice.Checked = False) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select at least one Screen.", Me.Text)
                    Return
                End If
            Else
                If (ChkAll.Checked = False AndAlso chkSaleInvoice.Checked = False AndAlso chkARinvoice.Checked = False AndAlso ChkSaleReturn.Checked = False AndAlso ChkSRInter.Checked = False AndAlso chkSaleInvoice.Checked = False AndAlso chkTransfer.Checked = False) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select at least one Screen.", Me.Text)
                    Return
                End If
            End If
            Dim from_Date As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy")
            Dim to_Date As String = clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy")

            If rdoPur.IsChecked Then
                If Chksummary.Checked = False Then
                    qry = "Select max(source) as source,max(TAXRATEFILTER) as TAXRATEFILTER,max(TAXCODEFILTER) as TAXCODEFILTER,max(RType) as RType,max(fromdate) as fromdate," & _
                    "max(todate) as todate,max(code) as code,max(Name) as Name,max([Vendor Address]) as [Vendor Address],max([Vendor GST NO]) as [Vendor GST NO], " & _
                    "max([Vendor GST State]) as [Vendor GST State],max(Vendor_Customer_Tin_No) as Vendor_Customer_Tin_No,max(Document_No) as Document_No, " & _
                    "max(DocDate) as DocDate,[Source Doc No],tax,TaxRate,sum(Document_Total) as Document_Total,sum(TaxBase) as TaxBase,sum(TaxAmt) as TaxAmt, " & _
                    "sum(Document_Total) as TotalAmount,max(Loc_Code) as Loc_Code,max(Loc_Segment) as Loc_Segment,case when max(Tax_Calculation_Type)=1 then 'Manual' else 'Automatic' end as Tax_Calculation_Type from ( "
                    qry += "select  source, "
                    qry += " '" + taxratefilter + "' as TAXRATEFILTER,'" + loccode + "' as LOCATIONFILTER ,'" + tacxodefilter + "' as TAXCODEFILTER, 'Purchase' as RType,'" + from_Date + "' as fromdate ,'" + to_Date + "' as todate ,"
                    qry += " Code, Name,TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2 as [Vendor Address],tspl_vendor_master.GSTFinalNo as [Vendor GST NO],tspl_state_master.GST_STATE_Code as [Vendor GST State], TSPL_VENDOR_MASTER.Tin_No as Vendor_Customer_Tin_No,Document_No,convert(varchar(10),DocDate,103) as DocDate,SOURCEDOC as [Source Doc No] ,Tax,TaxRate,Document_Total,TaxBase,TaxAmt,(TaxBase+TAxAmt)TotalAmount,loc_code,TSPL_GL_SEGMENT_CODE.description as Loc_Segment,Tax_Calculation_Type from( "
                ElseIf Chksummary.Checked = True Then
                    qry = " select  max(source) as Source ,max(Code) as Code,max(Name) as Name,max(TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2) as [Vendor Address],max(tspl_vendor_master.GSTFinalNo) as [Vendor GST NO],max(tspl_state_master.GST_STATE_Code) as [Vendor GST State], MAX(TSPL_VENDOR_MASTER.Tin_No) as Vendor_Customer_Tin_No,max(Document_No)as Document_No ,[Source Doc No] as [Source Doc No] ,max(convert(varchar(10),DocDate,103)) as DocDate,max(TaxBase) as TaxBase,sum(TaxAmt) as TaxAmt ,max(Document_Total) as TotalAmt,max(loc_code) as loc_code,max(TSPL_GL_SEGMENT_CODE.description) as Loc_Segment,max(Tax_Calculation_Type) as Tax_Calculation_Type from ( " & _
                        " select max(source) as source,max(Code) as Code,max(Name) as Name,max(Document_No)as Document_No ,SOURCEDOC as [Source Doc No] ,max(convert(varchar(10),DocDate,103)) as DocDate,tax,TaxRate,sum(Document_Total) as Document_Total,sum(TaxBase) as TaxBase,sum(TaxAmt) as TaxAmt, " & _
                        "sum(Document_Total) as TotalAmount,max(Loc_Code) as Loc_Code,case when max(Tax_Calculation_Type)=1 then 'Manual' else 'Automatic' end as Tax_Calculation_Type from ( "
                End If

                For ii As Integer = 1 To 10 ''BHA/25/09/18-000571 by balwinder on 03/10/2018
                    qry += " select TSPL_VENDOR_INVOICE_HEAD.Document_No as SOURCEDOC ,'Vendor-Invoice' as source, Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,convert(varchar,Posting_Date,103) as DocDate, " & _
                "TSPL_VENDOR_INVOICE_DETAIL.TAX" + clsCommon.myCstr(ii) + " as Tax,TSPL_VENDOR_INVOICE_DETAIL.TAX" + clsCommon.myCstr(ii) + "_Rate as TaxRate, " & _
                "(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 1 else -1 end )*Total_Amount as Document_Total, " & _
                " (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 1 else -1 end )* case when TSPL_VENDOR_INVOICE_HEAD.Tax_Calculation_Type=1 then  TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount else TSPL_VENDOR_INVOICE_DETAIL.TAX" + clsCommon.myCstr(ii) + "_Base_Amt end as TaxBase, " & _
                "(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 1 else -1 end )*TSPL_VENDOR_INVOICE_DETAIL.TAX" + clsCommon.myCstr(ii) + "_Amt	as TaxAmt " & _
                ",TSPL_VENDOR_INVOICE_HEAD.loc_code,TSPL_VENDOR_INVOICE_HEAD.Tax_Calculation_Type " & _
                "from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  " & _
                "left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX" + clsCommon.myCstr(ii) + "_Amt<>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and cast(TSPL_VENDOR_INVOICE_HEAD.Posting_Date as date) between '" & from_Date & "' and '" & to_Date & "' "
                    If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                        qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                    End If

                    If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                        qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                    End If
                    qry += Environment.NewLine + " Union all " + Environment.NewLine
                Next

                ''By Accounts

                qry += " select  TSPL_VENDOR_INVOICE_HEAD.Document_No as SOURCEDOC  ,'Vendor-Invoice' as source,  Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No, convert(varchar,Posting_Date,103) as DocDate, TSPL_GL_ACCOUNTS.Tax_Type as Tax,0 as TaxRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then -1 else 1 end )*TSPL_VENDOR_INVOICE_DETAIL.Total_Amount as Document_Total, 0 as  TaxBase, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then -1 else 1 end )*TSPL_VENDOR_INVOICE_DETAIL.Total_Amount	as TaxAmt " + Environment.NewLine
                qry += ",TSPL_VENDOR_INVOICE_HEAD.loc_code,TSPL_VENDOR_INVOICE_HEAD.Tax_Calculation_Type "
                qry += " from TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine
                qry += " left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  " + Environment.NewLine
                qry += " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  " + Environment.NewLine
                qry += " where LEN(ISNULL(TSPL_GL_ACCOUNTS.Tax_Type,''))>0 AND TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS NOT NULL  " + Environment.NewLine
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                End If

                qry += Environment.NewLine + " Union all " + Environment.NewLine
                qry += " select  TSPL_PAYMENT_HEADER.Payment_No as SOURCEDOC  ,'Payment' as source,  TSPL_PAYMENT_HEADER.Vendor_Code as Code,TSPL_PAYMENT_HEADER.Vendor_Name as Name,TSPL_PAYMENT_HEADER.Payment_No as Document_No,convert(varchar, TSPL_PAYMENT_HEADER.Payment_Date,103) as DocDate, TSPL_GL_ACCOUNTS.Tax_Type as Tax,0 as TaxRate,  TSPL_PAYMENT_DETAIL.Net_Balance as Document_Total, 0 as  TaxBase, TSPL_PAYMENT_DETAIL.Net_Balance as TaxAmt "
                qry += ",TSPL_PAYMENT_HEADER.location_code as loc_code,0 as Tax_Calculation_Type "
                qry += " from TSPL_PAYMENT_DETAIL "
                qry += " left outer join TSPL_PAYMENT_HEADER  on TSPL_PAYMENT_HEADER .Payment_No =TSPL_PAYMENT_DETAIL.Payment_No  "
                qry += " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PAYMENT_DETAIL.Account_Code  "
                qry += " where TSPL_PAYMENT_HEADER.Payment_Type='MI' and LEN(ISNULL(TSPL_GL_ACCOUNTS.Tax_Type,''))>0 AND TSPL_PAYMENT_HEADER.Posted ='1' and cast(TSPL_PAYMENT_HEADER.Payment_Date as date) between '" & from_Date & "' and '" & to_Date & "'"
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                End If
                qry += Environment.NewLine + " Union all " + Environment.NewLine
                qry += " select  TSPL_VCGL_Head.Document_No as SOURCEDOC  ,'VCGL' as source,  VC_Code as Code,VC_Name as Name,TSPL_VCGL_Head.Document_No as Document_No,CONVERT(varchar, Document_Date,103) as DocDate, TSPL_GL_ACCOUNTS.Tax_Type as Tax,0 as TaxRate, (case when TSPL_VCGL_Detail.Cr_Amount>0  then -1*Cr_Amount else Dr_Amount end ) as Document_Total, 0 as  TaxBase, (case when TSPL_VCGL_Detail.Cr_Amount>0  then -1*Cr_Amount else Dr_Amount end ) as TaxAmt "
                qry += ",tspl_vcgl_head.Location_Segment as loc_code,0 as Tax_Calculation_Type "
                qry += " from TSPL_VCGL_Detail "
                qry += " left outer join TSPL_VCGL_Head  on TSPL_VCGL_Head .Document_No =TSPL_VCGL_Detail.Document_No  "
                qry += " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VCGL_Detail.GL_Account_Code  "
                qry += " where LEN(ISNULL(TSPL_GL_ACCOUNTS.Tax_Type,''))>0 AND TSPL_VCGL_Head.Posting_Date IS NOT NULL  "
                qry += " and (TSPL_VCGL_Detail.Row_Type='Vendor'  or (TSPL_VCGL_Detail.Row_Type='GL' and TSPL_GL_ACCOUNTS.Purchase_Sale_Type=1)) and cast(TSPL_VCGL_Head.Document_Date as date) between '" & from_Date & "' and '" & to_Date & "'"
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                End If

                qry += Environment.NewLine + " Union all " + Environment.NewLine
                qry += " select  TSPL_JOURNAL_MASTER.Voucher_No as SOURCEDOC  ,'GL-JE' as source,  '' as Code,'' as Name,TSPL_JOURNAL_MASTER.Voucher_No as Document_No,CONVERT(varchar, TSPL_JOURNAL_MASTER.Voucher_Date,103) as DocDate, TSPL_GL_ACCOUNTS.Tax_Type as Tax,0 as TaxRate,  TSPL_JOURNAL_DETAILS.Amount as Document_Total, 0 as  TaxBase,TSPL_JOURNAL_DETAILS.Amount as TaxAmt "
                qry += ",TSPL_JOURNAL_MASTER.Segment_code as loc_code,0 as Tax_Calculation_Type "
                qry += " from TSPL_JOURNAL_DETAILS "
                qry += " left outer join TSPL_JOURNAL_MASTER  on TSPL_JOURNAL_MASTER .Voucher_No =TSPL_JOURNAL_DETAILS.Voucher_No  "
                qry += " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_JOURNAL_DETAILS.Account_code  "
                qry += " where LEN(ISNULL(TSPL_GL_ACCOUNTS.Tax_Type,''))>0 AND TSPL_JOURNAL_MASTER.Authorized ='A' and TSPL_GL_ACCOUNTS.Purchase_Sale_Type=1 and TSPL_JOURNAL_MASTER.Source_Code='GL-JE' and cast(TSPL_JOURNAL_MASTER.Voucher_Date as date) between '" & from_Date & "' and '" & to_Date & "'"
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                End If

                ''End of By Accounts

                If Chksummary.Checked Then
                    qry += "  ) a group by SOURCEDOC,Tax,TaxRate "
                End If
                qry += " )abc  LEFT OUTER JOIN TSPL_VENDOR_MASTER ON abc.Code=TSPL_VENDOR_MASTER.Vendor_Code  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VENDOR_MASTER.State_Code "
                qry += " left outer join TSPL_GL_SEGMENT_CODE on abc.loc_code=TSPL_GL_SEGMENT_CODE.segment_code and TSPL_GL_SEGMENT_CODE.seg_no=7 where 2=2 and convert(date,DocDate,103) >= convert(date,'" + clsCommon.GetPrintDate(from_Date, "dd/MM/yyyy") + "',103) and convert(date,DocDate,103) <= convert(date,'" + clsCommon.GetPrintDate(to_Date, "dd/MM/yyyy") + "',103) "

                If chkSelectTax.IsChecked AndAlso cgvTaxCode.CheckedValue.Count > 0 Then
                    qry += " and tax in  (" + clsCommon.GetMulcallString(cgvTaxCode.CheckedValue) + ")  "
                End If
                If chkRateselect.IsChecked AndAlso cgvRate.CheckedValue.Count > 0 Then
                    qry += " and TaxRate in  (" + clsCommon.GetMulcallString(cgvRate.CheckedValue) + ")  "
                ElseIf chkRateselect.IsChecked Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Tax Rate")
                    Exit Sub
                End If
                If (Chksummary.Checked = True) Then
                    qry += "  group by [Source Doc No]"
                End If
                If Chksummary.Checked = False Then
                    qry += " ) a group by [Source Doc No],Tax,TaxRate"
                End If
            ElseIf rdoSale.IsChecked = True Then
                If (Chksummary.Checked = False) Then
                    qry = " select  Source, "
                    qry += "  '" + taxratefilter + "' as TAXRATEFILTER,'" + loccode + "' as LOCATIONFILTER ,'" + tacxodefilter + "' as TAXCODEFILTER, max(RType) as RType,max(fromdate) as fromdate ,max(todate) as todate,"
                    qry += "Code,Name,max(Cust_Add2) as [Customer Address],max(Cust_GST_No) as [Customer GST No],max([Customer State Code]) as [Customer State Code], MAX(Vendor_Tin_No) as Vendor_Customer_Tin_No,Document_No,"
                    If (ShowVatSeriesNo.Equals(1)) Then
                        qry += "(case when MAX(TSPL_TAX_MASTER.Excisable)='N' AND MAX(TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type)='E' THEN MAX(VAT_InvoiceNo) ELSE SourceDoc END) AS SourceDoc,"
                    Else
                        qry += "SourceDoc,"
                    End If
                    qry += "DocDate,Tax,TaxRate,sum(Document_Total) as Document_Total,sum(TaxBase) as TaxBase,sum(TaxAmt) TaxAmt, (sum(TaxBase) +sum(TaxAmt))TotalAmount,loc_code,Loc_Segment,'Automatic' as Tax_Calculation_Type from  ("
                Else
                    qry = "  select max(source) as Source ,max(Code) as Code,max(Name) as Name,max(Cust_Add2) as [Customer Address],max(Cust_GST_No) as [Customer GST No],max([Customer State Code]) as [Customer State Code], MAX(Vendor_Tin_No) as Vendor_Customer_Tin_No,Document_No ,SourceDoc,max(convert(varchar(10),DocDate,103)) as DocDate,sum(TaxBase) as TaxBase,sum(TaxAmt) as TaxAmt,max(loc_code) as loc_code,max(loc_segment) as Loc_Segment,'Automatic' as Tax_Calculation_Type from("
                End If

                qry += " select Source, 'Sale' as RType,'" + from_Date + "' as fromdate ,'" + to_Date + "' as todate ,Code, TSPL_CUSTOMER_MASTER.Tin_No as Vendor_Tin_No,Name,Document_No,SourceDoc,convert(varchar(10),DocDate,103) as DocDate,Tax,TaxRate,Document_Total,TaxBase,TaxAmt,loc_code,tspl_gl_segment_code.description as Loc_Segment,TSPL_CUSTOMER_MASTER.Add1+' '+(TSPL_CUSTOMER_MASTER.Add2) as Cust_Add2,(TSPL_CUSTOMER_MASTER.GSTNO) as Cust_GST_No,(select TSPL_STATE_MASTER.GST_STATE_Code from TSPL_STATE_MASTER where STATE_CODE=TSPL_CUSTOMER_MASTER.State) as [Customer State Code] from(  "


                '--------------for AR-Invoice------------------
                If (ChkAll.Checked Or chkARinvoice.Checked) Then
                    qry += "   select Source,  Code,Name,Document_No,SourceDoc,DocDate,Tax,TaxRate,Document_Total,TaxBase,TaxAmt,loc as loc_code from ("
                    For i As Integer = 1 To 10
                        Dim ii As String = i
                        qry += "select case when TSPL_Customer_Invoice_Head.Document_Type='I' and  Against_Sale_No <> '' then Against_Sale_No when TSPL_Customer_Invoice_Head.Document_Type='I' and AgainstScrap <> '' then AgainstScrap when TSPL_Customer_Invoice_Head.Document_Type='C' then  Against_Sale_Return_No else '' end as SourceDoc, case when TSPL_Customer_Invoice_Head.Document_Type='I' then 'AR-Invoice' when TSPL_Customer_Invoice_Head.Document_Type='C' then'AR-Credit'  else 'AR-Invoice' end as Source, Customer_Code as Code,Customer_Name as Name,(case when TSPL_Customer_Invoice_Head.AgainstScrap='' then  TSPL_Customer_Invoice_Head.Document_No else TSPL_Customer_Invoice_Head.Document_No end ) as Document_No,Document_Date as DocDate, TSPL_Customer_Invoice_Detail.TAX" + ii + " as Tax, TSPL_Customer_Invoice_Detail.TAX" + ii + "_Rate as TaxRate,Document_Total as Document_Total,(case when TSPL_Customer_Invoice_Head.Document_Type='I' then 1 else -1 end) * Tax" + ii + "_Base_Amt as TaxBase, (case when TSPL_Customer_Invoice_Head.Document_Type='I' then 1 else -1 end) * TSPL_Customer_Invoice_Detail.TAX" + ii + "_Amt as TaxAmt,(select substring(TSPL_Customer_Invoice_Detail.GL_Account_Code,len(TSPL_Customer_Invoice_Detail.GL_Account_Code)-2,4) from TSPL_Customer_Invoice_Detail where TSPL_Customer_Invoice_Detail.Document_No=TSPL_Customer_Invoice_Head.Document_No and SNo='1' )as loc   from TSPL_Customer_Invoice_Detail LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_Customer_Invoice_Detail.Document_No where TSPL_Customer_Invoice_Detail.TAX" + ii + "_Amt>0 and cast(TSPL_Customer_Invoice_Head.Document_Date as date) between '" & from_Date & "' and '" & to_Date & "'"
                        If Not ChkAll.Checked Then
                            qry += "  AND  (Against_Sale_Return_No  <> '' or Against_Sale_No <> '')"
                        End If

                        If i < 10 Then
                            qry += " Union All  "
                        End If

                    Next
                    qry += ") finalAR  where 2=2"
                    If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                        qry += "and finalAR.loc   in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    End If
                    If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                        qry += "  and finalAR.loc  in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                    End If

                End If


                ''By Accounts
                qry += Environment.NewLine + " Union all " + Environment.NewLine
                qry += "  select  Source, Code,Name,Document_No,SourceDoc,DocDate,Tax,TaxRate,Document_Total,TaxBase,TaxAmt,loc_code from ("
                qry += " select  TSPL_Customer_Invoice_Head.Document_No as SOURCEDOC  ,'Customer-Invoice' as source,  Customer_Code as Code,Customer_Name as Name,TSPL_Customer_Invoice_Head.Document_No as Document_No,Document_Date as DocDate, TSPL_GL_ACCOUNTS.Tax_Type as Tax,0 as TaxRate, (case when TSPL_Customer_Invoice_Head.Document_Type='C' then 1 else -1 end )*TSPL_Customer_Invoice_Detail.Total_Amount as Document_Total, 0 as  TaxBase, (case when TSPL_Customer_Invoice_Head.Document_Type='C' then -1 else 1 end )*TSPL_Customer_Invoice_Detail.Total_Amount	as TaxAmt "
                qry += ",TSPL_Customer_Invoice_Head.loc_code "
                qry += " from TSPL_Customer_Invoice_Detail "
                qry += " left outer join TSPL_Customer_Invoice_Head  on TSPL_Customer_Invoice_Head .Document_No =TSPL_Customer_Invoice_Detail.Document_No  "
                qry += " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_Customer_Invoice_Detail.GL_Account_Code  "
                qry += " where LEN(ISNULL(TSPL_GL_ACCOUNTS.Tax_Type,''))>0 AND TSPL_Customer_Invoice_Head.Status ='1'  and cast(TSPL_Customer_Invoice_Head.Document_Date as date) between '" & from_Date & "' and '" & to_Date & "'" + Environment.NewLine
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                End If

                qry += Environment.NewLine + " Union all " + Environment.NewLine
                qry += "select  TSPL_RECEIPT_HEADER.Receipt_No as SOURCEDOC  ,'Receipt' as source,  TSPL_RECEIPT_HEADER.Cust_Code as Code,TSPL_RECEIPT_HEADER.Customer_Name as Name,TSPL_RECEIPT_HEADER.Receipt_No as Document_No,TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, TSPL_GL_ACCOUNTS.Tax_Type as Tax,0 as TaxRate,  TSPL_RECEIPT_DETAIL.Applied_Amount as Document_Total, 0 as  TaxBase, TSPL_RECEIPT_DETAIL.Applied_Amount	as TaxAmt "
                qry += ",TSPL_RECEIPT_HEADER.Location_GL_Code as loc_code "
                qry += " from TSPL_RECEIPT_DETAIL "
                qry += " left outer join TSPL_RECEIPT_HEADER  on TSPL_RECEIPT_HEADER .Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No"
                qry += " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_RECEIPT_DETAIL.Account_Code  "
                qry += " where TSPL_RECEIPT_HEADER.Receipt_Type='M' and LEN(ISNULL(TSPL_GL_ACCOUNTS.Tax_Type,''))>0 AND TSPL_RECEIPT_HEADER.Posted ='Y' and cast(TSPL_RECEIPT_HEADER.Receipt_Date as date) between '" & from_Date & "' and '" & to_Date & "'"
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                End If

                qry += Environment.NewLine + " Union all " + Environment.NewLine
                qry += " select  TSPL_VCGL_Head.Document_No as SOURCEDOC  ,'VCGL' as source,  VC_Code as Code,VC_Name as Name,TSPL_VCGL_Head.Document_No as Document_No,CONVERT(datetime, Document_Date,103) as DocDate, TSPL_GL_ACCOUNTS.Tax_Type as Tax,0 as TaxRate, (case when TSPL_VCGL_Detail.Cr_Amount>0  then Cr_Amount else -1*Dr_Amount end ) as Document_Total, 0 as  TaxBase, (case when TSPL_VCGL_Detail.Cr_Amount>0  then -1*Cr_Amount else Dr_Amount end ) as TaxAmt "
                qry += ",TSPL_VCGL_Head.location_segment "
                qry += " from TSPL_VCGL_Detail "
                qry += " left outer join TSPL_VCGL_Head  on TSPL_VCGL_Head .Document_No =TSPL_VCGL_Detail.Document_No  "
                qry += " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VCGL_Detail.GL_Account_Code  "
                qry += " where LEN(ISNULL(TSPL_GL_ACCOUNTS.Tax_Type,''))>0 AND TSPL_VCGL_Head.Posting_Date IS NOT NULL  "
                qry += " and (TSPL_VCGL_Detail.Row_Type='Customer'  or (TSPL_VCGL_Detail.Row_Type='GL' and TSPL_GL_ACCOUNTS.Purchase_Sale_Type=2)) and cast(TSPL_VCGL_Head.Document_Date as date) between '" & from_Date & "' and '" & to_Date & "'"
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                End If

                qry += Environment.NewLine + " Union all " + Environment.NewLine
                qry += " select  TSPL_JOURNAL_MASTER.Voucher_No as SOURCEDOC  ,'GL-JE' as source,  '' as Code,'' as Name,TSPL_JOURNAL_MASTER.Voucher_No as Document_No,CONVERT(datetime, TSPL_JOURNAL_MASTER.Voucher_Date,103) as DocDate, TSPL_GL_ACCOUNTS.Tax_Type as Tax,0 as TaxRate,-1*  TSPL_JOURNAL_DETAILS.Amount as Document_Total, 0 as  TaxBase,-1*TSPL_JOURNAL_DETAILS.Amount as TaxAmt "
                qry += ",TSPL_JOURNAL_MASTER.segment_code "
                qry += " from TSPL_JOURNAL_DETAILS "
                qry += " left outer join TSPL_JOURNAL_MASTER  on TSPL_JOURNAL_MASTER .Voucher_No =TSPL_JOURNAL_DETAILS.Voucher_No  "
                qry += " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_JOURNAL_DETAILS.Account_code  "
                qry += " where LEN(ISNULL(TSPL_GL_ACCOUNTS.Tax_Type,''))>0 AND TSPL_JOURNAL_MASTER.Authorized ='A' and TSPL_GL_ACCOUNTS.Purchase_Sale_Type=2 and TSPL_JOURNAL_MASTER.Source_Code='GL-JE' and cast(TSPL_JOURNAL_MASTER.Voucher_Date as date) between '" & from_Date & "' and '" & to_Date & "'"
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    qry += "  and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (select Loc_Segment_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                End If

                ''End By Accounts
                qry += Environment.NewLine + "  ) ExtraData" + Environment.NewLine
                qry += " )abc LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=abc.Code left outer join tspl_gl_segment_code on tspl_gl_segment_code.segment_code=abc.loc_code and tspl_gl_segment_code.seg_no=7 where 2=2 and convert(date,DocDate,103) >= convert(date,'" + clsCommon.GetPrintDate(from_Date, "dd/MM/yyyy") + "',103) and convert(date,DocDate,103) <= convert(date,'" + clsCommon.GetPrintDate(to_Date, "dd/MM/yyyy") + "',103) "


                If chkSelectTax.IsChecked AndAlso cgvTaxCode.CheckedValue.Count > 0 Then
                    qry += " and tax in  (" + clsCommon.GetMulcallString(cgvTaxCode.CheckedValue) + ")  "
                End If

                If chkRateselect.IsChecked Then

                    If cgvRate.CheckedValue.Count > 0 Then


                        qry += " and TaxRate in  (" + clsCommon.GetMulcallString(cgvRate.CheckedValue) + ")  "
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Tax Rate", Me.Text)
                        Exit Sub
                    End If
                End If

                If rdoSale.IsChecked = True Then
                    qry += " )final "
                    '====Sanjeet(UDL)17/11/2016======
                    If (ShowVatSeriesNo.Equals(1)) Then
                        qry += "left outer join TSPL_SD_SALE_INVOICE_HEAD on final.SourceDoc=TSPL_SD_SALE_INVOICE_HEAD.Document_Code left outer join TSPL_TAX_MASTER on " & _
                                 "final.Tax=TSPL_TAX_MASTER.Tax_Code "
                    End If
                    '=====================
                    qry += " where  2=2 " ''final.TaxRate<>0  "

                    If (Chksummary.Checked = False) Then
                        qry += " and final.TaxRate <>0  group by Tax,Code,Name,Document_No,SourceDoc,DocDate,TaxRate,Source,loc_code,Loc_Segment"
                    Else
                        qry += "  group by Document_No ,SourceDoc,loc_code,Loc_Segment  "
                    End If
                End If


            ElseIf rdoTransfer.CheckState = CheckState.Checked Then
                If (Chksummary.Checked = False) Then

                    qry = " select  Source, "
                    qry += "  '" + taxratefilter + "' as TAXRATEFILTER,'" + loccode + "' as LOCATIONFILTER ,'" + tacxodefilter + "' as TAXCODEFILTER, max(RType) as RType,max(fromdate) as fromdate ,max(todate) as todate,"
                    qry += "Code,Name, MAX(Vendor_Tin_No) as Vendor_Customer_Tin_No,Document_No,SourceDoc,"
                    qry += "DocDate,Tax,TaxRate,sum(Document_Total) as Document_Total,sum(TaxBase) as TaxBase,sum(TaxAmt) TaxAmt, (sum(TaxBase) +sum(TaxAmt))TotalAmount,loc_code,max(Loc_Segment) as Loc_Segment,'Automatic' as Tax_Calculation_Type from  ("
                Else
                    qry = "  select max(source) as Source ,max(Code) as Code,max(Name) as Name, MAX(Vendor_Tin_No) as Vendor_Customer_Tin_No,Document_No ,SourceDoc,max(convert(varchar(10),DocDate,103)) as DocDate,sum(TaxBase) as TaxBase,sum(TaxAmt) as TaxAmt,max(loc_code) as loc_code,max(loc_segment) as Loc_Segment,'Automatic' as Tax_Calculation_Type from("
                End If

                qry += " select Source, 'Sale' as RType,'" + from_Date + "' as fromdate ,'" + to_Date + "' as todate ,Code, '' as Vendor_Tin_No,Name,Document_No,SourceDoc,convert(varchar(10),DocDate,103) as DocDate,Tax,TaxRate,Document_Total,TaxBase,TaxAmt,loc_code,'' as Loc_Segment from(  "


                '--------------for Transfer------------------
                If (ChkAll.Checked Or chkTransfer.Checked) Then
                    qry += "   select Source,  Code,Name,Document_No,SourceDoc,DocDate,Tax,TaxRate,Document_Total,TaxBase,TaxAmt,loc as loc_code from ("
                    For i As Integer = 1 To 10
                        Dim ii As String = i
                        If ChkAll.Checked Then
                            qry += " select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as SourceDoc, 'CSA Transfer' as Source, '' as Code,'' as Name,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Document_No, Transfer_Date as DocDate, TSPL_CSA_TRANSFER_DETAIL.TAX" & i & " as Tax, TSPL_CSA_TRANSFER_DETAIL.TAX" & i & "_Rate as TaxRate,TSPL_CSA_TRANSFER_HEAD.Document_Amount as Document_Total," & _
                              " -1 * TSPL_CSA_TRANSFER_DETAIL.Tax" & i & "_Base_Amt as TaxBase,  -1 * TSPL_CSA_TRANSFER_DETAIL.TAX" & i & "_Amt as TaxAmt,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Loc " & _
                              " from TSPL_CSA_TRANSFER_DETAIL LEFT OUTER JOIN TSPL_CSA_TRANSFER_HEAD ON TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE " & _
                              " where TSPL_CSA_TRANSFER_DETAIL.TAX" & i & "_Amt > 0  and cast(TSPL_CSA_TRANSFER_HEAD.Transfer_Date as date) between '" & from_Date & "' and '" & to_Date & "' and TSPL_CSA_TRANSFER_HEAD.Status=1 " & _
                              " union all " & _
                              " select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as SourceDoc, 'CSA Transfer Return' as Source, '' as Code,'' as Name,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Document_No, " & _
                              " Transfer_Date as DocDate, TSPL_SD_SALE_RETURN_DETAIL.TAX" & i & " as Tax, TSPL_SD_SALE_RETURN_DETAIL.TAX" & i & "_Rate as TaxRate,TSPL_CSA_TRANSFER_HEAD.Document_Amount as Document_Total, " & _
                              " -1 * TSPL_SD_SALE_RETURN_DETAIL.Tax" & i & "_Base_Amt as TaxBase,  -1 * TSPL_SD_SALE_RETURN_DETAIL.TAX" & i & "_Amt as TaxAmt,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Loc from TSPL_SD_SALE_RETURN_DETAIL inner join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code " & _
                              " inner join TSPL_CSA_TRANSFER_HEAD on TSPL_SD_SALE_RETURN_DETAIL.Transfer_No=TSPL_CSA_TRANSFER_HEAD.DOC_CODE " & _
                              " where Return_Type is not null and Trans_Type='CSA' and TSPL_SD_SALE_RETURN_DETAIL.TAX" & i & "_Amt > 0  " & _
                              " and cast(TSPL_SD_SALE_RETURN_HEAD.Document_Date as date) between '" & from_Date & "' and '" & to_Date & "' and TSPL_SD_SALE_RETURN_HEAD.Status=1 "
                        End If

                        If chkTransfer.Checked Or ChkAll.Checked Then
                            qry += " " & If(ChkAll.Checked = True, "Union All", "") & " select TSPL_TRANSFER_ORDER_HEAD.Document_No as SourceDoc, 'Transfer' as Source, '' as Code,'' as Name,TSPL_TRANSFER_ORDER_HEAD.Document_No as Document_No," & _
                                   " Document_Date as DocDate, TSPL_TRANSFER_ORDER_DETAIL.TAX" & i & " as Tax, TSPL_TRANSFER_ORDER_DETAIL.TAX" & i & "_Rate as TaxRate,TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as Document_Total," & _
                                   " (case when len(coalesce(TSPL_TRANSFER_ORDER_HEAD.TransferOutNo,''))>0 then 1 else -1 end) * TSPL_TRANSFER_ORDER_DETAIL.Tax" & i & "_Base_Amt as TaxBase, " & _
                                   " (case when len(coalesce(TSPL_TRANSFER_ORDER_HEAD.TransferOutNo,''))>0 then 1 else -1 end) * TSPL_TRANSFER_ORDER_DETAIL.TAX" & i & "_Amt as TaxAmt,TSPL_TRANSFER_ORDER_HEAD.From_Location as Loc " & _
                                   " from TSPL_TRANSFER_ORDER_DETAIL LEFT OUTER JOIN TSPL_TRANSFER_ORDER_HEAD ON TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
                                   " where TSPL_TRANSFER_ORDER_DETAIL.TAX" & i & "_Amt > 0 " & _
                                   " and cast(TSPL_TRANSFER_ORDER_HEAD.Document_Date as date) between '" & from_Date & "' and '" & to_Date & "' and TSPL_TRANSFER_ORDER_HEAD.Status=1" & Environment.NewLine & _
                                   " union all " & Environment.NewLine & _
                                   " select TSPL_TRANSFER_ORDER_HEAD.Document_No as SourceDoc, 'Transfer Return' as Source, '' as Code,'' as Name,TSPL_TRANSFER_RETURN.Document_No as Document_No, " & _
                                   " TSPL_TRANSFER_RETURN.Document_Date as DocDate, TSPL_TRANSFER_ORDER_DETAIL.TAX" & i & " as Tax, TSPL_TRANSFER_ORDER_DETAIL.TAX" & i & "_Rate as TaxRate,TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as Document_Total, (case when len(coalesce(TSPL_TRANSFER_ORDER_HEAD.TransferOutNo,''))>0 then -1 else 1 end) * TSPL_TRANSFER_ORDER_DETAIL.Tax" & i & "_Base_Amt as TaxBase,  (case when len(coalesce(TSPL_TRANSFER_ORDER_HEAD.TransferOutNo,''))>0 then -1 else 1 end) * TSPL_TRANSFER_ORDER_DETAIL.TAX" & i & "_Amt as TaxAmt,TSPL_TRANSFER_ORDER_HEAD.From_Location as Loc  from TSPL_TRANSFER_ORDER_DETAIL " & _
                                   " LEFT OUTER JOIN TSPL_TRANSFER_ORDER_HEAD ON TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
                                   " inner join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_RETURN.Transfer_No where TSPL_TRANSFER_ORDER_DETAIL.TAX" & i & "_Amt > 0  " & _
                                   " and cast(TSPL_TRANSFER_RETURN.Document_Date as date) between '" & from_Date & "' and '" & to_Date & "'"
                        End If

                        'If Not ChkAll.Checked Then
                        '    qry += "  AND  (Against_Sale_Return_No  <> '' or Against_Sale_No <> '')"
                        'End If

                        If i < 10 Then
                            qry += " Union All  "
                        End If

                    Next
                    qry += ") finalAR  where 2=2"
                    If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                        qry += "and finalAR.loc   in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    End If
                    If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                        qry += "  and finalAR.loc  in (select Location_Code from TSPL_LOCATION_MASTER where State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) )"
                    End If
                Else
                    Throw New Exception("Select All or Transfer checkbox option")
                End If
                qry += " )final "

                qry += " where  2=2 "

                If (Chksummary.Checked = False) Then
                    qry += " and final.TaxRate <>0) as t3  group by Tax,Code,Name,Document_No,SourceDoc,DocDate,TaxRate,Source,loc_code"
                Else
                    qry += ") as t3 group by Document_No ,SourceDoc,loc_code  "
                End If
            End If
            '----------By Monika----------------BM00000003030-----04/07/2014---------------------
            Dim query As String = ""
            If rdoPur.IsChecked Then
                query = "select com1.*,(select ','+xx.Description from (select distinct a.Vendor_Code,TSPL_ITEM_MASTER.Cheapter_Heads,TSPL_CHAPTER_HEAD.Description from TSPL_ITEM_MASTER left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads left outer join (select distinct TSPL_PI_HEAD.vendor_invoice_no,TSPL_PI_HEAD.Vendor_Code,TSPL_PI_DETAIL.Item_Code from TSPL_PI_HEAD left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No) a on a.Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_ITEM_MASTER.Item_Code in (select TSPL_PI_DETAIL.Item_Code from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No and tspl_pi_head.vendor_invoice_no=com1.Document_No) and a.Vendor_Code=com1.code and a.vendor_invoice_no=com1.Document_No)xx for XML path('')) as commodity from (" + qry + ") com1"
                qry = query
                If Chksummary.Checked Then
                    '  qry += "  order by com1.DocDate ,com1.[SOURCE DOC No]"
                End If
            ElseIf rdoSale.IsChecked Then
                query = "select com1.*,(case when (select 'y' from TSPL_SD_SALE_INVOICE_HEAD where Document_Code=com1.sourcedoc)='y' then (select ','+xx.Description from (select distinct TSPL_ITEM_MASTER.Cheapter_Heads,TSPL_CHAPTER_HEAD.Description from TSPL_ITEM_MASTER left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads left outer join (select distinct TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code) a on a.Item_Code=TSPL_ITEM_MASTER.Item_Code where a.Document_Code=com1.sourcedoc)xx for XML path('')) else (select ','+xx.Description from (select distinct TSPL_ITEM_MASTER.Cheapter_Heads,TSPL_CHAPTER_HEAD.Description from TSPL_ITEM_MASTER left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads left outer join (select distinct TSPL_SD_SALE_RETURN_HEAD.Document_Code,TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code) aa on aa.Item_Code=TSPL_ITEM_MASTER.Item_Code where aa.Document_Code=com1.sourcedoc)xx for XML path('')) end) as commodity from (" + qry + ") com1"
                qry = query
                If Chksummary.Checked Then
                    '   qry += "  order by com1.DocDate ,com1.Document_No"
                End If
            End If
            '--------------------------------------------------------------------------------------

            If Not Chksummary.Checked AndAlso rdoPur.IsChecked Then
                '' Anubhooti 24-Feb-2015 (Wrong joining TSPL_PI_HEAD.Vendor_Code = final.Code)
                'qry = "select final.*, TSPL_PI_HEAD.VehicleNo,TSPL_PI_HEAD.LR_No,TSPL_PI_HEAD.TransporterDesc, (TSPL_TRANSPORT_MASTER.Add1+TSPL_TRANSPORT_MASTER.Add2) TrasnporterAdd  from ( " + qry + ") final left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.Vendor_Code = final.Code left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id = TSPL_PI_HEAD.Transporter "
                qry = "select final.*, TSPL_PI_HEAD.VehicleNo,TSPL_PI_HEAD.LR_No,TSPL_PI_HEAD.TransporterDesc, (TSPL_TRANSPORT_MASTER.Add1+TSPL_TRANSPORT_MASTER.Add2) TrasnporterAdd  from ( " + qry + ") final LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No =final.[Source Doc No] LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No =TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id = TSPL_PI_HEAD.Transporter "
            End If
            ''RICHA AGARWAL TICKET NO BM00000008797
            Dim StrWhrmain As String = String.Empty
            If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                StrWhrmain = "  where isnull(TSPL_LOCATION_MASTER.State,'') in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + " ) "
            End If
            Dim mainqry As String = "select m.*,isnull(TSPL_LOCATION_MASTER.State,'') as State from(" & qry & " )as m left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =m.Loc_Code " & StrWhrmain & "  order by convert(date,DocDate,103) "
            ''richa agarwal 18 July,2019 UDL/18/07/19-000305 UDL/02/08/19-000310
            If rdoPur.IsChecked = True AndAlso ChkVendorInvoice.Checked = True AndAlso ChkItemWise.Checked = True Then
                mainqry = "select  Source , Code, Name, [Vendor Address] as [Vendor Address],[Vendor GST NO] as [Vendor GST NO],[Vendor GST State] as [Vendor GST State], Vendor_Customer_Tin_No, " & Environment.NewLine & _
" [Item/Sevices description] as [Item/Sevices description],[Vendor Doc No] as [Vendor Doc No],[Vendor DOC Date]  as [Vendor DOC Date] ,[Source Doc No] as [Source Doc No] , DocDate,[State of Vendor] as [State of Vendor],TSPL_LOCATION_MASTER_For_GSTIN.City_Code  as [Place of supply],[ITC nature] as [ITC nature],  " & Environment.NewLine & _
 "[HSN/SAC code] as [HSN/SAC code], Tax,TAX_Base_Amt as [Tax Base Amount],TaxRate, TaxAmt,[Total Amount] as [Total Amount],[Segment Code] as [Segment Code], Loc_Segment,VehicleNo,LR_No,TransporterDesc, TrasnporterAdd, z.State from (select 'Vendor-Invoice' as Source ,TSPL_PI_HEAD.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2 as [Vendor Address],tspl_vendor_master.GSTFinalNo as [Vendor GST NO],tspl_state_master.GST_STATE_Code as [Vendor GST State], TSPL_VENDOR_MASTER.Tin_No as Vendor_Customer_Tin_No, " & Environment.NewLine & _
                " TSPL_PI_DETAIL.Item_Desc as [Item/Sevices description],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Doc No],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [Vendor DOC Date] ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [Source Doc No] ,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as DocDate,tspl_state_master.STATE_NAME as [State of Vendor],TSPL_LOCATION_MASTER.Location_Desc  as [Place of supply],TSPL_PI_DETAIL .Category as [ITC nature],  " & Environment.NewLine & _
                " case when isnull(TSPL_PI_DETAIL.Row_Type,'')='Item' then TSPL_ITEM_MASTER.HSN_Code else TSPL_Additional_Charges.SAC_Code   end  as [HSN/SAC code], TSPL_PI_DETAIL.TAX1 + case when len(isnull(TSPL_PI_DETAIL.TAX2,''))>0 then ', '+isnull(TSPL_PI_DETAIL.TAX2,'') else '' end as Tax, TSPL_PI_DETAIL.TAX1_Base_Amt as TAX_Base_Amt,TSPL_PI_DETAIL.TAX1_Rate +TSPL_PI_DETAIL.TAX2_Rate +TSPL_PI_DETAIL.TAX3_Rate +TSPL_PI_DETAIL.TAX4_Rate as TaxRate,CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_PI_DETAIL.Total_Tax_Amt as TaxAmt,CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_PI_DETAIL.Item_Net_Amt as [Total Amount],TSPL_PI_HEAD.Bill_To_Location as [Segment Code],TSPL_GL_SEGMENT_CODE.description as Loc_Segment,TSPL_PI_HEAD.VehicleNo,TSPL_PI_HEAD.LR_No,TSPL_PI_HEAD.TransporterDesc, (TSPL_TRANSPORT_MASTER.Add1+TSPL_TRANSPORT_MASTER.Add2) TrasnporterAdd,TSPL_VENDOR_MASTER.State_Code as State " & Environment.NewLine & _
                " from TSPL_PI_DETAIL " & Environment.NewLine & _
                " left outer join TSPL_PI_HEAD on TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD.PI_No " & Environment.NewLine & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PI_HEAD.Vendor_Code " & Environment.NewLine & _
                " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VENDOR_MASTER.State_Code  " & Environment.NewLine & _
                " left outer join tspl_item_master on tspl_item_master.Item_Code= TSPL_PI_DETAIL.Item_Code  " & Environment.NewLine & _
                " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No =TSPL_PI_HEAD.PI_No " & Environment.NewLine & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_PI_HEAD.Bill_To_Location  " & Environment.NewLine & _
                " left outer join TSPL_GL_SEGMENT_CODE on TSPL_LOCATION_MASTER .Location_Code=TSPL_GL_SEGMENT_CODE.segment_code and TSPL_GL_SEGMENT_CODE.seg_no=7  " & Environment.NewLine & _
                " left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code =TSPL_PI_DETAIL.Item_Code  " & Environment.NewLine & _
                " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id = TSPL_PI_HEAD.Transporter  " & Environment.NewLine & _
                " where ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and cast(TSPL_VENDOR_INVOICE_HEAD.Posting_Date as date) between '" & from_Date & "' and '" & to_Date & "'  " & Environment.NewLine & _
                " and TSPL_VENDOR_INVOICE_HEAD.Total_Tax >0  " & Environment.NewLine & _
                " union all " & Environment.NewLine & _
                " select 'Vendor-Service' as Source ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as Name,TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2 as [Vendor Address],tspl_vendor_master.GSTFinalNo as [Vendor GST NO],tspl_state_master.GST_STATE_Code as [Vendor GST State], TSPL_VENDOR_MASTER.Tin_No as Vendor_Customer_Tin_No, " & Environment.NewLine & _
                " TSPL_VENDOR_INVOICE_DETAIL.AddChargeDesc  as [Item/Sevices description],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Doc No],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [Vendor DOC Date] ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [Source Doc No] ,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as DocDate,tspl_state_master.STATE_NAME as [State of Vendor],TSPL_LOCATION_MASTER.Location_Desc  as [Place of supply],'Service' as [ITC nature], TSPL_Additional_Charges.SAC_Code   as [HSN/SAC code],TSPL_VENDOR_INVOICE_DETAIL.TAX1 + case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX2,''))>0 then ', '+isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX2,'') else '' end as Tax, TSPL_VENDOR_INVOICE_DETAIL.TAX1_Base_Amt as TAX_Base_Amt,TSPL_VENDOR_INVOICE_DETAIL.TAX1_Rate +TSPL_VENDOR_INVOICE_DETAIL.TAX2_Rate +TSPL_VENDOR_INVOICE_DETAIL.TAX3_Rate +TSPL_VENDOR_INVOICE_DETAIL.TAX4_Rate as TaxRate,CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_VENDOR_INVOICE_DETAIL.Total_Tax  as TaxAmt,CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_VENDOR_INVOICE_DETAIL.Total_Amount as [Total Amount],TSPL_VENDOR_INVOICE_HEAD .Loc_Code as [Segment Code],TSPL_GL_SEGMENT_CODE.description as Loc_Segment,null as VehicleNo,null as LR_No,null as TransporterDesc, null as TrasnporterAdd,TSPL_VENDOR_MASTER.State_Code as State " & Environment.NewLine & _
                " from TSPL_VENDOR_INVOICE_DETAIL " & Environment.NewLine & _
                " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No  " & Environment.NewLine & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code " & Environment.NewLine & _
                " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " & Environment.NewLine & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code   " & Environment.NewLine & _
                " left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code =TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode  " & Environment.NewLine & _
                " left outer join TSPL_GL_SEGMENT_CODE on TSPL_LOCATION_MASTER .Location_Code=TSPL_GL_SEGMENT_CODE.segment_code and TSPL_GL_SEGMENT_CODE.seg_no=7  " & Environment.NewLine & _
                " where TSPL_VENDOR_INVOICE_HEAD.Invoice_Type ='VS' " & Environment.NewLine & _
                " and ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and cast(TSPL_VENDOR_INVOICE_HEAD.Posting_Date as date) between '" & from_Date & "' and '" & to_Date & "' and TSPL_VENDOR_INVOICE_HEAD.Total_Tax >0 " & Environment.NewLine & _
                " union all " & Environment.NewLine & _
                " select 'Vendor-Service' as Source ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as Name,TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2 as [Vendor Address],tspl_vendor_master.GSTFinalNo as [Vendor GST NO],tspl_state_master.GST_STATE_Code as [Vendor GST State], TSPL_VENDOR_MASTER.Tin_No as Vendor_Customer_Tin_No, " & Environment.NewLine & _
                " TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Desc  as [Item/Sevices description],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Doc No],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [Vendor DOC Date] ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [Source Doc No] ,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as DocDate,tspl_state_master.STATE_NAME as [State of Vendor],TSPL_LOCATION_MASTER.Location_Desc  as [Place of supply],'Service' as [ITC nature], TSPL_Additional_Charges.SAC_Code   as [HSN/SAC code],TSPL_VENDOR_INVOICE_DETAIL.TAX1 + case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX2,''))>0 then ', '+isnull(TSPL_VENDOR_INVOICE_DETAIL.TAX2,'') else '' end as Tax, TSPL_VENDOR_INVOICE_DETAIL.TAX1_Base_Amt as TAX_Base_Amt,TSPL_VENDOR_INVOICE_DETAIL.TAX1_Rate +TSPL_VENDOR_INVOICE_DETAIL.TAX2_Rate +TSPL_VENDOR_INVOICE_DETAIL.TAX3_Rate +TSPL_VENDOR_INVOICE_DETAIL.TAX4_Rate as TaxRate,CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_VENDOR_INVOICE_DETAIL.Total_Tax  as TaxAmt,CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_VENDOR_INVOICE_DETAIL.Total_Amount as [Total Amount],TSPL_VENDOR_INVOICE_HEAD .Loc_Code as [Segment Code],TSPL_GL_SEGMENT_CODE.description as Loc_Segment,null as VehicleNo,null as LR_No,null as TransporterDesc, null as TrasnporterAdd ,TSPL_VENDOR_MASTER.State_Code as State " & Environment.NewLine & _
                " from TSPL_VENDOR_INVOICE_DETAIL " & Environment.NewLine & _
                " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No " & Environment.NewLine & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code" & Environment.NewLine & _
                " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VENDOR_MASTER.State_Code" & Environment.NewLine & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code  " & Environment.NewLine & _
                " left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code =TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode " & Environment.NewLine & _
                " left outer join TSPL_GL_SEGMENT_CODE on TSPL_LOCATION_MASTER .Location_Code=TSPL_GL_SEGMENT_CODE.segment_code and TSPL_GL_SEGMENT_CODE.seg_no=7 " & Environment.NewLine & _
                " where (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then 'PI'  when (isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='BM-PR' and isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,'')<>'') then 'BMPR' WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') ='VS' THEN 'VSR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then 'PR' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then 'BMPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then 'MPI' when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then 'VCGL' when (isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No ,'')='' AND isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL ,'')='' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Invoice_Type,'') <>'VS' ) then 'DAP'  end) IN ('DAP') and" & Environment.NewLine & _
                " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and cast(TSPL_VENDOR_INVOICE_HEAD.Posting_Date as date) between '" & from_Date & "' and '" & to_Date & "' and TSPL_VENDOR_INVOICE_HEAD.Total_Tax >0" & Environment.NewLine & _
                " union all  " & Environment.NewLine & _
                " select 'Vendor-Invoice' as Source ,TSPL_PR_HEAD.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,TSPL_VENDOR_MASTER.Add1+' '+TSPL_VENDOR_MASTER.Add2 as [Vendor Address],tspl_vendor_master.GSTFinalNo as [Vendor GST NO],tspl_state_master.GST_STATE_Code as [Vendor GST State], TSPL_VENDOR_MASTER.Tin_No as Vendor_Customer_Tin_No," & Environment.NewLine & _
                " TSPL_PR_DETAIL.Item_Desc as [Item/Sevices description],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Doc No],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [Vendor DOC Date] ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [Source Doc No] ,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as DocDate,tspl_state_master.STATE_NAME as [State of Vendor],TSPL_LOCATION_MASTER.Location_Desc  as [Place of supply],TSPL_PR_DETAIL .Category as [ITC nature],  " & Environment.NewLine & _
                " case when isnull(TSPL_PR_DETAIL.Row_Type,'')='Item' then TSPL_ITEM_MASTER.HSN_Code else TSPL_Additional_Charges.SAC_Code   end  as [HSN/SAC code], TSPL_PR_DETAIL.TAX1 + case when len(isnull(TSPL_PR_DETAIL.TAX2,''))>0 then ', '+isnull(TSPL_PR_DETAIL.TAX2,'') else '' end as Tax, TSPL_PR_DETAIL.TAX1_Base_Amt  as TAX_Base_Amt,TSPL_PR_DETAIL.TAX1_Rate +TSPL_PR_DETAIL.TAX2_Rate +TSPL_PR_DETAIL.TAX3_Rate +TSPL_PR_DETAIL.TAX4_Rate as TaxRate, CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_PR_DETAIL.Total_Tax_Amt as TaxAmt,CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_PR_DETAIL.Item_Net_Amt as [Total Amount],TSPL_PR_HEAD.Bill_To_Location as [Segment Code],TSPL_GL_SEGMENT_CODE.description as Loc_Segment,TSPL_PR_HEAD.VehicleNo,NULL AS LR_No,NULL AS TransporterDesc, NULL AS  TrasnporterAdd,TSPL_VENDOR_MASTER.State_Code as State  " & Environment.NewLine & _
                " from TSPL_PR_DETAIL " & Environment.NewLine & _
                " left outer join TSPL_PR_HEAD on TSPL_PR_DETAIL.PR_No =TSPL_PR_HEAD.PR_No " & Environment.NewLine & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PR_HEAD.Vendor_Code " & Environment.NewLine & _
                " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VENDOR_MASTER.State_Code" & Environment.NewLine & _
                " left outer join tspl_item_master on tspl_item_master.Item_Code= TSPL_PR_DETAIL.Item_Code " & Environment.NewLine & _
                " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  =TSPL_PR_HEAD.PR_No" & Environment.NewLine & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_PR_HEAD.Bill_To_Location " & Environment.NewLine & _
                " left outer join TSPL_GL_SEGMENT_CODE on TSPL_LOCATION_MASTER .Location_Code=TSPL_GL_SEGMENT_CODE.segment_code and TSPL_GL_SEGMENT_CODE.seg_no=7 " & Environment.NewLine & _
                " left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code =TSPL_PR_DETAIL.Item_Code " & Environment.NewLine & _
                " where ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and cast(TSPL_VENDOR_INVOICE_HEAD.Posting_Date as date) between '" & from_Date & "' and '" & to_Date & "' " & Environment.NewLine & _
                " and TSPL_VENDOR_INVOICE_HEAD.Total_Tax >0" & Environment.NewLine & _
                "  ) z  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_For_GSTIN on TSPL_LOCATION_MASTER_For_GSTIN.Location_Code =z.[Segment Code]  where 1=1 "
                If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                    mainqry += "and z.[Segment Code]   in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkStateselect.IsChecked AndAlso cgvState.CheckedValue.Count >= 0 Then
                    mainqry += "  and z.state  in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + "  )"
                End If
                mainqry += "  order by convert(date,z.DocDate,103)"

            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(mainqry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            ElseIf Chksummary.Checked = False Then
                If rdoPur.IsChecked = True AndAlso ChkVendorInvoice.Checked = True AndAlso ChkItemWise.Checked = True Then
                    gv.MasterTemplate.SummaryRowsBottom.Clear()
                    gv.DataSource = dt
                    RadPageView1.SelectedPage = RadPageViewPage2
                    For ii As Integer = 0 To gv.Columns.Count - 1
                        gv.Columns(ii).ReadOnly = True
                        gv.Columns(ii).Width = "100"
                    Next

                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim item2 As New GridViewSummaryItem("TaxAmt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    Dim item3 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item3)
                    gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    gv.BestFitColumns()
                    ReStoreGridLayout()
                Else
                    gvformat(dt)
                    ReStoreGridLayout()
                End If
               
            ElseIf Chksummary.Checked = True Then
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                gv.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                For ii As Integer = 0 To gv.Columns.Count - 1
                    gv.Columns(ii).ReadOnly = True
                    gv.Columns(ii).Width = "100"
                Next
                gv.Columns("TaxBase").IsVisible = False
                gv.Columns("Vendor_Customer_Tin_No").HeaderText = IIf(rdoPur.IsChecked, "Vendor Tin No", "Customer Tin No")
                gv.Columns("Loc_Segment").HeaderText = "Location Segment"
                gv.Columns("Loc_Code").HeaderText = "Segment Code"
                gv.Columns("Tax_Calculation_Type").HeaderText = "Tax Calculation Type"
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("TaxBase", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("TaxAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv.BestFitColumns()
                ReStoreGridLayout()
            End If
            StrWhrmain = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.TaxTracking & "'"))
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If chkSelectTax.IsChecked Then
                strtemp = ""
                For Each Str As String In cgvTaxCode.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Tax Code : " + strtemp)
            End If
            If chkStateselect.IsChecked Then
                strtemp = ""
                For Each Str As String In cgvState.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("State : " + strtemp)
            End If
            If chkRateselect.IsChecked Then
                strtemp = ""
                For Each Str As String In cgvRate.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Tax Rate : " + strtemp)
            End If
            If chkLocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Tax Tracking Report", gv, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Tax Tracking Report", gv, arrHeader, "Tax Tracking Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Sub gvformat(ByVal dt As DataTable)
        Try

            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.DataSource = dt
            RadPageView1.SelectedPage = RadPageViewPage2
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            gv.AllowAddNewRow = False
            gv.TableElement.TableHeaderHeight = 40
            gv.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = False
            Next




            'DocumentNo ,DocumentDate ,Route ,RouteDescription ,Salesmancode ,SalesmanDesc,VehicleNo
            gv.Columns("Source").IsVisible = True
            gv.Columns("Source").Width = 100
            gv.Columns("Source").HeaderText = "Source"

            gv.Columns("State").IsVisible = True
            gv.Columns("State").Width = 100
            gv.Columns("State").HeaderText = "State"

            'gv.Columns("TAXRATEFILTER").IsVisible = False


            'gv.Columns("LOCATIONFILTER").IsVisible = False
            'gv.Columns("TAXCODEFILTER").IsVisible = False
            'gv.Columns("fromdate").IsVisible = False
            'gv.Columns("todate").IsVisible = False


            'gv.Columns("RType").IsVisible = True
            'gv.Columns("RType").Width = 200
            'gv.Columns("RType").HeaderText = "R Type"

            gv.Columns("Code").IsVisible = True
            gv.Columns("Code").Width = 70
            gv.Columns("Code").HeaderText = "Code"


            gv.Columns("Name").IsVisible = True
            gv.Columns("Name").Width = 200
            gv.Columns("Name").HeaderText = "Name"

            gv.Columns("Document_No").IsVisible = True
            gv.Columns("Document_No").Width = 100
            If (rdoPur.IsChecked) Then
                gv.Columns("Document_No").HeaderText = "Vendor Doc No"
            Else
                gv.Columns("Document_No").HeaderText = "Customer Doc No"
            End If

            gv.Columns("Vendor_Customer_Tin_No").IsVisible = True
            gv.Columns("Vendor_Customer_Tin_No").Width = 100
            If (rdoPur.IsChecked) Then
                gv.Columns("Source Doc No").IsVisible = True
                gv.Columns("Source Doc No").Width = 100
                gv.Columns("Source Doc No").HeaderText = "Source Doc No"

                gv.Columns("Vendor Address").IsVisible = True
                gv.Columns("Vendor Address").Width = 100
                gv.Columns("Vendor Address").HeaderText = "Vendor Address"

                gv.Columns("Vendor GST NO").IsVisible = True
                gv.Columns("Vendor GST NO").Width = 100
                gv.Columns("Vendor GST NO").HeaderText = "Vendor GST NO"

                gv.Columns("Vendor GST State").IsVisible = True
                gv.Columns("Vendor GST State").Width = 100
                gv.Columns("Vendor GST State").HeaderText = "Vendor GST State"

                gv.Columns("Vendor_Customer_Tin_No").HeaderText = "Vendor Tin No"

            Else
                gv.Columns("SourceDoc").IsVisible = True
                gv.Columns("SourceDoc").Width = 100
                gv.Columns("SourceDoc").HeaderText = "Source Doc No"

                gv.Columns("Customer Address").IsVisible = True
                gv.Columns("Customer Address").Width = 100
                gv.Columns("Customer Address").HeaderText = "Customer Address"

                gv.Columns("Customer GST No").IsVisible = True
                gv.Columns("Customer GST No").Width = 100
                gv.Columns("Customer GST No").HeaderText = "Customer GST NO"

                gv.Columns("Customer State Code").IsVisible = True
                gv.Columns("Customer State Code").Width = 100
                gv.Columns("Customer State Code").HeaderText = "Customer State Code"

                gv.Columns("Vendor_Customer_Tin_No").HeaderText = "Customer Tin No"
            End If
            gv.Columns("DocDate").IsVisible = True
            gv.Columns("DocDate").Width = 100
            gv.Columns("DocDate").HeaderText = "DocDate"

            gv.Columns("Tax").IsVisible = True
            gv.Columns("Tax").Width = 100
            gv.Columns("Tax").HeaderText = "Tax"
            'item1 = New GridViewSummaryItem("Tax", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)

            gv.Columns("TaxRate").IsVisible = True
            gv.Columns("TaxRate").Width = 100
            gv.Columns("TaxRate").HeaderText = "TaxRate"
            gv.Columns("TaxRate").FormatString = "{0:F2}"
            ' item1 = New GridViewSummaryItem("TaxRate", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)

            gv.Columns("Document_Total").IsVisible = False
            gv.Columns("Document_Total").Width = 100
            gv.Columns("Document_Total").HeaderText = "Document_Total"
            'item1 = New GridViewSummaryItem("Document_Total", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)

            'If Chksummary.Checked = False Then

            gv.Columns("TaxBase").IsVisible = True
            gv.Columns("TaxBase").Width = 100
            gv.Columns("TaxBase").HeaderText = "TaxBase"
            gv.Columns("TaxBase").FormatString = "{0:F2}"

            gv.Columns("TotalAmount").IsVisible = True
            gv.Columns("TotalAmount").Width = 100
            gv.Columns("TotalAmount").HeaderText = "Total Amount"
            gv.Columns("TotalAmount").FormatString = "{0:F2}"

            ' item1 = New GridViewSummaryItem("TaxBase", "{0:F2}", GridAggregateFunction.Sum)
            ' summaryRowItem.Add(item1)
            'End If

            gv.Columns("TaxAmt").IsVisible = True
            gv.Columns("TaxAmt").Width = 100
            gv.Columns("TaxAmt").HeaderText = "TaxAmt"
            gv.Columns("TaxAmt").FormatString = "{0:F2}"
            'item1 = New GridViewSummaryItem("TaxAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)



            If Chksummary.Checked = False AndAlso rdoPur.IsChecked Then
                gv.Columns("VehicleNo").IsVisible = True
                gv.Columns("VehicleNo").Width = 100
                gv.Columns("VehicleNo").HeaderText = "Vehicle No"

                gv.Columns("LR_No").IsVisible = True
                gv.Columns("LR_No").Width = 100
                gv.Columns("LR_No").HeaderText = "LR No"

                gv.Columns("TransporterDesc").IsVisible = True
                gv.Columns("TransporterDesc").Width = 100
                gv.Columns("TransporterDesc").HeaderText = "Transporter Name"

                gv.Columns("TrasnporterAdd").IsVisible = True
                gv.Columns("TrasnporterAdd").Width = 100
                gv.Columns("TrasnporterAdd").HeaderText = "Tranporter Address"
            End If




            Try
                gv.Columns("commodity").IsVisible = True
                gv.Columns("commodity").Width = 100
                gv.Columns("commodity").HeaderText = "Commodity"
            Catch exx As Exception
            End Try

            gv.Columns("loc_segment").IsVisible = True
            gv.Columns("loc_segment").Width = 100
            gv.Columns("loc_segment").HeaderText = "Location Segment"

            gv.Columns("loc_code").IsVisible = True
            gv.Columns("loc_code").Width = 100
            gv.Columns("loc_code").HeaderText = "Segment Code"


            gv.Columns("Tax_Calculation_Type").IsVisible = True
            gv.Columns("Tax_Calculation_Type").Width = 100
            gv.Columns("Tax_Calculation_Type").HeaderText = "Tax Calculation Type"

            'gv.GroupDescriptors.Add(New GridGroupByExpression("Tax as Tax format ""{0}: {2}"" Group By Tax"))
            'gv.GroupDescriptors.Add(New GridGroupByExpression("TaxRate as TaxRate format ""{0}: {2}"" Group By TaxRate"))

            gv.MasterTemplate.ExpandAllGroups()
            gv.ShowGroupPanel = True
            gv.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            If Not (rdoPur.IsChecked) Then
                Dim item1 As New GridViewSummaryItem("TaxBase", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)

                Dim item3 As New GridViewSummaryItem("TotalAmount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
            End If

            Dim item2 As New GridViewSummaryItem("TaxAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


            'For j As Integer = 0 To arritem.Count - 1
            '    gv.Columns(arritem.Item(j)).IsVisible = True
            '    gv.Columns(arritem.Item(j)).Width = 100
            '    gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

            'Next

            'For ii As Integer = 12 To gv.Columns.Count - 1
            '    intCount = intCount + 1
            '    strItemCode = gv.Columns(ii).FieldName
            '    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(item16)
            'Next





            'For j As Integer = 0 To arritem.Count - 1
            '    gv.Columns(arritem.Item(j)).IsVisible = True
            '    gv.Columns(arritem.Item(j)).Width = 100
            '    gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

            'Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    ' ''Sub PrintData()
    ' ''    Try

    ' ''        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    ' ''            common.clsCommon.MyMessageBoxShow("Please Select at least one Location.")
    ' ''            Return
    ' ''        End If

    ' ''        Dim qry As String

    ' ''        If rdoPur.IsChecked = True Then


    ' ''            qry = "select 'Purchase' as RType,'" + txtFromDate.Value + "' as fromdate ,'" + txtToDate.Value + "' as todate ,Code,Name,Document_No,convert(varchar(10),DocDate,103) as DocDate,Tax,TaxRate,Document_Total,TaxBase,TaxAmt from( " & _
    ' ''                  " select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX1 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax1_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt	as   TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>''  "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += " select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX2 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX2_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax2_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>''  " & _
    ' ''                      " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")" & _
    ' ''                    " Union All " & _
    ' ''                  " select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX3 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX3_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax3_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt>0  AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += " Union All "
    ' ''            qry += " select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX4 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX4_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax4_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>''  "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += " Union All "
    ' ''            qry += " select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX5 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX5_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax5_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD  left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code where TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt>0  AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += "  Union  All "
    ' ''            qry += "   select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX6 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX6_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax6_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt>0  AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "   select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX7 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX7_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax7_BAmount   TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt>0  AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "  select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX8 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX8_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax8_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code where TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt>0  AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "  select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX9 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX9_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax9_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1' left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt>0  AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "    select Vendor_Code as Code,Vendor_Name as Name,Vendor_Invoice_No as Document_No,Vendor_Invoice_Date as DocDate,TSPL_VENDOR_INVOICE_HEAD.TAX10 as Tax,TSPL_VENDOR_INVOICE_HEAD.TAX10_Rate as TaxRate,Document_Total,TSPL_VENDOR_INVOICE_HEAD.Tax10_BAmount TaxBase,TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt	as TaxAmt  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No and  TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No='1'  left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt>0  AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If
    ' ''            qry += " )abc where 2=2 and convert(date,DocDate,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,DocDate,103) <= convert(date,'" + txtToDate.Value + "',103) "

    ' ''        ElseIf rdoSale.IsChecked = True Then

    ' ''            ''                qry = " select max(RType) as RType,max(fromdate) as fromdate ,max(todate) as todate,Code,Name,Document_No,DocDate,Tax,TaxRate,sum(Document_Total) as Document_Total,sum(TaxBase) as TaxBase,sum(TaxAmt) TaxAmt from  (" & _
    ' ''            ''                      "     select 'Sale' as RType,'" + txtFromDate.Value + "' as fromdate ,'" + txtToDate.Value + "' as todate ,Code,Name,Document_No,convert(varchar(10),DocDate,103) as DocDate,Tax,TaxRate,Document_Total,TaxBase,TaxAmt from(  " & _
    ' ''            ''                      " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX1 as Tax,TAX1_Rate as TaxRate,Doc_Amt as Document_Total,TAX1_Base_Amt as TaxBase	,TAX1_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX1_Amt>0  " & _
    ' ''            ''                      " union all " & _
    ' ''            ''                      "  select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX2 as Tax,TAX2_Rate as TaxRate,Doc_Amt as Document_Total,TAX2_Base_Amt as TaxBase	,TAX2_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX2_Amt>0   " & _
    ' ''            ''                      " union all " & _
    ' ''            ''                      " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX3 as Tax,TAX3_Rate as TaxRate,Doc_Amt as Document_Total,TAX3_Base_Amt as TaxBase	,TAX3_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX3_Amt>0   " & _
    ' ''            ''                      " union all " & _
    ' ''            ''                      " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX4 as Tax,TAX4_Rate as TaxRate,Doc_Amt as Document_Total,TAX4_Base_Amt as TaxBase	,TAX4_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX4_Amt>0   " & _
    ' ''            ''                      " union all " & _
    ' ''            ''                      " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX5 as Tax,TAX5_Rate as TaxRate,Doc_Amt as Document_Total,TAX5_Base_Amt as TaxBase	,TAX5_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX5_Amt>0   " & _
    ' ''            ''                      " union all " & _
    ' ''            ''                      " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX6 as Tax,TAX6_Rate as TaxRate,Doc_Amt as Document_Total,TAX6_Base_Amt as TaxBase	,TAX6_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX6_Amt>0   " & _
    ' ''            ''                      " union all " & _
    ' ''            ''                      " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX7 as Tax,TAX7_Rate as TaxRate,Doc_Amt as Document_Total,TAX7_Base_Amt as TaxBase	,TAX7_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX7_Amt>0   " & _
    ' ''            ''               " union all " & _
    ' ''            ''  " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX8 as Tax,TAX1_Rate as TaxRate,Doc_Amt as Document_Total,TAX8_Base_Amt as TaxBase	,TAX8_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX8_Amt>0   " & _
    ' ''            ''              " union all " & _
    ' ''            ''  " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX9 as Tax,TAX9_Rate as TaxRate,Doc_Amt as Document_Total,TAX9_Base_Amt as TaxBase	,TAX9_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX9_Amt>0   " & _
    ' ''            ''               " union all " & _
    ' ''            ''  " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX10 as Tax,TAX10_Rate as TaxRate,Doc_Amt as Document_Total,TAX10_Base_Amt as TaxBase	,TAX10_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX10_Amt>0   " & _
    ' ''            ''                " union all " & _
    ' ''            ''                " select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX1 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX1_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX1_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX1_Amt>0  " & _
    ' ''            ''             " union all " & _
    ' ''            '' "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX2 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX2_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX2_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX2_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX2_Amt>0  " & _
    ' ''            ''            " union all " & _
    ' ''            '' " select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX3 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX3_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX3_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX3_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX3_Amt>0  " & _
    ' ''            ''              " union all " & _
    ' ''            '' "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX4 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX4_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX4_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX4_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX4_Amt>0  " & _
    ' ''            ''             " union all " & _
    ' ''            ''"   select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX5 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX5_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX5_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX5_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX5_Amt>0  " & _
    ' ''            ''              " union all " & _
    ' ''            '' "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX6 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX6_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX6_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX6_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX6_Amt>0  " & _
    ' ''            ''            " union all " & _
    ' ''            ''"   select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX7 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX7_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX7_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX7_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX7_Amt>0  " & _
    ' ''            ''             " union all " & _
    ' ''            '' "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX8 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX8_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX8_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX8_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX8_Amt>0  " & _
    ' ''            ''              " union all " & _
    ' ''            '' "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX9 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX9_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX9_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX9_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX9_Amt>0  " & _
    ' ''            ''             " union all " & _
    ' ''            '' "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX10 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX10_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX10_Assessable_Amt as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX10_Amt as TaxAmt  from TSPL_SALE_INVOICE_HEAD   " & _
    ' ''            ''" Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No where TSPL_SALE_INVOICE_DETAIL.TAX10_Amt>0 )abc   where 2=2 and convert(date,DocDate,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,DocDate,103) <= convert(date,'" + txtToDate.Value + "',103)   "




    ' ''            qry = " select max(RType) as RType,max(fromdate) as fromdate ,max(todate) as todate,Code,Name,Document_No,DocDate,Tax,TaxRate,sum(Document_Total) as Document_Total,sum(TaxBase) as TaxBase,sum(TaxAmt) TaxAmt from  (" & _
    ' ''                 "     select 'Sale' as RType,'" + txtFromDate.Value + "' as fromdate ,'" + txtToDate.Value + "' as todate ,Code,Name,Document_No,convert(varchar(10),DocDate,103) as DocDate,Tax,TaxRate,Document_Total,TaxBase,TaxAmt from(  " & _
    ' ''                 " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX1 as Tax,TAX1_Rate as TaxRate,Doc_Amt as Document_Total,TAX1_Base_Amt as TaxBase	,TAX1_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX1_Amt>0 AND TSPL_SCRAPINVOICE_HEAD.ispost=1 " & _
    ' ''                 " union all " & _
    ' ''                 "  select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX2 as Tax,TAX2_Rate as TaxRate,Doc_Amt as Document_Total,TAX2_Base_Amt as TaxBase	,TAX2_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX2_Amt>0 AND TSPL_SCRAPINVOICE_HEAD.ispost=1  " & _
    ' ''                 " union all " & _
    ' ''                 " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX3 as Tax,TAX3_Rate as TaxRate,Doc_Amt as Document_Total,TAX3_Base_Amt as TaxBase	,TAX3_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX3_Amt>0 AND TSPL_SCRAPINVOICE_HEAD.ispost=1  " & _
    ' ''                 " union all " & _
    ' ''                 " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX4 as Tax,TAX4_Rate as TaxRate,Doc_Amt as Document_Total,TAX4_Base_Amt as TaxBase	,TAX4_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX4_Amt>0 AND TSPL_SCRAPINVOICE_HEAD.ispost=1  " & _
    ' ''                 " union all " & _
    ' ''                 " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX5 as Tax,TAX5_Rate as TaxRate,Doc_Amt as Document_Total,TAX5_Base_Amt as TaxBase	,TAX5_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX5_Amt>0 AND TSPL_SCRAPINVOICE_HEAD.ispost=1  " & _
    ' ''                 " union all " & _
    ' ''                 " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX6 as Tax,TAX6_Rate as TaxRate,Doc_Amt as Document_Total,TAX6_Base_Amt as TaxBase	,TAX6_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX6_Amt>0 AND TSPL_SCRAPINVOICE_HEAD.ispost=1  " & _
    ' ''                 " union all " & _
    ' ''                 " select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX7 as Tax,TAX7_Rate as TaxRate,Doc_Amt as Document_Total,TAX7_Base_Amt as TaxBase	,TAX7_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD where TAX7_Amt>0 AND TSPL_SCRAPINVOICE_HEAD.ispost=1  " & _
    ' ''          " union all " & _
    ' ''" select cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX8 as Tax,TAX1_Rate as TaxRate,Doc_Amt as Document_Total,TAX8_Base_Amt as TaxBase	,TAX8_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD " & _
    ' ''             "      left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_SCRAPINVOICE_HEAD.Loc_Code  " & _
    ' ''            " where TAX8_Amt>0  AND TSPL_SCRAPINVOICE_HEAD.ispost=1 "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += " union all "
    ' ''            qry += " select  cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX9 as Tax,TAX9_Rate as TaxRate,Doc_Amt as Document_Total,TAX9_Base_Amt as TaxBase	,TAX9_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD       left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_SCRAPINVOICE_HEAD.Loc_Code   where TAX9_Amt>0  AND TSPL_SCRAPINVOICE_HEAD.ispost=1 "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then


    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += " select  cust_Code as Code,cust_Name as Name,invoice_No as Document_No,shipment_Date as DocDate,TAX10 as Tax,TAX10_Rate as TaxRate,Doc_Amt as Document_Total,TAX10_Base_Amt as TaxBase	,TAX10_Amt as TaxAmt from TSPL_SCRAPINVOICE_HEAD       left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_SCRAPINVOICE_HEAD.Loc_Code   where TAX10_Amt>0 AND TSPL_SCRAPINVOICE_HEAD.ispost=1  "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += " select  TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX1 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX1_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX1_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No    left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SALE_INVOICE_DETAIL.TAX1_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += " select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX2 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX2_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX2_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX2_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += "  Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No   left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code   where TSPL_SALE_INVOICE_DETAIL.TAX2_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += " select  TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX3 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX3_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX3_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX3_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No   left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SALE_INVOICE_DETAIL.TAX3_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX4 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX4_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX4_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX4_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code   where TSPL_SALE_INVOICE_DETAIL.TAX4_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "   select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX5 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX5_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX5_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX5_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No   left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SALE_INVOICE_DETAIL.TAX5_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX6 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX6_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX6_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX6_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No   left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SALE_INVOICE_DETAIL.TAX6_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "   select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX7 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX7_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX7_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX7_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No   left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SALE_INVOICE_DETAIL.TAX7_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX8 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX8_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX8_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX8_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No   left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SALE_INVOICE_DETAIL.TAX8_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX9 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX9_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX9_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX9_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No   left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SALE_INVOICE_DETAIL.TAX9_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    ' ''            End If
    ' ''            qry += "  Union All "
    ' ''            qry += "  select TSPL_SALE_INVOICE_HEAD.cust_Code as Code,TSPL_SALE_INVOICE_HEAD.cust_Name as Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Document_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as DocDate,TSPL_SALE_INVOICE_DETAIL.TAX10 as Tax,TSPL_SALE_INVOICE_DETAIL.TAX10_Rate as TaxRate,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt+TSPL_SALE_INVOICE_DETAIL.Empty_Value) as Document_Total,TSPL_SALE_INVOICE_DETAIL.TAX10_Assessable_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxBase	,TSPL_SALE_INVOICE_DETAIL.TAX10_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as TaxAmt  from TSPL_SALE_INVOICE_HEAD   "
    ' ''            qry += " Left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_DETAIL.Location =TSPL_LOCATION_MASTER .Location_Code   where TSPL_SALE_INVOICE_DETAIL.TAX10_Amt>0 AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
    ' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
    ' ''                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

    ' ''            End If

    ' ''            qry += " )abc where 2=2 and convert(date,DocDate,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,DocDate,103) <= convert(date,'" + txtToDate.Value + "',103) "


    ' ''            If chkSelectTax.IsChecked Then
    ' ''                qry += " and tax in  (" + clsCommon.GetMulcallString(cgvTaxCode.CheckedValue) + ")  "
    ' ''            End If

    ' ''            If chkRateselect.IsChecked Then

    ' ''                If cgvRate.CheckedValue.Count > 0 Then


    ' ''                    qry += " and TaxRate in  (" + clsCommon.GetMulcallString(cgvRate.CheckedValue) + ")  "
    ' ''                Else
    ' ''                    common.clsCommon.MyMessageBoxShow("Please select atleast one Tax Rate")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            End If

    ' ''            If rdoSale.IsChecked = True Then
    ' ''                qry += " )final  group by Tax,Code,Name,Document_No,DocDate,TaxRate"
    ' ''            End If
    ' ''        End If

    ' ''        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    ' ''        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    ' ''            Throw New Exception("No Data found to Print")
    ' ''        Else
    ' ''            CommonServicesViewer.funreport(dt, "crptTaxTracking", "Tax Tracking Report")
    ' ''        End If





    ' ''    Catch ex As Exception
    ' ''        common.clsCommon.MyMessageBoxShow(ex.Message)
    ' ''    End Try
    ' ''End Sub



    Private Sub FrmTaxTracking_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            PrintData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub

    Sub loadlocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub loadlocationState()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        Dim strState As String = " and State in (" + clsCommon.GetMulcallString(cgvState.CheckedValue) + ")  "
        cbgLocation.DataSource = clsLocation.GetLocationSegmentsStatewise(strState)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub



    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged, chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
        If cgvState.Enabled = True Then
            If cgvState.CheckedValue.Count < 0 Then
                loadlocation()
            Else
                loadlocationState()
            End If
        End If
    End Sub
    Private Sub rdoSale_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdoSale.ToggleStateChanged
        If (rdoSale.IsChecked) Then
            ChkVendorInvoice.Visible = False
            chkARinvoice.Visible = True
            'ChkSaleReturn.Visible = True
            'ChkSRInter.Visible = True
            'chkSaleInvoice.Visible = True
            'chkSaleInvoice.Checked = False
            ChkVendorInvoice.Checked = True
            chkARinvoice.Checked = False
            'ChkSaleReturn.Checked = False
            'ChkSRInter.Checked = False
            'chkSaleInvoice.Checked = False
            chkTransfer.Visible = False
            chkTransfer.Checked = False
        End If
    End Sub

    Private Sub rdoPur_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdoPur.ToggleStateChanged
        If (rdoPur.IsChecked) Then
            ChkVendorInvoice.Visible = True
            chkARinvoice.Visible = False
            'ChkSaleReturn.Visible = False
            'ChkSRInter.Visible = False
            'chkSaleInvoice.Visible = False
            'chkSaleInvoice.Checked = False
            ChkVendorInvoice.Checked = True
            chkARinvoice.Checked = False
            'ChkSaleReturn.Checked = False
            'ChkSRInter.Checked = False
            'chkSaleInvoice.Checked = False
            chkTransfer.Visible = False
            chkTransfer.Checked = False
        End If
    End Sub

    Private Sub cgvState_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cgvState.Click

    End Sub



    Private Sub cgvState_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cgvState.Leave

    End Sub

    Private Sub chkStateAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkStateAll.ToggleStateChanged
        cgvState.Enabled = Not chkStateAll.IsChecked
        loadlocation()
    End Sub

    Private Sub gv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv.DoubleClick
        Dim strTransCode As String = ""
        Dim strTransType As String = ""
        If (rdoPur.IsChecked) Then
            strTransCode = gv.CurrentRow.Cells("Source Doc No").Value
            strTransType = gv.CurrentRow.Cells("source").Value
        Else
            strTransCode = gv.CurrentRow.Cells("Document_No").Value
            strTransType = gv.CurrentRow.Cells("source").Value
        End If
        If clsCommon.CompairString(strTransType, "Vendor-Invoice") = CompairStringResult.Equal Then
            strTransType = "AP-IN"
        ElseIf clsCommon.CompairString(strTransType, "Payment") = CompairStringResult.Equal Then
            strTransType = "AP-PY"
            'ElseIf clsCommon.CompairString(strTransType, "VCGL") = CompairStringResult.Equal Then
            '    strTransType = ""
        ElseIf clsCommon.CompairString(strTransType, "GL-JE") = CompairStringResult.Equal Then
            strTransType = "GL-JE"
        ElseIf clsCommon.CompairString(strTransType, "AR-Invoice") = CompairStringResult.Equal Then
            strTransType = "AR-IN"
        ElseIf clsCommon.CompairString(strTransType, "AR-Credit") = CompairStringResult.Equal Then
            strTransType = "AR-CN"
        ElseIf clsCommon.CompairString(strTransType, "Customer-Invoice") = CompairStringResult.Equal Then
            strTransType = "AR-IN"
        ElseIf clsCommon.CompairString(strTransType, "Receipt") = CompairStringResult.Equal Then
            strTransType = "AR-PY"

        End If
        If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
            Select Case strTransType
                Case "AP-IN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strTransCode)
                Case "AP-PY"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, strTransCode)
                Case "GL-JE"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strTransCode)
                Case "AR-IN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, strTransCode)
                Case "AR-CN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, strTransCode)
                Case "AR-PY"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, strTransCode)
            End Select
        End If
    End Sub

    Private Sub cgvState__MyCheckChanged(sender As Object, e As EventArgs) Handles cgvState._MyCheckChanged
        If cgvState.CheckedValue.Count > 0 Then
            loadlocationState()
        Else
            loadlocation()
        End If
    End Sub


    Private Sub rdoTransfer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdoTransfer.ToggleStateChanged
        If (rdoTransfer.IsChecked) Then
            ChkVendorInvoice.Visible = False
            chkARinvoice.Visible = False
            chkTransfer.Visible = True
            chkTransfer.Checked = True
            ChkVendorInvoice.Checked = False
            chkARinvoice.Checked = False


        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If gv.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        If gv.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Function GetReportId()
        Dim report_id As String = Me.Form_ID
        If rdoSale.IsChecked = True Then
            report_id = report_id & "S" + IIf(Chksummary.Checked = True, "S", "")
        ElseIf rdoPur.IsChecked = True Then
            report_id = report_id & "P" + IIf(Chksummary.Checked = True, "S", "")
        ElseIf rdoTransfer.IsChecked = True Then
            report_id = report_id & "T" + IIf(Chksummary.Checked = True, "S", "")
        End If
        Return report_id
    End Function
End Class
