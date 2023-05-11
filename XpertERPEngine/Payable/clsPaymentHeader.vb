'==================================================================================
'-------Created By--Pankaj Kuamar
'-------Created Date--08/03/2013:01:00PM
'-------Table Used--TSPL_PAYMENT_HEADER, TSPL_PAYMENT_DETAILS
'==================================================================================
Imports System.Data.SqlClient
Imports common


Public Class clsPaymentHeader
#Region "Variables"
    Public Vendor_Bank_Code As String = Nothing
    Public Vendor_Bank_Name As String = Nothing
    Public Vendor_IFSC_Code As String = Nothing
    Public Vendor_Branch_Name As String = Nothing
    Public Vendor_Bank_ACNo As String = Nothing
    Public isFarmerLoanPayment As Integer = 0
    Public memorndmamt As String = Nothing
    Public Payment_No As String = Nothing
    Public Payment_Date As DateTime
    Public Payment_Post_Date As Date?
    Public Bank_Code As String = Nothing
    Public Payment_Type As String = Nothing
    Public DateAndTime As DateTime?
    Public TapalNo As String = String.Empty
    Public GSTRegistered As Integer = 0
    Public isReceipt As Integer = 0
    Public Vendor_Code As String = Nothing
    Public Employee_Type As String = String.Empty
    Public Employee_Advance_Type As String = String.Empty
    Public WaveOFFBankCharges As String = String.Empty
    Public Vendor_Name As String = Nothing
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
    Public MP_Code_For_Advance As String = Nothing
    Public Bank_Charges_Ac As String = Nothing
    Public Bank_Charges As Double = 0.0
    Public ArrTr As List(Of clsPaymentDetail) = Nothing
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
    Public Against_VSP_Asset_Lost As String = Nothing
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

    Public Interest_Rate As Decimal = 0
    Public No_Of_EMI As Decimal = 0
    Public ArrTrGST As List(Of clsPaymentDetailGST) = Nothing

    Public PurchaseOrder_No_GST As String = String.Empty
    Public Tax_Group As String = String.Empty
    Public Tax_Amount_Advance As Double = 0.0
    Public PurchaseOrder_Add_Amount As Double = 0.0
    Public PurchaseOrder_Amount As Double = 0.0
    Public PO_Location_Code As String = String.Empty
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

    '' Bank Charges 
    Public Tax_Group_BankCharges As String = String.Empty
    Public Bank_Charges_Tax As Double = 0
    Public objBCT As New List(Of clsPaymentBankChargesTax)

    Public Against_Salary_Generation_Code As String
    Public Against_Incentive_Detail_No As String
