Imports System.Data.SqlClient
Imports common
Public Class ClsPerformanceGroupMapping
#Region "variables"
    Public Emp_Code As String = Nothing
    Public User_Name As String = Nothing
    Public PERFORMANCE_GROUP_Code As String = Nothing
    Public Percent As Double = 0
    Public Category As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of ClsPerformanceGroupMapping), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_HR_PERFORMANCE_GROUP_MAPPING where Emp_Code = '" + strCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsPerformanceGroupMapping In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "PERFORMANCE_GROUP_Code", obj.PERFORMANCE_GROUP_Code)
                clsCommon.AddColumnsForChange(coll, "Persent", obj.Percent)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PERFORMANCE_GROUP_MAPPING", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As DataTable
        Dim Arr As List(Of ClsPerformanceGroupMapping) = Nothing
        Dim qry As String = "select distinct TSPL_EMPLOYEE_MASTER.Emp_Code, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_HR_PERFORMANCE_GROUP_MAPPING.PERFORMANCE_GROUP_Code, TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent,TSPL_HR_PERFORMANCE_GROUP.PER_CAT from TSPL_EMPLOYEE_MASTER " & _
        " left outer join TSPL_HR_PERFORMANCE_GROUP_MAPPING on TSPL_EMPLOYEE_MASTER.Emp_Code =TSPL_HR_PERFORMANCE_GROUP_MAPPING.Emp_Code " & _
        " left outer join TSPL_HR_PERFORMANCE_GROUP on TSPL_HR_PERFORMANCE_GROUP_MAPPING.PERFORMANCE_GROUP_Code =TSPL_HR_PERFORMANCE_GROUP.CODE  where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EMPLOYEE_MASTER.Emp_Code = (select MIN(Emp_Code) from TSPL_EMPLOYEE_MASTER WHERE 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_EMPLOYEE_MASTER.Emp_Code = (select Max(Emp_Code) from TSPL_EMPLOYEE_MASTER WHERE 1=1  )"
            Case NavigatorType.Current
                qry += " and TSPL_EMPLOYEE_MASTER.Emp_Code = (select top 1 Emp_Code from TSPL_EMPLOYEE_MASTER WHERE Emp_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_EMPLOYEE_MASTER.Emp_Code = (select Min(Emp_Code) from TSPL_EMPLOYEE_MASTER where Emp_Code>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_EMPLOYEE_MASTER.Emp_Code = (select Max(Emp_Code) from TSPL_EMPLOYEE_MASTER where Emp_Code<'" + strCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

End Class
