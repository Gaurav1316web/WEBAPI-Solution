Imports System.Data.SqlClient
Imports common
Imports System.Net.Mail
Imports System.Net

Public Class ClsAssetDetails
    Public Asset_Code As String
    Public Asset_Description As String
    Public Asset_Specification As String
    Public Asset_Type_Code As String
    Public Asset_Sub_Category As String
    Public Company As String
    Public Serial_No As String
    Public Date_Of_Purchase As Date
    Public CREATE_BY As String
    Public CREATE_DATE As DateTime
    Public MODIFY_BY As String
    Public MODIFY_DATE As DateTime
    Public FILENAME As String
    Public Shared Function SaveData(ByVal obj As ClsAssetDetails, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Asset_Description", obj.Asset_Description)
            clsCommon.AddColumnsForChange(coll, "Asset_Specification", obj.Asset_Specification)
            clsCommon.AddColumnsForChange(coll, "Asset_Type_Code", obj.Asset_Type_Code)
            clsCommon.AddColumnsForChange(coll, "Asset_Sub_Category", obj.Asset_Sub_Category, True)
            clsCommon.AddColumnsForChange(coll, "Company", obj.Company)
            clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No)
            clsCommon.AddColumnsForChange(coll, "Date_Of_Purchase", clsCommon.GetPrintDate(obj.Date_Of_Purchase, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "CREATE_BY", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "CREATE_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "MODIFY_BY", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "MODIFY_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)

            If isNewEntry Then
                obj.Asset_Code = ACAutogenerate(trans)
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Asset_Details", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Asset_Details", OMInsertOrUpdate.Update, "TSPL_Asset_Details.Asset_Code='" + obj.Asset_Code + "'", trans)
            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function ACAutogenerate(ByVal trans As SqlTransaction) As String
        Dim sql As String = "SELECT MAX(Asset_Code) as MaxValue from TSPL_Asset_Details"
        Dim Maxvlu As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql, trans))
        If clsCommon.myLen(Maxvlu) > 0 Then
            Maxvlu = clsCommon.incval(Maxvlu)
        Else
            Maxvlu = "AC0001"
        End If
        Return Maxvlu
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsAssetDetails
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsAssetDetails
        Dim obj As ClsAssetDetails = Nothing
        Dim Arr As List(Of ClsAssetDetails) = Nothing
        Dim qry As String = "select * From TSPL_Asset_Details where 2=2"
        Dim whrclas As String = " and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Asset_Details.Asset_Code = (select MIN(Asset_Code) from TSPL_Asset_Details WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Asset_Details.Asset_Code = (select Max(Asset_Code) from TSPL_Asset_Details WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Asset_Details.Asset_Code = (select top 1 Asset_Code from TSPL_Asset_Details WHERE 1=1 " + whrclas + " and Asset_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Asset_Details.Asset_Code = (select Min(Asset_Code) from TSPL_Asset_Details where Asset_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Asset_Details.Asset_Code = (select Max(Asset_Code) from TSPL_Asset_Details where Asset_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAssetDetails()
            obj.Asset_Code = clsCommon.myCstr(dt.Rows(0)("Asset_Code"))
            obj.Asset_Description = clsCommon.myCstr(dt.Rows(0)("Asset_Description"))
            obj.Asset_Specification = clsCommon.myCstr(dt.Rows(0)("Asset_Specification"))
            obj.Asset_Type_Code = clsCommon.myCstr(dt.Rows(0)("Asset_Type_Code"))
            obj.Asset_Sub_Category = clsCommon.myCstr(dt.Rows(0)("Asset_Sub_Category"))
            obj.Company = clsCommon.myCstr(dt.Rows(0)("Company"))
            obj.Serial_No = clsCommon.myCstr(dt.Rows(0)("Serial_No"))
            obj.Date_Of_Purchase = clsCommon.myCstr(dt.Rows(0)("Date_Of_Purchase"))
            obj.FILENAME = clsCommon.myCstr(dt.Rows(0)("FILENAME"))
        End If
        Return obj
    End Function
End Class
