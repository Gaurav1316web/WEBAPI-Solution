'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptESICChallan
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptESICChallan)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadReport()
    End Sub
    Sub LoadReport()
        Dim FinalQry As String = String.Empty
        Dim MainQry As String
        Dim EmployerShare As Double = 0
        Dim Employer_Rate As Double = 0
        Dim EmployeeShare As Double = 0
        Dim strWages As String = ""
        Dim TotalWages As Double = 0
        Dim strEmployeeShare As String
        Dim strEmployerShare As String
        Dim PensionRate As Double
        Dim LocAddress As String = ""
        Dim LocationFirstTime As Integer = 0
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count = 1 Then
            LocationFirstTime += 1
            LocAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + add4 As [Address] FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"))
        Else
            LocAddress = objCommonVar.CurrentCompanyName
        End If
        Dim ESIC As String = ""
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count = 1 Then
            LocationFirstTime += 1
            ESIC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ESIC_NO FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"))
        Else
            ESIC = ""
        End If
        Dim RountOffType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT top 1 COESI_ROUNDOFF_YPE  FROM TSPL_ESI_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
        PensionRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COESI_PER FROM TSPL_ESI_RULE_MASTER  where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
        Dim Comp_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
        Dim CompESIC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_ESIC_NO from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
        strWages = "select sum(xxx.HEAD_VALUE  )as Total_Wages  from(select head.ESI_NO,head.Emp_Name,head.HEAD_VALUE," _
                               & " RATE_AMOUNT,head.ACTUAL_AMOUNT,head.PAYABLE_DAYS,head.Average_Daily_Wages,head.SUB_HEAD_TYPE,head.Joining_date ," _
                               & " head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2,head.Add3 ,head.City_Code,Location " _
                               & " from (select TSPL_COMPANY_MASTER.Comp_Name ,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER. add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.City_Code, TSPL_EMPLOYEE_MASTER.ESI_NO,Emp_Name,Joining_date ," _
                               & " RELIEVING_DATE as Left_Date,RATE_AMOUNT ,ACTUAL_AMOUNT,ROUND(HEAD_VALUE,0)AS HEAD_VALUE,TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages,Location_Desc as Location " _
                               & "  from TSPL_GENERATE_SALARY_PAYHEADS " _
                            & " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE " _
                            & " INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE " _
                           & " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " _
                            & " INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE  " _
                           & "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code " _
                             & " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State " _
                          & " left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code " _
                         & " left outer join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_GENERATE_SALARY.DEVISION_CODE " _
                        & "  left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_GENERATE_SALARY_PAYHEADS.Emp_code  where TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 "
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strWages += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            strWages += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        strWages &= " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "')head  )xxx"
        TotalWages = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strWages))

        'strEmployeeShare = "select sum(xxx.ACTUAL_AMOUNT )as Employee_Share  from(select head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,detail.Month,RATE_AMOUNT,head.ACTUAL_AMOUNT ,detail.Date_Of_Challan,head.PAYABLE_DAYS,head.Average_Daily_Wages,head.SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2 ,head.Add3 ,head.City_Code,Location  from (select TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER. add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.City_Code, ESI_NO,Emp_Name,Joining_date ,RELIEVING_DATE as Left_Date,RATE_AMOUNT ,ACTUAL_AMOUNT,HEAD_VALUE,SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages,Location_Desc as Location  from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_code left join TSPL_DEVISION_MASTER on  TSPL_DEVISION_MASTER.devision_code=TSPL_GENERATE_SALARY.devision_code where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0"
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    strEmployeeShare += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        'End If
        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    strEmployeeShare += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        'End If
        'strEmployeeShare &= " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "')head   Left Join (select DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as Month,max(GENERATE_DATE)as Date_Of_Challan,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  from TSPL_GENERATE_SALARY left join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_code left join TSPL_DEVISION_MASTER on  TSPL_DEVISION_MASTER.devision_code=TSPL_GENERATE_SALARY.devision_code where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 "
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    strEmployeeShare += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        'End If
        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    strEmployeeShare += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        'End If
        'strEmployeeShare &= " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "' group by GENERATE_DATE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE    )detail on head.SALARY_GENERATION_CODE=detail.SALARY_GENERATION_CODE)xxx"
        strEmployeeShare = "select sum(xxx.ACTUAL_AMOUNT )as Employee_Share  from(select head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,RATE_AMOUNT,head.ACTUAL_AMOUNT ,head.PAYABLE_DAYS,head.Average_Daily_Wages,head.SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2 ,head.Add3 ,head.City_Code,Location  from (select TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER. add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.City_Code, TSPL_EMPLOYEE_MASTER.ESI_NO,Emp_Name,Joining_date ,RELIEVING_DATE as Left_Date,RATE_AMOUNT ,ACTUAL_AMOUNT,HEAD_VALUE,TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages,Location_Desc as Location   from TSPL_GENERATE_SALARY_PAYHEADS "
        strEmployeeShare += " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE  "
        strEmployeeShare += " INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE "
        strEmployeeShare += " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE "
        strEmployeeShare += " INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE   "
        strEmployeeShare += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code "
        strEmployeeShare += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  "
        strEmployeeShare += " left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code   "
        strEmployeeShare += " left outer join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_GENERATE_SALARY.DEVISION_CODE   "
        strEmployeeShare += " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_GENERATE_SALARY_PAYHEADS.Emp_code where TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0"
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strEmployeeShare += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            strEmployeeShare += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        strEmployeeShare &= " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "')head  )xxx"
        EmployeeShare = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strEmployeeShare))

        'strEmployerShare = " select  sum(xx.ACTUAL_AMOUNT ) as Employer_Share  from (select   head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,detail.Month,RATE_AMOUNT,convert(decimal(18,2),head.ACTUAL_AMOUNT)as  ACTUAL_AMOUNT ,detail.Date_Of_Challan,head.PAYABLE_DAYS,head.Average_Daily_Wages,'COESI' as SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2 ,head.Add3 ,head.City_Code,Location  from (select TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER. add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.City_Code , ESI_NO,Emp_Name,Joining_date ,RELIEVING_DATE as Left_Date,4.75 as Rate_Amount, "

        'strEmployerShare += " Co_ESI_AMT as ACTUAL_AMOUNT,HEAD_VALUE,SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages,Location_Desc as Location  from TSPL_GENERATE_SALARY_PAYHEADS  left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_code left join TSPL_DEVISION_MASTER on  TSPL_DEVISION_MASTER.devision_code=TSPL_GENERATE_SALARY.devision_code where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 "
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    strEmployerShare += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        'End If
        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    strEmployerShare += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        'End If
        'strEmployerShare &= " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "' )head  Left Join (select DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as Month,max(GENERATE_DATE)as Date_Of_Challan,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  from TSPL_GENERATE_SALARY left join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_code left join TSPL_DEVISION_MASTER on  TSPL_DEVISION_MASTER.devision_code=TSPL_GENERATE_SALARY.devision_code where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 "
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    strEmployerShare += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        'End If
        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    strEmployerShare += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        'End If
        'strEmployerShare &= "and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "'  group by GENERATE_DATE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE    )detail on head.SALARY_GENERATION_CODE=detail.SALARY_GENERATION_CODE)xx"
        strEmployerShare = " select  sum(xx.ACTUAL_AMOUNT ) as Employer_Share  from (select   head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,RATE_AMOUNT,convert(decimal(18,2),head.ACTUAL_AMOUNT)as  ACTUAL_AMOUNT ,head.PAYABLE_DAYS,head.Average_Daily_Wages,'COESI' as SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2 ,head.Add3 ,head.City_Code,Location  from (select TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER. add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.City_Code , TSPL_EMPLOYEE_MASTER.ESI_NO,Emp_Name,Joining_date ,RELIEVING_DATE as Left_Date,4.75 as Rate_Amount, "

        strEmployerShare += " Co_ESI_AMT as ACTUAL_AMOUNT,HEAD_VALUE,TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages,Location_Desc as Location  from TSPL_GENERATE_SALARY_PAYHEADS "
        strEmployerShare += " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE  "
        strEmployerShare += " INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE "
        strEmployerShare += " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE "
        strEmployerShare += " INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE   "
        strEmployerShare += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code "
        strEmployerShare += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  "
        strEmployerShare += " left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code   "
        strEmployerShare += " left outer join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_GENERATE_SALARY.DEVISION_CODE   "
        strEmployerShare += " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_GENERATE_SALARY_PAYHEADS.Emp_code where TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 "
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strEmployerShare += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            strEmployerShare += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        strEmployerShare &= " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "' )head )xx"
        EmployerShare = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strEmployerShare))
        MainQry = "select round('" & EmployerShare & "',0) as Employer_Share,'" & EmployeeShare & "' as Employee_Share,'" & TotalWages & "' as Total_Wages ,Emp_Name,TSPL_EMPLOYEE_MASTER.ESI_NO ,PAYPERIOD_DAYS,ACTUAL_AMOUNT,ROUND(HEAD_VALUE ,0)AS HEAD_VALUE,"

        MainQry += " '" & LocAddress & "' as Location,'" & fndFromPeriod.Value & "' AS FROM_PAY_PERIOD,NearestCity,ESIC_No from TSPL_GENERATE_SALARY_PAYHEADS"
        MainQry += " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
        MainQry += " INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE "
        MainQry += " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE  "
        MainQry += "  INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE"
        MainQry += "  left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code"
        MainQry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_code"
        MainQry += " left join TSPL_DEVISION_MASTER on  TSPL_DEVISION_MASTER.devision_code=TSPL_GENERATE_SALARY.devision_code"
        MainQry += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State"
        MainQry += "  WHERE  TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='EMPESI'  " 'AND TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0 
        ' MainQry += " AND TSPL_EMPLOYEE_MASTER.ISPF=1 "
        MainQry += " and TSPL_GENERATE_SALARY_PAYHEADS.ESI_Applicable  = 1 "
        MainQry += " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "'"
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            MainQry += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            MainQry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If

        FinalQry += " select tt.Employee_Share ,tt.Employer_Share,Total_Wages,Emp_Name,ESI_NO, PAYPERIOD_DAYS,ACTUAL_AMOUNT,HEAD_VALUE,Location,FROM_PAY_PERIOD,'" + CompESIC + "' as Comp_ESIC_No,NearestCity,'" & ESIC & "' as ESIC_No from (" & MainQry & ")tt"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(FinalQry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptESICChallan", "ESIC Challan Report")
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub fndFromPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFromPeriod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
         & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        fndFromPeriod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", fndFromPeriod.Value, "", isButtonClicked)
        lblFromPeriodName.Text = clsPayPeriodMaster.GetName(fndFromPeriod.Value, Nothing)
    End Sub

    Private Sub RptESICChallan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")

    End Sub

    'Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
    '    fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
    '    If clsCommon.myLen(fndLocationCode.Value) > 0 Then
    '        lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
    '    Else
    '        lblLocationName.Text = ""
    '    End If
    'End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMSelESI", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
End Class
