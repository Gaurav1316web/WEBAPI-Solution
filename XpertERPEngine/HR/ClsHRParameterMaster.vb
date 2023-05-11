Imports System.Data.SqlClient
Imports common

Public Class ClsHRParameterMaster

#Region "Variables"
    Public Parameter_Code As String = Nothing
    Public Parameter_Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsHRParameterMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Parameter_Name", obj.Parameter_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Parameter_Code", obj.Parameter_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PARAMETER_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PARAMETER_MASTER", OMInsertOrUpdate.Update, "TSPL_HR_PARAMETER_MASTER.Parameter_Code='" + obj.Parameter_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    'Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRParameterMaster
    '    Return GetData(strCode, NavType, Nothing)
    'End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRParameterMaster
        Dim obj As ClsHRParameterMaster = Nothing
        Dim Arr As List(Of ClsHRParameterMaster) = Nothing
        Dim qry As String = "select Parameter_Code ,Parameter_Name from TSPL_HR_PARAMETER_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_PARAMETER_MASTER.Parameter_Code = (select MIN(Parameter_Code) from TSPL_HR_PARAMETER_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_PARAMETER_MASTER.Parameter_Code = (select Max(Parameter_Code) from TSPL_HR_PARAMETER_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_PARAMETER_MASTER.Parameter_Code = (select TOP 1 Parameter_Code from TSPL_HR_PARAMETER_MASTER WHERE 1=1 " + whrclas + " and Parameter_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_PARAMETER_MASTER.Parameter_Code = (select Min(Parameter_Code) from TSPL_HR_PARAMETER_MASTER where Parameter_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_PARAMETER_MASTER.Parameter_Code = (select Max(Parameter_Code) from TSPL_HR_PARAMETER_MASTER where Parameter_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHRParameterMaster()
            obj.Parameter_Code = clsCommon.myCstr(dt.Rows(0)("Parameter_Code"))
            obj.Parameter_Name = clsCommon.myCstr(dt.Rows(0)("Parameter_Name"))
        End If
        Return obj
    End Function
End Class
