Imports System.Data.SqlClient

Public Class clsCustomerTenderGatePass
#Region "Variables"
    Public Document_Code As String = ""
    Public Document_Date As DateTime = Nothing
    Public Dispatch_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Sub_Location As String = Nothing
    Public Transporter_Code As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Vehicle_No As String = Nothing
    Public Loading_Slip As String = Nothing
    Public Driver_Name As String = Nothing
    Public Driver_Mob_No As String = Nothing
    Public Remarks As String = ""
    Public Status As Integer = 0
    Public Posted_Date As DateTime = Nothing
    Public Arr As List(Of clsCustomerTenderGatePassDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsCustomerTenderGatePass, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsCustomerTenderGatePass, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL where Document_Code='" & obj.Document_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Dispatch_Code", obj.Dispatch_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Sub_Location", obj.Sub_Location, True)
            clsCommon.AddColumnsForChange(coll, "Transporter_Code", obj.Transporter_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "Loading_Slip", obj.Loading_Slip)
            clsCommon.AddColumnsForChange(coll, "Driver_Name", obj.Driver_Name)
            clsCommon.AddColumnsForChange(coll, "Driver_Mob_No", obj.Driver_Mob_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmCustomerTenderGatePass, "", obj.Location_Code)
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_GATE_PASS", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_GATE_PASS", OMInsertOrUpdate.Update, "Document_Code='" & clsCommon.myCstr(obj.Document_Code) & "'", trans)
            End If
            clsCustomerTenderGatePassDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_CUSTOMER_TENDER_GATE_PASS", "Document_Code", "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", "Document_Code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerTenderGatePass
        Dim obj As clsCustomerTenderGatePass = Nothing
        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select * From TSPL_CUSTOMER_TENDER_GATE_PASS  where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Code = (select MIN(Document_Code) from TSPL_CUSTOMER_TENDER_GATE_PASS where 1=1 " & Whrcls & "  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Code = (select Max(Document_Code) from TSPL_CUSTOMER_TENDER_GATE_PASS where 1=1 " & Whrcls & "  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Code = (select Min(Document_Code) from TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code>'" & clsCommon.myCstr(strCode) & "' " & Whrcls & "   )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Code = (select Max(Document_Code) from TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code<'" & clsCommon.myCstr(strCode) & "' " & Whrcls & "  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Code = '" & clsCommon.myCstr(strCode) & "'  " & Whrcls & " "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsCustomerTenderGatePass()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")
                obj.Dispatch_Code = clsCommon.myCstr(dt.Rows(0)("Dispatch_Code"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Sub_Location = clsCommon.myCstr(dt.Rows(0)("Sub_Location"))
                obj.Transporter_Code = clsCommon.myCstr(dt.Rows(0)("Transporter_Code"))
                obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
                obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
                obj.Loading_Slip = clsCommon.myCstr(dt.Rows(0)("Loading_Slip"))
                obj.Driver_Name = clsCommon.myCstr(dt.Rows(0)("Driver_Name"))
                obj.Driver_Mob_No = clsCommon.myCstr(dt.Rows(0)("Driver_Mob_No"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                obj.Arr = clsCustomerTenderGatePassDetail.GetData(obj.Document_Code, trans)
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
            Dim obj As clsCustomerTenderGatePass = clsCustomerTenderGatePass.GetData(strCode, NavigatorType.Current, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_GATE_PASS", OMInsertOrUpdate.Update, "Document_Code='" & clsCommon.myCstr(obj.Document_Code) & "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_GATE_PASS", "Document_Code", "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", "Document_Code", trans)

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

        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_GATE_PASS", "Document_Code", "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", "Document_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_GATE_PASS", "Document_Code", "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", "Document_Code", trans)
            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code = '" & strCode & "' and Status=1", trans)
            If (isPosted = 1) Then
                Dim PostedDate As DateTime = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue("SELECT Posted_Date FROM TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code = '" & strCode & "' and Status=1", trans), "dd/MMM/yyyy")
                Throw New Exception("Already Posted on :" & clsCommon.myCstr(PostedDate))
            End If
            Dim qry As String
            qry = "delete from TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL where Document_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select isnull(Status,0) as Status from TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code='" & strCode & "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 0 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "Update TSPL_CUSTOMER_TENDER_GATE_PASS set Status = 0 where Document_Code='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_GATE_PASS", "Document_Code", "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", "Document_Code", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CancelData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            CancelData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CancelData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim Qry As String = "select isnull(Status,0) as Status from TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code='" & strCode & "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 0 Then
                Throw New Exception("Transaction status should be posted for Cancel")
            End If
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_GATE_PASS", "Document_Code", "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", "Document_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_GATE_PASS", "Document_Code", "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", "Document_Code", trans)
            Qry = "delete from TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL where Document_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "delete from TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsCustomerTenderGatePassDetail
#Region "Variables"
    Public Document_Code As String = ""
    Public Item_Code As String = ""
    Public Unit_Code As String = ""
    Public HSN_Code As String = ""
    Public Qty As Double = 0
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCustomerTenderGatePassDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsCustomerTenderGatePassDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "HSN_Code", obj.HSN_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCustomerTenderGatePassDetail)
        Dim arr As List(Of clsCustomerTenderGatePassDetail) = Nothing
        Try
            Dim dt As DataTable
            Dim strQry As String = "select * from TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL where Document_Code='" & strCode & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsCustomerTenderGatePassDetail)
                Dim objTr As clsCustomerTenderGatePassDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCustomerTenderGatePassDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.HSN_Code = clsCommon.myCstr(dr("HSN_Code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    arr.Add(objTr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
End Class
