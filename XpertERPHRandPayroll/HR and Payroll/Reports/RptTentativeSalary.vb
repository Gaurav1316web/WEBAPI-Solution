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

    Private Sub RptTentativeSalary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            PageSetupReport_ID = Form_ID
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

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
        Try
            txtmultiEmpcode.arrValueMember = Nothing
            txtFinYear.Value = ""
            lblFinYear.Text = ""

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtFinYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Finacial Year !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            Dim whr As String = ""
            Dim whrEmp As String = ""
            Dim WHEEMPBONUS As String = ""
            If txtmultiEmpcode.arrValueMember IsNot Nothing AndAlso txtmultiEmpcode.arrValueMember.Count > 0 Then
                whr = " in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ")"
                whrEmp = " EMP.EMP_CODE in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ") And "
                WHEEMPBONUS = " TSPL_EMPLOYEE_MASTER.EMP_CODE in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ") "
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
					 union all 

                     Select TSPL_EMPLOYEE_BONUS.PAYABLE_PAY_PERIOD_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EMPLOYEE_MASTER.EMP_CODE,'25-26' AS FinYear,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,
                    TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.Location_Desc,'BONUS' AS PayHead,Final_BONUS_AMOUNT as ACTUAL_AMOUNT,RIGHT(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM), 2) AS PAY_YEAR,
                    MONTH(TSPL_PAYPERIOD_MASTER.DATE_FROM) AS GENERATE_MONTH,DATENAME(MONTH, TSPL_PAYPERIOD_MASTER.DATE_TO) AS PAY_MONTH, MONTH(TSPL_PAYPERIOD_MASTER.DATE_TO) AS Mon,
                    TSPL_PAYPERIOD_MASTER.DATE_TO ,TSPL_EMPBONUS_DETAIL.Final_BONUS_AMOUNT AS PAYABLE_AMOUNT from TSPL_EMPLOYEE_BONUS
                     left join TSPL_EMPBONUS_DETAIL on TSPL_EMPBONUS_DETAIL.EMP_BONUS_CODE=TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE
                     left join TSPL_EMPLOYEE_MASTER  ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EMPBONUS_DETAIL.EMP_CODE  
                     LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = TSPL_EMPLOYEE_BONUS.PAYABLE_PAY_PERIOD_CODE
                     LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_EMPLOYEE_MASTER.LOCATION_CODE
                     WHERE  " + WHEEMPBONUS + "  And TSPL_EMPLOYEE_BONUS.PAYABLE_PAY_PERIOD_CODE IN ( " & PayperiodCode & "  ) and Final_BONUS_AMOUNT<>0


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
            If clsCommon.myLen(txtFinYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Finacial Year !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
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
            qry = " SELECT EMPCODE,Emp_Name as [Employee Name],PAY_MONTH as Month," & PayheadsCodes & ", ISNULL([BONUS], 0) AS [BONUS],ISNULL(OT_AMOUNT, 0) as [OT AMOUNT]   FROM ( SELECT EMP.EMP_CODE AS EMPCODE,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE,emp.Emp_Name,'" + txtFinYear.Value + "' AS FinYear,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,
                    TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE  AS PayHead,TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT,RIGHT(YEAR(TSPL_PAYPERIOD_MASTER.DATE_FROM), 2) AS PAY_YEAR,
                    MONTH(TSPL_PAYPERIOD_MASTER.DATE_FROM) AS GENERATE_MONTH,DATENAME(MONTH, TSPL_PAYPERIOD_MASTER.DATE_TO) AS PAY_MONTH, MONTH(TSPL_PAYPERIOD_MASTER.DATE_TO) AS Mon,
                    TSPL_PAYPERIOD_MASTER.DATE_TO,ISNULL(TSPL_EMPBONUS_DETAIL.Final_BONUS_AMOUNT,0) AS BONUS_AMOUNT,ISNULL(TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Amount,0) AS OT_AMOUNT FROM TSPL_GENERATE_SALARY_PAYHEADS 
                    LEFT JOIN tspl_employee_master EMP ON EMP.EMP_CODE = TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
                    INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  
                    INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE 
                    AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
                    LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
                    LEFT OUTER JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE = TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE
                    LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = EMP.LOCATION_CODE
                    LEFT OUTER JOIN TSPL_EMPLOYEE_BONUS on TSPL_EMPLOYEE_BONUS.PAYABLE_PAY_PERIOD_CODE=TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
					LEFT OUTER JOIN TSPL_EMPBONUS_DETAIL  ON TSPL_EMPBONUS_DETAIL.EMP_CODE = TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE
                    left outer join TSPL_EMPLOYEE_OT_ENTRY_HEAD ON TSPL_EMPLOYEE_OT_ENTRY_HEAD.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE
					left outer join TSPL_EMPLOYEE_OT_ENTRY_DETAIL ON TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Document_Code =TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code and TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE
                    WHERE " + whrEmp + "  TSPL_GENERATE_SALARY.PAY_PERIOD_CODE IN (" & PayperiodCode & ")) AS src
                    PIVOT ( SUM(ACTUAL_AMOUNT)FOR PayHead IN (" & PayheadCode & " )) AS pvt  
                    LEFT JOIN (SELECT EMP_CODE, PAYABLE_PAY_PERIOD_CODE, SUM(Final_BONUS_AMOUNT) AS BONUS FROM TSPL_EMPLOYEE_BONUS
					left join TSPL_EMPBONUS_DETAIL on TSPL_EMPBONUS_DETAIL.EMP_BONUS_CODE=TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE
					GROUP BY EMP_CODE, PAYABLE_PAY_PERIOD_CODE ) AS B ON B.EMP_CODE = pvt.EMPCODE AND B.PAYABLE_PAY_PERIOD_CODE = pvt.PAY_PERIOD_CODE
                    ORDER BY Mon "

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
        Try
            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = True
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).VisibleInColumnChooser = False
            Next
            gv1.Columns("EMPCODE").IsVisible = False
            Dim index As Integer
            Dim summaryRowItem As New GridViewSummaryRowItem()
            index = 2
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Try
            If gv1 Is Nothing OrElse gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            ExportGrid(EnumExportTo.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim rptName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTentativeReport & "'"))
            arrHeader.Add("Name : " & rptName)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtFinYear.Value IsNot Nothing AndAlso clsCommon.myLen(txtFinYear.Value) > 0 Then
                arrHeader.Add("Finacial Year : " & lblFinYear.Text)
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel(rptName, gv1, arrHeader, rptName)
            Else
                clsCommon.MyExportToPDF(rptName, gv1, arrHeader, rptName, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class