Imports common
Imports System.Data.SqlClient

Public Class clsRevaluationHead
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Description As String
    Public Trans_Type As String = Nothing
    Public Currency_Code As String = Nothing
    Public Currency_Rate As Decimal
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_By As String = Nothing
    Public Posted_Date As DateTime? = Nothing
    Public Arr As List(Of clsRevaluationDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsRevaluationHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            obj.SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Private Function SaveData(ByVal obj As clsRevaluationHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        If obj.Arr.Count <= 0 Then
            Throw New Exception("Please fill at least one Document")
        End If
        Dim qry As String = "delete from TSPL_REVALUATION_DETAIL where Document_No='" + obj.Document_No + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        If isNewEntry Then
            obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.RevaluationEntry, "", "")
        End If
        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
        clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
        clsCommon.AddColumnsForChange(coll, "Currency_Code", obj.Currency_Code)
        clsCommon.AddColumnsForChange(coll, "Currency_Rate", obj.Currency_Rate)
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REVALUATION_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REVALUATION_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
        End If
        clsRevaluationDetail.SaveData(obj.Document_No, Arr, trans)
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsRevaluationHead
        Dim obj As clsRevaluationHead = Nothing
        Dim qry As String = "Select * from TSPL_REVALUATION_HEAD where 2=2 and "
        Select Case NavType
            Case NavigatorType.First
                qry += "TSPL_REVALUATION_HEAD.Document_No=( select MIN(TSPL_REVALUATION_HEAD.Document_No) from TSPL_REVALUATION_HEAD Where 1=1 ) "
            Case NavigatorType.Last
                qry += "TSPL_REVALUATION_HEAD.Document_No=( select Max(TSPL_REVALUATION_HEAD.Document_No) from TSPL_REVALUATION_HEAD Where 1=1  )"
            Case NavigatorType.Next
                qry += "TSPL_REVALUATION_HEAD.Document_No=( select Min(TSPL_REVALUATION_HEAD.Document_No) from TSPL_REVALUATION_HEAD  where  Document_No>'" + strDocumentNo + "' ) "
            Case NavigatorType.Previous
                qry += "TSPL_REVALUATION_HEAD.Document_No=( select Max(TSPL_REVALUATION_HEAD.Document_No) from TSPL_REVALUATION_HEAD  where Document_No<'" + strDocumentNo + "' )"
            Case NavigatorType.Current
                qry += " TSPL_REVALUATION_HEAD.Document_No='" + strDocumentNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRevaluationHead()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))

            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            obj.Currency_Code = clsCommon.myCstr(dt.Rows(0)("Currency_Code"))
            obj.Currency_Rate = clsCommon.myCdbl(dt.Rows(0)("Currency_Rate"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 0, ERPTransactionStatus.Pending, ERPTransactionStatus.Approved)
            obj.Posted_By = clsCommon.myCstr(dt.Rows(0)("Posted_By"))
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCstr(dt.Rows(0)("Posted_Date"))
            Else
                obj.Posted_Date = Nothing
            End If
            If clsCommon.CompairString(obj.Trans_Type, "AP") = CompairStringResult.Equal Then
                qry = "Select TSPL_REVALUATION_DETAIL.*,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_HEAD.Document_Total,(Balance_Amount*Tran_Conv_Rate) as BaseCurrencyAmt ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as Vendor_Customer_Code,TSPL_VENDOR_MASTER.Vendor_Name as Vendor_Customer_Name,Invoice_Entry_Date as Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE   from TSPL_REVALUATION_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_REVALUATION_DETAIL.AP_Invoice_No left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  where TSPL_REVALUATION_DETAIL.Document_No='" + obj.Document_No + "' ORDER BY TSPL_REVALUATION_DETAIL.SNo"
            ElseIf clsCommon.CompairString(obj.Trans_Type, "AR") = CompairStringResult.Equal Then
                qry = "Select TSPL_REVALUATION_DETAIL.*,TSPL_Customer_Invoice_Head.Loc_Code,TSPL_Customer_Invoice_Head.Document_Total,(Balance_Amount*Tran_Conv_Rate) as BaseCurrencyAmt , TSPL_Customer_Invoice_Head.Customer_Code as Vendor_Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Vendor_Customer_Name,Document_Date  as Invoice_Date,TSPL_Customer_Invoice_Head.CURRENCY_CODE    from TSPL_REVALUATION_DETAIL left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_REVALUATION_DETAIL.Ar_invoice_no left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code  where TSPL_REVALUATION_DETAIL.Document_No='" + obj.Document_No + "' ORDER BY TSPL_REVALUATION_DETAIL.SNo "
            ElseIf clsCommon.CompairString(obj.Trans_Type, "PY") = CompairStringResult.Equal Then
                qry = "Select TSPL_REVALUATION_DETAIL.*,TSPL_PAYMENT_HEADER.Location_GL_Code as Loc_Code,TSPL_PAYMENT_HEADER.Payment_Amount as Document_Total,(Balance_Amount*Tran_Conv_Rate) as BaseCurrencyAmt , TSPL_PAYMENT_HEADER.Vendor_Code as Vendor_Customer_Code,TSPL_VENDOR_MASTER.Vendor_Name as Vendor_Customer_Name,TSPL_PAYMENT_HEADER.Payment_Date  as Invoice_Date,TSPL_PAYMENT_HEADER.CURRENCY_CODE    from TSPL_REVALUATION_DETAIL left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REVALUATION_DETAIL.Payment_No left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code   where TSPL_REVALUATION_DETAIL.Document_No='" + obj.Document_No + "' ORDER BY TSPL_REVALUATION_DETAIL.SNo "
            ElseIf clsCommon.CompairString(obj.Trans_Type, "RC") = CompairStringResult.Equal Then
                qry = "Select TSPL_REVALUATION_DETAIL.*,TSPL_RECEIPT_HEADER.Location_GL_Code as Loc_Code,TSPL_RECEIPT_HEADER.Receipt_Amount as Document_Total,(Balance_Amount*Tran_Conv_Rate) as BaseCurrencyAmt , TSPL_RECEIPT_HEADER.Cust_Code as Vendor_Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Vendor_Customer_Name,TSPL_RECEIPT_HEADER.Receipt_Date  as Invoice_Date,TSPL_RECEIPT_HEADER.CURRENCY_CODE  from TSPL_REVALUATION_DETAIL left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_REVALUATION_DETAIL.Receipt_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  where TSPL_REVALUATION_DETAIL.Document_No='" + obj.Document_No + "' ORDER BY TSPL_REVALUATION_DETAIL.SNo "
            Else
                Throw New Exception("Document Type is not correcct")
            End If
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsRevaluationDetail)
                Dim objTr As clsRevaluationDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsRevaluationDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                    objTr.AP_Invoice_No = clsCommon.myCstr(dr("AP_Invoice_No"))
                    objTr.AR_Invoice_No = clsCommon.myCstr(dr("AR_Invoice_No"))
                    objTr.Receipt_No = clsCommon.myCstr(dr("Receipt_No"))
                    objTr.Payment_No = clsCommon.myCstr(dr("Payment_No"))
                    objTr.Trans_Sub_Type = clsCommon.myCstr(dr("Trans_Sub_Type"))
                    objTr.Revaluate_Amount = clsCommon.myCdbl(dr("Revaluate_Amount"))
                    objTr.Balance_Amount = clsCommon.myCdbl(dr("Balance_Amount"))
                    objTr.Gain_Amount = clsCommon.myCdbl(dr("Gain_Amount"))
                    objTr.Loss_Amount = clsCommon.myCdbl(dr("Loss_Amount"))
                    objTr.Vendor_Customer_Code = clsCommon.myCstr(dr("Vendor_Customer_Code"))
                    objTr.Vendor_Customer_Name = clsCommon.myCstr(dr("Vendor_Customer_Name"))
                    objTr.Invoice_Date = clsCommon.myCDate(dr("Invoice_Date"))
                    objTr.Invoice_Amount = clsCommon.myCdbl(dr("Document_Total"))
                    objTr.Company_Currency_Amount = clsCommon.myCdbl(dr("BaseCurrencyAmt"))
                    objTr.Loc_Segment = clsCommon.myCstr(dr("Loc_Code"))
                    objTr.Tran_Conv_Rate = clsCommon.myCdbl(dr("Tran_Conv_Rate"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsRevaluationHead = clsRevaluationHead.GetData(strDocNo, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If obj.Status = ERPTransactionStatus.Approved Then
                    Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(clsCommon.myCDate(obj.Posted_Date), "dd/MM/yyyy"))
                End If
                Dim qry As String = "delete from TSPL_REVALUATION_DETAIL where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_REVALUATION_HEAD where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        End If
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsRevaluationHead = clsRevaluationHead.GetData(strDocNo, NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0 Then
                Throw New Exception("Document no not found to post")
            End If
            If obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Transaction is already posted on " + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            For Each objtr As clsRevaluationDetail In obj.Arr
                If clsCommon.CompairString(obj.Trans_Type, "AP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Trans_Type, "PY") = CompairStringResult.Equal Then
                    CreateAPInvoice(obj, objtr, trans)
                ElseIf clsCommon.CompairString(obj.Trans_Type, "AR") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Trans_Type, "RC") = CompairStringResult.Equal Then
                    CreateARInvoice(obj, objtr, trans)
                Else
                    Throw New Exception("Wrong Transaction Type")
                End If
            Next

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REVALUATION_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Sub CreateAPInvoice(ByVal obj As clsRevaluationHead, ByVal objtr As clsRevaluationDetail, ByVal trans As SqlTransaction)
        Dim objVendInv As New clsVedorInvoiceHead()
        objVendInv.Document_No = ""
        objVendInv.Invoice_Entry_Date = obj.Document_Date
        Dim dblAmt As Double = 0
        If objtr.Gain_Amount > 0 Then
            objVendInv.Document_Type = "D"
            dblAmt = objtr.Gain_Amount
        Else
            objVendInv.Document_Type = "C"
            dblAmt = objtr.Loss_Amount
        End If
        objVendInv.Invoice_Type = "AP"
        objVendInv.loc_code = objtr.Loc_Segment
        objVendInv.Document_Total = dblAmt
        objVendInv.Vendor_Code = objtr.Vendor_Customer_Code
        objVendInv.Vendor_Name = objtr.Vendor_Customer_Name
        objVendInv.Posting_Date = obj.Document_Date
        objVendInv.Vendor_Invoice_Date = obj.Document_Date
        Dim strDocNo As String = ""
         
        If clsCommon.CompairString(obj.Trans_Type, "AP") = CompairStringResult.Equal Then
            objVendInv.Description = "Auto Generated Entry for Revalualtion No - " + obj.Document_No + " And Source Document No - " + objtr.AP_Invoice_No + " At Line No - " + clsCommon.myCstr(objtr.SNo)
        ElseIf clsCommon.CompairString(obj.Trans_Type, "PY") = CompairStringResult.Equal Then
            objVendInv.Description = "Auto Generated Entry for Revalualtion No - " + obj.Document_No + " And Source Document No - " + objtr.Payment_No + " At Line No - " + clsCommon.myCstr(objtr.SNo)
        End If

        Dim qry As String = " select TSPL_VENDOR_MASTER.Vendor_Account,TSPL_VENDOR_MASTER.Terms_Code,TSPL_TERMS_MASTER.Terms_Desc,TSPL_TERMS_MASTER.No_Days  from TSPL_VENDOR_MASTER left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_VENDOR_MASTER.Terms_Code where  TSPL_VENDOR_MASTER.Vendor_Code='" + objtr.Vendor_Customer_Code + "'"
        Dim dtVendor As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtVendor Is Nothing OrElse dtVendor.Rows.Count <= 0 Then
            Throw New Exception("Please set vendor account of " + objtr.Vendor_Customer_Code)
        End If

        objVendInv.Account_Set = clsCommon.myCstr(dtVendor.Rows(0)("Vendor_Account"))
        objVendInv.Terms_Code = clsCommon.myCstr(dtVendor.Rows(0)("Terms_Code"))
        objVendInv.Terms_Description = clsCommon.myCstr(dtVendor.Rows(0)("Terms_Desc"))
        objVendInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dtVendor.Rows(0)("No_Days")))

        objVendInv.On_Hold = 0
        objVendInv.Remarks = ""
        'objVendInv.Description = obj.Description
        objVendInv.Balance_Amt = dblAmt
        objVendInv.RefDocType = "REVALUATION ENTRY"
        objVendInv.RefDocNo = obj.Document_No
        objVendInv.ConvRate = 1
        objVendInv.CURRENCY_CODE = objCommonVar.BaseCurrencyCode
        objVendInv.Discount_Base = dblAmt
        objVendInv.Amount_Less_Discount = dblAmt
        objVendInv.Document_Total = dblAmt
        objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

        '' Detail Saving
        ''----------------------------------------------
        Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()
        Dim dtAC As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT, TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT FROM TSPL_VENDOR_MASTER LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Account  WHERE TSPL_VENDOR_MASTER.Vendor_Code ='" + objtr.Vendor_Customer_Code + "' ", trans)
        If dtAC Is Nothing AndAlso dtAC.Rows.Count <= 0 Then
            Throw New Exception("Please set vendor account set")
        End If
        Dim strAccount As String = ""

        If objtr.Gain_Amount > 0 Then
            If clsCommon.myLen(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT")) <= 0 Then
                Throw New Exception("Please set Exchage gain account for vendor" + objtr.Vendor_Customer_Code)
            End If
            strAccount = clsCommon.myCstr(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))

        ElseIf objtr.Loss_Amount > 0 Then
            If clsCommon.myLen(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT")) <= 0 Then
                Throw New Exception("Please set Exchage Loss account for vendor" + objtr.Vendor_Customer_Code)
            End If
            strAccount = clsCommon.myCstr(dtAC.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
        End If
        strAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strAccount, objVendInv.loc_code, True, trans)

        objVendInvTR.Detail_Line_No = 1


        objVendInvTR.Amount = dblAmt
        objVendInvTR.Amount_less_Discount = dblAmt
        objVendInvTR.Total_Amount = dblAmt


        objVendInvTR.GL_Account_Code = strAccount
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendInv.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendInv.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Vendor_Control_AC, objVendInv.loc_code, True, trans)
        End If
        If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If

        objVendInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & strAccount & "'", trans))
        objVendInv.Arr.Add(objVendInvTR)

        ''----------------------------------------------

        objVendInv.SaveData(objVendInv, True, trans)
        clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
    End Sub

    Private Shared Sub CreateARInvoice(ByVal obj As clsRevaluationHead, ByVal objtr As clsRevaluationDetail, ByVal trans As SqlTransaction)
        Dim objCustInv As New clsCustomerInvoiceHead()
        objCustInv.Document_No = ""
        objCustInv.Document_Date = obj.Document_Date
        Dim dblAmt As Double = 0
        If objtr.Gain_Amount > 0 Then
            objCustInv.Document_Type = "D"
            dblAmt = objtr.Gain_Amount
        Else
            objCustInv.Document_Type = "C"
            dblAmt = objtr.Loss_Amount
        End If
        'objVendInv.Invoice_Type = "AP"
        objCustInv.loc_code = objtr.Loc_Segment
        objCustInv.Document_Total = dblAmt
        objCustInv.Customer_Code = objtr.Vendor_Customer_Code
        objCustInv.Customer_Name = objtr.Vendor_Customer_Name
        objCustInv.Posting_Date = obj.Document_Date


        Dim qry As String = " select TSPL_CUSTOMER_MASTER.Cust_Account,TSPL_CUSTOMER_MASTER.Terms_Code,TSPL_TERMS_MASTER.Terms_Desc,TSPL_TERMS_MASTER.No_Days from TSPL_CUSTOMER_MASTER left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code where TSPL_CUSTOMER_MASTER.Cust_Code = '" + objtr.Vendor_Customer_Code + "'"
        Dim dtCutomer As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtCutomer Is Nothing OrElse dtCutomer.Rows.Count <= 0 Then
            Throw New Exception("Please set customer account of " + objtr.Vendor_Customer_Code)
        End If
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)

        If clsCommon.CompairString(obj.Trans_Type, "AR") = CompairStringResult.Equal Then
            objCustInv.Description = "Revalualtion No - " + obj.Document_No + " And Document No - " + objtr.AR_Invoice_No + " At Line No - " + clsCommon.myCstr(objtr.SNo)
        ElseIf clsCommon.CompairString(obj.Trans_Type, "RC") = CompairStringResult.Equal Then
            objCustInv.Description = "Revalualtion No - " + obj.Document_No + " And Document No - " + objtr.Receipt_No + " At Line No - " + clsCommon.myCstr(objtr.SNo)
        End If
        objCustInv.Account_Set = clsCommon.myCstr(dtCutomer.Rows(0)("Cust_Account"))
        objCustInv.Terms_Code = clsCommon.myCstr(dtCutomer.Rows(0)("Terms_Code"))
        objCustInv.Terms_Description = clsCommon.myCstr(dtCutomer.Rows(0)("Terms_Desc"))
        objCustInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dtCutomer.Rows(0)("No_Days")))
        objCustInv.On_Hold = 0
        objCustInv.Remarks = ""
        'objCustInv.Description = obj.Description
        objCustInv.Balance_Amt = dblAmt
        objCustInv.RefDocType = "REVALUATION ENTRY"
        objCustInv.RefDocNo = obj.Document_No
        objCustInv.ConvRate = 1
        objCustInv.CURRENCY_CODE = objCommonVar.BaseCurrencyCode
        objCustInv.Discount_Base = dblAmt
        objCustInv.Amount_Less_Discount = dblAmt
        objCustInv.Document_Total = dblAmt

        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)

        '' Detail Saving
        ''----------------------------------------------
        Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
        Dim dtAC As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT, TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT FROM  TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account =TSPL_CUSTOMER_MASTER.Cust_Account  WHERE TSPL_CUSTOMER_MASTER.Cust_Code ='" + objtr.Vendor_Customer_Code + "' ", trans)
        If dtAC Is Nothing OrElse dtAC.Rows.Count <= 0 Then
            Throw New Exception("Please set customer account set")
        End If
        Dim strAccount As String = ""

        If objtr.Gain_Amount > 0 Then
            If clsCommon.myLen(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT")) <= 0 Then
                Throw New Exception("Please set Exchage gain account for vendor" + objtr.Vendor_Customer_Code)
            End If
            strAccount = clsCommon.myCstr(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))

        ElseIf objtr.Loss_Amount > 0 Then
            If clsCommon.myLen(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT")) <= 0 Then
                Throw New Exception("Please set Exchage Loss account for vendor" + objtr.Vendor_Customer_Code)
            End If
            strAccount = clsCommon.myCstr(dtAC.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
        End If
        strAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strAccount, objCustInv.loc_code, True, trans)

        objCustInvTR.SNo = 1
        objCustInvTR.Amount = dblAmt
        objCustInvTR.Amount_less_Discount = dblAmt
        objCustInvTR.Total_Amount = dblAmt


        objCustInvTR.GL_Account_Code = strAccount
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Receivable_Control_acct  FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + objtr.Vendor_Customer_Code + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
            objCustInv.Customer_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.Customer_Control_AC, objCustInv.loc_code, True, trans)
        End If
        If clsCommon.myLen(objCustInv.Customer_Control_AC) <= 0 Then
            Throw New Exception("Please set the customer control Account for " + objCustInv.Customer_Code)
        End If

        objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & strAccount & "'", trans))
        objCustInv.Arr.Add(objCustInvTR)

        '----------------------------------------------

        objCustInv.SaveData(objCustInv, True, trans, "")
        clsCustomerInvoiceHead.PostData("", objCustInv.Document_No, "", trans)
    End Sub
