Imports common
Public Class frmEmpWiseReturnableReport
    Inherits FrmMainTranScreen




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("EMP-RGB")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
    End Sub
    Private Sub FrmfrmGRNReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        SetUserMgmtNew()

        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()

        LoadGRNNo()
        LoadVendor()
        LoadLocation()

        rptnEmpAll.IsChecked = True
        rptnItemAll.IsChecked = True
        rptnDocAll.IsChecked = True

        'If objCommonVar.CurrentUser <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Sub LoadGRNNo()
        Dim qry As String = "select GRN_No,GRN_Date from TSPL_GRN_HEAD"
        cbgEmp.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgEmp.ValueMember = "GRN_No"
        cbgEmp.DisplayMember = "GRN_Date"
    End Sub

    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Vendor_Code"
        cbgItem.DisplayMember = "Vendor_Name"
    End Sub

    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
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
    End Sub
    Sub PrintData()

        Try


            Dim FromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
            Dim ToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            PrintData(FromDate, ToDate, rptnEmpSelect.IsChecked, cbgEmp.CheckedValue, rptnItemSelect.IsChecked, cbgItem.CheckedValue, rptnDocSelect.IsChecked, cbgLocation.CheckedValue)

            'frmInventoryReportViewer.proShowReport("Transfer Report", clsCommon.GetPrintDate(txtFromDate.Value, "yyyy-MM-dd"), clsCommon.GetPrintDate(txtToDate.Value, "yyyy-MM-dd"), txtFromTransferNo.Value, txtToTransferNo.Value, strType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        rptnEmpAll.IsChecked = True
        rptnItemAll.IsChecked = True
        rptnDocAll.IsChecked = True
    End Sub

    Private Sub chkGRNNoAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rptnEmpAll.ToggleStateChanged, rptnEmpSelect.ToggleStateChanged
        cbgEmp.Enabled = Not rptnEmpAll.IsChecked
        If rptnEmpSelect.IsChecked Then
            rptnItemAll.IsChecked = rptnEmpSelect.IsChecked
        End If
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rptnItemAll.ToggleStateChanged, rptnItemSelect.ToggleStateChanged
        cbgItem.Enabled = Not rptnItemAll.IsChecked
        If rptnItemSelect.IsChecked Then
            rptnEmpAll.IsChecked = rptnItemSelect.IsChecked
        End If
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rptnDocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rptnDocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Public Shared Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isGRNNoSelect As Boolean, ByVal ArrGRNNo As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isLocationSelect As Boolean, ByVal ArrLocation As ArrayList)

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

        If isGRNNoSelect Then
            qry += " and HEAD.GRN_No in (" + clsCommon.GetMulcallString(ArrGRNNo) + ") "
        End If
        If isVendorSelect Then
            qry += " and HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        End If
        If isLocationSelect Then
            qry += " and head.Bill_To_Location in (" + clsCommon.GetMulcallString(ArrLocation) + ")"
        End If

        qry += " and convert(date,HEAD.GRN_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,HEAD.GRN_Date,103)<= convert(date,'" + ToDate + "',103)"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptGRNReport", "GRN Report")
        frmCRV = Nothing
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "EMP-RGB" ''"GRN-RPT"
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

    Private Sub frmEmpWiseReturnableReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

End Class
