Imports common
Imports System.Data.SqlClient
Imports System.Collections

Public Class ClsBreakDownMaster
#Region "Variables"
    Public Code As String = Nothing
    Public Name As String = Nothing

#End Region

    Public Function SaveData(ByVal obj As ClsBreakDownMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            If isNewEntry = True Then
                Dim qry As String = "Delete from TSPL_BREAK_DOWN_MASTER where Code='" + obj.Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BREAK_DOWN_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_BREAK_DOWN_MASTER", "Code", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BREAK_DOWN_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsBreakDownMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsBreakDownMaster
        Dim obj As ClsBreakDownMaster = Nothing
        Dim qry As String = "SELECT TSPL_BREAK_DOWN_MASTER.* FROM TSPL_BREAK_DOWN_MASTER where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BREAK_DOWN_MASTER.Code=(select MIN(Code) from TSPL_BREAK_DOWN_MASTER  )"
            Case NavigatorType.Last
                qry += " and TSPL_BREAK_DOWN_MASTER.Code=(select Max(Code) from TSPL_BREAK_DOWN_MASTER  )"
            Case NavigatorType.Next
                qry += " and TSPL_BREAK_DOWN_MASTER.Code=(select Min(Code) from TSPL_BREAK_DOWN_MASTER where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_BREAK_DOWN_MASTER.Code=(select Max(Code) from TSPL_BREAK_DOWN_MASTER where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_BREAK_DOWN_MASTER.Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsBreakDownMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
        End If

        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Break Down not found to Delete")
        End If
        Dim obj As ClsBreakDownMaster = ClsBreakDownMaster.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try

                Dim qry As String = "delete from TSPL_BREAK_DOWN_MASTER where Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_BREAK_DOWN_MASTER.CODE from TSPL_BREAK_DOWN_MASTER where TSPL_BREAK_DOWN_MASTER.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class