#End Region


    Public Function SaveData(ByVal obj As clsPaymentHeader, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal isPosted As Boolean = False) As Boolean
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
    Public Function SaveDataWithPaymentNo(ByVal obj As clsPaymentHeader, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal isPosted As Boolean = False) As clsPaymentHeader
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

    Public Function SaveData1(ByVal obj As clsPaymentHeader, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal CreateNewDocumentNoWithExistingDocumentNo As String = Nothing) As Boolean
        If trans Is Nothing OrElse trans.Connection Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
        End If
        Dim isSaved As Boolean = True
        Try


            '--------Checks Whertrher Transaction Is Locked Or Not------------UDL/24/07/18-000206 richa 
            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans)
            Dim strAllowtoUnlockTransactionsforSetOff As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoUnlockTransactionsforSetOff, clsFixedParameterCode.AllowtoUnlockTransactionsforSetOff, trans))
            If clsCommon.CompairString(strAllowtoUnlockTransactionsforSetOff, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
            Else
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, obj.Payment_Date, trans)
            End If
            '----------------------------------------------------------------
            If clsCommon.myLen(obj.Payment_No) > 0 Then
                Dim isPosted As Integer = clsDBFuncationality.getSingleValue("Select Posted from TSPL_PAYMENT_HEADER Where Payment_No='" + obj.Payment_No + "'", trans)
                If isPosted = 1 Then
                    Throw New Exception("Document already posted")
                End If
            End If

            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Payment_No, "TSPL_PAYMENT_HEADER", "Payment_No", "TSPL_PAYMENT_DETAIL", "Payment_No", "TSPL_PAYMENT_BANK_CHARGES_TAX", "Payment_No", "TSPL_PAYMENT_DETAIL_GST", "Payment_No", "", "", "", "", "", "", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Payment_No, "TSPL_REMITTANCE", "Document_No", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Payment_No, "TSPL_bank_book", "SOURCEDOC_NO", trans)
            End If

            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_REMITTANCE Where Document_No='" + obj.Payment_No + "'", trans)

            Dim qry As String = "DELETE From TSPL_PAYMENT_DETAIL WHERE Payment_No ='" + obj.Payment_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE From TSPL_PAYMENT_DETAIL_GST WHERE Payment_No ='" + obj.Payment_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE From TSPL_PAYMENT_BANK_CHARGES_TAX WHERE Payment_No ='" + obj.Payment_No + "'"
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
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
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
            clsCommon.AddColumnsForChange(coll, "GSTRegistered", obj.GSTRegistered)
            clsCommon.AddColumnsForChange(coll, "isReceipt", obj.isReceipt)
            clsCommon.AddColumnsForChange(coll, "Employee_Type", obj.Employee_Type, True)
            clsCommon.AddColumnsForChange(coll, "Employee_Advance_Type", obj.Employee_Advance_Type, True)
            clsCommon.AddColumnsForChange(coll, "WaveOFFBankCharges", obj.WaveOFFBankCharges)

            clsCommon.AddColumnsForChange(coll, "Vendor_Bank_Code", obj.Vendor_Bank_Code, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_IFSC_Code", obj.Vendor_IFSC_Code, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Bank_Name", obj.Vendor_Bank_Name, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Branch_Name", obj.Vendor_Branch_Name, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Bank_ACNo", obj.Vendor_Bank_ACNo, True)

            clsCommon.AddColumnsForChange(coll, "Account_Payee", obj.Account_Payee)
            clsCommon.AddColumnsForChange(coll, "Is_Security", obj.Is_Security)
            clsCommon.AddColumnsForChange(coll, "Account_Payee_Name", obj.Account_Payee_Name)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_PP_Detail_No", obj.Against_PP_Detail_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_PP_Detail_No_Advance_Payment", obj.Against_PP_Detail_No_Advance_Payment, True)
            clsCommon.AddColumnsForChange(coll, "Loan_Code", obj.Loan_Code, True)
            clsCommon.AddColumnsForChange(coll, "Against_TDS_PAYMENT_No", obj.Against_TDS_PAYMENT_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_GL_Code", obj.Location_GL_Code, True)
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
            obj.Vendor_Account_Set = clsDBFuncationality.getSingleValue("select Vendor_Account  from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans)
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
            clsCommon.AddColumnsForChange(coll, "Tax_Group_BankCharges", obj.Tax_Group_BankCharges, True)
            clsCommon.AddColumnsForChange(coll, "Bank_Charges_Tax", obj.Bank_Charges_Tax)
            clsCommon.AddColumnsForChange(coll, "TapalNo", obj.TapalNo, True)
            If clsCommon.myLen(obj.DateAndTime) > 0 Then
                clsCommon.AddColumnsForChange(coll, "DateAndTime", clsCommon.GetPrintDate(obj.DateAndTime, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "DateAndTime", Nothing, True)
            End If
            If (clsCommon.CompairString(obj.Payment_Type, "PY") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal) Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select s.Payable_Account,s.Discount_Account,s.Advance_Against_Imprest ,s.Advance_Against_Travelling,s.Employee_Salary  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "TD") = CompairStringResult.Equal Then
                        obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "T") = CompairStringResult.Equal Then
                        obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "S") = CompairStringResult.Equal Then
                        obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Employee_Salary"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "I") = CompairStringResult.Equal Then
                        obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                    Else
                        obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    End If

                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please enter Vendor Account in Vendor Account Set for Vendor " + clsCommon.myCstr(obj.Vendor_Code))
                    End If
                End If
                If clsCommon.myLen(obj.Location_GL_Code) <= 0 Then
                    obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "')", trans))
                End If

                obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
            ElseIf clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal Then
                If clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(obj.Employee_Type, "") <> CompairStringResult.Equal Or clsCommon.CompairString(obj.Employee_Advance_Type, "") <> CompairStringResult.Equal Then
                        If clsCommon.CompairString(obj.Employee_Type, "") <> CompairStringResult.Equal Then
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select s.Payable_Account,s.Discount_Account,s.Advance_Against_Imprest ,s.Advance_Against_Travelling,s.Employee_Salary  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "TD") = CompairStringResult.Equal Then
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "T") = CompairStringResult.Equal Then
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "S") = CompairStringResult.Equal Then
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Employee_Salary"))
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "I") = CompairStringResult.Equal Then
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                                Else
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                End If
                            End If
                        Else
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select s.Advance_Against_Imprest ,s.Advance_Against_Travelling,s.Advance_Against_Salary,Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Advance_Type), "T") = CompairStringResult.Equal Then
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Advance_Type), "S") = CompairStringResult.Equal Then
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Salary"))
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Advance_Type), "I") = CompairStringResult.Equal Then
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                                Else
                                    obj.Debit_Account = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                End If
                            End If
                        End If
                    Else
                        obj.Debit_Account = clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + obj.Vendor_Code + "'", trans)
                    End If
                Else
                    obj.Debit_Account = clsDBFuncationality.getSingleValue("select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + obj.Vendor_Code + "'", trans)
                End If

                '' Anubhooti 27-Mar-2015 (Advance/On-Account: Debit Amount should be Advance_Against_Salary instead of advance account if Advance_Against_Salary is checked)
                If clsCommon.myCdbl(obj.Advance_Against_Salary) = 1 AndAlso (clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal) Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Salary,'') AS Advance_Against_Salary  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + obj.Vendor_Code + "'", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill advance against salary account on vendor account set")
                    End If
                End If

                '' changes by richa agarwal against ticket no 
                If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(obj.Employee_Advance_Type, "T") = CompairStringResult.Equal Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Travelling,'') AS Advance_Against_Travelling from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill advance against travelling account on vendor account set.")
                    End If
                ElseIf (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(obj.Employee_Advance_Type, "I") = CompairStringResult.Equal Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Imprest,'') AS Advance_Against_Imprest from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill advance against imprest account on vendor account set.")
                    End If
                End If

                '============Commented By Rohit After Talked with Anubhuti and Amit Sir(09-Jul-2015)=====
                'obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
                '=====================================================================================
                '' Anubhooti 31-Mar-2015 (Receipt/Security Refund :If security is checked then security account will go on GL)
                If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Is_Security) = 1 Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.SECURITY_ACCOUNT,'') AS SECURITY_ACCOUNT from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + obj.Vendor_Code + "' ", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill security account on vendor account set.")
                    End If
                Else
                    '============Add By Rohit After Talked with Anubhuti and Amit Sir(09-Jul-2015)=====
                    obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
                    '=======================================================
                End If
            End If
            '' richa agarwal changes done against ticket no KDI/18/04/18-000263 on 19 Apr,2018
            If clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                obj.Credit_Account = clsDBFuncationality.getSingleValue("Select Debit_Account from tspl_payment_header where payment_no='" + obj.Applied_Payment + "' union all Select Vendor_Control_AC from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" + obj.Applied_Payment + "' ", trans)
            Else
                obj.Credit_Account = clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + obj.Bank_Code + "'", trans)
            End If
            If clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal Or (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal AndAlso obj.isReceipt = 1) Then
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
            clsCommon.AddColumnsForChange(coll, "isFarmerLoanPayment", obj.isFarmerLoanPayment)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            If clsCommon.CompairString(obj.Payment_Type, "MI") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Loadout_No", obj.Loadout_No)
                clsCommon.AddColumnsForChange(coll, "MP_Code_For_Advance", obj.MP_Code_For_Advance, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Bank_Charges", obj.Bank_Charges)
            If obj.Bank_Charges <> 0 Then
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
            clsCommon.AddColumnsForChange(coll, "Against_VSP_Asset_Lost", obj.Against_VSP_Asset_Lost, True)
            clsCommon.AddColumnsForChange(coll, "memorandum_amt", obj.memorndmamt)
            clsCommon.AddColumnsForChange(coll, "is_Opening", IIf(obj.is_Opening, 1, 0))


            clsCommon.AddColumnsForChange(coll, "Interest_Rate", obj.Interest_Rate)
            clsCommon.AddColumnsForChange(coll, "No_Of_EMI", obj.No_Of_EMI)

            ''RICHA 
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No_GST", obj.PurchaseOrder_No_GST, True)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Amount_Advance", obj.Tax_Amount_Advance)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Amount", obj.PurchaseOrder_Amount)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Add_Amount", obj.PurchaseOrder_Add_Amount)
            clsCommon.AddColumnsForChange(coll, "PO_Location_Code", obj.PO_Location_Code)
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
            clsCommon.AddColumnsForChange(coll, "Against_Salary_Generation_Code", obj.Against_Salary_Generation_Code, True)
            clsCommon.AddColumnsForChange(coll, "Against_Incentive_Detail_No", obj.Against_Incentive_Detail_No, True)

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

                Dim strPaymentType As String = clsDocType.Payment
                If clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PaymentReceiptTypeRunReceiptCounter, clsFixedParameterCode.PaymentReceiptTypeRunReceiptCounter, trans)) = 1 Then
                    strPaymentType = clsDocType.Receipt
                End If

                ''richa agarwal 31 Aug,2018
                If clsCommon.myLen(CreateNewDocumentNoWithExistingDocumentNo) > 0 And isNewEntry = True Then
                    obj.Payment_No = CreateNewDocumentNoWithExistingDocumentNo
                Else
                    If clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseApplyDocSeriesForPayment, clsFixedParameterCode.AllowUseApplyDocSeriesForPayment, trans)), "1") = CompairStringResult.Equal Then
                        obj.Payment_No = clsERPFuncationality.GetNextCode(trans, obj.Payment_Date, strPaymentType, clsDocTransactionType.ApplyDoc, BankAcc, True)
                    Else
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
                            Throw New Exception("Please set the Bank Type for Bank SETTLEMENT")
                        End If
                    End If

                End If
                ''------------

                clsCommon.AddColumnsForChange(coll, "Payment_No", obj.Payment_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_HEADER", OMInsertOrUpdate.Update, "Payment_No='" + obj.Payment_No + "'", trans)
            End If
            If clsCommon.myLen(obj.Document_No) > 0 Then
                qry = "Update TSPL_PJC_EXPENSE_HEADER set Payment_No='" & obj.Payment_No & "',Posted='Y' ,Posting_Date='" & clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy hh:mm tt") & "' WHERE Document_No ='" + obj.Document_No + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            '----------------Remmitance ENtry------------------------
            If clsCommon.myCdbl(obj.TDS_Amount) > 0 Then
                If clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Then
                    ''KDI/21/05/18-000327
                    objRemittance.Document_Date = obj.Payment_Date
                    clsRemittance.SaveData(objRemittance, obj.Payment_No, obj.Location_GL_Code, trans)
                End If
            End If
            '--------------------------------------------------------
            isSaved = isSaved AndAlso clsPaymentDetail.SaveData(obj.Payment_No, obj.ArrTr, trans)
            isSaved = isSaved AndAlso clsPaymentDetailGST.SaveData(obj.Payment_No, obj.ArrTrGST, trans)
            isSaved = isSaved AndAlso clsPaymentBankChargesTax.SaveData(obj.Payment_No, obj.objBCT, trans)
            '' update currency loss and gain in case of payment type entr
            'If obj.ConvRate <> 1 Then
            '    If obj.Payment_Type = "PY" Then
            '        Dim obj1 As New clsPaymentHeader
            '        Dim diff As Double = 0.0
            '        diff = obj.PAYMENT_AMOUNT_BASE_CURRENCY - clsPaymentDetail.GetAppliedAmountInBaseCurrency(obj.Payment_No, trans)
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
            '        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_PAYMENT_HEADER", OMInsertOrUpdate.Update, "Payment_No='" + obj.Payment_No + "'", trans)
            '    End If
            'Else
            '    Dim coll1 As New Hashtable()
            '    clsCommon.AddColumnsForChange(coll1, "EXCHANGE_LOSS_AMT", 0)
            '    clsCommon.AddColumnsForChange(coll1, "EXCHANGE_GAIN_AMT", 0)
            'End If

            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Payment_No, obj.arrCustomFields, trans)
            '' check for bankBookentry against ticket No:BM00000008469
            If clsCommon.CompairString(obj.Payment_Type, "AD") <> CompairStringResult.Equal Then
                If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal) And (clsCommon.CompairString(clsCommon.myCstr(obj.Is_Security), "1") = CompairStringResult.Equal) AndAlso clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, Description,103) from TSPL_FIXED_PARAMETER where code ='ERPStartDate' and Type ='ERPStartDate'", trans)) > clsCommon.myCDate(obj.Payment_Date) Then
                Else
                    qry = "select count(ID) as Rec from TSPL_BANK_BOOK where SOURCEDOC_NO='" & obj.Payment_No & "' and DocType='Payment'"
                    Dim totalRec As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                    If totalRec <= 0 Then
                        Throw New Exception("Payment No-" & obj.Payment_No & " could not sent to Bank Book")
                    End If
                End If
            End If

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetFarmerAccountQry(ByVal ISFarmerLoanPayment As Boolean) As String
        Dim qry As String = "select " & IIf(ISFarmerLoanPayment = True, "isnull(Discount_Account,'')", "isnull(Advance_Account,'')") & " from tspl_vendor_account_set where isFarmer=1 "
        Return qry
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As clsPaymentHeader
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsPaymentHeader
        Dim obj As New clsPaymentHeader()


        Dim qry As String = "SELECT TSPL_PAYMENT_HEADER.ConvRateOld,TSPL_PAYMENT_HEADER.Vendor_Bank_Code, TSPL_PAYMENT_HEADER.Vendor_IFSC_Code, TSPL_PAYMENT_HEADER.Vendor_Bank_Name, TSPL_PAYMENT_HEADER.Vendor_Branch_Name, TSPL_PAYMENT_HEADER.Vendor_Bank_ACNo,isnull (TSPL_PAYMENT_HEADER.isReceipt,0) as isReceipt,  TSPL_PAYMENT_HEADER.TapalNo,TSPL_PAYMENT_HEADER.DateAndTime,TSPL_PAYMENT_HEADER.Against_Salary_Generation_Code, isnull(TSPL_PAYMENT_HEADER.WaveOFFBankCharges,'N') as WaveOFFBankCharges,TSPL_PAYMENT_HEADER.Employee_Advance_Type,TSPL_PAYMENT_HEADER.Employee_Type,TSPL_PAYMENT_HEADER.GSTRegistered,TSPL_PAYMENT_HEADER.Interest_Rate,TSPL_PAYMENT_HEADER.No_Of_EMI,TSPL_PAYMENT_HEADER.memorandum_amt,TSPL_PAYMENT_HEADER.Payment_No,  TSPL_PAYMENT_HEADER.Payment_Date,  TSPL_PAYMENT_HEADER.Payment_Post_Date,  " &
        " TSPL_PAYMENT_HEADER.Bank_Code, TSPL_PAYMENT_HEADER.Payment_Type, TSPL_PAYMENT_HEADER.Vendor_Code, TSPL_PAYMENT_HEADER.Vendor_Name, " &
        " TSPL_PAYMENT_HEADER.Remit_To, TSPL_PAYMENT_HEADER.Entry_Desc, TSPL_PAYMENT_HEADER.Reference, TSPL_PAYMENT_HEADER.Narration, " &
        " TSPL_PAYMENT_HEADER.Payment_Code, TSPL_PAYMENT_HEADER.Cheque_No, TSPL_PAYMENT_HEADER.Cheque_Date, TSPL_PAYMENT_HEADER.PDC_Cheque, TSPL_PAYMENT_HEADER.Payment_Amount, " &
        " TSPL_PAYMENT_HEADER.Vendor_Account_Set, TSPL_PAYMENT_HEADER.TDS_Amount, TSPL_PAYMENT_HEADER.Total_Prepayment, " &
        " TSPL_PAYMENT_HEADER.Apply_By, TSPL_PAYMENT_HEADER.Apply_To, TSPL_PAYMENT_HEADER.Posted, TSPL_PAYMENT_HEADER.Level1_User_code, " &
        " TSPL_PAYMENT_HEADER.Level2_User_code, TSPL_PAYMENT_HEADER.Level3_User_code, TSPL_PAYMENT_HEADER.Level4_User_code, " &
        " TSPL_PAYMENT_HEADER.Level5_User_code, TSPL_PAYMENT_HEADER.Comp_Code, TSPL_PAYMENT_HEADER.Debit_Account, TSPL_PAYMENT_HEADER.Credit_Account, " &
        " TSPL_PAYMENT_HEADER.Balance_Amt, TSPL_PAYMENT_HEADER.Total_Applied_Amount, TSPL_PAYMENT_HEADER.Total_Security_Amount, TSPL_PAYMENT_HEADER.Transport_Id, TSPL_PAYMENT_HEADER.FIFO_Balance, " &
        " TSPL_PAYMENT_HEADER.QuickEntryNo, TSPL_PAYMENT_HEADER.LoadOutNo, TSPL_PAYMENT_HEADER.Salesman_Code, TSPL_PAYMENT_HEADER.Salesman_Name, " &
        " TSPL_PAYMENT_HEADER.Route_NO, TSPL_PAYMENT_HEADER.Route_Description, TSPL_PAYMENT_HEADER.Location_Code, " &
        " TSPL_PAYMENT_HEADER.Location_Description, TSPL_PAYMENT_HEADER.IsRecoCleared, TSPL_PAYMENT_HEADER.IsChkReverse,Loadout_No,TSPL_PAYMENT_HEADER.MP_Code_For_Advance, TSPL_PAYMENT_HEADER.Bank_Charges, TSPL_PAYMENT_HEADER.Bank_Charges_Ac,CFormRecd,CForm_InvoiceNo,TSPL_PAYMENT_HEADER.CURRENCY_CODE,TSPL_PAYMENT_HEADER.CONVRATE,TSPL_PAYMENT_HEADER.APPLICABLEFROM, " &
        " TSPL_PAYMENT_HEADER.PAYMENT_AMOUNT_BASE_CURRENCY,EMP_CODE,PROJECT_CODE,TSPL_PAYMENT_HEADER.CHECK_PRINT,TSPL_PAYMENT_HEADER.CHECK_CODE, TSPL_PAYMENT_HEADER.Applied_Payment,TSPL_PAYMENT_HEADER.Against_VSP_Asset_Lost,TSPL_PAYMENT_HEADER.Account_Payee,TSPL_PAYMENT_HEADER.PurchaseOrder_No,TSPL_PAYMENT_HEADER.Loan_Code,ISNULL(TSPL_PAYMENT_HEADER.Location_GL_Code,'') As Location_GL_Code,TSPL_PAYMENT_HEADER.Is_Security,TSPL_PAYMENT_HEADER.Account_Payee_Name,ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0) AS Advance_Against_Salary,TSPL_PAYMENT_HEADER.is_Opening,TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT,TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT,TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_ACCOUNT,TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_ACCOUNT,TSPL_PAYMENT_HEADER.Against_PP_Detail_No,TSPL_PAYMENT_HEADER.Against_PP_Detail_No_Advance_Payment,TSPL_PAYMENT_HEADER.Against_TDS_PAYMENT_No, " &
        " TSPL_PAYMENT_HEADER.TAX1,TSPL_PAYMENT_HEADER.TAX1_Rate,TSPL_PAYMENT_HEADER.TAX1_Amt,TSPL_PAYMENT_HEADER.TAX1_Base_Amt,TSPL_PAYMENT_HEADER.TAX2,TSPL_PAYMENT_HEADER.TAX2_Rate,TSPL_PAYMENT_HEADER.TAX2_Amt,TSPL_PAYMENT_HEADER.TAX2_Base_Amt," &
        " TSPL_PAYMENT_HEADER.TAX3,TSPL_PAYMENT_HEADER.TAX3_Rate,TSPL_PAYMENT_HEADER.TAX3_Amt,TSPL_PAYMENT_HEADER.TAX3_Base_Amt,TSPL_PAYMENT_HEADER.TAX4,TSPL_PAYMENT_HEADER.TAX4_Rate," &
        " TSPL_PAYMENT_HEADER.TAX4_Amt,TSPL_PAYMENT_HEADER.TAX4_Base_Amt,TSPL_PAYMENT_HEADER.TAX5,TSPL_PAYMENT_HEADER.TAX5_Rate,TSPL_PAYMENT_HEADER.TAX5_Amt,TSPL_PAYMENT_HEADER.TAX5_Base_Amt," &
        " TSPL_PAYMENT_HEADER.TAX6,TSPL_PAYMENT_HEADER.TAX6_Rate,TSPL_PAYMENT_HEADER.TAX6_Amt,TSPL_PAYMENT_HEADER.TAX6_Base_Amt,TSPL_PAYMENT_HEADER.tax7, TSPL_PAYMENT_HEADER.TAX7_Rate, " &
        " TSPL_PAYMENT_HEADER.TAX7_Amt, TSPL_PAYMENT_HEADER.TAX7_Base_Amt, TSPL_PAYMENT_HEADER.TAX8, TSPL_PAYMENT_HEADER.TAX8_Rate, TSPL_PAYMENT_HEADER.TAX8_Amt," &
        " TSPL_PAYMENT_HEADER.TAX8_Base_Amt, TSPL_PAYMENT_HEADER.TAX9, TSPL_PAYMENT_HEADER.TAX9_Rate, TSPL_PAYMENT_HEADER.TAX9_Amt, TSPL_PAYMENT_HEADER.TAX9_Base_Amt, " &
        " TSPL_PAYMENT_HEADER.TAX10, TSPL_PAYMENT_HEADER.TAX10_Rate, TSPL_PAYMENT_HEADER.TAX10_Amt, TSPL_PAYMENT_HEADER.TAX10_Base_Amt," &
        " TSPL_PAYMENT_HEADER.PurchaseOrder_No_GST,TSPL_PAYMENT_HEADER.Tax_Group,TSPL_PAYMENT_HEADER.Tax_Amount_Advance,TSPL_PAYMENT_HEADER.PurchaseOrder_Amount,TSPL_PAYMENT_HEADER.PurchaseOrder_Add_Amount,TSPL_PAYMENT_HEADER.PO_Location_Code,TSPL_PAYMENT_HEADER.Tax_Group_BankCharges,TSPL_PAYMENT_HEADER.Bank_Charges_Tax,TSPL_PAYMENT_HEADER.isFarmerLoanPayment " &
        " FROM TSPL_PAYMENT_HEADER " &
        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code" &
        " where  2=2"
        Dim whrclas As String = " "
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(trans)
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, trans)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrclas = " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, trans)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                whrclas = " AND TSPL_PAYMENT_HEADER.Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        qry += " " + whrclas + ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PAYMENT_HEADER.Payment_No=(select MIN(Payment_No) from TSPL_PAYMENT_HEADER TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code Where 1=1 " + whrclas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_PAYMENT_HEADER.Payment_No=(select Max(Payment_No) from TSPL_PAYMENT_HEADER TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code Where  1=1 " + whrclas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_PAYMENT_HEADER.Payment_No=(select Min(Payment_No) from TSPL_PAYMENT_HEADER TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code where Payment_No > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_PAYMENT_HEADER.Payment_No=(select Max(Payment_No) from TSPL_PAYMENT_HEADER TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code where Payment_No < '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_PAYMENT_HEADER.Payment_No='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPaymentHeader()
            obj.Payment_No = clsCommon.myCstr(dt.Rows(0)("Payment_No"))
            obj.Payment_Date = clsCommon.myCDate(dt.Rows(0)("Payment_Date"))
            If obj.Payment_Post_Date.HasValue Then
                obj.Payment_Post_Date = clsCommon.myCDate(dt.Rows(0)("Payment_Post_Date"))
            End If
            obj.ConvRateOld = clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
            obj.GSTRegistered = clsCommon.myCdbl(dt.Rows(0)("GSTRegistered"))
            obj.isFarmerLoanPayment = clsCommon.myCdbl(dt.Rows(0)("isFarmerLoanPayment"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Payment_Type = clsCommon.myCstr(dt.Rows(0)("Payment_Type"))
            obj.memorndmamt = clsCommon.myCstr(dt.Rows(0)("memorandum_amt"))
            obj.Employee_Type = clsCommon.myCstr(dt.Rows(0)("Employee_Type"))
            obj.Employee_Advance_Type = clsCommon.myCstr(dt.Rows(0)("Employee_Advance_Type"))
            obj.WaveOFFBankCharges = clsCommon.myCstr(dt.Rows(0)("WaveOFFBankCharges"))

            obj.Vendor_Bank_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Bank_Code"))
            obj.Vendor_Bank_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Bank_Name"))
            obj.Vendor_IFSC_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_IFSC_Code"))
            obj.Vendor_Branch_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Branch_Name"))
            obj.Vendor_Bank_ACNo = clsCommon.myCstr(dt.Rows(0)("Vendor_Bank_ACNo"))

            obj.Account_Payee = clsCommon.myCdbl(dt.Rows(0)("Account_Payee"))
            obj.PurchaseOrder_No = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
            obj.Loan_Code = clsCommon.myCstr(dt.Rows(0)("Loan_Code"))
            obj.Against_TDS_PAYMENT_No = clsCommon.myCstr(dt.Rows(0)("Against_TDS_PAYMENT_No"))
            obj.Is_Security = clsCommon.myCdbl(dt.Rows(0)("Is_Security"))
            obj.Location_GL_Code = clsCommon.myCstr(dt.Rows(0)("Location_GL_Code"))
            If IsDBNull(dt.Rows(0)("DateAndTime")) = True Then
                obj.DateAndTime = Nothing
            Else
                obj.DateAndTime = clsCommon.myCstr(dt.Rows(0)("DateAndTime"))
            End If
            obj.TapalNo = clsCommon.myCstr(dt.Rows(0)("TapalNo"))
            obj.Account_Payee_Name = clsCommon.myCstr(dt.Rows(0)("Account_Payee_Name"))
            obj.Advance_Against_Salary = clsCommon.myCdbl(dt.Rows(0)("Advance_Against_Salary"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Remit_To = clsCommon.myCstr(dt.Rows(0)("Remit_To"))
            obj.Entry_Desc = clsCommon.myCstr(dt.Rows(0)("Entry_Desc"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Narration = clsCommon.myCstr(dt.Rows(0)("Narration"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            obj.isReceipt = clsCommon.myCdbl(dt.Rows(0)("isReceipt"))
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
            obj.MP_Code_For_Advance = clsCommon.myCstr(dt.Rows(0)("MP_Code_For_Advance"))
            obj.Bank_Charges_Ac = clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))
            obj.Bank_Charges = clsCommon.myCdbl(dt.Rows(0)("Bank_Charges"))
            obj.Against_PP_Detail_No = clsCommon.myCstr(dt.Rows(0)("Against_PP_Detail_No"))
            obj.Against_PP_Detail_No_Advance_Payment = clsCommon.myCstr(dt.Rows(0)("Against_PP_Detail_No_Advance_Payment"))
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
                obj.Balance_Amt = clsPaymentHeader.GetBalance(obj.Applied_Payment, obj.Payment_No, trans)
            End If
            obj.Against_VSP_Asset_Lost = clsCommon.myCstr(dt.Rows(0)("Against_VSP_Asset_Lost"))
            obj.CHECK_PRINT = clsCommon.myCdbl(dt.Rows(0)("CHECK_PRINT"))
            obj.CHECK_CODE = clsCommon.myCstr(dt.Rows(0)("CHECK_CODE"))

            obj.Interest_Rate = clsCommon.myCdbl(dt.Rows(0)("Interest_Rate"))
            obj.No_Of_EMI = clsCommon.myCdbl(dt.Rows(0)("No_Of_EMI"))

            'clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.is_Opening = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Opening")) = 1, True, False)

            ''RICHA 
            obj.PurchaseOrder_Amount = clsCommon.myCdbl(dt.Rows(0)("PurchaseOrder_Amount"))
            obj.PurchaseOrder_Add_Amount = clsCommon.myCdbl(dt.Rows(0)("PurchaseOrder_Add_Amount"))
            obj.Tax_Amount_Advance = clsCommon.myCdbl(dt.Rows(0)("Tax_Amount_Advance"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.PurchaseOrder_No_GST = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No_GST"))
            obj.PO_Location_Code = clsCommon.myCstr(dt.Rows(0)("PO_Location_Code"))
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

            obj.Tax_Group_BankCharges = clsCommon.myCstr(dt.Rows(0)("Tax_Group_BankCharges"))
            obj.Bank_Charges_Tax = clsCommon.myCdbl(dt.Rows(0)("Bank_Charges_Tax"))
            ''------
            obj.Against_Salary_Generation_Code = clsCommon.myCstr(dt.Rows(0)("Against_Salary_Generation_Code"))

            qry = "select * from (SELECT  case when TSPL_Payment_Header.Payment_Type ='SR' then  convert(varchar,TSPL_Payment_Header.Payment_Date,103) else convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) end as Payment_Date,TSPL_PAYMENT_DETAIL.Payment_No, TSPL_PAYMENT_DETAIL.Payment_Line_No, TSPL_PAYMENT_DETAIL.Apply, " & _
          " TSPL_PAYMENT_DETAIL.Payment_Type, TSPL_PAYMENT_DETAIL.Document_No, TSPL_PAYMENT_DETAIL.Vendor_Invoice_No, " & _
             " Case When ISNULL(Against_POInvoice_No,'')<>'' Then Against_POInvoice_No When ISNULL(Against_PurchaseReturn_No,'')<> '' Then Against_PurchaseReturn_No Else TSPL_PAYMENT_DETAIL.Document_No End as PurchaseInvoice," & _
             " TSPL_PAYMENT_DETAIL.Pending_Balance, TSPL_PAYMENT_DETAIL.Applied_Amount,TSPL_PAYMENT_DETAIL.Security_Amount, TSPL_PAYMENT_DETAIL.Original_Invoice_Amt, " & _
             " Case When ISNULL(Against_POInvoice_No,'')<>'' Then (Select convert(varchar,PI_Date,103)  from TSPL_PI_HEAD where PI_No =Against_POInvoice_No)  When ISNULL(Against_PurchaseReturn_No,'')<> '' Then (Select convert(varchar,PR_Date,103) from TSPL_PR_HEAD where PR_No  =Against_PurchaseReturn_No ) Else convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) End as DocumentDate, " & _
             " TSPL_PAYMENT_DETAIL.TDS_Amount, TSPL_PAYMENT_DETAIL.Account_Code, "
            '' " Case When TSPL_Payment_Header.Payment_Type='MI' Then Net_Balance Else TSPL_VENDOR_INVOICE_HEAD.balance_amt - ISNULL((Select SUM(Applied_Amount)+coalesce(SUM(Security_Amount),0) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No AND Post NOT IN ('1','P')),0) END as [Net_Balance], " & _
            qry += " Case When TSPL_Payment_Header.Payment_Type='MI' Then Net_Balance " & _
" when ISNULL((Select TPH.Payment_Type from TSPL_PAYMENT_HEADER TPH where TPH.Payment_Type ='RC' AND TPH.Payment_No =TSPL_PAYMENT_DETAIL .Document_No ),'')='RC' THEN Net_Balance " & _
" Else TSPL_VENDOR_INVOICE_HEAD.Document_Total- isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount ,0) - ISNULL((Select SUM(Applied_Amount)+coalesce(SUM(Security_Amount),0) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No AND Post NOT IN ('1','P')),0) END as [Net_Balance], " & _
          " TSPL_PAYMENT_DETAIL.Description, TSPL_PAYMENT_DETAIL.Remarks, TSPL_PAYMENT_DETAIL.Comment, TSPL_PAYMENT_DETAIL.ESI_WCT_Percentage, " & _
           " TSPL_PAYMENT_DETAIL.Post, TSPL_PAYMENT_DETAIL.Settlement_code, TSPL_PAYMENT_DETAIL.Settlement_Description,EXPENSE_CODE,TSPL_PAYMENT_DETAIL.ConvRateOld," + _
           " TSPL_PAYMENT_DETAIL.Cost_Center_Fin_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,TSPL_PAYMENT_DETAIL.Hirerachy_Level_Code,TSPL_HIRERACHY_LEVEL_MASTER.Description as Hirerachy_Level_Name " & _
           " FROM TSPL_PAYMENT_DETAIL   left join TSPL_Payment_Header on TSPL_Payment_Header.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No" + _
           " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_PAYMENT_DETAIL.Cost_Center_Fin_Code " + _
           " LEFT OUTER JOIN TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE=TSPL_PAYMENT_DETAIL.Hirerachy_Level_Code " + _
           " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No " & _
           " WHERE TSPL_PAYMENT_DETAIL.Payment_No = '" + obj.Payment_No + "')  as final order by convert(date,Payment_Date,103)"

            '            qry += " Case When TSPL_Payment_Header.Payment_Type='MI' Then Net_Balance " & _
            '" when ISNULL((Select TPH.Payment_Type from TSPL_PAYMENT_HEADER TPH where TPH.Payment_Type ='RC' AND TPH.Payment_No =TSPL_PAYMENT_DETAIL .Document_No ),'')='RC' THEN (select  Payment_Amount -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_PAYMENT_DETAIL where TSPL_PAYMENT_DETAIL.Document_No=TSPL_PAYMENT_HEADER.Payment_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No )),0)  as BalAmt from TSPL_PAYMENT_HEADER  WHERE  Payment_Type  ='RC' and IsChkReverse ='N' AND TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL .Document_No) " & _
            '" Else TSPL_VENDOR_INVOICE_HEAD.Document_Total- isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount ,0) - ISNULL((Select SUM(Applied_Amount)+coalesce(SUM(Security_Amount),0) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No AND Post NOT IN ('1','P')),0) END as [Net_Balance], " & _
            '            " TSPL_PAYMENT_DETAIL.Description, TSPL_PAYMENT_DETAIL.Remarks, TSPL_PAYMENT_DETAIL.Comment, TSPL_PAYMENT_DETAIL.ESI_WCT_Percentage, " & _
            '             " TSPL_PAYMENT_DETAIL.Post, TSPL_PAYMENT_DETAIL.Settlement_code, TSPL_PAYMENT_DETAIL.Settlement_Description,EXPENSE_CODE,TSPL_PAYMENT_DETAIL.ConvRateOld," + _
            '             " TSPL_PAYMENT_DETAIL.Cost_Center_Fin_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,TSPL_PAYMENT_DETAIL.Hirerachy_Level_Code,TSPL_HIRERACHY_LEVEL_MASTER.Description as Hirerachy_Level_Name " & _
            '             " FROM TSPL_PAYMENT_DETAIL   left join TSPL_Payment_Header on TSPL_Payment_Header.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No" + _
            '             " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_PAYMENT_DETAIL.Cost_Center_Fin_Code " + _
            '             " LEFT OUTER JOIN TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE=TSPL_PAYMENT_DETAIL.Hirerachy_Level_Code " + _
            '             " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No " & _
            '             " WHERE TSPL_PAYMENT_DETAIL.Payment_No = '" + obj.Payment_No + "')  as final order by convert(date,Payment_Date,103)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrTr = New List(Of clsPaymentDetail)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsPaymentDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsPaymentDetail()
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


            ''richa
            qry = "SELECT * from TSPL_PAYMENT_DETAIL_GST WHERE TSPL_PAYMENT_DETAIL_GST.Payment_No = '" + obj.Payment_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrTrGST = New List(Of clsPaymentDetailGST)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsPaymentDetailGST
                For Each dr As DataRow In dt.Rows
                    objTr = New clsPaymentDetailGST()
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Payment_No = clsCommon.myCstr(dr("Payment_No"))
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

                    objTr.TAX1_Amt_Payment = clsCommon.myCdbl(dr("TAX1_Amt_Payment"))
                    objTr.TAX2_Amt_Payment = clsCommon.myCdbl(dr("TAX2_Amt_Payment"))
                    objTr.TAX3_Amt_Payment = clsCommon.myCdbl(dr("TAX3_Amt_Payment"))
                    objTr.TAX4_Amt_Payment = clsCommon.myCdbl(dr("TAX4_Amt_Payment"))
                    objTr.TAX5_Amt_Payment = clsCommon.myCdbl(dr("TAX5_Amt_Payment"))
                    objTr.TAX6_Amt_Payment = clsCommon.myCdbl(dr("TAX6_Amt_Payment"))
                    objTr.TAX7_Amt_Payment = clsCommon.myCdbl(dr("TAX7_Amt_Payment"))
                    objTr.TAX8_Amt_Payment = clsCommon.myCdbl(dr("TAX8_Amt_Payment"))
                    objTr.TAX9_Amt_Payment = clsCommon.myCdbl(dr("TAX9_Amt_Payment"))
                    objTr.TAX10_Amt_Payment = clsCommon.myCdbl(dr("TAX10_Amt_Payment"))
                    objTr.PaymentAdvance = clsCommon.myCdbl(dr("PaymentAdvance"))
                    objTr.PaymentTotalTax = clsCommon.myCdbl(dr("PaymentTotalTax"))
                    objTr.PaymentTotalAdvanceAmt = clsCommon.myCdbl(dr("PaymentTotalAdvanceAmt"))
                    obj.ArrTrGST.Add(objTr)
                Next


            End If
            '' Bank Charges Tax
            obj.objBCT = clsPaymentBankChargesTax.GetBankChargesTaxList(obj.Payment_No, trans)
            ''----------


        Else

        End If
        Return obj
    End Function

    Public Shared Function GetBalance(ByVal strAppliedPayment As String, ByVal strPaymentNo As String, ByVal trans As SqlTransaction) As Double
        Try
            ''richa agarwal against ticket no BM00000008630 on 07-Jan-2016
            '    Dim qry As String = "Select [Bal Amt] from (" & _
            '" Select Payment_No as [Code], Entry_Desc as [Description], Payment_Date as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount as [Payment Amt], Balance_Amt-ISNULL((Select SUM(Payment_Amount) from TSPL_PAYMENT_HEADER PH WHERE PH.Posted<>'1' AND PH.Payment_Type='AD' AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No AND PH.Payment_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_PAYMENT_HEADER WHERE Posted='1' AND Payment_Type IN ('AV','OA') AND Payment_No <> '" + strPaymentNo + "'" & _
            '" ) Final WHERE Code='" + strAppliedPayment + "'"
            '    Dim qry As String = "Select [Bal Amt] from (" & _
            '" Select Payment_No as [Code], Entry_Desc as [Description], Payment_Date as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount+isnull(TDS_Amount ,0) as [Payment Amt], Balance_Amt+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_PAYMENT_HEADER PH WHERE PH.Posted<>'1' AND PH.Payment_Type='AD' AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No AND PH.Payment_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_PAYMENT_HEADER WHERE Posted='1' AND Payment_Type IN ('AV','OA') AND Payment_No <> '" + strPaymentNo + "'" & _
            '" ) Final WHERE Code='" + strAppliedPayment + "'"

            '      Dim qry As String = "Select [Bal Amt] from (" & _
            '" Select Payment_No as [Code], Entry_Desc as [Description], Payment_Date as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount+isnull(TDS_Amount ,0) as [Payment Amt], Payment_Amount+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_PAYMENT_HEADER PH WHERE PH.IsChkReverse='N' AND PH.Payment_Type='AD' AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No AND PH.Payment_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_PAYMENT_HEADER WHERE Posted='1' AND Payment_Type IN ('AV','OA') AND Payment_No <> '" + strPaymentNo + "'" & _
            '" ) Final WHERE Code='" + strAppliedPayment + "'"

            Dim strQryForRejectedAmt As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " & _
          "from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_PurchaseReturn_No  in (SELECT PR_No  FROM TSPL_PR_HEAD WHERE Against_PI =TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ) and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "

            '' this code is wrritten for that debit note which is created auto through PI and Pr is not created against that PI
            Dim strQryForRejectedAmtforNonPR As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " & _
          "from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_POInvoice_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "


            Dim strTaxRecovarableQuery As String = " - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then " & _
            " ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'  " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end + " & _
            " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y' " & _
            " then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end) "

            'Dim qry As String = "Select [Bal Amt] from (" & _
            '" Select Payment_No as [Code], Entry_Desc as [Description], Payment_Date as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount+isnull(TDS_Amount ,0) as [Payment Amt], Payment_Amount+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_PAYMENT_HEADER PH WHERE PH.IsChkReverse='N' AND PH.Payment_Type='AD' AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No AND PH.Payment_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_PAYMENT_HEADER WHERE Posted='1' AND Payment_Type IN ('AV','OA') AND Payment_No <> '" + strPaymentNo + "'" & _
            '" UNION ALL " & Environment.NewLine & _
            '" Select Document_No AS Code,'' as Description,DocumentDate as [Payment Date],DocType as [Payment Type],OriginalAmt as [Payment Amt],PendingAmt as [Bal Amt] from ( select Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then 'Debit Note' Else case  When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then 'Credit Note' Else 'Invoice' End End As [DocType],  " & _
            '" TSPL_VENDOR_INVOICE_HEAD.Document_No, Case When ISNULL(Against_POInvoice_No,'')<>'' Then Against_POInvoice_No When ISNULL(Against_PurchaseReturn_No,'')<> '' Then Against_PurchaseReturn_No Else Document_No End as PurchaseInvoice," & _
            '" Case When ISNULL(Against_POInvoice_No,'')<>'' Then (Select convert(varchar,PI_Date,103)  from TSPL_PI_HEAD where PI_No =Against_POInvoice_No)  When ISNULL(Against_PurchaseReturn_No,'')<> '' Then (Select convert(varchar,PR_Date,103) from TSPL_PR_HEAD where PR_No  =Against_PurchaseReturn_No ) Else TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date End as DocumentDate, " & _
            '" TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [DocDate] ,  " & _
            '" TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [VendorInvoiceNo], " & _
            '" TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " as [OriginalAmt]  ," & _
            '" TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as [TDSAmt], " & _
            '" (TSPL_VENDOR_INVOICE_HEAD.Document_Total-TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount) " + strTaxRecovarableQuery + "  as [NetAmount], " & _
            '" TSPL_VENDOR_INVOICE_HEAD.Document_Total - ISNULL(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) " + strTaxRecovarableQuery + " - ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No) ),0) " & _
            '" -isnull((select sum(isnull(Payment_Amount,0)) from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Applied_Payment =TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No ) and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and isnull(TSPL_PAYMENT_HEADER.Applied_Payment ,'')<>'' AND Payment_No <> '" + strPaymentNo + "'),0)  " & Environment.NewLine & _
            '" " + strQryForRejectedAmt + " " + strQryForRejectedAmtforNonPR + " " & _
            '" -ISNULL((Select SUM(isnull(TSPL_PAYMENT_ADJUSTMENT_DETAIL.Amount,0)) from TSPL_PAYMENT_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_ADJUSTMENT_DETAIL on TSPL_PAYMENT_ADJUSTMENT_Header.Adjustment_No=TSPL_PAYMENT_ADJUSTMENT_DETAIL.Adjustment_No Where isnull(TSPL_PAYMENT_ADJUSTMENT_Header.Doc_No,'') = isnull(TSPL_VENDOR_INVOICE_HEAD.Document_No,'') ),0) " & _
            '" as [PendingAmt],TSPL_VENDOR_INVOICE_HEAD.ConvRate " & _
            '" ,isnull(( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation " & _
            '" from TSPL_VENDOR_INVOICE_HEAD " & _
            '" Left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  " & _
            '  " WHERE ((ISNULL(RefDocNo,'')= '' or ISNULL((select VI.Document_Type from TSPL_VENDOR_INVOICE_HEAD VI where VI.Document_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo ),'') in ('I','')) AND ISNULL(Against_PurchaseReturn_No,'')= '') and not ( ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D') " & _
            '" and TSPL_VENDOR_INVOICE_HEAD.RefDocType<>'REVALUATION ENTRY'" & _
            '" and isnull(TSPL_VENDOR_INVOICE_HEAD.posting_date,'')<>'') FINALQRY WHERE FINALQRY.PendingAmt>0  AND DocType ='Debit Note' " & _
            '" ) Final WHERE Code='" + strAppliedPayment + "'"

            Dim qry As String = "Select [Bal Amt] from (" & _
           " Select Payment_No as [Code], Entry_Desc as [Description], Payment_Date as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount+isnull(TDS_Amount ,0) as [Payment Amt], Payment_Amount+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_PAYMENT_HEADER PH WHERE PH.IsChkReverse='N' AND PH.Payment_Type='AD' AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No AND PH.Payment_No<>'" + strPaymentNo + "'),0) as [Bal Amt] from TSPL_PAYMENT_HEADER WHERE Posted='1' AND Payment_Type IN ('AV','OA') AND Payment_No <> '" + strPaymentNo + "'" & _
           " UNION ALL " & Environment.NewLine & _
           " Select Document_No AS Code,'' as Description,DocumentDate as [Payment Date],DocType as [Payment Type],OriginalAmt as [Payment Amt],PendingAmt as [Bal Amt] from ( select Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then 'Debit Note' Else case  When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then 'Credit Note' Else 'Invoice' End End As [DocType],  " & _
           " TSPL_VENDOR_INVOICE_HEAD.Document_No, Case When ISNULL(Against_POInvoice_No,'')<>'' Then Against_POInvoice_No When ISNULL(Against_PurchaseReturn_No,'')<> '' Then Against_PurchaseReturn_No Else Document_No End as PurchaseInvoice," & _
           " Case When ISNULL(Against_POInvoice_No,'')<>'' Then (Select convert(varchar,PI_Date,103)  from TSPL_PI_HEAD where PI_No =Against_POInvoice_No)  When ISNULL(Against_PurchaseReturn_No,'')<> '' Then (Select convert(varchar,PR_Date,103) from TSPL_PR_HEAD where PR_No  =Against_PurchaseReturn_No ) Else TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date End as DocumentDate, " & _
           " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [DocDate] ,  " & _
           " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [VendorInvoiceNo], " & _
           " TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " as [OriginalAmt]  ," & _
           " TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as [TDSAmt], " & _
           " (TSPL_VENDOR_INVOICE_HEAD.Document_Total - case when isnull(TSPL_VENDOR_INVOICE_HEAD .is_For_TDS,0) =1 and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) end) " + strTaxRecovarableQuery + "  as [NetAmount], " & _
           " TSPL_VENDOR_INVOICE_HEAD.Document_Total - case when isnull(TSPL_VENDOR_INVOICE_HEAD .is_For_TDS,0) =1 and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) end " + strTaxRecovarableQuery + " - ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No) ),0) " & _
           " -isnull((select sum(isnull(Payment_Amount,0)) from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Applied_Payment =TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No ) and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and isnull(TSPL_PAYMENT_HEADER.Applied_Payment ,'')<>'' AND Payment_No <> '" + strPaymentNo + "'),0)  " & Environment.NewLine & _
           " " + strQryForRejectedAmt + " " + strQryForRejectedAmtforNonPR + " " & _
           " -ISNULL((Select SUM(isnull(TSPL_PAYMENT_ADJUSTMENT_DETAIL.Amount,0)) from TSPL_PAYMENT_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_ADJUSTMENT_DETAIL on TSPL_PAYMENT_ADJUSTMENT_Header.Adjustment_No=TSPL_PAYMENT_ADJUSTMENT_DETAIL.Adjustment_No Where isnull(TSPL_PAYMENT_ADJUSTMENT_Header.Doc_No,'') = isnull(TSPL_VENDOR_INVOICE_HEAD.Document_No,'') ),0) " & _
           " as [PendingAmt],TSPL_VENDOR_INVOICE_HEAD.ConvRate " & _
           " ,isnull(( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation " & _
           " from TSPL_VENDOR_INVOICE_HEAD " & _
           " Left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  " & _
             " WHERE ((ISNULL(RefDocNo,'')= '' or ISNULL((select VI.Document_Type from TSPL_VENDOR_INVOICE_HEAD VI where VI.Document_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo ),'') in ('I','')) AND ISNULL(Against_PurchaseReturn_No,'')= '') and not ( ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D') " & _
           " and TSPL_VENDOR_INVOICE_HEAD.RefDocType<>'REVALUATION ENTRY'" & _
           " and isnull(TSPL_VENDOR_INVOICE_HEAD.posting_date,'')<>'') FINALQRY WHERE FINALQRY.PendingAmt>0  AND DocType ='Debit Note' " & _
           " ) Final WHERE Code='" + strAppliedPayment + "'"

            '" WHERE (ISNULL(RefDocNo,'')= '' AND ISNULL(Against_PurchaseReturn_No,'')= '')  and not ( ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D') " & _
            '" and TSPL_VENDOR_INVOICE_HEAD.RefDocType<>'REVALUATION ENTRY'" & _
            '" and isnull(TSPL_VENDOR_INVOICE_HEAD.posting_date,'')<>'') FINALQRY WHERE FINALQRY.PendingAmt>0  AND DocType ='Debit Note' " & _
            '" ) Final WHERE Code='" + strAppliedPayment + "'"

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
            'If Bank_Balance <= 0 Then
            Dim strdocdatefilter As Date = clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy")
            Dim strserverdatefilter As Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            If strdocdatefilter < strserverdatefilter Then
                Dim qryForbalanceAmountForPayment As String = clsBankReco.GetAmountforbackdateentry(Doc_Date, "'" & Bank_Code & "'", trans)
                Bank_Balance = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qryForbalanceAmountForPayment, trans))
            End If
            strdocdatefilter = Nothing
            strserverdatefilter = Nothing
            'End If

            Bank_Balance = clsCommon.myFormat(Bank_Balance)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return Bank_Balance
    End Function

    Public Shared Function UpdateBalance(ByVal strAppliedPayment As String, ByVal dblAmtToBeDeduct As Double, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "Update TSPL_PAYMENT_HEADER SET Balance_Amt=Balance_Amt-" + clsCommon.myCstr(dblAmtToBeDeduct) + " WHERE Payment_No='" + strAppliedPayment + "'"
            clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function fundelete(ByVal strPaymentType As String, ByVal strPaymentNo As String, ByVal strVendorCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            fundelete(strPaymentType, strPaymentNo, strVendorCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function fundelete(ByVal strPaymentType As String, ByVal strPaymentNo As String, ByVal strVendorCode As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = Nothing
        Try
            Dim obj As clsPaymentHeader
            If clsCommon.myLen(strPaymentNo) > 0 Then
                obj = clsPaymentHeader.GetData(strPaymentNo, NavigatorType.Current, trans)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Payment_No) > 0) Then
                    'trans = clsDBFuncationality.GetTransactin()
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

            'Ticket No-TEC/06/09/19-001003,Save Deleted data ,sanjay
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strPaymentNo, "TSPL_PAYMENT_HEADER", "Payment_no", trans)
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, strPaymentNo, "TSPL_PAYMENT_HEADER", "Payment_No", "TSPL_PAYMENT_DETAIL", "Payment_No", "TSPL_PAYMENT_BANK_CHARGES_TAX", "Payment_No", "TSPL_PAYMENT_DETAIL_GST", "Payment_No", "", "", "", "", "", "", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strPaymentNo, "TSPL_REMITTANCE", "Document_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strPaymentNo, "TSPL_bank_book", "SOURCEDOC_NO", trans)

            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_PAYMENT_DETAIL where Payment_no='" + strPaymentNo + "'", trans)
            Dim qry As String = "DELETE From TSPL_PAYMENT_DETAIL_GST WHERE Payment_No ='" + strPaymentNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_REMITTANCE where Document_No='" + strPaymentNo + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_PAYMENT_BANK_CHARGES_TAX where Payment_No='" + strPaymentNo + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_PAYMENT_HEADER where Payment_no='" + strPaymentNo + "'", trans)
            clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Payment_No, trans)
            ''richa to set outanstanding balance of bank reco
            clsBankReco.SetOutstandingEntry(strPaymentNo, obj.Payment_Date, "Payment", trans, False)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
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
                Dim Qry As String = "Select TSPL_PAYMENT_DETAIL.Applied_Amount, TSPL_PAYMENT_DETAIL.Net_Balance  from TSPL_PAYMENT_DETAIL Left Outer Join TSPL_PAYMENT_HEADER ON  TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No where TSPL_PAYMENT_HEADER.Vendor_Code = '" + VendorCode + "' and TSPL_PAYMENT_DETAIL.Vendor_Invoice_No = '" + InvoiceNo + "' AND TSPL_PAYMENT_DETAIL.Payment_No = '" + PaymentNo + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    BalAmt = clsCommon.myCdbl(dt.Rows(0)("Applied_Amount")) + clsCommon.myCdbl(dt.Rows(0)("Net_Balance"))
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = Balance_Amt - " + clsCommon.myCstr(BalAmt) + " where vendor_code = '" + VendorCode + "' and Vendor_Invoice_No = '" + InvoiceNo + "' AND Document_No='" + DocNo + "'", trans)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function IsValidVendorForRequitionItem(ByVal strReqNo As String, ByVal strICode As String, ByVal strVendorCode As String) As Boolean
        Dim qry As String = "select 1 from TSPL_PAYMENT_DETAIL where Payment_No ='" + strReqNo + "' and Item_Code='" + strICode + "' and Vendor_Code not in ('','" + strVendorCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function

    'Public Shared Function PostData(ByVal strPaymentNo As String, Optional ByVal Module_Code As String = "Payable", Optional ByVal transOPen As SqlTransaction = Nothing) As Boolean
    '    Dim isSourceCode As Boolean = False
    '    Dim isSaved As Boolean = True
    '    Dim Payment_Line_No As Integer = 0
    '    Dim obj As clsPaymentHeader = clsPaymentHeader.GetData(strPaymentNo, NavigatorType.Current, transOPen)
    '    Dim trans As SqlTransaction = Nothing
    '    If transOPen IsNot Nothing Then
    '        trans = transOPen
    '    Else
    '        trans = clsDBFuncationality.GetTransactin()
    '    End If


    '    Try
    '        If clsCommon.myLen(strPaymentNo) <= 0 Then
    '            Throw New Exception("Document No. not found to Post")
    '        End If
    '        Dim qry As String = "select TSPL_PAYMENT_HEADER.Payment_No,TSPL_PAYMENT_HEADER.Entry_Desc,TSPL_PAYMENT_HEADER.Vendor_Code, " & _
    '        " TSPL_PAYMENT_HEADER.Vendor_Name,TSPL_PAYMENT_HEADER.Payment_Amount,TSPL_PAYMENT_HEADER.Payment_Type,TSPL_PAYMENT_HEADER.Bank_Code, " & _
    '        "ISNULL(TSPL_PAYMENT_HEADER.Location_GL_Code,'') As Location_GL_Code, " & _
    '        " TSPL_PAYMENT_HEADER.Reference,TSPL_PAYMENT_HEADER.Narration,TSPL_PAYMENT_HEADER.Total_Applied_Amount,TSPL_PAYMENT_HEADER.Total_Security_Amount, TSPL_PAYMENT_HEADER.Bank_Charges, " & _
    '        " TSPL_PAYMENT_HEADER.Bank_Charges_Ac,TSPL_PAYMENT_DETAIL.Document_No, ISNULL(TSPL_PAYMENT_DETAIL.Net_Balance,0) as Net_Balance," & _
    '        " ISNULL(TSPL_PAYMENT_DETAIL.TDS_Amount,0) as TDS_Amount, TSPL_PAYMENT_HEADER.Posted,TSPL_PAYMENT_HEADER.Location_Code, " & _
    '        " TSPL_PAYMENT_HEADER.Remit_To,TSPL_PAYMENT_HEADER.CURRENCY_CODE,CONVERT(DATE,TSPL_PAYMENT_HEADER.APPLICABLEFROM,101) AS APPLICABLEFROM,TSPL_PAYMENT_HEADER.PAYMENT_AMOUNT_BASE_CURRENCY,TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT, " & _
    '        " TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT,TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_ACCOUNT,TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_ACCOUNT,TSPL_PAYMENT_HEADER.ConvRate,TSPL_PAYMENT_HEADER.ConvRateOld,TSPL_PAYMENT_HEADER.PAYMENT_DATE,CForm_InvoiceNo," & _
    '        " TSPL_PAYMENT_HEADER.Applied_Payment, TSPL_PAYMENT_HEADER.Balance_Amt" & _
    '        " from TSPL_PAYMENT_DETAIL right outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No  " & _
    '        " where TSPL_PAYMENT_HEADER.Payment_No='" + strPaymentNo + "'"

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dt Is Nothing Or dt.Rows.Count <= 0 Then
    '            Throw New Exception("Document No. not found to Post")
    '        Else
    '            '--------Checks Whertrher Transaction Is Locked Or Not-----------
    '            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans)
    '            clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, clsCommon.myCstr(dt.Rows(0)("Payment_Date")), trans)
    '            '----------------------------------------------------------------
    '        End If
    '        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Posted")), "1") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Posted")), "P") = CompairStringResult.Equal Then
    '            Throw New Exception("Already Posted Document no : " + strPaymentNo + "")
    '        End If
    '        Dim DocNo As String = clsCommon.myCstr(dt.Rows(0)("Document_No"))
    '        Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + dt.Rows(0)("Bank_Code") + "'", trans)
    '        Dim PostDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" Select Payment_Post_Date from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No='" + strPaymentNo + "' ", trans))
    '        Dim strentrydesc As String = "Payment Against" + " " + strPaymentNo
    '        Dim sourceType As String = "AP-PY"
    '        Dim sourceDesc As String = "PAYMENT"
    '        Dim paymentDesc As String = clsCommon.myCstr(dt.Rows(0)("Entry_Desc"))

    '        If clsCommon.myCstr(dt.Rows(0)("Remit_To")) <> "" Then
    '            paymentDesc += " " + clsCommon.myCstr(dt.Rows(0)("Remit_To"))
    '        End If

    '        Dim strsrctype As String = "V"
    '        Dim strsrctypecode As String = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
    '        Dim strsrctypedesc As String = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
    '        Dim SettlementLoc As String = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
    '        Dim Loc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "' ", trans))
    '        Dim straccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "' ", trans))

    '        straccount = clsERPFuncationality.ChangeGLAccountLocationSegment(straccount, BankLocation, True, trans)
    '        '' Anubhooti 03-Sep-2014 BM00000003437(Remarks : if setting "AllowToUseSubAccount" is ON Then BankAccount is Sub_Account Else previous)
    '        Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans))
    '        Dim bankAccount As String
    '        Dim UseSubAcc As String
    '        UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
    '        If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
    '            bankAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Sub_Account,'')  BANKACC from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans))
    '        Else
    '            bankAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans))
    '        End If
    '        ''

    '        If clsCommon.myLen(bankAccount) <= 0 Then
    '            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
    '                Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(dt.Rows(0)("Bank_Code")))
    '            Else
    '                Throw New Exception("Please enter bank account for bank " + clsCommon.myCstr(dt.Rows(0)("Bank_Code")))
    '            End If

    '        End If

    '        bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, BankLocation, True, trans)

    '        Dim stradvance As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "' ", trans))
    '        stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)

    '        '' Anubhooti 31-Mar-2015 (If security is checked then security account will go on GL)
    '        If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Is_Security) = 1 Then
    '            stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.SECURITY_ACCOUNT,'') AS SECURITY_ACCOUNT from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "' ", trans))
    '            stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
    '            If clsCommon.myLen(stradvance) <= 0 Then
    '                Throw New Exception("Please fill security account on vendor account set.")
    '            End If
    '        End If

    '        '' Anubhooti 27-Mar-2015 (Replace AdvanceAccount From AdvanceAgainstSalary In Case Of Advance & On-Account Only)
    '        If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Advance_Against_Salary) = 1 Then
    '            stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Salary,'') AS Advance_Against_Salary from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "' ", trans))
    '            stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
    '            If clsCommon.myLen(stradvance) <= 0 Then
    '                Throw New Exception("Please fill advance against salary account on vendor account set.")
    '            End If
    '        End If

    '        Dim BankChargesAc As String = clsCommon.myCstr(dt.Rows(0)("Bank_Charges_Ac"))




    '        '' MultiCurrency
    '        Dim IsMultiCurrency As Boolean = clsModuleCurrencyMapping.CheckMultiCurrency(Module_Code, trans)
    '        Dim CURRENCY_CODE As String = dt.Rows(0)("CURRENCY_CODE").ToString
    '        ' Dim APPLICABLEFROM As Date? = Nothing
    '        Dim APPLICABLEFROM As String = String.Empty
    '        If IsDBNull(dt.Rows(0)("APPLICABLEFROM")) = True Then
    '            APPLICABLEFROM = Nothing
    '        Else
    '            'APPLICABLEFROM = dt.Rows(0)("APPLICABLEFROM")
    '            APPLICABLEFROM = clsCommon.GetPrintDate(clsCommon.myCstr(dt.Rows(0)("APPLICABLEFROM")), "yyyy-MM-dd")
    '        End If
    '        Dim EXCHANGE_LOSS_AMT As Double = clsCommon.myCdbl(dt.Rows(0)("EXCHANGE_LOSS_AMT"))
    '        Dim EXCHANGE_GAIN_AMT As Double = clsCommon.myCdbl(dt.Rows(0)("EXCHANGE_GAIN_AMT"))
    '        Dim EXCHANGE_GAIN_ACCOUNT As String = clsCommon.myCstr(dt.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))
    '        Dim EXCHANGE_LOSS_ACCOUNT As String = clsCommon.myCstr(dt.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
    '        Dim ConvRateOld As Double = IIf(clsCommon.myCdbl(dt.Rows(0)("ConvRateOld")) = 0, 1, clsCommon.myCdbl(dt.Rows(0)("ConvRateOld")))
    '        Dim ConvRate As Double = IIf(clsCommon.myCdbl(dt.Rows(0)("ConvRate")) = 0, 1, clsCommon.myCdbl(dt.Rows(0)("ConvRate")))

    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", CURRENCY_CODE, True)
    '        clsCommon.AddColumnsForChange(coll, "APPLICABLEFROM", APPLICABLEFROM, True)
    '        clsCommon.AddColumnsForChange(coll, "ConvRate", ConvRate)
    '        clsCommon.AddColumnsForChange(coll, "ConvRateOld", ConvRateOld)
    '        '' End MultiCurrency
    '        Dim arr As New ArrayList()
    '        Dim drtotal As Double = 0
    '        Dim crtotal As Double = 0
    '        Dim bankCharges As Double = 0
    '        If (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AV") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "OA") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "RC") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "MI") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "PY") = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AD") = CompairStringResult.Equal) Then
    '            ''richa agarwal 01/07/2015 in case of multicurrency 
    '            'drtotal = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
    '            'crtotal = -1 * clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
    '            'bankCharges = clsCommon.myCdbl(dt.Rows(0)("Bank_Charges"))
    '            'Dim Credit() As String = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '            'Dim Debit() As String = {straccount, drtotal}
    '            drtotal = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount")) * ConvRate
    '            bankCharges = clsCommon.myCdbl(dt.Rows(0)("Bank_Charges"))
    '            crtotal = -1 * (clsCommon.myCdbl(dt.Rows(0)("Payment_Amount")) * ConvRate + bankCharges)
    '            Dim Credit() As String = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '            Dim Debit() As String = {straccount, drtotal}
    '            ''------------------------------------
    '            arr.Add(Credit)
    '            arr.Add(Debit)
    '        Else
    '            drtotal = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount")) * clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
    '            crtotal = -1 * clsCommon.myCdbl(dt.Rows(0)("Payment_Amount")) * clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
    '            bankCharges = clsCommon.myCdbl(dt.Rows(0)("Bank_Charges")) * clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
    '            Dim Credit() As String = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '            Dim Debit() As String = {straccount, drtotal}
    '            arr.Add(Credit)
    '            arr.Add(Debit)
    '        End If


    '        '' Anubhooti 27-Nov-2014 (TDS GL Account)
    '        'Dim RemittanceObj As String
    '        'Dim objRem As clsPaymentHeader = Nothing
    '        obj.objRemittance = clsRemittance.GetData(strPaymentNo, trans)
    '        ''
    '        If (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "PY") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AD") = CompairStringResult.Equal) Then
    '            arr = New ArrayList()
    '            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AD") = CompairStringResult.Equal Then
    '                Dim tBankLocation As String = bankAccount.Substring(clsCommon.myLen(bankAccount) - 3, 3)
    '                qry = "select TSPL_VENDOR_ACCOUNT_SET.Advance_Account from TSPL_VENDOR_ACCOUNT_SET  INNER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Account= TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  where TSPL_VENDOR_MASTER.Vendor_Code ='" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "'"
    '                bankAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    '                bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, tBankLocation, trans)
    '            End If

    '            Dim strQry As String = "Select TSPL_PAYMENT_DETAIL.Vendor_Invoice_No,case when isnull(TSPL_VENDOR_INVOICE_HEAD.ConvRate,0)=0 then 1 else TSPL_VENDOR_INVOICE_HEAD.ConvRate end  as ConvRateOld, TSPL_PAYMENT_DETAIL.Document_No, TSPL_PAYMENT_DETAIL.Applied_Amount,TSPL_PAYMENT_DETAIL.Security_Amount,TSPL_VENDOR_INVOICE_HEAD.Loc_Code "
    '            strQry += " from TSPL_PAYMENT_HEADER "
    '            strQry += " INNER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No "
    '            strQry += "     INNER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_PAYMENT_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No  "
    '            strQry += " Where TSPL_PAYMENT_HEADER.Vendor_Code='" + strsrctypecode + "' AND TSPL_PAYMENT_HEADER.Payment_No ='" + strPaymentNo + "'"
    '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
    '            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
    '                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
    '                For Each dr As DataRow In dt1.Rows
    '                    Dim strDocLocation As String = clsCommon.myCstr(dr("Loc_Code"))
    '                    ''richa agarwal 24/06/2015
    '                    'Dim dblAmount As Double = clsCommon.myCdbl(dr("Applied_Amount")) / ConvRateOld
    '                    Dim dblAmount As Double = clsCommon.myCdbl(dr("Applied_Amount")) * clsCommon.myCdbl(dr("ConvRateOld"))
    '                    Dim dblAmount1 As Double = clsCommon.myCdbl(dr("Applied_Amount")) * ConvRate
    '                    ''-----------------
    '                    If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(BankLocation, strDocLocation) = CompairStringResult.Equal) Then
    '                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(strDocLocation, BankLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + strDocLocation + " and to location " + BankLocation)
    '                        End If
    '                        ''branch accounting of customer
    '                        ''richa agarwal 01/07/2015
    '                        'Dim RcvblAcc = New String() {strTemp, -1 * dblAmount}
    '                        Dim RcvblAcc = New String() {strTemp, -1 * dblAmount1}
    '                        arr.Add(RcvblAcc)
    '                        ' changed due to ap invoice takes segment not location
    '                        strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(straccount, strDocLocation, True, trans)
    '                        RcvblAcc = New String() {strTemp, dblAmount}
    '                        arr.Add(RcvblAcc)

    '                        ''richa agarwal 01/07/2015
    '                        'RcvblAcc = New String() {bankAccount, -1 * dblAmount}
    '                        RcvblAcc = New String() {bankAccount, -1 * dblAmount1}
    '                        arr.Add(RcvblAcc)
    '                        ''-------------------

    '                        strTemp = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strDocLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strDocLocation)
    '                        End If
    '                        ''richa agarwal 01/07/2015
    '                        'RcvblAcc = New String() {strTemp, dblAmount}
    '                        RcvblAcc = New String() {strTemp, dblAmount1}
    '                        '--------------------
    '                        arr.Add(RcvblAcc)
    '                    Else
    '                        'Dim RcvblAcc = New String() {bankAccount, -1 * dblAmount}
    '                        Dim RcvblAcc = New String() {bankAccount, -1 * dblAmount1}
    '                        arr.Add(RcvblAcc)
    '                        RcvblAcc = New String() {straccount, dblAmount}
    '                        arr.Add(RcvblAcc)
    '                    End If

    '                    If clsCommon.myCdbl(dr("Security_Amount")) > 0 Then
    '                        Create_Security_Credit_Note(dr("Vendor_Invoice_No"), dt.Rows(0)("Vendor_Code"), obj, trans)
    '                    End If
    '                    strQry = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = Balance_Amt - " + clsCommon.myCstr(dr("Applied_Amount")) + " where vendor_code = '" + strsrctypecode + "' and Vendor_Invoice_No = '" + clsCommon.myCstr(dr("Vendor_Invoice_No")) + "' AND Document_No = '" + clsCommon.myCstr(dr("Document_No")) + "'"
    '                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
    '                Next
    '                ' '' richa agarwal 01/07/2015 to add bank charges in bank amount
    '                If bankCharges > 0 Then
    '                    Dim BankChargeCredit() As String = {bankAccount, -1 * bankCharges}
    '                    arr.Add(BankChargeCredit)
    '                End If
    '            End If


    '            If bankCharges > 0 And clsCommon.myLen(BankChargesAc) > 0 Then
    '                ''---richa agarwal 01/07/2015
    '                BankChargesAc = clsERPFuncationality.ChangeGLAccountLocationSegment(BankChargesAc, BankLocation, True, trans)
    '                ''---------------------
    '                Dim BankCharge() As String = {BankChargesAc, bankCharges}
    '                arr.Add(BankCharge)
    '            End If

    '            '' MULTICURRENCY
    '            If IsMultiCurrency Then
    '                If EXCHANGE_LOSS_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                    arr.Add(CURR_EXCHANGE)
    '                ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                    arr.Add(CURR_EXCHANGE)
    '                End If
    '            End If
    '            '' END MULTICURRENCY
    '            If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arr, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                isSourceCode = True
    '            End If

    '            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AD") = CompairStringResult.Equal Then
    '                'This function chaged Balance Amount of Payment Which is Applied------------
    '                clsPaymentHeader.UpdateBalance(clsCommon.myCstr(dt.Rows(0)("Applied_Payment")), clsCommon.myCdbl(dt.Rows(0)("Payment_Amount")), trans)
    '                isSourceCode = True
    '            End If
    '            If isSourceCode = True Then
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '            End If
    '        ElseIf (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "SR") = CompairStringResult.Equal) Then

    '            Dim tds As Double = 0
    '            Dim paymentAmt As Double = 0
    '            Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld,PAYMENT_AMOUNT_BASE_CURRENCY  from TSPL_PAYMENT_HEADER where Payment_No = '" + strPaymentNo + "'"
    '            Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(checkall, trans)
    '            If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
    '                tds = clsCommon.myCdbl(dtNew.Rows(0)("TDS_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
    '                paymentAmt = clsCommon.myCdbl(dtNew.Rows(0)("PAYMENT_AMOUNT_BASE_CURRENCY")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
    '            End If
    '            Dim BankCharge() As String = {BankChargesAc, bankCharges}
    '            drtotal = clsCommon.myCdbl(dt.Rows(0)("PAYMENT_AMOUNT_BASE_CURRENCY")) * clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
    '            crtotal = -1 * clsCommon.myCdbl(dt.Rows(0)("PAYMENT_AMOUNT_BASE_CURRENCY")) * clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
    '            If tds <> 0 And paymentAmt <> 0 Then

    '                'Dim strquery As String = "select r.Branch_GL_AC    from TSPL_TDS_BRANCH_MASTER bm join TSPL_REMITTANCE r on bm.Branch_Code = r.Branch_Code where r.Document_No = '" + strPaymentNo + "'"
    '                'Dim strtaxaccount As String = clsDBFuncationality.getSingleValue(strquery, trans)
    '                'strtaxaccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strtaxaccount, BankLocation, True, trans)
    '                '' Anubhooti 27-Nov-2014
    '                If (obj.objRemittance IsNot Nothing) Then ''is_For_TDS Entry made by ap invoice is come in this section
    '                    obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, BankLocation, True, trans)
    '                Else
    '                    Throw New Exception("Please enter TDS account")
    '                End If
    '                Dim acc4() As String = {stradvance, (paymentAmt + tds)} '{stradvance, (paymentAmt + tds)}
    '                Dim acc3() As String = {obj.objRemittance.Branch_GL_AC, -1 * (paymentAmt + tds)} '{strtaxaccount, -1 * tds}
    '                Dim acc5() As String = {bankAccount, crtotal - bankCharges - (+EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                Dim arrtotal As New ArrayList()
    '                arrtotal.Add(acc4)
    '                arrtotal.Add(acc3)
    '                arrtotal.Add(acc5)

    '                If bankCharges > 0 And clsCommon.myLen(BankChargesAc) > 0 Then
    '                    arrtotal.Add(BankCharge)
    '                End If
    '                '' MULTICURRENCY
    '                If IsMultiCurrency Then
    '                    If EXCHANGE_LOSS_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                        arrtotal.Add(CURR_EXCHANGE)
    '                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                        arrtotal.Add(CURR_EXCHANGE)
    '                    End If
    '                End If

    '                ''
    '                '' END MULTICURRENCY

    '                If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                    isSourceCode = True
    '                End If
    '            Else
    '                'If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "SR") = CompairStringResult.Equal Then
    '                '    drtotal = drtotal * -1
    '                '    crtotal = crtotal * -1
    '                'End If

    '                '---------------------------------------------------------
    '                Dim arrlist As New ArrayList()
    '                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "SR") = CompairStringResult.Equal Then
    '                    Dim arr6() As String = {stradvance, drtotal}
    '                    Dim arr7() As String = {bankAccount, crtotal - (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT)}
    '                    arrlist.Add(arr6)
    '                    arrlist.Add(arr7)
    '                    '' MULTICURRENCY
    '                    If IsMultiCurrency Then
    '                        If EXCHANGE_LOSS_AMT > 0 Then
    '                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, -EXCHANGE_LOSS_AMT}
    '                            arrlist.Add(CURR_EXCHANGE)
    '                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, EXCHANGE_GAIN_AMT}
    '                            arrlist.Add(CURR_EXCHANGE)
    '                        End If
    '                    End If
    '                    '' END MULTICURRENCY
    '                Else
    '                    Dim arr6() As String = {stradvance, drtotal}
    '                    Dim arr7() As String = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                    Dim arr8() As String = {BankChargesAc, bankCharges}
    '                    arrlist.Add(arr6)
    '                    arrlist.Add(arr7)
    '                    If bankCharges > 0 And clsCommon.myLen(BankChargesAc) > 0 Then
    '                        arrlist.Add(arr8)
    '                    End If
    '                    '' MULTICURRENCY
    '                    If IsMultiCurrency Then
    '                        If EXCHANGE_LOSS_AMT > 0 Then
    '                            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                            arrlist.Add(CURR_EXCHANGE)
    '                        ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                            arrlist.Add(CURR_EXCHANGE)
    '                        End If
    '                    End If
    '                    '' END MULTICURRENCY
    '                End If



    '                If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                    isSourceCode = True
    '                End If
    '            End If
    '            'If isSourceCode = True Then
    '            clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '            clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '            'End If

    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AV") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "RC") = CompairStringResult.Equal Then
    '            Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
    '            Dim strGLLoc As String = clsCommon.myCstr(dt.Rows(0)("Location_GL_Code"))

    '            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AV") = CompairStringResult.Equal Then
    '                If Not String.IsNullOrEmpty(dt.Rows(0)("CForm_InvoiceNo")) Then
    '                    qry = "update TSPL_PI_HEAD set CFormRecd=1 WHERE PI_No ='" + dt.Rows(0)("CForm_InvoiceNo") + "'"
    '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                End If

    '            End If
    '            Dim tds As Double = 0
    '            Dim paymentAmt As Double = 0
    '            Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld  from TSPL_PAYMENT_HEADER where Payment_No = '" + strPaymentNo + "'"
    '            Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(checkall, trans)
    '            If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
    '                tds = clsCommon.myCdbl(dtNew.Rows(0)("TDS_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
    '                paymentAmt = clsCommon.myCdbl(dtNew.Rows(0)("Payment_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
    '            End If
    '            ''---richa agarwal 01/07/2015
    '            BankChargesAc = clsERPFuncationality.ChangeGLAccountLocationSegment(BankChargesAc, BankLocation, True, trans)
    '            Dim BankCharge() As String = {BankChargesAc, bankCharges}
    '            ''---------------------

    '            If tds <> 0 And paymentAmt <> 0 Then
    '                '' Anubhooti 27-Nov-2014 
    '                If (obj.objRemittance IsNot Nothing) Then ''is_For_TDS Entry made by ap invoice is come in this section
    '                    obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, BankLocation, True, trans)
    '                Else
    '                    Throw New Exception("Please enter TDS account")
    '                End If
    '                'Dim strquery As String = "select r.Branch_GL_AC    from TSPL_TDS_BRANCH_MASTER bm join TSPL_REMITTANCE r on bm.Branch_Code = r.Branch_Code where r.Document_No = '" + strPaymentNo + "'"
    '                'Dim strtaxaccount As String = clsDBFuncationality.getSingleValue(strquery, trans)
    '                'strtaxaccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strtaxaccount, BankLocation, True, trans)
    '                '' Anubhooti 08-Jan-2014 (Advance A/C loc should be overrite from new Loc )
    '                If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                    obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, strGLLoc, True, trans)
    '                Else
    '                    stradvance = stradvance
    '                    obj.objRemittance.Branch_GL_AC = obj.objRemittance.Branch_GL_AC
    '                End If

    '                Dim acc4() As String = {stradvance, (paymentAmt + tds)}  '{stradvance, (paymentAmt + tds)}
    '                Dim acc3() As String = {obj.objRemittance.Branch_GL_AC, -1 * tds} '{strtaxaccount, -1 * tds}
    '                Dim acc5() As String = {bankAccount, crtotal - bankCharges - (+EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                Dim arrtotal As New ArrayList()
    '                arrtotal.Add(acc4)
    '                arrtotal.Add(acc3)
    '                arrtotal.Add(acc5)
    '                If bankCharges > 0 And clsCommon.myLen(BankChargesAc) > 0 Then
    '                    arrtotal.Add(BankCharge)
    '                End If
    '                '' MULTICURRENCY
    '                If IsMultiCurrency Then
    '                    If EXCHANGE_LOSS_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                        arrtotal.Add(CURR_EXCHANGE)
    '                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                        arrtotal.Add(CURR_EXCHANGE)
    '                    End If
    '                End If

    '                '' END MULTICURRENCY


    '                '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location)
    '                If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    'stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
    '                    End If
    '                    Dim BranchAccCR = New String() {strTemp, drtotal}
    '                    arrtotal.Add(BranchAccCR)

    '                    strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
    '                    End If
    '                    Dim BranchAccDR = New String() {strTemp, -1 * drtotal}
    '                    arrtotal.Add(BranchAccDR)

    '                End If
    '                ''
    '                If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                    isSourceCode = True
    '                End If

    '            Else
    '                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "RC") = CompairStringResult.Equal Then
    '                    drtotal = drtotal * -1
    '                    crtotal = crtotal * -1
    '                End If

    '                '---------------------------------------------------------
    '                Dim arrlist As New ArrayList()
    '                'If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "RC") = CompairStringResult.Equal Then
    '                '    Dim arr6() As String = {stradvance, drtotal}
    '                '    Dim arr7() As String = {bankAccount, crtotal - (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT)}
    '                '    arrlist.Add(arr6)
    '                '    arrlist.Add(arr7)
    '                '    '' MULTICURRENCY
    '                '    If IsMultiCurrency Then
    '                '        If EXCHANGE_LOSS_AMT > 0 Then
    '                '            Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, -EXCHANGE_LOSS_AMT}
    '                '            arrlist.Add(CURR_EXCHANGE)
    '                '        ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                '            Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, EXCHANGE_GAIN_AMT}
    '                '            arrlist.Add(CURR_EXCHANGE)
    '                '        End If
    '                '    End If
    '                '    '' END MULTICURRENCY
    '                'Else
    '                '' Anubhooti 08-Jan-2014 (Advance A/C loc should be overrite from new Loc )
    '                If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                Else
    '                    stradvance = stradvance
    '                End If
    '                ''
    '                Dim arr6() As String = {stradvance, drtotal}
    '                ''richa agarwal 01/07/2015
    '                ' Dim arr7() As String = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                Dim arr7() As String = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                ''----------------
    '                Dim arr8() As String = {BankChargesAc, bankCharges}
    '                arrlist.Add(arr6)
    '                arrlist.Add(arr7)
    '                If bankCharges > 0 And clsCommon.myLen(BankChargesAc) > 0 Then
    '                    arrlist.Add(arr8)
    '                End If
    '                '' MULTICURRENCY
    '                If IsMultiCurrency Then
    '                    If EXCHANGE_LOSS_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                        arrlist.Add(CURR_EXCHANGE)
    '                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                        arrlist.Add(CURR_EXCHANGE)
    '                    End If
    '                End If
    '                '' END MULTICURRENCY

    '                '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location,in case of "AV" & "OA")
    '                If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    'stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
    '                    End If
    '                    Dim BranchAccCR = New String() {strTemp, drtotal}
    '                    arrlist.Add(BranchAccCR)

    '                    strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
    '                    End If
    '                    Dim BranchAccDR = New String() {strTemp, -1 * drtotal}
    '                    arrlist.Add(BranchAccDR)
    '                End If
    '                ''
    '                'End If



    '                If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                    isSourceCode = True
    '                End If
    '            End If
    '            If isSourceCode = True Then
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '            End If

    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "OA") = CompairStringResult.Equal Then
    '            Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
    '            Dim strGLLoc As String = clsCommon.myCstr(dt.Rows(0)("Location_GL_Code"))

    '            Dim tds As Double = 0
    '            Dim paymentAmt As Double = 0
    '            Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld  from TSPL_PAYMENT_HEADER where Payment_No = '" + strPaymentNo + "'"
    '            Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(checkall, trans)
    '            If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
    '                tds = clsCommon.myCdbl(dtNew.Rows(0)("TDS_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
    '                paymentAmt = clsCommon.myCdbl(dtNew.Rows(0)("Payment_Amount")) * clsCommon.myCdbl(dtNew.Rows(0)("ConvRateOld"))
    '            End If
    '            ''---richa agarwal 01/07/2015
    '            BankChargesAc = clsERPFuncationality.ChangeGLAccountLocationSegment(BankChargesAc, BankLocation, True, trans)
    '            Dim BankCharge() As String = {BankChargesAc, bankCharges}
    '            ''---------------------

    '            If tds <> 0 And paymentAmt <> 0 Then
    '                '' Anubhooti 27-Nov-2014 
    '                If (obj.objRemittance IsNot Nothing) Then ''is_For_TDS Entry made by ap invoice is come in this section
    '                    obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, BankLocation, True, trans)
    '                Else
    '                    Throw New Exception("Please enter TDS account")
    '                End If
    '                'Dim strquery As String = "select r.Branch_GL_AC   from TSPL_TDS_BRANCH_MASTER bm join TSPL_REMITTANCE r on bm.Branch_Code = r.Branch_Code where r.Document_No = '" + strPaymentNo + "'"
    '                'Dim strtaxaccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strquery, trans))
    '                'strtaxaccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strtaxaccount, BankLocation, True, trans)

    '                '' Anubhooti 08-Jan-2014 (Advance A/C loc should be overrite from new Loc )
    '                If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                    obj.objRemittance.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.objRemittance.Branch_GL_AC, strGLLoc, True, trans)
    '                Else
    '                    stradvance = stradvance
    '                    obj.objRemittance.Branch_GL_AC = obj.objRemittance.Branch_GL_AC
    '                End If
    '                ''
    '                Dim acc4() As String = {stradvance, paymentAmt + tds} ' {stradvance, paymentAmt + tds}
    '                Dim acc3() As String = {obj.objRemittance.Branch_GL_AC, -1 * tds} ' {strtaxaccount, -1 * tds}
    '                Dim acc5() As String = {bankAccount, crtotal - bankCharges - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                Dim arrtotal As New ArrayList()
    '                arrtotal.Add(acc4)
    '                arrtotal.Add(acc3)
    '                arrtotal.Add(acc5)
    '                If bankCharges > 0 And clsCommon.myLen(BankChargesAc) > 0 Then
    '                    arrtotal.Add(BankCharge)
    '                End If
    '                '' MULTICURRENCY
    '                If IsMultiCurrency Then
    '                    If EXCHANGE_LOSS_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                        arrtotal.Add(CURR_EXCHANGE)
    '                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                        arrtotal.Add(CURR_EXCHANGE)
    '                    End If
    '                End If

    '                '' END MULTICURRENCY

    '                '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location)
    '                If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    'stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
    '                    End If
    '                    Dim BranchAccCR = New String() {strTemp, drtotal}
    '                    arrtotal.Add(BranchAccCR)

    '                    strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
    '                    End If
    '                    Dim BranchAccDR = New String() {strTemp, -1 * drtotal}
    '                    arrtotal.Add(BranchAccDR)

    '                End If
    '                ''

    '                If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                    isSourceCode = True
    '                End If
    '            Else
    '                '' Anubhooti 08-Jan-2014 (Advance A/C loc should be overrite from new Loc )
    '                If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                Else
    '                    stradvance = stradvance
    '                End If
    '                ''
    '                Dim arr6() As String = {stradvance, drtotal}
    '                ''richa agarwal 01/07/2015
    '                ' Dim arr7() As String = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                Dim arr7() As String = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                ''-----------------------
    '                Dim arrlist As New ArrayList()
    '                arrlist.Add(arr6)
    '                arrlist.Add(arr7)
    '                If bankCharges > 0 And clsCommon.myLen(BankChargesAc) > 0 Then
    '                    arrlist.Add(BankCharge)
    '                End If

    '                '' MULTICURRENCY
    '                If IsMultiCurrency Then
    '                    If EXCHANGE_LOSS_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                        arrlist.Add(CURR_EXCHANGE)
    '                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                        arrlist.Add(CURR_EXCHANGE)
    '                    End If
    '                End If
    '                '' END MULTICURRENCY


    '                '' Anubhooti 08-Jan-2014 BM00000005309 (Branch Accounting From Location To Bank A/C Location,in case of "AV" & "OA")
    '                If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
    '                    'stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, strGLLoc, True, trans)
    '                    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
    '                    End If
    '                    Dim BranchAccCR = New String() {strTemp, drtotal}
    '                    arrlist.Add(BranchAccCR)

    '                    strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
    '                    If clsCommon.myLen(strTemp) <= 0 Then
    '                        Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
    '                    End If
    '                    Dim BranchAccDR = New String() {strTemp, -1 * drtotal}
    '                    arrlist.Add(BranchAccDR)
    '                End If
    '                ''
    '                If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                    isSourceCode = True
    '                End If
    '            End If
    '            Dim InvcNo As String = ""
    '            Dim BalAmt As Decimal = 0.0
    '            Dim PayAmt As Decimal = drtotal
    '            Dim strQ As String = "select Document_No,Due_Date ,case when fifo_balance>0 then fifo_balance else   Balance_Amt end as  Balance_Amt  from TSPL_VENDOR_INVOICE_HEAD where Balance_Amt>0  and Vendor_Code='" + strsrctypecode + "' and fifo_knockoff='N'" & _
    '                                 "order by TSPL_VENDOR_INVOICE_HEAD.Due_Date "
    '            Dim Dt1 As DataTable = New DataTable()
    '            Dt1 = clsDBFuncationality.GetDataTable(strQ, trans)
    '            For Each dr As DataRow In Dt1.Rows
    '                InvcNo = dr.Item("Document_No").ToString()
    '                BalAmt = dr.Item("Balance_Amt")
    '                If drtotal > BalAmt Then
    '                    drtotal = drtotal - BalAmt
    '                    strQ = "update TSPL_VENDOR_INVOICE_HEAD set fifo_balance=0.00 , fifo_knockoff='Y' where Document_No ='" + InvcNo + "' and Vendor_Code ='" + strsrctypecode + "'"
    '                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
    '                ElseIf drtotal < BalAmt Then
    '                    drtotal = drtotal - BalAmt
    '                    strQ = "update TSPL_VENDOR_INVOICE_HEAD set fifo_balance=" + (drtotal * -1).ToString() + "-fifo_balance , fifo_knockoff='N' where Document_No ='" + InvcNo + "' and Vendor_Code ='" + strsrctypecode + "'"
    '                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
    '                End If
    '                If drtotal < 0 Then
    '                    Exit For
    '                End If
    '            Next
    '            If drtotal > 0 Then
    '                strQ = "update TSPL_PAYMENT_HEADER set fifo_balance=" + drtotal.ToString() + " where Payment_No ='" + strPaymentNo + "'"
    '                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
    '            End If

    '            If isSourceCode = True Then
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '            End If

    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "MI") = CompairStringResult.Equal Then
    '            Dim arrmis As New ArrayList()
    '            Dim ESiAmt As Decimal = 0.0
    '            Dim MiscAmt As Decimal = 0.0
    '            Dim ESI_Percent As Decimal = 0.0
    '            qry = "select TSPL_PAYMENT_detail.Account_code,TSPL_PAYMENT_detail.Net_Balance,TSPL_PAYMENT_detail.Remarks,TSPL_PAYMENT_HEADER.ConvRateOld from TSPL_PAYMENT_detail inner join TSPL_PAYMENT_HEADER on " & _
    '            " TSPL_PAYMENT_detail.payment_no=TSPL_PAYMENT_HEADER.payment_no where TSPL_PAYMENT_detail.Payment_No='" + strPaymentNo + "'"
    '            Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

    '            If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
    '                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
    '                For Each dr As DataRow In dtNew.Rows
    '                    Dim strAccountLocation As String = clsCommon.myCstr(dr("Account_code")).Substring(clsCommon.myLen(clsCommon.myCstr(dr("Account_code"))) - 3, 3)
    '                    Dim dblAmount As Double = clsCommon.myCdbl(dr("Net_Balance")) * clsCommon.myCdbl(dr("ConvRateOld"))
    '                    If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(BankLocation, strAccountLocation) = CompairStringResult.Equal) Then
    '                        Dim Acc4() As String = {bankAccount, -1 * dblAmount}
    '                        arrmis.Add(Acc4)

    '                        Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strAccountLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strAccountLocation)
    '                        End If
    '                        Dim RcvblAcc = New String() {strTemp, dblAmount}
    '                        arrmis.Add(RcvblAcc)

    '                        Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), dblAmount, clsCommon.myCstr(dr("Remarks"))}
    '                        arrmis.Add(acc3)

    '                        strTemp = ClsBranchAccountMapping.GetBranchAccount(strAccountLocation, BankLocation, trans)
    '                        If clsCommon.myLen(strTemp) <= 0 Then
    '                            Throw New Exception("Please set Brach account mapping with from location " + strAccountLocation + " and to location " + BankLocation)
    '                        End If
    '                        RcvblAcc = New String() {strTemp, -1 * dblAmount}
    '                        arrmis.Add(RcvblAcc)
    '                    Else
    '                        Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), dblAmount, clsCommon.myCstr(dr("Remarks"))}
    '                        arrmis.Add(acc3)

    '                        Dim Acc4() As String = {bankAccount, -1 * dblAmount}
    '                        arrmis.Add(Acc4)
    '                    End If



    '                    If clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) < 0 Then
    '                        ESiAmt = ESiAmt + (clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) * clsCommon.myCdbl(dr("ConvRateOld")) * -1)
    '                    End If
    '                Next
    '            End If


    '            Dim strbankacct1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(dt.Rows(0)("Bank_Code")) + "'", trans))
    '            If SettlementLoc <> "" Then
    '                bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, SettlementLoc, False, trans)
    '            End If
    '            If -ESiAmt - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT) <> 0 Then
    '                Dim Acc4() As String = {bankAccount, -ESiAmt - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
    '                arrmis.Add(Acc4)
    '            End If


    '            If bankCharges > 0 And clsCommon.myLen(BankChargesAc) > 0 Then
    '                Dim BankCharge() As String = {BankChargesAc, bankCharges}
    '                arrmis.Add(BankCharge)
    '            End If
    '            If ESiAmt <> 0 Then
    '                Dim Acc5() As String = {bankAccount, ESiAmt}
    '                arrmis.Add(Acc5)
    '            End If

    '            '' MULTICURRENCY
    '            If IsMultiCurrency Then
    '                If EXCHANGE_LOSS_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                    arrmis.Add(CURR_EXCHANGE)
    '                ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                    arrmis.Add(CURR_EXCHANGE)
    '                End If
    '            End If
    '            '' END MULTICURRENCY

    '            sourceType = "AP-MI"
    '            If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                isSourceCode = True
    '            End If
    '            If isSourceCode = True Then
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '                For Each dr As DataRow In dtNew.Rows
    '                    Payment_Line_No = Payment_Line_No + 1
    '                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'  and Payment_Line_No=" & Payment_Line_No & " ", trans)
    '                Next
    '            End If
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Payment_Type")), "AD") = CompairStringResult.Equal Then
    '            Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Total_Prepayment*ConvRateOld  from  TSPL_PAYMENT_HEADER where Payment_No like '" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'  and Vendor_Code = '" + clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) + "'", trans))
    '            Dim arrcontrol() As String = {straccount, clsCommon.myCdbl(dt.Rows(0)("Payment_Amount")) * clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))}
    '            Dim arradvance() As String = {stradvance, -1 * clsCommon.myCdbl(dt.Rows(0)("Payment_Amount")) * clsCommon.myCdbl(dt.Rows(0)("ConvRateOld")) - EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT}
    '            Dim applydocument As New ArrayList()
    '            applydocument.Add(arrcontrol)
    '            applydocument.Add(arradvance)

    '            '' MULTICURRENCY
    '            If IsMultiCurrency Then
    '                If EXCHANGE_LOSS_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, EXCHANGE_LOSS_AMT}
    '                    applydocument.Add(CURR_EXCHANGE)
    '                ElseIf EXCHANGE_GAIN_AMT > 0 Then
    '                    Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, -EXCHANGE_GAIN_AMT}
    '                    applydocument.Add(CURR_EXCHANGE)
    '                End If
    '            End If
    '            '' END MULTICURRENCY

    '            If (transportSql.FunGrnlEntryWithTrans(BankLocation, True, trans, PostDate, paymentDesc, sourceType, sourceDesc, strPaymentNo, strentrydesc, strsrctype, strsrctypecode, strsrctypedesc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, applydocument, , clsCommon.myCstr(dt.Rows(0)("Reference")), clsCommon.myCstr(dt.Rows(0)("Narration")), coll)) Then
    '                isSourceCode = True
    '            End If
    '            If isSourceCode = True Then
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
    '            End If

    '        End If
    '        If isSourceCode = True Then
    '            qry = "update TSPL_REMITTANCE set Remit_TDS='N' where Document_No='" + strPaymentNo + "'"
    '            clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '            'Dim stramount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT d.Applied_Amount FROM  TSPL_PAYMENT_HEADER h INNER JOIN TSPL_PAYMENT_DETAIL d ON h.Payment_No = d.Payment_No where h.Payment_No = '" + strPaymentNo + "'", trans))
    '            If transOPen Is Nothing Then
    '                trans.Commit()
    '            End If
    '            Return True
    '        Else
    '            Throw New Exception("GL Entry could not created for document " & strPaymentNo & "")
    '            'Return False
    '        End If

    '    Catch ex As Exception
    '        Try
    '            If transOPen Is Nothing Then
    '                trans.Rollback()
    '            End If
    '        Catch ex1 As Exception
    '            Throw New Exception(ex1.Message)
    '        End Try

    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    Public Shared Function Create_Security_Credit_Note(ByVal strDocNo As String, ByVal vspcode As String, ByVal obj As clsPaymentHeader, ByVal trans As SqlTransaction) As Boolean
        ' Dim DtJoint As DataTable = clsDBFuncationality.GetDataTable("select vendor_code,vendor_name from TSPL_VENDOR_MASTER where Vendor_Code=(select Joint_Name from tspl_vendor_Master where vendor_code='" & obj.VSP_CODE & "')", trans)
        Dim qry As String = ""
        Dim issaved As Boolean = True

        Dim strICode As String = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel("PYMT-NEW", "TSPL_PAYMENT_HEADER", "Payment_No", obj.Document_No, trans)
        If isResult = False Then
            ' trans.Commit()
            Return False
        End If
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.Vendor_Code & "'", trans)
        For Each objTr As clsPaymentDetail In obj.ArrTr
            If clsCommon.myLen(objTr.Vendor_Invoice_No) > 0 And clsCommon.CompairString(objTr.Vendor_Invoice_No, strDocNo) = CompairStringResult.Equal Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.Security_Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT_New", OMInsertOrUpdate.Update, "Item_Code='" + strICode + "' and Source_Doc_No='" + objTr.Vendor_Invoice_No + "' and Trans_Type='Payment Entry'", trans)

            End If
        Next




        For Each objPIDetail As clsPaymentDetail In obj.ArrTr
            If clsCommon.myLen(objPIDetail.Vendor_Invoice_No) > 0 And clsCommon.CompairString(objPIDetail.Vendor_Invoice_No, strDocNo) = CompairStringResult.Equal Then
                Dim Loc_Code As String = clsDBFuncationality.getSingleValue("SELECT Loc_Code FROM TSPL_VENDOR_INVOICE_HEAD where Document_No='" & objPIDetail.Document_No & "'", trans)
                obj.Location_Code = Loc_Code
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy")
                objVendorInvHead.Vendor_Code = obj.Vendor_Code 'DtJoint.Rows(0)("Vendor_Code")
                objVendorInvHead.Vendor_Name = vendor_name 'DtJoint.Rows(0)("Vendor_name")
                objVendorInvHead.Vendor_Invoice_No = objPIDetail.Vendor_Invoice_No
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy")
                objVendorInvHead.loc_code = obj.Location_Code
                'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans)) 'DtJoint.Rows(0)("Vendor_Code")
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Code) 'DtJoint.Rows(0)("Vendor_name")
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



                'objVendorInvHead.Description = "Incentive of Vendor " + obj.VSP_CODE 'DtJoint.Rows(0)("Vendor_Code") + "/" + DtJoint.Rows(0)("Vendor_name") + " .Against PO Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
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
                Dim sQuery As String = "Update Tspl_payment_detail set Ap_Invoice_no='" & objVendorInvHead.Document_No & "' where payment_no='" & obj.Payment_No & "' and vendor_Invoice_No= '" & objPIDetail.Vendor_Invoice_No & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            End If
        Next
        Return issaved
    End Function

    Public Shared Function Create_Security_Debit_Note(ByVal strDocNo As String, ByVal vspcode As String, ByVal obj As clsPaymentHeader, ByVal trans As SqlTransaction) As Boolean
        ' Dim DtJoint As DataTable = clsDBFuncationality.GetDataTable("select vendor_code,vendor_name from TSPL_VENDOR_MASTER where Vendor_Code=(select Joint_Name from tspl_vendor_Master where vendor_code='" & obj.VSP_CODE & "')", trans)
        Dim qry As String = ""
        Dim issaved As Boolean = True

        Dim strICode As String = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel("PYMT-NEW", "TSPL_PAYMENT_HEADER", "Payment_No", obj.Document_No, trans)
        If isResult = False Then
            ' trans.Commit()
            Return False
        End If
        Dim vendor_name As String = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj.Vendor_Code & "'", trans)
        For Each objTr As clsPaymentDetail In obj.ArrTr
            If clsCommon.myLen(objTr.Vendor_Invoice_No) > 0 And clsCommon.CompairString(objTr.Vendor_Invoice_No, strDocNo) = CompairStringResult.Equal Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.Security_Amount)
                clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.Security_Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT_New", OMInsertOrUpdate.Update, "Item_Code='" + strICode + "' and Source_Doc_No='" + objTr.Vendor_Invoice_No + "' and Trans_Type='Payment Entry'", trans)

            End If
        Next




        For Each objPIDetail As clsPaymentDetail In obj.ArrTr
            If clsCommon.myLen(objPIDetail.Vendor_Invoice_No) > 0 And clsCommon.CompairString(objPIDetail.Vendor_Invoice_No, strDocNo) = CompairStringResult.Equal Then
                Dim Loc_Code As String = clsDBFuncationality.getSingleValue("SELECT Loc_Code FROM TSPL_VENDOR_INVOICE_HEAD where Document_No='" & objPIDetail.Document_No & "'", trans)
                obj.Location_Code = Loc_Code
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy")
                objVendorInvHead.Vendor_Code = obj.Vendor_Code 'DtJoint.Rows(0)("Vendor_Code")
                objVendorInvHead.Vendor_Name = vendor_name 'DtJoint.Rows(0)("Vendor_name")
                objVendorInvHead.Vendor_Invoice_No = objPIDetail.Vendor_Invoice_No
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy")
                objVendorInvHead.loc_code = obj.Location_Code
                'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans)) 'DtJoint.Rows(0)("Vendor_Code")
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Code) 'DtJoint.Rows(0)("Vendor_name")
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



                'objVendorInvHead.Description = "Incentive of Vendor " + obj.VSP_CODE 'DtJoint.Rows(0)("Vendor_Code") + "/" + DtJoint.Rows(0)("Vendor_name") + " .Against PO Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
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
                Dim sQuery As String = "Update Tspl_payment_detail set Ap_Invoice_no='" & objVendorInvHead.Document_No & "' where payment_no='" & obj.Payment_No & "' and vendor_Invoice_No= '" & objPIDetail.Vendor_Invoice_No & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            End If
        Next
        Return issaved
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
        Return ReverseAndUnpost(strCode, trans, True)
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal isUpdateAllAPInvocieBalanceAmt As Boolean) As Boolean
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Posted from TSPL_PAYMENT_HEADER where Payment_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
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

            '' to check bank reverse
            Qry = "select Reverse_Code from TSPL_BANK_REVERSE where Document_No='" + strCode + "' and Source_Type='AP'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Bank Reverse -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Reverse_Code"))
                Next
                Throw New Exception(Qry)
            End If

            ''to check applied document is used in further document whose bank reverse is not done richa agarwal
            Qry = "Select Payment_no from TSPL_PAYMENT_HEADER WHERE Applied_Payment ='" + strCode + "' and isnull(IsChkReverse,'')='N'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Payment Entry -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Payment_no"))
                Next
                Throw New Exception(Qry)
            End If

            ''to check payment process ERO/02/05/20-001226 
            Qry = "select Doc_No from TSPL_PAYMENT_PROCESS_DETAIL where PP_Detail_No  in (select Against_PP_Detail_No from tspl_payment_header where Payment_no='" + strCode + "')"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Payment Process -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Doc_No"))
                Next
                Throw New Exception(Qry)
            End If

            ''to check advance document used into payment process ERO/02/05/20-001226
            Qry = "select Doc_No from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT where  payment_no='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Payment Process -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Doc_No"))
                Next
                Throw New Exception(Qry)
            End If

            ''to check misc document used into payment process ERO/04/08/20-001321 07 Aug,2020
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_type from TSPL_PAYMENT_HEADER where PAyment_no='" + strCode + "'", trans)), "MI") = CompairStringResult.Equal Then
                Qry = "Select top 1 Payment_Process_Code Doc_No from TSPL_MP_PAY_HEAD where Misc_Payment_No='" + strCode + "'"
                dt = clsDBFuncationality.GetDataTable(Qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Qry = "Current document is used in following Payment Process Farmer-"
                    For Each dr As DataRow In dt.Rows
                        Qry += Environment.NewLine + clsCommon.myCstr(dr("Doc_No"))
                    Next
                    Throw New Exception(Qry)
                End If
            End If
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where (Source_Code like 'AP%' or Source_Code='GL-JE' ) and  Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            '' TO DELETE DATA FROM JOURNAL MASTER OPENING TABLE
            Dim VoucherNoOP As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER_OP where (Source_Code like 'AP%' or Source_Code='GL-JE' ) and  Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNoOP) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS_OP where Voucher_No ='" + VoucherNoOP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER_OP where Voucher_No ='" + VoucherNoOP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            '' to update remittance BHA/22/02/19-000818 done by richa 22 Feb,2019
            Qry = "update TSPL_REMITTANCE set Remit_TDS=NULL where Document_No='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            Qry = "Update TSPL_PAYMENT_HEADER set Posted = '0' where Payment_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_PAYMENT_DETAIL set Post = '0' where Payment_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_PAYMENT_HEADER set Balance_Amt=Balance_Amt+ISNULL((Select Payment_Amount from TSPL_PAYMENT_HEADER WHERE Payment_No='" + strCode + "'),0) WHERE Payment_No=(Select Applied_Payment from TSPL_PAYMENT_HEADER WHERE Payment_No='" + strCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            If isUpdateAllAPInvocieBalanceAmt Then
                Dim strpaymentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Payment_Type from TSPL_PAYMENT_HEADER WHERE Payment_No='" + strCode + "'", trans))
                If clsCommon.CompairString(strpaymentType, "AD") = CompairStringResult.Equal Or clsCommon.CompairString(strpaymentType, "PY") = CompairStringResult.Equal Then
                    Xtra.UpdateAPInvoiceBalanceAmount("", trans)
                End If
            End If
            'richa upadte balance in case of debit note when we apply it as advance/onaccount
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Payment_Type from TSPL_PAYMENT_HEADER WHERE Payment_No='" + strCode + "'", trans)), "AD") = CompairStringResult.Equal Then
                Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_VENDOR_INVOICE_HEAD where Document_No=(Select Applied_Payment from TSPL_PAYMENT_HEADER WHERE Payment_No='" + strCode + "')", trans))
                If clsCommon.CompairString(strdocumentType, "D") = CompairStringResult.Equal Then
                    Qry = " Update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt=Balance_Amt+ISNULL((Select Payment_Amount from TSPL_PAYMENT_HEADER WHERE Payment_No='" + strCode + "'),0) WHERE Document_No=(Select Applied_Payment from TSPL_PAYMENT_HEADER WHERE Payment_No='" + strCode + "') "
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                End If
            End If
            ''--------------
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, strCode, "TSPL_PAYMENT_HEADER", "Payment_No", "TSPL_PAYMENT_DETAIL", "Payment_No", "TSPL_PAYMENT_BANK_CHARGES_TAX", "Payment_No", "TSPL_PAYMENT_DETAIL_GST", "Payment_No", "", "", "", "", "", "", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_REMITTANCE", "Document_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_bank_book", "SOURCEDOC_NO", trans)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetExchangeDetailDt(ByVal VendorCode As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim strq As String
        strq = "SELECT TSPL_VENDOR_MASTER.Vendor_Account,TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT,TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT" & _
               " FROM TSPL_VENDOR_MASTER LEFT JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_MASTER.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code " & _
               " WHERE TSPL_VENDOR_MASTER.Vendor_Code='" & VendorCode & "'"
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
            Dim obj As clsPaymentHeader = clsPaymentHeader.GetData(strPaymentNo, NavigatorType.Current, trans)
            If obj Is Nothing Then
                Throw New Exception("Document No. not found to Post")
            Else
                '--------Checks Whertrher Transaction Is Locked Or Not------------UDL/24/07/18-000206 richa 
                LocSegmentCode = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans)
                Dim strAllowtoUnlockTransactionsforSetOff As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoUnlockTransactionsforSetOff, clsFixedParameterCode.AllowtoUnlockTransactionsforSetOff, trans))
                If clsCommon.CompairString(strAllowtoUnlockTransactionsforSetOff, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                Else
                    clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, obj.Payment_Date, trans)
                End If
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
            obj.objRemittance = clsRemittance.GetData(strPaymentNo, trans)
            Dim strRecreateVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from  TSPL_JOURNAL_MASTER where Source_Code<>'GL-JE' and  (Source_Code ='AP-PY' and Source_Doc_No ='" & obj.Payment_No & "' and voucher_desc='" & obj.Entry_Desc & "' )", trans))
            ' Dim ERPStartDate As Date = clsCommon.myCDate(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)) = 1, True, False))
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal) And (clsCommon.CompairString(clsCommon.myCstr(obj.Is_Security), "1") = CompairStringResult.Equal) AndAlso clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, Description,103) from TSPL_FIXED_PARAMETER where code ='ERPStartDate' and Type ='ERPStartDate'", trans)) > clsCommon.myCDate(obj.Payment_Date) Then

                ''richa 11 fEB,2019  TEC/05/02/19-000412 create journal entry for opening in case of  Receipt (Security) as Journal Master table instead of journal master op table
                'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
                Dim JEWithOPening As Boolean = False
                If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                    Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                    If clsCommon.myCDate(obj.Payment_Date) <= clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy") Then
                        JEWithOPening = True
                    End If
                End If

                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, trans)), "1") = CompairStringResult.Equal And JEWithOPening = True Then
                    CreateJournalEntryForOpening(obj, Module_Code, strRecreateVoucherNo, trans)
                End If
            Else
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSkipJournalEntryofPaymentandReceiptforAD, clsFixedParameterCode.AllowtoSkipJournalEntryofPaymentandReceiptforAD, trans)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal) Then
                Else
                    ''richa 12 Nov,2018  TEC/02/11/18-000362 create journal entry for opening in case of Misc Payment and Receipt (Security) as Journal Master table instead of journal master op table
                    'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
                    Dim JEWithOPening As Boolean = False
                    If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                        Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                        If clsCommon.myCDate(obj.Payment_Date) <= clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy") Then
                            JEWithOPening = True
                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, trans)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal) And JEWithOPening = True Then
                        CreateJournalEntryForOpening(obj, Module_Code, strRecreateVoucherNo, trans)
                    Else
                        CreateJournalEntry(obj, Module_Code, strRecreateVoucherNo, trans)
                    End If
                    ' CreateJournalEntry(obj, Module_Code, strRecreateVoucherNo, trans)
                End If

            End If

            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "PY") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal) Then
                Dim strQry As String = "Select TSPL_PAYMENT_DETAIL.Vendor_Invoice_No,case when isnull(TSPL_VENDOR_INVOICE_HEAD.ConvRate,0)=0 then 1 else TSPL_VENDOR_INVOICE_HEAD.ConvRate end  as ConvRateOld, TSPL_PAYMENT_DETAIL.Document_No, TSPL_PAYMENT_DETAIL.Applied_Amount,TSPL_PAYMENT_DETAIL.Security_Amount,TSPL_VENDOR_INVOICE_HEAD.Loc_Code "
                strQry += " from TSPL_PAYMENT_HEADER "
                strQry += " INNER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No "
                strQry += "     INNER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_PAYMENT_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No  "
                strQry += " Where TSPL_PAYMENT_HEADER.Vendor_Code='" + obj.Vendor_Code + "' AND TSPL_PAYMENT_HEADER.Payment_No ='" + strPaymentNo + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        If clsCommon.myCdbl(dr("Security_Amount")) > 0 Then
                            Create_Security_Credit_Note(dr("Vendor_Invoice_No"), obj.Vendor_Code, obj, trans)
                        End If
                        strQry = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = Balance_Amt - " + clsCommon.myCstr(dr("Applied_Amount")) + " where vendor_code = '" + obj.Vendor_Code + "' and Vendor_Invoice_No = '" + clsCommon.myCstr(dr("Vendor_Invoice_No")) + "' AND Document_No = '" + clsCommon.myCstr(dr("Document_No")) + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                    'This function chaged Balance Amount of Payment Which is Applied------------
                    clsPaymentHeader.UpdateBalance(clsCommon.myCstr(obj.Applied_Payment), clsCommon.myCdbl(obj.Payment_Amount), trans)

                    'richa upadte balance in case of DEBIT note when we apply it as advance/onaccount
                    Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + obj.Applied_Payment + "'", trans))
                    If clsCommon.CompairString(strdocumentType, "D") = CompairStringResult.Equal Then
                        qry = " Update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt=Balance_Amt - " & obj.Payment_Amount & " WHERE  Document_No='" + obj.Applied_Payment + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                    ''--------------
                End If
                ''KDI/04/06/18-000340 by balwinder on 04/05/2018

                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1',Modify_By='" + objCommonVar.CurrentUserCode + "' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            ElseIf (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1',Modify_By='" + objCommonVar.CurrentUserCode + "' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal Then
                    If Not String.IsNullOrEmpty(obj.CForm_InvoiceNo) Then
                        qry = "update TSPL_PI_HEAD set CFormRecd=1 WHERE PI_No ='" + obj.CForm_InvoiceNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1',Modify_By='" + objCommonVar.CurrentUserCode + "' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal Then
                Dim InvcNo As String = ""
                Dim BalAmt As Decimal = 0.0
                Dim PayAmt As Decimal = drtotal
                Dim strQ As String = "select Document_No,Due_Date ,case when fifo_balance>0 then fifo_balance else   Balance_Amt end as  Balance_Amt  from TSPL_VENDOR_INVOICE_HEAD where Balance_Amt>0  and Vendor_Code='" + obj.Vendor_Code + "' and fifo_knockoff='N'" & _
                                     "order by TSPL_VENDOR_INVOICE_HEAD.Due_Date "
                Dim Dt1 As DataTable = New DataTable()
                Dt1 = clsDBFuncationality.GetDataTable(strQ, trans)
                For Each dr As DataRow In Dt1.Rows
                    InvcNo = dr.Item("Document_No").ToString()
                    BalAmt = dr.Item("Balance_Amt")
                    If drtotal > BalAmt Then
                        drtotal = drtotal - BalAmt
                        strQ = "update TSPL_VENDOR_INVOICE_HEAD set fifo_balance=0.00 , fifo_knockoff='Y' where Document_No ='" + InvcNo + "' and Vendor_Code ='" + obj.Vendor_Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                    ElseIf drtotal < BalAmt Then
                        drtotal = drtotal - BalAmt
                        strQ = "update TSPL_VENDOR_INVOICE_HEAD set fifo_balance=" + (drtotal * -1).ToString() + "-fifo_balance , fifo_knockoff='N' where Document_No ='" + InvcNo + "' and Vendor_Code ='" + obj.Vendor_Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                    End If
                    If drtotal < 0 Then
                        Exit For
                    End If
                Next
                If drtotal > 0 Then
                    strQ = "update TSPL_PAYMENT_HEADER set fifo_balance=" + drtotal.ToString() + " where Payment_No ='" + strPaymentNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                End If
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1',Modify_By='" + objCommonVar.CurrentUserCode + "' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal Then
                qry = "select TSPL_PAYMENT_detail.Account_code,TSPL_PAYMENT_detail.Net_Balance,TSPL_PAYMENT_detail.Remarks,TSPL_PAYMENT_HEADER.ConvRateOld from TSPL_PAYMENT_detail inner join TSPL_PAYMENT_HEADER on " & _
                " TSPL_PAYMENT_detail.payment_no=TSPL_PAYMENT_HEADER.payment_no where TSPL_PAYMENT_detail.Payment_No='" + strPaymentNo + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1',Modify_By='" + objCommonVar.CurrentUserCode + "' where Payment_No = '" + strPaymentNo + "'", trans)
                For Each dr As DataRow In dtNew.Rows
                    Payment_Line_No = Payment_Line_No + 1
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'  and Payment_Line_No=" & Payment_Line_No & " ", trans)
                Next
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1',Modify_By='" + objCommonVar.CurrentUserCode + "' where Payment_No = '" + strPaymentNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_DETAIL set Post = '1' where Payment_No = '" + strPaymentNo + "'", trans)
            End If
            qry = "update TSPL_REMITTANCE set Remit_TDS='N' where Document_No='" + strPaymentNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            clsBankReco.SetOutstandingEntry(strPaymentNo, obj.Payment_Date, "Payment", trans)

            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "PY") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal) Then
                CreateTransactionEmailContent(obj, trans)
            End If

            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, strPaymentNo, "TSPL_PAYMENT_HEADER", "Payment_No", "TSPL_PAYMENT_DETAIL", "Payment_No", "TSPL_PAYMENT_BANK_CHARGES_TAX", "Payment_No", "TSPL_PAYMENT_DETAIL_GST", "Payment_No", "", "", "", "", "", "", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strPaymentNo, "TSPL_REMITTANCE", "Document_No", trans)
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

    Private Shared Sub CreateTransactionEmailContent(ByVal obj As clsPaymentHeader, ByVal trans As SqlTransaction)

        Dim Form_ID As String = clsUserMgtCode.PaymentEntryNew
        Dim objContent As clsESContent = clsESContent.GetData(Form_ID, trans)
        Dim strVEmailID As String = clsVendorMaster.GetVendorEmailID(obj.Vendor_Code, trans)
        If objContent IsNot Nothing AndAlso clsCommon.myLen(objContent.EMail_Text) > 0 AndAlso clsCommon.myLen(strVEmailID) > 0 Then
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Subject = clsCommon.myCstr(objContent.EMail_Subject)
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, obj.Payment_No)
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy"))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.Payment_Amount))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Vendor_Code, obj.Vendor_Code)
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Vendor_Name, obj.Vendor_Name)


            objSMSH.Attachment_1_Path = PrintPaymentAdvice(obj.Payment_No, trans, True)

            objSMSH.Email_Text = clsCommon.myCstr(objContent.EMail_Text)
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.PurchaseOrder_No)
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy"))
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.Payment_Amount))
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, obj.Vendor_Code)
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, obj.Vendor_Name)


            objSMSH.arrEMail = New List(Of String)()
            objSMSH.arrEMail.Add(strVEmailID)
            objSMSH.SaveData(Form_ID, objSMSH, trans)
            objSMSH = Nothing
            objContent = Nothing
            strVEmailID = Nothing
        End If
    End Sub

    Public Shared Function PrintPaymentAdvice(ByVal PaymentNo As String, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal IsPDF As Boolean = False) As String
        Dim StrPDFPath As String = Nothing
        Dim qry As String = ""
        'qry += " Select TSPL_PAYMENT_HEADER.Entry_Desc,TSPL_VENDOR_MASTER.Cheque_In_Favour_Of as Vendor_Name_For_Cheque,TSPL_VENDOR_MASTER.bank_code as Vendor_Bank_Code,TSPL_Vendor_Bank_Branch_Details.Bank_IFSC_Code as IFSC_Code,tspl_vendor_bank_Master.Bank_Name as Vendor_Bank_Name,TSPL_Vendor_Bank_Branch_Details.Branch_name as Vendor_Branch_Name,Account_No as Credit_Account,Value, TSPL_PAYMENT_HEADER.Payment_Type,TSPL_COMPANY_MASTER.Comp_Name,TSPL_PAYMENT_HEADER.Bank_Code,TSpl_BANK_MASTER.DESCRIPTION as Bank_Name,case when TSPL_PAYMENT_HEADER.Payment_Type ='PY' then 'Your Inv. No' else 'Po No.' end as PO_No ,case when TSPL_PAYMENT_HEADER.Payment_Type ='PY' then 'Your Inv. Amt' else 'Adv Amt' end as Adv_Amt ,  TSPL_PAYMENT_HEADER.Payment_No, convert(varchar,TSPL_PAYMENT_HEADER.Payment_Date,103) as Payment_Date, TSPL_PAYMENT_HEADER.Vendor_Code, TSPL_PAYMENT_HEADER.Vendor_Name, TSPL_PAYMENT_DETAIL.Document_No as InvoiceNo, YYY.Vendor_Invoice_No, TSPL_PAYMENT_HEADER.Payment_Code as PaymentMode, YYY.InvoiceAmt, YYY.DrNoteAmt, CrNoteAmt,case when TSPL_PAYMENT_HEADER.Payment_Type ='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount else Balance_Amt end * (CASE WHEN YYY.Document_Type ='D' THEN -1 ELSE 1 END)  as PaymentAmt, TSPL_PAYMENT_DETAIL.TDS_Amount, (YYY.InvoiceAmt-YYY.DrNoteAmt+CrNoteAmt-TSPL_PAYMENT_DETAIL.TDS_Amount-TSPL_PAYMENT_DETAIL.Applied_Amount) as [RemainingAmt], TSPL_PAYMENT_DETAIL.Comment,(TSPL_VENDOR_MASTER.Add1+TSPL_VENDOR_MASTER.Add2+TSPL_VENDOR_MASTER.Add3)as Vendor_Address,Comp_Name, Cheque_No, convert(varchar,Cheque_Date,103)as Cheque_Date,case  when TSPL_PAYMENT_HEADER.Payment_Code='Cheque' then TSPL_PAYMENT_HEADER.Cheque_No +'  ' +'Dated' +' ' + convert(varchar,Cheque_Date,103) +' '+'Drawn on' when  TSPL_PAYMENT_HEADER.Payment_Code='DD' then value +' '+'Drawn on' else 'Drawn on' end as Payment  from TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN (Select MAX(Document_Type) as Document_Type ,InvoiceNo, MAX(Vendor_Invoice_No) as Vendor_Invoice_No, MAX(InvoiceAmt) as InvoiceAmt, SUM(DrNoteAmt) as DrNoteAmt, SUM(CrNoteAmt) as CrNoteAmt from ( Select TSPL_VENDOR_INVOICE_HEAD.Document_Type ,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Document_Total as InvoiceAmt, ISNULL(DRHEAD.Document_No,'') as DrNote, case when  TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0) ELSE 0 end as DrNoteAmt, ISNULL(CRHEAD.Document_No,'') as CrNote, case when  TSPL_VENDOR_INVOICE_HEAD.Document_Type<>'D' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0)  ELSE 0  end as CrNoteAmt from TSPL_VENDOR_INVOICE_HEAD LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD DRHEAD ON DRHEAD.RefDocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No AND DRHEAD.RefDocType='AP' AND DRHEAD.Document_Type='D' LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD CRHEAD ON CRHEAD.RefDocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No AND CRHEAD.RefDocType='AP' AND CRHEAD.Document_Type='C' ) XXX GROUP BY InvoiceNo ) YYY ON YYY.InvoiceNo=TSPL_PAYMENT_DETAIL.Document_No LEFT OUTER JOIn TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_HEADER.Comp_Code left join TSpl_BANK_MASTER on TSpl_BANK_MASTER.BANK_CODE =TSPL_PAYMENT_HEADER.Bank_Code "
        'qry += " left join TSPL_VENDOR_MASTEr on  TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code  left join tspl_vendor_bank_Master on tspl_vendor_bank_Master.Bank_Code=TSPL_VENDOR_MASTER.Bank_Code left join  TSPL_Vendor_Bank_Branch_Details on TSPL_Vendor_Bank_Branch_Details.Bank_code=tspl_vendor_bank_Master.bank_code  left join (select Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES left join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_VALUES.Custom_Field_Code left join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_CUSTOM_FIELD_VALUES.Program_Code where TSPL_CUSTOM_FIELD_VALUES.Program_Code='PYMT-NEW'  and  Name ='DD Number')tt   on tt.transaction_Code=TSPL_PAYMENT_HEADER.Payment_No  WHERE TSPL_PAYMENT_HEADER.Payment_No = '" & PaymentNo & "' and TSPL_PAYMENT_HEADER.Payment_Type in ('PY','OA','AV')"

        qry += " Select TSPL_PAYMENT_HEADER.Entry_Desc,TSPL_VENDOR_MASTER.Cheque_In_Favour_Of as Vendor_Name_For_Cheque,TSPL_VENDOR_MASTER.bank_code as Vendor_Bank_Code,TSPL_VENDOR_MASTER.IFSC_Code as IFSC_Code,TSPL_VENDOR_MASTER.Bank_Name as Vendor_Bank_Name,TSPL_VENDOR_MASTER.Branch_name as Vendor_Branch_Name,Account_No as Credit_Account,Value, TSPL_PAYMENT_HEADER.Payment_Type,TSPL_COMPANY_MASTER.Comp_Name,TSPL_PAYMENT_HEADER.Bank_Code,TSpl_BANK_MASTER.DESCRIPTION as Bank_Name,case when TSPL_PAYMENT_HEADER.Payment_Type ='PY' then 'Your Inv. No' else 'Po No.' end as PO_No ,case when TSPL_PAYMENT_HEADER.Payment_Type ='PY' then 'Your Inv. Amt' else 'Adv Amt' end as Adv_Amt ,  TSPL_PAYMENT_HEADER.Payment_No, convert(varchar,TSPL_PAYMENT_HEADER.Payment_Date,103) as Payment_Date, TSPL_PAYMENT_HEADER.Vendor_Code, TSPL_PAYMENT_HEADER.Vendor_Name, TSPL_PAYMENT_DETAIL.Document_No as InvoiceNo, YYY.Vendor_Invoice_No, TSPL_PAYMENT_HEADER.Payment_Code as PaymentMode, YYY.InvoiceAmt, YYY.DrNoteAmt, CrNoteAmt,case when (TSPL_PAYMENT_HEADER.Payment_Type ='PY' or TSPL_PAYMENT_HEADER.Payment_Type ='AD') then TSPL_PAYMENT_DETAIL.Applied_Amount else Balance_Amt end * (CASE WHEN YYY.Document_Type ='D' THEN -1 ELSE 1 END)  as PaymentAmt, TSPL_PAYMENT_DETAIL.TDS_Amount, (YYY.InvoiceAmt-YYY.DrNoteAmt+CrNoteAmt-TSPL_PAYMENT_DETAIL.TDS_Amount-TSPL_PAYMENT_DETAIL.Applied_Amount) as [RemainingAmt], TSPL_PAYMENT_DETAIL.Comment,(TSPL_VENDOR_MASTER.Add1+TSPL_VENDOR_MASTER.Add2+TSPL_VENDOR_MASTER.Add3)as Vendor_Address,Comp_Name, Cheque_No, convert(varchar,Cheque_Date,103)as Cheque_Date,case  when TSPL_PAYMENT_HEADER.Payment_Code='Cheque' then TSPL_PAYMENT_HEADER.Cheque_No +'  ' +'Dated' +' ' + convert(varchar,Cheque_Date,103) +' '+'Drawn on' when  TSPL_PAYMENT_HEADER.Payment_Code='DD' then value +' '+'Drawn on' else 'Drawn on' end as Payment  from TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN (Select MAX(Document_Type) as Document_Type ,InvoiceNo, MAX(Vendor_Invoice_No) as Vendor_Invoice_No, MAX(InvoiceAmt) as InvoiceAmt, SUM(DrNoteAmt) as DrNoteAmt, SUM(CrNoteAmt) as CrNoteAmt from ( Select TSPL_VENDOR_INVOICE_HEAD.Document_Type ,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Document_Total as InvoiceAmt, ISNULL(DRHEAD.Document_No,'') as DrNote, case when  TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0) ELSE 0 end as DrNoteAmt, ISNULL(CRHEAD.Document_No,'') as CrNote, case when  TSPL_VENDOR_INVOICE_HEAD.Document_Type<>'D' then ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0)  ELSE 0  end as CrNoteAmt from TSPL_VENDOR_INVOICE_HEAD LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD DRHEAD ON DRHEAD.RefDocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No AND DRHEAD.RefDocType='AP' AND DRHEAD.Document_Type='D' LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD CRHEAD ON CRHEAD.RefDocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No AND CRHEAD.RefDocType='AP' AND CRHEAD.Document_Type='C' ) XXX GROUP BY InvoiceNo ) YYY ON YYY.InvoiceNo=TSPL_PAYMENT_DETAIL.Document_No LEFT OUTER JOIn TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_HEADER.Comp_Code left join TSpl_BANK_MASTER on TSpl_BANK_MASTER.BANK_CODE =TSPL_PAYMENT_HEADER.Bank_Code "
        qry += " left join TSPL_VENDOR_MASTEr on  TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code  left join (select Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES left join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_VALUES.Custom_Field_Code left join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_CUSTOM_FIELD_VALUES.Program_Code where TSPL_CUSTOM_FIELD_VALUES.Program_Code='PYMT-NEW'  and  Name ='DD Number')tt   on tt.transaction_Code=TSPL_PAYMENT_HEADER.Payment_No  WHERE TSPL_PAYMENT_HEADER.Payment_No = '" & PaymentNo & "' and TSPL_PAYMENT_HEADER.Payment_Type in ('PY','OA','AV','AD')"
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            StrPDFPath = frmCRV.funreport(IsPDF, CrystalReportFolder.Purchase, dt2, EnumTecxpertPaperSize.NA, "crptPaymentAdvice", "Payment Advice", clsCommon.myCDate(dt2.Rows(0)("Payment_Date")))
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow("Payment Advice Not Available this mode.")
        End If
        Return StrPDFPath
    End Function


    Public Shared Function CheckNegativeBankBalance(ByVal obj As clsPaymentHeader, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
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

    ''richa TEC/02/11/18-000362 on 16 nov,2018 
    Public Shared Function CreateJournalEntryForOpening(ByVal obj As clsPaymentHeader, ByVal Module_Code As String, ByVal strVoucherNoifExists As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + obj.Bank_Code + "'", trans)
            Dim PostDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select Payment_Date from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No='" + obj.Payment_No + "' ", trans))
            Dim strentrydesc As String = "Payment Against" + " " + obj.Payment_No
            Dim sourceType As String = "GL-JE"
            Dim sourceDesc As String = "PAYMENT"
            Dim paymentDesc As String = clsCommon.myCstr(obj.Entry_Desc)
            If clsCommon.myCstr(obj.Remit_To) <> "" Then
                paymentDesc += " " + clsCommon.myCstr(obj.Remit_To)
            End If
            Dim strsrctype As String = "V"
            Dim Loc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
            Dim strQ1 As String = String.Empty

            Dim strBankOpeningClearingAcc As String = ""
            Dim strCustomerSecurityOpeningClearingAcc As String = ""
            Dim dblAmount As Decimal = clsCommon.myCdbl(obj.Payment_Amount)
            Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
            Dim bankAccount As String
            Dim UseSubAcc As String
            Dim arrlist As New ArrayList()
            'If clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
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
            If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal Then
                sourceDesc = "AP Payment Miscellaneous"
                ''credit ac
                strQ1 = "select isnull(Bank_Opening_Clearing_Account,'') as Bank_Opening_Clearing_Account  from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(obj.Bank_Code) + "'"
                strBankOpeningClearingAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                If clsCommon.myLen(strBankOpeningClearingAcc) <= 0 Then
                    Throw New Exception("Please enter Opening Clearing account for bank " + clsCommon.myCstr(obj.Bank_Code))
                End If

                strBankOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankOpeningClearingAcc, BankLocation, True, trans)

                '' debit a/c
                Dim OpeningClearingAcc() As String = {strBankOpeningClearingAcc, dblAmount * 1, "", "", "", "", "", "", "B"}
                arrlist.Add(OpeningClearingAcc)

                '' credit A/c
                Dim BankAcc() As String = {bankAccount, dblAmount * -1, "", "", "", "", "", "", "B"}
                arrlist.Add(BankAcc)
                strentrydesc = "Bank Opening"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Then
                'Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                Dim strGLLoc As String = clsCommon.myCstr(obj.Location_GL_Code)
                sourceDesc = "AP Payment Receipt Security"
                Dim strColumn As String = ""
                ''debit ac
                strQ1 = "select ISNULL(s.SECURITY_ACCOUNT,'') AS SECURITY_ACCOUNT from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' "

                strBankOpeningClearingAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                If clsCommon.myLen(strBankOpeningClearingAcc) <= 0 Then
                    Throw New Exception("Please fill security account on vendor account set for Vendor " + clsCommon.myCstr(obj.Vendor_Code))
                End If
                If clsCommon.myLen(strGLLoc) > 0 Then
                    strBankOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankOpeningClearingAcc, strGLLoc, True, trans)
                Else
                    strBankOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankOpeningClearingAcc, BankLocation, True, trans)
                End If


                '' credit a/c
                strQ1 = "select ISNULL(s.Security_Opening_Clearing,'') AS Security_Opening_Clearing from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' "
                strCustomerSecurityOpeningClearingAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ1, trans))
                If clsCommon.myLen(strCustomerSecurityOpeningClearingAcc) <= 0 Then
                    Throw New Exception("Please enter Vendor Security Opening Clearing account on vendor account set for Vendor " + clsCommon.myCstr(obj.Vendor_Code))
                End If

                If clsCommon.myLen(strGLLoc) > 0 Then
                    strCustomerSecurityOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustomerSecurityOpeningClearingAcc, strGLLoc, True, trans)
                Else
                    strCustomerSecurityOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustomerSecurityOpeningClearingAcc, BankLocation, True, trans)
                End If


                'If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                '    strBankOpeningClearingAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strBankOpeningClearingAcc, strGLLoc, True, trans)
                'Else
                '    strBankOpeningClearingAcc = strBankOpeningClearingAcc
                'End If
                '' debit a/c
                Dim SecurityAcc() As String = {strBankOpeningClearingAcc, dblAmount, "", "", "", "", "", "", "B"}
                arrlist.Add(SecurityAcc)

                '' credit A/c
                Dim SecurityOpeningClearing() As String = {strCustomerSecurityOpeningClearingAcc, dblAmount * -1, "", "", "", "", "", "", "B"}
                arrlist.Add(SecurityOpeningClearing)
                strentrydesc = "Vendor Security"

                ''richa agarwal apply branch accounting

                'If isApplyBrachAccounting AndAlso clsCommon.myLen(strGLLoc) > 0 AndAlso Not (clsCommon.CompairString(BankLocation, strGLLoc) = CompairStringResult.Equal) Then
                '    Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strGLLoc, trans)
                '    If clsCommon.myLen(strTemp) <= 0 Then
                '        Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strGLLoc)
                '    End If
                '    Dim BranchAccCR = New String() {strTemp, dblAmount}
                '    arrlist.Add(BranchAccCR)

                '    strTemp = ClsBranchAccountMapping.GetBranchAccount(strGLLoc, BankLocation, trans)
                '    If clsCommon.myLen(strTemp) <= 0 Then
                '        Throw New Exception("Please set Branch account mapping with from location " + strGLLoc + " and to location " + BankLocation)
                '    End If
                '    Dim BranchAccDR = New String() {strTemp, -1 * dblAmount}
                '    arrlist.Add(BranchAccDR)
                'End If

            End If
            ''richa TEC/28/11/18-000377 28 Nov,2018
            ' transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, strentrydesc, sourceType, sourceDesc, obj.Payment_No, "", IIf(clsCommon.CompairString(obj.Payment_Type, "MI") = CompairStringResult.Equal, "O", "V"), obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), Nothing)
            transportSql.FunGrnlEntryWithTrans(True, 0, "", "N", BankLocation, True, False, strVoucherNoifExists, trans, PostDate, strentrydesc, sourceType, sourceDesc, obj.Payment_No, "", IIf(clsCommon.CompairString(obj.Payment_Type, "MI") = CompairStringResult.Equal, "O", "V"), obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), Nothing)

            '' to update VSP Code against Farmer code into Journal master
            If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(obj.MP_Code_For_Advance)) > 0 Then
                Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where source_doc_no ='" & obj.Payment_No & "' and source_code='AP-MI'", trans))
                If clsCommon.myLen(strVoucherNo) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("update tspl_journal_master set VSP_code=(select TSPL_VLC_MASTER_HEAD.VSP_Code  from TSPL_MP_MASTER inner JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MP_MASTER.VLC_Code where mp_code='" & obj.MP_Code_For_Advance & "') where Voucher_No ='" & strVoucherNo & "' and source_code='AP-MI' ", trans)
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    '=========BM00000008292
    Public Shared Function CreateJournalEntry(ByVal obj As clsPaymentHeader, ByVal Module_Code As String, ByVal strVoucherNoifExists As String, ByVal trans As SqlTransaction) As Boolean
        'Dim isSourceCode As Boolean = False
        Dim isSaved As Boolean = True
        Dim Payment_Line_No As Integer = 0
        Dim qry As String = ""

        Try
            Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + obj.Bank_Code + "'", trans)
            Dim PostDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select Payment_Date from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No='" + obj.Payment_No + "' ", trans))
            Dim strentrydesc As String = "Payment Against" + " " + obj.Payment_No
            Dim sourceType As String = "AP-PY"
            Dim sourceDesc As String = "PAYMENT"
            Dim paymentDesc As String = clsCommon.myCstr(obj.Entry_Desc)
            Dim isReceipt As String = obj.isReceipt
            If clsCommon.myCstr(obj.Remit_To) <> "" Then
                paymentDesc += " " + clsCommon.myCstr(obj.Remit_To)
            End If
            ''richa 7 Feb,2020
            Dim objJE As New clsJEExtraColumns
            If clsCommon.myLen(obj.TapalNo) > 0 Or clsCommon.myLen(obj.DateAndTime) > 0 Then
                objJE.TapalNo = clsCommon.myCstr(obj.TapalNo)
                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    objJE.DateAndTime = obj.DateAndTime
                End If
            End If
            Dim strsrctype As String = "V"
            Dim Loc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
            Dim straccount As String = String.Empty
            If clsCommon.CompairString(obj.Payment_Type, "PY") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select s.Payable_Account,s.Discount_Account,s.Advance_Against_Imprest ,s.Advance_Against_Travelling,s.Employee_Salary  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "TD") = CompairStringResult.Equal Then
                        straccount = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "T") = CompairStringResult.Equal Then
                        straccount = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "S") = CompairStringResult.Equal Then
                        straccount = clsCommon.myCstr(dt.Rows(0)("Employee_Salary"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "I") = CompairStringResult.Equal Then
                        straccount = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                    Else
                        straccount = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    End If
                End If
            Else
                straccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
            End If
            If Not clsCommon.CompairString(obj.Payment_Type, "MI") = CompairStringResult.Equal Then
                If clsCommon.myLen(straccount) <= 0 Then
                    Throw New Exception("Please enter Vendor Account in Vendor Account Set for Vendor " + clsCommon.myCstr(obj.Vendor_Code))
                End If
            End If
           

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
            If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal And (clsCommon.CompairString(obj.Employee_Type, "") <> CompairStringResult.Equal Or clsCommon.CompairString(obj.Employee_Advance_Type, "") <> CompairStringResult.Equal) Then
                    stradvance = obj.Credit_Account
                Else
                    stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
                End If
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
            Else
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
            End If


            '' Anubhooti 31-Mar-2015 (If security is checked then security account will go on GL)
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Is_Security) = 1 Then
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.SECURITY_ACCOUNT,'') AS SECURITY_ACCOUNT from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
                If clsCommon.myLen(stradvance) <= 0 Then
                    Throw New Exception("Please fill security account on vendor account set.")
                End If
            End If
            '' Anubhooti 27-Mar-2015 (Replace AdvanceAccount From AdvanceAgainstSalary In Case Of Advance & On-Account Only)
            '' changes by richa agarwal against ticket no BM00000007565
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Advance_Against_Salary) = 1 Then
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Salary,'') AS Advance_Against_Salary from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
                If clsCommon.myLen(stradvance) <= 0 Then
                    Throw New Exception("Please fill advance against salary account on vendor account set.")
                End If
            End If

            '' changes by richa agarwal against ticket no 
            If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(obj.Employee_Advance_Type, "T") = CompairStringResult.Equal Then
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Travelling,'') AS Advance_Against_Travelling from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
                If clsCommon.myLen(stradvance) <= 0 Then
                    Throw New Exception("Please fill advance against travelling account on vendor account set.")
                End If
            ElseIf (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(obj.Employee_Advance_Type, "I") = CompairStringResult.Equal Then
                stradvance = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Imprest,'') AS Advance_Against_Imprest from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans))
                stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
                If clsCommon.myLen(stradvance) <= 0 Then
                    Throw New Exception("Please fill advance against imprest account on vendor account set.")
                End If
            End If

            ''for on account
            If clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select s.Payable_Account,s.Discount_Account,s.Advance_Against_Imprest ,s.Advance_Against_Travelling,s.Employee_Salary  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "TD") = CompairStringResult.Equal Then
                        stradvance = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "T") = CompairStringResult.Equal Then
                        stradvance = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "S") = CompairStringResult.Equal Then
                        stradvance = clsCommon.myCstr(dt.Rows(0)("Employee_Salary"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "I") = CompairStringResult.Equal Then
                        stradvance = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                    Else
                        stradvance = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    End If
                    stradvance = clsERPFuncationality.ChangeGLAccountLocationSegment(stradvance, BankLocation, True, trans)
                End If
            End If

            ''---------

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
                Dim Credit() As String = Nothing
                If Not (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal) Then
                    If isReceipt = 0 Then
                        Credit = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
                    Else
                        Credit = {bankAccount, -1 * (crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)), "", "", "", "", "", "", "B"}
                    End If

                Else
                    Credit = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                End If

                Dim Debit() As String = {straccount, drtotal}
                ''------------------------------------
                arr.Add(Credit)
                arr.Add(Debit)

            Else
                drtotal = clsCommon.myCdbl(obj.Payment_Amount) * clsCommon.myCdbl(obj.ConvRateOld)
                crtotal = -1 * clsCommon.myCdbl(obj.Payment_Amount) * clsCommon.myCdbl(obj.ConvRateOld)
                bankCharges = clsCommon.myCdbl(obj.Bank_Charges) * clsCommon.myCdbl(obj.ConvRateOld)
                Dim Credit() As String = Nothing
                If Not (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal) Then
                    Credit = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
                Else
                    Credit = {bankAccount, crtotal - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                End If
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
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_Type  from TSPL_PAYMENT_HEADER where PAYMENT_NO ='" & obj.Applied_Payment & "' ", trans)), "AV") = CompairStringResult.Equal Then
                        '' for employee salary integration
                        Dim strAdvanceEmployeeAdvanceType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Employee_Advance_Type  from TSPL_PAYMENT_HEADER where PAYMENT_NO ='" & obj.Applied_Payment & "' ", trans))
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select s.Advance_Against_Imprest ,s.Advance_Against_Travelling,s.Advance_Against_Salary,Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(strAdvanceEmployeeAdvanceType), "T") = CompairStringResult.Equal Then
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(strAdvanceEmployeeAdvanceType), "S") = CompairStringResult.Equal Then
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Salary"))
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(strAdvanceEmployeeAdvanceType), "I") = CompairStringResult.Equal Then
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                            Else
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Advance_Account"))
                            End If
                            bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, tBankLocation, True, trans)
                        End If
                        ''---------
                    Else
                        Dim strAdvanceEmployeeExpenseType As String = String.Empty
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_Type  from TSPL_PAYMENT_HEADER where PAYMENT_NO ='" & obj.Applied_Payment & "' ", trans)), "OA") = CompairStringResult.Equal Then
                            strAdvanceEmployeeExpenseType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Employee_Type  from TSPL_PAYMENT_HEADER where PAYMENT_NO ='" & obj.Applied_Payment & "' ", trans))
                        Else
                            strAdvanceEmployeeExpenseType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Employee_Type from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & obj.Applied_Payment & "' ", trans))
                        End If
                        '' for employee salary integration
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select s.Payable_Account,s.Discount_Account,s.Advance_Against_Imprest ,s.Advance_Against_Travelling,s.Employee_Salary  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "' ", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(strAdvanceEmployeeExpenseType), "TD") = CompairStringResult.Equal Then
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(strAdvanceEmployeeExpenseType), "T") = CompairStringResult.Equal Then
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(strAdvanceEmployeeExpenseType), "S") = CompairStringResult.Equal Then
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Employee_Salary"))
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(strAdvanceEmployeeExpenseType), "I") = CompairStringResult.Equal Then
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                            Else
                                bankAccount = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                            End If

                            bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, tBankLocation, True, trans)
                        End If
                    End If
                End If
                'Dim strQry As String = "select Vendor_Invoice_No,(case when ISNULL(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRateOld end) as ConvRateOld,Document_No,Applied_Amount,Security_Amount,Loc_Code from (Select TSPL_PAYMENT_DETAIL.Vendor_Invoice_No,case when isnull(TSPL_VENDOR_INVOICE_HEAD.ConvRate,0)=0 then 1 else TSPL_VENDOR_INVOICE_HEAD.ConvRate end  as ConvRateOld,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 and convert(date, TSPL_REVALUATION_HEAD.Document_Date,103)<= convert(date, TSPL_PAYMENT_HEADER.Payment_Date,103)  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation, TSPL_PAYMENT_DETAIL.Document_No, TSPL_PAYMENT_DETAIL.Applied_Amount,TSPL_PAYMENT_DETAIL.Security_Amount,TSPL_VENDOR_INVOICE_HEAD.Loc_Code "
                'strQry += " from TSPL_PAYMENT_HEADER "
                'strQry += " INNER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No "
                'strQry += " INNER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_PAYMENT_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No "
                'strQry += " Where TSPL_PAYMENT_HEADER.Vendor_Code='" + obj.Vendor_Code + "' AND TSPL_PAYMENT_HEADER.Payment_No ='" + obj.Payment_No + "')xx"
                Dim strQry As String = "select Vendor_Invoice_No,(case when ISNULL(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRateOld end) as ConvRateOld,Document_No,Applied_Amount,Security_Amount,Loc_Code,Document_Type from (Select TSPL_PAYMENT_DETAIL.Vendor_Invoice_No,case when isnull(TSPL_VENDOR_INVOICE_HEAD.ConvRate,0)=0 then 1 else TSPL_VENDOR_INVOICE_HEAD.ConvRate end  as ConvRateOld,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 and convert(date, TSPL_REVALUATION_HEAD.Document_Date,103)<= convert(date, TSPL_PAYMENT_HEADER.Payment_Date,103)  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation, TSPL_PAYMENT_DETAIL.Document_No, TSPL_PAYMENT_DETAIL.Applied_Amount,TSPL_PAYMENT_DETAIL.Security_Amount, " &
                 " case when len(ISNULL(TSPL_VENDOR_INVOICE_HEAD.Loc_Code,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Loc_Code " + Environment.NewLine +
                " when len(ISNULL(HeadPaymentTable.Location_GL_Code,''))>0  then HeadPaymentTable.Location_GL_Code  " + Environment.NewLine +
                " ELSE (Select right(BANKACC,3) from TSPL_PAYMENT_HEADER left outer join TSPL_BANK_MASTER on TSPL_PAYMENT_HEADER .Bank_Code =TSPL_BANK_MASTER .BANK_CODE  where Payment_No  =TSPL_PAYMENT_DETAIL.Document_No) END AS Loc_Code, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'' THEN 0 ELSE 1 END Document_Type from TSPL_PAYMENT_HEADER " &
                " LEFT OUTER JOIN  TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No " &
                 " LEFT OUTER JOIN  TSPL_VENDOR_INVOICE_HEAD ON TSPL_PAYMENT_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No " &
                 " left outer join TSPL_PAYMENT_HEADER as HeadPaymentTable on HeadPaymentTable.Payment_No=TSPL_PAYMENT_DETAIL.Document_No" + Environment.NewLine +
                 " Where TSPL_PAYMENT_HEADER.Vendor_Code='" + obj.Vendor_Code + "' AND TSPL_PAYMENT_HEADER.Payment_No ='" + obj.Payment_No + "')xx ORDER BY Document_Type "

                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                Dim isApplyBrachAccounting As Boolean = False
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    isApplyBrachAccounting = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                    For Each dr As DataRow In dt1.Rows
                        Dim strDocLocation As String = clsCommon.myCstr(dr("Loc_Code"))
                        '' Debit Note Should be deducted from Applied Amount 25-Aug-2015 BM00000007721 
                        DocType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then 'Debit Note' Else case  When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then 'Credit Note' Else 'Invoice' End End As [DocType]From TSPL_VENDOR_INVOICE_HEAD Where Document_No='" + clsCommon.myCstr(clsCommon.myCstr(dr("Document_No"))) + "'", trans))

                        ''richa agarwal 24/06/2015
                        'Dim dblAmount As Double = clsCommon.myCdbl(dr("Applied_Amount")) * clsCommon.myCdbl(dr("ConvRateOld"))
                        'Dim dblAmount1 As Double = clsCommon.myCdbl(dr("Applied_Amount")) * ConvRate

                        Dim dblAmount As Double = Math.Round(clsCommon.myCdbl(dr("Applied_Amount")) * clsCommon.myCdbl(dr("ConvRateOld")), 2, MidpointRounding.AwayFromZero)
                        Dim dblAmount1 As Double = Math.Round(clsCommon.myCdbl(dr("Applied_Amount")) * ConvRate, 2, MidpointRounding.AwayFromZero)


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
                            RcvblAcc = New String() {bankAccount, -1 * dblAmount1, "", "", "", "", "", "", "B"}

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
                                Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & obj.Applied_Payment & "'", trans))
                                Dim STRGLACCOUNTFORAD As String = String.Empty
                                If clsCommon.CompairString(strdocumentType, "D") = CompairStringResult.Equal Then
                                    STRGLACCOUNTFORAD = clsCommon.myCstr(clsDBFuncationality.getSingleValue("sELECT ISNULL(Loc_Code ,'') FROM TSPL_VENDOR_INVOICE_HEAD WHERE Document_No ='" & obj.Applied_Payment & "'", trans))
                                Else
                                    STRGLACCOUNTFORAD = clsCommon.myCstr(clsDBFuncationality.getSingleValue("sELECT ISNULL(Location_GL_Code ,'') FROM TSPL_PAYMENT_HEADER WHERE Payment_No ='" & obj.Applied_Payment & "'", trans))
                                End If

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
                                Dim RcvblAcc As String() = Nothing
                                If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                                    RcvblAcc = New String() {bankAccount, -1 * dblAmount1}
                                Else
                                    RcvblAcc = New String() {bankAccount, -1 * dblAmount1, "", "", "", "", "", "", "B"}
                                End If

                                arr.Add(RcvblAcc)
                                RcvblAcc = New String() {straccount, dblAmount}
                                arr.Add(RcvblAcc)
                            End If
                        End If
                    Next
                    ' '' richa agarwal 01/07/2015 to add bank charges in bank amount
                    If bankCharges > 0 Then
                        Dim BankChargeCredit() As String = Nothing
                        If Not clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                            BankChargeCredit = {bankAccount, -1 * bankCharges, "", "", "", "", "", "", "B"}
                        Else
                            BankChargeCredit = {bankAccount, -1 * bankCharges}
                        End If
                        arr.Add(BankChargeCredit)
                    End If
                End If


                If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                    ''---richa agarwal 01/07/2015
                    obj.Bank_Charges_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Bank_Charges_Ac, BankLocation, True, trans)
                    ''---------------------
                    Dim BankCharge() As String = {obj.Bank_Charges_Ac, bankCharges}
                    arr.Add(BankCharge)
                    '' add Bank charges tax account done by panch Raj
                    arr = AddBankChargesTaxAccount(obj, bankAccount, BankLocation, arr, trans)
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

                transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arr, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)

            ElseIf (clsCommon.CompairString(obj.Payment_Type, "SR") = CompairStringResult.Equal) Then
                Dim tds As Double = 0
                Dim paymentAmt As Double = 0
                Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld,PAYMENT_AMOUNT_BASE_CURRENCY  from TSPL_PAYMENT_HEADER where Payment_No = '" + obj.Payment_No + "'"
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
                    Dim acc5() As String = {bankAccount, crtotal - bankCharges - (+EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
                    Dim arrtotal As New ArrayList()
                    arrtotal.Add(acc4)
                    arrtotal.Add(acc3)
                    arrtotal.Add(acc5)

                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrtotal.Add(BankCharge)
                        '' add Bank charges tax account done by panch Raj
                        arrtotal = AddBankChargesTaxAccount(obj, bankAccount, BankLocation, arrtotal, trans)
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
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)
                Else
                    Dim arrlist As New ArrayList()
                    Dim arr6() As String = {stradvance, drtotal}
                    Dim arr7() As String = {bankAccount, crtotal - (-EXCHANGE_LOSS_AMT + EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
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

                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)
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
                Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld  from TSPL_PAYMENT_HEADER where Payment_No = '" + obj.Payment_No + "'"
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
                    '' richa agarwal remove bank charges from credit total because it has been added in credit total above BHA/10/08/18-000414
                    'Dim acc5() As String = {bankAccount, crtotal - bankCharges - (+EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
                    Dim acc5() As String = {bankAccount, crtotal - (+EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
                    Dim arrtotal As New ArrayList()
                    arrtotal.Add(acc4)
                    arrtotal.Add(acc3)
                    arrtotal.Add(acc5)
                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrtotal.Add(BankCharge)
                        '' add Bank charges tax account done by panch Raj
                        arrtotal = AddBankChargesTaxAccount(obj, bankAccount, BankLocation, arrtotal, trans)
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
                    ''richa agarwal include tax in case of gst
                    Dim GSTStatus As Boolean = False
                    GSTStatus = clsERPFuncationality.GetGSTStatus(obj.Payment_Date)
                    If Not objCommonVar.TreatUnregisteredVendorAsRegisteredVendor Then
                        If GSTStatus AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Tax_Group)) > 0 AndAlso clsCommon.myCdbl(obj.Tax_Amount_Advance) > 0 Then
                            Dim objTM As clsTaxMaster
                            ' Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                            Dim strDOLocation As String = obj.PO_Location_Code
                            Dim isTaxRecoverable As Boolean = False
                            If clsCommon.myCdbl(obj.TAX1_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.TAX1), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX1))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX1_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX1))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX1_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If

                            If clsCommon.myCdbl(obj.TAX2_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax2, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax2), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax2))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX2_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax2))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX2_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If



                            If clsCommon.myCdbl(obj.TAX3_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.TAX3), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX3))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX3_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX3))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX3_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX4_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.TAX4), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX4))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX4_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX4))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX4_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If



                            If clsCommon.myCdbl(obj.TAX5_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax5, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax5), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax5))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX5_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax5))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX5_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If



                            If clsCommon.myCdbl(obj.TAX6_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax6, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax6), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax6))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX6_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax6))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX6_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If



                            If clsCommon.myCdbl(obj.TAX7_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax7, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax7), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax7))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX7_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax7))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX7_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX8_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax8, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax8), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax8))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX8_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax8))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX8_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX9_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax9, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax9), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax9))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX9_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax9))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX9_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX10_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax10, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax10), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax10))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX10_Amt) * -1}
                                        arrtotal.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax10))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX10_Amt)}
                                        arrtotal.Add(Acc2)
                                    End If
                                End If
                            End If
                        End If
                    End If
                    ''-----------------------------------------


                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)

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
                    Dim arr7() As String = Nothing
                    If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                        arr7 = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    Else
                        arr7 = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
                    End If

                    ''----------------
                    Dim arr8() As String = {obj.Bank_Charges_Ac, bankCharges}
                    arrlist.Add(arr6)
                    arrlist.Add(arr7)
                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrlist.Add(arr8)
                        '' add Bank charges tax account done by panch Raj
                        arrlist = AddBankChargesTaxAccount(obj, bankAccount, BankLocation, arrlist, trans)
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

                    ''richa agarwal include tax in case of gst
                    Dim GSTStatus As Boolean = False
                    GSTStatus = clsERPFuncationality.GetGSTStatus(obj.Payment_Date)
                    If Not objCommonVar.TreatUnregisteredVendorAsRegisteredVendor Then
                        If GSTStatus AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AV") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Tax_Group)) > 0 AndAlso clsCommon.myCdbl(obj.Tax_Amount_Advance) > 0 Then
                            Dim objTM As clsTaxMaster
                            ' Dim strBankLocation As String = strBankAcc.Substring(clsCommon.myLen(strBankAcc) - 3, 3)
                            Dim strDOLocation As String = obj.PO_Location_Code
                            Dim isTaxRecoverable As Boolean = False
                            If clsCommon.myCdbl(obj.TAX1_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.TAX1), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX1))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX1_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX1))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX1_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If

                            If clsCommon.myCdbl(obj.TAX2_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax2, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax2), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax2))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX2_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax2))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX2_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX3_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.TAX3), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX3))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX3_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX3))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX3_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If

                            If clsCommon.myCdbl(obj.TAX4_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.TAX4), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX4))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX4_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.TAX4))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX4_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If



                            If clsCommon.myCdbl(obj.TAX5_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax5, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax5), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax5))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX5_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax5))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX5_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX6_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax6, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax6), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax6))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX6_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax6))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX6_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If



                            If clsCommon.myCdbl(obj.TAX7_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax7, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax7), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax7))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX7_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax7))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX7_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX8_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax8, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax8), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax8))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX8_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax8))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX8_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX9_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax9, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax9), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax9))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX9_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax9))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX9_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If


                            If clsCommon.myCdbl(obj.TAX10_Amt) <> 0 Then
                                isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.tax10, trans)
                                If isTaxRecoverable Then
                                    objTM = clsTaxMaster.GetTaxDetailsForSale(clsCommon.myCstr(obj.tax10), trans)
                                    If objTM IsNot Nothing Then
                                        If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                                            Throw New Exception("Please set Payable Control Account of Tax Authority " + clsCommon.myCstr(obj.tax10))
                                        End If
                                        objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, strDOLocation, False, trans)
                                        Dim Acc1() As String = {objTM.PayableControl, clsCommon.myCdbl(obj.TAX10_Amt) * -1}
                                        arrlist.Add(Acc1)

                                        If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                            Throw New Exception("Please set Deposit Control Account of Tax Authority " + clsCommon.myCstr(obj.tax10))
                                        End If

                                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, strDOLocation, False, trans)
                                        Dim Acc2() As String = {objTM.DepositControl, clsCommon.myCdbl(obj.TAX10_Amt)}
                                        arrlist.Add(Acc2)
                                    End If
                                End If
                            End If

                        End If
                    End If
                    ''-----------------------------------------



                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "OA") = CompairStringResult.Equal Then
                Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                Dim strGLLoc As String = clsCommon.myCstr(obj.Location_GL_Code)
                Dim tds As Double = 0
                Dim paymentAmt As Double = 0
                Dim checkall As String = "select TDS_Amount , Payment_Amount,ConvRateOld  from TSPL_PAYMENT_HEADER where Payment_No = '" + obj.Payment_No + "'"
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
                    Dim acc5() As String = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
                    Dim arrtotal As New ArrayList()
                    arrtotal.Add(acc4)
                    arrtotal.Add(acc3)
                    arrtotal.Add(acc5)
                    If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrtotal.Add(BankCharge)
                        '' add Bank charges tax account done by panch Raj
                        arrtotal = AddBankChargesTaxAccount(obj, bankAccount, BankLocation, arrtotal, trans)
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
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrtotal, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)
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
                    Dim arr7() As String = Nothing
                    If clsCommon.CompairString(obj.Payment_Type, "AD") = CompairStringResult.Equal Then
                        arr7 = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)}
                    Else
                        arr7 = {bankAccount, crtotal - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT), "", "", "", "", "", "", "B"}
                    End If

                    ''-----------------------
                    Dim arrlist As New ArrayList()
                    arrlist.Add(arr6)
                    arrlist.Add(arr7)
                    If bankCharges <> 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                        arrlist.Add(BankCharge)
                        '' add Bank charges tax account by Panch Raj
                        arrlist = AddBankChargesTaxAccount(obj, bankAccount, BankLocation, arrlist, trans)
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
                    transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)
                End If
                Dim InvcNo As String = ""
                Dim BalAmt As Decimal = 0.0
                Dim PayAmt As Decimal = drtotal
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal Then
                Dim arrmis As New ArrayList()
                Dim ESiAmt As Decimal = 0.0
                Dim MiscAmt As Decimal = 0.0
                Dim ESI_Percent As Decimal = 0.0
                qry = "select TSPL_PAYMENT_detail.Account_code,TSPL_PAYMENT_detail.Net_Balance,TSPL_PAYMENT_detail.Remarks,TSPL_PAYMENT_HEADER.ConvRateOld,TSPL_PAYMENT_detail.Hirerachy_Level_Code,TSPL_PAYMENT_detail.Cost_Center_Fin_Code from TSPL_PAYMENT_detail inner join TSPL_PAYMENT_HEADER on " &
                " TSPL_PAYMENT_detail.payment_no=TSPL_PAYMENT_HEADER.payment_no where TSPL_PAYMENT_detail.Payment_No='" + obj.Payment_No + "'"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                    Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
                    For Each dr As DataRow In dtNew.Rows
                        Dim strAccountLocation As String = clsCommon.myCstr(dr("Account_code")).Substring(clsCommon.myLen(clsCommon.myCstr(dr("Account_code"))) - 3, 3)
                        Dim dblAmount As Double = clsCommon.myCdbl(dr("Net_Balance")) * clsCommon.myCdbl(dr("ConvRateOld"))
                        Dim strHirerchyCode As String = clsCommon.myCstr(dr("Hirerachy_Level_Code"))
                        Dim strCostCenterCode As String = clsCommon.myCstr(dr("Cost_Center_Fin_Code"))
                        If isApplyBrachAccounting AndAlso Not (clsCommon.CompairString(BankLocation, strAccountLocation) = CompairStringResult.Equal) Then
                            Dim Acc4() As String = {bankAccount, IIf(isReceipt = 0, -1, 1) * dblAmount, "", "", "", "", "", "", "B"}
                            arrmis.Add(Acc4)

                            Dim strTemp As String = ClsBranchAccountMapping.GetBranchAccount(BankLocation, strAccountLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Branch account mapping with from location " + BankLocation + " and to location " + strAccountLocation)
                            End If
                            Dim RcvblAcc = New String() {strTemp, IIf(isReceipt = 0, 1, -1) * dblAmount}
                            arrmis.Add(RcvblAcc)

                            Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), IIf(isReceipt = 0, 1, -1) * dblAmount, clsCommon.myCstr(dr("Remarks")), "", strHirerchyCode, strCostCenterCode}
                            arrmis.Add(acc3)

                            strTemp = ClsBranchAccountMapping.GetBranchAccount(strAccountLocation, BankLocation, trans)
                            If clsCommon.myLen(strTemp) <= 0 Then
                                Throw New Exception("Please set Brach account mapping with from location " + strAccountLocation + " and to location " + BankLocation)
                            End If
                            RcvblAcc = New String() {strTemp, IIf(isReceipt = 0, -1, 1) * dblAmount}
                            arrmis.Add(RcvblAcc)
                        Else
                            Dim acc3() As String = {clsCommon.myCstr(dr("Account_code")), IIf(isReceipt = 0, 1, -1) * dblAmount, clsCommon.myCstr(dr("Remarks")), "", strHirerchyCode, strCostCenterCode}
                            arrmis.Add(acc3)

                            Dim Acc4() As String = {bankAccount, IIf(isReceipt = 0, -1, 1) * dblAmount, "", "", "", "", "", "", "B"}
                            arrmis.Add(Acc4)
                        End If
                        If clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) < 0 Then
                            ESiAmt = ESiAmt + (clsCommon.myCdbl(clsCommon.myCstr(dr("Net_Balance"))) * clsCommon.myCdbl(dr("ConvRateOld")) * -1)
                        End If
                    Next
                End If
                Dim strbankacct1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
                If obj.Location_Code <> "" Then
                    bankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(bankAccount, obj.Location_Code, True, trans)
                End If
                If -ESiAmt - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT) <> 0 Then
                    Dim Acc4() As String = {bankAccount, IIf(isReceipt = 0, -1, 1) * (ESiAmt - bankCharges - (EXCHANGE_LOSS_AMT - EXCHANGE_GAIN_AMT)), "", "", "", "", "", "", "B"}
                    arrmis.Add(Acc4)
                End If
                If bankCharges > 0 And clsCommon.myLen(obj.Bank_Charges_Ac) > 0 Then
                    Dim BankCharge() As String = {obj.Bank_Charges_Ac, IIf(isReceipt = 0, 1, -1) * bankCharges}
                    arrmis.Add(BankCharge)
                    '' add Bank charges tax account by Panch Raj
                    arrmis = AddBankChargesTaxAccount(obj, bankAccount, BankLocation, arrmis, trans)
                End If
                If ESiAmt <> 0 Then
                    Dim Acc5() As String = {bankAccount, IIf(isReceipt = 0, 1, -1) * ESiAmt, "", "", "", "", "", "", "B"}
                    arrmis.Add(Acc5)
                End If
                '' MULTICURRENCY
                If IsMultiCurrency Then
                    If EXCHANGE_LOSS_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_LOSS_ACCOUNT, IIf(isReceipt = 0, 1, -1) * EXCHANGE_LOSS_AMT}
                        arrmis.Add(CURR_EXCHANGE)
                    ElseIf EXCHANGE_GAIN_AMT > 0 Then
                        Dim CURR_EXCHANGE() As String = {EXCHANGE_GAIN_ACCOUNT, IIf(isReceipt = 0, -1, 1) * EXCHANGE_GAIN_AMT}
                        arrmis.Add(CURR_EXCHANGE)
                    End If
                End If
                '' END MULTICURRENCY
                sourceType = "AP-MI"
                transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "AD") = CompairStringResult.Equal Then
                Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Total_Prepayment*ConvRateOld  from  TSPL_PAYMENT_HEADER where Payment_No like '" + clsCommon.myCstr(obj.Document_No) + "'  and Vendor_Code = '" + clsCommon.myCstr(obj.Vendor_Code) + "'", trans))
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
                transportSql.FunGrnlEntryWithTrans(BankLocation, True, strVoucherNoifExists, trans, PostDate, paymentDesc, sourceType, sourceDesc, obj.Payment_No, strentrydesc, strsrctype, obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, applydocument, , clsCommon.myCstr(obj.Reference), clsCommon.myCstr(obj.Narration), coll, objJE)
            End If
            '' to update VSP Code against Farmer code into Journal master
            If clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "MI") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(obj.MP_Code_For_Advance)) > 0 Then
                Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where source_doc_no ='" & obj.Payment_No & "' and source_code='AP-MI'", trans))
                If clsCommon.myLen(strVoucherNo) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("update tspl_journal_master set VSP_code=(select TSPL_VLC_MASTER_HEAD.VSP_Code  from TSPL_MP_MASTER inner JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MP_MASTER.VLC_Code where mp_code='" & obj.MP_Code_For_Advance & "') where Voucher_No ='" & strVoucherNo & "' and source_code='AP-MI' ", trans)
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function AddBankChargesTaxAccount(ByVal obj As clsPaymentHeader, ByVal BankAccount As String, ByVal BankLocation As String, ByVal arrAcc As ArrayList, ByVal trans As SqlTransaction) As ArrayList
        If obj.Bank_Charges_Tax > 0 Then
            'bankCharges = clsCommon.myCdbl(obj.Bank_Charges)
            'crtotal = -1 * (clsCommon.myCdbl(obj.Payment_Amount) * ConvRate + bankCharges)
            Dim qry As String = ""
            qry = " select TSPL_PAYMENT_BANK_CHARGES_TAX.Payment_No,TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Code,TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Amount," & _
                  " TSPL_TAX_MASTER.Tax_Recoverable_Account,TSPL_TAX_MASTER.Tax_Recover_Rate,(TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Amount*coalesce(TSPL_TAX_MASTER.Tax_Recover_Rate,0)/100) as Recover_Tax_Amount" & _
                  " from TSPL_PAYMENT_BANK_CHARGES_TAX left join TSPL_TAX_MASTER on TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Code=TSPL_TAX_MASTER.Tax_Code" & _
                  " where TSPL_PAYMENT_BANK_CHARGES_TAX.Payment_No='" & obj.Payment_No & "' and TSPL_TAX_MASTER.Tax_Recoverable='Y' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim Recover_Bank_ChargeTaxTotal As Double = 0
            For Each dr As DataRow In dt.Rows
                If clsCommon.myLen(dr.Item("Tax_Recoverable_Account")) > 0 Then
                    Dim DrAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("Tax_Recoverable_Account")), BankLocation, True, trans)
                    Dim Debit() As String = {DrAcc, clsCommon.myCdbl(dr.Item("Recover_Tax_Amount"))}
                    Recover_Bank_ChargeTaxTotal = Recover_Bank_ChargeTaxTotal + clsCommon.myCdbl(dr.Item("Recover_Tax_Amount"))
                    arrAcc.Add(Debit)
                End If
            Next
            BankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(BankAccount, BankLocation, True, trans)
            Dim Credit() As String = {BankAccount, -1 * Recover_Bank_ChargeTaxTotal}
            arrAcc.Add(Credit)
        End If
        Return arrAcc
    End Function

    Public Function UpdatePostedData(ByVal obj As clsPaymentHeader) As Boolean
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

    Public Function UpdatePostedData(ByVal obj As clsPaymentHeader, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        Try
            '--------Checks Whertrher Transaction Is Locked Or Not-----------
            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans)
            clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, obj.Payment_Date, trans)
            '----------------------------------------------------------------
            'If clsCommon.myLen(obj.Payment_No) > 0 Then
            '    Dim isPosted As Integer = clsDBFuncationality.getSingleValue("Select Posted from TSPL_PAYMENT_HEADER Where Payment_No='" + obj.Payment_No + "'", trans)
            '    If isPosted = 1 Then
            '        Throw New Exception("Document already posted")
            '    End If
            'End If

            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_REMITTANCE Where Document_No='" + obj.Payment_No + "'", trans)

            Dim qry As String = "DELETE From TSPL_PAYMENT_DETAIL WHERE Payment_No ='" + obj.Payment_No + "'"
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
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
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
            obj.Vendor_Account_Set = clsDBFuncationality.getSingleValue("select Vendor_Account  from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans)
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
                obj.Debit_Account = clsDBFuncationality.getSingleValue("select s.Payable_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + obj.Vendor_Code + "' ", trans)

                If clsCommon.myLen(obj.Location_GL_Code) <= 0 Then
                    obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "')", trans))
                End If

                obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
            ElseIf clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal Then
                obj.Debit_Account = clsDBFuncationality.getSingleValue("select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + obj.Vendor_Code + "'", trans)
                '' Anubhooti 27-Mar-2015 (Advance/On-Account: Debit Amount should be Advance_Against_Salary instead of advance account if Advance_Against_Salary is checked)
                If clsCommon.myCdbl(obj.Advance_Against_Salary) = 1 AndAlso (clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Type, "OA") = CompairStringResult.Equal) Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.Advance_Against_Salary,'') AS Advance_Against_Salary  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + obj.Vendor_Code + "'", trans))
                    If clsCommon.myLen(obj.Debit_Account) <= 0 Then
                        Throw New Exception("Please fill advance against salary account on vendor account set")
                    End If
                End If
                '============Commented By Rohit After Talked with Anubhuti and Amit Sir(09-Jul-2015)=====
                'obj.Debit_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Debit_Account, obj.Location_GL_Code, True, trans)
                '=====================================================================================
                '' Anubhooti 31-Mar-2015 (Receipt/Security Refund :If security is checked then security account will go on GL)
                If (clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "RC") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(obj.Payment_Type), "SR") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(obj.Is_Security) = 1 Then
                    obj.Debit_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(s.SECURITY_ACCOUNT,'') AS SECURITY_ACCOUNT from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + obj.Vendor_Code + "' ", trans))
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
                clsCommon.AddColumnsForChange(coll, "MP_Code_For_Advance", obj.MP_Code_For_Advance, True)
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
            '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_HEADER", OMInsertOrUpdate.Insert, "", trans)
            'Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_HEADER", OMInsertOrUpdate.Update, "Payment_No='" + obj.Payment_No + "'", trans)
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
            isSaved = isSaved AndAlso clsPaymentDetail.SaveData(obj.Payment_No, obj.ArrTr, trans)
            '' update currency loss and gain in case of payment type entr
            'If obj.ConvRate <> 1 Then
            '    If obj.Payment_Type = "PY" Then
            '        Dim obj1 As New clsPaymentHeader
            '        Dim diff As Double = 0.0
            '        diff = obj.PAYMENT_AMOUNT_BASE_CURRENCY - clsPaymentDetail.GetAppliedAmountInBaseCurrency(obj.Payment_No, trans)
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
            '        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_PAYMENT_HEADER", OMInsertOrUpdate.Update, "Payment_No='" + obj.Payment_No + "'", trans)
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
            clsPaymentHeader.CreateJournalEntry(obj, "MPayable", Voucher_No, trans)
        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function isPosted(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select Posted from TSPL_PAYMENT_HEADER where Payment_No='" + strCode + "'"
        qry = clsDBFuncationality.getSingleValue(qry, trans)
        If clsCommon.myLen(qry) <= 0 Then
            Throw New Exception("Payment No " + strCode + "is Not exists")
        ElseIf clsCommon.myCdbl(qry) = 1 Then
            Return True
        End If
        Return False
    End Function


    Public Shared Function GetAssetLostPaymentQry(ByVal strVendorCode As String, ByVal dtFrom As DateTime?, ByVal dtTo As DateTime, ByVal skipPreviousDocumeent As Boolean, ByVal tran As SqlTransaction) As String
        Dim dtToDateForQry As DateTime = dtTo
        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotConsiderTheFutureDateOfAdvancePayment, clsFixedParameterCode.DoNotConsiderTheFutureDateOfAdvancePayment, tran)) = 0) Then
            dtToDateForQry = clsCommon.GETSERVERDATE(tran)
        End If

        Dim qry As String = " Select TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_PAYMENT_HEADER.Vendor_Name,TSPL_PAYMENT_HEADER.Payment_No,TSPL_PAYMENT_HEADER.Payment_Date,TSPL_PAYMENT_HEADER.Payment_Amount+isnull(TDS_Amount ,0) as Payment_Amount " &
               "  from TSPL_PAYMENT_HEADER WHERE Posted='1' "
        If clsCommon.myLen(strVendorCode) <= 0 Then
        Else
            qry += " AND Vendor_Code in  (" + strVendorCode + ") "
        End If
        qry += " AND Payment_Type IN ('AD') and TSPL_PAYMENT_HEADER.Entry_Desc = 'Apply document for Asset Lost'  and len(isnull(TSPL_PAYMENT_HEADER.Applied_Payment,''))>0 "

        If skipPreviousDocumeent Then
            qry += " and TSPL_PAYMENT_HEADER.Payment_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' "
        End If
        qry += " and TSPL_PAYMENT_HEADER.Payment_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDateForQry), "dd/MMM/yyyy hh:mm tt") + "' " &
        "and not exists ( select 1 from TSPL_PAYMENT_PROCESS_ASSET_LOST where TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No=TSPL_PAYMENT_HEADER.Payment_No) order by TSPL_PAYMENT_HEADER.Payment_Date"
        Return qry
    End Function
