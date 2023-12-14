Imports System.Data.SqlClient
Imports common

Public Class ClsJobTitle

#Region "Variables"
    Public Job_Title_Code As String = Nothing
    Public Job_Title As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
    Public arrInterview As List(Of ClsChkInterviewJoblist) = Nothing
    Public arrOffer As List(Of ClsChkOfferJoblist) = Nothing
    Public arrJoin As List(Of ClsChkJoiningJoblist) = Nothing
    Public arrinter As New ArrayList
    Public Shared trans As SqlTransaction = Nothing
#End Region


    Public Shared Function SaveData(ByVal obj As ClsJobTitle, ByVal isNewEntry As Boolean, ByVal Ara As List(Of ClsChkOfferJoblist), ByVal Arraa As List(Of ClsChkJoiningJoblist)) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        trans = clsDBFuncationality.GetTransactin()
        Try
            'clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_HR_Interview_ChkList_JOB_TITLE WHERE job_title_code='" + obj.Job_Title_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_HR_Offer_ChkList_JOB_TITLE WHERE job_title_code='" + obj.Job_Title_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_HR_Joining_ChkList_JOB_TITLE WHERE job_title_code='" + obj.Job_Title_Code + "'", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Job_Title", obj.Job_Title)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Job_Title_Code", obj.Job_Title_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_JOB_TITLE", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_JOB_TITLE", OMInsertOrUpdate.Update, "TSPL_HR_JOB_TITLE.Job_Title_Code='" + obj.Job_Title_Code + "'", trans)
            End If
            'ClsChkInterviewJoblist.SaveData(obj.Job_Title_Code, Arra, trans)
            ClsChkOfferJoblist.SaveData(obj.Job_Title_Code, Ara, trans)
            ClsChkJoiningJoblist.SaveData(obj.Job_Title_Code, Arraa, trans)
            trans.Commit()

            'If Not isNewEntry Then
            '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Job_Title_Code), "TSPL_HR_JOB_TITLE", "Job_Title_Code", trans)
            'End If

        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
   

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsJobTitle
        Dim obj As ClsJobTitle = Nothing
        Dim objInterview As ClsChkInterviewJoblist = Nothing
        Dim objoffer As ClsChkOfferJoblist = Nothing
        Dim Arr As List(Of ClsJobTitle) = Nothing
        Dim qry As String = "select * from TSPL_HR_JOB_TITLE where 2=2 "

        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.Current
                qry += "and TSPL_HR_JOB_TITLE.Job_Title_Code in ('" + strCode + "')"
            Case NavigatorType.Next
                qry += "and TSPL_HR_JOB_TITLE.Job_Title_Code  in (select  min(Job_Title_Code) from TSPL_HR_JOB_TITLE where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Job_Title_Code >'" + strCode + "')"
            Case NavigatorType.First
                qry += "and TSPL_HR_JOB_TITLE.Job_Title_Code  in (select MIN(Job_Title_Code) from TSPL_HR_JOB_TITLE where comp_code='" + objCommonVar.CurrentCompanyCode + "')"

            Case NavigatorType.Last
                qry += "and TSPL_HR_JOB_TITLE.Job_Title_Code  in (select Max(Job_Title_Code) from TSPL_HR_JOB_TITLE where comp_code='" + objCommonVar.CurrentCompanyCode + "')"
            Case NavigatorType.Previous
                qry += "and TSPL_HR_JOB_TITLE.Job_Title_Code  in (select  max(Job_Title_Code) from TSPL_HR_JOB_TITLE where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Job_Title_Code <'" + strCode + "')"


        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsJobTitle()
            obj.Job_Title_Code = clsCommon.myCstr(dt.Rows(0)("Job_Title_Code"))
            obj.Job_Title = clsCommon.myCstr(dt.Rows(0)("Job_Title"))
            strCode = clsCommon.myCstr(dt.Rows(0)("Job_Title_Code"))

            ''--FOR INTERVIEW
            'qry = "select  TSPL_HR_Interview_ChkList_JOB_TITLE.Chk_Code , TSPL_HR_Interview_ChkList_JOB_TITLE.Job_Title_Code , TSPL_HR_Interview_ChkList_JOB_TITLE.mandatory " _
            '    & ",TSPL_HR_Check_List.Chk_Description   from TSPL_HR_Interview_ChkList_JOB_TITLE left outer join TSPL_HR_JOB_TITLE on TSPL_HR_Interview_ChkList_JOB_TITLE.Job_Title_Code" _
            '    & " =TSPL_HR_JOB_TITLE.Job_Title_Code left outer join TSPL_HR_Check_List on TSPL_HR_Interview_ChkList_JOB_TITLE .Chk_Code = TSPL_HR_Check_List.Chk_Code " _
            '    & " where TSPL_HR_JOB_TITLE.Job_Title_Code='" + strCode + "'"
            'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            'If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
            '    obj.arrInterview = New List(Of ClsChkInterviewJoblist)
            '    For Each dr As DataRow In dt1.Rows
            '        Dim objTr As ClsChkInterviewJoblist = New ClsChkInterviewJoblist()

            '        objTr.Mandatory = clsCommon.myCdbl(dr("mandatory"))
            '        objTr.ChkListCode = clsCommon.myCstr(dr("Chk_Code"))
            '        objTr.Code = clsCommon.myCstr(dr("Job_Title_Code"))
            '        objTr.Description = clsCommon.myCstr(dr("Chk_Description"))
            '        'obj.arrqul.Add(objTr.Mandatory)
            '        obj.arrinter.Add(objTr.ChkListCode)
            '        obj.arrinter.Add(objTr.Code)
            '        obj.arrinter.Add(objTr.Description)

            '        obj.arrInterview.Add(objTr)
            '    Next
            'End If

            '---FOR OFFER
            qry = "select TSPL_HR_Offer_ChkList_JOB_TITLE  .Job_Title_Code ,TSPL_HR_Offer_ChkList_JOB_TITLE  .Chk_Code ,TSPL_HR_Offer_ChkList_JOB_TITLE  .mandatory , TSPL_HR_Check_List.Chk_Description from TSPL_HR_Offer_ChkList_JOB_TITLE left outer join TSPL_HR_JOB_TITLE on TSPL_HR_Offer_ChkList_JOB_TITLE .Job_Title_Code =TSPL_HR_JOB_TITLE.Job_Title_Code left outer join TSPL_HR_Check_List on TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code =TSPL_HR_Check_List.Chk_Code   where TSPL_HR_JOB_TITLE.Job_Title_Code='" + strCode + "'"

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                obj.arrOffer = New List(Of ClsChkOfferJoblist)
                For Each dr As DataRow In dt2.Rows
                    Dim objTr As ClsChkOfferJoblist = New ClsChkOfferJoblist()

                    objTr.Mandatory = clsCommon.myCdbl(dr("mandatory"))
                    objTr.ChkListCode = clsCommon.myCstr(dr("Chk_Code"))
                    objTr.Code = clsCommon.myCstr(dr("Job_Title_Code"))
                    objTr.Description = clsCommon.myCstr(dr("Chk_Description"))
                    'obj.arrqul.Add(objTr.Mandatory)
                    obj.arrinter.Add(objTr.ChkListCode)
                    obj.arrinter.Add(objTr.Code)
                    obj.arrinter.Add(objTr.Description)
                    obj.arrOffer.Add(objTr)
                Next
            End If
            '-----FOR JOINING
            qry = "select TSPL_HR_Joining_ChkList_JOB_TITLE.Job_Title_Code, TSPL_HR_Joining_ChkList_JOB_TITLE.Chk_Code, TSPL_HR_Joining_ChkList_JOB_TITLE.mandatory, TSPL_HR_Check_List.Chk_Description from TSPL_HR_Joining_ChkList_JOB_TITLE left outer join TSPL_HR_JOB_TITLE on TSPL_HR_Joining_ChkList_JOB_TITLE.Job_Title_Code =TSPL_HR_JOB_TITLE.Job_Title_Code left outer join TSPL_HR_Check_List on TSPL_HR_Joining_ChkList_JOB_TITLE.Chk_Code =TSPL_HR_Check_List.Chk_Code where TSPL_HR_JOB_TITLE.Job_Title_Code='" + strCode + "'"

            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt3 IsNot Nothing AndAlso dt3.Rows.Count > 0) Then
                obj.arrJoin = New List(Of ClsChkJoiningJoblist)
                For Each dr As DataRow In dt3.Rows
                    Dim objTr As ClsChkJoiningJoblist = New ClsChkJoiningJoblist()

                    objTr.Mandatory = clsCommon.myCdbl(dr("mandatory"))
                    objTr.ChkListCode = clsCommon.myCstr(dr("Chk_Code"))
                    objTr.Code = clsCommon.myCstr(dr("Job_Title_Code"))
                    objTr.Description = clsCommon.myCstr(dr("Chk_Description"))
                    'obj.arrqul.Add(objTr.Mandatory)
                    obj.arrinter.Add(objTr.ChkListCode)
                    obj.arrinter.Add(objTr.Code)
                    obj.arrinter.Add(objTr.Description)
                    obj.arrJoin.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
