Imports common
Imports System.Data.SqlClient

Public Class clsDepreciationField
#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsDepreciationField, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEPRECIATION_FIELD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEPRECIATION_FIELD", OMInsertOrUpdate.Update, "TSPL_DEPRECIATION_FIELD.Code='" + obj.Code + "'", trans)
            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDepreciationField
        Dim obj As clsDepreciationField = Nothing
        Dim qry As String = "SELECT TSPL_DEPRECIATION_FIELD.Code,TSPL_DEPRECIATION_FIELD.Description FROM TSPL_DEPRECIATION_FIELD where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DEPRECIATION_FIELD.Code = (select MIN(Code) from TSPL_DEPRECIATION_FIELD where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_DEPRECIATION_FIELD.Code = (select Max(Code) from TSPL_DEPRECIATION_FIELD where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_DEPRECIATION_FIELD.Code = (select Min(Code) from TSPL_DEPRECIATION_FIELD where Code>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_DEPRECIATION_FIELD.Code = (select Max(Code) from TSPL_DEPRECIATION_FIELD where Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_DEPRECIATION_FIELD.Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDepreciationField()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If

        Dim qry As String = "delete from TSPL_DEPRECIATION_FIELD where Code='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)

    End Function
End Class
