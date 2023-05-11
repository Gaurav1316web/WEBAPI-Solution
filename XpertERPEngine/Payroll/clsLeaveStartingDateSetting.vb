Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLeaveStartingDateSetting

#Region "Variables"
    Public EMP_CODE As String
    Public LEAVE_CODE As String
    Public ALLOT_STARTDATE As DateTime
    Public AVAIL_STARTDATE As DateTime
    Public Emp_Name As String
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal strLeaveCode As String) As clsLeaveStartingDateSetting
        Return GetData(strCode, strLeaveCode, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal strLeaveCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Or (clsCommon.myLen(strLeaveCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_LEAVE_STARTINGDATE where EMP_CODE ='" + strCode + "' and LEAVE_CODE ='" + strLeaveCode + "' "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal strLeaveCode As String, ByVal trans As SqlTransaction) As clsLeaveStartingDateSetting
        Dim obj As clsLeaveStartingDateSetting = Nothing
        Dim qry As String = "select TSPL_EMPLOYEE_MASTER.EMP_CODE, TSPL_LEAVE_STARTINGDATE.LEAVE_CODE, TSPL_LEAVE_STARTINGDATE.ALLOT_STARTDATE, TSPL_LEAVE_STARTINGDATE.AVAIL_STARTDATE, TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_EMPLOYEE_MASTER left outer join TSPL_LEAVE_STARTINGDATE on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_LEAVE_STARTINGDATE.EMP_CODE where 2=2"
        qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = '" + strCode + "'"
        If clsCommon.myLen(strLeaveCode) > 0 Then
            qry += " and TSPL_LEAVE_STARTINGDATE.LEAVE_CODE = '" + strLeaveCode + "'"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLeaveStartingDateSetting()
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.LEAVE_CODE = clsCommon.myCstr(dt.Rows(0)("LEAVE_CODE"))
            If clsCommon.myLen(dt.Rows(0)("ALLOT_STARTDATE")) > 0 Then
                obj.ALLOT_STARTDATE = clsCommon.GetPrintDate(dt.Rows(0)("ALLOT_STARTDATE"), "dd/MMM/yyyy")
            Else
                obj.ALLOT_STARTDATE = Nothing
            End If
            If clsCommon.myLen(dt.Rows(0)("ALLOT_STARTDATE")) > 0 Then
                obj.AVAIL_STARTDATE = clsCommon.GetPrintDate(dt.Rows(0)("AVAIL_STARTDATE"), "dd/MMM/yyyy")
            Else
                obj.AVAIL_STARTDATE = Nothing
            End If
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsLeaveStartingDateSetting, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "AVAIL_STARTDATE", clsCommon.GetPrintDate(obj.AVAIL_STARTDATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ALLOT_STARTDATE", clsCommon.GetPrintDate(obj.ALLOT_STARTDATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

            Dim qry As String = "SELECT Count(*) FROM TSPL_LEAVE_STARTINGDATE where EMP_CODE= '" & obj.EMP_CODE & "' and LEAVE_CODE ='" + obj.LEAVE_CODE + "' "
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                clsCommon.AddColumnsForChange(coll, "LEAVE_CODE", obj.LEAVE_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_STARTINGDATE", OMInsertOrUpdate.Insert, "")
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_STARTINGDATE", OMInsertOrUpdate.Update, "EMP_CODE='" + obj.EMP_CODE + "' and LEAVE_CODE ='" + obj.LEAVE_CODE + "' ")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
