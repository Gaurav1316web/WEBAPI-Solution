Imports System.Data.SqlClient
Imports common
Public Class clsJobWorkDebitNote
    Public AgainstDocumentNo As String = Nothing
    Public DocumentDate As DateTime?
    Public ItemCode As String = Nothing
    Public ItemName As String = Nothing
    Public FatPer As Decimal = Nothing
    Public SnfPer As Decimal = Nothing
    Public Qty As Decimal = Nothing
    Public UOM As String = Nothing
    Public NetAmt As Decimal = Nothing
    Public rate As Decimal = Nothing
    Public IsSelect As Boolean = False
    Public Gain_Amount As Decimal
    Public Loss_Amount As Decimal




    Public Shared Function GetData(ByVal Location As String, ByVal Customer As String, ByVal FromDate As String, ByVal ToDate As String, ByVal trans As SqlTransaction) As List(Of clsJobWorkDebitNote)

        Dim arr As New List(Of clsJobWorkDebitNote)
        Dim qry As String = "select TSPL_SCRAPSALE_Detail.*,convert(varchar(20),TSPL_SCRAPSALE_HEAD.Shipment_Date,103) as Shipment_Date,TSPL_SCRAPSALE_HEAD.ship_Total_Amt,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_UOM_DETAIL.Job_Work_Rate  from TSPL_SCRAPSALE_HEAD" & _
 " left join TSPL_SCRAPSALE_Detail on TSPL_SCRAPSALE_Detail.shipment_No =TSPL_SCRAPSALE_HEAD.shipment_No " & _
 " left join TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code =TSPL_SCRAPSALE_HEAD.Loc_Code " & _
 "left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code " & _
" left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
" left join TSPL_JOBWORK_DEBIT_NOTE_DETAIL on TSPL_JOBWORK_DEBIT_NOTE_DETAIL.AgainstDocument_No=TSPL_SCRAPSALE_HEAD.shipment_No " & _
                          " where TSPL_SCRAPSALE_HEAD.Loc_Code='" & Location & "' and TSPL_SCRAPSALE_HEAD.Cust_Code='" & Customer & "' and TSPL_SCRAPSALE_HEAD.shipment_Date>=convert(date,'" & FromDate & "',103) and TSPL_SCRAPSALE_HEAD.shipment_Date<=convert(date,'" & ToDate & "',103)  and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 "
        Dim dt As DataTable
        Try
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                Dim qry1 As String = clsDBFuncationality.getSingleValue(" Select AgainstDocument_No from TSPL_JOBWORK_DEBIT_NOTE_DETAIL where AgainstDocument_No='" & clsCommon.myCstr(dr("Shipment_No")) & "'")
                If clsCommon.myLen(qry1) <= 0 Then
                    Dim obj As New clsJobWorkDebitNote
                    obj.AgainstDocumentNo = clsCommon.myCstr(dr("Shipment_No"))
                    obj.DocumentDate = clsCommon.myCstr(dr("Shipment_Date"))
                    obj.ItemCode = clsCommon.myCstr(dr("Item_code"))
                    obj.ItemName = clsCommon.myCstr(dr("Item_desc"))
                    obj.Qty = clsCommon.myCdbl(dr("shipped_qty"))
                    obj.FatPer = clsCommon.myCstr(dr("FAT"))
                    obj.SnfPer = clsCommon.myCstr(dr("SNF"))
                    obj.NetAmt = clsCommon.myCstr(dr("ship_Total_Amt"))
                    obj.rate = clsCommon.myCstr(dr("Job_Work_Rate"))

                    arr.Add(obj)
                End If
            Next
            Return arr

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveDataDetail(ByVal strDocNo As String, ByVal Arr As List(Of clsJobWorkDebitNote), ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = Nothing

        Try
            Dim DuplicateEntry As String = ""

            For Each obj As clsJobWorkDebitNote In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "AgainstDocument_No", obj.AgainstDocumentNo)
                clsCommon.AddColumnsForChange(coll, "AgainstDocument_Date", clsCommon.GetPrintDate(obj.DocumentDate, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "ItemDesc", obj.ItemName)
                clsCommon.AddColumnsForChange(coll, "ItemCode", obj.ItemCode)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SnfPer)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.rate)
                clsCommon.AddColumnsForChange(coll, "UnitCost", obj.NetAmt)
                clsCommon.AddColumnsForChange(coll, "Is_select", 1)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JobWork_Debit_Note_detail", OMInsertOrUpdate.Insert, "", trans)

            Next

            ' trans.Commit()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function LoadData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, Optional ByVal arrLoc As String = "") As clsJobWorkDebitNoteHead
        Return LoadData(strDocumentNo, NavType, arrLoc, Nothing)
    End Function

    Public Shared Function LoadData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal arrLoc As String, ByVal trans As SqlTransaction) As clsJobWorkDebitNoteHead
        Dim obj As clsJobWorkDebitNoteHead = Nothing
        Dim qry As String = "SELECT tspl_jobwork_debit_note_head.*,TSPL_LOCATION_MASTER.Location_Desc,TSPL_CUSTOMER_MASTER.Customer_Name " & _
                " FROM tspl_jobwork_debit_note_head " & _
" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_jobwork_debit_note_head.CustomerName " & _
" left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_jobwork_debit_note_head.Location  where 2=2 "

        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND tspl_jobwork_debit_note_head.Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        If clsCommon.myLen(arrLoc) > 0 Then
            whrClas = " and tspl_jobwork_debit_note_head.Location in (" + arrLoc + ") "
        End If

        Select Case NavType
            Case NavigatorType.First
                qry += " and tspl_jobwork_debit_note_head.Document_No = (select MIN(Document_No) from tspl_jobwork_debit_note_head where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and tspl_jobwork_debit_note_head.Document_No  = (select Max(Document_No) from tspl_jobwork_debit_note_head where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and tspl_jobwork_debit_note_head.Document_No  = (select Min(Document_No) from tspl_jobwork_debit_note_head where Document_No>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and tspl_jobwork_debit_note_head.Document_No = (select Max(Document_No) from tspl_jobwork_debit_note_head where Document_No<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and tspl_jobwork_debit_note_head.Document_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJobWorkDebitNoteHead()
            obj.DocumentNo = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Customer = clsCommon.myCstr(dt.Rows(0)("CustomerName"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            If dt.Rows(0)("FromDate") IsNot DBNull.Value Then
                obj.FromDate = clsCommon.myCDate(dt.Rows(0)("FromDate"))
            End If
            If dt.Rows(0)("ToDate") IsNot DBNull.Value Then
                obj.ToDate = clsCommon.myCDate(dt.Rows(0)("ToDate"))
            End If

            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.CustomerName = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))

        End If

        ''---------Detail table to record fetch
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            qry = "select * from TSPL_JOBWORK_DEBIT_NOTE_DETAIL where TSPL_JOBWORK_DEBIT_NOTE_DETAIL.Document_No='" + obj.DocumentNo + "' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsJobWorkDebitNote)
                Dim objTr As clsJobWorkDebitNote
                For Each dr As DataRow In dt.Rows
                    objTr = New clsJobWorkDebitNote
                    objTr.AgainstDocumentNo = clsCommon.myCstr(dr("AgainstDocument_No"))
                    objTr.DocumentDate = clsCommon.myCstr(dr("AgainstDocument_Date"))
                    objTr.ItemCode = clsCommon.myCstr(dr("ItemCode"))
                    objTr.ItemName = clsCommon.myCstr(dr("ItemDesc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.rate = clsCommon.myCdbl(dr("Rate"))
                    objTr.FatPer = clsCommon.myCdbl(dr("FatPer"))
                    objTr.SnfPer = clsCommon.myCdbl(dr("SNFPer"))
                    objTr.NetAmt = clsCommon.myCdbl(dr("UnitCost"))
                    objTr.IsSelect = clsCommon.myCBool(dr("Is_select"))

                    obj.Arr.Add(objTr)

                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, Optional ByVal arrLoc As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, arrLoc, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As New clsJobWorkDebitNoteHead()
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            obj = clsJobWorkDebitNote.LoadData(strDocNo, NavigatorType.Current, arrLoc, trans)

            Dim strPostby As String = objCommonVar.CurrentUserCode
            Dim qry As String = "Update TSPL_JOBWORK_DEBIT_NOTE_HEAD set Status=1, Posted_Date='" + strPostDate + "', Posted_By='" + strPostby + "' where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            ''--------- Auto AR Generte
            For Each objtr As clsJobWorkDebitNote In obj.Arr
                CreateARInvoice(obj, objtr, trans)
            Next

          
            ''----------End Script

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Sub CreateARInvoice(ByVal obj As clsJobWorkDebitNoteHead, ByVal objtr As clsJobWorkDebitNote, ByVal trans As SqlTransaction)
        Dim objCustInv As New clsCustomerInvoiceHead()
        objCustInv.Document_No = ""
        objCustInv.Document_Date = obj.Document_Date
        Dim dblAmt As Double = 0
        If objtr.Gain_Amount > 0 Then
            objCustInv.Document_Type = "D"
            'dblAmt = objtr.Gain_Amount
            objtr.Gain_Amount = objtr.NetAmt
            dblAmt = objtr.Gain_Amount
        Else
            objCustInv.Document_Type = "C"
            objtr.Loss_Amount = objtr.NetAmt
            dblAmt = objtr.Loss_Amount
        End If
        'objVendInv.Invoice_Type = "AP"
        objCustInv.loc_code = obj.Location
        objCustInv.Document_Total = dblAmt
        objCustInv.Customer_Code = obj.Customer
        objCustInv.Customer_Name = obj.CustomerName
        objCustInv.Posting_Date = obj.Document_Date


        Dim qry As String = " select TSPL_CUSTOMER_MASTER.Cust_Account,TSPL_CUSTOMER_MASTER.Terms_Code,TSPL_TERMS_MASTER.Terms_Desc,TSPL_TERMS_MASTER.No_Days from TSPL_CUSTOMER_MASTER left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code where TSPL_CUSTOMER_MASTER.Cust_Code = '" + obj.Customer + "'"
        Dim dtCutomer As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtCutomer Is Nothing OrElse dtCutomer.Rows.Count <= 0 Then
            Throw New Exception("Please set customer account of " + obj.CustomerName)
        End If
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)


        objCustInv.Description = "JW Debit Note - " + obj.DocumentNo + " And Document No - " + objtr.AgainstDocumentNo

        objCustInv.Account_Set = clsCommon.myCstr(dtCutomer.Rows(0)("Cust_Account"))
        objCustInv.Terms_Code = clsCommon.myCstr(dtCutomer.Rows(0)("Terms_Code"))
        objCustInv.Terms_Description = clsCommon.myCstr(dtCutomer.Rows(0)("Terms_Desc"))
        'objCustInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dtCutomer.Rows(0)("No_Days")))
        objCustInv.On_Hold = 0
        objCustInv.Remarks = ""
        'objCustInv.Description = obj.Description
        objCustInv.Balance_Amt = dblAmt
        objCustInv.RefDocType = "Job-Work debit Note"
        objCustInv.RefDocNo = obj.DocumentNo
        objCustInv.ConvRate = 1
        objCustInv.CURRENCY_CODE = objCommonVar.BaseCurrencyCode
        objCustInv.Discount_Base = dblAmt
        objCustInv.Amount_Less_Discount = dblAmt
        objCustInv.Document_Total = dblAmt

        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)

        '' Detail Saving
        ''----------------------------------------------
        Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
        Dim dtAC As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT, TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT FROM  TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account =TSPL_CUSTOMER_MASTER.Cust_Account  WHERE TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.Customer + "' ", trans)
        If dtAC Is Nothing OrElse dtAC.Rows.Count <= 0 Then
            Throw New Exception("Please set customer account set")
        End If
        Dim strAccount As String = ""

        If objtr.Gain_Amount > 0 Then
            If clsCommon.myLen(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT")) <= 0 Then
                Throw New Exception("Please set Exchage gain account for vendor" + obj.CustomerName)
            End If
            strAccount = clsCommon.myCstr(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))

        ElseIf objtr.Loss_Amount > 0 Then
            If clsCommon.myLen(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT")) <= 0 Then
                Throw New Exception("Please set Exchage Loss account for vendor" + obj.CustomerName)
            End If
            strAccount = clsCommon.myCstr(dtAC.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
        End If
        strAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strAccount, objCustInv.loc_code, True, trans)

        objCustInvTR.SNo = 1
        objCustInvTR.Amount = dblAmt
        objCustInvTR.Amount_less_Discount = dblAmt
        objCustInvTR.Total_Amount = dblAmt


        objCustInvTR.GL_Account_Code = strAccount
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Receivable_Control_acct  FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account  where TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.Customer + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
            objCustInv.Customer_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.Customer_Control_AC, objCustInv.loc_code, True, trans)
        End If
        If clsCommon.myLen(objCustInv.Customer_Control_AC) <= 0 Then
            Throw New Exception("Please set the customer control Account for " + objCustInv.Customer_Code)
        End If

        objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & strAccount & "'", trans))
        objCustInv.Arr.Add(objCustInvTR)

        '----------------------------------------------

        objCustInv.SaveData(objCustInv, True, trans, "")
        clsCustomerInvoiceHead.PostData("", objCustInv.Document_No, "", trans)
    End Sub
