'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmAttendedDaysReport
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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmAttendedDaysReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAttendedDaysReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        '' Anubhooti 23-July-2014 (BM00000003141)
        'btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)

        qry = ""
        qry = " SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE, TSPL_EMPLOYEE_MASTER .Emp_Name FROM TSPL_GENERATE_SALARY "
        qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE "
        qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE= TSPL_EMPLOYEE_MASTER .EMP_CODE "
        qry += " where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtFromPP.Value + "' "
        qry += " ORDER BY TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "EMP_CODE"
        cbgLocation.DisplayMember = "Emp_Name"
    End Sub

    Sub PrintData()
        Try
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Employee Or Select All")
                Return
            End If

            Dim Qry As String = ""

            Qry = ""
            Qry += " SELECT '" & objCommonVar.CurrentCompanyName & "' AS Company_Name,'" & objCommonVar.CurrLocationName & "' AS Company_Address, " _
            & " '" & Me.txtFromPP.Value & "' as Pay_Period_Code,T1.EMP_CODE as Employee_Code,T2.Emp_Name as Employee_Name,T1.PRESENT_DAYS FROM TSPL_ATTENDANCE_SUMMARY T1 INNER JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.EMP_CODE=T2.EMP_CODE "
            Qry += " WHERE T1.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "

            If cbgLocation.CheckedValue.Count > 0 Then
                Qry += " AND T1.EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            Qry += " ORDER BY T1.EMP_CODE"
            Dim dtFinal As DataTable
            dtFinal = clsDBFuncationality.GetDataTable(Qry)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpAttendedDaysReport", "Attended Days Report")


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkLocAll_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If chkLocAll.IsChecked = True Then
        '    cbgLocation.CheckedAll()
        'Else
        '    cbgLocation.UnCheckedAll()

        'End If
    End Sub

    
End Class
