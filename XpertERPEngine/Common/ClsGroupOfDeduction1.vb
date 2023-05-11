Imports common
Imports System.Data.SqlClient
Public Class ClsGroupOfDeduction1

#Region "Variables"
    Public DedCode As String = Nothing
    Public MCCCode As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
#End Region
    Public Function SaveData(ByVal Code As List(Of String), ByVal Arr As List(Of ClsGroupOfDeduction1)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'For Each Usercode As String In Usr
            Dim qry As String = " delete from TSPL_MCC_GROUP_OF_DEDUCTION where Ded_Code='" + DedCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Next
            For Each obj As ClsGroupOfDeduction1 In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Ded_Code", obj.DedCode)
                clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCCCode)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_GROUP_OF_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
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
