Imports System.Data.SqlClient
Imports common
Public Class clsServiceName
#Region "Variables"
    Public Service_Name_Code As String = Nothing
    Public Service_Desc As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
#End Region
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Service_Name_Code as [Code], Service_Desc AS [Description],Created_By as [Created By] ,Convert(varchar,Created_Date,103) as [Created Date] ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date]  from TSPL_Paravet_Service_Name "
        str = clsCommon.ShowSelectForm("SERVISENAMETYPE", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    'Public Shared Function SaveData(ByVal obj As clsBredType, ByVal isNewEntry As Boolean) As Boolean
    '    Dim qry As String = ""
    '    Dim IsSaved As Boolean = False
    '    Try
    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "Bred_Type_Name", obj.Bred_Type_Name)
    '        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
    '        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
    '        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
    '        If isNewEntry Then
    '            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
    '            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
    '            clsCommon.AddColumnsForChange(coll, "Service_Name_Code", obj.Service_Name_Code)
    '            IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BRED_TYPE_MASTER", OMInsertOrUpdate.Insert, "")
    '        Else
    '            IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BRED_TYPE_MASTER", OMInsertOrUpdate.Update, "TSPL_BRED_TYPE_MASTER.Bred_Type_Code='" + obj.Bred_Type_Code + "'")
    '        End If

    '    Catch err As Exception
    '        Throw New Exception(err.Message)
    '    End Try
    '    Return IsSaved
    'End Function






End Class
