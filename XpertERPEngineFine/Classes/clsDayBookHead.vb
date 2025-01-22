Public Class clsDayBookHead
#Region "Variable"
    Public Document_Date As DateTime
    Public Cust_Code As String = ""
    Public Cust_Name As String = ""
    Public TaxGroup As String = ""
    Public InvoiceNo As String = ""
    Public DebitAmt As String = 0
    Public CreditAmt As String = 0
    Public Bank_Code As String = ""
    Public Arr As List(Of clsDayBookItems) = Nothing
    Public ArrTax As List(Of clsDayBookTaxDetail) = Nothing
#End Region
    Public Shared Function GetData(ByVal docDate As DateTime, ByVal strLocation As String) As List(Of clsDayBookHead)
        Dim lstobj As New List(Of clsDayBookHead)
        Dim strqry As String = ""
        Try
            strqry = "select Receipt_Date,Receipt_No,Bank_Code,Cust_Code,Customer_Name,Receipt_Amount from TSPL_RECEIPT_HEADER where convert(date,Receipt_Date,103)='" + clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") + "' and Posted='Y'"
            Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dtReceipt IsNot Nothing AndAlso dtReceipt.Rows.Count > 0 Then
                For Each drReceipt As DataRow In dtReceipt.Rows
                    Dim obj As New clsDayBookHead()
                    obj.Document_Date = clsCommon.myCDate(drReceipt("Receipt_Date"))
                    obj.Cust_Code = clsCommon.myCstr(drReceipt("Cust_Code"))
                    obj.Cust_Name = clsCommon.myCstr(drReceipt("Customer_Name"))
                    obj.InvoiceNo = clsCommon.myCstr(drReceipt("Receipt_No"))
                    obj.Bank_Code = clsCommon.myCstr(drReceipt("Bank_Code"))
                    obj.TaxGroup = "Receipt"
                    obj.DebitAmt = clsCommon.myCstr(clsCommon.myCdbl(drReceipt("Receipt_Amount")))
                    obj.CreditAmt = clsCommon.myCstr(clsCommon.myCdbl(drReceipt("Receipt_Amount")))
                    lstobj.Add(obj)
                Next
            End If
            strqry = "select Document_Date,Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name, Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Tax_Group,Total_Amt from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
where CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)='" + clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") + "' and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + strLocation + "' and TSPL_SD_SALE_INVOICE_HEAD.Status=1 "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim obj As New clsDayBookHead()

                    obj.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                    obj.Cust_Code = clsCommon.myCstr(dr("Customer_Code"))
                    obj.Cust_Name = clsCommon.myCstr(dr("Customer_Name"))
                    obj.InvoiceNo = clsCommon.myCstr(dr("Document_Code"))
                    obj.TaxGroup = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + clsCommon.myCstr(dr("Tax_Group")) + "' and Tax_Group_Type='S'"))
                    'obj.TaxGroup = clsCommon.myCstr(dr("Tax_Group"))
                    obj.DebitAmt = clsCommon.myCstr(clsCommon.myCdbl(dr("Total_Amt")))
                    Dim qry As String = "select sum(TSPL_SD_SALE_INVOICE_DETAIL.Amount) as Amount from TSPL_SD_SALE_INVOICE_DETAIL
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code where DOCUMENT_CODE='" + obj.InvoiceNo + "' group by DOCUMENT_CODE"
                    obj.CreditAmt = clsCommon.myCstr(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)))
                    obj.Arr = clsDayBookItems.GetData(obj.InvoiceNo)
                    obj.ArrTax = clsDayBookTaxDetail.GetData(obj.InvoiceNo)
                    lstobj.Add(obj)
                Next
            End If
            strqry = "
    select TSPL_TRANSFER_ORDER_HEAD.document_no,tspl_location_master.location_desc, TSPL_INVENTORY_MOVEMENT.Trans_Type, source_doc_date, TSPL_TRANSFER_ORDER_HEAD.From_Location, 
      TSPL_TRANSFER_ORDER_HEAD.To_Location, Inout,TSPL_INVENTORY_MOVEMENT.item_code,TSPL_ITEM_master.item_desc,Case when TSPL_INVENTORY_MOVEMENT.Inout = 'I' then TSPL_INVENTORY_MOVEMENT.qty else 0 end as INWARDQTY, 
      Case when TSPL_INVENTORY_MOVEMENT.Inout = 'O' then TSPL_INVENTORY_MOVEMENT.qty else 0 end as OUTWARDQTY, TSPL_INVENTORY_MOVEMENT.Qty,TSPL_ITEM_master.Item_Type,TSPL_INVENTORY_MOVEMENT.uom as UOM 
    from TSPL_INVENTORY_MOVEMENT 
      left join TSPL_TRANSFER_ORDER_HEAD ON TSPL_TRANSFER_ORDER_HEAD.DOCUMENT_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No 
