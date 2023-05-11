Imports common
Imports System.Data.SqlClient

Public Class clsJournalEntryHeader

#Region "Variables"
    Public IND_AS As Integer = 0
    Public Journal_No As Integer
    Public Voucher_No As String = Nothing
    Public Voucher_Date As DateTime
    Public Source_Code As String = Nothing
    Public Source_Desc As String = Nothing
    Public Source_Doc_No As String = Nothing
    Public Source_Doc_Date As DateTime?
    Public Posting_Date As DateTime?
    Public Voucher_Desc As String = Nothing
    Public Source_Narration As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public Auto_Reverse As String = "Y"
    Public Reverse_Date As String = Nothing
    Public Source_Type As String = Nothing
    Public CustVend_Code As String = Nothing
    Public CustVend_Name As String = Nothing
    Public Transaction_Type As String = Nothing
    Public Provisional_Post As String = Nothing
    Public Authorized As String = "N"
    Public Total_Debit_Amt As Double = 0
    Public Total_Credit_Amt As Double = 0
    Public Type As String = Nothing
    Public Segment_Code As String = Nothing
    Public MonthlyReverse As Integer = 0
    Public ProgramCode As String = ""

#End Region

    Public Shared Function fnAutoGenerateNo(ByVal trans As SqlTransaction, ByVal TranDate As Date) As String
        Return fnAutoGenerateNo(trans, TranDate, False, "")
    End Function
    Public Shared Function fnAutoGenerateNo(ByVal trans As SqlTransaction, ByVal TranDate As Date, ByVal isGLJE As Boolean, ByVal strLocSegmentCode As String) As String
        Dim strJournalNo As String = Nothing
        If isGLJE Then
            If Len(strLocSegmentCode) <= 0 Then
                Throw New Exception("First Account Should have location Segment")
            End If
            strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.JournalEntry, clsDocTransactionType.JournalEntryJLJE, strLocSegmentCode, True)
        Else
            strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.JournalEntry, clsDocTransactionType.JournalEntryOther, "")
        End If
        Return strJournalNo
    End Function
    Public Shared Function fnAutoGenerateReverseNo(ByVal trans As SqlTransaction, ByVal TranDate As Date, ByVal isGLJE As Boolean, ByVal strLocSegmentCode As String, ByVal MonthlyReverse As Integer) As String
        Dim strJournalNo As String = Nothing
        If isGLJE Then
            If Len(strLocSegmentCode) <= 0 Then
                Throw New Exception("First Account Should have location Segment")
            End If

            If MonthlyReverse = 1 Then
                strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.ReverseJournalEntry, clsDocTransactionType.JournalEntryJLJEReverseMonthly, strLocSegmentCode, True)
            Else
                strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.ReverseJournalEntry, clsDocTransactionType.JournalEntryJLJEReverseGeneral, strLocSegmentCode, True)
            End If
        Else
            'strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.JournalEntry, clsDocTransactionType.JournalEntryOther, "")
            If MonthlyReverse = 1 Then
                strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.ReverseJournalEntry, clsDocTransactionType.JournalEntryJLJEReverseMonthly, strLocSegmentCode, True)
            Else
                strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.ReverseJournalEntry, clsDocTransactionType.JournalEntryJLJEReverseGeneral, strLocSegmentCode, True)
            End If
        End If
        Return strJournalNo
    End Function
    Public Shared Function fnAutoGenerateNo(ByVal trans As SqlTransaction) As String
        Dim DefaltDate As Date = clsCommon.myCDate(connectSql.serverDate(trans), "dd/MM/yyyy")
        Return fnAutoGenerateNo(trans, DefaltDate)
    End Function

    Public Function AutoReverse(ByVal VoucherNo As String, ByVal VoucherDate As Date, ByVal MonthlyReverse As Integer) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            AutoReverse(VoucherNo, VoucherDate, trans, MonthlyReverse)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function AutoReverse(ByVal VoucherNo As String, ByVal VoucherDate As Date, ByVal trans As SqlTransaction, ByVal MonthlyReverse As Integer) As Boolean
        Dim dt As DataTable
        Dim Qry As String
        Try
            If clsCommon.myLen(VoucherNo) <= 0 Then
                Throw New Exception("Document not found to reverse.")
            Else
                Qry = "SELECT  TSPL_JOURNAL_MASTER.IND_AS,TSPL_JOURNAL_MASTER.Journal_No , TSPL_JOURNAL_MASTER.Voucher_No , TSPL_JOURNAL_MASTER.Voucher_Date , "
                Qry += " TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Source_Desc, TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_MASTER.Source_Doc_Date, TSPL_JOURNAL_MASTER.Posting_Date, "
                Qry += " TSPL_JOURNAL_MASTER.Voucher_Desc ,TSPL_JOURNAL_MASTER.Source_Narration ,TSPL_JOURNAL_MASTER.Remarks ,TSPL_JOURNAL_MASTER.Comments ,"
                Qry += " TSPL_JOURNAL_MASTER.Auto_Reverse, TSPL_JOURNAL_MASTER.Reverse_Date, TSPL_JOURNAL_MASTER.Source_Type, TSPL_JOURNAL_MASTER.CustVend_Code, "
                Qry += " TSPL_JOURNAL_MASTER.CustVend_Name, TSPL_JOURNAL_MASTER.Transaction_Type, TSPL_JOURNAL_MASTER.Provisional_Post, TSPL_JOURNAL_MASTER.Authorized, "
                Qry += " TSPL_JOURNAL_MASTER.Total_Debit_Amt, TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_MASTER.Type,TSPL_JOURNAL_MASTER.Segment_Code, "
                Qry += " TSPL_JOURNAL_DETAILS.Detail_Line_No "
                Qry += " ,TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc , TSPL_JOURNAL_DETAILS.Amount , TSPL_JOURNAL_DETAILS.Description ,"
                Qry += " TSPL_JOURNAL_DETAILS.Reference, TSPL_JOURNAL_DETAILS.Posting_Date, TSPL_JOURNAL_DETAILS.Account_Type, "
                Qry += " TSPL_JOURNAL_DETAILS.Account_Group_Code "
                Qry += " ,TSPL_JOURNAL_DETAILS.Account_Seg_Code1 ,TSPL_JOURNAL_DETAILS.Account_Seg_Desc1 ,"
                Qry += " TSPL_JOURNAL_DETAILS.Account_Seg_Code2 ,TSPL_JOURNAL_DETAILS.Account_Seg_Desc2 ,"
                Qry += " TSPL_JOURNAL_DETAILS.Account_Seg_Code3 ,TSPL_JOURNAL_DETAILS.Account_Seg_Desc3 ,"
                Qry += " TSPL_JOURNAL_DETAILS.Account_Seg_Code4 ,TSPL_JOURNAL_DETAILS.Account_Seg_Desc4 ,"
                Qry += " TSPL_JOURNAL_DETAILS.Account_Seg_Code5, TSPL_JOURNAL_DETAILS.Account_Seg_Desc5, "
                Qry += " TSPL_JOURNAL_DETAILS.Account_Seg_Code6, TSPL_JOURNAL_DETAILS.Account_Seg_Desc6, "
                Qry += " TSPL_JOURNAL_DETAILS.Account_Seg_Code7, TSPL_JOURNAL_DETAILS.Account_Seg_Desc7 "
                Qry += " ,TSPL_JOURNAL_DETAILS.Account_Seg_Code8, TSPL_JOURNAL_DETAILS.Account_Seg_Desc8, "
                Qry += " TSPL_JOURNAL_DETAILS.Account_Seg_Code9  ,TSPL_JOURNAL_DETAILS.Account_Seg_Desc9, "
                Qry += " TSPL_JOURNAL_DETAILS.Account_Seg_Code10, TSPL_JOURNAL_DETAILS.Account_Seg_Desc10,"
                Qry += " TSPL_JOURNAL_DETAILS.Hirerachy_Code, TSPL_JOURNAL_DETAILS.Cost_Centre_Code" 'added by stuti on 22/03/2017
                Qry += " FROM TSPL_JOURNAL_DETAILS INNER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No "
                Qry += " WHERE TSPL_JOURNAL_MASTER.Voucher_No='" + VoucherNo + "' Order By TSPL_JOURNAL_DETAILS.Detail_Line_No "

                Dim JournalNo As String = ""

                dt = clsDBFuncationality.GetDataTable(Qry, trans)
                If dt.Rows.Count <= 0 Then
                    Throw New Exception("Document not found to reverse.")
                Else
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_JOURNAL_MASTER SET Auto_Reverse='R' Where Voucher_No='" + VoucherNo + "'", trans)
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        If ii = 0 Then
                            Dim obj As New clsJournalEntryHeader()
                            obj.Journal_No = clsDBFuncationality.getSingleValue("select (case when max(journal_no) is not null then max(journal_no) else 0 end)+1 from TSPL_JOURNAL_MASTER ", trans)
                            Dim LocationCode As String = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
                            LocationCode = LocationCode.Substring(clsCommon.myLen(LocationCode) - 3, 3)
                            obj.IND_AS = clsCommon.myCdbl(dt.Rows(0)("IND_AS"))
                            obj.Voucher_No = fnAutoGenerateReverseNo(trans, clsCommon.GetPrintDate(VoucherDate, "dd/MM/yyyy"), True, LocationCode, MonthlyReverse)
                            obj.Voucher_Date = clsCommon.GetPrintDate(VoucherDate, "dd/MMM/yyyy hh:mm tt")
                            obj.Source_Code = clsCommon.myCstr(dt.Rows(0)("Source_Code"))
                            obj.Source_Desc = clsCommon.myCstr(dt.Rows(0)("Source_Desc"))
                            'obj.Source_Doc_No = clsCommon.myCstr(dt.Rows(0)("Source_Doc_No"))
                            obj.Source_Doc_No = clsCommon.myCstr(dt.Rows(0)("Voucher_No")) ''SourceDocNo in Auto Reverse Entry will be DocNo/Voucher_No of previous entry
                            obj.Segment_Code = clsCommon.myCstr(dt.Rows(0)("Segment_Code"))
                            obj.Source_Doc_Date = clsCommon.GetPrintDate(dt.Rows(0)("Source_Doc_Date"), "dd/MMM/yyyy hh:mm tt")
                            obj.Posting_Date = clsCommon.GetPrintDate(VoucherDate, "dd/MMM/yyyy hh:mm tt")
                            obj.Voucher_Desc = "Reverse voucher against Voucher - " + VoucherNo + ""
                            obj.Source_Narration = clsCommon.myCstr(dt.Rows(0)("Source_Narration"))
                            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
                            obj.Auto_Reverse = "N"
                            obj.Reverse_Date = ""
                            obj.Source_Type = clsCommon.myCstr(dt.Rows(0)("Source_Type"))
                            obj.CustVend_Code = clsCommon.myCstr(dt.Rows(0)("CustVend_Code"))
                            obj.CustVend_Name = clsCommon.myCstr(dt.Rows(0)("CustVend_Name"))
                            obj.Transaction_Type = clsCommon.myCstr(dt.Rows(0)("Transaction_Type"))
                            obj.Provisional_Post = clsCommon.myCstr(dt.Rows(0)("Provisional_Post"))
                            obj.Authorized = clsCommon.myCstr(dt.Rows(0)("Authorized"))
                            obj.Total_Debit_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Credit_Amt"))
                            obj.Total_Credit_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Debit_Amt"))
                            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
                            obj.ProgramCode = clsUserMgtCode.ReversejournalEntry
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", obj.Journal_No), New SqlParameter("@Voucher_No", obj.Voucher_No), New SqlParameter("@Voucher_Date", obj.Voucher_Date), New SqlParameter("@Source_Code", obj.Source_Code), New SqlParameter("@Source_Desc", obj.Source_Desc), New SqlParameter("@Source_Doc_No", obj.Source_Doc_No), New SqlParameter("@Source_Doc_Date", obj.Source_Doc_Date), New SqlParameter("@Posting_Date", obj.Posting_Date), New SqlParameter("@Voucher_Desc", obj.Voucher_Desc), New SqlParameter("@Source_Narration", obj.Source_Narration), New SqlParameter("@Remarks", obj.Remarks), New SqlParameter("@Comments", obj.Comments), New SqlParameter("@Auto_Reverse", obj.Auto_Reverse), New SqlParameter("@Reverse_Date", obj.Reverse_Date), New SqlParameter("@Source_Type", obj.Source_Type), New SqlParameter("@CustVend_Code", obj.CustVend_Code), New SqlParameter("@CustVend_Name", obj.CustVend_Name), New SqlParameter("@Transaction_Type", obj.Transaction_Type), New SqlParameter("@Total_Debit_Amt", obj.Total_Credit_Amt), New SqlParameter("@Total_Credit_Amt", obj.Total_Debit_Amt), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                            '' 31-July-2015
                            Dim Seg As String = "Update TSPL_JOURNAL_MASTER set IND_AS='" & obj.IND_AS & "' ,Segment_Code='" + obj.Segment_Code + "',MonthlyReverse='" & MonthlyReverse & "',ProgramCode='" & obj.ProgramCode & "' where Voucher_No='" + obj.Voucher_No + "' "
                            clsDBFuncationality.ExecuteNonQuery(Seg, trans)
                            ''
                            JournalNo = obj.Journal_No
                            VoucherNo = obj.Voucher_No
                        End If

                        Dim objTr As clsJournalEntryDetail = New clsJournalEntryDetail()
                        objTr.Detail_Line_No = ii + 1
                        objTr.Account_code = clsCommon.myCstr(dt.Rows(ii)("Account_code"))
                        objTr.Account_Desc = clsCommon.myCstr(dt.Rows(ii)("Account_Desc"))
                        objTr.Amount = clsCommon.myCdbl(dt.Rows(ii)("Amount")) * -1
                        objTr.Description = clsCommon.myCstr(dt.Rows(ii)("Description"))
                        objTr.Reference = clsCommon.myCstr(dt.Rows(ii)("Reference"))
                        objTr.Posting_Date = clsCommon.GetPrintDate(VoucherDate, "dd/MMM/yyyy")
                        objTr.Account_Type = clsCommon.myCstr(dt.Rows(ii)("Account_Type"))
                        objTr.Account_Group_Code = clsCommon.myCstr(dt.Rows(ii)("Account_Group_Code"))
                        objTr.Account_Seg_Code1 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code1"))
                        objTr.Account_Seg_Desc1 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc1"))
                        objTr.Account_Seg_Code2 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code2"))
                        objTr.Account_Seg_Desc2 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc2"))
                        objTr.Account_Seg_Code3 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code3"))
                        objTr.Account_Seg_Desc3 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc3"))
                        objTr.Account_Seg_Code4 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code4"))
                        objTr.Account_Seg_Desc4 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc4"))
                        objTr.Account_Seg_Code5 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code5"))
                        objTr.Account_Seg_Desc5 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc5"))
                        objTr.Account_Seg_Code6 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code6"))
                        objTr.Account_Seg_Desc6 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc6"))
                        objTr.Account_Seg_Code7 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code7"))
                        objTr.Account_Seg_Desc7 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc7"))
                        objTr.Account_Seg_Code8 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code8"))
                        objTr.Account_Seg_Desc8 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc8"))
                        objTr.Account_Seg_Code9 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code9"))
                        objTr.Account_Seg_Desc9 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc9"))
                        objTr.Account_Seg_Code10 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Code10"))
                        objTr.Account_Seg_Desc10 = clsCommon.myCstr(dt.Rows(ii)("Account_Seg_Desc10"))
                        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", JournalNo), New SqlParameter("@Voucher_No", VoucherNo), New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(VoucherDate, "dd/MMM/yyyy")), New SqlParameter("@Detail_Line_No", ii + 1), New SqlParameter("@Account_code", objTr.Account_code), New SqlParameter("@Account_Desc", objTr.Account_Desc), New SqlParameter("@Amount", objTr.Amount), New SqlParameter("@Description", objTr.Description), New SqlParameter("@Reference", objTr.Reference), New SqlParameter("@Posting_Date", clsCommon.GetPrintDate(VoucherDate, "dd/MMM/yyyy")), New SqlParameter("@Account_Type", objTr.Account_Type), New SqlParameter("@Account_Group_Code", objTr.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objTr.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objTr.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objTr.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objTr.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objTr.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objTr.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objTr.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objTr.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objTr.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objTr.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objTr.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objTr.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objTr.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objTr.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objTr.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objTr.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objTr.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objTr.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objTr.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objTr.Account_Seg_Desc10))

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", clsCommon.myCstr(dt.Rows(ii)("Hirerachy_Code")), True)
                        clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", clsCommon.myCstr(dt.Rows(ii)("Cost_Centre_Code")), True)
                        clsCommonFunctionality.UpdateDataTable(coll, "tspl_journal_details", OMInsertOrUpdate.Update, "tspl_journal_details.Journal_No='" + clsCommon.myCstr(JournalNo) + "' and tspl_journal_details.Voucher_No='" + clsCommon.myCstr(VoucherNo) + "' and tspl_journal_details.Account_Code='" + clsCommon.myCstr(dt.Rows(ii)("Account_code")) + "' and Detail_Line_No='" + clsCommon.myCstr(ii + 1) + "'", trans)

                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetVoucherQuery(ByVal strSourceTransaction As String, ByVal strSourceDoc As String, ByVal txtFromDateTag As Object) As String
        Dim str As String = ""
        If clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.frmPaymentProcess) = CompairStringResult.Equal Then
            str = "Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from 
