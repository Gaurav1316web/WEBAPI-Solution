Imports System.Data.SqlClient
Imports common

Public Class ClsActivityType
#Region "Variables"
    Public Activity_Type_Code As String = String.Empty
    Public Activity_Type_Name As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = String.Empty
    Public Modified_Date As Date? = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select Activity_Type_Code AS [Code],Activity_Type_Name AS [Call Type Name],Created_By AS [Created By],CONVERT(varchar,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_SW_ACTIVITY_TYPE_MASTER  "
        str = clsCommon.ShowSelectForm("SWActT", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal obj As ClsActivityType, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Activity_Type_Name", obj.Activity_Type_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Activity_Type_Code", obj.Activity_Type_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_ACTIVITY_TYPE_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_ACTIVITY_TYPE_MASTER", OMInsertOrUpdate.Update, "TSPL_SW_ACTIVITY_TYPE_MASTER.Activity_Type_Code='" + obj.Activity_Type_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsActivityType
        Dim obj As ClsActivityType = Nothing
        Dim Arr As List(Of ClsActivityType) = Nothing
        Dim qry As String = "SELECT * FROM TSPL_SW_ACTIVITY_TYPE_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " AND TSPL_SW_ACTIVITY_TYPE_MASTER.Activity_Type_Code = (select MIN(Activity_Type_Code) FROM TSPL_SW_ACTIVITY_TYPE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " AND TSPL_SW_ACTIVITY_TYPE_MASTER.Activity_Type_Code = (select Max(Activity_Type_Code) FROM TSPL_SW_ACTIVITY_TYPE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " AND TSPL_SW_ACTIVITY_TYPE_MASTER.Activity_Type_Code = (select TOP 1 Activity_Type_Code FROM TSPL_SW_ACTIVITY_TYPE_MASTER WHERE 1=1 " + whrclas + " AND Activity_Type_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " AND TSPL_SW_ACTIVITY_TYPE_MASTER.Activity_Type_Code = (select Min(Activity_Type_Code) FROM TSPL_SW_ACTIVITY_TYPE_MASTER where Activity_Type_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " AND TSPL_SW_ACTIVITY_TYPE_MASTER.Activity_Type_Code = (select Max(Activity_Type_Code) FROM TSPL_SW_ACTIVITY_TYPE_MASTER where Activity_Type_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsActivityType()
            obj.Activity_Type_Code = clsCommon.myCstr(dt.Rows(0)("Activity_Type_Code"))
            obj.Activity_Type_Name = clsCommon.myCstr(dt.Rows(0)("Activity_Type_Name"))
        End If
        Return obj
    End Function
End Class
