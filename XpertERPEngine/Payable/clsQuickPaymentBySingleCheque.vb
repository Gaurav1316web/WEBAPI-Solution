Imports System.Data.SqlClient
Imports common

Public Class clsQuickPaymentBySingleCheque
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_date As DateTime? = Nothing
    Public Bank_Code As String = Nothing
    Public Bank_Name As String = Nothing
    Public Payment_Code As String = Nothing
    Public Cheque_No As String = Nothing
    Public Cheque_Date As Date?
    Public Cheque_Amount As Double = 0.0
    Public CHECK_CODE As String = Nothing
    Public Remarks As String = Nothing
    Public Account_Payee As Integer = 0
    Public Status As Integer = 0
    Public Posted_Date As DateTime? = Nothing
    Public Ref_PK_Id As Integer
    Public Arr As List(Of clsQuickPaymentBySingleChequeDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsQuickPaymentBySingleCheque, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans, AutoSave)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsQuickPaymentBySingleCheque, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)
            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Cheque_Amount", obj.Cheque_Amount)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Account_Payee", obj.Account_Payee)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.QuickPaymentBySingleCheque, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE", OMInsertOrUpdate.Update, "TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Document_No='" & obj.Document_No & "'", trans)
            End If
            Dim objDetail As New clsQuickPaymentBySingleChequeDetail()
            isSaved = isSaved AndAlso objDetail.SaveData(obj.Document_No, obj.Arr, trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsQuickPaymentBySingleCheque
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsQuickPaymentBySingleCheque
        Dim obj As clsQuickPaymentBySingleCheque = Nothing
        Dim qry As String = "select * from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry &= " and TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Document_No = (select MIN(Document_No) from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE)"
            Case NavigatorType.Last
                qry &= " and TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Document_No = (select Max(Document_No) from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE)"
            Case NavigatorType.Next
                qry &= " and TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Document_No = (select Min(Document_No) from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE where Document_No >'" & strCode & "')"
            Case NavigatorType.Previous
                qry &= " and TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Document_No = (select Max(Document_No) from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE where Document_No <'" & strCode & "')"
            Case NavigatorType.Current
                qry &= " and TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Document_No = '" & strCode & "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsQuickPaymentBySingleCheque()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            obj.Cheque_Date = clsCommon.myCDate(dt.Rows(0)("Cheque_Date"))
            obj.Cheque_Amount = clsCommon.myCDecimal(dt.Rows(0)("Cheque_Amount"))
            obj.CHECK_CODE = clsCommon.myCstr(dt.Rows(0)("CHECK_CODE"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Account_Payee = clsCommon.myCdbl(dt.Rows(0)("Account_Payee"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            Dim objDetail As New clsQuickPaymentBySingleChequeDetail()
            obj.Arr = objDetail.GetData(obj.Document_No, trans)
        End If
        Return obj
    End Function

    Public Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,convert(varchar(12),Document_date,103) as DocumentDate, Bank_Code as [Bank Code],Bank_Name as [Bank Name],Payment_Code AS [Payment Mode],case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE"
        str = clsCommon.ShowSelectForm("QKPTSCHQ", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function
    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsQuickPaymentBySingleCheque = New clsQuickPaymentBySingleCheque()
            obj = obj.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If
            Dim objPaymentEntry As clsPaymentHeader = New clsPaymentHeader()

            For Each objDetail As clsQuickPaymentBySingleChequeDetail In obj.Arr
                objPaymentEntry = New clsPaymentHeader()
                objPaymentEntry.IsChkReverse = "N"
                objPaymentEntry.Vendor_Code = objDetail.Vendor_Code
                objPaymentEntry.Vendor_Name = objDetail.Vendor_Name
                objPaymentEntry.REF_PK_ID = objDetail.PK_ID
                objPaymentEntry.Payment_Type = "OA"
                objPaymentEntry.Payment_Code = obj.Payment_Code
                objPaymentEntry.Cheque_No = obj.Cheque_No
                objPaymentEntry.Cheque_Date = obj.Cheque_Date
                objPaymentEntry.Bank_Code = obj.Bank_Code
                objPaymentEntry.Vendor_Bank_Code = objDetail.Vendor_Bank_Code
                objPaymentEntry.Vendor_Bank_Name = objDetail.Vendor_Bank_Name
                objPaymentEntry.Vendor_Bank_ACNo = objDetail.Vendor_Bank_ACNo
                objPaymentEntry.Vendor_IFSC_Code = objDetail.Vendor_IFSC_Code
                objPaymentEntry.Payment_Date = obj.Document_date
                objPaymentEntry.Payment_Amount = objDetail.Amount
                objPaymentEntry.Total_Prepayment = objDetail.Amount
                objPaymentEntry.Balance_Amt = objDetail.Amount
                objPaymentEntry.PAYMENT_AMOUNT_BASE_CURRENCY = objDetail.Amount
                objPaymentEntry.Account_Payee = obj.Account_Payee
                objPaymentEntry.Form_ID = "PYMT-NEW"
                objPaymentEntry.WaveOFFBankCharges = "N"
                objPaymentEntry.SaveData(objPaymentEntry, True, trans, True)
                clsPaymentHeader.PostData(objPaymentEntry.Payment_No, "Payable", trans)
            Next
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE set Status= 1, Posted_By = '" & objCommonVar.CurrentUserCode & "',Posted_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "'  where Document_No='" & obj.Document_No & "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim isResponse As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                isResponse = True
            Else
                isResponse = False
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsQuickPaymentBySingleCheque = New clsQuickPaymentBySingleCheque
            obj = obj.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                isResponse = False
            End If

            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                isResponse = False
            End If
            Dim qry As String = ""

            qry = "select distinct isnull(TSPL_PAYMENT_HEADER.Payment_No,'') Payment_No from TSPL_PAYMENT_HEADER where REF_PK_ID in ( select PK_ID FROM TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL WHERE Document_No= '" + strCode + "' ) "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsPaymentHeader.ReverseAndUnpost(clsCommon.myCstr(dr("Payment_No")), trans)
                Next
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE", OMInsertOrUpdate.Update, "Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function
    Public Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsQuickPaymentBySingleCheque = New clsQuickPaymentBySingleCheque()
        obj = obj.GetData(strCode, NavigatorType.Current, trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If
            Dim qry As String = " select distinct isnull(TSPL_PAYMENT_HEADER.Payment_No,'') Payment_No,VENDOR_CODE from TSPL_PAYMENT_HEADER where REF_PK_ID in ( select PK_ID FROM TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL WHERE Document_No= '" + strCode + "' ) "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsPaymentHeader.fundelete("OA", clsCommon.myCstr(dr("Payment_No")), clsCommon.myCstr(dr("VENDOR_CODE")), trans)
                Next
            End If
            qry = "delete from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsQuickPaymentBySingleChequeDetail

#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Vendor_Bank_Code As String = Nothing
    Public Vendor_Bank_Name As String = Nothing
    Public Vendor_Bank_ACNo As String = Nothing
    Public Vendor_IFSC_Code As String = Nothing
    Public Amount As Decimal = 0
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsQuickPaymentBySingleChequeDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsQuickPaymentBySingleChequeDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Vendor_Bank_Code", obj.Vendor_Bank_Code)
                clsCommon.AddColumnsForChange(coll, "Vendor_Bank_Name", obj.Vendor_Bank_Name)
                clsCommon.AddColumnsForChange(coll, "Vendor_Bank_ACNo", obj.Vendor_Bank_ACNo)
                clsCommon.AddColumnsForChange(coll, "Vendor_IFSC_Code", obj.Vendor_IFSC_Code)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsQuickPaymentBySingleChequeDetail)
        Dim arr As List(Of clsQuickPaymentBySingleChequeDetail) = Nothing
        Dim qry As String = "select TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.*,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL left outer join TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Vendor_Code where  TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Document_No = '" & strCode & "' order by Document_No,PK_ID "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsQuickPaymentBySingleChequeDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsQuickPaymentBySingleChequeDetail = New clsQuickPaymentBySingleChequeDetail()
                obj.pk_id = clsCommon.myCdbl(dr("PK_ID"))
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                obj.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                obj.Vendor_Bank_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Bank_Code"))
                obj.Vendor_Bank_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Bank_Name"))
                obj.Vendor_Bank_ACNo = clsCommon.myCstr(dr("Vendor_Bank_ACNo"))
                obj.Vendor_IFSC_Code = clsCommon.myCstr(dr("Vendor_IFSC_Code"))
                obj.Amount = clsCommon.myCDecimal(dr("Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class
