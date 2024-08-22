Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.IO
Public Class rptEmployeeDeductionMasterReport
#Region "Variables"
    Dim FromDate As String
    Dim ToDate As String
#End Region
    Private Sub rptEmployeeDeductionMasterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        fndEmpMult.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Refresh()
        fndEmpMult.arrValueMember = Nothing
        txtMultDeduction.arrValueMember = Nothing
        rbtnEmpDed.Checked = True
        MyLabel2.Visible = False
        txtMultMonths.Visible = False
        txtMultMonths.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub fndEmpMult__My_Click(sender As Object, e As EventArgs) Handles fndEmpMult._My_Click
        Try
            Dim qry As String = "Select EMP_CODE As [Code],Emp_Name As [Name], Designation,Emp_Status As Status,LOCATION_CODE As [Location] from TSPL_EMPLOYEE_MASTER"
            fndEmpMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", fndEmpMult.arrValueMember, fndEmpMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        PrintReport(False)
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rbtnEmpDed.Checked Then
            VarID += "_S"
        ElseIf rbtnPayHeadDed.Checked Then
            VarID += "_T"
        End If
        Gv1.VarID = VarID
    End Sub

    Private Sub PrintReport(ByVal isPrint As Boolean)
        Try
            Dim colSummary As String = Nothing
            Dim CaseDeduction As String = Nothing
            Dim Qry As String = Nothing
            Dim EmpDedQry As String = Nothing
            Dim PayHeadDedQry As String = Nothing
            Dim dt As DataTable = Nothing
            Dim whrCls As String = Nothing
            If rbtnEmpDed.Checked Then
                If isPrint Then
                    EmpDedQry = "Select '" + objCommonVar.CurrentUser + "' As PrintBy,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,"
                    If txtMultDeduction.arrValueMember IsNot Nothing AndAlso txtMultDeduction.arrValueMember.Count > 0 Then
                        EmpDedQry += "('" + clsCommon.GetMulcallString(txtMultDeduction.arrValueMember).Replace("'", "") + "') As PayHead,"
                    Else
                        EmpDedQry += " 'All' As PayHead,"
                    End If
                Else
                    EmpDedQry = "Select "
                End If
                EmpDedQry += "TSPL_EMPLOYEE_MASTER.EMP_CODE As [Emp. Code],TSPL_EMPLOYEE_MASTER.Emp_Name As [Emp. Name]"
                If txtMultDeduction.arrValueMember IsNot Nothing AndAlso txtMultDeduction.arrValueMember.Count > 0 Then
                    For i As Integer = 0 To txtMultDeduction.arrValueMember.Count - 1
                        If clsCommon.CompairString(txtMultDeduction.arrValueMember(i), "LIC") = CompairStringResult.Equal Then
                            EmpDedQry += ", TSPL_EMPLOYEE_DEDUCTION_MASTER.LIC_POLICY_NO As [LIC Policy No],TSPL_EMPLOYEE_DEDUCTION_MASTER.LIC_PREMIUM_AMT As [LIC Premium Amt.]"
                        ElseIf clsCommon.CompairString(txtMultDeduction.arrValueMember(i), "Bank") = CompairStringResult.Equal Then
                            EmpDedQry += ", TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_ACCOUNT_NO As [Bank Account No],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_INSTALMENT As [Bank Instalment],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_NAME As [Bank Name]"
                        ElseIf clsCommon.CompairString(txtMultDeduction.arrValueMember(i), "Quarter") = CompairStringResult.Equal Then
                            EmpDedQry += ", TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_TYPE As [Quarter Type],Convert(Varchar,TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_ALLOTED_DATE,103) As [Quarter Alloted Date],Convert(Varchar,TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_LEFT_DATE,103) As [Quarter Left Date]"
                        ElseIf clsCommon.CompairString(txtMultDeduction.arrValueMember(i), "KKK") = CompairStringResult.Equal Then
                            EmpDedQry += ", TSPL_EMPLOYEE_DEDUCTION_MASTER.KKK_INSTALMENT As [KKK Instalment],TSPL_EMPLOYEE_DEDUCTION_MASTER.KKK_LOAN_TOTAL As [KKK Total Loan] "
                        End If
                    Next
                Else
                    EmpDedQry += ", TSPL_EMPLOYEE_DEDUCTION_MASTER.LIC_POLICY_NO As [LIC Policy No],TSPL_EMPLOYEE_DEDUCTION_MASTER.LIC_PREMIUM_AMT As [LIC Premium Amt.],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_ACCOUNT_NO As [Bank Account No],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_INSTALMENT As [Bank Instalment],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_NAME As [Bank Name],TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_TYPE As [Quarter Type],Convert(Varchar,TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_ALLOTED_DATE,103) As [Quarter Alloted Date],Convert(Varchar,TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_LEFT_DATE,103) As [Quarter Left Date],TSPL_EMPLOYEE_DEDUCTION_MASTER.KKK_INSTALMENT As [KKK Instalment],TSPL_EMPLOYEE_DEDUCTION_MASTER.KKK_LOAN_TOTAL As [KKK Total Loan] "
                End If
                EmpDedQry += " from TSPL_EMPLOYEE_DEDUCTION_MASTER
                     Left Outer Join TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EMPLOYEE_DEDUCTION_MASTER.EMP_CODE
                     Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "' Where 2=2 "
                If fndEmpMult.arrValueMember IsNot Nothing AndAlso fndEmpMult.arrValueMember.Count > 0 Then
                    EmpDedQry += "  and TSPL_EMPLOYEE_MASTER.EMP_CODE IN (" + clsCommon.GetMulcallString(fndEmpMult.arrValueMember) + ") "
                End If

            Else
                Qry = "Select PAY_HEAD_CODE from TSPL_PAYHEAD_MASTER  Where convert(int,TSPL_PAYHEAD_MASTER.ISEARNING)=0 "
                If txtMultDeduction.arrValueMember IsNot Nothing AndAlso txtMultDeduction.arrValueMember.Count > 0 Then
                    Qry += "  and PAY_HEAD_CODE IN (" + clsCommon.GetMulcallString(txtMultDeduction.arrValueMember) + ") "
                End If
                dt = clsDBFuncationality.GetDataTable(Qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each row In dt.Rows
                        If CaseDeduction IsNot Nothing AndAlso clsCommon.myLen(CaseDeduction) > 0 Then
                            CaseDeduction += ",Case When (PAY_HEAD_CODE)='" + clsCommon.myCstr(row("PAY_HEAD_CODE")) + "' Then (Payable_Amount) Else 0 End As '" + clsCommon.myCstr(row("PAY_HEAD_CODE")) + "'"
                            colSummary += ",Sum([" + clsCommon.myCstr(row("PAY_HEAD_CODE")) + "]) As '" + clsCommon.myCstr(row("PAY_HEAD_CODE")) + "' "
                        Else
                            CaseDeduction = ",Case When (PAY_HEAD_CODE)='" + clsCommon.myCstr(row("PAY_HEAD_CODE")) + "' Then (Payable_Amount) Else 0 End As '" + clsCommon.myCstr(row("PAY_HEAD_CODE")) + "'"
                            colSummary = ",Sum([" + clsCommon.myCstr(row("PAY_HEAD_CODE")) + "]) As '" + clsCommon.myCstr(row("PAY_HEAD_CODE")) + "' "
                        End If
                    Next
                End If

                If isPrint Then
                    PayHeadDedQry = "Select '" + objCommonVar.CurrentUser + "' As PrintBy,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,xyz.* from"
                Else
                    PayHeadDedQry = "(Select "
                End If

                If Not isPrint Then
                    PayHeadDedQry += " EMP_CODE, Max(Emp_Name)Emp_Name " + colSummary + " from (Select EMP_CODE,(Emp_Name)Emp_Name " + CaseDeduction + " from"
                End If

                PayHeadDedQry += "(SELECT t1.PAY_PERIOD_CODE,T2.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,T2.PAY_HEAD_CODE,T2.ACTUAL_AMOUNT,T2.Payable_Amount
                            FROM TSPL_GENERATE_SALARY T1  
                            INNER JOIN TSPL_GENERATE_SALARY_PAYHEADS T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE  
                            inner JOIN TSPL_GENERATE_SALARY_ATTENDANCE T5 ON T5.Emp_code =T2.Emp_code  AND T1.SALARY_GENERATION_CODE=T5.SALARY_GENERATION_CODE  
                            Left Outer Join TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE=T5.EMP_CODE                            
                            WHERE 2=2  "
                If fndEmpMult.arrValueMember IsNot Nothing AndAlso fndEmpMult.arrValueMember.Count > 0 Then
                    PayHeadDedQry += " And TSPL_EMPLOYEE_MASTER.Emp_Code In (" + clsCommon.GetMulcallString(fndEmpMult.arrValueMember) + ") "
                End If
                If txtMultMonths.arrValueMember IsNot Nothing AndAlso txtMultMonths.arrValueMember.Count > 0 Then
                    PayHeadDedQry += " AND T1.PAY_PERIOD_CODE in (" + clsCommon.GetMulcallString(txtMultMonths.arrValueMember) + ")"
                End If
                PayHeadDedQry += "And T2.PAY_HEAD_CODE IN (Select PAY_HEAD_CODE from TSPL_PAYHEAD_MASTER  Where convert(int,TSPL_PAYHEAD_MASTER.ISEARNING)=0 "
                If txtMultDeduction.arrValueMember IsNot Nothing AndAlso txtMultDeduction.arrValueMember.Count > 0 Then
                    PayHeadDedQry += "  and PAY_HEAD_CODE IN (" + clsCommon.GetMulcallString(txtMultDeduction.arrValueMember) + ") "
                End If
                PayHeadDedQry += ") and PAYABLE_AMOUNT>0 )xyz Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'"
                If Not isPrint Then
                    PayHeadDedQry += ")zzz Group By zzz.EMP_CODE) "
                End If
                'If isPrint Then
                '    PayHeadDedQry += "final Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "' "
                'End If
            End If
            If rbtnEmpDed.Checked Then
                Qry = EmpDedQry
            Else
                Qry = PayHeadDedQry
            End If
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If isPrint Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    If rbtnEmpDed.Checked Then
                        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "crptEmployeeDeductionMaster", "Employee Deduction Report")
                    Else
                        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "crptEmployeePayHeadDeductionReport", "Employee Deduction Report")
                    End If
                    frmcrystal = Nothing
                Else
                    Gv1.DataSource = Nothing
                    Gv1.Rows.Clear()
                    Gv1.Refresh()
                    Gv1.DataSource = dt
                    SetSummaryRow()
                    Gv1.AllowAddNewRow = False
                    Gv1.ShowGroupPanel = True
                    Gv1.ReadOnly = True
                    Gv1.EnableFiltering = True
                    Gv1.EnableGrouping = True
                    SetGroup()
                    Gv1.BestFitColumns()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGroup()
        If Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then
            Dim Ded As String = ""
            If txtMultDeduction.arrValueMember Is Nothing Then
                Ded = "All"
            End If
            Dim i As Integer = 0
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(i).Rows.Add(New GridViewColumnGroupRow())
            i += 1
            If rbtnEmpDed.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Employee"))
                view.ColumnGroups(i).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("Emp. Code").Name)
                view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("Emp. Name").Name)
                i += 1
                If clsCommon.GetMulcallString(txtMultDeduction.arrValueMember).Contains("LIC") OrElse Ded.Contains("All") Then
                    view.ColumnGroups.Add(New GridViewColumnGroup("LIC"))
                    view.ColumnGroups(i).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("LIC Policy No").Name)
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("LIC Premium Amt.").Name)
                    i += 1
                End If
                If clsCommon.GetMulcallString(txtMultDeduction.arrValueMember).Contains("Bank") OrElse Ded.Contains("All") Then
                    view.ColumnGroups.Add(New GridViewColumnGroup("Bank"))
                    view.ColumnGroups(i).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("Bank Account No").Name)
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("Bank Instalment").Name)
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("Bank Name").Name)
                    i += 1
                End If
                If clsCommon.GetMulcallString(txtMultDeduction.arrValueMember).Contains("Quarter") OrElse Ded.Contains("All") Then
                    view.ColumnGroups.Add(New GridViewColumnGroup("Quarter"))
                    view.ColumnGroups(i).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("Quarter Type").Name)
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("Quarter Alloted Date").Name)
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("Quarter Left Date").Name)
                    i += 1
                End If
                If clsCommon.GetMulcallString(txtMultDeduction.arrValueMember).Contains("KKK") OrElse Ded.Contains("All") Then
                    view.ColumnGroups.Add(New GridViewColumnGroup("KKK"))
                    view.ColumnGroups(i).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("KKK Instalment").Name)
                    view.ColumnGroups(i).Rows(0).ColumnNames.Add(Gv1.Columns("KKK Total Loan").Name)
                End If
            Else
                If Gv1.Columns.Count > 0 Then
                    view.ColumnGroups.Add(New GridViewColumnGroup("Pay Head Deduction"))
                    For i = 0 To Gv1.Columns.Count - 1
                        view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                        view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns(i).Name)
                    Next
                End If
            End If
            Gv1.ViewDefinition = view
        End If
    End Sub
    Sub SetSummaryRow()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If Gv1.Columns.Count > 0 Then
            For i As Integer = 2 To Gv1.Columns.Count - 1
                Dim item As New GridViewSummaryItem(Gv1.Columns(i).Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
            Next
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintReport(True)
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strTemp As String = ""
                arrHeader.Add("" & clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'"))
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmEmployeeDeductionMaster & "'"))
                If Not fndEmpMult.arrValueMember Is Nothing Then
                    arrHeader.Add("Employee : " & clsCommon.GetMulcallStringWithComma(fndEmpMult.arrValueMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultDeduction__My_Click(sender As Object, e As EventArgs) Handles txtMultDeduction._My_Click
        Try
            Dim qry As String = "Select Code from ("
            If rbtnEmpDed.Checked Then
                qry += "Select 'LIC' As Code
                                Union All
                                Select 'Bank' As Code
                                Union All
                                Select 'Quarter' As Code
                                Union All
                                Select 'KKK' As Code"
            Else
                qry += " select PAY_HEAD_CODE as Code from TSPL_PAYHEAD_MASTER left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PAYHEAD_MASTER.Account_Code Where convert(int,TSPL_PAYHEAD_MASTER.ISEARNING)=0"
            End If
            qry += " )xxx Group By Code"
            txtMultDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtMultDeduction.arrValueMember, txtMultDeduction.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnEmpDed_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnEmpDed.CheckedChanged
        Try
            MyLabel2.Visible = False
            txtMultMonths.Visible = False
            txtMultMonths.arrValueMember = Nothing
            txtMultDeduction.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnPayHeadDed_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnPayHeadDed.CheckedChanged
        Try
            MyLabel2.Visible = True
            txtMultMonths.Visible = True
            txtMultMonths.arrValueMember = Nothing
            txtMultDeduction.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultMonths__My_Click(sender As Object, e As EventArgs) Handles txtMultMonths._My_Click
        Try
            Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
       & " PAY_PERIOD_NAME as 'Pay Period Name',date_from as [From Date],date_to as [Date To] FROM TSPL_PAYPERIOD_MASTER Where  POSTED=1 AND FREEZED=0 and convert(date, date_from,103) <= Convert (date,SYSDATETIME(),103) "
            txtMultMonths.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtMultMonths.arrValueMember, txtMultMonths.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class