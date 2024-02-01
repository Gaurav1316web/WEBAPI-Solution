Imports common
Imports System.IO

Public Class frmRptBalanceSheetFormula
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim StrQry As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim ReportID As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private tableView As TableViewDefinition
    Dim dt As DataTable = Nothing
    Dim DtGroup As DataTable = Nothing
    Dim dtAccount As DataTable = Nothing
    Dim arrBack As New List(Of String)
    Public arrLocalSegment As ArrayList
    Public arrGLAccountGroup As ArrayList
    Public arrGLMainAccountGroup As ArrayList
    Dim settSelectGLInCashFlowPerforma As Boolean = False
    Public ReportLevel As Integer = 0
    Public strCurrentGrp As String = Nothing
    Public strCurrentRollupCode As String = Nothing
    Public arrGlgroup As ArrayList = Nothing
    Public arrGlgroupRollUp As ArrayList = Nothing
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
        btnRefresh.Visible = MyBase.isPrintFlag
    End Sub

    Sub LoadGLMainAC()
        Dim qry As String = "select Main_GL_Account as Code,Main_GL_Account_desc as Name from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
        cbgGLMainAccount.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgGLMainAccount.ValueMember = "Code"
        cbgGLMainAccount.DisplayMember = "Name"
    End Sub

    Sub LoadGLMainACGroup()
        Dim qry As String = "select Account_Main_Group_Code as Code,Account_Main_Group_Desc as Name from TSPL_ACCOUNT_main_GROUPS"
        CbgAccountmainGrp.DataSource = clsDBFuncationality.GetDataTable(qry)
        CbgAccountmainGrp.ValueMember = "Code"
        CbgAccountmainGrp.DisplayMember = "Name"
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReportID = Me.Form_ID
        settSelectGLInCashFlowPerforma = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SelectGLInCashFlowPerforma, clsFixedParameterCode.SelectGLInCashFlowPerforma, Nothing)) > 0)
        SetUserMgmtNew()
        arrBack = New List(Of String)
        tableView = CType(Me.gv1.ViewDefinition, TableViewDefinition)
        RadPageView1.SelectedPage = RadPageViewPage1

        LoadLocatinSegment()

        cbgLocSeg.CheckedAll()

        LoadLocatinSegment()
        LoadGLMainACGroup()
        cbgLocSeg.CheckedAll()

        LoadRollupAC()
        LoadGLACGroup()
        LoadGLMainAC()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+R Adding New Trasnaction")

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
        Dim qry As String = "select Account_Sub_Group_Code as Code,Account_Sub_Group_Desc as Name from TSPL_ACCOUNT_Sub_GROUPS"
        cbgRollupAC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRollupAC.ValueMember = "Code"
        cbgRollupAC.DisplayMember = "Name"
    End Sub

    Sub LoadGLACGroup()
        Dim qry As String = "select Account_Group_Code as Code,Account_Group_Desc as Name from TSPL_ACCOUNT_GROUPS"
        cbgGLACGrp.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgGLACGrp.ValueMember = "Code"
        cbgGLACGrp.DisplayMember = "Name"
        dtAccount = clsDBFuncationality.GetDataTable("select * from TSPL_Gl_Accounts")
    End Sub

    Private Sub frmRptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            RefreshData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E Then
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


        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = ReportID
        RefreshData()
    End Sub

    Public Sub RefreshData()
        Try
            gv1.EnableFiltering = True
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
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
                            strExtraColumn += ",( Amount * case when convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(dtStartDate, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "dd/MMM/yyyy") + "',103) and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + StrLoc + "' then 1 else 0 end ) "
                            If rbtnMonthly.IsChecked Then
                                objTempBC.Interval = clsCommon.GetPrintDate(dtStartDate, "MMM-yy")
                                objTempBC.location = StrLoc
                                objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " (" + StrLoc + ")]"
                                objTempBC.ColumnName = clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " (" + StrLoc + ")"
                                arr.Add(objTempBC.FullName, objTempBC)
                            Else
                                objTempBC.Interval = clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy")
                                objTempBC.location = StrLoc
                                objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + " (" + StrLoc + ")]"
                                objTempBC.ColumnName = clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + " (" + StrLoc + ")"
                                arr.Add(objTempBC.FullName, objTempBC)
                            End If
                            strExtraColumn += " as  " + objTempBC.FullName
                        Next
                    End If

                    objTempBC = New clsTempBalanceSheet

                    strExtraColumn += ",( Amount * case when convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(dtStartDate, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "dd/MMM/yyyy") + "',103) then 1 else 0 end ) "
                    If rbtnMonthly.IsChecked Then
                        objTempBC.Interval = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + "]"
                        objTempBC.location = ""
                        objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + "]"
                        objTempBC.ColumnName = clsCommon.GetPrintDate(dtStartDate, "MMM-yy")
                        arr.Add(objTempBC.FullName, objTempBC)
                    Else
                        objTempBC.Interval = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + "]"
                        objTempBC.location = ""
                        objTempBC.FullName = "[" + clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy") + "]"
                        objTempBC.ColumnName = clsCommon.GetPrintDate(dtStartDate, "MMM-yy") + " To " + clsCommon.GetPrintDate(tempEndDate.AddDays(-1), "MMM-yy")
                        arr.Add(objTempBC.FullName, objTempBC)
                    End If
                    strExtraColumn += " as  " + objTempBC.FullName

                    dtStartDate = tempEndDate
                End While
            End If

            Dim FromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim ToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim BaseQry As String = " select TSPL_GL_ACCOUNTS.Account_Balance,TSPL_JOURNAL_DETAILS.Account_code,TSPL_GL_ACCOUNTS.Description as AccountName,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account as Main_GL_Account,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.main_gl_Account_desc as MainGLName,TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code as SubGrpCode,TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_desc as SubGrpdesc,TSPL_BALANCE_SHEET_PERFORMA_FORMULA.Group_Code as GrpCode,TSPL_BALANCE_SHEET_PERFORMA_FORMULA.Group_Name as GrpName, Amount,TSPL_BALANCE_SHEET_PERFORMA_FORMULA.S_No,TSPL_BALANCE_SHEET_PERFORMA_FORMULA.Main_Particular,TSPL_BALANCE_SHEET_PERFORMA_FORMULA.type as typ,TSPL_BALANCE_SHEET_PERFORMA_FORMULA.Particular,TSPL_BALANCE_SHEET_PERFORMA_FORMULA.Type,TSPL_BALANCE_SHEET_PERFORMA_FORMULA.Note,TSPL_ACCOUNT_GROUPS.Account_Group_Code,TSPL_ACCOUNT_GROUPS.Account_Group_Desc " & strExtraColumn & " from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
                " inner join TSPL_GL_ACCOUNTS as TabRollupName on TabRollupName.Account_Code=" + Environment.NewLine + _
                                     " TSPL_JOURNAL_DETAILS.Account_Code inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code " + Environment.NewLine + _
                                     " inner join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.gl_main_code " + Environment.NewLine + _
                                     " inner join TSPL_ACCOUNT_Sub_GROUPS on TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.sub_Group_Code " + Environment.NewLine + _
                                     " inner join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_Sub_GROUPS.Account_Group_Code " + Environment.NewLine + _
                                     " inner join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_Main_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code " + Environment.NewLine + _
                                     " inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine + _
                                     " inner join TSPL_BALANCE_SHEET_PERFORMA_FORMULA on TSPL_BALANCE_SHEET_PERFORMA_FORMULA.Group_Code= TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code "
            If settSelectGLInCashFlowPerforma Then
                BaseQry += " inner join TSPL_BALANCE_SHEET_PERFORMA_FORMULA_GL_MAIN on TSPL_BALANCE_SHEET_PERFORMA_FORMULA_GL_MAIN.Main_GL_Account=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account and TSPL_BALANCE_SHEET_PERFORMA_FORMULA.S_No=TSPL_BALANCE_SHEET_PERFORMA_FORMULA_GL_MAIN.SNo "
            End If
            BaseQry += " left outer join TSPL_FISCAL_YEAR_MASTER on convert(date, TSPL_FISCAL_YEAR_MASTER.Start_Date,106) <= '" + FromDate + "'  and  TSPL_FISCAL_YEAR_MASTER.End_Date >=  '" + FromDate + "'" + Environment.NewLine


            BaseQry += " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A'  and  TSPL_GL_ACCOUNTS.Account_Seg_Code7 in  (" + ChkLocationSegment + ") and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_JOURNAL_DETAILS.Voucher_Date,103)<= convert(date,'" + ToDate + "',103) " + Environment.NewLine
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
                BaseQry += " and  TSPL_ACCOUNT_GROUPS.Account_Group_Code in (" + clsCommon.GetMulcallString(txtmultAccountGroup.arrValueMember) + ")  "
            End If
            If txtMultGLMainAmountGroup.arrValueMember IsNot Nothing AndAlso txtMultGLMainAmountGroup.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account in (" + clsCommon.GetMulcallString(txtMultGLMainAmountGroup.arrValueMember) + ")  "
            End If
            If ChkGLMainAccount.IsChecked Then
                BaseQry += " and TSPL_ACCOUNT_Sub_GROUPS.Account_Sub_Group_Code in (" + clsCommon.GetMulcallString(txtmultAccountSubGroup.arrValueMember) + ")"
            End If

            Dim fnlQty As String = ""

            If rbtnNA.IsChecked OrElse rbtnTreeView.IsChecked Then
                fnlQty = "select Account_Balance,MAX(S_No) as S_No, Main_Particular ,Particular ,max(GrpCode) as GrpCode,(max(isnull(GrpName,''))) as GrpName,max(Note) as Note "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ",sum(" + strkey.Key + ")* typ as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount)* typ  as Amt ,(typ*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Main_Particular,typ,Particular,Account_Balance order by S_No"
            ElseIf rbtnGLGroupLevel.IsChecked Then
                fnlQty = "select Account_Balance,MAX(S_No) as S_No, Account_Group_code as [Account Group Code] ,(max(isnull(Account_group_desc,''))) as [Account Group Desc],max(Note) as Note "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ",sum(" + strkey.Key + ")* typ as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount)* typ  as Amt ,(typ*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Account_Group_Code,Typ,Account_Balance  order by S_No"
            ElseIf rbtnGLRollupLevel.IsChecked Then
                fnlQty = "select SubGrpCode as [Sub Group Code],max( SubGrpDesc ) as [Sub Grp Desc] "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ",sum(" + strkey.Key + ")* type as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount) * type as Amt ,(type *  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by SubGrpCode,type "
            ElseIf ChkGLMainAccount.IsChecked Then
                fnlQty = "select Main_GL_Account as [Main GL Account],max( MainGLName) as [Main GL Name]"
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ", sum(" + strkey.Key + ") as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount)  as Amt ,(Type*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Main_GL_Account,Type "
            ElseIf ChkGLAccount.IsChecked Then
                fnlQty = "select Account_code,max( AccountName ) as AccountName "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ", sum(" + strkey.Key + ") as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount)  as Amt ,(Type*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Account_code,Type "
            End If
            Dim sQuery As String = ""
            dt = clsDBFuncationality.GetDataTable(fnlQty)
            dt.Columns.Add("IsFormula")
            If rbtnNA.IsChecked OrElse rbtnTreeView.IsChecked Then
                sQuery = "select palp.S_No,palp.Main_Particular,palp.Particular,GrpCode,GrpName,case when palp.Formula='' then null else Formula end as Formula,subqry.* from " _
                       & " TSPL_BALANCE_SHEET_PERFORMA_FORMULA palp Left join(" + Replace(fnlQty, "order by S_No", "") + ") subqry on subqry.s_no=palp.s_no order by palp.s_no"
                Dim dtTotal As DataTable = clsDBFuncationality.GetDataTable(sQuery)

                For Each dr As DataRow In dtTotal.Select("Formula is not null", "S_No ASC")
                    Dim Formula As String = ""
                    Dim StartCol As Integer = 13
                    For ix As Integer = StartCol To dtTotal.Columns.Count - 1
                        Formula = dr("Formula")
                        For Each dr1 As DataRow In dtTotal.Select("S_No<'" & dr("S_No") & "'")
                            If Formula.ToString.Contains("[" & dr1("S_No") & "]") Then
                                Formula = Replace(Formula, Formula.ToString.Substring(Formula.ToString.IndexOf("["), Formula.ToString.IndexOf("]") + 1 - Formula.ToString.IndexOf("[")), IIf(dr1(dtTotal.Columns(ix).ColumnName) Is DBNull.Value, 0, dr1(dtTotal.Columns(ix).ColumnName)))
                            End If
                        Next
                        If Formula.ToString.Contains("[") Then
                            Formula = Replace(Formula, Formula.ToString.Substring(Formula.ToString.IndexOf("["), Formula.ToString.IndexOf("]") + 1 - Formula.ToString.IndexOf("[")), 0)
                        End If
                        dr(dtTotal.Columns(ix).ColumnName) = EvaluateExpression(Formula)
                    Next
                    Dim drdt As DataRow = dt.NewRow()
                    drdt("S_no") = dr("S_No")
                    drdt("Main_particular") = dr("Main_particular")
                    drdt("Particular") = dr("Particular")
                    drdt("GrpCode") = dr("Particular")
                    drdt("GrpName") = dr("Particular")
                    drdt("Note") = dr("Note")
                    drdt("IsFormula") = "Yes"
                    For col As Integer = 13 To dtTotal.Columns.Count - 1
                        drdt(dtTotal.Columns(col).ColumnName) = dr(dtTotal.Columns(col).ColumnName)
                    Next
                    dt.Rows.Add(drdt)
                    dt.AcceptChanges()
                Next
            End If
            If rbtnTreeView.IsChecked Then
                Dim dtSumm As DataTable = dt.Copy()
                dt.Rows.Clear()
                fnlQty = "select Account_Balance,MAX(S_No) as S_No, Main_Particular ,Particular ,max(GrpCode) as GrpCode,(max(isnull(GrpName,''))) as GrpName,max(Note) as Note,Account_Group_code as [Account Group Code] ,(max(isnull(Account_group_desc,''))) as [Account Group Desc],SubGrpCode as [Sub Group Code],max( SubGrpDesc ) as [Sub Grp Desc],Main_GL_Account as [Main GL Account],max( MainGLName) as [Main GL Name] "
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                        fnlQty += ",sum(" + strkey.Key + ")* typ as " + strkey.Key
                    Next
                End If
                fnlQty += ", sum(Amount)* typ  as Amt ,(typ*  sum(Amount) )  as AmtActual  from(" + BaseQry + ") Final group by Main_Particular,typ,Particular,Account_Balance,Account_Group_Code,SubGrpCode,Main_GL_Account order by S_No"
                Dim dtRawData As DataTable = clsDBFuncationality.GetDataTable(fnlQty)
                Dim SNo As Integer = -1
                Dim strGrpCode As String = Nothing
                Dim strAccountGrpCode As String = Nothing
                Dim strSubGrpCode As String = Nothing
                Dim strMainGLCode As String = Nothing

                Dim intGrpCodeVersion As Integer = 0
                Dim intAccountGrpCodeVersion As Integer = 0
                Dim intSubGrpCodeVersion As Integer = 0
                Dim intMainGLCodeVersion As Integer = 0

                If dtRawData IsNot Nothing AndAlso dtRawData.Rows.Count > 0 Then
                    For ii As Integer = 0 To dtRawData.Rows.Count - 1
                        If Not (SNo = clsCommon.myCdbl(dtRawData.Rows(ii)("S_No")) OrElse clsCommon.CompairString(strGrpCode, clsCommon.myCstr(dtRawData.Rows(ii)("GrpCode"))) = CompairStringResult.Equal) Then
                            SNo = clsCommon.myCdbl(dtRawData.Rows(ii)("S_No"))
                            strGrpCode = clsCommon.myCstr(dtRawData.Rows(ii)("GrpCode"))
                            intGrpCodeVersion = clsCommon.myCdbl(dtRawData.Rows(ii)("S_No"))
                            intAccountGrpCodeVersion = 0
                            intSubGrpCodeVersion = 0
                            intMainGLCodeVersion = 0
                            AddNewRowForTree(dt, dtRawData.Rows(ii), clsCommon.myCstr(dtRawData.Rows(ii)("GrpName")), clsCommon.myCstr(intGrpCodeVersion))
                        End If
                        If Not clsCommon.CompairString(strAccountGrpCode, clsCommon.myCstr(dtRawData.Rows(ii)("Account Group Code"))) = CompairStringResult.Equal Then
                            strAccountGrpCode = clsCommon.myCstr(dtRawData.Rows(ii)("Account Group Code"))
                            intAccountGrpCodeVersion += 1
                            intSubGrpCodeVersion = 0
                            intMainGLCodeVersion = 0
                            AddNewRowForTree(dt, dtRawData.Rows(ii), clsCommon.myCstr(dtRawData.Rows(ii)("Account Group Desc")), clsCommon.myCstr(intGrpCodeVersion) + "." + clsCommon.myCstr(intAccountGrpCodeVersion))
                        End If
                        If Not clsCommon.CompairString(strSubGrpCode, clsCommon.myCstr(dtRawData.Rows(ii)("Sub Group Code"))) = CompairStringResult.Equal Then
                            strSubGrpCode = clsCommon.myCstr(dtRawData.Rows(ii)("Sub Group Code"))
                            intSubGrpCodeVersion += 1
                            intMainGLCodeVersion = 0
                            AddNewRowForTree(dt, dtRawData.Rows(ii), clsCommon.myCstr(dtRawData.Rows(ii)("Sub Grp Desc")), clsCommon.myCstr(intGrpCodeVersion) + "." + clsCommon.myCstr(intAccountGrpCodeVersion) + "." + clsCommon.myCstr(intSubGrpCodeVersion))
                        End If
                        If Not clsCommon.CompairString(strMainGLCode, clsCommon.myCstr(dtRawData.Rows(ii)("Main GL Account"))) = CompairStringResult.Equal Then
                            strMainGLCode = clsCommon.myCstr(dtRawData.Rows(ii)("Main GL Account"))
                            intMainGLCodeVersion += 1
                            AddNewRowForTree(dt, dtRawData.Rows(ii), clsCommon.myCstr(dtRawData.Rows(ii)("Main GL Name")), clsCommon.myCstr(intGrpCodeVersion) + "." + clsCommon.myCstr(intAccountGrpCodeVersion) + "." + clsCommon.myCstr(intSubGrpCodeVersion) + "." + clsCommon.myCstr(intMainGLCodeVersion))
                            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                                For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                                    dt.Rows(dt.Rows.Count - 1)(strkey.Value.ColumnName) = clsCommon.myCdbl(dtRawData.Rows(ii)(strkey.Value.ColumnName))
                                Next
                            End If
                            dt.Rows(dt.Rows.Count - 1)("Amt") = clsCommon.myCdbl(dtRawData.Rows(ii)("Amt"))
                            dt.Rows(dt.Rows.Count - 1)("AmtActual") = clsCommon.myCdbl(dtRawData.Rows(ii)("AmtActual"))
                        End If
                    Next
                    If dtSumm IsNot Nothing AndAlso dtSumm.Rows.Count > 0 Then
                        For ii As Integer = 0 To dtSumm.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(dtSumm.Rows(ii)("IsFormula")), "Yes") = CompairStringResult.Equal Then
                                AddNewRowForTree(dt, dtSumm.Rows(ii), clsCommon.myCstr(dtSumm.Rows(ii)("GrpName")), "")
                                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                                    For Each strkey As KeyValuePair(Of String, clsTempBalanceSheet) In arr
                                        dt.Rows(dt.Rows.Count - 1)(strkey.Value.ColumnName) = clsCommon.myCdbl(dtSumm.Rows(ii)(strkey.Value.ColumnName))
                                    Next
                                End If
                                dt.Rows(dt.Rows.Count - 1)("Amt") = clsCommon.myCdbl(dtSumm.Rows(ii)("Amt"))
                                dt.Rows(dt.Rows.Count - 1)("AmtActual") = clsCommon.myCdbl(dtSumm.Rows(ii)("AmtActual"))
                            End If
                        Next
                    End If
                End If
            End If

            If dt.Rows.Count <= 0 Then
                gv1.DataSource = Nothing
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
            gv1.DataSource = dt
            SetGridFormation(arr)

            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddNewRowForTree(ByVal dt As DataTable, ByVal drRawData As DataRow, ByVal strParticular As String, ByVal strVersion As String)
        Dim dr As DataRow = dt.NewRow()
        dr("Account_Balance") = clsCommon.myCstr(drRawData("Account_Balance"))
        dr("S_No") = clsCommon.myCstr(drRawData("S_No"))
        dr("Main_Particular") = clsCommon.myCstr(drRawData("Main_Particular"))
        dr("Particular") = clsCommon.myCstr(drRawData("Particular"))
        dr("GrpCode") = clsCommon.myCstr(drRawData("GrpCode"))
        dr("GrpName") = strVersion + " " + strParticular
        dr("Note") = clsCommon.myCstr(drRawData("Note"))
        dt.Rows.Add(dr)
    End Sub

    Private Function EvaluateExpression(ByVal eqn As String)
        Dim dtev As DataTable = New DataTable()
        Dim result As Decimal = dtev.Compute(eqn, String.Empty)
        Return result
    End Function

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
        Dim qry As String = "select * from TSPL_BALANCE_SHEET_PERFORMA_FORMULA where Font_Style>'1.000000'"
        DtGroup = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        Dim view As New ColumnGroupsViewDefinition()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        '================Rohit on May 22,2014 to SHow data as per Balance Sheet=================
        If rbtnNA.IsChecked OrElse rbtnTreeView.IsChecked Then
            Dim dv As DataView = New DataView(dt, "S_No>0", "S_No ASC", DataViewRowState.CurrentRows)
            gv1.DataSource = dv 'dt
        Else
            gv1.DataSource = dt
        End If

        '=====================================================
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rbtnNA.IsChecked OrElse rbtnTreeView.IsChecked Then
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
            gv1.Columns("Particular").HeaderText = ""

            gv1.Columns("Account_Balance").IsVisible = False
            gv1.Columns("Account_Balance").Width = 150
            gv1.Columns("Account_Balance").HeaderText = "Account_Balance"

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
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "Account Group Code"))
                view.ColumnGroups("Account Group Code").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Account Group Code").Rows(0).ColumnNames.Add(gv1.Columns("Account Group Code").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "Account Group Desc"))
                view.ColumnGroups("Account Group Desc").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Account Group Desc").Rows(0).ColumnNames.Add(gv1.Columns("Account Group Desc").Name)
            End If
            gv1.Columns("Account Group Code").IsVisible = True
            gv1.Columns("Account Group Code").Width = 150
            gv1.Columns("Account Group Code").HeaderText = "Account Group Code"

            gv1.Columns("Account Group Desc").IsVisible = True
            gv1.Columns("Account Group Desc").Width = 150
            gv1.Columns("Account Group Desc").HeaderText = "Account Group Desc"
        ElseIf rbtnGLRollupLevel.IsChecked Then
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "Sub Group Code"))
                view.ColumnGroups("Sub Group Code").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Sub Group Code").Rows(0).ColumnNames.Add(gv1.Columns("Sub Group Code").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "Sub Grp Desc"))
                view.ColumnGroups("Sub Grp Desc").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Sub Grp Desc").Rows(0).ColumnNames.Add(gv1.Columns("Sub Grp Desc").Name)
            End If
            gv1.Columns("Sub Group Code").IsVisible = True
            gv1.Columns("Sub Group Code").Width = 150
            gv1.Columns("Sub Group Code").HeaderText = "Sub Group Code"

            gv1.Columns("Sub Grp Desc").IsVisible = True
            gv1.Columns("Sub Grp Desc").Width = 150
            gv1.Columns("Sub Grp Desc").HeaderText = "Sub Group Name"
        ElseIf ChkGLMainAccount.IsChecked Then
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "Main GL Account"))
                view.ColumnGroups("Main GL Account").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Main GL Account").Rows(0).ColumnNames.Add(gv1.Columns("Main GL Account").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "Main GL Name"))
                view.ColumnGroups("Main GL Name").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Main GL Name").Rows(0).ColumnNames.Add(gv1.Columns("Main GL Name").Name)
            End If
            gv1.Columns("Main GL Account").IsVisible = True
            gv1.Columns("Main GL Account").Width = 150
            gv1.Columns("Main GL Account").HeaderText = "GL Main Account Code"

            gv1.Columns("Main GL Name").IsVisible = True
            gv1.Columns("Main GL Name").Width = 150
            gv1.Columns("Main GL Name").HeaderText = "GL Main Account Desc"
        ElseIf ChkGLAccount.IsChecked Then
            If chkLocationWise.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("", "Account_Code"))
                view.ColumnGroups("Account_Code").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("Account_Code").Rows(0).ColumnNames.Add(gv1.Columns("Account_Code").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("", "AccountName"))
                view.ColumnGroups("AccountName").Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups("AccountName").Rows(0).ColumnNames.Add(gv1.Columns("AccountName").Name)
            End If
            gv1.Columns("Account_Code").IsVisible = True
            gv1.Columns("Account_Code").Width = 150
            gv1.Columns("Account_Code").HeaderText = "Account Code"

            gv1.Columns("AccountName").IsVisible = True
            gv1.Columns("AccountName").Width = 400
            gv1.Columns("AccountName").HeaderText = "Account Name"
        ElseIf rbtnTreeView.IsChecked Then
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
            gv1.Columns("Particular").HeaderText = ""

            gv1.Columns("Account_Balance").IsVisible = False
            gv1.Columns("Account_Balance").Width = 150
            gv1.Columns("Account_Balance").HeaderText = "Account_Balance"

            gv1.Columns("GrpCode").IsVisible = False
            gv1.Columns("GrpCode").Width = 150
            gv1.Columns("GrpCode").HeaderText = "Account Group Code"

            gv1.Columns("GrpName").IsVisible = True
            gv1.Columns("GrpName").Width = 300
            gv1.Columns("GrpName").HeaderText = "Particular"

            gv1.Columns("Note").IsVisible = True
            gv1.Columns("Note").Width = 80
            gv1.Columns("Note").HeaderText = "Note"

            gv1.Columns("Account Group Code").IsVisible = True
            gv1.Columns("Account Group Code").Width = 150
            gv1.Columns("Account Group Code").HeaderText = "Account Group Code"

            gv1.Columns("Account Group Desc").IsVisible = True
            gv1.Columns("Account Group Desc").Width = 150
            gv1.Columns("Account Group Desc").HeaderText = "Account Group Desc"

            gv1.Columns("Sub Group Code").IsVisible = True
            gv1.Columns("Sub Group Code").Width = 150
            gv1.Columns("Sub Group Code").HeaderText = "Sub Group Code"

            gv1.Columns("Sub Grp Desc").IsVisible = True
            gv1.Columns("Sub Grp Desc").Width = 150
            gv1.Columns("Sub Grp Desc").HeaderText = "Sub Group Name"

            gv1.Columns("Main GL Account").IsVisible = True
            gv1.Columns("Main GL Account").Width = 150
            gv1.Columns("Main GL Account").HeaderText = "GL Main Account Code"

            gv1.Columns("Main GL Name").IsVisible = True
            gv1.Columns("Main GL Name").Width = 150
            gv1.Columns("Main GL Name").HeaderText = "GL Main Account Desc"


        End If
        Dim strInterval As String = Nothing
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
                    If gv1.Columns(ii).HeaderText = "" Then
                        gv1.Columns(ii).HeaderText = "Total"
                    End If
                    view.ColumnGroups(strInterval).Rows(0).ColumnNames.Add(gv1.Columns(ii).Name)
                End If
                Dim item As New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
            End If
        Next
        gv1.Columns("IsFormula").IsVisible = False

        gv1.Columns("Amt").IsVisible = False
        gv1.Columns("Amt").Width = 150
        gv1.Columns("Amt").HeaderText = "Amount"
        gv1.Columns("Amt").FormatString = "{0:F2}"

        gv1.Columns("AmtActual").IsVisible = Not rbtnYearly.IsChecked
        gv1.Columns("AmtActual").Width = 150
        gv1.Columns("AmtActual").HeaderText = "Amount"
        gv1.Columns("AmtActual").FormatString = "{0:F2}"

        If rbtnNA.IsChecked OrElse rbtnTreeView.IsChecked Then
            gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
        End If

        Dim item1 As New GridViewSummaryItem("AmtActual", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        If chkShowTotalRow.Checked Then
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If

        If chkLocationWise.Checked Then
            gv1.ViewDefinition = view
        End If
        gv1.ReadOnly = True
        EnableDisableControls(False)
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
        chkIncludeingClosingEntry.Enabled = Val
        chkShowTotalRow.Enabled = Val
        txtmultLocationSegment.Enabled = Val
        txtmultAccountMainGroup.Enabled = Val
        txtmultAccountGroup.Enabled = Val
        txtmultAccountSubGroup.Enabled = Val
        txtMultGLMainAmountGroup.Enabled = Val
    End Sub

    Private Sub FunExportToExcel(ByVal exporter As EnumExportTo)
        Try
            RefreshData()
            If dt.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strTemp As String = ""
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProfitAndLoss & "'"))
                If txtmultLocationSegment.arrDispalyMember IsNot Nothing AndAlso txtmultLocationSegment.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Location Segment : " + clsCommon.GetMulcallStringWithComma(txtmultLocationSegment.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text, True)
                ElseIf exporter = EnumExportTo.PDF Then
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        Try
            If rbtnNA.IsChecked Or rbtnTreeView.IsChecked Then
                If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight
                End If
                If e.RowIndex < 0 Then Exit Sub

                Dim dr() As DataRow = DtGroup.Select("S_No=" & gv1.Rows(e.RowIndex).Cells("S_No").Value)
                If dr.Length > 0 Then
                    If dr(0)("Font_Style") = 2 Then
                        e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Bold)
                    ElseIf dr(0)("Font_Style") = 3 Then
                        e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Italic)
                    ElseIf dr(0)("Font_Style") = 4 Then
                        e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Underline)
                    End If
                    If dr(0)("Formula") <> "" Then
                        e.CellElement.TextAlignment = ContentAlignment.MiddleRight
                    End If

                End If
            End If
        Catch ex As Exception
        End Try
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
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

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        DrillDown()
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
                    frm.MyLabel2.Text = "To Date"
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
                    frm.arrAcc.Add(strACode)
                    If txtmultAccountGroup.arrValueMember Is Nothing OrElse txtmultAccountGroup.arrValueMember.Count <= 0 Then '--cbgGLACGrp.CheckedValue.Count <= 0 Then
                        rbtnGLAccountGrpAll.IsChecked = True
                    End If
                    frm.Show()
                End If
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

    Private Sub txtmultLocationSegment__My_Click(sender As Object, e As EventArgs) Handles txtmultLocationSegment._My_Click
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
        dtAccount = clsDBFuncationality.GetDataTable("select * from TSPL_Gl_Accounts") ' where ControlAccount='Y'
    End Sub

    Private Sub txtmultAccountSubGroup__My_Click(sender As Object, e As EventArgs) Handles txtmultAccountSubGroup._My_Click
        StrQry = "select Account_Sub_Group_Code as Code,Account_Sub_Group_Desc as Name from TSPL_ACCOUNT_Sub_GROUPS"
        txtmultAccountSubGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("multAccountSubGroup", StrQry, "Code", "Name", txtmultAccountSubGroup.arrValueMember, txtmultAccountSubGroup.arrDispalyMember)
    End Sub

    Private Sub txtMultGLMainAmountGroup__My_Click(sender As Object, e As EventArgs) Handles txtMultGLMainAmountGroup._My_Click
        Dim qry As String = "select Main_GL_Account as Code,Main_GL_Account_desc as Name from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
        txtMultGLMainAmountGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("MultGLMainAmountGrou", StrQry, "Code", "Name", txtMultGLMainAmountGroup.arrValueMember, txtMultGLMainAmountGroup.arrDispalyMember)
    End Sub

    Private Sub btnQuickExp_Click(sender As Object, e As EventArgs) Handles btnQuickExp.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProfitAndLoss & "'"))
            If txtmultLocationSegment.arrDispalyMember IsNot Nothing AndAlso txtmultLocationSegment.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location Segment : " + clsCommon.GetMulcallStringWithComma(txtmultLocationSegment.arrDispalyMember))
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
 
