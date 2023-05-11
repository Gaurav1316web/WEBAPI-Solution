Imports common
Imports System.Data.SqlClient

Public Class clsLocationDistanceMapping
#Region "Variables"
    Public Location_Code As String = Nothing
    Public Customer_Code As String = Nothing
    Public Distance As Integer
    Public comp_code As String = Nothing
    Public TransType As String = Nothing
#End Region

    Public Function SaveData(ByVal LocationCode As String, ByVal LocationDesc As String, ByVal strTransType As String, ByVal company As String, ByVal Arr As List(Of clsLocationDistanceMapping)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_LOCATION_DISTANCE_MAPPING where Location_Code='" + LocationCode + "' and TransType='" & strTransType & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            For Each obj As clsLocationDistanceMapping In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Location_Code", LocationCode)
                clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
                clsCommon.AddColumnsForChange(coll, "Distance", obj.Distance)
                clsCommon.AddColumnsForChange(coll, "comp_code", company)
                clsCommon.AddColumnsForChange(coll, "TransType", obj.TransType)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCATION_DISTANCE_MAPPING", OMInsertOrUpdate.Insert, "", trans)
            Next

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal strTransType As String) As List(Of clsLocationDistanceMapping)
        Dim obj As clsLocationDistanceMapping = Nothing
        Dim qry As String = "select Customer_Code ,Distance ,TransType  from TSPL_LOCATION_DISTANCE_MAPPING WHERE Location_Code = '" + strCode + "' and transtype='" & strTransType & "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of clsLocationDistanceMapping)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As clsLocationDistanceMapping
            For Each dr As DataRow In dt.Rows
                objTr = New clsLocationDistanceMapping()
                objTr.Customer_Code = clsCommon.myCstr(dr("Customer_Code"))
                objTr.Distance = clsCommon.myCstr(dr("Distance"))
                objTr.TransType = clsCommon.myCstr(dr("TransType"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function DeleteData(ByVal strLocationCode As String) As Boolean
        Try
            If clsCommon.myLen(strLocationCode) > 0 Then
                Dim qry As String = ""
                qry = "delete from TSPL_LOCATION_DISTANCE_MAPPING  Where Location_Code='" + strLocationCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


End Class


















