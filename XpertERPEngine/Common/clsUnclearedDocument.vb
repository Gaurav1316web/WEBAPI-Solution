Imports System.Data.SqlClient

Public Class clsUnclearedDocumentHead
#Region "Variables"
    Public DOC_No As String = Nothing
    Public DOC_Date As DateTime = Nothing
    Public Doc_Type As String = Nothing
    Public Business_Partner_Code As String = Nothing
    Public Business_Partner_Name As String = Nothing
    Public Cleared_Doc_No As String = Nothing
    Public Remarks As String = Nothing
    Public Doc_Amount As Decimal
    Public Apply_Amount As Decimal
    Public Outstand_Amount As Decimal
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Arr As List(Of clsUnclearedDocumentDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsUnclearedDocumentHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsUnclearedDocumentHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_Uncleared_Doc_Detail where DOC_No='" + obj.DOC_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry Then
                obj.DOC_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.DOC_Date), clsDocType.UnclearedDoc, "", "")
            End If
            If (clsCommon.myLen(obj.DOC_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_Date", clsCommon.GetPrintDate(obj.DOC_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Business_Partner_Code", obj.Business_Partner_Code)
            clsCommon.AddColumnsForChange(coll, "Cleared_Doc_No", obj.Cleared_Doc_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Doc_Amount", obj.Doc_Amount)
            clsCommon.AddColumnsForChange(coll, "Apply_Amount", obj.Apply_Amount)
            clsCommon.AddColumnsForChange(coll, "Outstand_Amount", obj.Outstand_Amount)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "DOC_No", obj.DOC_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Uncleared_Doc_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Uncleared_Doc_Head", OMInsertOrUpdate.Update, "TSPL_Uncleared_Doc_Head.DOC_No='" + obj.DOC_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsUnclearedDocumentDetail.SaveData(obj.DOC_No, Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsUnclearedDocumentHead

        Dim obj As clsUnclearedDocumentHead = Nothing
        Dim qry As String = "SELECT TSPL_Uncleared_Doc_Head.*,case when TSPL_Uncleared_Doc_Head.Doc_Type='R' then TSPL_CUSTOMER_MASTER.Customer_Name else tspl_Vendor_master.Vendor_Name  end as Business_Partner_Name  FROM TSPL_Uncleared_Doc_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Uncleared_Doc_Head.Business_Partner_Code left outer join tspl_Vendor_master on tspl_Vendor_master.vendor_code=TSPL_Uncleared_Doc_Head.Business_Partner_Code where 2=2  "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Uncleared_Doc_Head.DOC_No = (select MIN(DOC_No) from TSPL_Uncleared_Doc_Head where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_Uncleared_Doc_Head.DOC_No = (select Max(DOC_No) from TSPL_Uncleared_Doc_Head where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_Uncleared_Doc_Head.DOC_No= (select Min(DOC_No) from TSPL_Uncleared_Doc_Head where DOC_No>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Uncleared_Doc_Head.DOC_No= (select Max(DOC_No) from TSPL_Uncleared_Doc_Head where DOC_No<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_Uncleared_Doc_Head.DOC_No= '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsUnclearedDocumentHead()
            obj.DOC_No = clsCommon.myCstr(dt.Rows(0)("DOC_No"))
            obj.DOC_Date = clsCommon.myCstr(dt.Rows(0)("DOC_Date"))
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Business_Partner_Code = clsCommon.myCstr(dt.Rows(0)("Business_Partner_Code"))
            obj.Business_Partner_Name = clsCommon.myCstr(dt.Rows(0)("Business_Partner_Name"))
            obj.Cleared_Doc_No = clsCommon.myCstr(dt.Rows(0)("Cleared_Doc_No"))

            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Doc_Amount = clsCommon.myCdbl(dt.Rows(0)("Doc_Amount"))
            obj.Apply_Amount = clsCommon.myCdbl(dt.Rows(0)("Apply_Amount"))
            obj.Outstand_Amount = clsCommon.myCdbl(dt.Rows(0)("Outstand_Amount"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            qry = "SELECT TSPL_Uncleared_Doc_Detail.*,(select Payment_Date from TSPL_PAYMENT_HEADER where Payment_No=TSPL_Uncleared_Doc_Detail.Cleared_Doc_No) as  PaymentDate ,(select Receipt_Date from TSPL_RECEIPT_HEADER where Receipt_No=TSPL_Uncleared_Doc_Detail.Cleared_Doc_No) as ReceiptDate FROM TSPL_Uncleared_Doc_Detail where TSPL_Uncleared_Doc_Detail.DOC_No='" + obj.DOC_No + "' ORDER BY TSPL_Uncleared_Doc_Detail.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsUnclearedDocumentDetail)
                Dim objTr As clsUnclearedDocumentDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsUnclearedDocumentDetail
                    objTr.DOC_No = clsCommon.myCstr(dr("DOC_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Cleared_Doc_No = clsCommon.myCstr(dr("Cleared_Doc_No"))
                    If clsCommon.CompairString(obj.Doc_Type, "R") = CompairStringResult.Equal Then
                        objTr.Cleared_Doc_Date = clsCommon.myCstr(dr("ReceiptDate"))
                    Else
                        objTr.Cleared_Doc_Date = clsCommon.myCstr(dr("PaymentDate"))
                    End If
                    objTr.Doc_Amount = clsCommon.myCdbl(dr("Doc_Amount"))
                    objTr.Apply_Amount = clsCommon.myCstr(dr("Apply_Amount"))
                    objTr.Outstand_Amount = clsCommon.myCstr(dr("Outstand_Amount"))
                    objTr.Date_To_Be = clsCommon.myCDate(dr("Date_To_Be"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim objDocSeq As New clsDocumentSequence
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Docuemnt No not found to Post")
            End If
            Dim obj As clsUnclearedDocumentHead = clsUnclearedDocumentHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post Document")
            End If
            Dim qry As String = ""
            If obj.Outstand_Amount = 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where source_doc_no='" + obj.Cleared_Doc_No + "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where source_doc_no='" + obj.Cleared_Doc_No + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                If clsCommon.CompairString(obj.Doc_Type, "R") = CompairStringResult.Equal Then
                    qry = "delete from TSPL_RECEIPT_DETAIL where Receipt_No='" + obj.Cleared_Doc_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_RECEIPT_HEADER where Receipt_No='" + obj.Cleared_Doc_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    qry = "delete from TSPL_PAYMENT_DETAIL where Payment_No='" + obj.Cleared_Doc_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_PAYMENT_HEADER where Payment_No='" + obj.Cleared_Doc_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            Else
                qry = "update TSPL_JOURNAL_DETAILS set amount= case when amount<0 then -1 else 1 end * " + clsCommon.myCstr(obj.Outstand_Amount) + " where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where source_doc_no='" + obj.Cleared_Doc_No + "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_JOURNAL_MASTER set Total_Debit_Amt=" + clsCommon.myCstr(obj.Outstand_Amount) + ",Total_Credit_Amt=" + clsCommon.myCstr(obj.Outstand_Amount) + " where source_doc_no='" + obj.Cleared_Doc_No + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                If clsCommon.CompairString(obj.Doc_Type, "R") = CompairStringResult.Equal Then
                    qry = "update TSPL_RECEIPT_HEADER set Receipt_Amount=" + clsCommon.myCstr(obj.Outstand_Amount) + ",Balance_Amt=" + clsCommon.myCstr(obj.Outstand_Amount) + ",UnApply_Amt=" + clsCommon.myCstr(obj.Outstand_Amount) + "  where Receipt_No='" + obj.Cleared_Doc_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    qry = "update TSPL_PAYMENT_HEADER set Payment_Amount=" + clsCommon.myCstr(obj.Outstand_Amount) + ",Balance_Amt=" + clsCommon.myCstr(obj.Outstand_Amount) + " where Payment_No='" + obj.Cleared_Doc_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
            For Each objtr In obj.Arr
                If clsCommon.CompairString(obj.Doc_Type, "R") = CompairStringResult.Equal Then
                    qry = "update TSPL_RECEIPT_HEADER set Receipt_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "',Receipt_Post_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy") + "',Created_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MM/yyyy") + "',Modify_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MM/yyyy") + "' where Receipt_No='" + objtr.Cleared_Doc_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_BANK_BOOK set SOURCEDOC_DATE='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy") + "' where SOURCEDOC_NO='" + objtr.Cleared_Doc_No + "' and DocType in ('Receipt')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update tspl_BankReco_Detail set Document_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy") + "' where Document_No='" + objtr.Cleared_Doc_No + "' and Document_Type in ('Receipt')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    clsBankReco.SetOutstandingEntry(objtr.Cleared_Doc_No, objtr.Date_To_Be, "Receipt", trans)

                    qry = "update TSPL_JOURNAL_MASTER set Voucher_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "',Source_Doc_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "',Posting_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "' where Source_Doc_No='" + objtr.Cleared_Doc_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    ''---------on 21/06/2016 by balwinder for change receipt is month wise or day wise counter set

                    qry = "select TSPL_BANK_MASTER.Bank_type,TSPL_BANK_MASTER.BANKACC,TSPL_RECEIPT_HEADER.Receipt_Type,TSPL_BANK_MASTER.BANK_CODE from TSPL_RECEIPT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code  where TSPL_RECEIPT_HEADER.Receipt_No='" + objtr.Cleared_Doc_No + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Document No " + objtr.Cleared_Doc_No + " not found")
                    End If
                    Dim strBankCode As String = clsCommon.myCstr(dt.Rows(0)("BANK_CODE"))
                    Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                    Dim BankAcc As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                    Dim strReceiptType As String = clsCommon.myCstr(dt.Rows(0)("Receipt_Type"))
                    Dim strNewDocNo As String = ""
                    If (BankAcc.Length >= 3) Then
                        BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                    Else
                        Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                    End If
                    Dim isApplyMonthly As Boolean = False
                    Dim strType As String = clsDocType.Receipt
                    If clsCommon.CompairString(strReceiptType, "F") = CompairStringResult.Equal Then
                        strType = clsDocType.Payment
                    End If

                    If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Bank, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Bank, BankAcc, True)
                        End If

                    ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Cash, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Cash, BankAcc, True)
                        End If

                    ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.PettyCash, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.PettyCash, BankAcc, True)
                        End If
                    ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Others, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Others, BankAcc, True)
                        End If
                    ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Settlement, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Settlement, BankAcc, True)
                        End If
                    Else
                        Throw New Exception("Please set the Bank Type for Bank " + strBankCode)
                    End If

                    If isApplyMonthly Then
                        If clsCommon.myLen(strNewDocNo) <= 0 Then
                            Throw New Exception("Error in document Generation for doc no" + objtr.Cleared_Doc_No)
                        End If
                        objDocSeq.UpdateReceipt(objtr.Cleared_Doc_No, strNewDocNo, trans)
                        clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST(S_NO,OLD_NO,NEW_NO,Trans_Code,Hist_By,Hist_Date) values('UCD0001','" + objtr.Cleared_Doc_No + "','" + strNewDocNo + "','ReceiptEntry','" + objCommonVar.CurrentUserCode + "',GETDATE())", trans)
                    End If
                Else
                    qry = "update TSPL_PAYMENT_HEADER set Payment_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "',Payment_Post_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "',Created_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "',Modify_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "' where Payment_No='" + objtr.Cleared_Doc_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_BANK_BOOK set SOURCEDOC_DATE='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy") + "' where SOURCEDOC_NO='" + objtr.Cleared_Doc_No + "' and DocType in ('Payment')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update tspl_BankReco_Detail set Document_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy") + "' where Document_No='" + objtr.Cleared_Doc_No + "' and Document_Type in ('Payment')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    clsBankReco.SetOutstandingEntry(objtr.Cleared_Doc_No, objtr.Date_To_Be, "Payment", trans)

                    qry = "update TSPL_JOURNAL_MASTER set Voucher_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "',Source_Doc_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "',Posting_Date='" + clsCommon.GetPrintDate(objtr.Date_To_Be, "dd/MMM/yyyy hh:mm tt") + "' where Source_Doc_No='" + objtr.Cleared_Doc_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    ''---------on 29/06/2016 by balwinder for change Payment is month wise or day wise counter set

                    qry = "select TSPL_BANK_MASTER.Bank_type,TSPL_BANK_MASTER.BANKACC,TSPL_PAYMENT_HEADER.Payment_Type,TSPL_BANK_MASTER.BANK_CODE from TSPL_PAYMENT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code  where TSPL_PAYMENT_HEADER.Payment_No='" + objtr.Cleared_Doc_No + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Document No " + objtr.Cleared_Doc_No + " not found")
                    End If
                    Dim strBankCode As String = clsCommon.myCstr(dt.Rows(0)("BANK_CODE"))
                    Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                    Dim BankAcc As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                    Dim strReceiptType As String = clsCommon.myCstr(dt.Rows(0)("Payment_Type"))
                    Dim strNewDocNo As String = ""
                    If (BankAcc.Length >= 3) Then
                        BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                    Else
                        Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                    End If
                    Dim isApplyMonthly As Boolean = False

                    Dim strType As String = clsDocType.Payment
                    If clsCommon.CompairString(strReceiptType, "RC") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PaymentReceiptTypeRunReceiptCounter, clsFixedParameterCode.PaymentReceiptTypeRunReceiptCounter, trans)) = 1 Then
                        strType = clsDocType.Receipt
                    End If
                    If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Bank, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Bank, BankAcc, True)
                        End If
                    ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Cash, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Cash, BankAcc, True)
                        End If

                    ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.PettyCash, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.PettyCash, BankAcc, True)
                        End If

                    ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Others, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Others, BankAcc, True)
                        End If

                    ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
                        clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Others, BankAcc, True, False, False, isApplyMonthly)
                        If isApplyMonthly Then
                            strNewDocNo = clsERPFuncationality.GetNextCode(trans, objtr.Date_To_Be, strType, clsDocTransactionType.Others, BankAcc, True)
                        End If
                    Else
                        Throw New Exception("Please set the Bank Type for Bank " + strBankCode)
                    End If

                    If isApplyMonthly Then
                        If clsCommon.myLen(strNewDocNo) <= 0 Then
                            Throw New Exception("Error in document Generation for doc no" + objtr.Cleared_Doc_No)
                        End If
                        objDocSeq.UpdatePayment(objtr.Cleared_Doc_No, strNewDocNo, trans)
                        clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST(S_NO,OLD_NO,NEW_NO,Trans_Code,Hist_By,Hist_Date) values('UCD0001','" + objtr.Cleared_Doc_No + "','" + strNewDocNo + "','PaymentEntry','" + objCommonVar.CurrentUserCode + "',GETDATE())", trans)
                    End If
                End If

            Next
            qry = "Update TSPL_Uncleared_Doc_Head set Status=1,Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsUnclearedDocumentHead = clsUnclearedDocumentHead.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOC_No) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Post Document")
                End If
                Dim qry As String = "delete from TSPL_Uncleared_Doc_Detail where DOC_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Uncleared_Doc_Head where DOC_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
