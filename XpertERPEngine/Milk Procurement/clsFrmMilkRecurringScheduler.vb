Imports common
Imports System.Data.SqlClient
Public Class clsFrmMilkRecurringScheduler
#Region "Variables"
    Public code As String = ""
    Public desc As String = Nothing

    Public DOC_DATE As DateTime
    Public User_Mode As String = String.Empty
    Public Remind_In_Advance As Integer = 0
    Public Recurring_period As String = String.Empty
    Public Sch_Start_date As String = String.Empty
    Public Duration As Double
    Public Posted As ERPTransactionStatus
    Public Posting_Date As DateTime
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modified_By As String = String.Empty
    Public Modified_Date As String = String.Empty
    Public Comp_Code As String = String.Empty

    Public Shared arr_Recurring_Detail As List(Of clsfrmMilkRecurringScheduler_Detail)
    Public Shared arr_Recurring_User_Detail As List(Of clsfrmMilkRecurringScheduler_User_Detail)

#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select Tspl_Recurring_Scheduler_Head.Doc_code as Code,Tspl_Recurring_Scheduler_Head.Doc_Date as [Doc Date],Tspl_Recurring_Scheduler_Head.Description as [Description],Tspl_Recurring_Scheduler_Head.created_By as [Created By],Tspl_Recurring_Scheduler_Head.created_By as [Created By],Tspl_Recurring_Scheduler_Head.created_Date as [Created Date],Tspl_Recurring_Scheduler_Head.modified_by as [Modified By],Tspl_Recurring_Scheduler_Head.modified_date as [Modified Date] from Tspl_Recurring_Scheduler_Head "
            str = clsCommon.ShowSelectForm("MCC_Rec", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "Tspl_Recurring_Scheduler_Head.Doc_Date")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal obj As clsFrmMilkRecurringScheduler, ByVal isNewEntry As Boolean) As Boolean
        Try
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.DOC_DATE, clsDocType.MilkRecurringSchedule, "", ""))
                strCode = obj.code
            End If


            Dim coll As New Hashtable()

            obj.code = strCode
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.code)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy")))
            clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
            clsCommon.AddColumnsForChange(coll, "User_Mode", obj.User_Mode)
            clsCommon.AddColumnsForChange(coll, "Remind_In_Advance", obj.Remind_In_Advance)
            clsCommon.AddColumnsForChange(coll, "Sch_Start_date", clsCommon.GetPrintDate(obj.Sch_Start_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Recurring_period", obj.Recurring_period)
            clsCommon.AddColumnsForChange(coll, "Duration", obj.Duration)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Recurring_Scheduler_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Recurring_Scheduler_Head", OMInsertOrUpdate.Update, " Tspl_Recurring_Scheduler_Head.Doc_code='" + obj.code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsfrmMilkRecurringScheduler_Detail.SaveData(obj.code, clsFrmMilkRecurringScheduler.arr_Recurring_Detail, trans)
            If obj.User_Mode = "N" Then
                isSaved = isSaved AndAlso clsfrmMilkRecurringScheduler_User_Detail.SaveData(obj.code, clsFrmMilkRecurringScheduler.arr_Recurring_User_Detail, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsFrmMilkRecurringScheduler
        Try
            Dim objtr As New clsfrmMilkRecurringScheduler_Detail
            Dim objtr_user As New clsfrmMilkRecurringScheduler_User_Detail

            arr_Recurring_Detail = New List(Of clsfrmMilkRecurringScheduler_Detail)
            arr_Recurring_User_Detail = New List(Of clsfrmMilkRecurringScheduler_User_Detail)


            Dim qry As String = "select * from Tspl_Recurring_Scheduler_Head "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where Tspl_Recurring_Scheduler_Head.Doc_code='" + strCode + "' "
                Case NavigatorType.First
                    qry += " where Tspl_Recurring_Scheduler_Head.Doc_code in (select min(Doc_code) from Tspl_Recurring_Scheduler_Head where 1=1 )"
                Case NavigatorType.Last
                    qry += " where Tspl_Recurring_Scheduler_Head.Doc_code in (select max(Doc_code) from Tspl_Recurring_Scheduler_Head where 1=1 )"
                Case NavigatorType.Previous
                    qry += " where Tspl_Recurring_Scheduler_Head.Doc_code in (select max(Doc_code) from Tspl_Recurring_Scheduler_Head where Doc_code<'" + strCode + "')"
                Case NavigatorType.Next
                    qry += " where Tspl_Recurring_Scheduler_Head.Doc_code in (select min(Doc_code) from Tspl_Recurring_Scheduler_Head where Doc_code>'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim obj As clsFrmMilkRecurringScheduler = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsFrmMilkRecurringScheduler()

                obj.code = clsCommon.myCstr(dt.Rows(0)("Doc_code"))
                obj.desc = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.DOC_DATE = clsCommon.myCstr(dt.Rows(0)("Doc_date"))
                obj.User_Mode = clsCommon.myCstr(dt.Rows(0)("User_Mode"))
                obj.Remind_In_Advance = CInt(clsCommon.myCdbl(dt.Rows(0)("Remind_In_Advance")))
                obj.Recurring_period = clsCommon.myCstr(dt.Rows(0)("Recurring_period"))
                obj.Sch_Start_date = clsCommon.myCstr(dt.Rows(0)("Sch_Start_Date"))
                obj.Duration = clsCommon.myCstr(dt.Rows(0)("Duration"))
                obj.Created_By = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
                obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                '        strCode = dt.Rows(0)("DOC_CODE")

                If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                Else
                    obj.Posting_Date = Nothing
                End If

                qry = "select * from Tspl_Recurring_Scheduler_Detail where Doc_Code='" & obj.code & "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)

                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        objtr = New clsfrmMilkRecurringScheduler_Detail

                        objtr.Doc_COde = strCode
                        objtr.Checked_Value = clsCommon.myCstr(dr("Checked_Value_Name"))
                        objtr.Day_Index = clsCommon.myCstr(dr("Day_Index"))
                        objtr.Day_of_Week = clsCommon.myCstr(dr("Day_Name"))
                        arr_Recurring_Detail.Add(objtr)
                    Next
                End If
                clsFrmMilkRecurringScheduler.arr_Recurring_Detail = arr_Recurring_Detail

                qry = "select * from Tspl_Recurring_Scheduler_User_Detail where Doc_Code='" & obj.code & "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)

                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        objtr_user = New clsfrmMilkRecurringScheduler_User_Detail

                        objtr_user.Doc_Code = strCode
                        objtr_user.User_Code = clsCommon.myCstr(dr("User_Code"))

                        arr_Recurring_User_Detail.Add(objtr_user)
                    Next
                End If
                clsFrmMilkRecurringScheduler.arr_Recurring_Detail = arr_Recurring_Detail
                clsFrmMilkRecurringScheduler.arr_Recurring_User_Detail = arr_Recurring_User_Detail

            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsfrmMilkRecurringScheduler_Detail
#Region "Variables"
    Public Doc_COde As String
    Public Checked_Value As String
    Public Day_of_Week As String
    Public Day_Index As String
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsfrmMilkRecurringScheduler_Detail), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "delete from Tspl_Recurring_Scheduler_Detail where Doc_Code='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsfrmMilkRecurringScheduler_Detail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Checked_Value_Name", obj.Checked_Value)
                clsCommon.AddColumnsForChange(coll, "Day_Index", obj.Day_Index)
                clsCommon.AddColumnsForChange(coll, "Day_Name", obj.Day_of_Week)

                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Recurring_Scheduler_Detail", OMInsertOrUpdate.Insert, "Tspl_Recurring_Scheduler_Detail.Doc_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
End Class

Public Class clsfrmMilkRecurringScheduler_User_Detail
#Region "Variables"
    Public Doc_Code As String
    Public User_Code As String
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsfrmMilkRecurringScheduler_User_Detail), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "delete from Tspl_Recurring_Scheduler_User_Detail where Doc_Code='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsfrmMilkRecurringScheduler_User_Detail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "User_Code", obj.User_Code)

                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Recurring_Scheduler_User_Detail", OMInsertOrUpdate.Insert, "Tspl_Recurring_Scheduler_User_Detail.Doc_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
End Class

