Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
'=============Created By Preeti Gupta==========
Public Class RptFromNo21

  
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptForm34)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub RptFromNo21_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+P Print ")
    End Sub
    Sub LoadData()
        Dim Qry As String = ""
        Dim From_Date As Date
        Dim To_Date As Date
        Dim Weekenddays As Integer
        Dim count As Integer
        Dim i As Integer
        count = 0
        From_Date = "01/01/" & (txtFromYear.Text)
        To_Date = "31/12/" & (txtFromYear.Text)
        Dim TotalDays As Integer = clsDBFuncationality.getSingleValue("select (Case When  DAY(EOMONTH(DATEFROMPARTS('" & txtFromYear.Text & "',2,1)))=29 Then 366 else 365 end) ")

        For i = 0 To TotalDays
            Dim weekday As DayOfWeek = From_Date.AddDays(i).DayOfWeek
            If weekday = DayOfWeek.Sunday Then
                count += 1
            End If
        Next
        Weekenddays = count



        Dim Holiday As Integer = clsDBFuncationality.getSingleValue("select distinct count(*) from TSPL_GENERAL_HOLIDAYS ")

        TotalDays = TotalDays - Holiday - Weekenddays

        Dim ageAdult As String = clsDBFuncationality.getSingleValue("select count(case when dateDiff(Year,convert(date,birth_date,103),convert(date,Getdate(),103))>18 then 'A' end) Adult from TSPL_EMPLOYEE_MASTER where  TSPL_EMPLOYEE_MASTER. sex='Male'")
        Dim AgeChildren As String = clsDBFuncationality.getSingleValue("select count(case when dateDiff(Year,convert(date,birth_date,103),convert(date,Getdate(),103))<18 then 'A' end) Children from TSPL_EMPLOYEE_MASTER where  TSPL_EMPLOYEE_MASTER. sex='Male' ")

        Dim ageAvgAdult As String = clsDBFuncationality.getSingleValue("select count(case when dateDiff(Year,convert(date,birth_date,103),convert(date,Getdate(),103))>18 then 'A' end) Adult from TSPL_EMPLOYEE_MASTER where  TSPL_EMPLOYEE_MASTER. sex='Male'")
        Dim AgeAvgChildren As String = clsDBFuncationality.getSingleValue("select count(case when dateDiff(Year,convert(date,birth_date,103),convert(date,Getdate(),103))<18 then 'A' end) Children from TSPL_EMPLOYEE_MASTER where  TSPL_EMPLOYEE_MASTER. sex='Male' ")

        Dim CompCode As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Code from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")

        Dim CompName As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Name from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")
        Dim CompanyAdress As String = clsDBFuncationality.getSingleValue(" select  TSPL_COMPANY_MASTER.Add1+Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2+ Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add3+ Case When ISNULL(TSPL_COMPANY_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code) End End End as Comp_Address from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")
        Qry = " select distinct '" + txtFromYear.Text + "' as Year,'" + CompCode + "' as Comp_code,'" + CompanyName + "' as Comp_Name,'" + CompanyName + "' as Comp_Name,TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+  Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.City_Code) End End End as Loc_Add,TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT as ACTUAL_AMOUNT,TSPL_EMPLOYEE_MASTER.EMP_CODE as EMP_CODE,'" + ageAdult + "' as AgeADult,'" + AgeChildren + "' as AgeChildren"
        Qry += " from TSPL_GENERATE_SALARY_PAYHEADS"
        Qry += " left outer join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE  "
        Qry += " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE "
        Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE "
        Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.LOCATION_CODE  "
        Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER  .Comp_Code =TSPL_LOCATION_MASTER.Comp_code "
        Qry += " left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER .Designation_id =TSPL_EMPLOYEE_MASTER.Designation"
        Qry += " left outer join TSPL_DEVISION_MASTER   on TSPL_EMPLOYEE_MASTER .DEVISION_CODE  =TSPL_DEVISION_MASTER.DEVISION_CODE "
        Qry += " Where and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103)>=convert(date,'" + From_Date + "',103) and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103) <=convert(date,'" + To_Date + "' ,103) "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "CrptPayrollForm4", "Form 34 Report")
    End Sub
    Private Sub RptFromNo21_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub btnGo_Click_1(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
        FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
    End Sub
End Class
