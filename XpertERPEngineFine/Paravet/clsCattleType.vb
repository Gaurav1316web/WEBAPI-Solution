Imports System.Data.SqlClient
Imports common
Public Class clsCattleType
#Region "Variables"
    Public Cattle_Type_Code As String = Nothing
    Public Cattle_Type_Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
#End Region
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Cattle_Type_Code as [Code], Cattle_Type_Name AS [Description],Created_By as [Created By] ,Convert(varchar,Created_Date,103) as [Created Date] ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date]  from TSPL_CATTLE_TYPE_MASTER "
        str = clsCommon.ShowSelectForm("CATTLETYPE", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsCattleType, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Cattle_Type_Name", obj.Cattle_Type_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Cattle_Type_Code", obj.Cattle_Type_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_TYPE_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_TYPE_MASTER", OMInsertOrUpdate.Update, "TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Code='" + obj.Cattle_Type_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCattleType
        Dim obj As clsCattleType = Nothing
        Dim Arr As List(Of clsCattleType) = Nothing
        Dim qry As String = " select Cattle_Type_Code as [Code], Cattle_Type_Name as  [Name] from TSPL_CATTLE_TYPE_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Code = (select MIN(Cattle_Type_Code) from TSPL_CATTLE_TYPE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Code  = (select Max(Cattle_Type_Code) from TSPL_CATTLE_TYPE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Code  = (select TOP 1 Cattle_Type_Code from TSPL_CATTLE_TYPE_MASTER WHERE 1=1 " + whrclas + " and Cattle_Type_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Code  = (select Min(Cattle_Type_Code) from TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CATTLE_TYPE_MASTER.Cattle_Type_Code  = (select Max(Cattle_Type_Code) from TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCattleType()
            obj.Cattle_Type_Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Cattle_Type_Name = clsCommon.myCstr(dt.Rows(0)("Name"))
        End If
        Return obj
    End Function
End Class


