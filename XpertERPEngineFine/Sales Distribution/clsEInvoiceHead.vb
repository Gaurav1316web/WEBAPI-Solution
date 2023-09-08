Imports common
Imports System.Data.SqlClient

Public Class clsEInvoiceHead
    Public url As String = Nothing
    Public username As String = Nothing
    Public password As String = Nothing
    Public ip_address As String = Nothing
    Public client_id As String = Nothing
    Public client_secret As String = Nothing
    Public GSTin As String = Nothing
    Public RequiredFor As String = Nothing
    Public VendorName As String = Nothing
    Public Location_Code As String = Nothing

    Public Function SaveData(ByVal obj As clsEInvoiceHead, ByVal isNewEntry As Boolean, code As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()


        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "url", obj.url)
            clsCommon.AddColumnsForChange(coll, "username", obj.username)
            clsCommon.AddColumnsForChange(coll, "password", obj.password)
            clsCommon.AddColumnsForChange(coll, "ip_address", obj.ip_address)
            clsCommon.AddColumnsForChange(coll, "client_id", obj.client_id)
            clsCommon.AddColumnsForChange(coll, "client_secret", obj.client_secret)
            clsCommon.AddColumnsForChange(coll, "GSTin", obj.GSTin)
            clsCommon.AddColumnsForChange(coll, "RequiredFor", obj.RequiredFor)
            clsCommon.AddColumnsForChange(coll, "VendorName", obj.VendorName)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)

            If isNewEntry Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EINVOICEHEADER_INFO", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EINVOICEHEADER_INFO", OMInsertOrUpdate.Update, "TSPL_EINVOICEHEADER_INFO.code= " + code + "", trans)
            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved

    End Function
End Class
