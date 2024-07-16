Imports common
Imports System.Data.SqlClient

Public Class clsPaymentAdjustmentEntry
#Region "Variables"
    Public Adjustment_No As String = ""
    Public Description As String = ""
    Public Adjustment_Date As DateTime
    Public Post_Date As DateTime
    Public Vendor_No As String = ""
    Public Vendor_Name As String = ""
    Public Doc_No As String = ""
    Public Doc_Amount As Decimal = 0.0
    Public Bal_Amount As Decimal = 0.0
    Public Remarks As String = ""
    Public Adjustment_Amount As Decimal = 0.0
    Public Round_Off_Amount As Decimal
    Public Adjustment_Amount_Before_RO As Decimal
    Public Is_Post As Char = "N"
    Public Arr As List(Of clsPaymentAdjustmentEntryDetail) = Nothing
    Public Adjust_Type As String = "P"
    Public Against_Payment_Process As String = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsPaymentAdjustmentEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsPaymentAdjustmentEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Adjustment_No, "TSPL_Payment_Adjustment_Header", "Adjustment_No", "TSPL_Payment_Adjustment_Detail", "Adjustment_No", trans)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Adjustment_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vendor_No", obj.Vendor_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Amount", obj.Doc_Amount)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Against_Payment_Process", obj.Against_Payment_Process, True)

            obj.Adjustment_Amount_Before_RO = obj.Adjustment_Amount
            obj.Round_Off_Amount = 0
            If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoRoundOffSeprateAccountOnVendorTransaction, clsFixedParameterCode.AutoRoundOffSeprateAccountOnVendorTransaction, trans)) = 1) Then
                obj.Round_Off_Amount = Math.Round(obj.Adjustment_Amount_Before_RO, 0, MidpointRounding.AwayFromZero) - obj.Adjustment_Amount_Before_RO
                obj.Adjustment_Amount = Math.Round(obj.Adjustment_Amount, 0, MidpointRounding.AwayFromZero)
            End If
            clsCommon.AddColumnsForChange(coll, "Adjustment_Amount_Before_RO", obj.Adjustment_Amount_Before_RO)
            clsCommon.AddColumnsForChange(coll, "Round_Off_Amount", obj.Round_Off_Amount)
            clsCommon.AddColumnsForChange(coll, "Adjustment_Amount", obj.Adjustment_Amount)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If clsCommon.myLen(obj.Adjust_Type) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Adjust_Type", obj.Adjust_Type)
            End If

            If isNewEntry Then
                obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Adjustment_Date), clsDocType.PaymentAdjustmentEntry, "", "")
                If (clsCommon.myLen(obj.Adjustment_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Adjustment_No", obj.Adjustment_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Payment_Adjustment_Header", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Payment_Adjustment_Header", OMInsertOrUpdate.Update, "TSPL_Payment_Adjustment_Header.Adjustment_No='" + obj.Adjustment_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsPaymentAdjustmentEntryDetail.SaveData(obj.Adjustment_No, Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsPaymentAdjustmentEntry
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strAdjNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPaymentAdjustmentEntry
        Dim obj As clsPaymentAdjustmentEntry = Nothing
        Dim qry As String = "SELECT [Adjustment_No], [TSPL_Payment_Adjustment_Header].[Description], [Adjustment_Date], [Post_Date],[Vendor_No], [TSPL_Payment_Adjustment_Header].[Vendor_Name],[Doc_No],[Doc_Amount]," &
       "  0 as BalanceAmt," &
       " [TSPL_Payment_Adjustment_Header].[Remarks], [Adjustment_Amount], ISNULL([Is_Post],'N') as Is_Post,[TSPL_Payment_Adjustment_Header].Adjust_Type,TSPL_Payment_Adjustment_Header.Adjustment_Amount_Before_RO,TSPL_Payment_Adjustment_Header.Round_Off_Amount,TSPL_Payment_Adjustment_Header.Against_Payment_Process FROM [TSPL_Payment_Adjustment_Header]" &
       " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No= TSPL_Payment_Adjustment_Header.Doc_No where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Payment_Adjustment_Header.Adjustment_No = (select MIN(Adjustment_No) from TSPL_Payment_Adjustment_Header)"
            Case NavigatorType.Last
                qry += " and TSPL_Payment_Adjustment_Header.Adjustment_No = (select Max(Adjustment_No) from TSPL_Payment_Adjustment_Header)"
            Case NavigatorType.Next
                qry += " and TSPL_Payment_Adjustment_Header.Adjustment_No = (select Min(Adjustment_No) from TSPL_Payment_Adjustment_Header where Adjustment_No>'" + strAdjNo + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_Payment_Adjustment_Header.Adjustment_No = (select Max(Adjustment_No) from TSPL_Payment_Adjustment_Header where Adjustment_No<'" + strAdjNo + "')"
            Case NavigatorType.Current
                qry += " and TSPL_Payment_Adjustment_Header.Adjustment_No = '" + strAdjNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPaymentAdjustmentEntry()
            obj.Adjustment_No = clsCommon.myCstr(dt.Rows(0)("Adjustment_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Adjustment_Date = clsCommon.myCstr(dt.Rows(0)("Adjustment_Date"))
            obj.Post_Date = clsCommon.myCstr(dt.Rows(0)("Post_Date"))
            obj.Vendor_No = clsCommon.myCstr(dt.Rows(0)("Vendor_No"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
            obj.Doc_Amount = clsCommon.myCdbl(dt.Rows(0)("Doc_Amount"))
            obj.Bal_Amount = clsCommon.myCdbl(dt.Rows(0)("BalanceAmt"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Adjustment_Amount = clsCommon.myCdbl(dt.Rows(0)("Adjustment_Amount"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
            obj.Adjust_Type = clsCommon.myCstr(dt.Rows(0)("Adjust_Type"))
            obj.Adjustment_Amount_Before_RO = clsCommon.myCdbl(dt.Rows(0)("Adjustment_Amount_Before_RO"))
            obj.Round_Off_Amount = clsCommon.myCdbl(dt.Rows(0)("Round_Off_Amount"))
            obj.Against_Payment_Process = clsCommon.myCstr(dt.Rows(0)("Against_Payment_Process"))

            qry = "SELECT [TSPL_Payment_Adjustment_Detail].[Adjustment_No],[TSPL_Payment_Adjustment_Detail].[Line_No],[TSPL_Payment_Adjustment_Detail].[Account_No],[TSPL_Payment_Adjustment_Detail].[Account_Description],[TSPL_Payment_Adjustment_Detail].[Amount],[TSPL_Payment_Adjustment_Detail].[Remarks],[TSPL_Payment_Adjustment_Detail].[Discount_Code],[TSPL_Payment_Adjustment_Detail].[Discount_Description],[TSPL_Payment_Adjustment_Detail].FarmerCode,[TSPL_Payment_Adjustment_Detail].FarmerName,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No  " &
            " FROM [TSPL_Payment_Adjustment_Detail] left join TSPL_MP_PAY_PROCESS_DETAIL on TSPL_MP_PAY_PROCESS_DETAIL.AP_Adjustment_No  =[TSPL_Payment_Adjustment_Detail].Adjustment_No   and TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code =[TSPL_Payment_Adjustment_Detail].FarmerCode  WHERE [TSPL_Payment_Adjustment_Detail].[Adjustment_No]='" + obj.Adjustment_No + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsPaymentAdjustmentEntryDetail)
                Dim objTr As clsPaymentAdjustmentEntryDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsPaymentAdjustmentEntryDetail
                    objTr.Adjustment_No = clsCommon.myCstr(dr("Adjustment_No"))
                    objTr.Line_No = Convert.ToInt32(clsCommon.myCdbl(dr("Line_No")))
                    objTr.Account_No = clsCommon.myCstr((dr("Account_No")))
                    objTr.Account_Description = clsCommon.myCstr(dr("Account_Description"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Discount_Code = clsCommon.myCstr(dr("Discount_Code"))
                    objTr.Discount_Description = clsCommon.myCstr(dr("Discount_Description"))
                    objTr.FarmerCode = clsCommon.myCstr(dr("FarmerCode"))
                    objTr.FarmerName = clsCommon.myCstr(dr("FarmerName"))
                    objTr.Farmer_invoice_no = clsCommon.myCstr(dr("Farmer_Invoice_No"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Payment_Adjustment_Header", "Adjustment_No", "TSPL_Payment_Adjustment_Detail", "Adjustment_No", trans)
            Dim qry As String = ""
            qry = "delete from TSPL_Payment_Adjustment_Detail where Adjustment_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Payment_Adjustment_Header where Adjustment_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getTotalUnApprovedAdjAmtOfInvoice(ByVal strsaleInvoiceNo As String, ByVal trans As SqlTransaction) As Decimal
        Try
            clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_Payment_Adjustment_Header WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No='" & strsaleInvoiceNo & "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return 0
    End Function

    Public Shared Function FunPost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            FunPost(strDocNo, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function FunPost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strQ As String
            Dim obj As New clsPaymentAdjustmentEntry
            obj = GetData(strDocNo, NavigatorType.Current, trans)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0 Then
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Document is already posted.")
                End If
                '' done by Panch Raj against Internal Ticket -MPD: Referred by Amit Sir
                If clsCommon.CompairString(obj.Adjust_Type, "R") = CompairStringResult.Equal Then
                    FunPostReverseEntry(strDocNo, trans)
                Else
                    CreateJournalEntry(obj, "", trans)
                End If

                strQ = "update TSPL_Payment_Adjustment_Header set TSPL_Payment_Adjustment_Header.is_Post = 'Y', TSPL_Payment_Adjustment_Header.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                clsPaymentAdjustmentEntry.funUpdateInvoice(obj.Adjustment_No, obj.Vendor_No, obj.Doc_No, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Payment_Adjustment_Header", "Adjustment_No", "TSPL_Payment_Adjustment_Detail", "Adjustment_No", trans)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateJournalEntry(ByVal obj As clsPaymentAdjustmentEntry, ByVal strVoucherNoifExists As String, ByVal trans As SqlTransaction) As Boolean
        Dim strQ As String
        Dim strRcvblAcc As String
        Dim ArrList As ArrayList = New ArrayList()
        Dim AdjAcc() As String
        Try
            strQ = " SELECT TSPL_VENDOR_ACCOUNT_SET.Payable_Account FROM  TSPL_VENDOR_ACCOUNT_SET  INNER JOIN " & _
                        " TSPL_VENDOR_MASTER ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  = TSPL_VENDOR_MASTER.Vendor_Account " & _
                        " where TSPL_VENDOR_MASTER.Vendor_Code ='" + obj.Vendor_No + "'"

            strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
            Dim strPINo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Against_POInvoice_No from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & obj.Doc_No & "'  ", trans))
            'Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
            Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
            ' ap invoice saves the location segment, not the location
            strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strLocation, True, trans)
            Dim CustAcc() As String = {strRcvblAcc, obj.Adjustment_Amount}
            ArrList.Add(CustAcc)

            For ii As Integer = 0 To obj.Arr.Count - 1
                obj.Arr(ii).Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Arr(ii).Account_No, funLocationByAdj(obj.Doc_No, trans), True, trans)
                Dim amt As Decimal = obj.Arr(ii).Amount
                If ii = 0 Then
                    If obj.Round_Off_Amount <> 0 Then
                        amt += obj.Round_Off_Amount
                    End If
                End If
                AdjAcc = New String() {obj.Arr(ii).Account_No, -1 * amt}
                ArrList.Add(AdjAcc)
            Next

            strQ = " select Document_Type from TSPL_Vendor_Invoice_Head where Document_No='" + obj.Doc_No + "'"
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans)), "D") = CompairStringResult.Equal Then
                Dim ArryLstNew As ArrayList = New ArrayList()
                For Each Str() As String In ArrList
                    Dim strNew() As String = {Str(0), -1 * Str(1)}
                    ArryLstNew.Add(strNew)
                Next
                clsJournalMaster.FunGrnlEntryWithTrans(strLocation, True, strVoucherNoifExists, trans, obj.Adjustment_Date, obj.Description, "AP-AD", "AP Payment Received", obj.Adjustment_No, "", "V", obj.Vendor_No, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew, , obj.Remarks, "")
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(strLocation, True, strVoucherNoifExists, trans, obj.Adjustment_Date, obj.Description, "AP-AD", "AP Payment Received", obj.Adjustment_No, "", "V", obj.Vendor_No, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , obj.Remarks, "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function FunPostReverseEntry(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strQ As String
            Dim ArrList As ArrayList = New ArrayList()
            Dim AdjAcc() As String
            Dim strRcvblAcc As String = ""
            Dim obj As New clsPaymentAdjustmentEntry
            obj = GetData(strDocNo, NavigatorType.Current, trans)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0 Then
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Document is already posted.")
                End If
                strQ = " SELECT TSPL_VENDOR_ACCOUNT_SET.Payable_Account FROM  TSPL_VENDOR_ACCOUNT_SET  INNER JOIN " &
                   " TSPL_VENDOR_MASTER ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  = TSPL_VENDOR_MASTER.Vendor_Account " &
                   " where TSPL_VENDOR_MASTER.Vendor_Code ='" + obj.Vendor_No + "'"

                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
                Dim strPINo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Against_POInvoice_No from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & obj.Doc_No & "'  ", trans))
                Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
                strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strLocation, True, trans)
                Dim CustAcc() As String = {strRcvblAcc, -1 * obj.Adjustment_Amount}
                ArrList.Add(CustAcc)
                For Each objtr As clsPaymentAdjustmentEntryDetail In obj.Arr
                    objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), True, trans)
                    AdjAcc = New String() {objtr.Account_No, objtr.Amount}
                    ArrList.Add(AdjAcc)
                Next
                clsJournalMaster.FunGrnlEntryWithTrans(strLocation, True, trans, obj.Adjustment_Date, obj.Description, "AP-AD", "AP Payment Received", strDocNo, "", "V", obj.Vendor_No, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , obj.Remarks, "")
                strQ = "update TSPL_Payment_Adjustment_Header set TSPL_Payment_Adjustment_Header.is_Post = 'Y', TSPL_Payment_Adjustment_Header.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                clsPaymentAdjustmentEntry.funUpdateInvoice(obj.Adjustment_No, obj.Vendor_No, obj.Doc_No, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Payment_Adjustment_Header", "Adjustment_No", "TSPL_Payment_Adjustment_Detail", "Adjustment_No", trans)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function funUpdateInvoice(ByVal PaymentNo As String, ByVal VendorCode As String, ByVal InvoiceNo As String, ByVal trans As SqlTransaction)
        Try
            Dim BalAmt As Decimal
            Dim Qry As String = "Select Sum(case when TSPL_Payment_Adjustment_Header.Adjust_Type ='R' then  -TSPL_Payment_Adjustment_Detail.Amount else TSPL_Payment_Adjustment_Detail.Amount end) as Amount from TSPL_Payment_Adjustment_Detail Left Outer Join TSPL_Payment_Adjustment_Header ON  TSPL_Payment_Adjustment_Detail.Adjustment_No=TSPL_Payment_Adjustment_Header.Adjustment_No where TSPL_Payment_Adjustment_Header.Vendor_No = '" + VendorCode + "' and TSPL_Payment_Adjustment_Header.Doc_No = '" + InvoiceNo + "' AND TSPL_Payment_Adjustment_Detail.Adjustment_No = '" + PaymentNo + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                BalAmt = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                ''richa 29 June 2020 , deduction amount is subtracted from adjustment
                'Dim dblDeductionAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Deduction_Amount) from TSPL_MP_PAY_PROCESS_DETAIL where vsp_code= '" + VendorCode + "' and AP_Invoice_No='" & InvoiceNo & "'", trans))
                Dim dblDeductionAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select ISNULL(MP_Total_Deduction,0) + ISNULL(NextCycleDebitNote,0) from TSPL_PAYMENT_PROCESS_DETAIL where vsp_code= '" + VendorCode + "' and AP_Invoice_No='" & InvoiceNo & "'", trans))
              
                    Dim strviBalAmt As Double = clsDBFuncationality.getSingleValue("Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD  where vendor_code = '" + VendorCode + "' AND Document_No='" + InvoiceNo + "'", trans)
                        BalAmt = BalAmt - dblDeductionAmt
                        If strviBalAmt >= BalAmt Then
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = Balance_Amt - " + clsCommon.myCstr(BalAmt) + " where vendor_code = '" + VendorCode + "' AND Document_No='" + InvoiceNo + "'", trans)
                        Else
                            Throw New Exception("Adjustment amount cannot be greater than balance amount" + Environment.NewLine + "VSP [" + VendorCode + "] Milk Amount [" + clsCommon.myCstr(strviBalAmt) + "] Sale Amount [" + clsCommon.myCstr(BalAmt) + "]")
                        End If

                'Dim strDocNo As String = clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD WHERE Against_POInvoice_No='" + InvoiceNo + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function
    'Public Shared Function FunPostForUtility(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim strQ As String
    '    Dim ArrList As ArrayList = New ArrayList()
    '    Dim AdjAcc() As String
    '    Dim CustAcc() As String
    '    Dim strVendor As String = ""
    '    Dim PostDate As DateTime
    '    Dim Desc As String = ""
    '    Dim Vendorname As String = ""
    '    Dim Remarks As String = ""
    '    Dim TAdjAmt As Decimal
    '    Dim AdjAccnt As String
    '    Dim AdjAmt As Decimal
    '    strQ = " SELECT TSPL_Payment_Adjustment_Header.Adjustment_No, TSPL_Payment_Adjustment_Header.Description, " & _
    '               "   TSPL_Payment_Adjustment_Header.Adjustment_Date, TSPL_Payment_Adjustment_Header.Post_Date, TSPL_Payment_Adjustment_Header.Vendor_No, " & _
    '               "   TSPL_Payment_Adjustment_Header.Vendor_Name, TSPL_Payment_Adjustment_Header.Doc_No,TSPL_Payment_Adjustment_Header.Remarks,  " & _
    '               "   ISNULL(TSPL_Payment_Adjustment_Header.Adjustment_Amount, 0) AS Adjustment_Amount, TSPL_Payment_Adjustment_Detail.Account_No,  " & _
    '               "   TSPL_Payment_Adjustment_Detail.Account_Description, ISNULL(TSPL_Payment_Adjustment_Detail.Amount, 0) AS Amount,  " & _
    '               "   TSPL_Payment_Adjustment_Detail.Amount as [Amt] FROM TSPL_Payment_Adjustment_Header INNER JOIN" & _
    '               "   TSPL_Payment_Adjustment_Detail ON TSPL_Payment_Adjustment_Header.Adjustment_No = TSPL_Payment_Adjustment_Detail.Adjustment_No WHERE  TSPL_Payment_Adjustment_Header.Adjustment_No = '" + strDocNo + "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
    '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '        Throw New Exception("Adjustment No not found to post")
    '    End If
    '    Dim strSaleInvNo As String = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
    '    Dim Loc As String = funLocationByAdj(strDocNo, trans)
    '    For Each dr As DataRow In dt.Rows
    '        strVendor = dr("Vendor_No").ToString()
    '        PostDate = Convert.ToDateTime(dr("Post_Date").ToString()).Date
    '        Desc = dr("Description").ToString()
    '        Vendorname = dr("Vendor_Name").ToString()
    '        Remarks = dr("Remarks").ToString()
    '        TAdjAmt = CDec(dr("Adjustment_Amount").ToString())
    '        AdjAccnt = dr("Account_No").ToString()
    '        AdjAmt = CDec(dr("Amt").ToString())
    '        AdjAcc = New String() {AdjAccnt, Convert.ToDecimal(AdjAmt)}
    '        ArrList.Add(AdjAcc)
    '    Next
    '    Dim strQuery As String = " SELECT TSPL_VENDOR_ACCOUNT_SET.Payable_Account FROM  TSPL_VENDOR_ACCOUNT_SET  INNER JOIN " & _
    '               " TSPL_VENDOR_MASTER ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  = TSPL_VENDOR_MASTER.Vendor_Account " & _
    '               " where TSPL_VENDOR_MASTER.Vendor_Code ='" + strVendor + "'"
    '    Dim strRcvblAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQuery, trans))
    '    Dim strRcvblLocAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, Loc, trans)
    '    CustAcc = New String() {strRcvblLocAcc, TAdjAmt}
    '    ArrList.Add(CustAcc)
    '    clsJournalMaster.FunGrnlEntryWithTrans(Loc, False, trans, PostDate, Desc, "AP-AD", "AP Payment Received", strDocNo, "", "C", strVendor, Vendorname, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , Remarks, "")
    '    Dim str1 As String = "update TSPL_Payment_Adjustment_Header set TSPL_Payment_Adjustment_Header.is_Post = 'Y', TSPL_Payment_Adjustment_Header.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
    '    clsDBFuncationality.ExecuteNonQuery(str1, trans)
    'End Function
    Shared Function funLocation(ByVal InvNo As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(connectSql.RunScalar(trans, "Select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code=(Select Bill_To_Location from TSPL_PI_HEAD where PI_No ='" & InvNo & "')"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Shared Function funLocationByAdj(ByVal strInvoiceNo As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Code  FROM TSPL_Vendor_Invoice_Head WHERE TSPL_Vendor_Invoice_Head.Document_No='" & strInvoiceNo & "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            ReverseAndUnpost(strCode, trans, False)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal isCheckForPost As Boolean) As Boolean
        Try
            Dim obj As New clsPaymentAdjustmentEntry
            obj = clsPaymentAdjustmentEntry.GetData(strCode, NavigatorType.Current, trans)
            If obj IsNot Nothing And obj.Adjustment_No <> "" Then
                If Not clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    If isCheckForPost Then
                        Return True
                    Else
                        Throw New Exception("Transaction status should be posted for reverse and unpost [" + obj.Adjustment_No + "]")
                    End If
                End If

                Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-AD' and Source_Doc_No='" + obj.Adjustment_No + "'", trans)
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                    clsDBFuncationality.ExecuteNonQuery("delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'", trans)
                End If
                VoucherNo = obj.Doc_No
                clsPaymentAdjustmentEntry.funUpdateInvoiceReverse(obj.Adjustment_No, obj.Vendor_No, obj.Doc_No, trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_Payment_Adjustment_Header set is_Post = 'N' where Adjustment_No ='" + obj.Adjustment_No + "'", trans)

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Adjustment_No, "TSPL_Payment_Adjustment_Header", "Adjustment_No", "TSPL_Payment_Adjustment_Detail", "Adjustment_No", trans)

            Else
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function funUpdateInvoiceReverse(ByVal PaymentNo As String, ByVal VendorCode As String, ByVal InvoiceNo As String, ByVal trans As SqlTransaction)
        Try
            Dim BalAmt As Decimal
            Dim Qry As String = "Select Sum(TSPL_Payment_Adjustment_Detail.Amount) as Amount from TSPL_Payment_Adjustment_Detail Left Outer Join TSPL_Payment_Adjustment_Header ON  TSPL_Payment_Adjustment_Detail.Adjustment_No=TSPL_Payment_Adjustment_Header.Adjustment_No where TSPL_Payment_Adjustment_Header.Vendor_No = '" + VendorCode + "' and TSPL_Payment_Adjustment_Header.Doc_No = '" + InvoiceNo + "' AND TSPL_Payment_Adjustment_Detail.Adjustment_No = '" + PaymentNo + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                BalAmt = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                ' Dim strDocNo As String = clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD WHERE Against_POInvoice_No='" + InvoiceNo + "'", trans)
                Dim strviBalAmt As Double = clsDBFuncationality.getSingleValue("Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD  where vendor_code = '" + VendorCode + "' AND Document_No='" + InvoiceNo + "'", trans)

                clsDBFuncationality.ExecuteNonQuery("update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = Balance_Amt + " + clsCommon.myCstr(BalAmt) + " where vendor_code = '" + VendorCode + "' AND Document_No='" + InvoiceNo + "'", trans)
            

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function
End Class

Public Class clsPaymentAdjustmentEntryDetail
    Public Adjustment_No As String = ""
    Public Line_No As Integer = 0
    Public Account_No As String = ""
    Public Account_Description As String = ""
    Public Amount As Decimal = 0.0
    Public Remarks As String = ""
    Public Discount_Code As String = ""
    Public Discount_Description As String = ""
    Public FarmerCode As String = String.Empty
    Public FarmerName As String = String.Empty
    Public Farmer_invoice_no As String = ""

    Public Shared Function SaveData(ByVal strAdjNo As String, ByVal arr As List(Of clsPaymentAdjustmentEntryDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_Payment_Adjustment_Detail where Adjustment_No='" + strAdjNo + "'", trans)

            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            Dim ii As Integer = 1
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPaymentAdjustmentEntryDetail In arr
                    If clsCommon.myLen(objtr.Account_No) <= 0 Then
                        Throw New Exception("Account No cant be left blank of Amount [" + clsCommon.myCstr(objtr.Amount) + "]")
                    End If
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Adjustment_No", strAdjNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", ii)
                    ii += 1
                    clsCommon.AddColumnsForChange(coll, "Account_No", objtr.Account_No)
                    clsCommon.AddColumnsForChange(coll, "Account_Description", objtr.Account_Description)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Discount_Code", objtr.Discount_Code)
                    clsCommon.AddColumnsForChange(coll, "Discount_Description", objtr.Discount_Description)
                    clsCommon.AddColumnsForChange(coll, "FarmerCode", objtr.FarmerCode, True)
                    clsCommon.AddColumnsForChange(coll, "FarmerName", objtr.FarmerName, True)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Payment_Adjustment_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("Error While Saveing Payment Adusment Details" + ex.Message)
        End Try
    End Function

End Class