'==================================================================================
'-------Created By--Pankaj Kuamar
'-------Created Date--08/03/2013:01:00PM
'-------Table Used--TSPL_MP_PAY_HEAD, TSPL_MP_PAY_DETAILS
'==================================================================================
Imports common
Imports System.Data.SqlClient

Public Class clsFarmerPaymentHeader
#Region "Variables"
    Public memorndmamt As String = Nothing
    Public Payment_No As String = Nothing
    Public Payment_Date As DateTime
    Public Payment_Post_Date As Date?
    Public Bank_Code As String = Nothing
    Public Payment_Type As String = Nothing
    Public Farmer_Code As String = Nothing
    Public Farmer_Name As String = Nothing
    Public Remit_To As String = Nothing
    Public Entry_Desc As String = Nothing
    Public Reference As String = Nothing
    Public Narration As String = Nothing
    Public Payment_Code As String = Nothing
    Public Cheque_No As String = Nothing
    Public IsApplyDocAuto As Integer = 0
    Public Cheque_Date As Date?
    Public PDC_Cheque As Char = "N"
    Public Payment_Amount As Double = 0.0
    Public Vendor_Account_Set As String = Nothing
    Public TDS_Amount As Double = 0.0
    Public Total_Prepayment As Double = 0.0
    Public Apply_By As String = Nothing
    Public Apply_To As String = Nothing
    Public Level1_User_code As String = Nothing
    Public Level2_User_code As String = Nothing
    Public Level3_User_code As String = Nothing
    Public Level4_User_code As String = Nothing
    Public Level5_User_code As String = Nothing
    Public Debit_Account As String = Nothing
    Public Credit_Account As String = Nothing
    Public Balance_Amt As Double = 0.0
    Public Total_Applied_Amount As Double = 0.0
    Public Total_Security_Amount As Double = 0.0
    Public Transport_Id As String = Nothing
    Public FIFO_Balance As Double = 0.0
    Public QuickEntryNo As String = Nothing
    Public LoadOutNo As String = Nothing
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Route_NO As String = Nothing
    Public Route_Description As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Description As String = Nothing
    Public IsRecoCleared As String = Nothing
    Public IsChkReverse As String = Nothing
    Public Posted As String = "0"
    Public Loadout_No As String = Nothing
    Public Bank_Charges_Ac As String = Nothing
    Public Bank_Charges As Double = 0.0
    Public ArrTr As List(Of clsFarmerPaymentDetail) = Nothing
    Public objRemittance As clsRemittance

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public BASE_CURRENCY_CODE As String
    Public PAYMENT_AMOUNT_BASE_CURRENCY As Double = 0.0
    Public EXCHANGE_LOSS_AMT As Double = 0.0
    Public EXCHANGE_GAIN_AMT As Double = 0.0
    Public EXCHANGE_LOSS_ACCOUNT As String
    Public EXCHANGE_GAIN_ACCOUNT As String
    Public ConvRateOld As Decimal
    Public CForm_InvoiceNo As String = Nothing
    Public CFormRecd As String = "N"
    Public Applied_Payment As String = Nothing
    Public Applied_Balance As Double = 0.0
    Public Form_ID As String = ""

    Public EMP_CODE As String = Nothing
    Public PROJECT_CODE As String = Nothing
    Public Document_No As String = Nothing
    Public CHECK_PRINT As Integer = 0
    Public CHECK_CODE As String = Nothing
    Public Account_Payee As Integer = 0
    Public Advance_Against_Salary As Integer = 0
    Public Is_Security As Integer = 0
    Public Account_Payee_Name As String = Nothing
    '' Anubhooti 21-Aug-2014
    Public PurchaseOrder_No As String = Nothing
    Public Location_GL_Code As String = Nothing
    Public Against_PP_Detail_No As String = Nothing
    Public Against_PP_Detail_No_Advance_Payment As String = Nothing
    Public is_Opening As Boolean = False
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    '==shivani
    Public Loan_Code As String = Nothing
    Public Against_TDS_PAYMENT_No As String = String.Empty
    Public Payment_Process_Code As String = ""
