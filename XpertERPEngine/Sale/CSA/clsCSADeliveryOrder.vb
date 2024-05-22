'=================BM00000003598===========created by Monika
Imports common
Imports System.Data.SqlClient
Public Class clsCSADeliveryOrder
#Region "variables"
    Public is_post As String = Nothing
    Public docno As String = Nothing
    Public docdate As String = Nothing
    Public cust_code As String = Nothing
    Public cust_name As String = Nothing
    Public frm_loc_code As String = Nothing
    Public frm_loc_name As String = Nothing
    Public to_loc_code As String = Nothing
    Public to_loc_name As String = Nothing
    Public state_code As String = Nothing
    Public state_name As String = Nothing
    Public trans_rate As Decimal = Nothing
    Public tax As String = Nothing
    Public doc_amt As String = Nothing
    Public csa_header_type As String = Nothing
    Public RT_UOM As String = Nothing
    Public isDOAmended As Boolean = False
    Public Arr As List(Of clsCSADeliveryOrderDetail) = Nothing

    Public CSA_Request_No As String = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Try
            Dim str As String = ""
            Dim qry As String = "select TSPL_CSA_DO_HEAD.doc_no as Code,TSPL_CSA_DO_HEAD.doc_date as [Doc Date],(case when TSPL_CSA_DO_HEAD.is_post='1' then'Approved' else 'Pending' end) as Status,TSPL_CSA_DO_HEAD.cust_code as [CSA Code],tspl_customer_master.customer_name as [CSA Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_CSA_DO_HEAD.from_location_code as [From Location],TSPL_CSA_DO_HEAD.to_location_code as [To Location],TSPL_CSA_DO_HEAD.state_code as [State Code],tspl_state_master.state_name as [State],TSPL_CSA_DO_HEAD.csa_rate as [Transfer Rate],TSPL_CSA_DO_HEAD.csa_type as [CSA Type],TSPL_CSA_DO_HEAD.including_tax as [Including Tax],TSPL_CSA_DO_HEAD.document_amount as [Order Amount] from TSPL_CSA_DO_HEAD "
            qry += "left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CSA_DO_HEAD.cust_code "
            qry += "left outer join tspl_state_master on tspl_state_master.state_code=TSPL_CSA_DO_HEAD.state_code "
            ''qry += " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO=TSPL_CSA_DO_HEAD.Doc_No"

            str = clsCommon.myCstr(clsCommon.ShowSelectForm("DOFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked))

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSADeliveryOrder, ByVal FromPostEvent As Boolean, ByVal isNewentry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewentry, FromPostEvent, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSADeliveryOrder, ByVal isNewentry As Boolean, ByVal FromPostEvent As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSADeliveryOrder, obj.frm_loc_code, clsCommon.myCDate(obj.docdate), trans)
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True
            Dim qry As String = ""
            If Not FromPostEvent Then
                qry = "delete from TSPL_TRANSACTION_APPROVAL where screen_name='CSA Delivery Order' and Program_Code='" + clsUserMgtCode.frmCSADeliveryOrder + "' and Document_No='" + obj.docno + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            '' done by Panch Raj against Ticket: BM00000007826
            If isNewentry Then
                obj.docno = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"), clsDocType.CSADELIVERYORDER, "", obj.frm_loc_code))
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.docno)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.cust_code)
            clsCommon.AddColumnsForChange(coll, "From_Location_Code", obj.frm_loc_code)
            clsCommon.AddColumnsForChange(coll, "To_Location_Code", obj.to_loc_code)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.state_code, True)
            clsCommon.AddColumnsForChange(coll, "CSA_Rate", obj.trans_rate)
            clsCommon.AddColumnsForChange(coll, "Including_Tax", obj.tax)
            clsCommon.AddColumnsForChange(coll, "csa_type", obj.csa_header_type)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.doc_amt)
            clsCommon.AddColumnsForChange(coll, "RT_UOM", obj.RT_UOM)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
            clsCommon.AddColumnsForChange(coll, "CSA_Request_No", obj.CSA_Request_No, True)

            If obj.isDOAmended Then
                qry = "select max(Amendment_No) from TSPL_CSA_DO_HEAD"
                Dim amndmentno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                If clsCommon.myLen(amndmentno) <= 0 Then
                    amndmentno = "CDO-AMD-000000001"
                Else
                    amndmentno = clsCommon.incval(amndmentno)
                End If

                clsCommon.AddColumnsForChange(coll, "Amendment_No", amndmentno, True)
            End If

            If isNewentry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_DO_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_DO_HEAD", OMInsertOrUpdate.Update, " doc_no='" + obj.docno + "'", trans)
            End If

            isSaved = isSaved AndAlso clsCSADeliveryOrderDetail.SaveData(obj.docno, obj.Arr, trans)

            If obj.isDOAmended Then
                isSaved = isSaved AndAlso clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.docno, "TSPL_CSA_DO_HEAD", "doc_no", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.docno, "TSPL_CSA_DO_DETAIL", "doc_no", trans)
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsCSADeliveryOrder
        Try
            Dim obj As New clsCSADeliveryOrder()
            obj.Arr = New List(Of clsCSADeliveryOrderDetail)

            Dim qry As String = "select TSPL_CSA_DO_HEAD.CSA_Request_No,TSPL_CSA_DO_HEAD.RT_UOM,TSPL_CSA_DO_HEAD.is_post,TSPL_CSA_DO_HEAD.doc_no,TSPL_CSA_DO_HEAD.doc_date,TSPL_CSA_DO_HEAD.cust_code,tspl_customer_master.customer_name,TSPL_CSA_DO_HEAD.from_location_code,tspl_location_master.location_desc,TSPL_CSA_DO_HEAD.to_location_code,location.location_desc as to_location_desc,TSPL_CSA_DO_HEAD.state_code,tspl_state_master.state_name,TSPL_CSA_DO_HEAD.csa_rate,TSPL_CSA_DO_HEAD.including_tax,TSPL_CSA_DO_HEAD.document_amount,TSPL_CSA_DO_HEAD.csa_type from TSPL_CSA_DO_HEAD "
            qry += "left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CSA_DO_HEAD.cust_code left outer join tspl_location_master on tspl_location_master.location_code=TSPL_CSA_DO_HEAD.from_location_code "
            qry += "left outer join tspl_state_master on tspl_state_master.state_code=TSPL_CSA_DO_HEAD.state_code left outer join tspl_location_master location on location.location_code=TSPL_CSA_DO_HEAD.to_location_code where 1=1 "
            If clsCommon.myLen(arrLoc) > 0 Then
                qry += " and TSPL_CSA_DO_HEAD.from_location_code in (" + arrLoc + ") "
            End If


            Select Case NavType
                Case NavigatorType.Current
                    qry += "and TSPL_CSA_DO_HEAD.doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += "and TSPL_CSA_DO_HEAD.doc_no in (select min(doc_no) from TSPL_CSA_DO_HEAD where from_location_code in (" + arrLoc + "))"
                Case NavigatorType.Last
                    qry += "and TSPL_CSA_DO_HEAD.doc_no in (select max(doc_no) from TSPL_CSA_DO_HEAD where from_location_code in (" + arrLoc + "))"
                Case NavigatorType.Next
                    qry += "and TSPL_CSA_DO_HEAD.doc_no in (select min(doc_no) from TSPL_CSA_DO_HEAD where doc_no>'" + strCode + "' and from_location_code in (" + arrLoc + "))"
                Case NavigatorType.Previous
                    qry += "and TSPL_CSA_DO_HEAD.doc_no in (select max(doc_no) from TSPL_CSA_DO_HEAD where doc_no<'" + strCode + "' and from_location_code in (" + arrLoc + "))"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.docno = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.docdate = clsCommon.myCDate(dt.Rows(0)("doc_date"))
                obj.cust_code = clsCommon.myCstr(dt.Rows(0)("cust_code"))
                obj.cust_name = clsCommon.myCstr(dt.Rows(0)("customer_name"))
                obj.frm_loc_code = clsCommon.myCstr(dt.Rows(0)("from_location_code"))
                obj.frm_loc_name = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.to_loc_code = clsCommon.myCstr(dt.Rows(0)("to_location_code"))
                obj.to_loc_name = clsCommon.myCstr(dt.Rows(0)("to_location_desc"))
                obj.state_code = clsCommon.myCstr(dt.Rows(0)("state_code"))
                obj.state_name = clsCommon.myCstr(dt.Rows(0)("state_name"))
                obj.trans_rate = clsCommon.myCdbl(dt.Rows(0)("csa_rate"))
                obj.tax = clsCommon.myCstr(dt.Rows(0)("including_tax"))
                obj.csa_header_type = clsCommon.myCstr(dt.Rows(0)("csa_type"))
                obj.doc_amt = clsCommon.myCdbl(dt.Rows(0)("document_amount"))
                obj.is_post = clsCommon.myCstr(dt.Rows(0)("is_post"))
                obj.RT_UOM = clsCommon.myCstr(dt.Rows(0)("RT_UOM"))
                obj.CSA_Request_No = clsCommon.myCstr(dt.Rows(0)("CSA_Request_No"))

                qry = "select TSPL_CSA_DO_DETAIL.*,tspl_item_master.item_desc,tspl_item_master.csa_type from TSPL_CSA_DO_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_DO_DETAIL.item_code where TSPL_CSA_DO_DETAIL.doc_no='" + obj.docno + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsCSADeliveryOrderDetail()

                        objtr.Doc_No = clsCommon.myCstr(dr("doc_no"))
                        objtr.lineno = CInt(dr("line_no"))
                        objtr.icode = clsCommon.myCstr(dr("item_code"))
                        objtr.iname = clsCommon.myCstr(dr("item_desc"))
                        objtr.uom = clsCommon.myCstr(dr("uom"))
                        objtr.csa_type = clsCommon.myCstr(dr("csa_type"))
                        objtr.qty = clsCommon.myCdbl(dr("qty"))
                        objtr.unit_rate = clsCommon.myCdbl(dr("unit_rate"))
                        objtr.tax = clsCommon.myCstr(dr("including_tax"))
                        objtr.toltalamt = clsCommon.myCdbl(dr("total_amt"))
                        objtr.remarks = clsCommon.myCstr(dr("remarks"))
                        objtr.CSA_Request_No = clsCommon.myCstr(dr("CSA_Request_No"))
                        objtr.Pending_Qty = clsCommon.myCdbl(dr("bal_qty"))

                        ''==============scheme columns=====================================================
                        objtr.Cash_Scheme_Amount = clsCommon.myCdbl(dr("Cash_Scheme_Amount"))
                        objtr.Cash_Scheme_Code = clsCommon.myCstr(dr("Cash_Scheme_Code"))
                        objtr.Cash_Scheme_Pers = clsCommon.myCdbl(dr("Cash_Scheme_Pers"))
                        objtr.Cash_Scheme_Type = clsCommon.myCstr(dr("Cash_Scheme_Type"))
                        objtr.Scheme_Applicable = clsCommon.myCBool(dr("Scheme_Applicable"))
                        objtr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                        objtr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme_Item_Code"))
                        objtr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme_Item_UOM"))
                        objtr.Scheme_Qty = clsCommon.myCdbl(dr("Scheme_Qty"))
                        objtr.Scheme_Type = clsCommon.myCstr(dr("Scheme_Type"))
                        objtr.FOC = clsCommon.myCBool(dr("FOC"))
                        ''============end here======================================================

                        obj.Arr.Add(objtr)
                    Next
                End If
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select From_Location_Code,Doc_Date from TSPL_CSA_DO_HEAD where doc_no='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSADeliveryOrder, clsCommon.myCstr(dt.Rows()("From_Location_Code")), clsCommon.myCDate(dt.Rows()("Doc_Date")), trans)

            End If
            Dim qry As String = "delete from TSPL_CSA_DO_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TRANSACTION_APPROVAL where screen_name='CSA Delivery Order' and Program_Code='" + clsUserMgtCode.frmCSADeliveryOrder + "' and Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CSA_DO_HEAD where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select From_Location_Code,Doc_Date from TSPL_CSA_DO_HEAD where doc_no='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSADeliveryOrder, clsCommon.myCstr(dt.Rows()("From_Location_Code")), clsCommon.myCDate(dt.Rows()("Doc_Date")), trans)

            End If
            Dim qry As String = "update TSPL_CSA_DO_HEAD set is_post='1',modified_by='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")) + "' where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function IsValidCustomerForDOItem(ByVal strDONo As String, ByVal strICode As String, ByVal strCustCode As String) As Boolean
        Dim qry As String = "select 1 from TSPL_CSA_DO_DETAIL left outer join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.doc_no=TSPL_CSA_DO_DETAIL.doc_no where TSPL_CSA_DO_HEAD.doc_no ='" + strDONo + "' and Item_Code='" + strICode + "' and TSPL_CSA_DO_HEAD.Cust_Code not in ('','" + strCustCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function GetBalanceDOQtyByTransfer(ByVal strDOCode As String, ByVal strICode As String, ByVal strCurrTransferNo As String, ByVal strUOM As String) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_CSA_DO_DETAIL.Item_Code as ICode,TSPL_CSA_DO_DETAIL.Qty as Qty,1 as RI from TSPL_CSA_DO_DETAIL left outer join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.doc_no=TSPL_CSA_DO_DETAIL.doc_no where TSPL_CSA_DO_HEAD.is_post=1 and TSPL_CSA_DO_DETAIL.doc_no ='" + strDOCode + "' and TSPL_CSA_DO_DETAIL.Item_Code='" + strICode + "' and  TSPL_CSA_DO_DETAIL.uom='" + strUOM + "' " & _
            " union all " & _
         " select  TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,TSPL_CSA_TRANSFER_DETAIL.Qty,-1 as RI from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code where TSPL_CSA_TRANSFER_DETAIL.DELEVERY_ORDER_NO='" + strDOCode + "' and TSPL_CSA_TRANSFER_DETAIL.Item_Code='" + strICode + "' and TSPL_CSA_TRANSFER_DETAIL.Unit_code='" + strUOM + "' and TSPL_CSA_TRANSFER_DETAIL.doc_code not in ('" + strCurrTransferNo + "')  "
        qry += " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function


