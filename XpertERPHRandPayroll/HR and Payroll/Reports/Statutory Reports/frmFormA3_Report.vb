'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmFormA3_Report
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isLoadEmp As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Private Max_date As Date
    Private Min_date As Date

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PrintData()
    End Sub

    Private Sub frmFormA3_Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmFormA3_Report)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
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

            Dim Qry As String = ""
            Qry = ""
            Qry += " select T3.PF_NO , T1.EMP_CODE, T3.Emp_Name, T3.FATHERS_NAME  ,(SELECT (ISNULL(Add1,'')+' ' +ISNULL(Add2,'')+' '+ISNULL(Add3,'')) AS 'add'  FROM TSPL_COMPANY_MASTER WHERE TSPL_COMPANY_MASTER.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "') as 'Company_Address', T1.HEAD_VALUE,  T1.ACTUAL_AMOUNT, T2.PAYABLE_DAYS,( CASE When T4.EPS_TO_EPF= 1 then 'Yes' else 'No' end ) as EPS_TO_EPF,  (case when T1.PF_MAX_LM > 0 then 'No' else 'Yes' end ) as PF_MAX_LM,"
            Qry += " (CASE WHEN T4.EPS_TO_EPF =0 THEN T1.ACTUAL_AMOUNT ELSE (CASE WHEN (T1.HEAD_VALUE*.0833) > 541 THEN (T1.ACTUAL_AMOUNT-541) ELSE (T1.ACTUAL_AMOUNT-(T1.HEAD_VALUE*.0833)) END)END) AS COPF,  (CASE WHEN T4.EPS_TO_EPF =0 THEN 0.00 ELSE (CASE WHEN (T1.HEAD_VALUE*.0833) < 541 THEN (T1.HEAD_VALUE*.0833) ELSE 541 END)END) AS PENSION , '" + objCommonVar.CurrentCompanyName + "' AS 'Company_Name', T6.PAY_PERIOD_CODE, "
            Qry += " '" + Max_date + "' as Max_date, '" + Min_date + "' as Min_date  from TSPL_GENERATE_SALARY_PAYHEADS T1 "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE T2 ON T2.EMP_CODE = T1.EMP_CODE  AND T1.SALARY_GENERATION_CODE = T2.SALARY_GENERATION_CODE "
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER  T3 ON T3.EMP_CODE = T1.EMP_CODE  "
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_STATUS T4 ON T4.EMP_CODE = T1.EMP_CODE  and t4.REVISION_NO = ( select MAX(REVISION_NO) as 'Revision_no ' from TSPL_EMPLOYEE_STATUS where TSPL_EMPLOYEE_STATUS.EMP_CODE = t1.EMP_CODE )"
            Qry += " LEFT OUTER JOIN TSPL_PAYHEAD_MASTER  T5 ON T5.PAY_HEAD_CODE  = T1.PAY_HEAD_CODE  "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY T6 ON T6.SALARY_GENERATION_CODE = T1.SALARY_GENERATION_CODE  "
            Qry += " LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER T7 ON T7.PAY_PERIOD_CODE   = T6.PAY_PERIOD_CODE   "
            Qry += " WHERE (T7.DATE_FROM  BETWEEN CONVERT(DATE, '" + Min_date + "',103)  AND CONVERT(DATE, '" + Max_date + "',103))  AND  (T7.DATE_TO BETWEEN CONVERT(DATE, '" + Min_date + "',103)  AND CONVERT(DATE, '" + Max_date + "',103)) AND   T5.SUB_HEAD_TYPE  = 'EPF' and T3.ISPF =1 AND T1.EMP_CODE IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") ORDER BY T1.EMP_CODE,T7.DATE_FROM  "

            Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If DT.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmcrsytal As New frmCrystalReportViewer()
                frmcrsytal.funreport(CrystalReportFolder.HRPayroll, DT, "crptFormA3", "Form A3")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub cboYear_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboYear.Enter
        Dim qry As String = " SELECT (case when month(MIN(DATE_FROM)) > 4 then (year(MIN(DATE_FROM)))-1 else year(MIN(DATE_FROM)) end ) AS 'MIN', "
        qry += " (case when month (MAX((DATE_TO))) > 4 then (year(MIN(DATE_FROM)))+1 else year(MIN(DATE_FROM))end ) AS  'MAX' "
        qry += " FROM TSPL_PAYPERIOD_MASTER "
        Dim DT As DataTable = clsDBFuncationality.GetDataTable(qry)
        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
            Dim min As Int16 = Convert.ToInt16(DT.Rows(0)("MIN"))
            Dim max As Int16 = Convert.ToInt16(DT.Rows(0)("MAX"))
            Dim DT_CBO As New DataTable()
            DT_CBO.Columns.Add("Select", GetType(String))
            Dim dr As DataRow
            For ii As Int16 = min To max - 1
                dr = DT_CBO.NewRow()
                dr("Select") = ii.ToString() + "-" + (ii + 1).ToString()
                DT_CBO.Rows.Add(dr)
            Next
            DT_CBO.AcceptChanges()
            isLoadEmp = False
            cboYear.DataSource = DT_CBO
            cboYear.DisplayMember = "Select"
            cboYear.ValueMember = "Select"
            isLoadEmp = True
            cboYear_SelectedValueChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub cboYear_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboYear.SelectedValueChanged
        If clsCommon.myLen(cboYear.SelectedValue) > 0 AndAlso isLoadEmp Then
            Dim str As String = cboYear.SelectedValue
            Dim ind As Int16 = str.IndexOf("-")
            Max_date = clsCommon.myCDate("31/03/" + str.Substring(ind + 1, 4))
            Min_date = clsCommon.myCDate("01/04/" + str.Substring(0, 4))

            Qry = ""
            Qry += " SELECT DISTINCT TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name "
            Qry += " from TSPL_GENERATE_SALARY_PAYHEADS "
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE =TSPL_EMPLOYEE_MASTER .EMP_CODE "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY T1 ON T1.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE "
            Qry += " LEFT OUTER JOIN TSPL_PAYHEAD_MASTER  T5 ON T5.PAY_HEAD_CODE  = TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE "
            Qry += " LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER T6 ON T6.PAY_PERIOD_CODE   = T1.PAY_PERIOD_CODE "
            Qry += " WHERE (T6.DATE_FROM  BETWEEN CONVERT(DATE, '01/04/2013',103)  AND CONVERT(DATE, '31/03/2014',103))  AND "
            Qry += " (T6.DATE_TO BETWEEN CONVERT(DATE, '" + Min_date + "',103)  AND CONVERT(DATE, '" + Max_date + "',103)) AND"
            Qry += " T5.SUB_HEAD_TYPE  = 'EPF' and TSPL_EMPLOYEE_MASTER.ISPF =1 "
            Qry += " ORDER BY TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE"


            cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
            cbgLocation.ValueMember = "EMP_CODE"
            cbgLocation.DisplayMember = "Emp_Name"
        End If
    End Sub
End Class
