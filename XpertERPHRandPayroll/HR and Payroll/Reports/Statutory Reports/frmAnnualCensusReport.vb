'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmAnnualCensusReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Dim Qry As String
    Dim Fill_Emp As Boolean = False

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PrintData()
    End Sub

    Private Sub frmAnnualCensusReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAnnualCensusReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        '' Anubhooti 23-July-2014 (BM00000003141)
        'btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(cboYear.SelectedValue) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Year.", Me.Text)
                Return
            End If

            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select AtLeast Single Employee Or Select All", Me.Text)
                Return
            End If

            Dim qry As String = ""

            qry += " SELECT T1.EMP_CODE,T2.Emp_Name ,T2.FATHERS_NAME, '" + objCommonVar.CurrentCompanyName + "' as 'Company','" + cboYear.SelectedValue.ToString + "' AS 'Year', "
            qry += " (isnull(T2.[Add1],'')+ isnull((SELECT  City_Name FROM TSPL_CITY_MASTER WHERE City_Code= T2.[PRESENT_CITY_CODE]),'') + ' '+isnull((SELECT STATE_NAME  FROM TSPL_STATE_MASTER WHERE STATE_CODE =T2.[PRESENT_STATE_CODE]),'')+ ' '+isnull((SELECT COUNTRY_NAME  FROM TSPL_COUNTRY_MASTER  WHERE COUNTRY_CODE  =T2.[PRESENT_COUNTRY_CODE]),'')+' ' +isnull([Pin_Code],''))  AS Address, "
            qry += " t2.Joining_date, T3.NO_OF_CHILDS, "
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=1 THEN T1.PRESENT_DAYS ELSE 0 END) AS JAN,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=2 THEN T1.PRESENT_DAYS ELSE 0 END) AS FEB,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=3 THEN T1.PRESENT_DAYS ELSE 0 END) AS MAR,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=4 THEN T1.PRESENT_DAYS ELSE 0 END) AS APR,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=5 THEN T1.PRESENT_DAYS ELSE 0 END) AS MAY,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=6 THEN T1.PRESENT_DAYS ELSE 0 END) AS JUN,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=7 THEN T1.PRESENT_DAYS ELSE 0 END) AS JUL,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=8 THEN T1.PRESENT_DAYS ELSE 0 END) AS AUG,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=9 THEN T1.PRESENT_DAYS ELSE 0 END) AS SEP,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=10 THEN T1.PRESENT_DAYS ELSE 0 END) AS OCT,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=11 THEN T1.PRESENT_DAYS ELSE 0 END) AS NOV,"
            qry += " (CASE WHEN MONTH(T4.DATE_FROM)=12 THEN T1.PRESENT_DAYS ELSE 0 END) AS DEC FROM TSPL_ATTENDANCE_SUMMARY T1 INNER JOIN TSPL_EMPLOYEE_MASTER T2 "
            qry += " ON T1.EMP_CODE=T2.EMP_CODE LEFT JOIN (select EMP_CODE,COUNT(LINE_NO) AS NO_OF_CHILDS from TSPL_EMPLOYEE_FAMILIES where RELATION_WITH_EMP in ('SON','DAUGHTER') GROUP BY EMP_CODE) T3 ON T2.EMP_CODE=T3.EMP_CODE "
            qry += " INNER JOIN TSPL_PAYPERIOD_MASTER T4 ON T1.PAY_PERIOD_CODE=T4.PAY_PERIOD_CODE "
            qry += " WHERE YEAR(T4.DATE_FROM)=" + cboYear.SelectedValue.ToString + " "
            qry += " and T1.EMP_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            qry += " ORDER BY T1.EMP_CODE;"

            Dim dt_final As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt_final.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmcrsytal As New frmCrystalReportViewer()
                frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dt_final, "crptAnnualCensus", "Employee Annual Census Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboYear_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboYear.Enter
        Fill_Emp = False
        Dim dt_Cbo As DataTable = clsDBFuncationality.GetDataTable("SELECT DISTINCT YEAR (T2.DATE_FROM ) as yr  FROM TSPL_GENERATE_SALARY T1 LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER T2 ON T2.PAY_PERIOD_CODE = T1.PAY_PERIOD_CODE ")
        cboYear.DataSource = dt_Cbo.Copy()
        cboYear.ValueMember = "yr"
        cboYear.DisplayMember = "yr"
        cboYear.SelectedIndex = -1
        Fill_Emp = True
    End Sub

    Private Sub cboYear_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboYear.SelectedValueChanged
        If clsCommon.myLen(cboYear.SelectedValue) > 0 AndAlso Fill_Emp Then
            Dim qry As String = ""
            qry += ""
            qry += " SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE, TSPL_EMPLOYEE_MASTER .Emp_Name FROM TSPL_GENERATE_SALARY "
            qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE "
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE= TSPL_EMPLOYEE_MASTER .EMP_CODE "
            qry += " left outer join TSPL_PAYPERIOD_MASTER  on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = TSPL_GENERATE_SALARY.PAY_PERIOD_CODE"
            qry += " where(Year(TSPL_PAYPERIOD_MASTER.DATE_FROM) = " + cboYear.SelectedValue.ToString + ")"
            qry += " ORDER BY TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE "

            cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgLocation.ValueMember = "EMP_CODE"
            cbgLocation.DisplayMember = "Emp_Name"
        End If

    End Sub

    Private Sub cboYear_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.Validated
        'If clsCommon.myLen(cboYear.SelectedValue) > 0 Then
        '    Dim qry As String = ""
        '    qry += ""
        '    qry += " SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE, TSPL_EMPLOYEE_MASTER .Emp_Name FROM TSPL_GENERATE_SALARY "
        '    qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE "
        '    qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE= TSPL_EMPLOYEE_MASTER .EMP_CODE "
        '    qry += " left outer join TSPL_PAYPERIOD_MASTER  on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = TSPL_GENERATE_SALARY.PAY_PERIOD_CODE"
        '    qry += " where(Year(TSPL_PAYPERIOD_MASTER.DATE_FROM) = " + cboYear.SelectedValue.ToString + ")"
        '    qry += " ORDER BY TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE "

        '    cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        '    cbgLocation.ValueMember = "EMP_CODE"
        '    cbgLocation.DisplayMember = "Emp_Name"
        'End If
    End Sub
End Class
