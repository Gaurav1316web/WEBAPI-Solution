Imports System.Data.SqlClient
Imports common


Public Class ClsAssetType
#Region "variables"
    Public Asset_Type_Code As String = Nothing
    Public Asset_Type_Description As String = Nothing
    Public Asset_Type As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As ClsAssetType, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim qry As String = ""

        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Asset_Type_Description", obj.Asset_Type_Description)
            clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.Asset_Type)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Asset_Type_Code", obj.Asset_Type_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Asset_Type_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Asset_Type_Master", OMInsertOrUpdate.Update, "TSPL_Asset_Type_Master.Asset_Type_Code='" + obj.Asset_Type_Code + "'", trans)
            End If

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsAssetType
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsAssetType
        Dim obj As ClsAssetType = Nothing
        Dim Arr As List(Of ClsAssetType) = Nothing
        Dim qry As String = "select * from TSPL_Asset_Type_Master where 2=2 "
        Dim whrclas As String = " and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Asset_Type_Master.Asset_Type_Code = (select MIN(Asset_Type_Code) from TSPL_Asset_Type_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Asset_Type_Master.Asset_Type_Code = (select Max(Asset_Type_Code) from TSPL_Asset_Type_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Asset_Type_Master.Asset_Type_Code = (select top 1 Asset_Type_Code from TSPL_Asset_Type_Master WHERE 1=1 " + whrclas + " and Asset_Type_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Asset_Type_Master.Asset_Type_Code = (select Min(Asset_Type_Code) from TSPL_Asset_Type_Master where Asset_Type_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Asset_Type_Master.Asset_Type_Code = (select Max(Asset_Type_Code) from TSPL_Asset_Type_Master where Asset_Type_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAssetType()
            obj.Asset_Type_Code = clsCommon.myCstr(dt.Rows(0)("Asset_Type_Code"))
            obj.Asset_Type_Description = clsCommon.myCstr(dt.Rows(0)("Asset_Type_Description"))
            obj.Asset_Type = clsCommon.myCstr(dt.Rows(0)("Asset_Type"))

        End If
        Return obj
    End Function
End Class
