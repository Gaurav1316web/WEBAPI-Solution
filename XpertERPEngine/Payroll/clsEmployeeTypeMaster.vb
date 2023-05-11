Imports System.Data.SqlClient
Imports common

Public Class clsEmployeeTypeMaster

#Region "Variables"
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsEmployeeTypeMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_EMP_TYPE_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_EMP_TYPE_MASTER", OMInsertOrUpdate.Update, "TSPL_HR_EMP_TYPE_MASTER.Code='" + obj.Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    'Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEmployeeTypeMaster
    '    Return GetData(strCode, NavType, Nothing)
    'End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEmployeeTypeMaster
        Dim obj As clsEmployeeTypeMaster = Nothing
        Dim Arr As List(Of clsEmployeeTypeMaster) = Nothing
        Dim qry As String = "select Code ,Name from TSPL_HR_EMP_TYPE_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_EMP_TYPE_MASTER.Code = (select MIN(Code) from TSPL_HR_EMP_TYPE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_EMP_TYPE_MASTER.Code = (select Max(Code) from TSPL_HR_EMP_TYPE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_EMP_TYPE_MASTER.Code = (select TOP 1 Code from TSPL_HR_EMP_TYPE_MASTER WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_EMP_TYPE_MASTER.Code = (select Min(Code) from TSPL_HR_EMP_TYPE_MASTER where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_EMP_TYPE_MASTER.Code = (select Max(Code) from TSPL_HR_EMP_TYPE_MASTER where Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEmployeeTypeMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
        End If
        Return obj
    End Function
End Class

