Imports System.Data.SqlClient
Imports common
Public Class clsBullMaster
#Region "Variables"
    Public Bull_No As String = Nothing
    Public Bull_Desc As String = Nothing
    Public Bull_Date As Date? = Nothing
    Public Bull_Profile_Id As String = Nothing
    Public Cattle_Type As String = Nothing
    Public No_of_Straws As String = Nothing
    Public DOB As Date? = Nothing
    Public Status As String = Nothing
    Public Site_Id As String = Nothing
    Public Dams_Yield As String = Nothing
    Public Breed_Details As String = Nothing
    Public Breed_info As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
    Public Comp_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsBullMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Bull_Desc", obj.Bull_Desc)
            clsCommon.AddColumnsForChange(coll, "Bull_Date", clsCommon.GetPrintDate(obj.Bull_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Bull_Profile_Id", obj.Bull_Profile_Id)
            clsCommon.AddColumnsForChange(coll, "Cattle_Type", obj.Cattle_Type)
            clsCommon.AddColumnsForChange(coll, "No_of_Straws", obj.No_of_Straws)
            clsCommon.AddColumnsForChange(coll, "DOB", clsCommon.GetPrintDate(obj.DOB, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Site_Id", obj.Site_Id)
            clsCommon.AddColumnsForChange(coll, "Dams_Yield", obj.Dams_Yield)
            clsCommon.AddColumnsForChange(coll, "Breed_Details", obj.Breed_Details)
            clsCommon.AddColumnsForChange(coll, "Breed_info", obj.Breed_info)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Bull_No", obj.Bull_No)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Paravet_Bull_Master", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Paravet_Bull_Master", OMInsertOrUpdate.Update, "TSPL_Paravet_Bull_Master.Bull_No='" + obj.Bull_No + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullMaster
        Dim obj As clsBullMaster = Nothing
        Dim Arr As List(Of clsBullMaster) = Nothing
        Dim qry As String = " select Bull_No ,Bull_Desc,convert (varchar, Bull_Date,103) as Bull_Date,Bull_Profile_Id,Cattle_Type,No_of_Straws,convert (varchar,DOB,103) as DOB,Status,Dams_Yield,Breed_Details,Breed_info,Site_Id  from TSPL_Paravet_Bull_Master where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Paravet_Bull_Master.Bull_No = (select MIN(Bull_No) from TSPL_Paravet_Bull_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Paravet_Bull_Master.Bull_No  = (select Max(Bull_No) from TSPL_Paravet_Bull_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Paravet_Bull_Master.Bull_No  = (select TOP 1 Bull_No from TSPL_Paravet_Bull_Master WHERE 1=1 " + whrclas + " and Bull_No='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Paravet_Bull_Master.Bull_No  = (select Min(Bull_No) from TSPL_Paravet_Bull_Master where Bull_No > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Paravet_Bull_Master.Bull_No  = (select Max(Bull_No) from TSPL_Paravet_Bull_Master where Bull_No < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBullMaster()
            obj.Bull_No = clsCommon.myCstr(dt.Rows(0)("Bull_No"))
            obj.Bull_Desc = clsCommon.myCstr(dt.Rows(0)("Bull_Desc"))
            obj.Bull_Date = clsCommon.myCstr(dt.Rows(0)("Bull_Date"))
            obj.Bull_Profile_Id = clsCommon.myCstr(dt.Rows(0)("Bull_Profile_Id"))
            obj.Cattle_Type = clsCommon.myCstr(dt.Rows(0)("Cattle_Type"))
            obj.No_of_Straws = clsCommon.myCstr(dt.Rows(0)("No_of_Straws"))
            obj.DOB = clsCommon.myCstr(dt.Rows(0)("DOB"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Dams_Yield = clsCommon.myCstr(dt.Rows(0)("Dams_Yield"))
            obj.Breed_Details = clsCommon.myCstr(dt.Rows(0)("Breed_Details"))
            obj.Breed_info = clsCommon.myCstr(dt.Rows(0)("Breed_info"))
            obj.Site_Id = clsCommon.myCstr(dt.Rows(0)("Site_Id"))
        End If
        Return obj
    End Function






End Class