#End Region


    Public Function SaveData(ByVal obj As clsFarmerPaymentHeader, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal isPosted As Boolean = False) As Boolean
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
        End If
        Try
            SaveData1(obj, isNewEntry, trans)
            If isPosted = False Then
                trans.Commit()
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveDataWithPaymentNo(ByVal obj As clsFarmerPaymentHeader, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal isPosted As Boolean = False) As clsFarmerPaymentHeader
        If trans Is Nothing OrElse trans.Connection Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
        End If
        Try
            SaveData1(obj, isNewEntry, trans)
            'If isPosted = False Then
            '    trans.Commit()
            'End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Function SaveData1(ByVal obj As clsFarmerPaymentHeader, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        If trans Is Nothing OrElse trans.Connection Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
        End If
        Dim isSaved As Boolean = True
        Try
            '--------Checks Whertrher Transaction Is Locked Or Not-----------
            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans)
            clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, obj.Payment_Date, trans)
            '----------------------------------------------------------------
            If clsCommon.myLen(obj.Payment_No) > 0 Then
                Dim isPosted As Integer = clsDBFuncationality.getSingleValue("Select Posted from TSPL_MP_PAY_HEAD Where Payment_No='" + obj.Payment_No + "'", trans)
                If isPosted = 1 Then
                    Throw New Exception("Document already posted")
                End If
            End If

            'clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_REMITTANCE Where Document_No='" + obj.Payment_No + "'", trans)

            Dim qry As String = "DELETE From TSPL_MP_PAY_DETAIL WHERE Payment_No ='" + obj.Payment_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Payment_Date", clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy hh:mm tt"))
            If obj.Payment_Post_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Payment_Post_Date", clsCommon.GetPrintDate(obj.Payment_Post_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Payment_Post_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Payment_Type", obj.Payment_Type)
            clsCommon.AddColumnsForChange(coll, "Farmer_Code", obj.Farmer_Code)
            clsCommon.AddColumnsForChange(coll, "Farmer_Name", obj.Farmer_Name)
            clsCommon.AddColumnsForChange(coll, "Remit_To", obj.Remit_To)
            clsCommon.AddColumnsForChange(coll, "Entry_Desc", obj.Entry_Desc)
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Narration", obj.Narration)
            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            clsCommon.AddColumnsForChange(coll, "CHECK_PRINT", obj.CHECK_PRINT)
            'clsCommon.AddColumnsForChange(coll, "CHECK_CODE", obj.CHECK_CODE, True)
            clsCommon.AddColumnsForChange(coll, "IsApplyDocAuto", obj.IsApplyDocAuto)
            clsCommon.AddColumnsForChange(coll, "CFormRecd", obj.CFormRecd)
            clsCommon.AddColumnsForChange(coll, "CForm_InvoiceNo", obj.CForm_InvoiceNo)
            '' Anubhooti 22-July-2014
            clsCommon.AddColumnsForChange(coll, "Account_Payee", obj.Account_Payee)
            clsCommon.AddColumnsForChange(coll, "Is_Security", obj.Is_Security)
            clsCommon.AddColumnsForChange(coll, "Account_Payee_Name", obj.Account_Payee_Name)
            '' Anubhooti 21-Aug-2014
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No, True)
            'clsCommon.AddColumnsForChange(coll, "Farmer_Invoice_No", obj.Against_PP_Detail_No, True)
            'clsCommon.AddColumnsForChange(coll, "Against_PP_Detail_No_Advance_Payment", obj.Against_PP_Detail_No_Advance_Payment, True)
            ''shivani
            clsCommon.AddColumnsForChange(coll, "Loan_Code", obj.Loan_Code, True)
            clsCommon.AddColumnsForChange(coll, "Payment_Process_Code", obj.Payment_Process_Code, True)
            clsCommon.AddColumnsForChange(coll, "Against_TDS_PAYMENT_No", obj.Against_TDS_PAYMENT_No, True)
            '' Anubhooti 07-Jan-2014 BM00000005309
            clsCommon.AddColumnsForChange(coll, "Location_GL_Code", obj.Location_GL_Code, True)
            '' Anubhooti 27-Mar-2015 (Advance Against Salary in case of Advance/On-Account)
            clsCommon.AddColumnsForChange(coll, "Advance_Against_Salary", obj.Advance_Against_Salary)
            If clsCommon.myLen(obj.Cheque_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "PDC_Cheque", obj.PDC_Cheque)
            If clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Payment_Amount", obj.Total_Prepayment - Math.Round(obj.TDS_Amount, 0, MidpointRounding.ToEven))
            Else
                clsCommon.AddColumnsForChange(coll, "Payment_Amount", obj.Payment_Amount)
            End If
            obj.Vendor_Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Acct_Set_Code from TSPL_MP_MASTER where MP_CODE ='" + obj.Farmer_Code + "'", trans))
            clsCommon.AddColumnsForChange(coll, "Vendor_Account_Set", obj.Vendor_Account_Set)
            clsCommon.AddColumnsForChange(coll, "TDS_Amount", Math.Round(obj.TDS_Amount, 0, MidpointRounding.ToEven))
            clsCommon.AddColumnsForChange(coll, "Total_Prepayment", obj.Total_Prepayment)
            clsCommon.AddColumnsForChange(coll, "Apply_By", obj.Apply_By)
            clsCommon.AddColumnsForChange(coll, "Apply_To", obj.Apply_To)
            clsCommon.AddColumnsForChange(coll, "Level1_User_code", obj.Level1_User_code)
            clsCommon.AddColumnsForChange(coll, "Level2_User_code", obj.Level2_User_code)
            clsCommon.AddColumnsForChange(coll, "Level3_User_code", obj.Level3_User_code)
            clsCommon.AddColumnsForChange(coll, "Level4_User_code", obj.Level4_User_code)
            clsCommon.AddColumnsForChange(coll, "Level5_User_code", obj.Level5_User_code)
            If (clsCommon.CompairString(obj.Payment_Type, "PY") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal) Then
                obj.Debit_Account = clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + obj.Farmer_Code + "' ", trans)

                If clsCommon.myLen(obj.Location_GL_Code) <= 0 Then
                    obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "')", trans))
                End If

                obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
            ElseIf clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal Then
                obj.Debit_Account = clsDBFuncationality.getSingleValue("select s.Advance_Account  from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + obj.Farmer_Code + "'", trans)

                If clsCommon.myCdbl(obj.Advance_Against_Salary) = 1 AndAlso (clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal) Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Salary,'') AS Advance_Against_Salary  from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + obj.Farmer_Code + "'", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill advance against salary account on vendor account set")
                    End If
                End If
                '============Commented By Rohit After Talked with Anubhuti and Amit Sir(09-Jul-2015)=====
                'obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
                '=====================================================================================
                '' Anubhooti 31-Mar-2015 (Receipt/Security Refund :If security is checked then security account will go on GL)
                If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Is_Security) = 1 Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.SECURITY_ACCOUNT,'') AS SECURITY_ACCOUNT from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + obj.Farmer_Code + "' ", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill security account on vendor account set.")
                    End If
                Else
                    '============Add By Rohit After Talked with Anubhuti and Amit Sir(09-Jul-2015)=====
                    obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
                    '=======================================================
                End If
            End If
            obj.Credit_Account = clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + obj.Bank_Code + "'", trans)
            If clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Debit_Account", obj.Credit_Account)
                clsCommon.AddColumnsForChange(coll, "Credit_Account", obj.Debit_Account)
            Else
                clsCommon.AddColumnsForChange(coll, "Debit_Account", obj.Debit_Account)
                clsCommon.AddColumnsForChange(coll, "Credit_Account", obj.Credit_Account)
            End If
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Applied_Amount", obj.Total_Applied_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Security_Amount", obj.Total_Security_Amount)
            clsCommon.AddColumnsForChange(coll, "Transport_Id", obj.Transport_Id)
            clsCommon.AddColumnsForChange(coll, "FIFO_Balance", obj.FIFO_Balance)
            clsCommon.AddColumnsForChange(coll, "QuickEntryNo", obj.QuickEntryNo)
            clsCommon.AddColumnsForChange(coll, "LoadOutNo", obj.LoadOutNo)
            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
            clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
            clsCommon.AddColumnsForChange(coll, "Route_NO", obj.Route_NO)
            clsCommon.AddColumnsForChange(coll, "Route_Description", obj.Route_Description)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Description", obj.Location_Description)
            clsCommon.AddColumnsForChange(coll, "IsRecoCleared", obj.IsRecoCleared)
            clsCommon.AddColumnsForChange(coll, "IsChkReverse", obj.IsChkReverse)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            If clsCommon.CompairString(obj.Payment_Type, "MI") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Loadout_No", obj.Loadout_No)
            End If
            clsCommon.AddColumnsForChange(coll, "Bank_Charges", obj.Bank_Charges)
            If obj.Bank_Charges > 0 Then
                obj.Bank_Charges_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CREDITACC from TSPL_BANK_MASTER Where BANK_CODE='" + obj.Bank_Code + "'", trans))
            End If
            clsCommon.AddColumnsForChange(coll, "Bank_Charges_Ac", obj.Bank_Charges_Ac)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", IIf(clsCommon.myLen(obj.CURRENCY_CODE) <= 0, objCommonVar.BaseCurrencyCode, obj.CURRENCY_CODE), True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", IIf(obj.ConvRate <= 0, 1, obj.ConvRate))


            If obj.ApplicableFrom IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)

            End If


            clsCommon.AddColumnsForChange(coll, "BASE_CURRENCY_CODE", IIf(clsCommon.myLen(obj.BASE_CURRENCY_CODE) <= 0, objCommonVar.BaseCurrencyCode, obj.BASE_CURRENCY_CODE), True)
            clsCommon.AddColumnsForChange(coll, "PAYMENT_AMOUNT_BASE_CURRENCY", obj.PAYMENT_AMOUNT_BASE_CURRENCY)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_AMT", obj.EXCHANGE_LOSS_AMT)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_AMT", obj.EXCHANGE_GAIN_AMT)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_ACCOUNT", obj.EXCHANGE_LOSS_ACCOUNT, True)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_ACCOUNT", obj.EXCHANGE_GAIN_ACCOUNT, True)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRateOld)
            '' End currencyconversion
            '''' code by priti for PJC entry
            'If clsCommon.myLen(obj.PROJECT_CODE) > 0 Then

            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PROJECT_CODE", obj.PROJECT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No, True)
            'End If
            '''' priti code ends here
            If clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Applied_Payment", obj.Applied_Payment)
            End If
            clsCommon.AddColumnsForChange(coll, "memorandum_amt", obj.memorndmamt)
            clsCommon.AddColumnsForChange(coll, "is_Opening", IIf(obj.is_Opening, 1, 0))
            If isNewEntry Then
                qry = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                Dim BankAcc As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                If (BankAcc.Length >= 3) Then
                    BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                    ''No need to check becaulse all acount will be now with location segment.
                    'If (IsNumeric(BankAcc)) Then
                    '    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                    'End If
                Else
                    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                End If

                Dim strPaymentType As String = clsDocType.FarmerPayment
                If clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PaymentReceiptTypeRunReceiptCounter, clsFixedParameterCode.PaymentReceiptTypeRunReceiptCounter, trans)) = 1 Then
                    strPaymentType = clsDocType.Receipt
                End If


                If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                    obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.Bank, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
                    obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.Cash, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
                    obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.PettyCash, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
                    obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.Others, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
                    obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.Others, BankAcc, True)
                Else
                    Throw New Exception("Plase set the Bank Type for Bank SETTLEMENT")
                End If
                clsCommon.AddColumnsForChange(coll, "Payment_No", obj.Payment_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_HEAD", OMInsertOrUpdate.Update, "Payment_No='" + obj.Payment_No + "'", trans)
            End If
            'If clsCommon.myLen(obj.Document_No) > 0 Then
            '    qry = "Update TSPL_PJC_EXPENSE_HEADER set Payment_No='" & obj.Payment_No & "',Posted='Y' ,Posting_Date='" & clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy hh:mm tt") & "' WHERE Document_No ='" + obj.Document_No + "'"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'End If
            ''----------------Remmitance ENtry------------------------
            'If clsCommon.myCdbl(obj.TDS_Amount) > 0 Then
            '    If clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Then
            '        clsRemittance.SaveData(objRemittance, obj.Payment_No, obj.Location_GL_Code, trans)
            '    End If
            'End If
            '--------------------------------------------------------
            isSaved = isSaved AndAlso clsFarmerPaymentDetail.SaveData(obj.Payment_No, obj.ArrTr, trans)
            '' update currency loss and gain in case of payment type entr
            'If obj.ConvRate <> 1 Then
            '    If obj.Payment_Type = "PY" Then
            '        Dim obj1 As New clsFarmerPaymentHeader
            '        Dim diff As Double = 0.0
            '        diff = obj.PAYMENT_AMOUNT_BASE_CURRENCY - clsFarmerPaymentDetail.GetAppliedAmountInBaseCurrency(obj.Payment_No, trans)
            '        If diff = 0 Then
            '            obj1.EXCHANGE_LOSS_AMT = 0
            '            obj1.EXCHANGE_GAIN_AMT = 0
            '        ElseIf diff > 0 Then
            '            obj1.EXCHANGE_LOSS_AMT = diff
            '            obj1.EXCHANGE_GAIN_AMT = 0
            '        Else
            '            obj1.EXCHANGE_LOSS_AMT = 0
            '            obj1.EXCHANGE_GAIN_AMT = -diff
            '        End If
            '        Dim coll1 As New Hashtable()
            '        clsCommon.AddColumnsForChange(coll1, "EXCHANGE_LOSS_AMT", obj1.EXCHANGE_LOSS_AMT)
            '        clsCommon.AddColumnsForChange(coll1, "EXCHANGE_GAIN_AMT", obj1.EXCHANGE_GAIN_AMT)
            '        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_MP_PAY_HEAD", OMInsertOrUpdate.Update, "Payment_No='" + obj.Payment_No + "'", trans)
            '    End If
            'Else
            '    Dim coll1 As New Hashtable()
            '    clsCommon.AddColumnsForChange(coll1, "EXCHANGE_LOSS_AMT", 0)
            '    clsCommon.AddColumnsForChange(coll1, "EXCHANGE_GAIN_AMT", 0)
            'End If

            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Payment_No, obj.arrCustomFields, trans)
            ' '' check for bankBookentry against ticket No:BM00000008469
            'If clsCommon.CompairString(obj.Payment_Type, "AD") <> CompairStringResult.Equal Then
            '    qry = "select count(ID) as Rec from TSPL_BANK_BOOK where SOURCEDOC_NO='" & obj.Payment_No & "' and DocType='Payment'"
            '    Dim totalRec As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            '    If totalRec <= 0 Then
            '        Throw New Exception("Payment No-" & obj.Payment_No & " could not sent to Bank Book")
            '    End If
            'End If

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As clsFarmerPaymentHeader
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsFarmerPaymentHeader
        Dim obj As clsFarmerPaymentHeader = Nothing


        Dim qry As String = "SELECT TSPL_MP_PAY_HEAD.memorandum_amt,TSPL_MP_PAY_HEAD.Payment_No,  TSPL_MP_PAY_HEAD.Payment_Date,  TSPL_MP_PAY_HEAD.Payment_Post_Date,  " & _
         " TSPL_MP_PAY_HEAD.Bank_Code, TSPL_MP_PAY_HEAD.Payment_Type, TSPL_MP_PAY_HEAD.Farmer_Code, TSPL_MP_PAY_HEAD.Farmer_Name, " & _
         " TSPL_MP_PAY_HEAD.Remit_To, TSPL_MP_PAY_HEAD.Entry_Desc, TSPL_MP_PAY_HEAD.Reference, TSPL_MP_PAY_HEAD.Narration, " & _
         " TSPL_MP_PAY_HEAD.Payment_Code, TSPL_MP_PAY_HEAD.Cheque_No, TSPL_MP_PAY_HEAD.Cheque_Date, TSPL_MP_PAY_HEAD.PDC_Cheque, TSPL_MP_PAY_HEAD.Payment_Amount, " & _
        " TSPL_MP_PAY_HEAD.Vendor_Account_Set, TSPL_MP_PAY_HEAD.TDS_Amount, TSPL_MP_PAY_HEAD.Total_Prepayment, " & _
         " TSPL_MP_PAY_HEAD.Apply_By, TSPL_MP_PAY_HEAD.Apply_To, TSPL_MP_PAY_HEAD.Posted, TSPL_MP_PAY_HEAD.Level1_User_code, " & _
        " TSPL_MP_PAY_HEAD.Level2_User_code, TSPL_MP_PAY_HEAD.Level3_User_code, TSPL_MP_PAY_HEAD.Level4_User_code, " & _
        " TSPL_MP_PAY_HEAD.Level5_User_code, TSPL_MP_PAY_HEAD.Comp_Code, TSPL_MP_PAY_HEAD.Debit_Account, TSPL_MP_PAY_HEAD.Credit_Account, " & _
         " TSPL_MP_PAY_HEAD.Balance_Amt, TSPL_MP_PAY_HEAD.Total_Applied_Amount, TSPL_MP_PAY_HEAD.Total_Security_Amount, TSPL_MP_PAY_HEAD.Transport_Id, TSPL_MP_PAY_HEAD.FIFO_Balance, " & _
         " TSPL_MP_PAY_HEAD.QuickEntryNo, TSPL_MP_PAY_HEAD.LoadOutNo, TSPL_MP_PAY_HEAD.Salesman_Code, TSPL_MP_PAY_HEAD.Salesman_Name, " & _
        " TSPL_MP_PAY_HEAD.Route_NO, TSPL_MP_PAY_HEAD.Route_Description, TSPL_MP_PAY_HEAD.Location_Code, " & _
         " TSPL_MP_PAY_HEAD.Location_Description, TSPL_MP_PAY_HEAD.IsRecoCleared, TSPL_MP_PAY_HEAD.IsChkReverse,Loadout_No, TSPL_MP_PAY_HEAD.Bank_Charges, TSPL_MP_PAY_HEAD.Bank_Charges_Ac,CFormRecd,CForm_InvoiceNo,TSPL_MP_PAY_HEAD.CURRENCY_CODE,TSPL_MP_PAY_HEAD.CONVRATE,TSPL_MP_PAY_HEAD.APPLICABLEFROM, " & _
         " TSPL_MP_PAY_HEAD.PAYMENT_AMOUNT_BASE_CURRENCY,EMP_CODE,PROJECT_CODE,TSPL_MP_PAY_HEAD.CHECK_PRINT,TSPL_MP_PAY_HEAD.CHECK_CODE, TSPL_MP_PAY_HEAD.Applied_Payment,TSPL_MP_PAY_HEAD.Account_Payee,TSPL_MP_PAY_HEAD.PurchaseOrder_No,TSPL_MP_PAY_HEAD.Loan_Code,ISNULL(TSPL_MP_PAY_HEAD.Location_GL_Code,'') As Location_GL_Code,TSPL_MP_PAY_HEAD.Is_Security,TSPL_MP_PAY_HEAD.Account_Payee_Name,ISNULL(TSPL_MP_PAY_HEAD.Advance_Against_Salary,0) AS Advance_Against_Salary,TSPL_MP_PAY_HEAD.is_Opening,TSPL_MP_PAY_HEAD.EXCHANGE_LOSS_AMT,TSPL_MP_PAY_HEAD.EXCHANGE_GAIN_AMT,TSPL_MP_PAY_HEAD.EXCHANGE_LOSS_ACCOUNT,TSPL_MP_PAY_HEAD.EXCHANGE_GAIN_ACCOUNT,TSPL_MP_PAY_HEAD.Against_TDS_PAYMENT_No,TSPL_MP_PAY_HEAD.Payment_Process_Code FROM TSPL_MP_PAY_HEAD " & _
        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_MP_PAY_HEAD.Bank_Code" & _
        " where  2=2"
        Dim whrclas As String = " "
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(trans)
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, trans)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrclas = " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, trans)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                whrclas = " AND TSPL_MP_PAY_HEAD.Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        qry += " " + whrclas + ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MP_PAY_HEAD.Payment_No=(select MIN(Payment_No) from TSPL_MP_PAY_HEAD TSPL_MP_PAY_HEAD LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_MP_PAY_HEAD.Bank_Code Where 1=1 " + whrclas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MP_PAY_HEAD.Payment_No=(select Max(Payment_No) from TSPL_MP_PAY_HEAD TSPL_MP_PAY_HEAD LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_MP_PAY_HEAD.Bank_Code Where  1=1 " + whrclas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_MP_PAY_HEAD.Payment_No=(select Min(Payment_No) from TSPL_MP_PAY_HEAD TSPL_MP_PAY_HEAD LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_MP_PAY_HEAD.Bank_Code where Payment_No > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MP_PAY_HEAD.Payment_No=(select Max(Payment_No) from TSPL_MP_PAY_HEAD TSPL_MP_PAY_HEAD LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_MP_PAY_HEAD.Bank_Code where Payment_No < '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MP_PAY_HEAD.Payment_No='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsFarmerPaymentHeader()
            obj.Payment_No = clsCommon.myCstr(dt.Rows(0)("Payment_No"))
            obj.Payment_Date = clsCommon.myCDate(dt.Rows(0)("Payment_Date"))
            If obj.Payment_Post_Date.HasValue Then
                obj.Payment_Post_Date = clsCommon.myCDate(dt.Rows(0)("Payment_Post_Date"))
            End If

            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Payment_Type = clsCommon.myCstr(dt.Rows(0)("Payment_Type"))
            obj.memorndmamt = clsCommon.myCstr(dt.Rows(0)("memorandum_amt"))
            '' Anubhooti 22-July-2014
            obj.Account_Payee = clsCommon.myCdbl(dt.Rows(0)("Account_Payee"))
            '' Anubhooti 21-Aug-2014
            obj.PurchaseOrder_No = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
            '==shivani
            obj.Loan_Code = clsCommon.myCstr(dt.Rows(0)("Loan_Code"))
            obj.Payment_Process_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Process_Code"))
            obj.Against_TDS_PAYMENT_No = clsCommon.myCstr(dt.Rows(0)("Against_TDS_PAYMENT_No"))
            '' Anubhooti 24-Sep-2014
            obj.Is_Security = clsCommon.myCdbl(dt.Rows(0)("Is_Security"))
            '' Anubhooti 07-Jan-2014 BM00000005309
            obj.Location_GL_Code = clsCommon.myCstr(dt.Rows(0)("Location_GL_Code"))

            obj.Account_Payee_Name = clsCommon.myCstr(dt.Rows(0)("Account_Payee_Name"))
            ''Anubhooti 27-Mar-2015
            obj.Advance_Against_Salary = clsCommon.myCdbl(dt.Rows(0)("Advance_Against_Salary"))
            obj.Farmer_Code = clsCommon.myCstr(dt.Rows(0)("Farmer_Code"))
            obj.Farmer_Name = clsCommon.myCstr(dt.Rows(0)("Farmer_Name"))
            obj.Remit_To = clsCommon.myCstr(dt.Rows(0)("Remit_To"))
            obj.Entry_Desc = clsCommon.myCstr(dt.Rows(0)("Entry_Desc"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Narration = clsCommon.myCstr(dt.Rows(0)("Narration"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            ''richa 
            obj.EXCHANGE_LOSS_AMT = clsCommon.myCdbl(dt.Rows(0)("EXCHANGE_LOSS_AMT"))
            obj.EXCHANGE_GAIN_AMT = clsCommon.myCdbl(dt.Rows(0)("EXCHANGE_GAIN_AMT"))
            obj.EXCHANGE_LOSS_ACCOUNT = clsCommon.myCstr(dt.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
            obj.EXCHANGE_GAIN_ACCOUNT = clsCommon.myCstr(dt.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))

            ''--------------------
            If clsCommon.myLen(dt.Rows(0)("Cheque_Date")) > 0 Then
                obj.Cheque_Date = clsCommon.myCstr(dt.Rows(0)("Cheque_Date"))
            End If
            obj.PDC_Cheque = clsCommon.myCstr(dt.Rows(0)("PDC_Cheque"))
            obj.Payment_Amount = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
            obj.TDS_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Amount"))
            obj.Total_Prepayment = clsCommon.myCdbl(dt.Rows(0)("Total_Prepayment"))
            obj.Apply_By = clsCommon.myCstr(dt.Rows(0)("Apply_By"))
            obj.Level1_User_code = clsCommon.myCstr(dt.Rows(0)("Level1_User_code"))
            obj.Level2_User_code = clsCommon.myCstr(dt.Rows(0)("Level2_User_code"))
            obj.Level3_User_code = clsCommon.myCstr(dt.Rows(0)("Level3_User_code"))
            obj.Level4_User_code = clsCommon.myCstr(dt.Rows(0)("Level4_User_code"))
            obj.Level5_User_code = clsCommon.myCstr(dt.Rows(0)("Level5_User_code"))
            obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Debit_Account"))
            obj.Credit_Account = clsCommon.myCstr(dt.Rows(0)("Credit_Account"))
            obj.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("Balance_Amt"))
            obj.Total_Applied_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Applied_Amount"))
            obj.Total_Security_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Security_Amount"))
            obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
            obj.FIFO_Balance = clsCommon.myCdbl(dt.Rows(0)("FIFO_Balance"))
            obj.QuickEntryNo = clsCommon.myCstr(dt.Rows(0)("QuickEntryNo"))
            obj.LoadOutNo = clsCommon.myCstr(dt.Rows(0)("LoadOutNo"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Emp_Name from TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + obj.Salesman_Code + "'", trans))
            obj.Route_NO = clsCommon.myCstr(dt.Rows(0)("Route_NO"))
            obj.Route_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_Desc from TSPL_ROUTE_MASTER WHERE Route_No='" + obj.Route_NO + "'", trans))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER WHERE Location_Code='" + obj.Location_Code + "'", trans))
            obj.IsRecoCleared = clsCommon.myCstr(dt.Rows(0)("IsRecoCleared"))
            obj.IsChkReverse = clsCommon.myCstr(dt.Rows(0)("IsChkReverse"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            obj.Loadout_No = clsCommon.myCstr(dt.Rows(0)("Loadout_No"))
            obj.Bank_Charges_Ac = clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))
            obj.Bank_Charges = clsCommon.myCdbl(dt.Rows(0)("Bank_Charges"))
            'obj.Against_PP_Detail_No = clsCommon.myCstr(dt.Rows(0)("Against_PP_Detail_No"))
            'obj.Against_PP_Detail_No_Advance_Payment = clsCommon.myCstr(dt.Rows(0)("Against_PP_Detail_No_Advance_Payment"))
            obj.CForm_InvoiceNo = clsCommon.myCstr(dt.Rows(0)("CForm_InvoiceNo"))
            obj.CFormRecd = clsCommon.myCstr(dt.Rows(0)("CFormRecd"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.PROJECT_CODE = clsCommon.myCstr(dt.Rows(0)("PROJECT_CODE"))
            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            obj.PAYMENT_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(dt.Rows(0)("PAYMENT_AMOUNT_BASE_CURRENCY"))
            '' END CURRENCYCONVERSION
            If clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                obj.Applied_Payment = clsCommon.myCstr(dt.Rows(0)("Applied_Payment"))
                obj.Balance_Amt = clsFarmerPaymentHeader.GetBalance(obj.Applied_Payment, obj.Payment_No, trans)
            End If
            obj.CHECK_PRINT = clsCommon.myCdbl(dt.Rows(0)("CHECK_PRINT"))
            obj.CHECK_CODE = clsCommon.myCstr(dt.Rows(0)("CHECK_CODE"))
            'clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.is_Opening = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Opening")) = 1, True, False)
            qry = "select * from (SELECT  case when TSPL_MP_PAY_HEAD.Payment_Type ='SR' then  convert(varchar,TSPL_MP_PAY_HEAD.Payment_Date,103) else convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) end as Payment_Date,TSPL_MP_PAY_DETAIL.Payment_No, TSPL_MP_PAY_DETAIL.Payment_Line_No, TSPL_MP_PAY_DETAIL.Apply, " & _
          " TSPL_MP_PAY_DETAIL.Payment_Type, TSPL_MP_PAY_DETAIL.Document_No, TSPL_MP_PAY_DETAIL.Vendor_Invoice_No, " & _
             " Case When ISNULL(Against_POInvoice_No,'')<>'' Then Against_POInvoice_No When ISNULL(Against_PurchaseReturn_No,'')<> '' Then Against_PurchaseReturn_No Else TSPL_MP_PAY_DETAIL.Document_No End as PurchaseInvoice," & _
             " TSPL_MP_PAY_DETAIL.Pending_Balance, TSPL_MP_PAY_DETAIL.Applied_Amount,TSPL_MP_PAY_DETAIL.Security_Amount, TSPL_MP_PAY_DETAIL.Original_Invoice_Amt, " & _
             " Case When ISNULL(Against_POInvoice_No,'')<>'' Then (Select convert(varchar,PI_Date,103)  from TSPL_PI_HEAD where PI_No =Against_POInvoice_No)  When ISNULL(Against_PurchaseReturn_No,'')<> '' Then (Select convert(varchar,PR_Date,103) from TSPL_PR_HEAD where PR_No  =Against_PurchaseReturn_No ) Else convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) End as DocumentDate, " & _
             " TSPL_MP_PAY_DETAIL.TDS_Amount, TSPL_MP_PAY_DETAIL.Account_Code, "
            '' " Case When TSPL_MP_PAY_HEAD.Payment_Type='MI' Then Net_Balance Else TSPL_VENDOR_INVOICE_HEAD.balance_amt - ISNULL((Select SUM(Applied_Amount)+coalesce(SUM(Security_Amount),0) from TSPL_MP_PAY_DETAIL Where TSPL_MP_PAY_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No AND Post NOT IN ('1','P')),0) END as [Net_Balance], " & _
            qry += " Case When TSPL_MP_PAY_HEAD.Payment_Type='MI' Then Net_Balance Else TSPL_VENDOR_INVOICE_HEAD.Document_Total- isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount ,0) - ISNULL((Select SUM(Applied_Amount)+coalesce(SUM(Security_Amount),0) from TSPL_MP_PAY_DETAIL Where TSPL_MP_PAY_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No AND Post NOT IN ('1','P')),0) END as [Net_Balance], " & _
            " TSPL_MP_PAY_DETAIL.Description, TSPL_MP_PAY_DETAIL.Remarks, TSPL_MP_PAY_DETAIL.Comment, TSPL_MP_PAY_DETAIL.ESI_WCT_Percentage, " & _
             " TSPL_MP_PAY_DETAIL.Post, TSPL_MP_PAY_DETAIL.Settlement_code, TSPL_MP_PAY_DETAIL.Settlement_Description,EXPENSE_CODE,TSPL_MP_PAY_DETAIL.ConvRateOld," + _
             " TSPL_MP_PAY_DETAIL.Cost_Center_Fin_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,TSPL_MP_PAY_DETAIL.Hirerachy_Level_Code,TSPL_HIRERACHY_LEVEL_MASTER.Description as Hirerachy_Level_Name " & _
             " FROM TSPL_MP_PAY_DETAIL   left join TSPL_MP_PAY_HEAD on TSPL_MP_PAY_HEAD.Payment_No =TSPL_MP_PAY_DETAIL.Payment_No" + _
             " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_MP_PAY_DETAIL.Cost_Center_Fin_Code " + _
             " LEFT OUTER JOIN TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE=TSPL_MP_PAY_DETAIL.Hirerachy_Level_Code " + _
             " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_MP_PAY_DETAIL.Document_No " & _
             " WHERE TSPL_MP_PAY_DETAIL.Payment_No = '" + obj.Payment_No + "')  as final order by convert(date,Payment_Date,103)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrTr = New List(Of clsFarmerPaymentDetail)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsFarmerPaymentDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsFarmerPaymentDetail()
                    objTr.Payment_Line_No = clsCommon.myCdbl(dr("Payment_Line_No"))
                    objTr.Payment_No = clsCommon.myCstr(dr("Payment_No"))
                    objTr.PaymentDate = clsCommon.myCstr(dr("Payment_Date"))
                    objTr.DocumentDate = clsCommon.myCstr(dr("DocumentDate"))
                    objTr.Apply = clsCommon.myCstr(dr("Apply"))
                    objTr.Payment_Type = clsCommon.myCstr(dr("Payment_Type"))
                    objTr.Vendor_Invoice_No = clsCommon.myCstr(dr("Vendor_Invoice_No"))
                    objTr.PurchaseInvoice = clsCommon.myCstr(dr("PurchaseInvoice"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Pending_Balance = clsCommon.myCdbl(dr("Pending_Balance"))
                    objTr.Applied_Amount = clsCommon.myCdbl(dr("Applied_Amount"))
                    objTr.Security_Amount = clsCommon.myCdbl(dr("Security_Amount"))
                    objTr.Original_Invoice_Amt = clsCommon.myCdbl(dr("Original_Invoice_Amt"))
                    objTr.TDS_Amount = clsCommon.myCdbl(dr("TDS_Amount"))
                    objTr.Net_Balance = clsCommon.myCdbl(dr("Net_Balance"))
                    objTr.Account_Code = clsCommon.myCstr(dr("Account_Code"))
                    objTr.Description = clsCommon.myCstr(dr("Description"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Comment = clsCommon.myCstr(dr("Comment"))
                    objTr.Post = clsCommon.myCstr(dr("Post"))
                    objTr.Settlement_code = clsCommon.myCdbl(dr("Settlement_code"))
                    objTr.Settlement_Description = clsCommon.myCstr(dr("Settlement_Description"))
                    objTr.EXPENSE_CODE = clsCommon.myCstr(dr("EXPENSE_CODE"))
                    objTr.ConvRateOld = clsCommon.myCdbl(dr("ConvRateOld"))
                    objTr.Cost_Center_Fin_Code = clsCommon.myCstr(dr("Cost_Center_Fin_Code"))
                    objTr.Cost_Center_Fin_Name = clsCommon.myCstr(dr("Cost_Center_Fin_Name"))
                    objTr.Hirerachy_Level_Code = clsCommon.myCstr(dr("Hirerachy_Level_Code"))
                    objTr.Hirerachy_Level_Name = clsCommon.myCstr(dr("Hirerachy_Level_Name"))
                    obj.ArrTr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function GetBalance(ByVal strAppliedPayment As String, ByVal strPaymentNo As String, ByVal trans As SqlTransaction) As Double
        Try
            ''richa agarwal against ticket no BM00000008630 on 07-Jan-2016
            '    Dim qry As String = "Select [Bal Amt] from (" & _
            '" Select Payment_No as [Code], Entry_Desc as [Description], Payment_Date as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount as [Payment Amt], Balance_Amt-ISNULL((Select SUM(Payment_Amount) from TSPL_MP_PAY_HEAD PH WHERE PH.Posted<>'1' AND PH.Payment_Type='AD' AND PH.Farmer_Code=TSPL_MP_PAY_HEAD.Farmer_Code AND PH.Applied_Payment=TSPL_MP_PAY_HEAD.Payment_No AND PH.Payment_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_MP_PAY_HEAD WHERE Posted='1' AND Payment_Type IN ('AV','OA') AND Payment_No <> '" + strPaymentNo + "'" & _
            '" ) Final WHERE Code='" + strAppliedPayment + "'"
            Dim qry As String = "Select [Bal Amt] from (" & _
        " Select Payment_No as [Code], Entry_Desc as [Description], Payment_Date as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount+isnull(TDS_Amount ,0) as [Payment Amt], Balance_Amt+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_MP_PAY_HEAD PH WHERE PH.Posted<>'1' AND PH.Payment_Type='AD' AND PH.Farmer_Code=TSPL_MP_PAY_HEAD.Farmer_Code AND PH.Applied_Payment=TSPL_MP_PAY_HEAD.Payment_No AND PH.Payment_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_MP_PAY_HEAD WHERE Posted='1' AND Payment_Type IN ('AV','OA') AND Payment_No <> '" + strPaymentNo + "'" & _
        " ) Final WHERE Code='" + strAppliedPayment + "'"
            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetBankBalance(ByVal DocNo As String, ByVal Doc_Date As Date, ByVal Bank_Code As String, ByVal Location_Code As String, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal isCurrentDocNOIncluded As Boolean = False) As Decimal
        Dim Bank_Balance As Decimal = 0
        Try
            ''richa agarwal 03 Aug,2016 changes in query to find bank balance
            'Dim qry As String = clsBankReco.GetBankBookQuery(objCommonVar.CurrFiscalStartDate, Doc_Date, "'" & Bank_Code & "'", "'" & Location_Code & "'", "''", "Y", "", "", "", False, trans)
            Dim qryForPosted As String = clsBankReco.GetBankBookQuery(objCommonVar.CurrFiscalStartDate, Doc_Date, "'" & Bank_Code & "'", "'" & Location_Code & "'", "''", "Y", "", "", "", False, trans)
            Dim qryForUnPosted As String = clsBankReco.GetBankBookQuery(objCommonVar.CurrFiscalStartDate, Doc_Date, "'" & Bank_Code & "'", "'" & Location_Code & "'", "''", "N", "", "", "", False, trans)
            Dim qryForForwardingEntries As String = clsBankReco.GetBankBookQueryToCalculateForwardingDocAmt(objCommonVar.CurrFiscalStartDate, Doc_Date, "'" & Bank_Code & "'", "'" & Location_Code & "'", "''", "", "", "", "", False, trans)
            Dim qry As String = qryForPosted + Environment.NewLine + "union All " + Environment.NewLine + qryForUnPosted + "union All " + Environment.NewLine + qryForForwardingEntries
            qry = "SELECT (SUM(Debit_Amount)-SUM(Credit_Amount)+SUM(BalAmt)) AS Closing_Balance   FROM (" + qry + ")POP where 1=1 "
            If clsCommon.myLen(DocNo) > 0 And isCurrentDocNOIncluded = False Then
                qry += "  and isnull(POP.DocNo,'')<>'" & DocNo & "' "
            End If
            qry += " GROUP BY BANK_CODE ORDER BY  BANK_CODE"
            ''-------------------
            Bank_Balance = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            ''richa agarwal 01 sep, 2016
            If Bank_Balance <= 0 Then
                Dim qryForbalanceAmountForPayment As String = clsBankReco.GetAmountforbackdateentry(Doc_Date, "'" & Bank_Code & "'", trans)
                Bank_Balance = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qryForbalanceAmountForPayment, trans))
            End If
            Bank_Balance = clsCommon.myFormat(Bank_Balance)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return Bank_Balance
    End Function

    Public Shared Function UpdateBalance(ByVal strAppliedPayment As String, ByVal dblAmtToBeDeduct As Double, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "Update TSPL_MP_PAY_HEAD SET Balance_Amt=Balance_Amt-" + clsCommon.myCstr(dblAmtToBeDeduct) + " WHERE Payment_No='" + strAppliedPayment + "'"
            clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function fundelete(ByVal strPaymentType As String, ByVal strPaymentNo As String, ByVal strVendorCode As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            Dim obj As clsFarmerPaymentHeader
            If clsCommon.myLen(strPaymentNo) > 0 Then
                obj = clsFarmerPaymentHeader.GetData(strPaymentNo, NavigatorType.Current)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Payment_No) > 0) Then
                    trans = clsDBFuncationality.GetTransactin()
                    If clsCommon.myCdbl(obj.Posted) = 1 Then
                        Throw New Exception("Document is already posted, Can not be delete.")
                    End If
                Else
                    Throw New Exception("Document not found to delete.")
                End If
                '--------Checks Whertrher Transaction Is Locked Or Not-----------
                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans)
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, obj.Payment_Date, trans)
                '----------------------------------------------------------------
            Else
                Throw New Exception("Document not found to delete.")
            End If

            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_MP_PAY_DETAIL_DELETE", New SqlParameter("@paymentno", strPaymentNo))
            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_MP_PAY_HEAD_DELETE", New SqlParameter("@paymentno", strPaymentNo))
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_REMITTANCE where Document_No='" + strPaymentNo + "'", trans)
            clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Payment_No, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CheckGLAccountType(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = Nothing
        qry = "select TSPL_ACCOUNT_MAIN_GROUPS.Group_Type from TSPL_GL_ACCOUNTS" & _
                " left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code" & _
                " left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code" & _
                " left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code" & _
                " left outer join TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code" & _
                " where TSPL_GL_ACCOUNTS.Account_Code='" + clsCommon.myCstr(strcode) + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

    End Function
    Public Shared Function funUpdateInvoice(ByVal PaymentType As String, ByVal PaymentNo As String, ByVal VendorCode As String, ByVal InvoiceNo As String, ByVal DocNo As String, ByVal trans As SqlTransaction)
        Try
            If clsCommon.CompairString(PaymentType, "PY") = CompairStringResult.Equal Then
                Dim BalAmt As Decimal
                Dim Qry As String = "Select TSPL_MP_PAY_DETAIL.Applied_Amount, TSPL_MP_PAY_DETAIL.Net_Balance  from TSPL_MP_PAY_DETAIL Left Outer Join TSPL_MP_PAY_HEAD ON  TSPL_MP_PAY_DETAIL.Payment_No=TSPL_MP_PAY_HEAD.Payment_No where TSPL_MP_PAY_HEAD.Farmer_Code = '" + VendorCode + "' and TSPL_MP_PAY_DETAIL.Vendor_Invoice_No = '" + InvoiceNo + "' AND TSPL_MP_PAY_DETAIL.Payment_No = '" + PaymentNo + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    BalAmt = clsCommon.myCdbl(dt.Rows(0)("Applied_Amount")) + clsCommon.myCdbl(dt.Rows(0)("Net_Balance"))
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = Balance_Amt - " + clsCommon.myCstr(BalAmt) + " where Farmer_Code = '" + VendorCode + "' and Vendor_Invoice_No = '" + InvoiceNo + "' AND Document_No='" + DocNo + "'", trans)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function IsValidVendorForRequitionItem(ByVal strReqNo As String, ByVal strICode As String, ByVal strVendorCode As String) As Boolean
        Dim qry As String = "select 1 from TSPL_MP_PAY_DETAIL where Payment_No ='" + strReqNo + "' and Item_Code='" + strICode + "' and Farmer_Code not in ('','" + strVendorCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function Create_Security_Credit_Note(ByVal strDocNo As String, ByVal vspcode As String, ByVal obj As clsFarmerPaymentHeader, ByVal trans As SqlTransaction) As Boolean
        ' Dim DtJoint As DataTable = clsDBFuncationality.GetDataTable("select Farmer_Code,Farmer_Name from TSPL_VENDOR_MASTER where Farmer_Code=(select Joint_Name from tspl_vendor_Master where Farmer_Code='" & obj.VSP_CODE & "')", trans)
        Dim qry As String = ""
        Dim issaved As Boolean = True

        Dim strICode As String = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel("PYMT-NEW", "TSPL_MP_PAY_HEAD", "Payment_No", obj.Document_No, trans)
        If isResult = False Then
            ' trans.Commit()
            Return False
        End If
        Dim Farmer_Name As String = clsDBFuncationality.getSingleValue("select Farmer_Name from TSPL_VENDOR_MASTER where form_type='VSP' and Farmer_Code='" & obj.Farmer_Code & "'", trans)
        For Each objTr As clsFarmerPaymentDetail In obj.ArrTr
            If clsCommon.myLen(objTr.Vendor_Invoice_No) > 0 And clsCommon.CompairString(objTr.Vendor_Invoice_No, strDocNo) = CompairStringResult.Equal Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.Security_Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT_New", OMInsertOrUpdate.Update, "Item_Code='" + strICode + "' and Source_Doc_No='" + objTr.Vendor_Invoice_No + "' and Trans_Type='Payment Entry'", trans)

            End If
        Next




        For Each objPIDetail As clsFarmerPaymentDetail In obj.ArrTr
            If clsCommon.myLen(objPIDetail.Vendor_Invoice_No) > 0 And clsCommon.CompairString(objPIDetail.Vendor_Invoice_No, strDocNo) = CompairStringResult.Equal Then
                Dim Loc_Code As String = clsDBFuncationality.getSingleValue("SELECT Loc_Code FROM TSPL_VENDOR_INVOICE_HEAD where Document_No='" & objPIDetail.Document_No & "'", trans)
                obj.Location_Code = Loc_Code
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy")
                'objVendorInvHead.Farmer_Code = obj.Farmer_Code 'DtJoint.Rows(0)("Farmer_Code")
                'objVendorInvHead.Farmer_Name = Farmer_Name 'DtJoint.Rows(0)("Farmer_Name")
                objVendorInvHead.Vendor_Invoice_No = objPIDetail.Vendor_Invoice_No
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy")
                objVendorInvHead.loc_code = obj.Location_Code
                'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Farmer_Code ='" + obj.Farmer_Code + "'", trans)) 'DtJoint.Rows(0)("Farmer_Code")
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Farmer_Code) 'DtJoint.Rows(0)("Farmer_Name")
                End If

                objVendorInvHead.Document_Type = "C" ''For Purchase Invoice Type

                objVendorInvHead.On_Hold = False
                'Dim srndate As String = ""
                'Dim srncode As String = ""
                'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
                '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                '        Dim query As String = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + objTr.SRN_CODE + "' "
                '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"))
                '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
                '    End If
                'Next



                'objVendorInvHead.Description = "Incentive of Vendor " + obj.VSP_CODE 'DtJoint.Rows(0)("Farmer_Code") + "/" + DtJoint.Rows(0)("Farmer_Name") + " .Against PO Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
                objVendorInvHead.Due_Date = obj.Payment_Date
                objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
                objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
                objVendorInvHead.Amount_Less_Discount = objPIDetail.Security_Amount 'obj.Incentive
                objVendorInvHead.Document_Total = objPIDetail.Security_Amount 'obj.Incentive
                objVendorInvHead.Balance_Amt = objPIDetail.Security_Amount 'obj.Incentive
                'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
                objVendorInvHead.RefDocNo = objPIDetail.Vendor_Invoice_No
                objVendorInvHead.Against_MillkPurchaseInvoice_No = objPIDetail.Vendor_Invoice_No
                objVendorInvHead.RefDocType = "MI-PY"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Incentive_Account,Security_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Location_Code, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Location_Code, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If



                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                Dim ii As Integer = 0
                Dim isFirstTime As Boolean = True
                ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
                'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                objVendorInvHead.Total_Landed_Amt = 0



                Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Security_ACCount"))
                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Location_Code, trans)
                Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                objVendorInvDetail.Amount = (clsCommon.myCdbl(objPIDetail.Security_Amount))
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = (clsCommon.myCdbl(objPIDetail.Security_Amount))
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = (clsCommon.myCdbl(objPIDetail.Security_Amount))
                objVendorInvDetail.Landed_Amount = (clsCommon.myCdbl(objPIDetail.Security_Amount))
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                objVendorInvHead.Description = "Security Credit Note Against Payment No [" & obj.Payment_No & "] and AP Invoice No [" & objPIDetail.Vendor_Invoice_No & "]"

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
                'sQuery = "update tspl_Milk_srn_Head set is_Incentive_Created='Y' where Doc_Code='" & objPIDetail.SRN_CODE & "'"
                'clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                ''End of Fill Vendor Invoice Detail Data
                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If

                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                'objVendorInvHead.ConvRate = 1applicable
                objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy") 'obj.DOC_DATE
                ''end multicurrency

                issaved = issaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                issaved = issaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.Payment_Date)
                Dim sQuery As String = "Update TSPL_MP_PAY_DETAIL set Ap_Invoice_no='" & objVendorInvHead.Document_No & "' where payment_no='" & obj.Payment_No & "' and vendor_Invoice_No= '" & objPIDetail.Vendor_Invoice_No & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            End If
        Next
        Return issaved
    End Function

    Public Shared Function Create_Security_Debit_Note(ByVal strDocNo As String, ByVal vspcode As String, ByVal obj As clsFarmerPaymentHeader, ByVal trans As SqlTransaction) As Boolean
        ' Dim DtJoint As DataTable = clsDBFuncationality.GetDataTable("select Farmer_Code,Farmer_Name from TSPL_VENDOR_MASTER where Farmer_Code=(select Joint_Name from tspl_vendor_Master where Farmer_Code='" & obj.VSP_CODE & "')", trans)
        Dim qry As String = ""
        Dim issaved As Boolean = True

        Dim strICode As String = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel("PYMT-NEW", "TSPL_MP_PAY_HEAD", "Payment_No", obj.Document_No, trans)
        If isResult = False Then
            ' trans.Commit()
            Return False
        End If
        Dim Farmer_Name As String = clsDBFuncationality.getSingleValue("select Farmer_Name from TSPL_VENDOR_MASTER where form_type='VSP' and Farmer_Code='" & obj.Farmer_Code & "'", trans)
        For Each objTr As clsFarmerPaymentDetail In obj.ArrTr
            If clsCommon.myLen(objTr.Vendor_Invoice_No) > 0 And clsCommon.CompairString(objTr.Vendor_Invoice_No, strDocNo) = CompairStringResult.Equal Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.Security_Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT_New", OMInsertOrUpdate.Update, "Item_Code='" + strICode + "' and Source_Doc_No='" + objTr.Vendor_Invoice_No + "' and Trans_Type='Payment Entry'", trans)

            End If
        Next




        For Each objPIDetail As clsFarmerPaymentDetail In obj.ArrTr
            If clsCommon.myLen(objPIDetail.Vendor_Invoice_No) > 0 And clsCommon.CompairString(objPIDetail.Vendor_Invoice_No, strDocNo) = CompairStringResult.Equal Then
                Dim Loc_Code As String = clsDBFuncationality.getSingleValue("SELECT Loc_Code FROM TSPL_VENDOR_INVOICE_HEAD where Document_No='" & objPIDetail.Document_No & "'", trans)
                obj.Location_Code = Loc_Code
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy")
                'objVendorInvHead.Farmer_Code = obj.Farmer_Code 'DtJoint.Rows(0)("Farmer_Code")
                'objVendorInvHead.Farmer_Name = Farmer_Name 'DtJoint.Rows(0)("Farmer_Name")
                objVendorInvHead.Vendor_Invoice_No = objPIDetail.Vendor_Invoice_No
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy")
                objVendorInvHead.loc_code = obj.Location_Code
                'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Farmer_Code ='" + obj.Farmer_Code + "'", trans)) 'DtJoint.Rows(0)("Farmer_Code")
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Farmer_Code) 'DtJoint.Rows(0)("Farmer_Name")
                End If

                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type

                objVendorInvHead.On_Hold = False
                'Dim srndate As String = ""
                'Dim srncode As String = ""
                'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
                '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                '        Dim query As String = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + objTr.SRN_CODE + "' "
                '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MMM/yyyy"))
                '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
                '    End If
                'Next



                'objVendorInvHead.Description = "Incentive of Vendor " + obj.VSP_CODE 'DtJoint.Rows(0)("Farmer_Code") + "/" + DtJoint.Rows(0)("Farmer_Name") + " .Against PO Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
                objVendorInvHead.Due_Date = obj.Payment_Date
                objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
                objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
                objVendorInvHead.Amount_Less_Discount = objPIDetail.Security_Amount 'obj.Incentive
                objVendorInvHead.Document_Total = objPIDetail.Security_Amount 'obj.Incentive
                objVendorInvHead.Balance_Amt = objPIDetail.Security_Amount 'obj.Incentive
                'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
                objVendorInvHead.RefDocNo = objPIDetail.Vendor_Invoice_No
                objVendorInvHead.Against_MillkPurchaseInvoice_No = objPIDetail.Vendor_Invoice_No
                objVendorInvHead.RefDocType = "MI-PY"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Incentive_Account,Security_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Location_Code, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Location_Code, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If



                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                Dim ii As Integer = 0
                Dim isFirstTime As Boolean = True
                ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
                'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                objVendorInvHead.Total_Landed_Amt = 0



                Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Security_ACCount"))
                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Location_Code, trans)
                Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                objVendorInvDetail.Amount = (clsCommon.myCdbl(objPIDetail.Security_Amount))
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = (clsCommon.myCdbl(objPIDetail.Security_Amount))
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = (clsCommon.myCdbl(objPIDetail.Security_Amount))
                objVendorInvDetail.Landed_Amount = (clsCommon.myCdbl(objPIDetail.Security_Amount))
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                objVendorInvHead.Description = "Security Credit Note Against Payment No [" & obj.Payment_No & "] and AP Invoice No [" & objPIDetail.Vendor_Invoice_No & "]"

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
                'sQuery = "update tspl_Milk_srn_Head set is_Incentive_Created='Y' where Doc_Code='" & objPIDetail.SRN_CODE & "'"
                'clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                ''End of Fill Vendor Invoice Detail Data
                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If

                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                'objVendorInvHead.ConvRate = 1applicable
                objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy") 'obj.DOC_DATE
                ''end multicurrency

                issaved = issaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                issaved = issaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.Payment_Date)
                Dim sQuery As String = "Update TSPL_MP_PAY_DETAIL set Ap_Invoice_no='" & objVendorInvHead.Document_No & "' where payment_no='" & obj.Payment_No & "' and vendor_Invoice_No= '" & objPIDetail.Vendor_Invoice_No & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            End If
        Next
        Return issaved
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Posted from TSPL_MP_PAY_HEAD where Payment_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select Reverse_Code from TSPL_BANK_REVERSE where Document_No='" + strCode + "' and Source_Type='AP'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Bank Reverse -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Reverse_Code"))
                Next
                Throw New Exception(Qry)
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code like 'AP%' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "Update TSPL_MP_PAY_HEAD set Posted = '0' where Payment_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_MP_PAY_DETAIL set Post = '0' where Payment_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_MP_PAY_HEAD set Balance_Amt=Balance_Amt+ISNULL((Select Payment_Amount from TSPL_MP_PAY_HEAD WHERE Payment_No='" + strCode + "'),0) WHERE Payment_No=(Select Applied_Payment from TSPL_MP_PAY_HEAD WHERE Payment_No='" + strCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Xtra.UpdateAPInvoiceBalanceAmount("", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetExchangeDetailDt(ByVal VendorCode As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim strq As String
        strq = "SELECT TSPL_VENDOR_MASTER.Vendor_Account,TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT,TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT" & _
               " FROM TSPL_VENDOR_MASTER LEFT JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_MASTER.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code " & _
               " WHERE TSPL_VENDOR_MASTER.Farmer_Code='" & VendorCode & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        Return dt
    End Function
    Public Shared Function GetExchangeRateAmount(ByVal PaymentNNo As String, ByVal PaymentDate As DateTime, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim strq As String
        strq = "select case when ConvRateRevaluation<>0 then ConvRateRevaluation else ConvRate end as ConvRate from (Select ConvRate,isnull(( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(PaymentDate), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation  from TSPL_Vendor_Invoice_Head where Document_No ='" & PaymentNNo & "')xx"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        Return dt
    End Function

    Public Shared Function PostData(ByVal strPaymentNo As String, Optional ByVal Module_Code As String = "MPayable", Optional ByVal transOpen As SqlTransaction = Nothing) As Boolean
        Dim Payment_Line_No As Integer = 0
        Dim qry As String = ""
        Dim LocSegmentCode As String = ""
        Dim trans As SqlTransaction = Nothing
        If transOpen IsNot Nothing Then
            trans = transOpen
        Else
            trans = clsDBFuncationality.GetTransactin()
        End If
        Try
            If clsCommon.myLen(strPaymentNo) <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            Dim obj As clsFarmerPaymentHeader = clsFarmerPaymentHeader.GetData(strPaymentNo, NavigatorType.Current, trans)
            If obj Is Nothing Then
                Throw New Exception("Document No. not found to Post")
            Else
                '--------Checks Whertrher Transaction Is Locked Or Not-----------
                LocSegmentCode = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans)
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, clsCommon.myCstr(obj.Payment_Date), trans)
                '----------------------------------------------------------------
            End If
            If clsCommon.CompairString(clsCommon.myCstr(obj.Posted), "1") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Posted), "P") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Document no : " + strPaymentNo + "")
            End If
            '' Validation check: by Panch Raj against ticket No:BM00000008437
            'CheckNegativeBankBalance(obj, trans)

            Dim ConvRate As Double = IIf(clsCommon.myCdbl(obj.ConvRate) = 0, 1, clsCommon.myCdbl(obj.ConvRate))
            Dim drtotal As Double = 0
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "PY") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal) Then
                drtotal = clsCommon.myCdbl(obj.Payment_Amount) * ConvRate
            Else
                drtotal = clsCommon.myCdbl(obj.Payment_Amount) * clsCommon.myCdbl(obj.ConvRateOld)
            End If
            'obj.objRemittance = clsRemittance.GetData(strPaymentNo, trans)
            Dim strRecreateVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from  TSPL_JOURNAL_MASTER where Source_Code<>'GL-JE' and  (Source_Code ='AP-PY' and Source_Doc_No ='" & obj.Payment_No & "' and voucher_desc='" & obj.Entry_Desc & "' )", trans))
            'CreateJournalEntry(obj, Module_Code, strRecreateVoucherNo, trans)

            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "PY") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal) Then
                'Dim strQry As String = "Select TSPL_MP_PAY_DETAIL.Vendor_Invoice_No,case when isnull(TSPL_VENDOR_INVOICE_HEAD.ConvRate,0)=0 then 1 else TSPL_VENDOR_INVOICE_HEAD.ConvRate end  as ConvRateOld, TSPL_MP_PAY_DETAIL.Document_No, TSPL_MP_PAY_DETAIL.Applied_Amount,TSPL_MP_PAY_DETAIL.Security_Amount,TSPL_VENDOR_INVOICE_HEAD.Loc_Code "
                'strQry += " from TSPL_MP_PAY_HEAD "
                'strQry += " INNER JOIN TSPL_MP_PAY_DETAIL ON TSPL_MP_PAY_HEAD.Payment_No=TSPL_MP_PAY_DETAIL.Payment_No "
                'strQry += "     INNER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_MP_PAY_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No  "
                'strQry += " Where TSPL_MP_PAY_HEAD.Farmer_Code='" + obj.Farmer_Code + "' AND TSPL_MP_PAY_HEAD.Payment_No ='" + strPaymentNo + "'"
                'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                '    For Each dr As DataRow In dt1.Rows
                '        If clsCommon.myCdbl(dr("Security_Amount")) > 0 Then
                '            Create_Security_Credit_Note(dr("Vendor_Invoice_No"), obj.Farmer_Code, obj, trans)
                '        End If
                '        strQry = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = Balance_Amt - " + clsCommon.myCstr(dr("Applied_Amount")) + " where Farmer_Code = '" + obj.Farmer_Code + "' and Vendor_Invoice_No = '" + clsCommon.myCstr(dr("Vendor_Invoice_No")) + "' AND Document_No = '" + clsCommon.myCstr(dr("Document_No")) + "'"
                '        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                '    Next
                'End If
                'If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                '    'This function chaged Balance Amount of Payment Which is Applied------------
                '    clsFarmerPaymentHeader.UpdateBalance(clsCommon.myCstr(obj.Applied_Payment), clsCommon.myCdbl(obj.Payment_Amount), trans)
                'End If
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_HEAD set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            ElseIf (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_HEAD set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Then
                'If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal Then
                '    If Not String.IsNullOrEmpty(obj.CForm_InvoiceNo) Then
                '        qry = "update TSPL_PI_HEAD set CFormRecd=1 WHERE PI_No ='" + obj.CForm_InvoiceNo + "'"
                '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '    End If
                'End If
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_HEAD set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal Then
                'Dim InvcNo As String = ""
                'Dim BalAmt As Decimal = 0.0
                'Dim PayAmt As Decimal = drtotal
                'Dim strQ As String = "select Document_No,Due_Date ,case when fifo_balance>0 then fifo_balance else   Balance_Amt end as  Balance_Amt  from TSPL_VENDOR_INVOICE_HEAD where Balance_Amt>0  and Farmer_Code='" + obj.Farmer_Code + "' and fifo_knockoff='N'" & _
                '                     "order by TSPL_VENDOR_INVOICE_HEAD.Due_Date "
                'Dim Dt1 As DataTable = New DataTable()
                'Dt1 = clsDBFuncationality.GetDataTable(strQ, trans)
                'For Each dr As DataRow In Dt1.Rows
                '    InvcNo = dr.Item("Document_No").ToString()
                '    BalAmt = dr.Item("Balance_Amt")
                '    If drtotal > BalAmt Then
                '        drtotal = drtotal - BalAmt
                '        strQ = "update TSPL_VENDOR_INVOICE_HEAD set fifo_balance=0.00 , fifo_knockoff='Y' where Document_No ='" + InvcNo + "' and Farmer_Code ='" + obj.Farmer_Code + "'"
                '        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                '    ElseIf drtotal < BalAmt Then
                '        drtotal = drtotal - BalAmt
                '        strQ = "update TSPL_VENDOR_INVOICE_HEAD set fifo_balance=" + (drtotal * -1).ToString() + "-fifo_balance , fifo_knockoff='N' where Document_No ='" + InvcNo + "' and Farmer_Code ='" + obj.Farmer_Code + "'"
                '        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                '    End If
                '    If drtotal < 0 Then
                '        Exit For
                '    End If
                'Next
                'If drtotal > 0 Then
                '    strQ = "update TSPL_MP_PAY_HEAD set fifo_balance=" + drtotal.ToString() + " where Payment_No ='" + strPaymentNo + "'"
                '    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                'End If
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_HEAD set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal Then
                qry = "select TSPL_MP_PAY_DETAIL.Account_code,TSPL_MP_PAY_DETAIL.Net_Balance,TSPL_MP_PAY_DETAIL.Remarks,TSPL_MP_PAY_HEAD.ConvRateOld from TSPL_MP_PAY_DETAIL inner join TSPL_MP_PAY_HEAD on " & _
                " TSPL_MP_PAY_DETAIL.payment_no=TSPL_MP_PAY_HEAD.payment_no where TSPL_MP_PAY_DETAIL.Payment_No='" + strPaymentNo + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_HEAD set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
                For Each dr As DataRow In dtNew.Rows
                    Payment_Line_No = Payment_Line_No + 1
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'  and Payment_Line_No=" & Payment_Line_No & " ", trans)
                Next
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_HEAD set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_PAY_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            End If
            'qry = "update TSPL_REMITTANCE set Remit_TDS='N' where Document_No='" + strPaymentNo + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)


            'clsBankReco.SetOutstandingEntry(strPaymentNo, obj.Payment_Date, "Payment", trans)
            If transOpen Is Nothing Then
                trans.Commit()
            End If
        Catch ex As Exception
            Try
                If transOpen Is Nothing Then
                    trans.Rollback()
                End If
            Catch ex1 As Exception
                Throw New Exception(ex1.Message)
            End Try
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckNegativeBankBalance(ByVal obj As clsFarmerPaymentHeader, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        '' Validation check: by Panch Raj against ticket No:BM00000008437
        If clsCommon.CompairString(obj.Payment_Type, "RC") <> CompairStringResult.Equal Then
            Dim Bank_Type_Check As String = "0"
            Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, trans)
            Dim Bank_Type As String = clsBankMaster.GetBankType(obj.Bank_Code, trans)
            Dim Bank_Balance As Decimal = 0
            Dim Bank_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" & obj.Bank_Code & "')", trans))
            If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                '' allow for all
            ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                    ''richa agarwal 03 Aug,2016
                    ' Bank_Balance = GetBankBalance(obj.Payment_Date, obj.Bank_Code, Bank_Location, trans)
                    Bank_Balance = GetBankBalance(obj.Payment_No, obj.Payment_Date, obj.Bank_Code, Bank_Location, trans)
                    If Bank_Balance < obj.PAYMENT_AMOUNT_BASE_CURRENCY Then
                        Throw New Exception("Payment Amount : " & obj.PAYMENT_AMOUNT_BASE_CURRENCY & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                    ''richa agarwal 03 Aug,2016
                    ' Bank_Balance = GetBankBalance(obj.Payment_Date, obj.Bank_Code, Bank_Location, trans)
                    Bank_Balance = GetBankBalance(obj.Payment_No, obj.Payment_Date, obj.Bank_Code, Bank_Location, trans)
                    If Bank_Balance < obj.PAYMENT_AMOUNT_BASE_CURRENCY Then
                        Throw New Exception("Payment Amount : " & obj.PAYMENT_AMOUNT_BASE_CURRENCY & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                ''richa agarwal 03 Aug,2016
                'Bank_Balance = GetBankBalance(obj.Payment_Date, obj.Bank_Code, Bank_Location, trans)
                Bank_Balance = GetBankBalance(obj.Payment_No, obj.Payment_Date, obj.Bank_Code, Bank_Location, trans)
                If Bank_Balance < obj.PAYMENT_AMOUNT_BASE_CURRENCY Then
                    Throw New Exception("Payment Amount : " & obj.PAYMENT_AMOUNT_BASE_CURRENCY & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                End If
            End If
        End If
        Return True
    End Function
    '=========BM00000008292
    Public Shared Function CreateJournalEntry(ByVal obj As clsFarmerPaymentHeader, ByVal Module_Code As String, ByVal strVoucherNoifExists As String, ByVal trans As SqlTransaction) As Boolean
        'Dim isSourceCode As Boolean = False
        Dim isSaved As Boolean = True
        Dim Payment_Line_No As Integer = 0
        Dim qry As String = ""
        Try
            Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + obj.Bank_Code + "'", trans)
            Dim PostDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select Payment_Date from TSPL_MP_PAY_HEAD where TSPL_MP_PAY_HEAD.Payment_No='" + obj.Payment_No + "' ", trans))
            Dim strentrydesc As String = "Payment Against" + " " + obj.Payment_No
            Dim sourceType As String = "AP-PY"
            Dim sourceDesc As String = "PAYMENT"
            Dim paymentDesc As String = clsCommon.myCstr(obj.Entry_Desc)
            If clsCommon.myCstr(obj.Remit_To) <> "" Then
                paymentDesc += " " + clsCommon.myCstr(obj.Remit_To)
            End If
            Dim strsrctype As String = "V"
            Dim Loc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + clsCommon.myCstr(obj.Farmer_Code) + "' ", trans))
            Dim straccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + clsCommon.myCstr(obj.Farmer_Code) + "' ", trans))
            straccount = clsERPFuncationality.ChangeGLAccountLocationSegment(straccount, BankLocation, True, trans)
            Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
            Dim bankAccount As String
            Dim UseSubAcc As String
            UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                bankAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Sub_Account,'')  BANKACC from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
            Else
                bankAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
            End If
            If clsCommon.myLen(bankAccount) <= 0 Then
                If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(obj.Bank_Code))
                Else
                    Throw New Exception("Please enter bank account for bank " + clsCommon.myCstr(obj.Bank_Code))
                End If
            End If
            bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, BankLocation, True, trans)
            Dim stradvance As String
            If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal Then
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + clsCommon.myCstr(obj.Farmer_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
            Else
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Advance_Account  from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + clsCommon.myCstr(obj.Farmer_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
            End If


            '' Anubhooti 31-Mar-2015 (If security is checked then security account will go on GL)
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Is_Security) = 1 Then
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.SECURITY_ACCOUNT,'') AS SECURITY_ACCOUNT from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + clsCommon.myCstr(obj.Farmer_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
                If clsCommon.myLen(stradvance) <= 0 Then
                    Throw New Exception("Please fill security account on vendor account set.")
                End If
            End If
            '' Anubhooti 27-Mar-2015 (Replace AdvanceAccount From AdvanceAgainstSalary In Case Of Advance & On-Account Only)
            '' changes by richa agarwal against ticket no BM00000007565
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Advance_Against_Salary) = 1 Then
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Salary,'') AS Advance_Against_Salary from TSPL_MP_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Acct_Set_Code =s.Acct_Set_Code where m.MP_CODE = '" + clsCommon.myCstr(obj.Farmer_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
                If clsCommon.myLen(stradvance) <= 0 Then
                    Throw New Exception("Please fill advance against salary account on vendor account set.")
                End If
            End If
            '' MultiCurrency
            Dim IsMultiCurrency As Boolean = clsModuleCurrencyMapping.CheckMultiCurrency(Module_Code, trans)
            Dim CURRENCY_CODE As String = obj.CURRENCY_CODE
            ' Dim APPLICABLEFROM As Date? = Nothing
            Dim APPLICABLEFROM As String = String.Empty
            If obj.ApplicableFrom Is Nothing Then
                APPLICABLEFROM = Nothing
            Else
                'APPLICABLEFROM = obj.APPLICABLEFROM")
                APPLICABLEFROM = clsCommon.GetPrintDate(clsCommon.myCstr(obj.ApplicableFrom), "yyyy-MM-dd")
            End If
            Dim EXCHANGE_LOSS_AMT As Double = clsCommon.myCdbl(obj.EXCHANGE_LOSS_AMT)
            Dim EXCHANGE_GAIN_AMT As Double = clsCommon.myCdbl(obj.EXCHANGE_GAIN_AMT)
            Dim EXCHANGE_GAIN_ACCOUNT As String = clsCommon.myCstr(obj.EXCHANGE_GAIN_ACCOUNT)
            Dim EXCHANGE_LOSS_ACCOUNT As String = clsCommon.myCstr(obj.EXCHANGE_LOSS_ACCOUNT)
            Dim ConvRateOld As Double = IIf(clsCommon.myCdbl(obj.ConvRateOld) = 0, 1, clsCommon.myCdbl(obj.ConvRateOld))
            Dim ConvRate As Double = IIf(clsCommon.myCdbl(obj.ConvRate) = 0, 1, clsCommon.myCdbl(obj.ConvRate))

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "APPLICABLEFROM", APPLICABLEFROM, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", ConvRate)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", ConvRateOld)
            '' End MultiCurrency
            Dim arr As New ArrayList()
            Dim drtotal As Double = 0
            Dim crtotal As Double = 0
            Dim bankCharges As Double = 0
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "PY") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal) Then
                drtotal = clsCommon.myCdbl(obj.Payment_Amount) * ConvRate
                bankCharges = clsCommon.myCdbl(obj.Bank_Charges)
                crtotal = -1 * (clsCommon.myCdbl(obj.Payment_Amount) * ConvRate + bankCharges)
                Dim Credit() As String = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                Dim Debit() As String = {straccount, drtotal}
                ''------------------------------------
                arr.Add(Credit)
                arr.Add(Debit)
            Else
                drtotal = clsCommon.myCdbl(obj.Payment_Amount) * clsCommon.myCdbl(obj.ConvRateOld)
                crtotal = -1 * clsCommon.myCdbl(obj.Payment_Amount) * clsCommon.myCdbl(obj.ConvRateOld)
                bankCharges = clsCommon.myCdbl(obj.Bank_Charges) * clsCommon.myCdbl(obj.ConvRateOld)
                Dim Credit() As String = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                Dim Debit() As String = {straccount, drtotal}
                arr.Add(Credit)
                arr.Add(Debit)
            End If
            '' Anubhooti 27-Nov-2014 (TDS GL Account)
            obj.objRemittance = clsRemittance.GetData(obj.Payment_No, trans)
            ''
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "PY") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal) Then
                Dim DrAmt As Double = 1
                Dim TotalCrAmt As Double = 0
                Dim DocType As String = String.Empty

                arr = New ArrayList()
                If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                    Dim tBankLocation As String = bankAccount.Substring(clsCommon.myLen(bankAccount) - 3, 3)
                    qry = "select TSPL_VENDOR_ACCOUNT_SET.Advance_Account from TSPL_VENDOR_ACCOUNT_SET  INNER JOIN TSPL_MP_MASTER ON TSPL_VENDOR_MASTER.Acct_Set_Code= TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  where TSPL_MP_MASTER.Farmer_Code ='" + clsCommon.myCstr(obj.Farmer_Code) + "'"
                    bankAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, tBankLocation, trans)
                End If
                Dim strQry As String = " select Vendor_Invoice_No,(case when ISNULL(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRateOld end) as ConvRateOld,Document_No,Applied_Amount," & _
                                       " Security_Amount,Loc_Code from (Select TSPL_MP_PAY_DETAIL.Vendor_Invoice_No,1  as ConvRateOld,1 as ConvRateRevaluation, TSPL_MP_PAY_DETAIL.Document_No, " & _
                                       " TSPL_MP_PAY_DETAIL.Applied_Amount,TSPL_MP_PAY_DETAIL.Security_Amount,PPH.Loc_Seg_Code as Loc_Code " & _
                                       " from TSPL_MP_PAY_HEAD INNER JOIN TSPL_MP_PAY_DETAIL ON TSPL_MP_PAY_HEAD.Payment_No=TSPL_MP_PAY_DETAIL.Payment_No " & _
                                       " INNER JOIN TSPL_MP_PAY_PROCESS_DETAIL ON TSPL_MP_PAY_DETAIL.Document_No =TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No " & _
                                       " inner join TSPL_PAYMENT_PROCESS_HEAD PPH ON TSPL_MP_PAY_PROCESS_DETAIL.Doc_No=PPH.Doc_No " & _
                                       " Where TSPL_MP_PAY_HEAD.Farmer_Code='" + obj.Farmer_Code + "' AND TSPL_MP_PAY_HEAD.Payment_No ='" + obj.Payment_No + "')xx"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                Dim isApplyBrachAccounting As Boolean = False
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    isApplyBrachAccounting = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                    For Each dr As DataRow In dt1.Rows
                        Dim strDocLocation As String = clsCommon.myCstr(dr("Loc_Code"))
                        '' Debit Note Should be deducted from Applied Amount 25-Aug-2015 BM00000007721 
                        'DocType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then 'Debit Note' Else case  When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then 'Credit Note' Else 'Invoice' End End As [DocType]From TSPL_VENDOR_INVOICE_HEAD Where Document_No='" + clsCommon.myCstr(clsCommon.myCstr(dr("Document_No"))) + "'", trans))
                        DocType = "Invoice"
                        ''richa agarwal 24/06/2015
                        Dim dblAmount As Double = clsCommon.myCdbl(dr("Applied_Amount")) * clsCommon.myCdbl(dr("ConvRateOld"))
                        Dim dblAmount1 As Double = clsCommon.myCdbl(dr("Applied_Amount")) * ConvRate

                        If clsCommon.CompairString(DocType, "Debit Note") = CompairStringResult.Equal Then
                            dblAmount = dblAmount * -1
                            dblAmount1 = dblAmount1 * -1
                        Else
                            dblAmount = dblAmount
                            dblAmount1 = dblAmount1
                        End If


                        ''-----------------
                        If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(BankLocation, strDocLocation) = CompairStringResult.Equal) AndAlso Not (clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal) Then
                            Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, BankLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + strDocLocation + " and to location " + BankLocation)
                            End If
                            ''branch accounting of customer
                            ''richa agarwal 01/07/2015
                            Dim RcvblAcc = New String() {strTemp, -1 * dblAmount1}
                            arr.Add(RcvblAcc)
                            ' changed due to ap invoice takes segment not location
                            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(straccount, strDocLocation, True, trans)
                            RcvblAcc = New String() {strTemp, dblAmount}
                            arr.Add(RcvblAcc)
                            ''richa agarwal 01/07/2015
                            RcvblAcc = New String() {bankAccount, -1 * dblAmount1}
                            arr.Add(RcvblAcc)
                            ''-------------------

                            strTemp = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strDocLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strDocLocation)
                            End If
                            ''richa agarwal 01/07/2015
                            RcvblAcc = New String() {strTemp, dblAmount1}
                            '--------------------
                            arr.Add(RcvblAcc)

                            ''richa 30/10/2015
                            EXCHANGE_GAIN_ACCOUNT = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.EXCHANGE_GAIN_ACCOUNT, strDocLocation, True, trans)
                            EXCHANGE_LOSS_ACCOUNT = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.EXCHANGE_LOSS_ACCOUNT, strDocLocation, True, trans)
                        Else
                            If clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                                ''richa agarwal
                                Dim STRGLACCOUNTFORAD As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("sELECT ISNULL(Location_GL_Code ,'') FROM TSPL_MP_PAY_HEAD WHERE Payment_No ='" & obj.Applied_Payment & "'", trans))
                                If clsCommon.myLen(STRGLACCOUNTFORAD) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, STRGLACCOUNTFORAD) = CompairStringResult.Equal) Then
                                    bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, STRGLACCOUNTFORAD, True, trans)
                                    '  straccount = clsERPFuncationality.ChangeGLAccountLocationSegment(straccount, STRGLACCOUNTFORAD, True, trans)
                                End If
                                Dim RcvblAcc = New String() {bankAccount, -1 * dblAmount1}
                                arr.Add(RcvblAcc)
                                straccount = clsERPFuncationality.ChangeGLAccountLocationSegment(straccount, strDocLocation, True, trans)
                                RcvblAcc = New String() {straccount, dblAmount}
                                arr.Add(RcvblAcc)

                                ''richa 22 apr,2016
                                EXCHANGE_GAIN_ACCOUNT = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.EXCHANGE_GAIN_ACCOUNT, strDocLocation, True, trans)
                                EXCHANGE_LOSS_ACCOUNT = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.EXCHANGE_LOSS_ACCOUNT, strDocLocation, True, trans)


                                If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(STRGLACCOUNTFORAD, strDocLocation) = CompairStringResult.Equal) Then
                                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, STRGLACCOUNTFORAD, trans)
                                    If clsCommon.myLen(strTemp) <= 0 Then
                                        Throw New Exception("Please set Branch account mapping with from location " + strDocLocation + " and to location " + STRGLACCOUNTFORAD)
                                    End If
                                    strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strTemp, strDocLocation, True, trans)
                                    RcvblAcc = New String() {strTemp, -1 * dblAmount1}
                                    arr.Add(RcvblAcc)

                                    strTemp = ClsBranchAccountMapping.GetBranchAccount(STRGLACCOUNTFORAD, strDocLocation, trans)
                                    If clsCommon.myLen(strTemp) <= 0 Then
                                        Throw New Exception("Please set Branch account mapping with from location " + STRGLACCOUNTFORAD + " and to location " + strDocLocation)
                                    End If

                                    strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strTemp, STRGLACCOUNTFORAD, True, trans)
                                    RcvblAcc = New String() {strTemp, 1 * dblAmount1}
                                    arr.Add(RcvblAcc)

                                End If
                            Else
                                Dim RcvblAcc = New String() {bankAccount, -1 * dblAmount1}
                                arr.Add(RcvblAcc)
                                RcvblAcc = New String() {straccount, dblAmount}
                                arr.Add(RcvblAcc)
                            End If
                        End If
                    Next
                    ' '' richa agarwal 01/07/2015 to add bank charges in bank amount
                    If bankCharges > 0 Then
                        Dim BankChargeCredit() As String = {bankAccount, -1 * bankCharges}
                        arr.Add(BankChargeCredit)
                    End If
                End If


                If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                    ''---richa agarwal 01/07/2015
                    obj.Bank_Charges_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Bank_Charges_Ac, BankLocation, True, trans)
                    ''---------------------
                    Dim BankCharge() As String = {obj.Bank_Charges_Ac, bankCharges}
                    arr.Add(BankCharge)
                End If


                '' MULTICURRENCY
                If IsMultiCurrency Then

                    If EXCHANGE_LOSS_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                        arr.Add(CURR_EXCHANGE)
                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                        arr.Add(CURR_EXCHANGE)
                    End If
                End If
                '' END MULTICURRENCY

                transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arr, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)

            ElseIf (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) Then

                Dim tds As Double = 0
                Dim paymentAmt As Double = 0
                Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld,PAYMENT_AMOUNT_BASE_CURRENCY  from TSPL_MP_PAY_HEAD where Payment_No = '" + obj.Payment_No + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(checkall, trans)
                If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                    tds = clsCommon.myCdbl(dtNew.Rows(0)("TDS_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
                    paymentAmt = clsCommon.myCdbl(dtNew.Rows(0)("PAYMENT_AMOUNT_BASE_CURRENCY")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
                End If
                Dim BankCharge() As String = {obj.Bank_Charges_Ac, bankCharges}
                drtotal = clsCommon.myCdbl(obj.PAYMENT_AMOUNT_BASE_CURRENCY) * clsCommon.myCdbl(obj.ConvRateOld)
                crtotal = -1 * clsCommon.myCdbl(obj.PAYMENT_AMOUNT_BASE_CURRENCY) * clsCommon.myCdbl(obj.ConvRateOld)
                If tds <> 0 And paymentAmt <> 0 Then
                    '' Anubhooti 27-Nov-2014
                    If (obj.objRemittance IsNot Nothing) Then ''is_For_TDS Entry made by ap invoice is come in this section
                        obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, BankLocation, True, trans)
                    Else
                        Throw New Exception("Please enter TDS account")
                    End If
                    Dim acc4() As String = {stradvance, (paymentAmt + tds)}
                    Dim acc3() As String = {obj.objRemittance.Branch_GL_AC, -1 * (paymentAmt + tds)}
                    Dim acc5() As String = {bankAccount, crtotal - bankCharges - (+EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    Dim arrtotal As New ArrayList()
                    arrtotal.Add(acc4)
                    arrtotal.Add(acc3)
                    arrtotal.Add(acc5)

                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrtotal.Add(BankCharge)
                    End If
                    '' MULTICURRENCY
                    If IsMultiCurrency Then
                        If EXCHANGE_LOSS_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                            arrtotal.Add(CURR_EXCHANGE)
                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                            arrtotal.Add(CURR_EXCHANGE)
                        End If
                    End If
                    '' END MULTICURRENCY
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)

                Else
                    '---------------------------------------------------------
                    Dim arrlist As New ArrayList()
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal Then
                        Dim arr6() As String = {stradvance, drtotal}
                        Dim arr7() As String = {bankAccount, crtotal - (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT)}
                        arrlist.Add(arr6)
                        arrlist.Add(arr7)
                        '' MULTICURRENCY
                        If IsMultiCurrency Then
                            If EXCHANGE_LOSS_AMT > 0 Then
                                Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, -EXCHANGE_LOSS_AMT}
                                arrlist.Add(CURR_EXCHANGE)
                            ElseIf EXCHANGE_GAIN_AMT > 0 Then
                                Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, EXCHANGE_GAIN_AMT}
                                arrlist.Add(CURR_EXCHANGE)
                            End If
                        End If
                        '' END MULTICURRENCY
                    Else
                        Dim arr6() As String = {stradvance, drtotal}
                        Dim arr7() As String = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                        Dim arr8() As String = {obj.Bank_Charges_Ac, bankCharges}
                        arrlist.Add(arr6)
                        arrlist.Add(arr7)
                        If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                            arrlist.Add(arr8)
                        End If
                        '' MULTICURRENCY
                        If IsMultiCurrency Then
                            If EXCHANGE_LOSS_AMT > 0 Then
                                Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                                arrlist.Add(CURR_EXCHANGE)
                            ElseIf EXCHANGE_GAIN_AMT > 0 Then
                                Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                                arrlist.Add(CURR_EXCHANGE)
                            End If
                        End If
                        '' END MULTICURRENCY
                    End If
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Then
                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                Dim strGLLoc As String = clsCommon.myCstr(obj.Location_GL_Code)
                If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal Then
                    If Not String.IsNullOrEmpty(obj.CForm_InvoiceNo) Then
                        qry = "update TSPL_PI_HEAD set CFormRecd=1 WHERE PI_No ='" + obj.CForm_InvoiceNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
                Dim tds As Double = 0
                Dim paymentAmt As Double = 0
                Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld  from TSPL_MP_PAY_HEAD where Payment_No = '" + obj.Payment_No + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(checkall, trans)
                If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                    tds = clsCommon.myCdbl(dtNew.Rows(0)("TDS_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
                    paymentAmt = clsCommon.myCdbl(dtNew.Rows(0)("Payment_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
                End If
                ''---richa agarwal 01/07/2015
                obj.Bank_Charges_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Bank_Charges_Ac, BankLocation, True, trans)
                Dim BankCharge() As String = {obj.Bank_Charges_Ac, bankCharges}
                ''---------------------
                If tds <> 0 And paymentAmt <> 0 Then
                    '' Anubhooti 27-Nov-2014 
                    If (obj.objRemittance IsNot Nothing) Then ''is_For_TDS Entry made by ap invoice is come in this section
                        obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, BankLocation, True, trans)
                    Else
                        Throw New Exception("Please enter TDS account")
                    End If
                    '' Anubhooti 08-Jan-2014 (Advance A/C loc should be overrite from new Loc )
                    If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
                        obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, strGLLoc, True, trans)
                    Else
                        stradvance = stradvance
                        obj.objRemittance.Branch_GL_AC = obj.objRemittance.Branch_GL_AC
                    End If

                    Dim acc4() As String = {stradvance, (paymentAmt + tds)}
                    Dim acc3() As String = {obj.objRemittance.Branch_GL_AC, -1 * tds}
                    Dim acc5() As String = {bankAccount, crtotal - bankCharges - (+EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    Dim arrtotal As New ArrayList()
                    arrtotal.Add(acc4)
                    arrtotal.Add(acc3)
                    arrtotal.Add(acc5)
                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrtotal.Add(BankCharge)
                    End If
                    '' MULTICURRENCY
                    If IsMultiCurrency Then
                        If EXCHANGE_LOSS_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                            arrtotal.Add(CURR_EXCHANGE)
                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                            arrtotal.Add(CURR_EXCHANGE)
                        End If
                    End If
                    '' END MULTICURRENCY
                    '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location)
                    If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
                        End If
                        Dim BranchAccCR = New String() {strTemp, drtotal}
                        arrtotal.Add(BranchAccCR)

                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
                        End If
                        Dim BranchAccDR = New String() {strTemp, -1 * drtotal}
                        arrtotal.Add(BranchAccDR)
                    End If
                    ''
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)

                Else
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Then
                        drtotal = drtotal * -1
                        crtotal = crtotal * -1
                    End If

                    '---------------------------------------------------------
                    Dim arrlist As New ArrayList()
                    '' Anubhooti 08-Jan-2014 (Advance A/C loc should be overrite from new Loc )
                    If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
                    Else
                        stradvance = stradvance
                    End If
                    ''
                    Dim arr6() As String = {stradvance, drtotal}
                    ''richa agarwal 01/07/2015
                    Dim arr7() As String = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    ''----------------
                    Dim arr8() As String = {obj.Bank_Charges_Ac, bankCharges}
                    arrlist.Add(arr6)
                    arrlist.Add(arr7)
                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrlist.Add(arr8)
                    End If
                    '' MULTICURRENCY
                    If IsMultiCurrency Then
                        If EXCHANGE_LOSS_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                            arrlist.Add(CURR_EXCHANGE)
                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                            arrlist.Add(CURR_EXCHANGE)
                        End If
                    End If
                    '' END MULTICURRENCY

                    '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location,in case of "AV" & "OA")
                    If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
                        End If
                        Dim BranchAccCR = New String() {strTemp, drtotal}
                        arrlist.Add(BranchAccCR)

                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
                        End If
                        Dim BranchAccDR = New String() {strTemp, -1 * drtotal}
                        arrlist.Add(BranchAccDR)
                    End If
                    'End If
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal Then
                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                Dim strGLLoc As String = clsCommon.myCstr(obj.Location_GL_Code)
                Dim tds As Double = 0
                Dim paymentAmt As Double = 0
                Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld  from TSPL_MP_PAY_HEAD where Payment_No = '" + obj.Payment_No + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(checkall, trans)
                If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                    tds = clsCommon.myCdbl(dtNew.Rows(0)("TDS_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
                    paymentAmt = clsCommon.myCdbl(dtNew.Rows(0)("Payment_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
                End If
                ''---richa agarwal 01/07/2015
                obj.Bank_Charges_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Bank_Charges_Ac, BankLocation, True, trans)
                Dim BankCharge() As String = {obj.Bank_Charges_Ac, bankCharges}
                ''---------------------
                If tds <> 0 And paymentAmt <> 0 Then
                    '' Anubhooti 27-Nov-2014 
                    If (obj.objRemittance IsNot Nothing) Then ''is_For_TDS Entry made by ap invoice is come in this section
                        obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, BankLocation, True, trans)
                    Else
                        Throw New Exception("Please enter TDS account")
                    End If
                    '' Anubhooti 08-Jan-2014 (Advance A/C loc should be overrite from new Loc )
                    If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
                        obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, strGLLoc, True, trans)
                    Else
                        stradvance = stradvance
                        obj.objRemittance.Branch_GL_AC = obj.objRemittance.Branch_GL_AC
                    End If
                    ''
                    Dim acc4() As String = {stradvance, paymentAmt + tds}
                    Dim acc3() As String = {obj.objRemittance.Branch_GL_AC, -1 * tds}
                    ''richa agarwal remove bank charges from acc5() because it will added bank charges twice
                    ' Dim acc5() As String = {bankAccount, crtotal - bankCharges - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    Dim acc5() As String = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    Dim arrtotal As New ArrayList()
                    arrtotal.Add(acc4)
                    arrtotal.Add(acc3)
                    arrtotal.Add(acc5)
                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrtotal.Add(BankCharge)
                    End If
                    '' MULTICURRENCY
                    If IsMultiCurrency Then
                        If EXCHANGE_LOSS_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                            arrtotal.Add(CURR_EXCHANGE)
                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                            arrtotal.Add(CURR_EXCHANGE)
                        End If
                    End If

                    '' END MULTICURRENCY

                    '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location)
                    If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
                        End If
                        Dim BranchAccCR = New String() {strTemp, drtotal}
                        arrtotal.Add(BranchAccCR)

                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
                        End If
                        Dim BranchAccDR = New String() {strTemp, -1 * drtotal}
                        arrtotal.Add(BranchAccDR)
                    End If
                    ''
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)
                Else
                    '' Anubhooti 08-Jan-2014 (Advance A/C loc should be overrite from new Loc )
                    If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
                    Else
                        stradvance = stradvance
                    End If
                    ''
                    Dim arr6() As String = {stradvance, drtotal}
                    ''richa agarwal 01/07/2015
                    Dim arr7() As String = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    ''-----------------------
                    Dim arrlist As New ArrayList()
                    arrlist.Add(arr6)
                    arrlist.Add(arr7)
                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrlist.Add(BankCharge)
                    End If
                    '' MULTICURRENCY
                    If IsMultiCurrency Then
                        If EXCHANGE_LOSS_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                            arrlist.Add(CURR_EXCHANGE)
                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                            arrlist.Add(CURR_EXCHANGE)
                        End If
                    End If
                    '' END MULTICURRENCY
                    '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location,in case of "AV" & "OA")
                    If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
                        End If
                        Dim BranchAccCR = New String() {strTemp, drtotal}
                        arrlist.Add(BranchAccCR)

                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
                        End If
                        Dim BranchAccDR = New String() {strTemp, -1 * drtotal}
                        arrlist.Add(BranchAccDR)
                    End If
                    ''
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)
                End If
                Dim InvcNo As String = ""
                Dim BalAmt As Decimal = 0.0
                Dim PayAmt As Decimal = drtotal
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal Then
                Dim arrmis As New ArrayList()
                Dim ESiAmt As Decimal = 0.0
                Dim MiscAmt As Decimal = 0.0
                Dim ESI_Percent As Decimal = 0.0
                qry = "select TSPL_MP_PAY_DETAIL.Account_code,TSPL_MP_PAY_DETAIL.Net_Balance,TSPL_MP_PAY_DETAIL.Remarks,TSPL_MP_PAY_HEAD.ConvRateOld,TSPL_MP_PAY_DETAIL.Hirerachy_Level_Code,TSPL_MP_PAY_DETAIL.Cost_Center_Fin_Code from TSPL_MP_PAY_DETAIL inner join TSPL_MP_PAY_HEAD on " & _
                " TSPL_MP_PAY_DETAIL.payment_no=TSPL_MP_PAY_HEAD.payment_no where TSPL_MP_PAY_DETAIL.Payment_No='" + obj.Payment_No + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                    Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                    For Each dr As DataRow In dtNew.Rows
                        Dim strAccountLocation As String = clsCommon.myCstr(dr("Account_code")).Substring(clsCommon.myLen(clsCommon.myCstr(dr("Account_code"))) - 3, 3)
                        Dim dblAmount As Double = clsCommon.myCdbl(dr("Net_Balance")) * clsCommon.myCdbl(dr("ConvRateOld"))
                        Dim strHirerchyCode As String = clsCommon.myCstr(dr("Hirerachy_Level_Code"))
                        Dim strCostCenterCode As String = clsCommon.myCstr(dr("Cost_Center_Fin_Code"))
                        If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(BankLocation, strAccountLocation) = CompairStringResult.Equal) Then
                            Dim Acc4() As String = {bankAccount, -1 * dblAmount}
                            arrmis.Add(Acc4)

                            Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strAccountLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strAccountLocation)
                            End If
                            Dim RcvblAcc = New String() {strTemp, dblAmount}
                            arrmis.Add(RcvblAcc)

                            Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), dblAmount, clsCommon.myCstr(dr("Remarks")), "", strHirerchyCode, strCostCenterCode}
                            arrmis.Add(acc3)

                            strTemp = ClsBranchAccountMapping.GetBranchAccount(strAccountLocation, BankLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Brach account mapping with from location " + strAccountLocation + " and to location " + BankLocation)
                            End If
                            RcvblAcc = New String() {strTemp, -1 * dblAmount}
                            arrmis.Add(RcvblAcc)
                        Else
                            Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), dblAmount, clsCommon.myCstr(dr("Remarks")), "", strHirerchyCode, strCostCenterCode}
                            arrmis.Add(acc3)

                            Dim Acc4() As String = {bankAccount, -1 * dblAmount}
                            arrmis.Add(Acc4)
                        End If
                        If clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) < 0 Then
                            ESiAmt = ESiAmt + (clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) * clsCommon.myCdbl(dr("ConvRateOld")) * -1)
                        End If
                    Next
                End If
                Dim strbankacct1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
                If obj.Location_Code <> "" Then
                    bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, obj.Location_Code, False, trans)
                End If
                If -ESiAmt - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT) <> 0 Then
                    Dim Acc4() As String = {bankAccount, -ESiAmt - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    arrmis.Add(Acc4)
                End If
                If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                    Dim BankCharge() As String = {obj.Bank_Charges_Ac, bankCharges}
                    arrmis.Add(BankCharge)
                End If
                If ESiAmt <> 0 Then
                    Dim Acc5() As String = {bankAccount, ESiAmt}
                    arrmis.Add(Acc5)
                End If
                '' MULTICURRENCY
                If IsMultiCurrency Then
                    If EXCHANGE_LOSS_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                        arrmis.Add(CURR_EXCHANGE)
                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                        arrmis.Add(CURR_EXCHANGE)
                    End If
                End If
                '' END MULTICURRENCY
                sourceType = "AP-MI"
                transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Total_Prepayment*ConvRateOld  from  TSPL_MP_PAY_HEAD where Payment_No like '" + clsCommon.myCstr(obj.Document_No) + "'  and Farmer_Code = '" + clsCommon.myCstr(obj.Farmer_Code) + "'", trans))
                Dim arrcontrol() As String = {straccount, clsCommon.myCdbl(obj.Payment_Amount) * clsCommon.myCdbl(obj.ConvRateOld)}
                Dim arradvance() As String = {stradvance, -1 * clsCommon.myCdbl(obj.Payment_Amount) * clsCommon.myCdbl(obj.ConvRateOld) - EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT}
                Dim applydocument As New ArrayList()
                applydocument.Add(arrcontrol)
                applydocument.Add(arradvance)

                '' MULTICURRENCY
                If IsMultiCurrency Then
                    If EXCHANGE_LOSS_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                        applydocument.Add(CURR_EXCHANGE)
                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                        applydocument.Add(CURR_EXCHANGE)
                    End If
                End If
                '' END MULTICURRENCY
                transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, applydocument, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function UpdatePostedData(ByVal obj As clsFarmerPaymentHeader) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            UpdatePostedData(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function UpdatePostedData(ByVal obj As clsFarmerPaymentHeader, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        Try
            '--------Checks Whertrher Transaction Is Locked Or Not-----------
            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans)
            clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, obj.Payment_Date, trans)
            '----------------------------------------------------------------
            'If clsCommon.myLen(obj.Payment_No) > 0 Then
            '    Dim isPosted As Integer = clsDBFuncationality.getSingleValue("Select Posted from TSPL_MP_PAY_HEAD Where Payment_No='" + obj.Payment_No + "'", trans)
            '    If isPosted = 1 Then
            '        Throw New Exception("Document already posted")
            '    End If
            'End If

            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_REMITTANCE Where Document_No='" + obj.Payment_No + "'", trans)

            Dim qry As String = "DELETE From TSPL_MP_PAY_DETAIL WHERE Payment_No ='" + obj.Payment_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Payment_Date", clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy hh:mm tt"))
            If obj.Payment_Post_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Payment_Post_Date", clsCommon.GetPrintDate(obj.Payment_Post_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Payment_Post_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Payment_Type", obj.Payment_Type)
            clsCommon.AddColumnsForChange(coll, "Farmer_Code", obj.Farmer_Code)
            clsCommon.AddColumnsForChange(coll, "Farmer_Name", obj.Farmer_Name)
            clsCommon.AddColumnsForChange(coll, "Remit_To", obj.Remit_To)
            clsCommon.AddColumnsForChange(coll, "Entry_Desc", obj.Entry_Desc)
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Narration", obj.Narration)
            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            clsCommon.AddColumnsForChange(coll, "CHECK_PRINT", obj.CHECK_PRINT)
            'clsCommon.AddColumnsForChange(coll, "CHECK_CODE", obj.CHECK_CODE, True)

            clsCommon.AddColumnsForChange(coll, "CFormRecd", obj.CFormRecd)
            clsCommon.AddColumnsForChange(coll, "CForm_InvoiceNo", obj.CForm_InvoiceNo)
            '' Anubhooti 22-July-2014
            clsCommon.AddColumnsForChange(coll, "Account_Payee", obj.Account_Payee)
            clsCommon.AddColumnsForChange(coll, "Is_Security", obj.Is_Security)
            clsCommon.AddColumnsForChange(coll, "Account_Payee_Name", obj.Account_Payee_Name)
            '' Anubhooti 21-Aug-2014
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No, True)
            ''shivani
            clsCommon.AddColumnsForChange(coll, "Loan_Code", obj.Loan_Code, True)
            clsCommon.AddColumnsForChange(coll, "Payment_Process_Code", obj.Payment_Process_Code, True)
            '' Anubhooti 07-Jan-2014 BM00000005309
            clsCommon.AddColumnsForChange(coll, "Location_GL_Code", obj.Location_GL_Code, True)
            '' Anubhooti 27-Mar-2015 (Advance Against Salary in case of Advance/On-Account)
            clsCommon.AddColumnsForChange(coll, "Advance_Against_Salary", obj.Advance_Against_Salary)
            If clsCommon.myLen(obj.Cheque_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "PDC_Cheque", obj.PDC_Cheque)
            If clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Payment_Amount", obj.Total_Prepayment - Math.Round(obj.TDS_Amount, 0, MidpointRounding.ToEven))
            Else
                clsCommon.AddColumnsForChange(coll, "Payment_Amount", obj.Payment_Amount)
            End If
            obj.Vendor_Account_Set = clsDBFuncationality.getSingleValue("select Vendor_Account  from TSPL_VENDOR_MASTER where Farmer_Code ='" + obj.Farmer_Code + "'", trans)
            clsCommon.AddColumnsForChange(coll, "Vendor_Account_Set", obj.Vendor_Account_Set)
            clsCommon.AddColumnsForChange(coll, "TDS_Amount", Math.Round(obj.TDS_Amount, 0, MidpointRounding.ToEven))
            clsCommon.AddColumnsForChange(coll, "Total_Prepayment", obj.Total_Prepayment)
            clsCommon.AddColumnsForChange(coll, "Apply_By", obj.Apply_By)
            clsCommon.AddColumnsForChange(coll, "Apply_To", obj.Apply_To)
            clsCommon.AddColumnsForChange(coll, "Level1_User_code", obj.Level1_User_code)
            clsCommon.AddColumnsForChange(coll, "Level2_User_code", obj.Level2_User_code)
            clsCommon.AddColumnsForChange(coll, "Level3_User_code", obj.Level3_User_code)
            clsCommon.AddColumnsForChange(coll, "Level4_User_code", obj.Level4_User_code)
            clsCommon.AddColumnsForChange(coll, "Level5_User_code", obj.Level5_User_code)
            If (clsCommon.CompairString(obj.Payment_Type, "PY") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal) Then
                obj.Debit_Account = clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.MP_CODE = '" + obj.Farmer_Code + "' ", trans)

                If clsCommon.myLen(obj.Location_GL_Code) <= 0 Then
                    obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "')", trans))
                End If

                obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
            ElseIf clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal Then
                obj.Debit_Account = clsDBFuncationality.getSingleValue("select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.MP_CODE = '" + obj.Farmer_Code + "'", trans)
                '' Anubhooti 27-Mar-2015 (Advance/On-Account: Debit Amount should be Advance_Against_Salary instead of advance account if Advance_Against_Salary is checked)
                If clsCommon.myCdbl(obj.Advance_Against_Salary) = 1 AndAlso (clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal) Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Salary,'') AS Advance_Against_Salary  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.MP_CODE = '" + obj.Farmer_Code + "'", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill advance against salary account on vendor account set")
                    End If
                End If
                '============Commented By Rohit After Talked with Anubhuti and Amit Sir(09-Jul-2015)=====
                'obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
                '=====================================================================================
                '' Anubhooti 31-Mar-2015 (Receipt/Security Refund :If security is checked then security account will go on GL)
                If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Is_Security) = 1 Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.SECURITY_ACCOUNT,'') AS SECURITY_ACCOUNT from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.MP_CODE = '" + obj.Farmer_Code + "' ", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill security account on vendor account set.")
                    End If
                Else
                    '============Add By Rohit After Talked with Anubhuti and Amit Sir(09-Jul-2015)=====
                    If clsCommon.myLen(obj.Location_GL_Code) <= 0 Then
                        obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "')", trans))
                    End If
                    obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
                    '=======================================================
                End If
            End If
            obj.Credit_Account = clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + obj.Bank_Code + "'", trans)
            If clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Debit_Account", obj.Credit_Account)
                clsCommon.AddColumnsForChange(coll, "Credit_Account", obj.Debit_Account)
            Else
                clsCommon.AddColumnsForChange(coll, "Debit_Account", obj.Debit_Account)
                clsCommon.AddColumnsForChange(coll, "Credit_Account", obj.Credit_Account)
            End If
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Applied_Amount", obj.Total_Applied_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Security_Amount", obj.Total_Security_Amount)
            clsCommon.AddColumnsForChange(coll, "Transport_Id", obj.Transport_Id)
            clsCommon.AddColumnsForChange(coll, "FIFO_Balance", obj.FIFO_Balance)
            clsCommon.AddColumnsForChange(coll, "QuickEntryNo", obj.QuickEntryNo)
            clsCommon.AddColumnsForChange(coll, "LoadOutNo", obj.LoadOutNo)
            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
            clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
            clsCommon.AddColumnsForChange(coll, "Route_NO", obj.Route_NO)
            clsCommon.AddColumnsForChange(coll, "Route_Description", obj.Route_Description)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Description", obj.Location_Description)
            clsCommon.AddColumnsForChange(coll, "IsRecoCleared", obj.IsRecoCleared)
            clsCommon.AddColumnsForChange(coll, "IsChkReverse", obj.IsChkReverse)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            If clsCommon.CompairString(obj.Payment_Type, "MI") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Loadout_No", obj.Loadout_No)
            End If
            clsCommon.AddColumnsForChange(coll, "Bank_Charges", obj.Bank_Charges)
            If obj.Bank_Charges > 0 Then
                obj.Bank_Charges_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CREDITACC from TSPL_BANK_MASTER Where BANK_CODE='" + obj.Bank_Code + "'", trans))
            End If
            clsCommon.AddColumnsForChange(coll, "Bank_Charges_Ac", obj.Bank_Charges_Ac)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", IIf(clsCommon.myLen(obj.CURRENCY_CODE) <= 0, objCommonVar.BaseCurrencyCode, obj.CURRENCY_CODE), True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", IIf(obj.ConvRate <= 0, 1, obj.ConvRate))


            If obj.ApplicableFrom IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)

            End If


            clsCommon.AddColumnsForChange(coll, "BASE_CURRENCY_CODE", IIf(clsCommon.myLen(obj.BASE_CURRENCY_CODE) <= 0, objCommonVar.BaseCurrencyCode, obj.BASE_CURRENCY_CODE), True)
            clsCommon.AddColumnsForChange(coll, "PAYMENT_AMOUNT_BASE_CURRENCY", obj.PAYMENT_AMOUNT_BASE_CURRENCY)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_AMT", obj.EXCHANGE_LOSS_AMT)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_AMT", obj.EXCHANGE_GAIN_AMT)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_ACCOUNT", obj.EXCHANGE_LOSS_ACCOUNT, True)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_ACCOUNT", obj.EXCHANGE_GAIN_ACCOUNT, True)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRateOld)
            '' End currencyconversion
            '''' code by priti for PJC entry
            'If clsCommon.myLen(obj.PROJECT_CODE) > 0 Then

            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PROJECT_CODE", obj.PROJECT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No, True)
            'End If
            '''' priti code ends here
            If clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Applied_Payment", obj.Applied_Payment)
            End If
            clsCommon.AddColumnsForChange(coll, "memorandum_amt", obj.memorndmamt)
            clsCommon.AddColumnsForChange(coll, "is_Opening", IIf(obj.is_Opening, 1, 0))
            'If isNewEntry Then
            '    qry = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "'"
            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            '    Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
            '    Dim BankAcc As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
            '    If (BankAcc.Length >= 3) Then
            '        BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
            '        ''No need to check becaulse all acount will be now with location segment.
            '        'If (IsNumeric(BankAcc)) Then
            '        '    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
            '        'End If
            '    Else
            '        Throw New Exception("Bank Master's Bank Account should be have location segment Type")
            '    End If

            '    Dim strPaymentType As String = clsDocType.Payment
            '    If clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PaymentReceiptTypeRunReceiptCounter, clsFixedParameterCode.PaymentReceiptTypeRunReceiptCounter, trans)) = 1 Then
            '        strPaymentType = clsDocType.Receipt
            '    End If


            '    If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
            '        obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.Bank, BankAcc, True)
            '    ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
            '        obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.Cash, BankAcc, True)
            '    ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
            '        obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.PettyCash, BankAcc, True)
            '    ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
            '        obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.Others, BankAcc, True)
            '    ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
            '        obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.Others, BankAcc, True)
            '    Else
            '        Throw New Exception("Plase set the Bank Type for Bank SETTLEMENT")
            '    End If
            '    clsCommon.AddColumnsForChange(coll, "Payment_No", obj.Payment_No)
            '    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            '    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_HEAD", OMInsertOrUpdate.Insert, "", trans)
            'Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_HEAD", OMInsertOrUpdate.Update, "Payment_No='" + obj.Payment_No + "'", trans)
            'End If
            If clsCommon.myLen(obj.Document_No) > 0 Then
                qry = "Update TSPL_PJC_EXPENSE_HEADER set Payment_No='" & obj.Payment_No & "',Posted='Y' ,Posting_Date='" & clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy hh:mm tt") & "' WHERE Document_No ='" + obj.Document_No + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            '----------------Remmitance ENtry------------------------
            If clsCommon.myCdbl(obj.TDS_Amount) > 0 Then
                If clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Then
                    clsRemittance.SaveData(objRemittance, obj.Payment_No, obj.Location_GL_Code, trans)
                End If
            End If
            '--------------------------------------------------------
            isSaved = isSaved AndAlso clsFarmerPaymentDetail.SaveData(obj.Payment_No, obj.ArrTr, trans)
            '' update currency loss and gain in case of payment type entr
            'If obj.ConvRate <> 1 Then
            '    If obj.Payment_Type = "PY" Then
            '        Dim obj1 As New clsFarmerPaymentHeader
            '        Dim diff As Double = 0.0
            '        diff = obj.PAYMENT_AMOUNT_BASE_CURRENCY - clsFarmerPaymentDetail.GetAppliedAmountInBaseCurrency(obj.Payment_No, trans)
            '        If diff = 0 Then
            '            obj1.EXCHANGE_LOSS_AMT = 0
            '            obj1.EXCHANGE_GAIN_AMT = 0
            '        ElseIf diff > 0 Then
            '            obj1.EXCHANGE_LOSS_AMT = diff
            '            obj1.EXCHANGE_GAIN_AMT = 0
            '        Else
            '            obj1.EXCHANGE_LOSS_AMT = 0
            '            obj1.EXCHANGE_GAIN_AMT = -diff
            '        End If
            '        Dim coll1 As New Hashtable()
            '        clsCommon.AddColumnsForChange(coll1, "EXCHANGE_LOSS_AMT", obj1.EXCHANGE_LOSS_AMT)
            '        clsCommon.AddColumnsForChange(coll1, "EXCHANGE_GAIN_AMT", obj1.EXCHANGE_GAIN_AMT)
            '        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_MP_PAY_HEAD", OMInsertOrUpdate.Update, "Payment_No='" + obj.Payment_No + "'", trans)
            '    End If
            'Else
            '    Dim coll1 As New Hashtable()
            '    clsCommon.AddColumnsForChange(coll1, "EXCHANGE_LOSS_AMT", 0)
            '    clsCommon.AddColumnsForChange(coll1, "EXCHANGE_GAIN_AMT", 0)
            'End If

            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Payment_No, obj.arrCustomFields, trans)
            '' check for bankBookentry against ticket No:BM00000008469
            If clsCommon.CompairString(obj.Payment_Type, "AD") <> CompairStringResult.Equal Then
                qry = "select count(ID) as Rec from TSPL_BANK_BOOK where SOURCEDOC_NO='" & obj.Payment_No & "' and DocType='Payment'"
                Dim totalRec As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                If totalRec <= 0 Then
                    Throw New Exception("Payment No-" & obj.Payment_No & " could not sent to Bank Book")
                End If
            End If
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-PY' and  source_doc_no='" & obj.Payment_No & "'", trans))
            clsFarmerPaymentHeader.CreateJournalEntry(obj, "MPayable", Voucher_No, trans)
        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

