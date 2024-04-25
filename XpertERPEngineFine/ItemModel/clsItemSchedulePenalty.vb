Imports System.Data.SqlClient
Public Class clsItemSchedulePenalty
    Public Against_Schedule_PK_Id As String
    Public Penalty_Days As Integer = 0
    Public Penalty As Decimal = 0



    Public Shared Function SaveData(ByVal AgainstSchedulePKId As Integer, ByVal Arr As List(Of clsItemSchedulePenalty), ByVal trans As SqlTransaction) As Boolean
        For Each obj As clsItemSchedulePenalty In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Against_Schedule_PK_Id", AgainstSchedulePKId)
            clsCommon.AddColumnsForChange(coll, "Penalty_Days", obj.Penalty_Days)
            clsCommon.AddColumnsForChange(coll, "Penalty", obj.Penalty)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_SCHEDULE_PENALTY", OMInsertOrUpdate.Insert, "", trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal AgainstSchedulePKId As Integer, ByVal trans As SqlTransaction) As List(Of clsItemSchedulePenalty)
        Dim arr As List(Of clsItemSchedulePenalty) = Nothing
        Dim qry As String = "select TSPL_ITEM_SCHEDULE_PENALTY.* from TSPL_ITEM_SCHEDULE_PENALTY  where TSPL_ITEM_SCHEDULE_PENALTY.Against_Schedule_PK_Id='" + clsCommon.myCstr(AgainstSchedulePKId) + "' order by TSPL_ITEM_SCHEDULE_PENALTY.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsItemSchedulePenalty)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsItemSchedulePenalty
                obj.Against_Schedule_PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("Against_Schedule_PK_Id"))
                obj.Penalty_Days = clsCommon.myCDecimal(dt.Rows(ii)("Penalty_Days"))
                obj.Penalty = clsCommon.myCDecimal(dt.Rows(ii)("Penalty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class