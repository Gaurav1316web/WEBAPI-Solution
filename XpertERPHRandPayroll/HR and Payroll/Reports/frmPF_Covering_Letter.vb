'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmPF_Covering_Letter
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

    Private Sub frmPF_Covering_Letter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPF_Covering_Letter)
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
        qry += " SELECT DISTINCT TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name "
        qry += " from TSPL_GENERATE_SALARY_PAYHEADS "
        qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS .EMP_CODE =TSPL_EMPLOYEE_MASTER .EMP_CODE "
        qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY T1 ON T1.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE "
        qry += " LEFT OUTER JOIN TSPL_PAYHEAD_MASTER  T5 ON T5.PAY_HEAD_CODE  = TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE "
        qry += " WHERE T1.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' AND T5.SUB_HEAD_TYPE  = 'EPF' and TSPL_EMPLOYEE_MASTER.ISPF =1 ORDER BY TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "EMP_CODE"
        cbgLocation.DisplayMember = "Emp_Name"
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Pay Period.", Me.Text)
                Return
            End If

            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select AtLeast Single Employee Or Select All", Me.Text)
                Return
            End If

            Dim Qry As String = ""
            Qry = ""
            Qry += " select T3.PF_NO , T1.EMP_CODE, T3.Emp_Name ,T1.HEAD_VALUE,(CASE WHEN T1.HEAD_VALUE > 6500 THEN 6500 ELSE T1.HEAD_VALUE  END ) AS EDLI_WAGES, "
            Qry += " (CASE WHEN T4.EPS_TO_EPF =0 THEN 0 else(CASE WHEN T1.HEAD_VALUE > 6500 THEN 6500 ELSE T1.HEAD_VALUE  END ) END) AS Pension_WAGES,T1.ACTUAL_AMOUNT, T2.PAYABLE_DAYS, "
            Qry += " (CASE WHEN T4.EPS_TO_EPF =0 THEN T1.ACTUAL_AMOUNT ELSE (CASE WHEN (T1.HEAD_VALUE*.0833) > 541 THEN (T1.ACTUAL_AMOUNT-541) ELSE (T1.ACTUAL_AMOUNT-(T1.HEAD_VALUE*.0833)) END)END) AS COPF,"
            Qry += " (CASE WHEN T4.EPS_TO_EPF =0 THEN 0.00 ELSE (CASE WHEN (T1.HEAD_VALUE*.0833) < 541 THEN (T1.HEAD_VALUE*.0833) ELSE 541 END)END) AS PENSION , "
            Qry += " '" + objCommonVar.CurrentCompanyName + "' AS 'Company_Name', T6.PAY_PERIOD_CODE, 1 as grp "
            Qry += " from TSPL_GENERATE_SALARY_PAYHEADS T1"
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE T2 ON T2.EMP_CODE = T1.EMP_CODE "
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER  T3 ON T3.EMP_CODE = T1.EMP_CODE "
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_STATUS T4 ON T4.EMP_CODE = T1.EMP_CODE "
            Qry += " LEFT OUTER JOIN TSPL_PAYHEAD_MASTER  T5 ON T5.PAY_HEAD_CODE  = T1.PAY_HEAD_CODE "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY T6 ON T6.SALARY_GENERATION_CODE = T1.SALARY_GENERATION_CODE "
            Qry += " WHERE T5.SUB_HEAD_TYPE  = 'EPF' and T3.ISPF =1"
            Qry += " AND T1.EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") and T6.PAY_PERIOD_CODE ='" + txtFromPP.Value + "' "
            Qry += " ORDER BY T1.EMP_CODE"

            Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If DT.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmcrsytal As New frmCrystalReportViewer()
                frmcrsytal.funreport(CrystalReportFolder.HRPayroll, DT, "crptPFCoveringLetter", "PF Covering Letter ")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
