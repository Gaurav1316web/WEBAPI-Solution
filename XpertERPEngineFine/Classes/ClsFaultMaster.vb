Imports System.Data.SqlClient
Imports common

Public Class ClsFaultMaster
#Region "Variables"
    Public Fault_Master_Code As String = String.Empty
    Public Fault_Master_Name As String = String.Empty
    Public Fault_Category_Code As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = String.Empty
    Public Modified_Date As Date? = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select Fault_Master_Code AS [Code],Fault_Master_Name AS [Fault Master Name],Fault_Category_Code AS [Fault Category Code],Created_By AS [Created By],CONVERT(varchar,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_SW_FAULT_MASTER  "
        str = clsCommon.ShowSelectForm("SWFauM", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal obj As ClsFaultMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Fault_Master_Name", obj.Fault_Master_Name)
            clsCommon.AddColumnsForChange(coll, "Fault_Category_Code", obj.Fault_Category_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Fault_Master_Code", obj.Fault_Master_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_FAULT_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_FAULT_MASTER", OMInsertOrUpdate.Update, "TSPL_SW_FAULT_MASTER.Fault_Master_Code='" + obj.Fault_Master_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsFaultMaster
        Dim obj As ClsFaultMaster = Nothing
        Dim Arr As List(Of ClsFaultMaster) = Nothing
        Dim qry As String = "SELECT * FROM TSPL_SW_FAULT_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " AND TSPL_SW_FAULT_MASTER.Fault_Master_Code = (select MIN(Fault_Master_Code) FROM TSPL_SW_FAULT_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " AND TSPL_SW_FAULT_MASTER.Fault_Master_Code = (select Max(Fault_Master_Code) FROM TSPL_SW_FAULT_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " AND TSPL_SW_FAULT_MASTER.Fault_Master_Code = (select TOP 1 Fault_Master_Code FROM TSPL_SW_FAULT_MASTER WHERE 1=1 " + whrclas + " AND Fault_Master_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " AND TSPL_SW_FAULT_MASTER.Fault_Master_Code = (select Min(Fault_Master_Code) FROM TSPL_SW_FAULT_MASTER where Fault_Master_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " AND TSPL_SW_FAULT_MASTER.Fault_Master_Code = (select Max(Fault_Master_Code) FROM TSPL_SW_FAULT_MASTER where Fault_Master_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsFaultMaster()
            obj.Fault_Master_Code = clsCommon.myCstr(dt.Rows(0)("Fault_Master_Code"))
            obj.Fault_Master_Name = clsCommon.myCstr(dt.Rows(0)("Fault_Master_Name"))
            obj.Fault_Category_Code = clsCommon.myCstr(dt.Rows(0)("Fault_Category_Code"))
        End If
        Return obj
    End Function
End Class
