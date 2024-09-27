Imports System.Data.SqlClient
Imports common

Public Class ClsSolutionKnowledgeBase
#Region "Variables"
    Public Document_Code As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Updated_By As String = String.Empty
    Public Updated_On As String = String.Empty
    Public Solution As String = String.Empty
    Public Symptom As String = String.Empty
    Public Cause As String = String.Empty
    Public Remarks As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = String.Empty
    Public Modified_Date As Date? = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select Document_Code AS [Code],Item_Code AS [Item Code],Updated_By As [Updated By],Updated_On AS [Updated On],Solution,Symptom,Created_By AS [Created By],CONVERT(varchar,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_SW_SOLUTION_KNOWLEDGE_BASE  "
        str = clsCommon.ShowSelectForm("SWSKnowB", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal obj As ClsSolutionKnowledgeBase, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Updated_By", obj.Updated_By)
            clsCommon.AddColumnsForChange(coll, "Updated_On", obj.Updated_On)
            clsCommon.AddColumnsForChange(coll, "Solution", obj.Solution)
            clsCommon.AddColumnsForChange(coll, "Symptom", obj.Symptom)
            clsCommon.AddColumnsForChange(coll, "Cause", obj.Cause)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SOLUTION_KNOWLEDGE_BASE", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SOLUTION_KNOWLEDGE_BASE", OMInsertOrUpdate.Update, "TSPL_SW_SOLUTION_KNOWLEDGE_BASE.Document_Code='" + obj.Document_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsSolutionKnowledgeBase
        Dim obj As ClsSolutionKnowledgeBase = Nothing
        Dim Arr As List(Of ClsSolutionKnowledgeBase) = Nothing
        Dim qry As String = "SELECT * FROM TSPL_SW_SOLUTION_KNOWLEDGE_BASE where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " AND TSPL_SW_SOLUTION_KNOWLEDGE_BASE.Document_Code = (select MIN(Document_Code) FROM TSPL_SW_SOLUTION_KNOWLEDGE_BASE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " AND TSPL_SW_SOLUTION_KNOWLEDGE_BASE.Document_Code = (select Max(Document_Code) FROM TSPL_SW_SOLUTION_KNOWLEDGE_BASE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " AND TSPL_SW_SOLUTION_KNOWLEDGE_BASE.Document_Code = (select TOP 1 Document_Code FROM TSPL_SW_SOLUTION_KNOWLEDGE_BASE WHERE 1=1 " + whrclas + " AND Document_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " AND TSPL_SW_SOLUTION_KNOWLEDGE_BASE.Document_Code = (select Min(Document_Code) FROM TSPL_SW_SOLUTION_KNOWLEDGE_BASE where Document_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " AND TSPL_SW_SOLUTION_KNOWLEDGE_BASE.Document_Code = (select Max(Document_Code) FROM TSPL_SW_SOLUTION_KNOWLEDGE_BASE where Document_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsSolutionKnowledgeBase()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Updated_By = clsCommon.myCstr(dt.Rows(0)("Updated_By"))
            obj.Updated_On = clsCommon.myCstr(dt.Rows(0)("Updated_On"))
            obj.Solution = clsCommon.myCstr(dt.Rows(0)("Solution"))
            obj.Symptom = clsCommon.myCstr(dt.Rows(0)("Symptom"))
            obj.Cause = clsCommon.myCstr(dt.Rows(0)("Cause"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
        End If
        Return obj
    End Function
End Class
