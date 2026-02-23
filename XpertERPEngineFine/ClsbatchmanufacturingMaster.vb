Imports common
Imports System.Data.SqlClient
Imports System.Collections

Public Class ClsbatchmanufacturingMaster
    Public CODE As String = Nothing
    Public Name As String = Nothing

    Public Function SaveData(ByVal obj As ClsbatchmanufacturingMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If isNewEntry = True Then
                Dim qry As String = "Delete from TSPL_BATCH_MANUFECTURING_MASTER where Code='" + obj.CODE + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            Dim coll As New Hashtable()
            Dim objCommonVar As Object = New ClsbatchmanufacturingMaster()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BATCH_MANUFECTURING_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BATCH_MANUFECTURING_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.CODE + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsbatchmanufacturingMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsbatchmanufacturingMaster
        Dim obj As ClsbatchmanufacturingMaster = Nothing
        Dim qry As String = "SELECT TSPL_BATCH_MANUFECTURING_MASTER.* FROM TSPL_BATCH_MANUFECTURING_MASTER where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BATCH_MANUFECTURING_MASTER.CODE=(select MIN(Code) from TSPL_BATCH_MANUFECTURING_MASTER  )"
            Case NavigatorType.Last
                qry += " and TSPL_BATCH_MANUFECTURING_MASTER.CODE=(select Max(Code) from TSPL_BATCH_MANUFECTURING_MASTER  )"
            Case NavigatorType.Next
                qry += " and TSPL_BATCH_MANUFECTURING_MASTER.CODE=(select Min(Code) from TSPL_BATCH_MANUFECTURING_MASTER where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_BATCH_MANUFECTURING_MASTER.CODE=(select Max(Code) from TSPL_BATCH_MANUFECTURING_MASTER where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_BATCH_MANUFECTURING_MASTER.CODE='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsbatchmanufacturingMaster()
            obj.CODE = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
        End If
        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Bank Group code not found to Delete")
        End If
        Dim obj As ClsbatchmanufacturingMaster = ClsbatchmanufacturingMaster.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.CODE) > 0) Then
            Try
                '  clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_BATCH_MANUFECTURING_MASTER", "BANK_GROUP_CODE", trans)
                ' clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_BATCH_MANUFECTURING_MASTER", "BANK_GROUP_CODE", trans)

                Dim qry As String = "delete from TSPL_BATCH_MANUFECTURING_MASTER where Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

End Class
