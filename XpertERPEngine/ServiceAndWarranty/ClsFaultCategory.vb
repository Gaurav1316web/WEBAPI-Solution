Imports System.Data.SqlClient
Imports common

Public Class ClsFaultCategory
#Region "Variables"
    Public Fault_Category_Code As String = Nothing
    Public Fault_Category_Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsFaultCategory, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Fault_Category_Name", obj.Fault_Category_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Fault_Category_Code", obj.Fault_Category_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_FAULT_CATEGORY_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_FAULT_CATEGORY_MASTER", OMInsertOrUpdate.Update, "TSPL_SW_FAULT_CATEGORY_MASTER.Fault_Category_Code='" + obj.Fault_Category_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsFaultCategory
        Dim obj As ClsFaultCategory = Nothing
        Dim Arr As List(Of ClsFaultCategory) = Nothing
        Dim qry As String = "select Fault_Category_Code ,Fault_Category_Name from TSPL_SW_FAULT_CATEGORY_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SW_FAULT_CATEGORY_MASTER.Fault_Category_Code = (select MIN(Fault_Category_Code) from TSPL_SW_FAULT_CATEGORY_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_SW_FAULT_CATEGORY_MASTER.Fault_Category_Code = (select Max(Fault_Category_Code) from TSPL_SW_FAULT_CATEGORY_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_SW_FAULT_CATEGORY_MASTER.Fault_Category_Code = (select TOP 1 Fault_Category_Code from TSPL_SW_FAULT_CATEGORY_MASTER WHERE 1=1 " + whrclas + " and Fault_Category_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_SW_FAULT_CATEGORY_MASTER.Fault_Category_Code = (select Min(Fault_Category_Code) from TSPL_SW_FAULT_CATEGORY_MASTER where Fault_Category_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SW_FAULT_CATEGORY_MASTER.Fault_Category_Code = (select Max(Fault_Category_Code) from TSPL_SW_FAULT_CATEGORY_MASTER where Fault_Category_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsFaultCategory()
            obj.Fault_Category_Code = clsCommon.myCstr(dt.Rows(0)("Fault_Category_Code"))
            obj.Fault_Category_Name = clsCommon.myCstr(dt.Rows(0)("Fault_Category_Name"))
        End If
        Return obj
    End Function
End Class
