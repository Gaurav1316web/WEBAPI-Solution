
''richa agarwal 22/05/2015 BM00000006837 remove grouping from grid,BM00000007095
''Balwinder BM00000008733 time is not cosider in date range in query
''BM00000008351
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Microsoft.VisualBasic.FileIO
Public Class frmRptTrialBalanceVC
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim arrBack As List(Of String)
    Const colSegCode As String = "SEGCODE"
    Const colSegName As String = "SEGNAME"
    Const colFrom As String = "FROMFILTER"
    Const colFromName As String = "FROMFILTERNAME"
    Const colTo As String = "TOFILTER"
    Const colToName As String = "TOFILTERNAME"
    Const colIsForAC As String = "ISFORAC"
    Dim StrQry As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Public arrCompany As ArrayList
    Public arrSourceCode As ArrayList
    Public arrEmployee As ArrayList
    Public arrLocationSegment As ArrayList
    Public arrAccount As ArrayList
    Public arrDepartment As ArrayList
    Public arrVISI As ArrayList
    Public arrMachine As ArrayList
    Public arrVehicle As ArrayList

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False

    Dim dt As DataTable = Nothing
    'Dim strERPStartDate As String
    Dim isRunDoubleClick As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptTrialBalanceCV)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
        btnRefresh.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub frmRptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            RefreshData()
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        cboReportType.DataSource = LoadReportType(False)
        cboReportType.ValueMember = "Code"
        cboReportType.DisplayMember = "Name"

        cboAddCVAfter.DataSource = LoadReportType(True)
        cboAddCVAfter.ValueMember = "Code"
        cboAddCVAfter.DisplayMember = "Name"

        cboAddCVAfter.SelectedValue = "Group Wise"

        chkDateRange.Checked = True
        arrBack = New List(Of String)
         

        RadPageView1.SelectedPage = RadPageViewPage1
        cboReportType.SelectedIndex = 0
        If System.DateTime.Now.Date.Month >= 1 AndAlso System.DateTime.Now.Date.Month <= 3 Then
            txtFromDate.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year - 1))
        Else
            txtFromDate.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year))
        End If
        txtToDate.Value = clsCommon.GETSERVERDATE()
        LoadBlankGrid()
        SetDataBaseGrid()


        If isDataLoad Then
            txtEmployee.arrValueMember = arrEmployee
            txtSourceCode.arrValueMember = arrSourceCode
            txtLocationSegmant.arrValueMember = arrLocationSegment
            txtAccount.arrValueMember = arrAccount
        End If
        '==================

        ReportFilters()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        funreset()

        isRunDoubleClick = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoubleClickOnVC, clsFixedParameterCode.DoubleClickOnVC, Nothing)) = 1, True, False)
    End Sub

    Function LoadReportType(ByVal isAddNone As Boolean) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        If isAddNone Then
            dr = dt.NewRow()
            dr("Code") = " "
            dr("Name") = "None"
            dt.Rows.Add(dr)
        End If

        dr = dt.NewRow()
        dr("Code") = "Main Group Wise"
        dr("Name") = "Main Group Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Group Wise"
        dr("Name") = "Group Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Sub Group Wise"
        dr("Name") = "Sub Group Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Main Account Wise"
        dr("Name") = "Main Account Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "GL Account Wise"
        dr("Name") = "GL Account Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Customer/Vedndor Wise"
        dr("Name") = "Customer/Vedndor Wise"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub ReportFilters()
        Dim AccSettings As String
        Dim VehicleSettings As String
        Dim DeptSettings As String
        Dim EmpSettings As String
        Dim MachineSettings As String
        Dim OthersSettings As String
        Dim LocationSettings As String

        AccSettings = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =1"))
        If (clsCommon.CompairString(AccSettings, "0") = CompairStringResult.Equal) Then
            'gbAcc.Visible = False
            txtAccount.Visible = False
            lblAccount.Visible = False
        End If
        VehicleSettings = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =2"))
        If (clsCommon.CompairString(VehicleSettings, "0") = CompairStringResult.Equal) Then
            'gbVehicle.Visible = False
            txtVehicle.Visible = False
            lblVehicle.Visible = False
        End If
        DeptSettings = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =3"))
        If (clsCommon.CompairString(DeptSettings, "0") = CompairStringResult.Equal) Then
            'gbDept.Visible = False
            txtDepartment.Visible = False
            lblDepartment.Visible = False
        End If
        EmpSettings = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =4"))
        If (clsCommon.CompairString(EmpSettings, "0") = CompairStringResult.Equal) Then
            'gbEmployee.Visible = False
            ' txtSourceCode.Visible = False
            txtEmployee.Visible = False
            lblEmployee.Visible = False
        End If
        MachineSettings = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =5"))
        If (clsCommon.CompairString(MachineSettings, "0") = CompairStringResult.Equal) Then
            'gbMachines.Visible = False
            txtMachine.Visible = False
            lblMachine.Visible = False
        End If
        OthersSettings = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =6"))
        If (clsCommon.CompairString(OthersSettings, "0") = CompairStringResult.Equal) Then
            'gbVisi.Visible = False
            txtVISIPMX.Visible = False
            lblVISIPMX.Visible = False
        End If
        LocationSettings = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Report_Filters From TSPL_GL_SEGMENT Where Seg_No =7"))
        If (clsCommon.CompairString(LocationSettings, "0") = CompairStringResult.Equal) Then
            'grpLocaSegment.Visible = False
            txtLocationSegmant.Visible = False
            lblLocationSegment.Visible = False
        End If
    End Sub

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub

    Private Sub LoadBlankGrid()


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        RefreshData()
        PrintData()
    End Sub

    Private Sub PrintData()
        'If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
        '    If chkRollupWise.Checked Then
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceRP", "Trial Balance")
        '    Else

        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalance", "Trial Balance")
        '    End If
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
        '    If chkRollupWise.Checked Then
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceSubLdgRP", "Trial Balance")
        '    Else
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceSubLdg", "Trial Balance")
        '    End If

        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
        '    If chkShowOPBal.Checked Then
        '        If chkRollupWise.Checked Then
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodRP", "Periodical Trial Balance")
        '        Else
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriod", "Periodical Trial Balance")
        '        End If
        '    Else
        '        If chkRollupWise.Checked Then
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodOPBalRP", "Periodical Trial Balance")
        '        Else
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodOPBal", "Periodical Trial Balance")
        '        End If
        '    End If
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
        '    frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceBasic", "Trial Balance")
        'End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        'chkRollupWise.Checked = False
        '===============Preeti Gupta=====
        txtEmployee.arrValueMember = Nothing
        txtSourceCode.arrValueMember = Nothing
        txtLocationSegmant.arrValueMember = Nothing

        txtEmployee.arrValueMember = Nothing
        txtVehicle.arrValueMember = Nothing
        txtDepartment.arrValueMember = Nothing
        txtMachine.arrValueMember = Nothing
        txtVISIPMX.arrValueMember = Nothing
        '================================
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        EnableDisableControls(True)
        strCustomerVendorCode = ""
        strCustomerVendorType = ""
        arrBack = New List(Of String)
    End Sub

    'Private Sub txtFrom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim qry As String = ""
    '    Dim WhrCls As String = ""
    '    Dim OrderCls As String = ""
    '    qry = "select Account_Group_Code as Code,Account_Group_Desc  as [Description] from TSPL_ACCOUNT_GROUPS "
    '    OrderCls = "Code"
    '    txtFrom.Value = clsCommon.ShowSelectForm("ACGroupFinder", qry, "Code", WhrCls, txtFrom.Value, OrderCls, isButtonClicked)
    '    lblFrom.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Group_Desc  from TSPL_ACCOUNT_GROUPS  where Account_Group_Code='" + txtFrom.Value + "'"))
    '    ''End If
    'End Sub

    'Private Sub txtTo__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim qry As String = ""
    '    Dim WhrCls As String = ""
    '    Dim OrderCls As String = ""
    '    qry = "select Account_Group_Code as Code,Account_Group_Desc  as [Description] from TSPL_ACCOUNT_GROUPS "
    '    OrderCls = "Code"
    '    txtTo.Value = clsCommon.ShowSelectForm("ACGroupFinder", qry, "Code", WhrCls, txtTo.Value, OrderCls, isButtonClicked)
    '    lblTO.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Group_Desc  from TSPL_ACCOUNT_GROUPS  where Account_Group_Code='" + txtTo.Value + "'"))
    'End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        'For ii As Integer = 0 To gvDB.Rows.Count - 1
        '    If txtCompany.arrValueMember Then
        '        arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
        '    End If
        'Next
        arrDBName.Add(objCommonVar.CurrDatabase)
        Return arrDBName
    End Function

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboReportType.SelectedIndexChanged
        'chkShowOPBal.Visible = False
        ''chkRollupWise.Visible = True
        'If clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
        '    chkShowOPBal.Visible = True
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
        '    'chkRollupWise.Visible = False
        '    chkMultipleRollup.Visible = False
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
        '    'chkRollupWise.Visible = True
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = False
        '    txtFromDate.Visible = False
        '    lblToDate.Text = "As On Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Location wise") = CompairStringResult.Equal Then

        'Else
        '    chkMultipleRollup.Visible = False
        'End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv1
        RefreshData()
    End Sub

    Public Sub RefreshData()
        Try
            gv1.EnableFiltering = True
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim FinalQty As String = ""
            Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)
            If dtCompany IsNot Nothing AndAlso dtCompany.Rows.Count > 0 Then
                Dim BaseQry As String = "select TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code ,TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc ,TSPL_ACCOUNT_GROUPS.Account_Group_Code ,TSPL_ACCOUNT_GROUPS.Account_Group_Desc ,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code ,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc ,TSPL_GL_ACCOUNTS.GL_Main_Code ,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc ,TSPL_JOURNAL_DETAILS.Account_code , TSPL_JOURNAL_DETAILS.Account_Desc ,TSPL_JOURNAL_DETAILS.Voucher_No,TSPL_JOURNAL_MASTER.Voucher_Date,TSPL_GL_ACCOUNTS.Account_Seg_Code7 as LocationCode,TSPL_LOCATION_MASTER.Location_Desc,TSPL_JOURNAL_MASTER.Source_Type as CustVend_Type,TSPL_JOURNAL_MASTER.CustVend_Code,case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_CUSTOMER_MASTER.Customer_Name else case when TSPL_JOURNAL_MASTER.Source_Type='V' then TSPL_VENDOR_MASTER.Vendor_Name else '' end end as CustVend_Name,Amount " + Environment.NewLine +
                 " from TSPL_JOURNAL_DETAILS  " + Environment.NewLine +
                 " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code " + Environment.NewLine +
                 " left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code " + Environment.NewLine +
                 " left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code " + Environment.NewLine +
                 " left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code  " + Environment.NewLine +
                 " left outer join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code " + Environment.NewLine +
                 " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine +
                 " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 " + Environment.NewLine +
                 " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_JOURNAL_MASTER.CustVend_Code  " + Environment.NewLine +
                 " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_JOURNAL_MASTER.CustVend_Code  " + Environment.NewLine +
                 " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A' " + Environment.NewLine

                BaseQry += " AND TSPL_JOURNAL_MASTER.Transaction_Type in ('','N'"
                If chkIncludeingAdjustmentEntry.Checked Then
                    BaseQry += ",'A'"
                End If
                If chkIncludeingClosingEntry.Checked Then
                    BaseQry += ",'X'"
                End If
                BaseQry += ")"

                If txtACGrpType.arrValueMember IsNot Nothing AndAlso txtACGrpType.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_ACCOUNT_MAIN_GROUPS.Group_Type in (" + clsCommon.GetMulcallString(txtACGrpType.arrValueMember) + ")"
                End If
                If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_JOURNAL_DETAILS.Account_code in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")"
                End If
                If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code2 in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ")"
                End If
                If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code3 in (" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ") "
                End If
                If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code4 in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ")"
                End If
                If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code5 in (" + clsCommon.GetMulcallString(txtMachine.arrValueMember) + ")"
                End If
                If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_JOURNAL_MASTER.Source_Code in (" + clsCommon.GetMulcallString(txtSourceCode.arrValueMember) + ")"
                End If
                If txtVISIPMX.arrValueMember IsNot Nothing AndAlso txtVISIPMX.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_JOURNAL_DETAILS.Account_Seg_Code6 in (" + clsCommon.GetMulcallString(txtVISIPMX.arrValueMember) + ")"
                End If
                If txtLocationSegmant.arrValueMember IsNot Nothing AndAlso txtLocationSegmant.arrValueMember.Count > 0 Then
                    BaseQry += " and  TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in  (" + clsCommon.GetMulcallString(txtLocationSegmant.arrValueMember) + ")"
                End If
                If txtMainGroup.arrValueMember IsNot Nothing AndAlso txtMainGroup.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code in (" + clsCommon.GetMulcallString(txtMainGroup.arrValueMember) + ")"
                End If
                If txtGroup.arrValueMember IsNot Nothing AndAlso txtGroup.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_ACCOUNT_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtGroup.arrValueMember) + ")"
                End If
                If txtSubGroup.arrValueMember IsNot Nothing AndAlso txtSubGroup.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code in (" + clsCommon.GetMulcallString(txtSubGroup.arrValueMember) + ")"
                End If
                If txtMainAccount.arrValueMember IsNot Nothing AndAlso txtMainAccount.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account in (" + clsCommon.GetMulcallString(txtMainAccount.arrValueMember) + ")"
                End If
                If clsCommon.myLen(cboAddCVAfter.SelectedValue) > 0 Then
                    BaseQry += " and len(isnull(TSPL_JOURNAL_MASTER.CustVend_Code,''))>0"
                End If

                Dim strConditionAmt As String = ""
                Dim strConditionOP As String = ""
                Dim strConditionCL As String = ""
                If chkDateRange.Checked Then
                    strConditionAmt = " case when Voucher_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  Voucher_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   then 1 else 0 end "
                    strConditionOP = " case when Voucher_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' then 1 else 0 end "
                    strConditionCL = " case when Voucher_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' then 1 else 0 end "
                Else
                    strConditionAmt = " case when  Voucher_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' then 1 else 0 end "
                End If
                Dim strCustVendorColumn As String = ""
                Dim strCustVendorColumnForGroup As String = ""
                If clsCommon.myLen(cboAddCVAfter.SelectedValue) > 0 Then
                    If cboReportType.SelectedIndex >= cboAddCVAfter.SelectedIndex Then
                        If clsCommon.myLen(strCustomerVendorType) > 0 Then
                            BaseQry += " and TSPL_JOURNAL_MASTER.Source_Type ='" + strCustomerVendorType + "'"
                        End If
                        If clsCommon.myLen(strCustomerVendorCode) > 0 Then
                            BaseQry += " and TSPL_JOURNAL_MASTER.CustVend_Code ='" + strCustomerVendorCode + "'"
                        End If
                        strCustVendorColumn = ",CustVend_Type,CustVend_Code,max(CustVend_Name) as CustVend_Name"
                        strCustVendorColumnForGroup = ",CustVend_Type,CustVend_Code having len(isnull(CustVend_Code,''))>0"
                    End If
                End If
                If Not clsCommon.CompairString(clsCommon.myCstr(cboAddCVAfter.SelectedValue), "None") = CompairStringResult.Equal Then
                    If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                        Dim BaseQryOP As String = BaseQry
                        BaseQryOP = clsCommon.ReplaceString(BaseQryOP, "TSPL_JOURNAL_MASTER", "TSPL_JOURNAL_MASTER_OP")
                        BaseQryOP = clsCommon.ReplaceString(BaseQryOP, "TSPL_JOURNAL_DETAILS", "TSPL_JOURNAL_DETAILS_OP")

                        BaseQry += " and 2= ( case when TSPL_JOURNAL_MASTER.Voucher_Date < '" + objCommonVar.ERPStartDate + "' and TSPL_JOURNAL_MASTER.Source_Code ='GL-JE' then 1 else 2 end )"

                        BaseQry = BaseQry + Environment.NewLine + " union all " + Environment.NewLine + BaseQryOP
                    End If
                End If

                If clsCommon.CompairString(cboReportType.SelectedValue, "Main Group Wise") = CompairStringResult.Equal Then
                    If chkDateRange.Checked Then
                        FinalQty = "select Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc" + strCustVendorColumn + ",sum(Amount * " + strConditionOP + ") as OPBal,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt,sum(Amount * " + strConditionCL + ") as CLBal  from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by Account_Main_Group_Code " + strCustVendorColumnForGroup
                    Else
                        FinalQty = "select Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc" + strCustVendorColumn + ",case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by Account_Main_Group_Code " + strCustVendorColumnForGroup
                    End If
                ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Group Wise") = CompairStringResult.Equal Then
                    If chkDateRange.Checked Then
                        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc" + strCustVendorColumn + ",sum(Amount * " + strConditionOP + ") as OPBal,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt,sum(Amount * " + strConditionCL + ") as CLBal  from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by Account_Group_Code " + strCustVendorColumnForGroup
                    Else
                        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc" + strCustVendorColumn + ",case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by Account_Group_Code " + strCustVendorColumnForGroup
                    End If
                ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Sub Group Wise") = CompairStringResult.Equal Then
                    If chkDateRange.Checked Then
                        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,max(Account_Group_Code) as Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc,Account_Sub_Group_Code,max(Account_Sub_Group_Desc) as Account_Sub_Group_Desc" + strCustVendorColumn + ",sum(Amount * " + strConditionOP + ") as OPBal,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt,sum(Amount * " + strConditionCL + ") as CLBal  from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by Account_Sub_Group_Code " + strCustVendorColumnForGroup
                    Else
                        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,max(Account_Group_Code) as Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc,Account_Sub_Group_Code,max(Account_Sub_Group_Desc) as Account_Sub_Group_Desc" + strCustVendorColumn + ",case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by Account_Sub_Group_Code " + strCustVendorColumnForGroup
                    End If
                ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Main Account Wise") = CompairStringResult.Equal Then
                    If chkDateRange.Checked Then
                        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,max(Account_Group_Code) as Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc,max(Account_Sub_Group_Code) as Account_Sub_Group_Code,max(Account_Sub_Group_Desc) as Account_Sub_Group_Desc,GL_Main_Code,max(Main_GL_Account_Desc) as Main_GL_Account_Desc" + strCustVendorColumn + ",sum(Amount * " + strConditionOP + ") as OPBal,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt,sum(Amount * " + strConditionCL + ") as CLBal  from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by GL_Main_Code " + strCustVendorColumnForGroup
                    Else
                        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,max(Account_Group_Code) as Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc,max(Account_Sub_Group_Code) as Account_Sub_Group_Code,max(Account_Sub_Group_Desc) as Account_Sub_Group_Desc,GL_Main_Code,max(Main_GL_Account_Desc) as Main_GL_Account_Desc" + strCustVendorColumn + ",case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by GL_Main_Code " + strCustVendorColumnForGroup
                    End If
                ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "GL Account Wise") = CompairStringResult.Equal Then
                    If chkDateRange.Checked Then
                        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,max(Account_Group_Code) as Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc,max(Account_Sub_Group_Code) as Account_Sub_Group_Code,max(Account_Sub_Group_Desc) as Account_Sub_Group_Desc,max(GL_Main_Code) as GL_Main_Code,max(Main_GL_Account_Desc) as Main_GL_Account_Desc,Account_code,max(Account_Desc) as Account_Desc" + strCustVendorColumn + ",sum(Amount * " + strConditionOP + ") as OPBal,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt,sum(Amount * " + strConditionCL + ") as CLBal  from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by Account_code " + strCustVendorColumnForGroup
                    Else
                        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,max(Account_Group_Code) as Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc,max(Account_Sub_Group_Code) as Account_Sub_Group_Code,max(Account_Sub_Group_Desc) as Account_Sub_Group_Desc,max(GL_Main_Code) as GL_Main_Code,max(Main_GL_Account_Desc) as Main_GL_Account_Desc,Account_code,max(Account_Desc) as Account_Desc" + strCustVendorColumn + ",case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by Account_code " + strCustVendorColumnForGroup
                    End If
                ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Customer/Vedndor Wise") = CompairStringResult.Equal Then
                    If chkDateRange.Checked Then
                        FinalQty = "select CustVend_Type,CustVend_Code,max(CustVend_Name) as CustVend_Name,sum(Amount * " + strConditionOP + ") as OPBal,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt,sum(Amount * " + strConditionCL + ") as CLBal  from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by CustVend_Type,CustVend_Code having len(isnull(CustVend_Code,''))>0 "
                    Else
                        FinalQty = "select CustVend_Type,CustVend_Code,max(CustVend_Name) as CustVend_Name,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt from (" + Environment.NewLine +
                        BaseQry + Environment.NewLine +
                        " )xxx group by CustVend_Type,CustVend_Code having len(isnull(CustVend_Code,''))>0 "
                    End If
                    'ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Customer/Vedndor And GL Account Wise") = CompairStringResult.Equal Then
                    '    If clsCommon.myLen(strCustomerVendorType) > 0 Then
                    '        BaseQry += " and TSPL_JOURNAL_MASTER.Source_Type ='" + strCustomerVendorType + "'"
                    '    End If
                    '    If clsCommon.myLen(strCustomerVendorCode) > 0 Then
                    '        BaseQry += " and TSPL_JOURNAL_MASTER.CustVend_Code ='" + strCustomerVendorCode + "'"
                    '    End If
                    '    If chkDateRange.Checked Then
                    '        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,max(Account_Group_Code) as Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc,max(Account_Sub_Group_Code) as Account_Sub_Group_Code,max(Account_Sub_Group_Desc) as Account_Sub_Group_Desc,max(GL_Main_Code) as GL_Main_Code,max(Main_GL_Account_Desc) as Main_GL_Account_Desc,Account_code,max(Account_Desc) as Account_Desc,CustVend_Type,CustVend_Code,max(CustVend_Name) as CustVend_Name,sum(Amount * " + strConditionOP + ") as OPBal,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt,sum(Amount * " + strConditionCL + ") as CLBal  from (" + Environment.NewLine +
                    '        BaseQry + Environment.NewLine +
                    '        " )xxx group by CustVend_Type,CustVend_Code,Account_code  having len(isnull(CustVend_Code,''))>0 "
                    '    Else
                    '        FinalQty = "select max(Account_Main_Group_Code) as Account_Main_Group_Code,max(Account_Main_Group_Desc) as Account_Main_Group_Desc,max(Account_Group_Code) as Account_Group_Code,max(Account_Group_Desc) as Account_Group_Desc,max(Account_Sub_Group_Code) as Account_Sub_Group_Code,max(Account_Sub_Group_Desc) as Account_Sub_Group_Desc,max(GL_Main_Code) as GL_Main_Code,max(Main_GL_Account_Desc) as Main_GL_Account_Desc,Account_code,max(Account_Desc) as Account_Desc,CustVend_Type, CustVend_Code,max(CustVend_Name) as CustVend_Name,case when sum(Amount * " + strConditionAmt + " )>0 then sum(Amount * " + strConditionAmt + ") else 0 end as DrAmt,-1 *(case when sum(Amount * " + strConditionAmt + ")>0 then 0  else sum(Amount * " + strConditionAmt + ") end) as CrAmt from (" + Environment.NewLine +
                    '        BaseQry + Environment.NewLine +
                    '        " )xxx group by CustVend_Type,CustVend_Code,Account_code having len(isnull(CustVend_Code,''))>0 "
                    '    End If
                End If
                dt = clsDBFuncationality.GetDataTable(FinalQty)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("No Data found to display")
                End If

                SetGridFormation(IIf(clsCommon.myLen(strCustVendorColumn) > 0, True, False))
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Company Details Not found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation(ByVal isVendorCustomerWise As Boolean)
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As GridViewSummaryItem
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt
        RadPageView1.SelectedPage = RadPageViewPage2
        RadPageViewPage2.Text = "Report ( " + clsCommon.myCstr(cboReportType.SelectedValue) + IIf(isVendorCustomerWise, " and Vendor/Customer Wise", "") + " )"
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        If clsCommon.CompairString(cboReportType.SelectedValue, "Main Group Wise") = CompairStringResult.Equal Then
            gv1.Columns("Account_Main_Group_Code").IsVisible = True
            gv1.Columns("Account_Main_Group_Code").HeaderText = "Main Group Code"

            gv1.Columns("Account_Main_Group_Desc").IsVisible = True
            gv1.Columns("Account_Main_Group_Desc").HeaderText = "Main Group"

            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").HeaderText = "Debit"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").HeaderText = "Credit"
        ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Group Wise") = CompairStringResult.Equal Then
            gv1.Columns("Account_Main_Group_Code").IsVisible = True
            gv1.Columns("Account_Main_Group_Code").HeaderText = "Main Group Code"

            gv1.Columns("Account_Main_Group_Desc").IsVisible = True
            gv1.Columns("Account_Main_Group_Desc").HeaderText = "Main Group"

            gv1.Columns("Account_Group_Code").IsVisible = True
            gv1.Columns("Account_Group_Code").HeaderText = "Group Code"

            gv1.Columns("Account_Group_Desc").IsVisible = True
            gv1.Columns("Account_Group_Desc").HeaderText = "Group"


            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").HeaderText = "Debit"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").HeaderText = "Credit"
        ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Sub Group Wise") = CompairStringResult.Equal Then
            gv1.Columns("Account_Main_Group_Code").IsVisible = True
            gv1.Columns("Account_Main_Group_Code").HeaderText = "Main Group Code"

            gv1.Columns("Account_Main_Group_Desc").IsVisible = True
            gv1.Columns("Account_Main_Group_Desc").HeaderText = "Main Group"

            gv1.Columns("Account_Group_Code").IsVisible = True
            gv1.Columns("Account_Group_Code").HeaderText = "Group Code"

            gv1.Columns("Account_Group_Desc").IsVisible = True
            gv1.Columns("Account_Group_Desc").HeaderText = "Group"

            gv1.Columns("Account_Sub_Group_Code").IsVisible = True
            gv1.Columns("Account_Sub_Group_Code").HeaderText = "Sub Group Code"

            gv1.Columns("Account_Sub_Group_Desc").IsVisible = True
            gv1.Columns("Account_Sub_Group_Desc").HeaderText = "Sub Group"


            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").HeaderText = "Debit"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").HeaderText = "Credit"
        ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Main Account Wise") = CompairStringResult.Equal Then
            gv1.Columns("Account_Main_Group_Code").IsVisible = True
            gv1.Columns("Account_Main_Group_Code").HeaderText = "Main Group Code"

            gv1.Columns("Account_Main_Group_Desc").IsVisible = True
            gv1.Columns("Account_Main_Group_Desc").HeaderText = "Main Group"

            gv1.Columns("Account_Group_Code").IsVisible = True
            gv1.Columns("Account_Group_Code").HeaderText = "Group Code"

            gv1.Columns("Account_Group_Desc").IsVisible = True
            gv1.Columns("Account_Group_Desc").HeaderText = "Group"

            gv1.Columns("Account_Sub_Group_Code").IsVisible = True
            gv1.Columns("Account_Sub_Group_Code").HeaderText = "Sub Group Code"

            gv1.Columns("Account_Sub_Group_Desc").IsVisible = True
            gv1.Columns("Account_Sub_Group_Desc").HeaderText = "Sub Group"

            gv1.Columns("GL_Main_Code").IsVisible = True
            gv1.Columns("GL_Main_Code").HeaderText = "Main Account Code"

            gv1.Columns("Main_GL_Account_Desc").IsVisible = True
            gv1.Columns("Main_GL_Account_Desc").HeaderText = "Main Account"

            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").HeaderText = "Debit"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").HeaderText = "Credit"
        ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "GL Account Wise") = CompairStringResult.Equal Then
            gv1.Columns("Account_Main_Group_Code").IsVisible = True
            gv1.Columns("Account_Main_Group_Code").HeaderText = "Main Group Code"

            gv1.Columns("Account_Main_Group_Desc").IsVisible = True
            gv1.Columns("Account_Main_Group_Desc").HeaderText = "Main Group"

            gv1.Columns("Account_Group_Code").IsVisible = True
            gv1.Columns("Account_Group_Code").HeaderText = "Group Code"

            gv1.Columns("Account_Group_Desc").IsVisible = True
            gv1.Columns("Account_Group_Desc").HeaderText = "Group"

            gv1.Columns("Account_Sub_Group_Code").IsVisible = True
            gv1.Columns("Account_Sub_Group_Code").HeaderText = "Sub Group Code"

            gv1.Columns("Account_Sub_Group_Desc").IsVisible = True
            gv1.Columns("Account_Sub_Group_Desc").HeaderText = "Sub Group"

            gv1.Columns("GL_Main_Code").IsVisible = True
            gv1.Columns("GL_Main_Code").HeaderText = "Main Account Code"

            gv1.Columns("Main_GL_Account_Desc").IsVisible = True
            gv1.Columns("Main_GL_Account_Desc").HeaderText = "Main Account"

            gv1.Columns("Account_code").IsVisible = True
            gv1.Columns("Account_code").HeaderText = "GL Account Code"

            gv1.Columns("Account_Desc").IsVisible = True
            gv1.Columns("Account_Desc").HeaderText = "GL Account"

            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").HeaderText = "Debit"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").HeaderText = "Credit"
        ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Customer/Vedndor Wise") = CompairStringResult.Equal Then
            gv1.Columns("CustVend_Type").IsVisible = False
            gv1.Columns("CustVend_Type").HeaderText = "Vendor/Customer Type"

            gv1.Columns("CustVend_Code").IsVisible = True
            gv1.Columns("CustVend_Code").HeaderText = "Vendor/Customer Code"

            gv1.Columns("CustVend_Name").IsVisible = True
            gv1.Columns("CustVend_Name").HeaderText = "Vendor/Customer"

            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").HeaderText = "Debit"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").HeaderText = "Credit"
            'ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Customer/Vedndor And GL Account Wise") = CompairStringResult.Equal Then
            '    gv1.Columns("Account_Main_Group_Code").IsVisible = True
            '    gv1.Columns("Account_Main_Group_Code").HeaderText = "Main Group Code"

            '    gv1.Columns("Account_Main_Group_Desc").IsVisible = True
            '    gv1.Columns("Account_Main_Group_Desc").HeaderText = "Main Group"

            '    gv1.Columns("Account_Group_Code").IsVisible = True
            '    gv1.Columns("Account_Group_Code").HeaderText = "Group Code"

            '    gv1.Columns("Account_Group_Desc").IsVisible = True
            '    gv1.Columns("Account_Group_Desc").HeaderText = "Group"

            '    gv1.Columns("Account_Sub_Group_Code").IsVisible = True
            '    gv1.Columns("Account_Sub_Group_Code").HeaderText = "Sub Group Code"

            '    gv1.Columns("Account_Sub_Group_Desc").IsVisible = True
            '    gv1.Columns("Account_Sub_Group_Desc").HeaderText = "Sub Group"

            '    gv1.Columns("GL_Main_Code").IsVisible = True
            '    gv1.Columns("GL_Main_Code").HeaderText = "Main Account Code"

            '    gv1.Columns("Main_GL_Account_Desc").IsVisible = True
            '    gv1.Columns("Main_GL_Account_Desc").HeaderText = "Main Account"

            '    gv1.Columns("Account_code").IsVisible = True
            '    gv1.Columns("Account_code").HeaderText = "GL Account Code"

            '    gv1.Columns("Account_Desc").IsVisible = True
            '    gv1.Columns("Account_Desc").HeaderText = "GL Account"

            '    gv1.Columns("CustVend_Type").IsVisible = False
            '    gv1.Columns("CustVend_Type").HeaderText = "Vendor/Customer Type"

            '    gv1.Columns("CustVend_Code").IsVisible = True
            '    gv1.Columns("CustVend_Code").HeaderText = "Vendor/Customer Code"

            '    gv1.Columns("CustVend_Name").IsVisible = True
            '    gv1.Columns("CustVend_Name").HeaderText = "Vendor/Customer"

            '    gv1.Columns("DrAmt").IsVisible = True
            '    gv1.Columns("DrAmt").HeaderText = "Debit"

            '    gv1.Columns("CrAmt").IsVisible = True
            '    gv1.Columns("CrAmt").HeaderText = "Credit"
        End If

        item1 = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        If chkDateRange.Checked Then
            gv1.Columns("OPBal").IsVisible = True
            gv1.Columns("OPBal").HeaderText = "Opening Balance"

            gv1.Columns("CLBal").IsVisible = True
            gv1.Columns("CLBal").HeaderText = "Closing Balance"

            item1 = New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            item1 = New GridViewSummaryItem("CLBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
        End If

        If isVendorCustomerWise Then
            gv1.Columns("CustVend_Type").IsVisible = False
            gv1.Columns("CustVend_Type").HeaderText = "Vendor/Customer Type"

            gv1.Columns("CustVend_Code").IsVisible = True
            gv1.Columns("CustVend_Code").HeaderText = "Vendor/Customer Code"

            gv1.Columns("CustVend_Name").IsVisible = True
            gv1.Columns("CustVend_Name").HeaderText = "Vendor/Customer"
        End If

        gv1.MasterTemplate.ExpandAllGroups()
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        EnableDisableControls(False)
        gv1.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtMainGroup.Enabled = Val
        txtGroup.Enabled = Val
        txtSubGroup.Enabled = Val
        txtMainAccount.Enabled = Val
        chkDateRange.Enabled = Val
        cboAddCVAfter.Enabled = Val
        chkIncludeingAdjustmentEntry.Enabled = Val
        chkIncludeingClosingEntry.Enabled = Val
        txtACGrpType.Enabled = Val
        cboReportType.Enabled = Val
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        FlowLayoutPanel2.Enabled = Val
        FlowLayoutPanel1.Enabled = Val
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
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
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    Private Sub txtLocationSegmant__My_Click(sender As Object, e As EventArgs) Handles txtLocationSegmant._My_Click
        Dim qry As String = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        qry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        qry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        qry += " order by xxx.Loc_Segment_Code"
        txtLocationSegmant.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationMulSel", qry, "Code", "Name", txtLocationSegmant.arrValueMember, txtLocationSegmant.arrDispalyMember)
        SetDiplayMember(txtLocationSegmant, "Description", "TSPL_LOCATION_MASTER", "Loc_Segment_Code")
    End Sub

    Private Sub txtAccount__My_Click(sender As Object, e As EventArgs) Handles txtAccount._My_Click
        Dim qry As String = " select Account_Code as Code,[Description] as [Name] from TSPL_GL_ACCOUNTS"
        txtAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("AccountMulSel", qry, "Code", "Name", txtAccount.arrValueMember, txtAccount.arrDispalyMember)
    End Sub

    Private Sub txtSourceCode__My_Click(sender As Object, e As EventArgs) Handles txtSourceCode._My_Click
        Dim qry As String = " Select Distinct Source_Code as Code, Source_Desc as Name from TSPL_JOURNAL_MASTER"
        txtSourceCode.arrValueMember = clsCommon.ShowMultipleSelectForm("SourceCodeMulSel", qry, "Code", "Name", txtSourceCode.arrValueMember, txtSourceCode.arrDispalyMember)
    End Sub

    Private Sub txtEmployee__My_Click(sender As Object, e As EventArgs) Handles txtEmployee._My_Click
        Dim qry As String = "select Distinct segment_code as [Code],description as [Name] from tspl_Gl_segment_code where Segment_name ='Employees'"
        txtEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("EmployeeMulSel", qry, "Code", "Name", txtEmployee.arrValueMember, txtEmployee.arrDispalyMember)
    End Sub


    Private Sub txtDepartment__My_Click(sender As Object, e As EventArgs) Handles txtDepartment._My_Click
        Dim qry As String = " Select Segment_code as Code , Description as Name  from TSPL_GL_SEGMENT_CODE Where Seg_No=3"
        txtDepartment.arrValueMember = clsCommon.ShowMultipleSelectForm("Department", qry, "Code", "Name", txtDepartment.arrValueMember, txtDepartment.arrDispalyMember)
    End Sub

    Private Sub txtVISIPMX__My_Click(sender As Object, e As EventArgs) Handles txtVISIPMX._My_Click
        Dim qry As String = "Select Segment_code as COde , Description  as Name from TSPL_GL_SEGMENT_CODE Where Seg_No=6"
        txtVISIPMX.arrValueMember = clsCommon.ShowMultipleSelectForm("VISI", qry, "Code", "Name", txtVISIPMX.arrValueMember, txtVISIPMX.arrDispalyMember)
    End Sub

    Private Sub txtMachine__My_Click(sender As Object, e As EventArgs) Handles txtMachine._My_Click
        Dim qry As String = "Select Segment_code as Code , Description as Name from TSPL_GL_SEGMENT_CODE Where Seg_No=5"
        txtVISIPMX.arrValueMember = clsCommon.ShowMultipleSelectForm("Machine", qry, "Code", "Name", txtVISIPMX.arrValueMember, txtVISIPMX.arrDispalyMember)
    End Sub

    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click

        Dim qry As String = "Select Segment_code as Code , Description as Name from TSPL_GL_SEGMENT_CODE Where Seg_No=2"
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("Vehicle", qry, "Code", "Name", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)

    End Sub

    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtACGrpType._My_Click
        Dim qry As String = "select distinct Group_Type as Code from TSPL_ACCOUNT_MAIN_GROUPS  "
        txtACGrpType.arrValueMember = clsCommon.ShowMultipleSelectForm("rptACMGTlBal", qry, "Code", "", txtACGrpType.arrValueMember, Nothing)
    End Sub

    Private Sub chkDateRange_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkDateRange.ToggleStateChanged
        If chkDateRange.Checked Then
            lblFromdate.Visible = True
            txtFromDate.Visible = True
            lblToDate.Text = "To Date"
        Else
            lblFromdate.Visible = False
            txtFromDate.Visible = False
            lblToDate.Text = "As On Date"
        End If
    End Sub

    Private Sub txtMainGroup__My_Click(sender As Object, e As EventArgs) Handles txtMainGroup._My_Click
        Dim qry As String = "select Account_Main_Group_Code as Code,Account_Main_Group_Desc as Name,Group_Type as Type from TSPL_ACCOUNT_MAIN_GROUPS"
        txtMainGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("rptMGTilBal", qry, "Code", "Name", txtMainGroup.arrValueMember, txtMainGroup.arrDispalyMember)
    End Sub

    Private Sub txtGroup__My_Click(sender As Object, e As EventArgs) Handles txtGroup._My_Click
        Dim qry As String = " select Account_Group_Code,Account_Group_Desc  from TSPL_ACCOUNT_GROUPS  "
        txtGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("cvtrailACG", qry, "Account_Group_Code", "Account_Group_Desc", txtGroup.arrValueMember, txtGroup.arrDispalyMember)
    End Sub

    Private Sub txtSubGroup__My_Click(sender As Object, e As EventArgs) Handles txtSubGroup._My_Click
        Dim qry As String = " select Account_Sub_Group_Code,Account_Sub_Group_Desc  from TSPL_ACCOUNT_SUB_GROUPS  "
        txtSubGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("cvtrailACSG", qry, "Account_Sub_Group_Code", "Account_Sub_Group_Desc", txtSubGroup.arrValueMember, txtSubGroup.arrDispalyMember)
    End Sub

    Private Sub txtMainAccount__My_Click(sender As Object, e As EventArgs) Handles txtMainAccount._My_Click
        Dim qry As String = "select Main_GL_Account,Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
        txtMainAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("cvtrailMAC", qry, "Main_GL_Account", "Main_GL_Account_Desc", txtMainAccount.arrValueMember, txtMainAccount.arrDispalyMember)
    End Sub

    Dim arrMainGroup As ArrayList
    Dim arrGroup As ArrayList
    Dim arrSubGroup As ArrayList
    Dim arrMainAccount As ArrayList
    Dim arrGLAccount As ArrayList
    Dim strCustomerVendorCode As String
    Dim strCustomerVendorType As String

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If isRunDoubleClick Then
                If clsCommon.myLen(cboAddCVAfter.SelectedValue) > 0 Then
                    If cboReportType.SelectedIndex >= cboAddCVAfter.SelectedIndex Then
                        strCustomerVendorType = clsCommon.myCstr(gv1.CurrentRow.Cells("CustVend_Type").Value)
                        strCustomerVendorCode = clsCommon.myCstr(gv1.CurrentRow.Cells("CustVend_Code").Value)
                    End If
                End If
                If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Main Group Wise") = CompairStringResult.Equal Then
                    If Not arrBack.Contains("Main Group Wise") Then
                        arrBack.Add("Main Group Wise")
                    End If
                    cboReportType.SelectedValue = "Group Wise"
                    arrMainGroup = New ArrayList()
                    arrMainGroup = txtMainGroup.arrValueMember()

                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Main_Group_Code").Value))
                    txtMainGroup.arrValueMember = tmp

                    tmp = New ArrayList
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Main_Group_Desc").Value))
                    txtMainGroup.arrDispalyMember = tmp

                    RefreshData()
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Group Wise") = CompairStringResult.Equal Then
                    If Not arrBack.Contains("Group Wise") Then
                        arrBack.Add("Group Wise")
                    End If
                    cboReportType.SelectedValue = "Sub Group Wise"
                    arrGroup = New ArrayList()
                    arrGroup = txtGroup.arrValueMember()

                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Group_Code").Value))
                    txtGroup.arrValueMember = tmp

                    tmp = New ArrayList
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Group_Desc").Value))
                    txtGroup.arrDispalyMember = tmp
                    RefreshData()
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Sub Group Wise") = CompairStringResult.Equal Then
                    If Not arrBack.Contains("Sub Group Wise") Then
                        arrBack.Add("Sub Group Wise")
                    End If

                    cboReportType.SelectedValue = "Main Account Wise"
                    arrSubGroup = New ArrayList()
                    arrSubGroup = txtSubGroup.arrValueMember()

                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Sub_Group_Code").Value))
                    txtSubGroup.arrValueMember = tmp

                    tmp = New ArrayList
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Sub_Group_Desc").Value))
                    txtSubGroup.arrDispalyMember = tmp

                    RefreshData()

                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Main Account Wise") = CompairStringResult.Equal Then
                    If Not arrBack.Contains("Main Account Wise") Then
                        arrBack.Add("Main Account Wise")
                    End If
                    cboReportType.SelectedValue = "GL Account Wise"
                    arrMainGroup = New ArrayList()
                    arrMainGroup = txtMainAccount.arrValueMember()

                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("GL_Main_Code").Value))
                    txtMainAccount.arrValueMember = tmp

                    tmp = New ArrayList
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Main_GL_Account_Desc").Value))
                    txtMainAccount.arrDispalyMember = tmp

                    RefreshData()
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "GL Account Wise") = CompairStringResult.Equal Then
                    OpenGLTransReport()
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Customer/Vedndor Wise") = CompairStringResult.Equal Then
                    If Not arrBack.Contains("Customer/Vedndor Wise") Then
                        arrBack.Add("Customer/Vedndor Wise")
                    End If
                    cboReportType.SelectedValue = "Customer/Vedndor And GL Account Wise"
                    arrMainGroup = New ArrayList()
                    arrMainGroup = txtMainAccount.arrValueMember()

                    strCustomerVendorType = clsCommon.myCstr(gv1.CurrentRow.Cells("CustVend_Type").Value)
                    strCustomerVendorCode = clsCommon.myCstr(gv1.CurrentRow.Cells("CustVend_Code").Value)

                    RefreshData()
                    'ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Customer/Vedndor And GL Account Wise") = CompairStringResult.Equal Then
                    '    OpenGLTransReport()
                End If
                PageSetupReport_ID = GetReportID()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub OpenGLTransReport()
        If gv1.CurrentRow IsNot Nothing Then
            Dim strACode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Account_code").Value)
            If clsCommon.myLen(strACode) > 0 Then
                Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                frm.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
                frm.strPrevFormACode = strACode
                frm.strPrevFormAName = clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Desc").Value)
                frm.dTPrevFormFromDate = txtFromDate.Value
                frm.dTPrevFormToDate = txtToDate.Value
                If Not chkDateRange.Checked Then
                    frm.RadLabel7.Visible = False
                    frm.txtFromDate.Visible = False
                    frm.MyLabel2.Text = "As On date"
                Else
                    frm.RadLabel7.Visible = True
                    frm.txtFromDate.Visible = True
                    frm.MyLabel2.Text = "To Date"
                End If
                Dim i As Integer = 0

                frm.arrLocSeg = New ArrayList()
                If txtLocationSegmant.arrValueMember IsNot Nothing AndAlso txtLocationSegmant.arrValueMember.Count > 0 Then
                    frm.arrLocSeg = txtLocationSegmant.arrValueMember
                End If
                frm.arrAcc = New ArrayList()
                frm.arrAcc.Add(strACode)

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
                If txtVISIPMX.arrValueMember IsNot Nothing AndAlso txtVISIPMX.arrValueMember.Count > 0 Then
                    frm.arrVisi = txtVISIPMX.arrValueMember
                End If
                If clsCommon.myLen(cboAddCVAfter.SelectedValue) > 0 Then
                    If clsCommon.CompairString(strCustomerVendorType, "V") = CompairStringResult.Equal Then
                        frm.strPreVendorCode = strCustomerVendorCode
                    Else
                        frm.strPreCustomerCode = strCustomerVendorCode
                    End If
                    frm.IsVendorCustomerWiseSummary = True
                Else
                    frm.IsVendorCustomerWiseSummary = False
                End If
                


                'frm.MdiParent = MDI
                frm.Show()
            End If
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(cboAddCVAfter.SelectedValue) > 0 Then
                If cboReportType.SelectedIndex >= cboAddCVAfter.SelectedIndex Then
                    strCustomerVendorType = ""
                    strCustomerVendorCode = ""
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Main Group Wise") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Group Wise") = CompairStringResult.Equal Then
                If arrBack.Contains("Main Group Wise") Then
                    arrBack.Remove("Main Group Wise")
                    cboReportType.SelectedValue = "Main Group Wise"
                    txtMainGroup.arrValueMember = arrMainGroup
                    RefreshData()
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Sub Group Wise") = CompairStringResult.Equal Then
                If arrBack.Contains("Group Wise") Then
                    arrBack.Remove("Group Wise")
                    cboReportType.SelectedValue = "Group Wise"
                    txtGroup.arrValueMember = arrGroup
                    RefreshData()
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Main Account Wise") = CompairStringResult.Equal Then
                If arrBack.Contains("Sub Group Wise") Then
                    arrBack.Remove("Sub Group Wise")
                    cboReportType.SelectedValue = "Sub Group Wise"
                    txtSubGroup.arrValueMember = arrSubGroup
                    RefreshData()
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "GL Account Wise") = CompairStringResult.Equal Then
                If arrBack.Contains("Main Account Wise") Then
                    arrBack.Remove("Main Account Wise")
                    cboReportType.SelectedValue = "Main Account Wise"
                    txtMainAccount.arrValueMember = arrMainAccount
                    RefreshData()
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Customer/Vedndor Wise") = CompairStringResult.Equal Then
                If arrBack.Contains("Sub Group Wise") Then
                    arrBack.Remove("Sub Group Wise")
                    cboReportType.SelectedValue = "Sub Group Wise"
                    txtSubGroup.arrValueMember = arrSubGroup
                    RefreshData()
                End If
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Customer/Vedndor And GL Account Wise") = CompairStringResult.Equal Then
                '    If arrBack.Contains("Customer/Vedndor Wise") Then
                '        arrBack.Remove("Customer/Vedndor Wise")
                '        strCustomerVendorCode = ""
                '        strCustomerVendorType = ""
                '        cboReportType.SelectedValue = "Customer/Vedndor Wise"
                '        RefreshData()
                '    End If
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = "VCTB"
        If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Main Group Wise") = CompairStringResult.Equal Then
            ReportID += "MGW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Group Wise") = CompairStringResult.Equal Then
            ReportID += "GW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Sub Group Wise") = CompairStringResult.Equal Then
            ReportID += "SGW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Main Account Wise") = CompairStringResult.Equal Then
            ReportID += "MAW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "GL Account Wise") = CompairStringResult.Equal Then
            ReportID += "GLACW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Customer/Vedndor Wise") = CompairStringResult.Equal Then
            ReportID += "CVW"
        End If
        If clsCommon.myLen(cboAddCVAfter.SelectedValue) > 0 Then
            If cboReportType.SelectedIndex >= cboAddCVAfter.SelectedIndex Then
                ReportID += "VC"
            End If
        End If
        If chkDateRange.Checked Then
            ReportID += "DR"
        End If
        Return ReportID
    End Function

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTrialBalanceCV & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Type : " + clsCommon.myCstr(cboReportType.SelectedValue))
            '+ IIf(chkCusVendWiseSummary.Checked, "[Customer/Vendor Wise]", ""))
            If chkDateRange.Checked Then
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            Else
                arrHeader.Add("As On Date" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            End If
            If txtACGrpType.arrValueMember IsNot Nothing AndAlso txtACGrpType.arrValueMember.Count > 0 Then
                arrHeader.Add(txtACGrpType.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtACGrpType.arrValueMember))
            End If
            If txtMainGroup.arrValueMember IsNot Nothing AndAlso txtMainGroup.arrValueMember.Count > 0 Then
                arrHeader.Add(txtMainGroup.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtMainGroup.arrDispalyMember))
            End If
            If txtGroup.arrValueMember IsNot Nothing AndAlso txtGroup.arrValueMember.Count > 0 Then
                arrHeader.Add(txtGroup.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtGroup.arrDispalyMember))
            End If
            If txtSubGroup.arrValueMember IsNot Nothing AndAlso txtSubGroup.arrValueMember.Count > 0 Then
                arrHeader.Add(txtSubGroup.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtSubGroup.arrDispalyMember))
            End If
            If txtMainAccount.arrValueMember IsNot Nothing AndAlso txtMainAccount.arrValueMember.Count > 0 Then
                arrHeader.Add(txtMainAccount.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtMainAccount.arrDispalyMember))
            End If
            If txtLocationSegmant.arrValueMember IsNot Nothing AndAlso txtLocationSegmant.arrValueMember.Count > 0 Then
                arrHeader.Add(txtLocationSegmant.MyLinkLable1.Text + " :  " + clsCommon.GetMulcallStringWithComma(txtLocationSegmant.arrDispalyMember))
            End If
            If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                arrHeader.Add(txtAccount.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtAccount.arrValueMember))
            End If
            If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                arrHeader.Add(txtEmployee.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtEmployee.arrValueMember))
            End If
            If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                arrHeader.Add(txtSourceCode.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtSourceCode.arrValueMember))
            End If
            If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                arrHeader.Add(txtDepartment.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtDepartment.arrValueMember))
            End If
            If txtVISIPMX.arrValueMember IsNot Nothing AndAlso txtVISIPMX.arrValueMember.Count > 0 Then
                arrHeader.Add(txtVISIPMX.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtVISIPMX.arrValueMember))
            End If
            If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
                arrHeader.Add(txtMachine.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtMachine.arrValueMember))
            End If
            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                arrHeader.Add(txtVehicle.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtVehicle.arrValueMember))
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
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
