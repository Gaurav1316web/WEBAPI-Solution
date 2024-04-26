Imports common
Imports System.Data.SqlClient
Imports XpertERPEngineFine
Public Class ClsLocation_JobworkItem
#Region "Variables"
    Public Location_Code As String = Nothing
    Public Jobwork_Item As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strLocationCode As String, ByVal Arr As List(Of ClsLocation_JobworkItem), ByVal trans As SqlTransaction) As Boolean
        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_LOCATION_MASTER_Jobwork_Item where Main_Location_COde='" & strLocationCode & "'", trans)
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsLocation_JobworkItem In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Main_Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Jobwork_Item", obj.Jobwork_Item)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCATION_MASTER_Jobwork_Item", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class