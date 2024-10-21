Imports common
Public Class frmRptAPInvoice
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnAPInvoiceReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub frmRptAPInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            'resetForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub
    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'LoadInvoiceType()
        LoadDocuemntNo()
        LoadVendorGroup()
        LoadVendor()
        loadlocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        chkDocAll.IsChecked = True
        chkVendorAll.IsChecked = True
        chkVGroupAll.IsChecked = True

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

    End Sub

    Sub LoadDocuemntNo()
        Dim fromdate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
        Dim qry As String = "select Document_No,Invoice_Entry_Date from TSPL_VENDOR_INVOICE_HEAD where convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>= convert(date,'" + fromdate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<= convert(date,'" + todate + "',103) order by convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) "
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Document_No"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"
        'cbgDocument.CheckedValue

    End Sub
    Sub LoadVendorGroup()
        Dim qry As String = "select Ven_Group_Code,Group_Desc from TSPL_VENDOR_GROUP order by Ven_Group_Code"
        cbgVendorGroup.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendorGroup.ValueMember = "Ven_Group_Code"
        cbgVendorGroup.DisplayMember = "Group_Desc"
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  where Status='N' "
        If chkVGroupSelect.IsChecked AndAlso cbgVendorGroup.CheckedValue.Count > 0 Then
            qry = qry + " and Vendor_Group_Code in (" + clsCommon.GetMulcallString(cbgVendorGroup.CheckedValue) + ") "
        End If
        qry = qry + "  order by Vendor_Code"
        cbgVendor.DataSource = Nothing
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
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
        Print()
    End Sub
    Sub Print()
        Try
            Dim arrLocation As ArrayList = Nothing
            If chkLocSelect.IsChecked Then
                arrLocation = cbgLocation.CheckedValue
            End If


            Dim FromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
            Dim ToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            '===== Change By : Prabhakar  ==============='
            '===== Add prameter chkVGroupSelect.IsChecked, cbgVendorGroup.CheckedValue ==============='
            PrintData(FromDate, ToDate, chkDocSelect.IsChecked, cbgDocument.CheckedValue, chkVGroupSelect.IsChecked, cbgVendorGroup.CheckedValue, chkVendorSelect.IsChecked, cbgVendor.CheckedValue, chkLocSelect.IsChecked, arrLocation)
            '===== End ================================='

            'frmInventoryReportViewer.proShowReport("Transfer Report", clsCommon.GetPrintDate(txtFromDate.Value, "yyyy-MM-dd"), clsCommon.GetPrintDate(txtToDate.Value, "yyyy-MM-dd"), txtFromTransferNo.Value, txtToTransferNo.Value, strType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(me,ex.Message,Me.text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadVendorGroup()
        LoadVendor()
        loadlocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        chkDocAll.IsChecked = True
        chkVendorAll.IsChecked = True
        chkLocAll.IsChecked = True
        chkVGroupAll.IsChecked = True



    End Sub

    Private Sub txtFromTransferNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        ''Dim qry As String = "select Transfer_No,CONVERT(varchar(11),Transfer_Date,103) as [Date], FromLocation.Location_Desc as [From Location],ToLocation.Location_Desc as [To Location] from TSPL_TRANSFER_HEAD left outer join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code=TSPL_TRANSFER_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as ToLocation on ToLocation .Location_Code=TSPL_TRANSFER_HEAD.To_Location "
        ''Dim WhrCls As String = ""
        ''If rbtnLoadin.IsChecked Then
        ''    WhrCls = " Transfer_Type='LI'"
        ''ElseIf rbtnExcisable.IsChecked Then
        ''    WhrCls = " Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))>0 "
        ''Else
        ''    WhrCls = " Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))<=0"
        ''End If
        ''txtFromTransferNo.Value = clsCommon.ShowSelectForm("TransferList", qry, "Transfer_No", WhrCls, txtFromTransferNo.Value, "Transfer_No", isButtonClicked)
    End Sub


    Private Sub txtToTransferNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        ''Dim qry As String = "select Transfer_No,CONVERT(varchar(11),Transfer_Date,103) as [Date], FromLocation.Location_Desc as [From Location],ToLocation.Location_Desc as [To Location] from TSPL_TRANSFER_HEAD left outer join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code=TSPL_TRANSFER_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as ToLocation on ToLocation .Location_Code=TSPL_TRANSFER_HEAD.To_Location "
        ''Dim WhrCls As String = ""
        ''If rbtnLoadin.IsChecked Then
        ''    WhrCls = " Transfer_Type='LI'"
        ''ElseIf rbtnExcisable.IsChecked Then
        ''    WhrCls = " Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))>0 "
        ''Else
        ''    WhrCls = " Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))<=0"
        ''End If
        ''txtToTransferNo.Value = clsCommon.ShowSelectForm("TransferList", qry, "Transfer_No", WhrCls, txtToTransferNo.Value, "Transfer_No", isButtonClicked)
    End Sub

    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDocAll.ToggleStateChanged, chkDocSelect.ToggleStateChanged
        cbgDocument.Enabled = Not chkDocAll.IsChecked
        If chkDocSelect.IsChecked Then
            ''==below code commented because it always reset when one control is selected and want to select other one thats create problem--done by Monika
            'chkVendorAll.IsChecked = True
            'chkVGroupAll.IsChecked = True
            cbgDocument.UnCheckedAll()
        End If
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged, chkVendorSelect.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
        If chkVendorSelect.IsChecked Then
            'chkDocAll.IsChecked = True
            'chkVGroupAll.IsChecked = True
            cbgVendor.UnCheckedAll()
            LoadVendor()
        End If
    End Sub

    Public Shared Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList)

        If isDocSelect AndAlso ArrDoc.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Document")
            Return
        ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
            Return
        End If

        Dim qry As String = "select  Final.Document_No,final.ACCode,Final.ACName,DrAmt,CrAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.Due_Date,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,CreatedBy.User_Name as CreateBy,AuthorisedBy.User_Name as ApproveBy,case when len(ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,''))>0 then 'Posted' else 'Pending' end as InvStatus,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Bill Inward Voucher' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end end end as InvoiceType    from (" & _
        " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('D') then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end as DrAmt,case when Document_Type in('I','C') then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')" & _
        " union all" & _
        " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code as ACCode,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Desc as ACName,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as DrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as CrAmt from TSPL_VENDOR_INVOICE_DETAIL " & _
        " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')"

        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            qry += " union all" & _
                " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else 0 end as DrAmt,case when Document_Type in('D') then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else 0 end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')"
        Next
        qry += " union all" & _
        " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('D') then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else 0 end as DrAmt,case when Document_Type in('I','C') then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else 0 end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC where TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')" & _
        " union all " & _
        " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else 0 end as DrAmt,case when Document_Type in ('D') then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else 0 end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC where TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')" & _
        " union all " & _
        " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_REMITTANCE.Branch_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') then TSPL_REMITTANCE.Actual_Total_TDS else 0 end as DrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') then TSPL_REMITTANCE.Actual_Total_TDS else 0 end as CrAmt from TSPL_REMITTANCE left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE.Branch_GL_AC left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_REMITTANCE.Document_No where TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')" & _
        " )Final left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=final.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code left outer join TSPL_USER_MASTER as CreatedBy on CreatedBy.User_Code=TSPL_VENDOR_INVOICE_HEAD.Created_By" & _
        " left outer join TSPL_USER_MASTER as AuthorisedBy on AuthorisedBy .User_Code=TSPL_VENDOR_INVOICE_HEAD.Modify_By where 2=2 "

        If isDocSelect Then
            qry += " and TSPL_VENDOR_INVOICE_HEAD.Document_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        ElseIf isVendorSelect Then
            qry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        Else
            qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<= convert(date,'" + ToDate + "',103)"
        End If
        qry += " order by Final.Document_No,Final.DrAmt desc,Final.CrAmt desc"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.Purchase, dt, "rptAPInvoice", "AP Invoice")
        frmCRV = Nothing
    End Sub

    Public Shared Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isLocationselect As Boolean, ByVal Arrlocation As ArrayList)
        Try
            Dim LocCode As String = ""
            Dim Vendor As String = ""
            Dim DocNo As String = ""
            If isDocSelect AndAlso ArrDoc.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Document")
                Return
            ElseIf isDocSelect AndAlso ArrDoc.Count > 0 Then
                DocNo = clsCommon.GetMulcallString(ArrDoc)
                DocNo = DocNo.Replace("'", "")
            End If
            If isVendorSelect AndAlso ArrVendor.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
                Return
            ElseIf isVendorSelect AndAlso ArrVendor.Count > 0 Then
                Vendor = clsCommon.GetMulcallString(ArrVendor)
                Vendor = Vendor.Replace("'", "")

            End If
            If Arrlocation IsNot Nothing AndAlso Arrlocation.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location segment")
                Return
            ElseIf isLocationselect AndAlso Arrlocation.Count > 0 Then
                LocCode = clsCommon.GetMulcallString(Arrlocation)
                LocCode = LocCode.Replace("'", "")
            End If

            'done by priti KDI/05/07/18-000390 for updating vendor name from master
            Dim qryForAddChagesOfFirstRow As String = " + case when TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 then TSPL_VENDOR_INVOICE_HEAD.Total_Add_Charge else 0 end"
            Dim qry As String = "select  '" + FromDate + "' as FromDate,'" + ToDate + "' as ToDate, '" + LocCode + "' as Location,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_HEAD.Document_Type,'" + Vendor + "' as Vendor,'" + DocNo + "'as Document, Final.Document_No,final.ACCode,Final.ACName,DrAmt,CrAmt,Hirerachy_Code,Cost_Centre_Code ,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Created_By  ,TSPL_VENDOR_INVOICE_HEAD.Created_Date,TSPL_VENDOR_INVOICE_HEAD.Due_Date,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,CreatedBy.User_Name as CreateBy,AuthorisedBy.User_Name as ApproveBy,case when ((TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS null ) Or (TSPL_VENDOR_INVOICE_HEAD.Posting_Date='') ) then 'Pending' else 'Posted' end as InvStatus,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Bill Inward Voucher' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end end end as InvoiceType " & _
            " , ( select case when LEN(ISNULL(RefDocType,''))>0 then case when RefDocType='S' then 'SRN' else case when RefDocType='AP' then 'AP Invoice' end end +' : '+RefDocNo  +' - ' +(case when RefDocType='S' then (Select top 1 convert(varchar(100),SRN_Date,110)   from TSPL_SRN_HEAD where SRN_No =RefDocNo) else (select top 1 convert(varchar(100),Invoice_Entry_Date,110)  from TSPL_VENDOR_INVOICE_HEAD where RefDocNo  = RefDocNo) end)  else '' end from TSPL_VENDOR_INVOICE_HEAD where Document_No=Final.Document_No) as RefDocDescription  from (" & _
            " select  TSPL_VENDOR_INVOICE_HEAD.RefDocNo,TSPL_VENDOR_INVOICE_HEAD.RefDocType,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,"
            qry += "     (case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 then TSPL_VENDOR_INVOICE_HEAD.Document_Total else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Document_Total<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Document_Total) else 0 end end"
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) as DrAmt,"
            qry += " (case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 then TSPL_VENDOR_INVOICE_HEAD.Document_Total else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Document_Total<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Document_Total  ) else 0 end end "
            qry += " +   case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) as CrAmt,'' as Hirerachy_Code,'' as Cost_Centre_Code  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
            If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If
            qry += " union all" + Environment.NewLine
            'qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code as ACCode,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Desc as ACName,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end )as DrAmt,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end) as CrAmt from TSPL_VENDOR_INVOICE_DETAIL " & _
            '" left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No  left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "

            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code as ACCode, " & _
            "TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Desc as ACName,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and " & _
            "TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else " & _
            "(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and " & _
            "TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end )  " & _
            " +  (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then " & _
            "case when TaxM1.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX1_Amt " & _
            "when TaxM2.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX2_Amt " & _
            "when TaxM3.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX3_Amt " & _
            "when TaxM4.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX4_Amt " & _
            "when TaxM5.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX5_Amt " & _
            "when TaxM6.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX6_Amt " & _
            "when TaxM7.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX7_Amt " & _
            "when TaxM8.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX8_Amt " & _
            "when TaxM9.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX9_Amt " & _
            "when TaxM10.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX10_Amt else 0 end  else 0 end )as DrAmt,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end) as CrAmt,TSPL_HIRERACHY_LEVEL_MASTER.Description as Hirerachy_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Cost_Centre_Code  from TSPL_VENDOR_INVOICE_DETAIL " & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No   " & _
            "left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code " & _
            "left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX1 " & _
             "left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX2 " & _
             "left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX3 " & _
             "left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX4 " & _
             "left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX5 " & _
             "left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX6 " & _
             "left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX7 " & _
             "left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX8 " & _
             "left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX9 " & _
             "left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX10  " & _
             "left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_VENDOR_INVOICE_DETAIL.Cost_Centre_Code " & _
            "left outer join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code=TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code " & _
            " where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "

            If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If

            For ii As Integer = 1 To 10
                Dim strii As String = clsCommon.myCstr(ii)
                For jj As Integer = 1 To 5
                    Dim strjj As String = ""
                    If jj > 1 Then
                        strjj = clsCommon.myCstr(jj)
                    End If
                    qry += " union all " + Environment.NewLine
                    qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + " as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt) else 0 end end as CrAmt,'' as Hirerachy_Code,'' as Cost_Centre_Code  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + " where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
                    If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                        qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
                    End If
                Next
                'qry += " union all" & _
                '    " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as CrAmt,'' as Hirerachy_Code,'' as Cost_Centre_Code  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')"
                qry += " union all" + Environment.NewLine
                qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then  (TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then  (TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as CrAmt,'' as Hirerachy_Code,'' as Cost_Centre_Code  from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
                If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                    qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
                End If
            Next
            qry += " union all" + Environment.NewLine
            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Discount_Amount) else 0 end end as DrAmt,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Discount_Amount) else 0 end end as CrAmt,'' as Hirerachy_Code,'' as Cost_Centre_Code from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC where TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
            If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If
            qry += " union all " + Environment.NewLine
            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else case when Document_Type in ('D') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount<0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount) else 0 end end as DrAmt,case when Document_Type in ('D') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount<0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount) else 0 end end as CrAmt,'' as Hirerachy_Code,'' as Cost_Centre_Code   from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC where TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
            If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If
            qry += " union all " + Environment.NewLine
            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_REMITTANCE.Branch_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then TSPL_REMITTANCE.Actual_Total_TDS else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_REMITTANCE.Actual_Total_TDS<0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then -1*(TSPL_REMITTANCE.Actual_Total_TDS) else 0 end end as DrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then TSPL_REMITTANCE.Actual_Total_TDS else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_REMITTANCE.Actual_Total_TDS<0  then -1*(TSPL_REMITTANCE.Actual_Total_TDS) else 0 end end as CrAmt,'' as Hirerachy_Code,'' as Cost_Centre_Code  from TSPL_REMITTANCE left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE.Branch_GL_AC left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_REMITTANCE.Document_No where TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
            If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If
            qry += " )Final left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=final.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code left outer join TSPL_USER_MASTER as CreatedBy on CreatedBy.User_Code=TSPL_VENDOR_INVOICE_HEAD.Created_By"
            qry += " left outer join TSPL_USER_MASTER as AuthorisedBy on AuthorisedBy .User_Code=TSPL_VENDOR_INVOICE_HEAD.Modify_By where 2=2 "

            If isDocSelect Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.Document_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            ElseIf isVendorSelect Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
            Else
                qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<= convert(date,'" + ToDate + "',103)"
            End If
            'qry += " order by Final.Document_No,Final.DrAmt desc,Final.CrAmt,'' as Hirerachy_Code,'' as Cost_Centre_Code  desc"

            qry = "select *,TSPL_COMPANY_MASTER .Logo_Img ,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code, TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode  from (select MAX( FromDate) as FromDate,max(ToDate) as ToDate,max(Location) as Location,max(Document_Type) as Document_Type,max(Loc_Code) as Loc_Code,max(Vendor) as Vendor,max(Document) as Document,max(Document_No) as Document_No,max(ACCode) as ACCode,max(ACName) as ACName, case when SUM(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end   as DrAmt ,case when SUM( CrAmt-DrAmt)>0 then SUM( CrAmt-DrAmt) else 0 end   as CrAmt,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name,max(Vendor_Code) as Vendor_Code,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(RefDocNo) as RefDocNo,max(RefDocType) as RefDocType,max(Vendor_Name) as Vendor_Name,max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Due_Date) as Due_Date,max(Description) as Description,max(Vendor_Invoice_No) as Vendor_Invoice_No,max(Vendor_Invoice_Date) as Vendor_Invoice_Date,max(CreateBy) as CreateBy,max(ApproveBy) as ApproveBy,max(InvStatus) as InvStatus,max(InvoiceType) as InvoiceType,max(RefDocDescription) as RefDocDescription,max(Hirerachy_Code) as Hirerachy_Code ,max(Cost_Centre_Code) as Cost_Centre_Code  from(" + qry + " )xxx group by Document_No,ACCode )xxxx left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =xxxx.Comp_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xxxx.Loc_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = xxxx.Vendor_Code   left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state   order by xxxx.Document_No,xxxx.DrAmt desc,xxxx.CrAmt desc"
            Dim qry1 As String = "select  TSPL_ITEM_MASTER.HSN_Code, TSPL_SRN_DETAIL.Item_Code ,TSPL_SRN_DETAIL.Item_Desc,TSPL_VENDOR_INVOICE_HEAD .Description ,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType   from TSPL_SRN_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SRN_DETAIL.Item_Code       where RefDocType ='S'and TSPL_VENDOR_INVOICE_HEAD .Document_No in(" + clsCommon.GetMulcallString(ArrDoc) + ") and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= ''  "
            Dim strInvoiceEntryDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date from TSPL_VENDOR_INVOICE_HEAD where TSPL_VENDOR_INVOICE_HEAD.Document_No=  '" + DocNo + "' "))
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreport(CrystalReportFolder.Purchase, qry, qry1, "rptAPInvoice", "AP Invoice", "AP_InvoiceDetails.rpt", clsCommon.myCDate(strInvoiceEntryDate))
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'Public Shared Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isLocationselect As Boolean, ByVal Arrlocation As ArrayList)
    '    Try
    '        Dim LocCode As String
    '        Dim Vendor As String
    '        Dim DocNo As String
    '        If isDocSelect AndAlso ArrDoc.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please select at least one Document")
    '            Return
    '        ElseIf isDocSelect AndAlso ArrDoc.Count > 0 Then
    '            DocNo = clsCommon.GetMulcallString(ArrDoc)
    '            DocNo = DocNo.Replace("'", "")
    '        End If
    '        If isVendorSelect AndAlso ArrVendor.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
    '            Return
    '        ElseIf isVendorSelect AndAlso ArrVendor.Count > 0 Then
    '            Vendor = clsCommon.GetMulcallString(ArrVendor)
    '            Vendor = Vendor.Replace("'", "")

    '        End If
    '        If Arrlocation IsNot Nothing AndAlso Arrlocation.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please select at least one Location segment")
    '            Return
    '        ElseIf isLocationselect AndAlso Arrlocation.Count > 0 Then
    '            LocCode = clsCommon.GetMulcallString(Arrlocation)
    '            LocCode = LocCode.Replace("'", "")
    '        End If
    '        Dim qryForAddChagesOfFirstRow As String = " + case when TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 then TSPL_VENDOR_INVOICE_HEAD.Total_Add_Charge else 0 end"
    '        Dim qry As String = "select  '" + FromDate + "' as FromDate,'" + ToDate + "' as ToDate, '" + LocCode + "' as Location,'" + Vendor + "' as Vendor,'" + DocNo + "'as Document, Final.Document_No,final.ACCode,Final.ACName,DrAmt,CrAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Created_By  ,TSPL_VENDOR_INVOICE_HEAD.Created_Date,TSPL_VENDOR_INVOICE_HEAD.Due_Date,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,CreatedBy.User_Name as CreateBy,AuthorisedBy.User_Name as ApproveBy,case when ((TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS null ) Or (TSPL_VENDOR_INVOICE_HEAD.Posting_Date='') ) then 'Pending' else 'Posted' end as InvStatus,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Bill Inward Voucher' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end end end as InvoiceType " & _
    '        " , ( select case when LEN(ISNULL(RefDocType,''))>0 then case when RefDocType='S' then 'SRN' else case when RefDocType='AP' then 'AP Invoice' end end +' : '+RefDocNo  +' - ' +(case when RefDocType='S' then (Select top 1 convert(varchar(100),SRN_Date,110)   from TSPL_SRN_HEAD where SRN_No =RefDocNo) else (select top 1 convert(varchar(100),Invoice_Entry_Date,110)  from TSPL_VENDOR_INVOICE_HEAD where RefDocNo  = RefDocNo) end)  else '' end from TSPL_VENDOR_INVOICE_HEAD where Document_No=Final.Document_No) as RefDocDescription ,final.remarks   from (" & _
    '        " select  TSPL_VENDOR_INVOICE_HEAD.RefDocNo,TSPL_VENDOR_INVOICE_HEAD.RefDocType,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,"
    '        qry += "     (case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 then TSPL_VENDOR_INVOICE_HEAD.Document_Total else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Document_Total<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Document_Total) else 0 end end"
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end "
    '        qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) as DrAmt,"
    '        qry += " (case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 then TSPL_VENDOR_INVOICE_HEAD.Document_Total else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Document_Total<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Document_Total  ) else 0 end end "
    '        qry += " +   case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end "
    '        qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) as CrAmt,TSPL_VENDOR_INVOICE_DETAIL.remarks from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC  left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No  where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
    '        If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
    '            qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
    '        End If
    '        qry += " union all" + Environment.NewLine
    '        qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code as ACCode,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Desc as ACName,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end )as DrAmt,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end) as CrAmt,TSPL_VENDOR_INVOICE_DETAIL.remarks from TSPL_VENDOR_INVOICE_DETAIL " & _
    '        " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No  left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
    '        If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
    '            qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
    '        End If

    '        For ii As Integer = 1 To 10
    '            Dim strii As String = clsCommon.myCstr(ii)
    '            For jj As Integer = 1 To 5
    '                Dim strjj As String = ""
    '                If jj > 1 Then
    '                    strjj = clsCommon.myCstr(jj)
    '                End If
    '                qry += " union all " + Environment.NewLine
    '                qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + " as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt) else 0 end end as CrAmt,TSPL_VENDOR_INVOICE_DETAIL.remarks from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "  left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No  where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
    '                If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
    '                    qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
    '                End If
    '            Next
    '            ''qry += " union all" & _
    '            ''    " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')"
    '            qry += " union all" + Environment.NewLine
    '            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then  (TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then  (TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as CrAmt,TSPL_VENDOR_INVOICE_DETAIL.remarks from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC  left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No  where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
    '            If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
    '                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
    '            End If
    '        Next
    '        qry += " union all" + Environment.NewLine
    '        qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Discount_Amount) else 0 end end as DrAmt,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Discount_Amount) else 0 end end as CrAmt,TSPL_VENDOR_INVOICE_DETAIL.remarks from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC  left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No  where TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
    '        If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
    '            qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
    '        End If
    '        qry += " union all " + Environment.NewLine
    '        qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else case when Document_Type in ('D') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount) else 0 end end as DrAmt,case when Document_Type in ('D') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount) else 0 end end as CrAmt,TSPL_VENDOR_INVOICE_DETAIL.remarks from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC  left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No  where TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
    '        If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
    '            qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
    '        End If
    '        qry += " union all " + Environment.NewLine
    '        qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_REMITTANCE.Branch_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_REMITTANCE.Actual_Total_TDS>0 then TSPL_REMITTANCE.Actual_Total_TDS else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_REMITTANCE.Actual_Total_TDS<0 then -1*(TSPL_REMITTANCE.Actual_Total_TDS) else 0 end end as DrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_REMITTANCE.Actual_Total_TDS>0 then TSPL_REMITTANCE.Actual_Total_TDS else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_REMITTANCE.Actual_Total_TDS<0 then -1*(TSPL_REMITTANCE.Actual_Total_TDS) else 0 end end as CrAmt,TSPL_VENDOR_INVOICE_DETAIL.remarks from TSPL_REMITTANCE left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE.Branch_GL_AC left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_REMITTANCE.Document_No  left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No  where TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
    '        If Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
    '            qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
    '        End If
    '        qry += " )Final left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=final.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code left outer join TSPL_USER_MASTER as CreatedBy on CreatedBy.User_Code=TSPL_VENDOR_INVOICE_HEAD.Created_By"
    '        qry += " left outer join TSPL_USER_MASTER as AuthorisedBy on AuthorisedBy .User_Code=TSPL_VENDOR_INVOICE_HEAD.Modify_By where 2=2 "

    '        If isDocSelect Then
    '            qry += " and TSPL_VENDOR_INVOICE_HEAD.Document_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
    '        ElseIf isVendorSelect Then
    '            qry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
    '        Else
    '            qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<= convert(date,'" + ToDate + "',103)"
    '        End If
    '        ''qry += " order by Final.Document_No,Final.DrAmt desc,Final.CrAmt desc"

    '        qry = "select * from (select MAX( FromDate) as FromDate,max(ToDate) as ToDate,max(Location) as Location,max(Vendor) as Vendor,max(Document) as Document,max(Document_No) as Document_No,max(ACCode) as ACCode,max(ACName) as ACName, case when SUM(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end   as DrAmt ,case when SUM( CrAmt-DrAmt)>0 then SUM( CrAmt-DrAmt) else 0 end   as CrAmt,max(Comp_Name) as Comp_Name,max(Vendor_Code) as Vendor_Code,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(RefDocNo) as RefDocNo,max(RefDocType) as RefDocType,max(Vendor_Name) as Vendor_Name,max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Due_Date) as Due_Date,max(Description) as Description,max(Vendor_Invoice_No) as Vendor_Invoice_No,max(Vendor_Invoice_Date) as Vendor_Invoice_Date,max(CreateBy) as CreateBy,max(ApproveBy) as ApproveBy,max(InvStatus) as InvStatus,max(InvoiceType) as InvoiceType,max(RefDocDescription) as RefDocDescription,MAX(Remarks) as Remarks from(" + qry + " )xxx group by Document_No,ACCode )xxxx  order by xxxx.Document_No,xxxx.DrAmt desc,xxxx.CrAmt desc"
    '        Dim qry1 As String = "select Item_Code ,Item_Desc,TSPL_VENDOR_INVOICE_HEAD .Description ,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType   from TSPL_SRN_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo where RefDocType ='S'and TSPL_VENDOR_INVOICE_HEAD .Document_No in(" + clsCommon.GetMulcallString(ArrDoc) + ") and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= ''  "

    '        PurchaseViewer.funsubreport(qry, qry1, "rptAPInvoice", "AP Invoice", "AP_InvoiceDetails.rpt")
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try
    'End Sub
    ''This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    '    Private Function funSetUserAccess() As Boolean
    '        Try

    '            Dim strRights As String
    '            Dim strTemp() As String
    '            Dim strProgCode = "AP-INV-RPT"
    '            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '            strTemp = Split(strRights, ",")
    '            If strTemp(0) = "0" Then
    '                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '                funSetUserAccess = False
    '                blnRead = False
    '                Me.Close()
    '                Exit Function
    '            Else
    '                blnRead = True
    '            End If
    '            If strTemp(1) = "0" Then 'Grant modify access

    '            End If
    '            If strTemp(2) = "0" Then 'Grant modify access

    '            End If

    '            funSetUserAccess = True
    '        Catch er As Exception
    '            myMessages.myExceptions(er)
    '        End Try
    '    End Function

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged, chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
    Sub loadlocation()
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    '====done by shivani against ticket [BM00000009237]
    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged
        Dim strqry As String = "select  Document_No ," & _
           " Invoice_Entry_Date   from  TSPL_VENDOR_INVOICE_HEAD where CONVERT(Date,Invoice_Entry_Date ,103)>=Convert(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "',103) and CONVERT(Date,Invoice_Entry_Date,103)<=Convert(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "',103) order by convert(date,Invoice_Entry_Date,103) "
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgDocument.ValueMember = "Document_No"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"
    End Sub

    Private Sub txtToDate_ValueChanged(sender As Object, e As EventArgs) Handles txtToDate.ValueChanged
        Dim strqry As String = "select  Document_No ," & _
           " Invoice_Entry_Date   from  TSPL_VENDOR_INVOICE_HEAD where CONVERT(Date,Invoice_Entry_Date ,103)>=Convert(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "',103) and CONVERT(Date,Invoice_Entry_Date,103)<=Convert(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "',103) order by convert(date,Invoice_Entry_Date,103) "
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgDocument.ValueMember = "Document_No"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"
    End Sub
    ' Created by : Prabhakar 
    Public Shared Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVGroupSelect As Boolean, ByVal ArrVGroup As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isLocationselect As Boolean, ByVal Arrlocation As ArrayList)
        Try
            Dim LocCode As String = ""
            Dim Vendor As String = ""
            Dim DocNo As String = ""
            Dim VGroup As String = ""
            If isDocSelect AndAlso ArrDoc.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Document")
                Return
            ElseIf isDocSelect AndAlso ArrDoc.Count > 0 Then
                DocNo = clsCommon.GetMulcallString(ArrDoc)
                DocNo = DocNo.Replace("'", "")
            End If


            If isVGroupSelect AndAlso ArrVGroup.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Vendor Group For Print")
                Return
            ElseIf isVGroupSelect AndAlso ArrVGroup.Count > 0 Then
                VGroup = clsCommon.GetMulcallString(ArrVGroup)
                VGroup = VGroup.Replace("'", "")

            End If



            If isVendorSelect AndAlso ArrVendor.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
                Return
            ElseIf isVendorSelect AndAlso ArrVendor.Count > 0 Then
                Vendor = clsCommon.GetMulcallString(ArrVendor)
                Vendor = Vendor.Replace("'", "")

            End If
            If Arrlocation IsNot Nothing AndAlso Arrlocation.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location segment")
                Return
            ElseIf isLocationselect AndAlso Arrlocation.Count > 0 Then
                LocCode = clsCommon.GetMulcallString(Arrlocation)
                LocCode = LocCode.Replace("'", "")
            End If
            Dim qryForAddChagesOfFirstRow As String = " + case when TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 then TSPL_VENDOR_INVOICE_HEAD.Total_Add_Charge else 0 end"
            Dim qry As String = "select  '" + FromDate + "' as FromDate,'" + ToDate + "' as ToDate, '" + LocCode + "' as Location,'" + Vendor + "' as Vendor,'" + DocNo + "'as Document, Final.Document_No,final.ACCode,Final.ACName,DrAmt,CrAmt,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_MASTER.Vendor_Group_Code,TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType, tspl_vendor_master.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Created_By  ,TSPL_VENDOR_INVOICE_HEAD.Created_Date,TSPL_VENDOR_INVOICE_HEAD.Due_Date,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,CreatedBy.User_Name as CreateBy,AuthorisedBy.User_Name as ApproveBy,case when ((TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS null ) Or (TSPL_VENDOR_INVOICE_HEAD.Posting_Date='') ) then 'Pending' else 'Posted' end as InvStatus,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Bill Inward Voucher' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end end end as InvoiceType " & _
            " , ( select case when LEN(ISNULL(RefDocType,''))>0 then case when RefDocType='S' then 'SRN' else case when RefDocType='AP' then 'AP Invoice' end end +' : '+RefDocNo  +' - ' +(case when RefDocType='S' then (Select top 1 convert(varchar(100),SRN_Date,110)   from TSPL_SRN_HEAD where SRN_No =RefDocNo) else (select top 1 convert(varchar(100),Invoice_Entry_Date,110)  from TSPL_VENDOR_INVOICE_HEAD where RefDocNo  = RefDocNo) end)  else '' end from TSPL_VENDOR_INVOICE_HEAD where Document_No=Final.Document_No) as RefDocDescription  from (" & _
            " select  TSPL_VENDOR_INVOICE_HEAD.RefDocNo,TSPL_VENDOR_INVOICE_HEAD.RefDocType,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,"
            qry += "     (case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 then TSPL_VENDOR_INVOICE_HEAD.Document_Total else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Document_Total<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Document_Total) else 0 end end"
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end "
            qry += " + case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) as DrAmt,"
            qry += " (case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 then TSPL_VENDOR_INVOICE_HEAD.Document_Total else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Document_Total<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Document_Total  ) else 0 end end "
            qry += " +   case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end "
            qry += " + case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
            If isLocationselect AndAlso Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If

            ''==================Done By Monika(18/11/2016)======================================
            If isDocSelect AndAlso ArrDoc IsNot Nothing AndAlso ArrDoc.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.document_no in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            End If
            If isVendorSelect AndAlso ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
            End If
            If isVGroupSelect AndAlso ArrVGroup IsNot Nothing AndAlso ArrVGroup.Count > 0 Then
                qry += " and TSPL_VENDOR_Master.vendor_group_code in (" + clsCommon.GetMulcallString(ArrVGroup) + ") "
            End If
            ''=============================================================================


            qry += " union all" + Environment.NewLine
            'qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code as ACCode,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Desc as ACName,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end )as DrAmt,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end) as CrAmt from TSPL_VENDOR_INVOICE_DETAIL " & _
            '" left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No  left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "

            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code as ACCode, " & _
            "TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Desc as ACName,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and " & _
            "TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else " & _
            "(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and " & _
            "TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end )  " & _
            " +  (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then " & _
            "case when TaxM1.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX1_Amt " & _
            "when TaxM2.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX2_Amt " & _
            "when TaxM3.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX3_Amt " & _
            "when TaxM4.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX4_Amt " & _
            "when TaxM5.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX5_Amt " & _
            "when TaxM6.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX6_Amt " & _
            "when TaxM7.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX7_Amt " & _
            "when TaxM8.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX8_Amt " & _
            "when TaxM9.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX9_Amt " & _
            "when TaxM10.Tax_Recoverable= 'N' then TSPL_VENDOR_INVOICE_DETAIL.TAX10_Amt else 0 end  else 0 end )as DrAmt,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_VENDOR_INVOICE_DETAIL.Amount>0 then TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + " else (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_VENDOR_INVOICE_DETAIL.Amount<0 then -1*(TSPL_VENDOR_INVOICE_DETAIL.Amount" + qryForAddChagesOfFirstRow + ") else 0 end) end) as CrAmt from TSPL_VENDOR_INVOICE_DETAIL " & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No   " & _
            "left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code =TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code " & _
            "left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX1 " & _
             "left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX2 " & _
             "left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX3 " & _
             "left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX4 " & _
             "left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX5 " & _
             "left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX6 " & _
             "left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX7 " & _
             "left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX8 " & _
             "left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX9 " & _
             "left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX10  " & _
             " left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code " & _
            " where TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "

            If isLocationselect AndAlso Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If

            ''==================Done By Monika(18/11/2016)======================================
            If isDocSelect AndAlso ArrDoc IsNot Nothing AndAlso ArrDoc.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.document_no in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            End If
            If isVendorSelect AndAlso ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
            End If
            If isVGroupSelect AndAlso ArrVGroup IsNot Nothing AndAlso ArrVGroup.Count > 0 Then
                qry += " and TSPL_VENDOR_Master.vendor_group_code in (" + clsCommon.GetMulcallString(ArrVGroup) + ") "
            End If
            ''=============================================================================

            For ii As Integer = 1 To 10
                Dim strii As String = clsCommon.myCstr(ii)
                For jj As Integer = 1 To 5
                    Dim strjj As String = ""
                    If jj > 1 Then
                        strjj = clsCommon.myCstr(jj)
                    End If
                    qry += " union all " + Environment.NewLine
                    qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + " as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt) else 0 end end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + " left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC" + strjj + "_Amt<>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
                    If isLocationselect AndAlso Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                        qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
                    End If

                    ''==================Done By Monika(18/11/2016)======================================
                    If isDocSelect AndAlso ArrDoc IsNot Nothing AndAlso ArrDoc.Count > 0 Then
                        qry += " and TSPL_VENDOR_INVOICE_HEAD.document_no in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
                    End If
                    If isVendorSelect AndAlso ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                        qry += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
                    End If
                    If isVGroupSelect AndAlso ArrVGroup IsNot Nothing AndAlso ArrVGroup.Count > 0 Then
                        qry += " and TSPL_VENDOR_Master.vendor_group_code in (" + clsCommon.GetMulcallString(ArrVGroup) + ") "
                    End If
                    ''=============================================================================
                Next
                'qry += " union all" & _
                '    " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_GLAC where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D')"
                qry += " union all" + Environment.NewLine
                qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then  (TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as DrAmt,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 then -1*TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt>0 then  (TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt) else 0 end end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code where TSPL_VENDOR_INVOICE_HEAD.TAX" + strii + "_Amt<0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
                If isLocationselect AndAlso Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                    qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
                End If

                ''==================Done By Monika(18/11/2016)======================================
                If isDocSelect AndAlso ArrDoc IsNot Nothing AndAlso ArrDoc.Count > 0 Then
                    qry += " and TSPL_VENDOR_INVOICE_HEAD.document_no in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
                End If
                If isVendorSelect AndAlso ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                    qry += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
                End If
                If isVGroupSelect AndAlso ArrVGroup IsNot Nothing AndAlso ArrVGroup.Count > 0 Then
                    qry += " and TSPL_VENDOR_Master.vendor_group_code in (" + clsCommon.GetMulcallString(ArrVGroup) + ") "
                End If
                ''=============================================================================
            Next
            qry += " union all" + Environment.NewLine
            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Discount_Amount) else 0 end end as DrAmt,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 then TSPL_VENDOR_INVOICE_HEAD.Discount_Amount else case when Document_Type in('D') and TSPL_VENDOR_INVOICE_HEAD.Discount_Amount<0 then -1*(TSPL_VENDOR_INVOICE_HEAD.Discount_Amount) else 0 end end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code where TSPL_VENDOR_INVOICE_HEAD.Discount_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
            If isLocationselect AndAlso Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If

            ''==================Done By Monika(18/11/2016)======================================
            If isDocSelect AndAlso ArrDoc IsNot Nothing AndAlso ArrDoc.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.document_no in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            End If
            If isVendorSelect AndAlso ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
            End If
            If isVGroupSelect AndAlso ArrVGroup IsNot Nothing AndAlso ArrVGroup.Count > 0 Then
                qry += " and TSPL_VENDOR_Master.vendor_group_code in (" + clsCommon.GetMulcallString(ArrVGroup) + ") "
            End If
            ''=============================================================================

            qry += " union all " + Environment.NewLine
            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else case when Document_Type in ('D') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount<0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount) else 0 end end as DrAmt,case when Document_Type in ('D') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount else case when Document_Type in('I','C') and TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount<0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then -1*(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount) else 0 end end as CrAmt from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code where TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
            If isLocationselect AndAlso Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If

            ''==================Done By Monika(18/11/2016)======================================
            If isDocSelect AndAlso ArrDoc IsNot Nothing AndAlso ArrDoc.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.document_no in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            End If
            If isVendorSelect AndAlso ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
            End If
            If isVGroupSelect AndAlso ArrVGroup IsNot Nothing AndAlso ArrVGroup.Count > 0 Then
                qry += " and TSPL_VENDOR_Master.vendor_group_code in (" + clsCommon.GetMulcallString(ArrVGroup) + ") "
            End If
            ''=============================================================================

            qry += " union all " + Environment.NewLine
            qry += " select RefDocNo,RefDocType ,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_REMITTANCE.Branch_GL_AC as ACCode,TSPL_GL_ACCOUNTS.Description as ACName,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then TSPL_REMITTANCE.Actual_Total_TDS else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_REMITTANCE.Actual_Total_TDS<0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then -1*(TSPL_REMITTANCE.Actual_Total_TDS) else 0 end end as DrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') and TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 then TSPL_REMITTANCE.Actual_Total_TDS else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('D') and TSPL_REMITTANCE.Actual_Total_TDS<0  then -1*(TSPL_REMITTANCE.Actual_Total_TDS) else 0 end end as CrAmt from TSPL_REMITTANCE left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE.Branch_GL_AC left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_REMITTANCE.Document_No left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code where TSPL_REMITTANCE.Actual_Total_TDS>0 and TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C','D') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= '' "
            If isLocationselect AndAlso Arrlocation IsNot Nothing AndAlso Arrlocation.Count > 0 Then
                qry += "and TSPL_GL_ACCOUNTS. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(Arrlocation) + ")"
            End If

            ''==================Done By Monika(18/11/2016)======================================
            If isDocSelect AndAlso ArrDoc IsNot Nothing AndAlso ArrDoc.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.document_no in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            End If
            If isVendorSelect AndAlso ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
            End If
            If isVGroupSelect AndAlso ArrVGroup IsNot Nothing AndAlso ArrVGroup.Count > 0 Then
                qry += " and TSPL_VENDOR_Master.vendor_group_code in (" + clsCommon.GetMulcallString(ArrVGroup) + ") "
            End If
            ''=============================================================================
            qry += " )Final left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=final.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code left outer join TSPL_USER_MASTER as CreatedBy on CreatedBy.User_Code=TSPL_VENDOR_INVOICE_HEAD.Created_By"
            qry += " left outer join TSPL_USER_MASTER as AuthorisedBy on AuthorisedBy .User_Code=TSPL_VENDOR_INVOICE_HEAD.Modify_By left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code where 2=2 "

            If isDocSelect AndAlso ArrDoc IsNot Nothing AndAlso ArrDoc.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.Document_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
            End If
            If isVGroupSelect AndAlso ArrVGroup IsNot Nothing AndAlso ArrVGroup.Count > 0 Then
                qry += " and TSPL_VENDOR_MASTER.Vendor_Group_Code in (" + clsCommon.GetMulcallString(ArrVGroup) + ") "
            End If
            If isVendorSelect AndAlso ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
            End If

            qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<= convert(date,'" + ToDate + "',103)"
            'qry += " order by Final.Document_No,Final.DrAmt desc,Final.CrAmt desc"

            qry = "select *,TSPL_COMPANY_MASTER .Logo_Img from (select MAX( FromDate) as FromDate,max(ToDate) as ToDate,max(Location) as Location,max(Vendor) as Vendor,max(Document) as Document,max(Document_No) as Document_No,max(ACCode) as ACCode,max(ACName) as ACName, case when SUM(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end   as DrAmt ,case when SUM( CrAmt-DrAmt)>0 then SUM( CrAmt-DrAmt) else 0 end   as CrAmt,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name,max(Vendor_Code) as Vendor_Code,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(RefDocNo) as RefDocNo,max(RefDocType) as RefDocType,max(Vendor_Name) as Vendor_Name,max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Due_Date) as Due_Date,max(Description) as Description,max(Vendor_Invoice_No) as Vendor_Invoice_No,max(Vendor_Invoice_Date) as Vendor_Invoice_Date,max(CreateBy) as CreateBy,max(ApproveBy) as ApproveBy,max(InvStatus) as InvStatus,max(InvoiceType) as InvoiceType,max(RefDocDescription) as RefDocDescription from(" + qry + " )xxx group by Document_No,ACCode )xxxx left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =xxxx.Comp_Code   order by xxxx.Document_No,xxxx.DrAmt desc,xxxx.CrAmt desc"
            Dim qry1 As String = "select Item_Code ,Item_Desc,TSPL_VENDOR_INVOICE_HEAD .Description ,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType   from TSPL_SRN_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo where RefDocType ='S'and TSPL_VENDOR_INVOICE_HEAD .Document_No in(" + clsCommon.GetMulcallString(ArrDoc) + ") and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= ''  "
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreport(CrystalReportFolder.Purchase, qry, qry1, "rptAPInvoice", "AP Invoice", "AP_InvoiceDetails.rpt")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

   
    Private Sub chkVGroupAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVGroupAll.ToggleStateChanged
        cbgVendorGroup.Enabled = Not chkVGroupAll.IsChecked
        If chkVGroupSelect.IsChecked = True Then
            'chkDocAll.IsChecked = True
            'chkVendorAll.IsChecked = True
            cbgVendorGroup.UnCheckedAll()
        End If
    End Sub
End Class
