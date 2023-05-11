Imports common
Imports System.Data.SqlClient
Imports System.Collections

Public Class ClsbankGroupMaster
#Region "Variables"
    Public BANK_GROUP_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public CurrentUserCode As String = Nothing

#End Region

    'Public Class clsbankgroupmaster
    '    Public Code As String = Nothing
    '    Public Description As String = Nothing
    '    Public Name As String = Nothing

    '    Public ReadOnly Property SaveData(obj As clsbankgroupmaster, isNewEntry As Boolean, trans As SqlTransaction) As Boolean
    '        Get
    '            Throw New NotImplementedException()
    '        End Get
    '    End Property
    'End Class
    Public Function SaveData(ByVal obj As ClsbankGroupMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            If isNewEntry = True Then
                Dim qry As String = "Delete from TSPL_BANK_GROUP_MASTER where BANK_GROUP_CODE='" + obj.BANK_GROUP_CODE + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            Dim coll As New Hashtable()
            Dim objCommonVar As Object = New ClsbankGroupMaster()
            'clsCommon.AddColumnsForChange(coll, "BANK_GROUP_CODE", obj.BANK_GROUP_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "BANK_GROUP_CODE", obj.BANK_GROUP_CODE)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_GROUP_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_DCS_FOR_SALE", "Code", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_GROUP_MASTER", OMInsertOrUpdate.Update, "BANK_GROUP_CODE='" + obj.BANK_GROUP_CODE + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsbankGroupMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsbankGroupMaster
        Dim obj As ClsbankGroupMaster = Nothing
        Dim qry As String = "SELECT TSPL_BANK_GROUP_MASTER.* FROM TSPL_BANK_GROUP_MASTER where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BANK_GROUP_MASTER.BANK_GROUP_CODE=(select MIN(BANK_GROUP_CODE) from TSPL_BANK_GROUP_MASTER  )"
            Case NavigatorType.Last
                qry += " and TSPL_BANK_GROUP_MASTER.BANK_GROUP_CODE=(select Max(BANK_GROUP_CODE) from TSPL_BANK_GROUP_MASTER  )"
            Case NavigatorType.Next
                qry += " and TSPL_BANK_GROUP_MASTER.BANK_GROUP_CODE=(select Min(BANK_GROUP_CODE) from TSPL_BANK_GROUP_MASTER where BANK_GROUP_CODE > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_BANK_GROUP_MASTER.BANK_GROUP_CODE=(select Max(BANK_GROUP_CODE) from TSPL_BANK_GROUP_MASTER where BANK_GROUP_CODE < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_BANK_GROUP_MASTER.BANK_GROUP_CODE='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsbankGroupMaster()
            obj.BANK_GROUP_CODE = clsCommon.myCstr(dt.Rows(0)("BANK_GROUP_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
        End If

        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Bank Group code not found to Delete")
        End If
        Dim obj As ClsbankGroupMaster = ClsbankGroupMaster.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.BANK_GROUP_CODE) > 0) Then
            Try

                Dim qry As String = "delete from TSPL_BANK_GROUP_MASTER where BANK_GROUP_CODE='" + strCode + "'"
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

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_BANK_GROUP_MASTER.BANK_GROUP_CODE from TSPL_BANK_GROUP_MASTER where TSPL_BANK_GROUP_MASTER.BANK_GROUP_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class

