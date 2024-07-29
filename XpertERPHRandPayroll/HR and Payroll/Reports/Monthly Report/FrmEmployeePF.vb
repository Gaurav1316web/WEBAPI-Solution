'--22/04/2014--form Add By- Ashwani Raghav ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports XpertERPEngine

Public Class FrmEmployeePF
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PrintData()
    End Sub

    Private Sub FrmSalarySummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        TxtDiv.MendatroryField = False
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmEmployeePFRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)

    End Sub

    Sub PrintData()
        Try
            Dim DivCond As String = ""
            Dim Location_Code As String = ""
            'If txtLocationMult.arrValueMember Is Nothing AndAlso txtLocationMult.arrValueMember.Count <= 0 Then
            '    clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
            '    Exit Sub
            'End If
            'If clsCommon.myLen(txtFromPP.Value) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please Select Pay Period.")
            '    Return
            'End If

            'If clsCommon.myLen(TxtLoc.Value) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select location.")
            '    Return
            'End If
            Dim LocAddress As String = ""
            Dim FirmPf As String = ""
            Dim Qry As String = ""
            'Qry = "select Regn_No as ESTCode,Comp_Name as Name,Add1 Address1,Add2 as Address2,Add3 as Address3,"
            'Qry += " DATEPART(month,(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + txtFromPP.Value + "'))Month,"
            'Qry += " DATEPART(Year,(select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + txtFromPP.Value + "'))Year,"
            'Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EPF')TotalEmpEPF,"
            'Qry += " (select SUM(HEAD_VALUE)  from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EPF')TotalSalaryEPF,"
            'Qry += " ((select top(1) EMPEPF_PER from TSPL_PF_RULE_MASTER)*(select SUM(HEAD_VALUE)  "
            'Qry += " from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EPF')/100)Challan1,"
            'Qry += " ((select top(1) COEPS_PER from TSPL_PF_RULE_MASTER)*(select SUM(HEAD_VALUE)  "
            'Qry += " from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EPF')/100)EPF3,"
            'Qry += " ((select top(1) ACCOEPF_PER from TSPL_PF_RULE_MASTER)*(select SUM(HEAD_VALUE)  "
            'Qry += " from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EPF')/100)Challan2,"
            'Qry += " ((select top(1) COEPF_PER from TSPL_PF_RULE_MASTER)*(select SUM(HEAD_VALUE)  "
            'Qry += " from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EPF')/100)Challan10,"
            'Qry += " ((select top(1) COEDLI_PER from TSPL_PF_RULE_MASTER)*(select SUM(HEAD_VALUE)  "
            'Qry += " from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EPF')/100)Challan21,"
            'Qry += " ((select top(1) ACCOEDLI_PER from TSPL_PF_RULE_MASTER)*(select SUM(HEAD_VALUE)  from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EPF')/100)Challan22,"
            'Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE in('EMPESI','COESI'))TotEmpESI,"
            'Qry += " (select SUM(HEAD_VALUE)  from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE in('EMPESI','COESI'))TotSalESI,"
            'Qry += " (select SUM(ACTUAL_AMOUNT)  from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='EMPESI')EmployeeShare,"
            'Qry += " (select SUM(ACTUAL_AMOUNT)  from TSPL_GENERATE_SALARY_PAYHEADS where SUB_HEAD_TYPE='COESI')EmployerShare"
            'Qry += " from TSPL_COMPANY_MASTER where Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
            Dim LocationFirstTime As Integer = 0
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
                LocationFirstTime += 1
                LocAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + add4 As [Address] FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"))
                FirmPf = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_LOCATION_MASTER.PF_NO FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"))
            Else
                LocAddress = objCommonVar.CurrentCompanyName
                FirmPf = ""
            End If
            'If clsCommon.myLen(TxtDiv.Value) > 0 Then
            '    DivCond = " AND ISNULL(EMP.Devision_code,'') ='" & TxtDiv.Value & "'"
            'Else
            '    DivCond = ""
            'End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                DivCond += " AND ISNULL(EMP.Devision_code,'')  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "

            End If
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                Location_Code += "(" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"

            End If
            Dim LocDesc As String = ""
            Qry = clsSalaryGeneration.GetPFESIQuery(txtFromPP.Value, Location_Code, DivCond, LblLocName.Text, LocAddress, FirmPf, LocDesc)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "crptSalaryEPF", "Employee PF ")
                frmcrystal = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtLoc._MYValidating
        Dim qry As String = " Select Location_Code As [Code],Location_Desc As [Loc Desp],Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + add4 As [Address],City_Code As [City Code],State AS [State Coe],Country AS [Country] ,Location_Type As [Loc Type],Loc_Status As [Loc Status],Loc_Segment_Code As [Loc Segment Code],Division_Code As [Division Code],Division_Name As [Division Name] From TSPL_location_master  "
        TxtLoc.Value = clsCommon.ShowSelectForm("LocPFOrg", qry, "Code", " ", TxtLoc.Value, "", isButtonClicked)
        If clsCommon.myLen(TxtLoc.Value) > 0 Then
            LblLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') AS Location_Desc FROM TSPL_location_master WHERE Location_Code='" & TxtLoc.Value & "'"))
        Else
            LblLocName.Text = ""
        End If
    End Sub

    Private Sub TxtDiv__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtDiv._MYValidating
        Dim qry As String = " SELECT DEVISION_CODE AS [Code],DEVISION_NAME AS [Division Name],DESCRIPTION AS [Description] FROM TSPL_DEVISION_MASTER "
        TxtDiv.Value = clsCommon.ShowSelectForm("DivPFOrg", qry, "Code", " ", TxtDiv.Value, "", isButtonClicked)
        If clsCommon.myLen(TxtDiv.Value) > 0 Then
            LblDiv.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT DEVISION_NAME AS [Division Name] FROM TSPL_DEVISION_MASTER WHERE DEVISION_CODE='" & TxtDiv.Value & "'"))
        Else
            LblDiv.Text = ""
        End If
    End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & txtFromPP.Value & "') "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
End Class

