Imports System.Data.SqlClient
Imports common

Public Class ClsInterviewFeedback
#Region "Variables"
    Public Feedback_Code As String
    Public Applicant_Code As String
    Public Round_Code As String
    Public Action As String
    Public Final_Action As String
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    'Public Remarks As String
    'Public Comments As String
    'Public Total_Score As Integer
    'Public Clearing_Score As Integer
    'Public Score As Integer
    'Public Percentage As Integer

    Public ObjList As List(Of ClsInterviewFeedbackDetail) = Nothing
    Dim objDetail As New ClsInterviewFeedbackDetail()
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE AS [Code],Applicant_Description AS [Applicant Description],CONVERT (VARCHAR,Applicant_Date ,103) AS [Applicant Date],Requisition_Code AS [Requisition Code],CASE WHEN Gender='F' THEN 'Female' WHEN Gender ='M' THEN 'Male' END AS [Gender],First_Name + ' ' + Middle_Name + ' ' + Last_Name AS [Applicant Name],CONVERT(VARCHAR,Applicant_Date_Of_Birth,103) As [Applicant Date Of Birth],CONVERT (VARCHAR,Date_Of_Interview,103) AS [Date Of Interview],CASE WHEN Maritial_Status='M' THEN 'Married' WHEN Maritial_Status ='U' THEN 'UnMarried' END As [Maritial Status],Pan_No AS [Pan No],Add1 +' '+Add2 +' '+Add3 +' '+Add4 AS [Address],TELEPHONE_NO As [Phone No],Email,Source_Type_Code AS [Source Type Code],Source_Type_Detail_Code AS [Source Type Detail Code],Is_Fresher AS [Is Fresher],Short AS [Is Shortlisted],Rejected As [Is Rejected],COUNTRY_CODE AS [Country Code],State_Code As [State Code],City_code As [City Code],Pin_Code As [Pin Code],Blood_Group As [Blooad Group],Bank_Code As [Bank Code],Branch_Code As [Branch Code],Location_Code AS [Location Code],Preferred_Location_Code AS [Preferred Location Code],Current_Gross_Salary As [Current Gross Salary],Total_CTC AS [Total CTC],Performance_By As [Performance By] ,Is_Handicaped As [Is Handicaped] FROM TSPL_HR_INTERVIEW_FEEDBACK LEFT OUTER JOIN TSPL_HR_APPLICANT_ENTRY ON TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE =TSPL_HR_INTERVIEW_FEEDBACK.APPLICANT_CODE "
        str = clsCommon.ShowSelectForm("HRINTRFD", qry, "Code", "  TSPL_HR_INTERVIEW_FEEDBACK.Posted =1 AND TSPL_HR_INTERVIEW_FEEDBACK.Final_Action='H' ", curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal arr As List(Of ClsInterviewFeedback)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If ClsInterviewFeedback.SaveData(arr, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsInterviewFeedback), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry1 As String = ""
            Dim DBEmpty As String = ""
            Dim FCode As String = ""
            For Each obj As ClsInterviewFeedback In arr
                If clsCommon.myLen(obj.Feedback_Code) <= 0 Then
                    qry1 = "select max(Feedback_Code) from TSPL_HR_INTERVIEW_FEEDBACK where Feedback_Code='" + obj.Feedback_Code + "'"
                    Dim value As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))

                    If clsCommon.myLen(value) <= 0 Then
                        DBEmpty = "select max(Feedback_Code) from TSPL_HR_INTERVIEW_FEEDBACK"
                        FCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(DBEmpty, trans))
                        If clsCommon.myLen(FCode) > 0 Then
                            value = clsCommon.myCstr(clsCommon.incval(FCode))
                        Else
                            value = "F-001"
                        End If

                    Else
                        value = clsCommon.myCstr(clsCommon.incval(FCode))
                    End If
                    obj.Feedback_Code = value
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Feedback_Code", obj.Feedback_Code)
                clsCommon.AddColumnsForChange(coll, "Applicant_Code", obj.Applicant_Code)
                clsCommon.AddColumnsForChange(coll, "Final_Action", obj.Final_Action)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Posted", clsCommon.myCdbl(obj.Posted))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_INTERVIEW_FEEDBACK WHERE Applicant_Code='" + obj.Applicant_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_INTERVIEW_FEEDBACK where Applicant_Code= '" & obj.Applicant_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_INTERVIEW_FEEDBACK", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")
                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_INTERVIEW_FEEDBACK", OMInsertOrUpdate.Update, "Applicant_Code='" + obj.Applicant_Code + "'", trans)
                End If
                ClsInterviewFeedbackDetail.SaveData(obj.Applicant_Code, obj.Feedback_Code, obj.Round_Code, obj.ObjList, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return isSaved
    End Function
    '' --------------------------------------------- NAV QUERY(=) -------------------------------------------------------------------------
    Public Shared Function GetDataForNav(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsInterviewFeedback
        Return GetDataForNav(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetDataForNav(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsInterviewFeedback
        Dim obj As ClsInterviewFeedback = Nothing

        Dim qry As String = "Select * From TSPL_HR_INTERVIEW_FEEDBACK WHERE APPLICANT_CODE='" + strCode + "' "
        
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsInterviewFeedback()

            obj.Feedback_Code = clsCommon.myCstr(dt.Rows(0)("Feedback_Code"))
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("Applicant_Code"))
            obj.Round_Code = clsCommon.myCstr(dt.Rows(0)("Round_Code"))
            obj.Final_Action = clsCommon.myCstr(dt.Rows(0)("Final_Action"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            obj.ObjList = ClsInterviewFeedbackDetail.GetData(obj.Applicant_Code, trans)
        End If
        Return obj
    End Function
    '' --------------------------------------------- NAV QUERY(=) -------------------------------------------------------------------------
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsInterviewFeedback
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsInterviewFeedback
        Dim obj As ClsInterviewFeedback = Nothing

        Dim qry As String = "Select * From TSPL_HR_INTERVIEW_FEEDBACK where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_INTERVIEW_FEEDBACK.Applicant_Code = (select MIN(Applicant_Code) from TSPL_HR_INTERVIEW_FEEDBACK)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_INTERVIEW_FEEDBACK.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_INTERVIEW_FEEDBACK)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_INTERVIEW_FEEDBACK.Applicant_Code = (select Min(Applicant_Code) from TSPL_HR_INTERVIEW_FEEDBACK where  Applicant_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_INTERVIEW_FEEDBACK.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_INTERVIEW_FEEDBACK where Applicant_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_INTERVIEW_FEEDBACK.Applicant_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsInterviewFeedback()

            obj.Feedback_Code = clsCommon.myCstr(dt.Rows(0)("Feedback_Code"))
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("Applicant_Code"))
            obj.Round_Code = clsCommon.myCstr(dt.Rows(0)("Round_Code"))
            obj.Final_Action = clsCommon.myCstr(dt.Rows(0)("Final_Action"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            obj.ObjList = ClsInterviewFeedbackDetail.GetData(obj.Applicant_Code, trans)
        End If
        Return obj
    End Function
       Public Shared Function DeleteData(ByVal strFCode As String, ByVal strCode As String, ByVal strRCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "Delete From TSPL_HR_INTERVIEW_FEEDBACK_DETAIL Where Feedback_Code='" + strFCode + "' AND Applicant_Code ='" + strCode + "' AND Round_Code ='" + strRCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete From TSPL_HR_INTERVIEW_FEEDBACK Where Feedback_Code='" + strFCode + "' AND Applicant_Code ='" + strCode + "' AND Round_Code ='" + strRCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
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
                Throw New Exception("code not found to Post")
            End If
            'Dim obj As ClsInterviewFeedback = ClsInterviewFeedback.GetData(strDocNo, NavigatorType.Current, trans)

            'If (obj Is Nothing OrElse clsCommon.myLen(obj.Feedback_Code) <= 0) Then
            '    Throw New Exception("No Data found to Post")
            'End If

            Dim qry = "Update TSPL_HR_INTERVIEW_FEEDBACK set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Feedback_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ' ----------------- Get_Action ------------------------
    Public Shared Function GetAction() As DataTable
        Dim DT_Action As DataTable = New DataTable
        DT_Action.Columns.Add("Code", GetType(String))
        DT_Action.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Action.NewRow()
        DR("Name") = "Next Round"
        DR("Code") = "NR"
        DT_Action.Rows.Add(DR)

        DR = DT_Action.NewRow()
        DR("Name") = "Reschedule"
        DR("Code") = "R"
        DT_Action.Rows.Add(DR)

        DR = DT_Action.NewRow()
        DR("Name") = "Final Opinion"
        DR("Code") = "FO"
        DT_Action.Rows.Add(DR)

        Return DT_Action
    End Function
    ' ----------------- Get_Final_Action ------------------------
    Public Shared Function GetFA() As DataTable
        Dim DT_FA As DataTable = New DataTable
        DT_FA.Columns.Add("Code", GetType(String))
        DT_FA.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_FA.NewRow()
        DR("Name") = "Hire"
        DR("Code") = "H"
        DT_FA.Rows.Add(DR)

        DR = DT_FA.NewRow()
        DR("Name") = "On Hold"
        DR("Code") = "OH"
        DT_FA.Rows.Add(DR)

        DR = DT_FA.NewRow()
        DR("Name") = "Reject"
        DR("Code") = "R"
        DT_FA.Rows.Add(DR)
        DT_FA.AcceptChanges()

        Return DT_FA
    End Function
End Class

Public Class ClsInterviewFeedbackDetail

#Region "Variables"
    Public Feedback_Code As String = Nothing
    Public Parameter_Code As String = Nothing
    Public Applicant_Code As String = Nothing
    Public Round_Code As String = Nothing
    Public Round_Action As String = Nothing
    Public Rating As Double = 0
    Public Remarks As String
    Public Comments As String
    Public Total_Score As Integer
    Public Clearing_Score As Integer
    Public Score As Integer
    Public Percentage As Integer
    Public IsNR As String

#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_INTERVIEW_FEEDBACK_DETAIL where Applicant_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal strFCode As String, ByVal RoundCode As String, ByVal ObjList As List(Of ClsInterviewFeedbackDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_INTERVIEW_FEEDBACK_DETAIL where Applicant_Code ='" & strCode & "' AND  Feedback_Code = '" & strFCode & "' AND Round_Code  = '" + RoundCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsInterviewFeedbackDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Feedback_Code", strFCode)
                clsCommon.AddColumnsForChange(coll, "Applicant_Code", obj.Applicant_Code, True)
                clsCommon.AddColumnsForChange(coll, "Parameter_Code", obj.Parameter_Code, True)
                clsCommon.AddColumnsForChange(coll, "Round_Code", obj.Round_Code, True)
                clsCommon.AddColumnsForChange(coll, "Rating", obj.Rating)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
                clsCommon.AddColumnsForChange(coll, "Total_Score", obj.Total_Score)
                clsCommon.AddColumnsForChange(coll, "Clearing_Score", obj.Clearing_Score)
                clsCommon.AddColumnsForChange(coll, "Round_Action", obj.Round_Action)
                clsCommon.AddColumnsForChange(coll, "Score", obj.Score)
                clsCommon.AddColumnsForChange(coll, "Percentage", obj.Percentage)
                '' Next round 19-Aug
                clsCommon.AddColumnsForChange(coll, "IsNR", obj.IsNR)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_INTERVIEW_FEEDBACK_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsInterviewFeedbackDetail)
        Dim obj As ClsInterviewFeedbackDetail = Nothing
        Dim ObjList As New List(Of ClsInterviewFeedbackDetail)
        Dim qry As String = " select *  from TSPL_HR_INTERVIEW_FEEDBACK_DETAIL WHERE Applicant_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsInterviewFeedbackDetail()
                obj.Parameter_Code = clsCommon.myCstr(dr("Parameter_Code"))
                obj.Feedback_Code = clsCommon.myCstr(dr("Feedback_Code"))
                obj.Applicant_Code = clsCommon.myCstr(dr("Applicant_Code"))
                obj.Rating = clsCommon.myCdbl(dr("Rating"))
                obj.Remarks = clsCommon.myCstr(dr("Remarks"))
                obj.Comments = clsCommon.myCstr(dr("Comments"))
                obj.Total_Score = clsCommon.myCdbl(dr("Total_Score"))
                obj.Score = clsCommon.myCdbl(dr("Score"))
                obj.Clearing_Score = clsCommon.myCdbl(dr("Clearing_Score"))
                obj.Percentage = clsCommon.myCdbl(dr("Percentage"))
                '' Next Round 19-Aug
                obj.IsNR = clsCommon.myCstr(dr("IsNR"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function

End Class

