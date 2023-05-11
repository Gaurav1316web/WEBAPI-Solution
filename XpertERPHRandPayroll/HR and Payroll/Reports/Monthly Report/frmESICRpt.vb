Imports common
Imports System.Data
Imports XpertERPEngine
Public Class FrmESICRpt
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#End Region

    
    Private Sub txtFromPP__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)

        'qry = ""
        'qry = " SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE, TSPL_EMPLOYEE_MASTER .Emp_Name FROM TSPL_GENERATE_SALARY "
        'qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE "
        'qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE= TSPL_EMPLOYEE_MASTER .EMP_CODE "
        'qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_MASTER.LOCATION_CODE "
        'qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code= TSPL_EMPLOYEE_MASTER.Comp_Code"
        'qry += " where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtFromPP.Value + "'  and TSPL_EMPLOYEE_MASTER.Location_Code ='" + FndLocationCode.Value + "' "
        'qry += " ORDER BY TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.CheckedAll()
        'cbgLocation.ValueMember = "EMP_CODE"
        'cbgLocation.DisplayMember = "Emp_Name"
        'cbgLocation.Enabled = False

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmESICRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub PrintData()
        Try
            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Pay Period.")
                Return
            End If
            Dim Qry As String
            Dim TotalEsi As Decimal=0
            Dim TotalEmpCount As Decimal
            Dim EmpESI As Decimal
            Dim CompESI As Decimal
            Dim companyName As String
            Dim companyAddress As String
            Dim TotalWages As Decimal

           
            Qry = ""
            Qry += " select coalesce(SUM(TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT),0) AS TOTAL_ESI from TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" + txtFromPP.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            TotalEsi = dt.Rows(0).Item("TOTAL_ESI")


            Qry = ""
            Qry += "select coalesce( count(TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE ),0) AS TOTAL_Employee  from TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += "TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE"
            Qry += " WHERE  TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" + txtFromPP.Value + "' AND TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE IN ('COESI','EMPESI')"

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)

            TotalEmpCount = dt1.Rows(0).Item("TOTAL_Employee")
            Qry = ""
            Qry += "select coalesce(SUM(TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT),0) AS COMP_ESI from TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += "TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE"
            Qry += " WHERE  TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" + txtFromPP.Value + "' AND TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE IN ('COESI')"

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry)
            CompESI = dt2.Rows(0).Item("COMP_ESI")

            Qry = ""
            Qry += " select coalesce(SUM(TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT),0) AS EMP_ESI from TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE"
            Qry += " WHERE  TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" + txtFromPP.Value + "' AND TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE IN ('EMPESI')"

            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(Qry)
            EmpESI = dt3.Rows(0).Item("EMP_ESI")

            Qry = ""
            Qry += " select coalesce(SUM(case when ISEARNING=1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else -TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT end),0) AS TOTAL_Wages from TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE "

            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable(Qry)
            TotalWages = dt4.Rows(0).Item("TOTAL_Wages")


            Qry = ""
            Qry += " Select  Add1+Case When ISNULL(Add2,'')='' Then '' else ', '+Add2+ Case When ISNULL(Add3,'')='' Then '' Else ', '+Add3+ Case When ISNULL(City_Code ,'')='' Then '' else '-'+CONVERT(varchar, City_Code) End End End as companyAddress from TSPL_COMPANY_MASTER  where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(Qry)
            companyAddress = dt5.Rows(0).Item("companyAddress")


            Qry = ""
            Qry += "Select  Comp_Name as companyName from TSPL_COMPANY_MASTER  where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"
            Dim dt6 As DataTable = clsDBFuncationality.GetDataTable(Qry)
            companyName = dt6.Rows(0).Item("companyName")



            Qry = ""
            Qry += "select TSPL_GENERATE_SALARY.PAY_PERIOD_CODE," & TotalEsi & "  as TotalESI ," & TotalEmpCount & " as TotalEmpCount," & EmpESI & " as EmpESI," & CompESI & "  as CompESI," & TotalWages & " as TotalWages,'" & companyAddress & "' as companyAddress,'" & companyName & " ' as companyName,TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(City_Code ,'')='' Then '' else '-'+CONVERT(varchar, City_Code) End End End as Location_Address,TSPL_LOCATION_MASTER.Location_Desc from TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += "  TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += "  INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE"
            Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER .EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  "
            Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_EMPLOYEE_MASTER.LOCATION_CODE "
            Qry += "    WHERE  TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" + txtFromPP.Value + "'  AND TSPL_LOCATION_MASTER.Location_Code ='" + FndLocationCode.Value + "' and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE IN ('COESI','EMPESI')"

            Dim dtFinal As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "ESICCrpt", "ESIC Report")


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub

    Private Sub FrmESICRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
        FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
    End Sub
    Function allowtosave()

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
    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        If (allowtosave()) Then
            Qry = ""
            Qry = " SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE, TSPL_EMPLOYEE_MASTER .Emp_Name FROM TSPL_GENERATE_SALARY "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE "
            Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE= TSPL_EMPLOYEE_MASTER .EMP_CODE "
            Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_MASTER.LOCATION_CODE "
            Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code= TSPL_EMPLOYEE_MASTER.Comp_Code"
            Qry += " where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtFromPP.Value + "'  and TSPL_EMPLOYEE_MASTER.Location_Code ='" + FndLocationCode.Value + "' "
            Qry += " ORDER BY TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE "
            cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
            cbgLocation.CheckedAll()
            cbgLocation.ValueMember = "EMP_CODE"
            cbgLocation.DisplayMember = "Emp_Name"
            cbgLocation.Enabled = False

        End If
            End Sub
End Class
