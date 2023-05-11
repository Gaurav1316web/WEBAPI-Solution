Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsDailyAttendance

#Region "Variables"
    Public DLA_CODE As String
    Public PAY_PERIOD_CODE As String
    Public REGISTER_TYPE As String    
    Public DESCRIPTION As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public attendanceDate As Date
    Public PAY_PERIOD_NAME As String
    Public PP_TOTAL_DAYS As Integer
    Public Location_Code As String = ""
    ' '' grid columns
    'Public empCode As String
    'Public empName As String
    'Public firstHalf As String
    'Public secondHalf As String
    'Public Shared ObjList As List(Of clsDailyAttendance)
    Public Arr As New List(Of clsDailyAttendanceDetail)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDailyAttendance
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_DAILY_ATTENDANCE_DETAIL where DLA_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_DAILY_ATTENDANCE where DLA_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDailyAttendance
        Dim obj As New clsDailyAttendance()
        Dim objtr As New clsDailyAttendanceDetail()

        Dim ObjList As List(Of clsDailyAttendanceDetail) = New List(Of clsDailyAttendanceDetail)

        Dim qry As String = "SELECT MA.*," _
        & " (DATEDIFF(DAY,TPM.date_from,TPM.date_to)+1) as TOTAL_DAYS, " _
        & " TPM.PAY_PERIOD_NAME  FROM TSPL_DAILY_ATTENDANCE MA" _
        & " INNER JOIN TSPL_PAYPERIOD_MASTER TPM ON MA.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE  where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and DLA_CODE = (select MIN(DLA_CODE) from TSPL_DAILY_ATTENDANCE)"
            Case NavigatorType.Last
                qry += " and DLA_CODE = (select Max(DLA_CODE) from TSPL_DAILY_ATTENDANCE)"
            Case NavigatorType.Next
                qry += " and DLA_CODE = (select Min(DLA_CODE) from TSPL_DAILY_ATTENDANCE where  DLA_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and DLA_CODE = (select Max(DLA_CODE) from TSPL_DAILY_ATTENDANCE where DLA_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and DLA_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.DLA_CODE = dt.Rows(0)("DLA_CODE")
            strCode = dt.Rows(0)("DLA_CODE")
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.PP_TOTAL_DAYS = clsCommon.myCdbl(dt.Rows(0)("TOTAL_DAYS"))
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            If Not IsDBNull(dt.Rows(0)("Att_Date")) Then
                obj.attendanceDate = clsCommon.myCDate(dt.Rows(0)("Att_Date"))
            End If
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = "select DA.DLA_CODE,DAD.EMP_CODE,EMP.EMP_NAME,dad.ATTENDANCE_DATE,DAD.FIRST_HALF,DAD.SECOND_HALF,DA.PAY_PERIOD_CODE " _
        & " FROM TSPL_DAILY_ATTENDANCE_DETAIL DAD INNER JOIN  TSPL_DAILY_ATTENDANCE DA ON DAD.DLA_CODE=DA.DLA_CODE " _
        & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON DAD.EMP_CODE=EMP.EMP_CODE where 2=2"
        qry += " and DAD.DLA_CODE = '" + strCode + "' ORDER BY DAD.EMP_CODE,DAD.ATTENDANCE_DATE;"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsDailyAttendanceDetail()
                objtr.DLA_CODE = clsCommon.myCstr(dr("DLA_CODE"))
                objtr.empCode = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.empName = clsCommon.myCstr(dr("EMP_NAME"))
                objtr.attendanceDate = Format(clsCommon.myCDate(clsCommon.myCDate(dr("ATTENDANCE_DATE"))), "dd MMM yyyy")
                objtr.firstHalf = clsCommon.myCstr(dr("FIRST_HALF"))
                objtr.secondHalf = clsCommon.myCstr(dr("SECOND_HALF"))
                objtr.PAY_PERIOD_CODE = clsCommon.myCstr(dr("PAY_PERIOD_CODE"))
                ObjList.Add(objtr)
            Next
        End If

        obj.Arr = ObjList
        Return obj
    End Function
    Public Shared Function SaveData(ByVal obj As clsDailyAttendance, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsDailyAttendance, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        If isNewEntry Then
            If clsCommon.myLen(obj.DLA_CODE) <= 0 Then
                obj.DLA_CODE = clsERPFuncationality.GetNextCode(trans, obj.attendanceDate, clsDocType.DailyAttendance, "", "")
            End If
        End If
        Dim qry As String = "delete from TSPL_DAILY_ATTENDANCE_DETAIL where DLA_CODE='" & obj.DLA_CODE & "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim strDocNo As String = ""
        If (clsCommon.myLen(obj.DLA_CODE) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "DLA_CODE", obj.DLA_CODE)
        clsCommon.AddColumnsForChange(coll, "Att_Date", clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy"), True)
        clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
        clsCommon.AddColumnsForChange(coll, "REGISTER_TYPE", "DL")
        clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
        clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
        clsCommon.AddColumnsForChange(coll, "POSTED", "0")

        'clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
        'clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
        If isNewEntry Then
            'clsCommon.AddColumnsForChange(coll, "DLA_CODE", obj.DLA_CODE)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAILY_ATTENDANCE", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAILY_ATTENDANCE", OMInsertOrUpdate.Update, "TSPL_DAILY_ATTENDANCE.DLA_CODE='" + obj.DLA_CODE + "'", trans)
        End If
        isSaved = isSaved AndAlso clsDailyAttendanceDetail.SaveData(obj.DLA_CODE, obj, trans)

        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsDailyAttendance = clsDailyAttendance.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.DLA_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
            clsDailyAttendance.SaveCoffLeaveAllotment(obj, trans)
            Dim qry As String = "Update TSPL_DAILY_ATTENDANCE set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DLA_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveCoffLeaveAllotment(ByVal obj As clsDailyAttendance, ByVal trans As SqlTransaction) As Integer
        Dim PP_Start_Date As Date
        PP_Start_Date = clsPayPeriodMaster.GetFromDate(obj.PAY_PERIOD_CODE, trans)
        Dim Total As Integer = 0
        For Each objTr As clsDailyAttendanceDetail In obj.Arr
            If clsCommon.CompairString(objTr.firstHalf, "COFF") = CompairStringResult.Equal Or clsCommon.CompairString(objTr.secondHalf, "COFF") = CompairStringResult.Equal Then
                Dim Allot_Leave As Decimal = 0
                Dim Leave_Code As String = ""
                Dim AllotRemarks As String = "Automatic COFF allotted on Daily Attendance."

                Leave_Code = clsLeaveMaster.GetLeaveCodeByLeaveType("COFF", trans)
                If clsCommon.myLen(Leave_Code) <= 0 Then
                    trans.Rollback()
                    clsCommon.MyMessageBoxShow("Leave not created of type COFF.")
                    Return False
                End If

                If clsCommon.CompairString(objTr.firstHalf, "COFF") = CompairStringResult.Equal Then
                    Allot_Leave = Allot_Leave + 0.5
                End If
                If clsCommon.CompairString(objTr.secondHalf, "COFF") = CompairStringResult.Equal Then
                    Allot_Leave = Allot_Leave + 0.5
                End If

                If clsLeaveAllotment.AssignAndSave(objTr.empCode, Leave_Code, Allot_Leave, AllotRemarks, obj.PAY_PERIOD_CODE, PP_Start_Date, "", trans) Then
                    Total = Total = 1
                End If
            End If
        Next
        Return Total
    End Function
    Public Shared Function GetAttendanceStatus() As ArrayList
        Dim lstAtt As New ArrayList
        lstAtt.Add("A")
        lstAtt.Add("P")
        lstAtt.Add("H")
        lstAtt.Add("WO")
        lstAtt.Add("CL")
        lstAtt.Add("PL")
        lstAtt.Add("OD")
        lstAtt.Add("T")
        lstAtt.Add("COFF")
        lstAtt.Add("NJ") '' NOT JOINED
        lstAtt.Add("SEP") '' SEPARATED OR LEFT
        Dim qry As String = "select LEAVE_CODE from TSPL_LEAVE_MASTER where LEAVE_CODE not in ('A','P','H','WO','CL','PL','OD','T','COFF','NJ','SEP')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        For Each dr As DataRow In dt.Rows
            lstAtt.Add(dr.Item("LEAVE_CODE"))
        Next
        Return lstAtt
    End Function
    Public Shared Function CheckEmployeeAttendanceType(ByVal Emp_Code As String, ByVal Pay_Period As String, ByVal Attendance_Type As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = ""
        Dim PP As String = " select max(DATE_FROM) as DATE_FROM from (" & _
                           " select DATE_FROM from TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Pay_Period & "' " & _
                           " union all " & _
                           " select convert(date,Joining_date,103) as Joining from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & Emp_Code & "' " & _
                           " ) as PP"

        qry = " SELECT CURRENT_STATUS.EMP_CODE,CURRENT_STATUS.REVISION_NO,CURRENT_STATUS.ATTENDANCE_CODE,ATTD.ATTN_REGISTER_TYPE " & _
              " FROM TSPL_EMPLOYEE_STATUS AS  CURRENT_STATUS " & _
              " INNER JOIN ( " & _
              " select EMP_CODE,MAX(REVISION_NO) AS REVISION_NO " & _
              " from TSPL_EMPLOYEE_STATUS WHERE EMP_CODE='" & Emp_Code & "' " & _
              " and APPLICABLE_FROM<=(" & PP & ") " & _
              " GROUP BY EMP_CODE " & _
              " HAVING MAX(APPLICABLE_FROM)<=(" & PP & ") " & _
              " ) AS lAST_STATUS ON  CURRENT_STATUS.EMP_CODE=lAST_STATUS.EMP_CODE " & _
              " AND CURRENT_STATUS.REVISION_NO=lAST_STATUS.REVISION_NO " & _
              " left join TSPL_ATTENDANCE_MASTER AS ATTD ON CURRENT_STATUS.ATTENDANCE_CODE=ATTD.ATTENDANCE_CODE where CURRENT_STATUS.WORKING_STATUS='Working' "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Employee Status is not set till start date of " & Pay_Period & "")
            Return False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("ATTN_REGISTER_TYPE")), Attendance_Type) = CompairStringResult.Equal Then
            Return True
        Else
            Return False
                End If
    End Function
End Class

Public Class clsDailyAttendanceDetail
#Region "Variables"
    '' grid columns
    Public DLA_CODE As String
    Public PAY_PERIOD_CODE As String
    Public attendanceDate As Date
    Public empCode As String
    Public empName As String
    Public firstHalf As String
    Public secondHalf As String
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal obj As clsDailyAttendance, ByVal trans As SqlTransaction) As Boolean


        If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
            For Each objTr As clsDailyAttendanceDetail In obj.Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DLA_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "emp_Code", objTr.empCode)
                clsCommon.AddColumnsForChange(coll, "ATTENDANCE_DATE", clsCommon.GetPrintDate(objTr.attendanceDate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "FIRST_HALF", objTr.firstHalf)
                clsCommon.AddColumnsForChange(coll, "SECOND_HALF", objTr.secondHalf)

                Dim qry As String = "select count(DLA_CODE) from TSPL_DAILY_ATTENDANCE_DETAIL where emp_Code='" & objTr.empCode & "' and ATTENDANCE_DATE='" & clsCommon.GetPrintDate(objTr.attendanceDate, "dd/MMM/yyyy") & "' "
                Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                If Count <= 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAILY_ATTENDANCE_DETAIL", OMInsertOrUpdate.Insert, "TSPL_DAILY_ATTENDANCE_DETAIL.DLA_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAILY_ATTENDANCE_DETAIL", OMInsertOrUpdate.Update, "TSPL_DAILY_ATTENDANCE_DETAIL.emp_Code='" + objTr.empCode + "' and TSPL_DAILY_ATTENDANCE_DETAIL.ATTENDANCE_DATE='" & clsCommon.GetPrintDate(objTr.attendanceDate, "dd/MMM/yyyy") & "'", trans)
                End If

            Next

        End If

        Return True
    End Function
End Class
