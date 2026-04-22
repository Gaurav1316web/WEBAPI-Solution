Imports common
Imports System.Data.SqlClient
Public Class clsSubRouteMaster

#Region "variable"
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public RouteCode As String = Nothing
    Public RouteName As String = Nothing
#End Region
    Public Function SaveData(ByVal SubRoute As clsSubRouteMaster, ByVal isnewentry As Boolean)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select count(*) from TSPL_Sub_Route_Master where Code='" & SubRoute.Code & "'"
            Dim isexist As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If isexist = 0 Then
                isnewentry = True
            Else
                isnewentry = False
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", SubRoute.Code)
            clsCommon.AddColumnsForChange(coll, "Name", SubRoute.Name)
            clsCommon.AddColumnsForChange(coll, "Route_Code", SubRoute.RouteCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isnewentry Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sub_Route_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sub_Route_Master ", OMInsertOrUpdate.Update, " Code='" & SubRoute.Code & "'", trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode > 0) Then
                Dim qry As String = "delete from TSPL_Sub_Route_Master where code='" & strCode & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal navigatortype As NavigatorType) As clsSubRouteMaster
        Try
            Dim obj As clsSubRouteMaster = Nothing
            Dim qst As String = "select Code,Name,Route_Code from TSPL_Sub_Route_Master where 2=2 "
            Select Case navigatortype
                Case NavigatorType.Current
                    qst += "and Code in ('" & strCode & "')"
                Case NavigatorType.Next
                    qst += "and Code in (select  min(Code) from TSPL_Sub_Route_Master where Code >'" & strCode & "')"
                Case NavigatorType.First
                    qst += "and Code in (select MIN(Code) from TSPL_Sub_Route_Master where 1=1)"
                Case NavigatorType.Last
                    qst += "and Code in (select Max(Code) from TSPL_Sub_Route_Master where 1=1)"
                Case NavigatorType.Previous
                    qst += "and Code in (select  max(Code) from TSPL_Sub_Route_Master where Code <'" & strCode & "')"

            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New clsSubRouteMaster()
                obj.Code = clsCommon.myCstr(dt1.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt1.Rows(0)("Name"))
                obj.RouteCode = clsCommon.myCstr(dt1.Rows(0)("Route_Code"))
                obj.RouteName = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER Where Route_No='" & obj.RouteCode & "'")
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
