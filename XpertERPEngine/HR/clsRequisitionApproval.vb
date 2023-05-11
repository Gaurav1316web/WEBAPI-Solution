Imports common
Imports System.Data.SqlClient
Public Class clsRequisitionApproval

#Region "Variables"
    Public Level1 As Double = 0
    Public Level2 As Double = 0
    Public Level3 As Double = 0
    Public Approval_Level As Double = 0
#End Region

    Public Function SaveData(ByVal obj As clsRequisitionApproval) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If SaveData(obj, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsRequisitionApproval, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim Qry As String

            Qry = "Delete From TSPL_REQUISITION_APPROVAL"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Level1", obj.Level1)
            clsCommon.AddColumnsForChange(coll, "Level2", obj.Level2)
            clsCommon.AddColumnsForChange(coll, "Level3", obj.Level3)
            clsCommon.AddColumnsForChange(coll, "Approval_Level", obj.Approval_Level)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REQUISITION_APPROVAL", OMInsertOrUpdate.Insert, "", trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData() As clsRequisitionApproval
        Dim obj As clsRequisitionApproval = Nothing
        Dim qry As String = "Select Level1, Level2, Level3, Approval_Level from  TSPL_REQUISITION_APPROVAL"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRequisitionApproval()
            obj.Level1 = clsCommon.myCstr(dt.Rows(0)("Level1"))
            obj.Level2 = clsCommon.myCstr(dt.Rows(0)("Level2"))
            obj.Level3 = clsCommon.myCstr(dt.Rows(0)("Level3"))
            obj.Approval_Level = clsCommon.myCstr(dt.Rows(0)("Approval_Level"))
        End If
        Return obj
    End Function


    Public Shared Function DeleteData() As Boolean
        Dim isSaved As Boolean = False
        Try
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_REQUISITION_APPROVAL")
            If (isSaved) Then
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class

