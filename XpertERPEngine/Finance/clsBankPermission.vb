Imports common
Imports System.Data.SqlClient
Public Class clsBankPermission

#Region "Variables"
    Public Usercode As String = Nothing
    Public BankCode As String = Nothing
#End Region
    Public Function SaveData(ByVal Usr As List(Of String), ByVal Arr As List(Of clsBankPermission)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'For Each Usercode As String In Usr
            Dim qry As String = "delete from tspl_user_bank_mapping where Item_Code='" + Usercode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Next
            For Each obj As clsBankPermission In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Usercode)
                clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.BankCode)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_user_bank_mapping", OMInsertOrUpdate.Insert, "", trans)
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
End Class
