'-Created By--[Pankaj Kumar Chaudhary]-Against Ticket No-[BM00000001367]
'' Anubhooti(1-July-2014) Added Export Permission Against BM00000003016 ''''''''
'' Puran Singh Negi. Ticket No- BM00000003435
''richa agarwal solved problem Ambiguous column at the time of fetching data BM00000006807

Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmCFormReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCFormReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        '' Anubhooti(1-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub FrmCFormReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'LoadVendor()
        LoadCustomer()
        'LoadLocation()
        gbCustomer.Enabled = True
        chkCustAll.Enabled = True
        chkCustSelect.Enabled = True
        cmbCustVend.SelectedIndex = 0
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        'chkVendorAll.IsChecked = True
        chkCustAll.IsChecked = True
        'chkLocAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

    End Sub

    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER Order By Cust_Code"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub
    '' Anubhooti added Loc Filter BM00000007023
    Sub LoadLoc(Optional ByVal WhrState As String = "")
        Dim qry As String = "select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER WHERE 1=1 "
        If clsCommon.myLen(WhrState) > 0 Then
            qry += " AND State='" & WhrState & "'"
        End If
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Sub LoadLocation()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        'LoadVendor()
        'LoadLocation()
        chkVendorAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkLocAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFormCode.Value = ""
        TxtState.Value = ""
        gv.DataSource = Nothing
    End Sub

    Private Sub cmbCustVend_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbCustVend.SelectedIndexChanged
        'gbCustomer.Enabled = True
        'gbVendor.Enabled = True
        'If clsCommon.CompairString(cmbCustVend.Text, "Customer") = CompairStringResult.Equal Then
        '    gbCustomer.Enabled = False
        'ElseIf clsCommon.CompairString(cmbCustVend.Text, "Vendor") = CompairStringResult.Equal Then
        '    gbVendor.Enabled = False
        'End If

        If clsCommon.CompairString(cmbCustVend.Text, "Customer") = CompairStringResult.Equal Then
            gbCustomer.HeaderText = "Customer"
            LoadCustomer()
        ElseIf clsCommon.CompairString(cmbCustVend.Text, "Vendor") = CompairStringResult.Equal Then
            gbCustomer.HeaderText = "Vendor"
            LoadVendor()
        ElseIf clsCommon.CompairString(cmbCustVend.Text, "Location") = CompairStringResult.Equal Then
            gbCustomer.HeaderText = "Location"
            LoadLoc()
            txtFormCode.Value = ""
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            LoadData(Exporter.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Dim dt As DataTable
    Dim qry As String
    Dim FromDate As String
    Dim ToDate As String
    Dim runDate As String
    ''changes by shivani against ticket no[BM00000007002]
    Sub LoadData(ByVal IsPrint As Exporter)
        Try
            'If clsCommon.myLen(txtFormCode.Value) <= 0 Then
            '    Throw New Exception("Please select Form Type First")

            'End If
            'If chkLocSelect.IsChecked And cbgLocation.CheckedValue.Count <= 0 Then
            '    Throw New Exception("Please select atleast single location or select all.")
            If chkCustSelect.IsChecked And cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single customer or select all.")
                'ElseIf chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count <= 0 Then
                'Throw New Exception("Please select atleast single vendor or select all.")
            End If

            Dim strTemp As String = ""
            If clsCommon.CompairString(ddl_SourceApplication.Text, "Pending") = CompairStringResult.Equal Then
                strTemp = " AND (ISNULL(CFormRecd,0)+ISNULL(CFormApplied,0))<=0"
            ElseIf clsCommon.CompairString(ddl_SourceApplication.Text, "Received") = CompairStringResult.Equal Then
                strTemp = " AND (ISNULL(CFormRecd,0)+ISNULL(CFormApplied,0))>0"
            End If

            FromDate = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
            ToDate = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            runDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE())

            qry = "Select CustVendCode, CustVendName,Tin_No as [Tin No], DocType,DocNo,convert(varchar, DocDate,103)as DocDate, Invoice_No as [Against Invoice No],Loc_Code as [From Location] ,Segment_name as [From Location Name],Description,Amount_Less_Discount as [Taxable Amount],Total_Tax_Amt as [Tax Amount], Amount,CFormAmount as [Received Amount],Amount-coalesce(CFormAmount,0) as [Pending Amount],case when Status=1 then 'Approved' else 'Pending' end as Status,State ,State_Code,ISNULL((Select State_Name From TSPL_STATE_MASTER Where TSPL_STATE_MASTER.STATE_CODE =xxx.State_Code ),'') As [State Name],TaxType as [Form Type]    from ("

            If clsCommon.CompairString(cmbCustVend.Text, "Vendor") = CompairStringResult.Equal Or clsCommon.CompairString(cmbCustVend.Text, "All") = CompairStringResult.Equal Then
                qry += " select * from ("
                'qry &= "Select distinct PI_No AS DocNo, PI_Date as DocDate,Tin_No, Description,Amount_Less_Discount,Total_Tax_Amt,CFormAmount, PI_Total_Amt as Amount, TSPL_PI_HEAD.Vendor_Code as CustVendCode, "
                'qry += " TSPL_VENDOR_MASTER.Vendor_Name as CustVendName, 'Purchase Invoice' as DocType, TSPL_PI_HEAD.Bill_To_Location as Location "
                'qry += " from TSPL_PI_HEAD inner join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=Tax_Group and TAX1_Rate=Tax_Rate and (_TYPE='C' or Against_C_Form=1 ) LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PI_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  left join TSPL_CForm_DETAIL on TSPL_CForm_DETAIL.Invoice_No=TSPL_PI_HEAD.PI_No "
                'qry += " WHERE TSPL_PI_HEAD.Status = 1 " + strTemp + " " 'AND Against_C_Form=1 
                'If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                '    qry += " AND TSPL_PI_HEAD.Vendor_Code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                'End If
                If clsCommon.CompairString(ddl_SourceApplication.Text, "Received") = CompairStringResult.Equal Or clsCommon.CompairString(ddl_SourceApplication.Text, "Both") = CompairStringResult.Equal Then
                    qry &= "select distinct TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as DocNo  ,convert(date,Invoice_Entry_Date,103) as DocDate,TSPL_VENDOR_MASTER.Tin_No,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount,TSPL_VENDOR_INVOICE_HEAD.Total_Tax as Total_Tax_Amt,CFormAmount" _
                        & " ,Document_Total as Amount,TSPL_VENDOR_MASTER.Vendor_Code as CustVendCode,TSPL_VENDOR_MASTER.Vendor_Name as CustVendName,'Vendor-Invoice' as DocType,  Loc_Code,Location_Desc,Against_POInvoice_No as Invoice_No, TSPL_PI_HEAD.Status,  " _
                        & " ISNULL((SELECT TOP 1 _TYPE FROM TSPL_TAX_RATES  WHERE Tax_Type='P' AND Tax_Rate IN (TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate ,TSPL_VENDOR_INVOICE_HEAD.TAX2_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX3_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX4_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX5_Rate ,TSPL_VENDOR_INVOICE_HEAD.TAX6_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX7_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX8_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX9_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX10_Rate) and Tax_Code in (TSPL_VENDOR_INVOICE_HEAD.TAX1,TSPL_VENDOR_INVOICE_HEAD.TAX2,TSPL_VENDOR_INVOICE_HEAD.TAX3,TSPL_VENDOR_INVOICE_HEAD.TAX4,TSPL_VENDOR_INVOICE_HEAD.TAX5,TSPL_VENDOR_INVOICE_HEAD.TAX6,TSPL_VENDOR_INVOICE_HEAD.TAX7,TSPL_VENDOR_INVOICE_HEAD.TAX8,TSPL_VENDOR_INVOICE_HEAD.TAX9,TSPL_VENDOR_INVOICE_HEAD.TAX10)),'') As TaxType,TSPL_VENDOR_MASTER.State ,TSPL_VENDOR_MASTER.State_Code " _
                        & " , TSPL_GL_SEGMENT_CODE.Description as  Segment_name from TSPL_VENDOR_INVOICE_HEAD " _
                        & " Inner join TSPL_PI_HEAD on Against_POInvoice_No=TSPL_PI_HEAD.PI_No " _
                    '"inner join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=TSPL_PI_HEAD.Tax_Group and TSPL_PI_HEAD.TAX1_Rate=Tax_Rate and (_TYPE='C') " _
                    qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  left join tspl_location_master on tspl_location_master.Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code  left join TSPL_CForm_DETAIL " _
                        & " on TSPL_CForm_DETAIL.Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt>0 AND  (ISNULL(CFormRecd,0)+ISNULL(CFormApplied,0))>0 and ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
                    If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND TSPL_VENDOR_INVOICE_HEAD.Vendor_Code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                    End If
                    If clsCommon.CompairString(ddl_SourceApplication.Text, "Both") = CompairStringResult.Equal Then
                        qry &= " Union All " & vbNewLine
                    End If
                End If
                If clsCommon.CompairString(ddl_SourceApplication.Text, "Pending") = CompairStringResult.Equal Or clsCommon.CompairString(ddl_SourceApplication.Text, "Both") = CompairStringResult.Equal Then
                    qry &= "select distinct TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as DocNo  ,convert(date,Invoice_Entry_Date,103) as DocDate,TSPL_VENDOR_MASTER.Tin_No,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount,TSPL_VENDOR_INVOICE_HEAD.Total_Tax as Total_Tax_Amt,CFormAmount" _
                        & " ,Document_Total as Amount,TSPL_VENDOR_MASTER.Vendor_Code as CustVendCode,TSPL_VENDOR_MASTER.Vendor_Name as CustVendName,'Vendor-Invoice' as DocType,  Loc_Code,Location_Desc,Against_POInvoice_No as Invoice_No, TSPL_PI_HEAD.Status,  " _
                        & " ISNULL((SELECT TOP 1 _TYPE FROM TSPL_TAX_RATES  WHERE Tax_Type='P' AND Tax_Rate IN (TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate ,TSPL_VENDOR_INVOICE_HEAD.TAX2_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX3_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX4_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX5_Rate ,TSPL_VENDOR_INVOICE_HEAD.TAX6_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX7_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX8_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX9_Rate,TSPL_VENDOR_INVOICE_HEAD.TAX10_Rate) and Tax_Code in (TSPL_VENDOR_INVOICE_HEAD.TAX1,TSPL_VENDOR_INVOICE_HEAD.TAX2,TSPL_VENDOR_INVOICE_HEAD.TAX3,TSPL_VENDOR_INVOICE_HEAD.TAX4,TSPL_VENDOR_INVOICE_HEAD.TAX5,TSPL_VENDOR_INVOICE_HEAD.TAX6,TSPL_VENDOR_INVOICE_HEAD.TAX7,TSPL_VENDOR_INVOICE_HEAD.TAX8,TSPL_VENDOR_INVOICE_HEAD.TAX9,TSPL_VENDOR_INVOICE_HEAD.TAX10)),'') As TaxType,TSPL_VENDOR_MASTER.State ,TSPL_VENDOR_MASTER.State_Code " _
                        & " , TSPL_GL_SEGMENT_CODE.Description as  Segment_name from TSPL_VENDOR_INVOICE_HEAD  "
                    ' & " inner join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group and TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate=Tax_Rate and (_TYPE='C') " _
                    qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  left join tspl_location_master on tspl_location_master.Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code  left join TSPL_CForm_DETAIL " _
                        & " on TSPL_CForm_DETAIL.Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No left join TSPL_PI_HEAD on Against_POInvoice_No=TSPL_PI_HEAD.PI_NO left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code  where TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "
                    If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND TSPL_VENDOR_INVOICE_HEAD.Vendor_Code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                    End If
                End If
                qry &= " )xx"
            End If
            If clsCommon.CompairString(cmbCustVend.Text, "All") = CompairStringResult.Equal Then
                qry += " Union ALL"
            End If
            If clsCommon.CompairString(cmbCustVend.Text, "Customer") = CompairStringResult.Equal Or clsCommon.CompairString(cmbCustVend.Text, "All") = CompairStringResult.Equal Then
                qry += " select * from ("
                'qry &= "Select distinct Document_Code as DocNo,Tin_No, Document_Date as DocDate, Description,Amount_Less_Discount,Total_Tax_Amt,CFormAmount, Total_Amt as Amount, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As CustVendCode, "
                'qry += " TSPL_CUSTOMER_MASTER.Customer_Name As CustVendName, 'Sale Invoice' as DocType, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location "
                'qry += " from TSPL_SD_SALE_INVOICE_HEAD inner join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=Tax_Group and TAX1_Rate=Tax_Rate and (_TYPE='C' or Against_C_Form=1 ) LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left join TSPL_CForm_DETAIL on TSPL_CForm_DETAIL.Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.Document_code"
                'qry += " WHERE TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " + strTemp + "" 'AND Against_C_Form=1
                'If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                '    qry += " AND TSPL_SD_SALE_INVOICE_HEAD.Customer_Code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                'End If
                If clsCommon.CompairString(ddl_SourceApplication.Text, "Received") = CompairStringResult.Equal Or clsCommon.CompairString(ddl_SourceApplication.Text, "Both") = CompairStringResult.Equal Then
                    qry &= "select distinct TSPL_Customer_Invoice_Head.Document_no as DocNo,TSPL_CUSTOMER_MASTER.tin_No,convert(date,TSPL_Customer_Invoice_Head.Document_date,103) as Docdate,TSPL_Customer_Invoice_Head.Description,TSPL_Customer_Invoice_Head.Tax1_BAmount as Amount_Less_Discount,Total_Tax as Total_Tax_Amt,CFormAmount,Document_Total  as Amount,TSPL_Customer_Invoice_Head.Customer_Code as CustVendCode," _
                        & " TSPL_CUSTOMER_MASTER.Customer_Name as CustVendName,case when TSPL_Customer_Invoice_Head.Document_Type='I' then 'AR-Invoice' when" _
                        & " TSPL_Customer_Invoice_Head.Document_Type='C' then'AR-Credit'  else 'AR-Invoice' end as DocType, " _
                        & " Loc_Code  ,Against_Sale_No  as Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Status, " _
                        & " ISNULL((SELECT TOP 1 _TYPE FROM TSPL_TAX_RATES  WHERE Tax_Type='S' AND Tax_Rate IN (TSPL_Customer_Invoice_Head.TAX1_Rate ,TSPL_Customer_Invoice_Head.TAX2_Rate,TSPL_Customer_Invoice_Head.TAX3_Rate,TSPL_Customer_Invoice_Head.TAX4_Rate,TSPL_Customer_Invoice_Head.TAX5_Rate ,TSPL_Customer_Invoice_Head.TAX6_Rate,TSPL_Customer_Invoice_Head.TAX7_Rate,TSPL_Customer_Invoice_Head.TAX8_Rate,TSPL_Customer_Invoice_Head.TAX9_Rate,TSPL_Customer_Invoice_Head.TAX10_Rate) and Tax_Code in (TSPL_Customer_Invoice_Head.TAX1,TSPL_Customer_Invoice_Head.TAX2,TSPL_Customer_Invoice_Head.TAX3,TSPL_Customer_Invoice_Head.TAX4,TSPL_Customer_Invoice_Head.TAX5,TSPL_Customer_Invoice_Head.TAX6,TSPL_Customer_Invoice_Head.TAX7,TSPL_Customer_Invoice_Head.TAX8,TSPL_Customer_Invoice_Head.TAX9,TSPL_Customer_Invoice_Head.TAX10)),'') As TaxType,'' AS  State,TSPL_CUSTOMER_MASTER.State AS State_Code,Location_Desc,TSPL_GL_SEGMENT_CODE.Description as Segment_name  " _
                        & " from TSPL_Customer_Invoice_Head Inner join TSPL_SD_SALE_INVOICE_HEAD on Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code "
                    ' & " inner join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate=Tax_Rate and (_TYPE='C'  or Against_C_Form=1 )  " _
                    qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code  left join TSPL_CForm_DETAIL " _
                        & " on TSPL_CForm_DETAIL.Invoice_No=TSPL_Customer_Invoice_Head.Against_Sale_No  left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_Customer_Invoice_Head.Loc_Code where TSPL_Customer_Invoice_Head.Status=1   AND (ISNULL(CFormRecd,0)+ISNULL(CFormApplied,0))>0 "
                    If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND TSPL_Customer_Invoice_Head.Customer_Code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                    End If

                    '================csa transfer document===BM00000004333=KDIL===================
                    qry += " union all "
                    qry += "select distinct tspl_csa_transfer_head.doc_code as DocNo,TSPL_CUSTOMER_MASTER.tin_No,convert(date,tspl_csa_transfer_head.transfer_date,103) as Docdate,'CSA Transfer' as Description,tspl_csa_transfer_head.Tax1_Base_Amt as Amount_Less_Discount,tspl_csa_transfer_head.Total_Tax_Amt,CFormAmount,Document_amount as Amount,tspl_csa_transfer_head.cust_code as CustVendCode," _
                        & " TSPL_CUSTOMER_MASTER.customer_name as CustVendName, 'CSA Transfer' as DocType, " _
                        & " from_location_code  as Loc_Code,'' as Invoice_No,tspl_csa_transfer_head.Status,ISNULL(tspl_csa_transfer_head.Against_Form,'') AS TaxType,TSPL_CUSTOMER_MASTER.State ,TSPL_CSA_TRANSFER_HEAD.State_Code,Location_Desc,TSPL_GL_SEGMENT_CODE.Description as Segment_name  from tspl_csa_transfer_head " _
                       ' & " inner join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=tspl_csa_transfer_head.Tax_Group and _TYPE='F'  " _
                    qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON tspl_csa_transfer_head.cust_code=TSPL_CUSTOMER_MASTER.Cust_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =tspl_csa_transfer_head.From_Location_Code  left join TSPL_CForm_DETAIL " _
                        & " on TSPL_CForm_DETAIL.Invoice_No=tspl_csa_transfer_head.doc_code left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=tspl_csa_transfer_head.From_Location_Code   where tspl_csa_transfer_head.Status=1 and TSPL_CSA_TRANSFER_HEAD.against_form='F' AND (ISNULL(CFormRecd,0)+ISNULL(CFormApplied,0))>0 "
                    If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND tspl_csa_transfer_head.cust_code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                    End If
                    '========================================================================

                    If clsCommon.CompairString(ddl_SourceApplication.Text, "Both") = CompairStringResult.Equal Then
                        qry &= " Union ALL " & vbNewLine
                    End If
                End If
                If clsCommon.CompairString(ddl_SourceApplication.Text, "Pending") = CompairStringResult.Equal Or clsCommon.CompairString(ddl_SourceApplication.Text, "Both") = CompairStringResult.Equal Then
                    qry &= "select distinct TSPL_Customer_Invoice_Head.Document_no as DocNo,TSPL_CUSTOMER_MASTER.tin_No,convert(date,TSPL_Customer_Invoice_Head.Document_date,103) as Docdate,TSPL_Customer_Invoice_Head.Description,TSPL_Customer_Invoice_Head.Tax1_BAmount as Amount_Less_Discount,Total_Tax as Total_Tax_Amt,CFormAmount,Document_Total  as Amount,TSPL_Customer_Invoice_Head.Customer_Code as CustVendCode," _
                        & " TSPL_CUSTOMER_MASTER.Customer_Name as CustVendName,case when TSPL_Customer_Invoice_Head.Document_Type='I' then 'AR-Invoice' when" _
                        & " TSPL_Customer_Invoice_Head.Document_Type='C' then'AR-Credit'  else 'AR-Invoice' end as DocType, " _
                        & " Loc_Code  ,Against_Sale_No  as Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Status," _
                        & " ISNULL((SELECT TOP 1 _TYPE FROM TSPL_TAX_RATES  WHERE Tax_Type='S' AND Tax_Rate IN (TSPL_Customer_Invoice_Head.TAX1_Rate ,TSPL_Customer_Invoice_Head.TAX2_Rate,TSPL_Customer_Invoice_Head.TAX3_Rate,TSPL_Customer_Invoice_Head.TAX4_Rate,TSPL_Customer_Invoice_Head.TAX5_Rate ,TSPL_Customer_Invoice_Head.TAX6_Rate,TSPL_Customer_Invoice_Head.TAX7_Rate,TSPL_Customer_Invoice_Head.TAX8_Rate,TSPL_Customer_Invoice_Head.TAX9_Rate,TSPL_Customer_Invoice_Head.TAX10_Rate) and Tax_Code in (TSPL_Customer_Invoice_Head.TAX1,TSPL_Customer_Invoice_Head.TAX2,TSPL_Customer_Invoice_Head.TAX3,TSPL_Customer_Invoice_Head.TAX4,TSPL_Customer_Invoice_Head.TAX5,TSPL_Customer_Invoice_Head.TAX6,TSPL_Customer_Invoice_Head.TAX7,TSPL_Customer_Invoice_Head.TAX8,TSPL_Customer_Invoice_Head.TAX9,TSPL_Customer_Invoice_Head.TAX10)),'') As TaxType ,'' AS State,TSPL_CUSTOMER_MASTER.State  as state_code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_GL_SEGMENT_CODE.Description as  Segment_name  " _
                        & "  from TSPL_Customer_Invoice_Head  "
                    ' & " inner join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=Tax_Group and TSPL_Customer_Invoice_Head.TAX1_Rate=Tax_Rate and (_TYPE='C')  " _
                    qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left join TSPL_CForm_DETAIL " _
                        & " on TSPL_CForm_DETAIL.Invoice_No=TSPL_Customer_Invoice_Head.Against_Sale_No  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_Customer_Invoice_Head.Loc_Code  left join TSPL_SD_SALE_INVOICE_HEAD on Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_Customer_Invoice_Head.Loc_Code  where TSPL_Customer_Invoice_Head.Status=1 "
                    If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND TSPL_Customer_Invoice_Head.Customer_Code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                    End If

                    '================csa transfer document==BM00000004333==KDIL===================


                    qry += " Union all "
                    qry += "select distinct TSPL_CSA_TRANSFER_HEAD.doc_code as DocNo,TSPL_CUSTOMER_MASTER.tin_No,convert(date,TSPL_CSA_TRANSFER_HEAD.transfer_date,103) as Docdate,'CSA Transfer' as Description,TSPL_CSA_TRANSFER_HEAD.Tax1_Base_Amt as Amount_Less_Discount,TSPL_CSA_TRANSFER_HEAD.Total_Tax_Amt,CFormAmount,Document_amount  as Amount,TSPL_CSA_TRANSFER_HEAD.cust_code as CustVendCode," _
                        & " TSPL_CUSTOMER_MASTER.Customer_Name as CustVendName,'CSA Transfer' as DocType, " _
                        & " TSPL_CSA_TRANSFER_HEAD.from_location_code  as Loc_Code,''  as Invoice_No,TSPL_CSA_TRANSFER_HEAD.Status,ISNULL(TSPL_CSA_TRANSFER_HEAD.Against_Form ,'') AS TaxType,TSPL_CUSTOMER_MASTER.State, tspl_csa_transfer_head.State_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GL_SEGMENT_CODE.Description as  Segment_name    from TSPL_CSA_TRANSFER_HEAD  " _
                    '& " inner join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=Tax_Group and (_TYPE='F')  " _
                    qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CSA_TRANSFER_HEAD.cust_code=TSPL_CUSTOMER_MASTER.Cust_Code  left join TSPL_CForm_DETAIL " _
                        & " on TSPL_CForm_DETAIL.Invoice_No=TSPL_CSA_TRANSFER_HEAD.doc_code    left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_CSA_TRANSFER_HEAD.From_Location_Code  where TSPL_CSA_TRANSFER_HEAD.Status=1 and TSPL_CSA_TRANSFER_HEAD.against_form='F' and (ISNULL(CFormRecd,0)+ISNULL(CFormApplied,0))<=0 "
                    If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                        qry += " AND TSPL_CSA_TRANSFER_HEAD.cust_code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                    End If
                    'and TSPL_CSA_TRANSFER_HEAD.TAX1_Rate=Tax_Rate
                End If

                qry &= " )xx"
            End If
            '' 16-June-2015 
            If clsCommon.CompairString(cmbCustVend.Text, "Location") = CompairStringResult.Equal Or clsCommon.CompairString(cmbCustVend.Text, "All") = CompairStringResult.Equal Then
                qry = " Select CustVendCode, CustVendName,Tin_No as [Tin No], DocType,DocNo, convert(varchar,DocDate,103)as DocDate, Invoice_No as [Against Invoice No],Description,Amount_Less_Discount as [Taxable Amount],Total_Tax_Amt as [Tax Amount], Amount,CFormAmount as [Received Amount],Amount-coalesce(CFormAmount,0) as [Pending Amount],case when Status=1 then 'Approved' else 'Pending' end as Status,[To_Location], To_Location_Desc ,Loc_Code as [From Location]  ,Location_Desc as [From Location Name],State ,State_Code,ISNULL((Select State_Name From TSPL_STATE_MASTER Where TSPL_STATE_MASTER.STATE_CODE =xxx.State_Code ),'') As [State Name],TaxType as [Form Type] From ( " & _
                      " Select *  from  ( " & _
                      " SELECT Document_No As DocNo,convert(date,Document_date,103) As DocDate,'' As Invoice_No,'' As [Tin_No],'' as CustVendCode,'' as CustVendName,'' As TinNo,Description,Amount_Less_Discount,Total_Tax_Amt  As Total_Tax_Amt,0 AS CFormAmount,DOC_Total_Amt As Amount,Remarks ,Status,'Transfer' as DocType,TSPL_LOCATION_MASTER.Location_Code AS To_Location,TSPL_LOCATION_MASTER.Location_Desc as [To_Location_Desc],TSPL_TRANSFER_ORDER_HEAD.From_Location As Loc_Code,TSPL_For_LOCATION_MASTER.Location_Desc,'F' As TaxType ,Form_Received AS Rcvd,'' As State ,TSPL_LOCATION_MASTER.State As State_Code  FROM TSPL_TRANSFER_ORDER_HEAD " & _
                      " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.GIT_Location= TSPL_TRANSFER_ORDER_HEAD.To_Location  left join TSPL_LOCATION_MASTER as TSPL_For_LOCATION_MASTER on TSPL_For_LOCATION_MASTER.Location_Code =TSPL_TRANSFER_ORDER_HEAD.From_Location  " & _
                      " Where Status = 1 and Is_AgainstFormF = 1 and DOC_Total_Amt > 0 AND Transfer_Type='O' " & _
                      " UNION ALL " & _
                      " SELECT DOC_CODE AS DocNo,convert(date,Transfer_Date,103) AS DocDate,'' As Invoice_No,'' As [Tin_No],TSPL_CSA_TRANSFER_HEAD.Cust_Code as CustVendCode,TSPL_CUSTOMER_MASTER.Customer_Name  as CustVendName,'' As TinNo,Description ,Amount_Less_Discount ,Total_Tax_Amt AS Total_Tax_Amt ,0 AS CFormAmount,Document_Amount AS Amount,'' AS Remarks,TSPL_CSA_TRANSFER_HEAD.Status,'CSA Transfer' as DocType,To_Location_Code AS To_Location,TSPL_LOCATION_MASTER.Location_Desc as To_Location_Desc  ,From_Location_Code As [From Location],TSPL_For_LOCATION_MASTER.Location_Desc,'F' As TaxType,CFormApplied AS Rcvd,'' As State ,State_Code  FROM TSPL_CSA_TRANSFER_HEAD " & _
                      " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CSA_TRANSFER_HEAD.Cust_Code " & _
                      " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.GIT_Location= TSPL_CSA_TRANSFER_HEAD.To_Location_Code left join TSPL_LOCATION_MASTER as TSPL_For_LOCATION_MASTER on TSPL_For_LOCATION_MASTER.Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code " & _
                      " WHERE 1=1 AND TSPL_CSA_TRANSFER_HEAD.Status =1 AND Against_Form='F' AND Document_Amount >0 " & _
                      " ) XX WHERE 1=1 "
                If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                    qry += " AND Loc_Code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                End If

                txtFormCode.Value = "F"
                If clsCommon.CompairString(ddl_SourceApplication.Text, "Pending") = CompairStringResult.Equal Then
                    qry += " AND XX.Rcvd = 0 "
                ElseIf clsCommon.CompairString(ddl_SourceApplication.Text, "Received") = CompairStringResult.Equal Then
                    qry += " AND XX.Rcvd = 1 "
                End If
            End If
            ''
            qry += " ) XXX WHERE XXX.TaxType='" & clsCommon.myCstr(txtFormCode.Value) & "' and DocDate>='" + clsCommon.GetPrintDate(dtpFromDate.Value, "yyyy-MM-dd") + "' AND DocDate<='" + clsCommon.GetPrintDate(dtpToDate.Value, "yyyy-MM-dd") + "' "
            If clsCommon.myLen(TxtState.Value) > 0 Then
                qry += "  AND XXX.State_Code='" & TxtState.Value & "' "
            End If
            'qry += ""
            'If chkLocSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
            '    qry += " AND Location IN (Select Location_Code from TSPL_LOCATION_MASTER WHERE Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
            'End If
            Dim MainQry As String = "select * from (" & qry & " )as m order by convert(date,DocDate ,103)"
            dt = clsDBFuncationality.GetDataTable(MainQry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Record Found")
            Else
                gv.DataSource = dt
                FormatGrid()
                ReStoreGridLayout()
            End If
            If IsPrint = Exporter.Refresh Then

            ElseIf IsPrint = Exporter.Print Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.CommonServices, dt, "crptCForm", "C-Form Report")
                frmCRV = Nothing
            ElseIf IsPrint = Exporter.Excel Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + " ")

                'If Not IsNothing(TxtState.Value) Then
                '    arrHeader.Add("State : " + clsCommon.myCstr(TxtState.Value))
                'End If
                If chkCustSelect.IsChecked Then
                    Dim stVSPName As String = ""
                    For Each StrName As String In cbgCustomer.CheckedDisplayMember
                        If clsCommon.myLen(stVSPName) > 0 Then
                            stVSPName += ", "
                        End If
                        stVSPName += StrName
                    Next
                    Dim strVSPCode As String = ""
                    For Each StrCode As String In cbgCustomer.CheckedValue
                        If clsCommon.myLen(strVSPCode) > 0 Then
                            strVSPCode += ", "
                        End If
                        strVSPCode += StrCode
                    Next
                    arrHeader.Add(("Customer: " + stVSPName + " "))
                End If
                clsCommon.MyExportToExcelGrid("C - Form Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("C- Form Report", gv, Nothing, "Shipment Detail", False)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub FormatGrid()
        '  Dim strItemCode, head2 As String
        gv.AllowAddNewRow = False

        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("DocNo").IsVisible = True
        gv.Columns("DocNo").Width = 100
        gv.Columns("DocNo").HeaderText = "Document No"

        gv.Columns("Against Invoice No").IsVisible = True
        gv.Columns("Against Invoice No").Width = 100
        gv.Columns("Against Invoice No").HeaderText = "Invoice No"

        gv.Columns("DocDate").IsVisible = True
        gv.Columns("DocDate").Width = 100
        gv.Columns("DocDate").HeaderText = "Document Date"
        gv.Columns("DocDate").FormatString = "{0:d}"

        gv.Columns("From Location").IsVisible = True
        gv.Columns("From Location").Width = 300
        gv.Columns("From Location").HeaderText = "From Location Code"

        gv.Columns("From Location Name").IsVisible = True
        gv.Columns("From Location Name").Width = 300

        'gv.Columns("To_Location_Desc").IsVisible = True
        'gv.Columns("To_Location_Desc").Width = 300

        gv.Columns("Tin No").IsVisible = True
        gv.Columns("Tin No").Width = 100

        gv.Columns("Amount").IsVisible = True
        gv.Columns("Amount").Width = 100

        gv.Columns("Taxable Amount").IsVisible = True
        gv.Columns("Taxable Amount").Width = 100

        gv.Columns("Tax Amount").IsVisible = True
        gv.Columns("Tax Amount").Width = 100

        gv.Columns("Received Amount").IsVisible = True
        gv.Columns("Received Amount").Width = 100

        gv.Columns("Pending Amount").IsVisible = True
        gv.Columns("Pending Amount").Width = 100

        gv.Columns("Form Type").IsVisible = True
        gv.Columns("Form Type").Width = 100

        If cmbCustVend.SelectedIndex = 0 OrElse cmbCustVend.SelectedIndex = 2 Then
            gv.Columns("CustVendCode").IsVisible = True
            gv.Columns("CustVendCode").Width = 100
            gv.Columns("CustVendCode").HeaderText = "Customer No"

            gv.Columns("CustVendName").IsVisible = True
            gv.Columns("CustVendName").Width = 200
            gv.Columns("CustVendName").HeaderText = "Customer Name"
        Else
            gv.Columns("CustVendCode").IsVisible = True
            gv.Columns("CustVendCode").Width = 100
            gv.Columns("CustVendCode").HeaderText = "Vendor No"

            gv.Columns("CustVendName").IsVisible = True
            gv.Columns("CustVendName").Width = 200
            gv.Columns("CustVendName").HeaderText = "Vendor Name"
        End If
        
        gv.Columns("DocType").IsVisible = True
        gv.Columns("DocType").Width = 100
        gv.Columns("DocType").HeaderText = "Document Type"

        gv.Columns("Status").IsVisible = True
        gv.Columns("Status").Width = 100

        gv.Columns("State").IsVisible = True
        gv.Columns("State").Width = 100

        gv.Columns("State_Code").IsVisible = True
        gv.Columns("State_Code").Width = 100
        gv.Columns("State_Code").HeaderText = "State Code"

        gv.Columns("State Name").IsVisible = True
        gv.Columns("State Name").Width = 100

        If clsCommon.CompairString(cmbCustVend.Text, "Location") = CompairStringResult.Equal Then
            gv.Columns("To_Location").IsVisible = True
            gv.Columns("To_Location").Width = 100
            gv.Columns("To_Location").HeaderText = "To Location"

            gv.Columns("To_Location_Desc").IsVisible = True
            gv.Columns("To_Location_Desc").Width = 100
            gv.Columns("To_Location_Desc").HeaderText = "To Location Name"

            gv.Columns("Against Invoice No").IsVisible = False
            gv.Columns("Tin No").IsVisible = False
            gv.Columns("Status").IsVisible = False
        Else
            gv.Columns("Against Invoice No").IsVisible = True
            gv.Columns("Tin No").IsVisible = True
            gv.Columns("Status").IsVisible = True
        End If

        RadPageView1.SelectedPage = RadPageViewPage2
        gv.BestFitColumns()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmRptVendorLedger_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            LoadData(Exporter.Print)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        'Try
        '    If (gv.Rows.Count <= 0) Then
        '        common.clsCommon.MyMessageBoxShow("No Data To Export")
        '        Exit Sub
        '    End If
        '    LoadData(Exporter.Excel)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        'Try
        '    If (gv.Rows.Count <= 0) Then
        '        common.clsCommon.MyMessageBoxShow("No Data To Export")
        '        Exit Sub
        '    End If
        '    LoadData(Exporter.PDF)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
        Export(EnumExportTo.PDF)
    End Sub
     
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cmbCustVend.Text)
            TemplateGridview = gv
            LoadData(Exporter.Refresh)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            LoadData(Exporter.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Dim DocNo As String
        Dim DocType As String
        DocNo = clsCommon.myCstr(gv.CurrentRow.Cells("Against Invoice No").Value)
        DocType = clsCommon.myCstr(gv.CurrentRow.Cells("DocType").Value)

        If clsCommon.CompairString(DocType, "CSA Transfer") = CompairStringResult.Equal OrElse clsCommon.CompairString(DocType, "Transfer") = CompairStringResult.Equal Then
            DocNo = clsCommon.myCstr(gv.CurrentRow.Cells("DocNo").Value)
        End If

        If clsCommon.CompairString(cmbCustVend.Text, "Customer") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbCustVend.Text, "Location") = CompairStringResult.Equal Then
            If clsCommon.CompairString(DocType, "CSA Transfer") = CompairStringResult.Equal Then
                Dim frm As New frmCSATransfer
                frm.SetUserMgmt(clsUserMgtCode.frmCSATransfer)
                frm.StrDocNo = DocNo
                frm.Show()
            ElseIf clsCommon.CompairString(DocType, "Transfer") = CompairStringResult.Equal Then
                Dim frmT As New FrmTransferKDIL
                frmT.SetUserMgmt(clsUserMgtCode.Transfer)
                frmT.Show()
                frmT.LoadData(DocNo, NavigatorType.Current)
            Else
                Dim frm As New frmSNSaleInvoice
                frm.SetUserMgmt(clsUserMgtCode.frmSNSaleInvoice)
                frm.Show()
                frm.LoadData(DocNo, NavigatorType.Current)
            End If
        Else
            Dim frm As New frmPurchaseInvoice
            frm.SetUserMgmt(clsUserMgtCode.mbtnPurchaseInvoice)
            frm.Show()
            frm.LoadData(DocNo, NavigatorType.Current)
        End If

    End Sub

   
    Private Sub txtFormCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtFormCode._MYValidating
        Dim qry1 As String = String.Empty
        Dim WhrCls As String = String.Empty

        If clsCommon.CompairString(cmbCustVend.Text, "Location") = CompairStringResult.Equal Then
            WhrCls = " ISNULL(Form_Type,'') ='F' "
        Else
            WhrCls = ""
        End If
        qry1 = "select Form_Code as [Code] ,Form_Name as [Name], Form_Type as [Type] from TSPL_Form_Master"
        txtFormCode.Value = clsCommon.ShowSelectForm("Formtype", qry1, "Type", WhrCls, txtFormCode.Value, "Type", isButtonClicked)
    End Sub

   
    Private Sub TxtState__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtState._MYValidating
        TxtState.Value = clsStateMaster.getFinder("", TxtState.Value, isButtonClicked)
        If clsCommon.myLen(TxtState.Value) > 0 Then
            LblState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select ISNULL(STATE_NAME,'') AS STATE_NAME From TSPL_STATE_MASTER WHERE STATE_CODE='" & TxtState.Value & "'"))
            If clsCommon.CompairString(cmbCustVend.Text, "Location") = CompairStringResult.Equal Then
                LoadLoc(TxtState.Value)
            End If
        Else
            LblState.Text = ""
            If clsCommon.CompairString(cmbCustVend.Text, "Location") = CompairStringResult.Equal Then
                LoadLoc("")
            End If
        End If

    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmCFormReport & "'"))
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + " ")

            If clsCommon.myLen(TxtState.Value) > 0 Then
                arrHeader.Add("State : " + clsCommon.myCstr(LblState.Text))
            End If
            If chkCustSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgCustomer.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Customer: " + stVSPName + " "))
            End If

            If chkVendorSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Location: " + stVSPName + " "))
            End If

            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)

            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("C - Form Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
End Class
