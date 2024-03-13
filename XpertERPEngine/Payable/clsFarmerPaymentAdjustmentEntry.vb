Imports common
Imports System.Data.SqlClient

Public Class clsFarmerPaymentAdjustmentEntry
#Region "Variables"
    Public Adjustment_No As String = ""
    Public Description As String = ""
    Public Adjustment_Date As DateTime
    Public Post_Date As DateTime
    Public Farmer_Code As String = ""
    Public Mcc_Code As String = ""
    Public Mcc_Name As String = ""
    Public Farmer_Name As String = ""
    Public Doc_No As String = ""
    Public Doc_Amount As Decimal = 0.0
    Public Bal_Amount As Decimal = 0.0
    Public Remarks As String = ""
    Public Adjustment_Amount As Decimal = 0.0
    Public Is_Post As Char = "N"
    Public Adjustment_Type As String = ""
    
    Public Arr As List(Of clsFarmerPaymentAdjustmentEntryDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsFarmerPaymentAdjustmentEntry, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsFarmerPaymentAdjustmentEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmFarmerPaymentAdjustment, obj.Mcc_Code, obj.Adjustment_Date, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Adjustment_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Farmer_Code", obj.Farmer_Code)
            If clsCommon.myLen(obj.Mcc_Code) <= 0 Then
                obj.Mcc_Code = GetMPLocation(obj.Farmer_Code, trans)
                clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.Mcc_Code)
            Else
                clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.Mcc_Code)
            End If

            clsCommon.AddColumnsForChange(coll, "Farmer_Name", obj.Farmer_Name)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No, True)
            clsCommon.AddColumnsForChange(coll, "Doc_Amount", obj.Doc_Amount)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Adjustment_Type", obj.Adjustment_Type)

            clsCommon.AddColumnsForChange(coll, "Adjustment_Amount", obj.Adjustment_Amount)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Adjustment_Date), clsDocType.PaymentAdjustmentEntry, "", "")
                If (clsCommon.myLen(obj.Adjustment_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Adjustment_No", obj.Adjustment_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_Pay_Adj_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_Pay_Adj_Head", OMInsertOrUpdate.Update, "TSPL_MP_Pay_Adj_Head.Adjustment_No='" + obj.Adjustment_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsFarmerPaymentAdjustmentEntryDetail.SaveData(obj.Adjustment_No, Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetMPLocation(ByVal MP_Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select TSPL_VLC_MASTER_HEAD.MCC  from TSPL_MP_MASTER  " & _
                            " LEFT JOIN TSPL_VLC_MASTER_HEAD ON TSPL_MP_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code " & _
                            " WHERE TSPL_MP_MASTER.MP_Code ='" & MP_Code & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsFarmerPaymentAdjustmentEntry
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strAdjNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsFarmerPaymentAdjustmentEntry
        Dim obj As clsFarmerPaymentAdjustmentEntry = Nothing
        'Dim qry As String = "SELECT [Adjustment_No], [TSPL_MP_Pay_Adj_Head].[Description], [Adjustment_Date], [Post_Date],[Farmer_Code], [TSPL_MP_Pay_Adj_Head].[Farmer_Name],[Doc_No],[Doc_Amount]," & _
        '"  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=Doc_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_MP_Pay_Adj_Head AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No AND AH.Adjustment_No <>'" & strAdjNo & "') + ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No AND Post NOT IN ('1','P')),0)) as BalanceAmt," & _
        '" [TSPL_MP_Pay_Adj_Head].[Remarks], [Adjustment_Amount], ISNULL([Is_Post],'N') as Is_Post FROM [TSPL_MP_Pay_Adj_Head]" & _
        '" LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No= TSPL_MP_Pay_Adj_Head.Doc_No where 2=2"
        Dim qry As String = "SELECT [Adjustment_No], [TSPL_MP_Pay_Adj_Head].[Description], [Adjustment_Date], [Post_Date],[Farmer_Code], [TSPL_MP_Pay_Adj_Head].[Farmer_Name],[Doc_No],[Doc_Amount]," & _
       "  0 as BalanceAmt," & _
       " [TSPL_MP_Pay_Adj_Head].[Remarks], [Adjustment_Amount], ISNULL([Is_Post],'N') as Is_Post,Adjustment_Type,TSPL_MP_Pay_Adj_Head.mcc_Code as [MCC Code],tspl_location_master.Location_Desc as [MCC Name] FROM [TSPL_MP_Pay_Adj_Head]" & _
       " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No= TSPL_MP_Pay_Adj_Head.Doc_No left join tspl_location_master on tspl_location_master.Location_Code=TSPL_MP_Pay_Adj_Head.MCC_Code where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MP_Pay_Adj_Head.Adjustment_No = (select MIN(Adjustment_No) from TSPL_MP_Pay_Adj_Head)"
            Case NavigatorType.Last
                qry += " and TSPL_MP_Pay_Adj_Head.Adjustment_No = (select Max(Adjustment_No) from TSPL_MP_Pay_Adj_Head)"
            Case NavigatorType.Next
                qry += " and TSPL_MP_Pay_Adj_Head.Adjustment_No = (select Min(Adjustment_No) from TSPL_MP_Pay_Adj_Head where Adjustment_No>'" + strAdjNo + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_MP_Pay_Adj_Head.Adjustment_No = (select Max(Adjustment_No) from TSPL_MP_Pay_Adj_Head where Adjustment_No<'" + strAdjNo + "')"
            Case NavigatorType.Current
                qry += " and TSPL_MP_Pay_Adj_Head.Adjustment_No = '" + strAdjNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsFarmerPaymentAdjustmentEntry()
            obj.Adjustment_No = clsCommon.myCstr(dt.Rows(0)("Adjustment_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Adjustment_Date = clsCommon.myCstr(dt.Rows(0)("Adjustment_Date"))
            obj.Post_Date = clsCommon.myCstr(dt.Rows(0)("Post_Date"))
            obj.Farmer_Code = clsCommon.myCstr(dt.Rows(0)("Farmer_Code"))
            obj.Farmer_Name = clsCommon.myCstr(dt.Rows(0)("Farmer_Name"))
            obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
            obj.Doc_Amount = clsCommon.myCdbl(dt.Rows(0)("Doc_Amount"))
            obj.Bal_Amount = clsCommon.myCdbl(dt.Rows(0)("BalanceAmt"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Adjustment_Type = clsCommon.myCstr(dt.Rows(0)("Adjustment_Type"))
            obj.Adjustment_Amount = clsCommon.myCdbl(dt.Rows(0)("Adjustment_Amount"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
            obj.Mcc_Code = clsCommon.myCstr(dt.Rows(0)("MCC Code"))
            obj.Mcc_Name = clsCommon.myCstr(dt.Rows(0)("MCC Name"))

            qry = "SELECT [Adjustment_No],[Line_No],[Account_No],[Account_Description],[Amount],[Remarks],[Discount_Code],[Discount_Description] " & _
            " FROM [TSPL_MP_PAY_ADJ_DETAIL] WHERE [Adjustment_No]='" + obj.Adjustment_No + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsFarmerPaymentAdjustmentEntryDetail)
                Dim objTr As clsFarmerPaymentAdjustmentEntryDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsFarmerPaymentAdjustmentEntryDetail
                    objTr.Adjustment_No = clsCommon.myCstr(dr("Adjustment_No"))
                    objTr.Line_No = Convert.ToInt32(clsCommon.myCdbl(dr("Line_No")))
                    objTr.Account_No = clsCommon.myCstr((dr("Account_No")))
                    objTr.Account_Description = clsCommon.myCstr(dr("Account_Description"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Discount_Code = clsCommon.myCstr(dr("Discount_Code"))
                    objTr.Discount_Description = clsCommon.myCstr(dr("Discount_Description"))
                    'objTr.FarmerCode = clsCommon.myCstr(dr("FarmerCode"))
                    'objTr.FarmerName = clsCommon.myCstr(dr("FarmerName"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Adjustment_Date,MCC_Code from TSPL_MP_Pay_Adj_Head where Adjustment_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmFarmerPaymentAdjustment, clsCommon.myCstr(dt.Rows(0)("MCC_Code")), clsCommon.myCDate(dt.Rows(0)("Adjustment_Date")), trans)

            End If
            Dim qry As String = ""
            qry = "delete from TSPL_MP_PAY_ADJ_DETAIL where Adjustment_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MP_Pay_Adj_Head where Adjustment_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function getTotalUnApprovedAdjAmtOfInvoice(ByVal strsaleInvoiceNo As String, ByVal trans As SqlTransaction) As Decimal
        Try
            clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_MP_Pay_Adj_Head WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No='" & strsaleInvoiceNo & "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return 0
    End Function

    Public Shared Function FunPost(ByVal FormId As String, ByVal strDocNo As String) As Boolean
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
            Dim obj As New clsFarmerPaymentAdjustmentEntry
            obj = GetData(strDocNo, NavigatorType.Current, trans)
            ' clsLockMPPaymentCycle.LockMPTransaction(obj.Mcc_Code, obj.Adjustment_Date, trans)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0 Then
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Document is already posted.")
                End If

                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmFarmerPaymentAdjustment, obj.Mcc_Code, obj.Adjustment_Date, trans)

                '' done by Panch Raj against Internal Ticket -MPD: Referred by Amit Sir
                'CreateJournalEntry(obj, "", trans)
                'strQ = " SELECT TSPL_VENDOR_ACCOUNT_SET.Payable_Account FROM  TSPL_VENDOR_ACCOUNT_SET  INNER JOIN " & _
                '   " TSPL_VENDOR_MASTER ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  = TSPL_VENDOR_MASTER.Vendor_Account " & _
                '   " where TSPL_VENDOR_MASTER.Vendor_Code ='" + obj.Farmer_Code + "'"

                'strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
                'Dim strPINo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Against_POInvoice_No from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & obj.Doc_No & "'  ", trans))
                ''Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
                'Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
                '' ap invoice saves the location segment, not the location
                'strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strLocation, True, trans)
                'Dim CustAcc() As String = {strRcvblAcc, obj.Adjustment_Amount}
                'ArrList.Add(CustAcc)
                'For Each objtr As clsFarmerPaymentAdjustmentEntryDetail In obj.Arr
                '    'objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), trans)
                '    objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), True, trans)
                '    AdjAcc = New String() {objtr.Account_No, -1 * objtr.Amount}
                '    ArrList.Add(AdjAcc)
                'Next
                'clsJournalMaster.FunGrnlEntryWithTrans(strLocation, True, trans, obj.Adjustment_Date, obj.Description, "AP-AD", "AP Payment Received", strDocNo, "", "C", obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , obj.Remarks, "")
                strQ = "update TSPL_MP_Pay_Adj_Head set TSPL_MP_Pay_Adj_Head.is_Post = 'Y', TSPL_MP_Pay_Adj_Head.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                'obj.Doc_No = clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD WHERE Against_POInvoice_No='" + obj.Doc_No + "'", trans)
                ' clsReceiptDettail.funBalanceAmtSave(obj.Doc_No, obj.Adjustment_Amount, trans, "C") '------Changes Balance Amount of Customer Invoice.
                'clsFarmerPaymentAdjustmentEntry.funUpdateInvoice(obj.Adjustment_No, obj.Farmer_Code, obj.Doc_No, trans)
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
    Public Shared Function CreateJournalEntry(ByVal obj As clsFarmerPaymentAdjustmentEntry, ByVal strVoucherNoifExists As String, ByVal trans As SqlTransaction) As Boolean
        Dim strQ As String
        Dim strRcvblAcc As String
        Dim ArrList As ArrayList = New ArrayList()
        Dim AdjAcc() As String
        Try
            strQ = " SELECT TSPL_VENDOR_ACCOUNT_SET.Payable_Account FROM  TSPL_VENDOR_ACCOUNT_SET  INNER JOIN " & _
                        " TSPL_VENDOR_MASTER ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  = TSPL_VENDOR_MASTER.Vendor_Account " & _
                        " where TSPL_VENDOR_MASTER.Vendor_Code ='" + obj.Farmer_Code + "'"

            strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
            Dim strPINo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Against_POInvoice_No from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & obj.Doc_No & "'  ", trans))
            'Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
            Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
            ' ap invoice saves the location segment, not the location
            strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strLocation, True, trans)
            Dim CustAcc() As String = {strRcvblAcc, obj.Adjustment_Amount}
            ArrList.Add(CustAcc)
            For Each objtr As clsFarmerPaymentAdjustmentEntryDetail In obj.Arr
                'objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), trans)
                objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), True, trans)
                AdjAcc = New String() {objtr.Account_No, -1 * objtr.Amount}
                ArrList.Add(AdjAcc)
            Next
            strQ = " select Document_Type from TSPL_Vendor_Invoice_Head where Document_No='" + obj.Doc_No + "'"
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans)), "D") = CompairStringResult.Equal Then
                Dim ArryLstNew As ArrayList = New ArrayList()
                For Each Str() As String In ArrList
                    Dim strNew() As String = {Str(0), -1 * Str(1)}
                    ArryLstNew.Add(strNew)
                Next
                clsJournalMaster.FunGrnlEntryWithTrans(strLocation, True, strVoucherNoifExists, trans, obj.Adjustment_Date, obj.Description, "AP-AD", "AP Payment Received", obj.Adjustment_No, "", "C", obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew, , obj.Remarks, "")
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(strLocation, True, strVoucherNoifExists, trans, obj.Adjustment_Date, obj.Description, "AP-AD", "AP Payment Received", obj.Adjustment_No, "", "C", obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , obj.Remarks, "")
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
            Dim obj As New clsFarmerPaymentAdjustmentEntry
            obj = GetData(strDocNo, NavigatorType.Current, trans)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0 Then
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Document is already posted.")
                End If
                strQ = " SELECT TSPL_VENDOR_ACCOUNT_SET.Payable_Account FROM  TSPL_VENDOR_ACCOUNT_SET  INNER JOIN " & _
                   " TSPL_VENDOR_MASTER ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  = TSPL_VENDOR_MASTER.Vendor_Account " & _
                   " where TSPL_VENDOR_MASTER.Vendor_Code ='" + obj.Farmer_Code + "'"

                strRcvblAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQ, trans))
                Dim strPINo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Against_POInvoice_No from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & obj.Doc_No & "'  ", trans))
                'Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
                Dim strLocation As String = funLocationByAdj(obj.Doc_No, trans)
                ' ap invoice saves the location segment, not the location
                strRcvblAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strRcvblAcc, strLocation, True, trans)
                Dim CustAcc() As String = {strRcvblAcc, -1 * obj.Adjustment_Amount}
                ArrList.Add(CustAcc)
                For Each objtr As clsFarmerPaymentAdjustmentEntryDetail In obj.Arr
                    'objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), trans)
                    objtr.Account_No = clsERPFuncationality.ChangeGLAccountLocationSegment(objtr.Account_No, funLocationByAdj(obj.Doc_No, trans), True, trans)
                    AdjAcc = New String() {objtr.Account_No, objtr.Amount}
                    ArrList.Add(AdjAcc)
                Next
                clsJournalMaster.FunGrnlEntryWithTrans(strLocation, True, trans, obj.Adjustment_Date, obj.Description, "AP-AD", "AP Payment Received", strDocNo, "", "C", obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArrList, , obj.Remarks, "")
                strQ = "update TSPL_MP_Pay_Adj_Head set TSPL_MP_Pay_Adj_Head.is_Post = 'Y', TSPL_MP_Pay_Adj_Head.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                'obj.Doc_No = clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD WHERE Against_POInvoice_No='" + obj.Doc_No + "'", trans)
                ' clsReceiptDettail.funBalanceAmtSave(obj.Doc_No, obj.Adjustment_Amount, trans, "C") '------Changes Balance Amount of Customer Invoice.
                clsFarmerPaymentAdjustmentEntry.funUpdateInvoice(obj.Adjustment_No, obj.Farmer_Code, obj.Doc_No, trans)
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

    Public Shared Function funUpdateInvoice(ByVal PaymentNo As String, ByVal VendorCode As String, ByVal InvoiceNo As String, ByVal trans As SqlTransaction)
        Try
            Dim BalAmt As Decimal
            Dim Qry As String = "Select Sum(TSPL_MP_PAY_ADJ_DETAIL.Amount) as Amount from TSPL_MP_PAY_ADJ_DETAIL Left Outer Join TSPL_MP_Pay_Adj_Head ON  TSPL_MP_PAY_ADJ_DETAIL.Adjustment_No=TSPL_MP_Pay_Adj_Head.Adjustment_No where TSPL_MP_Pay_Adj_Head.Farmer_Code = '" + VendorCode + "' and TSPL_MP_Pay_Adj_Head.Doc_No = '" + InvoiceNo + "' AND TSPL_MP_PAY_ADJ_DETAIL.Adjustment_No = '" + PaymentNo + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                BalAmt = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                'Dim strDocNo As String = clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD WHERE Against_POInvoice_No='" + InvoiceNo + "'", trans)
                Dim strviBalAmt As Double = clsDBFuncationality.getSingleValue("Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD  where vendor_code = '" + VendorCode + "' AND Document_No='" + InvoiceNo + "'", trans)
                If strviBalAmt >= BalAmt Then
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = Balance_Amt - " + clsCommon.myCstr(BalAmt) + " where vendor_code = '" + VendorCode + "' AND Document_No='" + InvoiceNo + "'", trans)
                Else
                    Throw New Exception("Adjustment amount cannot be greater than balance amount")
                End If

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
    '    strQ = " SELECT TSPL_MP_Pay_Adj_Head.Adjustment_No, TSPL_MP_Pay_Adj_Head.Description, " & _
    '               "   TSPL_MP_Pay_Adj_Head.Adjustment_Date, TSPL_MP_Pay_Adj_Head.Post_Date, TSPL_MP_Pay_Adj_Head.Farmer_Code, " & _
    '               "   TSPL_MP_Pay_Adj_Head.Farmer_Name, TSPL_MP_Pay_Adj_Head.Doc_No,TSPL_MP_Pay_Adj_Head.Remarks,  " & _
    '               "   ISNULL(TSPL_MP_Pay_Adj_Head.Adjustment_Amount, 0) AS Adjustment_Amount, TSPL_MP_PAY_ADJ_DETAIL.Account_No,  " & _
    '               "   TSPL_MP_PAY_ADJ_DETAIL.Account_Description, ISNULL(TSPL_MP_PAY_ADJ_DETAIL.Amount, 0) AS Amount,  " & _
    '               "   TSPL_MP_PAY_ADJ_DETAIL.Amount as [Amt] FROM TSPL_MP_Pay_Adj_Head INNER JOIN" & _
    '               "   TSPL_MP_PAY_ADJ_DETAIL ON TSPL_MP_Pay_Adj_Head.Adjustment_No = TSPL_MP_PAY_ADJ_DETAIL.Adjustment_No WHERE  TSPL_MP_Pay_Adj_Head.Adjustment_No = '" + strDocNo + "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
    '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '        Throw New Exception("Adjustment No not found to post")
    '    End If
    '    Dim strSaleInvNo As String = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
    '    Dim Loc As String = funLocationByAdj(strDocNo, trans)
    '    For Each dr As DataRow In dt.Rows
    '        strVendor = dr("Farmer_Code").ToString()
    '        PostDate = Convert.ToDateTime(dr("Post_Date").ToString()).Date
    '        Desc = dr("Description").ToString()
    '        Vendorname = dr("Farmer_Name").ToString()
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
    '    Dim str1 As String = "update TSPL_MP_Pay_Adj_Head set TSPL_MP_Pay_Adj_Head.is_Post = 'Y', TSPL_MP_Pay_Adj_Head.Post_Date= '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Adjustment_No ='" + clsCommon.myCstr(strDocNo) + "'"
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
            Dim obj As New clsFarmerPaymentAdjustmentEntry
            obj = clsFarmerPaymentAdjustmentEntry.GetData(strCode, NavigatorType.Current, trans)
            If obj IsNot Nothing And obj.Adjustment_No <> "" Then
                If Not clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Transaction status should be posted for reverse and unpost")
                End If
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmFarmerPaymentAdjustment, obj.Mcc_Code, obj.Adjustment_Date, trans)

                Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-AD' and Source_Doc_No='" + obj.Adjustment_No + "'", trans)
                    If clsCommon.myLen(VoucherNo) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'", trans)
                End If

                'VoucherNo = clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD WHERE Against_POInvoice_No='" + obj.Doc_No + "'", trans)
                VoucherNo = obj.Doc_No
                ' clsReceiptDettail.funBalanceAmtSave(VoucherNo, obj.Adjustment_Amount * -1, trans, "C") '------Changes Balance Amount of Customer Invoice.
                clsFarmerPaymentAdjustmentEntry.funUpdateInvoiceReverse(obj.Adjustment_No, obj.Farmer_Code, obj.Doc_No, trans)
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MP_Pay_Adj_Head set is_Post = 'N' where Adjustment_No ='" + obj.Adjustment_No + "'", trans)
            Else
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function funUpdateInvoiceReverse(ByVal PaymentNo As String, ByVal VendorCode As String, ByVal InvoiceNo As String, ByVal trans As SqlTransaction)
        Try
            Dim BalAmt As Decimal
            Dim Qry As String = "Select Sum(TSPL_MP_PAY_ADJ_DETAIL.Amount) as Amount from TSPL_MP_PAY_ADJ_DETAIL Left Outer Join TSPL_MP_Pay_Adj_Head ON  TSPL_MP_PAY_ADJ_DETAIL.Adjustment_No=TSPL_MP_Pay_Adj_Head.Adjustment_No where TSPL_MP_Pay_Adj_Head.Farmer_Code = '" + VendorCode + "' and TSPL_MP_Pay_Adj_Head.Doc_No = '" + InvoiceNo + "' AND TSPL_MP_PAY_ADJ_DETAIL.Adjustment_No = '" + PaymentNo + "' "
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

Public Class clsFarmerPaymentAdjustmentEntryDetail
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

    Public Shared Function SaveData(ByVal strAdjNo As String, ByVal arr As List(Of clsFarmerPaymentAdjustmentEntryDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MP_PAY_ADJ_DETAIL where Adjustment_No='" + strAdjNo + "'", trans)

            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsFarmerPaymentAdjustmentEntryDetail In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Adjustment_No", strAdjNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No + 1)
                    clsCommon.AddColumnsForChange(coll, "Account_No", objtr.Account_No)
                    clsCommon.AddColumnsForChange(coll, "Account_Description", objtr.Account_Description)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Discount_Code", objtr.Discount_Code)
                    clsCommon.AddColumnsForChange(coll, "Discount_Description", objtr.Discount_Description)
                    'clsCommon.AddColumnsForChange(coll, "FarmerCode", objtr.FarmerCode, True)
                    'clsCommon.AddColumnsForChange(coll, "FarmerName", objtr.FarmerName, True)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MP_PAY_ADJ_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class