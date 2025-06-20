Imports System.Data.SqlClient
Public Class clsItemSchedule

    Public Item_Code As String
    Public SNo As Integer = 0
    Public Days As Integer = 0
    Public Qty_Per As Integer = 0
    Public Short_Per As Integer = 0
    Public Late_Days As Integer = 0
    Public Quantity As Integer = 0
    Public Arr As List(Of clsItemSchedulePenalty) = Nothing


    Public Shared Function SaveData(ByVal strICode As String, ByVal Arr As List(Of clsItemSchedule), ByVal trans As SqlTransaction) As Boolean
        For Each obj As clsItemSchedule In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
            clsCommon.AddColumnsForChange(coll, "Days", obj.Days)
            clsCommon.AddColumnsForChange(coll, "Qty_Per", obj.Qty_Per)
            clsCommon.AddColumnsForChange(coll, "Short_Per", obj.Short_Per)
            clsCommon.AddColumnsForChange(coll, "Late_Days", obj.Late_Days)
            clsCommon.AddColumnsForChange(coll, "Quantity", obj.Quantity)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_SCHEDULE", OMInsertOrUpdate.Insert, "", trans)
            Dim PK As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select MAX(PK_ID) from TSPL_ITEM_SCHEDULE where Item_Code='" + strICode + "'", trans))
            clsItemSchedulePenalty.SaveData(PK, obj.Arr, trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal Item_Code As String, ByVal trans As SqlTransaction) As List(Of clsItemSchedule)
        Dim arr As List(Of clsItemSchedule) = Nothing
        Dim qry As String = "select TSPL_ITEM_SCHEDULE.* from TSPL_ITEM_SCHEDULE  where TSPL_ITEM_SCHEDULE.Item_Code='" + Item_Code + "' order by TSPL_ITEM_SCHEDULE.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsItemSchedule)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsItemSchedule
                obj.SNo = ii + 1
                obj.Item_Code = clsCommon.myCDecimal(dt.Rows(ii)("Item_Code"))
                obj.Days = clsCommon.myCDecimal(dt.Rows(ii)("Days"))
                obj.Qty_Per = clsCommon.myCDecimal(dt.Rows(ii)("Qty_Per"))
                obj.Short_Per = clsCommon.myCDecimal(dt.Rows(ii)("Short_Per"))
                obj.Late_Days = clsCommon.myCDecimal(dt.Rows(ii)("Late_Days"))
                obj.Quantity = clsCommon.myCDecimal(dt.Rows(ii)("Quantity"))
                obj.Arr = clsItemSchedulePenalty.GetData(clsCommon.myCDecimal(dt.Rows(ii)("PK_ID")), trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class

Public Class clsItemNOCSchedule

    Public Item_Code As String
    Public SNo As Integer = 0
    Public Days As Integer = 0
    Public Qty_Per As Integer = 0
    Public Short_Per As Integer = 0
    Public Late_Days As Integer = 0
    Public Arr As List(Of clsItemNOCSchedulePenalty) = Nothing


    Public Shared Function SaveData(ByVal strICode As String, ByVal Arr As List(Of clsItemNOCSchedule), ByVal trans As SqlTransaction) As Boolean
        For Each obj As clsItemNOCSchedule In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
            clsCommon.AddColumnsForChange(coll, "Days", obj.Days)
            clsCommon.AddColumnsForChange(coll, "Qty_Per", obj.Qty_Per)
            clsCommon.AddColumnsForChange(coll, "Short_Per", obj.Short_Per)
            clsCommon.AddColumnsForChange(coll, "Late_Days", obj.Late_Days)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_NOC_SCHEDULE", OMInsertOrUpdate.Insert, "", trans)
            Dim PK As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select MAX(PK_ID) from TSPL_ITEM_NOC_SCHEDULE where Item_Code='" + strICode + "'", trans))
            clsItemNOCSchedulePenalty.SaveData(PK, obj.Arr, trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal Item_Code As String, ByVal trans As SqlTransaction) As List(Of clsItemNOCSchedule)
        Dim arr As List(Of clsItemNOCSchedule) = Nothing
        Dim qry As String = "select TSPL_ITEM_NOC_SCHEDULE.* from TSPL_ITEM_NOC_SCHEDULE  where TSPL_ITEM_NOC_SCHEDULE.Item_Code='" + Item_Code + "' order by TSPL_ITEM_NOC_SCHEDULE.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsItemNOCSchedule)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsItemNOCSchedule
                obj.SNo = ii + 1
                obj.Item_Code = clsCommon.myCDecimal(dt.Rows(ii)("Item_Code"))
                obj.Days = clsCommon.myCDecimal(dt.Rows(ii)("Days"))
                obj.Qty_Per = clsCommon.myCDecimal(dt.Rows(ii)("Qty_Per"))
                obj.Short_Per = clsCommon.myCDecimal(dt.Rows(ii)("Short_Per"))
                obj.Late_Days = clsCommon.myCDecimal(dt.Rows(ii)("Late_Days"))
                obj.Arr = clsItemNOCSchedulePenalty.GetData(clsCommon.myCDecimal(dt.Rows(ii)("PK_ID")), trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class

Public Class clsItemSlabTolerance
    Public SNo As Integer = 0
    Public Item_Code As String
    Public From_Range As Double = 0
    Public To_Range As Double = 0
    Public Qty As Integer = 0
    Public Arr As List(Of clsItemSlabTolerance) = Nothing

    Public Shared Function SaveData(ByVal strICode As String, ByVal Arr As List(Of clsItemSlabTolerance), ByVal trans As SqlTransaction) As Boolean
        Dim strQry As String = "delete from TSPL_ITEM_SLAB_TOLERANCE Where Item_Code='" + strICode + "'"
        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
        For Each obj As clsItemSlabTolerance In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
            clsCommon.AddColumnsForChange(coll, "Range_From", obj.From_Range)
            clsCommon.AddColumnsForChange(coll, "Range_To", obj.To_Range)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_SLAB_TOLERANCE", OMInsertOrUpdate.Insert, "", trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal Item_Code As String, ByVal trans As SqlTransaction) As List(Of clsItemSlabTolerance)
        Dim arr As List(Of clsItemSlabTolerance) = Nothing
        Dim qry As String = "select TSPL_ITEM_SLAB_TOLERANCE.* from TSPL_ITEM_SLAB_TOLERANCE  where TSPL_ITEM_SLAB_TOLERANCE.Item_Code='" + Item_Code + "' order by TSPL_ITEM_SLAB_TOLERANCE.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsItemSlabTolerance)
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsItemSlabTolerance
                obj.SNo = ii + 1
                obj.Item_Code = clsCommon.myCDecimal(dt.Rows(ii)("Item_Code"))
                obj.From_Range = clsCommon.myCDecimal(dt.Rows(ii)("Range_From"))
                obj.To_Range = clsCommon.myCDecimal(dt.Rows(ii)("Range_To"))
                obj.Qty = clsCommon.myCDecimal(dt.Rows(ii)("Qty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class