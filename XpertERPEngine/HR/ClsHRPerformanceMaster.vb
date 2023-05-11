'Created by Anubhooti @BM00000000731
'Created on 08/10/2013 
'Table created: TSPL_HR_Performance_Master
'Class used ClsHRPerformanceMaster
Imports System.Data.SqlClient
Imports common
Public Class ClsHRPerformanceMaster
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public PerCat_Code As String = Nothing

    Public Shared Function SaveData(ByVal obj As ClsHRPerformanceMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ClsHRPerformanceMaster.SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                Return True
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsHRPerformanceMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "PERCAT_CODE", obj.PerCat_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Performance_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Performance_Master", OMInsertOrUpdate.Update, "TSPL_HR_Performance_Master.Code='" + obj.Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRPerformanceMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsHRPerformanceMaster
        Dim obj As ClsHRPerformanceMaster = Nothing
        Dim Arr As List(Of ClsHRPerformanceMaster) = Nothing
        Dim qry As String = "select * from TSPL_HR_Performance_Master where 2=2 "
        Dim whrclas As String = " and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_Performance_Master.Code = (select MIN(Code) from TSPL_HR_Performance_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_Performance_Master.Code = (select Max(Code) from TSPL_HR_Performance_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_Performance_Master.Code = (select top 1 Code from TSPL_HR_Performance_Master WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_Performance_Master.Code = (select Min(Code) from TSPL_HR_Performance_Master where Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_Performance_Master.Code = (select Max(Code) from TSPL_HR_Performance_Master where Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHRPerformanceMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            obj.PerCat_Code = clsCommon.myCstr(dt.Rows(0)("PERCAT_CODE"))
        End If
        Return obj
    End Function
    Public Shared Function GetName(ByVal strCode As String) As String
        Dim qry As String = "select Name from TSPL_HR_Performance_Master where 2=2 and Code='" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function
End Class
