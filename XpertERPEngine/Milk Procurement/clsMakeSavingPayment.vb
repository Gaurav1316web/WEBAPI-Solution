Imports System.Data.SqlClient

Public Class clsMakeSavingPayment
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Doc_Date As DateTime
    Public Remarks As String = Nothing
    Public Filter_From_Date As Date
    Public Filter_To_Date As Date
    Public Filter_arrVSP As ArrayList = Nothing '' not in table
    Public Filter_arrBMC As ArrayList = Nothing '' not in table
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending

    Public arr As List(Of clsMakeSavingPaymentDetail) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsMakeSavingPayment, ByVal isNewEntry As Boolean) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, tran, "")
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsMakeSavingPayment, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal strVoucherNoRecreateOnly As String) As Boolean
        Dim qry As String = "delete from TSPL_MAKE_SAVING_PAYMENT_SAVING where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DCS_SALE where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DEDUCTION where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        If obj.arr.Count <= 0 Then
            Throw New Exception("Please fill at one invoice details")
        End If

        If isNewEntry Then
            obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.MakeSavingPayment, clsDocTransactionType.Transaction, "")
        End If
        If (clsCommon.myLen(obj.Doc_Date) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Filter_From_Date", clsCommon.GetPrintDate(obj.Filter_From_Date, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Filter_To_Date", clsCommon.GetPrintDate(obj.Filter_To_Date, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT", OMInsertOrUpdate.Update, "Doc_Code='" + obj.Doc_Code + "'", trans)
        End If
        clsMakeSavingPaymentDetail.SaveData(obj.Doc_Code, obj.arr, trans)
        clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Doc_Code, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_SAVING", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", "Doc_Code", "", "", "", "", "", "", trans)
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMakeSavingPayment
        Dim obj As clsMakeSavingPayment = Nothing
        Dim qry As String = "SELECT TSPL_MAKE_SAVING_PAYMENT.* from TSPL_MAKE_SAVING_PAYMENT where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = (select MIN(Doc_Code) from TSPL_MAKE_SAVING_PAYMENT where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = (select Max(Doc_Code) from TSPL_MAKE_SAVING_PAYMENT where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = (select Min(Doc_Code) from TSPL_MAKE_SAVING_PAYMENT where Doc_Code>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = (select Max(Doc_Code) from TSPL_MAKE_SAVING_PAYMENT where Doc_Code<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMakeSavingPayment()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Posted, ERPTransactionStatus.Pending)
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Filter_From_Date = clsCommon.myCDate(dt.Rows(0)("Filter_From_Date"))
            obj.Filter_To_Date = clsCommon.myCDate(dt.Rows(0)("Filter_To_Date"))
            obj.arr = clsMakeSavingPaymentDetail.GetData(obj.Doc_Code, trans, obj.Filter_arrVSP, obj.Filter_arrBMC)
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.myLen(strDocNo) <= 0 Then
            Throw New Exception("Please provide document no to post")
        End If

        Dim obj As clsMakeSavingPayment = clsMakeSavingPayment.GetData(strDocNo, NavigatorType.Current, trans)
        If obj.Status = ERPTransactionStatus.Posted Then
            Throw New Exception("Already posted transaction - " + obj.Doc_Code)
        End If
        Dim DisCCodeForArAdj As String = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForArAdj, clsFixedParameterCode.DiscountCodeForArAdj, trans)
        If clsCommon.myLen(DisCCodeForArAdj) <= 0 Then
            Throw New Exception("Please Map Discount code from Sale setting")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select  Code,Description,Account_Code,Account_Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Discount Code not Exists [" + DisCCodeForArAdj + "] as Defined is Fixed Parameter [" + clsFixedParameterType.DiscountCodeForArAdj + "]")
        End If
        Dim DiscDiscForArAdj As String = clsCommon.myCstr(dt.Rows(0)("Description"))
        Dim GLAcARAdj As String = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
        If clsCommon.myLen(GLAcARAdj) <= 0 Then
            Throw New Exception("Please set account for Discount master:" + DisCCodeForArAdj)
        End If
        Dim GLAcDescARAdj As String = clsCommon.myCstr(dt.Rows(0)("Account_Description"))

        For Each objTr As clsMakeSavingPaymentDetail In obj.arr
            Dim SaleAmt As Decimal = 0
            If objTr.ArrDCSSale IsNot Nothing AndAlso objTr.ArrDCSSale.Count > 0 Then
                For Each objSale As clsMakeSavingPaymentDCSSale In objTr.ArrDCSSale
                    Dim objRcpt As New clsAdjustmentEntryReceivables
                    objRcpt.Adjustment_No = ""
                    objRcpt.Description = " Adjustment Against Make Saving Payment"
                    objRcpt.Adjustment_Date = obj.Doc_Date
                    objRcpt.Customer_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cust_Code  from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" & objTr.DCS_Code & "' ", trans))
                    objRcpt.Customer_Name = clsCustomerMaster.GetName(objRcpt.Customer_No, trans)
                    objRcpt.Doc_No = obj.Doc_Code
                    objRcpt.ARInvoiceNo = objSale.AR_Invoice_No
                    objRcpt.Doc_Amount = objSale.Amount
                    objRcpt.Remarks = ""
                    objRcpt.Against_Make_Saving_Payment = objTr.PK_ID
                    objRcpt.Adjustment_Amount = (objSale.Amount - objSale.Red_Ded_Amount)
                    objRcpt.Arr = New List(Of clsAdjustmentEntryReceivablesDetail)
                    Dim objTrRcpt As New clsAdjustmentEntryReceivablesDetail()
                    objTrRcpt.Discount_Code = DisCCodeForArAdj
                    objTrRcpt.Discount_Description = DiscDiscForArAdj
                    objTrRcpt.Account_No = GLAcARAdj
                    objTrRcpt.Account_Description = GLAcDescARAdj
                    objTrRcpt.Amount = (objSale.Amount - objSale.Red_Ded_Amount)
                    objTrRcpt.Remarks = ""
                    objRcpt.Arr.Add(objTrRcpt)
                    If objTrRcpt.Amount > 0 Then
                        objRcpt.SaveData(objRcpt, True, trans)
                        clsAdjustmentEntryReceivables.FunPost(objRcpt.Adjustment_No, trans)
                    End If
                    SaleAmt += (objSale.Amount - objSale.Red_Ded_Amount)
                Next
            End If

            If SaleAmt > 0 Then
                If objTr.ArrDCSSaving IsNot Nothing AndAlso objTr.ArrDCSSaving.Count > 0 Then
                    For Each objSaving As clsMakeSavingPaymentSaving In objTr.ArrDCSSaving
                        objSaving.AdjustAmt = 0
                        If SaleAmt >= objSaving.Amount Then
                            objSaving.AdjustAmt = objSaving.Amount
                            SaleAmt -= objSaving.AdjustAmt
                        Else
                            objSaving.AdjustAmt = SaleAmt
                            SaleAmt = 0
                        End If

                        Dim objPayAdj As New clsPaymentAdjustmentEntry
                        objPayAdj.Adjustment_No = ""
                        objPayAdj.Description = " Saving Adjustment Against Make Saving Payment"
                        objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                        objPayAdj.Vendor_No = objTr.DCS_Code
                        objPayAdj.Vendor_Name = clsVendorMaster.GetName(objTr.DCS_Code, trans)
                        objPayAdj.Doc_No = objSaving.AP_Invoice_No
                        objPayAdj.Doc_Amount = objSaving.Amount
                        objPayAdj.Remarks = objTr.PK_ID
                        objPayAdj.Adjustment_Amount = objSaving.AdjustAmt
                        objPayAdj.Against_Make_Saving_Payment = objTr.PK_ID
                        objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                        Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                        objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                        objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                        objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                        objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                        objTrPay.Amount = objSaving.AdjustAmt
                        objTrPay.Remarks = "Credit note Adjusment"
                        objPayAdj.Arr.Add(objTrPay)
                        objPayAdj.SaveData(objPayAdj, True, trans)
                        clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                        If SaleAmt = 0 Then
                            Exit For
                        End If
                    Next
                End If
            End If


            ''------------------Payment Entry Start Here
            If objTr.Payable_Amt > 0 Then
                Dim objPay As New clsPaymentHeader()
                objPay.Against_Make_Saving_Payment = objTr.PK_ID
                objPay.Payment_No = ""
                objPay.Entry_Desc = obj.Remarks + " " + obj.Doc_Code
                objPay.Payment_Date = clsCommon.myCDate(obj.Doc_Date)
                objPay.Payment_Post_Date = clsCommon.myCDate(obj.Doc_Date)
                Dim qry As String = "select Company_Bank from TSPL_VENDOR_MASTER where Vendor_Code='" + objTr.DCS_Code + "'"
                objPay.Bank_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(objPay.Bank_Code) <= 0 Then
                    Throw New Exception("Please define Saving bank for DCS " + objTr.DCS_UploaderNo + "  [" + objTr.DCS_Code + "] [" + objTr.DCS_Name + "] ")
                End If
                objPay.Payment_Type = "PY"
                objPay.Vendor_Code = objTr.DCS_Code
                objPay.Vendor_Name = objTr.DCS_Name
                objPay.Payment_Code = "NEFT"
                'objPay.Cheque_No = obj.ArrPPDetail.Item(i).Cheque_No
                'If Not obj.ArrPPDetail.Item(i).Cheque_Dated Is Nothing Then
                '    objPay.Cheque_Date = obj.ArrPPDetail.Item(i).Cheque_Dated
                'End If

                objPay.Account_Payee = 0
                objPay.memorndmamt = "0"
                'objPay.Applied_Payment = clsCommon.myCstr(obj.ArrPPDetail.Item(i).AP_Invoice_No)
                objPay.Is_Security = 0
                objPay.IsChkReverse = "N"
                objPay.Bank_Charges = 0
                objPay.Payment_Amount = objTr.Payable_Amt
                objPay.Balance_Amt = objTr.Payable_Amt
                qry = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (select MCC from TSPL_VLC_MASTER_HEAD where VSP_Code='" + objTr.DCS_Code + "')"
                objPay.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                objPay.Entry_Desc = obj.Remarks + " " + obj.Doc_Code
                objPay.ArrTr = New List(Of clsPaymentDetail)

                If objTr.ArrDCSSaving IsNot Nothing AndAlso objTr.ArrDCSSaving.Count > 0 Then
                    For Each objSaving As clsMakeSavingPaymentSaving In objTr.ArrDCSSaving
                        If objSaving.Amount - objSaving.AdjustAmt > 0 Then
                            Dim objPayTr As New clsPaymentDetail()
                            objPayTr.Apply = "1"
                            objPayTr.Payment_Type = "PY"
                            objPayTr.Document_No = objSaving.AP_Invoice_No
                            objPayTr.Original_Invoice_Amt = objSaving.Amount
                            objPayTr.Applied_Amount = objSaving.Amount - objSaving.AdjustAmt
                            objPayTr.Pending_Balance = 0
                            Dim vendorInvNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Invoice_No  from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & objSaving.AP_Invoice_No & "'", trans))
                            objPayTr.Net_Balance = 0
                            objPayTr.Vendor_Invoice_No = vendorInvNo
                            objPayTr.Security_Amount = 0
                            objPay.ArrTr.Add(objPayTr)
                        End If
                    Next
                End If
                If objTr.ArrDCSDeduction IsNot Nothing AndAlso objTr.ArrDCSDeduction.Count > 0 Then
                    For Each objDeduction As clsMakeSavingPaymentDeduction In objTr.ArrDCSDeduction
                        If (objDeduction.Amount - objDeduction.Red_Ded_Amount) > 0 Then
                            Dim objPayTr As New clsPaymentDetail()
                            objPayTr.Apply = "1"
                            objPayTr.Payment_Type = "PY"
                            objPayTr.Document_No = objDeduction.AP_Invoice_No
                            objPayTr.Original_Invoice_Amt = objDeduction.Amount
                            objPayTr.Applied_Amount = (objDeduction.Amount - objDeduction.Red_Ded_Amount)
                            objPayTr.Pending_Balance = objDeduction.Red_Ded_Amount
                            Dim vendorInvNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Invoice_No  from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & objDeduction.AP_Invoice_No & "'", trans))
                            objPayTr.Net_Balance = 0
                            objPayTr.Vendor_Invoice_No = vendorInvNo
                            objPayTr.Security_Amount = 0
                            objPay.ArrTr.Add(objPayTr)
                        End If
                    Next
                End If
                objPay.SaveData(objPay, True, trans, True)
                clsPaymentHeader.PostData(objPay.Payment_No, "Payable", trans)
            End If
            ''------------------Payment Entry End Here
        Next

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Status", 1)
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT", OMInsertOrUpdate.Update, "Doc_Code='" + strDocNo + "'", trans)

        clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Doc_Code, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_SAVING", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", "Doc_Code", "", "", "", "", "", "", trans)
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsMakeSavingPayment = clsMakeSavingPayment.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Posted) Then
                    Throw New Exception("Already Posted")
                End If

                clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Doc_Code, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_SAVING", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", "Doc_Code", "", "", "", "", "", "", trans)
                clsCommonFunctionality.SaveHistoryData(EnumSaveType.Delete, objCommonVar.CurrentUserCode, obj.Doc_Code, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_SAVING", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", "Doc_Code", "", "", "", "", "", "", trans)

                Dim qry As String = "delete from TSPL_MAKE_SAVING_PAYMENT_SAVING where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DCS_SALE where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DEDUCTION where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MAKE_SAVING_PAYMENT where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Payment process Document no Not found to reverse and unpost")
            End If

            Dim qry As String = "select Status from TSPL_MAKE_SAVING_PAYMENT where Doc_Code='" + strDocNo + "'"
            If clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans)) <> 1 Then
                Throw New Exception("Payment process Document no should be posted to reverse and unpost")
            End If

            '-------Payment Entry
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_BANK_CHARGES_TAX where  Payment_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_DETAIL_GST where  Payment_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_DETAIL where Payment_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_HEADER where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '-------End of Payment Entry

            '---------Payable adjustment
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header  where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code= '" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code= '" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from  TSPL_Payment_Adjustment_Detail where Adjustment_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code= '" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Payment_Adjustment_Header where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code= '" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '---------End of Payable adjustment

            '----------- Receipt Adjustment 
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Receipt_Adjustment_Detail where Adjustment_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Receipt_Adjustment_Header where Against_Make_Saving_Payment in (select PK_ID from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '----------- End of Receipt Adjustment 



            ''Update AP Invoice Balance Amont Type Debit/Credit Note/ItemIssue/ItemIssueRetrun
            qry = "update TSPL_VENDOR_INVOICE_HEAD  set Balance_Amt=Balance_Amt+xx.Amt from ( 
select AP_Invoice_No,Amount as Amt from TSPL_MAKE_SAVING_PAYMENT_SAVING where Doc_Code='" + strDocNo + "'
union all
select AP_Invoice_No,(Amount-Red_Ded_Amount) as Amt from TSPL_MAKE_SAVING_PAYMENT_DEDUCTION where Doc_Code='" + strDocNo + "'
)xx 
inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=xx.AP_Invoice_No"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            ''UpdateAR Invoice balance amount for MCC Sale/MCC Sale Return
            qry = "update TSPL_Customer_Invoice_Head set Balance_Amt=Balance_Amt+xx.Amt from (
