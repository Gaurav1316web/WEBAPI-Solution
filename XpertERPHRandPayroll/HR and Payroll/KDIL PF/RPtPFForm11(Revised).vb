'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RPtPFForm11_Revised_
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RPtPFForm11_Revised_)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub RPtPFForm11_Revised__Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
    End Sub
    Sub LoadReport()
        Dim sQry As String
        sQry = "select Emp_Code,Emp_Name ,FATHERS_NAME ,Joining_date,TSPL_Employee_Master.PF_NO  ,Designation_Desc ,Comp_Name,ISNULL(TSPL_Employee_Master.PF_NO,'') As Comp_PF_NO  from TSPL_Employee_Master left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =TSPL_Employee_Master.DEPARTMENT_CODE left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Employee_Master.Comp_Code left join TSPL_LOCATION_MASTEr on TSPL_LOCATION_MASTEr.Location_Code =TSPL_Employee_Master.LOCATION_CODE left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_Employee_Master.DEVISION_CODE   where ISpf ='1'"
        If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
            sQry += " and TSPL_Employee_Master.Emp_Code  in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ") "
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            sQry += " and TSPL_Employee_Master.LOcation_Code  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If
        If txtMultDivision.arrValueMember IsNot Nothing AndAlso txtMultDivision.arrValueMember.Count > 0 Then
            sQry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtMultDivision.arrValueMember) + ") "
        End If
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFForm11", "PF Form 11")
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadReport()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub RPtPFForm11_Revised__KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
       
        End If
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtEmployee__My_Click(sender As Object, e As EventArgs) Handles txtEmployee._My_Click
        Dim qry As String = " select EMP_CODE as Code, Emp_Name as Name from TSPL_Employee_Master where ispf='1' and Location_code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ") "
        txtEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtEmployee.arrValueMember, txtEmployee.arrDispalyMember)

    End Sub

    Private Sub txtMultDivision__My_Click(sender As Object, e As EventArgs) Handles txtMultDivision._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtMultDivision.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtMultDivision.arrValueMember, txtMultDivision.arrDispalyMember)
    End Sub
End Class
