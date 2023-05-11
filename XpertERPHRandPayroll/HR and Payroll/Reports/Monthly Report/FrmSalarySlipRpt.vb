
'--17/04/2014--form Add By- Meenesh ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class FrmSalarySlipRpt
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

    Private Sub frmPaySlip_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSalarySlipRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
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
                common.clsCommon.MyMessageBoxShow("Please Select Pay Period.")
                Return
            End If

            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Employee Or Select All")
                Return
            End If

            Dim Qry As String = ""
            Qry = ""
            Qry += "  SELECT T1.EMP_CODE As [Code] ,T1.Emp_Name as [Name],T1.PF_NO as [PFNo], T1.ESI_NO  as [ESINo], "
            Qry += " t1.FATHERS_NAME as [FathersName],T2.Comp_Name as [CompanyName],(Select t2.Add1+Case When ISNULL(t2.Add2,'')='' Then '' "
            Qry += " else ', '+t2.Add2+ Case When ISNULL(t2.Add3,'')='' Then '' Else ', '+t2.Add3+ Case When ISNULL(t2.Pincode,'')='' Then '' else "
            Qry += "'-'+CONVERT(varchar, t2.Pincode) End End End from tspl_company_Master t2) as [CompanyAddress] from TSPL_EMPLOYEE_MASTER T1"
            Qry += " left Outer join tspl_company_Master T2 on T2.Comp_Code =T1.Comp_Code  "
            Qry += "LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE T6 ON T6.EMP_CODE =T1.EMP_CODE "
            Qry += "inner JOIN TSPL_GENERATE_SALARY T7 on T7.SALARY_GENERATION_CODE =T6.SALARY_GENERATION_CODE "

            If cbgLocation.CheckedValue.Count > 0 Then
                Qry += " WHERE T1.EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)


            If Hader_Info.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                Dim dtFinal As DataTable = New DataTable
                dtFinal.Columns.Add("Code", GetType(String))
                dtFinal.Columns.Add("Name", GetType(String))
                dtFinal.Columns.Add("FathersName", GetType(String))
                dtFinal.Columns.Add("CompanyName", GetType(String))
                dtFinal.Columns.Add("CompanyAddress", GetType(String))
                dtFinal.Columns.Add("PFNo", GetType(String))
                dtFinal.Columns.Add("ESINo", GetType(String))
                'dtFinal.Columns.Add("Present Days", GetType(Int16))
                'dtFinal.Columns.Add("Absent Days", GetType(Int16))
                'dtFinal.Columns.Add("Holiday Days", GetType(Int16))
                'dtFinal.Columns.Add("Leave Days", GetType(Int16))
                'dtFinal.Columns.Add("Period Code", GetType(String))
                'dtFinal.Columns.Add("Name of Month", GetType(String))
                
                dtFinal.Columns.Add("ErPayHead_name", GetType(String))
                dtFinal.Columns.Add("ErPayHead_Rate", GetType(Double))
                dtFinal.Columns.Add("ErPayHead_amt", GetType(Double))
                dtFinal.Columns.Add("DuPayHead_name", GetType(String))
                dtFinal.Columns.Add("DuPayHead_amt", GetType(Double))
                dtFinal.AcceptChanges()

                Dim DrFinal As DataRow = dtFinal.NewRow()
                Dim DrDT As DataRow
                Dim DrDT1 As DataRow
                Dim DrDT2 As DataRow

                For Each DrHead As DataRow In Hader_Info.Rows


                    Qry = ""
                    Qry += "select T3.PRESENT_DAYS as [Present Days],T4.PAY_PERIOD_CODE ,T3.ABSENT_DAYS as [Absent Days],T3.HOLIDAY_DAYS as [Holiday Days] ,T3.LEAVE_DAYS as [Leave Days], "
                    Qry += "T4.PAY_PERIOD_CODE as [Period Code],T4.GENERATE_REMARKS as [Name of Month]  from TSPL_GENERATE_SALARY_ATTENDANCE T3 left outer  join  "
                    Qry += "TSPL_GENERATE_SALARY  T4 on T4.SALARY_GENERATION_CODE =T3.SALARY_GENERATION_CODE  inner join TSPL_EMPLOYEE_MASTER t1  "
                    Qry += "on t1.EMP_CODE =T3.EMP_CODE where T4.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' and T3.EMP_CODE ='" + DrHead("Code") + "' "
                    Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(Qry)



                    Qry = ""
                    Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    Qry += " INNER JOIN ("
                    Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    Qry += " WHERE(1 = 1)"
                    Qry += " and T1.ISEARNING=1 "

                    Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' "
                    Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    Qry = ""
                    Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    Qry += " INNER JOIN ("
                    Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    Qry += " WHERE(1 = 1)"
                    Qry += " and T1.ISEARNING=0 "
                    Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' "
                    Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)


                    Dim Counter As Int16 = dt.Rows.Count
                    If dt1.Rows.Count > dt.Rows.Count Then
                        Counter = dt1.Rows.Count
                    End If

                    For ii As Int16 = 0 To Counter

                        If dt3.Rows.Count > ii Then
                            DrDT2 = dt3.Rows(ii)
                        Else
                            DrDT2 = dt3.NewRow()
                        End If
                        If dt.Rows.Count > ii Then
                            DrDT = dt.Rows(ii)
                        Else
                            DrDT = dt.NewRow()
                        End If

                        If dt1.Rows.Count > ii Then
                            DrDT1 = dt1.Rows(ii)
                        Else
                            DrDT1 = dt1.NewRow()
                        End If

                        DrFinal = dtFinal.NewRow()

                        DrFinal.Item("Code") = clsCommon.myCstr(DrHead("Code"))
                        DrFinal.Item("Name") = clsCommon.myCstr(DrHead("Name"))
                        DrFinal.Item("FathersName") = clsCommon.myCstr(DrHead("FathersName"))
                        DrFinal.Item("CompanyName") = objCommonVar.CurrentCompanyName
                        DrFinal.Item("CompanyAddress") = clsCommon.myCstr(DrHead("CompanyAddress"))
                        DrFinal.Item("PFNo") = clsCommon.myCstr(DrHead("PFNo"))
                        DrFinal.Item("ESINo") = clsCommon.myCstr(DrHead("ESINo"))
                       

                        If clsCommon.myLen(DrDT("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("ErPayHead_name") = clsCommon.myCstr(DrDT("PAY_HEAD_NAME"))
                            DrFinal.Item("ErPayHead_Rate") = clsCommon.myCdbl(DrDT("RATE_AMOUNT"))
                            DrFinal.Item("ErPayHead_amt") = clsCommon.myCdbl(DrDT("ACTUAL_AMOUNT"))
                        End If

                        If clsCommon.myLen(DrDT1("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("DuPayHead_name") = clsCommon.myCstr(DrDT1("PAY_HEAD_NAME"))
                            DrFinal.Item("DuPayHead_amt") = clsCommon.myCdbl(DrDT1("ACTUAL_AMOUNT"))
                        End If
                        'If clsCommon.myLen(DrDT2("PAY_PERIOD_CODE")) > 0 Then

                        '    DrFinal.Item("Present Days") = clsCommon.myCdbl(DrHead("Present Days"))
                        '    DrFinal.Item("Absent Days") = clsCommon.myCdbl(DrHead("Absent Days"))
                        '    DrFinal.Item("Holiday Days") = clsCommon.myCdbl(DrHead("Holiday Days"))
                        '    DrFinal.Item("Leave Days") = clsCommon.myCdbl(DrHead("Leave Days"))

                        '    DrFinal.Item("Period Code") = clsCommon.myCstr(DrHead("Period Code"))
                        '    DrFinal.Item("Name of Month") = clsCommon.myCstr(DrHead("Name of Month"))
                        'End If
                        dtFinal.Rows.Add(DrFinal)
                    Next

                Next
                dtFinal.AcceptChanges()

                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crptSalarySlip", "Employee Salary Slip Report")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class


