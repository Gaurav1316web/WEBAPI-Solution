Imports System.Data.SqlClient
Imports common
Public Class clsCattleColor
#Region "Variables"
    Public Cattle_Color_Code As String = Nothing
    Public Cattle_Color_Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Cattle_Color_Code as [Code], Cattle_Color_Name AS [Description],Created_By as [Created By] ,Convert(varchar,Created_Date,103) as [Created Date] ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date]  from TSPL_CATTLE_COLOR_MASTER "
        str = clsCommon.ShowSelectForm("BREDTYPE", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsCattleColor, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Cattle_Color_Name", obj.Cattle_Color_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Cattle_Color_Code", obj.Cattle_Color_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_COLOR_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_COLOR_MASTER", OMInsertOrUpdate.Update, "TSPL_CATTLE_COLOR_MASTER.Cattle_Color_Code='" + obj.Cattle_Color_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    'select Cattle_Color_Code,Cattle_Color_Name,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code from TSPL_CATTLE_COLOR_MASTER
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCattleColor
        Dim obj As clsCattleColor = Nothing
        Dim Arr As List(Of clsCattleColor) = Nothing
        Dim qry As String = " select Cattle_Color_Code as [Code], Cattle_Color_Name as  [Name] from TSPL_CATTLE_COLOR_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CATTLE_COLOR_MASTER.Cattle_Color_Code = (select MIN(Cattle_Color_Code) from TSPL_CATTLE_COLOR_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_CATTLE_COLOR_MASTER.Cattle_Color_Code  = (select Max(Cattle_Color_Code) from TSPL_CATTLE_COLOR_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_CATTLE_COLOR_MASTER.Cattle_Color_Code  = (select TOP 1 Cattle_Color_Code from TSPL_CATTLE_COLOR_MASTER WHERE 1=1 " + whrclas + " and Cattle_Color_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_CATTLE_COLOR_MASTER.Cattle_Color_Code  = (select Min(Cattle_Color_Code) from TSPL_CATTLE_COLOR_MASTER where Cattle_Color_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CATTLE_COLOR_MASTER.Cattle_Color_Code  = (select Max(Cattle_Color_Code) from TSPL_CATTLE_COLOR_MASTER where Cattle_Color_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCattleColor()
            obj.Cattle_Color_Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Cattle_Color_Name = clsCommon.myCstr(dt.Rows(0)("Name"))
        End If
        Return obj
    End Function
End Class


