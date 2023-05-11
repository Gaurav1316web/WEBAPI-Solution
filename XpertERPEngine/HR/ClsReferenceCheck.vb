Imports System.Data.SqlClient
Imports common

Public Class ClsReferenceCheck

#Region "Variables"
    Public Applicant_Code As String = Nothing
    ' Public Refrence_Code As String = Nothing
    Public Remarks As String = Nothing
    Public Initiate_By As String = Nothing
    Public Ref_Date As String = Nothing
    'Public Category_Item As String = Nothing
    Public Past_Mode_Of_Check As String = Nothing
    Public Past_Category_Feedback As String = Nothing
    Public Past_Feedback_Remarks As String = Nothing
    Public Cand_Mode_Of_Check As String = Nothing
    Public Cand_Category_Feedback As String = Nothing
    Public Cand_Feedback_Remarks As String = Nothing
    Public Is_Override As Double = 0
    Public Is_PastDetail As Double = 0
    Public Is_CandidateDetail As Double = 0
    Public Final_Feedback As String = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE AS [Code],Applicant_Description AS [Applicant Description],CONVERT (VARCHAR,Applicant_Date ,103) AS [Applicant Date],Requisition_Code AS [Requisition Code],CASE WHEN Gender='F' THEN 'Female' WHEN Gender ='M' THEN 'Male' END AS [Gender],First_Name + ' ' + Middle_Name + ' ' + Last_Name AS [Applicant Name],CONVERT(VARCHAR,Applicant_Date_Of_Birth,103) As [Applicant Date Of Birth],CONVERT (VARCHAR,Date_Of_Interview,103) AS [Date Of Interview],CASE WHEN Maritial_Status='M' THEN 'Married' WHEN Maritial_Status ='U' THEN 'UnMarried' END As [Maritial Status],Pan_No AS [Pan No],Add1 +' '+Add2 +' '+Add3 +' '+Add4 AS [Address],TELEPHONE_NO As [Phone No],Email,Source_Type_Code AS [Source Type Code],Source_Type_Detail_Code AS [Source Type Detail Code],Is_Fresher AS [Is Fresher],Short AS [Is Shortlisted],Rejected As [Is Rejected],COUNTRY_CODE AS [Country Code],State_Code As [State Code],City_code As [City Code],Pin_Code As [Pin Code],Blood_Group As [Blooad Group],Bank_Code As [Bank Code],Branch_Code As [Branch Code],Location_Code AS [Location Code],Preferred_Location_Code AS [Preferred Location Code],Current_Gross_Salary As [Current Gross Salary],Total_CTC AS [Total CTC],Performance_By As [Performance By] ,Is_Handicaped As [Is Handicaped] FROM TSPL_HR_REFERENCE_CHECK LEFT OUTER JOIN TSPL_HR_APPLICANT_ENTRY ON TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE =TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE  "
        str = clsCommon.ShowSelectForm("HRREF", qry, "Code", "TSPL_HR_REFERENCE_CHECK.Posted =1 AND Final_Feedback='P'", curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal obj As ClsReferenceCheck, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Applicant_Code", obj.Applicant_Code, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Initiate_By", obj.Initiate_By, True)
            clsCommon.AddColumnsForChange(coll, "Ref_Date", clsCommon.GetPrintDate(obj.Ref_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Past_Category_Feedback", obj.Past_Category_Feedback)
            clsCommon.AddColumnsForChange(coll, "Past_Mode_Of_Check", obj.Past_Mode_Of_Check)
            clsCommon.AddColumnsForChange(coll, "Past_Feedback_Remarks", obj.Past_Feedback_Remarks)
            clsCommon.AddColumnsForChange(coll, "Cand_Category_Feedback", obj.Cand_Category_Feedback)
            clsCommon.AddColumnsForChange(coll, "Cand_Feedback_Remarks", obj.Cand_Feedback_Remarks)
            clsCommon.AddColumnsForChange(coll, "Cand_Mode_Of_Check", obj.Cand_Mode_Of_Check)
            clsCommon.AddColumnsForChange(coll, "Is_Override", obj.Is_Override)
            clsCommon.AddColumnsForChange(coll, "Is_PastDetail", obj.Is_PastDetail)
            clsCommon.AddColumnsForChange(coll, "Is_CandidateDetail", obj.Is_CandidateDetail)
            clsCommon.AddColumnsForChange(coll, "Final_Feedback", obj.Final_Feedback)
            clsCommon.AddColumnsForChange(coll, "Posted", clsCommon.myCdbl(obj.Posted))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))

            If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_REFERENCE_CHECK WHERE APPLICANT_CODE='" + obj.Applicant_Code + "'") <= 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))

                Dim qry1 As String = "SELECT Count(*) FROM TSPL_HR_REFERENCE_CHECK where APPLICANT_CODE= '" & obj.Applicant_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check = 0 Then
                    IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_REFERENCE_CHECK", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_REFERENCE_CHECK", OMInsertOrUpdate.Update, "APPLICANT_CODE='" + obj.Applicant_Code + "'")
            End If
            'If isNewEntry Then
            '    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            '    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            '    'clsCommon.AddColumnsForChange(coll, "Refrence_Code", obj.Refrence_Code)
            '    IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_REFERENCE_CHECK", OMInsertOrUpdate.Insert, "")
            'Else
            '    IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_REFERENCE_CHECK", OMInsertOrUpdate.Update, "TSPL_HR_REFERENCE_CHECK.Applicant_Code='" + obj.Applicant_Code + "'")
            'End If
            'trans.Commit()
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
  
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsReferenceCheck
        Dim obj As ClsReferenceCheck = Nothing
        Dim Arr As List(Of ClsReferenceCheck) = Nothing
        Dim qry As String = "select * from TSPL_HR_REFERENCE_CHECK where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_REFERENCE_CHECK WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_REFERENCE_CHECK WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select TOP 1 APPLICANT_CODE from TSPL_HR_REFERENCE_CHECK WHERE 1=1 " + whrclas + " and APPLICANT_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select Min(APPLICANT_CODE) from TSPL_HR_REFERENCE_CHECK where APPLICANT_CODE > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_REFERENCE_CHECK where APPLICANT_CODE < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsReferenceCheck()
            obj.APPLICANT_CODE = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Initiate_By = clsCommon.myCstr(dt.Rows(0)("Initiate_By"))
            obj.Ref_Date = clsCommon.myCstr(dt.Rows(0)("Ref_Date"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Past_Mode_Of_Check = clsCommon.myCstr(dt.Rows(0)("Past_Mode_Of_Check"))
            obj.Past_Category_Feedback = clsCommon.myCstr(dt.Rows(0)("Past_Category_Feedback"))
            obj.Past_Feedback_Remarks = clsCommon.myCstr(dt.Rows(0)("Past_Feedback_Remarks"))
            obj.Cand_Category_Feedback = clsCommon.myCstr(dt.Rows(0)("Cand_Category_Feedback"))
            obj.Cand_Feedback_Remarks = clsCommon.myCstr(dt.Rows(0)("Cand_Feedback_Remarks"))
            obj.Cand_Mode_Of_Check = clsCommon.myCstr(dt.Rows(0)("Cand_Mode_Of_Check"))
            obj.Is_Override = clsCommon.myCstr(dt.Rows(0)("Is_Override"))
            obj.Is_PastDetail = clsCommon.myCstr(dt.Rows(0)("Is_PastDetail"))
            obj.Is_CandidateDetail = clsCommon.myCstr(dt.Rows(0)("Is_CandidateDetail"))
            obj.Final_Feedback = clsCommon.myCstr(dt.Rows(0)("Final_Feedback"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function
    '' ------------------------------------------- Posted Data for Salary Fitment --------------------------------------------------------
    Public Shared Function GetPostedData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsReferenceCheck
        Dim obj As ClsReferenceCheck = Nothing
        Dim Arr As List(Of ClsReferenceCheck) = Nothing
        Dim qry As String = "select * from TSPL_HR_REFERENCE_CHECK where 2=2 "
        Dim whrclas As String = " AND Posted =1 AND Final_Feedback='P' "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_REFERENCE_CHECK WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_REFERENCE_CHECK WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select TOP 1 APPLICANT_CODE from TSPL_HR_REFERENCE_CHECK WHERE 1=1 " + whrclas + " and APPLICANT_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select Min(APPLICANT_CODE) from TSPL_HR_REFERENCE_CHECK where APPLICANT_CODE > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_REFERENCE_CHECK.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_REFERENCE_CHECK where APPLICANT_CODE < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsReferenceCheck()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Initiate_By = clsCommon.myCstr(dt.Rows(0)("Initiate_By"))
            obj.Ref_Date = clsCommon.myCstr(dt.Rows(0)("Ref_Date"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Past_Mode_Of_Check = clsCommon.myCstr(dt.Rows(0)("Past_Mode_Of_Check"))
            obj.Past_Category_Feedback = clsCommon.myCstr(dt.Rows(0)("Past_Category_Feedback"))
            obj.Past_Feedback_Remarks = clsCommon.myCstr(dt.Rows(0)("Past_Feedback_Remarks"))
            obj.Cand_Category_Feedback = clsCommon.myCstr(dt.Rows(0)("Cand_Category_Feedback"))
            obj.Cand_Feedback_Remarks = clsCommon.myCstr(dt.Rows(0)("Cand_Feedback_Remarks"))
            obj.Cand_Mode_Of_Check = clsCommon.myCstr(dt.Rows(0)("Cand_Mode_Of_Check"))
            obj.Is_Override = clsCommon.myCstr(dt.Rows(0)("Is_Override"))
            obj.Is_PastDetail = clsCommon.myCstr(dt.Rows(0)("Is_PastDetail"))
            obj.Is_CandidateDetail = clsCommon.myCstr(dt.Rows(0)("Is_CandidateDetail"))
            obj.Final_Feedback = clsCommon.myCstr(dt.Rows(0)("Final_Feedback"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function
    '' -----------------------------------------------------------------------------------------------------------------------------------
    '' --------------------------------------- Nav. Query(=) -------------------------------------------------------------
    Public Shared Function GetDataForNav(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsReferenceCheck
        Dim obj As ClsReferenceCheck = Nothing
        Dim Arr As List(Of ClsReferenceCheck) = Nothing
        Dim qry As String = "select * from TSPL_HR_REFERENCE_CHECK where APPLICANT_CODE= '" + strCode + "'"
        
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsReferenceCheck()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Initiate_By = clsCommon.myCstr(dt.Rows(0)("Initiate_By"))
            obj.Ref_Date = clsCommon.myCstr(dt.Rows(0)("Ref_Date"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Past_Mode_Of_Check = clsCommon.myCstr(dt.Rows(0)("Past_Mode_Of_Check"))
            obj.Past_Category_Feedback = clsCommon.myCstr(dt.Rows(0)("Past_Category_Feedback"))
            obj.Past_Feedback_Remarks = clsCommon.myCstr(dt.Rows(0)("Past_Feedback_Remarks"))
            obj.Cand_Category_Feedback = clsCommon.myCstr(dt.Rows(0)("Cand_Category_Feedback"))
            obj.Cand_Feedback_Remarks = clsCommon.myCstr(dt.Rows(0)("Cand_Feedback_Remarks"))
            obj.Cand_Mode_Of_Check = clsCommon.myCstr(dt.Rows(0)("Cand_Mode_Of_Check"))
            obj.Is_Override = clsCommon.myCstr(dt.Rows(0)("Is_Override"))
            obj.Is_PastDetail = clsCommon.myCstr(dt.Rows(0)("Is_PastDetail"))
            obj.Is_CandidateDetail = clsCommon.myCstr(dt.Rows(0)("Is_CandidateDetail"))
            obj.Final_Feedback = clsCommon.myCstr(dt.Rows(0)("Final_Feedback"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function
    '' -------------------------------------------------------------------------------------------------------------------
    ' ----------------- Get_Mode_Of_Check ------------------------
    Public Shared Function GetMOC() As DataTable
        Dim DT_MOC As DataTable = New DataTable
        DT_MOC.Columns.Add("Code", GetType(String))
        DT_MOC.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_MOC.NewRow()
        DR("Name") = "Email"
        DR("Code") = "E"
        DT_MOC.Rows.Add(DR)

        DR = DT_MOC.NewRow()
        DR("Name") = "Telephonic Conversation"
        DR("Code") = "TC"
        DT_MOC.Rows.Add(DR)
        DT_MOC.AcceptChanges()

        Return DT_MOC
    End Function
    ' ----------------- Get_Feedback ------------------------
    Public Shared Function GetFeedback() As DataTable
        Dim DT_FB As DataTable = New DataTable
        DT_FB.Columns.Add("Code", GetType(String))
        DT_FB.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_FB.NewRow()
        DR("Name") = "Positive"
        DR("Code") = "P"
        DT_FB.Rows.Add(DR)

        DR = DT_FB.NewRow()
        DR("Name") = "Negative"
        DR("Code") = "N"
        DT_FB.Rows.Add(DR)
        DT_FB.AcceptChanges()

        Return DT_FB
    End Function
    ' ----------------- Get_Final_Feedback ------------------------
    Public Shared Function GetFinalFeedback() As DataTable
        Dim DT_FF As DataTable = New DataTable
        DT_FF.Columns.Add("Code", GetType(String))
        DT_FF.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_FF.NewRow()
        DR("Name") = "Positive"
        DR("Code") = "P"
        DT_FF.Rows.Add(DR)

        DR = DT_FF.NewRow()
        DR("Name") = "Negative"
        DR("Code") = "N"
        DT_FF.Rows.Add(DR)
        DT_FF.AcceptChanges()

        Return DT_FF
    End Function
    'Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        PostData(FormId, strDocNo, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Applicant code not found to Post")
            End If
            Dim obj As ClsReferenceCheck = ClsReferenceCheck.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Applicant_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_HR_REFERENCE_CHECK set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "',Posted_By ='" + objCommonVar.CurrentUserCode + "' " & _
            " where Applicant_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

