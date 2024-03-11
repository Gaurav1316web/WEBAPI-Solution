'-Ticket No-[BM00000000581, BM00000000582,BM00000000770]
'================Rohit on June 04,2014 Add Column Named as Against Invoice No ===============
'' richa BM00000008755
Imports common
Imports System.IO
Public Class frmRptCSACustomerLedger
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isExportToExcel As Boolean = False
    Dim btnrefresh As Boolean = False
    'Const ReportID As String = "CustomerLedgerReport"
    Dim dtCustGrp As DataTable
    Dim dtCustomer As DataTable
    Dim dtMain As DataTable
    Dim dtOpening As DataTable
    Dim dvTemp As DataView
    Dim VisibleGrid As Integer = 0
    Dim FormType As String = Nothing
    Dim strQry As String = ""
    Dim FilterStr As String = String.Empty
    Dim IsDrillDown As Boolean = False
    Dim BackProcess As Boolean = False

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FormType = formid
    End Sub

    Private Sub frmRptCustomerLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled AndAlso MyBase.isPrintFlag Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
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

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'LoadCompany()
        'chkCompanyAll.IsChecked = True
        'LoadCustomer()
        'chkCustomerAll.IsChecked = True
        'LoadCustomerGroup()
        'chkCustGrpAll.IsChecked = True
        'LoadLocationCode()
        'chkLOcALL.IsChecked = True
        'LoadParentCustomer()
        'ChkParentCustAll.IsChecked = True
        'LoadCustomerType()
        'ChkCustTypeAll.IsChecked = True
        'LoadCustomerCategory()
        'ChkCustCatAll.IsChecked = True
        SetUserMgmtNew()

        ' KUNAL > TICKET : BM00000009466 =========
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        '==================================================

        chkActive.Checked = True
        chkNone.Checked = True
        ChkISParentCust.Checked = False

        chkCumulativeClosing.Checked = True

        lblParentCustomer.Enabled = False
        txtParentCustomer.Enabled = False
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        gvCustomerGroup.Dock = DockStyle.Fill
        gvCustomer.Dock = DockStyle.Fill
        gvDetails.Dock = DockStyle.Fill
        gvCustomerGroup.Visible = False
        gvCustomer.Visible = False
        btnBack.Enabled = False
        'If FormType = clsUserMgtCode.RptCSACustomerLedger Then
        '    chkNone.Checked = True
        '    pnlActiveInActiveCustomer.Visible = False
        '    chkAll.Checked = True
        'End If
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(FormType) ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnCustomerLedger)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        isExportToExcel = False
        print()
    End Sub

    'Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
    '    Try
    '        If gvCustomerGroup.Visible Then
    '            If gvCustomerGroup.Rows.Count <= 0 Then
    '                Throw New Exception("No data found for Export.")
    '            End If
    '        End If
    '        If gvCustomer.Visible Then
    '            If gvCustomer.Rows.Count <= 0 Then
    '                Throw New Exception("No data found for Export.")
    '            End If
    '        End If
    '        If gvDetails.Visible Then
    '            If gvDetails.Rows.Count <= 0 Then
    '                Throw New Exception("No data found for Export.")
    '            End If
    '        End If
    '        Dim arrHeader As List(Of String) = New List(Of String)()
    '        Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
    '        arrHeader.Add(strtemp)
    '        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
    '        'If chkVendorSelect.IsChecked Then
    '        '    strtemp = ""
    '        '    For Each Str As String In cbgVendor.CheckedDisplayMember
    '        '        If clsCommon.myLen(strtemp) > 0 Then
    '        '            strtemp += ", "
    '        '        End If
    '        '        strtemp += Str
    '        '    Next
    '        '    arrHeader.Add("Vendor(s) : " + strtemp)
    '        'End If

    '        'If rbtnchildslct.IsChecked Then
    '        '    strtemp = ""
    '        '    For Each Str As String In cbgchild.CheckedDisplayMember
    '        '        If clsCommon.myLen(strtemp) > 0 Then
    '        '            strtemp += ", "
    '        '        End If
    '        '        strtemp += Str
    '        '    Next
    '        '    arrHeader.Add("Child Vendor(s) : " + strtemp)
    '        'End If

    '        'If chkVndrSelect.IsChecked Then
    '        '    strtemp = ""
    '        '    For Each Str As String In cbgVndrGroup.CheckedDisplayMember
    '        '        If clsCommon.myLen(strtemp) > 0 Then
    '        '            strtemp += ", "
    '        '        End If
    '        '        strtemp += Str
    '        '    Next
    '        '    arrHeader.Add("Vendor Group : " + strtemp)
    '        'End If

    '        'If chkLOcSelect.IsChecked Then
    '        '    strtemp = ""
    '        '    For Each Str As String In cbgLocation.CheckedDisplayMember
    '        '        If clsCommon.myLen(strtemp) > 0 Then
    '        '            strtemp += ", "
    '        '        End If
    '        '        strtemp += Str
    '        '    Next
    '        '    arrHeader.Add("Location : " + strtemp)
    '        'End If

    '        If gvCustomerGroup.Visible Then
    '            clsCommon.MyExportToExcelGrid("Customer Ledger Report ", gvCustomerGroup, arrHeader, Me.Text)
    '        End If
    '        If gvCustomer.Visible Then
    '            clsCommon.MyExportToExcelGrid("Customer Ledger Report ", gvCustomer, arrHeader, Me.Text)
    '        End If
    '        If gvDetails.Visible Then
    '            clsCommon.MyExportToExcelGrid("Customer Ledger Report ", gvDetails, arrHeader, Me.Text)
    '        End If
    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
    '    End Try
    'End Sub

    Sub print()
        Dim CompanyAdd As String = ""
        Dim compname As String = ""
        Try

            Dim qry As String
            Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            compname = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            Dim runDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy hh:mm tt")

            'If chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 1 Then
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                CompanyAdd = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                'CompanyAdd = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                qry = "select top(1) Add1+' '+Add2+' '+Add3  from TSPL_LOCATION_MASTER  where Location_Type ='Physical' and Loc_Segment_Code  in ( " + CompanyAdd + ") "
                CompanyAdd = clsDBFuncationality.getSingleValue(qry)
            Else
                qry = "select top(1) Add1+' '+Add2+' '+Add3  from TSPL_COMPANY_MASTER where Comp_Code in ( '" + objCommonVar.CurrentCompanyCode + "') "
                CompanyAdd = clsDBFuncationality.getSingleValue(qry)
            End If

            Dim CheckCustomer As String = ""
            If chkActive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='N'"
            ElseIf chkInactive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If
            If btnrefresh = True Then
                Dim BaseQry As String = ""
                Dim BaseQryOpening As String = String.Empty

                Dim strcustomerfilter As String = String.Empty
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    strcustomerfilter = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                End If

                If Not chkItemWise.Checked Then
                    'BaseQry = clsCustomerMaster.GetCustomerBaseQry(False, ChkSecurity.Checked, clsCommon.GetMulcallString(TxtSecurity.arrValueMember), rbtnDocWise.Checked, clsCommon.myCstr(ddlCurrencyType.SelectedValue))
                    'BaseQry = clsCustomerMaster.GetCustomerBaseQry(False, ChkSecurity.Checked, "", False, "ConvRate", True)
                    BaseQry = clsCustomerMaster.GetCustomerBaseQry(False, ChkSecurity.Checked, "", False, "ConvRate", strcustomerfilter, False, strFromDate, strToDate, True)
                    BaseQryOpening = clsCustomerMaster.GetCustomerBaseQry(False, ChkSecurity.Checked, "", False, "ConvRate", strcustomerfilter, True, strFromDate, strToDate, True)
                Else

                End If
                Dim strFIlterCheck As String = ""
                Dim StrDocWiseFilter As String = String.Empty
                If ChkISParentCust.Checked = True Then
                    If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                        strFIlterCheck += " AND ((TSPL_CUSTOMER_MASTER.Parent_Customer_No IN  (" + clsCommon.GetMulcallString(txtParentCustomer.arrValueMember) + ") and ACode in  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")) or TSPL_CUSTOMER_MASTER.Cust_Code IN (" + clsCommon.GetMulcallString(txtParentCustomer.arrValueMember) + ")) "
                        'If ChkDocWise.Checked = True Then
                        '    If txtParentCustomer.arrValueMember IsNot Nothing AndAlso txtParentCustomer.arrValueMember.Count > 0 Then
                        '        StrDocWiseFilter += " AND CASE WHEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')<>''  THEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'') ELSE CustCode END IN  (" + clsCommon.GetMulcallString(txtParentCustomer.arrValueMember) + ") and CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END IN  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                        '    Else
                        '        StrDocWiseFilter += " AND CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END IN  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                        '    End If
                        'End If
                    Else
                        strFIlterCheck += " AND (TSPL_CUSTOMER_MASTER.Parent_Customer_No IN  (" + clsCommon.GetMulcallString(txtParentCustomer.arrValueMember) + ") or TSPL_CUSTOMER_MASTER.Cust_Code IN  (" + clsCommon.GetMulcallString(txtParentCustomer.arrValueMember) + "))"
                        If txtParentCustomer.arrValueMember IsNot Nothing AndAlso txtParentCustomer.arrValueMember.Count > 0 Then
                            StrDocWiseFilter += " AND (CASE WHEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')<>''  THEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'') ELSE CustCode END IN  (" + clsCommon.GetMulcallString(txtParentCustomer.arrValueMember) + ") or CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END IN  (" + clsCommon.GetMulcallString(txtParentCustomer.arrValueMember) + "))"
                        End If
                    End If
                Else
                    If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                        strFIlterCheck += "and ACode in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                        'If ChkDocWise.Checked = True Then
                        '    StrDocWiseFilter += " AND CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                        'End If
                    End If
                End If

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    If chkItemWise.Checked Then
                        strFIlterCheck += "and Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                    Else
                        strFIlterCheck += "and Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                    End If
                End If
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
                End If
                If txtCustomerType.arrValueMember IsNot Nothing AndAlso txtCustomerType.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(txtCustomerType.arrValueMember) + ")  "
                End If
                If txtCustomerCategory.arrValueMember IsNot Nothing AndAlso txtCustomerCategory.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Category_Code in (" + clsCommon.GetMulcallString(txtCustomerCategory.arrValueMember) + ")  "
                End If

                '---------------Customer Group Wise Data-----------
                If chkCustGroupWise.Checked = True Then
                    strQry = "Select 'CustomerGroup' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, Cust_Group_Code, MAX(Cust_Group_Desc) as Cust_Group_Desc, '' as ACode, '' as AName, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, SUM(DrCrNote) as DrCrNote, ( SUM(OpngBal) + SUM(DrAmt) ) -SUM(CrAmt)  as BalAmt,MAX(xxx.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(xxx.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc  From (" + Environment.NewLine & _
                    " Select TSPL_CUSTOMER_MASTER.Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc, SUM(DrAmt)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrCrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " + BaseQryOpening + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + " GROUP BY TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
                    Environment.NewLine + " UNION ALL-----------------------------------BADA UNION--------------------" + Environment.NewLine & _
                    " Select TSPL_CUSTOMER_MASTER.Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc, 0 as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, (SUM(DrNote)+SUM(CrNote)) as DrCrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + " GROUP BY TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
                    " ) XXX GROUP BY Cust_Group_Code ORDER BY Cust_Group_Code"
                    dtCustGrp = clsDBFuncationality.GetDataTable(strQry)
                End If

                '---------------Customer Wise Data-----------------
                If chkCustWise.Checked = True Then
                    strQry = "Select 'Customer' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, MAX(Cust_Group_Code) AS Cust_Group_Code, '' as Cust_Group_Desc, ACode, MAX(AName) as AName, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, SUM(DrCrNote) as DrCrNote,( SUM(OpngBal) + SUM(DrAmt) ) -SUM(CrAmt)  as BalAmt,MAX(Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(xxx.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc From (" + Environment.NewLine & _
                    " Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, SUM(DrAmt)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrCrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " + BaseQryOpening + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine & _
                    Environment.NewLine + " UNION ALL" + Environment.NewLine & _
                    " Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, 0 as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, (SUM(DrNote)+SUM(CrNote)) as DrCrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine & _
                    " ) XXX GROUP BY ACode ORDER BY ACode"
                    dtCustomer = clsDBFuncationality.GetDataTable(strQry)
                End If
                '---------------Opening Data-----------------------
                'strQry = "With CTETemp as (" + Environment.NewLine & _
                '" Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, OrderDate, OrderDocType) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, XXX.* from (" + Environment.NewLine & _
                '" Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, DocNo, MAX(AgainstInvoiceNo) as AgainstInvoiceNo, MAX(DocDate) as DocDate, MAX(DocType) as DocType,isnull(MAX(DocNarr),'') as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Location) as Location, convert(date,MAX(final.DocDate),103) as OrderDate, (Select MAX( TSPL_LOCATION_MASTER.Location_Desc) from  TSPL_LOCATION_MASTER where Location_Code=MAX(Location)) as LocDesc, '' as Item_Code, '' as Item_Desc, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, (SUM(DrNote)+SUM(CrNote)) as DrCrNote, SUM(dramt)-SUM(cramt) as BalAmt, MAX(SourceCode) as SourceCode, CASE  WHEN MAX(DocType) = 'IN' THEN 1  WHEN MAX(DocType) = 'RC' THEN 2 WHEN MAX(DocType) = 'SR' THEN 3 WHEN MAX(DocType) = 'AD' THEN 4 ELSE 5 END  as OrderDocType," + Environment.NewLine & _
                '" Case When Max(TSPL_BANK_MASTER.Bank_type)='B' Then (Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else 'Pending' End) Else '' End as [Reconciliation_Date]" + Environment.NewLine & _
                '" ,MAX(Receipt_Type) AS Receipt_Type, MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC" + Environment.NewLine & _
                '" from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
                '" where  CONVERT(DATE,final.DocDate,103) < '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + "" + Environment.NewLine & _
                '" GROUP By ACode, DocNo,Location" + Environment.NewLine & _
                '" ) XXX left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode" + Environment.NewLine & _
                '" ) Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate,CTETemp.ParentCode,CTETemp.ParentName ,CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, CTETemp.DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.Sales, CTETemp.CollectionRefund, CTETemp.DrCrNote, CTETemp.BalAmt, SUM(BalAmt) OVER (PARTITION BY ACode ORDER BY ACode, RowNo) as [Closing], CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date " + Environment.NewLine & _
                '" ,CTETemp.Receipt_Type, CTETemp.Cust_Type_Code, CTETemp.Cust_Type_Desc, CTETemp.Cust_Category_Code, CTETemp.CUST_CATEGORY_DESC " + Environment.NewLine & _
                '" from CTETemp ORDER BY CTETemp.ACode, CTETemp.OrderDate, CTETemp.OrderDocType"
                'dtOpening = clsDBFuncationality.GetDataTable(strQry)

                '------------------Detail Level Data------------------- 

                If chkItemWise.Checked Then
                    strQry = "WITH CTETemp as ("
                    strQry += "Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, OrderDate, OrderDocType) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, * from ("
                    strQry += " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as DocNo,'' as AgainstInvoiceNo, NULL as DocDate, '' as DocType, 'Opening Balance' as DocNarr, '' as ChequeDetails, '' as Location, NULL as OrderDate, '' as LocDesc, '' as Item_Code, '' as Item_Desc, Case WHEN SUM(DrAmt)>=SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End as DrAmt, Case WHEN SUM(DrAmt)< SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, (SUM(DrNote)+SUM(CrNote)) as DrCrNote, SUM(DrAmt)-SUM(CrAmt) as BalAmt, '' as SourceCode, 0 as OrderDocType, '' as [Reconciliation_Date] "
                    strQry += ",MAX(Receipt_Type) AS Receipt_Type "
                    strQry += " from ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code "
                    strQry += " where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND TSPL_CUSTOMER_MASTER.Status='N'  and LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + " "
                    strQry += " AND Final.Receipt_Type <> 'U'"
                    strQry += "  GROUP BY ACode"
                    strQry += " UNION ALL"
                    strQry += " Select TSPL_CUSTOMER_MASTER.Parent_Customer_No as ParentCode, ACode, TSPL_CUSTOMER_MASTER.Customer_Name as AName, DocNo,AgainstInvoiceNo,DocDate,DocType,isnull(  tspl_BankReco_Head.Description,'') as DocNarr,ChequeDetails, Loc_Code,convert(date,final.DocDate,103) as OrderDate, (Select MAX( TSPL_LOCATION_MASTER.Location_Desc) from  TSPL_LOCATION_MASTER where Loc_Segment_Code=Location_Code) as LocDesc, Item_Code, Item_Desc, DrAmt, CrAmt, [Sales], CollectionRefund, DrCrNote, dramt-cramt as BalAmt,SourceCode, CASE  WHEN DocType  = 'IN' THEN 1  WHEN DocType = 'RC' THEN 2 WHEN DocType = 'SR' THEN 3 WHEN DocType = 'AD' THEN 4 ELSE 5 END  as OrderDocType, Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then CONVERT(VARCHAR,tspl_BankReco_Detail.Reconciliation_Date,103) Else 'Pending' End as [Reconciliation_Date] "
                    strQry += ",Receipt_Type AS Receipt_Type "
                    strQry += " from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' and   CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + ""
                    strQry += " AND Final.Receipt_Type <> 'U'"
                    strQry += " ) XXX   left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode"
                    strQry += ") Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate,CTETemp.ParentCode,CTETemp.ParentName ,CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, CTETemp.DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, Item_Code, Item_Desc, CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.BalAmt, SUM(BalAmt) OVER (PARTITION BY ACode ORDER BY ACode, RowNo) as [Closing], CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date "
                    strQry += ",CTETemp.Receipt_Type "
                    strQry += " from CTETemp ORDER BY CTETemp.ACode, CTETemp.OrderDate, CTETemp.OrderDocType"
                Else
                    strQry = "WITH CTETemp as (" + Environment.NewLine & _
                    " Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, OrderDate, OrderDocType) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, XXX.* from (" + Environment.NewLine & _
                    " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as DocNo,'' as AgainstInvoiceNo, NULL as DocDate, '' as DocType, 'Opening Balance' as DocNarr, '' as ChequeDetails, '' as Location, NULL as OrderDate, '' as LocDesc, '' as Item_Code, '' as Item_Desc, Case WHEN SUM(DrAmt)>=SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End as DrAmt, Case WHEN SUM(DrAmt)< SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrCrNote, SUM(DrAmt)-SUM(CrAmt) as BalAmt, '' as SourceCode, 0 as OrderDocType, '' as [Reconciliation_Date]" + Environment.NewLine & _
                    " ,MAX(Receipt_Type) AS Receipt_Type ,MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC" + Environment.NewLine & _
                    " from ( " + BaseQryOpening + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND TSPL_CUSTOMER_MASTER.Status='N'  and LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + "" + Environment.NewLine & _
                    " AND Final.Receipt_Type <> 'U'" + Environment.NewLine & _
                    " GROUP BY ACode" + Environment.NewLine & _
                    Environment.NewLine + " UNION ALL----------------------------------------------Bada UNION---------------" + Environment.NewLine & _
                    " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, DocNo, MAX(AgainstInvoiceNo) as AgainstInvoiceNo, MAX(DocDate) as DocDate, MAX(DocType) as V,isnull(MAX(DocNarr),'') as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Location) as Location, convert(date,MAX(final.DocDate),103) as OrderDate, (Select MAX( TSPL_LOCATION_MASTER.Location_Desc) from  TSPL_LOCATION_MASTER where Location_Code=MAX(Location)) as LocDesc, '' as Item_Code, '' as Item_Desc, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, (SUM(DrNote)+SUM(CrNote)) as DrCrNote, SUM(dramt)-SUM(cramt) as BalAmt, MAX(SourceCode) as SourceCode, CASE  WHEN MAX(DocType) = 'IN' THEN 1  WHEN MAX(DocType) = 'RC' THEN 2 WHEN MAX(DocType) = 'SR' THEN 3 WHEN MAX(DocType) = 'AD' THEN 4 ELSE 5 END  as OrderDocType," + Environment.NewLine & _
                    " Case When Max(TSPL_BANK_MASTER.Bank_type)='B' Then (Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else 'Pending' End) Else '' End as [Reconciliation_Date]" + Environment.NewLine & _
                    ",MAX(Receipt_Type) AS Receipt_Type, MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC" + Environment.NewLine & _
                    " from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No AND tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                    " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
                    " where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' and   CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + CheckCustomer + "" + Environment.NewLine & _
                    " GROUP By ACode, DocNo,Location" + Environment.NewLine & _
                    " ) XXX   left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode" + Environment.NewLine & _
                    ") Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate,CTETemp.ParentCode,CTETemp.ParentName ,CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo,CONVERT(VARCHAR, CTETemp.DocDate,103) AS DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.Sales, CTETemp.CollectionRefund, CTETemp.DrCrNote,round( CTETemp.BalAmt,2) as BalAmt, SUM(round(BalAmt,2)) OVER (PARTITION BY ACode ORDER BY ACode, RowNo) as [Closing], CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date " + Environment.NewLine & _
                    ",CTETemp.Receipt_Type,CTETemp.Cust_Type_Code ,CTETemp.Cust_Type_Desc ,CTETemp.Cust_Category_Code ,CTETemp.CUST_CATEGORY_DESC " + Environment.NewLine & _
                    " from CTETemp ORDER BY CTETemp.ACode, CTETemp.OrderDate, CTETemp.OrderDocType"
                End If

                If chkNone.Checked = True Then
                    dtMain = clsDBFuncationality.GetDataTable(strQry)
                End If

                If chkNone.Checked = True AndAlso dtMain.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Data not found", Me.Text)
                    gvDetails.DataSource = Nothing
                    gvDetails.Columns.Clear()
                    gvDetails.Rows.Clear()
                    Exit Sub
                Else
                    gvDetails.DataSource = Nothing
                    gvDetails.Columns.Clear()
                    gvDetails.Rows.Clear()
                    If chkCustWise.Checked Or chkNone.Checked Then
                        btnBack.Enabled = True
                    Else
                        btnBack.Enabled = False
                    End If
                End If

                If chkCustGroupWise.Checked = True Then
                    gvCustomerGroup.DataSource = dtCustGrp
                    FormatgvCustGroup()
                End If

                If chkCustWise.Checked = True Then
                    gvCustomer.DataSource = dtCustomer
                    FormatgvCustomer()
                End If


                If chkNone.Checked = True Then
                    gvDetails.DataSource = dtMain
                    gvDetails.AllowRowReorder = False
                    FormatGrid(False)
                End If
               
                gridHideVisible()
                PageSetupReport_ID = GetReportId()
                If chkCustGroupWise.Checked = True Then
                    TemplateGridview = gvCustomerGroup
                ElseIf chkCustWise.Checked = True Then
                    TemplateGridview = gvCustomer
                ElseIf chkNone.Checked = True Then
                    TemplateGridview = gvDetails
                End If
                ReStoreGridLayout()
            End If
            If IsDrillDown = True Then
                'If (chkCustWise.Checked = True AndAlso IsDrillDown = True) Then
                '    If gvCustomer.Visible = True Then

                '        'gvDetails.Visible = True
                '        'gvCustomer.Visible = False
                '        dvTemp = New DataView(dtMain)
                '        ' dvTemp.RowFilter = FilterStr
                '        gvDetails.DataSource = Nothing
                '        gvDetails.DataSource = dvTemp.ToTable()
                '    Else
                '        'gvDetails.Visible = True
                '        'gvCustomer.Visible = False
                '        dvTemp = New DataView(dtMain)
                '        dvTemp.RowFilter = FilterStr
                '        gvDetails.DataSource = Nothing
                '        gvDetails.DataSource = dvTemp.ToTable()
                '    End If

                '    FormatGrid(True)
                'ElseIf (chkCustGroupWise.Checked = True AndAlso IsDrillDown = True) Then
                '    'If gvDetails.Visible = False Then

                '    If gvCustomer.Visible = True Then
                '        dvTemp = New DataView(dtCustomer)
                '        dvTemp.RowFilter = FilterStr
                '        gvCustomer.DataSource = Nothing
                '        gvCustomer.DataSource = dvTemp.ToTable()


                '    ElseIf gvDetails.Visible = True Then
                '        dvTemp = New DataView(dtMain)
                '        dvTemp.RowFilter = FilterStr
                '        gvDetails.DataSource = Nothing
                '        gvDetails.DataSource = dvTemp.ToTable()
                '        FormatGrid(True)

                '    End If
                '    FormatgvCustomer()
                'End If

            End If

            'BackProcess = False
            'IsDrillDown = False
            If btnrefresh = False Then
                If isExportToExcel = True Then
                    Dim arrHeadrer As New List(Of String)
                    arrHeadrer.Add("From Date : " + strFromDate + "")
                    arrHeadrer.Add("To Date : " + strToDate + "")
                    If gvCustomerGroup.Visible Then
                        clsCommon.MyExportToExcel("Customer Ledger (Customer Group Wise)", gvCustomerGroup, arrHeadrer, "CustomerLedger")
                    ElseIf gvCustomer.Visible Then
                        clsCommon.MyExportToExcel("Customer Ledger (Customer Wise)", gvCustomer, arrHeadrer, "CustomerLedger")
                    Else
                        clsCommon.MyExportToExcel("Customer Ledger", gvDetails, arrHeadrer, "CustomerLedger")
                    End If
                Else
                    'If ChkDocWise.Checked = False Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If chkCustGroupWise.Checked = True Then
                        'If gvCustomerGroup.Visible = True andalso IsDrillDown = False Then
                        '    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                        'Else
                        '    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dvTemp.ToTable(), "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                        'End If
                        If chkCustGroupWise.Checked AndAlso gvCustomerGroup.Visible = True Then
                            frmCRV.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                        ElseIf chkCustGroupWise.Checked = True AndAlso gvCustomer.Visible = True Then
                            frmCRV.funreport(CrystalReportFolder.SalesReport, dvTemp.ToTable(), "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                        ElseIf chkCustGroupWise.Checked = True AndAlso gvDetails.Visible = True Then
                            frmCRV.funreport(CrystalReportFolder.SalesReport, dvTemp.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                        End If

                    ElseIf chkCustWise.Checked = True Then
                        If gvCustomer.Visible = True Then
                            frmCRV.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                        ElseIf gvDetails.Visible = True Then
                            frmCRV.funreport(CrystalReportFolder.SalesReport, dvTemp.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                        ElseIf gvCustomerGroup.Visible = True Then
                            frmCRV.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                        End If
                    ElseIf chkNone.Checked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedger_DEMO", "Customer Ledger")
                    End If
                    frmCRV = Nothing
                    'Else
                    '    '' Anubhooti 11-Mar-2015 (Doc Wise Rpt)
                    '    If ChkDocSumm.Checked = True Then
                    '        frm.funreport(dtMain, "rptCustomerLedgerDocSummWise", "Customer Ledger")
                    '    Else
                    '        frm.funreport(dtMain, "rptCustomerLedgerDocWise", "Customer Ledger")
                    '    End If

                    'End If
                End If
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid(ByVal IsFromDrillDown As Boolean)
        gvDetails.AllowAddNewRow = False
        gvDetails.TableElement.TableHeaderHeight = 40
        gvDetails.MasterTemplate.ShowRowHeaderColumn = False
        gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To gvDetails.Columns.Count - 1
            gvDetails.Columns(ii).ReadOnly = True
            gvDetails.Columns(ii).IsVisible = False
        Next
        If chkCustWise.Checked Then
            gvDetails.Columns("ParentCode").IsVisible = False
            gvDetails.Columns("ParentCode").Width = 101
            gvDetails.Columns("ParentCode").HeaderText = "Parent Customer Code"

            gvDetails.Columns("ParentName").IsVisible = False
            gvDetails.Columns("ParentName").Width = 221
            gvDetails.Columns("ParentName").HeaderText = "Parent Name"

            gvDetails.Columns("ACode").IsVisible = True
            gvDetails.Columns("ACode").Width = 101
            gvDetails.Columns("ACode").HeaderText = "Customer Code"

            gvDetails.Columns("AName").IsVisible = True
            gvDetails.Columns("AName").Width = 221
            gvDetails.Columns("AName").HeaderText = "Customer Name"

            If FormType = clsUserMgtCode.RptCSACustomerLedger Then
                gvCustomer.Columns("Sales").IsVisible = True
                gvCustomer.Columns("Sales").Width = 150
                gvCustomer.Columns("Sales").HeaderText = "Sales"
                gvCustomer.Columns("Sales").FormatString = "{0:f2}"

                gvCustomer.Columns("CollectionRefund").IsVisible = True
                gvCustomer.Columns("CollectionRefund").Width = 150
                gvCustomer.Columns("CollectionRefund").HeaderText = "Collections"
                gvCustomer.Columns("CollectionRefund").FormatString = "{0:f2}"

                gvCustomer.Columns("DrCrNote").IsVisible = True
                gvCustomer.Columns("DrCrNote").Width = 150
                gvCustomer.Columns("DrCrNote").HeaderText = "DR. / CR. Note"
                gvCustomer.Columns("DrCrNote").FormatString = "{0:f2}"
            Else
                gvDetails.Columns("DrAmt").IsVisible = True
                gvDetails.Columns("DrAmt").Width = 101
                gvDetails.Columns("DrAmt").HeaderText = "Debit Amt"
                gvDetails.Columns("DrAmt").FormatString = "{0:f2}"

                gvDetails.Columns("CrAmt").IsVisible = True
                gvDetails.Columns("CrAmt").Width = 101
                gvDetails.Columns("CrAmt").HeaderText = "Credit Amt"
                gvDetails.Columns("CrAmt").FormatString = "{0:f2}"
            End If

            gvDetails.Columns("BalAmt").IsVisible = True
            gvDetails.Columns("BalAmt").Width = 101
            gvDetails.Columns("BalAmt").HeaderText = "Balance Amount"
            '  gvDetails.Columns("BalAmt").FormatString = "{0:n2}"
            gvDetails.Columns("BalAmt").FormatString = "{0:f2}"

        Else
            gvDetails.Columns("ParentCode").IsVisible = False
            gvDetails.Columns("ParentCode").Width = 101
            gvDetails.Columns("ParentCode").HeaderText = "Parent Customer Code"

            gvDetails.Columns("ParentName").IsVisible = False
            gvDetails.Columns("ParentName").Width = 221
            gvDetails.Columns("ParentName").HeaderText = "Parent Name"

            gvDetails.Columns("ACode").IsVisible = True
            gvDetails.Columns("ACode").Width = 101
            gvDetails.Columns("ACode").HeaderText = "Customer Code"

            gvDetails.Columns("AName").IsVisible = True
            gvDetails.Columns("AName").Width = 221
            gvDetails.Columns("AName").HeaderText = "Customer Name"

            gvDetails.Columns("DocDate").IsVisible = True
            gvDetails.Columns("DocDate").Width = 81
            gvDetails.Columns("DocDate").HeaderText = "Date"
            gvDetails.Columns("DocDate").FormatString = "{0:d}"

            gvDetails.Columns("DocType").IsVisible = True
            gvDetails.Columns("DocType").Width = 51
            gvDetails.Columns("DocType").HeaderText = "From"

            gvDetails.Columns("Location").IsVisible = True
            gvDetails.Columns("Location").Width = 71
            gvDetails.Columns("Location").HeaderText = "Unit"

            '' Anubhooti 23-Dec-2014 (Show Unit Desc)
            gvDetails.Columns("LocDesc").IsVisible = True
            gvDetails.Columns("LocDesc").Width = 101
            gvDetails.Columns("LocDesc").HeaderText = "Unit Description"
            ''

            gvDetails.Columns("DocNo").IsVisible = True
            gvDetails.Columns("DocNo").Width = 151
            gvDetails.Columns("DocNo").HeaderText = "Doc No"

            gvDetails.Columns("AgainstInvoiceNo").IsVisible = False
            gvDetails.Columns("AgainstInvoiceNo").Width = 151
            gvDetails.Columns("AgainstInvoiceNo").HeaderText = "Against Invoice No"

            gvDetails.Columns("DocNarr").IsVisible = True
            gvDetails.Columns("DocNarr").Width = 171
            gvDetails.Columns("DocNarr").HeaderText = "Narration"

            gvDetails.Columns("chequedetails").IsVisible = False
            gvDetails.Columns("chequedetails").Width = 171
            gvDetails.Columns("chequedetails").HeaderText = "Cheque Details"

            If chkItemWise.Checked Then
                gvDetails.Columns("Item_Code").IsVisible = True
                gvDetails.Columns("Item_Code").Width = 70
                gvDetails.Columns("Item_Code").HeaderText = "Item Code"

                gvDetails.Columns("Item_Desc").IsVisible = True
                gvDetails.Columns("Item_Desc").Width = 101
                gvDetails.Columns("Item_Desc").HeaderText = "Item Desc"
            End If

            If FormType = clsUserMgtCode.RptCSACustomerLedger Then
                gvDetails.Columns("Sales").IsVisible = True
                gvDetails.Columns("Sales").Width = 150
                gvDetails.Columns("Sales").HeaderText = "Sales"
                gvDetails.Columns("Sales").FormatString = "{0:f2}"

                gvDetails.Columns("CollectionRefund").IsVisible = True
                gvDetails.Columns("CollectionRefund").Width = 150
                gvDetails.Columns("CollectionRefund").HeaderText = "Collections"
                gvDetails.Columns("CollectionRefund").FormatString = "{0:f2}"

                gvDetails.Columns("DrCrNote").IsVisible = True
                gvDetails.Columns("DrCrNote").Width = 150
                gvDetails.Columns("DrCrNote").HeaderText = "DR. / CR. Note"
                gvDetails.Columns("DrCrNote").FormatString = "{0:f2}"
            Else
                gvDetails.Columns("DrAmt").IsVisible = True
                gvDetails.Columns("DrAmt").Width = 101
                gvDetails.Columns("DrAmt").HeaderText = "Total Dr"
                gvDetails.Columns("DrAmt").FormatString = "{0:f2}"

                gvDetails.Columns("CrAmt").IsVisible = True
                gvDetails.Columns("CrAmt").Width = 101
                gvDetails.Columns("CrAmt").HeaderText = "Total Cr"
                gvDetails.Columns("CrAmt").FormatString = "{0:f2}"
            End If

            gvDetails.Columns("BalAmt").IsVisible = Not IsFromDrillDown
            gvDetails.Columns("BalAmt").Width = 101
            gvDetails.Columns("BalAmt").HeaderText = "Balance Amount"
            gvDetails.Columns("BalAmt").FormatString = "{0:f2}"
            If chkCumulativeClosing.Checked Then
                gvDetails.Columns("Closing").IsVisible = True
                gvDetails.Columns("Closing").Width = 101
                '' Anubhooti 23-Dec-2014 (Bal Amt is irrelevant in case of cumulative)
                gvDetails.Columns("Closing").HeaderText = "Balance Amount"
                gvDetails.Columns("BalAmt").IsVisible = False
                gvDetails.Columns("Closing").FormatString = "{0:f2}"
                ''
            End If

            gvDetails.Columns("SourceCode").IsVisible = False
            gvDetails.Columns("SourceCode").Width = 101
            gvDetails.Columns("SourceCode").HeaderText = "Source Code"

            gvDetails.Columns("Reconciliation_Date").IsVisible = False
            gvDetails.Columns("Reconciliation_Date").Width = 120
            gvDetails.Columns("Reconciliation_Date").HeaderText = "Reco Date" '

            gvDetails.Columns("Cust_Type_Code").IsVisible = False
            gvDetails.Columns("Cust_Type_Code").Width = 101
            gvDetails.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"

            gvDetails.Columns("Cust_Type_Desc").IsVisible = False
            gvDetails.Columns("Cust_Type_Desc").Width = 120
            gvDetails.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc" '

            gvDetails.Columns("Cust_Category_Code").IsVisible = False
            gvDetails.Columns("Cust_Category_Code").Width = 101
            gvDetails.Columns("Cust_Category_Code").HeaderText = "Cust Category Code"

            gvDetails.Columns("CUST_CATEGORY_DESC").IsVisible = False
            gvDetails.Columns("CUST_CATEGORY_DESC").Width = 120
            gvDetails.Columns("CUST_CATEGORY_DESC").HeaderText = "Cust Category Desc" '
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim ColumnTotal As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)
        ColumnTotal = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)
        ColumnTotal = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)
        ColumnTotal = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)
        ColumnTotal = New GridViewSummaryItem("DrCrNote", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)
        ColumnTotal = New GridViewSummaryItem("BalAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ColumnTotal)
        gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'FindAndRestoreGridLayout(Me)
    End Sub

    Private Sub FormatgvCustGroup()
        Try
            gvCustomerGroup.AllowAddNewRow = False
            gvCustomerGroup.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gvCustomerGroup.Columns.Count - 1
                gvCustomerGroup.Columns(ii).ReadOnly = True
                gvCustomerGroup.Columns(ii).IsVisible = False
            Next

            gvCustomerGroup.Columns("Cust_Group_Code").IsVisible = True
            gvCustomerGroup.Columns("Cust_Group_Code").Width = 180
            gvCustomerGroup.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            gvCustomerGroup.Columns("Cust_Group_Desc").IsVisible = True
            gvCustomerGroup.Columns("Cust_Group_Desc").Width = 350
            gvCustomerGroup.Columns("Cust_Group_Desc").HeaderText = "Description"

            gvCustomerGroup.Columns("OpngBal").IsVisible = True
            gvCustomerGroup.Columns("OpngBal").Width = 150
            gvCustomerGroup.Columns("OpngBal").HeaderText = "Opening Balance"
            gvCustomerGroup.Columns("OpngBal").FormatString = "{0:f2}"

            If FormType = clsUserMgtCode.RptCSACustomerLedger Then
                gvCustomerGroup.Columns("Sales").IsVisible = True
                gvCustomerGroup.Columns("Sales").Width = 150
                gvCustomerGroup.Columns("Sales").HeaderText = "Sales"
                gvCustomerGroup.Columns("Sales").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("CollectionRefund").IsVisible = True
                gvCustomerGroup.Columns("CollectionRefund").Width = 150
                gvCustomerGroup.Columns("CollectionRefund").HeaderText = "Collections"
                gvCustomerGroup.Columns("CollectionRefund").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("DrCrNote").IsVisible = True
                gvCustomerGroup.Columns("DrCrNote").Width = 150
                gvCustomerGroup.Columns("DrCrNote").HeaderText = "DR. / CR. Note"
                gvCustomerGroup.Columns("DrCrNote").FormatString = "{0:f2}"
            Else
                gvCustomerGroup.Columns("DrAmt").IsVisible = True
                gvCustomerGroup.Columns("DrAmt").Width = 150
                gvCustomerGroup.Columns("DrAmt").HeaderText = "Debit Amt"
                gvCustomerGroup.Columns("DrAmt").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("CrAmt").IsVisible = True
                gvCustomerGroup.Columns("CrAmt").Width = 150
                gvCustomerGroup.Columns("CrAmt").HeaderText = "Credit Amt"
                gvCustomerGroup.Columns("CrAmt").FormatString = "{0:f2}"
            End If
            gvCustomerGroup.Columns("BalAmt").IsVisible = True
            gvCustomerGroup.Columns("BalAmt").Width = 150
            gvCustomerGroup.Columns("BalAmt").HeaderText = "Balance Amount"
            gvCustomerGroup.Columns("BalAmt").FormatString = "{0:f2}"

            gvCustomerGroup.Columns("Cust_Type_Code").IsVisible = True
            gvCustomerGroup.Columns("Cust_Type_Code").Width = 101
            gvCustomerGroup.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"

            gvCustomerGroup.Columns("Cust_Type_Desc").IsVisible = True
            gvCustomerGroup.Columns("Cust_Type_Desc").Width = 120
            gvCustomerGroup.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc" '

            gvCustomerGroup.Columns("Cust_Category_Code").IsVisible = True
            gvCustomerGroup.Columns("Cust_Category_Code").Width = 101
            gvCustomerGroup.Columns("Cust_Category_Code").HeaderText = "Cust Category Code"

            gvCustomerGroup.Columns("CUST_CATEGORY_DESC").IsVisible = True
            gvCustomerGroup.Columns("CUST_CATEGORY_DESC").Width = 120
            gvCustomerGroup.Columns("CUST_CATEGORY_DESC").HeaderText = "Cust Category Desc" '

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrCrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("BalAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            gvCustomerGroup.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatgvCustomer()
        Try
            gvCustomer.AllowAddNewRow = False
            gvCustomer.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gvCustomer.Columns.Count - 1
                gvCustomer.Columns(ii).ReadOnly = True
                gvCustomer.Columns(ii).IsVisible = False
            Next

            gvCustomer.Columns("ACode").IsVisible = True
            gvCustomer.Columns("ACode").Width = 180
            gvCustomer.Columns("ACode").HeaderText = "Customer Code"

            gvCustomer.Columns("AName").IsVisible = True
            gvCustomer.Columns("AName").Width = 350
            gvCustomer.Columns("AName").HeaderText = "Name"

            gvCustomer.Columns("OpngBal").IsVisible = True
            gvCustomer.Columns("OpngBal").Width = 150
            gvCustomer.Columns("OpngBal").HeaderText = "Opening Balance"
            gvCustomer.Columns("OpngBal").FormatString = "{0:f2}"

            If FormType = clsUserMgtCode.RptCSACustomerLedger Then
                gvCustomer.Columns("Sales").IsVisible = True
                gvCustomer.Columns("Sales").Width = 150
                gvCustomer.Columns("Sales").HeaderText = "Sales"
                gvCustomer.Columns("Sales").FormatString = "{0:f2}"

                gvCustomer.Columns("CollectionRefund").IsVisible = True
                gvCustomer.Columns("CollectionRefund").Width = 150
                gvCustomer.Columns("CollectionRefund").HeaderText = "Collections"
                gvCustomer.Columns("CollectionRefund").FormatString = "{0:f2}"

                gvCustomer.Columns("DrCrNote").IsVisible = True
                gvCustomer.Columns("DrCrNote").Width = 150
                gvCustomer.Columns("DrCrNote").HeaderText = "DR. / CR. Note"
                gvCustomer.Columns("DrCrNote").FormatString = "{0:f2}"
            Else
                gvCustomer.Columns("DrAmt").IsVisible = True
                gvCustomer.Columns("DrAmt").Width = 150
                gvCustomer.Columns("DrAmt").HeaderText = "Debit Amt"
                gvCustomer.Columns("DrAmt").FormatString = "{0:f2}"

                gvCustomer.Columns("CrAmt").IsVisible = True
                gvCustomer.Columns("CrAmt").Width = 150
                gvCustomer.Columns("CrAmt").HeaderText = "Credit Amt"
                gvCustomer.Columns("CrAmt").FormatString = "{0:f2}"
            End If

            gvCustomer.Columns("BalAmt").IsVisible = True
            gvCustomer.Columns("BalAmt").Width = 150
            gvCustomer.Columns("BalAmt").HeaderText = "Balance Amount"
            gvCustomer.Columns("BalAmt").FormatString = "{0:f2}"

            gvCustomer.Columns("Cust_Type_Code").IsVisible = True
            gvCustomer.Columns("Cust_Type_Code").Width = 101
            gvCustomer.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"

            gvCustomer.Columns("Cust_Type_Desc").IsVisible = True
            gvCustomer.Columns("Cust_Type_Desc").Width = 120
            gvCustomer.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc" '

            gvCustomer.Columns("Cust_Category_Code").IsVisible = True
            gvCustomer.Columns("Cust_Category_Code").Width = 101
            gvCustomer.Columns("Cust_Category_Code").HeaderText = "Cust Category Code"

            gvCustomer.Columns("CUST_CATEGORY_DESC").IsVisible = True
            gvCustomer.Columns("CUST_CATEGORY_DESC").Width = 120
            gvCustomer.Columns("CUST_CATEGORY_DESC").HeaderText = "Cust Category Desc" '

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrCrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("BalAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            gvCustomer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    '' Anubhooti 02-March-2015
    Private Sub FormatGridDocWise()
        gvDetails.Visible = True
        gvCustomer.Visible = False
        gvCustomerGroup.Visible = False
        gvDetails.AllowAddNewRow = False
        gvDetails.TableElement.TableHeaderHeight = 40
        gvDetails.MasterTemplate.ShowRowHeaderColumn = False
        gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
        gvDetails.GroupDescriptors.Clear()
        For ii As Integer = 0 To gvDetails.Columns.Count - 1
            gvDetails.Columns(ii).ReadOnly = True
            gvDetails.Columns(ii).IsVisible = False
        Next
        'If ChkDocWise.Checked Then
        '    gvDetails.Columns("CustomerCode").IsVisible = True
        '    gvDetails.Columns("CustomerCode").Width = 150
        '    gvDetails.Columns("CustomerCode").HeaderText = "Customer Code"

        '    gvDetails.Columns("Customer Name").IsVisible = True
        '    gvDetails.Columns("Customer Name").Width = 220
        '    gvDetails.Columns("Customer Name").HeaderText = "Customer Name"

        '    gvDetails.Columns("MainCustCode").IsVisible = True
        '    gvDetails.Columns("MainCustCode").Width = 150
        '    gvDetails.Columns("MainCustCode").HeaderText = "MainCustCode"

        '    gvDetails.Columns("MainCustName").IsVisible = True
        '    gvDetails.Columns("MainCustName").Width = 220
        '    gvDetails.Columns("MainCustName").HeaderText = "MainCustName"

        '    gvDetails.Columns("DocumentNo").IsVisible = True
        '    gvDetails.Columns("DocumentNo").Width = 100
        '    gvDetails.Columns("DocumentNo").HeaderText = "Document No"

        '    gvDetails.Columns("Doc Type").IsVisible = True
        '    gvDetails.Columns("Doc Type").Width = 130
        '    gvDetails.Columns("Doc Type").HeaderText = "Document Type"

        '    gvDetails.Columns("Document Date").IsVisible = True
        '    gvDetails.Columns("Document Date").Width = 101
        '    gvDetails.Columns("Document Date").HeaderText = "Document Date"

        '    gvDetails.Columns("Ref Doc No").IsVisible = True
        '    gvDetails.Columns("Ref Doc No").Width = 101
        '    gvDetails.Columns("Ref Doc No").HeaderText = "Ref Doc No"

        '    gvDetails.Columns("Ref Doc Date").IsVisible = True
        '    gvDetails.Columns("Ref Doc Date").Width = 101
        '    gvDetails.Columns("Ref Doc Date").HeaderText = "Ref Doc Date"

        '    gvDetails.Columns("Sub Doc Type").IsVisible = True
        '    gvDetails.Columns("Sub Doc Type").Width = 130
        '    gvDetails.Columns("Sub Doc Type").HeaderText = "Sub Doc Type"

        '    gvDetails.Columns("DrAmt").IsVisible = False
        '    gvDetails.Columns("DrAmt").Width = 150
        '    gvDetails.Columns("DrAmt").HeaderText = "Debit Amt"

        '    gvDetails.Columns("CrAmt").IsVisible = False
        '    gvDetails.Columns("CrAmt").Width = 150
        '    gvDetails.Columns("CrAmt").HeaderText = "Credit Amt"
        '    'If ChkDocSumm.Checked Then
        '    '    gvDetails.Columns("Trans Amt").IsVisible = False
        '    '    gvDetails.Columns("Trans Amt").Width = 101
        '    '    gvDetails.Columns("Trans Amt").HeaderText = "Old Trans Amt"

        '    '    gvDetails.Columns("TransAmt1").IsVisible = False
        '    '    gvDetails.Columns("TransAmt1").Width = 101
        '    '    gvDetails.Columns("TransAmt1").HeaderText = "Transaction Amount"
        '    'Else
        '    gvDetails.Columns("Trans Amt").IsVisible = False
        '    gvDetails.Columns("Trans Amt").Width = 101
        '    gvDetails.Columns("Trans Amt").HeaderText = "Old Trans Amt"

        '    gvDetails.Columns("TransAmt1").IsVisible = True
        '    gvDetails.Columns("TransAmt1").Width = 101
        '    gvDetails.Columns("TransAmt1").HeaderText = "Transaction Amount"
        '    'End If

        '    gvDetails.Columns("BalAmt").IsVisible = False
        '    gvDetails.Columns("BalAmt").Width = 101
        '    gvDetails.Columns("BalAmt").HeaderText = "Bal Amount"

        '    gvDetails.Columns("CumAmt").IsVisible = False
        '    gvDetails.Columns("CumAmt").Width = 150
        '    gvDetails.Columns("CumAmt").HeaderText = "CumAmt"

        '    gvDetails.Columns("CumAmt1").IsVisible = True
        '    gvDetails.Columns("CumAmt1").Width = 150
        '    gvDetails.Columns("CumAmt1").HeaderText = "Balance"
        'End If
        gvDetails.ShowGroupPanel = True
        ' gvDetails.GroupDescriptors.Add(New GridGroupByExpression("CustomerCode As CustomerCode format ""{0}: {1}"" group by CustomerCode"))
        ' gvDetails.GroupDescriptors.Add(New GridGroupByExpression(" ParentCode As  ParentCode format ""{0}: {1}"" group by  ParentCode"))
        ' gvDetails.GroupDescriptors.Add(New GridGroupByExpression(" ChildCustCode As  MainCustCode format ""{0}: {1}"" group by  MainCustCode"))
        gvDetails.GroupDescriptors.Add(New GridGroupByExpression("DocumentNo As DocumentNo format ""{0}: {1}"" group by DocumentNo"))
        gvDetails.MasterTemplate.ExpandAllGroups()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim dramt As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(dramt)
        'Dim Tramt As New GridViewSummaryItem("Trans Amt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(Tramt)
        'If ChkDocSumm.Checked = True Then
        '    Dim CumAmt1 As New GridViewSummaryItem("CumAmt1", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(CumAmt1)
        'Else
        Dim summaryItem As New GridViewSummaryItem()
        summaryItem.FormatString = "{0:F2}"
        summaryItem.Name = "CumAmt1"
        summaryItem.AggregateExpression = "SUM([TransAmt1])"
        summaryRowItem.Add(summaryItem)
        gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'End If
        gvDetails.BestFitColumns()
    End Sub



    Private Sub refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles refreshbtn.Click
        btnrefresh = True
        IsDrillDown = False
        BackProcess = False
        btnBack.Enabled = False
        FilterStr = String.Empty
        PageSetupReport_ID = GetReportId()
        If chkCustGroupWise.Checked = True Then
            TemplateGridview = gvCustomerGroup
        ElseIf chkCustWise.Checked = True Then
            TemplateGridview = gvCustomer
        ElseIf chkNone.Checked = True Then
            TemplateGridview = gvDetails
        End If
        print()
        btnrefresh = False
    End Sub

    Private Sub chkReconcile_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReconcile.ToggleStateChanged
        If chkReconcile.Checked Then
            chkCustWise.Checked = True
        End If
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        'LoadCustomer()
    End Sub

    Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInactive.CheckedChanged
        'LoadCustomer()
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        'LoadCustomer()
    End Sub

    '-----------------Save Layout----------------------
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        PageSetupReport_ID = GetReportId()
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            If chkCustGroupWise.Checked = True Then
                gvCustomerGroup.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvCustomerGroup.SaveLayout(obj.GridLayout)
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                obj.GridColumns = gvCustomerGroup.ColumnCount
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            ElseIf chkCustWise.Checked = True Then
                gvCustomer.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvCustomer.SaveLayout(obj.GridLayout)
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                obj.GridColumns = gvCustomer.ColumnCount
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            ElseIf chkNone.Checked = True Then
                gvDetails.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvDetails.SaveLayout(obj.GridLayout)
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                obj.GridColumns = gvDetails.ColumnCount
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        End If
    End Sub

    '-----------------Delete Layout---------------------
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        PageSetupReport_ID = GetReportId()
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub gridHideVisible()
        gvDetails.Visible = False
        gvCustomer.Visible = False
        gvCustomerGroup.Visible = False
        If chkCustGroupWise.Checked Then
            gvCustomerGroup.Visible = True
        ElseIf chkCustWise.Checked Then
            gvCustomer.Visible = True
        Else
            gvDetails.Visible = True
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'If chkNone.Checked = False Then
        If gvDetails.Visible = True Then

            gvCustomer.Visible = True
            gvCustomerGroup.Visible = False
            gvDetails.Visible = False
            IsDrillDown = True
            BackProcess = True
            FilterStr = String.Empty
            txtCustomer.arrValueMember = Nothing
            chkCustWise.Checked = True
            PageSetupReport_ID = GetReportId()
            TemplateGridview = gvCustomer
        ElseIf gvCustomer.Visible = True Then
            'gvCustomerGroup.Visible = True
            'gvCustomer.Visible = False
            'gvDetails.Visible = False
            'btnBack.Enabled = False


            'If chkCustWise.Checked = True Then
            '    btnBack.Enabled = False
            '    gvCustomerGroup.Visible = False
            '    gvCustomer.Visible = True
            '    gvDetails.Visible = False
            '    IsDrillDown = True
            '    BackProcess = True
            '    FilterStr = String.Empty
            'Else
            gvCustomerGroup.Visible = True
            gvCustomer.Visible = False
            gvDetails.Visible = False
            IsDrillDown = True
            BackProcess = True
            FilterStr = String.Empty
            txtCustomerGroup.arrValueMember = Nothing
            chkCustGroupWise.Checked = True
            'End If
            PageSetupReport_ID = GetReportId()
            TemplateGridview = gvCustomerGroup
        End If
        'End If
    End Sub

    Private Sub gvCustomerGroup_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCustomerGroup.CellDoubleClick
        Try
            If clsCommon.myLen(gvCustomerGroup.CurrentRow.Cells("Cust_Group_Code").Value) > 0 Then
                'dvTemp = New DataView(dtCustomer)
                'FilterStr = "Cust_Group_Code='" + gvCustomerGroup.CurrentRow.Cells("Cust_Group_Code").Value + "'"
                'dvTemp.RowFilter = FilterStr
                'gvCustomer.DataSource = dvTemp.ToTable()

                chkCustWise.Checked = True
                Dim arrCustomerGroup As New ArrayList
                arrCustomerGroup.Add(gvCustomerGroup.CurrentRow.Cells("Cust_Group_Code").Value)
                txtCustomerGroup.arrValueMember = arrCustomerGroup
                btnrefresh = True
                print()

                FormatgvCustomer()
                gvCustomer.Visible = True
                gvCustomerGroup.Visible = False
                gvDetails.Visible = False
                PageSetupReport_ID = GetReportId()
                TemplateGridview = gvCustomer
                btnBack.Enabled = True
                IsDrillDown = True
                BackProcess = False
                btnrefresh = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvCustomer_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCustomer.CellDoubleClick
        Try
            If clsCommon.myLen(gvCustomer.CurrentRow.Cells("ACode").Value) > 0 Then
                'If e.Column Is gvCustomer.Columns("ACode") Or e.Column Is gvCustomer.Columns("AName") Then
                '    dvTemp = New DataView(dtMain)
                '    FilterStr = "ACode='" + gvCustomer.CurrentRow.Cells("ACode").Value + "'"
                '    dvTemp.RowFilter = FilterStr
                '    gvDetails.DataSource = dvTemp.ToTable()
                '    ' chkNone.Checked = True
                '    FormatGrid(False)
                'ElseIf e.Column Is gvCustomer.Columns("OpngBal") Then
                '    dvTemp = New DataView(dtOpening)
                '    FilterStr = "ACode='" + gvCustomer.CurrentRow.Cells("ACode").Value + "'"
                '    dvTemp.RowFilter = FilterStr
                '    gvDetails.DataSource = dvTemp.ToTable()
                '    ' chkNone.Checked = True
                '    FormatGrid(True)
                'ElseIf e.Column Is gvCustomer.Columns("Sales") Then
                '    dvTemp = New DataView(dtMain)
                '    FilterStr = "ACode='" + gvCustomer.CurrentRow.Cells("ACode").Value + "' AND ISNULL(Sales,0)<>0"
                '    dvTemp.RowFilter = FilterStr
                '    gvDetails.DataSource = dvTemp.ToTable()
                '    'chkNone.Checked = True
                '    FormatGrid(True)
                'ElseIf e.Column Is gvCustomer.Columns("CollectionRefund") Then
                '    dvTemp = New DataView(dtMain)
                '    FilterStr = "ACode='" + gvCustomer.CurrentRow.Cells("ACode").Value + "' AND ISNULL(CollectionRefund,0)<>0"
                '    dvTemp.RowFilter = FilterStr
                '    gvDetails.DataSource = dvTemp.ToTable()
                '    FormatGrid(True)
                'ElseIf e.Column Is gvCustomer.Columns("DrCrNote") Then
                '    dvTemp = New DataView(dtMain)
                '    FilterStr = "ACode='" + gvCustomer.CurrentRow.Cells("ACode").Value + "' AND ISNULL(DrCrNote,0)<>0"
                '    dvTemp.RowFilter = FilterStr
                '    gvDetails.DataSource = dvTemp.ToTable()
                '    'chkNone.Checked = True
                '    FormatGrid(True)
                'ElseIf e.Column Is gvCustomer.Columns("DrAmt") Then
                '    dvTemp = New DataView(dtMain)
                '    FilterStr = "ACode='" + gvCustomer.CurrentRow.Cells("ACode").Value + "' AND ISNULL(DrAmt,0)<>0"
                '    dvTemp.RowFilter = FilterStr
                '    gvDetails.DataSource = dvTemp.ToTable()
                '    'chkNone.Checked = True
                '    FormatGrid(True)
                'ElseIf e.Column Is gvCustomer.Columns("CrAmt") Then
                '    dvTemp = New DataView(dtMain)
                '    FilterStr = "ACode='" + gvCustomer.CurrentRow.Cells("ACode").Value + "' AND ISNULL(CrAmt,0)<>0"
                '    dvTemp.RowFilter = FilterStr
                '    gvDetails.DataSource = dvTemp.ToTable()
                '    'chkNone.Checked = True
                '    FormatGrid(True)
                'End If

                chkNone.Checked = True
                Dim arrCustomer As New ArrayList
                arrCustomer.Add(gvCustomer.CurrentRow.Cells("ACode").Value)
                txtCustomer.arrValueMember = arrCustomer
                btnrefresh = True
                print()

                gvDetails.Visible = True
                gvCustomerGroup.Visible = False
                gvCustomer.Visible = False
                PageSetupReport_ID = GetReportId()
                TemplateGridview = gvDetails
                btnBack.Enabled = True
                IsDrillDown = True
                BackProcess = False
                btnrefresh = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvReport_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDetails.CellDoubleClick
        'If cbgCompany.CheckedValue.Count > 1 Then
        '    RadMessageBox.Show("Please Select Only One Company")
        'Else
        If (chkCustGroupWise.Checked <> True) Then

            If clsCommon.myLen(e.Row.Cells.Item("DocNo").Value) > 0 Then
                Dim SoucrCode As String = clsCommon.myCstr(gvDetails.Rows(e.RowIndex).Cells.Item("SourceCode").Value)
                Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("DocNo").Value)


                If SoucrCode = "AR-PY" Or SoucrCode = "AR-RC" Or SoucrCode = "AR-UN" Or SoucrCode = "AR-OA" Or SoucrCode = "AR-RF" Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "AP-PY") = CompairStringResult.Equal Or clsCommon.CompairString(SoucrCode, "AP-MI") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "AP-IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(SoucrCode, "AP-DN") = CompairStringResult.Equal OrElse clsCommon.CompairString(SoucrCode, "AP-CN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "SD-IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "BK-TF") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "IC-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "PO-RC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "SD-LO") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.LoadOut, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "MM-TF") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "RV-TA") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "AR-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptAdjustmentEntry, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "SD-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "AR-IN") = CompairStringResult.Equal Or clsCommon.CompairString(SoucrCode, "AR-CR") = CompairStringResult.Equal Or clsCommon.CompairString(SoucrCode, "AR-DN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "VC-GL") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "SD-CSATRANS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "SD-CSATRANS-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, DocNo)
                Else
                    Return
                End If

            End If
        End If
    End Sub

    Private Sub ChkISParentCust_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkISParentCust.CheckStateChanged
        txtParentCustomer.Enabled = ChkISParentCust.Checked
    End Sub

    '===========================using new controls
    Private Sub txtCompany__My_Click(sender As Object, e As EventArgs) Handles txtCompany._My_Click
        strQry = "SELECT Comp_Code as Code,Comp_Name as Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        txtCompany.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCompany.arrValueMember, txtCompany.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        strQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        strQry += " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQry = "Select distinct TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER  " & _
                 " inner join tspl_customer_master on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=tspl_customer_master.Cust_Group_Code where tspl_customer_master.CSA_Type='Y'"

        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub

    Sub LoadCustomerNew()
        strQry = "select cust_code as Code, Customer_Name as Name from tspl_customer_master where  CSA_Type='Y'"
        If chkActive.Checked Then
            strQry += " and Status='N'"
        ElseIf chkInactive.Checked Then
            strQry += " and Status='Y'"
        End If
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        LoadCustomerNew()
    End Sub

    Private Sub txtParentCustomer__My_Click(sender As Object, e As EventArgs) Handles txtParentCustomer._My_Click
        strQry = "select cust_code as  Code, Customer_Name as  Name from tspl_customer_master "
        If ChkISParentCust.Checked Then
            strQry += " Where Parent_Customer_YN ='Y' "
        End If
        txtParentCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtParentCustomer.arrValueMember, txtParentCustomer.arrDispalyMember)
    End Sub

    Private Sub txtCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles txtCustomerCategory._My_Click
        strQry = "SELECT Distinct TSPL_CUSTOMER_MASTER.Cust_Category_Code AS [Code],TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC  AS [Description] FROM TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER  ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE  =  TSPL_CUSTOMER_MASTER.Cust_Category_Code where  tspl_customer_master.CSA_Type='Y'"
        txtCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerCategorySelector@CustomerLedger", strQry, "Code", "Description", txtCustomerCategory.arrValueMember, txtCustomerCategory.arrDispalyMember)
    End Sub

    Private Sub txtCustomerType__My_Click(sender As Object, e As EventArgs) Handles txtCustomerType._My_Click
        strQry = "SELECT DISTINCT TSPL_CUSTOMER_MASTER.Cust_Type_Code AS [Code],TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc  AS [Description] FROM TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code where  tspl_customer_master.CSA_Type='Y'"
        txtCustomerType.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerTypeSelector@CustomerLedger", strQry, "Code", "Description", txtCustomerType.arrValueMember, txtCustomerType.arrDispalyMember)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            If chkCustGroupWise.Checked = True Then
                If gvCustomerGroup.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If
            If chkCustWise.Checked = True Then
                If gvCustomer.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If
            If chkNone.Checked = True Then
                If gvDetails.Rows.Count <= 0 Then
                    Throw New Exception("No data found for Export.")
                End If
            End If

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptCSACustomerLedger & "'"))

            If chkCustGroupWise.Checked = True Then
                arrHeader.Add("Report Type: " + "Customer Group Wise")
            ElseIf chkCustWise.Checked = True Then
                arrHeader.Add("Report Type: " + "Customer Wise")
            ElseIf chkNone.Checked = True Then
                arrHeader.Add("Report Type: " + "Detail")
            End If

            If txtCompany.arrValueMember IsNot Nothing AndAlso txtCompany.arrValueMember.Count > 0 Then
                arrHeader.Add("Company : " + clsCommon.GetMulcallStringWithComma(txtCompany.arrValueMember))
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrValueMember))
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrValueMember))
            End If
            If txtCustomerCategory.arrValueMember IsNot Nothing AndAlso txtCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Category : " + clsCommon.GetMulcallStringWithComma(txtCustomerCategory.arrValueMember))
            End If
            If txtCustomerType.arrValueMember IsNot Nothing AndAlso txtCustomerType.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Type : " + clsCommon.GetMulcallStringWithComma(txtCustomerType.arrValueMember))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrValueMember))
            End If

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
                If chkCustGroupWise.Checked = True Then
                    transportSql.applyExportTemplate(gvCustomerGroup, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvCustomerGroup, "", Me.Text, , arrHeader)
                    'transportSql.exportdata(gvCustomerGroup, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                End If
                If chkCustWise.Checked = True Then
                    transportSql.applyExportTemplate(gvCustomer, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
                    ' transportSql.exportdata(gvCustomer, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                End If
                If chkNone.Checked = True Then
                    transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
                    'transportSql.exportdata(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                End If
                'transportSql.exportdata(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1)) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                If chkCustGroupWise.Checked = True Then
                    transportSql.applyExportTemplate(gvCustomerGroup, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Customer Ledger (Customer Group Wise)", gvCustomerGroup, arrHeader, "CustomerLedger", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                ElseIf chkCustWise.Checked = True Then
                    transportSql.applyExportTemplate(gvCustomer, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Customer Ledger (Customer Wise)", gvCustomer, arrHeader, "CustomerLedger", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                ElseIf chkNone.Checked = True Then
                    transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Customer Ledger", gvDetails, arrHeader, "CustomerLedger", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function GetReportId()
        Dim Report_Id As String = FormType
        If chkCustGroupWise.Checked = True Then
            Report_Id = Report_Id + "CG"
        ElseIf chkCustWise.Checked = True Then
            Report_Id = Report_Id + "C"
        ElseIf chkNone.Checked = True Then
            Report_Id = Report_Id + "D"
        End If
        Return Report_Id
    End Function
    Private Sub ReStoreGridLayout()
        Try
            PageSetupReport_ID = GetReportId()
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                If chkCustGroupWise.Checked = True Then
                    Dim obj As clsGridLayout = New clsGridLayout()
                    obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                    If Not obj Is Nothing AndAlso obj.GridColumns >= gvCustomerGroup.ColumnCount Then
                        Dim ii As Integer
                        For ii = 0 To gvCustomerGroup.Columns.Count - 1 Step ii + 1
                            gvCustomerGroup.Columns(ii).IsVisible = False
                            gvCustomerGroup.Columns(ii).VisibleInColumnChooser = True
                        Next
                        gvCustomerGroup.LoadLayout(obj.GridLayout)
                        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                    End If
                ElseIf chkCustWise.Checked = True Then
                    Dim obj As clsGridLayout = New clsGridLayout()
                    obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                    If Not obj Is Nothing AndAlso obj.GridColumns >= gvCustomer.ColumnCount Then
                        Dim ii As Integer
                        For ii = 0 To gvCustomer.Columns.Count - 1 Step ii + 1
                            gvCustomer.Columns(ii).IsVisible = False
                            gvCustomer.Columns(ii).VisibleInColumnChooser = True
                        Next
                        gvCustomer.LoadLayout(obj.GridLayout)
                        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                    End If
                ElseIf chkNone.Checked = True Then
                    Dim obj As clsGridLayout = New clsGridLayout()
                    obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                    If Not obj Is Nothing AndAlso obj.GridColumns >= gvDetails.ColumnCount Then
                        Dim ii As Integer
                        For ii = 0 To gvDetails.Columns.Count - 1 Step ii + 1
                            gvDetails.Columns(ii).IsVisible = False
                            gvDetails.Columns(ii).VisibleInColumnChooser = True
                        Next
                        gvDetails.LoadLayout(obj.GridLayout)
                        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                    End If
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

End Class
