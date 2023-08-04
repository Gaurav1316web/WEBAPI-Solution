Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports Telerik.WinControls
Imports System.Windows.Forms
Imports Telerik.WinControls.UI
Imports System.Drawing
Imports System.Net
Imports Newtonsoft.Json

Public Class Xtra

    '---- Created By Richa Agarwal-----Ticket no. BM00000003242 on 29/07/2014
    Public Shared Function CustomerPermission() As String
        Dim qry As String = ""
        Dim strvalue As String = ""
        qry = "select distinct Cust_Code from TSPL_CUSTOMER_MAPPING where User_Code ='" + objCommonVar.CurrentUserCode + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
            For Each dr As DataRow In dtNew.Rows
                strvalue = strvalue & "'" & clsCommon.myCstr(dr("Cust_Code")).Replace("'", "''").ToString() & "'" & ","
            Next

            If strvalue <> "" Then
                strvalue = strvalue.Substring(0, strvalue.Length - 1)
            End If

        End If
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strvalue
    End Function
    Public Shared Function CustomerCategory() As String
        Return CustomerCategory(Nothing)
    End Function
    Public Shared Function CustomerCategory(ByVal trans As SqlTransaction) As String
        Dim qry As String = ""
        Dim strvalue As String = ""
        qry = "select distinct Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where User_Code ='" + objCommonVar.CurrentUserCode + "'"
        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
            For Each dr As DataRow In dtNew.Rows
                strvalue = strvalue & "'" & clsCommon.myCstr(dr("Customer_Category")).Replace("'", "''").ToString() & "'" & ","
            Next

            If strvalue <> "" Then
                strvalue = strvalue.Substring(0, strvalue.Length - 1)
            End If

        End If
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strvalue
    End Function


    Public Shared Function UpdateBalanceQtyAndBalanceQtyInBottleOFTransfer(ByVal strTransferNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = " update TSPL_TRANSFER_DETAIL set Pending_Qty=xxxxx.BalanceQty,Pending_Balance_In_Bottle=xxxxx.balanceInBottel"
        qry += "  from("
        qry += "  select xxxx.*,(xxxx.BalanceQty*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=xxxx.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB')) as balanceInBottel from ("
        qry += "  select  Transfer_No,max(Line_No) as Line_No,Item_Code,sum(Item_Qty * case when RI in (1,5) then 1 else case when RI in (2,3,4) then -1 else 0 end end) as BalanceQty,MRP from ("
        qry += "  select TSPL_TRANSFER_DETAIL.Transfer_No,TSPL_TRANSFER_DETAIL.Line_No,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,TSPL_TRANSFER_DETAIL.MRP,TSPL_TRANSFER_DETAIL.Item_Qty,1 as RI,1 as chk from TSPL_TRANSFER_DETAIL "
        qry += "  left outer join  TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No where TSPL_TRANSFER_HEAD.Transfer_Type='LO' and TSPL_TRANSFER_HEAD.Post='Y'"

        If clsCommon.myLen(strTransferNo) > 0 Then
            qry += " and TSPL_TRANSFER_DETAIL.Transfer_No='" + strTransferNo + "'"
        End If

        qry += "  union all"
        qry += "  select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,0 as Line_No,TSPL_SHIPMENT_DETAILS.Item_Code,TSPL_SHIPMENT_DETAILS.Price_Date,MRP_Amt*Conversion_Factor as MRP,TSPL_SHIPMENT_DETAILS.Shipped_Qty /Conversion_Factor as Item_Qty ,2 as RI,0 as chk"
        qry += "  from TSPL_SHIPMENT_DETAILS "
        qry += "  left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No"
        qry += "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SHIPMENT_DETAILS.Unit_code"
        qry += "  where TSPL_SHIPMENT_MASTER.Is_Post='Y' and LEN( ISNULL(TSPL_SHIPMENT_MASTER.Transfer_No,''))>0"
        qry += "  union all"
        qry += "  select TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,0 as Line_No,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,MRP*Conversion_Factor as MRP,(ISNULL( TSPL_TRANSFER_DETAIL.Burst,0)+isnull(TSPL_TRANSFER_DETAIL.Leak,0)+isnull(TSPL_TRANSFER_DETAIL.Shortage,0)+TSPL_TRANSFER_DETAIL.LoadIn_Qty) /Conversion_Factor  as Item_Qty  ,4 as RI,0 as chk"
        qry += "  from TSPL_TRANSFER_DETAIL "
        qry += "  left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No"
        qry += "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom where  Transfer_Type='LI' and len(ISNULL(TSPL_TRANSFER_HEAD.Load_Out_No,''))>0 and TSPL_TRANSFER_HEAD.Post='Y'"
        qry += "  union all"
        qry += "  select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,0 as Line_No,TSPL_SALE_RETURN_DETAIL.Item_Code,TSPL_SALE_RETURN_DETAIL.Price_Date,MRP_Amt*Conversion_Factor as MRP,  TSPL_SALE_RETURN_DETAIL.Return_Qty/Conversion_Factor  as Item_Qty  ,5 as RI,0 as chk from TSPL_SALE_RETURN_DETAIL left outer join TSPL_SALE_RETURN_HEAD on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_RETURN_HEAD.Invoice_No left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_RETURN_DETAIL.Unit_code where  TSPL_SHIPMENT_MASTER.Shipment_Type='Transfer' and LEN(ISNULL(TSPL_SHIPMENT_MASTER.Transfer_No,''))>0 and ISNULL( TSPL_SALE_RETURN_HEAD.Is_Post,'')='Y'"
        qry += "  ) xxx group by Transfer_No,Item_Code,MRP having SUM(chk)>0   "
        qry += "  )xxxx"
        qry += "  )xxxxx "
        qry += "  inner join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_DETAIL.Transfer_No=xxxxx.Transfer_No and TSPL_TRANSFER_DETAIL.Line_No=xxxxx.Line_No"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Return True
    End Function

    Public Shared Function UpdateSaleInvoiceBalanceAmt() As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            UpdateSaleInvoiceBalanceAmt(trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateSaleInvoiceBalanceAmt(ByVal trans As SqlTransaction) As Boolean

        Try
            Dim qry As String = "update  TSPL_SALE_INVOICE_HEAD set Balance_Amt=xxxx.Amt"
            qry += " from("
            qry += " select Code,sum(Amt* RI ) as Amt from ("
            qry += " select Sale_Invoice_No as Code, Empty_Value+Total_Invoice_Amt as Amt,1 as RI,1 as Chk   from TSPL_SALE_INVOICE_HEAD where Is_Post='Y'"
            qry += " union all "
            qry += " select TSPL_RECEIPT_DETAIL.Document_No as Code,Applied_Amount as Amt,-1 as RI,0 as chk  from TSPL_RECEIPT_DETAIL"
            qry += " left outer join  TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No"
            qry += " where TSPL_RECEIPT_HEADER.Posted='Y'  and LEN(ISNULL(TSPL_RECEIPT_DETAIL.Document_No,''))>0"
            qry += " union all "
            qry += " select TSPL_Receipt_Adjustment_Header.Doc_No as Code,TSPL_Receipt_Adjustment_Detail.Amount as Amt,-1 as RI,0 as Chk from TSPL_Receipt_Adjustment_Detail "
            qry += " left outer join TSPL_Receipt_Adjustment_Header on TSPL_Receipt_Adjustment_Header.Adjustment_No=TSPL_Receipt_Adjustment_Detail.Adjustment_No"
            qry += " where TSPL_Receipt_Adjustment_Header.Is_Post='Y' and LEN(ISNULL(TSPL_Receipt_Adjustment_Header.Doc_No,''))>0"
            qry += " union all "
            qry += " select TSPL_ADJUSTMENT_HEADER.Document_No as Code,TSPL_ADJUSTMENT_DETAIL.Item_Cost as Amt,case when Trans_Type='Out' then 1 else -1 end as RI,0 as Chk from TSPL_ADJUSTMENT_DETAIL"
            qry += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No"
            qry += " where TSPL_ADJUSTMENT_HEADER.Posted='Y' and TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' and TSPL_ADJUSTMENT_HEADER.ItemType='E' "
            qry += " and LEN(ISNULL(TSPL_ADJUSTMENT_HEADER.Document_No,''))>0"
            qry += "  union all "
            qry += " select Invoice_No as Code, Empty_Value+Total_Invoice_Amt as Amt,-1 as RI,0 as Chk   from TSPL_SALE_RETURN_HEAD where Is_Post='Y'"
            qry += " union all "
            qry += " select TSPL_RECEIPT_DETAIL.Document_No,TSPL_RECEIPT_DETAIL.Applied_Amount as Amt,1 as RI,0 as chk from TSPL_BANK_REVERSE"
            qry += " inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Bank_Code=TSPL_BANK_REVERSE.Bank_Code and TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No and TSPL_RECEIPT_HEADER.Cust_Code=TSPL_BANK_REVERSE.Cust_Code"
            qry += " left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No "
            qry += " where TSPL_BANK_REVERSE.Source_Type='AR' and Reverse_Document='Receipts'  and LEN(ISNULL(TSPL_RECEIPT_DETAIL.Document_No,''))>0 and TSPL_BANK_REVERSE.Post='P'"
            qry += " ) xxx"
            qry += " group by Code having SUM(chk)>0"
            qry += " )xxxx"
            qry += " inner join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = xxxx.Code"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateAPInvoiceBalanceAmount(ByVal strAPDocumentNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt=xxxx.Amt" + Environment.NewLine
        qry += " from(" + Environment.NewLine
        qry += " select Document_No,SUM(Amt*RI) as Amt from (" + Environment.NewLine
        qry += " select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Total-TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as Amt,1 as RI,1 as Chk from TSPL_VENDOR_INVOICE_HEAD where Document_Type= 'I'"
        If clsCommon.myLen(strAPDocumentNo) > 0 Then
            qry += " and TSPL_VENDOR_INVOICE_HEAD.Document_No='" + strAPDocumentNo + "'"
        End If
        qry += Environment.NewLine + " union all" + Environment.NewLine
        qry += " select PIVendorInvooiceHead.Document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Total-TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as Amt,-1 as RI,0 as Chk " + Environment.NewLine
        qry += " from TSPL_PR_HEAD" + Environment.NewLine
        qry += " left outer join TSPL_PR_DETAIL on TSPL_PR_DETAIL.PR_No=TSPL_PR_HEAD.PR_No and TSPL_PR_DETAIL.Line_No=1" + Environment.NewLine
        qry += " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No=TSPL_PR_HEAD.PR_No" + Environment.NewLine
        qry += " left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PR_DETAIL.PI_Id" + Environment.NewLine
        qry += " left outer join TSPL_VENDOR_INVOICE_HEAD as PIVendorInvooiceHead on PIVendorInvooiceHead.Against_POInvoice_No=TSPL_PI_HEAD.PI_No" + Environment.NewLine
        qry += " where TSPL_VENDOR_INVOICE_HEAD.Document_Type= 'D'"
        If clsCommon.myLen(strAPDocumentNo) > 0 Then
            qry += " and PIVendorInvooiceHead.Document_No='" + strAPDocumentNo + "'"
        End If
        qry += " union all" + Environment.NewLine
        qry += " select APVendorInvoiceNo.Document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Total-TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as Amt,(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type ='C' then 1 else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then -1 else 0 end end) as RI,0 as Chk " + Environment.NewLine
        qry += " from TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine
        qry += " left outer join TSPL_VENDOR_INVOICE_HEAD as APVendorInvoiceNo on APVendorInvoiceNo.Document_No=TSPL_VENDOR_INVOICE_HEAD.RefDocNo" + Environment.NewLine
        qry += " where  TSPL_VENDOR_INVOICE_HEAD.RefDocType='AP'"
        If clsCommon.myLen(strAPDocumentNo) > 0 Then
            qry += "  and APVendorInvoiceNo.Document_No='" + strAPDocumentNo + "'"
        End If
        qry += Environment.NewLine + " union all" + Environment.NewLine
        qry += " select TSPL_PAYMENT_DETAIL.Document_No,TSPL_PAYMENT_DETAIL.Applied_Amount as Amt,-1 as RI ,0 as Chk" + Environment.NewLine
        qry += " from TSPL_PAYMENT_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No= TSPL_PAYMENT_DETAIL.Payment_No" + Environment.NewLine
        qry += " where  TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV') and TSPL_PAYMENT_HEADER.Posted=1 "
        If clsCommon.myLen(strAPDocumentNo) > 0 Then
            qry += " and TSPL_PAYMENT_DETAIL.Document_No='" + strAPDocumentNo + "'"
        End If
        qry += Environment.NewLine + " ) xxx" + Environment.NewLine
        qry += " group by Document_No having sum(chk)>0" + Environment.NewLine
        qry += " )xxxx" + Environment.NewLine
        qry += " inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=xxxx.Document_No" + Environment.NewLine
        Return clsDBFuncationality.ExecuteNonQuery(qry, trans)
    End Function

    Public Shared Function GetCapexCombo() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Capex"
        dr("Name") = "Capex"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Regular"
        dr("Name") = "Regular"
        dt.Rows.Add(dr)
        Return dt
    End Function

    'Public Shared Function MyDivide(ByVal MainValue As Decimal, ByVal divideValue As Decimal) As Decimal
    '    Dim retValue As Decimal = 0
    '    Try
    '        retValue = MainValue / divideValue
    '    Catch ex As Exception
    '    End Try
    '    Return retValue
    'End Function

    'Public Shared Function MyRoundOFF(ByVal Value As Decimal, ByVal DecimalPlaces As Integer, ByVal IncreaseAfter As Integer) As Decimal
    '    Dim retValue As Decimal = 0
    '    Try
    '        Dim DecimalPlacesValue As Integer = 1
    '        For index As Integer = 1 To DecimalPlaces
    '            DecimalPlacesValue = DecimalPlacesValue * 10
    '        Next
    '        Dim RFValue As Decimal = ((Value * DecimalPlacesValue) - Math.Truncate(Value * DecimalPlacesValue)) * 10
    '        RFValue = Math.Truncate(RFValue)
    '        Dim dclIntPart As Decimal = Math.Truncate(Value * DecimalPlacesValue)
    '        If RFValue > IncreaseAfter Then
    '            dclIntPart = dclIntPart + 1
    '        End If
    '        retValue = dclIntPart / DecimalPlacesValue
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return retValue
    'End Function

    Public Shared Function MyNoDecimalToDecimal(ByVal Value As Object) As Decimal
        Dim retValue As Decimal = 0
        Try
            Dim x As Integer = clsCommon.myCDecimal(Value)
            Dim str As String = clsCommon.myCstr(x).Replace(".", "")
            If clsCommon.myLen(str) > 1 Then
                str = str.Insert(1, ".")
            End If
            retValue = clsCommon.myCDecimal(str)
        Catch ex As Exception
        End Try
        Return retValue
    End Function
    Public Shared Function GetDataFromAPI(ByVal API As EnumAPI, ByVal APIName As String, ByVal ArrFilter As Dictionary(Of String, String)) As DataTable
        Dim dt As DataTable = Nothing
        Try
            If clsCommon.myInternetWork() Then
                Dim baseAddress As String = ""
                Dim reqparm As New System.Collections.Specialized.NameValueCollection
                Select Case API
                    Case EnumAPI.BankIFSC
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                            ''Live
                            baseAddress = clsCommon.myCstr("http://172.21.80.251:8093/MilkProcurement.asmx" & "/" & APIName)

                            ''Local
                            'baseAddress = clsCommon.myCstr("http://103.122.38.34:8093/MilkProcurement.asmx" & "/" & APIName)
                        Else
                            baseAddress = clsCommon.myCstr("http://103.47.149.49:8093/MilkProcurement.asmx" & "/" & APIName)
                        End If
                        reqparm.Add("Key", "Tecxpert@MP#123$456%789^")
                End Select

                Dim c As WebClient = New WebClient()
                If ArrFilter IsNot Nothing AndAlso ArrFilter.Count > 0 Then
                    For Each key As String In ArrFilter.Keys
                        reqparm.Add(key, ArrFilter(key))
                    Next
                End If
                Dim responsebytes = c.UploadValues(baseAddress, "POST", reqparm)
                Dim responsebody = (New System.Text.UTF8Encoding()).GetString(responsebytes)
                Dim jArray = Newtonsoft.Json.Linq.JArray.Parse(responsebody)
                dt = JsonConvert.DeserializeObject(Of DataTable)(jArray.ToString())
                Dim a As Integer = 0
            End If
        Catch ex As Exception
            Throw New Exception("API Error " + Environment.NewLine + ex.Message)
        End Try
        Return dt
    End Function
End Class

Public Class clsEmailSMSConstants
    '----------------complaint detail entry-------------
    Public Const Complnt_code As String = "$comp_id$"
    Public Const Assetcode As String = "$item_code$"
    Public Const outlet As String = "$cust_code$"
    Public Const complnt_date As String = "$comp_date$"
    Public Const SerivceDealer As String = "$Executive_Code$"
    '---------------------------------------------------


    '----------------HR EM Resignation Letter-------------
    Public Const doccode As String = "$doccode$"
    Public Const docdate As String = "$docdate$"
    Public Const EmpCode As String = "$EmpCode$"
    Public Const EmpName As String = "$EmpName$"
    Public Const DepCode As String = "$DepCode$"
    Public Const DepName As String = "$DepName$"
    Public Const ResonOfResignation As String = "$ResonOfResignation$"
    Public Const ResignationDate As String = "$ResignationDate$"
    Public Const Remarks As String = "$Remarks$"
    Public Const HandoverCode As String = "$HandoverCode$"
    Public Const HandoverName As String = "$HandoverName$"
    '---------------------------------------------------

    '----------------Sale Order------------------------
    Public Const SaleOrderNo As String = "$DocNo$"
    Public Const SaleOrderDate As String = "$DocDate$"
    Public Const VendorNo As String = "$VendorNo$"
    Public Const VendorName As String = "$VendorName$"
    Public Const ContactPerson As String = "$ContactPerson$"
    Public Const TotalAmount As String = "$TotalAmount$"
    Public Const RoundOffAmount As String = "$RoundOffAmount$"
    '------------------------------------------------------

    '----------------Delivery Note Fresh Sale------------------------
    Public Const DeliveryNo As String = "$DocNo$"
    Public Const DeliveryDate As String = "$DocDate$"
    Public Const LocationCode As String = "$LocationCode$"
    Public Const LocationName As String = "$LocationName$"
    Public Const BookingNo As String = "$BookingNo$"
    '------------------------------------------------------

    Public Const CustomerNo As String = "$CustomerNo$"
    Public Const CustomerName As String = "$CustomerName$"
    Public Const InvoiceNo As String = "$Purchase InvoiceNo$"

    '---------------Sale register------------------
    Public Const FromDate As String = "$From Date$"
    Public Const ToDate As String = "$To Date$"
    Public Const ReportType As String = "$Summary Or Detail$"
    Public Const InvoiceType As String = "$Invoice Type$"
    '----------------Purchase Requistion------------------------
    Public Const PurchaseRequisitionNo As String = "$PurchaseRequisitionNo$"
    Public Const PurchaseRequisitionDate As String = "$PurchaseRequisitionDate$"

    'Public Const VendorNo As String = "$VendorNo$"
    'Public Const VendorName As String = "$VendorName$"
    'Public Const ContactPerson As String = "$ContactPerson$"
    'Public Const TotalAmount As String = "$TotalAmount$"
    '------------------------------------------------------
    '---------------Quality Check------------------
    Public Const QcNo As String = "$QC No$"
    Public Const inDateTime As String = "$In Date Time$"
    Public Const outDateTime As String = "$Out Date Time$"

    Public Const Form_Code As String = "$FormId$"
    Public Const UserCode As String = "$UserCode$"

    '-------------RFQ---------------------
    Public Const RFQ_No As String = "$DOC No$"
    Public Const RFQ_Date As String = "$DOC DATE$"
    Public Const Request_No As String = "$REQ NO$"
    Public Const Request_Date As String = "$REQ Date$"
    Public Const Request_Amount As String = "$REQ Amt$"
    '-----------------------------------------------------

    '' Anubhooti 25-Aug-2014 BM00000003528
    '-------------Offer Letter HR---------------------
    Public Const App_No As String = "$Applicant No$"
    Public Const Offer_Date As String = "$Offer Date$"
    Public Const DOJ As String = "$DOJ$"
    Public Const Salary As String = "$Salary$"
    Public Const ApplicantName As String = "$Applicant Name$"
    '' Anubhooti 25-Aug-2014 BM00000003528
    '-------------Appointment Letter HR---------------------
    Public Const Appointment_Date As String = "$Appointment Date$"
    '-----------------------------------------------------
    '' Anubhooti 20-Oct-2015 BM00000008219
    '-------------Service Call SW---------------------
    Public Const Call_No As String = "$Service Call No$"
    Public Const Call_Date As String = "$Call Date$"
    Public Const Problem_Type As String = "$Problem_Type$"
    Public Const Subject As String = "$Subject$"
    Public Const ItemPartNo As String = "$Item Part No$"
    Public Const IssueNotice As String = "$Issue Notice$"
    Public Const AssignedTo As String = "$Assigned To$"
    Public Const AssignedBy As String = "$Assigned By$"
    '=================CSA DO====================
    Public Const DOC_NO As String = "$Document No$"
    Public Const DOC_Date As String = "$Document Date$"
    Public Const Cust_Name As String = "$CSA Name$"
    Public Const From_Location As String = "$From Location$"
    Public Const RT_Detail As String = "$RT Rate And UOM$"
    Public Const CSA_Item_Type As String = "$CSA Item Type$"
    Public Const Doc_Amount As String = "$Document Amount$"

    '-------------Leave Application---------------------
    Public Const Leave_App_No As String = "$Application No$"
    Public Const Application_Date As String = "$Application Date$"
    Public Const Leave_From As String = "$Leave From$"
    Public Const Leave_To As String = "$Leave To$"
    Public Const Leave_Type As String = "$Leave Type$"
    Public Const Leave_Days As String = "$Leave Days$"
    Public Const Leave_Reason As String = "$Leave Reason$"
    Public Const Employee_Name As String = "$Employee Name$"
    Public Const EMP_CODE As String = "$Employee Code$"

    '-------------Employeee Master---------------------
    Public Const Birth_Date As String = "$Birth Date$"
    Public Const AnniversaryDate As String = "$Anniversary Date$"
    Public Const ProbPeriodEnDate As String = "$Probation Period End Date$"

    '----------------Milk Shift End------------------------
    Public Const Doc_Code As String = "$DocNo$"
    'Public Const Doc_Date As String = "$DocDate$"
    Public Const Mcc_Code As String = "$Mcc_Code$"
    Public Const Mcc_Name As String = "$Mcc_Name$"
    Public Const Shift As String = "$Shift$"
    Public Const User_Code As String = "$Created_By$"
    Public Const State_Name As String = "$State$"
    Public Const Total_collection As String = "$Total_collection$"

    Public Const FAT As String = "$FAT$"
    Public Const SNF As String = "$SNF$"
    Public Const Rate As String = "$Rate$"
    Public Const Amount As String = "$Amount$"

    Public Const UOM As String = "$UOM$"
    ''richa agarwal against ticket no BM00000008361
    Public Const VLCCode As String = "$VLC_Code$"
    Public Const VLCUploaderCode As String = "$VLCUploaderCode$"
    Public Const VLCName As String = "$VLC_Name$"
    Public Const CowQty As String = "$Cow_Qty$"
    Public Const BuffaloQty As String = "$Buffalo_Qty$"
    Public Const CowFat As String = "$CowFat_%$"
    Public Const BuffaloFat As String = "$BuffaloFat%$"
    Public Const CowSNF As String = "$CowSNF_%$"
    Public Const BuffaloSNF As String = "$BuffaloSNF%$"
    Public Const CowAmount As String = "$Cow_Amount$"
    Public Const BuffaloAmount As String = "$Buffalo_Amount$"

    '----------------MCC Master------------------------
    Public Const Shift_Open_Time As String = "$Shift_Open_Time$"
    Public Const Total_Route As String = "$Total_Route$"
    Public Const Total_Vlc As String = "$Total_Vlc$"
    Public Const Shift_Close_Time As String = "$Shift_Close_Time$"
    '------------------------------------------------------

    Public Const CompanyName As String = "$CompanyName$"
End Class

Public Class AdjustmentEnum
    Public Const strCostTransaction As String = "Store Adjustment"
    Public Const strJWInvetoryTrans As String = "Job Work Inventory"
    Public Const strCostTransactionProductionEntry As String = "Production Entry"
    Public Const strCostTransactionEmpty As String = "Empty Transactions"
End Class

Public Class clsEmailAndSMSRecipients
    Public Const strTransTrype As String = "POS"
End Class


Public Class clsCalculationlApplyON
    Public Const RowTypeApplyOnAmount As String = "A"
    Public Const RowTypeApplyOnPercent As String = "P"

    Public Shared Function GetApplyOnType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = clsCalculationlApplyON.RowTypeApplyOnAmount
        dr("Name") = "Amount"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsCalculationlApplyON.RowTypeApplyOnPercent
        dr("Name") = "% Age"
        dt.Rows.Add(dr)

        Return dt
    End Function
End Class
Public Class clsItemRowType
    Public Const RowTypeItem As String = "Item"
    Public Const RowTypeMisc As String = "Misc"

End Class
Public Class MIlkComponentType
    Public FAT_Per As Decimal = 0
    Public SNF_Per As Decimal = 0
    Public FAT_Cost As Decimal = 0
    Public SNF_Cost As Decimal = 0
    Public FAT_Kg As Decimal = 0
    Public SNF_Kg As Decimal = 0

    Public Stock_Qty As Decimal = 0
    Public Stock_UOM As String = ""
End Class

Public Class MIlkComponentTypeNew
    Public FATRate As Decimal = 0
    Public SNFRate As Decimal = 0
    'Public Shared Function CalculateFATSNFRate(ByVal TotalAmt As Decimal, ByVal FATKg As Decimal, ByVal SNFKg As Decimal, ByVal FATPer As Decimal, ByVal SNFPer As Decimal) As MIlkComponentTypeNew
    '    Dim obj As New MIlkComponentTypeNew()
    '    Try
    '        Dim FATPrice As Decimal = clsCommon.myCDivide(TotalAmt, FATKg)
    '        Dim NoOfUnit As Decimal = FATPer + clsCommon.myCDivide(SNFPer * 2, 3)
    '        Dim UnitPrice As Decimal = clsCommon.myCDivide(FATPrice, NoOfUnit)
    '        obj.FATRate = UnitPrice * FATPer
    '        obj.SNFRate = clsCommon.myCDivide(obj.FATRate * 2, 3)
    '    Catch ex As Exception
    '    End Try
    '    Return obj
    'End Function

    'Public Shared Function CalculateFATSNFRate(ByVal TotalAmt As Decimal, ByVal ProduceItemQty As Decimal, ByVal PorduceItemFATPer As Decimal, ByVal PorduceItemSNFPer As Decimal) As MIlkComponentTypeNew
    '    Dim FATKg As Decimal = ProduceItemQty * PorduceItemFATPer / 100
    '    Dim SNFKg As Decimal = ProduceItemQty * PorduceItemSNFPer / 100
    '    Dim obj As New MIlkComponentTypeNew()
    '    Try
    '        Dim FATPrice As Decimal = clsCommon.myCDivide(TotalAmt, FATKg)
    '        Dim NoOfUnit As Decimal = PorduceItemFATPer + clsCommon.myCDivide(PorduceItemSNFPer * 2, 3)
    '        Dim UnitPrice As Decimal = clsCommon.myCDivide(FATPrice, NoOfUnit)
    '        obj.FATRate = UnitPrice * PorduceItemFATPer
    '        obj.SNFRate = clsCommon.myCDivide(obj.FATRate * 2, 3)
    '    Catch ex As Exception
    '    End Try
    '    Return obj
    'End Function

    Public Shared Function CalculateFATSNFRate(ByVal TotalAmt As Decimal, ByVal ProduceItemQty As Decimal, ByVal FATKg As Decimal, ByVal SNFKg As Decimal) As MIlkComponentTypeNew
        Dim FATPer As Decimal = clsCommon.myCDivide(FATKg * 100, ProduceItemQty)
        Dim SNFPer As Decimal = clsCommon.myCDivide(SNFKg * 100, ProduceItemQty)
        Dim obj As New MIlkComponentTypeNew()
        Try
            Dim FATPrice As Decimal = clsCommon.myCDivide(TotalAmt, FATKg)
            Dim NoOfUnit As Decimal = FATPer + clsCommon.myCDivide(SNFPer * 2, 3)
            Dim UnitPrice As Decimal = clsCommon.myCDivide(FATPrice, NoOfUnit)
            obj.FATRate = UnitPrice * FATPer
            obj.SNFRate = clsCommon.myCDivide(obj.FATRate * 2, 3)
            If FATKg = 0 Then
                obj.FATRate = 0
                obj.SNFRate = clsCommon.myCDivide(TotalAmt, SNFKg)
            End If
            If SNFKg = 0 Then
                obj.SNFRate = 0
                obj.FATRate = clsCommon.myCDivide(TotalAmt, FATKg)
            End If
        Catch ex As Exception
        End Try
        Return obj
    End Function
End Class


Public Class clsOpenTransactionForm

    Dim strRvalue As String = ""
    Function getNavigatorValue(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing) As String
        If clsCommon.myLen(strRvalue) > 0 Then
            Return strRvalue
            Exit Function
        End If

        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    'getNavigatorValue(Me, ctrl)
                End If
                If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        strRvalue = clsCommon.myCstr(CType(ctrl, common.UserControls.txtNavigator).Value)
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    ' getNavigatorValue(Me, ctrl)
                End If
                If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        strRvalue = clsCommon.myCstr(CType(ctrl, common.UserControls.txtNavigator).Value)
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        End If
        If clsCommon.myLen(strRvalue) > 0 Then
            Return strRvalue
            Exit Function
        End If
        Return ""
    End Function

    Public Shared Sub OpenTransacionForm(ByVal StrclsUserMgtCode As String, ByVal DocumnetNo As String)
        Try
            Select Case StrclsUserMgtCode
                Case clsUserMgtCode.frmSaleReturnProductSale, clsUserMgtCode.frmSaleReturnFreshSale, clsUserMgtCode.frmSaleReturndairy
                    Dim strDocType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Trans_Type from TSPL_SD_SALE_RETURN_HEAD where Document_Code='" + DocumnetNo + "'"))
                    Dim strScreen_Type As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Screen_Type from TSPL_SD_SALE_RETURN_HEAD where Document_Code='" + DocumnetNo + "'"))
                    If clsCommon.CompairString(strScreen_Type, "DS") = CompairStringResult.Equal Then
                        StrclsUserMgtCode = clsUserMgtCode.frmSaleReturndairy
                    Else
                        If clsCommon.CompairString(strDocType, "PS") = CompairStringResult.Equal Then
                            StrclsUserMgtCode = clsUserMgtCode.frmSaleReturnProductSale
                        ElseIf clsCommon.CompairString(strDocType, "FS") = CompairStringResult.Equal Then
                            StrclsUserMgtCode = clsUserMgtCode.frmSaleReturnFreshSale
                        End If
                    End If
                Case clsUserMgtCode.frmSNSaleReturn, clsUserMgtCode.saleReturn
                    Dim qry As String = "select 1 from TSPL_SALE_RETURN_HEAD where Sale_Return_No='" + DocumnetNo + "'"
                    Dim dt As DataTable = common.clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        StrclsUserMgtCode = clsUserMgtCode.frmSNSaleReturn
                    Else
                        StrclsUserMgtCode = clsUserMgtCode.saleReturn
                    End If
                Case clsUserMgtCode.frmShipmentProductSale
                    Dim strDocType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Trans_Type from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + DocumnetNo + "'"))
                    Dim strScreen_Type As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Screen_Type from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + DocumnetNo + "'"))
                    If clsCommon.CompairString(strScreen_Type, "DS") = CompairStringResult.Equal Then
                        StrclsUserMgtCode = clsUserMgtCode.frmSaleDispatchDairy
                    Else
                        If clsCommon.CompairString(strDocType, "PS") = CompairStringResult.Equal Then
                            StrclsUserMgtCode = clsUserMgtCode.frmShipmentProductSale
                        ElseIf clsCommon.CompairString(strDocType, "FS") = CompairStringResult.Equal Then
                            StrclsUserMgtCode = clsUserMgtCode.FrmDispatchFreshSale
                        Else
                            StrclsUserMgtCode = clsUserMgtCode.frmSNShipment
                        End If
                    End If
                Case clsUserMgtCode.FrmVendorService
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Type from TSPL_VENDOR_INVOICE_HEAD WHERE Document_No='" & DocumnetNo & "'")), "VS") = CompairStringResult.Equal Then
                        StrclsUserMgtCode = clsUserMgtCode.FrmVendorService
                    Else
                        StrclsUserMgtCode = clsUserMgtCode.mbtnAPInvoiceEntry
                    End If
                Case clsUserMgtCode.ScrapSale
                    Dim qry As String = "SELECT shipment_No,TSPL_SCRAPINVOICE_HEAD.Doc_Type from TSPL_SCRAPINVOICE_HEAD where invoice_No='" + DocumnetNo + "'"
                    Dim dt As DataTable = common.clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        DocumnetNo = clsCommon.myCstr(dt.Rows(0)("shipment_No"))
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Doc_Type")), "J") = CompairStringResult.Equal Then
                            StrclsUserMgtCode = clsUserMgtCode.JobWorkDispatchProduction
                        Else
                            StrclsUserMgtCode = clsUserMgtCode.ScrapSale
                        End If
                    End If
                Case clsUserMgtCode.FrmVendorService

                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Type from TSPL_VENDOR_INVOICE_HEAD WHERE Document_No='" & DocumnetNo & "'")), "VS") = CompairStringResult.Equal Then
                        StrclsUserMgtCode = clsUserMgtCode.FrmVendorService
                    Else
                        StrclsUserMgtCode = clsUserMgtCode.mbtnAPInvoiceEntry
                    End If
                Case clsUserMgtCode.frmSNServiceInvoice, clsUserMgtCode.frmSNSaleInvoice
                    Dim strInvType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Type from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & DocumnetNo & "' "))
                    If clsCommon.CompairString(strInvType, "S") = CompairStringResult.Equal Then
                        StrclsUserMgtCode = clsUserMgtCode.frmSNServiceInvoice
                    Else
                        StrclsUserMgtCode = clsUserMgtCode.frmSNSaleInvoice
                    End If
            End Select



            Application.OpenForms("MDI").Controls("__txtScreenID").Text = StrclsUserMgtCode
            Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo

        Catch ex As Exception
        End Try
    End Sub

    'Public Shared Sub OpenTransacionForm(ByVal TransType As EnumTransType, ByVal DocumnetNo As String)
    '    'clsCommon.ProgressBarShow()
    '    'clsCommon.ProgressBarUpdate("Opening Transacion.Please wait...")
    '    Try
    '        Select Case TransType
    '            Case clsUserMgtCode.journalEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.journalEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
    '                'frm.strVoucherNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.ReceiptEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.ReceiptEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmReceipttNew
    '                'frm.strRcptNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMCCDispatch
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMCCDispatch
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmMccDispatch
    '                'frm.strDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMilkTransferInReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkTransferInReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmMilkTransferIn
    '                'frm.strDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMilkTransferIn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkTransferIn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmMilkTransferIn
    '                'frm.strDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.PaymentEntryNew
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.PaymentEntryNew
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmPaymentNew()
    '                'frm.strPaymentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.PaymentAdjustmentEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.PaymentAdjustmentEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmPaymentNew()
    '                'frm.strPaymentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.LoadOut
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.LoadOut
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmShipmentInvoice()
    '                'frm.strLoadOutNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.FrmVendorService
    '                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Type from TSPL_VENDOR_INVOICE_HEAD WHERE Document_No='" & DocumnetNo & "'")), "VS") = CompairStringResult.Equal Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmVendorService
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New FrmVendorService()
    '                    'frm.text = "Vendor Service Charge"
    '                Else
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnAPInvoiceEntry
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New FrmAPInvoiceEntry()
    '                End If
    '                'frm.strAPInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.bankTransfer
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.bankTransfer
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmBankTransfer(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
    '                'frm.strbankTrans = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnSRN
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnSRN
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmSRN()
    '                'frm.strSRNno = DocumnetNo
    '                'frm.FORMTYPE = clsUserMgtCode.mbtnSRN
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmInvoiceFreshSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmInvoiceFreshSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmInvoiceFreshSale()
    '                'frm.strSRNno = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()

    '            Case clsUserMgtCode.frmCreateReceived
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmCreateReceived
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                ''frm = New frmCreateReceived()
    '                ''frm.strCrateReceived = DocumnetNo
    '                ''frm.WindowState = FormWindowState.Maximized
    '                ''frm.Show()

    '            Case clsUserMgtCode.mbtnPurchaseOrder
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnPurchaseOrder
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmPurchaseOrder(clsUserMgtCode.mbtnPurchaseOrder)
    '                'frm.PurchaseOrderNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnPurchaseRequistion
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnPurchaseRequistion
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmPurchaseOrder(clsUserMgtCode.mbtnPurchaseOrder)
    '                'frm.PurchaseOrderNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.FrmExpiryDateEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmExpiryDateEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmExpiryDateEntry()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '                'Case clsUserMgtCode.Transfer
    '                '    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmExpiryDateEntry
    '                '    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmTransferNew(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
    '                'frm.strTrnasfer = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.reverseTransaction
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.reverseTransaction
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmReverseTransaction(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
    '                'frm.strBankRvrse = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnStoreAdjustment

    '                Dim strAdjustmentType As String = ClsAdjustments.GetTransactionType(DocumnetNo, Nothing)
    '                'Dim strCode As String
    '                'Dim qry As String
    '                'qry = "select TSPL_ADJUSTMENT_DETAIL.Adjustment_No  from TSPL_ADJUSTMENT_DETAIL where adjustment_No='" & DocumnetNo & "'"
    '                'Dim strCodeAdjustment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    '                'qry = "select TSPL_ADJUSTMENT_DETAIL.Adjustment_No  from TSPL_ADJUSTMENT_DETAIL "
    '                'Dim strCodeAdjustmentProduction As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    '                'qry = "select TSPL_ADJUSTMENT_DETAIL.Adjustment_No  from TSPL_ADJUSTMENT_DETAIL "
    '                'Dim strCodeStoreAdjustment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    '                If common.clsCommon.CompairString(AdjustmentEnum.strCostTransactionEmpty, strAdjustmentType) = common.CompairStringResult.Equal Then
    '                    'frm = New frmAdjustmentEmpty()
    '                    'frm.strDocumentNo = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnEmptyTrans
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo



    '                ElseIf common.clsCommon.CompairString(AdjustmentEnum.strCostTransactionProductionEntry, strAdjustmentType) = common.CompairStringResult.Equal Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnProductionEntry
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmAdjustmentProduction()
    '                    'frm.strDocumentNo = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                ElseIf common.clsCommon.CompairString(AdjustmentEnum.strCostTransaction, strAdjustmentType) = common.CompairStringResult.Equal Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnStoreAdjustment
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmAdjustmentStore()
    '                    'frm.strDocumentNo = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                End If
    '            Case clsUserMgtCode.frmSNSaleInvoice
    '                'frm = New frmShipmentInvoice()
    '                'frm.strLoadOutNo = common.clsCommon.myCstr(common.clsDBFuncationality.getSingleValue("select Shipment_No from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + DocumnetNo + "'"))
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()

    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSNSaleInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmSNSaleInvoice
    '                'frm.DocumentNo = DocumnetNo 'common.clsCommon.myCstr(common.clsDBFuncationality.getSingleValue("select Against_Shipment_No from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + DocumnetNo + "'"))
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.MdiParent = MDI
    '                'frm.Show()
    '            Case clsUserMgtCode.frmSNServiceInvoice
    '                Dim strInvType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Type from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & DocumnetNo & "' "))
    '                If clsCommon.CompairString(strInvType, "S") = CompairStringResult.Equal Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSNServiceInvoice
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmSNServiceInvoice()
    '                    'frm.strSaleInvoice = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                Else
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSNSaleInvoice
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmSNSaleInvoice()
    '                    'frm.strSaleInvoice = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                End If


    '            Case clsUserMgtCode.ReceiptAdjustmentEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.ReceiptAdjustmentEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmAdj()
    '                'frm.strAdjNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnPurchaseInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnPurchaseInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmPurchaseInvoice()
    '                'frm.strPOInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmSalesmanTarget
    '                'frm = New FrmSalesmanTarget()
    '                'frm.Code = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnPurchaseReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnPurchaseReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmPurchaseReturn()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.saleReturn, clsUserMgtCode.saleReturn
    '                Dim qry As String = "select 1 from TSPL_SALE_RETURN_HEAD where Sale_Return_No='" + DocumnetNo + "'"
    '                Dim dt As DataTable = common.clsDBFuncationality.GetDataTable(qry)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSNSaleReturn
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmSNSaleReturn()
    '                    'frm.DocumentNo = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                Else
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.saleReturn
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmSalesReturnNew()
    '                    'frm.strPOInvoice = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                End If
    '            Case clsUserMgtCode.mbtnARInvoiceEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnARInvoiceEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmARInvoiceEntry()
    '                'frm.strAPInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.ScrapSale
    '                Dim qry As String = "SELECT shipment_No from TSPL_SCRAPINVOICE_HEAD where invoice_No='" + DocumnetNo + "'"
    '                Dim dt As DataTable = common.clsDBFuncationality.GetDataTable(qry)
    '                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.ScrapSale
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmScrapSale()
    '                    'frm.strShipmentno = clsCommon.myCstr(dt.Rows(0)("shipment_No"))
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                End If
    '            Case clsUserMgtCode.ScrapSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.ScrapSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmScrapSale()
    '                'frm.strShipmentno = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnIssueReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnIssueReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmIssueReturn()
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnGatePass
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnGatePass
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmRGP()
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnGatePass
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnGatePass
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmRGP()
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmShipmentProductSale
    '                Dim strDocType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Trans_Type from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + DocumnetNo + "'"))
    '                If clsCommon.CompairString(strDocType, "PS") = CompairStringResult.Equal Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmShipmentProductSale
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmShipmentProductSale()
    '                    'frm.DocumentNo = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                ElseIf clsCommon.CompairString(strDocType, "FS") = CompairStringResult.Equal Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmDispatchFreshSale
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmDispatchNoteFreshSale()
    '                    'frm.DocumentNo = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                Else
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSNShipment
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmSNShipment()
    '                    'frm.DocumentNo = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                End If
    '                '===============================================================================

    '            Case clsUserMgtCode.frmSNSaleReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSNSaleReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmSNSaleReturn()
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmAssemblies
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmAssemblies
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmAssemblies()
    '                'frm.Document_No = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case EnumTransType.WareHouseBreakage
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmWarehouseBreakage
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmWarehouseBreakage()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnVCGLEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnVCGLEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmVCGLEntry()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnAPInvoiceEntryTDS
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnAPInvoiceEntryTDS
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmAPInvoiceEntryTDS()
    '                'frm.strAPInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case EnumTransType.SaleQuotation
    '                'Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSNShipment
    '                'Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmSaleQuotation()
    '                'frm.DocCode = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmSNSalesOrder
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSNSalesOrder
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmSNSalesOrder()
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.glAccount
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.glAccount
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmGLAccount(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
    '                'frm.strAccountCode = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnProductionEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnProductionEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmAdjustmentProduction()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.Transfer
    '                'frm = New frmTransferDCC()
    '                'frm.strTransferno = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.Transfer
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New FrmTransferKDIL()
    '                    'frm.strTransferno = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                    'formShow(frm, strProgramName)
    '                Else
    '                    Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.Transfer
    '                    Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                    'frm = New frmTransferDCC()
    '                    'frm.strTransferno = DocumnetNo
    '                    'frm.WindowState = FormWindowState.Maximized
    '                    'frm.Show()
    '                    'formShow(frm, strProgramName)
    '                End If
    '                '===============================================================================
    '            Case clsUserMgtCode.frmCSATransfer
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmCSATransfer
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmCSATransfer()
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmCSASaleInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmCSASaleInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmCSASaleInvoice()
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case EnumTransType.SDCSADO
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmCSADeliveryOrder
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmCSADeliveryOrder()
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.FrmBankGuaranteeMaster1
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmBankGuaranteeMaster1
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmBankGuaranteeMaster1()
    '                'frm.strPaymentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMilkSRN
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkSRN
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmMilkSRNMCC()
    '                'frmMilkSRNMCC.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMilkPurchaseInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkPurchaseInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmMilkPurchaseInvoiceMCC()
    '                'frmMilkPurchaseInvoiceMCC.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmRiceMixingEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmRiceMixingEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmRiceMixingEntry()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmRiceProcessingEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmRiceProcessingEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmRiceProcessingEntry()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmProcessProductionIssueEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmProcessProductionIssueEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmProcessProductionIssueEntry()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMCCMaterial
    '                DocumnetNo = clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_shipment_HEAD where sale_Invoice_No='" & DocumnetNo & "'")
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMCCMaterial
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmMCCMaterialSale()
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMCCMaterial
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMCCMaterial
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmMCCMaterialSale()
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMCCMaterialSaleReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMCCMaterialSaleReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmMccMaterialSaleReturn()
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmEXSalesOrder
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSalesOrderMT
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXSalesOrder(clsUserMgtCode.frmEXSalesOrder)
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmEXSalesQuotation
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXSalesQuotation
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmEXSalesQuotation(clsUserMgtCode.frmEXSalesQuotation)
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmEXPorformaInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXPorformaInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXPorformaInvoice(clsUserMgtCode.frmEXPorformaInvoice)
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmEXCommercialInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXCommercialInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXCommercialInvoice(clsUserMgtCode.frmEXCommercialInvoice)
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmInvoiceFreshSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmInvoiceFreshSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmInvoiceFreshSale()
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()

    '            Case clsUserMgtCode.saleReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSaleReturnFreshSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmSaleReturnFreshSale()
    '                'frm.strSRNno = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '                '=====================================================================================================
    '            Case clsUserMgtCode.frmSaleInvoiceProductSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSaleInvoiceProductSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmSaleInvoiceProductSale()
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmSaleReturnProductSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSaleReturnProductSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmSaleReturnProductSale()
    '                'frm.strSRNno = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmCSASaleInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmCSASaleInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmCSASaleInvoice()
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmEXSalesInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXSalesInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXSalesInvoice(clsUserMgtCode.frmEXSalesInvoice)
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.FrmBulkSaleReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmBulkSaleReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmBulkSaleReturn()
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.FrmInvoiceBulkSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmInvoiceBulkSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmInvoiceBulkSale()
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()

    '            Case clsUserMgtCode.frmCSATransferReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmCSATransferReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmCSATransferReturn()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.TransferReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.TransferReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmCSATransferReturn()
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmProcessProductionStandardization
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmProcessProductionStandardization
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmProcessProductionStandardization()
    '                'frm.strDocumentCode = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmProcessProductionStageProcess
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmProcessProductionStageProcess
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmProcessProductionStandardization()
    '                'frm.strDocumentCode = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case EnumTransType.BulkSRNTrade
    '                'Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSaleReturnFreshSale
    '                'Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmBulkTradeSRN
    '                'frm.strDocumentCode = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.FrmDispatchBulkSaleTrade
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmDispatchBulkSaleTrade
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmDispatchBulkSaleTrade
    '                'frm.strDocumentCode = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.FrmDispatchBulkSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmDispatchBulkSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmDispatchBulkSale
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmBulkMilkSRN
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmBulkMilkSRN
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmBulkMilkSRN
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized

    '                'frm.Show()
    '            Case clsUserMgtCode.frmProcessProductionStageProcess
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmProcessProductionStageProcess
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmProcessProductionStageProcess
    '                'frm.strDocumentCode = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmVSPItemIssue
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmVSPItemIssue
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmVSPItemIssue
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmProductionEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmProductionEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmProductionEntry
    '                'frm.strDocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmEXSalesReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXSalesReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXSalesReturn(clsUserMgtCode.frmEXSalesReturn)
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmDispatchMultipleFreshSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmDispatchMultipleFreshSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmDispatchMultipleFreshSale
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmShipmentProductSale
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmShipmentProductSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmShipmentProductSale()
    '                'frm.DocumentNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmSalesOrderMT
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSalesOrderMT
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXSalesOrder(clsUserMgtCode.frmSalesOrderMT)
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case EnumTransType.MT_Proforma
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmProformaInvoiceMT
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXPorformaInvoice(clsUserMgtCode.frmProformaInvoiceMT)
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmEXCommercialInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmCommercialInvoiceMT
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXCommercialInvoice(clsUserMgtCode.frmCommercialInvoiceMT)
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmSalesInvoiceMT
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSalesInvoiceMT
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXSalesInvoice(clsUserMgtCode.frmSalesInvoiceMT)
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case EnumTransType.MT_Sale_Ret
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSalesReturnMT
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmEXSalesReturn(clsUserMgtCode.frmSalesReturnMT)
    '                'frm.strSaleInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmEXSalesQuotation
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXSalesQuotation
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmEXSalesQuotation(clsUserMgtCode.frmEXSalesQuotation)
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnPurchaseInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXSalesQuotation
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmEXSalesQuotation(clsUserMgtCode.mbtnPurchaseInvoice)
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnPurchaseInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXSalesQuotation
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmEXSalesQuotation(clsUserMgtCode.mbtnPurchaseInvoice)
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case EnumTransType.MilkReceipt
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXSalesQuotation
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmEXSalesQuotation(clsUserMgtCode.frmMilkPurchaseInvoice)
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMilkTransferIn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmEXSalesQuotation
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmEXSalesQuotation(clsUserMgtCode.mbtnPurchaseInvoice)
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmBulkMilkPurchaseInvoice
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmBulkMilkPurchaseInvoice
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmMilkPurchaseInvoice
    '                'frm.tag = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmMilkTransferIn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkTransferIn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmMilkTransferIn
    '                'frm.StrDocNo = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.mbtnGRN
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnGRN
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                ''frm = New frmGRN()
    '                ''frm.strGRN = DocumnetNo
    '                ''frm.WindowState = FormWindowState.Maximized
    '                ''frm.Show()
    '            Case clsUserMgtCode.mbtnMRN
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.mbtnMRN
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New frmMRN()
    '                'frm.strGRN = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.FrmVendorService
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.FrmVendorService
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmVendorService()
    '                'frm.strAPInvoice = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case clsUserMgtCode.frmComplaintDetailEntry
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmComplaintDetailEntry
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '                'frm = New FrmComplaintDetailEntry()
    '                'frm.strComplaint = DocumnetNo
    '                'frm.WindowState = FormWindowState.Maximized
    '                'frm.Show()
    '            Case EnumTransType.DeliveryOrderPS
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmDeliveryPrderProductSale
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo

    '            Case clsUserMgtCode.frmMilkJobWorkTransfer
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkJobWorkTransfer
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmMilkJobWorkTransferOther
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkJobWorkTransferOther
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmProcessProdReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmProcessProdReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.JWO_SRN_Return
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.JWO_SRN_Return
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.JWO_SRN
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.JWO_SRN
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmMilkJobWorkTransferReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkJobWorkTransferReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmMilkJobWorkTransferOtherReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMilkJobWorkTransferOtherReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmMCCMaterialFarmer
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMCCMaterialFarmer
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmMCCMaterialSaleReturnFarmer
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmMCCMaterialSaleReturnFarmer
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmFarmerPaymentAdjustment
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmFarmerPaymentAdjustment
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.MCCDispatchReturn
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.MCCDispatchReturn
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmDeliveryOrderDairy
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmDeliveryOrderDairy
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '            Case clsUserMgtCode.frmSaleDispatchDairy
    '                Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.frmSaleDispatchDairy
    '                Application.OpenForms("MDI").Controls("__txtDocNo").Text = DocumnetNo
    '        End Select
    '    Catch ex As Exception

    '    Finally
    '        'clsCommon.ProgressBarHide()
    '        'frm.focus()
    '    End Try
    'End Sub

End Class

Public Class clsDateRange
    Public FromDate As Date
    Public ToDate As Date
    Public Days As Integer
End Class
