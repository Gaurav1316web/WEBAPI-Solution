'--21/04/2014--form Add By- Ashwani Raghav ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports XpertERPEngine

Public Class FrmSalarySummary
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#End Region
    Function allowtoCheck()

        If clsCommon.myLen(clsCommon.myCstr(txtFromPP.Value)) <= 0 Then
            myMessages.blankValue("Select Pay Period ")
            txtFromPP.Focus()
            txtFromPP.Select()
            Errorcontrol.SetError(txtFromPP, "Select Pay Period ")
            Return False
        Else
            Errorcontrol.ResetError(FndLocationCode)
        End If

        If clsCommon.myLen(clsCommon.myCstr(FndLocationCode.Value)) <= 0 Then
            myMessages.blankValue("Location ")
            FndLocationCode.Focus()
            FndLocationCode.Select()
            Errorcontrol.SetError(FndLocationCode, "Location ")
            Return False
        Else
            Errorcontrol.ResetError(FndLocationCode)
        End If

        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowtoCheck() Then
            PrintData()
        End If

    End Sub

    Private Sub FrmSalarySummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSalarySummaryRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        'Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
        '    & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        'txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        txtFromPP.Value = clsPayPeriodMaster.getFinder("", txtFromPP.Value, isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)

    End Sub

    Sub PrintData()
        '' changes done by Panch Raj in crystel report:Ticket No - BM00000008036 
        Try
            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Pay Period.", Me.Text)
                Return
            End If
            Dim DivCond As String = ""
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                DivCond = " and EMP.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
            End If
            'Dim DivisionAddress As String = ""
            'Dim DivisionFirstTime As Integer = 0
            'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count = 1 Then
            '    DivisionFirstTime += 1
            '    DivisionAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + add4 As [Address] FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ")"))
            'Else
            '    DivisionAddress = ""
            'End If
            'Dim LocCode As String = clsDBFuncationality.getSingleValue("select Location_Code   from TSPL_LOCATION_MASTER where Location_Code ='" + FndLocationCode.Value + "'")
            Dim LocName As String = clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code ='" + FndLocationCode.Value + "'")
            LocName += " " + Environment.NewLine
            Dim LocAdress As String = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(City_Code ,'')='' Then '' else '-'+CONVERT(varchar, City_Code)  End End End as Location_Address from TSPL_LOCATION_MASTER where Location_Code ='" + FndLocationCode.Value + "'")
            Dim DivisionAddress As String = ""
            Dim DivisionFirstTime As Integer = 0
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count = 1 Then
                DivisionFirstTime += 1
                LocName += clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select DEVISION_NAME  from  TSPL_DEVISION_MASTER   WHERE DEVISION_CODE in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ")"))
            End If


            Dim Qry As String = clsSalaryGeneration.GetPFESIQuery(txtFromPP.Value, "('" & FndLocationCode.Value & "' )", DivCond, LocName, LocAdress, "", FndLocationCode.Value)
            'Qry = "select '" & LocCode & "' as Location_Code ,'" & LocName & "' as Location_Desc,'" & LocAdress & "' as Location_Address ,Comp_Name as Name,Add1 Address1,Add2 as Address2,Add3 as Address3,"
            'Qry += " (select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + txtFromPP.Value + "')DateFr,"
            'Qry += " (select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + txtFromPP.Value + "')DateTo,"
            'Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")TotalEmpEPFAc01,"
            'Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0 " & DivCond & ")TotalEmpPensionAC10,"
            'Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")TotalEmpEDLIAc21,"
            'Qry += " (select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")TotalSalaryEPFAc01,"
            'Qry += " (select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0 " & DivCond & ")TotalSalaryPensionAc10,"
            'Qry += " (select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.CoEPF_AMT_AC01>0 " & DivCond & ")TotalSalaryEDLIAc21,"

            'Qry += " (select SUM(Actual_Amount)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")EPFAmtAc01,"
            'Qry += " (select SUM(CoEPS_AMT_AC10)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0 " & DivCond & ")PensionAmtAc10,"
            'Qry += " (select SUM(Actual_Amount-CoEPS_AMT_AC10)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")DifferenceAmtAc01,"

            'Qry += " ((select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")* " & objPF.ACCOEPF_PER & ")/100 AdminAmtAc02,"
            'Qry += " ((select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")* " & objPF.COEDLI_PER & ")/100 EDLIAmtAc21,"
            'Qry += " (((select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")* " & objPF.COEDLI_PER & ")/100)*" & objPF.ACCOEDLI_PER & "/100 AdminEDLIAmtAc22,"

            'Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "'  and SUB_HEAD_TYPE in('EMPESI','EMPESI') and TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")TotEmpESI,"
            'Qry += " (select SUM(HEAD_VALUE)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "'  and SUB_HEAD_TYPE in('EMPESI','COESI') and TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")TotSalESI,"
            'Qry += " (select SUM(ACTUAL_AMOUNT)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "'  and SUB_HEAD_TYPE='EMPESI' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")EmpESIAmt,"
            'Qry += " (select SUM(Co_ESI_AMT)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='EMPESI' and  TSPL_GENERATE_SALARY_PAYHEADS.Co_ESI_AMT>0 " & DivCond & ")EmployerESIAMT, "

            'Qry += " (select SUM(Actual_Amount)*2  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE='" + FndLocationCode.Value + "' and SUB_HEAD_TYPE='LWF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 " & DivCond & ")LWFER"
            'Qry += " from TSPL_COMPANY_MASTER where Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
            Dim DTC As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If DTC.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

            Qry = "Select t1.PAY_HEAD_CODE,t1.PAY_HEAD_NAME,GROUP_SEQ,"
            Qry += " SUM( case when t1.ISEARNING=1 then t3.ACTUAL_AMOUNT else 0 end) Earning,"
            Qry += " SUM( case when t1.ISEARNING=0 then t3.ACTUAL_AMOUNT else 0 end) Deduction,"
            Qry += " TSPL_LOCATION_MASTER.Location_Code,max(TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(City_Code ,'')='' Then '' else '-'+CONVERT(varchar, City_Code) End End End )as Location_Address,max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc"
            Qry += " from TSPL_PAYHEAD_MASTER t1"
            Qry += " left outer join TSPL_GENERATE_SALARY t2 on  t2.PAY_PERIOD_CODE ='" + txtFromPP.Value + "'"
            Qry += " left outer join TSPL_GENERATE_SALARY_PAYHEADS t3 on t3.SALARY_GENERATION_CODE=t2.SALARY_GENERATION_CODE and "
            Qry += " t3.PAY_HEAD_CODE = t1.PAY_HEAD_CODE"
            Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =t2.LOCATION_CODE "
            Qry += " left outer join TSPL_EMPLOYEE_MASTER EMP on EMP.EMP_CODE =T3.EMP_CODE "

            If clsCommon.myLen(FndLocationCode.Value) > 0 Then
                Qry += " where TSPL_LOCATION_MASTER .Location_Code='" + FndLocationCode.Value + "' "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                Qry += " and EMP.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
            End If

            Qry += " group by t1.PAY_HEAD_CODE,t1.PAY_HEAD_NAME,TSPL_LOCATION_MASTER.Location_Code,ISEARNING,GROUP_SEQ having SUM(t3.ACTUAL_AMOUNT)>0 ORDER BY ISEARNING DESC,GROUP_SEQ"
            Dim DTP As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim dt As New DataTable
            dt.Columns.Add("Name", GetType(String))
            dt.Columns.Add("Address1", GetType(String))
            dt.Columns.Add("Address2", GetType(String))
            dt.Columns.Add("Address3", GetType(String))
            dt.Columns.Add("Month", GetType(String))
            dt.Columns.Add("Year", GetType(String))
            dt.Columns.Add("EPayHead", GetType(String))
            dt.Columns.Add("Earning", GetType(Decimal))
            dt.Columns.Add("DPayHead", GetType(String))
            dt.Columns.Add("Deduction", GetType(Decimal))
            dt.Columns.Add("TotalEmpEPFAc01", GetType(Integer))
            dt.Columns.Add("TotalEmpPensionAC10", GetType(Integer))
            dt.Columns.Add("TotalEmpEDLIAc21", GetType(Integer))
            dt.Columns.Add("TotalSalaryEPFAc01", GetType(Decimal))
            dt.Columns.Add("TotalSalaryPensionAc10", GetType(Decimal))
            dt.Columns.Add("TotalSalaryEDLIAc21", GetType(Decimal))
            dt.Columns.Add("EPFAmtAc01", GetType(Decimal))
            dt.Columns.Add("PensionAmtAc10", GetType(Decimal))
            dt.Columns.Add("DifferenceAmtAc01", GetType(Decimal))
            dt.Columns.Add("AdminAmtAc02", GetType(Decimal))

            dt.Columns.Add("EDLIAmtAc21", GetType(Decimal))
            dt.Columns.Add("AdminEDLIAmtAc22", GetType(Decimal))
            dt.Columns.Add("TotEmpESI", GetType(Integer))
            dt.Columns.Add("TotSalESI", GetType(Decimal))

            dt.Columns.Add("EmpESIAmt", GetType(Decimal))
            dt.Columns.Add("EmployerESIAMT", GetType(Decimal))
            dt.Columns.Add("LWFER", GetType(Decimal))

            dt.Columns.Add("Location_Code", GetType(String))
            dt.Columns.Add("Location_Desc", GetType(String))
            dt.Columns.Add("Location_Address", GetType(String))
            dt.Columns.Add("Total_Employee_Count", GetType(String))

            Dim drPayHeadEarning() As DataRow = DTP.Select("Earning>0", "GROUP_SEQ asc")
            Dim drPayHeadDeduction() As DataRow = DTP.Select("Deduction>0", "GROUP_SEQ asc")
            Dim NOF As Integer = 0
            If drPayHeadEarning.Length >= drPayHeadDeduction.Length Then
                NOF = drPayHeadEarning.Length
            Else
                NOF = drPayHeadDeduction.Length
            End If
            For i As Integer = 0 To NOF - 1
                dt.Rows.Add(dt.NewRow)
                dt.Rows(i)("Name") = DTC.Rows(0)("Name")
                dt.Rows(i)("Address1") = DTC.Rows(0)("Address1")
                dt.Rows(i)("Address2") = DTC.Rows(0)("Address2")
                dt.Rows(i)("Address3") = DTC.Rows(0)("Address3")
                dt.Rows(i)("Month") = MonthName(CDate(DTC.Rows(0)("DateFr").ToString()))
                dt.Rows(i)("Year") = CDate(DTC.Rows(0)("DateFr").ToString()).Year
                dt.Rows(i)("TotalEmpEPFAc01") = Val(DTC.Rows(0)("TotalEmpEPFAc01").ToString())
                dt.Rows(i)("TotalEmpPensionAC10") = Val(DTC.Rows(0)("TotalEmpPensionAC10").ToString())
                dt.Rows(i)("TotalEmpEDLIAc21") = Val(DTC.Rows(0)("TotalEmpEDLIAc21").ToString())
                dt.Rows(i)("TotalSalaryEPFAc01") = Val(DTC.Rows(0)("TotalSalaryEPFAc01").ToString())
                dt.Rows(i)("TotalSalaryPensionAc10") = Val(DTC.Rows(0)("TotalSalaryPensionAc10").ToString())
                dt.Rows(i)("TotalSalaryEDLIAc21") = Val(DTC.Rows(0)("TotalSalaryEDLIAc21").ToString())
                dt.Rows(i)("EPFAmtAc01") = Val(DTC.Rows(0)("EPFAmtAc01").ToString())
                dt.Rows(i)("PensionAmtAc10") = Val(DTC.Rows(0)("PensionAmtAc10").ToString())
                dt.Rows(i)("DifferenceAmtAc01") = Val(DTC.Rows(0)("DifferenceAmtAc01").ToString())
                dt.Rows(i)("AdminAmtAc02") = Val(DTC.Rows(0)("AdminAmtAc02").ToString())
                dt.Rows(i)("EDLIAmtAc21") = Val(DTC.Rows(0)("EDLIAmtAc21").ToString())
                dt.Rows(i)("AdminEDLIAmtAc22") = Val(DTC.Rows(0)("AdminEDLIAmtAc22").ToString())

                dt.Rows(i)("TotEmpESI") = Val(DTC.Rows(0)("TotEmpESI").ToString())
                dt.Rows(i)("TotSalESI") = Val(DTC.Rows(0)("TotSalESI").ToString())
                dt.Rows(i)("EmpESIAmt") = Val(DTC.Rows(0)("EmpESIAmt").ToString())
                dt.Rows(i)("EmployerESIAMT") = Val(DTC.Rows(0)("EmployerESIAMT").ToString())
                dt.Rows(i)("LWFER") = Val(DTC.Rows(0)("LWFER").ToString())

                dt.Rows(i)("Location_Code") = DTC.Rows(0)("Location_Code")
                dt.Rows(i)("Location_Desc") = DTC.Rows(0)("Location_Desc")
                dt.Rows(i)("Location_Address") = DTC.Rows(0)("Location_Address")
                dt.Rows(i)("Total_Employee_Count") = DTC.Rows(0)("Total_Employee_Count")
                If drPayHeadEarning.Length > i Then
                    dt.Rows(i)("EPayHead") = drPayHeadEarning(i)!PAY_HEAD_CODE
                    dt.Rows(i)("Earning") = drPayHeadEarning(i)!Earning
                Else
                    dt.Rows(i)("EPayHead") = ""
                    'dt.Rows(i)("Earning") = 0
                End If
                If drPayHeadDeduction.Length > i Then
                    dt.Rows(i)("DPayHead") = drPayHeadDeduction(i)!PAY_HEAD_CODE
                    dt.Rows(i)("Deduction") = drPayHeadDeduction(i)!Deduction
                Else
                    dt.Rows(i)("DPayHead") = ""
                    'dt.Rows(i)("Deduction") = 0
                End If
            Next

            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "crptSalarySummary", "Salary Summary ")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function MonthName(ByVal dateTime As DateTime) As String
        Return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month)
    End Function

    Private Sub txtFromPP_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromPP.Validating

    End Sub

    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
        FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub txtDivisionMult_My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
End Class
