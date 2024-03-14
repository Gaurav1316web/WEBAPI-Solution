Imports System.Data.SqlClient
Imports common

Public Class clsRemitanceEntry
#Region "Variables"
    Public Remittance_Code As String = Nothing
    Public Remittance_Date As DateTime
    Public Remit_TDS As String = Nothing
    Public Bank_Code As String = Nothing
    Public Amt_To_Remit As Double = 0
    Public Remit_To As String = Nothing
    Public AP_Posting_Date As Date = Nothing
    Public AP_Payment_Date As Date = Nothing
    Public Payment_Code As String = Nothing
    Public Cheque_No As String = Nothing
    Public Cheque_Date As Date? = Nothing
    Public BSR_Code As String = Nothing
    Public BSR_Name As String = Nothing
    Public Challan_No As String = Nothing
    Public Challan_Date As Date? = Nothing
    Public Posted As String = Nothing
    Public Section_Code As String = Nothing
    Public Section_Description As String = Nothing
    Public Branch_Code As String = Nothing
    Public Select_By As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modify_By As Date
    Public Modify_Date As Date
    Public report_status As String = Nothing
    Public Tax_Amount As Double = 0
    Public Description As String = Nothing
    Public Comp_Code As String = Nothing
    Public Arr As List(Of clsRemitanceEntryDetail)
