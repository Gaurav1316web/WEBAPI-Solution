'' print work done on UDL against ticket no. UDL/03/05/18-000145,KDI/09/05/18-000306(do work on report),BHA/04/10/18-000592,BHA/04/10/18-000598
Imports System.IO
Imports common

Public Class GLTransReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Public strPrevFormACode As String = ""
    Public strPrevFormAName As String = ""
    Public strPreVendorCode As String = ""
    Public strPreCustomerCode As String = ""
    Public dTPrevFormFromDate As Date = Nothing
    Public dTPrevFormToDate As Date = Nothing
    Public boolPrevWithoutOpening As Boolean = False
    Public arrAcc As ArrayList
    Public arrvehicle As ArrayList
    Public arrDept As ArrayList
    Public arrEmp As ArrayList
    Public arrMachine As ArrayList
    Public arrVisi As ArrayList
    Public arrLocSeg As ArrayList
    Public arrvoucher As ArrayList
    Public arrSourceCode As ArrayList
    Public IsVendorCustomerWiseSummary As Boolean = False
    Public IsIncludeAdjustmentEntry As Boolean = False
    Public IsIncludeClosingEntry As Boolean = False
    Public IsIncludeYearEndEntry As Boolean = False
    Const colSegCode As String = "SEGCODE"
    Const colSegName As String = "SEGNAME"
    Const colFrom As String = "FROMFILTER"
    Const colFromName As String = "FROMFILTERNAME"
    Const colTo As String = "TOFILTER"
    Const colToName As String = "TOFILTERNAME"
    Const colIsForAC As String = "ISFORAC"
    Dim userCode, companyCode As String
    Dim strqry As String
    Dim dt As DataTable = Nothing
    Dim dtFinal As DataTable = Nothing
    Dim dtView As DataView = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim IsFormatedExport As Boolean = False
    Private contextMenu1 As RadContextMenu
    Dim menuItem1 As New RadMenuItem("Open")
    Dim pnlLocSeg As Panel
    Dim isTriggerOfGLEntryForWinTable As Boolean = False
    Dim ShowOptionforSelectingCapexForFA As Boolean = False
    Dim ShowOptionforSelectingCapexForPO As Boolean = False
    Dim qryForJV As String = ""
    Const colDate As String = "Date"
    Const colSourceNo As String = "SOURCENO"
    Const colVoucherno As String = "VOUCHERNO"
    Const colNarration As String = "NARRATION"
    Const colDesc As String = "DESCRIPTION"
    Const colDr As String = "DEBIT(Rs.)"
    Const colCr As String = "CREDIT(Rs.)"

    Dim strSourceTransaction As String = ""
    Dim strSourceDoc As String = ""
