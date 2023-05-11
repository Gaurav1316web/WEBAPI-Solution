'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptBonusStatement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptBonusStatement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadData()
        Dim Qry As String = ""
        Dim From_Date As Date
        Dim To_Date As Date
        Dim year As String
        From_Date = "01/04/" & (txtFromYear.Text)
        To_Date = "31/03/" & (txtFromYear.Text + 1)
        year = clsCommon.myCstr(txtFromYear.Text + 1)
        Qry = "select ('" & year & "') as Year, count(TSPL_EMPBONUS_DETAIL.Emp_Code)as Emp_Code,sum(BONUS_AMOUNT)as BONUS_AMOUNT,max(TSPL_COMPANY_MASTER.Comp_Name+TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3)as Address "
        Qry += " from TSPL_EMPBONUS_DETAIL left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_EMPBONUS_DETAIL.Emp_Code left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_EMPLOYEE_MASTER.Comp_Code left join "
        Qry += " TSPL_EMPLOYEE_BONUS on TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE=TSPL_EMPBONUS_DETAIL.EMP_BONUS_CODE left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_EMPLOYEE_BONUS.PAYABLE_PAY_PERIOD_CODE  where "
        Qry += " convert(date,DATE_FROM ,103)>=convert(date,'" & From_Date.ToString & "',103) and convert(date,DATE_TO ,103) <=convert(date,'" & To_Date.ToString & "',103)"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptBonusStatement", "Bonus Statement Report")
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.close()
    End Sub

    Private Sub RptBonusStatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+P Print ")
    End Sub

    Private Sub RptBonusStatement_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag Then
            LoadData()

        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
End Class