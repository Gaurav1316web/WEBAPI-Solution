Imports System.Data.SqlClient
Public Class clsBullSubStatus
    Public Code As String = Nothing
    Public Name As String = Nothing

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullSubStatus
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsBullSubStatus = GetData(strCode, NavType, trans)
            trans.Commit()

            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BULL_STATUS_MASTER where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBullSubStatus
        Dim obj As clsBullSubStatus = Nothing

        Try
            Dim strQry As String = "select * from TSPL_BULL_SUB_STATUS_MASTER where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Code = (select MIN(Code) from TSPL_BULL_SUB_STATUS_MASTER where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Code = (Select Max(Code) from TSPL_BULL_SUB_STATUS_MASTER where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Code = (Select Min(Code) from TSPL_BULL_SUB_STATUS_MASTER where Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Code = (select Max(Code) from TSPL_BULL_SUB_STATUS_MASTER where Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBullSubStatus()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function


    Public Function SaveData(ByVal obj As clsBullSubStatus, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsBullSubStatus, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_BULL_SUB_STATUS_MASTER where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullBreedMaster, "", objCommonVar.strCurrUserLocations)
                'If clsCommon.myLen(obj.Code) <= 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_SUB_STATUS_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_SUB_STATUS_MASTER", OMInsertOrUpdate.Update, "TSPL_BULL_SUB_STATUS_MASTER.Code='" + obj.Code + "'", trans)
            End If

            'IsSaved = IsSaved AndAlso clsNotificationDetails.SaveData(obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
End Class