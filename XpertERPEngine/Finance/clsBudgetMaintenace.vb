Imports common
Imports System.Data.SqlClient


Public Class clsBudgetMaintenace
#Region "Variable"
    Public PROJECT_CODE As String = Nothing
    Public SPECIFICATION As String
    Public FROM_DATE As String
    Public TO_DATE As String
    Public Budget As String
    Public Remarks As String

#End Region
    Public Function SaveData(ByVal obj As clsBudgetMaintenace, ByVal isNewEntry As Boolean, ByVal Trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try

            'Dim qry As String = "delete from TSPL_BUDGET_MAINTENANCE where PROJECT_CODE= '" & obj.PROJECT_CODE & "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SPECIFICATION", obj.SPECIFICATION)
            clsCommon.AddColumnsForChange(coll, "FROM_DATE", clsCommon.GetPrintDate(obj.FROM_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "TO_DATE", clsCommon.GetPrintDate(obj.TO_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Budget", obj.Budget)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "PROJECT_CODE", obj.PROJECT_CODE)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            Dim Qry As String = "Select PROJECT_CODE from TSPL_BUDGET_MAINTENANCE where PROJECT_CODE='" & obj.PROJECT_CODE & "'"
            Dim check As String = clsDBFuncationality.getSingleValue(Qry, Trans)
            If check = "" OrElse check = Nothing Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BUDGET_MAINTENANCE", OMInsertOrUpdate.Insert, "", Trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BUDGET_MAINTENANCE", OMInsertOrUpdate.Update, "PROJECT_CODE='" + obj.PROJECT_CODE + "'", Trans)
            End If


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved

    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Project Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_BUDGET_MAINTENANCE where PROJECT_CODE='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class
