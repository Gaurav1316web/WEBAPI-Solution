Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmLeaveRegister
    Inherits FrmMainTranScreen
    Dim empCode As GridViewTextBoxColumn
    Dim empName As GridViewTextBoxColumn
    Dim leaveDate As GridViewDateTimeColumn
    Dim leaveCode As GridViewComboBoxColumn
    Dim leaveName As GridViewTextBoxColumn

    Dim firstHalf As GridViewCheckBoxColumn
    Dim secondHalf As GridViewCheckBoxColumn


    Sub LoadGridColumns()
        gvLeaveRegister.Rows.Clear()
        gvLeaveRegister.Columns.Clear()
        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = "empCode"
        empCode.Width = 100
        empCode.ReadOnly = True
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveRegister.Columns.Add(empCode)

        empName.FormatString = ""
        empName.HeaderText = "Employee Name"
        empName.Name = "EmpName"
        empName.Width = 100
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveRegister.Columns.Add(empName)

        leaveDate.FormatString = ""
        leaveDate.HeaderText = "Leave Date"
        leaveDate.Name = "LeaveDate"
        leaveDate.Width = 100
        leaveDate.ReadOnly = True
        leaveDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveRegister.Columns.Add(leaveDate)

        leaveCode.FormatString = ""
        leaveCode.HeaderText = "Leave Code"
        leaveCode.Name = "LeaveCode"
        leaveCode.Width = 100
        'leaveCode.ReadOnly = True
        leaveCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveRegister.Columns.Add(leaveCode)


        leaveName.FormatString = ""
        leaveName.HeaderText = "Leave Name"
        leaveName.Name = "LeaveName"
        leaveName.Width = 100
        leaveName.ReadOnly = True
        leaveName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveRegister.Columns.Add(leaveName)



        firstHalf.FormatString = ""
        firstHalf.HeaderText = "First Half"
        firstHalf.Name = "FirstHalf"
        firstHalf.Width = 100
        firstHalf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveRegister.Columns.Add(firstHalf)

        secondHalf.FormatString = ""
        secondHalf.HeaderText = "Second Half"
        secondHalf.Name = "SecondHalf"
        secondHalf.Width = 100
        secondHalf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveRegister.Columns.Add(secondHalf)




    End Sub
    Private Sub frmLeaveRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class