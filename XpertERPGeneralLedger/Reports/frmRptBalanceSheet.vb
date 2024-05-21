'Edited By Rohit on May 28,2014 to show drill down according to Trial Balance Report.
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
'===================Update by preeti gupta Against ticket no [BM00000007733]
Imports common
Imports System.IO

Public Class frmRptBalanceSheet
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim StrQry As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim ReportID As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ChkGroupCodeDD As String = Nothing
    Dim ChkAccountGroupCodeDD As String = Nothing
    Dim ChkSubGroupCodeDD As String = Nothing
    Dim ChkMainGLAccountDD As String = Nothing
    Private tableView As TableViewDefinition
    Dim dt As DataTable = Nothing
    Dim arrBack As New List(Of String)
    Public arrLocalSegment As ArrayList
    Public arrGLAccountGroup As ArrayList
    Public arrGLMainAccountGroup As ArrayList
    '============Created By Rohit on May 28,2014 to Add Accounts in general ledger Array.==========
    Dim dtAccount As DataTable = Nothing
    '===============================================================================================
    Dim settSelectGLInBalanceSheetPerforma As Boolean = False
#End Region
    ''Check Prabhakar 19/06/2019
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptBalanceSheet)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        ''updated by preeti gupta ticket no [BM00000007733]
        btnExport.Visible = MyBase.isExport
        btnRefresh.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReportID = Me.Form_ID
        settSelectGLInBalanceSheetPerforma = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SelectGLInBalanceSheetPerforma, clsFixedParameterCode.SelectGLInBalanceSheetPerforma, Nothing)) > 0)
        SetUserMgmtNew()
        arrBack = New List(Of String)
        tableView = CType(Me.gv1.ViewDefinition, TableViewDefinition)
        RadPageView1.SelectedPage = RadPageViewPage1

        LoadLocatinSegment()
        LoadGLMainACGroup()
        cbgLocSeg.CheckedAll()

        LoadRollupAC()
        LoadGLACGroup()
        LoadGLMainAC()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+R Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnExpoertToExcel, "Press Alt+E for Export To Excel")


        If ReportLevel = 1 Then
            ChkAccountMainGrpSelect.IsChecked = True
            rbtnGLGroupLevel.IsChecked = True
            Dim arr As New ArrayList()
            arr.Add(strCurrentGrp)
            CbgAccountmainGrp.CheckedValue = arr
            cbgLocSeg.CheckedValue = arrGlgroup
            RefreshData()
        ElseIf ReportLevel = 2 Then
            rbtnGLAccountGroupSelect.IsChecked = True
            rbtnGLRollupLevel.IsChecked = True
            Dim arr As New ArrayList()
            arr.Add(strCurrentGrp)
            cbgGLACGrp.CheckedValue = arr
            cbgLocSeg.CheckedValue = arrGlgroup
            RefreshData()
        ElseIf ReportLevel = 3 Then
            rbtnGLRollupLevel.IsChecked = True
            ChkGLMainAccount.IsChecked = True
            Dim arr As New ArrayList()
            arr.Add(strCurrentRollupCode)
            cbgRollupAC.CheckedValue = arr
            cbgLocSeg.CheckedValue = arrGlgroupRollUp
            RefreshData()
        ElseIf ReportLevel = 4 Then
            ChkGLAccount.IsChecked = True
            rbtnGlMainAccountSelect.IsChecked = True
            Dim arr As New ArrayList()
            arr.Add(strCurrentRollupCode)
            cbgGLMainAccount.CheckedValue = arr
            cbgLocSeg.CheckedValue = arrGlgroupRollUp
            RefreshData()
        Else
            rbtnRollupAll.IsChecked = True
            rbtnGLAccountGrpAll.IsChecked = True
            ChkAccountMainGrpAll.IsChecked = True
            rbtnGlMainAccountAll.IsChecked = True
            rbtnYearly.IsChecked = True
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value
        End If

    End Sub

    Sub LoadRollupAC()
        ' Dim qry As String = "select xxx.Account_Code AS Code,TSPL_GL_ACCOUNTS.Description as Name from (select Account_Code from TSPL_ACCOUNT_Sub_GROUPS group by Account_Code )xxx left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =xxx.Account_Code"
        Dim qry As String = "select Account_Sub_Group_Code as Code,Account_Sub_Group_Desc as Name from TSPL_ACCOUNT_Sub_GROUPS"
        cbgRollupAC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRollupAC.ValueMember = "Code"
        cbgRollupAC.DisplayMember = "Name"
    End Sub

    Sub LoadGLMainAC()
        ' Dim qry As String = "select xxx.Account_Code AS Code,TSPL_GL_ACCOUNTS.Description as Name from (select Account_Code from TSPL_ACCOUNT_Sub_GROUPS group by Account_Code )xxx left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =xxx.Account_Code"
        Dim qry As String = "select Main_GL_Account as Code,Main_GL_Account_desc as Name from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
        cbgGLMainAccount.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgGLMainAccount.ValueMember = "Code"
        cbgGLMainAccount.DisplayMember = "Name"
    End Sub

    Sub LoadGLACGroup()
        Dim qry As String = "select Account_Group_Code as Code,Account_Group_Desc as Name from TSPL_ACCOUNT_GROUPS"
        cbgGLACGrp.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgGLACGrp.ValueMember = "Code"
        cbgGLACGrp.DisplayMember = "Name"
        '============Created By Rohit on May 28,2014 to Add Accounts in general ledger Array.==========
        dtAccount = clsDBFuncationality.GetDataTable("select * from TSPL_Gl_Accounts") ' where ControlAccount='Y'
        '========================================================================================
    End Sub

    Sub LoadGLMainACGroup()
        Dim qry As String = "select Account_Main_Group_Code as Code,Account_Main_Group_Desc as Name from TSPL_ACCOUNT_main_GROUPS"
        CbgAccountmainGrp.DataSource = clsDBFuncationality.GetDataTable(qry)
        CbgAccountmainGrp.ValueMember = "Code"
        CbgAccountmainGrp.DisplayMember = "Name"
    End Sub


    Private Sub frmRptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            RefreshData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E Then
            ' FunExportToExcel(ByVal exporter As EnumExportTo)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Sub LoadLocatinSegment()
        cbgLocSeg.DataSource = clsLocation.GetLocationSegments()
        cbgLocSeg.ValueMember = "Code"
        cbgLocSeg.DisplayMember = "Name"
    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
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

        gv1.ViewDefinition = tableView
        'txtmultLocationSegment.arrValueMember = Nothing
        'txtmultAccountMainGroup.arrValueMember = Nothing
        'txtmultAccountGroup.arrValueMember = Nothing
        'txtmultAccountSubGroup.arrValueMember = Nothing
        'txtMultGLMainAmountGroup.arrValueMember = Nothing
        rbtnNA.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = ReportID
        RefreshData()
    End Sub
    '================Rohit,Ap 07,2015================
    'Public Sub RefreshData()
    '    Try
    '        If cbgLocSeg.CheckedValue.Count <= 0 Then
    '            Throw New Exception("Please select at least one location Segment")
    '        End If
    '        If rbtnGLAccountGroupSelect.IsChecked AndAlso cbgGLACGrp.CheckedValue.Count <= 0 Then
    '            Throw New Exception("Please select at least one Account Group")
    '        End If

    '        If rbtnRollupSelect.IsChecked AndAlso cbgRollupAC.CheckedValue.Count <= 0 Then
    '            Throw New Exception("Please select at least one Rollup Account")
    '        End If

    '        gv1.EnableFiltering = True
    '        gv1.GroupDescriptors.Clear()
    '        gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '        Dim strPAndLGroupCode As String = clsFixedParameter.GetData(clsFixedParameterType.BalanceSheetProftAndLossGroupCode, clsFixedParameterCode.BalanceSheetProftAndLossGroupCode, Nothing)
    '        Dim strPAndLGroupDesc As String = clsFixedParameter.GetData(clsFixedParameterType.BalanceSheetProftAndLossGroupDesc, clsFixedParameterCode.BalanceSheetProftAndLossGroupDesc, Nothing)


    '        Dim FinalQty As String = ""
    '        Dim strExtraColumn As String = ""
    '        Dim arr As New Dictionary(Of String, clsTempBalanceSheet)
    '        If Not rbtnDateRange.IsChecked Then
    '            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
    '                txtFiscalYear.Focus()
    '                Throw New Exception("Please enter fiscal year")
    '            End If

    '            Dim qry As String = "select  Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + txtFiscalYear.Value + "'"
    '            Dim dtFY As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            If dtFY Is Nothing OrElse dtFY.Rows.Count <= 0 Then
    '                Throw New Exception("fiscal year " + txtFiscalYear.Value + " date range Not found")
    '            End If
    '            Dim dtStartDate As Date = clsCommon.myCDate(dtFY.Rows(0)("Start_Date"))
    '            Dim dtEndDate As Date = clsCommon.myCDate(dtFY.Rows(0)("End_Date"))
    '            txtFromDate.Value = dtStartDate
    '            txtToDate.Value = dtEndDate
    '            Dim intMonth As Integer = 1
    '            If rbtnYearly.IsChecked Then
    '                intMonth = 12
    '            ElseIf rbtnHalfYearly.IsChecked Then
    '                intMonth = 6
    '            ElseIf rbtnQuarterly.IsChecked Then
    '                intMonth = 3
    '            End If

    '            Dim tempEndDate As Date = dtStartDate
    '            Dim objTempBC As clsTempBalanceSheet = Nothing
    '            While tempEndDate < dtEndDate
    '                tempEndDate = dtStartDate.AddMonths(intMonth)
    '                If chkLocationWise.Checked Then
    '                    For Each StrLoc As String In cbgLocSeg.CheckedValue
    '                        objTempBC = New clsTempBalanceSheet
    '                        strExtraColumn += ",( Amount * case when convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(dtStartDate, "dd/MM/yyyy") + "',103) and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(tempEndDate, "dd/MM/yyyy") + "',103) and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + StrLoc + "' then 1 else 0 end ) "
    '                        If rbtnMonthly.IsChecked Then
    '                            objTempBC.Interval = clsCommon.GetPrintDate(dtStartDate, "MMM-yy")
    '                            objTempBC.location = StrLoc
    '                            objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " (" + StrLoc + ")]"
    '                            arr.Add(objTempBC.FullName, objTempBC)
    '                        Else
    '                            objTempBC.Interval = clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy")
    '                            objTempBC.location = StrLoc
    '                            objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + " (" + StrLoc + ")]"
    '                            arr.Add(objTempBC.FullName, objTempBC)
    '                        End If
    '                        strExtraColumn += " as  " + objTempBC.FullName
    '                    Next
    '                End If

    '                objTempBC = New clsTempBalanceSheet

    '                strExtraColumn += ",( Amount * case when convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(dtStartDate, "dd/MM/yyyy") + "',103) and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(tempEndDate, "dd/MM/yyyy") + "',103) then 1 else 0 end ) "
    '                If rbtnMonthly.IsChecked Then
    '                    objTempBC.Interval = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + "]"
    '                    objTempBC.location = ""
    '                    objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + "]"
    '                    arr.Add(objTempBC.FullName, objTempBC)
    '                Else
    '                    objTempBC.Interval = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + "]"
    '                    objTempBC.location = ""
    '                    objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + "]"
    '                    arr.Add(objTempBC.FullName, objTempBC)
    '                End If
    '                strExtraColumn += " as  " + objTempBC.FullName

    '                dtStartDate = tempEndDate
    '            End While
    '        End If

    '        Dim FromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
    '        Dim ToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
    '        Dim BaseQry As String = "select TSPL_GL_ACCOUNTS.Account_Balance,TSPL_JOURNAL_DETAILS.Account_code,TSPL_GL_ACCOUNTS.Description as AccountName, TSPL_GL_ROLLUP.Account_Code as RollupCode,TabRollupName.Description as RollupName,TSPL_ACCOUNT_GROUPS.Account_Group_Code as GrpCode,tspl_balance_sheet_performa.Group_Name as GrpName, Amount,TSPL_BALANCE_SHEET_PERFORMA.S_No,TSPL_BALANCE_SHEET_PERFORMA.Main_Particular,TSPL_BALANCE_SHEET_PERFORMA.type as typ,TSPL_BALANCE_SHEET_PERFORMA.Particular,TSPL_BALANCE_SHEET_PERFORMA.Type,TSPL_BALANCE_SHEET_PERFORMA.Note " + strExtraColumn + Environment.NewLine
    '        BaseQry += " from TSPL_JOURNAL_DETAILS " + Environment.NewLine
    '        BaseQry += " inner join  TSPL_GL_ROLLUP on TSPL_GL_ROLLUP.account= TSPL_JOURNAL_DETAILS.Account_code " + Environment.NewLine
    '        BaseQry += " left outer join TSPL_GL_ACCOUNTS as TabRollupName on TabRollupName.Account_Code=TSPL_GL_ROLLUP.Account_Code " + Environment.NewLine
    '        BaseQry += " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code " + Environment.NewLine
    '        BaseQry += " left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_GL_ACCOUNTS.Account_Group_Code " + Environment.NewLine
    '        BaseQry += " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine
    '        BaseQry += " inner join TSPL_BALANCE_SHEET_PERFORMA on TSPL_BALANCE_SHEET_PERFORMA.Group_Code= TSPL_ACCOUNT_GROUPS.Account_Group_Code" + Environment.NewLine
    '        BaseQry += " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A'  and  TSPL_GL_ACCOUNTS.Account_Seg_Code7 in  (" + clsCommon.GetMulcallString(cbgLocSeg.CheckedValue) + ") and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)<= convert(date,'" + ToDate + "',103) " + Environment.NewLine

    '        If rbtnGLAccountGroupSelect.IsChecked Then
    '            BaseQry += " and TSPL_ACCOUNT_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(cbgGLACGrp.CheckedValue) + ")"
    '        End If
    '        If rbtnRollupSelect.IsChecked Then
    '            BaseQry += " and TSPL_GL_ROLLUP.Account_Code in (" + clsCommon.GetMulcallString(cbgRollupAC.CheckedValue) + ")"
    '        End If

    '        If clsCommon.myLen(strPAndLGroupCode) > 0 Then
    '            BaseQry += " Union all " + Environment.NewLine
    '            BaseQry += " select TSPL_GL_ACCOUNTS.Account_Balance,TSPL_JOURNAL_DETAILS.Account_code,TSPL_GL_ACCOUNTS.Description as AccountName,TSPL_GL_ROLLUP.Account_Code as RollupCode,TabRollupName.Description as RollupName,TSPL_BALANCE_SHEET_PERFORMA.Group_Code as GrpCode,tspl_balance_sheet_performa.Group_Name as GrpName, Amount,TSPL_BALANCE_SHEET_PERFORMA.S_No,TSPL_BALANCE_SHEET_PERFORMA.Main_Particular,TSPL_BALANCE_SHEET_PERFORMA.type as typ,TSPL_BALANCE_SHEET_PERFORMA.Particular,TSPL_BALANCE_SHEET_PERFORMA.Type,TSPL_BALANCE_SHEET_PERFORMA.Note " + strExtraColumn + Environment.NewLine
    '            BaseQry += " from TSPL_JOURNAL_DETAILS " + Environment.NewLine
    '            BaseQry += " Inner join  TSPL_GL_ROLLUP on TSPL_GL_ROLLUP.account= TSPL_JOURNAL_DETAILS.Account_code " + Environment.NewLine
    '            BaseQry += " left outer join TSPL_GL_ACCOUNTS as TabRollupName on TabRollupName.Account_Code=TSPL_GL_ROLLUP.Account_Code " + Environment.NewLine
    '            BaseQry += " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code " + Environment.NewLine
    '            BaseQry += " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine
    '            BaseQry += " inner join TSPL_BALANCE_SHEET_PERFORMA on TSPL_BALANCE_SHEET_PERFORMA.Group_Code= " + strPAndLGroupCode + "" + Environment.NewLine
    '            BaseQry += " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A'  and  TSPL_GL_ACCOUNTS.Account_Seg_Code7 in  (" + clsCommon.GetMulcallString(cbgLocSeg.CheckedValue) + ") and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)<= convert(date,'" + ToDate + "',103)  and TSPL_GL_ACCOUNTS.Account_Type='Income Statement'" + Environment.NewLine
    '            If rbtnGLAccountGroupSelect.IsChecked Then
    '                BaseQry += " and TSPL_BALANCE_SHEET_PERFORMA.Group_Code in (" + clsCommon.GetMulcallString(cbgGLACGrp.CheckedValue) + ")"
    '            End If
    '            If rbtnRollupSelect.IsChecked Then
    '                BaseQry += " and TSPL_GL_ROLLUP.Account_Code in (" + clsCommon.GetMulcallString(cbgRollupAC.CheckedValue) + ")"
    '            End If

    '        End If
    '        Dim fnlQty As String = ""

    '        If rbtnNA.IsChecked Then
    '            fnlQty = "select Account_Balance,MAX(S_No) as S_No, Main_Particular ,Particular ,GrpCode,(max(isnull(GrpName,''))) as GrpName,max(Note) as Note "
    '            If arr IsNot Nothing AndAlso arr.Count > 0 Then
    '                For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
    '                    '==============Changed by Rohit on May 28,2014 .if Account is Debit then Value will be Positive else Negetive.
    '                    'fnlQty += ",ABS( sum(" + strkey.Key + ")) as " + strkey.Key
    '                    fnlQty += ",sum(" + strkey.Key + ")* typ as " + strkey.Key
    '                    ' * case when Account_Balance='Debit' and typ=1 then 1 else -1 end)*(typ)* typ
    '                    '====================================================================================================
    '                Next
    '            End If

    '            'fnlQty += ",ABS( sum(Amount) ) as Amt ,(max(Type)*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Main_Particular,typ,Particular,GrpCode,Account_Balance order by S_No"
    '            fnlQty += ", sum(Amount)* typ  as Amt ,(typ*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Main_Particular,typ,Particular,GrpCode,Account_Balance order by S_No"
    '        ElseIf rbtnGLGroupLevel.IsChecked Then
    '            fnlQty = "select RollupCode,max( RollupName ) as RollupName "
    '            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
    '                For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
    '                    fnlQty += ",sum(" + strkey.Key + ")* type as " + strkey.Key
    '                    ' fnlQty += ",ABS( sum(" + strkey.Key + ")) as " + strkey.Key
    '                Next
    '            End If
    '            fnlQty += ", sum(Amount) * type as Amt ,(type *  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by RollupCode,type "
    '            'fnlQty += ",ABS( sum(Amount) ) as Amt ,(max(Type)*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by RollupCode "
    '        ElseIf rbtnGLRollupLevel.IsChecked Then
    '            fnlQty = "select Account_code,max( typ ) as AccountName "
    '            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
    '                For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
    '                    fnlQty += ", sum(" + strkey.Key + ") as " + strkey.Key
    '                    'fnlQty += ",ABS( sum(" + strkey.Key + ")) as " + strkey.Key
    '                Next
    '            End If
    '            fnlQty += ", sum(Amount)  as Amt ,(Type*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Account_code,Type "
    '            'fnlQty += ",ABS( sum(Amount) ) as Amt ,(max(Type)*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Account_code "

    '        End If

    '        dt = clsDBFuncationality.GetDataTable(fnlQty)
    '        If dt.Rows.Count <= 0 Then
    '            gv1.DataSource = Nothing
    '            common.clsCommon.MyMessageBoxShow("No Data Found")
    '            Exit Sub
    '        End If
    '        SetGridFormation(arr)
    '        'LoadBlankGrid()
    '        RadPageView1.SelectedPage = RadPageViewPage2
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Public Sub RefreshData()
        Try
            gv1.EnableFiltering = True
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strPAndLGroupCode As String = clsFixedParameter.GetData(clsFixedParameterType.BalanceSheetProftAndLossGroupCode, clsFixedParameterCode.BalanceSheetProftAndLossGroupCode, Nothing)
            Dim strPAndLGroupDesc As String = clsFixedParameter.GetData(clsFixedParameterType.BalanceSheetProftAndLossGroupDesc, clsFixedParameterCode.BalanceSheetProftAndLossGroupDesc, Nothing)
            Dim ChkLocationSegment As String

            If txtmultLocationSegment.arrValueMember IsNot Nothing AndAlso txtmultLocationSegment.arrValueMember.Count > 0 Then
                ChkLocationSegment = "" + clsCommon.GetMulcallString(txtmultLocationSegment.arrValueMember) + ""
            Else

                StrQry = "select xxx.Loc_Segment_Code as Code  from"
                StrQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
                StrQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
                StrQry += " order by xxx.Loc_Segment_Code"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry)
                Dim strlocdesc As String = String.Empty
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    For Each grow As DataRow In dt.Rows
                        strlocdesc += "'" + clsCommon.myCstr(grow("Code")) + "',"
                    Next
                    If clsCommon.myLen(strlocdesc) > 0 Then
                        strlocdesc = strlocdesc.Substring(0, strlocdesc.Length - 1)
                    End If
                End If


                ChkLocationSegment = strlocdesc
            End If

            Dim FinalQty As String = ""
            Dim strExtraColumn As String = ""
            Dim strExtraColumnWithMax As String = ""
            Dim arr As New Dictionary(Of String, clsTempBalanceSheet)
            If Not rbtnDateRange.IsChecked Then
                If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                    txtFiscalYear.Focus()
                    Throw New Exception("Please enter fiscal year")
                End If

                Dim qry As String = "select  Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + txtFiscalYear.Value + "'"
                Dim dtFY As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtFY Is Nothing OrElse dtFY.Rows.Count <= 0 Then
                    Throw New Exception("fiscal year " + txtFiscalYear.Value + " date range Not found")
                End If
                Dim dtStartDate As Date = clsCommon.myCDate(dtFY.Rows(0)("Start_Date"))
                Dim dtEndDate As Date = clsCommon.myCDate(dtFY.Rows(0)("End_Date"))
                txtFromDate.Value = dtStartDate
                txtToDate.Value = dtEndDate
                Dim intMonth As Integer = 1
                If rbtnYearly.IsChecked Then
                    intMonth = 12
                ElseIf rbtnHalfYearly.IsChecked Then
                    intMonth = 6
                ElseIf rbtnQuarterly.IsChecked Then
                    intMonth = 3
                End If

                Dim tempEndDate As Date = dtStartDate
                Dim objTempBC As clsTempBalanceSheet = Nothing
                While tempEndDate < dtEndDate
                    tempEndDate = dtStartDate.AddMonths(intMonth)
                    If chkLocationWise.Checked Then
                        For Each StrLoc As String In cbgLocSeg.CheckedValue
                            objTempBC = New clsTempBalanceSheet
                            strExtraColumn += ",( Amount * case when "
                            If Not chkIncludeingOpeningEntry.Checked Then
                                strExtraColumn += " convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(dtStartDate, "dd/MM/yyyy") + "',103) and "
                            End If
                            strExtraColumn += " convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "dd/MM/yyyy") + "',103) and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + StrLoc + "' then 1 else 0 end ) "
                            If rbtnMonthly.IsChecked Then
                                objTempBC.Interval = clsCommon.GetPrintDate(dtStartDate, "MMM-yy")
                                objTempBC.location = StrLoc
                                objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " (" + StrLoc + ")]"
                                arr.Add(objTempBC.FullName, objTempBC)
                            Else
                                objTempBC.Interval = clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy")
                                objTempBC.location = StrLoc
                                objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + " (" + StrLoc + ")]"
                                arr.Add(objTempBC.FullName, objTempBC)
                            End If
                            strExtraColumn += " as  " + objTempBC.FullName
                            strExtraColumnWithMax += " , sum(" + objTempBC.FullName + ") * -1 as  " + objTempBC.FullName
                        Next
                    End If

                    objTempBC = New clsTempBalanceSheet

                    strExtraColumn += ",( Amount * case when "
                    If Not chkIncludeingOpeningEntry.Checked Then
                        strExtraColumn += " convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(dtStartDate, "dd/MM/yyyy") + "',103) and "
                    End If
                    strExtraColumn += " convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "dd/MM/yyyy") + "',103) then 1 else 0 end ) "
                    If rbtnMonthly.IsChecked Then
                        objTempBC.Interval = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + "]"
                        objTempBC.location = ""
                        objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + "]"
                        arr.Add(objTempBC.FullName, objTempBC)
                    Else
                        objTempBC.Interval = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + "]"
                        objTempBC.location = ""
                        objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + "]"
                        arr.Add(objTempBC.FullName, objTempBC)
                    End If
                    strExtraColumn += " as  " + objTempBC.FullName
                    strExtraColumnWithMax += " , sum(" + objTempBC.FullName + ") * -1 as  " + objTempBC.FullName
                    dtStartDate = tempEndDate
                End While
            End If

            Dim FromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
            Dim ToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")


            Dim BaseQry As String = " select 0 as Position, TSPL_GL_ACCOUNTS.Account_Balance,TSPL_JOURNAL_DETAILS.Account_code,TSPL_GL_ACCOUNTS.Description as AccountName,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account as Main_GL_Account,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.main_gl_Account_desc as MainGLName,TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code as SubGrpCode,TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_desc as SubGrpdesc,TSPL_BALANCE_SHEET_PERFORMA.Group_Code as GrpCode,tspl_balance_sheet_performa.Group_Name as GrpName, Amount,TSPL_BALANCE_SHEET_PERFORMA.S_No,TSPL_BALANCE_SHEET_PERFORMA.Main_Particular,TSPL_BALANCE_SHEET_PERFORMA.type as typ,TSPL_BALANCE_SHEET_PERFORMA.Particular,TSPL_BALANCE_SHEET_PERFORMA.Type,TSPL_BALANCE_SHEET_PERFORMA.Note,TSPL_ACCOUNT_GROUPS.Account_Group_Code,TSPL_ACCOUNT_GROUPS.Account_Group_Desc " & strExtraColumn & " from TSPL_JOURNAL_DETAILS inner join TSPL_GL_ACCOUNTS as TabRollupName on TabRollupName.Account_Code=" _
                                     & " TSPL_JOURNAL_DETAILS.Account_Code inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code " _
                                     & " inner join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.gl_main_code " _
                                     & " inner join TSPL_ACCOUNT_Sub_GROUPS on TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.sub_Group_Code " _
                                     & " inner join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_Sub_GROUPS.Account_Group_Code " _
                                     & " inner join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_Main_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code " _
                                     & " inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " _
                                     & " inner join TSPL_BALANCE_SHEET_PERFORMA on TSPL_BALANCE_SHEET_PERFORMA.Group_Code= TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code " + Environment.NewLine
            If settSelectGLInBalanceSheetPerforma Then
                BaseQry += " inner join TSPL_BALANCE_SHEET_PERFORMA_GL_MAIN on TSPL_BALANCE_SHEET_PERFORMA_GL_MAIN.Main_GL_Account=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account and TSPL_BALANCE_SHEET_PERFORMA.S_No=TSPL_BALANCE_SHEET_PERFORMA_GL_MAIN.SNo "
            End If
            BaseQry += " left outer join TSPL_FISCAL_YEAR_MASTER on convert(date, TSPL_FISCAL_YEAR_MASTER.Start_Date,106) <= '" + FromDate + "'  and  TSPL_FISCAL_YEAR_MASTER.End_Date >=  '" + FromDate + "'" + Environment.NewLine + _
            " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A'  and  TSPL_GL_ACCOUNTS.Account_Seg_Code7 in  (" + ChkLocationSegment + ") "
            If Not chkIncludeingOpeningEntry.Checked Then
                BaseQry += " and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + FromDate + "',103) "
            End If
            BaseQry += " and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)<= convert(date,'" + ToDate + "',103) " + Environment.NewLine
            If Not chkIndAS.Checked Then
                BaseQry += " and TSPL_JOURNAL_MASTER.ind_as=0"
            End If
            If Not chkIncludeingClosingEntry.Checked Then
                BaseQry += "and TSPL_JOURNAL_MASTER.Transaction_Type<>'X'"
            End If

            If txtmultAccountMainGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountMainGroup.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_ACCOUNT_Main_GROUPS.Account_Main_Group_Code in (" + clsCommon.GetMulcallString(txtmultAccountMainGroup.arrValueMember) + ")  "
            End If
             
            If txtmultAccountGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountGroup.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_ACCOUNT_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtmultAccountGroup.arrValueMember) + ")  "
            End If
             
            If txtMultGLMainAmountGroup.arrValueMember IsNot Nothing AndAlso txtMultGLMainAmountGroup.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account in (" + clsCommon.GetMulcallString(txtMultGLMainAmountGroup.arrValueMember) + ")  "
            End If
             
            If txtMultGLMainAmountGroup.arrValueMember IsNot Nothing AndAlso txtMultGLMainAmountGroup.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account in (" + clsCommon.GetMulcallString(txtMultGLMainAmountGroup.arrValueMember) + ")  "
            End If

            If txtmultAccountSubGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountSubGroup.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code in (" + clsCommon.GetMulcallString(txtmultAccountSubGroup.arrValueMember) + ")"
            End If

            'If clsCommon.myLen(strPAndLGroupCode) > 0 Then
            '    BaseQry += " Union all " + Environment.NewLine
            '    'BaseQry += " select TSPL_GL_ACCOUNTS.Account_Balance,TSPL_JOURNAL_DETAILS.Account_code,TSPL_GL_ACCOUNTS.Description as AccountName,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account as Main_GL_Account,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.main_gl_Account_desc as MainGLName,TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code as SubGrpCode,TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_desc as SubGrpdesc,TSPL_BALANCE_SHEET_PERFORMA.Group_Code as GrpCode,tspl_balance_sheet_performa.Group_Name as GrpName, Amount,TSPL_BALANCE_SHEET_PERFORMA.S_No,TSPL_BALANCE_SHEET_PERFORMA.Main_Particular,TSPL_BALANCE_SHEET_PERFORMA.type as typ,TSPL_BALANCE_SHEET_PERFORMA.Particular,TSPL_BALANCE_SHEET_PERFORMA.Type,TSPL_BALANCE_SHEET_PERFORMA.Note,TSPL_ACCOUNT_GROUPS.Account_Group_Code,TSPL_ACCOUNT_GROUPS.Account_Group_Desc " + strExtraColumn + Environment.NewLine
            '    BaseQry += "  select 1 as Position, '999' as Account_Balance, '999' as  Account_code,tspl_balance_sheet_performa.Group_Name as AccountName,'999' as Main_GL_Account,tspl_balance_sheet_performa.Group_Name as MainGLName,'999' as SubGrpCode, tspl_balance_sheet_performa.Group_Name as SubGrpdesc,TSPL_BALANCE_SHEET_PERFORMA.Group_Code as GrpCode,tspl_balance_sheet_performa.Group_Name as GrpName, Amount,TSPL_BALANCE_SHEET_PERFORMA.S_No,TSPL_BALANCE_SHEET_PERFORMA.Main_Particular,TSPL_BALANCE_SHEET_PERFORMA.type as typ,TSPL_BALANCE_SHEET_PERFORMA.Particular,TSPL_BALANCE_SHEET_PERFORMA.Type,TSPL_BALANCE_SHEET_PERFORMA.Note, '999' as  Account_Group_Code,tspl_balance_sheet_performa.Group_Name as  Account_Group_Desc " + strExtraColumn + Environment.NewLine
            '    BaseQry += " from TSPL_JOURNAL_DETAILS " _
            '    & " inner join TSPL_GL_ACCOUNTS as TabRollupName on TabRollupName.Account_Code= TSPL_JOURNAL_DETAILS.Account_Code inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code  inner join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.gl_main_code  inner join TSPL_ACCOUNT_Sub_GROUPS on TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.sub_Group_Code  inner join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_Sub_GROUPS.Account_Group_Code  inner join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_Main_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code  inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + _
            '     " left outer join TSPL_FISCAL_YEAR_MASTER on convert(date, TSPL_FISCAL_YEAR_MASTER.Start_Date,106) <= '" + FromDate + "'  and  TSPL_FISCAL_YEAR_MASTER.End_Date >=  '" + FromDate + "'"
            '    BaseQry += " inner join TSPL_BALANCE_SHEET_PERFORMA on TSPL_BALANCE_SHEET_PERFORMA.Group_Code= " + strPAndLGroupCode + "" + Environment.NewLine
            '    BaseQry += " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A'  and  TSPL_GL_ACCOUNTS.Account_Seg_Code7 in   (" + ChkLocationSegment + ")  and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)<= convert(date,'" + ToDate + "',103)  and TSPL_ACCOUNT_Main_GROUPS.group_type='Income Statement'" + Environment.NewLine

            '    If Not chkIncludeingOpeningEntry.Checked Then
            '        BaseQry += " and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + FromDate + "',103)"
            '    Else
            '        Dim FinYarStartDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select convert(varchar, Start_Date,103) from TSPL_FISCAL_YEAR_MASTER  where convert(date,'" + FromDate + "',103) between Start_Date and End_Date"))
            '        If clsCommon.myLen(FinYarStartDate) > 0 Then
            '            BaseQry += " and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + FinYarStartDate + "',103)"
            '        End If
            '    End If
            '    If Not chkIndAS.Checked Then
            '        BaseQry += " and TSPL_JOURNAL_MASTER.ind_as=0"
            '    End If
            '    If Not chkIncludeingClosingEntry.Checked Then
            '        BaseQry += "and TSPL_JOURNAL_MASTER.Transaction_Type<>'X'"
            '    End If

            '    If txtmultAccountMainGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountMainGroup.arrValueMember.Count > 0 Then
            '        BaseQry += "   and TSPL_BALANCE_SHEET_PERFORMA.Group_Code in  (" + clsCommon.GetMulcallString(txtmultAccountMainGroup.arrValueMember) + ")  "
            '    End If
            '    If txtmultAccountGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountGroup.arrValueMember.Count > 0 Then
            '        BaseQry += " and TSPL_ACCOUNT_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtmultAccountGroup.arrValueMember) + ")  "
            '    End If

            '    If txtMultGLMainAmountGroup.arrValueMember IsNot Nothing AndAlso txtMultGLMainAmountGroup.arrValueMember.Count > 0 Then
            '        BaseQry += " and TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account in (" + clsCommon.GetMulcallString(txtMultGLMainAmountGroup.arrValueMember) + ")  "
            '    End If
            '    If txtmultAccountSubGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountSubGroup.arrValueMember.Count > 0 Then
            '        BaseQry += " and TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code in  (" + clsCommon.GetMulcallString(txtmultAccountSubGroup.arrValueMember) + ")  "
            '    End If
            'End If

            If clsCommon.myLen(strPAndLGroupCode) > 0 Then
                BaseQry += " Union all " + Environment.NewLine
                'If clsCommon.myLen(strExtraColumn) > 0 Then
                '    strExtraColumnWithMax = " , Sum (" + strExtraColumnWithMax + ") * -1 as " + strExtraColumnWithMax
                'End If
                'BaseQry += " select TSPL_GL_ACCOUNTS.Account_Balance,TSPL_JOURNAL_DETAILS.Account_code,TSPL_GL_ACCOUNTS.Description as AccountName,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account as Main_GL_Account,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.main_gl_Account_desc as MainGLName,TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code as SubGrpCode,TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_desc as SubGrpdesc,TSPL_BALANCE_SHEET_PERFORMA.Group_Code as GrpCode,tspl_balance_sheet_performa.Group_Name as GrpName, Amount,TSPL_BALANCE_SHEET_PERFORMA.S_No,TSPL_BALANCE_SHEET_PERFORMA.Main_Particular,TSPL_BALANCE_SHEET_PERFORMA.type as typ,TSPL_BALANCE_SHEET_PERFORMA.Particular,TSPL_BALANCE_SHEET_PERFORMA.Type,TSPL_BALANCE_SHEET_PERFORMA.Note,TSPL_ACCOUNT_GROUPS.Account_Group_Code,TSPL_ACCOUNT_GROUPS.Account_Group_Desc " + strExtraColumn + Environment.NewLine
                BaseQry += " Select max(XXVVV.Position) as Position,XXVVV.Account_Balance,max(Account_code) as Account_code , max(AccountName) as AccountName, max(Main_GL_Account) as Main_GL_Account, max(MainGLName) as MainGLName, max(SubGrpCode) as SubGrpCode,max( SubGrpdesc) as SubGrpdesc, max(GrpCode) as GrpCode, max(GrpName) as GrpName, sum ( Amount) * -1 as Amount, max(S_No) as S_No, max (Main_Particular) as Main_Particular, max(typ) as typ,max(Particular) as Particular, max(Type) as Type , max(Note) as Note, max(Account_Group_Code) as Account_Group_Code , max(Account_Group_Desc) as Account_Group_Desc  " + strExtraColumnWithMax + " from ( "


                BaseQry += " select 1 as Position, TSPL_GL_ACCOUNTS.Account_Balance as Account_Balance, '999' as  Account_code,'Profit And Loss' as AccountName,'999' as Main_GL_Account,'Profit And Loss' as MainGLName,'999' as SubGrpCode, 'Profit And Loss' as SubGrpdesc,'999' as GrpCode,'Profit And Loss' as GrpName, Amount  as Amount ,999 as S_No,'Liability' as Main_Particular,TSPL_PROFIT_AND_LOSS_PERFORMA.type as typ,TSPL_PROFIT_AND_LOSS_PERFORMA.Particular,TSPL_PROFIT_AND_LOSS_PERFORMA.Type,TSPL_PROFIT_AND_LOSS_PERFORMA.Note, '999' as  Account_Group_Code,'Profit And Loss' as  Account_Group_Desc  " + strExtraColumn + Environment.NewLine
                BaseQry += " from TSPL_JOURNAL_DETAILS " & _
                           " inner join TSPL_GL_ACCOUNTS as TabRollupName on TabRollupName.Account_Code=  TSPL_JOURNAL_DETAILS.Account_Code inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code " & _
                           " inner join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.gl_main_code " & _
                           " inner join TSPL_ACCOUNT_Sub_GROUPS on TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.sub_Group_Code  " & _
                           " inner join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_Sub_GROUPS.Account_Group_Code  " & _
                           " inner join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_Main_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code  " & _
                           " inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No  " & _
                           " inner join TSPL_PROFIT_AND_LOSS_PERFORMA on TSPL_PROFIT_AND_LOSS_PERFORMA.Group_Code= TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code  inner join TSPL_PROFIT_AND_LOSS_PERFORMA_GL_MAIN on TSPL_PROFIT_AND_LOSS_PERFORMA_GL_MAIN.Main_GL_Account=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account  " & _
                           " and TSPL_PROFIT_AND_LOSS_PERFORMA.S_No=TSPL_PROFIT_AND_LOSS_PERFORMA_GL_MAIN.SNo  " & _
                           " left outer join TSPL_FISCAL_YEAR_MASTER on convert(date, TSPL_FISCAL_YEAR_MASTER.Start_Date,106) <= '" + FromDate + "'  and  TSPL_FISCAL_YEAR_MASTER.End_Date >=  '" + FromDate + "'"



                'BaseQry += " inner join TSPL_BALANCE_SHEET_PERFORMA on TSPL_BALANCE_SHEET_PERFORMA.Group_Code= " + strPAndLGroupCode + "" + Environment.NewLine
                BaseQry += " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A'  and  TSPL_GL_ACCOUNTS.Account_Seg_Code7 in   (" + ChkLocationSegment + ")   and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)<= convert(date,'" + ToDate + "',103)  " + Environment.NewLine

                'If Not chkIncludeingOpeningEntry.Checked Then
                BaseQry += " and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + FromDate + "',103)"
                'Else
                '    Dim FinYarStartDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select convert(varchar, Start_Date,103) from TSPL_FISCAL_YEAR_MASTER  where convert(date,'" + FromDate + "',103) between Start_Date and End_Date"))
                '    If clsCommon.myLen(FinYarStartDate) > 0 Then
                '        BaseQry += " and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + FinYarStartDate + "',103)"
                '    End If
                'End If
                If Not chkIndAS.Checked Then
                    BaseQry += " and TSPL_JOURNAL_MASTER.ind_as=0"
                End If
                If Not chkIncludeingClosingEntry.Checked Then
                    BaseQry += "and TSPL_JOURNAL_MASTER.Transaction_Type<>'X'"
                End If

                If txtmultAccountMainGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountMainGroup.arrValueMember.Count > 0 Then
                    'BaseQry += "   and TSPL_BALANCE_SHEET_PERFORMA.Group_Code in  (" + clsCommon.GetMulcallString(txtmultAccountMainGroup.arrValueMember) + ")  "
                    BaseQry += "   and TSPL_ACCOUNT_Main_GROUPS.Account_Main_Group_Code in  (" + clsCommon.GetMulcallString(txtmultAccountMainGroup.arrValueMember) + ")  "
                End If
                If txtmultAccountGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountGroup.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_ACCOUNT_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtmultAccountGroup.arrValueMember) + ")  "
                End If

                If txtMultGLMainAmountGroup.arrValueMember IsNot Nothing AndAlso txtMultGLMainAmountGroup.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account in (" + clsCommon.GetMulcallString(txtMultGLMainAmountGroup.arrValueMember) + ")  "
                End If
                If txtmultAccountSubGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountSubGroup.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code in  (" + clsCommon.GetMulcallString(txtmultAccountSubGroup.arrValueMember) + ")  "
                End If
                BaseQry += " ) XXVVV group by XXVVV.Account_Balance "
            End If
            Dim fnlQty As String = ""

            If rbtnNA.IsChecked Then
                fnlQty = "select MAX(S_No) as S_No, Main_Particular ,Particular ,GrpCode,(max(isnull(GrpName,''))) as GrpName,max(Note) as Note "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        '==============Changed by Rohit on May 28,2014 .if Account is Debit then Value will be Positive else Negetive.
                        'fnlQty += ",ABS( sum(" + strkey.Key + ")) as " + strkey.Key
                        fnlQty += ",sum(" + strkey.Key + ")* typ as " + strkey.Key
                        ' * case when Account_Balance='Debit' and typ=1 then 1 else -1 end)*(typ)* typ
                        '====================================================================================================
                    Next
                End If
                fnlQty += ", sum(Amount)* typ  as Amt ,(typ*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final  "
                fnlQty += " where 2 = 2"
                If clsCommon.myLen(ChkGroupCodeDD) > 0 Then
                    fnlQty += " and GrpCode = '" + ChkGroupCodeDD + "'"
                End If


                fnlQty += " group by Position,Main_Particular,typ,Particular,GrpCode order by Position,S_No"
            ElseIf rbtnGLGroupLevel.IsChecked Then 'ChkGLAccount.IsChecked Then
                fnlQty = "select MAX(S_No) as S_No, Account_Group_code as [Account Group Code] ,(max(isnull(Account_group_desc,''))) as [Account Group Desc],max(Note) as Note "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        '==============Changed by Rohit on May 28,2014 .if Account is Debit then Value will be Positive else Negetive.
                        'fnlQty += ",ABS( sum(" + strkey.Key + ")) as " + strkey.Key
                        fnlQty += ",sum(" + strkey.Key + ")* typ as " + strkey.Key
                        ' * case when Account_Balance='Debit' and typ=1 then 1 else -1 end)*(typ)* typ
                        '====================================================================================================
                    Next
                End If

                'fnlQty += ",ABS( sum(Amount) ) as Amt ,(max(Type)*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Main_Particular,typ,Particular,GrpCode,Account_Balance order by S_No"
                fnlQty += ", sum(Amount)* typ  as Amt ,(typ*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final"
                fnlQty += " where 2 = 2"
                If clsCommon.myLen(ChkGroupCodeDD) > 0 Then
                    fnlQty += " and GrpCode = '" + ChkGroupCodeDD + "'"
                End If
                fnlQty += " group by Position,Account_Group_Code,Typ  order by Position,S_No"
            ElseIf rbtnGLRollupLevel.IsChecked Then
                fnlQty = "select max(Account_Group_Code) as [Account Group Code],SubGrpCode as [Sub Group Code],max( SubGrpDesc ) as [Sub Grp Desc] "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ",sum(" + strkey.Key + ")* type as " + strkey.Key
                        ' fnlQty += ",ABS( sum(" + strkey.Key + ")) as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount) * type as Amt ,(type *  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final"
                fnlQty += " where 2 = 2"
                If clsCommon.myLen(ChkAccountGroupCodeDD) > 0 Then
                    fnlQty += " and Account_Group_Code = '" + ChkAccountGroupCodeDD + "'"
                End If
                fnlQty += " group by Position , SubGrpCode,type order by Position "
                'fnlQty += ",ABS( sum(Amount) ) as Amt ,(max(Type)*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by RollupCode "
            ElseIf ChkGLMainAccount.IsChecked Then
                fnlQty = "select max(SubGrpCode) as [Sub Group Code], Main_GL_Account as [Main GL Account],max( MainGLName) as [Main GL Name]"
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ", sum(" + strkey.Key + ") as " + strkey.Key
                        'fnlQty += ",ABS( sum(" + strkey.Key + ")) as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount)  as Amt ,(Type*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final"
                fnlQty += " where 2 = 2"
                If clsCommon.myLen(ChkSubGroupCodeDD) > 0 Then
                    fnlQty += " and SubGrpCode = '" + ChkSubGroupCodeDD + "'"
                End If
                fnlQty += "group by Position,Main_GL_Account,Type order by Position "
                'fnlQty += ",ABS( sum(Amount) ) as Amt ,(max(Type)*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Account_code "

            ElseIf ChkGLAccount.IsChecked Then
                fnlQty = "select max(Main_GL_Account) as [Main GL Account] ,Account_code,max( AccountName ) as AccountName "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ", sum(" + strkey.Key + ") as " + strkey.Key
                        'fnlQty += ",ABS( sum(" + strkey.Key + ")) as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount)  as Amt ,(Type*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final "
                fnlQty += " where 2 = 2"
                If clsCommon.myLen(ChkMainGLAccountDD) > 0 Then
                    fnlQty += " and Main_GL_Account = '" + ChkMainGLAccountDD + "'"
                End If
                fnlQty += " group by Position , Account_code,Type order by Position  "
                'fnlQty += ",ABS( sum(Amount) ) as Amt ,(max(Type)*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Account_code "

            End If

            dt = clsDBFuncationality.GetDataTable(fnlQty)
            If dt.Rows.Count <= 0 Then
                gv1.DataSource = Nothing
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
            SetGridFormation(arr)
            'LoadBlankGrid()
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '==================================================================================
    Sub LoadBlankGrid()
        Dim columnGroupsView As New ColumnGroupsViewDefinition()
        columnGroupsView.ColumnGroups.Add(New GridViewColumnGroup("General", "General"))
        columnGroupsView.ColumnGroups.Add(New GridViewColumnGroup("Details", "Details"))

        columnGroupsView.ColumnGroups("General").Rows.Add(New GridViewColumnGroupRow())
        columnGroupsView.ColumnGroups("General").Rows.Add(New GridViewColumnGroupRow())
        columnGroupsView.ColumnGroups("General").Rows(0).ColumnNames.Add(gv1.Columns("CustomerID").Name)
        columnGroupsView.ColumnGroups("General").Rows(0).ColumnNames.Add(gv1.Columns("ContactName").Name)
        columnGroupsView.ColumnGroups("General").Rows(1).ColumnNames.Add(gv1.Columns("CompanyName").Name)

        columnGroupsView.ColumnGroups("Details").Groups.Add(New GridViewColumnGroup("Address"))
        columnGroupsView.ColumnGroups("Details").Groups.Add(New GridViewColumnGroup())
        columnGroupsView.ColumnGroups("Details").Groups(0).Rows.Add(New GridViewColumnGroupRow())
        columnGroupsView.ColumnGroups("Details").Groups(0).Rows(0).ColumnNames.Add(gv1.Columns("City").Name)
        columnGroupsView.ColumnGroups("Details").Groups(0).Rows(0).ColumnNames.Add(gv1.Columns("Country").Name)
        columnGroupsView.ColumnGroups("Details").Groups(1).Rows.Add(New GridViewColumnGroupRow())
        columnGroupsView.ColumnGroups("Details").Groups(1).Rows(0).ColumnNames.Add(gv1.Columns("Phone").Name)
    End Sub

    Sub SetGridFormation(ByVal arr As Dictionary(Of String, clsTempBalanceSheet))
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        Dim view As New ColumnGroupsViewDefinition()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.DataSource = dt
        Dim summaryRowItem As New GridViewSummaryRowItem()




        If rbtnNA.IsChecked Then
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "GrpName"))
                view.ColumnGroups("GrpName").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("GrpName").Rows(0).ColumnNames.Add(gv1.Columns("GrpName").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "Note"))
                view.ColumnGroups("Note").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Note").Rows(0).ColumnNames.Add(gv1.Columns("Note").Name)
            End If
            gv1.Columns("S_No").IsVisible = False
            gv1.Columns("S_No").Width = 150
            gv1.Columns("S_No").HeaderText = "SNo"

            gv1.Columns("Main_Particular").IsVisible = False
            gv1.Columns("Main_Particular").Width = 150
            gv1.Columns("Main_Particular").HeaderText = " "

            gv1.Columns("Particular").IsVisible = False
            gv1.Columns("Particular").Width = 150
            gv1.Columns("Particular").HeaderText = " "

            gv1.Columns("GrpCode").IsVisible = False
            gv1.Columns("GrpCode").Width = 150
            gv1.Columns("GrpCode").HeaderText = "Account Group Code"

            gv1.Columns("GrpName").IsVisible = True
            gv1.Columns("GrpName").Width = 300
            gv1.Columns("GrpName").HeaderText = "Particular"

            gv1.Columns("Note").IsVisible = True
            gv1.Columns("Note").Width = 80
            gv1.Columns("Note").HeaderText = "Note"


        ElseIf rbtnGLGroupLevel.IsChecked Then
            '============Added By Rohit on May 28,2014 to Show RollUp Account name in Drill Down Report.it was not showing.==========
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "Account Group Code"))
                view.ColumnGroups("Account Group Code").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Account Group Code").Rows(0).ColumnNames.Add(gv1.Columns("Account Group Code").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "Account Group Desc"))
                view.ColumnGroups("Account Group Desc").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Account Group Desc").Rows(0).ColumnNames.Add(gv1.Columns("Account Group Desc").Name)
            End If
            '============================================================================================================
            gv1.Columns("Account Group Code").IsVisible = True
            gv1.Columns("Account Group Code").Width = 150
            gv1.Columns("Account Group Code").HeaderText = "Account Group Code"

            gv1.Columns("Account Group Desc").IsVisible = True
            gv1.Columns("Account Group Desc").Width = 150
            gv1.Columns("Account Group Desc").HeaderText = "Account Group Desc"
        ElseIf rbtnGLRollupLevel.IsChecked Then
            '============Added By Rohit on May 28,2014 to Show Account name in Drill Down Report.it was not showing.==========
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "Sub Group Code"))
                view.ColumnGroups("Sub Group Code").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Sub Group Code").Rows(0).ColumnNames.Add(gv1.Columns("Sub Group Code").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "Sub Grp Desc"))
                view.ColumnGroups("Sub Grp Desc").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Sub Grp Desc").Rows(0).ColumnNames.Add(gv1.Columns("Sub Grp Desc").Name)
            End If
            '=======================================================================================================================
            gv1.Columns("Sub Group Code").IsVisible = True
            gv1.Columns("Sub Group Code").Width = 150
            gv1.Columns("Sub Group Code").HeaderText = "Sub Group Code"

            gv1.Columns("Sub Grp Desc").IsVisible = True
            gv1.Columns("Sub Grp Desc").Width = 150
            gv1.Columns("Sub Grp Desc").HeaderText = "Sub Group Name"
        ElseIf ChkGLMainAccount.IsChecked Then
            '============Added By Rohit on May 28,2014 to Show Account name in Drill Down Report.it was not showing.==========
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "Main GL Account"))
                view.ColumnGroups("Main GL Account").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Main GL Account").Rows(0).ColumnNames.Add(gv1.Columns("Main GL Account").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "Main GL Name"))
                view.ColumnGroups("Main GL Name").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Main GL Name").Rows(0).ColumnNames.Add(gv1.Columns("Main GL Name").Name)
            End If
            '=======================================================================================================================
            gv1.Columns("Main GL Account").IsVisible = True
            gv1.Columns("Main GL Account").Width = 150
            gv1.Columns("Main GL Account").HeaderText = "GL Main Account Code"

            gv1.Columns("Main GL Name").IsVisible = True
            gv1.Columns("Main GL Name").Width = 150
            gv1.Columns("Main GL Name").HeaderText = "GL Main Account Desc"
        ElseIf ChkGLAccount.IsChecked Then
            '============Added By Rohit on May 28,2014 to Show Account name in Drill Down Report.it was not showing.==========
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "Account_Code"))
                view.ColumnGroups("Account_Code").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Account_Code").Rows(0).ColumnNames.Add(gv1.Columns("Account_Code").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "AccountName"))
                view.ColumnGroups("AccountName").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("AccountName").Rows(0).ColumnNames.Add(gv1.Columns("AccountName").Name)
            End If
            '=======================================================================================================================
            gv1.Columns("Account_Code").IsVisible = True
            gv1.Columns("Account_Code").Width = 150
            gv1.Columns("Account_Code").HeaderText = "Account Code"

            gv1.Columns("AccountName").IsVisible = True
            gv1.Columns("AccountName").Width = 400
            gv1.Columns("AccountName").HeaderText = "Account Name"
        End If


        'Dim strInterval As String = Nothing
        'For ii As Integer = 0 To gv1.Columns.Count - 1
        '    If arr.ContainsKey("[" + gv1.Columns(ii).Name + "]") Then
        '        gv1.Columns(ii).IsVisible = True
        '        gv1.Columns(ii).Width = 100
        '        gv1.Columns(ii).FormatString = "{0:F2}"
        '        If chkLocationWise.Checked Then
        '            gv1.Columns(ii).HeaderText = arr.Item("[" + gv1.Columns(ii).Name + "]").location
        '            If Not clsCommon.CompairString(strInterval, arr.Item("[" + gv1.Columns(ii).Name + "]").Interval) = CompairStringResult.Equal Then
        '                strInterval = arr.Item("[" + gv1.Columns(ii).Name + "]").Interval
        '                view.ColumnGroups.Add(New GridViewColumnGroup(strInterval, strInterval))
        '                view.ColumnGroups(strInterval).Rows.Add(New GridViewColumnGroupRow())
        '            End If
        '            view.ColumnGroups(strInterval).Rows(0).Columns.Add(gv1.Columns(ii))
        '            If gv1.Columns(ii).HeaderText = "" Then
        '                gv1.Columns(ii).HeaderText = "Total"
        '            End If
        '        End If
        '        Dim item As New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(item)
        '    End If
        'Next

        gv1.Columns("Amt").IsVisible = False
        gv1.Columns("Amt").Width = 150
        gv1.Columns("Amt").HeaderText = "Amount"
        gv1.Columns("Amt").FormatString = "{0:F2}"

        gv1.Columns("AmtActual").IsVisible = Not rbtnYearly.IsChecked
        gv1.Columns("AmtActual").Width = 150
        gv1.Columns("AmtActual").HeaderText = "Amount"
        gv1.Columns("AmtActual").FormatString = "{0:F2}"





        If rbtnNA.IsChecked Then
            gv1.GroupDescriptors.Add(New GridGroupByExpression("Main_Particular as Main_Particular format ""{0}: {1}"" Group By Main_Particular"))
            gv1.GroupDescriptors.Add(New GridGroupByExpression("Particular as Particular format ""{0}: {1}"" Group By Particular"))
            gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
        End If


        ViewDef(view, arr, summaryRowItem)
        Dim item1 As New GridViewSummaryItem("AmtActual", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        If chkLocationWise.Checked Then
            gv1.ViewDefinition = view
        End If
        gv1.ReadOnly = True
        EnableDisableControls(False)
    End Sub

    Sub ViewDef(ByVal view As ColumnGroupsViewDefinition, ByVal arr As Dictionary(Of String, clsTempBalanceSheet), ByVal summaryRowItem As GridViewSummaryRowItem)
        Dim strInterval As String = Nothing
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Columns.Count - 1
                If arr.ContainsKey("[" + gv1.Columns(ii).Name + "]") Then
                    gv1.Columns(ii).IsVisible = True
                    gv1.Columns(ii).Width = 100
                    gv1.Columns(ii).FormatString = "{0:F2}"
                    If chkLocationWise.Checked Then
                        gv1.Columns(ii).HeaderText = arr.Item("[" + gv1.Columns(ii).Name + "]").location
                        If Not clsCommon.CompairString(strInterval, arr.Item("[" + gv1.Columns(ii).Name + "]").Interval) = CompairStringResult.Equal Then
                            strInterval = arr.Item("[" + gv1.Columns(ii).Name + "]").Interval
                            view.ColumnGroups.Add(New GridViewColumnGroup(strInterval, strInterval))
                            view.ColumnGroups(strInterval).Rows.Add(New GridViewColumnGroupRow())
                        End If
                        view.ColumnGroups(strInterval).Rows(0).ColumnNames.Add(gv1.Columns(ii).Name)
                        If gv1.Columns(ii).HeaderText = "" Then
                            gv1.Columns(ii).HeaderText = "Total"
                        End If
                    End If
                    Dim item As New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                End If
            Next
        End If
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        grpLocaSegment.Enabled = Val
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        pnlDateRange.Enabled = Val
        pnlFiscalYear.Enabled = Val
        GroupBox1.Enabled = Val
        chkLocationWise.Enabled = Val
        GroupBox2.Enabled = Val
        RadGroupBox2.Enabled = Val
        RadGroupBox3.Enabled = Val
        chkIndAS.Enabled = Val
        chkIncludeingOpeningEntry.Enabled = Val
        chkIncludeingClosingEntry.Enabled = Val
    End Sub

    Private Sub FunExportToExcel(ByVal exporter As EnumExportTo)
        Try
            RefreshData()
            If dt.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strTemp As String = ""
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                'strTemp = ""
                'For Each Str As String In cbgLocSeg.CheckedDisplayMember
                '    If clsCommon.myLen(strTemp) > 0 Then
                '        strTemp += ", "
                '    End If
                '    strTemp += Str
                'Next
                'arrHeader.Add("Location Segment : " + strTemp)

                'strTemp = ""
                'For Each Str As String In cbgCompany.CheckedDisplayMember
                '    If clsCommon.myLen(strTemp) > 0 Then
                '        strTemp += ", "
                '    End If
                '    strTemp += Str
                'Next
                'arrHeader.Add("Company : " + strTemp)
                ' clsCommon.MyExportToExcel("Balance Sheet", gv1, arrHeader, Me.Text)


                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Balance Sheet", gv1, arrHeader, Me.Text, True)
                ElseIf exporter = EnumExportTo.PDF Then
                    clsCommon.MyExportToPDF("Balance Sheet", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If

            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)

        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUser
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUser
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

    Private Sub btnRestoreLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreLayout.Click
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUser), clsGridLayout)
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

    Private Sub btnDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteLayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUser)
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        FunExportToExcel(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        FunExportToExcel(EnumExportTo.PDF)
    End Sub

    Private Sub txtFiscalYear__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFiscalYear._MYValidating
        Dim qry As String = "select fiscal_code as Code,fiscal_Name as Name from TSPL_FISCAL_YEAR_MASTER"
        Dim WhrCls As String = ""
        txtFiscalYear.Value = clsCommon.ShowSelectForm("BalSheetFisYear", qry, "Code", WhrCls, txtFiscalYear.Value, "", isButtonClicked)
        lblFiscalYear.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select fiscal_Name from TSPL_FISCAL_YEAR_MASTER WHERE fiscal_code='" + txtFiscalYear.Value + "'"))
    End Sub

    Private Sub MyRadioButton1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnYearly.ToggleStateChanged, rbtnDateRange.ToggleStateChanged, rbtnMonthly.ToggleStateChanged, rbtnQuarterly.ToggleStateChanged, rbtnHalfYearly.ToggleStateChanged
        If rbtnDateRange.IsChecked Then
            pnlDateRange.Visible = True
            pnlFiscalYear.Visible = False
        Else
            pnlDateRange.Visible = False
            pnlFiscalYear.Visible = True
        End If
    End Sub


    Private Sub rbtnRollupAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnRollupAll.ToggleStateChanged, rbtnRollupSelect.ToggleStateChanged
        cbgRollupAC.Enabled = rbtnRollupSelect.IsChecked
    End Sub

    Private Sub ChkAccountMainGrpAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkAccountMainGrpAll.ToggleStateChanged, ChkAccountMainGrpSelect.ToggleStateChanged
        CbgAccountmainGrp.Enabled = ChkAccountMainGrpSelect.IsChecked
    End Sub

    Private Sub rbtnGLAccountAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnGLAccountGrpAll.ToggleStateChanged, rbtnGLAccountGroupSelect.ToggleStateChanged
        cbgGLACGrp.Enabled = rbtnGLAccountGroupSelect.IsChecked
    End Sub

    Private Sub rbtnGLMainAccountAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnGlMainAccountAll.ToggleStateChanged, rbtnGlMainAccountSelect.ToggleStateChanged
        cbgGLMainAccount.Enabled = rbtnGlMainAccountSelect.IsChecked
    End Sub


    Public ReportLevel As Integer = 0
    Public strCurrentGrp As String = Nothing
    Public strCurrentRollupCode As String = Nothing
    Public arrGlgroup As ArrayList = Nothing
    Public arrGlgroupRollUp As ArrayList = Nothing

    'Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
    '    Try
    '        If rbtnNA.IsChecked Then
    '            If clsCommon.myLen(gv1.CurrentRow.Cells("GrpCode").Value) > 0 Then
    '                Dim frm As New frmRptBalanceSheet
    '                frm.ReportLevel = 1
    '                frm.rbtnYearly.IsChecked = Me.rbtnYearly.IsChecked
    '                frm.rbtnHalfYearly.IsChecked = Me.rbtnHalfYearly.IsChecked
    '                frm.rbtnQuarterly.IsChecked = Me.rbtnQuarterly.IsChecked
    '                frm.rbtnMonthly.IsChecked = Me.rbtnMonthly.IsChecked
    '                frm.rbtnDateRange.IsChecked = Me.rbtnDateRange.IsChecked
    '                frm.chkLocationWise.IsChecked = Me.chkLocationWise.IsChecked
    '                frm.txtFromDate.Value = Me.txtFromDate.Value
    '                frm.txtToDate.Value = Me.txtToDate.Value
    '                frm.txtFiscalYear.Value = Me.txtFiscalYear.Value
    '                frm.lblFiscalYear.Text = Me.lblFiscalYear.Text
    '                frm.strCurrentGrp = clsCommon.myCstr(gv1.CurrentRow.Cells("GrpCode").Value)
    '                frm.arrGlgroup = New ArrayList()
    '                frm.ChkGroupCodeDD = clsCommon.myCstr(gv1.CurrentRow.Cells("GrpCode").Value)
    '                If txtmultLocationSegment.arrValueMember IsNot Nothing AndAlso txtmultLocationSegment.arrValueMember.Count > 0 Then
    '                    For Each row As String In txtmultLocationSegment.arrValueMember
    '                        frm.arrGlgroup.Add(row)
    '                    Next
    '                End If
    '                frm.Show()
    '            End If
    '        ElseIf rbtnGLGroupLevel.IsChecked Then
    '            If clsCommon.myLen(gv1.CurrentRow.Cells("Account Group Code").Value) > 0 Then
    '                Dim frm As New frmRptBalanceSheet
    '                frm.ReportLevel = 2
    '                frm.rbtnYearly.IsChecked = Me.rbtnYearly.IsChecked
    '                frm.rbtnHalfYearly.IsChecked = Me.rbtnHalfYearly.IsChecked
    '                frm.rbtnQuarterly.IsChecked = Me.rbtnQuarterly.IsChecked
    '                frm.rbtnMonthly.IsChecked = Me.rbtnMonthly.IsChecked
    '                frm.rbtnDateRange.IsChecked = Me.rbtnDateRange.IsChecked
    '                frm.chkLocationWise.IsChecked = Me.chkLocationWise.IsChecked
    '                frm.txtFromDate.Value = Me.txtFromDate.Value
    '                frm.txtToDate.Value = Me.txtToDate.Value
    '                frm.txtFiscalYear.Value = Me.txtFiscalYear.Value
    '                frm.lblFiscalYear.Text = Me.lblFiscalYear.Text
    '                frm.strCurrentGrp = clsCommon.myCstr(gv1.CurrentRow.Cells("Account Group Code").Value)
    '                frm.ChkAccountGroupCodeDD = clsCommon.myCstr(gv1.CurrentRow.Cells("Account Group Code").Value)
    '                frm.arrGlgroup = New ArrayList()
    '                If txtmultLocationSegment.arrValueMember IsNot Nothing AndAlso txtmultLocationSegment.arrValueMember.Count > 0 Then
    '                    For Each row As String In txtmultLocationSegment.arrValueMember
    '                        frm.arrGlgroup.Add(row)
    '                    Next
    '                End If
    '                frm.Show()
    '            End If
    '        ElseIf rbtnGLRollupLevel.IsChecked Then
    '            If clsCommon.myLen(gv1.CurrentRow.Cells("Sub Group Code").Value) > 0 Then
    '                Dim frm As New frmRptBalanceSheet
    '                frm.ReportLevel = 3
    '                frm.rbtnYearly.IsChecked = Me.rbtnYearly.IsChecked
    '                frm.rbtnHalfYearly.IsChecked = Me.rbtnHalfYearly.IsChecked
    '                frm.rbtnQuarterly.IsChecked = Me.rbtnQuarterly.IsChecked
    '                frm.rbtnMonthly.IsChecked = Me.rbtnMonthly.IsChecked
    '                frm.rbtnDateRange.IsChecked = Me.rbtnDateRange.IsChecked
    '                frm.chkLocationWise.IsChecked = Me.chkLocationWise.IsChecked
    '                frm.txtFromDate.Value = Me.txtFromDate.Value
    '                frm.txtToDate.Value = Me.txtToDate.Value
    '                frm.txtFiscalYear.Value = Me.txtFiscalYear.Value
    '                frm.lblFiscalYear.Text = Me.lblFiscalYear.Text
    '                frm.strCurrentRollupCode = clsCommon.myCstr(gv1.CurrentRow.Cells("Sub Group Code").Value)
    '                frm.ChkSubGroupCodeDD = clsCommon.myCstr(gv1.CurrentRow.Cells("Sub Group Code").Value)
    '                frm.arrGlgroupRollUp = New ArrayList()
    '                If txtmultLocationSegment.arrValueMember IsNot Nothing AndAlso txtmultLocationSegment.arrValueMember.Count > 0 Then
    '                    For Each row As String In txtmultLocationSegment.arrValueMember
    '                        frm.arrGlgroupRollUp.Add(row)
    '                    Next
    '                End If
    '                frm.Show()
    '            End If
    '        ElseIf ChkGLMainAccount.IsChecked Then
    '            If clsCommon.myLen(gv1.CurrentRow.Cells("Main GL Account").Value) > 0 Then
    '                Dim frm As New frmRptBalanceSheet
    '                frm.ReportLevel = 4
    '                frm.rbtnYearly.IsChecked = Me.rbtnYearly.IsChecked
    '                frm.rbtnHalfYearly.IsChecked = Me.rbtnHalfYearly.IsChecked
    '                frm.rbtnQuarterly.IsChecked = Me.rbtnQuarterly.IsChecked
    '                frm.rbtnMonthly.IsChecked = Me.rbtnMonthly.IsChecked
    '                frm.rbtnDateRange.IsChecked = Me.rbtnDateRange.IsChecked
    '                frm.chkLocationWise.IsChecked = Me.chkLocationWise.IsChecked
    '                frm.txtFromDate.Value = Me.txtFromDate.Value
    '                frm.txtToDate.Value = Me.txtToDate.Value
    '                frm.txtFiscalYear.Value = Me.txtFiscalYear.Value
    '                frm.lblFiscalYear.Text = Me.lblFiscalYear.Text
    '                frm.strCurrentRollupCode = clsCommon.myCstr(gv1.CurrentRow.Cells("Main GL Account").Value)
    '                frm.ChkMainGLAccountDD = clsCommon.myCstr(gv1.CurrentRow.Cells("Main GL Account").Value)
    '                frm.arrGlgroupRollUp = New ArrayList()
    '                If txtmultLocationSegment.arrValueMember IsNot Nothing AndAlso txtmultLocationSegment.arrValueMember.Count > 0 Then
    '                    For Each row As String In txtmultLocationSegment.arrValueMember
    '                        frm.arrGlgroupRollUp.Add(row)
    '                    Next
    '                End If
    '                frm.Show()
    '            End If
    '        ElseIf ChkGLAccount.IsChecked Then
    '            '=========Added by Rohit on May 28,2014 to show general ledger on drill down if rollup level is checked.
    '            Dim col As String = gv1.CurrentColumn.HeaderText
    '            Dim strACode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Code").Value)
    '            If clsCommon.myLen(strACode) > 0 Then
    '                Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
    '                frm.strPrevFormACode = strACode
    '                frm.strPrevFormAName = clsCommon.myCstr(gv1.CurrentRow.Cells("AccountName").Value)
    '                frm.dTPrevFormFromDate = txtFromDate.Value
    '                frm.dTPrevFormToDate = txtToDate.Value
    '                frm.RadLabel7.Visible = True
    '                frm.txtFromDate.Visible = True
    '                'frm.RadLabel9.Text = "To Date"
    '                frm.MyLabel2.Text = "To Date"
    '                Dim i As Integer = 0

    '                frm.arrLocSeg = New ArrayList()
    '                If txtmultLocationSegment.arrValueMember IsNot Nothing AndAlso txtmultLocationSegment.arrValueMember.Count > 0 Then
    '                    frm.arrLocSeg = cbgLocSeg.CheckedValue
    '                End If
    '                frm.arrAcc = New ArrayList()
    '                Dim stracccode As String = ""
    '                For Each acccode As DataRow In dtAccount.Select("Account_Group_Code='" & strACode & "' and Account_Code like '%" & col.ToString & "%'")
    '                    frm.arrAcc.Add(acccode("Account_Code")) 'stracccode = IIf(stracccode = "", acccode("Account_Code"), stracccode & "," & acccode("Account_Code"))
    '                Next
    '                'frm.arrAcc.Add(stracccode) 'frm.arrAcc.Add(strACode)
    '                frm.arrAcc.Add(strACode)
    '                'frm.chkAccAll.IsChecked = True

    '                'If chkAccSelect.IsChecked AndAlso cbgAccount.CheckedValue.Count > 0 Then
    '                '    frm.arrAcc = cbgAccount.CheckedValue
    '                'End If
    '                If cbgGLACGrp.CheckedValue.Count <= 0 Then
    '                    rbtnGLAccountGrpAll.IsChecked = True
    '                End If
    '                frm.Show()
    '            End If
    '            '==================================================================================================
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Sub DrillDown()
        Try
            If rbtnNA.IsChecked Then
                If clsCommon.myLen(gv1.CurrentRow.Cells("GrpCode").Value) > 0 Then

                    If Not arrBack.Contains("Level1") Then
                        arrBack.Add("Level1")
                    End If
                    rbtnGLGroupLevel.IsChecked = True
                    arrLocalSegment = New ArrayList()
                    arrLocalSegment = txtmultLocationSegment.arrValueMember
                    txtmultLocationSegment.arrValueMember = arrLocalSegment
                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("GrpCode").Value))
                    txtmultAccountMainGroup.arrValueMember = tmp
                    RefreshData()
                End If
            ElseIf rbtnGLGroupLevel.IsChecked Then
                If clsCommon.myLen(gv1.CurrentRow.Cells("Account Group Code").Value) > 0 Then
                    If Not arrBack.Contains("Level2") Then
                        arrBack.Add("Level2")
                    End If
                    rbtnGLRollupLevel.IsChecked = True
                    arrGLAccountGroup = New ArrayList()
                    arrGLAccountGroup = txtmultAccountMainGroup.arrValueMember
                    txtmultAccountMainGroup.arrValueMember = arrGLAccountGroup
                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Account Group Code").Value))
                    txtmultAccountGroup.arrValueMember = tmp
                    RefreshData()
                End If
            ElseIf rbtnGLRollupLevel.IsChecked Then
                If clsCommon.myLen(gv1.CurrentRow.Cells("Sub Group Code").Value) > 0 Then
                    If Not arrBack.Contains("Level3") Then
                        arrBack.Add("Level3")
                    End If

                    ChkGLMainAccount.IsChecked = True
                    arrGLMainAccountGroup = New ArrayList()
                    arrGLMainAccountGroup = txtmultAccountSubGroup.arrValueMember

                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Sub Group Code").Value))
                    txtmultAccountSubGroup.arrValueMember = tmp
                    RefreshData()
                End If
            ElseIf ChkGLMainAccount.IsChecked Then
                If clsCommon.myLen(gv1.CurrentRow.Cells("Main GL Account").Value) > 0 Then
                    If Not arrBack.Contains("Level4") Then
                        arrBack.Add("Level4")
                    End If

                    ChkGLAccount.IsChecked = True
                    arrGLMainAccountGroup = New ArrayList()
                    arrGLMainAccountGroup = txtMultGLMainAmountGroup.arrValueMember

                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Main GL Account").Value))
                    txtMultGLMainAmountGroup.arrValueMember = tmp
                    RefreshData()
                End If
            ElseIf ChkGLAccount.IsChecked Then
                '=========Added by Rohit on May 28,2014 to show general ledger on drill down if rollup level is checked.
                Dim col As String = gv1.CurrentColumn.HeaderText
                Dim strACode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Code").Value)
                If clsCommon.myLen(strACode) > 0 Then
                    Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    frm.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
                    frm.strPrevFormACode = strACode
                    frm.strPrevFormAName = clsCommon.myCstr(gv1.CurrentRow.Cells("AccountName").Value)
                    frm.dTPrevFormFromDate = txtFromDate.Value
                    frm.dTPrevFormToDate = txtToDate.Value
                    frm.RadLabel7.Visible = True
                    frm.txtFromDate.Visible = True
                    'frm.RadLabel9.Text = "To Date"
                    frm.MyLabel2.Text = "To Date"
                    frm.IsIncludeClosingEntry = chkIncludeingClosingEntry.Checked ''BHA/26/09/19-000929 by balwinder on 03/10/2019
                    frm.boolPrevWithoutOpening = Not chkIncludeingOpeningEntry.Checked
                    Dim i As Integer = 0

                    frm.arrLocSeg = New ArrayList()
                    If txtmultLocationSegment.arrValueMember IsNot Nothing AndAlso txtmultLocationSegment.arrValueMember.Count > 0 Then
                        frm.arrLocSeg = cbgLocSeg.CheckedValue
                    End If
                    frm.arrAcc = New ArrayList()
                    Dim stracccode As String = ""
                    For Each acccode As DataRow In dtAccount.Select("Account_Group_Code='" & strACode & "' and Account_Code like '%" & col.ToString & "%'")
                        frm.arrAcc.Add(acccode("Account_Code")) 'stracccode = IIf(stracccode = "", acccode("Account_Code"), stracccode & "," & acccode("Account_Code"))
                    Next
                    'frm.arrAcc.Add(stracccode) 'frm.arrAcc.Add(strACode)
                    frm.arrAcc.Add(strACode)
                    'frm.chkAccAll.IsChecked = True

                    'If chkAccSelect.IsChecked AndAlso cbgAccount.CheckedValue.Count > 0 Then
                    '    frm.arrAcc = cbgAccount.CheckedValue
                    'End If
                    ' txtmultLocationSegment.arrValueMember IsNot Nothing AndAlso
                    If txtmultAccountGroup.arrValueMember IsNot Nothing AndAlso txtmultAccountGroup.arrValueMember.Count <= 0 Then '--cbgGLACGrp.CheckedValue.Count <= 0 Then
                        rbtnGLAccountGrpAll.IsChecked = True
                    End If
                    frm.Show()
                End If


                '==================================================================================================
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If rbtnGLGroupLevel.IsChecked Then
                arrBack.Remove("Level1")
                txtmultAccountMainGroup.arrValueMember = arrGLMainAccountGroup
                rbtnNA.IsChecked = True
                RefreshData()

            ElseIf rbtnGLRollupLevel.IsChecked Then
                arrBack.Remove("Level2")
                txtmultAccountGroup.arrValueMember = arrGLMainAccountGroup
                rbtnGLGroupLevel.IsChecked = True
                RefreshData()
            ElseIf ChkGLMainAccount.IsChecked Then
                arrBack.Remove("Level3")
                txtmultAccountSubGroup.arrValueMember = arrGLMainAccountGroup
                rbtnGLRollupLevel.IsChecked = True
                RefreshData()
            ElseIf ChkGLAccount.IsChecked Then
                arrBack.Remove("Level4")
                txtMultGLMainAmountGroup.arrValueMember = arrGLMainAccountGroup
                ChkGLMainAccount.IsChecked = True
                RefreshData()
            ElseIf rbtnNA.IsChecked Then
                txtmultLocationSegment.arrValueMember = arrGLMainAccountGroup
                RadPageView1.SelectedPage = RadPageViewPage1
                'RefreshData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        DrillDown()
    End Sub
    Private Sub txtmultLocationSegment__My_Click(sender As Object, e As EventArgs) Handles txtmultLocationSegment._My_Click
        'StrQry = "SELECT Comp_Code as Code,Comp_Name as Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"

        StrQry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        StrQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        StrQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        StrQry += " order by xxx.Loc_Segment_Code"
        txtmultLocationSegment.arrValueMember = clsCommon.ShowMultipleSelectForm("multLocationSegment", StrQry, "Code", "Name", txtmultLocationSegment.arrValueMember, txtmultLocationSegment.arrDispalyMember)
    End Sub

    Private Sub txtmultAccountMainGroup__My_Click(sender As Object, e As EventArgs) Handles txtmultAccountMainGroup._My_Click
        StrQry = "select Account_Main_Group_Code as Code,Account_Main_Group_Desc as Name from TSPL_ACCOUNT_main_GROUPS"
        txtmultAccountMainGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("multAccountMainGroup", StrQry, "Code", "Name", txtmultAccountMainGroup.arrValueMember, txtmultAccountMainGroup.arrDispalyMember)
    End Sub

    Private Sub txtmultAccountGroup__My_Click(sender As Object, e As EventArgs) Handles txtmultAccountGroup._My_Click
        StrQry = "select Account_Group_Code as Code,Account_Group_Desc as Name from TSPL_ACCOUNT_GROUPS"
        txtmultAccountGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("multAccountGroup", StrQry, "Code", "Name", txtmultAccountGroup.arrValueMember, txtmultAccountGroup.arrDispalyMember)
        '============Created By Rohit on May 28,2014 to Add Accounts in general ledger Array.==========
        dtAccount = clsDBFuncationality.GetDataTable("select * from TSPL_Gl_Accounts") ' where ControlAccount='Y'
        '========================================================================================
    End Sub

    Private Sub txtmultAccountSubGroup__My_Click(sender As Object, e As EventArgs) Handles txtmultAccountSubGroup._My_Click
        StrQry = "select Account_Sub_Group_Code as Code,Account_Sub_Group_Desc as Name from TSPL_ACCOUNT_Sub_GROUPS"
        txtmultAccountSubGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("multAccountSubGroup", StrQry, "Code", "Name", txtmultAccountSubGroup.arrValueMember, txtmultAccountSubGroup.arrDispalyMember)
    End Sub

    Private Sub txtMultGLMainAmountGroup__My_Click(sender As Object, e As EventArgs) Handles txtMultGLMainAmountGroup._My_Click
        StrQry = "select Main_GL_Account as Code,Main_GL_Account_Desc as Name from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
        txtMultGLMainAmountGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("MultGLMainAmountGrou", StrQry, "Code", "Name", txtMultGLMainAmountGroup.arrValueMember, txtMultGLMainAmountGroup.arrDispalyMember)
    End Sub

    Private Sub btnGridPDF_Click(sender As Object, e As EventArgs) Handles btnGridPDF.Click
        Try
            Dim doc As New RadPrintDocument()
            doc.HeaderHeight = 30
            doc.HeaderFont = New Font("Arial", 15)
            doc.DocumentName = "BalanceSheet"

            doc.MiddleHeader = clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBalanceSheet & "'")

            doc.AssociatedObject = gv1
            doc.Print()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

Public Class clsTempBalanceSheet
    Public Interval As String = Nothing
    Public location As String = Nothing
    Public FullName As String = Nothing
    Public ColumnName As String = Nothing
End Class
