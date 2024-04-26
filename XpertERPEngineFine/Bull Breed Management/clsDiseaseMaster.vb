Imports common
Imports System.Data.SqlClient
Public Class clsDiseaseMaster
#Region "Variables"
    Public Code As String = ""
    Public Name As String = ""
#End Region

    Public Shared Function SaveData(ByVal obj As clsDiseaseMaster, ByVal isNewEntry As Boolean) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Disease_Master", OMInsertOrUpdate.Insert, "", Nothing)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Disease_Master", OMInsertOrUpdate.Update, "  Code='" + obj.Code + "'", Nothing)
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDiseaseMaster
        Try
            Dim obj As New clsDiseaseMaster()
            Dim Qry As String = "Select Code,Name From TSPL_Disease_Master "
            Select Case NavType
                Case NavigatorType.Current
                    Qry += " where TSPL_Disease_Master.Code='" + strCode + "'"
                Case NavigatorType.First
                    Qry += " where TSPL_Disease_Master.Code in (select min(Code) from TSPL_Disease_Master )"
                Case NavigatorType.Last
                    Qry += " where TSPL_Disease_Master.Code in (select max(Code) from TSPL_Disease_Master )"
                Case NavigatorType.Next
                    Qry += "where TSPL_Disease_Master.Code in (select min(Code) from TSPL_Disease_Master where  Code>'" + strCode + "')"
                Case NavigatorType.Previous
                    Qry += " where TSPL_Disease_Master.Code in (select max(Code) from TSPL_Disease_Master Where Code <'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Try
            Dim Qry As String = "Delete from TSPL_Disease_Master Where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


End Class
