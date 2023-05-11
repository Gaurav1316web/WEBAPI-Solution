'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports XpertERPEngine
Imports System.IO
Public Class RptRegisterOfDeduction
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptRegisterOfDeduction)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub LoadData()
        Dim Qry As String = "select * from (select TSPL_HR_DAMAGE_DETAIL.Emp_Code,Emp_Name,Fathers_Name,SEX,Department_Name,Damage_Type,convert(varchar,TSPL_HR_DAMAGE_MASTER.Created_Date,103) as Damage_Date,convert(varchar,damage_detail_Date	,103)as damage_detail_Date,Deduction_Imposed,No_Of_Installment,convert(varchar,Amt_Realised_Date,103)as Amt_Realised_Date,Comp_Name,(TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3)as Address,DATENAME (MONTH ,CONVERT(date,damage_detail_Date,103))as Month,DATENAME (Year ,CONVERT(date,damage_detail_Date,103))as Year,Damage_Detail_Description from TSPL_HR_DAMAGE_DETAIL  left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_HR_DAMAGE_DETAIL.Emp_Code left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE=TSPL_EMPLOYEE_MASTER.Department_Code left join TSPL_HR_DAMAGE_MASTER on TSPL_HR_DAMAGE_MASTER.Damage_code=TSPL_HR_DAMAGE_DETAIL.Damage_code left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_Code=TSPL_HR_DAMAGE_DETAIL.Comp_Code)d where d.Month='" & cboMonth.Text & "' and d.year='" & Cboyear.Text & "'"
        If clsCommon.myLen(fndEmployeeCode.Value) > 0 Then
            Qry += " and d.Emp_Code='" & fndEmployeeCode.Value & "' "
        End If
        Dim dtgv As New DataTable
        Dim frmcrystal As New frmCrystalReportViewer()
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptDamageLoss", "Register Of Deduction")
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Addnew()
    End Sub
   
    Sub LoadYear()
        Dim Qry As String = "select distinct Year as [Code] from (select DATENAME (Year ,CONVERT(date,damage_detail_Date,103))as Year	 from TSPL_HR_DAMAGE_DETAIL)as t"
        Cboyear.DataSource = clsDBFuncationality.GetDataTable(Qry)
        Cboyear.ValueMember = "Code"
        Cboyear.DisplayMember = "Code"
    End Sub
    Sub Addnew()
        LoadYear()
        fndEmployeeCode.Value = ""
        lblEmployeeName.Text = ""
        cboMonth.Text = ""
        Cboyear.Text = ""
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub fndEmployeeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndEmployeeCode._MYValidating
        Dim qry As String = " Select Emp_Code AS Code ,Emp_Name  From TSPL_EMPLOYEE_MASTER "
        fndEmployeeCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("EMP", qry, "Code", "LEAVING_REASON = ''", fndEmployeeCode.Value, "Code", isButtonClicked))

        If clsCommon.myLen(clsCommon.myCstr(fndEmployeeCode.Value)) > 0 Then
            lblEmployeeName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_Code='" & fndEmployeeCode.Value & "'"))
        Else
            lblEmployeeName.Text = ""
        End If
    End Sub

    Private Sub RptRegisterOfDeduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Addnew()
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N for Save/Update ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        
    End Sub
    Private Sub RptRegisterOfDeduction_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Addnew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
       
        End If
    End Sub
End Class