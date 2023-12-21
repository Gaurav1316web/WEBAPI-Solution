
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class frmRptVendorLedgerVsAgeing
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim dtCustomer As DataTable = Nothing
    Dim dtOpening As DataTable = Nothing
    Dim AllowtoEmployeeSalaryIntegration As Boolean = False
    Dim dvTemp As DataView
    Dim FormType As String = Nothing
    Dim BaseQry As String = Nothing
    Dim BaseQryOpening As String = Nothing
    Dim strQry As String = Nothing
    Dim ReportID As String = String.Empty
    Dim strtempBaseQryforopening As String = String.Empty
    Dim strtempBaseQryforopeningForMIS As String = String.Empty
    Dim isRunDoubleClick As Boolean = False
    Dim StrPermission As String
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FormType = formid
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        btnQExport.Visible = MyBase.isExport ' MyBase.isQuickExportFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub frmRptVendorLedgerVsAgeing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCurrencyType()
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationSegment()
        gvVendor.Visible = True
        dtpFromdate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        dtptodate.Value = clsCommon.GETSERVERDATE()


        gvVendor.Dock = DockStyle.Fill
        ReportID = GetReportID()

        AllowtoEmployeeSalaryIntegration = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoEmployeeSalaryIntegration, clsFixedParameterCode.AllowtoEmployeeSalaryIntegration, Nothing)) = 1, True, False))

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset all Parameters")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        isRunDoubleClick = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoubleClickOnVC, clsFixedParameterCode.DoubleClickOnVC, Nothing)) = 1, True, False)
    End Sub



    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        RadGroupBox1.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
        gvVendor.DataSource = Nothing
        GC.Collect()
    End Sub

    Sub Print(Optional ByVal BulkExport As Integer = 0)
        Dim IsPDCCheque As String = String.Empty
        Dim CompanyAdd As String = String.Empty
        Dim Comp_Name As String = String.Empty
        Dim childvendrcode As String = String.Empty
        Dim AccountSet As String = String.Empty
        Dim qry As String = String.Empty
        Try
            Dim arr As New ArrayList
            arr.Add("TDS")
            arr.Add("I")
            arr.Add("C")
            arr.Add("D")
            arr.Add("AV")
            arr.Add("OA")
            arr.Add("PY")
            arr.Add("MI")
            arr.Add("RV")
            arr.Add("Vendor")
            arr.Add("RC")
            arr.Add("PAE")
            arr.Add("AD")


            Dim strFromDate As String = clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
            Dim runDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy hh:mm tt")
            Comp_Name = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count = 1 Then
                qry = "select top(1) Add1+' '+Add2+' '+Add3  from TSPL_LOCATION_MASTER  where Location_Type ='Physical' and Loc_Segment_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                CompanyAdd = clsDBFuncationality.getSingleValue(qry)
            Else
                qry = "select top(1) Add1+' '+Add2+' '+Add3  from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
                CompanyAdd = clsDBFuncationality.getSingleValue(qry)
            End If
            Dim strFIltervendor As String = String.Empty
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strFIltervendor += clsCommon.GetMulcallString(txtVendor.arrValueMember)
            End If

            BaseQry = clsVendorMaster.GetVendorLedgerBaseQry(True, False, chkAgainstSalaryAdvance.Checked, strFromDate, strToDate, strFIltervendor, False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), FormType, True, False, False, False)
            BaseQryOpening = clsVendorMaster.GetVendorLedgerBaseQry(True, False, chkAgainstSalaryAdvance.Checked, strFromDate, strToDate, strFIltervendor, True, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), FormType, True, False, False, False)

            Dim StrEmployeetypeCondition As String = String.Empty
            Dim StrEmployeeAdvancetypeCondition As String = String.Empty
            Dim StrEmployeeAdvancetypeConditionNotIncluded As String = String.Empty

            If chkAgainstSalaryAdvance.Checked = True Then
                StrEmployeeAdvancetypeCondition += "'S','AAS',"
            End If

            BaseQry += " where 1=1 "
            BaseQryOpening += " where 1=1 "

            If AllowtoEmployeeSalaryIntegration = True AndAlso chkAgainstSalaryAdvance.Checked = False Then
                StrEmployeeAdvancetypeConditionNotIncluded = " and InnQuery.Emp_Adv_Type not in ('S','AAS') "
                BaseQry += StrEmployeeAdvancetypeConditionNotIncluded
                BaseQryOpening += StrEmployeeAdvancetypeConditionNotIncluded
            End If

            If clsCommon.myLen(StrEmployeeAdvancetypeCondition) > 0 Then
                StrEmployeeAdvancetypeCondition = StrEmployeeAdvancetypeCondition.Substring(0, StrEmployeeAdvancetypeCondition.Length - 1)
                StrEmployeeAdvancetypeCondition = " and InnQuery.Emp_Adv_Type in (" & StrEmployeeAdvancetypeCondition & ") "
                BaseQry += StrEmployeeAdvancetypeCondition
                BaseQryOpening += StrEmployeeAdvancetypeCondition
            End If
            If clsCommon.myLen(StrEmployeeAdvancetypeCondition) > 0 And clsCommon.myLen(StrEmployeetypeCondition) > 0 Then
                StrEmployeetypeCondition = StrEmployeetypeCondition.Substring(0, StrEmployeetypeCondition.Length - 1)
                StrEmployeetypeCondition = " or InnQuery.Emp_type in (" & StrEmployeetypeCondition & ") "
                BaseQry += StrEmployeetypeCondition
                BaseQryOpening += StrEmployeetypeCondition
            ElseIf clsCommon.myLen(StrEmployeetypeCondition) > 0 Then
                StrEmployeetypeCondition = StrEmployeetypeCondition.Substring(0, StrEmployeetypeCondition.Length - 1)
                StrEmployeetypeCondition = " and InnQuery.Emp_type in (" & StrEmployeetypeCondition & ") "
                BaseQry += StrEmployeetypeCondition
                BaseQryOpening += StrEmployeetypeCondition
            End If



            Dim strFIlterCheck As String = String.Empty
            Dim strCheckForSumm As String = String.Empty
            Dim strVenFilterPDC As String = String.Empty
            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_VENDOR_MASTER.Vendor_Group_Code in (" + clsCommon.GetMulcallString(txtVendorGroup.arrValueMember) + ")  "
                strVenFilterPDC += " and TSPL_VENDOR_MASTER.Vendor_Group_Code IN (" + clsCommon.GetMulcallString(txtVendorGroup.arrValueMember) + ")  "
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strFIlterCheck += " and (TSPL_VENDOR_MASTER.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"

                strFIlterCheck += ")"
                strVenFilterPDC += " and TSPL_PAYMENT_HEADER.Vendor_Code IN (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strFIlterCheck += " and RIGHT(final.account,3) in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                strVenFilterPDC += " and right(TSPL_BANK_MASTER.BANKACC,3) IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + " )"
            Else
                strFIlterCheck += " and RIGHT(final.account,3) in (" + StrPermission + ")  "
                strVenFilterPDC += " and right(TSPL_BANK_MASTER.BANKACC,3) IN (" + StrPermission + " )"
            End If

            '-----------------------------------------------------


            Dim strLedgerQry As String = String.Empty
            '---------------Vendor Wise Data-----------------
            strLedgerQry = "Select MAX(Vendor_Group_Code) as Vendor_Group_Code, MAX(Vendor_Group_Desc) as Vendor_Group_Desc, VCode, MAX(VName) as VName,  SUM(CONVERT(DECIMAL(18,2),OpngBal)) as OpngBal,  SUM(CONVERT(DECIMAL(18,2),DrAmt)) as DrAmt, SUM( CONVERT(DECIMAL(18,2),CrAmt)) as CrAmt,    SUM(CONVERT(DECIMAL(18,2),OpngBal))+  SUM(CONVERT(DECIMAL(18,2),CrAmt))- SUM(CONVERT(DECIMAL(18,2),DrAmt)) as LedgerAmount,0 as AgeingAmt from ("

            strLedgerQry += " Select VCode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code) as Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, SUM(CrAmt)-SUM(DrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote From ( " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, strtempBaseQryforopeningForMIS, BaseQryOpening) & " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  <'" + strFromDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY VCode" + Environment.NewLine &
               " UNION ALL" + Environment.NewLine &
               " Select VCode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code) as Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, 0 as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(CONVERT(DECIMAL(18,2),Purchase*" & ddlCurrencyType.SelectedValue & ")) as Purchase, SUM(CONVERT(DECIMAL(18,2),Payments*" & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, "1", ddlCurrencyType.SelectedValue) & ")) as Payments, SUM(DrNote*" & ddlCurrencyType.SelectedValue & ") as DrNote, SUM(CrNote*" & ddlCurrencyType.SelectedValue & ") as CrNote from ( " + BaseQry + " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY VCode" + Environment.NewLine &
               " ) XXX GROUP BY VCode "

            '' vendor ageing 

            '------------------------------------------------------------
            Dim arr1 As New ArrayList
            arr1.Add("I")
            arr1.Add("C")
            arr1.Add("D")
            arr1.Add("AV")
            arr1.Add("OA")
            arr1.Add("P")
            arr1.Add("RC")
            arr1.Add("Adjustment")


            Dim strInnerQry As String = clsVendorMaster.GetOutStandingQry(dtptodate.Value.Date, clsCommon.GetPrintDate(dtptodate.Value.Date, "dd/MMM/yyyy"), arr1, ddlCurrencyType.SelectedValue, "DocumentDate", IIf(txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0, txtVendor.arrValueMember, Nothing), IIf(txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0, txtLocation.arrValueMember, Nothing), IIf(txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0, txtVendorGroup.arrValueMember, Nothing))

            Dim strAgeingQry As String = "   select max(Vendor_Group_Code) as Vendor_Group_Code,max (Vendor_Group_Desc) as Vendor_Group_Desc,[Vendor_code] ,max([Vendor Name] ) as [Vendor Name],0 as Opng, 0 as cr,0 as dr,0 as LedgerAmt,sum([Due Amount])+sum([Current]) as AgeingAmt from (select Vendor_Group_Code as Vendor_Group_Code,Cust_Group_Desc as Vendor_Group_Desc,[Customer Id] as [Vendor_code] ,[Customer Name]  as [Vendor Name],0 as Opng, 0 as cr,0 as dr,0 as LedderAmt,[Due Amount],case when [Document_Type] in ('DR','OA','AV') then -1 else 1 end * [Current] as [Current],[Document_Type] from 
  (select Vendor_Code,DocDate,docPosted,REFDOCNO,RefDocType,'" & strToDate & "' as RunDate, '" & strToDate & "' as AgeOfDate, '" & strToDate & "' as CutOfDate, '' as rptHeading, '0' AS First_Period, '15' AS Second_Period, '30' AS [Third Period], '45' AS [Fourth Period], '60' AS [Fifth Period],
 '' AS [Sixth Period], '' AS [Seventh Period],'' AS [Eight Period], '' AS [Nineth Period], '60' AS [Over], 
 YYY.Vendor_Group_Code , YYY.Vendor_Group_Code_Desc as Cust_Group_Desc, YYY.Vendor_Code as [Customer Id], '' as [Parent Code], YYY.Vendor_Name AS [Customer Name], 
 YYY.DocNo as [Document Id], '' as [Desc], Currency, ConvRate, case when ( DATEDIFF (day,convert(date, DocDate,101),'" & strToDate & "') +1)>0 then Case when Document_Type IN ('I','C','RC') then  convert(decimal(18,2),  [Document_Total]) else 0 End else 0 end as [Due Amount], case when Document_Type IN ('I','C','RC') then convert(varchar,Due_Date,103) else convert(varchar, YYY.DocDate,103) end as [Due Date],convert(varchar, YYY.DocDate,103) as [Document Date],  case when Document_Type NOT IN ('I','C','RC') then  convert(decimal(18,2),isnull([Document_Total],0)) else 0 End  as [Current], 
 case when Document_Type IN ('I','C','RC') then  DATEDIFF (day,convert(date, DocDate,101),'" & strToDate & "')+1  else 0 end  as [Ageing_Days],  case when Document_Type='I' then 'IN' when Document_Type='PY' then 'PY' when Document_Type='D' then 'DR' when Document_Type='C' then 'CR' when Document_Type='AV' then 'AV' when Document_Type='OA' then 'OA'when Document_Type='PY' then 'PY' when Document_Type='RC' then 'RC' end as [Document_Type], 
 '' AS From_Vendor, '' AS To_Vendor,  'Aged Payble by Document Date' AS Report_Type, 'SMry' as [Summary], 'N' as [IsFifoBased]  ,Location ,Terms_Code FROM (
   " & strInnerQry & ") YYY WHERE Convert(Date, DocDate, 103) <=Convert(Date,'" & strToDate & "', 103) and docPosted =1 and isnull(refdocno,'') not in (Select document_no from TSPL_REVALUATION_detail where isnull(ap_invoice_no,'')<>'') and YYY.DocNo not in (Select tspl_vendor_invoice_head.Document_No from tspl_vendor_invoice_head inner join tspl_pr_head on tspl_pr_head.pr_no=tspl_vendor_invoice_head.Against_PurchaseReturn_No where   isnull(tspl_pr_head.Against_PI,'')<>'' and tspl_vendor_invoice_head.Vendor_Code =YYY.Vendor_Code  and tspl_vendor_invoice_head.document_type='D' UNION ALL  Select tspl_vendor_invoice_head.Document_No from tspl_vendor_invoice_head where   isnull(tspl_vendor_invoice_head.Against_POInvoice_No ,'')<>'' and tspl_vendor_invoice_head.Vendor_Code =YYY.Vendor_Code  and tspl_vendor_invoice_head.document_type='D' ) )xx
  )XXX group by  [Vendor_code]"


            Dim strFinalQry As String = "Select * from (select MAX(Vendor_Group_Code) AS Vendor_Group_Code,  VCode,  MAX(VName) as VName, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt,sum(LedgerAmount) as  LedgerAmt,sum(AgeingAmt) as AgeingAmt,sum(LedgerAmount) -sum(AgeingAmt) as Diff from (  " &
                        " " & strLedgerQry & "" & Environment.NewLine &
                        " Union " & Environment.NewLine &
                    " " & strAgeingQry & "" & Environment.NewLine &
                    ") final group by VCode) Qry "
            If chkShowMismatcheddata.Checked Then
                strFinalQry += " Where Qry.Diff <>0"
            End If

            dtCustomer = clsDBFuncationality.GetDataTable(strFinalQry)

            If dtCustomer.Rows.Count <= 0 Then

                clsCommon.MyMessageBoxShow(Me, "Data not found", Me.Text)
                Exit Sub
            Else
                btnPrint.Enabled = True
            End If


            gvVendor.DataSource = dtCustomer
            FormatgvVendor()
            ReStoreGridVendor()

            RestoreGridSummaryRow()


            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox1.Enabled = False

            qry = Nothing
            strFromDate = Nothing
            strToDate = Nothing
            runDate = Nothing
            CompanyAdd = Nothing
            Comp_Name = Nothing
            strFIlterCheck = Nothing
            strCheckForSumm = Nothing
            strVenFilterPDC = Nothing
            strFIltervendor = Nothing
            strQry = Nothing
            BaseQry = Nothing
            BaseQryOpening = Nothing
            strtempBaseQryforopening = Nothing
            strtempBaseQryforopeningForMIS = Nothing

            GC.Collect()
            GC.WaitForFullGCComplete()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Private Sub FormatgvVendor()
        Try
            gvVendor.AllowAddNewRow = False
            gvVendor.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gvVendor.Columns.Count - 1
                gvVendor.Columns(ii).ReadOnly = True
                gvVendor.Columns(ii).IsVisible = False
            Next
            gvVendor.Columns("Vendor_Group_Code").IsVisible = False
            gvVendor.Columns("Vendor_Group_Code").Width = 101
            gvVendor.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            gvVendor.Columns("VCode").IsVisible = True
            gvVendor.Columns("VCode").Width = 180
            gvVendor.Columns("VCode").HeaderText = "Vendor Code"

            gvVendor.Columns("VName").IsVisible = True
            gvVendor.Columns("VName").Width = 350
            gvVendor.Columns("VName").HeaderText = "Vendor Name"

            gvVendor.Columns("OpngBal").IsVisible = False
            gvVendor.Columns("OpngBal").Width = 101
            gvVendor.Columns("OpngBal").HeaderText = "OpngBal"
            gvVendor.Columns("OpngBal").FormatString = "{0:f2}"

            gvVendor.Columns("DrAmt").IsVisible = False
            gvVendor.Columns("DrAmt").Width = 101
            gvVendor.Columns("DrAmt").HeaderText = "DrAmt"
            gvVendor.Columns("DrAmt").FormatString = "{0:f2}"


            gvVendor.Columns("CrAmt").IsVisible = False
            gvVendor.Columns("CrAmt").Width = 101
            gvVendor.Columns("CrAmt").HeaderText = "CrAmt"
            gvVendor.Columns("CrAmt").FormatString = "{0:f2}"

            gvVendor.Columns("LedgerAmt").IsVisible = True
            gvVendor.Columns("LedgerAmt").Width = 101
            gvVendor.Columns("LedgerAmt").HeaderText = "LedgerAmt"
            gvVendor.Columns("LedgerAmt").FormatString = "{0:f2}"

            gvVendor.Columns("AgeingAmt").IsVisible = True
            gvVendor.Columns("AgeingAmt").Width = 101
            gvVendor.Columns("AgeingAmt").HeaderText = "AgeingAmt"
            gvVendor.Columns("AgeingAmt").FormatString = "{0:f2}"

            gvVendor.Columns("Diff").IsVisible = True
            gvVendor.Columns("Diff").Width = 101
            gvVendor.Columns("Diff").HeaderText = "Diff"
            gvVendor.Columns("Diff").FormatString = "{0:f2}"

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    ''
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmRptVendorLedgerVsAgeing_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            funreset()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled AndAlso MyBase.isPrintFlag Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
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


    Private Sub ExportToExcel()
        Try
            If gvVendor.Rows.Count <= 0 Then
                Throw New Exception("No data found for Export.")
            End If

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)


            clsCommon.MyExportToExcelGrid("Vendor Ledger Report ", gvVendor, arrHeader, Me.Text)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub RestoreGridSummaryRow()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)
        TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)
        TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)
        TotalAmt = New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)
        TotalAmt = New GridViewSummaryItem("Payments", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)
        TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)
        TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)

        Dim TotalClosing As New GridViewSummaryItem()
        TotalClosing.FormatString = "{0:F2}"
        TotalClosing.Name = "Closing"
        TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
        summaryRowItem.Add(TotalClosing)


        gvVendor.MasterTemplate.SummaryRowsBottom.Clear()
        gvVendor.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        blnRefresh = True
        GC.Collect()
        Print()
        RestoreGridSummaryRow()
        ReportID = GetReportID()
        PageSetupReport_ID = GetReportID()
    End Sub
    Private Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        blnRefresh = False
        Print()

    End Sub



    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtVendorGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendorGroup._My_Click
        strQry = "select Ven_Group_Code as Code, Group_Desc as Name from TSPL_VENDOR_GROUP order by Ven_Group_Code "
        txtVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorGrpSelector@VendorLedger", strQry, "Code", "Name", txtVendorGroup.arrValueMember, txtVendorGroup.arrDispalyMember)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        strQry = "select TSPL_VENDOR_MASTER.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,TSPL_VENDOR_MASTER.Parent_Vendor_Code as [Parent Code],P1.Vendor_Name as [Parent Name]   from TSPL_VENDOR_MASTER  Left Outer Join TSPL_VENDOR_MASTER P1 on TSPL_VENDOR_MASTER.Parent_Vendor_Code =P1.Vendor_Code  where TSPL_VENDOR_MASTER.Status='N'  order by TSPL_VENDOR_MASTER.Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorSelector@VendorLedger", strQry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        'strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + StrPermission + "))xxx"
        strQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        strQry += " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationSelector@VendorLedge7", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub


    Private Sub LoadCurrencyType()
        dtOpening = New DataTable()
        dtOpening.Columns.Add("Code", GetType(String))
        dtOpening.Columns.Add("Name", GetType(String))
        dtOpening.Rows.Add("ConvRate", "Functional Currency")
        dtOpening.Rows.Add("1", "Vendor Currency")
        ddlCurrencyType.DataSource = dtOpening
        ddlCurrencyType.ValueMember = "Code"
        ddlCurrencyType.DisplayMember = "Name"
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            transportSql.QuickExportToExcel(gvVendor, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RbtnSaveLayout_Click(sender As Object, e As EventArgs) Handles RbtnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            Dim obj As New clsGridLayout()
            gvVendor.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = GetReportID() ' ReportID + "V"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvVendor.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvVendor.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()

        End If
    End Sub

    Private Sub ReStoreGridVendor()
        Try
            Dim TempReportID As String = "VendorLedgerVSAgeingReport"


            If clsCommon.myLen(TempReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempReportID + "V", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvVendor.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvVendor.Columns.Count - 1 Step ii + 1
                        gvVendor.Columns(ii).IsVisible = False
                        gvVendor.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvVendor.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub RbtnDeleteLayout_Click(sender As Object, e As EventArgs) Handles RbtnDeleteLayout.Click

        ReportID = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) 'ReportID & "V"
            FormatgvVendor()
            ReStoreGridVendor()
            RestoreGridSummaryRow()
        End If
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub



    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            transportSql.applyExportTemplate(gvVendor, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gvVendor, "", Me.Text, , arrHeader)

            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        Try

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

    Private Function GetReportID() As String

        ReportID = "VendorLedgerVsAgeingReport"
        ReportID = ReportID + "V"
        TemplateGridview = gvVendor
        Return ReportID
    End Function

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            If gvVendor.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)


            PageSetupReport_ID = GetReportID()
            transportSql.applyExportTemplate(gvVendor, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Vendor Ledger Vs Ageing Report", gvVendor, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        blnRefresh = True
        GC.Collect()
        Print(2)
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        blnRefresh = True
        GC.Collect()
        Print(1)
    End Sub

    Private Sub ExcelGrid_Click(sender As Object, e As EventArgs) Handles ExcelGrid.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If


            clsCommon.MyExportToExcelGrid(Me.Text, gvVendor, arrHeader, Me.Text, True)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDFGrid_Click(sender As Object, e As EventArgs) Handles PDFGrid.Click
        Try
            Dim FilePath As String = "C:\\ERPTempFolder\\Vendor Ledger Vs Ageing Report" + clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss") + ".pdf"
            Dim pdfExporter As ExportToPDF = Nothing

            pdfExporter = New ExportToPDF(gvVendor)
            pdfExporter.Font = New System.Drawing.Font("Verdana", 6)
            pdfExporter.TableBorderThickness = 1
            pdfExporter.FitToPageWidth = True
            pdfExporter.ExportVisualSettings = True
            pdfExporter.ExportHierarchy = True
            pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport
            pdfExporter.PageTitle = "Vendor Ledger Vs Ageing Report"
            pdfExporter.RunExport(FilePath)
            System.Diagnostics.Process.Start(FilePath)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class










