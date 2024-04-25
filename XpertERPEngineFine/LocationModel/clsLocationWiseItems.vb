Imports common
Imports System.Data.SqlClient
Imports XpertERPEngineFine
Public Class clsLocationWiseItems
#Region "Variables"
    Public Location_Code As String = String.Empty
    Public Item_code As String = String.Empty
    Public Item_desc As String = String.Empty
    Public Item_Type As String = String.Empty '' S-Sale, p-Purchase
    Public Item_Category As String = String.Empty  ''L-Local,I-InterState
#End Region

    Public Shared Function SaveData(ByVal strLocationCode As String, ByVal Arr As List(Of clsLocationWiseItems), ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsLocationWiseItems In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Location_Code", strLocationCode)
                clsCommon.AddColumnsForChange(coll, "Item_code", obj.Item_code)
                clsCommon.AddColumnsForChange(coll, "Item_desc", obj.Item_desc)
                clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Category", obj.Item_Category)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCATION_WISE_ITEM_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class