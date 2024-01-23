Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports XpertERPEngine
Imports Telerik.WinControls.UI


Public Class rptPurchaseReco
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLocation As ArrayList
    Public arrCustomer As ArrayList
    Public arrCustGroup As ArrayList
    Public arrItemGroup As ArrayList
    Public boolChecked As Boolean
    Public arrGLAccount As ArrayList
    Public Stocking_Uom As Boolean = False
    '' new filters
    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim strPivotForAddChargeFinalOutersumQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
#End Region

    Private Sub rptVendReco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyLabel9.Visible = True
        cboType.Visible = True
        btnBack.Visible = True
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        LoadReportTypes()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        Document_No = ""
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtLocation.Enabled = True
        txtTransaction.Enabled = True
        txtCustomer.Enabled = True
        Gv1.DataSource = Nothing
        cboType.SelectedValue = "Account Wise"
        RadPageViewPage2.Text = clsCommon.myCstr(cboType.SelectedValue)
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtLocation.arrValueMember = arrLocation
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            cboType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
    End Sub

    Sub LoadReportTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Account Wise")
        dt.Rows.Add("Vendor And Account Wise")
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Detail")
        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptPurReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Purchase Register:" + cboType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Purchase Register" + cboType.SelectedValue, Gv1, arrHeader, "Purchase Register", True)
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing

            Dim strMain As String = Nothing

            Dim qry As String = "select GL.Account_code,gl.Account_Desc, xx.TransType,xx.TransDesc,xx.Loc_Code,xx.Document_No,xx.Document_Date,xx.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Vendor_Group_Code,TSPL_VENDOR_GROUP.Description as Group_Desc,xx.RefDocNo,xx.Document_Type,GL.Voucher_No,gl.Voucher_Date,xx.Amount,xx.GL_Account_Code,case when Document_Type in ('D') then 0 else Amount  end DrAmount,case when Document_Type in ('D') then Amount  else 0 end CrAmount,Amount * case when Document_Type in ('D') then -1  else 1 end as NetAmount, isnull( GL.DRAmount,0) as GLDrAmount,isnull(GL.CRAmount,0) as GLCrAmount ,(isnull( GL.DRAmount,0) -isnull(GL.CRAmount,0)) as GLNetAmount,((Amount * case when Document_Type in ('D') then -1  else 1 end)-(isnull( GL.DRAmount,0) -isnull(GL.CRAmount,0))) as DiffAmount from ( " + Environment.NewLine + _
"select TransType,max(TransDesc) as TransDesc,Loc_Code,Document_No,max(Document_Date) as Document_Date,Vendor_Code,RefDocNo,Document_Type,sum(Amount ) as Amount ,GL_Account_Code from ( select case when TSPL_PI_HEAD.Document_Type='MT' then TSPL_PI_HEAD.Document_Type else 'PI' end as TransType, " + Environment.NewLine + _
"case when TSPL_PI_HEAD.Document_Type='MT' then 'Merchant Trade' else  'Purchase Invoice' end As TransDesc" + Environment.NewLine + _
",TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  as Document_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Document_Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Document_No as RefDocNo,TSPL_VENDOR_INVOICE_HEAD.Document_Type,cast(TSPL_VENDOR_INVOICE_HEAD.ConvRate* (TSPL_VENDOR_INVOICE_DETAIL.Landed_Amount+TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount-(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then 0 else TSPL_PI_DETAIL.Shortage_Amount end)) as decimal(18,2)) as Amount,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine + _
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No" + Environment.NewLine + _
"left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No" + Environment.NewLine + _
"left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No and TSPL_PI_DETAIL.Line_No=TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No" + Environment.NewLine + _
"            where TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No Is Not null" + Environment.NewLine + _
"and CAST(TSPL_VENDOR_INVOICE_HEAD.Posting_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'  " + Environment.NewLine
            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            If Not txtCustomer.arrValueMember Is Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
            End If
            qry += "            union all" + Environment.NewLine + _
"select  " + Environment.NewLine + _
"case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then 'PR' " + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No is not null then 'Bulk-PI' " + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No is not null then 'MCC-PI'" + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then 'Bulk-PI-Ret' " + Environment.NewLine + _
"else '' end as TransType" + Environment.NewLine + _
",case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then 'Purchase Return' " + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No is not null then 'Bulk Milk Purchase' " + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No is not null then 'Milk Purchase Invoice' " + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then 'Bulk Milk Purchase Return'" + Environment.NewLine + _
"else '' end  As TransDesc,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,  case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No " + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No" + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No" + Environment.NewLine + _
"when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then TSPL_VENDOR_INVOICE_HEAD.RefDocNo" + Environment.NewLine + _
"else TSPL_VENDOR_INVOICE_HEAD.Document_No end as Document_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Document_Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code," + Environment.NewLine + _
"TSPL_VENDOR_INVOICE_HEAD.Document_No as RefDocNo,TSPL_VENDOR_INVOICE_HEAD.Document_Type,cast(TSPL_VENDOR_INVOICE_HEAD.ConvRate*((case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then isnull(TSPL_VENDOR_INVOICE_DETAIL.Landed_Amount,0) else 0 end)+TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount )as decimal(18,2)) as Amount,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code " + Environment.NewLine + _
"                    from TSPL_VENDOR_INVOICE_DETAIL" + Environment.NewLine + _
"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No" + Environment.NewLine + _
"where ( TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null or TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No is not null or (TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No is not null  and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I') or TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR')" + Environment.NewLine + _
"and CAST(TSPL_VENDOR_INVOICE_HEAD.Posting_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            If Not txtCustomer.arrValueMember Is Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
            End If

            qry += "union all" + Environment.NewLine + _
"select 'MCC-TR' as TransType, 'MCC Transfer' As TransDesc, TSPL_LOCATION_MASTER.Loc_Segment_Code as Loc_Code, TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as Document_No,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date as Document_Date,null as Vendor_Code,null as RefDocNo,'C' as Document_Type,TSPL_MILK_TRANSFER_IN.Document_Amount,null as GL_Account_Code" + Environment.NewLine + _
"from TSPL_MILK_TRANSFER_IN " + Environment.NewLine + _
"left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No" + Environment.NewLine + _
"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_TRANSFER_IN.location_code" + Environment.NewLine + _
"where CAST(TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If

            qry += "union all" + Environment.NewLine + _
