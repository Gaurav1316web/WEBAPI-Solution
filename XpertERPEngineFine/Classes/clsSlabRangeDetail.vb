Imports common
Imports System.Data.SqlClient
Public Class clsSlabRangeDetail
    Public Form_ID As String = String.Empty
    Public Trans_ID As String = String.Empty
    Public Slab_Upto As Double = 0
    Public Slab_Rate As Double = 0
    Public Shared Function SaveData(ByVal arr As List(Of clsSlabRangeDetail), ByVal Trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                deleteData(arr.Item(0).Form_ID, arr.Item(0).Trans_ID, Trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Form_ID", arr.Item(i).Form_ID)
                    clsCommon.AddColumnsForChange(coll, "Trans_ID", arr.Item(i).Trans_ID)
                    clsCommon.AddColumnsForChange(coll, "Slab_Upto", arr.Item(i).Slab_Upto)
                    clsCommon.AddColumnsForChange(coll, "Slab_Rate", arr.Item(i).Slab_Rate)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_slab_range_detail", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    '==================================Create by Preeti Gupta=================
    Public Shared Function SaveBulkData(ByVal arrDistinct As List(Of clsSlabRangeDetail), ByVal arr As List(Of clsSlabRangeDetail), ByVal Trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            If arrDistinct IsNot Nothing AndAlso arrDistinct.Count > 0 Then
                For Each Str As clsSlabRangeDetail In arrDistinct
                    deleteData(Str.Form_ID, Str.Trans_ID, Trans)
                Next


                Dim i As Integer = 0
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For i = 0 To arr.Count - 1
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Form_ID", arr.Item(i).Form_ID)
                        clsCommon.AddColumnsForChange(coll, "Trans_ID", arr.Item(i).Trans_ID)
                        clsCommon.AddColumnsForChange(coll, "Slab_Upto", arr.Item(i).Slab_Upto)
                        clsCommon.AddColumnsForChange(coll, "Slab_Rate", arr.Item(i).Slab_Rate)
                        issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_slab_range_detail", OMInsertOrUpdate.Insert, "", Trans)
                    Next
                End If
            End If ''main cond.

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function getData(ByVal pcode As String, ByVal tcode As String) As List(Of clsSlabRangeDetail)
        Dim arr As New List(Of clsSlabRangeDetail)
        Try
            'Dim arr As New List(Of clsSlabRangeDetail)
            Dim obj As New clsSlabRangeDetail
            Dim q As String = "select * from tspl_slab_range_detail where form_id='" & pcode & "' and Trans_id='" & tcode & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsSlabRangeDetail
                    obj.Form_ID = clsCommon.myCstr(dtbl.Rows(i)("Form_ID"))
                    obj.Trans_ID = clsCommon.myCstr(dtbl.Rows(i)("Trans_ID"))
                    obj.Slab_Upto = clsCommon.myCdbl(dtbl.Rows(i)("Slab_Upto"))
                    obj.Slab_Rate = clsCommon.myCdbl(dtbl.Rows(i)("Slab_Rate"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal tcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from tspl_slab_range_detail where form_id='" & pcode & "' and trans_id='" & tcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

End Class
