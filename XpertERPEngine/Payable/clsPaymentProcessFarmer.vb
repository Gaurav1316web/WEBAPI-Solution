'==============BM00000007337===============================
Imports common
Imports System.Data.SqlClient

Public Class clsPaymentProcessFarmerHead
#Region "Variales"
    Public Doc_No As String = ""
    Public Doc_Date As String = ""
    Public From_Date As String = ""
    Public To_Date As String = ""
    Public Loc_Seg_Code As String = ""
    Public Created_By As String = ""
    Public Created_Date As String = ""
    Public Modified_By As String = ""
    Public Modified_Date As String = ""
    Public Comp_Code As String = ""
    Public DocRefNoForUploader As String = ""
    Public ArrVSPPPDetail As List(Of clsPaymentProcessDetail) = Nothing
    Public ArrPPDetail As List(Of clsPaymentProcessFarmerPaymentDetail) = Nothing
    Public arrclsPaymentProcessFarmerInvoices As List(Of clsPaymentProcessFarmerInvoices) = Nothing
    Public arrClsPaymentProcessMccSale As List(Of clsPaymentProcessMCCSale) = Nothing
    Public arrClsPaymentProcessMccSaleReturn As List(Of clsPaymentProcessMCCSaleReturn) = Nothing
    Public arrclsPaymentProcessFarmerMccSale As List(Of clsPaymentProcessFarmerMCCSale) = Nothing
    Public arrclsPaymentProcessFarmerMccSaleReturn As List(Of clsPaymentProcessFarmerMCCSaleReturn) = Nothing
    Public arrclsPaymentProcessFarmerItemIssue As List(Of clsPaymentProcessFarmerItemIssue) = Nothing
    Public arrclsPaymentProcessFarmerItemIssueReturn As List(Of clsPaymentProcessFarmerItemIssueReturn) = Nothing
    Public arrclsPaymentProcessFarmerDeductions As List(Of clsPaymentProcessFarmerDeduction) = Nothing
    Public arrclsPaymentProcessFarmerCreditNote As List(Of clsPaymentProcessFarmerCreditNote) = Nothing
    Public ArrPPAdvancePayment As List(Of clsPaymentProcessFarmerAdvancePayment) = Nothing
    Public ArrPPAdjustment As List(Of clsPaymentProcessAdjustment) = Nothing
    Public arrMPAdvance As List(Of clsPaymentProcessFarmerMPAdvance) = Nothing
    Public isPosted As Integer = 0
    Dim Posting_Date As String = ""
    Public PaymentDesc As String = ""
    Public FarmType As String = "PPF"
    Private Property arrClsPaymentProcessItemIssueReturn As Object
