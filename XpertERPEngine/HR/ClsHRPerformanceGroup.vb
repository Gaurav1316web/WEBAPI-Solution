Imports System.Data.SqlClient
Imports common
Public Class ClsHRPerformanceGroup
#Region "variables"
    Public Code As String = Nothing
    Public PerformanceCode As String = Nothing
    Public Persent As Double = 0
    Public PER_CAT As String = Nothing

    Public Performance_Name As String = Nothing
    Public Performance_Cat As String = Nothing
    Public Description As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of ClsHRPerformanceGroup)) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ClsHRPerformanceGroup.SaveData(strCode, arr, trans) Then
                trans.Commit()
                Return True
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of ClsHRPerformanceGroup), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_HR_PERFORMANCE_GROUP where Code ='" + strCode + "' and  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'  "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsHRPerformanceGroup In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "PerformanceCode", obj.PerformanceCode)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Persent", clsCommon.myCdbl(obj.Persent))
                clsCommon.AddColumnsForChange(coll, "PER_CAT", clsCommon.myCstr(obj.PER_CAT))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PERFORMANCE_GROUP", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    'Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRPerformanceGroup
    '    Return GetData(strCode, NavType, Nothing)
    'End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "select TSPL_HR_PERFORMANCE_GROUP.Code,TSPL_HR_PERFORMANCE_GROUP.Description As [Description], TSPL_HR_PERFORMANCE_GROUP.PerformanceCode, TSPL_HR_PERFORMANCE_GROUP.Persent, TSPL_HR_PERFORMANCE_GROUP.PER_CAT, TSPL_HR_PERFORMANCE_MASTER.Name, TSPL_HR_PERFORMANCE_MASTER.PERCAT_CODE as [Category] " & _
        " from TSPL_HR_PERFORMANCE_GROUP " & _
        " left outer join TSPL_HR_PERFORMANCE_MASTER on TSPL_HR_PERFORMANCE_GROUP.PerformanceCode =TSPL_HR_PERFORMANCE_MASTER.CODE  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_PERFORMANCE_GROUP.Code = (select MIN(Code) from TSPL_HR_PERFORMANCE_GROUP WHERE 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_PERFORMANCE_GROUP.Code = (select Max(Code) from TSPL_HR_PERFORMANCE_GROUP WHERE 1=1  )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_PERFORMANCE_GROUP.Code = (select top 1 Code from TSPL_HR_PERFORMANCE_GROUP WHERE   Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_PERFORMANCE_GROUP.Code = (select Min(Code) from TSPL_HR_PERFORMANCE_GROUP where Code>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_PERFORMANCE_GROUP.Code = (select Max(Code) from TSPL_HR_PERFORMANCE_GROUP where Code<'" + strCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

End Class
