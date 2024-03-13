Imports common
Imports System.Data.SqlClient
'' Updated By priti as on 31/10/2012 01:05 Am 

Public Class clsBankTrasnferNew
    Public Transfer_No As String = ""
    Public Transfer_Date As Date
    Public Description As String
    Public Reference As String = ""
    Public Check_Print As Integer
    Public Check_Code As String = ""
    Public From_Bank_Code As String
    Public To_Bank_Code As String
    Public From_Bank_Name As String
    Public From_Bank_Acc_No As String
    Public To_Bank_Acc_No As String
    Public From_Bank_GL_Acc As String = ""
    Public Cheque_No As String
    Public Transfer_Amount As Decimal = 0
    Public Transaction_Type As String = ""
    Public Bank_Charges_Ac As String = String.Empty
    Public BankCharges As Decimal = 0
    Public Payment_Mode As String = ""
    Public To_Bank_GL_Amount As Decimal
    Public From_Bank_GL_Amount As Decimal
    Public From_Bank_GLAcc_Desc As String = ""
    Public To_Bank_Name As String = ""
    Public Deposit_Amount As Decimal
    Public To_Bank_GL_Acc As String = ""
    Public To_Bank_GLAcc_Desc As String = ""
    Public Against_Withdrawal_No As String = ""
    Public Remitt_To As String
    Public Cheque_Date As Date?
    Public Shared Function PostData(ByVal strDocno As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            PostData(strDocno, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsBankTrasnferNew, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            'Dim coll As New Hashtable()
            If clsCommon.CompairString(obj.From_Bank_Code, obj.To_Bank_Code) = CompairStringResult.Equal Then
                Throw New Exception("From bank code and To bank code should not be same")                
            End If
           
            Dim STR As String = ""
            Dim qry As String = "select TSPL_BANK_MASTER.Bank_type,TSPL_BANK_MASTER.BANKACC,TSPL_GL_ACCOUNTS.Account_Seg_Code7 from TSPL_BANK_MASTER " & _
                                   " left join TSPL_GL_ACCOUNTS on TSPL_BANK_MASTER.BANKACC=TSPL_GL_ACCOUNTS.Account_Code where BANK_CODE='" & obj.From_Bank_Code & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
            Dim LocSegmentCode As String = clsCommon.myCstr(dt.Rows(0)("Account_Seg_Code7"))
            If clsCommon.myLen(LocSegmentCode) <= 0 Then
                Throw New Exception("Location segment not set for Bank Acc of the bank " & obj.From_Bank_Code & "")
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BankTransferRunPaymentCounter, clsFixedParameterCode.BankTransferRunPaymentCounter, trans)) = 1 Then
                'Dim qry As String = "select TSPL_BANK_MASTER.Bank_type,TSPL_BANK_MASTER.BANKACC,TSPL_GL_ACCOUNTS.Account_Seg_Code7 from TSPL_BANK_MASTER " & _
                '                    " left join TSPL_GL_ACCOUNTS on TSPL_BANK_MASTER.BANKACC=TSPL_GL_ACCOUNTS.Account_Code where BANK_CODE='" & obj.From_Bank_Code & "'"
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                'Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                'Dim LocSegmentCode As String = clsCommon.myCstr(dt.Rows(0)("Account_Seg_Code7"))
                'If clsCommon.myLen(LocSegmentCode) <= 0 Then
                '    Throw New Exception("Location segment not set for Bank Acc of the bank " & obj.From_Bank_Code & "")
                'End If
                'If (BankAcc.Length >= 3) Then
                '    BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                '    If (IsNumeric(BankAcc)) Then
                '        Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                '    End If
                'Else
                '    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                'End If

                If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.Payment, clsDocTransactionType.Bank, LocSegmentCode, True)
                ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.Payment, clsDocTransactionType.Cash, LocSegmentCode, True)
                ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.Payment, clsDocTransactionType.PettyCash, LocSegmentCode, True)
                ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.Payment, clsDocTransactionType.Others, LocSegmentCode, True)
                ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.Payment, clsDocTransactionType.Others, LocSegmentCode, True)
                Else
                    Throw New Exception("Plase set the Bank Type for Bank SETTLEMENT")
                End If
            Else
                'STR = funautogenerateno(trans)
                STR = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.ContraVoucher, "", LocSegmentCode, True)
            End If
            If clsCommon.myLen(STR) <= 0 Then
                Throw New Exception("Error in code generation")
            End If
            obj.Transfer_No = STR
            isNewEntry = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Transfer_No) as total from TSPL_BANK_TRANSFER where Transfer_No='" & STR & "'", trans)) > 0, False, True)

            Dim coll As New Hashtable()


            clsCommon.AddColumnsForChange(coll, "Check_Code", obj.Check_Code, True)
            clsCommon.AddColumnsForChange(coll, "Check_Print", obj.Check_Print)
            If Not obj.Cheque_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No, True)
            clsCommon.AddColumnsForChange(coll, "Deposit_Amount", obj.Deposit_Amount)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "From_Bank_Acc_No", obj.From_Bank_Acc_No)
            clsCommon.AddColumnsForChange(coll, "From_Bank_Code", obj.From_Bank_Code)
            clsCommon.AddColumnsForChange(coll, "From_Bank_GL_Acc", obj.From_Bank_GL_Acc)
            clsCommon.AddColumnsForChange(coll, "From_Bank_GL_Amount", obj.From_Bank_GL_Amount)
            clsCommon.AddColumnsForChange(coll, "From_Bank_GLAcc_Desc", obj.From_Bank_GLAcc_Desc)
            clsCommon.AddColumnsForChange(coll, "From_Bank_Name", obj.From_Bank_Name)
            clsCommon.AddColumnsForChange(coll, "Payment_Mode", obj.Payment_Mode)            
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Remitt_To", obj.Remitt_To, True)
            clsCommon.AddColumnsForChange(coll, "To_Bank_Acc_No", obj.To_Bank_Acc_No)
            clsCommon.AddColumnsForChange(coll, "To_Bank_Code", obj.To_Bank_Code)
            clsCommon.AddColumnsForChange(coll, "To_Bank_GL_Acc", obj.To_Bank_GL_Acc, True)
            clsCommon.AddColumnsForChange(coll, "To_Bank_GL_Amount", obj.To_Bank_GL_Amount)
            clsCommon.AddColumnsForChange(coll, "To_Bank_GLAcc_Desc", obj.To_Bank_GLAcc_Desc, True)
            clsCommon.AddColumnsForChange(coll, "To_Bank_Name", obj.To_Bank_Name)

            clsCommon.AddColumnsForChange(coll, "Transfer_Amount", obj.Transfer_Amount)
            clsCommon.AddColumnsForChange(coll, "Transfer_Date", clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Transfer_No", obj.Transfer_No)
            clsCommon.AddColumnsForChange(coll, "Transfer_Posting_Date", clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy"))

            '' other columns
            clsCommon.AddColumnsForChange(coll, "Post", "N")

           
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            'Dim PostingDate As String = clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy")
            'connectSql.RunSpTransaction(trans, "sp_tspl_banktransfer_insert", New SqlParameter("@Transfer_No", STR), New SqlParameter("@Transfer_Date", PostingDate), New SqlParameter("@Transfer_Posting_Date", PostingDate), New SqlParameter("@Description", obj.Description), New SqlParameter("@Reference", obj.Reference), New SqlParameter("@From_Bank_Code", obj.From_Bank_Code), New SqlParameter("@From_Bank_Name", obj.From_Bank_Name), New SqlParameter("@From_Bank_Acc_No", obj.From_Bank_Acc_No), New SqlParameter("@Transfer_Amount", obj.Transfer_Amount), New SqlParameter("@From_Bank_GL_Acc", obj.From_Bank_GL_Acc), New SqlParameter("@From_Bank_GLAcc_Desc", obj.From_Bank_GLAcc_Desc), New SqlParameter("@From_Bank_GL_Amount", obj.From_Bank_GL_Amount), New SqlParameter("@To_Bank_Code", obj.To_Bank_Code), New SqlParameter("@To_Bank_Name", obj.To_Bank_Name), New SqlParameter("@To_Bank_Acc_No", obj.To_Bank_Acc_No), New SqlParameter("@Deposit_Amount", obj.Deposit_Amount), New SqlParameter("@To_Bank_GL_Acc", obj.To_Bank_GL_Acc), New SqlParameter("@To_Bank_GLAcc_Desc", obj.To_Bank_GLAcc_Desc), New SqlParameter("@To_Bank_GL_Amount", obj.To_Bank_GL_Amount), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@comp_code", objCommonVar.CurrentCompanyCode), New SqlParameter("@Cheque_No", obj.Cheque_No), New SqlParameter("@Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy")), New SqlParameter("@Payment_Mode", obj.Payment_Mode), New SqlParameter("@frmbnkaccno", obj.From_Bank_Acc_No), New SqlParameter("@tobnkaccno", obj.To_Bank_Acc_No))
            Dim InTransitSettings As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InTransitFeatureIsRequired, clsFixedParameterCode.InTransitFeatureIsRequired, trans))
            If clsCommon.CompairString(InTransitSettings, "1") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Transaction_Type", obj.Transaction_Type)
                'clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Transaction_Type='" & obj.Transaction_Type & "',Against_Withdrawal_No=NULL Where Transfer_No='" & STR & "'", trans)
                If clsCommon.CompairString(obj.Transaction_Type, "R") = CompairStringResult.Equal AndAlso obj.Transfer_Amount > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Against_Withdrawal_No", obj.Against_Withdrawal_No, True)
                    'clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Against_Withdrawal_No='" & obj.Against_Withdrawal_No & "' Where Transfer_No='" & STR & "'", trans)
                Else
                    clsCommon.AddColumnsForChange(coll, "Against_Withdrawal_No", "", True)
                End If
            Else
                clsCommon.AddColumnsForChange(coll, "Transaction_Type", "B")
                clsCommon.AddColumnsForChange(coll, "Against_Withdrawal_No", "", True)
                ' ''  '' Balwinder on  11-Jan-2015 change transaction_type null to 'B' because trigger not fired for bank book is null exists
                'clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Transaction_Type='B', Against_Withdrawal_No=NULL Where Transfer_No='" & STR & "'", trans)
            End If

            If isNewEntry Then               
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))                
                isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_TRANSFER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_TRANSFER", OMInsertOrUpdate.Update, "TSPL_BANK_TRANSFER.Transfer_No='" & STR & "'", trans)
            End If

            obj.Transfer_No = STR                        
        Catch ex As Exception           
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function funautogenerateno(ByVal Tran As SqlTransaction) As String
        Dim strgeneratecode As String = ""
        Dim strgenerate As String
        Dim total As String = ""
        Dim cutgenerate As String
        Try
            strgenerate = strgeneratecode + "%"
            Dim str1 As String = ""
            Dim check As Integer
            If Tran IsNot Nothing Then
                check = connectSql.RunScalar(Tran, "select count(*) from TSPL_BANK_TRANSFER")
            Else
                check = connectSql.RunScalar("select count(*) from TSPL_BANK_TRANSFER")
            End If
            If check <> 0 Then
                If Tran IsNot Nothing Then
                    str1 = connectSql.RunScalar(Tran, "select MAX(Transfer_No)  from TSPL_BANK_TRANSFER  where Transfer_No like 'TN%'")
                Else
                    str1 = connectSql.RunScalar("select MAX(Transfer_No)  from TSPL_BANK_TRANSFER  where Transfer_No like 'TN%'")
                End If
            Else
            End If
            If String.IsNullOrEmpty(str1) Then
                total = "TN" + "000001"
            Else
                cutgenerate = str1.Substring(2, 6)
                Dim i As Integer = Integer.Parse(cutgenerate)
                i = i + 1
                Dim stri As String = CStr(i)
                If stri.Length = 1 Then
                    Dim t As String = Convert.ToString(i)
                    total = Convert.ToString("TN" + "00000" + t)
                ElseIf stri.Length = 2 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + "0000" + t
                ElseIf stri.Length = 3 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + "000" + t
                ElseIf stri.Length = 4 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + "00" + t
                ElseIf stri.Length = 5 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + "0" + t
                ElseIf stri.Length = 6 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + t
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return total

    End Function

    Public Shared Function GetQuery(ByVal strDocno As String) As String
        Return "select From_Bank_Acc_No,Transfer_Amount,To_Bank_Acc_No,Transfer_No,Deposit_Amount,Transfer_Posting_Date,Description,Transfer_No,Post,From_Bank_Code ,To_Bank_Code,ISNULL(Transaction_Type,'') AS Transaction_Type,isnull(BankCharges,0) as BankCharges,isnull(Bank_Charges_Ac,'') as Bank_Charges_Ac  from TSPL_BANK_TRANSFER where Transfer_No='" + strDocno + "'"
    End Function


    Public Shared Function PostData(ByVal strDocno As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Dim isOuterTrans As Boolean = False
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
            isOuterTrans = False
        Else
            isOuterTrans = True
        End If
        Try
            If clsCommon.myLen(strDocno) <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            '' Anubhooti 05-Sep-2014 BM00000003437 (Only Fetches ,From_Bank_Code ,To_Bank_Code)
            Dim qry As String = GetQuery(strDocno)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            If clsCommon.CompairString("p", clsCommon.myCstr(dt.Rows(0)("Post"))) = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Document")
            End If

            '' Validation check: by Panch Raj against ticket No:BM00000008437
            Dim obj As clsBankTrasnferNew = clsBankTrasnferNew.GetData(strDocno, NavigatorType.Current, trans)
            '  CheckNegativeBankBalance(obj, trans)

            CreateJournalEntry(strDocno, dt, trans)


            qry = "update tspl_bank_transfer set post = 'p' where transfer_no = '" + strDocno + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsBankReco.SetOutstandingEntry(strDocno, clsCommon.myCDate(dt.Rows(0)("Transfer_Posting_Date")), "BankTransfer", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocno, "TSPL_BANK_TRANSFER", "Transfer_No", trans)
            If isOuterTrans = False Then
                If isSaved Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            End If


        Catch ex As Exception
            If isOuterTrans = False Then
                trans.Rollback()
            End If
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNegativeBankBalance(ByVal obj As clsBankTrasnferNew, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        '' Validation check: by Panch Raj against ticket No:BM00000008437
        Dim Bank_Code As String = ""
        Dim Bank_GL_Location As String = ""
        Try
        
        If clsCommon.CompairString(obj.Transaction_Type, "W") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Transaction_Type, "B") = CompairStringResult.Equal Then
            Bank_Code = obj.From_Bank_Code
            Bank_GL_Location = clsGLAccount.Get_Location_Segment(obj.From_Bank_Acc_No, trans)
        ElseIf clsCommon.CompairString(obj.Transaction_Type, "R") = CompairStringResult.Equal Then
            'Bank_Code = obj.To_Bank_Code
            'Bank_GL_Location = clsGLAccount.Get_Location_Segment(obj.To_Bank_Acc_No, trans)
            Return True
        End If
        'Bank_Code = obj.From_Bank_Code
        'Bank_GL_Location = clsGLAccount.Get_Location_Segment(obj.From_Bank_Acc_No, trans)

        Dim Bank_Type_Check As String = "0"
        Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, trans)

        Dim Bank_Type As String = clsBankMaster.GetBankType(Bank_Code, trans)
        Dim Bank_Balance As Double = 0
        If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
            '' allow for all
        ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
            If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                ''richa agarwal 03 Aug,2016
                'Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_Date, Bank_Code, Bank_GL_Location, trans)
                Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_No, obj.Transfer_Date, Bank_Code, Bank_GL_Location, trans)
                If Bank_Balance < obj.Transfer_Amount Then
                    Throw New Exception("Payment Amount : " & obj.Transfer_Amount & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
                End If
            End If
        ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
            If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                ''richa agarwal 03 Aug,2016
                'Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_Date, Bank_Code, Bank_GL_Location, trans)
                Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_No, obj.Transfer_Date, Bank_Code, Bank_GL_Location, trans)
                If Bank_Balance < obj.Transfer_Amount Then
                    Throw New Exception("Payment Amount : " & obj.Transfer_Amount & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
                End If
            End If
        ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
            ''richa agarwal 03 Aug,2016
            'Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_Date, Bank_Code, Bank_GL_Location, trans)
            Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_No, obj.Transfer_Date, Bank_Code, Bank_GL_Location, trans)
            If Bank_Balance < obj.Transfer_Amount Then
                Throw New Exception("Payment Amount : " & obj.Transfer_Amount & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
            End If
        End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsBankTrasnferNew
        Dim obj As New clsBankTrasnferNew
        Dim qry As String = "select * from TSPL_BANK_TRANSFER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TRANSFER_NO = (select MIN(TRANSFER_NO) from TSPL_BANK_TRANSFER)"
            Case NavigatorType.Last
                qry += " and TRANSFER_NO = (select Max(TRANSFER_NO) from TSPL_BANK_TRANSFER)"
            Case NavigatorType.Next
                qry += " and TRANSFER_NO = (select Min(TRANSFER_NO) from TSPL_BANK_TRANSFER where  TRANSFER_NO>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TRANSFER_NO = (select Max(TRANSFER_NO) from TSPL_BANK_TRANSFER where TRANSFER_NO<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TRANSFER_NO = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.Transfer_No = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Check_Print = clsCommon.myCstr(dt.Rows(0)("Check_Print"))
            obj.Check_Code = clsCommon.myCstr(dt.Rows(0)("Check_Code"))
            obj.From_Bank_Code = clsCommon.myCstr(dt.Rows(0)("From_Bank_Code"))
            obj.To_Bank_Code = clsCommon.myCstr(dt.Rows(0)("To_Bank_Code"))
            obj.From_Bank_Name = clsCommon.myCstr(dt.Rows(0)("From_Bank_Name"))
            obj.From_Bank_Acc_No = clsCommon.myCstr(dt.Rows(0)("From_Bank_Acc_No"))
            obj.To_Bank_Acc_No = clsCommon.myCstr(dt.Rows(0)("To_Bank_Acc_No"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            obj.Transfer_Date = clsCommon.myCDate(dt.Rows(0)("Transfer_Date"))
            obj.Transfer_Amount = clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))
            obj.Transaction_Type = clsCommon.myCstr(dt.Rows(0)("Transaction_Type"))
            obj.BankCharges = clsCommon.myCdbl(dt.Rows(0)("BankCharges"))
            obj.Bank_Charges_Ac = clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))
        End If
        Return obj
    End Function

    Public Shared Function CreateJournalEntry(ByVal strDocno As String, ByVal dt As DataTable, ByVal trans As SqlTransaction) As Boolean
        Return CreateJournalEntry(strDocno, "", dt, trans)
    End Function

    Public Shared Function CreateJournalEntry(ByVal strDocno As String, ByVal strJENo As String, ByVal dt As DataTable, ByVal trans As SqlTransaction) As Boolean
        Dim arrylst As ArrayList = New ArrayList()
        Dim strFromBankAC As String
        Dim strToBankAC As String
        Dim strFromSeg As String = ""
        Dim strToSeg As String = ""
        Dim UseSubAcc As String
        Dim strBankChargesAcc As String = Nothing
        Dim FromBankTypeOfBank As String = ""
        Dim ToBankTypeOfBank As String = ""
        '' Anubhooti 17-Dec-2014 BM00000004959 (In Transit Settings For Withdrawal/Receipt/Both Cases) TEC/01/03/19-000433
        Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
        Dim InTransitSettings As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InTransitFeatureIsRequired, clsFixedParameterCode.InTransitFeatureIsRequired, trans))
        If clsCommon.CompairString(InTransitSettings, "0") = CompairStringResult.Equal Then
            '' Anubhooti 30-Jan-2015 (Remarks : If Bank Code is of bank type only then sub account will consider)
            FromBankTypeOfBank = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans))
            ToBankTypeOfBank = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) + "'", trans))
            UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    strFromBankAC = clsDBFuncationality.getSingleValue("Select ISNULL(Sub_Account,'') AS Sub_Account FROM TSPL_BANK_MASTER WHERE BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans)
                Else
                    strFromBankAC = clsCommon.myCstr(dt.Rows(0)("From_Bank_Acc_No"))
                End If
                If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    strToBankAC = clsDBFuncationality.getSingleValue("Select ISNULL(Sub_Account,'') AS Sub_Account FROM TSPL_BANK_MASTER WHERE BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) + "'", trans)
                Else
                    strToBankAC = clsCommon.myCstr(dt.Rows(0)("To_Bank_Acc_No"))
                End If
                ''richa BHA/13/09/18-000545
                If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    strBankChargesAcc = clsDBFuncationality.getSingleValue("Select ISNULL(Sub_Account,'') AS Sub_Account FROM TSPL_BANK_MASTER WHERE BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) + "'", trans)
                Else
                    strBankChargesAcc = clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))
                End If
            Else
                strFromBankAC = clsCommon.myCstr(dt.Rows(0)("From_Bank_Acc_No"))
                strToBankAC = clsCommon.myCstr(dt.Rows(0)("To_Bank_Acc_No"))
                ''richa BHA/13/09/18-000545
                strBankChargesAcc = clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))
                ''---------------
            End If
            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal Then
                If clsCommon.myLen(strFromBankAC) <= 0 AndAlso clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")))
                End If

                If clsCommon.myLen(strToBankAC) <= 0 AndAlso clsCommon.CompairString(ToBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")))
                End If
                ''richa KDI/02/01/19-000446 2 Jan,2019
                If clsCommon.myLen(strBankChargesAcc) <= 0 AndAlso clsCommon.CompairString(ToBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(0)("BankCharges"))) > 0 Then
                    Throw New Exception("Please enter sub account/Bank Charges Acc for bank " + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")))
                End If
            End If
            ''richa BHA/13/09/18-000545
            Dim acc1() As String = {strFromBankAC, -1 * (clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount")) + clsCommon.myCdbl(dt.Rows(0)("BankCharges"))), "", "", "", "", "", "", "B"}
            arrylst.Add(acc1)

            ''richa agarwal 28 Feb,2019 if branch account is off and segment of to bank is not same as From Bank then to Bank Seg code replaced with To Bank Seg Code
            If isApplyBrachAccounting = False Then
                Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans)
                strToBankAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strToBankAC, BankLocation, True, trans)

            End If
            Dim acc2() As String = {strToBankAC, clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount")), "", "", "", "", "", "", "B"}
            arrylst.Add(acc2)

            ''richa BHA/13/09/18-000545
            If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dt.Rows(0)("BankCharges")) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))) <= 0 Then
                Throw New Exception("Please enter credit account for bank " + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")))
            End If

            If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dt.Rows(0)("BankCharges")) > 0 Then
                Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) + "'", trans)

                strBankChargesAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankChargesAcc, BankLocation, True, trans)
                Dim acc13() As String = {strBankChargesAcc, clsCommon.myCdbl(dt.Rows(0)("BankCharges")), "", "", "", "", "", "", "B"}
                arrylst.Add(acc13)

            End If
            ''-------------------------end of bank charges

            strFromSeg = strFromBankAC.Substring(clsCommon.myLen(strFromBankAC) - 3, 3)
            If clsCommon.myLen(strToBankAC) > 0 Then
                strToSeg = strToBankAC.Substring(clsCommon.myLen(strToBankAC) - 3, 3)
            End If

            If Not clsCommon.CompairString(strFromSeg, strToSeg) = CompairStringResult.Equal And isApplyBrachAccounting = True Then
                '' When Branch Accounting is ON (BOTH CASE) richa agarwal SWA/03/12/18-000061
                Dim strTempAC As String = String.Empty
                If isApplyBrachAccounting = True Then
                    strTempAC = ClsBranchAccountMapping.GetBranchAccount(strToSeg, strFromSeg, trans)
                    If clsCommon.myLen(strTempAC) <= 0 Then
                        Throw New Exception("Please set Branch account mapping with from location " + strFromSeg + " and to location " + strToSeg)
                    End If
                Else
                    strTempAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP(1) Transfer_Clearing FROM TSPL_PURCHASE_ACCOUNTS", trans))
                    If clsCommon.myLen(strTempAC) <= 0 Then
                        Throw New Exception("Please set Transfer_Clearing")
                    End If
                End If
                ''------------------------

                If clsCommon.myLen(strTempAC) > 0 Then
                    ''richa BHA/13/09/18-000545
                    strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strFromSeg, True, trans)
                    Dim acc3() As String = {strTempAC, clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))}
                    arrylst.Add(acc3)

                    strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strToSeg, True, trans)
                    Dim acc4() As String = {strTempAC, -1 * (clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount")) + clsCommon.myCdbl(dt.Rows(0)("BankCharges")))}
                    arrylst.Add(acc4)

                    ''richa BHA/13/09/18-000545 bank charges
                    strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strToSeg, True, trans)
                    Dim acc5() As String = {strTempAC, 1 * clsCommon.myCdbl(dt.Rows(0)("BankCharges"))}
                    arrylst.Add(acc5)
                    ''-----------
                Else
                    clsCommon.MyMessageBoxShow("Transfer Clearing account not found.")
                    Return False
                End If
            End If
            '' Anubhooti 17-Dec-2014 BM00000004959
        Else
            '' Anubhooti 30-Jan-2015 (Remarks : If Bank Code is of bank type only then sub account will consider)
            FromBankTypeOfBank = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans))
            ToBankTypeOfBank = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) + "'", trans))

            UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    strFromBankAC = clsDBFuncationality.getSingleValue("Select ISNULL(Sub_Account,'') AS Sub_Account FROM TSPL_BANK_MASTER WHERE BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans)
                Else
                    strFromBankAC = clsCommon.myCstr(dt.Rows(0)("From_Bank_Acc_No"))
                End If
                If clsCommon.CompairString(ToBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    strToBankAC = clsDBFuncationality.getSingleValue("Select ISNULL(Sub_Account,'') AS Sub_Account FROM TSPL_BANK_MASTER WHERE BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) + "'", trans)
                Else
                    strToBankAC = clsCommon.myCstr(dt.Rows(0)("To_Bank_Acc_No"))
                End If
                ''richa BHA/13/09/18-000545
                If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    strBankChargesAcc = clsDBFuncationality.getSingleValue("Select ISNULL(Sub_Account,'') AS Sub_Account FROM TSPL_BANK_MASTER WHERE BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) + "'", trans)
                Else
                    strBankChargesAcc = clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))
                End If
            Else
                strFromBankAC = clsCommon.myCstr(dt.Rows(0)("From_Bank_Acc_No"))
                strToBankAC = clsCommon.myCstr(dt.Rows(0)("To_Bank_Acc_No"))
                ''richa BHA/13/09/18-000545
                strBankChargesAcc = clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))
                ''---------------
            End If
            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal Then
                If clsCommon.myLen(strFromBankAC) <= 0 AndAlso clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")))
                End If
                If clsCommon.myLen(strToBankAC) <= 0 AndAlso clsCommon.CompairString(ToBankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")))
                End If
                ''richa BHA/13/09/18-000545 bank charges KDI/02/01/19-000446
                If clsCommon.myLen(strBankChargesAcc) <= 0 AndAlso clsCommon.CompairString(ToBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(0)("BankCharges"))) > 0 Then
                    Throw New Exception("Please enter sub account/Bank Charges Acc for bank " + clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")))
                End If
            End If

            strFromSeg = strFromBankAC.Substring(clsCommon.myLen(strFromBankAC) - 3, 3)
            strToSeg = strToBankAC.Substring(clsCommon.myLen(strToBankAC) - 3, 3)

            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Transaction_Type")), "W") = CompairStringResult.Equal Then '' WITHDRAWAL
                If isApplyBrachAccounting = False Or (isApplyBrachAccounting = True AndAlso (clsCommon.CompairString(strFromSeg, strToSeg) = CompairStringResult.Equal)) Then
                    Dim strTempAC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Transfer_Clearing_Account FROM TSPL_BANK_MASTER WHERE BANK_CODE ='" & clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) & "'", trans))
                    If clsCommon.myLen(strTempAC) > 0 Then
                        strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strFromSeg, True, trans)
                        Dim acc3() As String = {strTempAC, clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))}
                        arrylst.Add(acc3) '' DEBIT

                        strFromBankAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromBankAC, strFromSeg, True, trans)
                        Dim acc4() As String = {strFromBankAC, -1 * (clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount")) + clsCommon.myCdbl(dt.Rows(0)("BankCharges"))), "", "", "", "", "", "", "B"}
                        arrylst.Add(acc4) '' CREDIT

                        ''richa BHA/13/09/18-000545
                        If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dt.Rows(0)("BankCharges")) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))) <= 0 Then
                            Throw New Exception("Please enter credit account for bank " + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")))
                        End If

                        If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dt.Rows(0)("BankCharges")) > 0 Then
                            Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans)

                            strBankChargesAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankChargesAcc, BankLocation, True, trans)
                            Dim acc13() As String = {strBankChargesAcc, 1 * clsCommon.myCdbl(dt.Rows(0)("BankCharges")), "", "", "", "", "", "", "B"}
                            arrylst.Add(acc13)

                        End If
                        ''----------------
                    Else
                        Throw New Exception("Please fill transfer clearing account in bank master for bank - " & clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) & ".")
                    End If
                End If
                '' When Branch Accounting is ON (WITHDRAWAL CASE)
                If isApplyBrachAccounting = True AndAlso Not (clsCommon.CompairString(strFromSeg, strToSeg) = CompairStringResult.Equal) Then
                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strFromSeg, strToSeg, trans)
                    If clsCommon.myLen(strTemp) <= 0 Then
                        Throw New Exception("Please set Branch account mapping with from location " + strFromSeg + " and to location " + strToSeg)
                    End If
                    Dim BranchAccDR = New String() {strTemp, clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))}
                    arrylst.Add(BranchAccDR) '' DEBIT
                    Dim BranchAccCR() As String = {strFromBankAC, -1 * (clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount")) + clsCommon.myCdbl(dt.Rows(0)("BankCharges"))), "", "", "", "", "", "", "B"}
                    arrylst.Add(BranchAccCR) '' CREDIT

                    ''richa BHA/13/09/18-000545 
                    If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dt.Rows(0)("BankCharges")) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))) <= 0 Then
                        Throw New Exception("Please enter credit account for bank " + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")))
                    End If

                    If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dt.Rows(0)("BankCharges")) > 0 Then
                        Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans)

                        strBankChargesAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankChargesAcc, BankLocation, True, trans)
                        Dim acc13() As String = {strBankChargesAcc, 1 * clsCommon.myCdbl(dt.Rows(0)("BankCharges")), "", "", "", "", "", "", "B"}
                        arrylst.Add(acc13)

                    End If
                    ''----------------
                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Transaction_Type")), "R") = CompairStringResult.Equal Then '' RECEIPT
                If isApplyBrachAccounting = False Or (isApplyBrachAccounting = True AndAlso (clsCommon.CompairString(strFromSeg, strToSeg) = CompairStringResult.Equal)) Then
                    Dim strTempAC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Transfer_Clearing_Account FROM TSPL_BANK_MASTER WHERE BANK_CODE ='" & clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) & "'", trans))
                    If clsCommon.myLen(strTempAC) > 0 Then
                        strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strToSeg, True, trans)
                        Dim acc3() As String = {strTempAC, -1 * clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))}
                        arrylst.Add(acc3) '' CREDIT 
                        strToBankAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strToBankAC, strToSeg, True, trans)
                        Dim acc4() As String = {strToBankAC, clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount")), "", "", "", "", "", "", "B"}
                        arrylst.Add(acc4) '' DEBIT
                    Else
                        Throw New Exception("Please fill transfer clearing account in bank master for bank - " & clsCommon.myCstr(dt.Rows(0)("To_Bank_Code")) & ".")
                    End If
                End If
                '' When Branch Accounting is ON (RECEIPT CASE)
                If isApplyBrachAccounting = True AndAlso Not (clsCommon.CompairString(strFromSeg, strToSeg) = CompairStringResult.Equal) Then
                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strToSeg, strFromSeg, trans)
                    If clsCommon.myLen(strTemp) <= 0 Then
                        Throw New Exception("Please set Branch account mapping with from location " + strFromSeg + " and to location " + strToSeg)
                    End If
                    Dim BranchAccCR = New String() {strTemp, -1 * clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))}
                    arrylst.Add(BranchAccCR) '' CREDIT
                    Dim BranchAccDR() As String = {strToBankAC, clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount")), "", "", "", "", "", "", "B"}
                    arrylst.Add(BranchAccDR) '' DEBIT
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Transaction_Type")), "B") = CompairStringResult.Equal Then '' BOTH
                UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
                ''Dim acc1() As String = {strFromBankAC, -1 * clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount")), "", "", "", "", "", "", "B"}

                ''richa BHA/13/09/18-000545
                Dim acc1() As String = {strFromBankAC, -1 * (clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount")) + clsCommon.myCdbl(dt.Rows(0)("BankCharges"))), "", "", "", "", "", "", "B"}
                arrylst.Add(acc1)

                ''richa agarwal 28 Feb,2019 if branch account is off and segment of to bank is not same as From Bank then to Bank Seg code replaced with To Bank Seg Code
                If isApplyBrachAccounting = False Then
                    Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans)
                    strToBankAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strToBankAC, BankLocation, True, trans)
                End If

                Dim acc2() As String = {strToBankAC, clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount")), "", "", "", "", "", "", "B"}
                arrylst.Add(acc2)


                ''richa BHA/13/09/18-000545
                If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dt.Rows(0)("BankCharges")) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))) <= 0 Then
                    Throw New Exception("Please enter credit account for bank " + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")))
                End If

                If clsCommon.CompairString(FromBankTypeOfBank, "B") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(dt.Rows(0)("BankCharges")) > 0 Then
                    Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("From_Bank_Code")) + "'", trans)

                    strBankChargesAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankChargesAcc, BankLocation, True, trans)
                    Dim acc13() As String = {strBankChargesAcc, clsCommon.myCdbl(dt.Rows(0)("BankCharges")), "", "", "", "", "", "", "B"}
                    arrylst.Add(acc13)

                End If
                ''-------------------------end of bank charges
                ''richa BHA/13/09/18-000545
                If Not clsCommon.CompairString(strFromSeg, strToSeg) = CompairStringResult.Equal And isApplyBrachAccounting = True Then

                    '' When Branch Accounting is ON (BOTH CASE) richa agarwal SWA/03/12/18-000061
                    Dim strTempAC As String = String.Empty
                    If isApplyBrachAccounting = True Then
                        strTempAC = ClsBranchAccountMapping.GetBranchAccount(strToSeg, strFromSeg, trans)
                        If clsCommon.myLen(strTempAC) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + strFromSeg + " and to location " + strToSeg)
                        End If
                    Else
                        strTempAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP(1) Transfer_Clearing FROM TSPL_PURCHASE_ACCOUNTS", trans))
                        If clsCommon.myLen(strTempAC) <= 0 Then
                            Throw New Exception("Please set Transfer_Clearing")
                        End If
                    End If
                    ''------------------------

                    If clsCommon.myLen(strTempAC) > 0 Then

                        strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strFromSeg, True, trans)
                        Dim acc3() As String = {strTempAC, clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))}
                        arrylst.Add(acc3)

                        strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strToSeg, True, trans)
                        Dim acc4() As String = {strTempAC, -1 * (clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount")) + clsCommon.myCdbl(dt.Rows(0)("BankCharges")))}
                        arrylst.Add(acc4)

                        strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strToSeg, True, trans)
                        Dim acc5() As String = {strTempAC, 1 * clsCommon.myCdbl(dt.Rows(0)("BankCharges"))}
                        arrylst.Add(acc5)

                    Else
                        clsCommon.MyMessageBoxShow("Transfer Clearing account not found.")
                        Return False
                    End If
                End If
            End If
        End If
        clsJournalMaster.FunGrnlEntryWithTrans(strFromSeg, True, strJENo, trans, clsCommon.myCDate(dt.Rows(0)("Transfer_Posting_Date")), clsCommon.myCstr(dt.Rows(0)("Description")), "BK-TF", "Bank Transfer", strDocno, clsCommon.myCstr(dt.Rows(0)("Description")), "o", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrylst)
        Return True
    End Function


    Public Shared Function GetBankCodeByBankName(ByVal strName As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select BANK_CODE from TSPL_BANK_MASTER where DESCRIPTION = '" + strName + "' "
        Dim StrCode As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return StrCode
    End Function

End Class


Public Class clsBankReverse
    '''' This Function Is For Posting Bank Transaction i.e. (Reverse Transaction)

    Public Shared Function ReverseTDSCheck(ByVal strDocno As String, ByVal dtNew As DataTable, ByVal trans As SqlTransaction) As DataTable
        Try
            'DONE BY STUTI ON 10/11/2016
            If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                Dim tdsamt As Double = 0
                Dim dtaccount As DataTable = New DataTable()
                Dim query As String = "(select LEFT(TSPL_TDS_DEDUCTION_HEAD.Gl_Account,LEN(TSPL_TDS_DEDUCTION_HEAD.Gl_Account)-4) as TDSAccount," & _
                                       " LEFT(TSPL_VENDOR_ACCOUNT_SET.Payable_Account,LEN(TSPL_VENDOR_ACCOUNT_SET.Payable_Account)-4) as VENDORAccount1,LEFT(TSPL_PAYMENT_HEADER.Debit_Account,LEN(TSPL_PAYMENT_HEADER.Debit_Account)-4) as VENDORAccount  from TSPL_VENDOR_MASTER " & _
                                       " LEFT OUTER JOIN TSPL_BANK_REVERSE on TSPL_BANK_REVERSE.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code" & _
                                       " Left OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No " & _
                                       " LEFT OUTER JOIN TSPL_REMITTANCE ON TSPL_REMITTANCE.Document_No =TSPL_PAYMENT_HEADER.Payment_No " & _
                                       " LEFT OUTER JOIN TSPL_TDS_DEDUCTION_HEAD ON TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_REMITTANCE.Deduction_Code" & _
                                       " LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  = TSPL_VENDOR_MASTER.Vendor_Account" & _
                                       " where TSPL_BANK_REVERSE.Reverse_Code='" + strDocno + "' )"
                dtaccount = clsDBFuncationality.GetDataTable(query, trans)
                If dtaccount IsNot Nothing AndAlso dtaccount.Rows.Count > 0 Then
                    For i As Integer = dtNew.Rows.Count - 1 To 0 Step -1
                        If clsCommon.myCstr(dtNew.Rows(i)("Account_code")).Contains(dtaccount.Rows(0)("TDSAccount")) Then
                            tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                            dtNew.Rows.RemoveAt(i)
                        End If
                    Next

                    For i As Integer = 0 To dtNew.Rows.Count - 1
                        If clsCommon.myCstr(dtNew.Rows(i)("Account_code")).Contains(dtaccount.Rows(0)("VENDORAccount")) Then
                            dtNew.Rows(i)("Amount") = clsCommon.myCdbl(dtNew.Rows(i)("Amount")) + tdsamt
                            Exit For
                        End If
                    Next
                End If
            End If
            '----------------end here----------------

            Return dtNew
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function ReverseTaxCheckForreceipt(ByVal strDocno As String, ByVal dtNew As DataTable, ByVal trans As SqlTransaction) As DataTable
        Try
            'DONE BY STUTI ON 10/11/2016
            If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                Dim tdsamt As Double = 0
                Dim dtaccount As DataTable = New DataTable()
                Dim query As String = "Select isnull(TaxMaster1.PayableControl,'') as  PayableControl1,isnull(TaxMaster1 .DepositControl ,'') as DepositControl1,isnull(TaxMaster2.PayableControl,'') as  PayableControl2,isnull(TaxMaster2 .DepositControl ,'') as DepositControl2," & _
                " isnull(TaxMaster3.PayableControl,'') as  PayableControl3,isnull(TaxMaster3 .DepositControl ,'') as DepositControl3,isnull(TaxMaster4.PayableControl,'') as  PayableControl4,isnull(TaxMaster4 .DepositControl ,'') as DepositControl4," & _
                " isnull(TaxMaster5.PayableControl,'') as  PayableControl5,isnull(TaxMaster5 .DepositControl ,'') as DepositControl5,isnull(TaxMaster6.PayableControl,'') as  PayableControl6,isnull(TaxMaster6 .DepositControl ,'') as DepositControl6," & _
                " isnull(TaxMaster7.PayableControl,'') as  PayableControl7,isnull(TaxMaster7 .DepositControl ,'') as DepositControl7,isnull(TaxMaster8.PayableControl,'') as  PayableControl8,isnull(TaxMaster8 .DepositControl ,'') as DepositControl8," & _
                " isnull(TaxMaster9.PayableControl,'') as  PayableControl9,isnull(TaxMaster9 .DepositControl ,'') as DepositControl9,isnull(TaxMaster10.PayableControl,'') as  PayableControl10,isnull(TaxMaster10 .DepositControl ,'') as DepositControl10" & _
                " from TSPL_RECEIPT_HEADER " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster1 on TaxMaster1.Tax_Code =TSPL_RECEIPT_HEADER .TAX1 LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster2 on TaxMaster2.Tax_Code =TSPL_RECEIPT_HEADER .TAX2 " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster3 on TaxMaster3.Tax_Code =TSPL_RECEIPT_HEADER .TAX3 LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster4 on TaxMaster4.Tax_Code =TSPL_RECEIPT_HEADER .TAX4  " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster5 on TaxMaster5.Tax_Code =TSPL_RECEIPT_HEADER .TAX5 LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster6 on TaxMaster6.Tax_Code =TSPL_RECEIPT_HEADER .TAX6 " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster7 on TaxMaster7.Tax_Code =TSPL_RECEIPT_HEADER .TAX7  LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster8 on TaxMaster8.Tax_Code =TSPL_RECEIPT_HEADER .TAX8  " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster9 on TaxMaster9.Tax_Code =TSPL_RECEIPT_HEADER .TAX9  LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster10 on TaxMaster10.Tax_Code =TSPL_RECEIPT_HEADER .TAX10  " & _
                " left outer join TSPL_BANK_REVERSE on TSPL_BANK_REVERSE.Document_No =TSPL_RECEIPT_HEADER.Receipt_No  " & _
                "where TSPL_BANK_REVERSE.Reverse_Document ='Receipts' and TSPL_BANK_REVERSE.Reverse_Code  ='" + strDocno + "' "

                dtaccount = clsDBFuncationality.GetDataTable(query, trans)
                If dtaccount IsNot Nothing AndAlso dtaccount.Rows.Count > 0 Then
                    Dim PayableControl1 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl1"))
                    If (PayableControl1.Length > 0) Then
                        PayableControl1 = PayableControl1.Substring(0, PayableControl1.Length - 4)
                    End If

                    Dim PayableControl2 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl2"))
                    If (PayableControl2.Length > 0) Then
                        PayableControl2 = PayableControl2.Substring(0, PayableControl2.Length - 4)
                    End If

                    Dim PayableControl3 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl3"))
                    If (PayableControl3.Length > 0) Then
                        PayableControl3 = PayableControl3.Substring(0, PayableControl3.Length - 4)
                    End If

                    Dim PayableControl4 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl4"))
                    If (PayableControl4.Length > 0) Then
                        PayableControl4 = PayableControl4.Substring(0, PayableControl4.Length - 4)
                    End If

                    Dim PayableControl5 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl5"))
                    If (PayableControl5.Length > 0) Then
                        PayableControl5 = PayableControl5.Substring(0, PayableControl5.Length - 4)
                    End If

                    Dim PayableControl6 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl6"))
                    If (PayableControl6.Length > 0) Then
                        PayableControl6 = PayableControl6.Substring(0, PayableControl6.Length - 4)
                    End If

                    Dim PayableControl7 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl7"))
                    If (PayableControl7.Length > 0) Then
                        PayableControl7 = PayableControl7.Substring(0, PayableControl7.Length - 4)
                    End If

                    Dim PayableControl8 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl8"))
                    If (PayableControl8.Length > 0) Then
                        PayableControl8 = PayableControl8.Substring(0, PayableControl8.Length - 4)
                    End If

                    Dim PayableControl9 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl9"))
                    If (PayableControl9.Length > 0) Then
                        PayableControl9 = PayableControl9.Substring(0, PayableControl9.Length - 4)
                    End If

                    Dim PayableControl10 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl10"))
                    If (PayableControl10.Length > 0) Then
                        PayableControl10 = PayableControl10.Substring(0, PayableControl10.Length - 4)
                    End If

                    Dim DepositControl1 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl1"))
                    If (DepositControl1.Length > 0) Then
                        DepositControl1 = DepositControl1.Substring(0, DepositControl1.Length - 4)
                    End If

                    Dim DepositControl2 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl2"))
                    If (DepositControl2.Length > 0) Then
                        DepositControl2 = DepositControl2.Substring(0, DepositControl2.Length - 4)
                    End If

                    Dim DepositControl3 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl3"))
                    If (DepositControl3.Length > 0) Then
                        DepositControl3 = DepositControl3.Substring(0, DepositControl3.Length - 4)
                    End If

                    Dim DepositControl4 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl4"))
                    If (DepositControl4.Length > 0) Then
                        DepositControl4 = DepositControl4.Substring(0, DepositControl4.Length - 4)
                    End If

                    Dim DepositControl5 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl5"))
                    If (DepositControl5.Length > 0) Then
                        DepositControl5 = DepositControl5.Substring(0, DepositControl5.Length - 4)
                    End If

                    Dim DepositControl6 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl6"))
                    If (DepositControl6.Length > 0) Then
                        DepositControl6 = DepositControl6.Substring(0, DepositControl6.Length - 4)
                    End If

                    Dim DepositControl7 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl7"))
                    If (DepositControl7.Length > 0) Then
                        DepositControl7 = DepositControl7.Substring(0, DepositControl7.Length - 4)
                    End If

                    Dim DepositControl8 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl8"))
                    If (DepositControl8.Length > 0) Then
                        DepositControl8 = DepositControl8.Substring(0, DepositControl8.Length - 4)
                    End If

                    Dim DepositControl9 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl9"))
                    If (DepositControl9.Length > 0) Then
                        DepositControl9 = DepositControl9.Substring(0, DepositControl9.Length - 4)
                    End If

                    Dim DepositControl10 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl10"))
                    If (DepositControl10.Length > 0) Then
                        DepositControl10 = DepositControl10.Substring(0, DepositControl10.Length - 4)
                    End If


                    For i As Integer = dtNew.Rows.Count - 1 To 0 Step -1
                        Dim strAccount_code As String = clsCommon.myCstr(dtNew.Rows(i)("Account_code"))
                        If clsCommon.myLen(PayableControl1) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl1)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(PayableControl2) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl2)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl3) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl3)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl4) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl4)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl5) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl5)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl6) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl6)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl7) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl7)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl8) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl8)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl9) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl9)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl10) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl10)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If


                        If clsCommon.myLen(DepositControl1) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl1)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(DepositControl2) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl2)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(DepositControl3) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl3)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(DepositControl4) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl4)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl5) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl5)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(DepositControl6) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl6)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl7) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl7)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl8) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl8)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl9) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl9)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl10) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl10)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                    Next

                End If
            End If
            '----------------end here----------------

            Return dtNew
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function ReverseTaxCheck(ByVal strDocno As String, ByVal dtNew As DataTable, ByVal trans As SqlTransaction) As DataTable
        Try
            'DONE BY STUTI ON 10/11/2016
            If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                Dim tdsamt As Double = 0
                Dim dtaccount As DataTable = New DataTable()
                Dim query As String = "Select isnull(TaxMaster1.PayableControl,'') as  PayableControl1,isnull(TaxMaster1 .DepositControl ,'') as DepositControl1,isnull(TaxMaster2.PayableControl,'') as  PayableControl2,isnull(TaxMaster2 .DepositControl ,'') as DepositControl2, " & _
                " isnull(TaxMaster3.PayableControl,'') as  PayableControl3,isnull(TaxMaster3 .DepositControl ,'') as DepositControl3,isnull(TaxMaster4.PayableControl,'') as  PayableControl4,isnull(TaxMaster4 .DepositControl ,'') as DepositControl4,isnull(TaxMaster5.PayableControl,'') as  PayableControl5,isnull(TaxMaster5 .DepositControl ,'') as DepositControl5, " & _
                " isnull(TaxMaster6.PayableControl,'') as  PayableControl6,isnull(TaxMaster6 .DepositControl ,'') as DepositControl6, isnull(TaxMaster7.PayableControl,'') as  PayableControl7,isnull(TaxMaster7 .DepositControl ,'') as DepositControl7, isnull(TaxMaster8.PayableControl,'') as  PayableControl8,isnull(TaxMaster8 .DepositControl ,'') as DepositControl8, " & _
                " isnull(TaxMaster9.PayableControl,'') as  PayableControl9,isnull(TaxMaster9 .DepositControl ,'') as DepositControl9, isnull(TaxMaster10.PayableControl,'') as  PayableControl10,isnull(TaxMaster10 .DepositControl ,'') as DepositControl10 from TSPL_PAYMENT_HEADER " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster1 on TaxMaster1.Tax_Code =TSPL_PAYMENT_HEADER .TAX1 LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster2 on TaxMaster2.Tax_Code =TSPL_PAYMENT_HEADER .TAX2 " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster3 on TaxMaster3.Tax_Code =TSPL_PAYMENT_HEADER .TAX3 LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster4 on TaxMaster4.Tax_Code =TSPL_PAYMENT_HEADER .TAX4  " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster5 on TaxMaster5.Tax_Code =TSPL_PAYMENT_HEADER .TAX5 LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster6 on TaxMaster6.Tax_Code =TSPL_PAYMENT_HEADER .TAX6 " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster7 on TaxMaster7.Tax_Code =TSPL_PAYMENT_HEADER .TAX7  LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster8 on TaxMaster8.Tax_Code =TSPL_PAYMENT_HEADER .TAX8  " & _
                " LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster9 on TaxMaster9.Tax_Code =TSPL_PAYMENT_HEADER .TAX9 LEFT OUTER JOIN TSPL_TAX_MASTER TaxMaster10 on TaxMaster10.Tax_Code =TSPL_PAYMENT_HEADER .TAX10  " & _
                " left outer join TSPL_BANK_REVERSE on TSPL_BANK_REVERSE.Document_No =TSPL_PAYMENT_HEADER.Payment_No " & _
                " where TSPL_BANK_REVERSE.Reverse_Document ='Payments' and TSPL_BANK_REVERSE.Reverse_Code  ='" + strDocno + "' "
                dtaccount = clsDBFuncationality.GetDataTable(query, trans)
                If dtaccount IsNot Nothing AndAlso dtaccount.Rows.Count > 0 Then
                    Dim PayableControl1 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl1"))
                    If (PayableControl1.Length > 0) Then
                        PayableControl1 = PayableControl1.Substring(0, PayableControl1.Length - 4)
                    End If

                    Dim PayableControl2 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl2"))
                    If (PayableControl2.Length > 0) Then
                        PayableControl2 = PayableControl2.Substring(0, PayableControl2.Length - 4)
                    End If

                    Dim PayableControl3 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl3"))
                    If (PayableControl3.Length > 0) Then
                        PayableControl3 = PayableControl3.Substring(0, PayableControl3.Length - 4)
                    End If

                    Dim PayableControl4 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl4"))
                    If (PayableControl4.Length > 0) Then
                        PayableControl4 = PayableControl4.Substring(0, PayableControl4.Length - 4)
                    End If

                    Dim PayableControl5 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl5"))
                    If (PayableControl5.Length > 0) Then
                        PayableControl5 = PayableControl5.Substring(0, PayableControl5.Length - 4)
                    End If

                    Dim PayableControl6 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl6"))
                    If (PayableControl6.Length > 0) Then
                        PayableControl6 = PayableControl6.Substring(0, PayableControl6.Length - 4)
                    End If

                    Dim PayableControl7 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl7"))
                    If (PayableControl7.Length > 0) Then
                        PayableControl7 = PayableControl7.Substring(0, PayableControl7.Length - 4)
                    End If

                    Dim PayableControl8 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl8"))
                    If (PayableControl8.Length > 0) Then
                        PayableControl8 = PayableControl8.Substring(0, PayableControl8.Length - 4)
                    End If

                    Dim PayableControl9 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl9"))
                    If (PayableControl9.Length > 0) Then
                        PayableControl9 = PayableControl9.Substring(0, PayableControl9.Length - 4)
                    End If

                    Dim PayableControl10 As String = clsCommon.myCstr(dtaccount.Rows(0)("PayableControl10"))
                    If (PayableControl10.Length > 0) Then
                        PayableControl10 = PayableControl10.Substring(0, PayableControl10.Length - 4)
                    End If

                    Dim DepositControl1 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl1"))
                    If (DepositControl1.Length > 0) Then
                        DepositControl1 = DepositControl1.Substring(0, DepositControl1.Length - 4)
                    End If

                    Dim DepositControl2 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl2"))
                    If (DepositControl2.Length > 0) Then
                        DepositControl2 = DepositControl2.Substring(0, DepositControl2.Length - 4)
                    End If

                    Dim DepositControl3 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl3"))
                    If (DepositControl3.Length > 0) Then
                        DepositControl3 = DepositControl3.Substring(0, DepositControl3.Length - 4)
                    End If

                    Dim DepositControl4 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl4"))
                    If (DepositControl4.Length > 0) Then
                        DepositControl4 = DepositControl4.Substring(0, DepositControl4.Length - 4)
                    End If

                    Dim DepositControl5 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl5"))
                    If (DepositControl5.Length > 0) Then
                        DepositControl5 = DepositControl5.Substring(0, DepositControl5.Length - 4)
                    End If

                    Dim DepositControl6 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl6"))
                    If (DepositControl6.Length > 0) Then
                        DepositControl6 = DepositControl6.Substring(0, DepositControl6.Length - 4)
                    End If

                    Dim DepositControl7 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl7"))
                    If (DepositControl7.Length > 0) Then
                        DepositControl7 = DepositControl7.Substring(0, DepositControl7.Length - 4)
                    End If

                    Dim DepositControl8 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl8"))
                    If (DepositControl8.Length > 0) Then
                        DepositControl8 = DepositControl8.Substring(0, DepositControl8.Length - 4)
                    End If

                    Dim DepositControl9 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl9"))
                    If (DepositControl9.Length > 0) Then
                        DepositControl9 = DepositControl9.Substring(0, DepositControl9.Length - 4)
                    End If

                    Dim DepositControl10 As String = clsCommon.myCstr(dtaccount.Rows(0)("DepositControl10"))
                    If (DepositControl10.Length > 0) Then
                        DepositControl10 = DepositControl10.Substring(0, DepositControl10.Length - 4)
                    End If


                    For i As Integer = dtNew.Rows.Count - 1 To 0 Step -1
                        Dim strAccount_code As String = clsCommon.myCstr(dtNew.Rows(i)("Account_code"))
                        If clsCommon.myLen(PayableControl1) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl1)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(PayableControl2) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl2)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl3) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl3)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl4) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl4)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl5) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl5)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl6) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl6)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl7) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl7)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl8) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl8)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl9) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl9)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(PayableControl10) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(PayableControl10)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If


                        If clsCommon.myLen(DepositControl1) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl1)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(DepositControl2) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl2)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(DepositControl3) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl3)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(DepositControl4) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl4)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl5) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl5)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                        If clsCommon.myLen(DepositControl6) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl6)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl7) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl7)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl8) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl8)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl9) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl9)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If
                        If clsCommon.myLen(DepositControl10) > 0 Then
                            If (clsCommon.myCstr(strAccount_code).Contains(DepositControl10)) Then
                                tdsamt += clsCommon.myCdbl(dtNew.Rows(i)("Amount"))
                                dtNew.Rows.RemoveAt(i)
                            End If
                        End If

                    Next

                End If
            End If
            '----------------end here----------------

            Return dtNew
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function PostData(ByVal strDocno As String, Optional ByVal tdsreverse As Boolean = True, Optional ByVal taxreverse As Boolean = True) As Boolean
        'Dim dtNew As DataTable
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocno) <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            Dim strqry As String = "select Reverse_Code, Document_No, Reversal_Date,Bank_Code, Back_Acc_No, Post, Source_Type ,amount,Vendor_Code,Vendor_Name,Cust_Code,Cust_Name from TSPL_BANK_REVERSE where Reverse_Code='" + strDocno + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            If clsCommon.CompairString("p", clsCommon.myCstr(dt.Rows(0)("Post"))) = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Document")
            End If
            Dim strSourceType As String = "O"
            If clsCommon.myLen(dt.Rows(0)("Vendor_Code")) > 0 Then
                strSourceType = "V"
            ElseIf clsCommon.myLen(dt.Rows(0)("Cust_Code")) > 0 Then
                strSourceType = "C"
            End If

            If clsCommon.myCstr(dt.Rows(0)("Source_Type")) = "AP" Then
                Dim qry As String = "select Document_No,SNo from TSPL_REVALUATION_DETAIL where Payment_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'"
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    Throw New Exception("Can't post because this document is used in Revaluation Entry." + Environment.NewLine + "Doc no : " + clsCommon.myCstr(dtTemp.Rows(0)("Document_No")) + " at Sno :" + clsCommon.myCstr(dtTemp.Rows(0)("SNo")))
                End If


                Dim InvoiceNo As String = clsDBFuncationality.getSingleValue("select    Payment_No   from TSPL_PAYMENT_HEADER  where Payment_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "' ", trans)
                Dim ch As String = ""
                If clsCommon.myCstr(dt.Rows(0)("Document_No")) <> "" Then
                    ch = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_Type from TSPL_PAYMENT_HEADER where Payment_No = '" + InvoiceNo + "'", trans))
                End If
                Dim strdocument As String = ""
                If ch = "PY" Then
                    strdocument = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No  from TSPL_PAYMENT_DETAIL where Payment_No = '" + InvoiceNo + "'", trans))
                End If

                Dim dt1 As DataTable = Nothing
                Dim Doc_No As String = Nothing
                Dim AppliedAmt As Decimal = 0
                Dim BalAmt As Decimal = 0
                qry = "select Document_No ,Applied_Amount  from TSPL_PAYMENT_DETAIL where Payment_No ='" + InvoiceNo + "'"
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                For Each dr As DataRow In dt1.Rows
                    Doc_No = dr("Document_No").ToString()
                    AppliedAmt = CDec(dr("Applied_Amount").ToString())
                    BalAmt = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Balance_Amt  from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" + Doc_No + "' ")) + AppliedAmt
                    qry = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt ='" + BalAmt.ToString() + "' where Document_No ='" + Doc_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next

                '' to update balance amount against debit note which is applied with apply document on header
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Document_Type ,'') from TSPL_VENDOR_INVOICE_HEAD WHERE DOCUMENT_NO IN (SELECT APPLIED_PAYMENT FROM TSPL_PAYMENT_HEADER WHERE Payment_No ='" + InvoiceNo + "' AND Payment_Type ='AD')", trans)), "D") = CompairStringResult.Equal Then
                    Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT APPLIED_PAYMENT FROM TSPL_PAYMENT_HEADER WHERE Payment_No ='" + InvoiceNo + "' AND Payment_Type ='AD'", trans))
                    AppliedAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select AMOUNT from TSPL_BANK_REVERSE WHERE Reverse_Code ='" & strDocno & "'", trans))
                    BalAmt = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Balance_Amt  from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" + Doc_No + "' ")) + AppliedAmt
                    qry = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt ='" + BalAmt.ToString() + "' where Document_No ='" + Doc_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                ''-------------------------

                '' Journal entry start here
                CreateJEPayment(strDocno, "", tdsreverse, taxreverse, trans)
                ''Dim payment As String = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                ''Dim qry1 As String = "select m.Voucher_No,m.Source_Doc_No,d.Account_code,d.Amount,m.Segment_code,d.Hirerachy_Code,d.Cost_Centre_Code from TSPL_JOURNAL_MASTER m right outer join " & _
                ''                               "TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No where m.Source_Doc_No in " & _
                ''                               "(select Payment_No from TSPL_PAYMENT_HEADER where Payment_No = '" + InvoiceNo + "')"
                ''Dim arrglentry1 As New ArrayList()
                ''dtNew = New DataTable()
                ''dtNew = clsDBFuncationality.GetDataTable(qry1, trans)
                ' ''DONE BY STUTI ON 10/11/2016
                ''If Not tdsreverse Then
                ''    Dim dtR As New DataTable()
                ''    dtR = ReverseTDSCheck(strDocno, dtNew, trans)
                ''    If dtR IsNot Nothing AndAlso dtR.Rows.Count > 0 Then
                ''        dtNew = New DataTable()
                ''        dtNew = dtR
                ''    End If
                ''End If
                ' ''----------------end here----------------
                '' '' "", "", objTR.Hirerachy_Code, objTR.Cost_Centre_Code
                ''If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                ''    For i As Integer = 0 To dtNew.Rows.Count - 1
                ''        Dim value As Decimal = CDec(dtNew.Rows(i)(3))
                ''        If value > 0 Then
                ''            Dim account As String = dtNew.Rows(i)(2).ToString()
                ''            Dim amount As Decimal = -1 * (dtNew.Rows(i)(3).ToString())
                ''            Dim Hirerachy_Code As String = dtNew.Rows(i)("Hirerachy_Code").ToString()
                ''            Dim Cost_Centre_Code As String = dtNew.Rows(i)("Cost_Centre_Code").ToString()
                ''            Dim accdr() As String = {account, amount, "", "", Hirerachy_Code, Cost_Centre_Code}
                ''            arrglentry1.Add(accdr)
                ''        Else
                ''            Dim account As String = dtNew.Rows(i)(2).ToString()
                ''            Dim amount As Decimal = -1 * (dtNew.Rows(i)(3).ToString())
                ''            Dim Hirerachy_Code As String = dtNew.Rows(i)("Hirerachy_Code").ToString()
                ''            Dim Cost_Centre_Code As String = dtNew.Rows(i)("Cost_Centre_Code").ToString()
                ''            Dim acccr() As String = {account, amount, "", "", Hirerachy_Code, Cost_Centre_Code}
                ''            arrglentry1.Add(acccr)
                ''        End If
                ''    Next
                ''End If

                ''Dim Seg_Bank As String = ""
                ''Dim strdesc1 As String = "REVERSE " + clsCommon.myCstr(dt.Rows(0)("Document_No"))
                ' ''=================update by preeti gupta[error below in code  "There is no row at position 0"]
                ''If dtNew.Rows.Count > 0 Then
                ''    If clsCommon.myLen(clsCommon.myCstr(dtNew.Rows(0)("Segment_code"))) <= 0 Then
                ''        Seg_Bank = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans)
                ''    Else
                ''        Seg_Bank = clsCommon.myCstr(dtNew.Rows(0)("Segment_code"))
                ''    End If
                ''End If

                '' ''shivani 11 march 2016 show reverse reason with cheque no on journal entry against ticket no [BM00000008970]
                ''Dim strremarksforje As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Reason,'') + case when isnull(Cheque_No,'')<>'' then ' Against Cheque No. '+Cheque_No else ''  end    Cheque_No ,* from TSPL_BANK_REVERSE where reverse_code='" & clsCommon.myCstr(dt.Rows(0)("Reverse_Code")) & "'", trans))
                '' ''--------------

                ''clsJournalMaster.FunGrnlEntryWithTrans(Seg_Bank, True, trans, clsCommon.myCDate(dt.Rows(0)("Reversal_Date")), strdesc1, "RV-TA", "Bank Reverse", clsCommon.myCstr(dt.Rows(0)("Reverse_Code")), clsCommon.myCstr(dt.Rows(0)("Back_Acc_No")), strSourceType, clsCommon.myCstr(dt.Rows(0)("Vendor_Code")), clsCommon.myCstr(dt.Rows(0)("Vendor_Name")), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrglentry1, Nothing, strremarksforje)
                '' end journal enter
                Dim STR1 As String = "UPDATE TSPL_BANK_REVERSE SET POST = 'P' WHERE Reverse_Code = '" + clsCommon.myCstr(dt.Rows(0)("Reverse_Code")) + "'"
                clsDBFuncationality.ExecuteNonQuery(STR1, trans)
                '===========================Update By preeti gupta Against Ticket No[BM00000009021]
                Dim PaymentType As String = clsDBFuncationality.getSingleValue("select Payment_Type  from TSPL_PAYMENT_HEADER where Payment_No in (select Applied_Payment from TSPL_PAYMENT_HEADER where Payment_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "')", trans)
                If clsCommon.CompairString(PaymentType, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(PaymentType, "OA") = CompairStringResult.Equal Then
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Balance_Amt=Balance_Amt+('" + clsCommon.myCstr(dt.Rows(0)("amount")) + "') where Payment_No in (select Applied_Payment from TSPL_PAYMENT_HEADER where Payment_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "')", trans)
                End If
            Else
                Dim qry As String = "select Document_No,SNo from TSPL_REVALUATION_DETAIL where Receipt_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'"
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    Throw New Exception("Can't post because this document is used in Revaluation Entry." + Environment.NewLine + "Doc no : " + clsCommon.myCstr(dtTemp.Rows(0)("Document_No")) + " at Sno :" + clsCommon.myCstr(dtTemp.Rows(0)("SNo")))
                End If

                Dim InvoiceNo As String = clsDBFuncationality.getSingleValue("select    Receipt_No   from TSPL_RECEIPT_HEADER  where Receipt_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'", trans)
                Dim ch As String = ""
                If clsCommon.myCstr(dt.Rows(0)("Document_No")) <> "" Then
                    ch = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Receipt_Type from TSPL_RECEIPT_HEADER where Receipt_No = '" + InvoiceNo + "'", trans))
                End If
                Dim dtrc As DataTable = Nothing
                If ch = "R" Then
                    dtrc = clsDBFuncationality.GetDataTable("select Document_No  from TSPL_RECEIPT_DETAIL where Receipt_No = '" + InvoiceNo + "'", trans)
                End If
                ''---------for sale Invoice------------------------
                'If dtrc IsNot Nothing AndAlso dtrc.Rows.Count > 0 Then
                '    For ii As Integer = 0 To dtrc.Rows.Count - 1
                '        Dim strdocument1 As String = dtrc.Rows(ii)("Document_No")
                '        Dim chekexist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Applied_Amount  from TSPL_RECEIPT_DETAIL where  Receipt_No = '" + InvoiceNo + "' and Document_No='" + strdocument1 + "' ", trans))
                '        Dim str2 As String = "update TSPL_Customer_Invoice_Head set Balance_Amt = Balance_Amt+" + chekexist + " where Document_No = '" + strdocument1 + "'"
                '        clsDBFuncationality.ExecuteNonQuery(str2, trans)
                '    Next
                'End If
                ''------------------------------------------------
                ''richa agarwal to update balance of sale invoice
                Dim dt1 As DataTable = Nothing
                Dim Doc_No As String = Nothing
                Dim AppliedAmt As Decimal = 0
                Dim BalAmt As Decimal = 0
                qry = "select Document_No ,Applied_Amount,Receipt_Type  from TSPL_RECEIPT_DETAIL where Receipt_No = '" + InvoiceNo + "'"
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                For Each dr As DataRow In dt1.Rows
                    Doc_No = dr("Document_No").ToString()
                    AppliedAmt = CDec(dr("Applied_Amount").ToString())
                    BalAmt = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Balance_Amt  from TSPL_Customer_Invoice_Head where Document_No ='" + Doc_No + "' ")) + AppliedAmt
                    qry = "update TSPL_Customer_Invoice_Head set Balance_Amt ='" + BalAmt.ToString() + "' where Document_No ='" + Doc_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    If clsCommon.CompairString(dr("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then
                        BalAmt = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Balance_Amt  from TSPL_RECEIPT_HEADER where Receipt_No ='" + Doc_No + "' ")) + AppliedAmt
                        qry = "update TSPL_RECEIPT_HEADER set Balance_Amt ='" + BalAmt.ToString() + "' where Receipt_No ='" + Doc_No + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next

                '' to update balance amount against credit note which is applied with apply document on header
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Document_Type ,'') from TSPL_Customer_Invoice_Head WHERE DOCUMENT_NO IN (SELECT Applied_Receipt from TSPL_RECEIPT_HEADER WHERE Receipt_No ='" + InvoiceNo + "' AND Receipt_Type ='A')", trans)), "C") = CompairStringResult.Equal Then
                    Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Applied_Receipt FROM TSPL_RECEIPT_HEADER WHERE Receipt_No ='" + InvoiceNo + "' AND Receipt_Type ='A'", trans))
                    AppliedAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select AMOUNT from TSPL_BANK_REVERSE WHERE Reverse_Code ='" & strDocno & "'", trans))
                    BalAmt = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Balance_Amt  from TSPL_Customer_Invoice_Head where Document_No ='" + Doc_No + "' ")) + AppliedAmt
                    qry = "update TSPL_Customer_Invoice_Head set Balance_Amt ='" + BalAmt.ToString() + "' where Document_No ='" + Doc_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                ''-------------------------


                ''------------------


                '' Start Create JE
                CreateJEReceipt(strDocno, "", tdsreverse, taxreverse, trans)

                'Dim receipt As String = clsCommon.myCstr(dt.Rows(0)("Document_No"))                
                'qry = "select m.Voucher_No,m.Source_Doc_No,d.Account_code,d.Amount,m.Segment_code,d.Hirerachy_Code,d.Cost_Centre_Code from TSPL_JOURNAL_MASTER m right outer join " & _
                '                               "TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No where m.Source_Doc_No in " & _
                '                               "(select Receipt_No from TSPL_RECEIPT_HEADER where  Receipt_No= '" + InvoiceNo + "')"
                'Dim arrglentry As New ArrayList()
                'dtNew = clsDBFuncationality.GetDataTable(qry, trans)

                ''DONE BY STUTI ON 10/11/2016
                'If Not tdsreverse Then
                '    Dim dtR As New DataTable()
                '    dtR = ReverseTDSCheck(strDocno, dtNew, trans)
                '    If dtR IsNot Nothing AndAlso dtR.Rows.Count > 0 Then
                '        dtNew = New DataTable()
                '        dtNew = dtR
                '    End If
                'End If
                ''----------------end here----------------

                'If dtNew.Rows.Count > 0 Then
                '    For i As Integer = 0 To dtNew.Rows.Count - 1
                '        'Revrese Entry of (Journal Entry of Payment/Receipt)
                '        Dim acccr() As String = {clsCommon.myCstr(dtNew.Rows(i)("Account_code")), clsCommon.myCdbl(dtNew.Rows(i)("Amount")) * -1, "", "", clsCommon.myCstr(dtNew.Rows(i)("Hirerachy_Code")), clsCommon.myCstr(dtNew.Rows(i)("Cost_Centre_Code"))}
                '        arrglentry.Add(acccr)
                '    Next
                'End If
                'Dim Seg_Bank As String = ""
                'Dim strdesc As String = "REVERSE" + clsCommon.myCstr(dt.Rows(0)("Document_No"))
                ' ''RICHA AGARWAL AGAINST TICKET NO BM00000008002
                'If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                '    If clsCommon.myLen(clsCommon.myCstr(dtNew.Rows(0)("Segment_code"))) <= 0 Then
                '        Seg_Bank = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans)
                '    Else
                '        Seg_Bank = clsCommon.myCstr(dtNew.Rows(0)("Segment_code"))
                '    End If
                'Else
                '    Seg_Bank = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans)
                'End If
                ' ''-----------------------

                ' ''shivani 11 march 2016 show reverse reason with cheque no on journal entry against ticket [BM00000008970]
                'Dim strremarksforje As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Reason,'') + case when isnull(Cheque_No,'')<>'' then ' Against Cheque No. '+Cheque_No else ''  end    Cheque_No ,* from TSPL_BANK_REVERSE where reverse_code='" & clsCommon.myCstr(dt.Rows(0)("Reverse_Code")) & "'", trans))
                ' ''--------------

                'clsJournalMaster.FunGrnlEntryWithTrans(Seg_Bank, True, trans, clsCommon.myCDate(dt.Rows(0)("Reversal_Date")), strdesc, "RV-TA", "Bank Reverse", clsCommon.myCstr(dt.Rows(0)("Reverse_Code")), clsCommon.myCstr(dt.Rows(0)("Back_Acc_No")), strSourceType, clsCommon.myCstr(dt.Rows(0)("Cust_Code")), clsCommon.myCstr(dt.Rows(0)("Cust_Name")), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrglentry, Nothing, strremarksforje)
                ' '' end Create JE
                Dim STR1 As String = "UPDATE TSPL_BANK_REVERSE SET POST = 'P' WHERE Reverse_Code = '" + clsCommon.myCstr(dt.Rows(0)("Reverse_Code")) + "'"
                clsDBFuncationality.ExecuteNonQuery(STR1, trans)

                Dim qrychk As String = "update TSPL_RECEIPT_HEADER set IsChkReverse='Y' where Receipt_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'"
                clsDBFuncationality.ExecuteNonQuery(qrychk, trans)

                ''to update chkreverse='Y' of unappiledentry
                qrychk = "update TSPL_RECEIPT_HEADER set IsChkReverse='Y'  where Receipt_No in (sELECT UnApplied_No from TSPL_RECEIPT_HEADER WHERE Receipt_No ='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "')"
                clsDBFuncationality.ExecuteNonQuery(qrychk, trans)
                ''----------

                '===========================Update By Balwinder on 09/06/2016
                Dim ReceiptType As String = clsDBFuncationality.getSingleValue("select Receipt_Type  from  TSPL_RECEIPT_HEADER where Receipt_No in (select innRH.Applied_Receipt from TSPL_RECEIPT_HEADER as innRH where innRH.Receipt_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "')", trans)
                If clsCommon.CompairString(ReceiptType, "O") = CompairStringResult.Equal Or clsCommon.CompairString(ReceiptType, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ReceiptType, "U") = CompairStringResult.Equal Then
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_RECEIPT_HEADER set Balance_Amt=Balance_Amt+('" + clsCommon.myCstr(dt.Rows(0)("amount")) + "') where  Receipt_No in (select innRH.Applied_Receipt from TSPL_RECEIPT_HEADER as innRH where innRH.Receipt_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "')", trans)
                End If
            End If
            clsBankReco.SetOutstandingEntry(strDocno, clsCommon.myCDate(dt.Rows(0)("Reversal_Date")), "Reverse", trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocno, "TSPL_BANK_REVERSE", "Reverse_Code", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CreateJEPayment(ByVal Reverse_Code As String, ByVal Voucher_No As String, ByVal tdsreverse As Boolean, ByVal taxreverse As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = " select Reverse_Code,Reversal_Date,Document_No,Bank_Code,Back_Acc_No,Source_Type,Reverse_Document,Vendor_Code,Vendor_Name,Cust_Code,Cust_Name " & _
                            " from TSPL_BANK_REVERSE  where Reverse_Code='" & Reverse_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count <= 0 Then
            Throw New Exception("Bank Reverse Doc No-" & Reverse_Code & " not found.")
        End If
        Dim Payment_No As String = clsCommon.myCstr(dt.Rows(0).Item("Document_No"))

        Dim qry1 As String = " select m.Voucher_No,m.Source_Doc_No,d.Account_code,d.Amount,m.Segment_code,d.Hirerachy_Code,d.Cost_Centre_Code,d.Reco_Control_Account from TSPL_JOURNAL_MASTER m " & _
                             " right outer join TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No where m.Source_Doc_No in " & _
                             " (select Payment_No from TSPL_PAYMENT_HEADER where Payment_No = '" & Payment_No & "')"
        Dim arrglentry1 As New ArrayList()
        Dim dtNew As DataTable = New DataTable()
        dtNew = clsDBFuncationality.GetDataTable(qry1, trans)
        'DONE BY STUTI ON 10/11/2016
        If Not tdsreverse Then
            Dim dtR As New DataTable()
            dtR = ReverseTDSCheck(Reverse_Code, dtNew, trans)
            If dtR IsNot Nothing AndAlso dtR.Rows.Count > 0 Then
                dtNew = New DataTable()
                dtNew = dtR
            End If
        End If
        '----------------end here----------------
        'DONE BY richa
        If Not taxreverse Then
            Dim dtR As New DataTable()
            dtR = ReverseTaxCheck(Reverse_Code, dtNew, trans)
            If dtR IsNot Nothing AndAlso dtR.Rows.Count > 0 Then
                dtNew = New DataTable()
                dtNew = dtR
            End If
        End If
        '----------------end here----------------

        '' "", "", objTR.Hirerachy_Code, objTR.Cost_Centre_Code
        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
            For i As Integer = 0 To dtNew.Rows.Count - 1
                Dim value As Decimal = CDec(dtNew.Rows(i)(3))
                If value > 0 Then
                    Dim account As String = dtNew.Rows(i)(2).ToString()
                    Dim amount As Decimal = -1 * (dtNew.Rows(i)(3).ToString())
                    Dim Hirerachy_Code As String = dtNew.Rows(i)("Hirerachy_Code").ToString()
                    Dim Cost_Centre_Code As String = dtNew.Rows(i)("Cost_Centre_Code").ToString()
                    Dim accdr() As String = {account, amount, "", "", Hirerachy_Code, Cost_Centre_Code, "", "", clsCommon.myCstr(dtNew.Rows(i)("Reco_Control_Account"))}
                    arrglentry1.Add(accdr)
                Else
                    Dim account As String = dtNew.Rows(i)(2).ToString()
                    Dim amount As Decimal = -1 * (dtNew.Rows(i)(3).ToString())
                    Dim Hirerachy_Code As String = dtNew.Rows(i)("Hirerachy_Code").ToString()
                    Dim Cost_Centre_Code As String = dtNew.Rows(i)("Cost_Centre_Code").ToString()
                    Dim acccr() As String = {account, amount, "", "", Hirerachy_Code, Cost_Centre_Code, "", "", clsCommon.myCstr(dtNew.Rows(i)("Reco_Control_Account"))}
                    arrglentry1.Add(acccr)
                End If
            Next
        End If

        Dim Seg_Bank As String = ""
        Dim strdesc1 As String = "REVERSE " + Payment_No
        '=================update by preeti gupta[error below in code  "There is no row at position 0"]
        If dtNew.Rows.Count > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(dtNew.Rows(0)("Segment_code"))) <= 0 Then
                Seg_Bank = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans)
            Else
                Seg_Bank = clsCommon.myCstr(dtNew.Rows(0)("Segment_code"))
            End If
        End If

        ''shivani 11 march 2016 show reverse reason with cheque no on journal entry against ticket no [BM00000008970]
        Dim strremarksforje As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Reason,'') + case when isnull(Cheque_No,'')<>'' then ' Against Cheque No. '+Cheque_No else ''  end    Cheque_No ,* from TSPL_BANK_REVERSE where reverse_code='" & clsCommon.myCstr(dt.Rows(0)("Reverse_Code")) & "'", trans))
        ''--------------
        Dim strSourceType As String = "V"
        'clsJournalMaster.FunGrnlEntryWithTrans(strPrefixTransType, "", obj.loc_code, True, isForUnpostedTransaction, strVoucherNo, trans, strPostDate, pjvNOVochdesc, "AP-DN", "AP Invoice", obj.Document_No, obj.Description, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew, Nothing, Nothing, Nothing, coll)
        If clsCommon.myLen(Voucher_No) <= 0 Then
            clsJournalMaster.FunGrnlEntryWithTrans(Seg_Bank, True, trans, clsCommon.myCDate(dt.Rows(0)("Reversal_Date")), strdesc1, "RV-TA", "Bank Reverse", clsCommon.myCstr(dt.Rows(0)("Reverse_Code")), clsCommon.myCstr(dt.Rows(0)("Back_Acc_No")), strSourceType, clsCommon.myCstr(dt.Rows(0)("Vendor_Code")), clsCommon.myCstr(dt.Rows(0)("Vendor_Name")), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrglentry1, Nothing, strremarksforje)
        Else
            clsJournalMaster.FunGrnlEntryWithTrans(Seg_Bank, True, Voucher_No, trans, clsCommon.myCDate(dt.Rows(0)("Reversal_Date")), strdesc1, "RV-TA", "Bank Reverse", clsCommon.myCstr(dt.Rows(0)("Reverse_Code")), clsCommon.myCstr(dt.Rows(0)("Back_Acc_No")), strSourceType, clsCommon.myCstr(dt.Rows(0)("Vendor_Code")), clsCommon.myCstr(dt.Rows(0)("Vendor_Name")), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrglentry1, Nothing, strremarksforje)
        End If

        Return True
    End Function
    Public Shared Function CreateJEReceipt(ByVal Reverse_Code As String, ByVal Voucher_No As String, ByVal tdsreverse As Boolean, ByVal taxreverse As Boolean, ByVal trans As SqlTransaction) As Boolean
        '' Start Create JE
        Dim qry As String = " select Reverse_Code,Reversal_Date,Document_No,Bank_Code,Back_Acc_No,Source_Type,Reverse_Document,Vendor_Code,Vendor_Name,Cust_Code,Cust_Name " & _
                           " from TSPL_BANK_REVERSE  where Reverse_Code='" & Reverse_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim receipt As String = clsCommon.myCstr(dt.Rows(0)("Document_No"))
        qry = "select m.Voucher_No,m.Source_Doc_No,d.Account_code,d.Amount,m.Segment_code,d.Hirerachy_Code,d.Cost_Centre_Code,d.Reco_Control_Account from TSPL_JOURNAL_MASTER m right outer join " & _
                                       "TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No where m.Source_Doc_No in " & _
                                       "(select Receipt_No from TSPL_RECEIPT_HEADER where  Receipt_No= '" + receipt + "')"
        Dim arrglentry As New ArrayList()
        Dim dtNew As DataTable = New DataTable()
        dtNew = clsDBFuncationality.GetDataTable(qry, trans)

        'DONE BY STUTI ON 10/11/2016
        If Not tdsreverse Then
            Dim dtR As New DataTable()
            dtR = ReverseTDSCheck(Reverse_Code, dtNew, trans)
            If dtR IsNot Nothing AndAlso dtR.Rows.Count > 0 Then
                dtNew = New DataTable()
                dtNew = dtR
            End If
        End If
        '----------------end here----------------

        'DONE BY richa
        If Not taxreverse Then
            Dim dtR As New DataTable()
            dtR = ReverseTaxCheckForreceipt(Reverse_Code, dtNew, trans)
            If dtR IsNot Nothing AndAlso dtR.Rows.Count > 0 Then
                dtNew = New DataTable()
                dtNew = dtR
            End If
        End If
        '----------------end here----------------

        If dtNew.Rows.Count > 0 Then
            For i As Integer = 0 To dtNew.Rows.Count - 1
                'Revrese Entry of (Journal Entry of Payment/Receipt)
                Dim acccr() As String = {clsCommon.myCstr(dtNew.Rows(i)("Account_code")), clsCommon.myCdbl(dtNew.Rows(i)("Amount")) * -1, "", "", clsCommon.myCstr(dtNew.Rows(i)("Hirerachy_Code")), clsCommon.myCstr(dtNew.Rows(i)("Cost_Centre_Code")), "", "", clsCommon.myCstr(dtNew.Rows(i)("Reco_Control_Account"))}
                arrglentry.Add(acccr)
            Next
        End If
        Dim Seg_Bank As String = ""
        Dim strdesc As String = "REVERSE" + clsCommon.myCstr(dt.Rows(0)("Document_No"))
        ''RICHA AGARWAL AGAINST TICKET NO BM00000008002
        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(dtNew.Rows(0)("Segment_code"))) <= 0 Then
                Seg_Bank = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans)
            Else
                Seg_Bank = clsCommon.myCstr(dtNew.Rows(0)("Segment_code"))
            End If
        Else
            Seg_Bank = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans)
        End If
        ''-----------------------

        ''shivani 11 march 2016 show reverse reason with cheque no on journal entry against ticket [BM00000008970]
        Dim strremarksforje As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Reason,'') + case when isnull(Cheque_No,'')<>'' then ' Against Cheque No. '+Cheque_No else ''  end    Cheque_No ,* from TSPL_BANK_REVERSE where reverse_code='" & clsCommon.myCstr(dt.Rows(0)("Reverse_Code")) & "'", trans))
        ''--------------
        Dim strSourceType As String = "C"
        If clsCommon.myLen(Voucher_No) <= 0 Then
            clsJournalMaster.FunGrnlEntryWithTrans(Seg_Bank, True, trans, clsCommon.myCDate(dt.Rows(0)("Reversal_Date")), strdesc, "RV-TA", "Bank Reverse", clsCommon.myCstr(dt.Rows(0)("Reverse_Code")), clsCommon.myCstr(dt.Rows(0)("Back_Acc_No")), strSourceType, clsCommon.myCstr(dt.Rows(0)("Cust_Code")), clsCommon.myCstr(dt.Rows(0)("Cust_Name")), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrglentry, Nothing, strremarksforje)
        Else
            clsJournalMaster.FunGrnlEntryWithTrans(Seg_Bank, True, Voucher_No, trans, clsCommon.myCDate(dt.Rows(0)("Reversal_Date")), strdesc, "RV-TA", "Bank Reverse", clsCommon.myCstr(dt.Rows(0)("Reverse_Code")), clsCommon.myCstr(dt.Rows(0)("Back_Acc_No")), strSourceType, clsCommon.myCstr(dt.Rows(0)("Cust_Code")), clsCommon.myCstr(dt.Rows(0)("Cust_Name")), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrglentry, Nothing, strremarksforje)
        End If
        Return True
        '' end Create JE
    End Function
    Public Shared Function CreateJE(ByVal Reverse_Code As String, ByVal Voucher_No As String, ByVal tdsreverse As Boolean, ByVal taxreverse As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select Source_Type from TSPL_BANK_REVERSE where Reverse_Code='" & Reverse_Code & "'"
        Try
            Dim Source_Type As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.CompairString(Source_Type, "AP") = CompairStringResult.Equal Then
                CreateJEPayment(Reverse_Code, Voucher_No, tdsreverse, taxreverse, trans)
            ElseIf clsCommon.CompairString(Source_Type, "AR") = CompairStringResult.Equal Then
                CreateJEReceipt(Reverse_Code, Voucher_No, tdsreverse, tdsreverse, trans)
            Else
                Throw New Exception("Invalid Source Type of Bank Reverse Doc-" & Reverse_Code & "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
