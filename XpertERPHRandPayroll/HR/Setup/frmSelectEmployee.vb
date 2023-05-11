Imports common
Imports XpertERPEngine
Public Class FrmSelectEmployee
    Public arrC As ArrayList = Nothing
    Public arrD As ArrayList = Nothing
    Public strSegment As String = Nothing
    Public isCencelButtonClicked As Boolean = False

    Private Sub FrmSelectEmployee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim qry As String = " select EMP_CODE as [Code],Emp_Name as [Employee Name] from tspl_employee_master " 'WHERE EMP_CODE='" + strSegment + "'"
        cbg1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbg1.ValueMember = "Code"
        cbg1.DisplayMember = "Employee Name"

        cbg1.CheckedValue = arrC
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        arrC = New ArrayList()
        arrD = New ArrayList()

        arrC = cbg1.CheckedValue
        arrD = cbg1.CheckedDisplayMember
        Me.Close()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        isCencelButtonClicked = True
        Me.Close()
    End Sub
End Class
