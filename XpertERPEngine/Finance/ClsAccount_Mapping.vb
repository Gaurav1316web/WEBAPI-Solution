Imports System
Imports System.Data.SqlClient
Imports common


Public Class ClsAccount_Mapping
#Region "veriables"
    Public Account_Code As String
    Public Mapped_account_Code As String
#End Region

    Public Shared Function SaveData(ByVal strAccountCode As String, ByVal Arr As List(Of ClsAccount_Mapping)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_GL_ACCOUNT_mapping where Account_Code='" + strAccountCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each Obj As ClsAccount_Mapping In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Account_Code", strAccountCode)
                clsCommon.AddColumnsForChange(coll, "Mapped_Account_Code ", Obj.Mapped_account_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_ACCOUNT_mapping", OMInsertOrUpdate.Insert, "", trans)
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
