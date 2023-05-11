'--------Created By Richa 23/07/2014 Against Ticket No BM00000003237
Imports common
Imports System.Data.SqlClient
Public Class ClsVendorPermission
#Region "Variables"
    Public User_Code As String = Nothing
    Public Vendor_Group_Code As String = Nothing
    Public Vendor_Code As String = Nothing
#End Region
    Public Function SaveData(ByVal UserCode As String, ByVal Arr As List(Of ClsVendorPermission)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(UserCode) > 0 Then
                Dim qry As String = "delete from TSPL_VENDOR_MAPPING where User_Code='" + UserCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            For Each obj As ClsVendorPermission In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "User_Code", obj.User_Code)
                clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", obj.Vendor_Group_Code)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MAPPING", OMInsertOrUpdate.Insert, "", trans)
            Next
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = "delete from TSPL_VENDOR_MAPPING where User_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
End Class