End Class


Public Class clsPaymentDetail
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

    Public Shared Function SaveData(ByVal strPaymentNo As String, ByVal Arr As List(Of clsPaymentDetail), ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsPaymentDetail In Arr
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function
    Public Shared Function GetAppliedAmountInBaseCurrency(ByVal PaymentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select coalesce(sum(Applied_Amount*ConvRateOld),0) as AppliedAmtBase from TSPL_PAYMENT_DETAIL where Payment_No='" & PaymentNo & "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

End Class

Public Class clsPaymentDetailGST
#Region "Variable"

    ''-------------------------
    Public Payment_No As String = String.Empty
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
  
    Public Item_Tax As Double = 0
    Public Total_MRP_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Disc_Amt As Double = 0
    Public ActualRate As Double = 0
    Public TotalItem_Weight As Double = 0
    Public Conv_Factor As Double = 0
    Public Landing_Cost As Double = 0
    Public TAX1_Amt_Payment As Double = 0
    Public TAX2_Amt_Payment As Double = 0
    Public TAX3_Amt_Payment As Double = 0
    Public TAX4_Amt_Payment As Double = 0
    Public TAX5_Amt_Payment As Double = 0
    Public TAX6_Amt_Payment As Double = 0
    Public TAX7_Amt_Payment As Double = 0
    Public TAX8_Amt_Payment As Double = 0
    Public TAX9_Amt_Payment As Double = 0
    Public TAX10_Amt_Payment As Double = 0
    Public PaymentAdvance As Double = 0
    Public PaymentTotalTax As Double = 0
    Public PaymentTotalAdvanceAmt As Double = 0


    ''-------------
#End Region
    Public Shared Function SaveData(ByVal strReceiptNo As String, ByVal Arr As List(Of clsPaymentDetailGST), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim intLineNo As Integer = 1
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsPaymentDetailGST In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Payment_No", strReceiptNo)
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
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt_Payment", obj.TAX1_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt_Payment", obj.TAX2_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt_Payment", obj.TAX3_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt_Payment", obj.TAX4_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt_Payment", obj.TAX5_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Amt_Payment", obj.TAX6_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Amt_Payment", obj.TAX7_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Amt_Payment", obj.TAX8_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Amt_Payment ", obj.TAX9_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Amt_Payment", obj.TAX10_Amt_Payment)
                    clsCommon.AddColumnsForChange(coll, "PaymentAdvance", obj.PaymentAdvance)
                    clsCommon.AddColumnsForChange(coll, "PaymentTotalTax", obj.PaymentTotalTax)
                    clsCommon.AddColumnsForChange(coll, "PaymentTotalAdvanceAmt", obj.PaymentTotalAdvanceAmt)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_DETAIL_GST", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsPOAdvanceAdjustmentKnockOff
#Region "Variable"
    Public PI_No As String = String.Empty
    Public Trans_Type As String = String.Empty
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

    Public Shared Function GetBalanceAdvanceAmt(ByVal strPINo As String, strTransType As String, ByVal tran As SqlTransaction) As clsPOAdvanceAdjustmentKnockOff
        Dim obj As clsPOAdvanceAdjustmentKnockOff = Nothing
        If clsCommon.myLen(strPINo) > 0 Then
            Dim strPOquery As String
            If clsCommon.CompairString(strTransType, "WO") = CompairStringResult.Equal Then
                strPOquery = "select RefDocNo from TSPL_VENDOR_INVOICE_HEAD where Document_No in ('" + strPINo + "')"
            Else
                strPOquery = "select distinct PO_ID from TSPL_PI_DETAIL where PI_No in ('" + strPINo + "')"
            End If

            Dim qry As String = "select distinct TSPL_PAYMENT_HEADER.Payment_No from TSPL_PAYMENT_DETAIL_GST" + Environment.NewLine + _
            "left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL_GST.Payment_No" + Environment.NewLine + _
            "where TSPL_PAYMENT_DETAIL_GST.Document_Code in (" + strPOquery + ") and TSPL_PAYMENT_HEADER.Posted=0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "Following Unposted Advance payment Entry Found :" + Environment.NewLine
                For Each dr As DataRow In dt.Rows
                    qry += clsCommon.myCstr(dr("Payment_No")) + ", "
                Next
                Throw New Exception(qry)
            End If
            qry = "Delete from TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF where PI_No='" + strPINo + "' and Trans_Type='" + strTransType + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "insert  into TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF(PI_No,Trans_Type,PurchaseOrder_No,TAX1_Amt,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt )" + Environment.NewLine + _
            "select '" + strPINo + "' as PI_No,'" + strTransType + "' as Trans_Type,PurchaseOrder_No, case when TAX1_Amt<0 then 0 else TAX1_Amt end as TAX1_Amt,case when TAX2_Amt<0 then 0 else TAX2_Amt end as TAX2_Amt ,case when TAX3_Amt<0 then 0 else TAX3_Amt end as TAX3_Amt ,case when TAX4_Amt<0 then 0 else TAX4_Amt end as TAX4_Amt ,case when TAX5_Amt<0 then 0 else TAX5_Amt end as TAX5_Amt ,case when TAX6_Amt<0 then 0 else TAX6_Amt end as TAX6_Amt ,case when TAX7_Amt<0 then 0 else TAX7_Amt end as TAX7_Amt ,case when TAX8_Amt<0 then 0 else TAX8_Amt end as TAX8_Amt ,case when TAX9_Amt<0 then 0 else TAX9_Amt end as TAX9_Amt ,case when TAX10_Amt<0 then 0 else TAX10_Amt end as TAX10_Amt  from (" + Environment.NewLine + _
            "select PurchaseOrder_No, min(TAX1_Amt) as TAX1_Amt,min(TAX2_Amt) as TAX2_Amt,min(TAX3_Amt) as  TAX3_Amt,min (TAX4_Amt) as TAX4_Amt,min(TAX5_Amt) as TAX5_Amt,min(TAX6_Amt) as TAX6_Amt,min(TAX7_Amt) as TAX7_Amt,min(TAX8_Amt) as TAX8_Amt,min(TAX9_Amt) as TAX9_Amt ,min(TAX10_Amt) as TAX10_Amt from (" + Environment.NewLine + _
            "select 'PI' as DocType,PO_ID as PurchaseOrder_No,sum(TAX1_Amt) as TAX1_Amt,sum(TAX2_Amt) as TAX2_Amt,sum(TAX3_Amt) as TAX3_Amt,sum(TAX4_Amt) as TAX4_Amt,sum(TAX5_Amt) as TAX5_Amt,sum(TAX6_Amt) as TAX6_Amt,sum(TAX7_Amt) as TAX7_Amt,sum(TAX8_Amt) as TAX8_Amt,sum(TAX9_Amt) as TAX9_Amt,sum(TAX10_Amt) as TAX10_Amt,0 as Chk from TSPL_PI_DETAIL where  PI_No in ('" + strPINo + "') group by PO_ID" + Environment.NewLine + _
            "union all" + Environment.NewLine + _
            "select 'BAL'as DocType, PurchaseOrder_No,sum(TAX1_Amt * RI) as TAX1_Amt,sum(TAX2_Amt * RI) as TAX2_Amt,sum(TAX3_Amt * RI) as TAX3_Amt,sum(TAX4_Amt * RI) as TAX4_Amt,sum(TAX5_Amt * RI) as TAX5_Amt,sum(TAX6_Amt * RI) as TAX6_Amt,sum(TAX7_Amt * RI) as TAX7_Amt,sum(TAX8_Amt * RI) as TAX8_Amt,sum(TAX9_Amt * RI) as TAX9_Amt,sum(TAX10_Amt * RI) as TAX10_Amt,1 as Chk   from (" + Environment.NewLine + _
            "select 'AD' as DocType,Document_Code as PurchaseOrder_No,Item_Code,1 as RI,convert(decimal(18,2),TAX1_Amt_Payment) as TAX1_Amt,convert(decimal(18,2),TAX2_Amt_Payment) as TAX2_Amt,convert(decimal(18,2),TAX3_Amt_Payment) as TAX3_Amt,convert(decimal(18,2),TAX4_Amt_Payment) as TAX4_Amt,convert(decimal(18,2),TAX5_Amt_Payment) as TAX5_Amt,convert(decimal(18,2),TAX6_Amt_Payment) as TAX6_Amt,convert(decimal(18,2),TAX7_Amt_Payment) as TAX7_Amt,convert(decimal(18,2),TAX8_Amt_Payment) as TAX8_Amt,convert(decimal(18,2),TAX9_Amt_Payment) as TAX9_Amt,convert(decimal(18,2),TAX10_Amt_Payment) as TAX10_Amt from TSPL_PAYMENT_DETAIL_GST where Document_Code in (" + strPOquery + ")" + Environment.NewLine + _
            "union all" + Environment.NewLine + _
            "select 'PO_AD_Adj' as DocType,PurchaseOrder_No,'' as Item_Code,-1 as RI,TAX1_Amt,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt from TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF where PurchaseOrder_No  in (" + strPOquery + ") " + Environment.NewLine + _
            ")xx Group by PurchaseOrder_No" + Environment.NewLine + _
            ")xxx group by PurchaseOrder_No having sum(chk)>0 " + Environment.NewLine + _
            ")xxxx where ( TAX1_Amt>0 or TAX2_Amt>0 or TAX3_Amt>0 or TAX4_Amt>0 or TAX5_Amt>0 or TAX6_Amt>0 or TAX7_Amt>0 or TAX8_Amt>0 or TAX9_Amt>0 or TAX10_Amt>0)"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "select PI_No,sum(TAX1_Amt) as TAX1_Amt,sum(TAX2_Amt) as TAX2_Amt,sum(TAX3_Amt ) as TAX3_Amt,sum(TAX4_Amt ) as TAX4_Amt,sum(TAX5_Amt ) as TAX5_Amt,sum(TAX6_Amt ) as TAX6_Amt,sum(TAX7_Amt ) as TAX7_Amt,sum(TAX8_Amt ) as TAX8_Amt,sum(TAX9_Amt ) as TAX9_Amt,sum(TAX10_Amt ) as TAX10_Amt from TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF where PI_No='" + strPINo + "' and Trans_Type='" + strTransType + "' group by PI_No"
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsPOAdvanceAdjustmentKnockOff()
                obj.PI_No = clsCommon.myCstr(dt.Rows(0)("PI_No"))
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
Public Class clsPaymentBankChargesTax
#Region "Variable"
    ''-------------------------
    Public Payment_No As String = String.Empty    
    Public Line_No As Integer
    'Public Tax_Group_BankCharges As String = String.Empty
    Public Tax_Code As String = String.Empty
    Public Tax_Rate As Decimal
    Public Tax_Base_Amount As Decimal
    Public Tax_Amount As Decimal
    ''-------------
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsPaymentBankChargesTax), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim intLineNo As Integer = 1
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsPaymentBankChargesTax In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Payment_No", strDocNo)                    
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)                    
                    'clsCommon.AddColumnsForChange(coll, "Tax_Group_BankCharges", obj.Tax_Group_BankCharges)
                    clsCommon.AddColumnsForChange(coll, "Tax_Code", obj.Tax_Code)
                    clsCommon.AddColumnsForChange(coll, "Tax_Rate", obj.Tax_Rate)
                    clsCommon.AddColumnsForChange(coll, "Tax_Base_Amount", obj.Tax_Base_Amount)
                    clsCommon.AddColumnsForChange(coll, "Tax_Amount", obj.Tax_Amount)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_BANK_CHARGES_TAX", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetBankChargesTaxList(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsPaymentBankChargesTax)
        Dim objList As New List(Of clsPaymentBankChargesTax)
        Dim objtr As New clsPaymentBankChargesTax
        If clsCommon.myLen(strDocNo) <= 0 Then
            Return objList
        End If
        Dim qry As String = "select Payment_No,Line_No,Tax_Code,Tax_Rate,Tax_Base_Amount,Tax_Amount from TSPL_PAYMENT_BANK_CHARGES_TAX where Payment_No='" & strDocNo & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsPaymentBankChargesTax
                objtr.Payment_No = strDocNo
                objtr.Line_No = clsCommon.myCdbl(dr.Item("Line_No"))
                'objtr.Tax_Group_BankCharges = clsCommon.myCstr(dr.Item("Tax_Group_BankCharges"))
                objtr.Tax_Code = clsCommon.myCstr(dr.Item("Tax_Code"))
                objtr.Tax_Rate = clsCommon.myCdbl(dr.Item("Tax_Rate"))
                objtr.Tax_Base_Amount = clsCommon.myCdbl(dr.Item("Tax_Base_Amount"))
                objtr.Tax_Amount = clsCommon.myCdbl(dr.Item("Tax_Amount"))
                objList.Add(objtr)
            Next
        End If
        Return objList
    End Function
End Class
