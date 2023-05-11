'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmMonthlyESI_Report
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

    Private Sub frmMonthlyESI_Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMonthlyESI_Report)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
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
        qry += " SELECT DISTINCT TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name "
        qry += " from TSPL_GENERATE_SALARY_PAYHEADS "
        qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS .EMP_CODE =TSPL_EMPLOYEE_MASTER .EMP_CODE "
        qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY T1 ON T1.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE "
        qry += " LEFT OUTER JOIN TSPL_PAYHEAD_MASTER  T5 ON T5.PAY_HEAD_CODE  = TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE "
        qry += " WHERE T1.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' AND TSPL_EMPLOYEE_MASTER.ISESI  =1"
        qry += " AND T5.SUB_HEAD_TYPE  = 'EMPESI' "
        qry += " ORDER BY TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "EMP_CODE"
        cbgLocation.DisplayMember = "Emp_Name"
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Pay Period.")
                Return
            End If

            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Employee Or Select All")
                Return
            End If

            Dim Qry As String = ""
            Qry = ""

            Qry += " select T3.ESI_NO, T1.EMP_CODE, T3.Emp_Name, T2.PAYABLE_DAYS, T1.HEAD_VALUE AS ESI_EARNING, T1.ACTUAL_AMOUNT as EMPESI, "
            Qry += " (CASE WHEN T1.ACTUAL_AMOUNT  > 0 THEN (T1.HEAD_VALUE*.0475) ELSE 0.00  END ) AS COESI,"
            Qry += "   '" + objCommonVar.CurrentCompanyName + "' AS 'Company_Name', T6.PAY_PERIOD_CODE, 1 as grp  "
            Qry += " from TSPL_GENERATE_SALARY_PAYHEADS T1 "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE T2 ON T2.EMP_CODE = T1.EMP_CODE  AND T1.SALARY_GENERATION_CODE = T2.SALARY_GENERATION_CODE "
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER  T3 ON T3.EMP_CODE = T1.EMP_CODE  "
            Qry += " LEFT OUTER JOIN TSPL_PAYHEAD_MASTER  T5 ON T5.PAY_HEAD_CODE  = T1.PAY_HEAD_CODE  "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY T6 ON T6.SALARY_GENERATION_CODE = T1.SALARY_GENERATION_CODE  "
            Qry += " WHERE T5.SUB_HEAD_TYPE  = 'EMPESI' and T3.ISESI  =1"
            Qry += " AND T1.EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") and T6.PAY_PERIOD_CODE ='" + txtFromPP.Value + "' "
            If chkZeroESI.Checked = False Then
                Qry += " AND T1.ACTUAL_AMOUNT > 0  "
            End If
            Qry += " ORDER BY T1.EMP_CODE"

            Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If DT.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                Dim frmcrsytal As New frmCrystalReportViewer()
                frmcrsytal.funreport(CrystalReportFolder.HRPayroll, DT, "crptMonthlyESI", "Monthly ESI Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
