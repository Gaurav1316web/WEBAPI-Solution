'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptForm22
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptForm22)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadData()
        Dim From_Date As Date
        Dim To_Date As Date
        Dim FromMonth As String
        Dim ToMonth As String
        Dim From_Period As String
        Dim To_Period As String

        If cbMonth.Text = "JAN-JUNE" Then
            From_Date = "01/01/" & (txtFromYear.Text)
            To_Date = "30/06/" & (txtFromYear.Text)
            From_Period = "01/" & (txtFromYear.Text)
            To_Period = "06/" & (txtFromYear.Text)

            FromMonth = "January"
            ToMonth = "30th June,"
            Dim sQuery As String = "	select Loc_Address,(April+May+ June+ January+ February + March) as Working_Days,FromDate,ToDate,Year,Month,AvgMale,AvgFemale, Comp_Name,Address,Male,Female,ChildMale,ChildFemale from (select Loc_Address,'" & From_Period.ToString & "' as FromDate,'" & To_Period.ToString & "' as ToDate,'" & txtFromYear.Text & "' as Year,'" & ToMonth & "' as Month,case when Male='0' and ChildMale='0'then convert(decimal(18,2),(SumMale+SumChildMale)/1)else convert(decimal(18,2),(SumMale+SumChildMale)/(Male+ChildMale)) end as AvgMale, case when Female='0' and ChildFemale='0' then convert(decimal(18,2),(SumFemale+SumChildFemale)/ 1) else convert(decimal(18,2),(SumFemale+SumChildFemale)/ (Female+ChildFemale)) end as AvgFemale,Comp_Name,Address,Male,Female,ChildMale,ChildFemale "
            sQuery += " ,isnull(April,0)as April,isnull(May,0)as May, isnull(June,0)as June,isnull( January,0)as January,isnull( February,0)as February,isnull(March,0)as March from (select * from  (select  max(TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End) as Loc_Address,coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumMale,	coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='FEMALE' "
            sQuery += " then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumFemale ,coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumChildMale,coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 "
            sQuery += " and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumChildFemale,max(Comp_Name)as Comp_Name,max(Present_Days)as Present_Days ,max(TSPL_COMPANY_MASTER.Comp_Name+TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3+TSPL_COMPANY_MASTER.City_Code)as Address,coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 "
            sQuery += " and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as Male,coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end),0 )as Female ,coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end),0 )as ChildMale, "
            sQuery += " coalesce (count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as ChildFemale,DATENAME (MONTH ,CONVERT(date,max(DATE_FROM),103))as DateMonth    from TSPL_EMPLOYEE_MASTER left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_Employee_Master.DEVISION_CODE left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_EMPLOYEE_MASTER.Emp_Code left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE "
            sQuery += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_EMPLOYEE_MASTER.Comp_Code   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code  left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State where convert(date,DATE_FROM,103)>=convert(date,'" & From_Date.ToString & "',103) and convert(date,DATE_TO,103) <=convert(date,'" & To_Date.ToString & "',103) "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_Employee_Master.LOcation_Code  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtMultDivision.arrValueMember IsNot Nothing AndAlso txtMultDivision.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtMultDivision.arrValueMember) + ") "
            End If

            sQuery += " )as tt Pivot(max(  Present_Days) FOR [DateMonth] IN (April,May, June, January, February,March) )AS pivott)as p)as b"

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptForm22", "Form 22 Report")
        ElseIf cbMonth.Text = "JULY-DECEMBER" Then
            From_Date = "01/07/" & (txtFromYear.Text)
            To_Date = "31/12/" & (txtFromYear.Text)
            From_Period = "07/" & (txtFromYear.Text)
            To_Period = "12/" & (txtFromYear.Text)
            FromMonth = "July"
            ToMonth = "31th December,"
            Dim sQuery As String = "select Loc_Address,(July+August+September+October+November+December) as Working_Days,FromDate,ToDate,Year,Month,AvgMale,AvgFemale, Comp_Name,Address,Male,Female,ChildMale,ChildFemale from (select Loc_Address,'" & From_Period.ToString & "' as FromDate,'" & To_Period.ToString & "' as ToDate,'" & txtFromYear.Text & "' as Year,'" & ToMonth & "' as Month,case when Male='0' and ChildMale='0'then convert(decimal(18,2),(SumMale+SumChildMale)/1)else convert(decimal(18,2),(SumMale+SumChildMale)/(Male+ChildMale)) end as AvgMale, case when Female='0' and ChildFemale='0' then convert(decimal(18,2),(SumFemale+SumChildFemale)/ 1) else convert(decimal(18,2),(SumFemale+SumChildFemale)/ (Female+ChildFemale)) end as AvgFemale,Comp_Name,Address,Male,Female,ChildMale,ChildFemale,isnull(July,0)as July,isnull(August,0)as August,isnull(September,0)as September,isnull(October,0)as October,isnull(November,0)as November,isnull(December,0)as December "
            sQuery += " from (select * from (select max(TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End ) as Loc_Address, coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumMale,	coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='FEMALE' "
            sQuery += " then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumFemale ,coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumChildMale,coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 "
            sQuery += " and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumChildFemale,max(Comp_Name)as Comp_Name,max(Present_Days)as Present_Days ,max(TSPL_COMPANY_MASTER.Comp_Name+TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3+TSPL_COMPANY_MASTER.City_Code)as Address,coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 "
            sQuery += " and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as Male,coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end),0 )as Female ,coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end),0 )as ChildMale, "
            sQuery += " coalesce (count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as ChildFemale ,DATENAME (MONTH ,CONVERT(date,max(DATE_FROM),103))as DateMonth   from TSPL_EMPLOYEE_MASTER left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_Employee_Master.DEVISION_CODE left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_EMPLOYEE_MASTER.Emp_Code left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE "
            sQuery += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_EMPLOYEE_MASTER.Comp_Code   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.Location_Code  left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State where convert(date,DATE_FROM,103)>=convert(date,'" & From_Date.ToString & "',103) and convert(date,DATE_TO,103) <=convert(date,'" & To_Date.ToString & "',103) "


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_Employee_Master.LOcation_Code  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtMultDivision.arrValueMember IsNot Nothing AndAlso txtMultDivision.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtMultDivision.arrValueMember) + ") "
            End If
            sQuery += " )as tt Pivot(max(  Present_Days) FOR [DateMonth] IN (July,August,September,October,November,December) )AS pivott)as p)as b"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptForm22", "Form 22 Report")
        End If
        

    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptForm22_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag Then
            LoadData()

        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub RptForm22_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+P Print ")
        txtFromYear.Text = clsCommon.GETSERVERDATE

    End Sub

    Private Sub txtMultDivision__My_Click(sender As Object, e As EventArgs) Handles txtMultDivision._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtMultDivision.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtMultDivision.arrValueMember, txtMultDivision.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub
End Class