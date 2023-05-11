Imports common
Imports System.Data.SqlClient

Public Class clsSendSMSEmailSetting
#Region "Variables"
    Public SCHEDULER_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public SCREEN_ID As String = Nothing

    Public IS_EMAIL_EVERY_DAY As Boolean = Nothing
    Public IS_EMAIL_WEEKLY As Boolean = Nothing
    Public EMAIL_WEEKLY_DAY As String = Nothing
    Public IS_EMAIL_MONTHLY As Boolean = Nothing
    Public MONTHLY_EMAIL_DATE As Date? = Nothing
    Public IS_EMAIL_LAST_DAY_OF_MONTHLY As Boolean = Nothing
    Public SCHEDULE_EMAIL_TIME As Date? = Nothing

    Public IS_SMS_EVERY_DAY As Boolean = Nothing
    Public IS_SMS_WEEKLY As Boolean = Nothing
    Public SMS_WEEKLY_DAY As String = Nothing
    Public IS_SMS_MONTHLY As Boolean = Nothing
    Public MONTHLY_SMS_DATE As Date? = Nothing
    Public IS_SMS_LAST_DAY_OF_MONTHLY As Boolean = Nothing
    Public SCHEDULE_SMS_TIME As Date? = Nothing
    'TSPL_EMAIL_SMS_SCHEDULING
    'SCHEDULER_CODE,DESCRIPTION,SCREEN_ID,IS_EMAIL_EVERY_DAY,IS_EMAIL_WEEKLY,EMAIL_WEEKLY_DAY,IS_EMAIL_MONTHLY,MONTHLY_EMAIL_DATE,IS_EMAIL_LAST_DAY_OF_MONTHLY,SCHEDULE_EMAIL_TIME
    'IS_SMS_EVERY_DAY,IS_SMS_WEEKLY,SMS_WEEKLY_DAY,IS_SMS_MONTHLY,MONTHLY_SMS_DATE,IS_SMS_LAST_DAY_OF_MONTHLY,SCHEDULE_SMS_TIME
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_EMAIL_SMS_SCHEDULING.SCHEDULER_CODE as [Code] ,TSPL_EMAIL_SMS_SCHEDULING.DESCRIPTION as [Description] ,TSPL_EMAIL_SMS_SCHEDULING.Created_By as [Created By] ,TSPL_EMAIL_SMS_SCHEDULING.Created_Date as [Created Date] ,TSPL_EMAIL_SMS_SCHEDULING.Modified_By as [Modified By] ,TSPL_EMAIL_SMS_SCHEDULING.Modified_Date as [Modified Date]  From TSPL_EMAIL_SMS_SCHEDULING   "
        str = clsCommon.ShowSelectForm("EMAIL_SMS_SHEDULING@FND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

   

    Public Function SaveData(ByVal arr As List(Of clsSendSMSEmailSetting), ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            isSaved = SaveData(arr, isNewEntry, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal arr As List(Of clsSendSMSEmailSetting), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As clsSendSMSEmailSetting In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "SCREEN_ID", obj.SCREEN_ID)

                clsCommon.AddColumnsForChange(coll, "IS_EMAIL_EVERY_DAY", obj.IS_EMAIL_EVERY_DAY)
                clsCommon.AddColumnsForChange(coll, "IS_EMAIL_WEEKLY", obj.IS_EMAIL_WEEKLY)
                clsCommon.AddColumnsForChange(coll, "EMAIL_WEEKLY_DAY", obj.EMAIL_WEEKLY_DAY, True)
                clsCommon.AddColumnsForChange(coll, "IS_EMAIL_MONTHLY", obj.IS_EMAIL_MONTHLY)
                If String.IsNullOrEmpty(clsCommon.myCstr(obj.MONTHLY_EMAIL_DATE)) = False Then
                    clsCommon.AddColumnsForChange(coll, "MONTHLY_EMAIL_DATE", clsCommon.GetPrintDate(obj.MONTHLY_EMAIL_DATE, "dd/MMM/yyyy hh:mm tt"), True)
                End If

                clsCommon.AddColumnsForChange(coll, "IS_EMAIL_LAST_DAY_OF_MONTHLY", obj.IS_EMAIL_LAST_DAY_OF_MONTHLY)
                If String.IsNullOrEmpty(clsCommon.myCstr(obj.SCHEDULE_EMAIL_TIME)) = False Then
                    clsCommon.AddColumnsForChange(coll, "SCHEDULE_EMAIL_TIME", clsCommon.GetPrintDate(obj.SCHEDULE_EMAIL_TIME, "dd/MMM/yyyy hh:mm tt"), True)
                End If



                clsCommon.AddColumnsForChange(coll, "IS_SMS_EVERY_DAY", obj.IS_SMS_EVERY_DAY)
                clsCommon.AddColumnsForChange(coll, "IS_SMS_WEEKLY", obj.IS_SMS_WEEKLY)
                clsCommon.AddColumnsForChange(coll, "SMS_WEEKLY_DAY", obj.SMS_WEEKLY_DAY, True)
                clsCommon.AddColumnsForChange(coll, "IS_SMS_MONTHLY", obj.IS_SMS_MONTHLY)

                If String.IsNullOrEmpty(clsCommon.myCstr(obj.MONTHLY_SMS_DATE)) = False Then
                    clsCommon.AddColumnsForChange(coll, "MONTHLY_SMS_DATE", clsCommon.GetPrintDate(obj.MONTHLY_SMS_DATE, "dd/MMM/yyyy hh:mm tt"), True)
                End If

                clsCommon.AddColumnsForChange(coll, "IS_SMS_LAST_DAY_OF_MONTHLY", obj.IS_SMS_LAST_DAY_OF_MONTHLY)
                If String.IsNullOrEmpty(clsCommon.myCstr(obj.SCHEDULE_SMS_TIME)) = False Then
                    clsCommon.AddColumnsForChange(coll, "SCHEDULE_SMS_TIME", clsCommon.GetPrintDate(obj.SCHEDULE_SMS_TIME, "dd/MMM/yyyy hh:mm tt"), True)
                End If


                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                If isNewEntry = True Then
                    obj.SCHEDULER_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.SmsEmailScheduler, "", "")
                    clsCommon.AddColumnsForChange(coll, "SCHEDULER_CODE", obj.SCHEDULER_CODE)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMAIL_SMS_SCHEDULING", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMAIL_SMS_SCHEDULING", OMInsertOrUpdate.Update, "TSPL_EMAIL_SMS_SCHEDULING.SCHEDULER_CODE='" + obj.SCHEDULER_CODE + "'", trans)
                End If
            Next
            Return isSaved
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSendSMSEmailSetting
        Dim obj As clsSendSMSEmailSetting = Nothing
        Dim qry As String = "SELECT TSPL_EMAIL_SMS_SCHEDULING.* FROM TSPL_EMAIL_SMS_SCHEDULING  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EMAIL_SMS_SCHEDULING.SCHEDULER_CODE = (select MIN(SCHEDULER_CODE) from TSPL_EMAIL_SMS_SCHEDULING where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_EMAIL_SMS_SCHEDULING.SCHEDULER_CODE = (select Max(SCHEDULER_CODE) from TSPL_EMAIL_SMS_SCHEDULING where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_EMAIL_SMS_SCHEDULING.SCHEDULER_CODE = (select Min(SCHEDULER_CODE) from TSPL_EMAIL_SMS_SCHEDULING where SCHEDULER_CODE>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_EMAIL_SMS_SCHEDULING.SCHEDULER_CODE = (select Max(SCHEDULER_CODE) from TSPL_EMAIL_SMS_SCHEDULING where SCHEDULER_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_EMAIL_SMS_SCHEDULING.SCHEDULER_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSendSMSEmailSetting()

            'SCHEDULER_CODE,DESCRIPTION,SCREEN_ID,IS_EMAIL_EVERY_DAY,IS_EMAIL_WEEKLY,EMAIL_WEEKLY_DAY,IS_EMAIL_MONTHLY, MONTHLY_EMAIL_DATE, IS_EMAIL_LAST_DAY_OF_MONTHLY
            ', , , SCHEDULE_EMAIL_TIME
            'IS_SMS_EVERY_DAY,IS_SMS_WEEKLY,SMS_WEEKLY_DAY,IS_SMS_MONTHLY,MONTHLY_SMS_DATE,IS_SMS_LAST_DAY_OF_MONTHLY,SCHEDULE_SMS_TIME
            obj.SCHEDULER_CODE = clsCommon.myCstr(dt.Rows(0)("SCHEDULER_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.SCREEN_ID = clsCommon.myCstr(dt.Rows(0)("SCREEN_ID"))

            obj.IS_EMAIL_EVERY_DAY = clsCommon.myCBool(dt.Rows(0)("IS_EMAIL_EVERY_DAY"))
            obj.IS_EMAIL_WEEKLY = clsCommon.myCBool(dt.Rows(0)("IS_EMAIL_WEEKLY"))
            If obj.IS_EMAIL_WEEKLY Then
                obj.EMAIL_WEEKLY_DAY = clsCommon.myCstr(dt.Rows(0)("EMAIL_WEEKLY_DAY"))
            End If
            obj.IS_EMAIL_MONTHLY = clsCommon.myCBool(dt.Rows(0)("IS_EMAIL_MONTHLY"))
            If obj.IS_EMAIL_MONTHLY Then
                obj.IS_EMAIL_LAST_DAY_OF_MONTHLY = clsCommon.myCBool(dt.Rows(0)("IS_EMAIL_LAST_DAY_OF_MONTHLY"))
                If obj.IS_EMAIL_LAST_DAY_OF_MONTHLY = False Then
                    obj.MONTHLY_EMAIL_DATE = clsCommon.myCDate(dt.Rows(0)("MONTHLY_EMAIL_DATE"))
                End If
            End If
            If String.IsNullOrEmpty(clsCommon.myCstr(dt.Rows(0)("SCHEDULE_EMAIL_TIME"))) = False Then
                obj.SCHEDULE_EMAIL_TIME = clsCommon.myCDate(dt.Rows(0)("SCHEDULE_EMAIL_TIME"))
            End If




            obj.IS_SMS_EVERY_DAY = clsCommon.myCBool(dt.Rows(0)("IS_SMS_EVERY_DAY"))
            obj.IS_SMS_WEEKLY = clsCommon.myCBool(dt.Rows(0)("IS_SMS_WEEKLY"))
            If obj.IS_SMS_WEEKLY Then
                obj.SMS_WEEKLY_DAY = clsCommon.myCstr(dt.Rows(0)("SMS_WEEKLY_DAY"))
            End If
            obj.IS_SMS_MONTHLY = clsCommon.myCBool(dt.Rows(0)("IS_SMS_MONTHLY"))
            If obj.IS_SMS_MONTHLY Then
                obj.IS_SMS_LAST_DAY_OF_MONTHLY = clsCommon.myCBool(dt.Rows(0)("IS_SMS_LAST_DAY_OF_MONTHLY"))
                If obj.IS_SMS_LAST_DAY_OF_MONTHLY = False Then
                    obj.MONTHLY_SMS_DATE = clsCommon.myCDate(dt.Rows(0)("MONTHLY_SMS_DATE"))
                End If
            End If
            If String.IsNullOrEmpty(clsCommon.myCstr(dt.Rows(0)("SCHEDULE_SMS_TIME"))) = False Then
                obj.SCHEDULE_SMS_TIME = clsCommon.myCDate(dt.Rows(0)("SCHEDULE_SMS_TIME"))
            End If



        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim qry As String = "delete from TSPL_EMAIL_SMS_SCHEDULING where SCHEDULER_CODE='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_EMAIL_SMS_SCHEDULING where SCHEDULER_CODE='" + strCode + "'"))
    End Function

End Class
