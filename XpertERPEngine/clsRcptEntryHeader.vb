'--Updation By--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000001264]
Imports System.Data.SqlClient
Imports common
Public Class clsRcptEntryHeader

#Region "Variables"
    Public Booking_Code As String = Nothing
    Public CSATransfer_No As String = Nothing
    Public SecurityDepositType As String = Nothing
    Public memorndmamt As String = Nothing
    Public Receipt_No As String = Nothing
    Public isCardSale As Integer = 0
    Public Rec_Route_Code As String = String.Empty
    Public Rec_Zone_Code As String = String.Empty
    Public Foreign_Bank_Charges_Amt As Double = 0
    Public Bank_Charges_Amt As Double = 0
    Public Receipt_Date As Date
    Public Receipt_Post_Date As Date
    Public Bank_Code As String = Nothing
    Public DateAndTime As DateTime?
    Public TapalNo As String = String.Empty
    Public Receipt_Type As String = Nothing
    Public IsApplyDocAuto As Integer = 0
    Public Cust_Code As String = Nothing
    Public Against_RCDF_Loadin As String = Nothing
    Public Customer_Name As String = Nothing
    Public Entry_Desc As String = Nothing
    Public Reference As String = Nothing
    Public Narration As String = Nothing
    Public Payment_Code As String = Nothing
    Public IsAutoApplyDoc_Refund As Integer = 0
    Public Cheque_No As String = Nothing
    Public Cheque_Date As String = Nothing
    Public Cheque_From As String = Nothing
    Public From_Branch As String = Nothing
    Public Receipt_Amount As Double = 0.0
    Public Cust_Account As String = Nothing
    Public Apply_By As String = Nothing
    Public Apply_To As String = Nothing
    Public Posted As String = "N"
    Public Level1_User_code As String = Nothing
    Public Level2_User_code As String = Nothing
    Public Level3_User_code As String = Nothing
    Public Level4_User_code As String = Nothing
    Public Level5_User_code As String = Nothing
    Public Balance_Amt As Double = 0.0
    Public UnApply_Amt As Double = 0.0
    Public Document_No As String = Nothing
    Public Payer As String = Nothing
    Public Dr_Account As String = Nothing
    Public Cr_Account As String = Nothing
    Public UnApplied_Balance As Double = 0.0
    Public UnApplied_No As String = Nothing
    Public FIFO_Balance As Double = 0.0
    Public QuickEntryNo As String = Nothing
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public IsSalesmanType As String = "N"
    Public SecurityDeposit As String = "N"
    Public IsRecoCleared As String = "N"
    Public IsChkReverse As String = "N"
    Public Loadout_No As String = Nothing
    Public Applied_Receipt As String = Nothing
    Public ArrTr As List(Of clsReceiptDettail) = Nothing
    Public ArrTrGST As List(Of clsReceiptDetailGST) = Nothing
    Public ArrTrRefund As List(Of clsReceiptDetail_Refund) = Nothing
    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public BASE_CURRENCY_CODE As String
    Public RECEIVED_AMOUNT_BASE_CURRENCY As Double = 0.0
    Public EXCHANGE_LOSS_AMT As Double = 0.0
    Public EXCHANGE_GAIN_AMT As Double = 0.0
    Public EXCHANGE_LOSS_ACCOUNT As String
    Public EXCHANGE_GAIN_ACCOUNT As String
    Public ConvRateOld As Decimal
    Public IsParentCust As Boolean = False
    Public CHECK_PRINT As Integer = 0
    Public CHECK_CODE As String = Nothing
    Public CForm_InvoiceNo As String = Nothing
    Public CFormRecd As String = "N"
    Public Form_ID As String = ""
    Public AUTO_GEN_BT_ENTRY As Boolean = False
    Public TO_BANK_CODE As String = Nothing
    Public Transfer_No As String = Nothing
    Public SaleOrderNo As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Location_GL_Code As String = Nothing
    Public is_Opening As Boolean = False
    Public Distr_Code As String = ""
    Public Delivery_Code_PS As String = String.Empty
    Public Tax_Group As String = String.Empty
    Public Tax_Amount_Advance As Double = 0.0
    Public Delivery_order_Amount As Double = 0.0
    Public DO_Total_Add_Amount As Double = 0.0
    Public SO_Location_Code As String = String.Empty
    Public TAX1 As String = String.Empty
    Public TAX1_Amt As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public tax2 As String = String.Empty
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = String.Empty
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = String.Empty
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public tax5 As String = String.Empty
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public tax6 As String = String.Empty
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public tax7 As String = String.Empty
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public tax8 As String = String.Empty
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public tax9 As String = String.Empty
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public tax10 As String = String.Empty
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public ReceiptAgainstSO_DO As String = String.Empty
    Public AC_Payee As Integer = 0
    Public cheque_in_favour_of As String = String.Empty
    Public Set_Off_Date As Date?
    Public SetOffSkipJE As Boolean

