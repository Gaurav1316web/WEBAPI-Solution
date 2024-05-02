
''richa agarwal 22/05/2015 BM00000006837 remove grouping from grid,BM00000007095
''Balwinder BM00000008733 time is not cosider in date range in query
''BM00000008351
''BM00000009438 by balwinder on 16/08/16 
''BM00000009461 by balwinder on 18/10/2016.Add fiscal year master.
Imports common
Imports System.IO
Public Class frmRptTrialBalanceNew
    Inherits FrmMainTranScreen
#Region "Variables"
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
    Dim isRunDoubleClick As Boolean = False
    Dim dt As DataTable = Nothing
    Dim gvIndex As Integer = -1


    Dim strSourceTransaction As String = ""
    Dim strSourceDoc As String = ""
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptTrialBalance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
        btnQExport.Visible = MyBase.isQuickExportFlag
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()


        RadPageView1.SelectedPage = RadPageViewPage1
        cbgSrcCode.SelectedIndex = 0

        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        SetFiscalYear()
        LoadBlankGrid()
        SetDataBaseGrid()
        If isDataLoad Then
            txtCompany.arrValueMember = arrCompany
            txtEmployee.arrValueMember = arrEmployee
            txtSourceCode.arrValueMember = arrSourceCode
            txtLocationSegmant.arrValueMember = arrLocationSegment
            txtAccount.arrValueMember = arrAccount
        End If
        ReportFilters()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

        isRunDoubleClick = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoubleClickOnVC, clsFixedParameterCode.DoubleClickOnVC, Nothing)) = 1, True, False)

        If clsCommon.myLen(Me.Tag) > 0 Then
            txtFromDate.MinDate = New Date(2001, 4, 1)
            txtFromDate.MaxDate = New Date(3000, 12, 1)
            txtToDate.MinDate = txtFromDate.MinDate
            txtToDate.MaxDate = txtFromDate.MaxDate

            txtFiscalYear.Value = ""
            strSourceDoc = clsCommon.myCstr(Me.Tag)
            Dim strBreak As String()
            If strSourceDoc.Contains("#$#") Then
                strBreak = clsCommon.myCstr(strSourceDoc).Split(New String() {"#$#"}, StringSplitOptions.None)
                strSourceTransaction = strBreak(0)
                strSourceDoc = strBreak(1)

                If clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.frmPaymentProcess) = CompairStringResult.Equal OrElse
                    clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.frmPaymentProcessFarmer) = CompairStringResult.Equal Then

                    txtFromDate.MinDate = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select From_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No='" + strSourceDoc + "'")).AddDays(-30)
                    txtFromDate.Value = txtFromDate.MinDate

                    txtFromDate.MaxDate = txtFromDate.Value.AddDays(60)
                    txtToDate.Value = txtFromDate.MaxDate
                Else
                    txtFromDate.Tag = strBreak(2)
                    txtFromDate.Value = clsCommon.myCDate(txtFromDate.Tag).AddDays(-30)
                    txtToDate.Value = txtFromDate.Value.AddDays(60)
                End If
                cbgSrcCode.SelectedValue = "Basic Trial Balance"
                btnRefresh.PerformClick()
            End If
        Else
            funreset()
        End If

    End Sub

    Private Sub frmRptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then 'AndAlso MyBase.isModifyFlag
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPrintFlag Then
            RefreshData()
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub



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

#Region "Load Filters"
    Sub LoadLocatinSegment()
        cbgLocSeg.DataSource = clsLocation.GetLocationSegments()
        cbgLocSeg.ValueMember = "Code"
        cbgLocSeg.DisplayMember = "Name"
    End Sub

    Sub LoadAccounts()
        StrQry = "select Account_Code as Code,[Description] from TSPL_GL_ACCOUNTS"
        cbgAccount.DataSource = clsDBFuncationality.GetDataTable(StrQry)
        cbgAccount.ValueMember = "Code"
        cbgAccount.DisplayMember = "Description"
    End Sub

    Sub LoadVehicles()
        StrQry = "Select Segment_code as Code , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=2"
        cbgVehicle.DataSource = clsDBFuncationality.GetDataTable(StrQry)
        cbgVehicle.ValueMember = "Code"
        cbgVehicle.DisplayMember = "Description"
    End Sub

    Sub Loadmachines()
        StrQry = "Select Segment_code as COde , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=5"
        cbgmachine.DataSource = clsDBFuncationality.GetDataTable(StrQry)
        cbgmachine.ValueMember = "Code"
        cbgmachine.DisplayMember = "Description"
    End Sub

    Sub LoadDepartments()
        StrQry = "Select Segment_code as Code , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=3"
        cbgDept.DataSource = clsDBFuncationality.GetDataTable(StrQry)
        cbgDept.ValueMember = "Code"
        cbgDept.DisplayMember = "Description"
    End Sub

    Sub LoadEmployees()
        StrQry = "Select Segment_code as Code , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=4"
        cbgEmployee.DataSource = clsDBFuncationality.GetDataTable(StrQry)
        cbgEmployee.ValueMember = "Code"
        cbgEmployee.DisplayMember = "Description"
    End Sub

    Sub LoadVisi()
        StrQry = "Select Segment_code as COde , Description  from TSPL_GL_SEGMENT_CODE Where Seg_No=6"
        cbgVisi.DataSource = clsDBFuncationality.GetDataTable(StrQry)
        cbgVisi.ValueMember = "Code"
        cbgVisi.DisplayMember = "Description"
    End Sub

    Sub LoadSourceCode()
        StrQry = "Select Distinct Source_Code as Code, Source_Desc as Description from TSPL_JOURNAL_MASTER "
        cbgSourceCode.DataSource = clsDBFuncationality.GetDataTable(StrQry)
        cbgSourceCode.ValueMember = "Code"
        cbgSourceCode.DisplayMember = "Description"
    End Sub
