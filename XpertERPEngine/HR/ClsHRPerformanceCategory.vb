Imports System.Data.SqlClient
Imports common

Public Class ClsHRPerformanceCategory

#Region "variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public IsKRA As Boolean = False
#End Region

    Public Shared Function SaveData(ByVal obj As ClsHRPerformanceCategory, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ClsHRPerformanceCategory.SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                Return True
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsHRPerformanceCategory, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "IsKRA", obj.IsKRA)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PERFORMANCE_CATEGORY", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PERFORMANCE_CATEGORY", OMInsertOrUpdate.Update, "TSPL_HR_PERFORMANCE_CATEGORY.Code='" + obj.Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRPerformanceCategory
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsHRPerformanceCategory
        Dim obj As ClsHRPerformanceCategory = Nothing
        Dim qry As String = " select Code,Description,IsKRA from TSPL_HR_PERFORMANCE_CATEGORY where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_PERFORMANCE_CATEGORY.Code = (select MIN(Code) from TSPL_HR_PERFORMANCE_CATEGORY WHERE 1=1 and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'  )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_PERFORMANCE_CATEGORY.Code = (select Max(Code) from TSPL_HR_PERFORMANCE_CATEGORY WHERE 1=1 and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_PERFORMANCE_CATEGORY.Code = (select top 1 Code from TSPL_HR_PERFORMANCE_CATEGORY WHERE 1=1  and Code='" + strCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
            Case NavigatorType.Next
                qry += " and TSPL_HR_PERFORMANCE_CATEGORY.Code = (select Min(Code) from TSPL_HR_PERFORMANCE_CATEGORY where Code>'" + strCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_PERFORMANCE_CATEGORY.Code = (select Max(Code) from TSPL_HR_PERFORMANCE_CATEGORY where Code<'" + strCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHRPerformanceCategory()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.IsKRA = clsCommon.myCBool(dt.Rows(0)("IsKRA"))
        End If
        Return obj
    End Function

    Public Shared Function GetDescription(ByVal StrCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_HR_PERFORMANCE_CATEGORY Where Code='" + StrCode + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
