Imports System.Data.SqlClient
Imports common
Public Class clsPrintCheck
#Region "variables"
    Public PRINTING_ID As String = ""
    Public BANK_CODE As String = ""
    Public CHECK_CODE As String = ""
    Public PINTED_BY As String = ""
    Public PRINT_STATUS As String = ""
    Public CHECK_NUMBER As Integer = 0
    Public DOCUMENT_TYPE As String = Nothing
    Public PRINT_DATE As Date
    Public DOCUMENT_NO As String = Nothing
    Public DOCUMENT_Date As DateTime? = Nothing
    Public Form_ID As String = ""
    Public Account_Payee As Integer = 0
    Public Not_For_High_Value As Integer = 0
    Public Void_Check As Integer = 0
    Public Pay_To As String = String.Empty
    Public Amount As Double = 0
    Public Manual_Print As Integer = 0
    'Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region
#Region "Functions"
    Public Shared Function SaveData(ByVal objList As List(Of clsPrintCheck)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Dim isSaved As Boolean = True
        For Each obj As clsPrintCheck In objList
            isSaved = isSaved AndAlso SaveData(obj, trans)
        Next
        trans.Commit()
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal obj As clsPrintCheck, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "PRINTING_ID", obj.PRINTING_ID)
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE)
            clsCommon.AddColumnsForChange(coll, "CHECK_CODE", obj.CHECK_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PINTED_BY", obj.PINTED_BY)
            clsCommon.AddColumnsForChange(coll, "PRINT_STATUS", obj.PRINT_STATUS)
            clsCommon.AddColumnsForChange(coll, "CHECK_NUMBER", obj.CHECK_NUMBER)
            clsCommon.AddColumnsForChange(coll, "DOCUMENT_NO", obj.DOCUMENT_NO)
            clsCommon.AddColumnsForChange(coll, "DOCUMENT_Date", clsCommon.GetPrintDate(obj.DOCUMENT_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "DOCUMENT_TYPE", obj.DOCUMENT_TYPE)

            clsCommon.AddColumnsForChange(coll, "Pay_To", obj.Pay_To)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)

            'clsCommon.AddColumnsForChange(coll, "DOCUMENT_TYPE", obj.DOCUMENT_TYPE)

            '' Anubhooti 22-July-2014 BM00000003161
            clsCommon.AddColumnsForChange(coll, "Not_For_High_Value", obj.Not_For_High_Value)
            clsCommon.AddColumnsForChange(coll, "Void_Check", obj.Void_Check)
            clsCommon.AddColumnsForChange(coll, "Manual_Print", obj.Manual_Print)

            clsCommon.AddColumnsForChange(coll, "PRINT_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_CHECK_PRINTING_STATUS", OMInsertOrUpdate.Insert, "", trans)
            '' update payment header table for check_code
            Dim col2 As New Hashtable()
            If clsCommon.CompairString(obj.DOCUMENT_TYPE, "Payment Entry") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(col2, "CHECK_CODE", obj.CHECK_CODE, True)
                clsCommon.AddColumnsForChange(col2, "Cheque_No", obj.CHECK_NUMBER)
                clsCommonFunctionality.UpdateDataTable(col2, "TSPL_PAYMENT_HEADER", OMInsertOrUpdate.Update, "TSPL_PAYMENT_HEADER.PAYMENT_NO='" + obj.DOCUMENT_NO + "'", trans)
            ElseIf clsCommon.CompairString(obj.DOCUMENT_TYPE, "Receipt Entry") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(col2, "CHECK_CODE", obj.CHECK_CODE, True)
                clsCommon.AddColumnsForChange(col2, "Cheque_No", obj.CHECK_NUMBER)
                clsCommonFunctionality.UpdateDataTable(col2, "TSPL_RECEIPT_HEADER", OMInsertOrUpdate.Update, "TSPL_RECEIPT_HEADER.Receipt_No='" + obj.DOCUMENT_NO + "'", trans)
            End If

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal BankCode As String, ByVal CheckCode As String, ByVal DocumentType As String, ByVal DocumentCode As String, Optional ByVal trans As SqlTransaction = Nothing) As clsPrintCheck
        Dim obj As New clsPrintCheck
        Dim Arr As List(Of clsPrintCheck) = Nothing
        Dim qry As String = "select * from TSPL_BANK_CHECK_PRINTING_STATUS where 2=2 "
        Dim whrclas As String = ""
        If clsCommon.myLen(DocumentType) > 0 Then
            whrclas = " Bank_Code='" & BankCode & "' and coalesce(Check_Code,'')='" & CheckCode & "' and DOCUMENT_TYPE='" & DocumentType & "' and  DOCUMENT_NO='" & DocumentCode & "' and Void_Check=0"
        Else
            whrclas = " Bank_Code='" & BankCode & "' and coalesce(Check_Code,'')='" & CheckCode & "' "
        End If

        qry += " and TSPL_BANK_CHECK_PRINTING_STATUS.PRINTING_ID = (select Max(PRINTING_ID) from TSPL_BANK_CHECK_PRINTING_STATUS where  " + whrclas + ")"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPrintCheck()
            obj.PRINTING_ID = clsCommon.myCstr(dt.Rows(0)("PRINTING_ID"))
            obj.CHECK_NUMBER = clsCommon.myCdbl(dt.Rows(0)("CHECK_NUMBER"))

            If dt.Rows(0)("DOCUMENT_Date") IsNot DBNull.Value Then
                obj.DOCUMENT_Date = clsCommon.myCDate(dt.Rows(0)("DOCUMENT_Date"))
            End If
            obj.DOCUMENT_TYPE = clsCommon.myCstr(dt.Rows(0)("DOCUMENT_TYPE"))
            obj.DOCUMENT_NO = clsCommon.myCstr(dt.Rows(0)("DOCUMENT_NO"))
            obj.BANK_CODE = clsCommon.myCstr(dt.Rows(0)("BANK_CODE"))
            obj.CHECK_CODE = clsCommon.myCstr(dt.Rows(0)("CHECK_CODE"))
            obj.PINTED_BY = clsCommon.myCstr(dt.Rows(0)("PINTED_BY"))
            obj.PRINT_STATUS = clsCommon.myCstr(dt.Rows(0)("PRINT_STATUS"))
            obj.PRINT_DATE = dt.Rows(0)("PRINT_DATE")

            '' Anubhooti 22-July-2014 BM00000003161
            obj.Account_Payee = clsCommon.myCdbl(dt.Rows(0)("Account_Payee"))
            obj.Not_For_High_Value = clsCommon.myCdbl(dt.Rows(0)("Not_For_High_Value"))
            obj.Void_Check = clsCommon.myCdbl(dt.Rows(0)("Void_Check"))
            obj.Manual_Print = clsCommon.myCdbl(dt.Rows(0)("Manual_Print"))
            obj.Pay_To = clsCommon.myCstr(dt.Rows(0)("Pay_To"))
            obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
        Else
            obj.PRINTING_ID = 0
            obj.CHECK_NUMBER = GetNextCheckNumber(BankCode, CheckCode, trans)
            obj.BANK_CODE = BankCode
            obj.CHECK_CODE = CheckCode
            obj.PINTED_BY = objCommonVar.CurrentUserCode
            obj.PRINT_STATUS = "Pending"
            obj.PRINT_DATE = clsCommon.GETSERVERDATE(trans)
            obj.DOCUMENT_TYPE = DocumentType
            obj.DOCUMENT_NO = DocumentCode
            obj.Account_Payee = 1
            obj.Not_For_High_Value = 1


        End If
        Return obj
    End Function
    Public Shared Function GetDataMultiple(ByVal BankCode As String, ByVal CheckCode As String, ByVal DocumentType As String, ByVal From_Date As Date, ByVal To_Date As Date, Optional ByVal Document_Code As String = "", Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPrintCheck)
        Dim obj As New clsPrintCheck
        Dim objList As New List(Of clsPrintCheck)
        Dim Arr As List(Of clsPrintCheck) = Nothing
        Dim qry As String = ""
        Dim whrCond As String = ""
       
        If clsCommon.CompairString(DocumentType, "Payment Entry") = CompairStringResult.Equal Then
            If clsCommon.myLen(Document_Code) <= 0 Then
                whrCond = " TSPL_PAYMENT_HEADER.Bank_Code='" & BankCode & "' and TSPL_PAYMENT_HEADER.PAYMENT_CODE='CHEQUE' and (TSPL_BANK_CHECK_PRINTING_STATUS.check_code='" & CheckCode & "' or  TSPL_BANK_CHECK_PRINTING_STATUS.check_code is null) and TSPL_PAYMENT_HEADER.Payment_Date between '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "'"
            Else
                whrCond = " TSPL_PAYMENT_HEADER.Payment_No='" & Document_Code & "'"
            End If
            'and Check_Code='" & CheckCode & "'
            qry = " select Payment_No as Document_No,Payment_Date as Document_Date,Payment_Post_Date as Post_date,TSPL_PAYMENT_HEADER.Bank_Code," & _
                  " Payment_Type as Transaction_Type,TSPL_PAYMENT_HEADER.Vendor_Code, coalesce((CASE WHEN TSPL_VENDOR_MASTER.Form_Type in ('VSP','PTM','TTM') then coalesce(Cheque_In_Favour_Of,'') else (TSPL_VENDOR_MASTER.Vendor_Name) end),Remit_To) as Pay_To,Payment_Amount as Amount," & _
                  " Printing.Max_Printing_Id as Printing_Id,TSPL_BANK_CHECK_PRINTING_STATUS.Check_Code,TSPL_BANK_CHECK_PRINTING_STATUS.Check_Number, " & _
                  " TSPL_BANK_CHECK_PRINTING_STATUS.PRINT_DATE,TSPL_BANK_CHECK_PRINTING_STATUS.PRINT_STATUS,TSPL_BANK_CHECK_PRINTING_STATUS.PINTED_BY, " & _
                  " 'Payment Entry' as DOCUMENT_TYPE,TSPL_BANK_CHECK_PRINTING_STATUS.DOCUMENT_NO,TSPL_PAYMENT_HEADER.Account_Payee," & _
                  " TSPL_BANK_CHECK_PRINTING_STATUS.Not_For_High_Value,Manual_Print from TSPL_PAYMENT_HEADER " & _
                  " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code " & _
                  " left join ( " & _
                  " select DOCUMENT_NO,Max(PRINTING_ID) as Max_Printing_Id from TSPL_BANK_CHECK_PRINTING_STATUS " & _
                  " where  Bank_Code='" & BankCode & "' and Void_Check=0 " & _
                  " and DOCUMENT_TYPE='Payment Entry' group by  DOCUMENT_NO) as Printing on TSPL_PAYMENT_HEADER.Payment_No=Printing.DOCUMENT_NO " & _
                  " left join TSPL_BANK_CHECK_PRINTING_STATUS on TSPL_BANK_CHECK_PRINTING_STATUS.printing_id=Printing.Max_Printing_Id " & _
                  " where " & whrCond & ""
        ElseIf clsCommon.CompairString(DocumentType, "Receipt Entry") = CompairStringResult.Equal Then
            If clsCommon.myLen(Document_Code) <= 0 Then
                whrCond = " TSPL_RECEIPT_HEADER.Bank_Code='" & BankCode & "'  and TSPL_RECEIPT_HEADER.PAYMENT_CODE='CHEQUE' and (TSPL_BANK_CHECK_PRINTING_STATUS.check_code='" & CheckCode & "' or  TSPL_BANK_CHECK_PRINTING_STATUS.check_code is null) and TSPL_RECEIPT_HEADER.Receipt_Date between '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "'"
            Else
                whrCond = " TSPL_RECEIPT_HEADER.Receipt_No='" & Document_Code & "'"
            End If
            qry = " select Receipt_No as Document_No,Receipt_Date as Document_Date,Receipt_Post_Date as Post_date,TSPL_RECEIPT_HEADER.Bank_Code," & _
                  " Receipt_Type as Transaction_Type,Cust_Code,Customer_Name as Pay_To,Receipt_Amount as Amount," & _
                  " Printing.Max_Printing_Id as Printing_Id,TSPL_BANK_CHECK_PRINTING_STATUS.Check_Code,TSPL_BANK_CHECK_PRINTING_STATUS.Check_Number," & _
                  " TSPL_BANK_CHECK_PRINTING_STATUS.PRINT_DATE,TSPL_BANK_CHECK_PRINTING_STATUS.PRINT_STATUS, " & _
                  " TSPL_BANK_CHECK_PRINTING_STATUS.PINTED_BY,'Payment Entry' as DOCUMENT_TYPE," & _
                  " TSPL_BANK_CHECK_PRINTING_STATUS.DOCUMENT_NO,TSPL_BANK_CHECK_PRINTING_STATUS.Account_Payee," & _
                  " TSPL_BANK_CHECK_PRINTING_STATUS.Not_For_High_Value from TSPL_RECEIPT_HEADER  " & _
                  " left join ( " & _
                  " select DOCUMENT_NO,Max(PRINTING_ID) as Max_Printing_Id from TSPL_BANK_CHECK_PRINTING_STATUS " & _
                  " where  Bank_Code='" & BankCode & "' and Void_Check=0 " & _
                  " and DOCUMENT_TYPE='Receipt Entry' group by  DOCUMENT_NO) as Printing on TSPL_RECEIPT_HEADER.Receipt_No=Printing.DOCUMENT_NO " & _
                  " left join TSPL_BANK_CHECK_PRINTING_STATUS on TSPL_BANK_CHECK_PRINTING_STATUS.printing_id=Printing.Max_Printing_Id " & _
                  " where " & whrCond & " "
        End If
        Dim dt As DataTable
        If clsCommon.myLen(qry) > 0 Then
            dt = clsDBFuncationality.GetDataTable(qry, trans)
        Else
            clsCommon.MyMessageBoxShow("Invalid document type")
            Return objList
        End If

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsPrintCheck()
                obj.PRINTING_ID = clsCommon.myCstr(dr("PRINTING_ID"))
                obj.CHECK_NUMBER = clsCommon.myCdbl(dr("CHECK_NUMBER"))
                obj.DOCUMENT_TYPE = clsCommon.myCstr(dr("DOCUMENT_TYPE"))
                obj.DOCUMENT_NO = clsCommon.myCstr(dr("DOCUMENT_NO"))
                obj.DOCUMENT_Date = clsCommon.myCDate(dr("Document_Date"))
                obj.BANK_CODE = clsCommon.myCstr(dr("BANK_CODE"))
                obj.CHECK_CODE = clsCommon.myCstr(dr("CHECK_CODE"))
                obj.PINTED_BY = clsCommon.myCstr(dr("PINTED_BY"))
                obj.PRINT_STATUS = clsCommon.myCstr(dr("PRINT_STATUS"))
                If clsCommon.myLen(dr("PRINT_DATE")) > 0 Then
                    obj.PRINT_DATE = dr("PRINT_DATE")
                End If

                obj.Pay_To = clsCommon.myCstr(dr("Pay_To"))
                obj.Amount = clsCommon.myCdbl(dr("Amount"))
                obj.Manual_Print = clsCommon.myCdbl(dr("Manual_Print"))
                '' Anubhooti 22-July-2014 BM00000003161
                obj.Account_Payee = clsCommon.myCdbl(dr("Account_Payee"))
                obj.Not_For_High_Value = clsCommon.myCdbl(dr("Not_For_High_Value"))
                objList.Add(obj)
            Next

        End If
        Return objList
    End Function
   
    Public Shared  Function GetNextCheckNumber(ByVal BankCode As String, ByVal CheckCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim qry As String
        Dim CheckNumber As Integer = 0
        qry = "select TSPL_BANK_CHECK_PRINTING.NEXT_CHECK_NUMBER,TSPL_BANK_CHECK_PRINTING.LAST_CHECK_NUMBER," & _
              " coalesce(TSPL_BANK_CHECK_PRINTING_STATUS.CHECK_NUMBER,0) as CHECK_NUMBER from TSPL_BANK_CHECK_PRINTING " & _
              " left join (select BANK_CODE,CHECK_CODE,max(TSPL_BANK_CHECK_PRINTING_STATUS.CHECK_NUMBER) as CHECK_NUMBER from TSPL_BANK_CHECK_PRINTING_STATUS " & _
              " group by BANK_CODE,CHECK_CODE) TSPL_BANK_CHECK_PRINTING_STATUS on " & _
              " TSPL_BANK_CHECK_PRINTING.BANK_CODE = TSPL_BANK_CHECK_PRINTING_STATUS.BANK_CODE " & _
              " and TSPL_BANK_CHECK_PRINTING.CHECK_CODE=TSPL_BANK_CHECK_PRINTING_STATUS.CHECK_CODE " & _
              " where TSPL_BANK_CHECK_PRINTING.BANK_CODE='" & BankCode & "' and TSPL_BANK_CHECK_PRINTING.CHECK_CODE='" & CheckCode & "' "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count <= 0 Then
            'clsCommon.MyMessageBoxShow("No check found for the bank " & BankCode & "")
            Return CheckNumber
        Else
            If dt.Rows(0).Item("LAST_CHECK_NUMBER") <= dt.Rows(0).Item("CHECK_NUMBER") Then
                clsCommon.MyMessageBoxShow("No check found for the bank " & BankCode & "")
                Return CheckNumber
            ElseIf dt.Rows(0).Item("CHECK_NUMBER") = 0 Then
                CheckNumber = dt.Rows(0).Item("NEXT_CHECK_NUMBER")
                Return CheckNumber
            Else
                CheckNumber = dt.Rows(0).Item("CHECK_NUMBER") + 1
                Return CheckNumber
            End If
        End If
    End Function
    Public Shared Function VoidCheck(ByVal BankCode As String, ByVal CheckCode As String, ByVal DocumentType As String, ByVal DocumentCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim qry As String = "update TSPL_BANK_CHECK_PRINTING_STATUS set Void_Check=1 where document_type='" & DocumentType & "' and bank_code='" & BankCode & "' and coalesce(Check_Code,'')='" & CheckCode & "' and document_no='" & DocumentCode & "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry, trans)
    End Function

    Public Shared Function CheckforVoidCheck(ByVal BankCode As String, ByVal Check_Number As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select COUNT(CHECK_NUMBER) as total from TSPL_BANK_CHECK_PRINTING_STATUS where Void_Check=1 and CHECK_NUMBER='" & Check_Number & "' and BANK_CODE='" & BankCode & "'"
        Dim total As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        If total > 0 Then
            Return True
        Else
            Return False
        End If
        Return True
    End Function
#End Region

End Class
