'-----------Created By Monika 03/07/2014
Imports common
Imports System.Data.SqlClient

Public Class clsfrmNotificationScreen
#Region "Variables"
    Public modulecode As String = Nothing
    Public modulename As String = Nothing
    Public doctype As String = Nothing
    Public scrncode As String = Nothing
    Public scrnname As String = Nothing
    Public sno As Integer = Nothing
    Public status As String = Nothing
    Public criteria As String = Nothing
    Public startdate As Date = Nothing
    Public msg As String = Nothing
    Public noofuser As Integer = Nothing

    Public user_sno As Integer = Nothing
    Public user_code As String = Nothing
    Public user_name As String = Nothing

    Public criteriavalue As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsfrmNotificationScreen, ByVal arr As List(Of clsfrmNotificationScreen), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            If clsCommon.myLen(obj.modulecode) > 0 Then
                qry = "delete from TSPL_SCREEN_REMAINDER_SETTING where module_code='" + obj.modulecode + "' and screen_type='" + obj.doctype + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SCREEN_REMAINDER_USERS where module_code='" + obj.modulecode + "' and screen_type='" + obj.doctype + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SCREEN_REMAINDER_CRITERIA where module_code='" + obj.modulecode + "' and screen_type='" + obj.doctype + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            '-------------Parent Grid-----------------------------
            For Each objtr As clsfrmNotificationScreen In arr
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "module_code", obj.modulecode)
                clsCommon.AddColumnsForChange(coll, "screen_type", obj.doctype)
                clsCommon.AddColumnsForChange(coll, "sno", objtr.sno)
                clsCommon.AddColumnsForChange(coll, "screen_code", objtr.scrncode)
                clsCommon.AddColumnsForChange(coll, "status", objtr.status)
                clsCommon.AddColumnsForChange(coll, "criteria", objtr.criteria)
                clsCommon.AddColumnsForChange(coll, "startdate", clsCommon.GetPrintDate(objtr.startdate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "notify_message", objtr.msg)
                clsCommon.AddColumnsForChange(coll, "no_of_users", objtr.noofuser)
                clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                'If isnewentry = True Then
                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCREEN_REMAINDER_SETTING", OMInsertOrUpdate.Insert, "", trans)
                'Else
                'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCREEN_REMAINDER_SETTING", OMInsertOrUpdate.Update, " module_code='" + obj.modulecode + "' and screen_type='" + obj.doctype + "'", trans)
                'End If

                '--------update in display table ---------------------
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Message", objtr.msg)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISPLAY_NOTIFICATIONS", OMInsertOrUpdate.Update, " Module_Code='" + obj.modulecode + "' and Screen_Code='" + objtr.scrncode + "'", trans)
            Next

            '--------------Saved User Detail/Criteria Here From temporary Table----------------------
            qry = "Insert into TSPL_SCREEN_REMAINDER_USERS select * from Temp_User_Notification"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Insert into TSPL_SCREEN_REMAINDER_CRITERIA select * from Temp_Criteria_Notification"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '---------------------------------------------------

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveChildGrid(ByVal mname As String, ByVal doctype As String, ByVal userscrncode As String, ByVal arr1 As List(Of clsfrmNotificationScreen), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim issaved As Boolean = True
            If clsCommon.myLen(mname) > 0 Then
                qry = "delete from Temp_User_Notification where module_code='" + mname + "' and screen_type='" + doctype + "' and screen_code='" + userscrncode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            Dim coll As New Hashtable()
            '-------------Child Grid-------------------------------------
            For Each objtr1 As clsfrmNotificationScreen In arr1
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "module_code", objtr1.modulecode)
                clsCommon.AddColumnsForChange(coll, "screen_type", objtr1.doctype)
                clsCommon.AddColumnsForChange(coll, "sno", objtr1.user_sno)
                clsCommon.AddColumnsForChange(coll, "screen_code", objtr1.scrncode)
                clsCommon.AddColumnsForChange(coll, "user_code", objtr1.user_code)
                clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Temp_User_Notification", OMInsertOrUpdate.Insert, "", trans)
            Next

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveCriteriaGrid(ByVal mname As String, ByVal doctype As String, ByVal userscrncode As String, ByVal arr1 As List(Of clsfrmNotificationScreen), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim issaved As Boolean = True
            If clsCommon.myLen(mname) > 0 Then
                qry = "delete from Temp_Criteria_Notification where module_code='" + mname + "' and screen_type='" + doctype + "' and screen_code='" + userscrncode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            Dim coll As New Hashtable()
            '-------------Child Grid-------------------------------------
            For Each objtr1 As clsfrmNotificationScreen In arr1
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "module_code", objtr1.modulecode)
                clsCommon.AddColumnsForChange(coll, "screen_type", objtr1.doctype)
                clsCommon.AddColumnsForChange(coll, "screen_code", objtr1.scrncode)
                clsCommon.AddColumnsForChange(coll, "Criteria", objtr1.criteriavalue)
                clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Temp_Criteria_Notification", OMInsertOrUpdate.Insert, "", trans)
            Next

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal ModuleCode As String, ByVal Doctype As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_SCREEN_REMAINDER_SETTING where module_code='" + ModuleCode + "' and screen_type='" + Doctype + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SCREEN_REMAINDER_USERS where module_code='" + ModuleCode + "' and screen_type='" + Doctype + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SCREEN_REMAINDER_CRITERIA where module_code='" + ModuleCode + "' and screen_type='" + Doctype + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

