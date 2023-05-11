Imports common
Imports System.Data.SqlClient
Public Class clsLocationDistanceMaster
#Region "Variables"
    Public From_Location_code As String = Nothing
    Public to_Location_Code As String = Nothing
    Public Distance As Decimal = Nothing
    Public Toll_Amt As Decimal = Nothing

#End Region

    Public Shared Function SaveData(ByVal arr As List(Of clsLocationDistanceMaster), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                Dim qry As String = "delete from tspl_location_distance_master "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For Each obj As clsLocationDistanceMaster In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "From_Location_code", obj.From_Location_code)
                    clsCommon.AddColumnsForChange(coll, "to_Location_Code", obj.to_Location_Code)
                    clsCommon.AddColumnsForChange(coll, "Distance", clsCommon.myCdbl(obj.Distance))
                    clsCommon.AddColumnsForChange(coll, "Toll_Amt", clsCommon.myCdbl(obj.Toll_Amt))
                    clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy")))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_location_distance_master", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If


            Return True
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function getDistance(ByVal fromLocation As String, ByVal toLocation As String, ByVal trans As SqlTransaction) As Double
        Dim distance As Double = 0
        Dim qry As String = "  select distance from tspl_location_distance_master where from_location_code='" & fromLocation & "' and to_location_code='" & toLocation & "'"
        distance = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return distance
    End Function
End Class

