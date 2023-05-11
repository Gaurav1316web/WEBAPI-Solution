Imports System.Data.SqlClient
Imports common
'===============Created By preeti gupta===============
Public Class clsHREMInterviewQuestion
#Region "Variables"
    Public Question_Code As String = Nothing
    Public description As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsHREMInterviewQuestion, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "description", obj.description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Ques_Code", obj.Question_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Exit_Question", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Exit_Question", OMInsertOrUpdate.Update, "Tspl_HR_EM_Exit_Question.Ques_Code='" + obj.Question_Code + "'")
            End If
            'trans.Commit()
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
   
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsHREMInterviewQuestion
        Dim obj As clsHREMInterviewQuestion = Nothing
        Dim Arr As List(Of clsHREMInterviewQuestion) = Nothing
        Dim qry As String = "select Ques_Code ,description from Tspl_HR_EM_Exit_Question where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and Tspl_HR_EM_Exit_Question.Ques_Code = (select MIN(Ques_Code) from Tspl_HR_EM_Exit_Question WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and Tspl_HR_EM_Exit_Question.Ques_Code = (select Max(Ques_Code) from Tspl_HR_EM_Exit_Question WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and Tspl_HR_EM_Exit_Question.Ques_Code = (select TOP 1 Ques_Code from Tspl_HR_EM_Exit_Question WHERE 1=1 " + whrclas + " and Ques_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and Tspl_HR_EM_Exit_Question.Ques_Code = (select Min(Ques_Code) from Tspl_HR_EM_Exit_Question where Ques_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and Tspl_HR_EM_Exit_Question.Ques_Code = (select Max(Ques_Code) from Tspl_HR_EM_Exit_Question where Ques_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsHREMInterviewQuestion()
            obj.Question_Code = clsCommon.myCstr(dt.Rows(0)("Ques_Code"))
            obj.description = clsCommon.myCstr(dt.Rows(0)("description"))
        End If
        Return obj
    End Function
End Class



