Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class clsJoiningCheckListHead

#Region "variable"
    Public ApplicantCode As String = Nothing
    Public Joiningdate As DateTime = Nothing
    Public JoiningCode As String = Nothing
    Public JoiningDescription As String = Nothing
    Public Remarks As String = Nothing
    Public Received As Integer
    Public Attachment As String = Nothing

    'Public ObjheadList As List(Of clsJoiningCheckListHead) = Nothing
    Public ObjList As List(Of clsJoiningDetail) = Nothing
    Public ObjListAttachment As List(Of clsJoiningAttachment) = Nothing
    Public arrjoin As New ArrayList
    Public Shared trans As SqlTransaction = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Rejected As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE AS [Code],Applicant_Description AS [Applicant Description],CONVERT (VARCHAR,TSPL_HR_APPLICANT_ENTRY.Applicant_Date ,103) AS [Applicant Date],Requisition_Code AS [Requisition Code],CASE WHEN Gender='F' THEN 'Female' WHEN Gender ='M' THEN 'Male' END AS [Gender],First_Name + ' ' + Middle_Name + ' ' + Last_Name AS [Applicant Name],CONVERT(VARCHAR,Applicant_Date_Of_Birth,103) As [Applicant Date Of Birth],CONVERT (VARCHAR,Date_Of_Interview,103) AS [Date Of Interview],CASE WHEN Maritial_Status='M' THEN 'Married' WHEN Maritial_Status ='U' THEN 'UnMarried' END As [Maritial Status],Pan_No AS [Pan No],Add1 +' '+Add2 +' '+Add3 +' '+Add4 AS [Address],TELEPHONE_NO As [Phone No],Email,Source_Type_Code AS [Source Type Code],Source_Type_Detail_Code AS [Source Type Detail Code],Is_Fresher AS [Is Fresher],Short AS [Is Shortlisted],TSPL_HR_APPLICANT_ENTRY.Rejected As [Is Rejected],COUNTRY_CODE AS [Country Code],State_Code As [State Code],City_code As [City Code],Pin_Code As [Pin Code],Blood_Group As [Blooad Group],Bank_Code As [Bank Code],Branch_Code As [Branch Code],Location_Code AS [Location Code],Preferred_Location_Code AS [Preferred Location Code],Current_Gross_Salary As [Current Gross Salary],Total_CTC AS [Total CTC],Performance_By As [Performance By] ,Is_Handicaped As [Is Handicaped] FROM TSPL_HR_JOINING_HEAD LEFT OUTER JOIN TSPL_HR_APPLICANT_ENTRY ON TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE = TSPL_HR_JOINING_HEAD.APPLICANT_CODE  "
        str = clsCommon.ShowSelectForm("HRJCL", qry, "Code", "TSPL_HR_JOINING_HEAD.Posted =1 ", curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal obj As clsJoiningCheckListHead, ByVal Arra As List(Of clsJoiningDetail), ByVal arrattachment As List(Of clsJoiningAttachment)) As Boolean
        Dim isSaved As Boolean = True
        trans = clsDBFuncationality.GetTransactin()
        Try
            'Dim qry As String = "delete from TSPL_HR_JOINING_DETAIL where APPLICANT_CODE='" + obj.Code + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "APPLICANT_CODE", obj.ApplicantCode)
            clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(obj.Joiningdate, "dd/MMM/yyyy hh:mm tt "))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_JOINING_HEAD WHERE Applicant_Code='" + obj.ApplicantCode + "'", trans) <= 0 Then

                'Dim qry As String = "SELECT Count(*) FROM TSPL_HR_JOINING_HEAD where Applicant_Code= '" & obj.ApplicantCode & "'"
                'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                'If check = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_JOINING_HEAD", OMInsertOrUpdate.Insert, "", trans)
                'Else
                '    common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                '    Exit Function
                'End If
            Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_JOINING_HEAD", OMInsertOrUpdate.Update, "Applicant_Code='" + obj.ApplicantCode + "'", trans)


            End If
            clsJoiningDetail.SaveData(obj.ApplicantCode, Arra, trans)
            trans.Commit()
            clsJoiningAttachment.SaveData(obj.ApplicantCode, arrattachment, trans)


        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsJoiningCheckListHead
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsJoiningCheckListHead
        Dim obj As clsJoiningCheckListHead = Nothing



        Dim qry As String = "select TSPL_HR_Joining_ChkList_JOB_TITLE.Chk_Code,TSPL_HR_Check_List.Chk_Description,TSPL_HR_JOINING_DETAIL.Remarks ,TSPL_HR_JOINING_DETAIL.Received,coalesce(  TSPL_HR_JOINING_DETAIL.JoiningMandatory,mandatory )as JoiningMandatory, TSPL_HR_JOINING_DETAIL.Attachment, TSPL_ATTACHMENTS .FileName,TSPL_HR_JOINING_HEAD.Date ,TSPL_HR_JOINING_HEAD .Posted,TSPL_HR_OFFER_LETTER.APPLICANT_CODE , TSPL_HR_JOINING_HEAD.Rejected   from TSPL_HR_OFFER_LETTER left outer join TSPL_HR_APPLICANT_ENTRY on  TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE = TSPL_HR_OFFER_LETTER.APPLICANT_CODE left outer join TSPL_HR_REQUISITION on TSPL_HR_REQUISITION.Requisition_Code=TSPL_HR_APPLICANT_ENTRY.Requisition_Code left outer join TSPL_HR_Joining_ChkList_JOB_TITLE on TSPL_HR_Joining_ChkList_JOB_TITLE.Job_Title_Code=TSPL_HR_REQUISITION.Job_Title_Code left outer join TSPL_HR_Check_List on TSPL_HR_Check_List.Chk_Code=TSPL_HR_Joining_ChkList_JOB_TITLE.Chk_Code left outer join TSPL_HR_JOINING_DETAIL on TSPL_HR_JOINING_DETAIL .Chk_Code =TSPL_HR_Check_List.Chk_Code AND TSPL_HR_JOINING_DETAIL.APPLICANT_CODE=TSPL_HR_OFFER_LETTER.APPLICANT_CODE left outer join TSPL_ATTACHMENTS on TSPL_HR_JOINING_DETAIL .Attachment =TSPL_ATTACHMENTS.CODE  left outer join TSPL_HR_JOINING_HEAD on TSPL_HR_JOINING_DETAIL.APPLICANT_CODE =TSPL_HR_JOINING_HEAD.APPLICANT_CODE where TSPL_HR_OFFER_LETTER.Posted=1"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_OFFER_LETTER.APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_OFFER_LETTER )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_OFFER_LETTER.APPLICANT_CODE = (select Max(Applicant_Code) from  TSPL_HR_OFFER_LETTER)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_OFFER_LETTER.APPLICANT_CODE = (select Min(Applicant_Code) from TSPL_HR_OFFER_LETTER where  Applicant_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_OFFER_LETTER.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_OFFER_LETTER where Applicant_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_OFFER_LETTER.Applicant_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJoiningCheckListHead()
            obj.ApplicantCode = clsCommon.myCstr(dt.Rows(0)("Applicant_Code"))
            'obj.ApplicantCode = strCode
            If (dt.Rows(0)("Date")) Is DBNull.Value Then
            Else
                obj.Joiningdate = clsCommon.myCDate(dt.Rows(0)("Date"))
            End If

            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Rejected = IIf(clsCommon.myCdbl(dt.Rows(0)("Rejected")) = 1, ERPTransactionStatus.Reject, ERPTransactionStatus.Pending)
        End If

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.ObjList = New List(Of clsJoiningDetail)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsJoiningDetail = New clsJoiningDetail()

                objTr.Chk_code = clsCommon.myCstr(dr("Chk_Code"))
                objTr.JoiningDescription = clsCommon.myCstr(dr("Chk_Description"))
                objTr.Applicant_Code = clsCommon.myCstr(dr("APPLICANT_CODE"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objTr.Received = clsCommon.myCBool(dr("Received"))
                objTr.JoiningMandatory = clsCommon.myCBool(dr("JoiningMandatory"))

                objTr.Attachment = clsCommon.myCstr(dr("Attachment"))
                objTr.FileName = clsCommon.myCstr(dr("FileName"))
                obj.arrjoin.Add(objTr.Chk_code)
                obj.arrjoin.Add(objTr.JoiningDescription)
                obj.arrjoin.Add(objTr.Received)
                obj.arrjoin.Add(objTr.JoiningMandatory)

                obj.arrjoin.Add(objTr.Remarks)
                obj.arrjoin.Add(objTr.Attachment)
                obj.arrjoin.Add(objTr.Applicant_Code)
                obj.ObjList.Add(objTr)


            Next
        End If


        Return obj



    End Function

    '' ------------------------------------------------------------- Posted Data---------------------------------------------------------
    Public Shared Function GetPostedData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsJoiningCheckListHead
        Return GetPostedData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetPostedData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsJoiningCheckListHead
        Dim obj As clsJoiningCheckListHead = Nothing

        ' Dim qry As String = "select TSPL_HR_Joining_ChkList_JOB_TITLE.Chk_Code,TSPL_HR_Check_List.Chk_Description,TSPL_HR_JOINING_DETAIL.Remarks ,TSPL_HR_JOINING_DETAIL.Received,coalesce(  TSPL_HR_JOINING_DETAIL.JoiningMandatory,mandatory )as JoiningMandatory, TSPL_HR_JOINING_DETAIL.Attachment, TSPL_ATTACHMENTS .FileName,TSPL_HR_JOINING_HEAD.Date ,TSPL_HR_JOINING_HEAD .Posted,TSPL_HR_OFFER_LETTER.APPLICANT_CODE , TSPL_HR_JOINING_HEAD.Rejected   from TSPL_HR_OFFER_LETTER left outer join TSPL_HR_APPLICANT_ENTRY on  TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE = TSPL_HR_OFFER_LETTER.APPLICANT_CODE left outer join TSPL_HR_REQUISITION on TSPL_HR_REQUISITION.Requisition_Code=TSPL_HR_APPLICANT_ENTRY.Requisition_Code left outer join TSPL_HR_Joining_ChkList_JOB_TITLE on TSPL_HR_Joining_ChkList_JOB_TITLE.Job_Title_Code=TSPL_HR_REQUISITION.Job_Title_Code left outer join TSPL_HR_Check_List on TSPL_HR_Check_List.Chk_Code=TSPL_HR_Joining_ChkList_JOB_TITLE.Chk_Code left outer join TSPL_HR_JOINING_DETAIL on TSPL_HR_JOINING_DETAIL .Chk_Code =TSPL_HR_Check_List.Chk_Code AND TSPL_HR_JOINING_DETAIL.APPLICANT_CODE=TSPL_HR_OFFER_LETTER.APPLICANT_CODE left outer join TSPL_ATTACHMENTS on TSPL_HR_JOINING_DETAIL .Attachment =TSPL_ATTACHMENTS.CODE  left outer join TSPL_HR_JOINING_HEAD on TSPL_HR_JOINING_DETAIL.APPLICANT_CODE =TSPL_HR_JOINING_HEAD.APPLICANT_CODE where TSPL_HR_OFFER_LETTER.Posted=1 " 'AND TSPL_HR_JOINING_HEAD.Posted =1"
        Dim qry As String = "Select * From TSPL_HR_JOINING_HEAD Where Posted =1 And Rejected= 0"
        Select Case NavType
            Case NavigatorType.First
                qry += " and APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_JOINING_HEAD )"
            Case NavigatorType.Last
                qry += " and APPLICANT_CODE = (select Max(Applicant_Code) from  TSPL_HR_JOINING_HEAD)"
            Case NavigatorType.Next
                qry += " and APPLICANT_CODE = (select Min(Applicant_Code) from TSPL_HR_JOINING_HEAD where  Applicant_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_JOINING_HEAD where Applicant_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Applicant_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJoiningCheckListHead()
            obj.ApplicantCode = clsCommon.myCstr(dt.Rows(0)("Applicant_Code"))
            'obj.ApplicantCode = strCode
            If (dt.Rows(0)("Date")) Is DBNull.Value Then
            Else
                obj.Joiningdate = clsCommon.myCDate(dt.Rows(0)("Date"))
            End If

            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Rejected = IIf(clsCommon.myCdbl(dt.Rows(0)("Rejected")) = 1, ERPTransactionStatus.Reject, ERPTransactionStatus.Pending)
        End If

        Return obj
    End Function
    '''''''''''''''''''''''''''''''''''''''''''''''


    '    Select Case NavType
    '        Case NavigatorType.First
    '            qry += " and TSPL_HR_JOINING_HEAD.APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_JOINING_HEAD )"
    '        Case NavigatorType.Last
    '            qry += " and TSPL_HR_JOINING_HEAD.APPLICANT_CODE = (select Max(Applicant_Code) from  TSPL_HR_JOINING_HEAD)"
    '        Case NavigatorType.Next
    '            qry += " and TSPL_HR_JOINING_HEAD.APPLICANT_CODE = (select Min(Applicant_Code) from TSPL_HR_JOINING_HEAD where  Applicant_Code >'" + strCode + "')"
    '        Case NavigatorType.Previous
    '            qry += " and TSPL_HR_JOINING_HEAD.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_JOINING_HEAD  where Applicant_Code <'" + strCode + "')"
    '        Case NavigatorType.Current
    '            qry += " and TSPL_HR_JOINING_HEAD.Applicant_Code = '" + strCode + "'"
    '    End Select
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

    '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '        obj = New clsJoiningCheckListHead()
    '        obj.ApplicantCode = clsCommon.myCstr(dt.Rows(0)("Applicant_Code"))

    '        obj.Joiningdate = clsCommon.myCDate(dt.Rows(0)("date"))

    '        obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
    '    End If

    '    qry = "select TSPL_HR_JOINING_DETAIL.APPLICANT_CODE ,TSPL_HR_JOINING_DETAIL.Chk_Code ,TSPL_HR_JOINING_DETAIL.Remarks ,TSPL_HR_JOINING_DETAIL.Received ,TSPL_HR_Check_List.Chk_Description,TSPL_HR_JOINING_DETAIL.Attachment,TSPL_ATTACHMENTS .FileName from TSPL_HR_JOINING_DETAIL  left outer join TSPL_HR_Check_List on TSPL_HR_JOINING_DETAIL .Chk_Code =TSPL_HR_Check_List.Chk_Code left outer join TSPL_ATTACHMENTS on TSPL_HR_JOINING_DETAIL .Attachment =TSPL_ATTACHMENTS.CODE and Received =1 "
    '    dt = New DataTable()
    '    dt = clsDBFuncationality.GetDataTable(qry, trans)

    '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '        obj.ObjList = New List(Of clsJoiningDetail)
    '        For Each dr As DataRow In dt.Rows
    '            Dim objTr As clsJoiningDetail = New clsJoiningDetail()
    '            objtr.Applicant_Code = strCode
    '            objtr.Chk_code = clsCommon.myCstr(dr("Chk_Code"))
    '            objTr.JoiningDescription = clsCommon.myCstr(dr("Chk_Description"))
    '            objtr.Received = clsCommon.myCstr(dr("Received"))
    '            objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
    '            objTr.Attachment = clsCommon.myCstr(dr("Attachment"))
    '            objTr.FileName = clsCommon.myCstr(dr("FileName"))
    '            obj.arrjoin.Add(objTr.Chk_code)
    '            obj.arrjoin.Add(objTr.JoiningDescription)
    '            obj.arrjoin.Add(objTr.Received)
    '            obj.arrjoin.Add(objTr.Remarks)
    '            obj.arrjoin.Add(objTr.Attachment)
    '            obj.arrjoin.Add(objTr.Applicant_Code)
    '            obj.ObjList.Add(objTr)


    '        Next
    '    End If
    '    'clsJoiningCheckListHead.ObjList = ObjList

    '    Return obj



    'End Function
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
            Dim obj As clsJoiningCheckListHead = clsJoiningCheckListHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.ApplicantCode) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_HR_JOINING_HEAD  set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' " & _
            " where Applicant_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
   
   

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            ''
            Dim Applicant_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Applicant_Code,'') As Applicant_Code from TSPL_HR_JOINING_HEAD where Applicant_Code='" + strCode + "'", trans))
            If clsCommon.myLen(Applicant_Code) > 0 Then
                Dim qry As String
                qry = "Delete From TSPL_HR_JOINING_DETAIL Where Applicant_Code ='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "Delete From TSPL_HR_JOINING_HEAD Where Applicant_Code ='" + strCode + "'"
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


End Class
Public Class clsJoiningDetail
#Region "variable"
    Public Applicant_Code As String = Nothing
    Public Chk_code As String = Nothing
    Public Remarks As String = Nothing
    Public Received As Integer
    Public Attachment As String = Nothing
    Public JoiningDescription As String = Nothing
    Public FileName As String = Nothing
    Public Attachmentid As String = Nothing
    Public JoiningMandatory As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsJoiningDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try

            Dim qry As String = "DELETE FROM TSPL_HR_JOINING_DETAIL where Applicant_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsJoiningDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Applicant_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Chk_Code", obj.Chk_code)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Received", obj.Received)
                clsCommon.AddColumnsForChange(coll, "JoiningMandatory", obj.JoiningMandatory)
                'If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_JOINING_DETAIL WHERE Applicant_Code='" + obj.Applicant_Code + "' And Chk_code ='" + obj.Chk_code + "'", trans) <= 0 Then

                '    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_JOINING_DETAIL where Applicant_Code= '" & obj.Applicant_Code & "'And Chk_code ='" + obj.Chk_code + "'"
                '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                '    If check = 0 Then
                '        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_JOINING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                '    Else
                '        common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                '        Exit Function
                'Else

                'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_JOINING_DETAIL", OMInsertOrUpdate.Update, "Applicant_Code='" + obj.Applicant_Code + "'And Chk_code ='" + obj.Chk_code + "'", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_JOINING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                'End If

                'If obj.Received = 0 Then
                '    Dim sqry As String = "delete from TSPL_ATTACHMENTS where Code='" + obj.Attachment + "'"
                '    clsDBFuncationality.ExecuteNonQuery(sqry, trans)

                'End If
                'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_JOINING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
Public Class clsJoiningAttachment
    Public Form_ID As String = ""
    Public Transaction_ID As String = ""
    Dim isInsideLoadData As Boolean = False

    ''
    Public ColCODE As String = ""
    Public ColFormId As String = ""
    Public ColTransactionId As String = ""
    Public ColSNo As String = ""
    Public ColFileName As String = ""
    Public ColCOMMENTS As String = ""
    Public ColPath As String = ""
    Public ColView As String = ""
    Public ColDelete As String = ""
    Public ColSelect As String = ""
    Dim objListAttachment As New List(Of clsJoiningAttachment)

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsJoiningAttachment), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim obj As clsAttachDocument
                For Each obj1 As clsJoiningAttachment In Arr
                    obj = New clsAttachDocument
                    obj.CODE = clsCommon.myCstr(obj1.ColCODE)
                    obj.FormId = obj1.Form_ID
                    obj.TransactionId = IIf(obj1.Transaction_ID = "", strDocNo, obj1.Transaction_ID)
                    obj.SNo = 1 'clsCommon.myCstr(obj1.ColSNo)
                    obj.FileName = clsCommon.myCstr(obj1.ColFileName)
                    obj.COMMENTS = clsCommon.myCstr(obj1.ColCOMMENTS)
                    obj.SaveData(obj)

                    Dim str As String
                    If clsCommon.myLen(obj1.ColPath) > 0 Then
                        Dim bData As Byte()
                        Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(obj1.ColPath)))
                        bData = br.ReadBytes(br.BaseStream.Length)

                        str = " UPDATE TSPL_ATTACHMENTS set FileData = @BLOBData where CODE='" + obj.CODE + "'"
                        Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                        Dim prm As New SqlParameter("@BLOBData", bData)
                        cmd.Parameters.Add(prm)
                        cmd.ExecuteNonQuery()
                        br.Close() ' done by stuti reagrding memory leakage
                    End If
                    str = "update TSPL_HR_JOINING_DETAIL set Attachment='" & obj.CODE & "' where Applicant_Code ='" & strDocNo & "' and Chk_Code='" & obj.TransactionId & "'"
                    clsDBFuncationality.ExecuteNonQuery(str, trans)
                Next
            End If
            Return True
            'LoadData(Transaction_ID)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

    End Function
End Class

