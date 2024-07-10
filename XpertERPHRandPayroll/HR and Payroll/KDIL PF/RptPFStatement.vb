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
        Try
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

            Qry += " '" & LocAddress & "' as Address from TSPL_GENERATE_SALARY_PAYHEADS Inner Join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
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
            If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFStatement", "PF Statement Report")
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        ' LoadReport()
        loaddata()
    End Sub



    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptPFStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
            chkpfsno.Checked = False
            MyLabel1.Visible = False
            TxtPFSNO.Visible = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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


    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Try
            Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') "
            txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
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
    Sub loaddata()

        Try
            Dim pf As String = ""
            Dim str As String = ""
            Dim qry As String = ""
            Dim whr As String = ""
            If chkTransPF.Checked = True Then
                str += " where TSPL_EMPLOYEE_MASTER.Transfer_PF=1 "
            Else
                str += " where  TSPL_EMPLOYEE_MASTER.Transfer_PF<>1 "
            End If
            If chkpfsno.Checked = True Then
                If clsCommon.myLen(TxtPFSNO.Value) > 0 Then
                    pf = " and [PF No] LIKE '" + TxtPFSNO.Value + "%' "
                End If
            End If
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                whr = " AND T1.LOCATION_CODE in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
            End If

            qry = " select '" + objCommonVar.CurrentUser + "' As PrintBy,GS.Pay_Period_Code as [Pay Period],DATEDIFF(YEAR, Convert(date,Birth_date, 103), GETDATE()) -
    CASE 
        WHEN MONTH(GETDATE()) < MONTH(TRY_CAST(Birth_date AS DATE)) OR 
             (MONTH(GETDATE()) = MONTH(TRY_CAST(Birth_date AS DATE)) AND DAY(GETDATE()) < DAY(TRY_CAST(Birth_date AS DATE)))
        THEN 1
        ELSE 0
    END AS age,Final.*,cast(EMPStatus.IS_PF_APPL as integer) AS EPF_AC_01,((case when EMPStatus.IS_PF_APPL=1  then 1 else 0 end)-(case when EMPStatus.IS_PF_APPL=1 and CoEPF_AMT_AC01>0 and  coalesce(CoEPS_AMT_AC10,0)<=0 then 1 else 0 end)) as EPF_AC_10,cast(EMPStatus.IS_PF_APPL as integer) AS EDLI_AC_21, Salary_EPF_AC_01,Salary_EPF_AC_10,Salary_EDLI_AC_21,EPF_Amount_AC_01,Pension_Amount_AC_10,Diff_Amount_AC_01,Admin_Amt_AC_02, CoEPF_RATE_AC01,CoEPF_AMT_AC01,CoEPS_RATE_AC10,CoEPS_AMT_AC10,EDLI_RATE_AC21,EDLI_Amt_AC_21,ESI_HEAD_VALUE  ,ESI_Amount,Co_ESI_RATE,Co_ESI_AMT,convert (int, isnull(Final.OT_HOURS,0)/8 ) + case when  (isnull(Final.OT_HOURS,0)/8 - convert (int, isnull(Final.OT_HOURS,0)/8 ) ) > .49 then .50 else 0 end as OT_HOURS_In_Days,TSPL_EMPLOYEE_MASTER.UANNo as [UAN No] from (SELECT SALARY_GENERATION_CODE,EMP_CODE AS EMP_CODE,EMPLOYEE_NAME,FATHERS_NAME AS [Father Name],[Working City],PF_NO AS [PF No],ESI_NO as [ESI No],BANK_ACC_NO as [Bank Acc No],Birth_date AS [Date of Birth],Joining_date as [Joining Date],CONVERT(VARCHAR,RELIEVING_DATE,103) as [Relieving Date], Designation,Department,LOCATION_DESC AS Location,DEVISION_NAME AS Division,BANK_NAME as [Bank Name],Bank_Branch as [Bank Branch],Bank_Branch_Name as [Bank Branch Name],PAYMENT_MODE AS [Payment Mode],PAYPERIOD_DAYS AS [Month Days],PRESENT_DAYS As [Present Days], PAYABLE_DAYS As [Payable Days],HOLIDAY_DAYS as [Holidays],WEEKLY_OFF_DAYS as [Week Off Days],LEAVE_DAYS as [Leave Days] , SUM(sbasic) AS basic, SUM(scca) AS cca ,SUM(sda) AS da, SUM(shra) AS hra, (case when 'Yes'='Yes' then SUM(sbasic) else 0 end )+(case when 'Yes'='Yes' then SUM(scca) else 0 end )+(case when 'Yes'='Yes' then SUM(sda) else 0 end )+(case when 'Yes'='Yes' then SUM(shra) else 0 end )+(case when 'Yes'='Yes' then 0 else 0 end ) AS Gross , SUM(basic) AS vbasic, SUM(cca) AS vcca, SUM(da) AS vda, SUM(hra) AS vhra, (case when 'Yes'='Yes' then SUM(basic) else 0 end )+(case when 'Yes'='Yes' then SUM(cca) else 0 end )+(case when 'Yes'='Yes' then SUM( da) else 0 end )+(case when 'Yes'='Yes' then SUM(hra) else 0 end )+(case when 'Yes'='Yes' then 0 else 0 end ) AS [GROSS SALARY] , SUM(epf) AS vepf, SUM(gsli) AS vgsli, SUM(kkk) AS vkkk, SUM(swsf) AS vswsf, 0 +(case when 'No'='No' then SUM(epf) else 0 end )+(case when 'No'='No' then SUM(gsli) else 0 end )+(case when 'No'='No' then SUM(kkk) else 0 end )+(case when 'No'='No' then SUM(swsf) else 0 end ) AS [TOTAL DEDUCTION] ,((case when 'Yes'='Yes' then SUM(basic) else 0 end )+(case when 'Yes'='Yes' then SUM(cca) else 0 end )+(case when 'Yes'='Yes' then SUM(da) else 0 end )+(case when 'Yes'='Yes' then SUM(hra) else 0 end )+(case when 'Yes'='Yes' then SUM(wash) else 0 end ))-( 0 +(case when 'No'='No' then SUM(epf) else 0 end )+(case when 'No'='No' then SUM(gsli) else 0 end )+(case when 'No'='No' then SUM(kkk) else 0 end )+(case when 'No'='No' then SUM(swsf) else 0 end )) as [NET SALARY] , sum (OT_HOURS) as OT_HOURS FROM (SELECT ACD.SALARY_GENERATION_CODE,ACD.EMP_CODE,ACD.PAY_HEAD_CODE,EMPLOYEE_NAME,FATHERS_NAME,[Working City],Birth_date,Joining_date,Designation,Department,LOCATION_DESC,DEVISION_NAME, BANK_NAME, Bank_Branch,Bank_Branch_Name,PAYMENT_MODE, ACD.PAYPERIOD_DAYS, ACD.PRESENT_DAYS, ACD.PAYABLE_DAYS, ACD.HOLIDAY_DAYS,ACD.WEEKLY_OFF_DAYS,ACD.LEAVE_DAYS ,PF_NO,ESI_NO,BANK_ACC_NO,RELIEVING_DATE,ACD.OT_HOURS, CASE WHEN  ACD.PAY_HEAD_CODE ='BASIC' THEN acd.Payable_Amount else 0 END  AS sbasic,CASE WHEN  ACD.PAY_HEAD_CODE ='CCA' THEN acd.Payable_Amount else 0 END  AS scca,CASE WHEN  ACD.PAY_HEAD_CODE ='DA' THEN acd.Payable_Amount else 0 END  AS sda,CASE WHEN  ACD.PAY_HEAD_CODE ='HRA' THEN acd.Payable_Amount else 0 END  AS shra,CASE WHEN  ACD.PAY_HEAD_CODE ='WASH.ALL' THEN acd.Payable_Amount else 0 END  AS swashall,CASE WHEN  ACD.PAY_HEAD_CODE ='BASIC' THEN acd.Actual_Amount else 0 END  AS basic,CASE WHEN  ACD.PAY_HEAD_CODE ='CCA' THEN acd.Actual_Amount else 0 END  AS cca,CASE WHEN  ACD.PAY_HEAD_CODE ='DA' THEN acd.Actual_Amount else 0 END  AS da,CASE WHEN  ACD.PAY_HEAD_CODE ='HRA' THEN acd.Actual_Amount else 0 END  AS hra,CASE WHEN  ACD.PAY_HEAD_CODE ='WASH.ALL' THEN acd.Actual_Amount else 0 END  AS wash,CASE WHEN  ACD.PAY_HEAD_CODE ='EPF' THEN acd.Payable_Amount else 0 END  AS epf,CASE WHEN  ACD.PAY_HEAD_CODE ='GSLI LIC' THEN acd.Payable_Amount else 0 END  AS gsli,CASE WHEN  ACD.PAY_HEAD_CODE ='KKK' THEN acd.Payable_Amount else 0 END  AS kkk,CASE WHEN  ACD.PAY_HEAD_CODE ='SWSF' THEN acd.Payable_Amount else 0 END  AS swsf FROM  ( SELECT T1.SALARY_GENERATION_CODE,T1.EMP_CODE,T2.EMP_NAME AS EMPLOYEE_NAME,T2.FATHERS_NAME,T2.WORKING_LOCATION_CODE,Working_City.City_Name as [Working City],T3.DESIGNATION_DESC AS DESIGNATION,T4.DEPARTMENT_NAME AS DEPARTMENT, T1.DEVISION_CODE,T1.LOCATION_CODE,LOC.LOCATION_DESC,Dev.DEVISION_NAME,T2.Bank_Name,T2.Bank_Branch,T2.Bank_Branch_Name,T2.PAYMENT_MODE_New as PAYMENT_MODE,T2.Birth_date,T2.Joining_date,T2.RELIEVING_DATE,T2.BRANCH_CODE,T1.PAY_HEAD_CODE , T1.ACTUAL_AMOUNT,T1.Payable_Amount,T1.PAYPERIOD_DAYS ,T1.PRESENT_DAYS ,T1.PAYABLE_DAYS,T1.HOLIDAY_DAYS,T1.WEEKLY_OFF_DAYS,T1.LEAVE_DAYS,T2.PF_NO,T2.ESI_NO,T2.BANK_ACC_NO,T1.OT_HOURS FROM (SELECT T1.SALARY_GENERATION_CODE,T2.EMP_CODE,T2.PAY_HEAD_CODE,T2.ACTUAL_AMOUNT,T2.Payable_Amount,T5.PAYPERIOD_DAYS,T5.PRESENT_DAYS,T5.PAYABLE_DAYS,T5.HOLIDAY_DAYS,T5.LEAVE_DAYS, (T5.PAYPERIOD_DAYS-T5.PRESENT_DAYS-T5.HOLIDAY_DAYS-T5.LEAVE_DAYS-T5.ABSENT_DAYS) as WEEKLY_OFF_DAYS,T1.LOCATION_CODE,T1.DEVISION_CODE,T2.OT_HOURS FROM TSPL_GENERATE_SALARY T1  INNER JOIN TSPL_GENERATE_SALARY_PAYHEADS T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE  inner JOIN TSPL_GENERATE_SALARY_ATTENDANCE T5 ON T5.Emp_code =T2.Emp_code  AND T1.SALARY_GENERATION_CODE=T5.SALARY_GENERATION_CODE  WHERE 2=2  AND T1.PAY_PERIOD_CODE in ( '" + fndFromPeriod.Value + "') " + whr + " ) T1 LEFT JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.EMP_CODE=T2.EMP_CODE  LEFT JOIN TSPL_DESIGNATION_MASTER T3 ON T2.DESIGNATION=T3.DESIGNATION_ID LEFT JOIN TSPL_DEPARTMENT_MASTER T4 ON T2.DEPARTMENT_CODE=T4.DEPARTMENT_CODE LEFT JOIN TSPL_DEVISION_MASTER Dev ON T1.DEVISION_CODE=Dev.DEVISION_CODE LEFT JOIN TSPL_LOCATION_MASTER LOC ON T1.LOCATION_CODE=LOC.LOCATION_CODE LEFT JOIN TSPL_BANK_MASTER Bank ON T2.BANK_CODE=Bank.BANK_CODE  left join TSPL_City_MASTER as Working_City on Working_City.City_Code  =T2.WORKING_City_CODE) AS ACD) AS X where 2=2   GROUP BY x.[Working City] , X.SALARY_GENERATION_CODE,X.EMP_CODE,X.EMPLOYEE_NAME,X.DESIGNATION,X.DEPARTMENT,X.DEVISION_NAME ,X.PAYPERIOD_DAYS,X.PRESENT_DAYS,X.PAYABLE_DAYS,X.PF_NO,X.BANK_ACC_NO,X.Birth_date,X.HOLIDAY_DAYS,X.WEEKLY_OFF_DAYS, X.LEAVE_DAYS,X.ESI_NO,X.FATHERS_NAME,X.Bank_Name,X.Bank_Branch,X.Bank_Branch_Name,X.PAYMENT_MODE,X.Joining_date,X.RELIEVING_DATE,X.LOCATION_DESC) as Final  left join TSPL_GENERATE_SALARY_ATTENDANCE GSA ON Final.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE  AND Final.EMP_CODE=GSA.EMP_CODE left join TSPL_GENERATE_SALARY GS ON Final.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE  left join TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE  LEFT JOIN TSPL_EMPLOYEE_STATUS EMPStatus on GSA.EMP_STATUS_CODE=EMPStatus.EMP_STATUS_CODE  LEFT JOIN (select TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_01,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_10,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EDLI_AC_21,  max(CASE WHEN SUB_HEAD_TYPE='EPF' then ACTUAL_AMOUNT ELSE 0 END) AS EPF_Amount_AC_01,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then CoEPS_AMT_AC10 ELSE 0 END) AS Pension_Amount_AC_10,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and Actual_Amount>0 then (Actual_Amount-(case when HEAD_VALUE>PF_MAX_LM then CoEPS_AMT_AC10*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else CoEPS_AMT_AC10 end)) ELSE 0 END) AS Diff_Amount_AC_01,  (max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*1.1/100 AS Admin_Amt_AC_02,  (max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*0.5/100 AS EDLI_Amt_AC_21,  max(CASE WHEN SUB_HEAD_TYPE='EPF' then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS ELSE 0 END) AS PF_MAX_LM,  max(CoEPF_RATE_AC01) as CoEPF_RATE_AC01,max(CoEPF_AMT_AC01) as CoEPF_AMT_AC01,max(CoEPS_RATE_AC10) as CoEPS_RATE_AC10,  max(CoEPS_AMT_AC10) as CoEPS_AMT_AC10,max(EDLI_RATE_AC21) as EDLI_RATE_AC21,  max(EDLI_AMT_AC21) as EDLI_AMT_AC21,  max(CASE WHEN SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 then HEAD_VALUE ELSE 0 END) as ESI_HEAD_VALUE,  max(CASE WHEN SUB_HEAD_TYPE='EMPESI' then ACTUAL_AMOUNT ELSE 0 END) AS ESI_Amount,  max(Co_ESI_RATE) as Co_ESI_RATE,max(Co_ESI_AMT) as Co_ESI_AMT  from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where SUB_HEAD_TYPE in ('EPF','EMPESI')  group by TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE) AS GSP ON Final.SALARY_GENERATION_CODE=GSP.SALARY_GENERATION_CODE  AND Final.EMP_CODE=GSP.EMP_CODE LEFT JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=Final.EMP_CODE " + str + " " + pf + "  ORDER BY Final.EMP_CODE,PPM.DATE_FROM



"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFstatementt", "PF Statement Report")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkpfsno_CheckStateChanged(sender As Object, e As EventArgs) Handles chkpfsno.CheckStateChanged
        If chkpfsno.Checked = True Then
            TxtPFSNO.Visible = True
            MyLabel1.Visible = True
        Else
            TxtPFSNO.Visible = False
            MyLabel1.Visible = False
            TxtPFSNO.Value = ""
        End If
    End Sub

    Private Sub TxtPFSNO__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtPFSNO._MYValidating
        Try
            Dim qry As String = " SELECT 
                 distinct SUBSTRING(PF_NO, 1, CHARINDEX('/', PF_NO) - 1) AS PF_SNo
            FROM TSPL_EMPLOYEE_MASTER "
            Dim whrcls As String = " CHARINDEX('/', PF_NO) > 0"
            TxtPFSNO.Value = clsCommon.ShowSelectForm("PSSNO", qry, "PF_SNo", whrcls, TxtPFSNO.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        TxtPFSNO.Value = ""
        fndFromPeriod.Value = ""
        txtLocationMult.arrValueMember = Nothing
        txtDivisionMult.arrValueMember = Nothing
        lblFromPeriodName.Text = ""
        chkpfsno.Checked = False
    End Sub
End Class
