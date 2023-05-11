'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmEmp_Id
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

    Private Sub frmEmp_Id_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        Qry = ""
        Qry = " SELECT DISTINCT EMP_CODE, Emp_Name FROM TSPL_EMPLOYEE_MASTER "
        Qry += " where Emp_Status= 'Active' "
        Qry += " ORDER BY EMP_CODE "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "EMP_CODE"
        cbgLocation.DisplayMember = "Emp_Name"

    End Sub

    Private Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.frmEmp_Id)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        '' Anubhooti 23-July-2014 (BM00000003141)
        ' btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub PrintData()
        Try
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Employee Or Select All")
                Return
            End If
            Dim Qry As String = ""
            Qry = ""
            Qry += " SELECT '" + objCommonVar.CurrentCompanyName + "' as Company_name,EMP_CODE as Emp_Id,Emp_Name, Joining_date as doj FROM TSPL_EMPLOYEE_MASTER "
            If cbgLocation.CheckedValue.Count > 0 Then
                Qry += " WHERE EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If DT.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, DT, "crptEmployeeIdCard", "Employee Id Card")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
