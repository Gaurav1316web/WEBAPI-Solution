'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptPFStatement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPFStatement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadReport()
        '' changed by Panch raj agaist Ticket No:BM00000008188
        ''changes by shivani against [BM00000008636]
        Dim Qry As String
        Dim PensionRate As Double
        Dim COEPF As Double
        Dim ACCOEPF As Double
        Dim COEDLI As Double
        Dim ACCOEDLI As Double
        Dim EMPEPF_MAX As Double
        Dim LocationFirstTime As Integer = 0
        Dim LocAddress As String = ""
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
            LocationFirstTime += 1
            LocAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + add4 As [Address] FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"))
        Else
            LocAddress = objCommonVar.CurrentCompanyName
        End If
        Dim objPF As clsPFRulesMaster = clsPFRulesMaster.GetRecentPFRule(fndFromPeriod.Value)
        Dim RountOffType As String = objPF.COEPF_ROUNDOFF_YPE 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPF_ROUNDOFF_YPE  FROM TSPL_PF_RULE_MASTER where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
        Dim Comp_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
        Dim CompPF As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_PF_NO from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
        PensionRate = objPF.COEPS_PER 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPS_PER FROM TSPL_PF_RULE_MASTER  where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
        COEPF = objPF.COEPF_PER 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPF_PER FROM TSPL_PF_RULE_MASTER  where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
        ACCOEPF = objPF.ACCOEPF_PER 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 ACCOEPF_PER FROM TSPL_PF_RULE_MASTER  where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
        COEDLI = objPF.ACCOEDLI_PER 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEDLI_PER FROM TSPL_PF_RULE_MASTER  where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
        ACCOEDLI = objPF.ACCOEDLI_PER 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 ACCOEDLI_PER FROM TSPL_PF_RULE_MASTER  where convert(date,APPLICABLE_FROM,103) <= (select convert(date,DATE_FROM,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" & fndFromPeriod.Value & "') ORDER BY APPLICABLE_FROM Desc"))
        EMPEPF_MAX = objPF.EMPEPF_MAX
        'Qry = " select *, (ACTUAL_AMOUNT - pension_fund)as Employer_Share   from(select Emp_Name,PF_NO ,ACTUAL_AMOUNT,HEAD_VALUE,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,City_Code ,'" & fndFromPeriod.Value & "' AS FROM_PAY_PERIOD ,case when TSPL_GENERATE_SALARY_ATTENDANCE.EPS_TO_EPF=0 then ACTUAL_AMOUNT else (('" & PensionRate & "') * HEAD_VALUE /100) end as pension_fund from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE    INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE"
        'Qry += " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  "
        'Qry += " left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code   WHERE  TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='EPF' AND TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0 and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "')as d"
        Qry = "  select *,'" + objCommonVar.CurrentCompanyName + "' as CompName,(pension_fund + Diff)as Total from (select *,(ACTUAL_AMOUNT-pension_fund)as Diff from(select *, (pension_fund + COEPF_PER + ACCOEPF_PER+COEDLI_PER+ACCOEDLI_PER)as Employer_Share   from(select TSPL_EMPLOYEE_MASTER.UANNo,TSPL_DEVISION_MASTER.DEVISION_CODE ,TSPL_DEVISION_MASTER.DEVISION_NAME ,TSPL_LOCATION_MASTER .Location_Code ,TSPL_EMPLOYEE_MASTER.EMP_CODE, Emp_Name,convert(varchar,TSPL_EMPLOYEE_MASTER.Joining_date,103) as Joining_date,TSPL_EMPLOYEE_MASTER.PF_NO ,TSPL_LOCATION_MASTER.PF_NO as Comp_PF_NO,(case when coalesce(TSPL_GENERATE_SALARY_PAYHEADS.EPF_RATE,0)>0 then (ACTUAL_AMOUNT-(CoEPS_AMT_AC10+CoEPF_AMT_AC01)) else 0 end) as VPF,(case when coalesce(TSPL_GENERATE_SALARY_PAYHEADS.EPF_RATE,0)<=0 then ACTUAL_AMOUNT else (CoEPS_AMT_AC10+CoEPF_AMT_AC01) end) as ACTUAL_AMOUNT,ROUND(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end,0)AS HEAD_VALUE,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.City_Code ,'" & fndFromPeriod.Value & "' AS FROM_PAY_PERIOD ,"
        'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
        '    Qry += " (ROUND(('" & PensionRate & "')* HEAD_VALUE /100, 0))"
        'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
        '    Qry += " (FLOOR('" & PensionRate & "')* HEAD_VALUE /100)"
        'Else
        '    Qry += " (CEILING('" & PensionRate & "')* HEAD_VALUE /100)"
        'End If
        'Qry += "  round((case when HEAD_VALUE>PF_MAX_LM and TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0  then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS*CoEPS_RATE_AC10/100 when TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0 then TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10 else 0 end),0)   as pension_fund,"
        Qry += "  round((case when TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10<0 then 0 else TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10 end),0)   as pension_fund,"
        'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
        '    Qry += " (ROUND(('" & COEPF & "')* HEAD_VALUE /100, 0))"
        'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
        '    Qry += " (FLOOR('" & COEPF & "')* HEAD_VALUE /100)"
        'Else
        '    Qry += " (CEILING('" & COEPF & "')* HEAD_VALUE /100)"
        'End If
        Qry += " TSPL_GENERATE_SALARY_PAYHEADS.CoEPF_AMT_AC01 as COEPF_PER,"
        'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
        '    Qry += " (ROUND(('" & ACCOEPF & "')* HEAD_VALUE /100, 0))"
        'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
        '    Qry += " (FLOOR('" & ACCOEPF & "')* HEAD_VALUE /100)"
        'Else
        '    Qry += " (CEILING('" & ACCOEPF & "')* HEAD_VALUE /100)"
        'End If
        Qry += " TSPL_GENERATE_SALARY_PAYHEADS.ADMIN_AMT_AC02 as ACCOEPF_PER,"
        'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
        '    Qry += " (ROUND(('" & COEDLI & "')* HEAD_VALUE /100, 0))"
        'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
        '    Qry += " (FLOOR('" & COEDLI & "')* HEAD_VALUE /100)"
        'Else
        '    Qry += " (CEILING('" & COEDLI & "')* HEAD_VALUE /100)"
        'End If
        Qry += " TSPL_GENERATE_SALARY_PAYHEADS.EDLI_AMT_AC21 as COEDLI_PER,"
        'If clsCommon.CompairString("" + RountOffType + "", "R") = CompairStringResult.Equal Then
        '    Qry += " (ROUND(('" & ACCOEDLI & "')* HEAD_VALUE /100, 0))"
        'ElseIf clsCommon.CompairString("" + RountOffType + "", "L") = CompairStringResult.Equal Then
        '    Qry += " (FLOOR('" & ACCOEDLI & "')* HEAD_VALUE /100)"
        'Else
        '    Qry += " (CEILING('" & ACCOEDLI & "')* HEAD_VALUE /100)"
        'End If
        Qry += " TSPL_GENERATE_SALARY_PAYHEADS.EDLI_AMT_AC21 as ACCOEDLI_PER,"

        Qry += " TSPL_LOCATION_MASTER.Location_Desc , "


        'Qry += "TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End  "

        Qry += " '" & LocAddress & "' as Address from TSPL_GENERATE_SALARY_PAYHEADS Inner Join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " & _
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE "
        ' " left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  " -- Duplicate Records Coming because of this join
        Qry += " left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_GENERATE_SALARY.DEVISION_CODE   left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code   left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_Code left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State WHERE  TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='EPF' " ' AND TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0  
        Qry += " AND (TSPL_GENERATE_SALARY_PAYHEADS.PF_Applicable=1 or  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0)"
        Qry += "  and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "' "
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If

        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Devision_Code  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        If chkTransPF.Checked = True Then
            Qry += " and TSPL_EMPLOYEE_MASTER.Transfer_PF=1 "
        Else
            Qry += " and  TSPL_EMPLOYEE_MASTER.Transfer_PF<>1 "
        End If
        If chkTransPF.Checked = False Then
            Qry += "  union 
            (select TSPL_EMPLOYEE_MASTER.UANNo,TSPL_DEVISION_MASTER.DEVISION_CODE ,TSPL_DEVISION_MASTER.DEVISION_NAME ,TSPL_LOCATION_MASTER .Location_Code ,TSPL_EMPLOYEE_MASTER.EMP_CODE, Emp_Name,convert(varchar,TSPL_EMPLOYEE_MASTER.Joining_date,103) as Joining_date,TSPL_EMPLOYEE_MASTER.PF_NO ,TSPL_LOCATION_MASTER.PF_NO as Comp_PF_NO,

            (case when coalesce(TSPL_EPF_ENTRY_DETAIL.MAX_EPF_AMT,0)>0 then (ACTUAL_AMOUNT-(TSPL_EPF_ENTRY_DETAIL.COEPS_AC10+TSPL_EPF_ENTRY_DETAIL.COEPF_AC01)) else 0 end) as VPF,
            (case when coalesce(TSPL_EPF_ENTRY_DETAIL.MAX_EPF_AMT,0)<=0 then TSPL_EPF_ENTRY_DETAIL.ACTUAL_AMOUNT else (TSPL_EPF_ENTRY_DETAIL.COEPS_AC10+TSPL_EPF_ENTRY_DETAIL.COEPF_AC01) end) as ACTUAL_AMOUNT,
            ROUND(TSPL_EPF_ENTRY_DETAIL.HeadValue,0)AS HEAD_VALUE,
            TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.City_Code ,'" & fndFromPeriod.Value & "' AS FROM_PAY_PERIOD , 
            round((case when TSPL_EPF_ENTRY_DETAIL.COEPS_AC10<0 then 0 else TSPL_EPF_ENTRY_DETAIL.COEPS_AC10 end),0)   as pension_fund, 

            TSPL_EPF_ENTRY_DETAIL.COEPF_AC01 as COEPF_PER,	
            TSPL_EPF_ENTRY_DETAIL.Adm_EPF_ACEPF_AC02 as ACCOEPF_PER, TSPL_EPF_ENTRY_DETAIL.EDLI_COM_AC21 as COEDLI_PER, 
            TSPL_EPF_ENTRY_DETAIL.EDLI_COM_AC21 as ACCOEDLI_PER, TSPL_LOCATION_MASTER.Location_Desc, '" & LocAddress & "' as Address from
            TSPL_EPF_ENTRY_DETAIL
            LEFT OUTER JOIN TSPL_EPF_ENTRY ON TSPL_EPF_ENTRY.DOC_Code=TSPL_EPF_ENTRY_DETAIL.DOC_CODE
            INNER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EPF_ENTRY_DETAIL.EMP_CODE
            left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_EMPLOYEE_MASTER.DEVISION_CODE   
            left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code   
            left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EMPLOYEE_MASTER.Location_Code 
            left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State
            WHERE  TSPL_EPF_ENTRY.Status=1 AND  TSPL_EPF_ENTRY.PAY_PERIOD_CODE='" & fndFromPeriod.Value & "'  and TSPL_EMPLOYEE_MASTER.Transfer_PF<>1 )"
        End If
        Qry += " )as d)as m)as ll"
        Qry += " order by PF_NO "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFStatement", "PF Statement Report")
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadReport()
    End Sub


  
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptPFStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
    End Sub

    Private Sub fndFromPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFromPeriod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
           & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        fndFromPeriod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", fndFromPeriod.Value, "", isButtonClicked)
        lblFromPeriodName.Text = clsPayPeriodMaster.GetName(fndFromPeriod.Value, Nothing)
    End Sub


    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
End Class
