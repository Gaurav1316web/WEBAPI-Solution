Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsGeneralHolidays

#Region "Variables"
    Public Code As String
    Public Description As String
    Public ATTENDANCE_CODE As String
    Public ATTENDANCE_NAME As String
    Public HOLIDAY_DATE As DateTime
    Public NATIONAL_HOLIDAY As Int16
    Public Location_Code As String = Nothing
    Public Division As String = Nothing
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsGeneralHolidays
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
            qry = "delete from TSPL_GENERAL_HOLIDAYS where GHOLIDAY_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsGeneralHolidays
        Dim obj As clsGeneralHolidays = Nothing
        Dim qry As String = "select TSPL_GENERAL_HOLIDAYS.GHOLIDAY_CODE, TSPL_GENERAL_HOLIDAYS.ATTENDANCE_CODE, TSPL_GENERAL_HOLIDAYS.DESCRIPTION, TSPL_GENERAL_HOLIDAYS.HOLIDAY_DATE,TSPL_GENERAL_HOLIDAYS.NATIONAL_HOLIDAY, TSPL_ATTENDANCE_MASTER.ATTENDANCE_NAME,TSPL_GENERAL_HOLIDAYS.Location_Code ,TSPL_GENERAL_HOLIDAYS.Division from TSPL_GENERAL_HOLIDAYS Left outer join TSPL_ATTENDANCE_MASTER on TSPL_ATTENDANCE_MASTER.ATTENDANCE_CODE = TSPL_GENERAL_HOLIDAYS.ATTENDANCE_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and GHOLIDAY_CODE = (select MIN(GHOLIDAY_CODE) from TSPL_GENERAL_HOLIDAYS)"
            Case NavigatorType.Last
                qry += " and GHOLIDAY_CODE = (select Max(GHOLIDAY_CODE) from TSPL_GENERAL_HOLIDAYS)"
            Case NavigatorType.Next
                qry += " and GHOLIDAY_CODE = (select Min(GHOLIDAY_CODE) from TSPL_GENERAL_HOLIDAYS where  GHOLIDAY_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and GHOLIDAY_CODE = (select Max(GHOLIDAY_CODE) from TSPL_GENERAL_HOLIDAYS where GHOLIDAY_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and GHOLIDAY_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsGeneralHolidays()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("GHOLIDAY_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.ATTENDANCE_CODE = clsCommon.myCstr(dt.Rows(0)("ATTENDANCE_CODE"))
            obj.HOLIDAY_DATE = clsCommon.myCDate(dt.Rows(0)("HOLIDAY_DATE"))
            obj.NATIONAL_HOLIDAY = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("NATIONAL_HOLIDAY")))
            obj.ATTENDANCE_NAME = clsCommon.myCstr(dt.Rows(0)("ATTENDANCE_NAME"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Division = clsCommon.myCstr(dt.Rows(0)("Division"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsGeneralHolidays, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ATTENDANCE_CODE", obj.ATTENDANCE_CODE)
            clsCommon.AddColumnsForChange(coll, "HOLIDAY_DATE", clsCommon.GetPrintDate(obj.HOLIDAY_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "NATIONAL_HOLIDAY ", obj.NATIONAL_HOLIDAY)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            '====
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Division", obj.Division)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If clsCommon.myLen(obj.Code) <= 0 Then
                    obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.HOLIDAY_DATE, "dd/MMM/yyyy"), clsDocType.GeneralHolidays, "", obj.Location_Code)
                    If clsCommon.myLen(obj.Code) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "GHOLIDAY_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_GENERAL_HOLIDAYS where GHOLIDAY_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GENERAL_HOLIDAYS", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GENERAL_HOLIDAYS", OMInsertOrUpdate.Update, "GHOLIDAY_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


End Class
