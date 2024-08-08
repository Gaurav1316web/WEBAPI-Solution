Imports common
Imports XpertERPEngine
Public Class RptTentativeSalary
    Inherits FrmMainTranScreen

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtFinYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFinYear._MYValidating
        Try
            Dim qry As String = "SELECT Fiscal_Code as Code , Fiscal_Name as Name FROM TSPL_Fiscal_Year_Master "
            txtFinYear.Value = clsCommon.ShowSelectForm("FinYear", qry, "Code", "", txtFinYear.Value, "Code", isButtonClicked)
            lblFinYear.Text = clsDBFuncationality.getSingleValue("Select Fiscal_Name  from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYear.Value + "' ")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtEmpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        Try
            Dim qry As String = "select emp_code as Code,Emp_Name as Name,Designation,Birth_date as [Birth Date],Joining_date as [Joining Date] from TSPL_EMPLOYEE_MASTER"
            txtEmpCode.Value = clsCommon.ShowSelectForm("EmpCode", qry, "Code", "", txtEmpCode.Value, "Code", isButtonClicked)
            lblEmpName.Text = clsDBFuncationality.getSingleValue("Select Emp_Name from TSPL_EMPLOYEE_MASTER where emp_code='" + txtEmpCode.Value + "' ")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtEmpCode.Value = ""
        lblEmpName.Text = ""
        txtFinYear.Value = ""
        lblFinYear.Text = ""
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim dt As DataTable = Nothing
            Dim FinalQry As String = ""
            Dim finYear As String = txtFinYear.Value
            Dim firstPart As String = finYear.Substring(0, 2)
            Dim GetLastGenerateMonth As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select top 1 MONTH(DATE_FROM)AS GENERATE_MONTH from TSPL_PAYPERIOD_MASTER Inner Join TSPL_GENERATE_SALARY On TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE where TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE='" + txtEmpCode.Value + "' Order By DATE_FROM desc"))
            Dim GetLastMonthName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select top 1 DATENAME(MONTH,TSPL_PAYPERIOD_MASTER.DATE_FROM )AS PAY_MONTH from TSPL_PAYPERIOD_MASTER Inner Join TSPL_GENERATE_SALARY On TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE where TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE='" + txtEmpCode.Value + "' Order By DATE_FROM desc "))
            Dim Qry As String = "select emp.Emp_Name,'" + txtFinYear.Value + "' as FinYear,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_PAYHEAD_MASTER.ISEARNING,case when ISEARNING=1 then 'A'+ ' ' +TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE else 'D'+' '+TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE end  as PayHead,right(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM),2) AS PAY_YEAR,MONTH(DATE_FROM)AS GENERATE_MONTH,DATENAME(MONTH,TSPL_PAYPERIOD_MASTER.DATE_FROM )AS PAY_MONTH,0 as Mon,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE,TSPL_GENERATE_SALARY_PAYHEADS.* from TSPL_GENERATE_SALARY_PAYHEADS 
            left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
            INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  
            inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
            LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
            left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
            left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=EMP.LOCATION_CODE
            where  EMP.EMP_CODE='" + txtEmpCode.Value + "' and right(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM),2) ='" + firstPart + "' and MONTH(DATE_FROM) not in (1,2,3) "

            If GetLastGenerateMonth > 0 Then
                Dim m As Integer = (GetLastGenerateMonth + 1)
                Dim FromMonth As Integer = 12
                For i As Integer = m To FromMonth

                    Qry += " Union All 
                             select emp.Emp_Name, '" + txtFinYear.Value + "' as FinYear,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_PAYHEAD_MASTER.ISEARNING,case when ISEARNING=1 then 'A'+ ' ' +TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE else 'D'+' '+TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE end  as PayHead,right(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM),2) AS PAY_YEAR,MONTH(DATE_FROM)AS GENERATE_MONTH, '" + MonthName(m) + "'AS PAY_MONTH,'" + clsCommon.myCstr(m) + "' as Mon,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE,TSPL_GENERATE_SALARY_PAYHEADS.* from TSPL_GENERATE_SALARY_PAYHEADS 
            left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
            INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  
            inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
            LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
            left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
            left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=EMP.LOCATION_CODE
            where  EMP.EMP_CODE='" + txtEmpCode.Value + "' And TSPL_GENERATE_SALARY.PAY_PERIOD_CODE IN ('" + GetLastMonthName + "') and right(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM),2) ='" + firstPart + "'"
                    m += 1

                    If i = 12 Then
                        i = 1
                        m = 1
                        FromMonth = 3
                    End If
                    If i > FromMonth Then
                        Exit For
                    End If
                Next
                FinalQry = "Select * from (" & Qry & ")YYY order by ISEARNING desc"
            End If
            dt = clsDBFuncationality.GetDataTable(FinalQry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.HRPayroll, dt, "rptTentativeSalary", "Tentative Salary", Nothing)
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

End Class