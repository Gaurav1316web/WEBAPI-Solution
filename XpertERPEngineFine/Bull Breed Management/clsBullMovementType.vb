Imports System.Data.SqlClient
Public Class clsBullMovementType
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public Peridocity As String = Nothing


    Public Function SaveData(ByVal obj As clsBullMovementType, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsBullMovementType, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_BULL_MOVEMENT_TYPE where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Peridocity", obj.Peridocity)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                '    obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullTestParameter, "", objCommonVar.strCurrUserLocations)
                '    If clsCommon.myLen(obj.Code) <= 0 Then
                '        Throw New Exception("Error in Code Generation")
                '    End If
                '    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MOVEMENT_TYPE", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MOVEMENT_TYPE", OMInsertOrUpdate.Update, "TSPL_BULL_MOVEMENT_TYPE.Code='" + obj.Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullMovementType
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsBullMovementType = GetData(strCode, NavType, trans)
            trans.Commit()

            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBullMovementType
        Dim obj As clsBullMovementType = Nothing

        Try
            Dim strQry As String = "select * from TSPL_BULL_MOVEMENT_TYPE where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Code = (select MIN(Code) from TSPL_BULL_MOVEMENT_TYPE where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Code = (Select Max(Code) from TSPL_BULL_MOVEMENT_TYPE where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Code = (Select Min(Code) from TSPL_BULL_MOVEMENT_TYPE where Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Code = (select Max(Code) from TSPL_BULL_MOVEMENT_TYPE where Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBullMovementType()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.Peridocity = clsCommon.myCstr(dt.Rows(0)("Peridocity"))

            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BULL_MOVEMENT_TYPE where code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getBullMovementTypeQuery(Optional ByVal WhrCls As String = "") As DataTable
        Dim dt As DataTable = New DataTable()

        Dim qry As String = " SELECT '' AS Code,'Select' as Name union SELECT Code , Name  FROM TSPL_BULL_MOVEMENT_TYPE WHERE 1=1 " + WhrCls
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
End Class