left join tspl_location_master on tspl_location_master.location_code=TSPL_TRANSFER_ORDER_HEAD.from_location
      left join TSPL_ITEM_master on TSPL_ITEM_master.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code     
    where TSPL_INVENTORY_MOVEMENT.trans_type in ('ITransfer', 'Trasnfer') and CONVERT(date,TSPL_TRANSFER_ORDER_HEAD.document_date,103)='" + clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") + "'  and TSPL_INVENTORY_MOVEMENT.Location_Code='" + strLocation + "' and TSPL_TRANSFER_ORDER_HEAD.Status=1 "

            Dim dtTransfer As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dtTransfer IsNot Nothing AndAlso dtTransfer.Rows.Count > 0 Then
                For Each drt As DataRow In dtTransfer.Rows
                    Dim obj As New clsDayBookHead()
                    obj.Document_Date = clsCommon.myCDate(drt("source_doc_date"))
                    obj.Cust_Code = clsCommon.myCstr(drt("item_code"))
                    obj.Cust_Name = clsCommon.myCstr(drt("item_desc"))
                    obj.InvoiceNo = clsCommon.myCstr(drt("document_no"))
                    obj.TaxGroup = clsCommon.myCstr(drt("Inout"))
                    If clsCommon.CompairString(obj.TaxGroup, "O") = CompairStringResult.Equal Then
                        obj.CreditAmt = clsCommon.myCstr(clsCommon.myCdbl(drt("OUTWARDQTY"))) + " " + clsCommon.myCstr(drt("UOM"))

                    Else
                        obj.DebitAmt = clsCommon.myCstr(clsCommon.myCdbl(drt("INWARDQTY"))) + " " + clsCommon.myCstr(drt("UOM"))

                    End If
                    obj.Bank_Code = clsCommon.myCstr(drt("location_desc"))
                    'obj.Arr = clsDayBookItems.GetData(obj.InvoiceNo)
                    'obj.ArrTax = clsDayBookTaxDetail.GetData(obj.InvoiceNo)
                    lstobj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return lstobj
    End Function

End Class
Public Class clsDayBookItems
#Region "Variable"
    Public DOCUMENT_CODE As String = ""
    Public Item_Code As String = ""
    Public Item_Name As String = ""
    Public Qty As Decimal = 0
    Public UOM As String = ""
    Public Item_Rate As Decimal = 0
    Public Amount As Decimal = 0

#End Region
    Public Shared Function GetData(ByVal strDoc As String) As List(Of clsDayBookItems)
        Dim arrObj As List(Of clsDayBookItems) = Nothing
        Dim obj As clsDayBookItems = Nothing
        Try

            Dim strQry As String = "select TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE,
TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,
TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,
(TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Item_Rate,
TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,TSPL_SD_SALE_INVOICE_DETAIL.Amount from TSPL_SD_SALE_INVOICE_DETAIL
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code where DOCUMENT_CODE='" + strDoc + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsDayBookItems)
                For Each dr As DataRow In dt.Rows
                    obj = New clsDayBookItems()
                    obj.DOCUMENT_CODE = clsCommon.myCstr(dr("DOCUMENT_CODE"))
                    obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    obj.Item_Name = clsCommon.myCstr(dr("Short_Description"))
                    obj.Qty = clsCommon.myCdbl(dr("Qty"))
                    obj.UOM = clsCommon.myCstr(dr("Unit_code"))
                    obj.Item_Rate = clsCommon.myCdbl(dr("Item_Rate"))
                    obj.Amount = clsCommon.myCdbl(dr("Amount"))
                    arrObj.Add(obj)
                Next
            Else
                Throw New Exception("No Data Found!")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class
Public Class clsDayBookTaxDetail
#Region "Variable"
    Public Document_Code As String = ""
    Public Tax_Name As String = ""
    Public Tax_Rate As Decimal = 0
    Public Tax_Amt As Decimal = 0
#End Region
    Public Shared Function GetData(ByVal strDoc As String) As List(Of clsDayBookTaxDetail)
        Dim ArrTaxObj As List(Of clsDayBookTaxDetail) = Nothing
        Dim obj As clsDayBookTaxDetail = Nothing
        Try
            Dim strQry As String = "Select XX.* From ( SELECT DOCUMENT_CODE,ISNULL(TAX1, '') AS Tax,TAX1_Rate AS Tax_Rate,sum(TAX1_Amt) as TaxAmt FROM TSPL_SD_SALE_INVOICE_DETAIL 
    WHERE TAX1 <> '' and TAX1_Amt>0 and Document_Code='" + strDoc + "'	group by TAX1,DOCUMENT_CODE,TAX1_Rate    
    UNION    
    SELECT DOCUMENT_CODE,ISNULL(TAX2, '') AS Tax,TAX2_Rate AS Tax_Rate,Sum(TAX2_Amt) as TaxAmt FROM TSPL_SD_SALE_INVOICE_DETAIL 
    WHERE TAX2 <> ''  and TAX2_Amt>0 and Document_Code='" + strDoc + "' group by TAX2,DOCUMENT_CODE,TAX2_Rate
    UNION    
    SELECT  DOCUMENT_CODE, ISNULL(TAX3, '') AS Tax,TAX3_Rate AS Tax_Rate,Sum(TAX3_Amt) as TaxAmt FROM TSPL_SD_SALE_INVOICE_DETAIL 
    WHERE TAX3 <> ''  and TAX3_Amt>0 and Document_Code='" + strDoc + "' group by Tax3,DOCUMENT_CODE,TAX3_Rate
    UNION    
    SELECT  DOCUMENT_CODE,ISNULL(TAX4, '') AS Tax,TAX4_Rate AS Tax_Rate,Sum(TAX4_Amt) as TaxAmt FROM TSPL_SD_SALE_INVOICE_DETAIL 
    WHERE TAX4 <> ''  and TAX4_Amt>0 and Document_Code='" + strDoc + "' group by Tax4,DOCUMENT_CODE,TAX4_Rate
    UNION    
    SELECT  DOCUMENT_CODE,ISNULL(TAX5, '') AS Tax,TAX5_Rate AS Tax_Rate,Sum(TAX5_Amt) as TaxAmt FROM TSPL_SD_SALE_INVOICE_DETAIL 
    WHERE TAX5 <> ''  and TAX5_Amt>0  and Document_Code='" + strDoc + "' group by Tax5,DOCUMENT_CODE,TAX5_Rate
	 UNION    
    SELECT DOCUMENT_CODE, ISNULL(TAX6, '') AS Tax,TAX6_Rate AS Tax_Rate,Sum(TAX6_Amt) as TaxAmt FROM TSPL_SD_SALE_INVOICE_DETAIL 
    WHERE TAX6 <> ''  and TAX6_Amt>0 and Document_Code='" + strDoc + "' group by Tax6,DOCUMENT_CODE,TAX6_Rate ) xx order by xx.Tax_Rate"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ArrTaxObj = New List(Of clsDayBookTaxDetail)
                For Each dr As DataRow In dt.Rows
                    obj = New clsDayBookTaxDetail()
                    obj.DOCUMENT_CODE = clsCommon.myCstr(dr("DOCUMENT_CODE"))
                    obj.Tax_Name = clsCommon.myCstr(dr("Tax"))
                    obj.Tax_Rate = clsCommon.myCstr(dr("Tax_Rate"))
                    obj.Tax_Amt = clsCommon.myCdbl(dr("TaxAmt"))
                    ArrTaxObj.Add(obj)
                Next

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ArrTaxObj
    End Function
End Class
