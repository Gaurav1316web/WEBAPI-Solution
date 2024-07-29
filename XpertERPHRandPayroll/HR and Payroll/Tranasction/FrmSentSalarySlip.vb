Imports common
Imports System.IO
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI


Public Class FrmSentSalarySlip
    Inherits FrmMainTranScreen
    ''check in Sanjay 20200619
    Public arr As New List(Of ClsSentMailSlip)
    Dim isnewwntry As Boolean = True
    Dim SalarySlipLeaveStatusOnTheBasisOfCalendarYear As Boolean
    'Public ObjList As New List(Of ClsSentMailSlip)
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSentSalarySlip)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        'btnExport.Visible = MyBase.isExport
    End Sub
    'Sub save(ByVal empCode As String, ByVal email As String, ByVal MailSend As String)
    '    Dim obj As ClsSentMailSlip
    '    Dim arr As New List(Of ClsSentMailSlipDetail)
    '    obj = New ClsSentMailSlip()
    '    'ObjList = New List(Of ClsSentMailSlip)
    '    Dim strQuery As String = "select TSPL_GENERATE_SALARY. SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE,TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE,TSPL_EMPLOYEE_MASTER.EMail_ID    from TSPL_GENERATE_SALARY  left outer join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY. SALARY_GENERATION_CODE left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE where TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE='" + txtFromPP1.Value + "' and LEN(TSPL_EMPLOYEE_MASTER.EMail_ID)>5"
    '    If chkLocationSelect.IsChecked And cbgEmp.CheckedValue.Count > 0 Then
    '        strQuery += "and TSPL_EMPLOYEE_MASTER.EMP_CODE  IN (" + clsCommon.GetMulcallString(cbgEmp.CheckedValue) + ") "
    '    End If
    '    Dim dtt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
    '    If clsCommon.myLen(strQuery) > 0 Then
    '        For Each Str As String In cbgEmp.CheckedValue ' For Each obj1 As ClsSentMailSlip In cbgEmp
    '            Dim _objtr As New ClsSentMailSlipDetail
    '            _objtr.Emp_Code = clsCommon.myCstr(dtt.Select("EMP_CODE='" & Str & "'")(0)("EMP_CODE"))
    '            _objtr.Email_ID = clsCommon.myCstr(dtt.Select("EMP_CODE='" & Str & "'")(0)("EMail_ID"))
    '            '_objtr.PAY_PERIOD_CODE = txtFromPP1.Value
    '            '_objtr.User_Code = objCommonVar.CurrentUserCode
    '            arr.Add(_objtr)
    '        Next
    '        obj.PAY_PERIOD_CODE = txtFromPP1.Value
    '    End If
    '    ClsSentMailSlip.SaveData(obj, arr, isnewwntry)

    'End Sub
    Sub sendSalarySlipToMail(ByVal strStartupPath As String)
        Dim obj As New ClsSentMailSlip
        Dim arr As New List(Of ClsSentMailSlipDetail)
        Dim objtr As New ClsSentMailSlipDetail
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim sent, NotSent As Integer
            Dim ErrorLog As String = ""
            '' changes by shivani[BM00000008214]
            Dim arrTo As List(Of String) = Nothing

            Dim strQuery As String = "select TSPL_GENERATE_SALARY. SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE,TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE,TSPL_EMPLOYEE_MASTER.EMail_ID    from TSPL_GENERATE_SALARY  left outer join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY. SALARY_GENERATION_CODE left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE where TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE='" + txtFromPP1.Value + "' and LEN(TSPL_EMPLOYEE_MASTER.EMail_ID)>5"
            If chkLocationSelect.IsChecked And cbgEmp.CheckedValue.Count > 0 Then
                strQuery += "and TSPL_EMPLOYEE_MASTER.EMP_CODE  IN (" + clsCommon.GetMulcallString(cbgEmp.CheckedValue) + ") "
            End If
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                arr = New List(Of ClsSentMailSlipDetail)
                For i As Integer = 0 To dtt.Rows.Count - 1
                    ''save in obj
                    obj.PAY_PERIOD_CODE = clsCommon.myCstr(dtt.Rows(0)("PAY_PERIOD_CODE"))
                    objtr = New ClsSentMailSlipDetail()

                    objtr.Email_ID = clsCommon.myCstr(dtt.Rows(i)("EMail_ID"))
                    objtr.Emp_Code = clsCommon.myCstr(dtt.Rows(i)("EMP_CODE"))
                    ''end here======================================

                    Dim header1 As String = "Salary Slip for the month of " & dtt.Rows(0)("PAY_PERIOD_CODE") & ""
                    Dim header2 As String = ""

                    Dim onePagePrint As String = Nothing
                    onePagePrint = 1
                    Dim Qry As String = ""
                    Qry = ""
                    Qry += "  SELECT '" + clsCommon.myCstr(onePagePrint) + "' as onePagePrint,'' as Status,T1.EMP_CODE As [Code] ,T1.Emp_Name as [Name],T1.PF_NO as [PFNo], T1.ESI_NO  as [ESINo], "
                    Qry += " t1.FATHERS_NAME as [FathersName],'" & objCommonVar.CurrentCompanyName & "' as [CompanyName],  t2.Add1+Case When ISNULL(t2.Add2,'')='' Then ''  else ', '+t2.Add2+ Case When ISNULL(t2.Add3,'')='' Then '' Else ', '+t2.Add3+ Case When ISNULL(t2.Pincode,'')='' Then '' else '-'+CONVERT(varchar, t2.Pincode) End End End  as [CompanyAddress],Logo_Img,Location_Desc as LocationDesc,"
                    Qry += " T1.Location_Code as LocationCode, "
                    Qry += " TSPL_DESIGNATION_MASTER.Designation_id ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE ,"
                    Qry += " TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME ,TSPL_BANK_MASTER.BANK_CODE ,t1.Bank_Name,T1.BANK_ACC_NO ,T1.PAN_NO as Emp_Pan_no,"
                    Qry += "  TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+"
                    Qry += "  Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End as Location_Address"
                    Qry += " ,t1.Joining_date ,t7.PAY_PERIOD_CODE,T6.PAYPERIOD_DAYS,HOLIDAY_DAYS,PAYABLE_DAYS,(T6.PAYPERIOD_DAYS-HOLIDAY_DAYS) as Working_days,T6.Present_Days as [Present Days],(T6.PAYPERIOD_DAYS-Present_Days-HOLIDAY_DAYS-LEAVE_DAYS-LOP_DAYS) as Weekly_off ,D1.DEVISION_CODE AS DevCode,TSPL_LOCATION_MASTER.PF_NO as Firm_PF_No from TSPL_EMPLOYEE_MASTER T1"
                    Qry += " left Outer join tspl_company_Master T2 on T2.Comp_Code =T1.Comp_Code  "
                    Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = T1.LOCATION_CODE "
                    Qry += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code = TSPL_LOCATION_MASTER.State "
                    Qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =t1.Designation "
                    Qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =t1.DEPARTMENT_CODE "
                    Qry += " left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =t1.BANK_CODE"
                    Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE T6 ON T6.EMP_CODE =T1.EMP_CODE "
                    Qry += " inner JOIN TSPL_GENERATE_SALARY T7 on T7.SALARY_GENERATION_CODE =T6.SALARY_GENERATION_CODE LEFT OUTER JOIN TSPL_DEVISION_MASTER D1 ON D1.DEVISION_CODE = T1.DEVISION_CODE where T7.Pay_Period_Code='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "' and T1.EMP_CODE ='" & dtt.Rows(i)("EMP_CODE") & "'"
                    If txtLocCode.arrValueMember IsNot Nothing AndAlso txtLocCode.arrValueMember.Count > 0 Then
                        Qry += " and T1.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocCode.arrValueMember) + ") "
                    End If
                    If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                        Qry += " and T1.Devision_Code  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
                    End If
                    If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                        Qry += " and T1.Department_Code  in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ") "
                    End If
                    'If chkLocationSelect.IsChecked And cbgEmp.CheckedValue.Count > 0 Then
                    '    Qry += "and T1.EMP_CODE  IN (" + clsCommon.GetMulcallString(cbgEmp.CheckedValue) + ") "
                    'End If

                    Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)


                    If Hader_Info.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                        'trans.Rollback()
                    Else


                        Dim dtFinal As DataTable = New DataTable
                        dtFinal.Columns.Add("onePagePrint", GetType(String))
                        dtFinal.Columns.Add("Code", GetType(String))
                        dtFinal.Columns.Add("Name", GetType(String))
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
                        dtFinal.Columns.Add("HEADER1", GetType(String))
                        dtFinal.Columns.Add("HEADER2", GetType(String))
                        dtFinal.Columns.Add("Status", GetType(String))
                        dtFinal.AcceptChanges()

                        Dim DrFinal As DataRow = dtFinal.NewRow()
                        Dim DrDT As DataRow
                        Dim DrDT1 As DataRow
                        Dim DrDT3 As DataRow
                        For Each DrHead As DataRow In Hader_Info.Rows
                            Dim WD As Decimal = 0
                            Dim PD As Decimal = 0
                            Dim HD As Decimal = 0
                            Dim WF As Decimal = 0
                            WD = DrHead.Item("Present Days")
                            PD = DrHead.Item("PAYABLE_DAYS")
                            HD = DrHead.Item("HOLIDAY_DAYS")
                            WF = DrHead.Item("Weekly_off")

                            'Qry = ""
                            'Qry += "select LINE_NO,head.PAY_HEAD_CODE,head.PAY_HEAD_NAME,EMP_CODE,RATE_AMOUNT,ACTUAL_AMOUNT,Arrear_Amount from"
                            'Qry += " (SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "' "

                            'Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "'"
                            'Qry += " and t1.SUB_HEAD_TYPE <> 'Arrear'  and ACTUAL_AMOUNT <> 0 )as head"
                            'Qry += " Left Join (SELECT T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.ACTUAL_AMOUNT as  Arrear_Amount,ARREAR_TYPE  FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "' "

                            'Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' "
                            'Qry += "  and  t1.SUB_HEAD_TYPE = 'Arrear' and ACTUAL_AMOUNT <> 0 )as detail on  detail.ARREAR_TYPE=head.PAY_HEAD_CODE"
                            'Qry += "  ORDER BY EMP_CODE,LINE_NO"

                            Qry = ""
                            Qry += "select LINE_NO,head.PAY_HEAD_CODE,head.PAY_HEAD_NAME,EMP_CODE,RATE_AMOUNT,(ACTUAL_AMOUNT-head.Arrear_Amount) as ACTUAL_AMOUNT,COALESCE(detail.Arrear_Amount,head.Arrear_Amount) as Arrear_Amount from"
                            Qry += " (SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT,COALESCE(T2.ARREAR_AMT,0) as Arrear_Amount FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.LINE_NO,T1.PAY_HEAD_CODE,T1.EMP_CODE,T1.RATE_AMOUNT,T1.ACTUAL_AMOUNT,T1.ARREAR_AMT FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "' "
                            Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' and t1.SUB_HEAD_TYPE <> 'Arrear'  and ACTUAL_AMOUNT <> 0  )as head"
                            Qry += " Left Join (SELECT T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.ACTUAL_AMOUNT as  Arrear_Amount,ARREAR_TYPE  FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "' "
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
                            Qry += " AND T2.PAY_PERIOD_CODE='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "'  "

                            Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' and ACTUAL_AMOUNT <> 0"
                            Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                            Qry = ""
                            Qry += "select 'W.D' as Leave_Code," & WD & " as AVAILED,0 as Balance Union All select 'H.D' as Leave_Code," & HD & " as AVAILED,0 as Balance  Union All select 'W.F' as Leave_Code," & WF & " as AVAILED,0 as Balance Union All select Leave_Code,AVAILED,Balance from TSPL_FUN_LEAVE_STATUS ('" + dtt.Rows(0)("PAY_PERIOD_CODE") + "') where 2=2 "

                            Qry += " and Emp_Code='" + DrHead("Code") + "'"
                            Qry += " Union All select 'P.D' as Leave_Code," & PD & " as AVAILED,0 as Balance"
                            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                            Dim Counter As Int16 = dt.Rows.Count
                            If dt1.Rows.Count > dt.Rows.Count Then
                                Counter = dt1.Rows.Count
                            End If
                            If dt4.Rows.Count > dt1.Rows.Count Then
                                Counter = dt4.Rows.Count
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
                                If dt4.Rows.Count > ii Then
                                    DrDT3 = dt4.Rows(ii)
                                Else
                                    DrDT3 = dt4.NewRow()
                                End If
                                DrFinal = dtFinal.NewRow()
                                header2 = "This is to certify that " & clsCommon.myCstr(DrHead("Name")) & " is in employment with us  " _
                                & " since " & clsCommon.GetPrintDate(DrHead("JOINING_DATE"), "dd/MMM/yyyy") & " and is in receipt of following monthly emoluments."
                                DrFinal.Item("onePagePrint") = clsCommon.myCstr(DrHead("onePagePrint"))
                                DrFinal.Item("Code") = clsCommon.myCstr(DrHead("Code"))
                                DrFinal.Item("Name") = clsCommon.myCstr(DrHead("Name"))
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
                                DrFinal.Item("PAY_PERIOD_CODE") = clsCommon.myCstr(dtt.Rows(0)("PAY_PERIOD_CODE"))

                                ' For Each rowleave As DataRow In dt4.Rows
                                DrFinal.Item("Leave_Code") = clsCommon.myCstr(DrDT3("Leave_Code"))
                                DrFinal.Item("AVAILED") = clsCommon.myCstr(DrDT3("AVAILED"))

                                '' balance of leaves
                                'DrFinal.Item("EL_Bal") = clsCommon.myCstr(DrDT3("Balance"))
                                'DrFinal.Item("CL_Bal") = clsCommon.myCstr(DrDT3("Balance"))
                                'DrFinal.Item("SL_Bal") = clsCommon.myCstr(DrDT3("Balance"))
                                DrFinal.Item("EL_Bal") = clsCommon.myCdbl(dt4.Compute("sum(Balance)", "leave_code='EL'")) ' clsCommon.myCstr(DrDT3("Balance"))
                                DrFinal.Item("CL_Bal") = clsCommon.myCdbl(dt4.Compute("sum(Balance)", "leave_code='CL'")) ' clsCommon.myCstr(DrDT3("Balance"))
                                DrFinal.Item("SL_Bal") = clsCommon.myCdbl(dt4.Compute("sum(Balance)", "leave_code='SL'")) ' clsCommon.myCstr(DrDT3("Balance"))

                                DrFinal.Item("HEADER1") = header1
                                DrFinal.Item("HEADER2") = header2



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
                                dtFinal.Rows.Add(DrFinal)
                            Next
                        Next
                        dtFinal.AcceptChanges()

                        Dim strTrgtFile As String = ("SalarySlip_" & dtt.Rows(i)("PAY_PERIOD_CODE") & "_" & dtt.Rows(i)("EMP_CODE")).ToString.Replace("/", "").Replace("\", "")
                        If rbtnFormat1.IsChecked = True Then
                            strStartupPath = GetReportPath(CrystalReportFolder.HRPayroll, "crptKDILSalarySlipFormat1", Nothing)
                            If clsERPFuncationalityOLD.exportCrystalToPDF(dtFinal, strStartupPath, "crptKDILSalarySlipFormat1", strTrgtFile, strStartupPath) Then
                                arrTo = New List(Of String)
                                If dtFinal.Rows.Count > 0 Then
                                    arrTo = New List(Of String)
                                    arrTo.Add(dtt.Select("EMP_CODE='" & dtFinal.Rows(0)("Code") & "'")(0)("EMail_ID"))
                                    Try
                                        ' clsMailViaOutlook.SendEmail("Salary Slip", "Salary Slip For The Period- " & dtt.Rows(0)("PAY_PERIOD_CODE"), arrTo, Nothing, strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf")
                                        SaveEmailText(dtFinal.Rows(0)("Code"), strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf", arrTo, Nothing)
                                        objtr.Status = "Y"
                                        sent = sent + 1
                                    Catch ex As Exception
                                        objtr.Status = "N"
                                        objtr.Error_Log = ex.Message
                                        ErrorLog = ErrorLog & Environment.NewLine & ex.Message
                                        NotSent = NotSent + 1
                                    End Try
                                Else

                                    arrTo.Add(dtt.Rows(0)("EMail_ID"))
                                    Try
                                        'clsMailViaOutlook.SendEmail("Salary Slip", "Salary Slip For The Period- " & dtt.Rows(0)("PAY_PERIOD_CODE"), arrTo, Nothing, strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf")
                                        SaveEmailText(dtFinal.Rows(0)("Code"), strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf", arrTo, Nothing)
                                        objtr.Status = "Y"
                                        sent = sent + 1
                                    Catch ex As Exception
                                        objtr.Status = "N"
                                        objtr.Error_Log = ex.Message
                                        ErrorLog = ErrorLog & Environment.NewLine & ex.Message
                                        NotSent = NotSent + 1
                                    End Try

                                End If
                                'clsMailViaOutlook.SendEmail("Salary Slip", "Salary Slip For The Period- " & dtt.Rows(0)("PAY_PERIOD_CODE"), arrTo, Nothing, strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf")
                            End If
                        Else
                            ' If clsERPFuncationality.exportCrystalToPDF(dtFinal, strStartupPath & "\Crystal Reports\HR_Payroll", "crptKDILSalarySlip ForSingleEmployee", strTrgtFile, strStartupPath) Then
                            strStartupPath = GetReportPath(CrystalReportFolder.HRPayroll, "crptKDILSalarySlip ForSingleEmployee", Nothing)
                            If clsERPFuncationalityOLD.exportCrystalToPDF(dtFinal, strStartupPath, "crptKDILSalarySlip ForSingleEmployee", strTrgtFile, strStartupPath) Then
                                arrTo = New List(Of String)
                                If dtFinal.Rows.Count > 0 Then
                                    arrTo = New List(Of String)
                                    arrTo.Add(dtt.Select("EMP_CODE='" & dtFinal.Rows(0)("Code") & "'")(0)("EMail_ID"))
                                    Try
                                        ' clsMailViaOutlook.SendEmail("Salary Slip", "Salary Slip For The Period- " & dtt.Rows(0)("PAY_PERIOD_CODE"), arrTo, Nothing, strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf")
                                        SaveEmailText(dtFinal.Rows(0)("Code"), strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf", arrTo, Nothing)
                                        objtr.Status = "Y"
                                        sent = sent + 1
                                    Catch ex As Exception
                                        objtr.Status = "N"
                                        objtr.Error_Log = ex.Message
                                        ErrorLog = ErrorLog & Environment.NewLine & ex.Message
                                        NotSent = NotSent + 1
                                    End Try

                                Else
                                    arrTo = New List(Of String)
                                    arrTo.Add(dtt.Rows(0)("EMail_ID"))
                                    Try
                                        'clsMailViaOutlook.SendEmail("Salary Slip", "Salary Slip For The Period- " & dtt.Rows(0)("PAY_PERIOD_CODE"), arrTo, Nothing, strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf")
                                        SaveEmailText(dtFinal.Rows(0)("Code"), strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf", arrTo, Nothing)
                                        objtr.Status = "Y"
                                        sent = sent + 1
                                    Catch ex As Exception
                                        objtr.Status = "N"
                                        objtr.Error_Log = ex.Message
                                        ErrorLog = ErrorLog & Environment.NewLine & ex.Message
                                        NotSent = NotSent + 1

                                    End Try
                                End If
                                'clsMailViaOutlook.SendEmail("Salary Slip", "Salary Slip For The Period- " & dtt.Rows(0)("PAY_PERIOD_CODE"), arrTo, Nothing, strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf")
                            End If
                        End If

                    End If

                    If clsCommon.myLen(objtr.Emp_Code) > 0 Then
                        arr.Add(objtr)
                    End If
                Next
                'trans.Commit()
                'ClsSentMailSlip.SaveData(obj, arr, True)

                clsCommon.MyMessageBoxShow("Sent:" & sent & ",Not Sent:" & NotSent & "")

                'Dim logFile As String =  "c:\ERPTempFolder\salgenlog.txt"
                'If System.IO.File.Exists(logFile) Then
                '    Dim stream As New IO.StreamWriter(logFile, False)
                '    stream.WriteLine("")
                '    stream.Close()
                'Else
                '    Dim fs As IO.FileStream = System.IO.File.Create(logFile)
                '    fs.Close()
                'End If
                'Dim streamWr As New IO.StreamWriter(logFile, False)
                'streamWr.WriteLine(ErrorLog)
                'streamWr.Close()

            End If
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            'trans.Rollback()
        End Try
    End Sub

    Sub PrintData()
        Try
            Dim onePagePrint As String = Nothing
            onePagePrint = 1
            Dim arrTo As List(Of String) = Nothing
            Dim dtt As New DataTable()
            Dim Qry As String = ""
            Dim Print As Boolean = IIf(rbtnFormat1.IsChecked, True, False)

            Qry = " select TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE as [Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Name],TSPL_EMPLOYEE_MASTER.EMail_ID [Email]  from TSPL_GENERATE_SALARY_ATTENDANCE inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE  left join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" + txtFromPP1.Value + "' and LEN(TSPL_EMPLOYEE_MASTER.EMail_ID)>5 "
            If txtLocCode.arrValueMember IsNot Nothing AndAlso txtLocCode.arrValueMember.Count > 0 Then
                Qry += " and TSPL_EMPLOYEE_MASTER.Location_code  in (" + clsCommon.GetMulcallString(txtLocCode.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_EMPLOYEE_MASTER.Devision_Code  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ")"
            End If
            If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                Qry += " and TSPL_EMPLOYEE_MASTER.Department_Code  in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ")"
            End If


            If chkLocationSelect.IsChecked And cbgEmp.CheckedValue.Count > 0 Then
                Qry += " and TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  in (" + clsCommon.GetMulcallString(cbgEmp.CheckedValue) + ") "
            End If
            dtt = clsDBFuncationality.GetDataTable(Qry)

            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For I As Integer = 0 To dtt.Rows.Count() - 1

                    Qry = ""
                    Qry += "  SELECT TSPL_Payment_MODE.NAME as Payment_Name, '" + clsCommon.myCstr(onePagePrint) + "' as onePagePrint,T1.EMP_CODE As [Code] ,T1.Emp_Name as [Name],T1.UIN_NO,T1.PF_NO as [PFNo], T1.ESI_NO  as [ESINo], "
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
                    Qry += " inner JOIN TSPL_GENERATE_SALARY T7 on T7.SALARY_GENERATION_CODE =T6.SALARY_GENERATION_CODE where T7.Pay_Period_Code='" & txtFromPP1.Value & "'"
                    Qry += " and T1.EMP_CODE  ='" + clsCommon.myCstr(dtt.Rows(I).Item("Code")) + "' "
                    Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)


                    'If Hader_Info.Rows.Count <= 0 Then
                    '    common.clsCommon.MyMessageBoxShow("No Data Found")
                    If Hader_Info.Rows.Count > 0 Then
                        Dim dtFinal As DataTable = New DataTable
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
                        dtFinal.AcceptChanges()

                        Dim DrFinal As DataRow = dtFinal.NewRow()
                        Dim DrDT As DataRow
                        Dim DrDT1 As DataRow
                        ' Dim DrDT2 As DataRow
                        Dim DrDT3 As DataRow

                        For Each DrHead As DataRow In Hader_Info.Rows

                            Dim WD As Decimal = 0
                            Dim PD As Decimal = 0
                            Dim HD As Decimal = 0
                            Dim WF As Decimal = 0
                            WD = DrHead.Item("Present Days")
                            PD = DrHead.Item("PAYABLE_DAYS")
                            HD = DrHead.Item("HOLIDAY_DAYS")
                            WF = DrHead.Item("Weekly_off")

                            Qry = ""
                            Qry += "select LINE_NO,head.PAY_HEAD_CODE,head.PAY_HEAD_NAME,EMP_CODE,RATE_AMOUNT,(ACTUAL_AMOUNT-head.Arrear_Amount) as ACTUAL_AMOUNT,COALESCE(detail.Arrear_Amount,head.Arrear_Amount) as Arrear_Amount from"
                            Qry += " (SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT,COALESCE(T2.ARREAR_AMT,0) as Arrear_Amount FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.LINE_NO,T1.PAY_HEAD_CODE,T1.EMP_CODE,T1.PAYABLE_AMOUNT as RATE_AMOUNT,T1.ACTUAL_AMOUNT,T1.ARREAR_AMT FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + txtFromPP1.Value + "' "
                            Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' and t1.SUB_HEAD_TYPE <> 'Arrear'  and ACTUAL_AMOUNT <> 0  )as head"
                            Qry += " Left Join (SELECT T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.ACTUAL_AMOUNT as  Arrear_Amount,ARREAR_TYPE  FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.LINE_NO,T1.PAY_HEAD_CODE,T1.EMP_CODE,T1. PAYABLE_AMOUNT as RATE_AMOUNT,T1.ACTUAL_AMOUNT,T1.ARREAR_AMT FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + txtFromPP1.Value + "' "
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
                            Qry += " AND T2.PAY_PERIOD_CODE='" + txtFromPP1.Value + "' "
                            Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "'   AND ACTUAL_AMOUNT <> 0 "
                            Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                            Qry = ""
                            Qry += "select 'W.D' as Leave_Code," & WD & " as AVAILED,0 as Balance Union All select 'H.D' as Leave_Code," & HD & " as AVAILED,0 as Balance  Union All select 'W.F' as Leave_Code," & WF & " as AVAILED,0 as Balance Union All select Leave_Code,AVAILED,Balance from " + IIf(SalarySlipLeaveStatusOnTheBasisOfCalendarYear, "TSPL_FUN_LEAVE_STATUS_WO_PRV_BAL_CAL_YEAR", "TSPL_FUN_LEAVE_STATUS_WO_PRV_BAL") + " ('" + txtFromPP1.Value + "') where Emp_Code='" + DrHead("Code") + "'  Union All select 'P.D' as Leave_Code," & PD & " as AVAILED,0 as Balance"
                            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                            '===============================
                            Dim MyDataRow As DataRow
                            For Each MyDataRow In dt4.Rows
                                If clsCommon.CompairString((MyDataRow("Leave_Code")), "CL") = CompairStringResult.Equal Then
                                    Dim aaabc As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select convert (decimal(18,2), Casual_Leave) as Casual_Leave from TSPL_MONTHLY_ATTENDANCE_DETAIL left outer Join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_CODE = TSPL_MONTHLY_ATTENDANCE.MTA_CODE where TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE = '" + txtFromPP1.Value + "' and TSPL_MONTHLY_ATTENDANCE_DETAIL.EMP_CODE = '" + clsCommon.myCstr(DrHead("Code")) + "' "))
                                    MyDataRow("AVAILED") = aaabc.ToString("N2") 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select convert (decimal(18,2), Casual_Leave) as Casual_Leave from TSPL_MONTHLY_ATTENDANCE_DETAIL left outer Join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_CODE = TSPL_MONTHLY_ATTENDANCE.MTA_CODE where TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' and TSPL_MONTHLY_ATTENDANCE_DETAIL.EMP_CODE = '" + clsCommon.myCstr(DrHead("Code")) + "' "))
                                    MyDataRow.AcceptChanges()
                                End If
                            Next
                            '==================================

                            Dim Counter As Int16 = dt.Rows.Count
                            If dt1.Rows.Count > dt.Rows.Count Then
                                Counter = dt1.Rows.Count
                            End If
                            If dt4.Rows.Count > dt1.Rows.Count Then
                                Counter = dt4.Rows.Count
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
                                If dt4.Rows.Count > ii Then
                                    DrDT3 = dt4.Rows(ii)
                                Else
                                    DrDT3 = dt4.NewRow()
                                End If

                                DrFinal = dtFinal.NewRow()
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



                                'Next



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

                                dtFinal.Rows.Add(DrFinal)
                            Next

                        Next
                        dtFinal.AcceptChanges()
                        Dim StrPDFPath As String = Nothing
                        Dim frmCRV As New frmCrystalReportViewer()
                        If Print = True Then
                            StrPDFPath = frmCRV.funreport(True, CrystalReportFolder.HRPayroll, dtFinal, EnumTecxpertPaperSize.NA, "crptKDILSalarySlipFormat1", "Employee Salary Slip Report", Nothing)
                        Else
                            StrPDFPath = frmCRV.funreport(True, CrystalReportFolder.HRPayroll, dtFinal, EnumTecxpertPaperSize.NA, "crptKDILSalarySlip ForSingleEmployee", "Employee Salary Slip Report", Nothing)
                        End If
                        arrTo = New List(Of String)
                        arrTo.Add(clsCommon.myCstr(dtt.Rows(I).Item("Email")))
                        SaveEmailText(clsCommon.myCstr(dtt.Rows(I).Item("Code")), StrPDFPath, arrTo, Nothing)
                        frmCRV.Close()
                        frmCRV.Dispose()
                    End If
                Next
                clsCommon.MyMessageBoxShow(Me, "Sent Mail Successfully.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ' Ticket No : MIL/03/05/19-000078 By Prabhakar
    Private Sub btnSendMail_Click(sender As Object, e As EventArgs) Handles btnSendMail.Click
        Try
            clsCommon.ProgressBarShow()
            'sendSalarySlipToMail(Application.StartupPath)
            PrintData()
            'save()
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString)
        End Try
    End Sub

    Private Sub txtFromPP__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFromPP1._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
           & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP1.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP1.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP1.Value, Nothing)
        LoadEmployee()
    End Sub

    Private Sub txtLocCode__My_Click(sender As Object, e As EventArgs) Handles txtLocCode._My_Click
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " And LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & txtFromPP1.Value & "') " + whrcls
        txtLocCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocCode.arrValueMember, txtLocCode.arrDispalyMember)
        LoadEmployee()
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
        LoadEmployee()
    End Sub

    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder1._My_Click
        Dim qry As String = "select distinct TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE as Code,DEPARTMENT_NAME  as Name from TSPL_EMPLOYEE_MASTER left join TSPL_DEPARTMENT_MASTER on  TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE =TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE where TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE<> '' and Location_code   in (" + clsCommon.GetMulcallString(txtLocCode.arrValueMember) + ")"
        TxtMultiSelectFinder1.arrValueMember = clsCommon.ShowMultipleSelectForm("Depart", qry, "Code", "Name", TxtMultiSelectFinder1.arrValueMember, TxtMultiSelectFinder1.arrDispalyMember)
        LoadEmployee()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub LoadEmployee()
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " And TSPL_EMPLOYEE_MASTER.LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        'Dim qry As String = "select Emp_Code as [Code] ,Emp_Name as [Name],EMail_ID as [Email] from TSPL_EMPLOYEE_MASTER where LEN(TSPL_EMPLOYEE_MASTER.EMail_ID)>5 "
        Dim qry As String = " select TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE as [Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Name],TSPL_EMPLOYEE_MASTER.EMail_ID [Email]  from TSPL_GENERATE_SALARY_ATTENDANCE inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE  left join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE='" + txtFromPP1.Value + "' and LEN(TSPL_EMPLOYEE_MASTER.EMail_ID)>5 " + whrcls
        If txtLocCode.arrValueMember IsNot Nothing AndAlso txtLocCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.Location_code  in (" + clsCommon.GetMulcallString(txtLocCode.arrValueMember) + ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.Devision_Code  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ")"
        End If
        If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.Department_Code  in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ")"
        End If
        cbgEmp.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgEmp.ValueMember = "Code"
        cbgEmp.DisplayMember = "Name"
        cbgEmp.DisplayMember = "Email"
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgEmp.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Sub funreset()
        chkLocationAll.CheckState = CheckState.Checked
        rbtnFormat2.IsChecked = True
    End Sub
    Private Sub FrmSentSalarySlip_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SalarySlipLeaveStatusOnTheBasisOfCalendarYear = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SalarySlipLeaveStatusOnTheBasisOfCalendarYear, clsFixedParameterCode.SalarySlipLeaveStatusOnTheBasisOfCalendarYear, Nothing)) = 1, True, False)
        funreset()
    End Sub

    Private Sub FrmSentSalarySlip_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub
    ' Ticket No :  BHA/17/04/19-000863
    Private Function GetReportPath(ByVal crpfolder As CrystalReportFolder, ByVal strReportName As String, ByVal dtTransDate As Date?) As String
        Dim strpath = Application.StartupPath
        Dim strGST As String = ""

        If clsERPFuncationality.GetGSTStatus(dtTransDate) Then
            strGST = " GST"
        End If


        If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
            strpath += "\Crystal Reports\Crystal Reports\" + objCommonVar.CurrentCompanyCode
        End If
        Dim strReportPath As String = ""
        If crpfolder = CrystalReportFolder.CommonServices Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Common Services"
        ElseIf crpfolder = CrystalReportFolder.FixedAssets Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Fixed Assets"
        ElseIf crpfolder = CrystalReportFolder.GeneralLedger Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\General Ledger"
        ElseIf crpfolder = CrystalReportFolder.HRPayroll Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\HR_Payroll"
        ElseIf crpfolder = CrystalReportFolder.HumanResource Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Human Resource"
        ElseIf crpfolder = CrystalReportFolder.InventoryReport Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Inventory Report"
        ElseIf crpfolder = CrystalReportFolder.KwalitySalesReport Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Kwality Sales Report"
        ElseIf crpfolder = CrystalReportFolder.MilkProcurement Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Milk Procurement"
        ElseIf crpfolder = CrystalReportFolder.NewSalesReports Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\New Sales Reports"
        ElseIf crpfolder = CrystalReportFolder.PRODUCTION Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\PRODUCTION"
        ElseIf crpfolder = CrystalReportFolder.Purchase Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Purchase"
        ElseIf crpfolder = CrystalReportFolder.PurchaseOrder Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Purchase Order"
        ElseIf crpfolder = CrystalReportFolder.SalesReport Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Sales Report"
        ElseIf crpfolder = CrystalReportFolder.ServiceReport Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Service Report"
        ElseIf crpfolder = CrystalReportFolder.TDS Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\TDS"
        ElseIf crpfolder = CrystalReportFolder.UtilityReports Then
            strReportPath = strpath + "\Crystal Reports" + strGST + "\Utility Reports"
        End If
        Return strReportPath
    End Function

    '=====================================================================================================================================================
    Function SaveEmailText(ByVal EmpCode As String, ByVal strPath As String, ByVal arrRecepients As List(Of String), ByVal trans As SqlTransaction) As Boolean
        Dim objES As clsESContent
        objES = clsESContent.GetData(clsUserMgtCode.FrmSentSalarySlip, trans)
        If objES IsNot Nothing Then
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Subject = objES.EMail_Subject
            objSMSH.Email_Text = objES.EMail_Text

            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.PayHeadCode, txtFromPP1.Value)
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.PayHeadCode, txtFromPP1.Value)

            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.EmpName, clsEmployeeMaster.GetName(EmpCode, trans))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.EmpName, clsEmployeeMaster.GetName(EmpCode, trans))

            objSMSH.Attachment_1_Path = strPath
            objSMSH.arrEMail = New List(Of String)()
            'Dim qry As String = ""
            'If clsCommon.CompairString(obj.TICKET_TYPE, "Bug") <> CompairStringResult.Equal Then
            '    qry = "select PERSON_EMAIL from TSPL_CLIENT_DETAIL where len(isnull( PERSON_EMAIL,''))>0 and IS_SEND_MAIL=1 and CLIENT_CODE = '" + obj.CLIENT_CODE + "' "
            '    qry += "Union "
            'End If
            'qry += " select TSPL_USER_MASTER.EMAIL as PERSON_EMAIL from TSPL_USER_MASTER where len(isnull( TSPL_USER_MASTER.EMAIL,''))>0 and TSPL_USER_MASTER.USER_CODE in (  select TSPL_MAPPING_USER_DETAIL.MAPPING_USER_CODE from TSPL_MAPPING_USER_DETAIL inner join TSPL_USER_MASTER on TSPL_USER_MASTER.USER_CODE =TSPL_MAPPING_USER_DETAIL.USER_CODE where  TSPL_MAPPING_USER_DETAIL.USER_CODE ='" + objCommonVar.CurrentUserCode + "' ) "
            'If clsCommon.myLen(obj.DEVELOPER_CODE) > 0 Then
            '    qry += "Union "
            '    qry += "select TSPL_USER_MASTER.EMAIL as PERSON_EMAIL from TSPL_REQUEST_ANALYSIS_MASTER inner join TSPL_USER_MASTER on TSPL_USER_MASTER.USER_CODE = TSPL_REQUEST_ANALYSIS_MASTER.DEVELOPER_CODE where len(isnull( TSPL_USER_MASTER.EMAIL,''))>0 and TSPL_REQUEST_ANALYSIS_MASTER.DEVELOPER_CODE = '" + obj.DEVELOPER_CODE + "' and TSPL_REQUEST_ANALYSIS_MASTER.REQUEST_ANALYSIS_NO = '" + obj.REQUEST_ANALYSIS_NO + "'  "
            'End If
            'If clsCommon.myLen(obj.TESTER_CODE) > 0 Then
            '    qry += "Union "
            '    qry += " select TSPL_USER_MASTER.EMAIL as PERSON_EMAIL from TSPL_REQUEST_ANALYSIS_MASTER inner join TSPL_USER_MASTER on TSPL_USER_MASTER.USER_CODE = TSPL_REQUEST_ANALYSIS_MASTER.TESTER_CODE where len(isnull( TSPL_USER_MASTER.EMAIL,''))>0 and TSPL_REQUEST_ANALYSIS_MASTER.TESTER_CODE = '" + obj.TESTER_CODE + "'  and TSPL_REQUEST_ANALYSIS_MASTER.REQUEST_ANALYSIS_NO = '" + obj.REQUEST_ANALYSIS_NO + "' "
            'End If

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        objSMSH.arrEMail.Add(clsCommon.myCstr(dr("PERSON_EMAIL")))
            '    Next
            'End If
            For Each strRece As String In arrRecepients
                objSMSH.arrEMail.Add(strRece)
            Next

            objSMSH.SaveData(clsUserMgtCode.FrmSentSalarySlip, objSMSH, trans)
            objSMSH = Nothing
        End If
        Return True
    End Function


    '======================================================================================================================================================

End Class
