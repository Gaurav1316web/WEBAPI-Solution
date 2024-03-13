Imports common
Imports System.Data.SqlClient
''updation by richa agarwal 27/07/2015 against ticket no. BM00000007496
Public Class clsBankReco


#Region "Variable"
    Public Reconciliation_Id As String = Nothing
    Public Bank_Code As String = Nothing
    Public Bank_Name As String = Nothing
    Public Description As String = Nothing
    Public Post As Boolean = Nothing
    Public Bank_AccountNo As String = Nothing
    Public Statement_Date As DateTime
    Public Statement_Balance As Double = 0
    Public Reconciliation_Date As DateTime
    Public Deposit_OutstandingAmt As Double = 0
    Public Withdrawal_OutstandingAmt As Double = 0
    Public AdjustmentStatement_Balance As Double = 0
    Public AdjustmentBook_Balance As Double = 0
    Public Book_Balance As Double = 0
    Public OutOf_Balance As Double = 0
    Public TotalWithdrawal As Double = 0
    Public TotalDeposit As Double = 0
    Public Arr As List(Of clsBankRecoDetails) = Nothing
#End Region

    Public Shared Function funSave(ByVal obj As clsBankReco, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim qry1 As String = Nothing
        Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select TSPL_GL_ACCOUNTS.Account_Seg_Code7 from TSPL_BANK_MASTER LEFT OUTER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC Where BANK_CODE='" + obj.Bank_Code + "'")
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Common Services", "Bank Reco", LocSegmentCode, obj.Statement_Date, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Reconciliation_Id, "tspl_BankReco_Head", "Reconciliation_Id", "tspl_BankReco_Detail", "Reconciliation_Id", trans)
            End If

            Dim qry As String = "delete from tspl_BankReco_Detail    where Reconciliation_Id='" + obj.Reconciliation_Id + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                '-obj.Reconciliation_Id = fnAutoGenerateNo(trans)
                obj.Reconciliation_Id = clsERPFuncationality.GetNextCode(trans, obj.Reconciliation_Date, clsDocType.BankReco, clsDocTransactionType.Bank_Reco, LocSegmentCode, True)

                If (clsCommon.myLen(obj.Reconciliation_Id) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Bank_AccountNo", obj.Bank_AccountNo)
            clsCommon.AddColumnsForChange(coll, "Statement_Date", clsCommon.GetPrintDate(obj.Statement_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Statement_Balance", obj.Statement_Balance)
            clsCommon.AddColumnsForChange(coll, "Reconciliation_Date", clsCommon.GetPrintDate(obj.Reconciliation_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Deposit_OutstandingAmt", obj.Deposit_OutstandingAmt)
            clsCommon.AddColumnsForChange(coll, "Withdrawal_OutstandingAmt", obj.Withdrawal_OutstandingAmt)
            clsCommon.AddColumnsForChange(coll, "AdjustmentStatement_Balance", obj.AdjustmentStatement_Balance)
            clsCommon.AddColumnsForChange(coll, "AdjustmentBook_Balance", obj.AdjustmentBook_Balance)
            clsCommon.AddColumnsForChange(coll, "Book_Balance", obj.Book_Balance)
            clsCommon.AddColumnsForChange(coll, "TotalDeposit", obj.TotalDeposit)
            clsCommon.AddColumnsForChange(coll, "TotalWithdrawal", obj.TotalWithdrawal)
            clsCommon.AddColumnsForChange(coll, "OutOf_Balance", obj.OutOf_Balance)
            clsCommon.AddColumnsForChange(coll, "Post", "N")
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Reconciliation_Id", obj.Reconciliation_Id)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_BankReco_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_BankReco_Head", OMInsertOrUpdate.Update, "tspl_BankReco_Head.Reconciliation_Id='" + obj.Reconciliation_Id + "'", trans)
            End If
            isSaved = isSaved AndAlso clsBankRecoDetails.SaveData(obj.Reconciliation_Id, obj.Arr, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = Nothing
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Reconciliation Id  not found to Delete")
        End If
        Dim obj As clsBankReco = clsBankReco.GetData(strCode)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Reconciliation_Id) > 0) Then
            Try
                If (obj.Post = True) Then
                    Throw New Exception("Already Posted.")
                End If
                Dim Arr As List(Of clsBankRecoDetails) = obj.Arr
                If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                    For Each obj1 As clsBankRecoDetails In Arr
                        If obj1.Document_Type = "W" Then
                            qry = "update TSPL_PAYMENT_HEADER set IsRecoCleared='N' where Payment_No  ='" + obj1.Document_No + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj1.Document_No, "TSPL_PAYMENT_HEADER", "Payment_No", trans)
                        End If
                        If obj1.Document_Type = "D" Then
                            qry = "update TSPL_RECEIPT_HEADER set IsRecoCleared='N' where Receipt_No ='" + obj1.Document_No + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj1.Document_No, "TSPL_RECEIPT_HEADER", "Receipt_No", trans)
                        End If
                    Next
                End If

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "tspl_BankReco_Head", "Reconciliation_Id", "tspl_BankReco_Detail", "Reconciliation_Id", trans)

                qry = "delete from tspl_BankReco_Detail where Reconciliation_Id='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from tspl_BankReco_Head where Reconciliation_Id='" + strCode + "'"
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

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = True
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Reconciliation Id  not found to Delete")
        End If
        Dim obj As clsBankReco = clsBankReco.GetData(strCode)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim strBankAcc As String
        Dim strBankSubAcc As String
        Dim JournalEntryDoc As Double

        Dim BankIn As String
        Dim BankOut As String
        Dim TranBankInMainAcc As String
        Dim TranBankInSubAcc As String
        Dim TranBankOutMainAcc As String
        Dim TranBankOutSubAcc As String
        Dim UseSubAcc As String
        Dim strLocationSeg As String = ""

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Reconciliation_Id) > 0) Then
            Try
                If (obj.Post = True) Then
                    Throw New Exception("Already Posted.")
                End If
                If clsCommon.myCdbl(obj.OutOf_Balance) <> 0 Then
                    Throw New Exception("Outstanding Entry can not be posted.")
                End If
                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select TSPL_GL_ACCOUNTS.Account_Seg_Code7 from TSPL_BANK_MASTER LEFT OUTER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC Where BANK_CODE='" + obj.Bank_Code + "'", trans)
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Common Services", "Bank Reco", LocSegmentCode, obj.Statement_Date, trans)

                '' Anubhooti 04-Sep-2014 BM00000003437 (Created GL Entry Based on settings)
                UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
                If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal Then

                    strBankAcc = clsDBFuncationality.getSingleValue("select isnull(BANKACC,'') As BANKACC from TSPL_BANK_MASTER WHERE BANK_CODE='" + obj.Bank_Code + "'", trans)
                    strBankSubAcc = clsDBFuncationality.getSingleValue("select ISNULL(Sub_Account,'') As Sub_Account from TSPL_BANK_MASTER WHERE BANK_CODE='" + obj.Bank_Code + "'", trans)

                    ' Dim BankQry As String
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        Dim arrylst As ArrayList = New ArrayList()
                        For Each objTr As clsBankRecoDetails In obj.Arr
                            If clsCommon.CompairString(objTr.Reconciliation_Status, "C") = CompairStringResult.Equal Then

                                JournalEntryDoc = clsDBFuncationality.getSingleValue("Select Count(*) As Row From TSPL_JOURNAL_MASTER Where Source_Doc_No  ='" + strCode + "-" + objTr.Document_No + "'", trans)
                                If clsCommon.CompairString(objTr.Entry_Type, "P") = CompairStringResult.Equal AndAlso JournalEntryDoc = 0 Then
                                    Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + obj.Bank_Code + "'", trans)
                                    If clsCommon.myCstr(strLocationSeg) <= 0 Then
                                        strLocationSeg = BankLocation
                                    End If

                                    Dim acc1() As String = {strBankAcc, -1 * clsCommon.myCdbl(objTr.Cleared_Amount)}
                                    arrylst.Add(acc1)
                                    Dim Acc2() As String = {strBankSubAcc, clsCommon.myCdbl(objTr.Cleared_Amount)}
                                    arrylst.Add(Acc2)
                                    strBankSubAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankSubAcc, BankLocation, True, trans)
                                ElseIf clsCommon.CompairString(objTr.Entry_Type, "R") = CompairStringResult.Equal AndAlso JournalEntryDoc = 0 Then
                                    Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + obj.Bank_Code + "'", trans)
                                    If clsCommon.myCstr(strLocationSeg) <= 0 Then
                                        strLocationSeg = BankLocation
                                    End If
                                    Dim acc3() As String = {strBankSubAcc, -1 * clsCommon.myCdbl(objTr.Cleared_Amount)}
                                    arrylst.Add(acc3)
                                    Dim Acc4() As String = {strBankAcc, clsCommon.myCdbl(objTr.Cleared_Amount)}
                                    arrylst.Add(Acc4)
                                    strBankSubAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankSubAcc, BankLocation, True, trans)
                                ElseIf (clsCommon.CompairString(objTr.Entry_Type, "TW") = CompairStringResult.Equal Or clsCommon.CompairString(objTr.Entry_Type, "TD") = CompairStringResult.Equal) AndAlso JournalEntryDoc = 0 Then

                                    BankIn = clsDBFuncationality.getSingleValue("select ISNULL(To_Bank_Code,'') As To_Bank_Code from TSPL_BANK_TRANSFER WHERE Transfer_No='" + objTr.Document_No + "'", trans)
                                    BankOut = clsDBFuncationality.getSingleValue("select  ISNULL(From_Bank_Code,'') As From_Bank_Code from TSPL_BANK_TRANSFER WHERE Transfer_No='" + objTr.Document_No + "'", trans)

                                    TranBankInMainAcc = clsDBFuncationality.getSingleValue("select ISNULL(BANKACC,'') As BANKACC from TSPL_BANK_MASTER WHERE BANK_CODE='" + BankIn + "'", trans)
                                    TranBankInSubAcc = clsDBFuncationality.getSingleValue("select ISNULL(Sub_Account,'') As Sub_Account from TSPL_BANK_MASTER WHERE BANK_CODE='" + BankIn + "'", trans)

                                    TranBankOutMainAcc = clsDBFuncationality.getSingleValue("select ISNULL(BANKACC,'') As BANKACC from TSPL_BANK_MASTER WHERE BANK_CODE='" + BankOut + "'", trans)
                                    TranBankOutSubAcc = clsDBFuncationality.getSingleValue("select ISNULL(Sub_Account,'') As Sub_Account from TSPL_BANK_MASTER WHERE BANK_CODE='" + BankOut + "'", trans)

                                    Dim InBankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + BankIn + "'", trans)
                                    Dim OutBankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + BankOut + "'", trans)
                                    If clsCommon.myCstr(strLocationSeg) <= 0 Then
                                        strLocationSeg = InBankLocation
                                    End If
                                    TranBankInSubAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(TranBankInSubAcc, InBankLocation, True, trans)
                                    TranBankOutSubAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(TranBankOutSubAcc, OutBankLocation, True, trans)

                                    Dim acc5() As String = {TranBankInMainAcc, -1 * clsCommon.myCdbl(objTr.Cleared_Amount)}
                                    arrylst.Add(acc5)
                                    Dim Acc6() As String = {TranBankInSubAcc, clsCommon.myCdbl(objTr.Cleared_Amount)}
                                    arrylst.Add(Acc6)
                                    Dim acc7() As String = {TranBankOutSubAcc, -1 * clsCommon.myCdbl(objTr.Cleared_Amount)}
                                    arrylst.Add(acc7)
                                    Dim Acc8() As String = {TranBankOutMainAcc, clsCommon.myCdbl(objTr.Cleared_Amount)}
                                    arrylst.Add(Acc8)
                                End If
                            End If
                        Next
                        If arrylst IsNot Nothing AndAlso arrylst.Count > 0 Then
                            clsJournalMaster.FunGrnlEntryWithTrans(strLocationSeg, True, trans, obj.Reconciliation_Date, obj.Description, "BK-RE", "Bank Reco", strCode, obj.Description, "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrylst)
                        End If
                    End If
                End If
                clsDBFuncationality.ExecuteNonQuery("update tspl_BankReco_Head set Post ='Y' where Reconciliation_Id ='" + strCode + "'", trans)

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "tspl_BankReco_Head", "Reconciliation_Id", trans)

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, Optional ByVal StatementDate As Date = Nothing) As clsBankReco
        Dim obj As clsBankReco = Nothing
        Dim Qry As String = " SELECT     Reconciliation_Id, Bank_Code, Bank_Name, Description,  Bank_AccountNo,   Statement_Date,   Statement_Balance," & _
                            " Reconciliation_Date,      Deposit_OutstandingAmt,  TotalWithdrawal,  TotalDeposit,  Withdrawal_OutstandingAmt,AdjustmentStatement_Balance, Post,AdjustmentBook_Balance,Book_Balance,OutOf_Balance FROM tspl_BankReco_Head where Reconciliation_Id='" + strDocNo + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBankReco()
            obj.Reconciliation_Id = strDocNo
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
            obj.Bank_AccountNo = clsCommon.myCstr(dt.Rows(0)("Bank_AccountNo"))
            obj.Statement_Date = clsCommon.myCDate(dt.Rows(0)("Statement_Date"))
            obj.Statement_Balance = clsCommon.myCdbl(dt.Rows(0)("Statement_Balance"))
            obj.Reconciliation_Date = clsCommon.myCDate(dt.Rows(0)("Reconciliation_Date"), "dd/MM/yyyy")
            obj.Deposit_OutstandingAmt = clsCommon.myCdbl(dt.Rows(0)("Deposit_OutstandingAmt"))
            obj.Withdrawal_OutstandingAmt = clsCommon.myCdbl(dt.Rows(0)("Withdrawal_OutstandingAmt"))
            obj.AdjustmentStatement_Balance = clsCommon.myCdbl(dt.Rows(0)("AdjustmentStatement_Balance"))
            obj.AdjustmentBook_Balance = clsCommon.myCdbl(dt.Rows(0)("AdjustmentBook_Balance"))
            obj.Book_Balance = clsCommon.myCdbl(dt.Rows(0)("Book_Balance"))
            obj.OutOf_Balance = clsCommon.myCdbl(dt.Rows(0)("OutOf_Balance"))
            obj.TotalWithdrawal = clsCommon.myCdbl(dt.Rows(0)("TotalWithdrawal"))
            obj.TotalDeposit = clsCommon.myCdbl(dt.Rows(0)("TotalDeposit"))
            If clsCommon.myCstr(dt.Rows(0)("Post")) = "N" Then
                obj.Post = False
            Else
                obj.Post = True
            End If

            Qry = " SELECT Reconciliation_Id, Bank_Code, convert(varchar,Cheque_Date) as [Cheque_Date],Cheque_No,Document_No,convert(varchar,Document_Date)  as Document_Date ,Description,Withdrawal,Deposit, Cleared_Amount,Document_Type,Entry_Type,Reconciliation_Status,Reconciliation_Date,Reconciliation_Description,Customer_Name,Payment_Code_reco,is_Hide,tspl_BankReco_Detail.ReconciliationDone_Date,tspl_BankReco_Detail.ReferenceDocNo,tspl_BankReco_Detail.Remarks FROM tspl_BankReco_Detail where Reconciliation_Id ='" + strDocNo + "' order by Document_Date "
            dt = clsDBFuncationality.GetDataTable(Qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsBankRecoDetails)
                Dim objTr As clsBankRecoDetails
                Dim iii As Integer = 0
                For Each dr As DataRow In dt.Rows
                    iii += 1
                    objTr = New clsBankRecoDetails
                    objTr.Cheque_No = clsCommon.myCstr(dr("Cheque_No"))
                    If clsCommon.myLen(objTr.Cheque_No) > 0 Then
                        objTr.Cheque_Date = clsCommon.GetPrintDate(dr("Cheque_Date"), "dd/MM/yyyy")
                    End If
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))

                    If dr("Document_Date") IsNot DBNull.Value Then
                        objTr.Document_Date = clsCommon.GetPrintDate(dr("Document_Date"), "dd/MM/yyyy")
                    Else
                        objTr.Document_Date = Nothing
                    End If

                    objTr.Description = clsCommon.myCstr(dr("Description"))
                    objTr.Withdrawal = clsCommon.myCdbl(dr("Withdrawal"))
                    objTr.Deposit = clsCommon.myCdbl(dr("Deposit"))
                    objTr.Cleared_Amount = clsCommon.myCdbl(dr("Cleared_Amount"))
                    objTr.Reconciliation_Status = clsCommon.myCstr(dr("Reconciliation_Status"))
                    objTr.Reconciliation_Date = clsCommon.myCDate(dr("Reconciliation_Date"), "dd/MM/yyyy")
                    objTr.Reconciliation_Description = clsCommon.myCstr(dr("Reconciliation_Description"))
                    objTr.Document_Type = clsCommon.myCstr(dr("Document_Type"))
                    objTr.Entry_Type = clsCommon.myCstr(dr("Entry_Type"))
                    objTr.Customer_Name = clsCommon.myCstr(dr("Customer_Name"))
                    objTr.Payment_Code = clsCommon.myCstr(dr("Payment_Code_reco"))
                    objTr.is_Hide = IIf(clsCommon.myCdbl(dr("is_Hide")) = 1, True, False)
                    '' richa against ticket no BHA/18/08/18-000460 on 29 Aug,2018
                    If dr("ReconciliationDone_Date") IsNot DBNull.Value Then
                        objTr.ReconciliationDone_Date = clsCommon.GetPrintDate(dr("ReconciliationDone_Date"), "dd/MM/yyyy")
                    Else
                        objTr.ReconciliationDone_Date = Nothing
                    End If
                    objTr.ReferenceDocNo = clsCommon.myCstr(dr("ReferenceDocNo"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        
        Return obj
    End Function

    Public Shared Function fnAutoGenerateNo(ByVal trans As SqlTransaction) As String

        Dim sql As String = "SELECT MAX(Reconciliation_Id) as MaxValue from tspl_BankReco_Head  where Reconciliation_Id like '%BRC%' "
        Dim Maxvlu As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql, trans))

        If clsCommon.myLen(Maxvlu) > 0 Then
            Maxvlu = clsCommon.incval(Maxvlu)
        Else
            Maxvlu = "BRC0001"
        End If
        Return Maxvlu
    End Function
    '' Anubhooti 04-Sep-2014 Create GL BM00000003437
    Public Shared Function CreateJournalEntry(ByVal strDocno As String, ByVal PRTDocNo As String, ByVal dt As DataTable, ByVal trans As SqlTransaction) As Boolean
        Dim arrylst As ArrayList = New ArrayList()
        Dim strFromBankAC As String = clsCommon.myCstr(dt.Rows(0)("Sub_Account"))
        Dim acc1() As String = {strFromBankAC, -1 * clsCommon.myCdbl(dt.Rows(0)("Cleared_Amount"))}
        arrylst.Add(acc1)

        Dim strToBankAC As String = clsCommon.myCstr(dt.Rows(0)("MainAcc"))
        Dim acc2() As String = {strToBankAC, clsCommon.myCdbl(dt.Rows(0)("Cleared_Amount"))}
        arrylst.Add(acc2)

        Dim StrSrcDocNo As String = strDocno + " " + PRTDocNo
        Dim strFromSeg As String = strFromBankAC.Substring(clsCommon.myLen(strFromBankAC) - 3, 3)
        Dim strToSeg As String = strToBankAC.Substring(clsCommon.myLen(strToBankAC) - 3, 3)

        If Not clsCommon.CompairString(strFromSeg, strToSeg) = CompairStringResult.Equal Then
            Dim strTempAC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP(1) Transfer_Clearing FROM TSPL_PURCHASE_ACCOUNTS", trans))
            If clsCommon.myLen(strTempAC) > 0 Then
                strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strFromSeg, True, trans)
                Dim acc3() As String = {strTempAC, clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))}
                arrylst.Add(acc3)

                strTempAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strTempAC, strToSeg, True, trans)
                Dim acc4() As String = {strTempAC, -1 * clsCommon.myCdbl(dt.Rows(0)("Deposit_Amount"))}
                arrylst.Add(acc4)
            Else
                clsCommon.MyMessageBoxShow("Transfer Clearing account not found.")
                Return False
            End If
        End If

        clsJournalMaster.FunGrnlEntryWithTrans(strFromSeg, True, trans, clsCommon.myCDate(dt.Rows(0)("DocDate")), clsCommon.myCstr(dt.Rows(0)("Entry_Desc")), "BK-RE", "Bank Reco", StrSrcDocNo, clsCommon.myCstr(dt.Rows(0)("SrcDesc")), "B", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrylst)
        Return True
    End Function
    ''richa agarwal 02 aug,2016
    ''
    Public Shared Function GetBankBookQueryToCalculateForwardingDocAmt(ByVal StartDate As String, ByVal EndDate As String, ByVal strSelectedBank As String, ByVal strSelectedLocation As String, ByVal strLocationAddress As String, ByVal strStatus As String, ByVal chrBankType As String, ByVal rptHead As String, ByVal rptRecoStatus As String, ByVal IsIncludeOpeningBankReco As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        Return GetBankBookQueryToCalculateForwardingDocAmt(False, StartDate, EndDate, strSelectedBank, strSelectedLocation, strLocationAddress, strStatus, chrBankType, rptHead, rptRecoStatus, IsIncludeOpeningBankReco, trans)
    End Function
    Public Shared Function GetBankBookQueryToCalculateForwardingDocAmt(ByVal isOPByBankCodeOnly As Boolean, ByVal StartDate As String, ByVal EndDate As String, ByVal strSelectedBank As String, ByVal strSelectedLocation As String, ByVal strLocationAddress As String, ByVal strStatus As String, ByVal chrBankType As String, ByVal rptHead As String, ByVal rptRecoStatus As String, ByVal IsIncludeOpeningBankReco As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            If Not clsCommon.myLen(StartDate) > 0 Then
                StartDate = ""
            Else
                StartDate = clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy")
            End If

            Dim Qry As String = "Select  xxx.Reconciliation_Date, DocType,'" + rptHead + "' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' as RunDate, '" + StartDate + "' as Startdate, '" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType , " & _
            " TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode, " & _
            " case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   , " & _
            " 0.0 as CummulativeBal, Status, (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img else Null end) as Logo_Img , (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img2 else null end) as Logo_Img2, " + strLocationAddress + " as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code] " & _
            " From ( " + Environment.NewLine & _
            " Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, LOC_CODE,Payment_Code, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End as Debit_Amount, CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then BankCharge Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt " & _
            ", (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine & _
            " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine & _
            " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine & _
            " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType," + Environment.NewLine & _
            " (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type]" & _
            " from ( " + Environment.NewLine & _
            " Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type in ('F','S') " + Environment.NewLine & _
            " Union All " + Environment.NewLine & _
            " Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type <>'RC' " + Environment.NewLine & _
            " Union All " + Environment.NewLine & _
            " Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode from TSPL_BANK_TRANSFER " + Environment.NewLine & _
            " Union All   " + Environment.NewLine & _
            "  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' and PY.Payment_Type ='RC' 	  " + Environment.NewLine & _
            " Union All " + Environment.NewLine & _
            " Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR' and  RC.Receipt_Type not in ('F','S')  " + Environment.NewLine & _
            " ) as DocMaster " + Environment.NewLine & _
            " LEFT OUTER JOIN  TSPL_BANK_BOOk ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType and TSPL_BANK_BOOk.TransactionType <>'ToLoc' " + Environment.NewLine & _
            " LEFT OUTER JOIN ( Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post in ('Y','N') and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType " + Environment.NewLine & _
            " WHERE 1=1 "
            Qry += " AND  SOURCEDOC_DATE >'" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "'  " + rptRecoStatus + " "
            If IsIncludeOpeningBankReco Then
                Qry += " and not exists ( select 1 from (select id from TSPL_BANK_BOOK inner join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Payment' and TSPL_PAYMENT_HEADER.is_Opening=1 union all select id from TSPL_BANK_BOOK  inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Receipt' and TSPL_RECEIPT_HEADER.is_Opening=1 ) xxx where xxx.Id=TSPL_BANK_BOOk.Id ) "
            End If
            If IsIncludeOpeningBankReco Then
                Qry += "  union all " & _
                        " select TSPL_BANK_OPENING_RECO.Reco_Date as Reconciliation_Date,'' as Id,TSPL_BANK_OPENING_RECO.Code as SOURCEDOC_NO, TSPL_BANK_OPENING_RECO.Description as Entry_Desc, TSPL_BANK_OPENING_RECO.Reco_Date as SOURCEDOC_DATE, case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Vendor_Code else TSPL_BANK_OPENING_RECO.Cust_Code end as SOURCE_CODE,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_VENDOR_MASTER.Vendor_Name else TSPL_CUSTOMER_MASTER.Customer_Name end as SOURCE_NAME,TSPL_BANK_OPENING_RECO.Bank_Code as BANK_CODE,TSPL_BANK_MASTER.DESCRIPTION as BANK_NAME,SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) as LOC_CODE,TSPL_LOCATION_MASTER.Location_Desc as LOC_NAME,TSPL_BANK_MASTER.BANKACC as BANKGL_Account_Code,TSPL_GL_ACCOUNTS.Description as BANKGL_Account_Name,'' as GL_Account_Code,''GL_Account_Name,TSPL_BANK_OPENING_RECO.Cheque_No as ChequeNo,CONVERT(VARCHAR,TSPL_BANK_OPENING_RECO.Cheque_Date,103) as ChequeDate,TSPL_BANK_OPENING_RECO.Description as NARR_MASTER,'' as NARR_DETAIL,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then 0 else TSPL_BANK_OPENING_RECO.Amt end as Debit_Amount,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Amt else 0 end as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt,(case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then -1 else 1 end) * Amt as Balance,'OP-BR' as DocType,Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 2 Else 1 End as orderColumn,'Opening Bank Reco' as TransType ,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 'Vendor' Else 'Customer' End as Type,'' as Project_Code" & _
                        " from TSPL_BANK_OPENING_RECO " & _
                        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BANK_OPENING_RECO.Vendor_Code " & _
                        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_OPENING_RECO.Cust_Code " & _
                        " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_OPENING_RECO.Bank_Code " & _
                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) " & _
                        " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC " & _
                        " LEFT OUTER JOIN (Select Distinct tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_OPENING_RECO.Bank_Code AND BR.Document_No=TSPL_BANK_OPENING_RECO.Code AND BR.Document_Type='OP-BR' "
                Qry += " where TSPL_BANK_OPENING_RECO.Status='1'"
                If clsCommon.myLen(StartDate) > 0 Then
                    Qry += " AND TSPL_BANK_OPENING_RECO.Reco_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm tt") + "' "
                End If
                Qry += " AND  TSPL_BANK_OPENING_RECO.Reco_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm tt") + "'"

            End If
            Qry += " ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON '" + objCommonVar.CurrentCompanyCode + "'=TSPL_COMPANY_MASTER.Comp_Code " & _
            " Where 1=1 "

            If clsCommon.myLen(chrBankType) > 0 Then
                Qry += " And Bank_type='" & chrBankType & "'"
            End If

            If clsCommon.myLen(strSelectedBank) > 0 Then
                Qry += " AND xxx.BANK_CODE in (" + strSelectedBank + ")"
            End If
            If clsCommon.myLen(strSelectedLocation) > 0 Then
                Qry += " And LOC_CODE in (" + strSelectedLocation + ")"
            End If
            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''richa agarwal 01 Sep,2016

    Public Shared Function GetAmountforbackdateentry(ByVal StartDate As String, ByVal strSelectedBank As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            ' --bank cash book detail query to find minimum amount to alllow payment 
            'Dim Qry As String = " WITH CTETemp as (" + Environment.NewLine & _
            '" Select YYY.DocType, YYY.rptHeading, YYY.NARR_MASTER, YYY.NARR_DETAIL, " + Environment.NewLine & _
            '" convert(varchar,yyy.Reconciliation_Date,103) as [Reco Date], YYY.BANK_CODE, YYY.BankType, YYY.DESCRIPTION, YYY.DocNo, YYY.Entry_Desc, convert(varchar,YYY.DocDate,103)as DocDate, YYY.CHEQUE_NO, YYY.CHEQUE_DATE, YYY.CustVendorCode, YYY.CustVendName, YYY.Source_Code, YYY.Source_Name, YYY.Loc_Code, YYY.Loc_Name, YYY.BANKGL_account_Code, YYY.BANKGL_Account_Name, YYY.BalAmt, YYY.Balance, YYY.Debit_Amount, YYY.Credit_Amount, YYY.CummulativeBal, YYY.Status, YYY.Logo_Img, YYY.Logo_Img2, YYY.Add1, YYY.CompName, YYY.TransType, YYY.Type, ROW_NUMBER() OVER (Partition By YYY.Bank_Code ORDER BY CONVERT(Date,YYY.DocDate,103),DocNo) as [RowNo] From ( " + Environment.NewLine & _
            '" Select  xxx.Reconciliation_Date, DocType,'Cash Book' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, " + Environment.NewLine & _
            '" TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType ,  TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   ,  0.0 as CummulativeBal, Status, (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img else Null end) as Logo_Img , (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img2 else null end) as Logo_Img2, (Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_STATE_MASTER.State_Name) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code LEFT OUTER JOIN TSPL_BANK_MASTER ON RIGHT(TSPL_BANK_MASTER.BANKACC,3)=TSPL_LOCATION_MASTER.Loc_Segment_Code Where TSPL_BANK_MASTER.Bank_Code in (" & strSelectedBank & "))   as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code]  From (  " + Environment.NewLine & _
            '" Select DISTINCT NULL as Reconciliation_Date,'' AS [Id], '' AS [SourceDoc_No], '' as Entry_Desc, NULL AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name], '' AS [Loc_Code],'' as Payment_Code, BANK_CODE AS [Bank_Code],'' AS [Bank_Name], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount],  SUM(TotCredAmt) as TotCredAmt, SUM(TotDebAmt) as  TotDebAmt, SUM(TotDebAmt)-SUM(TotCredAmt) As Balance, '' as DocType, '' as Status, 0 as orderColumn ,'' as TransType, '' as Type From  (Select NULL as Reconciliation_Date,BANK_CODE,Loc_Code, CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as TotCredAmt, CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt,Payment_Code   from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE sourceDoc_Date < '" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'    ) XXX Group by BANK_CODE  UNION All " + Environment.NewLine & _
            '" Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, LOC_CODE,Payment_Code, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End as Debit_Amount, CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then BankCharge Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt , (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine & _
            '" case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine & _
            '" case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine & _
            '" case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType, " + Environment.NewLine & _
            '" (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type] from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y'  and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE 1=1  AND SOURCEDOC_DATE >='" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "' " + Environment.NewLine & _
            '" ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON '" + objCommonVar.CurrentCompanyCode + "'=TSPL_COMPANY_MASTER.Comp_Code  Where 1=1  and  xxx.BANK_CODE in (" & strSelectedBank & ") " + Environment.NewLine & _
            '" ) YYY " + Environment.NewLine & _
            '" )" + Environment.NewLine & _
            '" Select CASE WHEN isnull(min(final.CummulativeBal),0)<0 THEN 0 ELSE isnull(min(final.CummulativeBal),0) END as Balance from  (Select DocType, rptHeading, NARR_MASTER, NARR_DETAIL, " + Environment.NewLine & _
            '" [Reco Date], BANK_CODE, BankType, DESCRIPTION, DocNo, Entry_Desc, DocDate, CHEQUE_NO, CHEQUE_DATE, CustVendorCode, CustVendName, CustomerVendor_Master.Cust_Group_Desc, CustomerVendor_Master.Cust_Type_Desc, CustomerVendor_Master.CUST_CATEGORY_DESC, Source_Code, Source_Name, Loc_Code, Loc_Name, BANKGL_account_Code, BANKGL_Account_Name, Debit_Amount, Credit_Amount, BalAmt, " + Environment.NewLine & _
            '" Balance, (sum(Balance) over (partition by Bank_Code order by RowNo)) as CummulativeBal, Status, Logo_Img, Logo_Img2, Add1, CompName, TransType, RowNo " + Environment.NewLine & _
            '" from CTETemp " + Environment.NewLine & _
            '" LEFT OUTER JOIN (select TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, TSPL_CUSTOMER_MASTER.Cust_Category_Code, TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC, TSPL_CUSTOMER_MASTER.Cust_Type_Code, TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, 'Customer' as [Type] from TSPL_CUSTOMER_MASTER " + Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.Cust_Category_Code " + Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Type_Code" + Environment.NewLine & _
            '" UNION ALL " + Environment.NewLine & _
            '" select TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc as Cust_Group_Desc, '' as Cust_Category_Code, '' as CUST_CATEGORY_DESC, '' as Cust_Type_Code, '' as Cust_Type_Desc, 'Vendor' as [Type] from TSPL_VENDOR_MASTER" + Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code) CustomerVendor_Master on CustomerVendor_Master.Type=CTETemp.Type AND CustomerVendor_Master.Cust_Code=CTETemp.CustVendorCode" + Environment.NewLine & _
            '" ) final"
            ''" where CTETemp.Debit_Amount<>0 and CTETemp.Credit_Amount <>0 " + Environment.NewLine & _

            '' -- richa agarwal changes on 05 May, 2017

            'Dim Qry As String = "WITH CTETemp as (" + Environment.NewLine & _
            '" Select YYY.DocType, YYY.rptHeading, YYY.NARR_MASTER, YYY.NARR_DETAIL, convert(varchar,yyy.Reconciliation_Date,103) as [Reco Date], YYY.BANK_CODE, YYY.BankType, YYY.DESCRIPTION, YYY.DocNo, YYY.Entry_Desc, convert(varchar,YYY.DocDate,103)as DocDate, YYY.CHEQUE_NO, YYY.CHEQUE_DATE, YYY.CustVendorCode, YYY.CustVendName, YYY.Source_Code, YYY.Source_Name, YYY.Loc_Code, YYY.Loc_Name, YYY.BANKGL_account_Code, YYY.BANKGL_Account_Name, YYY.BalAmt, YYY.Balance, YYY.Debit_Amount, YYY.Credit_Amount, YYY.CummulativeBal, YYY.Status, YYY.Logo_Img, YYY.Logo_Img2, YYY.Add1, YYY.CompName, YYY.TransType, YYY.Type, ROW_NUMBER() OVER (Partition By YYY.Bank_Code ORDER BY CONVERT(Date,YYY.DocDate,103),DocNo) as [RowNo],doctypefororder From ( " + Environment.NewLine & _
            '" Select  xxx.Reconciliation_Date, DocType,'Cash Book' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType ,  TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   ,  0.0 as CummulativeBal, Status, (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img else Null end) as Logo_Img , (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img2 else null end) as Logo_Img2, (Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_STATE_MASTER.State_Name) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code LEFT OUTER JOIN TSPL_BANK_MASTER ON RIGHT(TSPL_BANK_MASTER.BANKACC,3)=TSPL_LOCATION_MASTER.Loc_Segment_Code Where TSPL_BANK_MASTER.Bank_Code in (" & strSelectedBank & "))   as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code],doctypefororder  From ( " + Environment.NewLine & _
            '" Select DISTINCT NULL as Reconciliation_Date,'' AS [Id], '' AS [SourceDoc_No], '' as Entry_Desc, NULL AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name], '' AS [Loc_Code],'' as Payment_Code, BANK_CODE AS [Bank_Code],'' AS [Bank_Name], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount],  SUM(TotCredAmt) as TotCredAmt, SUM(TotDebAmt) as  TotDebAmt, SUM(TotDebAmt)-SUM(TotCredAmt) As Balance, '' as DocType, '' as Status, 0 as orderColumn ,'' as TransType, '' as Type,max(doctypefororder) as doctypefororder From  (Select NULL as Reconciliation_Date,BANK_CODE,Loc_Code, CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as TotCredAmt, CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt,Payment_Code,doctypefororder   from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'0' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE sourceDoc_Date < '" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'    ) XXX Group by BANK_CODE  UNION All " + Environment.NewLine & _
            '" Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, LOC_CODE,Payment_Code, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End as Debit_Amount, CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then BankCharge Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt , (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine & _
            '" case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine & _
            '" case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine & _
            '" case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType, " + Environment.NewLine & _
            '" (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type],doctypefororder from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y'  and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'2' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'4' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'1' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'5' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'3' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE 1=1  AND SOURCEDOC_DATE >='" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'     ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON '" + objCommonVar.CurrentCompanyCode + "'=TSPL_COMPANY_MASTER.Comp_Code  Where 1=1 AND xxx.BANK_CODE in (" & strSelectedBank & ") " + Environment.NewLine & _
            '" ) YYY " + Environment.NewLine & _
            '" ) " + Environment.NewLine & _
            '" Select CASE WHEN isnull(min(final.CummulativeBal),0)<0 THEN 0 ELSE isnull(min(final.CummulativeBal),0) END as Balance from  ( " + Environment.NewLine & _
            '" Select DocType, rptHeading, NARR_MASTER, NARR_DETAIL,[Reco Date], BANK_CODE, BankType, DESCRIPTION, DocNo, Entry_Desc, DocDate, CHEQUE_NO, CHEQUE_DATE, CustVendorCode, CustVendName, CustomerVendor_Master.Cust_Group_Desc, CustomerVendor_Master.Cust_Type_Desc, CustomerVendor_Master.CUST_CATEGORY_DESC, Source_Code, Source_Name, Loc_Code, Loc_Name, BANKGL_account_Code, BANKGL_Account_Name, Debit_Amount, Credit_Amount, BalAmt," + Environment.NewLine & _
            '" Balance, (sum(Balance) over (partition by Bank_Code order by convert(date,DocDate,103) ,doctypefororder,rowno)) as CummulativeBal, Status, Logo_Img, Logo_Img2, Add1, CompName, TransType, RowNo,doctypefororder" + Environment.NewLine & _
            '" from CTETemp " + Environment.NewLine & _
            '" LEFT OUTER JOIN (select TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, TSPL_CUSTOMER_MASTER.Cust_Category_Code, TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC, TSPL_CUSTOMER_MASTER.Cust_Type_Code, TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, 'Customer' as [Type] from TSPL_CUSTOMER_MASTER " + Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.Cust_Category_Code" + Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Type_Code " + Environment.NewLine & _
            '" UNION ALL" + Environment.NewLine & _
            '" select TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc as Cust_Group_Desc, '' as Cust_Category_Code, '' as CUST_CATEGORY_DESC, '' as Cust_Type_Code, '' as Cust_Type_Desc, 'Vendor' as [Type] from TSPL_VENDOR_MASTER" + Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code) CustomerVendor_Master on CustomerVendor_Master.Type=CTETemp.Type AND CustomerVendor_Master.Cust_Code=CTETemp.CustVendorCode" + Environment.NewLine & _
            '") final"
            Dim Qry As String = String.Empty

            Qry = "WITH CTETemp as (" + Environment.NewLine & _
            " Select max(finalgrouping.DocType) as DocType, max(finalgrouping.rptHeading) as rptHeading,max( finalgrouping.NARR_MASTER) as NARR_MASTER,max( finalgrouping.NARR_DETAIL) as NARR_DETAIL, max(finalgrouping.[Reco Date]) as [Reco Date], max(finalgrouping.BANK_CODE) as BANK_CODE, max(finalgrouping.BankType) as BankType, max(finalgrouping.DESCRIPTION) as DESCRIPTION, max(finalgrouping.DocNo) as DocNo,max( finalgrouping.Entry_Desc) as Entry_Desc, max(finalgrouping.DocDate) as DocDate, max(finalgrouping.CHEQUE_NO) as CHEQUE_NO, max(finalgrouping.CHEQUE_DATE) as CHEQUE_DATE, max(finalgrouping.CustVendorCode) as CustVendorCode, max(finalgrouping.CustVendName) as CustVendName, max(finalgrouping.Source_Code) as Source_Code, max(finalgrouping.Source_Name) as Source_Name, max(finalgrouping.Loc_Code) as Loc_Code, max(finalgrouping.Loc_Name) as Loc_Name, max(finalgrouping.BANKGL_account_Code) as BANKGL_account_Code, max(finalgrouping.BANKGL_Account_Name) as BANKGL_Account_Name, sum(finalgrouping.BalAmt) as BalAmt, sum(finalgrouping.Balance) as Balance, sum(finalgrouping.Debit_Amount) as Debit_Amount, sum(finalgrouping.Credit_Amount) as Credit_Amount , sum(finalgrouping.CummulativeBal) as CummulativeBal, max(finalgrouping.Status) as Status,max(finalgrouping.Add1) as Add1, max(finalgrouping.CompName) as CompName,max(finalgrouping.TransType) as TransType, max(finalgrouping.Type) as Type, max(finalgrouping.[RowNo]) as [RowNo],max(finalgrouping.doctypefororder) as doctypefororder from ( " & Environment.NewLine & _
            " Select YYY.DocType, YYY.rptHeading, YYY.NARR_MASTER, YYY.NARR_DETAIL, convert(varchar,yyy.Reconciliation_Date,103) as [Reco Date], YYY.BANK_CODE, YYY.BankType, YYY.DESCRIPTION, YYY.DocNo, YYY.Entry_Desc, convert(varchar,YYY.DocDate,103)as DocDate, YYY.CHEQUE_NO, YYY.CHEQUE_DATE, YYY.CustVendorCode, YYY.CustVendName, YYY.Source_Code, YYY.Source_Name, YYY.Loc_Code, YYY.Loc_Name, YYY.BANKGL_account_Code, YYY.BANKGL_Account_Name, YYY.BalAmt, YYY.Balance, YYY.Debit_Amount, YYY.Credit_Amount, YYY.CummulativeBal, YYY.Status, YYY.Logo_Img, YYY.Logo_Img2, YYY.Add1, YYY.CompName, YYY.TransType, YYY.Type, ROW_NUMBER() OVER (Partition By YYY.Bank_Code ORDER BY CONVERT(Date,YYY.DocDate,103),DocNo) as [RowNo],doctypefororder From ( " + Environment.NewLine & _
            " Select  xxx.Reconciliation_Date, DocType,'Cash Book' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType ,  TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   ,  0.0 as CummulativeBal, Status, (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img else Null end) as Logo_Img , (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img2 else null end) as Logo_Img2, (Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_STATE_MASTER.State_Name) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code LEFT OUTER JOIN TSPL_BANK_MASTER ON RIGHT(TSPL_BANK_MASTER.BANKACC,3)=TSPL_LOCATION_MASTER.Loc_Segment_Code Where TSPL_BANK_MASTER.Bank_Code in (" & strSelectedBank & "))   as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code],doctypefororder  From ( " + Environment.NewLine & _
            " Select DISTINCT NULL as Reconciliation_Date,'' AS [Id], '' AS [SourceDoc_No], '' as Entry_Desc,  DATEADD (DAY,-1,'" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "')  AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name], '' AS [Loc_Code],'' as Payment_Code, BANK_CODE AS [Bank_Code],'' AS [Bank_Name], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount],  SUM(TotCredAmt) as TotCredAmt, SUM(TotDebAmt) as  TotDebAmt, SUM(TotDebAmt)-SUM(TotCredAmt) As Balance, '' as DocType, '' as Status, 0 as orderColumn ,'' as TransType, '' as Type,max(doctypefororder) as doctypefororder From  (Select NULL as Reconciliation_Date,BANK_CODE,Loc_Code, CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as TotCredAmt, CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt,Payment_Code,doctypefororder   from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'0' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE sourceDoc_Date < '" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'    ) XXX Group by BANK_CODE  UNION All " + Environment.NewLine & _
            " Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, LOC_CODE,Payment_Code, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End as Debit_Amount, CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then BankCharge Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt , (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine & _
            " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine & _
            " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine & _
            " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType, " + Environment.NewLine & _
            " (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type],doctypefororder from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y'  and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'2' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'4' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'1' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'5' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'3' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE 1=1  AND SOURCEDOC_DATE >='" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'     ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON '" + objCommonVar.CurrentCompanyCode + "'=TSPL_COMPANY_MASTER.Comp_Code  Where 1=1 AND xxx.BANK_CODE in (" & strSelectedBank & ") " + Environment.NewLine & _
            " ) YYY " + Environment.NewLine & _
            " ) finalgrouping  group by DocDate  " + Environment.NewLine & _
            " ) " + Environment.NewLine & _
            " Select count(*) from  ( " + Environment.NewLine & _
            " Select DocType, rptHeading, NARR_MASTER, NARR_DETAIL,[Reco Date], BANK_CODE, BankType, DESCRIPTION, DocNo, Entry_Desc, convert(date,DocDate,103) as DocDate, CHEQUE_NO, CHEQUE_DATE, CustVendorCode, CustVendName, CustomerVendor_Master.Cust_Group_Desc, CustomerVendor_Master.Cust_Type_Desc, CustomerVendor_Master.CUST_CATEGORY_DESC, Source_Code, Source_Name, Loc_Code, Loc_Name, BANKGL_account_Code, BANKGL_Account_Name, Debit_Amount, Credit_Amount, BalAmt," + Environment.NewLine & _
            " Balance, (sum(Balance) over (partition by Bank_Code order by convert(date,DocDate,103) ,doctypefororder,rowno)) as CummulativeBal, Status, Add1, CompName, TransType, RowNo,doctypefororder" + Environment.NewLine & _
            " from CTETemp " + Environment.NewLine & _
            " LEFT OUTER JOIN (select TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, TSPL_CUSTOMER_MASTER.Cust_Category_Code, TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC, TSPL_CUSTOMER_MASTER.Cust_Type_Code, TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, 'Customer' as [Type] from TSPL_CUSTOMER_MASTER " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.Cust_Category_Code" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Type_Code " + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            " select TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc as Cust_Group_Desc, '' as Cust_Category_Code, '' as CUST_CATEGORY_DESC, '' as Cust_Type_Code, '' as Cust_Type_Desc, 'Vendor' as [Type] from TSPL_VENDOR_MASTER" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code) CustomerVendor_Master on CustomerVendor_Master.Type=CTETemp.Type AND CustomerVendor_Master.Cust_Code=CTETemp.CustVendorCode" + Environment.NewLine & _
            ") final "

            Dim doucount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))


            Qry = "WITH CTETemp as (" + Environment.NewLine & _
            " Select max(finalgrouping.DocType) as DocType, max(finalgrouping.rptHeading) as rptHeading,max( finalgrouping.NARR_MASTER) as NARR_MASTER,max( finalgrouping.NARR_DETAIL) as NARR_DETAIL, max(finalgrouping.[Reco Date]) as [Reco Date], max(finalgrouping.BANK_CODE) as BANK_CODE, max(finalgrouping.BankType) as BankType, max(finalgrouping.DESCRIPTION) as DESCRIPTION, max(finalgrouping.DocNo) as DocNo,max( finalgrouping.Entry_Desc) as Entry_Desc, max(finalgrouping.DocDate) as DocDate, max(finalgrouping.CHEQUE_NO) as CHEQUE_NO, max(finalgrouping.CHEQUE_DATE) as CHEQUE_DATE, max(finalgrouping.CustVendorCode) as CustVendorCode, max(finalgrouping.CustVendName) as CustVendName, max(finalgrouping.Source_Code) as Source_Code, max(finalgrouping.Source_Name) as Source_Name, max(finalgrouping.Loc_Code) as Loc_Code, max(finalgrouping.Loc_Name) as Loc_Name, max(finalgrouping.BANKGL_account_Code) as BANKGL_account_Code, max(finalgrouping.BANKGL_Account_Name) as BANKGL_Account_Name, sum(finalgrouping.BalAmt) as BalAmt, sum(finalgrouping.Balance) as Balance, sum(finalgrouping.Debit_Amount) as Debit_Amount, sum(finalgrouping.Credit_Amount) as Credit_Amount , sum(finalgrouping.CummulativeBal) as CummulativeBal, max(finalgrouping.Status) as Status,max(finalgrouping.Add1) as Add1, max(finalgrouping.CompName) as CompName,max(finalgrouping.TransType) as TransType, max(finalgrouping.Type) as Type, max(finalgrouping.[RowNo]) as [RowNo],max(finalgrouping.doctypefororder) as doctypefororder from ( " & Environment.NewLine & _
            " Select YYY.DocType, YYY.rptHeading, YYY.NARR_MASTER, YYY.NARR_DETAIL, convert(varchar,yyy.Reconciliation_Date,103) as [Reco Date], YYY.BANK_CODE, YYY.BankType, YYY.DESCRIPTION, YYY.DocNo, YYY.Entry_Desc, convert(varchar,YYY.DocDate,103)as DocDate, YYY.CHEQUE_NO, YYY.CHEQUE_DATE, YYY.CustVendorCode, YYY.CustVendName, YYY.Source_Code, YYY.Source_Name, YYY.Loc_Code, YYY.Loc_Name, YYY.BANKGL_account_Code, YYY.BANKGL_Account_Name, YYY.BalAmt, YYY.Balance, YYY.Debit_Amount, YYY.Credit_Amount, YYY.CummulativeBal, YYY.Status, YYY.Logo_Img, YYY.Logo_Img2, YYY.Add1, YYY.CompName, YYY.TransType, YYY.Type, ROW_NUMBER() OVER (Partition By YYY.Bank_Code ORDER BY CONVERT(Date,YYY.DocDate,103),DocNo) as [RowNo],doctypefororder From ( " + Environment.NewLine & _
            " Select  xxx.Reconciliation_Date, DocType,'Cash Book' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType ,  TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   ,  0.0 as CummulativeBal, Status, (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img else Null end) as Logo_Img , (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img2 else null end) as Logo_Img2, (Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_STATE_MASTER.State_Name) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code LEFT OUTER JOIN TSPL_BANK_MASTER ON RIGHT(TSPL_BANK_MASTER.BANKACC,3)=TSPL_LOCATION_MASTER.Loc_Segment_Code Where TSPL_BANK_MASTER.Bank_Code in (" & strSelectedBank & "))   as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code],doctypefororder  From ( " + Environment.NewLine & _
            " Select DISTINCT NULL as Reconciliation_Date,'' AS [Id], '' AS [SourceDoc_No], '' as Entry_Desc,  DATEADD (DAY,-1,'" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "')  AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name], '' AS [Loc_Code],'' as Payment_Code, BANK_CODE AS [Bank_Code],'' AS [Bank_Name], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount],  SUM(TotCredAmt) as TotCredAmt, SUM(TotDebAmt) as  TotDebAmt, SUM(TotDebAmt)-SUM(TotCredAmt) As Balance, '' as DocType, '' as Status, 0 as orderColumn ,'' as TransType, '' as Type,max(doctypefororder) as doctypefororder From  (Select NULL as Reconciliation_Date,BANK_CODE,Loc_Code, CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as TotCredAmt, CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt,Payment_Code,doctypefororder   from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'0' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE sourceDoc_Date < '" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'    ) XXX Group by BANK_CODE  UNION All " + Environment.NewLine & _
            " Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, LOC_CODE,Payment_Code, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End as Debit_Amount, CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then BankCharge Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt , (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine & _
            " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine & _
            " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine & _
            " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType, " + Environment.NewLine & _
            " (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type],doctypefororder from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y'  and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'2' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'4' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'1' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'5' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'3' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE 1=1  AND SOURCEDOC_DATE >='" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'     ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON '" + objCommonVar.CurrentCompanyCode + "'=TSPL_COMPANY_MASTER.Comp_Code  Where 1=1 AND xxx.BANK_CODE in (" & strSelectedBank & ") " + Environment.NewLine & _
            " ) YYY " + Environment.NewLine & _
            " ) finalgrouping  group by DocDate  " + Environment.NewLine & _
            " ) " + Environment.NewLine & _
            " Select CASE WHEN isnull(min(final.CummulativeBal),0)<0 THEN 0 ELSE isnull(min(final.CummulativeBal),0) END as Balance from  ( " + Environment.NewLine & _
            " Select DocType, rptHeading, NARR_MASTER, NARR_DETAIL,[Reco Date], BANK_CODE, BankType, DESCRIPTION, DocNo, Entry_Desc, convert(date,DocDate,103) as DocDate, CHEQUE_NO, CHEQUE_DATE, CustVendorCode, CustVendName, CustomerVendor_Master.Cust_Group_Desc, CustomerVendor_Master.Cust_Type_Desc, CustomerVendor_Master.CUST_CATEGORY_DESC, Source_Code, Source_Name, Loc_Code, Loc_Name, BANKGL_account_Code, BANKGL_Account_Name, Debit_Amount, Credit_Amount, BalAmt," + Environment.NewLine & _
            " Balance, (sum(Balance) over (partition by Bank_Code order by convert(date,DocDate,103) ,doctypefororder,rowno)) as CummulativeBal, Status, Add1, CompName, TransType, RowNo,doctypefororder" + Environment.NewLine & _
            " from CTETemp " + Environment.NewLine & _
            " LEFT OUTER JOIN (select TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, TSPL_CUSTOMER_MASTER.Cust_Category_Code, TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC, TSPL_CUSTOMER_MASTER.Cust_Type_Code, TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, 'Customer' as [Type] from TSPL_CUSTOMER_MASTER " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.Cust_Category_Code" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Type_Code " + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            " select TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc as Cust_Group_Desc, '' as Cust_Category_Code, '' as CUST_CATEGORY_DESC, '' as Cust_Type_Code, '' as Cust_Type_Desc, 'Vendor' as [Type] from TSPL_VENDOR_MASTER" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code) CustomerVendor_Master on CustomerVendor_Master.Type=CTETemp.Type AND CustomerVendor_Master.Cust_Code=CTETemp.CustVendorCode" + Environment.NewLine & _
            ") final "
            If doucount > 1 Then
                Qry += " where DocDate >='" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'"
            End If


            ''-------------------------


            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    ''richa agarwal 16 Nov,2016
    Public Shared Function GetQueryForTransactionOFBB(ByVal StartDate As String, ByVal EndDate As String, ByVal strSelectedBank As String, ByVal strSelectedLocation As String, ByVal strLocationAddress As String, ByVal strStatus As String, ByVal chrBankType As String, ByVal rptHead As String, ByVal rptRecoStatus As String, ByVal IsIncludeOpeningBankReco As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        Return GetQueryForTransactionOFBB(False, StartDate, EndDate, strSelectedBank, strSelectedLocation, strLocationAddress, strStatus, chrBankType, rptHead, rptRecoStatus, IsIncludeOpeningBankReco, trans)
    End Function
    Public Shared Function GetQueryForTransactionOFBB(ByVal isOPByBankCodeOnly As Boolean, ByVal StartDate As String, ByVal EndDate As String, ByVal strSelectedBank As String, ByVal strSelectedLocation As String, ByVal strLocationAddress As String, ByVal strStatus As String, ByVal chrBankType As String, ByVal rptHead As String, ByVal rptRecoStatus As String, ByVal IsIncludeOpeningBankReco As Boolean, Optional ByVal trans As SqlTransaction = Nothing, Optional isExcludeProvisionBank As Boolean = False) As String
        Try
            If strStatus = "Y" Then
                strStatus = " AND DocMaster.Posted='Y'"
            ElseIf strStatus = "N" Then
                strStatus = " AND DocMaster.Posted='N'"
            End If

            If Not clsCommon.myLen(StartDate) > 0 Then
                StartDate = ""
            Else
                StartDate = clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy")
            End If

            Dim strCustCatforCurrentUser As String = ""
            strCustCatforCurrentUser = Xtra.CustomerCategory(trans)
            Dim strwhrCustCatforCurrentUser As String = String.Empty

            '-------richa fetch data according to customer category rights of user---------
            If clsCommon.myLen(strCustCatforCurrentUser) > 0 Then
                strwhrCustCatforCurrentUser = " and isnull(TSPL_CUSTOMER_MASTER.customer_category,'') in (" + strCustCatforCurrentUser + ")"

            End If


            'Dim Qry As String = "Select  xxx.Reconciliation_Date, DocType,'" + rptHead + "' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' as RunDate, '" + StartDate + "' as Startdate, '" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType , " & _
            '    " TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode, " & _
            '    " case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   , " & _
            '    " 0.0 as CummulativeBal, Status, (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img else Null end) as Logo_Img , (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img2 else null end) as Logo_Img2, " + strLocationAddress + " as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code],doctypefororder " & _
            '    " From ( " + Environment.NewLine

            Dim Qry As String = "Select  xxx.Reconciliation_Date, DocType,'" + rptHead + "' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' as RunDate, '" + StartDate + "' as Startdate, '" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType , " &
                " TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode, " &
                " case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   , " &
                " 0.0 as CummulativeBal, Status, TSPL_COMPANY_MASTER.Logo_Img as Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2 Logo_Img2, " + strLocationAddress + " as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code],doctypefororder " &
                " From ( " + Environment.NewLine

            Qry += " Select DISTINCT NULL as Reconciliation_Date,'' AS [Id], '' AS [SourceDoc_No], '' as Entry_Desc, NULL AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name],"
            If isOPByBankCodeOnly Then
                ''Qry += " '' AS [Loc_Code],'' as Payment_Code,"
                Qry += " max(RIGHT(TSPL_BANK_MASTER.BANKACC,3)) AS [Loc_Code], '' as Payment_Code,"
            Else
                Qry += " Loc_Code AS [Loc_Code],Payment_Code,"
            End If

            Qry += " XXX.BANK_CODE AS [Bank_Code],'' AS [Bank_Name], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount], " &
            " SUM(TotCredAmt) as TotCredAmt, SUM(TotDebAmt) as  TotDebAmt, SUM(TotDebAmt)-SUM(TotCredAmt) As Balance, '' as DocType, '' as Status, 0 as orderColumn ,'' as TransType, '' as Type,max(doctypefororder) as doctypefororder From " &
            " (Select NULL as Reconciliation_Date,BANK_CODE,Loc_Code, CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as TotCredAmt, CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt,Payment_Code,doctypefororder " &
            "  from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO" &
            " LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'0' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType" &
            " WHERE sourceDoc_Date < '" + StartDate + "' " + strStatus + " " + rptRecoStatus + " " &
            "  ) XXX left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER .BANK_CODE =XXX.BANK_CODE  Group by XXX.BANK_CODE "
            ''  " ) XXX Group by BANK_CODE "
            If Not isOPByBankCodeOnly Then
                Qry += " ,Payment_Code,[Loc_Code] " + Environment.NewLine
            End If
            Qry += " UNION All " + Environment.NewLine &
            " Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, LOC_CODE,Payment_Code, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End as Debit_Amount, CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then BankCharge Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt " &
            ", (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine &
            " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine &
            " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine &
            " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType," + Environment.NewLine &
            " (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type],doctypefororder" &
            " from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y'  and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType" &
            " LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,TSPL_RECEIPT_HEADER.CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,TSPL_RECEIPT_HEADER.Payment_Code,'2' as doctypefororder From TSPL_RECEIPT_HEADER left outer join tspl_customer_master on tspl_customer_master.Cust_code=TSPL_RECEIPT_HEADER.cust_code where 1=1 " & strwhrCustCatforCurrentUser & " " &
            " Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'5' as doctypefororder From TSPL_PAYMENT_HEADER Union All  " + Environment.NewLine &
            "   -------- in query of tranfer ------ " + Environment.NewLine &
            " Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'1' as doctypefororder from TSPL_BANK_TRANSFER where to_bank_code in  (Select to_bank_code from TSPL_BANK_TRANSFER  ) and from_bank_code not  in  (Select from_bank_code from TSPL_BANK_TRANSFER  ) " &
            " Union All " + Environment.NewLine &
            "   -------- out query of tranfer --------- " + Environment.NewLine &
            " Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'4' as doctypefororder from TSPL_BANK_TRANSFER where from_bank_code in  (Select from_bank_code from TSPL_BANK_TRANSFER  ) and to_bank_code in  (Select to_bank_code from TSPL_BANK_TRANSFER  )  " &
            " Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'3' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' " &
            "  Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'6' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No left outer join tspl_customer_master on tspl_customer_master.Cust_code=RC.cust_code where Source_Type='AR'  " & strwhrCustCatforCurrentUser & ") as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType" &
            " WHERE 1=1 "
            '" LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'2' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'4' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'1' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'5' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'3' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType" & _
            '" WHERE 1=1 "
            If clsCommon.myLen(StartDate) > 0 Then
                Qry += " AND SOURCEDOC_DATE >='" + StartDate + "' "
            End If
            Qry += " AND  SOURCEDOC_DATE <='" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "' " + strStatus + " " + rptRecoStatus + " "
            If IsIncludeOpeningBankReco Then
                Qry += " and not exists ( select 1 from (select id from TSPL_BANK_BOOK inner join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Payment' and TSPL_PAYMENT_HEADER.is_Opening=1 union all select id from TSPL_BANK_BOOK  inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Receipt' and TSPL_RECEIPT_HEADER.is_Opening=1 ) xxx where xxx.Id=TSPL_BANK_BOOk.Id ) "
            End If
            If IsIncludeOpeningBankReco Then
                Qry += "  union all " &
                        " select TSPL_BANK_OPENING_RECO.Reco_Date as Reconciliation_Date,'' as Id,TSPL_BANK_OPENING_RECO.Code as SOURCEDOC_NO, TSPL_BANK_OPENING_RECO.Description as Entry_Desc, TSPL_BANK_OPENING_RECO.Reco_Date as SOURCEDOC_DATE, case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Vendor_Code else TSPL_BANK_OPENING_RECO.Cust_Code end as SOURCE_CODE,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_VENDOR_MASTER.Vendor_Name else TSPL_CUSTOMER_MASTER.Customer_Name end as SOURCE_NAME,TSPL_BANK_OPENING_RECO.Bank_Code as BANK_CODE,TSPL_BANK_MASTER.DESCRIPTION as BANK_NAME,SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) as LOC_CODE,TSPL_LOCATION_MASTER.Location_Desc as LOC_NAME,TSPL_BANK_MASTER.BANKACC as BANKGL_Account_Code,TSPL_GL_ACCOUNTS.Description as BANKGL_Account_Name,'' as GL_Account_Code,''GL_Account_Name,TSPL_BANK_OPENING_RECO.Cheque_No as ChequeNo,CONVERT(VARCHAR,TSPL_BANK_OPENING_RECO.Cheque_Date,103) as ChequeDate,TSPL_BANK_OPENING_RECO.Description as NARR_MASTER,'' as NARR_DETAIL,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then 0 else TSPL_BANK_OPENING_RECO.Amt end as Debit_Amount,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Amt else 0 end as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt,(case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then -1 else 1 end) * Amt as Balance,'OP-BR' as DocType,Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 2 Else 1 End as orderColumn,'Opening Bank Reco' as TransType ,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 'Vendor' Else 'Customer' End as Type,'' as Project_Code" &
                        " from TSPL_BANK_OPENING_RECO " &
                        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BANK_OPENING_RECO.Vendor_Code " &
                        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_OPENING_RECO.Cust_Code " &
                        " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_OPENING_RECO.Bank_Code " &
                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) " &
                        " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC " &
                        " LEFT OUTER JOIN (Select Distinct tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_OPENING_RECO.Bank_Code AND BR.Document_No=TSPL_BANK_OPENING_RECO.Code AND BR.Document_Type='OP-BR' "
                Qry += " where TSPL_BANK_OPENING_RECO.Status='1'"
                If clsCommon.myLen(StartDate) > 0 Then
                    Qry += " AND TSPL_BANK_OPENING_RECO.Reco_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm tt") + "' "
                End If
                Qry += " AND  TSPL_BANK_OPENING_RECO.Reco_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm tt") + "'"

            End If
            Qry += " ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON '" + objCommonVar.CurrentCompanyCode + "'=TSPL_COMPANY_MASTER.Comp_Code " &
            " Where 1=1 "

            If clsCommon.myLen(chrBankType) > 0 Then
                Qry += " And Bank_type='" & chrBankType & "'"
            End If
            If isExcludeProvisionBank = True Then
                Qry += " And TSPL_BANK_MASTER.IsProvisionBank=0 "
            End If

            If clsCommon.myLen(strSelectedBank) > 0 Then
                Qry += " AND xxx.BANK_CODE in (" + strSelectedBank + ")"
            End If
            If clsCommon.myLen(strSelectedLocation) > 0 Then
                Qry += " And LOC_CODE in (" + strSelectedLocation + ")"
            End If
            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    ''---------- end--------------
    ''-----------------------


    ''
    Public Shared Function GetBankBookQuery(ByVal StartDate As String, ByVal EndDate As String, ByVal strSelectedBank As String, ByVal strSelectedLocation As String, ByVal strLocationAddress As String, ByVal strStatus As String, ByVal chrBankType As String, ByVal rptHead As String, ByVal rptRecoStatus As String, ByVal IsIncludeOpeningBankReco As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        Return GetBankBookQuery(True, StartDate, EndDate, strSelectedBank, strSelectedLocation, strLocationAddress, strStatus, chrBankType, rptHead, rptRecoStatus, IsIncludeOpeningBankReco, trans)
    End Function
    Public Shared Function GetBankBookQuery(ByVal isOPByBankCodeOnly As Boolean, ByVal StartDate As String, ByVal EndDate As String, ByVal strSelectedBank As String, ByVal strSelectedLocation As String, ByVal strLocationAddress As String, ByVal strStatus As String, ByVal chrBankType As String, ByVal rptHead As String, ByVal rptRecoStatus As String, ByVal IsIncludeOpeningBankReco As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            '-27/11/2012---------------Added By--Pankaj Kumar------This Part Is Used When We Select Status [Posted Or Unposted]-----
            If strStatus = "Y" Then
                strStatus = " AND DocMaster.Posted='Y'"
            ElseIf strStatus = "N" Then
                strStatus = " AND DocMaster.Posted='N'"
            End If
            '-----------------------------------------------------------------------------------------------------------------

            If Not clsCommon.myLen(StartDate) > 0 Then
                StartDate = ""
            Else
                StartDate = clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy")
            End If

            Dim Qry As String = "Select  xxx.Reconciliation_Date, DocType,'" + rptHead + "' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' as RunDate, '" + StartDate + "' as Startdate, '" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType , " & _
                " TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode, " & _
                " case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   , " & _
                " 0.0 as CummulativeBal, Status, (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img else Null end) as Logo_Img , (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img2 else null end) as Logo_Img2, " + strLocationAddress + " as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code] " & _
                " From ( " + Environment.NewLine
            

            Qry += " Select DISTINCT NULL as Reconciliation_Date,'' AS [Id], '' AS [SourceDoc_No], '' as Entry_Desc, NULL AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name],"
            If isOPByBankCodeOnly Then
                Qry += " Loc_Code AS [Loc_Code],'' as Payment_Code,"
            Else
                Qry += " Loc_Code AS [Loc_Code],Payment_Code,"
            End If

            Qry += " BANK_CODE AS [Bank_Code],'' AS [Bank_Name], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount], " & _
            " SUM(TotCredAmt) as TotCredAmt, SUM(TotDebAmt) as  TotDebAmt, SUM(TotDebAmt)-SUM(TotCredAmt) As Balance, '' as DocType, '' as Status, 0 as orderColumn ,'' as TransType, '' as Type From " & _
            " (Select NULL as Reconciliation_Date,BANK_CODE,Loc_Code, CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as TotCredAmt, CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt,Payment_Code " & _
            "  from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO" & _
            " LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType" & _
            " WHERE sourceDoc_Date < '" + StartDate + "' " + strStatus + " " + rptRecoStatus + " " & _
            " ) XXX Group by BANK_CODE "
            'If Not isOPByBankCodeOnly Then
            '    Qry += " ,Payment_Code,[Loc_Code] " + Environment.NewLine
            'End If
            ''BHA/10/12/18-000747  by balwinder on 10/12/2018
            If Not isOPByBankCodeOnly Then
                Qry += " ,Payment_Code ,[Loc_Code] " + Environment.NewLine
            Else
                Qry += " ,[Loc_Code] " + Environment.NewLine
            End If
            Qry += " UNION All " + Environment.NewLine & _
            " Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, LOC_CODE,Payment_Code, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then  case when (TSPL_BANK_BOOK.DocType ='BankTransfer' and TSPL_BANK_BOOK.TransactionType='ToLoc') or (TSPL_BANK_BOOK.DocType ='Payment' and TSPL_BANK_BOOK.TransactionType in ('MI','MIOther'))  then 0 else  BankCharge end Else 0 End as Debit_Amount" + Environment.NewLine + _
        ",CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then case when (TSPL_BANK_BOOK.DocType ='BankTransfer' and TSPL_BANK_BOOK.TransactionType='ToLoc') or (TSPL_BANK_BOOK.DocType ='Payment' and TSPL_BANK_BOOK.TransactionType in ('MI','MIOther'))  then 0 else  BankCharge end Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt " & _
        ", (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then case when (TSPL_BANK_BOOK.DocType ='BankTransfer' and TSPL_BANK_BOOK.TransactionType='ToLoc') or (TSPL_BANK_BOOK.DocType ='Payment' and TSPL_BANK_BOOK.TransactionType in ('MI','MIOther'))  then 0 else  BankCharge end Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then case when (TSPL_BANK_BOOK.DocType ='BankTransfer' and TSPL_BANK_BOOK.TransactionType='ToLoc') or (TSPL_BANK_BOOK.DocType ='Payment' and TSPL_BANK_BOOK.TransactionType in ('MI','MIOther'))  then 0 else  BankCharge end Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine & _
        " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine & _
        " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine & _
        " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType," + Environment.NewLine & _
        " (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type]" & _
        " from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y'  and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType" & _
        " LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, -isnull(BankCharges,0) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType" & _
        " WHERE 1=1 "
            If clsCommon.myLen(StartDate) > 0 Then
                Qry += " AND SOURCEDOC_DATE >='" + StartDate + "' "
            End If
            Qry += " AND  SOURCEDOC_DATE <='" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "' " + strStatus + " " + rptRecoStatus + " "
            If IsIncludeOpeningBankReco Then
                Qry += " and not exists ( select 1 from (select id from TSPL_BANK_BOOK inner join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Payment' and TSPL_PAYMENT_HEADER.is_Opening=1 union all select id from TSPL_BANK_BOOK  inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Receipt' and TSPL_RECEIPT_HEADER.is_Opening=1 ) xxx where xxx.Id=TSPL_BANK_BOOk.Id ) "
            End If
            If IsIncludeOpeningBankReco Then
                Qry += "  union all "
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then ''KDI/15/06/18-000367 By balwinder becuase 2 documents come.
                    Qry += " select TSPL_BANK_OPENING_RECO.Reco_Date as Reconciliation_Date,'' as Id,TSPL_BANK_OPENING_RECO.Code as SOURCEDOC_NO, TSPL_BANK_OPENING_RECO.Description as Entry_Desc, TSPL_BANK_OPENING_RECO.Reco_Date as SOURCEDOC_DATE, case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Vendor_Code else TSPL_BANK_OPENING_RECO.Cust_Code end as SOURCE_CODE,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_VENDOR_MASTER.Vendor_Name else TSPL_CUSTOMER_MASTER.Customer_Name end as SOURCE_NAME,TSPL_BANK_OPENING_RECO.Bank_Code as BANK_CODE,TSPL_BANK_MASTER.DESCRIPTION as BANK_NAME,SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) as LOC_CODE,TSPL_LOCATION_MASTER.Location_Desc as LOC_NAME,TSPL_BANK_MASTER.BANKACC as BANKGL_Account_Code,TSPL_GL_ACCOUNTS.Description as BANKGL_Account_Name,'' as GL_Account_Code,''GL_Account_Name,TSPL_BANK_OPENING_RECO.Cheque_No as ChequeNo,CONVERT(VARCHAR,TSPL_BANK_OPENING_RECO.Cheque_Date,103) as ChequeDate,TSPL_BANK_OPENING_RECO.Description as NARR_MASTER,'' as NARR_DETAIL,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then 0 else TSPL_BANK_OPENING_RECO.Amt end as Debit_Amount,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Amt else 0 end as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt,(case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then -1 else 1 end) * Amt as Balance,'OP-BR' as DocType,Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 2 Else 1 End as orderColumn,'Opening Bank Reco' as TransType ,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 'Vendor' Else 'Customer' End as Type,'' as Project_Code"
                Else
                    Qry += " select TSPL_BANK_OPENING_RECO.Reco_Date as Reconciliation_Date,'' as Id,TSPL_BANK_OPENING_RECO.Code as SOURCEDOC_NO, TSPL_BANK_OPENING_RECO.Description as Entry_Desc, TSPL_BANK_OPENING_RECO.Reco_Date as SOURCEDOC_DATE, case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Vendor_Code else TSPL_BANK_OPENING_RECO.Cust_Code end as SOURCE_CODE,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_VENDOR_MASTER.Vendor_Name else TSPL_CUSTOMER_MASTER.Customer_Name end as SOURCE_NAME,SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) as LOC_CODE, 'OP' as Payment_Code,TSPL_BANK_OPENING_RECO.Bank_Code as BANK_CODE,TSPL_BANK_MASTER.DESCRIPTION as BANK_NAME,TSPL_LOCATION_MASTER.Location_Desc as LOC_NAME,TSPL_BANK_MASTER.BANKACC as BANKGL_Account_Code,TSPL_GL_ACCOUNTS.Description as BANKGL_Account_Name,'' as GL_Account_Code,''GL_Account_Name,TSPL_BANK_OPENING_RECO.Cheque_No as ChequeNo,CONVERT(VARCHAR,TSPL_BANK_OPENING_RECO.Cheque_Date,103) as ChequeDate,TSPL_BANK_OPENING_RECO.Description as NARR_MASTER,'' as NARR_DETAIL,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then 0 else TSPL_BANK_OPENING_RECO.Amt end as Debit_Amount,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Amt else 0 end as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt,(case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then -1 else 1 end) * Amt as Balance,'OP-BR' as DocType,Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 2 Else 1 End as orderColumn,'Opening Bank Reco' as TransType ,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 'Vendor' Else 'Customer' End as Type "
                End If
                Qry += " from TSPL_BANK_OPENING_RECO " & _
                        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BANK_OPENING_RECO.Vendor_Code " & _
                        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_OPENING_RECO.Cust_Code " & _
                        " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_OPENING_RECO.Bank_Code " & _
                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) " & _
                        " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC " & _
                        " LEFT OUTER JOIN (Select Distinct tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_OPENING_RECO.Bank_Code AND BR.Document_No=TSPL_BANK_OPENING_RECO.Code AND BR.Document_Type='OP-BR' "
                Qry += " where TSPL_BANK_OPENING_RECO.Status='1'"
                If clsCommon.myLen(StartDate) > 0 Then
                    Qry += " AND TSPL_BANK_OPENING_RECO.Reco_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm tt") + "' "
                End If
                Qry += " AND  TSPL_BANK_OPENING_RECO.Reco_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm tt") + "'"

            End If
            Qry += " ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON '" + objCommonVar.CurrentCompanyCode + "'=TSPL_COMPANY_MASTER.Comp_Code " & _
            " Where 1=1 "

            If clsCommon.myLen(chrBankType) > 0 Then
                Qry += " And Bank_type='" & chrBankType & "'"
            End If

            If clsCommon.myLen(strSelectedBank) > 0 Then
                Qry += " AND xxx.BANK_CODE in (" + strSelectedBank + ")"
            End If
            If clsCommon.myLen(strSelectedLocation) > 0 Then
                Qry += " And LOC_CODE in (" + strSelectedLocation + ")"
            End If
            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ''richa 23 june 2016
    Public Shared Function GetBankBookLocationDetailQuery(ByVal StartDate As String, ByVal EndDate As String, ByVal strSelectedBank As String, ByVal strSelectedLocation As String, ByVal strLocationAddress As String, ByVal strStatus As String, ByVal chrBankType As String, ByVal rptHead As String, ByVal rptRecoStatus As String, ByVal IsIncludeOpeningBankReco As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            '-27/11/2012---------------Added By--Pankaj Kumar------This Part Is Used When We Select Status [Posted Or Unposted]-----
            If strStatus = "Y" Then
                strStatus = " AND DocMaster.Posted='Y'"
            ElseIf strStatus = "N" Then
                strStatus = " AND DocMaster.Posted='N'"
            End If
            '-----------------------------------------------------------------------------------------------------------------

            If Not clsCommon.myLen(StartDate) > 0 Then
                StartDate = ""
            Else
                StartDate = clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy")
            End If

            Dim Qry As String = "Select  xxx.Reconciliation_Date, DocType,'" + rptHead + "' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' as RunDate, '" + StartDate + "' as Startdate, '" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType , " & _
            " TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode, " & _
            " case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   , " & _
            " 0.0 as CummulativeBal, Status, (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img else Null end) as Logo_Img , (case when ROW_NUMBER() over (order by xxx.Reconciliation_Date)= 1 then TSPL_COMPANY_MASTER.Logo_Img2 else null end) as Logo_Img2, " + strLocationAddress + " as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType, [Type],Payment_Code as [Payment Code], PayeeLocation as [Payee Location Code],PayeeLocation_Name as [Payee Location Name]  " & _
            " From ( " + Environment.NewLine & _
            " Select DISTINCT NULL as Reconciliation_Date,'' AS [Id], '' AS [SourceDoc_No], '' as Entry_Desc, NULL AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name], BANK_CODE AS [Bank_Code], '' AS [Bank_Name], Loc_Code AS [Loc_Code], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount],'' PayeeLocation, '' as PayeeLocation_Name, " & _
            " SUM(TotCredAmt) as TotCredAmt, SUM(TotDebAmt) as  TotDebAmt, SUM(TotDebAmt)-SUM(TotCredAmt) As Balance, '' as DocType, '' as Status, 0 as orderColumn ,'' as TransType, '' as Type,Payment_Code From " & _
            " (Select NULL as Reconciliation_Date,BANK_CODE,Loc_Code, CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as TotCredAmt, CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt,Payment_Code " & _
            "  from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO" & _
            " LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType" & _
            " WHERE sourceDoc_Date < '" + StartDate + "' " + strStatus + " " + rptRecoStatus + " " & _
            " ) XXX Group by BANK_CODE,Payment_Code,[Loc_Code] " + Environment.NewLine & _
            " UNION All " + Environment.NewLine & _
            "Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_CODE, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, " + Environment.NewLine & _
            " case when DocType IN ('Payment','Receipt') AND TransactionType IN ('PY','R') THEN  CONVERT(DECIMAL(18,2),DocMaster.Dramt  * DocMaster.ConvRate )-Case When DocMaster.Dramt  <>0 Then DOCMASTER.BankCharge Else 0 End  when DocMaster.RevType  IN ('Reverse Receipt','Reverse Payment') THEN  CONVERT(DECIMAL(18,2),DocMaster.Dramt  * DocMaster.ConvRate )-Case When DocMaster.Dramt  <>0 Then DOCMASTER.BankCharge Else 0 End ELSE  CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End END AS Debit_Amount, " + Environment.NewLine & _
            " case when DocType IN ('Payment','Receipt') AND TransactionType IN ('PY','R') THEN  CONVERT(DECIMAL(18,2),DocMaster.CrAmt * DocMaster.ConvRate )-Case When DocMaster.CrAmt <>0 Then DOCMASTER.BankCharge Else 0 End when DocMaster.RevType  IN ('Reverse Receipt','Reverse Payment') THEN  CONVERT(DECIMAL(18,2),DocMaster.CrAmt * DocMaster.ConvRate )-Case When DocMaster.CrAmt <>0 Then DOCMASTER.BankCharge Else 0 End ELSE  CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then DOCMASTER.BankCharge Else 0 End END AS Credit_Amount,DocMaster.PayeeLocation,'' as PayeeLocation_Name, 0 as TotCredAmt, 0 AS  TotDebAmt , case when DocType IN ('Payment','Receipt') AND TransactionType IN ('PY','R') THEN  " + Environment.NewLine & _
            " (CONVERT(DECIMAL(18,2),DocMaster.Dramt * DocMaster.ConvRate -Case When DocMaster.Dramt <>0 Then DocMaster.BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),DocMaster.CrAmt * DocMaster.ConvRate -Case When DocMaster.CrAmt <>0 Then DocMaster.BankCharge Else 0 End)) when DocMaster.RevType  IN ('Reverse Receipt','Reverse Payment') THEN  (CONVERT(DECIMAL(18,2),DocMaster.Dramt * DocMaster.ConvRate -Case When DocMaster.Dramt <>0 Then DocMaster.BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),DocMaster.CrAmt * DocMaster.ConvRate -Case When DocMaster.CrAmt <>0 Then DocMaster.BankCharge Else 0 End)) ELSE  (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) END AS Balance, " + Environment.NewLine & _
            " (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine & _
            " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine & _
            " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine & _
            " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType, " + Environment.NewLine & _
            " (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type],Payment_Code from TSPL_BANK_BOOk LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y'  and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType LEFT OUTER JOIN " + Environment.NewLine & _
            " (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,TSPL_RECEIPT_HEADER.Location_GL_Code AS  PayeeLocation ,Payment_Code,	0 as Dramt, 0 as CrAmt,'' as RevType  From TSPL_RECEIPT_HEADER WHERE TSPL_RECEIPT_HEADER.Receipt_Type<>'R' " + Environment.NewLine & _
            " Union All " + Environment.NewLine & _
            " Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,TSPL_PAYMENT_HEADER.Location_GL_Code AS  PayeeLocation ,Payment_Code,0 as Dramt, 0 as CrAmt,'' as RevType From TSPL_PAYMENT_HEADER  WHERE TSPL_PAYMENT_HEADER.Payment_Type <>'PY' " + Environment.NewLine & _
            " Union All " + Environment.NewLine & _
            " Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,  '' AS  PayeeLocation ,Payment_Mode,0 as Dramt, 0 as CrAmt,'' as RevType from TSPL_BANK_TRANSFER " + Environment.NewLine & _
            " Union All " + Environment.NewLine & _
            " Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Location_GL_Code AS  PayeeLocation ,PY.Payment_Code,	0 as Dramt, 0 as CrAmt,'' as RevType from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' AND PY.Payment_Type <>'PY' " + Environment.NewLine & _
            " Union All   " + Environment.NewLine & _
            " Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate, RC.Location_GL_Code AS  PayeeLocation , RC.Payment_Code, " + Environment.NewLine & _
            " 0 as Dramt, 0 as CrAmt,'' as RevType from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR' AND RC.Receipt_Type<>'R' " + Environment.NewLine & _
            " Union All " + Environment.NewLine & _
            " Select DocNo as DocNo , max(Entry_Desc) as Entry_Desc, max(Bank_Charges_Amt)+max(Foreign_Bank_Charges_Amt)*max(ConvRate) as BankCharge, max(Posted) as Posted, max(DocType) as Doc_Type, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate,  PayeeLocation as PayeeLocation, max(Payment_Code) as Payment_Code , Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt,'' as RevType from ( " + Environment.NewLine & _
            " Select TSPL_RECEIPT_HEADER.Receipt_No as DocNo,TSPL_RECEIPT_HEADER.Entry_Desc , TSPL_RECEIPT_HEADER.Bank_Charges_Amt ,TSPL_RECEIPT_HEADER.Foreign_Bank_Charges_Amt,TSPL_RECEIPT_HEADER.ConvRate, TSPL_RECEIPT_HEADER.Posted,'Receipt' as DocType,TSPL_RECEIPT_HEADER.CURRENCY_CODE ,TSPL_RECEIPT_HEADER.Payment_Code , CIH.Loc_Code as PayeeLocation,CIH.Document_No,case when CIH.Document_Type ='C' THEN -RD.Applied_Amount ELSE RD.Applied_Amount end as DrAmt, 0 as CrAmt " + Environment.NewLine & _
            " from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No " + Environment.NewLine & _
            " WHERE TSPL_RECEIPT_HEADER.Receipt_Type='R' " + Environment.NewLine & _
            " )z group by DocNo ,PayeeLocation  " + Environment.NewLine & _
            " union all " + Environment.NewLine & _
            " Select DocNo as DocNo , max(Entry_Desc) as Entry_Desc, max(Bank_Charges_Amt)+max(Foreign_Bank_Charges_Amt)*max(ConvRate) as BankCharge, max(Posted) as Posted, max(DocType) as Doc_Type, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate,  PayeeLocation as PayeeLocation, max(Payment_Code) as Payment_Code , Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt,max(RevType) as RevType  from ( " + Environment.NewLine & _
            " Select TSPL_BANK_REVERSE.Reverse_Code  as DocNo,TSPL_RECEIPT_HEADER.Entry_Desc , TSPL_RECEIPT_HEADER.Bank_Charges_Amt ,TSPL_RECEIPT_HEADER.Foreign_Bank_Charges_Amt,TSPL_RECEIPT_HEADER.ConvRate,Case When TSPL_BANK_REVERSE.Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse Receipt' as RevType ,'Reverse' as DocType,TSPL_RECEIPT_HEADER.CURRENCY_CODE ,TSPL_RECEIPT_HEADER.Payment_Code , CIH.Loc_Code as PayeeLocation,CIH.Document_No,0 as DrAmt, case when CIH.Document_Type ='C' THEN -RD.Applied_Amount ELSE RD.Applied_Amount end as CrAmt from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_BANK_REVERSE   ON TSPL_BANK_REVERSE.Document_No =TSPL_RECEIPT_HEADER.Receipt_No " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No " + Environment.NewLine & _
            " WHERE TSPL_RECEIPT_HEADER.Receipt_Type='R' and TSPL_BANK_REVERSE.Source_Type='AR' " + Environment.NewLine & _
            " )z group by DocNo ,PayeeLocation " + Environment.NewLine & _
            " union all " + Environment.NewLine & _
            " Select DocNo as DocNo , max(Entry_Desc) as Entry_Desc, max(BankCharge) as BankCharge, max(Posted) as Posted, max(DocType) as Doc_Type, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate,  PayeeLocation as PayeeLocation, max(Payment_Code) as Payment_Code , Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt,'' as RevType  from " + Environment.NewLine & _
            " ( " + Environment.NewLine & _
            " Select TSPL_PAYMENT_HEADER.Payment_No  as DocNo,TSPL_PAYMENT_HEADER.Entry_Desc , - TSPL_PAYMENT_HEADER.Bank_Charges AS BankCharge, Case When TSPL_PAYMENT_HEADER.Posted=1 Then 'Y' Else 'N' End as Posted,'Payment' as DocType,TSPL_PAYMENT_HEADER.CURRENCY_CODE ,TSPL_PAYMENT_HEADER.ConvRate,TSPL_PAYMENT_HEADER.Payment_Code , TSPL_VENDOR_INVOICE_HEAD.Loc_Code as PayeeLocation,TSPL_VENDOR_INVOICE_HEAD.Document_No,0 as DrAmt,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_PAYMENT_DETAIL.Applied_Amount end as CrAmt " + Environment.NewLine & _
            " from TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL .Payment_No " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD  ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No" + Environment.NewLine & _
            " WHERE TSPL_PAYMENT_HEADER.Payment_Type ='PY' " + Environment.NewLine & _
            " )z group by DocNo ,PayeeLocation " + Environment.NewLine & _
            " Union all" + Environment.NewLine & _
            " Select DocNo as DocNo , max(Entry_Desc) as Entry_Desc, max(BankCharge) as BankCharge, max(Posted) as Posted, max(DocType) as Doc_Type, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate,  PayeeLocation as PayeeLocation, max(Payment_Code) as Payment_Code , Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt,max(RevType) as RevType from " + Environment.NewLine & _
            " (" + Environment.NewLine & _
            " Select TSPL_BANK_REVERSE.Reverse_Code   as DocNo,TSPL_PAYMENT_HEADER.Entry_Desc , - TSPL_PAYMENT_HEADER.Bank_Charges AS BankCharge, Case When TSPL_BANK_REVERSE.Post='P' Then 'Y' Else 'N' End as Posted,'Reverse Payment' as RevType,'Reverse' as DocType,TSPL_PAYMENT_HEADER.CURRENCY_CODE ,TSPL_PAYMENT_HEADER.ConvRate,TSPL_PAYMENT_HEADER.Payment_Code , TSPL_VENDOR_INVOICE_HEAD.Loc_Code as PayeeLocation,TSPL_VENDOR_INVOICE_HEAD.Document_No,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_PAYMENT_DETAIL.Applied_Amount end as DrAmt,0  as CrAmt " + Environment.NewLine & _
            " from TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL .Payment_No  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_BANK_REVERSE   ON TSPL_BANK_REVERSE.Document_No =TSPL_PAYMENT_HEADER.Payment_No " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD  ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No " + Environment.NewLine & _
            " WHERE TSPL_PAYMENT_HEADER.Payment_Type ='PY' and TSPL_BANK_REVERSE.Source_Type='AP' " + Environment.NewLine & _
            " )z group by DocNo ,PayeeLocation " + Environment.NewLine & _
            " ) as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE 1=1"
            If clsCommon.myLen(StartDate) > 0 Then
                Qry += " AND SOURCEDOC_DATE >='" + StartDate + "' "
                                                            End If
            Qry += " AND  SOURCEDOC_DATE <='" + clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy") + "' " + strStatus + " " + rptRecoStatus + " "
            If IsIncludeOpeningBankReco Then
                Qry += " and not exists ( select 1 from (select id from TSPL_BANK_BOOK inner join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Payment' and TSPL_PAYMENT_HEADER.is_Opening=1 union all select id from TSPL_BANK_BOOK  inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Receipt' and TSPL_RECEIPT_HEADER.is_Opening=1 ) xxx where xxx.Id=TSPL_BANK_BOOk.Id ) "
                                                            End If
            If IsIncludeOpeningBankReco Then
                Qry += "  union all " & _
                        " select TSPL_BANK_OPENING_RECO.Reco_Date as Reconciliation_Date,'' as Id,TSPL_BANK_OPENING_RECO.Code as SOURCEDOC_NO, TSPL_BANK_OPENING_RECO.Description as Entry_Desc, TSPL_BANK_OPENING_RECO.Reco_Date as SOURCEDOC_DATE, case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Vendor_Code else TSPL_BANK_OPENING_RECO.Cust_Code end as SOURCE_CODE,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_VENDOR_MASTER.Vendor_Name else TSPL_CUSTOMER_MASTER.Customer_Name end as SOURCE_NAME,TSPL_BANK_OPENING_RECO.Bank_Code as BANK_CODE,TSPL_BANK_MASTER.DESCRIPTION as BANK_NAME,SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) as LOC_CODE,TSPL_LOCATION_MASTER.Location_Desc as LOC_NAME,TSPL_BANK_MASTER.BANKACC as BANKGL_Account_Code,TSPL_GL_ACCOUNTS.Description as BANKGL_Account_Name,'' as GL_Account_Code,''GL_Account_Name,TSPL_BANK_OPENING_RECO.Cheque_No as ChequeNo,CONVERT(VARCHAR,TSPL_BANK_OPENING_RECO.Cheque_Date,103) as ChequeDate,TSPL_BANK_OPENING_RECO.Description as NARR_MASTER,'' as NARR_DETAIL,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then 0 else TSPL_BANK_OPENING_RECO.Amt end as Debit_Amount,case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then TSPL_BANK_OPENING_RECO.Amt else 0 end as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt,(case when TSPL_BANK_OPENING_RECO.Type ='Withdrawal' then -1 else 1 end) * Amt as Balance,'OP-BR' as DocType,Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 2 Else 1 End as orderColumn,'Opening Bank Reco' as TransType ,Case When TSPL_BANK_OPENING_RECO.Type ='Withdrawal'  Then 'Vendor' Else 'Customer' End as Type,'' as Project_Code" & _
                        " from TSPL_BANK_OPENING_RECO " & _
                        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BANK_OPENING_RECO.Vendor_Code " & _
                        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_OPENING_RECO.Cust_Code " & _
                        " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_OPENING_RECO.Bank_Code " & _
                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=SUBSTRING( TSPL_BANK_MASTER.BANKACC,len(TSPL_BANK_MASTER.BANKACC)-2,len(TSPL_BANK_MASTER.BANKACC)) " & _
                        " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC " & _
                        " LEFT OUTER JOIN (Select Distinct tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_OPENING_RECO.Bank_Code AND BR.Document_No=TSPL_BANK_OPENING_RECO.Code AND BR.Document_Type='OP-BR' "
                Qry += " where TSPL_BANK_OPENING_RECO.Status='1'"
                If clsCommon.myLen(StartDate) > 0 Then
                    Qry += " AND TSPL_BANK_OPENING_RECO.Reco_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm tt") + "' "
                                                                End If
                Qry += " AND  TSPL_BANK_OPENING_RECO.Reco_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm tt") + "'"

                                                            End If
            Qry += " ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE Left Outer Join TSPL_COMPANY_MASTER ON '" + objCommonVar.CurrentCompanyCode + "'=TSPL_COMPANY_MASTER.Comp_Code " & _
            " Where 1=1 "

            If clsCommon.myLen(chrBankType) > 0 Then
                Qry += " And Bank_type='" & chrBankType & "'"
                                                            End If

            If clsCommon.myLen(strSelectedBank) > 0 Then
                Qry += " AND xxx.BANK_CODE in (" + strSelectedBank + ")"
                                                            End If
            If clsCommon.myLen(strSelectedLocation) > 0 Then
                Qry += " And LOC_CODE in (" + strSelectedLocation + ")"
                                                            End If
            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''-------------------------

    '===========BM00000008262
    Public Shared Function SetOutstandingEntry(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal strDocType As String, ByVal tran As SqlTransaction) As Boolean
        Return SetOutstandingEntry(strDocNo, dtDocDate, strDocType, tran, True)
    End Function
    Public Shared Function SetOutstandingEntry(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal strDocType As String, ByVal tran As SqlTransaction, ByVal CheckThatRecoExistsAfterDocDate As Boolean) As Boolean
        Dim qry As String = "Select   TSPL_BANK_MASTER.BANK_CODE , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE, ChequeNo as CHEQUE_NO,SOURCEDOC_NO as DocNo,SOURCE_NAME," + Environment.NewLine & _
        " Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount,Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else convert(decimal(18,2),Debit_Amount) end as Debit_Amount,TransType, SOURCEDOC_DATE as DocDate,Case When Credit_Amount>0 Then 'W' else 'D' end as 'Entry_Type',SOURCE_CODE as Cutomer_Name,Payment_Code as Payment_Code_reco" + Environment.NewLine & _
        " From ( " + Environment.NewLine & _
        " Select  null as  Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_CODE, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End as Debit_Amount, CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then BankCharge Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt , (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else " + Environment.NewLine & _
        " case when DocType='BankTransfer' then 'BK-TF' else " + Environment.NewLine & _
        " case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  " + Environment.NewLine & _
        " case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType" + Environment.NewLine & _
        " ,'O' as  Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType," + Environment.NewLine & _
        " (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as[Type],Payment_Code " + Environment.NewLine & _
        "  from TSPL_BANK_BOOk" + Environment.NewLine & _
        " LEFT OUTER JOIN (" + Environment.NewLine & _
        " Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_RECEIPT_HEADER " + Environment.NewLine & _
        " Union All" + Environment.NewLine & _
        " Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code From TSPL_PAYMENT_HEADER " + Environment.NewLine & _
        " Union All" + Environment.NewLine & _
        " Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode from TSPL_BANK_TRANSFER " + Environment.NewLine & _
        " Union All" + Environment.NewLine & _
        " Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP'" + Environment.NewLine & _
        "  Union All" + Environment.NewLine & _
        " Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code " + Environment.NewLine & _
        " from TSPL_BANK_REVERSE" + Environment.NewLine & _
        " left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR'" + Environment.NewLine & _
        " ) as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType " + Environment.NewLine & _
        " WHERE 1=1 AND DocMaster.Posted='Y'   " + Environment.NewLine & _
        " and not exists ( select 1 from (select id from TSPL_BANK_BOOK inner join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Payment' and TSPL_PAYMENT_HEADER.is_Opening=1 " + Environment.NewLine & _
        " union all" + Environment.NewLine & _
        " select id from TSPL_BANK_BOOK  inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_BOOK.SOURCEDOC_NO and TSPL_BANK_BOOK.DocType='Receipt' and TSPL_RECEIPT_HEADER.is_Opening=1 ) xxx where xxx.Id=TSPL_BANK_BOOk.Id)" + Environment.NewLine & _
        " ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE " + Environment.NewLine & _
        " Where 1 = 1" + Environment.NewLine & _
        " and  SOURCEDOC_NO='" + strDocNo + "' " + Environment.NewLine & _
        " and TransType='" + strDocType + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If CheckThatRecoExistsAfterDocDate Then
                    ''Check that reco exists after doc date
                    qry = "select 1 from  tspl_BankReco_Head where Reconciliation_Date>='" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "' and Bank_Code='" + clsCommon.myCstr(dr("BANK_CODE")) + "'"
                    Dim dtcheck As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dtcheck Is Nothing OrElse dtcheck.Rows.Count <= 0 Then
                        Continue For
                    End If
                End If

                ''Delete Outstanding reco 
                qry = "select tspl_BankReco_Head.Reconciliation_Id,tspl_BankReco_Head.Reconciliation_Date,tspl_BankReco_Detail.Deposit,tspl_BankReco_Detail.Withdrawal,tspl_BankReco_Detail.Entry_Type from tspl_BankReco_Detail" + Environment.NewLine & _
                " left outer join tspl_BankReco_Head on tspl_BankReco_Head.Reconciliation_Id=tspl_BankReco_Detail.Reconciliation_Id " + Environment.NewLine & _
                " where  tspl_BankReco_Detail.Document_No='" + strDocNo + "' and Document_Type='" + strDocType + "' and tspl_BankReco_Detail.Reconciliation_Status='O'"
                Dim dtDeleteReco As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                If dtDeleteReco IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each drDeleteReco As DataRow In dtDeleteReco.Rows
                        Dim strAmt As String
                        Dim strEntryType As String = clsCommon.myCstr(drDeleteReco("Entry_Type"))
                        If clsCommon.CompairString(strEntryType, "D") = CompairStringResult.Equal Then
                            strAmt = clsCommon.myCstr(clsCommon.myCdbl(drDeleteReco("Deposit")))
                        Else
                            strAmt = clsCommon.myCstr(clsCommon.myCdbl(drDeleteReco("Withdrawal")))
                        End If


                        If clsCommon.CompairString(strEntryType, "D") = CompairStringResult.Equal Then
                            qry = "update tspl_BankReco_Head set Book_Balance=Book_Balance-'" + strAmt + "',AdjustmentBook_Balance=AdjustmentBook_Balance-'" + strAmt + "',AdjustmentStatement_Balance=AdjustmentStatement_Balance-'" + strAmt + "'"
                            qry += ",Deposit_OutstandingAmt=Deposit_OutstandingAmt-'" + strAmt + "'"
                            qry += " where Reconciliation_Id='" + clsCommon.myCstr(drDeleteReco("Reconciliation_Id")) + "'"
                        Else
                            qry = "update tspl_BankReco_Head set Book_Balance=Book_Balance+'" + strAmt + "',AdjustmentBook_Balance=AdjustmentBook_Balance+'" + strAmt + "',AdjustmentStatement_Balance=AdjustmentStatement_Balance+'" + strAmt + "'"
                            qry += ",Withdrawal_OutstandingAmt=Withdrawal_OutstandingAmt-'" + strAmt + "'"
                            qry += " where Reconciliation_Id='" + clsCommon.myCstr(drDeleteReco("Reconciliation_Id")) + "'"
                        End If
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        qry = "Delete from tspl_BankReco_Detail where  Reconciliation_Id='" + clsCommon.myCstr(drDeleteReco("Reconciliation_Id")) + "' and  tspl_BankReco_Detail.Document_No='" + strDocNo + "' and Document_Type='" + strDocType + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    Next
                End If

                ''Insert Outstanding reco 
                qry = "select max(InnHead.Reconciliation_Date) as Reconciliation_Date,max(InnDetail.Reconciliation_Id) as Reconciliation_Id from tspl_BankReco_Detail as InnDetail" + Environment.NewLine & _
                " left outer join tspl_BankReco_Head as InnHead on InnHead.Reconciliation_Id=InnDetail.Reconciliation_Id" + Environment.NewLine & _
                " where InnDetail.Document_No='" + clsCommon.myCstr(dr("DocNo")) + "' and InnDetail.Document_Type='" + clsCommon.myCstr(dr("TransType")) + "' and InnDetail.Reconciliation_Status='C' "
                Dim dtMaxClearDate As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                Dim dtLastClear As DateTime?
                Dim strLastRecoID As String = ""
                If dtMaxClearDate.Rows(0)("Reconciliation_Date") IsNot DBNull.Value Then
                    dtLastClear = clsCommon.myCDate(dtMaxClearDate.Rows(0)("Reconciliation_Date"))
                    strLastRecoID = clsCommon.myCstr(dtMaxClearDate.Rows(0)("Reconciliation_Id"))
                End If
                qry = "select tspl_BankReco_Head.Reconciliation_Id,tspl_BankReco_Head.Reconciliation_Date " + Environment.NewLine & _
                " from tspl_BankReco_Head " + Environment.NewLine & _
                " where tspl_BankReco_Head.Reconciliation_Date>='" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "' and tspl_BankReco_Head.Bank_Code='" + clsCommon.myCstr(dr("BANK_CODE")) + "'"
                If dtLastClear IsNot Nothing Then
                    qry += " and tspl_BankReco_Head.Reconciliation_Date <= '" + clsCommon.GetPrintDate(dtLastClear, "dd/MMM/yyyy") + "' and Reconciliation_Id not in  ('" + strLastRecoID + "')"
                End If

                Dim dtInsertReco As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                If dtInsertReco IsNot Nothing AndAlso dtInsertReco.Rows.Count > 0 Then
                    For Each drInsertReco As DataRow In dtInsertReco.Rows
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Reconciliation_Id", clsCommon.myCstr(drInsertReco("Reconciliation_Id")))
                        clsCommon.AddColumnsForChange(coll, "Bank_Code", clsCommon.myCstr(dr("BANK_CODE")))
                        clsCommon.AddColumnsForChange(coll, "Cheque_No", clsCommon.myCstr(dr("CHEQUE_NO")))
                        If clsCommon.myLen(dr("CHEQUE_NO")) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(clsCommon.myCDate(dr("CHEQUE_DATE")), "dd/MMM/yyyy"))
                        End If
                        clsCommon.AddColumnsForChange(coll, "Document_No", clsCommon.myCstr(dr("DocNo")))
                        If clsCommon.myLen(dr("DocDate")) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(clsCommon.myCDate(dr("DocDate")), "dd/MMM/yyyy"))
                        End If
                        clsCommon.AddColumnsForChange(coll, "Description", clsCommon.myCstr(dr("SOURCE_NAME")))
                        clsCommon.AddColumnsForChange(coll, "Withdrawal ", clsCommon.myCdbl(dr("Credit_Amount")))
                        clsCommon.AddColumnsForChange(coll, "Deposit", clsCommon.myCdbl(dr("Debit_Amount")))
                        clsCommon.AddColumnsForChange(coll, "Cleared_Amount ", 0)
                        clsCommon.AddColumnsForChange(coll, "Reconciliation_Status", "O")
                        clsCommon.AddColumnsForChange(coll, "Reconciliation_Date", clsCommon.GetPrintDate(clsCommon.myCDate(drInsertReco("Reconciliation_Date")), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Reconciliation_Description", "")
                        clsCommon.AddColumnsForChange(coll, "Document_Type", clsCommon.myCstr(dr("TransType")))
                        clsCommon.AddColumnsForChange(coll, "Entry_Type", clsCommon.myCstr(dr("Entry_Type")))
                        clsCommon.AddColumnsForChange(coll, "Customer_Name", clsCommon.myCstr(dr("Cutomer_Name")))
                        clsCommon.AddColumnsForChange(coll, "Payment_Code_reco", clsCommon.myCstr(dr("Payment_Code_reco")))
                        clsCommonFunctionality.UpdateDataTable(coll, "tspl_BankReco_Detail", OMInsertOrUpdate.Insert, "", tran)

                        Dim strAmt As String
                        Dim strEntryType As String = clsCommon.myCstr(dr("Entry_Type"))

                        If clsCommon.CompairString(strEntryType, "D") = CompairStringResult.Equal Then
                            strAmt = clsCommon.myCdbl(clsCommon.myCdbl(dr("Debit_Amount")))
                        Else
                            strAmt = clsCommon.myCdbl(clsCommon.myCdbl(dr("Credit_Amount")))
                        End If

                        If clsCommon.CompairString(strEntryType, "D") = CompairStringResult.Equal Then
                            qry = "update tspl_BankReco_Head set Book_Balance=Book_Balance+'" + strAmt + "',AdjustmentBook_Balance=AdjustmentBook_Balance+'" + strAmt + "',AdjustmentStatement_Balance=AdjustmentStatement_Balance+'" + strAmt + "'"
                            qry += ",Deposit_OutstandingAmt=Deposit_OutstandingAmt+'" + strAmt + "'"
                            qry += " where Reconciliation_Id='" + clsCommon.myCstr(drInsertReco("Reconciliation_Id")) + "'"
                        Else
                            qry = "update tspl_BankReco_Head set Book_Balance=Book_Balance-'" + strAmt + "',AdjustmentBook_Balance=AdjustmentBook_Balance-'" + strAmt + "',AdjustmentStatement_Balance=AdjustmentStatement_Balance-'" + strAmt + "'"
                            qry += ",Withdrawal_OutstandingAmt=Withdrawal_OutstandingAmt+'" + strAmt + "'"
                            qry += " where Reconciliation_Id='" + clsCommon.myCstr(drInsertReco("Reconciliation_Id")) + "'"
                        End If

                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    Next
                End If
            Next
        End If
        Return True
    End Function
    ''richa BHA/04/10/18-000599
    Public Shared Function UpdateBankChargesAndRemarksOnBankBook(ByVal strDocNo As String, ByVal strDocType As String, ByVal dblBankCharges As Double, ByVal strRemarks As String, ByVal tran As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        If clsCommon.CompairString(strDocType, "Receipt") = CompairStringResult.Equal Then
            qry = "update TSPL_BANK_BOOK set Bank_charges=" & clsCommon.myCdbl(dblBankCharges) & ",Remarks='" & clsCommon.myCstr(strRemarks) & "' where SOURCEDOC_NO ='" & clsCommon.myCstr(strDocNo) & "' and DocType ='Receipt' "
        ElseIf clsCommon.CompairString(strDocType, "Payment") = CompairStringResult.Equal Then
            qry = "update TSPL_BANK_BOOK set Bank_charges=" & clsCommon.myCdbl(dblBankCharges) & ",Remarks='" & clsCommon.myCstr(strRemarks) & "' where SOURCEDOC_NO ='" & clsCommon.myCstr(strDocNo) & "' and DocType ='Payment' "
        ElseIf clsCommon.CompairString(strDocType, "Reverse") = CompairStringResult.Equal Then
            qry = "update TSPL_BANK_BOOK set Bank_charges=" & clsCommon.myCdbl(dblBankCharges) & ",Remarks='" & clsCommon.myCstr(strRemarks) & "' where SOURCEDOC_NO ='" & clsCommon.myCstr(strDocNo) & "' and DocType ='Reverse' "
        ElseIf clsCommon.CompairString(strDocType, "BankTransfer") = CompairStringResult.Equal Then
            qry = "update TSPL_BANK_BOOK set Bank_charges=" & clsCommon.myCdbl(dblBankCharges) & ",Remarks='" & clsCommon.myCstr(strRemarks) & "' where SOURCEDOC_NO ='" & clsCommon.myCstr(strDocNo) & "' and DocType ='BankTransfer' "
        End If
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return True
    End Function

    Private Shared Function getDocumentWilleffect(ByVal arr As ArrayList, ByVal tran As SqlTransaction) As Boolean
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(VerifyAllReco(arr, "", True), tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("Reco No " + clsCommon.myCstr(dt.Rows(0)("Reconciliation_Id")) + Environment.NewLine + "Effective Total Deposit:  " + clsCommon.myCstr(dt.Rows(0)("Deposit")) + Environment.NewLine + "Effective Total Withdraw:  " + clsCommon.myCstr(dt.Rows(0)("Withdrawal")))
        End If
        Return True
    End Function

    Public Shared Function VerifyAllReco(ByVal Arr As ArrayList, ByVal strReconID As String, ByVal isShowInRecoGroup As Boolean) As String
        Dim qry As String = ""
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            qry = "select Reconciliation_Id,Document_No,Deposit,Withdrawal from tspl_bankreco_Detail where document_no in (" + clsCommon.GetMulcallString(Arr) + ") "
            If clsCommon.myLen(strReconID) > 0 Then
                qry += " And Reconciliation_Id='" + strReconID + "'"
            End If
            If isShowInRecoGroup Then
                qry = "select Reconciliation_Id,sum(Withdrawal) as Withdrawal,sum(Deposit) as Deposit from ( " + qry + ")xx group by Reconciliation_Id having sum(Deposit-Withdrawal)<>0"
            End If
        End If
        Return qry
    End Function

    Public Shared Function SetHideEntry(ByVal Arr As List(Of clsBankRecoDetails)) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Dim strAmt As String
        Dim AddSub As String
        Dim dt As DataTable
        Try
            Dim arrUnhideToHide As New ArrayList
            Dim arrHideToUnhide As New ArrayList
            For Each obj As clsBankRecoDetails In Arr
                If obj.is_Hide Then
                    arrUnhideToHide.Add(obj.Document_No)
                Else
                    arrHideToUnhide.Add(obj.Document_No)
                End If
            Next
            If arrUnhideToHide IsNot Nothing AndAlso arrUnhideToHide.Count > 0 Then
                Try
                    getDocumentWilleffect(arrUnhideToHide, tran)
                Catch ex As Exception
                    Throw New Exception("Error in Unhide to Hide Document" + Environment.NewLine + ex.Message)
                End Try
            End If
            If arrHideToUnhide IsNot Nothing AndAlso arrHideToUnhide.Count > 0 Then
                Try
                    getDocumentWilleffect(arrHideToUnhide, tran)
                Catch ex As Exception
                    Throw New Exception("Error in Hide to Unhide Document " + Environment.NewLine + ex.Message)
                End Try
            End If


            For Each obj As clsBankRecoDetails In Arr
                qry = "select top 1 Document_No from tspl_bankreco_Detail where Document_No='" + obj.Document_No + "' and Document_Type='" + obj.Document_Type + "'  and Reconciliation_Status='C'"
                dt = clsDBFuncationality.GetDataTable(qry, tran)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Document No - " + obj.Document_No + " Document Type - " + obj.Document_Type + " is cleared in Bank reco No-" + clsCommon.myCstr(dt.Rows(0)("Document_No")))
                End If

                qry = "update tspl_BankReco_Detail set is_Hide=" + IIf(obj.is_Hide, "1", "0") + " where  Document_No='" + obj.Document_No + "' and Document_Type='" + obj.Document_Type + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)



                If clsCommon.CompairString(obj.Entry_Type, "D") = CompairStringResult.Equal Then
                    strAmt = clsCommon.myCdbl(obj.Deposit)
                Else
                    strAmt = clsCommon.myCdbl(obj.Withdrawal)
                End If
                If clsCommon.CompairString(obj.Entry_Type, "D") = CompairStringResult.Equal Then
                    If obj.is_Hide Then
                        AddSub = "-"
                    Else
                        AddSub = "+"
                    End If
                    qry = "update tspl_BankReco_Head set Book_Balance=Book_Balance" + AddSub + "'" + strAmt + "',AdjustmentBook_Balance=AdjustmentBook_Balance" + AddSub + "'" + strAmt + "',AdjustmentStatement_Balance=AdjustmentStatement_Balance" + AddSub + "'" + strAmt + "'"
                    qry += ",Deposit_OutstandingAmt=Deposit_OutstandingAmt" + AddSub + "'" + strAmt + "'"
                    qry += " where Reconciliation_Id in (select Reconciliation_Id from tspl_BankReco_Detail where  Document_No='" + obj.Document_No + "' and Document_Type='" + obj.Document_Type + "')"
                Else
                    If obj.is_Hide Then
                        AddSub = "+"
                    Else
                        AddSub = "-"
                    End If
                    qry = "update tspl_BankReco_Head set Book_Balance=Book_Balance" + AddSub + "'" + strAmt + "',AdjustmentBook_Balance=AdjustmentBook_Balance" + AddSub + "'" + strAmt + "',AdjustmentStatement_Balance=AdjustmentStatement_Balance" + AddSub + "'" + strAmt + "'"
                    qry += ",Withdrawal_OutstandingAmt=Withdrawal_OutstandingAmt"
                    If obj.is_Hide Then
                        qry += "-"
                    Else
                        qry += "+"
                    End If
                    qry += "'" + strAmt + "' where Reconciliation_Id in (select Reconciliation_Id from tspl_BankReco_Detail where  Document_No='" + obj.Document_No + "' and Document_Type='" + obj.Document_Type + "')"
                End If
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Next
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


Public Class clsBankRecoDetails

#Region "Variable"
    Public Reconciliation_Id As String = Nothing
    Public Bank_Code As String = Nothing
    Public Cheque_Date As Date?
    Public Cheque_No As String = Nothing
    Public Document_No As String = Nothing
    Public Document_Date As Date?
    Public Description As String = Nothing
    Public Withdrawal As Double = 0
    Public Deposit As Double = 0
    Public Cleared_Amount As Double = 0
    Public Reconciliation_Status As Char = Nothing
    Public Reconciliation_Date As DateTime
    Public Reconciliation_Description As String = Nothing
    Public Document_Type As String = Nothing
    Shared StrQuery As String = Nothing
    '' Anubhooti 04-Sep-2014 BM00000003437
    Public Entry_Type As String = Nothing
    Public Customer_Name As String = Nothing
    Public Payment_Code As String = Nothing
    Public is_Hide As Boolean = False

    Public ReconciliationDone_Date As Date?
    Public ReferenceDocNo As String = Nothing
    Public Remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBankRecoDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBankRecoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Reconciliation_Id", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
                clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
                If clsCommon.myLen(obj.Cheque_No) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                If clsCommon.myLen(obj.Document_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Withdrawal ", obj.Withdrawal)
                clsCommon.AddColumnsForChange(coll, "Deposit", obj.Deposit)
                clsCommon.AddColumnsForChange(coll, "Cleared_Amount ", obj.Cleared_Amount)
                clsCommon.AddColumnsForChange(coll, "Reconciliation_Status", obj.Reconciliation_Status)
                clsCommon.AddColumnsForChange(coll, "Reconciliation_Date", clsCommon.GetPrintDate(obj.Reconciliation_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Reconciliation_Description", obj.Reconciliation_Description)
                clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
                '' Anubhooti 04-Sep-2014 (Save EntryType)     
                clsCommon.AddColumnsForChange(coll, "Entry_Type", obj.Entry_Type)
                clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
                clsCommon.AddColumnsForChange(coll, "Payment_Code_reco", obj.Payment_Code)
                clsCommon.AddColumnsForChange(coll, "is_Hide", IIf(obj.is_Hide, 1, 0)) ''

                '' richa against ticket no BHA/18/08/18-000460 on 29 Aug,2018
                clsCommon.AddColumnsForChange(coll, "ReconciliationDone_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "ReferenceDocNo", obj.ReferenceDocNo)
                clsCommon.AddColumnsForChange(coll, "Remarks ", obj.Remarks)

                clsCommonFunctionality.UpdateDataTable(coll, "tspl_BankReco_Detail", OMInsertOrUpdate.Insert, "", trans)
                If obj.Document_Type = "W" Then
                    StrQuery = "update TSPL_PAYMENT_HEADER set IsRecoCleared='Y' where Payment_No  ='" + obj.Document_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(StrQuery, trans)
                End If
                If obj.Document_Type = "D" Then
                    StrQuery = "update TSPL_RECEIPT_HEADER set IsRecoCleared='Y' where Receipt_No ='" + obj.Document_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(StrQuery, trans)
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function isHide(ByVal strDocNo As String, ByVal docType As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select top 1 Document_No from tspl_bankreco_Detail where Document_No='" + strDocNo + "' and Document_Type='" + docType + "'  and is_Hide='1'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function


End Class