End Class
Public Class ClsChkInterviewJoblist
#Region "Variables"
    Public Code As String = Nothing
    Public ChkListCode As String = Nothing
    Public Mandatory As Integer = Nothing
    Public Description As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal Code As String, ByVal Arr As List(Of ClsChkInterviewJoblist), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each chk As ClsChkInterviewJoblist In Arr
                Try

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Job_Title_Code", Code)
                    clsCommon.AddColumnsForChange(coll, "Chk_Code", chk.ChkListCode)
                    clsCommon.AddColumnsForChange(coll, "mandatory", chk.Mandatory)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Interview_ChkList_JOB_TITLE ", OMInsertOrUpdate.Insert, "", trans)
                    'clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Offer_ChkList_JOB_TITLE ", OMInsertOrUpdate.Insert, "", trans)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Next chk
        End If
        Return True
    End Function

End Class
Public Class ClsChkOfferJoblist
#Region "Variables"
    Public Code As String = Nothing
    Public ChkListCode As String = Nothing
    Public Mandatory As Integer = Nothing
    Public Description As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal Code As String, ByVal Arr As List(Of ClsChkOfferJoblist), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each chk As ClsChkOfferJoblist In Arr
                Try

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Job_Title_Code", Code)
                    clsCommon.AddColumnsForChange(coll, "Chk_Code", chk.ChkListCode)
                    clsCommon.AddColumnsForChange(coll, "mandatory", chk.Mandatory)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Offer_ChkList_JOB_TITLE ", OMInsertOrUpdate.Insert, "", trans)
                    'clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Offer_ChkList_JOB_TITLE ", OMInsertOrUpdate.Insert, "", trans)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Next chk
        End If
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim obj As ClsJobTitle = ClsJobTitle.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Job_Title_Code) > 0) Then
            Try
                

                'Dim qry As String = "Delete from TSPL_HR_Interview_ChkList_JOB_TITLE WHERE job_title_code='" + strCode + "'"
                'isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Dim qry As String = "Delete from TSPL_HR_Offer_ChkList_JOB_TITLE WHERE job_title_code ='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "Delete from TSPL_HR_Joining_ChkList_JOB_TITLE WHERE job_title_code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "DELETE FROM TSPL_HR_JOB_TITLE WHERE Job_Title_Code ='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception

                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

End Class
Public Class ClsChkJoiningJoblist
#Region "Variables"
    Public Code As String = Nothing
    Public ChkListCode As String = Nothing
    Public Mandatory As Integer = Nothing
    Public Description As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal Code As String, ByVal Arraa As List(Of ClsChkJoiningJoblist), ByVal trans As SqlTransaction) As Boolean
        If (Arraa IsNot Nothing AndAlso Arraa.Count > 0) Then
            For Each chk As ClsChkJoiningJoblist In Arraa
                Try

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Job_Title_Code", Code)
                    clsCommon.AddColumnsForChange(coll, "Chk_Code", chk.ChkListCode)
                    clsCommon.AddColumnsForChange(coll, "mandatory", chk.Mandatory)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Joining_ChkList_JOB_TITLE ", OMInsertOrUpdate.Insert, "", trans)

                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Next chk
        End If
        Return True
    End Function
    End Class 

