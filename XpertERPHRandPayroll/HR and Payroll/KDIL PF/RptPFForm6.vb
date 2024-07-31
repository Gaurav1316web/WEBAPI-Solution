'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptPFForm6
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPFForm6)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub Reset()
        txtFromYear.Value = clsCommon.GETSERVERDATE
        txtToYear.Value = txtFromYear.Value
        txtLocCode.arrValueMember = Nothing
    End Sub
    Sub LoadReportForm6()
        Try
            Dim FromDate As Date
            Dim ToDate As Date
            FromDate = "01/04/" & (txtFromYear.Text)
            ToDate = "31/03/" & (txtToYear.Text)
            If (txtToYear.Text) < (txtFromYear.Text) Then
                clsCommon.MyMessageBoxShow(Me, "From Year is not greater than To Date ", Me.Text)
                Exit Sub
            End If

            Dim PensionRate As Double
            Dim StatutoryRate As Double
            Dim Qry As String
            'Dim Main As String
            Dim RountOffType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPF_ROUNDOFF_YPE  FROM TSPL_PF_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= convert(date,'" + ToDate + "',103) ORDER BY APPLICABLE_FROM Desc;"))
            Dim Comp_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            StatutoryRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 EMPEPF_PER  FROM TSPL_PF_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= convert(date,'" + ToDate + "',103) ORDER BY APPLICABLE_FROM Desc;"))
            PensionRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPS_PER FROM TSPL_PF_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= convert(date,'" + ToDate + "',103) ORDER BY APPLICABLE_FROM Desc;"))
            'Qry = "select *,isnull(AprilPension  ,0)+isnull(MayPension ,0 )+isnull(JunePension ,0 )+isnull(JulyPension ,0  )+isnull(AugustPension,0  )+isnull(SeptemberPension,0  )+isnull(OctoberPension,0  )+isnull(NovemberPension,0  )+isnull(DecemberPension,0 ) +isnull(JanuaryPension,0  )+isnull(FebruaryPension,0  )+isnull(MarchPension,0  )as TotalPensionFund,convert(decimal(18,2),(isnull(AprilEmployer ,0)+isnull(MayEmployer ,0 )+isnull(JuneEmployer ,0 )+isnull(JulyEmployer ,0  )+isnull(AugustEmployer,0  )+isnull(SeptemberEmployer,0  )+isnull(OctoberEmployer,0  )+isnull(NovemberEmployer,0  )+isnull(DecemberEmployer,0 ) +isnull(JanuaryEmployer,0  )+isnull(FebruaryEmployer,0  )+isnull(MarchEmployer,0 )) )as TotalEmployerShare,isnull(AprilShare,0)+isnull(MayShare ,0 )+isnull(JuneShare ,0 )+isnull(JulyShare ,0  )+isnull(AugustShare,0  )+isnull(SeptemberShare,0  )+isnull(OctoberShare,0  )+isnull(NovemberShare,0  )+isnull(DecemberShare,0 ) +isnull(JanuaryShare,0  )+isnull(FebruaryShare,0  )+isnull(MarchShare,0  )as TotalWorkerShare,isnull(AprilAmt,0)+isnull(MayAmt,0 )+isnull(JuneAmt,0 )+isnull(JulyAmt,0  )+isnull(AugustAmt,0  )+isnull(SeptemberAmt,0  )+isnull(OctoberAmt,0  )+isnull(NovemberAmt,0  )+isnull(DecemberAmt,0 ) +isnull(JanuaryAmt,0  )+isnull(FebruaryAmt,0  )+isnull(MarchAmt,0  )as TotalAmountWages from(select * from (select * from (select * from (select '" & StatutoryRate & "' as Employee_pf_per,'" & EmployerRate & "' as Employee_Share_Rate ,Emp_Name,max(FATHERS_NAME) as FATHERS_NAME "
            'Qry += " ,max(BANK_ACC_NO) as BANK_ACC_NO,MAX( MonthlyAmt) as MonthlyAmt ,max (MonthlyShare) as MonthlyShare,max(monthlyEmployer) as monthlyEmployer,MAX(monthlyPension)as monthlyPension"
            'Qry += " ,max(Comp_Name)as Comp_Name ,max(Add1) as Add1,max(Add2) as Add2 ,max(City_Code) as City_Code ,HEAD_VALUE,max(ACTUAL_AMOUNT) as ACTUAL_AMOUNT ,max(pension_fund)as pension_fund,'" & FromDate.Year & "' as From_Year,'" & ToDate.Year & "' as To_Year"
            'Qry += " ,max(Employer_Share) as Employer_Share from (select Emp_Name,FATHERS_NAME,BANK_ACC_NO,tt._Month+'Amt'  as MonthlyAmt ,tt._Month+'Share' as MonthlyShare,tt. _Month+'Employer' as monthlyEmployer ,tt. _Month+'Pension' as monthlyPension"
            'Qry += ",Comp_Name,Add1,Add2,City_Code,HEAD_VALUE,ACTUAL_AMOUNT,(ACTUAL_AMOUNT - Employer_Share)as pension_fund ,Employer_Share from (select Emp_Name ,FATHERS_NAME ,BANK_ACC_NO ,Comp_Name ,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Add2 "
            'Qry += ",City_Code,HEAD_VALUE ,ACTUAL_AMOUNT ,(('" & EmployerRate & "')*HEAD_VALUE /100)as Employer_Share,DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as _Month,TSPL_GENERATE_SALARY.GENERATE_DATE   from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on"
            'Qry += "  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE "
            'Qry += " left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE  where SUB_HEAD_TYPE in ('EPF')and ACTUAL_AMOUNT > 0  and   convert(date,GENERATE_DATE,103)>=convert(date,'" & FromDate.ToString & "',103) "
            'Qry += " and convert(date,GENERATE_DATE,103) <=convert(date,'" & ToDate.ToString & "' ,103))tt )as dd group by HEAD_VALUE ,MonthlyAmt,Emp_Name )as s     Pivot(max(  HEAD_VALUE) FOR [MonthlyAmt] IN (AprilAmt,MayAmt,JuneAmt,JulyAmt,AugustAmt,SeptemberAmt,OctoberAmt,NovemberAmt,DecemberAmt, JanuaryAmt, FebruaryAmt,MarchAmt) )AS pivott) as g Pivot("
            'Qry += "  MAX(ACTUAL_AMOUNT) FOR [MonthlyShare] IN (AprilShare,MayShare,JuneShare,JulyShare,AugustShare,SeptemberShare,OctoberShare,NovemberShare,DecemberShare, JanuaryShare, FebruaryShare,MarchShare) )AS pivotr)AS m Pivot( MAX(Employee_Share_Rate) FOR [monthlyEmployer] IN (AprilEmployer,MayEmployer,AugustEmployer,JuneEmployer,JulyEmployer,SeptemberEmployer,OctoberEmployer"
            'Qry += " ,NovemberEmployer,JanuaryEmployer, FebruaryEmployer, MarchEmployer,DecemberEmployer) )AS pivo)as n Pivot( MAX(pension_fund) FOR monthlyPension IN (AprilPension,MayPension,JunePension,JulyPension,AugustPension,SeptemberPension,OctoberPension,NovemberPension,JanuaryPension, FebruaryPension, MarchPension,DecemberPension) )AS pivo"


            'Qry = " select *,'" + Comp_Name + "' as Comp_Name1,"
            'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
            '    Qry += " ROUND(isnull(AprilPension  ,0)+isnull(MayPension ,0 )+isnull(JunePension ,0 )+isnull(JulyPension ,0  )+isnull(AugustPension,0  )+isnull(SeptemberPension,0  )+isnull(OctoberPension,0  )+isnull(NovemberPension,0  )+isnull(DecemberPension,0 ) +isnull(JanuaryPension,0  )+isnull(FebruaryPension,0  )+isnull(MarchPension,0 ),0 ) "
            'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
            '    Qry += " FLOOR(isnull(AprilPension  ,0)+isnull(MayPension ,0 )+isnull(JunePension ,0 )+isnull(JulyPension ,0  )+isnull(AugustPension,0  )+isnull(SeptemberPension,0  )+isnull(OctoberPension,0  )+isnull(NovemberPension,0  )+isnull(DecemberPension,0 ) +isnull(JanuaryPension,0  )+isnull(FebruaryPension,0  )+isnull(MarchPension,0 ) ) "
            'Else
            '    Qry += " CEILING(isnull(AprilPension  ,0)+isnull(MayPension ,0 )+isnull(JunePension ,0 )+isnull(JulyPension ,0  )+isnull(AugustPension,0  )+isnull(SeptemberPension,0  )+isnull(OctoberPension,0  )+isnull(NovemberPension,0  )+isnull(DecemberPension,0 ) +isnull(JanuaryPension,0  )+isnull(FebruaryPension,0  )+isnull(MarchPension,0 ) ) "
            'End If
            'Qry += " as TotalPensionFund,"
            'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
            '    Qry += " ROUND(convert(decimal(18,2),(isnull(AprilEmployer ,0)+isnull(MayEmployer ,0 )+isnull(JuneEmployer ,0 )+isnull(JulyEmployer ,0  )+isnull(AugustEmployer,0  )+isnull(SeptemberEmployer,0  )+isnull(OctoberEmployer,0  )+isnull(NovemberEmployer,0  )+isnull(DecemberEmployer,0 ) +isnull(JanuaryEmployer,0  )+isnull(FebruaryEmployer,0  )+isnull(MarchEmployer,0 )) ),0)"
            'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
            '    Qry += " FLOOR(convert(decimal(18,2),(isnull(AprilEmployer ,0)+isnull(MayEmployer ,0 )+isnull(JuneEmployer ,0 )+isnull(JulyEmployer ,0  )+isnull(AugustEmployer,0  )+isnull(SeptemberEmployer,0  )+isnull(OctoberEmployer,0  )+isnull(NovemberEmployer,0  )+isnull(DecemberEmployer,0 ) +isnull(JanuaryEmployer,0  )+isnull(FebruaryEmployer,0  )+isnull(MarchEmployer,0 )) ))"
            'Else
            '    Qry += " CEILING(convert(decimal(18,2),(isnull(AprilEmployer ,0)+isnull(MayEmployer ,0 )+isnull(JuneEmployer ,0 )+isnull(JulyEmployer ,0  )+isnull(AugustEmployer,0  )+isnull(SeptemberEmployer,0  )+isnull(OctoberEmployer,0  )+isnull(NovemberEmployer,0  )+isnull(DecemberEmployer,0 ) +isnull(JanuaryEmployer,0  )+isnull(FebruaryEmployer,0  )+isnull(MarchEmployer,0 )) ))"
            'End If
            'Qry += " as TotalEmployerShare,"
            'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
            '    Qry += " ROUND(isnull(AprilShare,0)+isnull(MayShare ,0 )+isnull(JuneShare ,0 )+isnull(JulyShare ,0  )+isnull(AugustShare,0  )+isnull(SeptemberShare,0  )+isnull(OctoberShare,0  )+isnull(NovemberShare,0  )+isnull(DecemberShare,0 ) +isnull(JanuaryShare,0  )+isnull(FebruaryShare,0  )+isnull(MarchShare,0  ),0)"
            'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
            '    Qry += " FLOOR(isnull(AprilShare,0)+isnull(MayShare ,0 )+isnull(JuneShare ,0 )+isnull(JulyShare ,0  )+isnull(AugustShare,0  )+isnull(SeptemberShare,0  )+isnull(OctoberShare,0  )+isnull(NovemberShare,0  )+isnull(DecemberShare,0 ) +isnull(JanuaryShare,0  )+isnull(FebruaryShare,0  )+isnull(MarchShare,0  ))"
            'Else
            '    Qry += " CEILING(isnull(AprilShare,0)+isnull(MayShare ,0 )+isnull(JuneShare ,0 )+isnull(JulyShare ,0  )+isnull(AugustShare,0  )+isnull(SeptemberShare,0  )+isnull(OctoberShare,0  )+isnull(NovemberShare,0  )+isnull(DecemberShare,0 ) +isnull(JanuaryShare,0  )+isnull(FebruaryShare,0  )+isnull(MarchShare,0  ))"
            'End If
            'Qry += " as TotalWorkerShare,"

            'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
            '    Qry += " ROUND(isnull(AprilAmt,0)+isnull(MayAmt,0 )+isnull(JuneAmt,0 )+isnull(JulyAmt,0  )+isnull(AugustAmt,0  )+isnull(SeptemberAmt,0  )+isnull(OctoberAmt,0  )+isnull(NovemberAmt,0  )+isnull(DecemberAmt,0 ) +isnull(JanuaryAmt,0  )+isnull(FebruaryAmt,0  )+isnull(MarchAmt,0  ),0) "
            'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
            '    Qry += " FLOOR(isnull(AprilAmt,0)+isnull(MayAmt,0 )+isnull(JuneAmt,0 )+isnull(JulyAmt,0  )+isnull(AugustAmt,0  )+isnull(SeptemberAmt,0  )+isnull(OctoberAmt,0  )+isnull(NovemberAmt,0  )+isnull(DecemberAmt,0 ) +isnull(JanuaryAmt,0  )+isnull(FebruaryAmt,0  )+isnull(MarchAmt,0  )) "
            'Else
            '    Qry += " CEILING(isnull(AprilAmt,0)+isnull(MayAmt,0 )+isnull(JuneAmt,0 )+isnull(JulyAmt,0  )+isnull(AugustAmt,0  )+isnull(SeptemberAmt,0  )+isnull(OctoberAmt,0  )+isnull(NovemberAmt,0  )+isnull(DecemberAmt,0 ) +isnull(JanuaryAmt,0  )+isnull(FebruaryAmt,0  )+isnull(MarchAmt,0  )) "
            'End If
            'Qry += " as TotalAmountWages"
            'Qry += " from (select * from (select * from (select * from (select '" & StatutoryRate & "' as Employee_pf_per,Emp_Name,max(FATHERS_NAME) as FATHERS_NAME  ,max(PF_NO) as PF_NO,MAX( MonthlyAmt) as MonthlyAmt ,max (MonthlyShare) as MonthlyShare,max(monthlyEmployer) as monthlyEmployer,MAX(monthlyPension)as monthlyPension ,max(Comp_Name)as Comp_Name ,max(Add1) as Add1,max(Add2) as Add2  ,max(City_Code) as City_Code ,HEAD_VALUE,max(ACTUAL_AMOUNT) as ACTUAL_AMOUNT ,max(pension_fund)as pension_fund,'" & FromDate.Year & "' as From_Year,'" & ToDate.Year & "' as To_Year ,max(Employer_Share) as Employer_Share,Max(Location) as Location,Max(Address) as Address,MAX(esi_rate) AS ESI_Rate  from (select Emp_Name,FATHERS_NAME,PF_NO,tt._Month+'Amt'  as MonthlyAmt ,tt._Month+'Share' as MonthlyShare,tt. _Month+'Employer' as monthlyEmployer ,tt. _Month+'Pension' as monthlyPension, Comp_Name,Add1,Add2,City_Code,HEAD_VALUE,ACTUAL_AMOUNT,(ACTUAL_AMOUNT - pension_fund)"
            'Qry += " as Employer_Share ,pension_fund"
            'Qry += ",Location,Address,esi_rate from (select Emp_Name ,FATHERS_NAME ,TSPL_EMPLOYEE_MASTER.PF_NO ,Comp_Name ,TSPL_LOCATION_MASTER.Add1 ,TSPL_LOCATION_MASTER.Add2 ,TSPL_LOCATION_MASTER.City_Code,HEAD_VALUE ,ACTUAL_AMOUNT , (('" & PensionRate & "') * HEAD_VALUE /100)  as pension_fund, DATENAME (MONTH ,CONVERT(date,GENERATE_DATE,103))as _Month,TSPL_GENERATE_SALARY.GENERATE_DATE,"
            ''If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            'Qry += " ISNULL(TSPL_LOCATION_MASTER.Location_Desc,'')  "
            ''Else
            ''    Qry += "' " + Comp_Name + " '"
            ''End If
            'Qry += " as Location,"
            '' If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            'Qry += "TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.City_Code) End End End  "
            ''Else
            ''Qry += "''"
            ''End If
            'Qry += "  as Address,tspl_employee_status.esi_rate from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE   left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join tspl_Location_Master on tspl_Location_Master.Location_Code=TSPL_GENERATE_SALARY.Location_Code left join tspl_employee_status on  TSPL_EMPLOYEE_MASTER.EMP_CODE =tspl_employee_status.EMP_CODE   where SUB_HEAD_TYPE in ('EPF')and ACTUAL_AMOUNT > 0 "
            ''If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            ''    Qry += " and TSPL_GENERATE_SALARY.Location_Code='" & FndLocationCode.Value & "' "
            ''End If
            'If txtLocCode.arrValueMember IsNot Nothing AndAlso txtLocCode.arrValueMember.Count > 0 Then
            '    Qry += " and TSPL_GENERATE_SALARY.Location_Code in (" & clsCommon.GetMulcallString(txtLocCode.arrValueMember) & " )"
            'End If
            'Qry += " and   convert(date,GENERATE_DATE,103)>=convert(date,'" & FromDate.ToString & "' ,103)  and convert(date,GENERATE_DATE,103) <=convert(date,'" & ToDate.ToString & "' ,103))tt )as dd group by HEAD_VALUE ,MonthlyAmt,Emp_Name )as s     Pivot(max(  HEAD_VALUE) FOR [MonthlyAmt] IN (AprilAmt,MayAmt,JuneAmt,JulyAmt,AugustAmt,SeptemberAmt,OctoberAmt,NovemberAmt,DecemberAmt, JanuaryAmt, FebruaryAmt,MarchAmt) )AS pivott) as g Pivot(  MAX(ACTUAL_AMOUNT) FOR [MonthlyShare] IN (AprilShare,MayShare,JuneShare,JulyShare,AugustShare,SeptemberShare,OctoberShare,NovemberShare,DecemberShare, JanuaryShare, FebruaryShare,MarchShare) )AS pivotr)AS m Pivot( MAX(Employer_Share) FOR [monthlyEmployer] IN (AprilEmployer,MayEmployer,AugustEmployer,JuneEmployer,JulyEmployer,SeptemberEmployer, OctoberEmployer ,NovemberEmployer,JanuaryEmployer, FebruaryEmployer, MarchEmployer,DecemberEmployer) )AS pivo)as n Pivot( MAX(pension_fund) FOR monthlyPension IN (AprilPension,MayPension,JunePension,JulyPension,AugustPension,SeptemberPension,OctoberPension,NovemberPension,JanuaryPension, FebruaryPension, MarchPension,DecemberPension) )AS pivo"


            '' New Query


            Qry = ""
            Qry += " SELECT max(Comp_Name) as Comp_Name,MAX(PF_NO ) AS PF_NO,max(Comp_PF_NO) as Comp_PF_NO,MAX(Emp_Name) AS Emp_Name,SUM(Wages) AS TotalAmountWages,SUM(ACTUAL_AMOUNT) AS TotalWorkerShare,SUM(Pension_Fund) As TotalPensionFund ,max(EPF_Rate) As EPF_Rate,max(RATE_AMOUNT) as Employee_PF_Per,SUM(COEPF_PER) AS TotalEmployerShare,MAX(Location_Code) As Location ,MAX(Add1 ) AS Add1 ,MAX(Add2) AS Add2,MAX(Location_Desc) AS Location_Desc,MAX(Address) As [Address],EMP_CODE FROM (" &
                    " select *,'" & objCommonVar.CurrentCompanyName & "' as CompName,(pension_fund + Diff)as Total from  " &
                    " (select DISTINCT *,(ACTUAL_AMOUNT-pension_fund)as Diff from " &
                    " (select TSPL_DEVISION_MASTER.DEVISION_CODE ,TSPL_DEVISION_MASTER.DEVISION_NAME ,TSPL_LOCATION_MASTER .Location_Code , Emp_Name,TSPL_LOCATION_MASTER.PF_NO as Comp_PF_NO,TSPL_EMPLOYEE_MASTER.PF_NO , " &
                    " (case when coalesce(TSPL_GENERATE_SALARY_PAYHEADS.EPF_RATE,0)>0 then (ACTUAL_AMOUNT-(CoEPS_AMT_AC10+CoEPF_AMT_AC01)) else 0 end) as VPF,(case when coalesce(TSPL_GENERATE_SALARY_PAYHEADS.EPF_RATE,0)<=0 then ACTUAL_AMOUNT else (CoEPS_AMT_AC10+CoEPF_AMT_AC01) end) as ACTUAL_AMOUNT,ROUND(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end,0)AS HEAD_VALUE,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.City_Code , TSPL_GENERATE_SALARY.pay_period_code as FROM_PAY_PERIOD , ((IsNull(EmpWages.Wages,0)*" & PensionRate & ")/100)  as pension_fund, (TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT-TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10) as COEPF_PER, TSPL_GENERATE_SALARY_PAYHEADS.ADMIN_AMT_AC02 as ACCOEPF_PER, TSPL_GENERATE_SALARY_PAYHEADS.EDLI_AMT_AC21 as COEDLI_PER, TSPL_GENERATE_SALARY_PAYHEADS.EDLI_AMT_AC21 as ACCOEDLI_PER, TSPL_LOCATION_MASTER.Location_Desc , TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End As Address " &
                    " ,TSPL_GENERATE_SALARY_PAYHEADS.EPF_RATE As [EPF_Rate],ISNULL(CoEPF_Rate_Ac01,0)+ISNULL(CoEPS_RATE_AC10,0) AS RATE_AMOUNT,TSPL_EMPLOYEE_MASTER.EMP_CODE,IsNull(EmpWages.Wages,0)Wages  " &
                    " from TSPL_GENERATE_SALARY_PAYHEADS " &
                    " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE " &
                    " INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  " &
                    " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE    " &
                    " INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " &
                    " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE " &
                    " left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_GENERATE_SALARY.DEVISION_CODE   " &
                    " left join TSPL_DEVISION_MASTER as TSPL_DEVISION_MASTER_For_Employee  on TSPL_DEVISION_MASTER_For_Employee.DEVISION_CODE =TSPL_EMPLOYEE_MASTER.DEVISION_CODE   " &
                    " left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code   " &
                    " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_Code " &
                    " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State "
            Qry += " Left Join (select (Case When SUB_HEAD_TYPE='Basic' Then ACTUAL_AMOUNT Else 0 End)+(Case When SUB_HEAD_TYPE='DA' Then ACTUAL_AMOUNT Else 0 End)Wages,TSPL_EMPLOYEE_MASTER.EMP_CODE,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
