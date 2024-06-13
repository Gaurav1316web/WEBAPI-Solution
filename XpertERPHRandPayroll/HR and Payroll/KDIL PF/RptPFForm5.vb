'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptPFForm5
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPFForm5)
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
    Sub LoadReport()
        Dim CompESIC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_PF_NO from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
        Dim Comp_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
      
        Dim Qry As String
        Qry = "select distinct Emp_Name,ISNULL(TSPL_EMPLOYEE_MASTER.PF_No,'') AS PF_No ,FATHERS_NAME ,Birth_date,SEX ,TSPL_COMPANY_MASTER.Comp_Name, convert(varchar,Joining_Date,103)as  Joining_Date  ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.City_Code ,'" & fndFromPeriod.Value & "'  AS FROM_PAY_PERIOD,"
        'If clsCommon.myLen(FndLocationCode.Value) > 0 Then
        '    Qry += " TSPL_LOCATION_MASTER.Location_Desc  "
        'Else
        '    Qry += "'" + Comp_Name + "'"
        'End If
        Qry += " ISNULL(TSPL_LOCATION_MASTER.Location_Desc,'') "
        Qry += " as Location_Desc,"
        'If clsCommon.myLen(FndLocationCode.Value) > 0 Then
        Qry += "TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.City_Code) End End End  "
        'Else
        '    Qry += "''"
        'End If
        Qry += " as Address,TSPL_LOCATION_MASTER.PF_No as Comp_PF_NO from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE   INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE    INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code Left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.Location_Code   WHERE CONVERT(date,joining_date,103) BETWEEN  (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & fndFromPeriod.Value & "') and TSPL_GENERATE_SALARY_PAYHEADS.PF_Applicable = '1'"
        'If clsCommon.myLen(FndLocationCode.Value) > 0 Then
        '    Qry += " and TSPL_GENERATE_SALARY.Location_Code='" & FndLocationCode.Value & "' "
        'End If
        If txtLocCode.arrValueMember IsNot Nothing AndAlso txtLocCode.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code in (" & clsCommon.GetMulcallString(txtLocCode.arrValueMember) & " )"
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_EMPLOYEE_MASTER.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
        End If
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFForm5", "PF Form5")
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadReport()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptPFForm5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
    End Sub

    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
        FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub txtLocCode__My_Click(sender As Object, e As EventArgs) Handles txtLocCode._My_Click
        'Dim qry As String = " Select Location_Code As Code,location_Desc As Name ,Add1 + ' ' + Add2 + Add3 + ' ' + Add4 As [Address],City_Code As [City Code],State As [State],Pin_Code As [Pin Code]," &
        '                    " Country ,Telphone,Email,Location_Type AS [Location Type],Loc_Status AS [Loc Status] ,Loc_Segment_Code As [Loc Segment Code],Type As [Type] From TSPL_LOCATION_MASTER "
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ") "

        txtLocCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulESCI", qry, "Code", "Name", txtLocCode.arrValueMember, txtLocCode.arrDispalyMember)
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMSel5", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
End Class
