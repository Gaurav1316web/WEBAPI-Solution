Imports common
Imports System.Data.SqlClient
Imports XpertERPEngineFine
Public Class clsLocationItemMapping
#Region "Variables"
    Public Item_Code As String = String.Empty
    Public Item_Name As String = String.Empty
    Public Location_Code As String = String.Empty
    Public Location_Name As String = String.Empty
    Public SequenceNo As Integer = 0


#End Region
    Public Shared Function SaveData(ByVal obj As clsLocation, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, obj.ArrLocItemMap, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsLocation, ByVal Arr As List(Of clsLocationItemMapping), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim strqry As String = "Delete from TSPL_Location_ItemMAPPING where Location_Code='" & obj.Location_Code & "' "
            clsDBFuncationality.getSingleValue(strqry, trans)


            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then

                For Each obj1 As clsLocationItemMapping In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj1.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Name", obj1.Item_Name)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                    clsCommon.AddColumnsForChange(coll, "Location_Name", obj1.Location_Name)
                    clsCommon.AddColumnsForChange(coll, "SequenceNo", obj1.SequenceNo)
                    isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Location_ItemMAPPING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


End Class