#End Region

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnBulkExp.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub GLTransReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isTriggerOfGLEntryForWinTable = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TriggerOfGLEntryForWinTable, clsFixedParameterCode.TriggerOfGLEntryForWinTable, Nothing)) > 0, True, False)
        ShowOptionforSelectingCapexForFA = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapexForFA, clsFixedParameterCode.ShowOptionforSelectingCapexForFA, Nothing)) > 0, True, False)
        ShowOptionforSelectingCapexForPO = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) > 0, True, False)
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        GroupingType()
        ddlGroupingType.SelectedIndex = 0
        btnPrint.Enabled = True
        ReportFilters()
        ''
        If clsCommon.myLen(strPrevFormACode) > 0 Then
            txtFromDate.Value = dTPrevFormFromDate
            txtToDate.Value = dTPrevFormToDate
            txtAccount.arrValueMember = arrAcc
            txtLocationSeg.arrValueMember = arrLocSeg
            txtVehicle.arrValueMember = arrvehicle
            txtDepartment.arrValueMember = arrDept
            txtEmployee.arrValueMember = arrEmp
            txtMachine.arrValueMember = arrMachine
            txtVisiPMX.arrValueMember = arrVisi
            chkCusVendWiseSummary.Checked = IsVendorCustomerWiseSummary
            chkIncludeingAdjustmentEntry.Checked = IsIncludeAdjustmentEntry
            chkIncludeingClosingEntry.Checked = IsIncludeClosingEntry
            chkIncludeYearEndEntry.Checked = IsIncludeYearEndEntry
            txtSourceCode.arrValueMember = arrSourceCode
            chkWithoutOpening.Checked = boolPrevWithoutOpening
            LoadDataNew(False)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            txtFromDate.MinDate = New Date(2001, 4, 1)
            txtFromDate.MaxDate = New Date(3000, 12, 1)
            txtToDate.MinDate = txtFromDate.MinDate
            txtToDate.MaxDate = txtFromDate.MaxDate

            strSourceDoc = clsCommon.myCstr(Me.Tag)
            Dim strBreak As String()
            If strSourceDoc.Contains("#$#") Then
                strBreak = clsCommon.myCstr(strSourceDoc).Split(New String() {"#$#"}, StringSplitOptions.None)
                strSourceTransaction = strBreak(0)
                strSourceDoc = strBreak(1)

                If clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.frmPaymentProcess) = CompairStringResult.Equal OrElse
                    clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.frmPaymentProcessFarmer) = CompairStringResult.Equal Then
                    txtFromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select From_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No='" + strSourceDoc + "'")).AddDays(-30)
                    txtToDate.Value = txtFromDate.Value.AddDays(60)
                Else
                    txtFromDate.Tag = strBreak(2)
                    txtFromDate.Value = clsCommon.myCDate(txtFromDate.Tag).AddDays(-30)
                    txtToDate.Value = txtFromDate.Value.AddDays(60)
                End If
                LoadDataNew(False)
                RadButton2.Visible = True
            End If
        End If


        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        AddHandler menuItem1.Click, AddressOf OpenfrmGLSummary
        GC.Collect()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If clsCommon.myLen(Me.Tag) > 0 Then
                'Application.OpenForms("MDI").Controls("__txtDocNo").Text = clsCommon.myCstr(Me.Tag)
                'Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.rptTrialBalance

                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.rptTrialBalance, clsCommon.myCstr(Me.Tag))

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub ReportFilters()
        Dim StrSetting As String = ""
        StrSetting = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =1"))
        If (clsCommon.CompairString(StrSetting, "0") = CompairStringResult.Equal) Then
            pnlAccount.Visible = False
        End If
        StrSetting = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =2"))
        If (clsCommon.CompairString(StrSetting, "0") = CompairStringResult.Equal) Then
            pnlVehicle.Visible = False
        End If
        StrSetting = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =3"))
        If (clsCommon.CompairString(StrSetting, "0") = CompairStringResult.Equal) Then
            pnlDepartment.Visible = False
        End If
        StrSetting = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =4"))
        If (clsCommon.CompairString(StrSetting, "0") = CompairStringResult.Equal) Then
            pnlEmployee.Visible = False
        End If
        StrSetting = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =5"))
        If (clsCommon.CompairString(StrSetting, "0") = CompairStringResult.Equal) Then
            pnlMachine.Visible = False
        End If
        StrSetting = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =6"))
        If (clsCommon.CompairString(StrSetting, "0") = CompairStringResult.Equal) Then
            pnlVisiPMX.Visible = False
        End If
        StrSetting = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =7"))
        If (clsCommon.CompairString(StrSetting, "0") = CompairStringResult.Equal) Then
            pnlLocSeg.Visible = False
        End If
    End Sub

    Sub GroupingType()
        ddlGroupingType.Items.Add("None")
        ddlGroupingType.Items.Add("Account No")
        ddlGroupingType.Items.Add("Source Document No")
    End Sub

    Public Sub SetDiplayMember(ByVal Fnd As common.UserControls.txtMultiSelectFinder, ByVal Col_Name As String, ByVal tb_name As String, ByVal val_col_Name As String)
        Try
            Dim sQuery As String = "select TSPL_GL_SEGMENT_CODE." & Col_Name & " as Name,xxx." & val_col_Name & " as Code from (select Loc_Segment_Code  from " & tb_name & " where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7' where Loc_Segment_Code in (" & clsCommon.GetMulcallString(Fnd.arrValueMember) & ") order by xxx.Loc_Segment_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            Dim arrList As New ArrayList
            For Each row As DataRow In dt.Rows
                arrList.Add(row(0))
            Next
            Fnd.arrDispalyMember = arrList
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadLocatinSegment()
        strqry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name from (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7' order by xxx.Loc_Segment_Code"
        txtLocationSeg.arrValueMember = clsCommon.ShowMultipleSelectForm("Location@GLTrans", strqry, "Code", "Name", txtLocationSeg.arrValueMember, txtLocationSeg.arrDispalyMember)
        SetDiplayMember(txtLocationSeg, "Description", "TSPL_LOCATION_MASTER", "Loc_Segment_Code")
    End Sub
    Sub LoadAccountGroup()
        strqry = "select Account_Group_Code as [Code], Account_Group_Desc as [Description] from TSPL_ACCOUNT_GROUPS"
        txtACGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ACGroup@GLTrans", strqry, "Code", "Description", txtACGroup.arrValueMember, txtACGroup.arrDispalyMember)
    End Sub

    Sub LoadAccounts()
        strqry = "select Account_Code as Code,[Description] from TSPL_GL_ACCOUNTS"
        txtAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("Accounts@GLTrans", strqry, "Code", "Description", txtAccount.arrValueMember, txtAccount.arrDispalyMember)
    End Sub

    Sub LoadVehicles()
        strqry = "Select Segment_code as Code , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=2"
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("Vehicle@GLTrans", strqry, "Code", "Description", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)
    End Sub

    Sub Loadmachines()
        strqry = "Select Segment_code as COde , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=5"
        txtMachine.arrValueMember = clsCommon.ShowMultipleSelectForm("Machine@GLTrans", strqry, "Code", "Description", txtMachine.arrValueMember, txtMachine.arrDispalyMember)
    End Sub

    Sub LoadDepartments()
        strqry = "Select Segment_code as Code , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=3"
        txtDepartment.arrValueMember = clsCommon.ShowMultipleSelectForm("Dept@GLTrans", strqry, "Code", "Description", txtDepartment.arrValueMember, txtDepartment.arrDispalyMember)
    End Sub

    Sub LoadEmployees()
        strqry = "Select Segment_code as Code , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=4"
        txtEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("Emp@GLTrans", strqry, "Code", "Description", txtEmployee.arrValueMember, txtEmployee.arrDispalyMember)
    End Sub

    Sub LoadVisi()
        strqry = "Select Segment_code as COde , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=6"
        txtVisiPMX.arrValueMember = clsCommon.ShowMultipleSelectForm("VisiPMX@GLTrans", strqry, "Code", "Description", txtVisiPMX.arrValueMember, txtVisiPMX.arrDispalyMember)
    End Sub

    ''richa 16 Apr,2019 SourceDescription column name change which is used as column refernce in ShowMultipleSelectForm BHA/16/04/19-000859
    Sub LoadSourceCode()
        strqry = "select SourceCode as Code,SourceDescription as Description from TSPL_GL_SOURCECODE "
        txtSourceCode.arrValueMember = clsCommon.ShowMultipleSelectForm("SourceCoce@GLTrans", strqry, "Code", "Description", txtSourceCode.arrValueMember, txtSourceCode.arrDispalyMember)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub

    Sub PrintData()
        LoadDataNew(True)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        Try
            strPreCustomerCode = ""
            strPreVendorCode = ""
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            dt = Nothing
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            EnableDisableControls(True)
            RadPageView1.SelectedPage = RadPageViewPage1

            Me.Tag = ""
            strSourceTransaction = ""
            strSourceDoc = ""
            RadButton2.Visible = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        '' Added By abhishek As on 2006/2012 for Month Wise Report
        ' Ticket No : ERO/23/05/19-000620 By Prabhakar   
        Try
            gv1.EnableFiltering = True
            PageSetupReport_ID = Getreportid()
            TemplateGridview = gv1
            LoadDataNew(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        RadPanel1.Enabled = Val
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        ddlGroupingType.Enabled = Val
        chkIndAS.Enabled = Val
    End Sub

    Private Sub LoadDataNew(ByVal IsPrint As Boolean, Optional ByVal BulkExport As Integer = 0)
        Dim dtStart As DateTime = DateTime.Now

        If isTriggerOfGLEntryForWinTable Then
            LoadData(IsPrint, BulkExport)
        Else
            LoadDataOld(IsPrint, BulkExport)
        End If
        Dim dtEnd As DateTime = DateTime.Now
        Dim timespan As TimeSpan = dtEnd.Subtract(dtStart)
        'clsCommon.MyMessageBoxShow("Minute:" + clsCommon.myCstr(timespan.Minutes) + "Seconds:" + clsCommon.myCstr(timespan.Seconds))
    End Sub

    Private Sub LoadData(ByVal IsPrint As Boolean, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim DtFrm As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim DtTo As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            strqry = " SELECT TSPL_JOURNAL_MASTER.Voucher_No,CONVERT(VARCHAR, TSPL_JOURNAL_MASTER.Voucher_Date,103) AS Voucher_Date," & _
            " Cast(CONVERT(Date,TSPL_JOURNAL_MASTER.Voucher_Date,103)as varchar(7))as Monthdate, TSPL_JOURNAL_MASTER.Voucher_Desc as Description, TSPL_JOURNAL_MASTER.Remarks, TSPL_JOURNAL_MASTER.Comments," & _
            " Case When TSPL_JOURNAL_MASTER.Source_Type ='V' Then TSPL_JOURNAL_MASTER.CustVend_Code  Else '' End as [Vendor Code],Case When TSPL_JOURNAL_MASTER.Source_Type ='V' Then TSPL_JOURNAL_MASTER.CustVend_name Else '' End as [Vendor],Case When TSPL_JOURNAL_MASTER.Source_Type ='C' Then TSPL_JOURNAL_MASTER.CustVend_Code  Else '' End as [Customer Code],Case When TSPL_JOURNAL_MASTER.Source_Type ='C' Then TSPL_JOURNAL_MASTER.CustVend_name Else '' End as [Customer],  " & _
            " TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Source_Desc, TSPL_JOURNAL_MASTER.Source_doc_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as Document_Date,TSPL_VENDOR_INVOICE_HEAD.Description as Descs, TSPL_JOURNAL_MASTER.Source_Narration, TSPL_JOURNAL_MASTER.Posting_Date, " & _
            " TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_DETAILS.Amount, case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt, case when TSPL_JOURNAL_DETAILS.Amount<0 then 0 else TSPL_JOURNAL_DETAILS.Amount end as DrAmt," & _
            " case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as TotalCrAmt, case when TSPL_JOURNAL_DETAILS.Amount<0 then 0 else TSPL_JOURNAL_DETAILS.Amount end as TotalDrAmt," & _
            " TSPL_JOURNAL_DETAILS.Description as Description1, TSPL_JOURNAL_DETAILS.Reference as Reference1, TSPL_JOURNAL_DETAILS.Detail_Line_No,TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code,TSPL_ACCOUNT_GROUPS.Account_Group_Desc AS Group_Desc, TSPL_GL_ACCOUNTS.Account_Group_Desc," & _
            " (case when TSPL_JOURNAL_MASTER.Source_Code='AP-IN' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No else '' end) as PINo , TSPL_PI_HEAD.Description [Pi Desc], TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date ,   TSPL_MILK_PURCHASE_INVOICE_HEAD.Description [MPI_DESC], TSPL_INVOICE_MASTER_BULKSAlE.Comments [MLK_BULK_SALE_DESC],TSPL_SD_SHIPMENT_HEAD.Description AS [MCC Desc] ,TSPL_Customer_Invoice_Head.Document_No as [Customer Invoice No]     " + Environment.NewLine & _
            " FROM TSPL_JOURNAL_DETAILS" + Environment.NewLine & _
            " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine & _
            " left outer join TSPL_GL_ACCOUNTS ON TSPL_JOURNAL_DETAILS.Account_code = TSPL_GL_ACCOUNTS.Account_Code" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ACCOUNT_MAIN_GL_ACCOUNT ON TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account  =TSPL_GL_ACCOUNTS.GL_Main_Code " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ACCOUNT_SUB_GROUPS ON TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code  = TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ACCOUNT_GROUPS ON TSPL_ACCOUNT_GROUPS.Account_Group_Code =TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No   LEFT OUTER JOIN tspl_pi_head on  TSPL_PI_HEAD.PI_No = tspl_vendor_invoice_head.Against_POInvoice_No LEFT JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_JOURNAL_MASTER.Source_Code =  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE LEFT JOIN TSPL_INVOICE_MASTER_BULKSAlE ON TSPL_INVOICE_MASTER_BULKSAlE.DOCUMENT_NO = tspl_vendor_invoice_head.Against_BulkMillkPurchaseInvoice_No " + Environment.NewLine & _
            " left outer join TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No left outer join TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No left outer join TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" & _
            " left outer join TSPL_COMPANY_MASTER on TSPL_JOURNAL_MASTER .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code" + Environment.NewLine & _
            " left outer join TSPL_FISCAL_YEAR_MASTER on convert(date, TSPL_FISCAL_YEAR_MASTER.Start_Date,106) <= '" + DtFrm + "'  and  TSPL_FISCAL_YEAR_MASTER.End_Date >=  '" + DtFrm + "'" & _
            " WHERE  TSPL_JOURNAL_MASTER.Authorized ='A'"
            If Not chkIndAS.Checked Then
                strqry += " and TSPL_JOURNAL_MASTER.ind_as=0"
            End If
            If txtAccountGroup.arrValueMember IsNot Nothing AndAlso txtAccountGroup.arrValueMember.Count > 0 Then
                strqry += " And TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtACGroup.arrValueMember) + ") "
            End If
            If txtACGroup.arrValueMember IsNot Nothing AndAlso txtACGroup.arrValueMember.Count > 0 Then
                strqry += " And TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtACGroup.arrValueMember) + ") "
            End If
            If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_code in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")"
            End If
            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code2 in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ")"
            End If
            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code3 in (" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ") "
            End If
            If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code4 in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ")"
            End If
            If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code5 in (" + clsCommon.GetMulcallString(txtMachine.arrValueMember) + ")"
            End If
            If txtVisiPMX.arrValueMember IsNot Nothing AndAlso txtVisiPMX.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code6 in (" + clsCommon.GetMulcallString(txtVisiPMX.arrValueMember) + ")"
            End If
            If txtLocationSeg.arrValueMember IsNot Nothing AndAlso txtLocationSeg.arrValueMember.Count > 0 Then
                strqry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(txtLocationSeg.arrValueMember) + ")"
            Else
                strqry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
            If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                strqry += "  and  TSPL_JOURNAL_MASTER.Source_Code in (" + clsCommon.GetMulcallString(txtSourceCode.arrValueMember) + ")"
            End If
            If clsCommon.myLen(strPreVendorCode) > 0 Then
                strqry += "  and  TSPL_JOURNAL_MASTER.Source_Type='V' and TSPL_JOURNAL_MASTER.CustVend_Code = '" + strPreVendorCode + "'"
            End If
            If clsCommon.myLen(strPreCustomerCode) > 0 Then
                strqry += "  and  TSPL_JOURNAL_MASTER.Source_Type='C' and TSPL_JOURNAL_MASTER.CustVend_Code = '" + strPreCustomerCode + "'"
            End If

            If Not chkIncludeingAdjustmentEntry.Checked Then
                strqry += " and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='A' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
            End If
            If Not chkIncludeingClosingEntry.Checked Then
                strqry += " and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_MASTER.Voucher_Desc not like 'Fiscal Year End%' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
            End If
            If Not chkIncludeYearEndEntry.Checked Then
                strqry += "and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_MASTER.Voucher_Desc like 'Fiscal Year End%' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
            End If


            Dim strMainQry As String = ""
            strMainQry = "select YYY.*,'" + DtFrm + "'as dtfrom,'" + DtTo + "'as dttodate, case when YYY.Source_Code IN ('AP-MI','AP-PI','AP-PY') then TSPL_PAYMENT_HEADER.Cheque_No else case when YYY.Source_Code IN ('AR-PY','AR-PI','AR-MI') then TSPL_RECEIPT_HEADER.Cheque_No else '' end end as Cheque_num, case when YYY.Source_Code IN ('AP-MI','AP-PI','AP-PY') then CONVERT(varchar, TSPL_PAYMENT_HEADER.Cheque_Date,103) else case when YYY.Source_Code IN ('AR-PY','AR-MI','AR-MI') then TSPL_RECEIPT_HEADER.Cheque_Date else '' end end as Cheque_date from (" + Environment.NewLine & _
                " select XXX.Account_code, XXX.Account_Desc, XXX.Account_Group_Code,XXX.Group_Desc , XXX.Account_Group_Desc, XXX.Voucher_No, XXX.Voucher_Date, XXX.Monthdate as Monthdate, XXX.Description, XXX.Remarks, XXX.Comments,XXX.[Vendor Code], XXX.[Vendor],xxx.[Customer Code], XXX.[Customer], XXX.Source_Narration as Narration, XXX.Source_Code, XXX.Source_Desc, XXX.Source_doc_No,xxx.Document_No as AP_Document_No,xxx.Document_Date as AP_Document_Date,xxx.descs, XXX.Posting_Date, XXX.Description1, XXX.Reference1, XXX.Detail_Line_No, XXX.PINo,  XXX.[Pi Desc] , XXX.Amount, XXX.DrAmt, XXX.CrAmt, XXX.TotalDrAmt, XXX.TotalCrAmt ,(XXX.TotalDrAmt + (-1 * XXX.TotalCrAmt)) as FinalAmount  , xxx.[Vendor_Invoice_No ], xxx.[Vendor_Invoice_Date ] ,   CASE  WHEN LEN(COALESCE(XXX.MLK_BULK_SALE_DESC, '')) > 0 THEN XXX.MLK_BULK_SALE_DESC   WHEN LEN(COALESCE(XXX.MPI_DESC,'')) > 0  THEN XXX.MPI_DESC WHEN LEN(COALESCE(XXX.[Pi Desc], '')) > 0 THEN [Pi Desc] ELSE '' END  AS [Invoices_Description],xxx.[Customer Invoice No],xxx.[MCC Desc] as [MCC Sale Desc]   from (" + Environment.NewLine
            If Not chkWithoutOpening.Checked Then
                'strMainQry += " Select 'Opening Balance---->' as Voucher_No, NULL as Voucher_Date, NULL as MonthDate, '' as [Description], '' as Remarks, '' as Comments,'' as [Vendor Code], '' as [Vendor],'' as [Customer Code], '' as [Customer], '' as Source_Code, '' as Source_Desc, '' as Source_Doc_No,'' as Document_No,Null as Document_Date,'' as Descs, '' as Source_Narration, NULL as Posting_Date" & IIf(clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal, ", '' as Account_code, '' as Account_Desc", ", Account_code, MAX(Account_Desc) as Account_Desc") & ", Sum(ISNULL(Amount,0)) as Amount, 0 as CrAmt, 0 as DrAmt,case when Sum(ISNULL(Amount,0))<0 then -1*Sum(ISNULL(Amount,0)) else 0 end as TotalCrAmt,case when Sum(ISNULL(Amount,0))>0 then Sum(ISNULL(Amount,0)) else 0 end as TotalDrAmt, '' as [Description1], '' as Reference1, 0 as Detail_Line_No, MAX(Account_Group_Code) as Account_Group_Code,MAX(Group_Desc) AS Group_Desc, MAX(Account_Group_Desc) as Account_Group_Desc, '' as PINo , MAX([Pi Desc]) [Pi Desc],  MAX(OPNING.Vendor_Invoice_No) [Vendor_Invoice_No ], MAX(OPNING.Vendor_Invoice_Date)[Vendor_Invoice_Date ] ,  MAX(OPNING.MLK_BULK_SALE_DESC) [MLK_BULK_SALE_DESC], MAX(OPNING.[MPI_DESC]) [MPI_DESC]   from  (" + Environment.NewLine & _
                '    " " + strqry + " and  TSPL_JOURNAL_MASTER.Voucher_Date<'" + DtFrm + "'" + Environment.NewLine & _
                ' " ) OPNING " & IIf(clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal, "", "Group By Account_Code") & "" + Environment.NewLine & _
                ' " UNION ALL" + Environment.NewLine

                Dim strOP As String = strqry
                strOP = strOP.Replace("TSPL_JOURNAL_DETAILS", "TSPL_JOURNAL_DETAILS_WIN")
                strOP = strOP.Replace("FROM TSPL_JOURNAL_DETAILS_WIN", "FROM (select CustVend_Code,CustVend_Name,PK_Id,Journal_No,Voucher_No,Voucher_Date,Detail_Line_No,Account_code,Account_Desc,Amount * case when OP_TYPE='D' then -1 else 1 end as Amount,Description,Reference,Posting_Date,Account_Type,Account_Group_Code,Account_Seg_Code1,Account_Seg_Desc1,Account_Seg_Code2,Account_Seg_Desc2,Account_Seg_Code3,Account_Seg_Desc3,Account_Seg_Code4,Account_Seg_Desc4,Account_Seg_Code5,Account_Seg_Desc5,Account_Seg_Code6,Account_Seg_Desc6,Account_Seg_Code7,Account_Seg_Desc7,Account_Seg_Code8,Account_Seg_Desc8,Account_Seg_Code9,Account_Seg_Desc9,Account_Seg_Code10,Account_Seg_Desc10,Hirerachy_Code,Cost_Centre_Code from TSPL_JOURNAL_DETAILS_WIN ) as TSPL_JOURNAL_DETAILS_WIN")

                If clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal Then
                    strMainQry += "Select 'Opening Balance---->' as Voucher_No, NULL as Voucher_Date, NULL as MonthDate, '' as [Description], '' as Remarks, '' as Comments,'' as [Vendor Code], '' as [Vendor],'' as [Customer Code], '' as [Customer], '' as Source_Code, '' as Source_Desc, '' as Source_Doc_No,'' as Document_No,Null as Document_Date,'' as Descs, '' as Source_Narration, NULL as Posting_Date, '' as Account_code, '' as Account_Desc, Sum(ISNULL(Amount,0)) as Amount, 0 as CrAmt, 0 as DrAmt,case when Sum(ISNULL(Amount,0))<0 then -1*Sum(ISNULL(Amount,0)) else 0 end as TotalCrAmt,case when Sum(ISNULL(Amount,0))>0 then Sum(ISNULL(Amount,0)) else 0 end as TotalDrAmt, '' as [Description1], '' as Reference1, 0 as Detail_Line_No, '' as Account_Group_Code,'' AS Group_Desc, '' as Account_Group_Desc, '' as PINo , '' as  [Pi Desc],  '' as  [Vendor_Invoice_No], null as Vendor_Invoice_Date,'' as [MLK_BULK_SALE_DESC], '' as [MPI_DESC]   from(" + Environment.NewLine
                Else
                    strMainQry += "Select 'Opening Balance---->' as Voucher_No, NULL as Voucher_Date, NULL as MonthDate, '' as [Description], '' as Remarks, '' as Comments,'' as [Vendor Code], '' as [Vendor],'' as [Customer Code], '' as [Customer], '' as Source_Code, '' as Source_Desc, '' as Source_Doc_No,'' as Document_No,Null as Document_Date,'' as Descs, '' as Source_Narration, NULL as Posting_Date, Account_code, MAX(Account_Desc) as Account_Desc, Sum(ISNULL(Amount,0)) as Amount, 0 as CrAmt, 0 as DrAmt,case when Sum(ISNULL(Amount,0))<0 then -1*Sum(ISNULL(Amount,0)) else 0 end as TotalCrAmt,case when Sum(ISNULL(Amount,0))>0 then Sum(ISNULL(Amount,0)) else 0 end as TotalDrAmt, '' as [Description1], '' as Reference1, 0 as Detail_Line_No, MAX(Account_Group_Code) as Account_Group_Code,MAX(Group_Desc) AS Group_Desc, MAX(Account_Group_Desc) as Account_Group_Desc, '' as PINo , '' as  [Pi Desc],  '' as  [Vendor_Invoice_No], null as Vendor_Invoice_Date,'' as [MLK_BULK_SALE_DESC], '' as [MPI_DESC]   from(" + Environment.NewLine
                End If
                strMainQry += "select xxx.Account_code, TSPL_GL_ACCOUNTS.Description  as Account_Desc,TSPL_GL_ACCOUNTS.Account_Group_Desc,TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code,TSPL_ACCOUNT_GROUPS.Account_Group_Desc AS Group_Desc ,Amount  as Amount from (" + Environment.NewLine + _
                "select TSPL_JOURNAL_DETAILS_DL_SC.Account_code,(TSPL_JOURNAL_DETAILS_DL_SC.CL_Amount -TSPL_JOURNAL_DETAILS_DL_SC.CL_IND "

                If Not chkIncludeingAdjustmentEntry.Checked Then
                    strMainQry += " -TSPL_JOURNAL_DETAILS_DL_SC.CL_Adjustment "
                End If
                If Not chkIncludeingClosingEntry.Checked Then
                    strMainQry += " -TSPL_JOURNAL_DETAILS_DL_SC.CL_Closing "
                End If
                strMainQry += ") as Amount from TSPL_JOURNAL_DETAILS_DL_SC" + Environment.NewLine + _
                "inner join (" + Environment.NewLine + _
                "select TSPL_JOURNAL_DETAILS_DL_SC.Account_code,Source_Code,MAX(B_Date) as B_Date " + Environment.NewLine + _
                "from TSPL_JOURNAL_DETAILS_DL_SC " + Environment.NewLine + _
                "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS_DL_SC.Account_code" + Environment.NewLine + _
                "LEFT OUTER JOIN TSPL_ACCOUNT_MAIN_GL_ACCOUNT ON TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account  =TSPL_GL_ACCOUNTS.GL_Main_Code " + Environment.NewLine + _
                "LEFT OUTER JOIN TSPL_ACCOUNT_SUB_GROUPS ON TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code  = TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code " + Environment.NewLine + _
                "where B_Date<'" + DtFrm + "'  " + Environment.NewLine
                If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                    strMainQry += "  and Source_Code in (" + clsCommon.GetMulcallString(txtSourceCode.arrValueMember) + ")" + Environment.NewLine
                End If
                If txtAccountGroup.arrValueMember IsNot Nothing AndAlso txtAccountGroup.arrValueMember.Count > 0 Then
                    strMainQry += " And TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtACGroup.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtACGroup.arrValueMember IsNot Nothing AndAlso txtACGroup.arrValueMember.Count > 0 Then
                    strMainQry += " And TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtACGroup.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                    strMainQry += " AND TSPL_GL_ACCOUNTS.Account_code in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")" + Environment.NewLine
                End If
                If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                    strMainQry += " AND TSPL_GL_ACCOUNTS.Account_Seg_Code2 in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ")" + Environment.NewLine
                End If
                If txtVehicle.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                    strMainQry += " AND TSPL_GL_ACCOUNTS.Account_Seg_Code3 in (" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                    strMainQry += " AND TSPL_GL_ACCOUNTS.Account_Seg_Code4 in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ")" + Environment.NewLine
                End If
                If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
                    strMainQry += " AND TSPL_GL_ACCOUNTS.Account_Seg_Code5 in (" + clsCommon.GetMulcallString(txtMachine.arrValueMember) + ")" + Environment.NewLine
                End If
                If txtVisiPMX.arrValueMember IsNot Nothing AndAlso txtVisiPMX.arrValueMember.Count > 0 Then
                    strMainQry += " AND TSPL_GL_ACCOUNTS.Account_Seg_Code6 in (" + clsCommon.GetMulcallString(txtVisiPMX.arrValueMember) + ")" + Environment.NewLine
                End If
                If txtLocationSeg.arrValueMember IsNot Nothing AndAlso txtLocationSeg.arrValueMember.Count > 0 Then
                    strMainQry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(txtLocationSeg.arrValueMember) + ")" + Environment.NewLine
                Else
                    strMainQry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (" + objCommonVar.strCurrUserLocationsSegment + ")" + Environment.NewLine
                End If
                strMainQry += " group by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,TSPL_JOURNAL_DETAILS_DL_SC.Source_Code)xx on xx.Account_code=TSPL_JOURNAL_DETAILS_DL_SC.Account_code and xx.Source_Code=TSPL_JOURNAL_DETAILS_DL_SC.Source_Code and xx.B_Date=TSPL_JOURNAL_DETAILS_DL_SC.B_Date " + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select Account_code,sum(DrAmt-CrAmt) as Amount from (" + Environment.NewLine + _
                " " + strOP + "" + Environment.NewLine + _
                "and TSPL_JOURNAL_MASTER.Voucher_Date<'" + DtFrm + "'" + Environment.NewLine + _
                ")xx group by Account_code" + Environment.NewLine + _
                ")xxx " + Environment.NewLine + _
                "left outer join TSPL_GL_ACCOUNTS ON xxx.Account_code = TSPL_GL_ACCOUNTS.Account_Code" + Environment.NewLine + _
                "LEFT OUTER JOIN TSPL_ACCOUNT_MAIN_GL_ACCOUNT ON TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account  =TSPL_GL_ACCOUNTS.GL_Main_Code " + Environment.NewLine + _
                "LEFT OUTER JOIN TSPL_ACCOUNT_SUB_GROUPS ON TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code  = TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  " + Environment.NewLine + _
                "LEFT OUTER JOIN TSPL_ACCOUNT_GROUPS ON TSPL_ACCOUNT_GROUPS.Account_Group_Code =TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code" + Environment.NewLine + _
                ") OPNING " + Environment.NewLine

                If clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal Then
                Else
                    strMainQry += "Group By Account_Code" + Environment.NewLine
                End If
                strMainQry += " UNION ALL" + Environment.NewLine
            End If
            strMainQry += " " + strqry + " "
            If Not clsCommon.CompairString(MyLabel2.Text, "As On date") = CompairStringResult.Equal Then
                strMainQry += " and  TSPL_JOURNAL_MASTER.Voucher_Date >= '" + DtFrm + "' "
            End If
            strMainQry += "  and TSPL_JOURNAL_MASTER.Voucher_Date <= '" + DtTo + "'  and TSPL_JOURNAL_MASTER.Transaction_Type<>'O' " + Environment.NewLine & _
            ") XXX " + Environment.NewLine & _
            ") YYY " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =YYY.Source_Doc_No" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_Payment_HEADER on TSPL_Payment_HEADER.Payment_No =YYY.Source_Doc_No"

            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            If chkCusVendWiseSummary.Checked Then
                strMainQry = "select Account_code,Account_Desc,Account_Group_Code,Group_Desc,Account_Group_Desc,[Vendor Code],[Vendor],[Customer Code],Customer, DrAmt,CrAmt  from (" + Environment.NewLine
                If chkWithoutOpening.Checked = False Then
                    strMainQry += " select 0 as TempSNO, Account_code,max(Account_Desc) as Account_Desc,max(Account_Group_Code) as Account_Group_Code,max(Group_Desc) as Group_Desc,max(Account_Group_Desc) as Account_Group_Desc,[Vendor Code],max(Vendor) as [Vendor],[Customer Code],max(Customer) as Customer,case when sum(xxxx.Amount)<0 then -1 * sum(xxxx.Amount) else 0 end as CrAmt, case when sum(xxxx.Amount)<0 then 0 else sum(xxxx.Amount) end as DrAmt from ( Select 'Opening Balance---->' as Voucher_No, NULL as Voucher_Date, NULL as MonthDate, '' as [Description], '' as Remarks, '' as Comments,'' as [Vendor Code], 'Opening Balance---->' as [Vendor],'' as [Customer Code], 'Opening Balance---->' as [Customer], '' as Source_Code, '' as Source_Desc, '' as Source_Doc_No,'' as Document_No,Null as Document_Date,'' as Descs, '' as Source_Narration, NULL as Posting_Date" & IIf(clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal, ", '' as Account_code, '' as Account_Desc", ", Account_code, MAX(Account_Desc) as Account_Desc") & ", Sum(ISNULL(Amount,0)) as Amount, 0 as CrAmt, 0 as DrAmt,case when Sum(ISNULL(Amount,0))<0 then -1*Sum(ISNULL(Amount,0)) else 0 end as TotalCrAmt,case when Sum(ISNULL(Amount,0))>0 then Sum(ISNULL(Amount,0)) else 0 end as TotalDrAmt, '' as [Description1], '' as Reference1, 0 as Detail_Line_No, MAX(Account_Group_Code) as Account_Group_Code,MAX(Group_Desc) AS Group_Desc, MAX(Account_Group_Desc) as Account_Group_Desc, '' as PINo , MAX(OPNING.Vendor_Invoice_No) [Vendor_Invoice_No ], MAX(OPNING.Vendor_Invoice_Date) [Vendor_Invoice_Date ]  from (" + Environment.NewLine & _
                    " " + strqry + " and  TSPL_JOURNAL_MASTER.Voucher_Date<'" + DtFrm + "'" + Environment.NewLine & _
                    " ) OPNING " & IIf(clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal, "", "Group By Account_Code") & "" + Environment.NewLine & _
                    "  )xxxx group by xxxx.Account_code,xxxx.[Vendor Code],xxxx.[Customer Code] " & _
                    " UNION ALL" + Environment.NewLine
                End If
                strMainQry += " select 1 as TempSNO,Account_code,max(Account_Desc) as Account_Desc,max(Account_Group_Code) as Account_Group_Code,max(Group_Desc) as Group_Desc,max(Account_Group_Desc) as Account_Group_Desc,[Vendor Code],max(Vendor) as [Vendor],[Customer Code],max(Customer) as Customer,case when sum(xxxx.Amount)<0 then -1 * sum(xxxx.Amount) else 0 end as CrAmt, case when sum(xxxx.Amount)<0 then 0 else sum(xxxx.Amount) end as DrAmt from ( " + strqry + "  "
                If Not clsCommon.CompairString(MyLabel2.Text, "As On date") = CompairStringResult.Equal Then
                    strMainQry += " and TSPL_JOURNAL_MASTER.Voucher_Date >= '" + DtFrm + "' "
                End If
                strMainQry += " and TSPL_JOURNAL_MASTER.Voucher_Date <= '" + DtTo + "' )xxxx group by xxxx.Account_code,xxxx.[Vendor Code],xxxx.[Customer Code] " + Environment.NewLine & _
                ") XXX   order by  Account_code,TempSNO"

                If BulkExport = 1 Then
                    transportSql.BulkExport("GLReportSummary", strMainQry, "order by  Account_code,TempSNO", "csv")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("GLReportSummary", strMainQry, "order by  Account_code,TempSNO", "xls")
                    Exit Sub
                End If
                dt = clsDBFuncationality.GetDataTable(strMainQry)
                gv1.DataSource = dt
                SetGridFormation()

            ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "None") = CompairStringResult.Equal Then
                strMainQry = "Select * from (" & strMainQry & ") ZZZ where 2=2 order by Account_code, convert(date,Voucher_Date ,103)"
                If BulkExport = 1 Then
                    transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQry, "order by Account_code, convert(date,Voucher_Date ,103)", "csv")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQry, "order by Account_code, convert(date,Voucher_Date ,103)", "xls")
                    Exit Sub
                End If
                dt = clsDBFuncationality.GetDataTable(strMainQry)
                gv1.DataSource = dt
                SetGridFormation()
            ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Account No") = CompairStringResult.Equal Then
                strMainQry = "Select Account_Code, MAX(Account_Desc) as Account_Desc, MAX(Account_Group_Code) as Account_Group_Code,MAX(Group_Desc) AS Group_Desc, MAX(Account_Group_Desc) as Account_Group_Desc, SUM(Amount) as Opening, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, " & IIf(chkWithoutOpening.Checked, 0, "SUM(Amount)") & "+SUM(DrAmt)-SUM(CrAmt) as Closing from (" & strMainQry & ") ZZZ where 2=2 Group By Account_Code order by Account_code"
                If BulkExport = 1 Then
                    transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQry, "order by Account_code", "csv")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQry, "order by Account_code", "xls")
                    Exit Sub
                End If
                dt = clsDBFuncationality.GetDataTable(strMainQry)
                gv1.DataSource = dt
                SetGridFormation()
            Else
                dt = Nothing
                If pnlAdminSetting.Visible And Not rbtnNone.IsChecked Then
                    If rbtnVendor.IsChecked Then
                        strqry = GetQueryForVendor()
                    ElseIf rbtnCustomer.IsChecked Then
                        strqry = GetQueryForCustomer()
                    ElseIf rbtnBank.IsChecked Then
                        strqry = GetQueryForBank()
                    ElseIf rbtnSaleRecoChart.IsChecked Then
                        strqry = GetQueryForSaleReco()
                    ElseIf rbtnPurchaseBook.IsChecked Then
                        strqry = GetQueryForPurchaseBook()
                    ElseIf rbtnPurchaseBook.IsChecked Then
                        strqry = GetQueryForPurchaseBook()
                    ElseIf rbtnSaleRegister.IsChecked Then
                        strqry = GetQueryForSaleRegiter()
                    End If
                    If BulkExport = 1 Then
                        transportSql.BulkExport("GLReportOthers", strMainQry, "", "csv")
                        Exit Sub
                    ElseIf BulkExport = 2 Then
                        transportSql.BulkExport("GLReportOthers", strMainQry, "", "xls")
                        Exit Sub
                    End If
                    Dim dtAS As DataTable = clsDBFuncationality.GetDataTable(strqry)
                    Dim arr As Dictionary(Of String, clsTempDrCrAmt) = Nothing
                    dt.Columns.Add("SubledgerDrAmt", GetType(Double))
                    dt.Columns.Add("SubledgerCrAmt", GetType(Double))
                    If dtAS IsNot Nothing AndAlso dtAS.Rows.Count > 0 Then
                        arr = New Dictionary(Of String, clsTempDrCrAmt)
                        For Each dr As DataRow In dtAS.Rows
                            Dim obj As clsTempDrCrAmt = New clsTempDrCrAmt()
                            obj.DrAmt = clsCommon.myCdbl(dr("SubledgerDrAmt"))
                            obj.CrAmt = clsCommon.myCdbl(dr("SubledgerCrAmt"))
                            arr.Add(clsCommon.myCstr(clsCommon.myCstr(dr("docNo")) + clsCommon.myCstr(dr("DocType"))).ToUpper(), obj)
                        Next
                        For ii As Integer = 0 To dt.Rows.Count - 1
                            Dim strSourceDocNo As String = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(ii)("Source_doc_No")) + clsCommon.myCstr(dt.Rows(ii)("Source_Code"))).ToUpper()
                            If arr.ContainsKey(strSourceDocNo) Then
                                dt.Rows(ii)("SubledgerDrAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).DrAmt)
                                dt.Rows(ii)("SubledgerCrAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).CrAmt)
                            End If
                        Next
                    End If
                Else
                    strMainQry = "select Source_Doc_No,AP_Document_No,AP_Document_Date,Descs, MAX(ZZZ.Posting_Date) as Posting_Date, max([Vendor Code]) as [Vendor Code],MAX(Vendor) as Vendor,max([Customer Code]) as [Customer Code], MAX(Customer) as Customer, MAX(Account_Code) as" _
                        & " Account_Code, MAX(Account_Desc) as Account_Desc, MAX(Account_Group_Desc) as [Account Group], MAX(Account_Group_Code) as Account_Group_Code," _
                        & " MAX(Group_Desc) AS Group_Desc, ZZZ.Voucher_No as Voucher_No, MAX(convert(varchar,ZZZ.Voucher_Date,103)) as Voucher_Date, MAX(ZZZ.Description)" _
                        & " as Description, MAX(ZZZ.Remarks) as Remarks, MAX(Comments) as Comments, (ZZZ.Source_Code) as Source_Code, MAX(ZZZ.Source_Desc) as Source_Desc, " _
                        & " SUM(ZZZ.TotalDrAmt) as TotalDrAmt, SUM(ZZZ.TotalCrAmt) as TotalCrAmt from (" + strMainQry + " ) as ZZZ group by ZZZ.Source_Doc_No, ZZZ.Voucher_No," _
                        & " ZZZ.Voucher_Date, ZZZ.Source_Code,zzz.AP_Document_No,zzz.AP_Document_Date,zzz.Descs  ORDER BY CONVERT(DATE,ZZZ.Voucher_Date,103)"
                    dt = clsDBFuncationality.GetDataTable(strMainQry)
                End If
                gv1.DataSource = dt
                SetGridFormationForGroupingType()
            End If
            If IsPrint Then
                Dim frmCRV As New frmCrystalReportViewer()
                If chkMonthWise.Checked = True Then
                    If clsCommon.myLen(strPrevFormACode) > 0 Then
                        txtFromDate.Value = dTPrevFormFromDate
                        txtToDate.Value = dTPrevFormToDate
                    End If
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "CrptGL_Trans_Month_Wise", "General Ledger Month Wise")
                Else
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "crptGLTrans", "General Ledger")
                End If
                frmCRV = Nothing
            End If
            dt = Nothing
            dtFinal = Nothing
            GC.Collect()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            IsFormatedExport = False
        End Try
        dt = Nothing
        dtView = Nothing

        dtFinal = Nothing


        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub LoadDataOld(ByVal IsPrint As Boolean, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim DtFrm As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim DtTo As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016 
            'KUNAL > TICKET : BM00000007570 > DATE : 27-OCT-2016 
            ''ERO/14/06/21-001410 By balwinder on 14/06/2021
            strqry = " SELECT TSPL_JOURNAL_MASTER.Voucher_No,CONVERT(VARCHAR, TSPL_JOURNAL_MASTER.Voucher_Date,103) AS Voucher_Date," &
            " Cast(CONVERT(Date,TSPL_JOURNAL_MASTER.Voucher_Date,103)as varchar(7))as Monthdate, TSPL_JOURNAL_MASTER.Voucher_Desc as Description, TSPL_JOURNAL_MASTER.Remarks, TSPL_JOURNAL_MASTER.Comments," &
            " Case When TSPL_JOURNAL_MASTER.Source_Type ='V' Then TSPL_JOURNAL_MASTER.CustVend_Code  Else '' End as [Vendor Code],Case When TSPL_JOURNAL_MASTER.Source_Type ='V' Then TSPL_Vendor_MASTER.Vendor_name Else '' End as [Vendor],Case When TSPL_JOURNAL_MASTER.Source_Type ='C' Then TSPL_JOURNAL_MASTER.CustVend_Code  Else '' End as [Customer Code],Case When TSPL_JOURNAL_MASTER.Source_Type ='C' Then TSPL_JOURNAL_MASTER.CustVend_name Else '' End as [Customer],  " &
            " TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Source_Desc, TSPL_JOURNAL_MASTER.Source_doc_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as Document_Date,TSPL_VENDOR_INVOICE_HEAD.Description as Descs, TSPL_JOURNAL_MASTER.Source_Narration, TSPL_JOURNAL_MASTER.Posting_Date, " &
            " TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_DETAILS.Amount, case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt, case when TSPL_JOURNAL_DETAILS.Amount<0 then 0 else TSPL_JOURNAL_DETAILS.Amount end as DrAmt," &
            " case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as TotalCrAmt, case when TSPL_JOURNAL_DETAILS.Amount<0 then 0 else TSPL_JOURNAL_DETAILS.Amount end as TotalDrAmt," &
            " TSPL_JOURNAL_DETAILS.Description as Description1, TSPL_JOURNAL_DETAILS.Reference as Reference1, TSPL_JOURNAL_DETAILS.Detail_Line_No,TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code,TSPL_ACCOUNT_GROUPS.Account_Group_Desc AS Group_Desc, TSPL_GL_ACCOUNTS.Account_Group_Desc," &
            " (case when TSPL_JOURNAL_MASTER.Source_Code='AP-IN' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No else '' end) as PINo , TSPL_PI_HEAD.Description [Pi Desc], TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date ,   TSPL_MILK_PURCHASE_INVOICE_HEAD.Description [MPI_DESC], TSPL_INVOICE_MASTER_BULKSAlE.Comments [MLK_BULK_SALE_DESC],TSPL_SD_SHIPMENT_HEAD.Description AS [MCC Desc] ,TSPL_Customer_Invoice_Head.Document_No as [Customer Invoice No] ,TSPL_JOURNAL_MASTER.VSP_CODE " + Environment.NewLine &
            ",(case when TSPL_JOURNAL_MASTER.Source_Code='PR-IS' then TSPL_PP_ISSUE_HEAD.Batch_Code else (case when TSPL_JOURNAL_MASTER.Source_Code='PS-FQ' then TSPL_PP_STD_FINALQC_HEAD.Main_Batch_Code else (case when TSPL_JOURNAL_MASTER.Source_Code='PR-ER' then TSPL_PP_PRODUCTION_ENTRY.Batch_Code else null end) end) end) as BatchOrderNo" + Environment.NewLine &
            " FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine &
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine &
                " left outer join TSPL_GL_ACCOUNTS ON TSPL_JOURNAL_DETAILS.Account_code = TSPL_GL_ACCOUNTS.Account_Code" + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_ACCOUNT_MAIN_GL_ACCOUNT ON TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account  =TSPL_GL_ACCOUNTS.GL_Main_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_ACCOUNT_SUB_GROUPS ON TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code  = TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_ACCOUNT_GROUPS ON TSPL_ACCOUNT_GROUPS.Account_Group_Code =TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No   LEFT OUTER JOIN tspl_pi_head on  TSPL_PI_HEAD.PI_No = tspl_vendor_invoice_head.Against_POInvoice_No LEFT JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_JOURNAL_MASTER.Source_Code =  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE LEFT JOIN TSPL_INVOICE_MASTER_BULKSAlE ON TSPL_INVOICE_MASTER_BULKSAlE.DOCUMENT_NO = tspl_vendor_invoice_head.Against_BulkMillkPurchaseInvoice_No " + Environment.NewLine &
                " left outer join TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No left outer join TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No left outer join TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" &
                " left outer join TSPL_COMPANY_MASTER on TSPL_JOURNAL_MASTER .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code " + Environment.NewLine &
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_JOURNAL_MASTER.CustVend_Code and TSPL_JOURNAL_MASTER.Source_Type ='V' " + Environment.NewLine &
                " left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_JOURNAL_MASTER.Source_Doc_No and TSPL_JOURNAL_MASTER.Source_Code='PR-IS'" + Environment.NewLine &
                " Left outer join TSPL_PP_STD_FINALQC_HEAD On TSPL_PP_STD_FINALQC_HEAD.QC_Code=TSPL_JOURNAL_MASTER.Source_Doc_No And TSPL_JOURNAL_MASTER.Source_Code='PS-FQ'" + Environment.NewLine &
                " Left outer join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_JOURNAL_MASTER.Source_Doc_No And TSPL_JOURNAL_MASTER.Source_Code='PR-ER'" + Environment.NewLine &
                " left outer join TSPL_FISCAL_YEAR_MASTER on convert(date, TSPL_FISCAL_YEAR_MASTER.Start_Date,106) <= '" + DtFrm + "'  and  TSPL_FISCAL_YEAR_MASTER.End_Date >=  '" + DtFrm + "'"
            If clsCommon.myLen(Me.Tag) > 0 Then
                strqry += "inner join ( "
                strqry += clsJournalEntryHeader.GetVoucherQuery(strSourceTransaction, strSourceDoc, txtFromDate.Tag)
                strqry += " )SelectedVoucher on SelectedVoucher.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No"
            End If
            strqry += " WHERE  TSPL_JOURNAL_MASTER.Authorized ='A'"
            If Not chkIndAS.Checked Then
                strqry += " and TSPL_JOURNAL_MASTER.ind_as=0"
            End If
            If txtAccountGroup.arrValueMember IsNot Nothing AndAlso txtAccountGroup.arrValueMember.Count > 0 Then
                strqry += " And TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtACGroup.arrValueMember) + ") "
            End If
            If txtACGroup.arrValueMember IsNot Nothing AndAlso txtACGroup.arrValueMember.Count > 0 Then
                strqry += " And TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtACGroup.arrValueMember) + ") "
            End If
            If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_code in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")"
            End If
            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code2 in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ")"
            End If
            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code3 in (" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ") "
            End If
            If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code4 in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ")"
            End If
            If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code5 in (" + clsCommon.GetMulcallString(txtMachine.arrValueMember) + ")"
            End If
            If txtVisiPMX.arrValueMember IsNot Nothing AndAlso txtVisiPMX.arrValueMember.Count > 0 Then
                strqry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code6 in (" + clsCommon.GetMulcallString(txtVisiPMX.arrValueMember) + ")"
            End If
            If txtLocationSeg.arrValueMember IsNot Nothing AndAlso txtLocationSeg.arrValueMember.Count > 0 Then
                strqry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(txtLocationSeg.arrValueMember) + ")"
            Else
                strqry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If

            If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                strqry += "  and  TSPL_JOURNAL_MASTER.Source_Code in (" + clsCommon.GetMulcallString(txtSourceCode.arrValueMember) + ")"
            End If
            If clsCommon.myLen(strPreVendorCode) > 0 Then
                strqry += "  and  TSPL_JOURNAL_MASTER.Source_Type='V' and TSPL_JOURNAL_MASTER.CustVend_Code = '" + strPreVendorCode + "'"
            End If
            If clsCommon.myLen(strPreCustomerCode) > 0 Then
                strqry += "  and  TSPL_JOURNAL_MASTER.Source_Type='C' and TSPL_JOURNAL_MASTER.CustVend_Code = '" + strPreCustomerCode + "'"
            End If

            If Not chkIncludeingAdjustmentEntry.Checked Then
                strqry += " and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='A' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
            End If
            If Not chkIncludeingClosingEntry.Checked Then
                strqry += " and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_MASTER.Voucher_Desc not like 'Fiscal Year End%' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
            End If
            If Not chkIncludeYearEndEntry.Checked Then
                strqry += "and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_MASTER.Voucher_Desc like 'Fiscal Year End%' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
            End If
            If chkExcludeTemplete.Checked Then
                strqry += " and not exists (select 1 from  TSPL_Trial_GLAccounts_Excluded where  TSPL_Trial_GLAccounts_Excluded.Account_Code= TSPL_GL_ACCOUNTS.Account_Seg_Code1 )"
            End If

            Dim strMainQry As String = ""
            strMainQry = "select YYY.*,'" + DtFrm + "'as dtfrom,'" + DtTo + "'as dttodate, case when YYY.Source_Code IN ('AP-MI','AP-PI','AP-PY') then TSPL_PAYMENT_HEADER.Cheque_No else case when YYY.Source_Code IN ('AR-PY','AR-PI','AR-MI') then TSPL_RECEIPT_HEADER.Cheque_No else '' end end as Cheque_num, case when YYY.Source_Code IN ('AP-MI','AP-PI','AP-PY') then CONVERT(varchar, TSPL_PAYMENT_HEADER.Cheque_Date,103) else case when YYY.Source_Code IN ('AR-PY','AR-MI','AR-MI') then TSPL_RECEIPT_HEADER.Cheque_Date else '' end end as Cheque_date," &
                " case when YYY.Source_Code='AR-IN' then " + Environment.NewLine +
                "case when   isnull(against_sale_no,'')='' then " + Environment.NewLine +
                "case when isnull(tspl_customer_invoice_head.against_vcgl,'')='' then " + Environment.NewLine +
                "case when  isnull(against_service_visit_code,'')='' then " + Environment.NewLine +
                "case when isnull(against_asset_disposal,'')=''  then " + Environment.NewLine +
                "case when isnull(against_subsidy_no,'')='' then " + Environment.NewLine +
                " case when isnull(against_security_receipt_no,'')='' then 'Direct' " + Environment.NewLine +
                "else isnull(against_security_receipt_no,'') end  " + Environment.NewLine +
                "else isnull(against_subsidy_no,'') end  " + Environment.NewLine +
                "else isnull(against_asset_disposal,'') end  " + Environment.NewLine +
                "else isnull(against_service_visit_code,'') end  " + Environment.NewLine +
                "else isnull(tspl_customer_invoice_head.against_vcgl,'') end  " + Environment.NewLine +
                "else against_sale_no end " + Environment.NewLine +
                "when YYY.Source_Code='AR-CR' then  " + Environment.NewLine +
                " case when   isnull(against_sale_no,'')='' then " + Environment.NewLine +
                "case when   isnull(against_sale_return_no,'')='' then  " + Environment.NewLine +
                "case when isnull(tspl_customer_invoice_head.against_mcc_material_sale_return,'')='' then " + Environment.NewLine +
                "case when  isnull(againstscrapreturn,'')='' then 'Direct'  " + Environment.NewLine +
                "else isnull(againstscrapreturn,'') end " + Environment.NewLine +
                "else isnull(tspl_customer_invoice_head.against_mcc_material_sale_return,'') end  " + Environment.NewLine +
                "else against_sale_return_no end " + Environment.NewLine +
                 "else against_sale_no end " + Environment.NewLine +
                "when YYY.Source_Code='AP-IN' then  " + Environment.NewLine +
                "case when   isnull(Against_POInvoice_No,'')='' then " + Environment.NewLine +
                "case when isnull(Against_Acquisition,'')='' then " + Environment.NewLine +
                "case when  isnull(Against_MillkPurchaseInvoice_No,'')='' then " + Environment.NewLine +
                "case when isnull(Against_BulkMillkPurchaseInvoice_No,'')=''  then " + Environment.NewLine +
                "case when isnull(Against_VSPItemIssue_No,'')='' then " + Environment.NewLine +
                "case when isnull(tspl_vendor_invoice_head.Against_VCGL,'')='' then " + Environment.NewLine +
                "case when isnull(Against_Asset_Work,'')='' then 'Direct' " + Environment.NewLine +
                "else isnull(Against_Asset_Work,'') end " + Environment.NewLine +
                "else isnull(tspl_vendor_invoice_head.Against_VCGL,'') end " + Environment.NewLine +
                "else isnull(Against_VSPItemIssue_No,'') end " + Environment.NewLine +
                "else isnull(Against_BulkMillkPurchaseInvoice_No,'') end " + Environment.NewLine +
                "else isnull(Against_MillkPurchaseInvoice_No,'') end " + Environment.NewLine +
                "else isnull(Against_Acquisition,'') end  " + Environment.NewLine +
                "else Against_POInvoice_No end " + Environment.NewLine +
                "when YYY.Source_Code='AP-DN' then " + Environment.NewLine +
                "case when   isnull(Against_POInvoice_No,'')='' then " + Environment.NewLine +
                "case when   isnull(Against_PurchaseReturn_No,'')='' then 'Direct' else Against_PurchaseReturn_No end " + Environment.NewLine +
                " else isnull(Against_POInvoice_No,'') end " + Environment.NewLine +
                "end as ReferenceNo " + Environment.NewLine +
                ", (sum(FinalAmount) over (partition by Account_code order by convert(date,Voucher_Date,103) ,RowNo)) as [Running Balance] " + Environment.NewLine +
                "from (" + Environment.NewLine &
                " select ROW_NUMBER() OVER (Partition By XXX.Account_code ORDER BY CONVERT(Date,XXX.Voucher_Date,103), XXX.Voucher_No) as RowNo,XXX.Account_code, XXX.Account_Desc, XXX.Account_Group_Code,XXX.Group_Desc , XXX.Account_Group_Desc, XXX.Voucher_No, XXX.Voucher_Date, XXX.Monthdate as Monthdate, XXX.Description, XXX.Remarks, XXX.Comments,XXX.[Vendor Code], XXX.[Vendor],xxx.[Customer Code], XXX.[Customer], XXX.Source_Narration as Narration, XXX.Source_Code, XXX.Source_Desc, XXX.Source_doc_No,xxx.Document_No as AP_Document_No,xxx.Document_Date as AP_Document_Date,xxx.descs, XXX.Posting_Date, XXX.Description1, XXX.Reference1, XXX.Detail_Line_No, XXX.PINo,  XXX.[Pi Desc] , XXX.Amount, XXX.DrAmt, XXX.CrAmt, XXX.TotalDrAmt, XXX.TotalCrAmt ,(XXX.TotalDrAmt + (-1 * XXX.TotalCrAmt)) as FinalAmount , xxx.[Vendor_Invoice_No ], xxx.[Vendor_Invoice_Date ] ,   CASE  WHEN LEN(COALESCE(XXX.MLK_BULK_SALE_DESC, '')) > 0 THEN XXX.MLK_BULK_SALE_DESC   WHEN LEN(COALESCE(XXX.MPI_DESC,'')) > 0  THEN XXX.MPI_DESC WHEN LEN(COALESCE(XXX.[Pi Desc], '')) > 0 THEN [Pi Desc] ELSE '' END  AS [Invoices_Description],xxx.[Customer Invoice No],xxx.[MCC Desc] as [MCC Sale Desc],xxx.VSP_CODE,xxx.BatchOrderNo   from (" + Environment.NewLine
            If chkWithoutOpening.Checked = False Then
                strMainQry += " Select 'Opening Balance---->' as Voucher_No, NULL as Voucher_Date, NULL as MonthDate, '' as [Description], '' as Remarks, '' as Comments,'' as [Vendor Code], '' as [Vendor],'' as [Customer Code], '' as [Customer], '' as Source_Code, '' as Source_Desc, '' as Source_Doc_No,'' as Document_No,Null as Document_Date,'' as Descs, '' as Source_Narration, NULL as Posting_Date" & IIf(clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal, ", '' as Account_code, '' as Account_Desc", ", Account_code, MAX(Account_Desc) as Account_Desc") & ", Sum(ISNULL(Amount,0)) as Amount, 0 as CrAmt, 0 as DrAmt,case when Sum(ISNULL(Amount,0))<0 then -1*Sum(ISNULL(Amount,0)) else 0 end as TotalCrAmt,case when Sum(ISNULL(Amount,0))>0 then Sum(ISNULL(Amount,0)) else 0 end as TotalDrAmt, '' as [Description1], '' as Reference1, 0 as Detail_Line_No, MAX(Account_Group_Code) as Account_Group_Code,MAX(Group_Desc) AS Group_Desc, MAX(Account_Group_Desc) as Account_Group_Desc, '' as PINo , MAX([Pi Desc]) [Pi Desc],  MAX(OPNING.Vendor_Invoice_No) [Vendor_Invoice_No ], MAX(OPNING.Vendor_Invoice_Date)[Vendor_Invoice_Date ] ,  MAX(OPNING.MLK_BULK_SALE_DESC) [MLK_BULK_SALE_DESC], MAX(OPNING.[MPI_DESC]) [MPI_DESC],MAX(OPNING.[MCC Desc]) [MCC Desc] ,MAX(OPNING.[Customer Invoice No]) [Customer Invoice No],max(OPNING.VSP_CODE) as VSP_CODE,null as BatchOrderNo   from  (" + Environment.NewLine &
                    " " + strqry + " and  TSPL_JOURNAL_MASTER.Voucher_Date<'" + DtFrm + "'" + Environment.NewLine &
                 " ) OPNING " & IIf(clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal, "", "Group By Account_Code") & "" + Environment.NewLine &
                 " UNION ALL" + Environment.NewLine
            End If
            '' changes done by richa agarwal ''' problem amount of amount column will not include into opening becuase amount column is used only for opening purpose not for from to to date documnets
            'strMainQry += " " + strqry + " "
            'If Not clsCommon.CompairString(MyLabel2.Text, "As On date") = CompairStringResult.Equal Then
            '    strMainQry += " and  TSPL_JOURNAL_MASTER.Voucher_Date >= '" + DtFrm + "' "
            'End If
            'strMainQry += "  and TSPL_JOURNAL_MASTER.Voucher_Date <= '" + DtTo + "'" + Environment.NewLine
            Dim strFromtoToDateQry As String = " " + strqry + " "
            If Not clsCommon.CompairString(MyLabel2.Text, "As On date") = CompairStringResult.Equal Then
                strFromtoToDateQry += " and  TSPL_JOURNAL_MASTER.Voucher_Date >= '" + DtFrm + "' "
            End If
            strFromtoToDateQry += "  and TSPL_JOURNAL_MASTER.Voucher_Date <= '" + DtTo + "'   and TSPL_JOURNAL_MASTER.Transaction_Type<>'O' " + Environment.NewLine
            strFromtoToDateQry = "Select FromtoToDateqry.Voucher_No ,FromtoToDateqry.Voucher_Date ,FromtoToDateqry.Monthdate ,FromtoToDateqry.Description ,FromtoToDateqry.Remarks ,FromtoToDateqry.Comments ,FromtoToDateqry.[Vendor Code] ,FromtoToDateqry.[Vendor],FromtoToDateqry.[Customer Code] ,FromtoToDateqry.Customer ,FromtoToDateqry.Source_Code ,FromtoToDateqry.Source_Desc ,FromtoToDateqry.Source_Doc_No ,FromtoToDateqry.Document_No ,FromtoToDateqry.Document_Date ,FromtoToDateqry.Descs ,FromtoToDateqry.Source_Narration ,FromtoToDateqry.Posting_Date ,FromtoToDateqry.Account_code ,FromtoToDateqry.Account_Desc ,0 as Amount,FromtoToDateqry.CrAmt ,FromtoToDateqry.DrAmt ,FromtoToDateqry.TotalCrAmt ,FromtoToDateqry.TotalDrAmt ,FromtoToDateqry.Description1 ,FromtoToDateqry.Reference1 ,FromtoToDateqry.Detail_Line_No ,FromtoToDateqry.Account_Group_Code ,FromtoToDateqry.Group_Desc ,FromtoToDateqry.Account_Group_Desc ,FromtoToDateqry.PINo ,FromtoToDateqry.[Pi Desc] ,FromtoToDateqry.Vendor_Invoice_No ,FromtoToDateqry.Vendor_Invoice_Date ,FromtoToDateqry.MPI_DESC ,FromtoToDateqry.MLK_BULK_SALE_DESC ,FromtoToDateqry.[MCC Desc] ,FromtoToDateqry.[Customer Invoice No],FromtoToDateqry.VSP_CODE as VSP_CODE,BatchOrderNo  from ( " + strFromtoToDateQry + " )   FromtoToDateqry "
            strMainQry += " " + strFromtoToDateQry + " "

            strMainQry += ") XXX " + Environment.NewLine & _
          ") YYY " + Environment.NewLine & _
          " LEFT OUTER JOIN TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =YYY.Source_Doc_No" + Environment.NewLine & _
          " LEFT OUTER JOIN TSPL_Payment_HEADER on TSPL_Payment_HEADER.Payment_No =YYY.Source_Doc_No " + Environment.NewLine & _
          " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =YYY.Source_Doc_No " + Environment.NewLine & _
          " LEFT OUTER JOIN TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No =YYY.Source_Doc_No "

            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            If chkCusVendWiseSummary.Checked Then
                strMainQry = "select Account_code,Account_Desc,Account_Group_Code,Group_Desc,Account_Group_Desc,[Vendor Code],[Vendor],[Customer Code],Customer, DrAmt,CrAmt  from (" + Environment.NewLine
                If chkWithoutOpening.Checked = False Then
                    strMainQry += " select 0 as TempSNO, Account_code,max(Account_Desc) as Account_Desc,max(Account_Group_Code) as Account_Group_Code,max(Group_Desc) as Group_Desc,max(Account_Group_Desc) as Account_Group_Desc,[Vendor Code],max(Vendor) as [Vendor],[Customer Code],max(Customer) as Customer,case when sum(xxxx.Amount)<0 then -1 * sum(xxxx.Amount) else 0 end as CrAmt, case when sum(xxxx.Amount)<0 then 0 else sum(xxxx.Amount) end as DrAmt from ( Select 'Opening Balance---->' as Voucher_No, NULL as Voucher_Date, NULL as MonthDate, '' as [Description], '' as Remarks, '' as Comments,'' as [Vendor Code], 'Opening Balance---->' as [Vendor],'' as [Customer Code], 'Opening Balance---->' as [Customer], '' as Source_Code, '' as Source_Desc, '' as Source_Doc_No,'' as Document_No,Null as Document_Date,'' as Descs, '' as Source_Narration, NULL as Posting_Date" & IIf(clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal, ", '' as Account_code, '' as Account_Desc", ", Account_code, MAX(Account_Desc) as Account_Desc") & ", Sum(ISNULL(Amount,0)) as Amount, 0 as CrAmt, 0 as DrAmt,case when Sum(ISNULL(Amount,0))<0 then -1*Sum(ISNULL(Amount,0)) else 0 end as TotalCrAmt,case when Sum(ISNULL(Amount,0))>0 then Sum(ISNULL(Amount,0)) else 0 end as TotalDrAmt, '' as [Description1], '' as Reference1, 0 as Detail_Line_No, MAX(Account_Group_Code) as Account_Group_Code,MAX(Group_Desc) AS Group_Desc, MAX(Account_Group_Desc) as Account_Group_Desc, '' as PINo , MAX(OPNING.Vendor_Invoice_No) [Vendor_Invoice_No ], MAX(OPNING.Vendor_Invoice_Date) [Vendor_Invoice_Date ]  from (" + Environment.NewLine & _
                     " " + strqry + " and  TSPL_JOURNAL_MASTER.Voucher_Date<'" + DtFrm + "'" + Environment.NewLine & _
                     " ) OPNING " & IIf(clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal, "", "Group By Account_Code") & "" + Environment.NewLine & _
                    "  )xxxx group by xxxx.Account_code,xxxx.[Vendor Code],xxxx.[Customer Code] " & _
                    " UNION ALL" + Environment.NewLine
                End If
                strMainQry += " select 1 as TempSNO,Account_code,max(Account_Desc) as Account_Desc,max(Account_Group_Code) as Account_Group_Code,max(Group_Desc) as Group_Desc,max(Account_Group_Desc) as Account_Group_Desc,[Vendor Code],max(Vendor) as [Vendor],[Customer Code],max(Customer) as Customer,case when sum(xxxx.Amount)<0 then -1 * sum(xxxx.Amount) else 0 end as CrAmt, case when sum(xxxx.Amount)<0 then 0 else sum(xxxx.Amount) end as DrAmt from ( " + strqry + "  "
                If Not clsCommon.CompairString(MyLabel2.Text, "As On date") = CompairStringResult.Equal Then
                    strMainQry += " and TSPL_JOURNAL_MASTER.Voucher_Date >= '" + DtFrm + "' "
                End If
                strMainQry += " and TSPL_JOURNAL_MASTER.Voucher_Date <= '" + DtTo + "' )xxxx group by xxxx.Account_code,xxxx.[Vendor Code],xxxx.[Customer Code] " + Environment.NewLine & _
                ") XXX   order by  Account_code,TempSNO"

                If BulkExport = 1 Then
                    transportSql.BulkExport("GLReportSummary", strMainQry, "order by  Account_code,TempSNO", "csv")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("GLReportSummary", strMainQry, "order by  Account_code,TempSNO", "xls")
                    Exit Sub
                End If
                dt = clsDBFuncationality.GetDataTable(strMainQry)
                gv1.DataSource = dt
                SetGridFormation()

            ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "None") = CompairStringResult.Equal Then
                strMainQry = "Select * from (" & strMainQry & ") ZZZ where 2=2 order by Account_code, convert(date,Voucher_Date ,103)"

                If BulkExport = 1 Then
                    transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQry, "order by Account_code, convert(date,Voucher_Date ,103)", "csv")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQry, "order by Account_code, convert(date,Voucher_Date ,103)", "xls")
                    Exit Sub
                End If

                dt = clsDBFuncationality.GetDataTable(strMainQry)
                gv1.DataSource = dt
                SetGridFormation()
                ' dt = Nothing
                ' GC.Collect()
            ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Account No") = CompairStringResult.Equal Then
                strMainQry = "Select Account_Code, MAX(Account_Desc) as Account_Desc, MAX(Account_Group_Code) as Account_Group_Code,MAX(Group_Desc) AS Group_Desc, MAX(Account_Group_Desc) as Account_Group_Desc, SUM(Amount) as Opening, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, " & IIf(chkWithoutOpening.Checked, 0, "SUM(Amount)") & "+SUM(DrAmt)-SUM(CrAmt) as Closing from (" & strMainQry & ") ZZZ where 2=2 Group By Account_Code order by Account_code"

                If BulkExport = 1 Then
                    transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQry, "order by Account_code", "csv")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQry, "order by Account_code", "xls")
                    Exit Sub
                End If

                dt = clsDBFuncationality.GetDataTable(strMainQry)
                gv1.DataSource = dt
                SetGridFormation()
                'dt = Nothing
                'GC.Collect()

            Else
                dt = Nothing
                If pnlAdminSetting.Visible And Not rbtnNone.IsChecked Then
                    If rbtnVendor.IsChecked Then
                        strqry = GetQueryForVendor()
                    ElseIf rbtnCustomer.IsChecked Then
                        strqry = GetQueryForCustomer()
                    ElseIf rbtnBank.IsChecked Then
                        strqry = GetQueryForBank()
                    ElseIf rbtnSaleRecoChart.IsChecked Then
                        strqry = GetQueryForSaleReco()
                    ElseIf rbtnPurchaseBook.IsChecked Then
                        strqry = GetQueryForPurchaseBook()
                    ElseIf rbtnPurchaseBook.IsChecked Then
                        strqry = GetQueryForPurchaseBook()
                    ElseIf rbtnSaleRegister.IsChecked Then
                        strqry = GetQueryForSaleRegiter()
                    End If
                    If BulkExport = 1 Then
                        transportSql.BulkExport("GLReportOthers", strMainQry, "", "csv")
                        Exit Sub
                    ElseIf BulkExport = 2 Then
                        transportSql.BulkExport("GLReportOthers", strMainQry, "", "xls")
                        Exit Sub
                    End If
                    Dim dtAS As DataTable = clsDBFuncationality.GetDataTable(strqry)
                    Dim arr As Dictionary(Of String, clsTempDrCrAmt) = Nothing
                    dt.Columns.Add("SubledgerDrAmt", GetType(Double))
                    dt.Columns.Add("SubledgerCrAmt", GetType(Double))
                    If dtAS IsNot Nothing AndAlso dtAS.Rows.Count > 0 Then
                        arr = New Dictionary(Of String, clsTempDrCrAmt)
                        For Each dr As DataRow In dtAS.Rows
                            Dim obj As clsTempDrCrAmt = New clsTempDrCrAmt()
                            obj.DrAmt = clsCommon.myCdbl(dr("SubledgerDrAmt"))
                            obj.CrAmt = clsCommon.myCdbl(dr("SubledgerCrAmt"))
                            arr.Add(clsCommon.myCstr(clsCommon.myCstr(dr("docNo")) + clsCommon.myCstr(dr("DocType"))).ToUpper(), obj)
                        Next
                        For ii As Integer = 0 To dt.Rows.Count - 1
                            Dim strSourceDocNo As String = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(ii)("Source_doc_No")) + clsCommon.myCstr(dt.Rows(ii)("Source_Code"))).ToUpper()
                            If arr.ContainsKey(strSourceDocNo) Then
                                dt.Rows(ii)("SubledgerDrAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).DrAmt)
                                dt.Rows(ii)("SubledgerCrAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).CrAmt)
                            End If
                        Next
                    End If
                Else
                    '' BM00000007292
                    Dim strMainQuery = "select Source_Doc_No,AP_Document_No,AP_Document_Date,Descs, MAX(ZZZ.Posting_Date) as Posting_Date, max([Vendor Code]) as [Vendor Code],MAX(Vendor) as Vendor,max([Customer Code]) as [Customer Code], MAX(Customer) as Customer, MAX(Account_Code) as" _
                        & " Account_Code, MAX(Account_Desc) as Account_Desc, MAX(Account_Group_Desc) as [Account Group], MAX(Account_Group_Code) as Account_Group_Code," _
                        & " MAX(Group_Desc) AS Group_Desc, ZZZ.Voucher_No as Voucher_No, MAX(convert(varchar,ZZZ.Voucher_Date,103)) as Voucher_Date, MAX(ZZZ.Description)" _
                        & " as Description, MAX(ZZZ.Remarks) as Remarks, MAX(Comments) as Comments, (ZZZ.Source_Code) as Source_Code, MAX(ZZZ.Source_Desc) as Source_Desc, " _
                        & " SUM(ZZZ.TotalDrAmt) as TotalDrAmt, SUM(ZZZ.TotalCrAmt) as TotalCrAmt "
                    ''richa UDL/23/04/19-000291 on 30 Apr,2019 SHOW CAPEX AND SUB CAPEX CODE WHEN BOTH SETTINGS ARE ON
                    If ShowOptionforSelectingCapexForFA = True AndAlso ShowOptionforSelectingCapexForPO = True Then
                        strMainQuery += " ,max(final.Capex_Code) as Capex_Code ,max(TSPL_CAPEX_MASTER .DESCRIPTION) as [Capex Desc] ,max(Final.Capex_SubCode) as Capex_SubCode,max(TSPL_CAPEX_BUDGET_MASTER .DESCRIPTION) as [Sub Capex Desc] "
                    End If

                    strMainQuery += " from (" + strMainQry + " ) as ZZZ  "
                    ''richa UDL/23/04/19-000291 on 30 Apr,2019 SHOW CAPEX AND SUB CAPEX CODE WHEN BOTH SETTINGS ARE ON
                    If ShowOptionforSelectingCapexForFA = True AndAlso ShowOptionforSelectingCapexForPO = True Then
                        strMainQuery += " left outer join (select distinct Capex_Code,Capex_SubCode ,Acquisition_Code  from TSPL_ACQUISITION_DETAIL where isnull(Capex_Code,'')<>'' " & Environment.NewLine & _
                        " union all  " & Environment.NewLine & _
                        " select distinct Capex_Code,Capex_SubCode ,Doc_No from TSPL_IssueItemToAssembledAsset_Detail  where isnull(Capex_Code,'')<>'' " & Environment.NewLine & _
                        " union all " & Environment.NewLine & _
                        " select distinct Capex_Code,Capex_SubCode ,SRN_No from TSPL_SRN_DETAIL  where isnull(Capex_Code,'')<>'' " & Environment.NewLine & _
                        " ) Final on final.Acquisition_Code =ZZZ.Source_Doc_No  " & Environment.NewLine & _
                        " left outer join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER .CODE =Final.Capex_SubCode  " & Environment.NewLine & _
                        " left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER .code =final.Capex_Code "
                    End If

                    strMainQuery += " group by ZZZ.Source_Doc_No, ZZZ.Voucher_No," _
                        & " ZZZ.Voucher_Date, ZZZ.Source_Code,zzz.AP_Document_No,zzz.AP_Document_Date,zzz.Descs  ORDER BY CONVERT(DATE,ZZZ.Voucher_Date,103)"

                    dt = clsDBFuncationality.GetDataTable(strMainQuery)

                    ''richa TEC/05/06/19-000520 18 June,2019
                    If BulkExport = 1 Then
                        transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQuery, "", "csv")
                        Exit Sub
                    ElseIf BulkExport = 2 Then
                        transportSql.BulkExport("GLReport" & ddlGroupingType.SelectedItem.Text, strMainQuery, "", "xls")
                        Exit Sub
                    End If
                End If
                gv1.DataSource = dt
                SetGridFormationForGroupingType()
            End If
            If IsPrint Then
                Dim frmCRV As New frmCrystalReportViewer()
                If chkMonthWise.Checked = True Then
                    If clsCommon.myLen(strPrevFormACode) > 0 Then
                        txtFromDate.Value = dTPrevFormFromDate
                        txtToDate.Value = dTPrevFormToDate
                    End If
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "CrptGL_Trans_Month_Wise", "General Ledger Month Wise")
                Else
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "crptGLTrans", "General Ledger")
                End If
                frmCRV = Nothing
            End If
            dt = Nothing
            dtFinal = Nothing
            GC.Collect()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            IsFormatedExport = False
        End Try
        dt = Nothing
        dtView = Nothing
        dtFinal = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub ExportToExcelNew(ByVal dt As DataTable)
        Dim AccountCode As String = ""
        Dim Amount As Double = 0
        LoadBlankGrid()
        Dim GroupCounter As Integer = 0
        Dim RecCount As Integer = dt.Rows.Count
        Dim count As Integer = 0
        Try
            For Each dr As DataRow In dt.Rows
                Dim drAmt As Double
                Dim crAmt As Double
                If Not clsCommon.CompairString(clsCommon.myCstr(dr("Account_Code")), AccountCode) = CompairStringResult.Equal Then
                    AccountCode = clsCommon.myCstr(dr("Account_Code"))
                    If GroupCounter > 0 Then
                        gv1.Rows.AddNew()
                        gv1.CurrentRow.Cells(colDesc).Value = "CLOSING BALANCE"
                        If drAmt > crAmt Then
                            gv1.CurrentRow.Cells(colDr).Value = drAmt - crAmt
                        Else
                            gv1.CurrentRow.Cells(colCr).Value = crAmt - drAmt
                        End If
                        drAmt = 0
                        crAmt = 0
                    End If
                    If (clsCommon.myCdbl(dr("DrAmt")) + clsCommon.myCdbl(dr("CrAmt"))) <> 0 And clsCommon.myCdbl(dr("opening_balance")) = 0 Then
                        GroupCounter += 1
                        gv1.Rows.AddNew()
                        gv1.CurrentRow.Cells(colDate).Value = "Account :  " + clsCommon.myCstr(dr("Account_Code")) + ""
                        gv1.Rows.AddNew()
                        gv1.CurrentRow.Cells(colDate).Value = "Description :  " + clsCommon.myCstr(dr("Account_Desc")) + ""
                        gv1.CurrentRow.Cells(colDesc).Value = "OPENING BALANCE----->"

                        gv1.Rows.AddNew()
                        gv1.CurrentRow.Cells(colDate).Value = clsCommon.myCstr(dr("Voucher_Date"))
                        gv1.CurrentRow.Cells(colFrom).Value = clsCommon.myCstr(dr("Source_Desc"))
                        gv1.CurrentRow.Cells(colSourceNo).Value = clsCommon.myCstr(dr("Source_doc_no"))
                        gv1.CurrentRow.Cells(colVoucherno).Value = clsCommon.myCstr(dr("Voucher_No"))
                        gv1.CurrentRow.Cells(colNarration).Value = clsCommon.myCstr(dr("Narration"))
                        gv1.CurrentRow.Cells(colDesc).Value = clsCommon.myCstr(dr("Description1"))
                        gv1.CurrentRow.Cells(colDr).Value = clsCommon.myCdbl(dr("DrAmt"))
                        drAmt += clsCommon.myCdbl(dr("DrAmt"))
                        gv1.CurrentRow.Cells(colCr).Value = clsCommon.myCdbl(dr("CrAmt"))
                        crAmt += clsCommon.myCdbl(dr("CrAmt"))
                    Else
                        GroupCounter += 1
                        gv1.Rows.AddNew()
                        gv1.CurrentRow.Cells(colDate).Value = "Account :  " + clsCommon.myCstr(dr("Account_Code")) + ""
                        gv1.Rows.AddNew()
                        gv1.CurrentRow.Cells(colDate).Value = "Description :  " + clsCommon.myCstr(dr("Account_Desc")) + ""
                        gv1.CurrentRow.Cells(colDesc).Value = "OPENING BALANCE----->"
                        If clsCommon.myCdbl(dr("opening_balance")) < 0 Then
                            crAmt += clsCommon.myCdbl(dr("opening_balance")) * -1
                            gv1.CurrentRow.Cells(colCr).Value = clsCommon.myCdbl(dr("opening_balance")) * -1
                        Else
                            drAmt += clsCommon.myCdbl(dr("opening_balance"))
                            gv1.CurrentRow.Cells(colDr).Value = clsCommon.myCdbl(dr("opening_balance"))
                        End If
                    End If
                Else
                    gv1.Rows.AddNew()
                    gv1.CurrentRow.Cells(colDate).Value = clsCommon.myCstr(dr("Voucher_Date"))
                    gv1.CurrentRow.Cells(colFrom).Value = clsCommon.myCstr(dr("Source_Desc"))
                    gv1.CurrentRow.Cells(colSourceNo).Value = clsCommon.myCstr(dr("Source_doc_no"))
                    gv1.CurrentRow.Cells(colVoucherno).Value = clsCommon.myCstr(dr("Voucher_No"))
                    gv1.CurrentRow.Cells(colNarration).Value = clsCommon.myCstr(dr("Narration"))
                    gv1.CurrentRow.Cells(colDesc).Value = clsCommon.myCstr(dr("Description1"))
                    gv1.CurrentRow.Cells(colDr).Value = clsCommon.myCdbl(dr("DrAmt"))
                    drAmt += clsCommon.myCdbl(dr("DrAmt"))
                    gv1.CurrentRow.Cells(colCr).Value = clsCommon.myCdbl(dr("CrAmt"))
                    crAmt += clsCommon.myCdbl(dr("CrAmt"))
                End If
                count += 1
                If count = RecCount Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow.Cells(colDesc).Value = "CLOSING BALANCE"
                    If drAmt > crAmt Then
                        gv1.CurrentRow.Cells(colDr).Value = drAmt - crAmt
                    Else
                        gv1.CurrentRow.Cells(colCr).Value = crAmt - drAmt
                    End If
                End If
            Next
            AccountCode = ""
            If gv1.Rows.Count > 0 Then
                clsCommon.MyExportToExcelGrid("GENERAL LEDGER", gv1, Nothing, "General_Ledger")
            Else
                Throw New Exception("No data found.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False

        Dim DocDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DocDate.FormatString = ""
        DocDate.HeaderText = "DATE"
        DocDate.Name = colDate
        DocDate.Width = 80
        DocDate.ReadOnly = True
        DocDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(DocDate)

        Dim From As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        From.FormatString = ""
        From.HeaderText = "FROM"
        From.Name = colFrom
        From.Width = 80
        From.ReadOnly = True
        From.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(From)

        Dim Source As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Source.FormatString = ""
        Source.HeaderText = "SOURCE NO."
        Source.Name = colSourceNo
        Source.Width = 120
        Source.ReadOnly = True
        Source.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Source)

        Dim Voucher As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Voucher.FormatString = ""
        Voucher.HeaderText = "VOUCHER NO."
        Voucher.Name = colVoucherno
        Voucher.Width = 120
        Voucher.ReadOnly = True
        Voucher.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Voucher)

        Dim narration As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        narration.FormatString = ""
        narration.HeaderText = "NARRATION"
        narration.Name = colNarration
        narration.Width = 200
        narration.ReadOnly = True
        narration.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(narration)

        Dim Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Desc.FormatString = ""
        Desc.HeaderText = "DESCRIPTION"
        Desc.Name = colDesc
        Desc.Width = 80
        Desc.ReadOnly = True
        Desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Desc)

        Dim DrAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        DrAmt = New GridViewDecimalColumn()
        DrAmt.FormatString = ""
        DrAmt.HeaderText = "DEBIT(Rs)"
        DrAmt.Name = colDr
        DrAmt.Width = 100
        DrAmt.ReadOnly = True
        DrAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(DrAmt)

        Dim CrAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        CrAmt = New GridViewDecimalColumn()
        CrAmt.FormatString = ""
        CrAmt.HeaderText = "CREDIT(Rs)"
        CrAmt.Name = colCr
        CrAmt.Width = 100
        CrAmt.ReadOnly = True
        CrAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(CrAmt)


        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
    End Sub

    Sub SetGridFormationAccount()

        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        dtView = dtFinal.DefaultView

        If dtView.Count <= 0 Then

            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Exit Sub
        End If
        gv1.DataSource = dtFinal

        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        Dim idx As Integer = 0

        gv1.Columns("Account_code").IsVisible = True
        gv1.Columns("Account_code").Width = 100
        gv1.Columns("Account_code").HeaderText = "Account"

        gv1.Columns("Account_Desc").IsVisible = True
        gv1.Columns("Account_Desc").Width = 100
        gv1.Columns("Account_Desc").HeaderText = "Description"

        gv1.Columns("opening_balance").IsVisible = True
        gv1.Columns("opening_balance").Width = 100
        gv1.Columns("opening_balance").HeaderText = "Opening Balance"
        gv1.Columns("opening_balance").TextAlignment = ContentAlignment.MiddleRight

        gv1.Columns("DrAmt").IsVisible = True
        gv1.Columns("DrAmt").Width = 100
        gv1.Columns("DrAmt").HeaderText = "Debit"

        gv1.Columns("CrAmt").IsVisible = True
        gv1.Columns("CrAmt").Width = 100
        gv1.Columns("CrAmt").HeaderText = "Credit"

        gv1.Columns("Closing_balance").IsVisible = True
        gv1.Columns("Closing_balance").Width = 100
        gv1.Columns("Closing_balance").HeaderText = "Closing Balance"
        gv1.Columns("Closing_balance").TextAlignment = ContentAlignment.MiddleRight

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
        EnableDisableControls(False)
    End Sub

    Function GetQueryForSaleRegiter() As String
        Dim strColumnToBePick As String = ""
        If rbtnVAT.IsChecked Then
            strColumnToBePick = "VatTaxAmt"
        ElseIf rbtnCST.IsChecked Then
            strColumnToBePick = "CSTTaxAmt"
        End If

        Dim qry As String = "select Sale_Invoice_No as docNo, DocType ,case when " + strColumnToBePick + "<0 then CONVERT(decimal(18,2),-1*" + strColumnToBePick + ") else 0 end as SubledgerDrAmt,case when " + strColumnToBePick + ">0 then  CONVERT(decimal(18,2)," + strColumnToBePick + ") else 0 end as SubledgerCrAmt from("
        qry += "select (xxxxxx.Sale_Invoice_No)as Sale_Invoice_No,max(xxxxxx.Sale_Invoice_Date) as Sale_Invoice_Date,max(xxxxxx.Cust_Code) as Cust_Code ,max(xxxxxx.Cust_Name) as Cust_Name,max(xxxxxx.Customer_Class) as Customer_Class ,sum(xxxxxx.Inv_Detail_Total_Amt) as Inv_Detail_Total_Amt,sum(xxxxxx.Inv_Tax_Amt) as Inv_Tax_Amt,sum(xxxxxx.Total_Invoice_Amt) as Total_Invoice_Amt,sum(xxxxxx.TotalAmount) as TotalAmount,sum(xxxxxx.TPT) as TPT,sum(xxxxxx.[Customer Discount Amt]) as [Customer Discount Amt],sum(xxxxxx.[Gross Amount]) as [Gross Amount],sum(xxxxxx.AddTaxAmt) as AddTaxAmt,sum(xxxxxx.ExciseAmt) as ExciseAmt,sum(xxxxxx.ECessAmt) as ECessAmt,sum(xxxxxx.HCessAmt) as HCessAmt,sum(xxxxxx.VatTaxAmt) as VatTaxAmt,sum(xxxxxx.CSTTaxAmt) as CSTTaxAmt,sum(xxxxxx.OtherTaxAmt) as OtherTaxAmt,sum(xxxxxx.Tot_Sale_Account_Amount) as Tot_Sale_Account_Amount,max(xxxxxx.Location_Code) as Location_Code,max(xxxxxx.Location_Desc) as Location_Desc,max(xxxxxx.Comp_Code) as Comp_Code,max(xxxxxx.Comp_Name) as Comp_Name,MAX( xxxxxx.RunDate) as RunDate,max(xxxxxx.FromDate) as FromDate,max(xxxxxx.ToDate) as ToDate,max(xxxxxx.CurrentComp) as CurrentComp,DocType from(select '08-Mar-2013' as RunDate, '01-Jan-2013' as FromDate, '08-Mar-2013' as ToDate, 'Pearl Drinks Limited' as CurrentComp,  xxxxx.Sale_Invoice_No," + Environment.NewLine
        qry += " CONVERT(varchar(10), xxxxx.Sale_Invoice_Date,103) as Sale_Invoice_Date,xxxxx.Inv_Detail_Total_Amt,xxxxx.Cust_Code,xxxxx.Cust_Name, xxxxx.Inv_Tax_Amt, xxxxx.Total_Invoice_Amt, TSPL_LOCATION_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name, xxxxx.Customer_Class,((-ISNULL(xxxxx.Inv_Discount_Amt, 0)+ISNULL(xxxxx.Inv_Detail_Total_Amt,0))+ xxxxx.Inv_Tax_Amt)as TotalAmount, xxxxx.TPT, xxxxx.Inv_Discount_Amt as [Customer Discount Amt], (-ISNULL(xxxxx.Inv_Discount_Amt, 0)+ISNULL(xxxxx.Inv_Detail_Total_Amt,0)) as [Gross Amount],  (case when  TaxM1.Type='A' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM2.Type='A' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM3.Type='A' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM4.Type='A' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM5.Type='A' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM6.Type='A' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM7.Type='A' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM8.Type='A' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM9.Type='A' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM10.Type='A' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
        qry += " ) as AddTaxAmt," + Environment.NewLine
        qry += " (case when  TaxM1.Type='V' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM2.Type='V' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM3.Type='V' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM4.Type='V' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM5.Type='V' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM6.Type='V' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM7.Type='V' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM8.Type='V' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM9.Type='V' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM10.Type='V' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
        qry += " ) as VatTaxAmt," + Environment.NewLine
        qry += " (case when  TaxM1.Type='O' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM2.Type='O' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM3.Type='O' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM4.Type='O' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM5.Type='O' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM6.Type='O' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM7.Type='O' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM8.Type='O' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM9.Type='O' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM10.Type='O' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
        qry += " ) as OtherTaxAmt," + Environment.NewLine
        qry += " (case when  TaxM1.Type='C' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM2.Type='C' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM3.Type='C' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM4.Type='C' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM5.Type='C' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM6.Type='C' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM7.Type='C' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM8.Type='C' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM9.Type='C' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " + Environment.NewLine
        qry += " +case when  TaxM10.Type='C' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " + Environment.NewLine
        qry += " ) as CSTTaxAmt," + Environment.NewLine
        qry += " (case when  TaxM1.Type='E' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " + Environment.NewLine
        qry += " ) as ExciseAmt," + Environment.NewLine
        qry += " (case when  TaxM2.Type='E' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " + Environment.NewLine
        qry += " ) as ECessAmt," + Environment.NewLine
        qry += " (case when  TaxM3.Type='E' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " + Environment.NewLine
        qry += " ) as HCessAmt" + Environment.NewLine
        qry += " , isnull(xxxxx.Tot_Sale_Account_Amount,0) as Tot_Sale_Account_Amount,xxxxx.Item_Code,xxxxx.Item_Desc,xxxxx.MRP,xxxxx.Qty ,DocType" + Environment.NewLine
        qry += " from (" + Environment.NewLine
        qry += " select * from ( " + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Item_Desc,TSPL_SALE_INVOICE_DETAIL.MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRP,TSPL_SALE_INVOICE_DETAIL.Unit_code,(case when TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N' then  TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as Qty," + Environment.NewLine
        qry += " TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt as  Inv_Detail_Total_Amt,TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt as Inv_Tax_Amt, TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt  as Total_Invoice_Amt,TSPL_SALE_INVOICE_DETAIL.Total_TPT as TPT,TSPL_SALE_INVOICE_DETAIL.Total_Disc_Amt as Inv_Discount_Amt,TSPL_SALE_INVOICE_DETAIL.TAX1,TSPL_SALE_INVOICE_DETAIL.TAX2,TSPL_SALE_INVOICE_DETAIL.TAX3,TSPL_SALE_INVOICE_DETAIL.TAX4,TSPL_SALE_INVOICE_DETAIL.TAX5,TSPL_SALE_INVOICE_DETAIL.TAX6,TSPL_SALE_INVOICE_DETAIL.TAX7,TSPL_SALE_INVOICE_DETAIL.TAX8,TSPL_SALE_INVOICE_DETAIL.TAX9,TSPL_SALE_INVOICE_DETAIL.TAX10," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX1_Amt * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX1_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX2_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX2_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX3_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX3_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX4_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX4_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX5_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX5_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX6_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX6_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX7_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX7_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX8_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX8_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX9_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX9_Amt," + Environment.NewLine
        qry += " (TSPL_SALE_INVOICE_DETAIL.TAX10_Amt* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as TAX10_Amt" + Environment.NewLine
        qry += " ,TSPL_SALE_INVOICE_HEAD.Location,TSPL_SALE_INVOICE_HEAD.Comp_Code,TSPL_SALE_INVOICE_DETAIL.Sale_Account_Amount as Tot_Sale_Account_Amount,TSPL_CUSTOMER_MASTER.Customer_Class,'SD-IN' as DocType" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join  TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join  TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code" + Environment.NewLine
        qry += " where  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date  >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "'  " + Environment.NewLine
        qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='y' AND TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N'" + Environment.NewLine
        qry += " Union all " + Environment.NewLine
        qry += " select TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No,TSPL_SALE_RETURN_HEAD.Sale_Return_Date as Sale_Invoice_Date," + Environment.NewLine
        qry += " TSPL_SALE_RETURN_DETAIL.Item_Code,TSPL_SALE_RETURN_DETAIL.Item_Desc,TSPL_SALE_RETURN_DETAIL.MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as MRP,TSPL_SALE_RETURN_DETAIL.Unit_code,-1*(case when TSPL_SALE_RETURN_DETAIL.Scheme_Item='N' then  TSPL_SALE_RETURN_DETAIL.Return_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end ) as Qty," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.Total_Basic_Amt) as Inv_Detail_Total_Amt,TSPL_SALE_RETURN_HEAD.Cust_Code,TSPL_SALE_RETURN_HEAD.Cust_Name,-1*(TSPL_SALE_RETURN_DETAIL.Total_Tax_Amt) as Inv_Tax_Amt, -1*(TSPL_SALE_RETURN_DETAIL.Total_Item_Amt)  as Total_Invoice_Amt,-1*(TSPL_SALE_RETURN_DETAIL.Total_TPT) as TPT,-1*(TSPL_SALE_RETURN_DETAIL.Total_Disc_Amt) as Inv_Discount_Amt,TSPL_SALE_RETURN_DETAIL.TAX1,TSPL_SALE_RETURN_DETAIL.TAX2,TSPL_SALE_RETURN_DETAIL.TAX3,TSPL_SALE_RETURN_DETAIL.TAX4,TSPL_SALE_RETURN_DETAIL.TAX5,TSPL_SALE_RETURN_DETAIL.TAX6,TSPL_SALE_RETURN_DETAIL.TAX7,TSPL_SALE_RETURN_DETAIL.TAX8,TSPL_SALE_RETURN_DETAIL.TAX9,TSPL_SALE_RETURN_DETAIL.TAX10," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX1_Amt * TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX1_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX2_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX2_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX3_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX3_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX4_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX4_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX5_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX5_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX6_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX6_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX7_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX7_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX8_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX8_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX9_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX9_Amt," + Environment.NewLine
        qry += " -1*(TSPL_SALE_RETURN_DETAIL.TAX10_Amt* TSPL_SALE_RETURN_DETAIL.Return_Qty) as TAX10_Amt" + Environment.NewLine
        qry += " ,TSPL_SALE_RETURN_HEAD.Location,TSPL_SALE_RETURN_HEAD.Comp_Code,-1*(isnull( TSPL_SALE_RETURN_DETAIL.Sale_Account_Amount,0)) as Tot_Sale_Account_Amount,TSPL_CUSTOMER_MASTER.Customer_Class,'SD-SR' as DocType" + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_DETAIL " + Environment.NewLine
        qry += " left outer join  TSPL_SALE_RETURN_HEAD on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No" + Environment.NewLine
        qry += " left outer join  TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_HEAD.Cust_Code " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_RETURN_DETAIL.Unit_code" + Environment.NewLine
        qry += " where  TSPL_SALE_RETURN_HEAD.Invoice_Date  >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_SALE_RETURN_HEAD.Invoice_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "'  " + Environment.NewLine
        qry += " and TSPL_SALE_RETURN_HEAD.Is_Post='y' AND TSPL_SALE_RETURN_DETAIL.Scheme_Item='N'" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_TRANSFER_HEAD.Transfer_No as Sale_Invoice_No,TSPL_TRANSFER_HEAD.Transfer_Date as Sale_Invoice_Date,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_TRANSFER_DETAIL.MRP*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRP,TSPL_TRANSFER_DETAIL.Uom as Unit_code,TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Qty,TSPL_TRANSFER_DETAIL.Total_Item_Amt as Inv_Detail_Total_Amt,TSPL_TRANSFER_HEAD.To_Location as Cust_Code,ToLoc.Location_Desc as Cust_Name,TSPL_TRANSFER_DETAIL.Total_Tax as Inv_Tax_Amt, (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax+TSPL_TRANSFER_DETAIL.TPT_Value+TSPL_TRANSFER_DETAIL.Empty_Value)*TSPL_TRANSFER_DETAIL.Item_Qty  as Total_Invoice_Amt,TSPL_TRANSFER_DETAIL.TPT_Value as TPT,0 as Inv_Discount_Amt,TSPL_TRANSFER_DETAIL.TAX1,TSPL_TRANSFER_DETAIL.TAX2,TSPL_TRANSFER_DETAIL.TAX3,TSPL_TRANSFER_DETAIL.TAX4,TSPL_TRANSFER_DETAIL.TAX5,TSPL_TRANSFER_DETAIL.TAX6,TSPL_TRANSFER_DETAIL.TAX7,TSPL_TRANSFER_DETAIL.TAX8,TSPL_TRANSFER_DETAIL.TAX9,TSPL_TRANSFER_DETAIL.TAX10," + Environment.NewLine
        qry += " TSPL_TRANSFER_DETAIL.TAX1_Amt,TSPL_TRANSFER_DETAIL.TAX2_Amt,TSPL_TRANSFER_DETAIL.TAX3_Amt,TSPL_TRANSFER_DETAIL.TAX4_Amt,TSPL_TRANSFER_DETAIL.TAX5_Amt,TSPL_TRANSFER_DETAIL.TAX6_Amt,TSPL_TRANSFER_DETAIL.TAX7_Amt,TSPL_TRANSFER_DETAIL.TAX8_Amt,TSPL_TRANSFER_DETAIL.TAX9_Amt,TSPL_TRANSFER_DETAIL.TAX10_Amt ,TSPL_TRANSFER_HEAD.From_Location as Location ,TSPL_TRANSFER_HEAD.Comp_Code,0 as Tot_Sale_Account_Amount,'' as Customer_Class,'MM-TF' as DocType" + Environment.NewLine
        qry += " from TSPL_TRANSFER_DETAIL" + Environment.NewLine
        qry += " left outer join  TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No" + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.From_Location" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom" + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code=TSPL_TRANSFER_HEAD.To_Location where TSPL_LOCATION_MASTER.Excisable='T' and TSPL_TRANSFER_HEAD.Post='Y' and " + Environment.NewLine
        qry += " TSPL_TRANSFER_HEAD.Transfer_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and  TSPL_TRANSFER_HEAD.Transfer_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' " + Environment.NewLine
        qry += " )xxx" + Environment.NewLine
        qry += " ) xxxxx" + Environment.NewLine
        qry += " Left Outer Join TSPL_LOCATION_MASTER on xxxxx.Location=TSPL_LOCATION_MASTER.Location_Code " + Environment.NewLine
        qry += " Left Outer Join TSPL_COMPANY_MASTER on xxxxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=xxxxx.TAX1" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=xxxxx.TAX2" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=xxxxx.TAX3" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=xxxxx.TAX4" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=xxxxx.TAX5" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=xxxxx.TAX6" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=xxxxx.TAX7" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=xxxxx.TAX8" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=xxxxx.TAX9" + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=xxxxx.TAX10" + Environment.NewLine
        qry += " )xxxxxx Group by xxxxxx.Sale_Invoice_No,DocType "
        qry += " )xxxxxxx "
        Return qry
    End Function

    Function GetQueryForPurchaseBook() As String
        Dim qry As String = "select docNo, DocType ,case when Landed_Cost_Amount>0 then Landed_Cost_Amount else 0 end as SubledgerDrAmt,case when Landed_Cost_Amount<0 then -1*Landed_Cost_Amount else 0 end as SubledgerCrAmt from("
        qry += " select  Document_No as docNo,MAX(DocType) as DocType,sum(Landed_Cost_Amount) as Landed_Cost_Amount from ("
        qry += " SELECT    TSPL_VENDOR_INVOICE_HEAD.Document_No ,  D.Landed_Cost_Amount,'Pur.Invoice' as DocType FROM TSPL_Item_Category "
        qry += " INNER JOIN TSPL_ITEM_MASTER ON TSPL_Item_Category.Category_Code = TSPL_ITEM_MASTER.item_category "
        qry += " INNER JOIN TSPL_ITEM_SUB_CATEGORY ON TSPL_ITEM_MASTER.Sub_item_category = TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code "
        qry += " RIGHT OUTER JOIN TSPL_PI_DETAIL AS D "
        qry += " INNER JOIN TSPL_PI_HEAD AS H ON D.PI_No = H.PI_No "
        qry += " INNER JOIN  TSPL_SRN_HEAD ON H.Against_SRN = TSPL_SRN_HEAD.SRN_No "
        qry += " INNER JOIN  TSPL_PJV_HEAD ON D.PI_No = TSPL_PJV_HEAD.Invoice_No ON TSPL_ITEM_MASTER.Item_Code = D.Item_Code  "
        qry += " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=H.PI_No"
        qry += " WHERE  H.Status =1 and (D.PI_Qty+D.Leak_Qty +D.Short_Qty+D.Burst_Qty ) >0  and  H.PI_Date  >=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and  H.PI_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "'  " ''AND TSPL_ITEM_MASTER.Item_Type not in ('F')
        qry += " ) as xxx Group By Document_No "
        qry += " )xxxx"
        Return qry
    End Function

    Function GetQueryForSaleReco() As String
        Dim strColumnToBePick As String = ""
        If rbtnSaleAccount.IsChecked Then
            strColumnToBePick = "SaleAccountAmt"
        ElseIf rbtnExciseAccount.IsChecked Then
            strColumnToBePick = "ExciseAmt"
        ElseIf rbtnECessAccount.IsChecked Then
            strColumnToBePick = "CessAmt"
        ElseIf rbtnHCessAccount.IsChecked Then
            strColumnToBePick = "HcessAmt"
        ElseIf rbtnTPT.IsChecked Then
            strColumnToBePick = "TPTAmt"
        End If
        Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt")
        Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt")

        Dim qry As String = " "
        qry += " select Sale_Invoice_No as docNo,DocType,case when " + strColumnToBePick + "<0 then -1*" + strColumnToBePick + " else 0 end  as SubledgerDrAmt,case when " + strColumnToBePick + ">0 then " + strColumnToBePick + " else 0 end  as SubledgerCrAmt from (" + Environment.NewLine
        qry += " select Sale_Invoice_No,DocType,SUM([TPT Amt]) as TPTAmt,SUM([Excise Amt]) as  ExciseAmt,SUM([Cess Amt]) as CessAmt,SUM([Hcess Amt]) as HcessAmt ,sum([Sale Account Amt]) as SaleAccountAmt  from (" + Environment.NewLine
        qry += " select Adjustment_Amount,Type,Sale_Invoice_No,Item_Code, Unit_Code,mrp,TP, CONVERT(DECIMAL(18,2),basic_rate) as basic_rate,CONVERT(DECIMAL(18,2),Excise) as Excise, CONVERT(DECIMAL(18,2),(Cess))   as Cess, CONVERT(DECIMAL(18,2),(Hcess))  as Hcess, CONVERT(DECIMAL(18,2),(DVAT))   as DVAT, [TPT Rate], CONVERT(DECIMAL(18,2),(MRP - Margin))  AS [T.Rate], (Margin)  as Margin, CONVERT(DECIMAL(18,2),(MRP - Margin))  AS [T.Price], ([GrossQTY]) as [Gross Qty], CONVERT(DECIMAL(18,2),(NetQty)) as [Net Qty], CONVERT(DECIMAL(18,2),(DiscQTY)) as [Qty Disc], convert(decimal (18,2),(FOCAMt)) as FOCAMt, (Total_Basic_Amt)  AS [Total Basic Amt],  CONVERT(DECIMAL(18,2),(Excise * InvoiceQty))  as [Excise Amt], CONVERT(DECIMAL(18,2),(Cess * InvoiceQty))    as [Cess Amt], CONVERT(DECIMAL(18,2),(Hcess * InvoiceQty))   as [Hcess Amt], CONVERT(DECIMAL(18,2),(DVAT * netInvoice_Qty))  as [DVAT Amt], CONVERT(DECIMAL(18,2),(Total_TPT))  as [TPT Amt], CONVERT(DECIMAL(18,2),((MRP - Margin) * InvoiceQty))   as [T.Rate Amt], CONVERT(DECIMAL(18,2),(Total_MRP_Amt))  as [Total MRP] , CONVERT(DECIMAL(18,2),(Margin * InvoiceQty))  as [T.Margin], CONVERT(DECIMAL(18,2),((MRP - Margin) * InvoiceQty))  as [T.Price Amt], CONVERT(DECIMAL(18,2),(isnull(ISNULL(commamt,CommHisamt) * GrossQTY,0)))  as COMMAmt, CONVERT(DECIMAL(18,2),((Cust_Discount * netInvoice_Qty) ))  as DISC,[Sale Account Amt],Total_Cust_Discount,DocType from ( " + Environment.NewLine
        qry += " select * from ( " + Environment.NewLine
        qry += " SELECT 0 as Adjustment_Amount,'Sale Invoice' as Type,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Unit_code,  TSPL_SALE_INVOICE_DETAIL.MRP_Amt * Conversion_Factor as mrp,TSPL_SALE_INVOICE_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS TP, TSPL_SALE_INVOICE_DETAIL.Basic_Rate * Conversion_Factor as Basic_Rate, CASE WHEN Excisable = 'T' THEN TSPL_SALE_INVOICE_DETAIL.TAX1_Amt ELSE 0 END AS Excise,CASE WHEN Excisable = 'T' THEN TSPL_SALE_INVOICE_DETAIL.TAX2_Amt ELSE 0 END AS Cess,  CASE WHEN Excisable = 'T' THEN TSPL_SALE_INVOICE_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, CASE WHEN Excisable = 'T' THEN TSPL_SALE_INVOICE_DETAIL.TAX4_Amt + TSPL_SALE_INVOICE_DETAIL.TAX5_Amt ELSE TSPL_SALE_INVOICE_DETAIL.TAX1_Amt END AS DVAT,TSPL_SALE_INVOICE_DETAIL.TPT  as [TPT Rate],(TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + TSPL_SALE_INVOICE_DETAIL.Price_Amount4  + TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + TSPL_SALE_INVOICE_DETAIL.Price_Amount8 + TSPL_SALE_INVOICE_DETAIL.Price_Amount9)  as Margin,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as GrossQTY,case when Scheme_Item='Y' then 0 else Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end AS NetQty,case when Scheme_Item='Y' then  Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS DiscQTY, TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt,TSPL_SALE_INVOICE_DETAIL.Total_MRP_Amt, Invoice_Qty as Invoiceqty, case when Scheme_Item='Y' then 0 else Invoice_Qty end AS netInvoice_Qty, case when Scheme_Item='Y' then Invoice_Qty else 0 end AS disc,Cust_Discount, (select top 1 Commission from TSPL_Commission_Master where Hierarchy='HOS' and Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code  and Cust_Group=Cust_Group_Code and (TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>= TSPL_Commission_Master.Start_Date) and UOM='FC'  ) as Commamt, (select top 1 Commission from TSPL_Commission_Master_History where Hierarchy='HOS' and Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code  and Cust_Group=Cust_Group_Code and (TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>= TSPL_Commission_Master_History.Start_Date and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<= TSPL_Commission_Master_History.End_Date) and UOM='FC'  ) as CommHisamt, CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR Discount_Code = NULL) THEN ((Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END AS FOCAMt, isnull(tspl_sale_invoice_detail.Sale_Account_Amount,0) as [Sale Account Amt]" + Environment.NewLine
        qry += " ,Total_Cust_Discount,Total_TPT,'SD-IN'  as DocType " + Environment.NewLine
        qry += " FROM TSPL_LOCATION_MASTER " + Environment.NewLine
        qry += " RIGHT OUTER JOIN TSPL_SALE_INVOICE_HEAD " + Environment.NewLine
        qry += " INNER JOIN TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_INVOICE_HEAD.Location " + Environment.NewLine
        qry += " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER " + Environment.NewLine
        qry += " RIGHT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine
        qry += " where  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date  >= '" + strFromDate + "' AND   TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <= '" + strToDate + "' and  Is_Post='Y'   and Scheme_Item='N'" + Environment.NewLine
        qry += " Union all " + Environment.NewLine
        qry += " SELECT 0 as Adjustment_Amount,'Sale Return' as Type,TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No, TSPL_SALE_RETURN_DETAIL.Item_Code,TSPL_SALE_RETURN_DETAIL.Unit_code, TSPL_SALE_RETURN_DETAIL.MRP_Amt * Conversion_Factor as mrp, TSPL_SALE_RETURN_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - TSPL_SALE_RETURN_DETAIL.Price_Amount1 AS TP,TSPL_SALE_RETURN_DETAIL.Basic_Rate * Conversion_Factor as Basic_Rate, CASE WHEN Excisable = 'T' THEN TSPL_SALE_RETURN_DETAIL.TAX1_Amt ELSE 0 END AS Excise, CASE WHEN Excisable = 'T' THEN TSPL_SALE_RETURN_DETAIL.TAX2_Amt ELSE 0 END AS Cess, CASE WHEN Excisable = 'T' THEN TSPL_SALE_RETURN_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, CASE WHEN Excisable = 'T' THEN TSPL_SALE_RETURN_DETAIL.TAX4_Amt + TSPL_SALE_RETURN_DETAIL.TAX5_Amt ELSE TSPL_SALE_RETURN_DETAIL.TAX1_Amt END AS DVAT, TSPL_SALE_RETURN_DETAIL.TPT  as [TPT Rate], (TSPL_SALE_RETURN_DETAIL.Price_Amount1 + TSPL_SALE_RETURN_DETAIL.Price_Amount2 + TSPL_SALE_RETURN_DETAIL.Price_Amount3 + TSPL_SALE_RETURN_DETAIL.Price_Amount4  + TSPL_SALE_RETURN_DETAIL.Price_Amount5 + TSPL_SALE_RETURN_DETAIL.Price_Amount6 + TSPL_SALE_RETURN_DETAIL.Price_Amount7 + TSPL_SALE_RETURN_DETAIL.Price_Amount8 + TSPL_SALE_RETURN_DETAIL.Price_Amount9)  as Margin, - (TSPL_SALE_RETURN_DETAIL.Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  as GrossQTY, -(case when Scheme_Item='Y' then 0 else Return_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) AS NetQty, -(case when Scheme_Item='Y' then  Return_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) AS DiscQTY, -(TSPL_SALE_RETURN_DETAIL.Total_Basic_Amt) as Total_Basic_Amt,  -(TSPL_SALE_RETURN_DETAIL.Total_MRP_Amt) as Total_MRP_Amt, -(Return_Qty) as Invoiceqty, -(case when Scheme_Item='Y' then 0 else Return_Qty end) AS netInvoice_Qty, -(case when Scheme_Item='Y' then Return_Qty else 0 end) AS disc, - (Cust_Discount) as Cust_Discount, ((select top 1 Commission from TSPL_Commission_Master where Hierarchy='HOS' and  Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code  and Cust_Group=Cust_Group_Code and  (TSPL_SALE_RETURN_HEAD.Invoice_Date>= TSPL_Commission_Master.Start_Date) and UOM='FC'  )) as Commamt, ((select top 1 Commission from TSPL_Commission_Master_History where Hierarchy='HOS' and Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code  and Cust_Group=Cust_Group_Code and  (TSPL_SALE_RETURN_HEAD.Invoice_Date>= TSPL_Commission_Master_History.Start_Date and TSPL_SALE_RETURN_HEAD.Invoice_Date<= TSPL_Commission_Master_History.End_Date) and UOM='FC'  )) as CommHisamt, -(CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR Discount_Code = NULL) THEN -((Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS FOCAMt, -isnull(TSPL_SALE_RETURN_DETAIL.Sale_Account_Amount,0) as [Sale Account Amt],- (Total_Cust_Discount ) as Total_Cust_Discount,-(Total_TPT) as Total_TPT ,'SD-SR' as DocType" + Environment.NewLine
        qry += " FROM TSPL_LOCATION_MASTER " + Environment.NewLine
        qry += " RIGHT OUTER JOIN TSPL_SALE_RETURN_HEAD " + Environment.NewLine
        qry += " INNER JOIN TSPL_SALE_RETURN_DETAIL ON TSPL_SALE_RETURN_HEAD.Sale_Return_No = TSPL_SALE_RETURN_DETAIL.Sale_Return_No " + Environment.NewLine
        qry += " INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND  TSPL_SALE_RETURN_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_RETURN_HEAD.Location " + Environment.NewLine
        qry += " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER " + Environment.NewLine
        qry += " RIGHT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code ON TSPL_SALE_RETURN_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine
        qry += " where  TSPL_SALE_RETURN_HEAD.Invoice_Date >= '" + strFromDate + "' AND  TSPL_SALE_RETURN_HEAD.Invoice_Date <= '" + strToDate + "' and TSPL_SALE_RETURN_HEAD.Is_Post='Y' and Scheme_Item='N'  " + Environment.NewLine
        qry += " Union all " + Environment.NewLine
        qry += " SELECT Adjustment_Amount,'Sale Invoice' as Type, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,'' as Item_Code,'' as Unit_code, 0 as mrp,0 AS TP, 0 as Basic_Rate, 0 AS Excise,0 AS Cess,  0 AS Hcess, 0 AS DVAT, 0 as [TPT Rate],0  as Margin,0  as GrossQTY,0 AS NetQty,0 AS DiscQTY, 0 as Total_Basic_Amt, 0 as Total_MRP_Amt, 0 as Invoiceqty, 0 AS netInvoice_Qty, 0 AS disc,0 as Cust_Discount, 0 as Commamt, 0 as CommHisamt, 0 AS FOCAMt, 0 as [Sale Account Amt],0 as Total_Cust_Discount,0 as Total_TPT,'SD-IN' as  DocType" + Environment.NewLine
        qry += " FROM  TSPL_LOCATION_MASTER " + Environment.NewLine
        qry += " RIGHT OUTER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_INVOICE_HEAD.Location " + Environment.NewLine
        qry += " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code Right outer join TSPL_Receipt_Adjustment_Header on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No =TSPL_Receipt_Adjustment_Header.Doc_No " + Environment.NewLine
        qry += " where   TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >= '" + strFromDate + "' AND  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <= '" + strToDate + "'   " + Environment.NewLine
        qry += " Union all  " + Environment.NewLine
        qry += " SELECT 0 as Adjustment_Amount,'Sale InerCompany' as Type,TSPL_SALE_RETURN_INTER_HEAD.Document_No as Sale_Invoice_No, TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, TSPL_SALE_RETURN_INTER_DETAIL.Unit_code, TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * Conversion_Factor as mrp, TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 AS TP, TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate * Conversion_Factor as Basic_Rate, CASE WHEN Excisable = 'T' THEN TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt ELSE 0 END AS Excise, CASE WHEN Excisable = 'T' THEN TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt ELSE 0 END AS Cess, CASE WHEN Excisable = 'T' THEN TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, CASE WHEN Excisable = 'T' THEN TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt + TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt ELSE TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt END AS DVAT, TSPL_SALE_RETURN_INTER_DETAIL.TPT  as [TPT Rate], (TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 + TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount2 + TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount3 + TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount4  + TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount5 + TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount6 + TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount7 + TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount8 + TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount9)  as Margin, - TSPL_SALE_RETURN_INTER_DETAIL.Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as GrossQTY, - case when Scheme_Item='Y' then 0 else qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end AS NetQty, - case when Scheme_Item='Y' then  Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS DiscQTY, - TSPL_SALE_RETURN_INTER_DETAIL.Total_Basic_Amt as  Total_Basic_Amt, - TSPL_SALE_RETURN_INTER_DETAIL.Total_MRP_Amt as Total_MRP_Amt, - Qty as Invoiceqty, - case when Scheme_Item='Y' then 0 else Qty end AS netInvoice_Qty, - case when Scheme_Item='Y' then Qty else 0 end AS disc,- Cust_Discount as Cust_Discount, - (select top 1 Commission from TSPL_Commission_Master where Hierarchy='HOS' and Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code  and  Cust_Group=Cust_Group_Code and (TSPL_SALE_RETURN_INTER_HEAD.Document_Date >= TSPL_Commission_Master.Start_Date) and UOM='FC'  ) as Commamt, - (select top 1 Commission from TSPL_Commission_Master_History where Hierarchy='HOS' and Item_Code=TSPL_SALE_RETURN_INTER_DETAIL.Item_Code  and Cust_Group=Cust_Group_Code and (TSPL_SALE_RETURN_INTER_HEAD.Document_Date>= TSPL_Commission_Master_History.Start_Date and TSPL_SALE_RETURN_INTER_HEAD.Document_Date<= TSPL_Commission_Master_History.End_Date) and UOM='FC'  ) as CommHisamt, 0 AS FOCAMt, - TSPL_SALE_RETURN_INTER_DETAIL.Sale_Account_Amount as [Sale Account Amt],- Total_Cust_Discount as Total_Cust_Discount,-(TSPL_SALE_RETURN_INTER_DETAIL.Total_TPT ) as Total_TPT,'SD-SR' as  DocType" + Environment.NewLine
        qry += " FROM TSPL_LOCATION_MASTER " + Environment.NewLine
        qry += " RIGHT OUTER JOIN TSPL_SALE_RETURN_INTER_HEAD " + Environment.NewLine
        qry += " INNER JOIN TSPL_SALE_RETURN_INTER_DETAIL ON TSPL_SALE_RETURN_INTER_HEAD.Document_No = TSPL_SALE_RETURN_INTER_DETAIL.Document_No " + Environment.NewLine
        qry += " INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SALE_RETURN_INTER_HEAD.Location " + Environment.NewLine
        qry += " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER " + Environment.NewLine
        qry += " RIGHT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine
        qry += " where  TSPL_SALE_RETURN_INTER_HEAD.Document_Date >=  '" + strFromDate + "' AND  TSPL_SALE_RETURN_INTER_HEAD.Document_Date <=  '" + strToDate + "' and  Is_Post=1  " + Environment.NewLine
        qry += " )xxx" + Environment.NewLine
        qry += " ) a " + Environment.NewLine
        qry += " )xxxx group by Sale_Invoice_No,DocType" + Environment.NewLine
        qry += " )xxxxx"

        Return qry
    End Function

    Function GetQueryForBank() As String
        Dim qry As String = " select  DocNo ,DocType,case when SubledgerDrAmt-SubledgerCrAmt > 0 then SubledgerDrAmt-SubledgerCrAmt else 0 end as SubledgerDrAmt ,case when SubledgerCrAmt-SubledgerDrAmt>0 then SubledgerCrAmt-SubledgerDrAmt else 0 end as SubledgerCrAmt from (  Select  DocNo , SUM( Debit_Amount) as SubledgerDrAmt , SUM( Credit_Amount) as SubledgerCrAmt,DocType from ( "
        qry += "  Select 'Bank Book' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '17-Jan-2013' as RunDate, '01-Dec-2012' as Startdate, '17-Jan-2013' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType ,  TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   ,  (TotDebAmt-TotCredAmt ) as BalAmt, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, (TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end)  as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,DocType  From ("
        qry += "  Select DISTINCT '' AS [Id], '' AS [SourceDoc_No], '' AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name], BANK_CODE AS [Bank_Code], '' AS [Bank_Name], '' AS [Loc_Code], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount], (select isnull(SUM(Credit_Amount),0) from TSPL_BANK_BOOK a where  a.BANKGL_Account_Code=TSPL_BANK_BOOK.BANKGL_Account_Code AND a.BANK_CODE=TSPL_BANK_BOOK.BANK_CODE and  CONVERT(date, a.sourceDoc_Date,103) < CONVERT(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "',103)) as TotCredAmt, (select isnull(SUM(Debit_Amount),0) from TSPL_BANK_BOOK b where  b.BANKGL_Account_Code=TSPL_BANK_BOOK.BANKGL_Account_Code AND b.BANK_CODE=TSPL_BANK_BOOK.BANK_CODE and CONVERT(date, b.sourceDoc_Date,103) < CONVERT(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "',103)) as TotDebAmt,"
        qry += "(case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine
        qry += " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine
        qry += " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine
        qry += " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType" + Environment.NewLine
        qry += "  from TSPL_BANK_BOOk WHERE 1=1    AND SOURCEDOC_NO in ("
        qry += " Select Receipt_No as DocNo From TSPL_RECEIPT_HEADER Where  TSPL_RECEIPT_HEADER.Posted = 'Y'  "
        qry += " Union All "
        qry += " Select Payment_No  as DocNo From TSPL_PAYMENT_HEADER Where TSPL_PAYMENT_HEADER.Posted = 'P'   "
        qry += " Union All "
        qry += " Select Transfer_No as DocNo from TSPL_BANK_TRANSFER Where TSPL_BANK_TRANSFER.Post = 'P'   "
        qry += " Union All"
        qry += " Select Reverse_Code as DocNo  from TSPL_BANK_REVERSE Where TSPL_BANK_REVERSE.Post = 'P' )   "
        qry += " UNION All "
        qry += " Select Id, SOURCEDOC_NO, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, BANK_CODE, BANK_NAME, LOC_CODE, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, CHEQUE_NO as ChequeNo, CHEQUE_DATE as ChequeDate, NARR_MASTER, NARR_DETAIL, Debit_Amount, Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt,"
        qry += "(case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine
        qry += " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine
        qry += " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine
        qry += " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType" + Environment.NewLine
        qry += "  from TSPL_BANK_BOOk WHERE CONVERT(Date, SOURCEDOC_DATE, 103)>=CONVERT(date, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "', 103) AND  CONVERT(date,SOURCEDOC_DATE, 103)<=CONVERT(date, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "', 103)    AND SOURCEDOC_NO in (Select Receipt_No as DocNo From TSPL_RECEIPT_HEADER Where  TSPL_RECEIPT_HEADER.Posted = 'Y'  "
        qry += " Union All "
        qry += " Select Payment_No  as DocNo From TSPL_PAYMENT_HEADER Where TSPL_PAYMENT_HEADER.Posted = 'P'   "
        qry += "   Union All "
        qry += " Select Transfer_No as DocNo from TSPL_BANK_TRANSFER Where TSPL_BANK_TRANSFER.Post = 'P'   "
        qry += " Union All "
        qry += " Select Reverse_Code as DocNo  from TSPL_BANK_REVERSE Where TSPL_BANK_REVERSE.Post = 'P' )   ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON 'DEMO'=TSPL_COMPANY_MASTER.Comp_Code  Where 1=1  And Bank_type='B'  "
        qry += " ) YYY Where ISNULL(DocNo, '')<>''  "
        qry += " Group By DocNo,DocType )xyz"
        Return qry
    End Function

    Function GetQueryForCustomer() As String
        Dim qry As String = " select DocNo,(case when DrAmt-CrAmt>0 then DrAmt-CrAmt else 0 end) as SubledgerDrAmt, (case when CrAmt-DrAmt>0 then CrAmt-DrAmt else 0 end) as SubledgerCrAmt,DocType from ("
        qry += " select DocNo,sum(DrAmt) as DrAmt,sum(CrAmt) as CrAmt,DocType from ("
        qry += " select  ACode,AName,DocNo,DocDate,DocType, CrAmt,DrAmt   from ("
        qry += " select Cust_Code as ACode ,(cust_name) as AName, Sale_Invoice_No as DocNo,convert(varchar(11),Sale_Invoice_Date,103) as DocDate,'SD-IN' as DocType,((Description)+(case when description='' then '' else ' - ' end) +(Shipment_No))  as DocNarr,'' as ChequeDetails,0 as CrAmt,(Empty_Value+Total_Invoice_Amt) as DrAmt , TSPL_LOCATION_MASTER.Loc_Segment_Code as Location,Empty_Value as [Empty Value],Total_Invoice_Amt as [DocTot] "
        qry += " from TSPL_SALE_INVOICE_HEAD "
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location  "
        qry += " WHERE  TSPL_SALE_INVOICE_HEAD.Is_Post='y'   "
        qry += " union all "
        qry += " Select Cust_Code as ACode,Customer_Name as AName,Receipt_No as DocNo,CONVERT(Varchar,Receipt_Date,103) as DocDate,case when Receipt_Type='O' then 'AR-OA' else case when Receipt_Type='F' then 'AR-RF' else  case when Receipt_Type='P' then 'AR-PI' else  case when Receipt_Type='U' then 'AR-UC' else  case when Receipt_Type='R' then 'AR-PY' else  case when Receipt_Type='M' then 'AR-MI' else '' end end end end end end as DocType,'' as DocNarr,(rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end) + Cheque_No + (case when LEN(ISNULL(Cheque_No,''))>0 then ' - '+Cheque_Date else '' end)) as ChequeDetails, case when Receipt_Type='F' then 0 else Receipt_Amount end as CrAmt,case when Receipt_Type='F' then Receipt_Amount else 0 end as DrAmt ,substring(TSPL_RECEIPT_HEADER.Dr_Account,len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) as [Location] ,0 ,Receipt_Amount as DocTot "
        qry += " from TSPL_RECEIPT_HEADER"
        qry += " where  TSPL_RECEIPT_HEADER.Posted='Y' and TSPL_RECEIPT_HEADER.Receipt_Type not in ('M')"
        qry += " union all"
        qry += " SELECT    max(TSPL_ADJUSTMENT_HEADER.Customer_CODE ) as ACode,max(TSPL_ADJUSTMENT_HEADER.Customer_NAME ) as AName, Final.Adjustment_No AS DocNo, CONVERT(varchar(11), MAX(Final.Adjustment_Date), 103) AS DocDate, 'IC-AD' AS DocType, MAX(Final.Description) AS DocNarr, '' AS ChequeDetails, case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost) else 0 end AS CrAmt, case when max(Final.Trans_Type) = 'In' then 0 else  SUM(Final.Cost)  end  AS DrAmt ,max(Final.Location) as [Location],SUM(Final.Cost),0 FROM ("
        qry += " SELECT TSPL_ADJUSTMENT_HEADER.Adjustment_No, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, TSPL_ADJUSTMENT_HEADER.Description + (CASE WHEN TSPL_ADJUSTMENT_HEADER.Description = '' THEN '' ELSE ' - ' END) AS Description, case when tspl_customer_master.cust_type_code ='F' or tspl_customer_master.cust_type_code ='S' then 0 else  ISNULL(TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) end  + ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) AS Cost, TSPL_ADJUSTMENT_HEADER.Document_No ,TSPL_LOCATION_MASTER.Loc_Segment_Code as [Location],TSPL_ADJUSTMENT_HEADER.Trans_Type   "
        qry += " FROM TSPL_ADJUSTMENT_HEADER "
        qry += " LEFT OUTER JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No "
        qry += " LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_ADJUSTMENT_HEADER.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No "
        qry += " Left OUTER JOIN TSPL_LOCATION_MASTER on TSPL_ADJUSTMENT_DETAIL.Location_Code=TSPL_LOCATION_MASTER.Location_Code "
        qry += " inner join tspl_customer_master on tspl_adjustment_header.customer_code=tspl_customer_master.cust_code   "
        qry += " WHERE ( (TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND (TSPL_ADJUSTMENT_HEADER.Document_No= '' and TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '')   or   TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice')  AND (ISNULL(TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) + ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) <> 0)and TSPL_ADJUSTMENT_HEADER.Posted='Y'"
        qry += " ) AS Final "
        qry += " INNER JOIN TSPL_ADJUSTMENT_HEADER  ON Final.Adjustment_No = TSPL_ADJUSTMENT_HEADER.Adjustment_No  "
        qry += " GROUP BY Final.Adjustment_No "
        qry += " union all"
        qry += " select TSPL_Customer_Invoice_Head.Customer_Code ,TSPL_Customer_Invoice_Head.Customer_Name ,case when len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 then  TSPL_Customer_Invoice_Head.AgainstScrap else  TSPL_Customer_Invoice_Head.Document_No  end DocNo,convert(varchar(11),TSPL_Customer_Invoice_Head.Document_Date,103) ,(case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'AR-IN' else case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'AR-CR' else case when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'AR-DN' end end end) as [DocType], TSPL_Customer_Invoice_Head.Description ,'',case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end as [CRAmt] ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 else TSPL_Customer_Invoice_Head.Document_Total end as [DRAmt] ,TSPL_Customer_Invoice_Head.Loc_Code, 0,TSPL_Customer_Invoice_Head.Document_Total   "
        qry += " from TSPL_Customer_Invoice_Head "
        qry += " where TSPL_Customer_Invoice_Head.Status=1  "
        qry += " union all"
        qry += " select Cust_Code as ACode ,(cust_name) as AName, Sale_Return_No  as DocNo,convert(varchar(11),Sale_Return_Date ,103) as DocDate,'SD-SR' as DocType,((Description)+ (case when description='' then '' else ' - ' end) +(Invoice_No))  as DocNarr,'' as ChequeDetails,(Empty_Value+Total_Invoice_Amt) as CrAmt,0 as DrAmt , TSPL_LOCATION_MASTER.Loc_Segment_Code as Location ,Empty_Value,Total_Invoice_Amt "
        qry += " from TSPL_SALE_RETURN_HEAD "
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_RETURN_HEAD.Location  "
        qry += " where TSPL_SALE_RETURN_HEAD.Is_Post='Y' "
        qry += " union all"
        qry += " select TSPL_SALE_RETURN_INTER_HEAD.Cust_Code as ACode ,(TSPL_SALE_RETURN_INTER_HEAD.cust_name) as AName, TSPL_SALE_RETURN_INTER_HEAD.Document_No  as DocNo, CONVERT(Varchar,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as DocDate,'SD-SR' as DocType,((TSPL_SALE_RETURN_INTER_HEAD.Description)+ (case when TSPL_SALE_RETURN_INTER_HEAD.description='' then '' else ' - ' end)  )  as DocNarr,'' as ChequeDetails,(TSPL_SALE_RETURN_INTER_HEAD.Empty_Value+TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt) as CrAmt,0 as DrAmt , TSPL_LOCATION_MASTER.Loc_Segment_Code as Location,TSPL_SALE_RETURN_INTER_HEAD.Empty_Value,TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt from  TSPL_SALE_RETURN_INTER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_RETURN_INTER_HEAD.Location  where TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1union all select TSPL_Receipt_Adjustment_Header.Customer_No as ACode,TSPL_CUSTOMER_MASTER.Customer_Name as AName,Adjustment_No as DocNo,CONVERT(varchar(10),Adjustment_Date,103) as DocDate,'AD' as DocType,TSPL_Receipt_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails,TSPL_Receipt_Adjustment_Header.Adjustment_Amount as CrAmt,0 as DrAmt ,TSPL_LOCATION_MASTER.Loc_Segment_Code as Location ,0 as [Empty Value],TSPL_Receipt_Adjustment_Header.Adjustment_Amount  as [DocTot] "
        qry += " from TSPL_Receipt_Adjustment_Header "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Receipt_Adjustment_Header.Customer_No "
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_Receipt_Adjustment_Header.Doc_No "
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location "
        qry += " where TSPL_Receipt_Adjustment_Header.Is_Post='Y' "
        qry += " union all"
        qry += " select   TSPL_BANK_REVERSE.Cust_Code as ACode, TSPL_BANK_REVERSE.Cust_Name as AName,TSPL_BANK_REVERSE.Reverse_Code as DocNo ,CONVERT(Varchar,TSPL_BANK_REVERSE.Reversal_Date,103) as DocDate , 'RV-TA' as DocType, TSPL_BANK_REVERSE.Document_No as DocNarr,TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, 0 as CrAmt , TSPL_BANK_REVERSE.Amount as  DrAmt,substring( TSPL_BANK_MASTER.BANKACC ,len(TSPL_BANK_MASTER.BANKACC )-2,3) as [Location],0,TSPL_BANK_REVERSE.Amount "
        qry += " from TSPL_BANK_REVERSE  "
        qry += " left outer join TSPL_BANK_MASTER on TSPL_BANK_REVERSE .Bank_Code =TSPL_BANK_MASTER.BANK_CODE     "
        qry += " where TSPL_BANK_REVERSE.Source_Type='AR' and TSPL_BANK_REVERSE.post='P'   "
        qry += " union all"
        qry += " select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,convert(varchar(11),TSPL_VCGL_Head.Document_Date,103) as DocDate,'VC-GL' as docType, TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails,case when TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount else 0 end as CrAmt,case when TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount else 0 end  as DrAmt , TSPL_VCGL_Head.Location_Segment as Location,0,TSPL_VCGL_Head.Amount "
        qry += " from  TSPL_VCGL_Head  "
        qry += " where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 "
        qry += " union all"
        qry += " select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,convert(varchar(11),TSPL_VCGL_Head.Document_Date,103) as DocDate,'VC-GL' as docType, TSPL_VCGL_Detail.Remarks as DocNarr,'' as ChequeDetails,TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt , TSPL_VCGL_Head.Location_Segment as Location ,0,TSPL_VCGL_Detail.Dr_Amount "
        qry += " from TSPL_VCGL_Detail "
        qry += " left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No "
        qry += " where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' "
        qry += " ) Final "
        qry += " left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code "
        qry += " where convert(date,final.DocDate,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "',103) and  convert(date,final.DocDate,103)<=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "',103)  "
        qry += " and LEN(ACode)>0   "
        qry += " )SuperFinal group by DocNo,DocType )xxxxx"
        Return qry
    End Function

    Function GetQueryForVendor() As String
        Dim qry As String = "select docNo,(case when DrAmt-CrAmt>0 then DrAmt-CrAmt else 0 end) as SubledgerDrAmt,(case when CrAmt-DrAmt>0 then CrAmt-DrAmt else 0 end) as SubledgerCrAmt,GLDocType as DocType from (  "
        qry += " select  DocNo ,sum(DrAmt) as DrAmt,SUM( CrAmt) as CrAmt,SUM(1) as Rep,GLDocType   from( "
        qry += " select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when Document_Type='D' then 'Debit Note' else case when Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then '' else ' - 'end) +(RefDocNo)+Vendor_Invoice_No ) as DocNarr,'' as ChequeDetails,((case when Document_Type IN('I','C') AND TAX1_Amt<0 then (-1*TAX1_Amt) else 0 end + case when Document_Type IN('I','C') AND TAX2_Amt<0 then (-1*TAX2_Amt)  else 0   end + case when Document_Type IN('I','C') AND TAX3_Amt<0 then (-1*TAX3_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX4_Amt<0 then (-1*TAX4_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX5_Amt<0 then (-1*TAX5_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX6_Amt<0 then (-1*TAX6_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX7_Amt<0 then (-1*TAX7_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX8_Amt<0 then (-1*TAX8_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX9_Amt<0 then (-1*TAX9_Amt) else  0  end + case when Document_Type IN('I','C') AND TAX10_Amt<0 then (-1*TAX10_Amt) else 0 end)+case when Document_Type IN('I','C') then document_total else 0 end  ) as CrAmt, case when Document_Type IN('D') then Document_Total else 0 end as DrAmt,Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,(" + Environment.NewLine
        qry += " select GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,'AP-IN' as GLDocType   from tspl_vendor_invoice_head    left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0 " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX1 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX1_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX1  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX1_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX2 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX2_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX2  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX2_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX3 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX3_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX3  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX3_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX4 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX4_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX4  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX4_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX5 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX5_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX5  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX5_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX6 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX6_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX6  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX6_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX7 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX7_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX7  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX7_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX8 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX8_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX8  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX8_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX9 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX9_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX9  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX9_Amt<0  " + Environment.NewLine
        qry += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX10 as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX10_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX10  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX10_Amt<0  " + Environment.NewLine
        qry += " union all select TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.Document_Total as CrAmt, 0  as DrAmt,'I' as Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date,'AP-IN' as GLDocType    from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No  left outer join TSPL_COMPANY_MASTER on TSPL_PI_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1 " + Environment.NewLine
        qry += " union all select Vendor_Code ,Vendor_Name ,Document_No ,'TDS' as [DocType],convert(date,Document_Date,103)as Document_Date,'', '',case when Document_Type IN('I','C','OA','AV') then 0 else Actual_Total_TDS END,case when Document_Type IN('I','C','OA','AV') then Actual_Total_TDS else 0 END  ,'TDS',0,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, right( Branch_GL_AC,3) as account,'' as Posting_Date,case when TSPL_REMITTANCE.Document_Type in ('D','C','I') then 'AP-IN' else case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end end as GLDocType   from TSPL_REMITTANCE left outer join TSPL_COMPANY_MASTER on TSPL_REMITTANCE.Comp_Code  =TSPL_COMPANY_MASTER.Comp_Code where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 " + Environment.NewLine
        qry += " union all select TSPL_REMITTANCE.Vendor_Code ,TSPL_REMITTANCE.Vendor_Name ,TSPL_REMITTANCE.Document_No ,'TDS REVERSE' as [DocType],convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)as Document_Date,'', '',0 AS CrAmt,   case when (TSPL_PAYMENT_HEADER.Cheque_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end  as DrAmt ,'TDS',0,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, right( Branch_GL_AC,3) as account,'' as Posting_Date,'RV-TA' as GLDocType from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Cheque_No=TSPL_REMITTANCE.Document_No    inner join  TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No  left outer join TSPL_COMPANY_MASTER on  TSPL_REMITTANCE.comp_code=TSPL_COMPANY_MASTER.Comp_Code where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P'  and   Branch_GL_AC  is not null and Actual_Total_TDS<>0 " + Environment.NewLine
        qry += " union all select TSPL_BANK_REVERSE.vendor_code as VCode,TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate,Document_No as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+Pay_Rec_Date )as ChequeDetails, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 else ( case when ( TSPL_PAYMENT_HEADER.Total_Prepayment) IS null then TSPL_BANK_REVERSE.Amount else  TSPL_PAYMENT_HEADER.Total_Prepayment end )end as CrAmt,case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then  ( case when ( TSPL_PAYMENT_HEADER.Total_Prepayment) IS null then TSPL_BANK_REVERSE.Amount else  TSPL_PAYMENT_HEADER.Total_Prepayment end ) else 0 end  as DrAmt,'RV'as Document_Type, amount as Balance_Amount,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,right(BANKACC,3) account,'' as Posting_Date,'RV-TA' as GLDocType from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Cheque_No=TSPL_BANK_REVERSE.Cheque_No left outer join TSPL_COMPANY_MASTER on TSPL_BANK_REVERSE .Comp_Code  =TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y'  " + Environment.NewLine
        qry += " union all select  VC_Code as VCode,VC_Name as VName,Document_No as DocNo,case when Document_Type='V' then 'Vendor' else '' end as DocType,CONVERT(Date,Document_Date,103) as DocDate,Remarks as DocNarr,'' as ChequeDetails,(case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt,(case when  Document_Type='V' then 'Vendor' else '' end ) as Document_Type ,0 as Balance_Amount, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,(TSPL_VCGL_Head.Location_Segment) as account,'' as Posting_Date,'VC-GL' as GLDocType from TSPL_VCGL_Head   left outer join TSPL_COMPANY_MASTER on TSPL_VCGL_Head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code where Document_Type='v' and TSPL_VCGL_Head.Status='1' " + Environment.NewLine
        qry += " union all select TSPL_VCGL_Detail.VCGL_Code as VCode,TSPL_VCGL_Detail.VCGL_Name as VName,TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Detail.Row_Type as DocType,CONVERT(date,TSPL_VCGL_Head.Document_Date,103) as DocDate,TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails,TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt, TSPL_VCGL_Detail.Row_Type as Document_Type,0 as Balance_Amount, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,(TSPL_VCGL_Head.Location_Segment) as account,'' as Posting_Date,'VC-GL' as GLDocType from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_VCGL_Head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code    where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' " + Environment.NewLine
        qry += " union all select vendor_code as VCode,vendor_name as VName,payment_no as DocNo ,case when Payment_Type='AV' then 'Advance' else case when Payment_Type='OA' then 'On Account' else case  when Payment_Type='py' then 'Payment' else case when Payment_Type='RC' then 'Receipt' else 'Mislleneous' end end end   end as DocType,COnvert(date,payment_date, 103) as DocDate,((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr,((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +( Cheque_Date )) as ChequeDetails,case when payment_type in ('m','RC') then Total_Prepayment else 0 end as CrAmt ,case when payment_type IN('py','oa','Av','mi') then Payment_Amount  else 0 end as DrAmt, Payment_Type as Document_Type,Payment_Amount as Balance_Amount, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,right(TSPL_BANK_MASTER.BANKACC,3) account,'' as Posting_Date,case when Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType  from TSPL_PAYMENT_HEADER  left outer join TSPL_COMPANY_MASTER on tspl_payment_header.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE Where Posted='P'" + Environment.NewLine
        qry += " )final " + Environment.NewLine
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine
        qry += " Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine
        qry += " where final.Document_Type in ('TDS','I','C','D','AV','OA','PY','MI','RV','Vendor','RC') and CONVERT(date,DocDate ,103)  <=CONVERT(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103) and  CONVERT(date,DocDate ,103) >=CONVERT(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103)   and DocType <>'Mislleneous'" + Environment.NewLine
        qry += " group by DocNo,GLDocType" + Environment.NewLine
        qry += " )SuperFinal  "
        Return qry
    End Function

    Sub SetGridFormationForGroupingType()
        Dim isShowSubLedger As Boolean = False
        If Not clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "None") = CompairStringResult.Equal Then
            If pnlAdminSetting.Visible And Not rbtnNone.IsChecked Then
                isShowSubLedger = True
            End If
        End If
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next


        gv1.Columns("Account_code").IsVisible = False
        gv1.Columns("Account_code").Width = 100
        gv1.Columns("Account_code").HeaderText = "Account Code"

        gv1.Columns("Account_Desc").IsVisible = False
        gv1.Columns("Account_Desc").Width = 200
        gv1.Columns("Account_Desc").HeaderText = "Account Description"

        gv1.Columns("Voucher_No").IsVisible = True
        gv1.Columns("Voucher_No").Width = 100
        gv1.Columns("Voucher_No").HeaderText = "Voucher No"

        gv1.Columns("Voucher_Date").IsVisible = True
        gv1.Columns("Voucher_Date").Width = 100
        gv1.Columns("Voucher_Date").HeaderText = "Voucher Date"

        gv1.Columns("Description").IsVisible = True
        gv1.Columns("Description").Width = 200

        gv1.Columns("Remarks").IsVisible = True
        gv1.Columns("Remarks").Width = 200

        gv1.Columns("Comments").IsVisible = True
        gv1.Columns("Comments").Width = 200

        gv1.Columns("Vendor Code").IsVisible = True
        gv1.Columns("Vendor Code").Width = 200

        gv1.Columns("Customer Code").IsVisible = True
        gv1.Columns("Customer Code").Width = 200

        gv1.Columns("Vendor").IsVisible = True
        gv1.Columns("Vendor").Width = 200

        gv1.Columns("Customer").IsVisible = True
        gv1.Columns("Customer").Width = 200

        gv1.Columns("Source_Code").IsVisible = True
        gv1.Columns("Source_Code").Width = 100
        gv1.Columns("Source_Code").HeaderText = "Source Code"

        gv1.Columns("Source_Desc").IsVisible = True
        gv1.Columns("Source_Desc").Width = 200
        gv1.Columns("Source_Desc").HeaderText = "Source Description"

        gv1.Columns("Source_doc_No").IsVisible = True
        gv1.Columns("Source_doc_No").Width = 100
        gv1.Columns("Source_doc_No").HeaderText = "Source Doc No"

        gv1.Columns("TotalDrAmt").IsVisible = True
        gv1.Columns("TotalDrAmt").Width = 100
        gv1.Columns("TotalDrAmt").HeaderText = "Dr Amt"

        gv1.Columns("TotalCrAmt").IsVisible = True
        gv1.Columns("TotalCrAmt").Width = 100
        gv1.Columns("TotalCrAmt").HeaderText = "Cr Amt"

        ''richa UDL/23/04/19-000291 on 30 Apr,2019 SHOW CAPEX AND SUB CAPEX CODE WHEN BOTH SETTINGS ARE ON
        If ShowOptionforSelectingCapexForFA = True AndAlso ShowOptionforSelectingCapexForPO = True Then
            gv1.Columns("Capex_Code").IsVisible = True
            gv1.Columns("Capex_Code").Width = 200
            gv1.Columns("Capex_Code").HeaderText = "Capex Code"

            gv1.Columns("Capex Desc").IsVisible = True
            gv1.Columns("Capex Desc").Width = 100
            gv1.Columns("Capex Desc").HeaderText = "Capex Desc"

            gv1.Columns("Capex_SubCode").IsVisible = True
            gv1.Columns("Capex_SubCode").Width = 200
            gv1.Columns("Capex_SubCode").HeaderText = "Sub Capex Code"

            gv1.Columns("Sub Capex Desc").IsVisible = True
            gv1.Columns("Sub Capex Desc").Width = 100
            gv1.Columns("Sub Capex Desc").HeaderText = "Sub Capex Desc"
        End If

        If gv1.Columns.Contains("AP_Document_No") Then
            gv1.Columns("AP_Document_No").IsVisible = True
            gv1.Columns("AP_Document_No").Width = 200
            gv1.Columns("AP_Document_No").HeaderText = "AP Document No"

            gv1.Columns("AP_Document_Date").IsVisible = True
            gv1.Columns("AP_Document_Date").Width = 200
            gv1.Columns("AP_Document_Date").HeaderText = "AP Document Date"

            gv1.Columns("Descs").IsVisible = True
            gv1.Columns("Descs").Width = 200
            gv1.Columns("Descs").HeaderText = "AP Description"
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim total As New GridViewSummaryItem("TotalDrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(total)
        total = New GridViewSummaryItem("TotalCrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(total)



        If isShowSubLedger Then
            gv1.Columns("SubledgerDrAmt").IsVisible = True
            gv1.Columns("SubledgerDrAmt").Width = 100
            gv1.Columns("SubledgerDrAmt").HeaderText = "Sub Ledger Debit Amount"

            gv1.Columns("SubledgerCrAmt").IsVisible = True
            gv1.Columns("SubledgerCrAmt").Width = 100
            gv1.Columns("SubledgerCrAmt").HeaderText = "Sub Ledger Credit Amount"

            Dim item3 As New GridViewSummaryItem("SubledgerDrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)

            Dim item4 As New GridViewSummaryItem("SubledgerCrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
        End If

        ReStoreGridSoucreDocNo()
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
        EnableDisableControls(False)
    End Sub

    Sub SetGridFormation()
        For jj As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(jj).ReadOnly = True
            gv1.Columns(jj).IsVisible = False
        Next




        If gv1.Columns.Contains("AP_Document_No") Then
            gv1.Columns("AP_Document_No").IsVisible = True
            gv1.Columns("AP_Document_No").Width = 200
            gv1.Columns("AP_Document_No").HeaderText = "AP Document No"

            gv1.Columns("AP_Document_Date").IsVisible = True
            gv1.Columns("AP_Document_Date").Width = 200
            gv1.Columns("AP_Document_Date").HeaderText = "AP Document Date"

            gv1.Columns("Descs").IsVisible = True
            gv1.Columns("Descs").Width = 200
            gv1.Columns("Descs").HeaderText = "AP Description"
        End If


        If chkCusVendWiseSummary.Checked Then
            gv1.Columns("Account_code").IsVisible = True
            gv1.Columns("Account_code").Width = 100
            gv1.Columns("Account_code").HeaderText = "Voucher No"

            gv1.Columns("Account_Desc").IsVisible = True
            gv1.Columns("Account_Desc").Width = 100
            gv1.Columns("Account_Desc").HeaderText = "Voucher Date"

            gv1.Columns("Account_Group_Code").IsVisible = False
            gv1.Columns("Account_Group_Code").Width = 200

            gv1.Columns("Group_Desc").IsVisible = True
            gv1.Columns("Group_Desc").Width = 200

            gv1.Columns("Vendor Code").IsVisible = True
            gv1.Columns("Vendor Code").Width = 200

            gv1.Columns("Vendor").IsVisible = True
            gv1.Columns("Vendor").Width = 200

            gv1.Columns("Customer Code").IsVisible = True
            gv1.Columns("Customer Code").Width = 200

            gv1.Columns("Customer").IsVisible = True
            gv1.Columns("Customer").Width = 200

            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").Width = 100
            gv1.Columns("DrAmt").HeaderText = "Dr Amt"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").Width = 100
            gv1.Columns("CrAmt").HeaderText = "Cr Amt"

            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Account_Code as Account_Code  format ""{0}: {1}"" group by Account_Code"))

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim total As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            total = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "None") = CompairStringResult.Equal Then

            gv1.Columns("Account_code").IsVisible = True
            gv1.Columns("Account_code").Width = 100
            gv1.Columns("Account_code").HeaderText = "Account Code"

            gv1.Columns("Account_Desc").IsVisible = True
            gv1.Columns("Account_Desc").Width = 200
            gv1.Columns("Account_Desc").HeaderText = "Account Description"

            'gv1.Columns("Account_Group_Desc").IsVisible = True
            'gv1.Columns("Account_Group_Desc").Width = 200
            'gv1.Columns("Account_Group_Desc").HeaderText = "Account Group"

            gv1.Columns("Group_Desc").IsVisible = True
            gv1.Columns("Group_Desc").Width = 200
            gv1.Columns("Group_Desc").HeaderText = "Account Group"

            gv1.Columns("Voucher_No").IsVisible = True
            gv1.Columns("Voucher_No").Width = 100
            gv1.Columns("Voucher_No").HeaderText = "Voucher No"

            gv1.Columns("Voucher_Date").IsVisible = True
            gv1.Columns("Voucher_Date").Width = 100
            gv1.Columns("Voucher_Date").HeaderText = "Voucher Date"

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").Width = 200

            gv1.Columns("Remarks").IsVisible = True
            gv1.Columns("Remarks").Width = 200

            gv1.Columns("Comments").IsVisible = True
            gv1.Columns("Comments").Width = 200

            gv1.Columns("Vendor Code").IsVisible = True
            gv1.Columns("Vendor Code").Width = 200

            gv1.Columns("Vendor").IsVisible = True
            gv1.Columns("Vendor").Width = 200

            gv1.Columns("Customer Code").IsVisible = True
            gv1.Columns("Customer Code").Width = 200

            gv1.Columns("Customer").IsVisible = True
            gv1.Columns("Customer").Width = 200

            gv1.Columns("Narration").IsVisible = True
            gv1.Columns("Narration").Width = 200
            gv1.Columns("Narration").HeaderText = "Narration"

            gv1.Columns("Source_Code").IsVisible = True
            gv1.Columns("Source_Code").Width = 100
            gv1.Columns("Source_Code").HeaderText = "Source Code"

            gv1.Columns("Source_Desc").IsVisible = True
            gv1.Columns("Source_Desc").Width = 200
            gv1.Columns("Source_Desc").HeaderText = "Source Description"

            gv1.Columns("Source_doc_No").IsVisible = True
            gv1.Columns("Source_doc_No").Width = 100
            gv1.Columns("Source_doc_No").HeaderText = "Source Doc No"

            gv1.Columns("Description1").IsVisible = True
            gv1.Columns("Description1").Width = 200
            gv1.Columns("Description1").HeaderText = "Detail Description"


            gv1.Columns("VSP_CODE").IsVisible = True
            gv1.Columns("VSP_CODE").Width = 100
            gv1.Columns("VSP_CODE").HeaderText = "VSP Code"

            gv1.Columns("TotalDrAmt").IsVisible = True
            gv1.Columns("TotalDrAmt").Width = 100
            gv1.Columns("TotalDrAmt").HeaderText = "Dr Amt"

            gv1.Columns("TotalCrAmt").IsVisible = True
            gv1.Columns("TotalCrAmt").Width = 100
            gv1.Columns("TotalCrAmt").HeaderText = "Cr Amt"

            gv1.Columns("FinalAmount").IsVisible = True
            gv1.Columns("FinalAmount").Width = 100
            gv1.Columns("FinalAmount").HeaderText = "Closing"

            'KUNAL > TICKET : BM00000007570 > DATE : 19-OCT-2016
            gv1.Columns("Vendor_Invoice_No ").IsVisible = True
            gv1.Columns("Vendor_Invoice_No ").Width = 100
            gv1.Columns("Vendor_Invoice_No ").HeaderText = "Vendor_Invoice_No"

            'KUNAL > TICKET : BM00000007570 > DATE : 19-OCT-2016
            gv1.Columns("Vendor_Invoice_Date ").IsVisible = True
            gv1.Columns("Vendor_Invoice_Date ").Width = 100
            gv1.Columns("Vendor_Invoice_Date ").HeaderText = "Vendor_Invoice_Date"


            gv1.Columns("Invoices_Description").IsVisible = True
            gv1.Columns("Invoices_Description").Width = 100
            gv1.Columns("Invoices_Description").HeaderText = "Descriptions "

            gv1.Columns("Customer Invoice No").IsVisible = True
            gv1.Columns("Customer Invoice No").Width = 100
            gv1.Columns("Customer Invoice No").HeaderText = "Customer Invoice No"


            gv1.Columns("MCC Sale Desc").IsVisible = True
            gv1.Columns("MCC Sale Desc").Width = 100
            gv1.Columns("MCC Sale Desc").HeaderText = "MCC Sale Description"


            gv1.Columns("ReferenceNo").IsVisible = True
            gv1.Columns("ReferenceNo").Width = 100
            gv1.Columns("ReferenceNo").HeaderText = "ReferenceNo"

            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Account_Code as Account_Code  format ""{0}: {1}"" group by Account_Code"))

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim total As New GridViewSummaryItem("TotalDrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            total = New GridViewSummaryItem("TotalCrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            total = New GridViewSummaryItem("FinalAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Account No") = CompairStringResult.Equal Then

            gv1.Columns("Account_code").IsVisible = True
            gv1.Columns("Account_code").Width = 100
            gv1.Columns("Account_code").HeaderText = "Account Code"

            gv1.Columns("Account_Desc").IsVisible = True
            gv1.Columns("Account_Desc").Width = 200
            gv1.Columns("Account_Desc").HeaderText = "Account Description"

            'gv1.Columns("Account_Group_Desc").IsVisible = True
            'gv1.Columns("Account_Group_Desc").Width = 200
            'gv1.Columns("Account_Group_Desc").HeaderText = "Account Group"

            gv1.Columns("Group_Desc").IsVisible = True
            gv1.Columns("Group_Desc").Width = 200
            gv1.Columns("Group_Desc").HeaderText = "Account Group"

            gv1.Columns("Opening").IsVisible = Not chkWithoutOpening.Checked
            gv1.Columns("Opening").Width = 100

            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").Width = 100
            gv1.Columns("DrAmt").HeaderText = "Dr Amt"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").Width = 100
            gv1.Columns("CrAmt").HeaderText = "Cr Amt"

            gv1.Columns("Closing").IsVisible = True
            gv1.Columns("Closing").Width = 100

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim total As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            total = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            total = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            total = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(total)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        End If

        If clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal Then
            ReStoreGridSoucreDocNo()
        ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Account No") = CompairStringResult.Equal Then
            ReStoreGridAccountNo()
        Else
            ReStoreGridDetail()
        End If

        gv1.MasterTemplate.AutoExpandGroups = True
        RadPageView1.SelectedPage = RadPageViewPage2
        EnableDisableControls(False)
    End Sub

    Private Sub gv1_ContextMenuOpening(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ContextMenuOpeningEventArgs)

        contextMenu1 = New RadContextMenu
        If gv1.CurrentRow.Index >= 0 Then
            If clsCommon.myLen(gv1.CurrentRow.Cells("Voucher_No").Value) > 0 Then
                contextMenu1.Items.Add(menuItem1)
            End If
        End If
        e.ContextMenu = contextMenu1.DropDown
    End Sub

    Private Sub OpenfrmGLSummary()
        If gv1.CurrentRow.Cells("Voucher_No").Value <> "" Then
            Dim frm As New FrmGLSummary
            frm.strVoucherNo = gv1.CurrentRow.Cells("Voucher_No").Value
            frm.Show()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.CellDoubleClick

        'If chkCusVendWiseSummary.Checked AndAlso gv1.CurrentRow IsNot Nothing Then
        '    Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        '    frm.strPrevFormACode = clsCommon.myCstr(gv1.CurrentRow.Cells("Account_code").Value)
        '    frm.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
        '    'frm.strPrevFormAName = clsCommon.myCstr(gv1.CurrentRow.Cells("AccName").Value)
        '    frm.dTPrevFormFromDate = txtFromDate.Value
        '    frm.dTPrevFormToDate = txtToDate.Value
        '    frm.RadLabel7.Visible = True
        '    frm.txtFromDate.Visible = True
        '    frm.MyLabel2.Text = "To Date"
        '    frm.arrLocSeg = New ArrayList()
        '    If txtLocationSeg.arrValueMember IsNot Nothing AndAlso txtLocationSeg.arrValueMember.Count > 0 Then
        '        frm.arrLocSeg = txtLocationSeg.arrValueMember 'cbgLocSeg.CheckedValue
        '    End If
        '    frm.arrAcc = New ArrayList()
        '    frm.arrAcc.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account_code").Value))

        '    frm.arrvehicle = New ArrayList()
        '    If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
        '        frm.arrvehicle = txtVehicle.arrValueMember
        '    End If
        '    frm.arrDept = New ArrayList()
        '    If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
        '        frm.arrDept = txtDepartment.arrValueMember
        '    End If
        '    frm.arrEmp = New ArrayList
        '    If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
        '        frm.arrEmp = txtEmployee.arrValueMember
        '    End If
        '    frm.arrMachine = New ArrayList
        '    If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
        '        frm.arrMachine = txtMachine.arrValueMember
        '    End If
        '    frm.arrVisi = New ArrayList
        '    If txtVisiPMX.arrValueMember IsNot Nothing AndAlso txtVisiPMX.arrValueMember.Count > 0 Then
        '        frm.arrVisi = txtVisiPMX.arrValueMember
        '    End If
        '    frm.IsVendorCustomerWiseSummary = False
        '    If clsCommon.myLen(gv1.CurrentRow.Cells("Vendor Code").Value) > 0 Then
        '        frm.strPreVendorCode = clsCommon.myCstr(gv1.CurrentRow.Cells("Vendor Code").Value)
        '    End If
        '    If clsCommon.myLen(gv1.CurrentRow.Cells("Customer Code").Value) > 0 Then
        '        frm.strPreCustomerCode = clsCommon.myCstr(gv1.CurrentRow.Cells("Customer Code").Value)
        '    End If
        '    frm.MdiParent = MDI
        '    frm.Show()
        'ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "None") = CompairStringResult.Equal AndAlso gv1.CurrentRow IsNot Nothing Then
        '    If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells("Voucher_No").Value)) > 0 Then
        '        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, clsCommon.myCstr(gv1.CurrentRow.Cells("Voucher_No").Value))
        '    End If
        'End If
    End Sub

    Private Sub GLTransReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            funreset()

        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            PrintData()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then
            If pnlAdminSetting.Visible Then
                pnlAdminSetting.Visible = False
            Else
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    pnlAdminSetting.Visible = True
                End If
            End If
        End If

    End Sub

    Private Sub ddlGroupingType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlGroupingType.SelectedIndexChanged
        If Not clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "None") = CompairStringResult.Equal Then
            btnPrint.Enabled = False
        Else
            btnPrint.Enabled = True
        End If
    End Sub

    Private Sub chkSettlementSheetClearance_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSettlementSheetClearance.ToggleStateChanged
        'If chkSettlementSheetClearance.Checked Then
        '    chkAccSelect.IsChecked = chkSettlementSheetClearance.Checked
        '    Dim Arr As ArrayList = New ArrayList()
        '    Arr.Add("370051")
        '    Arr.Add("370051-BUR")
        '    Arr.Add("370051-LWR")
        '    Arr.Add("370051-MUN")
        '    Arr.Add("370051-NDA")
        '    Arr.Add("370051-SUL")
        '    cbgAccount.CheckedValue = Arr

        'End If
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Dim dsEmployment As DataSet = New DataSet
        Dim dtEmployees As DataTable = New DataTable("Employees")

        Dim dcEmployees(5)
        dcEmployees(0) = New DataColumn("EmployeeID", System.Type.GetType("System.Int32"))
        dtEmployees.Columns.Add(dcEmployees(0))
        dcEmployees(1) = New DataColumn("FirstName", System.Type.GetType("System.String"))
        dtEmployees.Columns.Add(dcEmployees(1))
        dcEmployees(2) = New DataColumn("LastName", System.Type.GetType("System.String"))
        dtEmployees.Columns.Add(dcEmployees(2))
        dcEmployees(3) = New DataColumn("Department", System.Type.GetType("System.String"))
        dtEmployees.Columns.Add(dcEmployees(3))
        dcEmployees(4) = New DataColumn("EmplStatusID", System.Type.GetType("System.Int32"))
        dtEmployees.Columns.Add(dcEmployees(4))

        Dim drEmplRecord As DataRow = dtEmployees.NewRow()
        drEmplRecord("EmployeeID") = "1"
        drEmplRecord("FirstName") = "Peter"
        drEmplRecord("LastName") = "Larsen"
        drEmplRecord("Department") = "Accounting"
        drEmplRecord("EmplStatusID") = "1"
        dtEmployees.Rows.Add(drEmplRecord)

        drEmplRecord = dtEmployees.NewRow()
        drEmplRecord("EmployeeID") = "2"
        drEmplRecord("FirstName") = "Paul"
        drEmplRecord("LastName") = "Banack"
        drEmplRecord("Department") = "IT/IM"
        drEmplRecord("EmplStatusID") = "3"
        dtEmployees.Rows.Add(drEmplRecord)

        drEmplRecord = dtEmployees.NewRow()
        drEmplRecord("EmployeeID") = "3"
        drEmplRecord("FirstName") = "Helene"
        drEmplRecord("LastName") = "Cassavoy"
        drEmplRecord("Department") = "Accounting"
        drEmplRecord("EmplStatusID") = "2"
        dtEmployees.Rows.Add(drEmplRecord)

        drEmplRecord = dtEmployees.NewRow()
        drEmplRecord("EmployeeID") = "4"
        drEmplRecord("FirstName") = "Anselme"
        drEmplRecord("LastName") = "Thomas"
        drEmplRecord("Department") = "Public Relations"
        drEmplRecord("EmplStatusID") = "1"
        dtEmployees.Rows.Add(drEmplRecord)

        drEmplRecord = dtEmployees.NewRow()
        drEmplRecord("EmployeeID") = "5"
        drEmplRecord("FirstName") = "Bertha"
        drEmplRecord("LastName") = "Um"
        drEmplRecord("Department") = "Corporate"
        drEmplRecord("EmplStatusID") = "4"
        dtEmployees.Rows.Add(drEmplRecord)

        drEmplRecord = dtEmployees.NewRow()
        drEmplRecord("EmployeeID") = "6"
        drEmplRecord("FirstName") = "Renée"
        drEmplRecord("LastName") = "Bright"
        drEmplRecord("Department") = "IT/IM"
        drEmplRecord("EmplStatusID") = "3"
        dtEmployees.Rows.Add(drEmplRecord)

        drEmplRecord = dtEmployees.NewRow()
        drEmplRecord("EmployeeID") = "7"
        drEmplRecord("FirstName") = "Jeanne"
        drEmplRecord("LastName") = "Tristan"
        drEmplRecord("Department") = "Corporate"
        drEmplRecord("EmplStatusID") = "1"
        dtEmployees.Rows.Add(drEmplRecord)

        drEmplRecord = dtEmployees.NewRow()
        drEmplRecord("EmployeeID") = "8"
        drEmplRecord("FirstName") = "Sandrine"
        drEmplRecord("LastName") = "Holland"
        drEmplRecord("Department") = "Public Relations"
        drEmplRecord("EmplStatusID") = "4"
        dtEmployees.Rows.Add(drEmplRecord)

        Dim dtEmplStatus As DataTable = New DataTable("EmploymentStatus")

        Dim dcEmployment(2)
        dcEmployment(0) = New DataColumn("EmplStatusID", System.Type.GetType("System.Int32"))
        dtEmplStatus.Columns.Add(dcEmployment(0))
        dcEmployment(1) = New DataColumn("EmplStatus", System.Type.GetType("System.String"))
        dtEmplStatus.Columns.Add(dcEmployment(1))

        Dim drEmployment As DataRow = dtEmplStatus.NewRow()
        drEmployment("EmplStatusID") = "1"
        drEmployment("EmplStatus") = "Full Time"
        dtEmplStatus.Rows.Add(drEmployment)

        drEmployment = dtEmplStatus.NewRow()
        drEmployment("EmplStatusID") = "2"
        drEmployment("EmplStatus") = "Part Time"
        dtEmplStatus.Rows.Add(drEmployment)

        drEmployment = dtEmplStatus.NewRow()
        drEmployment("EmplStatusID") = "3"
        drEmployment("EmplStatus") = "Contractor"
        dtEmplStatus.Rows.Add(drEmployment)

        drEmployment = dtEmplStatus.NewRow()
        drEmployment("EmplStatusID") = "4"
        drEmployment("EmplStatus") = "Intern"
        dtEmplStatus.Rows.Add(drEmployment)

        'dtEmployees.Merge(dtEmplStatus, False, MissingSchemaAction.AddWithKey)


        dsEmployment.Tables.Add(dtEmployees)
        dsEmployment.Tables.Add(dtEmplStatus)
        dsEmployment.Relations.Add("TheRelation", dtEmplStatus.Columns("EmplStatusID"), dtEmployees.Columns("EmplStatusID"))
        dtEmployees.Columns.Add("EmplStatus", GetType(System.String), "Parent.EmplStatus")

        '        DataSet ds=new DataSet();
        'ds.Tables.Add(Table1);
        'ds.Tables.Add(Table2);
        'ds.Relations.Add("TheRelation", Table1.Columns["a"], Table2.Columns["a"]);
        '        Table2.Columns.Add("b", GetType(System.String), "Parent.b")


        'Dim colParent As DataColumn = dsEmployment.Tables("EmploymentStatus").Columns("EmplStatusID")
        'Dim colChild As DataColumn = dsEmployment.Tables("Employees").Columns("EmplStatusID")
        'Dim drEmployeeStatus As DataRelation = New DataRelation("EmployeeStatus", colParent, colChild)

        'dsEmployment.Relations.Add(drEmployeeStatus)

        'Me.DataGrid1.DataSource = dsEmployment
        'Me.DataGrid1.DataMember = "EmploymentStatus"
    End Sub

    Private Sub rbtnVendor_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnVendor.ToggleStateChanged, rbtnNone.ToggleStateChanged, rbtnCustomer.ToggleStateChanged, rbtnBank.ToggleStateChanged, rbtnTPT.ToggleStateChanged, rbtnSaleAccount.ToggleStateChanged, rbtnHCessAccount.ToggleStateChanged, rbtnExciseAccount.ToggleStateChanged, rbtnECessAccount.ToggleStateChanged, rbtnSaleRecoChart.ToggleStateChanged, rbtnPurchaseBook.ToggleStateChanged, rbtnOtherGoods.ToggleStateChanged, rbtnFinishGoods.ToggleStateChanged, rbtnVAT.ToggleStateChanged, rbtnCST.ToggleStateChanged
        'LoadAccounts()
        'If rbtnVendor.IsChecked Then
        '    txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.Vendor, clsRecoSettingReportComponent.VendorAccount)
        'ElseIf rbtnCustomer.IsChecked Then
        '    txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.Customer, clsRecoSettingReportComponent.CustomerAccount)
        'ElseIf rbtnBank.IsChecked Then
        '    txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.Bank, clsRecoSettingReportComponent.BankAccount)
        'ElseIf rbtnSaleRecoChart.IsChecked Then
        '    If rbtnSaleAccount.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.SaleRecoChart, clsRecoSettingReportComponent.SaleRecoChartSaleAccount)
        '    ElseIf rbtnExciseAccount.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.SaleRecoChart, clsRecoSettingReportComponent.SaleRecoChartExciseAmount)
        '    ElseIf rbtnECessAccount.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.SaleRecoChart, clsRecoSettingReportComponent.SaleRecoChartECess)
        '    ElseIf rbtnHCessAccount.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.SaleRecoChart, clsRecoSettingReportComponent.SaleRecoChartHCess)
        '    ElseIf rbtnTPT.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.SaleRecoChart, clsRecoSettingReportComponent.SaleRecoChartTPT)
        '    End If
        'ElseIf rbtnPurchaseBook.IsChecked Then
        '    If rbtnFinishGoods.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.PurchaseBook, clsRecoSettingReportComponent.PurchaseBookFAAccountFG)
        '    ElseIf rbtnOtherGoods.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.PurchaseBook, clsRecoSettingReportComponent.PurchaseBookFAAccountOG)
        '    End If
        'ElseIf rbtnSaleRegister.IsChecked Then
        '    If rbtnVAT.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.SaleRegister, clsRecoSettingReportComponent.SaleRegisterVat)
        '    ElseIf rbtnCST.IsChecked Then
        '        txtAccount.arrValueMember = clsReconciliationSetting.GetData(clsRecoSettingReportName.SaleRegister, clsRecoSettingReportComponent.SaleRegisterCST)
        '    End If
        'Else

        'End If

        'Panel4.Visible = rbtnSaleRecoChart.IsChecked
        'pnlPurchaseBook.Visible = rbtnPurchaseBook.IsChecked
        'pnlSaleRegister.Visible = rbtnSaleRegister.IsChecked
    End Sub

    Private Sub txtLocationSeg__My_Click(sender As Object, e As EventArgs) Handles txtLocationSeg._My_Click
        LoadLocatinSegment()
    End Sub

    Private Sub txtAccount__My_Click(sender As Object, e As EventArgs) Handles txtAccount._My_Click
        LoadAccounts()
    End Sub

    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click
        LoadVehicles()
    End Sub

    Private Sub txtMachine__My_Click(sender As Object, e As EventArgs) Handles txtMachine._My_Click
        Loadmachines()
    End Sub

    Private Sub txtDepartment__My_Click(sender As Object, e As EventArgs) Handles txtDepartment._My_Click
        LoadDepartments()
    End Sub

    Private Sub txtEmployee__My_Click(sender As Object, e As EventArgs) Handles txtEmployee._My_Click
        LoadEmployees()
    End Sub

    Private Sub txtVisiPMX__My_Click(sender As Object, e As EventArgs) Handles txtVisiPMX._My_Click
        LoadVisi()
    End Sub

    Private Sub txtSourceCode_My_Click(sender As Object, e As EventArgs) Handles txtSourceCode._My_Click
        LoadSourceCode()
    End Sub

    Private Sub txtAccountGroup__My_Click(sender As Object, e As EventArgs) Handles txtAccountGroup._My_Click
        LoadAccountGroup()
    End Sub

    Private Sub MyLabel1_Click(sender As Object, e As EventArgs) Handles MyLabel1.Click

    End Sub

    Private Sub txtACGroup__My_Click(sender As Object, e As EventArgs) Handles txtACGroup._My_Click
        LoadAccountGroup()
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmGLTransReport & "'"))
            If txtLocationSeg.arrDispalyMember IsNot Nothing AndAlso txtLocationSeg.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location: " + clsCommon.GetMulcallStringWithComma(txtLocationSeg.arrDispalyMember))
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
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        LoadDataNew(False, 1)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        LoadDataNew(False, 2)
    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If e.RowIndex >= 0 Then
            If chkCusVendWiseSummary.Checked AndAlso gv1.CurrentRow IsNot Nothing Then
                Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                frm.strPrevFormACode = clsCommon.myCstr(gv1.CurrentRow.Cells("Account_code").Value)
                frm.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
                'frm.strPrevFormAName = clsCommon.myCstr(gv1.CurrentRow.Cells("AccName").Value)
                frm.dTPrevFormFromDate = txtFromDate.Value
                frm.dTPrevFormToDate = txtToDate.Value
                frm.RadLabel7.Visible = True
                frm.txtFromDate.Visible = True
                frm.MyLabel2.Text = "To Date"
                frm.arrLocSeg = New ArrayList()
                If txtLocationSeg.arrValueMember IsNot Nothing AndAlso txtLocationSeg.arrValueMember.Count > 0 Then
                    frm.arrLocSeg = txtLocationSeg.arrValueMember 'cbgLocSeg.CheckedValue
                End If
                frm.arrAcc = New ArrayList()
                frm.arrAcc.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account_code").Value))

                frm.arrvehicle = New ArrayList()
                If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                    frm.arrvehicle = txtVehicle.arrValueMember
                End If
                frm.arrDept = New ArrayList()
                If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                    frm.arrDept = txtDepartment.arrValueMember
                End If
                frm.arrEmp = New ArrayList
                If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                    frm.arrEmp = txtEmployee.arrValueMember
                End If
                frm.arrMachine = New ArrayList
                If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
                    frm.arrMachine = txtMachine.arrValueMember
                End If
                frm.arrVisi = New ArrayList
                If txtVisiPMX.arrValueMember IsNot Nothing AndAlso txtVisiPMX.arrValueMember.Count > 0 Then
                    frm.arrVisi = txtVisiPMX.arrValueMember
                End If
                frm.IsVendorCustomerWiseSummary = False
                If clsCommon.myLen(gv1.CurrentRow.Cells("Vendor Code").Value) > 0 Then
                    frm.strPreVendorCode = clsCommon.myCstr(gv1.CurrentRow.Cells("Vendor Code").Value)
                End If
                If clsCommon.myLen(gv1.CurrentRow.Cells("Customer Code").Value) > 0 Then
                    frm.strPreCustomerCode = clsCommon.myCstr(gv1.CurrentRow.Cells("Customer Code").Value)
                End If
                'frm.MdiParent = MDI
                frm.Show()
            ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "None") = CompairStringResult.Equal AndAlso gv1.CurrentRow IsNot Nothing Then
                If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells("Voucher_No").Value)) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, clsCommon.myCstr(gv1.CurrentRow.Cells("Voucher_No").Value))
                End If
            End If
        End If
    End Sub

    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        Try
            '', ByVal FileName As String, 

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            IO.File.WriteAllLines(filePath, transportSql.ExportCSV(sender, AddHeader))
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim obj As New clsGridLayout()
        If clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = clsUserMgtCode.frmGLTransReport + "SD"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Account No") = CompairStringResult.Equal Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = clsUserMgtCode.frmGLTransReport + "AN"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        Else
            gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = clsUserMgtCode.frmGLTransReport + "N"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(clsUserMgtCode.frmGLTransReport) > 0 Then
            If clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal Then
                clsGridLayout.DeleteData(clsUserMgtCode.frmGLTransReport & "SD", objCommonVar.CurrentUserCode)
                SetGridFormationForGroupingType()
                ReStoreGridSoucreDocNo()
            ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Account No") = CompairStringResult.Equal Then
                clsGridLayout.DeleteData(clsUserMgtCode.frmGLTransReport & "AN", objCommonVar.CurrentUserCode)
                SetGridFormation()
                ReStoreGridAccountNo()
            Else
                clsGridLayout.DeleteData(clsUserMgtCode.frmGLTransReport & "N", objCommonVar.CurrentUserCode)
                SetGridFormation()
                ReStoreGridDetail()
            End If

            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
        End If
    End Sub

    Private Sub ReStoreGridDetail()
        Try
            If clsCommon.myLen(clsUserMgtCode.frmGLTransReport) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(clsUserMgtCode.frmGLTransReport + "N", "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub ReStoreGridAccountNo()
        Try
            If clsCommon.myLen(clsUserMgtCode.frmGLTransReport) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(clsUserMgtCode.frmGLTransReport + "AN", "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub ReStoreGridSoucreDocNo()
        Try
            If clsCommon.myLen(clsUserMgtCode.frmGLTransReport) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(clsUserMgtCode.frmGLTransReport + "SD", "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiCSV_Click(sender As Object, e As EventArgs) Handles rmiCSV.Click
        Try
            If gv1 Is Nothing OrElse gv1.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(gv1, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub Export(ByVal IsPrint As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmGLTransReport & "'"))
                If txtLocationSeg.arrDispalyMember IsNot Nothing AndAlso txtLocationSeg.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Location: " + clsCommon.GetMulcallStringWithComma(txtLocationSeg.arrDispalyMember))
                End If

                If IsPrint = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("General Ledger", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Function Getreportid()
        Dim reportid As String = ""
        If clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Source Document No") = CompairStringResult.Equal Then
            reportid = clsUserMgtCode.frmGLTransReport + "SD"
        ElseIf clsCommon.CompairString(ddlGroupingType.SelectedItem.Text, "Account No") = CompairStringResult.Equal Then
            reportid = clsUserMgtCode.frmGLTransReport + "AN"
        Else
            reportid = clsUserMgtCode.frmGLTransReport + "N"
        End If
        Return reportid
    End Function
End Class

