'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmSalaryCertificate
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

    Private Sub frmSalaryCertificate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalaryCertificate)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
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

            Dim header1 As String = "Salary Certificate for the month of " & Me.txtFromPP.Value & ""
            Dim header2 As String = ""

            Dim Qry As String = ""
            Qry = ""
            Qry += " SELECT T1.EMP_CODE ,T1.Emp_Name,CONVERT(date,T1.joining_date,103) AS JOINING_DATE,T1.PF_NO, T1.ESI_NO ,T2.PRESENT_DAYS, T2.PAYABLE_DAYS ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME  ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_GRADE_MASTER.GRADE_NAME , T1.BANK_ACC_NO,TSPL_BANK_MASTER.DESCRIPTION as 'Bank_name' FROM TSPL_EMPLOYEE_MASTER T1"
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
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                Dim dtFinal As DataTable = New DataTable
                dtFinal.Columns.Add("Emp_Id", GetType(String))
                dtFinal.Columns.Add("Emp_Name", GetType(String))
                dtFinal.Columns.Add("CompanyName", GetType(String))
                dtFinal.Columns.Add("CompanyAdd", GetType(String))
                dtFinal.Columns.Add("PFNo", GetType(String))
                dtFinal.Columns.Add("ESINo", GetType(String))
                dtFinal.Columns.Add("PaidDays", GetType(Int16))
                dtFinal.Columns.Add("PresentDays", GetType(Int16))
                dtFinal.Columns.Add("Designation", GetType(String))
                dtFinal.Columns.Add("Department", GetType(String))
                dtFinal.Columns.Add("Grade", GetType(String))
                dtFinal.Columns.Add("ACNo", GetType(String))
                dtFinal.Columns.Add("BankName", GetType(String))
                dtFinal.Columns.Add("ErPayHead_name", GetType(String))
                dtFinal.Columns.Add("ErPayHead_Rate", GetType(Double))
                dtFinal.Columns.Add("ErPayHead_amt", GetType(Double))
                dtFinal.Columns.Add("DuPayHead_name", GetType(String))
                dtFinal.Columns.Add("DuPayHead_amt", GetType(Double))
                dtFinal.Columns.Add("PAY_PERIOD_CODE", GetType(String))
                dtFinal.Columns.Add("HEADER1", GetType(String))
                dtFinal.Columns.Add("HEADER2", GetType(String))
                dtFinal.AcceptChanges()

                Dim DrFinal As DataRow = dtFinal.NewRow()
                Dim DrDT As DataRow
                Dim DrDT1 As DataRow

                For Each DrHead As DataRow In Hader_Info.Rows

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
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

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
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)


                    Dim Counter As Int16 = dt.Rows.Count
                    If dt1.Rows.Count > dt.Rows.Count Then
                        Counter = dt1.Rows.Count
                    End If

                    For ii As Int16 = 0 To Counter

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
                        header2 = "This is to certify that " & clsCommon.myCstr(DrHead("Emp_Name")) & " is in employment with us  " _
                        & " since " & clsCommon.GetPrintDate(DrHead("JOINING_DATE"), "dd/MMM/yyyy") & " and is in receipt of following monthly emoluments."
                        DrFinal.Item("Emp_Id") = clsCommon.myCstr(DrHead("EMP_CODE"))
                        DrFinal.Item("Emp_Name") = clsCommon.myCstr(DrHead("Emp_Name"))
                        DrFinal.Item("CompanyName") = objCommonVar.CurrentCompanyName
                        DrFinal.Item("CompanyAdd") = objCommonVar.CurrLocationCode
                        DrFinal.Item("PAY_PERIOD_CODE") = clsCommon.myCstr(Me.txtFromPP.Value)
                        DrFinal.Item("HEADER1") = header1
                        DrFinal.Item("HEADER2") = header2


                        'DrFinal.Item("CompanyName") = "Tecxpert Software Pvt Ltd."
                        'DrFinal.Item("CompanyAdd") = "B-12, SEC-2, NOIDA ,UP , India"

                        DrFinal.Item("PFNo") = clsCommon.myCstr(DrHead("PF_NO"))
                        DrFinal.Item("ESINo") = clsCommon.myCstr(DrHead("ESI_NO"))
                        DrFinal.Item("PaidDays") = clsCommon.myCdbl(DrHead("PAYABLE_DAYS"))
                        DrFinal.Item("PresentDays") = clsCommon.myCdbl(DrHead("PRESENT_DAYS"))
                        DrFinal.Item("Designation") = clsCommon.myCstr(DrHead("Designation_Desc"))
                        DrFinal.Item("Department") = clsCommon.myCstr(DrHead("DEPARTMENT_NAME"))
                        DrFinal.Item("Grade") = clsCommon.myCstr(DrHead("GRADE_NAME"))
                        DrFinal.Item("ACNo") = clsCommon.myCstr(DrHead("BANK_ACC_NO"))
                        DrFinal.Item("BankName") = clsCommon.myCstr(DrHead("Bank_name"))

                        If clsCommon.myLen(DrDT("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("ErPayHead_name") = clsCommon.myCstr(DrDT("PAY_HEAD_NAME"))
                            DrFinal.Item("ErPayHead_Rate") = clsCommon.myCdbl(DrDT("RATE_AMOUNT"))
                            DrFinal.Item("ErPayHead_amt") = clsCommon.myCdbl(DrDT("ACTUAL_AMOUNT"))
                        End If

                        If clsCommon.myLen(DrDT1("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("DuPayHead_name") = clsCommon.myCstr(DrDT1("PAY_HEAD_NAME"))
                            DrFinal.Item("DuPayHead_amt") = clsCommon.myCdbl(DrDT1("ACTUAL_AMOUNT"))
                        End If
                        dtFinal.Rows.Add(DrFinal)
                    Next
                Next
                dtFinal.AcceptChanges()

                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpSalaryCertificate", "Salary Certificate")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
