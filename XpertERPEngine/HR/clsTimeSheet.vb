Imports System.Data.SqlClient
Imports common
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls.UI

Public Class clsTimeSheet

#Region "Variables"
    Public CODE As String = Nothing
    Public EMP_CODE As String = Nothing
    Public PROJECT_CODE As String = Nothing
    Public PROJECT_DESC As String = Nothing
    Public CUST_CODE As String = Nothing
    Public CUST_DESC As String = Nothing
    Public TASK_DATE As DateTime? = Nothing
    Public FROM_TIME As DateTime? = Nothing
    Public TO_TIME As DateTime? = Nothing
    Public WORK_TIME_HOURS As Double
    Public WORK_TIME_MINS As Double
    Public WORK_DONE As String = Nothing
    Public JOB_CODE As String = Nothing
    Public TASK_CODE As String = Nothing

    Public UNIT_COST As Decimal = 0
    Public TOTAL_COST As Decimal = 0
    Public BILLING_RATE As Decimal = 0
    Public TOTAL_BILLING As Decimal = 0

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    Public Shared Function SaveData(ByVal arr As List(Of clsTimeSheet), ByVal colCode As String, ByVal gv As RadGridView, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Dim qry As String
        Try
            For Each obj As clsTimeSheet In arr

                Dim isNewEntry As Boolean = False

                If clsCommon.myLen(obj.CODE) <= 0 Then
                    isNewEntry = True
                    qry = "select  MAX(Code)  from TSPL_PJC_TIMESHEET"
                    obj.CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(obj.CODE) <= 0 Then
                        obj.CODE = "TS00000000001"
                    Else
                        obj.CODE = clsCommon.incval(obj.CODE)
                    End If
                End If

                '' check for repeated
                If checkRepeatedTimesheet(obj.EMP_CODE, obj.TASK_DATE, obj.FROM_TIME, obj.TO_TIME, obj.CODE, trans) = True Then
                    Return False
                End If
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                clsCommon.AddColumnsForChange(coll, "PROJECT_CODE", obj.PROJECT_CODE)
                clsCommon.AddColumnsForChange(coll, "CUST_CODE", obj.CUST_CODE)

                clsCommon.AddColumnsForChange(coll, "TASK_DATE", clsCommon.GetPrintDate(obj.TASK_DATE, "dd/MMM/yyyy hh:mm tt"))
                Dim dtActualDate As DateTime = New Date(obj.TASK_DATE.Value.Year, obj.TASK_DATE.Value.Month, obj.TASK_DATE.Value.Day, obj.FROM_TIME.Value.Hour, obj.FROM_TIME.Value.Minute, obj.FROM_TIME.Value.Second)
                clsCommon.AddColumnsForChange(coll, "FROM_TIME", clsCommon.GetPrintDate(dtActualDate, "dd/MMM/yyyy hh:mm tt"))
                dtActualDate = New Date(obj.TASK_DATE.Value.Year, obj.TASK_DATE.Value.Month, obj.TASK_DATE.Value.Day, obj.TO_TIME.Value.Hour, obj.TO_TIME.Value.Minute, obj.TO_TIME.Value.Second)
                clsCommon.AddColumnsForChange(coll, "TO_TIME", clsCommon.GetPrintDate(dtActualDate, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "WORK_TIME_HOURS", obj.WORK_TIME_HOURS)
                clsCommon.AddColumnsForChange(coll, "WORK_TIME_MINS", obj.WORK_TIME_MINS)

                clsCommon.AddColumnsForChange(coll, "UNIT_COST", obj.UNIT_COST)
                clsCommon.AddColumnsForChange(coll, "TOTAL_COST", obj.TOTAL_COST)

                clsCommon.AddColumnsForChange(coll, "BILLING_RATE", obj.BILLING_RATE)
                clsCommon.AddColumnsForChange(coll, "TOTAL_BILLING", obj.TOTAL_BILLING)

                clsCommon.AddColumnsForChange(coll, "WORK_DONE", obj.WORK_DONE)
                clsCommon.AddColumnsForChange(coll, "JOB_CODE", obj.JOB_CODE, True)
                clsCommon.AddColumnsForChange(coll, "TASK_CODE", obj.TASK_CODE, True)
                clsCommon.AddColumnsForChange(coll, "MODIFIED_BY", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "MODIFIED_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                    clsCommon.AddColumnsForChange(coll, "CREATED_BY", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "CREATED_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_TIMESHEET", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_TIMESHEET", OMInsertOrUpdate.Update, "TSPL_PJC_TIMESHEET.Code='" + obj.CODE + "'", trans)
                End If
                '' custom fields
                gv.Rows(arr.IndexOf(obj)).Cells(colCode).Value = obj.CODE
                'isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.CODE, obj.arrCustomFields, trans)
            Next

            'If isSaved Then
            '    trans.Commit()
            '    sendMail(arr)
            'End If
        Catch err As Exception
            'trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal EMP_CODE As String, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal TaskType As String, ByVal strDoc As String) As List(Of clsTimeSheet)
        Dim Arr As List(Of clsTimeSheet) = Nothing
        Dim qryNonFilled As String = ""
        Dim qryFilled As String = ""
        Dim strTimesheetCode As String = ""

        If clsCommon.myLen(strDoc) > 0 Then
            strTimesheetCode = " where  TSPL_PJC_TIMESHEET.CODE ='" & strDoc & "'"
        Else
            strTimesheetCode = " where TSPL_PJC_TIMESHEET.EMP_CODE  ='" + EMP_CODE + "'  " & _
        " and TSPL_PJC_TIMESHEET.TASK_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate), "dd/MMM/yyyy") + "' and TSPL_PJC_TIMESHEET.TASK_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate), "dd/MMM/yyyy") + "'" & _
        " and  TSPL_PJC_TIMESHEET.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"
        End If

        qryNonFilled = " SELECT '' AS CODE,'" & EMP_CODE & "' AS EMP_CODE,TSPL_PJC_PROJECT.Cust_Code,TSPL_CUSTOMER_MASTER.CUSTOMER_NAME AS CUST_DESC,TSPL_PJC_PROJECT.PROJECT_CODE ,TSPL_PJC_PROJECT.SPECIFICATION AS PROJECT_DESC,NULL AS TASK_DATE,NULL AS FROM_TIME," & _
                       " NULL AS TO_TIME,0 AS WORK_TIME_HOURS ,0 AS WORK_TIME_MINS,'' AS WORK_DONE,'' AS JOB_CODE,'' AS TASK_CODE, " & _
                       " 0 AS UNIT_COST,0 AS TOTAL_COST,0 AS BILLING_RATE,0 AS TOTAL_BILLING,'' AS CREATED_DATE  FROM TSPL_PJC_PROJECT" & _
                       " LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_PJC_PROJECT.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
                       " where TSPL_PJC_PROJECT.PROJECT_STATUS not in('Close','Complete','Cancel') and  TSPL_PJC_PROJECT.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'  "


        qryFilled = " SELECT TSPL_PJC_TIMESHEET.CODE,TSPL_PJC_TIMESHEET.EMP_CODE,TSPL_PJC_TIMESHEET.CUST_CODE," & _
        " TSPL_CUSTOMER_MASTER.Customer_Name AS CUST_DESC,TSPL_PJC_TIMESHEET.PROJECT_CODE,TSPL_PJC_PROJECT.SPECIFICATION AS PROJECT_DESC, " & _
        " TSPL_PJC_TIMESHEET.TASK_DATE,TSPL_PJC_TIMESHEET.FROM_TIME,TSPL_PJC_TIMESHEET.TO_TIME,TSPL_PJC_TIMESHEET.WORK_TIME_HOURS, " & _
        " TSPL_PJC_TIMESHEET.WORK_TIME_MINS,TSPL_PJC_TIMESHEET.WORK_DONE,TSPL_PJC_TIMESHEET.JOB_CODE,TSPL_PJC_TIMESHEET.TASK_CODE," & _
        " TSPL_PJC_TIMESHEET.UNIT_COST,TSPL_PJC_TIMESHEET.TOTAL_COST,TSPL_PJC_TIMESHEET.BILLING_RATE,TSPL_PJC_TIMESHEET.TOTAL_BILLING, " & _
        " TSPL_PJC_TIMESHEET.CREATED_DATE FROM TSPL_PJC_TIMESHEET LEFT JOIN TSPL_PJC_PROJECT ON TSPL_PJC_TIMESHEET.PROJECT_CODE=TSPL_PJC_PROJECT.PROJECT_CODE " & _
        " LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_PJC_TIMESHEET.CUST_CODE=TSPL_CUSTOMER_MASTER.Cust_Code" & _
        "  " & strTimesheetCode & " "


        Dim qry As String = ""

        If clsCommon.CompairString(TaskType, "ALL") = CompairStringResult.Equal Then
            qry = qryNonFilled + " union all " + qryFilled
        ElseIf clsCommon.CompairString(TaskType, "Filled") = CompairStringResult.Equal Then
            qry = qryFilled
        Else
            qry = qryNonFilled
        End If

        'qry += " order by PROJECT_CODE "

        Dim obj As clsTimeSheet = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsTimeSheet)
            For Each dr As DataRow In dt.Rows
                obj = New clsTimeSheet()
                obj.CODE = clsCommon.myCstr(dr("CODE"))
                If dr("TASK_DATE") IsNot DBNull.Value Then
                    obj.TASK_DATE = clsCommon.myCDate(dr("TASK_DATE"))
                End If
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))

                If dr("FROM_TIME") IsNot DBNull.Value Then
                    obj.FROM_TIME = clsCommon.myCDate(dr("FROM_TIME"))
                End If

                If dr("TO_TIME") IsNot DBNull.Value Then
                    obj.TO_TIME = clsCommon.myCDate(dr("TO_TIME"))
                End If
                obj.PROJECT_CODE = dr("PROJECT_CODE")
                obj.PROJECT_DESC = dr("PROJECT_DESC")

                obj.CUST_CODE = dr("CUST_CODE")
                obj.CUST_DESC = dr("CUST_DESC")

                obj.WORK_TIME_HOURS = clsCommon.myCdbl(dr("WORK_TIME_HOURS"))
                obj.WORK_TIME_MINS = clsCommon.myCdbl(dr("WORK_TIME_MINS"))

                obj.WORK_DONE = clsCommon.myCstr(dr("WORK_DONE"))
                obj.JOB_CODE = clsCommon.myCstr(dr("JOB_CODE"))
                obj.TASK_CODE = clsCommon.myCstr(dr("TASK_CODE"))

                obj.UNIT_COST = clsCommon.myCdbl(dr("UNIT_COST"))
                obj.TOTAL_COST = clsCommon.myCdbl(dr("TOTAL_COST"))
                obj.BILLING_RATE = clsCommon.myCdbl(dr("BILLING_RATE"))
                obj.TOTAL_BILLING = clsCommon.myCdbl(dr("TOTAL_BILLING"))

                Arr.Add(obj)
            Next
        End If


        Return Arr
    End Function
    Public Shared Function findEmpCode(ByVal UserCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strq As String
        strq = "select emp_code from tspl_employee_master where user_code='" & UserCode & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("emp_code")
        Else
            Return ""
        End If
    End Function
    Public Shared Function checkRepeatedTimesheet(ByVal EmpCode As String, ByVal TaskDate As Date, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal ExcludeCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        ''Check that same time is not repeated for  
        Dim strq As String

        'strq = "SELECT CODE,PROJECT_CODE,CUST_CODE,TASK_DATE,FROM_TIME,TO_TIME FROM TSPL_PJC_TIMESHEET where EMP_CODE='" & EmpCode & "' and TASK_DATE='" & clsCommon.GetPrintDate(TaskDate, "dd/MMM/yyyy") & "'" & _
        '       " and (convert(varchar(5),'" & FromTime.TimeOfDay.ToString & "',108) between CONVERT(varchar(5),FROM_TIME,108) and CONVERT(varchar(5),TO_TIME,108)" & _
        '       " or  convert(varchar(5),'" & ToTime.TimeOfDay.ToString & "',108) between CONVERT(varchar(5),FROM_TIME,108) and CONVERT(varchar(5),TO_TIME,108) " & _
        '       " or CONVERT(varchar(5),FROM_TIME,108) between convert(varchar(5),'" & FromTime.TimeOfDay.ToString & "',108) and  convert(varchar(5),'" & ToTime.TimeOfDay.ToString & "',108) " & _
        '       " or CONVERT(varchar(5),TO_TIME,108) between convert(varchar(5),'" & FromTime.TimeOfDay.ToString & "',108) and  convert(varchar(5),'" & ToTime.TimeOfDay.ToString & "',108) ) and code <>'" & ExcludeCode & "'  order by FROM_TIME "

        strq = "SELECT CODE,PROJECT_CODE,CUST_CODE,TASK_DATE,FROM_TIME,TO_TIME FROM TSPL_PJC_TIMESHEET where EMP_CODE='" & EmpCode & "' and TASK_DATE='" & clsCommon.GetPrintDate(TaskDate, "dd/MMM/yyyy") & "'" & _
               " and ((convert(varchar(5),'" & FromTime.TimeOfDay.ToString & "',108) > CONVERT(varchar(5),FROM_TIME,108) and convert(varchar(5),'" & FromTime.TimeOfDay.ToString & "',108)< CONVERT(varchar(5),TO_TIME,108)) " & _
               " or  (convert(varchar(5),'" & ToTime.TimeOfDay.ToString & "',108) > CONVERT(varchar(5),FROM_TIME,108) and convert(varchar(5),'" & ToTime.TimeOfDay.ToString & "',108)< CONVERT(varchar(5),TO_TIME,108)) " & _
               " or (CONVERT(varchar(5),FROM_TIME,108) > convert(varchar(5),'" & FromTime.TimeOfDay.ToString & "',108) and CONVERT(varchar(5),FROM_TIME,108)< convert(varchar(5),'" & ToTime.TimeOfDay.ToString & "',108)) " & _
               " or (CONVERT(varchar(5),TO_TIME,108) > convert(varchar(5),'" & FromTime.TimeOfDay.ToString & "',108) and CONVERT(varchar(5),TO_TIME,108)< convert(varchar(5),'" & ToTime.TimeOfDay.ToString & "',108)) ) and code <>'" & ExcludeCode & "'  order by FROM_TIME "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq, trans)
        Dim repeated As Boolean = False
        For ii As Integer = 0 To dt.Rows.Count - 1
            repeated = True
            Throw New Exception("Repeated time Entered on " + clsCommon.GetPrintDate(TaskDate, "dd/MMM/yyyy") + Environment.NewLine + "Project-" + clsCommon.myCstr(dt.Rows(ii)("PROJECT_CODE")) + " " + clsCommon.GetPrintDate(dt.Rows(ii)("FROM_TIME"), "hh:mm tt") + " To " + clsCommon.GetPrintDate(dt.Rows(ii)("TO_TIME"), "hh:mm tt") + Environment.NewLine + "Client-" + clsCommon.myCstr(dt.Rows(ii)("cust_code")))

        Next
        Return repeated
    End Function
    Public Shared Function GetBillingRate(ByVal EmpCode As String, ByVal CustCode As String) As Decimal
        Dim strq As String
        strq = "select TSPL_EMPLOYEE_MASTER.EMP_CODE,TSPL_EMPLOYEE_MASTER.UNIT_COST,COALESCE((case when TSPL_EMPLOYEE_MASTER.APPLY_ALL_CUST=0 " & _
               " then TSPL_PJC_CUSTOMER_BILLING_RATE.BILLING_RATE else TSPL_EMPLOYEE_MASTER.BILLING_RATE end),0) as BILLING_RATE from TSPL_EMPLOYEE_MASTER " & _
               " left join(select * from TSPL_PJC_CUSTOMER_BILLING_RATE where Cust_Code='" & CustCode & "' and EMP_CODE='" & EmpCode & "') as TSPL_PJC_CUSTOMER_BILLING_RATE " & _
               " on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_PJC_CUSTOMER_BILLING_RATE.EMP_CODE WHERE TSPL_EMPLOYEE_MASTER.EMP_CODE='" & EmpCode & "'"

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("BILLING_RATE")
        Else
            Return 0
        End If

    End Function
    Public Shared Function GetUnitCost(ByVal EmpCode As String) As Decimal
        Dim strq As String
        strq = "select TSPL_EMPLOYEE_MASTER.EMP_CODE,TSPL_EMPLOYEE_MASTER.UNIT_COST from TSPL_EMPLOYEE_MASTER " & _
               " WHERE TSPL_EMPLOYEE_MASTER.EMP_CODE='" & EmpCode & "'"

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("UNIT_COST")
        Else
            Return 0
        End If

    End Function

End Class
