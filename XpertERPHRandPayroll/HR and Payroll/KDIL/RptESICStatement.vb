'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptESICStatement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    ''against[BM00000008145]
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptESICStatement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub fndFromPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFromPeriod._MYValidating
        Try
            Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
           & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
            fndFromPeriod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", fndFromPeriod.Value, "", isButtonClicked)
            lblFromPeriodName.Text = clsPayPeriodMaster.GetName(fndFromPeriod.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadReport()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub LoadReport()
        Try
            'Dim FinalQry As String
            'Dim MainQry As String
            'Dim FromDate As Date
            'Dim ToDate As Date
            'FromDate = "01/04/" & (txtFromYear.Text)
            'ToDate = "31/03/" & (txtFromYear.Text + 1)
            'Dim EmployerShare As Double = 0
            'Dim Employer_Rate As Double = 0
            'Dim EmployeeShare As Double = 0
            'Dim PensionRate As Double
            'If clsCommon.myLen(fndLocationCode.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please select Location")
            '    Exit Sub
            'End If
            Dim LocationFirstTime As Integer = 0
            Dim LocAddress As String = ""
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
                LocationFirstTime += 1
                LocAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + add4 As [Address] FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"))
            Else
                LocAddress = objCommonVar.CurrentCompanyName
            End If
            'Dim RountOffType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT top 1 COESI_ROUNDOFF_YPE  FROM TSPL_ESI_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
            Dim CompESIC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_ESIC_NO from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            'Dim Comp_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            ''Employer_Rate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COESI_PER as Employer_Rate FROM TSPL_ESI_RULE_MASTER  where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
            'PensionRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COESI_PER FROM TSPL_ESI_RULE_MASTER  where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))

            'Dim qry As String = ""
            'Dim Cond As String = ""

            ''If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            ''    qry += " and TSPL_GENERATE_SALARY.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
            ''End If

            'If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            '    Cond = Cond & " and TSPL_GENERATE_SALARY.Location_Code='" & fndLocationCode.Value & "'"
            'End If
            ''If clsCommon.myLen(fndDevisionCode.Value) > 0 Then
            ''    Cond = Cond & " and TSPL_GENERATE_SALARY.Devision_Code='" & fndDevisionCode.Value & "'"
            ''End If
            'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            '    Cond = Cond & " and TSPL_GENERATE_SALARY.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
            'End If

            'qry = "select sum(xxx.ACTUAL_AMOUNT )as Employee_Share  from(select head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,detail.Month,RATE_AMOUNT,head.ACTUAL_AMOUNT , " & _
            '    " detail.Date_Of_Challan,head.PAYABLE_DAYS,head.Average_Daily_Wages,head.SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, " & _
            '    " case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2 ,head.Comp_Name ,head.City_Code  " & _
            '    " from (select TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER. add2,TSPL_COMPANY_MASTER.City_Code, ESI_NO," & _
            '    " Emp_Name,Joining_date ,RELIEVING_DATE as Left_Date,RATE_AMOUNT ,ACTUAL_AMOUNT,round(HEAD_VALUE,0)as HEAD_VALUE,SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE, " & _
            '    " PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages  from TSPL_GENERATE_SALARY_PAYHEADS " & _
            '    " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE " & _
            '    " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   " & _
            '    " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            '    " left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE  " & _
            '    " where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0  " & Cond & " )head   Left Join (select DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as Month," & _
            '    " max(GENERATE_DATE)as Date_Of_Challan,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  from TSPL_GENERATE_SALARY " & _
            '    " left join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE " & _
            '    " where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0  " & Cond & " group by GENERATE_DATE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE    )detail " & _
            '    " on head.SALARY_GENERATION_CODE=detail.SALARY_GENERATION_CODE)xxx"
            'EmployeeShare = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            'qry = " select "
            'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
            '    qry += " (ROUND(sum(xx.ACTUAL_AMOUNT ), 0))"
            'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
            '    qry += " (FLOOR(sum(xx.ACTUAL_AMOUNT )))"
            'Else
            '    qry += " (CEILING(sum(xx.ACTUAL_AMOUNT )))"
            'End If
            'qry += " as Employer_Share  from (select   head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,detail.Month,RATE_AMOUNT," & _
            '        " convert(decimal(18,2),head.ACTUAL_AMOUNT)as  ACTUAL_AMOUNT ,detail.Date_Of_Challan,head.PAYABLE_DAYS,head.Average_Daily_Wages,'COESI' as SUB_HEAD_TYPE,head.Joining_date ," & _
            '        " head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2 ,head.Comp_Name ,head.City_Code " & _
            '        " from (select TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER. add2,TSPL_COMPANY_MASTER.City_Code , ESI_NO,Emp_Name,Joining_date ," & _
            '        " RELIEVING_DATE as Left_Date,4.75 as Rate_Amount ,"

            'qry += "  ('" & PensionRate & "' * HEAD_VALUE /100) as ACTUAL_AMOUNT,HEAD_VALUE,SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE," & _
            '        " PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages  from TSPL_GENERATE_SALARY_PAYHEADS " & _
            '         " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  " & _
            '        " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            '        " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code " & _
            '        " left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE " & _
            '        " where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0  " & Cond & ")head " & _
            '        " Left Join (select DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as Month,max(GENERATE_DATE)as Date_Of_Challan,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " & _
            '        " from TSPL_GENERATE_SALARY left join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE" & _
            '        " where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 " & Cond & " group by GENERATE_DATE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE    )detail " & _
            '        " on head.SALARY_GENERATION_CODE=detail.SALARY_GENERATION_CODE)xx"
            'EmployerShare = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            'MainQry = "select distinct TSPL_DEVISION_MASTER.DEVISION_NAME ,"
            'If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            '    MainQry += " TSPL_LOCATION_MASTER.Location_Desc  "
            'Else
            '    MainQry += "' " + Comp_Name + " '"
            'End If
            'MainQry += " as Location_Desc,"
            'If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            '    MainQry += "TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End  "
            'Else
            '    MainQry += "''"
            'End If

            'MainQry += " as Location_Address,'" & EmployerShare & "' as Employer_Share,'" & EmployeeShare & "' as Employee_Share ,Emp_Name,ESI_NO ,Payable_Days as PAYPERIOD_DAYS,ACTUAL_AMOUNT,Round(HEAD_VALUE,0)as HEAD_VALUE,TSPL_COMPANY_MASTER.Comp_Name ,"

            ''If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
            ''    MainQry += " ROUND(('" & PensionRate & "')* HEAD_VALUE /100, 0)"
            ''ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
            ''    MainQry += " FLOOR(('" & PensionRate & "')* HEAD_VALUE /100)"
            ''Else
            ''    MainQry += " CEILING(('" & PensionRate & "')* HEAD_VALUE /100)"
            ''End If
            'MainQry += "  TSPL_GENERATE_SALARY_PAYHEADS.Co_ESI_AMT "
            'MainQry += "   as Employer ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.City_Code ,'" & fndFromPeriod.Value & "' AS FROM_PAY_PERIOD   from TSPL_GENERATE_SALARY_PAYHEADS"
            'MainQry += " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            'MainQry += " INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE "
            'MainQry += " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE  "
            'MainQry += "  INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE"
            'MainQry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code"
            'MainQry += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State"
            'MainQry += "  left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code"
            'MainQry += "  left outer join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_GENERATE_SALARY.DEVISION_CODE  "
            'MainQry += "  left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_GENERATE_SALARY_PAYHEADS.Emp_code "
            'MainQry += "   WHERE  TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='EMPESI' AND TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0 and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "'  " & Cond & ""
            ''If clsCommon.myLen(fndDevisionCode.Value) > 0 Then
            ''    MainQry += " and TSPL_EMPLOYEE_MASTER.Devision_Code='" & fndDevisionCode.Value & "' "
            ''End If
            'FinalQry = " select (select "
            'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
            '    FinalQry += " (ROUND( sum(xx.ACTUAL_AMOUNT),0))"
            'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
            '    FinalQry += " FLOOR ( sum(xx.ACTUAL_AMOUNT))"
            'Else
            '    FinalQry += " CEILING  (sum(xx.ACTUAL_AMOUNT))"
            'End If

            'FinalQry += " as Employer_Share  from (select   head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,detail.Month,RATE_AMOUNT,convert(decimal(18,2),head.ACTUAL_AMOUNT)as  ACTUAL_AMOUNT ,detail.Date_Of_Challan,head.PAYABLE_DAYS,head.Average_Daily_Wages,'COESI' as SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2 ,head.Comp_Name ,head.City_Code  from (select TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER. add2,TSPL_COMPANY_MASTER.City_Code , ESI_NO,Emp_Name,Joining_date ,RELIEVING_DATE as Left_Date,4.75 as Rate_Amount ,HEAD_VALUE * '" & PensionRate & "' /100 as ACTUAL_AMOUNT,HEAD_VALUE,SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages  from TSPL_GENERATE_SALARY_PAYHEADS  left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE  where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 )head  Left Join (select DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as Month,max(GENERATE_DATE)as Date_Of_Challan,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  from TSPL_GENERATE_SALARY left join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 group by GENERATE_DATE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE    )detail on head.SALARY_GENERATION_CODE=detail.SALARY_GENERATION_CODE)xx)+ (select sum(xxx.ACTUAL_AMOUNT )as Employee_Share  from(select head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,detail.Month,RATE_AMOUNT,head.ACTUAL_AMOUNT ,detail.Date_Of_Challan,head.PAYABLE_DAYS,head.Average_Daily_Wages,head.SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'YES' else 'No' end as Still_Working ,head.Add1 ,head.Add2 ,head.Comp_Name ,head.City_Code  from (select TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER. add2,TSPL_COMPANY_MASTER.City_Code, ESI_NO,Emp_Name,Joining_date ,RELIEVING_DATE as Left_Date,RATE_AMOUNT ,ACTUAL_AMOUNT,HEAD_VALUE,SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,(HEAD_VALUE/PAYABLE_DAYS)as Average_Daily_Wages  from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE  where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 )head   Left Join (select DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as Month,max(GENERATE_DATE)as Date_Of_Challan,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  from TSPL_GENERATE_SALARY left join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 group by GENERATE_DATE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE    )detail on head.SALARY_GENERATION_CODE=detail.SALARY_GENERATION_CODE)xxx) "
            'FinalQry += " as Total_Amount,tt.Employee_Share ,tt.Employer_Share,Emp_Name,ESI_NO, PAYPERIOD_DAYS,ACTUAL_AMOUNT,HEAD_VALUE,Comp_Name,Add1,Add2,City_Code,FROM_PAY_PERIOD ,DEVISION_NAME,Location_Desc,Location_Address,ACTUAL_AMOUNT+Employer as Total,Employer,'" + CompESIC + "' as Comp_ESIC_NO from (" & MainQry & ")tt"
            Dim qry As String = " select g.Comp_Name,EMP_CODE,Emp_Name,Joining_date,ESI_NO, PAYPERIOD_DAYS,ACTUAL_AMOUNT,HEAD_VALUE,FROM_PAY_PERIOD ,DEVISION_NAME,Location_Address,ACTUAL_AMOUNT+Employer as Total,Employer, Comp_ESIC_NO,g.Location_Code from "
            qry += " (select distinct TSPL_LOCATION_MASTER.ESIC_NO as  Comp_ESIC_NO,TSPL_COMPANY_MASTER.Comp_Name,TSPL_DEVISION_MASTER.DEVISION_NAME ,TSPL_GENERATE_SALARY.Location_Code,'" & LocAddress & "' as Location_Address ,TSPL_EMPLOYEE_MASTER.EMP_CODE ,Emp_Name,convert(varchar,Joining_date,103) as Joining_date ,TSPL_EMPLOYEE_MASTER.ESI_NO ,Payable_Days as PAYPERIOD_DAYS,ACTUAL_AMOUNT,Round(HEAD_VALUE,0)as HEAD_VALUE ,  round(TSPL_GENERATE_SALARY_PAYHEADS.Co_ESI_AMT,0)    as Employer  ,'" & fndFromPeriod.Value & "'  AS FROM_PAY_PERIOD   from TSPL_GENERATE_SALARY_PAYHEADS"
            qry += " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            qry += "  INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  "
            qry += " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE "
            qry += " INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE "
            qry += "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code"
            qry += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State "
            qry += " left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code "
            qry += "  left outer join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_GENERATE_SALARY.DEVISION_CODE  "
            qry += "  left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_GENERATE_SALARY_PAYHEADS.Emp_code    WHERE  TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='EMPESI' " '' AND TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0
            qry += " and TSPL_EMPLOYEE_MASTER.ISESI =1 "
            qry += " and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "'  "
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                qry += " and TSPL_GENERATE_SALARY.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
            End If

            qry += " )as g"


            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptESCIStatement", "ESIC Statement")
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RptESICStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        Try
            fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
            If clsCommon.myLen(fndLocationCode.Value) > 0 Then
                lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
            Else
                lblLocationName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDevisionCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDevisionCode._MYValidating
        Try
            Dim qry As String = "select DEVISION_CODE as Code, DEVISION_NAME as Name, DESCRIPTION as Description from TSPL_DEVISION_MASTER"
            fndDevisionCode.Value = clsCommon.ShowSelectForm("DEVISION_MASTER", qry, "Code", "", fndDevisionCode.Value, "DEVISION_CODE", isButtonClicked)
            If clsCommon.myLen(fndDevisionCode.Value) > 0 Then
                lblDevisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME from TSPL_DEVISION_MASTER where DEVISION_CODE='" & fndDevisionCode.Value & "'")
            Else
                lblDevisionName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        fndDevisionCode.Value = ""
        lblDevisionName.Text = ""
        fndLocationCode.Value = ""
        lblLocationName.Text = ""
        fndFromPeriod.Value = ""
        lblFromPeriodName.Text = ""
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Try
            Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
            txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulESCI", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Try
            Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') "
            txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMSelESI", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
