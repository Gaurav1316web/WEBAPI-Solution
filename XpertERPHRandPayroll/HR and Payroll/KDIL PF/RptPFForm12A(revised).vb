'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptPFForm12A_revised_
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPFForm12A_revised_)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub fndFromPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFromPeriod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
          & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        fndFromPeriod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", fndFromPeriod.Value, "", isButtonClicked)
        lblFromPeriodName.Text = clsPayPeriodMaster.GetName(fndFromPeriod.Value, Nothing)
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        ''LoadReport()
        PrintData()
    End Sub
    Sub PrintData()
        Try
            Dim QryBase As String = ""
            Dim PrevPPCode As String = ""
            Dim CurrPPCode As String = fndFromPeriod.Value
            Dim Location_Code As String = ""
            Dim DivCond As String = ""
            Dim LEFT_EMP_EPF As Integer = 0
            Dim NEW_EMP_EPF As Integer = 0
            Dim LEFT_EMP_Pension As Integer = 0
            Dim NEW_EMP_Pension As Integer = 0
            Dim LEFT_EMP_EDLI As Integer = 0
            Dim NEW_EMP_EDLI As Integer = 0
            Dim NoOfEmployee As Integer = 0
            Dim WithPFEMP As Integer = 0
            Dim NoPFEMP As Integer = 0
            Dim TotalEMPEPFCurr As Integer = 0
            Dim TotalEMPPensionCurr As Integer = 0
            Dim TotalEMPEDLICurr As Integer = 0
            Dim TotalEMPEPFPrev As Integer = 0
            Dim TotalEMPPensionPrev As Integer = 0
            Dim TotalEMPEDLIPreV As Integer = 0
            Dim PF_No As String = ""

            PrevPPCode = clsPayPeriodMaster.GetPreviousPayPeriod(CurrPPCode, Nothing)

            If clsCommon.myLen(CurrPPCode) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Pay Period.", Me.Text)
                Return
            End If


            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                Location_Code = "(" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
                Exit Sub
            End If

            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                DivCond = " and EMP.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
            End If
            
            Dim LocName As String = clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code in " & Location_Code & "")            
            LocName += " " + Environment.NewLine
            PF_No = clsDBFuncationality.getSingleValue("SELECT top 1 PF_NO FROM TSPL_LOCATION_MASTER  where Location_Code in " & Location_Code & "")
            'Dim LocAdress As String = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(City_Code ,'')='' Then '' else '-'+CONVERT(varchar, City_Code)  End End End as Location_Address from TSPL_LOCATION_MASTER where Location_Code in " & Location_Code & "")
            Dim LocationAddress As String
            Dim LocationFirstTime As Integer = 0
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
                LocationFirstTime += 1
                LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")")
            Else
                LocationAddress = objCommonVar.CurrentCompanyName
            End If
            Dim DivisionAddress As String = ""
            Dim DivisionFirstTime As Integer = 0
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count = 1 Then
                DivisionFirstTime += 1
                LocName += clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select DEVISION_NAME  from  TSPL_DEVISION_MASTER   WHERE DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & ")"))
            End If
            Dim dtPrev As New DataTable
            If clsCommon.myLen(PrevPPCode) > 0 Then
                QryBase = clsSalaryGeneration.GetPFESIQuery(PrevPPCode, Location_Code, DivCond, LocName, LocationAddress, PF_No, "")
                dtPrev = clsDBFuncationality.GetDataTable(QryBase)
                If dtPrev.Rows.Count > 0 Then
                    TotalEMPEPFPrev = clsCommon.myCdbl(dtPrev.Rows(0).Item("TotalEmpEPFAc01"))
                    TotalEMPPensionPrev = clsCommon.myCdbl(dtPrev.Rows(0).Item("TotalEmpPensionAC10"))
                    TotalEMPEDLIPreV = clsCommon.myCdbl(dtPrev.Rows(0).Item("TotalEmpEDLIAc21"))
                End If
            End If
            QryBase = ""
            QryBase = clsSalaryGeneration.GetPFESIQuery(CurrPPCode, Location_Code, DivCond, LocName, LocationAddress, PF_No, "")
            Dim dtCurr As DataTable
            dtCurr = clsDBFuncationality.GetDataTable(QryBase)
            If dtCurr.Rows.Count > 0 Then
                TotalEMPEPFCurr = clsCommon.myCdbl(dtCurr.Rows(0).Item("TotalEmpEPFAc01"))
                TotalEMPPensionCurr = clsCommon.myCdbl(dtCurr.Rows(0).Item("TotalEmpPensionAC10"))
                TotalEMPEDLICurr = clsCommon.myCdbl(dtCurr.Rows(0).Item("TotalEmpEDLIAc21"))
                NoOfEmployee = clsCommon.myCdbl(dtCurr.Rows(0).Item("Total_Employee_Count"))
                WithPFEMP = TotalEMPEPFCurr
                NoPFEMP = NoOfEmployee - WithPFEMP

                'If TotalEMPEPFCurr > TotalEMPEPFPrev Then
                '    NEW_EMP_EPF = TotalEMPEPFCurr - TotalEMPEPFPrev
                '    LEFT_EMP_EPF = 0
                'ElseIf TotalEMPEPFCurr < TotalEMPEPFPrev Then
                '    NEW_EMP_EPF = 0
                '    LEFT_EMP_EPF = TotalEMPEPFPrev - TotalEMPEPFCurr
                'Else
                '    NEW_EMP_EPF = 0
                '    LEFT_EMP_EPF = 0
                'End If

                'If TotalEMPPensionCurr > TotalEMPPensionPrev Then
                '    NEW_EMP_Pension = TotalEMPPensionCurr - TotalEMPPensionPrev
                '    LEFT_EMP_Pension = 0
                'ElseIf TotalEMPPensionCurr < TotalEMPPensionPrev Then
                '    NEW_EMP_Pension = 0
                '    LEFT_EMP_Pension = TotalEMPPensionPrev - TotalEMPPensionCurr
                'Else
                '    NEW_EMP_Pension = 0
                '    LEFT_EMP_Pension = 0
                'End If

                'If TotalEMPEDLICurr > TotalEMPEDLIPreV Then
                '    NEW_EMP_EDLI = TotalEMPEDLICurr - TotalEMPEDLIPreV
                '    LEFT_EMP_EDLI = 0
                'ElseIf TotalEMPEDLICurr < TotalEMPEDLIPreV Then
                '    NEW_EMP_EDLI = 0
                '    LEFT_EMP_EDLI = TotalEMPEDLIPreV - TotalEMPEDLICurr
                'Else
                '    NEW_EMP_EDLI = 0
                '    LEFT_EMP_EDLI = 0
                'End If
            End If
            NEW_EMP_EPF = clsSalaryGeneration.GetPFNewEmpListCount(PrevPPCode, CurrPPCode, txtLocationMult.arrValueMember, txtDivisionMult.arrValueMember, Nothing)
            LEFT_EMP_EPF = clsSalaryGeneration.GetPFLeftEmpListCount(PrevPPCode, CurrPPCode, txtLocationMult.arrValueMember, txtDivisionMult.arrValueMember, Nothing)

            NEW_EMP_Pension = clsSalaryGeneration.GetPFPensionNewEmpListCount(PrevPPCode, CurrPPCode, txtLocationMult.arrValueMember, txtDivisionMult.arrValueMember, Nothing)
            LEFT_EMP_Pension = TotalEMPPensionPrev + NEW_EMP_Pension - TotalEMPPensionCurr 'clsSalaryGeneration.GetPFLeftPensionEmpListCount(PrevPPCode, CurrPPCode, txtLocationMult.arrValueMember, txtDivisionMult.arrValueMember, Nothing)

            NEW_EMP_EDLI = NEW_EMP_EPF
            LEFT_EMP_EDLI = LEFT_EMP_EPF

            QryBase = "select " & LEFT_EMP_EPF & " as LEFT_EMP_EPF," & NEW_EMP_EPF & " as NEW_EMP_EPF," & LEFT_EMP_Pension & " as LEFT_EMP_Pension, " & _
                " " & NEW_EMP_Pension & " as NEW_EMP_Pension," & LEFT_EMP_EDLI & " as LEFT_EMP_EDLI," & NEW_EMP_EDLI & " as NEW_EMP_EDLI, " & NoOfEmployee & " as NoOfEmployee, " & _
                " " & WithPFEMP & " as WithPFEMP," & NoPFEMP & " as NoPFEMP," & TotalEMPEPFCurr & " as TotalEMPEPFCurr," & TotalEMPPensionCurr & " as TotalEMPPensionCurr, " & _
                " " & TotalEMPEDLICurr & " as TotalEMPEDLICurr," & TotalEMPEPFPrev & " as TotalEMPEPFPrev," & TotalEMPPensionPrev & " as TotalEMPPensionPrev, " & _
                " " & TotalEMPEDLIPreV & " as TotalEMPEDLIPreV,'EPS' as Ac10,'EPF A/c No. 01' as AC01,'D.L.I A/c No. 21' as Ac21,12 as Statutory,'" & CurrPPCode & "' as PayPeriod,'" & LocationAddress & "' as LocAddress,Final.* from (" & QryBase & ") Final"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(QryBase)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "crptPFForm12A(Revised)", "PF Form 12(A)")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub
    
    Sub LoadReport()
        
        Dim TOTAL_EMP_LAST_MONTH As Integer = 0
        Dim NEW_EMP As Integer = 0
        Dim LEFT_EMP As Integer = 0
        Dim NET_EMP As Integer = 0
        Dim StatutoryRate As Double
        Dim EMPEPF_MAX As Double
        Dim PensionRate As Double
        Dim NoOfExcludeEmp As Double
        Dim RestEmp As Double
        Dim ACCOEDLI_PER As Double
        Dim COEDLI_PER As Double
        Dim COEPS_PER As Double
        Dim ACCOEPF_PER As Double
        Dim Location_Code As String = ""
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Location_Code = "(" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"

        Else
            clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
            Exit Sub
        End If
        Dim objPF As clsPFRulesMaster = clsPFRulesMaster.GetRecentPFRule(fndFromPeriod.Value)
        Dim LocationFirstTime As Integer = 0
        Dim LocationAddress As String

        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
            LocationFirstTime += 1
            LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")")
        Else
            LocationAddress = objCommonVar.CurrentCompanyName
        End If
        Dim DivCond As String = ""
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            DivCond += " AND ISNULL(EMP.Devision_code,'')  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "

        End If
        PensionRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPS_PER FROM TSPL_PF_RULE_MASTER  where CONVERT(date,APPLICABLE_FROM ,103)<=(select convert(date,date_from,103) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "') ORDER BY APPLICABLE_FROM Desc;"))
        StatutoryRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 EMPEPF_PER  FROM TSPL_PF_RULE_MASTER where CONVERT(date,APPLICABLE_FROM ,103)<=(select convert(date,date_from,103) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "') ORDER BY APPLICABLE_FROM Desc;"))
        EMPEPF_MAX = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Top 1 EMPEPF_MAX from TSPL_PF_RULE_MASTER where CONVERT(date,APPLICABLE_FROM ,103)<=(select convert(date,date_from,103) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "') order by APPLICABLE_FROM desc;"))
        ACCOEDLI_PER = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 ACCOEDLI_PER  FROM TSPL_PF_RULE_MASTER where CONVERT(date,APPLICABLE_FROM ,103)<=(select convert(date,date_from,103) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "') ORDER BY APPLICABLE_FROM Desc;"))
        COEDLI_PER = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEDLI_PER  FROM TSPL_PF_RULE_MASTER where CONVERT(date,APPLICABLE_FROM ,103)<=(select convert(date,date_from,103) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "') ORDER BY APPLICABLE_FROM Desc;"))
        COEPS_PER = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 COEPS_PER  FROM TSPL_PF_RULE_MASTER where CONVERT(date,APPLICABLE_FROM ,103)<=(select convert(date,date_from,103) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "') ORDER BY APPLICABLE_FROM Desc;"))
        ACCOEPF_PER = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT TOP 1 ACCOEPF_PER  FROM TSPL_PF_RULE_MASTER where CONVERT(date,APPLICABLE_FROM ,103)<=(select convert(date,date_from,103) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "') ORDER BY APPLICABLE_FROM Desc;"))


        Dim arrLoc() As String = Location_Code.Replace("(", "").Replace(")", "").Split(",")
        Dim AdminChargesAc21 As Decimal = 0
        Dim qryAdminEDLI As String = ""
        For Each strLoc As String In arrLoc
            qryAdminEDLI = "select (CASE WHEN  (((select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE = " & strLoc & " and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & fndFromPeriod.Value & "' " & DivCond & ")* " & ACCOEDLI_PER & ")/100)>200 THEN ((select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE in " & Location_Code & " and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & fndFromPeriod.Value & "' " & DivCond & ")* " & ACCOEDLI_PER & ")/100 ELSE 200 END) as Admin_EDLI_Amt"
            AdminChargesAc21 = AdminChargesAc21 + clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qryAdminEDLI))
        Next

      
        Dim Qry As String = ""
        Qry += " SELECT (SELECT COUNT(EMP_CODE) FROM TSPL_EMPLOYEE_MASTER T1 "
        Qry += " left outer join TSPL_LOCATION_MASTER on tspl_location_master .Location_Code =t1.LOCATION_CODE left outer join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE =t1.DEVISION_CODE WHERE 2=2"

        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_LOCATION_MASTER.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and CONVERT(date,joining_date,103) < "
        Qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "') AND RELIEVING_DATE IS  NULL "
        Qry += " AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS )) AS TOTAL_EMP_LAST_MONTH"
        TOTAL_EMP_LAST_MONTH = clsDBFuncationality.GetDataTable(Qry).Rows(0).Item(0)
        Qry = ""
        Qry += "SELECT (SELECT COUNT(EMP_CODE) FROM TSPL_EMPLOYEE_MASTER T1  left outer join TSPL_LOCATION_MASTER on tspl_location_master .Location_Code =t1.LOCATION_CODE left outer join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE =t1.DEVISION_CODE "
        Qry += " WHERE 2=2 "
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_LOCATION_MASTER.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and CONVERT(date,joining_date,103) BETWEEN "
        Qry += "(SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "')"
        Qry += " AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "')"
        Qry += "AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS )) AS NEW_EMP"

        NEW_EMP = clsDBFuncationality.GetDataTable(Qry).Rows(0).Item(0)
        Qry = ""
        Qry += " SELECT (SELECT COUNT(EMP_CODE) FROM TSPL_EMPLOYEE_MASTER T1  left outer join TSPL_LOCATION_MASTER on tspl_location_master .Location_Code =t1.LOCATION_CODE left outer join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE =t1.DEVISION_CODE  "
        Qry += " WHERE 2=2 "
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_LOCATION_MASTER.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and CONVERT(date,RELIEVING_DATE,105) BETWEEN "
        Qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "')"
        Qry += " AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "') "
        Qry += " AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS )) AS LEFT_EMP"

        LEFT_EMP = clsDBFuncationality.GetDataTable(Qry).Rows(0).Item(0)

        NET_EMP = TOTAL_EMP_LAST_MONTH + NEW_EMP + NET_EMP - LEFT_EMP
        NoOfExcludeEmp = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT distinct count(EMP_CODE) as NoOfExcludeEmp FROM TSPL_EMPLOYEE_MASTER WHERE ISPF='0' and  CONVERT(date,joining_date,103) <  (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='JAN/2015') AND RELIEVING_DATE IS  NULL  AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS )"))
        RestEmp = NET_EMP - NoOfExcludeEmp
        'Dim txtYear As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT   DATENAME (Year ,CONVERT(date,GENERATE_DATE,103))as year FROM TSPL_GENERATE_SALARY  WHERE CONVERT(date,GENERATE_DATE,105) BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "') AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "') "))
        'Dim txtfromYear As String = txtYear
        'Dim txtToYear As String = txtYear + 1
        Qry = ""
        'Qry += " SELECT  '" & StatutoryRate & "' as Statutory,'" & NoOfExcludeEmp & "' as NoOfExcludeEmp,'" & RestEmp & "' as RestEmp ,MAX(GENERATE_DATE )as GENERATE_DATE,MAX(Comp_Name )as Comp_Name,max(add1)as add1,max(add2)as add2 ,max(city_code)as city_code ,MAX(City_Name )as City_Name ,'" & Me.fndFromPeriod.Value & "' as Pay_Period_Code,'" & TOTAL_EMP_LAST_MONTH & "' AS TOTAL_EMP_LAST_MONTH,'" & NEW_EMP & "' AS NEW_EMP,'" & LEFT_EMP & "' AS LEFT_EMP ,'" & NET_EMP & "' as NET_EMP,  T1.ACC_NO,SUM(T1.HEAD_VALUE) AS HEAD_VALUE,SUM(T1.ACTUAL_AMOUNT ) AS EMP_SHARE,SUM(T1.Employer_share ) AS CO_SHARE, SUM(T1.EMP_SHARE_REMITTED) AS EMP_SHARE_REMITTED,  SUM(T1.Employer_share ) AS CO_SHARE_REMITTED,SUM(T1.ADMIN_CHARGES) AS ADMIN_CHARGES,SUM(T1.Admin_charge_Remitted ) AS ADMIN_CHARGES_REMITTED FROM (select distinct GENERATE_DATE, ACC_NO,Comp_Name,Add1,Add2,City_Code,city_name,(ACTUAL_AMOUNT- pension_fund) as Employer_share,(ACTUAL_AMOUNT- pension_fund) as Employer_Remiited,EMP_CODE,HEAD_VALUE ,ACTUAL_AMOUNT ,Admin_Charges,Admin_Charges as Admin_charge_Remitted ,ACTUAL_AMOUNT as EMP_SHARE_REMITTED from (select   TSPL_GENERATE_SALARY_PAYHEADS. EMP_CODE ,Emp_Name ,Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.add2,TSPL_COMPANY_MASTER.City_Code  ,City_Name ,HEAD_VALUE ,ACTUAL_AMOUNT ,case when TSPL_GENERATE_SALARY_ATTENDANCE.EPS_TO_EPF=0 then ACTUAL_AMOUNT else (('" & PensionRate & "')*HEAD_VALUE /100) end as pension_fund ,'EPF A/c No. 01' AS ACC_NO , (('1.10')* HEAD_VALUE /100) as Admin_Charges ,convert(varchar,GENERATE_DATE,103) as GENERATE_DATE  from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Comp_code =TSPL_COMPANY_MASTER.Comp_Code  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code   "
        'Qry += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State   where SUB_HEAD_TYPE in ('EPF') and PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "')as tt"
        'Qry += " union all select distinct GENERATE_DATE,ACC_NO,Comp_Name,Add1 ,add2 ,City_Code,city_name, 0 as Employer_share,EMP_CODE  ,HEAD_VALUE ,pension_fund,Admin_Charges,Admin_Charges as Admin_Charges_Remitted ,0 as Employer_Remitted,pension_fund  as Pension_Find_remitted from (select TSPL_GENERATE_SALARY_PAYHEADS. EMP_CODE ,Emp_Name ,Comp_Name ,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.add2,TSPL_COMPANY_MASTER.City_Code ,City_Name ,CASE WHEN HEAD_VALUE>('" & EMPEPF_MAX & "') THEN ('" & EMPEPF_MAX & "') ELSE HEAD_VALUE END AS HEAD_VALUE ,ACTUAL_AMOUNT ,case when TSPL_GENERATE_SALARY_ATTENDANCE.EPS_TO_EPF=0 then ACTUAL_AMOUNT else (('8.33')*HEAD_VALUE /100) end as pension_fund ,'F.P.F. A/c No. 10' AS ACC_NO ,0 as Admin_Charges,convert(varchar,GENERATE_DATE,103) as GENERATE_DATE  from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Comp_code =TSPL_COMPANY_MASTER.Comp_Code  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code   "
        'Qry += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State where SUB_HEAD_TYPE in ('EPF') and PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "')as tt"
        'Qry += " union all select distinct GENERATE_DATE,ACC_NO,Comp_Name,Add1 ,add2 ,City_Code,city_name, EMP_CODE,HEAD_VALUE ,EMP_EDLI,ADMIN_CHARGES,EDLI ,ADMIN_CHARGES as ADMIN_CHARGES_remitted,EMP_EDLI as EMP_EDLI_Remitted,EDLI as EDLI_Remitted from (select TSPL_GENERATE_SALARY_PAYHEADS. EMP_CODE ,Emp_Name ,Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.add2,TSPL_COMPANY_MASTER.City_Code  ,City_Name,CASE WHEN HEAD_VALUE>('" & EMPEPF_MAX & "') THEN ('" & EMPEPF_MAX & "') ELSE HEAD_VALUE END AS HEAD_VALUE ,ACTUAL_AMOUNT ,0 AS EMP_EDLI,HEAD_VALUE*0.5/100 AS EDLI,'D.L.I A/c No. 21' AS ACC_NO ,(HEAD_VALUE * 0.01/100) AS ADMIN_CHARGES,convert(varchar,GENERATE_DATE,103) as GENERATE_DATE from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Comp_code =TSPL_COMPANY_MASTER.Comp_Code  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code   "
        'Qry += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  where SUB_HEAD_TYPE in ('EPF') and PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "')as g)AS T1 GROUP BY T1.ACC_NO "
        '=======================
        'Qry += " SELECT  '" & clsCommon.myCstr(StatutoryRate) & "' as Statutory,'" & clsCommon.myCstr(NoOfExcludeEmp) & "' as NoOfExcludeEmp,case when '" & clsCommon.myCstr(RestEmp) & "' < 0 then  '" & clsCommon.myCstr(RestEmp) & "' * -1  else '" & clsCommon.myCstr(RestEmp) & "' end as RestEmp,MAX(GENERATE_DATE )as GENERATE_DATE,MAX(Comp_Name )as Comp_Name,max(add1)as add1,max(add2)as add2 ,max(city_code)as city_code ,MAX(City_Name )as City_Name ,'" & Me.fndFromPeriod.Value & "' as Pay_Period_Code,'" & clsCommon.myCstr(TOTAL_EMP_LAST_MONTH) & "' AS TOTAL_EMP_LAST_MONTH,'" & clsCommon.myCstr(NEW_EMP) & "' AS NEW_EMP,'" & clsCommon.myCstr(LEFT_EMP) & "' AS LEFT_EMP ,'" & clsCommon.myCstr(NET_EMP) & "' as NET_EMP,  T1.ACC_NO,SUM(T1.HEAD_VALUE) AS HEAD_VALUE,SUM(T1.ACTUAL_AMOUNT ) AS EMP_SHARE,SUM(T1.Employer_share ) AS CO_SHARE, SUM(T1.EMP_SHARE_REMITTED) AS EMP_SHARE_REMITTED,  SUM(T1.Employer_share ) AS CO_SHARE_REMITTED,SUM(T1.ADMIN_CHARGES) AS ADMIN_CHARGES,SUM(T1.Admin_charge_Remitted ) AS ADMIN_CHARGES_REMITTED,max(Location) as Location,max(LocAddress) AS LocAddress  FROM (select distinct GENERATE_DATE, ACC_NO,Comp_Name,Add1,Add2,City_Code,city_name,(ACTUAL_AMOUNT- pension_fund) as Employer_share,(ACTUAL_AMOUNT- pension_fund) as Employer_Remiited,EMP_CODE,HEAD_VALUE ,ACTUAL_AMOUNT ,Admin_Charges,Admin_Charges as Admin_charge_Remitted ,ACTUAL_AMOUNT as EMP_SHARE_REMITTED,Location, LocAddress  from (select   TSPL_GENERATE_SALARY_PAYHEADS. EMP_CODE ,Emp_Name ,Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.add2,TSPL_COMPANY_MASTER.City_Code  ,City_Name ,HEAD_VALUE ,ACTUAL_AMOUNT , TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10  as pension_fund ,'EPF A/c No. 01' AS ACC_NO , (('1.10')* HEAD_VALUE /100) as Admin_Charges ,convert(varchar,GENERATE_DATE,103) as GENERATE_DATE,Location_desc as Location,'" & LocationAddress & "' AS [LocAddress]  from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on  TSPL_LOCATION_MASTER.lOCATION_code =TSPL_GENERATE_SALARY.Location_Code left join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_GENERATE_SALARY .DEVISION_CODE    LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State   where SUB_HEAD_TYPE in ('EPF') and PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "'"
        'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
        '    Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        'End If
        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        'End If

        'Qry += " )as tt"
        'Qry += " union all select distinct GENERATE_DATE,ACC_NO,Comp_Name,Add1 ,add2 ,City_Code,city_name, 0 as Employer_share,0 as Employer_Remitted,EMP_CODE  ,HEAD_VALUE ,pension_fund,Admin_Charges,Admin_Charges as Admin_Charges_Remitted ,pension_fund  as Pension_Find_remitted,Location,LocAddress from (select TSPL_GENERATE_SALARY_PAYHEADS. EMP_CODE ,Emp_Name ,Comp_Name ,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.add2,TSPL_COMPANY_MASTER.City_Code ,City_Name , HEAD_VALUE  AS HEAD_VALUE ,ACTUAL_AMOUNT , TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10  as pension_fund ,'F.P.F. A/c No. 10' AS ACC_NO ,0 as Admin_Charges,convert(varchar,GENERATE_DATE,103) as GENERATE_DATE,Location_Desc as Location,'" & LocationAddress & "' AS [LocAddress]    from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_Code left join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_GENERATE_SALARY .DEVISION_CODE    LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State where SUB_HEAD_TYPE in ('EPF') and PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "'"
        'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
        '    Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        'End If
        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        'End If

        'Qry += "  )as tt"
        'Qry += " union all select distinct GENERATE_DATE,ACC_NO,Comp_Name,Add1 ,add2 ,City_Code,city_name,EMP_EDLI,EDLI, EMP_CODE,HEAD_VALUE ,ADMIN_CHARGES ,ADMIN_CHARGES as ADMIN_CHARGES_remitted,EMP_EDLI as EMP_EDLI_Remitted,EDLI as EDLI_Remitted,Location,LocAddress from (select TSPL_GENERATE_SALARY_PAYHEADS. EMP_CODE ,Emp_Name ,Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.add2,TSPL_COMPANY_MASTER.City_Code  ,City_Name, HEAD_VALUE  AS HEAD_VALUE ,ACTUAL_AMOUNT ,0 AS EMP_EDLI,HEAD_VALUE*('" & clsCommon.myCstr(COEDLI_PER) & "')/100 AS EDLI,'D.L.I A/c No. 21' AS ACC_NO ,(HEAD_VALUE * ('" & clsCommon.myCstr(ACCOEDLI_PER) & "')/100) AS ADMIN_CHARGES,convert(varchar,GENERATE_DATE,103) as GENERATE_DATE,Location_Desc as Location,'" & LocationAddress & "'AS [LocAddress]  from TSPL_GENERATE_SALARY_PAYHEADS left join TSPL_EMPLOYEE_MASTER on  TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code   left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE left join TSPL_LOCATION_MASTER on  TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_Code left join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_GENERATE_SALARY .DEVISION_CODE   LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  where SUB_HEAD_TYPE in ('EPF') and PAY_PERIOD_CODE='" & Me.fndFromPeriod.Value & "'"
        'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
        '    Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        'End If
        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        'End If

        'Qry += " )as g)AS T1 GROUP BY T1.ACC_NO "
        Dim Prev_PP_Code As String
        Prev_PP_Code = clsPayPeriodMaster.GetPreviousPayPeriod(fndFromPeriod.Value, Nothing)
        Qry += " select *,'" & LocationAddress & "' AS [LocAddress],'" & StatutoryRate & "' as Statutory ,NoOfEmployee-RestEmployee as ExcludeEmp, 'F.P.F. A/c No. 10' as Ac10,'EPF A/c No. 01' as AC01,'D.L.I A/c No. 21' as Ac21 from"
        Qry += " ( select '' as Location_Code ,'' as Location_Desc,'KWALITY LIMITED' as Location_Address ,Comp_Name as Name,Add1 Address1,Add2 as Address2,Add3 as Address3,"
        Qry += " DATEPART(month,(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "'))Month,"
        Qry += " DATEPART(Year,(select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "'))Year, (select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "')DateFr, (select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "')DateTo,"
        Qry += "(select PAY_PERIOD_CODE  from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "')PayPeriod,"
        Qry += " (select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end) as 'A/c No.1'  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE "
        Qry += " where 2=2 and PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "'  and SUB_HEAD_TYPE='EPF'  and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " ) HeadValueACNo01,"
        Qry += " (select SUM(case when HEAD_VALUE>" & EMPEPF_MAX & " then " & EMPEPF_MAX & " else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where 2=2 and PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and  TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0 )HeadValueACNo10,"
        Qry += " (select SUM(case when HEAD_VALUE> " & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " * PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where 2=2"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and  TSPL_GENERATE_SALARY_PAYHEADS.CoEPF_AMT_AC01>0 and TSPL_GENERATE_SALARY.Pay_Period_Code= '" + fndFromPeriod.Value + "')HeadValueACNo21,"
        Qry += "(select SUM(Actual_Amount-CoEPS_AMT_AC10)   from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where 2=2 and PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 )EmployerShareofAcNo1 ,"
        Qry += " (select SUM(CoEPS_AMT_AC10) from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where 2=2"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0 )EmployerShareofAcNo10,"
        Qry += " ((select SUM(case when HEAD_VALUE> " & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " * PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where 2=2 and PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 )* 0.5)/100  EmployerShareofAcNo21,"
        Qry += " (select SUM(Actual_Amount)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where 2=2 and PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 ) EmployeeShareofAcNo1,"
        'Qry += " ((select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where 2=2"
        'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
        '    Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        'End If
        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        'End If
        'Qry += " and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 )* " & ACCOEDLI_PER & ")/100 AdminChargesAc01,"


        Qry += "  ((select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where 2=2 and PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += "  and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0   )* " & ACCOEPF_PER & ")/100 AdminChargesAc01,"


        Qry += " '" & AdminChargesAc21 & "' AdminChargesAc21,"
        Qry += "(select COUNT(*) from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_EMPLOYEE_STATUS EMPSTS ON GSA.EMP_STATUS_CODE=EMPSTS.EMP_STATUS_CODE where 2=2"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and PAY_PERIOD_CODE ='" + fndFromPeriod.Value + "' and SUB_HEAD_TYPE='EPF'  and  EMPSTS.IS_PF_APPL=1  )RestEmployee,"
        Qry += "  (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_EMPLOYEE_STATUS EMPSTS ON GSA.EMP_STATUS_CODE=EMPSTS.EMP_STATUS_CODE  where 2=2  "
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += "  and TSPL_GENERATE_SALARY.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += "  and TSPL_GENERATE_SALARY.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += "  and  SUB_HEAD_TYPE='EPF' and TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" + fndFromPeriod.Value + "')NoOfEmployee,"
        Qry += "  (SELECT COUNT(EMP_CODE) FROM TSPL_EMPLOYEE_MASTER T1  left outer join TSPL_LOCATION_MASTER on tspl_location_master .Location_Code =t1.LOCATION_CODE left outer join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE =t1.DEVISION_CODE  WHERE 2=2  "
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_LOCATION_MASTER.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += " and CONVERT(date,joining_date,103) BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + fndFromPeriod.Value + "') AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='APR/2015')AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS ))  NEW_EMP,"
        Qry += " (SELECT COUNT(EMP_CODE) FROM TSPL_EMPLOYEE_MASTER T1  left outer join TSPL_LOCATION_MASTER on tspl_location_master .Location_Code =t1.LOCATION_CODE left outer join TSPL_DEVISION_MASTER  on TSPL_DEVISION_MASTER.DEVISION_CODE =t1.DEVISION_CODE   WHERE 2=2 "
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_LOCATION_MASTER.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
        End If
        Qry += "   and CONVERT(date,RELIEVING_DATE,105) BETWEEN  (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + fndFromPeriod.Value + "') AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + fndFromPeriod.Value + "')  AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS ))  LEFT_EMP"
        Qry += "  from TSPL_COMPANY_MASTER) final"
      
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFForm12A(Revised)", "PF Form 12(A)")
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptPFForm12A_revised__Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
    End Sub

    Private Sub RptPFForm12A_revised__KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()

        End If
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        'Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') "
        Dim qry As String = clsSalaryGeneration.GetFinderQueryForSalaryLocationMulti(fndFromPeriod.Value)
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
    End Sub
End Class