TSPL_PAYMENT_PROCESS_MCC_SALE
inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No
inner Join  TSPL_Receipt_Adjustment_Header On TSPL_Receipt_Adjustment_Header.Doc_No =TSPL_PAYMENT_PROCESS_MCC_SALE.Sale_Doc_No And TSPL_Receipt_Adjustment_Header.ARInvoiceNo=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No And convert(Date, TSPL_PAYMENT_PROCESS_HEAD.Doc_Date, 103)=convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)
inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_Receipt_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AR-AD'
where TSPL_Receipt_Adjustment_Header.Description ='Adjustment Against Bulk Payment Process ' and TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No='" + strSourceDoc + "'
union all
Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL
inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)
inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'
where TSPL_Payment_Adjustment_Header.Adjust_Type = 'P' and TSPL_Payment_Adjustment_Header.description =' AP Adjustment Against Bulk Payment Process ' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'
union all
Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL
inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)
inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'
where TSPL_Payment_Adjustment_Header.Adjust_Type = 'R' and TSPL_Payment_Adjustment_Header.description =' AP Return Adjustment Against Bulk Payment Process ' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'
union all
Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_PAYMENT_PROCESS_DETAIL 
inner Join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Against_PP_Detail_No_Advance_Payment=TSPL_PAYMENT_PROCESS_DETAIL.PP_Detail_No
inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_PAYMENT_HEADER.Payment_Code And TSPL_JOURNAL_MASTER.Source_Code='AR-AD'
where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No ='" + strSourceDoc + "'
union all
Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_PAYMENT_PROCESS_DETAIL 
inner Join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Against_PP_Detail_No=TSPL_PAYMENT_PROCESS_DETAIL.PP_Detail_No
inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_PAYMENT_HEADER.Payment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-PY'
where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No ='" + strSourceDoc + "'
union all
Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL
inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)
inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'
where TSPL_Payment_Adjustment_Header.Adjust_Type = '' and TSPL_Payment_Adjustment_Header.description ='AP Debit Note Adjustment Against Hold Process' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'
union all
Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL
inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)
inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'
where TSPL_Payment_Adjustment_Header.Adjust_Type = '' and TSPL_Payment_Adjustment_Header.description ='AP Invoice Adjustment Against Hold Process' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'
union all
Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from 
TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN
inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No
inner Join  TSPL_Receipt_Adjustment_Header On TSPL_Receipt_Adjustment_Header.Doc_No =TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Sale_Doc_No And TSPL_Receipt_Adjustment_Header.ARInvoiceNo=TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.AR_Invoice_No And convert(Date, TSPL_PAYMENT_PROCESS_HEAD.Doc_Date, 103)=convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)
inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_Receipt_Adjustment_Header.Adjustment_No And Source_Code='AR-AD'
where TSPL_Receipt_Adjustment_Header.Description ='Return Adjustment Against Bulk Payment Process' and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No='" + strSourceDoc + "'"

        End If

        If clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.frmPaymentProcessFarmer) = CompairStringResult.Equal Then
            str = "  Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_Payment_Adjustment_Header " + Environment.NewLine +
