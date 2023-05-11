Imports System.Data.SqlClient
Imports common


Public Class ClsRelationMaster
#Region "Variables"
    Public Relation_Code As String = Nothing
    Public Relation_Name As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsRelationMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Relation_Name", obj.Relation_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Relation_Code", obj.Relation_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_RELATION_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_RELATION_MASTER", OMInsertOrUpdate.Update, "TSPL_HR_RELATION_MASTER.Relation_Code='" + obj.Relation_Code + "'")
            End If
            'trans.Commit()
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    'Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsRelationMaster
    '    Return GetData(strCode, NavType, Nothing)
    'End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsRelationMaster
        Dim obj As ClsRelationMaster = Nothing
        Dim Arr As List(Of ClsRelationMaster) = Nothing
        Dim qry As String = "select Relation_Code ,Relation_Name from TSPL_HR_RELATION_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_RELATION_MASTER.Relation_Code = (select MIN(Relation_Code) from TSPL_HR_RELATION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_RELATION_MASTER.Relation_Code = (select Max(Relation_Code) from TSPL_HR_RELATION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_RELATION_MASTER.Relation_Code = (select TOP 1 Relation_Code from TSPL_HR_RELATION_MASTER WHERE 1=1 " + whrclas + " and Relation_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_RELATION_MASTER.Relation_Code = (select Min(Relation_Code) from TSPL_HR_RELATION_MASTER where Relation_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_RELATION_MASTER.Relation_Code = (select Max(Relation_Code) from TSPL_HR_RELATION_MASTER where Relation_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsRelationMaster()
            obj.Relation_Code = clsCommon.myCstr(dt.Rows(0)("Relation_Code"))
            obj.Relation_Name = clsCommon.myCstr(dt.Rows(0)("Relation_Name"))
        End If
        Return obj
    End Function
End Class
