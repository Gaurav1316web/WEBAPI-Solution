Imports System.Data.SqlClient

Public Class ClsBullCurlingEntry

    Public Code As String = ""
    Public Remarks As String = ""
    Public Doc_Date As Date
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_By As String = Nothing
    Public Post_Date As DateTime
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime
    Public Arr As List(Of ClsBullCurlingEntryDeatil) = Nothing


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsBullCurlingEntry
        Try
            Dim obj As ClsBullCurlingEntry = Nothing
            Dim qry As String = "SELECT Document_No,CONVERT(varchar, Document_Date, 103) as Document_Date,Remarks,Status,* FROM TSPL_BULL_CURLING WHERE 2=2 "
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Document_No = (select MIN(Document_No) from TSPL_BULL_CURLING where 1=1  )"
                Case NavigatorType.Last
                    qry += " And Document_No = (Select Max(Document_No) from TSPL_BULL_CURLING where 1=1 )"
                Case NavigatorType.Next
                    qry += " And Document_No = (Select Min(Document_No) from TSPL_BULL_CURLING where Document_No>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    qry += " and Document_No = (select Max(Document_No) from TSPL_BULL_CURLING where Document_No<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    qry += " and Document_No = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsBullCurlingEntry()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                'clsCommon.GetPrintDate(dt.Rows(0)("Start_Date"), "dd/MMM/yyyy")
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.Arr = ClsBullCurlingEntryDeatil.GetData(obj.Code, trans)
            End If
            Return obj

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function SaveData(ByVal obj As ClsBullCurlingEntry, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As ClsBullCurlingEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim IsSaved As Boolean = True
        Try
            IsSaved = True

            Dim StrQry As String = ""
            StrQry = "delete from TSPL_BULL_CURLING_Detail where Document_No='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)

            'StrQry = "delete from TSPL_BULL_CURLING where Document_No='" + obj.Code + "'"
            'clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullCurlingEntry, "", objCommonVar.strCurrUserLocations)
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CURLING", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CURLING", OMInsertOrUpdate.Update, "TSPL_BULL_CURLING.Document_No='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso ClsBullCurlingEntryDeatil.SaveData(clsCommon.myCstr(obj.Code), obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
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
                Throw New Exception("Document_No not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_BULL_CURLING_Detail where Document_No='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BULL_CURLING where Document_No='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
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
                Throw New Exception("Document_No not found to Post")
            End If
            Dim obj As ClsBullCurlingEntry = ClsBullCurlingEntry.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Document_No : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Post_Date)
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CURLING", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Code) + "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class ClsBullCurlingEntryDeatil
    Public Document_No As String = Nothing
    Public Species_Code As String = Nothing
    Public Category_Code As String = Nothing
    Public Sub_Category_Code As String = Nothing
    Public SS_Centre_Code As String = Nothing
    Public Breed_Code As String = Nothing
    Public Shed_Code As String = Nothing
    Public Pen_Code As String = Nothing
    Public Status_Code As String = Nothing
    Public Sub_Status_Code As String = Nothing
    Public Bull_Imported As String = Nothing
    Public Exotic_Blood_Per As String = Nothing
    Public Bull_Book_Value As String = Nothing
    Public Registration_Date As Date
    Public Prev_Bull_Id As String = Nothing
    Public Date_Of_Birth As Date
    Public SS_Bull_Id As String = Nothing
    Public Bull_Alia_Name As String = Nothing

    Public BullID As String = Nothing
    Public Amount As Double = 0


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsBullCurlingEntryDeatil), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As ClsBullCurlingEntryDeatil In Arr
                    Dim colm As New Hashtable()
                    clsCommon.AddColumnsForChange(colm, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(colm, "Bull_ID", obj.BullID)
                    clsCommon.AddColumnsForChange(colm, "Amount", obj.Amount)
                    clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_CURLING_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of ClsBullCurlingEntryDeatil)
        Dim arr As List(Of ClsBullCurlingEntryDeatil) = Nothing
        Try
            Dim dt As DataTable
            Dim strQry As String = " select TSPL_BULL_MASTER.Species_Code,TSPL_BULL_MASTER.Category_Code,TSPL_BULL_MASTER.Sub_Category_Code,TSPL_BULL_MASTER.SS_Centre_Code,
                                     TSPL_BULL_MASTER.Breed_Code,TSPL_BULL_MASTER.Shed_Code,TSPL_BULL_MASTER.Pen_Code,TSPL_BULL_MASTER.Status_Code,TSPL_BULL_MASTER.Sub_Status_Code,
                                     TSPL_BULL_MASTER.Exotic_Blood_Per,TSPL_BULL_MASTER.Bull_Book_Value,CONVERT(varchar, TSPL_BULL_MASTER.Registration_Date, 103) as Registration_Date,
                                     TSPL_BULL_MASTER.Prev_Bull_Id,CONVERT(varchar, TSPL_BULL_MASTER.Date_Of_Birth, 103) as Date_Of_Birth,TSPL_BULL_MASTER.SS_Bull_Id,TSPL_BULL_MASTER.Bull_Alia_Name,
                                     TSPL_BULL_CURLING_Detail.Document_No,TSPL_BULL_CURLING_Detail.Bull_ID,TSPL_BULL_CURLING_Detail.Amount from TSPL_BULL_CURLING_Detail
                                     left outer join TSPL_BULL_CURLING on TSPL_BULL_CURLING.Document_No = TSPL_BULL_CURLING_Detail.Document_No
                                     left outer join TSPL_BULL_MASTER ON TSPL_BULL_MASTER.Bull_Code = TSPL_BULL_CURLING_Detail.Bull_ID where TSPL_BULL_CURLING_Detail.Document_No='" & strDocNo & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of ClsBullCurlingEntryDeatil)
                Dim objTr As ClsBullCurlingEntryDeatil
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsBullCurlingEntryDeatil
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.BullID = clsCommon.myCstr(dr("Bull_ID"))
                    objTr.Amount = clsCommon.myCstr(dr("Amount"))
                    objTr.Species_Code = clsCommon.myCstr(dr("Species_Code"))
                    objTr.Category_Code = clsCommon.myCstr(dr("Category_Code"))
                    objTr.Sub_Category_Code = clsCommon.myCstr(dr("Sub_Category_Code"))
                    objTr.SS_Centre_Code = clsCommon.myCstr(dr("SS_Centre_Code"))
                    objTr.Breed_Code = clsCommon.myCstr(dr("Breed_Code"))
                    objTr.Shed_Code = clsCommon.myCstr(dr("Shed_Code"))
                    objTr.Pen_Code = clsCommon.myCstr(dr("Pen_Code"))
                    objTr.Status_Code = clsCommon.myCstr(dr("Status_Code"))
                    objTr.Sub_Status_Code = clsCommon.myCstr(dr("Sub_Status_Code"))
                    objTr.Exotic_Blood_Per = clsCommon.myCstr(dr("Exotic_Blood_Per"))
                    objTr.Bull_Book_Value = clsCommon.myCstr(dr("Bull_Book_Value"))
                    'objTr.Registration_Date = clsCommon.myCDate(dr("Registration_Date"))
                    objTr.Registration_Date = clsCommon.myCDate(dr("Registration_Date"), "dd/MM/yyyy")
                    objTr.Prev_Bull_Id = clsCommon.myCstr(dr("Prev_Bull_Id"))
                    'objTr.Date_Of_Birth = clsCommon.myCDate(dr("Date_Of_Birth"))
                    objTr.Date_Of_Birth = clsCommon.myCDate(dr("Date_Of_Birth"), "dd/MM/yyyy")
                    objTr.SS_Bull_Id = clsCommon.myCstr(dr("SS_Bull_Id"))
                    objTr.Bull_Alia_Name = clsCommon.myCstr(dr("Bull_Alia_Name"))

                    arr.Add(objTr)
                Next
            End If
        Catch ex As Exception

        End Try
        Return arr
    End Function
End Class

