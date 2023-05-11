'--------Created By Richa 21/07/2014 Against Ticket No BM00000003236
Imports common
Imports System.Data.SqlClient
Public Class ClsCustomerPermission
    ''Checkin Sanjay 22/06/2020
#Region "Variables"
    Public User_Code As String = Nothing
    Public Cust_Group_Code As String = Nothing
    Public Cust_Code As String = Nothing
#End Region
    Public Function SaveData(ByVal UserCode As String, ByVal Arr As List(Of ClsCustomerPermission), Optional ByVal Is_Import As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(UserCode) > 0 Then
                If Is_Import = True Then
                    Dim qry As String = "delete from TSPL_CUSTOMER_MAPPING where User_Code in (" + UserCode + ")"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    Dim qry As String = "delete from TSPL_CUSTOMER_MAPPING where User_Code = '" + UserCode + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
            For Each obj As ClsCustomerPermission In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "User_Code", obj.User_Code)
                clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_MAPPING", OMInsertOrUpdate.Insert, "", trans)
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
            Dim qry As String = "delete from TSPL_CUSTOMER_MAPPING where User_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
End Class