#End Region
    Public Function SaveData(ByVal obj As clsRemitanceEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = " delete from TSPL_REMITTANCE_ENTRY_Detail where Remittance_Code='" + obj.Remittance_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Remittance_Date", clsCommon.GetPrintDate(obj.Remittance_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Remit_TDS", obj.Remit_TDS)
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Amt_To_Remit", obj.Amt_To_Remit)


            clsCommon.AddColumnsForChange(coll, "AP_Posting_Date", clsCommon.GetPrintDate(obj.AP_Posting_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "AP_Payment_Date", clsCommon.GetPrintDate(obj.AP_Payment_Date, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            If clsCommon.myLen(obj.Cheque_No) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Remit_To", obj.Remit_To)
            clsCommon.AddColumnsForChange(coll, "BSR_Code", obj.BSR_Code)
            clsCommon.AddColumnsForChange(coll, "BSR_Name", obj.BSR_Name)
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Challan_Date", obj.Challan_Date)
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
            clsCommon.AddColumnsForChange(coll, "Section_Description", obj.Section_Description)
            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
            clsCommon.AddColumnsForChange(coll, "Select_By", obj.Select_By)
            clsCommon.AddColumnsForChange(coll, "report_status", obj.report_status)
            clsCommon.AddColumnsForChange(coll, "Tax_Amount", obj.Tax_Amount)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Remittance_Code", obj.Remittance_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REMITTANCE_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REMITTANCE_ENTRY", OMInsertOrUpdate.Update, " Remittance_Code='" + obj.Remittance_Code + "'", trans)
            End If
            clsRemitanceEntryDetail.SaveData(obj.Remittance_Code, obj.Arr, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As clsRemitanceEntry
        Dim obj As clsRemitanceEntry = Nothing
        Dim qry As String = " select * from TSPL_REMITTANCE_ENTRY where Remittance_Code='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsRemitanceEntry
            obj.Remittance_Code = clsCommon.myCstr(dt.Rows(0)("Remittance_Code"))
            obj.Remittance_Date = clsCommon.myCDate(dt.Rows(0)("Remittance_Date"))
            obj.Remit_TDS = clsCommon.myCstr(dt.Rows(0)("Remit_TDS"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Amt_To_Remit = clsCommon.myCdbl(dt.Rows(0)("Amt_To_Remit"))
            obj.Remit_To = clsCommon.myCstr(dt.Rows(0)("Remit_To"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            If dt.Rows(0)("Cheque_Date") IsNot DBNull.Value Then
                obj.Cheque_Date = clsCommon.myCDate(dt.Rows(0)("Cheque_Date"))
            End If
            obj.BSR_Code = clsCommon.myCstr(dt.Rows(0)("BSR_Code"))
            obj.BSR_Name = clsCommon.myCstr(dt.Rows(0)("BSR_Name"))
            obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
            If dt.Rows(0)("Challan_Date") IsNot DBNull.Value Then
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
            End If
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
            obj.Section_Description = clsCommon.myCstr(dt.Rows(0)("Section_Description"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Select_By = clsCommon.myCstr(dt.Rows(0)("Select_By"))
            obj.report_status = clsCommon.myCstr(dt.Rows(0)("report_status"))
            obj.Tax_Amount = clsCommon.myCdbl(dt.Rows(0)("Tax_Amount"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.AP_Posting_Date = clsCommon.myCDate(dt.Rows(0)("AP_Posting_Date"))
            obj.AP_Payment_Date = clsCommon.myCDate(dt.Rows(0)("AP_Payment_Date"))

            qry = " select * from TSPL_REMITTANCE_ENTRY_DETAIL where Remittance_Code='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsRemitanceEntryDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objTr As New clsRemitanceEntryDetail()
                    objTr.Remittance_Code = clsCommon.myCstr(dr("Remittance_Code"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                    objTr.Document_Type = clsCommon.myCstr(dr("Document_Type"))
                    objTr.Document_Amount = clsCommon.myCdbl(dr("Document_Amount"))
                    objTr.Service_Type = clsCommon.myCstr(dr("Service_Type"))
                    objTr.Actual_TDS_Base = clsCommon.myCdbl(dr("Actual_TDS_Base"))
                    objTr.Calculated_TDS_Base = clsCommon.myCdbl(dr("Calculated_TDS_Base"))
                    objTr.Actual_TDS = clsCommon.myCdbl(dr("Actual_TDS"))
                    objTr.Calculated_TDS = clsCommon.myCdbl(dr("Calculated_TDS"))
                    objTr.Actual_Surcharge = clsCommon.myCdbl(dr("Actual_Surcharge"))
                    objTr.Calculated_Surcharge = clsCommon.myCdbl(dr("Calculated_Surcharge"))
                    objTr.Actual_Edu_Cess = clsCommon.myCdbl(dr("Actual_Edu_Cess"))
                    objTr.Calculated_Edu_Cess = clsCommon.myCdbl(dr("Calculated_Edu_Cess"))
                    objTr.Actual_Sec_Educess = clsCommon.myCdbl(dr("Actual_Sec_Educess"))
                    objTr.Actual_Total_TDS = clsCommon.myCdbl(dr("Actual_Total_TDS"))
                    objTr.Calculated_Total_TDS = clsCommon.myCdbl(dr("Calculated_Total_TDS"))
                    objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objTr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    objTr.TDS_Per = clsCommon.myCdbl(dr("TDS_Per"))
                    objTr.Surcharge_Per = clsCommon.myCdbl(dr("Surcharge_Per"))
                    objTr.Edu_Cess_Per = clsCommon.myCdbl(dr("Edu_Cess_Per"))
                    objTr.Sec_Educess_Per = clsCommon.myCdbl(dr("Sec_Educess_Per"))
                    objTr.Fiscal_Year = clsCommon.myCstr(dr("Fiscal_Year"))
                    objTr.Quarter = clsCommon.myCstr(dr("Quarter"))
                    objTr.Deduction_Code = clsCommon.myCstr(dr("Deduction_Code"))
                    obj.Arr.Add(objTr)
                Next

            End If
        End If
        Return obj
    End Function

    ''Public Shared Function PostData(ByVal strDocNo As String) As Boolean
    ''    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()


    ''    Dim strqry As String = " select Remittance_Code, Vendor_Code, Bank_Code, Amt_To_Remit, " & _
    ''                            " Remit_To, AP_Posting_Date, AP_Payment_Date,Payment_Code, Cheque_No, Cheque_Date, BSR_Code," & _
    ''                            " BSR_Name, Challan_No, Challan_Date, Posted,Vendor_Name, Vendor_Code, Document_No, Document_Date, Document_Type," & _
    ''                            " Document_Amount,Service_Type, Calculated_Total_TDS , Section_Code, Posted, " & _
    ''                            " Section_Description , Created_By ,Created_Date , Modify_By , Modify_Date ,Comp_Code, TDS_Per, " & _
    ''                            " Surcharge_Per , Edu_Cess_Per ,Sec_Educess_Per, Fiscal_Year, Quarter, Deduction_Code, " & _
    ''                            " Remittance_Date, Branch_Code, Tax_Amount, Description from TSPL_REMITTANCE_ENTRY where Remittance_Code='" + strDocNo + "'"
    ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry, trans)
    ''    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    ''        Throw New Exception("Document No. not found to Post")
    ''    End If
    ''    If clsCommon.CompairString("Y", clsCommon.myCstr(dt.Rows(0)("Posted"))) = CompairStringResult.Equal Then
    ''        Throw New Exception("Already Posted Document")
    ''    End If

    ''    Dim isSaved As Boolean = True
    ''    Dim tdspayable As String = String.Empty
    ''    Dim interest As String = String.Empty
    ''    Dim other As String = String.Empty
    ''    Dim penalty As String = String.Empty
    ''    Dim tdspayabledesc As String = String.Empty
    ''    Dim interestdesc As String = String.Empty
    ''    Dim otherdesc As String = String.Empty
    ''    Dim penaltydesc As String = String.Empty
    ''    Dim srctype As String = "AP-MI"
    ''    Dim strsrcdesc As String = "PAYMENT"
    ''    Dim strgeneratecode As String = "MI"
    ''    Dim STRPAYmentno As String = String.Empty
    ''    Dim paymentdate As String = clsCommon.myCstr(dt.Rows(0)("AP_Payment_Date"))
    ''    Dim strdocumentno As String = STRPAYmentno
    ''    Dim strdocdesc As String = clsCommon.myCstr(dt.Rows(0)("Description"))
    ''    Dim strsrctype As String = "V"
    ''    Dim strsrctypecode As String = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
    ''    Dim strsrctypedesc As String = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
    ''    Try
    ''        Dim strtaxaccount As String()
    ''        Dim qry As String = "select Bank_type from TSPL_BANK_MASTER where BANK_CODE = '" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'"
    ''        Dim strBankType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    ''        If clsCommon.CompairString(strBankType, "B") Then
    ''            STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dt.Rows(0)("Document_Date")), clsDocType.Payment, clsDocTransactionType.Bank, "")
    ''        ElseIf clsCommon.CompairString(strBankType, "C") Then
    ''            STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dt.Rows(0)("Document_Date")), clsDocType.Payment, clsDocTransactionType.Cash, "")
    ''        ElseIf clsCommon.CompairString(strBankType, "P") Then
    ''            STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dt.Rows(0)("Document_Date")), clsDocType.Payment, clsDocTransactionType.PettyCash, "")
    ''        ElseIf clsCommon.CompairString(strBankType, "O") Then
    ''            STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dt.Rows(0)("Document_Date")), clsDocType.Payment, clsDocTransactionType.Others, "")
    ''        Else
    ''            Throw New Exception("Plase set the Bank Type for Bank " + clsCommon.myCstr(dt.Rows(0)("Bank_Code")))
    ''        End If

    ''        Dim strbankaccount As String()
    ''        Dim arrtax As New ArrayList()
    ''        Dim query As String = "select Tax_Account , TaxAcct_Description , Interest_Account , Interest_Acc_Desc , Others_Account , Other_Acct_Desc , Penalty_Account, Penalty_Acct_Desc   from TSPL_TDS_BRANCH_MASTER "
    ''        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(query, trans)
    ''        If dt1.Rows.Count > 0 Then
    ''            'dr.Read()
    ''            tdspayable = clsCommon.myCstr(dt1.Rows(0)("Tax_Account"))
    ''            interest = clsCommon.myCstr(dt1.Rows(0)("Interest_Account"))
    ''            other = clsCommon.myCstr(dt1.Rows(0)("Others_Account"))
    ''            penalty = clsCommon.myCstr(dt1.Rows(0)("Penalty_Account"))
    ''            tdspayabledesc = clsCommon.myCstr(dt1.Rows(0)("TaxAcct_Description"))
    ''            interestdesc = clsCommon.myCstr(dt1.Rows(0)("Interest_Acc_Desc"))
    ''            otherdesc = clsCommon.myCstr(dt1.Rows(0)("Other_Acct_Desc"))
    ''            penaltydesc = clsCommon.myCstr(dt1.Rows(0)("Penalty_Acct_Desc"))
    ''        End If
    ''        Dim straccount As String = String.Empty
    ''        Dim strbankacct As String = String.Empty
    ''        Dim arrmis As New ArrayList()
    ''        strbankacct = Convert.ToString(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans))

    ''        Dim dramt As Decimal = CDec(dt.Rows(0)("Tax_Amount"))
    ''        Dim cramt As Decimal = CDec(dt.Rows(0)("Tax_Amount")) * -1
    ''        Dim strbankaccount1 As String() = {strbankacct, Convert.ToString(cramt)}
    ''        Dim strtdsaccount As String() = {tdspayable, Convert.ToString(dramt)}
    ''        arrmis.Add(strbankaccount1)
    ''        arrmis.Add(strtdsaccount)
    ''        Dim narration As String = clsCommon.myCstr(dt.Rows(0)("BSR_Code")) + clsCommon.myCstr(dt.Rows(0)("BSR_Name"))
    ''        Dim reference As String = clsCommon.myCstr(dt.Rows(0)("Challan_No")) + " " + clsCommon.myCstr(dt.Rows(0)("Challan_Date"))
    ''        clsDBFuncationality.ExecuteNonQuery("update TSPL_REMITTANCE_ENTRY set Posted = 'Y' WHERE Remittance_Code = '" + strDocNo + "'", trans)
    ''        clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", paymentdate), New SqlParameter("@paymentpostdate", paymentdate), New SqlParameter("@bankcode", clsCommon.myCstr(dt.Rows(0)("Bank_Code"))), New SqlParameter("@paymenttype", strgeneratecode), New SqlParameter("@vendorcode", clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))), New SqlParameter("@vendorname", clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))), New SqlParameter("@remitto", clsCommon.myCstr(dt.Rows(0)("Remit_To"))), New SqlParameter("@entrydesc", clsCommon.myCstr(dt.Rows(0)("description"))), New SqlParameter("@reference", reference), New SqlParameter("@narration", narration), New SqlParameter("@paymentcode", clsCommon.myCstr(dt.Rows(0)("Payment_Code"))), New SqlParameter("@chequeno", clsCommon.myCstr(dt.Rows(0)("Cheque_No"))), New SqlParameter("@chequedate", clsCommon.myCstr(dt.Rows(0)("Cheque_Date"))), New SqlParameter("@paymentamount", CDec(clsCommon.myCdbl(dt.Rows(0)("Tax_Amount")))), New SqlParameter("@vendoraccountset", ""), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "P"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans))), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans))), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
    ''        clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + STRPAYmentno + "'", trans)
    ''        clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Remit_To = '" + clsCommon.myCstr(dt.Rows(0)("Remit_To")) + "', Entry_Desc = '" + clsCommon.myCstr(dt.Rows(0)("Description")) + "' where Payment_Code = '" + STRPAYmentno + "'", trans)

    ''        clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_DETAIL_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@paymentline", Convert.ToString(1)), New SqlParameter("@apply", ""), New SqlParameter("@vendorinvoiceno", ""), New SqlParameter("@paymenttype", strgeneratecode), New SqlParameter("@documentno", ""), New SqlParameter("@pendingbalance", "0"), New SqlParameter("@appliedamount", "0"), New SqlParameter("@originalamount", "0"), New SqlParameter("@tdsamoount", "0"), New SqlParameter("@netbalance", clsCommon.myCstr(dt.Rows(0)("Tax_Amount"))), New SqlParameter("@accountcode", tdspayable), New SqlParameter("@description", tdspayabledesc), New SqlParameter("@remark", ""), New SqlParameter("@comment", ""))
    ''        clsJournalMaster.FunGrnlEntryWithTrans(trans, paymentdate, clsCommon.myCstr(dt.Rows(0)("Description")), srctype, strsrcdesc, STRPAYmentno, strdocdesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis)
    ''        trans.Commit()
    ''        Dim journalno As String = connectSql.RunScalar("select MAX(Voucher_No ) from TSPL_JOURNAL_MASTER")
    ''        connectSql.RunSql("update TSPL_JOURNAL_MASTER set Source_Desc = '" + strsrcdesc + " ', Remarks = '" + reference + "', Comments = '" + narration + "' where Voucher_No = '" + journalno + "'")
    ''        myMessages.post()
    ''    Catch ex As Exception
    ''        trans.Rollback()
    ''        Throw New Exception(ex.Message)
    ''    End Try
    ''End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean

        Dim obj As clsRemitanceEntry = clsRemitanceEntry.GetData(strDocNo)
        If clsCommon.myLen(obj.Remittance_Code) <= 0 Then
            Throw New Exception("Remittance code not found for post")
        End If
        If clsCommon.CompairString("Y", obj.Posted) = CompairStringResult.Equal Then
            Throw New Exception("Already Posted Document")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Dim tdspayable As String = String.Empty



        Dim tdspayabledesc As String = String.Empty



        Dim srctype As String = "AP-MI"
        Dim strsrcdesc As String = "PAYMENT"
        Dim strgeneratecode As String = "MI"
        Dim STRPAYmentno As String = String.Empty
        'Dim paymentdate As String = clsCommon.GetPrintDate(obj.AP_Payment_Date, "dd/MMM/yyyy")
        Dim strdocumentno As String = STRPAYmentno
        Dim strdocdesc As String = clsCommon.myCstr(obj.Description)
        Dim strsrctype As String = "V"
        Dim strsrctypecode As String = clsCommon.myCstr(obj.Arr(0).Vendor_Code)
        Dim strsrctypedesc As String = clsCommon.myCstr(obj.Arr(0).Vendor_Name)
        Try
            'Dim strtaxaccount As String()
            Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE = '" + obj.Bank_Code + "'"
            Dim dtBank As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtBank Is Nothing OrElse dtBank.Rows.Count <= 0 Then
                Throw New Exception("Bank type Not found for bank " + obj.Bank_Code)
            End If

            Dim strBankType As String = clsCommon.myCstr(dtBank.Rows(0)("Bank_type"))
            Dim strBankLocaSeg As String = clsCommon.myCstr(dtBank.Rows(0)("BANKACC"))
            strBankLocaSeg = strBankLocaSeg.Substring(strBankLocaSeg.Length - 3, 3)

            If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Arr(0).Document_Date), clsDocType.Payment, clsDocTransactionType.Bank, strBankLocaSeg, True)
            ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Arr(0).Document_Date), clsDocType.Payment, clsDocTransactionType.Cash, strBankLocaSeg, True)
            ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Arr(0).Document_Date), clsDocType.Payment, clsDocTransactionType.PettyCash, strBankLocaSeg, True)
            ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Arr(0).Document_Date), clsDocType.Payment, clsDocTransactionType.Others, strBankLocaSeg, True)
            Else
                Throw New Exception("Plase set the Bank Type for Bank " + clsCommon.myCstr(obj.Bank_Code))
            End If

            'Dim strbankaccount As String()
            Dim arrtax As New ArrayList()
            'Dim query As String = "select Tax_Account , TaxAcct_Description , Interest_Account , Interest_Acc_Desc , Others_Account , Other_Acct_Desc , Penalty_Account, Penalty_Acct_Desc   from TSPL_TDS_BRANCH_MASTER "

            Dim query As String = "Select Gl_Account  from TSPL_TDS_DEDUCTION_HEAD where TDS_Section='" + obj.Section_Code + "'"
            tdspayable = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query, trans))
            If clsCommon.myLen(tdspayable) <= 0 Then
                Throw New Exception("GL Account not found for section " + obj.Section_Code)
            End If
            tdspayabledesc = clsGLAccount.GetName(tdspayable, trans)

            Dim straccount As String = String.Empty
            Dim strbankacct As String = String.Empty
            Dim arrmis As New ArrayList()

            strbankacct = Convert.ToString(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))

            Dim dramt As Decimal = CDec(obj.Tax_Amount)
            Dim cramt As Decimal = CDec(obj.Tax_Amount) * -1
            Dim strbankaccount1 As String() = {strbankacct, Convert.ToString(cramt)}
            Dim strtdsaccount As String() = {tdspayable, Convert.ToString(dramt)}
            arrmis.Add(strbankaccount1)
            arrmis.Add(strtdsaccount)



            Dim narration As String = clsCommon.myCstr(obj.BSR_Code) + clsCommon.myCstr(obj.BSR_Name)
            Dim reference As String = clsCommon.myCstr(obj.Challan_No) + " " + clsCommon.myCstr(obj.Challan_Date)
            clsDBFuncationality.ExecuteNonQuery("update TSPL_REMITTANCE_ENTRY set Posted = 'Y' WHERE Remittance_Code = '" + strDocNo + "'", trans)

            '------Start Commit
            'clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", paymentdate), New SqlParameter("@paymentpostdate", paymentdate), New SqlParameter("@bankcode", clsCommon.myCstr(obj.Bank_Code)), New SqlParameter("@paymenttype", strgeneratecode), New SqlParameter("@vendorcode", clsCommon.myCstr(obj.Arr(0).Vendor_Code)), New SqlParameter("@vendorname", obj.Arr(0).Vendor_Name), New SqlParameter("@remitto", obj.Remit_To), New SqlParameter("@entrydesc", "Against Remitance No-" + obj.Remittance_Code + " " + clsCommon.myCstr(obj.Description)), New SqlParameter("@reference", reference), New SqlParameter("@narration", narration), New SqlParameter("@paymentcode", clsCommon.myCstr(obj.Payment_Code)), New SqlParameter("@chequeno", clsCommon.myCstr(obj.Cheque_No)), New SqlParameter("@chequedate", IIf(obj.Cheque_Date.HasValue, clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"), "null")), New SqlParameter("@paymentamount", CDec(clsCommon.myCdbl(obj.Tax_Amount))), New SqlParameter("@vendoraccountset", ""), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "1"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
            'clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + STRPAYmentno + "'", trans)
            'clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Remit_To = '" + clsCommon.myCstr(obj.Remit_To) + "', Entry_Desc = '" + clsCommon.myCstr(obj.Description) + "' where Payment_Code = '" + STRPAYmentno + "'", trans)
            'clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_DETAIL_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@paymentline", Convert.ToString(1)), New SqlParameter("@apply", ""), New SqlParameter("@vendorinvoiceno", ""), New SqlParameter("@paymenttype", strgeneratecode), New SqlParameter("@documentno", ""), New SqlParameter("@pendingbalance", "0"), New SqlParameter("@appliedamount", "0"), New SqlParameter("@originalamount", "0"), New SqlParameter("@tdsamoount", "0"), New SqlParameter("@netbalance", clsCommon.myCstr(obj.Tax_Amount)), New SqlParameter("@accountcode", tdspayable), New SqlParameter("@description", tdspayabledesc), New SqlParameter("@remark", ""), New SqlParameter("@comment", ""))
            'clsJournalMaster.FunGrnlEntryWithTrans(trans, obj.AP_Payment_Date, clsCommon.myCstr(obj.Description), srctype, strsrcdesc, STRPAYmentno, strdocdesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis)
            'trans.Commit()
            'Dim journalno As String = connectSql.RunScalar("select MAX(Voucher_No ) from TSPL_JOURNAL_MASTER")
            'connectSql.RunSql("update TSPL_JOURNAL_MASTER set Source_Desc = '" + strsrcdesc + " ', Remarks = '" + reference + "', Comments = '" + narration + "' where Voucher_No = '" + journalno + "'")
            '------end Commit


            Dim objPayment As New clsPaymentHeader()
            'objPayment.Payment_No = clsCommon.myCstr(txtPaymentNo.Value)
            objPayment.Entry_Desc = "Against Remitance No-" + obj.Remittance_Code + " " + clsCommon.myCstr(obj.Description)
            objPayment.Payment_Date = obj.AP_Payment_Date
            objPayment.Payment_Post_Date = obj.AP_Payment_Date
            objPayment.Bank_Code = obj.Bank_Code
            objPayment.Payment_Type = "MI"
            objPayment.Vendor_Code = obj.Arr(0).Vendor_Code
            objPayment.Vendor_Name = obj.Arr(0).Vendor_Name
            objPayment.Payment_Code = obj.Payment_Code
            If clsCommon.CompairString(objPayment.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                objPayment.Cheque_No = obj.Cheque_No
                objPayment.Cheque_Date = obj.Cheque_Date
            Else
                objPayment.Cheque_No = ""
                objPayment.Cheque_Date = Nothing
            End If

            objPayment.Payment_Amount = obj.Tax_Amount
            'objPayment.Total_Applied_Amount = clsCommon.myCdbl(txtPaymentAmt.Text)
            objPayment.Remit_To = obj.Remit_To



            objPayment.IsChkReverse = "N"
            'objPayment.objRemittance = objRemittance
            objPayment.ArrTr = New List(Of clsPaymentDetail)
            '============================Detail Section==============================




            Dim objPaymentTr As New clsPaymentDetail()
            objPaymentTr.Payment_Type = objPayment.Payment_Type
            objPaymentTr.Account_Code = tdspayable
            objPaymentTr.Description = tdspayabledesc
            objPaymentTr.Applied_Amount = 0
            objPaymentTr.Net_Balance = obj.Tax_Amount
            objPaymentTr.Remarks = ""
            'objPaymentTr.ESI_WCT_Percentage = ESI_Percent
            objPayment.ArrTr.Add(objPaymentTr)


            '==================Detail Section Ends Here=======================
            objPayment.SaveData(objPayment, True, trans)
            clsPaymentHeader.PostData(objPayment.Payment_No, "Payable", trans)
            trans.Commit()
            myMessages.post()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    'Public Shared Function PostData(ByVal strDocNo As String) As Boolean

    '    Dim obj As clsRemitanceEntry = clsRemitanceEntry.GetData(strDocNo)
    '    If clsCommon.myLen(obj.Remittance_Code) <= 0 Then
    '        Throw New Exception("Remittance code not found for post")
    '    End If
    '    If clsCommon.CompairString("Y", obj.Posted) = CompairStringResult.Equal Then
    '        Throw New Exception("Already Posted Document")
    '    End If
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

    '    Dim isSaved As Boolean = True
    '    Dim tdspayable As String = String.Empty



    '    Dim tdspayabledesc As String = String.Empty



    '    Dim srctype As String = "AP-MI"
    '    Dim strsrcdesc As String = "PAYMENT"
    '    Dim strgeneratecode As String = "MI"
    '    Dim STRPAYmentno As String = String.Empty
    '    Dim paymentdate As String = clsCommon.GetPrintDate(obj.AP_Payment_Date, "dd/MMM/yyyy")
    '    Dim strdocumentno As String = STRPAYmentno
    '    Dim strdocdesc As String = clsCommon.myCstr(obj.Description)
    '    Dim strsrctype As String = "V"
    '    Dim strsrctypecode As String = clsCommon.myCstr(obj.Arr(0).Vendor_Code)
    '    Dim strsrctypedesc As String = clsCommon.myCstr(obj.Arr(0).Vendor_Name)
    '    Try
    '        Dim strtaxaccount As String()
    '        Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE = '" + obj.Bank_Code + "'"
    '        Dim dtBank As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dtBank Is Nothing OrElse dtBank.Rows.Count <= 0 Then
    '            Throw New Exception("Bank type Not found for bank " + obj.Bank_Code)
    '        End If

    '        Dim strBankType As String = clsCommon.myCstr(dtBank.Rows(0)("Bank_type"))
    '        Dim strBankLocaSeg As String = clsCommon.myCstr(dtBank.Rows(0)("BANKACC"))
    '        strBankLocaSeg = strBankLocaSeg.Substring(strBankLocaSeg.Length - 3, 3)

    '        If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
    '            STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Arr(0).Document_Date), clsDocType.Payment, clsDocTransactionType.Bank, strBankLocaSeg, True)
    '        ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
    '            STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Arr(0).Document_Date), clsDocType.Payment, clsDocTransactionType.Cash, strBankLocaSeg, True)
    '        ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
    '            STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Arr(0).Document_Date), clsDocType.Payment, clsDocTransactionType.PettyCash, strBankLocaSeg, True)
    '        ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
    '            STRPAYmentno = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Arr(0).Document_Date), clsDocType.Payment, clsDocTransactionType.Others, strBankLocaSeg, True)
    '        Else
    '            Throw New Exception("Plase set the Bank Type for Bank " + clsCommon.myCstr(obj.Bank_Code))
    '        End If

    '        Dim strbankaccount As String()
    '        Dim arrtax As New ArrayList()
    '        'Dim query As String = "select Tax_Account , TaxAcct_Description , Interest_Account , Interest_Acc_Desc , Others_Account , Other_Acct_Desc , Penalty_Account, Penalty_Acct_Desc   from TSPL_TDS_BRANCH_MASTER "

    '        Dim query As String = "Select Gl_Account  from TSPL_TDS_DEDUCTION_HEAD where TDS_Section='" + obj.Section_Code + "'"
    '        tdspayable = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query, trans))
    '        If clsCommon.myLen(tdspayable) <= 0 Then
    '            Throw New Exception("GL Account not found for section " + obj.Section_Code)
    '        End If
    '        tdspayabledesc = clsGLAccount.GetName(tdspayable, trans)

    '        Dim straccount As String = String.Empty
    '        Dim strbankacct As String = String.Empty
    '        Dim arrmis As New ArrayList()

    '        strbankacct = Convert.ToString(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))

    '        Dim dramt As Decimal = CDec(obj.Tax_Amount)
    '        Dim cramt As Decimal = CDec(obj.Tax_Amount) * -1
    '        Dim strbankaccount1 As String() = {strbankacct, Convert.ToString(cramt)}
    '        Dim strtdsaccount As String() = {tdspayable, Convert.ToString(dramt)}
    '        arrmis.Add(strbankaccount1)
    '        arrmis.Add(strtdsaccount)



    '        Dim narration As String = clsCommon.myCstr(obj.BSR_Code) + clsCommon.myCstr(obj.BSR_Name)
    '        Dim reference As String = clsCommon.myCstr(obj.Challan_No) + " " + clsCommon.myCstr(obj.Challan_Date)
    '        clsDBFuncationality.ExecuteNonQuery("update TSPL_REMITTANCE_ENTRY set Posted = 'Y' WHERE Remittance_Code = '" + strDocNo + "'", trans)
    '        clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", paymentdate), New SqlParameter("@paymentpostdate", paymentdate), New SqlParameter("@bankcode", clsCommon.myCstr(obj.Bank_Code)), New SqlParameter("@paymenttype", strgeneratecode), New SqlParameter("@vendorcode", clsCommon.myCstr(obj.Arr(0).Vendor_Code)), New SqlParameter("@vendorname", obj.Arr(0).Vendor_Name), New SqlParameter("@remitto", obj.Remit_To), New SqlParameter("@entrydesc", "Against Remitance No-" + obj.Remittance_Code + " " + clsCommon.myCstr(obj.Description)), New SqlParameter("@reference", reference), New SqlParameter("@narration", narration), New SqlParameter("@paymentcode", clsCommon.myCstr(obj.Payment_Code)), New SqlParameter("@chequeno", clsCommon.myCstr(obj.Cheque_No)), New SqlParameter("@chequedate", IIf(obj.Cheque_Date.HasValue, clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"), "null")), New SqlParameter("@paymentamount", CDec(clsCommon.myCdbl(obj.Tax_Amount))), New SqlParameter("@vendoraccountset", ""), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "1"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
    '        clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + STRPAYmentno + "'", trans)
    '        clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Remit_To = '" + clsCommon.myCstr(obj.Remit_To) + "', Entry_Desc = '" + clsCommon.myCstr(obj.Description) + "' where Payment_Code = '" + STRPAYmentno + "'", trans)

    '        clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_PAYMENT_DETAIL_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@paymentline", Convert.ToString(1)), New SqlParameter("@apply", ""), New SqlParameter("@vendorinvoiceno", ""), New SqlParameter("@paymenttype", strgeneratecode), New SqlParameter("@documentno", ""), New SqlParameter("@pendingbalance", "0"), New SqlParameter("@appliedamount", "0"), New SqlParameter("@originalamount", "0"), New SqlParameter("@tdsamoount", "0"), New SqlParameter("@netbalance", clsCommon.myCstr(obj.Tax_Amount)), New SqlParameter("@accountcode", tdspayable), New SqlParameter("@description", tdspayabledesc), New SqlParameter("@remark", ""), New SqlParameter("@comment", ""))
    '        clsJournalMaster.FunGrnlEntryWithTrans(trans, obj.AP_Payment_Date, clsCommon.myCstr(obj.Description), srctype, strsrcdesc, STRPAYmentno, strdocdesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis)
    '        trans.Commit()
    '        Dim journalno As String = connectSql.RunScalar("select MAX(Voucher_No ) from TSPL_JOURNAL_MASTER")
    '        connectSql.RunSql("update TSPL_JOURNAL_MASTER set Source_Desc = '" + strsrcdesc + " ', Remarks = '" + reference + "', Comments = '" + narration + "' where Voucher_No = '" + journalno + "'")
    '        myMessages.post()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
End Class

Public Class clsRemitanceEntryDetail
#Region "Variables"
    Public SNO As Integer = 0
    Public Remittance_Code As String = Nothing
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Document_Type As String = Nothing
    Public Document_Amount As Double = 0
    Public Service_Type As String = Nothing
    Public Actual_TDS_Base As Double = 0
    Public Calculated_TDS_Base As Double = 0
    Public Actual_TDS As Double = 0
    Public Calculated_TDS As Double = 0
    Public Actual_Surcharge As Double = 0
    Public Calculated_Surcharge As Double = 0
    Public Actual_Edu_Cess As Double = 0
    Public Calculated_Edu_Cess As Double = 0
    Public Actual_Sec_Educess As Double = 0
    Public Calculated_Sec_Educess As Double = 0
    Public Actual_Total_TDS As Double = 0
    Public Calculated_Total_TDS As Double = 0
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public TDS_Per As Double = 0
    Public Surcharge_Per As Double = 0
    Public Edu_Cess_Per As Double = 0
    Public Sec_Educess_Per As Double = 0
    Public Fiscal_Year As String = Nothing
    Public Quarter As String = Nothing
    Public Deduction_Code As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strRemittanceCode As String, ByVal arr As List(Of clsRemitanceEntryDetail), ByVal trans As SqlTransaction) As Boolean
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsRemitanceEntryDetail In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "Remittance_Code", strRemittanceCode)
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
                clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)
                clsCommon.AddColumnsForChange(coll, "Service_Type", obj.Service_Type)
                clsCommon.AddColumnsForChange(coll, "Actual_TDS_Base", obj.Actual_TDS_Base)
                clsCommon.AddColumnsForChange(coll, "Calculated_TDS_Base", obj.Calculated_TDS_Base)
                clsCommon.AddColumnsForChange(coll, "Actual_TDS", obj.Actual_TDS)
                clsCommon.AddColumnsForChange(coll, "Calculated_TDS", obj.Calculated_TDS)
                clsCommon.AddColumnsForChange(coll, "Actual_Surcharge", obj.Actual_Surcharge)
                clsCommon.AddColumnsForChange(coll, "Calculated_Surcharge", obj.Calculated_Surcharge)
                clsCommon.AddColumnsForChange(coll, "Actual_Edu_Cess", obj.Actual_Edu_Cess)
                clsCommon.AddColumnsForChange(coll, "Calculated_Edu_Cess", obj.Calculated_Edu_Cess)
                clsCommon.AddColumnsForChange(coll, "Actual_Sec_Educess", obj.Actual_Sec_Educess)
                clsCommon.AddColumnsForChange(coll, "Calculated_Sec_Educess", obj.Calculated_Sec_Educess)

                clsCommon.AddColumnsForChange(coll, "Actual_Total_TDS", obj.Actual_Total_TDS)
                clsCommon.AddColumnsForChange(coll, "Calculated_Total_TDS", obj.Calculated_Total_TDS)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
                clsCommon.AddColumnsForChange(coll, "TDS_Per", obj.TDS_Per)
                clsCommon.AddColumnsForChange(coll, "Surcharge_Per", obj.Surcharge_Per)
                clsCommon.AddColumnsForChange(coll, "Edu_Cess_Per", obj.Edu_Cess_Per)
                clsCommon.AddColumnsForChange(coll, "Sec_Educess_Per", obj.Sec_Educess_Per)
                clsCommon.AddColumnsForChange(coll, "Fiscal_Year", obj.Fiscal_Year)
                clsCommon.AddColumnsForChange(coll, "Quarter", obj.Quarter)
                clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REMITTANCE_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class



