Imports common
Imports System.Data.SqlClient
Imports System.Collections

Public Class ClsCompetitorMaster
#Region "Variables"
    Public Code As String = Nothing
    Public Name As String = Nothing

#End Region

    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select TSPL_COMPETITOR_MASTER.Name  from TSPL_COMPETITOR_MASTER where Code='" + strCode + "' "
        Return clsDBFuncationality.getSingleValue(qry, trans)
    End Function

    Public Shared Function getFinder(ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "SELECT Code,Name FROM TSPL_COMPETITOR_MASTER"

        str = clsCommon.ShowSelectForm("COMPET1", qry, "Code", "", curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Function SaveData(ByVal obj As ClsCompetitorMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean ', Optional ByVal import As Boolean = False, Optional ByVal isMakeAbandomentNo As Boolean = False
        Dim isSaved As Boolean = True
        Dim qry As String = ""
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try


            If isNewEntry = True Then
                qry = "Delete from TSPL_COMPETITOR_MASTER where Code='" + obj.Code + "'"
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
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_COMPETITOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_COMPETITOR_MASTER", "Code", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_COMPETITOR_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            'trans.Commit()
        Catch err As Exception
            'trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsCompetitorMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsCompetitorMaster
        Dim obj As ClsCompetitorMaster = Nothing
        Dim qry As String = "SELECT TSPL_COMPETITOR_MASTER.* FROM TSPL_COMPETITOR_MASTER where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_COMPETITOR_MASTER.Code=(select MIN(Code) from TSPL_COMPETITOR_MASTER  )"
            Case NavigatorType.Last
                qry += " and TSPL_COMPETITOR_MASTER.Code=(select Max(Code) from TSPL_COMPETITOR_MASTER  )"
            Case NavigatorType.Next
                qry += " and TSPL_COMPETITOR_MASTER.Code=(select Min(Code) from TSPL_COMPETITOR_MASTER where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_COMPETITOR_MASTER.Code=(select Max(Code) from TSPL_COMPETITOR_MASTER where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_COMPETITOR_MASTER.Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsCompetitorMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
        End If

        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        Dim qry As String = ""
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Competitor not found to Delete")
        End If
        Dim obj As ClsCompetitorMaster = ClsCompetitorMaster.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try


                qry = "delete from TSPL_COMPETITOR_MASTER where Code='" + strCode + "'"
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
        Dim qry As String = "select TSPL_COMPETITOR_MASTER.CODE from TSPL_COMPETITOR_MASTER where TSPL_COMPETITOR_MASTER.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class




