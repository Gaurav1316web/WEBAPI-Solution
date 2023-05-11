Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsWeeklyHolidays

#Region "Variables"
    Public WKHOLIDAY_CODE As String
    Public WKHOLIDAY_NAME As String
    Public APPLICABLE_FROM As DateTime
    Public APPLY_IN As String
    Public APPLIED_ON As String
    Public WEEKDAY_NAME As String

    Public FSTWK_FSTHALF As Boolean
    Public FSTWK_SECHALF As Boolean

    Public SECWK_FSTHALF As Boolean
    Public SECWK_SECHALF As Boolean

    Public THDWK_FSTHALF As Boolean
    Public THDWK_SECHALF As Boolean

    Public FTHWK_FSTHALF As Boolean
    Public FTHWK_SECHALF As Boolean

    Public FIVWK_FSTHALF As Boolean
    Public FIVWK_SECHALF As Boolean

    '==============
    Public Division As String
    Public Location_Code As String
    Public arr As ArrayList = Nothing
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsWeeklyHolidays
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
            qry = "delete from TSPL_WEEKLY_HOLIDAYS_EMP_MAPPING where WKHOLIDAY_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "delete from TSPL_WEEKLY_HOLIDAYS where WKHOLIDAY_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsWeeklyHolidays
        Dim obj As clsWeeklyHolidays = Nothing
        Dim qry As String = "select * from TSPL_WEEKLY_HOLIDAYS where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and WKHOLIDAY_CODE = (select MIN(WKHOLIDAY_CODE) from TSPL_WEEKLY_HOLIDAYS)"
            Case NavigatorType.Last
                qry += " and WKHOLIDAY_CODE = (select Max(WKHOLIDAY_CODE) from TSPL_WEEKLY_HOLIDAYS)"
            Case NavigatorType.Next
                qry += " and WKHOLIDAY_CODE = (select Min(WKHOLIDAY_CODE) from TSPL_WEEKLY_HOLIDAYS where WKHOLIDAY_CODE >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and WKHOLIDAY_CODE = (select Max(WKHOLIDAY_CODE) from TSPL_WEEKLY_HOLIDAYS where WKHOLIDAY_CODE <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and WKHOLIDAY_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsWeeklyHolidays()
            obj.WKHOLIDAY_CODE = clsCommon.myCstr(dt.Rows(0)("WKHOLIDAY_CODE"))
            obj.WKHOLIDAY_NAME = clsCommon.myCstr(dt.Rows(0)("WKHOLIDAY_NAME"))
            If clsCommon.myLen(dt.Rows(0)("APPLICABLE_FROM")) > 0 Then
                obj.APPLICABLE_FROM = clsCommon.GetPrintDate(dt.Rows(0)("APPLICABLE_FROM"), "dd/MMM/yyyy")
            Else
                obj.APPLICABLE_FROM = Nothing
            End If
            obj.APPLY_IN = clsCommon.myCstr(dt.Rows(0)("APPLY_IN"))
            obj.APPLIED_ON = clsCommon.myCstr(dt.Rows(0)("APPLIED_ON"))
            obj.WEEKDAY_NAME = clsCommon.myCstr(dt.Rows(0)("WEEKDAY_NAME"))

            obj.FSTWK_FSTHALF = clsCommon.myCBool(dt.Rows(0)("FSTWK_FSTHALF"))
            obj.FSTWK_SECHALF = clsCommon.myCBool(dt.Rows(0)("FSTWK_SECHALF"))
            obj.SECWK_FSTHALF = clsCommon.myCBool(dt.Rows(0)("SECWK_FSTHALF"))
            obj.SECWK_SECHALF = clsCommon.myCBool(dt.Rows(0)("SECWK_SECHALF"))
            obj.THDWK_FSTHALF = clsCommon.myCBool(dt.Rows(0)("THDWK_FSTHALF"))
            obj.THDWK_SECHALF = clsCommon.myCBool(dt.Rows(0)("THDWK_SECHALF"))
            obj.FTHWK_FSTHALF = clsCommon.myCBool(dt.Rows(0)("FTHWK_FSTHALF"))
            obj.FTHWK_SECHALF = clsCommon.myCBool(dt.Rows(0)("FTHWK_SECHALF"))
            obj.FIVWK_FSTHALF = clsCommon.myCBool(dt.Rows(0)("FIVWK_FSTHALF"))
            obj.FIVWK_SECHALF = clsCommon.myCBool(dt.Rows(0)("FIVWK_SECHALF"))
            '===============
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Division = clsCommon.myCstr(dt.Rows(0)("Division"))

            Dim templist As New ArrayList
            qry = "SELECT TSPL_WEEKLY_HOLIDAYS_EMP_MAPPING.* FROM TSPL_WEEKLY_HOLIDAYS_EMP_MAPPING  where TSPL_WEEKLY_HOLIDAYS_EMP_MAPPING.WKHOLIDAY_CODE='" + obj.WKHOLIDAY_CODE + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    templist.Add(clsCommon.myCstr(dr("EMP_CODE")))
                Next
            End If
            obj.arr = templist

        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsWeeklyHolidays, ByVal strCode As String, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = Nothing
        trans = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Payroll", "Weekly Holidays", obj.Location_Code, obj.APPLICABLE_FROM, trans)
            If strCode = "" Then
                obj.WKHOLIDAY_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"), clsDocType.WeeklyHoliday, "", obj.Location_Code)
            Else
                obj.WKHOLIDAY_CODE = strCode
            End If

            Dim qry1 As String = "delete from TSPL_WEEKLY_HOLIDAYS_EMP_MAPPING where WKHOLIDAY_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry1, trans)

            clsCommon.AddColumnsForChange(coll, "WKHOLIDAY_NAME", obj.WKHOLIDAY_NAME)
            clsCommon.AddColumnsForChange(coll, "APPLY_IN", obj.APPLY_IN)
            clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "APPLIED_ON", obj.APPLIED_ON)
            clsCommon.AddColumnsForChange(coll, "WEEKDAY_NAME", obj.WEEKDAY_NAME)

            clsCommon.AddColumnsForChange(coll, "FSTWK_FSTHALF", obj.FSTWK_FSTHALF)
            clsCommon.AddColumnsForChange(coll, "FSTWK_SECHALF", obj.FSTWK_SECHALF)
            clsCommon.AddColumnsForChange(coll, "SECWK_FSTHALF", obj.SECWK_FSTHALF)
            clsCommon.AddColumnsForChange(coll, "SECWK_SECHALF", obj.SECWK_SECHALF)
            clsCommon.AddColumnsForChange(coll, "THDWK_FSTHALF", obj.THDWK_FSTHALF)
            clsCommon.AddColumnsForChange(coll, "THDWK_SECHALF", obj.THDWK_SECHALF)
            clsCommon.AddColumnsForChange(coll, "FTHWK_FSTHALF", obj.FTHWK_FSTHALF)
            clsCommon.AddColumnsForChange(coll, "FTHWK_SECHALF", obj.FTHWK_SECHALF)
            clsCommon.AddColumnsForChange(coll, "FIVWK_FSTHALF", obj.FIVWK_FSTHALF)
            clsCommon.AddColumnsForChange(coll, "FIVWK_SECHALF", obj.FIVWK_SECHALF)
            '=================
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Division", obj.Division)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "WKHOLIDAY_CODE", obj.WKHOLIDAY_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_WEEKLY_HOLIDAYS where WKHOLIDAY_CODE= '" & obj.WKHOLIDAY_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEEKLY_HOLIDAYS", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEEKLY_HOLIDAYS", OMInsertOrUpdate.Update, "WKHOLIDAY_CODE='" + obj.WKHOLIDAY_CODE + "'", trans)
            End If
            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                isSaved = isSaved AndAlso clsWeeklyHolidaysEmpMapping.SaveData(obj.WKHOLIDAY_CODE, obj.arr, trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetWeeklyHolidayList(Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsWeeklyHolidays)
        Dim objTr As New clsWeeklyHolidays
        Dim objList As New List(Of clsWeeklyHolidays)
        Dim qry As String = "select * from TSPL_WEEKLY_HOLIDAYS "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        For Each dr As DataRow In dt.Rows
            objTr = clsWeeklyHolidays.GetData(dr.Item("WKHOLIDAY_CODE"), NavigatorType.Current, trans)
            objList.Add(objTr)
        Next
        Return objList
    End Function
End Class

Public Class clsWeeklyHolidaysEmpMapping

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i As Integer = 0 To arr.Count - 1
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "WKHOLIDAY_CODE", DocNo)
                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", arr.Item(i))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEEKLY_HOLIDAYS_EMP_MAPPING", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class
