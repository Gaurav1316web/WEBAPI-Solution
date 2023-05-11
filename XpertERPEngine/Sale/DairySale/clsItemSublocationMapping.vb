Imports common
Imports System.Data.SqlClient
Public Class clsItemSublocationMapping

#Region "Variable"
    Public Location_Code As String = Nothing
    Public Arr As List(Of clsItemSublocationMappingDetail) = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsItemSublocationMapping, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsItemSublocationMapping) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsItemSublocationMapping, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            Dim isSaved As Boolean = True
            qry = "delete from TSPL_Item_Sublocation_Mapping where Main_Location_Code ='" + obj.Location_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsItemSublocationMappingDetail.SaveData(obj.Location_Code, Arr, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
    End Function

End Class
Public Class clsItemSublocationMappingDetail
#Region "Variable"
    Public TR_Code As String = Nothing
    Public Main_Location_Code As String = String.Empty
    Public Item_code As String = String.Empty
    Public Item_Name As String = String.Empty
    Public Item_ShortName As String = String.Empty
    Public Sub_Location_Code As String = String.Empty
    Public Sub_Location_Name As String = String.Empty
#End Region

    Public Shared Function SaveData(ByVal strlocationCode As String, ByVal Arr As List(Of clsItemSublocationMappingDetail), ByVal trans As SqlTransaction) As Boolean
        Dim LineNo As Integer = 0
        Dim arrRepeat As New List(Of String)
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsItemSublocationMappingDetail In Arr
                If arrRepeat.Contains(obj.Item_code) Then
                    LineNo += 1
                Else
                    arrRepeat.Add(obj.Item_code)
                    LineNo = 0
                    LineNo += 1
                End If
                Dim coll As New Hashtable()
                Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")), clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", strTRCode)
                clsCommon.AddColumnsForChange(coll, "Main_Location_Code", strlocationCode)
                'clsCommon.AddColumnsForChange(coll, "Line_No", LineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_code)
                clsCommon.AddColumnsForChange(coll, "Sub_Location_Code", obj.Sub_Location_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Sublocation_Mapping", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
        Arr = Nothing
    End Function

    'Public Shared Function getData(ByVal strLocationCode As String, ByVal trans As SqlTransaction) As List(Of clsItemSublocationMappingDetail)
    '    Try
    '        Dim arrObj As List(Of clsItemSublocationMappingDetail) = Nothing
    '        Dim obj As clsItemSublocationMappingDetail = Nothing
    '        Dim qry As String = "  select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description , TSPL_Item_Sublocation_Mapping.Sub_Location_Code , TSPL_LOCATION_MASTER.Location_Desc as Sub_Location_Name from tspl_item_master
    '                           left outer join (select TSPL_Item_Sublocation_Mapping.Main_Location_Code, TSPL_Item_Sublocation_Mapping.Item_code ,TSPL_Item_Sublocation_Mapping.Sub_Location_Code  from TSPL_Item_Sublocation_Mapping where Main_Location_Code = '" + strLocationCode + "') as TSPL_Item_Sublocation_Mapping on TSPL_Item_Sublocation_Mapping.Item_Code = tspl_item_master.item_code
    '                           left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_Item_Sublocation_Mapping.Sub_Location_Code  
    '                           where isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1  order by tspl_item_master.Sku_Seq "
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            arrObj = New List(Of clsItemSublocationMappingDetail)
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                obj = New clsItemSublocationMappingDetail()
    '                obj.Item_code = clsCommon.myCstr(dt.Rows(i)("Item_code"))
    '                obj.Item_Name = clsCommon.myCstr(dt.Rows(i)("Item_Name"))
    '                obj.Item_ShortName = clsCommon.myCstr(dt.Rows(i)("Item_ShortName"))
    '                obj.Sub_Location_Code = clsCommon.myCstr(dt.Rows(i)("Sub_Location_Code"))
    '                obj.Sub_Location_Name = clsCommon.myCstr(dt.Rows(i)("Sub_Location_Name"))
    '                arrObj.Add(obj)
    '            Next
    '        End If
    '        Return arrObj
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

End Class