from TSPL_GENERATE_SALARY_PAYHEADS 
left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   
left join TSPL_GENERATE_SALARY_ATTENDANCE ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  
left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_MASTER.Location_Code   
left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE    
left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.Pay_Period_Code=TSPL_GENERATE_SALARy.Pay_Period_Code   
where SUB_HEAD_TYPE IN ('BASIC','DA') and ACTUAL_AMOUNT > 0 
and convert(date,TSPL_PAYPERIOD_MASTER.DATE_TO,103)>=convert(date,'01/APR/" & txtFromYear.Value.Year & "',103)  
and convert(date,TSPL_PAYPERIOD_MASTER.DATE_TO,103)<=convert(date,'31/MAR/" & (txtToYear.Value.Year + 1) & "' ,103) "
            If txtLocCode.arrValueMember IsNot Nothing AndAlso txtLocCode.arrValueMember.Count > 0 Then
                Qry += "and TSPL_GENERATE_SALARy.LOCATION_CODE in (" & clsCommon.GetMulcallString(txtLocCode.arrValueMember) & " ) "
            End If
            Qry += ")EmpWages on EmpWages.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE And EmpWages.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE "
            Qry += " WHERE TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='EPF' AND TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0 "
            If txtLocCode.arrValueMember IsNot Nothing AndAlso txtLocCode.arrValueMember.Count > 0 Then
                Qry += " and TSPL_EMPLOYEE_MASTER.Location_Code in (" & clsCommon.GetMulcallString(txtLocCode.arrValueMember) & " )"
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_DEVISION_MASTER_For_Employee.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
            End If
            '    Qry += " and TSPL_EMPLOYEE_MASTER.LOCATION_CODE  in ('001') " & _
            Qry += " )as d)as m) AS Final " &
                    " left join tspl_payperiod_master ppm on final.FROM_PAY_PERIOD=ppm.pay_period_code " &
                    " where ppm.DATE_FROM between '01/APR/" & txtFromYear.Value.Year & "' and '31/MAR/" & (txtToYear.Value.Year + 1) & "' " &
                    " GROUP BY FINAL.EMP_CODE"

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(Qry)
            If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFForm6", "PF Form6")
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RptPFForm6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadReportForm6()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
        Try
            FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
            If clsCommon.myLen(FndLocationCode.Value) > 0 Then
                lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
            Else
                lblLocationName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocCode__My_Click(sender As Object, e As EventArgs) Handles txtLocCode._My_Click
        Try
            'Dim qry As String = " Select Location_Code As Code,location_Desc As Name ,Add1 + ' ' + Add2 + Add3 + ' ' + Add4 As [Address],City_Code As [City Code],State As [State],Pin_Code As [Pin Code]," & _
            '                    " Country ,Telphone,Email,Location_Type AS [Location Type],Loc_Status AS [Loc Status] ,Loc_Segment_Code As [Loc Segment Code],Type As [Type] From TSPL_LOCATION_MASTER "
            Dim qry As String = clsSalaryGeneration.GetFinderQueryForSalaryLocationMulti("")
            txtLocCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMul5", qry, "Code", "Name", txtLocCode.arrValueMember, txtLocCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Try
            Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
            txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
