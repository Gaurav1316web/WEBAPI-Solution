Imports System.Data.SqlClient
Public Class clsBullMovement
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Status As Integer = 0
    Public Bull_Code As String = Nothing
    Public Bull_Shed As String = Nothing
    Public Bull_Movement_Type As String = Nothing
    Public Perid As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsBullMovement, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsBullMovement, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_BULL_MOVEMENT where Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", obj.Document_Date)
            clsCommon.AddColumnsForChange(coll, "Bull_Code", obj.Bull_Code)
            clsCommon.AddColumnsForChange(coll, "Bull_Movement_Type", obj.Bull_Movement_Type)
            clsCommon.AddColumnsForChange(coll, "Bull_Shed", obj.Bull_Shed)
            clsCommon.AddColumnsForChange(coll, "Perid", obj.Perid)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.frmBullMovement, "", "")
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MOVEMENT", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MOVEMENT", OMInsertOrUpdate.Update, "TSPL_BULL_MOVEMENT.Document_Code='" + obj.Document_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullMovement
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsBullMovement = GetData(strCode, NavType, trans)
            trans.Commit()
            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBullMovement
        Dim obj As clsBullMovement = Nothing

        Try
            Dim strQry As String = "select * from TSPL_BULL_MOVEMENT where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Document_Code = (select MIN(Code) from TSPL_BULL_MOVEMENT where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Document_Code = (Select Max(Code) from TSPL_BULL_MOVEMENT where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Document_Code = (Select Min(Code) from TSPL_BULL_MOVEMENT where Document_Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Document_Code = (select Max(Code) from TSPL_BULL_MOVEMENT where Document_Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Document_Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBullMovement()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
                obj.Bull_Code = clsCommon.myCstr(dt.Rows(0)("Bull_Code"))
                obj.Bull_Movement_Type = clsCommon.myCstr(dt.Rows(0)("Bull_Movement_Type"))
                obj.Bull_Shed = clsCommon.myCstr(dt.Rows(0)("Bull_Shed"))
                obj.Perid = clsCommon.myCstr(dt.Rows(0)("Perid"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))

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
            Dim qry As String = "delete from TSPL_BULL_MOVEMENT where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
