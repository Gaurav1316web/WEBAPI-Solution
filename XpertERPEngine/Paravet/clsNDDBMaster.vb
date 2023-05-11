Imports System.Data.SqlClient
Imports common
Public Class clsNDDBMaster
#Region "Variables"
    Public NDDB_No As String = Nothing
    Public NDDB_Desc As String = Nothing
    Public NDDB_Date As Date? = Nothing
    Public Tag_Prefix As String = Nothing
    Public Tag_SNO As String = Nothing
    Public USED_By As String = Nothing
    Public Farmer_Id As String = Nothing
    Public Cattle_Id As String = Nothing
    Public Cattle_Type As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
    Public Comp_Code As String = Nothing

    'NDDB_No,NDDB_Desc,NDDB_Date,Tag_Prefix,Tag_SNO,USED_By,Farmer_Id,Cattle_Id,Cattle_Type

#End Region
    Public Shared Function SaveData(ByVal obj As clsNDDBMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "NDDB_Desc", obj.NDDB_Desc)
            clsCommon.AddColumnsForChange(coll, "NDDB_Date", clsCommon.GetPrintDate(obj.NDDB_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tag_Prefix", obj.Tag_Prefix)
            clsCommon.AddColumnsForChange(coll, "Tag_SNO", obj.Tag_SNO)
            clsCommon.AddColumnsForChange(coll, "USED_By", obj.USED_By)
            'clsCommon.AddColumnsForChange(coll, "Farmer_Id", obj.Farmer_Id)
            'clsCommon.AddColumnsForChange(coll, "Cattle_Id", obj.Cattle_Id)
            'clsCommon.AddColumnsForChange(coll, "Cattle_Type", obj.Cattle_Type)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "NDDB_No", obj.NDDB_No)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Paravet_NDDB_Master", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Paravet_NDDB_Master", OMInsertOrUpdate.Update, "TSPL_Paravet_NDDB_Master.NDDB_No='" + obj.NDDB_No + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsNDDBMaster
        Dim obj As clsNDDBMaster = Nothing
        Dim Arr As List(Of clsNDDBMaster) = Nothing
        Dim qry As String = " select NDDB_No,NDDB_Desc,NDDB_Date,Tag_Prefix,Tag_SNO,USED_By,Farmer_Id,Cattle_Id,Cattle_Type from TSPL_Paravet_NDDB_Master where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Paravet_NDDB_Master.NDDB_No = (select MIN(NDDB_No) from TSPL_Paravet_NDDB_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Paravet_NDDB_Master.NDDB_No  = (select Max(NDDB_No) from TSPL_Paravet_NDDB_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Paravet_NDDB_Master.NDDB_No  = (select TOP 1 NDDB_No from TSPL_Paravet_NDDB_Master WHERE 1=1 " + whrclas + " and NDDB_No='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Paravet_NDDB_Master.NDDB_No  = (select Min(NDDB_No) from TSPL_Paravet_NDDB_Master where NDDB_No > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Paravet_NDDB_Master.NDDB_No  = (select Max(NDDB_No) from TSPL_Paravet_NDDB_Master where NDDB_No < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsNDDBMaster()
            obj.NDDB_No = clsCommon.myCstr(dt.Rows(0)("NDDB_No"))
            obj.NDDB_Desc = clsCommon.myCstr(dt.Rows(0)("NDDB_Desc"))
            obj.NDDB_Date = clsCommon.myCstr(dt.Rows(0)("NDDB_Date"))
            obj.Tag_Prefix = clsCommon.myCstr(dt.Rows(0)("Tag_Prefix"))
            obj.Tag_SNO = clsCommon.myCstr(dt.Rows(0)("Tag_SNO"))
            obj.USED_By = clsCommon.myCstr(dt.Rows(0)("USED_By"))
            obj.Farmer_Id = clsCommon.myCstr(dt.Rows(0)("Farmer_Id"))
            obj.Cattle_Id = clsCommon.myCstr(dt.Rows(0)("Cattle_Id"))
            obj.Cattle_Type = clsCommon.myCstr(dt.Rows(0)("Cattle_Type"))
        End If
        Return obj
    End Function


End Class
