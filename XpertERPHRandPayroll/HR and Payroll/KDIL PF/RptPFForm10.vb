'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptPFForm10
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPFForm10)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub RptPFForm10_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    Sub LoadReport()
       
        Dim Qry As String
        Qry = " select distinct Emp_Name,BANK_ACC_NO ,FATHERS_NAME ,Birth_date,SEX ,TSPL_COMPANY_MASTER.Comp_Name  ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.City_Code ,'" & fndFromPeriod.Value & "' AS FROM_PAY_PERIOD ,TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End  as Loc_Address,convert(varchar,Joining_date,103)as  Joining_date,convert(varchar,RELIEVING_DATE,103)as RELIEVING_DATE ,Leaving_REASON,TSPL_GENERATE_SALARY.Location_Code   from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE    INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.LOCATION_CODE  left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State WHERE CONVERT(date,RELIEVING_DATE,105) BETWEEN  (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & fndFromPeriod.Value & "' and TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='EPF' and ISPF ='1'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_EMPLOYEE_MASTER.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
        End If
        Qry += "  )"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crpForm10PF", "PF Form10")
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadReport()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
    '    fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
    '    If clsCommon.myLen(fndLocationCode.Value) > 0 Then
    '        lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
    '    Else
    '        lblLocationName.Text = ""
    '    End If
    'End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMSel10", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
End Class