End Class


Public Class clsFarmerPaymentDetail
#Region "Variables"
    Public Payment_Line_No As Integer = 0
    Public Payment_No As String = Nothing
    Public PaymentDate As String
    Public DocumentDate As String
    Public Apply As String = Nothing
    Public Payment_Type As String = Nothing
    Public PurchaseInvoice As String = Nothing
    Public Document_No As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public Pending_Balance As Double = 0.0
    Public Applied_Amount As Double = 0.0
    Public Security_Amount As Double = 0.0
    Public Original_Invoice_Amt As Double = 0.0
    Public TDS_Amount As Double = 0.0
    Public Net_Balance As Double = 0.0
    Public Account_Code As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Comment As String = Nothing
    Public ESI_WCT_Percentage As Double = 0.0
    Public Post As String = Nothing
    Public Settlement_code As String = Nothing
    Public Settlement_Description As String = Nothing
    Public EXPENSE_CODE As String = Nothing
    Public ConvRateOld As Decimal = 1
    Public Cost_Center_Fin_Code As String = Nothing
    Public Cost_Center_Fin_Name As String = Nothing ''Not a table column
    Public Hirerachy_Level_Code As String = Nothing
    Public Hirerachy_Level_Name As String = Nothing ''Not a table column


#End Region

    Public Shared Function SaveData(ByVal strPaymentNo As String, ByVal Arr As List(Of clsFarmerPaymentDetail), ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsFarmerPaymentDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Payment_Line_No", intLineNo)
                clsCommon.AddColumnsForChange(coll, "Payment_No", strPaymentNo)
                clsCommon.AddColumnsForChange(coll, "Apply", obj.Apply)
                clsCommon.AddColumnsForChange(coll, "Payment_Type", obj.Payment_Type)
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", obj.Vendor_Invoice_No)
                clsCommon.AddColumnsForChange(coll, "Original_Invoice_Amt", obj.Original_Invoice_Amt)
                clsCommon.AddColumnsForChange(coll, "TDS_Amount", obj.TDS_Amount)
                clsCommon.AddColumnsForChange(coll, "Pending_Balance", obj.Pending_Balance)
                clsCommon.AddColumnsForChange(coll, "Applied_Amount", obj.Applied_Amount)
                clsCommon.AddColumnsForChange(coll, "Security_Amount", obj.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "Net_Balance", obj.Net_Balance)
                clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
                clsCommon.AddColumnsForChange(coll, "ESI_WCT_Percentage", obj.ESI_WCT_Percentage)
                clsCommon.AddColumnsForChange(coll, "Post", obj.Post)
                clsCommon.AddColumnsForChange(coll, "Settlement_code", obj.Settlement_code)
                clsCommon.AddColumnsForChange(coll, "Settlement_Description", obj.Settlement_Description)
                clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRateOld)
                clsCommon.AddColumnsForChange(coll, "Cost_Center_Fin_Code", obj.Cost_Center_Fin_Code, True)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Level_Code", obj.Hirerachy_Level_Code, True)
                '''' code by priti for PJC entry
                If clsCommon.myLen(obj.EXPENSE_CODE) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EXPENSE_CODE", obj.EXPENSE_CODE, True)
                End If
                '''' priti code ends here
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function
    Public Shared Function GetAppliedAmountInBaseCurrency(ByVal PaymentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select coalesce(sum(Applied_Amount*ConvRateOld),0) as AppliedAmtBase from TSPL_MP_PAY_DETAIL where Payment_No='" & PaymentNo & "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

End Class

