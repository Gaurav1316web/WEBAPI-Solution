'Updated by Dipti waila on 2-Nov-2012 added a Report Grid And button for Export To Excel,TEC/05/06/19-000519
'By vipin for Recipt type on 27/11/2012
'-12/12/12-11:35AM---Updation By--Pankaj Kumar, Change the format of start date and end date as well as yyyy/MMM/dd to dd/MMM/yyyy
'---Updation By--[Pankaj Kumar Chaudhary]---Against Ticket No---[BM00000000686, BM00000000490, BM00000000722, BM00000001052, BM00000001137, BM00000001138, BM00000001360]
'                   ,BM00000001442,BM00000002080]
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''BHA/04/10/18-000602
''updation by richa agarwal BM00000006475,BM00000006432,BM00000006829(drill down working),remove coalesce(TSPL_VENDOR_INVOICE_HEAD.is_Security,0)<>1 from print qry BM00000008065,BM00000008050,BM00000008443,BM00000008541,BM00000008563,BM00000008655,BM00000007353,BM00000008771,BM00000008768,BM00000008985
'==========BM00000007746,Rohit(On 27-Aug-2015)===================
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class frmRptVendorLedger
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim dtCustGrp As DataTable = Nothing
    Dim dtCustomer As DataTable = Nothing
    Dim dtMain As DataTable = Nothing
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
    Dim ShowVendorLedgerasPerRightsForLocation As Boolean = False
    Dim isRunDoubleClick As Boolean = False
    Dim StrPermission As String
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FormType = formid
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(FormType)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnExportToExcel.Visible = MyBase.isExport

        btnQExport.Visible = MyBase.isExport ' MyBase.isQuickExportFlag
        'radbtnBulkExp.Visible = MyBase.isQuickExportFlag
        btnPrint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmRptVendorLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'LoadVendorGroup()
        'LoadVendor()
        'chkVendorAll.IsChecked = True
        'LoadLocation()
        LoadCurrencyType()
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationSegment()
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            rbLandScape.IsChecked = True
            rbPortrait.IsChecked = False
        Else
            rbPortrait.IsChecked = True
            rbLandScape.IsChecked = False
        End If

        'dtpFromdate.Value = "01/Apr/2015"

        ' KUNAL > TICKET : BM00000009466 ============
        dtpFromdate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        dtptodate.Value = clsCommon.GETSERVERDATE()

        chkinvoice.Checked = True
        chkdebitnote.Checked = True
        chkcreditnote.Checked = True
        chkonaccount.Checked = True
        chkAdvance.Checked = True
        chkpayment.Checked = True
        chkLocAll.IsChecked = True
        chkVndrAll.IsChecked = True
        chkadjustment.Checked = True
        rbtnchildall.IsChecked = True

        gvVendorGroup.Dock = DockStyle.Fill
        gvVendor.Dock = DockStyle.Fill
        gv.Dock = DockStyle.Fill
        gvVendorGroup.Visible = False
        gvVendor.Visible = False
        ChkPDC.Visible = False
        chkNone.IsChecked = True

        SetMultiCurrencyVisibility()
        '' Anubhooti 24-Nov-2014 BM00000004658
        ''

        'If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
        '    ReportID = "MISCreditorReport"
        'Else
        '    ReportID = "VendorLedgerReport"
        'End If
        ReportID = GetReportID()
        rbtnDocWise.Visible = False

        '-- done by richa agarwal related to ticket no. KDI/12/03/18-000107 on 13 Mar,2018
        Dim AllowtoMakeApplyDocOnbyDefault As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoMakeApplyDocOnbyDefault, clsFixedParameterCode.AllowtoMakeApplyDocOnbyDefault, Nothing)) = 1, True, False))
        If AllowtoMakeApplyDocOnbyDefault = True Then
            chkIncludeApplyDocument.Checked = True
        End If
        AllowtoEmployeeSalaryIntegration = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoEmployeeSalaryIntegration, clsFixedParameterCode.AllowtoEmployeeSalaryIntegration, Nothing)) = 1, True, False))
        ShowVendorLedgerasPerRightsForLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowVendorLedgerasPerRightsForLocation, clsFixedParameterCode.ShowVendorLedgerasPerRightsForLocation, Nothing)) = 1, True, False))
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset all Parameters")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        isRunDoubleClick = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoubleClickOnVC, clsFixedParameterCode.DoubleClickOnVC, Nothing)) = 1, True, False)
    End Sub

    Sub LoadVendorGroup()
        Dim qry As String = "select Ven_Group_Code, Group_Desc  from TSPL_VENDOR_GROUP order by Ven_Group_Code "
        cbgVndrGroup.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVndrGroup.ValueMember = "Ven_Group_Code"
        cbgVndrGroup.DisplayMember = "Group_Desc"
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        RadGroupBox1.Enabled = True

        'rbPortrait.IsChecked = True
        'dtpFromdate.Value = "01/Apr/2015"
        'dtptodate.Value = clsCommon.GETSERVERDATE()

        'chkinvoice.Checked = True
        'chkdebitnote.Checked = True
        'chkcreditnote.Checked = True
        'chkonaccount.Checked = True
        'chkAdvance.Checked = True
        'chkpayment.Checked = True
        'chkLocAll.IsChecked = True
        'chkVndrAll.IsChecked = True
        'chkadjustment.Checked = True
        'rbtnchildall.IsChecked = True
        'ChkVendorWithZero.IsChecked = True
        'ChkPDC.IsChecked = False
        'ChkchildVendor.IsChecked = False
        'chkItemWise.IsChecked = False
        'txtCurrencyCode.Value = ""
        'gvVendorGroup.Dock = DockStyle.Fill
        'gvVendor.Dock = DockStyle.Fill
        'gv.Dock = DockStyle.Fill
        'gvVendorGroup.Visible = False
        'gvVendor.Visible = False
        'chkNone.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        chkIncludeApplyDocument.Checked = False

        gvVendorGroup.DataSource = Nothing
        gvVendor.DataSource = Nothing
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        chkExcludeOpening.Checked = False
        chkTurnOver.Checked = False
        '-- done by richa agarwal related to ticket no. KDI/12/03/18-000107 on 13 Mar,2018
        Dim AllowtoMakeApplyDocOnbyDefault As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoMakeApplyDocOnbyDefault, clsFixedParameterCode.AllowtoMakeApplyDocOnbyDefault, Nothing)) = 1, True, False))
        If AllowtoMakeApplyDocOnbyDefault = True Then
            chkIncludeApplyDocument.Checked = True
        End If
        ' btnPrint.Enabled = False
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
            If chkVndrSelect.IsChecked AndAlso cbgVndrGroup.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vendor Group.", Me.Text)
                Return
            End If
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vendor", Me.Text)
                Return
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single Location", Me.Text)
                Return
            End If
            If rbtnchildslct.IsChecked AndAlso cbgchild.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single Child Vendor", Me.Text)
                Return
            End If
            If rbtnchildall.IsChecked AndAlso chkVendorSelect.IsChecked Then
                Dim query As String = "select distinct (select ',''''+vendor_code+''''' from tspl_vendor_master where parent_vendor_code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") and is_parent_vendor<>'1' for xml path('')) as xvalue"
                childvendrcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
                Try
                    If childvendrcode.Substring(0, 1) = "," Then
                        childvendrcode = childvendrcode.Substring(1, childvendrcode.Length - 1)
                    End If
                Catch exx As Exception
                End Try
            End If

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
            'objCommonVar.BaseCurrencyCode
            ''richa agarwal 27/07/2015
            ' BaseQry = GetVendorBaseQry(rbPortrait.IsChecked, rbLandScape.IsChecked, chkAgainstSalary.Checked)
            Dim strFIltervendor As String = String.Empty
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strFIltervendor += clsCommon.GetMulcallString(txtVendor.arrValueMember)
            End If

            BaseQry = GetVendorBaseQry(rbPortrait.IsChecked, rbLandScape.IsChecked, chkAgainstSalaryAdvance.Checked, strFromDate, strToDate, strFIltervendor, False)
            BaseQryOpening = GetVendorBaseQry(rbPortrait.IsChecked, rbLandScape.IsChecked, chkAgainstSalaryAdvance.Checked, strFromDate, strToDate, strFIltervendor, True)


            Dim StrEmployeetypeCondition As String = String.Empty
            Dim StrEmployeeAdvancetypeCondition As String = String.Empty
            Dim StrEmployeeAdvancetypeConditionNotIncluded As String = String.Empty
            If chkTADA.Checked = True Then
                StrEmployeetypeCondition += "'TD',"
            End If
            If chkTravel.Checked = True Then
                StrEmployeetypeCondition += "'T',"
            End If
            If chkImprest.Checked = True Then
                StrEmployeetypeCondition += "'I',"
            End If
            If chkSalary.Checked = True Then
                StrEmployeetypeCondition += "'S',"
            End If
            If chkAdvanceImprest.Checked = True Then
                StrEmployeeAdvancetypeCondition += "'I',"
            End If
            If chkAdvanceTravel.Checked = True Then
                StrEmployeeAdvancetypeCondition += "'T',"
            End If
            If chkAgainstSalaryAdvance.Checked = True Then
                StrEmployeeAdvancetypeCondition += "'S','AAS',"
            End If

            BaseQry += " where 1=1 "
            BaseQryOpening += " where 1=1 "
            strtempBaseQryforopeningForMIS += " where 1=1 "

            ''richa ERO/25/06/21-001418
            If chkTurnOver.Checked Then
                BaseQry += " and  (len(isnull(Against_PurchaseReturn_No,''))>0 or len(isnull(Against_POInvoice_No,''))>0 or len(isnull(Against_BulkMillkPurchaseInvoice_No,''))>0 or len(isnull(Against_MillkPurchaseInvoice_No,''))>0 ) "
                BaseQryOpening += " and  (len(isnull(Against_PurchaseReturn_No,''))>0 or len(isnull(Against_POInvoice_No,''))>0 or len(isnull(Against_BulkMillkPurchaseInvoice_No,''))>0 or len(isnull(Against_MillkPurchaseInvoice_No,''))>0 ) "
                strtempBaseQryforopeningForMIS += " and  (len(isnull(Against_PurchaseReturn_No,''))>0 or len(isnull(Against_POInvoice_No,''))>0 or len(isnull(Against_BulkMillkPurchaseInvoice_No,''))>0 or len(isnull(Against_MillkPurchaseInvoice_No,''))>0 ) "
            Else
                If AllowtoEmployeeSalaryIntegration = True AndAlso chkAgainstSalaryAdvance.Checked = False Then
                    StrEmployeeAdvancetypeConditionNotIncluded = " and InnQuery.Emp_Adv_Type not in ('S','AAS') "
                    BaseQry += StrEmployeeAdvancetypeConditionNotIncluded
                    BaseQryOpening += StrEmployeeAdvancetypeConditionNotIncluded
                    strtempBaseQryforopeningForMIS += StrEmployeeAdvancetypeConditionNotIncluded
                End If

                If clsCommon.myLen(StrEmployeeAdvancetypeCondition) > 0 Then
                    StrEmployeeAdvancetypeCondition = StrEmployeeAdvancetypeCondition.Substring(0, StrEmployeeAdvancetypeCondition.Length - 1)
                    StrEmployeeAdvancetypeCondition = " and InnQuery.Emp_Adv_Type in (" & StrEmployeeAdvancetypeCondition & ") "
                    BaseQry += StrEmployeeAdvancetypeCondition
                    BaseQryOpening += StrEmployeeAdvancetypeCondition
                    strtempBaseQryforopeningForMIS += StrEmployeeAdvancetypeCondition
                End If
                If clsCommon.myLen(StrEmployeeAdvancetypeCondition) > 0 And clsCommon.myLen(StrEmployeetypeCondition) > 0 Then
                    StrEmployeetypeCondition = StrEmployeetypeCondition.Substring(0, StrEmployeetypeCondition.Length - 1)
                    StrEmployeetypeCondition = " or InnQuery.Emp_type in (" & StrEmployeetypeCondition & ") "
                    BaseQry += StrEmployeetypeCondition
                    BaseQryOpening += StrEmployeetypeCondition
                    strtempBaseQryforopeningForMIS += StrEmployeetypeCondition
                ElseIf clsCommon.myLen(StrEmployeetypeCondition) > 0 Then
                    StrEmployeetypeCondition = StrEmployeetypeCondition.Substring(0, StrEmployeetypeCondition.Length - 1)
                    StrEmployeetypeCondition = " and InnQuery.Emp_type in (" & StrEmployeetypeCondition & ") "
                    BaseQry += StrEmployeetypeCondition
                    BaseQryOpening += StrEmployeetypeCondition
                    strtempBaseQryforopeningForMIS += StrEmployeetypeCondition
                End If

            End If



            Dim strFIlterCheck As String = String.Empty
            Dim strCheckForSumm As String = String.Empty
            Dim strVenFilterPDC As String = String.Empty
            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_VENDOR_MASTER.Vendor_Group_Code in (" + clsCommon.GetMulcallString(txtVendorGroup.arrValueMember) + ")  " '----------changed here customer is used changed to vendor
                strVenFilterPDC += " and TSPL_VENDOR_MASTER.Vendor_Group_Code IN (" + clsCommon.GetMulcallString(txtVendorGroup.arrValueMember) + ")  "
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strFIlterCheck += " and (TSPL_VENDOR_MASTER.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")" 'Monika
                If ChkchildVendor.Checked Then
                    strFIlterCheck += " OR TSPL_VENDOR_MASTER.vendor_code in (select Vendor_Code from TSPL_VENDOR_MASTER Where ISNULL(Parent_Vendor_Code,'') in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + "))"
                End If
                strFIlterCheck += ")"
                strVenFilterPDC += " and TSPL_PAYMENT_HEADER.Vendor_Code IN (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + "," + clsCommon.GetMulcallString(txtChildVendor.arrValueMember) + ")"
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strFIlterCheck += " and RIGHT(final.account,3) in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  " 'by Monika
                strVenFilterPDC += " and right(TSPL_BANK_MASTER.BANKACC,3) IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + " )"
            Else
                If ShowVendorLedgerasPerRightsForLocation = True Then
                    strFIlterCheck += " and RIGHT(final.account,3) in (" + StrPermission + ")  "
                    strVenFilterPDC += " and right(TSPL_BANK_MASTER.BANKACC,3) IN (" + StrPermission + " )"
                End If
            End If

            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_VENDOR_MASTER.Vendor_Account in (" + clsCommon.GetMulcallString(txtAccountSet.arrValueMember) + ")  "
                strVenFilterPDC += " and TSPL_VENDOR_MASTER.Vendor_Account IN (" + clsCommon.GetMulcallString(txtAccountSet.arrValueMember) + " )"
            End If


            If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
                strFIlterCheck += " and TSPL_VENDOR_MASTER.CURRENCY_CODE in ('" + clsCommon.myCstr(txtCurrencyCode.Value) + "')"
            End If
            '-----------------------------------------------------
            If chkExcludeOpening.Checked = True Then
                BaseQryOpening = "   Select '' as Emp_type,'' as Emp_Adv_Type,null as Due_Date, '' as PO_SRN, '' as VCode, '' as VName, '' as DocNo, '' as DocType, null as DocDate, '' as DocNarr, '' as ChequeDetails, '' as Currency_Code, 1 as ConvRate, 0 AS CrAmt,0 AS DrAmt,0 AS Purchase,0 AS Payments ,  0 as drNote,0 as CrNote,'' as Document_Type, 0 as Balance_Amount, '' as account,null as Posting_Date,'' as GLDocType, '' as Comp_Code where 1=2"
                strtempBaseQryforopeningForMIS = "   Select '' as Emp_type,'' as Emp_Adv_Type,null as Due_Date, '' as PO_SRN, '' as VCode, '' as VName, '' as DocNo, '' as DocType, null as DocDate, '' as DocNarr, '' as ChequeDetails, '' as Currency_Code, 1 as ConvRate, 0 AS CrAmt,0 AS DrAmt,0 AS Purchase,0 AS Payments ,  0 as drNote,0 as CrNote,'' as Document_Type, 0 as Balance_Amount, '' as account,null as Posting_Date,'' as GLDocType, '' as Comp_Code where 1=2"
            End If

            Dim strQry As String = String.Empty
            '---------------Vendor Group Wise Data-----------
            If chkVendorGrupWise.IsChecked Then
                strQry = "Select 'VendorGroup' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, Vendor_Group_Code, MAX(Vendor_Group_Desc) as Vendor_Group_Desc, MAX(AccountSet) as AccountSet, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Purchase) as Purchase, SUM(Payments) as Payments, SUM(DrNote) as DrNote, SUM(CrNote) as CrNote, "
                If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                    strQry += "  SUM(OpngBal)-SUM(DrAmt)+SUM(CrAmt)+Sum(Purchase)-Sum(Payments) as [Closing] from ("
                Else
                    strQry += " SUM(OpngBal) + SUM(CrAmt)-SUM(DrAmt) as [Closing] from ("
                End If
                strQry += " Select Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, SUM(CrAmt)-SUM(DrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote From ( " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, strtempBaseQryforopeningForMIS, BaseQryOpening) & " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  <'" + strFromDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY Vendor_Group_Code" + Environment.NewLine & _
                Environment.NewLine + " UNION ALL" + Environment.NewLine & _
                " Select Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, 0 as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(CONVERT(DECIMAL(18,2),Purchase*" & ddlCurrencyType.SelectedValue & ")) as Purchase, SUM(CONVERT(DECIMAL(18,2),Payments* " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, "1", ddlCurrencyType.SelectedValue) & " )) as Payments, SUM(DrNote*" & ddlCurrencyType.SelectedValue & ") as DrNote, SUM(CrNote*" & ddlCurrencyType.SelectedValue & ") as CrNote from ( " + BaseQry + " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY Vendor_Group_Code" + Environment.NewLine & _
                " ) XXX GROUP BY Vendor_Group_Code ORDER BY Vendor_Group_Code"
                dtCustGrp = clsDBFuncationality.GetDataTable(strQry)
            End If
            '===============================update by richa agarwal 3 July,2018 ticket no. KDI/02/07/18-000383 pick vendor name from vendor master table instead of transaction table
            '---------------Vendor Wise Data-----------------
            If chkVendorWise.IsChecked Then
                strQry = "Select 'Vendor' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, VCode, MAX(VName) as VName, MAX(Vendor_Group_Code) as Vendor_Group_Code, MAX(Vendor_Group_Desc) as Vendor_Group_Desc, MAX(AccountSet) as AccountSet,  SUM(CONVERT(DECIMAL(18,2),OpngBal)) as OpngBal,  SUM(CONVERT(DECIMAL(18,2),DrAmt)) as DrAmt, SUM( CONVERT(DECIMAL(18,2),CrAmt)) as CrAmt,  SUM(CONVERT(DECIMAL(18,2),Purchase)) as Purchase, SUM( CONVERT(DECIMAL(18,2),Payments)) as Payments,  SUM(CONVERT(DECIMAL(18,2),DrNote)) as DrNote,  SUM(CONVERT(DECIMAL(18,2),CrNote)) as CrNote,  "
                If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                    strQry += "  SUM(CONVERT(DECIMAL(18,2),OpngBal))- SUM(CONVERT(DECIMAL(18,2),DrAmt))+ SUM(CONVERT(DECIMAL(18,2),CrAmt))+  Sum(CONVERT(DECIMAL(18,2),Purchase))- Sum(CONVERT(DECIMAL(18,2),Payments)) as [Closing] from ("
                Else
                    strQry += "  SUM(CONVERT(DECIMAL(18,2),OpngBal))+  SUM(CONVERT(DECIMAL(18,2),CrAmt))- SUM(CONVERT(DECIMAL(18,2),DrAmt)) as [Closing] from ("
                End If
                strQry += " Select VCode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code) as Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, SUM(CrAmt)-SUM(DrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote From ( " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, strtempBaseQryforopeningForMIS, BaseQryOpening) & " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  <'" + strFromDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY VCode" + Environment.NewLine & _
               " UNION ALL" + Environment.NewLine & _
               " Select VCode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code) as Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, 0 as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(CONVERT(DECIMAL(18,2),Purchase*" & ddlCurrencyType.SelectedValue & ")) as Purchase, SUM(CONVERT(DECIMAL(18,2),Payments*" & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, "1", ddlCurrencyType.SelectedValue) & ")) as Payments, SUM(DrNote*" & ddlCurrencyType.SelectedValue & ") as DrNote, SUM(CrNote*" & ddlCurrencyType.SelectedValue & ") as CrNote from ( " + BaseQry + " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY VCode" + Environment.NewLine & _
               " ) XXX GROUP BY VCode ORDER BY VCode"
                dtCustomer = clsDBFuncationality.GetDataTable(strQry)

            End If




            If chkAgainstSalaryAdvance.Checked = True Then
                AccountSet = "Advance_Against_Salary"
            Else
                AccountSet = "Payable_Account"
            End If
            If (rbtnDocWise.IsChecked = True) AndAlso chkAgainstSalaryAdvance.Checked = False Then
                strQry = " With CTETemp as " &
                " ( SELECT * " &
                " ,( SELECT MAX( TSPL_JOURNAL_DETAILS.Account_code) AS Account_code    FROM TSPL_JOURNAL_DETAILS " &
                " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =XXXX.Vendor_Account   " &
                "LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " &
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = case when XXXX.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when XXXX.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when XXXX.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when XXXX.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when XXXX.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end " &
                " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=XXXX.Voucher_No ) AS JEAccount_code" &
                " ,( SELECT SUM( TSPL_JOURNAL_DETAILS.Amount)   AS Amount  FROM TSPL_JOURNAL_DETAILS " &
                " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =XXXX.Vendor_Account   " &
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " &
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') =  case when XXXX.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when XXXX.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when XXXX.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when XXXX.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when XXXX.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end " &
                " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=XXXX.Voucher_No ) AS JEAMT FROM  " &
                " ( Select ROW_NUMBER() OVER (PARTITION BY VCode ORDER BY VCode, DocDate) as RowNo, '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,* from ( " &
                " Select max(EXCHANGE_GAIN_AMT) as EXCHANGE_GAIN_AMT,max(EXCHANGE_LOSS_AMT) as  EXCHANGE_LOSS_AMT,  MAX(Due_Date) AS Due_Date,  MAX(PO_SRN) AS PO_SRN,  MAX(vcode) AS vcode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, DocNo, MAX(DocType) AS DocType, MAX(DocDate) AS DocDate,  MAX(DocNarr) AS DocNarr, MAX(ChequeDetails) AS ChequeDetails, MAX(Final.Currency_Code) AS Currency_Code, CASE WHEN SUM(DrAmt) >= SUM(CrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") THEN SUM(DrAmt) - SUM(CrAmt) ELSE 0 END AS DrAmt, CASE WHEN SUM(CrAmt) > SUM(DrAmt) THEN  SUM(CrAmt) - SUM(DrAmt) ELSE 0 END AS CrAmt, SUM(Purchase* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Purchase, SUM(Payments* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Payments, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote, SUM(Purchase* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Purchase1, SUM(Payments* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Payments1, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote1, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote1, SUM(CrAmt-DrAmt) as EffectiveAmt, MAX(final.Document_Type) AS Document_Type, SUM(Balance_Amount* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Balance_Amount,substring(MAX(account),LEN(MAX(account))-2,3) + ' - ' + MAX(TSPL_GL_SEGMENT_CODE.Description)  as account, CONVERT(VARCHAR,MAX(final.Posting_Date),103) As Posting_Date,MAX(GLDocType) AS GLDocType, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code) AS Vendor_Group_Code,  MAX(TSPL_VENDOR_GROUP.Group_Desc) AS Group_Desc  , Case When SUM(DrAmt)>0 Then 0 else 1 End as OrderdrCr, Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then 'Done' Else 'Pending' End as [Reconciliation], Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else '' End as [Reconciliation_Date],MAX(TSPL_JOURNAL_MASTER.Voucher_No) AS Voucher_No ,MAX(TSPL_VENDOR_MASTER.Vendor_Account) AS Vendor_Account  FROM " + Environment.NewLine &
                "( " + BaseQry + " )  Final LEFT OUTER JOIN TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =substring(Final.account,LEN(Final.account)-2,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code LEFT OUTER JOIN " + Environment.NewLine &
                 "tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No and tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " &
                " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo and TSPL_JOURNAL_MASTER.Source_Code not in ('AR-AD') " &
                 " where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous'   " + strFIlterCheck + "" + Environment.NewLine &
                " GROUP By DocNo  ) XXX " + Environment.NewLine &
                "  )XXXX " + Environment.NewLine &
                " ) Select CTETemp.RowNo, CTETemp.RunDate , CTETemp.Address , CTETemp.Comp_Name , CTETemp.FilterFromDate , CTETemp.FilterToDate, CTETemp.Due_Date, CTETemp.PO_SRN, CTETemp.VCode, CTETemp.VName , CTETemp.DocNo, CASE WHEN (sELECT ISNULL(Against_BulkMillkPurchaseInvoice_No,'')  FROM TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO=CTETemp.DocNo) <>'' THEN 'Pur. Bulk' else CASE WHEN (sELECT ISNULL(Against_MillkPurchaseInvoice_No,'')  FROM TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO=CTETemp.DocNo) <>'' THEN 'Pur. MCC' else CTETemp.DocType end end as DocType, CONVERT(VARCHAR,CTETemp.DocDate,103) as DocDate, CTETemp.DocNarr,  CASE WHEN (sELECT ISNULL(Against_BulkMillkPurchaseInvoice_No,'')  FROM TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO=CTETemp.DocNo) <>'' THEN 'Pur. Bulk' else CASE WHEN (sELECT ISNULL(Against_MillkPurchaseInvoice_No,'')  FROM TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO=CTETemp.DocNo) <>'' THEN 'Pur. MCC' else CTETemp.DocType end end as DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, CTETemp.DrAmt , CTETemp.CrAmt, CTETemp.Purchase, CTETemp.Payments, CTETemp.DrNote, CTETemp.CrNote, CTETemp.Purchase1, CTETemp.Payments1, CTETemp.DrNote1, CTETemp.CrNote1, SUM(CrAmt-DrAmt) Over (Partition by VCode ORDER BY RowNo) as [Closing], CTETemp.EffectiveAmt , CTETemp.Document_Type , CTETemp.Balance_Amount , CTETemp.account , " &
                " CTETemp.Posting_Date, CTETemp.GLDocType, CTETemp.Vendor_Group_Code, CTETemp.Group_Desc, CTETemp.OrderdrCr, CTETemp.Reconciliation, CTETemp.Reconciliation_Date,CTETemp.Voucher_No AS [JE No],CTETemp.JEAccount_Code AS [JE Account],CTETemp.JEAMT [JE Amount],CTETemp.EffectiveAmt + CTETemp.JEAMT AS DiffAmt,(Case when CTETemp.DocType='Pur.Invoice' and CTETemp.Document_Type='D' then 'Debit Note' else CTETemp.DocType end) as [Document Type Detail]  from CTETemp ORDER BY CTETemp.VCode , RowNo "

            Else
                If chkNone.IsChecked = True OrElse rbntDocWiseMerge.IsChecked = True Then
                    '------------------Detail Level Data------------------- ''richa TEC/05/06/19-000514 add  TSPL_JOURNAL_MASTER.source_code<>'AR-AD' so that only payment adjustment header data shown,UDL/23/07/19-000307,TEC/23/07/19-000953
                    strQry = "With CTETemp as " &
                    " ( Select ROW_NUMBER() OVER (PARTITION BY VCode ORDER BY VCode, DocDate) as RowNo, '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, "
                    If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
                        strQry += "DrAmt  AS DrPDC,CrAmt  AS CrPDC,EffectiveAmt  AS EffPDC,Balance_Amount  AS BalPDC,"
                    End If
                    strQry += " * from ( Select Null As Due_Date ,max(PO_SRN) As PO_SRN, vcode,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName,'' as DocNo,'' as DocType, NULL as DocDate, 'Opening Balance' as DocNarr,null as Reference_Doc_No, '' as ChequeDetails, '' as Currency_Code, Case WHEN SUM(DrAmt)>=SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End as DrAmt, Case WHEN SUM(DrAmt)< SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End as CrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote, SUM(Purchase) as Purchase1, SUM(Payments) as Payments1, SUM(DrNote) as DrNote1, SUM(CrNote) as CrNote1, 0 as EffectiveAmt, '' as Document_Type, 0 as Balance_Amount, '' as account, '' As Posting_Date, '' as GLDocType, '' as Vendor_Group_Code, '' as Group_Desc , 0 as OrderdrCr, '' as [Reconciliation], '' as [Reconciliation_Date] ,'' AS Voucher_No,'' AS Vendor_Account   ,'' As JEAccount_Code,0 As JEAmt   " + Environment.NewLine &
                    " From ( " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, strtempBaseQryforopeningForMIS, BaseQryOpening) & " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " &
                    " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo and TSPL_JOURNAL_MASTER.Source_Code not in ('AR-AD')" &
                   " where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  <'" + strFromDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY VCode" + Environment.NewLine &
                    " UNION ALL ------------------Main UNION Between [Opening And Data]------------------------------------" + Environment.NewLine &
                    " Select Due_Date, PO_SRN, vcode, TSPL_VENDOR_MASTER.Vendor_Name as VName, DocNo, DocType, DocDate, DocNarr,Reference_Doc_No, ChequeDetails, Final.Currency_Code, DrAmt as DrAmt, CrAmt as CrAmt, Purchase* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as Purchase, Payments* " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, "1", IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "")) & " as Payments, DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as DrNote, CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as CrNote, Purchase* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as Purchase1, Payments* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as Payments1, DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as DrNote1, CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as CrNote1, (CrAmt-DrAmt) as EffectiveAmt, final.Document_Type, Balance_Amount* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as Balance_Amount,substring(account,LEN(account)-2,3) + ' - ' + TSPL_GL_SEGMENT_CODE.Description  as account, CONVERT(VARCHAR,final.Posting_Date,103) As Posting_Date,GLDocType, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc , Case When DrAmt>0 Then 0 else 1 End as OrderdrCr, Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then 'Done' Else 'Pending' End as [Reconciliation], Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then CONVERT(VARCHAR,tspl_BankReco_Detail.Reconciliation_Date,103) Else '' End as [Reconciliation_Date] ,TSPL_JOURNAL_MASTER.Voucher_No AS Voucher_No   , TSPL_VENDOR_MASTER.Vendor_Account ,( SELECT top 1 TSPL_JOURNAL_DETAILS.Account_code AS Account_code    FROM TSPL_JOURNAL_DETAILS " &
                    " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Account " &
                    " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " &
                    " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') =  case when Final.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType='TDS' then (Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code= ( Select Deduction_Code from tspl_vendor_master where vendor_code=Final.Vcode)) else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end  " &
                    " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No and TSPL_JOURNAL_DETAILS.Account_code =case when Final.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType='TDS' then (Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code= ( Select Deduction_Code from tspl_vendor_master where vendor_code=Final.Vcode)) else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end ) AS JEAccount_code " &
                    " ,( SELECT top 1  TSPL_JOURNAL_DETAILS.Amount AS Amount  FROM TSPL_JOURNAL_DETAILS " &
                    " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Account   " &
                    " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " &
                    " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') =  case when Final.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType='TDS' then (Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code= ( Select Deduction_Code from tspl_vendor_master where vendor_code=Final.Vcode)) else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end " &
                    " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No and TSPL_JOURNAL_DETAILS.Account_code =case when Final.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType='TDS' then (Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code= ( Select Deduction_Code from tspl_vendor_master where vendor_code=Final.Vcode)) else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end ) AS JEAmount from" + Environment.NewLine &
                    "( " + BaseQry + " )  Final LEFT OUTER JOIN TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =substring(Final.account,LEN(Final.account)-2,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' " + Environment.NewLine &
                    " LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code LEFT OUTER JOIN " + Environment.NewLine &
                    " tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No and tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " &
                    " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo and TSPL_JOURNAL_MASTER.Source_Code not in ('AR-AD')" &
                   " where isnull(TSPL_JOURNAL_MASTER.source_code,'')<>'AR-AD' and final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous'   " + strFIlterCheck + "" + Environment.NewLine &
                    " ) XXX " + Environment.NewLine &
                    " ) Select CTETemp.RowNo, CTETemp.RunDate , CTETemp.Address , CTETemp.Comp_Name , CTETemp.FilterFromDate , CTETemp.FilterToDate, CTETemp.Due_Date, CTETemp.PO_SRN, CTETemp.VCode, CTETemp.VName , CTETemp.DocNo, CTETemp.DocType, CONVERT(VARCHAR,CTETemp.DocDate,103) as DocDate, CTETemp.DocNarr,CTETemp.Reference_Doc_No, CTETemp.ChequeDetails, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, convert(decimal(18,2), CTETemp.DrAmt) as DrAmt , convert(decimal(18,2), CTETemp.CrAmt) as CrAmt ,convert(decimal(18,2), CTETemp.Purchase) as Purchase ,convert(decimal(18,2), CTETemp.Payments) as Payments, CTETemp.DrNote, CTETemp.CrNote, convert(decimal(18,2),CTETemp.Purchase1) as Purchase1, convert(decimal(18,2),CTETemp.Payments1) as Payments1, CTETemp.DrNote1, CTETemp.CrNote1, " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, " SUM(CrAmt+Purchase -DrAmt-Payments ) ", "  SUM(CrAmt-DrAmt)") & "   Over (Partition by VCode ORDER BY RowNo) as [Closing], CTETemp.EffectiveAmt , CTETemp.Document_Type , CTETemp.Balance_Amount , CTETemp.account , " &
                    " CTETemp.Posting_Date, CTETemp.GLDocType, CTETemp.Vendor_Group_Code, CTETemp.Group_Desc, CTETemp.OrderdrCr, CTETemp.Reconciliation, CTETemp.Reconciliation_Date ,CTETemp.Voucher_No AS [JE No],CTETemp.JEAccount_Code AS [JE Account],isnull(CTETemp.JEAMT,0) as  [JE Amount],CASE WHEN CTETemp.DocNarr='Opening Balance' THEN 0 ELSE isnull(CTETemp.EffectiveAmt,0) +  isnull(CTETemp.JEAMT,0) END  AS DiffAmt,(Case when CTETemp.DocType='Pur.Invoice' and CTETemp.Document_Type='D' then 'Debit Note' else CTETemp.DocType end) as [Document Type Detail] from CTETemp ORDER BY CTETemp.VCode , RowNo "
                End If
            End If


            '' bulk export
            If BulkExport = 1 Then
                transportSql.BulkExport("Vendor_Ledger", strQry, "ORDER BY CTETemp.VCode , RowNo", "csv", "Select CTETemp.RowNo")
                Exit Sub
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("Vendor_Ledger", strQry, "ORDER BY CTETemp.VCode , RowNo", "xls", "Select CTETemp.RowNo")
                Exit Sub
            End If

            If chkNone.IsChecked OrElse rbntDocWiseMerge.IsChecked Then
                dtMain = clsDBFuncationality.GetDataTable(strQry)
            End If

            'strQry = "With CTETemp as ( Select ROW_NUMBER() OVER (PARTITION BY VCode ORDER BY VCode, DocDate) as RowNo, '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, "
            'If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
            '    strQry += "DrAmt  AS DrPDC,CrAmt  AS CrPDC,EffectiveAmt  AS EffPDC,Balance_Amount  AS BalPDC,"
            'End If
            'strQry += " XXX.* from (" + Environment.NewLine & _
            '" Select Due_Date, PO_SRN, vcode,TSPL_VENDOR_MASTER.Vendor_Name as VName, DocNo, DocType, DocDate, DocNarr, ChequeDetails, Final.Currency_Code, DrAmt*" & ddlCurrencyType.SelectedValue & " as DrAmt, CrAmt*" & ddlCurrencyType.SelectedValue & " as CrAmt, Purchase*" & ddlCurrencyType.SelectedValue & " as Purchase, Payments*" & ddlCurrencyType.SelectedValue & " as Payments, DrNote*" & ddlCurrencyType.SelectedValue & " as DrNote, CrNote*" & ddlCurrencyType.SelectedValue & " as CrNote, (CrAmt*" & ddlCurrencyType.SelectedValue & "-DrAmt*" & ddlCurrencyType.SelectedValue & ") as EffectiveAmt, final.Document_Type, Balance_Amount*" & ddlCurrencyType.SelectedValue & " as Balance_Amount, substring(account,LEN(account)-2,3) + ' - ' + TSPL_GL_SEGMENT_CODE.Description  as account, CONVERT(VARCHAR,Posting_Date,103) As Posting_Date,GLDocType, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc , Case When DrAmt>0 Then 0 else 1 End as OrderdrCr, Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then 'Done' Else 'Pending' End as [Reconciliation], Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then CONVERT(VARCHAR,tspl_BankReco_Detail.Reconciliation_Date,103) Else '' End as [Reconciliation_Date] from" + Environment.NewLine & _
            '  "( " + strtempBaseQryforopening + " ) Final LEFT OUTER JOIN TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =substring(Final.account,LEN(Final.account)-2,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' " + Environment.NewLine & _
            '   " LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code LEFT OUTER JOIN (select * from tspl_BankReco_Detail where  tspl_BankReco_Detail.Reconciliation_Status='C') tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103) < '" + strFromDate + "' and DocType <>'Mislleneous'  " + strFIlterCheck + "" + Environment.NewLine & _
            '  " ) XXX " + Environment.NewLine & _
            '  " ) Select CTETemp.RowNo, CTETemp.RunDate , CTETemp.Address , CTETemp.Comp_Name , CTETemp.FilterFromDate , CTETemp.FilterToDate, CTETemp.Due_Date, CTETemp.PO_SRN, CTETemp.VCode, CTETemp.VName , CTETemp.DocNo, CTETemp.DocType, CONVERT(VARCHAR,CTETemp.DocDate,103) as DocDate, CTETemp.DocNarr,  CTETemp.ChequeDetails, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, CTETemp.DrAmt , CTETemp.CrAmt, CTETemp.Purchase, CTETemp.Payments, CTETemp.DrNote, CTETemp.CrNote, CTETemp.Purchase as Purchase1, CTETemp.Payments as Payments1, CTETemp.DrNote as DrNote1, CTETemp.CrNote as CrNote1, SUM(CrAmt-DrAmt) Over (Partition by VCode ORDER BY RowNo) as [Closing], CTETemp.EffectiveAmt , CTETemp.Document_Type , CTETemp.Balance_Amount , CTETemp.account , " & _
            '  " CTETemp.Posting_Date, CTETemp.GLDocType, CTETemp.Vendor_Group_Code, CTETemp.Group_Desc, CTETemp.OrderdrCr, CTETemp.Reconciliation, CTETemp.Reconciliation_Date  from CTETemp ORDER BY CTETemp.VCode , RowNo "


            'dtOpening = clsDBFuncationality.GetDataTable(strQry)

            If (chkNone.IsChecked = True OrElse rbntDocWiseMerge.IsChecked = True) AndAlso dtMain.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data not found", Me.Text)
                gv.DataSource = Nothing
                gv.Columns.Clear()
                gv.Rows.Clear()
                Exit Sub
            Else
                If chkVendorWise.IsChecked Or chkNone.IsChecked Or rbntDocWiseMerge.IsChecked Then
                    btnBack.Enabled = True
                Else
                    btnBack.Enabled = False
                End If
            End If

            If chkVendorGrupWise.IsChecked Then
                gvVendorGroup.DataSource = dtCustGrp
                FormatgvVendorGroup()
            End If

            If chkVendorWise.IsChecked Then
                gvVendor.DataSource = dtCustomer
                FormatgvVendor()
            End If



            ' gv.MasterTemplate.SortDescriptors.Clear()
            If chkNone.IsChecked = True OrElse rbntDocWiseMerge.IsChecked = True Then
                gv.DataSource = Nothing
                gv.Columns.Clear()
                gv.Rows.Clear()
                gv.DataSource = dtMain
                SetGridFormat(False)
            End If


            gridHideVisible()

            ''gv.SortDescriptors=""

            ReStoreGridDetail()
            ReStoreGridVendor()
            ReStoreGridVendorGrp()

            RestoreGridSummaryRow()

            gv.MasterTemplate.SortDescriptors.Clear()
            If blnRefresh = False Then
                'If chkVendorGrupWise.IsChecked Then
                '    frmCrystalReportViewer.funreport(CrystalReportFolder.Purchase, dtCustGrp, "rptVendorLedgerSummary_DEMO", "Vendor Ledger")
                'ElseIf chkVendorWise.IsChecked Then
                '    frmCrystalReportViewer.funreport(CrystalReportFolder.Purchase, dtCustomer, "rptVendorLedgerSummary_DEMO", "Vendor Ledger")
                ' Else
                If chkNone.IsChecked = False OrElse rbntDocWiseMerge.IsChecked = False Then
                    dtMain = clsDBFuncationality.GetDataTable(strQry)
                End If
                Dim frmCRV As New frmCrystalReportViewer()
                If (chkNone.IsChecked OrElse rbntDocWiseMerge.IsChecked) AndAlso rbPortrait.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedger", "Vendor Invoice Report")
                ElseIf chkVendorGrupWise.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedgerSummaryGroupBy-KDIL", "Vendor Ledger Report")
                ElseIf chkVendorWise.IsChecked = True Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedgerSummary_DEMO", "Vendor Ledger Report")
                ElseIf (chkNone.IsChecked OrElse rbntDocWiseMerge.IsChecked) AndAlso rbLandScape.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedgerLandScape", "Vendor Ledger Report")
                ElseIf (chkNone.IsChecked OrElse rbntDocWiseMerge.IsChecked) Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedgerSummaryGroupByDoc-KDIL", "Vendor Ledger Report")
                End If
                frmCRV = Nothing
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox1.Enabled = False

            qry = Nothing
            strFromDate = Nothing
            strToDate = Nothing
            runDate = Nothing
            IsPDCCheque = Nothing
            CompanyAdd = Nothing
            Comp_Name = Nothing
            childvendrcode = Nothing
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


    Public Function GetVendorBaseQry(ByVal strPortrait As Boolean, ByVal strLandscape As Boolean, ByVal IsOnlyForAgainstSalary As Boolean, ByVal strfromdate As String, ByVal strtodate As String, ByVal strvendor As String, ByVal isOpening As Boolean, Optional ByVal ISShowApplyDocument As Boolean = False) As String
        Dim strtempBaseQry As String = String.Empty
        Try
            'If IsOnlyForAgainstSalary Then
            ''BHA/28/08/18-000492
            If ISShowApplyDocument = True Then
                chkIncludeApplyDocument.Checked = True
            End If

            Dim strShowReferenceDocNoofAPInvoice As String = "   (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,"

            Dim strTaxRecovarableQuery As String = " - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then " &
       " ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'  " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end + " &
       " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y' " &
       " then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end) "

            Dim strQryForRMDA As String = " - (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and LEN(ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0 then (Select sum(TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt) from TSPL_TRANSFER_ORDER_HEAD where Status=1 and TSPL_TRANSFER_ORDER_HEAD.RMDA_Code in (select TSPL_SRN_HEAD.RMDA_No from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN where TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  )) else 0 end )"
            strtempBaseQry = " select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.vendor_code as VCode,TSPL_VENDOR_INVOICE_HEAD.vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate,"

            strtempBaseQry += " case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total " + strTaxRecovarableQuery + " else 0 end + ( case when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END) as CrAmt, " &
              "  case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('D') then Document_Total " + strTaxRecovarableQuery + " else 0 end as DrAmt, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total " + strTaxRecovarableQuery + " Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total " + strTaxRecovarableQuery + " Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total " + strTaxRecovarableQuery + " Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code " + Environment.NewLine
            If rbntDocWiseMerge.IsChecked Then
                strtempBaseQry += ",case when TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No is null then TSPL_VENDOR_INVOICE_HEAD.Document_No else TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No end as Main_VSP_Milk_AP_Invoice_No  "
            End If
            strtempBaseQry += " from TSPL_VENDOR_INVOICE_HEAD where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  
