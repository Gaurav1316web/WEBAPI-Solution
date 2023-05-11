Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports XpertERPEngine
Imports Telerik.WinControls.UI

'====================shivani Tyagi
Public Class RptBOILetterReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptBOILetterReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Sub LoadEmployee()
        Dim strquery As String = "select distinct Emp_Code as [Employee Code],Emp_Name as [Employee Name] from TSPL_EMPLOYEE_MASTER"
        cbgEmployee.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgEmployee.ValueMember = "Employee Code"
        cbgEmployee.DisplayMember = "Employee Name"
    End Sub
    Sub PrintData()
        Try
            If rbtnEmployeeSelect.IsChecked AndAlso cbgEmployee.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one location")
            End If
            Dim Qry As String
            Qry = "select Emp_Name,(Add1+Add2)as Address,Joining_Date,Emp_Code from TSPL_EMPLOYEE_MASTER where 2=2"
            If rbtnEmployeeSelect.IsChecked Then
                Qry += " and  TSPL_EMPLOYEE_MASTER.Emp_Code in (" + clsCommon.GetMulcallString(cbgEmployee.CheckedValue) + ") "
            End If
            Dim dtFinal As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crptBOILetter", "BOI Letter Report")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub Reset()
        rbtnEmployeeAll.IsChecked = True
        LoadEmployee()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptBOILetterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Adding New")
        Reset()
    End Sub

    Private Sub RptBOILetterReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            PrintData()
        End If
    End Sub

    Private Sub rbtnEmployeeAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnEmployeeAll.ToggleStateChanged
        cbgEmployee.Enabled = rbtnEmployeeSelect.IsChecked
    End Sub
End Class