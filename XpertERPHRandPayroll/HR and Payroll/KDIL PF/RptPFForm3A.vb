'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptPFForm3A
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPFForm3A)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub fndDivisionCode__MY_click(sender As Object, e As EventArgs) Handles fndDivisionCode._My_Click
        Try
            Dim qry As String = "select DEVISION_CODE as Code, DEVISION_NAME as Name, DESCRIPTION as Description from TSPL_DEVISION_MASTER"
            fndDivisionCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulDev", qry, "Code", "Name", fndDivisionCode.arrValueMember, fndDivisionCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RptPFForm3A_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
            txtFromYear.Text = clsCommon.GETSERVERDATE()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadReport()
        '=====================changes by shivani against ticket no [BM00000008841]
        Try
            Dim FromDate As Date
            Dim ToDate As Date
            FromDate = "01/04/" & (txtFromYear.Text)
            ToDate = "31/03/" & (txtFromYear.Text + 1)
            'Dim PensionRate As Double
            'Dim StatutoryRate As Double
            Dim Qry As String
            Dim TotalAmountWages As Double
            Dim TotalWorkerShare As Double
            Dim TotalEmployerShare As Double
            Dim TotalPensionFund As Double
            Dim MainQry As String
            Dim TotalContribution As Double
            Dim TotalDays As Double
            Dim lstFPRule As List(Of String) = Nothing

            Dim lstPFQry As String = "(Select ISNull((COEPS_PER),0)COEPS_PER,ISNull((COEPF_PER),0)COEPF_PER,EPS_MAX  From TSPL_PF_RULE_MASTER Where PFRULE_CODE IN ( select PFRULE_CODE from TSPL_PF_RULE_MASTER  
                      where APPLICABLE_FROM<=(select top 1 DATE_FROM from TSPL_PAYPERIOD_MASTER where convert(date,TSPL_PAYPERIOD_MASTER.DATE_TO,103)>=convert(date,'" & FromDate.ToString & "',103)  and convert(date,TSPL_PAYPERIOD_MASTER.DATE_TO,103) <=convert(date,'" & ToDate.ToString & "' ,103) Order By DATE_FROM) 
                      GROUP BY PFRULE_CODE ))"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(lstPFQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                lstFPRule = New List(Of String)
                lstFPRule.Add(clsCommon.myCstr(dt.Rows(0)("COEPS_PER")))
                lstFPRule.Add(clsCommon.myCstr(dt.Rows(0)("COEPF_PER")))
                lstFPRule.Add(clsCommon.myCstr(dt.Rows(0)("EPS_MAX")))
            End If

            'Dim RountOffType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPF_ROUNDOFF_YPE  FROM TSPL_PF_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= convert(date,'" + ToDate + "',103) ORDER BY APPLICABLE_FROM Desc;"))

            'StatutoryRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 EMPEPF_PER  FROM TSPL_PF_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= convert(date,'" + ToDate + "',103) ORDER BY APPLICABLE_FROM Desc;"))
            'PensionRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPS_PER FROM TSPL_PF_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= convert(date,'" + ToDate + "',103) ORDER BY APPLICABLE_FROM Desc;"))
            'Qry = "select * from(select * from (select * from (select * from (select '" & StatutoryRate & "' as Employee_pf_per,'" & EmployerRate & "' as Employee_Share_Rate ,Emp_Name,max(FATHERS_NAME) as FATHERS_NAME "
            'Qry += " ,max(BANK_ACC_NO) as BANK_ACC_NO,MAX( MonthlyAmt) as MonthlyAmt ,max (MonthlyShare) as MonthlyShare,max(monthlyEmployer) as monthlyEmployer,MAX(monthlyPension)as monthlyPension"
            'Qry += " ,max(Comp_Name)as Comp_Name ,max(Add1) as Add1,max(Add2) as Add2 ,max(City_Code) as City_Code ,HEAD_VALUE,max(ACTUAL_AMOUNT) as ACTUAL_AMOUNT ,max(pension_fund)as pension_fund,'" & FromDate.Year & "' as From_Year,'" & ToDate.Year & "' as To_Year"
            'Qry += " ,max(Employer_Share) as Employer_Share from (select Emp_Name,FATHERS_NAME,BANK_ACC_NO,tt._Month+'Amt'  as MonthlyAmt ,tt._Month+'Share' as MonthlyShare,tt. _Month+'Employer' as monthlyEmployer ,tt. _Month+'Pension' as monthlyPension"
            'Qry += ",Comp_Name,Add1,Add2,City_Code,HEAD_VALUE,ACTUAL_AMOUNT,(ACTUAL_AMOUNT - Employer_Share)as pension_fund ,Employer_Share from (select Emp_Name ,FATHERS_NAME ,BANK_ACC_NO ,Comp_Name ,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Add2 "
            'Qry += ",City_Code,HEAD_VALUE ,ACTUAL_AMOUNT ,(('" & EmployerRate & "')*HEAD_VALUE /100)as Employer_Share,DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as _Month,TSPL_GENERATE_SALARY.GENERATE_DATE   from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on"
            'Qry += "  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE "
            'Qry += " left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE  where SUB_HEAD_TYPE in ('EPF')and ACTUAL_AMOUNT > 0 and Emp_Name ='" & lblEmployeeName.Text & "' and   convert(date,GENERATE_DATE,103)>=convert(date,'" & FromDate.ToString & "',103) "
            'Qry += " and convert(date,GENERATE_DATE,103) <=convert(date,'" & ToDate.ToString & "' ,103))tt )as dd group by HEAD_VALUE ,MonthlyAmt,Emp_Name )as s     Pivot(max(  HEAD_VALUE) FOR [MonthlyAmt] IN (AprilAmt,MayAmt,JuneAmt,JulyAmt,AugustAmt,SeptemberAmt,OctoberAmt,NovemberAmt,DecemberAmt, JanuaryAmt, FebruaryAmt,MarchAmt) )AS pivott) as g Pivot("
            'Qry += "  MAX(ACTUAL_AMOUNT) FOR [MonthlyShare] IN (AprilShare,MayShare,JuneShare,JulyShare,AugustShare,SeptemberShare,OctoberShare,NovemberShare,DecemberShare, JanuaryShare, FebruaryShare,MarchShare) )AS pivotr)AS m Pivot( MAX(Employee_Share_Rate) FOR [monthlyEmployer] IN (AprilEmployer,MayEmployer,AugustEmployer,JuneEmployer,JulyEmployer,SeptemberEmployer,OctoberEmployer"
            'Qry += " ,NovemberEmployer,JanuaryEmployer, FebruaryEmployer, MarchEmployer,DecemberEmployer) )AS pivo)as n Pivot( MAX(pension_fund) FOR monthlyPension IN (AprilPension,MayPension,JunePension,JulyPension,AugustPension,SeptemberPension,OctoberPension,NovemberPension,JanuaryPension, FebruaryPension, MarchPension,DecemberPension) )AS pivo"

            Qry = " select * from(select * from(select * from(select * from (select * from (select * from (select '12' as Employee_pf_per,Emp_Name,max(FATHERS_NAME) as FATHERS_NAME  ,max(BANK_ACC_NO) as BANK_ACC_NO,MAX( MonthlyAmt) as MonthlyAmt ,max (MonthlyShare) as MonthlyShare,max(monthlyEmployer) as monthlyEmployer,MAX(monthlyPension)as monthlyPension ,max(Comp_Name)as Comp_Name ,max(Add1) as Add1,max(Add2) as Add2,max(location_desc) As [Location_Desc] "
            Qry += " ,max(City_Code) as City_Code , HEAD_VALUE,max(ACTUAL_AMOUNT) as ACTUAL_AMOUNT ,max(pension_fund)as pension_fund,'" & FromDate.Year & "' as From_Year,'" & ToDate.Year & "' as To_Year ,max(Employer_Share) as Employer_Share,max(RELIEVING_DATE)as RELIEVING_DATE,max(LEAVING_REASON)as LEAVING_REASON,max(monthlyDays)as monthlyDays,max(ABSENT_DAYS)as ABSENT_DAYS,MAX(EPF_Rate) As EPF_Rate ,MAX(Rate_Amount) AS Rate_Amount,MAX(PF_No) AS PF_No,max(Emp_PF_NO) as Emp_PF_NO  from (select Emp_Name,FATHERS_NAME,BANK_ACC_NO,tt._Month+'Amt'  as MonthlyAmt ,tt._Month+'Share' as MonthlyShare,tt. _Month+'Employer' as monthlyEmployer ,tt. _Month+'Pension' as monthlyPension,"
            Qry += " Comp_Name,Add1,Add2,location_desc,City_Code, HEAD_VALUE,ACTUAL_AMOUNT,(((HEAD_VALUE*" & clsCommon.myCdbl(lstFPRule(0)) & ") /100))+(Case When ((HEAD_VALUE*" & clsCommon.myCdbl(lstFPRule(0)) & ") /100)>" & clsCommon.myCdbl(lstFPRule(2)) & " Then  ((HEAD_VALUE*" & clsCommon.myCdbl(lstFPRule(1)) & ") /100)-" & clsCommon.myCdbl(lstFPRule(2)) & " Else 0 End )as Employer_Share ,pension_fund,RELIEVING_DATE,LEAVING_REASON,tt. _Month+'Days' as monthlyDays ,ABSENT_DAYS,EPF_Rate,Rate_Amount,PF_No,Emp_PF_NO from "

            Qry += "(select Emp_Name ,FATHERS_NAME ,BANK_ACC_NO ,Comp_Name ,TSPL_LOCATION_MASTER.Add1 ,TSPL_LOCATION_MASTER.Add2 ,TSPL_LOCATION_MASTER.location_desc,TSPL_LOCATION_MASTER.City_Code,EmpWages.Wages as HEAD_VALUE ,ACTUAL_AMOUNT ,"

            Qry += " Case When ((EmpWages.Wages*" & clsCommon.myCdbl(lstFPRule(1)) & ") /100)>" & clsCommon.myCdbl(clsCommon.myCdbl(lstFPRule(2))) & " Then " & clsCommon.myCdbl(clsCommon.myCdbl(lstFPRule(2))) & " Else ((EmpWages.Wages*" & clsCommon.myCdbl(lstFPRule(1)) & ") /100) End  as pension_fund, "
            Qry += " DATENAME (MONTH ,CONVERT(date,Date_from,103))as _Month,TSPL_PAYPERIOD_MASTER.DATE_TO as GENERATE_DATE ,convert(date,RELIEVING_DATE,103) as RELIEVING_DATE,LEAVING_REASON,ABSENT_DAYS,coalesce(TSPL_GENERATE_SALARY_PAYHEADS.EPF_Rate,0) as EPF_Rate,ISNULL(CoEPF_Rate_Ac01,0)+ISNULL(CoEPS_RATE_AC10,0) As Rate_Amount ,ISNULL(TSPL_LOCATION_MASTER.PF_NO ,'') AS [PF_No],isnull(TSPL_EMPLOYEE_MASTER.PF_NO,'') as Emp_PF_NO from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE "
            Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_MASTER.Location_Code "
            Qry += "  left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE "
            Qry += "   left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.Pay_Period_Code=TSPL_GENERATE_SALARy.Pay_Period_Code"
            Qry += " Left Join (select (Case When SUB_HEAD_TYPE='Basic' Then ACTUAL_AMOUNT Else 0 End)+(Case When SUB_HEAD_TYPE='DA' Then ACTUAL_AMOUNT Else 0 End)Wages,TSPL_EMPLOYEE_MASTER.EMP_CODE,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
                    from TSPL_GENERATE_SALARY_PAYHEADS 
                    left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
                    left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   
                    left join TSPL_GENERATE_SALARY_ATTENDANCE ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
                    left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_MASTER.Location_Code   
                    left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE    
                    left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.Pay_Period_Code=TSPL_GENERATE_SALARy.Pay_Period_Code   
                    where SUB_HEAD_TYPE IN ('BASIC','DA') And ACTUAL_AMOUNT > 0 and   convert(date,TSPL_PAYPERIOD_MASTER.DATE_TO,103)>=convert(date,'" & FromDate.ToString & "',103)  and convert(date,TSPL_PAYPERIOD_MASTER.DATE_TO,103) <=convert(date,'" & ToDate.ToString & "' ,103)"
            If FndEmployeeMult.arrValueMember IsNot Nothing AndAlso FndEmployeeMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_GENERATE_SALARY_PAYHEADS.Emp_Code  in (" + clsCommon.GetMulcallString(FndEmployeeMult.arrValueMember) + ") "
            End If
            If fndDivisionCode.arrValueMember IsNot Nothing AndAlso fndDivisionCode.arrValueMember.Count > 0 Then
                Qry += "and TSPL_GENERATE_SALARy.DEVISION_CODE in (" + clsCommon.GetMulcallString(fndDivisionCode.arrValueMember) + ")"
            End If
            If fndLocationCode.arrValueMember IsNot Nothing AndAlso fndLocationCode.arrValueMember.Count > 0 Then
                Qry += " and TSPL_GENERATE_SALARy.LOCATION_CODE in (" + clsCommon.GetMulcallString(fndLocationCode.arrValueMember) + ") "
            End If
            Qry += ")EmpWages on EmpWages.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE And EmpWages.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE "
            Qry += "   where SUB_HEAD_TYPE in ('EPF')and ACTUAL_AMOUNT > 0 and   convert(date,TSPL_PAYPERIOD_MASTER.DATE_TO,103)>=convert(date,'" & FromDate.ToString & "',103)  and convert(date,TSPL_PAYPERIOD_MASTER.DATE_TO,103) <=convert(date,'" & ToDate.ToString & "' ,103)"

            If FndEmployeeMult.arrValueMember IsNot Nothing AndAlso FndEmployeeMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_GENERATE_SALARY_PAYHEADS.Emp_Code  in (" + clsCommon.GetMulcallString(FndEmployeeMult.arrValueMember) + ") "
            End If
            If fndDivisionCode.arrValueMember IsNot Nothing AndAlso fndDivisionCode.arrValueMember.Count > 0 Then
                Qry += "and TSPL_GENERATE_SALARy.DEVISION_CODE in (" + clsCommon.GetMulcallString(fndDivisionCode.arrValueMember) + ")"
            End If
            If fndLocationCode.arrValueMember IsNot Nothing AndAlso fndLocationCode.arrValueMember.Count > 0 Then
                Qry += " and TSPL_GENERATE_SALARy.LOCATION_CODE in (" + clsCommon.GetMulcallString(fndLocationCode.arrValueMember) + ") "
            End If

            Qry += ")tt )as dd group by HEAD_VALUE ,MonthlyAmt,Emp_Name )as s     Pivot(max(  HEAD_VALUE) FOR [MonthlyAmt]"
            Qry += " IN (AprilAmt,MayAmt,JuneAmt,JulyAmt,AugustAmt,SeptemberAmt,OctoberAmt,NovemberAmt,DecemberAmt, JanuaryAmt, FebruaryAmt,MarchAmt) )AS pivott) as g Pivot(  MAX(ACTUAL_AMOUNT) FOR [MonthlyShare] IN (AprilShare,MayShare,JuneShare,JulyShare,AugustShare,SeptemberShare,OctoberShare,NovemberShare,DecemberShare, JanuaryShare, FebruaryShare,MarchShare) )AS pivotr)AS m Pivot( MAX(Employer_Share) FOR [monthlyEmployer] IN (AprilEmployer,MayEmployer,AugustEmployer,JuneEmployer,JulyEmployer,SeptemberEmployer,"
            Qry += " OctoberEmployer ,NovemberEmployer,JanuaryEmployer, FebruaryEmployer, MarchEmployer,DecemberEmployer) )AS pivo)as n Pivot( MAX(pension_fund) FOR monthlyPension IN (AprilPension,MayPension,JunePension,JulyPension,AugustPension,SeptemberPension,OctoberPension,NovemberPension,JanuaryPension, FebruaryPension, MarchPension,DecemberPension) )AS pivo)as mm "
            Qry += " Pivot( MAX(ABSENT_DAYS) FOR [monthlyDays] IN (AprilDays,MayDays,AugustDays,JuneDays,JulyDays,SeptemberDays, OctoberDays ,NovemberDays,JanuaryDays, FebruaryDays, MarchDays,DecemberDays) )AS pivo)as u  "

            TotalAmountWages = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(isnull(g.AprilAmt,0)+isnull(g.MayAmt,0 )+isnull(g.JuneAmt,0 )+isnull(g.JulyAmt,0  )+isnull(g.AugustAmt,0  )+isnull(g.SeptemberAmt,0  )+isnull(g.OctoberAmt,0  )+isnull(g.NovemberAmt,0  )+isnull(g.DecemberAmt,0 ) +isnull(g.JanuaryAmt,0  )+isnull(g.FebruaryAmt,0  )+isnull(g.MarchAmt,0  ))as Total_Amount_Wages from (" & Qry & ")as g"))
            TotalWorkerShare = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(isnull(g.AprilShare,0)+isnull(g.MayShare ,0 )+isnull(g.JuneShare ,0 )+isnull(g.JulyShare ,0  )+isnull(g.AugustShare,0  )+isnull(g.SeptemberShare,0  )+isnull(g.OctoberShare,0  )+isnull(g.NovemberShare,0  )+isnull(g.DecemberShare,0 ) +isnull(g.JanuaryShare,0  )+isnull(g.FebruaryShare,0  )+isnull(g.MarchShare,0  ))as TotalWorkerShare from (" & Qry & ")as g"))

            TotalEmployerShare = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(isnull(g.AprilEmployer ,0)+isnull(g.MayEmployer ,0 )+isnull(g.JuneEmployer ,0 )+isnull(g.JulyEmployer ,0  )+isnull(g.AugustEmployer,0  )+isnull(g.SeptemberEmployer,0  )+isnull(g.OctoberEmployer,0  )+isnull(g.NovemberEmployer,0  )+isnull(g.DecemberEmployer,0 ) +isnull(g.JanuaryEmployer,0  )+isnull(g.FebruaryEmployer,0  )+isnull(g.MarchEmployer,0  ))as TotalEmployerShare from (" & Qry & ")as g"))

            TotalPensionFund = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(isnull(g.AprilPension  ,0)+isnull(g.MayPension ,0 )+isnull(g.JunePension ,0 )+isnull(g.JulyPension ,0  )+isnull(g.AugustPension,0  )+isnull(g.SeptemberPension,0  )+isnull(g.OctoberPension,0  )+isnull(g.NovemberPension,0  )+isnull(g.DecemberPension,0 ) +isnull(g.JanuaryPension,0  )+isnull(g.FebruaryPension,0  )+isnull(g.MarchPension,0  ))as TotalPensionFund from (" & Qry & ")as g"))

            TotalContribution = (TotalWorkerShare + TotalEmployerShare)
            TotalDays = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(isnull(g.AprilDays  ,0)+isnull(g.MayDays ,0 )+isnull(g.JuneDays ,0 )+isnull(g.JulyDays ,0  )+isnull(g.AugustDays,0  )+isnull(g.SeptemberDays,0  )+isnull(g.OctoberDays,0  )+isnull(g.NovemberDays,0  )+isnull(g.DecemberDays,0 ) +isnull(g.JanuaryDays,0  )+isnull(g.FebruaryDays,0  )+isnull(g.MarchDays,0  ))as TotalDays from (" & Qry & ")as g"))
            MainQry = "select Emp_Name,'" & TotalContribution & "' as TotalContribution ,'" & TotalDays & "' as TotalDays,FATHERS_NAME ,BANK_ACC_NO,Comp_Name ,Add1 ,Add2,location_desc ,City_Code, Employee_pf_per,From_Year ,To_Year,max(EPF_Rate) as EPF_Rate,max(Rate_Amount) as Rate_Amount,PF_No,Emp_PF_NO,convert(varchar,RELIEVING_DATE,103)as RELIEVING_DATE,LEAVING_REASON,coalesce(max(AprilAmt),0) as AprilAmt ,coalesce(max(MayAmt),0) as MayAmt ,coalesce(max(JuneAmt),0) as JuneAmt ,coalesce(max(JulyAmt),0) as JulyAmt,coalesce(max(AugustAmt),0) as AugustAmt ,coalesce(max(SeptemberAmt),0) as SeptemberAmt ,coalesce(max(OctoberAmt),0) as OctoberAmt ,coalesce(max(NovemberAmt),0) as NovemberAmt ,coalesce(max(DecemberAmt),0) as DecemberAmt ,coalesce(max(JanuaryAmt),0) as JanuaryAmt ,coalesce(max(FebruaryAmt),0) as FebruaryAmt ,coalesce(max(MarchAmt),0) as MarchAmt ,coalesce(max(AprilShare),0) as AprilShare ,coalesce(max(MayShare),0) as MayShare ,coalesce(max(JuneShare),0) as JuneShare ,coalesce(max(JulyShare),0) as JulyShare ,coalesce(max(AugustShare),0) as AugustShare ,coalesce(max(SeptemberShare),0) as SeptemberShare ,coalesce(max(OctoberShare),0) as OctoberShare ,coalesce(max(NovemberShare),0) as NovemberShare ,coalesce(max(DecemberShare),0) as DecemberShare ,coalesce(max(JanuaryShare),0) as JanuaryShare ,coalesce(max(FebruaryShare),0) as FebruaryShare ,coalesce(max(MarchShare),0) as MarchShare ,coalesce(max(AprilEmployer),0) as AprilEmployer ,coalesce(max(MayEmployer),0) as MayEmployer ,coalesce(max(JuneEmployer),0) as JuneEmployer ,coalesce(max(JulyEmployer),0) as JulyEmployer ,coalesce(max(AugustEmployer),0) as AugustEmployer ,coalesce(max(SeptemberEmployer),0) as SeptemberEmployer ,coalesce(max(OctoberEmployer),0) as OctoberEmployer ,coalesce(max(NovemberEmployer),0) as NovemberEmployer ,coalesce(max(DecemberEmployer),0) as DecemberEmployer ,coalesce(max(JanuaryEmployer),0) as JanuaryEmployer ,coalesce(max(FebruaryEmployer),0) as FebruaryEmployer ,coalesce(max(MarchEmployer),0) as MarchEmployer ,coalesce(max(AprilPension),0) as AprilPension ,coalesce(max(MayPension),0) as MayPension ,coalesce(max(JunePension),0) as JunePension ,coalesce(max(JulyPension),0) as JulyPension ,coalesce(max(AugustPension),0) as AugustPension ,coalesce(max(SeptemberPension),0) as SeptemberPension ,coalesce(max(OctoberPension),0) as OctoberPension ,coalesce(max(NovemberPension),0) as NovemberPension ,coalesce(max(DecemberPension),0) as DecemberPension ,coalesce(max(JanuaryPension),0) as JanuaryPension ,coalesce(max(FebruaryPension),0) as FebruaryPension ,coalesce(max(MarchPension),0) as MarchPension,'" & TotalPensionFund & "' as TotalPensionFund,'" & TotalEmployerShare & "' as TotalEmployerShare,'" & TotalWorkerShare & "' as TotalWorkerShare,'" & TotalAmountWages & "' as TotalAmountWages,coalesce(max(AprilDays),0) as AprilDays,coalesce(max(MayDays),0) as MayDays,coalesce(max(JuneDays),0) as JuneDays,coalesce(max(JulyDays),0) as JulyDays,coalesce(max(AugustDays),0) as AugustDays,coalesce(max(SeptemberDays),0) as SeptemberDays, coalesce(max(OctoberDays),0) as OctoberDays ,coalesce(max(NovemberDays),0) as NovemberDays,coalesce(max(DecemberDays),0) as DecemberDays,coalesce(max(JanuaryDays),0) as JanuaryDays, coalesce(max(FebruaryDays),0) as FebruaryDays, coalesce(max(MarchDays),0) as MarchDays from (" & Qry & ")as ll group by ll.Emp_Name,FATHERS_NAME,BANK_ACC_NO,Comp_Name ,Add1 ,Add2,location_desc ,City_Code, Employee_pf_per,From_Year ,To_Year,PF_No,Emp_PF_NO,convert(varchar,RELIEVING_DATE,103),LEAVING_REASON"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(MainQry)
            If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptpfForm3", "PF Form3")
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
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

    Private Sub fndLocationCode__MY_click(sender As Object, e As EventArgs) Handles fndLocationCode._My_Click
        Try
            'fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
            Dim whrcls As String = ""
            Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
            If clsCommon.myLen(whrcls) <= 0 Then
                whrcls = " LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY )"
            End If
            fndLocationCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulLoc", qry, "Code", "Name", fndLocationCode.arrValueMember, fndLocationCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndEmployeeMult__My_Click(sender As Object, e As EventArgs) Handles FndEmployeeMult._My_Click
        Try
            Dim qry As String = "select EMP_CODE , Emp_Name as Name from TSPL_Employee_Master left join TSPL_Location_Master on  TSPL_Location_Master.Location_code=TSPL_Employee_Master.Location_Code where ISpf ='1' and TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(fndLocationCode.arrValueMember) & ") "
            FndEmployeeMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "EMP_CODE", "Name", FndEmployeeMult.arrValueMember, FndEmployeeMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class

