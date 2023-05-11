Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsProcessMaster
    Public Process_Code As String = Nothing
    Public Process_Desc As String = Nothing
    ' Public isNewEntry As Boolean = False
    'Public Item_Code As String = Nothing
    'Public Capacaity As String = Nothing
    Public arrProcessDetail As List(Of clsProcessMasterDetail) = Nothing
    'Public Shared ObjList As List(Of clsProcessMaster)

    'Public Shared Function SaveData(ByVal arr As List(Of clsProcessMaster), ByVal strProcessCode As String) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        SaveData(arr, strProcessCode, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
    Public Shared Function SaveData(ByVal obj As clsProcessMaster, ByVal trans As SqlTransaction, ByVal IsNewEntry As Boolean) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = ""
            'If ItemCode <> "" Then
            '    qry = "delete from TSPL_Process_master where Process_Code='" + strProcessCode + "' and Item_Code='" + ItemCode + "'"
            'Else
            '    qry = "delete from TSPL_Process_master where Process_Code='" + strProcessCode + "'"
            'End If
            'Dim qry As String = "delete from TSPL_Process_master where Process_Code='" + strProcessCode + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'For Each obj As clsProcessMaster In arr
            '    If (obj.Process_Code <> "") Then
            'DeleteData(obj.Process_Code)
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Process_Code", obj.Process_Code)
            clsCommon.AddColumnsForChange(coll, "Process_Desc", obj.Process_Desc)
            'clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            'clsCommon.AddColumnsForChange(coll, "Capacaity", obj.Capacaity)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If IsNewEntry = True Then
                isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_process_master", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_process_master", OMInsertOrUpdate.Update, "TSPL_process_master.Process_Code='" + obj.Process_Code + "'", trans)
            End If
            isSaved = isSaved And clsProcessMasterDetail.SaveData(obj.arrProcessDetail,obj.Process_Code, trans)
            trans.Commit()
            Return isSaved



            'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_process_master", OMInsertOrUpdate.Insert, "", trans)

            '  End If

            ' Next


        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    
    Public Shared Function GetData(ByVal strDocNo As String) As List(Of clsProcessMaster)
        Dim Arr As List(Of clsProcessMaster) = Nothing
        Dim qry As String = "select  * from TSPL_process_master  where Process_Code='" & strDocNo & "'"

        Dim obj As clsProcessMaster = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsProcessMaster)
            For Each dr As DataRow In dt.Rows
                obj = New clsProcessMaster()
                obj.Process_Code = clsCommon.myCstr(dr("Process_Code"))
                obj.Process_Desc = clsCommon.myCstr(dr("Process_Desc"))
                'obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                'obj.Capacaity = clsCommon.myCstr(dr("Capacaity"))
                Arr.Add(obj)
            Next
           
                End If
        Return Arr
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            clsProcessMasterDetail.deleteData(strCode, trans)
            qry = "delete from TSPL_process_master where Process_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
           
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function



End Class





Public Class clsProcessMasterDetail
    Public Process_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Capacity As String = Nothing
    Public Shared Function deleteData(ByVal pcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_PROCESS_DETAIL where Process_Code='" & pcode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function SaveData(ByVal Arr As List(Of clsProcessMasterDetail), ByVal pocode As String, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim i As Integer = 0

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                deleteData(pocode, Trans)
               
                For i = 0 To Arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Process_Code", Arr.Item(i).Process_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", Arr.Item(i).Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Capacity", Arr.Item(i).Capacity)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROCESS_DETAIL", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If
        Catch ex As Exception
            Trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try



        Return True
    End Function
    Public Shared Function LoadData(ByVal pcode As String) As List(Of clsProcessMasterDetail)
        Dim arr As New List(Of clsProcessMasterDetail)
        Try
            'Dim arr As New List(Of clsProcessMasterDetail)
            Dim obj As New clsProcessMasterDetail
            Dim q As String = "Select TSPL_Process_master.Process_Code ,TSPL_PROCESS_DETAIL.Item_Code,TSPL_PROCESS_DETAIL.Capacity from TSPL_Process_master inner join TSPL_PROCESS_DETAIL on TSPL_Process_master.Process_Code =TSPL_PROCESS_DETAIL.Process_Code where TSPL_Process_master.Process_Code='" & pcode & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsProcessMasterDetail
                    obj.Process_Code = clsCommon.myCstr(dtbl.Rows(i)("Process_Code"))
                    obj.Item_Code = clsCommon.myCstr(dtbl.Rows(i)("Item_Code"))
                    obj.Capacity = clsCommon.myCdbl(dtbl.Rows(i)("Capacity"))
                    arr.Add(obj)
                Next
            End If
            '  Return arr
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
End Class
