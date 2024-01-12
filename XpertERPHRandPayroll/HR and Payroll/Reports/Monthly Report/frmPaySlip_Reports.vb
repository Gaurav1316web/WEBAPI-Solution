'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmPaySlip_Reports
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

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
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPaySlip_Reports)
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
    Sub LoadData()

        qry = ""
        qry = " SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE, TSPL_EMPLOYEE_MASTER .Emp_Name FROM TSPL_GENERATE_SALARY "
        qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE "
        Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE= TSPL_EMPLOYEE_MASTER .EMP_CODE "
        Qry += " Left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= TSPL_GENERATE_SALARY.Location_Code"
        Qry += " where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtFromPP.Value + "' "
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            Qry += " and TSPL_LOCATION_MASTER.Location_Code='" & FndLocationCode.Value & "' "
        End If

        Qry += " ORDER BY TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE "

        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "EMP_CODE"
        cbgLocation.DisplayMember = "Emp_Name"
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)

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
            Dim LocAdress As String = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(City_Code ,'')='' Then '' else '-'+CONVERT(varchar, City_Code) End End End as Location_Address from TSPL_LOCATION_MASTER where Location_Code ='" + FndLocationCode.Value + "'")
            Dim Qry As String = ""
            Qry = ""
            Qry += " SELECT T1.EMP_CODE ,T1.Emp_Name,T1.PF_NO, T1.ESI_NO ,T2.PRESENT_DAYS, T2.PAYABLE_DAYS ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME  ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_GRADE_MASTER.GRADE_NAME , T1.BANK_ACC_NO,TSPL_BANK_MASTER.DESCRIPTION as 'Bank_name',Logo_Img,T1.Location_Code,Location_Desc FROM TSPL_EMPLOYEE_MASTER T1"
            Qry += " LEFT OUTER JOIN (SELECT TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE,TSPL_GENERATE_SALARY_ATTENDANCE.PRESENT_DAYS,TSPL_GENERATE_SALARY_ATTENDANCE.PAYABLE_DAYS FROM TSPL_GENERATE_SALARY_ATTENDANCE LEFT OUTER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE "
            Qry += " WHERE TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  AND TSPL_GENERATE_SALARY.PAY_PERIOD_CODE   = '" + txtFromPP.Value + "') AS T2 ON T2.EMP_CODE =T1.EMP_CODE "
            Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=T1.Location_Code"
            Qry += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=T1.Comp_Code "
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
                dtFinal.Columns.Add("Logo_Img", GetType(Byte()))
                dtFinal.Columns.Add("Location_Code", GetType(String))
                dtFinal.Columns.Add("Location_Desc", GetType(String))
                dtFinal.Columns.Add("ErPayHead_name", GetType(String))
                dtFinal.Columns.Add("ErPayHead_Rate", GetType(Double))
                dtFinal.Columns.Add("ErPayHead_amt", GetType(Double))
                dtFinal.Columns.Add("DuPayHead_name", GetType(String))
                dtFinal.Columns.Add("DuPayHead_amt", GetType(Double))
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

                        DrFinal.Item("Emp_Id") = clsCommon.myCstr(DrHead("EMP_CODE"))
                        DrFinal.Item("Emp_Name") = clsCommon.myCstr(DrHead("Emp_Name"))
                        DrFinal.Item("CompanyName") = objCommonVar.CurrentCompanyName
                        DrFinal.Item("CompanyAdd") = LocAdress

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
                        DrFinal.Item("Location_Code") = clsCommon.myCstr(DrHead("Location_Code"))
                        DrFinal.Item("Location_Desc") = clsCommon.myCstr(DrHead("Location_Desc"))
                        If clsCommon.myLen(DrHead("Logo_Img")) > 0 Then
                            DrFinal.Item("Logo_Img") = DrHead("Logo_Img")


                        End If
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
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crptPaySlip", "Employee Pay Slip Report")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
        FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If

    End Sub

    Function Check()

        If clsCommon.myLen(clsCommon.myCstr(txtFromPP.Value)) <= 0 Then
            myMessages.blankValue("Select Pay Period ")
            txtFromPP.Focus()
            txtFromPP.Select()
            Errorcontrol.SetError(txtFromPP, "Select Pay Period ")
            Return False
        Else
            Errorcontrol.ResetError(FndLocationCode)
        End If

        If clsCommon.myLen(clsCommon.myCstr(FndLocationCode.Value)) <= 0 Then
            myMessages.blankValue("Location ")
            FndLocationCode.Focus()
            FndLocationCode.Select()
            Errorcontrol.SetError(FndLocationCode, "Location ")
            Return False
        Else
            Errorcontrol.ResetError(FndLocationCode)
        End If

        Return True
    End Function
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Check() Then
            LoadData()
        End If
    End Sub
End Class
