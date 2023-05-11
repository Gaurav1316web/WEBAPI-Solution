'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmForm12APF
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

    Private Sub Form5PF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmForm5_PF)
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


    End Sub

    Sub PrintData()
        Try

            Dim TOTAL_EMP_LAST_MONTH As Integer = 0
            Dim NEW_EMP As Integer = 0
            Dim LEFT_EMP As Integer = 0
            Dim NET_EMP As Integer = 0
            'Dim dt As DataTable

            Dim Qry As String = ""
            Qry += " SELECT (SELECT COUNT(EMP_CODE) FROM TSPL_EMPLOYEE_MASTER T1 WHERE CONVERT(date,joining_date,103) < "
            Qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "') AND RELIEVING_DATE IS  NULL "
            Qry += " AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS )) AS TOTAL_EMP_LAST_MONTH"
            TOTAL_EMP_LAST_MONTH = clsDBFuncationality.GetDataTable(Qry).Rows(0).Item(0)


            Qry = ""
            Qry += "SELECT (SELECT COUNT(EMP_CODE) FROM TSPL_EMPLOYEE_MASTER T1 WHERE CONVERT(date,joining_date,103) BETWEEN "
            Qry += "(SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "')"
            Qry += " AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "')"
            Qry += "AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS )) AS NEW_EMP"

            NEW_EMP = clsDBFuncationality.GetDataTable(Qry).Rows(0).Item(0)
            Qry = ""


            Qry = " SELECT (SELECT COUNT(EMP_CODE) FROM TSPL_EMPLOYEE_MASTER T1 WHERE CONVERT(date,RELIEVING_DATE,105) BETWEEN "
            Qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "')"
            Qry += " AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "') "
            Qry += " AND EMP_CODE  IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS )) AS LEFT_EMP"

            LEFT_EMP = clsDBFuncationality.GetDataTable(Qry).Rows(0).Item(0)

            NET_EMP = TOTAL_EMP_LAST_MONTH + NEW_EMP + NET_EMP - LEFT_EMP

            Qry = ""

            Qry += " SELECT '" & objCommonVar.CurrentCompanyName & "' AS Company_Name,'" & objCommonVar.CurrLocationName & "' AS Company_Address, "
            Qry += " '" & Me.txtFromPP.Value & "' as Pay_Period_Code," & TOTAL_EMP_LAST_MONTH & " AS TOTAL_EMP_LAST_MONTH," & NEW_EMP & " AS NEW_EMP," & LEFT_EMP & " AS LEFT_EMP ," & NET_EMP & " as NET_EMP, "
            Qry += " T1.ACC_NO,SUM(T1.HEAD_VALUE) AS HEAD_VALUE,SUM(T1.EMP_SHARE) AS EMP_SHARE,SUM(T1.CO_SHARE) AS CO_SHARE,"
            Qry += " SUM(T1.EMP_SHARE_REMITTED) AS EMP_SHARE_REMITTED, "
            Qry += " SUM(T1.CO_SHARE_REMITTED) AS CO_SHARE_REMITTED,SUM(T1.ADMIN_CHARGES) AS ADMIN_CHARGES,SUM(T1.ADMIN_CHARGES_REMITTED) AS ADMIN_CHARGES_REMITTED FROM ( "
            Qry += " SELECT 'EPF a/c No. 01' AS ACC_NO,T1.EMP_CODE,T1.HEAD_VALUE,T1.ACTUAL_AMOUNT AS EMP_SHARE,(T1.HEAD_VALUE*3.67/100) AS CO_SHARE,T1.ACTUAL_AMOUNT AS EMP_SHARE_REMITTED "
            Qry += " ,(T1.HEAD_VALUE*3.67/100) AS CO_SHARE_REMITTED,(HEAD_VALUE * 1.10/100) AS ADMIN_CHARGES,(HEAD_VALUE * 1.10/100) AS ADMIN_CHARGES_REMITTED FROM   "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS T1 INNER JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE"
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T3 ON T1.PAY_HEAD_CODE=T3.PAY_HEAD_CODE "
            Qry += " WHERE T2.PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "' AND T3.SUB_HEAD_TYPE='EPF' "
            Qry += " UNION ALL "
            Qry += " SELECT 'Pension Fund a/c No. 10' AS ACC_NO,T1.EMP_CODE,(CASE WHEN T1.HEAD_VALUE>6500 THEN 6500 ELSE T1.HEAD_VALUE END) AS HEAD_VALUE  ,0 AS EMP_PENSION, "
            Qry += " (T1.HEAD_VALUE*8.33/100) AS CO_PENSION,0 AS EMP_PENSION_REMITTED,(T1.HEAD_VALUE*8.33/100) AS CO_PENSION_REMITTED,0 AS ADMIN_CHARGES, "
            Qry += " 0 AS ADMIN_CHARGES_REMITTED FROM TSPL_GENERATE_SALARY_PAYHEADS T1 INNER JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE "
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T3 ON T1.PAY_HEAD_CODE=T3.PAY_HEAD_CODE "
            Qry += " WHERE T2.PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "' AND T3.SUB_HEAD_TYPE='EPF' AND T1.EMP_CODE IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_STATUS WHERE EPS_TO_EPF=0) "
            Qry += " UNION ALL "
            Qry += " SELECT 'E.D.L.I a/c No. 21' AS ACC_NO,T1.EMP_CODE,(CASE WHEN T1.HEAD_VALUE>6500 THEN 6500 ELSE T1.HEAD_VALUE END) AS HEAD_VALUE,0 AS EMP_EDLI, "
            Qry += " (T1.HEAD_VALUE*0.5/100) AS CO_EDLI,0 AS EMP_EPF_REMITTED,(T1.HEAD_VALUE*0.5/100) AS CO_EDLI_REMITTED,(HEAD_VALUE * 0.01/100) AS ADMIN_CHARGES, "
            Qry += " (HEAD_VALUE * 0.01/100) AS ADMIN_CHARGES_REMITTED FROM TSPL_GENERATE_SALARY_PAYHEADS T1 INNER JOIN TSPL_GENERATE_SALARY T2  "
            Qry += " ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE "
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T3 ON T1.PAY_HEAD_CODE=T3.PAY_HEAD_CODE "
            Qry += " WHERE T2.PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "' AND T3.SUB_HEAD_TYPE='EPF') AS T1 GROUP BY T1.ACC_NO "




            Dim dtFinal As DataTable
            dtFinal = clsDBFuncationality.GetDataTable(Qry)
            Dim frmcrsytal As New frmCrystalReportViewer()
            frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpForm12APF", "Form-12A(PF)")


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



End Class