#Region "Credit Limit"
    Public Shared Function CustomerOutstandingAmount(ByVal strlocation As String, ByVal strCustomer As String, ByVal dblTotal As Double, ByVal Trans As SqlTransaction, ByVal strDoc As String, ByVal strDocDate As DateTime) As Double
        Dim qry As String = String.Empty
        Try
            Dim dblOutstandingAmt As Double = 0
            Dim dblCreditLimit As Double = 0
            Dim dblSecurityAmount As Double = 0
            Dim dblPendingDeliveryAmt As Double = 0
            Dim dblPendingSalePattiAmt As Double = 0
            Dim dblCSATransferAmount As Double = 0
            Dim dblAmt As Double = 0

            ''R-Receipt, P-Advance, A-Applied Document, M-Misc Receipt, O-On Account, U-Unapplied, F-Refund, S=Misc Fund
            qry = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
            "select SUM(isnull(TSPL_CSA_DO_HEAD.document_amount,0) ) as OutStandingAmt , 1 as RI from TSPL_CSA_DO_HEAD " & _
            "where coalesce(TSPL_CSA_DO_HEAD.is_post,0)=1 and TSPL_CSA_DO_HEAD.Cust_Code='" & strCustomer & "'  and TSPL_CSA_DO_HEAD.Doc_No not in ('" & strDoc & "')  " & _
            " union all " & _
            "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
           "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
           "where TSPL_Customer_Invoice_Head.trans_type='CSA' and TSPL_RECEIPT_HEADER.Posted='Y' and TSPL_RECEIPT_HEADER.Receipt_Type not in ('O','A','F','P')  and TSPL_Customer_Invoice_Head.Against_Sale_No <> ''  and TSPL_Customer_Invoice_Head.Customer_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='A' and Cust_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
            " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
           " union all " & _
           "select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
           " union all " & _
           "select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'", Trans))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDepositType  in ('S')  and Posted='Y' and Cust_Code='" & strCustomer & "'", Trans))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(document_amount) from TSPL_CSA_DO_HEAD where  coalesce(TSPL_CSA_DO_HEAD.is_post,0)=0 and TSPL_CSA_DO_HEAD.Doc_No <> '" & strDoc & "' and Cust_Code='" & strCustomer & "'", Trans))
            dblPendingSalePattiAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(total_amt) from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.document_code <> '" & strDoc & "' and customer_code='" & strCustomer & "'", Trans)) 'and coalesce(tspl_sd_sale_invoice_head.[Status],0)=0
            'dblPendingSalePattiAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select max(TSPL_CSA_TRANSFER_HEAD.Document_Amount) - sum(case when Conv_Factor<=1 then (Alt_Qty * transfer_rate) else ((qty * transfer_rate) /Conv_Factor) end) as amount from TSPL_CSA_SALE_TRANSFER_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_CSA_SALE_TRANSFER_DETAIL.DOCUMENT_CODE left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_SALE_TRANSFER_DETAIL.Against_Transfer_Code where tspl_sd_sale_invoice_head.trans_type='CSA' and customer_code='" & strCustomer & "'", Trans))
            dblCSATransferAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(total_amt) from tspl_sd_sale_return_head where tspl_sd_sale_return_head.trans_type='CSA' and tspl_sd_sale_return_head.document_code <> '" & strDoc & "' and customer_code='" & strCustomer & "'  ", Trans)) ''and coalesce(tspl_sd_sale_return_head.[Status],0)=0

            dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt + dblPendingSalePattiAmt + dblCSATransferAmount
            Dim dblCustOutstandingAmt As Double = dblAmt

            Dim dblNewCredtitLimit As Double = Nothing
            If dblAmt < dblTotal Then
                dblNewCredtitLimit = dblAmt - dblTotal
                If Not clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.frmCSADeliveryOrder, Trans) Then
                    ''=send data for approval-----------------------------------------------
                    Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_TRANSACTION_APPROVAL where screen_name='CSA Delivery Order' and program_code='" + clsUserMgtCode.frmCSADeliveryOrder + "' and document_no='" + clsCommon.myCstr(strDoc) + "' "))
                    If check <= 0 Then
                        qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code,cust_code,loc_code) " & _
                    "values ('CSA Delivery Order','" & clsUserMgtCode.frmCSADeliveryOrder & "','" & strDoc & "','" & clsCommon.GetPrintDate(strDocDate, "dd/MMM/yyyy") & "','Rate',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "','" + strCustomer + "','" + strlocation + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                    End If
                    ''=============================================
                End If
                Return dblNewCredtitLimit
                'Return False
            End If

            Return 1
            'Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Function
