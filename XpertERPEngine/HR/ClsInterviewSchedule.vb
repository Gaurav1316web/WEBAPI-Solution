Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsInterviewSchedule
#Region "Variables"
    'Public Schedule_Code As String
    Public Applicant_Code As String
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending

    Public ObjList As List(Of ClsInterviewScheduleDetail) = Nothing
    Dim objDetail As New ClsInterviewScheduleDetail()
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE AS [Code],Applicant_Description AS [Applicant Description],CONVERT (VARCHAR,Applicant_Date ,103) AS [Applicant Date],Requisition_Code AS [Requisition Code],CASE WHEN Gender='F' THEN 'Female' WHEN Gender ='M' THEN 'Male' END AS [Gender],First_Name + ' ' + Middle_Name + ' ' + Last_Name AS [Applicant Name],CONVERT(VARCHAR,Applicant_Date_Of_Birth,103) As [Applicant Date Of Birth],CONVERT (VARCHAR,Date_Of_Interview,103) AS [Date Of Interview],CASE WHEN Maritial_Status='M' THEN 'Married' WHEN Maritial_Status ='U' THEN 'UnMarried' END As [Maritial Status],Pan_No AS [Pan No],Add1 +' '+Add2 +' '+Add3 +' '+Add4 AS [Address],TELEPHONE_NO As [Phone No],Email,Source_Type_Code AS [Source Type Code],Source_Type_Detail_Code AS [Source Type Detail Code],Is_Fresher AS [Is Fresher],Short AS [Is Shortlisted],Rejected As [Is Rejected],COUNTRY_CODE AS [Country Code],State_Code As [State Code],City_code As [City Code],Pin_Code As [Pin Code],Blood_Group As [Blooad Group],Bank_Code As [Bank Code],Branch_Code As [Branch Code],Location_Code AS [Location Code],Preferred_Location_Code AS [Preferred Location Code],Current_Gross_Salary As [Current Gross Salary],Total_CTC AS [Total CTC],Performance_By As [Performance By] ,Is_Handicaped As [Is Handicaped] FROM TSPL_HR_INTERVIEW_SCHEDULE LEFT OUTER JOIN TSPL_HR_APPLICANT_ENTRY ON TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE =TSPL_HR_INTERVIEW_SCHEDULE.APPLICANT_CODE  "
        str = clsCommon.ShowSelectForm("HRINTR", qry, "Code", "TSPL_HR_INTERVIEW_SCHEDULE.Posted =1", curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal arr As List(Of ClsInterviewSchedule)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If ClsInterviewSchedule.SaveData(arr, trans) Then
                trans.Commit()

            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsInterviewSchedule), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsInterviewSchedule In arr
                Dim coll As New Hashtable()
                ' clsCommon.AddColumnsForChange(coll, "Schedule_Code", obj.Schedule_Code)
                clsCommon.AddColumnsForChange(coll, "Applicant_Code", obj.Applicant_Code)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Posted", clsCommon.myCdbl(ERPTransactionStatus.Pending))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_INTERVIEW_SCHEDULE WHERE Applicant_Code='" + obj.Applicant_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_INTERVIEW_SCHEDULE where Applicant_Code= '" & obj.Applicant_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_INTERVIEW_SCHEDULE", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")
                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_INTERVIEW_SCHEDULE", OMInsertOrUpdate.Update, "Applicant_Code='" + obj.Applicant_Code + "'", trans)
                End If
                ClsInterviewScheduleDetail.SaveData(obj.Applicant_Code, obj.ObjList, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsInterviewSchedule
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsInterviewSchedule
        Dim obj As ClsInterviewSchedule = Nothing

        Dim qry As String = "Select * From TSPL_HR_INTERVIEW_SCHEDULE where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = (select MIN(Applicant_Code) from TSPL_HR_INTERVIEW_SCHEDULE)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_INTERVIEW_SCHEDULE)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = (select Min(Applicant_Code) from TSPL_HR_INTERVIEW_SCHEDULE where  Applicant_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_INTERVIEW_SCHEDULE where Applicant_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsInterviewSchedule()
            'obj.Schedule_Code = clsCommon.myCstr(dt.Rows(0)("Schedule_Code"))
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("Applicant_Code"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.ObjList = ClsInterviewScheduleDetail.GetData(obj.Applicant_Code, trans)
        End If
        Return obj
    End Function
    '' --------------------------------------------- NAV QUERY(=) -------------------------------------------------------------------------
    Public Shared Function GetDataForNav(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsInterviewSchedule
        Return GetDataForNav(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetDataForNav(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsInterviewSchedule
        Dim obj As ClsInterviewSchedule = Nothing

        Dim qry As String = "Select * From TSPL_HR_INTERVIEW_SCHEDULE WHERE APPLICANT_CODE='" + strCode + "' "
       
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsInterviewSchedule()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("Applicant_Code"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.ObjList = ClsInterviewScheduleDetail.GetData(obj.Applicant_Code, trans)
        End If
        Return obj
    End Function
    '' --------------------------------------------------------- NAV QUERY (=) ---------------------------------------------
    '' ---------------------------------------- GET FINDER FOR POSTED DATA ------------------------------------------------- ''
    Public Shared Function GetPostedData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsInterviewSchedule
        Return GetPostedData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetPostedData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsInterviewSchedule
        Dim obj As ClsInterviewSchedule = Nothing

        Dim qry As String = "Select * From TSPL_HR_INTERVIEW_SCHEDULE where 2=2 AND Posted=1"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = (select MIN(Applicant_Code) from TSPL_HR_INTERVIEW_SCHEDULE)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_INTERVIEW_SCHEDULE)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = (select Min(Applicant_Code) from TSPL_HR_INTERVIEW_SCHEDULE where  Applicant_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_INTERVIEW_SCHEDULE where Applicant_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_INTERVIEW_SCHEDULE.Applicant_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsInterviewSchedule()
            'obj.Schedule_Code = clsCommon.myCstr(dt.Rows(0)("Schedule_Code"))
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("Applicant_Code"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.ObjList = ClsInterviewScheduleDetail.GetData(obj.Applicant_Code, trans)
        End If
        Return obj
    End Function
    '' ---------------------------------------------------------------------------------------------------------------------
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            ''
            Dim Applicant_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Applicant_Code,'') As Applicant_Code from TSPL_HR_INTERVIEW_SCHEDULE where Applicant_Code='" + strCode + "'", trans))
            If clsCommon.myLen(Applicant_Code) > 0 Then
                Dim qry As String
                qry = "Delete From TSPL_HR_INTERVIEW_SCHEDULE_DETAIL Where Applicant_Code ='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "Delete From TSPL_HR_INTERVIEW_SCHEDULE Where Applicant_Code ='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                If isSaved Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Else
                Throw New Exception("You cannot delete this entry before entering applicant code")
            End If
           
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Applicant code not found to Post")
            End If
            Dim obj As ClsInterviewSchedule = ClsInterviewSchedule.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Applicant_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_HR_INTERVIEW_SCHEDULE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Applicant_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class ClsInterviewScheduleDetail

#Region "Variables"
    Public Applicant_Code As String = Nothing
    Public S_No As String = Nothing
    Public Schedule_Description As String = Nothing
    Public Interviewer_Code As String = Nothing
    Public DEPARTMENT_CODE As String = Nothing
    Public Email As String = Nothing
    Public Location_Code As String = Nothing
    Public Sub_location_Code As String = Nothing
    Public Round_Code As String = Nothing
    Public Start_Time As String = Nothing
    Public End_Time As String = Nothing
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_INTERVIEW_SCHEDULE_DETAIL where Applicant_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of ClsInterviewScheduleDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_INTERVIEW_SCHEDULE_DETAIL where Applicant_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsInterviewScheduleDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Applicant_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "S_No", obj.S_No)
                clsCommon.AddColumnsForChange(coll, "Schedule_Description", obj.Schedule_Description)
                clsCommon.AddColumnsForChange(coll, "Interviewer_Code", obj.Interviewer_Code, True)
                'clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE, True)
                'clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Sub_location_Code", obj.Sub_location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Round_Code", obj.Round_Code, True)
                clsCommon.AddColumnsForChange(coll, "Start_Time", clsCommon.GetPrintDate(obj.Start_Time, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "End_Time", clsCommon.GetPrintDate(obj.End_Time, "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_INTERVIEW_SCHEDULE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsInterviewScheduleDetail)
        Dim obj As ClsInterviewScheduleDetail = Nothing
        Dim ObjList As New List(Of ClsInterviewScheduleDetail)
        Dim qry As String = " select *  from TSPL_HR_INTERVIEW_SCHEDULE_DETAIL WHERE Applicant_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsInterviewScheduleDetail()
                obj.S_No = clsCommon.myCstr(dr("S_No"))
                obj.Applicant_Code = clsCommon.myCstr(dr("Applicant_Code"))
                obj.Schedule_Description = clsCommon.myCstr(dr("Schedule_Description"))
                obj.Interviewer_Code = clsCommon.myCstr(dr("Interviewer_Code"))
                'obj.Email = clsCommon.myCstr(dr("Email"))
                'obj.DEPARTMENT_CODE = clsCommon.myCstr(dr("DEPARTMENT_CODE"))
                obj.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                obj.Sub_location_Code = clsCommon.myCstr(dr("Sub_location_Code"))
                'obj.DEPARTMENT_CODE = clsCommon.myCstr(dr("DEPARTMENT_CODE"))
                obj.Start_Time = clsCommon.myCstr(dr("Start_Time"))
                obj.End_Time = clsCommon.myCstr(dr("End_Time"))
                obj.Round_Code = clsCommon.myCstr(dr("Round_Code"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function

End Class
