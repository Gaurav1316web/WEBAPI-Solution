Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAttendanceMaster

#Region "Variables"
    Public Code As String
    Public Description As String
    Public Name As String
    Public SALARY_DEPENDENT_ON_ATTEN As String
    Public OT_CODE As String
    Public OT_Name As String
    Public CALC_SAL_ON As String
    Public ATTN_REGISTER_TYPE As String

#End Region


    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_ATTENDANCE_MASTER.ATTENDANCE_CODE as [Code] ,TSPL_ATTENDANCE_MASTER.ATTENDANCE_NAME as [Attendance Name] ,TSPL_ATTENDANCE_MASTER.SALARY_DEPENDENT_ON_ATTEN as [Salary Dependent On Attendance] ,TSPL_ATTENDANCE_MASTER.OT_CODE as [OT Code] ,TSPL_ATTENDANCE_MASTER.CALC_SAL_ON as [Calculate Salary On] ,TSPL_ATTENDANCE_MASTER.ATTN_REGISTER_TYPE as [Attandance Register Type] ,TSPL_ATTENDANCE_MASTER.DESCRIPTION as [Description] ,TSPL_ATTENDANCE_MASTER.Created_By as [Created By] ,TSPL_ATTENDANCE_MASTER.Created_Date as [Created Date] ,TSPL_ATTENDANCE_MASTER.Modified_By as [Modified By] ,TSPL_ATTENDANCE_MASTER.Modified_Date as [Modified Date]  From TSPL_ATTENDANCE_MASTER   "
        str = clsCommon.ShowSelectForm("ATTMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsAttendanceMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_ATTENDANCE_MASTER where ATTENDANCE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAttendanceMaster
        Dim obj As clsAttendanceMaster = Nothing
        Dim qry As String = "select TSPL_ATTENDANCE_MASTER.*, TSPL_OT_MASTER.OT_NAME from TSPL_ATTENDANCE_MASTER Left outer join TSPL_OT_MASTER on TSPL_OT_MASTER.OT_CODE =TSPL_ATTENDANCE_MASTER.OT_CODE  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and ATTENDANCE_CODE = (select MIN(ATTENDANCE_CODE) from TSPL_ATTENDANCE_MASTER)"
            Case NavigatorType.Last
                qry += " and ATTENDANCE_CODE = (select Max(ATTENDANCE_CODE) from TSPL_ATTENDANCE_MASTER)"
            Case NavigatorType.Next
                qry += " and ATTENDANCE_CODE = (select Min(ATTENDANCE_CODE) from TSPL_ATTENDANCE_MASTER where  ATTENDANCE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and ATTENDANCE_CODE = (select Max(ATTENDANCE_CODE) from TSPL_ATTENDANCE_MASTER where ATTENDANCE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and ATTENDANCE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAttendanceMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("ATTENDANCE_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("ATTENDANCE_NAME"))
            obj.SALARY_DEPENDENT_ON_ATTEN = clsCommon.myCstr(dt.Rows(0)("SALARY_DEPENDENT_ON_ATTEN"))
            obj.OT_CODE = clsCommon.myCstr(dt.Rows(0)("OT_CODE"))
            obj.OT_Name = clsCommon.myCstr(dt.Rows(0)("OT_Name"))
            obj.CALC_SAL_ON = clsCommon.myCstr(dt.Rows(0)("CALC_SAL_ON"))
            obj.ATTN_REGISTER_TYPE = clsCommon.myCstr(dt.Rows(0)("ATTN_REGISTER_TYPE"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsAttendanceMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ATTENDANCE_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "OT_CODE", obj.OT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "CALC_SAL_ON", obj.CALC_SAL_ON)
            clsCommon.AddColumnsForChange(coll, "SALARY_DEPENDENT_ON_ATTEN", obj.SALARY_DEPENDENT_ON_ATTEN)
            clsCommon.AddColumnsForChange(coll, "ATTN_REGISTER_TYPE", obj.ATTN_REGISTER_TYPE)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ATTENDANCE_MASTER where ATTENDANCE_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.AttendanceMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "ATTENDANCE_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_ATTENDANCE_MASTER where ATTENDANCE_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ATTENDANCE_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")
                    Exit Function
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ATTENDANCE_MASTER", OMInsertOrUpdate.Update, "ATTENDANCE_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetCboAttRegDataTable() As DataTable
        Dim DT_AttReg As DataTable = New DataTable
        DT_AttReg.Columns.Add("Code", GetType(String))
        DT_AttReg.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_AttReg.NewRow()
        DR("Name") = "Leave Register"
        DR("Code") = "LR"
        DT_AttReg.Rows.Add(DR)

        DR = DT_AttReg.NewRow()
        DR("Name") = "Monthly"
        DR("Code") = "MT"
        DT_AttReg.Rows.Add(DR)

        DR = DT_AttReg.NewRow()
        DR("Name") = "Daily"
        DR("Code") = "DL"
        DT_AttReg.Rows.Add(DR)

        DR = DT_AttReg.NewRow()
        DR("Name") = "Hourly"
        DR("Code") = "HR"
        DT_AttReg.Rows.Add(DR)

        DT_AttReg.AcceptChanges()

        Return DT_AttReg
    End Function
    Public Shared Function GetCboSalaryDepDataTable() As DataTable
        Dim DT_SalaryDep As DataTable = New DataTable
        DT_SalaryDep.Columns.Add("Code", GetType(String))
        DT_SalaryDep.Columns.Add("Name", GetType(String))
        Dim DR As DataRow = DT_SalaryDep.NewRow()
        DR("Code") = "DEPENDENT"
        DR("Name") = "Dependent"
        DT_SalaryDep.Rows.Add(DR)

        DR = DT_SalaryDep.NewRow()
        DR("Code") = "INDEPENDENT"
        DR("Name") = "Independent"
        DT_SalaryDep.Rows.Add(DR)

        DT_SalaryDep.AcceptChanges()
        Return DT_SalaryDep
    End Function

    Public Shared Function GetCboSalaryCalOnDayDataTable() As DataTable
        Dim DT_SalaryCalOnDay As DataTable = New DataTable
        DT_SalaryCalOnDay.Columns.Add("Code", GetType(String))
        DT_SalaryCalOnDay.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_SalaryCalOnDay.NewRow()
        DR("Name") = "Actual days/Month"
        DR("Code") = "ADM"
        DT_SalaryCalOnDay.Rows.Add(DR)

        DR = DT_SalaryCalOnDay.NewRow()
        DR("Name") = "Only Working Days"
        DR("Code") = "WD"
        DT_SalaryCalOnDay.Rows.Add(DR)

        DR = DT_SalaryCalOnDay.NewRow()
        DR("Name") = "Only Working days+Weekly Holidays"
        DR("Code") = "WDWH"
        DT_SalaryCalOnDay.Rows.Add(DR)

        DR = DT_SalaryCalOnDay.NewRow()
        DR("Name") = "Only Working Days + Holidays"
        DR("Code") = "WDH"
        DT_SalaryCalOnDay.Rows.Add(DR)

        DR = DT_SalaryCalOnDay.NewRow()
        DR("Name") = "Fixed(30) Days/Month"
        DR("Code") = "FM"
        DT_SalaryCalOnDay.Rows.Add(DR)

        DR = DT_SalaryCalOnDay.NewRow()
        DR("Name") = "User Defined"
        DR("Code") = "UD"
        DT_SalaryCalOnDay.Rows.Add(DR)

        DT_SalaryCalOnDay.AcceptChanges()
        Return DT_SalaryCalOnDay
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Dim qry As String = "Select ATTENDANCE_NAME from TSPL_ATTENDANCE_MASTER where ATTENDANCE_CODE='" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function GetCodeByName(ByVal strName As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select ATTENDANCE_CODE from TSPL_ATTENDANCE_MASTER where ATTENDANCE_NAME = '" + strName + "' "
        Dim StrCode As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return StrCode
    End Function


    Public Shared Function GetRegisterDT(ByVal strPayPeriod As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += "  SELECT T1.EMP_CODE,T4.EMP_NAME,'" + strPayPeriod + "' AS PAY_PERIOD_CODE,T2.REGISTER_TYPE,T2.ATTENDANCE_DAYS,T2.PRESENT_DAYS,"
            qry += "  T2.HOLIDAY_DAYS,T2.LEAVE_DAYS,T2.ABSENT_DAYS FROM ("
            qry += "  SELECT T1.EMP_STATUS_CODE,T1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS  FROM TSPL_EMPLOYEE_STATUS T1 "
            qry += "  INNER JOIN (  SELECT EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO "
            qry += "  FROM TSPL_EMPLOYEE_STATUS  WHERE WORKING_STATUS='WORKING' GROUP BY EMP_CODE) AS T2"
            qry += "  ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS T1"
            qry += "  LEFT JOIN ( "
            qry += "  SELECT T1.*,'HR' AS REGISTER_TYPE FROM ("
            qry += "  SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, "
            qry += "  SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS,"
            qry += "  SUM((CASE WHEN T2.FIRST_HALF IN ('WO','H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO','H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS,"
            qry += "  SUM((CASE WHEN T2.FIRST_HALF IN ('CL','PL','EL') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('CL','PL','EL') THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS,"
            qry += "  SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS "
            qry += "  FROM TSPL_HOURLY_ATTENDANCE T1 INNER JOIN TSPL_HOURLY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE "
            qry += "  WHERE T1.POSTED=1 "
            If clsCommon.myLen(strPayPeriod) > 0 Then
                qry += " AND T1.PAY_PERIOD_CODE='" + strPayPeriod + "' "
            End If
            qry += "  GROUP BY T2.EMP_CODE) AS T1"

            qry += "  UNION ALL "
            qry += "  SELECT T1.*,'DL' AS REGISTER_TYPE FROM ( "
            qry += "  SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, "
            qry += "  SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS,"
            qry += "  SUM((CASE WHEN T2.FIRST_HALF IN ('WO','H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO','H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS,"
            qry += "  SUM((CASE WHEN T2.FIRST_HALF IN ('CL','PL','EL') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('CL','PL','EL') THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS,"
            qry += "  SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS"
            qry += "  FROM TSPL_DAILY_ATTENDANCE T1 INNER JOIN TSPL_DAILY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE "
            qry += "  WHERE T1.POSTED=1 "
            If clsCommon.myLen(strPayPeriod) > 0 Then
                qry += " and T1.PAY_PERIOD_CODE='" + strPayPeriod + "' "
            End If
            qry += "   GROUP BY T2.EMP_CODE) AS T1"

            qry += "  UNION ALL "
            qry += "  SELECT T2.EMP_CODE,T2.TOTAL_DAYS,T2.PRESENT_DAYS,T2.HOLIDAYS_DAYS,T2.LEAVE_DAYS,T2.ABSENT_DAYS,'MT' AS REGISTER_TYPE "
            qry += "  FROM TSPL_MONTHLY_ATTENDANCE T1 INNER JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL T2 ON T1.MTA_CODE=T2.MTA_CODE"
            qry += "  WHERE T1.POSTED=1 "
            If clsCommon.myLen(strPayPeriod) > 0 Then
                qry += " AND T1.PAY_PERIOD_CODE='" + strPayPeriod + "' "
            End If
            qry += "   ) AS T2 ON T1.EMP_CODE=T2.EMP_CODE "
            qry += "  LEFT JOIN TSPL_ATTENDANCE_MASTER T3 ON T1.ATTENDANCE_CODE=T3.ATTENDANCE_CODE"
            qry += "  LEFT JOIN TSPL_EMPLOYEE_MASTER T4 ON T1.EMP_CODE=T4.EMP_CODE ORDER BY T1.EMP_CODE;"
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetRegisterDTDetailed(ByVal strPayPeriod As String, ByVal Location_Code As String, ByVal Division_Code As String, ByVal IsMonthlyAttendance As Boolean) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            'qry += "  SELECT T1.EMP_CODE,T2.DLA_CODE AS ATTENDANCE_CODE,T2.REGISTER_TYPE,T2.ATTENDANCE_DATE,T2.IN_TIME,T2.OUT_TIME,"
            'qry += "  T2.FIRST_HALF,T2.SECOND_HALF FROM ("
            'qry += "  SELECT T1.EMP_STATUS_CODE,T1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS  FROM TSPL_EMPLOYEE_STATUS T1 "
            'qry += "  INNER JOIN (SELECT EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO "
            'qry += "  FROM TSPL_EMPLOYEE_STATUS  WHERE WORKING_STATUS='WORKING' GROUP BY EMP_CODE) AS T2"
            'qry += "  ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS T1"
            'qry += "  LEFT JOIN ( "
            'qry += "  SELECT T1.*,'HR' AS REGISTER_TYPE FROM ("
            'qry += "  SELECT T1.DLA_CODE,T2.EMP_CODE,T2.ATTENDANCE_DATE,T2.IN_TIME,T2.OUT_TIME,T2.FIRST_HALF,T2.SECOND_HALF"
            'qry += "  FROM TSPL_HOURLY_ATTENDANCE T1 INNER JOIN TSPL_HOURLY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE "
            'qry += "  WHERE T1.POSTED in (0,1) "
            'If clsCommon.myLen(strPayPeriod) > 0 Then
            '    qry += " and T1.PAY_PERIOD_CODE='" + strPayPeriod + "' "
            'End If
            'If clsCommon.myLen(Location_Code) > 0 Then
            '    qry += " and EMP.Location_Code='" & Location_Code & "' "
            'End If
            'If clsCommon.myLen(Division_Code) > 0 Then
            '    qry += " and EMP.DEVISION_CODE='" & Division_Code & "' "
            'End If
            'qry += "  ) AS T1"
            'qry += "  UNION ALL "
            'qry += "  SELECT T1.*,'DL' AS REGISTER_TYPE FROM ( "
            'qry += "  SELECT T1.DLA_CODE,T2.EMP_CODE,T2.ATTENDANCE_DATE,NULL AS IN_TIME,NULL AS OUT_TIME, T2.FIRST_HALF,T2.SECOND_HALF"
            'qry += "  FROM TSPL_DAILY_ATTENDANCE T1 INNER JOIN TSPL_DAILY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE "
            'qry += "  WHERE T1.POSTED=1 "
            'If clsCommon.myLen(strPayPeriod) > 0 Then
            '    qry += " and T1.PAY_PERIOD_CODE='" + strPayPeriod + "' "
            'End If
            'qry += "  ) AS T1"
            'qry += "  ) AS T2 ON T1.EMP_CODE=T2.EMP_CODE "
            'qry += "  LEFT JOIN TSPL_ATTENDANCE_MASTER T3 ON T1.ATTENDANCE_CODE=T3.ATTENDANCE_CODE"
            'qry += "  ORDER BY T1.EMP_CODE,T2.ATTENDANCE_DATE"
            If IsMonthlyAttendance = False Then

                qry = "select * from ( select FH.EmployeeCode,FH.[Employee Name],[Location Code],[Location Name],[Division Code],[Division Name],FH.[Pay Period], [1 st(FH)], [1 st(SH)], [2 nd(FH)], [2 nd(SH)], " & _
             " [3 rd(FH)], [3 rd(SH)], [4 th(FH)], [4 th(SH)],  [5 th(FH)], [5 th(SH)], [6 th(FH)], [6 th(SH)], [7 th(FH)], [7 th(SH)] , " & _
             " [8 th(FH)], [8 th(SH)], [9 th(FH)], [9 th(SH)], [10 th(FH)], [10 th(SH)], [11 th(FH)], [11 th(SH)], [12 th(FH)], [12 th(SH)], " & _
             " [13 th(FH)], [13 th(SH)], [14 th(FH)], [14 th(SH)],  [15 th(FH)], [15 th(SH)], [16 th(FH)], [16 th(SH)], [17 th(FH)], [17 th(SH)]," & _
             " [18 th(FH)], [18 th(SH)], [19 th(FH)], [19 th(SH)], [20 th(FH)], [20 th(SH)], [21 st(FH)], [21 st(SH)], [22 nd(FH)], [22 nd(SH)], " & _
             " [23 rd(FH)], [23 rd(SH)], [24 th(FH)], [24 th(SH)], [25 th(FH)], [25 th(SH)], [26 th(FH)], [26 th(SH)], [27 th(FH)], [27 th(SH)], " & _
             " [28 th(FH)], [28 th(SH)], [29 th(FH)], [29 th(SH)], [30 th(FH)], [30 th(SH)], [31 st(FH)], [31 st(SH)] from " & _
             " ( " & _
             " select * from ( " & _
             " select DAD.EMP_CODE AS [EmployeeCode],Emp.Emp_Name as [Employee Name],EMP.Location_Code as [Location Code],Loc.Location_Desc as [Location Name] ,EMP.DEVISION_CODE as [Division Code],Div.Devision_Name as [Division Name],da.PAY_PERIOD_CODE as [Pay Period], " & _
             " (cast(day(dad.Attendance_date)  as varchar)+ case when day(dad.Attendance_date) in (1,21,31) then ' st(FH)' when day(dad.Attendance_date) in (2,22) " & _
             " then ' nd(FH)' when day(dad.Attendance_date) in (3,23) then ' rd(FH)' " & _
             " when day(dad.Attendance_date) in (4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,24,25,26,27,28,29,30) then ' th(FH)' end) as Attendance_Day_FH, " & _
             " DAD.FIRST_HALF from TSPL_DAILY_ATTENDANCE_DETAIL DAD " & _
             " inner join TSPL_DAILY_ATTENDANCE DA ON DA.DLA_CODE=DAD.DLA_CODE " & _
             " left join TSPL_EMPLOYEE_MASTER Emp on dad.EMP_CODE=emp.EMP_CODE " & _
             " left join TSPL_LOCATION_MASTER Loc on EMP.LOCATION_CODE=Loc.Location_Code" & _
             " left join TSPL_DEVISION_MASTER Div on EMP.DEVISION_CODE=Div.DEVISION_CODE " & _
             " where DA.Pay_Period_Code='" & strPayPeriod & "' " & _
             " ) as Attd  " & _
             " pivot ( max(attd.First_Half) for Attd.Attendance_Day_FH in  ([1 st(FH)],[2 nd(FH)],[3 rd(FH)],[4 th(FH)],[5 th(FH)],[6 th(FH)],[7 th(FH)],[8 th(FH)],[9 th(FH)],[10 th(FH)], " & _
             " [11 th(FH)],[12 th(FH)],[13 th(FH)],[14 th(FH)], " & _
             " [15 th(FH)],[16 th(FH)],[17 th(FH)],[18 th(FH)],[19 th(FH)],[20 th(FH)],[21 st(FH)],[22 nd(FH)],[23 rd(FH)],[24 th(FH)],[25 th(FH)],[26 th(FH)],[27 th(FH)],[28 th(FH)],[29 th(FH)],[30 th(FH)],[31 st(FH)])) Pvt " & _
             " ) as FH " & _
             " left join  " & _
             " ( " & _
             " select * from ( " & _
             " select DAD.EMP_CODE AS [EmployeeCode],Emp.Emp_Name as [Employee Name],da.PAY_PERIOD_CODE as [Pay Period], " & _
             " (cast(day(dad.Attendance_date)  as varchar)+ case when day(dad.Attendance_date) in (1,21,31) then ' st(SH)' when day(dad.Attendance_date) in (2,22) then ' nd(SH)' when day(dad.Attendance_date) in (3,23) then ' rd(SH)' " & _
             " when day(dad.Attendance_date) in (4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,24,25,26,27,28,29,30) then ' th(SH)' end) as Attendance_Day_SH, " & _
             " DAD.SECOND_HALF from TSPL_DAILY_ATTENDANCE_DETAIL DAD " & _
             " inner join TSPL_DAILY_ATTENDANCE DA ON DA.DLA_CODE=DAD.DLA_CODE " & _
             " left join TSPL_EMPLOYEE_MASTER Emp on dad.EMP_CODE=emp.EMP_CODE " & _
             " where DA.Pay_Period_Code='" & strPayPeriod & "'   " & _
             " ) as Attd " & _
             " pivot ( max(attd.SECOND_HALF) for Attd.Attendance_Day_SH in  ([1 st(SH)],[2 nd(SH)],[3 rd(SH)],[4 th(SH)],[5 th(SH)],[6 th(SH)],[7 th(SH)],[8 th(SH)],[9 th(SH)],[10 th(SH)], " & _
             " [11 th(SH)],[12 th(SH)],[13 th(SH)],[14 th(SH)], " & _
             " [15 th(SH)],[16 th(SH)],[17 th(SH)],[18 th(SH)],[19 th(SH)],[20 th(SH)],[21 st(SH)],[22 nd(SH)],[23 rd(SH)],[24 th(SH)],[25 th(SH)],[26 th(SH)],[27 th(SH)],[28 th(SH)],[29 th(SH)],[30 th(SH)],[31 st(SH)])) Pvt) as SH on FH.EmployeeCode=sh.EmployeeCode and fh.[Pay Period]=sh.[Pay Period]"

                qry += Environment.NewLine
                qry += " Union All "
                qry += " select FH.EmployeeCode,FH.[Employee Name],[Location Code],[Location Name],[Division Code],[Division Name],FH.[Pay Period], [1 st(FH)], [1 st(SH)], [2 nd(FH)], [2 nd(SH)], " & _
             " [3 rd(FH)], [3 rd(SH)], [4 th(FH)], [4 th(SH)],  [5 th(FH)], [5 th(SH)], [6 th(FH)], [6 th(SH)], [7 th(FH)], [7 th(SH)] , " & _
             " [8 th(FH)], [8 th(SH)], [9 th(FH)], [9 th(SH)], [10 th(FH)], [10 th(SH)], [11 th(FH)], [11 th(SH)], [12 th(FH)], [12 th(SH)], " & _
             " [13 th(FH)], [13 th(SH)], [14 th(FH)], [14 th(SH)],  [15 th(FH)], [15 th(SH)], [16 th(FH)], [16 th(SH)], [17 th(FH)], [17 th(SH)]," & _
             " [18 th(FH)], [18 th(SH)], [19 th(FH)], [19 th(SH)], [20 th(FH)], [20 th(SH)], [21 st(FH)], [21 st(SH)], [22 nd(FH)], [22 nd(SH)], " & _
             " [23 rd(FH)], [23 rd(SH)], [24 th(FH)], [24 th(SH)], [25 th(FH)], [25 th(SH)], [26 th(FH)], [26 th(SH)], [27 th(FH)], [27 th(SH)], " & _
             " [28 th(FH)], [28 th(SH)], [29 th(FH)], [29 th(SH)], [30 th(FH)], [30 th(SH)], [31 st(FH)], [31 st(SH)] from " & _
             " ( " & _
             " select * from ( " & _
             " select DAD.EMP_CODE AS [EmployeeCode],Emp.Emp_Name as [Employee Name],EMP.Location_Code as [Location Code],Loc.Location_Desc as [Location Name] ,EMP.DEVISION_CODE as [Division Code],Div.Devision_Name as [Division Name],da.PAY_PERIOD_CODE as [Pay Period], " & _
             " (cast(day(dad.Attendance_date)  as varchar)+ case when day(dad.Attendance_date) in (1,21,31) then ' st(FH)' when day(dad.Attendance_date) in (2,22) " & _
             " then ' nd(FH)' when day(dad.Attendance_date) in (3,23) then ' rd(FH)' " & _
             " when day(dad.Attendance_date) in (4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,24,25,26,27,28,29,30) then ' th(FH)' end) as Attendance_Day_FH, " & _
             " DAD.FIRST_HALF from TSPL_HOURLY_ATTENDANCE_DETAIL DAD " & _
             " inner join TSPL_HOURLY_ATTENDANCE DA ON DA.DLA_CODE=DAD.DLA_CODE " & _
             " left join TSPL_EMPLOYEE_MASTER Emp on dad.EMP_CODE=emp.EMP_CODE " & _
             " left join TSPL_LOCATION_MASTER Loc on EMP.LOCATION_CODE=Loc.Location_Code" & _
             " left join TSPL_DEVISION_MASTER Div on EMP.DEVISION_CODE=Div.DEVISION_CODE " & _
             " where DA.Pay_Period_Code='" & strPayPeriod & "' " & _
             " ) as Attd  " & _
             " pivot ( max(attd.First_Half) for Attd.Attendance_Day_FH in  ([1 st(FH)],[2 nd(FH)],[3 rd(FH)],[4 th(FH)],[5 th(FH)],[6 th(FH)],[7 th(FH)],[8 th(FH)],[9 th(FH)],[10 th(FH)], " & _
             " [11 th(FH)],[12 th(FH)],[13 th(FH)],[14 th(FH)], " & _
             " [15 th(FH)],[16 th(FH)],[17 th(FH)],[18 th(FH)],[19 th(FH)],[20 th(FH)],[21 st(FH)],[22 nd(FH)],[23 rd(FH)],[24 th(FH)],[25 th(FH)],[26 th(FH)],[27 th(FH)],[28 th(FH)],[29 th(FH)],[30 th(FH)],[31 st(FH)])) Pvt " & _
             " ) as FH " & _
             " left join  " & _
             " ( " & _
             " select * from ( " & _
             " select DAD.EMP_CODE AS [EmployeeCode],Emp.Emp_Name as [Employee Name],da.PAY_PERIOD_CODE as [Pay Period], " & _
             " (cast(day(dad.Attendance_date)  as varchar)+ case when day(dad.Attendance_date) in (1,21,31) then ' st(SH)' when day(dad.Attendance_date) in (2,22) then ' nd(SH)' when day(dad.Attendance_date) in (3,23) then ' rd(SH)' " & _
             " when day(dad.Attendance_date) in (4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,24,25,26,27,28,29,30) then ' th(SH)' end) as Attendance_Day_SH, " & _
             " DAD.SECOND_HALF from TSPL_HOURLY_ATTENDANCE_DETAIL DAD " & _
             " inner join TSPL_HOURLY_ATTENDANCE DA ON DA.DLA_CODE=DAD.DLA_CODE " & _
             " left join TSPL_EMPLOYEE_MASTER Emp on dad.EMP_CODE=emp.EMP_CODE " & _
             " where DA.Pay_Period_Code='" & strPayPeriod & "'   " & _
             " ) as Attd " & _
             " pivot ( max(attd.SECOND_HALF) for Attd.Attendance_Day_SH in  ([1 st(SH)],[2 nd(SH)],[3 rd(SH)],[4 th(SH)],[5 th(SH)],[6 th(SH)],[7 th(SH)],[8 th(SH)],[9 th(SH)],[10 th(SH)], " & _
             " [11 th(SH)],[12 th(SH)],[13 th(SH)],[14 th(SH)], " & _
             " [15 th(SH)],[16 th(SH)],[17 th(SH)],[18 th(SH)],[19 th(SH)],[20 th(SH)],[21 st(SH)],[22 nd(SH)],[23 rd(SH)],[24 th(SH)],[25 th(SH)],[26 th(SH)],[27 th(SH)],[28 th(SH)],[29 th(SH)],[30 th(SH)],[31 st(SH)])) Pvt) as SH on FH.EmployeeCode=sh.EmployeeCode and fh.[Pay Period]=sh.[Pay Period]) as Final where 2=2 "

                If clsCommon.myLen(Location_Code) > 0 Then
                    qry += " and Final.[Location Code]='" & Location_Code & "' "
                End If
                If clsCommon.myLen(Division_Code) > 0 Then
                    qry += " and Final.[DIVISION CODE]='" & Division_Code & "' "
                End If
            Else
                qry = "select * from (select MA.MTA_CODE AS [Document No],EMP.LOCATION_CODE AS [Location Code],Loc.Location_Desc as [Location Name]," & _
                   " EMP.DEVISION_CODE as [Devision Code],Div.DEVISION_NAME as [Devision Name],MA.PAY_PERIOD_CODE as [Pay Period Code],MAD.TOTAL_DAYS as [Total Days], " & _
                   " MAD.EMP_CODE as [Employee Code],MAD.PRESENT_DAYS as [Present days], " & _
                   " MAD.ABSENT_DAYS as [Absent Days],MAD.HOLIDAYS_DAYS as [Holidays Day],MAD.WEEKLY_OFF_DAYS as [Weekly Off Days], " & _
                   " MAD.Earned_Leave as [EL],MAD.Medical_Leave as [CL],MAD.Maternity_Leave as [Maternity Leave],MAD.Medical_Leave as [Medical Leave], " & _
                   " MAD.Coff as [Coff],MAD.Other_Leave as [Other Leave],MAD.PAYABLE_DAYS as [Payable Days] from TSPL_MONTHLY_ATTENDANCE_DETAIL MAD " & _
                   " inner join TSPL_MONTHLY_ATTENDANCE MA on MAD.MTA_CODE=MA.MTA_CODE " & _
                   " inner join TSPL_EMPLOYEE_MASTER EMP on MAD.EMP_CODE=EMP.EMP_CODE " & _
                   " left join TSPL_LOCATION_MASTER Loc on EMP.LOCATION_CODE=Loc.Location_Code " & _
                   " left join TSPL_DEVISION_MASTER Div on EMP.DEVISION_CODE=Div.DEVISION_CODE) as Final where 2=2"
                If clsCommon.myLen(strPayPeriod) > 0 Then
                    qry += " and Final.[Pay Period Code]='" & strPayPeriod & "' "
                End If
                If clsCommon.myLen(Location_Code) > 0 Then
                    qry += " and Final.[Location Code]='" & Location_Code & "' "
                End If
                If clsCommon.myLen(Division_Code) > 0 Then
                    qry += " and Final.[DeVISION CODE]='" & Division_Code & "' "
                End If
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select ATTENDANCE_CODE from TSPL_ATTENDANCE_MASTER where ATTENDANCE_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetMusterRollDT(ByVal strPayPeriod As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry = " with CTE_Attd as " & _
                  " (" & _
                  " SELECT T1.*,'HR' AS REGISTER_TYPE FROM (  SELECT T1.DLA_CODE,T1.PAY_PERIOD_CODE,T2.EMP_CODE,T2.ATTENDANCE_DATE,T2.IN_TIME,T2.OUT_TIME,T2.FIRST_HALF,T2.SECOND_HALF  FROM TSPL_HOURLY_ATTENDANCE T1 INNER JOIN TSPL_HOURLY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE  ) AS T1  " & _
                  " UNION ALL " & _
                  " SELECT T1.*,'DL' AS REGISTER_TYPE FROM (   SELECT T1.DLA_CODE,T1.PAY_PERIOD_CODE,T2.EMP_CODE,T2.ATTENDANCE_DATE,NULL AS IN_TIME,NULL AS OUT_TIME, T2.FIRST_HALF,T2.SECOND_HALF  FROM TSPL_DAILY_ATTENDANCE T1 INNER JOIN TSPL_DAILY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE ) AS T1 " & _
                  " ) " & _
                  " SELECT MUSTER.EMP_CODE AS [Employee Id],emp.emp_name as [Employee Name],Muster.PAY_PERIOD_CODE as [Pay Period Code],[1 st],[2 nd],[3 rd],[4 th],[5 th],[6 th],[7 th],[8 th],[9 th], " & _
                  " [10 th],[11 th],[12 th],[13 th],[14 th],[15 th],[16 th],[17 th],[18 th],[19 th],[20 th],[21 st],[22 nd],[23 rd],[24 th],[25 th],[26 th], " & _
                  " [27 th],[28 th],[29 th],[30 th],[31 st],Present_Days as [Present Days],WO,H,CL,EL,COFF,Paid_Days as [Paid Days] FROM (select * from (  SELECT T1.EMP_CODE,Attd_Detail.PAY_PERIOD_CODE, " & _
                  " cast(day(Attd_Detail.ATTENDANCE_DATE) as varchar) + " & _
                  " (CASE WHEN day(Attd_Detail.ATTENDANCE_DATE) % 10=1 and day(Attd_Detail.ATTENDANCE_DATE)<>11 THEN ' st' WHEN day(Attd_Detail.ATTENDANCE_DATE) % 10=2 " & _
                  " and day(Attd_Detail.ATTENDANCE_DATE)<>12 THEN ' nd' WHEN day(Attd_Detail.ATTENDANCE_DATE) % 10=3 and day(Attd_Detail.ATTENDANCE_DATE)<>13 THEN ' rd' " & _
                  " else ' th' end) as Att_Day_I, Attd_Detail.FIRST_HALF,Attd_Summary.Present_Days,Attd_Summary.WO,Attd_Summary.H,Attd_Summary.CL,Attd_Summary.EL, " & _
                  " Attd_Summary.COFF,Attd_Summary.Paid_Days FROM (SELECT T1.EMP_STATUS_CODE,T1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS  FROM TSPL_EMPLOYEE_STATUS T1   INNER JOIN (SELECT EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO   FROM TSPL_EMPLOYEE_STATUS GROUP BY EMP_CODE) AS T2  ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS T1 " & _
                  " Left Join " & _
                  " CTE_Attd  AS Attd_Detail ON T1.EMP_CODE=Attd_Detail.EMP_CODE " & _
                  " LEFT JOIN TSPL_ATTENDANCE_MASTER T3 ON T1.ATTENDANCE_CODE=T3.ATTENDANCE_CODE " & _
                  " Left Join " & _
                  " (select EMP_CODE,PAY_PERIOD_CODE,SUM(Present_Days) as Present_Days,SUM(WO) as WO,SUM(H) as H,SUM(CL) as CL,SUM(EL) as EL,SUM(COFF) as COFF, " & _
                  " SUM(Paid_Days) as Paid_Days " & _
                  " from (select EMP_CODE,PAY_PERIOD_CODE,(case when FIRST_HALF='P' then 1 else 0 end) as Present_Days, " & _
                  " (case when FIRST_HALF='WO' then 1 else 0 end) as WO,(case when FIRST_HALF='H' then 1 else 0 end) as H, " & _
                  " (case when FIRST_HALF='CL' then 1 else 0 end) as CL,(case when FIRST_HALF in ('PL','EL') then 1 else 0 end) as EL," & _
                  " (case when FIRST_HALF='COFF' then 1 else 0 end) as COFF,(case when FIRST_HALF='A' then 0 else 1 end) as Paid_Days from CTE_Attd) as Summary " & _
                  " group by EMP_CODE,PAY_PERIOD_CODE) as Attd_Summary on T1.EMP_CODE=Attd_Summary.EMP_CODE " & _
                  " ) as att " & _
                  " PIVOT ( " & _
                  " MAX(FIRST_HALF) " & _
                  " FOR Att_Day_I IN ([1 st],[2 nd],[3 rd],[4 th],[5 th],[6 th],[7 th],[8 th],[9 th],[10 th],[11 th],[12 th],[13 th],[14 th],[15 th],[16 th], " & _
                  " [17 th],[18 th],[19 th],[20 th],[21 st],[22 nd],[23 rd],[24 th],[25 th],[26 th],[27 th],[28 th],[29 th],[30 th],[31 st])) as pvt) AS Muster " & _
                  " INNER JOIN TSPL_EMPLOYEE_MASTER emp on Muster.EMP_CODE=emp.EMP_CODE " & _
                  " where Muster.PAY_PERIOD_CODE='" & strPayPeriod & "' order by Muster.EMP_CODE "
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Shared Function GetRegisterDTDetailed(p1 As String, p2 As String, p3 As String) As DataTable
        Throw New NotImplementedException
    End Function

End Class
