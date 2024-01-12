
'--01/08/2013--form Add By- Meenesh vashishtha ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class FrmNewSalCertificate
    'Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PrintData()
    End Sub

    Private Sub FrmNewSalCertificate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    'Private Sub SetUserMgmtNew()
    '    'MyBase.SetUserMgmt(clsUserMgtCode.frmSalaryCertificate)
    '    If Not (MyBase.isReadFlag) Then
    '        common.clsCommon.MyMessageBoxShow("Permission Denied")
    '        Me.Close()
    '        Exit Function
    '    End If
    '    btnSave.Visible = MyBase.isModifyFlag
    'End Function

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
            Qry += " SELECT *,coalesce(PF.ACTUAL_AMOUNT,0) AS PF,coalesce(ProfTax.ACTUAL_AMOUNT,0) AS ProfTax ,coalesce(LIC.ACTUAL_AMOUNT,0) AS LIC"
            Qry += " FROM (SELECT T1.EMP_CODE as [code] ,T1.Emp_Name as [Name],CONVERT(varchar,T1.joining_date,105) AS JOINING_DATE,t6.bonus_amount,"
            Qry += " TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME , TSPL_DESIGNATION_MASTER.Designation_Desc,(case when T1.SEX ='male' then 'He' else 'She' end)as Gender FROM TSPL_EMPLOYEE_MASTER T1   "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE ON TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =T1.EMP_CODE INNER  JOIN "
            Qry += " TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE  "
            Qry += "  LEFT OUTER  join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =T1.Designation  left outer join TSPL_DEPARTMENT_MASTER on "
            Qry += " TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE  =T1.DEPARTMENT_CODE   left outer join TSPL_EMPBONUS_DETAIL T6 on t6.emp_code=t1.emp_code  WHERE T1.EMP_CODE "
            Qry += "  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") AND TSPL_GENERATE_SALARY.PAY_PERIOD_CODE   ='" + txtFromPP.Value + "'  )as TSPL_EMPLOYEE_MASTER LEFT JOIN("
            Qry += " SELECT EMP_CODE,ACTUAL_AMOUNT FROM TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += " INNER join TSPL_PAYHEAD_MASTER  on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE"
            Qry += " WHERE PAY_PERIOD_CODE ='" + txtFromPP.Value + "'  AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") AND "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT > 0  AND TSPL_PAYHEAD_MASTER.ISEARNING=0 AND TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='EPF'and "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE ='EPF' ) AS PF ON TSPL_EMPLOYEE_MASTER.code =PF.EMP_CODE"
            Qry += " LEFT JOIN("
            Qry += " SELECT EMP_CODE,ACTUAL_AMOUNT FROM TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += " INNER join TSPL_PAYHEAD_MASTER  on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE"
            Qry += " WHERE PAY_PERIOD_CODE ='" + txtFromPP.Value + "'  AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") AND TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT >0  "
            Qry += " AND TSPL_PAYHEAD_MASTER.ISEARNING=0 AND TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='ESI' and TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE ='COESI') AS ProfTax ON TSPL_EMPLOYEE_MASTER.code=ProfTax.EMP_CODE"
           
            Qry += " LEFT JOIN(SELECT EMP_CODE,ACTUAL_AMOUNT FROM TSPL_GENERATE_SALARY_PAYHEADS INNER JOIN TSPL_GENERATE_SALARY ON "
            Qry += " TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE"
            Qry += " INNER join TSPL_PAYHEAD_MASTER  on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE"
            Qry += " WHERE PAY_PERIOD_CODE ='" + txtFromPP.Value + "'  AND TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") AND TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT >0  "
            Qry += " AND TSPL_PAYHEAD_MASTER.ISEARNING=0 AND TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='LIC' and TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE ='LIC') AS LIC ON TSPL_EMPLOYEE_MASTER.code=LIC.EMP_CODE"

            If cbgLocation.CheckedValue.Count > 0 Then
                Qry += " WHERE TSPL_EMPLOYEE_MASTER.CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
            End If
            Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)


            If Hader_Info.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim dtFinal As DataTable = New DataTable
                dtFinal.Columns.Add("code", GetType(String))
                dtFinal.Columns.Add("Name", GetType(String))
                dtFinal.Columns.Add("CompanyName", GetType(String))
                dtFinal.Columns.Add("CompanyAdd", GetType(String))
                dtFinal.Columns.Add("JOINING_DATE", GetType(Date))
                dtFinal.Columns.Add("Designation", GetType(String))
                dtFinal.Columns.Add("Department", GetType(String))
                dtFinal.Columns.Add("Earning", GetType(String))
                dtFinal.Columns.Add("Deduction", GetType(String))
                dtFinal.Columns.Add("ErPayHead_name", GetType(String))
                dtFinal.Columns.Add("ErPayHead_amt", GetType(Double))
                dtFinal.Columns.Add("LIC", GetType(Double))
                dtFinal.Columns.Add("PF", GetType(Double))
                dtFinal.Columns.Add("ProfTax", GetType(Double))
                dtFinal.Columns.Add("Bonus", GetType(Double))
                dtFinal.Columns.Add("PAY_PERIOD_CODE", GetType(String))
                dtFinal.Columns.Add("Gender", GetType(String))
                dtFinal.AcceptChanges()

                Dim DrFinal As DataRow = dtFinal.NewRow()
                Dim DrDT As DataRow

                For Each DrHead As DataRow In Hader_Info.Rows

                    Qry = ""
                    Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    Qry += " INNER JOIN ("
                    Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    Qry += " WHERE(1 = 1)"
                    Qry += " and T1.ISEARNING=1 "
                    Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("code") + "' "

                    Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    Qry = ""
                    'Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    'Qry += " INNER JOIN ("
                    'Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    'Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    'Qry += " WHERE(1 = 1)"
                    'Qry += " and T1.ISEARNING=0 "
                    'Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    'Qry += " AND T2.EMP_CODE ='" + DrHead("code") + "'"
                    'Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    ' Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)


                    Dim Counter As Int16 = dt.Rows.Count
                    'If dt1.Rows.Count > dt.Rows.Count Then
                    '    Counter = dt1.Rows.Count
                    'End If

                    For ii As Int16 = 0 To Counter

                        If dt.Rows.Count > ii Then
                            DrDT = dt.Rows(ii)
                        Else
                            DrDT = dt.NewRow()
                        End If

                        'If dt1.Rows.Count > ii Then
                        '    DrDT1 = dt1.Rows(ii)
                        'Else
                        '    DrDT1 = dt1.NewRow()
                        'End If

                        DrFinal = dtFinal.NewRow()
                        DrFinal.Item("Code") = clsCommon.myCstr(DrHead("CODE"))
                        DrFinal.Item("Name") = clsCommon.myCstr(DrHead("Name"))
                        DrFinal.Item("CompanyName") = objCommonVar.CurrentCompanyName
                        DrFinal.Item("CompanyAdd") = objCommonVar.CurrLocationCode
                        DrFinal.Item("PAY_PERIOD_CODE") = clsCommon.myCstr(Me.txtFromPP.Value)
                        DrFinal.Item("JOINING_DATE") = clsCommon.GetPrintDate(DrHead("JOINING_DATE"), "dd/MMM/yyyy")
                        DrFinal.Item("Bonus") = clsCommon.myCdbl(DrHead("Bonus_Amount"))
                        DrFinal.Item("Gender") = clsCommon.myCstr(DrHead("Gender"))
                        DrFinal.Item("Designation") = clsCommon.myCstr(DrHead("Designation_Desc"))
                        DrFinal.Item("Department") = clsCommon.myCstr(DrHead("DEPARTMENT_NAME"))
                        DrFinal.Item("LIC") = clsCommon.myCstr(DrHead("LIC"))
                        DrFinal.Item("PF") = clsCommon.myCstr(DrHead("PF"))
                        DrFinal.Item("ProfTax") = clsCommon.myCstr(DrHead("ProfTax"))

                        If clsCommon.myLen(DrDT("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("ErPayHead_name") = clsCommon.myCstr(DrDT("PAY_HEAD_NAME"))
                            DrFinal.Item("ErPayHead_amt") = clsCommon.myCdbl(DrDT("ACTUAL_AMOUNT"))
                        End If

                        'If clsCommon.myLen(DrDT1("PAY_HEAD_NAME")) > 0 Then
                        '    DrFinal.Item("DuPayHead_name") = clsCommon.myCstr(DrDT1("PAY_HEAD_NAME"))
                        '    DrFinal.Item("DuPayHead_amt") = clsCommon.myCdbl(DrDT1("ACTUAL_AMOUNT"))
                        'End If
                        dtFinal.Rows.Add(DrFinal)
                    Next
                Next
                dtFinal.AcceptChanges()

                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpSalCertificate", "Salary Certificate")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class




