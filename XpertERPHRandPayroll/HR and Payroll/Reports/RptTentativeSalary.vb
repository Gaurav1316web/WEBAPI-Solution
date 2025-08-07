Imports common
Imports XpertERPEngine
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI
Public Class RptTentativeSalary
    Inherits FrmMainTranScreen
    Dim fromdate As DateTime = Nothing
    Dim ToDate As DateTime = Nothing
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtFinYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFinYear._MYValidating
        Try

            Dim qry As String = "SELECT Fiscal_Code as Code , Fiscal_Name as Name FROM TSPL_Fiscal_Year_Master "
            txtFinYear.Value = clsCommon.ShowSelectForm("FinYear", qry, "Code", "", txtFinYear.Value, "Code", isButtonClicked)
            lblFinYear.Text = clsDBFuncationality.getSingleValue("Select Fiscal_Name  from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYear.Value + "' ")
            fromdate = clsDBFuncationality.getSingleValue("Select Start_Date  from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYear.Value + "' ")
            ToDate = clsDBFuncationality.getSingleValue("Select End_Date  from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYear.Value + "' ")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub txtEmpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Try
    '        Dim qry As String = "select emp_code as Code,Emp_Name as Name,Designation,Birth_date as [Birth Date],Joining_date as [Joining Date] from TSPL_EMPLOYEE_MASTER"
    '        txtEmpCode.Value = clsCommon.ShowSelectForm("EmpCode", qry, "Code", "", txtEmpCode.Value, "Code", isButtonClicked)
    '        lblEmpName.Text = clsDBFuncationality.getSingleValue("Select Emp_Name from TSPL_EMPLOYEE_MASTER where emp_code='" + txtEmpCode.Value + "' ")
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtmultiEmpcode.arrValueMember = Nothing
        txtFinYear.Value = ""
        lblFinYear.Text = ""
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim whr As String = ""
            Dim whrEmp As String = ""
            If txtmultiEmpcode.arrValueMember IsNot Nothing AndAlso txtmultiEmpcode.arrValueMember.Count > 0 Then
                whr = " in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ")"
                whrEmp = " EMP.EMP_CODE in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ") And "
            End If
            Dim dt As DataTable = Nothing
            Dim GetLastGenerateMonth As Double = 0
            Dim GetLastMonthName As String = ""
            Dim FinalQry As String = ""
            Dim finYear As String = txtFinYear.Value
            Dim firstPart As String = finYear.Substring(0, 2)
            If txtmultiEmpcode.arrValueMember IsNot Nothing AndAlso txtmultiEmpcode.arrValueMember.Count > 0 Then
                GetLastGenerateMonth = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select top 1 MONTH(DATE_FROM)AS GENERATE_MONTH from TSPL_PAYPERIOD_MASTER Inner Join TSPL_GENERATE_SALARY On TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE where TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE" + whr + " Order By DATE_FROM desc"))
                GetLastMonthName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select top 1 TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE AS PAY_MONTH from TSPL_PAYPERIOD_MASTER Inner Join TSPL_GENERATE_SALARY On TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE where TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE" + whr + " Order By DATE_FROM desc "))
            Else
                GetLastGenerateMonth = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select top 1 MONTH(DATE_FROM)AS GENERATE_MONTH from TSPL_PAYPERIOD_MASTER Inner Join TSPL_GENERATE_SALARY On TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE "))
                GetLastMonthName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select top 1 TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE AS PAY_MONTH from TSPL_PAYPERIOD_MASTER Inner Join TSPL_GENERATE_SALARY On TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE  "))

            End If
            Dim Qry As String = "select emp.Emp_Name,'" + txtFinYear.Value + "' as FinYear,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_PAYHEAD_MASTER.ISEARNING,case when ISEARNING=1 then 'A'+ ' ' +TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE else 'D'+' '+TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE end  as PayHead,right(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM),2) AS PAY_YEAR,MONTH(DATE_FROM)AS GENERATE_MONTH,DATENAME(MONTH,TSPL_PAYPERIOD_MASTER.DATE_FROM )AS PAY_MONTH,MONTH(DATE_FROM)  as Mon,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE as [PAY PERIOD CODE],TSPL_GENERATE_SALARY_PAYHEADS.* from TSPL_GENERATE_SALARY_PAYHEADS 
            left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
            INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  
            inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
            LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
            left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
            left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=EMP.LOCATION_CODE
            where " + whrEmp + "  right(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM),2) ='" + firstPart + "' and MONTH(DATE_FROM) not in (1,2,3) "

            'If GetLastGenerateMonth > 0 Then
            '    Dim m As Integer = (GetLastGenerateMonth + 1)
            '    Dim FromMonth As Integer = 12
            '    For i As Integer = m To FromMonth

            '        Qry += " Union All 
            '                 select emp.Emp_Name, '" + txtFinYear.Value + "' as FinYear,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_PAYHEAD_MASTER.ISEARNING,case when ISEARNING=1 then 'A'+ ' ' +TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE else 'D'+' '+TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE end  as PayHead,right(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM),2) AS PAY_YEAR,MONTH(DATE_FROM)AS GENERATE_MONTH, '" + MonthName(m) + "'AS PAY_MONTH,'" + clsCommon.myCstr(m) + "' as Mon,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE as [PAY PERIOD CODE],TSPL_GENERATE_SALARY_PAYHEADS.* from TSPL_GENERATE_SALARY_PAYHEADS 
            'left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
            'INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  
            'inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
            'LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
            'left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
            'left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=EMP.LOCATION_CODE
            'where  " + whrEmp + "  TSPL_GENERATE_SALARY.PAY_PERIOD_CODE IN ('" + GetLastMonthName + "') and right(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM),2) ='" + firstPart + "'"
            '        m += 1

            '        If i = 12 Then
            '            i = 1
            '            m = 1
            '            FromMonth = 3
            '        End If
            '        If i > FromMonth Then
            '            Exit For
            '        End If
            '    Next
            'FinalQry = "Select * from (" & Qry & ")YYY order by ISEARNING desc ,Mon"
            Dim PayperiodCode As String = Nothing
            Dim Payperiod As String = Nothing
            Payperiod = " Select Distinct TSPL_GENERATE_SALARY.PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER 
                                        Inner Join TSPL_GENERATE_SALARY On TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE 
                                        left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE 
                                        WHERE Convert(date,GENERATE_DATE,108)>=convert(date,'" & fromdate & "',103) and  Convert(date,GENERATE_DATE,108)<=convert(date,'" & ToDate & "',103) "
            Dim dtPayPeriod As DataTable = clsDBFuncationality.GetDataTable(Payperiod)

            If dtPayPeriod.Rows.Count > 0 Then
                For i As Integer = 0 To dtPayPeriod.Rows.Count - 1
                    Dim J As Integer = 0
                    If i = 0 Then
                        J = i
                        PayperiodCode += "  '" + clsCommon.myCstr(dtPayPeriod.Rows(i)("PAY_PERIOD_CODE")) + "' "
                    Else
                        PayperiodCode += ", '" + clsCommon.myCstr(dtPayPeriod.Rows(i)("PAY_PERIOD_CODE")) + "' "
                    End If
                    '(" & PayperiodCode & ")
                Next
            End If

            FinalQry = "( SELECT TSPL_GENERATE_SALARY.PAY_PERIOD_CODE,emp.Emp_Name,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE,'" + txtFinYear.Value + "' AS FinYear,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,
                    TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.Location_Desc,CASE WHEN ISEARNING = 1 THEN 'A ' + TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
                    ELSE 'D ' + TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE END AS PayHead,TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT,RIGHT(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM), 2) AS PAY_YEAR,
                    MONTH(TSPL_PAYPERIOD_MASTER.DATE_FROM) AS GENERATE_MONTH,DATENAME(MONTH, TSPL_PAYPERIOD_MASTER.DATE_TO) AS PAY_MONTH, MONTH(TSPL_PAYPERIOD_MASTER.DATE_TO) AS Mon,
                    TSPL_PAYPERIOD_MASTER.DATE_TO ,TSPL_GENERATE_SALARY_PAYHEADS.PAYABLE_AMOUNT FROM TSPL_GENERATE_SALARY_PAYHEADS 
                    LEFT JOIN tspl_employee_master EMP ON EMP.EMP_CODE = TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
                    INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  
                    INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE 
                    AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
                    LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
                    LEFT OUTER JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE = TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
                    LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = EMP.LOCATION_CODE
                    WHERE " + whrEmp + "   TSPL_GENERATE_SALARY.PAY_PERIOD_CODE IN (" & PayperiodCode & ") and ACTUAL_AMOUNT<>0
					) "
            'End If
            dt = clsDBFuncationality.GetDataTable(FinalQry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.HRPayroll, dt, "rptTentativeSalary", "Tentative Salary", Nothing)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Function MonthName(ByVal ii As Integer) As String
        Dim strMonth As String = ""
        Select Case ii
            Case 1
                strMonth = "Jan"
            Case 2
                strMonth = "Feb"
            Case 3
                strMonth = "Mar"
            Case 4
                strMonth = "Apr"
            Case 5
                strMonth = "May"
            Case 6
                strMonth = "June"
            Case 7
                strMonth = "Jul"
            Case 8
                strMonth = "Aug"
            Case 9
                strMonth = "Sept"
            Case 10
                strMonth = "Oct"
            Case 11
                strMonth = "Nov"
            Case 12
                strMonth = "Dec"
            Case Else
                strMonth = "Data Not Found"
        End Select
        Return strMonth
    End Function

    Private Sub txtmultiEmpcode__My_Click(sender As Object, e As EventArgs) Handles txtmultiEmpcode._My_Click
        Dim qry As String = "select emp_code as Code,Emp_Name as Name,Designation,Birth_date as [Birth Date],Joining_date as [Joining Date] from TSPL_EMPLOYEE_MASTER"
        txtmultiEmpcode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Code", txtmultiEmpcode.arrValueMember, txtmultiEmpcode.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PrintData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub PrintData()
        Try
            Dim whr As String = ""
            Dim whrEmp As String = ""
            Dim PayperiodCode As String = Nothing
            If txtmultiEmpcode.arrValueMember IsNot Nothing AndAlso txtmultiEmpcode.arrValueMember.Count > 0 Then
                whr = " in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ")"
                whrEmp = " EMP.EMP_CODE in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ") And "
            End If
            Dim Payperiod As String = Nothing
            Payperiod = " Select Distinct TSPL_GENERATE_SALARY.PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER 
                                        Inner Join TSPL_GENERATE_SALARY On TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE 
                                        left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE 
                                        WHERE Convert(date,GENERATE_DATE,108)>=convert(date,'" & fromdate & "',103) and  Convert(date,GENERATE_DATE,108)<=convert(date,'" & ToDate & "',103) "
            Dim dtPayPeriod As DataTable = clsDBFuncationality.GetDataTable(Payperiod)

            If dtPayPeriod.Rows.Count > 0 Then
                For i As Integer = 0 To dtPayPeriod.Rows.Count - 1
                    Dim J As Integer = 0
                    If i = 0 Then
                        J = i
                        PayperiodCode += "  '" + clsCommon.myCstr(dtPayPeriod.Rows(i)("PAY_PERIOD_CODE")) + "' "
                    Else
                        PayperiodCode += ", '" + clsCommon.myCstr(dtPayPeriod.Rows(i)("PAY_PERIOD_CODE")) + "' "
                    End If
                    '(" & PayperiodCode & ")
                Next
            End If

            Dim payhead As String = Nothing
            Dim PayheadCode As String = Nothing
            Dim PayheadsCodes As String = Nothing
            payhead = "  SELECT DISTINCT TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE,TSPL_PAYHEAD_MASTER.ISEARNING,TSPL_PAYHEAD_MASTER.GROUP_SEQ
                        FROM TSPL_GENERATE_SALARY_PAYHEADS 
                        INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE
                        INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE = TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
                        WHERE TSPL_GENERATE_SALARY.PAY_PERIOD_CODE IN (" & PayperiodCode & ") and ACTUAL_AMOUNT<> 0 order by GROUP_SEQ "
            Dim dtPayhead As DataTable = clsDBFuncationality.GetDataTable(payhead)
            If dtPayhead.Rows.Count > 0 Then
                For i As Integer = 0 To dtPayhead.Rows.Count - 1
                    Dim J As Integer = 0
                    If i = 0 Then
                        J = i
                        PayheadCode += "  [" + clsCommon.myCstr(dtPayhead.Rows(i)("PAY_HEAD_CODE")) + "] "
                        PayheadsCodes += "IsNull([" + clsCommon.myCstr(dtPayhead.Rows(i)("PAY_HEAD_CODE")) + "],0) As [" + clsCommon.myCstr(dtPayhead.Rows(i)("PAY_HEAD_CODE")) + "]"
                    Else
                        PayheadCode += ", [" + clsCommon.myCstr(dtPayhead.Rows(i)("PAY_HEAD_CODE")) + "] "
                        PayheadsCodes += ", IsNull([" + clsCommon.myCstr(dtPayhead.Rows(i)("PAY_HEAD_CODE")) + "],0) As [" + clsCommon.myCstr(dtPayhead.Rows(i)("PAY_HEAD_CODE")) + "]"
                    End If
                    '(" & PayperiodCode & ")
                Next
            End If

            Dim qry As String = ""
            qry = " SELECT Emp_Name as [Employee Name],PAY_MONTH as Month," & PayheadsCodes & "  FROM ( SELECT TSPL_GENERATE_SALARY.PAY_PERIOD_CODE,emp.Emp_Name,'" + txtFinYear.Value + "' AS FinYear,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,
                    TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE  AS PayHead,TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT,RIGHT(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM), 2) AS PAY_YEAR,
                    MONTH(TSPL_PAYPERIOD_MASTER.DATE_FROM) AS GENERATE_MONTH,DATENAME(MONTH, TSPL_PAYPERIOD_MASTER.DATE_TO) AS PAY_MONTH, MONTH(TSPL_PAYPERIOD_MASTER.DATE_TO) AS Mon,
                    TSPL_PAYPERIOD_MASTER.DATE_TO FROM TSPL_GENERATE_SALARY_PAYHEADS 
                    LEFT JOIN tspl_employee_master EMP ON EMP.EMP_CODE = TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
                    INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  
                    INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE 
                    AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
                    LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
                    LEFT OUTER JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE = TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
                    LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = EMP.LOCATION_CODE
                    WHERE " + whrEmp + "  TSPL_GENERATE_SALARY.PAY_PERIOD_CODE IN (" & PayperiodCode & ")) AS src
                    PIVOT ( SUM(ACTUAL_AMOUNT)FOR PayHead IN (" & PayheadCode & " )) AS pvt  ORDER BY Mon "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.DataSource = dt
            gv1.BestFitColumns()
            SetGridFormation()
            RadPageView1.SelectedPage = RadPageViewPage2
            gv1.BestFitColumns()
            'EnableDisableControls(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).VisibleInColumnChooser = False
        Next
        Dim index As Integer
        Dim summaryRowItem As New GridViewSummaryRowItem()
        index = 2
        For ii As Integer = index To gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
End Class