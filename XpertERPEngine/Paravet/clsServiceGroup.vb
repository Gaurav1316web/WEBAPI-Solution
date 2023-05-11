Imports System.Data.SqlClient
Imports common
Public Class clsServiceGroup
#Region "Variables"
    Public Service_Group_Code As String = Nothing
    Public Service_Group_Name As String = Nothing
    Public Service_Group_Desc As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
    Public Comp_Code As String = Nothing
#End Region
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Service_Group_Code as [Code],Service_Group_Name as [Group Name] ,Service_Group_Desc AS [Description],Created_By as [Created By] ,Convert(varchar,Created_Date,103) as [Created Date] ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date]  from TSPL_Paravet_Service_Group "
        str = clsCommon.ShowSelectForm("GRUPTYPE", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsServiceGroup, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Service_Group_Name", obj.Service_Group_Name)
            clsCommon.AddColumnsForChange(coll, "Service_Group_Desc", obj.Service_Group_Desc)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Service_Group_Code", obj.Service_Group_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Paravet_Service_Group", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Paravet_Service_Group", OMInsertOrUpdate.Update, "TSPL_Paravet_Service_Group.Service_Group_Code='" + obj.Service_Group_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsServiceGroup
        Dim obj As clsServiceGroup = Nothing
        Dim Arr As List(Of clsServiceGroup) = Nothing
        Dim qry As String = " select Service_Group_Code as [Code],Service_Group_Name [Name] ,Service_Group_Desc as  [Description] from TSPL_Paravet_Service_Group where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Paravet_Service_Group.Service_Group_Code = (select MIN(Service_Group_Code) from TSPL_Paravet_Service_Group WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Paravet_Service_Group.Service_Group_Code  = (select Max(Service_Group_Code) from TSPL_Paravet_Service_Group WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Paravet_Service_Group.Service_Group_Code  = (select TOP 1 Service_Group_Code from TSPL_Paravet_Service_Group WHERE 1=1 " + whrclas + " and Service_Group_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Paravet_Service_Group.Service_Group_Code  = (select Min(Service_Group_Code) from TSPL_Paravet_Service_Group where Service_Group_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Paravet_Service_Group.Service_Group_Code  = (select Max(Service_Group_Code) from TSPL_Paravet_Service_Group where Service_Group_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsServiceGroup()
            obj.Service_Group_Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Service_Group_Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            obj.Service_Group_Desc = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        Return obj
    End Function


End Class
