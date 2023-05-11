Imports common
Imports System.Data.SqlClient

Public Class clsBiometricAttendance
    Public FromDate As DateTime?
    Public ToDate As DateTime?
    Public LocationCode As String = Nothing
    Public arr As List(Of clsBiometricAttendanceDetail) = Nothing
    Public arrSummary As List(Of clsBiometricAttendanceSummary) = Nothing

    Public Shared Function SaveData(ByVal obj As clsBiometricAttendance) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = Nothing
        trans = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            isSaved = clsBiometricAttendanceDetail.SaveData(obj, trans)
            isSaved = isSaved And clsBiometricAttendanceSummary.SaveData(obj, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal LOCATION_CODE As String, ByVal PAY_PERIOD_CODE As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            'If (clsCommon.myLen(LOCATION_CODE) <= 0) Then
            '    Throw New Exception("Code not found to Delete")
            'End If

            'Dim obj As New clsMonthAttendance
            'obj = clsMonthAttendance.GetData(strCode, NavigatorType.Current, trans)
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(PAY_PERIOD_CODE, NavigatorType.Current, trans)

            Dim qry As String

            qry = "delete BM from TSPL_MONTHLY_ATTENDANCE_SUMMARY BM inner join TSPL_EMPLOYEE_MASTER EMP on EMP.BioMetricEmpID=BM.Emp_ID where EMP.LOCATION_CODE='" + LOCATION_CODE + "'
             and CONVERT(Date, BM.Attendance_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(clspp.DATE_FROM, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, BM.Attendance_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(clspp.DATE_TO, "dd/MMM/yyyy") + "',103)  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete BM from TSPL_BIOMETRIC_RAW_DATA_UPDATED BM inner join TSPL_EMPLOYEE_MASTER EMP on EMP.BioMetricEmpID=BM.Emp_ID where EMP.LOCATION_CODE='" + LOCATION_CODE + "'
             and CONVERT(Date, BM.In_Out_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(clspp.DATE_FROM, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, BM.In_Out_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(clspp.DATE_TO, "dd/MMM/yyyy") + "',103)  "
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved

    End Function

End Class

Public Class clsBiometricAttendanceDetail
    Public Machine_Sr_No As String = ""
    Public Emp_ID As String = Nothing
    Public In_Out_Date As DateTime?
    Public SYNC_STATUS As Integer

    Public Shared Function SaveData(ByVal obj As clsBiometricAttendance, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        'Dim trans As SqlTransaction = Nothing
        'trans = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete BM from TSPL_BIOMETRIC_RAW_DATA_UPDATED BM inner join TSPL_EMPLOYEE_MASTER EMP on EMP.BioMetricEmpID=BM.Emp_ID where EMP.LOCATION_CODE='" + obj.LocationCode + "'
             and CONVERT(Date, BM.In_Out_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, BM.In_Out_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(obj.ToDate, "dd/MMM/yyyy") + "',103)  "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            coll = New Hashtable()

            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                For Each objtr As clsBiometricAttendanceDetail In obj.arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Machine_Sr_No", objtr.Machine_Sr_No)
                    clsCommon.AddColumnsForChange(coll, "Emp_ID", objtr.Emp_ID)
                    clsCommon.AddColumnsForChange(coll, "In_Out_Date", clsCommon.GetPrintDate(objtr.In_Out_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", objtr.SYNC_STATUS, True)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BIOMETRIC_RAW_DATA_UPDATED", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            'trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class


Public Class clsBiometricAttendanceSummary
    Public Emp_ID As String = Nothing
    Public Attendance_Date As DateTime?
    Public Attendance_Status As String = ""

    Public Shared Function SaveData(ByVal obj As clsBiometricAttendance, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        'Dim trans As SqlTransaction = Nothing
        'trans = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete BM from TSPL_MONTHLY_ATTENDANCE_SUMMARY BM inner join TSPL_EMPLOYEE_MASTER EMP on EMP.BioMetricEmpID=BM.Emp_ID where EMP.LOCATION_CODE='" + obj.LocationCode + "'
             and CONVERT(Date, BM.Attendance_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, BM.Attendance_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(obj.ToDate, "dd/MMM/yyyy") + "',103)  "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            coll = New Hashtable()

            If obj.arrSummary IsNot Nothing AndAlso obj.arrSummary.Count > 0 Then
                For Each objtr As clsBiometricAttendanceSummary In obj.arrSummary
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Attendance_Status", objtr.Attendance_Status)
                    clsCommon.AddColumnsForChange(coll, "Emp_ID", objtr.Emp_ID)
                    clsCommon.AddColumnsForChange(coll, "Attendance_Date", clsCommon.GetPrintDate(objtr.Attendance_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MONTHLY_ATTENDANCE_SUMMARY", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            'trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class