#End Region

    Public Shared Function SaveData(ByVal obj As clsPaymentProcessFarmerHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans, isNewEntry)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ProcessData(ByVal DocNo As String, ByVal Desc As String) As Boolean
        Dim obj As clsPaymentProcessFarmerHead = clsPaymentProcessFarmerHead.getData(DocNo, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim i As Integer = 0
        Dim Counter As Integer = 0
        Dim AdjAmt As Double = 0
        Dim ReturnAdjAmt As Double = 0

        Dim todaydt As Date = clsCommon.GETSERVERDATE(trans)
        Dim objRcpt As clsAdjustmentEntryReceivables = Nothing
        Dim objPayAdj As clsPaymentAdjustmentEntry = Nothing
        Dim DisCCodeForArAdj As String = ""
        Dim GLAcARAdj As String = ""
        Dim DiscDiscForArAdj As String = ""
        Dim GLAcDescARAdj As String = ""
        Dim objPay As clsPaymentHeader = Nothing
        Dim objTr As New clsPaymentDetail()
        'Dim dtDed As DataTable = Nothing
        Try
            clsCommon.ProgressBarPercentShow()
            DisCCodeForArAdj = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForArAdj, clsFixedParameterCode.DiscountCodeForArAdj, trans)
            If clsCommon.myLen(DisCCodeForArAdj) <= 0 Then
                Throw New Exception("Please Map Discount code from Sale setting")
            End If
            DiscDiscForArAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))
            GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Code from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans)), obj.Loc_Seg_Code, True, trans)
            GLAcDescARAdj = clsGLAccount.GetName(GLAcARAdj, trans)
            '' process vsp payment
            If obj.ArrPPDetail IsNot Nothing And obj.ArrPPDetail.Count > 0 Then
                If obj.arrClsPaymentProcessMccSale IsNot Nothing AndAlso obj.arrClsPaymentProcessMccSale.Count > 0 Then
                    For i = 0 To obj.arrClsPaymentProcessMccSale.Count - 1
                        clsCommon.ProgressBarPercentUpdate(i * 100 / obj.arrClsPaymentProcessMccSale.Count, "Updating AR Adjustment Record " & (i + 1) & " Of " & obj.arrClsPaymentProcessMccSale.Count)
                        objRcpt = New clsAdjustmentEntryReceivables
                        objRcpt.Adjustment_No = ""
                        objRcpt.Description = " Adjustment Against Bulk Payment Process "
                        objRcpt.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                        objRcpt.Customer_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cust_Code  from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" & obj.arrClsPaymentProcessMccSale(i).Customer_CODE & "' ", trans))
                        objRcpt.Customer_Name = clsCommon.myCstr(clsCustomerMaster.GetName(objRcpt.Customer_No, trans))
                        objRcpt.Doc_No = clsCommon.myCstr(obj.arrClsPaymentProcessMccSale(i).Sale_Doc_No)
                        Dim ReturnAmt As Double = 0
                        For ireturn As Integer = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                            If clsCommon.myCstr(obj.arrClsPaymentProcessMccSale(i).Sale_Doc_No) = clsCommon.myCstr(obj.arrClsPaymentProcessMccSaleReturn(ireturn).Sale_Doc_No) Then
                                ReturnAmt += clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(ireturn).Amount)
                            End If
                        Next
                        objRcpt.ARInvoiceNo = clsCommon.myCstr(obj.arrClsPaymentProcessMccSale(i).AR_Invoice_No)
                        objRcpt.Doc_Amount = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale(i).Amount) - ReturnAmt
                        objRcpt.Remarks = ""
                        objRcpt.Adjustment_Amount = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale(i).Amount - obj.arrClsPaymentProcessMccSale(i).Reduce_Deduc_Amt) - ReturnAmt
                        objRcpt.Arr = New List(Of clsAdjustmentEntryReceivablesDetail)
                        Dim objTrRcpt As New clsAdjustmentEntryReceivablesDetail()
                        objTrRcpt.Discount_Code = DisCCodeForArAdj
                        objTrRcpt.Discount_Description = DiscDiscForArAdj
                        objTrRcpt.Account_No = GLAcARAdj
                        objTrRcpt.Account_Description = GLAcDescARAdj
                        objTrRcpt.Amount = clsCommon.myCdbl(clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale(i).Amount - obj.arrClsPaymentProcessMccSale(i).Reduce_Deduc_Amt - ReturnAmt))
                        objTrRcpt.Remarks = ""
                        objRcpt.Arr.Add(objTrRcpt)
                        If clsCommon.myCdbl(objTrRcpt.Amount) > 0 Then
                            objRcpt.SaveData(objRcpt, True, trans)
                            clsAdjustmentEntryReceivables.FunPost(objRcpt.Adjustment_No, trans)
                        End If
                    Next
                End If

                For i = 0 To obj.ArrVSPPPDetail.Count - 1
                    clsCommon.ProgressBarPercentUpdate(i * 100 / obj.ArrVSPPPDetail.Count, " Creating Payment Entry  Record " & (i + 1) & " Of " & obj.ArrVSPPPDetail.Count)
                    If Not obj.ArrVSPPPDetail(i).Is_select Then
                        Continue For
                    End If
                    ''Becuause of Knock off amount will always be create.
                    CreateApplyDocumentForAdavancePayment(clsCommon.myCDate(obj.Doc_Date), obj.ArrPPAdvancePayment, obj.ArrVSPPPDetail(i), obj.Loc_Seg_Code, trans)
                    ''Comment by balwinder on 19/02/2021
                    'If obj.ArrVSPPPDetail(i).Deduction_Amount > 0 Then
                    '    '' create adjustment entry
                    '    objPayAdj = New clsPaymentAdjustmentEntry
                    '    objPayAdj.Adjustment_No = ""
                    '    objPayAdj.Description = " AP Return Adjustment Against Bulk Payment Process for extra amount to be paid by VSP"
                    '    objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.To_Date)
                    '    objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                    '    objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                    '    objPayAdj.Doc_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).AP_Invoice_No)
                    '    objPayAdj.Doc_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Total_Invoice_Amount)
                    '    objPayAdj.Remarks = clsCommon.myCstr("")
                    '    objPayAdj.Adjust_Type = "R"
                    '    objPayAdj.Adjustment_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Deduction_Amount)
                    '    objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                    '    Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                    '    objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                    '    objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                    '    'objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                    '    'objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)

                    '    dtDed = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
                    '    If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                    '        Throw New Exception("Please set default VSP deduction code")
                    '    End If
                    '    If clsCommon.myLen(dtDed.Rows(0)("GL_Account_Code")) <= 0 Then
                    '        Throw New Exception("Please set gl Account for Deduction Code :" + clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code")))
                    '    End If
                    '    objTrPay.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code")), obj.Loc_Seg_Code, True, trans)
                    '    objTrPay.Account_Description = clsGLAccount.GetName(objTrPay.Account_No, trans)

                    '    objTrPay.Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Deduction_Amount)
                    '    objTrPay.Remarks = ""
                    '    objPayAdj.Arr.Add(objTrPay)
                    '    objPayAdj.SaveData(objPayAdj, True, trans)
                    '    clsPaymentAdjustmentEntry.FunPostReverseEntry(objPayAdj.Adjustment_No, trans)
                    'End If


                    If obj.ArrVSPPPDetail(i).NextCycleDebitNote > 0 Then
                        'createDebitNoteNextPaymentCycle(obj.ArrVSPPPDetail(i), obj.Loc_Seg_Code, trans) ''Comment by balwinder on 19/02/2021 
                    End If

                    If obj.ArrVSPPPDetail(i).MP_Total_Deduction > 0 Then
                        createCreditNoteFarmerDeduction(obj.ArrVSPPPDetail(i), obj.Loc_Seg_Code, trans)
                    End If

                    ReturnAdjAmt = 0
                    AdjAmt = 0
                    For Counter = 0 To obj.arrClsPaymentProcessMccSale.Count - 1
                        If clsCommon.CompairString(obj.arrClsPaymentProcessMccSale(Counter).Customer_CODE, obj.ArrVSPPPDetail(i).VSP_CODE) = CompairStringResult.Equal Then
                            AdjAmt = AdjAmt + (obj.arrClsPaymentProcessMccSale(Counter).Amount - obj.arrClsPaymentProcessMccSale(Counter).Reduce_Deduc_Amt)
                            Dim ReturnAmt As Double = 0
                            For ireturn As Integer = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                                If clsCommon.myCstr(obj.arrClsPaymentProcessMccSale(Counter).Sale_Doc_No) = clsCommon.myCstr(obj.arrClsPaymentProcessMccSaleReturn(ireturn).Sale_Doc_No) Then
                                    ReturnAmt += clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(ireturn).Amount)
                                    obj.arrClsPaymentProcessMccSaleReturn(ireturn).Amount = 0
                                End If
                            Next
                            AdjAmt -= ReturnAmt
                        End If
                    Next

                    If AdjAmt > 0 Then
                        objPayAdj = New clsPaymentAdjustmentEntry
                        objPayAdj.Adjustment_No = ""
                        objPayAdj.Description = " AP Adjustment Against Bulk Payment Process "
                        objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                        objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                        objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                        objPayAdj.Doc_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).AP_Invoice_No)
                        objPayAdj.Doc_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Total_Invoice_Amount)
                        objPayAdj.Remarks = clsCommon.myCstr("")
                        objPayAdj.Adjustment_Amount = clsCommon.myCdbl(AdjAmt)
                        objPayAdj.Adjust_Type = "P"
                        objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                        Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                        objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                        objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                        objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                        objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                        objTrPay.Amount = clsCommon.myCdbl(AdjAmt)
                        objTrPay.Remarks = ""
                        objPayAdj.Arr.Add(objTrPay)
                        objPayAdj.SaveData(objPayAdj, True, trans)
                        clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                    ElseIf AdjAmt < 0 Then
                        Dim CreditAjust As Decimal = Math.Abs(AdjAmt)
                        objPayAdj = New clsPaymentAdjustmentEntry
                        objPayAdj.Adjustment_No = ""
                        objPayAdj.Description = " AP Adjustment Against Credt note in Bulk Payment Process "
                        objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                        objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                        objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                        objPayAdj.Doc_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).AP_Invoice_No)
                        objPayAdj.Doc_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Total_Invoice_Amount)
                        objPayAdj.Remarks = clsCommon.myCstr("")
                        objPayAdj.Adjustment_Amount = clsCommon.myCdbl(CreditAjust)
                        objPayAdj.Adjust_Type = "R"
                        objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                        Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                        objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                        objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                        objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                        objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                        objTrPay.Amount = clsCommon.myCdbl(CreditAjust)
                        objTrPay.Remarks = ""
                        objPayAdj.Arr.Add(objTrPay)
                        objPayAdj.SaveData(objPayAdj, True, trans)
                        clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                    End If
                    For Counter = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                        If clsCommon.CompairString(obj.arrClsPaymentProcessMccSaleReturn(Counter).Customer_CODE, obj.ArrVSPPPDetail(i).VSP_CODE) = CompairStringResult.Equal Then
                            ReturnAdjAmt = ReturnAdjAmt + (obj.arrClsPaymentProcessMccSaleReturn(Counter).Amount)
                        End If
                    Next
                    If ReturnAdjAmt > 0 Then
                        objPayAdj = New clsPaymentAdjustmentEntry
                        objPayAdj.Adjustment_No = ""
                        objPayAdj.Description = " AP Return Adjustment Against Bulk Payment Process "
                        objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                        objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                        objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                        objPayAdj.Doc_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).AP_Invoice_No)
                        objPayAdj.Doc_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Total_Invoice_Amount)
                        objPayAdj.Remarks = clsCommon.myCstr("")
                        objPayAdj.Adjust_Type = "R"
                        objPayAdj.Adjustment_Amount = clsCommon.myCdbl(ReturnAdjAmt)
                        objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                        Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                        objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                        objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                        objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                        objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                        objTrPay.Amount = clsCommon.myCdbl(ReturnAdjAmt)
                        objTrPay.Remarks = clsCommon.myCstr("")
                        objPayAdj.Arr.Add(objTrPay)
                        objPayAdj.SaveData(objPayAdj, True, trans)
                        clsPaymentAdjustmentEntry.FunPostReverseEntry(objPayAdj.Adjustment_No, trans)
                    End If

                    If obj.ArrVSPPPDetail(i).is_Hold_Payment_Process Then
                        Dim XTotalAmount As Decimal = 0
                        For Counter = 0 To obj.arrclsPaymentProcessFarmerDeductions.Count - 1
                            If clsCommon.CompairString(obj.arrclsPaymentProcessFarmerDeductions(Counter).Vendor_CODE, obj.ArrVSPPPDetail(i).VSP_CODE) = CompairStringResult.Equal Then
                                Dim XAmount As Decimal = obj.arrclsPaymentProcessFarmerDeductions(Counter).Amount - obj.arrclsPaymentProcessFarmerDeductions(Counter).Reduce_Deduc_Amt
                                If XAmount > 0 Then
                                    XTotalAmount += XAmount
                                    objPayAdj = New clsPaymentAdjustmentEntry
                                    objPayAdj.Adjustment_No = "" ''To Be Generated
                                    objPayAdj.Description = "AP Debit Note Adjustment Against Hold Process"
                                    objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                                    objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                                    objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                                    objPayAdj.Doc_No = clsCommon.myCstr(obj.arrclsPaymentProcessFarmerDeductions(Counter).AP_Invoice_No)
                                    objPayAdj.Doc_Amount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Document_Total from TSPL_Vendor_Invoice_Head where Document_No='" + obj.arrclsPaymentProcessFarmerDeductions(Counter).AP_Invoice_No + "'", trans))
                                    objPayAdj.Remarks = clsCommon.myCstr("")
                                    objPayAdj.Adjustment_Amount = XAmount
                                    objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                                    Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                                    objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                                    objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                                    objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                                    objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                                    objTrPay.Amount = XAmount
                                    objTrPay.Remarks = clsCommon.myCstr("")
                                    objPayAdj.Arr.Add(objTrPay)
                                    objPayAdj.SaveData(objPayAdj, True, trans)
                                    clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                                End If
                            End If
                        Next
                        If XTotalAmount > 0 Then
                            objPayAdj = New clsPaymentAdjustmentEntry
                            objPayAdj.Adjustment_No = "" ''To Be Generated
                            objPayAdj.Description = "AP Invoice Adjustment Against Hold Process"
                            objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                            objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                            objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                            objPayAdj.Doc_No = obj.ArrVSPPPDetail(i).AP_Invoice_No
                            objPayAdj.Doc_Amount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Document_Total from TSPL_Vendor_Invoice_Head where Document_No='" + obj.ArrPPDetail(i).AP_Invoice_No + "'", trans))
                            objPayAdj.Remarks = clsCommon.myCstr("")
                            objPayAdj.Adjustment_Amount = XTotalAmount
                            objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                            Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                            objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                            objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                            objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                            objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                            objTrPay.Amount = XTotalAmount
                            objTrPay.Remarks = clsCommon.myCstr("")
                            objPayAdj.Arr.Add(objTrPay)
                            objPayAdj.SaveData(objPayAdj, True, trans)
                            clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                        End If
                        Continue For
                    End If


                    Dim ARNote As String = String.Empty

                    Dim objVendInv As New clsVedorInvoiceHead()
                    objVendInv.Invoice_Entry_Date = clsCommon.myCDate(obj.To_Date)

                    objVendInv.Document_Type = "D"
                    ARNote = "Debit Note Against " + DocNo

                    objVendInv.RefDocNo = DocNo
                    objVendInv.RefDocType = "Farmer DN"
                    objVendInv.loc_code = obj.Loc_Seg_Code
                    'objVendInv.Document_Total = clsCommon.myCdbl(obj.ArrVSPPPDetail.Item(i).Total_Invoice_Amount - obj.ArrVSPPPDetail.Item(i).FarmerPayment - AdjAmt + ReturnAdjAmt + clsCommon.myCdbl(obj.ArrVSPPPDetail(i).NextCycleDebitNote) - clsCommon.myCdbl(obj.ArrVSPPPDetail(i).PrevCycleDebitNote))
                    objVendInv.Document_Total = clsCommon.myCdbl(obj.ArrVSPPPDetail.Item(i).Total_Invoice_Amount - obj.ArrVSPPPDetail.Item(i).FarmerPayment)  ''Remove adj and return adjustment amount by balwinder on 19/02/2021
                    objVendInv.Vendor_Code = obj.ArrVSPPPDetail.Item(i).VSP_CODE

                    objVendInv.Invoice_Type = "AP"
                    objVendInv.Vendor_Name = obj.ArrVSPPPDetail.Item(i).VSP_NAME
                    objVendInv.Posting_Date = objVendInv.Invoice_Entry_Date
                    objVendInv.Vendor_Invoice_Date = objVendInv.Invoice_Entry_Date
                    objVendInv.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  ISNULL(Vendor_Account,'') AS [Vendor_Account] FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" + objVendInv.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendInv.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor account set for vendor : " + objVendInv.Vendor_Code)
                    End If

                    objVendInv.Vendor_Control_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payable_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans))
                    objVendInv.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Vendor_Control_AC, objVendInv.loc_code, True, trans)

                    If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If

                    objVendInv.On_Hold = 0
                    objVendInv.Remarks = "Auto Generated Debit Note Against VSP " & obj.ArrVSPPPDetail.Item(i).VSP_CODE & " and Farmer PP No. " & DocNo & ""
                    objVendInv.Description = "Auto Generated Debit Note Against VSP " & obj.ArrVSPPPDetail.Item(i).VSP_CODE & " and Farmer PP No. " & DocNo & ""
                    objVendInv.Balance_Amt = objVendInv.Document_Total
                    objVendInv.Amount_Less_Discount = objVendInv.Document_Total
                    objVendInv.Discount_Base = objVendInv.Document_Total
                    '=========================================================

                    objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

                    '' Detail Level Saving

                    Dim VendAccSet As String = String.Empty


                    Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()

                    objVendInvTR.Detail_Line_No = 1
                    objVendInvTR.Amount = objVendInv.Document_Total
                    objVendInvTR.Amount_less_Discount = objVendInv.Document_Total
                    objVendInvTR.Total_Amount = objVendInv.Document_Total
                    objVendInvTR.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Profit_Loss_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans))
                    objVendInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInvTR.GL_Account_Code, objVendInv.loc_code, True, trans)
                    If clsCommon.myLen(objVendInvTR.GL_Account_Code) <= 0 Then
                        Throw New Exception("Please set the vendor Profit Loss Account")
                    End If
                    objVendInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & objVendInvTR.GL_Account_Code & "'", trans))
                    objVendInv.Arr.Add(objVendInvTR)
                    If objVendInv.Document_Total > 0 Then
                        objVendInv.SaveData(objVendInv, True, trans)
                        clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
                    End If
                    '' end code
                Next


                '==============Add Code for save Mcc Sale Return Document Payment Details==================
                If obj.arrClsPaymentProcessMccSaleReturn IsNot Nothing AndAlso obj.arrClsPaymentProcessMccSaleReturn.Count > 0 Then
                    DisCCodeForArAdj = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForArAdj, clsFixedParameterCode.DiscountCodeForArAdj, trans)
                    If clsCommon.myLen(DisCCodeForArAdj) <= 0 Then
                        Throw New Exception("Please Map Discount code from Sale setting")
                    End If
                    DiscDiscForArAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))
                    GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Code from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans)), obj.Loc_Seg_Code, True, trans)
                    GLAcDescARAdj = clsGLAccount.GetName(GLAcARAdj, trans)
                    'GLAcDescARAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))
                    For i = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                        clsCommon.ProgressBarPercentUpdate(i * 100 / obj.arrClsPaymentProcessMccSaleReturn.Count, "Updating AR Adjustment Record " & (i + 1) & " Of " & obj.arrClsPaymentProcessMccSaleReturn.Count)
                        objRcpt = New clsAdjustmentEntryReceivables
                        objRcpt.Adjustment_No = ""
                        objRcpt.Description = " Return Adjustment Against Bulk Payment Process "
                        objRcpt.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                        objRcpt.Customer_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cust_Code  from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" & obj.arrClsPaymentProcessMccSaleReturn(i).Customer_CODE & "' ", trans))
                        objRcpt.Customer_Name = clsCommon.myCstr(clsCustomerMaster.GetName(objRcpt.Customer_No, trans))
                        objRcpt.Doc_No = clsCommon.myCstr(obj.arrClsPaymentProcessMccSaleReturn(i).Sale_Doc_No)
                        objRcpt.ARInvoiceNo = clsCommon.myCstr(obj.arrClsPaymentProcessMccSaleReturn(i).AR_Invoice_No)
                        objRcpt.Doc_Amount = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(i).Amount)
                        objRcpt.Remarks = ""
                        objRcpt.Adjustment_Amount = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(i).Amount)
                        objRcpt.Arr = New List(Of clsAdjustmentEntryReceivablesDetail)
                        Dim objTrRcpt As New clsAdjustmentEntryReceivablesDetail()
                        objTrRcpt.Discount_Code = DisCCodeForArAdj
                        objTrRcpt.Discount_Description = DiscDiscForArAdj
                        objTrRcpt.Account_No = GLAcARAdj
                        objTrRcpt.Account_Description = GLAcDescARAdj
                        Dim ReturnAmt As Double = 0
                        objTrRcpt.Amount = clsCommon.myCdbl(clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(i).Amount))
                        objTrRcpt.Remarks = ""
                        objRcpt.Arr.Add(objTrRcpt)
                        If clsCommon.myCdbl(objTrRcpt.Amount) > 0 Then
                            objRcpt.SaveData(objRcpt, True, trans)
                            clsAdjustmentEntryReceivables.FunPostReverseEntry(objRcpt.Adjustment_No, trans)
                        End If
                    Next
                End If


                '' Farmer Payment 
                DisCCodeForArAdj = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForMPAdj, clsFixedParameterCode.DiscountCodeForMPAdj, trans)
                If clsCommon.myLen(DisCCodeForArAdj) <= 0 Then
                    Throw New Exception("Please Map Discount code for Farmer/MP in Fixed Parameter.")
                End If
                DiscDiscForArAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))
                GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Code from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans)), obj.Loc_Seg_Code, True, trans)
                GLAcDescARAdj = clsGLAccount.GetName(GLAcARAdj, trans)
                'GLAcDescARAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))


                ''Get Raw Item Inventory Control Account
                Dim strICode = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
                If clsCommon.myLen(strICode) <= 0 Then
                    Throw New Exception("Please select MCC Defualt Milk item")
                End If
                Dim qry As String = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit,TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account,TSPL_PURCHASE_ACCOUNTS.RM_Consumption  from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + strICode + " ")
                End If
                ''1)


                'dtDed = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_PRO_Data=1", trans)
                'If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                '    Throw New Exception("Please set default Pro Data of Deduction Master")
                'End If
                'If clsCommon.myLen(dtDed.Rows(0)("GL_Account_Code")) <= 0 Then
                '    Throw New Exception("Please set gl Account for Deduction Code :" + clsCommon.myCstr(dtDed.Rows(0)("code")))
                'End If
                'Dim strPRoData As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code")), obj.Loc_Seg_Code, True, trans)

                'Dim ArryDeductionGLAC As ArrayList = New ArrayList()
                Dim ArryIncentiveGLAC As ArrayList = New ArrayList()
                '' mp payment start here
                If obj.ArrPPDetail IsNot Nothing And obj.ArrPPDetail.Count > 0 Then
                    For Each objVSP As clsPaymentProcessDetail In obj.ArrVSPPPDetail
                        Dim objFAdj As New clsFarmerPaymentAdjustmentEntry
                        Dim objAdj As New clsPaymentAdjustmentEntry
                        For i = 0 To obj.ArrPPDetail.Count - 1
                            If clsCommon.CompairString(objVSP.AP_Invoice_No, obj.ArrPPDetail.Item(i).AP_Invoice_No) = CompairStringResult.Equal Then
                                '' save payment adjustment entry against vsp invoice
                                objAdj.Adjustment_No = obj.ArrPPDetail.Item(i).AP_Adjustment_No
                                clsCommon.ProgressBarPercentUpdate(i * 100 / obj.ArrPPDetail.Count, " Creating Payment Entry  Record " & (i + 1) & " Of " & obj.ArrPPDetail.Count)
                                If Not obj.ArrPPDetail(i).Is_select Then
                                    Continue For
                                End If

                                ''Create JE For Deduction,Incentive By Balwinder on 20/02/2020 
                                'If obj.ArrPPDetail(i).Deduction_Amount > 0 Then
                                '    Dim AccDr() As String = {GLAcARAdj, Math.Round(((obj.ArrPPDetail(i).Deduction_Amount)), 2, MidpointRounding.ToEven)}
                                '    ArryDeductionGLAC.Add(AccDr)

                                '    Dim AccCr() As String = {strPRoData, -1 * Math.Round(((obj.ArrPPDetail(i).Deduction_Amount)), 2, MidpointRounding.ToEven)}
                                '    ArryDeductionGLAC.Add(AccCr)
                                'End If

                                If obj.ArrPPDetail(i).Incentive_Amount > 0 Then
                                    qry = "select Incentive_Account from TSPL_MP_MASTER where MP_Code='" + obj.ArrPPDetail(i).Farmer_Code + "'"
                                    qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                                    If clsCommon.myLen(qry) <= 0 Then
                                        Throw New Exception("Please set Incentive Account form MP " + clsCommon.myCstr(obj.ArrPPDetail(i).Farmer_Code))
                                    End If
                                    qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Loc_Seg_Code, True, trans)
                                    Dim AccDr() As String = {qry, Math.Round(((obj.ArrPPDetail(i).Incentive_Amount)), 2, MidpointRounding.ToEven)}
                                    ArryIncentiveGLAC.Add(AccDr)

                                    Dim AccCr() As String = {GLAcARAdj, -1 * Math.Round(((obj.ArrPPDetail(i).Incentive_Amount)), 2, MidpointRounding.ToEven)}
                                    ArryIncentiveGLAC.Add(AccCr)
                                End If

                                ''End of Create JE For Deduction,Incentive

                                ReturnAdjAmt = 0
                                AdjAmt = 0
                                Dim MCCSaleDocs As String = ""
                                Dim MCCSaleRetDocs As String = ""
                                For Counter = 0 To obj.arrclsPaymentProcessFarmerMccSale.Count - 1
                                    If clsCommon.CompairString(obj.arrclsPaymentProcessFarmerMccSale(Counter).Farmer_Code, obj.ArrPPDetail(i).Farmer_Code) = CompairStringResult.Equal Then
                                        AdjAmt = AdjAmt + (obj.arrclsPaymentProcessFarmerMccSale(Counter).Amount - obj.arrclsPaymentProcessFarmerMccSale(Counter).Reduce_Deduc_Amt)
                                        If clsCommon.myLen(MCCSaleDocs) <= 0 Then
                                            MCCSaleDocs = obj.arrclsPaymentProcessFarmerMccSale(Counter).Sale_Doc_No
                                        Else
                                            MCCSaleDocs = MCCSaleDocs & "," & obj.arrclsPaymentProcessFarmerMccSale(Counter).Sale_Doc_No
                                        End If
                                        Dim ReturnAmt As Double = 0
                                        For ireturn As Integer = 0 To obj.arrclsPaymentProcessFarmerMccSaleReturn.Count - 1
                                            If clsCommon.myCstr(obj.arrclsPaymentProcessFarmerMccSale(Counter).Sale_Doc_No) = clsCommon.myCstr(obj.arrclsPaymentProcessFarmerMccSaleReturn(ireturn).Sale_Doc_No) Then
                                                ReturnAmt += clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerMccSaleReturn(ireturn).Amount)
                                                obj.arrclsPaymentProcessFarmerMccSaleReturn(ireturn).Amount = 0
                                            End If
                                        Next
                                        AdjAmt -= ReturnAmt
                                    End If
                                Next
                                '' commented by panch raj because no need of adjustment entry . farmer ledger reuired to sho mcc sale , returns and adjustents docs directly. Only payment doc will be created here
                                ' '' check for negative net amount
                                If obj.ArrPPDetail(i).NextCycleDebitNoteMP > 0 Then
                                    '' commented because extra entry was creatig
                                    ' '' create adjustment entry for next payment cycle
                                    Dim NextPCAdjustAmt As Decimal = obj.ArrPPDetail(i).NextCycleDebitNoteMP
                                    If NextPCAdjustAmt > 0 Then
                                        objFAdj = New clsFarmerPaymentAdjustmentEntry
                                        objFAdj.Adjustment_No = ""
                                        objFAdj.Description = "Adjstment amount for next payment cycle->Payment Process doc No: " & DocNo & ",Farmer Invoice No: " & obj.ArrPPDetail(i).Farmer_Invoice_No & " "
                                        objFAdj.Adjustment_Date = obj.ArrPPDetail(i).AP_Invoice_Date.AddDays(1)
                                        objFAdj.Farmer_Code = clsCommon.myCstr(obj.ArrPPDetail(i).Farmer_Code)
                                        objFAdj.Farmer_Name = clsCommon.myCstr(obj.ArrPPDetail(i).Farmer_Name)
                                        objFAdj.Doc_No = ""
                                        objFAdj.Doc_Amount = 0
                                        objFAdj.Remarks = "MCC Farmer sale Documents:" & MCCSaleDocs
                                        objFAdj.Adjustment_Amount = NextPCAdjustAmt
                                        objFAdj.Adjustment_Type = "Payment"
                                        objFAdj.Arr = New List(Of clsFarmerPaymentAdjustmentEntryDetail)
                                        Dim objTrPay As New clsFarmerPaymentAdjustmentEntryDetail()
                                        objTrPay.Discount_Code = DisCCodeForArAdj
                                        objTrPay.Discount_Description = DiscDiscForArAdj
                                        objTrPay.Account_No = GLAcARAdj
                                        objTrPay.Account_Description = GLAcDescARAdj
                                        objTrPay.Amount = NextPCAdjustAmt
                                        objTrPay.Remarks = "Advance sale amount to be adjusted in next payment cycle "
                                        objFAdj.Arr.Add(objTrPay)
                                        objFAdj.SaveData(objFAdj, True, trans)
                                        clsFarmerPaymentAdjustmentEntry.FunPost(objFAdj.Adjustment_No, trans)
                                        ''change by Panch Raj on 01-may-2018 against ticket : KDI/30/04/18-000281
                                        '' adjustment entry to krockup farmer invoice on farmer invoice date
                                        ''1. date 2 remark/description 3. against farmer invoice no
                                        objFAdj.Adjustment_No = ""
                                        objFAdj.Description = "Invoice Setoff ->Payment Process doc No: " & DocNo & ",Farmer Invoice No: " & obj.ArrPPDetail(i).Farmer_Invoice_No & " "
                                        objFAdj.Adjustment_Date = obj.ArrPPDetail(i).AP_Invoice_Date
                                        objFAdj.Doc_No = obj.ArrPPDetail(i).Farmer_Invoice_No
                                        objFAdj.Adjustment_Type = "Invoice"
                                        objFAdj.SaveData(objFAdj, True, trans)
                                        clsFarmerPaymentAdjustmentEntry.FunPost(objFAdj.Adjustment_No, trans)
                                    End If
                                End If

                                ''------------------Payment Entry Start Here
                                If obj.ArrPPDetail(i).Payable_Amount > 0 Then
                                    Dim objPayF As clsFarmerPaymentHeader
                                    objPayF = New clsFarmerPaymentHeader
                                    objPayF.Payment_Process_Code = obj.Doc_No
                                    objPayF.Against_PP_Detail_No = obj.ArrPPDetail(i).Farmer_Invoice_No
                                    objPayF.Payment_No = ""
                                    objPayF.Entry_Desc = Desc + " " + DocNo
                                    objPayF.Payment_Date = clsCommon.myCDate(obj.Doc_Date)
                                    objPayF.Payment_Post_Date = clsCommon.myCDate(obj.Doc_Date)
                                    objPayF.Bank_Code = obj.ArrPPDetail.Item(i).Bank_Code
                                    objPayF.Payment_Type = "PY"
                                    objPayF.Farmer_Code = obj.ArrPPDetail.Item(i).Farmer_Code
                                    objPayF.Farmer_Name = obj.ArrPPDetail.Item(i).Farmer_Name
                                    objPayF.Payment_Code = obj.ArrPPDetail.Item(i).Payment_Mode
                                    objPayF.Cheque_No = obj.ArrPPDetail.Item(i).Cheque_No
                                    If Not obj.ArrPPDetail.Item(i).Cheque_Dated Is Nothing Then
                                        objPayF.Cheque_Date = obj.ArrPPDetail.Item(i).Cheque_Dated
                                    End If

                                    objPayF.Account_Payee = 0
                                    objPayF.memorndmamt = "0"
                                    objPayF.Applied_Payment = clsCommon.myCstr(obj.ArrPPDetail.Item(i).Farmer_Invoice_No)
                                    objPayF.Is_Security = 0
                                    objPayF.IsChkReverse = "N"
                                    objPayF.Bank_Charges = 0

                                    Dim objTrF As clsFarmerPaymentDetail
                                    objPayF.ArrTr = New List(Of clsFarmerPaymentDetail)
                                    objTrF = New clsFarmerPaymentDetail()
                                    objTrF.Apply = "1"
                                    objTrF.Payment_Type = "PY"
                                    objTrF.Document_No = clsCommon.myCstr(obj.ArrPPDetail.Item(i).Farmer_Invoice_No)
                                    objTrF.Original_Invoice_Amt = clsCommon.myCdbl(obj.ArrPPDetail.Item(i).MCC_Sale_Amount)
                                    objTrF.Applied_Amount = clsCommon.myCdbl(obj.ArrPPDetail.Item(i).MCC_Sale_Amount) - AdjAmt + ReturnAdjAmt
                                    objTrF.Pending_Balance = 0
                                    objTrF.Vendor_Invoice_No = ""
                                    objTrF.Net_Balance = 0
                                    objTrF.Security_Amount = 0
                                    objPayF.ArrTr.Add(objTrF)


                                    objPayF.Payment_Amount = obj.ArrPPDetail(i).Payable_Amount
                                    objPayF.Balance_Amt = obj.ArrPPDetail(i).Payable_Amount
                                    objPayF.Location_Code = clsCommon.myCstr(obj.Loc_Seg_Code)
                                    objPayF.Entry_Desc = obj.PaymentDesc + " " + DocNo

                                    objPayF.SaveData1(objPayF, True, trans)
                                    clsFarmerPaymentHeader.PostData(objPayF.Payment_No, "Payable", trans)
                                End If


                            End If

                            '' end of create bank transfer entry
                            ''------------------Payment Entry End Here
                        Next
                        '' posting payment adjustment entry
                        ''clsPaymentAdjustmentEntry.FunPost(objAdj.Adjustment_No, trans) ''Post when save data by balwinder on 11/01/2021
                    Next

                    '' map Farmer Adjustment to invoice 
                    For Each objAdj As clsPaymentProcessAdjustment In obj.ArrPPAdjustment
                        Dim qryAdj As String = "update TSPL_MP_PAY_ADJ_HEAD  set Is_Processed='Y' where Adjustment_No='" & objAdj.Adjustment_No & "'"
                        clsDBFuncationality.ExecuteNonQuery(qryAdj, trans)
                    Next
                    Dim qryB As String = " select TSPL_MP_PAY_HEAD.Payment_Date,CBank.BANK_CODE as From_Bank_Code,CBank.DESCRIPTION as From_Bank_Name," &
                                        " CBank.BANKACC as From_Bank_Acc_No,TSPL_MP_PAY_HEAD.Bank_Code as To_Bank_Code," &
                                        " bank.DESCRIPTION as To_Bank_Name,Bank.BANKACC as To_Bank_Acc_No,Bank.BANKACCNUMBER as TO_BANKACCNUMBER," &
                                        " sum(TSPL_MP_PAY_HEAD.Payment_Amount) as Transfer_Amount,sum(TSPL_MP_PAY_HEAD.Payment_Amount)as Deposit_Amount from TSPL_MP_PAY_HEAD  " &
                                        " inner join TSPL_MP_PAY_DETAIL on TSPL_MP_PAY_HEAD.Payment_No=TSPL_MP_PAY_DETAIL.Payment_No " &
                                        " left join TSPL_BANK_MASTER Bank on Bank.BANK_CODE=TSPL_MP_PAY_HEAD.Bank_Code " &
                                        " left join TSPL_BANK_MASTER CBank on CBank.BANK_CODE=Bank.Main_Bank_Code " &
                                        " where TSPL_MP_PAY_HEAD.PAYMENT_PROCESS_CODE='" & obj.Doc_No & "' " &
                                        " group by TSPL_MP_PAY_HEAD.Payment_Date,CBank.BANK_CODE,CBank.DESCRIPTION, " &
                                        " CBank.BANKACC,TSPL_MP_PAY_HEAD.Bank_Code,bank.DESCRIPTION,Bank.BANKACC,Bank.BANKACCNUMBER"
                    Dim dtB As DataTable = clsDBFuncationality.GetDataTable(qryB, trans)
                    If dtB.Rows.Count > 0 Then
                        For Each drB As DataRow In dtB.Rows
                            Dim objB As New clsBankTrasnferNew
                            objB.Deposit_Amount = clsCommon.myCdbl(drB.Item("Deposit_Amount"))
                            objB.Transfer_Amount = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                            objB.Description = "Bank Transfer against Farmer Payment Process-" & obj.Doc_No & ""
                            If clsCommon.myLen(drB.Item("From_Bank_Acc_No")) <= 0 Then
                                Throw New Exception("From Bank Acc No is not set for Bank -" & objB.From_Bank_Code & ".")
                            End If
                            If clsCommon.myLen(drB.Item("To_Bank_Acc_No")) <= 0 Then
                                Throw New Exception("To Bank Acc No is not set for Bank - " & objB.To_Bank_Code & ".")
                            End If
                            objB.From_Bank_Acc_No = clsCommon.myCstr(drB.Item("From_Bank_Acc_No"))
                            objB.From_Bank_Code = clsCommon.myCstr(drB.Item("From_Bank_Code"))
                            objB.From_Bank_Name = clsCommon.myCstr(drB.Item("From_Bank_Name"))
                            objB.Payment_Mode = "TRANSFER"
                            objB.Reference = "Bank Transfer against Farmer Payment Process-" & obj.Doc_No & ""
                            objB.To_Bank_Acc_No = clsCommon.myCstr(drB.Item("To_Bank_Acc_No"))
                            objB.To_Bank_Code = clsCommon.myCstr(drB.Item("To_Bank_Code"))
                            objB.To_Bank_Name = clsCommon.myCstr(drB.Item("To_Bank_Name"))
                            objB.Transaction_Type = "B"
                            objB.Transfer_Date = clsCommon.myCDate(drB.Item("Payment_Date"))
                            objB.Cheque_No = ""
                            objB.Cheque_Date = clsCommon.myCDate(drB.Item("Payment_Date"))
                            objB.Transfer_No = ""

                            ''2. check balance 
                            ''CheckNegativeBankBalance
                            clsBankTrasnferNew.CheckNegativeBankBalance(objB, trans)
                            clsBankTrasnferNew.SaveData(True, objB, trans)
                            clsBankTrasnferNew.PostData(objB.Transfer_No, trans)
                            objB = Nothing
                            '' create miscellaneous payment entry to transfer amount from bank to gl accounts(control account of farmers)
                            Dim objPayMI As New clsPaymentHeader
                            objPayMI.Payment_No = ""
                            objPayMI.Entry_Desc = "Misc Payment entry against Payment Process Farmer-" & DocNo & ""
                            objPayMI.Bank_Code = clsCommon.myCstr(drB.Item("To_Bank_Code"))
                            objPayMI.Payment_Type = "MI"
                            objPayMI.Bank_Charges = 0
                            objPayMI.Payment_Code = "TRANSFER"
                            objPayMI.Payment_Amount = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                            objPayMI.Total_Applied_Amount = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                            objPayMI.Applied_Payment = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                            objPayMI.CURRENCY_CODE = "INR"
                            objPayMI.ConvRate = 1
                            objPayMI.ConvRateOld = 1
                            objPayMI.BASE_CURRENCY_CODE = "INR"
                            objPayMI.Payment_Date = clsCommon.myCDate(drB.Item("Payment_Date"))
                            objPayMI.ArrTr = New List(Of clsPaymentDetail)
                            '' add account and amount detail
                            Dim objPayMITr As New clsPaymentDetail

                            'Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + objPayMI.Bank_Code + "'", trans)
                            'GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(GLAcARAdj, BankLocation, True, trans)
                            'By Balwinder on 22/02/2020
                            GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(GLAcARAdj, obj.Loc_Seg_Code, True, trans)
                            objPayMITr.Account_Code = GLAcARAdj
                            objPayMITr.Description = clsGLAccount.GetName(GLAcARAdj, trans)
                            objPayMITr.Applied_Amount = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                            objPayMITr.Payment_Type = objPayMI.Payment_Type
                            objPayMITr.Net_Balance = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                            objPayMITr.Remarks = "transfer amount from clearing Bank to control account of Farmer"
                            objPayMITr.ESI_WCT_Percentage = 0

                            objPayMITr.Hirerachy_Level_Code = ""
                            objPayMITr.Cost_Center_Fin_Code = ""
                            objPayMI.ArrTr.Add(objPayMITr)
                            objPayMI.SaveData1(objPayMI, True, trans)
                            clsPaymentHeader.PostData(objPayMI.Payment_No, "Payable", trans)
                            '' update Misc Payment No in farmer payment entry against ticket no: KDI/17/05/18-000318 point no-3
                            qryB = "update TSPL_MP_PAY_HEAD set Misc_Payment_No='" & objPayMI.Payment_No & "' where Payment_Process_Code='" & obj.Doc_No & "' and Bank_Code='" & objPayMI.Bank_Code & "'"
                            clsDBFuncationality.ExecuteNonQuery(qryB, trans)
                            objPayMI = Nothing
                        Next

                    End If
                End If
                'If ArryDeductionGLAC.Count > 0 Then
                '    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Seg_Code, True, trans, obj.Doc_Date, "Total Deduction Against Farmer Payment Process No ( " & obj.Doc_No & ")", "MP-DE", "MP Deduction", obj.Doc_No, "", "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryDeductionGLAC)
                'End If
                If ArryIncentiveGLAC.Count > 0 Then
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Seg_Code, True, trans, obj.To_Date, "Total Incetive Against Farmer Payment Process No ( " & obj.Doc_No & ")", "MP-IV", "MP Incetinve", obj.Doc_No, "", "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryIncentiveGLAC)
                End If





                If obj.ArrPPDetail IsNot Nothing AndAlso obj.ArrPPDetail.Count > 0 Then
                    For i = 0 To obj.ArrPPDetail.Count - 1
                        If obj.ArrPPDetail.Item(i).Total_Advance_Amount_Recovery > 0 Then
                            Dim ArryGLAC As ArrayList = New ArrayList()
                            qry = "select Payable_Account from tspl_vendor_account_set where isfarmer=1"
                            Dim FarmerCtrlAccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(FarmerCtrlAccount) <= 0 Then
                                Throw New Exception("Please set Payable Account for Farmer ")
                            End If
                            FarmerCtrlAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(FarmerCtrlAccount, obj.Loc_Seg_Code, True, trans)
                            Dim AccDr() As String = {FarmerCtrlAccount, Math.Round(((obj.ArrPPDetail.Item(i).Total_Advance_Amount_Recovery)), 2, MidpointRounding.ToEven)}
                            ArryGLAC.Add(AccDr)

                            Dim AccCr() As String = {FarmerCtrlAccount, -1 * Math.Round(((obj.ArrPPDetail.Item(i).Total_Advance_Amount_Recovery)), 2, MidpointRounding.ToEven)}
                            ArryGLAC.Add(AccCr)

                            clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Seg_Code, True, trans, obj.To_Date, "Advance Amount Recovery Against Farmer Payment Process No ( " & obj.Doc_No & ") and MP ( " & obj.ArrPPDetail.Item(i).Farmer_Code & ")", "MP-ST", "MP Short Term Loan", obj.Doc_No, "", "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryGLAC)
                        End If
                        If obj.ArrPPDetail.Item(i).Total_Loan_Payment_Recovery > 0 Then
                            Dim ArryGLAC As ArrayList = New ArrayList()
                            qry = "select Payable_Account from tspl_vendor_account_set where isfarmer=1"
                            Dim FarmerCtrlAccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(FarmerCtrlAccount) <= 0 Then
                                Throw New Exception("Please set Payable Account for Farmer ")
                            End If
                            FarmerCtrlAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(FarmerCtrlAccount, obj.Loc_Seg_Code, True, trans)

                            qry = "select TSPL_PAYMENT_DETAIL.Account_Code,TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE.Knock_Off_Amt   from TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE  " + Environment.NewLine +
     "left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE.Payment_No" + Environment.NewLine +
     "left outer join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and Payment_Line_No=1" + Environment.NewLine +
     "where Doc_No='" + obj.Doc_No + "' and MP_CODE='" + obj.ArrPPDetail.Item(i).Farmer_Code + "' and Is_Loan=1"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                For Each dr As DataRow In dt.Rows
                                    Dim AccDr() As String = {FarmerCtrlAccount, Math.Round((clsCommon.myCdbl(dr("Knock_Off_Amt"))), 2, MidpointRounding.ToEven)}
                                    ArryGLAC.Add(AccDr)

                                    Dim PaymentAccount As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr("Account_Code")), obj.Loc_Seg_Code, True, trans)
                                    Dim AccCr() As String = {PaymentAccount, -1 * Math.Round((clsCommon.myCdbl(dr("Knock_Off_Amt"))), 2, MidpointRounding.ToEven)}
                                    ArryGLAC.Add(AccCr)
                                Next
                                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Seg_Code, True, trans, obj.To_Date, "Loan Amount Recovery Against Farmer Payment Process No ( " & obj.Doc_No & ") and MP ( " & obj.ArrPPDetail.Item(i).Farmer_Code & ")", "MP-LT", "MP Long Term Loan", obj.Doc_No, "", "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryGLAC)
                            End If
                        End If
                    Next

                End If


                '===========================================================================================================  
                qry = " update TSPL_PAYMENT_PROCESS_HEAD set isPosted=1, Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where doc_no='" & obj.Doc_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Doc_No, "TSPL_PAYMENT_PROCESS_HEAD", "Doc_No", "TSPL_PAYMENT_PROCESS_DETAIL", "Doc_No", "TSPL_MP_PAY_PROCESS_DETAIL", "Doc_No", trans)

                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
        Return True
    End Function

    Shared Function createCreditNoteFarmerDeduction(ByVal objtr As clsPaymentProcessDetail, ByVal strLocSeg As String, ByVal trans As SqlTransaction)
        If objtr.MP_Total_Deduction > 0 Then
            Dim dblAmt As Double = objtr.MP_Total_Deduction
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objtr.AP_Invoice_Date, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = objtr.VSP_CODE
            objVendorInvHead.Vendor_Name = objtr.VSP_NAME
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
            objVendorInvHead.loc_code = strLocSeg
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Credit Note Against VSP for For farmer deduction : " & objtr.Doc_No & " .VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "C"
            objVendorInvHead.RefDocType = "FAR-DED"
            objVendorInvHead.RefDocNo = objtr.PP_Detail_No

            objVendorInvHead.On_Hold = False
            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strLocSeg, True, trans)
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If
            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

            Dim isFirstTime As Boolean = True
            objVendorInvHead.Total_Landed_Amt = 0
            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

            Dim dtFarmerCreditNote As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Acct_Set_Code from TSPL_VENDOR_ACCOUNT_SET  where IsFarmer=1 ", trans)
            If dtFarmerCreditNote Is Nothing OrElse dtFarmerCreditNote.Rows.Count <= 0 Then
                Throw New Exception("Please set a vendor Account set For Farmer")
            End If
            If dtFarmerCreditNote IsNot Nothing AndAlso dtFarmerCreditNote.Rows.Count > 1 Then
                Throw New Exception("Please set only one vendor Account set For Farmer")
            End If
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            objVendorInvDetail.Detail_Line_No = 1
            Dim strInvCtrlAC As String = clsCommon.myCstr(dtFarmerCreditNote.Rows(0)("Payable_Account"))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set Payable_Account for Vendor Account Set [" + clsCommon.myCstr(dtFarmerCreditNote.Rows(0)("Acct_Set_Code")) + "] ")
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strLocSeg, True, trans)

            objVendorInvDetail.GL_Account_Code = strInvCtrlAC
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
            objVendorInvDetail.Amount = dblAmt

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = dblAmt
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = dblAmt
            objVendorInvDetail.Landed_Amount = dblAmt
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += dblAmt
            objVendorInvHead.Discount_Base += dblAmt
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += dblAmt
            objVendorInvHead.Document_Total += dblAmt
            objVendorInvHead.Balance_Amt += dblAmt
            ''End of Set AP Invvoice Header Table

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
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(objVendorInvHead.Invoice_Entry_Date, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, "", False)
        End If
        Return True
    End Function

    Shared Function CreateApplyDocumentForAdavancePayment(ByVal dtTo As DateTime, ByVal ArrPPAdvancePayment As List(Of clsPaymentProcessFarmerAdvancePayment), ByVal objtr As clsPaymentProcessDetail, ByVal strLocSeg As String, ByVal trans As SqlTransaction)
        Dim KFAmt As Double = objtr.Advance_Payment_Amount_Knock_Off
        If objtr.Advance_Payment_Amount_Knock_Off > 0 Then
            If ArrPPAdvancePayment IsNot Nothing AndAlso ArrPPAdvancePayment.Count > 0 Then
                For Each objAdvancePayment As clsPaymentProcessFarmerAdvancePayment In ArrPPAdvancePayment
                    If clsCommon.CompairString(objAdvancePayment.Vendor_Code, objtr.VSP_CODE) = CompairStringResult.Equal Then
                        Dim objPay As clsPaymentHeader = New clsPaymentHeader()
                        objPay.Payment_No = ""
                        objPay.Payment_Date = dtTo
                        objPay.Payment_Post_Date = dtTo
                        objPay.Bank_Code = objtr.Bank_Code
                        objPay.Payment_Type = "AD"
                        objPay.Vendor_Code = objtr.VSP_CODE
                        objPay.Vendor_Name = objtr.VSP_NAME
                        objPay.Payment_Amount = objAdvancePayment.Amount_Knock_Off
                        objPay.Balance_Amt = objAdvancePayment.Amount_Knock_Off
                        objPay.Location_GL_Code = strLocSeg
                        objPay.Entry_Desc = "Apply document of Advance payment"
                        objPay.Applied_Payment = objAdvancePayment.Payment_No
                        objPay.Against_PP_Detail_No_Advance_Payment = objtr.PP_Detail_No

                        objPay.ArrTr = New List(Of clsPaymentDetail)

                        Dim objPayTr As clsPaymentDetail = New clsPaymentDetail()
                        objPayTr.Apply = "1"
                        objPayTr.Payment_Type = "AD"
                        objPayTr.Document_No = objtr.AP_Invoice_No
                        objPayTr.Net_Balance = objtr.Total_Invoice_Amount
                        objPayTr.Original_Invoice_Amt = objtr.Total_Invoice_Amount   '' objtr.Advance_Payment_Amount_Knock_Off + objtr.Payable_Amount
                        objPayTr.Applied_Amount = objAdvancePayment.Amount_Knock_Off
                        KFAmt -= objAdvancePayment.Amount_Knock_Off
                        objPayTr.Pending_Balance = KFAmt


                        objPayTr.Vendor_Invoice_No = objtr.Milk_Purchase_Invoice_No

                        objPayTr.Security_Amount = 0
                        objPayTr.ConvRateOld = 1
                        objPay.ArrTr.Add(objPayTr)

                        objPay.SaveData(objPay, True, trans, True)
                        clsPaymentHeader.PostData(objPay.Payment_No, "Payable", trans)
                    End If
                Next
            End If
        End If

        Return True
    End Function

    Shared Function createDebitNote(ByVal objtr As clsPaymentProcessDetail, ByVal strLocSeg As String, ByVal trans As SqlTransaction)
        If objtr.MP_Net_Amount > 0 AndAlso objtr.VSP_Amount > objtr.MP_Net_Amount Then
            Dim dblAmt As Double = objtr.VSP_Amount - objtr.MP_Net_Amount
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            objVendorInvHead.isDeduction = 1
            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                Throw New Exception("Please set default VSP deduction code")
            End If
            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))

            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objtr.AP_Invoice_Date.AddDays(1), "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = objtr.VSP_CODE
            objVendorInvHead.Vendor_Name = objtr.VSP_NAME
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
            objVendorInvHead.loc_code = strLocSeg
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against VSP Vs MP Collection : " & objtr.PP_Detail_No & " .VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p

            '' ''added by priti
            objVendorInvHead.RefDocType = "VSP-MP"
            objVendorInvHead.RefDocNo = objtr.PP_Detail_No
            'objVendorInvHead.Ref_SNo = objtr.SAMPLE_NO
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strLocSeg, True, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strLocSeg, True, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()



            ''Set AP Invvoice Detail Table

            Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set Deduction Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")))
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strLocSeg, True, trans)




            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strInvCtrlAC
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
            objVendorInvDetail.Amount = dblAmt

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = dblAmt
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = dblAmt
            objVendorInvDetail.Landed_Amount = dblAmt
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += dblAmt
            objVendorInvHead.Discount_Base += dblAmt
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += dblAmt
            objVendorInvHead.Document_Total += dblAmt
            objVendorInvHead.Balance_Amt += dblAmt
            ''End of Set AP Invvoice Header Table

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
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(objVendorInvHead.Invoice_Entry_Date, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, "", False)
        End If
        Return True
    End Function

    Shared Function createDebitNoteNextPaymentCycle(ByVal objtr As clsPaymentProcessDetail, ByVal strLocSeg As String, ByVal trans As SqlTransaction)
        If objtr.NextCycleDebitNote > 0 Then
            Dim dblAmt As Double = objtr.NextCycleDebitNote
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            objVendorInvHead.isDeduction = 1
            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                Throw New Exception("Please set default VSP deduction code")
            End If
            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))

            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objtr.AP_Invoice_Date.AddDays(1), "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = objtr.VSP_CODE
            objVendorInvHead.Vendor_Name = objtr.VSP_NAME
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
            objVendorInvHead.loc_code = strLocSeg
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against VSP for extra amount to be paid by vsp : " & objtr.PP_Detail_No & " .VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p

            '' ''added by priti
            objVendorInvHead.RefDocType = "VSP"
            objVendorInvHead.RefDocNo = objtr.PP_Detail_No

            objVendorInvHead.On_Hold = False

            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strLocSeg, True, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strLocSeg, True, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

            '' Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
            ''pick gl account from deduction master instead of Vendor account set  ERO/28/07/20-001313
            Dim strInvCtrlAC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GL_Account_Code  from tspl_deduction_master where code='" + objVendorInvDetail.DeductionCode + "'", trans))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set gl Account for Deduction Code :" + clsCommon.myCstr(objVendorInvDetail.DeductionCode))
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strLocSeg, True, trans)

            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strInvCtrlAC
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
            objVendorInvDetail.Amount = dblAmt

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = dblAmt
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = dblAmt
            objVendorInvDetail.Landed_Amount = dblAmt
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += dblAmt
            objVendorInvHead.Discount_Base += dblAmt
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += dblAmt
            objVendorInvHead.Document_Total += dblAmt
            objVendorInvHead.Balance_Amt += dblAmt
            ''End of Set AP Invvoice Header Table

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
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(objVendorInvHead.Invoice_Entry_Date, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, "", False)
        End If
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsPaymentProcessFarmerHead, ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try


            clsPaymentProcessFarmerHead.deleteData(obj.Doc_No, trans)
            Dim dt As Date = clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy") 'clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
            If isNewEntry Then
                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.PaymentProcessFarmer, "", obj.Loc_Seg_Code, True)
                If clsCommon.myLen(obj.Doc_No) <= 0 Then
                    Throw New Exception("Error In Doc No Generation")
                End If
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmPaymentProcessFarmer, obj.Loc_Seg_Code, obj.Doc_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "FarmType", "PPF")
            clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(obj.Doc_No))
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Loc_Seg_Code", clsCommon.myCstr(obj.Loc_Seg_Code))
            clsCommon.AddColumnsForChange(coll, "PaymentDesc", clsCommon.myCstr(obj.PaymentDesc))
            clsCommon.AddColumnsForChange(coll, "isPosted", 0)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "DocRefNoForUploader", clsCommon.myCstr(obj.DocRefNoForUploader))
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_HEAD", OMInsertOrUpdate.Insert, "", trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerInvoices.SaveData(obj.Doc_No, obj.arrclsPaymentProcessFarmerInvoices, trans)
            issaved = issaved AndAlso clsPaymentProcessMCCSale.SaveData(obj.Doc_No, obj.arrClsPaymentProcessMccSale, trans)
            issaved = issaved AndAlso clsPaymentProcessMCCSaleReturn.SaveData(obj.Doc_No, obj.arrClsPaymentProcessMccSaleReturn, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerMCCSale.SaveData(obj.Doc_No, obj.arrclsPaymentProcessFarmerMccSale, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerMCCSaleReturn.SaveData(obj.Doc_No, obj.arrclsPaymentProcessFarmerMccSaleReturn, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerItemIssue.SaveData(obj.Doc_No, obj.arrclsPaymentProcessFarmerItemIssue, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerItemIssueReturn.SaveData(obj.Doc_No, obj.arrclsPaymentProcessFarmerItemIssueReturn, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerDeduction.SaveData(obj.Doc_No, obj.arrclsPaymentProcessFarmerDeductions, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerCreditNote.SaveData(obj.Doc_No, obj.arrclsPaymentProcessFarmerCreditNote, trans)
            issaved = issaved AndAlso clsPaymentProcessDetail.SaveData(obj.Doc_No, obj.ArrVSPPPDetail, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerPaymentDetail.SaveData(obj.Doc_No, obj.Loc_Seg_Code, obj.ArrPPDetail, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerAdvancePayment.SaveData(obj.Doc_No, obj.ArrPPAdvancePayment, obj.ArrVSPPPDetail, trans) ''It Should be at last
            issaved = issaved AndAlso clsPaymentProcessAdjustment.SaveData(obj.Doc_No, obj.ArrPPAdjustment, trans)
            issaved = issaved AndAlso clsPaymentProcessFarmerMPAdvance.SaveData(obj.Doc_No, obj.arrMPAdvance, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing, Optional Vendorcode As String = "") As clsPaymentProcessFarmerHead
        Dim obj As clsPaymentProcessFarmerHead = Nothing
        Try
            Dim whrCls As String = String.Empty
            whrCls = " and FarmType='PPF' "
            Dim qst As String = " select * From TSPL_PAYMENT_PROCESS_HEAD   where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in (select min(Doc_No ) from TSPL_PAYMENT_PROCESS_HEAD where Doc_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in (select MIN(Doc_No ) from TSPL_PAYMENT_PROCESS_HEAD where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in (select Max(Doc_No ) from TSPL_PAYMENT_PROCESS_HEAD where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in (select Max(Doc_No ) from TSPL_PAYMENT_PROCESS_HEAD where Doc_No  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsPaymentProcessFarmerHead
                obj.FarmType = "PPF"
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.DocRefNoForUploader = clsCommon.myCstr(dt.Rows(0)("DocRefNoForUploader"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
                obj.Loc_Seg_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Seg_Code"))
                obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                obj.PaymentDesc = clsCommon.myCstr(dt.Rows(0)("PaymentDesc"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.ArrVSPPPDetail = clsPaymentProcessDetail.getData(obj.Doc_No, trans)
                obj.ArrPPDetail = clsPaymentProcessFarmerPaymentDetail.getData(obj.Doc_No, trans)
                obj.arrclsPaymentProcessFarmerInvoices = clsPaymentProcessFarmerInvoices.getData(obj.Doc_No, trans)

                obj.arrClsPaymentProcessMccSale = clsPaymentProcessMCCSale.getData(obj.Doc_No, trans)
                obj.arrClsPaymentProcessMccSaleReturn = clsPaymentProcessMCCSaleReturn.getData(obj.Doc_No, trans)

                obj.arrclsPaymentProcessFarmerMccSale = clsPaymentProcessFarmerMCCSale.getData(obj.Doc_No, trans)
                obj.arrclsPaymentProcessFarmerMccSaleReturn = clsPaymentProcessFarmerMCCSaleReturn.getData(obj.Doc_No, trans)
                obj.arrclsPaymentProcessFarmerItemIssue = clsPaymentProcessFarmerItemIssue.getData(obj.Doc_No, trans)
                obj.arrclsPaymentProcessFarmerItemIssueReturn = clsPaymentProcessFarmerItemIssueReturn.getData(obj.Doc_No, trans)
                obj.arrclsPaymentProcessFarmerDeductions = clsPaymentProcessFarmerDeduction.getData(obj.Doc_No, trans)
                obj.arrclsPaymentProcessFarmerCreditNote = clsPaymentProcessFarmerCreditNote.getData(obj.Doc_No, trans)
                obj.ArrPPAdvancePayment = clsPaymentProcessFarmerAdvancePayment.getData(obj.Doc_No, trans)
                obj.ArrPPAdjustment = clsPaymentProcessAdjustment.getData(obj.Doc_No, trans)
                obj.arrMPAdvance = clsPaymentProcessFarmerMPAdvance.getData(obj.Doc_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal DocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            deleteData(DocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Doc_Date , Loc_Seg_Code from TSPL_PAYMENT_PROCESS_HEAD where Doc_No='" + DocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmPaymentProcessFarmer, clsCommon.myCstr(dt.Rows(0)("Loc_Seg_Code")), clsCommon.myCDate(dt.Rows(0)("Doc_Date")), trans)

            End If
            'Dim BaseQry As String = "select Adjustment_No,TSPL_JOURNAL_MASTER.Voucher_No from (" + Environment.NewLine +
            '"select AP_Invoice_No from TSPL_MP_PAY_PROCESS_DETAIL where doc_No='" + DocNo + "' group by AP_Invoice_No" + Environment.NewLine +
            '")xx left outer join TSPL_Payment_Adjustment_Header on TSPL_Payment_Adjustment_Header.Doc_No=xx.AP_Invoice_No and Adjust_Type='P'" + Environment.NewLine +
            '"left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Payment_Adjustment_Header.Adjustment_No and TSPL_JOURNAL_MASTER.Source_Code='AP-AD'  "

            'Dim qry As String = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from (" + BaseQry + ")x)"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'qry = " delete from TSPL_JOURNAL_MASTER where Voucher_No in (select Voucher_No from (" + BaseQry + ")x)"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_Payment_Adjustment_Detail  where  Adjustment_No in (select Adjustment_No from (" + BaseQry + ")x)"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'qry = "delete from TSPL_Payment_Adjustment_Header where  Adjustment_No in (select Adjustment_No from (" + BaseQry + ")x)"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim arrAPNo As New List(Of String)
            Dim arr As List(Of clsPaymentProcessFarmerPaymentDetail) = clsPaymentProcessFarmerPaymentDetail.getData(DocNo, trans)
            For i = 0 To arr.Count - 1
                If Not arrAPNo.Contains(arr.Item(i).AP_Invoice_No) Then
                    arrAPNo.Add(arr.Item(i).AP_Invoice_No)
                    Dim Adjustment_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Adjustment_No from TSPL_Payment_Adjustment_Header where Doc_No='" & arr.Item(i).AP_Invoice_No & "'", trans))
                    If clsCommon.myLen(Adjustment_No) > 0 Then
                        clsPaymentAdjustmentEntry.ReverseAndUnpost(Adjustment_No, trans, True)
                        clsPaymentAdjustmentEntry.DeleteData(Adjustment_No, trans)
                    End If
                End If
            Next
            arrAPNo = Nothing
            arr = Nothing
            If clsCommon.myLen(clsCommon.myCstr(DocNo)) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, DocNo, "TSPL_PAYMENT_PROCESS_HEAD", "Doc_No", "TSPL_PAYMENT_PROCESS_DETAIL", "Doc_No", "TSPL_MP_PAY_PROCESS_DETAIL", "Doc_No", trans)
            End If

            clsPaymentProcessFarmerInvoices.deleteData(DocNo, trans)
            clsPaymentProcessFarmerMCCSale.deleteData(DocNo, trans)
            clsPaymentProcessMCCSale.deleteData(DocNo, trans)
            clsPaymentProcessFarmerMCCSaleReturn.deleteData(DocNo, trans)
            clsPaymentProcessMCCSaleReturn.deleteData(DocNo, trans)
            clsPaymentProcessFarmerItemIssue.deleteData(DocNo, trans)
            clsPaymentProcessFarmerItemIssueReturn.deleteData(DocNo, trans)
            clsPaymentProcessFarmerDeduction.deleteData(DocNo, trans)
            clsPaymentProcessFarmerCreditNote.deleteData(DocNo, trans)
            clsPaymentProcessFarmerPaymentDetail.deleteData(DocNo, trans)
            clsPaymentProcessFarmerAdvancePayment.deleteData(DocNo, trans)
            clsPaymentProcessFarmerPaymentDetail.deleteData(DocNo, trans)
            clsPaymentProcessFarmerMCCSale.deleteData(DocNo, trans)
            clsPaymentProcessFarmerMCCSaleReturn.deleteData(DocNo, trans)
            clsPaymentProcessDetail.deleteData(DocNo, trans)
            clsPaymentProcessAdjustment.deleteData(DocNo, trans)
            clsPaymentProcessFarmerMPAdvance.DeleteData(DocNo, trans)

            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_HEAD where  Doc_No='" & DocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Try
            Dim str As String = ""
            Dim qry As String = " select TSPL_PAYMENT_PROCESS_HEAD.Doc_No as [DocumentNo] ,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date as [Doc Date] ,TSPL_PAYMENT_PROCESS_HEAD.From_Date as [From Date] ,TSPL_PAYMENT_PROCESS_HEAD.To_Date as [To Date] ,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_GL_SEGMENT_CODE.description as [MCC Name] ,TSPL_PAYMENT_PROCESS_HEAD.Created_By as [Created By] ,TSPL_PAYMENT_PROCESS_HEAD.Created_Date as [Created Date] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_By as [Modified By] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_Date as [Modified Date] ,TSPL_PAYMENT_PROCESS_HEAD.Comp_Code as [Comp Code] ,case when isnull(TSPL_PAYMENT_PROCESS_HEAD.isPosted,0)=0 then 'NO' else 'YES' end as [Posting Status] ,TSPL_PAYMENT_PROCESS_HEAD.Posting_Date as [Posting Date],TSPL_PAYMENT_PROCESS_HEAD.DocRefNoForUploader as [NEFT Uploader Ref No],PMode.Payment_Mode as [Payment Mode],PMode.Payable_Amount as [Payable Amount]  " & _
                " From TSPL_PAYMENT_PROCESS_HEAD left outer join TSPL_GL_SEGMENT_CODE  on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code " & _
                " left join (select Doc_No as PP_Code,Max(Payment_Mode) as Payment_Mode,sum(Payable_Amount) as Payable_Amount  from TSPL_PAYMENT_PROCESS_DETAIL group by Doc_No) PMode on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=PMode.PP_Code "

            str = clsCommon.ShowSelectForm("fndPayProcess", qry, "DocumentNo", whrcls, curcode, "DocumentNo", isButtonClicked)
            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Payment process Document no Not found to reverse and unpost")
            End If
            Dim qry As String = "select isPosted from TSPL_PAYMENT_PROCESS_HEAD where Doc_No='" + strDocNo + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <> 1 Then
                Throw New Exception("Payment process Document no should be posted to reverse and unpost")
            End If


            ''Deduction Journal entry
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select  voucher_no from TSPL_JOURNAL_MASTER where Source_Code in ('MP-DE','MP-IV','MP-LT','MP-ST') and Source_Doc_No in ('" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER where Source_Code in ('MP-DE','MP-IV','MP-LT','MP-ST') and Source_Doc_No in ('" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            ''End of  Deduction Journal entry


            '------------------Bank Transfer
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='BK-TF' and Source_Doc_No in (select Transfer_No from TSPL_BANK_TRANSFER where Reference like '%" + strDocNo + "%'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='BK-TF' and Source_Doc_No in (select Transfer_No from TSPL_BANK_TRANSFER where Reference like '%" + strDocNo + "%')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BANK_TRANSFER where Reference like '%" + strDocNo + "%'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '------------------End of Bank Transfer

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select distinct Misc_Payment_No as Misc_Payment_No  from TSPL_MP_PAY_HEAD where Payment_Process_Code='" + strDocNo + "'", trans)



            '------------------Farmer Payment
            qry = "Delete from TSPL_MP_PAY_DETAIL where payment_no  in ( select payment_no from TSPL_MP_PAY_HEAD  where Payment_Process_Code='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_MP_PAY_HEAD  where Payment_Process_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '------------------End of Farmer Payment
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    '-------Misc Payment
                    qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + clsCommon.myCstr(dr("Misc_Payment_No")) + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + clsCommon.myCstr(dr("Misc_Payment_No")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Delete from TSPL_PAYMENT_BANK_CHARGES_TAX where  Payment_No ='" + clsCommon.myCstr(dr("Misc_Payment_No")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Delete from TSPL_PAYMENT_DETAIL_GST where  Payment_No ='" + clsCommon.myCstr(dr("Misc_Payment_No")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Delete from TSPL_PAYMENT_DETAIL where Payment_No ='" + clsCommon.myCstr(dr("Misc_Payment_No")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Delete from TSPL_PAYMENT_HEADER where Payment_No ='" + clsCommon.myCstr(dr("Misc_Payment_No")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '-------End of Misc Payment
                Next
            End If





            '------------------MP Payment Adjustment
            qry = "Delete from TSPL_MP_PAY_ADJ_DETAIL where Adjustment_No in (select Adjustment_No from TSPL_MP_PAY_ADJ_HEAD where Description like '%" + strDocNo + "%')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_MP_PAY_ADJ_HEAD where Description like '%" + strDocNo + "%'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '------------------End of MP Payment Adjustment


            '-------MCC Sale Return adjustment
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where exists (select 1 from TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN where TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Sale_Doc_No=TSPL_Receipt_Adjustment_Header.Doc_No and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.AR_Invoice_No=TSPL_Receipt_Adjustment_Header.ARInvoiceNo and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where exists (select 1 from TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN where TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Sale_Doc_No=TSPL_Receipt_Adjustment_Header.Doc_No and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.AR_Invoice_No=TSPL_Receipt_Adjustment_Header.ARInvoiceNo and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No='" + strDocNo + "')) "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Receipt_Adjustment_Detail where Adjustment_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where exists (select 1 from TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN where TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Sale_Doc_No=TSPL_Receipt_Adjustment_Header.Doc_No and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.AR_Invoice_No=TSPL_Receipt_Adjustment_Header.ARInvoiceNo and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Receipt_Adjustment_Header where exists (select 1 from TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN where TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Sale_Doc_No=TSPL_Receipt_Adjustment_Header.Doc_No and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.AR_Invoice_No=TSPL_Receipt_Adjustment_Header.ARInvoiceNo and TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '-------End of MCC Sale Return adjustment

            '-------Payment Entry
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_PP_Detail_No in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_PP_Detail_No in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_BANK_CHARGES_TAX where  Payment_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_PP_Detail_No in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_DETAIL_GST where  Payment_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_PP_Detail_No in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_DETAIL where Payment_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_PP_Detail_No in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_HEADER where Against_PP_Detail_No in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '-------End of Payment Entry

            '' delete AP Debit note of VSP when VSP amount is greater than farmer amount
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-DN' and Source_Doc_No in (select Document_no from tspl_vendor_invoice_head where refDocNo='" + strDocNo + "' and RefDocType='Farmer DN'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='AP-DN' and Source_Doc_No in (select Document_no from tspl_vendor_invoice_head where refDocNo='" + strDocNo + "' and RefDocType='Farmer DN')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from tspl_vendor_invoice_detail where Document_no in  (select Document_no from tspl_vendor_invoice_head where refDocNo='" + strDocNo + "' and RefDocType='Farmer DN')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from tspl_vendor_invoice_head where refDocNo='" + strDocNo + "' and RefDocType='Farmer DN'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete AP Credit Note of farmer Deduction
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-CN' and Source_Doc_No in (select Document_no from tspl_vendor_invoice_head where RefDocType='FAR-DED' and refDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where   doc_no='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='AP-CN' and Source_Doc_No in (select Document_no from tspl_vendor_invoice_head where RefDocType='FAR-DED' and refDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where   doc_no='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from tspl_vendor_invoice_detail where Document_no in  (select Document_no from tspl_vendor_invoice_head where RefDocType='FAR-DED' and refDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where   doc_no='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from tspl_vendor_invoice_head where  RefDocType='FAR-DED' and refDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where   doc_no='" + strDocNo + "') "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            '----------Delete debit note Next Payment Cycle Done
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from  TSPL_REMITTANCE where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from  TSPL_VENDOR_INVOICE_DETAIL where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_VENDOR_INVOICE_HEAD where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '----------End Of Delete debit note Next Payment Cycle

            '---------Payable adjustment Done
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header where Doc_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header where Doc_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from  TSPL_Payment_Adjustment_Detail where Adjustment_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header where Doc_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Payment_Adjustment_Header where Doc_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '---------End of Payable adjustment

            '---------------Advance Payment Done
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_PP_Detail_No_Advance_Payment in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "' )))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_PP_Detail_No_Advance_Payment in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "' ))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_DETAIL where Payment_No in (select Payment_No from TSPL_PAYMENT_HEADER where Against_PP_Detail_No_Advance_Payment in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "' ))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PAYMENT_HEADER where Against_PP_Detail_No_Advance_Payment in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "' )"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '---------------End of Advance Payment

            '----------- Receipt Adjustment Done
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where exists (select 1 from TSPL_PAYMENT_PROCESS_MCC_SALE where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No='" + strDocNo + "' and TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Sale_Doc_No and TSPL_Receipt_Adjustment_Header.ARInvoiceNo=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No)))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where exists (select 1 from TSPL_PAYMENT_PROCESS_MCC_SALE where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No='" + strDocNo + "' and TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Sale_Doc_No and TSPL_Receipt_Adjustment_Header.ARInvoiceNo=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Receipt_Adjustment_Detail where Adjustment_No in ( select Adjustment_No from TSPL_Receipt_Adjustment_Header where exists (select 1 from TSPL_PAYMENT_PROCESS_MCC_SALE where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No='" + strDocNo + "' and TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Sale_Doc_No and TSPL_Receipt_Adjustment_Header.ARInvoiceNo=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Receipt_Adjustment_Header where exists (select 1 from TSPL_PAYMENT_PROCESS_MCC_SALE where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No='" + strDocNo + "' and TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Sale_Doc_No and TSPL_Receipt_Adjustment_Header.ARInvoiceNo=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No)"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '----------- End of Receipt Adjustment 


            ''Update AP Invoice Balance Amont Type Invoice
            qry = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt=Document_Total where Document_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''Update AP Invoice Balance Amont Type Debit/Credit Note/ItemIssue/ItemIssueRetrun
            qry = "update TSPL_VENDOR_INVOICE_HEAD  set Balance_Amt=xx.BalanceAmt from (" + Environment.NewLine +
            " select AP_Invoice_No,( Amount-Reduce_Deduc_Amt) as BalanceAmt from TSPL_PAYMENT_PROCESS_DEDUCTION where Doc_No='" + strDocNo + "'  " + Environment.NewLine +
            " union all " + Environment.NewLine +
            " select AP_Invoice_No,(Amount) as BalanceAmt from TSPL_PAYMENT_PROCESS_CREDIT_NOTE where Doc_No='" + strDocNo + "'  " + Environment.NewLine +
            "union all" + Environment.NewLine +
            "select AP_Invoice_No,(Amount-Reduce_Deduc_Amt) as BalanceAmt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE where Doc_No='" + strDocNo + "'" + Environment.NewLine +
            "union all" + Environment.NewLine +
            "select AP_Invoice_No,(Amount) as BalanceAmt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  where Doc_No='" + strDocNo + "'" + Environment.NewLine +
            " )xx " + Environment.NewLine +
            "inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=xx.AP_Invoice_No"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Update Advance Payment entry Balance Amount
            qry = "update TSPL_PAYMENT_HEADER set Balance_Amt=Balance_Amt+xx.Amount_Knock_Off from (" + Environment.NewLine +
            "select  Payment_No,Amount_Knock_Off from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT where Doc_No='" + strDocNo + "'" + Environment.NewLine +
            ")xx inner join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=xx.Payment_No"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''UpdateAR Invoice balance amount for MCC Sale/MCC Sale Return
            qry = "update TSPL_Customer_Invoice_Head set Balance_Amt=xx.BalanceAmt from (" + Environment.NewLine +
            " select AR_Invoice_No,(Amount-Reduce_Deduc_Amt) as BalanceAmt  from TSPL_PAYMENT_PROCESS_MCC_SALE where Doc_No='" + strDocNo + "'" + Environment.NewLine +
            " union all " + Environment.NewLine +
            " select AR_Invoice_No,(Amount-Reduce_Deduc_Amt) as BalanceAmt  from TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN where Doc_No='" + strDocNo + "' " + Environment.NewLine +
            " )xx inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=xx.AR_Invoice_No"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PAYMENT_PROCESS_HEAD set isPosted=0, Posting_Date=null where doc_no='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_PAYMENT_PROCESS_HEAD", "Doc_No", "TSPL_PAYMENT_PROCESS_DETAIL", "Doc_No", "TSPL_MP_PAY_PROCESS_DETAIL", "Doc_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpostSelected(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Payment process Document no Not found to reverse and unpost")
            End If
            Dim qry As String = "select isPosted from TSPL_PAYMENT_PROCESS_HEAD where Doc_No='" + strDocNo + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <> 1 Then
                Throw New Exception("Payment process Document no should be posted to reverse and unpost")
            End If




            '' delete AP Debit note of VSP when VSP amount is greater than farmer amount
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-DN' and Source_Doc_No in (select Document_no from tspl_vendor_invoice_head where refDocNo='" + strDocNo + "' and RefDocType='Farmer DN'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='AP-DN' and Source_Doc_No in (select Document_no from tspl_vendor_invoice_head where refDocNo='" + strDocNo + "' and RefDocType='Farmer DN')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from tspl_vendor_invoice_detail where Document_no in  (select Document_no from tspl_vendor_invoice_head where refDocNo='" + strDocNo + "' and RefDocType='Farmer DN')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from tspl_vendor_invoice_head where refDocNo='" + strDocNo + "' and RefDocType='Farmer DN'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            '----------Delete debit note Next Payment Cycle Done
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from  TSPL_REMITTANCE where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from  TSPL_VENDOR_INVOICE_DETAIL where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_VENDOR_INVOICE_HEAD where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where RefDocType = 'VSP' and RefDocNo in (select PP_Detail_No from TSPL_PAYMENT_PROCESS_DETAIL where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '----------End Of Delete debit note Next Payment Cycle

            '---------Payable adjustment Done
            qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header where TSPL_Payment_Adjustment_Header.Adjust_Type='R' and Doc_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_JOURNAL_MASTER where Source_Doc_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header where TSPL_Payment_Adjustment_Header.Adjust_Type='R' and Doc_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from  TSPL_Payment_Adjustment_Detail where Adjustment_No in ( select Adjustment_No from TSPL_Payment_Adjustment_Header where TSPL_Payment_Adjustment_Header.Adjust_Type='R' and Doc_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Payment_Adjustment_Header where TSPL_Payment_Adjustment_Header.Adjust_Type='R' and Doc_No in (select AP_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '---------End of Payable adjustment

            qry = "update TSPL_PAYMENT_PROCESS_HEAD set isPosted=0, Posting_Date=null where doc_no='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_PAYMENT_PROCESS_HEAD", "Doc_No", "TSPL_PAYMENT_PROCESS_DETAIL", "Doc_No", "TSPL_MP_PAY_PROCESS_DETAIL", "Doc_No", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ProcessDataSelected(ByVal DocNo As String, ByVal Desc As String) As Boolean
        Dim obj As clsPaymentProcessFarmerHead = clsPaymentProcessFarmerHead.getData(DocNo, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim i As Integer = 0
        Dim Counter As Integer = 0
        Dim AdjAmt As Double = 0
        Dim ReturnAdjAmt As Double = 0

        Dim todaydt As Date = clsCommon.GETSERVERDATE(trans)
        Dim objRcpt As clsAdjustmentEntryReceivables = Nothing
        Dim objPayAdj As clsPaymentAdjustmentEntry = Nothing
        Dim DisCCodeForArAdj As String = ""
        Dim GLAcARAdj As String = ""
        Dim DiscDiscForArAdj As String = ""
        Dim GLAcDescARAdj As String = ""
        Dim objPay As clsPaymentHeader = Nothing
        Dim objTr As New clsPaymentDetail()
        Try
            clsCommon.ProgressBarPercentShow()
            DisCCodeForArAdj = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForArAdj, clsFixedParameterCode.DiscountCodeForArAdj, trans)
            If clsCommon.myLen(DisCCodeForArAdj) <= 0 Then
                Throw New Exception("Please Map Discount code from Sale setting")
            End If
            DiscDiscForArAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))
            GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Code from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans)), obj.Loc_Seg_Code, True, trans)
            GLAcDescARAdj = clsGLAccount.GetName(GLAcARAdj, trans)
            '' process vsp payment
            If obj.ArrPPDetail IsNot Nothing And obj.ArrPPDetail.Count > 0 Then
                ''If obj.arrClsPaymentProcessMccSale IsNot Nothing AndAlso obj.arrClsPaymentProcessMccSale.Count > 0 Then
                ''    For i = 0 To obj.arrClsPaymentProcessMccSale.Count - 1
                ''        clsCommon.ProgressBarPercentUpdate(i * 100 / obj.arrClsPaymentProcessMccSale.Count, "Updating AR Adjustment Record " & (i + 1) & " Of " & obj.arrClsPaymentProcessMccSale.Count)
                ''        objRcpt = New clsAdjustmentEntryReceivables
                ''        objRcpt.Adjustment_No = ""
                ''        objRcpt.Description = " Adjustment Against Bulk Payment Process "
                ''        objRcpt.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                ''        objRcpt.Customer_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cust_Code  from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" & obj.arrClsPaymentProcessMccSale(i).Customer_CODE & "' ", trans))
                ''        objRcpt.Customer_Name = clsCommon.myCstr(clsCustomerMaster.GetName(objRcpt.Customer_No, trans))
                ''        objRcpt.Doc_No = clsCommon.myCstr(obj.arrClsPaymentProcessMccSale(i).Sale_Doc_No)
                ''        Dim ReturnAmt As Double = 0
                ''        For ireturn As Integer = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                ''            If clsCommon.myCstr(obj.arrClsPaymentProcessMccSale(i).Sale_Doc_No) = clsCommon.myCstr(obj.arrClsPaymentProcessMccSaleReturn(ireturn).Sale_Doc_No) Then
                ''                ReturnAmt += clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(ireturn).Amount)
                ''            End If
                ''        Next
                ''        objRcpt.ARInvoiceNo = clsCommon.myCstr(obj.arrClsPaymentProcessMccSale(i).AR_Invoice_No)
                ''        objRcpt.Doc_Amount = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale(i).Amount) - ReturnAmt
                ''        objRcpt.Remarks = ""
                ''        objRcpt.Adjustment_Amount = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale(i).Amount - obj.arrClsPaymentProcessMccSale(i).Reduce_Deduc_Amt) - ReturnAmt
                ''        objRcpt.Arr = New List(Of clsAdjustmentEntryReceivablesDetail)
                ''        Dim objTrRcpt As New clsAdjustmentEntryReceivablesDetail()
                ''        objTrRcpt.Discount_Code = DisCCodeForArAdj
                ''        objTrRcpt.Discount_Description = DiscDiscForArAdj
                ''        objTrRcpt.Account_No = GLAcARAdj
                ''        objTrRcpt.Account_Description = GLAcDescARAdj
                ''        objTrRcpt.Amount = clsCommon.myCdbl(clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale(i).Amount - obj.arrClsPaymentProcessMccSale(i).Reduce_Deduc_Amt - ReturnAmt))
                ''        objTrRcpt.Remarks = ""
                ''        objRcpt.Arr.Add(objTrRcpt)
                ''        'If clsCommon.myCdbl(objTrRcpt.Amount) > 0 Then
                ''        '    objRcpt.SaveData(objRcpt, True, trans)
                ''        '    clsAdjustmentEntryReceivables.FunPost(objRcpt.Adjustment_No, trans)
                ''        'End If
                ''    Next
                ''End If

                For i = 0 To obj.ArrVSPPPDetail.Count - 1
                    clsCommon.ProgressBarPercentUpdate(i * 100 / obj.ArrVSPPPDetail.Count, " Creating Payment Entry  Record " & (i + 1) & " Of " & obj.ArrVSPPPDetail.Count)
                    If Not obj.ArrVSPPPDetail(i).Is_select Then
                        Continue For
                    End If
                    ''Becuause of Knock off amount will always be create.
                    'CreateApplyDocumentForAdavancePayment(clsCommon.myCDate(obj.Doc_Date), obj.ArrPPAdvancePayment, obj.ArrVSPPPDetail(i), obj.Loc_Seg_Code, trans)

                    If obj.ArrVSPPPDetail(i).Deduction_Amount > 0 Then
                        '' create adjustment entry
                        objPayAdj = New clsPaymentAdjustmentEntry
                        objPayAdj.Adjustment_No = ""
                        objPayAdj.Description = " AP Return Adjustment Against Bulk Payment Process for extra amount to be paid by VSP"
                        objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.To_Date)
                        objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                        objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                        objPayAdj.Doc_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).AP_Invoice_No)
                        objPayAdj.Doc_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Total_Invoice_Amount)
                        objPayAdj.Remarks = clsCommon.myCstr("")
                        objPayAdj.Adjust_Type = "R"
                        objPayAdj.Adjustment_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Deduction_Amount)
                        objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                        Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                        objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                        objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                        'objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                        'objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)

                        Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
                        If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                            Throw New Exception("Please set default VSP deduction code")
                        End If
                        If clsCommon.myLen(dtDed.Rows(0)("GL_Account_Code")) <= 0 Then
                            Throw New Exception("Please set gl Account for Deduction Code :" + clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code")))
                        End If
                        objTrPay.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code")), obj.Loc_Seg_Code, True, trans)
                        objTrPay.Account_Description = clsGLAccount.GetName(objTrPay.Account_No, trans)

                        objTrPay.Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Deduction_Amount)
                        objTrPay.Remarks = ""
                        objPayAdj.Arr.Add(objTrPay)
                        objPayAdj.SaveData(objPayAdj, True, trans)
                        clsPaymentAdjustmentEntry.FunPostReverseEntry(objPayAdj.Adjustment_No, trans)
                    End If


                    If obj.ArrVSPPPDetail(i).NextCycleDebitNote > 0 Then
                        createDebitNoteNextPaymentCycle(obj.ArrVSPPPDetail(i), obj.Loc_Seg_Code, trans)
                    End If

                    ReturnAdjAmt = 0
                    AdjAmt = 0
                    For Counter = 0 To obj.arrClsPaymentProcessMccSale.Count - 1
                        If clsCommon.CompairString(obj.arrClsPaymentProcessMccSale(Counter).Customer_CODE, obj.ArrVSPPPDetail(i).VSP_CODE) = CompairStringResult.Equal Then
                            AdjAmt = AdjAmt + (obj.arrClsPaymentProcessMccSale(Counter).Amount - obj.arrClsPaymentProcessMccSale(Counter).Reduce_Deduc_Amt)
                            Dim ReturnAmt As Double = 0
                            For ireturn As Integer = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                                If clsCommon.myCstr(obj.arrClsPaymentProcessMccSale(Counter).Sale_Doc_No) = clsCommon.myCstr(obj.arrClsPaymentProcessMccSaleReturn(ireturn).Sale_Doc_No) Then
                                    ReturnAmt += clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(ireturn).Amount)
                                    obj.arrClsPaymentProcessMccSaleReturn(ireturn).Amount = 0
                                End If
                            Next
                            AdjAmt -= ReturnAmt
                        End If
                    Next

                    If AdjAmt > 0 Then
                        'objPayAdj = New clsPaymentAdjustmentEntry
                        'objPayAdj.Adjustment_No = ""
                        'objPayAdj.Description = " AP Adjustment Against Bulk Payment Process "
                        'objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                        'objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                        'objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                        'objPayAdj.Doc_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).AP_Invoice_No)
                        'objPayAdj.Doc_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Total_Invoice_Amount)
                        'objPayAdj.Remarks = clsCommon.myCstr("")
                        'objPayAdj.Adjustment_Amount = clsCommon.myCdbl(AdjAmt)
                        'objPayAdj.Adjust_Type = "P"
                        'objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                        'Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                        'objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                        'objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                        'objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                        'objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                        'objTrPay.Amount = clsCommon.myCdbl(AdjAmt)
                        'objTrPay.Remarks = ""
                        'objPayAdj.Arr.Add(objTrPay)
                        'objPayAdj.SaveData(objPayAdj, True, trans)
                        'clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                    ElseIf AdjAmt < 0 Then
                        Dim CreditAjust As Decimal = Math.Abs(AdjAmt)
                        objPayAdj = New clsPaymentAdjustmentEntry
                        objPayAdj.Adjustment_No = ""
                        objPayAdj.Description = " AP Adjustment Against Credt note in Bulk Payment Process "
                        objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                        objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                        objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                        objPayAdj.Doc_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).AP_Invoice_No)
                        objPayAdj.Doc_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Total_Invoice_Amount)
                        objPayAdj.Remarks = clsCommon.myCstr("")
                        objPayAdj.Adjustment_Amount = clsCommon.myCdbl(CreditAjust)
                        objPayAdj.Adjust_Type = "R"
                        objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                        Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                        objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                        objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                        objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                        objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                        objTrPay.Amount = clsCommon.myCdbl(CreditAjust)
                        objTrPay.Remarks = ""
                        objPayAdj.Arr.Add(objTrPay)
                        objPayAdj.SaveData(objPayAdj, True, trans)
                        clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                    End If
                    'For Counter = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                    '    If clsCommon.CompairString(obj.arrClsPaymentProcessMccSaleReturn(Counter).Customer_CODE, obj.ArrVSPPPDetail(i).VSP_CODE) = CompairStringResult.Equal Then
                    '        ReturnAdjAmt = ReturnAdjAmt + (obj.arrClsPaymentProcessMccSaleReturn(Counter).Amount)
                    '    End If
                    'Next
                    'If ReturnAdjAmt > 0 Then
                    '    objPayAdj = New clsPaymentAdjustmentEntry
                    '    objPayAdj.Adjustment_No = ""
                    '    objPayAdj.Description = " AP Return Adjustment Against Bulk Payment Process "
                    '    objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                    '    objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                    '    objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                    '    objPayAdj.Doc_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).AP_Invoice_No)
                    '    objPayAdj.Doc_Amount = clsCommon.myCdbl(obj.ArrVSPPPDetail(i).Total_Invoice_Amount)
                    '    objPayAdj.Remarks = clsCommon.myCstr("")
                    '    objPayAdj.Adjust_Type = "R"
                    '    objPayAdj.Adjustment_Amount = clsCommon.myCdbl(ReturnAdjAmt)
                    '    objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                    '    Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                    '    objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                    '    objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                    '    objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                    '    objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                    '    objTrPay.Amount = clsCommon.myCdbl(ReturnAdjAmt)
                    '    objTrPay.Remarks = clsCommon.myCstr("")
                    '    objPayAdj.Arr.Add(objTrPay)
                    '    objPayAdj.SaveData(objPayAdj, True, trans)
                    '    clsPaymentAdjustmentEntry.FunPostReverseEntry(objPayAdj.Adjustment_No, trans)
                    'End If

                    'If obj.ArrVSPPPDetail(i).is_Hold_Payment_Process Then
                    '    Dim XTotalAmount As Decimal = 0
                    '    For Counter = 0 To obj.arrclsPaymentProcessFarmerDeductions.Count - 1
                    '        If clsCommon.CompairString(obj.arrclsPaymentProcessFarmerDeductions(Counter).Vendor_CODE, obj.ArrVSPPPDetail(i).VSP_CODE) = CompairStringResult.Equal Then
                    '            Dim XAmount As Decimal = obj.arrclsPaymentProcessFarmerDeductions(Counter).Amount - obj.arrclsPaymentProcessFarmerDeductions(Counter).Reduce_Deduc_Amt
                    '            If XAmount > 0 Then
                    '                XTotalAmount += XAmount
                    '                objPayAdj = New clsPaymentAdjustmentEntry
                    '                objPayAdj.Adjustment_No = "" ''To Be Generated
                    '                objPayAdj.Description = "AP Debit Note Adjustment Against Hold Process"
                    '                objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                    '                objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                    '                objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                    '                objPayAdj.Doc_No = clsCommon.myCstr(obj.arrclsPaymentProcessFarmerDeductions(Counter).AP_Invoice_No)
                    '                objPayAdj.Doc_Amount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Document_Total from TSPL_Vendor_Invoice_Head where Document_No='" + obj.arrclsPaymentProcessFarmerDeductions(Counter).AP_Invoice_No + "'", trans))
                    '                objPayAdj.Remarks = clsCommon.myCstr("")
                    '                objPayAdj.Adjustment_Amount = XAmount
                    '                objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                    '                Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                    '                objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                    '                objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                    '                objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                    '                objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                    '                objTrPay.Amount = XAmount
                    '                objTrPay.Remarks = clsCommon.myCstr("")
                    '                objPayAdj.Arr.Add(objTrPay)
                    '                objPayAdj.SaveData(objPayAdj, True, trans)
                    '                clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                    '            End If
                    '        End If
                    '    Next
                    '    If XTotalAmount > 0 Then
                    '        objPayAdj = New clsPaymentAdjustmentEntry
                    '        objPayAdj.Adjustment_No = "" ''To Be Generated
                    '        objPayAdj.Description = "AP Invoice Adjustment Against Hold Process"
                    '        objPayAdj.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                    '        objPayAdj.Vendor_No = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_CODE)
                    '        objPayAdj.Vendor_Name = clsCommon.myCstr(obj.ArrVSPPPDetail(i).VSP_NAME)
                    '        objPayAdj.Doc_No = obj.ArrVSPPPDetail(i).AP_Invoice_No
                    '        objPayAdj.Doc_Amount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Document_Total from TSPL_Vendor_Invoice_Head where Document_No='" + obj.ArrPPDetail(i).AP_Invoice_No + "'", trans))
                    '        objPayAdj.Remarks = clsCommon.myCstr("")
                    '        objPayAdj.Adjustment_Amount = XTotalAmount
                    '        objPayAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                    '        Dim objTrPay As New clsPaymentAdjustmentEntryDetail()
                    '        objTrPay.Discount_Code = clsCommon.myCstr(DisCCodeForArAdj)
                    '        objTrPay.Discount_Description = clsCommon.myCstr(DiscDiscForArAdj)
                    '        objTrPay.Account_No = clsCommon.myCstr(GLAcARAdj)
                    '        objTrPay.Account_Description = clsCommon.myCstr(GLAcDescARAdj)
                    '        objTrPay.Amount = XTotalAmount
                    '        objTrPay.Remarks = clsCommon.myCstr("")
                    '        objPayAdj.Arr.Add(objTrPay)
                    '        objPayAdj.SaveData(objPayAdj, True, trans)
                    '        clsPaymentAdjustmentEntry.FunPost(objPayAdj.Adjustment_No, trans)
                    '    End If
                    '    Continue For
                    'End If

                    ''Current cycle Debit note 
                    If obj.ArrVSPPPDetail.Item(i).Payable_Amount > 0 Then
                        Dim ARNote As String = String.Empty
                        Dim objVendInv As New clsVedorInvoiceHead()
                        objVendInv.Invoice_Entry_Date = clsCommon.myCDate(obj.Doc_Date)
                        objVendInv.Document_Type = "D"
                        ARNote = "Debit Note Against " + DocNo
                        objVendInv.RefDocNo = DocNo
                        objVendInv.RefDocType = "Farmer DN"
                        objVendInv.loc_code = obj.Loc_Seg_Code
                        'objVendInv.Document_Total = clsCommon.myCdbl(obj.ArrVSPPPDetail.Item(i).Total_Invoice_Amount - obj.ArrVSPPPDetail.Item(i).FarmerPayment - AdjAmt + ReturnAdjAmt + clsCommon.myCdbl(obj.ArrVSPPPDetail(i).NextCycleDebitNote) - clsCommon.myCdbl(obj.ArrVSPPPDetail(i).PrevCycleDebitNote))
                        objVendInv.Document_Total = clsCommon.myCdbl(obj.ArrVSPPPDetail.Item(i).Total_Invoice_Amount - obj.ArrVSPPPDetail.Item(i).FarmerPayment - AdjAmt + ReturnAdjAmt)
                        objVendInv.Vendor_Code = obj.ArrVSPPPDetail.Item(i).VSP_CODE
                        objVendInv.Invoice_Type = "AP"
                        objVendInv.Vendor_Name = obj.ArrVSPPPDetail.Item(i).VSP_NAME
                        objVendInv.Posting_Date = objVendInv.Invoice_Entry_Date
                        objVendInv.Vendor_Invoice_Date = objVendInv.Invoice_Entry_Date
                        objVendInv.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  ISNULL(Vendor_Account,'') AS [Vendor_Account] FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" + objVendInv.Vendor_Code + "'", trans))
                        If (clsCommon.myLen(objVendInv.Account_Set) < 0) Then
                            Throw New Exception("Please set the vendor account set for vendor : " + objVendInv.Vendor_Code)
                        End If

                        objVendInv.Vendor_Control_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payable_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans))
                        objVendInv.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Vendor_Control_AC, objVendInv.loc_code, True, trans)

                        If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                            Throw New Exception("Please set the vendor payable Account")
                        End If

                        objVendInv.On_Hold = 0
                        objVendInv.Remarks = "Auto Generated Debit Note Against VSP " & obj.ArrVSPPPDetail.Item(i).VSP_CODE & " and Farmer PP No. " & DocNo & ""
                        objVendInv.Description = "Auto Generated Debit Note Against VSP " & obj.ArrVSPPPDetail.Item(i).VSP_CODE & " and Farmer PP No. " & DocNo & ""
                        objVendInv.Balance_Amt = objVendInv.Document_Total
                        objVendInv.Amount_Less_Discount = objVendInv.Document_Total
                        objVendInv.Discount_Base = objVendInv.Document_Total
                        '=========================================================

                        objVendInv.Arr = New List(Of clsVedorInvoiceDetail)
                        '' Detail Level Saving
                        Dim VendAccSet As String = String.Empty
                        Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()
                        objVendInvTR.Detail_Line_No = 1
                        objVendInvTR.Amount = objVendInv.Document_Total
                        objVendInvTR.Amount_less_Discount = objVendInv.Document_Total
                        objVendInvTR.Total_Amount = objVendInv.Document_Total
                        objVendInvTR.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Profit_Loss_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans))
                        objVendInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInvTR.GL_Account_Code, objVendInv.loc_code, True, trans)
                        If clsCommon.myLen(objVendInvTR.GL_Account_Code) <= 0 Then
                            Throw New Exception("Please set the vendor Profit Loss Account")
                        End If
                        objVendInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & objVendInvTR.GL_Account_Code & "'", trans))
                        objVendInv.Arr.Add(objVendInvTR)
                        If objVendInv.Document_Total > 0 Then
                            objVendInv.SaveData(objVendInv, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
                        End If
                        '' end code
                    End If
                Next


                ''''==============Add Code for save Mcc Sale Return Document Payment Details==================
                ''If obj.arrClsPaymentProcessMccSaleReturn IsNot Nothing AndAlso obj.arrClsPaymentProcessMccSaleReturn.Count > 0 Then
                ''    DisCCodeForArAdj = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForArAdj, clsFixedParameterCode.DiscountCodeForArAdj, trans)
                ''    If clsCommon.myLen(DisCCodeForArAdj) <= 0 Then
                ''        Throw New Exception("Please Map Discount code from Sale setting")
                ''    End If
                ''    DiscDiscForArAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))
                ''    GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Code from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans)), obj.Loc_Seg_Code, True, trans)
                ''    GLAcDescARAdj = clsGLAccount.GetName(GLAcARAdj, trans)
                ''    'GLAcDescARAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))
                ''    For i = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                ''        clsCommon.ProgressBarPercentUpdate(i * 100 / obj.arrClsPaymentProcessMccSaleReturn.Count, "Updating AR Adjustment Record " & (i + 1) & " Of " & obj.arrClsPaymentProcessMccSaleReturn.Count)
                ''        objRcpt = New clsAdjustmentEntryReceivables
                ''        objRcpt.Adjustment_No = ""
                ''        objRcpt.Description = " Return Adjustment Against Bulk Payment Process "
                ''        objRcpt.Adjustment_Date = clsCommon.myCDate(obj.Doc_Date)
                ''        objRcpt.Customer_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cust_Code  from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" & obj.arrClsPaymentProcessMccSaleReturn(i).Customer_CODE & "' ", trans))
                ''        objRcpt.Customer_Name = clsCommon.myCstr(clsCustomerMaster.GetName(objRcpt.Customer_No, trans))
                ''        objRcpt.Doc_No = clsCommon.myCstr(obj.arrClsPaymentProcessMccSaleReturn(i).Sale_Doc_No)
                ''        objRcpt.ARInvoiceNo = clsCommon.myCstr(obj.arrClsPaymentProcessMccSaleReturn(i).AR_Invoice_No)
                ''        objRcpt.Doc_Amount = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(i).Amount)
                ''        objRcpt.Remarks = ""
                ''        objRcpt.Adjustment_Amount = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(i).Amount)
                ''        objRcpt.Arr = New List(Of clsAdjustmentEntryReceivablesDetail)
                ''        Dim objTrRcpt As New clsAdjustmentEntryReceivablesDetail()
                ''        objTrRcpt.Discount_Code = DisCCodeForArAdj
                ''        objTrRcpt.Discount_Description = DiscDiscForArAdj
                ''        objTrRcpt.Account_No = GLAcARAdj
                ''        objTrRcpt.Account_Description = GLAcDescARAdj
                ''        Dim ReturnAmt As Double = 0
                ''        objTrRcpt.Amount = clsCommon.myCdbl(clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn(i).Amount))
                ''        objTrRcpt.Remarks = ""
                ''        objRcpt.Arr.Add(objTrRcpt)
                ''        If clsCommon.myCdbl(objTrRcpt.Amount) > 0 Then
                ''            objRcpt.SaveData(objRcpt, True, trans)
                ''            clsAdjustmentEntryReceivables.FunPostReverseEntry(objRcpt.Adjustment_No, trans)
                ''        End If
                ''    Next
                ''End If


                '''' Farmer Payment 
                ''DisCCodeForArAdj = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForMPAdj, clsFixedParameterCode.DiscountCodeForMPAdj, trans)
                ''If clsCommon.myLen(DisCCodeForArAdj) <= 0 Then
                ''    Throw New Exception("Please Map Discount code for Farmer/MP in Fixed Parameter.")
                ''End If
                ''DiscDiscForArAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))
                ''GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Code from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans)), obj.Loc_Seg_Code, True, trans)
                ''GLAcDescARAdj = clsGLAccount.GetName(GLAcARAdj, trans)
                ''''GLAcDescARAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", trans))


                ''''Get Raw Item Inventory Control Account
                ''Dim strICode = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
                ''If clsCommon.myLen(strICode) <= 0 Then
                ''    Throw New Exception("Please select MCC Defualt Milk item")
                ''End If
                ''Dim qry As String = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit,TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account,TSPL_PURCHASE_ACCOUNTS.RM_Consumption  from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                ''Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                ''If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                ''    Throw New Exception("Please set Purchase Account set for item " + strICode + " ")
                ''End If
                ''''1)
                ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + strICode + "")
                ''End If
                ''strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, obj.Loc_Seg_Code, True, trans)
                ''''Get End of Raw Item Inventory Control Account

                ''Dim ArryDeductionGLAC As ArrayList = New ArrayList()
                ''Dim ArryIncentiveGLAC As ArrayList = New ArrayList()
                '''' mp payment start here
                ''If obj.ArrPPDetail IsNot Nothing And obj.ArrPPDetail.Count > 0 Then
                ''    For Each objVSP As clsPaymentProcessDetail In obj.ArrVSPPPDetail
                ''        Dim objFAdj As New clsFarmerPaymentAdjustmentEntry
                ''        Dim objAdj As New clsPaymentAdjustmentEntry
                ''        For i = 0 To obj.ArrPPDetail.Count - 1
                ''            If clsCommon.CompairString(objVSP.AP_Invoice_No, obj.ArrPPDetail.Item(i).AP_Invoice_No) = CompairStringResult.Equal Then
                ''                '' save payment adjustment entry against vsp invoice
                ''                objAdj.Adjustment_No = obj.ArrPPDetail.Item(i).AP_Adjustment_No
                ''                clsCommon.ProgressBarPercentUpdate(i * 100 / obj.ArrPPDetail.Count, " Creating Payment Entry  Record " & (i + 1) & " Of " & obj.ArrPPDetail.Count)
                ''                If Not obj.ArrPPDetail(i).Is_select Then
                ''                    Continue For
                ''                End If

                ''                ''Create JE For Deduction,Incentive By Balwinder on 20/02/2020 
                ''                If obj.ArrPPDetail(i).Deduction_Amount > 0 Then
                ''                    Dim AccDr() As String = {GLAcARAdj, Math.Round(((obj.ArrPPDetail(i).Deduction_Amount)), 2, MidpointRounding.ToEven)}
                ''                    ArryDeductionGLAC.Add(AccDr)

                ''                    Dim AccCr() As String = {strInvCtrlAC, -1 * Math.Round(((obj.ArrPPDetail(i).Deduction_Amount)), 2, MidpointRounding.ToEven)}
                ''                    ArryDeductionGLAC.Add(AccCr)
                ''                End If

                ''                If obj.ArrPPDetail(i).Incentive_Amount > 0 Then
                ''                    qry = "select Incentive_Account from TSPL_MP_MASTER where MP_Code='" + obj.ArrPPDetail(i).Farmer_Code + "'"
                ''                    qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                ''                    If clsCommon.myLen(qry) <= 0 Then
                ''                        Throw New Exception("Please set Incentive Account form MP " + clsCommon.myCstr(obj.ArrPPDetail(i).Farmer_Code))
                ''                    End If
                ''                    qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Loc_Seg_Code, True, trans)
                ''                    Dim AccDr() As String = {qry, Math.Round(((obj.ArrPPDetail(i).Incentive_Amount)), 2, MidpointRounding.ToEven)}
                ''                    ArryIncentiveGLAC.Add(AccDr)

                ''                    Dim AccCr() As String = {GLAcARAdj, -1 * Math.Round(((obj.ArrPPDetail(i).Incentive_Amount)), 2, MidpointRounding.ToEven)}
                ''                    ArryIncentiveGLAC.Add(AccCr)
                ''                End If

                ''                ''End of Create JE For Deduction,Incentive

                ''                ReturnAdjAmt = 0
                ''                AdjAmt = 0
                ''                Dim MCCSaleDocs As String = ""
                ''                Dim MCCSaleRetDocs As String = ""
                ''                For Counter = 0 To obj.arrclsPaymentProcessFarmerMccSale.Count - 1
                ''                    If clsCommon.CompairString(obj.arrclsPaymentProcessFarmerMccSale(Counter).Farmer_Code, obj.ArrPPDetail(i).Farmer_Code) = CompairStringResult.Equal Then
                ''                        AdjAmt = AdjAmt + (obj.arrclsPaymentProcessFarmerMccSale(Counter).Amount - obj.arrclsPaymentProcessFarmerMccSale(Counter).Reduce_Deduc_Amt)
                ''                        If clsCommon.myLen(MCCSaleDocs) <= 0 Then
                ''                            MCCSaleDocs = obj.arrclsPaymentProcessFarmerMccSale(Counter).Sale_Doc_No
                ''                        Else
                ''                            MCCSaleDocs = MCCSaleDocs & "," & obj.arrclsPaymentProcessFarmerMccSale(Counter).Sale_Doc_No
                ''                        End If
                ''                        Dim ReturnAmt As Double = 0
                ''                        For ireturn As Integer = 0 To obj.arrclsPaymentProcessFarmerMccSaleReturn.Count - 1
                ''                            If clsCommon.myCstr(obj.arrclsPaymentProcessFarmerMccSale(Counter).Sale_Doc_No) = clsCommon.myCstr(obj.arrclsPaymentProcessFarmerMccSaleReturn(ireturn).Sale_Doc_No) Then
                ''                                ReturnAmt += clsCommon.myCdbl(obj.arrclsPaymentProcessFarmerMccSaleReturn(ireturn).Amount)
                ''                                obj.arrclsPaymentProcessFarmerMccSaleReturn(ireturn).Amount = 0
                ''                            End If
                ''                        Next
                ''                        AdjAmt -= ReturnAmt
                ''                    End If
                ''                Next
                ''                '' commented by panch raj because no need of adjustment entry . farmer ledger reuired to sho mcc sale , returns and adjustents docs directly. Only payment doc will be created here
                ''                ' '' check for negative net amount
                ''                If obj.ArrPPDetail(i).NextCycleDebitNoteMP > 0 Then
                ''                    '' commented because extra entry was creatig
                ''                    ' '' create adjustment entry for next payment cycle
                ''                    Dim NextPCAdjustAmt As Decimal = obj.ArrPPDetail(i).NextCycleDebitNoteMP
                ''                    If NextPCAdjustAmt > 0 Then
                ''                        objFAdj = New clsFarmerPaymentAdjustmentEntry
                ''                        objFAdj.Adjustment_No = ""
                ''                        objFAdj.Description = "Adjstment amount for next payment cycle->Payment Process doc No: " & DocNo & ",Farmer Invoice No: " & obj.ArrPPDetail(i).Farmer_Invoice_No & " "
                ''                        objFAdj.Adjustment_Date = obj.ArrPPDetail(i).AP_Invoice_Date.AddDays(1)
                ''                        objFAdj.Farmer_Code = clsCommon.myCstr(obj.ArrPPDetail(i).Farmer_Code)
                ''                        objFAdj.Farmer_Name = clsCommon.myCstr(obj.ArrPPDetail(i).Farmer_Name)
                ''                        objFAdj.Doc_No = ""
                ''                        objFAdj.Doc_Amount = 0
                ''                        objFAdj.Remarks = "MCC Farmer sale Documents:" & MCCSaleDocs
                ''                        objFAdj.Adjustment_Amount = NextPCAdjustAmt
                ''                        objFAdj.Adjustment_Type = "Payment"
                ''                        objFAdj.Arr = New List(Of clsFarmerPaymentAdjustmentEntryDetail)
                ''                        Dim objTrPay As New clsFarmerPaymentAdjustmentEntryDetail()
                ''                        objTrPay.Discount_Code = DisCCodeForArAdj
                ''                        objTrPay.Discount_Description = DiscDiscForArAdj
                ''                        objTrPay.Account_No = GLAcARAdj
                ''                        objTrPay.Account_Description = GLAcDescARAdj
                ''                        objTrPay.Amount = NextPCAdjustAmt
                ''                        objTrPay.Remarks = "Advance sale amount to be adjusted in next payment cycle "
                ''                        objFAdj.Arr.Add(objTrPay)
                ''                        objFAdj.SaveData(objFAdj, True, trans)
                ''                        clsFarmerPaymentAdjustmentEntry.FunPost(objFAdj.Adjustment_No, trans)
                ''                        ''change by Panch Raj on 01-may-2018 against ticket : KDI/30/04/18-000281
                ''                        '' adjustment entry to krockup farmer invoice on farmer invoice date
                ''                        ''1. date 2 remark/description 3. against farmer invoice no
                ''                        objFAdj.Adjustment_No = ""
                ''                        objFAdj.Description = "Invoice Setoff ->Payment Process doc No: " & DocNo & ",Farmer Invoice No: " & obj.ArrPPDetail(i).Farmer_Invoice_No & " "
                ''                        objFAdj.Adjustment_Date = obj.ArrPPDetail(i).AP_Invoice_Date
                ''                        objFAdj.Doc_No = obj.ArrPPDetail(i).Farmer_Invoice_No
                ''                        objFAdj.Adjustment_Type = "Invoice"
                ''                        objFAdj.SaveData(objFAdj, True, trans)
                ''                        clsFarmerPaymentAdjustmentEntry.FunPost(objFAdj.Adjustment_No, trans)
                ''                    End If
                ''                End If

                ''                ''------------------Payment Entry Start Here
                ''                If obj.ArrPPDetail(i).Payable_Amount > 0 Then
                ''                    Dim objPayF As clsFarmerPaymentHeader
                ''                    objPayF = New clsFarmerPaymentHeader
                ''                    objPayF.Payment_Process_Code = obj.Doc_No
                ''                    objPayF.Against_PP_Detail_No = obj.ArrPPDetail(i).Farmer_Invoice_No
                ''                    objPayF.Payment_No = ""
                ''                    objPayF.Entry_Desc = Desc + " " + DocNo
                ''                    objPayF.Payment_Date = clsCommon.myCDate(obj.Doc_Date)
                ''                    objPayF.Payment_Post_Date = clsCommon.myCDate(obj.Doc_Date)
                ''                    objPayF.Bank_Code = obj.ArrPPDetail.Item(i).Bank_Code
                ''                    objPayF.Payment_Type = "PY"
                ''                    objPayF.Farmer_Code = obj.ArrPPDetail.Item(i).Farmer_Code
                ''                    objPayF.Farmer_Name = obj.ArrPPDetail.Item(i).Farmer_Name
                ''                    objPayF.Payment_Code = obj.ArrPPDetail.Item(i).Payment_Mode
                ''                    objPayF.Cheque_No = obj.ArrPPDetail.Item(i).Cheque_No
                ''                    If Not obj.ArrPPDetail.Item(i).Cheque_Dated Is Nothing Then
                ''                        objPayF.Cheque_Date = obj.ArrPPDetail.Item(i).Cheque_Dated
                ''                    End If

                ''                    objPayF.Account_Payee = 0
                ''                    objPayF.memorndmamt = "0"
                ''                    objPayF.Applied_Payment = clsCommon.myCstr(obj.ArrPPDetail.Item(i).Farmer_Invoice_No)
                ''                    objPayF.Is_Security = 0
                ''                    objPayF.IsChkReverse = "N"
                ''                    objPayF.Bank_Charges = 0

                ''                    Dim objTrF As clsFarmerPaymentDetail
                ''                    objPayF.ArrTr = New List(Of clsFarmerPaymentDetail)
                ''                    objTrF = New clsFarmerPaymentDetail()
                ''                    objTrF.Apply = "1"
                ''                    objTrF.Payment_Type = "PY"
                ''                    objTrF.Document_No = clsCommon.myCstr(obj.ArrPPDetail.Item(i).Farmer_Invoice_No)
                ''                    objTrF.Original_Invoice_Amt = clsCommon.myCdbl(obj.ArrPPDetail.Item(i).MCC_Sale_Amount)
                ''                    objTrF.Applied_Amount = clsCommon.myCdbl(obj.ArrPPDetail.Item(i).MCC_Sale_Amount) - AdjAmt + ReturnAdjAmt
                ''                    objTrF.Pending_Balance = 0
                ''                    objTrF.Vendor_Invoice_No = ""
                ''                    objTrF.Net_Balance = 0
                ''                    objTrF.Security_Amount = 0
                ''                    objPayF.ArrTr.Add(objTrF)


                ''                    objPayF.Payment_Amount = obj.ArrPPDetail(i).Payable_Amount
                ''                    objPayF.Balance_Amt = obj.ArrPPDetail(i).Payable_Amount
                ''                    objPayF.Location_Code = clsCommon.myCstr(obj.Loc_Seg_Code)
                ''                    objPayF.Entry_Desc = obj.PaymentDesc + " " + DocNo

                ''                    objPayF.SaveData1(objPayF, True, trans)
                ''                    clsFarmerPaymentHeader.PostData(objPayF.Payment_No, "Payable", trans)
                ''                End If


                ''            End If

                ''            '' end of create bank transfer entry
                ''            ''------------------Payment Entry End Here
                ''        Next
                ''        '' posting payment adjustment entry
                ''        clsPaymentAdjustmentEntry.FunPost(objAdj.Adjustment_No, trans)
                ''    Next

                ''    '' map Farmer Adjustment to invoice 
                ''    For Each objAdj As clsPaymentProcessAdjustment In obj.ArrPPAdjustment
                ''        Dim qryAdj As String = "update TSPL_MP_PAY_ADJ_HEAD set Is_Processed='Y' where Adjustment_No='" & objAdj.Adjustment_No & "'"
                ''        clsDBFuncationality.ExecuteNonQuery(qryAdj, trans)
                ''    Next
                ''    Dim qryB As String = " select TSPL_MP_PAY_HEAD.Payment_Date,CBank.BANK_CODE as From_Bank_Code,CBank.DESCRIPTION as From_Bank_Name," &
                ''                        " CBank.BANKACC as From_Bank_Acc_No,TSPL_MP_PAY_HEAD.Bank_Code as To_Bank_Code," &
                ''                        " bank.DESCRIPTION as To_Bank_Name,Bank.BANKACC as To_Bank_Acc_No,Bank.BANKACCNUMBER as TO_BANKACCNUMBER," &
                ''                        " sum(TSPL_MP_PAY_HEAD.Payment_Amount) as Transfer_Amount,sum(TSPL_MP_PAY_HEAD.Payment_Amount)as Deposit_Amount from TSPL_MP_PAY_HEAD  " &
                ''                        " inner join TSPL_MP_PAY_DETAIL on TSPL_MP_PAY_HEAD.Payment_No=TSPL_MP_PAY_DETAIL.Payment_No " &
                ''                        " left join TSPL_BANK_MASTER Bank on Bank.BANK_CODE=TSPL_MP_PAY_HEAD.Bank_Code " &
                ''                        " left join TSPL_BANK_MASTER CBank on CBank.BANK_CODE=Bank.Main_Bank_Code " &
                ''                        " where TSPL_MP_PAY_HEAD.PAYMENT_PROCESS_CODE='" & obj.Doc_No & "' " &
                ''                        " group by TSPL_MP_PAY_HEAD.Payment_Date,CBank.BANK_CODE,CBank.DESCRIPTION, " &
                ''                        " CBank.BANKACC,TSPL_MP_PAY_HEAD.Bank_Code,bank.DESCRIPTION,Bank.BANKACC,Bank.BANKACCNUMBER"
                ''    Dim dtB As DataTable = clsDBFuncationality.GetDataTable(qryB, trans)
                ''    If dtB.Rows.Count > 0 Then
                ''        For Each drB As DataRow In dtB.Rows
                ''            Dim objB As New clsBankTrasnferNew
                ''            objB.Deposit_Amount = clsCommon.myCdbl(drB.Item("Deposit_Amount"))
                ''            objB.Transfer_Amount = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                ''            objB.Description = "Bank Transfer against Farmer Payment Process-" & obj.Doc_No & ""
                ''            If clsCommon.myLen(drB.Item("From_Bank_Acc_No")) <= 0 Then
                ''                Throw New Exception("From Bank Acc No is not set for Bank -" & objB.From_Bank_Code & ".")
                ''            End If
                ''            If clsCommon.myLen(drB.Item("To_Bank_Acc_No")) <= 0 Then
                ''                Throw New Exception("To Bank Acc No is not set for Bank - " & objB.To_Bank_Code & ".")
                ''            End If
                ''            objB.From_Bank_Acc_No = clsCommon.myCstr(drB.Item("From_Bank_Acc_No"))
                ''            objB.From_Bank_Code = clsCommon.myCstr(drB.Item("From_Bank_Code"))
                ''            objB.From_Bank_Name = clsCommon.myCstr(drB.Item("From_Bank_Name"))
                ''            objB.Payment_Mode = "TRANSFER"
                ''            objB.Reference = "Bank Transfer against Farmer Payment Process-" & obj.Doc_No & ""
                ''            objB.To_Bank_Acc_No = clsCommon.myCstr(drB.Item("To_Bank_Acc_No"))
                ''            objB.To_Bank_Code = clsCommon.myCstr(drB.Item("To_Bank_Code"))
                ''            objB.To_Bank_Name = clsCommon.myCstr(drB.Item("To_Bank_Name"))
                ''            objB.Transaction_Type = "B"
                ''            objB.Transfer_Date = clsCommon.myCDate(drB.Item("Payment_Date"))
                ''            objB.Cheque_No = ""
                ''            objB.Cheque_Date = clsCommon.myCDate(drB.Item("Payment_Date"))
                ''            objB.Transfer_No = ""

                ''            ''2. check balance 
                ''            ''CheckNegativeBankBalance
                ''            clsBankTrasnferNew.CheckNegativeBankBalance(objB, trans)
                ''            clsBankTrasnferNew.SaveData(True, objB, trans)
                ''            clsBankTrasnferNew.PostData(objB.Transfer_No, trans)
                ''            objB = Nothing
                ''            '' create miscellaneous payment entry to transfer amount from bank to gl accounts(control account of farmers)
                ''            Dim objPayMI As New clsPaymentHeader
                ''            objPayMI.Payment_No = ""
                ''            objPayMI.Entry_Desc = "Misc Payment entry against Payment Process Farmer-" & DocNo & ""
                ''            objPayMI.Bank_Code = clsCommon.myCstr(drB.Item("To_Bank_Code"))
                ''            objPayMI.Payment_Type = "MI"
                ''            objPayMI.Bank_Charges = 0
                ''            objPayMI.Payment_Code = "TRANSFER"
                ''            objPayMI.Payment_Amount = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                ''            objPayMI.Total_Applied_Amount = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                ''            objPayMI.Applied_Payment = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                ''            objPayMI.CURRENCY_CODE = "INR"
                ''            objPayMI.ConvRate = 1
                ''            objPayMI.ConvRateOld = 1
                ''            objPayMI.BASE_CURRENCY_CODE = "INR"
                ''            objPayMI.Payment_Date = clsCommon.myCDate(drB.Item("Payment_Date"))
                ''            objPayMI.ArrTr = New List(Of clsPaymentDetail)
                ''            '' add account and amount detail
                ''            Dim objPayMITr As New clsPaymentDetail

                ''            'Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + objPayMI.Bank_Code + "'", trans)
                ''            'GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(GLAcARAdj, BankLocation, True, trans)
                ''            'By Balwinder on 22/02/2020
                ''            GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(GLAcARAdj, obj.Loc_Seg_Code, True, trans)
                ''            objPayMITr.Account_Code = GLAcARAdj
                ''            objPayMITr.Description = clsGLAccount.GetName(GLAcARAdj, trans)
                ''            objPayMITr.Applied_Amount = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                ''            objPayMITr.Payment_Type = objPayMI.Payment_Type
                ''            objPayMITr.Net_Balance = clsCommon.myCdbl(drB.Item("Transfer_Amount"))
                ''            objPayMITr.Remarks = "transfer amount from clearing Bank to control account of Farmer"
                ''            objPayMITr.ESI_WCT_Percentage = 0

                ''            objPayMITr.Hirerachy_Level_Code = ""
                ''            objPayMITr.Cost_Center_Fin_Code = ""
                ''            objPayMI.ArrTr.Add(objPayMITr)
                ''            objPayMI.SaveData1(objPayMI, True, trans)
                ''            clsPaymentHeader.PostData(objPayMI.Payment_No, "Payable", trans)
                ''            '' update Misc Payment No in farmer payment entry against ticket no: KDI/17/05/18-000318 point no-3
                ''            qryB = "update TSPL_MP_PAY_HEAD set Misc_Payment_No='" & objPayMI.Payment_No & "' where Payment_Process_Code='" & obj.Doc_No & "' and Bank_Code='" & objPayMI.Bank_Code & "'"
                ''            clsDBFuncationality.ExecuteNonQuery(qryB, trans)
                ''            objPayMI = Nothing
                ''        Next

                ''    End If
                ''End If
                ''If ArryDeductionGLAC.Count > 0 Then
                ''    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Seg_Code, True, trans, obj.Doc_Date, "Total Deduction Against Farmer Payment Process No ( " & obj.Doc_No & ")", "MP-DE", "MP Deduction", obj.Doc_No, "", "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryDeductionGLAC)
                ''End If
                ''If ArryIncentiveGLAC.Count > 0 Then
                ''    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Seg_Code, True, trans, obj.Doc_Date, "Total Incetive Against Farmer Payment Process No ( " & obj.Doc_No & ")", "MP-IV", "MP Incetinve", obj.Doc_No, "", "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryIncentiveGLAC)
                ''End If


                '===========================================================================================================  
                Dim qry As String = " update TSPL_PAYMENT_PROCESS_HEAD set isPosted=1, Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where doc_no='" & obj.Doc_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Doc_No, "TSPL_PAYMENT_PROCESS_HEAD", "Doc_No", "TSPL_PAYMENT_PROCESS_DETAIL", "Doc_No", "TSPL_MP_PAY_PROCESS_DETAIL", "Doc_No", trans)
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
        Return True
    End Function
End Class

Public Class clsPaymentProcessFarmerPaymentDetail
#Region "Variales"
    Public Farmer_Invoice_No As String = ""
    Public Farmer_Invoice_Date As DateTime
    Public Doc_No As String = ""
    Public Is_select As Boolean = False
    Public SNo As String = ""
    'Public Milk_Purchase_Invoice_No As String = ""

    Public AP_Invoice_No As String = ""
    Public AP_Invoice_Date As DateTime
    Public AP_Adjustment_No As String = ""
    Public AP_Adjustment_Date As DateTime
    Public VLC_CODE_Uploader As String = ""
    Public VLC_Name As String = ""
    Public VSP_CODE As String = ""
    Public VSP_NAME As String = ""
    Public Farmer_Code As String = ""
    Public Farmer_Name As String = ""
    Public Payee_Joint_Name As String = ""
    Public Payee_Joint_Bank_Code As String = ""
    Public Payee_Joint_Bank_Name As String = ""
    Public Payee_Joint_Branch_Code As String = ""
    Public Payee_Joint_Branch_Name As String = ""
    Public Payee_Joint_Account_No As String = ""
    Public Payee_Joint_IFSC_Code As String = ""
    Public Bank_Code As String = ""
    Public Bank_Desc As String = ""
    Public Payment_Mode As String = ""
    Public Cheque_No As String = ""
    Public Milk_Qty As Double = 0
    Public Milk_Amount As Double = 0

    Public MCC_Sale_Amount As Double = 0
    Public MCC_Sale_Return_Amount As Double = 0
    Public MP_Adjust_Amount As Double = 0

    Public Incentive_Amount As Decimal = 0
    Public Deduction_Amount As Decimal = 0

    Public Payable_Amount As Double = 0

    Public Cheque_Dated As Date? = Nothing

    Public is_Hold_Payment_Process As Boolean = False
    Public PrevCycleDebitNoteMP As Decimal = 0
    Public NextCycleDebitNoteMP As Decimal = 0
    Public Total_Advance_Amount As Decimal = 0
    Public Total_Advance_Amount_Recovery As Decimal = 0
    Public Total_Loan_Payment As Decimal = 0
    Public Total_Loan_Payment_Recovery As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal strLocSegCode As String, ByVal arr As List(Of clsPaymentProcessFarmerPaymentDetail), ByVal tran As SqlTransaction) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            Dim DisCCodeForArAdj As String = ""
            Dim DiscDiscForArAdj As String = ""
            Dim GLAcARAdj As String = ""
            Dim GLAcDescARAdj As String = ""

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                DisCCodeForArAdj = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForMPAdj, clsFixedParameterCode.DiscountCodeForMPAdj, tran)
                If clsCommon.myLen(DisCCodeForArAdj) <= 0 Then
                    Throw New Exception("Please Map Discount code for Farmer/MP in Fixed Parameter")
                End If
                DiscDiscForArAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", tran))
                GLAcARAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Code from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", tran))
                If clsCommon.myLen(GLAcARAdj) <= 0 Then
                    Throw New Exception("Create Discount Master with code '" & DisCCodeForArAdj & "' ")
                End If
                GLAcARAdj = clsERPFuncationality.ChangeGLAccountLocationSegment(GLAcARAdj, strLocSegCode, True, tran)

                'GLAcDescARAdj = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Account_Description from TSPL_Discount_Master WHERE Code='" & DisCCodeForArAdj & "'", tran))
                GLAcDescARAdj = clsGLAccount.GetName(GLAcARAdj, tran)

                Dim objVSPList As New List(Of clsPaymentProcessDetail)
                objVSPList = clsPaymentProcessDetail.getData(DocNo, tran)


                For Each objVSP As clsPaymentProcessDetail In objVSPList
                    Dim objAdj As New clsPaymentAdjustmentEntry
                    objAdj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                    Dim Adjarr As New List(Of clsPaymentAdjustmentEntryDetail)
                    'objAdj.Adjustment_Amount = objVSP.MP_Amount
                    'Dim totalMilkAmt As Decimal = 0
                    Dim isFound As Boolean = False
                    For i = 0 To arr.Count - 1
                        If clsCommon.CompairString(objVSP.AP_Invoice_No, arr(i).AP_Invoice_No) = CompairStringResult.Equal Then
                            isFound = True
                            ' '' save payment adjustment entry against vsp invoice
                            objAdj.Adjustment_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Adjustment_No from TSPL_Payment_Adjustment_Header where Doc_No='" & arr.Item(i).AP_Invoice_No & "'", tran))
                            objAdj.Adjustment_Amount = objAdj.Adjustment_Amount + clsCommon.myCdbl(arr.Item(i).Milk_Amount) 'Math.Round(clsCommon.myCdbl(arr.Item(i).Milk_Amount), 2)
                            objAdj.Bal_Amount = objAdj.Adjustment_Amount
                            objAdj.Adjustment_Date = clsCommon.GetPrintDate(arr.Item(i).Farmer_Invoice_Date, "dd/MMM/yyyy")
                            objAdj.Description = "Auto Payment Adjustment entry(Direct payment to Farmer) against AP Invoice-" & arr.Item(i).AP_Invoice_No & " for VSP-" & arr.Item(i).VSP_CODE & " against Payment Process-" & DocNo & ""
                            objAdj.Doc_Amount = objAdj.Doc_Amount + clsCommon.myCdbl(arr.Item(i).Milk_Amount)
                            objAdj.Is_Post = "N"
                            objAdj.Remarks = "Direct Payment to Farmer"
                            objAdj.Vendor_No = clsCommon.myCstr(arr.Item(i).VSP_CODE)
                            objAdj.Vendor_Name = clsCommon.myCstr(arr.Item(i).VSP_NAME)
                            objAdj.Doc_No = clsCommon.myCstr(arr.Item(i).AP_Invoice_No)

                            Dim objarr As New clsPaymentAdjustmentEntryDetail
                            objarr.Adjustment_No = objAdj.Adjustment_No
                            objarr.Discount_Code = DisCCodeForArAdj
                            objarr.Discount_Description = DiscDiscForArAdj
                            objarr.Account_No = GLAcARAdj
                            objarr.Account_Description = GLAcDescARAdj

                            objarr.Amount = clsCommon.myCdbl(arr.Item(i).Milk_Amount) 'Math.Round(clsCommon.myCdbl(arr.Item(i).Milk_Amount), 2)

                            objarr.FarmerCode = clsCommon.myCstr(arr.Item(i).Farmer_Code)
                            objarr.FarmerName = clsCommon.myCstr(arr.Item(i).Farmer_Name)
                            objarr.Line_No = objAdj.Arr.Count + 1
                            objarr.Remarks = "Direct payment to farmer-" & objarr.FarmerCode & ", Name-" & objarr.FarmerName & " against VSP Invoice by creating payment adjustment."
                            Adjarr.Add(objarr)
                        End If
                    Next
                    If isFound Then
                        If clsCommon.myLen(objAdj.Adjustment_No) > 0 Then
                            clsPaymentAdjustmentEntry.ReverseAndUnpost(objAdj.Adjustment_No, tran, True)
                        End If
                        objAdj.Arr = Adjarr
                        objAdj.SaveData(objAdj, IIf(clsCommon.myLen(objAdj.Adjustment_No) > 0, False, True), tran)
                        clsPaymentAdjustmentEntry.FunPost(objAdj.Adjustment_No, tran)
                    End If

                    For i = 0 To arr.Count - 1
                        If clsCommon.CompairString(objVSP.AP_Invoice_No, arr(i).AP_Invoice_No) = CompairStringResult.Equal Then
                            arr.Item(i).AP_Adjustment_No = objAdj.Adjustment_No
                            arr.Item(i).AP_Adjustment_Date = objAdj.Adjustment_Date

                            Dim coll As New Hashtable()

                            clsCommon.AddColumnsForChange(coll, "Farmer_Invoice_No", arr.Item(i).Farmer_Invoice_No)
                            clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                            clsCommon.AddColumnsForChange(coll, "Is_select", IIf(arr.Item(i).Is_select, 1, 0))
                            clsCommon.AddColumnsForChange(coll, "SNo", i + 1)
                            If clsCommon.myLen(arr.Item(i).AP_Adjustment_No) > 0 Then
                                clsCommon.AddColumnsForChange(coll, "AP_Adjustment_No", arr.Item(i).AP_Adjustment_No)
                                clsCommon.AddColumnsForChange(coll, "AP_Adjustment_Date", clsCommon.GetPrintDate(arr.Item(i).AP_Adjustment_Date, "dd/MMM/yyyy"))
                            End If
                            clsCommon.AddColumnsForChange(coll, "Farmer_Invoice_Date", clsCommon.GetPrintDate(arr.Item(i).Farmer_Invoice_Date, "dd/MMM/yyyy hh:mm tt"))
                            clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", arr.Item(i).AP_Invoice_No)
                            clsCommon.AddColumnsForChange(coll, "AP_Invoice_Date", clsCommon.GetPrintDate(arr.Item(i).AP_Invoice_Date, "dd/MMM/yyyy hh:mm tt"))
                            clsCommon.AddColumnsForChange(coll, "VLC_CODE_Uploader", arr.Item(i).VLC_CODE_Uploader)
                            clsCommon.AddColumnsForChange(coll, "VLC_Name", arr.Item(i).VLC_Name)
                            clsCommon.AddColumnsForChange(coll, "VSP_CODE", arr.Item(i).VSP_CODE)
                            clsCommon.AddColumnsForChange(coll, "VSP_NAME", arr.Item(i).VSP_NAME)
                            clsCommon.AddColumnsForChange(coll, "Farmer_Code", arr.Item(i).Farmer_Code)
                            clsCommon.AddColumnsForChange(coll, "Farmer_Name", arr.Item(i).Farmer_Name)
                            clsCommon.AddColumnsForChange(coll, "Payee_Joint_Name", arr.Item(i).Payee_Joint_Name)
                            clsCommon.AddColumnsForChange(coll, "Payee_Joint_Bank_Code", arr.Item(i).Payee_Joint_Bank_Code)
                            clsCommon.AddColumnsForChange(coll, "Payee_Joint_Bank_Name", arr.Item(i).Payee_Joint_Bank_Name)
                            clsCommon.AddColumnsForChange(coll, "Payee_Joint_Branch_Code", arr.Item(i).Payee_Joint_Branch_Code)
                            clsCommon.AddColumnsForChange(coll, "Payee_Joint_Branch_Name", arr.Item(i).Payee_Joint_Branch_Name)
                            clsCommon.AddColumnsForChange(coll, "Payee_Joint_Account_No", arr.Item(i).Payee_Joint_Account_No)
                            clsCommon.AddColumnsForChange(coll, "Payee_Joint_IFSC_Code", arr.Item(i).Payee_Joint_IFSC_Code)
                            clsCommon.AddColumnsForChange(coll, "Bank_Code", clsCommon.myCstr(arr.Item(i).Bank_Code))
                            clsCommon.AddColumnsForChange(coll, "Bank_Desc", clsCommon.myCstr(arr.Item(i).Bank_Desc))
                            clsCommon.AddColumnsForChange(coll, "Payment_Mode", clsCommon.myCstr(arr.Item(i).Payment_Mode))
                            clsCommon.AddColumnsForChange(coll, "Cheque_No", clsCommon.myCstr(arr.Item(i).Cheque_No))


                            If Not arr.Item(i).Cheque_Dated Is Nothing Then
                                clsCommon.AddColumnsForChange(coll, "Cheque_Dated", clsCommon.GetPrintDate(clsCommon.myCstr(arr.Item(i).Cheque_Dated), "dd/MMM/yyyy"), True)
                            End If

                            clsCommon.AddColumnsForChange(coll, "Milk_Qty", clsCommon.myCdbl(arr.Item(i).Milk_Qty))
                            clsCommon.AddColumnsForChange(coll, "Milk_Amount", clsCommon.myCdbl(arr.Item(i).Milk_Amount))

                            clsCommon.AddColumnsForChange(coll, "MCC_Sale_Amount", clsCommon.myCstr(arr.Item(i).MCC_Sale_Amount))
                            clsCommon.AddColumnsForChange(coll, "MCC_Sale_Return_Amount", clsCommon.myCdbl(arr.Item(i).MCC_Sale_Return_Amount))
                            clsCommon.AddColumnsForChange(coll, "MP_Adjust_Amount", clsCommon.myCdbl(arr.Item(i).MP_Adjust_Amount))

                            clsCommon.AddColumnsForChange(coll, "Incentive_Amount", clsCommon.myCdbl(arr.Item(i).Incentive_Amount))
                            clsCommon.AddColumnsForChange(coll, "Deduction_Amount", clsCommon.myCdbl(arr.Item(i).Deduction_Amount))

                            clsCommon.AddColumnsForChange(coll, "Payable_Amount", clsCommon.myCdbl(arr.Item(i).Payable_Amount))

                            clsCommon.AddColumnsForChange(coll, "PrevCycleDebitNoteMP", clsCommon.myCdbl(arr.Item(i).PrevCycleDebitNoteMP))
                            clsCommon.AddColumnsForChange(coll, "NextCycleDebitNoteMP", clsCommon.myCdbl(arr.Item(i).NextCycleDebitNoteMP))

                            clsCommon.AddColumnsForChange(coll, "Total_Advance_Amount", clsCommon.myCdbl(arr.Item(i).Total_Advance_Amount))
                            clsCommon.AddColumnsForChange(coll, "Total_Advance_Amount_Recovery", clsCommon.myCdbl(arr.Item(i).Total_Advance_Amount_Recovery))
                            clsCommon.AddColumnsForChange(coll, "Total_Loan_Payment", clsCommon.myCdbl(arr.Item(i).Total_Loan_Payment))
                            clsCommon.AddColumnsForChange(coll, "Total_Loan_Payment_Recovery", clsCommon.myCdbl(arr.Item(i).Total_Loan_Payment_Recovery))

                            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_PROCESS_DETAIL", OMInsertOrUpdate.Insert, "", tran)
                        End If
                    Next
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerPaymentDetail)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerPaymentDetail)
            Dim obj As New clsPaymentProcessFarmerPaymentDetail
            Dim qry As String = "select TSPL_MP_PAY_PROCESS_DETAIL.* from TSPL_MP_PAY_PROCESS_DETAIL  where TSPL_MP_PAY_PROCESS_DETAIL.Doc_No='" & doc_No & "' ORDER BY SNO"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerPaymentDetail
                    obj.Farmer_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("Farmer_Invoice_No"))
                    obj.Farmer_Invoice_Date = clsCommon.myCDate(dtbl.Rows(i)("Farmer_Invoice_Date"))
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.Is_select = IIf(clsCommon.myCdbl(dtbl.Rows(i)("Is_select")) > 0, True, False)
                    obj.SNo = clsCommon.myCstr(dtbl.Rows(i)("SNo"))

                    obj.AP_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_No"))
                    obj.AP_Invoice_Date = clsCommon.myCDate(dtbl.Rows(i)("AP_Invoice_Date"))
                    obj.AP_Adjustment_No = clsCommon.myCstr(dtbl.Rows(i)("AP_Adjustment_No"))
                    obj.AP_Adjustment_Date = clsCommon.myCDate(dtbl.Rows(i)("AP_Adjustment_Date"))

                    obj.VLC_CODE_Uploader = clsCommon.myCstr(dtbl.Rows(i)("VLC_CODE_Uploader"))
                    obj.VLC_Name = clsCommon.myCstr(dtbl.Rows(i)("VLC_Name"))
                    obj.VSP_CODE = clsCommon.myCstr(dtbl.Rows(i)("VSP_CODE"))
                    obj.VSP_NAME = clsCommon.myCstr(dtbl.Rows(i)("VSP_NAME"))
                    obj.Farmer_Code = clsCommon.myCstr(dtbl.Rows(i)("Farmer_Code"))
                    obj.Farmer_Name = clsCommon.myCstr(dtbl.Rows(i)("Farmer_Name"))
                    obj.Payee_Joint_Name = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Name"))
                    obj.Payee_Joint_Bank_Code = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Bank_Code"))
                    obj.Payee_Joint_Bank_Name = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Bank_Name"))
                    obj.Payee_Joint_Branch_Code = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Branch_Code"))
                    obj.Payee_Joint_Branch_Name = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Branch_Name"))
                    obj.Payee_Joint_Account_No = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Account_No"))
                    obj.Payee_Joint_IFSC_Code = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_IFSC_Code"))
                    obj.Bank_Code = clsCommon.myCstr(dtbl.Rows(i)("Bank_Code"))
                    obj.Bank_Desc = clsCommon.myCstr(dtbl.Rows(i)("Bank_Desc"))
                    obj.Payment_Mode = clsCommon.myCstr(dtbl.Rows(i)("Payment_Mode"))
                    obj.Cheque_No = clsCommon.myCstr(dtbl.Rows(i)("Cheque_No"))

                    obj.Milk_Qty = clsCommon.myCdbl(dtbl.Rows(i)("Milk_Qty"))
                    obj.Milk_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Milk_Amount"))

                    obj.MCC_Sale_Amount = clsCommon.myCstr(dtbl.Rows(i)("MCC_Sale_Amount"))
                    obj.MCC_Sale_Return_Amount = clsCommon.myCdbl(dtbl.Rows(i)("MCC_Sale_Return_Amount"))
                    obj.MP_Adjust_Amount = clsCommon.myCdbl(dtbl.Rows(i)("MP_Adjust_Amount"))

                    obj.Incentive_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Incentive_Amount"))
                    obj.Deduction_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Deduction_Amount"))

                    obj.Payable_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Payable_Amount"))
                    obj.PrevCycleDebitNoteMP = clsCommon.myCdbl(dtbl.Rows(i)("PrevCycleDebitNoteMP"))
                    obj.NextCycleDebitNoteMP = clsCommon.myCdbl(dtbl.Rows(i)("NextCycleDebitNoteMP"))

                    obj.Total_Advance_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Total_Advance_Amount"))
                    obj.Total_Advance_Amount_Recovery = clsCommon.myCdbl(dtbl.Rows(i)("Total_Advance_Amount_Recovery"))
                    obj.Total_Loan_Payment = clsCommon.myCdbl(dtbl.Rows(i)("Total_Loan_Payment"))
                    obj.Total_Loan_Payment_Recovery = clsCommon.myCdbl(dtbl.Rows(i)("Total_Loan_Payment_Recovery"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_MP_PAY_PROCESS_DETAIL where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPaymentProcessFarmerInvoices
#Region "Variales"
    Public Doc_No As String = ""
    Public SLNO As String = ""
    Public Milk_Purchase_Invoice_No As String = ""
    Public Milk_Purchase_Invoice_Date As String = ""
    Public AP_Invoice_No As String = ""
    Public AP_Invoice_Date As String = ""
    Public VLC_CODE As String = ""
    Public VSP_CODE As String = ""
    Public VSP_NAME As String = ""
    Public Payee_Joint_Name As String = ""
    Public Payee_Joint_Ac_No As String = ""
    Public Payee_Joint_Bank_Code As String = ""
    Public Payee_Joint_Bank_Name As String = ""
    Public Payee_Joint_Branch_Code As String = ""
    Public Payee_Joint_Branch_Name As String = ""
    Public Payee_Joint_IFSC_Code As String = ""
    Public Milk_Qty As Double = 0
    Public Inv_Amount As Double = 0
    Public Inv_EMP_Amount As Double = 0
    Public Inv_Amt_EMP_Amount As Double = 0
    Public Inv_Incentive_Amount As Double = 0
    Public Inv_Incentive_EMP_Amount As Double = 0
    Public Gross_Amount As Double = 0
    Public Vsp_Own_System_Amount As Double = 0
    Public Head_Load_Amount As Double = 0
    Public Deduction_Amount As Double = 0
    Public Vsp_Own_System_Doc_No As String = ""
    Public Head_Load_Doc_No As String = ""
    Public Deduction_Doc_No As String = ""
    Public Reduce_Deduc_Amt As Double = 0
    Public Bank_Code As String = ""
    Public Bank_Desc As String = ""
    Public Payment_Mode As String = ""
    Public Cheque_No As String = ""
    Public Service_Charge_Amt As Double = 0
    Public ActualVSPCode As String = ""
    Public ActualVSPName As String = ""

    Public MP_Amount As Decimal
    Public MP_EMP As Double
    Public MP_Incentive As Double
    Public MP_IncentiveEMP As Double
    Public MP_Net_Amount As Double
#End Region
    
    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerInvoices), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then

                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SLNO", arr.Item(i).SLNO)
                    clsCommon.AddColumnsForChange(coll, "Milk_Purchase_Invoice_No", arr.Item(i).Milk_Purchase_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "Milk_Purchase_Invoice_Date", arr.Item(i).Milk_Purchase_Invoice_Date)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", arr.Item(i).AP_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_Date", arr.Item(i).AP_Invoice_Date)
                    clsCommon.AddColumnsForChange(coll, "VLC_CODE", arr.Item(i).VLC_CODE)
                    clsCommon.AddColumnsForChange(coll, "VSP_CODE", arr.Item(i).VSP_CODE)
                    clsCommon.AddColumnsForChange(coll, "VSP_NAME", arr.Item(i).VSP_NAME)
                    clsCommon.AddColumnsForChange(coll, "Payee_Joint_Name", arr.Item(i).Payee_Joint_Name)
                    clsCommon.AddColumnsForChange(coll, "Payee_Joint_Bank_Code", arr.Item(i).Payee_Joint_Bank_Code)
                    clsCommon.AddColumnsForChange(coll, "Payee_Joint_Bank_Name", arr.Item(i).Payee_Joint_Bank_Name)
                    clsCommon.AddColumnsForChange(coll, "Payee_Joint_Branch_Code", arr.Item(i).Payee_Joint_Branch_Code)
                    clsCommon.AddColumnsForChange(coll, "Payee_Joint_Branch_Name", arr.Item(i).Payee_Joint_Branch_Name)
                    clsCommon.AddColumnsForChange(coll, "Payee_Joint_Ac_No", arr.Item(i).Payee_Joint_Ac_No)
                    clsCommon.AddColumnsForChange(coll, "Payee_Joint_IFSC_Code", arr.Item(i).Payee_Joint_IFSC_Code)
                    clsCommon.AddColumnsForChange(coll, "Milk_Qty", clsCommon.myCdbl(arr.Item(i).Milk_Qty))
                    clsCommon.AddColumnsForChange(coll, "Inv_Amount", clsCommon.myCdbl(arr.Item(i).Inv_Amount))
                    clsCommon.AddColumnsForChange(coll, "Inv_EMP_Amount", clsCommon.myCdbl(arr.Item(i).Inv_EMP_Amount))
                    clsCommon.AddColumnsForChange(coll, "Inv_Amt_EMP_Amount", clsCommon.myCdbl(arr.Item(i).Inv_Amt_EMP_Amount))
                    clsCommon.AddColumnsForChange(coll, "Inv_Incentive_Amount", clsCommon.myCdbl(arr.Item(i).Inv_Incentive_Amount))
                    clsCommon.AddColumnsForChange(coll, "Inv_Incentive_EMP_Amount", clsCommon.myCdbl(arr.Item(i).Inv_Incentive_EMP_Amount))
                    clsCommon.AddColumnsForChange(coll, "Gross_Amount", clsCommon.myCdbl(arr.Item(i).Gross_Amount))
                    clsCommon.AddColumnsForChange(coll, "Vsp_Own_System_Amount", clsCommon.myCdbl(arr.Item(i).Vsp_Own_System_Amount))
                    clsCommon.AddColumnsForChange(coll, "Head_Load_Amount", clsCommon.myCdbl(arr.Item(i).Head_Load_Amount))
                    clsCommon.AddColumnsForChange(coll, "Deduction_Amount", clsCommon.myCdbl(arr.Item(i).Deduction_Amount))
                    clsCommon.AddColumnsForChange(coll, "Vsp_Own_System_Doc_No", clsCommon.myCstr(arr.Item(i).Vsp_Own_System_Doc_No))
                    clsCommon.AddColumnsForChange(coll, "Head_Load_Doc_No", clsCommon.myCstr(arr.Item(i).Head_Load_Doc_No))
                    clsCommon.AddColumnsForChange(coll, "Deduction_Doc_No", clsCommon.myCstr(arr.Item(i).Deduction_Doc_No))
                    clsCommon.AddColumnsForChange(coll, "Reduce_Deduc_Amt", clsCommon.myCdbl(arr.Item(i).Reduce_Deduc_Amt))
                    clsCommon.AddColumnsForChange(coll, "Bank_Code", clsCommon.myCstr(arr.Item(i).Bank_Code))
                    clsCommon.AddColumnsForChange(coll, "Bank_Desc", clsCommon.myCstr(arr.Item(i).Bank_Desc))
                    clsCommon.AddColumnsForChange(coll, "Payment_Mode", clsCommon.myCstr(arr.Item(i).Payment_Mode))
                    clsCommon.AddColumnsForChange(coll, "Cheque_No", clsCommon.myCstr(arr.Item(i).Cheque_No))
                    clsCommon.AddColumnsForChange(coll, "Service_Charge_Amt", clsCommon.myCstr(arr.Item(i).Service_Charge_Amt))

                    clsCommon.AddColumnsForChange(coll, "MP_Amount", clsCommon.myCstr(arr.Item(i).MP_Amount))
                    clsCommon.AddColumnsForChange(coll, "MP_EMP", clsCommon.myCstr(arr.Item(i).MP_EMP))
                    clsCommon.AddColumnsForChange(coll, "MP_Incentive", clsCommon.myCstr(arr.Item(i).MP_Incentive))
                    clsCommon.AddColumnsForChange(coll, "MP_IncentiveEMP", clsCommon.myCstr(arr.Item(i).MP_IncentiveEMP))
                    clsCommon.AddColumnsForChange(coll, "MP_Net_Amount", clsCommon.myCstr(arr.Item(i).MP_Net_Amount))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_INVOICE", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerInvoices)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerInvoices)
            Dim obj As New clsPaymentProcessFarmerInvoices
            Dim q As String = "select TSPL_PAYMENT_PROCESS_INVOICE.*,TSPL_VENDOR_MASTER.Parent_Vendor_Code as ActualVSPCode,ParVen.Vendor_Name as ActualVSPName from TSPL_PAYMENT_PROCESS_INVOICE left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_INVOICE.VSP_CODE left outer join TSPL_VENDOR_MASTER as ParVen on ParVen.Vendor_Code=TSPL_VENDOR_MASTER.Parent_Vendor_Code where TSPL_PAYMENT_PROCESS_INVOICE.Doc_No='" & doc_No & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerInvoices
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.SLNO = clsCommon.myCstr(dtbl.Rows(i)("SLNO"))
                    obj.Milk_Purchase_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("Milk_Purchase_Invoice_No"))
                    obj.Milk_Purchase_Invoice_Date = clsCommon.myCstr(dtbl.Rows(i)("Milk_Purchase_Invoice_Date"))
                    obj.AP_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_No"))
                    obj.AP_Invoice_Date = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_Date"))
                    obj.VLC_CODE = clsCommon.myCstr(dtbl.Rows(i)("VLC_CODE"))
                    obj.VSP_CODE = clsCommon.myCstr(dtbl.Rows(i)("VSP_CODE"))
                    obj.VSP_NAME = clsCommon.myCstr(dtbl.Rows(i)("VSP_NAME"))
                    obj.Payee_Joint_Name = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Name"))
                    obj.Payee_Joint_Bank_Code = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Bank_Code"))
                    obj.Payee_Joint_Bank_Name = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Bank_Name"))
                    obj.Payee_Joint_Branch_Code = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Branch_Code"))
                    obj.Payee_Joint_Branch_Name = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Branch_Name"))
                    obj.Payee_Joint_IFSC_Code = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_IFSC_Code"))
                    obj.Payee_Joint_Ac_No = clsCommon.myCstr(dtbl.Rows(i)("Payee_Joint_Ac_No"))
                    obj.Milk_Qty = clsCommon.myCdbl(dtbl.Rows(i)("Milk_Qty"))
                    obj.Inv_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Inv_Amount"))
                    obj.Inv_EMP_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Inv_EMP_Amount"))
                    obj.Inv_Amt_EMP_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Inv_Amt_EMP_Amount"))
                    obj.Inv_Incentive_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Inv_Incentive_Amount"))
                    obj.Inv_Incentive_EMP_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Inv_Incentive_EMP_Amount"))
                    obj.Gross_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Gross_Amount"))
                    obj.Vsp_Own_System_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Vsp_Own_System_Amount"))
                    obj.Head_Load_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Head_Load_Amount"))
                    obj.Deduction_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Deduction_Amount"))
                    obj.Vsp_Own_System_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Vsp_Own_System_Doc_No"))
                    obj.Head_Load_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Head_Load_Doc_No"))
                    obj.Deduction_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Deduction_Doc_No"))
                    obj.Reduce_Deduc_Amt = clsCommon.myCdbl(dtbl.Rows(i)("Reduce_Deduc_Amt"))
                    obj.Bank_Code = clsCommon.myCstr(dtbl.Rows(i)("Bank_Code"))
                    obj.Bank_Desc = clsCommon.myCstr(dtbl.Rows(i)("Bank_Desc"))
                    obj.Payment_Mode = clsCommon.myCstr(dtbl.Rows(i)("Payment_Mode"))
                    obj.Cheque_No = clsCommon.myCstr(dtbl.Rows(i)("Cheque_No"))
                    obj.Service_Charge_Amt = clsCommon.myCdbl(dtbl.Rows(i)("Service_Charge_Amt"))
                    obj.ActualVSPCode = clsCommon.myCstr(dtbl.Rows(i)("ActualVSPCode"))
                    obj.ActualVSPName = clsCommon.myCstr(dtbl.Rows(i)("ActualVSPName"))

                    obj.MP_Amount = clsCommon.myCdbl(dtbl.Rows(i)("MP_Amount"))
                    obj.MP_EMP = clsCommon.myCdbl(dtbl.Rows(i)("MP_EMP"))
                    obj.MP_Incentive = clsCommon.myCdbl(dtbl.Rows(i)("MP_Incentive"))
                    obj.MP_IncentiveEMP = clsCommon.myCdbl(dtbl.Rows(i)("MP_IncentiveEMP"))
                    obj.MP_Net_Amount = clsCommon.myCdbl(dtbl.Rows(i)("MP_Net_Amount"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_INVOICE where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPaymentProcessFarmerMCCSale
#Region "Variales"
    Public Doc_No As String = ""
    Public SLNO As String = ""
    Public Shipment_Doc_No As String = ""
    Public Shipment_Doc_Date As String = ""
    Public Sale_Doc_No As String = ""
    Public Sale_Doc_Date As String = ""
    'Public AR_Invoice_No As String = ""
    'Public AR_Invoice_Date As String = ""
    Public VSP_CODE As String = ""
    Public VSP_NAME As String = ""
    Public Farmer_Code As String = ""
    Public Farmer_Name As String = ""
    Public Amount As Double = 0
    Public Reduce_Deduc_Amt As Double = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerMCCSale), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                'Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_MCC_SALE where  Doc_No='" & arr.Item(0).Doc_No & "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SLNO", arr.Item(i).SLNO)
                    clsCommon.AddColumnsForChange(coll, "Shipment_Doc_No", arr.Item(i).Shipment_Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Shipment_Doc_Date", arr.Item(i).Shipment_Doc_Date)
                    clsCommon.AddColumnsForChange(coll, "Sale_Doc_No", arr.Item(i).Sale_Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Sale_Doc_Date", arr.Item(i).Sale_Doc_Date)
                    'clsCommon.AddColumnsForChange(coll, "AR_Invoice_No", arr.Item(i).AR_Invoice_No)
                    'clsCommon.AddColumnsForChange(coll, "AR_Invoice_Date", arr.Item(i).AR_Invoice_Date)
                    clsCommon.AddColumnsForChange(coll, "VSP_CODE", arr.Item(i).VSP_CODE)
                    clsCommon.AddColumnsForChange(coll, "VSP_NAME", arr.Item(i).VSP_NAME)
                    clsCommon.AddColumnsForChange(coll, "Farmer_Code", arr.Item(i).Farmer_Code)
                    clsCommon.AddColumnsForChange(coll, "Farmer_Name", arr.Item(i).Farmer_Name)
                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(arr.Item(i).Amount))
                    clsCommon.AddColumnsForChange(coll, "Reduce_Deduc_Amt", clsCommon.myCdbl(arr.Item(i).Reduce_Deduc_Amt))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_PROCESS_MCC_SALE", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerMCCSale)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerMCCSale)
            Dim obj As New clsPaymentProcessFarmerMCCSale
            Dim q As String = "select * from TSPL_MP_PAY_PROCESS_MCC_SALE where Doc_No='" & doc_No & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerMCCSale
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.SLNO = clsCommon.myCstr(dtbl.Rows(i)("SLNO"))
                    obj.Shipment_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Shipment_Doc_No"))
                    obj.Shipment_Doc_Date = clsCommon.myCstr(dtbl.Rows(i)("Shipment_Doc_Date"))
                    obj.Sale_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Sale_Doc_No"))
                    obj.Sale_Doc_Date = clsCommon.myCstr(dtbl.Rows(i)("Sale_Doc_Date"))
                    'obj.AR_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("AR_Invoice_No"))
                    'obj.AR_Invoice_Date = clsCommon.myCstr(dtbl.Rows(i)("AR_Invoice_Date"))
                    obj.VSP_CODE = clsCommon.myCstr(dtbl.Rows(i)("VSP_CODE"))
                    obj.VSP_NAME = clsCommon.myCstr(dtbl.Rows(i)("VSP_NAME"))
                    obj.Farmer_Code = clsCommon.myCstr(dtbl.Rows(i)("Farmer_Code"))
                    obj.Farmer_Name = clsCommon.myCstr(dtbl.Rows(i)("Farmer_Name"))
                    obj.Amount = clsCommon.myCdbl(dtbl.Rows(i)("Amount"))
                    obj.Reduce_Deduc_Amt = clsCommon.myCdbl(dtbl.Rows(i)("Reduce_Deduc_Amt"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_MP_PAY_PROCESS_MCC_SALE where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPaymentProcessFarmerItemIssue
#Region "Variales"
    Public Doc_No As String = ""
    Public SLNO As String = ""
    Public Item_Issue_Doc_No As String = ""
    Public Item_Issue_Doc_Date As String = ""
    Public AP_Invoice_No As String = ""
    Public AP_Invoice_Date As String = ""
    Public Vendor_CODE As String = ""
    Public Vendor_NAME As String = ""
    Public Item_Code As String = ""
    Public Item_Desc As String = ""
    Public Amount As Double = 0
    Public Reduce_Deduc_Amt As Double = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerItemIssue), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                'Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_ITEM_ISSUE where  Doc_No='" & arr.Item(0).Doc_No & "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SLNO", arr.Item(i).SLNO)
                    clsCommon.AddColumnsForChange(coll, "Item_Issue_Doc_No", arr.Item(i).Item_Issue_Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Issue_Doc_Date", arr.Item(i).Item_Issue_Doc_Date)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", arr.Item(i).AP_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_Date", arr.Item(i).AP_Invoice_Date)
                    clsCommon.AddColumnsForChange(coll, "Vendor_CODE", arr.Item(i).Vendor_CODE)
                    clsCommon.AddColumnsForChange(coll, "Vendor_NAME", arr.Item(i).Vendor_NAME)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", arr.Item(i).Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", arr.Item(i).Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(arr.Item(i).Amount))
                    clsCommon.AddColumnsForChange(coll, "Reduce_Deduc_Amt", clsCommon.myCdbl(arr.Item(i).Reduce_Deduc_Amt))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_ITEM_ISSUE", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerItemIssue)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerItemIssue)
            Dim obj As New clsPaymentProcessFarmerItemIssue
            Dim q As String = "select * from TSPL_PAYMENT_PROCESS_ITEM_ISSUE where Doc_No='" & doc_No & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerItemIssue
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.SLNO = clsCommon.myCstr(dtbl.Rows(i)("SLNO"))
                    obj.Item_Issue_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Item_Issue_Doc_No"))
                    obj.Item_Issue_Doc_Date = clsCommon.myCstr(dtbl.Rows(i)("Item_Issue_Doc_Date"))
                    obj.AP_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_No"))
                    obj.AP_Invoice_Date = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_Date"))
                    obj.Vendor_CODE = clsCommon.myCstr(dtbl.Rows(i)("Vendor_CODE"))
                    obj.Vendor_NAME = clsCommon.myCstr(dtbl.Rows(i)("Vendor_NAME"))
                    obj.Item_Code = clsCommon.myCstr(dtbl.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dtbl.Rows(i)("Item_Desc"))
                    obj.Amount = clsCommon.myCdbl(dtbl.Rows(i)("Amount"))
                    obj.Reduce_Deduc_Amt = clsCommon.myCdbl(dtbl.Rows(i)("Reduce_Deduc_Amt"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_ITEM_ISSUE where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsPaymentProcessFarmerItemIssueReturn
#Region "Variales"
    Public Doc_No As String = ""
    Public SLNO As String = ""
    Public Item_Issue_Return_No As String = ""
    Public Item_Issue_Doc_No As String = ""
    Public Item_Issue_Return_Date As String = ""
    Public AP_Invoice_No As String = ""
    Public AP_Invoice_Date As String = ""
    Public Vendor_CODE As String = ""
    Public Vendor_NAME As String = ""
    Public Item_Code As String = ""
    Public Item_Desc As String = ""
    Public Amount As Double = 0
    'Public Reduce_Deduc_Amt As Double = 0
#End Region
    
    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerItemIssueReturn), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SLNO", arr.Item(i).SLNO)
                    clsCommon.AddColumnsForChange(coll, "Item_Issue_Return_No", arr.Item(i).Item_Issue_Return_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Issue_Doc_No", arr.Item(i).Item_Issue_Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Issue_Return_Date", arr.Item(i).Item_Issue_Return_Date)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", arr.Item(i).AP_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_Date", arr.Item(i).AP_Invoice_Date)
                    clsCommon.AddColumnsForChange(coll, "Vendor_CODE", arr.Item(i).Vendor_CODE)
                    clsCommon.AddColumnsForChange(coll, "Vendor_NAME", arr.Item(i).Vendor_NAME)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", arr.Item(i).Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", arr.Item(i).Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(arr.Item(i).Amount))
                    'clsCommon.AddColumnsForChange(coll, "Reduce_Deduc_Amt", clsCommon.myCdbl(arr.Item(i).Reduce_Deduc_Amt))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerItemIssueReturn)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerItemIssueReturn)
            Dim obj As New clsPaymentProcessFarmerItemIssueReturn
            Dim q As String = "select * from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN where Doc_No='" & doc_No & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerItemIssueReturn
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.SLNO = clsCommon.myCstr(dtbl.Rows(i)("SLNO"))
                    obj.Item_Issue_Return_No = clsCommon.myCstr(dtbl.Rows(i)("Item_Issue_Return_No"))
                    obj.Item_Issue_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Item_Issue_Doc_No"))
                    obj.Item_Issue_Return_Date = clsCommon.myCstr(dtbl.Rows(i)("Item_Issue_Return_Date"))
                    obj.AP_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_No"))
                    obj.AP_Invoice_Date = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_Date"))
                    obj.Vendor_CODE = clsCommon.myCstr(dtbl.Rows(i)("Vendor_CODE"))
                    obj.Vendor_NAME = clsCommon.myCstr(dtbl.Rows(i)("Vendor_NAME"))
                    obj.Item_Code = clsCommon.myCstr(dtbl.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dtbl.Rows(i)("Item_Desc"))
                    obj.Amount = clsCommon.myCdbl(dtbl.Rows(i)("Amount"))
                    'obj.Reduce_Deduc_Amt = clsCommon.myCdbl(dtbl.Rows(i)("Reduce_Deduc_Amt"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsPaymentProcessFarmerDeduction
#Region "Variales"
    Public Doc_No As String = ""
    Public SLNO As String = ""
    Public AP_Invoice_No As String = ""
    Public AP_Invoice_Date As String = ""
    Public Vendor_CODE As String = ""
    Public Vendor_NAME As String = ""
    Public Ded_Code As String = ""
    Public Ded_Desc As String = ""
    Public Amount As Double = 0
    Public Reduce_Deduc_Amt As Double = 0
    Public IsFromPrevPPCycle As Double = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerDeduction), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                'Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_DEDUCTION where  Doc_No='" & arr.Item(0).Doc_No & "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SLNO", arr.Item(i).SLNO)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", arr.Item(i).AP_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_Date", arr.Item(i).AP_Invoice_Date)
                    clsCommon.AddColumnsForChange(coll, "Vendor_CODE", arr.Item(i).Vendor_CODE)
                    clsCommon.AddColumnsForChange(coll, "Vendor_NAME", arr.Item(i).Vendor_NAME)
                    clsCommon.AddColumnsForChange(coll, "Ded_Code", arr.Item(i).Ded_Code)
                    clsCommon.AddColumnsForChange(coll, "Ded_Desc", arr.Item(i).Ded_Desc)
                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(arr.Item(i).Amount))
                    clsCommon.AddColumnsForChange(coll, "Reduce_Deduc_Amt", clsCommon.myCdbl(arr.Item(i).Reduce_Deduc_Amt))
                    clsCommon.AddColumnsForChange(coll, "IsFromPrevPPCycle", arr.Item(i).IsFromPrevPPCycle)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_DEDUCTION", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerDeduction)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerDeduction)
            Dim obj As New clsPaymentProcessFarmerDeduction
            Dim q As String = "select * from TSPL_PAYMENT_PROCESS_DEDUCTION where Doc_No='" & doc_No & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerDeduction
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.SLNO = clsCommon.myCstr(dtbl.Rows(i)("SLNO"))
                    obj.AP_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_No"))
                    obj.AP_Invoice_Date = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_Date"))
                    obj.Vendor_CODE = clsCommon.myCstr(dtbl.Rows(i)("Vendor_CODE"))
                    obj.Vendor_NAME = clsCommon.myCstr(dtbl.Rows(i)("Vendor_NAME"))
                    obj.Ded_Code = clsCommon.myCstr(dtbl.Rows(i)("Ded_Code"))
                    obj.Ded_Desc = clsCommon.myCstr(dtbl.Rows(i)("Ded_Desc"))
                    obj.Amount = clsCommon.myCdbl(dtbl.Rows(i)("Amount"))
                    obj.Reduce_Deduc_Amt = clsCommon.myCdbl(dtbl.Rows(i)("Reduce_Deduc_Amt"))
                    obj.IsFromPrevPPCycle = clsCommon.myCdbl(dtbl.Rows(i)("IsFromPrevPPCycle"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_DEDUCTION where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPaymentProcessFarmerCreditNote
#Region "Variales"
    Public Doc_No As String = ""
    Public SLNO As String = ""
    Public AP_Invoice_No As String = ""
    Public AP_Invoice_Date As String = ""
    Public Vendor_CODE As String = ""
    Public Vendor_NAME As String = ""
    Public Amount As Double = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerCreditNote), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                'Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_DEDUCTION where  Doc_No='" & arr.Item(0).Doc_No & "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SLNO", arr.Item(i).SLNO)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", arr.Item(i).AP_Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "AP_Invoice_Date", arr.Item(i).AP_Invoice_Date)
                    clsCommon.AddColumnsForChange(coll, "Vendor_CODE", arr.Item(i).Vendor_CODE)
                    clsCommon.AddColumnsForChange(coll, "Vendor_NAME", arr.Item(i).Vendor_NAME)
                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(arr.Item(i).Amount))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_CREDIT_NOTE", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerCreditNote)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerCreditNote)
            Dim obj As New clsPaymentProcessFarmerCreditNote
            Dim q As String = "select * from TSPL_PAYMENT_PROCESS_CREDIT_NOTE where Doc_No='" & doc_No & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerCreditNote
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.SLNO = clsCommon.myCstr(dtbl.Rows(i)("SLNO"))
                    obj.AP_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_No"))
                    obj.AP_Invoice_Date = clsCommon.myCstr(dtbl.Rows(i)("AP_Invoice_Date"))
                    obj.Vendor_CODE = clsCommon.myCstr(dtbl.Rows(i)("Vendor_CODE"))
                    obj.Vendor_NAME = clsCommon.myCstr(dtbl.Rows(i)("Vendor_NAME"))
                    obj.Amount = clsCommon.myCdbl(dtbl.Rows(i)("Amount"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_CREDIT_NOTE where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPaymentProcessFarmerMCCSaleReturn
#Region "Variales"
    Public Doc_No As String = ""
    Public SLNO As String = ""
    Public Return_Doc_No As String = ""
    Public Return_Doc_Type As String = ""
    Public Return_Doc_Date As String = ""
    Public Shipment_Doc_No As String = ""
    Public Shipment_Doc_Date As String = ""
    Public Sale_Doc_No As String = ""
    Public Sale_Doc_Date As String = ""
    'Public AR_Invoice_No As String = ""
    'Public AR_Invoice_Date As String = ""
    Public VSP_CODE As String = ""
    Public VSP_NAME As String = ""
    Public Farmer_Code As String = ""
    Public Farmer_Name As String = ""
    Public Amount As Double = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerMCCSaleReturn), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                Dim qry As String = "delete from TSPL_MP_PAY_PROCESS_MCC_SALE_Return where  Doc_No='" & DocNo & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SLNO", arr.Item(i).SLNO)
                    clsCommon.AddColumnsForChange(coll, "Return_Doc_No", arr.Item(i).Return_Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Return_Doc_TYpe", arr.Item(i).Return_Doc_Type)
                    clsCommon.AddColumnsForChange(coll, "Return_Doc_Date", arr.Item(i).Return_Doc_Date)
                    clsCommon.AddColumnsForChange(coll, "Shipment_Doc_No", arr.Item(i).Shipment_Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Shipment_Doc_Date", arr.Item(i).Shipment_Doc_Date)
                    clsCommon.AddColumnsForChange(coll, "Sale_Doc_No", arr.Item(i).Sale_Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Sale_Doc_Date", arr.Item(i).Sale_Doc_Date)
                    'clsCommon.AddColumnsForChange(coll, "AR_Invoice_No", arr.Item(i).AR_Invoice_No)
                    'clsCommon.AddColumnsForChange(coll, "AR_Invoice_Date", arr.Item(i).AR_Invoice_Date)
                    clsCommon.AddColumnsForChange(coll, "VSP_CODE", arr.Item(i).VSP_CODE)
                    clsCommon.AddColumnsForChange(coll, "VSP_NAME", arr.Item(i).VSP_NAME)
                    clsCommon.AddColumnsForChange(coll, "MP_Code", arr.Item(i).Farmer_Code)
                    clsCommon.AddColumnsForChange(coll, "MP_Name", arr.Item(i).Farmer_Name)
                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(arr.Item(i).Amount))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_PROCESS_MCC_SALE_Return", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerMCCSaleReturn)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerMCCSaleReturn)
            Dim obj As New clsPaymentProcessFarmerMCCSaleReturn
            Dim q As String = "select * from TSPL_MP_PAY_PROCESS_MCC_SALE_Return where Doc_No='" & doc_No & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerMCCSaleReturn
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.SLNO = clsCommon.myCstr(dtbl.Rows(i)("SLNO"))
                    obj.Shipment_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Shipment_Doc_No"))
                    obj.Shipment_Doc_Date = clsCommon.myCstr(dtbl.Rows(i)("Shipment_Doc_Date"))
                    obj.Return_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Return_Doc_No"))
                    obj.Return_Doc_Type = clsCommon.myCstr(dtbl.Rows(i)("Return_Doc_Type"))
                    obj.Return_Doc_Date = clsCommon.myCstr(dtbl.Rows(i)("Return_Doc_Date"))
                    obj.Sale_Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Sale_Doc_No"))
                    obj.Sale_Doc_Date = clsCommon.myCstr(dtbl.Rows(i)("Sale_Doc_Date"))
                    'obj.AR_Invoice_No = clsCommon.myCstr(dtbl.Rows(i)("AR_Invoice_No"))
                    'obj.AR_Invoice_Date = clsCommon.myCstr(dtbl.Rows(i)("AR_Invoice_Date"))
                    obj.VSP_CODE = clsCommon.myCstr(dtbl.Rows(i)("VSP_CODE"))
                    obj.VSP_NAME = clsCommon.myCstr(dtbl.Rows(i)("VSP_NAME"))
                    obj.Farmer_Code = clsCommon.myCstr(dtbl.Rows(i)("MP_Code"))
                    obj.Farmer_Name = clsCommon.myCstr(dtbl.Rows(i)("MP_Name"))
                    obj.Amount = clsCommon.myCdbl(dtbl.Rows(i)("Amount"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_MP_PAY_PROCESS_MCC_SALE_Return where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPaymentProcessFarmerAdvancePayment
#Region "Variales"
    Public Doc_No As String = ""
    Public SNo As Integer = 0
    Public Payment_No As String = ""
    Public Vendor_Code As String = ""
    Public Payment_Amount As Double = 0
    Public Payment_Balance As Double = 0
    Public Amount_Knock_Off As Double = 0
    ''No a table column
    Public Payment_Date As String = ""
    Public Vendor_Name As String = ""
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerAdvancePayment), ByVal arrPPD As List(Of clsPaymentProcessDetail), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SNo", arr.Item(i).SNo)
                    clsCommon.AddColumnsForChange(coll, "Payment_No", arr.Item(i).Payment_No)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", arr.Item(i).Vendor_Code)
                    clsCommon.AddColumnsForChange(coll, "Payment_Amount", arr.Item(i).Payment_Amount)
                    clsCommon.AddColumnsForChange(coll, "Payment_Balance", arr.Item(i).Payment_Balance)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If

            If arrPPD IsNot Nothing AndAlso arrPPD.Count > 0 Then
                For i = 0 To arrPPD.Count - 1
                    Dim qry As String = "select TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_No,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_Balance from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_No where TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Doc_No='" + DocNo + "' and TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Vendor_Code='" + arrPPD(i).VSP_CODE + "' order by TSPL_PAYMENT_HEADER.Payment_Date "
                    Dim KnockOffAmt As Double = arrPPD(i).Advance_Payment_Amount_Knock_Off
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If KnockOffAmt <= 0 Then
                                Exit For
                            End If
                            Dim ApplicableAmt As Double = 0
                            If KnockOffAmt >= clsCommon.myCdbl(dr("Payment_Balance")) Then
                                ApplicableAmt = clsCommon.myCdbl(dr("Payment_Balance"))
                            Else
                                ApplicableAmt = KnockOffAmt
                            End If
                            KnockOffAmt -= ApplicableAmt

                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Amount_Knock_Off", ApplicableAmt)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT", OMInsertOrUpdate.Update, "Doc_No='" + DocNo + "'  and Payment_No='" + clsCommon.myCstr(dr("Payment_No")) + "'", tran)
                        Next
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function AutoKnockOff(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean

        Return True
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerAdvancePayment)
        Try
            Dim arr As New List(Of clsPaymentProcessFarmerAdvancePayment)
            Dim obj As New clsPaymentProcessFarmerAdvancePayment
            Dim q As String = "select TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.*, TSPL_PAYMENT_HEADER.Payment_Date, TSPL_VENDOR_MASTER.Vendor_Name from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_No left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Vendor_Code where Doc_No='" & doc_No & "' order by SNo"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerAdvancePayment
                    obj.Doc_No = clsCommon.myCstr(dt.Rows(i)("Doc_No"))
                    obj.SNo = clsCommon.myCstr(dt.Rows(i)("SNo"))
                    obj.Payment_No = clsCommon.myCstr(dt.Rows(i)("Payment_No"))
                    obj.Payment_Date = clsCommon.GetPrintDate(dt.Rows(i)("Payment_Date"), "dd/MM/yyyy")
                    obj.Vendor_Code = clsCommon.myCstr(dt.Rows(i)("Vendor_Code"))
                    obj.Vendor_Name = clsCommon.myCstr(dt.Rows(i)("Vendor_Name"))
                    obj.Payment_Amount = clsCommon.myCdbl(dt.Rows(i)("Payment_Amount"))
                    obj.Payment_Balance = clsCommon.myCdbl(dt.Rows(i)("Payment_Balance"))
                    obj.Amount_Knock_Off = clsCommon.myCdbl(dt.Rows(i)("Amount_Knock_Off"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsPaymentProcessAdjustment
#Region "Variales"
    Public Doc_No As String = ""
    Public Is_select As Boolean = False
    Public SNo As String = ""

    Public Adjustment_Date As DateTime
    Public Adjustment_No As String = ""
    Public Adjustment_Type As String = ""

    Public VSP_CODE As String = ""
    Public VSP_NAME As String = ""
    Public Farmer_Code As String = ""
    Public Farmer_Name As String = ""
    Public Desciption As String = ""
    Public Remarks As String = ""
    Public Adjustment_Amount As Double = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessAdjustment), ByVal tran As SqlTransaction) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True

            If arr IsNot Nothing AndAlso arr.Count > 0 Then

                For i = 0 To arr.Count - 1

                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Adjustment_Amount", arr.Item(i).Adjustment_Amount)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "Is_select", IIf(arr.Item(i).Is_select, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "SNo", i + 1)

                    clsCommon.AddColumnsForChange(coll, "Adjustment_Date", clsCommon.GetPrintDate(arr.Item(i).Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Adjustment_No", arr.Item(i).Adjustment_No)
                    clsCommon.AddColumnsForChange(coll, "Adjustment_Type", arr.Item(i).Adjustment_Type)
                    clsCommon.AddColumnsForChange(coll, "Description", arr.Item(i).Desciption)
                    clsCommon.AddColumnsForChange(coll, "VSP_CODE", arr.Item(i).VSP_CODE)
                    'clsCommon.AddColumnsForChange(coll, "VSP_NAME", arr.Item(i).VSP_NAME)
                    clsCommon.AddColumnsForChange(coll, "Farmer_Code", arr.Item(i).Farmer_Code)
                    'clsCommon.AddColumnsForChange(coll, "Farmer_Name", arr.Item(i).Farmer_Name)

                    clsCommon.AddColumnsForChange(coll, "Remarks", arr.Item(i).Remarks)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_ADJ_DETAIL", OMInsertOrUpdate.Insert, "", tran)

                Next

            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessAdjustment)
        Try
            Dim arr As New List(Of clsPaymentProcessAdjustment)
            Dim obj As New clsPaymentProcessAdjustment
            Dim qry As String = "select TSPL_PAYMENT_PROCESS_ADJ_DETAIL.*,VSP.VENDOR_NAME AS VSP_NAME,MP.MP_NAME AS Farmer_Name from TSPL_PAYMENT_PROCESS_ADJ_DETAIL  " & _
                " left join TSPL_VENDOR_MASTER VSP ON TSPL_PAYMENT_PROCESS_ADJ_DETAIL.VSP_CODE=VSP.VENDOR_CODE " & _
                " LEFT JOIN TSPL_MP_MASTER MP ON TSPL_PAYMENT_PROCESS_ADJ_DETAIL.FARMER_CODE=MP.MP_CODE " & _
                "  where TSPL_PAYMENT_PROCESS_ADJ_DETAIL.Doc_No='" & doc_No & "' ORDER BY SNO"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessAdjustment
                    obj.Adjustment_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Adjustment_Amount"))
                    obj.Adjustment_Date = clsCommon.myCDate(dtbl.Rows(i)("Adjustment_Date"))
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.Is_select = IIf(clsCommon.myCdbl(dtbl.Rows(i)("Is_select")) > 0, True, False)
                    obj.SNo = clsCommon.myCdbl(dtbl.Rows(i)("SNo"))

                    obj.Adjustment_No = clsCommon.myCstr(dtbl.Rows(i)("Adjustment_No"))
                    obj.Adjustment_Type = clsCommon.myCstr(dtbl.Rows(i)("Adjustment_Type"))
                    obj.Desciption = clsCommon.myCstr(dtbl.Rows(i)("Description"))
                    obj.Remarks = clsCommon.myCstr(dtbl.Rows(i)("Remarks"))

                    obj.VSP_CODE = clsCommon.myCstr(dtbl.Rows(i)("VSP_CODE"))
                    obj.VSP_NAME = clsCommon.myCstr(dtbl.Rows(i)("VSP_NAME"))
                    obj.Farmer_Code = clsCommon.myCstr(dtbl.Rows(i)("Farmer_Code"))
                    obj.Farmer_Name = clsCommon.myCstr(dtbl.Rows(i)("Farmer_Name"))
                    
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_ADJ_DETAIL where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


Public Class clsPaymentProcessFarmerMPAdvance
#Region "Variales"
    Public Doc_No As String = ""
    Public SNo As String = ""
    Public MP_Code As String = ""
    Public MP_Name As String = "" ''Not a Table Column
    Public Payment_No As String = ""
    Public Payment_Date As DateTime
    Public Payment_Amount As Decimal = 0
    Public Is_Loan As Boolean = False
    Public Knock_Off_Amt As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsPaymentProcessFarmerMPAdvance), ByVal tran As SqlTransaction) As Boolean
        Try
            Dim i As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SNo", i + 1)
                    clsCommon.AddColumnsForChange(coll, "MP_CODE", arr.Item(i).MP_CODE)
                    clsCommon.AddColumnsForChange(coll, "Payment_No", arr.Item(i).Payment_No)
                    clsCommon.AddColumnsForChange(coll, "Payment_Date", clsCommon.GetPrintDate(arr.Item(i).Payment_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Payment_Amount", arr.Item(i).Payment_Amount)
                    clsCommon.AddColumnsForChange(coll, "Is_Loan", IIf(arr.Item(i).Is_Loan, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Knock_Off_Amt", arr.Item(i).Knock_Off_Amt)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPaymentProcessFarmerMPAdvance)
        Dim arr As List(Of clsPaymentProcessFarmerMPAdvance) = Nothing
        Try
            Dim obj As New clsPaymentProcessFarmerMPAdvance
            Dim qry As String = "select TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE.*,TSPL_MP_MASTER.MP_NAME from TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE  " & _
                " left join TSPL_MP_MASTER ON TSPL_MP_MASTER.MP_Code=TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE.MP_Code " & _
                "  where TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE.Doc_No='" & doc_No & "' ORDER BY SNO"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                arr = New List(Of clsPaymentProcessFarmerMPAdvance)()
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsPaymentProcessFarmerMPAdvance
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.SNo = clsCommon.myCdbl(dtbl.Rows(i)("SNo"))
                    obj.MP_Code = clsCommon.myCstr(dtbl.Rows(i)("MP_Code"))
                    obj.MP_Name = clsCommon.myCstr(dtbl.Rows(i)("MP_Name"))
                    obj.Payment_No = clsCommon.myCstr(dtbl.Rows(i)("Payment_No"))
                    obj.Payment_Date = clsCommon.myCDate(dtbl.Rows(i)("Payment_Date"))
                    obj.Payment_Amount = clsCommon.myCdbl(dtbl.Rows(i)("Payment_Amount"))
                    obj.Knock_Off_Amt = clsCommon.myCdbl(dtbl.Rows(i)("Knock_Off_Amt"))
                    obj.Is_Loan = clsCommon.myCdbl(dtbl.Rows(i)("Is_Loan"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function

    Public Shared Function DeleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_PAYMENT_PROCESS_FARMER_MP_ADVANCE where  Doc_No='" & DocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class



