Imports common
Imports System.Data.SqlClient

''BM00000000663
'' Ticket No : BM00000007765 by panch raj
Public Class clsVCGLHead

#Region "Variables"
    Public Document_No As String = Nothing
    Public Description As String = Nothing
    Public Document_Date As DateTime
    Public Document_Type As String = Nothing
    Public Location_Segment As String = Nothing
    Public DateAndTime As DateTime?
    Public TapalNo As String = String.Empty
    Public VC_Code As String = Nothing
    Public VC_Name As String = Nothing
    Public Remarks As String = Nothing
    Public Posting_Date As DateTime? = Nothing
    Public Tot_Dr_Amount As Double = 0
    Public Tot_Cr_Amount As Double = 0
    Public Amount_Type As String = Nothing
    Public Amount As Double = 0
    Public Is_Empty As Double = 0
    Public GL_Account_Code As String = Nothing
    Public On_Hold As Boolean = Nothing
    Public FarmerInVendor As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Arr As List(Of clsVCGLDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsVCGLHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsVCGLHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        If obj.Arr.Count <= 0 Then
            Throw New Exception("Please fill at least one Account")
        End If
        Dim isSaved As Boolean = True
        If Not isNewEntry Then
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_VCGL_Head", "Document_No", "TSPL_VCGL_Detail", "Document_No", trans)
        End If
        Dim qry As String = "delete from TSPL_VCGL_Detail where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim strDocNo As String = ""

        If (isNewEntry) Then
            obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.VCGLEntry, "", obj.Location_Segment, True)
        End If

        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        If clsCommon.CompairString(obj.Document_Type, "V") = CompairStringResult.Equal Then
            qry = "select  TSPL_VENDOR_ACCOUNT_SET.Payable_Account from TSPL_VENDOR_MASTER "
            qry += " left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_ACCOUNT_SET.Payable_Account where TSPL_VENDOR_MASTER.Vendor_Code='" + obj.VC_Code + "'"
            obj.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        ElseIf clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal Then
            Dim strGLAccount As String = "TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct"
            If obj.Is_Empty = 1 Then
                strGLAccount = "TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit"
            End If
            qry = "select  " + strGLAccount + " from TSPL_CUSTOMER_MASTER"
            qry += " left outer join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account "
            qry += " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct where Cust_Code='" + obj.VC_Code + "'"
            obj.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        ElseIf clsCommon.CompairString(obj.Document_Type, "F") = CompairStringResult.Equal Then
            qry = "select TSPL_GL_ACCOUNTS.Account_Code AS Payable_Account  from TSPL_VENDOR_ACCOUNT_SET left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(TSPL_VENDOR_ACCOUNT_SET.Payable_Account,0,LEN(TSPL_VENDOR_ACCOUNT_SET.Payable_Account)-3) where TSPL_VENDOR_ACCOUNT_SET.IsFarmer=1 and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & clsCommon.myCstr(obj.Location_Segment) & "'"
            obj.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        End If

        If clsCommon.myLen(obj.GL_Account_Code) <= 0 Then
            Throw New Exception("Vendor/Customer's Control account not found")
        End If
        obj.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.GL_Account_Code, obj.Location_Segment, True, trans)

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
        clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
        clsCommon.AddColumnsForChange(coll, "Location_Segment", obj.Location_Segment)
        clsCommon.AddColumnsForChange(coll, "VC_Code", obj.VC_Code)
        clsCommon.AddColumnsForChange(coll, "VC_Name", obj.VC_Name)
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Tot_Dr_Amount", obj.Tot_Dr_Amount)
        clsCommon.AddColumnsForChange(coll, "Tot_Cr_Amount", obj.Tot_Cr_Amount)
        clsCommon.AddColumnsForChange(coll, "Amount_Type", obj.Amount_Type)
        clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
        clsCommon.AddColumnsForChange(coll, "Is_Empty", obj.Is_Empty)
        clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
        clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "TapalNo", obj.TapalNo, True)
        clsCommon.AddColumnsForChange(coll, "FarmerInVendor", obj.FarmerInVendor, True)
        If clsCommon.myLen(obj.DateAndTime) > 0 Then
            clsCommon.AddColumnsForChange(coll, "DateAndTime", clsCommon.GetPrintDate(obj.DateAndTime, "dd/MMM/yyyy hh:mm tt"))
        Else
            clsCommon.AddColumnsForChange(coll, "DateAndTime", Nothing, True)
        End If
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VCGL_Head", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VCGL_Head", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
        End If
        isSaved = isSaved AndAlso clsVCGLDetail.SaveData(obj.Document_No, obj.Location_Segment, Arr, trans)
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String) As clsVCGLHead
        Return GetData(strDocumentNo, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal trans As SqlTransaction) As clsVCGLHead
        Dim obj As clsVCGLHead = Nothing
        Dim qry As String = "Select * from TSPL_VCGL_Head where Document_No='" + strDocumentNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVCGLHead()

            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.Location_Segment = clsCommon.myCstr(dt.Rows(0)("Location_Segment"))
            obj.VC_Code = clsCommon.myCstr(dt.Rows(0)("VC_Code"))
            obj.VC_Name = clsCommon.myCstr(dt.Rows(0)("VC_Name"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            If IsDBNull(dt.Rows(0)("DateAndTime")) = True Then
                obj.DateAndTime = Nothing
            Else
                obj.DateAndTime = clsCommon.myCstr(dt.Rows(0)("DateAndTime"))
            End If
            obj.TapalNo = clsCommon.myCstr(dt.Rows(0)("TapalNo"))
            obj.Tot_Dr_Amount = clsCommon.myCdbl(dt.Rows(0)("Tot_Dr_Amount"))
            obj.Tot_Cr_Amount = clsCommon.myCdbl(dt.Rows(0)("Tot_Cr_Amount"))
            obj.Amount_Type = clsCommon.myCstr(dt.Rows(0)("Amount_Type"))
            obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
            obj.On_Hold = IIf(clsCommon.myCstr(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 0, ERPTransactionStatus.Pending, ERPTransactionStatus.Approved)
            obj.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("GL_Account_Code"))
            obj.Is_Empty = clsCommon.myCdbl(dt.Rows(0)("Is_Empty"))
            obj.FarmerInVendor = clsCommon.myCstr(dt.Rows(0)("FarmerInVendor"))
            qry = "Select * from TSPL_VCGL_Detail where Document_No='" + strDocumentNo + "' ORDER BY Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsVCGLDetail)
                Dim objTr As clsVCGLDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsVCGLDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.VCGL_Code = clsCommon.myCstr(dr("VCGL_Code"))
                    objTr.VCGL_Name = clsCommon.myCstr(dr("VCGL_Name"))

                    objTr.Dr_Amount = clsCommon.myCdbl(dr("Dr_Amount"))
                    objTr.Cr_Amount = clsCommon.myCdbl(dr("Cr_Amount"))
                    objTr.GL_Account_Code = clsCommon.myCstr(dr("GL_Account_Code"))
                    objTr.GL_Account_Desc = clsCommon.myCstr(dr("GL_Account_Desc"))
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
            isSaved = clsVCGLHead.PostData(strDocNo, trans)
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

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Post")
        End If

        Dim obj As clsVCGLHead = clsVCGLHead.GetData(strDocNo, trans)
        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (clsCommon.myLen(obj.Posting_Date) > 0) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If
        If (obj.On_Hold) Then
            Throw New Exception("Document No " + obj.Document_No + " Is currently On Hold.Can't Post it")
        End If
        If clsCommon.myLen(obj.GL_Account_Code) <= 0 Then
            Throw New Exception("Vendor/Customer's Control A/C Not found")
        End If
        Dim ArryLst As ArrayList = New ArrayList()
        Dim Acc() As String = {obj.GL_Account_Code, obj.Amount * IIf(clsCommon.CompairString(obj.Amount_Type, "Dr") = CompairStringResult.Equal, -1, 1)}
        ArryLst.Add(Acc)
        For Each objtr As clsVCGLDetail In obj.Arr
            If objtr.Dr_Amount > 0 Then
                Dim Acc1() As String = {objtr.GL_Account_Code, objtr.Dr_Amount}
                ArryLst.Add(Acc1)
            ElseIf objtr.Cr_Amount > 0 Then
                Dim Acc1() As String = {objtr.GL_Account_Code, -1 * objtr.Cr_Amount}
                ArryLst.Add(Acc1)
            End If
        Next
        Dim strCap As String = "GL Entry for " + IIf(clsCommon.CompairString(obj.Document_Type, "V") = CompairStringResult.Equal, "Vendor", "Customer") + " " + obj.VC_Code + " (" + obj.VC_Name + " ), Documnet No " + obj.Document_No
        '' Anubhooti 18-Mar-2015 (Cancel VCGL GL)
        'clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Segment, True, trans, obj.Document_Date, strCap, "VC-GL", "VCGL Entry", obj.Document_No, obj.Remarks, obj.Document_Type, obj.VC_Code, obj.VC_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", obj.Remarks, "")

        Dim qry As String = "update TSPL_JOURNAL_DETAILS set TSPL_JOURNAL_DETAILS.CustVend_Code=xxxx.VC_Code,TSPL_JOURNAL_DETAILS.CustVend_Name=xxxx.VC_Name  from ("
        qry += " select * from ("
        qry += " select (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No=Document_No and Source_Code ='VC-GL' )as VoucherNo, Document_No,GL_Account_Code,Amount*(case when Amount_Type='Cr' then -1 else 1 end) as Amount,VC_Code,VC_Name from ("
        qry += " select Document_No, GL_Account_Code,case when Amount_Type='Dr' then 'Cr' else 'Dr' end as Amount_Type,Amount,VC_Code,VC_Name from TSPL_VCGL_Head where Document_No='" + obj.Document_No + "'"
        qry += "  union all "
        qry += " select Document_No,GL_Account_Code,case when Dr_Amount>0 then 'Dr' else 'Cr' end as Amount_Type ,case when Dr_Amount>0 then Dr_Amount else Cr_Amount end as Amount,VCGL_Code as VC_Code,VCGL_Name as VC_Name from TSPL_VCGL_Detail where Document_No='" + obj.Document_No + "' AND Row_Type in ('Vendor','Customer')"
        qry += " )xxx"
        qry += " )xxxxx where len(isnull(xxxxx.VoucherNo,''))>0"
        qry += " )xxxx"
        qry += " inner join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=xxxx.VoucherNo and TSPL_JOURNAL_DETAILS.Amount=xxxx.Amount and TSPL_JOURNAL_DETAILS.Account_code=xxxx.GL_Account_Code"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '' ******************************** Anubhooti 18-Mar-2015 (VCGL AUTO DEBIT/CREDIT WITH THEIR GL's) *********************************************
        Dim AllowTransferVSPAmtToFarmerinVCGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowTransferVSPAmtToFarmerinVCGL, clsFixedParameterCode.AllowTransferVSPAmtToFarmerinVCGL, trans)) = 1, True, False)


        If clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal Then
            CreateARInvoiceHeader(obj, trans)
        ElseIf clsCommon.CompairString(obj.Document_Type, "V") = CompairStringResult.Equal Then
            CreateAPInvoiceHeader(obj, trans)
        End If

        'For Each objtr1 As clsVCGLDetail In obj.Arr
        'If clsCommon.CompairString(objtr1.Row_Type, "Customer") = CompairStringResult.Equal Then

        If AllowTransferVSPAmtToFarmerinVCGL Then
            Dim strRowType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Row_type from tspl_vcgl_detail where document_no='" & obj.Document_No & "'", trans))
            If clsCommon.CompairString(strRowType, "Farmer") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Document_Type, "F") <> CompairStringResult.Equal Then
                CreateJournalEntry_FromVSPToFarmer(obj, trans, "")
            ElseIf clsCommon.CompairString(obj.Document_Type, "F") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strRowType, "Farmer") = CompairStringResult.Equal Then
                CreateJournalEntry_FromFarmerToFarmer(obj, trans, "")
            ElseIf clsCommon.CompairString(obj.Document_Type, "F") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strRowType, "GL") = CompairStringResult.Equal Then
                CreateJournalEntry_FromFarmertoFarmer(obj, trans, "")
            Else
                CreateARInvoiceDetail(obj, trans)
                CreateAPInvoiceDetail(obj, trans)
            End If
        Else
            CreateARInvoiceDetail(obj, trans)
            CreateAPInvoiceDetail(obj, trans)
        End If

        'End If
        'Next
        '' ******************************************************* '' ***********************************************************************************
        qry = "update TSPL_VCGL_Head set Status=1,Posting_Date=TSPL_VCGL_Head.Document_Date where Document_No='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_VCGL_Head", "Document_No", "TSPL_VCGL_Detail", "Document_No", trans)

        Return True
    End Function
    '' Anubhooti 17-Mar-2015 (CUSTOMER : Auto Debit/Credit for header level)
    Private Shared Function CreateARInvoiceHeader(ByVal obj As clsVCGLHead, ByVal trans As SqlTransaction) As Boolean
        '''''''''''''''''''''''''''''''''' For Making AR Invoice ''''''''''''''''''''''''''''''''''
        Dim ARNote As String = String.Empty

        Dim objCustInv As New clsCustomerInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed
        objCustInv.Document_Date = obj.Document_Date
        If clsCommon.CompairString(obj.Amount_Type, "Cr") = CompairStringResult.Equal Then
            objCustInv.Document_Type = "D"
            ARNote = "Debit Note"
        Else
            objCustInv.Document_Type = "C"
            ARNote = "Credit Note"
        End If
        objCustInv.loc_code = obj.Location_Segment
        objCustInv.Document_Total = obj.Amount
        objCustInv.Customer_Code = obj.VC_Code
        objCustInv.Customer_Name = obj.VC_Name
        objCustInv.Posting_Date = obj.Document_Date
        Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.VC_Code + "'"
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
        ''objCustInv.Order_No
        'objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Location_Segment, trans)
        objCustInv.On_Hold = 0
        objCustInv.Remarks = obj.Remarks
        objCustInv.Description = obj.Description
        objCustInv.Trans_Type = "VC"
        ''richa agarwal 24 nov,2016
        objCustInv.Balance_Amt = objCustInv.Document_Total
        objCustInv.Discount_Base = objCustInv.Document_Total
        ''--------------------
        objCustInv.TapalNo = obj.TapalNo
        If clsCommon.myLen(obj.DateAndTime) > 0 Then
            objCustInv.DateAndTime = obj.DateAndTime
        End If
        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)

        '' Detail Level Saving
        Dim counter As Integer = 1
        Dim CustAccSet As String = String.Empty
        For Each objTr As clsVCGLDetail In obj.Arr
            If counter = 1 Then

                Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
                CustAccSet = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Receivable_Control_acct,'') As [Receivable_Control_acct] FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.VC_Code + "' ", trans))

                objCustInvTR.SNo = counter
                If clsCommon.myLen(CustAccSet) <= 0 Then
                    Throw New Exception("Please set customer account set for customer -" + obj.VC_Code)
                End If
                objCustInv.Against_VCGL = obj.Document_No
                objCustInvTR.Amount = obj.Amount
                objCustInvTR.Amount_less_Discount = obj.Amount
                objCustInvTR.Total_Amount = obj.Amount
                Dim GLClrAcc As String = String.Empty
                GLClrAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Clearing_Account,'') AS Clearing_Account FROM TSPL_GLSETTING ", trans))
                GLClrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(GLClrAcc, obj.Location_Segment, True, trans)
                If clsCommon.myLen(GLClrAcc) <= 0 Then
                    Throw New Exception("Please set clearing account on GL option.")
                End If
                objCustInvTR.GL_Account_Code = GLClrAcc
                objCustInv.Customer_Control_AC = CustAccSet
                objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & GLClrAcc & "'", trans))
                objCustInv.Arr.Add(objCustInvTR)
                counter += 1

                '' ***************************************** GL Entry ***************************************** ''
                '' CUSTOMER ACCOUNT
                'Dim arrlist As New ArrayList()
                'CustAccSet = clsERPFuncationality.ChangeGLAccountLocationSegment(CustAccSet, obj.Location_Segment, trans)
                'If clsCommon.myLen(CustAccSet) <= 0 Then
                '    Throw New Exception("" & CustAccSet & " does not exits.")
                'End If
                'If clsCommon.CompairString(obj.Amount_Type, "Dr") = CompairStringResult.Equal Then
                '    Dim CustAcc() As String = {CustAccSet, obj.Amount} '' Credit
                '    arrlist.Add(CustAcc)
                'ElseIf clsCommon.CompairString(obj.Amount_Type, "Cr") = CompairStringResult.Equal Then
                '    Dim CustAcc() As String = {CustAccSet, -1 * obj.Amount} '' Debit
                '    arrlist.Add(CustAcc)
                'End If

                ' '' CLEARING ACCOUNT
                'Dim GLClrAcc As String = String.Empty
                'GLClrAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Clearing_Account,'') AS Clearing_Account FROM TSPL_GLSETTING ", trans))
                'If clsCommon.myLen(GLClrAcc) <= 0 Then
                '    Throw New Exception("Please set clearing account on GL option.")
                'End If
                'GLClrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(GLClrAcc, obj.Location_Segment, trans)
                'If clsCommon.myLen(GLClrAcc) <= 0 Then
                '    Throw New Exception("" & GLClrAcc & " does not exits.")
                'End If
                'If clsCommon.CompairString(obj.Amount_Type, "Dr") = CompairStringResult.Equal Then
                '    Dim ClearAcc = New String() {GLClrAcc, -1 * obj.Amount} '' Debit
                '    arrlist.Add(ClearAcc)
                'ElseIf clsCommon.CompairString(obj.Amount_Type, "Cr") = CompairStringResult.Equal Then
                '    Dim ClearAcc() As String = {GLClrAcc, obj.Amount} '' Credit
                '    arrlist.Add(ClearAcc)
                'End If


                objCustInv.SaveData(objCustInv, True, trans, "")
                clsCustomerInvoiceHead.PostData("", objCustInv.Document_No, "", trans)
                'clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Segment, False, trans, obj.Document_Date, "AR AGAINST VCGL-" & obj.Document_No & "", "AR-VC", "AR " & ARNote, objCustInv.Document_No, objCustInv.Description, obj.Document_Type, clsCommon.myCstr(obj.VC_Code), clsCommon.myCstr(obj.VC_Name), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist)
            End If
        Next

        Return True
    End Function
    ''
    '' Anubhooti 17-Mar-2015 (CUSTOMER : Auto Debit/Credit for Detail level)
    Private Shared Function CreateARInvoiceDetail(ByVal obj As clsVCGLHead, ByVal trans As SqlTransaction) As Boolean
        '''''''''''''''''''''''''''''''''' For Making AR Invoice ''''''''''''''''''''''''''''''''''
        Dim ARNote As String = String.Empty
        'Dim objCustInv As New clsCustomerInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed

        'If clsCommon.CompairString(obj.Amount_Type, "Cr") = CompairStringResult.Equal Then
        '    objCustInv.Document_Type = "D"
        'Else
        '    objCustInv.Document_Type = "C"
        'End If



        '' Detail Level Saving
        Dim counter As Integer = 1
        Dim CustAccSet As String = String.Empty
        For Each objTr As clsVCGLDetail In obj.Arr
            '' AR ENTRIES
            If clsCommon.CompairString(objTr.Row_Type, "Customer") = CompairStringResult.Equal Then
                Dim objCustInv As New clsCustomerInvoiceHead()
                objCustInv.Document_Total = IIf(clsCommon.myCdbl(objTr.Dr_Amount) > 0, objTr.Dr_Amount, objTr.Cr_Amount)
                objCustInv.Balance_Amt = objCustInv.Document_Total
                objCustInv.Posting_Date = obj.Document_Date
                ''richa 01 Nov,2018
                objCustInv.Discount_Base = objCustInv.Document_Total
                objCustInv.On_Hold = 0

                objCustInv.Description = obj.Description
                objCustInv.Document_Date = obj.Document_Date
                objCustInv.TapalNo = obj.TapalNo
                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    objCustInv.DateAndTime = obj.DateAndTime
                End If
                objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)

                Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
                CustAccSet = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Receivable_Control_acct,'') As [Receivable_Control_acct] FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + objTr.VCGL_Code + "' ", trans))
                objCustInvTR.SNo = counter
                If clsCommon.myLen(CustAccSet) <= 0 Then
                    Throw New Exception("Please set customer account set for customer -" + objTr.VCGL_Code)
                End If
                If clsCommon.CompairString(objTr.Row_Type, "Customer") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(objTr.Dr_Amount) > 0 Then
                        objCustInv.Document_Type = "D"
                        ARNote = "Debit Note"
                    ElseIf clsCommon.myCdbl(objTr.Cr_Amount) > 0 Then
                        objCustInv.Document_Type = "C"
                        ARNote = "Credit Note"
                    End If
                End If
                objCustInv.Trans_Type = "VC"
                objCustInvTR.Remarks = obj.Description
                objCustInv.Against_VCGL = obj.Document_No
                objCustInv.Customer_Control_AC = CustAccSet
                objCustInvTR.Amount = IIf(clsCommon.myCdbl(objTr.Dr_Amount) > 0, objTr.Dr_Amount, objTr.Cr_Amount)
                objCustInvTR.Amount_less_Discount = IIf(clsCommon.myCdbl(objTr.Dr_Amount) > 0, objTr.Dr_Amount, objTr.Cr_Amount)
                objCustInvTR.Total_Amount = IIf(clsCommon.myCdbl(objTr.Dr_Amount) > 0, objTr.Dr_Amount, objTr.Cr_Amount)
                Dim GLClrAcc As String = String.Empty
                GLClrAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Clearing_Account,'') AS Clearing_Account FROM TSPL_GLSETTING ", trans))
                GLClrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(GLClrAcc, obj.Location_Segment, True, trans)
                If clsCommon.myLen(GLClrAcc) <= 0 Then
                    Throw New Exception("Please set clearing account on GL option.")
                End If
                objCustInvTR.GL_Account_Code = GLClrAcc
                objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & GLClrAcc & "'", trans))
                objCustInv.Customer_Code = objTr.VCGL_Code
                objCustInv.Customer_Name = objTr.VCGL_Name
                Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + objTr.VCGL_Code + "'"
                objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
                ' objCustInv.loc_code = clsERPFuncationality.GetLocationSegment(objTr.GL_Account_Code, trans)
                objCustInv.loc_code = objTr.GL_Account_Code.Substring(objTr.GL_Account_Code.Length - 3)
                objCustInv.Arr.Add(objCustInvTR)
                'counter += 1

                objCustInv.SaveData(objCustInv, True, trans, "")
                clsCustomerInvoiceHead.PostData("", objCustInv.Document_No, "", trans)
                ' clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Segment, False, trans, obj.Document_Date, "AR AGAINST VCGL-" & obj.Document_No & "", "AR-VC", "AR " & ARNote, objCustInv.Document_No, objCustInv.Description, obj.Document_Type, clsCommon.myCstr(obj.VC_Code), clsCommon.myCstr(obj.VC_Name), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrlist)

            End If
        Next

        Return True
    End Function
    ''

    '' Anubhooti 19-Mar-2015 (VENDOR : Auto Debit/Credit for header level)
    Private Shared Function CreateAPInvoiceHeader(ByVal obj As clsVCGLHead, ByVal trans As SqlTransaction) As Boolean
        '''''''''''''''''''''''''''''''''' For Making AR Invoice ''''''''''''''''''''''''''''''''''
        Dim ARNote As String = String.Empty

        Dim objVendInv As New clsVedorInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed
        objVendInv.Invoice_Entry_Date = obj.Document_Date
        If clsCommon.CompairString(obj.Amount_Type, "Cr") = CompairStringResult.Equal Then
            objVendInv.Document_Type = "D"
            ARNote = "Debit Note"
        Else
            objVendInv.Document_Type = "C"
            ARNote = "Credit Note"
        End If
        objVendInv.Invoice_Type = "VC"
        objVendInv.loc_code = obj.Location_Segment
        objVendInv.Document_Total = obj.Amount
        objVendInv.Vendor_Code = obj.VC_Code
        objVendInv.Vendor_Name = obj.VC_Name
        objVendInv.Posting_Date = obj.Document_Date
        objVendInv.Vendor_Invoice_Date = obj.Document_Date
        Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.VC_Code + "'"
        objVendInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
        ''objCustInv.Order_No
        ' objVendInv.loc_code = clsLocation.GetSegmentCode(obj.Location_Segment, trans)
        objVendInv.On_Hold = 0
        objVendInv.Remarks = obj.Remarks
        objVendInv.Description = obj.Description
        objVendInv.Balance_Amt = objVendInv.Document_Total
        objVendInv.Against_VCGL = obj.Document_No
        '=========Added by preeti Gupta 29/10/2018
        objVendInv.Amount_Less_Discount = objVendInv.Document_Total
        objVendInv.Discount_Base = objVendInv.Document_Total
        '=========================================================
        objVendInv.TapalNo = obj.TapalNo
        If clsCommon.myLen(obj.DateAndTime) > 0 Then
            objVendInv.DateAndTime = obj.DateAndTime
        End If
        objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

        '' Detail Level Saving
        Dim counter As Integer = 1
        Dim VendAccSet As String = String.Empty
        For Each objTr As clsVCGLDetail In obj.Arr
            If counter = 1 Then

                Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()
                VendAccSet = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_VENDOR_ACCOUNT_SET.Payable_Account FROM TSPL_VENDOR_MASTER LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Account  WHERE TSPL_VENDOR_MASTER.Vendor_Code ='" + obj.VC_Code + "' ", trans))

                objVendInvTR.Detail_Line_No = counter
                If clsCommon.myLen(VendAccSet) <= 0 Then
                    Throw New Exception("Please set vendor account set for vendor -" + obj.VC_Code)
                End If
                'objVendInv.Against_VCGL = obj.Document_No
                objVendInvTR.Amount = obj.Amount
                objVendInvTR.Amount_less_Discount = obj.Amount
                objVendInvTR.Total_Amount = obj.Amount
                Dim GLClrAcc As String = String.Empty
                GLClrAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Clearing_Account,'') AS Clearing_Account FROM TSPL_GLSETTING ", trans))
                GLClrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(GLClrAcc, obj.Location_Segment, True, trans)
                If clsCommon.myLen(GLClrAcc) <= 0 Then
                    Throw New Exception("Please set clearing account on GL option.")
                End If
                objVendInvTR.GL_Account_Code = GLClrAcc
                ' objVendInvTR. = VendAccSet
                objVendInv.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  ISNULL(Vendor_Account,'') AS [Vendor_Account] FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" + obj.VC_Code + "'", trans))
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendInv.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendInv.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Vendor_Control_AC, obj.Location_Segment, True, trans)
                    If clsCommon.myCdbl(objVendInv.Discount_Amount) > 0 Then
                        objVendInv.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendInv.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Discount_GL_AC, obj.Location_Segment, True, trans)
                    End If
                End If
                If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If

                objVendInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & GLClrAcc & "'", trans))
                objVendInv.Arr.Add(objVendInvTR)
                counter += 1
                objVendInv.SaveData(objVendInv, True, trans)
                clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
            End If
        Next

        Return True
    End Function
    '' Anubhooti 19-Mar-2015 (VENDOR : Auto Debit/Credit for Detail level)
    Private Shared Function CreateAPInvoiceDetail(ByVal obj As clsVCGLHead, ByVal trans As SqlTransaction) As Boolean
        '''''''''''''''''''''''''''''''''' For Making AR Invoice ''''''''''''''''''''''''''''''''''
        Dim ARNote As String = String.Empty

        '' Detail Level Saving

        For Each objTr As clsVCGLDetail In obj.Arr
            If clsCommon.CompairString(objTr.Row_Type, "Vendor") = CompairStringResult.Equal Then
                Dim counter As Integer = 1
                Dim CustAccSet As String = String.Empty
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                objVendorInvHead = New clsVedorInvoiceHead()
                objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy")
                objVendorInvHead.Vendor_Code = objTr.VCGL_Code
                objVendorInvHead.Vendor_Name = objTr.VCGL_Name
                objVendorInvHead.Invoice_Type = "VC"
                objVendorInvHead.Vendor_Invoice_Date = clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy")
                objVendorInvHead.loc_code = obj.Location_Segment
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  ISNULL(Vendor_Account,'') AS [Vendor_Account] FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" + objTr.VCGL_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor account set for vendor : " + objTr.VCGL_Code)
                End If

                objVendorInvHead.On_Hold = False
                objVendorInvHead.Discount_Base = 0
                objVendorInvHead.Discount_Amount = 0
                objVendorInvHead.Amount_Less_Discount = 0
                objVendorInvHead.Total_Tax = 0
                objVendorInvHead.Document_Total = 0
                objVendorInvHead.Balance_Amt = 0
                objVendorInvHead.Against_VCGL = obj.Document_No
                objVendorInvHead.TapalNo = obj.TapalNo
                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    objVendorInvHead.DateAndTime = obj.DateAndTime
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Location_Segment, True, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Location_Segment, True, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If

                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                Dim ii As Integer = 0
                ii = 1
                objVendorInvHead.Total_Landed_Amt = 0

                If clsCommon.CompairString(objTr.Row_Type, "Vendor") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(objTr.Dr_Amount) > 0 Then
                        objVendorInvHead.Document_Type = "D"
                        ARNote = "Debit Note"
                    ElseIf clsCommon.myCdbl(objTr.Cr_Amount) > 0 Then
                        objVendorInvHead.Document_Type = "C"
                        ARNote = "Credit Note"
                    End If
                End If
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                objVendorInvDetail.Detail_Line_No = ii
                Dim GLClrAccAP As String = String.Empty
                GLClrAccAP = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Clearing_Account,'') AS Clearing_Account FROM TSPL_GLSETTING ", trans))
                GLClrAccAP = clsERPFuncationality.ChangeGLAccountLocationSegment(GLClrAccAP, obj.Location_Segment, True, trans)
                If clsCommon.myLen(GLClrAccAP) <= 0 Then
                    Throw New Exception("Please set clearing account on GL option.")
                End If
                objVendorInvDetail.GL_Account_Code = GLClrAccAP
                objVendorInvDetail.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & GLClrAccAP & "'", trans))
                objVendorInvDetail.Amount = IIf(clsCommon.myCdbl(objTr.Dr_Amount) > 0, objTr.Dr_Amount, objTr.Cr_Amount)
                objVendorInvDetail.Amount_less_Discount = IIf(clsCommon.myCdbl(objTr.Dr_Amount) > 0, objTr.Dr_Amount, objTr.Cr_Amount)
                objVendorInvDetail.Total_Amount = IIf(clsCommon.myCdbl(objTr.Dr_Amount) > 0, objTr.Dr_Amount, objTr.Cr_Amount)
                objVendorInvHead.Document_Total = objVendorInvDetail.Total_Amount
                objVendorInvHead.Balance_Amt = objVendorInvHead.Document_Total
                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
                objVendorInvHead.Vendor_Code = objTr.VCGL_Code
                objVendorInvHead.Vendor_Name = objTr.VCGL_Name
                objVendorInvHead.Balance_Amt = objVendorInvHead.Document_Total
                ''richa 01 Nov,2018
                objVendorInvHead.Discount_Base = objVendorInvHead.Document_Total
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If

                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''RICHA AGARWAL 
                objVendorInvHead.Remarks = objTr.Remarks
                ''-----------------------

                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.Document_Date)
            End If
        Next
        Return True
    End Function
    Public Shared Sub CreateJournalEntry_FromFarmerToFarmer(ByVal obj As clsVCGLHead, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing)
        Try
            Dim ArryLst As ArrayList = New ArrayList()
            Dim GLClrAccFarmerHeader As String = String.Empty
            Dim GLFarmerAccount As String = String.Empty
            GLClrAccFarmerHeader = obj.GL_Account_Code
            GLClrAccFarmerHeader = clsERPFuncationality.ChangeGLAccountLocationSegment(GLClrAccFarmerHeader, obj.Location_Segment, True, trans)
            For Each objTr As clsVCGLDetail In obj.Arr

                If clsCommon.CompairString(objTr.Row_Type, "Farmer") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Row_Type, "GL") = CompairStringResult.Equal Then

                    'GLClrAccAP = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Clearing_Account,'') AS Clearing_Account FROM TSPL_GLSETTING ", trans))
                    'GLClrAccAP = clsERPFuncationality.ChangeGLAccountLocationSegment(GLClrAccAP, obj.Location_Segment, True, trans)
                    'If clsCommon.myLen(GLClrAccFarmerHeader) <= 0 Then
                    '    Throw New Exception("Please set clearing account on GL option.")
                    'End If

                    GLFarmerAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(objTr.GL_Account_Code, obj.Location_Segment, True, trans)

                    If clsCommon.myCdbl(objTr.Dr_Amount) > 0 Then
                        Dim Acc() As String = {GLFarmerAccount, objTr.Dr_Amount}
                        ArryLst.Add(Acc)

                        Dim Acc1() As String = {GLClrAccFarmerHeader, -1 * objTr.Dr_Amount}
                        ArryLst.Add(Acc1)
                    Else
                        Dim Acc() As String = {GLClrAccFarmerHeader, objTr.Cr_Amount}
                        ArryLst.Add(Acc)

                        Dim Acc1() As String = {GLFarmerAccount, -1 * objTr.Cr_Amount}
                        ArryLst.Add(Acc1)
                    End If

                End If
            Next
            obj.Remarks = "Journal entry against VCGL " & obj.Document_No & " created Amount Transfer to Farmer From Farmer"
            Dim objJE As New clsJEExtraColumns
            If clsCommon.myLen(obj.VC_Code) > 0 Then
                objJE.VSP_CODE = clsCommon.myCstr(obj.VC_Code)

            End If
            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Segment, True, strVoucherNoForRecreateOnly, trans, obj.Document_Date, obj.Remarks, "VC-GL", "VCGL Farmer", obj.Document_No, "", "O", obj.VC_Code, obj.VC_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Description, obj.Remarks, Nothing, Nothing, objJE)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Segment, True, trans, obj.Document_Date, obj.Remarks, "VC-GL", "VCGL Farmer", obj.Document_No, "", "O", obj.VC_Code, obj.VC_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Description, obj.Remarks, Nothing, Nothing, objJE)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Public Shared Sub CreateJournalEntry_FromVSPToFarmer(ByVal obj As clsVCGLHead, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing)
        Try
            Dim ArryLst As ArrayList = New ArrayList()
            Dim GLClrAccAP As String = String.Empty
            Dim GLFarmerAccount As String = String.Empty
            For Each objTr As clsVCGLDetail In obj.Arr
                If clsCommon.CompairString(objTr.Row_Type, "Farmer") = CompairStringResult.Equal Then

                    GLClrAccAP = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Clearing_Account,'') AS Clearing_Account FROM TSPL_GLSETTING ", trans))
                    GLClrAccAP = clsERPFuncationality.ChangeGLAccountLocationSegment(GLClrAccAP, obj.Location_Segment, True, trans)
                    If clsCommon.myLen(GLClrAccAP) <= 0 Then
                        Throw New Exception("Please set clearing account on GL option.")
                    End If

                    GLFarmerAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(objTr.GL_Account_Code, obj.Location_Segment, True, trans)

                    If clsCommon.myCdbl(objTr.Dr_Amount) > 0 Then
                        Dim Acc() As String = {GLFarmerAccount, objTr.Dr_Amount}
                        ArryLst.Add(Acc)

                        Dim Acc1() As String = {GLClrAccAP, -1 * objTr.Dr_Amount}
                        ArryLst.Add(Acc1)
                    Else
                        Dim Acc() As String = {GLClrAccAP, objTr.Cr_Amount}
                        ArryLst.Add(Acc)

                        Dim Acc1() As String = {GLFarmerAccount, -1 * objTr.Cr_Amount}
                        ArryLst.Add(Acc1)
                    End If

                End If
            Next
            obj.Remarks = "Journal entry against VCGL " & obj.Document_No & " created Amount Transfer to Farmer From VSP"
            Dim objJE As New clsJEExtraColumns
            If clsCommon.myLen(obj.VC_Code) > 0 Then
                objJE.VSP_CODE = clsCommon.myCstr(obj.VC_Code)

            End If
            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Segment, True, strVoucherNoForRecreateOnly, trans, obj.Document_Date, obj.Remarks, "VC-GL", "VCGL Farmer", obj.Document_No, "", "V", obj.VC_Code, obj.VC_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Description, obj.Remarks, Nothing, Nothing, objJE)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Segment, True, trans, obj.Document_Date, obj.Remarks, "VC-GL", "VCGL Farmer", obj.Document_No, "", "V", obj.VC_Code, obj.VC_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Description, obj.Remarks, Nothing, Nothing, objJE)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsVCGLHead = clsVCGLHead.GetData(strDocNo)
        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleGL, clsUserMgtCode.mbtnVCGLEntry, obj.Location_Segment, obj.Document_Date, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If (clsCommon.myLen(obj.Posting_Date) > 0) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_VCGL_Head", "Document_No", "TSPL_VCGL_Detail", "Document_No", "TSPL_REMITTANCE", "Document_No", trans)
                Dim qry As String = "delete from TSPL_VCGL_Detail where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_REMITTANCE where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_VCGL_Head where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
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

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Status from TSPL_VCGL_Head where Document_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            If CheckDocumentUsedBeforeReverseUnpost(strCode, trans) = True Then
                Throw New Exception(" Document already used in (Receipt Entry/Payment Entry/Payment Process).You can not reverse and unpost this document.")
            End If
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='VC-GL' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='VC-GL' and Source_Doc_No='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='VC-GL' and Source_Doc_No='" + strCode + "') "
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            '' documents deleted from AP invoice entry with its Journal voucher
            Dim DocNo As String = clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_VENDOR_INVOICE_HEAD where Against_VCGL ='" + strCode + "'", trans)
            If clsCommon.myLen(DocNo) > 0 Then
                Qry = "delete  from TSPL_JOURNAL_DETAILS where Voucher_No in (Select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No in (Select Document_No  from TSPL_VENDOR_INVOICE_HEAD where Against_VCGL ='" + strCode + "'))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete  from TSPL_JOURNAL_MASTER where Source_Doc_No in (Select Document_No  from TSPL_VENDOR_INVOICE_HEAD where Against_VCGL ='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete   from TSPL_VENDOR_INVOICE_DETAIL  where Document_No in (Select Document_No  from TSPL_VENDOR_INVOICE_HEAD where Against_VCGL ='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete   from TSPL_VENDOR_INVOICE_HEAD where Against_VCGL ='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            End If

            '' documents deleted from AR invoice entry with its Journal voucher
            DocNo = clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_Customer_Invoice_Head  where Against_VCGL='" + strCode + "'", trans)
            If clsCommon.myLen(DocNo) > 0 Then
                Qry = "delete  from TSPL_JOURNAL_DETAILS where Voucher_No in (Select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No in (Select Document_No  from TSPL_Customer_Invoice_Head  where Against_VCGL ='" + strCode + "'))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete   from TSPL_JOURNAL_MASTER where Source_Doc_No in (Select Document_No  from TSPL_Customer_Invoice_Head  where Against_VCGL ='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete  from TSPL_Customer_Invoice_DETAIL  where  Document_No in (Select Document_No  from TSPL_Customer_Invoice_Head  where Against_VCGL ='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete  from TSPL_Customer_Invoice_Head  where Against_VCGL ='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            End If

            Qry = "Update TSPL_VCGL_Head set Status = 0,Posting_Date=null where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_VCGL_Head", "Document_No", "TSPL_VCGL_Detail", "Document_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ' Ticket No : UDL/12/08/19-000312  By prabhakar
    Public Shared Function CheckDocumentUsedBeforeReverseUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim Qry As String = " Select count ( Final.Against_VCGL) from ( " & _
        " select TSPL_VENDOR_INVOICE_HEAD.Against_VCGL from TSPL_PAYMENT_PROCESS_DEDUCTION  inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No = TSPL_VENDOR_INVOICE_HEAD.Document_No where TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  = '" + strCode + "' " & _
        " union all " & _
        " select TSPL_Customer_Invoice_Head.Against_VCGL from TSPL_PAYMENT_PROCESS_CREDIT_NOTE inner join TSPL_Customer_Invoice_Head on TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No = TSPL_Customer_Invoice_Head .Document_No where TSPL_Customer_Invoice_Head.Against_VCGL = '" + strCode + "' " & _
        " Union all " & _
        " select TSPL_Customer_Invoice_Head.Against_VCGL from TSPL_RECEIPT_DETAIL inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No = TSPL_RECEIPT_DETAIL.Document_No where TSPL_Customer_Invoice_Head.Against_VCGL = '" + strCode + "' " & _
        " Union All " & _
        "  select TSPL_Customer_Invoice_Head.Against_VCGL from TSPL_RECEIPT_HEADER  inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No = TSPL_RECEIPT_HEADER.Applied_Receipt  where TSPL_Customer_Invoice_Head.Against_VCGL ='" + strCode + "' " & _
        " Union All " & _
        " select TSPL_VENDOR_INVOICE_HEAD.Against_VCGL from TSPL_PAYMENT_DETAIL  inner join  TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_DETAIL.Document_No where TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  = '" + strCode + "' " & _
        "  ) Final "
        Dim isRecordExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(Qry, trans))
        Return isRecordExist
    End Function

End Class

Public Class clsVCGLDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public VCGL_Code As String = Nothing
    Public VCGL_Name As String = Nothing
    Public Dr_Amount As Double = 0
    Public Cr_Amount As Double = 0
    Public GL_Account_Code As String = Nothing
    Public GL_Account_Desc As String = Nothing
    Public Remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strLocaSegment As String, ByVal Arr As List(Of clsVCGLDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsVCGLDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "VCGL_Code", obj.VCGL_Code)
                clsCommon.AddColumnsForChange(coll, "VCGL_Name", obj.VCGL_Name)
                clsCommon.AddColumnsForChange(coll, "Dr_Amount", obj.Dr_Amount)
                clsCommon.AddColumnsForChange(coll, "Cr_Amount", obj.Cr_Amount)

                If Not (clsCommon.CompairString(obj.Row_Type, "GL") = CompairStringResult.Equal) Then
                    obj.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.GL_Account_Code, strLocaSegment, True, trans)
                End If


                clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Desc", clsGLAccount.GetName(obj.GL_Account_Code, trans))
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VCGL_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class