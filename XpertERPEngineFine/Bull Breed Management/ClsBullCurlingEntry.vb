Imports System.Data.SqlClient

Public Class ClsBullCurlingEntry

    Public Code As String = ""
    Public Remarks As String = ""
    Public Doc_Date As DateTime = Nothing
    Public Arr As List(Of ClsBullCurlingEntryDeatil) = Nothing


    Public Shared Function SaveData(ByVal obj As ClsBullCurlingEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As ClsBullCurlingEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_BULL_CURLING where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Date", obj.Doc_Date)


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullParameterGroup, "", objCommonVar.strCurrUserLocations)
                'If clsCommon.myLen(obj.Code) <= 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CURLING", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CURLING", OMInsertOrUpdate.Update, "TSPL_BULL_CURLING.Code='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso ClsBullCurlingEntryDeatil.SaveData(clsCommon.myCstr(obj.Code), obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = DeleteData(strCode, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(StrCode) <= 0) Then
                Throw New Exception("Document_No not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_BULL_CURLING_Detail where Document_No='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BULL_CURLING where Document_No='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document_No not found to Post")
            End If
            Dim obj As clsDistributorRouteTagging = clsDistributorRouteTagging.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Document_No : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Post_Date)
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CURLING", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Code) + "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class ClsBullCurlingEntryDeatil
    Public Document_No As String = ""
    Public BullID As String = ""
    Public Amount As Double = 0


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsBullCurlingEntryDeatil), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As ClsBullCurlingEntryDeatil In Arr
                    Dim colm As New Hashtable()
                    clsCommon.AddColumnsForChange(colm, "BullID", strDocNo)
                    clsCommon.AddColumnsForChange(colm, "BullID", obj.BullID)
                    clsCommon.AddColumnsForChange(colm, "Amount", obj.Amount)


                    clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_CURLING_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

    End Function
End Class

