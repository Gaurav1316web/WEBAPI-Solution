Imports System.Data.SqlClient
Imports common
'=======================Created By preeti Gupta============
Public Class clsHREMExitInterview
#Region "Variables"
    Public exit_code As String = Nothing
    Public Exit_Date As DateTime
    Public user_code As String = Nothing
    Public User_Name As String = Nothing
    Public Supervisor_code As String = Nothing
    Public SuperVisor_Name As String = Nothing
    Public DEPARTMENT_CODE As String = Nothing
    Public Department_Name As String = Nothing
    Public DESIGNATION_ID As String = Nothing
    Public Designation_Name As String = Nothing
    Public Reson_Of_Leaving As String = Nothing
    Public Detail_Reson As String = Nothing
    Public Suggestion As String = Nothing
    Public Return_To_Work_Here As String = Nothing
    Public Frnd_Recommend As String = Nothing
    Public Shared trans As SqlTransaction = Nothing
    Public arr As List(Of clsHrEmexitInterviewDetail) = Nothing
    Public arrqul As New ArrayList
#End Region
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""

        Dim qry As String = "select Tspl_HR_EM_Exit_Interview_Head.exit_code as [Exit Code],Tspl_HR_EM_Exit_Interview_Head.Exit_Date as [Exit Date] ,Tspl_HR_EM_Exit_Interview_Head.user_code as [User Code],TSPL_USER_MASTER.User_Name as [User Name]  ,Tspl_HR_EM_Exit_Interview_Head.Supervisor_code as [Supervisor Code],Supervisor.User_Name as [Supervisor Name] ,Tspl_HR_EM_Exit_Interview_Head.DEPARTMENT_CODE as [Department Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name] ,Tspl_HR_EM_Exit_Interview_Head.DESIGNATION_ID as [Position Code],TSPL_DESIGNATION_MASTER.Designation_Desc as [Position Name] ,Tspl_HR_EM_Exit_Interview_Head.Reson_Of_Leaving as [Reson of leaving],Tspl_HR_EM_Exit_Interview_Head.Detail_Reson as [Detail Reson],Tspl_HR_EM_Exit_Interview_Head.Suggestion ,Tspl_HR_EM_Exit_Interview_Head.Return_To_Work_Here as [Return to Work Here] ,Tspl_HR_EM_Exit_Interview_Head.Frnd_Recommend as [Friend Recommend]  from Tspl_HR_EM_Exit_Interview_Head"
        qry += " left outer join TSPL_USER_MASTER on tspl_user_master.User_Code =Tspl_HR_EM_Exit_Interview_Head.user_code "
        qry += " left outer join tspl_user_master as Supervisor on Supervisor.User_Code =Tspl_HR_EM_Exit_Interview_Head.Supervisor_code "
        qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Exit_Interview_Head.DEPARTMENT_CODE"
        qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER .Designation_id =Tspl_HR_EM_Exit_Interview_Head.DESIGNATION_ID"
        str = clsCommon.ShowSelectForm("ExitInterview", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function savedata(ByVal ExitInterview As clsHREMExitInterview, ByVal isnewentry As Boolean, ByVal Arra As List(Of clsHrEmexitInterviewDetail))
        trans = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As clsHREMExitInterview = New clsHREMExitInterview

            clsDBFuncationality.ExecuteNonQuery("delete from Tspl_HR_EM_Exit_Interview_detail where exit_code='" + ExitInterview.exit_code + "'", trans)
            Dim coll As New Hashtable()
            If isnewentry Then
                ExitInterview.exit_code = clsERPFuncationality.GetNextCode(trans, ExitInterview.Exit_Date, clsDocType.HRExitInterview, "", "")
            End If
            clsCommon.AddColumnsForChange(coll, "Exit_Date", clsCommon.GetPrintDate(ExitInterview.Exit_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "user_code", ExitInterview.user_code)
            clsCommon.AddColumnsForChange(coll, "Supervisor_code", ExitInterview.Supervisor_code)
            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", ExitInterview.DEPARTMENT_CODE)
            clsCommon.AddColumnsForChange(coll, "DESIGNATION_ID", ExitInterview.DESIGNATION_ID)
            clsCommon.AddColumnsForChange(coll, "Reson_Of_Leaving", ExitInterview.Reson_Of_Leaving)
            clsCommon.AddColumnsForChange(coll, "Detail_Reson", ExitInterview.Detail_Reson)
            clsCommon.AddColumnsForChange(coll, "Suggestion", ExitInterview.Suggestion)
            clsCommon.AddColumnsForChange(coll, "Return_To_Work_Here", ExitInterview.Return_To_Work_Here)
            clsCommon.AddColumnsForChange(coll, "Frnd_Recommend", ExitInterview.Frnd_Recommend)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isnewentry Then
                clsCommon.AddColumnsForChange(coll, "exit_code", ExitInterview.exit_code)
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Exit_Interview_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Exit_Interview_Head", OMInsertOrUpdate.Update, " exit_code='" + ExitInterview.exit_code + "'", trans)
            End If
            clsHrEmexitInterviewDetail.savedata(ExitInterview.exit_code, Arra, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getdata(ByVal code As String, ByVal navigatortype As NavigatorType) As clsHREMExitInterview
        Try
            Dim obj As clsHREMExitInterview = Nothing
            Dim qst As String = (" select Tspl_HR_EM_Exit_Question.Description as [Ques Name],Tspl_HR_EM_Exit_Interview_Head.exit_code as [Exit Code],Tspl_HR_EM_Exit_Interview_Head.Exit_Date as [Exit Date] ,Tspl_HR_EM_Exit_Interview_Head.user_code as [User Code],TSPL_USER_MASTER.User_Name as [User Name]  ,Tspl_HR_EM_Exit_Interview_Head.Supervisor_code as [Supervisor Code],Supervisor.User_Name as [Supervisor Name] ,Tspl_HR_EM_Exit_Interview_Head.DEPARTMENT_CODE as [Department Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name] ,Tspl_HR_EM_Exit_Interview_Head.DESIGNATION_ID as [Position Code],TSPL_DESIGNATION_MASTER.Designation_Desc as [Position Name] ,Tspl_HR_EM_Exit_Interview_Head.Reson_Of_Leaving as [Reson of leaving],Tspl_HR_EM_Exit_Interview_Head.Detail_Reson as [Detail Reson],Tspl_HR_EM_Exit_Interview_Head.Suggestion ,Tspl_HR_EM_Exit_Interview_Head.Return_To_Work_Here as [Return to Work Here] ,Tspl_HR_EM_Exit_Interview_Head.Frnd_Recommend as [Friend Recommend],Tspl_HR_EM_Exit_Interview_detail.line_no,Tspl_HR_EM_Exit_Interview_detail.Ques_code,Tspl_HR_EM_Exit_Interview_detail.Strongly_Agree,Tspl_HR_EM_Exit_Interview_detail.SomeWhat_agree,Tspl_HR_EM_Exit_Interview_detail.SomeWhat_disgree,Strongly_Disagree  from Tspl_HR_EM_Exit_Interview_Head left outer join TSPL_USER_MASTER on tspl_user_master.User_Code =Tspl_HR_EM_Exit_Interview_Head.user_code  left outer join tspl_user_master as Supervisor on Supervisor.User_Code =Tspl_HR_EM_Exit_Interview_Head.Supervisor_code  left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Exit_Interview_Head.DEPARTMENT_CODE left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER .Designation_id =Tspl_HR_EM_Exit_Interview_Head.DESIGNATION_ID left outer join Tspl_HR_EM_Exit_Interview_detail on Tspl_HR_EM_Exit_Interview_detail.exit_code=Tspl_HR_EM_Exit_Interview_Head.Exit_code  left outer join Tspl_HR_EM_Exit_Question on Tspl_HR_EM_Exit_Question.Ques_Code =Tspl_HR_EM_Exit_Interview_detail.Ques_Code where 2=2")
            Select Case navigatortype
                Case navigatortype.Current
                    qst += "and Tspl_HR_EM_Exit_Interview_Head.exit_code in ('" + code + "')"
                Case navigatortype.Next
                    qst += "and Tspl_HR_EM_Exit_Interview_Head.exit_code in (select  min(exit_code)from Tspl_HR_EM_Exit_Interview_Head  where Tspl_HR_EM_Exit_Interview_Head.exit_code  >'" + code + "')"
                Case navigatortype.First
                    qst += "and Tspl_HR_EM_Exit_Interview_Head.exit_code in (select MIN(exit_code)from Tspl_HR_EM_Exit_Interview_Head )"

                Case navigatortype.Last
                    qst += "and Tspl_HR_EM_Exit_Interview_Head.exit_code in (select Max(exit_code)from Tspl_HR_EM_Exit_Interview_Head )"
                Case navigatortype.Previous
                    qst += "and Tspl_HR_EM_Exit_Interview_Head.exit_code in (select  max(exit_code)from Tspl_HR_EM_Exit_Interview_Head  where Tspl_HR_EM_Exit_Interview_Head.exit_code <'" + code + "')"
            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New clsHREMExitInterview
                obj.exit_code = clsCommon.myCstr(dt1.Rows(0)("Exit Code"))
                obj.Exit_Date = clsCommon.myCDate(dt1.Rows(0)("Exit Date"))
                obj.user_code = clsCommon.myCstr(dt1.Rows(0)("User Code"))
                obj.User_Name = clsCommon.myCstr(dt1.Rows(0)("User Name"))
                obj.Supervisor_code = clsCommon.myCstr(dt1.Rows(0)("Supervisor Code"))
                obj.SuperVisor_Name = clsCommon.myCstr(dt1.Rows(0)("Supervisor Name"))
                obj.DEPARTMENT_CODE = clsCommon.myCstr(dt1.Rows(0)("Department Code"))
                obj.Department_Name = clsCommon.myCstr(dt1.Rows(0)("Department Name"))
                obj.DESIGNATION_ID = clsCommon.myCstr(dt1.Rows(0)("Position Code"))
                obj.Designation_Name = clsCommon.myCstr(dt1.Rows(0)("Position Name"))
                obj.Reson_Of_Leaving = clsCommon.myCstr(dt1.Rows(0)("Reson of leaving"))
                obj.Suggestion = clsCommon.myCstr(dt1.Rows(0)("Suggestion"))
                obj.Detail_Reson = clsCommon.myCstr(dt1.Rows(0)("Detail Reson"))
                obj.Return_To_Work_Here = clsCommon.myCstr(dt1.Rows(0)("Return to Work Here"))
                obj.Frnd_Recommend = clsCommon.myCstr(dt1.Rows(0)("Friend Recommend"))

            End If
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                obj.arr = New List(Of clsHrEmexitInterviewDetail)
                For Each dr As DataRow In dt1.Rows
                    Dim objTr As clsHrEmexitInterviewDetail = New clsHrEmexitInterviewDetail()
                    objTr.line_no = clsCommon.myCdbl(dr("line_no"))
                    objTr.Ques_code = clsCommon.myCstr(dr("Ques_code"))
                    objTr.Ques_Name = clsCommon.myCstr(dr("Ques Name"))
                    objTr.Strongly_Agree = clsCommon.myCstr(dr("Strongly_Agree"))
                    objTr.SomeWhat_agree = clsCommon.myCstr(dr("SomeWhat_agree"))
                    objTr.SomeWhat_disgree = clsCommon.myCstr(dr("SomeWhat_disgree"))
                    objTr.Strongly_Disagree = clsCommon.myCstr(dr("Strongly_Disagree"))
                    obj.arr.Add(objTr)
                    obj.arrqul.Add(objTr.Exit_Code)
                Next
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
            qry = "DELETE FROM Tspl_HR_EM_Exit_Interview_detail WHERE exit_code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "DELETE FROM Tspl_HR_EM_Exit_Interview_Head WHERE exit_code ='" + strCode + "'"
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
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

End Class
Public Class clsHrEmexitInterviewDetail


#Region "Variables"
    Public Exit_Code As String
    Public line_no As Integer
    Public Ques_code As String
    Public Ques_Name As String
    Public Strongly_Agree As String
    Public SomeWhat_agree As String
    Public SomeWhat_disgree As String
    Public Strongly_Disagree As String
#End Region
    Public Shared Function savedata(ByVal ExitInter As String, ByVal arr As List(Of clsHrEmexitInterviewDetail), ByVal trans As SqlTransaction) As Boolean

        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each ExitInterview As clsHrEmexitInterviewDetail In arr
                Try

                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "exit_code", ExitInter)
                    clsCommon.AddColumnsForChange(coll, "line_no", ExitInterview.line_no)
                    clsCommon.AddColumnsForChange(coll, "Ques_code", ExitInterview.Ques_code)
                    clsCommon.AddColumnsForChange(coll, "Strongly_Agree", ExitInterview.Strongly_Agree)
                    clsCommon.AddColumnsForChange(coll, "SomeWhat_agree", ExitInterview.SomeWhat_agree)
                    clsCommon.AddColumnsForChange(coll, "SomeWhat_disgree", ExitInterview.SomeWhat_disgree)
                    clsCommon.AddColumnsForChange(coll, "Strongly_Disagree", ExitInterview.Strongly_Disagree)
                    clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Exit_Interview_detail", OMInsertOrUpdate.Insert, "", trans)


                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Next ExitInterview

        End If
        Return True
    End Function
End Class


