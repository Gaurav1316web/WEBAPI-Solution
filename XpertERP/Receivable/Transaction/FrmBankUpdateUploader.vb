''updation by richa against ticket no. BM00000008140
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmBankUpdateUploader
    Private Sub RadButton149_Click(sender As Object, e As EventArgs) Handles RadButton149.Click
        Try

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT SNo from TEMP_PR_CHANGE_PARY_DATE_AMOUNT")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow("This table have data do you want to reset", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    For Each dr As DataRow In dt.Rows
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("SNo")), "TEMP_PR_CHANGE_PARY_DATE_AMOUNT", "SNo", Nothing)
                    Next
                    clsDBFuncationality.ExecuteNonQuery("delete from TEMP_PR_CHANGE_PARY_DATE_AMOUNT")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadButton148_Click(sender As Object, e As EventArgs) Handles RadButton148.Click
        Dim counter As Integer = 0
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT count(*) as CN from TEMP_PR_CHANGE_PARY_DATE_AMOUNT")) > 0 Then
                Throw New Exception("First reset")
            End If
            If transportSql.importExcel(gv, "Voucher Dt.", "BPV", "Credits", "Code", "Related with Vendor", "Payment Type") Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        counter += 1
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "SNo", counter)
                        clsCommon.AddColumnsForChange(coll, "DocumentNo", clsCommon.myCstr(grow.Cells("BPV").Value))
                        clsCommon.AddColumnsForChange(coll, "DocumentType", clsCommon.myCstr(grow.Cells("Payment Type").Value))
                        clsCommon.AddColumnsForChange(coll, "DocumentDate", clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("Voucher Dt.").Value), "dd/MMM/yyyy hh:mm tt"))
                        clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCstr(grow.Cells("Credits").Value))
                        clsCommon.AddColumnsForChange(coll, "ACode", clsCommon.myCstr(grow.Cells("Code").Value))
                        clsCommon.AddColumnsForChange(coll, "AName", clsCommon.myCstr(grow.Cells("Related with Vendor").Value))
                        clsCommonFunctionality.UpdateDataTable(coll, "TEMP_PR_CHANGE_PARY_DATE_AMOUNT", OMInsertOrUpdate.Insert, "", trans)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    myMessages.myExceptions(ex)
                Finally
                    Me.Controls.Remove(gv)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Error at Row No " + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub RadButton150_Click(sender As Object, e As EventArgs) Handles RadButton150.Click
        Try
            If clsCommon.myLen(txtRPBank.Value) <= 0 Then
                txtRPBank.Focus()
                Throw New Exception("Please select Bank")
            End If
            If clsCommon.myLen(txtRPLocation.Value) <= 0 Then
                txtRPLocation.Focus()
                Throw New Exception("Please select location")
            End If
            If clsCommon.myLen(txtPaymentMode.Value) <= 0 Then
                txtPaymentMode.Focus()
                Throw New Exception("Please select payment mode")
            End If
            Dim qry As String = "select * from TEMP_PR_CHANGE_PARY_DATE_AMOUNT where Status is null"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Pending Document " + clsCommon.myCstr(dt.Rows.Count) + "Found" + Environment.NewLine + "Continue...", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    DocumentExists()
                    DocumentNotExists()
                    qry = "select * from TEMP_PR_CHANGE_PARY_DATE_AMOUNT where Status is null"
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        clsCommon.MyMessageBoxShow("Problem in " + clsCommon.myCstr(dt.Rows.Count) + " transaction", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Task completed", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRPBank__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRPBank._MYValidating
        Dim Qry As String = clsERPFuncationality.glbankqueryNew("")
        Dim strWhrclas As String = " TSPL_bank_master.INACTIVE ='Active' "
        txtRPBank.Value = clsCommon.ShowSelectForm("NewBank@Uti", Qry, "Code", strWhrclas, txtRPBank.Value, "Code", isButtonClicked)
    End Sub

    Private Sub txtRPLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRPLocation._MYValidating
        Try
            Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
            Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtRPLocation.Value = clsCommon.ShowSelectForm("PELoc@u", qry, "Code", WhrCls, txtRPLocation.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtPaymentMode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentMode._MYValidating
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtRPBank.Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtRPBank.Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS')", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            End If

        End If
    End Sub

    Sub DocumentExists()
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            qry = "select xx.*,TSPL_BANK_MASTER.Bank_Type from ( " + Environment.NewLine + _
            "select x.*,TSPL_BANK_REVERSE.Reverse_Code,(select top 1 tspl_BankReco_Detail.Reconciliation_Id  from tspl_BankReco_Detail where tspl_BankReco_Detail.Document_No=x.DocumentNo and tspl_BankReco_Detail.Reconciliation_Status='C') as ClearRecoNo " + Environment.NewLine + _
            ",(select count(tspl_BankReco_Detail.Reconciliation_Id) from tspl_BankReco_Detail where tspl_BankReco_Detail.Document_No=x.DocumentNo and tspl_BankReco_Detail.Reconciliation_Status<>'C') as OutstandingReconNo  " + Environment.NewLine + _
            ",case when x.TransactionType='P' then (select top 1 Payment_Type  from TSPL_PAYMENT_HEADER where Payment_Type in ('AD') and Applied_Payment=x.DocumentNo )  " + Environment.NewLine + _
            "	else case when x.TransactionType='R' then (select  top 1 Receipt_No from TSPL_RECEIPT_HEADER where Receipt_Type in ('A') and Applied_Receipt=x.DocumentNo) else '' end end as ApplyDocumentCreated " + Environment.NewLine + _
            "from ( " + Environment.NewLine + _
            "select BBTemp.DocumentNo,case when TSPL_PAYMENT_HEADER.Payment_No=BBTemp.DocumentNo then 'P' else case when TSPL_RECEIPT_HEADER.Receipt_No=BBTemp.DocumentNo then 'R' else null end end as TransactionType " + Environment.NewLine + _
            ",TSPL_JOURNAL_MASTER.Voucher_No " + Environment.NewLine + _
            ",case when TSPL_PAYMENT_HEADER.Payment_No=BBTemp.DocumentNo then case when TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') then 1 else 0 end else case when TSPL_RECEIPT_HEADER.Receipt_No=BBTemp.DocumentNo then case when TSPL_RECEIPT_HEADER.Receipt_Type in ('A','O','F') then 1 else 0 end  else null end end as ISValidTransactionType   " + Environment.NewLine + _
            ",case when TSPL_PAYMENT_HEADER.Payment_No=BBTemp.DocumentNo then TSPL_PAYMENT_HEADER.Bank_Code else case when TSPL_RECEIPT_HEADER.Receipt_No=BBTemp.DocumentNo then TSPL_RECEIPT_HEADER.Bank_Code else null end end as Bank_Code " + Environment.NewLine + _
            ",BBTemp.ACode as NewACode,BBTemp.DocumentDate as NewDocumentDate,BBTemp.Amount as NewAmount,BBTemp.DocumentType as NewDocumentType,BBTemp.SNo as NewSNo,TSPL_REMITTANCE.Remittance_Code" + Environment.NewLine + _
            "from (select * from TEMP_PR_CHANGE_PARY_DATE_AMOUNT where isnull(DocumentNo,'') <>'' and status is null) as BBTemp " + Environment.NewLine + _
            "left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=BBTemp.DocumentNo " + Environment.NewLine + _
            "left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=BBTemp.DocumentNo " + Environment.NewLine + _
            "left outer join TSPL_JOURNAL_MASTER  on TSPL_JOURNAL_MASTER.Source_Doc_No=BBTemp.DocumentNo  " + Environment.NewLine + _
            "left outer join TSPL_REMITTANCE on TSPL_REMITTANCE.Document_No=BBTemp.DocumentNo  " + Environment.NewLine + _
            ") x  " + Environment.NewLine + _
            "left outer join TSPL_BANK_REVERSE on TSPL_BANK_REVERSE.Document_No=x.DocumentNo  " + Environment.NewLine + _
            "where x.TransactionType in ('P','R')  " + Environment.NewLine + _
            " ) xx     " + Environment.NewLine + _
            "left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.Bank_Code=xx.Bank_Code" + Environment.NewLine + _
            "order by NewSNo"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'If common.clsCommon.MyMessageBoxShow("Exists Document " + clsCommon.myCstr(dt.Rows.Count) + " Transaction", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                clsCommon.ProgressBarPercentShow()
                Dim oldValueSkipLockLocation As String = clsFixedParameter.GetData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, Nothing)
                For ii As Integer = 0 To dt.Rows.Count - 1
                    Dim strDocNo As String = clsCommon.myCstr(dt.Rows(ii)("DocumentNo"))
                    Dim strVoucherNo As String = clsCommon.myCstr(dt.Rows(ii)("Voucher_No"))
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Dim collStatus As New Hashtable()
                    Try

                        If clsCommon.myLen(dt.Rows(ii)("Reverse_Code")) > 0 Then
                            Throw New Exception("Bank reverse: " + clsCommon.myCstr(dt.Rows(ii)("Reverse_Code")) + " Document created ")
                        End If
                        If clsCommon.myLen(dt.Rows(ii)("ClearRecoNo")) > 0 Then
                            Throw New Exception("Clear Bank Reco found : " + clsCommon.myCstr(dt.Rows(ii)("ClearRecoNo")) + "")
                        End If
                        If clsCommon.myLen(dt.Rows(ii)("ApplyDocumentCreated")) > 0 Then
                            Throw New Exception("Apply Document created : " + clsCommon.myCstr(dt.Rows(ii)("ApplyDocumentCreated")) + "")
                        End If
                        If clsCommon.myCdbl(dt.Rows(ii)("ISValidTransactionType")) <= 0 Then
                            Throw New Exception("Document Type should be Advance/On Account/Refund(Receipt)")
                        End If
                        If clsCommon.myLen(dt.Rows(ii)("Remittance_Code")) > 0 Then
                            Throw New Exception("TDS Remittance found: " + clsCommon.myCstr(dt.Rows(ii)("Remittance_Code")) + " Document created ")
                        End If

                        EnableDisableFinancialTrigger(False, trans)
                        clsFixedParameter.UpdateData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, 1, trans)
                        Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dt.Rows(ii)("Bank_Code")) + "'", trans)

                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("TransactionType")), "P") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("NewDocumentType")), "Customer refund") = CompairStringResult.Equal Then
                                ''TO DELET DATA FROM PAYMENT TABLE
                                clsPaymentHeader.ReverseAndUnpost(strDocNo, trans, True)
                                clsPaymentHeader.fundelete("", strDocNo, "", trans)
                                ''------------

                                ''TO INSERT DATA INTO RECEIPT TABLE
                                Dim strNewDocNo As String = ""
                                Dim obj As New clsRcptEntryHeader()
                                obj.Receipt_Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(ii)("NewDocumentDate")), "dd/MMM/yyyy hh:mm tt")
                                obj.Receipt_Post_Date = obj.Receipt_Date
                                obj.Bank_Code = txtRPBank.Value
                                obj.Receipt_Type = "F"
                                obj.Payment_Code = txtPaymentMode.Value
                                obj.Location_GL_Code = txtRPLocation.Value
                                obj.CFormRecd = "0"
                                obj.CForm_InvoiceNo = ""
                                If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                                    Throw New Exception("invalid payment Mode Cheque")
                                Else
                                    obj.Cheque_No = ""
                                    obj.Cheque_Date = Nothing
                                End If
                                obj.Cust_Code = clsCommon.myCstr(dt.Rows(ii)("NewACode"))
                                obj.Customer_Name = clsCustomerMaster.GetName(obj.Cust_Code, trans)
                                obj.Receipt_Amount = clsCommon.myCdbl(dt.Rows(ii)("NewAmount"))
                                obj.Balance_Amt = obj.Receipt_Amount
                                obj.UnApply_Amt = obj.Receipt_Amount
                                obj.FIFO_Balance = obj.Receipt_Amount
                                obj.RECEIVED_AMOUNT_BASE_CURRENCY = obj.Receipt_Amount
                                obj.IsSalesmanType = "N"
                                obj.SecurityDeposit = "N"
                                obj.IsRecoCleared = "N"
                                obj.IsChkReverse = "N"
                                obj.ConvRate = 1
                                obj.ConvRateOld = 1
                                If clsCommon.myLen(obj.Cust_Code) > 0 Then
                                    Dim VenCurr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_Customer_MASTER where Cust_CODE='" & clsCommon.myCstr(obj.Cust_Code) & "'", trans))
                                    If clsCommon.myLen(VenCurr) > 0 Then
                                        obj.CURRENCY_CODE = VenCurr
                                    End If
                                    Dim dtMC As DataTable
                                    If clsCommon.myLen(obj.CURRENCY_CODE) > 0 Then
                                        dtMC = clsModuleCurrencyMapping.GetLatestCurConvRateDT(obj.Receipt_Date, obj.CURRENCY_CODE, trans)
                                        If dtMC.Rows.Count = 0 Then
                                            If obj.CURRENCY_CODE = objCommonVar.BaseCurrencyCode Then
                                                obj.ConvRate = 1
                                                obj.ConvRateOld = 1
                                            Else
                                                Throw New Exception("Conversion rate not entered for currency '" & obj.CURRENCY_CODE & "'")
                                            End If
                                        Else
                                            obj.ConvRate = clsCommon.myCdbl(dtMC.Rows(0).Item("Rate"))
                                            obj.ConvRateOld = clsCommon.myCdbl(dtMC.Rows(0).Item("Rate"))
                                            obj.ApplicableFrom = clsCommon.GetPrintDate(dtMC.Rows(0).Item("FROM_DATE"), "dd/MMM/yyyy")
                                        End If
                                    End If
                                End If
                                obj.Narration = obj.Entry_Desc
                                obj.SaveData(obj, True, trans, strDocNo)

                                qry = clsRcptEntryHeader.GetQuery(strDocNo)
                                Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                clsRcptEntryHeader.CreateJournalEntry(False, dtReceipt, strDocNo, LocSegmentCode, trans, strVoucherNo)

                                Dim strQue2 As String = "update TSPL_receipt_header set posted = 'Y',Modify_By='" + objCommonVar.CurrentUserCode + "' where receipt_no ='" + strDocNo + "' "
                                clsDBFuncationality.ExecuteNonQuery(strQue2, trans)
                                ''-----------
                            Else
                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Vendor_Code", clsCommon.myCstr(dt.Rows(ii)("NewACode")))
                                clsCommon.AddColumnsForChange(coll, "Vendor_Name", clsVendorMaster.GetName(clsCommon.myCstr(dt.Rows(ii)("NewACode")), trans))
                                clsCommon.AddColumnsForChange(coll, "Payment_Date", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(ii)("NewDocumentDate")), "dd/MMM/yyyy hh:mm tt"))
                                clsCommon.AddColumnsForChange(coll, "Payment_Post_Date", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(ii)("NewDocumentDate")), "dd/MMM/yyyy hh:mm tt"))
                                clsCommon.AddColumnsForChange(coll, "Payment_Amount", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommon.AddColumnsForChange(coll, "Total_Prepayment", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommon.AddColumnsForChange(coll, "Balance_Amt", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommon.AddColumnsForChange(coll, "FIFO_Balance", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommon.AddColumnsForChange(coll, "PAYMENT_AMOUNT_BASE_CURRENCY", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_HEADER", OMInsertOrUpdate.Update, "Payment_No='" + strDocNo + "'", trans)

                                Dim objPay As clsPaymentHeader = clsPaymentHeader.GetData(strDocNo, NavigatorType.Current, trans)
                                clsPaymentHeader.CreateJournalEntry(objPay, "MPayable", strVoucherNo, trans)
                                clsBankReco.SetOutstandingEntry(strDocNo, objPay.Payment_Date, "Payment", trans, False)
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("TransactionType")), "R") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("NewDocumentType")), "Customer refund") = CompairStringResult.Equal Then
                                ''TO DELET DATA FROM receipt TABLE
                                clsRcptEntryHeader.ReverseAndUnpost(strDocNo, trans)
                                clsRcptEntryHeader.fundelete(strDocNo, trans)
                                ''------------

                                ''TO INSERT DATA INTO Payment TABLE
                                 Dim obj As New clsPaymentHeader()
                               
                                obj.Payment_Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(ii)("NewDocumentDate")), "dd/MMM/yyyy hh:mm tt")
                                obj.Payment_Post_Date = obj.Payment_Date
                                obj.Bank_Code = txtRPBank.Value
                                obj.Payment_Type = "OA"
                                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(ii)("NewACode"))
                                obj.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                                obj.Location_GL_Code = txtRPLocation.Value
                                obj.Payment_Code = txtPaymentMode.Value
                                If clsCommon.myLen(obj.Location_Code) > 0 Then
                                    obj.Location_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') AS Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" + clsCommon.myCstr(obj.Location_Code) + "'", trans))
                                Else
                                    obj.Location_Description = ""
                                End If
                                If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                                    Dim VenCurr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE='" & clsCommon.myCstr(obj.Vendor_Code) & "'", trans))
                                    If clsCommon.myLen(VenCurr) > 0 Then
                                        obj.CURRENCY_CODE = VenCurr
                                    End If
                                End If
                                obj.ConvRate = 1
                                obj.ConvRateOld = 1
                                ''
                                If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                                    Throw New Exception("invalid payment Mode Cheque")
                                Else
                                    obj.Cheque_No = ""
                                    obj.Cheque_Date = Nothing
                                End If

                                obj.Total_Prepayment = clsCommon.myCdbl(dt.Rows(ii)("NewAmount"))
                                obj.Payment_Amount = obj.Total_Prepayment
                                obj.Balance_Amt = obj.Total_Prepayment
                                obj.PAYMENT_AMOUNT_BASE_CURRENCY = obj.Total_Prepayment
                                obj.IsChkReverse = "N"
                                obj.SaveData1(obj, True, trans, strDocNo)
                               

                                Dim objPay As clsPaymentHeader = clsPaymentHeader.GetData(strDocNo, NavigatorType.Current, trans)
                                clsPaymentHeader.CreateJournalEntry(objPay, "MPayable", strVoucherNo, trans)

                                clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set Posted = '1',Modify_By='" + objCommonVar.CurrentUserCode + "' where Payment_No = '" + strDocNo + "'", trans)
                                ''-----------
                            Else

                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Cust_Code", clsCommon.myCstr(dt.Rows(ii)("NewACode")))
                                clsCommon.AddColumnsForChange(coll, "Customer_Name", clsCustomerMaster.GetName(clsCommon.myCstr(dt.Rows(ii)("NewACode")), trans))
                                clsCommon.AddColumnsForChange(coll, "Receipt_Date", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(ii)("NewDocumentDate")), "dd/MMM/yyyy"))
                                clsCommon.AddColumnsForChange(coll, "Receipt_Post_Date", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(ii)("NewDocumentDate")), "dd/MMM/yyyy"))
                                clsCommon.AddColumnsForChange(coll, "Receipt_Amount", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommon.AddColumnsForChange(coll, "Balance_Amt", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommon.AddColumnsForChange(coll, "UnApply_Amt", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommon.AddColumnsForChange(coll, "RECEIVED_AMOUNT_BASE_CURRENCY", clsCommon.myCdbl(dt.Rows(ii)("NewAmount")))
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_HEADER", OMInsertOrUpdate.Update, "Receipt_No='" + strDocNo + "'", trans)

                                qry = clsRcptEntryHeader.GetQuery(strDocNo)
                                Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                clsRcptEntryHeader.CreateJournalEntry(False, dtReceipt, strDocNo, LocSegmentCode, trans, strVoucherNo)
                                clsBankReco.SetOutstandingEntry(strDocNo, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Date")), "Receipt", trans, False)
                            End If
                        End If
                            EnableDisableFinancialTrigger(True, trans)
                            clsCommon.AddColumnsForChange(collStatus, "Status", 1)
                            clsCommon.AddColumnsForChange(collStatus, "ERROR", Nothing, True)
                            clsCommonFunctionality.UpdateDataTable(collStatus, "TEMP_PR_CHANGE_PARY_DATE_AMOUNT", OMInsertOrUpdate.Update, "SNo='" + clsCommon.myCstr(dt.Rows(ii)("NewSNo")) + "'", trans)
                            clsFixedParameter.UpdateData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, oldValueSkipLockLocation, trans)
                            trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        If clsCommon.myLen(ex.Message) > 5000 Then
                            clsCommon.AddColumnsForChange(collStatus, "ERROR", ex.Message.Substring(0, 4999))
                        Else
                            clsCommon.AddColumnsForChange(collStatus, "ERROR", ex.Message)
                        End If
                        clsCommonFunctionality.UpdateDataTable(collStatus, "TEMP_PR_CHANGE_PARY_DATE_AMOUNT", OMInsertOrUpdate.Update, "SNo='" + clsCommon.myCstr(dt.Rows(ii)("NewSNo")) + "'")
                    End Try
                    clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, "Step [1]" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                Next
                clsCommon.ProgressBarPercentHide()
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub DocumentNotExists()
        Try
            ''Document No not Exists
            Dim qry As String = "select * from TEMP_PR_CHANGE_PARY_DATE_AMOUNT where isnull(DocumentNo,'') ='' and status is null and NewDocumentNo is null "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'If common.clsCommon.MyMessageBoxShow("New Document " + clsCommon.myCstr(dt.Rows.Count) + " Transaction", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                clsCommon.ProgressBarPercentShow()
                Dim oldValueSkipLockLocation As String = clsFixedParameter.GetData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, Nothing)
                For ii As Integer = 0 To dt.Rows.Count - 1
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Dim collStatus As New Hashtable()
                    Dim strNewDocNo As String = ""

                    Try
                        EnableDisableFinancialTrigger(False, trans)

                        clsFixedParameter.UpdateData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, 1, trans)
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("DocumentType")), "Vendor Payment") = CompairStringResult.Equal Then
                            Dim obj As New clsPaymentHeader()
                            'obj.Payment_No = clsCommon.myCstr(row.Cells("gvDocNo").Value)
                            'obj.Entry_Desc = clsCommon.myCstr(row.Cells("gvNarration").Value)
                            obj.Payment_Date = clsCommon.myCDate(dt.Rows(ii)("DocumentDate"))
                            obj.Payment_Post_Date = obj.Payment_Date
                            obj.Bank_Code = txtRPBank.Value
                            obj.Payment_Type = "OA"
                            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(ii)("ACode"))
                            obj.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                            obj.Location_GL_Code = txtRPLocation.Value
                            obj.Payment_Code = txtPaymentMode.Value
                            If clsCommon.myLen(obj.Location_Code) > 0 Then
                                obj.Location_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') AS Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" + clsCommon.myCstr(obj.Location_Code) + "'", trans))
                            Else
                                obj.Location_Description = ""
                            End If
                            If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                                Dim VenCurr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE='" & clsCommon.myCstr(obj.Vendor_Code) & "'", trans))
                                If clsCommon.myLen(VenCurr) > 0 Then
                                    obj.CURRENCY_CODE = VenCurr
                                End If
                            End If
                            obj.ConvRate = 1
                            obj.ConvRateOld = 1
                            ''
                            If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                                Throw New Exception("invalid payment Mode Cheque")
                            Else
                                obj.Cheque_No = ""
                                obj.Cheque_Date = Nothing
                            End If

                            obj.Total_Prepayment = clsCommon.myCdbl(dt.Rows(ii)("Amount"))
                            obj.Payment_Amount = obj.Total_Prepayment
                            obj.Balance_Amt = obj.Total_Prepayment
                            obj.PAYMENT_AMOUNT_BASE_CURRENCY = obj.Total_Prepayment
                            obj.IsChkReverse = "N"
                            'obj.QuickEntryNo = EntryNo
                            obj.SaveData1(obj, True, trans)
                            strNewDocNo = obj.Payment_No
                            clsPaymentHeader.PostData(strNewDocNo, , trans)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("DocumentType")), "Customer refund") = CompairStringResult.Equal Then
                            Dim obj As New clsRcptEntryHeader()
                            'obj.Receipt_No = clsCommon.myCstr(row.Cells("gvDocNo").Value)
                            'obj.Entry_Desc = clsCommon.myCstr(row.Cells("gvNarration").Value)
                            obj.Receipt_Date = clsCommon.myCDate(dt.Rows(ii)("DocumentDate"))
                            obj.Receipt_Post_Date = obj.Receipt_Date
                            obj.Bank_Code = txtRPBank.Value
                            obj.Receipt_Type = "F"
                            obj.Payment_Code = txtPaymentMode.Value
                            obj.Location_GL_Code = txtRPLocation.Value
                            obj.CFormRecd = "0"
                            obj.CForm_InvoiceNo = ""
                            If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                                Throw New Exception("invalid payment Mode Cheque")
                            Else
                                obj.Cheque_No = ""
                                obj.Cheque_Date = Nothing
                            End If
                            obj.Cust_Code = clsCommon.myCstr(dt.Rows(ii)("ACode"))
                            obj.Customer_Name = clsCustomerMaster.GetName(obj.Cust_Code, trans)
                            obj.Receipt_Amount = clsCommon.myCdbl(dt.Rows(ii)("Amount"))
                            obj.Balance_Amt = obj.Receipt_Amount
                            obj.UnApply_Amt = obj.Receipt_Amount
                            obj.FIFO_Balance = obj.Receipt_Amount
                            obj.RECEIVED_AMOUNT_BASE_CURRENCY = obj.Receipt_Amount
                            obj.IsSalesmanType = "N"
                            obj.SecurityDeposit = "N"
                            obj.IsRecoCleared = "N"
                            obj.IsChkReverse = "N"
                            obj.ConvRate = 1
                            obj.ConvRateOld = 1
                            If clsCommon.myLen(obj.Cust_Code) > 0 Then
                                Dim VenCurr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_Customer_MASTER where Cust_CODE='" & clsCommon.myCstr(obj.Cust_Code) & "'", trans))
                                If clsCommon.myLen(VenCurr) > 0 Then
                                    obj.CURRENCY_CODE = VenCurr
                                End If
                                Dim dtMC As DataTable
                                If clsCommon.myLen(obj.CURRENCY_CODE) > 0 Then
                                    dtMC = clsModuleCurrencyMapping.GetLatestCurConvRateDT(obj.Receipt_Date, obj.CURRENCY_CODE, trans)
                                    If dtMC.Rows.Count = 0 Then
                                        If obj.CURRENCY_CODE = objCommonVar.BaseCurrencyCode Then
                                            obj.ConvRate = 1
                                            obj.ConvRateOld = 1
                                        Else
                                            Throw New Exception("Conversion rate not entered for currency '" & obj.CURRENCY_CODE & "'")
                                        End If
                                    Else
                                        obj.ConvRate = clsCommon.myCdbl(dtMC.Rows(0).Item("Rate"))
                                        obj.ConvRateOld = clsCommon.myCdbl(dtMC.Rows(0).Item("Rate"))
                                        obj.ApplicableFrom = clsCommon.GetPrintDate(dtMC.Rows(0).Item("FROM_DATE"), "dd/MMM/yyyy")
                                    End If
                                End If
                            End If
                            obj.Narration = obj.Entry_Desc
                            'obj.QuickEntryNo = EntryNo
                            obj.SaveData(obj, True, trans)
                            strNewDocNo = obj.Receipt_No
                            clsRcptEntryHeader.funRcptPost(strNewDocNo, trans)
                        End If
                        EnableDisableFinancialTrigger(True, trans)
                        clsCommon.AddColumnsForChange(collStatus, "Status", 1)
                        clsCommon.AddColumnsForChange(collStatus, "NewDocumentNo", strNewDocNo)
                        clsCommon.AddColumnsForChange(collStatus, "ERROR", Nothing, True)
                        clsCommonFunctionality.UpdateDataTable(collStatus, "TEMP_PR_CHANGE_PARY_DATE_AMOUNT", OMInsertOrUpdate.Update, "SNo='" + clsCommon.myCstr(dt.Rows(ii)("SNo")) + "'", trans)
                        clsFixedParameter.UpdateData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, oldValueSkipLockLocation, trans)
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        If clsCommon.myLen(ex.Message) > 5000 Then
                            clsCommon.AddColumnsForChange(collStatus, "ERROR", ex.Message.Substring(0, 4999))
                        Else
                            clsCommon.AddColumnsForChange(collStatus, "ERROR", ex.Message)
                        End If
                        clsCommonFunctionality.UpdateDataTable(collStatus, "TEMP_PR_CHANGE_PARY_DATE_AMOUNT", OMInsertOrUpdate.Update, "SNo='" + clsCommon.myCstr(dt.Rows(ii)("SNo")) + "'")
                    End Try
                    clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, "Step [2] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                Next
                clsCommon.ProgressBarPercentHide()
                'End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function EnableDisableFinancialTrigger(ByVal isEnable As Boolean, ByVal tran As SqlTransaction) As Boolean
        Dim strPre As String = "disable"
        If isEnable Then
            strPre = "enable"
        End If
        clsDBFuncationality.ExecuteNonQuery(strPre + " TRIGGER dbo.TRG_JD_FiscaYearEndNoUpdateNoDelete ON dbo.TSPL_JOURNAL_DETAILS ", tran)
        clsDBFuncationality.ExecuteNonQuery(strPre + " TRIGGER dbo.TRG_JM_FiscaYearEndNoUpdateNoDelete ON dbo.TSPL_JOURNAL_MASTER ", tran)
        Return True
    End Function

    '' to update bank code in Payment /receipt code starts here 

    Private Sub RadButton145_Click(sender As Object, e As EventArgs) Handles RadButton145.Click
           
            Dim counter As Integer = 0
            Try
                Dim gv As New RadGridView()
                Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today

            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("DocumentNo", "varchar(30) null")
            coll.Add("BankCode", "varchar(30) null")
            clsCommonFunctionality.CreateOrAlterTable("TEMP_CREATED_RP_BANK_CHANGE_FOR_EXCEL", coll)

            coll = New Dictionary(Of String, String)()
            coll.Add("DocumentNo", "varchar(30) null")
            coll.Add("BankCode", "varchar(30) null")
            clsCommonFunctionality.CreateOrAlterTable("TEMP_DELETED_RP_BANK_CHANGE_FOR_EXCEL", coll)

            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CREATED_RP_BANK_CHANGE_FOR_EXCEL")
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_RP_BANK_CHANGE_FOR_EXCEL")

            'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT count(*) as CN from TEMP_DELETED_RP_BANK_CHANGE_FOR_EXCEL")) > 0 Then
            '    Throw New Exception("First reset")
            'End If
            If transportSql.importExcel(gv, "DocNo", "BankCode") Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        counter += 1
                        Dim coll1 As New Hashtable()
                        clsCommon.AddColumnsForChange(coll1, "DocumentNo", clsCommon.myCstr(grow.Cells("DocNo").Value))
                        clsCommon.AddColumnsForChange(coll1, "BankCode", clsCommon.myCstr(grow.Cells("BankCode").Value))
                        clsCommonFunctionality.UpdateDataTable(coll1, "TEMP_DELETED_RP_BANK_CHANGE_FOR_EXCEL", OMInsertOrUpdate.Insert, "", trans)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    BankExcelsavedata()
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    myMessages.myExceptions(ex)
                Finally
                    Me.Controls.Remove(gv)
                End Try
            End If
            Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Error at Row No " + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message, Me.Text)
        End Try
    End Sub

    Sub BankExcelsavedata()
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try


            qry = "select xx.*,TSPL_BANK_MASTER.Bank_Type from (" + Environment.NewLine + _
            "select x.*,TSPL_BANK_REVERSE.Reverse_Code,(select top 1 tspl_BankReco_Detail.Reconciliation_Id  from tspl_BankReco_Detail where tspl_BankReco_Detail.Document_No=x.DocNo and tspl_BankReco_Detail.Reconciliation_Status='C') as ClearRecoNo" + Environment.NewLine + _
            ",(select count(tspl_BankReco_Detail.Reconciliation_Id) from tspl_BankReco_Detail where tspl_BankReco_Detail.Document_No=x.DocNo and tspl_BankReco_Detail.Reconciliation_Status<>'C') as OutstandingReconNo " + Environment.NewLine + _
            ",case when x.TransactionType='P' then (select top 1 Payment_Type  from TSPL_PAYMENT_HEADER where Payment_Type in ('AD') and Applied_Payment=x.DocNo ) " + Environment.NewLine + _
            "	else case when x.TransactionType='R' then (select  top 1 Receipt_No from TSPL_RECEIPT_HEADER where Receipt_Type in ('A') and Applied_Receipt=x.DocNo) else '' end end as ApplyDocumentCreated" + Environment.NewLine + _
            "from (" + Environment.NewLine + _
            "select BBTemp.DocNo,BBTemp.NewBankCode,case when TSPL_PAYMENT_HEADER.Payment_No=BBTemp.DocNo then 'P' else case when TSPL_RECEIPT_HEADER.Receipt_No=BBTemp.DocNo then 'R' else null end end as TransactionType" + Environment.NewLine + _
            ",TSPL_JOURNAL_MASTER.Voucher_No" + Environment.NewLine + _
            ",case when TSPL_PAYMENT_HEADER.Payment_No=BBTemp.DocNo then case when TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') then 1 else 0 end else case when TSPL_RECEIPT_HEADER.Receipt_No=BBTemp.DocNo then case when TSPL_RECEIPT_HEADER.Receipt_Type in ('A','O','F') then 1 else 0 end  else null end end as ISValidTransactionType  " + Environment.NewLine + _
            ",case when TSPL_PAYMENT_HEADER.Payment_No=BBTemp.DocNo then TSPL_PAYMENT_HEADER.Bank_Code else case when TSPL_RECEIPT_HEADER.Receipt_No=BBTemp.DocNo then TSPL_RECEIPT_HEADER.Bank_Code else null end end as Bank_Code" + Environment.NewLine + _
            "from (select DocumentNo as DocNo,BankCode as NewBankCode from TEMP_DELETED_RP_BANK_CHANGE_FOR_EXCEL where DocumentNo not in (select DocumentNo from TEMP_CREATED_RP_BANK_CHANGE_FOR_EXCEL)) as BBTemp" + Environment.NewLine + _
            "left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=BBTemp.DocNo" + Environment.NewLine + _
            "left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=BBTemp.DocNo" + Environment.NewLine + _
            "left outer join TSPL_JOURNAL_MASTER  on TSPL_JOURNAL_MASTER.Source_Doc_No=BBTemp.DocNo " + Environment.NewLine + _
            ") x " + Environment.NewLine + _
            "left outer join TSPL_BANK_REVERSE on TSPL_BANK_REVERSE.Document_No=x.DocNo " + Environment.NewLine + _
            "where x.TransactionType in ('P','R') " + Environment.NewLine + _
            " ) xx    " + Environment.NewLine + _
            "left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.Bank_Code=xx.Bank_Code"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strErro As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow(Me, "Do you want to change Bank Code of " + clsCommon.myCstr(dt.Rows.Count) + " Transaction(s)", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strDocNo As String = clsCommon.myCstr(dt.Rows(ii)("DocNo"))
                        Dim strVoucherNo As String = clsCommon.myCstr(dt.Rows(ii)("Voucher_No"))
                        Dim strNewBankCode As String = clsCommon.myCstr(dt.Rows(ii)("NewBankCode"))
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try

                            If clsCommon.myLen(dt.Rows(ii)("Reverse_Code")) > 0 Then
                                Throw New Exception("Bank reverse: " + clsCommon.myCstr(dt.Rows(ii)("Reverse_Code")) + " Document created ")
                            End If
                            If clsCommon.myLen(dt.Rows(ii)("ClearRecoNo")) > 0 Then
                                Throw New Exception("Clear Bank Reco found : " + clsCommon.myCstr(dt.Rows(ii)("ClearRecoNo")) + "")
                            End If
                            If clsCommon.myLen(dt.Rows(ii)("ApplyDocumentCreated")) > 0 Then
                                Throw New Exception("Apply Document created : " + clsCommon.myCstr(dt.Rows(ii)("ApplyDocumentCreated")) + "")
                            End If
                            If clsCommon.myCdbl(dt.Rows(ii)("ISValidTransactionType")) <= 0 Then
                                Throw New Exception("Document Type should be Advance/On Account/Refund(Receipt)")
                            End If

                            If clsCommon.myLen(strNewBankCode) <= 0 Then
                                Throw New Exception("Please select New Bank Code")
                            End If
                            Dim strNewBankType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_BANK_MASTER.Bank_type  from TSPL_BANK_MASTER where BANK_CODE='" + strNewBankCode + "'", trans))
                            If clsCommon.myLen(strNewBankType) <= 0 Then
                                Throw New Exception("Invalid Bank Type of " + strNewBankCode)
                            End If

                            'If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("Bank_Type")), strNewBankType) = CompairStringResult.Equal Then
                            '    Throw New Exception("Transaction Bank Type:" + clsCommon.myCstr(dt.Rows(ii)("Bank_Type")) + " and New Bank Type " + strNewBankType)
                            'End If

                            EnableDisableFinancialTrigger(False, trans)
                            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + strNewBankCode + "'", trans)

                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("TransactionType")), "P") = CompairStringResult.Equal Then
                                qry = "update  TSPL_PAYMENT_HEADER set Bank_Code='" + strNewBankCode + "'  where Payment_No='" + strDocNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                Dim objPay As clsPaymentHeader = clsPaymentHeader.GetData(strDocNo, NavigatorType.Current, trans)
                                clsPaymentHeader.CreateJournalEntry(objPay, "MPayable", strVoucherNo, trans)
                                clsBankReco.SetOutstandingEntry(strDocNo, objPay.Payment_Date, "Payment", trans, False)
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("TransactionType")), "R") = CompairStringResult.Equal Then
                                qry = "Update TSPL_RECEIPT_HEADER set Bank_Code='" + strNewBankCode + "'  where Receipt_No='" + strDocNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = clsRcptEntryHeader.GetQuery(strDocNo)
                                Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                                'clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "Receipt Entry", LocSegmentCode, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Date")), trans)
                                clsRcptEntryHeader.CreateJournalEntry(False, dtReceipt, strDocNo, LocSegmentCode, trans, strVoucherNo)
                                clsBankReco.SetOutstandingEntry(strDocNo, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Date")), "Receipt", trans, False)
                            End If
                            clsDBFuncationality.ExecuteNonQuery("Insert into TEMP_CREATED_RP_BANK_CHANGE_FOR_EXCEL values('" & strDocNo & "','" & strNewBankCode & "')", trans)
                            EnableDisableFinancialTrigger(True, trans)
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            strErro += "Change Bank Code Document No- " + strDocNo + " Exception -" + ex.Message + Environment.NewLine
                        End Try
                        clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, "Change Bank Code " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.myLen(strErro) > 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, strErro, Me.Text)
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Task completed", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '' update bank code in Payment /receipt code ends here 

    Private Sub FrmBankUpdateUploader_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.F1 Then
            RadPageView1.Pages("RadPageViewPage1").Item.Visibility = ElementVisibility.Visible
            GroupBox52.Visible = True
            RadPageView1.SelectedPage = RadPageViewPage1
        End If
    End Sub

    Private Sub FrmBankUpdateUploader_Load(sender As Object, e As EventArgs) Handles Me.Load
        RadPageView1.Pages("RadPageViewPage1").Item.Visibility = ElementVisibility.Collapsed
        GroupBox52.Visible = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    '' DELETION OF PAYMENT ENTRY AFTER POSTING
    Private Sub RadButton119_Click(sender As Object, e As EventArgs) Handles RadButton119.Click
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CREATED_PAYMENT_ENTRY")
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_PAYMENT_ENTRY")
    End Sub

    Private Sub RadButton120_Click(sender As Object, e As EventArgs) Handles RadButton120.Click
        Try
            Dim qry As String = "select TSPL_PAYMENT_HEADER.Posted,TSPL_JOURNAL_MASTER.Source_Code,TSPL_PAYMENT_HEADER.Payment_No,TSPL_PAYMENT_HEADER.Payment_Date ,TSPL_PAYMENT_HEADER.Payment_Type ,TSPL_PAYMENT_HEADER.Vendor_Code ,TSPL_PAYMENT_HEADER.Vendor_Name ,TSPL_PAYMENT_HEADER.Payment_Amount ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_PAYMENT_HEADER.IsApplyDocAuto from TSPL_PAYMENT_HEADER   left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_PAYMENT_HEADER.Payment_No where isnull(TSPL_PAYMENT_HEADER.Posted,'0')='1' and isnull(TSPL_PAYMENT_HEADER.isChkReverse,'')='N'"
            Dim arr As ArrayList = clsCommon.ShowMultipleSelectForm("PymentUtiDel", qry, "Payment_No", "", Nothing, Nothing)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_PAYMENT_ENTRY")
                qry = "insert into TEMP_DELETED_PAYMENT_ENTRY "
                qry += "select TSPL_PAYMENT_HEADER.Payment_No  from TSPL_PAYMENT_HEADER   left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_PAYMENT_HEADER.Payment_No where isnull(TSPL_PAYMENT_HEADER.Posted,'0')='1' and TSPL_PAYMENT_HEADER.Payment_No in(" + clsCommon.GetMulcallString(arr) + ") and isnull(TSPL_PAYMENT_HEADER.isChkReverse,'')='N'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton121_Click(sender As Object, e As EventArgs) Handles RadButton121.Click
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            Dim flag As Boolean = False
            qry = "select * from TEMP_DELETED_PAYMENT_ENTRY where Doc_No not in (select Doc_No from TEMP_CREATED_PAYMENT_ENTRY)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strErro As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow(Me, "Delete Payment Entry of " + clsCommon.myCstr(dt.Rows.Count) + " Payments.", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCommon.ProgressBarPercentShow()
                    Dim oldValueSkipLockLocation As String = clsFixedParameter.GetData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, Nothing)
                    Dim obj As clsPaymentHeader
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strDocNo As String = clsCommon.myCstr(dt.Rows(ii)("Doc_No"))
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        EnableDisableFinancialTrigger(False, trans)
                        clsFixedParameter.UpdateData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, 1, trans)
                        Try
                            obj = clsPaymentHeader.GetData(strDocNo, NavigatorType.Current, trans)
                            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Payment_No) > 0) Then
                                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans)
                                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "Payment Entry", LocSegmentCode, clsCommon.myCDate(obj.Payment_Date), trans)
                                clsPaymentHeader.ReverseAndUnpost(strDocNo, trans, True)
                                clsPaymentHeader.fundelete(clsCommon.myCstr(obj.Payment_Type), strDocNo, clsCommon.myCstr(obj.Vendor_Code), trans)
                                clsDBFuncationality.ExecuteNonQuery("Insert into TEMP_CREATED_PAYMENT_ENTRY values('" + strDocNo + "')", trans)
                                flag = True
                            End If
                            EnableDisableFinancialTrigger(True, trans)
                            clsFixedParameter.UpdateData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, oldValueSkipLockLocation, trans)
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            strErro += "Payment No - " + strDocNo + " Exception -" + ex.Message + Environment.NewLine
                        End Try
                        clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, "Delete Payment Entry " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.myLen(strErro) > 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, strErro, Me.Text)
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Task completed", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton118_Click(sender As Object, e As EventArgs) Handles RadButton118.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "DocNo") Then
            Try
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_PAYMENT_ENTRY")
            Catch ex As Exception
            End Try
            Try
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CREATED_PAYMENT_ENTRY")
            Catch ex As Exception
            End Try
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strDocNo As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If clsCommon.myLen(strDocNo) > 0 Then
                        Dim Qry As String = "INSERT Into TEMP_DELETED_PAYMENT_ENTRY (Doc_No) values('" + strDocNo + "')"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub

    '' deletion of receipt entry after posting
    Private Sub RadButton111_Click(sender As Object, e As EventArgs) Handles RadButton111.Click
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CREATED_RECEIPT_ENTRY")
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_RECEIPT_ENTRY")
    End Sub

    Private Sub RadButton113_Click(sender As Object, e As EventArgs) Handles RadButton113.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "DocNo") Then
            Try
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_RECEIPT_ENTRY")
            Catch ex As Exception
            End Try
            Try
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CREATED_RECEIPT_ENTRY")
            Catch ex As Exception
            End Try
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strDocNo As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If clsCommon.myLen(strDocNo) > 0 Then
                        Dim Qry As String = "INSERT Into TEMP_DELETED_RECEIPT_ENTRY (Doc_No) values('" + strDocNo + "')"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub

    Private Sub RadButton112_Click(sender As Object, e As EventArgs) Handles RadButton112.Click
        Try
            Dim qry As String = "select TSPL_RECEIPT_HEADER.Receipt_No,TSPL_RECEIPT_HEADER.Receipt_Date,TSPL_RECEIPT_HEADER.Receipt_Type,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_RECEIPT_HEADER.Customer_Name,TSPL_RECEIPT_HEADER.Receipt_Amount,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_RECEIPT_HEADER.IsApplyDocAuto from TSPL_RECEIPT_HEADER   left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_JOURNAL_MASTER.Source_Code in ('AR-DC','AR-MI','AR-OA','AR-PI','AR-PY','AR-RF') where TSPL_RECEIPT_HEADER.Posted='Y' and isnull(TSPL_RECEIPT_HEADER.isChkReverse,'')='N'"
            Dim arr As ArrayList = clsCommon.ShowMultipleSelectForm("ReceiptUtiDel", qry, "Receipt_No", "", Nothing, Nothing)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_RECEIPT_ENTRY")
                qry = "insert into TEMP_DELETED_RECEIPT_ENTRY "
                qry += "select TSPL_RECEIPT_HEADER.Receipt_No  from TSPL_RECEIPT_HEADER   left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_JOURNAL_MASTER.Source_Code in ('AR-DC','AR-MI','AR-OA','AR-PI','AR-PY','AR-RF') where TSPL_RECEIPT_HEADER.Posted='Y' and TSPL_RECEIPT_HEADER.Receipt_No in(" + clsCommon.GetMulcallString(arr) + ") and isnull(TSPL_RECEIPT_HEADER.isChkReverse,'')='N'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton114_Click(sender As Object, e As EventArgs) Handles RadButton114.Click
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            qry = "select * from TEMP_DELETED_RECEIPT_ENTRY where Doc_No not in (select Doc_No from TEMP_CREATED_RECEIPT_ENTRY)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strErro As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow(Me, "Delete Receipt Entry of " + clsCommon.myCstr(dt.Rows.Count) + " Receipts", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCommon.ProgressBarPercentShow()
                    Dim oldValueSkipLockLocation As String = clsFixedParameter.GetData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, Nothing)
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strDocNo As String = clsCommon.myCstr(dt.Rows(ii)("Doc_No"))
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        EnableDisableFinancialTrigger(False, trans)
                        clsFixedParameter.UpdateData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, 1, trans)
                        Try
                            qry = clsRcptEntryHeader.GetQuery(strDocNo)
                            Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + clsCommon.myCstr(dtReceipt.Rows(0)("Bank_Code")) + "'", trans)
                            clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "Receipt Entry", LocSegmentCode, clsCommon.myCDate(dtReceipt.Rows(0)("Receipt_Date")), trans)

                            clsRcptEntryHeader.ReverseAndUnpost(strDocNo, trans)
                            clsRcptEntryHeader.fundelete(strDocNo, trans)
                            clsDBFuncationality.ExecuteNonQuery("Insert into TEMP_CREATED_RECEIPT_ENTRY values('" + strDocNo + "')", trans)
                            EnableDisableFinancialTrigger(True, trans)
                            clsFixedParameter.UpdateData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, oldValueSkipLockLocation, trans)
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            strErro += "Receipt No - " + strDocNo + " Exception -" + ex.Message + Environment.NewLine
                        End Try
                        clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, "Delete Receipt Entry " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.myLen(strErro) > 0 Then
                        common.clsCommon.MyMessageBoxShow(strErro, Me.Text)
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Task completed", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class