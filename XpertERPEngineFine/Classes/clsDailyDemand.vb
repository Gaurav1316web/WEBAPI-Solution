Imports System.Data.SqlClient
Public Class clsDailyDemand
    Public Code As String = Nothing
    Public Start_Date As Date
    Public End_Date As Date? = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime
    Public Post_By As String = Nothing
    Public Post_Date As DateTime
    Public Qty As Double = 0
    'Public Qty As Integer

    Public Union_Name As String
    'Public Daily_Date As Date
    Public Document_Date As Date
    'Public Date As String = Nothing
    Public Arr As List(Of clsDailyDemand) = Nothing

    Public Function SaveData(ByVal obj As clsDailyDemand, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsDailyDemand = clsDailyDemand.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Post_Date)
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Daily_Demand_Master", OMInsertOrUpdate.Update, "Code='" + clsCommon.myCstr(obj.Code) + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "Tspl_Daily_Demand_Master", "Code", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsDailyDemand, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            'Dim StrQry As String
            'StrQry = "delete from Tspl_Daily_Demand_Master where Code='" + clsCommon.myCstr(obj.Code) + "'"

            'clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "Union_Name", obj.Union_Name)
            'clsCommon.AddColumnsForChange(coll, "code", obj.Code)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))

            If obj.End_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            End If
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.DailyDemand, "", "")

                ' obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date, "dd/MMM/yyyy  hh:mm tt"), clsDocType.DisposalEntry, "", obj.Loc_Code)
                'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)

                'obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Daily_Date), clsDocType.DailyDemands, "", "")
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Daily_Demand_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Daily_Demand_Master", OMInsertOrUpdate.Update, "Tspl_Daily_Demand_Master.Code='" + obj.Code + "'", trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "Tspl_Daily_Demand_Master", "Code", trans)

            'IsSaved = IsSaved AndAlso clsDailyDemand.SaveData(obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function DeleteData(ByVal StrCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = DeleteData(StrCode, trans)
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
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, StrCode, "Tspl_Daily_Demand_Master", "Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, StrCode, "Tspl_Daily_Demand_Master", "Code", trans)

            Dim qry As String = ""
            qry = "delete from Tspl_Daily_Demand_Master where Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from Tspl_Daily_Demand_Master where Code='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDailyDemand
        Dim obj As clsDailyDemand = Nothing

        Try
            Dim strQry As String = "SELECT Code,Start_Date,End_Date,status,* FROM Tspl_Daily_Demand_Master where 1=1 "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Code = (select MIN(Code) from Tspl_Daily_Demand_Master where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Code = (Select Max(Code) from Tspl_Daily_Demand_Master where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Code = (Select Min(Code) from Tspl_Daily_Demand_Master where Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Code = (select Max(Code) from Tspl_Daily_Demand_Master where Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsDailyDemand()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Start_Date = clsCommon.GetPrintDate(dt.Rows(0)("Start_Date"), "dd/MMM/yyyy")
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")
                If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                    obj.End_Date = clsCommon.GetPrintDate(dt.Rows(0)("End_Date"), "dd/MMM/yyyy")
                End If
                obj.Qty = clsCommon.myCstr(dt.Rows(0)("Qty"))
                obj.Union_Name = clsCommon.myCstr(dt.Rows(0)("Union_Name"))

                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function
End Class
