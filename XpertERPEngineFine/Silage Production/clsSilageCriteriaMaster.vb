'=== Document Created by Kunal - 02 - 08 - 2016
Imports common
Imports System.Data.SqlClient

Public Class clsSilageCriteriaMaster

#Region "TSPL_SILAGE_CRITERIA_MASTER : Variables"
    Public Criteria_Code As String = Nothing
    Public Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsSilageCriteriaMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlClient.SqlTransaction = clsDBFuncationality.GetTransactin

        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsSilageCriteriaMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim qry As String = Nothing
        Dim tableName As String = "TSPL_SILAGE_CRITERIA_MASTER"

        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Criteria_Code", obj.Criteria_Code)
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            End If
            If isNewEntry Then
                clsCommonFunctionality.UpdateDataTable(coll, tableName, OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, tableName, OMInsertOrUpdate.Update, "TSPL_SILAGE_CRITERIA_MASTER.Criteria_Code ='" & obj.Criteria_Code & "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSilageCriteriaMaster
        Try
            Return GetData(strCode, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSilageCriteriaMaster

        Dim obj As clsSilageCriteriaMaster = Nothing
        Dim table As String = "TSPL_SILAGE_CRITERIA_MASTER"
        Dim column As String = "Criteria_Code"
        Dim qry As String = Nothing

        Try
            qry = "SELECT * FROM " & table & " where 2=2 "
            Select Case NavType
                Case NavigatorType.First
                    qry += " and " & table & "." & column & "  = (select MIN(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Last
                    qry += " and " & table & "." & column & "  = (select Max(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Next
                    qry += " and " & table & "." & column & "  = (select Min(" & column & ") from " & table & " where " & column & ">'" & strCode & "' )"
                Case NavigatorType.Previous
                    qry += " and " & table & "." & column & "  = (select Max(" & column & ") from " & table & " where " & column & "<'" & strCode & "' )"
                Case NavigatorType.Current
                    qry += " and " & table & "." & column & "  = '" + strCode + "'"
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsSilageCriteriaMaster()
                obj.Criteria_Code = clsCommon.myCstr(dt.Rows(0)("Criteria_Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
                obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
                obj.Modified_Date = clsCommon.myCstr(dt.Rows(0)("Modified_Date"))
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim table As String = "TSPL_SILAGE_CRITERIA_MASTER"
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsSilageCriteriaMaster = Nothing

        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If

        obj = New clsSilageCriteriaMaster()
        obj = clsSilageCriteriaMaster.GetData(strCode, NavigatorType.Current, trans)

        Try
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Criteria_Code) > 0) Then
                Dim qry As String = "Delete from " & table & " where Criteria_Code = '" + strCode + "'"
                If (clsDBFuncationality.ExecuteNonQuery(qry, trans)) Then
                    trans.Commit()
                    Return True
                Else
                    trans.Rollback()
                    Return False
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

        Return True
    End Function
    Public Shared Function ListCriteriaMaster() As DataTable

        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing

        Try
            qry = "SELECT * FROM TSPL_SILAGE_CRITERIA_MASTER"
            dt = clsDBFuncationality.GetDataTable(qry, Nothing)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return dt
    End Function

End Class