"select 'MCC-TR-RET' as TransType, 'MCC Transfer Return' As TransDesc, TSPL_LOCATION_MASTER.Loc_Segment_Code as Loc_Code, TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No as Document_No,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date as Document_Date,null as Vendor_Code,null as RefDocNo,'D' as Document_Type,TSPL_MILK_TRANSFER_IN.Document_Amount,null as GL_Account_Code" + Environment.NewLine + _
"from TSPL_MILK_TRANSFER_IN_RETURN" + Environment.NewLine + _
"left outer join  TSPL_MILK_TRANSFER_IN  on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No" + Environment.NewLine + _
"left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No" + Environment.NewLine + _
"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_TRANSFER_IN.location_code" + Environment.NewLine + _
"where CAST(TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            qry += "union all" + Environment.NewLine + _
"select 'Transfer' as  TransType,'Transfer' As TransDesc,TSPL_LOCATION_MASTER.Loc_Segment_Code as Loc_Code,TSPL_TRANSFER_ORDER_HEAD.Document_No,TSPL_TRANSFER_ORDER_HEAD.Document_Date,null as Vendor_Code,null as RefDocNo,'C' as Document_Type, (TSPL_TRANSFER_ORDER_HEAD.Discount_Base" + Environment.NewLine + _
") as DOC_Total_Amt,null as GL_Account_Code" + Environment.NewLine + _
"from TSPL_TRANSFER_ORDER_HEAD  " + Environment.NewLine + _
"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location" + Environment.NewLine + _
"where Transfer_Type='I' and CAST(TSPL_TRANSFER_ORDER_HEAD.Document_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            qry += "union all" + Environment.NewLine + _
"select 'Transfer RET' as  TransType,'Transfer Return' As TransDesc,TSPL_LOCATION_MASTER.Loc_Segment_Code as Loc_Code,TSPL_TRANSFER_RETURN.Document_No,TSPL_TRANSFER_RETURN.Document_Date,null as Vendor_Code,null as RefDocNo,'D' as Document_Type, (TSPL_TRANSFER_ORDER_HEAD.Discount_Base" + Environment.NewLine + _
") as DOC_Total_Amt,null as GL_Account_Code" + Environment.NewLine + _
"from TSPL_TRANSFER_RETURN" + Environment.NewLine + _
"left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_RETURN.Transfer_No  " + Environment.NewLine + _
"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location" + Environment.NewLine + _
"where Transfer_Type='I' and TSPL_TRANSFER_RETURN.DOcument_Type='I' and CAST(TSPL_TRANSFER_ORDER_HEAD.Document_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'"
            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            qry += ")x Group by TransType,Loc_Code,Document_No,Vendor_Code,RefDocNo,Document_Type,GL_Account_Code" + Environment.NewLine + _
            ")xx" + Environment.NewLine + _
         "left outer join (" + Environment.NewLine + _
         "select xxx.Voucher_No,max(xxx.Voucher_Date) as Voucher_Date,xxx.Source_Doc_No,max(xxx.Segment_code) as Segment_code, xxx.Account_code,max(xxx.Account_Desc) as Account_Desc," + Environment.NewLine + _
         "sum(xxx.Amount * case when xxx.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine + _
         "sum(xxx.Amount * case when xxx.Amount<0 then -1 else 0 end  ) as CRAmount,max(xxx.Account_Seg_Code7) as Account_Seg_Code7 " + Environment.NewLine + _
         "from (" + Environment.NewLine + _
         "select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Voucher_Date,TSPL_JOURNAL_MASTER.Source_Doc_No,TSPL_JOURNAL_MASTER.Segment_code, TSPL_JOURNAL_DETAILS.Account_code ,TSPL_GL_ACCOUNTS.Description as Account_Desc,TSPL_JOURNAL_DETAILS.Amount ,isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'') as Reco_Control_Account,TSPL_GL_ACCOUNTS.Account_Seg_Code7" + Environment.NewLine + _
         "from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
         "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code" + Environment.NewLine + _
         "inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No  " + Environment.NewLine + _
         "where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='P' and " + Environment.NewLine + _
         "CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") & "' " + Environment.NewLine

            qry += ")xxx" + Environment.NewLine + _
            "group by xxx.Voucher_No,xxx.Source_Doc_No,xxx.Account_code having sum(xxx.Amount)<>0  " + Environment.NewLine + _
            ")GL on GL.Source_Doc_No= (case when len(isnull(RefDocNo,''))>0  then RefDocNo else xx.Document_No end) and 2=(case when xx.GL_Account_Code is null then 2 else (case when xx.GL_Account_Code=gl.Account_code then 2 else 3 end) end )  " + Environment.NewLine + _
            "left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= xx.Vendor_Code" + Environment.NewLine + _
            "left outer join  TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code= TSPL_VENDOR_MASTER.Vendor_Group_Code" + Environment.NewLine + _
            " Where 2=2 "
            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                qry += " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Account,'') in (" & clsCommon.GetMulcallString(txtMultAccountNo.arrValueMember) & ") "
            End If
            If fndMultiVendorGroup.arrValueMember IsNot Nothing AndAlso fndMultiVendorGroup.arrValueMember.Count > 0 Then
                qry += " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Group_Code,'') in (" & clsCommon.GetMulcallString(fndMultiVendorGroup.arrValueMember) & ") "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and GL.Segment_code in  (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + "))"
            End If
            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                qry += " AND (coalesce(GL.Account_code,'') in (" & clsCommon.GetMulcallString(txtMultAccountNo.arrValueMember) & ")) "
            End If

            If chkMismatchDoc.Checked Then
                qry += " and abs(isnull(((Amount * case when Document_Type in ('D') then -1  else 1 end)-(isnull( GL.DRAmount,0) -isnull(GL.CRAmount,0))),0))>0"
            End If



            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                qry = "select xxxxx.*,isnull( JEOther.DRAmount,0)  as OtherDRAmount, isnull( JEOther.CRAmount,0)  as OtherCRAmount,(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)) as OtherNetAmount,(GLNetAmount+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0))) as TotalTrial  from (" + Environment.NewLine + _
                "select Account_code,max(Account_Desc) as Account_Desc,sum(DRAmount) as DRAmount,sum(CRAmount) as CRAmount,sum(NetAmount) as NetAmount,sum(GLDRAmount) as GLDRAmount,sum(GLCRAmount) as GLCRAmount,sum(GLNetAmount) as GLNetAmount,sum(DiffAmount) as DiffAmount " + Environment.NewLine + _
                "from (" + qry + ")Final group by Account_code" + Environment.NewLine + _
                 ")xxxxx  " + Environment.NewLine + _
                 "left outer join (select TSPL_JOURNAL_DETAILS.Account_code,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine + _
                "sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as CRAmount " + Environment.NewLine + _
                " from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine + _
                " where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'P' "
                qry += ") and TSPL_JOURNAL_MASTER.Authorized='A' and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + " ' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' group by TSPL_JOURNAL_DETAILS.Account_code ) JEOther on  JEOther.Account_code=xxxxx.Account_code"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal Then
                qry = "select  max(Vendor_Group_Code) as Vendor_Group_Code,max(Group_Desc) as Group_Desc,Vendor_Code,max(Vendor_Name) as Vendor_Name, Account_code,max(Account_Desc) as Account_Desc,sum(DRAmount) as DRAmount,sum(CRAmount) as CRAmount,sum(NetAmount) as NetAmount,sum(GLDRAmount) as GLDRAmount,sum(GLCRAmount) as GLCRAmount,sum(GLNetAmount) as GLNetAmount,sum(DiffAmount) as DiffAmount " + Environment.NewLine + _
                "from (" + qry + ")Final group by Vendor_Code,Account_code"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise") = CompairStringResult.Equal Then
                If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                    Throw New Exception("Filter account no will not work with document wise report.")
                End If
                qry = "select Reco.*,isnull(PURReg.Landed_Cost_Amount,0) as Landed_Cost_Amount,(abs(NetAmount)-isnull(Landed_Cost_Amount,0)) as Landed_Cost_Amount_Diff from  ( " + Environment.NewLine + _
                "select  max(TransType) as TransType,max(TransDesc) as TransDesc,max(Loc_Code) as Loc_Code,Document_No,max(Document_Date) as Document_Date,Vendor_Code,max(Vendor_Name) as Vendor_Name,max(Vendor_Group_Code) as Vendor_Group_Code,max(Group_Desc) asGroup_Desc,max(RefDocNo) as RefDocNo,max(Document_Type) as Document_Type,max(Voucher_No) as Voucher_No,max(Voucher_Date) as Voucher_Date,sum(DRAmount) as DRAmount,sum(CRAmount) as CRAmount,sum(NetAmount) as NetAmount,sum(GLDRAmount) as GLDRAmount,sum(GLCRAmount) as GLCRAmount,sum(GLNetAmount) as GLNetAmount,sum(DiffAmount) as DiffAmount" + Environment.NewLine + _
                "from (" + qry + ")Final group by TransType,Document_No,Vendor_Code" + Environment.NewLine + _
                " )Reco" + Environment.NewLine + _
                " left outer join (" + Environment.NewLine + _
                "  select [Document No],[Vendor Code],sum(Landed_Cost_Amount) as Landed_Cost_Amount" + Environment.NewLine + _
                "  from (" + Environment.NewLine + _
                " --------------Purchase Register Qeury" + Environment.NewLine + _
                ReturnPurRegQuery() + Environment.NewLine + _
                " --------------End of Purchase Register Qeury" + Environment.NewLine + _
                " )xx group by [Invoice Type],[Document No],[Vendor Code]" + Environment.NewLine + _
                 ") PURReg on PURReg.[Document No]=Reco.Document_No and PURReg.[Vendor Code]=Reco.Vendor_Code" + Environment.NewLine + _
                 "order by Document_Date"

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            Else
                Throw New Exception("Wrong Report type")
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            RadPageViewPage2.Text = cboType.Text
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            Gv1.Tag = cboType.SelectedValue
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            Else
                EnableDisableAllControl(False)
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            'FindAndRestoreGridLayout(Me)
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub


    Function ReturnPurRegQuery() As String
        Dim qryList As ArrayList
        qryList = clsPurchaseInvoiceHead.ReturnQuery(True, clsCommon.GetDateWithStartTime(fromDate.Value), clsCommon.GetDateWithEndTime(ToDate.Value), Unit_Code, Stocking_Uom)
        Dim strMCCMaterial As String = qryList(0)
        strPivotForFinalOuterQuery = qryList(1)
        strPivotForAddChargeFinalOutersumQuery = qryList(2)
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Trans Type] in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") "
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Vendor Code] in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
        End If
        If arrCustGroup IsNot Nothing AndAlso arrCustGroup.Count > 0 Then
            strMCCMaterial += " and coalesce(Cust.Vendor_Group_Code,'') in (" + clsCommon.GetMulcallString(arrCustGroup) + ") "
        End If
        If clsCommon.myLen(Document_No) > 0 Then
            strMCCMaterial += " and coalesce(xx.[Document No],'') ='" & clsCommon.myCstr(Document_No) & "' "
        End If
        Dim strWhrCatg As String = ""
        strMCCMaterial += " and xx.Status=1  "
        Return strMCCMaterial
    End Function

    '    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
    '        Try
    '            Dim arrHeader As List(Of String) = New List(Of String)()
    '            Dim strTemp As String = ""
    '            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + " ")
    '            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

    '            If clsCommon.myLen(cboType.Text) > 0 Then
    '                arrHeader.Add("Report Type : " + cboType.Text)
    '            End If

    '            If Not IsNothing(txtLocation.arrValueMember) Then
    '                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
    '            End If

    '            If Not IsNothing(txtCustomer.arrValueMember) Then
    '                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
    '            End If

    '            If IsPrint = Exporter.Excel Then
    '                clsCommon.MyExportToExcelGrid(" Purchase Register:" + cboType.SelectedValue, Gv1, arrHeader, Me.Text)
    '                Exit Sub
    '            ElseIf IsPrint = Exporter.PDF Then
    '                clsCommon.MyExportToPDF("Purchase Register" + cboType.SelectedValue, Gv1, arrHeader, "Purchase Register", True)
    '                Exit Sub
    '            End If
    '            clsCommon.ProgressBarShow()
    '            Gv1.DataSource = Nothing
    '            Gv1.Rows.Clear()

    '            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
    '            Dim str As String = ""
    '            Dim dt As DataTable = Nothing

    '            Dim strMain As String = Nothing

    '            Dim qry As String = "select GL.Account_code,gl.Account_Desc, xx.TransType,xx.TransDesc,xx.Loc_Code,xx.Document_No,xx.Document_Date,xx.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Vendor_Group_Code,TSPL_VENDOR_GROUP.Description as Group_Desc,xx.RefDocNo,xx.Document_Type,GL.Voucher_No,gl.Voucher_Date,xx.Amount,xx.GL_Account_Code,case when Document_Type in ('D') then 0 else Amount  end DrAmount,case when Document_Type in ('D') then Amount  else 0 end CrAmount,Amount * case when Document_Type in ('D') then -1  else 1 end as NetAmount, isnull( GL.DRAmount,0) as GLDrAmount,isnull(GL.CRAmount,0) as GLCrAmount ,(isnull( GL.DRAmount,0) -isnull(GL.CRAmount,0)) as GLNetAmount,((Amount * case when Document_Type in ('D') then -1  else 1 end)-(isnull( GL.DRAmount,0) -isnull(GL.CRAmount,0))) as DiffAmount from ( " + Environment.NewLine + _
    '"select TransType,max(TransDesc) as TransDesc,Loc_Code,Document_No,max(Document_Date) as Document_Date,Vendor_Code,RefDocNo,Document_Type,sum(Amount ) as Amount ,GL_Account_Code from ( select case when TSPL_PI_HEAD.Document_Type='MT' then TSPL_PI_HEAD.Document_Type else 'PI' end as TransType, " + Environment.NewLine + _
    '"case when TSPL_PI_HEAD.Document_Type='MT' then 'Merchant Trade' else  'Purchase Invoice' end As TransDesc" + Environment.NewLine + _
    '",TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  as Document_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Document_Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Document_No as RefDocNo,TSPL_VENDOR_INVOICE_HEAD.Document_Type,cast(TSPL_VENDOR_INVOICE_HEAD.ConvRate* (TSPL_VENDOR_INVOICE_DETAIL.Landed_Amount+TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount-(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then 0 else TSPL_PI_DETAIL.Shortage_Amount end)) as decimal(18,2)) as Amount,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine + _
    '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No" + Environment.NewLine + _
    '"left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No" + Environment.NewLine + _
    '"left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No and TSPL_PI_DETAIL.Line_No=TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No" + Environment.NewLine + _
    '"            where TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No Is Not null" + Environment.NewLine + _
    '"and CAST(TSPL_VENDOR_INVOICE_HEAD.Posting_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'  " + Environment.NewLine
    '            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '                qry += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
    '            End If
    '            If Not txtCustomer.arrValueMember Is Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
    '                qry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
    '            End If
    '            qry += "            union all" + Environment.NewLine + _
    '"select  " + Environment.NewLine + _
    '"case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then 'PR' " + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No is not null then 'Bulk-PI' " + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No is not null then 'MCC-PI'" + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then 'Bulk-PI-Ret' " + Environment.NewLine + _
    '"else '' end as TransType" + Environment.NewLine + _
    '",case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then 'Purchase Return' " + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No is not null then 'Bulk Milk Purchase' " + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No is not null then 'Milk Purchase Invoice' " + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then 'Bulk Milk Purchase Return'" + Environment.NewLine + _
    '"else '' end  As TransDesc,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,  case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No " + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No" + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No" + Environment.NewLine + _
    '"when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then TSPL_VENDOR_INVOICE_HEAD.RefDocNo" + Environment.NewLine + _
    '"else TSPL_VENDOR_INVOICE_HEAD.Document_No end as Document_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Document_Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code," + Environment.NewLine + _
    '"TSPL_VENDOR_INVOICE_HEAD.Document_No as RefDocNo,TSPL_VENDOR_INVOICE_HEAD.Document_Type,cast(TSPL_VENDOR_INVOICE_HEAD.ConvRate*((case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then isnull(TSPL_VENDOR_INVOICE_DETAIL.Landed_Amount,0) else 0 end)+TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount )as decimal(18,2)) as Amount,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code " + Environment.NewLine + _
    '"                    from TSPL_VENDOR_INVOICE_DETAIL" + Environment.NewLine + _
    '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No" + Environment.NewLine + _
    '"where ( TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null or TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No is not null or (TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No is not null  and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I') or TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR')" + Environment.NewLine + _
    '"and CAST(TSPL_VENDOR_INVOICE_HEAD.Posting_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
    '            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '                qry += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
    '            End If
    '            If Not txtCustomer.arrValueMember Is Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
    '                qry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
    '            End If

    '            qry += "union all" + Environment.NewLine + _
    '"select 'MCC-TR' as TransType, 'MCC Transfer' As TransDesc, TSPL_LOCATION_MASTER.Loc_Segment_Code as Loc_Code, TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as Document_No,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date as Document_Date,null as Vendor_Code,null as RefDocNo,'C' as Document_Type,TSPL_MILK_TRANSFER_IN.Document_Amount,null as GL_Account_Code" + Environment.NewLine + _
    '"from TSPL_MILK_TRANSFER_IN " + Environment.NewLine + _
    '"left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No" + Environment.NewLine + _
    '"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_TRANSFER_IN.location_code" + Environment.NewLine + _
    '"where CAST(TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
    '            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
    '            End If

    '            qry += "union all" + Environment.NewLine + _
    '"select 'MCC-TR-RET' as TransType, 'MCC Transfer Return' As TransDesc, TSPL_LOCATION_MASTER.Loc_Segment_Code as Loc_Code, TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No as Document_No,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date as Document_Date,null as Vendor_Code,null as RefDocNo,'D' as Document_Type,TSPL_MILK_TRANSFER_IN.Document_Amount,null as GL_Account_Code" + Environment.NewLine + _
    '"from TSPL_MILK_TRANSFER_IN_RETURN" + Environment.NewLine + _
    '"left outer join  TSPL_MILK_TRANSFER_IN  on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No" + Environment.NewLine + _
    '"left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No" + Environment.NewLine + _
    '"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_TRANSFER_IN.location_code" + Environment.NewLine + _
    '"where CAST(TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
    '            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
    '            End If
    '            qry += "union all" + Environment.NewLine + _
    '"select 'Transfer' as  TransType,'Transfer' As TransDesc,TSPL_LOCATION_MASTER.Loc_Segment_Code as Loc_Code,TSPL_TRANSFER_ORDER_HEAD.Document_No,TSPL_TRANSFER_ORDER_HEAD.Document_Date,null as Vendor_Code,null as RefDocNo,'C' as Document_Type, (TSPL_TRANSFER_ORDER_HEAD.Discount_Base" + Environment.NewLine + _
    '"+ case when TabTax1.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX1_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax2.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX2_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax3.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX3_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax4.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX4_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax5.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX5_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax6.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX6_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax7.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX7_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax8.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX8_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax9.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX9_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax10.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX10_Amt,0) else 0 end" + Environment.NewLine + _
    '") as DOC_Total_Amt,null as GL_Account_Code" + Environment.NewLine + _
    '"from TSPL_TRANSFER_ORDER_HEAD  " + Environment.NewLine + _
    '"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax1 on TabTax1.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX1" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax2 on TabTax2.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX2" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax3 on TabTax3.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX3" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax4 on TabTax4.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX4" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax5 on TabTax5.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX5" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax6 on TabTax6.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX6" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax7 on TabTax7.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX7" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax8 on TabTax8.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX8" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax9 on TabTax9.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX9" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax10 on TabTax10.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX10" + Environment.NewLine + _
    '"where Transfer_Type='I' and CAST(TSPL_TRANSFER_ORDER_HEAD.Document_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine
    '            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
    '            End If
    '            qry += "union all" + Environment.NewLine + _
    '"select 'Transfer RET' as  TransType,'Transfer Return' As TransDesc,TSPL_LOCATION_MASTER.Loc_Segment_Code as Loc_Code,TSPL_TRANSFER_RETURN.Document_No,TSPL_TRANSFER_RETURN.Document_Date,null as Vendor_Code,null as RefDocNo,'D' as Document_Type, (TSPL_TRANSFER_ORDER_HEAD.Discount_Base" + Environment.NewLine + _
    '"+ case when TabTax1.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX1_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax2.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX2_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax3.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX3_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax4.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX4_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax5.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX5_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax6.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX6_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax7.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX7_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax8.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX8_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax9.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX9_Amt,0) else 0 end" + Environment.NewLine + _
    '"+ case when TabTax10.Is_Mandi_Tax='Y' then isnull( TSPL_TRANSFER_ORDER_HEAD.TAX10_Amt,0) else 0 end" + Environment.NewLine + _
    '") as DOC_Total_Amt,null as GL_Account_Code" + Environment.NewLine + _
    '"from TSPL_TRANSFER_RETURN" + Environment.NewLine + _
    '"left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_RETURN.Transfer_No  " + Environment.NewLine + _
    '"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax1 on TabTax1.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX1" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax2 on TabTax2.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX2" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax3 on TabTax3.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX3" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax4 on TabTax4.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX4" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax5 on TabTax5.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX5" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax6 on TabTax6.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX6" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax7 on TabTax7.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX7" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax8 on TabTax8.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX8" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax9 on TabTax9.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX9" + Environment.NewLine + _
    '"left outer join TSPL_TAX_MASTER as TabTax10 on TabTax10.Tax_Code=TSPL_TRANSFER_ORDER_HEAD.TAX10" + Environment.NewLine + _
    '"where Transfer_Type='I' and TSPL_TRANSFER_RETURN.DOcument_Type='I' and CAST(TSPL_TRANSFER_ORDER_HEAD.Document_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'"
    '            If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
    '            End If
    '            qry += ")x Group by TransType,Loc_Code,Document_No,Vendor_Code,RefDocNo,Document_Type,GL_Account_Code" + Environment.NewLine + _
    '            ")xx" + Environment.NewLine + _
    '         "left outer join (" + Environment.NewLine + _
    '         "select xxx.Voucher_No,max(xxx.Voucher_Date) as Voucher_Date,xxx.Source_Doc_No,max(xxx.Segment_code) as Segment_code, xxx.Account_code,max(xxx.Account_Desc) as Account_Desc," + Environment.NewLine + _
    '         "sum(xxx.Amount * case when xxx.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine + _
    '         "sum(xxx.Amount * case when xxx.Amount<0 then -1 else 0 end  ) as CRAmount,max(xxx.Account_Seg_Code7) as Account_Seg_Code7 " + Environment.NewLine + _
    '         "from (" + Environment.NewLine + _
    '         "select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Voucher_Date,TSPL_JOURNAL_MASTER.Source_Doc_No,TSPL_JOURNAL_MASTER.Segment_code, TSPL_JOURNAL_DETAILS.Account_code ,TSPL_GL_ACCOUNTS.Description as Account_Desc,TSPL_JOURNAL_DETAILS.Amount ,isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'') as Reco_Control_Account,TSPL_GL_ACCOUNTS.Account_Seg_Code7" + Environment.NewLine + _
    '         "from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
    '         "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code" + Environment.NewLine + _
    '         "inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No  " + Environment.NewLine + _
    '         "where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='P' and " + Environment.NewLine + _
    '         "CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") & "' " + Environment.NewLine

    '            qry += ")xxx" + Environment.NewLine + _
    '            "group by xxx.Voucher_No,xxx.Source_Doc_No,xxx.Account_code having sum(xxx.Amount)<>0  " + Environment.NewLine + _
    '            ")GL on GL.Source_Doc_No= (case when len(isnull(RefDocNo,''))>0  then RefDocNo else xx.Document_No end) and 2=(case when xx.GL_Account_Code is null then 2 else (case when xx.GL_Account_Code=gl.Account_code then 2 else 3 end) end )  " + Environment.NewLine + _
    '            "left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= xx.Vendor_Code" + Environment.NewLine + _
    '            "left outer join  TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code= TSPL_VENDOR_MASTER.Vendor_Group_Code" + Environment.NewLine + _
    '            " Where 2=2 "
    '            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
    '                qry += " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Account,'') in (" & clsCommon.GetMulcallString(txtMultAccountNo.arrValueMember) & ") "
    '            End If
    '            If fndMultiVendorGroup.arrValueMember IsNot Nothing AndAlso fndMultiVendorGroup.arrValueMember.Count > 0 Then
    '                qry += " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Group_Code,'') in (" & clsCommon.GetMulcallString(fndMultiVendorGroup.arrValueMember) & ") "
    '            End If
    '            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '                qry += " and GL.Segment_code in  (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + "))"
    '            End If
    '            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
    '                qry += " AND (coalesce(GL.Account_code,'') in (" & clsCommon.GetMulcallString(txtMultAccountNo.arrValueMember) & ")) "
    '            End If

    '            If chkMismatchDoc.Checked Then
    '                qry += " and abs(isnull(((Amount * case when Document_Type in ('D') then -1  else 1 end)-(isnull( GL.DRAmount,0) -isnull(GL.CRAmount,0))),0))>0"
    '            End If



    '            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
    '                qry = "select xxxxx.*,isnull( JEOther.DRAmount,0)  as OtherDRAmount, isnull( JEOther.CRAmount,0)  as OtherCRAmount,(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)) as OtherNetAmount,(GLNetAmount+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0))) as TotalTrial  from (" + Environment.NewLine + _
    '                "select Account_code,max(Account_Desc) as Account_Desc,sum(DRAmount) as DRAmount,sum(CRAmount) as CRAmount,sum(NetAmount) as NetAmount,sum(GLDRAmount) as GLDRAmount,sum(GLCRAmount) as GLCRAmount,sum(GLNetAmount) as GLNetAmount,sum(DiffAmount) as DiffAmount " + Environment.NewLine + _
    '                "from (" + qry + ")Final group by Account_code" + Environment.NewLine + _
    '                 ")xxxxx  " + Environment.NewLine + _
    '                 "left outer join (select TSPL_JOURNAL_DETAILS.Account_code,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine + _
    '                "sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as CRAmount " + Environment.NewLine + _
    '                " from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
    '                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine + _
    '                " where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'P' "
    '                qry += ") and TSPL_JOURNAL_MASTER.Authorized='A' and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) BETWEEN '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + " ' and '" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' group by TSPL_JOURNAL_DETAILS.Account_code ) JEOther on  JEOther.Account_code=xxxxx.Account_code"
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal Then
    '                qry = "select  max(Vendor_Group_Code) as Vendor_Group_Code,max(Group_Desc) as Group_Desc,Vendor_Code,max(Vendor_Name) as Vendor_Name, Account_code,max(Account_Desc) as Account_Desc,sum(DRAmount) as DRAmount,sum(CRAmount) as CRAmount,sum(NetAmount) as NetAmount,sum(GLDRAmount) as GLDRAmount,sum(GLCRAmount) as GLCRAmount,sum(GLNetAmount) as GLNetAmount,sum(DiffAmount) as DiffAmount " + Environment.NewLine + _
    '                "from (" + qry + ")Final group by Vendor_Code,Account_code"
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
    '            Else
    '                Throw New Exception("Wrong Report type")
    '            End If
    '            dt = clsDBFuncationality.GetDataTable(qry)
    '            RadPageViewPage2.Text = cboType.Text
    '            Gv1.DataSource = Nothing
    '            Gv1.Columns.Clear()
    '            Gv1.Rows.Clear()
    '            Gv1.GroupDescriptors.Clear()
    '            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '            Gv1.EnableFiltering = True
    '            Gv1.Tag = cboType.SelectedValue
    '            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                Throw New Exception("No Data Found to Display")
    '            Else
    '                EnableDisableAllControl(False)
    '                Gv1.DataSource = dt
    '                SetGridFormationOFGV1()
    '            End If
    '            FindAndRestoreGridLayout(Me)
    '            Gv1.MasterTemplate.AllowAddNewRow = False
    '            RadPageView1.SelectedPage = RadPageViewPage2
    '        Catch ex As Exception
    '            clsCommon.ProgressBarHide()
    '            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '        Finally
    '            clsCommon.ProgressBarHide()
    '        End Try
    '    End Sub

    Sub EnableDisableAllControl(ByVal val As Boolean)
        txtTransaction.Enabled = val
        txtLocation.Enabled = val
        txtCustomer.Enabled = val
        txtMultAccountNo.Enabled = val
        fndMultiAccSet.Enabled = val
        fndMultiVendorGroup.Enabled = val
        cboType.Enabled = val
        fromDate.Enabled = val
        ToDate.Enabled = val
        chkMismatchDoc.Enabled = val
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            Gv1.Columns(ii).BestFit()
        Next
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("DRAmount").IsVisible = True
            Gv1.Columns("DRAmount").Width = 120
            Gv1.Columns("DRAmount").HeaderText = "Debit Amt"

            Gv1.Columns("CRAmount").IsVisible = True
            Gv1.Columns("CRAmount").Width = 120
            Gv1.Columns("CRAmount").HeaderText = "Credit Amt"

            Gv1.Columns("NetAmount").IsVisible = True
            Gv1.Columns("NetAmount").Width = 120
            Gv1.Columns("NetAmount").HeaderText = "Net Amt"

            Gv1.Columns("GLDRAmount").IsVisible = True
            Gv1.Columns("GLDRAmount").Width = 120
            Gv1.Columns("GLDRAmount").HeaderText = "Trial Debit Amt"

            Gv1.Columns("GLCRAmount").IsVisible = True
            Gv1.Columns("GLCRAmount").Width = 120
            Gv1.Columns("GLCRAmount").HeaderText = "Trial Credit Amt"

            Gv1.Columns("GLNetAmount").IsVisible = True
            Gv1.Columns("GLNetAmount").Width = 120
            Gv1.Columns("GLNetAmount").HeaderText = "Trial Net Amt"

            Gv1.Columns("OtherDRAmount").IsVisible = True
            Gv1.Columns("OtherDRAmount").Width = 120
            Gv1.Columns("OtherDRAmount").HeaderText = "Other Debit Amt"

            Gv1.Columns("OtherCRAmount").IsVisible = True
            Gv1.Columns("OtherCRAmount").Width = 120
            Gv1.Columns("OtherCRAmount").HeaderText = "Other Credit Amt"

            Gv1.Columns("OtherNetAmount").IsVisible = True
            Gv1.Columns("OtherNetAmount").Width = 120
            Gv1.Columns("OtherNetAmount").HeaderText = "Other Net Amt"

            Gv1.Columns("TotalTrial").IsVisible = True
            Gv1.Columns("TotalTrial").Width = 120
            Gv1.Columns("TotalTrial").HeaderText = "Total Trial"

            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("DRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("NetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLDRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherDRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherCRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("TotalTrial", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal Then
            Gv1.Columns("Vendor_Group_Code").IsVisible = True
            Gv1.Columns("Vendor_Group_Code").Width = 120
            Gv1.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 120
            Gv1.Columns("Group_Desc").HeaderText = "Vendor Group"



            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 120
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 120
            Gv1.Columns("Vendor_Name").HeaderText = "Vendor"

            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("DRAmount").IsVisible = True
            Gv1.Columns("DRAmount").Width = 120
            Gv1.Columns("DRAmount").HeaderText = "Debit Amt"

            Gv1.Columns("CRAmount").IsVisible = True
            Gv1.Columns("CRAmount").Width = 120
            Gv1.Columns("CRAmount").HeaderText = "Credit Amt"

            Gv1.Columns("NetAmount").IsVisible = True
            Gv1.Columns("NetAmount").Width = 120
            Gv1.Columns("NetAmount").HeaderText = "Net Amt"

            Gv1.Columns("GLDRAmount").IsVisible = True
            Gv1.Columns("GLDRAmount").Width = 120
            Gv1.Columns("GLDRAmount").HeaderText = "Trial Debit Amt"

            Gv1.Columns("GLCRAmount").IsVisible = True
            Gv1.Columns("GLCRAmount").Width = 120
            Gv1.Columns("GLCRAmount").HeaderText = "Trial Credit Amt"

            Gv1.Columns("GLNetAmount").IsVisible = True
            Gv1.Columns("GLNetAmount").Width = 120
            Gv1.Columns("GLNetAmount").HeaderText = "Trial Net Amt"

            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("DRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("NetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLDRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            Gv1.Columns("Vendor_Group_Code").IsVisible = True
            Gv1.Columns("Vendor_Group_Code").Width = 120
            Gv1.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 120
            Gv1.Columns("Group_Desc").HeaderText = "Vendor Group"

            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 120
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 120
            Gv1.Columns("Vendor_Name").HeaderText = "Vendor"

            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("DRAmount").IsVisible = True
            Gv1.Columns("DRAmount").Width = 120
            Gv1.Columns("DRAmount").HeaderText = "Debit Amt"

            Gv1.Columns("CRAmount").IsVisible = True
            Gv1.Columns("CRAmount").Width = 120
            Gv1.Columns("CRAmount").HeaderText = "Credit Amt"

            Gv1.Columns("NetAmount").IsVisible = True
            Gv1.Columns("NetAmount").Width = 120
            Gv1.Columns("NetAmount").HeaderText = "Net Amt"

            Gv1.Columns("GLDRAmount").IsVisible = True
            Gv1.Columns("GLDRAmount").Width = 120
            Gv1.Columns("GLDRAmount").HeaderText = "Trial Debit Amt"

            Gv1.Columns("GLCRAmount").IsVisible = True
            Gv1.Columns("GLCRAmount").Width = 120
            Gv1.Columns("GLCRAmount").HeaderText = "Trial Credit Amt"

            Gv1.Columns("GLNetAmount").IsVisible = True
            Gv1.Columns("GLNetAmount").Width = 120
            Gv1.Columns("GLNetAmount").HeaderText = "Trial Net Amt"

            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"

            Gv1.Columns("Document_No").IsVisible = True
            Gv1.Columns("Document_No").Width = 120
            Gv1.Columns("Document_No").HeaderText = "Document No"

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 120
            Gv1.Columns("Document_Date").HeaderText = "Document Date"

            Gv1.Columns("RefDocNo").IsVisible = True
            Gv1.Columns("RefDocNo").Width = 120
            Gv1.Columns("RefDocNo").HeaderText = "AP Invoice No"


            Gv1.Columns("TransType").IsVisible = True
            Gv1.Columns("TransType").Width = 120
            Gv1.Columns("TransType").HeaderText = "Trans Type"

            Gv1.Columns("Document_Type").IsVisible = True
            Gv1.Columns("Document_Type").Width = 120
            Gv1.Columns("Document_Type").HeaderText = "Document Type"

            Gv1.Columns("Voucher_No").IsVisible = True
            Gv1.Columns("Voucher_No").Width = 120
            Gv1.Columns("Voucher_No").HeaderText = "Voucher No"

            Gv1.Columns("Voucher_Date").IsVisible = True
            Gv1.Columns("Voucher_Date").Width = 120
            Gv1.Columns("Voucher_Date").HeaderText = "Voucher Date"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("DRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("NetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLDrAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCrAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False

        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        Try
            EnableDisableAllControl(True)
            RadPageView1.SelectedPage = RadPageViewPage1
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Purchase Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim qry As String
        Try
            qry = "CREATE NONCLUSTERED INDEX [TSPL_JOURNAL_DETAILS_Reco_Control_Account]" + _
            "ON [dbo].[TSPL_JOURNAL_DETAILS] ([Reco_Control_Account]) " + _
            "INCLUDE ([Voucher_No],[Account_code],[Amount])"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try

        Try
            qry = "CREATE NONCLUSTERED INDEX [TSPL_JOURNAL_MASTER_Authorized_Voucher_Date]" + _
            "ON [dbo].[TSPL_JOURNAL_MASTER] ([Authorized],[Voucher_Date])" + _
            "INCLUDE ([Voucher_No],[Source_Doc_No])"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try

        Try
            qry = "CREATE NONCLUSTERED INDEX [TSPL_VENDOR_INVOICE_HEAD_Posting_Date]" + _
            "ON [dbo].[TSPL_VENDOR_INVOICE_HEAD] ([Posting_Date])" + _
            "INCLUDE ([Vendor_Code],[Document_No],[Document_Type],[Document_Total],[Total_Tax],[TDS_Actual_Amount],[GSTRegistered],[RCM])"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try
        Me.Close()
    End Sub

    Private Sub rptVendReco_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtCustomer, "Vendor_name", "TSPL_VENDOR_master", "Vendor_Code")
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual')   "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " Select xxx.Code,  xxx.Name From (" & _
                                 " Select distinct 'PI' As Code,    'Purchase Invoice' As Name from TSPL_PI_HEAD " & _
                                 " Union  Select distinct 'MCC' As Code,    'Milk Receipt' As Name from TSPL_MILK_RECEIPT_HEAD " & _
                                 " Union  Select distinct 'Bulk' As Code,    'Bulk Purchase' As Name from tspl_Bulk_milk_purchase_Invoice_head " & _
                                  " Union  Select distinct 'Bulk Purchase Return' As Code,    'Bulk Purchase Return' As Name from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD " & _
                                 " Union  Select distinct 'MCC Transfer' As Code,    'MCC Transfer' As Name from TSPL_MILK_TRANSFER_IN " & _
                                 " Union  Select distinct 'Transfer' As Code,    'Transfer' As Name from TSPL_TRANSFER_ORDER_HEAD " & _
                                  " Union  Select distinct 'Transfer Return' As Code,    'Transfer Return' As Name from TSPL_TRANSFER_RETURN " & _
                                 " Union  Select distinct 'Return' As Code,    'Purchase Return' As Name from TSPL_PR_HEAD " & _
                                 " union Select distinct 'MT' As Code,    'Merchant Trade' As Name from TSPL_PI_HEAD " & _
                                 " ) xxx"
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulPur", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub


    Public Shared Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select TAX" & intloop & " from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select TAX" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeZeroQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        If clsCommon.myLen(qry) > 0 Then
            qry = "select * from( " & qry & ") as t1 where Add_Charge_Code1 not in ('AC_')"
        End If
        Return qry
    End Function

    Private Sub txtMultAccountNo__My_Click(sender As Object, e As EventArgs) Handles txtMultAccountNo._My_Click
        Dim qry As String = " select  Account_Code AS Code,Description as [Name] from TSPL_GL_ACCOUNTS "
        txtMultAccountNo.arrValueMember = clsCommon.ShowMultipleSelectForm("GLMulSel", qry, "Code", "Name", txtMultAccountNo.arrValueMember, txtMultAccountNo.arrDispalyMember)
    End Sub

    Private Sub fndMultiAccSet__My_Click(sender As Object, e As EventArgs) Handles fndMultiAccSet._My_Click
        Dim qry As String = " select Acct_Set_Code as [Code],Acct_Set_Desc as Name,Payable_Account as [Payable Account],Discount_Account as [Discount Account],Advance_Account as [Advance Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],CURRENCY_CODE as [Currency Code],EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account],EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] from TSPL_VENDOR_ACCOUNT_SET "
        fndMultiAccSet.arrValueMember = clsCommon.ShowMultipleSelectForm("VenAccMulSel", qry, "Code", "Name", fndMultiAccSet.arrValueMember, fndMultiAccSet.arrDispalyMember)
    End Sub

    Private Sub fndMultiVendorGroup__My_Click(sender As Object, e As EventArgs) Handles fndMultiVendorGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_Desc as Name from TSPL_VENDOR_GROUP  "
        fndMultiVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGrpMulSel", qry, "Code", "Name", fndMultiVendorGroup.arrValueMember, fndMultiVendorGroup.arrDispalyMember)
    End Sub

    Sub DrillDown()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Account Wise") Then
                    arrBack.Add("Account Wise")
                End If
                cboType.SelectedValue = "Vendor And Account Wise"
                arrGLAccount = New ArrayList()
                arrGLAccount = txtMultAccountNo.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Account_code").Value))
                txtMultAccountNo.arrValueMember = tmp
                If Gv1.CurrentColumn Is Gv1.Columns("DiffAmount") AndAlso clsCommon.myCdbl(Gv1.CurrentRow.Cells("DiffAmount").Value) <> 0 Then
                    boolChecked = chkMismatchDoc.Checked
                    chkMismatchDoc.Checked = True
                    chkMismatchDoc.Tag = "D"
                Else
                    chkMismatchDoc.Tag = Nothing
                End If

                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Vendor And Account Wise") Then
                    arrBack.Add("Vendor And Account Wise")
                End If
                cboType.SelectedValue = "Detail"
                arrCustomer = New ArrayList()
                arrCustomer = txtCustomer.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor_Code").Value))
                txtCustomer.arrValueMember = tmp
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
                ''Reached at last Node
                '' change for System memory
                'If Gv1.CurrentRow.Index >= 0 Then
                '    If Gv1.CurrentColumn Is Gv1.Columns("Voucher_No") AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells("Voucher_No").Value) > 0 Then
                '        MDI.ShowForm(clsUserMgtCode.journalEntry, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("Voucher_No").Value), True)
                '    Else
                '        If clsCommon.myLen(Gv1.CurrentRow.Cells("RefDocNo").Value) > 0 Then
                '            MDI.ShowForm(clsUserMgtCode.mbtnAPInvoiceEntry, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("RefDocNo").Value), True)
                '        ElseIf clsCommon.myLen(Gv1.CurrentRow.Cells("Document_No").Value) > 0 Then
                '            If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "MCC-TR") = CompairStringResult.Equal Then
                '                MDI.ShowForm(clsUserMgtCode.frmMilkTransferIn, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value), True)
                '            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "MCC-TR-RET") = CompairStringResult.Equal Then
                '                MDI.ShowForm(clsUserMgtCode.frmMilkTransferInReturn, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value), True)
                '            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "Transfer") = CompairStringResult.Equal Then
                '                MDI.ShowForm(clsUserMgtCode.Transfer, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value), True)
                '            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "Transfer RET") = CompairStringResult.Equal Then
                '                MDI.ShowForm(clsUserMgtCode.TransferReturn, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value), True)
                '            End If
                '        End If
                '    End If
                'End If
                If Gv1.CurrentRow.Index >= 0 Then
                    If Gv1.CurrentColumn Is Gv1.Columns("Voucher_No") AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells("Voucher_No").Value) > 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Voucher_No").Value))
                    Else
                        If clsCommon.myLen(Gv1.CurrentRow.Cells("RefDocNo").Value) > 0 Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, clsCommon.myCstr(Gv1.CurrentRow.Cells("RefDocNo").Value))
                        ElseIf clsCommon.myLen(Gv1.CurrentRow.Cells("Document_No").Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "MCC-TR") = CompairStringResult.Equal Then
                                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value))
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "MCC-TR-RET") = CompairStringResult.Equal Then
                                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value))

                            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "Transfer") = CompairStringResult.Equal Then
                                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value))
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "Transfer RET") = CompairStringResult.Equal Then
                                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value))
                            End If
                        End If
                    End If
                End If
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                ''Reached at First Node 
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Account Wise") Then
                arrBack.Remove("Account Wise")
                cboType.SelectedValue = "Account Wise"
                txtMultAccountNo.arrValueMember = arrGLAccount
                If clsCommon.CompairString(clsCommon.myCstr(chkMismatchDoc.Tag), "D") = CompairStringResult.Equal Then
                    chkMismatchDoc.Checked = boolChecked
                    chkMismatchDoc.Tag = Nothing
                End If
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Vendor And Account Wise") Then
                arrBack.Remove("Vendor And Account Wise")
                cboType.SelectedValue = "Vendor And Account Wise"
                txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Purchase Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Purchase Reco", Gv1, arrHeader, "Purchase Reco", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class