#Region "Notification Save/Delete For Displaying"
    Public Shared Function DisplayNotification(ByVal ModuleCode As String, ByVal formid As String, ByVal Start_Date As Date, ByVal Criteria As String, ByVal Doc_Caption As String, ByVal itemvalues As String) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_DISPLAY_NOTIFICATIONS where doc_id='" + Doc_Caption + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "insert into TSPL_DISPLAY_NOTIFICATIONS select TSPL_SCREEN_REMAINDER_SETTING.Notify_Message,TSPL_SCREEN_REMAINDER_USERS.User_code,'" + Doc_Caption + "' as doc_id,'0' as Status,cast('" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt") + "' as varchar) as Snooze_Time,'" + itemvalues + "' as item_name,'" + ModuleCode + "' as Module_Code,'" + formid + "' as Screen_Code from TSPL_SCREEN_REMAINDER_SETTING left outer join TSPL_SCREEN_REMAINDER_USERS on TSPL_SCREEN_REMAINDER_USERS.Module_Code=TSPL_SCREEN_REMAINDER_SETTING.Module_Code and TSPL_SCREEN_REMAINDER_USERS.Screen_Code=TSPL_SCREEN_REMAINDER_SETTING.Screen_Code and TSPL_SCREEN_REMAINDER_USERS.Screen_Type=TSPL_SCREEN_REMAINDER_SETTING.Screen_Type left outer join TSPL_SCREEN_REMAINDER_Criteria on TSPL_SCREEN_REMAINDER_Criteria.module_code=TSPL_SCREEN_REMAINDER_SETTING.module_code and TSPL_SCREEN_REMAINDER_Criteria.screen_type=TSPL_SCREEN_REMAINDER_SETTING.screen_type and TSPL_SCREEN_REMAINDER_Criteria.screen_code=TSPL_SCREEN_REMAINDER_SETTING.screen_code where isnull(TSPL_SCREEN_REMAINDER_USERS.User_code,'')<>'' and TSPL_SCREEN_REMAINDER_SETTING.Module_Code='" + ModuleCode + "' and TSPL_SCREEN_REMAINDER_SETTING.Screen_Code='" + formid + "' and TSPL_SCREEN_REMAINDER_SETTING.StartDate<='" + clsCommon.GetPrintDate(Start_Date, "dd/MMM/yyyy") + "' and TSPL_SCREEN_REMAINDER_SETTING.Status='YES' and TSPL_SCREEN_REMAINDER_Criteria.Criteria='" + Criteria + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            Return True
        Catch ex As Exception
        End Try
    End Function

    Public Shared Function NotificationDelete(ByVal docid As String) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_DISPLAY_NOTIFICATIONS where doc_id='" + docid + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            Return True
        Catch ex As Exception
        End Try
    End Function

    Public Shared Function Snooze(ByVal strCode As String) As Boolean
        Dim qry As String = "update TSPL_DISPLAY_NOTIFICATIONS "
        qry += " set Snooze_Time=DATEADD(minute,2,getdate()) "
        qry += " where 'Trans_Id : '+doc_id+' Notification : '+message+'(Detail :'+item_name+ ')'='" + strCode + "' and user_code='" + objCommonVar.CurrentUserCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        Return True
    End Function

    Public Shared Function DontShowAgain(ByVal strCode As String) As Boolean
        Dim qry As String = "update TSPL_DISPLAY_NOTIFICATIONS "
        qry += " set Status=1 "
        qry += " where 'Trans_Id : '+doc_id+' Notification : '+message+'(Detail :'+item_name+ ')'='" + strCode + "' and user_code='" + objCommonVar.CurrentUserCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        Return True
    End Function
#End Region
End Class
