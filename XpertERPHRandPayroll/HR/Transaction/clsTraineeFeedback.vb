Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Public Class clsTraineeFeedback
#Region "Variables"
    Public DocCode As String = Nothing
    Public DocDate As DateTime = Nothing
    Public ScheduleCode As String = Nothing
    Public SheduleDate As String = Nothing
    Public TrainerCode As String
    Public TrainerName As String
    Public Status As ERPTransactionStatus = 0
    Public FeedBack As String = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region
    Public Shared Function SaveData(ByVal obj As clsTraineeFeedback, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            If isNewEntry Then
                obj.DocCode = clsERPFuncationality.GetNextCode(trans, obj.DocDate, clsDocType.HRTraineeFeedBack, "", "")
            End If
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.DocDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Schedule_Code", obj.ScheduleCode, True)
            clsCommon.AddColumnsForChange(coll, "Trainer_Code", obj.TrainerCode)
            clsCommon.AddColumnsForChange(coll, "Feedback", obj.FeedBack)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))


            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.DocCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAINEE_FEEDBACK", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAINEE_FEEDBACK", OMInsertOrUpdate.Update, "Doc_Code='" + obj.DocCode + "'", trans)
            End If


            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal NavType As NavigatorType) As clsTraineeFeedback
        Dim obj As clsTraineeFeedback = Nothing
        Dim Arr As List(Of clsTraineeFeedback) = Nothing
        Dim qry As String = "select Doc_Code ,Doc_Date ,Trainer_Code ,Schedule_Code ,Feedback ,Status  from TSPL_HR_TRAINEE_FEEDBACK where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_TRAINEE_FEEDBACK.Doc_Code = (select MIN(Doc_Code) from TSPL_HR_TRAINEE_FEEDBACK WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_TRAINEE_FEEDBACK.Doc_Code = (select Max(Doc_Code) from TSPL_HR_TRAINEE_FEEDBACK WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_TRAINEE_FEEDBACK.Doc_Code = (select TOP 1 Doc_Code from TSPL_HR_TRAINEE_FEEDBACK WHERE 1=1 " + whrclas + " and Doc_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_TRAINEE_FEEDBACK.Doc_Code = (select Min(Doc_Code) from TSPL_HR_TRAINEE_FEEDBACK where Doc_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_TRAINEE_FEEDBACK.Doc_Code = (select Max(Doc_Code) from TSPL_HR_TRAINEE_FEEDBACK where Doc_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTraineeFeedback()
            obj.DocCode = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.DocDate = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.ScheduleCode = clsCommon.myCstr(dt.Rows(0)("Schedule_Code"))
            'obj.SheduleDate = clsCommon.myCstr(dt.Rows(0)("Qualification_Name"))
            obj.TrainerCode = clsCommon.myCstr(dt.Rows(0)("Trainer_Code"))
            'obj.TrainerName = clsCommon.myCstr(dt.Rows(0)("Qualification_Name"))
            obj.FeedBack = clsCommon.myCstr(dt.Rows(0)("Feedback"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If

        Return obj
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
                Throw New Exception("Document code not found to Post")
            End If
            Dim obj As clsTraineeFeedback = clsTraineeFeedback.GetData(strDocNo, trans, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DocCode) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry As String = "Update TSPL_HR_TRAINEE_FEEDBACK  set status=1 "
            qry += " where Doc_Code='" + strDocNo + "'"
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

            Dim qry As String
            qry = "Delete From TSPL_HR_TRAINEE_FEEDBACK Where Doc_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete From TSPL_HR_TRAINEE_FEEDBACK Where Doc_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
                isSaved = False
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

End Class
