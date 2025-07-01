Imports System.Data.SqlClient
Imports common
Public Class clsCheckList

#Region "Variables"
    Public Chk_Code As String = Nothing
    Public Chk_Description As String = Nothing

#End Region
    Public Shared Function SaveData(ByVal obj As clsCheckList, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "chk_description", obj.Chk_Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "chk_code", obj.Chk_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Check_List", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Check_List", OMInsertOrUpdate.Update, "TSPL_HR_Check_List.chk_code='" + obj.Chk_Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCheckList
        Dim obj As clsCheckList = Nothing
        Dim Arr As List(Of clsCheckList) = Nothing
        Dim qry As String = "select chk_code,chk_description from TSPL_HR_Check_List  where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_Check_List.chk_code = (select MIN(chk_code) from TSPL_HR_Check_List WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_Check_List.chk_code = (select Max(chk_code) from TSPL_HR_Check_List WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_Check_List.chk_code = (select TOP 1 chk_code from TSPL_HR_Check_List WHERE 1=1 " + whrclas + " and chk_code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_Check_List.chk_code = (select Min(chk_code) from TSPL_HR_Check_List where chk_code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_Check_List.chk_code = (select Max(chk_code) from TSPL_HR_Check_List where chk_code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCheckList()
            obj.Chk_Code = clsCommon.myCstr(dt.Rows(0)("chk_code"))
            obj.Chk_Description = clsCommon.myCstr(dt.Rows(0)("chk_description"))
        End If
        Return obj
    End Function
End Class