#End Region
    ''To be Uncomment
    'Public Function SaveData(ByVal obj As clsRcptEntryHeader, ByVal isNewEntry As Boolean, Optional ByVal objBT As FrmBankTransfer = Nothing) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        If Not objBT Is Nothing Then
    '            obj.Transfer_No = objBT.SaveAndReturnDocNo(True, trans)
    '        End If
    '        SaveData(obj, isNewEntry, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
    Public Function SaveData(ByVal obj As clsRcptEntryHeader, ByVal isNewEntry As Boolean) As Boolean
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

    
    Public Function SaveData(ByVal obj As clsRcptEntryHeader, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal CreateNewDocumentNoWithExistingDocumentNo As String = Nothing) As Boolean

        Dim isSaved As Boolean = True
        Try
            '--------------------Checks Whether the Transaction is Locked or not----------------------------UDL/24/07/18-000206 richa 
            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans)

            Dim strAllowtoUnlockTransactionsforSetOff As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoUnlockTransactionsforSetOff, clsFixedParameterCode.AllowtoUnlockTransactionsforSetOff, trans))
            If clsCommon.CompairString(strAllowtoUnlockTransactionsforSetOff, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal Then
            Else
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "Receipt Entry", LocSegmentCode, obj.Receipt_Date, trans)
            End If
            Dim qry As String = ""



            '-----------------------------------------------------------------------------------------------
            If clsCommon.myLen(obj.Receipt_No) > 0 Then
                qry = clsDBFuncationality.getSingleValue("Select Posted from TSPL_RECEIPT_HEADER Where Receipt_No='" + obj.Receipt_No + "'", trans)
                If clsCommon.CompairString(qry, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Document already posted")
                End If
            End If
            If clsCommon.myLen(obj.Against_RCDF_Loadin) > 0 Then
                qry = "select Customer_code from TSPL_RCDF_LOAD_IN where Document_Code='" + obj.Against_RCDF_Loadin + "' "
                qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If Not clsCommon.CompairString(qry, obj.Cust_Code) = CompairStringResult.Equal Then
                    Throw New Exception("Loadin Customer is [" + qry + "] but Receipt entry customer is [" + obj.Cust_Code + "].Both Should be same")
                End If
            End If
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Receipt_No, "TSPL_RECEIPT_HEADER", "Receipt_No", "TSPL_RECEIPT_DETAIL", "Receipt_No", "TSPL_RECEIPT_DETAIL_GST", "Receipt_No", "TSPL_RECEIPT_DETAIL_Refund", "Receipt_No", "", "", "", "", "", "", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Receipt_No, "TSPL_bank_book", "SOURCEDOC_NO", trans)
            End If

            qry = "DELETE TSPL_RECEIPT_DETAIL WHERE Receipt_No ='" + obj.Receipt_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE TSPL_RECEIPT_DETAIL_GST WHERE Receipt_No ='" + obj.Receipt_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE TSPL_RECEIPT_DETAIL_Refund WHERE Receipt_No ='" + obj.Receipt_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Receipt_Date", clsCommon.GetPrintDate(obj.Receipt_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Receipt_Post_Date", clsCommon.GetPrintDate(obj.Receipt_Post_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Receipt_Type", obj.Receipt_Type)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Against_RCDF_Loadin", obj.Against_RCDF_Loadin, True)
            clsCommon.AddColumnsForChange(coll, "Rec_Route_Code", obj.Rec_Route_Code, True)
            clsCommon.AddColumnsForChange(coll, "Rec_Zone_Code", obj.Rec_Zone_Code, True)
            clsCommon.AddColumnsForChange(coll, "isCardSale", obj.isCardSale)

            clsCommon.AddColumnsForChange(coll, "IsApplyDocAuto", obj.IsApplyDocAuto)
            clsCommon.AddColumnsForChange(coll, "IsAutoApplyDoc_Refund", obj.IsAutoApplyDoc_Refund)
            Dim chkInactiveCustomer As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Status from TSPL_CUSTOMER_MASTER Where Cust_Code ='" + obj.Cust_Code + "'", trans))
            If clsCommon.CompairString(chkInactiveCustomer, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Payment can not be created for Inactive Customer")
            End If
            obj.Customer_Name = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHere Cust_Code ='" + obj.Cust_Code + "'", trans)
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Entry_Desc", obj.Entry_Desc)
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Narration", obj.Narration)
            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            If clsCommon.myLen(obj.Cheque_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", "")
            End If
            '' Anubhooti 07-Jan-2014 BM00000005309
            clsCommon.AddColumnsForChange(coll, "Location_GL_Code", obj.Location_GL_Code, True)
            clsCommon.AddColumnsForChange(coll, "Cheque_From", obj.Cheque_From)
            clsCommon.AddColumnsForChange(coll, "From_Branch", obj.From_Branch)
            clsCommon.AddColumnsForChange(coll, "Receipt_Amount", obj.Receipt_Amount)
            obj.Cust_Account = clsDBFuncationality.getSingleValue("Select Cust_Account from TSPL_CUSTOMER_MASTER WHere Cust_Code ='" + obj.Cust_Code + "'", trans)
            clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account)
            clsCommon.AddColumnsForChange(coll, "Apply_By", obj.Apply_By)
            clsCommon.AddColumnsForChange(coll, "Apply_To", obj.Apply_To)
            clsCommon.AddColumnsForChange(coll, "Level1_User_code", obj.Level1_User_code)
            clsCommon.AddColumnsForChange(coll, "Level2_User_code", obj.Level2_User_code)
            clsCommon.AddColumnsForChange(coll, "Level3_User_code", obj.Level3_User_code)
            clsCommon.AddColumnsForChange(coll, "Level4_User_code", obj.Level4_User_code)
            clsCommon.AddColumnsForChange(coll, "Level5_User_code", obj.Level5_User_code)

            clsCommon.AddColumnsForChange(coll, "CFormRecd", obj.CFormRecd)
            clsCommon.AddColumnsForChange(coll, "CForm_InvoiceNo", obj.CForm_InvoiceNo)

            clsCommon.AddColumnsForChange(coll, "Foreign_Bank_Charges_Amt", obj.Foreign_Bank_Charges_Amt)
            clsCommon.AddColumnsForChange(coll, "Bank_Charges_Amt", obj.Bank_Charges_Amt)

            clsCommon.AddColumnsForChange(coll, "TapalNo", obj.TapalNo, True)
            If clsCommon.myLen(obj.DateAndTime) > 0 Then
                clsCommon.AddColumnsForChange(coll, "DateAndTime", clsCommon.GetPrintDate(obj.DateAndTime, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "DateAndTime", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Delivery_Code_PS", obj.Delivery_Code_PS)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Amount_Advance", obj.Tax_Amount_Advance)
            clsCommon.AddColumnsForChange(coll, "Delivery_order_Amount", obj.Delivery_order_Amount)
            clsCommon.AddColumnsForChange(coll, "DO_Total_Add_Amount", obj.DO_Total_Add_Amount)
            clsCommon.AddColumnsForChange(coll, "SO_Location_Code", obj.SO_Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "ReceiptAgainstSO_DO", obj.ReceiptAgainstSO_DO)

            '' Anubhooti 01-Jan-2015 (Remarks : if setting "AllowToUseSubAccount" is ON And Bank_Type should be "B" Then BankAccount is Sub_Account Else previous)
            Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
            '' Anubhooti 03-Sep-2014 BM00000003437(Remarks : if setting "AllowToUseSubAccount" is ON Then BankAccount is Sub_Account Else previous)
            Dim UseSubAcc As String
            'Dim strQ1 As String
            'Dim strBankAcc As String

            UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                qry = "select ISNULL(Sub_Account,'')  BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(obj.Bank_Code) + "'"
                obj.Dr_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                'strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            Else
                qry = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(obj.Bank_Code) + "'"
                obj.Dr_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                'strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            End If
            If clsCommon.myLen(obj.Dr_Account) <= 0 Then
                If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(obj.Bank_Code))
                Else
                    Throw New Exception("Please enter bank account for bank " + clsCommon.myCstr(obj.Bank_Code))
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(obj.Receipt_Type), "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(obj.Receipt_Type), "K") = CompairStringResult.Equal Then
                'Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                Dim strsecurityType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select SecurityDeposit from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(obj.Applied_Receipt) & "' ", trans))
                If clsCommon.CompairString(strsecurityType, "Y") = CompairStringResult.Equal Then
                    qry = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(obj.Cust_Code) + "'"
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Receipt_Type from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(obj.Applied_Receipt) & "' ", trans)), "O") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        qry = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(obj.Cust_Code) + "'"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_Customer_Invoice_Head where Document_No ='" & clsCommon.myCstr(obj.Applied_Receipt) & "' ", trans)), "C") = CompairStringResult.Equal Then
                        qry = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(obj.Cust_Code) + "'"
                    Else
                        qry = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(obj.Cust_Code) + "'"
                    End If
                End If
                obj.Dr_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                ' obj.Dr_Account  = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankAcc, tBankLocation, trans)

                If clsCommon.myLen(obj.Dr_Account) <= 0 Then
                    Throw New Exception("Please enter SECURITY_ACCOUNT/Receivable_Control_acct/Advance_acct for customer " + clsCommon.myCstr(obj.Cust_Code))
                End If

            End If
            '' Commented Old bank account as a debit account conditionally of sub account
            'qry = "select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + obj.Bank_Code + "'"
            'obj.Dr_Account = clsDBFuncationality.getSingleValue(qry, trans)
            ''
            'Dim strLocationSeg As String = obj.Dr_Account.Substring(clsCommon.myLen(obj.Dr_Account) - 3, 3)
            'obj.Dr_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Dr_Account, strLocationSeg, True, trans)
            obj.Dr_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Dr_Account, LocSegmentCode, True, trans)
            If clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                Dim strColumn As String = ""
                If clsCommon.CompairString(obj.SecurityDeposit, "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.SecurityDepositType) > 0 Then
                    If clsCommon.CompairString(obj.SecurityDepositType, "S") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT"
                    ElseIf clsCommon.CompairString(obj.SecurityDepositType, "C") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.CREATE_SECURITY_ACCOUNT"
                    ElseIf clsCommon.CompairString(obj.SecurityDepositType, "B") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.BANK_GUARANTEE"
                    ElseIf clsCommon.CompairString(obj.SecurityDepositType, "O") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT1"
                    ElseIf clsCommon.CompairString(obj.SecurityDepositType, "R") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT2"
                    End If
                Else
                    If clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(obj.Applied_Receipt), "") = CompairStringResult.Equal Then
                            strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct"
                        Else
                            strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct"
                        End If
                    Else
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct"
                    End If

                End If


                qry = "SELECT " & strColumn & " FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.Cust_Code + "' "
            Else
                qry = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.Cust_Code + "' "
            End If
            obj.Cr_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            obj.Cr_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Cr_Account, LocSegmentCode, True, trans)


            If clsCommon.CompairString(obj.Receipt_Type, "R") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "O") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Receipt_Type), "K") = CompairStringResult.Equal Then
                If clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.SecurityDeposit, "Y") = CompairStringResult.Equal Then
                    'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
                    Dim JEWithOPening As Boolean = False
                    If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                        Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                        If obj.Receipt_Date <= clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy") Then
                            clsCommon.AddColumnsForChange(coll, "Dr_Account", obj.Cr_Account)
                            clsCommon.AddColumnsForChange(coll, "Cr_Account", obj.Dr_Account)
                        Else
                            clsCommon.AddColumnsForChange(coll, "Dr_Account", obj.Dr_Account)
                            clsCommon.AddColumnsForChange(coll, "Cr_Account", obj.Cr_Account)
                        End If
                    End If
                Else
                    clsCommon.AddColumnsForChange(coll, "Dr_Account", obj.Dr_Account)
                    clsCommon.AddColumnsForChange(coll, "Cr_Account", obj.Cr_Account)
                End If



            ElseIf clsCommon.CompairString(obj.Receipt_Type, "M") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Dr_Account", obj.Dr_Account)
                clsCommon.AddColumnsForChange(coll, "Cr_Account", Nothing)
                clsCommon.AddColumnsForChange(coll, "Payer", obj.Payer)

                clsCommon.AddColumnsForChange(coll, "Loadout_No", obj.Loadout_No)

            ElseIf clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Dr_Account", obj.Cr_Account)
                clsCommon.AddColumnsForChange(coll, "Cr_Account", obj.Dr_Account)
            ElseIf clsCommon.CompairString(obj.Receipt_Type, "S") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Cr_Account", obj.Dr_Account)
                clsCommon.AddColumnsForChange(coll, "Dr_Account", Nothing)
                clsCommon.AddColumnsForChange(coll, "Payer", obj.Payer)
            End If
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "UnApply_Amt", obj.UnApply_Amt)
            clsCommon.AddColumnsForChange(coll, "UnApplied_Balance", obj.UnApplied_Balance)
            clsCommon.AddColumnsForChange(coll, "UnApplied_No", obj.UnApplied_No)
            clsCommon.AddColumnsForChange(coll, "FIFO_Balance", obj.FIFO_Balance)
            clsCommon.AddColumnsForChange(coll, "QuickEntryNo", obj.QuickEntryNo)
            clsCommon.AddColumnsForChange(coll, "SecurityDeposit", obj.SecurityDeposit)
            clsCommon.AddColumnsForChange(coll, "IsRecoCleared", obj.IsRecoCleared)
            clsCommon.AddColumnsForChange(coll, "IsChkReverse", obj.IsChkReverse)
            clsCommon.AddColumnsForChange(coll, "IsSalesmanType", obj.IsSalesmanType)
            If clsCommon.CompairString(obj.IsSalesmanType, "Y") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
                clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
            End If
            If clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Applied_Receipt", obj.Applied_Receipt)
            End If
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            obj.BASE_CURRENCY_CODE = objCommonVar.BaseCurrencyCode
            If clsCommon.myLen(obj.CURRENCY_CODE) <= 0 Then
                obj.CURRENCY_CODE = obj.BASE_CURRENCY_CODE
            End If
            If obj.ConvRate = 0 Then
                obj.ConvRate = 1
            End If
            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            If obj.ApplicableFrom.HasValue Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)
            End If

            obj.EXCHANGE_LOSS_ACCOUNT = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.EXCHANGE_LOSS_ACCOUNT, LocSegmentCode, True, trans)
            obj.EXCHANGE_GAIN_ACCOUNT = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.EXCHANGE_GAIN_ACCOUNT, LocSegmentCode, True, trans)

            clsCommon.AddColumnsForChange(coll, "BASE_CURRENCY_CODE", obj.BASE_CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "RECEIVED_AMOUNT_BASE_CURRENCY", obj.RECEIVED_AMOUNT_BASE_CURRENCY)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_AMT", obj.EXCHANGE_LOSS_AMT)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_AMT", obj.EXCHANGE_GAIN_AMT)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_ACCOUNT", obj.EXCHANGE_LOSS_ACCOUNT, True)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_ACCOUNT", obj.EXCHANGE_GAIN_ACCOUNT, True)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRateOld)
            clsCommon.AddColumnsForChange(coll, "IsParentCust", IIf(obj.IsParentCust = True, 1, 0))
            clsCommon.AddColumnsForChange(coll, "CHECK_PRINT", obj.CHECK_PRINT)

            clsCommon.AddColumnsForChange(coll, "AUTO_GEN_BT_ENTRY", obj.AUTO_GEN_BT_ENTRY)
            clsCommon.AddColumnsForChange(coll, "TO_BANK_CODE", obj.TO_BANK_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Transfer_No", obj.Transfer_No, True)
            '' End currencyconversion

            clsCommon.AddColumnsForChange(coll, "memorandum_amt", obj.memorndmamt)
            clsCommon.AddColumnsForChange(coll, "SecurityDepositType", obj.SecurityDepositType)
            '-----------------richa 28/08/2014 Against Ticket No .BM00000003667---------
            clsCommon.AddColumnsForChange(coll, "SaleOrderNo", obj.SaleOrderNo, True)
            ''------------------

            clsCommon.AddColumnsForChange(coll, "Against_CSA_Transfer_Code", obj.CSATransfer_No)
            If clsCommon.myLen(obj.Booking_Code) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Booking_Code", obj.Booking_Code)
            Else
                clsCommon.AddColumnsForChange(coll, "Booking_Code", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "is_Opening", IIf(obj.is_Opening, 1, 0))
            '' Done by Panch Raj(whollycow)
            clsCommon.AddColumnsForChange(coll, "Distr_Code", obj.Distr_Code, True)

            ''RICHA 
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "tax2", obj.tax2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "tax5", obj.tax5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "tax6", obj.tax6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "tax7", obj.tax7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "tax8", obj.tax8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "tax9", obj.tax9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "tax10", obj.tax10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)

            ''---------
            clsCommon.AddColumnsForChange(coll, "AC_Payee", obj.AC_Payee)
            clsCommon.AddColumnsForChange(coll, "cheque_in_favour_of", obj.cheque_in_favour_of)
            Dim SkipJe As Integer = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSkipJournalEntryofPaymentandReceiptforAD, clsFixedParameterCode.AllowtoSkipJournalEntryofPaymentandReceiptforAD, trans))
            If clsCommon.CompairString(SkipJe, "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(obj.Receipt_Type), "A") = CompairStringResult.Equal) Then
                obj.SetOffSkipJE = True
            Else
                obj.SetOffSkipJE = False
            End If
            '' setof columns that are used in Receipt triggers to skip them
            '' Set_Off_Date only be assigned from Customer SetOff screen. on all other screens it will be null
            If Not obj.Set_Off_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Set_Off_Date", clsCommon.GetPrintDate(obj.Set_Off_Date, "dd-MMM-yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "SetOffSkipJE", If(obj.SetOffSkipJE = True, "1", "0"))

            If isNewEntry Then
                qry = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                Dim BankAcc As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                'No need To Check because now all account with location segment.
                If (BankAcc.Length >= 3) Then
                    BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                    'If (IsNumeric(BankAcc)) Then
                    '    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                    'End If
                Else
                    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                End If
                ''richa agarwal 27 Aug,2018
                If clsCommon.myLen(CreateNewDocumentNoWithExistingDocumentNo) > 0 And isNewEntry = True Then
                    obj.Receipt_No = CreateNewDocumentNoWithExistingDocumentNo
                Else
                    If clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then '--in Case of Refund Document COunter will be of Payment
                        If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Tax_Group) <= 0 Then
                            obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Payment, clsDocTransactionType.Bank, BankAcc, True)
                        ElseIf clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Tax_Group) > 0 Then
                            obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Payment, clsDocTransactionType.Tax, BankAcc, True)
                        ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
                            obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Payment, clsDocTransactionType.Cash, BankAcc, True)
                        ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
                            obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Payment, clsDocTransactionType.PettyCash, BankAcc, True)
                        ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
                            obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Payment, clsDocTransactionType.Others, BankAcc, True)
                        ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
                            obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Payment, clsDocTransactionType.Others, BankAcc, True)
                        Else
                            Throw New Exception("Please set the Bank Type for Bank " + obj.Bank_Code)
                        End If
                    Else

                        ''====Added by Parteek 27/09/2017 Receipt_No location wise

                        If clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Tax_Group) > 0 AndAlso clsCommon.myLen(obj.Document_No) <= 0 Then
                            obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Normal, obj.Location_GL_Code, True)
                            If clsCommon.myLen(obj.Receipt_No) <= 0 Then
                                Throw New Exception("Please set the location " + obj.Location_GL_Code)
                            End If
                        ElseIf clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseApplyDocSeriesForReceipt, clsFixedParameterCode.AllowUseApplyDocSeriesForReceipt, trans)), "1") = CompairStringResult.Equal Then
                            obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.ApplyDoc, BankAcc, True)
                        Else
                            If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Tax_Group) <= 0 Then
                                obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Bank, BankAcc, True)
                            ElseIf clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Tax_Group) > 0 Then
                                obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Tax, BankAcc, True)
                            ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
                                obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Cash, BankAcc, True)
                            ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
                                obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.PettyCash, BankAcc, True)
                            ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
                                obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Others, BankAcc, True)
                            ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
                                obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Settlement, BankAcc, True)
                            Else
                                Throw New Exception("Please set the Bank Type for Bank " + obj.Bank_Code)
                            End If
                        End If

                        ''=====End
                    End If
                End If
                ''------------
                clsCommon.AddColumnsForChange(coll, "Receipt_No", obj.Receipt_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_HEADER", OMInsertOrUpdate.Update, "Receipt_No='" + obj.Receipt_No + "'", trans)
            End If

            isSaved = isSaved AndAlso clsReceiptDettail.SaveData(obj.Receipt_No, obj.ArrTr, obj.Receipt_Type, trans)
            isSaved = isSaved AndAlso clsReceiptDetailGST.SaveData(obj.Receipt_No, obj.ArrTrGST, trans)
            isSaved = isSaved AndAlso clsReceiptDetail_Refund.SaveData(obj.Receipt_No, obj.ArrTrRefund, obj.Receipt_Type, trans)
            ''richa agarwal 27 Aug,2018
            If clsCommon.myLen(CreateNewDocumentNoWithExistingDocumentNo) <= 0 Then
                If clsCommon.CompairString(SkipJe, "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(obj.Receipt_Type), "A") = CompairStringResult.Equal) Then
                Else
                    ''richa 12 Nov,2018  TEC/02/11/18-000363 create journal entry for opening in case of Misc receipt and Advance (Security) as Journal Master table instead of journal master op table
                    'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
                    Dim JEWithOPening As Boolean = False
                    If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                        Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                        If obj.Receipt_Date <= clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy") Then
                            JEWithOPening = True
                        End If
                    End If

                    Dim strQ As String = GetQuery(obj.Receipt_No)
                    Dim sqlDr As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)

                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, trans)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(obj.Receipt_Type), "P") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Receipt_Type), "M") = CompairStringResult.Equal) And JEWithOPening = True Then
                        isSaved = isSaved AndAlso CreateJournalEntryForOpening(True, sqlDr, obj.Receipt_No, LocSegmentCode, trans, "")
                    Else
                        isSaved = isSaved AndAlso CreateJournalEntry(True, sqlDr, obj.Receipt_No, LocSegmentCode, trans, "")
                    End If
                    ''-----------------

                    'Dim strQ As String = GetQuery(obj.Receipt_No)
                    'Dim sqlDr As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)

                    'isSaved = isSaved AndAlso CreateJournalEntry(True, sqlDr, obj.Receipt_No, LocSegmentCode, trans, "")
                End If
            End If

            '' update currency loss and gain in case of payment type entr
            'If obj.ConvRate <> 1 Then
            '    Dim obj1 As New clsRcptEntryHeader
            '    Dim diff As Double = 0.0
            '    If obj.Receipt_Type = "R" Then

            '        diff = obj.RECEIVED_AMOUNT_BASE_CURRENCY - clsReceiptDettail.GetAppliedAmountInBaseCurrency(obj.Receipt_No, trans)
            '        If diff = 0 Then
            '            obj1.EXCHANGE_LOSS_AMT = 0
            '            obj1.EXCHANGE_GAIN_AMT = 0
            '        ElseIf diff < 0 Then
            '            obj1.EXCHANGE_LOSS_AMT = -diff
            '            obj1.EXCHANGE_GAIN_AMT = 0
            '        Else
            '            obj1.EXCHANGE_LOSS_AMT = 0
            '            obj1.EXCHANGE_GAIN_AMT = diff
            '        End If

            '        'If diff = 0 Then
            '        '    obj1.EXCHANGE_LOSS_AMT = 0
            '        '    obj1.EXCHANGE_GAIN_AMT = 0
            '        'ElseIf diff > 0 Then
            '        '    obj1.EXCHANGE_LOSS_AMT = diff
            '        '    obj1.EXCHANGE_GAIN_AMT = 0
            '        'Else
            '        '    obj1.EXCHANGE_LOSS_AMT = 0
            '        '    obj1.EXCHANGE_GAIN_AMT = -diff
            '        'End If
            '        Dim coll1 As New Hashtable()
            '        clsCommon.AddColumnsForChange(coll1, "EXCHANGE_LOSS_AMT", obj1.EXCHANGE_LOSS_AMT)
            '        clsCommon.AddColumnsForChange(coll1, "EXCHANGE_GAIN_AMT", obj1.EXCHANGE_GAIN_AMT)
            '        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_RECEIPT_HEADER", OMInsertOrUpdate.Update, "Receipt_No='" + obj.Receipt_No + "'", trans)
            '    End If
            'Else
            '    Dim coll1 As New Hashtable()
            '    clsCommon.AddColumnsForChange(coll1, "EXCHANGE_LOSS_AMT", 0)
            '    clsCommon.AddColumnsForChange(coll1, "EXCHANGE_GAIN_AMT", 0)
            '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_RECEIPT_HEADER", OMInsertOrUpdate.Update, "Receipt_No='" + obj.Receipt_No + "'", trans)
            'End If

            UpdateBankBook(obj, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Receipt_No, obj.arrCustomFields, trans)
            ''richa agarwal 4 Nov,2019 to save receipt no into booking master table
            If clsCommon.myLen(obj.Booking_Code) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_BOOKING_MATSER set Against_Receipt_No ='" & obj.Receipt_No & "' where Document_No='" & obj.Booking_Code & "' ", trans)
            End If

            coll = Nothing
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function funUnAppliedEntry(ByVal obj As clsRcptEntryHeader, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "DELETE TSPL_RECEIPT_DETAIL WHERE Receipt_No ='" + obj.UnApplied_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strReceiptNo As String = obj.Receipt_No
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Receipt_Date", clsCommon.GetPrintDate(obj.Receipt_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Receipt_Post_Date", clsCommon.GetPrintDate(obj.Receipt_Post_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Receipt_Type", "U")
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            obj.Customer_Name = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHere Cust_Code ='" + obj.Cust_Code + "'", trans)
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Entry_Desc", "Unapplied receipt against " + obj.Receipt_No + "")
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Narration", obj.Narration)
            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            If clsCommon.myLen(obj.Cheque_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", "")
            End If
            clsCommon.AddColumnsForChange(coll, "Cheque_From", obj.Cheque_From)
            clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account)
            clsCommon.AddColumnsForChange(coll, "Apply_By", obj.Apply_By)
            clsCommon.AddColumnsForChange(coll, "Apply_To", obj.Apply_To)
            clsCommon.AddColumnsForChange(coll, "Level1_User_code", obj.Level1_User_code)
            clsCommon.AddColumnsForChange(coll, "Level2_User_code", obj.Level2_User_code)
            clsCommon.AddColumnsForChange(coll, "Level3_User_code", obj.Level3_User_code)
            clsCommon.AddColumnsForChange(coll, "Level4_User_code", obj.Level4_User_code)
            clsCommon.AddColumnsForChange(coll, "Level5_User_code", obj.Level5_User_code)

            If clsCommon.CompairString(obj.Receipt_Type, "R") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Dr_Account", obj.Dr_Account)
                clsCommon.AddColumnsForChange(coll, "Cr_Account", obj.Cr_Account)
            End If
            clsCommon.AddColumnsForChange(coll, "SecurityDeposit", obj.SecurityDeposit)
            clsCommon.AddColumnsForChange(coll, "IsRecoCleared", obj.IsRecoCleared)
            clsCommon.AddColumnsForChange(coll, "IsChkReverse", obj.IsChkReverse)
            If clsCommon.CompairString(obj.IsSalesmanType, "Y") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
                clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
            End If
            ''richa agarwal 14/oct/2016 BM00000009833
            obj.BASE_CURRENCY_CODE = objCommonVar.BaseCurrencyCode
            If clsCommon.myLen(obj.CURRENCY_CODE) <= 0 Then
                obj.CURRENCY_CODE = obj.BASE_CURRENCY_CODE
            End If
            If obj.ConvRate = 0 Then
                obj.ConvRate = 1
            End If
            If obj.ConvRateOld = 0 Then
                obj.ConvRateOld = 1
            End If
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "BASE_CURRENCY_CODE", obj.BASE_CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRateOld)

            ''----------------


            clsCommon.AddColumnsForChange(coll, "Receipt_Amount", obj.UnApplied_Balance)
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.UnApplied_Balance)
            clsCommon.AddColumnsForChange(coll, "UnApply_Amt", obj.UnApplied_Balance)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            Dim Count As Integer = clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_RECEIPT_HEADER WHERE Receipt_No ='" + obj.UnApplied_No + "' ", trans)

            ''richa ERO/21/10/19-001072
            qry = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
            Dim BankAcc As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
            If (BankAcc.Length >= 3) Then
                BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                '' Anubhooti 01-Dec-2014 
                'If (IsNumeric(BankAcc)) Then
                '    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                'End If
            Else
                Throw New Exception("Bank Master's Bank Account should be have location segment Type")
            End If

            If Count <= 0 Then
                'qry = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "'"
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                'Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                'Dim BankAcc As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                'If (BankAcc.Length >= 3) Then
                '    BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                '    '' Anubhooti 01-Dec-2014 
                '    'If (IsNumeric(BankAcc)) Then
                '    '    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                '    'End If
                'Else
                '    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                'End If
                If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                    obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Bank, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
                    obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Cash, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
                    obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.PettyCash, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
                    obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Others, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
                    obj.Receipt_No = clsERPFuncationality.GetNextCode(trans, obj.Receipt_Date, clsDocType.Receipt, clsDocTransactionType.Settlement, BankAcc, True)
                Else
                    Throw New Exception("Please set the Bank Type for Bank " + obj.Bank_Code)
                End If
                ''richa agarwal 21 Oct,2019 send bank location into location gl code column
                clsCommon.AddColumnsForChange(coll, "Location_GL_Code", BankAcc)
                clsCommon.AddColumnsForChange(coll, "Receipt_No", obj.Receipt_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                obj.Receipt_No = obj.UnApplied_No
                clsCommon.AddColumnsForChange(coll, "Receipt_No", obj.Receipt_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_HEADER", OMInsertOrUpdate.Update, "Receipt_No='" + obj.Receipt_No + "'", trans)
            End If
            qry = "Update TSPL_RECEIPT_HEADER SET UnApplied_No='" + obj.Receipt_No + "', UnApplied_Balance=" + clsCommon.myCstr(obj.UnApplied_Balance) + " WHERE Receipt_No='" + strReceiptNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsRcptEntryHeader
        Dim obj As clsRcptEntryHeader = Nothing

        Dim isCustomerFinderLocationWiseARReceipt As Boolean = False
        Dim strJointCond As String = ""

        Dim qry As String = "SELECT TSPL_RECEIPT_HEADER.Against_RCDF_Loadin, TSPL_RECEIPT_HEADER.isCardSale,TSPL_RECEIPT_HEADER.TapalNo,TSPL_RECEIPT_HEADER.DateAndTime,TSPL_RECEIPT_HEADER.AC_Payee,TSPL_RECEIPT_HEADER.cheque_in_favour_of,TSPL_RECEIPT_HEADER.ReceiptAgainstSO_DO,TSPL_RECEIPT_HEADER.SO_Location_Code,TSPL_RECEIPT_HEADER.Booking_Code,TSPL_RECEIPT_HEADER.Against_CSA_Transfer_Code,TSPL_RECEIPT_HEADER.SecurityDepositType,TSPL_RECEIPT_HEADER. memorandum_amt,TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Date, TSPL_RECEIPT_HEADER.Receipt_Post_Date,  TSPL_RECEIPT_HEADER.Entry_Desc, TSPL_RECEIPT_HEADER.Bank_Code,TSPL_RECEIPT_HEADER. Receipt_Type, TSPL_RECEIPT_HEADER.Cust_Code," &
        " TSPL_RECEIPT_HEADER.Customer_Name, TSPL_RECEIPT_HEADER.Reference, TSPL_RECEIPT_HEADER.Narration   , TSPL_RECEIPT_HEADER.Payment_Code, TSPL_RECEIPT_HEADER.Cheque_No, TSPL_RECEIPT_HEADER.Cheque_Date, TSPL_RECEIPT_HEADER.Cheque_From, TSPL_RECEIPT_HEADER.From_Branch, TSPL_RECEIPT_HEADER.Receipt_Amount," &
        " TSPL_RECEIPT_HEADER.Cust_Account, TSPL_RECEIPT_HEADER.Apply_By, TSPL_RECEIPT_HEADER.Apply_To, TSPL_RECEIPT_HEADER.Posted,TSPL_RECEIPT_HEADER.Foreign_Bank_Charges_Amt ,TSPL_RECEIPT_HEADER.Bank_Charges_Amt," &
        " TSPL_RECEIPT_HEADER.Level1_User_code, TSPL_RECEIPT_HEADER.Level2_User_code, TSPL_RECEIPT_HEADER.Level3_User_code, TSPL_RECEIPT_HEADER.Level4_User_code, TSPL_RECEIPT_HEADER.Level5_User_code," &
        " TSPL_RECEIPT_HEADER.Balance_Amt, TSPL_RECEIPT_HEADER.Document_No , TSPL_RECEIPT_HEADER.UnApply_Amt, TSPL_RECEIPT_HEADER.Payer, TSPL_RECEIPT_HEADER.Dr_Account, TSPL_RECEIPT_HEADER.Cr_Account, TSPL_RECEIPT_HEADER.UnApplied_Balance, TSPL_RECEIPT_HEADER.UnApplied_No," &
        " TSPL_RECEIPT_HEADER.FIFO_Balance, TSPL_RECEIPT_HEADER.QuickEntryNo, TSPL_RECEIPT_HEADER.SecurityDeposit, TSPL_RECEIPT_HEADER.IsRecoCleared, TSPL_RECEIPT_HEADER.IsChkReverse, TSPL_RECEIPT_HEADER.IsSalesmanType, TSPL_RECEIPT_HEADER.Salesman_Code, TSPL_RECEIPT_HEADER.Salesman_Name,TSPL_RECEIPT_HEADER.Loadout_No,TSPL_RECEIPT_HEADER.CFormRecd,TSPL_RECEIPT_HEADER.CForm_InvoiceNo,TSPL_RECEIPT_HEADER.SaleOrderNo," &
        " TSPL_RECEIPT_HEADER.CURRENCY_CODE,TSPL_RECEIPT_HEADER.CONVRATE,TSPL_RECEIPT_HEADER.APPLICABLEFROM,TSPL_RECEIPT_HEADER.RECEIVED_AMOUNT_BASE_CURRENCY,TSPL_RECEIPT_HEADER.IsParentCust,TSPL_RECEIPT_HEADER.CHECK_PRINT,TSPL_RECEIPT_HEADER.Check_Code,TSPL_RECEIPT_HEADER.AUTO_GEN_BT_ENTRY,TSPL_RECEIPT_HEADER.TO_BANK_CODE,TSPL_RECEIPT_HEADER.Transfer_No, TSPL_RECEIPT_HEADER.Applied_Receipt,TSPL_RECEIPT_HEADER.Against_CSA_Transfer_Code,ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'') As Location_GL_Code,TSPL_RECEIPT_HEADER.is_Opening,TSPL_RECEIPT_HEADER.Distr_Code, " &
        " TSPL_RECEIPT_HEADER.Delivery_Code_PS,TSPL_RECEIPT_HEADER.Tax_Group,TSPL_RECEIPT_HEADER.Tax_Amount_Advance,TSPL_RECEIPT_HEADER.Delivery_order_Amount,TSPL_RECEIPT_HEADER.DO_Total_Add_Amount, " &
        " TSPL_RECEIPT_HEADER.TAX1,TSPL_RECEIPT_HEADER.TAX1_Rate,TSPL_RECEIPT_HEADER.TAX1_Amt,TSPL_RECEIPT_HEADER.TAX1_Base_Amt,TSPL_RECEIPT_HEADER.TAX2,TSPL_RECEIPT_HEADER.TAX2_Rate,TSPL_RECEIPT_HEADER.TAX2_Amt,TSPL_RECEIPT_HEADER.TAX2_Base_Amt," &
        " TSPL_RECEIPT_HEADER.TAX3,TSPL_RECEIPT_HEADER.TAX3_Rate,TSPL_RECEIPT_HEADER.TAX3_Amt,TSPL_RECEIPT_HEADER.TAX3_Base_Amt,TSPL_RECEIPT_HEADER.TAX4,TSPL_RECEIPT_HEADER.TAX4_Rate," &
        " TSPL_RECEIPT_HEADER.TAX4_Amt,TSPL_RECEIPT_HEADER.TAX4_Base_Amt,TSPL_RECEIPT_HEADER.TAX5,TSPL_RECEIPT_HEADER.TAX5_Rate,TSPL_RECEIPT_HEADER.TAX5_Amt,TSPL_RECEIPT_HEADER.TAX5_Base_Amt," &
        " TSPL_RECEIPT_HEADER.TAX6,TSPL_RECEIPT_HEADER.TAX6_Rate,TSPL_RECEIPT_HEADER.TAX6_Amt,TSPL_RECEIPT_HEADER.TAX6_Base_Amt,TSPL_RECEIPT_HEADER.tax7, TSPL_RECEIPT_HEADER.TAX7_Rate, " &
        " TSPL_RECEIPT_HEADER.TAX7_Amt, TSPL_RECEIPT_HEADER.TAX7_Base_Amt, TSPL_RECEIPT_HEADER.TAX8, TSPL_RECEIPT_HEADER.TAX8_Rate, TSPL_RECEIPT_HEADER.TAX8_Amt," &
        " TSPL_RECEIPT_HEADER.TAX8_Base_Amt, TSPL_RECEIPT_HEADER.TAX9, TSPL_RECEIPT_HEADER.TAX9_Rate, TSPL_RECEIPT_HEADER.TAX9_Amt, TSPL_RECEIPT_HEADER.TAX9_Base_Amt, " &
        " TSPL_RECEIPT_HEADER.TAX10, TSPL_RECEIPT_HEADER.TAX10_Rate, TSPL_RECEIPT_HEADER.TAX10_Amt, TSPL_RECEIPT_HEADER.TAX10_Base_Amt,TSPL_RECEIPT_HEADER.Set_Off_Date,TSPL_RECEIPT_HEADER.SetOffSkipJE,TSPL_RECEIPT_HEADER.Rec_Route_Code,TSPL_RECEIPT_HEADER.Rec_Zone_Code " &
        " FROM  TSPL_RECEIPT_HEADER "


        ''===========================================================
        isCustomerFinderLocationWiseARReceipt = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CustomerMasterFinderOnLocationwiseARReceipt, clsFixedParameterCode.CustomerMasterFinderOnLocationwiseARReceipt, trans)) = 1, True, False)
        If isCustomerFinderLocationWiseARReceipt Then
            qry += " left outer join TSPL_CUSTOMER_LOCATION_MAPPING on TSPL_CUSTOMER_LOCATION_MAPPING.customer_code=TSPL_RECEIPT_HEADER.cust_code "
            strJointCond = " left outer join TSPL_CUSTOMER_LOCATION_MAPPING on TSPL_CUSTOMER_LOCATION_MAPPING.customer_code=TSPL_RECEIPT_HEADER.cust_code "
        Else
            strJointCond = ""
        End If
        ''===========================================================

        qry += " where  2=2 "

        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim whrClas As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = Xtra.CustomerPermission()
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                '    strwherecls = objCommonVar.strCurrUserCustomers
                'Else
                '    strwherecls = Xtra.CustomerPermission()
                whrClas = " and TSPL_RECEIPT_HEADER.Location_GL_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
            If clsCommon.myLen(strwherecls) > 0 Then
                whrClas = " and TSPL_RECEIPT_HEADER.Cust_Code in (" + strwherecls + ") "
            End If
        End If

        If isCustomerFinderLocationWiseARReceipt Then
            whrClas += " and TSPL_CUSTOMER_LOCATION_MAPPING.location_code in (select Default_Location from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "') "
        End If

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_RECEIPT_HEADER.Receipt_No=(select MIN(Receipt_No) from TSPL_RECEIPT_HEADER " + strJointCond + " Where 1=1 " + whrClas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_RECEIPT_HEADER.Receipt_No=(select Max(Receipt_No) from TSPL_RECEIPT_HEADER " + strJointCond + " Where 1=1 " + whrClas + " )"
            Case NavigatorType.Next
                qry += " and TSPL_RECEIPT_HEADER.Receipt_No=(select Min(Receipt_No) from TSPL_RECEIPT_HEADER " + strJointCond + " where Receipt_No > '" + strCode + "' " + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_RECEIPT_HEADER.Receipt_No=(select Max(Receipt_No) from TSPL_RECEIPT_HEADER " + strJointCond + " where Receipt_No < '" + strCode + "'  " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_RECEIPT_HEADER.Receipt_No='" + strCode + "'"
        End Select
        '-------------richa code ends here----------------------------------------
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRcptEntryHeader()
            obj.Booking_Code = clsCommon.myCstr(dt.Rows(0)("Booking_Code"))
            obj.CSATransfer_No = clsCommon.myCstr(dt.Rows(0)("Against_CSA_Transfer_Code"))
            obj.SecurityDepositType = clsCommon.myCstr(dt.Rows(0)("SecurityDepositType"))
            obj.memorndmamt = clsCommon.myCstr(dt.Rows(0)("memorandum_amt"))
            obj.Receipt_No = clsCommon.myCstr(dt.Rows(0)("Receipt_No"))
            obj.Rec_Route_Code = clsCommon.myCstr(dt.Rows(0)("Rec_Route_Code"))
            obj.Rec_Zone_Code = clsCommon.myCstr(dt.Rows(0)("Rec_Zone_Code"))
            obj.Receipt_Date = clsCommon.myCDate(dt.Rows(0)("Receipt_Date"))
            obj.Receipt_Post_Date = clsCommon.myCDate(dt.Rows(0)("Receipt_Post_Date"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Receipt_Type = clsCommon.myCstr(dt.Rows(0)("Receipt_Type"))
            obj.Entry_Desc = clsCommon.myCstr(dt.Rows(0)("Entry_Desc"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Distr_Code = clsCommon.myCstr(dt.Rows(0)("Distr_Code"))
            obj.Against_RCDF_Loadin = clsCommon.myCstr(dt.Rows(0)("Against_RCDF_Loadin"))
            obj.Customer_Name = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER Where Cust_Code='" + obj.Cust_Code + "'", trans)
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Narration = clsCommon.myCstr(dt.Rows(0)("Narration"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            If clsCommon.myCstr(obj.Cheque_No) <> "" Then
                obj.Cheque_Date = clsCommon.myCstr(dt.Rows(0)("Cheque_Date"))
            End If
            If IsDBNull(dt.Rows(0)("DateAndTime")) = True Then
                obj.DateAndTime = Nothing
            Else
                obj.DateAndTime = clsCommon.myCstr(dt.Rows(0)("DateAndTime"))
            End If
            obj.TapalNo = clsCommon.myCstr(dt.Rows(0)("TapalNo"))
            obj.Cheque_From = clsCommon.myCstr(dt.Rows(0)("Cheque_From"))
            obj.From_Branch = clsCommon.myCstr(dt.Rows(0)("From_Branch"))
            obj.Loadout_No = clsCommon.myCstr(dt.Rows(0)("Loadout_No"))
            obj.Location_GL_Code = clsCommon.myCstr(dt.Rows(0)("Location_GL_Code"))

            obj.Delivery_order_Amount = clsCommon.myCdbl(dt.Rows(0)("Delivery_order_Amount"))
            obj.DO_Total_Add_Amount = clsCommon.myCdbl(dt.Rows(0)("DO_Total_Add_Amount"))
            obj.Tax_Amount_Advance = clsCommon.myCdbl(dt.Rows(0)("Tax_Amount_Advance"))
            obj.isCardSale = clsCommon.myCdbl(dt.Rows(0)("isCardSale"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Delivery_Code_PS = clsCommon.myCstr(dt.Rows(0)("Delivery_Code_PS"))
            obj.ReceiptAgainstSO_DO = clsCommon.myCstr(dt.Rows(0)("ReceiptAgainstSO_DO"))

            obj.Receipt_Amount = clsCommon.myCdbl(dt.Rows(0)("Receipt_Amount"))
            obj.Apply_By = clsCommon.myCstr(dt.Rows(0)("Apply_By"))
            obj.Apply_To = clsCommon.myCstr(dt.Rows(0)("Apply_To"))
            obj.Cust_Account = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
            obj.Level1_User_code = clsCommon.myCstr(dt.Rows(0)("Level1_User_code"))
            obj.Level2_User_code = clsCommon.myCstr(dt.Rows(0)("Level2_User_code"))
            obj.Level3_User_code = clsCommon.myCstr(dt.Rows(0)("Level3_User_code"))
            obj.Level4_User_code = clsCommon.myCstr(dt.Rows(0)("Level4_User_code"))
            obj.Level5_User_code = clsCommon.myCstr(dt.Rows(0)("Level5_User_code"))
            obj.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("Balance_Amt"))
            obj.Document_No = clsCommon.myCdbl(dt.Rows(0)("Document_No"))
            obj.UnApply_Amt = clsCommon.myCdbl(dt.Rows(0)("UnApply_Amt"))
            obj.UnApplied_Balance = clsCommon.myCdbl(dt.Rows(0)("UnApplied_Balance"))
            obj.UnApplied_No = clsCommon.myCstr(dt.Rows(0)("UnApplied_No"))
            obj.Dr_Account = clsCommon.myCstr(dt.Rows(0)("Dr_Account"))
            obj.Cr_Account = clsCommon.myCstr(dt.Rows(0)("Cr_Account"))
            obj.Payer = clsCommon.myCstr(dt.Rows(0)("Payer"))
            obj.FIFO_Balance = clsCommon.myCstr(dt.Rows(0)("FIFO_Balance"))
            obj.QuickEntryNo = clsCommon.myCstr(dt.Rows(0)("QuickEntryNo"))
            obj.SecurityDeposit = clsCommon.myCstr(dt.Rows(0)("SecurityDeposit"))
            obj.IsRecoCleared = clsCommon.myCstr(dt.Rows(0)("IsRecoCleared"))
            obj.IsChkReverse = clsCommon.myCstr(dt.Rows(0)("IsChkReverse"))
            obj.IsSalesmanType = clsCommon.myCstr(dt.Rows(0)("IsSalesmanType"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Emp_Name from TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + obj.Salesman_Code + "'", trans))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            '-----------------richa 28/08/2014 Against Ticket No .BM00000003667---------
            obj.SaleOrderNo = clsCommon.myCstr(dt.Rows(0)("SaleOrderNo"))
            ''-------------------------------
            ''RICHA AGARWAL
            obj.Foreign_Bank_Charges_Amt = clsCommon.myCdbl(dt.Rows(0)("Foreign_Bank_Charges_Amt"))
            obj.Bank_Charges_Amt = clsCommon.myCdbl(dt.Rows(0)("Bank_Charges_Amt"))
            ''----------------
            obj.CForm_InvoiceNo = clsCommon.myCstr(dt.Rows(0)("CForm_InvoiceNo"))
            obj.CFormRecd = clsCommon.myCstr(dt.Rows(0)("CFormRecd"))

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            obj.RECEIVED_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(dt.Rows(0)("RECEIVED_AMOUNT_BASE_CURRENCY"))
            '' END CURRENCYCONVERSION
            obj.IsParentCust = IIf(dt.Rows(0)("IsParentCust") = 1, True, False)

            obj.CHECK_PRINT = clsCommon.myCdbl(dt.Rows(0)("CHECK_PRINT"))
            obj.CHECK_CODE = clsCommon.myCstr(dt.Rows(0)("CHECK_CODE"))

            obj.AUTO_GEN_BT_ENTRY = clsCommon.myCBool(dt.Rows(0)("AUTO_GEN_BT_ENTRY"))
            obj.TO_BANK_CODE = clsCommon.myCstr(dt.Rows(0)("TO_BANK_CODE"))
            obj.Transfer_No = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
            obj.Applied_Receipt = clsCommon.myCstr(dt.Rows(0)("Applied_Receipt"))
            obj.is_Opening = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Opening")) = 1, True, False)


            ''RICHA 
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))

            obj.tax2 = clsCommon.myCstr(dt.Rows(0)("tax2"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))

            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))

            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))

            obj.tax5 = clsCommon.myCstr(dt.Rows(0)("tax5"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))

            obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
            obj.tax6 = clsCommon.myCstr(dt.Rows(0)("tax6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))

            obj.tax7 = clsCommon.myCstr(dt.Rows(0)("tax7"))
            obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))

            obj.tax8 = clsCommon.myCstr(dt.Rows(0)("tax8"))
            obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))

            obj.tax9 = clsCommon.myCstr(dt.Rows(0)("tax9"))
            obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))

            obj.tax10 = clsCommon.myCstr(dt.Rows(0)("tax10"))
            obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))

            obj.SO_Location_Code = clsCommon.myCstr(dt.Rows(0)("SO_Location_Code"))
            ''------
            obj.AC_Payee = clsCommon.myCdbl(dt.Rows(0)("AC_Payee"))
            obj.cheque_in_favour_of = clsCommon.myCstr(dt.Rows(0)("cheque_in_favour_of"))

            '' setof columns that are used in Receipt triggers to skip them
            If IsDBNull(dt.Rows(0)("Set_Off_Date")) = False Then
                obj.Set_Off_Date = dt.Rows(0)("Set_Off_Date")
            End If
            If IsDBNull(dt.Rows(0)("SetOffSkipJE")) = False Then
                obj.SetOffSkipJE = clsCommon.myCBool(dt.Rows(0)("SetOffSkipJE"))
            Else
                obj.SetOffSkipJE = False
            End If


            qry = "SELECT Receipt_No, Receipt_Line_No, Apply, Receipt_Type, TSPL_RECEIPT_DETAIL.Document_No,  Original_Amt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='F' then (Select RH.Balance_Amt from TSPL_RECEIPT_HEADER RH WHERE RH.Receipt_No=TSPL_RECEIPT_DETAIL.Document_No)  else  TSPL_RECEIPT_DETAIL.Pending_Balance end as Pending_Balance," & _
            " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else TSPL_RECEIPT_DETAIL.Document_No End as SaleInvoice," & _
            " Applied_Amount,  Account_Code,  TSPL_RECEIPT_DETAIL.Description,  TSPL_RECEIPT_DETAIL.Remarks,  Comment,  TSPL_RECEIPT_DETAIL.Shipment_No,  Adjustment_Account," & _
            " Adjustment_Cost, Adjustment_No," & _
            " AutoId, TagType, Posted, TSPL_Customer_Invoice_Head.Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt As Total_Invoice_Amt, 0 as Empty_Value, TSPL_RECEIPT_DETAIL.ConvRateOld,TSPL_RECEIPT_DETAIL.Child_Cust_Code," & _
            "  TSPL_RECEIPT_DETAIL.Cost_Center_Fin_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,TSPL_RECEIPT_DETAIL.Hirerachy_Level_Code,TSPL_HIRERACHY_LEVEL_MASTER.Description as Hirerachy_Level_Name " & _
            " FROM TSPL_RECEIPT_DETAIL" & _
             " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_RECEIPT_DETAIL.Cost_Center_Fin_Code " + _
             " LEFT OUTER JOIN TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE=TSPL_RECEIPT_DETAIL.Hirerachy_Level_Code " + _
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No" & _
            " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No" & _
            " WHERE TSPL_RECEIPT_DETAIL.Receipt_No = '" + obj.Receipt_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrTr = New List(Of clsReceiptDettail)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsReceiptDettail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsReceiptDettail()
                    objTr.Receipt_Line_No = clsCommon.myCdbl(dr("Receipt_Line_No"))
                    objTr.Receipt_No = clsCommon.myCstr(dr("Receipt_No"))
                    objTr.Apply = clsCommon.myCstr(dr("Apply"))
                    objTr.Receipt_Type = clsCommon.myCstr(dr("Receipt_Type"))
                    objTr.SaleInvoice = clsCommon.myCstr(dr("SaleInvoice"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    If clsCommon.myLen(dr("Document_Date")) > 0 And clsCommon.myLen(objTr.Document_No) > 0 Then
                        objTr.Document_Date = clsCommon.myCstr(dr("Document_Date"))
                    End If
                    objTr.Original_Amt = clsCommon.myCdbl(dr("Original_Amt"))
                    objTr.Pending_Balance = clsCommon.myCdbl(dr("Pending_Balance"))
                    objTr.Applied_Amount = clsCommon.myCdbl(dr("Applied_Amount"))
                    objTr.Account_Code = clsCommon.myCstr(dr("Account_Code"))
                    objTr.Description = clsCommon.myCstr(dr("Description"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Comment = clsCommon.myCstr(dr("Comment"))
                    objTr.Shipment_No = clsCommon.myCstr(dr("Shipment_No"))
                    objTr.Adjustment_Account = clsCommon.myCstr(dr("Adjustment_Account"))
                    objTr.Adjustment_Cost = clsCommon.myCstr(dr("Adjustment_Cost"))
                    objTr.Adjustment_No = clsCommon.myCstr(dr("Adjustment_No"))
                    objTr.FilledTotal = clsCommon.myCdbl(dr("Total_Invoice_Amt"))
                    objTr.EmptyTotal = clsCommon.myCdbl(dr("Empty_Value"))
                    objTr.TagType = clsCommon.myCstr(dr("TagType"))
                    objTr.Posted = clsCommon.myCstr(dr("Posted"))
                    objTr.ConvRateOld = clsCommon.myCdbl(dr("ConvRateOld"))
                    objTr.Child_Cust_Code = clsCommon.myCstr(dr("Child_Cust_Code"))
                    objTr.Cost_Center_Fin_Code = clsCommon.myCstr(dr("Cost_Center_Fin_Code"))
                    objTr.Cost_Center_Fin_Name = clsCommon.myCstr(dr("Cost_Center_Fin_Name"))
                    objTr.Hirerachy_Level_Code = clsCommon.myCstr(dr("Hirerachy_Level_Code"))
                    objTr.Hirerachy_Level_Name = clsCommon.myCstr(dr("Hirerachy_Level_Name"))
                    obj.ArrTr.Add(objTr)
                Next


            End If

            ''richa
            qry = "SELECT * from TSPL_RECEIPT_DETAIL_GST WHERE TSPL_RECEIPT_DETAIL_GST.Receipt_No = '" + obj.Receipt_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrTrGST = New List(Of clsReceiptDetailGST)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsReceiptDetailGST
                For Each dr As DataRow In dt.Rows
                    objTr = New clsReceiptDetailGST()
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Receipt_No = clsCommon.myCstr(dr("Receipt_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))

                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))

                    objTr.tax2 = clsCommon.myCstr(dr("tax2"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))

                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))

                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))

                    objTr.tax5 = clsCommon.myCstr(dr("tax5"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))

                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.tax6 = clsCommon.myCstr(dr("tax6"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))

                    objTr.tax7 = clsCommon.myCstr(dr("tax7"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))

                    objTr.tax8 = clsCommon.myCstr(dr("tax8"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))

                    objTr.tax9 = clsCommon.myCstr(dr("tax9"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))

                    objTr.tax10 = clsCommon.myCstr(dr("tax10"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Abatement_Per = clsCommon.myCdbl(dr("Abatement_Per"))
                    objTr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                    objTr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                    objTr.FOC_Item = clsCommon.myCstr(dr("FOC_Item"))
                    objTr.Item_Tax = clsCommon.myCdbl(dr("Item_Tax"))
                    objTr.Total_MRP_Amt = clsCommon.myCdbl(dr("Total_MRP_Amt"))
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                    objTr.Total_Disc_Amt = clsCommon.myCdbl(dr("Total_Disc_Amt"))
                    objTr.ActualRate = clsCommon.myCdbl(dr("ActualRate"))
                    objTr.TotalItem_Weight = clsCommon.myCdbl(dr("TotalItem_Weight"))
                    objTr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                    objTr.Landing_Cost = clsCommon.myCdbl(dr("Landing_Cost"))
                    objTr.TAX1_Amt_Receipt = clsCommon.myCdbl(dr("TAX1_Amt_Receipt"))
                    objTr.TAX2_Amt_Receipt = clsCommon.myCdbl(dr("TAX2_Amt_Receipt"))
                    objTr.TAX3_Amt_Receipt = clsCommon.myCdbl(dr("TAX3_Amt_Receipt"))
                    objTr.TAX4_Amt_Receipt = clsCommon.myCdbl(dr("TAX4_Amt_Receipt"))
                    objTr.TAX5_Amt_Receipt = clsCommon.myCdbl(dr("TAX5_Amt_Receipt"))
                    objTr.TAX6_Amt_Receipt = clsCommon.myCdbl(dr("TAX6_Amt_Receipt"))
                    objTr.TAX7_Amt_Receipt = clsCommon.myCdbl(dr("TAX7_Amt_Receipt"))
                    objTr.TAX8_Amt_Receipt = clsCommon.myCdbl(dr("TAX8_Amt_Receipt"))
                    objTr.TAX9_Amt_Receipt = clsCommon.myCdbl(dr("TAX9_Amt_Receipt"))
                    objTr.TAX10_Amt_Receipt = clsCommon.myCdbl(dr("TAX10_Amt_Receipt"))
                    objTr.ReceiptAdvance = clsCommon.myCdbl(dr("ReceiptAdvance"))
                    objTr.ReceiptTotalTax = clsCommon.myCdbl(dr("ReceiptTotalTax"))
                    objTr.ReceiptTotalAdvanceAmt = clsCommon.myCdbl(dr("ReceiptTotalAdvanceAmt"))
                    obj.ArrTrGST.Add(objTr)
                Next


            End If

            ''----------
            ''richa agarwal ERO/18/07/19-000956
            qry = "SELECT Receipt_No, Receipt_Line_No, Apply, Receipt_Type, TSPL_RECEIPT_DETAIL_Refund.Document_No,  Original_Amt,  case when TSPL_RECEIPT_DETAIL_Refund.Receipt_Type='F' then (Select RH.Balance_Amt from TSPL_RECEIPT_HEADER RH WHERE RH.Receipt_No=TSPL_RECEIPT_DETAIL_Refund.Document_No)  else  TSPL_RECEIPT_DETAIL_Refund.Pending_Balance end as Pending_Balance," & _
           " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else TSPL_RECEIPT_DETAIL_Refund.Document_No End as SaleInvoice," & _
           " Applied_Amount,  Account_Code,  TSPL_RECEIPT_DETAIL_Refund.Description,  TSPL_RECEIPT_DETAIL_Refund.Remarks,  Comment,  TSPL_RECEIPT_DETAIL_Refund.Shipment_No,  Adjustment_Account," & _
           " Adjustment_Cost, Adjustment_No," & _
           " AutoId, TagType, Posted, TSPL_Customer_Invoice_Head.Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt As Total_Invoice_Amt, 0 as Empty_Value, TSPL_RECEIPT_DETAIL_Refund.ConvRateOld,TSPL_RECEIPT_DETAIL_Refund.Child_Cust_Code," & _
           "  TSPL_RECEIPT_DETAIL_Refund.Cost_Center_Fin_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,TSPL_RECEIPT_DETAIL_Refund.Hirerachy_Level_Code,TSPL_HIRERACHY_LEVEL_MASTER.Description as Hirerachy_Level_Name " & _
           " FROM TSPL_RECEIPT_DETAIL_Refund" & _
            " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_RECEIPT_DETAIL_Refund.Cost_Center_Fin_Code " + _
            " LEFT OUTER JOIN TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE=TSPL_RECEIPT_DETAIL_Refund.Hirerachy_Level_Code " + _
           " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL_Refund.Document_No" & _
           " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No" & _
           " WHERE TSPL_RECEIPT_DETAIL_Refund.Receipt_No = '" + obj.Receipt_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrTrRefund = New List(Of clsReceiptDetail_Refund)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsReceiptDetail_Refund
                For Each dr As DataRow In dt.Rows
                    objTr = New clsReceiptDetail_Refund()
                    objTr.Receipt_Line_No = clsCommon.myCdbl(dr("Receipt_Line_No"))
                    objTr.Receipt_No = clsCommon.myCstr(dr("Receipt_No"))
                    objTr.Apply = clsCommon.myCstr(dr("Apply"))
                    objTr.Receipt_Type = clsCommon.myCstr(dr("Receipt_Type"))
                    objTr.SaleInvoice = clsCommon.myCstr(dr("SaleInvoice"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    If clsCommon.myLen(dr("Document_Date")) > 0 And clsCommon.myLen(objTr.Document_No) > 0 Then
                        objTr.Document_Date = clsCommon.myCstr(dr("Document_Date"))
                    End If
                    objTr.Original_Amt = clsCommon.myCdbl(dr("Original_Amt"))
                    objTr.Pending_Balance = clsCommon.myCdbl(dr("Pending_Balance"))
                    objTr.Applied_Amount = clsCommon.myCdbl(dr("Applied_Amount"))
                    objTr.Account_Code = clsCommon.myCstr(dr("Account_Code"))
                    objTr.Description = clsCommon.myCstr(dr("Description"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Comment = clsCommon.myCstr(dr("Comment"))
                    objTr.Shipment_No = clsCommon.myCstr(dr("Shipment_No"))
                    objTr.Adjustment_Account = clsCommon.myCstr(dr("Adjustment_Account"))
                    objTr.Adjustment_Cost = clsCommon.myCstr(dr("Adjustment_Cost"))
                    objTr.Adjustment_No = clsCommon.myCstr(dr("Adjustment_No"))
                    objTr.FilledTotal = clsCommon.myCdbl(dr("Total_Invoice_Amt"))
                    objTr.EmptyTotal = clsCommon.myCdbl(dr("Empty_Value"))
                    objTr.TagType = clsCommon.myCstr(dr("TagType"))
                    objTr.Posted = clsCommon.myCstr(dr("Posted"))
                    objTr.ConvRateOld = clsCommon.myCdbl(dr("ConvRateOld"))
                    objTr.Child_Cust_Code = clsCommon.myCstr(dr("Child_Cust_Code"))
                    objTr.Cost_Center_Fin_Code = clsCommon.myCstr(dr("Cost_Center_Fin_Code"))
                    objTr.Cost_Center_Fin_Name = clsCommon.myCstr(dr("Cost_Center_Fin_Name"))
                    objTr.Hirerachy_Level_Code = clsCommon.myCstr(dr("Hirerachy_Level_Code"))
                    objTr.Hirerachy_Level_Name = clsCommon.myCstr(dr("Hirerachy_Level_Name"))
                    obj.ArrTrRefund.Add(objTr)
                Next


            End If

        End If
        Return obj
    End Function

    Public Shared Function GetBalance(ByVal strAppliedPayment As String, ByVal strPaymentNo As String, ByVal trans As SqlTransaction) As Double
        Try
            '    Dim qry As String = "Select [Bal Amt] from (" & _
            '" Select Receipt_No as [Code], Entry_Desc as [Description], Receipt_Date as [Payment Date], Case When Receipt_Type='P' Then 'Advance' When Receipt_Type='O' Then 'On Account' Else 'UnApplied' End As [Payment Type], Receipt_Amount as [Payment Amt], Balance_Amt-ISNULL((Select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER RH WHERE RH.Posted<>'Y' AND RH.Receipt_Type='A' AND RH.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code AND RH.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No AND RH.Receipt_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_RECEIPT_HEADER WHERE Posted='Y' AND Receipt_Type IN ('P','O','U') AND Receipt_No <> '" + strPaymentNo + "'" & _
            '" ) Final WHERE Code='" + strAppliedPayment + "'"

            Dim qry As String = "Select [Bal Amt] from (" & _
            " Select Receipt_No as [Code], Entry_Desc as [Description], Receipt_Date as [Payment Date], Case When Receipt_Type='P' Then 'Advance' When Receipt_Type='O' Then 'On Account' Else 'UnApplied' End As [Payment Type], Receipt_Amount as [Payment Amt], Balance_Amt-ISNULL((Select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER RH WHERE RH.Posted<>'Y' AND RH.Receipt_Type='A' AND RH.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code AND RH.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No AND RH.Receipt_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_RECEIPT_HEADER WHERE Posted='Y' AND Receipt_Type IN ('P','O','U') AND Receipt_No <> '" + strPaymentNo + "'" & _
            "union all " & Environment.NewLine & _
            " Select * from  (select [Invoice No] as [Code],Description ,[Invoice Date] as [Receipt Date],Type as [Receipt Type],[Doc Total] as [Receipt Amt], [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Bal Amt] from ( select " & Environment.NewLine & _
            " TSPL_Customer_Invoice_Head.Description , " & Environment.NewLine & _
            " 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type],  Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice, Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," & Environment.NewLine & _
            " (TSPL_Customer_Invoice_Head.Document_Total -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " & Environment.NewLine & _
            " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>''  AND Receipt_No <> '" + strPaymentNo + "' ),0)  " & Environment.NewLine & _
            " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " & Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine & _
            " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " & Environment.NewLine & _
            " -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " & Environment.NewLine & _
            " , '0.00' as [Apply_Amt],  Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY'  " & Environment.NewLine & _
            " ) as xxx  left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code ) XXX WHERE [Bal Amt]>0 " & Environment.NewLine & _
            "and [Receipt Type]  ='Credit Note' " & Environment.NewLine & _
            " ) Final WHERE Code='" + strAppliedPayment + "'"
            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UpdateBalance(ByVal strAppliedPayment As String, ByVal dblAmtToBeDeduct As Double, ByVal Set_Off_Date As String, ByVal SetOffSkipJE As Integer, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            If clsCommon.myLen(Set_Off_Date) <= 0 Then
                qry = "Update TSPL_RECEIPT_HEADER SET Balance_Amt=Balance_Amt-" + clsCommon.myCstr(dblAmtToBeDeduct) + " WHERE Receipt_No='" + strAppliedPayment + "'"
            Else
                qry = "Update TSPL_RECEIPT_HEADER SET Balance_Amt=Balance_Amt-" + clsCommon.myCstr(dblAmtToBeDeduct) + ",Set_Off_Date='" & Set_Off_Date & "',SetOffSkipJE='" & SetOffSkipJE & "' WHERE Receipt_No='" + strAppliedPayment + "'"
            End If
            clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function fundelete(ByVal strReceiptNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            fundelete(strReceiptNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function fundelete(ByVal strReceiptNo As String, ByVal trans As SqlTransaction) As Boolean

        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsRcptEntryHeader
            If clsCommon.myLen(strReceiptNo) > 0 Then
                obj = clsRcptEntryHeader.GetData(strReceiptNo, NavigatorType.Current, trans)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Receipt_No) > 0) Then
                    If clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                        Throw New Exception("This documentis already posted and can not be deleted.")
                    End If
                Else
                    Throw New Exception("Document not found to delete.")
                End If
                '--------------------Checks Whether the Transaction is Locked or not----------------------------
                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans)
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "Receipt Entry", LocSegmentCode, obj.Receipt_Date, trans)
                '-----------------------------------------------------------------------------------------------


                '' reverse has not done from receipt entry against Booking 
                Dim strBookingType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(TSPL_BOOKING_MATSER.Booking_Type ,'') from TSPL_BOOKING_PAYMENT_MODE_DETAIL left outer join  TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No= TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No where isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')='" & obj.Receipt_No & "'", trans))
                If clsCommon.myLen(strBookingType) > 0 Then
                    If clsCommon.CompairString(strBookingType, "CD") = CompairStringResult.Equal Then
                        Throw New Exception("Receipt can't be reversed because it has been created against Card Sale Booking.")
                    End If
                End If


                ''richa agarwal 5 Nov,2019 to save receipt no into booking master table
                If clsCommon.myLen(obj.Booking_Code) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_BOOKING_MATSER set Against_Receipt_No =Null where Document_No='" & obj.Booking_Code & "' ", trans)
                End If

            Else
                Throw New Exception("Document not found to delete.")
            End If
            Dim Qry As String = String.Empty
           

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where (Source_Code like 'AR%' or Source_Code='GL-JE') AND  Source_Doc_No='" + strReceiptNo + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            '' TO DELETE DATA FROM JOURNAL MASTER OPENING TABLE ''richa TEC/28/11/18-000376 28 Nov,2018
            Dim VoucherNoOP As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER_OP where (Source_Code like 'AR%' or Source_Code='GL-JE') AND  Source_Doc_No='" + strReceiptNo + "'", trans)
            If clsCommon.myLen(VoucherNoOP) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS_OP where Voucher_No ='" + VoucherNoOP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER_OP where Voucher_No ='" + VoucherNoOP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            'Ticket No-TEC/06/09/19-001003,Save Deleted data ,sanjay
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strReceiptNo, "TSPL_RECEIPT_HEADER", "Receipt_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Receipt_No, "TSPL_bank_book", "SOURCEDOC_NO", trans)
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Receipt_No, "TSPL_RECEIPT_HEADER", "Receipt_No", "TSPL_RECEIPT_DETAIL", "Receipt_No", "TSPL_RECEIPT_DETAIL_GST", "Receipt_No", "TSPL_RECEIPT_DETAIL_Refund", "Receipt_No", "", "", "", "", "", "", trans)

            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_RECEIPT_Detail Where Receipt_No='" + strReceiptNo + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_RECEIPT_Detail_GST Where Receipt_No='" + strReceiptNo + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_RECEIPT_Detail_Refund Where Receipt_No='" + strReceiptNo + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_RECEIPT_HEADER Where Receipt_No='" + strReceiptNo + "'", trans)
            clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Receipt_No, trans)
            If clsCommon.myLen(obj.Transfer_No) > 0 Then
                connectSql.RunSpTransaction(trans, "sp_tspl_banktransfer_delete", New SqlParameter("@Transfer_No", obj.Transfer_No))
            End If
            ''richa to set outanstanding balance of bank reco
            clsBankReco.SetOutstandingEntry(strReceiptNo, obj.Receipt_Date, "Receipt", trans, False)
            'trans.Commit()
            Return True

        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function funRcptPost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If funRcptPost(strDocNo, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ''created by richa against ticket no ERO/22/10/19-001074
    Public Shared Function UpdateBankBook(ByVal obj As clsRcptEntryHeader, ByVal trans As SqlTransaction) As Boolean
        Dim TransType_Str As String = ""
        Dim ArrBankBook As List(Of ClsBankBook) = New List(Of ClsBankBook)



        Dim strRgpNo As String = Nothing
        Dim intCounter As Integer = 0
        Dim isSaved As Boolean = True
        If clsCommon.CompairString(obj.Receipt_Type, "A") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Receipt_Type, "K") <> CompairStringResult.Equal Then
            ClsBankBook.fundelete(obj.Receipt_No, "Receipt", trans)
            If clsCommon.CompairString(obj.Receipt_Type, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Receipt_Type, "S") = CompairStringResult.Equal Then
                For Each objTr As clsReceiptDettail In obj.ArrTr
                    intCounter = intCounter + 1


                    Dim objBankBook As New ClsBankBook()
                    objBankBook.SOURCEDOC_NO = obj.Receipt_No
                    objBankBook.SOURCEDOC_DATE = obj.Receipt_Date
                    objBankBook.SOURCE_CODE = obj.Cust_Code
                    objBankBook.SOURCE_NAME = obj.Customer_Name
                    objBankBook.BANK_CODE = obj.Bank_Code
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_BANK_MASTER.DESCRIPTION ,RIGHT(TSPL_BANK_MASTER.BANKACC, 3) as Loc_Code, TSPL_GL_SEGMENT_CODE.Description as Loc_Name ,TSPL_BANK_MASTER.BANKACC ,TSPL_GL_ACCOUNTS.Description as BANKACC_Desc ,Narration FROM TSPL_RECEIPT_HEADER INNER JOIN  TSPL_BANK_MASTER ON TSPL_RECEIPT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE INNER JOIN TSPL_GL_SEGMENT_CODE ON RIGHT(TSPL_BANK_MASTER.BANKACC, 3) = TSPL_GL_SEGMENT_CODE.Segment_code inner join TSPL_GL_ACCOUNTS on TSPL_BANK_MASTER.BANKACC=TSPL_GL_ACCOUNTS.Account_Code  where Receipt_No='" & obj.Receipt_No & "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objBankBook.BANK_NAME = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
                        objBankBook.LOC_CODE = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
                        objBankBook.LOC_NAME = clsCommon.myCstr(dt.Rows(0)("Loc_Name"))
                        objBankBook.BANKGL_Account_Code = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                        objBankBook.BANKGL_Account_Name = clsCommon.myCstr(dt.Rows(0)("BANKACC_Desc"))
                        objBankBook.NARR_MASTER = clsCommon.myCstr(dt.Rows(0)("Narration"))

                    End If
                    objBankBook.CHEQUE_NO = obj.Cheque_No
                    objBankBook.CHEQUE_DATE = obj.Cheque_Date
                    objBankBook.GL_Account_Code = objTr.Account_Code
                    objBankBook.GL_Account_Name = objTr.Description
                    objBankBook.NARR_DETAIL = objTr.Remarks
                    objBankBook.Credit_Amount = 0
                    objBankBook.Debit_Amount = objTr.Applied_Amount
                    objBankBook.DocType = "Receipt"
                    objBankBook.TransactionType = obj.Receipt_Type
                    objBankBook.line_No = intCounter
                    objBankBook.Currency = obj.CURRENCY_CODE
                    objBankBook.Base_Currency = obj.BASE_CURRENCY_CODE
                    objBankBook.Conversion_Rate = obj.ConvRate
                    objBankBook.Remarks = obj.Entry_Desc
                    objBankBook.Bank_charges = obj.Bank_Charges_Amt

                    ArrBankBook.Add(objBankBook)
                Next
            Else
                Dim objBankBook As New ClsBankBook()
                objBankBook.SOURCEDOC_NO = obj.Receipt_No
                objBankBook.SOURCEDOC_DATE = obj.Receipt_Date
                objBankBook.SOURCE_CODE = obj.Cust_Code
                objBankBook.SOURCE_NAME = obj.Customer_Name
                objBankBook.BANK_CODE = obj.Bank_Code
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_BANK_MASTER.DESCRIPTION ,RIGHT(TSPL_BANK_MASTER.BANKACC, 3) as Loc_Code, TSPL_GL_SEGMENT_CODE.Description as Loc_Name ,TSPL_BANK_MASTER.BANKACC as BANKACC ,TSPL_GL_ACCOUNTS.Description as BANKACC_Desc,Receivable_Control_acct as GL_Account_Code,tspl_GL_Accounts1.Description as GL_Account_Name,Narration FROM         TSPL_RECEIPT_HEADER INNER JOIN TSPL_BANK_MASTER ON TSPL_RECEIPT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE INNER JOIN TSPL_GL_SEGMENT_CODE ON RIGHT(TSPL_BANK_MASTER.BANKACC, 3) = TSPL_GL_SEGMENT_CODE.Segment_code inner join TSPL_GL_ACCOUNTS on TSPL_BANK_MASTER.BANKACC=TSPL_GL_ACCOUNTS.Account_Code inner join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_RECEIPT_HEADER.Cust_Account=TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  inner join TSPL_GL_ACCOUNTS as tspl_GL_Accounts1 on TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct=tspl_GL_Accounts1.Account_Code where Receipt_No='" & obj.Receipt_No & "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objBankBook.BANK_NAME = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
                    objBankBook.LOC_CODE = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
                    objBankBook.LOC_NAME = clsCommon.myCstr(dt.Rows(0)("Loc_Name"))
                    objBankBook.BANKGL_Account_Code = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                    objBankBook.BANKGL_Account_Name = clsCommon.myCstr(dt.Rows(0)("BANKACC_Desc"))
                    objBankBook.NARR_MASTER = clsCommon.myCstr(dt.Rows(0)("Narration"))
                    objBankBook.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("GL_Account_Code"))
                    objBankBook.GL_Account_Name = clsCommon.myCstr(dt.Rows(0)("GL_Account_Name"))

                End If
                objBankBook.CHEQUE_NO = obj.Cheque_No
                objBankBook.CHEQUE_DATE = obj.Cheque_Date
                objBankBook.NARR_DETAIL = ""
                If clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                    objBankBook.Credit_Amount = obj.Receipt_Amount
                    objBankBook.Debit_Amount = 0
                ElseIf clsCommon.CompairString(obj.Receipt_Type, "R") = CompairStringResult.Equal Then
                    objBankBook.Credit_Amount = 0
                    objBankBook.Debit_Amount = obj.UnApply_Amt
                Else
                    objBankBook.Credit_Amount = 0
                    objBankBook.Debit_Amount = obj.Receipt_Amount
                End If

                objBankBook.line_No = 1
                objBankBook.DocType = "Receipt"
                objBankBook.TransactionType = obj.Receipt_Type
                objBankBook.Currency = obj.CURRENCY_CODE
                objBankBook.Base_Currency = obj.BASE_CURRENCY_CODE
                objBankBook.Conversion_Rate = obj.ConvRate
                objBankBook.Remarks = obj.Entry_Desc
                objBankBook.Bank_charges = obj.Bank_Charges_Amt
                ArrBankBook.Add(objBankBook)
            End If
            isSaved = isSaved AndAlso ClsBankBook.SaveData(ArrBankBook, True, trans)
        End If

        Return isSaved
    End Function
    'Public Shared Function funRcptPost(ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal Module_Code As String = "MReceivable") As Boolean
    '    Dim strQ As String = Nothing
    '    Dim sqlDr As DataTable
    '    Try
    '        If objCommonVar.IsDemoERP = False Then
    '            strQ = "   SELECT TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Date, TSPL_RECEIPT_HEADER.Receipt_Post_Date,TSPL_RECEIPT_HEADER.Foreign_Bank_Charges_Amt ,TSPL_RECEIPT_HEADER.Bank_Charges_Amt, " & _
    '              "   TSPL_RECEIPT_HEADER.Entry_Desc, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_RECEIPT_HEADER.Receipt_Type,  " & _
    '              "   TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Customer_Name, TSPL_RECEIPT_HEADER.Reference,  " & _
    '              "   TSPL_RECEIPT_HEADER.Narration, TSPL_RECEIPT_HEADER.Payment_Code, TSPL_RECEIPT_HEADER.Cheque_No,TSPL_RECEIPT_HEADER.BASE_CURRENCY_CODE,  " & _
    '              "   TSPL_RECEIPT_HEADER.Cheque_Date, Cheque_From, TSPL_RECEIPT_HEADER.Receipt_Amount as Receipt_Amount, TSPL_RECEIPT_HEADER.Cust_Account,  " & _
    '              "   TSPL_RECEIPT_HEADER.Apply_By, TSPL_RECEIPT_HEADER.Apply_To, TSPL_RECEIPT_HEADER.Posted, TSPL_RECEIPT_HEADER.Balance_Amt,  " & _
    '              "   TSPL_RECEIPT_HEADER.Document_No, TSPL_RECEIPT_HEADER.UnApply_Amt, TSPL_RECEIPT_HEADER.Payer,  " & _
    '              "   TSPL_RECEIPT_HEADER.Dr_Account, TSPL_RECEIPT_HEADER.Cr_Account, TSPL_RECEIPT_HEADER.UnApplied_Balance, TSPL_RECEIPT_DETAIL.Receipt_No as [ReceiptNo_D], " & _
    '              "   TSPL_RECEIPT_DETAIL.Receipt_Line_No, TSPL_RECEIPT_DETAIL.Apply, TSPL_RECEIPT_DETAIL.Receipt_Type as [Type_D], " & _
    '              "   TSPL_RECEIPT_DETAIL.Document_No AS [Doc_D], TSPL_RECEIPT_DETAIL.Original_Amt, TSPL_RECEIPT_DETAIL.Pending_Balance, " & _
    '              "   TSPL_RECEIPT_DETAIL.Applied_Amount  as Applied_Amount, TSPL_RECEIPT_DETAIL.Account_Code, TSPL_RECEIPT_DETAIL.Description,  " & _
    '              "   TSPL_RECEIPT_DETAIL.Remarks, TSPL_RECEIPT_DETAIL.Comment, TSPL_RECEIPT_DETAIL.Shipment_No,  " & _
    '              "   TSPL_RECEIPT_DETAIL.Adjustment_Account, TSPL_RECEIPT_DETAIL.Adjustment_Cost, TSPL_RECEIPT_DETAIL.Adjustment_No,TSPL_RECEIPT_HEADER.UnApplied_No,TSPL_RECEIPT_HEADER.CURRENCY_CODE,TSPL_RECEIPT_HEADER.APPLICABLEFROM,TSPL_RECEIPT_HEADER.RECEIVED_AMOUNT_BASE_CURRENCY,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT, " & _
    '              "   TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_ACCOUNT,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_ACCOUNT,TSPL_RECEIPT_HEADER.ConvRate,TSPL_RECEIPT_DETAIL.ConvRateOld,TSPL_RECEIPT_HEADER.CForm_InvoiceNo " & _
    '              "   ,TSPL_RECEIPT_HEADER.location_gl_Code FROM TSPL_RECEIPT_HEADER left JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_RECEIPT_DETAIL.Receipt_No " & _
    '              "   where TSPL_RECEIPT_HEADER.Receipt_No ='" + strDocNo + "'"
    '        Else
    '            strQ = "   SELECT TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Date, TSPL_RECEIPT_HEADER.Receipt_Post_Date,TSPL_RECEIPT_HEADER.Foreign_Bank_Charges_Amt ,TSPL_RECEIPT_HEADER.Bank_Charges_Amt, " & _
    '              "   TSPL_RECEIPT_HEADER.Entry_Desc, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_RECEIPT_HEADER.Receipt_Type,  " & _
    '              "   TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Customer_Name, TSPL_RECEIPT_HEADER.Reference,  " & _
    '              "   TSPL_RECEIPT_HEADER.Narration, TSPL_RECEIPT_HEADER.Payment_Code, TSPL_RECEIPT_HEADER.Cheque_No,TSPL_RECEIPT_HEADER.BASE_CURRENCY_CODE,  "

    '            '"   TSPL_RECEIPT_HEADER.Cheque_Date, Cheque_From, TSPL_RECEIPT_HEADER.Receipt_Amount * (case when TSPL_RECEIPT_HEADER.ConvRateOld=0 then 1 else TSPL_RECEIPT_HEADER.ConvRateOld end) as Receipt_Amount, TSPL_RECEIPT_HEADER.Cust_Account,  " & _
    '            strQ += " TSPL_RECEIPT_HEADER.Cheque_Date, Cheque_From, TSPL_RECEIPT_HEADER.Receipt_Amount as Receipt_Amount, TSPL_RECEIPT_HEADER.Cust_Account,  " & _
    '              "   TSPL_RECEIPT_HEADER.Apply_By, TSPL_RECEIPT_HEADER.Apply_To, TSPL_RECEIPT_HEADER.Posted, TSPL_RECEIPT_HEADER.Balance_Amt,  " & _
    '              "   TSPL_RECEIPT_HEADER.Document_No, TSPL_RECEIPT_HEADER.UnApply_Amt, TSPL_RECEIPT_HEADER.Payer,  " & _
    '              "   TSPL_RECEIPT_HEADER.Dr_Account, TSPL_RECEIPT_HEADER.Cr_Account, TSPL_RECEIPT_HEADER.UnApplied_Balance, TSPL_RECEIPT_DETAIL.Receipt_No as [ReceiptNo_D], " & _
    '              "   TSPL_RECEIPT_DETAIL.Receipt_Line_No, TSPL_RECEIPT_DETAIL.Apply, TSPL_RECEIPT_DETAIL.Receipt_Type as [Type_D], " & _
    '              "   TSPL_RECEIPT_DETAIL.Document_No AS [Doc_D], TSPL_RECEIPT_DETAIL.Original_Amt, TSPL_RECEIPT_DETAIL.Pending_Balance, " & _
    '              "   TSPL_RECEIPT_DETAIL.Applied_Amount * TSPL_RECEIPT_HEADER.ConvRateOld as Applied_Amount, TSPL_RECEIPT_DETAIL.Account_Code, TSPL_RECEIPT_DETAIL.Description,  " & _
    '              "   TSPL_RECEIPT_DETAIL.Remarks, TSPL_RECEIPT_DETAIL.Comment, TSPL_RECEIPT_DETAIL.Shipment_No,  " & _
    '              "   TSPL_RECEIPT_DETAIL.Adjustment_Account, TSPL_RECEIPT_DETAIL.Adjustment_Cost, TSPL_RECEIPT_DETAIL.Adjustment_No, ISNULL(TSPL_RECEIPT_ADJUSTMENT_HEADER.Is_Post,'N') as AdjStatus, TSPL_RECEIPT_HEADER.UnApplied_No,TSPL_RECEIPT_HEADER.CURRENCY_CODE,TSPL_RECEIPT_HEADER.APPLICABLEFROM,TSPL_RECEIPT_HEADER.RECEIVED_AMOUNT_BASE_CURRENCY,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT, " & _
    '              "   TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_ACCOUNT,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_ACCOUNT,TSPL_RECEIPT_HEADER.ConvRate,TSPL_RECEIPT_DETAIL.ConvRateOld,TSPL_RECEIPT_HEADER.CForm_InvoiceNo, TSPL_RECEIPT_HEADER.Applied_Receipt " & _
    '              "   ,TSPL_RECEIPT_HEADER.location_gl_Code FROM TSPL_RECEIPT_HEADER left JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_RECEIPT_DETAIL.Receipt_No " & _
    '              " LEFT OUTER JOIN TSPL_Receipt_Adjustment_Header ON TSPL_Receipt_Adjustment_Header.Adjustment_No=TSPL_RECEIPT_DETAIL.Adjustment_No" & _
    '              "   where TSPL_RECEIPT_HEADER.Receipt_No ='" + strDocNo + "'"

    '        End If

    '        sqlDr = clsDBFuncationality.GetDataTable(strQ, trans)
    '        If sqlDr Is Nothing OrElse sqlDr.Rows.Count <= 0 Then
    '            Throw New Exception("Document No. not found to Post")
    '        End If

    '        '--------------------Checks Whether the Transaction is Locked or not----------------------------
    '        Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'", trans)
    '        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "Receipt Entry", LocSegmentCode, clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Date")), trans)
    '        '-----------------------------------------------------------------------------------------------

    '        If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "P") = CompairStringResult.Equal Then
    '            If Not String.IsNullOrEmpty(sqlDr.Rows(0)("CForm_InvoiceNo")) Then
    '                Dim qry = "update TSPL_SD_SALE_INVOICE_HEAD set CFormRecd=1 WHERE Document_Code ='" + sqlDr.Rows(0)("CForm_InvoiceNo") + "'"
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            End If

    '        End If


    '        '' MultiCurrency
    '        Dim IsMultiCurrency As Boolean = clsModuleCurrencyMapping.CheckMultiCurrency(Module_Code, trans)
    '        Dim CURRENCY_CODE As String = sqlDr.Rows(0)("CURRENCY_CODE").ToString
    '        Dim APPLICABLEFROM As Date? = Nothing
    '        If IsDBNull(sqlDr.Rows(0)("APPLICABLEFROM")) = True Then
    '            APPLICABLEFROM = Nothing
    '        Else
    '            APPLICABLEFROM = sqlDr.Rows(0)("APPLICABLEFROM")
    '        End If
    '        Dim EXCHANGE_LOSS_AMT As Double = clsCommon.myCdbl(sqlDr.Rows(0)("EXCHANGE_LOSS_AMT"))
    '        Dim EXCHANGE_GAIN_AMT As Double = clsCommon.myCdbl(sqlDr.Rows(0)("EXCHANGE_GAIN_AMT"))
    '        Dim EXCHANGE_GAIN_ACCOUNT As String = clsCommon.myCstr(sqlDr.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))
    '        Dim EXCHANGE_LOSS_ACCOUNT As String = clsCommon.myCstr(sqlDr.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
    '        Dim ConvRateOld As Double
    '        If objCommonVar.IsDemoERP Then
    '            ConvRateOld = IIf(clsCommon.myCdbl(sqlDr.Rows(0)("ConvRateOld")) = 0, 1, clsCommon.myCdbl(sqlDr.Rows(0)("ConvRateOld")))
    '        Else
    '            ConvRateOld = 1
    '        End If

    '        Dim ConvRate As Double = IIf(clsCommon.myCdbl(sqlDr.Rows(0)("ConvRate")) = 0, 1, clsCommon.myCdbl(sqlDr.Rows(0)("ConvRate")))

    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", CURRENCY_CODE, True)
    '        If clsCommon.myLen(APPLICABLEFROM) > 0 Then
    '            clsCommon.AddColumnsForChange(coll, "APPLICABLEFROM", clsCommon.GetPrintDate(APPLICABLEFROM, "dd/MMM/yyyy"), True)
    '        Else
    '            clsCommon.AddColumnsForChange(coll, "APPLICABLEFROM", Nothing, True)
    '        End If

    '        clsCommon.AddColumnsForChange(coll, "ConvRate", ConvRate)
    '        clsCommon.AddColumnsForChange(coll, "ConvRateOld", ConvRateOld)
    '        '' End MultiCurrency
    '        Dim UseSubAcc As String

    '        '' Anubhooti 01-Jan-2015 (Remarks : if setting "AllowToUseSubAccount" is ON And Bank_Type should be "B" Then BankAccount is Sub_Account Else previous)
    '        Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'", trans))
    '        '' Anubhooti 03-Sep-2014 BM00000003437(Remarks : if setting "AllowToUseSubAccount" is ON Then BankAccount is Sub_Account Else previous)
    '        Dim strQ1 As String
    '        Dim strBankAcc As String

    '        UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
    '        If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
    '            strQ1 = "select ISNULL(Sub_Account,'')  BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'"
    '            strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '        Else
    '            strQ1 = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'"
    '            strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '        End If
    '        ''
    '        If clsCommon.myLen(strBankAcc) <= 0 Then
    '            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
    '                Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")))
    '            Else
    '                Throw New Exception("Please enter bank account for bank " + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")))
    '            End If
    '        End If
    '        ''richa
    '        If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
    '            Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '            strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(sqlDr.Rows(0)("Cust_Code")) + "'"
    '            strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '            strBankAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankAcc, tBankLocation, trans)
    '        End If



    '        If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "M") = CompairStringResult.Equal Then
    '            Dim arrlist As New ArrayList()


    '            Dim Receipt_Line_No As Integer = 0
    '            Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '            If sqlDr IsNot Nothing AndAlso sqlDr.Rows.Count > 0 Then
    '                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
    '                For Each dr As DataRow In sqlDr.Rows
    '                    Dim strAccountLocation As String = clsCommon.myCstr(dr("Account_Code")).Substring(clsCommon.myLen(clsCommon.myCstr(dr("Account_Code"))) - 3, 3)

    '                    Dim dblAmount As Decimal = clsCommon.myCdbl(dr("Applied_Amount"))

    '                    If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strBankLocation, strAccountLocation) = CompairStringResult.Equal) Then
    '                        Dim Acc4() As String = {strBankAcc, dblAmount}
    '                        arrlist.Add(Acc4)

    '                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strAccountLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strAccountLocation)
    '                        End If
    '                        Dim RcvblAcc = New String() {strTemp, -1 * dblAmount}
    '                        arrlist.Add(RcvblAcc)

    '                        Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), -1 * dblAmount, clsCommon.myCstr(dr("Remarks"))}
    '                        arrlist.Add(acc3)

    '                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strAccountLocation, strBankLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + strAccountLocation + " and to location " + strBankLocation)
    '                        End If
    '                        RcvblAcc = New String() {strTemp, dblAmount}
    '                        arrlist.Add(RcvblAcc)
    '                    Else
    '                        Dim RcvblAcc() As String = {clsCommon.myCstr(dr("Account_Code")), -1 * (dblAmount), clsCommon.myCstr(dr("Remarks"))}
    '                        arrlist.Add(RcvblAcc)

    '                        Dim BankAcc() As String = {strBankAcc, dblAmount}
    '                        arrlist.Add(BankAcc)
    '                    End If
    '                    Dim strQue As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
    '                    clsDBFuncationality.ExecuteNonQuery(strQue, trans)
    '                    Receipt_Line_No = Receipt_Line_No + 1
    '                    Dim strQue1 As String = "update TSPL_RECEIPT_DETAIL set posted = 'Y' where receipt_no ='" + strDocNo + "' and  Receipt_Line_No = " & Receipt_Line_No & " "
    '                    clsDBFuncationality.ExecuteNonQuery(strQue1, trans)
    '                Next
    '            End If





    '            '' MULTICURRENCY
    '            If IsMultiCurrency Then
    '                'If (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT) <> 0 Then
    '                '    Dim BankAcc() As String = {strBankAcc, -EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT}
    '                '    arrlist.Add(BankAcc)
    '                'End If

    '                'If EXCHANGE_LOSS_AMT > 0 Then
    '                '    Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, -EXCHANGE_LOSS_AMT}
    '                '    arrlist.Add(CURR_EXCHANGE)
    '                'ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                '    Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                '    arrlist.Add(CURR_EXCHANGE)
    '                'End If
    '                If (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT) <> 0 Then
    '                    Dim BankAcc() As String = {strBankAcc, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT) / ConvRate}
    '                    arrlist.Add(BankAcc)
    '                End If

    '                If EXCHANGE_LOSS_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, -EXCHANGE_LOSS_AMT / ConvRate}
    '                    arrlist.Add(CURR_EXCHANGE)
    '                ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT / ConvRate}
    '                    arrlist.Add(CURR_EXCHANGE)
    '                End If
    '            End If
    '            '' END MULTICURRENCY

    '            transportSql.FunGrnlEntryWithTrans(LocSegmentCode, True, trans, clsCommon.myCDate(sqlDr.Rows(0)("Receipt_Post_Date")), sqlDr.Rows(0)("Entry_Desc").ToString(), "AR-MI", "AR Payment Received", strDocNo, "", "C", sqlDr.Rows(0)("Cust_Code").ToString(), sqlDr.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , sqlDr.Rows(0)("Reference").ToString(), sqlDr.Rows(0)("Narration").ToString(), coll)
    '            Dim strQue2 As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
    '            clsDBFuncationality.ExecuteNonQuery(strQue2, trans)


    '            '-22/11/2012--Added By--Pankaj Kumar---When Added New Receipt type[Misc Refund]---------------
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "S") = CompairStringResult.Equal Then
    '            '' changed by richa agarwal 
    '            Dim arrlist As New ArrayList()
    '            Dim Receipt_Line_No As Integer = 0
    '            Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '            If sqlDr IsNot Nothing AndAlso sqlDr.Rows.Count > 0 Then
    '                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
    '                For Each dr As DataRow In sqlDr.Rows
    '                    Dim strAccountLocation As String = clsCommon.myCstr(dr("Account_Code")).Substring(clsCommon.myLen(clsCommon.myCstr(dr("Account_Code"))) - 3, 3)

    '                    Dim dblAmount As Decimal = clsCommon.myCdbl(dr("Applied_Amount"))

    '                    If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strBankLocation, strAccountLocation) = CompairStringResult.Equal) Then
    '                        Dim Acc4() As String = {strBankAcc, dblAmount * -1}
    '                        arrlist.Add(Acc4)

    '                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strAccountLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strAccountLocation)
    '                        End If
    '                        Dim RcvblAcc = New String() {strTemp, 1 * dblAmount}
    '                        arrlist.Add(RcvblAcc)

    '                        Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), 1 * dblAmount, clsCommon.myCstr(dr("Remarks"))}
    '                        arrlist.Add(acc3)

    '                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strAccountLocation, strBankLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + strAccountLocation + " and to location " + strBankLocation)
    '                        End If
    '                        RcvblAcc = New String() {strTemp, dblAmount * -1}
    '                        arrlist.Add(RcvblAcc)
    '                    Else
    '                        Dim RcvblAcc() As String = {clsCommon.myCstr(dr("Account_Code")), 1 * (dblAmount), clsCommon.myCstr(dr("Remarks"))}
    '                        arrlist.Add(RcvblAcc)

    '                        Dim BankAcc() As String = {strBankAcc, dblAmount * -1}
    '                        arrlist.Add(BankAcc)
    '                    End If
    '                    Dim strQue As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
    '                    clsDBFuncationality.ExecuteNonQuery(strQue, trans)
    '                    Receipt_Line_No = Receipt_Line_No + 1
    '                    Dim strQue1 As String = "update TSPL_RECEIPT_DETAIL set posted = 'Y' where receipt_no ='" + strDocNo + "' and  Receipt_Line_No = " & Receipt_Line_No & " "
    '                    clsDBFuncationality.ExecuteNonQuery(strQue1, trans)
    '                Next
    '            End If

    '            ''Dim strQ1 As String = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'"
    '            ''Dim strBankAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '            'Dim arrlist As New ArrayList()
    '            '' Dim BankAcc() As String = {strBankAcc, ((clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount")) * -1) - IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))}
    '            'Dim BankAcc() As String = {strBankAcc, ((clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount")) * -1 * ConvRate) - IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))}
    '            'arrlist.Add(BankAcc)
    '            ' ''Dim sqlDr1 As SqlDataReader = connectSql.RunSqlReturnDR(strQ)
    '            'Dim Receipt_Line_No As Integer = 0
    '            'For Each dr As DataRow In sqlDr.Rows
    '            '    If Not String.IsNullOrEmpty(dr("Applied_Amount").ToString()) And dr("Applied_Amount").ToString() <> 0 Then
    '            '        Dim amt As Decimal = 0
    '            '        amt = CDec(dr("Applied_Amount").ToString())
    '            '        Dim acct As String = CStr(dr("Account_Code").ToString())
    '            '        Dim strRef As String = CStr(dr("Remarks").ToString())
    '            '        Dim RcvblAcc() As String = {acct, amt, strRef}
    '            '        arrlist.Add(RcvblAcc)
    '            '       
    '            '        Dim strQue As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
    '            '        clsDBFuncationality.ExecuteNonQuery(strQue, trans)
    '            '        Receipt_Line_No = Receipt_Line_No + 1
    '            '        Dim strQue1 As String = "update TSPL_RECEIPT_DETAIL set posted = 'Y' where receipt_no ='" + strDocNo + "' and  Receipt_Line_No = " & Receipt_Line_No & " "
    '            '        clsDBFuncationality.ExecuteNonQuery(strQue1, trans)
    '            '    End If
    '            'Next
    '            ''-------------commented
    '            ''------------------------------------------------------------------------------------------------------------------------------------------------------

    '            '' MULTICURRENCY
    '            'If IsMultiCurrency Then
    '            '    If EXCHANGE_LOSS_AMT > 0 Then
    '            '        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '            '        arrlist.Add(CURR_EXCHANGE)
    '            '    ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '            '        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, EXCHANGE_GAIN_AMT}
    '            '        arrlist.Add(CURR_EXCHANGE)
    '            '    End If
    '            'End If
    '            If IsMultiCurrency Then
    '                If EXCHANGE_LOSS_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT / ConvRate}
    '                    arrlist.Add(CURR_EXCHANGE)
    '                ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, EXCHANGE_GAIN_AMT / ConvRate}
    '                    arrlist.Add(CURR_EXCHANGE)
    '                End If
    '            End If

    '            '' END MULTICURRENCY
    '            transportSql.FunGrnlEntryWithTrans(LocSegmentCode, True, trans, clsCommon.myCDate(sqlDr.Rows(0)("Receipt_Post_Date")), sqlDr.Rows(0)("Entry_Desc").ToString(), "AR-MR", "AR Payment Received", strDocNo, "", "C", sqlDr.Rows(0)("Cust_Code").ToString(), sqlDr.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , sqlDr.Rows(0)("Reference").ToString(), sqlDr.Rows(0)("Narration").ToString(), coll)
    '            Dim strQue2 As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
    '            clsDBFuncationality.ExecuteNonQuery(strQue2, trans)
    '            '-------------------------------------------------------------------------------------------------------------------
    '        Else
    '            Dim strRcvblAcc As String
    '            Dim ArrList As ArrayList = New ArrayList()
    '            If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
    '                Dim strQuery As String = "select Isnull(Dr_Account, '') as Dr_Account  from TSPL_RECEIPT_HEADER where Receipt_No='" + strDocNo + "'"
    '                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQuery, trans))
    '            Else
    '                Dim strQuery As String = "select Isnull(Cr_Account, '') as Cr_Account  from TSPL_RECEIPT_HEADER where Receipt_No='" + strDocNo + "'"
    '                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQuery, trans))
    '            End If


    '            'Dim strQ2 As String = "select Isnull(Dr_Account , '') as Dr_Account  from TSPL_RECEIPT_HEADER where Receipt_No='" + strDocNo + "'"
    '            'strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ2, trans))

    '            '' Anubhooti 01-Dec-2014 (Advance Account should be come acc. to amit sir)
    '            If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "U") = CompairStringResult.Equal Then
    '                Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '                strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(sqlDr.Rows(0)("Cust_Code")) + "'"
    '                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '                'strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankAcc, tBankLocation, trans)
    '            End If
    '            ''
    '            Dim RcvblAcc() As String
    '            Dim BankAcc() As String
    '            Dim bankAccountChargesAmount As Double = 0
    '            Dim AdjAmt As Decimal = 0
    '            If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
    '                If clsCommon.myLen(sqlDr.Rows(0)("Adjustment_No")) > 0 And clsCommon.CompairString(sqlDr.Rows(0)("AdjStatus"), "N") = CompairStringResult.Equal Then
    '                    clsAdjustmentEntryReceivables.FunPost(clsCommon.myCstr(sqlDr.Rows(0)("Adjustment_No")), trans)
    '                End If
    '                'BankAcc = New String() {strBankAcc, (CDec(sqlDr.Rows(0)("Receipt_Amount").ToString()) + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))}
    '                'ArrList.Add(BankAcc)
    '                'RcvblAcc = New String() {strRcvblAcc, (Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString())) * -1}
    '                'ArrList.Add(RcvblAcc)
    '                '--------------------------This COde Updates Balance Amount of Invoice--------------------------
    '                Dim qry As String = "select TSPL_RECEIPT_DETAIL.Document_No,case when isnull(TSPL_Customer_Invoice_Head.ConvRate,0)=0 then 1 else TSPL_Customer_Invoice_Head.ConvRate end  as ConvRateOld, (Case When TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' Then Applied_Amount Else Applied_Amount*-1 End) as Applied_Amount, TagType ,TSPL_Customer_Invoice_Head.Loc_Code from TSPL_RECEIPT_DETAIL "
    '                qry += " LEFT OUTER JOIN TSPL_Receipt_Adjustment_Header ON TSPL_Receipt_Adjustment_Header.Adjustment_No=TSPL_RECEIPT_DETAIL.Adjustment_No "
    '                qry += " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No"
    '                qry += " WHERE TSPL_RECEIPT_DETAIL.Receipt_No='" + strDocNo + "'"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '                Dim StartGLAccordingToBrach As Boolean = True
    '                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
    '                Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '                For Each dr As DataRow In dt.Rows
    '                    ''richa agarwal 18/05/2015
    '                    Dim dblAmount As Double = clsCommon.myCdbl(dr("Applied_Amount")) * clsCommon.myCdbl(dr("ConvRateOld"))
    '                    Dim dblAmount1 As Double = clsCommon.myCdbl(dr("Applied_Amount")) * ConvRate
    '                    ' Dim dblAmount1 As Double = clsCommon.myCdbl(dr("Applied_Amount")) * ConvRate - ((clsCommon.myCdbl(ConvRate * clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt"))))

    '                    '         Dim dblAmount As Double = clsCommon.myCdbl(dr("Applied_Amount")) / ConvRate
    '                    ' Dim dblAmount As Double = clsCommon.myCdbl(dr("Applied_Amount")) / ConvRateOld
    '                    ''------------------------
    '                    Dim strDocLocation As String = clsCommon.myCstr(dr("Loc_Code"))
    '                    If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strBankLocation, strDocLocation) = CompairStringResult.Equal) Then
    '                        ''Add By Balwinder for Apply branch.
    '                        If StartGLAccordingToBrach Then
    '                            ArrList = New ArrayList()
    '                            StartGLAccordingToBrach = False
    '                        End If
    '                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, strBankLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + strDocLocation + " and to location " + strBankLocation)
    '                        End If
    '                        ''branch accounting of customer
    '                        RcvblAcc = New String() {strTemp, dblAmount1}
    '                        'RcvblAcc = New String() {strTemp, -1 * dblAmount1}
    '                        ArrList.Add(RcvblAcc)

    '                        ''entry for customer
    '                        strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strDocLocation, trans)
    '                        'RcvblAcc = New String() {strTemp, -1 * dblAmount1}
    '                        RcvblAcc = New String() {strTemp, -1 * dblAmount}
    '                        ArrList.Add(RcvblAcc)

    '                        ''richa agarwal
    '                        'RcvblAcc = New String() {strBankAcc, dblAmount}
    '                        'ArrList.Add(RcvblAcc)
    '                        '' bank entry
    '                        RcvblAcc = New String() {strBankAcc, dblAmount1}
    '                        ArrList.Add(RcvblAcc)
    '                        ''----------------------

    '                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strDocLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strDocLocation)
    '                        End If
    '                        ''branch accounting of bank
    '                        'RcvblAcc = New String() {strTemp, dblAmount}
    '                        RcvblAcc = New String() {strTemp, -1 * dblAmount1}
    '                        ArrList.Add(RcvblAcc)

    '                        '' 01-Dec-2014 (Merge Un-Applied GL into Receipt GL Entry)
    '                        If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
    '                            If clsCommon.myCdbl(sqlDr.Rows(0)("UnApplied_Balance")) > 0 Then
    '                                Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '                                strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(sqlDr.Rows(0)("Cust_Code")) + "'"
    '                                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '                                'strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strDocLocation, trans)
    '                                RcvblAcc = New String() {strRcvblAcc, -1 * clsCommon.myCdbl(sqlDr.Rows(0)("UnApplied_Balance"))}
    '                                ArrList.Add(RcvblAcc)
    '                                '
    '                                RcvblAcc = New String() {strBankAcc, clsCommon.myCdbl(sqlDr.Rows(0)("UnApplied_Balance"))}
    '                                ArrList.Add(RcvblAcc)
    '                            End If

    '                        End If
    '                        ''
    '                        '-----------------------------------------------
    '                        'richa agarwal 15/06/2015
    '                        If clsCommon.CompairString(CURRENCY_CODE, clsCommon.myCstr(sqlDr.Rows(0)("BASE_CURRENCY_CODE"))) <> CompairStringResult.Equal Then

    '                            Dim totalgainlossamount As Double = dblAmount1 - dblAmount
    '                            If totalgainlossamount = 0 Then
    '                                EXCHANGE_LOSS_AMT = 0
    '                                EXCHANGE_GAIN_AMT = 0
    '                            ElseIf totalgainlossamount < 0 Then
    '                                If clsCommon.myLen(EXCHANGE_LOSS_ACCOUNT) = 0 Then
    '                                    Throw New Exception("Exchange Loss Account not defined.")
    '                                End If
    '                                EXCHANGE_LOSS_AMT = -totalgainlossamount
    '                                EXCHANGE_GAIN_AMT = 0
    '                            Else
    '                                If clsCommon.myLen(EXCHANGE_GAIN_ACCOUNT) = 0 Then
    '                                    Throw New Exception("Exchange Gain Account not defined.")
    '                                End If
    '                                EXCHANGE_LOSS_AMT = 0
    '                                EXCHANGE_GAIN_AMT = totalgainlossamount
    '                            End If

    '                            If EXCHANGE_LOSS_AMT > 0 Then
    '                                'Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                                'ArrList.Add(CURR_EXCHANGE)
    '                                Dim strLossLocation As String = EXCHANGE_LOSS_ACCOUNT.Substring(clsCommon.myLen(EXCHANGE_LOSS_ACCOUNT) - 3, 3)
    '                                'If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strLossLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                                Dim strTemp1 As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, strLossLocation, trans)
    '                                If clsCommon.myLen(strTemp1) <= 0 Then
    '                                    Throw New Exception("Please set Branch account mapping with from location " + strLossLocation + " and to location " + strDocLocation)
    '                                End If
    '                                Dim BranchAccCRLoss = New String() {strTemp1, EXCHANGE_LOSS_AMT}
    '                                ArrList.Add(BranchAccCRLoss)

    '                                'End If
    '                            ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                                'Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                                'ArrList.Add(CURR_EXCHANGE)
    '                                Dim strGainLocation As String = EXCHANGE_GAIN_ACCOUNT.Substring(clsCommon.myLen(EXCHANGE_GAIN_ACCOUNT) - 3, 3)
    '                                Dim strTemp1 As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, strGainLocation, trans)
    '                                If clsCommon.myLen(strTemp1) <= 0 Then
    '                                    Throw New Exception("Please set Branch account mapping with from location " + strGainLocation + " and to location " + strDocLocation)
    '                                End If
    '                                Dim BranchAccCRGain = New String() {strTemp1, -EXCHANGE_GAIN_AMT}
    '                                ArrList.Add(BranchAccCRGain)

    '                            End If

    '                        End If
    '                        ''------------------------


    '                        ''End of Apply branch.
    '                    Else
    '                        'If dblAmount1 < dblAmount Then
    '                        '    BankAcc = New String() {strBankAcc, dblAmount1}
    '                        '    ArrList.Add(BankAcc)
    '                        '    RcvblAcc = New String() {strRcvblAcc, -1 * dblAmount}
    '                        '    ArrList.Add(RcvblAcc)
    '                        'ElseIf dblAmount1 < dblAmount Then
    '                        '    BankAcc = New String() {strBankAcc, dblAmount1}
    '                        '    ArrList.Add(BankAcc)
    '                        '    RcvblAcc = New String() {strRcvblAcc, -1 * dblAmount}
    '                        '    ArrList.Add(RcvblAcc)
    '                        'Else

    '                        '    BankAcc = New String() {strBankAcc, dblAmount}
    '                        '    ArrList.Add(BankAcc)
    '                        '    RcvblAcc = New String() {strRcvblAcc, -1 * dblAmount1}
    '                        '    ArrList.Add(RcvblAcc)
    '                        'End If
    '                        BankAcc = New String() {strBankAcc, dblAmount1}
    '                        ArrList.Add(BankAcc)
    '                        RcvblAcc = New String() {strRcvblAcc, -1 * dblAmount}
    '                        ArrList.Add(RcvblAcc)
    '                        ''richa agarwal 16/06/2015
    '                        If clsCommon.CompairString(CURRENCY_CODE, clsCommon.myCstr(sqlDr.Rows(0)("BASE_CURRENCY_CODE"))) <> CompairStringResult.Equal Then
    '                            If clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "R") = CompairStringResult.Equal Or clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "A") = CompairStringResult.Equal Then
    '                                If EXCHANGE_LOSS_AMT > 0 Then
    '                                    Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                                    ArrList.Add(CURR_EXCHANGE)
    '                                ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                                    Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                                    ArrList.Add(CURR_EXCHANGE)
    '                                End If
    '                            End If
    '                        End If
    '                        ''------------------sdw
    '                        '' Anubhooti 01-Dec-2014 (If Branch Accounting is not applied Receiving)
    '                        If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
    '                            If clsCommon.myCdbl(sqlDr.Rows(0)("UnApplied_Balance")) > 0 Then
    '                                Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '                                strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(sqlDr.Rows(0)("Cust_Code")) + "'"
    '                                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '                                ''richa agarwal 30/06/2015
    '                                strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strDocLocation, trans)
    '                                ''--------------
    '                                RcvblAcc = New String() {strRcvblAcc, -1 * clsCommon.myCdbl(sqlDr.Rows(0)("UnApplied_Balance"))}
    '                                ArrList.Add(RcvblAcc)
    '                                '
    '                                RcvblAcc = New String() {strBankAcc, clsCommon.myCdbl(sqlDr.Rows(0)("UnApplied_Balance"))}
    '                                ArrList.Add(RcvblAcc)
    '                            End If

    '                        End If
    '                        ''
    '                    End If
    '                    ''richa agarwal 18/05/2015
    '                    clsReceiptDettail.funBalanceAmtSave(dr("Document_No"), clsCommon.myCdbl(dr("Applied_Amount")), trans, dr("TagType"))
    '                    'If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
    '                    '    clsReceiptDettail.funBalanceAmtSave(dr("Document_No"), clsCommon.myCdbl(dr("Applied_Amount")), trans, dr("TagType"))

    '                    'Else
    '                    '    clsReceiptDettail.funBalanceAmtSave(dr("Document_No"), clsCommon.myCdbl(dr("Applied_Amount") / ConvRateOld), trans, dr("TagType"))
    '                    'End If

    '                    ''------------------
    '                Next
    '                ''richa agarwal 09/06/2015
    '                If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
    '                    bankAccountChargesAmount = (clsCommon.myCdbl(ConvRate * clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt")))
    '                    BankAcc = New String() {strBankAcc, -1 * bankAccountChargesAmount}
    '                    ArrList.Add(BankAcc)
    '                End If

    '                '---------------------------
    '                '-----------------------------------------------------------------------------------------------

    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "O") = CompairStringResult.Equal Then
    '                Dim strTemp As String = ""


    '                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankAcc, sqlDr.Rows(0)("Location_GL_Code"), trans)
    '                'If clsCommon.myLen(strTemp) <= 0 Then
    '                '    Throw New Exception("Please set Brach account mapping with from location " + strBankAcc + " and to location " + sqlDr.Rows(0)("Location_GL_Code"))
    '                'End If




    '                '  BankAcc = New String() {strBankAcc, (sqlDr.Rows(0)("Receipt_Amount").ToString() + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))}
    '                BankAcc = New String() {strBankAcc, (sqlDr.Rows(0)("Receipt_Amount").ToString() * ConvRate - ((ConvRate * clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt"))) + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))}
    '                ArrList.Add(BankAcc)
    '                ''richa agarwal 05/06/2015
    '                If clsCommon.myLen(sqlDr.Rows(0)("Location_GL_Code").ToString()) > 0 Then
    '                    strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, sqlDr.Rows(0)("Location_GL_Code").ToString(), trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + strBankAcc + " and to location " + sqlDr.Rows(0)("Location_GL_Code"))
    '                    End If

    '                    RcvblAcc = New String() {strTemp, Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) * ConvRate * -1}
    '                    ArrList.Add(RcvblAcc)
    '                Else

    '                    'RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) * -1}
    '                    RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) * ConvRate * -1}
    '                    ArrList.Add(RcvblAcc)
    '                End If

    '                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, sqlDr.Rows(0)("Location_GL_Code").ToString(), trans)
    '                'If clsCommon.myLen(strTemp) <= 0 Then
    '                '    Throw New Exception("Please set Branch account mapping with from location " + strBankAcc + " and to location " + sqlDr.Rows(0)("Location_GL_Code"))
    '                'End If

    '                'RcvblAcc = New String() {strTemp, Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) * -1}
    '                'ArrList.Add(RcvblAcc)
    '                ''-------------------------------


    '                Dim InvcNo As String = ""
    '                Dim BalAmt As Decimal = 0.0
    '                Dim drtotal As Decimal = clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString())
    '                Dim PayAmt As Decimal = drtotal
    '                strQ = " select Document_No as Document_No  ,Due_Date ,case when fifo_balance>0 then fifo_balance else   Balance_Amt end as  Balance_Amt from TSPL_Customer_Invoice_Head" & _
    '                       " where Balance_Amt>0 and Customer_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "' and  fifo_knockoff='N' order by TSPL_Customer_Invoice_Head.Due_Date "
    '                Dim Dt1 As DataTable = New DataTable()
    '                Dt1 = clsDBFuncationality.GetDataTable(strQ, trans)
    '                For Each dr As DataRow In Dt1.Rows
    '                    InvcNo = dr.Item("Document_No").ToString()
    '                    BalAmt = dr.Item("Balance_Amt")
    '                    If drtotal > BalAmt Then
    '                        drtotal = drtotal - BalAmt
    '                        strQ = "update TSPL_Customer_Invoice_Head set fifo_balance=0.00 , fifo_knockoff='Y' where Document_No ='" + InvcNo + "' and Customer_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "'"
    '                        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
    '                    ElseIf drtotal < BalAmt Then
    '                        drtotal = drtotal - BalAmt
    '                        strQ = "update TSPL_Customer_Invoice_Head set fifo_balance=" + (drtotal * -1).ToString() + "-fifo_balance , fifo_knockoff='N' where Document_No ='" + InvcNo + "' and Customer_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "'"
    '                        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
    '                    End If
    '                    If drtotal < 0 Then
    '                        Exit For
    '                    End If
    '                Next
    '                If drtotal > 0 Then
    '                    strQ = "update TSPL_RECEIPT_HEADER set fifo_balance=" + drtotal.ToString() + " where Receipt_No ='" + strDocNo + "'"
    '                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
    '                End If

    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then

    '            Else
    '                ''richa AGARWAL 14/05/2015  BANK ACCOUNT CREDIT(MEANS -)
    '                If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
    '                    'BankAcc = New String() {strBankAcc, clsCommon.myCdbl((sqlDr.Rows(0)("Receipt_Amount").ToString() + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))) * -1}
    '                    BankAcc = New String() {strBankAcc, clsCommon.myCdbl((sqlDr.Rows(0)("Receipt_Amount").ToString() * ConvRate - ((ConvRate * clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt"))) + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))) * -1}
    '                Else
    '                    ''richa agarwal deduct foreign bank amount * converison + indian bANK AMOUNT from bank amount
    '                    ' BankAcc = New String() {strBankAcc, (sqlDr.Rows(0)("Receipt_Amount").ToString() + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))}
    '                    BankAcc = New String() {strBankAcc, (sqlDr.Rows(0)("Receipt_Amount").ToString() * ConvRate - ((ConvRate * clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt"))) + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))}

    '                End If
    '                ''----------------------------------------
    '                ArrList.Add(BankAcc)
    '                If clsCommon.myLen(clsCommon.myCstr(sqlDr.Rows(0)("Location_GL_Code"))) > 0 Then
    '                    strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, clsCommon.myCstr(sqlDr.Rows(0)("Location_GL_Code")), trans)
    '                End If
    '                ''richa AGARWAL 14/05/2015  CUSTOMER ACCOUNT DEBIT(MEANS +)
    '                If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
    '                    RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) * ConvRate * 1}
    '                Else
    '                    RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) * ConvRate * -1}
    '                End If
    '                ''-----------------------------
    '                ArrList.Add(RcvblAcc)
    '            End If

    '            Dim PayType As String = ""
    '            If sqlDr.Rows(0)("Receipt_Type").ToString() = "R" Then
    '                PayType = "AR-PY"
    '            ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "O" Then
    '                PayType = "AR-OA"
    '            ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "A" Then
    '                PayType = "AR-DC"
    '            ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "P" Then 'Or sqlDr.Rows(0)("Receipt_Type").ToString() = "A" Then
    '                PayType = "AR-PI"
    '            ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "U" Then
    '                PayType = "AR-UC"
    '            ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "F" Then
    '                PayType = "AR-RF"
    '            End If
    '            '' MULTICURRENCY
    '            'If IsMultiCurrency Then
    '            '    If EXCHANGE_LOSS_AMT > 0 Then
    '            '        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '            '        ArrList.Add(CURR_EXCHANGE)
    '            '    ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '            '        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '            '        ArrList.Add(CURR_EXCHANGE)
    '            '    End If
    '            'End If
    '            If IsMultiCurrency Then
    '                If clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "R") = CompairStringResult.Equal Or clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "A") = CompairStringResult.Equal Then
    '                    'If EXCHANGE_LOSS_AMT > 0 Then
    '                    '    Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                    '    ArrList.Add(CURR_EXCHANGE)
    '                    'ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                    '    Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                    '    ArrList.Add(CURR_EXCHANGE)
    '                    'End If
    '                Else
    '                    If EXCHANGE_LOSS_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT / ConvRate}
    '                        ArrList.Add(CURR_EXCHANGE)
    '                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT / ConvRate}
    '                        ArrList.Add(CURR_EXCHANGE)
    '                    End If
    '                End If


    '            End If

    '            If clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "P") = CompairStringResult.Equal Or clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "O") = CompairStringResult.Equal Or clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then
    '                '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location)

    '                Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '                Dim strGLLoc As String = clsCommon.myCstr(sqlDr.Rows(0)("Location_GL_Code"))

    '                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
    '                If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strBankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    'stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strGLLoc, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strGLLoc)
    '                    End If


    '                    ''--------acc. to priti mam code
    '                    'Dim BranchAccCR = New String() {strTemp, -1 * clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString())}
    '                    'ArrList.Add(BranchAccCR)

    '                    'strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, strBankLocation, trans)
    '                    'If clsCommon.myLen(strTemp) <= 0 Then
    '                    '    Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + strBankLocation)
    '                    'End If
    '                    'Dim BranchAccDR = New String() {strTemp, clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString())}
    '                    'ArrList.Add(BranchAccDR)

    '                    If clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then
    '                        'Dim BranchAccCR = New String() {strTemp, 1 * clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString())}
    '                        Dim BranchAccCR = New String() {strTemp, 1 * clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString()) * ConvRate}
    '                        ArrList.Add(BranchAccCR)
    '                    Else
    '                        'Dim BranchAccCR = New String() {strTemp, -1 * clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString())}
    '                        Dim BranchAccCR = New String() {strTemp, -1 * clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString()) * ConvRate}
    '                        ArrList.Add(BranchAccCR)
    '                    End If



    '                    strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, strBankLocation, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + strBankLocation)
    '                    End If
    '                    If clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then
    '                        Dim BranchAccDR = New String() {strTemp, -1 * clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString()) * ConvRate}
    '                        ArrList.Add(BranchAccDR)
    '                    Else
    '                        'Dim BranchAccDR = New String() {strTemp, clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString())}
    '                        Dim BranchAccDR = New String() {strTemp, clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString()) * ConvRate}
    '                        ArrList.Add(BranchAccDR)
    '                    End If

    '                End If
    '                ''----------------------------------------
    '            End If

    '            ''richa agarwal
    '            Dim strForeignBankAcc As String = String.Empty
    '            Dim strBankChargesOtherAcc As String = String.Empty
    '            Dim BankChargesAcc() As String
    '            If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "F") <> CompairStringResult.Equal Then
    '                ' Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
    '                If clsCommon.myCdbl(clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))) <> 0 Then
    '                    strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account  FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(sqlDr.Rows(0)("Cust_Code")) + "'"
    '                    strForeignBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '                    'strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strDocLocation, trans)
    '                    If clsCommon.myLen(strForeignBankAcc) <= 0 Then
    '                        Throw New Exception("Please Select Foreign Bank Charges Account first ")
    '                        Return False
    '                    End If
    '                    BankChargesAcc = New String() {strForeignBankAcc, ConvRate * clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))}
    '                    ArrList.Add(BankChargesAcc)
    '                    '
    '                End If
    '                If clsCommon.myCdbl(clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt"))) <> 0 Then
    '                    strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(sqlDr.Rows(0)("Cust_Code")) + "'"
    '                    strBankChargesOtherAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '                    If clsCommon.myLen(strBankChargesOtherAcc) <= 0 Then
    '                        Throw New Exception("Please Select Bank Charges Other Account first ")
    '                        Return False
    '                    End If
    '                    BankChargesAcc = New String() {strBankChargesOtherAcc, clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt"))}
    '                    ArrList.Add(BankChargesAcc)
    '                End If
    '            Else
    '                If clsCommon.myCdbl(clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))) <> 0 Then
    '                    strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account  FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(sqlDr.Rows(0)("Cust_Code")) + "'"
    '                    strForeignBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '                    If clsCommon.myLen(strForeignBankAcc) <= 0 Then
    '                        Throw New Exception("Please Select Foreign Bank Charges Account first ")
    '                        Return False
    '                    End If
    '                    BankChargesAcc = New String() {strForeignBankAcc, -1 * ConvRate * clsCommon.myCdbl(sqlDr.Rows(0)("Foreign_Bank_Charges_Amt"))}
    '                    ArrList.Add(BankChargesAcc)
    '                End If

    '                '
    '                If clsCommon.myCdbl(clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt"))) <> 0 Then
    '                    strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(sqlDr.Rows(0)("Cust_Code")) + "'"
    '                    strBankChargesOtherAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
    '                    If clsCommon.myLen(strBankChargesOtherAcc) <= 0 Then
    '                        Throw New Exception("Please Select Bank Charges Other Account first ")
    '                        Return False
    '                    End If
    '                    BankChargesAcc = New String() {strBankChargesOtherAcc, -1 * clsCommon.myCdbl(sqlDr.Rows(0)("Bank_Charges_Amt"))}
    '                    ArrList.Add(BankChargesAcc)
    '                End If

    '            End If
    '            ''--------------------

    '            transportSql.FunGrnlEntryWithTrans(LocSegmentCode, True, trans, clsCommon.myCDate(sqlDr.Rows(0)("Receipt_Post_Date")), sqlDr.Rows(0)("Entry_Desc").ToString(), PayType, "AR Payment Received", strDocNo, "", "C", sqlDr.Rows(0)("Cust_Code").ToString(), sqlDr.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , sqlDr.Rows(0)("Reference").ToString(), sqlDr.Rows(0)("Narration").ToString(), coll)

    '            If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
    '                clsRcptEntryHeader.UpdateBalance(clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")), clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount")), trans)
    '            End If
    '            Dim strQue As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
    '            clsDBFuncationality.ExecuteNonQuery(strQue, trans)
    '            strQue = "update TSPL_RECEIPT_DETAIL set posted = 'Y' where receipt_no ='" + strDocNo + "' "
    '            clsDBFuncationality.ExecuteNonQuery(strQue, trans)
    '            If sqlDr.Rows(0)("UnApplied_No").ToString() <> "" Then
    '                Dim DocUnAppliedNo As String = sqlDr.Rows(0)("UnApplied_No").ToString()
    '                If clsRcptEntryHeader.funRcptPostUnApplied(DocUnAppliedNo, trans) = False Then
    '                    Return False
    '                End If
    '            End If
    '        End If
    '        '-------------This Code Creates Unapplied entry and post also-----------------------
    '        If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
    '            If clsCommon.myCdbl(sqlDr.Rows(0)("UnApplied_Balance")) > 0 Then
    '                Dim obj As clsRcptEntryHeader
    '                obj = clsRcptEntryHeader.GetData(strDocNo, NavigatorType.Current, trans)
    '                clsRcptEntryHeader.funUnAppliedEntry(obj, trans)
    '                '' Anubhooti 02-Dec-2014 (No Seperate GL Entry will generate for Un-Applied)
    '                ' clsRcptEntryHeader.funRcptPost(obj.Receipt_No, trans)

    '            End If

    '        End If

    '        ' post  bank transfer
    '        Dim obj1 As clsRcptEntryHeader
    '        obj1 = clsRcptEntryHeader.GetData(strDocNo, NavigatorType.Current, trans)
    '        If clsCommon.myLen(obj1.Transfer_No) > 0 Then
    '            clsBankTrasnferNew.PostData(obj1.Transfer_No, trans)
    '        End If
    '        '---------------------------------------------------------------------

    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '        Return False
    '    End Try
    'End Function
   
    Public Shared Function funRcptPost(ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal Module_Code As String = "MReceivable", Optional strInvFromLoc As String = Nothing, Optional ByVal strJurEntryDefault As Boolean = False) As Boolean
        Try
            Dim strQ As String = GetQuery(strDocNo)
            Dim sqlDr As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
            If sqlDr Is Nothing OrElse sqlDr.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            Dim Set_Off_Date As String = ""
            If IsDBNull(sqlDr.Rows(0)("Set_Off_Date")) = False Then
                Set_Off_Date = clsCommon.GetPrintDate(sqlDr.Rows(0)("Set_Off_Date"), "dd-MMM-yyyy")
            End If
            '' Validation check: by Panch Raj against ticket No:BM00000008437
            ' CheckNegativeBankBalance(sqlDr, trans)
            '--------------------Checks Whether the Transaction is Locked or not-----------------------------UDL/24/07/18-000206 richa 
            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'", trans)
            Dim strAllowtoUnlockTransactionsforSetOff As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoUnlockTransactionsforSetOff, clsFixedParameterCode.AllowtoUnlockTransactionsforSetOff, trans))
            If clsCommon.CompairString(strAllowtoUnlockTransactionsforSetOff, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
            Else
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "Receipt Entry", LocSegmentCode, clsCommon.myCDate(sqlDr.Rows(0)("Receipt_Date")), trans)
            End If
                '-----------------------------------------------------------------------------------------------
                If clsCommon.myCBool(clsCommon.CompairString(strJurEntryDefault, True)) = CompairStringResult.Equal Then
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSkipJournalEntryofPaymentandReceiptforAD, clsFixedParameterCode.AllowtoSkipJournalEntryofPaymentandReceiptforAD, trans)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal) Then
                Else
                    ''richa 12 Nov,2018  TEC/02/11/18-000363 create journal entry for opening in case of Misc receipt and Advance (Security) as Journal Master table instead of journal master op table
                    'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
                    Dim JEWithOPening As Boolean = False
                    If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                        Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                        If clsCommon.myCDate(sqlDr.Rows(0)("Receipt_date")) <= clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy") Then
                            JEWithOPening = True
                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, trans)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "P") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "M") = CompairStringResult.Equal) And JEWithOPening = True Then
                        CreateJournalEntryForOpening(False, sqlDr, strDocNo, LocSegmentCode, trans, "", Module_Code, strInvFromLoc)
                    Else
                        CreateJournalEntry(False, sqlDr, strDocNo, LocSegmentCode, trans, "", Module_Code, strInvFromLoc)
                    End If
                    ''-----------------
                    ' CreateJournalEntry(False, sqlDr, strDocNo, LocSegmentCode, trans, "", Module_Code, strInvFromLoc)
                    End If

                End If

                If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "P") = CompairStringResult.Equal Then
                    If Not String.IsNullOrEmpty(sqlDr.Rows(0)("CForm_InvoiceNo")) Then
                        Dim qry = "update TSPL_SD_SALE_INVOICE_HEAD set CFormRecd=1 WHERE Document_Code ='" + sqlDr.Rows(0)("CForm_InvoiceNo") + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
                '' Anubhooti 01-Jan-2015 (Remarks : if setting "AllowToUseSubAccount" is ON And Bank_Type should be "B" Then BankAccount is Sub_Account Else previous)
                Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'", trans))
                '' Anubhooti 03-Sep-2014 BM00000003437(Remarks : if setting "AllowToUseSubAccount" is ON Then BankAccount is Sub_Account Else previous)
                Dim strBankAcc As String = ""
                Dim UseSubAcc As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
                If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                    strQ = "select ISNULL(Sub_Account,'')  BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'"
                    strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
                Else
                    strQ = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(sqlDr.Rows(0)("Bank_Code")) + "'"
                    strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
                End If
                If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "M") = CompairStringResult.Equal Then
                    Dim Receipt_Line_No As Integer = 0
                    If sqlDr IsNot Nothing AndAlso sqlDr.Rows.Count > 0 Then
                        For Each dr As DataRow In sqlDr.Rows
                            Dim strQue As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
                            clsDBFuncationality.ExecuteNonQuery(strQue, trans)
                            Receipt_Line_No = Receipt_Line_No + 1
                            Dim strQue1 As String = "update TSPL_RECEIPT_DETAIL set posted = 'Y' where receipt_no ='" + strDocNo + "' and  Receipt_Line_No = " & Receipt_Line_No & " "
                            clsDBFuncationality.ExecuteNonQuery(strQue1, trans)
                        Next
                    End If
                    Dim strQue2 As String = "update TSPL_receipt_header set posted = 'Y',Modify_By='" + objCommonVar.CurrentUserCode + "' where receipt_no ='" + strDocNo + "' "
                    clsDBFuncationality.ExecuteNonQuery(strQue2, trans)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "S") = CompairStringResult.Equal Then
                    Dim Receipt_Line_No As Integer = 0
                    If sqlDr IsNot Nothing AndAlso sqlDr.Rows.Count > 0 Then
                        Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                        For Each dr As DataRow In sqlDr.Rows
                            Dim strQue As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
                            clsDBFuncationality.ExecuteNonQuery(strQue, trans)
                            Receipt_Line_No = Receipt_Line_No + 1
                            Dim strQue1 As String = "update TSPL_RECEIPT_DETAIL set posted = 'Y' where receipt_no ='" + strDocNo + "' and  Receipt_Line_No = " & Receipt_Line_No & " "
                            clsDBFuncationality.ExecuteNonQuery(strQue1, trans)
                        Next
                    End If
                    Dim strQue2 As String = "update TSPL_receipt_header set posted = 'Y',Modify_By='" + objCommonVar.CurrentUserCode + "' where receipt_no ='" + strDocNo + "' "
                    clsDBFuncationality.ExecuteNonQuery(strQue2, trans)
                    '-------------------------------------------------------------------------------------------------------------------
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                        If clsCommon.myLen(sqlDr.Rows(0)("Adjustment_No")) > 0 And clsCommon.CompairString(sqlDr.Rows(0)("AdjStatus"), "N") = CompairStringResult.Equal Then
                            clsAdjustmentEntryReceivables.FunPost(clsCommon.myCstr(sqlDr.Rows(0)("Adjustment_No")), trans)
                        End If
                        '--------------------------This COde Updates Balance Amount of Invoice--------------------------
                        'Dim qry As String = "select TSPL_RECEIPT_DETAIL.Document_No,case when isnull(TSPL_Customer_Invoice_Head.ConvRate,0)=0 then 1 else TSPL_Customer_Invoice_Head.ConvRate end  as ConvRateOld, (Case When TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' Then Applied_Amount Else Applied_Amount*-1 End) as Applied_Amount, TagType,ISNULL(TSPL_Customer_Invoice_Head.Document_Type,'') AS  DocType ,TSPL_Customer_Invoice_Head.Loc_Code from TSPL_RECEIPT_DETAIL "
                        'qry += " LEFT OUTER JOIN TSPL_Receipt_Adjustment_Header ON TSPL_Receipt_Adjustment_Header.Adjustment_No=TSPL_RECEIPT_DETAIL.Adjustment_No "
                        'qry += " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No"
                        'qry += " WHERE TSPL_RECEIPT_DETAIL.Receipt_No='" + strDocNo + "'"

                        ''richa 
                        If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                            Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_Customer_Invoice_Head where Document_No='" & clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")) & "'", trans))
                            If clsCommon.myLen(strdocumentType) > 0 AndAlso clsCommon.CompairString(strdocumentType, "C") = CompairStringResult.Equal Then
                                Dim qry1 As String = "Select Applied_Receipt as Document_No,case when isnull(TSPL_Customer_Invoice_Head.ConvRate,0)=0 then 1 else TSPL_Customer_Invoice_Head.ConvRate end  as ConvRateOld, " & _
                                " (Case When TSPL_Customer_Invoice_Head.Document_Type not in ('C') Then Receipt_Amount Else Receipt_Amount*-1 End) as Applied_Amount, 'C' as TagType, ISNULL(TSPL_Customer_Invoice_Head.Document_Type,'') AS  DocType , TSPL_Customer_Invoice_Head.Loc_Code  as Loc_Code " & _
                                " from TSPL_RECEIPT_HEADER left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_HEADER.Applied_Receipt" & _
                                 " WHERE TSPL_RECEIPT_HEADER.Receipt_No='" + strDocNo + "'"
                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)
                                Dim StartGLAccordingToBrach1 As Boolean = True
                                Dim isApplyBrachAccounting1 As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                                Dim strBankLocation1 As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                                If clsCommon.myCBool(clsCommon.CompairString(strJurEntryDefault, True)) = CompairStringResult.Equal Then
                                Else
                                    For Each dr As DataRow In dt1.Rows
                                        If clsCommon.CompairString(clsCommon.myCstr(dr("DocType")), "F") <> CompairStringResult.Equal Then
                                            clsReceiptDettail.funBalanceAmtSave(dr("Document_No"), clsCommon.myCdbl(dr("Applied_Amount")), trans, clsCommon.myCstr(dr("DocType")), dr("TagType"))
                                        End If
                                    Next
                                End If
                            End If
                        End If

                        Dim qry As String = "select TSPL_RECEIPT_DETAIL.Document_No,case when isnull(TSPL_Customer_Invoice_Head.ConvRate,0)=0 then 1 else TSPL_Customer_Invoice_Head.ConvRate end  as ConvRateOld, (Case When TSPL_RECEIPT_DETAIL.Receipt_Type not in ('C') Then Applied_Amount Else Applied_Amount*-1 End) as Applied_Amount, TagType,case when TSPL_RECEIPT_DETAIL.Receipt_Type ='F' then 'F' else ISNULL(TSPL_Customer_Invoice_Head.Document_Type,'') end  AS  DocType ,case when TSPL_RECEIPT_DETAIL.Receipt_Type ='F' then (Select right(BANKACC,3) from TSPL_RECEIPT_HEADER left outer join TSPL_BANK_MASTER on TSPL_RECEIPT_HEADER .Bank_Code =TSPL_BANK_MASTER .BANK_CODE  where Receipt_No =TSPL_RECEIPT_DETAIL.Document_No) else TSPL_Customer_Invoice_Head.Loc_Code end as Loc_Code from TSPL_RECEIPT_DETAIL "
                        qry += " LEFT OUTER JOIN TSPL_Receipt_Adjustment_Header ON TSPL_Receipt_Adjustment_Header.Adjustment_No=TSPL_RECEIPT_DETAIL.Adjustment_No "
                        qry += " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No"
                        qry += " WHERE TSPL_RECEIPT_DETAIL.Receipt_No='" + strDocNo + "'"

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        Dim StartGLAccordingToBrach As Boolean = True
                        Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                        Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                        If clsCommon.myCBool(clsCommon.CompairString(strJurEntryDefault, True)) = CompairStringResult.Equal Then
                        Else
                            For Each dr As DataRow In dt.Rows
                                If clsCommon.CompairString(clsCommon.myCstr(dr("DocType")), "F") <> CompairStringResult.Equal Then
                                    clsReceiptDettail.funBalanceAmtSave(dr("Document_No"), clsCommon.myCdbl(dr("Applied_Amount")), trans, clsCommon.myCstr(dr("DocType")), dr("TagType"))
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("DocType")), "F") = CompairStringResult.Equal Then
                                    strQ = "update TSPL_RECEIPT_HEADER set Balance_Amt =Balance_Amt -'" & clsCommon.myCdbl(dr("Applied_Amount")) & "' where Receipt_No in ('" & dr("Document_No") & "')"
                                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                                End If
                            Next
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "O") = CompairStringResult.Equal Then
                        Dim InvcNo As String = ""
                        Dim BalAmt As Decimal = 0.0
                        Dim drtotal As Decimal = clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString())
                        Dim PayAmt As Decimal = drtotal
                        strQ = " select Document_No as Document_No  ,Due_Date ,case when fifo_balance>0 then fifo_balance else   Balance_Amt end as  Balance_Amt from TSPL_Customer_Invoice_Head" & _
                               " where Balance_Amt>0 and Customer_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "' and  fifo_knockoff='N' order by TSPL_Customer_Invoice_Head.Due_Date "
                        Dim Dt1 As DataTable = New DataTable()
                        Dt1 = clsDBFuncationality.GetDataTable(strQ, trans)
                        For Each dr As DataRow In Dt1.Rows
                            InvcNo = dr.Item("Document_No").ToString()
                            BalAmt = dr.Item("Balance_Amt")
                            If drtotal > BalAmt Then
                                drtotal = drtotal - BalAmt
                                strQ = "update TSPL_Customer_Invoice_Head set fifo_balance=0.00 , fifo_knockoff='Y' where Document_No ='" + InvcNo + "' and Customer_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "'"
                                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                            ElseIf drtotal < BalAmt Then
                                drtotal = drtotal - BalAmt
                                strQ = "update TSPL_Customer_Invoice_Head set fifo_balance=" + (drtotal * -1).ToString() + "-fifo_balance , fifo_knockoff='N' where Document_No ='" + InvcNo + "' and Customer_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "'"
                                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                            End If
                            If drtotal < 0 Then
                                Exit For
                            End If
                        Next
                        If drtotal > 0 Then
                            strQ = "update TSPL_RECEIPT_HEADER set fifo_balance=" + drtotal.ToString() + " where Receipt_No ='" + strDocNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                        ''---richa agarwal added to deduct advance amount when it is used with refund
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")), "") <> CompairStringResult.Equal Then

                            clsRcptEntryHeader.UpdateBalance(clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")), clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount")), Set_Off_Date, clsCommon.myCdbl(sqlDr.Rows(0)("SetOffSkipJE")), trans)
                        End If
                    Else

                    End If

                    Dim PayType As String = ""
                    If sqlDr.Rows(0)("Receipt_Type").ToString() = "R" Then
                        PayType = "AR-PY"
                    ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "O" Then
                        PayType = "AR-OA"
                    ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "A" Then
                        PayType = "AR-DC"
                    ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "P" Then 'Or sqlDr.Rows(0)("Receipt_Type").ToString() = "A" Then
                        PayType = "AR-PI"
                    ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "U" Then
                        PayType = "AR-UC"
                    ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "F" Then
                        PayType = "AR-RF"
                    End If

                    If clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "P") = CompairStringResult.Equal Or clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "O") = CompairStringResult.Equal Or clsCommon.CompairString(sqlDr.Rows(0)("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then

                    End If

                    ''richa agarwal
                    Dim strForeignBankAcc As String = String.Empty
                    Dim strBankChargesOtherAcc As String = String.Empty
                'If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                '    clsRcptEntryHeader.UpdateBalance(clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")), clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount")), Set_Off_Date, clsCommon.myCdbl(sqlDr.Rows(0)("SetOffSkipJE")), trans)
                'End If
                If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                    ''richa UDL/20/08/19-000313,ERO/26/08/19-001003
                    Dim strAppliedRecNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipt_No  from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")) & "'", trans))
                    If clsCommon.myLen(strAppliedRecNo) > 0 Then
                        Dim strAppliedRecdate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipt_Date  from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")) & "'", trans))
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select is_End_Year_Proceed from TSPL_Fiscal_Year_Master where convert(date,'" & strAppliedRecdate & "',103)>= convert(date, Start_Date,103) and convert(date, '" & strAppliedRecdate & "',103)<=CONVERT(date, End_Date,103) ", trans)), "1") = CompairStringResult.Equal Then
                            Dim qry As String = "DISABLE TRIGGER dbo.TRG_JM_FiscaYearEndNoUpdateNoDelete ON dbo.TSPL_JOURNAL_MASTER"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            clsRcptEntryHeader.UpdateBalance(clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")), clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount")), Set_Off_Date, clsCommon.myCdbl(sqlDr.Rows(0)("SetOffSkipJE")), trans)

                            qry = "ENABLE TRIGGER dbo.TRG_JM_FiscaYearEndNoUpdateNoDelete ON dbo.TSPL_JOURNAL_MASTER"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        Else
                            clsRcptEntryHeader.UpdateBalance(clsCommon.myCstr(sqlDr.Rows(0)("Applied_Receipt")), clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount")), Set_Off_Date, clsCommon.myCdbl(sqlDr.Rows(0)("SetOffSkipJE")), trans)
                        End If
                    End If
                End If

                    Dim strQue As String = "update TSPL_receipt_header set posted = 'Y',Modify_By='" + objCommonVar.CurrentUserCode + "' where receipt_no ='" + strDocNo + "' "
                    clsDBFuncationality.ExecuteNonQuery(strQue, trans)
                    strQue = "update TSPL_RECEIPT_DETAIL set posted = 'Y' where receipt_no ='" + strDocNo + "' "
                    clsDBFuncationality.ExecuteNonQuery(strQue, trans)
                    If sqlDr.Rows(0)("UnApplied_No").ToString() <> "" Then
                        Dim DocUnAppliedNo As String = sqlDr.Rows(0)("UnApplied_No").ToString()
                        clsRcptEntryHeader.funRcptPostUnApplied(DocUnAppliedNo, trans)
                    End If
                End If
                '-------------This Code Creates Unapplied entry and post also-----------------------
                If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(sqlDr.Rows(0)("UnApplied_Balance")) > 0 Then
                        Dim obj As clsRcptEntryHeader
                        obj = clsRcptEntryHeader.GetData(strDocNo, NavigatorType.Current, trans)
                        clsRcptEntryHeader.funUnAppliedEntry(obj, trans)
                    End If
                End If
                ' post  bank transfer
                Dim obj1 As clsRcptEntryHeader
                obj1 = clsRcptEntryHeader.GetData(strDocNo, NavigatorType.Current, trans)
                If clsCommon.myLen(obj1.Transfer_No) > 0 Then
                    clsBankTrasnferNew.PostData(obj1.Transfer_No, trans)
                End If
                '---------------------------------------------------------------------
                clsBankReco.SetOutstandingEntry(strDocNo, clsCommon.myCDate(sqlDr.Rows(0)("Receipt_Date")), "Receipt", trans)
            '' Work done for Swd 09/04/2018
            'Send SMS If document type other than Applied document
            If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "A") <> CompairStringResult.Equal Then
                CreateSMSContent(obj1, trans)
            End If
            '' End by Parteek


            ''richa 
            Dim RefundknockoffwithCreditNote As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RefundknockoffwithCreditNote, clsFixedParameterCode.RefundknockoffwithCreditNote, trans)) = 1, True, False)
            If clsCommon.CompairString(clsCommon.myCstr(sqlDr.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal AndAlso RefundknockoffwithCreditNote = True Then
                CreateApplyDocumentWithCreditNoteAndRefund(obj1, trans)
            End If
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, strDocNo, "TSPL_RECEIPT_HEADER", "Receipt_No", "TSPL_RECEIPT_DETAIL", "Receipt_No", "TSPL_RECEIPT_DETAIL_GST", "Receipt_No", "TSPL_RECEIPT_DETAIL_Refund", "Receipt_No", "", "", "", "", "", "", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function CreateApplyDocumentWithCreditNoteAndRefund(ByVal objReceipt As clsRcptEntryHeader, ByVal trans As SqlTransaction)
        Dim obj As New clsRcptEntryHeader()
        Try
            If (objReceipt.ArrTrRefund IsNot Nothing AndAlso objReceipt.ArrTrRefund.Count > 0) Then
                obj.ArrTrRefund = New List(Of clsReceiptDetail_Refund)

                For Each objDispatchDetail As clsReceiptDetail_Refund In objReceipt.ArrTrRefund
                    obj = New clsRcptEntryHeader()
                    obj.memorndmamt = "0"
                    obj.Entry_Desc = "Against auto apply document for document No. " + objDispatchDetail.Document_No
                    obj.Receipt_Date = clsCommon.myCDate(objReceipt.Receipt_Date)
                    obj.Receipt_Post_Date = clsCommon.myCDate(objReceipt.Receipt_Post_Date)
                    obj.Bank_Code = clsCommon.myCstr(objReceipt.Bank_Code)
                    obj.Payment_Code = "NEFT"
                    obj.ConvRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_Customer_Invoice_Head.ConvRate,1) as ConvRate, isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.Receipt_Date), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Document_No ='" & objDispatchDetail.Document_No & "' )xx", trans))
                    obj.ConvRateOld = obj.ConvRate
                    obj.BASE_CURRENCY_CODE = objCommonVar.BaseCurrencyCode
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select CURRENCY_CODE,ApplicableFrom ,Customer_Name from TSPL_Customer_Invoice_Head where Document_No ='" & objDispatchDetail.Document_No & "'", trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                        obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                        If clsCommon.myLen(dt.Rows(0)("ApplicableFrom")) > 0 Then
                            obj.ApplicableFrom = dt.Rows(0)("ApplicableFrom")
                        Else
                            obj.ApplicableFrom = Nothing
                        End If
                    End If
                    obj.Receipt_Type = "A"

                    obj.Cust_Code = clsCommon.myCstr(objReceipt.Cust_Code)
                    Dim OutstandingAmt As Decimal = 0
                    obj.Receipt_Amount = clsCommon.myCdbl(objDispatchDetail.Applied_Amount)
                    obj.UnApply_Amt = obj.Receipt_Amount
                    obj.Balance_Amt = obj.Receipt_Amount
                    obj.IsSalesmanType = "N"
                    obj.SecurityDeposit = "N"
                    obj.CFormRecd = "N"
                    obj.CHECK_PRINT = 0
                    obj.Applied_Receipt = clsCommon.myCstr(objDispatchDetail.Document_No)
                    obj.IsAutoApplyDoc_Refund = 1
                    obj.IsApplyDocAuto = 0
                    obj.Set_Off_Date = clsCommon.GETSERVERDATE(trans)


                    obj.ArrTr = New List(Of clsReceiptDettail)
                    '============================Detail Section==============================
                    Dim objTr As New clsReceiptDettail()
                    objTr.Apply = clsCommon.myCstr(objDispatchDetail.Apply)
                    objTr.Receipt_Type = "F"
                    objTr.TagType = "C"
                    objTr.Document_No = objReceipt.Receipt_No
                    objTr.Document_Date = clsCommon.GetPrintDate(objReceipt.Receipt_Date, "yyyy-MM-dd")
                    objTr.Original_Amt = clsCommon.myCdbl(objDispatchDetail.Original_Amt)
                    objTr.Pending_Balance = clsCommon.myCdbl(objDispatchDetail.Pending_Balance)
                    objTr.Applied_Amount = clsCommon.myCdbl(objDispatchDetail.Applied_Amount)
                    objTr.Adjustment_No = clsCommon.myCstr(objDispatchDetail.Adjustment_No)
                    objTr.Adjustment_Cost = clsCommon.myCdbl(objDispatchDetail.Adjustment_Cost)
                    objTr.ConvRateOld = clsCommon.myCdbl(objDispatchDetail.ConvRateOld)
                    objTr.Child_Cust_Code = clsCommon.myCstr(objDispatchDetail.Child_Cust_Code)
                    obj.ArrTr.Add(objTr)

                    '==================Detail Section Ends Here=======================


                    obj.RECEIVED_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(objReceipt.Receipt_Amount * objReceipt.ConvRate)


                    If clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(obj.CURRENCY_CODE, obj.BASE_CURRENCY_CODE) <> CompairStringResult.Equal Then
                            Dim dt1 As DataTable
                            dt1 = clsRcptEntryHeader.GetExchangeDetailDt(objReceipt.Cust_Code, trans)
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                obj.EXCHANGE_GAIN_ACCOUNT = clsCommon.myCstr(dt1.Rows(0).Item("EXCHANGE_GAIN_ACCOUNT"))
                                obj.EXCHANGE_LOSS_ACCOUNT = clsCommon.myCstr(dt1.Rows(0).Item("EXCHANGE_LOSS_ACCOUNT"))
                            Else
                                obj.EXCHANGE_GAIN_ACCOUNT = Nothing
                                obj.EXCHANGE_LOSS_ACCOUNT = Nothing
                            End If
                            Dim dtLastRate As DataTable
                            '' gather conv rate and amount of transaction to calculate exchange loss and gain
                            Dim strInvoiceNo As String = String.Empty
                            Dim lossorgainamount As Double = 0
                            Dim Totallossorgainamount As Double = 0

                            Dim InvoiceType As String = ""

                            strInvoiceNo = clsCommon.myCstr(objDispatchDetail.Document_No)
                            InvoiceType = "Credit Note"
                            dtLastRate = clsRcptEntryHeader.GetExchangeRateAmount(strInvoiceNo, obj.Receipt_Date, trans)
                            If clsCommon.CompairString(InvoiceType, "Credit Note") = CompairStringResult.Equal Then
                                lossorgainamount = clsCommon.myCdbl(objDispatchDetail.Applied_Amount) * dtLastRate.Rows(0).Item("ConvRate") * -1
                            Else
                                lossorgainamount = clsCommon.myCdbl(objDispatchDetail.Applied_Amount) * dtLastRate.Rows(0).Item("ConvRate")
                            End If

                            Totallossorgainamount = Totallossorgainamount + lossorgainamount

                            Dim diff As Double = 0.0
                            If Totallossorgainamount <> 0 Then
                                diff = obj.RECEIVED_AMOUNT_BASE_CURRENCY - Totallossorgainamount
                            End If

                            If diff = 0 Then
                                obj.EXCHANGE_LOSS_AMT = 0
                                obj.EXCHANGE_GAIN_AMT = 0
                            ElseIf diff < 0 Then
                                If clsCommon.myLen(obj.EXCHANGE_LOSS_ACCOUNT) = 0 Then
                                    Throw New Exception("Exchange Loss Account not defined.")
                                End If
                                obj.EXCHANGE_LOSS_AMT = -diff
                                obj.EXCHANGE_GAIN_AMT = 0
                            Else
                                If clsCommon.myLen(obj.EXCHANGE_GAIN_ACCOUNT) = 0 Then
                                    Throw New Exception("Exchange Gain Account not defined.")
                                End If
                                obj.EXCHANGE_LOSS_AMT = 0
                                obj.EXCHANGE_GAIN_AMT = diff
                            End If
                        End If
                    Else

                        obj.EXCHANGE_LOSS_AMT = 0
                        obj.EXCHANGE_GAIN_AMT = 0
                        obj.EXCHANGE_GAIN_ACCOUNT = Nothing
                        obj.EXCHANGE_LOSS_ACCOUNT = Nothing
                    End If

                    obj.SaveData(obj, True, trans)
                    clsRcptEntryHeader.funRcptPost(obj.Receipt_No, trans)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Shared Sub CreateSMSContent(ByVal obj As clsRcptEntryHeader, ByVal trans As SqlTransaction)

        Dim Form_ID As String = clsUserMgtCode.ReceiptEntry
        Dim dtCustomerOutstanding As DataTable = Nothing
        Dim DO_Date As Date
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", trans)
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
            Dim qry As String = "select distinct tspl_customer_master.Customer_Name,substring (tspl_customer_master.Phone1 ,6,10) as MobileNo,tspl_customer_master.Email from tspl_customer_master"
            qry += " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Cust_Code=tspl_customer_master.Cust_Code"
            qry += " where 2=2 and len(replace( isnull(substring(tspl_customer_master.Phone1,6,10),''),'_',''))>0 and TSPL_RECEIPT_HEADER.Receipt_Type='O' and tspl_customer_master.Cust_Code='" + obj.Cust_Code + "' "

            Dim dtParty As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtParty IsNot Nothing AndAlso dtParty.Rows.Count > 0 Then
                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    Dim objSMSH As New clsSMSHead()
                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Code, obj.Cust_Code)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Name, obj.Customer_Name)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.Receipt_No)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Receipt_Date, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Type, obj.Receipt_Type)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.Receipt_Amount))

                    DO_Date = clsCommon.GetPrintDate(obj.Receipt_Date, "dd/MMM/yyyy")

                    '' Outstanding Amount
                    dtCustomerOutstanding = getCustomerOutstandingOfAmt_Can_Crate("'" & obj.Cust_Code & "'", clsCommon.GetPrintDate(clsCommon.myCDate(DO_Date).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(DO_Date), "dd/MMM/yyyy"), trans)
                    ''end
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.OutStandingAmt, clsCommon.myFormat(dtCustomerOutstanding.Rows(0)("BalAmt")))

                    objSMSH.arrMobilNo = New List(Of String)()
                   
                        objSMSH.arrMobilNo.Add(clsCommon.myCstr(dtParty.Rows(0)("MobileNo")))
                        objSMSH.SaveData(Form_ID, objSMSH, trans)
                        objSMSH = Nothing
                End If
            End If
        End If

    End Sub
    Public Shared Function getCustomerOutstandingOfAmt_Can_Crate(ByVal strCustomer As String, ByVal strfromdate As String, ByVal strtodate As String, ByVal Trans As SqlTransaction) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim BaseQryForCustomer As String = Nothing
            Dim BaseQryForCustomerforOpening As String = Nothing
            Dim BaseQry As String = Nothing
            Dim ConvRate As String = "ConvRate"

            Dim BaseqryfromDate_Amount As String = Nothing
            Dim BaseqryToDate_Amount As String = Nothing

            Dim BaseqryfromDate_Crate As String = Nothing
            Dim BaseqryToDate_Crate As String = Nothing
            '' for Customer outstanding
            BaseqryfromDate_Amount = getCustomerOutstandingAmtWithOPeningAndClosing(strCustomer, strfromdate, strfromdate, ConvRate, Trans)
            BaseqryToDate_Amount = getCustomerOutstandingAmtWithOPeningAndClosing(strCustomer, strtodate, strtodate, ConvRate, Trans)
            ''for crate outstanding
            BaseqryfromDate_Crate = getCustomerOutstandingCrateWithOPeningAndClosing(strCustomer, strfromdate, strfromdate, ConvRate)
            BaseqryToDate_Crate = getCustomerOutstandingCrateWithOPeningAndClosing(strCustomer, strtodate, strtodate, ConvRate)

            BaseQry = "  Select convert(varchar,FinalGroup.DocDate,103) as Date ,FinalGroup.acode as Cust_code ,max(FinalGroup.AName) as Name,sum(FinalGroup.OpngBal) as OpngBal,sum(FinalGroup.DrAmt) as DrAmt,sum(FinalGroup.CrAmt) as CrAmt,sum(FinalGroup.BalAmt) as BalAmt,sum(FinalGroup.CrateOpng) as CrateOpng,sum(FinalGroup.CrateReceived) as CrateReceived,sum(FinalGroup.CrateIssue) as CrateIssue,sum(FinalGroup.CrateClosing) as CrateClosing,sum(FinalGroup.OpenCanQty) as OpenCanQty ,sum(FinalGroup.CanQtyRecd) as CanQtyRecd  ,sum(FinalGroup.CanOutQty) as CanOutQty ,sum(FinalGroup.CanAdjQty) as CanAdjQty ,sum(FinalGroup.CanQtyClosing) as CanQtyClosing  from  ( " & Environment.NewLine &
            " Select  DocDate, ACode, AName, OpngBal, DrAmt, CrAmt, BalAmt,0 as CrateOpng,0 as CrateReceived,0 as CrateIssue,0 as CrateClosing,0 as OpenCanQty ,0 as CanQtyRecd  ,0 as CanOutQty ,0 as CanAdjQty ,0 as CanQtyClosing  from (" & BaseqryfromDate_Amount & ") Z " & Environment.NewLine &
            " Union All " & Environment.NewLine &
            " Select  DocDate, ACode, AName, OpngBal, DrAmt, CrAmt, BalAmt,0 as CrateOpng,0 as CrateReceived,0 as CrateIssue,0 as CrateClosing,0 as OpenCanQty ,0 as CanQtyRecd  ,0 as CanOutQty ,0 as CanAdjQty ,0 as CanQtyClosing  from (" & BaseqryToDate_Amount & ") X " & Environment.NewLine &
            " Union All " & Environment.NewLine &
            " Select Doc_Date,Customer_Code,Customer_Name,0 as OpngBal, 0 as DrAmt, 0 as  CrAmt, 0 as  BalAmt,  OpencrateQty, CrateQtyRecd ,CrateOutQty, CrateQtyClosing ,OpenCanQty , CanQtyRecd  ,CanOutQty , CanAdjQty ,CanQtyClosing from (" & BaseqryfromDate_Crate & ") Y " & Environment.NewLine &
            " Union All " & Environment.NewLine &
            " Select Doc_Date,Customer_Code,Customer_Name,0 as OpngBal, 0 as DrAmt, 0 as  CrAmt, 0 as  BalAmt,  OpencrateQty, CrateQtyRecd ,CrateOutQty, CrateQtyClosing ,OpenCanQty , CanQtyRecd  ,CanOutQty , CanAdjQty ,CanQtyClosing from (" & BaseqryToDate_Crate & ") S " & Environment.NewLine &
            " Union All " & Environment.NewLine &
            " select '" + clsCommon.GetPrintDate(strfromdate, "dd/MM/yyyy") + "' as Date,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name  as Name ,0 as OpngBal, 0 as DrAmt, 0 as  CrAmt, 0 as  BalAmt,0 as CrateOpng,0 as CrateReceived,0 as CrateIssue,0 as CrateClosing,0 as OpenCanQty ,0 as CanQtyRecd  ,0 as CanOutQty ,0 as CanAdjQty ,0 as CanQtyClosing  from TSPL_CUSTOMER_MASTER where  TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ") " & Environment.NewLine &
            " Union All " & Environment.NewLine &
            " select '" + clsCommon.GetPrintDate(strtodate, "dd/MM/yyyy") + "'   as Date ,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name  as Name ,0 as OpngBal, 0 as DrAmt, 0 as  CrAmt, 0 as  BalAmt,0 as CrateOpng,0 as CrateReceived,0 as CrateIssue,0 as CrateClosing,0 as OpenCanQty ,0 as CanQtyRecd  ,0 as CanOutQty ,0 as CanAdjQty ,0 as CanQtyClosing   from TSPL_CUSTOMER_MASTER where  TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ")" & Environment.NewLine &
                " ) FinalGroup group by FinalGroup.DocDate ,FinalGroup.acode order by convert(date,FinalGroup.DocDate,103) desc"

            Return clsDBFuncationality.GetDataTable(BaseQry, Trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function getCustomerOutstandingAmtWithOPeningAndClosing(ByVal strCustomer As String, ByVal strfromdate As String, ByVal strtodate As String, ByVal ConvRate As String, ByVal Trans As SqlTransaction) As String
        Try
            Dim BaseQryForCustomer As String = Nothing
            Dim BaseQryForCustomerforOpening As String = Nothing
            Dim BaseQry As String = Nothing

            BaseQryForCustomer = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, ConvRate, strCustomer, False, strfromdate, strtodate, False, False, False, Trans)
            BaseQryForCustomerforOpening = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, ConvRate, strCustomer, True, strfromdate, strtodate, False, False, False, Trans)

            BaseQry = " Select '" + clsCommon.GetPrintDate(strtodate, "dd/MM/yyyy") + "' as DocDate, ACode,  MAX(AName) as AName, SUM(convert(decimal(18,2),OpngBal)) as OpngBal, SUM(convert(decimal(18,2),DrAmt)) as DrAmt, SUM(convert(decimal(18,2),CrAmt)) as CrAmt,  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt  From (" + Environment.NewLine & _
            "  Select max(DocDate) as DocDate, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName,  '' as CurrencyCode, null as ConvRate, SUM(DrAmt*ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt from  ( " + BaseQryForCustomerforOpening + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strfromdate + "' AND LEN(ACode)>0 AND TSPL_CUSTOMER_MASTER.Status='N'  GROUP BY ACode" + Environment.NewLine & _
            Environment.NewLine + " UNION ALL" + Environment.NewLine & _
            " Select  max(DocDate) as DocDate, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName,  MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(convert(decimal(18,2),DrAmt*  Final.ConvRate)) as DrAmt, " & Environment.NewLine & _
            " SUM(convert(decimal(18,2),CrAmt)) as CrAmt FROM ( " + BaseQryForCustomer + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
            " Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
            "where  CONVERT(DATE,final.DocDate,103) >= '" + strfromdate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strtodate + "' AND LEN(ACode)>0 AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode ) XXX GROUP BY ACode  " + Environment.NewLine

            Return BaseQry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getCustomerOutstandingCrateWithOPeningAndClosing(ByVal strCustomer As String, ByVal strfromdate As String, ByVal strtodate As String, ByVal ConvRate As String) As String
        Try
            Dim BaseQry As String = Nothing

            BaseQry = " ( Select convert(varchar,Doc_Date,103) as Doc_Date,Customer_Code,Customer_Name, OpencrateQty, CrateQtyRecd ,CrateOutQty, CrateAdjQty ,SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code) as CrateQtyClosing,OpenCanQty , CanQtyRecd  ,CanOutQty , CanAdjQty ,SUM(CanQtyClosing ) OVER (Partition BY Customer_Code ORDER BY Customer_Code) as CanQtyClosing from ( " & Environment.NewLine & _
            " select  pp.Doc_Date  as Doc_Date,TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Route_Desc,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " & Environment.NewLine & _
            " select  max(convert(date,Doc_Date,103))  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code,xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing, (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing, (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing  , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing  from (select  max(convert(date,'" + strfromdate + "' ,103)) as Doc_Date,max(Opening.Vehicle_Code) as Vehicle_Code , Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from  ( select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'  union all  select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty , 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  union all select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty, 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + strfromdate + "' ,103)) group by Customer_Code " & Environment.NewLine & _
            " UNION All " & Environment.NewLine & _
            " select Document_Date,Vehicle_Code,Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec, " & Environment.NewLine & _
            " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment " & Environment.NewLine & _
            " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & Environment.NewLine & _
            " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " & Environment.NewLine & _
            "where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I') " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & Environment.NewLine & _
            " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & Environment.NewLine & _
            " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, 0 as CrateQtyRecd, 0 JaaliQtyRecd ,  0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS')  ) as Closing " & Environment.NewLine & _
            " WHERE convert(date,Document_Date ,103)>= convert(date,'" + strfromdate + "' ,103) AND convert(date,Document_Date,103)<=convert(date,'" + strtodate + "' ,103) " & Environment.NewLine & _
            " ) as xx where 2=2   and xx.Customer_Code  in (" & strCustomer & ") " & Environment.NewLine & _
            " GROUP BY Customer_Code " & Environment.NewLine & _
            " ) as pp   left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code " & Environment.NewLine & _
            " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2  ) YYY )"


            Return BaseQry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
 
    Public Shared Function CheckNegativeBankBalance(ByVal SqlDr As DataTable, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        '' Validation check: by Panch Raj against ticket No:BM00000008437
        If clsCommon.CompairString(SqlDr.Rows(0)("Receipt_Type").ToString, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(SqlDr.Rows(0)("Receipt_Type").ToString, "S") = CompairStringResult.Equal Then
            Dim Bank_Type_Check As String = "0"
            Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, trans)
            Dim Bank_Type As String = clsBankMaster.GetBankType(SqlDr.Rows(0)("Bank_Code").ToString, trans)
            Dim Bank_Balance As Decimal = 0
            Dim Refund_Amount As Decimal = clsCommon.myCdbl(SqlDr.Rows(0)("RECEIVED_AMOUNT_BASE_CURRENCY"))
            Dim Bank_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" & SqlDr.Rows(0)("Bank_Code").ToString & "')", trans))

            If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                '' allow for all
            ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                    ''richa agarwal 03 Aug,2016
                    ' Bank_Balance = clsPaymentHeader.GetBankBalance(SqlDr.Rows(0)("Receipt_Date"), SqlDr.Rows(0)("Bank_Code").ToString, Bank_Location, trans)
                    Bank_Balance = clsPaymentHeader.GetBankBalance(SqlDr.Rows(0)("Receipt_No"), SqlDr.Rows(0)("Receipt_Date"), SqlDr.Rows(0)("Bank_Code").ToString, Bank_Location, trans)
                    ''------------
                    If Bank_Balance < Refund_Amount Then
                        Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                    ''richa agarwal 03 Aug,2016
                    ' Bank_Balance = clsPaymentHeader.GetBankBalance(SqlDr.Rows(0)("Receipt_Date"), SqlDr.Rows(0)("Bank_Code").ToString, Bank_Location, trans)
                    Bank_Balance = clsPaymentHeader.GetBankBalance(SqlDr.Rows(0)("Receipt_No"), SqlDr.Rows(0)("Receipt_Date"), SqlDr.Rows(0)("Bank_Code").ToString, Bank_Location, trans)
                    If Bank_Balance < Refund_Amount Then
                        Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                ''richa agarwal 03 Aug,2016
                ' Bank_Balance = clsPaymentHeader.GetBankBalance(SqlDr.Rows(0)("Receipt_Date"), SqlDr.Rows(0)("Bank_Code").ToString, Bank_Location, trans)
                Bank_Balance = clsPaymentHeader.GetBankBalance(SqlDr.Rows(0)("Receipt_No"), SqlDr.Rows(0)("Receipt_Date"), SqlDr.Rows(0)("Bank_Code").ToString, Bank_Location, trans)
                If Bank_Balance < Refund_Amount Then
                    Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                End If
            End If
        End If
        Return True
    End Function
    ''richa TEC/02/11/18-000363 on 12 nov,2018 
    Public Shared Function CreateJournalEntryForOpening(ByVal isForUnpostedTransaction As Boolean, ByVal dtReceipt As DataTable, ByVal strDocNo As String, ByVal LocSegmentCode As String, ByVal trans As SqlTransaction, ByVal strVoucherNoForRecreatedOnly As String, Optional ByVal Module_Code As String = "MReceivable", Optional strInvFromLoc As String = Nothing) As Boolean
        Try
            Dim qry1 As String = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + strDocNo + "' "
            Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))


            If strVoucherNoForRecreatedOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreatedOnly) > 0 Then
                strVoucherNo = strVoucherNoForRecreatedOnly
            End If

            Dim strQ As String = Nothing
            Dim IsMultiCurrency As Boolean = clsModuleCurrencyMapping.CheckMultiCurrency(Module_Code, trans)
            Dim CURRENCY_CODE As String = dtReceipt.Rows(0)("CURRENCY_CODE").ToString
            Dim APPLICABLEFROM As Date? = Nothing
            If IsDBNull(dtReceipt.Rows(0)("APPLICABLEFROM")) = True Then
                APPLICABLEFROM = Nothing
            Else
                APPLICABLEFROM = dtReceipt.Rows(0)("APPLICABLEFROM")
            End If
            Dim EXCHANGE_LOSS_AMT As Double = clsCommon.myCdbl(dtReceipt.Rows(0)("EXCHANGE_LOSS_AMT"))
            Dim EXCHANGE_GAIN_AMT As Double = clsCommon.myCdbl(dtReceipt.Rows(0)("EXCHANGE_GAIN_AMT"))
            Dim EXCHANGE_GAIN_ACCOUNT As String = clsCommon.myCstr(dtReceipt.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))
            Dim EXCHANGE_LOSS_ACCOUNT As String = clsCommon.myCstr(dtReceipt.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
            Dim ConvRateOld As Double
            If objCommonVar.IsDemoERP Then
                ConvRateOld = IIf(clsCommon.myCdbl(dtReceipt.Rows(0)("ConvRateOld")) = 0, 1, clsCommon.myCdbl(dtReceipt.Rows(0)("ConvRateOld")))
            Else
                ConvRateOld = 1
            End If

            Dim ConvRate As Double = IIf(clsCommon.myCdbl(dtReceipt.Rows(0)("ConvRate")) = 0, 1, clsCommon.myCdbl(dtReceipt.Rows(0)("ConvRate")))

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", CURRENCY_CODE, True)
            If clsCommon.myLen(APPLICABLEFROM) > 0 Then
                clsCommon.AddColumnsForChange(coll, "APPLICABLEFROM", clsCommon.GetPrintDate(APPLICABLEFROM, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "APPLICABLEFROM", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "ConvRate", ConvRate)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", ConvRateOld)
            Dim UseSubAcc As String

            '' (Remarks : if setting "AllowToUseSubAccount" is ON And Bank_Type should be "B" Then BankAccount is Sub_Account Else previous)
            Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")) + "'", trans))
            '' (Remarks : if setting "AllowToUseSubAccount" is ON Then BankAccount is Sub_Account Else previous)
            'If clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
            Dim strQ1 As String
            Dim strBankAcc As String = ""
            Dim strBankOpeningClearingAcc As String = ""
            Dim strCustomerSecurityOpeningClearingAcc As String = ""
            Dim strSourceTypeDesc As String = ""
            Dim dblAmount As Decimal = clsCommon.myCdbl(dtReceipt.Rows(0)("Receipt_Amount"))
            Dim strRemarks As String = ""
            UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))

            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                strQ1 = "select ISNULL(Sub_Account,'')  BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")) + "'"
                strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
            Else
                strQ1 = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")) + "'"
                strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
            End If
            If clsCommon.myLen(strBankAcc) <= 0 Then
                If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")))
                Else
                    Throw New Exception("Please enter bank account for bank " + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")))
                End If
            End If

            Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)

            Dim arrlist As New ArrayList()
            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "M") = CompairStringResult.Equal Then
                strSourceTypeDesc = "AR Receipt Miscellaneous"
                ''credit ac
                strQ1 = "select isnull(Bank_Opening_Clearing_Account,'') as Bank_Opening_Clearing_Account  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")) + "'"
                strBankOpeningClearingAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                If clsCommon.myLen(strBankOpeningClearingAcc) <= 0 Then
                    Throw New Exception("Please enter Opening Clearing account for bank " + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")))
                End If

                strBankOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankOpeningClearingAcc, strBankLocation, True, trans)

                '' credit a/c
                Dim OpeningClearingAcc() As String = {strBankOpeningClearingAcc, dblAmount * -1, "", "", "", "", "", "", ""}
                arrlist.Add(OpeningClearingAcc)

                '' debit A/c
                Dim BankAcc() As String = {strBankAcc, dblAmount, "", "", "", "", "", "", "B"}
                arrlist.Add(BankAcc)
                strRemarks = "Bank Opening"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "P") = CompairStringResult.Equal Then
                strSourceTypeDesc = "AR Receipt Advance Security"
                Dim strColumn As String = ""
                '' debit a/c
                If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("SecurityDeposit")), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(dtReceipt.Rows(0)("SecurityDeposit"))) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("SecurityDepositType")), "S") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("SecurityDepositType")), "C") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.CREATE_SECURITY_ACCOUNT"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("SecurityDepositType")), "B") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.BANK_GUARANTEE"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("SecurityDepositType")), "O") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT1"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("SecurityDepositType")), "R") = CompairStringResult.Equal Then
                        strColumn = "TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT2"
                    End If
                End If
                strQ1 = "SELECT " & strColumn & " FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "' "

                strBankOpeningClearingAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                strBankOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankOpeningClearingAcc, strBankLocation, True, trans)

                ''credit ac
                strQ1 = "SELECT isnull(TSPL_CUSTOMER_ACCOUNT_SET.Customer_Security_Opening_Clearing_AC,'') FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "' "
                strCustomerSecurityOpeningClearingAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                If clsCommon.myLen(strCustomerSecurityOpeningClearingAcc) <= 0 Then
                    Throw New Exception("Please enter Customer Security Opening Clearing account for Customer " + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_code")))
                End If
                strCustomerSecurityOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustomerSecurityOpeningClearingAcc, strBankLocation, True, trans)

                '' credit a/c change as per ranjana mam account changed means cr account exchange with dr account
                Dim SecurityAcc() As String = {strBankOpeningClearingAcc, dblAmount * -1, "", "", "", "", "", "", "B"}
                arrlist.Add(SecurityAcc)

                '' debit A/c
                Dim SecurityOpeningClearing() As String = {strCustomerSecurityOpeningClearingAcc, dblAmount, "", "", "", "", "", "", "B"}
                arrlist.Add(SecurityOpeningClearing)
                strRemarks = "Customer Security"
            End If
            ''richa TEC/28/11/18-000376 28 Nov,2018
            transportSql.FunGrnlEntryWithTrans(True, 0, "", "N", LocSegmentCode, True, isForUnpostedTransaction, strVoucherNo, trans, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Post_Date")), strRemarks, "GL-JE", strSourceTypeDesc, strDocNo, "", IIf(clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "P") = CompairStringResult.Equal, "C", "O"), dtReceipt.Rows(0)("Cust_Code").ToString(), dtReceipt.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , dtReceipt.Rows(0)("Reference").ToString(), dtReceipt.Rows(0)("Narration").ToString(), coll)
            'transportSql.FunGrnlEntryWithTrans(LocSegmentCode, True, isForUnpostedTransaction, strVoucherNo, trans, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Post_Date")), strRemarks, "GL-JE", strSourceTypeDesc, strDocNo, "", IIf(clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "P") = CompairStringResult.Equal, "C", "O"), dtReceipt.Rows(0)("Cust_Code").ToString(), dtReceipt.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , dtReceipt.Rows(0)("Reference").ToString(), dtReceipt.Rows(0)("Narration").ToString(), coll)
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function
    ''-------------------------------------------------
    Public Shared Function CreateJournalEntry(ByVal isForUnpostedTransaction As Boolean, ByVal dtReceipt As DataTable, ByVal strDocNo As String, ByVal LocSegmentCode As String, ByVal trans As SqlTransaction, ByVal strVoucherNoForRecreatedOnly As String, Optional ByVal Module_Code As String = "MReceivable", Optional strInvFromLoc As String = Nothing) As Boolean
        Try
            Dim qry1 As String = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + strDocNo + "' "
            Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))


            If strVoucherNoForRecreatedOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreatedOnly) > 0 Then
                strVoucherNo = strVoucherNoForRecreatedOnly
            End If

            ''richa 7 Feb,2020
            Dim objJE As New clsJEExtraColumns
            If clsCommon.myLen(clsCommon.myCstr(dtReceipt.Rows(0)("TapalNo"))) > 0 Or clsCommon.myLen(clsCommon.myCstr(dtReceipt.Rows(0)("DateAndTime"))) > 0 Then
                objJE.TapalNo = clsCommon.myCstr(dtReceipt.Rows(0)("TapalNo"))
                If clsCommon.myLen(clsCommon.myCstr(dtReceipt.Rows(0)("DateAndTime"))) > 0 Then
                    objJE.DateAndTime = clsCommon.myCstr(dtReceipt.Rows(0)("DateAndTime"))
                End If
            End If

            Dim strQ As String = Nothing
            Dim IsMultiCurrency As Boolean = clsModuleCurrencyMapping.CheckMultiCurrency(Module_Code, trans)
            Dim CURRENCY_CODE As String = dtReceipt.Rows(0)("CURRENCY_CODE").ToString
            Dim APPLICABLEFROM As Date? = Nothing
            If IsDBNull(dtReceipt.Rows(0)("APPLICABLEFROM")) = True Then
                APPLICABLEFROM = Nothing
            Else
                APPLICABLEFROM = dtReceipt.Rows(0)("APPLICABLEFROM")
            End If
            Dim EXCHANGE_LOSS_AMT As Double = clsCommon.myCdbl(dtReceipt.Rows(0)("EXCHANGE_LOSS_AMT"))
            Dim EXCHANGE_GAIN_AMT As Double = clsCommon.myCdbl(dtReceipt.Rows(0)("EXCHANGE_GAIN_AMT"))
            Dim EXCHANGE_GAIN_ACCOUNT As String = clsCommon.myCstr(dtReceipt.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))
            Dim EXCHANGE_LOSS_ACCOUNT As String = clsCommon.myCstr(dtReceipt.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
            Dim ConvRateOld As Double
            If objCommonVar.IsDemoERP Then
                ConvRateOld = IIf(clsCommon.myCdbl(dtReceipt.Rows(0)("ConvRateOld")) = 0, 1, clsCommon.myCdbl(dtReceipt.Rows(0)("ConvRateOld")))
            Else
                ConvRateOld = 1
            End If

            Dim ConvRate As Double = IIf(clsCommon.myCdbl(dtReceipt.Rows(0)("ConvRate")) = 0, 1, clsCommon.myCdbl(dtReceipt.Rows(0)("ConvRate")))

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", CURRENCY_CODE, True)
            If clsCommon.myLen(APPLICABLEFROM) > 0 Then
                clsCommon.AddColumnsForChange(coll, "APPLICABLEFROM", clsCommon.GetPrintDate(APPLICABLEFROM, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "APPLICABLEFROM", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "ConvRate", ConvRate)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", ConvRateOld)
            Dim UseSubAcc As String

            '' Anubhooti 01-Jan-2015 (Remarks : if setting "AllowToUseSubAccount" is ON And Bank_Type should be "B" Then BankAccount is Sub_Account Else previous)
            Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")) + "'", trans))
            '' Anubhooti 03-Sep-2014 BM00000003437(Remarks : if setting "AllowToUseSubAccount" is ON Then BankAccount is Sub_Account Else previous)
            Dim strQ1 As String
            Dim strBankAcc As String = ""

            UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                strQ1 = "select ISNULL(Sub_Account,'')  BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")) + "'"
                strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
            Else
                strQ1 = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")) + "'"
                strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
            End If
            ''
            If clsCommon.myLen(strBankAcc) <= 0 Then
                If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")))
                Else
                    Throw New Exception("Please enter bank account for bank " + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")))
                End If
            End If
            ''richa
            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                Dim strsecurityType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select SecurityDeposit from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(dtReceipt.Rows(0)("Applied_Receipt")) & "' ", trans))
                If clsCommon.CompairString(strsecurityType, "Y") = CompairStringResult.Equal Then
                    strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Receipt_Type from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(dtReceipt.Rows(0)("Applied_Receipt")) & "' ", trans)), "O") = CompairStringResult.Equal Then
                        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_Customer_Invoice_Head where Document_No ='" & clsCommon.myCstr(dtReceipt.Rows(0)("Applied_Receipt")) & "' ", trans)), "C") = CompairStringResult.Equal Then
                        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                    Else
                        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                    End If
                End If
                strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                strBankAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankAcc, tBankLocation, True, trans)
            End If

            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "M") = CompairStringResult.Equal Then
                Dim arrlist As New ArrayList()
                Dim Receipt_Line_No As Integer = 0
                Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                If dtReceipt IsNot Nothing AndAlso dtReceipt.Rows.Count > 0 Then
                    Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                    For Each dr As DataRow In dtReceipt.Rows
                        Dim strAccountLocation As String = clsCommon.myCstr(dr("Account_Code")).Substring(clsCommon.myLen(clsCommon.myCstr(dr("Account_Code"))) - 3, 3)
                        Dim dblAmount As Decimal = clsCommon.myCdbl(dr("Applied_Amount"))
                        If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strBankLocation, strAccountLocation) = CompairStringResult.Equal) Then
                            Dim Acc4() As String = {strBankAcc, dblAmount, "", "", "", "", "", "", "B"}
                            arrlist.Add(Acc4)

                            Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strAccountLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strAccountLocation)
                            End If
                            Dim RcvblAcc = New String() {strTemp, -1 * dblAmount}
                            arrlist.Add(RcvblAcc)

                            ''richa agarwal 16 Mar,2017
                            ' Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), -1 * dblAmount, clsCommon.myCstr(dr("Remarks"))}
                            Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), -1 * dblAmount, clsCommon.myCstr(dr("Remarks")), "", clsCommon.myCstr(dr("Hirerachy_Level_Code")), clsCommon.myCstr(dr("Cost_Center_Fin_Code"))}
                            arrlist.Add(acc3)

                            strTemp = ClsBranchAccountMapping.GetBranchAccount(strAccountLocation, strBankLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + strAccountLocation + " and to location " + strBankLocation)
                            End If
                            RcvblAcc = New String() {strTemp, dblAmount}
                            arrlist.Add(RcvblAcc)
                        Else
                            ''richa agarwal 16 Mar,2017
                            '  Dim RcvblAcc() As String = {clsCommon.myCstr(dr("Account_Code")), -1 * (dblAmount), clsCommon.myCstr(dr("Remarks"))}
                            Dim RcvblAcc() As String = {clsCommon.myCstr(dr("Account_Code")), -1 * (dblAmount), clsCommon.myCstr(dr("Remarks")), "", clsCommon.myCstr(dr("Hirerachy_Level_Code")), clsCommon.myCstr(dr("Cost_Center_Fin_Code"))}
                            arrlist.Add(RcvblAcc)

                            Dim BankAcc() As String = {strBankAcc, dblAmount, "", "", "", "", "", "", "B"}
                            arrlist.Add(BankAcc)
                        End If
                        Receipt_Line_No = Receipt_Line_No + 1
                    Next
                End If

                '' MULTICURRENCY
                If IsMultiCurrency Then
                    If (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT) <> 0 Then
                        Dim BankAcc() As String = {strBankAcc, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT) / ConvRate, "", "", "", "", "", "", "B"}
                        arrlist.Add(BankAcc)
                    End If
                    If EXCHANGE_LOSS_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, -EXCHANGE_LOSS_AMT / ConvRate}
                        arrlist.Add(CURR_EXCHANGE)
                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT / ConvRate}
                        arrlist.Add(CURR_EXCHANGE)
                    End If
                End If
                '' END MULTICURRENCY
                transportSql.FunGrnlEntryWithTrans(LocSegmentCode, True, isForUnpostedTransaction, strVoucherNo, trans, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Post_Date")), dtReceipt.Rows(0)("Entry_Desc").ToString(), "AR-MI", "AR Payment Received", strDocNo, "", "C", dtReceipt.Rows(0)("Cust_Code").ToString(), dtReceipt.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , dtReceipt.Rows(0)("Reference").ToString(), dtReceipt.Rows(0)("Narration").ToString(), coll, objJE)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "S") = CompairStringResult.Equal Then
                Dim arrlist As New ArrayList()
                Dim Receipt_Line_No As Integer = 0
                Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                If dtReceipt IsNot Nothing AndAlso dtReceipt.Rows.Count > 0 Then
                    Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                    For Each dr As DataRow In dtReceipt.Rows
                        Dim strAccountLocation As String = clsCommon.myCstr(dr("Account_Code")).Substring(clsCommon.myLen(clsCommon.myCstr(dr("Account_Code"))) - 3, 3)

                        Dim dblAmount As Decimal = clsCommon.myCdbl(dr("Applied_Amount"))

                        If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strBankLocation, strAccountLocation) = CompairStringResult.Equal) Then
                            Dim Acc4() As String = {strBankAcc, dblAmount * -1, "", "", "", "", "", "", "B"}
                            arrlist.Add(Acc4)

                            Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strAccountLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strAccountLocation)
                            End If
                            Dim RcvblAcc = New String() {strTemp, 1 * dblAmount}
                            arrlist.Add(RcvblAcc)

                            ''richa agarwal 16 March,2017
                            'Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), 1 * dblAmount, clsCommon.myCstr(dr("Remarks"))}
                            Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), 1 * dblAmount, clsCommon.myCstr(dr("Remarks")), "", clsCommon.myCstr(dr("Hirerachy_Level_Code")), clsCommon.myCstr(dr("Cost_Center_Fin_Code"))}
                            arrlist.Add(acc3)

                            strTemp = ClsBranchAccountMapping.GetBranchAccount(strAccountLocation, strBankLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + strAccountLocation + " and to location " + strBankLocation)
                            End If
                            RcvblAcc = New String() {strTemp, dblAmount * -1}
                            arrlist.Add(RcvblAcc)
                        Else
                            ''richa agarwal 16 March,2017
                            ' Dim RcvblAcc() As String = {clsCommon.myCstr(dr("Account_Code")), 1 * (dblAmount), clsCommon.myCstr(dr("Remarks"))}
                            Dim RcvblAcc() As String = {clsCommon.myCstr(dr("Account_Code")), 1 * (dblAmount), clsCommon.myCstr(dr("Remarks")), "", clsCommon.myCstr(dr("Hirerachy_Level_Code")), clsCommon.myCstr(dr("Cost_Center_Fin_Code"))}
                            arrlist.Add(RcvblAcc)

                            Dim BankAcc() As String = {strBankAcc, dblAmount * -1, "", "", "", "", "", "", "B"}
                            arrlist.Add(BankAcc)
                        End If
                    Next
                End If
                If IsMultiCurrency Then
                    If EXCHANGE_LOSS_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT / ConvRate}
                        arrlist.Add(CURR_EXCHANGE)
                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, EXCHANGE_GAIN_AMT / ConvRate}
                        arrlist.Add(CURR_EXCHANGE)
                    End If
                End If
                '' END MULTICURRENCY
                transportSql.FunGrnlEntryWithTrans(LocSegmentCode, True, isForUnpostedTransaction, strVoucherNo, trans, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Post_Date")), dtReceipt.Rows(0)("Entry_Desc").ToString(), "AR-MR", "AR Payment Received", strDocNo, "", "C", dtReceipt.Rows(0)("Cust_Code").ToString(), dtReceipt.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , dtReceipt.Rows(0)("Reference").ToString(), dtReceipt.Rows(0)("Narration").ToString(), coll, objJE)
            Else
                Dim strRcvblAcc As String
                Dim ArrList As ArrayList = New ArrayList()
                If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                    Dim strQuery As String = "select Isnull(Dr_Account, '') as Dr_Account  from TSPL_RECEIPT_HEADER where Receipt_No='" + strDocNo + "'"
                    strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQuery, trans))
                Else
                    Dim strQuery As String = "select Isnull(Cr_Account, '') as Cr_Account  from TSPL_RECEIPT_HEADER where Receipt_No='" + strDocNo + "'"
                    strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQuery, trans))
                    If clsCommon.myLen(strRcvblAcc) <= 0 Then
                        Throw New Exception("Please provide Advance account for customer Account Set for customer-" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")))
                    End If

                End If
                '' Anubhooti 01-Dec-2014 (Advance Account should be come acc. to amit sir)
                If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "U") = CompairStringResult.Equal Then

                    Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                    strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                    strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                    If clsCommon.myLen(strRcvblAcc) <= 0 Then
                        Throw New Exception("Please provide Advance account for customer Account Set for customer-" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")))
                    End If


                End If
                ''
                Dim RcvblAcc() As String
                Dim BankAcc() As String
                Dim bankAccountChargesAmount As Double = 0
                Dim AdjAmt As Decimal = 0
                Dim RCRowcount As Integer = 0
                If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "K") = CompairStringResult.Equal Then
                    '--------------------------This COde Updates Balance Amount of Invoice--------------------------
                    'Dim qry As String = "select  Document_No,  (case when ISNULL(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRateOld end) as ConvRateOld,  Applied_Amount, TagType ,Loc_Code from (   select TSPL_RECEIPT_DETAIL.Document_No,case when isnull(TSPL_Customer_Invoice_Head.ConvRate,0)=0 then 1 else TSPL_Customer_Invoice_Head.ConvRate end  as ConvRateOld,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and convert(date, TSPL_REVALUATION_HEAD.Document_Date,103)<= convert(date, TSPL_RECEIPT_HEADER.Receipt_Date,103)  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation, (Case When TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' Then Applied_Amount Else Applied_Amount*-1 End) as Applied_Amount, TagType ,TSPL_Customer_Invoice_Head.Loc_Code from TSPL_RECEIPT_DETAIL "
                    'qry += " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No= TSPL_RECEIPT_DETAIL.Receipt_No "
                    'qry += " LEFT OUTER JOIN TSPL_Receipt_Adjustment_Header ON TSPL_Receipt_Adjustment_Header.Adjustment_No=TSPL_RECEIPT_DETAIL.Adjustment_No "
                    'qry += " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No"
                    'qry += " WHERE TSPL_RECEIPT_DETAIL.Receipt_No='" + strDocNo + "' ) xx"


                    Dim qry As String = "select  Document_No,  (case when ISNULL(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRateOld end) as ConvRateOld,  Applied_Amount, TagType ,Loc_Code from (   select TSPL_RECEIPT_DETAIL.Document_No,case when isnull(TSPL_Customer_Invoice_Head.ConvRate,0)=0 then 1 else TSPL_Customer_Invoice_Head.ConvRate end  as ConvRateOld,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and convert(date, TSPL_REVALUATION_HEAD.Document_Date,103)<= convert(date, TSPL_RECEIPT_HEADER.Receipt_Date,103)  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation, (Case When TSPL_RECEIPT_DETAIL.Receipt_Type not in ('C') Then Applied_Amount Else Applied_Amount*-1 End) as Applied_Amount, TagType, " &
                    "  case when TSPL_RECEIPT_DETAIL.Receipt_Type ='F' then (Select right(BANKACC,3) from TSPL_RECEIPT_HEADER left outer join TSPL_BANK_MASTER on TSPL_RECEIPT_HEADER .Bank_Code =TSPL_BANK_MASTER .BANK_CODE  where Receipt_No =TSPL_RECEIPT_DETAIL.Document_No) else TSPL_Customer_Invoice_Head.Loc_Code end as Loc_Code" &
                    " from TSPL_RECEIPT_DETAIL " &
                    " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No= TSPL_RECEIPT_DETAIL.Receipt_No " &
                    " LEFT OUTER JOIN TSPL_Receipt_Adjustment_Header ON TSPL_Receipt_Adjustment_Header.Adjustment_No=TSPL_RECEIPT_DETAIL.Adjustment_No " &
                    " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No" &
                    " WHERE TSPL_RECEIPT_DETAIL.Receipt_No='" + strDocNo + "' ) xx"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim StartGLAccordingToBrach As Boolean = True
                    Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                    Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                    For Each dr As DataRow In dt.Rows

                        ''richa agarwal 18/05/2015
                        'Dim dblAmount As Double = clsCommon.myCdbl(dr("Applied_Amount")) * clsCommon.myCdbl(dr("ConvRateOld"))
                        'Dim dblAmount1 As Double = clsCommon.myCdbl(dr("Applied_Amount")) * ConvRate
                        Dim dblAmount As Double = Math.Round(clsCommon.myCdbl(dr("Applied_Amount")) * clsCommon.myCdbl(dr("ConvRateOld")), 2, MidpointRounding.AwayFromZero)
                        Dim dblAmount1 As Double = Math.Round(clsCommon.myCdbl(dr("Applied_Amount")) * ConvRate, 2, MidpointRounding.AwayFromZero)

                        Dim strDocLocation As String = clsCommon.myCstr(dr("Loc_Code"))  ''Balwinder on 24/02/2016 strDocLocation islocation location segment passes true.
                        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoRecieptPaymentMode, clsFixedParameterCode.AutoRecieptPaymentMode, trans), "Cash") = CompairStringResult.Equal Then
                            strDocLocation = strInvFromLoc
                        End If
                        ''richa agarwal
                        If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strBankLocation, strDocLocation) = CompairStringResult.Equal) AndAlso Not clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                            ''Add By Balwinder for Apply branch.
                            If StartGLAccordingToBrach Then
                                'ArrList = New ArrayList()
                                StartGLAccordingToBrach = False
                            End If
                            Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, strBankLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + strDocLocation + " and to location " + strBankLocation)
                            End If
                            ''branch accounting of customer
                            RcvblAcc = New String() {strTemp, dblAmount1}
                            ArrList.Add(RcvblAcc)

                            ''entry for customer
                            strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strDocLocation, True, trans)
                            RcvblAcc = New String() {strTemp, -1 * dblAmount}
                            ArrList.Add(RcvblAcc)

                            RcvblAcc = New String() {strBankAcc, dblAmount1, "", "", "", "", "", "", "B"}
                            ArrList.Add(RcvblAcc)
                            ''----------------------

                            strTemp = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strDocLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strDocLocation)
                            End If
                            ''branch accounting of bank
                            RcvblAcc = New String() {strTemp, -1 * dblAmount1}
                            ArrList.Add(RcvblAcc)

                            '' 01-Dec-2014 (Merge Un-Applied GL into Receipt GL Entry) commented by richa 
                            'If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
                            '    If clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance")) > 0 Then
                            '        RCRowcount += 1
                            '        Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                            '        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                            '        strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                            '        If clsCommon.myLen(strRcvblAcc) <= 0 Then
                            '            Throw New Exception("Please provide Advance account for customer Account Set for customer-" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")))
                            '        End If
                            '        If RCRowcount = 1 Then
                            '            RcvblAcc = New String() {strRcvblAcc, -1 * clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance"))}
                            '            ArrList.Add(RcvblAcc)
                            '            '
                            '            RcvblAcc = New String() {strBankAcc, clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance"))}
                            '            ArrList.Add(RcvblAcc)

                            '            ''richa agarwal 12 July,2018 TEC/03/04/18-000174 branch accounting account added for unapplied entry

                            '            strTemp = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, strBankLocation, trans)
                            '            If clsCommon.myLen(strTemp) <= 0 Then
                            '                Throw New Exception("Please set Branch account mapping with from location " + strDocLocation + " and to location " + strBankLocation)
                            '            End If
                            '            ''richa 13 Nov,2018 TEC/03/04/18-000174
                            '            'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strDocLocation, True, trans)
                            '            'RcvblAcc = New String() {strTemp, clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance"))}
                            '            'ArrList.Add(RcvblAcc)

                            '            'strTemp = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strDocLocation, trans)
                            '            'If clsCommon.myLen(strTemp) <= 0 Then
                            '            '    Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strDocLocation)
                            '            'End If
                            '            'RcvblAcc = New String() {strTemp, -1 * clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance"))}
                            '            'ArrList.Add(RcvblAcc)
                            '            ''------------------- end of richa agarwal 12 July,2018 TEC/03/04/18-000174 branch accounting account added for unapplied entry
                            '        End If
                            '    End If
                            'End If
                            'richa agarwal 15/06/2015
                            If clsCommon.CompairString(CURRENCY_CODE, clsCommon.myCstr(dtReceipt.Rows(0)("BASE_CURRENCY_CODE"))) <> CompairStringResult.Equal Then
                                Dim totalgainlossamount As Double = dblAmount1 - dblAmount
                                If totalgainlossamount = 0 Then
                                    EXCHANGE_LOSS_AMT = 0
                                    EXCHANGE_GAIN_AMT = 0
                                ElseIf totalgainlossamount < 0 Then
                                    If clsCommon.myLen(EXCHANGE_LOSS_ACCOUNT) = 0 Then
                                        Throw New Exception("Exchange Loss Account not defined.")
                                    End If
                                    EXCHANGE_LOSS_AMT = -totalgainlossamount
                                    EXCHANGE_GAIN_AMT = 0
                                Else
                                    If clsCommon.myLen(EXCHANGE_GAIN_ACCOUNT) = 0 Then
                                        Throw New Exception("Exchange Gain Account not defined.")
                                    End If
                                    EXCHANGE_LOSS_AMT = 0
                                    EXCHANGE_GAIN_AMT = totalgainlossamount
                                End If

                                If EXCHANGE_LOSS_AMT > 0 Then
                                    Dim strLossLocation As String = EXCHANGE_LOSS_ACCOUNT.Substring(clsCommon.myLen(EXCHANGE_LOSS_ACCOUNT) - 3, 3)
                                    ''richa agarwal BM00000008088
                                    ' Dim strTemp1 As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, strLossLocation, trans)
                                    Dim strTemp1 As String = clsERPFuncationality.ChangeGLAccountLocationSegment(EXCHANGE_LOSS_ACCOUNT, strDocLocation, True, trans)
                                    If clsCommon.myLen(strTemp1) <= 0 Then
                                        Throw New Exception("Please set Branch account mapping with from location " + strLossLocation + " and to location " + strDocLocation)
                                    End If
                                    Dim BranchAccCRLoss = New String() {strTemp1, EXCHANGE_LOSS_AMT}
                                    ArrList.Add(BranchAccCRLoss)
                                ElseIf EXCHANGE_GAIN_AMT > 0 Then
                                    Dim strGainLocation As String = EXCHANGE_GAIN_ACCOUNT.Substring(clsCommon.myLen(EXCHANGE_GAIN_ACCOUNT) - 3, 3)
                                    ''richa agarwal BM00000008088
                                    Dim strTemp1 As String = clsERPFuncationality.ChangeGLAccountLocationSegment(EXCHANGE_GAIN_ACCOUNT, strDocLocation, True, trans)
                                    ' Dim strTemp1 As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, strGainLocation, trans)
                                    If clsCommon.myLen(strTemp1) <= 0 Then
                                        Throw New Exception("Please set Branch account mapping with from location " + strGainLocation + " and to location " + strDocLocation)
                                    End If
                                    Dim BranchAccCRGain = New String() {strTemp1, -EXCHANGE_GAIN_AMT}
                                    ArrList.Add(BranchAccCRGain)
                                End If
                            End If
                            ''------------------------
                            ''End of Apply branch.
                        Else

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                                ''richa agarwal 03 July,2019 pick location code as per credit note or advance/on Account ERO/03/07/19-000672
                                'Dim STRGLACCOUNTFORAD As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Location_GL_Code,'')  from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(dtReceipt.Rows(0)("Applied_Receipt")) & "'", trans))
                                Dim qry2 As String = "select isnull(Location_GL_Code,'')  from TSPL_RECEIPT_HEADER where Receipt_No='" & clsCommon.myCstr(dtReceipt.Rows(0)("Applied_Receipt")) & "' " & Environment.NewLine &
                                " union all " & Environment.NewLine &
                                " select isnull(Loc_Code,'') from TSPL_Customer_Invoice_Head where Document_No ='" & clsCommon.myCstr(dtReceipt.Rows(0)("Applied_Receipt")) & "'" & Environment.NewLine

                                Dim STRGLACCOUNTFORAD As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry2, trans))

                                If clsCommon.myLen(STRGLACCOUNTFORAD) > 0 AndAlso Not (clsCommon.CompairString(strBankLocation, STRGLACCOUNTFORAD) = CompairStringResult.Equal) Then
                                    strBankAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankAcc, STRGLACCOUNTFORAD, True, trans)
                                End If

                                BankAcc = New String() {strBankAcc, dblAmount1, "", "", "", "", "", "", "B"}
                                ArrList.Add(BankAcc)
                                strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strDocLocation, True, trans)
                                RcvblAcc = New String() {strRcvblAcc, -1 * dblAmount}
                                ArrList.Add(RcvblAcc)


                                '''''''''''''''''''
                                If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(STRGLACCOUNTFORAD, strDocLocation) = CompairStringResult.Equal) Then
                                    ''richa agarwal 09 may,2016
                                    If STRGLACCOUNTFORAD = "" Then
                                        STRGLACCOUNTFORAD = strBankLocation
                                    End If
                                    ''------------------

                                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, STRGLACCOUNTFORAD, trans)
                                    If clsCommon.myLen(strTemp) <= 0 Then
                                        Throw New Exception("Please set Branch account mapping with from location " + strDocLocation + " and to location " + STRGLACCOUNTFORAD)
                                    End If
                                    ''branch accounting of customer
                                    RcvblAcc = New String() {strTemp, dblAmount1}
                                    ArrList.Add(RcvblAcc)


                                    strTemp = ClsBranchAccountMapping.GetBranchAccount(STRGLACCOUNTFORAD, strDocLocation, trans)
                                    If clsCommon.myLen(strTemp) <= 0 Then
                                        Throw New Exception("Please set Branch account mapping with from location " + STRGLACCOUNTFORAD + " and to location " + strDocLocation)
                                    End If
                                    ''branch accounting of bank
                                    RcvblAcc = New String() {strTemp, -1 * dblAmount1}
                                    ArrList.Add(RcvblAcc)


                                    ''------------------
                                End If


                                'richa agarwal 15/06/2015
                                If clsCommon.CompairString(CURRENCY_CODE, clsCommon.myCstr(dtReceipt.Rows(0)("BASE_CURRENCY_CODE"))) <> CompairStringResult.Equal Then
                                    Dim totalgainlossamount As Double = dblAmount1 - dblAmount
                                    If totalgainlossamount = 0 Then
                                        EXCHANGE_LOSS_AMT = 0
                                        EXCHANGE_GAIN_AMT = 0
                                    ElseIf totalgainlossamount < 0 Then
                                        If clsCommon.myLen(EXCHANGE_LOSS_ACCOUNT) = 0 Then
                                            Throw New Exception("Exchange Loss Account not defined.")
                                        End If
                                        EXCHANGE_LOSS_AMT = -totalgainlossamount
                                        EXCHANGE_GAIN_AMT = 0
                                    Else
                                        If clsCommon.myLen(EXCHANGE_GAIN_ACCOUNT) = 0 Then
                                            Throw New Exception("Exchange Gain Account not defined.")
                                        End If
                                        EXCHANGE_LOSS_AMT = 0
                                        EXCHANGE_GAIN_AMT = totalgainlossamount
                                    End If

                                    If EXCHANGE_LOSS_AMT > 0 Then
                                        Dim strLossLocation As String = EXCHANGE_LOSS_ACCOUNT.Substring(clsCommon.myLen(EXCHANGE_LOSS_ACCOUNT) - 3, 3)
                                        ''richa agarwal BM00000008088
                                        Dim strTemp1 As String = clsERPFuncationality.ChangeGLAccountLocationSegment(EXCHANGE_LOSS_ACCOUNT, strDocLocation, True, trans)
                                        If clsCommon.myLen(strTemp1) <= 0 Then
                                            Throw New Exception("Please set Branch account mapping with from location " + strLossLocation + " and to location " + strDocLocation)
                                        End If
                                        Dim BranchAccCRLoss = New String() {strTemp1, EXCHANGE_LOSS_AMT}
                                        ArrList.Add(BranchAccCRLoss)
                                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
                                        Dim strGainLocation As String = EXCHANGE_GAIN_ACCOUNT.Substring(clsCommon.myLen(EXCHANGE_GAIN_ACCOUNT) - 3, 3)
                                        ''richa agarwal BM00000008088
                                        Dim strTemp1 As String = clsERPFuncationality.ChangeGLAccountLocationSegment(EXCHANGE_GAIN_ACCOUNT, strDocLocation, True, trans)
                                        If clsCommon.myLen(strTemp1) <= 0 Then
                                            Throw New Exception("Please set Branch account mapping with from location " + strGainLocation + " and to location " + strDocLocation)
                                        End If
                                        Dim BranchAccCRGain = New String() {strTemp1, -EXCHANGE_GAIN_AMT}
                                        ArrList.Add(BranchAccCRGain)
                                    End If
                                End If
                                ''------------------------
                                ''End of Apply branch.



                                ' ''richa agarwal 16/06/2015
                                'If clsCommon.CompairString(CURRENCY_CODE, clsCommon.myCstr(dtReceipt.Rows(0)("BASE_CURRENCY_CODE"))) <> CompairStringResult.Equal Then
                                '    If clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "R") = CompairStringResult.Equal Or clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "A") = CompairStringResult.Equal Then
                                '        If EXCHANGE_LOSS_AMT > 0 Then
                                '            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                                '            ArrList.Add(CURR_EXCHANGE)
                                '        ElseIf EXCHANGE_GAIN_AMT > 0 Then
                                '            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                                '            ArrList.Add(CURR_EXCHANGE)
                                '        End If
                                '    End If
                                'End If


                            Else

                                BankAcc = New String() {strBankAcc, dblAmount1, "", "", "", "", "", "", "B"}
                                ArrList.Add(BankAcc)
                                RcvblAcc = New String() {strRcvblAcc, -1 * dblAmount}
                                ArrList.Add(RcvblAcc)

                                ''------ 30 Nov,2017
                                If clsCommon.CompairString(CURRENCY_CODE, clsCommon.myCstr(dtReceipt.Rows(0)("BASE_CURRENCY_CODE"))) <> CompairStringResult.Equal Then
                                    Dim totalgainlossamount As Double = dblAmount1 - dblAmount
                                    If totalgainlossamount = 0 Then
                                        EXCHANGE_LOSS_AMT = 0
                                        EXCHANGE_GAIN_AMT = 0
                                    ElseIf totalgainlossamount < 0 Then
                                        If clsCommon.myLen(EXCHANGE_LOSS_ACCOUNT) = 0 Then
                                            Throw New Exception("Exchange Loss Account not defined.")
                                        End If
                                        EXCHANGE_LOSS_AMT = -totalgainlossamount
                                        EXCHANGE_GAIN_AMT = 0
                                    Else
                                        If clsCommon.myLen(EXCHANGE_GAIN_ACCOUNT) = 0 Then
                                            Throw New Exception("Exchange Gain Account not defined.")
                                        End If
                                        EXCHANGE_LOSS_AMT = 0
                                        EXCHANGE_GAIN_AMT = totalgainlossamount
                                    End If

                                End If

                                ''-----------


                                ''richa agarwal 16/06/2015
                                If clsCommon.CompairString(CURRENCY_CODE, clsCommon.myCstr(dtReceipt.Rows(0)("BASE_CURRENCY_CODE"))) <> CompairStringResult.Equal Then
                                    If clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "R") = CompairStringResult.Equal Or clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "A") = CompairStringResult.Equal Then
                                        If EXCHANGE_LOSS_AMT > 0 Then
                                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
                                            ArrList.Add(CURR_EXCHANGE)
                                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
                                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
                                            ArrList.Add(CURR_EXCHANGE)
                                        End If
                                    End If
                                End If
                                ''------------------sdw
                                ' Anubhooti 01-Dec-2014 (If Branch Accounting is not applied Receiving)
                                'If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
                                '    If clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance")) > 0 Then
                                '        Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                                '        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                                '        strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                                '        ''richa replace location with bank location as per ranjana mam 13 Nov,2018 TEC/03/04/18-000174
                                '        strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strBankLocation, True, trans)
                                '        ''richa agarwal 30/06/2015
                                '        'strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strDocLocation, True, trans)
                                '        ''--------------
                                '        RcvblAcc = New String() {strRcvblAcc, -1 * clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance"))}
                                '        ArrList.Add(RcvblAcc)
                                '        '
                                '        RcvblAcc = New String() {strBankAcc, clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance")), "", "", "", "", "", "", "B"}
                                '        ArrList.Add(RcvblAcc)
                                '    End If

                                'End If

                            End If
                            ''
                        End If
                    Next
                    '' richa unapplied amount used only once 19 sep 2020
                    If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance")) > 0 Then
                            Dim tBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                            strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                            strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                            strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strBankLocation, True, trans)

                            RcvblAcc = New String() {strRcvblAcc, -1 * clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance"))}
                            ArrList.Add(RcvblAcc)
                            '
                            RcvblAcc = New String() {strBankAcc, clsCommon.myCdbl(dtReceipt.Rows(0)("UnApplied_Balance")), "", "", "", "", "", "", "B"}
                            ArrList.Add(RcvblAcc)
                        End If

                    End If


                    ''richa agarwal 09/06/2015
                    If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                        bankAccountChargesAmount = (clsCommon.myCdbl(ConvRate * clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt")))
                        If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                            BankAcc = New String() {strBankAcc, -1 * bankAccountChargesAmount}
                        Else
                            BankAcc = New String() {strBankAcc, -1 * bankAccountChargesAmount, "", "", "", "", "", "", "B"}
                        End If
                        ArrList.Add(BankAcc)
                    End If

                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "O") = CompairStringResult.Equal Then
                    Dim strTemp As String = ""
                    BankAcc = New String() {strBankAcc, (dtReceipt.Rows(0)("Receipt_Amount").ToString() * ConvRate - ((ConvRate * clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt"))) + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0)), "", "", "", "", "", "", "B"}
                    ArrList.Add(BankAcc)
                    ''richa agarwal 05/06/2015
                    If clsCommon.myLen(dtReceipt.Rows(0)("Location_GL_Code").ToString()) > 0 Then
                        strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, dtReceipt.Rows(0)("Location_GL_Code").ToString(), True, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + strBankAcc + " and to location " + dtReceipt.Rows(0)("Location_GL_Code"))
                        End If
                        RcvblAcc = New String() {strTemp, Convert.ToDecimal(dtReceipt.Rows(0)("Receipt_Amount").ToString()) * ConvRate * -1}
                        ArrList.Add(RcvblAcc)
                    Else
                        RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(dtReceipt.Rows(0)("Receipt_Amount").ToString()) * ConvRate * -1}
                        ArrList.Add(RcvblAcc)
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then

                Else
                    ''richa AGARWAL 14/05/2015  BANK ACCOUNT CREDIT(MEANS -)
                    If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                        '' richa agarwal 9 Jan,2017
                        ''BankAcc = New String() {strBankAcc, clsCommon.myCdbl((dtReceipt.Rows(0)("Receipt_Amount").ToString() * ConvRate - ((ConvRate * clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt"))) + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))) * -1}
                        BankAcc = New String() {strBankAcc, clsCommon.myCdbl((dtReceipt.Rows(0)("Receipt_Amount").ToString() * ConvRate + clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt")) - ((ConvRate * clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt")))) + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0))) * -1, "", "", "", "", "", "", "B"}
                    Else
                        ''richa agarwal deduct foreign bank amount * converison + indian bANK AMOUNT from bank amount
                        BankAcc = New String() {strBankAcc, (dtReceipt.Rows(0)("Receipt_Amount").ToString() * ConvRate - ((ConvRate * clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt"))) + clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt"))) + IIf(IsMultiCurrency, (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), 0)), "", "", "", "", "", "", "B"}

                    End If
                    ''----------------------------------------
                    ArrList.Add(BankAcc)
                    If clsCommon.myLen(clsCommon.myCstr(dtReceipt.Rows(0)("Location_GL_Code"))) > 0 Then
                        strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, clsCommon.myCstr(dtReceipt.Rows(0)("Location_GL_Code")), True, trans)
                    End If
                    ''richa AGARWAL 14/05/2015  CUSTOMER ACCOUNT DEBIT(MEANS +)
                    If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                        RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(dtReceipt.Rows(0)("Receipt_Amount").ToString()) * ConvRate * 1}
                    Else
                        RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(dtReceipt.Rows(0)("Receipt_Amount").ToString()) * ConvRate * -1}
                    End If
                    ''-----------------------------
                    ArrList.Add(RcvblAcc)
                End If

                Dim PayType As String = ""
                If dtReceipt.Rows(0)("Receipt_Type").ToString() = "R" Then
                    PayType = "AR-PY"
                ElseIf dtReceipt.Rows(0)("Receipt_Type").ToString() = "O" Then
                    PayType = "AR-OA"
                ElseIf dtReceipt.Rows(0)("Receipt_Type").ToString() = "A" Or dtReceipt.Rows(0)("Receipt_Type").ToString() = "K" Then
                    PayType = "AR-DC"
                ElseIf dtReceipt.Rows(0)("Receipt_Type").ToString() = "P" Then 'Or sqlDr.Rows(0)("Receipt_Type").ToString() = "A" Then
                    PayType = "AR-PI"
                ElseIf dtReceipt.Rows(0)("Receipt_Type").ToString() = "U" Then
                    PayType = "AR-UC"
                ElseIf dtReceipt.Rows(0)("Receipt_Type").ToString() = "F" Then
                    PayType = "AR-RF"
                End If

                If IsMultiCurrency Then
                    If clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "R") = CompairStringResult.Equal Or clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "A") = CompairStringResult.Equal Then

                    Else
                        If EXCHANGE_LOSS_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT / ConvRate}
                            ArrList.Add(CURR_EXCHANGE)
                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT / ConvRate}
                            ArrList.Add(CURR_EXCHANGE)
                        End If
                    End If
                End If

                If clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "P") = CompairStringResult.Equal Or clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "O") = CompairStringResult.Equal Or clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then
                    '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location)

                    Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                    Dim strGLLoc As String = clsCommon.myCstr(dtReceipt.Rows(0)("Location_GL_Code"))

                    Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                    If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(strBankLocation, strGLLoc) = CompairStringResult.Equal) Then
                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strBankLocation, strGLLoc, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + strBankLocation + " and to location " + strGLLoc)
                        End If
                        If clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then
                            Dim BranchAccCR = New String() {strTemp, 1 * clsCommon.myCdbl(dtReceipt.Rows(0)("Receipt_Amount").ToString()) * ConvRate}
                            ArrList.Add(BranchAccCR)
                        Else
                            Dim BranchAccCR = New String() {strTemp, -1 * clsCommon.myCdbl(dtReceipt.Rows(0)("Receipt_Amount").ToString()) * ConvRate}
                            ArrList.Add(BranchAccCR)
                        End If
                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, strBankLocation, trans)
                        If clsCommon.myLen(strTemp) <= 0 Then
                            Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + strBankLocation)
                        End If
                        If clsCommon.CompairString(dtReceipt.Rows(0)("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then
                            Dim BranchAccDR = New String() {strTemp, -1 * clsCommon.myCdbl(dtReceipt.Rows(0)("Receipt_Amount").ToString()) * ConvRate}
                            ArrList.Add(BranchAccDR)
                        Else
                            Dim BranchAccDR = New String() {strTemp, clsCommon.myCdbl(dtReceipt.Rows(0)("Receipt_Amount").ToString()) * ConvRate}
                            ArrList.Add(BranchAccDR)
                        End If

                    End If
                End If

                ''richa agarwal
                Dim strForeignBankAcc As String = String.Empty
                Dim strBankChargesOtherAcc As String = String.Empty
                Dim BankChargesAcc() As String
                If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") <> CompairStringResult.Equal Then
                    Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                    If clsCommon.myCdbl(clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt"))) <> 0 Then
                        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account  FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                        strForeignBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                        If clsCommon.myLen(strForeignBankAcc) <= 0 Then
                            Throw New Exception("Please Select Foreign Bank Charges Account first ")
                            Return False
                        End If
                        ''richa TEC/12/07/19-000944
                        strForeignBankAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strForeignBankAcc, IIf(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")) = "R", strBankLocation, clsCommon.myCstr(dtReceipt.Rows(0)("Location_GL_Code"))), True, trans)
                        BankChargesAcc = New String() {strForeignBankAcc, ConvRate * clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt"))}
                        ArrList.Add(BankChargesAcc)
                    End If
                    If clsCommon.myCdbl(clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt"))) <> 0 Then
                        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                        strBankChargesOtherAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                        If clsCommon.myLen(strBankChargesOtherAcc) <= 0 Then
                            Throw New Exception("Please Select Bank Charges Other Account first ")
                            Return False
                        End If
                        ''richa TEC/12/07/19-000944
                        strBankChargesOtherAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankChargesOtherAcc, IIf(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")) = "R", strBankLocation, clsCommon.myCstr(dtReceipt.Rows(0)("Location_GL_Code"))), True, trans)
                        BankChargesAcc = New String() {strBankChargesOtherAcc, clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt"))}
                        ArrList.Add(BankChargesAcc)
                    End If
                Else
                    Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                    If clsCommon.myCdbl(clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt"))) <> 0 Then
                        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account  FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                        strForeignBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                        If clsCommon.myLen(strForeignBankAcc) <= 0 Then
                            Throw New Exception("Please Select Foreign Bank Charges Account first ")
                            Return False
                        End If
                        ''richa TEC/12/07/19-000944
                        strForeignBankAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strForeignBankAcc, IIf(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")) = "R", strBankLocation, clsCommon.myCstr(dtReceipt.Rows(0)("Location_GL_Code"))), True, trans)
                        BankChargesAcc = New String() {strForeignBankAcc, -1 * ConvRate * clsCommon.myCdbl(dtReceipt.Rows(0)("Foreign_Bank_Charges_Amt"))}
                        ArrList.Add(BankChargesAcc)
                    End If
                    If clsCommon.myCdbl(clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt"))) <> 0 Then
                        strQ1 = "SELECT TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + clsCommon.myCstr(dtReceipt.Rows(0)("Cust_Code")) + "'"
                        strBankChargesOtherAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                        If clsCommon.myLen(strBankChargesOtherAcc) <= 0 Then
                            Throw New Exception("Please Select Bank Charges Other Account first ")
                            Return False
                        End If
                        '' richa agarwal 9 Jan,2017
                        ' BankChargesAcc = New String() {strBankChargesOtherAcc, -1 * clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt"))}
                        ''richa TEC/12/07/19-000944
                        strBankChargesOtherAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankChargesOtherAcc, IIf(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")) = "R", strBankLocation, clsCommon.myCstr(dtReceipt.Rows(0)("Location_GL_Code"))), True, trans)
                        BankChargesAcc = New String() {strBankChargesOtherAcc, 1 * clsCommon.myCdbl(dtReceipt.Rows(0)("Bank_Charges_Amt"))}
                        ArrList.Add(BankChargesAcc)
                    End If

                End If
                Dim GSTStatus As Boolean = False
                GSTStatus = clsERPFuncationality.GetGSTStatus(dtReceipt.Rows(0)("Receipt_Date"))
                If GSTStatus AndAlso (clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal) AndAlso clsCommon.myLen(clsCommon.myCstr(dtReceipt.Rows(0)("Tax_Group"))) > 0 AndAlso clsCommon.myCdbl(dtReceipt.Rows(0)("Tax_Amount_Advance")) > 0 Then
                    Dim objTM As clsTaxMaster
                    Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                    'Dim strDOLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Bill_To_Location  from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE where Document_Code ='" & clsCommon.myCstr(dtReceipt.Rows(0)("Delivery_Code_PS")) & "' ", trans))
                    Dim strDOLocation As String = clsCommon.myCstr(dtReceipt.Rows(0)("SO_Location_Code"))
                    Dim strSODONo As String = clsCommon.myCstr(dtReceipt.Rows(0)("Delivery_Code_PS"))
                    Dim Acc1() As String = Nothing
                    Dim Acc2() As String = Nothing
                    Dim STRGLACCOUNTFORAD As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Location_GL_Code,'')  from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_No")) & "'", trans))

                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX1_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX1")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX1")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            Else
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, STRGLACCOUNTFORAD, True, trans)
                            End If



                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX1_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX1_Amt")) * -1}
                            End If

                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX1")))
                            End If

                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            Else
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, STRGLACCOUNTFORAD, True, trans)
                            End If


                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX1_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX1_Amt"))}
                            End If

                            ArrList.Add(Acc2)
                        End If
                    End If

                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX2_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX2")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX2")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            Else
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, STRGLACCOUNTFORAD, True, trans)
                            End If


                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX2_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX2_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX2")))
                            End If

                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            Else
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, STRGLACCOUNTFORAD, True, trans)
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX2_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX2_Amt"))}
                            End If

                            ArrList.Add(Acc2)

                        End If
                    End If


                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX3_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX3")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX3")))
                            End If

                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            Else
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, STRGLACCOUNTFORAD, True, trans)
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX3_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX3_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX3")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            Else
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, STRGLACCOUNTFORAD, True, trans)
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX3_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX3_Amt"))}
                            End If
                            ArrList.Add(Acc2)

                        End If
                    End If

                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX4_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX4")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX4")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            Else
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, STRGLACCOUNTFORAD, True, trans)
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX4_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX4_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX4")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            Else
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, STRGLACCOUNTFORAD, True, trans)
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX4_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX4_Amt"))}
                            End If
                            ArrList.Add(Acc2)

                        End If
                    End If


                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX5_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX5")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX5")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            Else
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, STRGLACCOUNTFORAD, True, trans)
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX5_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX5_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX5")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            Else
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, STRGLACCOUNTFORAD, True, trans)
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX5_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX5_Amt"))}
                            End If
                            ArrList.Add(Acc2)

                        End If
                    End If

                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX6_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX6")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX6")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            Else
                                objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, STRGLACCOUNTFORAD, False, trans)
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX6_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX6_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX6")))
                            End If
                            If clsCommon.myLen(strSODONo) > 0 Then
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            Else
                                objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, STRGLACCOUNTFORAD, False, trans)
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX6_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX6_Amt"))}
                            End If
                            ArrList.Add(Acc2)

                        End If
                    End If

                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX7_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX7")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX7")))
                            End If
                            objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX7_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX7_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX7")))
                            End If

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX7_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX7_Amt"))}
                            End If
                            ArrList.Add(Acc2)

                        End If
                    End If

                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX8_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX8")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX8")))
                            End If
                            objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX8_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX8_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX8")))
                            End If

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX8_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX8_Amt"))}
                            End If
                            ArrList.Add(Acc2)

                        End If
                    End If

                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX9_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX9")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX9")))
                            End If
                            objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX9_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX9_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX9")))
                            End If

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX9_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX9_Amt"))}
                            End If
                            ArrList.Add(Acc2)

                        End If
                    End If

                    If clsCommon.myCdbl(dtReceipt.Rows(0)("TAX10_Amt")) <> 0 Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(dtReceipt.Rows(0)("TAX10")), trans)
                        If objTM IsNot Nothing Then
                            If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX10")))
                            End If
                            objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX10_Amt")) * 1}
                            Else
                                Acc1 = {objTM.PayableControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX10_Amt")) * -1}
                            End If
                            ArrList.Add(Acc1)

                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(dtReceipt.Rows(0)("TAX10")))
                            End If

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "F") = CompairStringResult.Equal Then
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX10_Amt")) * -1}
                            Else
                                Acc2 = {objTM.DepositControl, clsCommon.myCdbl(dtReceipt.Rows(0)("TAX10_Amt"))}
                            End If
                            ArrList.Add(Acc2)

                        End If
                    End If

                End If



                transportSql.FunGrnlEntryWithTrans(LocSegmentCode, True, isForUnpostedTransaction, strVoucherNo, trans, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Post_Date")), dtReceipt.Rows(0)("Entry_Desc").ToString(), PayType, "AR Payment Received", strDocNo, "", "C", dtReceipt.Rows(0)("Cust_Code").ToString(), dtReceipt.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , dtReceipt.Rows(0)("Reference").ToString(), dtReceipt.Rows(0)("Narration").ToString(), coll, objJE)
                coll = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function GetQuery(ByVal strDocNo As String) As String
        Dim strQ As String = Nothing
        strQ = "SELECT TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Date, TSPL_RECEIPT_HEADER.Receipt_Post_Date,TSPL_RECEIPT_HEADER.Foreign_Bank_Charges_Amt ,TSPL_RECEIPT_HEADER.Bank_Charges_Amt, " &
              "   TSPL_RECEIPT_HEADER.Entry_Desc, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_RECEIPT_HEADER.Receipt_Type,  " &
              "   TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Customer_Name, TSPL_RECEIPT_HEADER.Reference,  " &
              "   TSPL_RECEIPT_HEADER.Narration, TSPL_RECEIPT_HEADER.Payment_Code, TSPL_RECEIPT_HEADER.Cheque_No,TSPL_RECEIPT_HEADER.BASE_CURRENCY_CODE,  " &
              "   TSPL_RECEIPT_HEADER.Cheque_Date, Cheque_From, TSPL_RECEIPT_HEADER.Receipt_Amount as Receipt_Amount, TSPL_RECEIPT_HEADER.Cust_Account,  " &
              "   TSPL_RECEIPT_HEADER.Apply_By, TSPL_RECEIPT_HEADER.Apply_To, TSPL_RECEIPT_HEADER.Posted, TSPL_RECEIPT_HEADER.Balance_Amt,  " &
              "   TSPL_RECEIPT_HEADER.Document_No, TSPL_RECEIPT_HEADER.UnApply_Amt, TSPL_RECEIPT_HEADER.Payer,  " &
              "   TSPL_RECEIPT_HEADER.Dr_Account, TSPL_RECEIPT_HEADER.Cr_Account, TSPL_RECEIPT_HEADER.UnApplied_Balance, TSPL_RECEIPT_DETAIL.Receipt_No as [ReceiptNo_D], " &
              "   TSPL_RECEIPT_DETAIL.Receipt_Line_No, TSPL_RECEIPT_DETAIL.Apply, TSPL_RECEIPT_DETAIL.Receipt_Type as [Type_D], " &
              "   TSPL_RECEIPT_DETAIL.Document_No AS [Doc_D], TSPL_RECEIPT_DETAIL.Original_Amt, case when TSPL_RECEIPT_DETAIL.Receipt_Type='F' then (Select RH.Balance_Amt from TSPL_RECEIPT_HEADER RH WHERE RH.Receipt_No=TSPL_RECEIPT_DETAIL.Document_No)  else  TSPL_RECEIPT_DETAIL.Pending_Balance end as Pending_Balance, " &
              "   TSPL_RECEIPT_DETAIL.Applied_Amount * TSPL_RECEIPT_HEADER.ConvRateOld as Applied_Amount, TSPL_RECEIPT_DETAIL.Account_Code, TSPL_RECEIPT_DETAIL.Description,  " &
              "   TSPL_RECEIPT_DETAIL.Remarks, TSPL_RECEIPT_DETAIL.Comment, TSPL_RECEIPT_DETAIL.Shipment_No,  " &
              "   TSPL_RECEIPT_DETAIL.Adjustment_Account, TSPL_RECEIPT_DETAIL.Adjustment_Cost, TSPL_RECEIPT_DETAIL.Adjustment_No, ISNULL(TSPL_RECEIPT_ADJUSTMENT_HEADER.Is_Post,'N') as AdjStatus, TSPL_RECEIPT_HEADER.UnApplied_No,TSPL_RECEIPT_HEADER.CURRENCY_CODE,TSPL_RECEIPT_HEADER.APPLICABLEFROM,TSPL_RECEIPT_HEADER.RECEIVED_AMOUNT_BASE_CURRENCY,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT, " &
              "   TSPL_RECEIPT_DETAIL.Hirerachy_Level_Code ,TSPL_RECEIPT_DETAIL.Cost_Center_Fin_Code, " &
              "   TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_ACCOUNT,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_ACCOUNT,TSPL_RECEIPT_HEADER.ConvRate,TSPL_RECEIPT_DETAIL.ConvRateOld,TSPL_RECEIPT_HEADER.CForm_InvoiceNo, TSPL_RECEIPT_HEADER.Applied_Receipt, " &
               " TSPL_RECEIPT_HEADER.TAX1,TSPL_RECEIPT_HEADER.TAX1_Rate,TSPL_RECEIPT_HEADER.TAX1_Amt,TSPL_RECEIPT_HEADER.TAX1_Base_Amt,TSPL_RECEIPT_HEADER.TAX2,TSPL_RECEIPT_HEADER.TAX2_Rate,TSPL_RECEIPT_HEADER.TAX2_Amt,TSPL_RECEIPT_HEADER.TAX2_Base_Amt," &
                " TSPL_RECEIPT_HEADER.TAX3,TSPL_RECEIPT_HEADER.TAX3_Rate,TSPL_RECEIPT_HEADER.TAX3_Amt,TSPL_RECEIPT_HEADER.TAX3_Base_Amt,TSPL_RECEIPT_HEADER.TAX4,TSPL_RECEIPT_HEADER.TAX4_Rate," &
                " TSPL_RECEIPT_HEADER.TAX4_Amt,TSPL_RECEIPT_HEADER.TAX4_Base_Amt,TSPL_RECEIPT_HEADER.TAX5,TSPL_RECEIPT_HEADER.TAX5_Rate,TSPL_RECEIPT_HEADER.TAX5_Amt,TSPL_RECEIPT_HEADER.TAX5_Base_Amt," &
                " TSPL_RECEIPT_HEADER.TAX6,TSPL_RECEIPT_HEADER.TAX6_Rate,TSPL_RECEIPT_HEADER.TAX6_Amt,TSPL_RECEIPT_HEADER.TAX6_Base_Amt,TSPL_RECEIPT_HEADER.tax7, TSPL_RECEIPT_HEADER.TAX7_Rate, " &
                " TSPL_RECEIPT_HEADER.TAX7_Amt, TSPL_RECEIPT_HEADER.TAX7_Base_Amt, TSPL_RECEIPT_HEADER.TAX8, TSPL_RECEIPT_HEADER.TAX8_Rate, TSPL_RECEIPT_HEADER.TAX8_Amt," &
                " TSPL_RECEIPT_HEADER.TAX8_Base_Amt, TSPL_RECEIPT_HEADER.TAX9, TSPL_RECEIPT_HEADER.TAX9_Rate, TSPL_RECEIPT_HEADER.TAX9_Amt, TSPL_RECEIPT_HEADER.TAX9_Base_Amt, " &
                " TSPL_RECEIPT_HEADER.TAX10, TSPL_RECEIPT_HEADER.TAX10_Rate, TSPL_RECEIPT_HEADER.TAX10_Amt, TSPL_RECEIPT_HEADER.TAX10_Base_Amt,TSPL_RECEIPT_HEADER.Tax_Group,TSPL_RECEIPT_HEADER.Tax_Amount_Advance,TSPL_RECEIPT_HEADER.SO_Location_Code, " &
              "   TSPL_RECEIPT_HEADER.location_gl_Code,TSPL_RECEIPT_HEADER.SecurityDeposit,TSPL_RECEIPT_HEADER.SecurityDepositType,TSPL_RECEIPT_HEADER.Delivery_Code_PS,TSPL_RECEIPT_HEADER.Set_Off_Date,TSPL_RECEIPT_HEADER.SetOffSkipJE,TSPL_RECEIPT_HEADER.TapalNo,TSPL_RECEIPT_HEADER.DateAndTime FROM TSPL_RECEIPT_HEADER left JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_RECEIPT_DETAIL.Receipt_No " &
              " LEFT OUTER JOIN TSPL_Receipt_Adjustment_Header ON TSPL_Receipt_Adjustment_Header.Adjustment_No=TSPL_RECEIPT_DETAIL.Adjustment_No" &
              "   where TSPL_RECEIPT_HEADER.Receipt_No ='" + strDocNo + "'"
        Return strQ
    End Function

    Public Shared Function funRcptPostUnApplied(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strQ As String = Nothing
        Dim sqlDr As DataTable
        Try
            strQ = " SELECT TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Date, TSPL_RECEIPT_HEADER.Receipt_Post_Date, " & _
                   "   TSPL_RECEIPT_HEADER.Entry_Desc, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_RECEIPT_HEADER.Receipt_Type,  " & _
                   "   TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Customer_Name, TSPL_RECEIPT_HEADER.Reference,  " & _
                   "   TSPL_RECEIPT_HEADER.Narration, TSPL_RECEIPT_HEADER.Payment_Code, TSPL_RECEIPT_HEADER.Cheque_No,  " & _
                   "   TSPL_RECEIPT_HEADER.Cheque_Date, TSPL_RECEIPT_HEADER.Receipt_Amount, TSPL_RECEIPT_HEADER.Cust_Account,  " & _
                   "   TSPL_RECEIPT_HEADER.Apply_By, TSPL_RECEIPT_HEADER.Apply_To, TSPL_RECEIPT_HEADER.Posted, TSPL_RECEIPT_HEADER.Balance_Amt,  " & _
                   "   TSPL_RECEIPT_HEADER.Document_No, TSPL_RECEIPT_HEADER.UnApply_Amt, TSPL_RECEIPT_HEADER.Payer,  " & _
                   "   TSPL_RECEIPT_HEADER.Dr_Account, TSPL_RECEIPT_HEADER.Cr_Account, TSPL_RECEIPT_DETAIL.Receipt_No as [ReceiptNo_D], " & _
                   "   TSPL_RECEIPT_DETAIL.Receipt_Line_No, TSPL_RECEIPT_DETAIL.Apply, TSPL_RECEIPT_DETAIL.Receipt_Type as [Type_D], " & _
                   "   TSPL_RECEIPT_DETAIL.Document_No AS [Doc_D], TSPL_RECEIPT_DETAIL.Original_Amt, TSPL_RECEIPT_DETAIL.Pending_Balance, " & _
                   "   TSPL_RECEIPT_DETAIL.Applied_Amount, TSPL_RECEIPT_DETAIL.Account_Code, TSPL_RECEIPT_DETAIL.Description,  " & _
                   "   TSPL_RECEIPT_DETAIL.Remarks, TSPL_RECEIPT_DETAIL.Comment, TSPL_RECEIPT_DETAIL.Shipment_No,  " & _
                   "   TSPL_RECEIPT_DETAIL.Adjustment_Account, TSPL_RECEIPT_DETAIL.Adjustment_Cost, TSPL_RECEIPT_DETAIL.Adjustment_No,TSPL_RECEIPT_HEADER.UnApplied_No " & _
                   "   FROM TSPL_RECEIPT_HEADER left JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_RECEIPT_DETAIL.Receipt_No " & _
                   "   where TSPL_RECEIPT_HEADER.Receipt_No ='" + strDocNo + "'"
            sqlDr = clsDBFuncationality.GetDataTable(strQ, trans)

            If sqlDr Is Nothing OrElse sqlDr.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to Post")
            End If
            If sqlDr.Rows(0)("Receipt_Type").ToString() = "M" Then
                Dim strQ1 As String = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + sqlDr.Rows(0)("Bank_Code").ToString() + "'"
                Dim strBankAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                strQ1 = "select SUBSTRING(BANKACC ,6,3) as Segment_Code  from TSPL_BANK_MASTER where BANK_CODE='" + sqlDr.Rows(0)("Bank_Code").ToString() + "'"
                Dim BankLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                Dim arrlist As New ArrayList()
                Dim BankAcc() As String = {strBankAcc, clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount"))}
                arrlist.Add(BankAcc)
                'Dim sqlDr1 As SqlDataReader = connectSql.RunSqlReturnDR(strQ)
                For Each dr As DataRow In sqlDr.Rows
                    If Not String.IsNullOrEmpty(dr.Item("Applied_Amount").ToString()) And dr.Item("Applied_Amount").ToString() <> 0 Then
                        Dim amt As Decimal = 0
                        amt = CDec(dr.Item("Applied_Amount").ToString())
                        Dim acct As String = CStr(dr.Item("Account_Code").ToString())
                        Dim strRef As String = CStr(dr.Item("Remarks").ToString())
                        Dim RcvblAcc() As String = {acct, amt * (-1), strRef}
                        arrlist.Add(RcvblAcc)
                    End If
                Next

                transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, clsCommon.myCDate(sqlDr.Rows(0)("Receipt_Post_Date")), sqlDr.Rows(0)("Entry_Desc").ToString(), "AR-MI", "AR Payment Received", strDocNo, "", "C", sqlDr.Rows(0)("Cust_Code").ToString(), sqlDr.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , sqlDr.Rows(0)("Reference").ToString(), sqlDr.Rows(0)("Narration").ToString())

            Else
                Dim strRcvblAcc As String
                Dim strBankAcc As String
                Dim ArrList As ArrayList = New ArrayList()

                Dim strQuery As String = "select Isnull(Cr_Account, '') as Cr_Account  from TSPL_RECEIPT_HEADER where Receipt_No='" + strDocNo + "'"
                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQuery, trans))

                Dim strQ2 As String = "select Isnull(Dr_Account , '') as Dr_Account  from TSPL_RECEIPT_HEADER where Receipt_No='" + strDocNo + "'"
                strBankAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ2, trans))
                strQ2 = "select SUBSTRING(BANKACC ,6,3) as Segment_Code  from TSPL_BANK_MASTER where BANK_CODE='" + strBankAcc + "'"
                Dim BankLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ2, trans))

                Dim RcvblAcc() As String
                Dim ADjAcc() As String
                Dim BankAcc() As String
                Dim TAdjAmt As Decimal = 0
                Dim AdjAmt As Decimal = 0
                If sqlDr.Rows(0)("Receipt_Type").ToString() = "R" Then
                    ''Dim dr11 As SqlDataReader = connectSql.RunSqlReturnDR(strQ)
                    ''While dr11.Read
                    Dim i As Integer = 0
                    Dim strQ1 As String
                    Dim ADjNo As String = Convert.ToString(sqlDr.Rows(0)("Adjustment_No").ToString())
                    strQ1 = " SELECT isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount,0) as Adjustment_Amount,TSPL_Receipt_Adjustment_Header.Adjustment_No, TSPL_Receipt_Adjustment_Header.Description, " & _
                                      " TSPL_Receipt_Adjustment_Header.Adjustment_Date, TSPL_Receipt_Adjustment_Header.Post_Date, TSPL_Receipt_Adjustment_Header.Customer_No, " & _
                                      "  TSPL_Receipt_Adjustment_Header.Customer_Name, TSPL_Receipt_Adjustment_Header.Doc_No, TSPL_Receipt_Adjustment_Header.Doc_Amount, " & _
                                      "  TSPL_Receipt_Adjustment_Header.Remarks,  TSPL_Receipt_Adjustment_Detail.Account_No, " & _
                                      "  TSPL_Receipt_Adjustment_Detail.Account_Description, isnull(TSPL_Receipt_Adjustment_Detail.Amount,0) as Amount, TSPL_Receipt_Adjustment_Detail.Remarks AS Expr1" & _
                                      "  FROM TSPL_Receipt_Adjustment_Header INNER JOIN TSPL_Receipt_Adjustment_Detail ON TSPL_Receipt_Adjustment_Header.Adjustment_No = TSPL_Receipt_Adjustment_Detail.Adjustment_No WHERE     (TSPL_Receipt_Adjustment_Header.Adjustment_No = '" + ADjNo + "')"
                    If ADjNo <> "" Then
                        TAdjAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQ1, trans))
                    End If

                    Dim invNo As String = Convert.ToString(sqlDr.Rows(0)("Doc_D").ToString())
                    Dim Amt As String = clsCommon.myCdbl(sqlDr.Rows(0)("Applied_Amount"))

                    If i = 0 Then
                        BankAcc = New String() {strBankAcc, CDec(sqlDr.Rows(0)("Receipt_Amount").ToString())}
                        ArrList.Add(BankAcc)
                    End If
                    Dim dr As DataTable = clsDBFuncationality.GetDataTable(strQ1, trans)
                    For Each dr1 As DataRow In dr.Rows
                        Dim AdjAccount As String = dr1("Account_No").ToString()
                        AdjAmt = CDec(dr1("Amount").ToString())
                        ADjAcc = New String() {AdjAccount, Convert.ToDecimal(AdjAmt)}
                        ArrList.Add(ADjAcc)
                    Next

                    If Convert.ToString(sqlDr.Rows(0)("Adjustment_No").ToString()) <> "" Then
                        Dim str1 As String = "update TSPL_Receipt_Adjustment_Header set is_Post = 'Y' where Adjustment_No ='" + sqlDr.Rows(0)("Adjustment_No").ToString() + "'"
                        clsDBFuncationality.ExecuteNonQuery(str1, trans)
                    End If
                    If i = 0 Then
                        RcvblAcc = New String() {strRcvblAcc, (Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) + CDec(TAdjAmt)) * -1}
                        ArrList.Add(RcvblAcc)
                    End If


                ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "O" Then
                    BankAcc = New String() {strBankAcc, sqlDr.Rows(0)("Receipt_Amount").ToString()}
                    ArrList.Add(BankAcc)
                    RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) * -1}
                    ArrList.Add(RcvblAcc)
                    Dim InvcNo As String = ""
                    Dim BalAmt As Decimal = 0.0
                    Dim drtotal As Decimal = clsCommon.myCdbl(sqlDr.Rows(0)("Receipt_Amount").ToString())
                    Dim PayAmt As Decimal = drtotal
                    strQ = " select Sale_Invoice_No as Document_No  ,Due_Date ,case when fifo_balance>0 then fifo_balance else   Balance_Amt end as  Balance_Amt from TSPL_SALE_INVOICE_HEAD" & _
                           " where Balance_Amt>0 and Cust_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "' and  fifo_knockoff='N' order by TSPL_SALE_INVOICE_HEAD.Due_Date "
                    Dim Dt1 As DataTable = New DataTable()
                    Dt1 = clsDBFuncationality.GetDataTable(strQ, trans)
                    For Each dr As DataRow In Dt1.Rows
                        InvcNo = dr.Item("Document_No").ToString()
                        BalAmt = dr.Item("Balance_Amt")
                        If drtotal > BalAmt Then
                            drtotal = drtotal - BalAmt
                            strQ = "update TSPL_SALE_INVOICE_HEAD set fifo_balance=0.00 , fifo_knockoff='Y' where Sale_Invoice_No ='" + InvcNo + "' and Cust_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "'"
                            clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                        ElseIf drtotal < BalAmt Then
                            drtotal = drtotal - BalAmt
                            strQ = "update TSPL_SALE_INVOICE_HEAD set fifo_balance=" + (drtotal * -1).ToString() + "-fifo_balance , fifo_knockoff='N' where Sale_Invoice_No ='" + InvcNo + "' and Cust_Code ='" + sqlDr.Rows(0)("Cust_Code").ToString() + "'"
                            clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                        End If
                        If drtotal < 0 Then
                            Exit For
                        End If
                    Next
                    If drtotal > 0 Then
                        strQ = "update TSPL_RECEIPT_HEADER set fifo_balance=" + drtotal.ToString() + " where Receipt_No ='" + strDocNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                    End If
                Else
                    BankAcc = New String() {strBankAcc, sqlDr.Rows(0)("Receipt_Amount").ToString()}
                    ArrList.Add(BankAcc)
                    RcvblAcc = New String() {strRcvblAcc, Convert.ToDecimal(sqlDr.Rows(0)("Receipt_Amount").ToString()) * -1}
                    ArrList.Add(RcvblAcc)
                End If

                Dim PayType As String = ""
                If sqlDr.Rows(0)("Receipt_Type").ToString() = "R" Then
                    PayType = "AR-PY"
                ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "O" Then
                    PayType = "AR-OA"
                ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "A" Then
                    PayType = "AR-DC"
                ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "P" Or sqlDr.Rows(0)("Receipt_Type").ToString() = "A" Then
                    PayType = "AR-PI"
                ElseIf sqlDr.Rows(0)("Receipt_Type").ToString() = "U" Then
                    PayType = "AR-UC"
                End If
                transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, clsCommon.myCDate(sqlDr.Rows(0)("Receipt_Post_Date")), sqlDr.Rows(0)("Entry_Desc").ToString(), PayType, "AR Payment Received", strDocNo, "", "C", sqlDr.Rows(0)("Cust_Code").ToString(), sqlDr.Rows(0)("Customer_Name").ToString(), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , sqlDr.Rows(0)("Reference").ToString(), sqlDr.Rows(0)("Narration").ToString())
                Dim strQue As String = "update TSPL_receipt_header set posted = 'Y' where receipt_no ='" + strDocNo + "' "
                clsDBFuncationality.ExecuteNonQuery(strQue, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Posted from TSPL_RECEIPT_HEADER where Receipt_No='" + strCode + "'"
            If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans)), "Y") = CompairStringResult.Equal Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim dt As DataTable = Nothing
            '' to check bank reco
            Qry = " select tspl_BankReco_Detail.Reconciliation_Id  from tspl_BankReco_Detail where tspl_BankReco_Detail.Document_No='" + strCode + "' and tspl_BankReco_Detail.Reconciliation_Status='C'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Bank Reco -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Reconciliation_Id"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = "select Reverse_Code from TSPL_BANK_REVERSE where Document_No='" + strCode + "' and Source_Type='AR'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Bank Reverse -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Reverse_Code"))
                Next
                Throw New Exception(Qry)
            End If

            ''to check applied document is used in further document whose bank reverse is not done richa agarwal
            Qry = "Select Receipt_no from TSPL_RECEIPT_HEADER WHERE Applied_Receipt ='" + strCode + "' and isnull(IsChkReverse,'')='N'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Receipt Entry -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Receipt_no"))
                Next
                Throw New Exception(Qry)
            End If


            ''to check refund type of Receipt is used in detail table of receipt 7 Nov,2019
            ''not check for auto apply documen against refund
            Dim IsAutoApplyDoc_Refund As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsAutoApplyDoc_Refund  from TSPL_RECEIPT_HEADER where IsAutoApplyDoc_Refund =1 and  Receipt_Type ='A' and Applied_Receipt in (select Document_No  from TSPL_RECEIPT_DETAIL_REFUND where Receipt_No='" & strCode & "')", trans))
            If clsCommon.CompairString(IsAutoApplyDoc_Refund, "0") = CompairStringResult.Equal Then
                Qry = "select TSPL_RECEIPT_HEADER.Receipt_no from TSPL_RECEIPT_HEADER left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No where TSPL_RECEIPT_DETAIL.Document_No ='" + strCode + "' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'')='N' "
                dt = clsDBFuncationality.GetDataTable(Qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Qry = "Current document is used in following Receipt Entry -"
                    For Each dr As DataRow In dt.Rows
                        Qry += Environment.NewLine + clsCommon.myCstr(dr("Receipt_no"))
                    Next
                    Throw New Exception(Qry)
                End If
            End If
          

            '' reverse has not done from receipt entry against Booking 
            Dim strBookingType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(TSPL_BOOKING_MATSER.Booking_Type ,'') from TSPL_BOOKING_PAYMENT_MODE_DETAIL left outer join  TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No= TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No where isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')='" & strCode & "'", trans))
            If clsCommon.myLen(strBookingType) > 0 Then
                If clsCommon.CompairString(strBookingType, "CD") = CompairStringResult.Equal Then
                    Throw New Exception("Receipt can't be reversed because it has been created against Card Sale Booking.")
                End If
            End If

            ''-------------------
            ''to check apply doc is of type auto created from refund and credit note
            Qry = "select Receipt_No  from TSPL_RECEIPT_HEADER where IsAutoApplyDoc_Refund =1 and  Receipt_Type ='A' and Receipt_No ='" & strCode & "' and isnull(IsChkReverse,'')='N'"
            Dim strApplyDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
            If clsCommon.myLen(strApplyDocNo) > 0 Then
                Throw New Exception("You cannot Reverse this Apply Document because it has been created automatically through Refund.")
            End If
            ''-------------------


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where (Source_Code like 'AR%' or Source_Code='GL-JE') AND  Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            '' TO DELETE DATA FROM JOURNAL MASTER OPENING TABLE ''richa TEC/28/11/18-000376 28 Nov,2018
            Dim VoucherNoOP As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER_OP where (Source_Code like 'AR%' or Source_Code='GL-JE') AND  Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNoOP) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS_OP where Voucher_No ='" + VoucherNoOP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER_OP where Voucher_No ='" + VoucherNoOP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            ''to delete apply document which is created against refund and credit notes automatically
            Dim strApplyDocAgainstRefundCreditNote As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipt_No  from TSPL_RECEIPT_HEADER where IsAutoApplyDoc_Refund =1 and  Receipt_Type ='A' and Applied_Receipt in (select Document_No  from TSPL_RECEIPT_DETAIL_REFUND where Receipt_No='" & strCode & "')", trans))
            If clsCommon.myLen(strApplyDocAgainstRefundCreditNote) > 0 Then
                Qry = "delete from TSPL_RECEIPT_DETAIL where receipt_no in (select Receipt_No from TSPL_RECEIPT_HEADER where IsAutoApplyDoc_Refund =1 and Receipt_Type ='A' and Applied_Receipt in (select Document_No  from TSPL_RECEIPT_DETAIL_REFUND where Receipt_No='" & strCode & "' ))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_RECEIPT_HEADER where IsAutoApplyDoc_Refund =1 and  Receipt_Type ='A' and Applied_Receipt in (select Document_No  from TSPL_RECEIPT_DETAIL_REFUND where Receipt_No='" & strCode & "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_JOURNAL_DETAILS where voucher_no in (select voucher_no from tspl_journal_master where Source_Doc_No in (select Receipt_No  from TSPL_RECEIPT_HEADER where IsAutoApplyDoc_Refund =1 and  Receipt_Type ='A' and Applied_Receipt in (select Document_No  from TSPL_RECEIPT_DETAIL_REFUND where Receipt_No='" & strCode & "'  )))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from tspl_journal_master where Source_Doc_No in (select Receipt_No  from TSPL_RECEIPT_HEADER where IsAutoApplyDoc_Refund =1 and  Receipt_Type ='A' and Applied_Receipt in (select Document_No  from TSPL_RECEIPT_DETAIL_REFUND where Receipt_No='" & strCode & "' ))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                'richa upadte balance in case of credit note when we use it on Refund ERO/16/10/19-001060
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Receipt_Type from TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strCode + "'", trans)), "F") = CompairStringResult.Equal Then
                    clsReceiptDetail_Refund.funBalanceAmtDelete_CreaditNoteRefund(strCode, trans)
                End If

            End If

            Qry = "Delete from TSPL_RECEIPT_HEADER Where Receipt_No =(Select UnApplied_No from TSPL_RECEIPT_HEADER Where Receipt_No='" + strCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_RECEIPT_HEADER set Balance_Amt=Balance_Amt+ISNULL((Select Receipt_Amount from TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strCode + "'),0) WHERE Receipt_No=(Select Applied_Receipt from TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_RECEIPT_HEADER set Posted = 'N', UnApplied_No='' where Receipt_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_RECEIPT_DETAIL set Posted = 'N' where Receipt_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            'Xtra.UpdateSaleInvoiceBalanceAmt(trans)

            Dim strReceiptType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Receipt_Type from TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strCode + "'", trans))
            If clsCommon.CompairString(strReceiptType, "A") = CompairStringResult.Equal Or clsCommon.CompairString(strReceiptType, "R") = CompairStringResult.Equal Then
                clsReceiptDettail.funBalanceAmtDelete(strCode, trans)
            End If

            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, strCode, "TSPL_RECEIPT_HEADER", "Receipt_No", "TSPL_RECEIPT_DETAIL", "Receipt_No", "TSPL_RECEIPT_DETAIL_GST", "Receipt_No", "TSPL_RECEIPT_DETAIL_Refund", "Receipt_No", "", "", "", "", "", "", trans)

            'richa upadte balance in case of credit note when we apply it as advance/onaccount
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Receipt_Type from TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strCode + "'", trans)), "A") = CompairStringResult.Equal Then
                Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_Customer_Invoice_Head where Document_No=(Select Applied_Receipt from TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strCode + "')", trans))
                Qry = " Update TSPL_Customer_Invoice_Head set Balance_Amt=Balance_Amt+ISNULL((Select Receipt_Amount from TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strCode + "'),0) WHERE Document_No=(Select Applied_Receipt from TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strCode + "') "
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            ''--------------

            ' trans.Commit()
        Catch ex As Exception
            ' trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetExchangeDetailDt(ByVal CustomerCode As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim strq As String
        strq = " SELECT TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT,TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT " & _
               " FROM TSPL_CUSTOMER_MASTER LEFT JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_MASTER.Cust_Account=TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  " & _
               " WHERE TSPL_CUSTOMER_MASTER.Cust_Code='" & CustomerCode & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        Return dt
    End Function

    Public Shared Function GetExchangeRateAmount(ByVal ReceiptNo As String, ByVal ReceiptDate As DateTime, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim strq As String
        ' strq = "SELECT ConvRate,received_amount_base_currency FROM TSPL_RECEIPT_HEADER WHERE Receipt_No='" & ReceiptNo & "'"
        strq = "select case when ConvRateRevaluation<>0 then ConvRateRevaluation else ConvRate end as ConvRate from (Select ConvRate,isnull(( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ReceiptDate), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Document_No ='" & ReceiptNo & "')xx "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        Return dt
    End Function

    Public Shared Function GetAgainstSercurityQry(ByVal strCustCode As String, ByVal strCurrentDocNo As String, ByVal strReceiptNo As String) As String
        If clsCommon.myLen(strCustCode) <= 0 Then
            Throw New Exception("Please provide customer code")
        End If
        Dim qry As String = "select Receipt_No as ReceiptNo, max(Receipt_Date) as ReceiptDate,Cust_Code as CustomerCode,max(Customer_Name) as CustomerName,sum(Amount*RI) as Amount from (" + Environment.NewLine + _
       " select Receipt_No,Receipt_Date,Cust_Code,Customer_Name,RECEIVED_AMOUNT_BASE_CURRENCY AS Amount,1 as RI,1 as Chk from TSPL_RECEIPT_HEADER where SecurityDeposit='Y' and Posted='Y'" + Environment.NewLine + _
       " and Cust_Code='" + strCustCode + "'" + Environment.NewLine
        If clsCommon.myLen(strReceiptNo) > 0 Then
            qry += " and Receipt_No='" + strReceiptNo + "'"
        End If
        qry += " union all " + Environment.NewLine + _
       " select Against_Security_Receipt_No as Receipt_No,null as Receipt_Date,TSPL_Customer_Invoice_Head.Customer_Code as Cust_Code,'' as Customer_Name,Discount_Base AS Amount,-1 as RI,0 as Chk from TSPL_Customer_Invoice_Head where Is_Against_Security_Receipt=1 and TSPL_Customer_Invoice_Head.Document_No <> '" + strCurrentDocNo + "'" + Environment.NewLine + _
       " )xx group by Receipt_No,Cust_Code having sum(Chk)>0 "
        Return qry
    End Function

    Public Shared Function isPosted(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select Posted from TSPL_RECEIPT_HEADER where Receipt_No='" + strCode + "'"
        qry = clsDBFuncationality.getSingleValue(qry, trans)
        If clsCommon.myLen(qry) <= 0 Then
            Throw New Exception("Receipt No " + strCode + "is Not exists")
        ElseIf clsCommon.CompairString(qry, "Y") = CompairStringResult.Equal Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class clsReceiptDettail
#Region "Variables"
    Public Receipt_No As String = Nothing
    Public Receipt_Line_No As Integer = 0
    Public Apply As String = Nothing
    Public Receipt_Type As String = Nothing
    Public SaleInvoice As String = Nothing
    Public Document_No As String = Nothing
    Public Document_Date As Date?
    Public Original_Amt As Double = 0.0
    Public Pending_Balance As Double = 0.0
    Public Applied_Amount As Double = 0.0
    Public Account_Code As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Comment As String = Nothing
    Public Shipment_No As String = Nothing
    Public Adjustment_Account As String = Nothing
    Public Adjustment_Cost As Double = 0.0
    Public Adjustment_No As String = Nothing
    Public TagType As String = Nothing
    Public Posted As String = "N"
    Public FilledTotal As Double = 0.0
    Public EmptyTotal As String = 0.0
    Public ConvRateOld As Decimal = 1
    Public Child_Cust_Code As String = Nothing
    Public Cost_Center_Fin_Code As String = Nothing
    Public Cost_Center_Fin_Name As String = Nothing ''Not a table column
    Public Hirerachy_Level_Code As String = Nothing
    Public Hirerachy_Level_Name As String = Nothing ''Not a table column
#End Region

    Public Shared Function SaveData(ByVal strReceiptNo As String, ByVal Arr As List(Of clsReceiptDettail), ByVal ReceiptType As String, ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsReceiptDettail In Arr
                Dim coll As New Hashtable()                
                clsCommon.AddColumnsForChange(coll, "Receipt_Line_No", intLineNo)
                clsCommon.AddColumnsForChange(coll, "Receipt_No", strReceiptNo)
                clsCommon.AddColumnsForChange(coll, "Apply", obj.Apply)
                If ReceiptType = "S" Then
                    clsCommon.AddColumnsForChange(coll, "Receipt_Type", ReceiptType)
                Else
                    clsCommon.AddColumnsForChange(coll, "Receipt_Type", obj.Receipt_Type)
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                If clsCommon.myLen(obj.Document_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Document_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Original_Amt", obj.Original_Amt)
                clsCommon.AddColumnsForChange(coll, "Pending_Balance", obj.Pending_Balance)
                clsCommon.AddColumnsForChange(coll, "Applied_Amount", obj.Applied_Amount)
                clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
                clsCommon.AddColumnsForChange(coll, "Shipment_No", obj.Shipment_No)
                clsCommon.AddColumnsForChange(coll, "Adjustment_Account", obj.Adjustment_Account)
                clsCommon.AddColumnsForChange(coll, "Adjustment_No", obj.Adjustment_No)
                'obj.Adjustment_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Adjustment_Amount  from TSPL_Receipt_Adjustment_Header WHERE    Adjustment_No = '" + obj.Adjustment_No + "' ", trans))
                clsCommon.AddColumnsForChange(coll, "Adjustment_Cost", obj.Adjustment_Cost)
                clsCommon.AddColumnsForChange(coll, "TagType", obj.TagType)
                clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
                clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRateOld)
                clsCommon.AddColumnsForChange(coll, "Child_Cust_Code", obj.Child_Cust_Code, True)
                clsCommon.AddColumnsForChange(coll, "Cost_Center_Fin_Code", obj.Cost_Center_Fin_Code, True)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Level_Code", obj.Hirerachy_Level_Code, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                coll = Nothing
                intLineNo = intLineNo + 1
                'If clsCommon.CompairString(ReceiptType, "R") = CompairStringResult.Equal Then
                '    funBalanceAmtSave(obj.Document_No, obj.Applied_Amount, trans, obj.TagType)
                'End If
            Next
        End If
        Return True
    End Function

    Public Shared Function funBalanceAmtSave(ByVal strInvoice As String, ByVal App_Amt As Decimal, ByVal trans As SqlTransaction, ByVal strDocType As String, ByVal TagType As String) As Boolean
        Try
            Dim strQ1 As String
            Dim Bal_Amt As Decimal
            If TagType = "N" Then
                strQ1 = "select Sale_Invoice_No  from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No= '" + strInvoice + "'"
                Dim SaleInvc As String = connectSql.RunScalar(trans, strQ1)
                strQ1 = "select Balance_Amt  from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No= '" + strInvoice + "'"
                Bal_Amt = Convert.ToDecimal(connectSql.RunScalar(trans, strQ1))
            ElseIf TagType = "S" Then
                strQ1 = "select invoice_No from TSPL_SCRAPINVOICE_HEAD where invoice_No= '" + strInvoice + "' "
                Dim ScrapInvc As String = connectSql.RunScalar(trans, strQ1)
                strQ1 = "select Balance_Amt  from TSPL_SCRAPINVOICE_HEAD where invoice_No= '" + strInvoice + "'"
                Bal_Amt = Convert.ToDecimal(connectSql.RunScalar(trans, strQ1))
            ElseIf TagType = "C" Then
                strQ1 = "select Document_No from TSPL_Customer_Invoice_Head where Document_No= '" + strInvoice + "' "
                Dim CusInvc As String = connectSql.RunScalar(trans, strQ1)
                strQ1 = "select Balance_Amt  from TSPL_Customer_Invoice_Head where Document_No= '" + strInvoice + "'"
                Bal_Amt = Convert.ToDecimal(connectSql.RunScalar(trans, strQ1))
            End If
            Dim Bal_AmtNew As String = String.Empty
            ' 25-Sep-2015 BM00000007925 (In Case Of Credit Note Balance Amount= Original Amount-Applied Amount ,Here AppliedAmt is coming with negative sign)
            If clsCommon.CompairString(strDocType, "C") = CompairStringResult.Equal Then
                Bal_AmtNew = Convert.ToString(Bal_Amt + App_Amt)
            Else
                Bal_AmtNew = Convert.ToString(Bal_Amt - App_Amt)
            End If

            If clsCommon.myCdbl(Bal_AmtNew) < 0 Then
                Throw New Exception("Balance Amount : " + clsCommon.myFormat(Bal_Amt) + " and Applied Amount : " + clsCommon.myFormat(App_Amt) + ".")
            End If


            Dim strQ2 As String
            If TagType = "N" Then
                strQ2 = "Update TSPL_SALE_INVOICE_HEAD set Balance_Amt ='" + Bal_AmtNew + "' where Sale_Invoice_No= '" + strInvoice + "' "
                connectSql.RunSqlTransaction(trans, strQ2)
            ElseIf TagType = "S" Then
                strQ2 = " update TSPL_SCRAPINVOICE_HEAD set Balance_Amt = '" + Bal_AmtNew + "'  where invoice_No ='" + strInvoice + "' "
                connectSql.RunSqlTransaction(trans, strQ2)
            ElseIf TagType = "C" Then
                strQ2 = " update TSPL_Customer_Invoice_Head set Balance_Amt = '" + Bal_AmtNew + "'  where Document_No ='" + strInvoice + "' "
                connectSql.RunSqlTransaction(trans, strQ2)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Sub funBalanceAmtDelete(ByVal strReceiptNo As String, ByVal trans As SqlTransaction)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TagType, Document_No, isnull(Applied_Amount,0) as AppliedAmt,Receipt_Type  from TSPL_RECEIPT_DETAIL where Receipt_No ='" + strReceiptNo + "'", trans)
            For Each dr As DataRow In dt.Rows
                Dim strQ2 As String = ""
                Dim AppliedAmt As String = clsCommon.myCstr(clsCommon.myCdbl(dr("AppliedAmt")))
                Dim DocNo As String = clsCommon.myCstr(dr("Document_No"))
                Dim TagType As String = clsCommon.myCstr(dr("TagType"))
                Dim ReceiptType As String = clsCommon.myCstr(dr("Receipt_Type"))
                If TagType = "N" Then
                    strQ2 = "Update TSPL_SALE_INVOICE_HEAD set Balance_Amt = (Balance_Amt +" + AppliedAmt + ") where Sale_Invoice_No= '" + DocNo + "' "
                ElseIf TagType = "S" Then
                    strQ2 = " update TSPL_SCRAPINVOICE_HEAD set Balance_Amt = Balance_Amt +" + AppliedAmt + "  where invoice_No ='" + DocNo + "' "
                ElseIf TagType = "C" Then
                    strQ2 = " update TSPL_Customer_Invoice_Head set Balance_Amt = Balance_Amt +" + AppliedAmt + "  where Document_No ='" + DocNo + "' "
                End If
                If ReceiptType = "F" Then
                    strQ2 = "update TSPL_RECEIPT_HEADER set Balance_Amt =Balance_Amt +" + AppliedAmt + " where Receipt_No  ='" + DocNo + "' "
                End If
                If strQ2 IsNot Nothing AndAlso clsCommon.myLen(strQ2) > 0 Then
                    connectSql.RunSqlTransaction(trans, strQ2)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Public Shared Function GetAppliedAmountInBaseCurrency(ByVal Receipt_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select coalesce(sum(Applied_Amount*ConvRateOld),0) as AppliedAmtBase from TSPL_RECEIPT_DETAIL where RECEIPT_NO='" & Receipt_No & "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class

Public Class clsReceiptDetailGST
#Region "Variable"

    ''-------------------------
    Public Receipt_No As String = String.Empty
    Public Document_Code As String = String.Empty
    Public Line_No As String = String.Empty
    Public Row_Type As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Item_Cost As Double = 0
    Public TAX1 As String = String.Empty
    Public TAX1_Amt As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public Unit_code As String = String.Empty
    Public TAX1_Rate As Double = 0
    Public tax2 As String = String.Empty
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = String.Empty
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = String.Empty
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public tax5 As String = String.Empty
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public tax6 As String = String.Empty
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public tax7 As String = String.Empty
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public tax8 As String = String.Empty
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public tax9 As String = String.Empty
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public tax10 As String = String.Empty
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Amount As Double = 0
    Public Disc_Per As Double = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public MRP As Double = 0
    Public Abatement_Per As Double = 0
    Public Abatement_Amt As Double = 0
    Public Scheme_Code As String = String.Empty
    Public Scheme_Applicable As String = String.Empty
    Public Scheme_Item As String = String.Empty
    Public FOC_Item As String = String.Empty
    Public Item_Tax As Double = 0
    Public Total_MRP_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Disc_Amt As Double = 0
    Public ActualRate As Double = 0
    Public TotalItem_Weight As Double = 0
    Public Conv_Factor As Double = 0
    Public Landing_Cost As Double = 0
    Public TAX1_Amt_Receipt As Double = 0
    Public TAX2_Amt_Receipt As Double = 0
    Public TAX3_Amt_Receipt As Double = 0
    Public TAX4_Amt_Receipt As Double = 0
    Public TAX5_Amt_Receipt As Double = 0
    Public TAX6_Amt_Receipt As Double = 0
    Public TAX7_Amt_Receipt As Double = 0
    Public TAX8_Amt_Receipt As Double = 0
    Public TAX9_Amt_Receipt As Double = 0
    Public TAX10_Amt_Receipt As Double = 0
    Public ReceiptAdvance As Double = 0
    Public ReceiptTotalTax As Double = 0
    Public ReceiptTotalAdvanceAmt As Double = 0


    ''-------------
#End Region
    Public Shared Function SaveData(ByVal strReceiptNo As String, ByVal Arr As List(Of clsReceiptDetailGST), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim intLineNo As Integer = 1
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsReceiptDetailGST In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Receipt_No", strReceiptNo)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "tax2", obj.tax2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "tax5", obj.tax5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                    clsCommon.AddColumnsForChange(coll, "tax6", obj.tax6)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                    clsCommon.AddColumnsForChange(coll, "tax7", obj.tax7)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                    clsCommon.AddColumnsForChange(coll, "tax8", obj.tax8)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                    clsCommon.AddColumnsForChange(coll, "tax9", obj.tax9)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                    clsCommon.AddColumnsForChange(coll, "tax10", obj.tax10)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                    clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                    clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                    clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                    clsCommon.AddColumnsForChange(coll, "Abatement_Per", obj.Abatement_Per)
                    clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", obj.Scheme_Applicable)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Item", obj.Scheme_Item)
                    clsCommon.AddColumnsForChange(coll, "FOC_Item", obj.FOC_Item)
                    clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                    clsCommon.AddColumnsForChange(coll, "Total_MRP_Amt", obj.Total_MRP_Amt)
                    clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                    clsCommon.AddColumnsForChange(coll, "ActualRate", obj.ActualRate)
                    clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                    clsCommon.AddColumnsForChange(coll, "TotalItem_Weight", obj.TotalItem_Weight)
                    clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                    clsCommon.AddColumnsForChange(coll, "Landing_Cost", obj.Landing_Cost)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt_Receipt", obj.TAX1_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt_Receipt", obj.TAX2_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt_Receipt", obj.TAX3_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt_Receipt", obj.TAX4_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt_Receipt", obj.TAX5_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Amt_Receipt", obj.TAX6_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Amt_Receipt", obj.TAX7_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Amt_Receipt", obj.TAX8_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Amt_Receipt ", obj.TAX9_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Amt_Receipt", obj.TAX10_Amt_Receipt)
                    clsCommon.AddColumnsForChange(coll, "ReceiptAdvance", obj.ReceiptAdvance)
                    clsCommon.AddColumnsForChange(coll, "ReceiptTotalTax", obj.ReceiptTotalTax)
                    clsCommon.AddColumnsForChange(coll, "ReceiptTotalAdvanceAmt", obj.ReceiptTotalAdvanceAmt)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_DETAIL_GST", OMInsertOrUpdate.Insert, "", trans)
                    coll = Nothing
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


Public Class clsSOAdvanceAdjustmentKnockOff
#Region "Variable"
    Public SI_No As String = String.Empty
    Public PurchaseOrder_No As String = String.Empty
    Public TAX1_Amt As Decimal = 0
    Public TAX2_Amt As Decimal = 0
    Public TAX3_Amt As Decimal = 0
    Public TAX4_Amt As Decimal = 0
    Public TAX5_Amt As Decimal = 0
    Public TAX6_Amt As Decimal = 0
    Public TAX7_Amt As Decimal = 0
    Public TAX8_Amt As Decimal = 0
    Public TAX9_Amt As Decimal = 0
    Public TAX10_Amt As Decimal = 0
#End Region

    Public Shared Function GetBalanceAdvanceAmt(ByVal strSINo As String, ByVal tran As SqlTransaction) As clsSOAdvanceAdjustmentKnockOff
        Dim strreceiptrhroughSo As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowReceiptThroughSO, clsFixedParameterCode.AllowReceiptThroughSO, tran))
        Dim SODONo As String = ""
        If strreceiptrhroughSo = 1 Then
            SODONo = " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Against_Sales_Order "
        Else
            SODONo = " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code "
        End If
        Dim obj As clsSOAdvanceAdjustmentKnockOff = Nothing
        If clsCommon.myLen(strSINo) > 0 Then
            Dim qry As String = "select distinct TSPL_RECEIPT_HEADER.Receipt_No from TSPL_RECEIPT_DETAIL_GST" + Environment.NewLine + _
            " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL_GST.Receipt_No " + Environment.NewLine + _
            " where TSPL_RECEIPT_DETAIL_GST.Document_Code in (select distinct " & SODONo & " from " + Environment.NewLine + _
            "TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " + Environment.NewLine + _
            " left outer join TSPL_SD_SHIPMENT_DETAIL  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " + Environment.NewLine + _
            " left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS " + Environment.NewLine + _
            " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" & strSINo & "')) and isnull(TSPL_RECEIPT_HEADER.Posted,'N')='N' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "Following Unposted Advance Receipt Entry Found :" + Environment.NewLine
                For Each dr As DataRow In dt.Rows
                    qry += clsCommon.myCstr(dr("Receipt_No")) + ", "
                Next
                Throw New Exception(qry)
            End If

            qry = "Delete from TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF where SI_No='" + strSINo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "insert  into TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF(SI_No,SODO_No,TAX1_Amt,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt ) " + Environment.NewLine + _
            "select '" + strSINo + "' as SI_No,SODO_No, case when TAX1_Amt<0 then 0 else TAX1_Amt end as TAX1_Amt,case when TAX2_Amt<0 then 0 else TAX2_Amt end as TAX2_Amt ,case when TAX3_Amt<0 then 0 else TAX3_Amt end as TAX3_Amt ,case when TAX4_Amt<0 then 0 else TAX4_Amt end as TAX4_Amt ,case when TAX5_Amt<0 then 0 else TAX5_Amt end as TAX5_Amt ,case when TAX6_Amt<0 then 0 else TAX6_Amt end as TAX6_Amt ,case when TAX7_Amt<0 then 0 else TAX7_Amt end as TAX7_Amt ,case when TAX8_Amt<0 then 0 else TAX8_Amt end as TAX8_Amt ,case when TAX9_Amt<0 then 0 else TAX9_Amt end as TAX9_Amt ,case when TAX10_Amt<0 then 0 else TAX10_Amt end as TAX10_Amt  from ( " + Environment.NewLine + _
            "select SODO_No, case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX1_Amt) else min(TAX1_Amt) end as TAX1_Amt," + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX2_Amt) else min(TAX2_Amt) end as TAX2_Amt, " + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX3_Amt) else min(TAX3_Amt) end as  TAX3_Amt, " + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX4_Amt) else min(TAX4_Amt) end as TAX4_Amt, " + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX5_Amt) else min(TAX5_Amt) end as TAX5_Amt, " + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX7_Amt) else min(TAX7_Amt) end as TAX6_Amt, " + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX7_Amt) else min(TAX7_Amt) end as TAX7_Amt, " + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX8_Amt) else min(TAX8_Amt) end as TAX8_Amt, " + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX9_Amt) else min(TAX9_Amt) end as TAX9_Amt , " + Environment.NewLine + _
            "case when sum(InvoiceAmt) > = sum(SoDoAmt) then min(TAX10_Amt) else min(TAX10_Amt) end as TAX10_Amt from ( " + Environment.NewLine + _
            "select sum(InvoiceAmt) as InvoiceAmt,sum(SoDOAmt) as SoDoAmt,'SI' as DocType, SODO_No, max(TAX1_Amt) as TAX1_Amt, " & _
            "max(TAX2_Amt) as TAX2_Amt,max(TAX3_Amt) as  TAX3_Amt,max (TAX4_Amt) as TAX4_Amt,max(TAX5_Amt) as TAX5_Amt,max(TAX6_Amt) as TAX6_Amt, " & _
            "max(TAX7_Amt) as TAX7_Amt,max(TAX8_Amt) as TAX8_Amt,max(TAX9_Amt) as TAX9_Amt ,max(TAX10_Amt) as TAX10_Amt,0 as chk from ( " + Environment.NewLine + _
            "select  max(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt) as InvoiceAmt,0 as SoDOAmt, 'SI' as DocType, " & SODONo & " as SODO_No,sum(TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt) as TAX1_Amt,sum(TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt) as TAX2_Amt,sum(TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt) as TAX3_Amt,sum(TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt) as TAX4_Amt,sum(TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt) as TAX5_Amt,sum(TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt) as TAX6_Amt,sum(TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt) as TAX7_Amt, " + Environment.NewLine + _
            "sum(TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt) as TAX8_Amt,sum(TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt) as TAX9_Amt,sum(TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt) as TAX10_Amt,0 as Chk " + Environment.NewLine + _
            "from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " + Environment.NewLine + _
            "left outer join TSPL_SD_SHIPMENT_DETAIL  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " + Environment.NewLine + _
            "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS " + Environment.NewLine + _
            "where  TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strSINo + "') group by  " & SODONo & " " + Environment.NewLine + _
            "union all " + Environment.NewLine + _
            "select  sum(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt) as InvoiceAmt,0 as SoDOAmt,'SI' as DocType, " & SODONo & "  as SODO_No, " + Environment.NewLine + _
            "0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt, " + Environment.NewLine + _
            "0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt,0 as Chk  " + Environment.NewLine + _
            "from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code  " + Environment.NewLine + _
            "left outer join TSPL_SD_SHIPMENT_DETAIL  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  " + Environment.NewLine + _
            "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS " + Environment.NewLine + _
            "where  TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in ('" + strSINo + "') and " + Environment.NewLine + _
            " " & SODONo & " in (select distinct   " & SODONo & "  from  " + Environment.NewLine + _
            "TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " + Environment.NewLine + _
            "left outer join TSPL_SD_SHIPMENT_DETAIL  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " + Environment.NewLine + _
            "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS  " + Environment.NewLine + _
            "where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strSINo + "')) group by   " & SODONo & "  " + Environment.NewLine + _
            "union all " + Environment.NewLine + _
            "select 0 as InvoiceAmt, sum(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Total_Amt) as SoDOAmt,'SI' as DocType, " + Environment.NewLine + _
            "" & SODONo & " as SODO_No,0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt, " + Environment.NewLine + _
            "0 as TAX6_Amt,0 as TAX7_Amt, 0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt,0 as Chk " + Environment.NewLine + _
            "from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE where " & SODONo & " in (select distinct   " & SODONo & "  from " + Environment.NewLine + _
            "TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " + Environment.NewLine + _
            "left outer join TSPL_SD_SHIPMENT_DETAIL  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " + Environment.NewLine + _
            "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS " + Environment.NewLine + _
            "where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strSINo + "')) group by   " & SODONo & "  ) aa  group by   aa.SODO_No  " + Environment.NewLine + _
            "union all " + Environment.NewLine + _
            "select  0 as InvoiceAmt,0 as SoDOAmt,'BAL'as DocType, SODO_No,sum(TAX1_Amt * RI) as TAX1_Amt,sum(TAX2_Amt * RI) as TAX2_Amt,sum(TAX3_Amt * RI) as TAX3_Amt,sum(TAX4_Amt * RI) as TAX4_Amt,sum(TAX5_Amt * RI) as TAX5_Amt,sum(TAX6_Amt * RI) as TAX6_Amt,sum(TAX7_Amt * RI) as TAX7_Amt,sum(TAX8_Amt * RI) as TAX8_Amt,sum(TAX9_Amt * RI) as TAX9_Amt,sum(TAX10_Amt * RI) as TAX10_Amt,1 as Chk   from ( " + Environment.NewLine + _
            "select 'AD' as DocType,Document_Code as SODO_No,Item_Code,1 as RI,convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX1_Amt_Receipt) as TAX1_Amt,convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX2_Amt_Receipt) as TAX2_Amt,convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX3_Amt_Receipt) as TAX3_Amt,convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX4_Amt_Receipt) as TAX4_Amt, " + Environment.NewLine + _
            "convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX5_Amt_Receipt) as TAX5_Amt,convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX6_Amt_Receipt) as TAX6_Amt, " + Environment.NewLine + _
            "convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX7_Amt_Receipt) as TAX7_Amt,convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX8_Amt_Receipt) as TAX8_Amt, " + Environment.NewLine + _
            "convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX9_Amt_Receipt) as TAX9_Amt,convert(decimal(18,2),TSPL_RECEIPT_DETAIL_GST.TAX10_Amt_Receipt) as TAX10_Amt from " + Environment.NewLine + _
            "TSPL_RECEIPT_DETAIL_GST where Document_Code in (select distinct  " & SODONo & " from " + Environment.NewLine + _
            "TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " + Environment.NewLine + _
            "left outer join TSPL_SD_SHIPMENT_DETAIL  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " + Environment.NewLine + _
            "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS " + Environment.NewLine + _
            "where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strSINo + "')) " + Environment.NewLine + _
            "union all " + Environment.NewLine + _
            "select 'PO_AD_Adj' as DocType,SODO_No,'' as Item_Code,-1 as RI,TAX1_Amt,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt from TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF " + Environment.NewLine + _
            "where SODO_No  in (select distinct  " & SODONo & " from TSPL_SD_SALE_INVOICE_HEAD left outer join " + Environment.NewLine + _
            "TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " + Environment.NewLine + _
            "left outer join TSPL_SD_SHIPMENT_DETAIL  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " + Environment.NewLine + _
            "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS " + Environment.NewLine + _
            "where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strSINo + "')) " + Environment.NewLine + _
            " )xx Group by SODO_No " + Environment.NewLine + _
            ")xxx group by SODO_No having sum(chk)>0 " + Environment.NewLine + _
            ")xxxx where ( TAX1_Amt>0 or TAX2_Amt>0 or TAX3_Amt>0 or TAX4_Amt>0 or TAX5_Amt>0 or TAX6_Amt>0 or TAX7_Amt>0 or TAX8_Amt>0 or TAX9_Amt>0 or TAX10_Amt>0) "


            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "select SI_No,sum(TAX1_Amt) as TAX1_Amt,sum(TAX2_Amt) as TAX2_Amt,sum(TAX3_Amt ) as TAX3_Amt,sum(TAX4_Amt ) as TAX4_Amt,sum(TAX5_Amt ) as TAX5_Amt,sum(TAX6_Amt ) as TAX6_Amt,sum(TAX7_Amt ) as TAX7_Amt,sum(TAX8_Amt ) as TAX8_Amt,sum(TAX9_Amt ) as TAX9_Amt,sum(TAX10_Amt ) as TAX10_Amt from TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF where SI_No='" + strSINo + "' group by SI_No"
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsSOAdvanceAdjustmentKnockOff()
                obj.SI_No = clsCommon.myCstr(dt.Rows(0)("SI_No"))
                obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
                obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
                obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
                obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
                obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
                obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
                obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
                obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
                obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
                obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            End If
            dt = Nothing
        End If
        Return obj
    End Function
