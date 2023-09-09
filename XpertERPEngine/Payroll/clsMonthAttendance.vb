Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMonthAttendance
#Region "Variables"
    Public MTA_CODE As String
    Public PAY_PERIOD_CODE As String
    Public PAY_PERIOD_NAME As String
    Public REGISTER_TYPE As String
    Public ENTEREDBY_EMP_CODE
    Public DESCRIPTION As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public DATE_FROM As String
    Public DATE_TO As String
    Public ObjList As New List(Of clsMonthAttendanceDetail)
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public LOCATION_CODE As String = ""

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMonthAttendance
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

            Dim obj As New clsMonthAttendance
            obj = clsMonthAttendance.GetData(strCode, NavigatorType.Current, trans)
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(obj.PAY_PERIOD_CODE, NavigatorType.Current, trans)

            Dim qry As String

            'qry = "delete BM from TSPL_MONTHLY_ATTENDANCE_SUMMARY BM inner join TSPL_EMPLOYEE_MASTER EMP on EMP.BioMetricEmpID=BM.Emp_ID where EMP.LOCATION_CODE='" + obj.LOCATION_CODE + "'
            ' and CONVERT(Date, BM.Attendance_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(clspp.DATE_FROM, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, BM.Attendance_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(clspp.DATE_TO, "dd/MMM/yyyy") + "',103)  "
            'isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete BM from TSPL_BIOMETRIC_RAW_DATA_UPDATED BM inner join TSPL_EMPLOYEE_MASTER EMP on EMP.BioMetricEmpID=BM.Emp_ID where EMP.LOCATION_CODE='" + obj.LOCATION_CODE + "'
            ' and CONVERT(Date, BM.In_Out_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(clspp.DATE_FROM, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, BM.In_Out_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(clspp.DATE_TO, "dd/MMM/yyyy") + "',103)  "
            'isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MONTHLY_ATTENDANCE_DETAIL where MTA_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MONTHLY_ATTENDANCE where MTA_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved

    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select POSTED from TSPL_MONTHLY_ATTENDANCE where MTA_CODE='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "Update TSPL_MONTHLY_ATTENDANCE set POSTED = 0 where MTA_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMonthAttendance
        Dim obj As New clsMonthAttendance()
        Dim qry As String = "SELECT MA.*,(DATEDIFF(DAY,TPM.date_from,TPM.date_to)+1) as TOTAL_DAYS, " _
        & " TPM.PAY_PERIOD_NAME,TPM.DATE_FROM,TPM.DATE_TO  FROM TSPL_MONTHLY_ATTENDANCE MA" _
        & " INNER JOIN TSPL_PAYPERIOD_MASTER TPM ON MA.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and MTA_CODE = (select MIN(MTA_CODE) from TSPL_MONTHLY_ATTENDANCE)"
            Case NavigatorType.Last
                qry += " and MTA_CODE = (select Max(MTA_CODE) from TSPL_MONTHLY_ATTENDANCE)"
            Case NavigatorType.Next
                qry += " and MTA_CODE = (select Min(MTA_CODE) from TSPL_MONTHLY_ATTENDANCE where  MTA_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and MTA_CODE = (select Max(MTA_CODE) from TSPL_MONTHLY_ATTENDANCE where MTA_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and MTA_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.MTA_CODE = dt.Rows(0)("MTA_CODE")
            obj.PAY_PERIOD_CODE = dt.Rows(0)("PAY_PERIOD_CODE")
            obj.ENTEREDBY_EMP_CODE = dt.Rows(0)("ENTEREDBY_EMP_CODE")
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.REGISTER_TYPE = clsCommon.myCstr(dt.Rows(0)("REGISTER_TYPE"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            strCode = obj.MTA_CODE
            obj.DATE_FROM = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("DATE_FROM")), "dd/MMM/yyyy")
            obj.DATE_TO = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("DATE_TO")), "dd/MMM/yyyy")
        End If
        qry = " select  ROW_NUMBER ()over (order by MAD.EMP_CODE) as SNo, MA.MTA_CODE,MAD.EMP_CODE,EMP.EMP_NAME,convert(date,EMP.Joining_date,103) as DOJ,MAD.TOTAL_DAYS,MAD.PRESENT_DAYS,MAD.ABSENT_DAYS,MAD.PAYABLE_DAYS,MAD.HOLIDAYS_DAYS,WEEKLY_OFF_DAYS,MAD.LEAVE_DAYS, " _
        & " MAD.PAYABLE_DAYS,MAD.LOP_DAYS,MA.PAY_PERIOD_CODE,MAD.Earned_Leave,MAD.Casual_Leave," _
        & " MAD.Coff,MAD.Maternity_Leave,MAD.Medical_Leave," _
        & " MAD.Other_Leave FROM TSPL_MONTHLY_ATTENDANCE_DETAIL MAD INNER JOIN  TSPL_MONTHLY_ATTENDANCE MA ON MAD.MTA_CODE=MA.MTA_CODE " _
        & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON MAD.EMP_CODE=EMP.EMP_CODE where 2=2"
        qry += " and MAD.MTA_CODE = '" + strCode + "'"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim ObjList As New List(Of clsMonthAttendanceDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsMonthAttendanceDetail
                objtr.SNo = clsCommon.myCdbl(dr("SNo"))
                objtr.MTA_CODE = clsCommon.myCstr(dr("MTA_CODE"))
                objtr.empCode = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.empName = clsCommon.myCstr(dr("EMP_NAME"))
                objtr.DOJ = clsCommon.GetPrintDate(dr("DOJ"), "dd/MMM/yyyy")
                objtr.PayPeriodDays = clsCommon.myCstr(dr("TOTAL_DAYS"))
                objtr.PresentDays = clsCommon.myCstr(dr("PRESENT_DAYS"))
                objtr.AbsentDays = clsCommon.myCstr(dr("ABSENT_DAYS"))
                objtr.HolidayDays = clsCommon.myCstr(dr("HOLIDAYS_DAYS"))
                objtr.WEEKLY_OFF_DAYS = clsCommon.myCstr(dr("WEEKLY_OFF_DAYS"))
                'objtr.LeaveDays = clsCommon.myCdbl(dr("LEAVE_DAYS"))
                '===========Add By Rohit=================
                objtr.EarnedLeave = clsCommon.myCdbl(dr("Earned_Leave"))
                objtr.CasualLeave = clsCommon.myCdbl(dr("Casual_Leave"))
                objtr.Coff = clsCommon.myCdbl(dr("Coff"))
                objtr.MaternityLeave = clsCommon.myCdbl(dr("Maternity_Leave"))
                objtr.MedicalLeave = clsCommon.myCdbl(dr("Medical_Leave"))
                objtr.OtherLeave = clsCommon.myCdbl(dr("Other_Leave"))
                '=====================================
                objtr.PayDays = clsCommon.myCstr(dr("PAYABLE_DAYS"))
                objtr.LOPDays = clsCommon.myCstr(dr("LOP_DAYS"))
                objtr.PayDays = clsCommon.myCdbl(dr("PAYABLE_DAYS"))
                'objtr.ATTENDANCE_CODE = clsCommon.myCstr(dr("ATTENDANCE_CODE"))
                ObjList.Add(objtr)
            Next
        End If
        obj.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsMonthAttendance, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                If strCode = "" Then
                    obj.MTA_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MonthlyAttendance, "", "")
                Else
                    obj.MTA_CODE = strCode
                End If
            End If

            Dim qry As String = "delete from TSPL_MONTHLY_ATTENDANCE_DETAIL where MTA_CODE='" + obj.MTA_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If (clsCommon.myLen(obj.MTA_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()

            'clsCommon.AddColumnsForChange(coll, "MTA_CODE", obj.MTA_CODE)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "REGISTER_TYPE", "MT")
            clsCommon.AddColumnsForChange(coll, "ENTEREDBY_EMP_CODE", obj.ENTEREDBY_EMP_CODE, True)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "MTA_CODE", obj.MTA_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MONTHLY_ATTENDANCE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MONTHLY_ATTENDANCE", OMInsertOrUpdate.Update, "TSPL_MONTHLY_ATTENDANCE.MTA_CODE='" + obj.MTA_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMonthAttendanceDetail.SaveData(obj.MTA_CODE, obj.ObjList, trans, obj.PAY_PERIOD_CODE)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.MTA_CODE, obj.arrCustomFields, trans)
            If isSaved Then
                trans.Commit()
            Else
                isSaved = False
            End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function FinderForEmployee(ByVal strCode As String, ByVal isButtonClicked As Boolean, Optional ByVal strq As String = "", Optional ByVal cond As String = "") As clsEmployeeMaster
        Dim obj As clsEmployeeMaster = Nothing
        Dim qry As String
        If strq = "" Then
            qry = "select EMP_CODE as Code,Emp_Name as Name,Designation  from TSPL_EMPLOYEE_MASTER"
        Else
            qry = strq
        End If
        Dim WhrCls As String
        If cond = "" Then
            WhrCls = " MA.EMP_CODE IS NULL AND DA.EMP_CODE IS NULL AND HA.EMP_CODE IS NULL"
        Else
            WhrCls = cond
        End If

        strCode = clsCommon.ShowSelectForm("EmployeeFinder213", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select EMP_CODE as Code,Emp_Name as Name,Designation  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsEmployeeMaster()
                obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.Designation = clsCommon.myCstr(dt.Rows(0)("Designation"))
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsMonthAttendance = clsMonthAttendance.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.MTA_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MONTHLY_ATTENDANCE set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where MTA_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True

    End Function
    Public Shared Function GetLeaveBalance(ByVal Pay_Period_Code As String, ByVal EMP_Code As String, ByVal Pay_Head_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select BALANCE from TSPL_FUN_LEAVE_STATUS('" & Pay_Period_Code & "') where EMP_CODE='" & EMP_Code & "' and LEAVE_CODE='" & Pay_Head_Code & "'"
        Dim Balance As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return Balance

    End Function

End Class

Public Class clsMonthAttendanceDetail
#Region "Variables"
    Public SNo As Decimal
    Public MTA_CODE As String
    Public empCode As String
    Public empName As String
    Public DOJ As String
    Public PayPeriodDays As Decimal
    Public PresentDays As Decimal
    Public AbsentDays As Decimal
    Public PayDays As Decimal
    'Public LeaveDays As Decimal
    Public EarnedLeave As Decimal
    Public CasualLeave As Decimal
    Public Coff As Decimal
    Public MaternityLeave As Decimal
    Public MedicalLeave As Decimal
    Public OtherLeave As Decimal

    Public HolidayDays As Decimal
    Public WEEKLY_OFF_DAYS As Decimal
    Public PayableDays As Decimal
    Public LOPDays As Decimal
    Public ATTENDANCE_CODE As String
    Public Shared ObjList As List(Of clsMonthAttendanceDetail)
    'Public Const AttendanceCode As String = "MT"

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal ObjList As List(Of clsMonthAttendanceDetail), ByVal trans As SqlTransaction, Optional PAY_PERIOD_CODE As String = Nothing) As Boolean
        If (ObjList IsNot Nothing AndAlso ObjList.Count > 0) Then
            For Each obj As clsMonthAttendanceDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "MTA_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.empCode)
                clsCommon.AddColumnsForChange(coll, "TOTAL_DAYS", obj.PayPeriodDays)
                clsCommon.AddColumnsForChange(coll, "PRESENT_DAYS", obj.PresentDays)
                clsCommon.AddColumnsForChange(coll, "ABSENT_DAYS", obj.AbsentDays)
                clsCommon.AddColumnsForChange(coll, "HOLIDAYS_DAYS", obj.HolidayDays)
                clsCommon.AddColumnsForChange(coll, "WEEKLY_OFF_DAYS", obj.WEEKLY_OFF_DAYS)
                clsCommon.AddColumnsForChange(coll, "LEAVE_DAYS", obj.EarnedLeave + obj.CasualLeave + obj.Coff + obj.MaternityLeave + obj.MedicalLeave + obj.OtherLeave)
                clsCommon.AddColumnsForChange(coll, "PAYABLE_DAYS", obj.PayDays)
                clsCommon.AddColumnsForChange(coll, "LOP_DAYS", obj.AbsentDays)
                clsCommon.AddColumnsForChange(coll, "Earned_Leave", obj.EarnedLeave)
                clsCommon.AddColumnsForChange(coll, "Casual_Leave", obj.CasualLeave)
                clsCommon.AddColumnsForChange(coll, "COff", obj.Coff)
                clsCommon.AddColumnsForChange(coll, "Maternity_Leave", obj.MaternityLeave)
                clsCommon.AddColumnsForChange(coll, "Medical_Leave", obj.MedicalLeave)
                clsCommon.AddColumnsForChange(coll, "Other_Leave", obj.OtherLeave)
                '================================================================================
                'clsCommon.AddColumnsForChange(coll, "LOP_DAYS", obj.LOPDays)
                clsCommon.AddColumnsForChange(coll, "ATTENDANCE_CODE", obj.ATTENDANCE_CODE, True)

                Dim count As String = "select count(EMP_CODE) from TSPL_MONTHLY_ATTENDANCE_DETAIL inner join TSPL_MONTHLY_ATTENDANCE on TSPL_MONTHLY_ATTENDANCE.MTA_Code= TSPL_MONTHLY_ATTENDANCE_DETAIL.mta_code   where EMP_CODE='" & obj.empCode & "' and PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' "
                Dim check As Integer = clsDBFuncationality.getSingleValue(count, trans)
                If check > 0 Then
                    Throw New Exception("Please Check Attendance Employee, Same Employee is Already Exits.")
                    Exit Function
                End If

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MONTHLY_ATTENDANCE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class
