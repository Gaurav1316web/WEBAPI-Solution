Imports System.Data.SqlClient

Public Class clsBullShedParameterGroup
    Public Code As String = ""
    Public Name As String = ""
    Public Arr As List(Of clsBullShedParameterGroupDetail) = Nothing


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullShedParameterGroup
        Try
            Dim obj As clsBullShedParameterGroup = Nothing
            Dim qry As String = "SELECT 
                    Code,Name
                    FROM TSPL_BULL_SHED_PARAMETER_MASTER                   
                    WHERE TSPL_BULL_SHED_PARAMETER_MASTER.Code='" + strCode + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBullShedParameterGroup()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))

            End If
            qry = "select Code,PCode from TSPL_BULL_SHED_PARAMETER_Detail WHERE TSPL_BULL_SHED_PARAMETER_Detail.Code='" + strCode + "' "
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsBullShedParameterGroupDetail)
                Dim objTr As clsBullShedParameterGroupDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsBullShedParameterGroupDetail
                    objTr.PCode = clsCommon.myCstr(dr("PCode"))
                    'objTr.PCode = clsCommon.myCstr(dr("PCode"))
                    objTr.Name = clsDBFuncationality.getSingleValue("select TSPL_BULL_SHED_PARAMETER.Name As Name from TSPL_BULL_SHED_PARAMETER left outer join TSPL_BULL_SHED_PARAMETER_Detail on TSPL_BULL_SHED_PARAMETER_Detail.PCODE=TSPL_BULL_SHED_PARAMETER.cODE
 where TSPL_BULL_SHED_PARAMETER_Detail.Code='" + clsCommon.myCstr(dr("Code")) + "'", Nothing)
                    objTr.Type = clsDBFuncationality.getSingleValue("select TSPL_BULL_SHED_PARAMETER.Type As Type from TSPL_BULL_SHED_PARAMETER left outer join TSPL_BULL_SHED_PARAMETER_Detail on TSPL_BULL_SHED_PARAMETER_Detail.PCODE=TSPL_BULL_SHED_PARAMETER.cODE
 where TSPL_BULL_SHED_PARAMETER_Detail.Code='" + clsCommon.myCstr(dr("Code")) + "'", Nothing)

                    obj.Arr.Add(objTr)
                Next
            End If
            Return obj
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = DeleteData(strCode, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(StrCode) <= 0) Then
                Throw New Exception("Code No. not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_BULL_SHED_PARAMETER_Detail where Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BULL_SHED_PARAMETER_MASTER where Code='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsBullShedParameterGroup, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsBullShedParameterGroup, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_BULL_SHED_PARAMETER_MASTER where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullParameterGroup, "", objCommonVar.strCurrUserLocations)
                'If clsCommon.myLen(obj.Code) <= 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_SHED_PARAMETER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_SHED_PARAMETER_MASTER", OMInsertOrUpdate.Update, "TSPL_BULL_SHED_PARAMETER_MASTER.Code='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso clsBullShedParameterGroupDetail.SaveData(clsCommon.myCstr(obj.Code), obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

End Class
Public Class clsBullShedParameterGroupDetail
    Public Code As String = ""
    Public PK_Id As String = ""
    Public PCode As String = ""
    Public Name As String = ""
    Public Type As String = ""

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBullShedParameterGroupDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsBullShedParameterGroupDetail In Arr
                    Dim colm As New Hashtable()
                    clsCommon.AddColumnsForChange(colm, "Code", strDocNo)
                    clsCommon.AddColumnsForChange(colm, "PCode", obj.Code)


                    clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_SHED_PARAMETER_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

    End Function
End Class
