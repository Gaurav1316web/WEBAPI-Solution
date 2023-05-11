Imports common
Imports System.Data.SqlClient
Imports System.Collections

Public Class ClsProfitCenterMaster
#Region "Variables"
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public arrCenterList As ArrayList = Nothing

#End Region

    Public Function SaveData(ByVal obj As ClsProfitCenterMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean ', Optional ByVal import As Boolean = False, Optional ByVal isMakeAbandomentNo As Boolean = False
        Dim isSaved As Boolean = True
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try


            Dim qry As String = "delete from TSPL_PROFIT_CENTER_MAPPING where ProfitCenter_Code='" + obj.Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry = True Then
                qry = "Delete from TSPL_PROFIT_CENTER_MASTER where Code='" + obj.Code + "'"
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
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROFIT_CENTER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_PROFIT_CENTER_MASTER", "Code", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROFIT_CENTER_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsProfitCenterMapping.SaveData(obj.Code, obj.arrCenterList, trans)
            'trans.Commit()
        Catch err As Exception
            'trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsProfitCenterMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsProfitCenterMaster
        Dim obj As ClsProfitCenterMaster = Nothing
        Dim qry As String = "SELECT TSPL_PROFIT_CENTER_MASTER.* FROM TSPL_PROFIT_CENTER_MASTER where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PROFIT_CENTER_MASTER.Code=(select MIN(Code) from TSPL_PROFIT_CENTER_MASTER  )"
            Case NavigatorType.Last
                qry += " and TSPL_PROFIT_CENTER_MASTER.Code=(select Max(Code) from TSPL_PROFIT_CENTER_MASTER  )"
            Case NavigatorType.Next
                qry += " and TSPL_PROFIT_CENTER_MASTER.Code=(select Min(Code) from TSPL_PROFIT_CENTER_MASTER where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_PROFIT_CENTER_MASTER.Code=(select Max(Code) from TSPL_PROFIT_CENTER_MASTER where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_PROFIT_CENTER_MASTER.Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsProfitCenterMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))


            Dim templist As New ArrayList
            qry = "SELECT TSPL_PROFIT_CENTER_MAPPING.* FROM TSPL_PROFIT_CENTER_MAPPING  where TSPL_PROFIT_CENTER_MAPPING.ProfitCenter_Code='" + obj.Code + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    templist.Add(clsCommon.myCstr(dr("CostCenter_Code")))
                Next
            End If
            obj.arrCenterList = templist
        End If

        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Profit Center not found to Delete")
        End If
        Dim obj As ClsProfitCenterMaster = ClsProfitCenterMaster.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try
                
                Dim qry As String = "delete from TSPL_PROFIT_CENTER_MAPPING where ProfitCenter_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PROFIT_CENTER_MASTER where Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

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
        Dim qry As String = "select TSPL_PROFIT_CENTER_MASTER.CODE from TSPL_PROFIT_CENTER_MASTER where TSPL_PROFIT_CENTER_MASTER.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class

Public Class clsProfitCenterMapping
#Region "Variables"
    Public Center_Code As String = ""
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As ArrayList, ByVal tran As SqlTransaction) As Boolean
        Try

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i As Integer = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "ProfitCenter_Code", DocNo)
                    clsCommon.AddColumnsForChange(coll, "CostCenter_Code", arr.Item(i))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROFIT_CENTER_MAPPING", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class



