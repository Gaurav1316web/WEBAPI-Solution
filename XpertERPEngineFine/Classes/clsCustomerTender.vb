Imports System.Data.SqlClient
Public Class clsCustomerTender
#Region "Variable"
    Public Document_Code As String = ""
    Public Document_Date As DateTime = Nothing
    Public From_Date As DateTime = Nothing
    Public To_Date As DateTime = Nothing
    Public Location_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Customer_Group As String = Nothing
    Public Total_Qty As Double = 0
    Public Tolerance As Double = 0
    Public Inclusive_Tax As Integer = 0
    Public Inclusive_TPT As Integer = 0
    Public Remarks As String = ""
    Public Status As Integer = 0
    Public Posted_Date As DateTime = Nothing
    Public close_yn As String
    Public close_remarks As String
    Public Arr As List(Of clsCustomerTenderDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsCustomerTender, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsCustomerTender, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOMER_TENDER_DETAIL where Document_Code='" & obj.Document_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Group", obj.Customer_Group)
            clsCommon.AddColumnsForChange(coll, "Total_Qty", obj.Total_Qty)
            clsCommon.AddColumnsForChange(coll, "Tolerance", obj.Tolerance)
            clsCommon.AddColumnsForChange(coll, "Inclusive_Tax", obj.Inclusive_Tax)
            clsCommon.AddColumnsForChange(coll, "Inclusive_TPT", obj.Inclusive_TPT)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmCustomerTender, "", obj.Location_Code)
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER", OMInsertOrUpdate.Update, "Document_Code='" & clsCommon.myCstr(obj.Document_Code) & "'", trans)
            End If
            clsCustomerTenderDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_CUSTOMER_TENDER", "Document_Code", "TSPL_CUSTOMER_TENDER_DETAIL", "Document_Code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerTender
        Dim obj As clsCustomerTender = Nothing
        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select Document_Code,Document_Date,From_Date,To_Date,Location_Code,Item_Code,Customer_Group,Total_Qty,Tolerance,Inclusive_Tax,Inclusive_TPT,Status,Posted_Date,Remarks,close_yn from TSPL_CUSTOMER_TENDER  where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_CUSTOMER_TENDER.Document_Code = (select MIN(Document_Code) from TSPL_CUSTOMER_TENDER where 1=1 " & Whrcls & "  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_CUSTOMER_TENDER.Document_Code = (select Max(Document_Code) from TSPL_CUSTOMER_TENDER where 1=1 " & Whrcls & "  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_CUSTOMER_TENDER.Document_Code = (select Min(Document_Code) from TSPL_CUSTOMER_TENDER where Document_Code>'" & clsCommon.myCstr(strCode) & "' " & Whrcls & "   )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_CUSTOMER_TENDER.Document_Code = (select Max(Document_Code) from TSPL_CUSTOMER_TENDER where Document_Code<'" & clsCommon.myCstr(strCode) & "' " & Whrcls & "  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_CUSTOMER_TENDER.Document_Code = '" & clsCommon.myCstr(strCode) & "'  " & Whrcls & " "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsCustomerTender()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")
                obj.From_Date = clsCommon.GetPrintDate(dt.Rows(0)("From_Date"), "dd/MMM/yyyy")
                obj.To_Date = clsCommon.GetPrintDate(dt.Rows(0)("To_Date"), "dd/MMM/yyyy")
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Customer_Group = clsCommon.myCstr(dt.Rows(0)("Customer_Group"))
                obj.Total_Qty = clsCommon.myCdbl(dt.Rows(0)("Total_Qty"))
                obj.Tolerance = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                obj.Inclusive_Tax = clsCommon.myCdbl(dt.Rows(0)("Inclusive_Tax"))
                obj.Inclusive_TPT = clsCommon.myCdbl(dt.Rows(0)("Inclusive_TPT"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.close_yn = clsCommon.myCstr(dt.Rows(0)("close_yn"))
                obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                obj.Arr = clsCustomerTenderDetail.GetData(obj.Document_Code, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
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
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsCustomerTender = clsCustomerTender.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Code : " & strCode & " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" & obj.Posted_Date)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER", OMInsertOrUpdate.Update, "Document_Code='" & clsCommon.myCstr(obj.Document_Code) & "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER", "Document_Code", "TSPL_CUSTOMER_TENDER_Detail", "Document_Code", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean
        Dim obj As New clsCustomerTender()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER", "Document_Code", "TSPL_CUSTOMER_TENDER_Detail", "Document_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER", "Document_Code", "TSPL_CUSTOMER_TENDER_Detail", "Document_Code", trans)
            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_CUSTOMER_TENDER where Document_Code = '" & strCode & "' and Status=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" & obj.Posted_Date)
            End If
            Dim qry As String
            qry = "delete from TSPL_CUSTOMER_TENDER_Detail where Document_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CUSTOMER_TENDER where Document_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function closeCustomerTenderdata(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal cls As String, Optional ByVal strRemarks As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            closeCustomerTenderdata(trans, strDocNo, isCheckForPosted, cls, strRemarks)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function closeCustomerTenderdata(ByVal trans As SqlTransaction, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal cls As String, Optional ByVal strRemarks As String = "") As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Customer Tender not found to Close")
            End If
            Dim strClosedDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsCustomerTender = clsCustomerTender.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Close")
            End If
            obj.close_yn = cls
            obj.close_remarks = strRemarks
            Dim qry As String = "Update TSPL_CUSTOMER_TENDER set close_yn='" + obj.close_yn + "',close_remarks='" + obj.close_remarks + "',Closed_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Closed_Date='" + strClosedDate + "' where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsCustomerTenderDetail
#Region "Variable"
    Public Document_Code As String = ""
    Public Cust_Code As String = ""
    Public Item_Rate As Double = 0
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCustomerTenderDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsCustomerTenderDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCustomerTenderDetail)
        Dim arr As List(Of clsCustomerTenderDetail) = Nothing
        Try
            Dim dt As DataTable
            Dim strQry As String = "select Document_Code,Cust_Code,Item_Rate from TSPL_CUSTOMER_TENDER_Detail where Document_Code='" & strCode & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsCustomerTenderDetail)
                Dim objTr As clsCustomerTenderDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCustomerTenderDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Item_Rate = clsCommon.myCdbl(dr("Item_Rate"))
                    arr.Add(objTr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
End Class