End Class

Public Class clsUnclearedDocumentDetail
#Region "Variables"
    Public DOC_No As String = Nothing
    Public Line_No As Integer = 0
    Public Cleared_Doc_No As String = Nothing
    Public Cleared_Doc_Date As DateTime ''Not a table column 
    Public Doc_Amount As Double = 0
    Public Apply_Amount As Double = 0
    Public Outstand_Amount As Double = 0
    Public Date_To_Be As DateTime
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsUnclearedDocumentDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsUnclearedDocumentDetail In Arr
                If obj.Doc_Amount <> 0 Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Line_No", ii)
                    clsCommon.AddColumnsForChange(coll, "DOC_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Cleared_Doc_No", obj.Cleared_Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Doc_Amount", obj.Doc_Amount)
                    clsCommon.AddColumnsForChange(coll, "Apply_Amount", obj.Apply_Amount)
                    clsCommon.AddColumnsForChange(coll, "Outstand_Amount", obj.Outstand_Amount)
                    clsCommon.AddColumnsForChange(coll, "Date_To_Be", clsCommon.GetPrintDate(obj.Date_To_Be, "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Uncleared_Doc_Detail", OMInsertOrUpdate.Insert, "", trans)
                    ii += 1
                End If
            Next
        End If
        Return True
    End Function
End Class

Public Class clsDocumentSequence
    Sub ExecuteNonQueryWithDropContraint(ByVal strQuery As String, ByVal tran As SqlTransaction)
line1:
        Try
            clsDBFuncationality.ExecuteNonQuery(strQuery, tran)
        Catch ex As Exception
            If ex.Message.Contains("statement conflicted with the REFERENCE constraint") Then
                Dim TestArray() As String = ex.Message.Split("""")
                For i As Integer = 0 To TestArray.Length - 1
                    clsDBFuncationality.ExecuteNonQuery("Alter table " + TestArray(5) + " drop constraint " + TestArray(1), tran)
                    GoTo line1
                Next
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

    Public Sub UpdateReceipt(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_RECEIPT_HEADER set Receipt_No='" + strToBeReplaceNo + "' where Receipt_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_RECEIPT_DETAIL set Receipt_No='" + strToBeReplaceNo + "' where Receipt_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("delete from TSPL_BANK_BOOK where SOURCEDOC_NO='" + strFindNo + "' and DocType='Receipt'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BANK_REVERSE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "' and Source_Type='AR'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_BankReco_Detail set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "' and Document_Type='Receipt'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_RECEIPT_HEADER set Applied_Receipt='" + strToBeReplaceNo + "' where Applied_Receipt='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Uncleared_Doc_Detail set Cleared_Doc_No='" + strToBeReplaceNo + "' where Cleared_Doc_No='" + strFindNo + "' and exists(select 1 from TSPL_UNCLEARED_DOC_HEAD where TSPL_UNCLEARED_DOC_HEAD.Doc_Type='R' and TSPL_UNCLEARED_DOC_HEAD.DOC_No=TSPL_Uncleared_Doc_Detail.DOC_No )", trans)

        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set TSPL_Customer_Invoice_Head.Description=REPLACE(TSPL_Customer_Invoice_Head.Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where  TSPL_Customer_Invoice_Head.RefDocType ='REVALUATION ENTRY' and TSPL_Customer_Invoice_Head.RefDocNo in (select distinct TSPL_REVALUATION_DETAIL.Document_No from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.Receipt_No = '" + strFindNo + "')", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_REVALUATION_DETAIL set TSPL_REVALUATION_DETAIL.Receipt_No='" + strToBeReplaceNo + "' where TSPL_REVALUATION_DETAIL.Receipt_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkWeighment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Weighment_Detail set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_QUALITY_CHECK set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_UNLOADING set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Cleaning set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Gate_Out set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Bulk_MILK_SRN set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_milk_transfer_in set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkGateEntry(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update Tspl_Gate_Entry_Details set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Weighment_Detail set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_QUALITY_CHECK set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_UNLOADING set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Cleaning set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Gate_Out set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Bulk_MILK_SRN set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_milk_transfer_in set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkQC(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_QUALITY_CHECK set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_transaction_approval set  Document_No ='" + strToBeReplaceNo + "' where Document_No ='" + strFindNo + "' and Program_Code='M-QC'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_qc_parameter_detail set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_UNLOADING set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Cleaning set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Gate_Out set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Bulk_MILK_SRN set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_milk_transfer_in set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkSRN(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Bulk_MILK_SRN  set SRN_No='" + strToBeReplaceNo + "' where SRN_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_Bulk_milk_purchase_Invoice_Detail set  SRN_No='" + strToBeReplaceNo + "' where SRN_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_transaction_approval set Document_No ='" + strToBeReplaceNo + "' where Program_Code = 'M-SRN-B' and Document_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set  source_doc_no ='" + strToBeReplaceNo + "' where source_doc_no ='" + strFindNo + "' and trans_type = 'BulkSRN'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "' and Source_Code = 'BM-SR'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_RGP_Detail set  Bulk_Milk_SRN_Code='" + strToBeReplaceNo + "' where Bulk_Milk_SRN_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_ADJUSTMENT_HEADER set  Against_Bulk_Srn_PI_adjustment='" + strToBeReplaceNo + "' where Against_Bulk_Srn_PI_adjustment='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_vendor_invoice_head set  Description=REPLACE( Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where Description like '%" + strFindNo + "%'", trans)
    End Sub

    Public Sub UpdateBulkPI(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update tspl_Bulk_milk_purchase_Invoice_head  set Doc_no='" + strToBeReplaceNo + "' where Doc_no='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_Bulk_milk_purchase_Invoice_detail  set Doc_no='" + strToBeReplaceNo + "' where Doc_no='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_vendor_invoice_head set Against_BulkMillkPurchaseInvoice_No='" + strToBeReplaceNo + "', Description=REPLACE( Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where Against_BulkMillkPurchaseInvoice_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_ADJUSTMENT_HEADER set Against_Bulk_Srn_PI_adjustment='" + strToBeReplaceNo + "', Description=REPLACE( Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where Against_Bulk_Srn_PI_adjustment ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "' and Source_Code = 'BM-PI'", trans)
    End Sub

    Public Sub UpdateBulkSaleEntry(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEENTRY_SALE  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_WEIGHMENT_DETAIL_BULKSALE  set GateEntry_Document_No='" + strToBeReplaceNo + "' where GateEntry_Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Quality_Check_BulkSale set GateEntry_Document_No='" + strToBeReplaceNo + "' where GateEntry_Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEOUT_SALE set  GateEntryNo='" + strToBeReplaceNo + "' where GateEntryNo='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SALE_RETURN_MASTER_BULKSALE set  GateEntryNo='" + strToBeReplaceNo + "' where GateEntryNo='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEENTRY_SALE set  SaleReturnAgaintGEN='" + strToBeReplaceNo + "' where SaleReturnAgaintGEN='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkSaleWeighment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_WEIGHMENT_DETAIL_BULKSALE  set Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_LOADING_TANKER_DETAIL_BULKSALE  set Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Quality_Check_BulkSale set Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkSaleLoading(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_LOADING_TANKER_DETAIL_BULKSALE  set LoadingTanker_No='" + strToBeReplaceNo + "' where LoadingTanker_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Quality_Check_BulkSale  set LoadingTanker_No='" + strToBeReplaceNo + "' where LoadingTanker_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkSaleQC(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Quality_Check_BulkSale  set QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Dispatch_BulkSale  set QC_Code='" + strToBeReplaceNo + "' where QC_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_QC_Parameter_Detail_BulKSALE  set QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkSaleDispatch(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Dispatch_BulkSale  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Dispatch_Detail_BulkSale  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Remarks=REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "'),Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "' and Source_Code = 'DS-BS'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SALE_RETURN_MASTER_BULKSALE set DispatchNo='" + strToBeReplaceNo + "' where DispatchNo ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SALE_RETURN_DETAIL_BULKSALE set Dispatch_Code='" + strToBeReplaceNo + "' where Dispatch_Code ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEENTRY_SALE set Dispatch_No='" + strToBeReplaceNo + "' where Dispatch_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVOICE_DETAIL_BULKSALE set Dispatch_Code='" + strToBeReplaceNo + "' where Dispatch_Code ='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkSaleInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_INVOICE_MASTER_BULKSALE  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVOICE_DETAIL_BULKSALE  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Against_Sale_No='" + strToBeReplaceNo + "' where Against_Sale_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SALE_RETURN_MASTER_BULKSALE set InvoiceNo='" + strToBeReplaceNo + "' where InvoiceNo ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Remarks =REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "') where Remarks like'%" + strFindNo + "%' and Source_Code = 'AR-IN'", trans)
    End Sub

    Public Sub UpdateBulkGateOut(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEOUT_SALE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateARInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Detail set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_RECEIPT_DETAIL set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "'", trans)

        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set TSPL_Customer_Invoice_Head.Description=REPLACE(TSPL_Customer_Invoice_Head.Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where  TSPL_Customer_Invoice_Head.RefDocType ='REVALUATION ENTRY' and TSPL_Customer_Invoice_Head.RefDocNo in (select distinct TSPL_REVALUATION_DETAIL.Document_No from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.AR_Invoice_No = '" + strFindNo + "')", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_REVALUATION_DETAIL set TSPL_REVALUATION_DETAIL.AR_Invoice_No='" + strToBeReplaceNo + "' where TSPL_REVALUATION_DETAIL.AR_Invoice_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateAPInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_VENDOR_INVOICE_HEAD set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_VENDOR_INVOICE_DETAIL set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_REMITTANCE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PAYMENT_DETAIL set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "'", trans)

        ExecuteNonQueryWithDropContraint("update TSPL_VENDOR_INVOICE_HEAD set TSPL_VENDOR_INVOICE_HEAD.Description=REPLACE(TSPL_VENDOR_INVOICE_HEAD.Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where  TSPL_VENDOR_INVOICE_HEAD.RefDocType ='REVALUATION ENTRY' and TSPL_VENDOR_INVOICE_HEAD.RefDocNo in (select distinct TSPL_REVALUATION_DETAIL.Document_No from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.AP_Invoice_No = '" + strFindNo + "')", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_REVALUATION_DETAIL set TSPL_REVALUATION_DETAIL.AP_Invoice_No='" + strToBeReplaceNo + "' where TSPL_REVALUATION_DETAIL.AP_Invoice_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateJournalEntry(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Voucher_No='" + strToBeReplaceNo + "' where Voucher_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_DETAILS set Voucher_No='" + strToBeReplaceNo + "' where Voucher_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkPurchaseGateout(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSpl_Gate_Out set Doc_No='" + strToBeReplaceNo + "' where Doc_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateBulkPurchaseUnloading(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_UNLOADING set Unloading_No='" + strToBeReplaceNo + "' where Unloading_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdatePayment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_PAYMENT_HEADER set Payment_No='" + strToBeReplaceNo + "' where Payment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PAYMENT_DETAIL set Payment_No='" + strToBeReplaceNo + "' where Payment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("delete from TSPL_BANK_BOOK where SOURCEDOC_NO='" + strFindNo + "' and DocType='Payment'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BANK_REVERSE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "' and Source_Type='AP'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_BankReco_Detail set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "' and Document_Type='Payment'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "',Source_Narration=REPLACE( Source_Narration,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PAYMENT_HEADER set Applied_Payment='" + strToBeReplaceNo + "' where Applied_Payment='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Uncleared_Doc_Detail set Cleared_Doc_No='" + strToBeReplaceNo + "' where Cleared_Doc_No='" + strFindNo + "' and exists(select 1 from TSPL_UNCLEARED_DOC_HEAD where TSPL_UNCLEARED_DOC_HEAD.Doc_Type='P' and TSPL_UNCLEARED_DOC_HEAD.DOC_No=TSPL_Uncleared_Doc_Detail.DOC_No )", trans)


        ExecuteNonQueryWithDropContraint("update TSPL_VENDOR_INVOICE_HEAD set TSPL_VENDOR_INVOICE_HEAD.Description=REPLACE(TSPL_VENDOR_INVOICE_HEAD.Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where  TSPL_VENDOR_INVOICE_HEAD.RefDocType ='REVALUATION ENTRY' and TSPL_VENDOR_INVOICE_HEAD.RefDocNo in (select distinct TSPL_REVALUATION_DETAIL.Document_No from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.Payment_No = '" + strFindNo + "')", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_REVALUATION_DETAIL set TSPL_REVALUATION_DETAIL.Payment_No='" + strToBeReplaceNo + "' where TSPL_REVALUATION_DETAIL.Payment_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateStoreAdjustment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_ADJUSTMENT_DETAIL set  Adjustment_No ='" + strToBeReplaceNo + "' where Adjustment_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_ADJUSTMENT_HEADER set  Adjustment_No ='" + strToBeReplaceNo + "' where Adjustment_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set  source_doc_no ='" + strToBeReplaceNo + "' where source_doc_no ='" + strFindNo + "' and trans_type = 'IC-AD'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set  source_doc_no ='" + strToBeReplaceNo + "' where source_doc_no ='" + strFindNo + "' and trans_type = 'IC-AD'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "' and Source_Code = 'IC-AD'", trans)
    End Sub

    Public Sub UpdateMilkGateEntryIn(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_GATE_ENTRY_IN set Entry_Code='" + strToBeReplaceNo + "' where Entry_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_GATE_ENTRY_WEIGHTMENT set Against_Gate_Entry_No ='" + strToBeReplaceNo + "' where Against_Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_GATE_ENTRY_OUT set Against_Gate_Entry_No ='" + strToBeReplaceNo + "' where Against_Gate_Entry_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateMilkGateEntryWeighment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_GATE_ENTRY_WEIGHTMENT set Weighment_Code='" + strToBeReplaceNo + "' where Weighment_Code='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateMilkGateEntryOut(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_GATE_ENTRY_OUT set Gate_Out_Code ='" + strToBeReplaceNo + "' where Gate_Out_Code='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateMilkSRN(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        Dim strVSPCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code=(select VSP_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE='" + strFindNo + "')", trans))
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_SRN_HEAD  set DOC_CODE='" + strToBeReplaceNo + "' where DOC_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_SRN_DETAIL set  DOC_CODE='" + strToBeReplaceNo + "' where DOC_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_SAMPLE_DETAIL set  DOC_CODE='" + strToBeReplaceNo + "' where DOC_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Source_Doc_No ='" + strToBeReplaceNo + "' where Source_Doc_No ='" + strFindNo + "' and trans_type = 'MCC-MSRN'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc='Against Milk SRN ( " + strToBeReplaceNo + ")  For Vsp : " + strVSPCode + "' where Source_Doc_No='" + strFindNo + "' and Source_Code = 'MI-SR'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_RGP_Detail set  Bulk_Milk_SRN_Code='" + strToBeReplaceNo + "' where Bulk_Milk_SRN_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_PURCHASE_INVOICE_DETAIL set  SRN_CODE='" + strToBeReplaceNo + "' where SRN_CODE='" + strFindNo + "'", trans)
    End Sub

    ''-------------------For fresh sale
    Public Sub UpdateFSDispatchOrder(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BOOKING_DETAIL set Delivery_No='" + strToBeReplaceNo + "' where Delivery_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SHIPMENT_DETAIL set Delivery_Code='" + strToBeReplaceNo + "' where Delivery_Code='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateFSShipment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SHIPMENT_HEAD set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SHIPMENT_DETAIL set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "' and Trans_Type='FS-SH'", trans)
    End Sub

    Public Sub UpdateFSInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_INVOICE_HEAD set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_INVOICE_DETAIL set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Against_sale_No='" + strToBeReplaceNo + "' where Against_sale_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SHIPMENT_HEAD set Sale_Invoice_No='" + strToBeReplaceNo + "' where Sale_Invoice_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdateFSInvoiceReturn(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_RETURN_HEAD set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_RETURN_DETAIL set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Against_Sale_Return_No='" + strToBeReplaceNo + "' where Against_Sale_Return_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "' and Trans_Type='FS-SR'", trans)
    End Sub
    ''-------------------End of For fresh sale

    ''---------------------For Production
    Public Sub UpdatePPPlan(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PRODUCTION_PLAN_HEAD set plan_Code='" + strToBeReplaceNo + "' where plan_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PRODUCTION_PLAN_DETAIL set Plan_Code='" + strToBeReplaceNo + "' where Plan_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_BATCH_ORDER_HEAD set Plan_Code='" + strToBeReplaceNo + "' where Plan_Code='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdatePPBatchOrder(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_BATCH_ORDER_HEAD set Batch_Code='" + strToBeReplaceNo + "' where Batch_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_BATCH_ORDER_BOM_DETAIL set Batch_Code='" + strToBeReplaceNo + "' where Batch_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL set Batch_Code='" + strToBeReplaceNo + "' where Batch_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_ISSUE_HEAD set Batch_Code='" + strToBeReplaceNo + "' where Batch_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STANDARDIZATION_HEAD set Child_Batch_Code='" + strToBeReplaceNo + "' where Child_Batch_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STANDARDIZATION_HEAD set Main_Batch_Code='" + strToBeReplaceNo + "' where Main_Batch_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STAGE_PROCESS_HEAD set Main_Batch_Code='" + strToBeReplaceNo + "' where Main_Batch_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PRODUCTION_ENTRY set Batch_Code='" + strToBeReplaceNo + "' where Batch_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Batch_No='" + strToBeReplaceNo + "' where Batch_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Batch_No='" + strToBeReplaceNo + "' where Batch_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdatePPIssue(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_ISSUE_HEAD set Issue_Code='" + strToBeReplaceNo + "' where Issue_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_ISSUE_ITEM_DETAIL set Issue_Code='" + strToBeReplaceNo + "' where Issue_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_ISSUE_QC_DETAIL set Issue_Code='" + strToBeReplaceNo + "' where Issue_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_SP_ISSUE_ITEM_DETAIL set Issue_Code='" + strToBeReplaceNo + "' where Issue_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PE_ISSUE_ITEM_DETAIL set Issue_Code='" + strToBeReplaceNo + "' where Issue_Code='" + strFindNo + "'", trans)

    End Sub

    Public Sub UpdatePPStandization(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STANDARDIZATION_HEAD set Standardization_Code='" + strToBeReplaceNo + "' where Standardization_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL set Standardization_Code='" + strToBeReplaceNo + "' where Standardization_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STD_QC_LOG_SHEET set Standardization_Code='" + strToBeReplaceNo + "' where Standardization_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL set Standardization_Code='" + strToBeReplaceNo + "' where Standardization_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STD_QC_DETAIL set Standardization_Code='" + strToBeReplaceNo + "' where Standardization_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STD_STAGE_DETAIL set Standardization_Code='" + strToBeReplaceNo + "' where Standardization_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STD_ISSUE_ITEM_DETAIL set Standardization_Code='" + strToBeReplaceNo + "' where Standardization_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL set Standardization_Code='" + strToBeReplaceNo + "' where Standardization_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdatePPStageProcess(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STAGE_PROCESS_HEAD set STAGE_PROCESS_CODE='" + strToBeReplaceNo + "' where STAGE_PROCESS_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET set STAGE_PROCESS_CODE='" + strToBeReplaceNo + "' where STAGE_PROCESS_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL set STAGE_PROCESS_CODE='" + strToBeReplaceNo + "' where STAGE_PROCESS_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL set STAGE_PROCESS_CODE='" + strToBeReplaceNo + "' where STAGE_PROCESS_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_STAGE_PROCESS_DETAIL set STAGE_PROCESS_CODE='" + strToBeReplaceNo + "' where STAGE_PROCESS_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_SP_ISSUE_ITEM_DETAIL set STAGE_PROCESS_CODE='" + strToBeReplaceNo + "' where STAGE_PROCESS_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
    End Sub

    Public Sub UpdatePPProductionEntry(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PRODUCTION_ENTRY set PROD_ENTRY_CODE='" + strToBeReplaceNo + "' where PROD_ENTRY_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PRODUCTION_ENTRY_DETAIL set PROD_ENTRY_CODE='" + strToBeReplaceNo + "' where PROD_ENTRY_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL set PROD_ENTRY_CODE='" + strToBeReplaceNo + "' where PROD_ENTRY_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PE_ISSUE_ITEM_DETAIL set PROD_ENTRY_CODE='" + strToBeReplaceNo + "' where PROD_ENTRY_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PE_STAGE_DETAIL set PROD_ENTRY_CODE='" + strToBeReplaceNo + "' where PROD_ENTRY_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PE_STAGE_QC_LOG_SHEET set PROD_ENTRY_CODE='" + strToBeReplaceNo + "' where PROD_ENTRY_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PP_PE_QC_DETAIL set PROD_ENTRY_CODE='" + strToBeReplaceNo + "' where PROD_ENTRY_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)

    End Sub

    ''---------------------End of For Production


    ''-----------Balwinder on 10/08/2017 for document correction
    Public Sub UpdateDocumentSequenceOfFreshSaleInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_INVOICE_HEAD set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_INVOICE_DETAIL set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Remarks=REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "') where TSPL_JOURNAL_MASTER.Source_Code = 'AR-IN' and Source_Doc_No=(select Document_No from TSPL_Customer_Invoice_Head where Against_sale_No='" + strFindNo + "')", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Against_sale_No='" + strToBeReplaceNo + "' where Against_sale_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SHIPMENT_HEAD set Sale_Invoice_No='" + strToBeReplaceNo + "' where Sale_Invoice_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_RETURN_HEAD set Against_Invoice_No='" + strToBeReplaceNo + "' where Against_Invoice_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_RETURN_DETAIL set Invoice_Code='" + strToBeReplaceNo + "' where Invoice_Code='" + strFindNo + "'", trans)
    End Sub
    Public Sub UpdateDocumentSequenceOfFreshSaleDispatch(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SHIPMENT_HEAD set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SHIPMENT_detail set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Remarks=REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "') where TSPL_JOURNAL_MASTER.Source_Code = 'DS-FS' and Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "' and Trans_Type='FS-SH'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_INVOICE_HEAD set Against_Shipment_No='" + strToBeReplaceNo + "' where Against_Shipment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_INVOICE_DETAIL set Shipment_Code='" + strToBeReplaceNo + "' where Shipment_Code='" + strFindNo + "'", trans)
    End Sub
    Public Sub UpdateDocumentSequenceOfProductSale(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_INVOICE_HEAD set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_INVOICE_DETAIL set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Remarks=REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "') where TSPL_JOURNAL_MASTER.Source_Code = 'AR-IN' and Source_Doc_No=(select Document_No from TSPL_Customer_Invoice_Head where Against_sale_No='" + strFindNo + "')", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Against_sale_No='" + strToBeReplaceNo + "' where Against_sale_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SHIPMENT_HEAD set Sale_Invoice_No='" + strToBeReplaceNo + "' where Sale_Invoice_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_RETURN_HEAD set Against_Invoice_No='" + strToBeReplaceNo + "' where Against_Invoice_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_RETURN_DETAIL set Invoice_Code='" + strToBeReplaceNo + "' where Invoice_Code='" + strFindNo + "'", trans)
    End Sub


    Public Sub UpdateDocumentSequenceOfProductSaleReturn(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_RETURN_HEAD set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SD_SALE_RETURN_DETAIL set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Remarks=REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "') where TSPL_JOURNAL_MASTER.Source_Code = 'AR-CR' and Source_Doc_No=(select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_Return_No='" + strFindNo + "')", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Against_Sale_Return_No='" + strToBeReplaceNo + "' where Against_Sale_Return_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "' and Trans_Type='PS-SR'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BATCH_ITEM set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "' and Document_Type='PS-SR'", trans)
    End Sub

    Public Sub UpdateDocumentSequenceOfTransferOut(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_TRANSFER_ORDER_HEAD set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_TRANSFER_ORDER_DETAIL set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_TRANSFER_ORDER_HEAD set TransferOutNo='" + strToBeReplaceNo + "' where TransferOutNo='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "' and Trans_Type='Transfer'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BATCH_ITEM set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "' and Document_Type='Transfer'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "'),Remarks=REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "'", trans)
    End Sub


    Public Sub UpdateDocumentSequenceOfScrapInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_SCRAPINVOICE_HEAD set invoice_no='" + strToBeReplaceNo + "' where invoice_no='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SCRAPINVOICE_DETAIL set invoice_no='" + strToBeReplaceNo + "' where invoice_no='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set AgainstScrap='" + strToBeReplaceNo + "' where AgainstScrap='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "' and Trans_Type='ScrapIn'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BATCH_ITEM set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "' and Document_Type='ScrapIn'", trans)
    End Sub

    Public Sub UpdateDocumentSequenceOfCSATransfer(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_CSA_TRANSFER_HEAD set DOC_CODE='" + strToBeReplaceNo + "' where DOC_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_CSA_TRANSFER_DETAIL set DOC_CODE='" + strToBeReplaceNo + "' where DOC_CODE='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BATCH_ITEM set Document_Code='" + strToBeReplaceNo + "' where Document_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "'),Remarks=REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "') where TSPL_JOURNAL_MASTER.Source_Code = 'CS-TR' and Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_CSA_SALE_TRANSFER_DETAIL set Against_Transfer_Code='" + strToBeReplaceNo + "' where Against_Transfer_Code='" + strFindNo + "'", trans)
        ''No document found for csa retrun.
    End Sub

    ''-----------End of Balwinder on 10/08/2017 for document correction

    Public Sub UpdateDocumentSequenceOfGSTPurchaseTaxInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        clsDBFuncationality.ExecuteNonQuery("update tspl_vendor_invoice_head set Purchase_Tax_Invoice='" + strToBeReplaceNo + "' where Purchase_Tax_Invoice='" + strFindNo + "'", trans)
        clsDBFuncationality.ExecuteNonQuery("update TSPL_PI_HEAD set Purchase_Tax_Invoice='" + strToBeReplaceNo + "' where Purchase_Tax_Invoice='" + strFindNo + "'", trans)
        clsDBFuncationality.ExecuteNonQuery("update TSPL_MILK_PURCHASE_INVOICE_HEAD set Purchase_Tax_Invoice='" + strToBeReplaceNo + "' where Purchase_Tax_Invoice='" + strFindNo + "'", trans)
        clsDBFuncationality.ExecuteNonQuery("update TSPL_Bulk_MILK_PURCHASE_INVOICE_HEAD set Purchase_Tax_Invoice='" + strToBeReplaceNo + "' where Purchase_Tax_Invoice='" + strFindNo + "'", trans)

    End Sub
End Class