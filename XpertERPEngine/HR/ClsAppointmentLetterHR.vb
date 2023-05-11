Imports System.Data.SqlClient
Imports common


Public Class ClsAppointmentLetterHR

#Region "Variables"
    Public Applicant_Code As String = Nothing
    Public Appointment_Date As String = Nothing
    'Public Date_Of_Joining As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsAppointmentLetterHR, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Applicant_Code", obj.Applicant_Code, True)
            clsCommon.AddColumnsForChange(coll, "Appointment_Date", clsCommon.GetPrintDate(obj.Appointment_Date, "dd/MMM/yyyy hh:mm tt"))
            'clsCommon.AddColumnsForChange(coll, "Date_Of_Joining", clsCommon.GetPrintDate(obj.DateOfJoining, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))

            If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_APPOINTMENT_LETTER WHERE APPLICANT_CODE='" + obj.Applicant_Code + "'") <= 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))

                Dim qry1 As String = "SELECT Count(*) FROM TSPL_HR_APPOINTMENT_LETTER where APPLICANT_CODE= '" & obj.Applicant_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check = 0 Then
                    IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_APPOINTMENT_LETTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_APPOINTMENT_LETTER", OMInsertOrUpdate.Update, "APPLICANT_CODE='" + obj.Applicant_Code + "' AND Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsAppointmentLetterHR
        Dim obj As ClsAppointmentLetterHR = Nothing
        Dim Arr As List(Of ClsAppointmentLetterHR) = Nothing
        Dim qry As String = "select * from TSPL_HR_APPOINTMENT_LETTER where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'"
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_APPOINTMENT_LETTER.APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_APPOINTMENT_LETTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_APPOINTMENT_LETTER.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_APPOINTMENT_LETTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_APPOINTMENT_LETTER.APPLICANT_CODE = (select TOP 1 APPLICANT_CODE from TSPL_HR_APPOINTMENT_LETTER WHERE 1=1 " + whrclas + " and APPLICANT_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_APPOINTMENT_LETTER.APPLICANT_CODE = (select Min(APPLICANT_CODE) from TSPL_HR_APPOINTMENT_LETTER where APPLICANT_CODE > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_APPOINTMENT_LETTER.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_APPOINTMENT_LETTER where APPLICANT_CODE < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAppointmentLetterHR()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Appointment_Date = clsCommon.myCstr(dt.Rows(0)("Appointment_Date"))
            'obj.DateOfJoining = clsCommon.myCstr(dt.Rows(0)("Date_Of_Joining"))
        End If
        Return obj
    End Function
    '' ------------------------------------------------------- Nav. Query (=) ---------------------------------------------------------------
    Public Shared Function GetDataForNav(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsAppointmentLetterHR
        Dim obj As ClsAppointmentLetterHR = Nothing
        Dim Arr As List(Of ClsAppointmentLetterHR) = Nothing
        Dim qry As String = "select * from TSPL_HR_APPOINTMENT_LETTER where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'  AND APPLICANT_CODE ='" + strCode + "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAppointmentLetterHR()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Appointment_Date = clsCommon.myCstr(dt.Rows(0)("Appointment_Date"))
            'obj.DateOfJoining = clsCommon.myCstr(dt.Rows(0)("Date_Of_Joining"))
        End If
        Return obj
    End Function
    '' --------------------------------------------------------------------------------------------------------------------------------------
End Class