"Left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'" + Environment.NewLine +
"where description Like '%" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select  TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from " + Environment.NewLine +
"TSPL_PAYMENT_PROCESS_MCC_SALE" + Environment.NewLine +
"inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No" + Environment.NewLine +
"inner Join  TSPL_Receipt_Adjustment_Header On TSPL_Receipt_Adjustment_Header.Doc_No =TSPL_PAYMENT_PROCESS_MCC_SALE.Sale_Doc_No And TSPL_Receipt_Adjustment_Header.ARInvoiceNo=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No And convert(Date, TSPL_PAYMENT_PROCESS_HEAD.Doc_Date, 103)=convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_Receipt_Adjustment_Header.Adjustment_No And Source_Code='AR-AD'" + Environment.NewLine +
"where TSPL_Receipt_Adjustment_Header.Description ='Adjustment Against Bulk Payment Process ' and TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_PAYMENT_PROCESS_DETAIL " + Environment.NewLine +
"inner Join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Against_PP_Detail_No_Advance_Payment=TSPL_PAYMENT_PROCESS_DETAIL.PP_Detail_No" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_PAYMENT_HEADER.Payment_No And Source_Code='AR-AD'" + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No ='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
"inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No" + Environment.NewLine +
"inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'" + Environment.NewLine +
"where TSPL_Payment_Adjustment_Header.Adjust_Type = 'R' and TSPL_Payment_Adjustment_Header.description ='AP Return Adjustment Against Bulk Payment Process for extra amount to be paid by VSP' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code " + Environment.NewLine +
"from  TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_VENDOR_INVOICE_HEAD.Document_No And TSPL_JOURNAL_MASTER.Source_Code='AP-DN'" + Environment.NewLine +
"where TSPL_VENDOR_INVOICE_HEAD.RefDocNo ='" + strSourceDoc + "'  and" + Environment.NewLine +
"TSPL_VENDOR_INVOICE_HEAD.Description Like 'Auto Generated Debit Note Against VSP%' and" + Environment.NewLine +
"TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'Farmer DN'  and TSPL_VENDOR_INVOICE_HEAD.Document_Type = 'D' " + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
"inner Join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_PAYMENT_PROCESS_DETAIL.PP_Detail_No " + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_VENDOR_INVOICE_HEAD.Document_No And Source_Code='AP-DN'" + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No ='" + strSourceDoc + "' and TSPL_VENDOR_INVOICE_HEAD.Description like 'AP Debit Note Against VSP for extra amount to be paid by vsp%'" + Environment.NewLine +
"And TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'VSP' and TSPL_VENDOR_INVOICE_HEAD.Document_Type = 'D'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
"inner Join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_PAYMENT_PROCESS_DETAIL.PP_Detail_No " + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_VENDOR_INVOICE_HEAD.Document_No And Source_Code='AP-CN'" + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No ='" + strSourceDoc + "'  and" + Environment.NewLine +
" TSPL_VENDOR_INVOICE_HEAD.Description Like 'AP Credit Note Against VSP for For farmer deduction : %'" + Environment.NewLine +
"And TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'FAR-DED' and TSPL_VENDOR_INVOICE_HEAD.Document_Type = 'C'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
"inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No" + Environment.NewLine +
"inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'" + Environment.NewLine +
"where TSPL_Payment_Adjustment_Header.Adjust_Type = 'P' and TSPL_Payment_Adjustment_Header.description ='AP Adjustment Against Bulk Payment Process' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
"inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No" + Environment.NewLine +
"inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'" + Environment.NewLine +
"where TSPL_Payment_Adjustment_Header.Adjust_Type = 'R' and description ='AP Adjustment Against Credt note in Bulk Payment Process' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
"inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No" + Environment.NewLine +
"inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'" + Environment.NewLine +
"where TSPL_Payment_Adjustment_Header.Adjust_Type = 'R' and TSPL_Payment_Adjustment_Header.description ='AP Return Adjustment Against Bulk Payment Process' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
"inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No" + Environment.NewLine +
"inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'" + Environment.NewLine +
"where TSPL_Payment_Adjustment_Header.Adjust_Type = '' and TSPL_Payment_Adjustment_Header.description ='AP Debit Note Adjustment Against Hold Process' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code  from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
"inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No" + Environment.NewLine +
"inner Join TSPL_Payment_Adjustment_Header On TSPL_Payment_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No And convert(Date, TSPL_Payment_Adjustment_Header.Adjustment_Date, 103)=convert(date,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103)" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-AD'" + Environment.NewLine +
"where TSPL_Payment_Adjustment_Header.Adjust_Type = '' and TSPL_Payment_Adjustment_Header.description ='AP Invoice Adjustment Against Hold Process' and TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine +
"inner Join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_PAYMENT_PROCESS_HEAD.Doc_No" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_VENDOR_INVOICE_HEAD.Document_No And TSPL_JOURNAL_MASTER.Source_Code='AP-DN'" + Environment.NewLine +
"where TSPL_PAYMENT_PROCESS_HEAD.Doc_No ='" + strSourceDoc + "'  and" + Environment.NewLine +
" TSPL_VENDOR_INVOICE_HEAD.Description Like 'Debit Note Against %' and" + Environment.NewLine +
" TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'Farmer DN' and TSPL_VENDOR_INVOICE_HEAD.Document_Type = 'D'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from " + Environment.NewLine +
"TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN" + Environment.NewLine +
"inner Join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No" + Environment.NewLine +
"inner Join  TSPL_Receipt_Adjustment_Header On TSPL_Receipt_Adjustment_Header.Doc_No =TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Sale_Doc_No And TSPL_Receipt_Adjustment_Header.ARInvoiceNo=TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.AR_Invoice_No And convert(Date, TSPL_PAYMENT_PROCESS_HEAD.Doc_Date, 103)=convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)" + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_Receipt_Adjustment_Header.Adjustment_No And Source_Code='AR-AD'" + Environment.NewLine +
"where TSPL_Receipt_Adjustment_Header.Description ='Return Adjustment Against Bulk Payment Process' and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code" + Environment.NewLine +
"From TSPL_Payment_Process_head " + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER  on TSPL_JOURNAL_MASTER .Source_Doc_No= TSPL_Payment_Process_head.Doc_No" + Environment.NewLine +
"where TSPL_JOURNAL_MASTER.Source_Code in ('MP-DE' ,'MP-IV','MP-LT','MP-ST' ) and TSPL_Payment_Process_head.Doc_No ='" + strSourceDoc + "'" + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_BANK_TRANSFER " + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER  on TSPL_JOURNAL_MASTER.Source_Doc_No= TSPL_BANK_TRANSFER.Transfer_No" + Environment.NewLine +
"where Description Like 'Bank Transfer against Farmer Payment Process-" + strSourceDoc + "' and TSPL_JOURNAL_MASTER.Source_Code in ('BK-TF') " + Environment.NewLine +
"union all" + Environment.NewLine +
"Select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from  TSPL_PAYMENT_HEADER  " + Environment.NewLine +
"inner Join TSPL_JOURNAL_MASTER on  TSPL_JOURNAL_MASTER.source_doc_no=TSPL_PAYMENT_HEADER.Payment_No And TSPL_JOURNAL_MASTER.Source_Code='AP-MI'" + Environment.NewLine +
"where TSPL_PAYMENT_HEADER.Entry_Desc ='Misc Payment entry against Payment Process Farmer-" + strSourceDoc + "' and TSPL_PAYMENT_HEADER.Payment_Type='MI'  "
        End If

        If clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.MilkMPPayment) = CompairStringResult.Equal OrElse
        clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.MilkVSPPayment) = CompairStringResult.Equal OrElse
        clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.MPBillGeneration) = CompairStringResult.Equal OrElse
     clsCommon.CompairString(strSourceTransaction, clsUserMgtCode.MilkVSPIssuePayment) = CompairStringResult.Equal Then

            str += "select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_MILK_PURCHASE_INVOICE_HEAD
inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE
inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_JOURNAL_MASTER.Source_Code='AP-IN'
 where TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE='" + strSourceDoc + "' and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)='" + clsCommon.myCstr(txtFromDateTag) + "'
 union all
 select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_MILK_PURCHASE_INVOICE_HEAD
inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE 
inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No  
 where TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE='" + strSourceDoc + "' and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)='" + clsCommon.myCstr(txtFromDateTag) + "' and TSPL_VENDOR_INVOICE_HEAD.RefDocType in ('DED-MAP','VSP-COM','VSP-QLT','VSP-DIT','PRO-VFC','PRO-VFD','PRO-STD','PRO-LCS','VSP-CMP','VSP-PVK','NCM-DED','CM-DED','Milk_DE','Milk_HE','Milk_OW','VSP-MP','OWD-CRE','OWD-CRD','OWD-DBT','DCS-ADD ','DCS-DED','DCS-QAT','DCS-LYT')
 union all
select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code from TSPL_MILK_PURCHASE_INVOICE_HEAD
inner join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Reference=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE 
inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_PAYMENT_HEADER.Payment_No  
where TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE='" + strSourceDoc + "' and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)='" + clsCommon.myCstr(txtFromDateTag) + "'  and TSPL_PAYMENT_HEADER.Entry_Desc = 'Apply document for Asset Lost'"
        End If
        Return str
    End Function
