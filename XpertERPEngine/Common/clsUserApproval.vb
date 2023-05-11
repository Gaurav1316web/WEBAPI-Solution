Imports common
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class clsUserApproval
#Region "Variables"
    Public USER_CODE As String = Nothing
    Public Created As String = Nothing
    Public Opened As String = Nothing
    Public Approved As String = Nothing
    Public OnHold As String = Nothing
    Public Closed As String = Nothing
    Public Completed As String = Nothing
    Public Inactive As String = Nothing

    Public Shared Function SaveData(ByVal Arr As List(Of clsUserApproval)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Delete from TSPL_USER_APPROVAL "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsUserApproval In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "USER_CODE", obj.USER_CODE)
                    clsCommon.AddColumnsForChange(coll, "Created", obj.Created)
                    clsCommon.AddColumnsForChange(coll, "Opened", obj.Opened)
                    clsCommon.AddColumnsForChange(coll, "Approved", obj.Approved)
                    clsCommon.AddColumnsForChange(coll, "OnHold", obj.OnHold)
                    clsCommon.AddColumnsForChange(coll, "Closed", obj.Closed)
                    clsCommon.AddColumnsForChange(coll, "Completed", obj.Completed)
                    clsCommon.AddColumnsForChange(coll, "Inactive", obj.Inactive)
                    'clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    
                    
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_APPROVAL", OMInsertOrUpdate.Insert, "", trans)
                Next
                If isSaved Then
                    trans.Commit()
                    Throw New Exception("Data Saved Successfully")
                Else
                    trans.Rollback()
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

#End Region
End Class