End Class
Public Class clsJobWorkDebitNoteHead
    Public DocumentNo As String = Nothing
    Public Document_Date As DateTime?
    Public Location As String = Nothing
    Public Customer As String = Nothing
    Public FromDate As DateTime?
    Public ToDate As DateTime?
    Public Arr As List(Of clsJobWorkDebitNote) = Nothing
    Public Status As String = Nothing
    Public CustomerName As String = Nothing
    Public LocationName As String = Nothing
    Public Trans_Type As String = Nothing




    Public Function SaveData(ByVal obj As clsJobWorkDebitNoteHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_JobWork_Debit_Note_Detail where Document_No='" + obj.DocumentNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If isNewEntry Then
                obj.DocumentNo = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.JobWkDebit, "", obj.Location)
            End If

            If (clsCommon.myLen(obj.DocumentNo) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "CustomerName", obj.Customer)
         
            clsCommon.AddColumnsForChange(coll, "FromDate", clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "ToDate", clsCommon.GetPrintDate(obj.ToDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Status", 0)

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))



            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JobWork_Debit_Note_Head", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JobWork_Debit_Note_Head", OMInsertOrUpdate.Update, "TSPL_JobWork_Debit_Note_Head.Document_No='" + obj.DocumentNo + "'", trans)
            End If
            isSaved = isSaved AndAlso clsJobWorkDebitNote.SaveDataDetail(obj.DocumentNo, obj.Arr, trans)


            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

End Class