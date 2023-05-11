Imports System.Data.SqlClient
Imports common
Public Class clsSalesHierarchyMapping
#Region "Variables"
    Public CustCode As String = Nothing
    Public CustName As String = Nothing
    Public StructCode As String = Nothing
    Public StructName As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal Arr As List(Of clsSalesHierarchyMapping), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each chk As clsSalesHierarchyMapping In Arr
                    Try

                        'Dim coll As New Hashtable()
                        'clsCommon.AddColumnsForChange(coll, "Cust_Code", chk.CustCode)
                        'clsCommon.AddColumnsForChange(coll, "Struct_Code", chk.StructCode, True)
                        Dim qry As String = "update TSPL_CUSTOMER_MASTER set Struct_Code=" & IIf(clsCommon.myCstr(chk.StructCode) = "", "Null", "'" & clsCommon.myCstr(chk.StructCode) & "'") & " where Cust_Code='" & chk.CustCode & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try
                Next chk
            End If
            'trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            'Return False
            Throw New Exception(ex.Message)
        End Try
       
    End Function
End Class