End Class


Public Class clsReceiptDetail_Refund
#Region "Variables"
    Public Receipt_No As String = Nothing
    Public Receipt_Line_No As Integer = 0
    Public Apply As String = Nothing
    Public Receipt_Type As String = Nothing
    Public SaleInvoice As String = Nothing
    Public Document_No As String = Nothing
    Public Document_Date As Date?
    Public Original_Amt As Double = 0.0
    Public Pending_Balance As Double = 0.0
    Public Applied_Amount As Double = 0.0
    Public Account_Code As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Comment As String = Nothing
    Public Shipment_No As String = Nothing
    Public Adjustment_Account As String = Nothing
    Public Adjustment_Cost As Double = 0.0
    Public Adjustment_No As String = Nothing
    Public TagType As String = Nothing
    Public Posted As String = "N"
    Public FilledTotal As Double = 0.0
    Public EmptyTotal As String = 0.0
    Public ConvRateOld As Decimal = 1
    Public Child_Cust_Code As String = Nothing
    Public Cost_Center_Fin_Code As String = Nothing
    Public Cost_Center_Fin_Name As String = Nothing ''Not a table column
    Public Hirerachy_Level_Code As String = Nothing
    Public Hirerachy_Level_Name As String = Nothing ''Not a table column
#End Region

    Public Shared Function SaveData(ByVal strReceiptNo As String, ByVal Arr As List(Of clsReceiptDetail_Refund), ByVal ReceiptType As String, ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsReceiptDetail_Refund In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Receipt_Line_No", intLineNo)
                clsCommon.AddColumnsForChange(coll, "Receipt_No", strReceiptNo)
                clsCommon.AddColumnsForChange(coll, "Apply", obj.Apply)
                If ReceiptType = "S" Then
                    clsCommon.AddColumnsForChange(coll, "Receipt_Type", ReceiptType)
                Else
                    clsCommon.AddColumnsForChange(coll, "Receipt_Type", obj.Receipt_Type)
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                If clsCommon.myLen(obj.Document_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Document_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Original_Amt", obj.Original_Amt)
                clsCommon.AddColumnsForChange(coll, "Pending_Balance", obj.Pending_Balance)
                clsCommon.AddColumnsForChange(coll, "Applied_Amount", obj.Applied_Amount)
                clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
                clsCommon.AddColumnsForChange(coll, "Shipment_No", obj.Shipment_No)
                clsCommon.AddColumnsForChange(coll, "Adjustment_Account", obj.Adjustment_Account)
                clsCommon.AddColumnsForChange(coll, "Adjustment_No", obj.Adjustment_No)
                clsCommon.AddColumnsForChange(coll, "Adjustment_Cost", obj.Adjustment_Cost)
                clsCommon.AddColumnsForChange(coll, "TagType", obj.TagType)
                clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
                clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRateOld)
                clsCommon.AddColumnsForChange(coll, "Child_Cust_Code", obj.Child_Cust_Code, True)
                clsCommon.AddColumnsForChange(coll, "Cost_Center_Fin_Code", obj.Cost_Center_Fin_Code, True)
                clsCommon.AddColumnsForChange(coll, "Hirerachy_Level_Code", obj.Hirerachy_Level_Code, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_DETAIL_Refund", OMInsertOrUpdate.Insert, "", trans)
                coll = Nothing
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function
    Public Shared Sub funBalanceAmtDelete_CreaditNoteRefund(ByVal strReceiptNo As String, ByVal trans As SqlTransaction)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TagType, Document_No, isnull(Applied_Amount,0) as AppliedAmt,Receipt_Type  from TSPL_RECEIPT_DETAIL_REFUND where Receipt_No ='" + strReceiptNo + "'", trans)
            For Each dr As DataRow In dt.Rows
                Dim strQ2 As String = ""
                Dim AppliedAmt As String = clsCommon.myCstr(clsCommon.myCdbl(dr("AppliedAmt")))
                Dim DocNo As String = clsCommon.myCstr(dr("Document_No"))
                Dim TagType As String = clsCommon.myCstr(dr("TagType"))
                Dim ReceiptType As String = clsCommon.myCstr(dr("Receipt_Type"))
                If TagType = "C" Then
                    strQ2 = " update TSPL_Customer_Invoice_Head set Balance_Amt = Balance_Amt +" + AppliedAmt + "  where Document_No ='" + DocNo + "' "
                End If
                If strQ2 IsNot Nothing AndAlso clsCommon.myLen(strQ2) > 0 Then
                    connectSql.RunSqlTransaction(trans, strQ2)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
End Class


Public Class clsMakePayment
    Public Doc_Code As String = Nothing
    Public Doc_Date As DateTime
    Public Payment_Code As String = Nothing
    Public Payment_Name As String = Nothing
    Public Doc_Type As String = Nothing
    Public Zone_Code As String = Nothing
    Public Zone_Name As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Name As String = Nothing
    Public Transport_Id As String = Nothing
    Public Transport_Name As String = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Loc_Seg As String = Nothing
    Public Transporter_Receipt_Amount As Decimal
    Public Invoice_Amount As Decimal



    Public Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsMakePayment, ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
        clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code, True)
        clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type, True)
        clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Code, True)
        clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No, True)
        clsCommon.AddColumnsForChange(coll, "Transport_Id", obj.Transport_Id, True)
        clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
        clsCommon.AddColumnsForChange(coll, "Loc_Seg", obj.Loc_Seg, True)
        clsCommon.AddColumnsForChange(coll, "Transporter_Receipt_Amount", obj.Transporter_Receipt_Amount)
        clsCommon.AddColumnsForChange(coll, "Invoice_Amount", obj.Invoice_Amount)

        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Make_Payment", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Make_Payment", OMInsertOrUpdate.Update, "Doc_Code='" + obj.Doc_Code + "'", trans)
        End If

        coll = Nothing
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As clsMakePayment
        Dim obj As clsMakePayment = Nothing
        Dim qry As String = " select TSPL_Make_Payment.*,TSPL_PAYMENT_CODE.Payment_Desc,TSPL_ZONE_MASTER.Description as ZoneDescription,TSPL_ROUTE_MASTER.Route_Desc,TSPL_TRANSPORT_MASTER.Transporter_Name,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_Make_Payment
left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_Make_Payment.Payment_Code
left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_Make_Payment.Zone_Code
left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_Make_Payment.Route_No
left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_Make_Payment.Transport_Id
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Make_Payment.Cust_Code
where TSPL_Make_Payment.Doc_Code='" + strCode + "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMakePayment()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Payment_Name = clsCommon.myCstr(dt.Rows(0)("Payment_Desc"))
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Zone_Code = clsCommon.myCstr(dt.Rows(0)("Zone_Code"))
            obj.Zone_Name = clsCommon.myCstr(dt.Rows(0)("ZoneDescription"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
            obj.Transport_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Loc_Seg = clsCommon.myCstr(dt.Rows(0)("Loc_Seg"))
            obj.Transporter_Receipt_Amount = clsCommon.myCdbl(dt.Rows(0)("Transporter_Receipt_Amount"))
            obj.Invoice_Amount = clsCommon.myCdbl(dt.Rows(0)("Invoice_Amount"))
        End If
        Return obj
    End Function

End Class
