'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptForm34
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptForm34)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadData()
        Try
            Dim Qry As String = ""
            Dim From_Date As Date
            Dim To_Date As Date
            From_Date = "01/01/" & (txtFromYear.Text)
            To_Date = "31/12/" & (txtFromYear.Text)
            Qry = "select '" & txtFromYear.Text & "' as Year,(April+May+ June+July+August+September+October+November+December+ January+ February+March)as Working_Days,case when Male='0' then convert(decimal(18,2),(SumMale)/1)else convert(decimal(18,2),(SumMale)/(Male)) end as AvgMale , case when Female='0'  then convert(decimal(18,2),(SumFemale)/ 1) else convert(decimal(18,2),(SumFemale)/ (Female)) end as AvgFemale, case when ChildMale='0'  then convert(decimal(18,2),(SumChildMale)/ 1) else convert(decimal(18,2),(SumChildMale)/ (ChildMale)) end as AvgChildrenMale,case when ChildMale='0' and ChildFemale='0' then (SumChildFemale)/ 1 else (SumChildFemale)/ case when (ChildFemale)=0 then 1 else ChildFemale end end as AvgChildrenFemale,Male,Female ,(ChildMale+ChildFemale)as Children,Comp_Name,Address from (select isnull(April,0)as April,isnull(May,0)as May, isnull(June,0)as June,isnull(July,0)as July,isnull(August,0)as August,isnull(September,0)as September,isnull(October,0)as October,isnull(November,0)as November,isnull(December,0)as December,isnull( January,0)as January,isnull( February,0)as February,isnull(March,0)as March,SumMale,SumFemale,SumChildMale,SumChildFemale,Comp_Name,Address,Male,Female,ChildMale,ChildFemale  from(select max(Present_Days)as Present_Days,DATENAME (MONTH ,CONVERT(date,max(DATE_FROM),103))as DateMonth,"
            Qry += " coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumMale,"
            Qry += " coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='FEMALE'  then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumFemale ,"
            Qry += " coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumChildMale,"
            Qry += " 	coalesce(sum(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18  and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as SumChildFemale,"
            Qry += " max(Comp_Name)as Comp_Name,max(TSPL_COMPANY_MASTER.Comp_Name+TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3+City_Code)as Address,"
            Qry += " coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18  and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as Male,"
            Qry += " coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) >= 18 and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end),0 )as Female ,"
            Qry += " coalesce(count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='MALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end),0 )as ChildMale,"

            Qry += " coalesce (count(case when DATEDIFF(Year, convert(date,Birth_Date,103), getdate()) < 18 and SEX='FEMALE' then DATEDIFF(Year, convert(date,Birth_Date,103), getdate())end ),0)as ChildFemale"

            Qry += " from TSPL_EMPLOYEE_MASTER left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_Employee_Master.DEVISION_CODE left join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.Emp_Code=TSPL_EMPLOYEE_MASTER.Emp_Code left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_EMPLOYEE_MASTER.Comp_Code left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE left join TSPL_OT_SHEET on TSPL_OT_SHEET.PAY_PERIOD_CODE= TSPL_GENERATE_SALARY.PAY_PERIOD_CODE where convert(date,DATE_FROM,103)>=convert(date,'" & From_Date.ToString & "',103) and convert(date,DATE_TO,103) <=convert(date,'" & To_Date.ToString & "',103) "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Qry += " and TSPL_Employee_Master.LOcation_Code  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtMultDivision.arrValueMember IsNot Nothing AndAlso txtMultDivision.arrValueMember.Count > 0 Then
                Qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtMultDivision.arrValueMember) + ") "
            End If
            Qry += " )as t Pivot(max(  Present_Days) FOR [DateMonth] IN (April,May, June,July,August,September,October,November,December, January, February,March) )AS pivott) as t"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(Qry)
            If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptForm34", "Form 34 Report")
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.close()
    End Sub

    Private Sub RptForm34_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+P Print ")
            txtFromYear.Text = clsCommon.GETSERVERDATE
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RptForm34_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag Then
                LoadData()
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
            Dim frmpending As New FrmPendingRequisitionQty()
            frmpending.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultDivision__My_Click(sender As Object, e As EventArgs) Handles txtMultDivision._My_Click
        Try
            Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
            txtMultDivision.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtMultDivision.arrValueMember, txtMultDivision.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class