Select AR_Invoice_No,(Amount-Red_Ded_Amount) As Amt from TSPL_MAKE_SAVING_PAYMENT_DCS_SALE where Doc_Code='" + strDocNo + "'
)xx inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=xx.AR_Invoice_No"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_MAKE_SAVING_PAYMENT set Status=0,Posted_By=null,Posted_Date=null where Doc_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, strDocNo, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_SAVING", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", "Doc_Code", "", "", "", "", "", "", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsMakeSavingPaymentDetail
#Region "Variables"
    Public PK_ID As Integer
    Public Doc_Code As String
    Public DCS_Code As String
    Public DCS_Name As String ''Not a table Column
    Public DCS_UploaderNo As String ''Not a table Column
    Public Saving_Amt As Decimal
    Public DCS_Sale_Amt As Decimal
    Public Deduction_Amt As Decimal
    Public Payable_Amt As Decimal
    Public ArrDCSSaving As List(Of clsMakeSavingPaymentSaving)
    Public ArrDCSSale As List(Of clsMakeSavingPaymentDCSSale)
    Public ArrDCSDeduction As List(Of clsMakeSavingPaymentDeduction)
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMakeSavingPaymentDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMakeSavingPaymentDetail In Arr
                If obj.Payable_Amt < 0 Then
                    Throw New Exception("Payable amount should be greater than zero of DCS [" + obj.DCS_Code + "]")
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "DCS_Code", obj.DCS_Code)
                clsCommon.AddColumnsForChange(coll, "Saving_Amt", obj.Saving_Amt)
                clsCommon.AddColumnsForChange(coll, "DCS_Sale_Amt", obj.DCS_Sale_Amt)
                clsCommon.AddColumnsForChange(coll, "Deduction_Amt", obj.Deduction_Amt)
                clsCommon.AddColumnsForChange(coll, "Payable_Amt", obj.Payable_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Dim intPKID As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsMakeSavingPaymentSaving.SaveData(strDocNo, intPKID, obj.ArrDCSSaving, trans)
                clsMakeSavingPaymentDCSSale.SaveData(strDocNo, intPKID, obj.ArrDCSSale, trans)
                clsMakeSavingPaymentDeduction.SaveData(strDocNo, intPKID, obj.ArrDCSDeduction, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction, ByRef arrVSP As ArrayList, ByRef arrBMC As ArrayList) As List(Of clsMakeSavingPaymentDetail)
        arrVSP = New ArrayList
        arrBMC = New ArrayList
        Dim arr As List(Of clsMakeSavingPaymentDetail) = Nothing
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT_DETAIL.*,TSPL_VENDOR_MASTER.Vendor_Name as DCS_Name, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC
from TSPL_MAKE_SAVING_PAYMENT_DETAIL 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MAKE_SAVING_PAYMENT_DETAIL.DCS_Code 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_MAKE_SAVING_PAYMENT_DETAIL.DCS_Code
where Doc_Code='" + strDocNo + "' order by PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMakeSavingPaymentDetail)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMakeSavingPaymentDetail
                obj.PK_ID = clsCommon.myCDecimal(dr("PK_ID"))
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.DCS_Code = clsCommon.myCstr(dr("DCS_Code"))
                obj.DCS_UploaderNo = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                obj.DCS_Name = clsCommon.myCstr(dr("DCS_Name"))
                obj.Saving_Amt = clsCommon.myCDecimal(dr("Saving_Amt"))
                obj.DCS_Sale_Amt = clsCommon.myCDecimal(dr("DCS_Sale_Amt"))
                obj.Deduction_Amt = clsCommon.myCDecimal(dr("Deduction_Amt"))
                obj.Payable_Amt = clsCommon.myCDecimal(dr("Payable_Amt"))
                obj.ArrDCSSaving = clsMakeSavingPaymentSaving.GetData(obj.Doc_Code, obj.PK_ID, trans)
                obj.ArrDCSSale = clsMakeSavingPaymentDCSSale.GetData(obj.Doc_Code, obj.PK_ID, trans)
                obj.ArrDCSDeduction = clsMakeSavingPaymentDeduction.GetData(obj.Doc_Code, obj.PK_ID, trans)

                arr.Add(obj)

                arrVSP.Add(obj.DCS_Code)
                arrBMC.Add(clsCommon.myCstr(dr("MCC")))
            Next
        End If
        Return arr
    End Function
