Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptpayableReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region

    Private Sub frmAdvanceRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    'Ticket No-TEC/18/06/19-000544 ,Change Caption Refresh to Go
    Sub LoadData() ''TEC/18/01/19-000401 by balwinder on 26/Apr/2019
        Try
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                Throw New Exception("From date can not be greater then to Date")
            End If

            Dim qry As String = "select *,(PIAmount-SRNAmount+AdjAmount) as Diff from (" + Environment.NewLine +
"select TransType, max(case when RI=1 then SRNNo else null end) as SRNNo, max(case when RI=1 then Source_Doc_Date else null end) as SRNDate, max(case when RI=1 then Account_code else null end) as SRNAccount_code,sum(case when RI=1 then Amount else 0 end) as SRNAmount, PI_No, max(case when RI=2 then Source_Doc_Date else null end) as PIDate, max(case when RI=2 then Account_code else null end) as PIAccount_code,sum(case when RI=2 then Amount else 0 end) as PIAmount,sum(case when RI=3 then Amount else 0 end) as AdjAmount from (" + Environment.NewLine +
"-------General Purchase------" + Environment.NewLine +
"select 'SRN' as TransType, TAB_PI.PI_No, TSPL_JOURNAL_MASTER.Source_Doc_No as SRNNo,TSPL_JOURNAL_MASTER.Source_Doc_Date,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,1 as RI,1 as Chk from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
"inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine +
"left outer join (select x.PI_No,x.SRN_Id,tspl_srn_head.Bill_To_Location from( select PI_No,SRN_Id from TSPL_PI_DETAIL union all select Document_No as PI_No,SRN_No as SRN_Id from TSPL_SRN_RETURN  )x left outer join tspl_srn_head on tspl_srn_head.srn_no=x.SRN_Id left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=x.PI_No where tspl_pi_head.status=1 and tspl_srn_head.status=1 group by x.PI_No,x.SRN_Id,tspl_srn_head.Bill_To_Location) as TAB_PI on TAB_PI.SRN_Id=TSPL_JOURNAL_MASTER.Source_Doc_No and  TAB_PI.Bill_To_Location=TSPL_JOURNAL_DETAILS.Account_seg_code7 " + Environment.NewLine +
"where TSPL_JOURNAL_MASTER.Source_Code='PO-RC' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y' and TSPL_JOURNAL_MASTER.Source_Doc_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_JOURNAL_MASTER.Source_Doc_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' "
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and TSPL_JOURNAL_MASTER.Source_Type='V' and TSPL_JOURNAL_MASTER.CustVend_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")" + Environment.NewLine
            End If
            qry += " union all" + Environment.NewLine + _
            "--SRN Return" + Environment.NewLine + _
"select 'SRN' as TransType, TSPL_JOURNAL_MASTER.Source_Doc_No as PI_No,'' as SRNNo,TSPL_JOURNAL_MASTER.Source_Doc_Date ,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,2 as RI,0 as Chk from TSPL_JOURNAL_DETAILS" + Environment.NewLine + _
"Inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine + _
"where TSPL_JOURNAL_MASTER.Source_Code='SN-RT' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y'" + Environment.NewLine + _
"--End of SRN Return" + Environment.NewLine
            qry += "union all" + Environment.NewLine +
"select 'SRN' as TransType, TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No as PI_No,'' as SRNNo,TSPL_JOURNAL_MASTER.Source_Doc_Date ,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,2 as RI,0 as Chk from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
"Inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine +
"inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No" + Environment.NewLine +
"where TSPL_JOURNAL_MASTER.Source_Code='AP-IN' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' and len(isnull( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0 and  TSPL_VENDOR_INVOICE_HEAD.Loc_Code=TSPL_JOURNAL_DETAILS.Account_seg_code7  " + Environment.NewLine +
"union all" + Environment.NewLine +
"select 'SRN' as TransType,TSPL_JOURNAL_MASTER.Source_Doc_No as PI_No,null  as SRNNo,null as Source_Doc_Date,TSPL_JOURNAL_DETAILS.Account_code,(TSPL_JOURNAL_DETAILS.Amount) as Amount,3 as RI,0 as Chk from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
"inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine +
"where TSPL_JOURNAL_MASTER.Source_Code='PI-CM' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y'" + Environment.NewLine +
"-------End of General Purchase------" + Environment.NewLine +
"union all" + Environment.NewLine
            'BHA/30/04/19-000871 by balwinder on 02/May/2019
            Dim strMilkSRNBaseQry As String = "select 'Milk SRN' as TransType, TAB_PI.PI_No, TSPL_JOURNAL_MASTER.Source_Doc_No as SRNNo,TSPL_JOURNAL_MASTER.Source_Doc_Date,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,1 as RI,1 as Chk from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
"inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine +
"left outer join (select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE as PI_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE,TSPL_MILK_PURCHASE_INVOICE_head.DOC_DATE  from TSPL_MILK_PURCHASE_INVOICE_DETAIL inner join TSPL_MILK_PURCHASE_INVOICE_head on TSPL_MILK_PURCHASE_INVOICE_head.doc_code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.doc_code group by TSPL_MILK_PURCHASE_INVOICE_detail.DOC_CODE,TSPL_MILK_PURCHASE_INVOICE_detail.SRN_CODE,TSPL_MILK_PURCHASE_INVOICE_head.DOC_DATE) as TAB_PI on TAB_PI.SRN_CODE=TSPL_JOURNAL_MASTER.Source_Doc_No" + Environment.NewLine +
"where TSPL_JOURNAL_MASTER.Source_Code='MI-SR' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y'   "
            strMilkSRNBaseQry += " and TAB_PI.Doc_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TAB_PI.Doc_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strMilkSRNBaseQry += " and TSPL_JOURNAL_MASTER.Source_Type='V' and TSPL_JOURNAL_MASTER.CustVend_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")" + Environment.NewLine
                End If

            qry += "--------Milk Procurement-----------" + Environment.NewLine + _
                "select max(TransType) as TransType,PI_No,STUFF((select ',' + innPID.SRNNo from  (" + strMilkSRNBaseQry + "  and TAB_PI.PI_No=xx.PI_No )innPID  WHERE innPID.AMOUNT<>0 FOR XML PATH('')), 1, 1, '')  as SRNNo,max(Source_Doc_Date) as Source_Doc_Date,max(Account_code) as Account_code,sum(Amount) as Amount,1 as RI,1 as Chk from ( " + Environment.NewLine + _
strMilkSRNBaseQry + Environment.NewLine + _
")xx group by PI_No" + Environment.NewLine + _
" union all" + Environment.NewLine + _
"select 'Milk SRN' as TransType, TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No as PI_No,'' as SRNNo,TSPL_JOURNAL_MASTER.Source_Doc_Date ,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,2 as RI,0 as Chk " + Environment.NewLine + _
"from TSPL_JOURNAL_DETAILS" + Environment.NewLine + _
"Inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine + _
"inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No" + Environment.NewLine + _
"where TSPL_JOURNAL_MASTER.Source_Code='AP-IN' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' and len(isnull( TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,''))>0  " + Environment.NewLine + _
"--------End of Milk Procurement-----------" + Environment.NewLine + _
"union all" + Environment.NewLine + _
"---------Bulk Milk Purchase Invoice" + Environment.NewLine + _
" select max(TransType) as TransType,PI_No,STUFF((SELECT ',' + innPID.SRN_NO from (select SRN_NO FROM tspl_Bulk_milk_purchase_Invoice_Detail WHERE tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO =xx.PI_No union all select SRN_NO as SRN_Id from TSPL_Bulk_Milk_SRN_Return where SRN_Return_NO=xx.PI_No)as innPID  FOR XML PATH('')), 1, 1, '') as SRNNo,max(Source_Doc_Date) as Source_Doc_Date,max(Account_code) as Account_code,sum(Amount) as Amount,1 as RI,1 as Chk from ( " + Environment.NewLine + _
"select 'Bulk Milk SRN' as TransType, TAB_PI.PI_No, TSPL_JOURNAL_MASTER.Source_Doc_No as SRNNo,TSPL_JOURNAL_MASTER.Source_Doc_Date,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,1 as RI,1 as Chk from TSPL_JOURNAL_DETAILS" + Environment.NewLine + _
"inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine + _
"left outer join (select PI_No,SRN_Id from (select DOC_NO as PI_No,SRN_NO as SRN_Id from tspl_Bulk_milk_purchase_Invoice_Detail union all select SRN_Return_NO as PI_No,SRN_NO as SRN_Id from TSPL_Bulk_Milk_SRN_Return)x group by PI_No,SRN_Id) as TAB_PI on TAB_PI.SRN_Id=TSPL_JOURNAL_MASTER.Source_Doc_No" + Environment.NewLine + _
"where TSPL_JOURNAL_MASTER.Source_Code='BM-SR' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y' and TSPL_JOURNAL_MASTER.Source_Doc_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_JOURNAL_MASTER.Source_Doc_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and TSPL_JOURNAL_MASTER.Source_Type='V' and TSPL_JOURNAL_MASTER.CustVend_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")" + Environment.NewLine
                End If
            qry += ")xx group by PI_No" + Environment.NewLine +
                " union all" + Environment.NewLine +