End Class

Public Class clsJournalEntryDetail
#Region "Variables"
    Public Journal_No As Integer
    Public Voucher_No As String = Nothing
    Public Voucher_Date As DateTime?
    Public Detail_Line_No As Integer
    Public Account_code As String = Nothing
    Public Account_Desc As String = Nothing
    Public Amount As Double
    Public Description As String = Nothing
    Public Reference As String = Nothing
    Public Posting_Date As DateTime?
    Public Account_Type As String = Nothing
    Public Account_Group_Code As String = Nothing
    Public Account_Seg_Code1 As String = Nothing
    Public Account_Seg_Desc1 As String = Nothing
	Public Account_Seg_Code2 As String = Nothing
    Public Account_Seg_Desc2 As String = Nothing
    Public Account_Seg_Code3 As String = Nothing
    Public Account_Seg_Desc3 As String = Nothing
    Public Account_Seg_Code4 As String = Nothing
    Public Account_Seg_Desc4 As String = Nothing
    Public Account_Seg_Code5 As String = Nothing
    Public Account_Seg_Desc5 As String = Nothing
    Public Account_Seg_Code6 As String = Nothing
    Public Account_Seg_Desc6 As String = Nothing
    Public Account_Seg_Code7 As String = Nothing
    Public Account_Seg_Desc7 As String = Nothing
    Public Account_Seg_Code8 As String = Nothing
    Public Account_Seg_Desc8 As String = Nothing
    Public Account_Seg_Code9 As String = Nothing
    Public Account_Seg_Desc9 As String = Nothing
    Public Account_Seg_Code10 As String = Nothing
    Public Account_Seg_Desc10 As String = Nothing
#End Region
End Class

