Imports System.Data.SqlClient
Imports common

Public Class ClsSourceTypeMaster

#Region "Variables"
    Public Source_Type_Code As String = Nothing
    Public Source_Name As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsSourceTypeMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Source_Name", obj.Source_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Source_Type_Code", obj.Source_Type_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_SOURCE_TYPE", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_SOURCE_TYPE", OMInsertOrUpdate.Update, "TSPL_HR_SOURCE_TYPE.Source_Type_Code='" + obj.Source_Type_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsSourceTypeMaster
        Dim obj As ClsSourceTypeMaster = Nothing
        Dim Arr As List(Of ClsSourceTypeMaster) = Nothing
        Dim qry As String = "select Source_Type_Code ,Source_Name from TSPL_HR_SOURCE_TYPE where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_SOURCE_TYPE.Source_Type_Code = (select MIN(Source_Type_Code) from TSPL_HR_SOURCE_TYPE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_SOURCE_TYPE.Source_Type_Code = (select Max(Source_Type_Code) from TSPL_HR_SOURCE_TYPE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_SOURCE_TYPE.Source_Type_Code = (select TOP 1 Source_Type_Code from TSPL_HR_SOURCE_TYPE WHERE 1=1 " + whrclas + " and Source_Type_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_SOURCE_TYPE.Source_Type_Code = (select Min(Source_Type_Code) from TSPL_HR_SOURCE_TYPE where Source_Type_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_SOURCE_TYPE.Source_Type_Code = (select Max(Source_Type_Code) from TSPL_HR_SOURCE_TYPE where Source_Type_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsSourceTypeMaster()
            obj.Source_Type_Code = clsCommon.myCstr(dt.Rows(0)("Source_Type_Code"))
            obj.Source_Name = clsCommon.myCstr(dt.Rows(0)("Source_Name"))
        End If
        Return obj
    End Function
End Class
