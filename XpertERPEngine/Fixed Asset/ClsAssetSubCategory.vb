Imports System.Data.SqlClient
Imports common

Public Class ClsAssetSubCategory
#Region "variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Category As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As ClsAssetSubCategory, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim qry As String = ""

        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Category", obj.Category)
            clsCommon.AddColumnsForChange(coll, "CreateUser", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "ModUser", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "LastMod", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy hh:mm:ss"))
            clsCommon.AddColumnsForChange(coll, "CompCode", objCommonVar.CurrentCompanyCode, True)

            If isNewEntry Then
                obj.Code = ASCAutoGenerate(trans)
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Asset_SubCategory_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Asset_SubCategory_Master", OMInsertOrUpdate.Update, "TSPL_Asset_SubCategory_Master.Code='" + obj.Code + "'", trans)
            End If

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function ASCAutoGenerate(ByVal trans As SqlTransaction) As String
        Dim sql As String = "SELECT MAX(Code) as MaxValue from TSPL_Asset_SubCategory_Master "
        Dim Maxvlu As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql, trans))

        If clsCommon.myLen(Maxvlu) > 0 Then
            Maxvlu = clsCommon.incval(Maxvlu)
        Else
            Maxvlu = "AS0001"
        End If
        Return Maxvlu
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsAssetSubCategory
        Dim obj As ClsAssetSubCategory = Nothing
        Dim Arr As List(Of ClsAssetSubCategory) = Nothing
        Dim qry As String = "select * from TSPL_Asset_SubCategory_Master where 2=2 "
        Dim whrclas As String = " and CompCode = '" + objCommonVar.CurrentCompanyCode + "' "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Asset_SubCategory_Master.Code = (select MIN(Code) from TSPL_Asset_SubCategory_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Asset_SubCategory_Master.Code = (select Max(Code) from TSPL_Asset_SubCategory_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Asset_SubCategory_Master.Code = (select top 1 Code from TSPL_Asset_SubCategory_Master WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Asset_SubCategory_Master.Code = (select Min(Code) from TSPL_Asset_SubCategory_Master where Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Asset_SubCategory_Master.Code = (select Max(Code) from TSPL_Asset_SubCategory_Master where Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAssetSubCategory()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Category = clsCommon.myCstr(dt.Rows(0)("Category"))

        End If
        Return obj
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsAssetSubCategory
        Return GetData(strCode, NavType, Nothing)
    End Function
End Class
