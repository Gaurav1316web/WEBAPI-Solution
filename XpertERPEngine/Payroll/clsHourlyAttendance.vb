Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsHourlyAttendance

#Region "Variables"

    Public DLA_CODE As String
    Public PAY_PERIOD_CODE As String
    Public REGISTER_TYPE As String
    Public ENTEREDBY_EMP_CODE
    Public DESCRIPTION As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public attendanceDate As Date
    Public PAY_PERIOD_NAME As String
    Public PP_TOTAL_DAYS As Integer
    'Public PayPeriodCode As String
    'Public EnteredBy As String
    '' grid columns
    Public empCode As String
    Public empName As String
    Public IN_TIME As Date? = Nothing
    Public OUT_TIME As Date? = Nothing
    Public firstHalf As String
    Public secondHalf As String

    Public Shared ObjList As List(Of clsHourlyAttendance)
    Public Arr As New List(Of clsHourlyAttendanceDetail)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsHourlyAttendance
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
            qry = "delete from TSPL_HOURLY_ATTENDANCE_DETAIL where DLA_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_HOURLY_ATTENDANCE where DLA_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsHourlyAttendance
        Dim obj As New clsHourlyAttendance()
        Dim objtr As New clsHourlyAttendance()

        ObjList = New List(Of clsHourlyAttendance)

        Dim qry As String = "SELECT MA.*," _
        & " (DATEDIFF(DAY,TPM.date_from,TPM.date_to)+1) as TOTAL_DAYS, " _
        & " TPM.PAY_PERIOD_NAME  FROM TSPL_HOURLY_ATTENDANCE MA" _
        & " INNER JOIN TSPL_PAYPERIOD_MASTER TPM ON MA.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE  where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and DLA_CODE = (select MIN(DLA_CODE) from TSPL_HOURLY_ATTENDANCE)"
            Case NavigatorType.Last
                qry += " and DLA_CODE = (select Max(DLA_CODE) from TSPL_HOURLY_ATTENDANCE)"
            Case NavigatorType.Next
                qry += " and DLA_CODE = (select Min(DLA_CODE) from TSPL_HOURLY_ATTENDANCE where  DLA_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and DLA_CODE = (select Max(DLA_CODE) from TSPL_HOURLY_ATTENDANCE where DLA_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and DLA_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.DLA_CODE = dt.Rows(0)("DLA_CODE")
            strCode = dt.Rows(0)("DLA_CODE")
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            'obj.attendanceDate = clsCommon.myCstr(dt.Rows(0)("attendance_date"))
            obj.PP_TOTAL_DAYS = clsCommon.myCdbl(dt.Rows(0)("TOTAL_DAYS"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.ENTEREDBY_EMP_CODE = clsCommon.myCstr(dt.Rows(0)("ENTEREDBY_EMP_CODE"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = "select DA.DLA_CODE,DAD.EMP_CODE,EMP.EMP_NAME,DAD.IN_TIME,DAD.OUT_TIME,DA.PAY_PERIOD_CODE,DAD.ATTENDANCE_DATE,DAD.FIRST_HALF,DAD.SECOND_HALF " _
        & " FROM TSPL_HOURLY_ATTENDANCE_DETAIL DAD INNER JOIN  TSPL_HOURLY_ATTENDANCE DA ON DAD.DLA_CODE=DA.DLA_CODE " _
        & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON DAD.EMP_CODE=EMP.EMP_CODE where 2=2"
        qry += " and DAD.DLA_CODE = '" + strCode + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsHourlyAttendance()
                objtr.DLA_CODE = clsCommon.myCstr(dr("DLA_CODE"))
                objtr.empCode = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.empName = clsCommon.myCstr(dr("EMP_NAME"))
                objtr.attendanceDate = clsCommon.myCDate(dr("ATTENDANCE_DATE"))
                If clsCommon.myLen(dr("IN_TIME")) > 0 Then
                    objtr.IN_TIME = clsCommon.myCstr(dr("IN_TIME"))
                End If
                If clsCommon.myLen(dr("OUT_TIME")) > 0 Then
                    objtr.OUT_TIME = clsCommon.myCstr(dr("OUT_TIME"))
                End If

                objtr.firstHalf = dr("FIRST_HALF")
                objtr.secondHalf = dr("SECOND_HALF")

                objtr.PAY_PERIOD_CODE = clsCommon.myCstr(dr("PAY_PERIOD_CODE"))
                ObjList.Add(objtr)
            Next
        End If

        clsHourlyAttendance.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsHourlyAttendance, ByVal objList As List(Of clsHourlyAttendance), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If isNewEntry Then
                If strCode = "" Then
                    obj.DLA_CODE = clsERPFuncationality.GetNextCode(trans, obj.attendanceDate, clsDocType.HourlyAttendance, "", "")
                Else
                    obj.DLA_CODE = strCode
                End If
            End If
            Dim qryPay As String = clsDBFuncationality.getSingleValue("select PAY_PERIOD_CODE from TSPL_HOURLY_ATTENDANCE where PAY_PERIOD_CODE='" + obj.PAY_PERIOD_CODE + "'", trans)
            If (clsCommon.myLen(qryPay)) > 0 AndAlso isNewEntry Then
                Throw New Exception("Pay Period Already Used.")
            End If

            Dim qry As String = "delete from TSPL_HOURLY_ATTENDANCE_DETAIL where DLA_CODE='" + obj.DLA_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.DLA_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DLA_CODE", obj.DLA_CODE)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "REGISTER_TYPE", "HL")
            clsCommon.AddColumnsForChange(coll, "ENTEREDBY_EMP_CODE", obj.ENTEREDBY_EMP_CODE, True)
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
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HOURLY_ATTENDANCE", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HOURLY_ATTENDANCE", OMInsertOrUpdate.Update, "TSPL_HOURLY_ATTENDANCE.DLA_CODE='" + obj.DLA_CODE + "'", trans)
            End If


            isSaved = isSaved AndAlso clsHourlyAttendanceDetail.SaveData(obj.DLA_CODE, objList, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsHourlyAttendance = clsHourlyAttendance.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.DLA_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_HOURLY_ATTENDANCE set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DLA_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsHourlyAttendanceDetail
#Region "Variables"
    Public empCode As String
    Public empName As String
    Public attendanceDate As Date?
    Public InTime As String
    Public OutTime As String
    Public Shared ObjList As List(Of clsMonthAttendance)
    'Public Const AttendanceCode As String = "MT"
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsHourlyAttendance), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsHourlyAttendance In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DLA_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "emp_Code", obj.empCode)
                clsCommon.AddColumnsForChange(coll, "ATTENDANCE_DATE", clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy"))
                If clsCommon.myLen(obj.IN_TIME) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "IN_TIME", Format(obj.IN_TIME, "hh:mm:ss tt"), True)
                Else
                    clsCommon.AddColumnsForChange(coll, "IN_TIME", Nothing, True)
                End If
                If clsCommon.myLen(obj.OUT_TIME) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "OUT_TIME", Format(obj.OUT_TIME, "hh:mm:ss tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "OUT_TIME", Nothing, True)
                End If

                clsCommon.AddColumnsForChange(coll, "FIRST_HALF", obj.firstHalf)
                clsCommon.AddColumnsForChange(coll, "SECOND_HALF", obj.secondHalf)

                Dim qry As String = "select count(DLA_CODE) from TSPL_HOURLY_ATTENDANCE_DETAIL where emp_Code='" & obj.empCode & "' and ATTENDANCE_DATE='" & clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy") & "' "
                Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                If Count <= 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HOURLY_ATTENDANCE_DETAIL", OMInsertOrUpdate.Insert, "TSPL_HOURLY_ATTENDANCE_DETAIL.DLA_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HOURLY_ATTENDANCE_DETAIL", OMInsertOrUpdate.Update, "TSPL_HOURLY_ATTENDANCE_DETAIL.emp_Code='" & obj.empCode & "' and TSPL_HOURLY_ATTENDANCE_DETAIL.ATTENDANCE_DATE='" & clsCommon.GetPrintDate(obj.attendanceDate, "dd/MMM/yyyy") & "'", trans)
                End If

            Next
        End If
        Return True
    End Function

End Class
