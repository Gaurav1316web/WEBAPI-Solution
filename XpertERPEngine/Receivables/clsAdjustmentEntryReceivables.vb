Imports common
Imports System.Data.SqlClient

Public Class clsAdjustmentEntryReceivables
#Region "Variables"
    Public Adjustment_No As String = ""
    Public Description As String = ""
    Public Adjustment_Date As DateTime
    Public Post_Date As DateTime
    Public Customer_No As String = ""
    Public Customer_Name As String = ""
    Public Doc_No As String = ""
    Public Doc_Amount As Decimal = 0.0
    Public Bal_Amount As Decimal = 0.0
    Public Remarks As String = ""
    Public Adjustment_Amount As Decimal = 0.0
    Public Is_Post As Char = "N"
    Public ARInvoiceNo As String = ""
    Public Arr As List(Of clsAdjustmentEntryReceivablesDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsAdjustmentEntryReceivables, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsAdjustmentEntryReceivables, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Order", obj.Bill_To_Location, obj.PurchaseOrder_Date, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Adjustment_No, "TSPL_Receipt_Adjustment_Header", "Adjustment_No", "TSPL_Receipt_Adjustment_Detail", "Adjustment_No", trans)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Adjustment_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Customer_No", obj.Customer_No)
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Amount", obj.Doc_Amount)
            clsCommon.AddColumnsForChange(coll, "ARInvoiceNo", obj.ARInvoiceNo)
            clsCommon.AddColumnsForChange(coll, "Adjustment_Amount", obj.Adjustment_Amount)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Adjustment_Date), clsDocType.AdjustmentEntry, "", "")
                If (clsCommon.myLen(obj.Adjustment_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Adjustment_No", obj.Adjustment_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Receipt_Adjustment_Header", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Receipt_Adjustment_Header", OMInsertOrUpdate.Update, "TSPL_Receipt_Adjustment_Header.Adjustment_No='" + obj.Adjustment_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsAdjustmentEntryReceivablesDetail.SaveData(obj.Adjustment_No, Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsAdjustmentEntryReceivables
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strAdjNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAdjustmentEntryReceivables
        Dim obj As clsAdjustmentEntryReceivables = Nothing
        ''richa agarwal 13/04/2015
        'Dim qry As String = "SELECT [Adjustment_No], [TSPL_Receipt_Adjustment_Header].[Description], [Adjustment_Date], [Post_Date],[Customer_No], [TSPL_Receipt_Adjustment_Header].[Customer_Name],[Doc_No],[Doc_Amount],ARInvoiceNo," & _
        '" TSPL_Customer_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=Doc_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.Doc_No=TSPL_Customer_Invoice_Head.Against_Sale_No AND AH.Adjustment_No <>'" & strAdjNo & "')) as BalanceAmt," & _
        '" [TSPL_Receipt_Adjustment_Header].[Remarks], [Adjustment_Amount], ISNULL([Is_Post],'N') as Is_Post FROM [TSPL_Receipt_Adjustment_Header]" & _
        '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No= TSPL_Receipt_Adjustment_Header.Doc_No where 2=2"

        '    Dim qry As String = "SELECT [Adjustment_No], [TSPL_Receipt_Adjustment_Header].[Description], [Adjustment_Date], [Post_Date],[Customer_No], [TSPL_Receipt_Adjustment_Header].[Customer_Name],[Doc_No],[Doc_Amount],ARInvoiceNo," & _
        '  " TSPL_Customer_Invoice_Head.Balance_Amt- case when isnull([TSPL_Receipt_Adjustment_Header].Doc_No,'')<>'' then ((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=Doc_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.Doc_No=TSPL_Customer_Invoice_Head.Against_Sale_No AND AH.Adjustment_No <>'" & strAdjNo & "')) else " & _
        '" ((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=ARInvoiceNo )+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.ARInvoiceNo =TSPL_Customer_Invoice_Head.Document_No  AND AH.Adjustment_No <>'" & strAdjNo & "')) end  as BalanceAmt," & _
        '  " [TSPL_Receipt_Adjustment_Header].[Remarks], [Adjustment_Amount], ISNULL([Is_Post],'N') as Is_Post FROM [TSPL_Receipt_Adjustment_Header]" & _
        '  " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No = TSPL_Receipt_Adjustment_Header.ARInvoiceNo where 2=2"

        Dim qry As String = "SELECT [Adjustment_No], [TSPL_Receipt_Adjustment_Header].[Description], [Adjustment_Date], [Post_Date],[Customer_No], [TSPL_Receipt_Adjustment_Header].[Customer_Name],[Doc_No],[Doc_Amount],ARInvoiceNo," & _
        " TSPL_Customer_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=ARInvoiceNo)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.ARInvoiceNo=TSPL_Customer_Invoice_Head.Document_No AND AH.Adjustment_No <>'" & strAdjNo & "')) as BalanceAmt," & _
        " [TSPL_Receipt_Adjustment_Header].[Remarks], [Adjustment_Amount], ISNULL([Is_Post],'N') as Is_Post FROM [TSPL_Receipt_Adjustment_Header]" & _
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No = TSPL_Receipt_Adjustment_Header.ARInvoiceNo where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Receipt_Adjustment_Header.Adjustment_No = (select MIN(Adjustment_No) from TSPL_Receipt_Adjustment_Header)"
            Case NavigatorType.Last
                qry += " and TSPL_Receipt_Adjustment_Header.Adjustment_No = (select Max(Adjustment_No) from TSPL_Receipt_Adjustment_Header)"
            Case NavigatorType.Next
                qry += " and TSPL_Receipt_Adjustment_Header.Adjustment_No = (select Min(Adjustment_No) from TSPL_Receipt_Adjustment_Header where Adjustment_No>'" + strAdjNo + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_Receipt_Adjustment_Header.Adjustment_No = (select Max(Adjustment_No) from TSPL_Receipt_Adjustment_Header where Adjustment_No<'" + strAdjNo + "')"
            Case NavigatorType.Current
                qry += " and TSPL_Receipt_Adjustment_Header.Adjustment_No = '" + strAdjNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAdjustmentEntryReceivables()
            obj.Adjustment_No = clsCommon.myCstr(dt.Rows(0)("Adjustment_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Adjustment_Date = clsCommon.myCstr(dt.Rows(0)("Adjustment_Date"))
            obj.Post_Date = clsCommon.myCstr(dt.Rows(0)("Post_Date"))
            obj.Customer_No = clsCommon.myCstr(dt.Rows(0)("Customer_No"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
            obj.Doc_Amount = clsCommon.myCdbl(dt.Rows(0)("Doc_Amount"))
            obj.ARInvoiceNo = clsCommon.myCstr(dt.Rows(0)("ARInvoiceNo"))
            obj.Bal_Amount = clsCommon.myCdbl(dt.Rows(0)("BalanceAmt"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Adjustment_Amount = clsCommon.myCdbl(dt.Rows(0)("Adjustment_Amount"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))

            qry = "SELECT [Adjustment_No],[Line_No],[Account_No],[Account_Description],[Amount],[Remarks],[Discount_Code],[Discount_Description]" & _
            " FROM [TSPL_Receipt_Adjustment_Detail] WHERE [Adjustment_No]='" + obj.Adjustment_No + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsAdjustmentEntryReceivablesDetail)
                Dim objTr As clsAdjustmentEntryReceivablesDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsAdjustmentEntryReceivablesDetail
                    objTr.Adjustment_No = clsCommon.myCstr(dr("Adjustment_No"))
                    objTr.Line_No = Convert.ToInt32(clsCommon.myCdbl(dr("Line_No")))
                    objTr.Account_No = clsCommon.myCstr((dr("Account_No")))
                    objTr.Account_Description = clsCommon.myCstr(dr("Account_Description"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Discount_Code = clsCommon.myCstr(dr("Discount_Code"))
                    objTr.Discount_Description = clsCommon.myCstr(dr("Discount_Description"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function getTotalUnApprovedAdjAmtOfInvoice(ByVal strsaleInvoiceNo As String, ByVal trans As SqlTransaction) As Decimal
        Try
            clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No='" & strsaleInvoiceNo & "'", trans))
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
            'Dim ArrList As ArrayList = New ArrayList()
            'Dim AdjAcc() As String
            'Dim strRcvblAcc As String = ""
            Dim obj As New clsAdjustmentEntryReceivables
            obj = GetData(strDocNo, NavigatorType.Current, trans)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0 Then
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Document is already posted.")
                End If
                CreateJournalEntry(obj, "", trans)
                'strQ = " SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN" & _
                '           " TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account" & _
                '           " where TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.Customer_No + "'"
                'strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
                ' ''RICHA AGARWAL 23/03/2015
                ''Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
                'Dim strLocation As String = funLocationByAdj(obj.ARInvoiceNo, trans)
                ' ''-------------------
                'strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strLocation, True, trans)
                'Dim CustAcc() As String = {strRcvblAcc, -1 * obj.Adjustment_Amount}
                'ArrList.Add(CustAcc)
                'For Each objtr As clsAdjustmentEntryReceivablesDetail In obj.Arr
                '    ''RICHA AGARWAL 23/03/2015
                '    'objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), trans)
                '    objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.ARInvoiceNo, trans), True, trans)
                '    ''----------------
                '    AdjAcc = New String() {objtr.Account_No, objtr.Amount}
                '    ArrList.Add(AdjAcc)
                'Next
                'transportSql.FunGrnlEntryWithTrans(strLocation, True, trans, obj.Adjustment_Date, obj.Description, "AR-AD", "AR Payment Received", strDocNo, "", "C", obj.Customer_No, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , obj.Remarks, "")
                strQ = "update TSPL_Receipt_Adjustment_Header set TSPL_Receipt_Adjustment_Header.is_Post = 'Y', TSPL_Receipt_Adjustment_Header.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                ''richa agarwal 14/04/2015
                'obj.Doc_No = clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head WHERE Against_Sale_No='" + obj.Doc_No + "'", trans)
                'clsReceiptDettail.funBalanceAmtSave(obj.Doc_No, obj.Adjustment_Amount, trans, "C") '------Changes Balance Amount of Customer Invoice.
                clsReceiptDettail.funBalanceAmtSave(obj.ARInvoiceNo, obj.Adjustment_Amount, trans, "", "C") '------Changes Balance Amount of Customer Invoice.
                ''------------------
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Receipt_Adjustment_Header", "Adjustment_No", "TSPL_Receipt_Adjustment_Detail", "Adjustment_No", trans)
                Return True
            Else
                Return False
            End If
            '------30/11/2012--Added BY--Pankaj Kumar----For Validate Transaction By [Location AND Doc Date]-------------
            'Dim location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location from TSPL_SALE_INVOICE_HEAD  Where Is_Post ='Y' and Cust_Code='" + clsCommon.myCstr(dt.Rows(0)("Customer_No")) + "' AND Sale_Invoice_No='" + clsCommon.myCstr(dt.Rows(0)("Doc_No")) + "'", trans))
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Receivables", "Adjustment Entry", location, clsCommon.myCstr(dt.Rows(0)("Adjustment_Date")), trans)
            '------------------------------------------------------------------------------------------------------------
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateJournalEntry(ByVal obj As clsAdjustmentEntryReceivables, ByVal strVoucherNoifExists As String, ByVal trans As SqlTransaction) As Boolean
        Dim strQ As String
        Dim ArrList As ArrayList = New ArrayList()
        Dim AdjAcc() As String
        Dim strRcvblAcc As String = ""
        Try
            strQ = " SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN" & _
                           " TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account" & _
                           " where TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.Customer_No + "'"
            strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
            ''RICHA AGARWAL 23/03/2015
            'Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
            Dim strLocation As String = funLocationByAdj(obj.ARInvoiceNo, trans)
            ''-------------------
            strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strLocation, True, trans)
            Dim CustAcc() As String = {strRcvblAcc, -1 * obj.Adjustment_Amount}
            ArrList.Add(CustAcc)
            For Each objtr As clsAdjustmentEntryReceivablesDetail In obj.Arr
                ''RICHA AGARWAL 23/03/2015
                'objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), trans)
                objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.ARInvoiceNo, trans), True, trans)
                ''----------------
                AdjAcc = New String() {objtr.Account_No, objtr.Amount}
                ArrList.Add(AdjAcc)
            Next
            transportSql.FunGrnlEntryWithTrans(strLocation, True, strVoucherNoifExists, trans, obj.Adjustment_Date, obj.Description, "AR-AD", "AR Payment Received", obj.Adjustment_No, "", "C", obj.Customer_No, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , obj.Remarks, "")
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
            Dim obj As New clsAdjustmentEntryReceivables
            obj = GetData(strDocNo, NavigatorType.Current, trans)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0 Then
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Document is already posted.")
                End If
                strQ = " SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN" & _
                           " TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account" & _
                           " where TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.Customer_No + "'"
                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
                ''RICHA AGARWAL 23/03/2015
                'Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
                Dim strLocation As String = funLocationByAdj(obj.ARInvoiceNo, trans)
                ''-------------------
                strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strLocation, True, trans)
                Dim CustAcc() As String = {strRcvblAcc, obj.Adjustment_Amount}
                ArrList.Add(CustAcc)
                For Each objtr As clsAdjustmentEntryReceivablesDetail In obj.Arr
                    ''RICHA AGARWAL 23/03/2015
                    'objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), trans)
                    objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.ARInvoiceNo, trans), True, trans)
                    ''----------------
                    AdjAcc = New String() {objtr.Account_No, -1 * objtr.Amount}
                    ArrList.Add(AdjAcc)
                Next
                transportSql.FunGrnlEntryWithTrans(strLocation, True, trans, obj.Adjustment_Date, obj.Description, "AR-AD", "AR Payment Received", strDocNo, "", "C", obj.Customer_No, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , obj.Remarks, "")
                strQ = "update TSPL_Receipt_Adjustment_Header set TSPL_Receipt_Adjustment_Header.is_Post = 'Y', TSPL_Receipt_Adjustment_Header.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                ''richa agarwal 14/04/2015
                'obj.Doc_No = clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head WHERE Against_Sale_No='" + obj.Doc_No + "'", trans)
                'clsReceiptDettail.funBalanceAmtSave(obj.Doc_No, obj.Adjustment_Amount, trans, "C") '------Changes Balance Amount of Customer Invoice.
                clsReceiptDettail.funBalanceAmtSave(obj.ARInvoiceNo, obj.Adjustment_Amount, trans, "", "C") '------Changes Balance Amount of Customer Invoice.
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Receipt_Adjustment_Header", "Adjustment_No", "TSPL_Receipt_Adjustment_Detail", "Adjustment_No", trans)
                ''------------------
                Return True
            Else
                Return False
            End If
            '------30/11/2012--Added BY--Pankaj Kumar----For Validate Transaction By [Location AND Doc Date]-------------
            'Dim location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location from TSPL_SALE_INVOICE_HEAD  Where Is_Post ='Y' and Cust_Code='" + clsCommon.myCstr(dt.Rows(0)("Customer_No")) + "' AND Sale_Invoice_No='" + clsCommon.myCstr(dt.Rows(0)("Doc_No")) + "'", trans))
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Receivables", "Adjustment Entry", location, clsCommon.myCstr(dt.Rows(0)("Adjustment_Date")), trans)
            '------------------------------------------------------------------------------------------------------------
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function FunPostForUtility(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strQ As String = ""
        Dim ArrList As ArrayList = New ArrayList()
        Dim AdjAcc() As String = Nothing
        Dim CustAcc() As String = Nothing
        Dim strCust As String = ""
        Dim PostDate As DateTime
        Dim Desc As String = ""
        Dim Custname As String = ""
        Dim Remarks As String = ""
        Dim TAdjAmt As Decimal = Nothing
        Dim AdjAccnt As String = ""
        Dim AdjAmt As Decimal = 0
        strQ = " SELECT TSPL_Receipt_Adjustment_Header.Adjustment_No, TSPL_Receipt_Adjustment_Header.Description, " & _
                   "   TSPL_Receipt_Adjustment_Header.Adjustment_Date, TSPL_Receipt_Adjustment_Header.Post_Date, TSPL_Receipt_Adjustment_Header.Customer_No, " & _
                   "   TSPL_Receipt_Adjustment_Header.Customer_Name, TSPL_Receipt_Adjustment_Header.Doc_No,TSPL_Receipt_Adjustment_Header.Remarks,  " & _
                   "   ISNULL(TSPL_Receipt_Adjustment_Header.Adjustment_Amount, 0) AS Adjustment_Amount, TSPL_Receipt_Adjustment_Detail.Account_No,  " & _
                   "   TSPL_Receipt_Adjustment_Detail.Account_Description, ISNULL(TSPL_Receipt_Adjustment_Detail.Amount, 0) AS Amount,  " & _
                   "   TSPL_Receipt_Adjustment_Detail.Amount as [Amt] FROM TSPL_Receipt_Adjustment_Header INNER JOIN" & _
                   "   TSPL_Receipt_Adjustment_Detail ON TSPL_Receipt_Adjustment_Header.Adjustment_No = TSPL_Receipt_Adjustment_Detail.Adjustment_No WHERE  TSPL_Receipt_Adjustment_Header.Adjustment_No = '" + strDocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Adjustment No not found to post")
        End If
        Dim strSaleInvNo As String = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
        Dim Loc As String = funLocationByAdj(strDocNo, trans)
        For Each dr As DataRow In dt.Rows
            strCust = dr("Customer_No").ToString()
            PostDate = Convert.ToDateTime(dr("Post_Date").ToString()).Date
            Desc = dr("Description").ToString()
            Custname = dr("Customer_Name").ToString()
            Remarks = dr("Remarks").ToString()
            TAdjAmt = CDec(dr("Adjustment_Amount").ToString())
            AdjAccnt = dr("Account_No").ToString()
            AdjAmt = CDec(dr("Amt").ToString())
            AdjAcc = New String() {AdjAccnt, Convert.ToDecimal(AdjAmt)}
            ArrList.Add(AdjAcc)
        Next
        Dim strQuery As String = " SELECT     TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN" & _
                   " TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account" & _
                   " where TSPL_CUSTOMER_MASTER.Cust_Code ='" + strCust + "'"
        Dim strRcvblAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQuery, trans))
        Dim strRcvblLocAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, Loc, trans)
        CustAcc = New String() {strRcvblLocAcc, -1 * TAdjAmt}
        ArrList.Add(CustAcc)
        transportSql.FunGrnlEntryWithTrans(Loc, False, trans, PostDate, Desc, "AR-AD", "AR Payment Received", strDocNo, "", "C", strCust, Custname, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , Remarks, "")
        Dim str1 As String = "update TSPL_Receipt_Adjustment_Header set TSPL_Receipt_Adjustment_Header.is_Post = 'Y', TSPL_Receipt_Adjustment_Header.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
        clsDBFuncationality.ExecuteNonQuery(str1, trans)
        Return True
    End Function
    Shared Function funLocation(ByVal InvNo As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(connectSql.RunScalar(trans, "Select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code=(Select Bill_To_Location from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Code='" & InvNo & "')"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Shared Function funLocationByAdj(ByVal strInvoiceNo As String, ByVal trans As SqlTransaction) As String
        Try
            ''richa agarwal 20/03/2015 against ticket no BM00000005810 show data from bulk invoice also
            'Return clsDBFuncationality.getSingleValue(" Select Final.Location,Final.InvoiceNo from(select TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location As Location,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo from TSPL_SD_SALE_INVOICE_HEAD Union All Select TSPL_INVOICE_MASTER_BULKSALE.Location_Code As Location,TSPL_INVOICE_MASTER_BULKSALE.Document_No  as InvoiceNo from TSPL_INVOICE_MASTER_BULKSALE ) Final where Final.InvoiceNo ='" + strInvoiceNo + "'", trans)
            Return clsDBFuncationality.getSingleValue("sELECT TSPL_Customer_Invoice_Head.Loc_Code FROM TSPL_Customer_Invoice_Head WHERE TSPL_Customer_Invoice_Head.Document_No ='" + strInvoiceNo + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
        ''Dim trans As SqlTransaction = Nothing
        Try
            '' trans = clsDBFuncationality.GetTransactin()
            Dim obj As New clsAdjustmentEntryReceivables
            obj = clsAdjustmentEntryReceivables.GetData(strCode, NavigatorType.Current, trans)
            If obj IsNot Nothing And obj.Adjustment_No <> "" Then
                If Not clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Transaction status should be posted for reverse and unpost")
                End If

                Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AR-AD' and Source_Doc_No='" + obj.Adjustment_No + "'", trans)
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                    clsDBFuncationality.ExecuteNonQuery("delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'", trans)
                End If

                VoucherNo = clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head WHERE Against_Sale_No='" + obj.Doc_No + "'", trans)
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsReceiptDettail.funBalanceAmtSave(VoucherNo, obj.Adjustment_Amount * -1, trans, "", "C") '------Changes Balance Amount of Customer Invoice.
                End If


                clsDBFuncationality.ExecuteNonQuery("update TSPL_Receipt_Adjustment_Header set is_Post = 'N' where Adjustment_No ='" + obj.Adjustment_No + "'", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Adjustment_No, "TSPL_Receipt_Adjustment_Header", "Adjustment_No", "TSPL_Receipt_Adjustment_Detail", "Adjustment_No", trans)
            Else
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
            '' trans.Commit()
        Catch ex As Exception
            ''trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsAdjustmentEntryReceivablesDetail
    Public Adjustment_No As String = ""
    Public Line_No As Integer = 0
    Public Account_No As String = ""
    Public Account_Description As String = ""
    Public Amount As Decimal = 0.0
    Public Remarks As String = ""
    Public Discount_Code As String = ""
    Public Discount_Description As String = ""

    Public Shared Function SaveData(ByVal strAdjNo As String, ByVal arr As List(Of clsAdjustmentEntryReceivablesDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_Receipt_Adjustment_Detail where Adjustment_No='" + strAdjNo + "'", trans)

            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsAdjustmentEntryReceivablesDetail In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Adjustment_No", strAdjNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No + 1)
                    clsCommon.AddColumnsForChange(coll, "Account_No", objtr.Account_No)
                    clsCommon.AddColumnsForChange(coll, "Account_Description", objtr.Account_Description)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Discount_Code", objtr.Discount_Code)
                    clsCommon.AddColumnsForChange(coll, "Discount_Description", objtr.Discount_Description)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Receipt_Adjustment_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class