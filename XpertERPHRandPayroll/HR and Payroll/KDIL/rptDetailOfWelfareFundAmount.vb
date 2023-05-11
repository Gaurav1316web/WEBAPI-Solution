
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'===============================CREATED BY PREETI GUPTA Ticket No.[6607]====================
Public Class RptDetailOfWelfareFundAmount
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim Print As Boolean = True
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptDetailOfWelfareFundAmount)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnPrint.Visible = MyBase.isModifyFlag
    End Sub
#End Region
    Public Sub funPrint()

        'Dim FromDate As String = clsCommon.myCDate(txtfromDate.Value, "dd/MMM/YYYY")
        'Dim ToDate As String = clsCommon.myCDate(txtTodate.Value, "dd/MMM/yyyy")
        Dim FromDate As String = txtfromDate.Value
        Dim ToDate As String = txtTodate.Value
        Dim CompCode As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Code from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")

        Dim CompName As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Name from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")
        Dim CompanyAdress As String = clsDBFuncationality.getSingleValue(" select  TSPL_COMPANY_MASTER.Add1+Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2+ Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add3+ Case When ISNULL(TSPL_COMPANY_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code) End End End as Comp_Address from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")

        Try
            'Dim qry As String = " select substring(datename(MONTH,convert(date,case when convert(date,Joining_date,103)<=convert(date,'" + txtfromDate.Value + "',103) then '" + txtfromDate.Value + "' else  Joining_date end,103)),1,3) + ', ' + datename(YEAR,convert(date,case when convert(date,Joining_date,103)<=convert(date,'" + txtfromDate.Value + "',103) then '" + txtfromDate.Value + "' else  Joining_date end,103)) "
            'qry += " + ' to ' + substring(datename(month,cast(TSPL_PAYPERIOD_MASTER.DATE_FROM as date)),1,3) + ', ' + datename(YEAR,cast(TSPL_PAYPERIOD_MASTER.DATE_FROM as date)) as Period, '" + CompCode + "' as Comp_Code,'" + CompName + "' as Comp_Name,'" + CompanyAdress + "' as Comp_Address,'" + clsCommon.myCDate(FromDate, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(ToDate, "dd/MMM/yyyy") + "' as ToDate,"

            'qry += " TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.City_Code) End End End as Loc_Address,TSPL_EMPLOYEE_MASTER.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name ,TSPL_EMPLOYEE_MASTER.FATHERS_NAME ,TSPL_EMPLOYEE_MASTER.Designation ,TSPL_EMPLOYEE_MASTER.Joining_date ,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE ,isnull(TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT,0) as ACTUAL_AMOUNT,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE ,datepart(dd,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Datewise,datename(month,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Monthwise ,datepart(yy,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Yearwise from TSPL_GENERATE_SALARY_ATTENDANCE"
            'qry += " inner JOIN  TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE "
            'qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE"
            'qry += " left join (select SALARY_GENERATION_CODE,EMP_CODE,SUB_HEAD_TYPE,PAY_HEAD_CODE,ACTUAL_AMOUNT from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='LWF') TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE = TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE "
            'qry += " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE "
            'qry += "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.LOCATION_CODE "
            'qry += " where convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103)>=convert(date,('" + txtfromDate.Value + "'),103) and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103) <=convert(date,('" + txtTodate.Value + "'),103) "
            'qry += "   and ACTUAL_AMOUNT >0"

            'qry += " and TSPL_LOCATION_MASTER.LOCATION_CODE  in (" + clsCommon.myCstr(fndLocationCode.Value) + ") "

            'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_EMPLOYEE_MASTER.Devision_Code  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
            'End If

            'If txtEmployeeMult.arrValueMember IsNot Nothing AndAlso txtEmployeeMult.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE  in (" + clsCommon.GetMulcallString(txtEmployeeMult.arrValueMember) + ") "
            'End If
            Dim qry As String = "SELECT (datename(MONTH,convert(date, convert(date,MM.DATE_MIN,103)))) + ',' +(datename(YEAR,convert(date, convert(date,MM.DATE_MIN,103)))) + ' TO ' + (datename(MONTH,convert(date, convert(date,MM.DATE_MAX,103)))) + ',' +(datename(YEAR,convert(date, convert(date,MM.DATE_MAX,103)))) AS PERIOD,Comp_Code,Comp_Name,Comp_Address,FromDate,ToDate,Location_Code,Location_Desc,Loc_Address,EMP_CODE,Emp_Name,FATHERS_NAME,Designation,Joining_date,RELIEVING_DATE,ACTUAL_AMOUNT FROM "
            qry += " ( select MAX(DATE_FROM)AS DATE_MAX,MIN(DATE_FROM ) AS DATE_MIN,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name,max(Comp_Address) as Comp_Address,max(FromDate) as FromDate,max(ToDate) as ToDate,Location_Code,max(Location_Desc) as Location_Desc,max(Loc_Address) as Loc_Address,EMP_CODE,max(Emp_Name) as Emp_Name,max(FATHERS_NAME) as FATHERS_NAME,max(Designation) as Designation,max(Joining_date) as Joining_date,max(RELIEVING_DATE) as RELIEVING_DATE,sum(ACTUAL_AMOUNT) as ACTUAL_AMOUNT from (select TSPL_PAYPERIOD_MASTER.DATE_FROM"
            qry += " ,'" + CompCode + "' as Comp_Code,'" + CompanyName + "' as Comp_Name,'" + CompanyAdress + "' as Comp_Address,'" + clsCommon.myCDate(FromDate, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(ToDate, "dd/MMM/yyyy") + "' as ToDate, TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.City_Code ,'')='' Then '' "
            qry += " else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.City_Code) End End End as Loc_Address,TSPL_EMPLOYEE_MASTER.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name ,TSPL_EMPLOYEE_MASTER.FATHERS_NAME ,TSPL_EMPLOYEE_MASTER.Designation ,TSPL_EMPLOYEE_MASTER.Joining_date ,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE ,isnull(TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT,0) as ACTUAL_AMOUNT,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE ,datepart(dd,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Datewise,datename(month,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Monthwise ,datepart(yy,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Yearwise"
            qry += " from TSPL_GENERATE_SALARY_ATTENDANCE inner JOIN  TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE left join (select SALARY_GENERATION_CODE,EMP_CODE,SUB_HEAD_TYPE,PAY_HEAD_CODE,ACTUAL_AMOUNT from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='LWF') TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE and "
            qry += " TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE = TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.LOCATION_CODE  where convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103)>=convert(date,('" + txtfromDate.Value + "'),103) and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103) <=convert(date,('" + txtTodate.Value + "'),103)  and ACTUAL_AMOUNT >0 and TSPL_LOCATION_MASTER.LOCATION_CODE  in (" + clsCommon.myCstr(fndLocationCode.Value) + ")"
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                qry += " and TSPL_EMPLOYEE_MASTER.Devision_Code  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
            End If
            If txtEmployeeMult.arrValueMember IsNot Nothing AndAlso txtEmployeeMult.arrValueMember.Count > 0 Then
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE  in (" + clsCommon.GetMulcallString(txtEmployeeMult.arrValueMember) + ") "
            End If
            qry += " ) as mm group by EMP_CODE ,Location_Code ) AS  MM"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then

                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "rptDetailOfWelfareFundAmount", "Detail Of Welfare Fund Amount")
            Else
                clsCommon.MyMessageBoxShow("No Data Found")

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

   
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If fndLocationCode.Value = "" Then
            clsCommon.MyMessageBoxShow("Please select Location")
            Exit Sub
        End If
        funPrint()

    End Sub

   

    Private Sub RptDetailOfWelfareFundAmount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
        ButtonToolTip.SetToolTip(btnClosee, "Press Alt+C Close the Window")
        txtfromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub RptDetailOfWelfareFundAmount_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub

   

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
 
    'Private Sub txtEmployeeMult__My_Click(sender As Object, e As EventArgs) Handles txtEmployeeMult._My_Click
    '    Dim qry As String = GetEmploeeQry()
    '    txtEmployeeMult.arrValueMember = clsCommon.ShowMultipleSelectForm("EMPMulSel", qry, "Code", "Name", txtEmployeeMult.arrValueMember, txtEmployeeMult.arrDispalyMember)
    'End Sub

    Private Sub btnClosee_Click(sender As Object, e As EventArgs) Handles btnClosee.Click
        Me.close()
    End Sub

    
    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
    End Sub
End Class
