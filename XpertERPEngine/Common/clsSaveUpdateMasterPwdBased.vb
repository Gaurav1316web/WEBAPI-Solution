Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsSaveUpdateMasterPwdBased

#Region "variables"
    'Public Formid As String = Nothing
    'Public strdocno As String = Nothing
    'Public strremarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal Formid As String, ByVal strdocno As String, ByVal strremarks As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(Formid, strdocno, strremarks, trans) Then
                trans.Commit()
                isSaved = True
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal Formid As String, ByVal strdocno As String, ByVal strremarks As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_No", strdocno)
            clsCommon.AddColumnsForChange(coll, "Form_Id", Formid, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", strremarks)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode, True)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MASTER_SAVE_UPDATE_REMARKS", OMInsertOrUpdate.Insert, "", trans)
            '--------------------------------------------------------------------------------------------------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
