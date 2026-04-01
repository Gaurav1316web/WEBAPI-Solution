Imports System.Data.SqlClient
Imports common
Public Class ClsBMCCategory

#Region "variables"
    Public Document_Code As String = Nothing
    Public Description As String = Nothing
    Public Document_Date As Date
    Public End_Date As Date
    Public Is_Current_Year As Integer = 0
#End Region

    Public Function SaveData(ByVal obj As ClsBMCCategory, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.Document_Date), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BMC_Category_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BMC_Category_Master", OMInsertOrUpdate.Update, "TSPL_BMC_Category_Master.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strMOMCode As String, ByVal NavType As NavigatorType) As ClsBMCCategory
        Return GetData(strMOMCode, NavType, "", Nothing)
    End Function

    Public Shared Function GetData(ByVal strMOMCode As String, ByVal NavType As NavigatorType, ByVal fromScreen As String, ByVal trans As SqlTransaction) As ClsBMCCategory
        Dim obj As ClsBMCCategory = Nothing
        Dim Arr As List(Of ClsBMCCategory) = Nothing
        Dim qry As String = "select TSPL_BMC_Category_Master.* from TSPL_BMC_Category_Master where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BMC_Category_Master.Document_Code = (select MIN(Document_Code) from TSPL_BMC_Category_Master WHERE 1=1   )"
            Case NavigatorType.Last
                qry += " and TSPL_BMC_Category_Master.Document_Code = (select Max(Document_Code) from TSPL_BMC_Category_Master WHERE 1=1  )"
            Case NavigatorType.Current
                qry += " and TSPL_BMC_Category_Master.Document_Code = (select top 1 Document_Code from TSPL_BMC_Category_Master WHERE 1=1  and Document_Code='" + strMOMCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_BMC_Category_Master.Document_Code = (select Min(Document_Code) from TSPL_BMC_Category_Master where Document_Code>'" + strMOMCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_BMC_Category_Master.Document_Code = (select Max(Document_Code) from TSPL_BMC_Category_Master where Document_Code<'" + strMOMCode + "' )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsBMCCategory()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))

        End If
        Return obj
    End Function
End Class
