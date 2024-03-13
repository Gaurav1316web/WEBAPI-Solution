''Changes done by priti mam replace by balwinder

Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class clsReceiptInvoiceHead
#Region "Variables"
    Public Receipt_Location As String = Nothing
    Public Receipt_Tax_Amt As Double = 0
    Public Invoice_Tax_Amt As Double = 0
    Public Document_Date As DateTime
    Public Doc_Code As String = Nothing
    Public Receipt_No As String = Nothing
    Public Description As String = Nothing
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Dim Created_By As String = Nothing
    Dim Created_Date As DateTime = Nothing
    Dim Modify_By As String = Nothing
    Dim Modify_Date As DateTime = Nothing
    Dim Comp_Code As String = Nothing
    Public UOMMO As String = Nothing
    Public Arr As List(Of clsReceiptInvoiceDetails) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsReceiptInvoiceHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, "", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsReceiptInvoiceHead, ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_Receipt_InvoiceMapping_Detail where Doc_Code='" + obj.Doc_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            If clsCommon.myLen(strAdjustmentNoTemp) > 0 Then
                obj.Doc_Code = strAdjustmentNoTemp
                'isNewEntry = True
            Else
                isNewEntry = True
                If isNewEntry Then
                    obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.ReceiptInvoiceMapping, "", "")
                End If
            End If
            If (clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim dblchkBalnce As Double = GetReceiptBalanceTaxAmount(obj.Receipt_No, obj.Doc_Code, trans)
            Dim dblcalAmt As Double = 0
            For Each objtr As clsReceiptInvoiceDetails In obj.Arr
                dblcalAmt += objtr.Total_Tax_Amt
            Next
            If dblcalAmt > dblchkBalnce Then
                Throw New Exception("Receipt balance tax amount" + clsCommon.myCstr(dblchkBalnce) + " and used amount " + clsCommon.myCstr(dblcalAmt))
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Receipt_No", obj.Receipt_No)
            clsCommon.AddColumnsForChange(coll, "Receipt_Location", obj.Receipt_Location)
            clsCommon.AddColumnsForChange(coll, "Receipt_Tax_Amt", obj.Receipt_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Invoice_Tax_Amt", obj.Invoice_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Receipt_InvoiceMapping_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Receipt_InvoiceMapping_Head", OMInsertOrUpdate.Update, "Doc_Code='" + obj.Doc_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsReceiptInvoiceDetails.SaveData(obj.Doc_Code, Arr, isNewEntry, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType) As clsReceiptInvoiceHead

        Dim obj As clsReceiptInvoiceHead = Nothing
        Dim qry As String = "SELECT * from TSPL_Receipt_InvoiceMapping_Head  where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and Doc_Code = (select MIN(Doc_Code) from TSPL_Receipt_InvoiceMapping_Head where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and Doc_Code = (select Max(Doc_Code) from TSPL_Receipt_InvoiceMapping_Head where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and Doc_Code = (select Min(Doc_Code) from TSPL_Receipt_InvoiceMapping_Head where Doc_Code>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and Doc_Code = (select Max(Doc_Code) from TSPL_Receipt_InvoiceMapping_Head where Doc_Code<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and Doc_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsReceiptInvoiceHead()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Receipt_No = clsCommon.myCstr(dt.Rows(0)("Receipt_No"))
            obj.Receipt_Location = clsCommon.myCstr(dt.Rows(0)("Receipt_Location"))
            obj.Invoice_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Invoice_Tax_Amt"))
            obj.Receipt_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Receipt_Tax_Amt"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("isPosted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            qry = "SELECT* from TSPL_Receipt_InvoiceMapping_Detail  where  Doc_Code='" + obj.Doc_Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsReceiptInvoiceDetails)
                Dim objTr As clsReceiptInvoiceDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New clsReceiptInvoiceDetails()
                    objTr.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                    objTr.InvoiceNo = clsCommon.myCstr(dr("InvoiceNo"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.InvoiceLocation = clsCommon.myCstr(dr("InvoiceLocation"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Tax1_Amt = clsCommon.myCdbl(dr("Tax1_Amt"))
                    objTr.Tax2_Amt = clsCommon.myCdbl(dr("Tax2_Amt"))
                    objTr.Tax3_Amt = clsCommon.myCdbl(dr("Tax3_Amt"))
                    objTr.Tax4_Amt = clsCommon.myCdbl(dr("Tax4_Amt"))
                    objTr.Tax5_Amt = clsCommon.myCdbl(dr("Tax5_Amt"))
                    objTr.Tax6_Amt = clsCommon.myCdbl(dr("Tax6_Amt"))
                    objTr.Tax7_Amt = clsCommon.myCdbl(dr("Tax7_Amt"))
                    objTr.Tax8_Amt = clsCommon.myCdbl(dr("Tax8_Amt"))
                    objTr.Tax9_Amt = clsCommon.myCdbl(dr("Tax9_Amt"))
                    objTr.Tax10_Amt = clsCommon.myCdbl(dr("Tax10_Amt"))

                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New clsReceiptInvoiceHead()
        obj = clsReceiptInvoiceHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                If (clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted .")
                End If
                Dim qry As String = "delete from TSPL_Receipt_InvoiceMapping_Detail where Doc_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Receipt_InvoiceMapping_Head where Doc_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsReceiptInvoiceDetails In obj.Arr
                        qry = "Update tspl_sd_sale_invoice_head set IsAdvanceTaxGlEntry=0  where Document_Code ='" + objTr.InvoiceNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Next
                End If

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
    Public Shared Function CreateGLEntryForAllCases(ByVal obj As clsReceiptInvoiceHead, ByVal trans As SqlTransaction) As Boolean
        Dim ArryLst As ArrayList = New ArrayList()
        Dim intInvoiceLocCount As Integer = 0
        Dim intInvoiceCount As Integer = 0
        Dim objRec As clsRcptEntryHeader
        objRec = clsRcptEntryHeader.GetData(obj.Receipt_No, NavigatorType.Current, trans)
        Dim StrInvoice = clsDBFuncationality.getSingleValue("select isnull((Select distinct ' '+TSPL_Receipt_InvoiceMapping_Detail.InvoiceNo+' ,  ' from TSPL_Receipt_InvoiceMapping_Detail  where  TSPL_Receipt_InvoiceMapping_Detail.Doc_CODE='" & obj.Doc_Code & "' for xml path('')),'')  as DocNo ", trans)
        Dim strRemarks = "Advance Tax Entry for Customer : " + objRec.Cust_Code + " Receipt No : " + obj.Receipt_No & " Incoice No : " + StrInvoice + " "
        intInvoiceCount = obj.Arr.Count
        For Each objTr As clsReceiptInvoiceDetails In obj.Arr
            If Not clsCommon.CompairString(obj.Receipt_Location, objTr.InvoiceLocation) = CompairStringResult.Equal Then
                intInvoiceLocCount += 1
            End If
        Next
 
        Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
        If objRec.Tax_Amount_Advance > 0 Then
            Dim objTM As clsTaxMaster
            If objRec.TAX1_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.TAX1, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.TAX1)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.TAX1)
                    End If

                    'Cases start here
                    If ((obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0) OrElse (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX1_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX1_Amt}
                        ArryLst.Add(Acc3)
                    ElseIf obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then

                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax1_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax1_Amt}
                            ArryLst.Add(Acc3)

                        Next
                    ElseIf ((obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax1_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax1_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax1_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax1_Amt}
                                ArryLst.Add(Acc5)

                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax1_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax1_Amt}
                                ArryLst.Add(Acc3)
                            End If


                        Next
                    ElseIf (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX1_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX1_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX1_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX1_Amt}
                                ArryLst.Add(Acc5)

                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX1_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX1_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next

                    ElseIf obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX2_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax2, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax2)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax2)
                    End If

                    'Cases start here
                    If ((obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0) OrElse (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX2_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX2_Amt}
                        ArryLst.Add(Acc3)
                    ElseIf obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then

                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax2_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax2_Amt}
                            ArryLst.Add(Acc3)

                        Next
                    ElseIf ((obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax2_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax2_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax2_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax2_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax2_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax2_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX2_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX2_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX2_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX2_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX2_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX2_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX3_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.TAX3, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.TAX3)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.TAX3)
                    End If

                    'Cases start here
                    If ((obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0) OrElse (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX3_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX3_Amt}
                        ArryLst.Add(Acc3)
                    ElseIf obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then

                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax3_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax3_Amt}
                            ArryLst.Add(Acc3)

                        Next
                    ElseIf ((obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax3_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax3_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax3_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax3_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax3_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax3_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX3_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX3_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX3_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX3_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX3_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX3_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX4_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.TAX4, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.TAX4)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.TAX4)
                    End If

                    'Cases start here
                    If ((obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0) OrElse (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX4_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX4_Amt}
                        ArryLst.Add(Acc3)
                    ElseIf obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then

                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax4_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax4_Amt}
                            ArryLst.Add(Acc3)

                        Next
                    ElseIf ((obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax4_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax4_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax4_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax4_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax4_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax4_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX4_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX4_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX4_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX4_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX4_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX4_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX5_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax5, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax5)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax5)
                    End If

                    'Cases start here
                    If ((obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0) OrElse (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX5_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX5_Amt}
                        ArryLst.Add(Acc3)
                    ElseIf obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then
                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax5_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax5_Amt}
                            ArryLst.Add(Acc3)
                        Next
                    ElseIf ((obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0) OrElse (obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax5_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax5_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax5_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax5_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax5_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax5_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf (obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)
                            If isApplyBrachAccounting AndAlso Not clsCommon.CompairString(strInvoiceLoc, objRec.Location_GL_Code) = CompairStringResult.Equal Then


                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX5_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX5_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX5_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX5_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX5_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX5_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX6_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax6, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax6)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax5)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX6_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX6_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If

            If objRec.TAX7_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax7, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax7)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax7)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX7_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX7_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If

            If objRec.TAX8_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax8, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax8)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax8)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX8_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX8_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If

            If objRec.TAX9_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax9, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax9)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax9)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX9_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX9_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If

            If objRec.TAX10_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax10, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax10)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax10)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX10_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX10_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If
        End If
        Dim coll As New Hashtable()
        If clsCommon.myLen(objRec.CURRENCY_CODE) > 0 AndAlso clsCommon.CompairString(objRec.CURRENCY_CODE, objCommonVar.BaseCurrencyCode) <> CompairStringResult.Equal Then
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", objRec.CURRENCY_CODE)
            clsCommon.AddColumnsForChange(coll, "ConvRate", objRec.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", objRec.ConvRate)
        End If
        clsJournalMaster.FunGrnlEntryWithTrans(objRec.Location_GL_Code, True, trans, clsCommon.myCDate(obj.Document_Date), obj.Description, "RC-AD", "Receipt Advance Tax Knock off", obj.Doc_Code, obj.Description, "C", objRec.Cust_Code, objRec.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, objRec.Reference, strRemarks, Nothing, coll)

        Return True

    End Function

    Public Shared Function CreateGLEntryForAllCasesOLD(ByVal obj As clsReceiptInvoiceHead, ByVal trans As SqlTransaction) As Boolean
        Dim TAX1_Amt As Double = 0
        Dim TAX2_Amt As Double = 0
        Dim TAX3_Amt As Double = 0
        Dim TAX4_Amt As Double = 0
        Dim TAX5_Amt As Double = 0
        Dim TAX6_Amt As Double = 0
        Dim TAX7_Amt As Double = 0
        Dim TAX8_Amt As Double = 0
        Dim TAX9_Amt As Double = 0
        Dim TAX10_Amt As Double = 0

        Dim TAX1Invoice_Amt As Double = 0
        Dim TAX2Invoice_Amt As Double = 0
        Dim TAX3Invoice_Amt As Double = 0
        Dim TAX4Invoice_Amt As Double = 0
        Dim TAX5Invoice_Amt As Double = 0
        Dim ArryLst As ArrayList = New ArrayList()
        Dim intInvoiceLocCount As Integer = 0
        Dim intInvoiceCount As Integer = 0
        Dim objRec As clsRcptEntryHeader
        objRec = clsRcptEntryHeader.GetData(obj.Receipt_No, NavigatorType.Current, trans)
        Dim StrInvoice = clsDBFuncationality.getSingleValue("select isnull((Select distinct ' '+TSPL_Receipt_InvoiceMapping_Detail.InvoiceNo+' ,  ' from TSPL_Receipt_InvoiceMapping_Detail  where  TSPL_Receipt_InvoiceMapping_Detail.Doc_CODE='" & obj.Doc_Code & "' for xml path('')),'')  as DocNo ", trans)
        Dim strRemarks = "Advance Tax Entry for Customer : " + objRec.Cust_Code + " Receipt No : " + obj.Receipt_No & " Incoice No : " + StrInvoice + " "
        intInvoiceCount = obj.Arr.Count
        For Each objTr As clsReceiptInvoiceDetails In obj.Arr
            If Not clsCommon.CompairString(obj.Receipt_Location, objTr.InvoiceLocation) = CompairStringResult.Equal Then
                intInvoiceLocCount += 1
            End If
        Next
        Dim qry As String = "select sum(isnull(TAX1_Amt,0) * RI) as TAX1_Amt,sum(isnull(TAX2_Amt,0) * RI) as TAX2_Amt,sum(isnull(TAX3_Amt,0) * RI) as TAX3_Amt, " & _
            "sum(isnull(TAX4_Amt,0) * RI) as TAX4_Amt,sum(isnull(TAX5_Amt,0) * RI) as TAX5_Amt ,sum(isnull(TAX6_Amt,0) * RI) as TAX6_Amt, " & _
            "sum(isnull(TAX7_Amt,0) * RI) as TAX7_Amt,sum(isnull(TAX8_Amt,0) * RI) as TAX8_Amt,sum(isnull(TAX9_Amt,0) * RI) as TAX9_Amt, " & _
            "sum(isnull(TAX10_Amt,0) * RI) as TAX10_Amt from (  " & _
            "select tax1_amt,tax2_amt,tax3_amt,tax4_amt,tax5_amt,tax6_amt,tax7_amt,tax8_amt,tax9_amt,tax10_amt,1 as RI from TSPL_RECEIPT_HEADER where Receipt_No='" & obj.Receipt_No & "'  " & _
            "union all  " & _
            "select sum(tax1_amt) as tax1_amt,sum(tax2_amt) as tax2_amt,sum(tax3_amt) as tax3_amt,sum(tax4_amt) as tax4_amt,sum(tax5_amt) as tax5_amt, " & _
            "sum(tax6_amt) as tax6_amt,sum(tax7_amt) as tax7_amt,sum(tax8_amt) as tax8_amt,sum(tax9_amt) as tax9_amt,sum(tax10_amt) as tax10_amt, " & _
            "-1 as RI from TSPL_RECEIPT_ADVANCE_ADJUSTMENT_KNOCKOFF  where Receipt_no='" & obj.Receipt_No & "' ) a "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            objRec.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            objRec.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            objRec.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            objRec.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            objRec.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            objRec.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            objRec.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            objRec.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            objRec.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            objRec.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
        End If
        qry = "select sum(Tax1_Amt) as Tax1_Amt,sum(Tax2_Amt) as Tax2_Amt,sum(Tax3_Amt) as Tax3_Amt,sum(Tax4_Amt) as Tax4_Amt,sum(Tax5_Amt) as Tax5_Amt from TSPL_RECEIPT_INVOICEMAPPING_DETAIL where Doc_CODE='" & obj.Doc_Code & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            TAX1Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            TAX2Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            TAX3Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            TAX4Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            TAX5Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
        End If

        'If obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then ' CASE IF Amount and location same

        'ElseIf obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then ' CASE Advance Amount > Invoice and location same

        'ElseIf obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then ' CASE Advance Amount > Invoice and location Different

        'ElseIf obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount = 0 Then ' CASE Advance Amount < Invoice and location same

        'ElseIf obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1 Then ' CASE Advance Amount = Invoice and location Different and one invoice

        'ElseIf obj.Receipt_Tax_Amt = obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then ' CASE Advance Amount = Invoice and location Different and multiple invoices

        'ElseIf obj.Receipt_Tax_Amt < obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then ' CASE Advance Amount = Invoice and location Different and multiple invoices

        'ElseIf obj.Receipt_Tax_Amt > obj.Invoice_Tax_Amt AndAlso intInvoiceLocCount > 0 Then ' CASE Advance Amount = Invoice and location Different and multiple invoices

        'End If
        Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
        If objRec.Tax_Amount_Advance > 0 Then
            Dim objTM As clsTaxMaster
            If objRec.TAX1_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.TAX1, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.TAX1)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.TAX1)
                    End If

                    'Cases start here
                    If ((objRec.TAX1_Amt = TAX1Invoice_Amt AndAlso intInvoiceLocCount = 0) OrElse (objRec.TAX1_Amt < TAX1Invoice_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX1_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX1_Amt}
                        ArryLst.Add(Acc3)
                        TAX1_Amt = objRec.TAX1_Amt
                    ElseIf objRec.TAX1_Amt > TAX1Invoice_Amt AndAlso intInvoiceLocCount = 0 Then
                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax1_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax1_Amt}
                            ArryLst.Add(Acc3)
                            TAX1_Amt += objTR.Tax1_Amt
                        Next
                    ElseIf ((objRec.TAX1_Amt > TAX1Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX1_Amt = TAX1Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX1_Amt = TAX1Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax1_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax1_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax1_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax1_Amt}
                                ArryLst.Add(Acc5)

                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax1_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objTR.Tax1_Amt}
                                ArryLst.Add(Acc3)
                            End If
                            TAX1_Amt += objTR.Tax1_Amt

                        Next
                    ElseIf (objRec.TAX1_Amt < TAX1Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX1_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX1_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX1_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX1_Amt}
                                ArryLst.Add(Acc5)

                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX1_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objRec.TAX1_Amt}
                                ArryLst.Add(Acc3)
                            End If
                            TAX1_Amt += objRec.TAX1_Amt
                        Next

                    ElseIf objRec.TAX1_Amt < TAX1Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX2_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax2, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax2)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax2)
                    End If

                    'Cases start here
                    If ((objRec.TAX2_Amt = TAX2Invoice_Amt AndAlso intInvoiceLocCount = 0) OrElse (objRec.TAX2_Amt < TAX2Invoice_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX2_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX2_Amt}
                        ArryLst.Add(Acc3)
                        TAX2_Amt = objRec.TAX2_Amt
                    ElseIf objRec.TAX2_Amt > TAX2Invoice_Amt AndAlso intInvoiceLocCount = 0 Then
                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax2_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax2_Amt}
                            ArryLst.Add(Acc3)
                            TAX2_Amt += objTR.Tax2_Amt
                        Next
                    ElseIf ((objRec.TAX2_Amt > TAX2Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX2_Amt = TAX2Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX2_Amt = TAX2Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax2_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax2_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax2_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax2_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax2_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objTR.Tax2_Amt}
                                ArryLst.Add(Acc3)
                            End If
                            TAX2_Amt += objTR.Tax2_Amt
                        Next
                    ElseIf (objRec.TAX2_Amt < TAX2Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX2_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX2_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX2_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX2_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX2_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objRec.TAX2_Amt}
                                ArryLst.Add(Acc3)
                            End If
                            TAX2_Amt += objRec.TAX2_Amt
                        Next
                    ElseIf objRec.TAX2_Amt < TAX2Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX3_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.TAX3, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.TAX3)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.TAX3)
                    End If

                    'Cases start here
                    If ((objRec.TAX3_Amt = TAX3Invoice_Amt AndAlso intInvoiceLocCount = 0) OrElse (objRec.TAX3_Amt < TAX3Invoice_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX3_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX3_Amt}
                        ArryLst.Add(Acc3)
                        TAX3_Amt = objRec.TAX3_Amt
                    ElseIf objRec.TAX3_Amt > TAX3Invoice_Amt AndAlso intInvoiceLocCount = 0 Then
                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax3_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax3_Amt}
                            ArryLst.Add(Acc3)
                            TAX3_Amt += objTR.Tax3_Amt
                        Next
                    ElseIf ((objRec.TAX3_Amt > TAX3Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX3_Amt = TAX3Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX3_Amt = TAX3Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax3_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax3_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax3_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax3_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax3_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objTR.Tax3_Amt}
                                ArryLst.Add(Acc3)
                            End If
                            TAX3_Amt += objTR.Tax3_Amt
                        Next
                    ElseIf (objRec.TAX3_Amt < TAX3Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX3_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX3_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX3_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX3_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX3_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objRec.TAX3_Amt}
                                ArryLst.Add(Acc3)
                            End If
                            TAX3_Amt += objRec.TAX3_Amt
                        Next
                    ElseIf objRec.TAX3_Amt < TAX3Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX4_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.TAX4, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.TAX4)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.TAX4)
                    End If

                    'Cases start here
                    If ((objRec.TAX4_Amt = TAX4Invoice_Amt AndAlso intInvoiceLocCount = 0) OrElse (objRec.TAX4_Amt < TAX4Invoice_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX4_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX4_Amt}
                        ArryLst.Add(Acc3)
                        TAX4_Amt = objRec.TAX4
                    ElseIf objRec.TAX4_Amt > TAX4Invoice_Amt AndAlso intInvoiceLocCount = 0 Then
                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax4_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax4_Amt}
                            ArryLst.Add(Acc3)
                            TAX4_Amt += objTR.Tax4_Amt
                        Next
                    ElseIf ((objRec.TAX4_Amt > TAX4Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX4_Amt = TAX4Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX4_Amt = TAX4Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax4_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax4_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax4_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax4_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax4_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objTR.Tax4_Amt}
                                ArryLst.Add(Acc3)
                            End If
                            TAX4_Amt += objTR.Tax4_Amt
                        Next
                    ElseIf (objRec.TAX4_Amt < TAX4Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX4_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX4_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX4_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX4_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX4_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objRec.TAX4_Amt}
                                ArryLst.Add(Acc3)
                            End If
                            TAX4_Amt += objRec.TAX4_Amt
                        Next
                    ElseIf objRec.TAX4_Amt < TAX4Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX5_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax5, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax5)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax5)
                    End If

                    'Cases start here
                    If ((objRec.TAX5_Amt = TAX5Invoice_Amt AndAlso intInvoiceLocCount = 0) OrElse (objRec.TAX5_Amt < TAX5Invoice_Amt AndAlso intInvoiceLocCount = 0)) Then
                        '1 CASE  ( IF Amount and location same )  - 
                        '3 CASE (3 CASE Advance Amount < Invoice and location same)
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX5_Amt}
                        ArryLst.Add(Acc2)
                        Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX5_Amt}
                        ArryLst.Add(Acc3)
                    ElseIf objRec.TAX5_Amt > TAX5Invoice_Amt AndAlso intInvoiceLocCount = 0 Then
                        '2 CASE Advance Amount > Invoice and location same
                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax5_Amt}
                            ArryLst.Add(Acc2)
                            Dim Acc3() As String = {objTM.Tax_Liability_Account, objTR.Tax5_Amt}
                            ArryLst.Add(Acc3)
                        Next
                    ElseIf ((objRec.TAX5_Amt > TAX5Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX5_Amt = TAX5Invoice_Amt AndAlso intInvoiceLocCount > 0) OrElse (objRec.TAX5_Amt = TAX5Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1)) Then
                        '4 CASE( Advance Amount = Invoice Amount and location Different ) 
                        '5 CASE Advance Amount = Invoice and location Different and multiple invoices
                        '7(CASE Advance Amount > Invoice Amount and location Different)


                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax5_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objTR.Tax5_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objTR.Tax5_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objTR.Tax5_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objTR.Tax5_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objTR.Tax5_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf (objRec.TAX5_Amt < TAX5Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount = 1) Then
                        ' CASE( Advance Amount < Invoice Amount and location Different but only one invoice ) 

                        objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                        For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                            If isApplyBrachAccounting Then
                                Dim strInvoiceLoc = clsERPFuncationality.GetLocationSegment(objTR.InvoiceLocation, trans)

                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, strInvoiceLoc, True, trans)
                                Dim strInvoiceBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(strInvoiceLoc, objRec.Location_GL_Code, trans)
                                If clsCommon.myLen(strInvoiceBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + strInvoiceLoc + " and to location " + objRec.Location_GL_Code)
                                End If
                                Dim strReceiptBranchAcct As String = ClsBranchAccountMapping.GetBranchAccount(objRec.Location_GL_Code, strInvoiceLoc, trans)
                                If clsCommon.myLen(strReceiptBranchAcct) <= 0 Then
                                    Throw New Exception("Please set Branch account mapping with from location " + objRec.Location_GL_Code + " and to location " + strInvoiceLoc)
                                End If
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX5_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {strReceiptBranchAcct, objRec.TAX5_Amt}
                                ArryLst.Add(Acc3)
                                Dim Acc4() As String = {objTM.Tax_Liability_Account, objRec.TAX5_Amt}
                                ArryLst.Add(Acc4)
                                Dim Acc5() As String = {strInvoiceBranchAcct, -1 * objRec.TAX5_Amt}
                                ArryLst.Add(Acc5)
                            Else
                                objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                                Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX5_Amt}
                                ArryLst.Add(Acc2)
                                Dim Acc3() As String = {objTM.Tax_Liability_Account, -1 * objRec.TAX5_Amt}
                                ArryLst.Add(Acc3)
                            End If

                        Next
                    ElseIf objRec.TAX5_Amt < TAX5Invoice_Amt AndAlso intInvoiceLocCount > 0 AndAlso intInvoiceCount > 1 Then '6 CASE Advance Amount = Invoice and location Different and multiple invoices
                        Throw New Exception("Multiple Invoice is applied and Total Invoice Tax Amount is greater than Receipt Tax Amount So amount cannot be knock off")

                    End If

                End If
            End If

            If objRec.TAX6_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax6, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax6)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax5)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX6_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX6_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If

            If objRec.TAX7_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax7, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax7)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax7)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX7_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX7_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If

            If objRec.TAX8_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax8, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax8)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax8)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX8_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX8_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If

            If objRec.TAX9_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax9, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax9)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax9)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX9_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX9_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If

            If objRec.TAX10_Amt > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objRec.tax10, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                        Throw New Exception("Please set Tax Deposit Control Account of Tax Authority " + objRec.tax10)
                    End If
                    objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objRec.Location_GL_Code, True, trans)
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablity Account of Tax Authority " + objRec.tax10)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, objRec.Location_GL_Code, True, trans)
                    Dim Acc2() As String = {objTM.DepositControl, -1 * objRec.TAX10_Amt}
                    ArryLst.Add(Acc2)
                    Dim Acc3() As String = {objTM.Tax_Liability_Account, objRec.TAX10_Amt}
                    ArryLst.Add(Acc3)
                End If
            End If
        End If
        If (TAX1_Amt > 0 OrElse TAX2_Amt > 0 OrElse TAX3_Amt > 0 OrElse TAX4_Amt > 0 OrElse TAX5_Amt > 0 OrElse TAX6_Amt > 0 OrElse TAX7_Amt > 0 OrElse TAX8_Amt > 0 OrElse TAX9_Amt > 0 OrElse TAX10_Amt > 0) Then
            qry = "insert  into TSPL_RECEIPT_ADVANCE_ADJUSTMENT_KNOCKOFF(Receipt_No,TAX1_Amt,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt ) " + Environment.NewLine + _
                " values ('" & obj.Receipt_No & "','" & TAX1_Amt & "','" & TAX2_Amt & "','" & TAX3_Amt & "','" & TAX4_Amt & "','" & TAX5_Amt & "','" & TAX6_Amt & "','" & TAX7_Amt & "','" & TAX8_Amt & "','" & TAX9_Amt & "','" & TAX10_Amt & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If

        Dim coll As New Hashtable()
        If clsCommon.myLen(objRec.CURRENCY_CODE) > 0 AndAlso clsCommon.CompairString(objRec.CURRENCY_CODE, objCommonVar.BaseCurrencyCode) <> CompairStringResult.Equal Then
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", objRec.CURRENCY_CODE)
            clsCommon.AddColumnsForChange(coll, "ConvRate", objRec.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", objRec.ConvRate)
        End If
        clsJournalMaster.FunGrnlEntryWithTrans(objRec.Location_GL_Code, False, trans, clsCommon.myCDate(obj.Document_Date), obj.Description, "RC-AD", "Receipt Advance Tax Knock off", obj.Doc_Code, obj.Description, "C", objRec.Cust_Code, objRec.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, objRec.Reference, strRemarks, Nothing, coll)

        Return True

    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If
        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
        Dim obj As clsReceiptInvoiceHead = clsReceiptInvoiceHead.GetData(strDocNo, NavigatorType.Current)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (isCheckForPosted AndAlso obj.POSTED = 1) Then
            Throw New Exception("Already Post on :")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Update TSPL_Receipt_InvoiceMapping_Head set isPOSTED=1, Modified_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objTr As clsReceiptInvoiceDetails In obj.Arr
                    qry = "Update tspl_sd_sale_invoice_head set IsAdvanceTaxGlEntry=1  where Document_Code ='" + objTr.InvoiceNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
            End If
            CreateGLEntryForAllCases(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim obj As clsReceiptInvoiceHead = clsReceiptInvoiceHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select isPosted from TSPL_RECEIPT_INVOICEMAPPING_HEAD where Doc_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='RC-AD' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            For Each objTR As clsReceiptInvoiceDetails In obj.Arr
                Qry = "update TSPL_SD_SALE_INVOICE_HEAD set IsAdvanceTaxGlEntry=0  where Document_Code='" & objTR.InvoiceNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Next

            Qry = "update TSPL_RECEIPT_INVOICEMAPPING_HEAD set isPosted=0 where Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetReceiptBalanceTaxAmount(ByVal strReceiptNo As String, ByVal strCurrDocNo As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = "select sum(Tax_Amount_Advance*RI) as BalanceAmt from (" + Environment.NewLine + _
        "Select TSPL_RECEIPT_HEADER.Receipt_No,TSPL_RECEIPT_HEADER.Tax_Amount_Advance,1 as RI from  TSPL_RECEIPT_HEADER WHERE Receipt_No='" + strReceiptNo + "' " + Environment.NewLine + _
        "union all" + Environment.NewLine + _
        "select TSPL_Receipt_InvoiceMapping_Head.Receipt_No,TSPL_Receipt_InvoiceMapping_detail.Total_Tax_Amt,-1 as RI from TSPL_Receipt_InvoiceMapping_detail" + Environment.NewLine + _
        "left outer join TSPL_Receipt_InvoiceMapping_Head on TSPL_Receipt_InvoiceMapping_Head.Doc_Code=TSPL_Receipt_InvoiceMapping_detail.Doc_CODE" + Environment.NewLine + _
         "where TSPL_Receipt_InvoiceMapping_Head.Receipt_No='" + strReceiptNo + "'  and TSPL_Receipt_InvoiceMapping_Head.Doc_CODE not in ('" + strCurrDocNo + "' )" + Environment.NewLine + _
         ")x group by Receipt_No"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class
Public Class clsReceiptInvoiceDetails
#Region "Variables"
    Public Line_No As String = Nothing
    Public Doc_Code As String = Nothing
    Public InvoiceNo As String = Nothing
    Public Remarks As String = Nothing
    Public InvoiceLocation As String = Nothing
    Public Total_Tax_Amt As Double = 0
    Public Tax1_Amt As Double = 0
    Public Tax2_Amt As Double = 0
    Public Tax3_Amt As Double = 0
    Public Tax4_Amt As Double = 0
    Public Tax5_Amt As Double = 0
    Public Tax6_Amt As Double = 0
    Public Tax7_Amt As Double = 0
    Public Tax8_Amt As Double = 0
    Public Tax9_Amt As Double = 0
    Public Tax10_Amt As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsReceiptInvoiceDetails), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objtr As clsReceiptInvoiceDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "InvoiceNo", objtr.InvoiceNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                clsCommon.AddColumnsForChange(coll, "InvoiceLocation", objtr.InvoiceLocation)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", objtr.Total_Tax_Amt)

                Dim qry = "select Total_Tax_Amt, TAX1_Amt,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & objtr.InvoiceNo & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt")) > 0 Then
                        objtr.Tax1_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax2_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax3_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax4_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax5_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax6_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax7_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax8_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax9_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                        objtr.Tax10_Amt = objtr.Total_Tax_Amt * clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt")) / clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                    End If
                End If

                clsCommon.AddColumnsForChange(coll, "Tax1_Amt", objtr.Tax1_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax2_Amt", objtr.Tax2_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax3_Amt", objtr.Tax3_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax4_Amt", objtr.Tax4_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax5_Amt", objtr.Tax5_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax6_Amt", objtr.Tax6_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax7_Amt", objtr.Tax7_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax8_Amt", objtr.Tax8_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax9_Amt", objtr.Tax9_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax10_Amt", objtr.Tax10_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Receipt_InvoiceMapping_Detail", OMInsertOrUpdate.Insert, "", trans)

                'qry = "Update TSPL_Receipt_InvoiceMapping_Detail set Tax1_Amt='" & objtr.Tax1_Amt & "', " & _
                '    "Tax2_Amt='" & objtr.Tax2_Amt & "', Tax3_Amt='" & objtr.Tax3_Amt & "', Tax4_Amt='" & objtr.Tax4_Amt & "',  " & _
                '    "Tax5_Amt='" & objtr.Tax5_Amt & "', Tax6_Amt='" & objtr.Tax6_Amt & "', Tax7_Amt='" & objtr.Tax7_Amt & "',  " & _
                '    "Tax8_Amt='" & objtr.Tax8_Amt & "', Tax9_Amt='" & objtr.Tax9_Amt & "', Tax10_Amt='" & objtr.Tax10_Amt & "' where InvoiceNo='" & objtr.InvoiceNo & "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next
        End If
        Return True
    End Function
End Class