"select 'Bulk Milk SRN' as TransType, TSPL_JOURNAL_MASTER.Source_Doc_No as PI_No,'' as SRNNo,TSPL_JOURNAL_MASTER.Source_Doc_Date ,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,2 as RI,0 as Chk " + Environment.NewLine +
"from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
"Inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine +
"where TSPL_JOURNAL_MASTER.Source_Code='SR-RT' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y' " + Environment.NewLine +
" union all" + Environment.NewLine +
" select 'Bulk Milk SRN' as TransType, TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No as PI_No,'' as SRNNo,TSPL_JOURNAL_MASTER.Source_Doc_Date ,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,2 as RI,0 as Chk " + Environment.NewLine +
"from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
"Inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine +
"inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No" + Environment.NewLine +
"where TSPL_JOURNAL_MASTER.Source_Code='AP-IN' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' and len(isnull( TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No,''))>0 " + Environment.NewLine +
"union all" + Environment.NewLine +
"select 'Bulk Milk SRN' as TransType,TSPL_JOURNAL_MASTER.Source_Doc_No as PI_No,null  as SRNNo,null as Source_Doc_Date,TSPL_JOURNAL_DETAILS.Account_code,(TSPL_JOURNAL_DETAILS.Amount) as Amount,3 as RI,0 as Chk from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
"inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine +
"where TSPL_JOURNAL_MASTER.Source_Code='BM-PI' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='Y'" + Environment.NewLine +
"---------End of Bulk Milk Purchase Invoice" + Environment.NewLine +
")xx group by TransType,PI_No having sum(Chk)>0 " + Environment.NewLine +
")xxx where 2=2  " & IIf(chkShowOnlyMismatcheddata.Checked = True, " and (PIAmount-SRNAmount+AdjAmount)<>0", "") & " " + Environment.NewLine +
"order by srnDate "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv3.DataSource = Nothing
            gv3.Rows.Clear()
            gv3.Columns.Clear()
            gv3.MasterTemplate.SummaryRowsBottom.Clear()
            gv3.DataSource = dt
            gv3.GroupDescriptors.Clear()
            gv3.MasterTemplate.BestFitColumns()

            gv3.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage2

            gv3.ReadOnly = True
            btnGenrate.Enabled = True
            SetGridLayout()
            EnableDisableControl(False)
            ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            btnGenrate.Enabled = True
        End Try
    End Sub
   
    Sub SetGridLayout()

        gv3.TableElement.TableHeaderHeight = 20
        gv3.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv3.Columns.Count - 1
            gv3.Columns(ii).ReadOnly = True
            gv3.Columns(ii).IsVisible = True
        Next

        gv3.Columns("TransType").IsVisible = True
        gv3.Columns("TransType").Width = 100
        gv3.Columns("TransType").HeaderText = "Transaction"

        gv3.Columns("SRNNo").IsVisible = True
        gv3.Columns("SRNNo").Width = 200
        gv3.Columns("SRNNo").HeaderText = "SRN No"

        gv3.Columns("SRNDate").IsVisible = False
        gv3.Columns("SRNDate").Width = 100
        gv3.Columns("SRNDate").HeaderText = "SRN Date"

        gv3.Columns("SRNAccount_code").IsVisible = True
        gv3.Columns("SRNAccount_code").Width = 100
        gv3.Columns("SRNAccount_code").HeaderText = "SRN Account No"

        gv3.Columns("SRNAmount").IsVisible = True
        gv3.Columns("SRNAmount").Width = 100
        gv3.Columns("SRNAmount").HeaderText = "SRN Amount"

        gv3.Columns("PI_No").IsVisible = True
        gv3.Columns("PI_No").Width = 100
        gv3.Columns("PI_No").HeaderText = "Pur. Invoice/SRN Return"

        gv3.Columns("PIDate").IsVisible = False
        gv3.Columns("PIDate").Width = 100
        gv3.Columns("PIDate").HeaderText = "Pur. Invoice/SRN Return Date"

        gv3.Columns("PIAccount_code").IsVisible = True
        gv3.Columns("PIAccount_code").Width = 100
        gv3.Columns("PIAccount_code").HeaderText = "Pur. Invoice/SRN Return Account"

        gv3.Columns("PIAmount").IsVisible = True
        gv3.Columns("PIAmount").Width = 100
        gv3.Columns("PIAmount").HeaderText = "Pur. Invoice/SRN Return Amount"

        gv3.Columns("AdjAmount").IsVisible = True
        gv3.Columns("AdjAmount").Width = 100
        gv3.Columns("AdjAmount").HeaderText = "Adjustment Amount"

        gv3.Columns("Diff").IsVisible = True
        gv3.Columns("Diff").Width = 100
        gv3.Columns("Diff").HeaderText = "Difference Amount"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item2 As New GridViewSummaryItem("SRNAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("PIAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("AdjAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Diff", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    
   

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
       
        btnGenrate.Enabled = True
        gv3.DataSource = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        chkShowOnlyMismatcheddata.Checked = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmAdvanceRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv3
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv3.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv3.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv3.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv3.Columns.Count - 1 Step ii + 1
                        gv3.Columns(ii).IsVisible = False
                        gv3.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv3.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Payable Clearing Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToExcelGrid("Payable Clearing Report", gv3, arr, "Payable Clearing Report", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Payable Clearing Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Payable Clearing Report", gv3, arr, "Payable Clearing Report", False)
    End Sub


    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)

        arr.Add("Payable Clearing Report")
        arr.Add("From Date : From " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Dim stCustomerName As String = ""
            For Each StrName As String In txtVendor.arrDispalyMember
                If clsCommon.myLen(stCustomerName) > 0 Then
                    stCustomerName += ", "
                End If
                stCustomerName += StrName
            Next
            arr.Add(("Vendor : " + stCustomerName + " "))
        End If

        If gv3.Rows.Count <= 0 Then
            gv3.Focus()
            clsCommon.MyMessageBoxShow("Data not found.")
        Else
            transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Payable Clearing Report", gv3, arr, "Payable Clearing Report", False)
        End If

    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where PROGRAM_CODE='" & clsUserMgtCode.rptCustomerAdvanceReg & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)


            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv3, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisableControl(True)
    End Sub

    Sub EnableDisableControl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        txtVendor.Enabled = val
    End Sub



    Private Sub btnPDF_Click_1(sender As Object, e As EventArgs) Handles btnPDF.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Payable Clearing Report")
        arr.Add("From Date : From " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            Dim stCustomerName As String = ""
            For Each StrName As String In txtVendor.arrDispalyMember
                If clsCommon.myLen(stCustomerName) > 0 Then
                    stCustomerName += ", "
                End If
                stCustomerName += StrName
            Next
            arr.Add(("Vendor : " + stCustomerName + " "))
        End If
        transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
        clsCommon.MyExportToPDF("Payable Clearing Report", gv3, arr, "Payable Clearing Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub
    ' Ticket No : BHA/21/01/19-000786 By Prabhakar 
    ''TEC/10/07/19-000938 by balwinder on 30/07/2019
    Private Sub gv3_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv3.CellDoubleClick
        Try
            If clsCommon.CompairString(clsCommon.myCstr(gv3.CurrentRow.Cells("TransType").Value), "SRN") = CompairStringResult.Equal Then
                If e.Column Is gv3.Columns("PI_No") Then
                    Dim qry As String = "select 1 from TSPL_SRN_RETURN where Document_No ='" + clsCommon.myCstr(gv3.CurrentRow.Cells("PI_No").Value) + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.SRNReturn, clsCommon.myCstr(gv3.CurrentRow.Cells("PI_No").Value))
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, clsCommon.myCstr(gv3.CurrentRow.Cells("PI_No").Value))
                    End If
                ElseIf e.Column Is gv3.Columns("SRNNo") Then
                    If Not clsCommon.myCstr(gv3.CurrentRow.Cells("SRNNo").Value).Contains(",") Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(gv3.CurrentRow.Cells("SRNNo").Value))
                    End If
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv3.CurrentRow.Cells("TransType").Value), "Milk SRN") = CompairStringResult.Equal Then
                If e.Column Is gv3.Columns("PI_No") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, clsCommon.myCstr(gv3.CurrentRow.Cells("PI_No").Value))
                ElseIf e.Column Is gv3.Columns("SRNNo") Then
                    If Not clsCommon.myCstr(gv3.CurrentRow.Cells("SRNNo").Value).Contains(",") Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, clsCommon.myCstr(gv3.CurrentRow.Cells("SRNNo").Value))
                    End If
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv3.CurrentRow.Cells("TransType").Value), "Bulk Milk SRN") = CompairStringResult.Equal Then
                If e.Column Is gv3.Columns("PI_No") Then
                    Dim qry As String = "select 1   from TSPL_Bulk_Milk_SRN_Return where SRN_Return_NO='" + clsCommon.myCstr(gv3.CurrentRow.Cells("PI_No").Value) + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRNReturn, clsCommon.myCstr(gv3.CurrentRow.Cells("PI_No").Value))
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkPurchaseInvoice, clsCommon.myCstr(gv3.CurrentRow.Cells("PI_No").Value))
                    End If
                ElseIf e.Column Is gv3.Columns("SRNNo") Then
                    If Not clsCommon.myCstr(gv3.CurrentRow.Cells("SRNNo").Value).Contains(",") Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(gv3.CurrentRow.Cells("SRNNo").Value))
                    End If
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as [code],Vendor_Name as [Name] from TSPL_Vendor_MASTER"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("Vendor@pare", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub
End Class
