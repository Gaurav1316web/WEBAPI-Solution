Imports common
Imports System.Data.SqlClient
Public Class clsMultipleProcDeductionHead
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime?
    Public MCC_Code As String = String.Empty
    Public MCC_Name As String = String.Empty
    Public loc_code As String = Nothing
    Public Remarks As String = Nothing
    Public Amount As Double = 0
    Public Trans_Type As String = String.Empty
    Public Comp_Code As String = Nothing
    Public Posting_Date As DateTime?
    Public RemittanceObject As clsRemittance = Nothing
    Public IsPosted As Integer = 0
    Public Voucher_No As String = String.Empty
    Public IsOpening As Integer = 0
    Public Arr As List(Of clsMultipleProcDeductionDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsMultipleProcDeductionHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = obj.SaveData(obj, isNewEntry, trans)
            If (isSaved) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsMultipleProcDeductionHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        If clsCommon.myLen(obj.loc_code) <= 0 Then
            Throw New Exception("Please first select Location")
        End If
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMultipleProcDeduction, obj.loc_code, obj.Document_Date, trans)


        Dim qry As String = "delete from TSPL_MULTIPLE_DEDUCTION_DETAIL where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        If obj.Arr.Count <= 0 Then
            Throw New Exception("Please fill at list one Account")
        End If

        Dim strDocNo As String = ""


        If (isNewEntry) Then
            obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.MultipleProcDed, "", obj.loc_code, True)
        End If
        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.loc_code)
        clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code, True)
        clsCommon.AddColumnsForChange(coll, "MCC_Name", obj.MCC_Name, True)
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Voucher_No", obj.Voucher_No)
        clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "IsOpening", obj.IsOpening)
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_DEDUCTION_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_DEDUCTION_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
        End If

        isSaved = isSaved AndAlso clsMultipleProcDeductionDetail.SaveData(obj.Document_No, Arr, trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_MULTIPLE_DEDUCTION_HEAD", "Document_No", "TSPL_MULTIPLE_DEDUCTION_DETAIL", "Document_No", trans)


        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal trans As SqlTransaction) As clsMultipleProcDeductionHead

        Dim obj As clsMultipleProcDeductionHead = Nothing

        Dim qry As String = "Select *  from TSPL_MULTIPLE_DEDUCTION_HEAD where Document_No='" + strDocumentNo + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMultipleProcDeductionHead()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.loc_code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
            obj.Voucher_No = clsCommon.myCstr(dt.Rows(0)("Voucher_No"))
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            If IsDBNull(dt.Rows(0)("Posting_Date")) = True Then
                obj.Posting_Date = Nothing
            Else
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.IsPosted = clsCommon.myCdbl(dt.Rows(0)("IsPosted"))
            obj.IsOpening = clsCommon.myCdbl(dt.Rows(0)("IsOpening"))

            qry = "Select TSPL_MULTIPLE_DEDUCTION_DETAIL.* from TSPL_MULTIPLE_DEDUCTION_Detail where Document_No='" + strDocumentNo + "' ORDER BY Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMultipleProcDeductionDetail)
                Dim objTr As clsMultipleProcDeductionDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMultipleProcDeductionDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.DeductionCode = clsCommon.myCstr(dr("DeductionCode"))
                    objTr.Deduction_Desc = clsCommon.myCstr(dr("Deduction_Desc"))
                    objTr.GL_Account_Code = clsCommon.myCstr(dr("GL_Account_Code"))
                    objTr.GL_Account_Desc = clsCommon.myCstr(dr("GL_Account_Desc"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objTr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    objTr.Account_Set = clsCommon.myCstr(dr("Account_Set"))
                    objTr.GSTRegistered = clsCommon.myCdbl(dr("GSTRegistered"))
                    objTr.Terms_Code = clsCommon.myCstr(dr("Terms_Code"))
                    objTr.Terms_Description = clsCommon.myCstr(dr("Terms_Description"))
                    objTr.Due_Date = clsCommon.myCstr(dr("Due_Date"))
                    objTr.Vendor_Control_AC = clsCommon.myCstr(dr("Vendor_Control_AC"))
                    objTr.against_deduction_docNo = clsCommon.myCstr(dr("against_deduction_docNo"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = clsMultipleProcDeductionHead.PostData(strDocNo, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Private Shared Function CreateAPInvoiceHeader_CreditNote(ByVal obj As clsMultipleProcDeductionHead, ByVal trans As SqlTransaction) As Boolean
        Dim VendAccSet As String = String.Empty
        For Each objTr As clsMultipleProcDeductionDetail In obj.Arr
            '' Ap Invoice Header for procurement Deduction type 
            Dim objVendInv As New clsVedorInvoiceHead()
            objVendInv.Invoice_Entry_Date = obj.Document_Date
            objVendInv.Document_Type = "C"
            objVendInv.Invoice_Type = "AP"
            objVendInv.loc_code = obj.loc_code
            objVendInv.Document_Total = objTr.Amount
            objVendInv.Posting_Date = obj.Document_Date
            objVendInv.Vendor_Invoice_Date = obj.Document_Date

            objVendInv.On_Hold = 0
            objVendInv.Remarks = objTr.Remarks
            objVendInv.Description = "Auto Credit Note Created against Procurement Deduction"
            objVendInv.Balance_Amt = objTr.Amount
            objVendInv.ISProcurementDeduction = 1
            objVendInv.Amount_Less_Discount = objVendInv.Document_Total
            objVendInv.Discount_Base = objVendInv.Document_Total
            objVendInv.isDeduction = 1
            objVendInv.Account_Set = objTr.Account_Set
            objVendInv.Vendor_Code = objTr.Vendor_Code
            objVendInv.Vendor_Name = objTr.Vendor_Name
            objVendInv.Terms_Code = objTr.Terms_Code
            objVendInv.Terms_Description = objTr.Terms_Description
            objVendInv.Due_Date = objTr.Due_Date
            objVendInv.Vendor_Control_AC = objTr.Vendor_Control_AC
            objVendInv.GSTRegistered = objTr.GSTRegistered
            objVendInv.MCC_Code = obj.MCC_Code
            objVendInv.MCC_Name = obj.MCC_Name
            objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

            '' Detail Level Saving
            Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()
            VendAccSet = objVendInv.Account_Set

            objVendInvTR.Detail_Line_No = objTr.Line_No
            If clsCommon.myLen(VendAccSet) <= 0 Then
                Throw New Exception("Please set vendor account set for vendor -" + objTr.Vendor_Code)
            End If
            objVendInvTR.Amount = objTr.Amount
            objVendInvTR.Amount_less_Discount = objTr.Amount
            objVendInvTR.Total_Amount = objTr.Amount

            objVendInvTR.GL_Account_Code = objTr.GL_Account_Code
            objVendInvTR.DeductionCode = objTr.DeductionCode
            objVendInvTR.DeductionDesc = objTr.Deduction_Desc
            If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            objVendInvTR.GL_Account_Desc = objTr.GL_Account_Desc
            objVendInv.Arr.Add(objVendInvTR)

            objVendInv.SaveData(objVendInv, True, trans)
            clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)

            clsDBFuncationality.getSingleValue("Update TSPL_MULTIPLE_DEDUCTION_DETAIL set Against_Deduction_DocNo ='" & objVendInv.Document_No & "' where document_no='" & obj.Document_No & "' and Line_no='" & objTr.Line_No & "' and Vendor_Code='" & objTr.Vendor_Code & "' ", trans)
        Next

        Return True
    End Function
    Private Shared Function CreateAPInvoiceHeader(ByVal obj As clsMultipleProcDeductionHead, ByVal trans As SqlTransaction) As Boolean
        Dim VendAccSet As String = String.Empty
        For Each objTr As clsMultipleProcDeductionDetail In obj.Arr
            '' Ap Invoice Header for procurement Deduction type 
            Dim objVendInv As New clsVedorInvoiceHead()
            objVendInv.Invoice_Entry_Date = obj.Document_Date
            objVendInv.Document_Type = "D"
            objVendInv.Invoice_Type = "AP"
            objVendInv.loc_code = obj.loc_code
            objVendInv.Document_Total = objTr.Amount
            objVendInv.Posting_Date = obj.Document_Date
            objVendInv.Vendor_Invoice_Date = obj.Document_Date

            objVendInv.On_Hold = 0
            objVendInv.Remarks = objTr.Remarks
            objVendInv.Description = "Auto Debit Note Created against Procurement Deduction"
            objVendInv.Balance_Amt = objTr.Amount
            objVendInv.ISProcurementDeduction = 1
            objVendInv.Amount_Less_Discount = objVendInv.Document_Total
            objVendInv.Discount_Base = objVendInv.Document_Total
            objVendInv.isDeduction = 1
            objVendInv.Account_Set = objTr.Account_Set
            objVendInv.Vendor_Code = objTr.Vendor_Code
            objVendInv.Vendor_Name = objTr.Vendor_Name
            objVendInv.Terms_Code = objTr.Terms_Code
            objVendInv.Terms_Description = objTr.Terms_Description
            objVendInv.Due_Date = objTr.Due_Date
            objVendInv.Vendor_Control_AC = objTr.Vendor_Control_AC
            objVendInv.GSTRegistered = objTr.GSTRegistered
            objVendInv.MCC_Code = obj.MCC_Code
            objVendInv.MCC_Name = obj.MCC_Name
            objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

            '' Detail Level Saving
            Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()
            VendAccSet = objVendInv.Account_Set

            objVendInvTR.Detail_Line_No = objTr.Line_No
            If clsCommon.myLen(VendAccSet) <= 0 Then
                Throw New Exception("Please set vendor account set for vendor -" + objTr.Vendor_Code)
            End If
            objVendInvTR.Amount = objTr.Amount
            objVendInvTR.Amount_less_Discount = objTr.Amount
            objVendInvTR.Total_Amount = objTr.Amount

            objVendInvTR.GL_Account_Code = objTr.GL_Account_Code
            objVendInvTR.DeductionCode = objTr.DeductionCode
            objVendInvTR.DeductionDesc = objTr.Deduction_Desc
            If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            objVendInvTR.GL_Account_Desc = objTr.GL_Account_Desc
            objVendInv.Arr.Add(objVendInvTR)

            objVendInv.SaveData(objVendInv, True, trans)
            clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)

            clsDBFuncationality.getSingleValue("Update TSPL_MULTIPLE_DEDUCTION_DETAIL set Against_Deduction_DocNo ='" & objVendInv.Document_No & "' where document_no='" & obj.Document_No & "' and Line_no='" & objTr.Line_No & "' and Vendor_Code='" & objTr.Vendor_Code & "' ", trans)
        Next

        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Post")
        End If


        Dim obj As clsMultipleProcDeductionHead = clsMultipleProcDeductionHead.GetData(strDocNo, trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMultipleProcDeduction, obj.loc_code, obj.Document_Date, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        If (clsCommon.myLen(obj.Posting_Date) > 0) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If
        clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.MCC_Code, obj.Document_Date, trans)
        ''richa agarwal UCD/17/11/21-000012
        If clsCommon.CompairString(clsCommon.myCstr(obj.Trans_Type), "Addition") = CompairStringResult.Equal Then
            CreateAPInvoiceHeader_CreditNote(obj, trans)
        Else
            CreateAPInvoiceHeader(obj, trans)
        End If



        qry = "Update TSPL_MULTIPLE_DEDUCTION_HEAD set Posting_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy") + "',isPosted=1 ,Modify_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True

    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If


        Dim obj As clsMultipleProcDeductionHead = clsMultipleProcDeductionHead.GetData(strDocNo, trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMultipleProcDeduction, obj.loc_code, obj.Document_Date, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If (clsCommon.myLen(obj.Posting_Date) > 0) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_MULTIPLE_DEDUCTION_DETAIL where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MULTIPLE_DEDUCTION_HEAD where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
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
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "Select Posting_Date from tspl_multiple_deduction_head WHERE Document_No='" + strDocNo + "'"
            If clsCommon.myLen(clsDBFuncationality.getSingleValue(Qry, trans)) <= 0 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim obj As clsMultipleProcDeductionHead = clsMultipleProcDeductionHead.GetData(strDocNo, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Reverse And UnPost")
            End If


            '' to insert ap documents into temporaray table
            Qry = "delete from TSPL_Temp_Proc_Deduction "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "insert into TSPL_Temp_Proc_Deduction select Against_Deduction_DocNo from TSPL_MULTIPLE_DEDUCTION_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)



            ''to check  document used into payment process 
            Qry = "select Doc_No from TSPL_PAYMENT_PROCESS_DEDUCTION where AP_Invoice_No in (select * from TSPL_Temp_Proc_Deduction)"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following Payment Process -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Doc_No"))
                Next
                Throw New Exception(Qry)
            End If

            ''to check  document used into payment
            Qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in (select * from TSPL_Temp_Proc_Deduction)"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = ""
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT isnull(Reverse_Code,'')  from TSPL_BANK_REVERSE WHERE DOCUMENT_NO = '" & clsCommon.myCstr(dr("Payment_No")) & "' AND isnull(TSPL_BANK_REVERSE.POST,'')='P'", trans))) <= 0 Then
                        Qry += Environment.NewLine + clsCommon.myCstr(dr("Payment_No"))
                    End If
                Next
                If clsCommon.myLen(Qry) > 0 Then
                    Throw New Exception("Current AP-Invoice is used in following Payment -" & Qry)
                End If

            End If

            '-----Delete Journal ENtry against AP Invoice Debit Note----
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where  TSPL_JOURNAL_MASTER.Source_Code='AP-DN' and Source_Doc_No in (select Against_Deduction_DocNo from TSPL_MULTIPLE_DEDUCTION_DETAIL where Document_No='" & obj.Document_No & "')", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where  TSPL_JOURNAL_MASTER.Source_Code='AP-DN' and Source_Doc_No in (select Against_Deduction_DocNo from TSPL_MULTIPLE_DEDUCTION_DETAIL where Document_No='" & obj.Document_No & "'))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where  TSPL_JOURNAL_MASTER.Source_Code='AP-DN' and Source_Doc_No in (select Against_Deduction_DocNo from TSPL_MULTIPLE_DEDUCTION_DETAIL where Document_No='" & obj.Document_No & "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "update  TSPL_MULTIPLE_DEDUCTION_DETAIL set  Against_Deduction_DocNo=null where Document_No='" & obj.Document_No & "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            ''delete data from Ap Invoice
            Qry = "delete from tspl_vendor_invoice_detail where  document_No in (select * from TSPL_Temp_Proc_Deduction) "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from tspl_vendor_invoice_head where  document_No in (select * from TSPL_Temp_Proc_Deduction) "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            ''----------------

            Qry = "Update tspl_multiple_deduction_head set Posting_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "',isPosted=0 where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            'trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsMultipleProcDeductionDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Line_No As Integer = 0
    Public DeductionCode As String = Nothing
    Public Deduction_Desc As String = Nothing ''Not a table column
    Public GL_Account_Code As String = Nothing
    Public GL_Account_Desc As String = Nothing
    Public Amount As Double = 0
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Account_Set As String = Nothing
    Public GSTRegistered As Integer = 0
    Public Terms_Code As String = Nothing
    Public Terms_Description As String = Nothing
    Public Due_Date As String = Nothing
    Public Vendor_Control_AC As String = Nothing
    Public against_deduction_docNo As String = Nothing
    Public Remarks As String = Nothing

    Public VLCUploderCode As String ''Not a table column
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMultipleProcDeductionDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMultipleProcDeductionDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
                clsCommon.AddColumnsForChange(coll, "Vendor_Control_AC", obj.Vendor_Control_AC)
                clsCommon.AddColumnsForChange(coll, "DeductionCode", clsCommon.myCstr(obj.DeductionCode))
                clsCommon.AddColumnsForChange(coll, "Deduction_Desc", clsCommon.myCstr(obj.Deduction_Desc))
                clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Desc", obj.GL_Account_Desc)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
                clsCommon.AddColumnsForChange(coll, "Terms_Description", obj.Terms_Description)
                If obj.Due_Date IsNot Nothing AndAlso clsCommon.myLen(obj.Due_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Account_Set", obj.Account_Set)
                clsCommon.AddColumnsForChange(coll, "GSTRegistered", obj.GSTRegistered)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_DEDUCTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function


End Class
