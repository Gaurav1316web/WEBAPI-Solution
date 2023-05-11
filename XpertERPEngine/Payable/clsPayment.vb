Imports System.Data.SqlClient
Imports System.Data
Imports common
Public Class clsPayment

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
    'update by vipin on 08/11/2012
    Public PostDate As String
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSourceCode As Boolean = False
        Dim isSaved As Boolean = True
        Dim Payment_Line_No As Integer = 0
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            Dim qry As String = "select TSPL_PAYMENT_HEADER.Payment_No,TSPL_PAYMENT_HEADER.Entry_Desc,TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_PAYMENT_HEADER.Vendor_Name,TSPL_PAYMENT_HEADER.Payment_Amount,TSPL_PAYMENT_HEADER.Payment_Type,TSPL_PAYMENT_HEADER.Bank_Code,TSPL_PAYMENT_HEADER.Reference,TSPL_PAYMENT_HEADER.Narration,TSPL_PAYMENT_HEADER.Total_Applied_Amount,TSPL_PAYMENT_DETAIL.Document_No,TSPL_PAYMENT_HEADER.Posted,TSPL_PAYMENT_HEADER.Location_Code,TSPL_PAYMENT_HEADER.Remit_To  from TSPL_PAYMENT_DETAIL right outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No where TSPL_PAYMENT_HEADER.Payment_No='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing Or dt.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Posted")), "P") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Document no : " + strDocNo + "")
            End If

            Dim LocBank As String = clsDBFuncationality.getSingleValue("select SUBSTRING(BANKACC ,6,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + dt.Rows(0)("Bank_Code") + "'", trans)



            Dim strentrydate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" Select Payment_Post_Date from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No='" + strDocNo + "' ", trans))
            Dim strentrydesc As String = "Payment Against" + " " + strDocNo
            Dim srctype As String = "AP-PY"
            Dim strsrcdesc As String = "PAYMENT"
            Dim strdocumentno As String = strDocNo
            Dim strdocdesc As String = clsCommon.myCstr(dt.Rows(0)("Entry_Desc"))

            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Remit_To"))) > 0 Then
                strdocdesc += " " + clsCommon.myCstr(dt.Rows(0)("Remit_To"))
            End If



            Dim strsrctype As String = "V"
            Dim strsrctypecode As String = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            Dim strsrctypedesc As String = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            Dim SettlementLoc As String = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            Dim Loc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "' ", trans))
            Dim straccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "' ", trans))


            straccount = clsERPFuncationality.ChangeGLAccountLocationSegment(straccount, LocBank, True, trans)





            Dim strbankacct As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans))
            strbankacct = clsERPFuncationality.ChangeGLAccountLocationSegment(strbankacct, LocBank, True, trans)


            Dim BankType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC,bank_type  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans))
            If BankType <> "B" Then

            End If
            Dim stradvance As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "' ", trans))
            stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, LocBank, True, trans)

            Dim drtotal As Double = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
            Dim crtotal As Double = -1 * clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
            Dim Acc1() As String = {strbankacct, crtotal}
            Dim Acc2() As String = {straccount, drtotal}
            Dim arr As New ArrayList()
            arr.Add(Acc1)
            arr.Add(Acc2)
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "PY") = CompairStringResult.Equal Then
                If (transportSql.FunGrnlEntryWithTrans(LocBank, True, trans, strentrydate, strdocdesc, srctype, strsrcdesc, strdocumentno, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arr, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")))) Then
                    isSourceCode = True
                End If
                If isSourceCode = True Then
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + strDocNo + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = 'P' where Payment_No = '" + strDocNo + "'", trans)
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AV") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "RC") = CompairStringResult.Equal Then
                Dim tds As Double = 0
                Dim prepayment As Double = 0
                Dim checkall As String = "select TDS_Amount , Total_Prepayment  from TSPL_PAYMENT_HEADER where Payment_No = '" + strDocNo + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(checkall, trans)
                If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                    tds = clsCommon.myCdbl(dtNew.Rows(0)("TDS_Amount"))
                    prepayment = clsCommon.myCdbl(dtNew.Rows(0)("Total_Prepayment"))
                End If
                If tds <> 0 And prepayment <> 0 Then
                    Dim strquery As String = "select r.Branch_GL_AC    from TSPL_TDS_BRANCH_MASTER bm join TSPL_REMITTANCE r on bm.Branch_Code = r.Branch_Code where r.Document_No = '" + strDocNo + "'"
                    Dim strtaxaccount As String = clsDBFuncationality.getSingleValue(strquery, trans)
                    strtaxaccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strtaxaccount, LocBank, True, trans)




                    Dim acc4() As String = {stradvance, (prepayment + tds)}
                    Dim acc3() As String = {strtaxaccount, -1 * tds}
                    Dim acc5() As String = {strbankacct, crtotal}
                    Dim arrtotal As New ArrayList()
                    arrtotal.Add(acc4)
                    arrtotal.Add(acc3)
                    arrtotal.Add(acc5)
                    If (transportSql.FunGrnlEntryWithTrans(LocBank, True, trans, strentrydate, strdocdesc, srctype, strsrcdesc, strdocumentno, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")))) Then
                        isSourceCode = True
                    End If
                Else
                    '-----Added By--Pankaj Kumar---on-----22/11/2012----------
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "RC") = CompairStringResult.Equal Then
                        drtotal = drtotal * -1
                        crtotal = crtotal * -1
                    End If
                    '---------------------------------------------------------
                    Dim arr6() As String = {stradvance, drtotal}
                    Dim arr7() As String = {strbankacct, crtotal}
                    Dim arrlist As New ArrayList()
                    arrlist.Add(arr6)
                    arrlist.Add(arr7)
                    If (transportSql.FunGrnlEntryWithTrans(LocBank, True, trans, strentrydate, strdocdesc, srctype, strsrcdesc, strdocumentno, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")))) Then
                        isSourceCode = True
                    End If
                End If
                If isSourceCode = True Then
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + strDocNo + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = 'P' where Payment_No = '" + strDocNo + "'", trans)
                End If
                '''''''''''''''''''''''''''''''''Modified By Mayank''''''''''''''''''''''''''''''
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "OA") = CompairStringResult.Equal Then
                Dim tds As Double = 0
                Dim prepayment As Double = 0
                Dim checkall As String = "select TDS_Amount , Total_Prepayment  from TSPL_PAYMENT_HEADER where Payment_No = '" + strDocNo + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(checkall, trans)
                If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                    tds = clsCommon.myCdbl(dtNew.Rows(0)("TDS_Amount"))
                    prepayment = clsCommon.myCdbl(dtNew.Rows(0)("Total_Prepayment"))
                End If
                If tds <> 0 And prepayment <> 0 Then
                    Dim strquery As String = "select r.Branch_GL_AC   from TSPL_TDS_BRANCH_MASTER bm join TSPL_REMITTANCE r on bm.Branch_Code = r.Branch_Code where r.Document_No = '" + strDocNo + "'"
                    Dim strtaxaccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strquery, trans))
                    strtaxaccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strtaxaccount, LocBank, True, trans)

                    Dim acc4() As String = {stradvance, prepayment + tds}
                    Dim acc3() As String = {strtaxaccount, -1 * tds}
                    Dim acc5() As String = {strbankacct, crtotal}
                    Dim arrtotal As New ArrayList()
                    arrtotal.Add(acc4)
                    arrtotal.Add(acc3)
                    arrtotal.Add(acc5)
                    If (transportSql.FunGrnlEntryWithTrans(LocBank, True, trans, strentrydate, strdocdesc, srctype, strsrcdesc, strdocumentno, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")))) Then
                        isSourceCode = True
                    End If
                Else
                    Dim arr6() As String = {stradvance, drtotal}
                    Dim arr7() As String = {strbankacct, crtotal}
                    Dim arrlist As New ArrayList()
                    arrlist.Add(arr6)
                    arrlist.Add(arr7)
                    If (transportSql.FunGrnlEntryWithTrans(LocBank, True, trans, strentrydate, strdocdesc, srctype, strsrcdesc, strdocumentno, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")))) Then
                        isSourceCode = True
                    End If
                End If
                Dim InvcNo As String = ""
                Dim BalAmt As Decimal = 0.0
                Dim PayAmt As Decimal = drtotal
                Dim strQ As String = "select Document_No,Due_Date ,case when fifo_balance>0 then fifo_balance else   Balance_Amt end as  Balance_Amt  from TSPL_VENDOR_INVOICE_HEAD where Balance_Amt>0  and Vendor_Code='" + strsrctypecode + "' and fifo_knockoff='N'" & _
                                     "order by TSPL_VENDOR_INVOICE_HEAD.Due_Date "
                Dim Dt1 As DataTable = New DataTable()
                Dt1 = clsDBFuncationality.GetDataTable(strQ, trans)
                For Each dr As DataRow In Dt1.Rows
                    InvcNo = dr.Item("Document_No").ToString()
                    BalAmt = dr.Item("Balance_Amt")
                    If drtotal > BalAmt Then
                        drtotal = drtotal - BalAmt
                        strQ = "update TSPL_VENDOR_INVOICE_HEAD set fifo_balance=0.00 , fifo_knockoff='Y' where Document_No ='" + InvcNo + "' and Vendor_Code ='" + strsrctypecode + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                    ElseIf drtotal < BalAmt Then
                        drtotal = drtotal - BalAmt
                        strQ = "update TSPL_VENDOR_INVOICE_HEAD set fifo_balance=" + (drtotal * -1).ToString() + "-fifo_balance , fifo_knockoff='N' where Document_No ='" + InvcNo + "' and Vendor_Code ='" + strsrctypecode + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                    End If
                    If drtotal < 0 Then
                        Exit For
                    End If
                Next
                If drtotal > 0 Then
                    strQ = "update TSPL_PAYMENT_HEADER set fifo_balance=" + drtotal.ToString() + " where Payment_No ='" + strDocNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                End If

                If isSourceCode = True Then
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + strDocNo + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = 'P' where Payment_No = '" + strDocNo + "'", trans)
                End If
                ''''''''''''''''''''''''''''''''''''''END''''''''''''''''''''''''''''''''''''''''''''''''''


            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "MI") = CompairStringResult.Equal Then
                Dim arrmis As New ArrayList()
                Dim ESiAmt As Decimal = 0.0
                Dim MiscAmt As Decimal = 0.0
                Dim ESI_Percent As Decimal = 0.0
                qry = "select Account_code,Net_Balance,Remarks from TSPL_PAYMENT_detail where Payment_No='" + strDocNo + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                For Each dr As DataRow In dtNew.Rows
                    Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCstr(dr("Net_Balance")), clsCommon.myCstr(dr("Remarks"))}
                    arrmis.Add(acc3)
                    'If clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) > 0 Then
                    '    MiscAmt = MiscAmt + (clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))))
                    'End If
                    If clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) < 0 Then
                        ESiAmt = ESiAmt + (clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) * -1)
                    End If

                    'Payment_Line_No = Payment_Line_No + 1
                    ''clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + strDocNo + "'", trans)
                    'clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = 'P' where Payment_No = '" + strDocNo + "'  and Payment_Line_No=" & Payment_Line_No & " ", trans)

                Next
                'ESI_Percent = (ESiAmt / MiscAmt) * 100
                Dim strbankacct1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans))
                If SettlementLoc <> "" Then
                    strbankacct = clsERPFuncationality.ChangeGLAccountLocationSegment(strbankacct, SettlementLoc, False, trans)
                End If
                Dim Acc4() As String = {strbankacct, crtotal - ESiAmt}
                arrmis.Add(Acc4)
                If ESiAmt <> 0 Then
                    Dim Acc5() As String = {strbankacct, ESiAmt}
                    arrmis.Add(Acc5)
                End If
                srctype = "AP-MI"
                If (transportSql.FunGrnlEntryWithTrans(LocBank, True, trans, strentrydate, strdocdesc, srctype, strsrcdesc, strdocumentno, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")))) Then
                    isSourceCode = True
                End If
                If isSourceCode = True Then
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + strDocNo + "'", trans)
                    For Each dr As DataRow In dtNew.Rows
                        Payment_Line_No = Payment_Line_No + 1
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = 'P' where Payment_No = '" + strDocNo + "'  and Payment_Line_No=" & Payment_Line_No & " ", trans)
                    Next
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AD") = CompairStringResult.Equal Then
                Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Total_Prepayment  from  TSPL_PAYMENT_HEADER where Payment_No like '" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'  and Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "'", trans))
                Dim arrcontrol() As String = {straccount, clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))}
                Dim arradvance() As String = {stradvance, -1 * clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))}
                Dim applydocument As New ArrayList()
                applydocument.Add(arrcontrol)
                applydocument.Add(arradvance)
                If (transportSql.FunGrnlEntryWithTrans(LocBank, True, trans, strentrydate, strdocdesc, srctype, strsrcdesc, strdocumentno, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, applydocument, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")))) Then
                    isSourceCode = True
                End If
                If isSourceCode = True Then
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + strDocNo + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = 'P' where Payment_No = '" + strDocNo + "'", trans)
                End If

            End If
            If isSourceCode = True Then
                qry = "update TSPL_REMITTANCE set Remit_TDS='N' where Document_No='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Dim stramount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT d.Applied_Amount FROM  TSPL_PAYMENT_HEADER h INNER JOIN TSPL_PAYMENT_DETAIL d ON h.Payment_No = d.Payment_No where h.Payment_No = '" + strDocNo + "'", trans))
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = 'P' where Payment_No = '" + strDocNo + "'", trans)
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = 'P' where Payment_No = '" + strDocNo + "'", trans)
                ''trans.Rollback()
                 
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsPaymentTerms
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        ''richa Ticket no.BM00000003438 on 20/08/2014
        Dim qry As String = " select Terms_Code as [Code],Terms_Desc as [Description],No_Days as [No of Days],case when LCRequired=0 then CAST(0 as BIT) else CAST(1 as BIT) end as [LC Required], Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code]  from TSPL_TERMS_MASTER    "
        str = clsCommon.ShowSelectForm("RPTTRMFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsPaymentCode
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Payment_Code as [Code],Payment_Desc as [Description],Payment_Type as [Payment Type],bank_code as [Bank Code],Created_By as [Creatd By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code] from TSPL_PAYMENT_CODE  "
        str = clsCommon.ShowSelectForm("RPTPMTCDFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