End Class

Public Class clsRevaluationDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public SNo As Integer
    Public AP_Invoice_No As String = Nothing
    Public AR_Invoice_No As String = Nothing
    Public Receipt_No As String = Nothing
    Public Payment_No As String = Nothing
    Public Revaluate_Amount As Decimal
    Public Balance_Amount As Decimal
    Public Gain_Amount As Decimal
    Public Loss_Amount As Decimal
    Public Trans_Sub_Type As String
    Public Tran_Conv_Rate As Decimal
    Public Vendor_Customer_Code As String = Nothing ''Not a Table Column
    Public Vendor_Customer_Name As String = Nothing ''Not a Table Column
    Public Invoice_Date As DateTime ''Not a Table Column
    Public Invoice_Amount As Decimal ''Not a Table Column
    Public Company_Currency_Amount As Decimal ''Not a Table Column
    Public Loc_Segment As String ''Not a Table Column
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsRevaluationDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsRevaluationDetail In Arr
                Dim qry As String = "select TSPL_REVALUATION_DETAIL.Document_No,TSPL_REVALUATION_DETAIL.SNo from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No where TSPL_REVALUATION_HEAD.Status=0 and TSPL_REVALUATION_DETAIL.Document_No not in ('" + obj.Document_No + "')"
                If clsCommon.myLen(obj.AP_Invoice_No) > 0 Then
                    qry += "and TSPL_REVALUATION_DETAIL.AP_Invoice_No='" + obj.AP_Invoice_No + "' "
                ElseIf clsCommon.myLen(obj.AR_Invoice_No) > 0 Then
                    qry += "and TSPL_REVALUATION_DETAIL.AR_Invoice_No='" + obj.AR_Invoice_No + "' "
                ElseIf clsCommon.myLen(obj.Receipt_No) > 0 Then
                    qry += "and TSPL_REVALUATION_DETAIL.Receipt_No='" + obj.Receipt_No + "' "
                ElseIf clsCommon.myLen(obj.Payment_No) > 0 Then
                    qry += "and TSPL_REVALUATION_DETAIL.Payment_No='" + obj.Payment_No + "' "
                End If
                Dim dtCheck As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtCheck IsNot Nothing AndAlso dtCheck.Rows.Count > 0 Then
                    Dim strError As String = "Unposed revaluation entry found Document"
                    For Each dr As DataRow In dtCheck.Rows
                        strError += Environment.NewLine + "Revaluation No-" + clsCommon.myCstr(dr("Document_No")) + ".Document No- " + clsCommon.myCstr(obj.AP_Invoice_No) + clsCommon.myCstr(obj.AR_Invoice_No) + clsCommon.myCstr(obj.Receipt_No) + clsCommon.myCstr(obj.Payment_No) + ".At SNo-" + clsCommon.myCstr(dr("SNo"))
                    Next
                    Throw New Exception(strError)
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", obj.AP_Invoice_No, True)
                clsCommon.AddColumnsForChange(coll, "AR_Invoice_No", obj.AR_Invoice_No, True)
                clsCommon.AddColumnsForChange(coll, "Receipt_No", obj.Receipt_No, True)
                clsCommon.AddColumnsForChange(coll, "Payment_No", obj.Payment_No, True)
                clsCommon.AddColumnsForChange(coll, "Trans_Sub_Type", obj.Trans_Sub_Type)
                clsCommon.AddColumnsForChange(coll, "Revaluate_Amount", obj.Revaluate_Amount)
                clsCommon.AddColumnsForChange(coll, "Balance_Amount", obj.Balance_Amount)
                clsCommon.AddColumnsForChange(coll, "Gain_Amount", obj.Gain_Amount)
                clsCommon.AddColumnsForChange(coll, "Loss_Amount", obj.Loss_Amount)
                clsCommon.AddColumnsForChange(coll, "Tran_Conv_Rate", obj.Tran_Conv_Rate)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REVALUATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