#End Region

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
        Dim frmCRV As New frmCrystalReportViewer()
        If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
            If chkRollupWise.Checked Then
                frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceRP", "Trial Balance")
            Else

                frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalance", "Trial Balance")
            End If
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
            If chkRollupWise.Checked Then
                frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceSubLdgRP", "Trial Balance")
            Else
                frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceSubLdg", "Trial Balance")
            End If

        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
            If chkShowOPBal.Checked Then
                If chkRollupWise.Checked Then
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodRP", "Periodical Trial Balance")
                Else
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriod", "Periodical Trial Balance")
                End If
            Else
                If chkRollupWise.Checked Then
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodOPBalRP", "Periodical Trial Balance")
                Else
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodOPBal", "Periodical Trial Balance")
                End If
            End If
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
            frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceBasic", "Trial Balance")
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Account Wise") = CompairStringResult.Equal Then
            frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceAccountWise", "Account Wise Trial Balance")
        End If
        frmCRV = Nothing
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
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
    End Sub

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

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cbgSrcCode.SelectedIndexChanged
        chkShowOPBal.Visible = False
        chkShowNetBalance.Visible = False
        If clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
            chkShowOPBal.Visible = True
            chkMultipleRollup.Visible = True
            lblFromdate.Visible = True
            txtFromDate.Visible = True
            chkShowNetBalance.Visible = True
            lblToDate.Text = "To Date"
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
            chkMultipleRollup.Visible = False
            lblFromdate.Visible = True
            txtFromDate.Visible = True
            chkShowNetBalance.Visible = True
            lblToDate.Text = "To Date"
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Account Wise") = CompairStringResult.Equal Then
            chkMultipleRollup.Visible = False
            lblFromdate.Visible = True
            txtFromDate.Visible = True
            chkShowNetBalance.Visible = True
            lblToDate.Text = "To Date"
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
            chkMultipleRollup.Visible = True
            lblFromdate.Visible = False
            txtFromDate.Visible = False
            chkShowNetBalance.Visible = True
            lblToDate.Text = "As On Date"
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
            chkMultipleRollup.Visible = True
            lblFromdate.Visible = True
            txtFromDate.Visible = True
            chkShowNetBalance.Visible = True
            lblToDate.Text = "To Date"
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Location wise") = CompairStringResult.Equal Then
            chkShowOPBal.Visible = True
            chkMultipleRollup.Visible = False
            lblFromdate.Visible = True
            txtFromDate.Visible = True
            chkShowNetBalance.Visible = True
            lblToDate.Text = "To Date"
        Else
            chkMultipleRollup.Visible = False
        End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        gv1.EnableFiltering = True
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cbgSrcCode.Text)
        TemplateGridview = gv1
        RefreshData()
    End Sub

    Public Sub RefreshData()
        Try
            If Not clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cbgSrcCode.Text, "Location wise") <> CompairStringResult.Equal Then
                If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                    Throw New Exception("From date can't be greater than to date")
                End If
            End If

            Dim strAccCode As String
            If chkMultipleRollup.Checked = True Then
                strAccCode = " (case when len(ISNULL(GLACMappingAccountSeg.Mapped_Account_Code,''))>0 then GLACMappingAccountSeg.Account_Code else TSPL_GL_ACCOUNTS.Account_Seg_Code1 end) as Account_code"
                strAccCode += " ,(case when len(ISNULL(GLACMappingAccountSeg.Mapped_Account_Code,''))>0 then GLACMappingAccountSegDesc.Description else TSPL_GL_ACCOUNTS.Account_Seg_Desc1 end) as Account_Desc"
            Else
                strAccCode = "TSPL_JOURNAL_DETAILS.Account_code, TSPL_GL_ACCOUNTS.Description as Account_Desc"
            End If
            Dim arrLocation As IDictionary(Of String, String) = New Dictionary(Of String, String)
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim FinalQty As String = ""
            Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)
            If dtCompany IsNot Nothing AndAlso dtCompany.Rows.Count > 0 Then
                Dim FromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt")
                Dim ToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt")

                Dim FiscalDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
                If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
                    FiscalDate = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
                End If

                Dim BaseQry As String = " select TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code as MainGrpCode,TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as MainGrpDesc,TSPL_ACCOUNT_GROUPS.Account_Group_Code as GrpCode,TSPL_ACCOUNT_GROUPS.Account_Group_Desc as GrpName,TSPL_GL_ACCOUNTS.GL_Main_Code as RollupCode,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc as RollupName,"
                BaseQry += "TSPL_JOURNAL_DETAILS.Voucher_No,TSPL_JOURNAL_MASTER.Voucher_Date, "
                BaseQry += strAccCode + " ,"
                BaseQry += " case when  Amount>0 then Amount else 0 end as DrAmt,case when  Amount<0 then -1*Amount else 0 end as CrAmt,TSPL_ACCOUNT_GROUPS.Print_Order, TSPL_GL_ACCOUNTS.Rollup_Seq,TSPL_GL_ACCOUNTS.Account_Seg_Code7 as LocationCode,TSPL_LOCATION_MASTER.Location_Desc "
                BaseQry += " ,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code AS Sub_Group_Code,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS Sub_Group_Desc ,TSPL_FISCAL_YEAR_MASTER.Start_Date,TSPL_JOURNAL_MASTER.Transaction_Type "
                BaseQry += " from "
                BaseQry += " TSPL_JOURNAL_DETAILS " & _
                    " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code " & _
                    " left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code " & _
                    " left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code " & _
                    " left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code  " & _
                    " left outer join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code " & _
                    " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No "
                BaseQry += " left outer join  TSPL_Gl_Account_Mapping as GLACMappingAccountSeg on GLACMappingAccountSeg.Mapped_Account_Code= TSPL_JOURNAL_DETAILS.Account_Code"
                BaseQry += " left outer join  TSPL_GL_ACCOUNTS as GLACMappingAccountSegDesc on GLACMappingAccountSegDesc.Account_Code=GLACMappingAccountSeg.Account_Code"
                BaseQry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GL_ACCOUNTS.Account_Seg_Code7"
                BaseQry += " left outer join TSPL_FISCAL_YEAR_MASTER on convert(date, TSPL_FISCAL_YEAR_MASTER.Start_Date,106) <= '" + FiscalDate + "'  and  TSPL_FISCAL_YEAR_MASTER.End_Date >=  '" + FiscalDate + "'"
                If clsCommon.myLen(Me.Tag) > 0 Then
                    BaseQry += "inner join ( "
                    BaseQry += clsJournalEntryHeader.GetVoucherQuery(strSourceTransaction, strSourceDoc, txtFromDate.Tag)
                    BaseQry += " )SelectedVoucher on SelectedVoucher.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No"
                End If

                BaseQry += " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A' "
                If Not chkIndAS.Checked Then
                    BaseQry += " and TSPL_JOURNAL_MASTER.ind_as=0"
                End If
                If Not chkIncludeingAdjustmentEntry.Checked Then
                    BaseQry += "and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='A' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                End If
                If Not chkIncludeingClosingEntry.Checked Then
                    BaseQry += "and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_MASTER.Voucher_Desc not like 'Fiscal Year End%'  and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                End If
                If Not chkIncludeYearEndEntry.Checked Then ''UDL/12/06/18-000185 by balwinder on 13/06/2018
                    BaseQry += "and 2=case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_MASTER.Voucher_Desc like 'Fiscal Year End%' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                End If
                If txtACGrpType.arrValueMember IsNot Nothing AndAlso txtACGrpType.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_ACCOUNT_MAIN_GROUPS.Group_Type in (" + clsCommon.GetMulcallString(txtACGrpType.arrValueMember) + ")"
                End If
                If txtMainGroup.arrValueMember IsNot Nothing AndAlso txtMainGroup.arrValueMember.Count > 0 Then
                    BaseQry += " AND TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code in (" + clsCommon.GetMulcallString(txtMainGroup.arrValueMember) + ")"
                End If
                If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                    If chkMultipleRollup.Checked = True Then
                        BaseQry += " AND (case when len(ISNULL(GLACMappingAccountSeg.Mapped_Account_Code,''))>0 then GLACMappingAccountSeg.Account_Code else TSPL_GL_ACCOUNTS.Account_Seg_Code1 end)  in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")"
                    Else
                        BaseQry += " AND TSPL_JOURNAL_DETAILS.Account_code in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")"
                    End If
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
                Else
                    BaseQry += " and  TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in  (" + objCommonVar.strCurrUserLocationsSegment + ")"
                End If

                If chkExcludeTemplete.Checked Then
                    BaseQry += " and not exists (select 1 from  TSPL_Trial_GLAccounts_Excluded where  TSPL_Trial_GLAccounts_Excluded.Account_Code= TSPL_GL_ACCOUNTS.Account_Seg_Code1 )"
                End If

                Dim qryForOPBal As String = ""
                Dim qryForOther As String = ""
                Dim tempForSelecetedDatabase As String = ""
                Dim arrSelDB As List(Of String) = GetSelectedDatabase()
                Dim strdrAmtColumn As String = " sum(DrAmt) "
                Dim strCrAmtColumn As String = " sum(CrAmt) "
                If chkShowNetBalance.Checked Then
                    strdrAmtColumn = " case when sum(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end  "
                    strCrAmtColumn = " case when sum(CrAmt-DrAmt)>0 then sum(CrAmt-DrAmt) else 0 end "
                End If

                If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal OrElse clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
                    If chkRollupWise.Checked Then
                        tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date<= '" + ToDate + "' and 2 = case when TSPL_JOURNAL_MASTER.Transaction_Type='O' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                        If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
                            tempForSelecetedDatabase += " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "'"
                        End If
                        qryForOther = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName," + strdrAmtColumn + " as DrAmt," + strCrAmtColumn + " as CrAmt,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as RollupSeq from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode"
                        FinalQty = "select *,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FinlterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,case when  RollupName='Opening Balance' and DrAmt>0 then DrAmt else 0 end as OPDrAmt,case when  RollupName='Opening Balance' and CrAmt>0 then CrAmt else 0 end as OPCrAmt, Case When DrAmt>0 Then 0 else 1 END as [OrderDrCr]  from ( " + qryForOther + ") superFinal where abs(DrAmt-CrAmt)<>0 order by OrderDrCr, Print_Order,GrpCode, RollupSeq,RollupCode"
                    Else
                        tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date<= '" + ToDate + "' and 2 = case when TSPL_JOURNAL_MASTER.Transaction_Type='O' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "

                        '' richa agarwal 06/01/2017
                        If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") <> CompairStringResult.Equal Then
                            If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
                                tempForSelecetedDatabase += " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "'"
                            End If
                        End If

                        ''richa agarwal 17/06/2015
                        qryForOther = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,"

                        qryForOther += " Account_code as AccCode,(max(Account_Desc)) as AccName," + strdrAmtColumn + " as DrAmt," + strCrAmtColumn + " as CrAmt,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as RollupSeq "
                        qryForOther += " ,MAX(Sub_Group_Code) As Sub_Group_Code,MAX(Sub_Group_Desc ) AS Sub_Group_Desc ,MAX(MainGrpCode) AS MainGrpCode  ,MAX(MainGrpDesc ) AS MainGrpDesc "
                        qryForOther += " from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode,Account_code"
                        FinalQty = "select *,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FinlterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,case when  AccName='Opening Balance' and DrAmt>0 then DrAmt else 0 end as OPDrAmt,case when  AccName='Opening Balance' and CrAmt>0 then CrAmt else 0 end as OPCrAmt, Case When DrAmt>0 Then 0 else 1 END as [OrderDrCr] "
                        ''richa agarwal 11 july 2016 to show segment name and code
                        If clsCommon.CompairString(cbgSrcCode.Text, "Location wise") <> CompairStringResult.Equal AndAlso chkMultipleRollup.Checked = False Then
                            FinalQty += " , TSPL_GL_SEGMENT_CODE.Segment_code as [Segment Code],TSPL_GL_SEGMENT_CODE.Description "
                        End If
                        ''--------------------
                        FinalQty += " from (" + qryForOther + ") superFinal"
                        ''richa agarwal 11 july 2016 to show segment name and code
                        If clsCommon.CompairString(cbgSrcCode.Text, "Location wise") <> CompairStringResult.Equal AndAlso chkMultipleRollup.Checked = False Then
                            FinalQty += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =right(superFinal.AccCode,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' "
                        End If
                        ''--------------------
                        FinalQty += " where abs(DrAmt-CrAmt)<>0 order by Print_Order,GrpCode, RollupSeq,RollupCode " ''OrderDrCr, Print_Order,GrpCode, RollupSeq,RollupCode,AccCode"

                    End If
                ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
                    Dim strTempQry As String = "select Start_Date from TSPL_FISCAL_YEAR_MASTER where  convert(date, TSPL_FISCAL_YEAR_MASTER.Start_Date,106) <= '" + FiscalDate + "'  and  TSPL_FISCAL_YEAR_MASTER.End_Date >=  '" + FiscalDate + "' "
                    Dim dtfy As DataTable = clsDBFuncationality.GetDataTable(strTempQry)
                    If dtfy Is Nothing OrElse dtfy.Rows.Count <= 0 Then
                        Throw New Exception("Fiscal Year not exist with having date " + FiscalDate)
                    End If
                    Dim strPeriodDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(dtfy.Rows(0)("Start_Date"))), "dd/MMM/yyyy hh:mm tt")
                    If chkShowOPBal.Checked Then
                        If chkRollupWise.Checked Then
                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date< '" + strPeriodDate + "' "
                            qryForOPBal = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,case when sum(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end as DrAmt,case when sum(CrAmt-DrAmt)>0 then sum(CrAmt-DrAmt) else 0 end as CrAmt,1 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq from(" + tempForSelecetedDatabase + ") Final group by GrpCode,RollupCode "
                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + strPeriodDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date< '" + FromDate + "'"
                            Dim qryForPeriod As String = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,case when sum(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end as DrAmt,case when sum(CrAmt-DrAmt)>0 then sum(CrAmt-DrAmt) else 0 end as CrAmt,2 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode"
                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date<= '" + ToDate + "' and 2 = case when TSPL_JOURNAL_MASTER.Transaction_Type='O' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                            qryForOther = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName," + strdrAmtColumn + " as DrAmt," + strCrAmtColumn + " as CrAmt,3 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode"
                            FinalQty = "select GrpCode,GrpName,RollupCode,RollupName,case when (OPCr-OPDr)>0 then 'Cr' else 'Dr' end as OPSymbol,case when (OPCr-OPDr)>0 then (OPCr-OPDr) else OPDr-OPCr end as OPBal,(OPDr-OPCr) as OPActualBal,case when (PeriodCr-PeriodDr)>0 then 'Cr' else 'Dr' end as PeriodSymbol,case when (PeriodCr-PeriodDr)>0 then (PeriodCr-PeriodDr) else PeriodDr-PeriodCr end as PeriodBal,PeriodDr-PeriodCr as PeriodActualBal,NormalDr,NormalCr,case when (BalCr-BalDr)>0 then 'Cr' else 'Dr' end as BalSymbol,case when (BalCr-BalDr)>0 then (BalCr-BalDr) else BalDr-BalCr end as Bal,(BalDr-BalCr) as ActualBal,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FinlterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,case When BalDr>0 Then 0 else 1 End as [OrderDrCr] from ( select grpCode,MAX(GrpName) as GrpName,RollupCode,MAX(RollupName) as RollupName,SUM(DrAmt * case when Type=1 then 1 else 0 end) as OPDr,SUM(CrAmt * case when Type=1 then 1 else 0 end) as OPCr,SUM(DrAmt * case when Type in (2,1) then 1 else 0 end) as PeriodDr,SUM(CrAmt * case when Type in (2,1) then 1 else 0 end) as PeriodCr,SUM(DrAmt * case when Type=3 then 1 else 0 end) as NormalDr,SUM(CrAmt * case when Type=3 then 1 else 0 end) as NormalCr,SUM(DrAmt) as BalDr,SUM(CrAmt) as BalCr,MIN(Print_Order) as Print_Order, MAX(Rollup_Seq) as RollupSeq from ( " + qryForOPBal + " Union All " + qryForPeriod + " Union All " + qryForOther + " )superFinal Group By GrpCode,RollupCode)SuperDuperFinal order by  Print_Order, OrderDrCr, GrpCode, RollupSeq,RollupCode"
                        Else
                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date< '" + strPeriodDate + "' "
                            qryForOPBal = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,Account_code as AccCode,(max(Account_Desc)+' - '+Account_code) as AccName,case when sum(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end as DrAmt,case when sum(CrAmt-DrAmt)>0 then sum(CrAmt-DrAmt) else 0 end as CrAmt,1 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq "
                            qryForOPBal += " ,MAX(Sub_Group_Code) As Sub_Group_Code,MAX(Sub_Group_Desc) AS Sub_Group_Desc ,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc) AS MainGrpDesc "
                            qryForOPBal += "  from(" + tempForSelecetedDatabase + ") Final group by GrpCode,RollupCode,Account_code "

                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + strPeriodDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date< '" + FromDate + "'"
                            Dim qryForPeriod As String = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,Account_code as AccCode,(max(Account_Desc)) as AccName,case when sum(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end as DrAmt,case when sum(CrAmt-DrAmt)>0 then sum(CrAmt-DrAmt) else 0 end as CrAmt,2 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq "
                            qryForPeriod += " ,MAX(Sub_Group_Code) As Sub_Group_Code,MAX(Sub_Group_Desc) AS Sub_Group_Desc ,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc) AS MainGrpDesc "
                            qryForPeriod += " from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode,Account_code"

                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date<= '" + ToDate + "' and 2 = case when TSPL_JOURNAL_MASTER.Transaction_Type='O' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                            qryForOther = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,Account_code as AccCode,(max(Account_Desc)) as AccName," + strdrAmtColumn + " as DrAmt," + strCrAmtColumn + " as CrAmt,3 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq "
                            qryForOther += " ,MAX(Sub_Group_Code) As Sub_Group_Code,MAX(Sub_Group_Desc) AS Sub_Group_Desc ,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc) AS MainGrpDesc "
                            qryForOther += " from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode,Account_code"

                            FinalQty = "select GrpCode,GrpName,RollupCode,RollupName,AccCode,AccName,"
                            ''richa agarwal 11 july 2016 to show segment name and code
                            If clsCommon.CompairString(cbgSrcCode.Text, "Location wise") <> CompairStringResult.Equal AndAlso chkMultipleRollup.Checked = False Then
                                FinalQty += "TSPL_GL_SEGMENT_CODE.Segment_code as [Segment Code],TSPL_GL_SEGMENT_CODE.Description ,"
                            End If
                            ''--------------------

                            FinalQty += " case when (OPCr-OPDr)>0 then 'Cr' else 'Dr' end as OPSymbol,case when (OPCr-OPDr)>0 then (OPCr-OPDr) else OPDr-OPCr end as OPBal,(OPDr-OPCr) as OPActualBal,case when (PeriodCr-PeriodDr)>0 then 'Cr' else 'Dr' end as PeriodSymbol,case when (PeriodCr-PeriodDr)>0 then (PeriodCr-PeriodDr) else PeriodDr-PeriodCr end as PeriodBal,PeriodDr-PeriodCr as PeriodActualBal,NormalDr,NormalCr,case when (BalCr-BalDr)>0 then 'Cr' else 'Dr' end as BalSymbol,case when (BalCr-BalDr)>0 then (BalCr-BalDr) else BalDr-BalCr end as Bal,(BalDr-BalCr) as ActualBal,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FinlterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress, Print_Order, RollupSeq, case When BalDr>0 Then 0 else 1 End as [OrderDrCr] "
                            FinalQty += " ,Sub_Group_Code As Sub_Group_Code,Sub_Group_Desc AS Sub_Group_Desc ,MainGrpCode AS MainGrpCode  ,MainGrpDesc AS MainGrpDesc  "
                            FinalQty += "  from ( select grpCode,MAX(GrpName) as GrpName,RollupCode,MAX(RollupName) as RollupName,AccCode,MAX(AccName) as AccName,SUM(DrAmt * case when Type=1 then 1 else 0 end) as OPDr,SUM(CrAmt * case when Type=1 then 1 else 0 end) as OPCr,SUM(DrAmt * case when  Type in (2,1) then 1 else 0 end) as PeriodDr,SUM(CrAmt * case when Type in (2,1) then 1 else 0 end) as PeriodCr,SUM(DrAmt * case when Type=3 then 1 else 0 end) as NormalDr,SUM(CrAmt * case when Type=3 then 1 else 0 end) as NormalCr,SUM(DrAmt) as BalDr,SUM(CrAmt) as BalCr,MIN(Print_Order) as Print_Order, MAX(Rollup_Seq) as RollupSeq "
                            FinalQty += " ,MAX(Sub_Group_Code) As Sub_Group_Code,MAX(Sub_Group_Desc) AS Sub_Group_Desc ,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc) AS MainGrpDesc "
                            FinalQty += " from ( " + qryForOPBal + " Union All " + qryForPeriod + " Union All " + qryForOther + " " & Environment.NewLine
                            ''richa agarwal 25 March,2019 TEC/27/03/19-000458
                            If chkIncludeUnusedAccount.Checked = True Then
                                FinalQty += " union all " & Environment.NewLine & _
                                " select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,Account_code as AccCode,(max(Account_Desc)) as AccName, sum(DrAmt)  as DrAmt, sum(CrAmt)  as CrAmt,3 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq  ,MAX(Sub_Group_Code) As Sub_Group_Code,MAX(Sub_Group_Desc) AS Sub_Group_Desc ,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc) AS MainGrpDesc  from " & Environment.NewLine & _
                                " ( select  " & Environment.NewLine & _
                                " TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code as MainGrpCode,TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as MainGrpDesc,TSPL_ACCOUNT_GROUPS.Account_Group_Code as GrpCode,TSPL_ACCOUNT_GROUPS.Account_Group_Desc as GrpName,TSPL_GL_ACCOUNTS.GL_Main_Code as RollupCode,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc as RollupName,TSPL_JOURNAL_DETAILS.Voucher_No,TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_GL_ACCOUNTS.Account_code, TSPL_GL_ACCOUNTS.Description as Account_Desc , case when  Amount>0 then Amount else 0 end as DrAmt,case when  Amount<0 then -1*Amount else 0 end as CrAmt,TSPL_ACCOUNT_GROUPS.Print_Order, TSPL_GL_ACCOUNTS.Rollup_Seq,TSPL_GL_ACCOUNTS.Account_Seg_Code7 as LocationCode,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code AS Sub_Group_Code,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS Sub_Group_Desc " & Environment.NewLine & _
                                " from  " & Environment.NewLine & _
                                " TSPL_GL_ACCOUNTS   left outer join TSPL_JOURNAL_DETAILS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code  " & Environment.NewLine & _
                                "  left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code   " & Environment.NewLine & _
                                " left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code   " & Environment.NewLine & _
                                " left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code    " & Environment.NewLine & _
                                " left outer join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code   " & Environment.NewLine & _
                                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No   " & Environment.NewLine & _
                                " left outer join  TSPL_Gl_Account_Mapping as GLACMappingAccountSeg on GLACMappingAccountSeg.Mapped_Account_Code= TSPL_GL_ACCOUNTS.Account_Code  " & Environment.NewLine & _
                                " left outer join  TSPL_GL_ACCOUNTS as GLACMappingAccountSegDesc on GLACMappingAccountSegDesc.Account_Code=GLACMappingAccountSeg.Account_Code  " & Environment.NewLine & _
                                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GL_ACCOUNTS.Account_Seg_Code7  " & Environment.NewLine & _
                                " where 2 = 2  " & Environment.NewLine & _
                                " and not exists(select 1 from  TSPL_JOURNAL_DETAILS where  TSPL_JOURNAL_DETAILS.Account_Code= TSPL_GL_ACCOUNTS.Account_Code   )   " & Environment.NewLine & _
                                " )   Final group by GrpCode,RollupCode,Account_code "
                            End If
                            ''---------

                            FinalQty += " )superFinal Group By GrpCode,RollupCode,AccCode)SuperDuperFinal"
                            ''richa agarwal 11 july 2016 to show segment name and code
                            If clsCommon.CompairString(cbgSrcCode.Text, "Location wise") <> CompairStringResult.Equal AndAlso chkMultipleRollup.Checked = False Then
                                FinalQty += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =right(SuperDuperFinal.AccCode,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' "
                            End If
                            ''--------------------
                            FinalQty += " order by Print_Order, OrderDrCr,GrpCode, RollupSeq, RollupCode,AccCode"
                        End If
                    Else
                        If chkRollupWise.Checked Then
                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + strPeriodDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date< '" + FromDate + "'"
                            Dim qryForPeriod As String = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,case when sum(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end as DrAmt,case when sum(CrAmt-DrAmt)>0 then sum(CrAmt-DrAmt) else 0 end as CrAmt,2 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode"

                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date<= '" + ToDate + "' and 2 = case when TSPL_JOURNAL_MASTER.Transaction_Type='O' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                            qryForOther = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName," + strdrAmtColumn + " as DrAmt," + strCrAmtColumn + " as CrAmt,3 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode"

                            FinalQty = "select GrpCode,GrpName,RollupCode,RollupName,case when (OPCr-OPDr)>0 then 'Cr' else 'Dr' end as OPSymbol,case when (OPCr-OPDr)>0 then (OPCr-OPDr) else OPDr-OPCr end as OPBal,(OPDr-OPCr) as OPActualBal,case when (PeriodCr-PeriodDr)>0 then 'Cr' else 'Dr' end as PeriodSymbol,case when (PeriodCr-PeriodDr)>0 then (PeriodCr-PeriodDr) else PeriodDr-PeriodCr end as PeriodBal,PeriodDr-PeriodCr as PeriodActualBal,NormalDr,NormalCr,case when (BalCr-BalDr)>0 then 'Cr' else 'Dr' end as BalSymbol,case when (BalCr-BalDr)>0 then (BalCr-BalDr) else BalDr-BalCr end as Bal,(BalDr-BalCr) as ActualBal,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FinlterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,case When BalDr>0 Then 0 else 1 End as [OrderDrCr] from ( select grpCode,MAX(GrpName) as GrpName,RollupCode,MAX(RollupName) as RollupName,SUM(DrAmt * case when Type=1 then 1 else 0 end) as OPDr,SUM(CrAmt * case when Type=1 then 1 else 0 end) as OPCr,SUM(DrAmt * case when Type in (2,1) then 1 else 0 end) as PeriodDr,SUM(CrAmt * case when Type in (2,1) then 1 else 0 end) as PeriodCr,SUM(DrAmt * case when Type=3 then 1 else 0 end) as NormalDr,SUM(CrAmt * case when Type=3 then 1 else 0 end) as NormalCr,SUM(DrAmt) as BalDr,SUM(CrAmt) as BalCr,MIN(Print_Order) as Print_Order, MAX(Rollup_Seq) as RollupSeq from ( " + qryForPeriod + " Union All " + qryForOther + " )superFinal Group By GrpCode,RollupCode)SuperDuperFinal order by Print_Order, OrderDrCr,GrpCode, RollupSeq,RollupCode"
                        Else
                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + strPeriodDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date< '" + FromDate + "'"
                            Dim qryForPeriod As String = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,Account_code as AccCode,(max(Account_Desc)) as AccName,case when sum(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end as DrAmt,case when sum(CrAmt-DrAmt)>0 then sum(CrAmt-DrAmt) else 0 end as CrAmt,2 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as Rollup_Seq from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode,Account_code"

                            tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date<= '" + ToDate + "' and 2 = case when TSPL_JOURNAL_MASTER.Transaction_Type='O' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                            qryForOther = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,RollupCode,(max(RollupName)) as RollupName,Account_code as AccCode,(max(Account_Desc)) as AccName," + strdrAmtColumn + " as DrAmt," + strCrAmtColumn + " as CrAmt,3 as Type,min(Print_Order) as Print_Order, MAX(Rollup_Seq) as RollupSeq from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,RollupCode,Account_code"

                            FinalQty = "select GrpCode,GrpName,RollupCode,RollupName,AccCode,AccName,case when (OPCr-OPDr)>0 then 'Cr' else 'Dr' end as OPSymbol,case when (OPCr-OPDr)>0 then (OPCr-OPDr) else OPDr-OPCr end as OPBal,(OPDr-OPCr) as OPActualBal,case when (PeriodCr-PeriodDr)>0 then 'Cr' else 'Dr' end as PeriodSymbol,case when (PeriodCr-PeriodDr)>0 then (PeriodCr-PeriodDr) else PeriodDr-PeriodCr end as PeriodBal,PeriodDr-PeriodCr as PeriodActualBal,NormalDr,NormalCr,case when (BalCr-BalDr)>0 then 'Cr' else 'Dr' end as BalSymbol,case when (BalCr-BalDr)>0 then (BalCr-BalDr) else BalDr-BalCr end as Bal,(BalDr-BalCr) as ActualBal,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FinlterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress, Print_Order, case When BalDr>0 Then 0 else 1 End as [OrderDrCr] from ( select grpCode,MAX(GrpName) as GrpName,RollupCode,MAX(RollupName) as RollupName,AccCode,MAX(AccName) as AccName,SUM(DrAmt * case when Type=1 then 1 else 0 end) as OPDr,SUM(CrAmt * case when Type=1 then 1 else 0 end) as OPCr,SUM(DrAmt * case when Type in (2,1) then 1 else 0 end) as PeriodDr,SUM(CrAmt * case when Type in (2,1) then 1 else 0 end) as PeriodCr,SUM(DrAmt * case when Type=3 then 1 else 0 end) as NormalDr,SUM(CrAmt * case when Type=3 then 1 else 0 end) as NormalCr,SUM(DrAmt) as BalDr,SUM(CrAmt) as BalCr, MAX(Print_Order ) as Print_order, MAX(Rollup_Seq) as RollupSeq from ( " + qryForPeriod + " Union All " + qryForOther + " )superFinal Group By GrpCode,RollupCode,AccCode)SuperDuperFinal order by Print_Order, OrderDrCr, GrpCode, RollupSeq,RollupCode,AccCode"
                        End If
                    End If
                ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
                    tempForSelecetedDatabase = "" + BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date<= '" + ToDate + "' and 2 = case when TSPL_JOURNAL_MASTER.Transaction_Type='O' and TSPL_JOURNAL_MASTER.Voucher_Date >= TSPL_FISCAL_YEAR_MASTER.Start_Date then 3 else 2 end "
                    qryForOther = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,Account_code as AccCode,(max(Account_Desc)) as AccName," + strdrAmtColumn + " as DrAmt," + strCrAmtColumn + " as CrAmt "
                    qryForOther += " ,MAX(Sub_Group_Code) AS Sub_Group_Code ,MAX(Sub_Group_Desc) AS Sub_Group_Desc,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc ) AS MainGrpDesc  "
                    qryForOther += " from(" + tempForSelecetedDatabase + " ) Final group by GrpCode,Account_code" & Environment.NewLine

                    ''richa agarwal 25 March,2019 TEC/27/03/19-000458
                    If chkIncludeUnusedAccount.Checked = True Then
                        qryForOther += " union all " & Environment.NewLine & _
                        " select GrpCode,(max(isnull(GrpName,''))) as GrpName,Account_code as AccCode,(max(Account_Desc)) as AccName, sum(DrAmt)  as DrAmt, sum(CrAmt)  as CrAmt,MAX(Sub_Group_Code) As Sub_Group_Code,MAX(Sub_Group_Desc) AS Sub_Group_Desc ,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc) AS MainGrpDesc  from " & Environment.NewLine & _
                        " ( select  " & Environment.NewLine & _
                        " TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code as MainGrpCode,TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as MainGrpDesc,TSPL_ACCOUNT_GROUPS.Account_Group_Code as GrpCode,TSPL_ACCOUNT_GROUPS.Account_Group_Desc as GrpName,TSPL_GL_ACCOUNTS.GL_Main_Code as RollupCode,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc as RollupName,TSPL_JOURNAL_DETAILS.Voucher_No,TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_GL_ACCOUNTS.Account_code, TSPL_GL_ACCOUNTS.Description as Account_Desc , case when  Amount>0 then Amount else 0 end as DrAmt,case when  Amount<0 then -1*Amount else 0 end as CrAmt,TSPL_ACCOUNT_GROUPS.Print_Order, TSPL_GL_ACCOUNTS.Rollup_Seq,TSPL_GL_ACCOUNTS.Account_Seg_Code7 as LocationCode,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code AS Sub_Group_Code,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS Sub_Group_Desc " & Environment.NewLine & _
                        " from  " & Environment.NewLine & _
                        " TSPL_GL_ACCOUNTS   left outer join TSPL_JOURNAL_DETAILS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code  " & Environment.NewLine & _
                        "  left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code   " & Environment.NewLine & _
                        " left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code   " & Environment.NewLine & _
                        " left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code    " & Environment.NewLine & _
                        " left outer join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code   " & Environment.NewLine & _
                        " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No   " & Environment.NewLine & _
                        " left outer join  TSPL_Gl_Account_Mapping as GLACMappingAccountSeg on GLACMappingAccountSeg.Mapped_Account_Code= TSPL_GL_ACCOUNTS.Account_Code  " & Environment.NewLine & _
                        " left outer join  TSPL_GL_ACCOUNTS as GLACMappingAccountSegDesc on GLACMappingAccountSegDesc.Account_Code=GLACMappingAccountSeg.Account_Code  " & Environment.NewLine & _
                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GL_ACCOUNTS.Account_Seg_Code7  " & Environment.NewLine & _
                        " where 2 = 2  " & Environment.NewLine & _
                        " and not exists(select 1 from  TSPL_JOURNAL_DETAILS where  TSPL_JOURNAL_DETAILS.Account_Code= TSPL_GL_ACCOUNTS.Account_Code   )   " & Environment.NewLine & _
                        " )   Final group by GrpCode,Account_code "
                    End If
                    ''---------

                    FinalQty = "select *,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FinlterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,case when DrAmt >0 Then 0 else 1 End as [OrderDrCr] "
                    ''richa agarwal 11 july 2016 to show segment name and code
                    If clsCommon.CompairString(cbgSrcCode.Text, "Location wise") <> CompairStringResult.Equal Then
                        FinalQty += " , TSPL_GL_SEGMENT_CODE.Segment_code as [Segment Code],TSPL_GL_SEGMENT_CODE.Description "
                    End If
                    ''--------------------

                    FinalQty += " from (" + qryForOther + ") superFinal"

                    ''richa agarwal 11 july 2016 to show segment name and code
                    If clsCommon.CompairString(cbgSrcCode.Text, "Location wise") <> CompairStringResult.Equal Then
                        FinalQty += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =right(superFinal.AccCode,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' "
                    End If
                    ''--------------------
                    FinalQty += "  order by OrderDrCr, AccCode"
                ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Account Wise") = CompairStringResult.Equal Then
                    qryForOther = "select GrpCode,(max(isnull(GrpName,''))) as GrpName,MAX(Sub_Group_Code) AS Sub_Group_Code ,MAX(Sub_Group_Desc) AS Sub_Group_Desc,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc ) AS MainGrpDesc,Account_code as AccCode,(max(Account_Desc)) as AccName "
                    If chkShowNetBalance.Checked Then
                        qryForOther += ",case when sum( (DrAmt-CrAmt) * case when Final.Voucher_Date < '" + FromDate + "' then 1 else 0 end)>0 then sum( (DrAmt-CrAmt) * case when Final.Voucher_Date < '" + FromDate + "' then 1 else 0 end) else 0 end  as DrAmtOP, " + _
                       "case when sum((CrAmt-DrAmt) * case when Final.Voucher_Date < '" + FromDate + "' then 1 else 0 end)>0 then sum((CrAmt-DrAmt) * case when Final.Voucher_Date < '" + FromDate + "' then 1 else 0 end) else 0 end  as CrAmtOP," + _
                       "case when sum((DrAmt-CrAmt) * case when Final.Voucher_Date >= '" + FromDate + "' and Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end then 1 else 0 end)>0 then sum((DrAmt-CrAmt) * case when Final.Voucher_Date >= '" + FromDate + "' and Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end then 1 else 0 end) else 0 end  as DrAmt, " + _
                       "case when sum((CrAmt-DrAmt) * case when Final.Voucher_Date >= '" + FromDate + "' and Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end then 1 else 0 end)>0 then sum((CrAmt-DrAmt) * case when Final.Voucher_Date >= '" + FromDate + "' and Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end then 1 else 0 end) else 0 end  as CrAmt, " + _
                       "case when sum((DrAmt-CrAmt) * case when Final.Voucher_Date <= '" + ToDate + "' then 1 else 0 end)>0 then sum((DrAmt-CrAmt) * case when Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end  then 1 else 0 end) else 0 end as DrAmtCL, " + _
                       "case when sum((CrAmt-DrAmt) * case when Final.Voucher_Date <= '" + ToDate + "' then 1 else 0 end)>0 then sum((CrAmt-DrAmt) * case when Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end  then 1 else 0 end) else 0 end as CrAmtCL "
                    Else
                        qryForOther += ",sum( DrAmt * case when Final.Voucher_Date < '" + FromDate + "' then 1 else 0 end)  as DrAmtOP, " + _
                        "sum(CrAmt * case when Final.Voucher_Date < '" + FromDate + "' then 1 else 0 end)  as CrAmtOP," + _
                        "sum(DrAmt * case when Final.Voucher_Date >= '" + FromDate + "' and Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end then 1 else 0 end)  as DrAmt, " + _
                        "sum(CrAmt * case when Final.Voucher_Date >= '" + FromDate + "' and Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end then 1 else 0 end)  as CrAmt, " + _
                        "sum(DrAmt * case when Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end then 1 else 0 end)  as DrAmtCL, " + _
                        "sum(CrAmt * case when Final.Voucher_Date <= '" + ToDate + "' and 2 = case when Final.Transaction_Type='O' and Final.Voucher_Date >= Final.Start_Date then 3 else 2 end then 1 else 0 end)  as CrAmtCL "
                    End If
                    qryForOther += " from(" + BaseQry + " ) Final group by GrpCode,Account_code"
                    FinalQty = "select *,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FinlterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,case when DrAmt >0 Then 0 else 1 End as [OrderDrCr] "
                    FinalQty += " , TSPL_GL_SEGMENT_CODE.Segment_code as [Segment Code],TSPL_GL_SEGMENT_CODE.Description "
                    FinalQty += " from (" + qryForOther + ") superFinal"
                    FinalQty += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code =right(superFinal.AccCode,3) and TSPL_GL_SEGMENT_CODE.Seg_No ='7' "
                    FinalQty += "  order by OrderDrCr, AccCode"
                ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Location wise") = CompairStringResult.Equal Then
                    tempForSelecetedDatabase = BaseQry + " and TSPL_JOURNAL_MASTER.Voucher_Date<= '" + ToDate + "'"
                    'If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
                    '    tempForSelecetedDatabase += " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "'"
                    'End If
                    If Not chkShowOPBal.Checked Then
                        tempForSelecetedDatabase += " and TSPL_JOURNAL_MASTER.Voucher_Date>= '" + FromDate + "'"
                    End If
                    qryForOther = " select RollupCode,MAX(RollupName) as RollupName,LocationCode,MAX(Location_Desc) as Location_Desc, SUM(DrAmt-CrAmt) as Amt "
                    qryForOther += " ,MAX(Sub_Group_Code) AS Sub_Group_Code,MAX(Sub_Group_Desc) AS Sub_Group_Desc,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc) AS MainGrpDesc "
                    qryForOther += " from ( " + tempForSelecetedDatabase + "  )xxx group by RollupCode,LocationCode"
                    Dim qry As String = "select LocationCode,max(isnull(Location_Desc,'')) as Location_Desc from (" + qryForOther + ")xxxxx group by LocationCode"


                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("No Data found")
                    End If
                    Dim strPVColumns As String = ""
                    Dim strPVColumnsTotal As String = ""
                    For Each dr As DataRow In dt.Rows
                        arrLocation.Add(clsCommon.myCstr(dr("LocationCode")), IIf(clsCommon.myCstr(dr("Location_Desc")) = "", clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Description as Location_Desc from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code ='" & clsCommon.myCstr(dr("LocationCode")) & "' ")), clsCommon.myCstr(dr("Location_Desc"))))
                        If clsCommon.myLen(strPVColumns) > 0 Then
                            strPVColumns += ","
                            strPVColumnsTotal += "+"
                        End If
                        strPVColumns += "[" + clsCommon.myCstr(dr("LocationCode")) + "]"
                        strPVColumnsTotal += "isnull([" + clsCommon.myCstr(dr("LocationCode")) + "],0)"
                    Next

                    FinalQty = "select *,(" + strPVColumnsTotal + ") as Total from ( select * from( select RollupCode,MAX(RollupName) as RollupName,LocationCode, SUM(Amt) as Amt "
                    FinalQty += " ,MAX(Sub_Group_Code) AS Sub_Group_Code,MAX(Sub_Group_Desc) AS Sub_Group_Desc,MAX(MainGrpCode) AS MainGrpCode,MAX(MainGrpDesc) AS MainGrpDesc  "
                    FinalQty += " from (" + qryForOther + ")xxx group by RollupCode,LocationCode )as pv pivot (sum(amt) for locationcode in (" + strPVColumns + "))t ) xxxx "
                End If
                dt = clsDBFuncationality.GetDataTable(FinalQty)
                dt.Columns.Add("Logo_Img")
                dt.Columns.Add("Logo_Img2")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    dt.Rows(0)("Logo_Img") = dtCompany.Rows(0)("Logo_Img")
                    dt.Rows(0)("Logo_Img2") = dtCompany.Rows(0)("Logo_Img2")
                End If

                SetGridFormation(arrLocation)
                ReStoreGridLayout()
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Company Details Not found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation(ByVal arrLocation As Dictionary(Of String, String))
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt
        RadPageView1.SelectedPage = RadPageViewPage2
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
            If chkRollupWise.Checked Then
                gv1.Columns("GrpName").IsVisible = True
                gv1.Columns("GrpName").Width = 100
                gv1.Columns("GrpName").HeaderText = "Group"

                gv1.Columns("RollupCode").IsVisible = True
                gv1.Columns("RollupCode").Width = 100
                gv1.Columns("RollupCode").HeaderText = "Rollup Code"

                gv1.Columns("RollupName").IsVisible = True
                gv1.Columns("RollupName").Width = 300
                gv1.Columns("RollupName").HeaderText = "Rollup"

                gv1.Columns("DrAmt").IsVisible = True
                gv1.Columns("DrAmt").Width = 100
                gv1.Columns("DrAmt").HeaderText = "Debit"

                gv1.Columns("CrAmt").IsVisible = True
                gv1.Columns("CrAmt").Width = 100
                gv1.Columns("CrAmt").HeaderText = "Credit"


                gv1.MasterTemplate.ExpandAllGroups()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.AutoExpandGroups = True
                Dim summaryRowItem As New GridViewSummaryRowItem()

                Dim item1 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else
                gv1.Columns("Print_Order").HeaderText = " "
                gv1.Columns("RollupSeq").HeaderText = " "
                gv1.Columns("GrpName").HeaderText = " "

                gv1.Columns("RollupName").HeaderText = " "

                gv1.Columns("AccCode").IsVisible = True
                gv1.Columns("AccCode").Width = 100
                gv1.Columns("AccCode").HeaderText = "GL Code"

                gv1.Columns("AccName").IsVisible = True
                gv1.Columns("AccName").Width = 250
                gv1.Columns("AccName").HeaderText = "Particulars"

                ''richa agarwal 11 july 2016
                If chkMultipleRollup.Checked = False Then
                    gv1.Columns("Segment Code").IsVisible = True
                    gv1.Columns("Segment Code").Width = 100
                    gv1.Columns("Segment Code").HeaderText = "Segment Code"

                    gv1.Columns("Description").IsVisible = True
                    gv1.Columns("Description").Width = 300
                    gv1.Columns("Description").HeaderText = "Description"
                End If

                ''-------------------------

                gv1.Columns("DrAmt").IsVisible = True
                gv1.Columns("DrAmt").Width = 100
                gv1.Columns("DrAmt").HeaderText = "Debit"

                gv1.Columns("CrAmt").IsVisible = True
                gv1.Columns("CrAmt").Width = 100
                gv1.Columns("CrAmt").HeaderText = "Credit"

                ''richa agarwal 17/06/2015
                gv1.Columns("GrpCode").IsVisible = True
                gv1.Columns("GrpCode").Width = 100
                gv1.Columns("GrpCode").HeaderText = "Account Group Code"

                gv1.Columns("GrpName").IsVisible = True
                gv1.Columns("GrpName").Width = 100
                gv1.Columns("GrpName").HeaderText = "Account Group Name"

                gv1.Columns("MainGrpCode").IsVisible = True
                gv1.Columns("MainGrpCode").Width = 100
                gv1.Columns("MainGrpCode").HeaderText = "Main Group Code"

                gv1.Columns("MainGrpDesc").IsVisible = True
                gv1.Columns("MainGrpDesc").Width = 100
                gv1.Columns("MainGrpDesc").HeaderText = "Main Group Name"

                gv1.Columns("Sub_Group_Code").IsVisible = True
                gv1.Columns("Sub_Group_Code").Width = 100
                gv1.Columns("Sub_Group_Code").HeaderText = "Sub Group Code"

                gv1.Columns("Sub_Group_Desc").IsVisible = True
                gv1.Columns("Sub_Group_Desc").Width = 100
                gv1.Columns("Sub_Group_Desc").HeaderText = "Sub Group Name"

                gv1.Columns("RollupCode").IsVisible = True
                gv1.Columns("RollupCode").Width = 100
                gv1.Columns("RollupCode").HeaderText = "GL Main Code"

                gv1.Columns("RollupName").IsVisible = True
                gv1.Columns("RollupName").Width = 300
                gv1.Columns("RollupName").HeaderText = "GL Main Desc"
                ''---------------------------

                ''richa agarwal 22/05/2015 BM00000006837
                'gv1.GroupDescriptors.Add(New GridGroupByExpression("Print_Order as Print_Order format ""{0}: {1}"" Group By Print_Order"))
                'gv1.GroupDescriptors.Add(New GridGroupByExpression("GrpName as GrpName format ""{0}: {1}"" Group By GrpName"))
                'gv1.GroupDescriptors.Add(New GridGroupByExpression("RollupSeq as RollupSeq format ""{0}: {1}"" Group By RollupSeq"))
                'gv1.GroupDescriptors.Add(New GridGroupByExpression("RollupName as RollupName format ""{0}: {1}"" Group By RollupName"))
                ''---------------------
                gv1.MasterTemplate.ExpandAllGroups()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.AutoExpandGroups = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
            If chkRollupWise.Checked Then
                gv1.Columns("RollupCode").IsVisible = True
                gv1.Columns("RollupCode").Width = 100
                gv1.Columns("RollupCode").HeaderText = "RollupCode"

                gv1.Columns("RollupName").IsVisible = True
                gv1.Columns("RollupName").Width = 300
                gv1.Columns("RollupName").HeaderText = "Rollup"

                gv1.Columns("DrAmt").IsVisible = True
                gv1.Columns("DrAmt").Width = 100
                gv1.Columns("DrAmt").HeaderText = "Debit"

                gv1.Columns("CrAmt").IsVisible = True
                gv1.Columns("CrAmt").Width = 100
                gv1.Columns("CrAmt").HeaderText = "Credit"

                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.AutoExpandGroups = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else

                gv1.Columns("RollupName").HeaderText = " "

                gv1.Columns("AccCode").IsVisible = True
                gv1.Columns("AccCode").Width = 100
                gv1.Columns("AccCode").HeaderText = "Code"

                gv1.Columns("AccName").IsVisible = True
                gv1.Columns("AccName").Width = 600
                gv1.Columns("AccName").HeaderText = "Particulars"

                ''richa agarwal 11 july 2016
                If chkMultipleRollup.Checked = False Then
                    gv1.Columns("Segment Code").IsVisible = True
                    gv1.Columns("Segment Code").Width = 100
                    gv1.Columns("Segment Code").HeaderText = "Segment Code"

                    gv1.Columns("Description").IsVisible = True
                    gv1.Columns("Description").Width = 300
                    gv1.Columns("Description").HeaderText = "Description"
                End If
                ''-------------------------

                gv1.Columns("DrAmt").IsVisible = True
                gv1.Columns("DrAmt").Width = 100
                gv1.Columns("DrAmt").HeaderText = "Debit"

                gv1.Columns("CrAmt").IsVisible = True
                gv1.Columns("CrAmt").Width = 100
                gv1.Columns("CrAmt").HeaderText = "Credit"

                gv1.Columns("Sub_Group_Code").IsVisible = True
                gv1.Columns("Sub_Group_Code").Width = 100
                gv1.Columns("Sub_Group_Code").HeaderText = "Sub Group Code"

                gv1.Columns("Sub_Group_Desc").IsVisible = True
                gv1.Columns("Sub_Group_Desc").Width = 100
                gv1.Columns("Sub_Group_Desc").HeaderText = "Sub Group Name"

                gv1.Columns("MainGrpCode").IsVisible = True
                gv1.Columns("MainGrpCode").Width = 100
                gv1.Columns("MainGrpCode").HeaderText = "Main Group Code"

                gv1.Columns("MainGrpDesc").IsVisible = True
                gv1.Columns("MainGrpDesc").Width = 100
                gv1.Columns("MainGrpDesc").HeaderText = "Main Group Name"



                ''richa agarwal 22/05/2015
                '  gv1.GroupDescriptors.Add(New GridGroupByExpression("RollupName as RollupName format ""{0}: {1}"" Group By RollupName"))
                ''-----------------------------
                gv1.MasterTemplate.ExpandAllGroups()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.AutoExpandGroups = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
            If chkShowOPBal.Checked Then
                If chkRollupWise.Checked Then

                    gv1.Columns("GrpName").IsVisible = True
                    gv1.Columns("GrpName").Width = 100
                    gv1.Columns("GrpName").HeaderText = "Group"

                    gv1.Columns("RollupCode").IsVisible = True
                    gv1.Columns("RollupCode").Width = 100
                    gv1.Columns("RollupCode").HeaderText = "Rollup Code"

                    gv1.Columns("RollupName").IsVisible = True
                    gv1.Columns("RollupName").Width = 100
                    gv1.Columns("RollupName").HeaderText = "Rollup"

                    gv1.Columns("OPSymbol").IsVisible = True
                    gv1.Columns("OPSymbol").Width = 30
                    gv1.Columns("OPSymbol").HeaderText = "  "

                    gv1.Columns("OPBal").IsVisible = True
                    gv1.Columns("OPBal").Width = 100
                    gv1.Columns("OPBal").HeaderText = "Year Opening"

                    gv1.Columns("PeriodSymbol").IsVisible = True
                    gv1.Columns("PeriodSymbol").Width = 30
                    gv1.Columns("PeriodSymbol").HeaderText = " "

                    gv1.Columns("PeriodBal").IsVisible = True
                    gv1.Columns("PeriodBal").Width = 100
                    gv1.Columns("PeriodBal").HeaderText = "Period Opending"


                    gv1.Columns("NormalDr").IsVisible = True
                    gv1.Columns("NormalDr").Width = 100
                    gv1.Columns("NormalDr").HeaderText = "Debit"


                    gv1.Columns("NormalCr").IsVisible = True
                    gv1.Columns("NormalCr").Width = 100
                    gv1.Columns("NormalCr").HeaderText = "Credit"

                    gv1.Columns("BalSymbol").IsVisible = True
                    gv1.Columns("BalSymbol").Width = 30
                    gv1.Columns("BalSymbol").HeaderText = ""

                    gv1.Columns("Bal").IsVisible = True
                    gv1.Columns("Bal").Width = 100
                    gv1.Columns("Bal").HeaderText = "Closing"
                    ''richa agarwal 22/05/2015
                    ' gv1.GroupDescriptors.Add(New GridGroupByExpression("GrpName as GrpName format ""{0}: {1}"" Group By GrpName"))
                    ''-------------------
                    gv1.MasterTemplate.ExpandAllGroups()
                    gv1.ShowGroupPanel = False
                    gv1.MasterTemplate.AutoExpandGroups = True
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim item1 As New GridViewSummaryItem("NormalDr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                    Dim item2 As New GridViewSummaryItem("NormalCr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    Dim item3 As New GridViewSummaryItem("OPActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item3)
                    Dim item4 As New GridViewSummaryItem("PeriodActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item4)
                    Dim item5 As New GridViewSummaryItem("ActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item5)
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Else
                    gv1.Columns("GrpName").HeaderText = " "
                    gv1.Columns("RollupSeq").HeaderText = " "
                    gv1.Columns("RollupName").HeaderText = " "


                    gv1.Columns("AccCode").IsVisible = True
                    gv1.Columns("AccCode").Width = 100
                    gv1.Columns("AccCode").HeaderText = "Code"

                    gv1.Columns("AccName").IsVisible = True
                    gv1.Columns("AccName").Width = 300
                    gv1.Columns("AccName").HeaderText = "Particulars"

                    ''richa agarwal 11 july 2016
                    If chkMultipleRollup.Checked = False Then
                        gv1.Columns("Segment Code").IsVisible = True
                        gv1.Columns("Segment Code").Width = 100
                        gv1.Columns("Segment Code").HeaderText = "Segment Code"

                        gv1.Columns("Description").IsVisible = True
                        gv1.Columns("Description").Width = 300
                        gv1.Columns("Description").HeaderText = "Description"
                    End If
                    ''-------------------------

                    gv1.Columns("OPSymbol").IsVisible = True
                    gv1.Columns("OPSymbol").Width = 30
                    gv1.Columns("OPSymbol").HeaderText = "  "

                    gv1.Columns("OPBal").IsVisible = True
                    gv1.Columns("OPBal").Width = 100
                    gv1.Columns("OPBal").HeaderText = "Year Opening"

                    gv1.Columns("PeriodSymbol").IsVisible = True
                    gv1.Columns("PeriodSymbol").Width = 30
                    gv1.Columns("PeriodSymbol").HeaderText = " "

                    gv1.Columns("PeriodBal").IsVisible = True
                    gv1.Columns("PeriodBal").Width = 100
                    gv1.Columns("PeriodBal").HeaderText = "Period Opening"


                    gv1.Columns("NormalDr").IsVisible = True
                    gv1.Columns("NormalDr").Width = 100
                    gv1.Columns("NormalDr").HeaderText = "Debit"


                    gv1.Columns("NormalCr").IsVisible = True
                    gv1.Columns("NormalCr").Width = 100
                    gv1.Columns("NormalCr").HeaderText = "Credit"

                    gv1.Columns("BalSymbol").IsVisible = True
                    gv1.Columns("BalSymbol").Width = 30
                    gv1.Columns("BalSymbol").HeaderText = ""

                    gv1.Columns("Bal").IsVisible = True
                    gv1.Columns("Bal").Width = 100
                    gv1.Columns("Bal").HeaderText = "Closing"

                    'gv1.Columns("ActualBal").IsVisible = True
                    'gv1.Columns("ActualBal").Width = 100
                    'gv1.Columns("ActualBal").HeaderText = "Closing"

                    gv1.Columns("MainGrpCode").IsVisible = True
                    gv1.Columns("MainGrpCode").Width = 100
                    gv1.Columns("MainGrpCode").HeaderText = "Main Group Code"

                    gv1.Columns("MainGrpDesc").IsVisible = True
                    gv1.Columns("MainGrpDesc").Width = 100
                    gv1.Columns("MainGrpDesc").HeaderText = "Main Group Name"

                    gv1.Columns("Sub_Group_Code").IsVisible = True
                    gv1.Columns("Sub_Group_Code").Width = 100
                    gv1.Columns("Sub_Group_Code").HeaderText = "Sub Group Code"

                    gv1.Columns("Sub_Group_Desc").IsVisible = True
                    gv1.Columns("Sub_Group_Desc").Width = 100
                    gv1.Columns("Sub_Group_Desc").HeaderText = "Sub Group Name"

                    ''richa agarwal 22/05/2015 remove all groups from grid
                    'gv1.GroupDescriptors.Add(New GridGroupByExpression("Print_Order as Print_Order format ""{0}: {1}"" Group By Print_Order"))
                    'gv1.GroupDescriptors.Add(New GridGroupByExpression("GrpName as GrpName format ""{0}: {1}"" Group By GrpName"))
                    'gv1.GroupDescriptors.Add(New GridGroupByExpression("RollupSeq as RollupSeq format ""{0}: {1}"" Group By RollupSeq"))
                    'gv1.GroupDescriptors.Add(New GridGroupByExpression("RollupName as RollupName format ""{0}: {1}"" Group By RollupName"))
                    ''-----------------------
                    gv1.MasterTemplate.ExpandAllGroups()
                    gv1.ShowGroupPanel = False
                    gv1.MasterTemplate.AutoExpandGroups = True
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim item1 As New GridViewSummaryItem("NormalDr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                    Dim item2 As New GridViewSummaryItem("NormalCr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    Dim item3 As New GridViewSummaryItem("OPActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item3)
                    Dim item4 As New GridViewSummaryItem("PeriodActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item4)
                    Dim item5 As New GridViewSummaryItem("ActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item5)
                    'Dim item6 As New GridViewSummaryItem("PeriodBal", "{0:F2}", GridAggregateFunction.Sum)
                    'summaryRowItem.Add(item6)
                    'Dim item5 As New GridViewSummaryItem("PeriodSymbol", IIf(sum(PeriodActualBal) > 0, "Dr", "Cr"), GridAggregateFunction.Var)
                    'summaryRowItem.Add(item5)


                    Dim item7 As New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item7)
                    ''richa agarwal 17 Nov,2016
                    Dim TotalClosing As New GridViewSummaryItem()
                    TotalClosing.FormatString = "{0:F2}"
                    TotalClosing.Name = "Bal"
                    TotalClosing.AggregateExpression = "sum(PeriodActualBal)+sum(NormalDr)-sUM(NormalCr)"
                    summaryRowItem.Add(TotalClosing)

                    Dim TotalPeriodBal As New GridViewSummaryItem()
                    TotalPeriodBal.FormatString = "{0:F2}"
                    TotalPeriodBal.Name = "PeriodBal"
                    TotalPeriodBal.AggregateExpression = "sum(PeriodActualBal)"
                    summaryRowItem.Add(TotalPeriodBal)
                    ''-------------------------
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
            Else
                If chkRollupWise.Checked Then
                    gv1.Columns("GrpName").IsVisible = True
                    gv1.Columns("GrpName").Width = 100
                    gv1.Columns("GrpName").HeaderText = "Group"

                    gv1.Columns("RollupCode").IsVisible = True
                    gv1.Columns("RollupCode").Width = 100
                    gv1.Columns("RollupCode").HeaderText = "Rollup Code"

                    gv1.Columns("RollupName").IsVisible = True
                    gv1.Columns("RollupName").Width = 100
                    gv1.Columns("RollupName").HeaderText = "Rollup"

                    gv1.Columns("PeriodSymbol").IsVisible = True
                    gv1.Columns("PeriodSymbol").Width = 30
                    gv1.Columns("PeriodSymbol").HeaderText = " "

                    gv1.Columns("PeriodBal").IsVisible = True
                    gv1.Columns("PeriodBal").Width = 100
                    gv1.Columns("PeriodBal").HeaderText = "Period Opending"

                    gv1.Columns("NormalDr").IsVisible = True
                    gv1.Columns("NormalDr").Width = 100
                    gv1.Columns("NormalDr").HeaderText = "Debit"

                    gv1.Columns("NormalCr").IsVisible = True
                    gv1.Columns("NormalCr").Width = 100
                    gv1.Columns("NormalCr").HeaderText = "Credit"

                    gv1.Columns("BalSymbol").IsVisible = True
                    gv1.Columns("BalSymbol").Width = 30
                    gv1.Columns("BalSymbol").HeaderText = ""

                    gv1.Columns("Bal").IsVisible = True
                    gv1.Columns("Bal").Width = 100
                    gv1.Columns("Bal").HeaderText = "Closing"
                    ''richa agarwal 22/05/2015
                    'gv1.GroupDescriptors.Add(New GridGroupByExpression("GrpName as GrpName format ""{0}: {1}"" Group By GrpName"))
                    ''------------------
                    gv1.MasterTemplate.ExpandAllGroups()
                    gv1.ShowGroupPanel = False
                    gv1.MasterTemplate.AutoExpandGroups = True
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim item1 As New GridViewSummaryItem("NormalDr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                    Dim item2 As New GridViewSummaryItem("NormalCr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    Dim item4 As New GridViewSummaryItem("PeriodActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item4)
                    Dim item5 As New GridViewSummaryItem("ActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item5)
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Else
                    gv1.Columns("GrpName").HeaderText = " "

                    gv1.Columns("RollupName").HeaderText = " "

                    gv1.Columns("AccCode").IsVisible = True
                    gv1.Columns("AccCode").Width = 100
                    gv1.Columns("AccCode").HeaderText = "Code"

                    gv1.Columns("AccName").IsVisible = True
                    gv1.Columns("AccName").Width = 300
                    gv1.Columns("AccName").HeaderText = "Particulars"

                    gv1.Columns("PeriodSymbol").IsVisible = True
                    gv1.Columns("PeriodSymbol").Width = 30
                    gv1.Columns("PeriodSymbol").HeaderText = " "

                    gv1.Columns("PeriodBal").IsVisible = True
                    gv1.Columns("PeriodBal").Width = 100
                    gv1.Columns("PeriodBal").HeaderText = "Period Opending"

                    gv1.Columns("NormalDr").IsVisible = True
                    gv1.Columns("NormalDr").Width = 100
                    gv1.Columns("NormalDr").HeaderText = "Debit"

                    gv1.Columns("NormalCr").IsVisible = True
                    gv1.Columns("NormalCr").Width = 100
                    gv1.Columns("NormalCr").HeaderText = "Credit"

                    gv1.Columns("BalSymbol").IsVisible = True
                    gv1.Columns("BalSymbol").Width = 30
                    gv1.Columns("BalSymbol").HeaderText = ""

                    gv1.Columns("Bal").IsVisible = True
                    gv1.Columns("Bal").Width = 100
                    gv1.Columns("Bal").HeaderText = "Closing"
                    ''richa agarwal 22/05/2015
                    'gv1.GroupDescriptors.Add(New GridGroupByExpression("Print_Order as Print_Order format ""{0}: {1}"" Group By Print_Order"))
                    'gv1.GroupDescriptors.Add(New GridGroupByExpression("GrpName as GrpName format ""{0}: {1}"" Group By GrpName"))
                    'gv1.GroupDescriptors.Add(New GridGroupByExpression("RollupName as RollupName format ""{0}: {1}"" Group By RollupName"))
                    ''--------------------------
                    gv1.MasterTemplate.ExpandAllGroups()
                    gv1.ShowGroupPanel = False
                    gv1.MasterTemplate.AutoExpandGroups = True
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim item1 As New GridViewSummaryItem("NormalDr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                    Dim item2 As New GridViewSummaryItem("NormalCr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    Dim item4 As New GridViewSummaryItem("PeriodActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item4)
                    Dim item5 As New GridViewSummaryItem("ActualBal", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item5)

                    '' done by richa agarwal 04 Apr, 2018 only in case f period trial balance
                    If chkShowOPBal.Checked = False Then
                        Dim TotalClosing As New GridViewSummaryItem()
                        TotalClosing.FormatString = "{0:F2}"
                        TotalClosing.Name = "Bal"
                        TotalClosing.AggregateExpression = "sum(NormalDr)-sum(NormalCr)"
                        summaryRowItem.Add(TotalClosing)
                    End If
                    ''-----------
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
            End If
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
            gv1.Columns("AccCode").IsVisible = True
            gv1.Columns("AccCode").Width = 100
            gv1.Columns("AccCode").HeaderText = "Code"

            gv1.Columns("AccName").IsVisible = True
            gv1.Columns("AccName").Width = 600
            gv1.Columns("AccName").HeaderText = "Particulars"

            ''richa agarwal 11 july 2016
            gv1.Columns("Segment Code").IsVisible = True
            gv1.Columns("Segment Code").Width = 100
            gv1.Columns("Segment Code").HeaderText = "Segment Code"

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").Width = 300
            gv1.Columns("Description").HeaderText = "Description"

            ''-------------------------

            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").Width = 100
            gv1.Columns("DrAmt").HeaderText = "Debit"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").Width = 100
            gv1.Columns("CrAmt").HeaderText = "Credit"

            gv1.Columns("MainGrpCode").IsVisible = True
            gv1.Columns("MainGrpCode").Width = 100
            gv1.Columns("MainGrpCode").HeaderText = "Main Group Code"

            gv1.Columns("MainGrpDesc").IsVisible = True
            gv1.Columns("MainGrpDesc").Width = 100
            gv1.Columns("MainGrpDesc").HeaderText = "Main Group Name"

            gv1.Columns("Sub_Group_Code").IsVisible = True
            gv1.Columns("Sub_Group_Code").Width = 100
            gv1.Columns("Sub_Group_Code").HeaderText = "Sub Group Code"

            gv1.Columns("Sub_Group_Desc").IsVisible = True
            gv1.Columns("Sub_Group_Desc").Width = 100
            gv1.Columns("Sub_Group_Desc").HeaderText = "Sub Group Name"

            gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Account Wise") = CompairStringResult.Equal Then
            gv1.Columns("AccCode").IsVisible = True
            gv1.Columns("AccCode").Width = 100
            gv1.Columns("AccCode").HeaderText = "Code"

            gv1.Columns("AccName").IsVisible = True
            gv1.Columns("AccName").Width = 600
            gv1.Columns("AccName").HeaderText = "Particulars"


            gv1.Columns("Segment Code").IsVisible = True
            gv1.Columns("Segment Code").Width = 100
            gv1.Columns("Segment Code").HeaderText = "Segment Code"

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").Width = 300
            gv1.Columns("Description").HeaderText = "Description"

            gv1.Columns("DrAmtOP").IsVisible = True
            gv1.Columns("DrAmtOP").Width = 100
            gv1.Columns("DrAmtOP").HeaderText = "Opening Debit"

            gv1.Columns("CrAmtOP").IsVisible = True
            gv1.Columns("CrAmtOP").Width = 100
            gv1.Columns("CrAmtOP").HeaderText = "Opening Credit"

            gv1.Columns("DrAmt").IsVisible = True
            gv1.Columns("DrAmt").Width = 100
            gv1.Columns("DrAmt").HeaderText = "Debit"

            gv1.Columns("CrAmt").IsVisible = True
            gv1.Columns("CrAmt").Width = 100
            gv1.Columns("CrAmt").HeaderText = "Credit"

            gv1.Columns("DrAmtCL").IsVisible = True
            gv1.Columns("DrAmtCL").Width = 100
            gv1.Columns("DrAmtCL").HeaderText = "Closing Debit"

            gv1.Columns("CrAmtCL").IsVisible = True
            gv1.Columns("CrAmtCL").Width = 100
            gv1.Columns("CrAmtCL").HeaderText = "Closing Credit"

            gv1.Columns("MainGrpCode").IsVisible = True
            gv1.Columns("MainGrpCode").Width = 100
            gv1.Columns("MainGrpCode").HeaderText = "Main Group Code"

            gv1.Columns("MainGrpDesc").IsVisible = True
            gv1.Columns("MainGrpDesc").Width = 100
            gv1.Columns("MainGrpDesc").HeaderText = "Main Group Name"

            gv1.Columns("Sub_Group_Code").IsVisible = True
            gv1.Columns("Sub_Group_Code").Width = 100
            gv1.Columns("Sub_Group_Code").HeaderText = "Sub Group Code"

            gv1.Columns("Sub_Group_Desc").IsVisible = True
            gv1.Columns("Sub_Group_Desc").Width = 100
            gv1.Columns("Sub_Group_Desc").HeaderText = "Sub Group Name"

            gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("DrAmtOP", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("CrAmtOP", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("DrAmtCL", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("CrAmtCL", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Location wise") = CompairStringResult.Equal Then
            gv1.Columns("RollupCode").IsVisible = True
            gv1.Columns("RollupCode").Width = 100
            gv1.Columns("RollupCode").HeaderText = "GL Main Code"

            gv1.Columns("RollupName").IsVisible = True
            gv1.Columns("RollupName").Width = 100
            gv1.Columns("RollupName").HeaderText = "Description"

            gv1.Columns("Total").IsVisible = True
            gv1.Columns("Total").Width = 100
            gv1.Columns("Total").HeaderText = "Total"

            gv1.Columns("MainGrpCode").IsVisible = True
            gv1.Columns("MainGrpCode").Width = 100
            gv1.Columns("MainGrpCode").HeaderText = "Main Group Code"

            gv1.Columns("MainGrpDesc").IsVisible = True
            gv1.Columns("MainGrpDesc").Width = 100
            gv1.Columns("MainGrpDesc").HeaderText = "Main Group Name"

            gv1.Columns("Sub_Group_Code").IsVisible = True
            gv1.Columns("Sub_Group_Code").Width = 100
            gv1.Columns("Sub_Group_Code").HeaderText = "Sub Group Code"

            gv1.Columns("Sub_Group_Desc").IsVisible = True
            gv1.Columns("Sub_Group_Desc").Width = 100
            gv1.Columns("Sub_Group_Desc").HeaderText = "Sub Group Name"



            gv1.Columns("Logo_Img").IsVisible = False
            gv1.Columns("Logo_Img").HeaderText = "Logo 1"

            gv1.Columns("Logo_Img2").IsVisible = False
            gv1.Columns("Logo_Img2").HeaderText = "Log0 2"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            For Each key As String In arrLocation.Keys
                gv1.Columns(key).IsVisible = True
                gv1.Columns(key).Width = 100
                gv1.Columns(key).HeaderText = arrLocation(key)

                Dim item1 As New GridViewSummaryItem(key, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            Dim item2 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
        EnableDisableControls(False)

    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtMainGroup.Enabled = Val
        txtACGrpType.Enabled = Val
        chkIncludeingAdjustmentEntry.Enabled = Val
        chkIncludeingClosingEntry.Enabled = Val
        chkIncludeYearEndEntry.Enabled = Val
        cbgSrcCode.Enabled = Val
        chkShowOPBal.Enabled = Val
        grpCompany.Enabled = Val
        grpLocaSegment.Enabled = Val
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        chkRollupWise.Enabled = Val
        gbAcc.Enabled = Val
        gbDept.Enabled = Val
        gbEmployee.Enabled = Val
        gbMachines.Enabled = Val
        gbVehicle.Enabled = Val
        gbVisi.Enabled = Val
        gbSourceCode.Enabled = Val
        chkMultipleRollup.Enabled = Val
        chkExcludeTemplete.Enabled = Val
        FlowLayoutPanel2.Enabled = Val
        FlowLayoutPanel1.Enabled = Val
        chkCusVendWiseSummary.Enabled = Val
        chkShowNetBalance.Enabled = Val
        txtFiscalYear.Enabled = Val
        chkIndAS.Enabled = Val
        chkIncludeUnusedAccount.Enabled = Val
    End Sub
    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If isRunDoubleClick Then
            If e.RowIndex >= 0 Then
                If gv1.CurrentRow IsNot Nothing AndAlso Not chkRollupWise.Checked AndAlso Not clsCommon.CompairString(cbgSrcCode.Text, "Location Wise") = CompairStringResult.Equal Then
                    Dim strACode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("AccCode").Value)
                    If clsCommon.myLen(strACode) > 0 Then
                        Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                        frm.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
                        frm.strPrevFormACode = strACode
                        frm.strPrevFormAName = clsCommon.myCstr(gv1.CurrentRow.Cells("AccName").Value)
                        frm.dTPrevFormFromDate = txtFromDate.Value
                        frm.dTPrevFormToDate = txtToDate.Value
                        If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
                            ' frm.dTPrevFormFromDate = "01/01/2012"
                            frm.RadLabel7.Visible = False
                            frm.txtFromDate.Visible = False
                            frm.MyLabel2.Text = "As On date"
                            frm.chkWithoutOpening.Checked = True
                        Else
                            frm.RadLabel7.Visible = True
                            frm.txtFromDate.Visible = True
                            frm.MyLabel2.Text = "To Date"
                            frm.chkWithoutOpening.Checked = False
                        End If
                        Dim i As Integer = 0

                        frm.arrLocSeg = New ArrayList()
                        If txtLocationSegmant.arrValueMember IsNot Nothing AndAlso txtLocationSegmant.arrValueMember.Count > 0 Then
                            frm.arrLocSeg = txtLocationSegmant.arrValueMember 'cbgLocSeg.CheckedValue
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
                        'If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                        '    frm.arrEmp = txtSourceCode.arrValueMember
                        'End If
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
                        frm.arrSourceCode = New ArrayList
                        If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                            frm.arrSourceCode = txtSourceCode.arrValueMember
                        End If


                        frm.IsVendorCustomerWiseSummary = chkCusVendWiseSummary.Checked
                        frm.IsIncludeAdjustmentEntry = chkIncludeingAdjustmentEntry.Checked
                        frm.IsIncludeClosingEntry = chkIncludeingClosingEntry.Checked
                        frm.IsIncludeYearEndEntry = chkIncludeYearEndEntry.Checked
                        'frm.MdiParent = MDI
                        frm.Show()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        'If isRunDoubleClick Then

        '    If gv1.CurrentRow IsNot Nothing AndAlso Not chkRollupWise.Checked AndAlso Not clsCommon.CompairString(cbgSrcCode.Text, "Location Wise") = CompairStringResult.Equal Then
        '        Dim strACode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("AccCode").Value)
        '        If clsCommon.myLen(strACode) > 0 Then
        '            Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        '            frm.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
        '            frm.strPrevFormACode = strACode
        '            frm.strPrevFormAName = clsCommon.myCstr(gv1.CurrentRow.Cells("AccName").Value)
        '            frm.dTPrevFormFromDate = txtFromDate.Value
        '            frm.dTPrevFormToDate = txtToDate.Value
        '            If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
        '                ' frm.dTPrevFormFromDate = "01/01/2012"
        '                frm.RadLabel7.Visible = False
        '                frm.txtFromDate.Visible = False
        '                frm.MyLabel2.Text = "As On date"
        '                frm.chkWithoutOpening.Checked = True
        '            Else
        '                frm.RadLabel7.Visible = True
        '                frm.txtFromDate.Visible = True
        '                frm.MyLabel2.Text = "To Date"
        '                frm.chkWithoutOpening.Checked = False
        '            End If
        '            Dim i As Integer = 0

        '            frm.arrLocSeg = New ArrayList()
        '            If txtLocationSegmant.arrValueMember IsNot Nothing AndAlso txtLocationSegmant.arrValueMember.Count > 0 Then
        '                frm.arrLocSeg = txtLocationSegmant.arrValueMember 'cbgLocSeg.CheckedValue
        '            End If
        '            frm.arrAcc = New ArrayList()
        '            frm.arrAcc.Add(strACode)

        '            frm.arrvehicle = New ArrayList()
        '            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
        '                frm.arrvehicle = txtVehicle.arrValueMember
        '            End If
        '            frm.arrDept = New ArrayList()
        '            If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
        '                frm.arrDept = txtDepartment.arrValueMember
        '            End If
        '            frm.arrEmp = New ArrayList
        '            'If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
        '            '    frm.arrEmp = txtSourceCode.arrValueMember
        '            'End If
        '            If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
        '                frm.arrEmp = txtEmployee.arrValueMember
        '            End If
        '            frm.arrMachine = New ArrayList
        '            If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
        '                frm.arrMachine = txtMachine.arrValueMember
        '            End If
        '            frm.arrVisi = New ArrayList
        '            If txtVISIPMX.arrValueMember IsNot Nothing AndAlso txtVISIPMX.arrValueMember.Count > 0 Then
        '                frm.arrVisi = txtVISIPMX.arrValueMember
        '            End If
        '            frm.IsVendorCustomerWiseSummary = chkCusVendWiseSummary.Checked
        '            frm.IsIncludeAdjustmentEntry = chkIncludeingAdjustmentEntry.Checked
        '            frm.IsIncludeClosingEntry = chkIncludeingClosingEntry.Checked
        '            frm.MdiParent = MDI
        '            frm.Show()
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub btnChangeOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeOrder.Click
        Try
            Dim frm As New FrmChangePrntOrdr_ACGroup()
            frm.ShowDialog()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#Region "enable/Desable Filters"
    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSelectCompany.ToggleStateChanged, rbtnAllCompany.ToggleStateChanged
        'gvDB.Enabled = Not rbtnAllCompany.IsChecked
    End Sub


    Private Sub rbtnLocSegAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocSegAll.ToggleStateChanged
        cbgLocSeg.Enabled = False
    End Sub

    Private Sub rbtnLocSegSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocSegSelect.ToggleStateChanged
        cbgLocSeg.Enabled = True
    End Sub

    Private Sub chkAccAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAccAll.ToggleStateChanged
        cbgAccount.Enabled = False
    End Sub

    Private Sub chkAccSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAccSelect.ToggleStateChanged
        cbgAccount.Enabled = True
    End Sub

    Private Sub chkVehicleAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVehicleAll.ToggleStateChanged
        cbgVehicle.Enabled = False
    End Sub

    Private Sub chkVehicleSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVehicleSelect.ToggleStateChanged
        cbgVehicle.Enabled = True
    End Sub

    Private Sub chkMachineAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMachineAll.ToggleStateChanged
        cbgmachine.Enabled = False
    End Sub

    Private Sub chkMachineSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMachineSelect.ToggleStateChanged
        cbgmachine.Enabled = True
    End Sub

    Private Sub chkDeptAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDeptAll.ToggleStateChanged
        cbgDept.Enabled = False
    End Sub

    Private Sub chkDeptSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDeptSelect.ToggleStateChanged
        cbgDept.Enabled = True
    End Sub

    Private Sub chkEmpAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkEmpAll.ToggleStateChanged
        cbgEmployee.Enabled = False
    End Sub

    Private Sub chkEmpSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkEmpSelect.ToggleStateChanged
        cbgEmployee.Enabled = True
    End Sub

    Private Sub chkVisiAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVisiAll.ToggleStateChanged
        cbgVisi.Enabled = False
    End Sub

    Private Sub chkVisiSelct_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVisiSelct.ToggleStateChanged
        cbgVisi.Enabled = True
    End Sub

    Private Sub chkSrcCodeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSrcCodeAll.ToggleStateChanged
        cbgSourceCode.Enabled = False
    End Sub

    Private Sub chkSrcCodeSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSrcCodeSelect.ToggleStateChanged
        cbgSourceCode.Enabled = True
    End Sub
#End Region

    Private Sub chkRollupWise_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRollupWise.ToggleStateChanged
        If chkRollupWise.Checked = True Then
            chkMultipleRollup.Checked = False
        End If
        If chkMultipleRollup.Checked Then
            btnRefresh.Enabled = False
        Else
            If chkRollupWise.Checked Then
                btnRefresh.Enabled = False
            Else
                btnRefresh.Enabled = True
            End If
        End If
    End Sub

    Private Sub chkMultipleRollup_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMultipleRollup.ToggleStateChanged
        If chkMultipleRollup.Checked = True Then
            chkRollupWise.Checked = False
        End If
        If chkRollupWise.Checked Then
            btnRefresh.Enabled = False
        Else
            If chkMultipleRollup.Checked Then
                btnRefresh.Enabled = False
            Else
                btnRefresh.Enabled = True
            End If
        End If
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub


    Private Sub txtCompany__My_Click(sender As Object, e As EventArgs) Handles txtCompany._My_Click
        Dim qry As String = "SELECT Comp_Code as Code,Comp_Name as Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        txtCompany.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtCompany.arrValueMember, txtCompany.arrDispalyMember)
    End Sub
    '======shivani tyagi
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
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    'KUNAL > REVERTED QUERY TO VERSION WHERE BALWINDER SIR LAST COMPLETED > TICKET: BM0000009477 > DATE : 19 -OCT -2016
    Private Sub txtLocationSegmant__My_Click(sender As Object, e As EventArgs) Handles txtLocationSegmant._My_Click
        Try
            Dim qry As String = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name , TSPL_GL_SEGMENT_CODE.STATE_CODE ,SM.STATE_NAME from"
            qry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
            qry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7' LEFT JOIN TSPL_STATE_MASTER SM ON SM.STATE_CODE = TSPL_GL_SEGMENT_CODE.STATE_CODE "
            qry += " order by xxx.Loc_Segment_Code"
            txtLocationSegmant.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationMulSel", qry, "Code", "Name", txtLocationSegmant.arrValueMember, txtLocationSegmant.arrDispalyMember)
            SetDiplayMember(txtLocationSegmant, "Description", "TSPL_LOCATION_MASTER", "Loc_Segment_Code")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub





    Private Sub txtAccount__My_Click(sender As Object, e As EventArgs) Handles txtAccount._My_Click
        Dim qry As String = " select Account_Code as Code,[Description] as [Name] from TSPL_GL_ACCOUNTS"
        txtAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("AccountMulSel", qry, "Code", "Name", txtAccount.arrValueMember, txtAccount.arrDispalyMember)
    End Sub

    Private Sub txtSourceCode__My_Click(sender As Object, e As EventArgs) Handles txtSourceCode._My_Click
        Dim qry As String = " select SourceCode as Code,SourceDescription as Name from TSPL_GL_SOURCECODE"
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

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            ''richa agarwal 17/06/2015
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTrialBalance & "'"))
            If clsCommon.myLen(cbgSrcCode.Text) > 0 Then
                arrHeader.Add("Report Type : " + cbgSrcCode.Text)
            End If
            ''---------------------


            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            '==shivani
            If txtLocationSegmant.arrValueMember IsNot Nothing AndAlso txtLocationSegmant.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocationSegmant.arrDispalyMember))
            End If
            ''=========
            If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                arrHeader.Add("Account : " + clsCommon.GetMulcallStringWithComma(txtAccount.arrValueMember))
            End If


            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicle.arrValueMember))
            End If

            If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                arrHeader.Add("Department : " + clsCommon.GetMulcallStringWithComma(txtDepartment.arrValueMember))
            End If


            If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                arrHeader.Add("Employee : " + clsCommon.GetMulcallStringWithComma(txtEmployee.arrValueMember))
            End If

            If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
                arrHeader.Add("Machine : " + clsCommon.GetMulcallStringWithComma(txtMachine.arrValueMember))
            End If

            
            If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                arrHeader.Add("SourceCode : " + clsCommon.GetMulcallStringWithComma(txtSourceCode.arrValueMember))
            End If

            If txtVISIPMX.arrValueMember IsNot Nothing AndAlso txtVISIPMX.arrValueMember.Count > 0 Then
                arrHeader.Add("VISIPMX : " + clsCommon.GetMulcallStringWithComma(txtVISIPMX.arrValueMember))
            End If
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtMainGroup__My_Click(sender As Object, e As EventArgs) Handles txtMainGroup._My_Click
        Dim qry As String = "select Account_Main_Group_Code as Code,Account_Main_Group_Desc as Name,Group_Type as Type from TSPL_ACCOUNT_MAIN_GROUPS"
        txtMainGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("rptMGTilBal", qry, "Code", "Name", txtMainGroup.arrValueMember, txtMainGroup.arrDispalyMember)
    End Sub

    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtACGrpType._My_Click
        Dim qry As String = "select distinct Group_Type as Code from TSPL_ACCOUNT_MAIN_GROUPS  "
        txtACGrpType.arrValueMember = clsCommon.ShowMultipleSelectForm("rptACMGTlBal", qry, "Code", "", txtACGrpType.arrValueMember, Nothing)
    End Sub

    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("rptACMGTlBal", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
        SetFiscalYear()
    End Sub

    Sub SetFiscalYear()
        txtFromDate.MinDate = New Date(2001, 4, 1)
        txtFromDate.MaxDate = New Date(3000, 12, 1)
        txtToDate.MinDate = txtFromDate.MinDate
        txtToDate.MaxDate = txtFromDate.MaxDate
        If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
            Dim qry As String = " select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                txtFromDate.MinDate = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
                txtFromDate.MaxDate = clsCommon.myCDate(dt.Rows(0)("End_Date"))
                txtToDate.MinDate = txtFromDate.MinDate
                txtToDate.MaxDate = txtFromDate.MaxDate

                txtFromDate.Value = txtFromDate.MinDate
                txtToDate.Value = txtFromDate.MaxDate
            End If
        Else
            txtToDate.Value = clsCommon.GETSERVERDATE()
            If txtToDate.Value.Month >= 1 AndAlso txtToDate.Value.Month <= 3 Then
                txtFromDate.Value = New Date(txtToDate.Value.Year - 1, 4, 1)
            Else
                txtFromDate.Value = New Date(txtToDate.Value.Year, 4, 1)
            End If
        End If
    End Sub



    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            ''richa agarwal 17/06/2015
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTrialBalance & "'"))
            If clsCommon.myLen(cbgSrcCode.Text) > 0 Then
                arrHeader.Add("Report Type : " + cbgSrcCode.Text)
            End If
            ''---------------------


            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            '==shivani
            If txtLocationSegmant.arrValueMember IsNot Nothing AndAlso txtLocationSegmant.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocationSegmant.arrDispalyMember))
            End If
            ''=========
            If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                arrHeader.Add("Account : " + clsCommon.GetMulcallStringWithComma(txtAccount.arrValueMember))
            End If


            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                'arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicle.arrDispalyMember))
                arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicle.arrValueMember))
            End If

            If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                'arrHeader.Add("Department : " + clsCommon.GetMulcallStringWithComma(txtDepartment.arrDispalyMember))
                arrHeader.Add("Department : " + clsCommon.GetMulcallStringWithComma(txtDepartment.arrValueMember))
            End If

            'If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
            '    arrHeader.Add("Employee : " + clsCommon.GetMulcallStringWithComma(txtSourceCode.arrDispalyMember))
            'End If
            If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                'arrHeader.Add("Employee : " + clsCommon.GetMulcallStringWithComma(txtEmployee.arrDispalyMember))
                arrHeader.Add("Employee : " + clsCommon.GetMulcallStringWithComma(txtEmployee.arrValueMember))
            End If

            If txtMachine.arrValueMember IsNot Nothing AndAlso txtMachine.arrValueMember.Count > 0 Then
                'arrHeader.Add("Machine : " + clsCommon.GetMulcallStringWithComma(txtMachine.arrDispalyMember))
                arrHeader.Add("Machine : " + clsCommon.GetMulcallStringWithComma(txtMachine.arrValueMember))
            End If

            'If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
            '    arrHeader.Add("SourceCode : " + clsCommon.GetMulcallStringWithComma(txtEmployee.arrDispalyMember))
            'End If
            If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                ''arrHeader.Add("SourceCode : " + clsCommon.GetMulcallStringWithComma(txtSourceCode.arrDispalyMember))
                arrHeader.Add("SourceCode : " + clsCommon.GetMulcallStringWithComma(txtSourceCode.arrValueMember))
            End If

            If txtVISIPMX.arrValueMember IsNot Nothing AndAlso txtVISIPMX.arrValueMember.Count > 0 Then
                'arrHeader.Add("VISIPMX : " + clsCommon.GetMulcallStringWithComma(txtVISIPMX.arrDispalyMember))
                arrHeader.Add("VISIPMX : " + clsCommon.GetMulcallStringWithComma(txtVISIPMX.arrValueMember))
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
            ''richa agarwal 17/06/2015
            ' transportSql.exportdata(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1)) 'frm.Text)
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            ' '''''''''''''''---------------------
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub QExpCSV_Click(sender As Object, e As EventArgs) Handles QExpCSV.Click
        Try
            If Gv1 Is Nothing OrElse Gv1.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(Gv1, True)
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
            clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    '' richa KDI/24/08/18-000422
    'Private Sub ReStoreGridLayout()
    '    Try
    '        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
    '            Dim obj As clsGridLayout = New clsGridLayout()
    '            obj = CType(obj.GetData(Form_ID & gv1.Name.ToString() & clsCommon.myCstr(gv1.Tag), "", objCommonVar.CurrentUserCode), clsGridLayout)
    '            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
    '                Dim ii As Integer
    '                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
    '                    gv1.Columns(ii).IsVisible = False
    '                    gv1.Columns(ii).VisibleInColumnChooser = True
    '                Next
    '                gv1.LoadLayout(obj.GridLayout)
    '                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '            End If
    '            obj = Nothing
    '        End If

    '    Catch err As Exception
    '        MessageBox.Show(err.Message)
    '    End Try
    'End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
                    arrHeader.Add("Fiscal Year : " + txtFiscalYear.Value)
                End If
                If txtFromDate.Visible = True Then
                    arrHeader.Add(("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                Else
                    arrHeader.Add(("As on Date : " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                End If

                If clsCommon.myLen(cbgSrcCode.Text) > 0 Then
                    arrHeader.Add("Report Type : " + cbgSrcCode.Text)
                End If

                If txtACGrpType.arrValueMember IsNot Nothing AndAlso txtACGrpType.arrValueMember.Count > 0 Then
                    arrHeader.Add("Account Group Type : " + clsCommon.GetMulcallStringWithComma(txtACGrpType.arrValueMember))
                End If
                If txtMainGroup.arrValueMember IsNot Nothing AndAlso txtMainGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add("Account Group : " + clsCommon.GetMulcallStringWithComma(txtMainGroup.arrDispalyMember))
                End If
                If txtCompany.arrValueMember IsNot Nothing AndAlso txtCompany.arrValueMember.Count > 0 Then
                    arrHeader.Add("Company : " + clsCommon.GetMulcallStringWithComma(txtCompany.arrDispalyMember))
                End If
                If txtLocationSegmant.arrValueMember IsNot Nothing AndAlso txtLocationSegmant.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocationSegmant.arrDispalyMember))
                End If
                If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                    arrHeader.Add("Account : " + clsCommon.GetMulcallStringWithComma(txtAccount.arrValueMember))
                End If
                If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                    arrHeader.Add("Department : " + clsCommon.GetMulcallStringWithComma(txtDepartment.arrValueMember))
                End If
                If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("SourceCode : " + clsCommon.GetMulcallStringWithComma(txtSourceCode.arrValueMember))
                End If


                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Trial Balance", gv1, arrHeader, "Trial Balance", PageSetupReport_ID, objCommonVar.CurrentUserCode)
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
        Try
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
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
End Class