#End Region
End Class

Public Class clsCSADeliveryOrderDetail

#Region "variables"
    Public Doc_No As String = Nothing
    Public lineno As Integer = Nothing
    Public icode As String = Nothing
    Public iname As String = Nothing
    Public uom As String = Nothing
    Public csa_type As String = Nothing
    Public qty As Decimal = Nothing
    Public unit_rate As Decimal = Nothing
    Public tax As String = Nothing
    Public toltalamt As Decimal = Nothing
    Public remarks As String = Nothing
    Public mrp As Decimal = Nothing
    Public trans_rate As Decimal = Nothing
    Public Balance_Qty As Decimal = Nothing

    Public CSA_Request_No As String = Nothing
    Public Pending_Qty As Decimal = Nothing

    Public Scheme_Applicable As Boolean = Nothing
    Public FOC As Boolean = False
    Public Scheme_Code As String = Nothing
    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Decimal = Nothing
    Public Scheme_Item_UOM As String = Nothing
    Public Cash_Scheme_Code As String = Nothing
    Public Cash_Scheme_Type As String = Nothing
    Public Cash_Scheme_Pers As Decimal = Nothing
    Public Cash_Scheme_Amount As Decimal = Nothing
#End Region

    Public Shared Function GetBalanceRequestQty(ByVal strDocNo As String, ByVal strDocDate As DateTime, ByVal Request_No As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = "select sum(isnull(TSPL_CSA_BOOKING_DETAIL.BOOK_QTY,0))-sum(isnull(CSADO.qty,0)) as bal_qty from TSPL_CSA_BOOKING_DETAIL left outer join TSPL_CSA_BOOKING_HEAD on TSPL_CSA_BOOKING_HEAD.BOOKING_NO=TSPL_CSA_BOOKING_DETAIL.BOOKING_NO left outer join " & _
                            " (select TSPL_CSA_DO_DETAIL.CSA_Request_No,TSPL_CSA_DO_DETAIL.Item_Code,sum(isnull(TSPL_CSA_DO_DETAIL.Qty,0)) as qty from TSPL_CSA_DO_DETAIL where TSPL_CSA_DO_DETAIL.Doc_No<>'" + strDocNo + "' group by TSPL_CSA_DO_DETAIL.CSA_Request_No,TSPL_CSA_DO_DETAIL.Item_Code)CSADO " & _
                            " on CSADO.CSA_Request_No=TSPL_CSA_BOOKING_DETAIL.BOOKING_NO and CSADO.Item_Code=TSPL_CSA_BOOKING_DETAIL.Item_Code where TSPL_CSA_BOOKING_HEAD.Trans_Type='Request' and TSPL_CSA_BOOKING_HEAD.BOOKING_NO='" + Request_No + "' and TSPL_CSA_BOOKING_DETAIL.Item_Code='" + Item_Code + "' " & _
                            " and TSPL_CSA_BOOKING_HEAD.posted=1 "
        Dim qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        Return qty
    End Function

    Public Shared Function IsValidCustomerFor_CSARequestItem(ByVal strRequestNo As String, ByVal strICode As String, ByVal strCustCode As String) As Boolean
        Dim qry As String = "select 1 from TSPL_CSA_booking_DETAIL left outer join TSPL_CSA_booking_HEAD on TSPL_CSA_booking_HEAD.booking_no=TSPL_CSA_booking_DETAIL.booking_no where TSPL_CSA_booking_HEAD.booking_no ='" + strRequestNo + "' and TSPL_CSA_booking_detail.Item_Code='" + strICode + "' and TSPL_CSA_booking_HEAD.Csa_Code not in ('','" + strCustCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsCSADeliveryOrderDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            Dim qry As String = "delete from TSPL_CSA_DO_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsCSADeliveryOrderDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_no", objtr.lineno)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.icode)
                    clsCommon.AddColumnsForChange(coll, "UOM", objtr.uom)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "Unit_Rate", objtr.unit_rate)
                    clsCommon.AddColumnsForChange(coll, "Including_Tax", objtr.tax)
                    clsCommon.AddColumnsForChange(coll, "Total_Amt", objtr.toltalamt)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)
                    clsCommon.AddColumnsForChange(coll, "CSA_Request_No", objtr.CSA_Request_No, True)
                    clsCommon.AddColumnsForChange(coll, "bal_qty", objtr.Pending_Qty)

                    ''=================scheme column===========================================
                    clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", objtr.Scheme_Applicable)
                    clsCommon.AddColumnsForChange(coll, "FOC", objtr.FOC)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Code", objtr.Scheme_Code)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Type", objtr.Scheme_Type)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", objtr.Scheme_Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Qty", objtr.Scheme_Qty)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", objtr.Scheme_Item_UOM)
                    clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Code", objtr.Cash_Scheme_Code)
                    clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Type", objtr.Cash_Scheme_Type)
                    clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Pers", objtr.Cash_Scheme_Pers)
                    clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Amount", objtr.Cash_Scheme_Amount)
                    ''=================end here========================================

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_DO_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class