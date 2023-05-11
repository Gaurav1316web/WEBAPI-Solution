Imports System.Data.SqlClient
Imports common

Public Class clsDesignationHierarcy
#Region "variables"
    Public DesignationCode As String = Nothing
    Public Level As String = Nothing
    Public HigherDesignationCode As String = Nothing
#End Region


    Public Function SaveData(ByVal obj As clsDesignationHierarcy, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Designation_Code", obj.DesignationCode)
            ' clsCommon.AddColumnsForChange(coll, "Level_Code", obj.Level)
            clsCommon.AddColumnsForChange(coll, "Higher_Designation_Code", obj.HigherDesignationCode)

            ' clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
            clsCommon.AddColumnsForChange(coll, "Posted", "N")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            Dim sQuery As String = "select count(*) from TSPL_Designation_Master_Hierarchy where Designation_Code='" + obj.DesignationCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
            If check <= 0 Then
                isNewEntry = True
            Else
                isNewEntry = False
            End If


            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Designation_Master_Hierarchy", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Designation_Master_Hierarchy", OMInsertOrUpdate.Update, "TSPL_Designation_Master_Hierarchy.Designation_Code='" + obj.DesignationCode + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strMOMCode As String, ByVal NavType As NavigatorType) As clsDesignationHierarcy
        Return GetData(strMOMCode, NavType, "", Nothing)
    End Function

    Public Shared Function GetData(ByVal strMOMCode As String, ByVal NavType As NavigatorType, ByVal fromScreen As String, ByVal trans As SqlTransaction) As clsDesignationHierarcy
        Dim obj As clsDesignationHierarcy = Nothing
        Dim Arr As List(Of clsDesignationHierarcy) = Nothing
        Dim qry As String = "select TSPL_Designation_Master_Hierarchy.* from TSPL_Designation_Master_Hierarchy where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Designation_Master_Hierarchy.Designation_code = (select MIN(Designation_code) from TSPL_Designation_Master_Hierarchy WHERE 1=1 and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'  )"
            Case NavigatorType.Last
                qry += " and TSPL_Designation_Master_Hierarchy.Designation_code = (select Max(Designation_code) from TSPL_Designation_Master_Hierarchy WHERE 1=1 and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_Designation_Master_Hierarchy.Designation_code = (select top 1 Designation_code from TSPL_Designation_Master_Hierarchy WHERE 1=1  and Designation_code='" + strMOMCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
            Case NavigatorType.Next
                qry += " and TSPL_Designation_Master_Hierarchy.Designation_code = (select Min(Designation_code) from TSPL_Designation_Master_Hierarchy where Designation_code>'" + strMOMCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_Designation_Master_Hierarchy.Designation_code = (select Max(Designation_code) from TSPL_Designation_Master_Hierarchy where Designation_code<'" + strMOMCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDesignationHierarcy()
            obj.DesignationCode = clsCommon.myCstr(dt.Rows(0)("Designation_code"))
            obj.Level = clsCommon.myCstr(dt.Rows(0)("Level_Code"))
            obj.HigherDesignationCode = clsCommon.myCstr(dt.Rows(0)("Higher_Designation_Code"))
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal StrCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Fiscal_Name from TSPL_Designation_Master_Hierarchy Where Designation_code='" + StrCode + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
