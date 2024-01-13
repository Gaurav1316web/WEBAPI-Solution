Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine

Public Class RptKDILSalarySlip
    Inherits FrmMainTranScreen
    ''check in Sanjay 20200619
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim Print As Boolean = True
    Dim ChangeLeaveDescriptionOnSalarySlip As Boolean
    Dim SalarySlipLeaveStatusOnTheBasisOfCalendarYear As Boolean
#End Region
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Print = True
        PrintData()
    End Sub

    Private Sub frmPaySlip_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ChangeLeaveDescriptionOnSalarySlip = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ChangeLeaveDescriptionOnSalarySlip, clsFixedParameterCode.ChangeLeaveDescriptionOnSalarySlip, Nothing)) = 1, True, False)
        SalarySlipLeaveStatusOnTheBasisOfCalendarYear = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SalarySlipLeaveStatusOnTheBasisOfCalendarYear, clsFixedParameterCode.SalarySlipLeaveStatusOnTheBasisOfCalendarYear, Nothing)) = 1, True, False)
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptKDILSalarySlip)
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
        'Loaddata()
    End Sub
    '==========Update by preeti gupta Against ticket no[MIL/17/04/19-000064,BHA/07/05/19-000882]
    Sub PrintData()
        Try
            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Pay Period.", Me.Text)
                Return
            End If

            'If cbgLocation.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Employee Or Select All")
            '    Return
            'End If
            ''changes done by shivani against[BM00000008639]
            Dim onePagePrint As String = Nothing
            If ChkOnePagePrint.Checked = True Then
                onePagePrint = 1
            Else
                onePagePrint = 0
            End If

            'Ticket No  BHA/13/03/19-000842 sanjay, ADD Card No
            Dim Qry As String = ""
            Qry = ""
            Qry += "  SELECT T1.Bank_Branch,TSPL_Payment_MODE.NAME as Payment_Name, '" + clsCommon.myCstr(onePagePrint) + "' as onePagePrint,T1.EMP_CODE As [Code] ,T1.Emp_Name as [Name],T1.UIN_NO,T1.PF_NO as [PFNo], T1.ESI_NO  as [ESINo], "
            Qry += " t1.FATHERS_NAME as [FathersName],'" & objCommonVar.CurrentCompanyName & "' as [CompanyName],  t2.Add1+Case When ISNULL(t2.Add2,'')='' Then ''  else ', '+t2.Add2+ Case When ISNULL(t2.Add3,'')='' Then '' Else ', '+t2.Add3+ Case When ISNULL(t2.Pincode,'')='' Then '' else '-'+CONVERT(varchar, t2.Pincode) End End End  as [CompanyAddress],Logo_Img,Location_Desc as LocationDesc,"
            Qry += " T1.Location_Code as LocationCode, "
            Qry += " TSPL_DESIGNATION_MASTER.Designation_id ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE ,"
            Qry += " TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME ,TSPL_BANK_MASTER.BANK_CODE ,t1.Bank_Name,T1.BANK_ACC_NO ,T1.PAN_NO as Emp_Pan_no,"
            Qry += "  TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+"
            Qry += "  Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End as Location_Address"
            Qry += " ,t1.Joining_date ,t7.PAY_PERIOD_CODE,T6.PAYPERIOD_DAYS,HOLIDAY_DAYS,PAYABLE_DAYS,(T6.PAYPERIOD_DAYS-HOLIDAY_DAYS) as Working_days,T6.Present_Days as [Present Days],(T6.PAYPERIOD_DAYS-Present_Days-HOLIDAY_DAYS-LEAVE_DAYS-LOP_DAYS) as Weekly_off,D1.DEVISION_CODE AS DevCode,TSPL_LOCATION_MASTER.PF_NO as Firm_PF_No,isnull(Card_No,'') as Card_No from TSPL_EMPLOYEE_MASTER T1"
            Qry += " left Outer join tspl_company_Master T2 on T2.Comp_Code =T1.Comp_Code  "
            Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = T1.LOCATION_CODE "
            Qry += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code = TSPL_LOCATION_MASTER.State "
            Qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =t1.Designation "
            Qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =t1.DEPARTMENT_CODE "
            Qry += " left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =t1.BANK_CODE"
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE T6 ON T6.EMP_CODE =T1.EMP_CODE "
            Qry += " LEFT OUTER JOIN TSPL_DEVISION_MASTER D1 ON D1.DEVISION_CODE = T1.DEVISION_CODE  "
            Qry += "left outer join TSPL_Payment_MODE on TSPL_Payment_MODE.CODE=t1.payment_mode_new "
            Qry += " inner JOIN TSPL_GENERATE_SALARY T7 on T7.SALARY_GENERATION_CODE =T6.SALARY_GENERATION_CODE where T7.Pay_Period_Code='" & txtFromPP.Value & "'"

            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                Qry += " and T1.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                Qry += " and T1.Devision_Code  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
            End If

            If txtEmployeeMult.arrValueMember IsNot Nothing AndAlso txtEmployeeMult.arrValueMember.Count > 0 Then
                Qry += " and T1.EMP_CODE  in (" + clsCommon.GetMulcallString(txtEmployeeMult.arrValueMember) + ") "
            End If
            If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                Qry += " and T1.Department_Code  in (" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ") "
            End If

            Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)


            If Hader_Info.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim dtFinal As DataTable = New DataTable
                dtFinal.Columns.Add("Bank_Branch", GetType(String))
                dtFinal.Columns.Add("onePagePrint", GetType(String))
                dtFinal.Columns.Add("Code", GetType(String))
                dtFinal.Columns.Add("Name", GetType(String))
                dtFinal.Columns.Add("UIN_NO", GetType(String)) 'UIN_NO
                dtFinal.Columns.Add("FathersName", GetType(String))
                dtFinal.Columns.Add("CompanyName", GetType(String))
                dtFinal.Columns.Add("CompanyAddress", GetType(String))
                dtFinal.Columns.Add("PFNo", GetType(String))
                dtFinal.Columns.Add("ESINo", GetType(String))
                dtFinal.Columns.Add("Logo_Img", GetType(Byte()))
                dtFinal.Columns.Add("LocationCode", GetType(String))
                dtFinal.Columns.Add("LocationDesc", GetType(String))
                dtFinal.Columns.Add("DevCode", GetType(String))
                dtFinal.Columns.Add("Firm_PF_No", GetType(String))
                dtFinal.Columns.Add("Designation_id", GetType(String))
                dtFinal.Columns.Add("Designation_Desc", GetType(String))
                dtFinal.Columns.Add("DEPARTMENT_CODE", GetType(String))
                dtFinal.Columns.Add("DEPARTMENT_NAME", GetType(String))
                dtFinal.Columns.Add("BANK_CODE", GetType(String))
                dtFinal.Columns.Add("Bank_Name", GetType(String))
                dtFinal.Columns.Add("BANK_ACC_NO", GetType(String))

                dtFinal.Columns.Add("Emp_Pan_no", GetType(String))
                dtFinal.Columns.Add("Location_Address", GetType(String))
                dtFinal.Columns.Add("Joining_date", GetType(String))
                dtFinal.Columns.Add("PAY_PERIOD_CODE", GetType(String))
                dtFinal.Columns.Add("ErPayHead_name", GetType(String))
                dtFinal.Columns.Add("ErPayHead_Rate", GetType(Double))
                dtFinal.Columns.Add("ErPayHead_amt", GetType(Double))
                dtFinal.Columns.Add("ErArrHead_amt", GetType(Double))
                dtFinal.Columns.Add("DuPayHead_name", GetType(String))
                dtFinal.Columns.Add("DuPayHead_amt", GetType(Double))
                dtFinal.Columns.Add("Leave_Code", GetType(String))
                dtFinal.Columns.Add("AVAILED", GetType(String))
                dtFinal.Columns.Add("PAYPERIOD_DAYS", GetType(String))
                dtFinal.Columns.Add("HOLIDAY_DAYS", GetType(String))
                dtFinal.Columns.Add("PAYABLE_DAYS", GetType(String))
                dtFinal.Columns.Add("Working_days", GetType(String))
                dtFinal.Columns.Add("Present Days", GetType(String))
                dtFinal.Columns.Add("Weekly_off", GetType(String))
                dtFinal.Columns.Add("EL_Bal", GetType(String))
                dtFinal.Columns.Add("CL_Bal", GetType(String))
                dtFinal.Columns.Add("SL_Bal", GetType(String))
                dtFinal.Columns.Add("Card_No", GetType(String))
                dtFinal.Columns.Add("Payment_Name", GetType(String))
                dtFinal.Columns.Add("CL_Open", GetType(String))
                dtFinal.Columns.Add("CML_Open", GetType(String))
                dtFinal.Columns.Add("EL_Open", GetType(String))
                dtFinal.Columns.Add("HPL_Open", GetType(String))
                dtFinal.Columns.Add("OL_Open", GetType(String))
                dtFinal.Columns.Add("WECL_Open", GetType(String))
                dtFinal.Columns.Add("CL_Closeing", GetType(String))
                dtFinal.Columns.Add("CML_Closeing", GetType(String))
                dtFinal.Columns.Add("EL_Closeing", GetType(String))
                dtFinal.Columns.Add("HPL_Closeing", GetType(String))
                dtFinal.Columns.Add("OL_Closeing", GetType(String))
                dtFinal.Columns.Add("WECL_Closeing", GetType(String))
                dtFinal.AcceptChanges()

                Dim DrFinal As DataRow = dtFinal.NewRow()
                Dim DrDT As DataRow
                Dim DrDT1 As DataRow
                ' Dim DrDT2 As DataRow
                Dim DrDT3 As DataRow

                For Each DrHead As DataRow In Hader_Info.Rows
                    'Qry = ""
                    'Qry += "select T4.PAY_PERIOD_CODE ,T3.ABSENT_DAYS as [Absent Days],T3.HOLIDAY_DAYS as [Holiday Days] ,T3.LEAVE_DAYS as [Leave Days], "
                    'Qry += "T4.PAY_PERIOD_CODE as [Period Code],T4.GENERATE_REMARKS as [Name of Month]  from TSPL_GENERATE_SALARY_ATTENDANCE T3 left outer  join  "
                    'Qry += "TSPL_GENERATE_SALARY  T4 on T4.SALARY_GENERATION_CODE =T3.SALARY_GENERATION_CODE  inner join TSPL_EMPLOYEE_MASTER t1  "
                    'Qry += "on t1.EMP_CODE =T3.EMP_CODE where T4.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' and T3.EMP_CODE ='" + DrHead("Code") + "' "
                    'Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    Dim WD As Decimal = 0
                    Dim PD As Decimal = 0
                    Dim HD As Decimal = 0
                    Dim WF As Decimal = 0
                    WD = DrHead.Item("Present Days")
                    PD = DrHead.Item("PAYABLE_DAYS")
                    HD = DrHead.Item("HOLIDAY_DAYS")
                    WF = DrHead.Item("Weekly_off")

                    'Qry = ""
                    'Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,(T2.ACTUAL_AMOUNT-COALESCE(T2.ARREAR_AMT,0)) AS ACTUAL_AMOUNT,COALESCE(T2.ARREAR_AMT,0) AS Arrear_Amount FROM TSPL_PAYHEAD_MASTER T1 "
                    'Qry += " INNER JOIN ("
                    'Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    'Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"

                    'Qry += " WHERE(1 = 1)"
                    'Qry += " and T1.ISEARNING=1 "

                    'Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    'Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' "

                    'Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "

                    Dim dtAllocatedLeave As DataTable = clsDBFuncationality.GetDataTable("select TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE,TSPL_LEAVE_ALLOTMENTDETAIL.ALLOTED_LEAVE from TSPL_LEAVE_ALLOTMENTDETAIL left join TSPL_LEAVE_ALLOTMENT on TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE=TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE  WHERE TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE='" + clsCommon.myCstr(DrHead("Code")) + "' AND YEAR(TSPL_LEAVE_ALLOTMENT.ALLOTMENT_DATE)=(select year(date_to) from TSPL_PAYPERIOD_MASTER where pay_period_code= '" + txtFromPP.Value + "')")
                    Dim dtClosingLeave As DataTable = clsDBFuncationality.GetDataTable("select lv.EMP_CODE as empcode,lv.LEAVE_CODE , lv.ALLOTED-lv.AVAILED as Balance    from TSPL_VIEW_LEAVE_LEDGER as lv  left join TSPL_EMPLOYEE_MASTER emp on lv.EMP_CODE=emp.EMP_CODE  where  emp.RELIEVING_DATE is null    and (lv.ALLOTED >0 or lv.AVAILED>0) and  lv.EMP_CODE = '" + clsCommon.myCstr(DrHead("Code")) + "'  ")   'lv.PAY_PERIOD_CODE = '" + txtFromPP.Value + "'  and

                    Qry = ""
                    Qry += "select LINE_NO,head.PAY_HEAD_CODE,head.PAY_HEAD_NAME,EMP_CODE,RATE_AMOUNT,(ACTUAL_AMOUNT-head.Arrear_Amount) as ACTUAL_AMOUNT,COALESCE(detail.Arrear_Amount,head.Arrear_Amount) as Arrear_Amount from"
                    Qry += " (SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT,COALESCE(T2.ARREAR_AMT,0) as Arrear_Amount FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.LINE_NO,T1.PAY_HEAD_CODE,T1.EMP_CODE,T1.PAYABLE_AMOUNT as RATE_AMOUNT,T1.ACTUAL_AMOUNT,T1.ARREAR_AMT FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' and t1.SUB_HEAD_TYPE <> 'Arrear'  and ACTUAL_AMOUNT <> 0  )as head"
                    Qry += " Left Join (SELECT T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.ACTUAL_AMOUNT as  Arrear_Amount,ARREAR_TYPE  FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.LINE_NO,T1.PAY_HEAD_CODE,T1.EMP_CODE,T1. PAYABLE_AMOUNT as RATE_AMOUNT,T1.ACTUAL_AMOUNT,T1.ARREAR_AMT FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' and  t1.SUB_HEAD_TYPE = 'Arrear'  and ACTUAL_AMOUNT <> 0 )as detail on  detail.ARREAR_TYPE=head.PAY_HEAD_CODE"
                    Qry += "  ORDER BY EMP_CODE,LINE_NO"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    Qry = ""
                    Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                    Qry += " INNER JOIN ("
                    Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                    Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                    Qry += " WHERE(1 = 1)"
                    Qry += " and T1.ISEARNING=0 "
                    Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' "
                    Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "'   AND ACTUAL_AMOUNT <> 0 "
                    Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    Qry = ""
                    If ChangeLeaveDescriptionOnSalarySlip = True Then
                        Qry += "select 'P.P' as Leave_Code," & WD & " as AVAILED,0 as Balance Union All select 'H.D' as Leave_Code," & HD & " as AVAILED,0 as Balance  Union All select 'P.Holiday' as Leave_Code," & WF & " as AVAILED,0 as Balance Union All select Leave_Code,AVAILED,Balance from " + IIf(SalarySlipLeaveStatusOnTheBasisOfCalendarYear, "TSPL_FUN_LEAVE_STATUS_WO_PRV_BAL_CAL_YEAR", "TSPL_FUN_LEAVE_STATUS_WO_PRV_BAL") + " ('" + txtFromPP.Value + "') where Emp_Code='" + DrHead("Code") + "'  Union All select 'P.D' as Leave_Code," & PD & " as AVAILED,0 as Balance"
                    Else
                        Qry += "select 'W.D' as Leave_Code," & WD & " as AVAILED,0 as Balance Union All select 'H.D' as Leave_Code," & HD & " as AVAILED,0 as Balance  Union All select 'W.F' as Leave_Code," & WF & " as AVAILED,0 as Balance Union All select Leave_Code,AVAILED,Balance from " + IIf(SalarySlipLeaveStatusOnTheBasisOfCalendarYear, "TSPL_FUN_LEAVE_STATUS_WO_PRV_BAL_CAL_YEAR", "TSPL_FUN_LEAVE_STATUS_WO_PRV_BAL") + " ('" + txtFromPP.Value + "') where Emp_Code='" + DrHead("Code") + "'  Union All select 'P.D' as Leave_Code," & PD & " as AVAILED,0 as Balance"
                    End If

                    Dim dt4 As DataTable = clsDBFuncationality.GetDataTable(Qry)


                    '===============================
                    Dim MyDataRow As DataRow
                    For Each MyDataRow In dt4.Rows
                        If clsCommon.CompairString((MyDataRow("Leave_Code")), "CL") = CompairStringResult.Equal Then
                            Dim aaabc As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select convert (decimal(18,2), Casual_Leave) as Casual_Leave from TSPL_MONTHLY_ATTENDANCE_DETAIL left outer Join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_CODE = TSPL_MONTHLY_ATTENDANCE.MTA_CODE where TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' and TSPL_MONTHLY_ATTENDANCE_DETAIL.EMP_CODE = '" + clsCommon.myCstr(DrHead("Code")) + "' "))
                            MyDataRow("AVAILED") = aaabc.ToString("N2") 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select convert (decimal(18,2), Casual_Leave) as Casual_Leave from TSPL_MONTHLY_ATTENDANCE_DETAIL left outer Join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_CODE = TSPL_MONTHLY_ATTENDANCE.MTA_CODE where TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' and TSPL_MONTHLY_ATTENDANCE_DETAIL.EMP_CODE = '" + clsCommon.myCstr(DrHead("Code")) + "' "))
                            MyDataRow.AcceptChanges()
                        End If
                    Next
                    '==================================

                    'Qry = ""
                    'Qry += "select TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS,HOLIDAY_DAYS,PAYABLE_DAYS,(TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS-HOLIDAY_DAYS) as Working_days from TSPL_GENERATE_SALARY_ATTENDANCE left join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE where Emp_Code='" + DrHead("Code") + "' and PAY_PERIOD_CODE='" + txtFromPP.Value + "'"
                    'Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(Qry)


                    Dim Counter As Int16 = dt.Rows.Count
                    If dt1.Rows.Count > dt.Rows.Count Then
                        Counter = dt1.Rows.Count
                    End If
                    If dt4.Rows.Count > dt1.Rows.Count Then
                        Counter = dt4.Rows.Count
                    End If
                    For ii As Int16 = 0 To Counter

                        'If dt3.Rows.Count > ii Then
                        '    DrDT2 = dt3.Rows(ii)
                        'Else
                        '    DrDT2 = dt3.NewRow()
                        'End If
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
                        If dt4.Rows.Count > ii Then
                            DrDT3 = dt4.Rows(ii)
                        Else
                            DrDT3 = dt4.NewRow()
                        End If

                        DrFinal = dtFinal.NewRow()
                        DrFinal.Item("Bank_Branch") = clsCommon.myCstr(DrHead("Bank_Branch"))
                        DrFinal.Item("onePagePrint") = clsCommon.myCstr(DrHead("onePagePrint"))
                        DrFinal.Item("Code") = clsCommon.myCstr(DrHead("Code"))
                        DrFinal.Item("Name") = clsCommon.myCstr(DrHead("Name"))
                        DrFinal.Item("UIN_NO") = clsCommon.myCstr(DrHead("UIN_NO"))
                        DrFinal.Item("Card_No") = clsCommon.myCstr(DrHead("Card_No"))
                        DrFinal.Item("FathersName") = clsCommon.myCstr(DrHead("FathersName"))
                        DrFinal.Item("CompanyName") = clsCommon.myCstr(DrHead("CompanyName"))
                        DrFinal.Item("CompanyAddress") = clsCommon.myCstr(DrHead("CompanyAddress"))
                        DrFinal.Item("LocationCode") = clsCommon.myCstr(DrHead("LocationCode"))
                        DrFinal.Item("LocationDesc") = clsCommon.myCstr(DrHead("LocationDesc"))
                        DrFinal.Item("DevCode") = clsCommon.myCstr(DrHead("DevCode"))
                        DrFinal.Item("Firm_PF_No") = clsCommon.myCstr(DrHead("Firm_PF_No"))
                        DrFinal.Item("PAYPERIOD_DAYS") = clsCommon.myCstr(DrHead("PAYPERIOD_DAYS"))
                        DrFinal.Item("HOLIDAY_DAYS") = clsCommon.myCstr(DrHead("HOLIDAY_DAYS"))
                        DrFinal.Item("PAYABLE_DAYS") = clsCommon.myCstr(DrHead("PAYABLE_DAYS"))
                        DrFinal.Item("Working_days") = clsCommon.myCstr(DrHead("Working_days"))
                        DrFinal.Item("Present Days") = clsCommon.myCstr(DrHead("Present Days"))
                        DrFinal.Item("Weekly_off") = clsCommon.myCstr(DrHead("Weekly_off"))

                        If clsCommon.myLen(DrHead("Logo_Img")) > 0 Then
                            DrFinal.Item("Logo_Img") = DrHead("Logo_Img")


                        End If

                        DrFinal.Item("Designation_id") = clsCommon.myCstr(DrHead("Designation_id"))
                        DrFinal.Item("Designation_Desc") = clsCommon.myCstr(DrHead("Designation_Desc"))
                        DrFinal.Item("DEPARTMENT_CODE") = clsCommon.myCstr(DrHead("DEPARTMENT_CODE"))

                        DrFinal.Item("DEPARTMENT_NAME") = clsCommon.myCstr(DrHead("DEPARTMENT_NAME"))
                        DrFinal.Item("BANK_CODE") = clsCommon.myCstr(DrHead("BANK_CODE"))
                        DrFinal.Item("Bank_Name") = clsCommon.myCstr(DrHead("Bank_Name"))

                        DrFinal.Item("BANK_ACC_NO") = clsCommon.myCstr(DrHead("BANK_ACC_NO"))
                        DrFinal.Item("Emp_Pan_no") = clsCommon.myCstr(DrHead("Emp_Pan_no"))
                        DrFinal.Item("Location_Address") = clsCommon.myCstr(DrHead("Location_Address"))
                        DrFinal.Item("Joining_date") = clsCommon.myCstr(DrHead("Joining_date"))
                        DrFinal.Item("PFNo") = clsCommon.myCstr(DrHead("PFNo"))
                        DrFinal.Item("ESINo") = clsCommon.myCstr(DrHead("ESINo"))
                        DrFinal.Item("PAY_PERIOD_CODE") = clsCommon.myCstr(DrHead("PAY_PERIOD_CODE"))
                        DrFinal.Item("Payment_Name") = clsCommon.myCstr(DrHead("Payment_Name"))
                        ' For Each rowleave As DataRow In dt4.Rows
                        DrFinal.Item("Leave_Code") = clsCommon.myCstr(DrDT3("Leave_Code"))
                        DrFinal.Item("AVAILED") = clsCommon.myCstr(DrDT3("AVAILED"))

                        '' balance of leaves
                        DrFinal.Item("EL_Bal") = clsCommon.myCdbl(dt4.Compute("sum(Balance)", "leave_code='EL'")) ' clsCommon.myCstr(DrDT3("Balance"))
                        DrFinal.Item("CL_Bal") = clsCommon.myCdbl(dt4.Compute("sum(Balance)", "leave_code='CL'")) ' clsCommon.myCstr(DrDT3("Balance"))
                        DrFinal.Item("SL_Bal") = clsCommon.myCdbl(dt4.Compute("sum(Balance)", "leave_code='SL'")) ' clsCommon.myCstr(DrDT3("Balance"))

                        'DrFinal.Item("CL_Bal") = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Casual_Leave from TSPL_MONTHLY_ATTENDANCE_DETAIL left outer Join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_CODE = TSPL_MONTHLY_ATTENDANCE.MTA_CODE where TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' and TSPL_MONTHLY_ATTENDANCE_DETAIL.EMP_CODE = '" + clsCommon.myCstr(DrHead("Code")) + "' "))
                        'DrFinal.Item("EL_Bal") = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Earned_Leave from TSPL_MONTHLY_ATTENDANCE_DETAIL left outer Join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_CODE = TSPL_MONTHLY_ATTENDANCE.MTA_CODE where TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' and TSPL_MONTHLY_ATTENDANCE_DETAIL.EMP_CODE = '" + clsCommon.myCstr(DrHead("Code")) + "' "))
                        'Next
                        'DrFinal.Item("CL_Open") = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_LEAVE_ALLOTMENTDETAIL.ALLOTED_LEAVE from TSPL_LEAVE_ALLOTMENTDETAIL left join TSPL_LEAVE_ALLOTMENT on TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE=TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE  WHERE TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE='CL' AND TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE='" + clsCommon.myCstr(DrHead("Code")) + "' AND YEAR(TSPL_LEAVE_ALLOTMENT.ALLOTMENT_DATE)=(select year(date_to) from TSPL_PAYPERIOD_MASTER where pay_period_code= '" + txtFromPP.Value + "')"))

                        'Allocated Leave
                        If (dtAllocatedLeave IsNot Nothing AndAlso dtAllocatedLeave.Rows.Count > 0) Then
                            DrFinal.Item("CL_Open") = clsCommon.myCdbl(dtAllocatedLeave.Compute("sum(ALLOTED_LEAVE)", "leave_code='CL'"))
                            DrFinal.Item("CML_Open") = clsCommon.myCdbl(dtAllocatedLeave.Compute("sum(ALLOTED_LEAVE)", "leave_code='CML'"))
                            DrFinal.Item("EL_Open") = clsCommon.myCdbl(dtAllocatedLeave.Compute("sum(ALLOTED_LEAVE)", "leave_code='EL'"))
                            DrFinal.Item("HPL_Open") = clsCommon.myCdbl(dtAllocatedLeave.Compute("sum(ALLOTED_LEAVE)", "leave_code='HPL'"))
                            DrFinal.Item("OL_Open") = clsCommon.myCdbl(dtAllocatedLeave.Compute("sum(ALLOTED_LEAVE)", "leave_code='OL'"))
                            DrFinal.Item("WECL_Open") = clsCommon.myCdbl(dtAllocatedLeave.Compute("sum(ALLOTED_LEAVE)", "leave_code='WECL'"))
                        End If
                        'Closing Balance Leave
                        If (dtClosingLeave IsNot Nothing AndAlso dtClosingLeave.Rows.Count > 0) Then
                            DrFinal.Item("CL_Closeing") = clsCommon.myCdbl(dtClosingLeave.Compute("sum(Balance)", "leave_code='CL'"))
                            DrFinal.Item("CML_Closeing") = clsCommon.myCdbl(dtClosingLeave.Compute("sum(Balance)", "leave_code='CML'"))
                            DrFinal.Item("EL_Closeing") = clsCommon.myCdbl(dtClosingLeave.Compute("sum(Balance)", "leave_code='EL'"))
                            DrFinal.Item("HPL_Closeing") = clsCommon.myCdbl(dtClosingLeave.Compute("sum(Balance)", "leave_code='HPL'"))
                            DrFinal.Item("OL_Closeing") = clsCommon.myCdbl(dtClosingLeave.Compute("sum(Balance)", "leave_code='OL'"))
                            DrFinal.Item("WECL_Closeing") = clsCommon.myCdbl(dtClosingLeave.Compute("sum(Balance)", "leave_code='WECL'"))
                        End If

                        If clsCommon.myLen(DrDT("PAY_HEAD_NAME")) > 0 Then
                            DrFinal.Item("ErPayHead_name") = clsCommon.myCstr(DrDT("PAY_HEAD_NAME"))
                            DrFinal.Item("ErPayHead_Rate") = clsCommon.myCdbl(DrDT("RATE_AMOUNT"))
                            DrFinal.Item("ErPayHead_amt") = clsCommon.myCdbl(DrDT("ACTUAL_AMOUNT"))
                            DrFinal.Item("ErArrHead_amt") = clsCommon.myCdbl(DrDT("Arrear_Amount"))
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
                If Print = True Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crptKDILSalarySlipFormat1", "Employee Salary Slip Report")
                    frmcrystal = Nothing
                Else
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crptKDILSalarySlip ForSingleEmployee", "Employee Salary Slip Report")
                    frmcrystal = Nothing
                End If



            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocationMult_My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & txtFromPP.Value & "') "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
    End Sub
    Private Sub txtDivisionMult_My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
    Private Sub txtEmployeeMult_My_Click(sender As Object, e As EventArgs) Handles txtEmployeeMult._My_Click
        Dim qry As String = GetEmploeeQry()
        txtEmployeeMult.arrValueMember = clsCommon.ShowMultipleSelectForm("EMPMulSel", qry, "Code", "Name", txtEmployeeMult.arrValueMember, txtEmployeeMult.arrDispalyMember)
    End Sub
    'Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
    '    If clsCommon.myLen(FndLocationCode.Value) > 0 Then
    '        lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
    '    Else
    '        lblLocationName.Text = ""
    '    End If
    '    Loaddata()
    'End Sub

    Function GetEmploeeQry() As String
        Qry = ""
        Qry = " SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE as Code, TSPL_EMPLOYEE_MASTER .Emp_Name as Name FROM TSPL_GENERATE_SALARY "
        Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE "
        Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE= TSPL_EMPLOYEE_MASTER .EMP_CODE "
        Qry += " Left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= TSPL_GENERATE_SALARY.Location_Code"
        Qry += " where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtFromPP.Value + "' "
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY.Location_Code in (" & clsCommon.GetMulcallString(txtLocationMult.arrValueMember) & " )"
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_EMPLOYEE_MASTER.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
        End If
        Qry += " ORDER BY TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE "
        Return Qry
    End Function
    Private Sub lblPrintFormat2_Click(sender As Object, e As EventArgs) Handles lblPrintFormat2.Click
        Print = False
        PrintData()
    End Sub

    Private Sub txtDepartment__My_Click(sender As Object, e As EventArgs) Handles txtDepartment._My_Click
        Dim Qry As String = ""
        Qry = "		select distinct TSPL_EMPLOYEE_MASTER.Department_Code as Code,DEPARTMENT_NAME as Name from TSPL_DEPARTMENT_MASTER left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE =TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE left join TSPL_GENERATE_SALARY_ATTENDANCE on"
        Qry += " TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE =TSPL_EMPLOYEE_MASTER .EMP_CODE  LEFT OUTER JOIN TSPL_GENERATE_SALARY  ON TSPL_GENERATE_SALARY  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_ATTENDANCE .SALARY_GENERATION_CODE   where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtFromPP.Value + "' "
        If txtEmployeeMult.arrValueMember IsNot Nothing AndAlso txtEmployeeMult.arrValueMember.Count > 0 Then
            Qry += " and TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE in (" & clsCommon.GetMulcallString(txtEmployeeMult.arrValueMember) & " )"
        End If
        txtDepartment.arrValueMember = clsCommon.ShowMultipleSelectForm("DEPMulSel", Qry, "Code", "Name", txtDepartment.arrValueMember, txtDepartment.arrDispalyMember)
    End Sub
End Class