and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0  
and isnull(TSPL_VENDOR_INVOICE_HEAD.Is_Security,0)=0  " + Environment.NewLine
            If isOpening = True Then
                strtempBaseQry += "  and  convert(date,Invoice_Entry_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
            Else
                strtempBaseQry += "  and  convert(date,Invoice_Entry_Date,103)  >='" + strfromdate + "' and  convert(date,Invoice_Entry_Date,103)  <='" + strtodate + "' " + Environment.NewLine
            End If

            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and TSPL_VENDOR_INVOICE_HEAD.vendor_code in (" & strvendor & ")"
            End If

            If rbntDocWiseMerge.IsChecked Then
                strtempBaseQry = "select max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then Reference_Doc_No else null end) as Reference_Doc_No,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then Emp_type else null end) as Emp_type,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then Emp_Adv_Type else null end) as Emp_Adv_Type,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then Due_Date else null end) as Due_Date,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then PO_SRN else null end) as PO_SRN,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then VCode else null end) as VCode,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then VName else null end) as VName,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then DocNo else null end) as DocNo,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then DocType else null end) as DocType,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then DocDate else null end) as DocDate,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then DocNarr else null end) as DocNarr,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then ChequeDetails else null end) as ChequeDetails,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then CURRENCY_CODE else null end) as CURRENCY_CODE,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then ConvRate else null end) as ConvRate,case when sum(CrAmt-DrAmt)>0 then sum(CrAmt-DrAmt) else 0 end as CrAmt,case when sum(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end as DrAmt,sum(Purchase) as Purchase,sum(Payments) as Payments,sum(DrNote) as DrNote,sum(CrNote) as CrNote,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then Document_Type else null end) as Document_Type,sum(Balance_Amount) as Balance_Amount,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then account else null end) as account,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then Posting_Date else null end) as Posting_Date,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then GLDocType else null end) as GLDocType,max(case when DocNo=Main_VSP_Milk_AP_Invoice_No then Comp_Code else null end) as Comp_Code from  ( " + strtempBaseQry + ")x group by Main_VSP_Milk_AP_Invoice_No"
            End If

            strtempBaseQry += " UNION ALL" + Environment.NewLine
            ''------------- code inserted for WCT type of documents
            strtempBaseQry += " Select * from ( select " & strShowReferenceDocNoofAPInvoice & "  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,'WCT' as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate," &
            " 0 as CrAmt,case when isnull(TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END  DrAmt, " &
            " Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0  ) FinalWCt where (FinalWCt.CrAmt <>0 or FinalWCt.DrAmt <>0) " + Environment.NewLine

            If isOpening = True Then
                strtempBaseQry += " and  convert(date,FinalWCt.DocDate ,103) <'" + strfromdate + "'  "
            Else
                strtempBaseQry += " and  convert(date,FinalWCt.DocDate ,103)  >='" + strfromdate + "' and  convert(date,FinalWCt.DocDate ,103)  <='" + strtodate + "' "
            End If

            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and FinalWCt.VCode in (" & strvendor & ")"
            End If
            strtempBaseQry += "  UNION ALL" + Environment.NewLine
            ''---------------------------

            strtempBaseQry += " Select  Reference_Doc_No,Emp_type,Emp_Adv_Type,Due_Date, PO_SRN, VCode, VName, DocNo, DocType, DocDate, DocNarr, ChequeDetails, Currency_Code, ConvRate, CrAmt, DrAmt, "
            If chkVendorWise.IsChecked Or chkVendorGrupWise.IsChecked Then
                strtempBaseQry += "case when DocType='Pur.Invoice' then  CrAmt-DrAmt else 0 end as Purchase,"
            Else
                strtempBaseQry += " CrAmt-DrAmt as Purchase,"
            End If
            strtempBaseQry += " 0 as Payments, 0 as drNote, 0 as CrNote, Document_Type, Balance_Amount, account, Posting_Date, GLDocType, Comp_Code from (" + Environment.NewLine &
            " select " & strShowReferenceDocNoofAPInvoice & "  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date ,CASE WHEN isnull(TSPL_PI_HEAD.Against_PO,'') <> '' THEN isnull(TSPL_PI_HEAD.Against_PO,'')  ELSE ''  + isnull(TSPL_PI_HEAD.Against_SRN,'') END  AS PO_SRN ,"
            ' If rbPortrait.IsChecked = True Then
            If strPortrait = True Then
                ' strtempBaseQry += " TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date)+ (SELECT ' & Items Are- '+STUFF((SELECT ', ' + t2.Item_Desc+'('+Convert(Varchar,COnvert(Decimal(18,2),t2.Item_Cost))+' Rs)' FROM TSPL_PI_DETAIL t2 WHERE t2.PI_No = TSPL_PI_HEAD.PI_No FOR XML PATH ('')),1,2,'')) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end) as CrAmt, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt" + strQryForRMDA + " as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1" + Environment.NewLine

                strtempBaseQry += " TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date)+ (SELECT ' & Items Are- '+STUFF((SELECT ', ' + t2.Item_Desc+'('+Convert(Varchar,COnvert(Decimal(18,2),t2.Item_Cost))+' Rs)' FROM TSPL_PI_DETAIL t2 WHERE t2.PI_No = TSPL_PI_HEAD.PI_No FOR XML PATH ('')),1,2,'')) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " else 0 end) as CrAmt, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt" + strQryForRMDA + " as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1" + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += " and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  <'" + strfromdate + "'"
                Else
                    strtempBaseQry += " and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  <='" + strtodate + "'"
                End If


                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and TSPL_PI_HEAD.Vendor_Code in (" & strvendor & ")"
                End If
                ' ElseIf rbLandScape.IsChecked = True Then
            ElseIf strLandscape = True Then
                'strtempBaseQry += " TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end) as CrAmt,  (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt " + strQryForRMDA + " as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1" + Environment.NewLine
                strtempBaseQry += " TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " else 0 end) as CrAmt,  (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt " + strQryForRMDA + " as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1" + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += " and  convert(date,TSPL_PI_HEAD.PI_Date ,103) <'" + strfromdate + "' "
                Else
                    strtempBaseQry += " and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  <='" + strtodate + "'"
                End If

                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and TSPL_PI_HEAD.Vendor_Code in (" & strvendor & ")"
                End If
            End If
            strtempBaseQry += ") XXX"
            strtempBaseQry += " UNION ALL" + Environment.NewLine
            '  BM00000008275 BM00000008234 BM00000008238 case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then Actual_Total_TDS else 0 END, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then -1*Actual_Total_TDS else Actual_Total_TDS END as Payments CHANGED TO " case when TSPL_REMITTANCE.Document_Type ='D' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1* Actual_Total_TDS else Actual_Total_TDS END As Payments " as per ashok/amit sir. 26-Oct-2015
            strtempBaseQry += " select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, Case When TSPL_REMITTANCE.Document_Type IN ('I','C','D') Then TSPL_VENDOR_INVOICE_HEAD.Description Else TSPL_PAYMENT_HEADER.Entry_Desc End as DocNarr, '', Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE Else TSPL_PAYMENT_HEADER.CURRENCY_CODE End as Currency_Code, Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.ConvRate Else TSPL_PAYMENT_HEADER.ConvRate End as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then "
            'If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
            '    strtempBaseQry += " 0 "
            'Else
            '    strtempBaseQry += " Actual_Total_TDS "
            'End If
            strtempBaseQry += " Actual_Total_TDS "

            strtempBaseQry += " else 0 END, case when TSPL_REMITTANCE.Document_Type ='I' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as Purchase, case when TSPL_REMITTANCE.Document_Type IN "
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                strtempBaseQry += "('I','D','C')"
            Else
                strtempBaseQry += "('I','D')"
            End If
            strtempBaseQry += " AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then 0 else Actual_Total_TDS END as Payments, case when TSPL_REMITTANCE.Document_Type ='D' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, Case When ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'')<>'' Then TSPL_VENDOR_INVOICE_HEAD.Posting_Date Else TSPL_PAYMENT_HEADER.Payment_Post_Date End as Posting_Date,case when TSPL_REMITTANCE.Document_Type in ('C') then 'AP-CN' when TSPL_REMITTANCE.Document_Type in ('I') then 'AP-IN' when TSPL_REMITTANCE.Document_Type in ('D') then 'AP-DN' else case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE Left Outer Join TSPL_VENDOR_INVOICE_HEAD ON TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 and TSPL_REMITTANCE.Is_TDS_Provision = 'N' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   " + Environment.NewLine

            If isOpening = True Then
                strtempBaseQry += " and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  <'" + strfromdate + "' "
            Else
                strtempBaseQry += " and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  >='" + strfromdate + "' and  convert(date,TSPL_REMITTANCE.Document_Date ,103)  <='" + strtodate + "'"
            End If

            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and TSPL_REMITTANCE.Vendor_Code in (" & strvendor & ")"
            End If
            strtempBaseQry += " UNION ALL" + Environment.NewLine &
            " select '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name ,TSPL_REMITTANCE.Document_No ,'TDS REVERSE' as [DocType],convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)as Document_Date,'', '', TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end AS CrAmt, 0 as DrAmt, 0 as Purchase, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS*-1 else 0 end as Payments, 0 as DrNote, 0 as CrNote, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No inner join TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P' and Branch_GL_AC  is not null and Actual_Total_TDS<>0 and TSPL_REMITTANCE.Is_TDS_Provision = 'N' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   AND 1=2" + Environment.NewLine

            If isOpening = True Then
                strtempBaseQry += "  and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  <'" + strfromdate + "' "
            Else
                strtempBaseQry += "  and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  >='" + strfromdate + "' and  convert(date,TSPL_REMITTANCE.Document_Date ,103)  <='" + strtodate + "' "
            End If

            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and TSPL_REMITTANCE.Vendor_Code in (" & strvendor & ")"
            End If
            strtempBaseQry += " UNION ALL" + Environment.NewLine
            If objCommonVar.IsMultiCurrencyCompany = True Then
                strtempBaseQry += "select '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate,"

                '     strtempBaseQry += " case when TSPL_PAYMENT_HEADER.Payment_Type IN ('RC','AD') then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then  TSPL_PAYMENT_DETAIL.Applied_Amount * -1 else  TSPL_PAYMENT_DETAIL.Applied_Amount  end ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, " &
                '"case when TSPL_PAYMENT_HEADER.Payment_Type IN ('AD') then case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then  TSPL_PAYMENT_DETAIL.Applied_Amount * -1 else  TSPL_PAYMENT_DETAIL.Applied_Amount  end WHEN TSPL_PAYMENT_HEADER.Payment_Type IN ('RC') then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type IN('OA','AV') then -1*TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then -1*TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then case when isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code,'')='' then TSPL_PAYMENT_HEADER.Location_GL_Code else TSPL_VENDOR_INVOICE_HEAD.Loc_Code end Else CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type='AD' THEN (sELECT Location_GL_Code  FROM TSPL_PAYMENT_HEADER PR WHERE PR.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment )  ELSE substring(TSPL_PAYMENT_HEADER.Debit_Account , len(TSPL_PAYMENT_HEADER.Debit_Account )-2,3) END End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE" &
                '" LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No" &
                '" LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No" &
                '" LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No" &
                '" left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code" &
                '" where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  " & IIf(chkIncludeApplyDocument.Checked = False, " and TSPL_PAYMENT_HEADER.Payment_Type<>'AD' ", "") & " " + Environment.NewLine

                strtempBaseQry += " case when TSPL_PAYMENT_HEADER.Payment_Type IN ('RC') then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then  TSPL_PAYMENT_DETAIL.Applied_Amount * -1 else  TSPL_PAYMENT_DETAIL.Applied_Amount  end when TSPL_PAYMENT_HEADER.Payment_Type='AD' then case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then  TSPL_PAYMENT_DETAIL.Applied_Amount ELSE 0  end ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, " &
           "case when TSPL_PAYMENT_HEADER.Payment_Type IN ('AD') then case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else  TSPL_PAYMENT_DETAIL.Applied_Amount  end WHEN TSPL_PAYMENT_HEADER.Payment_Type IN ('RC') then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type IN('OA','AV') then -1*TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then -1*TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then case when isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code,'')='' then TSPL_PAYMENT_HEADER.Location_GL_Code else TSPL_VENDOR_INVOICE_HEAD.Loc_Code end Else CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type='AD' THEN CASE WHEN ISNULL((sELECT Location_GL_Code  FROM TSPL_PAYMENT_HEADER PR WHERE PR.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment ),'') ='' then  
 TSPL_VENDOR_INVOICE_HEAD.Loc_Code else  (sELECT Location_GL_Code  FROM TSPL_PAYMENT_HEADER PR WHERE PR.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment ) end  ELSE substring(TSPL_PAYMENT_HEADER.Debit_Account , len(TSPL_PAYMENT_HEADER.Debit_Account )-2,3) END End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE" &
           " LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No" &
           " LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No" &
           " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No" &
           " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code" &
           " where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  " & IIf(chkIncludeApplyDocument.Checked = False, " and TSPL_PAYMENT_HEADER.Payment_Type<>'AD' ", "") & " " + Environment.NewLine


                If isOpening = True Then
                    strtempBaseQry += " and  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103) <'" + strfromdate + "'  "
                Else
                    strtempBaseQry += " and  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date  ,103)  <='" + strtodate + "' "
                End If

                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and TSPL_BANK_REVERSE.vendor_code in (" & strvendor & ")"
                End If
                strtempBaseQry += " ----------- For bank reverse entry--------------- " + Environment.NewLine
            Else
                strtempBaseQry += " select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount ELSE -1*TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   " + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += " and  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103)  <'" + strfromdate + "'  "
                Else
                    strtempBaseQry += " and  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date  ,103)  <='" + strtodate + "' "
                End If

                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and TSPL_BANK_REVERSE.vendor_code in (" & strvendor & ")"
                End If
            End If
            strtempBaseQry += " UNION ALL" + Environment.NewLine &
            " select   " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, VC_Code as VCode, VC_Name as VName, TSPL_VCGL_Head.Document_No as DocNo,case when TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end as DocType,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, (case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt, 0 as Purchase, 0 as Payments, case when Amount_Type='Cr' then Amount else 0 end as DrNote, case when Amount_Type='Dr' then Amount else 0 end as CrNote, (case when  TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end ) as Document_Type ,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date,'VC-GL' as GLDocType,TSPL_VCGL_Head.Comp_Code from TSPL_VCGL_Head LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where TSPL_VCGL_Head.Document_Type='v' and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''  " + Environment.NewLine

            If isOpening = True Then
                strtempBaseQry += " and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  <'" + strfromdate + "'"
            Else
                strtempBaseQry += " and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + strtodate + "'"
            End If

            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and VC_Code in (" & strvendor & ")"
            End If
            strtempBaseQry += " UNION ALL" + Environment.NewLine &
            " select    " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_VCGL_Detail.VCGL_Code as VCode, TSPL_VCGL_Detail.VCGL_Name as VName, TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Detail.Row_Type as DocType,CONVERT(date,TSPL_VCGL_Head.Document_Date,103) as DocDate,TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt, 0 as Purchase, 0 as Payments, TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as CrNote, TSPL_VCGL_Detail.Row_Type as Document_Type,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date ,'VC-GL' as GLDocType, TSPL_VCGL_Head.Comp_Code from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''" + Environment.NewLine

            If isOpening = True Then
                strtempBaseQry += " and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  <'" + strfromdate + "'"
            Else
                strtempBaseQry += " and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + strtodate + "'"
            End If

            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and VC_Code in (" & strvendor & ")"
            End If
            strtempBaseQry += " UNION ALL" + Environment.NewLine
            strtempBaseQry += "  select    " & strShowReferenceDocNoofAPInvoice & "  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END as CrAmt, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END  as DrAmt, 0 as Purchase, 0 as Payments, TSPL_Payment_Adjustment_Header.Adjustment_Amount as DrNote, 0 as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Header  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='P'  " + Environment.NewLine

            If isOpening = True Then
                strtempBaseQry += " and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103) <'" + strfromdate + "'  "
            Else
                strtempBaseQry += " and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103)  <='" + strtodate + "' "
            End If

            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and TSPL_Payment_Adjustment_Header.Vendor_No in (" & strvendor & ")"
            End If
            strtempBaseQry += " UNION ALL" + Environment.NewLine
            strtempBaseQry += "  select    " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as CrAmt,  CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, TSPL_Payment_Adjustment_Detail.Amount as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Detail  left outer join  TSPL_Payment_Adjustment_Header on TSPL_Payment_Adjustment_Header.Adjustment_No =TSPL_Payment_Adjustment_Detail .Adjustment_No  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='R'  " + Environment.NewLine

            If isOpening = True Then
                strtempBaseQry += " and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  <'" + strfromdate + "' "
            Else
                strtempBaseQry += " and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103)  <='" + strtodate + "'"
            End If

            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and TSPL_Payment_Adjustment_Header.Vendor_No in (" & strvendor & ")"
            End If
            strtempBaseQry += " UNION ALL" + Environment.NewLine &
            " Select max(Reference_Doc_No) as Reference_Doc_No,max(Emp_type ) as Emp_type,max(Emp_Adv_Type) as Emp_Adv_Type ,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (" + Environment.NewLine &
            " select  '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then case when isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code,'')='' then TSPL_PAYMENT_HEADER.Location_GL_Code else TSPL_VENDOR_INVOICE_HEAD.Loc_Code end When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   " + Environment.NewLine

            If isOpening = True Then
                strtempBaseQry += "  and  convert(date,TSPL_PAYMENT_HEADER.payment_date ,103)  <'" + strfromdate + "'  "
            Else
                strtempBaseQry += "  and  convert(date,TSPL_PAYMENT_HEADER.payment_date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_PAYMENT_HEADER.payment_date,103)  <='" + strtodate + "' "
            End If


            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and tspl_payment_header.vendor_code in (" & strvendor & ")"
            End If
            strtempBaseQry += " ) XX Group By XX.account, XX.DocNo"
            If (rbtnDocWise.IsChecked = False) Then

                strtempBaseQry += " UNION ALL" + Environment.NewLine &
    " select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName,TSPL_PAYMENT_HEADER.Payment_No as DocNo,'EXC' as [DocType],convert(date,Payment_Date, 103) as DocDate, case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(TSPL_PAYMENT_HEADER.cheque_No+'-'+ CONVERT(VARCHAR,Cheque_Date , 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, 0 as Balance_Amount,isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_PAYMENT_HEADER.Comp_Code from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_Vendor_Master ON TSPL_Vendor_Master.Vendor_Code =TSPL_PAYMENT_HEADER.Vendor_Code " + Environment.NewLine &
    " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code" + Environment.NewLine &
    " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_PAYMENT_HEADER.Payment_No " + Environment.NewLine &
    " LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 " + Environment.NewLine &
    " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No  " + Environment.NewLine &
    " where TSPL_PAYMENT_HEADER.Posted=1 and  TSPL_PAYMENT_HEADER.Payment_Type  in ('PY','AD') " + Environment.NewLine &
    " AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1 and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0)  " + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  <'" + strfromdate + "' "
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + strtodate + "'"
                End If

                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and TSPL_VENDOR_MASTER.Vendor_Code in (" & strvendor & ")"
                End If
                strtempBaseQry += " ---------------------to find gain or loss amount for payment---------------" + Environment.NewLine &
    " UNION ALL" + Environment.NewLine &
    " Select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN,TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, TSPL_BANK_REVERSE.Reverse_Code as DocNo," + Environment.NewLine &
    " 'EXC'  as DocType  ,convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) as DocDate,case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security ,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_PAYMENT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+ convert(varchar ,TSPL_PAYMENT_HEADER . Cheque_Date ,103)else '' end)) as ChequeDetails, TSPL_PAYMENT_HEADER.Currency_Code, 1 as ConvRate,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT as DrAmt,  0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote," + Environment.NewLine &
    " 'RV',0, isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType,TSPL_PAYMENT_HEADER.Comp_Code " + Environment.NewLine &
    " from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_VENDOR_MASTER  ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code   " + Environment.NewLine &
    " LEFT OUTER JOIN TSPL_Vendor_TYPE_MASTER  ON TSPL_Vendor_TYPE_MASTER.Ven_Type_Code  = TSPL_VENDOR_MASTER.Ven_Type_Code   " + Environment.NewLine &
    " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  " + Environment.NewLine &
    " LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.PAYMENT_NO =TSPL_BANK_REVERSE.Document_No " + Environment.NewLine &
    " LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 " + Environment.NewLine &
    " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No " + Environment.NewLine &
    " where  TSPL_BANK_REVERSE.Reverse_Document='Payments' AND TSPL_BANK_REVERSE.Post ='P'" + Environment.NewLine &
    " and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0) " + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += "and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  <'" + strfromdate + "' "
                Else
                    strtempBaseQry += "and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + strtodate + "'"
                End If

                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and TSPL_VENDOR_MASTER.Vendor_Code in (" & strvendor & ")"
                End If
                strtempBaseQry += " ----------------------to find gain or loss amount FOR BANK REVERSE ---------------" + Environment.NewLine

            End If

            If chkIncludeApplyDocument.Checked Then
                strtempBaseQry += " UNION ALL" + Environment.NewLine &
                " select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type, NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'IM' else null end as DocType , COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then 0 else TSPL_PAYMENT_DETAIL.Applied_Amount  end AS CrAmt , case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then TSPL_PAYMENT_DETAIL.Applied_Amount  else 0  end AS  DrAmt ,0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote," + Environment.NewLine &
                " TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount,  CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type='AD' THEN substring(TSPL_PAYMENT_HEADER.Debit_Account , len(TSPL_PAYMENT_HEADER.Debit_Account )-2,3)  ELSE  TSPL_VENDOR_INVOICE_HEAD.Loc_Code END as account,TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code " + Environment.NewLine &
                " from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type='AD' AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  " + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += "   and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)<'" + strfromdate + "'  "
                Else
                    strtempBaseQry += "   and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + strtodate + "' "
                End If
                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and tspl_payment_header.vendor_code in (" & strvendor & ")"
                End If
                'strtempBaseQry += " --------------- INVOICE AGAINST APPLY DOCUMENT" + Environment.NewLine &
                '" UNION ALL" + Environment.NewLine &
                '" SELECT max(Reference_Doc_No) as Reference_Doc_No, max(Emp_type ) as Emp_type,max(Emp_Adv_Type) as Emp_Adv_Type ,NULL AS Due_Date,'' AS PO_SRN,  MAX(VCode) AS VCode,MAX(VName) as VName,DocNo AS DocNo,MAX(DocType) AS DocType,MAX(DocDate) AS DocDate,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(CrAmt) AS CrAmt,SUM(DrAmt) AS DrAmt,SUM(Purchase) AS Purchase,SUM(Payments) AS Payments  ,MAX(DrNote) AS DrNote,MAX(CrNote) AS CrNote, MAX(Document_Type) AS Document_Type,SUM(Balance_Amount) AS Balance_Amount,account , MAX(Posting_Date) AS Posting_Date,MAX(GLDocType) AS GLDocType,MAX(Comp_Code) AS Comp_Code FROM (select  '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'IM' else null end as DocType , COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr,((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then TSPL_PAYMENT_DETAIL.Applied_Amount  else 0  end  AS CrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then 0 else TSPL_PAYMENT_DETAIL.Applied_Amount  end AS  DrAmt, " + Environment.NewLine &
                '" 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, " + Environment.NewLine &
                '" TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account,TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code  " + Environment.NewLine &
                '" from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type='AD' AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  " + Environment.NewLine

                strtempBaseQry += " --------------- INVOICE AGAINST APPLY DOCUMENT" + Environment.NewLine &
                " UNION ALL" + Environment.NewLine &
                " SELECT max(Reference_Doc_No) as Reference_Doc_No, max(Emp_type ) as Emp_type,max(Emp_Adv_Type) as Emp_Adv_Type ,NULL AS Due_Date,'' AS PO_SRN,  MAX(VCode) AS VCode,MAX(VName) as VName,DocNo AS DocNo,MAX(DocType) AS DocType,MAX(DocDate) AS DocDate,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(CrAmt) AS CrAmt,SUM(DrAmt) AS DrAmt,SUM(Purchase) AS Purchase,SUM(Payments) AS Payments  ,MAX(DrNote) AS DrNote,MAX(CrNote) AS CrNote, MAX(Document_Type) AS Document_Type,SUM(Balance_Amount) AS Balance_Amount,account , MAX(Posting_Date) AS Posting_Date,MAX(GLDocType) AS GLDocType,MAX(Comp_Code) AS Comp_Code FROM (select  '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'IM' else null end as DocType , COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr,((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then TSPL_PAYMENT_DETAIL.Applied_Amount  else 0  end  AS CrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then 0 else TSPL_PAYMENT_DETAIL.Applied_Amount  end AS  DrAmt, " + Environment.NewLine &
                " 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, " + Environment.NewLine &
                " CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type='AD' THEN substring(TSPL_PAYMENT_HEADER.Debit_Account , len(TSPL_PAYMENT_HEADER.Debit_Account )-2,3)  ELSE  TSPL_VENDOR_INVOICE_HEAD.Loc_Code END as account,TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code  " + Environment.NewLine &
                " from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type='AD' AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  " + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) <'" + strfromdate + "' "
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  >='" + strfromdate + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + strtodate + "' "
                End If
                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and tspl_payment_header.vendor_code in (" & strvendor & ")"
                End If
                strtempBaseQry += " )INV  GROUP BY  DocNo,account  " + Environment.NewLine &
                " ------- APPLY DOCUMENT ENTRY WHEN LOCATION IS CHANGED OF APPLIED DOCUMENT WITH DOCUMENT LOCATION " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
                " SELECT  max(Reference_Doc_No) as Reference_Doc_No,max(Emp_type ) as Emp_type,max(Emp_Adv_Type) as Emp_Adv_Type ,NULL AS Due_Date,'' AS PO_SRN,  MAX(VCode) AS VCode,MAX(VName) as VName,DocNo AS DocNo,MAX(DocType) AS DocType,MAX(DocDate) AS DocDate,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(CrAmt) AS CrAmt,SUM(DrAmt) AS DrAmt,SUM(Purchase) AS Purchase,SUM(Payments) AS Payments  ,MAX(DrNote) AS DrNote,MAX(CrNote) AS CrNote, MAX(Document_Type) AS Document_Type,SUM(Balance_Amount) AS Balance_Amount,account , MAX(Posting_Date) AS Posting_Date,MAX(GLDocType) AS GLDocType,MAX(Comp_Code) AS Comp_Code FROM ( " + Environment.NewLine &
                " select '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,'Reverse Payment' as DocType , Convert(date,TSPL_BANK_REVERSE.Reversal_Date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr,((TSPL_PAYMENT_HEADER.Cheque_No) + (case when TSPL_PAYMENT_HEADER.Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then 0 else TSPL_PAYMENT_DETAIL.Applied_Amount  end  AS CrAmt,Case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then TSPL_PAYMENT_DETAIL.Applied_Amount  else 0  end AS  DrAmt, " + Environment.NewLine &
                " 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount,  " + Environment.NewLine &
                " TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account,TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code  " + Environment.NewLine &
                " from TSPL_BANK_REVERSE LEFT OUTER JOIN tspl_payment_header ON tspl_payment_header.Payment_No =TSPL_BANK_REVERSE.Document_No  LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where" + Environment.NewLine &
                " TSPL_BANK_REVERSE.Reverse_Document='Payments' AND TSPL_BANK_REVERSE.Post ='P' AND tspl_payment_header.Payment_Type='AD' AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1" + Environment.NewLine

                If isOpening = True Then
                    strtempBaseQry += "  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <'" + strfromdate + "'  "
                Else
                    strtempBaseQry += "  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <='" + strtodate + "' "
                End If
                If clsCommon.myLen(strvendor) > 0 Then
                    strtempBaseQry += " and tspl_payment_header.vendor_code in (" & strvendor & ")"
                End If
                strtempBaseQry += " )INV  GROUP BY  DocNo,account " + Environment.NewLine &
                " -------------- FOR BANK REVERSE ENTRY WHEN LOCATION IS CHANGED OF APPLIED DOCUMENT WITH DOCUMENT LOCATION" + Environment.NewLine
            End If
            ''  for only salary against advance invoices
            'If IsOnlyForAgainstSalary = True Then
            '    strtempBaseQry += " UNION ALL" + Environment.NewLine & _
            '              " Select  max(FinalQryForAdvance.Emp_type) as Emp_type,max(FinalQryForAdvance.Emp_Adv_Type) as Emp_Adv_Type,null as Due_Date ,max(FinalQryForAdvance.PO_SRN) as PO_SRN, max(FinalQryForAdvance.VCode) as VCode,max(FinalQryForAdvance.VName) as VName,FinalQryForAdvance.DocNo,max(FinalQryForAdvance.DocType) as DocType,max(FinalQryForAdvance.DocDate) as DocDate, max(FinalQryForAdvance.DocNarr) as DocNarr, max(FinalQryForAdvance.ChequeDetails) as ChequeDetails, max(FinalQryForAdvance.CURRENCY_CODE) as CURRENCY_CODE, max(FinalQryForAdvance.ConvRate) as ConvRate,sum(CrAmt) as CrAmt,sum(DrAmt) as DrAmt, sum(Purchase) as Purchase, sum(Payments) as Payments, sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(FinalQryForAdvance.Document_Type) as Document_Type ,0 as Balance_Amount, max(FinalQryForAdvance.account) as account " + Environment.NewLine & _
            '              " , max(FinalQryForAdvance.Posting_Date) as Posting_Date,max(FinalQryForAdvance.GLDocType) as GLDocType,max(FinalQryForAdvance.Comp_Code) as Comp_Code " + Environment.NewLine & _
            '              " from (select TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, '' AS Emp_type, 'AAS' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  as VCode, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'AAS' as DocType,CONVERT(Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as DocDate, TSPL_VENDOR_INVOICE_HEAD.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate,case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as CrAmt,case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end  as DrAmt, 0 as Purchase, 0 as Payments, case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end  as DrNote, case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type as Document_Type ,0 as Balance_Amount, (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ) as account, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Posting_Date,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType,TSPL_VENDOR_INVOICE_HEAD.Comp_Code from TSPL_VENDOR_INVOICE_DETAIL  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No " + Environment.NewLine & _
            '              " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
            '              " left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Group_Code" + Environment.NewLine & _
            '              " left outer join  TSPL_GL_ACCOUNTS AS GL1 ON GL1.account_code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code" + Environment.NewLine & _
            '              " left outer join  TSPL_GL_ACCOUNTS AS GL2 ON GL2.account_code=TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary " + Environment.NewLine & _
            '              " where  ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''   " + Environment.NewLine
            '    If isOpening = True Then
            '        strtempBaseQry += "  and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103) <'" + strfromdate + "' "
            '    Else
            '        strtempBaseQry += "  and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103)  >='" + strfromdate + "' and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103)  <='" + strtodate + "' "
            '    End If
            '    If clsCommon.myLen(strvendor) > 0 Then
            '        strtempBaseQry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" & strvendor & ")"
            '    End If
            '    strtempBaseQry += " and TSPL_VENDOR_MASTER.Vendor_Group_Code  ='EMPLOYEES' and GL1.Account_seg_code1=GL2.Account_seg_code1) FinalQryForAdvance" + Environment.NewLine & _
            '    " group by FinalQryForAdvance.DocNo,FinalQryForAdvance.GL_Account_Code"
            'End If
            '' changes done by richa 27 Mar,2018
            'If IsOnlyForAgainstSalary = True Then
            strtempBaseQry += " UNION ALL" + Environment.NewLine &
                      " Select  max(FinalQryForAdvance.Reference_Doc_No) as Reference_Doc_No,max(FinalQryForAdvance.Emp_type) as Emp_type,max(FinalQryForAdvance.Emp_Adv_Type) as Emp_Adv_Type,null as Due_Date ,max(FinalQryForAdvance.PO_SRN) as PO_SRN, max(FinalQryForAdvance.VCode) as VCode,max(FinalQryForAdvance.VName) as VName,FinalQryForAdvance.DocNo,max(FinalQryForAdvance.DocType) as DocType,max(FinalQryForAdvance.DocDate) as DocDate, max(FinalQryForAdvance.DocNarr) as DocNarr, max(FinalQryForAdvance.ChequeDetails) as ChequeDetails, max(FinalQryForAdvance.CURRENCY_CODE) as CURRENCY_CODE, max(FinalQryForAdvance.ConvRate) as ConvRate,sum(CrAmt) as CrAmt,sum(DrAmt) as DrAmt, sum(Purchase) as Purchase, sum(Payments) as Payments, sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(FinalQryForAdvance.Document_Type) as Document_Type ,0 as Balance_Amount, max(FinalQryForAdvance.account) as account " + Environment.NewLine &
                      " , max(FinalQryForAdvance.Posting_Date) as Posting_Date,max(FinalQryForAdvance.GLDocType) as GLDocType,max(FinalQryForAdvance.Comp_Code) as Comp_Code " + Environment.NewLine &
                      " from (select " & strShowReferenceDocNoofAPInvoice & " TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, '' AS Emp_type, 'AAS' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  as VCode, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'AAS' as DocType,CONVERT(Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as DocDate, TSPL_VENDOR_INVOICE_HEAD.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate,case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrAmt,case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as DrAmt, 0 as Purchase, 0 as Payments, case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end  as DrNote, case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type as Document_Type ,0 as Balance_Amount, (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ) as account, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Posting_Date,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType,TSPL_VENDOR_INVOICE_HEAD.Comp_Code from TSPL_VENDOR_INVOICE_DETAIL  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No " + Environment.NewLine &
                      " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine &
                      " left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Group_Code" + Environment.NewLine &
                      " left outer join  TSPL_GL_ACCOUNTS AS GL1 ON GL1.account_code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code" + Environment.NewLine &
                      " left outer join  TSPL_GL_ACCOUNTS AS GL2 ON GL2.account_code=TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary " + Environment.NewLine &
                      " where  ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''   " + Environment.NewLine
            If isOpening = True Then
                strtempBaseQry += "  and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103) <'" + strfromdate + "' "
            Else
                strtempBaseQry += "  and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103)  >='" + strfromdate + "' and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103)  <='" + strtodate + "' "
            End If
            If clsCommon.myLen(strvendor) > 0 Then
                strtempBaseQry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" & strvendor & ")"
            End If
            strtempBaseQry += " and TSPL_VENDOR_MASTER.Vendor_Group_Code  ='EMPLOYEES' and GL1.Account_seg_code1=GL2.Account_seg_code1) FinalQryForAdvance" + Environment.NewLine &
            " group by FinalQryForAdvance.DocNo,FinalQryForAdvance.GL_Account_Code"
            'End If


            ''richa 24/10/2017  Ticket No : KDI/08/01/19-000448 by Prabhakar add   Reference_Doc_No in tspl_payment_header table below code
            'If IsOnlyForAgainstSalary = True Then
            strtempBaseQry += " UNION ALL" + Environment.NewLine &
             "Select max(Reference_Doc_No) as Reference_Doc_No, MAX(Emp_type) as Emp_type, 'S' as Emp_Adv_Type,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (" + Environment.NewLine &
            " select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   " + Environment.NewLine &
            " ) XX Group By XX.account, XX.DocNo" + Environment.NewLine &
            " UNION ALL" + Environment.NewLine &
            " select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type  ,'S') as Emp_Adv_Type,TSPL_PAYMENT_HEADER.Payment_Date As Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, TSPL_PAYMENT_HEADER.Entry_Desc as DocNarr, '', TSPL_PAYMENT_HEADER.CURRENCY_CODE as Currency_Code, TSPL_PAYMENT_HEADER.ConvRate as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else 0 END as DrAmt, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else Actual_Total_TDS END as Payments, 0 as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 and TSPL_REMITTANCE.Is_TDS_Provision = 'N' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1" + Environment.NewLine &
            " UNION ALL" + Environment.NewLine &
            " select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'S') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE TSPL_BANK_REVERSE.Amount*-1 end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  "
            'End If
            ''---------------
            'Else
            '    '    strtempBaseQry = "Select NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (" + Environment.NewLine & _
            '    '    " select  NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   " + Environment.NewLine & _
            '    '    " ) XX Group By XX.account, XX.DocNo" + Environment.NewLine & _
            '    '" UNION ALL" + Environment.NewLine & _
            '    '    " select TSPL_PAYMENT_HEADER.Payment_Date As Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, TSPL_PAYMENT_HEADER.Entry_Desc as DocNarr, '', TSPL_PAYMENT_HEADER.CURRENCY_CODE as Currency_Code, TSPL_PAYMENT_HEADER.ConvRate as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else 0 END as DrAmt, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else Actual_Total_TDS END as Payments, 0 as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1" + Environment.NewLine & _
            '    '" UNION ALL" + Environment.NewLine & _
            '    '    " select null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE TSPL_BANK_REVERSE.Amount*-1 end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  "

            'End If
            strtempBaseQryforopening = strtempBaseQry
            '  Dim strBaseQryforVendor As String = String.Empty
            ''BM00000008527
            If clsCommon.CompairString(clsCommon.myCstr(ddlCurrencyType.SelectedValue), "") = CompairStringResult.Equal Then
                LoadCurrencyType()
            End If
            If (rbtnDocWise.IsChecked = True) Then

                If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                    strtempBaseQry = "  Select InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  * (case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end) as  CrAmt ," + Environment.NewLine &
                    " case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine &
                    "case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then " + Environment.NewLine &
                    "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else " + Environment.NewLine &
                    "case when isnull((DrAmt),0)>0 then (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else 0 end end else " + Environment.NewLine &
                    " case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT ),0) else  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else " + Environment.NewLine &
                    " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then " + Environment.NewLine &
                    "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end else " + Environment.NewLine &
                    "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)  else  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  end end end as DrAmt, " + Environment.NewLine &
                    " case when InnQuery.DocType='Pur.Invoice' then InnQuery.CrAmt-InnQuery.DrAmt else 0 end as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  (" & strtempBaseQry & " ) InnQuery " + Environment.NewLine &
                    "  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_M1ASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code  "
                Else


                    strtempBaseQry = "  Select isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) as EXCHANGE_GAIN_AMT,isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT ),0) as EXCHANGE_LOSS_AMT, InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  * (case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end) - case when (DocType)='Reverse Payment' then  (Select isnull((PH.EXCHANGE_LOSS_AMT  ),0) from TSPL_PAYMENT_HEADER PH where PH.Payment_No =(  Select Document_No   from TSPL_BANK_REVERSE where Reverse_Code =InnQuery.DocNo )) else 0 end as  CrAmt ," + Environment.NewLine &
                   " case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine &
                   "case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then " + Environment.NewLine &
                   "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else " + Environment.NewLine &
                   "case when isnull((DrAmt),0)>0 then (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else 0 end end else " + Environment.NewLine &
                   " case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT ),0) ELSE  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else " + Environment.NewLine &
                   " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then " + Environment.NewLine &
                   "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then CASE WHEN ((DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")-isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0))<0 THEN 0 ELSE (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")-isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) END  else CASE WHEN (DocType)<>'IM' THEN (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) ELSE (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") END end else " + Environment.NewLine &
                   "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)  else  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  end end end as DrAmt, " + Environment.NewLine &
                   " InnQuery.CrAmt-InnQuery.DrAmt  as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  (" & strtempBaseQry & " ) InnQuery " + Environment.NewLine &
                   "  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code  "


                End If

            Else
                ''richa to exclude exchange gain loss documnets for only apply documnets when include apply doc.checkbox is off  KDI/14/06/2018-000364
                Dim strExcludeEXcforApplyDocumnets As String = " where InnQuery.DocNo not in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)  "
                If isOpening = True Then
                    strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strvendor) > 0 Then
                    strExcludeEXcforApplyDocumnets += " and TSPL_PAYMENT_HEADER.Vendor_Code in (" & strvendor & ")"
                End If
                strExcludeEXcforApplyDocumnets += Environment.NewLine & " Union All" & Environment.NewLine &
                " Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)  "
                If isOpening = True Then
                    strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
                Else
                    strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + strtodate + "' " + Environment.NewLine
                End If
                If clsCommon.myLen(strvendor) > 0 Then
                    strExcludeEXcforApplyDocumnets += " and TSPL_PAYMENT_HEADER.Vendor_Code in (" & strvendor & ")"
                End If

                strExcludeEXcforApplyDocumnets += " ) ) "

                '----------------------


                strtempBaseQry = "  Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate,  "
                If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                    strtempBaseQry += " CONVERT(DECIMAL(18,2), CASE WHEN InnQuery.DocType NOT IN ('Pur.Invoice','Receipt')  THEN case when InnQuery.DocType not in ('TDS','AP Invoice') THEN InnQuery.CrAmt  *(case when (DocType) NOT IN ('EXC','Credit Note','IM','Reverse Payment') then   InnQuery.ConvRate else 1 end) when  InnQuery.DocType in ('AP Invoice') and (Select Document_Type  from tspl_vendor_invoice_head where document_no=InnQuery.DocNo)='C' then InnQuery.CrAmt ELSE CASE WHEN InnQuery.DocType in ('TDS') and (Select ISNULL(TSPL_REMITTANCE.Document_Type,'')  from TSPL_REMITTANCE LEFT OUTER join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_REMITTANCE.Document_No  and  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' where TSPL_REMITTANCE.Document_No=InnQuery.DocNo  )='I' or (Select ISNULL(TSPL_REMITTANCE.Document_Type,'')  from TSPL_REMITTANCE LEFT OUTER join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_REMITTANCE.Document_No  and  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' where TSPL_REMITTANCE.Document_No=InnQuery.DocNo   )='C' then (Select ISNULL (Actual_Total_TDS ,0)  from TSPL_REMITTANCE where TSPL_REMITTANCE.Document_No=InnQuery.DocNo  ) * -1 else 0 end END ELSE 0 END) as CrAmt ," &
                      " CONVERT(DECIMAL(18,2),case when InnQuery.DocType not in ('Payment','On Account','Advance','Receipt','Adjustment') then case when InnQuery.DocType not in ('AP Invoice','TDS') then InnQuery.DrAmt WHEN InnQuery.DocType in ('TDS') and   (Select ISNULL(TSPL_REMITTANCE.Document_Type,'')  from TSPL_REMITTANCE LEFT OUTER join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_REMITTANCE.Document_No  and  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' where TSPL_REMITTANCE.Document_No=InnQuery.DocNo  )='D' then (Select ISNULL (Actual_Total_TDS ,0)  from TSPL_REMITTANCE where TSPL_REMITTANCE.Document_No=InnQuery.DocNo  ) * -1 else case when  InnQuery.DocType in ('AP Invoice') AND  (Select Document_Type  from tspl_vendor_invoice_head where document_no=InnQuery.DocNo AND ISNULL(Against_PurchaseReturn_No,'')='')='D' then InnQuery.DrAmt else 0 end end else 0 end) DrAmt, " &
                      " CONVERT(DECIMAL(18,2),case when InnQuery.DocType not in ('Payment','On Account','Advance','Receipt','Debit Note','Reverse Payment','Credit Note','EXC','IM') then case when InnQuery.DocType not in ('AP Invoice','TDS') then CONVERT(DECIMAL(18,2),InnQuery.Purchase) else case when  InnQuery.DocType in ('AP Invoice') and (Select Document_Type  from tspl_vendor_invoice_head where document_no=InnQuery.DocNo)='I'   then CONVERT(DECIMAL(18,2),InnQuery.Purchase) when  InnQuery.DocType in ('AP Invoice')  and (Select Document_Type  from tspl_vendor_invoice_head where document_no=InnQuery.DocNo AND ISNULL(Against_PurchaseReturn_No,'')<>'')='D' then CONVERT(DECIMAL(18,2),InnQuery.Purchase) *-1  else 0 end end else 0 end) Purchase, " &
                      " CONVERT(DECIMAL(18,2), case when InnQuery.DocType  in ('Payment','On Account','Advance','Receipt','TDS','EXC','IM') then InnQuery.Payments * (case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end) else 0 end) as Payments , "

                Else
                    strtempBaseQry += " CONVERT(DECIMAL(18,2),InnQuery.CrAmt) AS CrAmt,CONVERT(DECIMAL(18,2),InnQuery.DrAmt) AS DrAmt,CONVERT(DECIMAL(18,2),InnQuery.Purchase) AS Purchase,CONVERT(DECIMAL(18,2),InnQuery.Payments) AS Payments , "
                End If

                strtempBaseQry += " InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code from " &
               "(Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  *(case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end)  as  CrAmt ," + Environment.NewLine &
               " case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine &
               "case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then " + Environment.NewLine &
               "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else " + Environment.NewLine &
               "case when isnull((DrAmt),0)>0 AND (DocType)<>'EXC' then (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else isnull((DrAmt),0) end end else " + Environment.NewLine &
               "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  else case when (DocType)<>'EXC' then (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") else CASE WHEN  isnull((DrAmt),0)>0 THEN isnull(DrAmt,0) ELSE isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  ),0) END end  end end else " + Environment.NewLine &
               "case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then " + Environment.NewLine &
               "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND  (DocType)<>'EXC' then   (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  else case when (DocType)<>'EXC' then  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  else isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else " + Environment.NewLine &
               "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  else  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  end end end as DrAmt, " + Environment.NewLine &
               " CONVERT(DECIMAL(18,2),InnQuery.CrAmt-InnQuery.DrAmt) as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  (" & strtempBaseQryforopening & " ) InnQuery " + Environment.NewLine &
               "  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code " & IIf(chkIncludeApplyDocument.Checked = False, " " & strExcludeEXcforApplyDocumnets & " ", "") & "  ) InnQuery left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =InnQuery.VCode left outer join TSPL_VENDOR_INVOICE_HEAD on  TSPL_VENDOR_INVOICE_HEAD.Document_No =InnQuery.DocNo  "

                ''richa KDI/15/05/19-000453,KDI/15/05/19-000452
                If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then

                    strtempBaseQryforopeningForMIS = "  Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode,TSPL_VENDOR_MASTER.Vendor_Name as VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate,  " &
                  "  CONVERT(DECIMAL(18,2),InnQuery.CrAmt) AS CrAmt,CONVERT(DECIMAL(18,2),InnQuery.DrAmt) AS DrAmt,0 as Purchase,0 as Payments, " &
                  " InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code from " &
                  " (Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  *(case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end) as  CrAmt ," + Environment.NewLine &
                  " case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine &
                  " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then " + Environment.NewLine &
                  " case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else " + Environment.NewLine &
                  " case when isnull((DrAmt),0)>0 AND (DocType)<>'EXC' then (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else isnull((DrAmt),0) end end else " + Environment.NewLine &
                  " case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  else case when (DocType)<>'EXC' then (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") else CASE WHEN  isnull((DrAmt),0)>0 THEN isnull(DrAmt,0) ELSE isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  ),0) END end  end end else " + Environment.NewLine &
                  " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then " + Environment.NewLine &
                  "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND  (DocType)<>'EXC' then   (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  else case when (DocType)<>'EXC' then  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  else isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else " + Environment.NewLine &
                  "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  else  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  end end end as DrAmt, " + Environment.NewLine &
                  " CONVERT(DECIMAL(18,2),InnQuery.CrAmt-InnQuery.DrAmt) as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  (" & strtempBaseQryforopening & " ) InnQuery " + Environment.NewLine &
                  "  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code " & IIf(chkIncludeApplyDocument.Checked = False, " " & strExcludeEXcforApplyDocumnets & " ", "") & " ) InnQuery left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =InnQuery.VCode left outer join TSPL_VENDOR_INVOICE_HEAD on  TSPL_VENDOR_INVOICE_HEAD.Document_No =InnQuery.DocNo  "


                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strtempBaseQry

    End Function


    Private Sub FormatgvVendorGroup()
        Try
            gvVendorGroup.AllowAddNewRow = False
            gvVendorGroup.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gvVendorGroup.Columns.Count - 1
                gvVendorGroup.Columns(ii).ReadOnly = True
                gvVendorGroup.Columns(ii).IsVisible = False
            Next
            gvVendorGroup.Columns("Vendor_Group_Code").IsVisible = True
            gvVendorGroup.Columns("Vendor_Group_Code").Width = 180
            gvVendorGroup.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            gvVendorGroup.Columns("Vendor_Group_Desc").IsVisible = True
            gvVendorGroup.Columns("Vendor_Group_Desc").Width = 350
            gvVendorGroup.Columns("Vendor_Group_Desc").HeaderText = "Vendor Group Desc"

            gvVendorGroup.Columns("AccountSet").IsVisible = True
            gvVendorGroup.Columns("AccountSet").Width = 200

            gvVendorGroup.Columns("OpngBal").IsVisible = True
            gvVendorGroup.Columns("OpngBal").Width = 150
            gvVendorGroup.Columns("OpngBal").HeaderText = "Opening Balance"
            gvVendorGroup.Columns("OpngBal").FormatString = "{0:f2}"


            If FormType = clsUserMgtCode.MISCreditorReport Then
                gvVendorGroup.Columns("Purchase").IsVisible = True
                gvVendorGroup.Columns("Purchase").Width = 100
                gvVendorGroup.Columns("Purchase").FormatString = "{0:f2}"

                gvVendorGroup.Columns("Payments").IsVisible = True
                gvVendorGroup.Columns("Payments").Width = 100
                gvVendorGroup.Columns("Payments").FormatString = "{0:f2}"

                'gvVendorGroup.Columns("DrNote").IsVisible = True
                'gvVendorGroup.Columns("DrNote").Width = 100
                'gvVendorGroup.Columns("DrNote").FormatString = "{0:f2}"

                'gvVendorGroup.Columns("CrNote").IsVisible = True
                'gvVendorGroup.Columns("CrNote").Width = 100
                'gvVendorGroup.Columns("CrNote").FormatString = "{0:f2}"
                gvVendorGroup.Columns("CrAmt").IsVisible = True
                gvVendorGroup.Columns("CrAmt").Width = 100
                gvVendorGroup.Columns("CrAmt").HeaderText = "Credit Note"
                gvVendorGroup.Columns("CrAmt").FormatString = "{0:f2}"

                gvVendorGroup.Columns("DrAmt").IsVisible = True
                gvVendorGroup.Columns("DrAmt").Width = 100
                gvVendorGroup.Columns("DrAmt").HeaderText = "Debit Note"
                gvVendorGroup.Columns("DrAmt").FormatString = "{0:f2}"
            Else
                gvVendorGroup.Columns("CrAmt").IsVisible = True
                gvVendorGroup.Columns("CrAmt").Width = 100
                gvVendorGroup.Columns("CrAmt").HeaderText = "CrAmt"
                gvVendorGroup.Columns("CrAmt").FormatString = "{0:f2}"

                gvVendorGroup.Columns("DrAmt").IsVisible = True
                gvVendorGroup.Columns("DrAmt").Width = 100
                gvVendorGroup.Columns("DrAmt").HeaderText = "DrAmt"
                gvVendorGroup.Columns("DrAmt").FormatString = "{0:f2}"
            End If

            gvVendorGroup.Columns("Closing").IsVisible = True
            gvVendorGroup.Columns("Closing").Width = 100
            gvVendorGroup.Columns("Closing").FormatString = "{0:f2}"

            'Dim summaryRowItem As New GridViewSummaryRowItem()
            'Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("Payments", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            ''TotalAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            ''summaryRowItem.Add(TotalAmt)
            ''Dim TotalClosing As New GridViewSummaryItem()
            ''TotalClosing.FormatString = "{0:F2}"
            ''TotalClosing.Name = "Closing"
            ''TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
            ''summaryRowItem.Add(TotalClosing)

            'If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
            '    Dim TotalClosing As New GridViewSummaryItem()
            '    TotalClosing.FormatString = "{0:F2}"
            '    TotalClosing.Name = "Closing"
            '    TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)+sUM(Purchase)-SUM(Payments)"
            '    summaryRowItem.Add(TotalClosing)
            'Else
            '    Dim TotalClosing As New GridViewSummaryItem()
            '    TotalClosing.FormatString = "{0:F2}"
            '    TotalClosing.Name = "Closing"
            '    TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
            '    summaryRowItem.Add(TotalClosing)
            'End If


            'gv.MasterTemplate.AutoExpandGroups = True
            'gvVendorGroup.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Catch ex As Exception
            Throw New Exception(ex.Message)
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
            gvVendor.Columns("VCode").IsVisible = True
            gvVendor.Columns("VCode").Width = 180
            gvVendor.Columns("VCode").HeaderText = "Vendor Code"

            gvVendor.Columns("VName").IsVisible = True
            gvVendor.Columns("VName").Width = 350
            gvVendor.Columns("VName").HeaderText = "Name"

            gvVendor.Columns("Vendor_Group_Desc").IsVisible = True
            gvVendor.Columns("Vendor_Group_Desc").Width = 350
            gvVendor.Columns("Vendor_Group_Desc").HeaderText = "Vendor Group"

            gvVendor.Columns("AccountSet").IsVisible = True
            gvVendor.Columns("AccountSet").Width = 200

            gvVendor.Columns("OpngBal").IsVisible = True
            gvVendor.Columns("OpngBal").Width = 150
            gvVendor.Columns("OpngBal").HeaderText = "Opening Balance"
            gvVendor.Columns("OpngBal").FormatString = "{0:f2}"

            If FormType = clsUserMgtCode.MISCreditorReport Then
                gvVendor.Columns("Purchase").IsVisible = True
                gvVendor.Columns("Purchase").Width = 100
                gvVendor.Columns("Purchase").FormatString = "{0:f2}"

                gvVendor.Columns("Payments").IsVisible = True
                gvVendor.Columns("Payments").Width = 100
                gvVendor.Columns("Payments").FormatString = "{0:f2}"

                'gvVendor.Columns("DrNote").IsVisible = True
                'gvVendor.Columns("DrNote").Width = 100
                'gvVendor.Columns("DrNote").FormatString = "{0:f2}"

                'gvVendor.Columns("CrNote").IsVisible = True
                'gvVendor.Columns("CrNote").Width = 100
                'gvVendor.Columns("CrNote").FormatString = "{0:f2}"
                gvVendor.Columns("CrAmt").IsVisible = True
                gvVendor.Columns("CrAmt").Width = 100
                gvVendor.Columns("CrAmt").HeaderText = "Credit Note"
                gvVendor.Columns("CrAmt").FormatString = "{0:f2}"

                gvVendor.Columns("DrAmt").IsVisible = True
                gvVendor.Columns("DrAmt").Width = 100
                gvVendor.Columns("DrAmt").HeaderText = "Debit Note"
                gvVendor.Columns("DrAmt").FormatString = "{0:f2}"
            Else
                gvVendor.Columns("CrAmt").IsVisible = True
                gvVendor.Columns("CrAmt").Width = 100
                gvVendor.Columns("CrAmt").HeaderText = "CrAmt"
                gvVendor.Columns("CrAmt").FormatString = "{0:f2}"

                gvVendor.Columns("DrAmt").IsVisible = True
                gvVendor.Columns("DrAmt").Width = 100
                gvVendor.Columns("DrAmt").HeaderText = "DrAmt"
                gvVendor.Columns("DrAmt").FormatString = "{0:f2}"
            End If

            gvVendor.Columns("Closing").IsVisible = True
            gvVendor.Columns("Closing").Width = 100
            gvVendor.Columns("Closing").FormatString = "{0:f2}"

            'Dim summaryRowItem As New GridViewSummaryRowItem()
            'Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("Payments", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            ''TotalAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            ''summaryRowItem.Add(TotalAmt)

            ''Dim TotalClosing As New GridViewSummaryItem()
            ''TotalClosing.FormatString = "{0:F2}"
            ''TotalClosing.Name = "Closing"
            ''TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
            ''summaryRowItem.Add(TotalClosing)

            'If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
            '    Dim TotalClosing As New GridViewSummaryItem()
            '    TotalClosing.FormatString = "{0:F2}"
            '    TotalClosing.Name = "Closing"
            '    TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)+sUM(Purchase)-SUM(Payments)"
            '    summaryRowItem.Add(TotalClosing)
            'Else
            '    Dim TotalClosing As New GridViewSummaryItem()
            '    TotalClosing.FormatString = "{0:F2}"
            '    TotalClosing.Name = "Closing"
            '    TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
            '    summaryRowItem.Add(TotalClosing)
            'End If


            'gv.MasterTemplate.AutoExpandGroups = True
            'gvVendor.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    '' Anubhooti 25-Nov-2014
    Sub SetGridFormat(ByVal IsFromDrillDown As Boolean)
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.ShowGroupPanel = False
        Dim summaryRowItem As New GridViewSummaryRowItem()

        gv.AllowAddNewRow = False

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        If gv.Columns.Contains("Document Type Detail") Then
            gv.Columns("Document Type Detail").Width = 100
        End If

        gv.Columns("vcode").IsVisible = True
        gv.Columns("vcode").Width = 50
        gv.Columns("vcode").HeaderText = "Vendor Code"

        gv.Columns("VName").IsVisible = True
        gv.Columns("VName").Width = 100
        gv.Columns("VName").HeaderText = "Vendor Name"

        gv.Columns("DocNo").IsVisible = True
        gv.Columns("DocNo").Width = 100
        gv.Columns("DocNo").HeaderText = "Document Number"

        gv.Columns("DocType").IsVisible = True
        gv.Columns("DocType").Width = 100
        gv.Columns("DocType").HeaderText = "Document Type"

        gv.Columns("DocDate").IsVisible = True
        gv.Columns("DocDate").Width = 100
        gv.Columns("DocDate").HeaderText = "Document Date"
        gv.Columns("DocDate").FormatString = "{0:d}"

        gv.Columns("DocNarr").IsVisible = True
        gv.Columns("DocNarr").Width = 250
        gv.Columns("DocNarr").HeaderText = "Document Narr"

        gv.Columns("Reference_Doc_No").IsVisible = True
        gv.Columns("Reference_Doc_No").Width = 250
        gv.Columns("Reference_Doc_No").HeaderText = "Reference Doc No"

        gv.Columns("ChequeDetails").IsVisible = True
        gv.Columns("ChequeDetails").Width = 100
        gv.Columns("ChequeDetails").HeaderText = "Cheque Details"

        gv.Columns("Currency_Code").IsVisible = True
        gv.Columns("Currency_Code").Width = 80
        gv.Columns("Currency_Code").HeaderText = "Currency"

        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            gv.Columns("ConvRate").IsVisible = True
            gv.Columns("ConvRate").Width = 100
            gv.Columns("ConvRate").HeaderText = "Conv Rate"
            gv.Columns("ConvRate").FormatString = "{0:f2}"
        End If

        If FormType = clsUserMgtCode.MISCreditorReport Then
            'gv.Columns("Purchase1").IsVisible = True
            'gv.Columns("Purchase1").Width = 100
            'gv.Columns("Purchase1").HeaderText = "Purchase"
            'gv.Columns("Purchase1").FormatString = "{0:f2}"

            'gv.Columns("Payments1").IsVisible = True
            'gv.Columns("Payments1").Width = 100
            'gv.Columns("Payments1").HeaderText = "Payments"
            'gv.Columns("Payments1").FormatString = "{0:f2}"


            gv.Columns("Purchase").IsVisible = True
            gv.Columns("Purchase").Width = 100
            gv.Columns("Purchase").HeaderText = "Purchase"
            gv.Columns("Purchase").FormatString = "{0:f2}"

            gv.Columns("Payments").IsVisible = True
            gv.Columns("Payments").Width = 100
            gv.Columns("Payments").HeaderText = "Payments"
            gv.Columns("Payments").FormatString = "{0:f2}"


            'gv.Columns("DrNote1").IsVisible = True
            'gv.Columns("DrNote1").Width = 100
            'gv.Columns("DrNote1").HeaderText = "DrNote"
            'gv.Columns("DrNote1").FormatString = "{0:f2}"

            'gv.Columns("CrNote1").IsVisible = True
            'gv.Columns("CrNote1").Width = 100
            'gv.Columns("CrNote1").HeaderText = "CrNote"
            'gv.Columns("CrNote1").FormatString = "{0:f2}"
            gv.Columns("CrAmt").IsVisible = True
            gv.Columns("CrAmt").Width = 100
            gv.Columns("CrAmt").HeaderText = "Credit Note"
            gv.Columns("CrAmt").FormatString = "{0:f2}"

            gv.Columns("DrAmt").IsVisible = True
            gv.Columns("DrAmt").Width = 100
            gv.Columns("DrAmt").HeaderText = "Debit Note"
            gv.Columns("DrAmt").FormatString = "{0:f2}"
        Else
            gv.Columns("CrAmt").IsVisible = True
            gv.Columns("CrAmt").Width = 100
            gv.Columns("CrAmt").HeaderText = "CrAmt"
            gv.Columns("CrAmt").FormatString = "{0:f2}"

            gv.Columns("DrAmt").IsVisible = True
            gv.Columns("DrAmt").Width = 100
            gv.Columns("DrAmt").HeaderText = "DrAmt"
            gv.Columns("DrAmt").FormatString = "{0:f2}"
        End If

        gv.Columns("Closing").IsVisible = Not IsFromDrillDown
        gv.Columns("Closing").Width = 100
        gv.Columns("Closing").FormatString = "{0:f2}"
        gv.Columns("Closing").HeaderText = "Closing"

        'gv.Columns("EffectiveAmt").IsVisible = True
        'gv.Columns("EffectiveAmt").Width = 100
        'gv.Columns("EffectiveAmt").HeaderText = "Balance Amount"

        gv.Columns("Document_Type").IsVisible = True
        gv.Columns("Document_Type").Width = 100
        gv.Columns("Document_Type").HeaderText = "Document Type"

        gv.Columns("account").IsVisible = True
        gv.Columns("account").Width = 100
        gv.Columns("account").HeaderText = "Location"

        gv.Columns("Posting_Date").IsVisible = True
        gv.Columns("Posting_Date").Width = 100
        gv.Columns("Posting_Date").HeaderText = "Posting Date"

        gv.Columns("Vendor_Group_Code").IsVisible = True
        gv.Columns("Vendor_Group_Code").Width = 100
        gv.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

        'gv.Columns("Address").IsVisible = False
        'gv.Columns("Address").Width = 300
        'gv.Columns("Address").HeaderText = "Address"

        gv.Columns("Reconciliation").IsVisible = True
        gv.Columns("Reconciliation").Width = 60

        gv.Columns("Reconciliation_Date").IsVisible = True
        gv.Columns("Reconciliation_Date").HeaderText = "Reco Date"
        gv.Columns("Reconciliation_Date").Width = 120

        '' Anubhooti 09-Jan-2014 (CrAmt/DrAmt/BalAmt/EffAmt should not be included when PDC is ON)
        If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
            gv.Columns("DrPDC").IsVisible = False
            gv.Columns("DrPDC").Width = 150
            gv.Columns("DrPDC").ReadOnly = True
            gv.Columns("DrPDC").HeaderText = "Debit Amt Of PDC"

            gv.Columns("CrPDC").IsVisible = False
            gv.Columns("CrPDC").Width = 150
            gv.Columns("CrPDC").ReadOnly = True
            gv.Columns("CrPDC").HeaderText = "Credit Amt Of PDC"

            gv.Columns("EffPDC").IsVisible = False
            gv.Columns("EffPDC").Width = 150
            gv.Columns("EffPDC").ReadOnly = True
            gv.Columns("EffPDC").HeaderText = "Effective Amt Of PDC"
        End If
        ''
        'If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
        '    Dim ttllAmt As New GridViewSummaryItem("DrAmt", "{0:F2}", "sum(DrPDC)")
        '    summaryRowItem.Add(ttllAmt)
        '    ttllAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", "sum(CrPDC)")
        '    summaryRowItem.Add(ttllAmt)
        '    ttllAmt = New GridViewSummaryItem("EffectiveAmt", "{0:F2}", "sum(EffPDC)")
        '    summaryRowItem.Add(ttllAmt)
        'Else
        '    Dim ttllAmt As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(ttllAmt)
        '    ttllAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(ttllAmt)
        '    ttllAmt = New GridViewSummaryItem("EffectiveAmt", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(ttllAmt)

        '    'ttllAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
        '    'summaryRowItem.Add(ttllAmt)

        'End If

        'Dim TotalAmt As New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(TotalAmt)
        'TotalAmt = New GridViewSummaryItem("Payments", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(TotalAmt)
        'TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(TotalAmt)
        'TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(TotalAmt)


        ''Dim TotalClosing As New GridViewSummaryItem()
        ''TotalClosing.FormatString = "{0:F2}"
        ''TotalClosing.Name = "Closing"
        ''TotalClosing.AggregateExpression = "sum(CrAmt)-sum(DrAmt)"
        ''summaryRowItem.Add(TotalClosing)

        'If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
        '    Dim TotalClosing As New GridViewSummaryItem()
        '    TotalClosing.FormatString = "{0:F2}"
        '    TotalClosing.Name = "Closing"
        '    TotalClosing.AggregateExpression = "sum(CrAmt)-sum(DrAmt)+sUM(Purchase)-SUM(Payments)"
        '    summaryRowItem.Add(TotalClosing)
        'Else
        '    Dim TotalClosing As New GridViewSummaryItem()
        '    TotalClosing.FormatString = "{0:F2}"
        '    TotalClosing.Name = "Closing"
        '    TotalClosing.AggregateExpression = "sum(CrAmt)-sum(DrAmt)"
        '    summaryRowItem.Add(TotalClosing)
        'End If

        'gv.MasterTemplate.AutoExpandGroups = True
        'gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    'Ticket No-ERO/18/11/19-001114
    Private Sub RestoreGridSummaryRow()

        If gvVendor.Visible = True Then
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
            'TotalAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)

            'Dim TotalClosing As New GridViewSummaryItem()
            'TotalClosing.FormatString = "{0:F2}"
            'TotalClosing.Name = "Closing"
            'TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
            'summaryRowItem.Add(TotalClosing)

            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "Closing"
                TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)+sUM(Purchase)-SUM(Payments)"
                summaryRowItem.Add(TotalClosing)
            Else
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "Closing"
                TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
                summaryRowItem.Add(TotalClosing)
            End If


            gv.MasterTemplate.AutoExpandGroups = True
            gvVendor.MasterTemplate.SummaryRowsBottom.Clear()
            gvVendor.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf gvVendorGroup.Visible = True Then
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
            'TotalAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'Dim TotalClosing As New GridViewSummaryItem()
            'TotalClosing.FormatString = "{0:F2}"
            'TotalClosing.Name = "Closing"
            'TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
            'summaryRowItem.Add(TotalClosing)

            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "Closing"
                TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)+sUM(Purchase)-SUM(Payments)"
                summaryRowItem.Add(TotalClosing)
            Else
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "Closing"
                TotalClosing.AggregateExpression = "sum(OpngBal)+sum(CrAmt)-sum(DrAmt)"
                summaryRowItem.Add(TotalClosing)
            End If


            gv.MasterTemplate.AutoExpandGroups = True
            gvVendorGroup.MasterTemplate.SummaryRowsBottom.Clear()
            gvVendorGroup.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        ElseIf gv.Visible = True Then
            Dim summaryRowItem As New GridViewSummaryRowItem()

            If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
                Dim ttllAmt As New GridViewSummaryItem("DrAmt", "{0:F2}", "sum(DrPDC)")
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", "sum(CrPDC)")
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("EffectiveAmt", "{0:F2}", "sum(EffPDC)")
                summaryRowItem.Add(ttllAmt)
            Else
                Dim ttllAmt As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("EffectiveAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)

                'ttllAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(ttllAmt)

            End If

            Dim TotalAmt As New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Payments", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)


            'Dim TotalClosing As New GridViewSummaryItem()
            'TotalClosing.FormatString = "{0:F2}"
            'TotalClosing.Name = "Closing"
            'TotalClosing.AggregateExpression = "sum(CrAmt)-sum(DrAmt)"
            'summaryRowItem.Add(TotalClosing)

            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "Closing"
                TotalClosing.AggregateExpression = "sum(CrAmt)-sum(DrAmt)+sUM(Purchase)-SUM(Payments)"
                summaryRowItem.Add(TotalClosing)
            Else
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "Closing"
                TotalClosing.AggregateExpression = "sum(CrAmt)-sum(DrAmt)"
                summaryRowItem.Add(TotalClosing)
            End If

            gv.MasterTemplate.AutoExpandGroups = True
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        End If
    End Sub

    ''
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmRptVendorLedger_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled AndAlso MyBase.isPrintFlag Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.KeyCode = Keys.F7 AndAlso (clsCommon.CompairString(FormType, clsUserMgtCode.VendorLedgerReport) = CompairStringResult.Equal) Then
            If rbtnDocWise.Visible = True Then
                rbtnDocWise.Visible = False
            Else
                rbtnDocWise.Visible = True
            End If
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
            If gvVendorGroup.Visible Then
                If gvVendorGroup.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If
            If gvVendor.Visible Then
                If gvVendor.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If
            If gv.Visible Then
                If gv.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkVendorSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Vendor(s) : " + strtemp)
            End If

            If rbtnchildslct.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgchild.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Child Vendor(s) : " + strtemp)
            End If

            If chkVndrSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgVndrGroup.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Vendor Group : " + strtemp)
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

            If gvVendorGroup.Visible Then
                clsCommon.MyExportToExcelGrid("Vendor Ledger Report ", gvVendorGroup, arrHeader, Me.Text)
            End If
            If gvVendor.Visible Then
                clsCommon.MyExportToExcelGrid("Vendor Ledger Report ", gvVendor, arrHeader, Me.Text)
            End If
            If gv.Visible Then
                clsCommon.MyExportToExcelGrid("Vendor Ledger Report ", gv, arrHeader, Me.Text)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        blnRefresh = True
        GC.Collect()
        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            PrintForVendorCurrency()
        Else
            Print()
        End If
        RestoreGridSummaryRow()
        ReportID = GetReportID()
        PageSetupReport_ID = GetReportID()
    End Sub
    Sub PrintForVendorCurrency(Optional ByVal BulkExport As Integer = 0)
        Dim IsPDCCheque As String = ""
        Dim CompanyAdd As String = ""
        Dim Comp_Name As String = ""
        Dim childvendrcode As String = ""
        Try
            If chkVndrSelect.IsChecked AndAlso cbgVndrGroup.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vendor Group.", Me.Text)
                Return
            End If
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vendor", Me.Text)
                Return
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single Location", Me.Text)
                Return
            End If
            If rbtnchildslct.IsChecked AndAlso cbgchild.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single Child Vendor", Me.Text)
                Return
            End If
            If rbtnchildall.IsChecked AndAlso chkVendorSelect.IsChecked Then
                Dim query As String = "select distinct (select ',''''+vendor_code+''''' from tspl_vendor_master where parent_vendor_code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") and is_parent_vendor<>'1' for xml path('')) as xvalue"
                childvendrcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
                Try
                    If childvendrcode.Substring(0, 1) = "," Then
                        childvendrcode = childvendrcode.Substring(1, childvendrcode.Length - 1)
                    End If
                Catch exx As Exception
                End Try
            End If

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

            Dim qry As String
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

            BaseQry = GetVendorBaseQryForVendorCurrency(rbPortrait.IsChecked, rbLandScape.IsChecked, chkAgainstSalaryAdvance.Checked)

            ''richa 24 oct,2017

            Dim StrEmployeetypeCondition As String = String.Empty
            Dim StrEmployeeAdvancetypeCondition As String = String.Empty
            If chkTADA.Checked = True Then
                StrEmployeetypeCondition += "'TD',"
            End If
            If chkTravel.Checked = True Then
                StrEmployeetypeCondition += "'T',"
            End If
            If chkImprest.Checked = True Then
                StrEmployeetypeCondition += "'I',"
            End If
            If chkSalary.Checked = True Then
                StrEmployeetypeCondition += "'S',"
            End If
            If chkAdvanceImprest.Checked = True Then
                StrEmployeeAdvancetypeCondition += "'I',"
            End If
            If chkAdvanceTravel.Checked = True Then
                StrEmployeeAdvancetypeCondition += "'T',"
            End If
            If chkAgainstSalaryAdvance.Checked = True Then
                StrEmployeeAdvancetypeCondition += "'S',"
            End If

            BaseQry += " where 1=1 "
            strtempBaseQryforopeningForMIS += " where 1=1 "

            If clsCommon.myLen(StrEmployeeAdvancetypeCondition) > 0 Then
                StrEmployeeAdvancetypeCondition = StrEmployeeAdvancetypeCondition.Substring(0, StrEmployeeAdvancetypeCondition.Length - 1)
                StrEmployeeAdvancetypeCondition = " and InnQuery.Emp_Adv_Type in (" & StrEmployeeAdvancetypeCondition & ") "
                BaseQry += StrEmployeeAdvancetypeCondition
                strtempBaseQryforopeningForMIS += StrEmployeeAdvancetypeCondition
            End If
            If clsCommon.myLen(StrEmployeeAdvancetypeCondition) > 0 And clsCommon.myLen(StrEmployeetypeCondition) > 0 Then
                StrEmployeetypeCondition = StrEmployeetypeCondition.Substring(0, StrEmployeetypeCondition.Length - 1)
                StrEmployeetypeCondition = " or InnQuery.Emp_type in (" & StrEmployeetypeCondition & ") "
                BaseQry += StrEmployeetypeCondition
                strtempBaseQryforopeningForMIS += StrEmployeetypeCondition
            ElseIf clsCommon.myLen(StrEmployeetypeCondition) > 0 Then
                StrEmployeetypeCondition = StrEmployeetypeCondition.Substring(0, StrEmployeetypeCondition.Length - 1)
                StrEmployeetypeCondition = " and InnQuery.Emp_type in (" & StrEmployeetypeCondition & ") "
                BaseQry += StrEmployeetypeCondition
                strtempBaseQryforopeningForMIS += StrEmployeetypeCondition
            End If

            ''-------------------





            Dim strFIlterCheck As String = ""
            Dim strCheckForSumm As String = ""
            Dim strVenFilterPDC As String = ""
            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_VENDOR_MASTER.Vendor_Group_Code in (" + clsCommon.GetMulcallString(txtVendorGroup.arrValueMember) + ")  " '----------changed here customer is used changed to vendor
                strVenFilterPDC += " and TSPL_VENDOR_MASTER.Vendor_Group_Code IN (" + clsCommon.GetMulcallString(txtVendorGroup.arrValueMember) + ")  "
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strFIlterCheck += " and (TSPL_VENDOR_MASTER.vendor_code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")" 'Monika
                If ChkchildVendor.Checked Then
                    strFIlterCheck += " OR TSPL_VENDOR_MASTER.vendor_code in (select Vendor_Code from TSPL_VENDOR_MASTER Where ISNULL(Parent_Vendor_Code,'') in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + "))"
                End If
                strFIlterCheck += ")"
                strVenFilterPDC += " and TSPL_PAYMENT_HEADER.Vendor_Code IN (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + "," + clsCommon.GetMulcallString(txtChildVendor.arrValueMember) + ")"
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strFIlterCheck += " and RIGHT(final.account,3) in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  " 'by Monika
                strVenFilterPDC += " and right(TSPL_BANK_MASTER.BANKACC,3) IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + " )"
            Else
                If ShowVendorLedgerasPerRightsForLocation = True Then
                    strFIlterCheck += " and RIGHT(final.account,3) in (" + StrPermission + ")  "
                    strVenFilterPDC += " and right(TSPL_BANK_MASTER.BANKACC,3) IN (" + StrPermission + " )"
                End If
            End If

            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_VENDOR_GROUP.Acct_Set_Code in (" + clsCommon.GetMulcallString(txtAccountSet.arrValueMember) + ")  "
                strVenFilterPDC += " and TSPL_VENDOR_GROUP.Acct_Set_Code IN (" + clsCommon.GetMulcallString(txtAccountSet.arrValueMember) + " )"
            End If
            If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
                strFIlterCheck += " and TSPL_VENDOR_MASTER.CURRENCY_CODE in ('" + clsCommon.myCstr(txtCurrencyCode.Value) + "')"
            End If
            '-----------------------------------------------------
            If chkExcludeOpening.Checked = True Then
                BaseQryOpening = "   Select '' as Emp_type,'' as Emp_Adv_Type,null as Due_Date, '' as PO_SRN, '' as VCode, '' as VName, '' as DocNo, '' as DocType, null as DocDate, '' as DocNarr, '' as ChequeDetails, '' as Currency_Code, 1 as ConvRate, 0 AS CrAmt,0 AS DrAmt,0 AS Purchase,0 AS Payments ,  0 as drNote,0 as CrNote,'' as Document_Type, 0 as Balance_Amount, '' as account,null as Posting_Date,'' as GLDocType, '' as Comp_Code where 1=2"
                strtempBaseQryforopeningForMIS = "   Select '' as Emp_type,'' as Emp_Adv_Type,null as Due_Date, '' as PO_SRN, '' as VCode, '' as VName, '' as DocNo, '' as DocType, null as DocDate, '' as DocNarr, '' as ChequeDetails, '' as Currency_Code, 1 as ConvRate, 0 AS CrAmt,0 AS DrAmt,0 AS Purchase,0 AS Payments ,  0 as drNote,0 as CrNote,'' as Document_Type, 0 as Balance_Amount, '' as account,null as Posting_Date,'' as GLDocType, '' as Comp_Code where 1=2"
            End If

            Dim strQry As String
            '---------------Vendor Group Wise Data-----------
            strQry = "Select 'VendorGroup' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, Vendor_Group_Code, MAX(Vendor_Group_Desc) as Vendor_Group_Desc, MAX(AccountSet) as AccountSet, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Purchase) as Purchase, SUM(Payments) as Payments, SUM(DrNote) as DrNote, SUM(CrNote) as CrNote, "
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                strQry += "  SUM(OpngBal)-SUM(DrAmt)+SUM(CrAmt)+Sum(Purchase)-Sum(Payments) as [Closing] from ("
            Else
                strQry += " SUM(OpngBal) + SUM(CrAmt)-SUM(DrAmt) as [Closing] from ("
            End If
            strQry += " Select Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, SUM(CrAmt)-SUM(DrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote From ( " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, strtempBaseQryforopeningForMIS, BaseQry) & " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  <'" + strFromDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY Vendor_Group_Code" + Environment.NewLine & _
            Environment.NewLine + " UNION ALL" + Environment.NewLine & _
            " Select Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, 0 as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(CONVERT(DECIMAL(18,2),Purchase*" & ddlCurrencyType.SelectedValue & ")) as Purchase, SUM(CONVERT(DECIMAL(18,2),Payments* " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, "1", ddlCurrencyType.SelectedValue) & ") ) as Payments, SUM(DrNote*" & ddlCurrencyType.SelectedValue & ") as DrNote, SUM(CrNote*" & ddlCurrencyType.SelectedValue & ") as CrNote from ( " + BaseQry + " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY Vendor_Group_Code" + Environment.NewLine & _
            " ) XXX GROUP BY Vendor_Group_Code ORDER BY Vendor_Group_Code"
            dtCustGrp = clsDBFuncationality.GetDataTable(strQry)

            '===============================update by richa agarwal 3 July,2018 ticket no. KDI/02/07/18-000383 pick vendor name from vendor master table instead of transaction table
            strQry = "Select 'Vendor' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, VCode, MAX(VName) as VName, MAX(Vendor_Group_Code) as Vendor_Group_Code, MAX(Vendor_Group_Desc) as Vendor_Group_Desc, MAX(AccountSet) as AccountSet, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Purchase) as Purchase, SUM(Payments) as Payments, SUM(DrNote) as DrNote, SUM(CrNote) as CrNote,  "
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                strQry += " SUM(OpngBal)-SUM(DrAmt)+SUM(CrAmt)+ Sum(Purchase)-Sum(Payments) as [Closing] from ("
            Else
                strQry += " SUM(OpngBal)+ SUM(CrAmt)-SUM(DrAmt) as [Closing] from ("
            End If
            strQry += " Select VCode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code) as Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, SUM(CrAmt)-SUM(DrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote From ( " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, strtempBaseQryforopeningForMIS, BaseQry) & " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  <'" + strFromDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY VCode" + Environment.NewLine & _
           " UNION ALL" + Environment.NewLine & _
           " Select VCode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code) as Vendor_Group_Code, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor_Group_Desc], MAX(TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Desc) as AccountSet, 0 as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(CONVERT(DECIMAL(18,2),Purchase*" & ddlCurrencyType.SelectedValue & ")) as Purchase, SUM(CONVERT(DECIMAL(18,2),Payments*" & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, "1", ddlCurrencyType.SelectedValue) & ")) as Payments, SUM(DrNote*" & ddlCurrencyType.SelectedValue & ") as DrNote, SUM(CrNote*" & ddlCurrencyType.SelectedValue & ") as CrNote from ( " + BaseQry + " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY VCode" + Environment.NewLine & _
           " ) XXX GROUP BY VCode ORDER BY VCode"
            dtCustomer = clsDBFuncationality.GetDataTable(strQry)


            Dim AccountSet As String = String.Empty
            If chkAgainstSalaryAdvance.Checked = True Then
                AccountSet = "Advance_Against_Salary"
            Else
                AccountSet = "Payable_Account"
            End If
            If (rbtnDocWise.IsChecked = True) AndAlso chkAgainstSalaryAdvance.Checked = False Then
                strQry = " With CTETemp as " &
                " ( SELECT * " &
                " ,( SELECT MAX( TSPL_JOURNAL_DETAILS.Account_code) AS Account_code    FROM TSPL_JOURNAL_DETAILS " &
                " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =XXXX.Vendor_Account   " &
                "LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " &
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = case when XXXX.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when XXXX.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when XXXX.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when XXXX.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when XXXX.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end " &
                " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=XXXX.Voucher_No ) AS JEAccount_code" &
                " ,( SELECT SUM( TSPL_JOURNAL_DETAILS.Amount)   AS Amount  FROM TSPL_JOURNAL_DETAILS " &
                " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =XXXX.Vendor_Account   " &
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " &
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') =  case when XXXX.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when XXXX.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when XXXX.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when XXXX.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when XXXX.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=XXXX.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end " &
                " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=XXXX.Voucher_No ) AS JEAMT FROM  " &
                " ( Select ROW_NUMBER() OVER (PARTITION BY VCode ORDER BY VCode, DocDate) as RowNo, '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,* from ( "
                strQry += " Select max(EXCHANGE_GAIN_AMT) as EXCHANGE_GAIN_AMT,max(EXCHANGE_LOSS_AMT) as  EXCHANGE_LOSS_AMT,  MAX(Due_Date) AS Due_Date,  MAX(PO_SRN) AS PO_SRN,  MAX(vcode) AS vcode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, DocNo, MAX(DocType) AS DocType, MAX(DocDate) AS DocDate,  MAX(DocNarr) AS DocNarr, MAX(ChequeDetails) AS ChequeDetails, MAX(Final.Currency_Code) AS Currency_Code, CASE WHEN SUM(DrAmt) >= SUM(CrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") THEN SUM(DrAmt) - SUM(CrAmt) ELSE 0 END AS DrAmt, CASE WHEN SUM(CrAmt) > SUM(DrAmt) THEN  SUM(CrAmt) - SUM(DrAmt) ELSE 0 END AS CrAmt, SUM(Purchase* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Purchase, SUM(Payments* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Payments, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote, SUM(Purchase* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Purchase1, SUM(Payments* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Payments1, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote1, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote1, SUM(CrAmt-DrAmt) as EffectiveAmt, MAX(final.Document_Type) AS Document_Type, SUM(Balance_Amount* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & ") as Balance_Amount,substring(MAX(account),LEN(MAX(account))-2,3) + ' - ' + MAX(TSPL_GL_SEGMENT_CODE.Description)  as account, CONVERT(VARCHAR,MAX(final.Posting_Date),103) As Posting_Date,MAX(GLDocType) AS GLDocType, MAX(TSPL_VENDOR_MASTER.Vendor_Group_Code) AS Vendor_Group_Code,  MAX(TSPL_VENDOR_GROUP.Group_Desc) AS Group_Desc  , Case When SUM(DrAmt)>0 Then 0 else 1 End as OrderdrCr, Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then 'Done' Else 'Pending' End as [Reconciliation], Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else '' End as [Reconciliation_Date],MAX(TSPL_JOURNAL_MASTER.Voucher_No) AS Voucher_No ,MAX(TSPL_VENDOR_MASTER.Vendor_Account) AS Vendor_Account  FROM " + Environment.NewLine &
                "( " + BaseQry + " )  Final LEFT OUTER JOIN TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =substring(Final.account,LEN(Final.account)-2,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code LEFT OUTER JOIN " + Environment.NewLine &
                "tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No and tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " &
                " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo " + Environment.NewLine &
               " where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous'   " + strFIlterCheck + "" + Environment.NewLine &
                " GROUP By DocNo  ) XXX " + Environment.NewLine &
                "  )XXXX " + Environment.NewLine &
                " ) Select CTETemp.RowNo, CTETemp.RunDate , CTETemp.Address , CTETemp.Comp_Name , CTETemp.FilterFromDate , CTETemp.FilterToDate, CTETemp.Due_Date, CTETemp.PO_SRN, CTETemp.VCode, CTETemp.VName , CTETemp.DocNo, CASE WHEN (sELECT ISNULL(Against_BulkMillkPurchaseInvoice_No,'')  FROM TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO=CTETemp.DocNo) <>'' THEN 'Pur. Bulk' else CASE WHEN (sELECT ISNULL(Against_MillkPurchaseInvoice_No,'')  FROM TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO=CTETemp.DocNo) <>'' THEN 'Pur. MCC' else CTETemp.DocType end end as DocType, CONVERT(VARCHAR,CTETemp.DocDate,103) as DocDate, CTETemp.DocNarr,  CASE WHEN (sELECT ISNULL(Against_BulkMillkPurchaseInvoice_No,'')  FROM TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO=CTETemp.DocNo) <>'' THEN 'Pur. Bulk' else CASE WHEN (sELECT ISNULL(Against_MillkPurchaseInvoice_No,'')  FROM TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO=CTETemp.DocNo) <>'' THEN 'Pur. MCC' else CTETemp.DocType end end as DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, CTETemp.DrAmt , CTETemp.CrAmt, CTETemp.Purchase, CTETemp.Payments, CTETemp.DrNote, CTETemp.CrNote, CTETemp.Purchase1, CTETemp.Payments1, CTETemp.DrNote1, CTETemp.CrNote1, SUM(CrAmt-DrAmt) Over (Partition by VCode ORDER BY RowNo) as [Closing], CTETemp.EffectiveAmt , CTETemp.Document_Type , CTETemp.Balance_Amount , CTETemp.account , " &
                " CTETemp.Posting_Date, CTETemp.GLDocType, CTETemp.Vendor_Group_Code, CTETemp.Group_Desc, CTETemp.OrderdrCr, CTETemp.Reconciliation, CTETemp.Reconciliation_Date,CTETemp.Voucher_No AS [JE No],CTETemp.JEAccount_Code AS [JE Account],CTETemp.JEAMT [JE Amount],CTETemp.EffectiveAmt + CTETemp.JEAMT AS DiffAmt  from CTETemp ORDER BY CTETemp.VCode , RowNo "

            Else

                '------------------Detail Level Data-------------------
                strQry = "With CTETemp as " & _
                " ( Select ROW_NUMBER() OVER (PARTITION BY VCode ORDER BY VCode, DocDate) as RowNo, '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, "
                If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
                    strQry += "DrAmt  AS DrPDC,CrAmt  AS CrPDC,EffectiveAmt  AS EffPDC,Balance_Amount  AS BalPDC,"
                End If
                strQry += " * from ( Select Null As Due_Date ,max(PO_SRN) As PO_SRN, vcode,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName,'' as DocNo,'' as DocType, NULL as DocDate, 'Opening Balance' as DocNarr,null as Reference_Doc_No, '' as ChequeDetails, '' as Currency_Code,NULL as ConvRate, Case WHEN SUM(DrAmt)>=SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End as DrAmt, Case WHEN SUM(DrAmt)< SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End as CrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote, SUM(Purchase) as Purchase1, SUM(Payments) as Payments1, SUM(DrNote) as DrNote1, SUM(CrNote) as CrNote1, 0 as EffectiveAmt, '' as Document_Type, 0 as Balance_Amount, '' as account, '' As Posting_Date, '' as GLDocType, '' as Vendor_Group_Code, '' as Group_Desc , 0 as OrderdrCr, '' as [Reconciliation], '' as [Reconciliation_Date] ,'' AS Voucher_No,'' AS Vendor_Account   ,'' As JEAccount_Code,0 As JEAmt   " + Environment.NewLine & _
                " From ( " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, strtempBaseQryforopeningForMIS, BaseQry) & " ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo " & _
                 " where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  <'" + strFromDate + "' and DocType <>'Mislleneous' " + strFIlterCheck + " GROUP BY VCode" + Environment.NewLine & _
                " UNION ALL ------------------Main UNION Between [Opening And Data]------------------------------------" + Environment.NewLine & _
                 " Select Due_Date, PO_SRN, vcode, TSPL_VENDOR_MASTER.Vendor_Name as VName, DocNo, DocType, DocDate, DocNarr, Reference_Doc_No,ChequeDetails, Final.Currency_Code,Final.ConvRate , DrAmt as DrAmt, CrAmt as CrAmt, Purchase* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as Purchase, Payments* " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, "1", IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "")) & " as Payments, DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as DrNote, CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as CrNote, Purchase* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as Purchase1, Payments* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as Payments1, DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as DrNote1, CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as CrNote1, (CrAmt-DrAmt) as EffectiveAmt, final.Document_Type, Balance_Amount* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " final." & ddlCurrencyType.SelectedValue & "") & " as Balance_Amount,substring(account,LEN(account)-2,3) + ' - ' + TSPL_GL_SEGMENT_CODE.Description  as account, CONVERT(VARCHAR,final.Posting_Date,103) As Posting_Date,GLDocType, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc , Case When DrAmt>0 Then 0 else 1 End as OrderdrCr, Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then 'Done' Else 'Pending' End as [Reconciliation], Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then CONVERT(VARCHAR,tspl_BankReco_Detail.Reconciliation_Date,103) Else '' End as [Reconciliation_Date] ,TSPL_JOURNAL_MASTER.Voucher_No AS Voucher_No   , TSPL_VENDOR_MASTER.Vendor_Account ,( SELECT top 1 TSPL_JOURNAL_DETAILS.Account_code AS Account_code    FROM TSPL_JOURNAL_DETAILS " & _
                " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Account " & _
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " & _
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') =  case when Final.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType='TDS' then (Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code= ( Select Deduction_Code from tspl_vendor_master where vendor_code=Final.Vcode)) else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end  " & _
                " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No and TSPL_JOURNAL_DETAILS.Account_code =case when Final.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType='TDS' then (Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code= ( Select Deduction_Code from tspl_vendor_master where vendor_code=Final.Vcode)) else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end ) AS JEAccount_code " & _
                " ,( SELECT top 1  TSPL_JOURNAL_DETAILS.Amount AS Amount  FROM TSPL_JOURNAL_DETAILS " & _
                " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Account   " & _
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " & _
                " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') =  case when Final.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType='TDS' then (Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code= ( Select Deduction_Code from tspl_vendor_master where vendor_code=Final.Vcode)) else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end " & _
                " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No and TSPL_JOURNAL_DETAILS.Account_code =case when Final.DocType ='AP Invoice' then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='Advance' and (Select top 1 Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=1  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary  ,'')  when Final.DocType ='On Account' and (Select top 1  Advance_Against_Salary  from TSPL_PAYMENT_HEADER where Payment_No=Final.DocNo)=0  then ISNULL(TSPL_VENDOR_ACCOUNT_SET.Advance_Account   ,'') when Final.DocType='TDS' then (Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code= ( Select Deduction_Code from tspl_vendor_master where vendor_code=Final.Vcode)) else ISNULL(TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,'') end ) AS JEAmount from" + Environment.NewLine & _
                "( " + BaseQry + " )  Final LEFT OUTER JOIN TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =substring(Final.account,LEN(Final.account)-2,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code LEFT OUTER JOIN " + Environment.NewLine
                strQry += " tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No and tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo " & _
                " where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' and DocType <>'Mislleneous'   " + strFIlterCheck + "" + Environment.NewLine & _
                " ) XXX " + Environment.NewLine & _
                " ) Select CTETemp.RowNo, CTETemp.RunDate , CTETemp.Address , CTETemp.Comp_Name , CTETemp.FilterFromDate , CTETemp.FilterToDate, CTETemp.Due_Date, CTETemp.PO_SRN, CTETemp.VCode, CTETemp.VName , CTETemp.DocNo, CTETemp.DocType, CONVERT(VARCHAR,CTETemp.DocDate,103) as DocDate, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Reference_Doc_No," & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code,CTETemp.ConvRate as ConvRate, CTETemp.DrAmt , CTETemp.CrAmt, CTETemp.Purchase, CTETemp.Payments, CTETemp.DrNote, CTETemp.CrNote, CTETemp.Purchase1, CTETemp.Payments1, CTETemp.DrNote1, CTETemp.CrNote1, " & IIf(clsCommon.myCstr(clsUserMgtCode.MISCreditorReport) = FormType, " SUM(CrAmt+Purchase -DrAmt-Payments ) ", "  SUM(CrAmt-DrAmt)") & "   Over (Partition by VCode ORDER BY RowNo) as [Closing], CTETemp.EffectiveAmt , CTETemp.Document_Type , CTETemp.Balance_Amount , CTETemp.account , " & _
                " CTETemp.Posting_Date, CTETemp.GLDocType, CTETemp.Vendor_Group_Code, CTETemp.Group_Desc, CTETemp.OrderdrCr, CTETemp.Reconciliation, CTETemp.Reconciliation_Date ,CTETemp.Voucher_No AS [JE No],CTETemp.JEAccount_Code AS [JE Account],isnull(CTETemp.JEAMT,0) as  [JE Amount],CASE WHEN CTETemp.DocNarr='Opening Balance' THEN 0 ELSE isnull(CTETemp.EffectiveAmt,0) +  isnull(CTETemp.JEAMT,0) END  AS DiffAmt from CTETemp ORDER BY CTETemp.VCode , RowNo "
            End If

            '' bulk export
            If BulkExport = 1 Then
                transportSql.BulkExport("Vendor_Ledger", strQry, "ORDER BY CTETemp.VCode , RowNo", "csv", "Select CTETemp.RowNo")
                Exit Sub
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("Vendor_Ledger", strQry, "ORDER BY CTETemp.VCode , RowNo", "xls", "Select CTETemp.RowNo")
                Exit Sub
            End If
            dtMain = clsDBFuncationality.GetDataTable(strQry)

            strQry = "With CTETemp as ( Select ROW_NUMBER() OVER (PARTITION BY VCode ORDER BY VCode, DocDate) as RowNo, '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as Address ,'" + Comp_Name + "' as Comp_Name, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, "
            If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
                strQry += "DrAmt  AS DrPDC,CrAmt  AS CrPDC,EffectiveAmt  AS EffPDC,Balance_Amount  AS BalPDC,"
            End If
            strQry += " XXX.* from (" + Environment.NewLine & _
             " Select Due_Date, PO_SRN, vcode,TSPL_VENDOR_MASTER.Vendor_Name as VName, DocNo, DocType, DocDate, DocNarr, ChequeDetails, Final.Currency_Code, DrAmt*" & ddlCurrencyType.SelectedValue & " as DrAmt, CrAmt*" & ddlCurrencyType.SelectedValue & " as CrAmt, Purchase*" & ddlCurrencyType.SelectedValue & " as Purchase, Payments*" & ddlCurrencyType.SelectedValue & " as Payments, DrNote*" & ddlCurrencyType.SelectedValue & " as DrNote, CrNote*" & ddlCurrencyType.SelectedValue & " as CrNote, (CrAmt*" & ddlCurrencyType.SelectedValue & "-DrAmt*" & ddlCurrencyType.SelectedValue & ") as EffectiveAmt, final.Document_Type, Balance_Amount*" & ddlCurrencyType.SelectedValue & " as Balance_Amount, substring(account,LEN(account)-2,3) + ' - ' + TSPL_GL_SEGMENT_CODE.Description  as account, CONVERT(VARCHAR,Posting_Date,103) As Posting_Date,GLDocType, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc , Case When DrAmt>0 Then 0 else 1 End as OrderdrCr, Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then 'Done' Else 'Pending' End as [Reconciliation], Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then CONVERT(VARCHAR,tspl_BankReco_Detail.Reconciliation_Date,103) Else '' End as [Reconciliation_Date] from" + Environment.NewLine & _
              "( " + strtempBaseQryforopening + " ) Final LEFT OUTER JOIN TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =substring(Final.account,LEN(Final.account)-2,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' " + Environment.NewLine & _
               " LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code LEFT OUTER JOIN (select * from tspl_BankReco_Detail where  tspl_BankReco_Detail.Reconciliation_Status='C') tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in (" + clsCommon.GetMulcallString(arr) + ") and CONVERT(date,DocDate ,103) < '" + strFromDate + "' and DocType <>'Mislleneous'  " + strFIlterCheck + "" + Environment.NewLine & _
              " ) XXX " + Environment.NewLine & _
              " ) Select CTETemp.RowNo, CTETemp.RunDate , CTETemp.Address , CTETemp.Comp_Name , CTETemp.FilterFromDate , CTETemp.FilterToDate, CTETemp.Due_Date, CTETemp.PO_SRN, CTETemp.VCode, CTETemp.VName , CTETemp.DocNo, CTETemp.DocType, CONVERT(VARCHAR,CTETemp.DocDate,103) as DocDate, CTETemp.DocNarr,CTETemp.ChequeDetails, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, CTETemp.DrAmt , CTETemp.CrAmt, CTETemp.Purchase, CTETemp.Payments, CTETemp.DrNote, CTETemp.CrNote, CTETemp.Purchase as Purchase1, CTETemp.Payments as Payments1, CTETemp.DrNote as DrNote1, CTETemp.CrNote as CrNote1, SUM(CrAmt-DrAmt) Over (Partition by VCode ORDER BY RowNo) as [Closing], CTETemp.EffectiveAmt , CTETemp.Document_Type , CTETemp.Balance_Amount , CTETemp.account , " & _
              " CTETemp.Posting_Date, CTETemp.GLDocType, CTETemp.Vendor_Group_Code, CTETemp.Group_Desc, CTETemp.OrderdrCr, CTETemp.Reconciliation, CTETemp.Reconciliation_Date  from CTETemp ORDER BY CTETemp.VCode , RowNo "
            dtOpening = clsDBFuncationality.GetDataTable(strQry)

            If dtMain.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data not found", Me.Text)
                gv.DataSource = Nothing
                gv.Columns.Clear()
                gv.Rows.Clear()
                Exit Sub
            Else
                If chkVendorWise.IsChecked Or chkNone.IsChecked OrElse rbntDocWiseMerge.IsChecked Then
                    btnBack.Enabled = True
                Else
                    btnBack.Enabled = False
                End If
            End If

            gvVendorGroup.DataSource = dtCustGrp
            FormatgvVendorGroup()

            gvVendor.DataSource = dtCustomer
            FormatgvVendor()

            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.DataSource = dtMain
            SetGridFormat(False)

            gridHideVisible()


            ReStoreGridDetail()
            ReStoreGridVendor()
            ReStoreGridVendorGrp()
            RestoreGridSummaryRow()
            gv.MasterTemplate.SortDescriptors.Clear()
            If blnRefresh = False Then
                Dim frmCRV As New frmCrystalReportViewer()
                If chkVendorGrupWise.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtCustGrp, "rptVendorLedgerSummary_DEMO", "Vendor Ledger")
                ElseIf chkVendorWise.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtCustomer, "rptVendorLedgerSummary_DEMO", "Vendor Ledger")
                ElseIf (chkNone.IsChecked OrElse rbntDocWiseMerge.IsChecked) AndAlso rbPortrait.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedger", "Vendor Invoice Report")
                ElseIf chkVendorWise.IsChecked = True Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedgerSummaryGroupBy-KDIL", "Vendor Ledger Report")
                ElseIf (chkNone.IsChecked OrElse rbntDocWiseMerge.IsChecked) AndAlso rbLandScape.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedgerLandScape", "Vendor Ledger Report")
                ElseIf (chkNone.IsChecked OrElse rbntDocWiseMerge.IsChecked) Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorLedgerSummaryGroupByDoc-KDIL", "Vendor Ledger Report")
                End If
                frmCRV = Nothing
            End If
            RadPageView1.SelectedPage = RadPageViewPage2

            RadGroupBox1.Enabled = False
            GC.Collect()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function GetVendorBaseQryForVendorCurrency(ByVal strPortrait As Boolean, ByVal strLandscape As Boolean, ByVal IsOnlyForAgainstSalary As Boolean) As String
        Dim strtempBaseQry As String = String.Empty
        Try
            'If Not IsOnlyForAgainstSalary Then
            ''BHA/28/08/18-000492
            Dim strShowReferenceDocNoofAPInvoice As String = "   (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,"

            Dim strTaxRecovarableQuery As String = " - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then " & _
      " ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'  " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end + " & _
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y' " & _
      " then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end) "

            Dim strQryForRMDA As String = " - (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and LEN(ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0 then (Select sum(TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt) from TSPL_TRANSFER_ORDER_HEAD where Status=1 and TSPL_TRANSFER_ORDER_HEAD.RMDA_Code in (select TSPL_SRN_HEAD.RMDA_No from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN where TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  )) else 0 end )"
            'strtempBaseQry = " select TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.vendor_code as VCode,TSPL_VENDOR_INVOICE_HEAD.vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total else 0 end as CrAmt, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('D') then Document_Total else 0 end as DrAmt, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code from tspl_vendor_invoice_head left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_vendor_invoice_head.Vendor_Code where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0 AND   TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  " + Environment.NewLine & _
            'strtempBaseQry = " select TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.vendor_code as VCode,TSPL_VENDOR_INVOICE_HEAD.vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total - (case when TSPL_VENDOR_MASTER.GSTRegistered =0 then TSPL_VENDOR_INVOICE_HEAD.Total_Tax else 0 end) else 0 end as CrAmt, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN ('D') then Document_Total - (case when TSPL_VENDOR_MASTER.GSTRegistered =0 then TSPL_VENDOR_INVOICE_HEAD.Total_Tax else 0 end) else 0 end as DrAmt, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total - (case when TSPL_VENDOR_MASTER.GSTRegistered =0 then TSPL_VENDOR_INVOICE_HEAD.Total_Tax else 0 end) Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total - (case when TSPL_VENDOR_MASTER.GSTRegistered =0 then TSPL_VENDOR_INVOICE_HEAD.Total_Tax else 0 end) Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total - (case when TSPL_VENDOR_MASTER.GSTRegistered =0 then TSPL_VENDOR_INVOICE_HEAD.Total_Tax else 0 end) Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code from tspl_vendor_invoice_head left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_vendor_invoice_head.Vendor_Code where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0 AND   TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  " + Environment.NewLine
            strtempBaseQry = " select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.vendor_code as VCode,TSPL_VENDOR_INVOICE_HEAD.vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total " + strTaxRecovarableQuery + " else 0 end as CrAmt, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN ('D') then Document_Total " + strTaxRecovarableQuery + " else 0 end as DrAmt, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total " + strTaxRecovarableQuery + " Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total " + strTaxRecovarableQuery + " Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total " + strTaxRecovarableQuery + " Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code from tspl_vendor_invoice_head left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_vendor_invoice_head.Vendor_Code where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0 AND   TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  " + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            " Select Reference_Doc_No,Emp_type, Emp_Adv_Type,Due_Date, PO_SRN, VCode, VName, DocNo, DocType, DocDate, DocNarr, ChequeDetails, Currency_Code, ConvRate, CrAmt, DrAmt, "
            If chkVendorWise.IsChecked Or chkVendorGrupWise.IsChecked Then
                strtempBaseQry += "case when DocType='Pur.Invoice' then  CrAmt-DrAmt else 0 end as Purchase,"
            Else
                strtempBaseQry += " CrAmt-DrAmt as Purchase,"
            End If
            strtempBaseQry += " 0 as Payments, 0 as drNote, 0 as CrNote, Document_Type, Balance_Amount, account, Posting_Date, GLDocType, Comp_Code from (" + Environment.NewLine & _
            " select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date ,CASE WHEN isnull(TSPL_PI_HEAD.Against_PO,'') <> '' THEN isnull(TSPL_PI_HEAD.Against_PO,'')  ELSE ''  + isnull(TSPL_PI_HEAD.Against_SRN,'') END  AS PO_SRN ,"
            'If strPortrait = True Then
            '    strtempBaseQry += " TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date)+ (SELECT ' & Items Are- '+STUFF((SELECT ', ' + t2.Item_Desc+'('+Convert(Varchar,COnvert(Decimal(18,2),t2.Item_Cost))+' Rs)' FROM TSPL_PI_DETAIL t2 WHERE t2.PI_No = TSPL_PI_HEAD.PI_No FOR XML PATH ('')),1,2,'')) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end) as CrAmt, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt" + strQryForRMDA + " as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1" + Environment.NewLine
            'ElseIf strLandscape = True Then
            '    strtempBaseQry += " TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end) as CrAmt,  (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt " + strQryForRMDA + " as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_vendor_invoice_head.Vendor_Code where TSPL_PI_HEAD.Status=1 AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'" + Environment.NewLine
            'End If
            If strPortrait = True Then
                strtempBaseQry += " TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date)+ (SELECT ' & Items Are- '+STUFF((SELECT ', ' + t2.Item_Desc+'('+Convert(Varchar,COnvert(Decimal(18,2),t2.Item_Cost))+' Rs)' FROM TSPL_PI_DETAIL t2 WHERE t2.PI_No = TSPL_PI_HEAD.PI_No FOR XML PATH ('')),1,2,'')) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " else 0 end) as CrAmt, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt" + strQryForRMDA + " as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1" + Environment.NewLine
            ElseIf strLandscape = True Then
                strtempBaseQry += " TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " else 0 end) as CrAmt,  (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt " + strQryForRMDA + " as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_vendor_invoice_head.Vendor_Code where TSPL_PI_HEAD.Status=1 AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'" + Environment.NewLine
            End If

            strtempBaseQry += ") XXX" + Environment.NewLine & _
             " UNION ALL" + Environment.NewLine & _
             " select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,isnull(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, Case When TSPL_REMITTANCE.Document_Type IN ('I','C','D') Then TSPL_VENDOR_INVOICE_HEAD.Description Else TSPL_PAYMENT_HEADER.Entry_Desc End as DocNarr, '', Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE Else TSPL_PAYMENT_HEADER.CURRENCY_CODE End as Currency_Code, Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.ConvRate Else TSPL_PAYMENT_HEADER.ConvRate End as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then "
            'If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
            '    strtempBaseQry += " 0 "
            'Else
            '    strtempBaseQry += " Actual_Total_TDS "
            'End If
            strtempBaseQry += " Actual_Total_TDS "
            strtempBaseQry += " else 0 END, case when TSPL_REMITTANCE.Document_Type ='I' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as Purchase, case when TSPL_REMITTANCE.Document_Type IN "
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                strtempBaseQry += "('I','D','C')"
            Else
                strtempBaseQry += "('I','D')"
            End If
            strtempBaseQry += " AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then 0 else Actual_Total_TDS END as Payments, case when TSPL_REMITTANCE.Document_Type ='D' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, Case When ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'')<>'' Then TSPL_VENDOR_INVOICE_HEAD.Posting_Date Else TSPL_PAYMENT_HEADER.Payment_Post_Date End as Posting_Date,case when TSPL_REMITTANCE.Document_Type in ('C') then 'AP-CN' when TSPL_REMITTANCE.Document_Type in ('I') then 'AP-IN' when TSPL_REMITTANCE.Document_Type in ('D') then 'AP-DN' else case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE Left Outer Join TSPL_VENDOR_INVOICE_HEAD ON TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_vendor_invoice_head.Vendor_Code where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 and TSPL_REMITTANCE.Is_TDS_Provision = 'N' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1 AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'   " + Environment.NewLine &
            " UNION ALL" + Environment.NewLine &
            " select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,isnull(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name ,TSPL_REMITTANCE.Document_No ,'TDS REVERSE' as [DocType],convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)as Document_Date,'', '', TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end AS CrAmt, 0 as DrAmt, 0 as Purchase, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS*-1 else 0 end as Payments, 0 as DrNote, 0 as CrNote, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No inner join TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_REMITTANCE.Vendor_Code where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P' and Branch_GL_AC  is not null and Actual_Total_TDS<>0 and TSPL_REMITTANCE.Is_TDS_Provision = 'N' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' " + Environment.NewLine &
            " UNION ALL" + Environment.NewLine
            If objCommonVar.IsMultiCurrencyCompany = True Then
                strtempBaseQry += "select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,isnull(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate," & _
                " case when TSPL_PAYMENT_HEADER.Payment_Type IN ('RC','AD') then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then case when TSPL_VENDOR_INVOICE_HEAD.Document_Type  in ('D') then  TSPL_PAYMENT_DETAIL.Applied_Amount * -1 else  TSPL_PAYMENT_DETAIL.Applied_Amount  end ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, " & _
            "case when TSPL_PAYMENT_HEADER.Payment_Type IN ('RC','AD') then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type IN('OA','AV') then -1*TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then -1*TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type='AD' THEN (sELECT Location_GL_Code  FROM TSPL_PAYMENT_HEADER PR WHERE PR.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment )  ELSE substring(TSPL_PAYMENT_HEADER.Debit_Account , len(TSPL_PAYMENT_HEADER.Debit_Account )-2,3) END End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE" & _
            " LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No" & _
            " LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No" & _
            " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No" & _
            " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code " & _
            " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code" & _
            " where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_PAYMENT_HEADER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' " & IIf(chkIncludeApplyDocument.Checked = False, " and TSPL_PAYMENT_HEADER.Payment_Type<>'AD' ", "") & " " + Environment.NewLine & _
            " --------- for bank reverse entry transactions --------------- " + Environment.NewLine
            Else
                strtempBaseQry += " select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,isnull(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount ELSE -1*TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'   " + Environment.NewLine
            End If
            strtempBaseQry += " UNION ALL" + Environment.NewLine & _
            " select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, VC_Code as VCode, VC_Name as VName, TSPL_VCGL_Head.Document_No as DocNo,case when TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end as DocType,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Remarks as DocNarr, '' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE as CURRENCY_CODE,TSPL_VENDOR_INVOICE_HEAD.ConvRate as ConvRate, (case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt, 0 as Purchase, 0 as Payments, case when Amount_Type='Cr' then Amount else 0 end as DrNote, case when Amount_Type='Dr' then Amount else 0 end as CrNote, (case when  TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end ) as Document_Type ,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date,'VC-GL' as GLDocType,TSPL_VCGL_Head.Comp_Code from TSPL_VCGL_Head LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_VCGL_Head.VC_Code where TSPL_VCGL_Head.Document_Type='v' and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  " + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            " select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_VCGL_Detail.VCGL_Code as VCode, TSPL_VCGL_Detail.VCGL_Name as VName, TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Detail.Row_Type as DocType,CONVERT(date,TSPL_VCGL_Head.Document_Date,103) as DocDate,TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE as CURRENCY_CODE,TSPL_VENDOR_INVOICE_HEAD.ConvRate as ConvRate, TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt, 0 as Purchase, 0 as Payments, TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as CrNote, TSPL_VCGL_Detail.Row_Type as Document_Type,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date ,'VC-GL' as GLDocType, TSPL_VCGL_Head.Comp_Code from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_VCGL_Head.VC_Code where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' " + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            "  select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE as CURRENCY_CODE,TSPL_VENDOR_INVOICE_HEAD.ConvRate as ConvRate, 0 as CrAmt,TSPL_Payment_Adjustment_Detail.Amount  as DrAmt, 0 as Purchase, 0 as Payments, TSPL_Payment_Adjustment_Detail.Amount as DrNote, 0 as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Detail  left outer join  TSPL_Payment_Adjustment_Header on TSPL_Payment_Adjustment_Header.Adjustment_No =TSPL_Payment_Adjustment_Detail .Adjustment_No  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_Payment_Adjustment_Header.Vendor_No where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y'  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' and TSPL_Payment_Adjustment_Header.Adjust_Type='P' " + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            "  select " & strShowReferenceDocNoofAPInvoice & " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE as CURRENCY_CODE,TSPL_VENDOR_INVOICE_HEAD.ConvRate as ConvRate, TSPL_Payment_Adjustment_Detail.Amount as CrAmt,0  as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, TSPL_Payment_Adjustment_Detail.Amount as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Detail  left outer join  TSPL_Payment_Adjustment_Header on TSPL_Payment_Adjustment_Header.Adjustment_No =TSPL_Payment_Adjustment_Detail .Adjustment_No  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_Payment_Adjustment_Header.Vendor_No where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y'  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' and TSPL_Payment_Adjustment_Header.Adjust_Type='R' " + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            " Select max(Reference_Doc_No) as Reference_Doc_No,MAX(Emp_type) as Emp_type, MAX(Emp_Adv_Type) as Emp_Adv_Type,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (" + Environment.NewLine & _
            " select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,  ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type, NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_payment_header.Vendor_Code Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_PAYMENT_HEADER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  " + Environment.NewLine & _
            " ) XX Group By XX.account, XX.DocNo" + Environment.NewLine

            If chkIncludeApplyDocument.Checked Then
                strtempBaseQry += " UNION ALL" + Environment.NewLine & _
            " select  '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,  ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'IM' else null end as DocType , COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then TSPL_PAYMENT_DETAIL.Applied_Amount * -1 else TSPL_PAYMENT_DETAIL.Applied_Amount  end AS CrAmt , 0  AS  DrAmt ,0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote," + Environment.NewLine & _
            " TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type='AD' THEN (sELECT Location_GL_Code  FROM TSPL_PAYMENT_HEADER PR WHERE PR.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment )  ELSE substring(TSPL_PAYMENT_HEADER.Debit_Account , len(TSPL_PAYMENT_HEADER.Debit_Account )-2,3) END as account,TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code " + Environment.NewLine & _
            " from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_payment_header.Vendor_Code Where tspl_payment_header.Payment_Type='AD' AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  " + Environment.NewLine & _
            " --------------- INVOICE AGAINST APPLY DOCUMENT" + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            " SELECT  max(Reference_Doc_No) as Reference_Doc_No, MAX(Emp_type) as Emp_type, MAX(Emp_Adv_Type) as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN,  MAX(VCode) AS VCode,MAX(VName) as VName,DocNo AS DocNo,MAX(DocType) AS DocType,MAX(DocDate) AS DocDate,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(CrAmt) AS CrAmt,SUM(DrAmt) AS DrAmt,SUM(Purchase) AS Purchase,SUM(Payments) AS Payments  ,MAX(DrNote) AS DrNote,MAX(CrNote) AS CrNote, MAX(Document_Type) AS Document_Type,SUM(Balance_Amount) AS Balance_Amount,account , MAX(Posting_Date) AS Posting_Date,MAX(GLDocType) AS GLDocType,MAX(Comp_Code) AS Comp_Code FROM (select  '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,  ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'IM' else null end as DocType , COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr,((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, 0  AS CrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then TSPL_PAYMENT_DETAIL.Applied_Amount * -1 else TSPL_PAYMENT_DETAIL.Applied_Amount  end AS  DrAmt," + Environment.NewLine & _
            " 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, " + Environment.NewLine & _
            " TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account,TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code  " + Environment.NewLine & _
            " from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_payment_header.Vendor_Code Where tspl_payment_header.Payment_Type='AD' AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' " + Environment.NewLine & _
            " )INV  GROUP BY  DocNo,account  " + Environment.NewLine & _
            " ------- APPLY DOCUMENT ENTRY WHEN LOCATION IS CHANGED OF APPLIED DOCUMENT WITH DOCUMENT LOCATION " + Environment.NewLine & _
            " UNION ALL " + Environment.NewLine & _
            " SELECT  max(Reference_Doc_No) as Reference_Doc_No, MAX(Emp_type) as Emp_type, MAX(Emp_Adv_Type) as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN,  MAX(VCode) AS VCode,MAX(VName) as VName,DocNo AS DocNo,MAX(DocType) AS DocType,MAX(DocDate) AS DocDate,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(CrAmt) AS CrAmt,SUM(DrAmt) AS DrAmt,SUM(Purchase) AS Purchase,SUM(Payments) AS Payments  ,MAX(DrNote) AS DrNote,MAX(CrNote) AS CrNote, MAX(Document_Type) AS Document_Type,SUM(Balance_Amount) AS Balance_Amount,account , MAX(Posting_Date) AS Posting_Date,MAX(GLDocType) AS GLDocType,MAX(Comp_Code) AS Comp_Code FROM ( " + Environment.NewLine & _
            " select  '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,  ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,'Reverse Payment' as DocType , Convert(date,TSPL_BANK_REVERSE.Reversal_Date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr,((TSPL_PAYMENT_HEADER.Cheque_No) + (case when TSPL_PAYMENT_HEADER.Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then TSPL_PAYMENT_DETAIL.Applied_Amount * -1 else TSPL_PAYMENT_DETAIL.Applied_Amount  end  AS CrAmt,0 AS  DrAmt, " + Environment.NewLine & _
            " 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount,  " + Environment.NewLine & _
            " TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account,TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code  " + Environment.NewLine & _
            " from TSPL_BANK_REVERSE LEFT OUTER JOIN tspl_payment_header ON tspl_payment_header.Payment_No =TSPL_BANK_REVERSE.Document_No  LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_payment_header.Vendor_Code Where" + Environment.NewLine & _
            " TSPL_BANK_REVERSE.Reverse_Document='Payments' AND TSPL_BANK_REVERSE.Post ='P' AND tspl_payment_header.Payment_Type='AD' AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'" + Environment.NewLine & _
            " )INV  GROUP BY  DocNo,account " + Environment.NewLine & _
            " -------------- FOR BANK REVERSE ENTRY WHEN LOCATION IS CHANGED OF APPLIED DOCUMENT WITH DOCUMENT LOCATION" + Environment.NewLine

            End If
            ''richa 24/10/2017
            'If IsOnlyForAgainstSalary = True Then ERO/29/02/20-001193
            strtempBaseQry += " UNION ALL" + Environment.NewLine & _
           " Select max(Reference_Doc_No) as Reference_Doc_No, MAX(Emp_type) as Emp_type, 'S' as Emp_Adv_Type,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (" + Environment.NewLine & _
             " select  '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_payment_header.Vendor_Code Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_PAYMENT_HEADER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  " + Environment.NewLine & _
                 " ) XX Group By XX.account, XX.DocNo" + Environment.NewLine & _
             " UNION ALL" + Environment.NewLine & _
                 " select  '' as Reference_Doc_No,ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type  ,'S') as Emp_Adv_Type,TSPL_PAYMENT_HEADER.Payment_Date As Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, TSPL_PAYMENT_HEADER.Entry_Desc as DocNarr, '', TSPL_PAYMENT_HEADER.CURRENCY_CODE as Currency_Code, TSPL_PAYMENT_HEADER.ConvRate as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else 0 END as DrAmt, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else Actual_Total_TDS END as Payments, 0 as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_REMITTANCE.Vendor_Code where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' " + Environment.NewLine & _
             " UNION ALL" + Environment.NewLine & _
                 " select  '' as Reference_Doc_No,ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'S') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE TSPL_BANK_REVERSE.Amount*-1 end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' "
            'End If
            ''---------------

            'Else
            '    strtempBaseQry = " Select NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (" + Environment.NewLine & _
            '    " select  NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =tspl_payment_header.Vendor_Code Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_PAYMENT_HEADER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  " + Environment.NewLine & _
            '    " ) XX Group By XX.account, XX.DocNo" + Environment.NewLine & _
            '" UNION ALL" + Environment.NewLine & _
            '    " select TSPL_PAYMENT_HEADER.Payment_Date As Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, TSPL_PAYMENT_HEADER.Entry_Desc as DocNarr, '', TSPL_PAYMENT_HEADER.CURRENCY_CODE as Currency_Code, TSPL_PAYMENT_HEADER.ConvRate as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else 0 END as DrAmt, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else Actual_Total_TDS END as Payments, 0 as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_REMITTANCE.Vendor_Code where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1  AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' " + Environment.NewLine & _
            '" UNION ALL" + Environment.NewLine & _
            '    " select null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE TSPL_BANK_REVERSE.Amount*-1 end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   AND TSPL_VENDOR_MASTER.CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "' "

            'End If
            strtempBaseQryforopening = strtempBaseQry
            If clsCommon.CompairString(clsCommon.myCstr(ddlCurrencyType.SelectedValue), "") = CompairStringResult.Equal Then
                LoadCurrencyType()
            End If
            If (rbtnDocWise.IsChecked = True) Then
                If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                    strtempBaseQry = "  Select InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  * (case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end) as  CrAmt ," + Environment.NewLine &
                    " case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine &
                    "case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then " + Environment.NewLine &
                    "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else " + Environment.NewLine &
                    "case when isnull((DrAmt),0)>0 then (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else 0 end end else " + Environment.NewLine &
                    " case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT ),0) else  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else " + Environment.NewLine &
                    " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then " + Environment.NewLine &
                    "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end else " + Environment.NewLine &
                    "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)  else  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  end end end as DrAmt, " + Environment.NewLine &
                    " case when InnQuery.DocType='Pur.Invoice' then InnQuery.CrAmt-InnQuery.DrAmt else 0 end as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  (" & strtempBaseQry & " ) InnQuery " + Environment.NewLine &
                    "  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_M1ASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code  "
                Else
                    strtempBaseQry = "  Select isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) as EXCHANGE_GAIN_AMT,isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT ),0) as EXCHANGE_LOSS_AMT, InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  * (case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end) - case when (DocType)='Reverse Payment' then  (Select isnull((PH.EXCHANGE_LOSS_AMT  ),0) from TSPL_PAYMENT_HEADER PH where PH.Payment_No =(  Select Document_No   from TSPL_BANK_REVERSE where Reverse_Code =InnQuery.DocNo )) else 0 end as  CrAmt ," + Environment.NewLine &
                   " case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine &
                   "case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then " + Environment.NewLine &
                   "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else " + Environment.NewLine &
                   "case when isnull((DrAmt),0)>0 then (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else 0 end end else " + Environment.NewLine &
                   " case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT ),0) ELSE  (DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else " + Environment.NewLine &
                   " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then " + Environment.NewLine &
                   "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then CASE WHEN ((DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")-isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0))<0 THEN 0 ELSE (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")-isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) END  else (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0)  end else " + Environment.NewLine &
                   "case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ") +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)  else  (DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " InnQuery." & ddlCurrencyType.SelectedValue & "") & ")  end end end as DrAmt, " + Environment.NewLine &
                   " InnQuery.CrAmt-InnQuery.DrAmt  as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  (" & strtempBaseQry & " ) InnQuery " + Environment.NewLine &
                   "  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code  "

                End If




            Else
                strtempBaseQry = "  Select  InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate,  "
                If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                    strtempBaseQry += "  CONVERT(DECIMAL(18,2),CASE WHEN InnQuery.DocType NOT IN ('Pur.Invoice','Receipt')  THEN case when InnQuery.DocType not in ('TDS','AP Invoice') THEN InnQuery.CrAmt  when  InnQuery.DocType in ('AP Invoice') and (Select Document_Type  from tspl_vendor_invoice_head where document_no=InnQuery.DocNo)='C' then InnQuery.CrAmt ELSE CASE WHEN InnQuery.DocType in ('TDS') and (Select ISNULL(TSPL_REMITTANCE.Document_Type,'')  from TSPL_REMITTANCE LEFT OUTER join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_REMITTANCE.Document_No  and  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' where TSPL_REMITTANCE.Document_No=InnQuery.DocNo  )='I' or (Select ISNULL(TSPL_REMITTANCE.Document_Type,'')  from TSPL_REMITTANCE LEFT OUTER join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_REMITTANCE.Document_No  and  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' where TSPL_REMITTANCE.Document_No=InnQuery.DocNo   )='C' then (Select ISNULL (Actual_Total_TDS ,0)  from TSPL_REMITTANCE where TSPL_REMITTANCE.Document_No=InnQuery.DocNo  ) * -1 else 0 end END ELSE 0 END) as CrAmt ," & _
                      "  CONVERT(DECIMAL(18,2),case when InnQuery.DocType not in ('Payment','On Account','Advance','Receipt','Adjustment') then case when InnQuery.DocType not in ('AP Invoice','TDS') then InnQuery.DrAmt WHEN InnQuery.DocType in ('TDS') and   (Select ISNULL(TSPL_REMITTANCE.Document_Type,'')  from TSPL_REMITTANCE LEFT OUTER join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_REMITTANCE.Document_No  and  isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' where TSPL_REMITTANCE.Document_No=InnQuery.DocNo  )='D' then (Select ISNULL (Actual_Total_TDS ,0)  from TSPL_REMITTANCE where TSPL_REMITTANCE.Document_No=InnQuery.DocNo  ) * -1 else case when  InnQuery.DocType in ('AP Invoice') AND  (Select Document_Type  from tspl_vendor_invoice_head where document_no=InnQuery.DocNo AND ISNULL(Against_PurchaseReturn_No,'')='')='D' then InnQuery.DrAmt else 0 end end else 0 end) DrAmt, " & _
                      "  CONVERT(DECIMAL(18,2),case when InnQuery.DocType not in ('Payment','On Account','Advance','Receipt','Debit Note','Reverse Payment','Credit Note','EXC','IM') then case when InnQuery.DocType not in ('AP Invoice','TDS') then InnQuery.Purchase else case when  InnQuery.DocType in ('AP Invoice') and (Select Document_Type  from tspl_vendor_invoice_head where document_no=InnQuery.DocNo)='I'	 then InnQuery.Purchase when  InnQuery.DocType in ('AP Invoice')  and (Select Document_Type  from tspl_vendor_invoice_head where document_no=InnQuery.DocNo AND ISNULL(Against_PurchaseReturn_No,'')<>'')='D' then InnQuery.Purchase *-1  else 0 end end else 0 end) Purchase, " & _
                      "  CONVERT(DECIMAL(18,2),case when InnQuery.DocType  in ('Payment','On Account','Advance','Receipt','TDS','EXC','IM') then InnQuery.Payments  else 0 end) as Payments , "
                Else
                    strtempBaseQry += "  CONVERT(DECIMAL(18,2),InnQuery.CrAmt) as CrAmt, CONVERT(DECIMAL(18,2),InnQuery.DrAmt) as DrAmt, CONVERT(DECIMAL(18,2),InnQuery.Purchase) as Purchase, CONVERT(DECIMAL(18,2),InnQuery.Payments) as Payments , "
                End If
                strtempBaseQry += " InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code from " & _
                "(Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  as  CrAmt ," + Environment.NewLine & _
                " InnQuery.DrAmt as DrAmt,InnQuery.CrAmt-InnQuery.DrAmt as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  (" & strtempBaseQryforopening & " ) InnQuery " + Environment.NewLine & _
                "  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code  " & _
                  " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =InnQuery.VCode where TSPL_VENDOR_MASTER .CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  ) InnQuery"

                ''richa KDI/15/05/19-000452 ,KDI/15/05/19-000453
                If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                    strtempBaseQryforopeningForMIS = "  Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate,  " & _
                    " CONVERT(DECIMAL(18,2),InnQuery.CrAmt) AS CrAmt,CONVERT(DECIMAL(18,2),InnQuery.DrAmt) AS DrAmt,0 as Purchase,0 as Payments, " & _
                    " InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code from " & _
                    " (Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt as  CrAmt ," + Environment.NewLine & _
                    " InnQuery.DrAmt AS DrAmt ,InnQuery.CrAmt-InnQuery.DrAmt as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  (" & strtempBaseQryforopening & " ) InnQuery " + Environment.NewLine & _
                    "  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code  " & _
                    " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =InnQuery.VCode where TSPL_VENDOR_MASTER .CURRENCY_CODE <>'" & objCommonVar.BaseCurrencyCode & "'  ) InnQuery"
                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strtempBaseQry

    End Function

    Private Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        blnRefresh = False
        ' Print()
        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            PrintForVendorCurrency()
        Else
            Print()
        End If
    End Sub

    Private Sub chkVndrAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVndrAll.ToggleStateChanged
        cbgVndrGroup.Enabled = Not chkVndrAll.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged, chkVendorSelect.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked

        If chkVendorSelect.IsChecked Then
            SetMultiCurrencyVisibility()
        End If
    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        Dim DocNo As String
        Dim DocType As String
        'If Not chkSummary.Checked = True Then
        DocNo = clsCommon.myCstr(gv.CurrentRow.Cells("DocNo").Value)
        DocType = clsCommon.myCstr(gv.CurrentRow.Cells("GLDocType").Value)
        If clsCommon.CompairString(DocType, "AP-PY") = CompairStringResult.Equal Or clsCommon.CompairString(DocType, "AP-MI") = CompairStringResult.Equal Then
            Dim frm As New FrmPaymentNew
            frm.SetUserMgmt(clsUserMgtCode.PaymentEntryNew)
            frm.Show()
            frm.LoadData(DocNo, NavigatorType.Current)
        ElseIf clsCommon.CompairString(DocType, "AP-IN") = CompairStringResult.Equal Then
            Dim frm As New FrmAPInvoiceEntry
            frm.SetUserMgmt(clsUserMgtCode.mbtnAPInvoiceEntry)
            frm.Show()
            frm.LoadData(DocNo)
        ElseIf clsCommon.CompairString(DocType, "RV-TA") = CompairStringResult.Equal Then
            Dim frm As New frmReverseTransaction(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.SetUserMgmt(clsUserMgtCode.reverseTransaction)
            frm.Show()
            frm.fndreversecode.Value = DocNo
            frm.funFill4()
        ElseIf clsCommon.CompairString(DocType, "VC-GL") = CompairStringResult.Equal Then
            Dim frm As New frmVCGLEntry
            frm.SetUserMgmt(clsUserMgtCode.mbtnVCGLEntry)
            frm.Show()
            frm.LoadData(DocNo)
        End If
        'End If

    End Sub

    Private Sub gridHideVisible()
        gv.Visible = False
        gvVendor.Visible = False
        gvVendorGroup.Visible = False
        If chkVendorGrupWise.IsChecked Then
            gvVendorGroup.Visible = True
            btnBack.Enabled = False
        ElseIf chkVendorWise.IsChecked Then
            gvVendor.Visible = True
            btnBack.Enabled = True
        Else
            gv.Visible = True
            btnBack.Enabled = True
        End If
    End Sub

    Private Sub gvVendorGroup_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvVendorGroup.CellDoubleClick
        If isRunDoubleClick Then
            Try
                If clsCommon.myLen(gvVendorGroup.CurrentRow.Cells("Vendor_Group_Code").Value) > 0 Then
                    'dvTemp = New DataView(dtCustomer)
                    'dvTemp.RowFilter = "Vendor_Group_Code='" + gvVendorGroup.CurrentRow.Cells("Vendor_Group_Code").Value + "'"
                    'gvVendor.DataSource = dvTemp.ToTable()
                    chkVendorWise.IsChecked = True
                    Dim arrVendorGroup As New ArrayList
                    arrVendorGroup.Add(gvVendorGroup.CurrentRow.Cells("Vendor_Group_Code").Value)
                    txtVendorGroup.arrValueMember = arrVendorGroup
                    ' FormatgvVendor()
                    If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
                        PrintForVendorCurrency()
                    Else
                        Print()
                    End If
                    gvVendor.Visible = True
                    gvVendorGroup.Visible = False
                    gv.Visible = False
                    RestoreGridSummaryRow()
                    btnBack.Enabled = True
                    PageSetupReport_ID = GetReportID()
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Private Sub gvVendor_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvVendor.CellDoubleClick
        Try
            If isRunDoubleClick Then
                If clsCommon.myLen(gvVendor.CurrentRow.Cells("VCode").Value) > 0 Then
                    'If e.Column Is gvVendor.Columns("VCode") Or e.Column Is gvVendor.Columns("VName") Then
                    '    dvTemp = New DataView(dtMain)
                    '    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "'"
                    '    gv.DataSource = dvTemp.ToTable()
                    '    SetGridFormat(False)
                    'ElseIf e.Column Is gvVendor.Columns("OpngBal") Then
                    '    dvTemp = New DataView(dtOpening)
                    '    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "'"
                    '    gv.DataSource = dvTemp.ToTable()
                    '    SetGridFormat(True)
                    'ElseIf e.Column Is gvVendor.Columns("Purchase") Then
                    '    dvTemp = New DataView(dtMain)
                    '    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "' AND ISNULL(Purchase,0)<>0"
                    '    gv.DataSource = dvTemp.ToTable()
                    '    SetGridFormat(True)
                    'ElseIf e.Column Is gvVendor.Columns("Payments") Then
                    '    dvTemp = New DataView(dtMain)
                    '    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "' AND ISNULL(Payments,0)<>0"
                    '    gv.DataSource = dvTemp.ToTable()
                    '    SetGridFormat(True)
                    'ElseIf e.Column Is gvVendor.Columns("DrNote") Then
                    '    dvTemp = New DataView(dtMain)
                    '    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "' AND ISNULL(DrNote,0)<>0"
                    '    gv.DataSource = dvTemp.ToTable()
                    '    SetGridFormat(True)
                    'ElseIf e.Column Is gvVendor.Columns("CrNote") Then
                    '    dvTemp = New DataView(dtMain)
                    '    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "' AND ISNULL(CrNote,0)<>0"
                    '    gv.DataSource = dvTemp.ToTable()
                    '    SetGridFormat(True)
                    'End If

                    chkNone.IsChecked = True
                    Dim arrVendor As New ArrayList
                    arrVendor.Add(gvVendor.CurrentRow.Cells("VCode").Value)
                    txtVendor.arrValueMember = arrVendor
                    ' FormatgvVendor()
                    If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
                        PrintForVendorCurrency()
                    Else
                        Print()
                    End If

                    gvVendorGroup.Visible = False
                    gvVendor.Visible = False
                    gv.Visible = True
                    RestoreGridSummaryRow()
                    btnBack.Enabled = True
                    PageSetupReport_ID = GetReportID()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If gv.Visible = True Then
            gvVendor.Visible = True
            gvVendorGroup.Visible = False
            gv.Visible = False
        ElseIf gvVendor.Visible = True Then
            gvVendorGroup.Visible = True
            gvVendor.Visible = False
            gv.Visible = False
            btnBack.Enabled = False
        End If
        PageSetupReport_ID = GetReportID()
    End Sub

    Private Sub gv_CellDoubleClick1(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If isRunDoubleClick Then
            'If chkSummary.Checked = False Then
            If clsCommon.myLen(e.Row.Cells.Item("DocNo").Value) > 0 Then
                Dim SoucrCode As String = clsCommon.myCstr(gv.Rows(e.RowIndex).Cells.Item("DocType").Value)
                Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("DocNo").Value)
                'Pur.Invoice()            Vendor,TDS, ,
                If SoucrCode = "Credit Note" Or SoucrCode = "AP Invoice" Or SoucrCode = "Pur.Invoice" Then
                    If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select isnull(is_For_TDS,0) from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & DocNo & "'"), "1") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnAPInvoiceEntryTDS, DocNo)
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, DocNo)
                    End If
                ElseIf SoucrCode = "Advance" Or SoucrCode = "Receipt" Or SoucrCode = "On Account" Or SoucrCode = "Payment" Or SoucrCode = "IM" Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, DocNo)
                    'ElseIf SoucrCode = "Pur.Invoice" Then
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, DocNo)
                ElseIf SoucrCode = "Vendor" Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, DocNo)
                ElseIf SoucrCode = "TDS" Or SoucrCode = "Debit Note" Then
                    If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select isnull(is_For_TDS,0) from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & DocNo & "'"), "1") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnAPInvoiceEntryTDS, DocNo)
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, DocNo)
                    End If
                Else
                    Return
                End If
                PageSetupReport_ID = GetReportID()
            End If
            'End If
        End If
    End Sub

    Private Sub rbtnchildall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnchildall.ToggleStateChanged, rbtnchildslct.ToggleStateChanged
        If rbtnchildall.IsChecked Then
            cbgchild.Enabled = False
        Else
            cbgchild.Enabled = True
        End If
    End Sub

    Private Sub cbgVendor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cbgVendor.Validating
        Try
            If cbgVendor.Enabled = True AndAlso cbgVendor.CheckedValue.Count > 0 Then
                Dim values As String = clsCommon.GetMulcallString(cbgVendor.CheckedValue)

                Dim qry As String = "select Vendor_Code as Code,vendor_name as Description from tspl_vendor_master where parent_vendor_code in (" + values + ") and is_parent_vendor<>'1'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    cbgchild.DataSource = dt
                    cbgchild.DisplayMember = "Description"
                    cbgchild.ValueMember = "Code"
                    rbtnchildslct.IsChecked = True
                Else
                    cbgchild.DataSource = Nothing
                    rbtnchildall.IsChecked = True
                End If
            Else
                cbgchild.DataSource = Nothing
                rbtnchildall.IsChecked = True
            End If
        Catch ex As Exception
            cbgchild.DataSource = Nothing
        End Try
    End Sub

    Private Sub txtCurrencyCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
    End Sub

    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            txtCurrencyCode.Enabled = True
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count > 0 Then
                strq = "select top 1 currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE in (" & clsCommon.GetMulcallString(cbgVendor.CheckedValue) & ")"
                Me.txtCurrencyCode.Value = clsDBFuncationality.getSingleValue(strq).ToString
            End If
        Else
            txtCurrencyCode.Enabled = False
        End If
    End Sub

    Sub LoadVendor()
        Dim qry As String = "select TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Parent_Vendor_Code as [Parent Code],P1.Vendor_Name as [Parent Name]   from TSPL_VENDOR_MASTER  Left Outer Join TSPL_VENDOR_MASTER P1 on TSPL_VENDOR_MASTER.Parent_Vendor_Code =P1.Vendor_Code WHERE TSPL_VENDOR_MASTER.Status='N'   order by TSPL_VENDOR_MASTER.Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub

    Sub LoadLocation()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub
    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
    Private Sub chkVndrAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgVndrGroup.Enabled = False
    End Sub
    Private Sub chkVndrSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgVndrGroup.Enabled = True
    End Sub

    Private Sub txtVendorGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendorGroup._My_Click
        strQry = "select Ven_Group_Code as Code, Group_Desc as Name from TSPL_VENDOR_GROUP order by Ven_Group_Code "
        txtVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorGrpSelector@VendorLedger", strQry, "Code", "Name", txtVendorGroup.arrValueMember, txtVendorGroup.arrDispalyMember)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        strQry = "select TSPL_VENDOR_MASTER.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,case when TSPL_VENDOR_MASTER.Status='Y' then 'N' else 'Y' end as Active,TSPL_VENDOR_MASTER.Parent_Vendor_Code as [Parent Code],P1.Vendor_Name as [Parent Name]   from TSPL_VENDOR_MASTER  Left Outer Join TSPL_VENDOR_MASTER P1 on TSPL_VENDOR_MASTER.Parent_Vendor_Code =P1.Vendor_Code  where 2=2  order by TSPL_VENDOR_MASTER.Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorSelector@VendorLedger", strQry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtChildVendor__My_Click(sender As Object, e As EventArgs) Handles txtChildVendor._My_Click
        Dim values As String = clsCommon.GetMulcallString(cbgVendor.CheckedValue)
        strQry = "select Vendor_Code as Code,vendor_name as Name from tspl_vendor_master where parent_vendor_code in (" + values + ") and is_parent_vendor<>'1'"
        txtChildVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorGrpSelector@VendorLedger", strQry, "Code", "Name", txtChildVendor.arrValueMember, txtChildVendor.arrDispalyMember)
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

    Private Sub txtAccountSet__My_Click(sender As Object, e As EventArgs) Handles txtAccountSet._My_Click
        strQry = "select Acct_Set_Code as Code, Acct_Set_Desc as Description from TSPL_VENDOR_ACCOUNT_SET"
        txtAccountSet.arrValueMember = clsCommon.ShowMultipleSelectForm("AcSetSelector@VendorLedger", strQry, "Code", "Description", txtAccountSet.arrValueMember, txtAccountSet.arrDispalyMember)
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

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            If gvVendorGroup.Visible Then
                'transportSql.exportdataChilRows(gvVendorGroup, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvVendorGroup, "", Me.Text, , arrHeader)
            End If
            If gvVendor.Visible Then
                'transportSql.exportdataChilRows(gvVendor, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvVendor, "", Me.Text, , arrHeader)
            End If
            If gv.Visible Then
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
            End If
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RbtnSaveLayout_Click(sender As Object, e As EventArgs) Handles RbtnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            Dim obj As New clsGridLayout()
            If gvVendor.Visible = True Then
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

                ''richa agarwal regarding memory leakage
                'obj = Nothing
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ''---------------

            ElseIf gvVendorGroup.Visible = True Then
                gvVendorGroup.MasterTemplate.FilterDescriptors.Clear()
                obj = New clsGridLayout()
                obj.ReportID = GetReportID() ' ReportID + "VG"
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvVendorGroup.SaveLayout(obj.GridLayout)
                obj.GridColumns = gvVendorGroup.ColumnCount
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If
                ''richa agarwal regarding memory leakage
                'obj = Nothing
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ''---------------
            ElseIf gv.Visible = True Then
                gv.MasterTemplate.FilterDescriptors.Clear()
                obj = New clsGridLayout()
                obj.ReportID = GetReportID() 'ReportID + "D"
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gv.SaveLayout(obj.GridLayout)
                obj.GridColumns = gv.ColumnCount
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If
                ''richa agarwal regarding memory leakage
                'obj = Nothing
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ''---------------
            End If

        End If
    End Sub
    Private Sub ReStoreGridDetail()
        Try
            Dim TempReportID As String = ""
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                TempReportID = "MISCreditorReport"
            Else
                TempReportID = "VendorLedgerReport"
            End If

            If clsCommon.myLen(TempReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempReportID + "D", "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub ReStoreGridVendor()
        Try
            Dim TempReportID As String = ""
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                TempReportID = "MISCreditorReport"
            Else
                TempReportID = "VendorLedgerReport"
            End If
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
    Private Sub ReStoreGridVendorGrp()
        Try
            Dim TempReportID As String = ""
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
                TempReportID = "MISCreditorReport"
            Else
                TempReportID = "VendorLedgerReport"
            End If
            If clsCommon.myLen(TempReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempReportID + "VG", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvVendorGroup.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvVendorGroup.Columns.Count - 1 Step ii + 1
                        gvVendorGroup.Columns(ii).IsVisible = False
                        gvVendorGroup.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvVendorGroup.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RbtnDeleteLayout_Click(sender As Object, e As EventArgs) Handles RbtnDeleteLayout.Click
        'clsGridLayout.DeleteData(ReportID & "V", objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(ReportID & "VG", objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(ReportID & "D", objCommonVar.CurrentUserCode)

        'FormatgvVendor()
        'FormatgvVendorGroup()
        'gridHideVisible()

        'ReStoreGridVendor()
        'ReStoreGridVendorGrp()
        ' ReStoreGridDetail()
        ReportID = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            If gvVendor.Visible = True Then
                clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) 'ReportID & "V"
                FormatgvVendor()
                ReStoreGridVendor()
                RestoreGridSummaryRow()
            ElseIf gvVendorGroup.Visible = True Then
                clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) 'ReportID & "VG"
                FormatgvVendorGroup()
                ReStoreGridVendorGrp()
                RestoreGridSummaryRow()
            ElseIf gv.Visible = True Then
                clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) 'ReportID & "D"
                SetGridFormat(False)
                ReStoreGridDetail()
                RestoreGridSummaryRow()
            End If
        End If
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub frmRptVendorLedger_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus

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


            If gv.Visible Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            ElseIf gvVendor.Visible Then
                transportSql.applyExportTemplate(gvVendor, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvVendor, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gvVendor, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            ElseIf gvVendorGroup.Visible Then
                transportSql.applyExportTemplate(gvVendorGroup, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvVendorGroup, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gvVendorGroup, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            End If
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub QExpCSV_Click(sender As Object, e As EventArgs) Handles QExpCSV.Click
        Try
            If gv Is Nothing OrElse gv.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(gv, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

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
            clsCommon.MyMessageBoxShow(Me,"Data Exported successfully",Me.text)
            Process.Start(filePath)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Function GetReportID() As String
        If clsCommon.CompairString(FormType, clsUserMgtCode.MISCreditorReport) = CompairStringResult.Equal Then
            ReportID = "MISCreditorReport"
        Else
            ReportID = "VendorLedgerReport"
        End If
        If gvVendor.Visible = True Then
            ReportID = ReportID + "V"
            TemplateGridview = gvVendor
        ElseIf gvVendorGroup.Visible = True Then
            ReportID = ReportID + "VG"
            TemplateGridview = gvVendorGroup
        ElseIf gv.Visible = True Then
            ReportID = ReportID + "D"
            TemplateGridview = gv
        End If
        'End If
        Return ReportID
    End Function

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            If gvVendorGroup.Visible Then
                If gvVendorGroup.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If
            If gvVendor.Visible Then
                If gvVendor.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If
            If gv.Visible Then
                If gv.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkVendorSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Vendor(s) : " + strtemp)
            End If

            If rbtnchildslct.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgchild.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Child Vendor(s) : " + strtemp)
            End If

            If chkVndrSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgVndrGroup.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Vendor Group : " + strtemp)
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

            PageSetupReport_ID = GetReportID()

            If gvVendorGroup.Visible = True Then
                transportSql.applyExportTemplate(gvVendorGroup, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor Ledger Report", gvVendorGroup, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            ElseIf gvVendor.Visible = True Then
                transportSql.applyExportTemplate(gvVendor, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor Ledger Report", gvVendor, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            ElseIf gv.Visible = True Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor Ledger Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        blnRefresh = True
        GC.Collect()
        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            PrintForVendorCurrency(2)
        Else
            Print(2)
        End If
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        blnRefresh = True
        GC.Collect()
        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            PrintForVendorCurrency(1)
        Else
            Print(1)
        End If
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

           
            If gvVendorGroup.Visible Then
                clsCommon.MyExportToExcelGrid(Me.Text, gvVendorGroup, arrHeader, Me.Text, True)
            End If
            If gvVendor.Visible Then
                clsCommon.MyExportToExcelGrid(Me.Text, gvVendor, arrHeader, Me.Text, True)
            End If
            If gv.Visible Then
                clsCommon.MyExportToExcelGrid(Me.Text, gv, arrHeader, Me.Text, True)
            End If
            
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDFGrid_Click(sender As Object, e As EventArgs) Handles PDFGrid.Click
        Try
            Dim FilePath As String = "C:\\ERPTempFolder\\Vendor Ledger Report" + clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss") + ".pdf"
            Dim pdfExporter As ExportToPDF = Nothing

            If gvVendorGroup.Visible = True Then
                pdfExporter = New ExportToPDF(gvVendorGroup)
            ElseIf gvVendor.Visible = True Then
                pdfExporter = New ExportToPDF(gvVendor)
            ElseIf gv.Visible = True Then
                pdfExporter = New ExportToPDF(gv)
            End If
            pdfExporter.Font = New System.Drawing.Font("Verdana", 6)
            pdfExporter.TableBorderThickness = 1
            pdfExporter.FitToPageWidth = True
            pdfExporter.ExportVisualSettings = True
            pdfExporter.ExportHierarchy = True
            pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport
            pdfExporter.PageTitle = "Vendor Ledger Report"
            pdfExporter.RunExport(FilePath)
            System.Diagnostics.Process.Start(FilePath)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class










