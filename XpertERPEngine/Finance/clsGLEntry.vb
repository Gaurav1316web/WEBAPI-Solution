'05/11/2012 1:04 PM
Imports System.Data.SqlClient
Imports common
Public Class clsGLEntry
    Shared Function funGLPOST(ByVal strDoc As String, ByVal PostDate As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            funGLPOST(strDoc, PostDate, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Shared Function funGLPOST(ByVal strDoc As String, ByVal PostDate As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select Source_Code,Source_Doc_No,Auto_Reverse,CONVERT(date, Reverse_Date,103) as Reverse_Date,MonthlyReverse from TSPL_JOURNAL_MASTER where Voucher_No='" + strDoc + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim strSocurceCode As String = clsCommon.myCstr(dt.Rows(0)("Source_Code"))
            If clsCommon.CompairString(strSocurceCode, "AR-IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(strSocurceCode, "AR-DN") = CompairStringResult.Equal OrElse clsCommon.CompairString(strSocurceCode, "AR-CR") = CompairStringResult.Equal Then
                qry = "select Status from TSPL_Customer_Invoice_Head where Document_No='" + clsCommon.myCstr(dt.Rows(0)("Source_Doc_No")) + "' "
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 0 Then
                    Throw New Exception("Please first post AR Invoice No - " + clsCommon.myCstr(dt.Rows(0)("Source_Doc_No")))
                End If
            End If
        End If
        qry = "select SUM(Amount) from TSPL_JOURNAL_DETAILS where Voucher_No='" + strDoc + "'"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <> 0 Then
            Throw New Exception("Total Debit Amount is not equla to Toal Credit amount Of Voucher No [" + strDoc + "]")
        End If
        Dim AllowJEofDifferentLocationOnJournalEntry As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJEofDifferentLocationOnJournalEntry, clsFixedParameterCode.AllowJEofDifferentLocationOnJournalEntry, trans)) = 1, True, False)
        If AllowJEofDifferentLocationOnJournalEntry = True Then
            qry = "DISABLE TRIGGER dbo.trg_MisMatchBal ON dbo.TSPL_JOURNAL_MASTER"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        qry = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' , Posting_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' WHERE Voucher_No='" + strDoc + "' "
        connectSql.RunSqlTransaction(trans, qry)

        If AllowJEofDifferentLocationOnJournalEntry = True Then
            qry = "ENABLE TRIGGER dbo.trg_MisMatchBal ON dbo.TSPL_JOURNAL_MASTER"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If

        ''End Fiscal Year Entry
        Dim EntryDate As String = clsCommon.GetPrintDate(PostDate, "dd/MMM/yyyy")
        qry = "select is_End_Year_Proceed from TSPL_Fiscal_Year_Master where convert(date, '" + EntryDate + "',103)>= convert(date, Start_Date,103) and convert(date, '" + EntryDate + "',103)<=CONVERT(date, End_Date,103)"
        Dim dtable As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtable Is Nothing OrElse dtable.Rows.Count <= 0 Then
            Throw New Exception("Please create financial year which contains " + EntryDate)
        End If
        If clsCommon.myCdbl(dtable.Rows(0)("is_End_Year_Proceed")) = 1 Then
            clsJournalMaster.CreateJEForEndYear(strDoc, EntryDate, trans)
        End If
        ''End of End Fiscal Year Entry

        'No Need to send data in tally
        'Dim objSendToTally As New clsSendToTally()
        'objSendToTally.SendToTally_JournalEntry(strDoc, trans)


        Dim ChkRev As String = clsCommon.myCstr(dt.Rows(0)("Auto_Reverse"))
        If clsCommon.CompairString(dt.Rows(0)("Auto_Reverse"), "Y") = CompairStringResult.Equal Then
            clsJournalEntryHeader.AutoReverse(clsCommon.myCstr(strDoc), clsCommon.myCDate(dt.Rows(0)("Reverse_Date")), trans, clsCommon.myCdbl(dt.Rows(0)("MonthlyReverse")))
            Dim strRevVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Voucher_No FROM TSPL_JOURNAL_MASTER WHERE Source_Doc_No ='" + strDoc + "'", trans))
            clsGLEntry.funGLPOST(strRevVoucherNo, clsCommon.myCDate(dt.Rows(0)("Reverse_Date")), trans)
        End If
        Return True
    End Function
End Class
