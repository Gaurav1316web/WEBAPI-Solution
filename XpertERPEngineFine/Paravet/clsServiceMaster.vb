Imports System.Data.SqlClient
Imports common
Public Class clsServiceMaster
#Region "Variables"
    Public Service_Code As String = Nothing
    Public Service_Desc As String = Nothing
    Public Service_Group_Name As String = Nothing
    Public Service_Group_Desc As String = Nothing
    Public Cattle_Type_Code As String = Nothing
    Public Breed_Code As String = Nothing
    Public Service_Charge As Decimal = Nothing
    Public Reminder_Days As Integer = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
    Public Comp_Code As String = Nothing

#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Service_Code as [Code],Service_Desc as [Service Description] ,Service_Group_Name AS [Service Group Name],Service_Group_Desc as [Group Description],Cattle_Type_Code as [Cattle Type Code],Created_By as [Created By] ,Convert(varchar,Created_Date,103) as [Created Date] ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date]  from TSPL_Paravet_Service_Master "
        str = clsCommon.ShowSelectForm("GRUPTYPE", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsServiceMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Service_Desc", obj.Service_Desc)
            clsCommon.AddColumnsForChange(coll, "Service_Group_Name", obj.Service_Group_Name)
            clsCommon.AddColumnsForChange(coll, "Service_Group_Desc", obj.Service_Group_Desc)
            clsCommon.AddColumnsForChange(coll, "Cattle_Type_Code", obj.Cattle_Type_Code)
            clsCommon.AddColumnsForChange(coll, "Service_Charge", obj.Service_Charge)
            clsCommon.AddColumnsForChange(coll, "Breed_Code", obj.Breed_Code)
            clsCommon.AddColumnsForChange(coll, "Reminder_Days", obj.Reminder_Days)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Service_Code", obj.Service_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Paravet_Service_Master", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Paravet_Service_Master", OMInsertOrUpdate.Update, "TSPL_Paravet_Service_Master.Service_Code='" + obj.Service_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    ' Service_Code,Service_Desc,Service_Group_Name,Service_Group_Desc,Cattle_Type_Code,Breed_Code,Service_Charge,Reminder_Days
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsServiceMaster
        Dim obj As clsServiceMaster = Nothing
        Dim Arr As List(Of clsServiceMaster) = Nothing
        Dim qry As String = " select Service_Code ,Service_Desc ,Service_Group_Name ,Service_Group_Desc ,Cattle_Type_Code ,Breed_Code ,Service_Charge ,Reminder_Days  from TSPL_Paravet_Service_Master where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Paravet_Service_Master.Service_Code = (select MIN(Service_Code) from TSPL_Paravet_Service_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Paravet_Service_Master.Service_Code  = (select Max(Service_Code) from TSPL_Paravet_Service_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Paravet_Service_Master.Service_Code  = (select TOP 1 Service_Code from TSPL_Paravet_Service_Master WHERE 1=1 " + whrclas + " and Service_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Paravet_Service_Master.Service_Code  = (select Min(Service_Code) from TSPL_Paravet_Service_Master where Service_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Paravet_Service_Master.Service_Code  = (select Max(Service_Code) from TSPL_Paravet_Service_Master where Service_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsServiceMaster()
            obj.Service_Code = clsCommon.myCstr(dt.Rows(0)("Service_Code"))
            obj.Service_Desc = clsCommon.myCstr(dt.Rows(0)("Service_Desc"))
            obj.Service_Group_Name = clsCommon.myCstr(dt.Rows(0)("Service_Group_Name"))
            obj.Service_Group_Desc = clsCommon.myCstr(dt.Rows(0)("Service_Group_Desc"))
            obj.Cattle_Type_Code = clsCommon.myCstr(dt.Rows(0)("Cattle_Type_Code"))
            obj.Breed_Code = clsCommon.myCstr(dt.Rows(0)("Breed_Code"))
            obj.Service_Charge = clsCommon.myCstr(dt.Rows(0)("Service_Charge"))
            obj.Reminder_Days = clsCommon.myCstr(dt.Rows(0)("Reminder_Days"))
        End If
        Return obj
    End Function



End Class
