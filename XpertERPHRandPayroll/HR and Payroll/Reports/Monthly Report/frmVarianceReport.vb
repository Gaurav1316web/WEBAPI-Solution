'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmVarianceReport
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

    Private Sub frmVarianceReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmVarianceReport)
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
            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Pay Period.", Me.Text)
                Return
            End If

            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select AtLeast Single Employee Or Select All", Me.Text)
                Return
            End If

            Dim Qry As String = ""
            Qry += "SELECT TOP 1 T1.PAY_PERIOD_CODE,T1.DATE_FROM FROM TSPL_PAYPERIOD_MASTER  T1 "
            Qry += "WHERE T1.DATE_FROM<(SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & txtFromPP.Value & "') "
            Qry += "ORDER BY T1.DATE_FROM DESC"
            Dim dt_PP As DataTable
            dt_PP = clsDBFuncationality.GetDataTable(Qry)
            If dt_PP.Rows.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Previous Pay Period does not exist.", Me.Text)
                Return
            End If

            Qry = ""
            Qry += " SELECT T1.EMP_CODE ,T1.Emp_Name,T1.PF_NO, T1.ESI_NO ,T2.PRESENT_DAYS, T2.PAYABLE_DAYS ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME  ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_GRADE_MASTER.GRADE_NAME , T1.BANK_ACC_NO,TSPL_BANK_MASTER.DESCRIPTION as 'Bank_name' FROM TSPL_EMPLOYEE_MASTER T1"
            Qry += " LEFT OUTER JOIN (SELECT TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE,TSPL_GENERATE_SALARY_ATTENDANCE.PRESENT_DAYS,TSPL_GENERATE_SALARY_ATTENDANCE.PAYABLE_DAYS FROM TSPL_GENERATE_SALARY_ATTENDANCE LEFT OUTER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE "
            Qry += " WHERE TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  AND TSPL_GENERATE_SALARY.PAY_PERIOD_CODE   = '" + txtFromPP.Value + "') AS T2 ON T2.EMP_CODE =T1.EMP_CODE "
            Qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =T1.Designation "
            Qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE  =T1.DEPARTMENT_CODE "
            Qry += " left outer join TSPL_GRADE_MASTER on TSPL_GRADE_MASTER.GRADE_CODE   =T1.GRADE_CODE"
            Qry += " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE     =T1.BANK_CODE"
            If cbgLocation.CheckedValue.Count > 0 Then
                Qry += " WHERE T1.EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)


            If Hader_Info.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim dtFinal As DataTable = New DataTable
                dtFinal.Columns.Add("Emp_Code", GetType(String))
                dtFinal.Columns.Add("Emp_Name", GetType(String))
                dtFinal.Columns.Add("Company_Name", GetType(String))
                dtFinal.Columns.Add("Company_Address", GetType(String))
                dtFinal.Columns.Add("Pay_Period_Code", GetType(String))
                dtFinal.Columns.Add("Prev_Pay_Period", GetType(String))
                dtFinal.Columns.Add("Curr_Pay_Period", GetType(String))
                dtFinal.Columns.Add("Prev_PP_EHeads", GetType(String))
                dtFinal.Columns.Add("Prev_PP_EAmount", GetType(Double))
                dtFinal.Columns.Add("Curr_PP_EHeads", GetType(String))
                dtFinal.Columns.Add("Curr_PP_EAmount", GetType(Double))

                dtFinal.Columns.Add("Prev_PP_DHeads", GetType(String))
                dtFinal.Columns.Add("Prev_PP_DAmount", GetType(Double))
                dtFinal.Columns.Add("Curr_PP_DHeads", GetType(String))
                dtFinal.Columns.Add("Curr_PP_DAmount", GetType(Double))

                
                dtFinal.AcceptChanges()

                Dim DrFinal As DataRow = dtFinal.NewRow()
                Dim DrDT_EPrev As DataRow
                Dim DrDT_ECurr As DataRow

                Dim DrDT_DPrev As DataRow
                Dim DrDT_DCurr As DataRow

                For Each DrHead As DataRow In Hader_Info.Rows

                    Qry = ""
                    Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    Qry += " INNER JOIN ("
                    Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    Qry += " WHERE(1 = 1)"
                    Qry += " and T1.ISEARNING=1 "
                    Qry += " AND T2.PAY_PERIOD_CODE='" + dt_PP.Rows(0).Item("Pay_Period_Code") + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("EMP_CODE") + "' "
                    Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    Dim dt_EPrev As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    Qry = ""
                    Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    Qry += " INNER JOIN ("
                    Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    Qry += " WHERE(1 = 1)"
                    Qry += " and T1.ISEARNING=1 "
                    Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("EMP_CODE") + "' "
                    Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    Dim dt_ECurr As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    Qry = ""
                    Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    Qry += " INNER JOIN ("
                    Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    Qry += " WHERE(1 = 1)"
                    Qry += " and T1.ISEARNING=0 "
                    Qry += " AND T2.PAY_PERIOD_CODE='" + dt_PP.Rows(0).Item("Pay_Period_Code") + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("EMP_CODE") + "' "
                    Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    Dim dt_DPrev As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    Qry = ""
                    Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    Qry += " INNER JOIN ("
                    Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    Qry += " WHERE(1 = 1)"
                    Qry += " and T1.ISEARNING=0 "
                    Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("EMP_CODE") + "' "
                    Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    Dim dt_DCurr As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    'Dim arr(3) As Integer
                    Dim lst As New List(Of Integer)(New Integer() {dt_EPrev.Rows.Count, dt_ECurr.Rows.Count, dt_DPrev.Rows.Count, dt_DCurr.Rows.Count})
                    lst.Add(dt_EPrev.Rows.Count)
                    lst.Add(dt_ECurr.Rows.Count)
                    lst.Add(dt_DPrev.Rows.Count)
                    lst.Add(dt_DCurr.Rows.Count)
                    lst.Sort()
                    Dim Counter As Int16 = lst.Item(lst.Count - 1)


                    For ii As Int16 = 0 To Counter

                        If dt_EPrev.Rows.Count > ii Then
                            DrDT_EPrev = dt_EPrev.Rows(ii)
                        Else
                            DrDT_EPrev = dt_EPrev.NewRow()
                        End If

                        If dt_ECurr.Rows.Count > ii Then
                            DrDT_ECurr = dt_ECurr.Rows(ii)
                        Else
                            DrDT_ECurr = dt_ECurr.NewRow()
                        End If

                        If dt_DPrev.Rows.Count > ii Then
                            DrDT_DPrev = dt_DPrev.Rows(ii)
                        Else
                            DrDT_DPrev = dt_DPrev.NewRow()
                        End If

                        If dt_DCurr.Rows.Count > ii Then
                            DrDT_DCurr = dt_DCurr.Rows(ii)
                        Else
                            DrDT_DCurr = dt_DCurr.NewRow()
                        End If

                        DrFinal = dtFinal.NewRow()

                        DrFinal.Item("Emp_Code") = clsCommon.myCstr(DrHead("EMP_CODE"))
                        DrFinal.Item("Emp_Name") = clsCommon.myCstr(DrHead("Emp_Name"))
                        DrFinal.Item("Company_Name") = objCommonVar.CurrentCompanyName
                        DrFinal.Item("Company_Address") = objCommonVar.CurrLocationCode
                        DrFinal.Item("Prev_Pay_period") = dt_PP.Rows(0).Item("Pay_Period_Code")
                        DrFinal.Item("Curr_Pay_period") = Me.txtFromPP.Value

                        
                        If clsCommon.myLen(DrDT_EPrev("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("Prev_PP_EHeads") = clsCommon.myCstr(DrDT_EPrev("PAY_HEAD_NAME"))
                            DrFinal.Item("Prev_PP_EAmount") = clsCommon.myCdbl(DrDT_EPrev("ACTUAL_AMOUNT"))
                        End If
                        If clsCommon.myLen(DrDT_ECurr("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("Curr_PP_EHeads") = clsCommon.myCstr(DrDT_ECurr("PAY_HEAD_NAME"))
                            DrFinal.Item("Curr_PP_EAmount") = clsCommon.myCdbl(DrDT_ECurr("ACTUAL_AMOUNT"))
                        End If

                        If clsCommon.myLen(DrDT_DPrev("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("Prev_PP_DHeads") = clsCommon.myCstr(DrDT_DPrev("PAY_HEAD_NAME"))
                            DrFinal.Item("Prev_PP_DAmount") = clsCommon.myCdbl(DrDT_DPrev("ACTUAL_AMOUNT"))
                        End If
                        If clsCommon.myLen(DrDT_DCurr("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("Curr_PP_DHeads") = clsCommon.myCstr(DrDT_DCurr("PAY_HEAD_NAME"))
                            DrFinal.Item("Curr_PP_DAmount") = clsCommon.myCdbl(DrDT_DCurr("ACTUAL_AMOUNT"))
                        End If

                        dtFinal.Rows.Add(DrFinal)
                    Next
                Next
                dtFinal.AcceptChanges()
                Dim frmcrsytal As New frmCrystalReportViewer()
                frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpVarianceReport", "Variance Report")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
