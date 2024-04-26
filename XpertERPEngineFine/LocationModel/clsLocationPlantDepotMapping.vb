Imports common
Imports System.Data.SqlClient
Imports XpertERPEngineFine
Public Class clsLocationPlantDepotMapping

#Region "Variables"
    Public Plant_Location_Code As String = String.Empty
    Public Depot_Location_Code As String = String.Empty

#End Region

    Public Shared Function SaveData(ByVal obj As clsLocationPlantDepotMapping) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Plant_Location_Code", obj.Plant_Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Depot_Location_Code", obj.Depot_Location_Code, True)
            isSaved = clsCommonFunctionality.UpdateDataTable(coll, "tspl_location_plantdepot_detail", OMInsertOrUpdate.Insert, "", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsLocation, ByVal Arr As List(Of clsLocationPlantDepotMapping), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim strqry As String = "Delete from tspl_location_plantdepot_detail where Plant_Location_Code='" & obj.Location_Code & "' "
            clsDBFuncationality.getSingleValue(strqry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj1 As clsLocationPlantDepotMapping In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Plant_Location_Code", obj1.Plant_Location_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Depot_Location_Code", obj1.Depot_Location_Code, True)
                    isSaved = clsCommonFunctionality.UpdateDataTable(coll, "tspl_location_plantdepot_detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


End Class