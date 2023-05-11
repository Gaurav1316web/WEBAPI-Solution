Imports System.Data.SqlClient
Imports common
Public Class ClsDamageMaster

#Region "Variables"
    Public Damage_Code As String = Nothing
    Public Damage_Type As String = Nothing
    Public Damage_Description As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As ClsDamageMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Damage_Description", obj.Damage_Description)
            clsCommon.AddColumnsForChange(coll, "Damage_Type", obj.Damage_Type)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Damage_Code", obj.Damage_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_DAMAGE_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_DAMAGE_MASTER", OMInsertOrUpdate.Update, "TSPL_HR_DAMAGE_MASTER.Damage_Code='" + obj.Damage_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsDamageMaster
        Dim obj As ClsDamageMaster = Nothing
        Dim Arr As List(Of ClsDamageMaster) = Nothing
        Dim qry As String = "select Damage_Code ,Damage_Type,Damage_Description from TSPL_HR_DAMAGE_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_DAMAGE_MASTER.Damage_Code = (select MIN(Damage_Code) from TSPL_HR_DAMAGE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_DAMAGE_MASTER.Damage_Code = (select Max(Damage_Code) from TSPL_HR_DAMAGE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_DAMAGE_MASTER.Damage_Code = (select TOP 1 Damage_Code from TSPL_HR_DAMAGE_MASTER WHERE 1=1 " + whrclas + " and Damage_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_DAMAGE_MASTER.Damage_Code = (select Min(Damage_Code) from TSPL_HR_DAMAGE_MASTER where Damage_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_DAMAGE_MASTER.Damage_Code = (select Max(Damage_Code) from TSPL_HR_DAMAGE_MASTER where Damage_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsDamageMaster()
            obj.Damage_Code = clsCommon.myCstr(dt.Rows(0)("Damage_Code"))
            obj.Damage_Description = clsCommon.myCstr(dt.Rows(0)("Damage_Description"))
            obj.Damage_Type = clsCommon.myCstr(dt.Rows(0)("Damage_Type"))
        End If
        Return obj
    End Function

End Class
