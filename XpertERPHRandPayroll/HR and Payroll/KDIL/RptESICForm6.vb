'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptESICForm6
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptESICForm6)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadData()
        Dim LocAddress As String = ""
        Dim NearestCity As String = ""
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
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count = 1 Then
            LocationFirstTime += 1
            NearestCity = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT NearestCity FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"))
        Else
            NearestCity = ""
        End If
        Dim From_Date As Date
        Dim To_Date As Date
        'Dim FromMonth As String
        'Dim ToMonth As String
        If cbMonth.Text = "OCT-MAR" Then
            From_Date = "01/10/" & (txtFromYear.Text)
            To_Date = "31/03/" & (txtFromYear.Text + 1)


            Dim Comp_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            Dim CompESIC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_ESIC_NO from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            
            Dim InnerQry As String = " select head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,detail.Month,RATE_AMOUNT,head.ACTUAL_AMOUNT ,detail.Date_Of_Challan,head.PAYABLE_DAYS,head.Average_Daily_Wages,head.SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'Yes' else 'No' end as Still_Working ,Location,Loc_Address,ACTUAL_Amount1,Employer_Name,Employer_desg,Employer_Address ,Nearestcity,ESIC_No from (select distinct TSPL_EMPLOYEE_MASTER.DEVISION_CODE  ,TSPL_EMPLOYEE_MASTER.EMP_CODE ,Emp_Name, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER. add2,TSPL_COMPANY_MASTER.City_Code" & _
                       " ,TSPL_EMPLOYEE_MASTER.ESI_NO,Joining_date ,convert(varchar,RELIEVING_DATE,103) as Left_Date,RATE_AMOUNT,ACTUAL_AMOUNT,Round(HEAD_VALUE,0)as HEAD_VALUE,TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,case when PAYABLE_DAYS = 0 then 0 else (HEAD_VALUE/PAYABLE_DAYS) end as Average_Daily_Wages,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE,Location_Desc as Location , "
            InnerQry += "  '" & LocAddress & "' as Loc_Address, round(TSPL_GENERATE_SALARY_PAYHEADS.Co_ESI_AMT,0) as ACTUAL_Amount1 ,Employer_Name,Employer_desg,(Employer_Add1+','+ Employer_Add2 + ',' + Employer_Add3) as Employer_Address,Nearestcity,ESIC_No from TSPL_GENERATE_SALARY_PAYHEADS " & _
                        " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE " & _
                        " INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE " & _
                        " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " & _
                        " INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                        "   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code  " & _
                        "  left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  " & _
                        "  left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code  " & _
                        "   left outer join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_GENERATE_SALARY.DEVISION_CODE   " & _
                         " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_GENERATE_SALARY_PAYHEADS.Emp_code " & _
                        " where TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                InnerQry += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                InnerQry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
            End If
            InnerQry += " )head"
            InnerQry += " Left Join (select DATENAME (MONTH ,CONVERT(date,Date_To,103))as Month,max(Date_To)as Date_Of_Challan,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  from TSPL_GENERATE_SALARY left join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.Pay_Period_Code=TSPL_GENERATE_SALARY.Pay_Period_Code left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.LOCATION_CODE left join TSPL_DEVISION_MASTER on  TSPL_DEVISION_MASTER.devision_code=TSPL_GENERATE_SALARY.devision_code where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                InnerQry += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                InnerQry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
            End If
            InnerQry += " group by Date_To,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE    )detail on head.SALARY_GENERATION_CODE=detail.SALARY_GENERATION_CODE "

            Dim SecondQry As String = " select xx.Emp_Name ,xx.ESI_NO ,xx.Month+'_Amt' as _Month,xx.Month+'_Date' as D_Month  ,xx.Emp_Actual ,xx.Date_Of_Challan ,xx.PAYABLE_DAYS ,xx.Average_Daily_Wages ,xx.Joining_date ,xx.Left_Date ,xx.Still_Working ,xx.HEAD_VALUE,17827 as Total_Amount,xx.Arrear ,xx.Location,xx.Loc_Address,xx.ACTUAL_Amount1,xx.total_actual,xx.Employer_Name,xx.Employer_desg,xx.Employer_Address,xx.Nearestcity,xx.ESIC_No  from(SELECT  SUM(case when SUB_HEAD_TYPE='ARREAR' THEN actual_amount ELSE 0 end) AS Arrear, Final.Emp_Name,Final.ESI_NO,Final.Month,sum( ACTUAL_AMOUNT ) as Emp_Actual,SUM(actual_amount+ACTUAL_Amount1) as total_actual,convert (varchar ,Final.Date_Of_Challan,103) as Date_Of_Challan ,Final.PAYABLE_DAYS ,Final.Average_Daily_Wages ,Final.Joining_date ,Final.Left_Date ,Final .Still_Working,final.HEAD_VALUE  ,Final.Location,final.Loc_Address,ACTUAL_Amount1,max(Employer_Name)  as Employer_Name,max(Employer_desg)as Employer_desg,max(Employer_Address)as Employer_Address,max(Nearestcity) as Nearestcity,max(ESIC_No) as ESIC_No FROM  (" + InnerQry + ") AS Final"
            SecondQry += " where convert(date,Date_Of_Challan,103)>=convert(date,'" & From_Date.ToString & "',103) and convert(date,Date_Of_Challan,103) <=convert(date,'" & To_Date.ToString & "' ,103) group by Final.Emp_Name,Final.ESI_NO,Final.Month,Final.Date_Of_Challan ,Final.PAYABLE_DAYS ,Final.Average_Daily_Wages ,Final.Joining_date ,Final.Left_Date ,Final .Still_Working,final.HEAD_VALUE ,Location,Loc_Address,ACTUAL_Amount1) xx"
            SecondQry += " union   select Top 1 Emp_Name,ESI_NO ,'October_Amt','October_Date'  , 0,'',0,0,'','','',0,0,0,'','',0,0,'','','','','' from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union select Top 1 Emp_Name,ESI_NO,'November_Amt','November_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''  from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union  select Top 1 Emp_Name,ESI_NO,'December_Amt','December_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''  from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union select Top 1 Emp_Name,ESI_NO,'January_Amt','January_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''   from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union select Top 1 Emp_Name,ESI_NO,'February_Amt','February_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''   from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union select Top 1 Emp_Name,ESI_NO,'March_Amt','March_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''   from TSPL_EMPLOYEE_MASTER where ESI_NO > 0  and ESI_NO <> ''"
            'Dim TotalActual1 As String = " select sum(total_actual)as total_actual from(" + SecondQry + ")as m "
            'Dim TotalActual As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(TotalActual1))

            Dim mainqry As String = "select '" + CompESIC + "' as Comp_ESIC_No,'" + Comp_Name + "' as Comp_Name,ESI_No,Emp_Name ,No_Of_Days  ,Average_Daily_Wages ,'October' as From_Month,'March' as To_Month ,Joining_date,Left_Date ,Still_Working ,HEAD_VALUE ,Emp_Actual ,From_Date ,To_date ,From_Year ,To_Year,Location ,October_Date,November_Date,December_Date,January_Date, February_Date, March_Date   "
            mainqry += " ,isnull(October_Amt,0)as October_Amt,isnull(November_Amt ,0)as November_Amt,isnull(December_Amt ,0)as December_Amt ,isnull(January_Amt  ,0)as January_Amt,isnull(February_Amt  ,0)as February_Amt,isnull(March_Amt  ,0)as "
            mainqry += " March_Amt ,isnull(Arrear ,0) as Arrear,Loc_Address,ACTUAL_Amount1 ,Employer_Name,Employer_desg,Employer_Address,Nearestcity,'" & ESIC & "' as ESIC_No from(select  * from(select  (dd.ESI_NO)as ESI_No,Emp_Name,_Month , D_Month ,Total_Amount,MAX (dd.Date_Of_Challan) as Date_Of_Challan,sum (dd.PAYABLE_DAYS )as No_Of_Days,MAX(dd.Average_Daily_Wages )as Average_Daily_Wages,MAX(dd.Joining_date ) as Joining_date ,MAX(dd.Left_Date )as Left_Date,MAX(dd.Still_Working )as Still_Working"
            mainqry += " ,MAX (dd.HEAD_VALUE )as HEAD_VALUE,MAX(dd.Emp_Actual)as Emp_Actual,'" & From_Date.ToString & "' as From_Date,'" & To_Date.ToString & "' as To_date,'" + txtFromYear.Text + "' as From_Year,'" + txtFromYear.Text + "' as To_Year ,max(Arrear) as  Arrear,max(Location) as Location,max(Loc_Address)as Loc_Address,max(ACTUAL_Amount1)as ACTUAL_Amount1,sum( total_actual) as total_actual   ,max(Employer_Name)as Employer_Name,max(Employer_desg)as Employer_desg,max(Employer_Address)as Employer_Address ,max(Nearestcity)as Nearestcity,max(ESIC_No) as ESIC_No  from(" + SecondQry + ")  as dd where dd.HEAD_VALUE >0  group by ESI_No, Emp_Name ,_Month,Total_Amount,D_Month) as s   Pivot(  max(total_actual) FOR [_month] IN (October_Amt,November_Amt,December_Amt, January_Amt, February_Amt,March_Amt) )AS pivott )as t Pivot( MAX(Date_Of_Challan) FOR [D_Month] IN (October_Date,November_Date,December_Date,January_Date, February_Date, March_Date) )AS pivotr "
            Dim finalqry As String = "select max(Comp_ESIC_No) as Comp_ESIC_No,max(Comp_Name) as Comp_Name,max(ESI_No) as ESI_No,Emp_Name ,sum(No_Of_Days) as No_Of_Days ,sum(Average_Daily_Wages) as Average_Daily_Wages , max(From_Month) as From_Month,max(To_Month) as To_Month ,max(Joining_date) as Joining_date,max(Left_Date) as Left_Date ,max(Still_Working) as Still_Working ,sum(HEAD_VALUE) as HEAD_VALUE ,sum(Emp_Actual) as Emp_Actual ,max(From_Date) From_Date ,max(To_date) as To_date ,max(From_Year) as From_Year ,max(To_Year) as To_Year ,max(Location) as Location ,max(October_Date)as October_Date,max(November_Date) as November_Date,max(December_Date) as December_Date, max(January_Date) as January_Date, max(February_Date) as February_Date,max(March_Date) as March_Date    ,max(Arrear) as Arrear,max(Loc_Address) as Loc_Address,sum(ACTUAL_Amount1) as ACTUAL_Amount1 ,max(Employer_Name) as Employer_Name,max(Employer_desg) as Employer_desg,max(Employer_Address) as Employer_Address ,max(Nearestcity) as Nearestcity, max(ESIC_No) as ESIC_No ,sum(October_Amt) as October_Amt,sum(November_Amt) as November_Amt,sum(December_Amt ) as December_Amt,sum(January_Amt ) as January_Amt,sum(February_Amt) as  February_Amt,sum(March_Amt ) as March_Amt from (" + mainqry + ")  as FinalQry  group  by Emp_Name "



            Dim bdiqry As String = "select * from (" + finalqry + ")as ll"
            Dim October_Amt1 As String = " select sum(October_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim October As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(October_Amt1))
            Dim November_Amt1 As String = " select sum(November_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim November As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(November_Amt1))
            Dim December_Amt1 As String = " select sum(December_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim December As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(December_Amt1))
            Dim January_Amt1 As String = " select sum(January_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim January As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(January_Amt1))
            Dim February_Amt1 As String = " select sum(February_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim February As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(February_Amt1))
            Dim March_Amt1 As String = " select sum(March_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim March As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(March_Amt1))
            Dim Arrear_Amt1 As String = " select sum(Arrear)as total_actual from(" + bdiqry + ")as m "
            Dim Arrear1 As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Arrear_Amt1))
            Dim Employershare1 As String = " select sum(ACTUAL_Amount1)as ACTUAL_Amount1 from(" + bdiqry + ")as tt "
            Dim Employershare As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Employershare1))

            Dim Sbsebdiqry As String = " select *," & October & " as October," & November & " as November," & December & " as December," & January & " as January," & February & " as February," & March & " as March," & Arrear1 & " as Arrear1,'" & Employershare & "' as Employer_Total from (" + bdiqry + ")as lll "
            Dim TotalAmountPaid As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (coalesce(October,0)+coalesce(November,0)+coalesce(December,0)+coalesce(January,0)+coalesce(February,0)+coalesce(March,0)+coalesce(Arrear,0)) as TotalAmountPaid from (" + Sbsebdiqry + ")as g "))
            Dim strqry As String = "select *,'" & TotalAmountPaid & "' as TotalAmountPaid from (" + Sbsebdiqry + ")as ll"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(strqry)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptESCIForm6", "ESCI Form6")

        ElseIf cbMonth.Text = "APR-SEP" Then
            

            From_Date = "01/04/" & (txtFromYear.Text)
            To_Date = "30/09/" & (txtFromYear.Text)
            Dim Comp_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
            Dim CompESIC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_ESIC_NO from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
       

            Dim InnerQry As String = " select head.ESI_NO,head.Emp_Name,head.HEAD_VALUE,detail.Month,RATE_AMOUNT,head.ACTUAL_AMOUNT ,detail.Date_Of_Challan,head.PAYABLE_DAYS,head.Average_Daily_Wages,head.SUB_HEAD_TYPE,head.Joining_date ,head.Left_Date, case when ISNULL(head.Left_Date,'')='' then 'Yes' else 'No' end as Still_Working ,Location,Loc_Address,ACTUAL_Amount1,Employer_Name,Employer_desg,Employer_Address ,Nearestcity,ESIC_No from (select distinct TSPL_EMPLOYEE_MASTER.DEVISION_CODE  ,TSPL_EMPLOYEE_MASTER.EMP_CODE ,Emp_Name, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER. add2,TSPL_COMPANY_MASTER.City_Code" & _
                        " ,TSPL_EMPLOYEE_MASTER.ESI_NO,Joining_date ,convert(varchar,RELIEVING_DATE,103) as Left_Date,RATE_AMOUNT,ACTUAL_AMOUNT,Round(HEAD_VALUE,0)as HEAD_VALUE,TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,PAYABLE_DAYS,case when PAYABLE_DAYS = 0 then 0 else (HEAD_VALUE/PAYABLE_DAYS) end as Average_Daily_Wages,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE,Location_Desc as Location , "
            InnerQry += "  '" & LocAddress & "' as Loc_Address, round(TSPL_GENERATE_SALARY_PAYHEADS.Co_ESI_AMT,0) as ACTUAL_Amount1 ,Employer_Name,Employer_desg,(Employer_Add1+','+ Employer_Add2 + ',' + Employer_Add3) as Employer_Address,Nearestcity,ESIC_No from TSPL_GENERATE_SALARY_PAYHEADS " & _
                        " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE " & _
                        " INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE " & _
                        " INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " & _
                        " INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                        "   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code  " & _
                        "  left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  " & _
                        "  left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code  " & _
                        "   left outer join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE  =TSPL_GENERATE_SALARY.DEVISION_CODE   " & _
                         " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_GENERATE_SALARY_PAYHEADS.Emp_code " & _
                        " where TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 and TSPL_EMPLOYEE_MASTER.ISESI =1 "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                InnerQry += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                InnerQry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
            End If
            InnerQry += " )head"
            InnerQry += " Left Join (select DATENAME (MONTH ,CONVERT(date,Date_To,103))as Month,max(Date_To)as Date_Of_Challan,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  from TSPL_GENERATE_SALARY left join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.Pay_Period_Code=TSPL_GENERATE_SALARY.Pay_Period_Code left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.LOCATION_CODE left join TSPL_DEVISION_MASTER on  TSPL_DEVISION_MASTER.devision_code=TSPL_GENERATE_SALARY.devision_code where SUB_HEAD_TYPE in ('EMPESI')and ACTUAL_AMOUNT > 0 "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                InnerQry += " and TSPL_GENERATE_SALARY.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                InnerQry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
            End If
            InnerQry += " group by Date_To,TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE    )detail on head.SALARY_GENERATION_CODE=detail.SALARY_GENERATION_CODE "

            Dim SecondQry As String = " select xx.Emp_Name ,xx.ESI_NO ,xx.Month+'_Amt' as _Month,xx.Month+'_Date' as D_Month  ,xx.Emp_Actual ,xx.Date_Of_Challan ,xx.PAYABLE_DAYS ,xx.Average_Daily_Wages ,xx.Joining_date ,xx.Left_Date ,xx.Still_Working ,xx.HEAD_VALUE,17827 as Total_Amount,xx.Arrear ,xx.Location,xx.Loc_Address,xx.ACTUAL_Amount1,xx.total_actual,xx.Employer_Name,xx.Employer_desg,xx.Employer_Address,xx.Nearestcity,xx.ESIC_No  from(SELECT  SUM(case when SUB_HEAD_TYPE='ARREAR' THEN actual_amount ELSE 0 end) AS Arrear, Final.Emp_Name,Final.ESI_NO,Final.Month,sum( ACTUAL_AMOUNT ) as Emp_Actual,SUM(actual_amount+ACTUAL_Amount1) as total_actual,convert (varchar ,Final.Date_Of_Challan,103) as Date_Of_Challan ,Final.PAYABLE_DAYS ,Final.Average_Daily_Wages ,Final.Joining_date ,Final.Left_Date ,Final .Still_Working,final.HEAD_VALUE  ,Final.Location,final.Loc_Address,ACTUAL_Amount1,max(Employer_Name)  as Employer_Name,max(Employer_desg)as Employer_desg,max(Employer_Address)as Employer_Address,max(Nearestcity) as Nearestcity,max(ESIC_No) as ESIC_No FROM  (" + InnerQry + ") AS Final"
            SecondQry += " where convert(date,Date_Of_Challan,103)>=convert(date,'" & From_Date.ToString & "',103) and convert(date,Date_Of_Challan,103) <=convert(date,'" & To_Date.ToString & "' ,103) group by Final.Emp_Name,Final.ESI_NO,Final.Month,Final.Date_Of_Challan ,Final.PAYABLE_DAYS ,Final.Average_Daily_Wages ,Final.Joining_date ,Final.Left_Date ,Final .Still_Working,final.HEAD_VALUE ,Location,Loc_Address,ACTUAL_Amount1) xx"
            SecondQry += " union   select Top 1 Emp_Name,ESI_NO ,'April_Amt','April_Date'  , 0,'',0,0,'','','',0,0,0,'','',0,0,'','','','','' from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union select Top 1 Emp_Name,ESI_NO,'May_Amt','May_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''  from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union  select Top 1 Emp_Name,ESI_NO,'June_Amt','June_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''  from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union select Top 1 Emp_Name,ESI_NO,'July_Amt','July_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''   from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union select Top 1 Emp_Name,ESI_NO,'August_Amt','August_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''   from TSPL_EMPLOYEE_MASTER where ESI_NO > 0 and ESI_NO <> '' union select Top 1 Emp_Name,ESI_NO,'September_Amt','September_Date' ,0,'',0,0,'','','',0,0,0,'','',0,0,'','','','',''   from TSPL_EMPLOYEE_MASTER where ESI_NO > 0  and ESI_NO <> ''"
            

            Dim mainqry As String = "select '" + CompESIC + "' as Comp_ESIC_No,'" + Comp_Name + "' as Comp_Name,ESI_No,Emp_Name ,No_Of_Days  ,Average_Daily_Wages ,'April' as From_Month,'September' as To_Month ,Joining_date,Left_Date ,Still_Working ,HEAD_VALUE ,Emp_Actual ,From_Date ,To_date ,From_Year ,To_Year,Location ,April_Date,May_Date,June_Date, July_Date, August_Date,September_Date   "
            mainqry += " ,isnull(April_Amt,0)as April_Amt,isnull(May_Amt ,0)as May_Amt,isnull(June_Amt ,0)as June_Amt ,isnull(July_Amt  ,0)as July_Amt,isnull(August_Amt  ,0)as August_Amt,isnull(September_Amt  ,0)as "
            mainqry += " September_Amt ,isnull(Arrear ,0) as Arrear,Loc_Address,ACTUAL_Amount1 ,Employer_Name,Employer_desg,Employer_Address,Nearestcity,'" & ESIC & "' as ESIC_No from(select  * from(select  (dd.ESI_NO)as ESI_No,Emp_Name,_Month , D_Month ,Total_Amount,MAX (dd.Date_Of_Challan) as Date_Of_Challan,sum (dd.PAYABLE_DAYS )as No_Of_Days,MAX(dd.Average_Daily_Wages )as Average_Daily_Wages,MAX(dd.Joining_date ) as Joining_date ,MAX(dd.Left_Date )as Left_Date,MAX(dd.Still_Working )as Still_Working"
            mainqry += " ,MAX (dd.HEAD_VALUE )as HEAD_VALUE,MAX(dd.Emp_Actual)as Emp_Actual,'" & From_Date.ToString & "' as From_Date,'" & To_Date.ToString & "' as To_date,'" + txtFromYear.Text + "' as From_Year,'" + txtFromYear.Text + "' as To_Year ,max(Arrear) as  Arrear,max(Location) as Location,max(Loc_Address)as Loc_Address,max(ACTUAL_Amount1)as ACTUAL_Amount1,sum( total_actual) as total_actual   ,max(Employer_Name)as Employer_Name,max(Employer_desg)as Employer_desg,max(Employer_Address)as Employer_Address ,max(Nearestcity)as Nearestcity,max(ESIC_No) as ESIC_No  from(" + SecondQry + ")  as dd where dd.HEAD_VALUE >0  group by ESI_No, Emp_Name ,_Month,Total_Amount,D_Month) as s   Pivot(  max(total_actual) FOR [_month] IN (April_Amt,May_Amt,June_Amt, August_Amt, July_Amt,September_Amt) )AS pivott )as t Pivot( MAX(Date_Of_Challan) FOR [D_Month] IN (April_Date,May_Date,June_Date, July_Date, August_Date,September_Date) )AS pivotr "
            Dim finalqry As String = "select max(Comp_ESIC_No) as Comp_ESIC_No,max(Comp_Name) as Comp_Name,max(ESI_No) as ESI_No,Emp_Name ,sum(No_Of_Days) as No_Of_Days ,sum(Average_Daily_Wages) as Average_Daily_Wages , max(From_Month) as From_Month,max(To_Month) as To_Month ,max(Joining_date) as Joining_date,max(Left_Date) as Left_Date ,max(Still_Working) as Still_Working ,sum(HEAD_VALUE) as HEAD_VALUE ,sum(Emp_Actual) as Emp_Actual ,max(From_Date) From_Date ,max(To_date) as To_date ,max(From_Year) as From_Year ,max(To_Year) as To_Year ,max(Location) as Location ,max(April_Date)April_Date,max(May_Date) as May_Date,max(June_Date) as June_Date, max(July_Date) as July_Date, max(August_Date) as August_Date,max(September_Date) as September_Date    ,max(Arrear) as Arrear,max(Loc_Address) as Loc_Address,sum(ACTUAL_Amount1) as ACTUAL_Amount1 ,max(Employer_Name) as Employer_Name,max(Employer_desg) as Employer_desg,max(Employer_Address) as Employer_Address ,max(Nearestcity) as Nearestcity, max(ESIC_No) as ESIC_No ,sum(April_Amt) as April_Amt,sum(may_amt) as may_amt,sum(June_Amt ) as June_Amt,sum(July_Amt ) as July_Amt,sum(August_amt) as  August_amt,sum(September_Amt ) as September_Amt from (" + mainqry + ")  as FinalQry  group  by Emp_Name "



            Dim bdiqry As String = "select * from (" + finalqry + ")as ll"
            Dim April_Amt1 As String = " select sum(April_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim April As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(April_Amt1))
            Dim May_Amt1 As String = " select sum(May_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim May As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(May_Amt1))
            Dim June_Amt1 As String = " select sum(June_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim June As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(June_Amt1))
            Dim July_Amt1 As String = " select sum(July_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim July As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(July_Amt1))
            Dim August_Amt1 As String = " select sum(August_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim August As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(August_Amt1))
            Dim September_Amt1 As String = " select sum(September_Amt)as total_actual from(" + bdiqry + ")as m "
            Dim September As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(September_Amt1))
            Dim Employershare1 As String = " select sum(ACTUAL_Amount1)as ACTUAL_Amount1 from(" + bdiqry + ")as tt "
            Dim Employershare As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Employershare1))
            Dim Sbsebdiqry As String = " select *," & April & " as April," & May & " as May," & June & " as June," & July & " as July," & August & " as August," & September & " as September,'" & Employershare & "' as Employer_Total from (" + bdiqry + ")as lll "
            Dim TotalAmountPaid As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (coalesce(April,0)+coalesce(May,0)+coalesce(June,0)+coalesce(July,0)+coalesce(September,0)+coalesce(August,0)+coalesce(Arrear,0)) as TotalAmountPaid from (" + Sbsebdiqry + ")as g "))
            Dim strqry As String = "select *,'" & TotalAmountPaid & "' as TotalAmountPaid from (" + Sbsebdiqry + ")as ll"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(strqry)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptESCIForm6(Apr-Sep)", "ESCI Form6")
            End If

    End Sub
    Private Sub RptESICForm6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")

    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
    '    Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    'End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ") "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
End Class