End Class


Public Class clsMakeSavingPaymentSaving
#Region "Variables"
    Public IsSelect As Boolean ''No a Table Column
    Public SNo As Integer ''No a Table Column
    Public DocDate As Date = Nothing ''No a Table Column
    Public AdjustAmt As Decimal = 0 ''No a Table Column
    Public PK_ID As Integer
    Public Ref_PK_ID As Integer
    Public Doc_Code As String = Nothing
    Public AP_Invoice_No As String = Nothing
    Public Amount As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal Arr As List(Of clsMakeSavingPaymentSaving), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsMakeSavingPaymentSaving In Arr
                If obj.IsSelect Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", Ref_PK_ID)
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", obj.AP_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT_SAVING", OMInsertOrUpdate.Insert, "", trans)
                    ii += 1
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal trans As SqlTransaction) As List(Of clsMakeSavingPaymentSaving)
        Dim arr As List(Of clsMakeSavingPaymentSaving) = Nothing
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT_SAVING.*,TSPL_VENDOR_INVOICE_HEAD.Posting_Date  from TSPL_MAKE_SAVING_PAYMENT_SAVING  
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_MAKE_SAVING_PAYMENT_SAVING.AP_Invoice_No
where TSPL_MAKE_SAVING_PAYMENT_SAVING.Doc_Code='" + strDocNo + "' and Ref_PK_ID='" + clsCommon.myCstr(Ref_PK_ID) + "' order by PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMakeSavingPaymentSaving)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMakeSavingPaymentSaving
                obj.IsSelect = True
                obj.SNo = arr.Count + 1
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.AP_Invoice_No = clsCommon.myCstr(dr("AP_Invoice_No"))
                obj.DocDate = clsCommon.myCDate(dr("Posting_Date"))
                obj.Amount = clsCommon.myCDecimal(dr("AMOUNT"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsMakeSavingPaymentDCSSale
#Region "Variables"
    Public SNo As Integer ''No a Table Column
    Public IsSelect As Boolean ''No a Table Column
    Public DocDate As Date = Nothing ''No a Table Column
    Public PK_ID As Integer
    Public Ref_PK_ID As Integer
    Public Doc_Code As String = Nothing
    Public AR_Invoice_No As String = Nothing
    Public Amount As String = Nothing
    Public Red_Ded_Amount As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal Arr As List(Of clsMakeSavingPaymentDCSSale), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsMakeSavingPaymentDCSSale In Arr
                If obj.IsSelect Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", Ref_PK_ID)
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "AR_Invoice_No", obj.AR_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Red_Ded_Amount", obj.Red_Ded_Amount, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", OMInsertOrUpdate.Insert, "", trans)
                    ii += 1
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal trans As SqlTransaction) As List(Of clsMakeSavingPaymentDCSSale)
        Dim arr As List(Of clsMakeSavingPaymentDCSSale) = Nothing
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT_DCS_SALE.*,TSPL_Customer_Invoice_Head.Document_Date  from TSPL_MAKE_SAVING_PAYMENT_DCS_SALE
left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_MAKE_SAVING_PAYMENT_DCS_SALE.AR_Invoice_No
where TSPL_MAKE_SAVING_PAYMENT_DCS_SALE.Doc_Code='" + strDocNo + "' and Ref_PK_ID='" + clsCommon.myCstr(Ref_PK_ID) + "' order by PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMakeSavingPaymentDCSSale)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMakeSavingPaymentDCSSale
                obj.IsSelect = True
                obj.SNo = arr.Count + 1
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.AR_Invoice_No = clsCommon.myCstr(dr("AR_Invoice_No"))
                obj.DocDate = clsCommon.myCDate(dr("Document_Date"))
                obj.Amount = clsCommon.myCDecimal(dr("AMOUNT"))
                obj.Red_Ded_Amount = clsCommon.myCDecimal(dr("Red_Ded_Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsMakeSavingPaymentDeduction
#Region "Variables"
    Public SNo As Integer ''No a Table Column
    Public IsSelect As Boolean ''No a Table Column
    Public DocDate As Date = Nothing ''No a Table Column

    Public PK_ID As Integer
    Public Ref_PK_ID As Integer
    Public Doc_Code As String = Nothing
    Public AP_Invoice_No As String = Nothing
    Public Amount As String = Nothing
    Public Red_Ded_Amount As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal Arr As List(Of clsMakeSavingPaymentDeduction), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsMakeSavingPaymentDeduction In Arr
                If obj.IsSelect Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", Ref_PK_ID)
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", obj.AP_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Red_Ded_Amount", obj.Red_Ded_Amount, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
                    ii += 1
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal trans As SqlTransaction) As List(Of clsMakeSavingPaymentDeduction)
        Dim arr As List(Of clsMakeSavingPaymentDeduction) = Nothing
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT_DEDUCTION.*,TSPL_VENDOR_INVOICE_HEAD.Posting_Date  from TSPL_MAKE_SAVING_PAYMENT_DEDUCTION  
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_MAKE_SAVING_PAYMENT_DEDUCTION.AP_Invoice_No
where TSPL_MAKE_SAVING_PAYMENT_DEDUCTION.Doc_Code='" + strDocNo + "' and Ref_PK_ID='" + clsCommon.myCstr(Ref_PK_ID) + "' order by PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMakeSavingPaymentDeduction)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMakeSavingPaymentDeduction
                obj.SNo = arr.Count + 1
                obj.IsSelect = True
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.AP_Invoice_No = clsCommon.myCstr(dr("AP_Invoice_No"))
                obj.DocDate = clsCommon.myCDate(dr("Posting_Date"))
                obj.Amount = clsCommon.myCDecimal(dr("AMOUNT"))
                obj.Red_Ded_Amount = clsCommon.myCDecimal(dr("Red_Ded_Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

