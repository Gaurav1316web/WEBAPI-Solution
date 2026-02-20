Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class clsTrainerfeedbackHead
#Region "variable"
    Public DocCode As String = Nothing
    Public Description As String = Nothing
    Public ScheduleCode As String = Nothing
    Public DocDate As DateTime = Nothing
    Public SchDate As DateTime = Nothing
    Public Shared trans As SqlTransaction = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public arr As List(Of clsTrainerfeedbackDetail) = Nothing
#End Region
    Public Shared Function savedata(ByVal obj As clsTrainerfeedbackHead, ByVal isnewentry As Boolean, ByVal Arra As List(Of clsTrainerfeedbackDetail))
        trans = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Try


            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_HR_TRAINER_FEEDBACK_Detail where Doc_Code='" + obj.DocCode + "'", trans)
            Dim coll As New Hashtable()
            If isnewentry Then
                obj.DocCode = clsERPFuncationality.GetNextCode(trans, obj.DocDate, clsDocType.HRTrainerFeedBack, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.DocCode)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.DocDate, "dd/MMM/yyyy hh:mm tt "))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Schedule_Code", obj.ScheduleCode, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_TRAINER_FEEDBACK_Head WHERE Doc_Code='" + obj.DocCode + "'", trans) <= 0 Then

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAINER_FEEDBACK_Head", OMInsertOrUpdate.Insert, "", trans)

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAINER_FEEDBACK_Head", OMInsertOrUpdate.Update, "Doc_Code='" + obj.DocCode + "'", trans)

            End If
            clsTrainerfeedbackDetail.SaveData(obj.DocCode, Arra, trans)
            trans.Commit()

        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function getdata(ByVal code As String, ByVal navigatortype As NavigatorType, ByVal trans As SqlTransaction) As clsTrainerfeedbackHead
        Try
            Dim obj As clsTrainerfeedbackHead = Nothing
            Dim qst As String = "select TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code ,TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Date  ,TSPL_HR_TRAINER_FEEDBACK_Head.Status,TSPL_HR_TRAINER_FEEDBACK_Head.Description ,TSPL_HR_TRAINER_FEEDBACK_Head.Schedule_Code,TSPL_HR_TRAINER_FEEDBACK_Detail .Doc_Code ,TSPL_HR_TRAINER_FEEDBACK_Detail.EMPLOYEE_CODE ,TSPL_HR_TRAINER_FEEDBACK_Detail.Feedback,TSPL_EMPLOYEE_MASTER.Emp_Name   from TSPL_HR_TRAINER_FEEDBACK_Detail left outer join TSPL_HR_TRAINER_FEEDBACK_Head on TSPL_HR_TRAINER_FEEDBACK_Detail.Doc_Code =TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code left outer join TSPL_EMPLOYEE_MASTER  on TSPL_HR_TRAINER_FEEDBACK_Detail.EMPLOYEE_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE  where 2=2"
            Select Case navigatortype
                Case navigatortype.Current
                    qst += "and TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code in ('" + code + "')"
                Case navigatortype.Next
                    qst += "and TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code in (select  min(Doc_Code)from TSPL_HR_TRAINER_FEEDBACK_Head  where TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code  >'" + code + "')"
                Case navigatortype.First
                    qst += "and TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code in (select MIN(Doc_Code)from TSPL_HR_TRAINER_FEEDBACK_Head )"

                Case navigatortype.Last
                    qst += "and TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code in (select Max(Doc_Code)from TSPL_HR_TRAINER_FEEDBACK_Head )"
                Case navigatortype.Previous
                    qst += "and TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code in (select  max(Doc_Code)from TSPL_HR_TRAINER_FEEDBACK_Head  where TSPL_HR_TRAINER_FEEDBACK_Head.Doc_Code <'" + code + "')"
            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New clsTrainerfeedbackHead
                obj.DocCode = clsCommon.myCstr(dt1.Rows(0)("Doc_Code"))
                obj.DocDate = clsCommon.myCDate(dt1.Rows(0)("Doc_Date"))
                obj.Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                obj.ScheduleCode = clsCommon.myCstr(dt1.Rows(0)("Schedule_Code"))


            End If



            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                obj.arr = New List(Of clsTrainerfeedbackDetail)
                For Each dr As DataRow In dt1.Rows
                    Dim objTr As clsTrainerfeedbackDetail = New clsTrainerfeedbackDetail()
                    objTr.DocCode = clsCommon.myCstr(dr("Doc_Code"))
                    objTr.EmployeeCode = clsCommon.myCstr(dr("EMPLOYEE_CODE"))
                    objTr.EmployeeName = clsCommon.myCstr(dr("Emp_Name"))
                    objTr.Feedback = clsCommon.myCstr(dr("Feedback"))

                    obj.arr.Add(objTr)
                Next
                obj.Posted = IIf(clsCommon.myCdbl(dt1.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
            qry = "Delete From TSPL_HR_TRAINER_FEEDBACK_Detail Where Doc_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete From TSPL_HR_TRAINER_FEEDBACK_Head Where Doc_Code ='" + strCode + "'"
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
            Dim obj As clsTrainerfeedbackHead = clsTrainerfeedbackHead.getdata(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DocCode) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry As String = "Update TSPL_HR_TRAINER_FEEDBACK_Head  set status=1 "
            qry += " where Doc_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsTrainerfeedbackDetail
#Region "variable"
    Public DocCode As String = Nothing
    Public EmployeeCode As String = Nothing
    Public EmployeeName As String = Nothing
    Public Feedback As String = Nothing
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsTrainerfeedbackDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try

            For Each obj As clsTrainerfeedbackDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Employee_Code", obj.EmployeeCode)
                clsCommon.AddColumnsForChange(coll, "Feedback", obj.Feedback)
              
                'If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_TRAINER_FEEDBACK_Detail WHERE Doc_Code='" + obj.DocCode + "'", trans) <= 0 Then

                '    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_TRAINER_FEEDBACK_Detail where Doc_Code= '" & strCode & "'"
                '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                '    If check = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAINER_FEEDBACK_Detail", OMInsertOrUpdate.Insert, "", trans)
                '    Else
                'common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                'Exit Function
                '    End If
                'Else

                'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAINER_FEEDBACK_Detail", OMInsertOrUpdate.Update, "Doc_Code='" + obj.DocCode + "', trans)
                'End If

            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
#End Region
End Class