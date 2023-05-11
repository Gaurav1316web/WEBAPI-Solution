Imports System.Data.SqlClient
Imports common

Public Class clsInvoicePriceCompHead
#Region "Variables"
    Public Inv_Price_Code As String = Nothing
    Public Invoice_NO As String = Nothing
    Public Invoice_Date As String = Nothing
    Public Loadout_No As String = Nothing
    Public Loadout_Date As String = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Price_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Invoice_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Total_Empty_Value As Double = 0
    Public Total_TPT As Double = 0
    Public Total_Invoice_Amt As Double = 0
    Public Arr As List(Of clsInvoicePirceCompDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsInvoicePriceCompHead, ByVal Trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim strCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Max(Inv_Price_Code) as Code from TSPL_INV_PRICE_cOM_HEAD", Trans))
            If clsCommon.myLen(strCode) <= 0 Then
                strCode = "INPC000001"
            Else
                strCode = clsCommon.incval(strCode)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Inv_Price_Code", strCode)
            clsCommon.AddColumnsForChange(coll, "Loadout_No", obj.Loadout_No)
            clsCommon.AddColumnsForChange(coll, "Loadout_Date", clsCommon.GetPrintDate(obj.Loadout_Date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Invoice_NO", obj.Invoice_NO)
            clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.Invoice_Date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Invoice_Amt", obj.Invoice_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Empty_Value", obj.Total_Empty_Value)
            clsCommon.AddColumnsForChange(coll, "Total_TPT", obj.Total_TPT)
            clsCommon.AddColumnsForChange(coll, "Total_Invoice_Amt", obj.Total_Invoice_Amt)

            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INV_PRICE_cOM_HEAD", OMInsertOrUpdate.Insert, "", Trans)
            isSaved = isSaved AndAlso clsInvoicePirceCompDetail.SaveData(strCode, Arr, Trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    ''Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsInvoicePirceCompHead
    ''    Dim obj As clsInvoicePirceCompHead = Nothing
    ''    Dim qry As String = "SELECT TSPL_PURCHASE_ORDER_HEAD.InvPrice_Code,TSPL_PURCHASE_ORDER_HEAD.Loadout_No  ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_HEAD.Customer_Code,TSPL_PURCHASE_ORDER_HEAD.Customer_Name,TSPL_PURCHASE_ORDER_HEAD.Status,TSPL_PURCHASE_ORDER_HEAD.On_Hold,TSPL_PURCHASE_ORDER_HEAD.Invoice_NO,TSPL_PURCHASE_ORDER_HEAD.Invoice_Date,TSPL_PURCHASE_ORDER_HEAD.Price_Code,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_HEAD.Route_No,TSPL_PURCHASE_ORDER_HEAD.Ship_To_Location,TSPL_PURCHASE_ORDER_HEAD.TAX1,TSPL_PURCHASE_ORDER_HEAD.TAX1_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX1_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX1_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX2,TSPL_PURCHASE_ORDER_HEAD.TAX2_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX2_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX2_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX3,TSPL_PURCHASE_ORDER_HEAD.TAX3_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX3_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX3_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX4,TSPL_PURCHASE_ORDER_HEAD.TAX4_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX4_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX4_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX5,TSPL_PURCHASE_ORDER_HEAD.TAX5_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX5_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX5_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX6,TSPL_PURCHASE_ORDER_HEAD.TAX6_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX6_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX6_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX7,TSPL_PURCHASE_ORDER_HEAD.TAX7_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX7_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX7_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX8,TSPL_PURCHASE_ORDER_HEAD.TAX8_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX8_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX8_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX9,TSPL_PURCHASE_ORDER_HEAD.TAX9_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX9_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX9_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX10,TSPL_PURCHASE_ORDER_HEAD.TAX10_Rate,TSPL_PURCHASE_ORDER_HEAD.TAX10_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX10_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.Total_TPT,TSPL_PURCHASE_ORDER_HEAD.Total_Empty_Value,TSPL_PURCHASE_ORDER_HEAD.Total_Tax_Amt,TSPL_PURCHASE_ORDER_HEAD.Total_Tax_Amt,TSPL_PURCHASE_ORDER_HEAD.Invoice_Amt,TSPL_PURCHASE_ORDER_HEAD.Mode_Of_Transport,TSPL_PURCHASE_ORDER_HEAD.Comments,TSPL_PURCHASE_ORDER_HEAD.Comp_Code,TSPL_PURCHASE_ORDER_HEAD.Terms_Code,TSPL_PURCHASE_ORDER_HEAD.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_PURCHASE_ORDER_HEAD.Posting_Date,TSPL_PURCHASE_ORDER_HEAD.Loadout_Date,TSPL_PURCHASE_ORDER_HEAD.Dept,TSPL_PURCHASE_ORDER_HEAD.Dept_Desc,TSPL_PURCHASE_ORDER_HEAD.Item_Type,TSPL_PURCHASE_ORDER_HEAD.Modify_By,TSPL_PURCHASE_ORDER_HEAD.Modify_Date,TSPL_PURCHASE_ORDER_HEAD.Created_By,TSPL_PURCHASE_ORDER_HEAD.Created_Date,TSPL_PURCHASE_ORDER_HEAD.Total_Invoice_Amt,TSPL_PURCHASE_ORDER_HEAD.Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PURCHASE_ORDER_HEAD.Route_No left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_PURCHASE_ORDER_HEAD.Ship_To_Location left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_PURCHASE_ORDER_HEAD.Tax_Group left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_PURCHASE_ORDER_HEAD.Terms_Code where 2=2"
    ''    Select Case NavType
    ''        Case NavigatorType.First
    ''            qry += " and TSPL_PURCHASE_ORDER_HEAD.InvPrice_Code = (select MIN(InvPrice_Code) from TSPL_PURCHASE_ORDER_HEAD)"
    ''        Case NavigatorType.Last
    ''            qry += " and TSPL_PURCHASE_ORDER_HEAD.InvPrice_Code = (select Max(InvPrice_Code) from TSPL_PURCHASE_ORDER_HEAD)"
    ''        Case NavigatorType.Next
    ''            qry += " and TSPL_PURCHASE_ORDER_HEAD.InvPrice_Code = (select Min(InvPrice_Code) from TSPL_PURCHASE_ORDER_HEAD where InvPrice_Code>'" + strPONo + "')"
    ''        Case NavigatorType.Previous
    ''            qry += " and TSPL_PURCHASE_ORDER_HEAD.InvPrice_Code = (select Max(InvPrice_Code) from TSPL_PURCHASE_ORDER_HEAD where InvPrice_Code<'" + strPONo + "')"
    ''        Case NavigatorType.Current
    ''            qry += " and TSPL_PURCHASE_ORDER_HEAD.InvPrice_Code = '" + strPONo + "'"
    ''    End Select
    ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

    ''    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    ''        obj = New clsInvoicePirceCompHead()
    ''        obj.InvPrice_Code = clsCommon.myCstr(dt.Rows(0)("InvPrice_Code"))
    ''        obj.PurchaseOrder_Date = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Date"))
    ''        obj.Loadout_Date = clsCommon.myCstr(dt.Rows(0)("Loadout_Date"))
    ''        obj.Loadout_No = clsCommon.myCstr(dt.Rows(0)("Loadout_No "))
    ''        obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
    ''        obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
    ''        obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
    ''        obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
    ''        obj.Invoice_NO = clsCommon.myCstr(dt.Rows(0)("Invoice_NO"))
    ''        obj.Invoice_Date = clsCommon.myCstr(dt.Rows(0)("Invoice_Date"))
    ''        obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
    ''        obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
    ''        obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
    ''        obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
    ''        obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
    ''        obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
    ''        obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
    ''        obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
    ''        obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
    ''        obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
    ''        obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
    ''        obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
    ''        obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
    ''        obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
    ''        obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
    ''        obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
    ''        obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
    ''        obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
    ''        obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
    ''        obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
    ''        obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
    ''        obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
    ''        obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
    ''        obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
    ''        obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
    ''        obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
    ''        obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
    ''        obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
    ''        obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
    ''        obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
    ''        obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
    ''        obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
    ''        obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
    ''        obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
    ''        obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
    ''        obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
    ''        obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
    ''        obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
    ''        obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
    ''        obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
    ''        obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
    ''        obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
    ''        obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
    ''        obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
    ''        obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
    ''        obj.Total_TPT = clsCommon.myCdbl(dt.Rows(0)("Total_TPT"))
    ''        obj.Total_Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Total_Empty_Value"))
    ''        obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
    ''        obj.Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("Invoice_Amt"))
    ''        obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
    ''        obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
    ''        obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
    ''        obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
    ''        obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
    ''        obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
    ''        obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
    ''        obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
    ''        obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))
    ''        obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))

    ''        obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
    ''        obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
    ''        obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
    ''        obj.Against_Requisition = clsCommon.myCstr(dt.Rows(0)("Against_Requisition"))
    ''        obj.Modify_By = clsCommon.myCstr(dt.Rows(0)("Modify_By"))
    ''        obj.Modify_Date = clsCommon.myCstr(dt.Rows(0)("Modify_Date"))
    ''        obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
    ''        obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
    ''        obj.Total_Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Invoice_Amt"))
    ''        qry = "SELECT  TSPL_INV_PRICE_COM_DETAIL.InvPrice_Code,TSPL_INV_PRICE_COM_DETAIL.Line_No,TSPL_INV_PRICE_COM_DETAIL.Status,TSPL_INV_PRICE_COM_DETAIL.Item_Code,TSPL_INV_PRICE_COM_DETAIL.Item_Desc,TSPL_INV_PRICE_COM_DETAIL.PurchaseOrder_Qty,TSPL_INV_PRICE_COM_DETAIL.Invoice_No,TSPL_INV_PRICE_COM_DETAIL.Balance_Qty,TSPL_INV_PRICE_COM_DETAIL.Unit_code,TSPL_INV_PRICE_COM_DETAIL.Location,TSPL_INV_PRICE_COM_DETAIL.Item_Cost,TSPL_INV_PRICE_COM_DETAIL.TAX1,TSPL_INV_PRICE_COM_DETAIL.TAX1_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX1_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX2,TSPL_INV_PRICE_COM_DETAIL.TAX2_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX2_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX3,TSPL_INV_PRICE_COM_DETAIL.TAX3_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX3_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX4,TSPL_INV_PRICE_COM_DETAIL.TAX4_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX4_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX5,TSPL_INV_PRICE_COM_DETAIL.TAX5_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX5_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX6,TSPL_INV_PRICE_COM_DETAIL.TAX6_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX6_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX7,TSPL_INV_PRICE_COM_DETAIL.TAX7_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX7_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX8,TSPL_INV_PRICE_COM_DETAIL.TAX8_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX8_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX9,TSPL_INV_PRICE_COM_DETAIL.TAX9_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX9_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX10,TSPL_INV_PRICE_COM_DETAIL.TAX10_Rate,TSPL_INV_PRICE_COM_DETAIL.TAX10_Amt,TSPL_INV_PRICE_COM_DETAIL.Net_Price,TSPL_INV_PRICE_COM_DETAIL.Disc_Per,TSPL_INV_PRICE_COM_DETAIL.Disc_Amt,TSPL_INV_PRICE_COM_DETAIL.Amt_Less_Discount,TSPL_INV_PRICE_COM_DETAIL.Total_Tax_Amt,TSPL_INV_PRICE_COM_DETAIL.Basic_Price,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_INV_PRICE_COM_DETAIL.TAX1_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX2_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX3_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX4_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX5_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX6_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX7_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX8_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX9_Base_Amt,TSPL_INV_PRICE_COM_DETAIL.TAX10_Base_Amt,(case when len(isnull(TSPL_INV_PRICE_COM_DETAIL.Invoice_No,''))>0 then (select MAX(Requisition_Qty) from TSPL_REQUISITION_DETAIL where Invoice_No=TSPL_INV_PRICE_COM_DETAIL.Invoice_No and Item_Code=TSPL_INV_PRICE_COM_DETAIL.Item_Code)  else 0 end) as OriginalReqQty,TSPL_INV_PRICE_COM_DETAIL.Loadout_No,TSPL_INV_PRICE_COM_DETAIL.Price_Code,(select sum(1) from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_INV_PRICE_COM_DETAIL.InvPrice_Code and TSPL_GRN_DETAIL.Item_Code=TSPL_INV_PRICE_COM_DETAIL.Item_Code) as IsUsedInGRN,TSPL_INV_PRICE_COM_DETAIL.MRP,TSPL_INV_PRICE_COM_DETAIL.Assessable,TSPL_INV_PRICE_COM_DETAIL.AssessableAmt FROM TSPL_INV_PRICE_COM_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INV_PRICE_COM_DETAIL.Location where TSPL_INV_PRICE_COM_DETAIL.InvPrice_Code='" + obj.InvPrice_Code + "' ORDER BY TSPL_INV_PRICE_COM_DETAIL.Line_No"
    ''        dt = New DataTable()
    ''        dt = clsDBFuncationality.GetDataTable(qry, trans)
    ''        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    ''            obj.Arr = New List(Of clsInvoicePirceCompDetail)
    ''            Dim objTr As clsInvoicePirceCompDetail
    ''            For Each dr As DataRow In dt.Rows
    ''                objTr = New clsInvoicePirceCompDetail
    ''                objTr.InvPrice_Code = clsCommon.myCstr(dr("InvPrice_Code"))
    ''                objTr.Line_No = Convert.ToInt32(clsCommon.myCdbl(dr("Line_No")))
    ''                objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
    ''                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
    ''                objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
    ''                objTr.PurchaseOrder_Qty = clsCommon.myCdbl(dr("PurchaseOrder_Qty"))
    ''                objTr.Invoice_No = clsCommon.myCstr(dr("Invoice_No"))
    ''                objTr.OriginalReqQty = clsCommon.myCdbl(dr("OriginalReqQty"))
    ''                objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
    ''                objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
    ''                objTr.Location = clsCommon.myCstr(dr("Location"))
    ''                objTr.LocationName = clsCommon.myCstr(dr("LocationName"))
    ''                objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
    ''                objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
    ''                objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
    ''                objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
    ''                objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
    ''                objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
    ''                objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
    ''                objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
    ''                objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
    ''                objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
    ''                objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
    ''                objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
    ''                objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
    ''                objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
    ''                objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
    ''                objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
    ''                objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
    ''                objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
    ''                objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
    ''                objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
    ''                objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
    ''                objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
    ''                objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
    ''                objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
    ''                objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
    ''                objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
    ''                objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
    ''                objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
    ''                objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
    ''                objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
    ''                objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
    ''                objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
    ''                objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
    ''                objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
    ''                objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
    ''                objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
    ''                objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
    ''                objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
    ''                objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
    ''                objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
    ''                objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
    ''                objTr.Net_Price = clsCommon.myCdbl(dr("Net_Price"))
    ''                objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
    ''                objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
    ''                objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
    ''                objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
    ''                objTr.Basic_Price = clsCommon.myCdbl(dr("Basic_Price"))

    ''                objTr.Loadout_No = clsCommon.myCstr(dr("Loadout_No"))
    ''                objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
    ''                objTr.IsUsedInGRN = clsCommon.myCBool(dr("IsUsedInGRN"))
    ''                objTr.MRP = clsCommon.myCdbl(dr("MRP"))
    ''                objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
    ''                objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
    ''                obj.Arr.Add(objTr)
    ''            Next
    ''        End If
    ''    End If

    ''    Return obj
    ''End Function
    Public Shared Function GetPriceComponentObject(ByVal strSaleInvoiceCode As String, ByVal Trans As SqlTransaction) As clsInvoicePriceCompHead
        Dim qry As String = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Shipment_No,TSPL_SALE_INVOICE_HEAD.Shipment_Date,TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_SALE_INVOICE_HEAD.Price_Code,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Inv_Detail_Total_Amt,TSPL_SALE_INVOICE_HEAD.Inv_Tax_Amt,TSPL_SALE_INVOICE_HEAD.Empty_Value,TSPL_SALE_INVOICE_HEAD.TPT,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.MRP_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.MRP_Amt-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount1,0) as Retailer_Price,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp1, TSPL_ITEM_PRICE_MASTER.Price_Amount1,TSPL_ITEM_PRICE_MASTER.Price_Amount1*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount1,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp2, TSPL_ITEM_PRICE_MASTER.Price_Amount2,TSPL_ITEM_PRICE_MASTER.Price_Amount2*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount2,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp3, TSPL_ITEM_PRICE_MASTER.Price_Amount3,TSPL_ITEM_PRICE_MASTER.Price_Amount3*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount3,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp4, TSPL_ITEM_PRICE_MASTER.Price_Amount4,TSPL_ITEM_PRICE_MASTER.Price_Amount4*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount4,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp5, TSPL_ITEM_PRICE_MASTER.Price_Amount5,TSPL_ITEM_PRICE_MASTER.Price_Amount5*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount5,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp6, TSPL_ITEM_PRICE_MASTER.Price_Amount6,TSPL_ITEM_PRICE_MASTER.Price_Amount6*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount6,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp7, TSPL_ITEM_PRICE_MASTER.Price_Amount7,TSPL_ITEM_PRICE_MASTER.Price_Amount7*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount7,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp8, TSPL_ITEM_PRICE_MASTER.Price_Amount8,TSPL_ITEM_PRICE_MASTER.Price_Amount8*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount8,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp9, TSPL_ITEM_PRICE_MASTER.Price_Amount9,TSPL_ITEM_PRICE_MASTER.Price_Amount9*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount9,"
        qry += " TSPL_ITEM_PRICE_MASTER.Price_Comp10, TSPL_ITEM_PRICE_MASTER.Price_Amount10,TSPL_ITEM_PRICE_MASTER.Price_Amount10*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Price_RAmount10,TSPL_SALE_INVOICE_HEAD.Tax_Group,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX1,TSPL_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_SALE_INVOICE_DETAIL.TAX1_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX1_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX2,TSPL_SALE_INVOICE_DETAIL.TAX2_Rate,TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX2_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX3,TSPL_SALE_INVOICE_DETAIL.TAX3_Rate,TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX3_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX4,TSPL_SALE_INVOICE_DETAIL.TAX4_Rate,TSPL_SALE_INVOICE_DETAIL.TAX4_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX4_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX5,TSPL_SALE_INVOICE_DETAIL.TAX5_Rate,TSPL_SALE_INVOICE_DETAIL.TAX5_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX5_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX6,TSPL_SALE_INVOICE_DETAIL.TAX6_Rate,TSPL_SALE_INVOICE_DETAIL.TAX6_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX6_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX7,TSPL_SALE_INVOICE_DETAIL.TAX7_Rate,TSPL_SALE_INVOICE_DETAIL.TAX7_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX7_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX8,TSPL_SALE_INVOICE_DETAIL.TAX8_Rate,TSPL_SALE_INVOICE_DETAIL.TAX8_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX8_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX9,TSPL_SALE_INVOICE_DETAIL.TAX9_Rate,TSPL_SALE_INVOICE_DETAIL.TAX9_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX9_Amt,"
        qry += " TSPL_SALE_INVOICE_DETAIL.TAX10,TSPL_SALE_INVOICE_DETAIL.TAX10_Rate,TSPL_SALE_INVOICE_DETAIL.TAX10_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TAX10_Amt,"
        qry += " (TSPL_SALE_INVOICE_DETAIL.MRP_Amt-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount1,0)-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount2,0)-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount3,0)-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount4,0)-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount5,0)-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount6,0)-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount7,0)-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount8,0)-isnull(TSPL_ITEM_PRICE_MASTER.Price_Amount9,0)) as Net_Price,(TSPL_SALE_INVOICE_DETAIL.Item_Tax*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as Total_Tax,"
        qry += " TSPL_ITEM_PRICE_MASTER.Item_Basic_Price as Basic_Price,TSPL_SALE_INVOICE_DETAIL.Scheme_Item,TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item,TSPL_SALE_INVOICE_DETAIL.Sampling_Item"
        qry += "  from TSPL_SALE_INVOICE_DETAIL"
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
        qry += " left outer join TSPL_ITEM_PRICE_MASTER on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_SALE_INVOICE_DETAIL.Price_code and TSPL_ITEM_PRICE_MASTER.Item_Basic_Net=TSPL_SALE_INVOICE_DETAIL.MRP_Amt and TSPL_ITEM_PRICE_MASTER.[Start_Date] =TSPL_SALE_INVOICE_DETAIL.Price_Date and TSPL_ITEM_PRICE_MASTER.Tax_group=TSPL_SALE_INVOICE_HEAD.Tax_Group"
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + strSaleInvoiceCode + "' order by TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Sale Invoice No not found for Price Component")
        End If

        Dim obj As New clsInvoicePriceCompHead
        obj.Invoice_NO = clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_No"))
        obj.Invoice_Date = clsCommon.GetPrintDate(clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_Date")), "dd/MM/yyyy")
        obj.Loadout_No = clsCommon.myCstr(dt.Rows(0)("Shipment_No"))
        obj.Loadout_Date = clsCommon.GetPrintDate(dt.Rows(0)("Shipment_Date"), "dd/MM/yyyy")
        obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
        obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
        obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
        obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
        obj.Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("Inv_Detail_Total_Amt"))
        obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Inv_Tax_Amt"))
        obj.Total_Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
        obj.Total_TPT = clsCommon.myCdbl(dt.Rows(0)("TPT"))
        obj.Total_Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Invoice_Amt"))

        obj.Arr = New List(Of clsInvoicePirceCompDetail)
        For Each dr As DataRow In dt.Rows
            Dim objtr As New clsInvoicePirceCompDetail
            objtr.Invoice_No = clsCommon.myCstr(dr("Sale_Invoice_No"))
            objtr.Loadout_No = clsCommon.myCstr(dr("Shipment_No"))
            objtr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
            objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            objtr.MRP = clsCommon.myCdbl(dr("MRP_Amt"))
            objtr.Retailer_Price = clsCommon.myCdbl(dr("Retailer_Price"))
            objtr.Price_Comp1 = clsCommon.myCstr(dr("Price_Comp1"))
            objtr.Price_Comp1_Amt = clsCommon.myCdbl(dr("Price_Amount1"))
            objtr.Price_Comp1_RAmt = clsCommon.myCdbl(dr("Price_RAmount1"))
            objtr.Price_Comp2 = clsCommon.myCstr(dr("Price_Comp2"))
            objtr.Price_Comp2_Amt = clsCommon.myCdbl(dr("Price_Amount2"))
            objtr.Price_Comp2_RAmt = clsCommon.myCdbl(dr("Price_RAmount2"))
            objtr.Price_Comp3 = clsCommon.myCstr(dr("Price_Comp3"))
            objtr.Price_Comp3_Amt = clsCommon.myCdbl(dr("Price_Amount3"))
            objtr.Price_Comp3_RAmt = clsCommon.myCdbl(dr("Price_RAmount3"))
            objtr.Price_Comp4 = clsCommon.myCstr(dr("Price_Comp4"))
            objtr.Price_Comp4_Amt = clsCommon.myCdbl(dr("Price_Amount4"))
            objtr.Price_Comp4_RAmt = clsCommon.myCdbl(dr("Price_RAmount4"))
            objtr.Price_Comp5 = clsCommon.myCstr(dr("Price_Comp5"))
            objtr.Price_Comp5_Amt = clsCommon.myCdbl(dr("Price_Amount5"))
            objtr.Price_Comp5_RAmt = clsCommon.myCdbl(dr("Price_RAmount5"))
            objtr.Price_Comp6 = clsCommon.myCstr(dr("Price_Comp6"))
            objtr.Price_Comp6_Amt = clsCommon.myCdbl(dr("Price_Amount6"))
            objtr.Price_Comp6_RAmt = clsCommon.myCdbl(dr("Price_RAmount6"))
            objtr.Price_Comp7 = clsCommon.myCstr(dr("Price_Comp7"))
            objtr.Price_Comp7_Amt = clsCommon.myCdbl(dr("Price_Amount7"))
            objtr.Price_Comp7_RAmt = clsCommon.myCdbl(dr("Price_RAmount7"))
            objtr.Price_Comp8 = clsCommon.myCstr(dr("Price_Comp8"))
            objtr.Price_Comp8_Amt = clsCommon.myCdbl(dr("Price_Amount8"))
            objtr.Price_Comp8_RAmt = clsCommon.myCdbl(dr("Price_RAmount8"))
            objtr.Price_Comp9 = clsCommon.myCstr(dr("Price_Comp9"))
            objtr.Price_Comp9_Amt = clsCommon.myCdbl(dr("Price_Amount9"))
            objtr.Price_Comp9_RAmt = clsCommon.myCdbl(dr("Price_RAmount9"))
            objtr.Price_Comp10 = clsCommon.myCstr(dr("Price_Comp10"))
            objtr.Price_Comp10_Amt = clsCommon.myCdbl(dr("Price_Amount10"))
            objtr.Price_Comp10_RAmt = clsCommon.myCdbl(dr("Price_RAmount10"))
            objtr.Tax_Group = clsCommon.myCstr(dr("Tax_Group"))
            objtr.TAX1 = clsCommon.myCstr(dr("TAX1"))
            objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
            objtr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
            objtr.TAX2 = clsCommon.myCstr(dr("TAX2"))
            objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
            objtr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
            objtr.TAX3 = clsCommon.myCstr(dr("TAX3"))
            objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
            objtr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
            objtr.TAX4 = clsCommon.myCstr(dr("TAX4"))
            objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
            objtr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
            objtr.TAX5 = clsCommon.myCstr(dr("TAX5"))
            objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
            objtr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
            objtr.TAX6 = clsCommon.myCstr(dr("TAX6"))
            objtr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
            objtr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
            objtr.TAX7 = clsCommon.myCstr(dr("TAX7"))
            objtr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
            objtr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
            objtr.TAX8 = clsCommon.myCstr(dr("TAX8"))
            objtr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
            objtr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
            objtr.TAX9 = clsCommon.myCstr(dr("TAX9"))
            objtr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
            objtr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
            objtr.TAX10 = clsCommon.myCstr(dr("TAX10"))
            objtr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
            objtr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
            objtr.Net_Price = clsCommon.myCdbl(dr("Net_Price"))
            objtr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax"))
            objtr.Basic_Price = clsCommon.myCdbl(dr("Basic_Price"))


            objtr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
            objtr.Promo_Scheme_Item = clsCommon.myCstr(dr("Promo_Scheme_Item"))
            objtr.Sampling_Item = clsCommon.myCstr(dr("Sampling_Item"))

            obj.Arr.Add(objtr)
        Next
        Return obj
    End Function
End Class

Public Class clsInvoicePirceCompDetail
#Region "Variables"
    Public Inv_Price_Code As String = Nothing
    Public Invoice_No As String = Nothing
    Public Loadout_No As String = Nothing
    Public Price_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public MRP As Double = 0
    Public Retailer_Price As Double = 0
    Public Price_Comp1 As String = Nothing
    Public Price_Comp1_Amt As Double = 0
    Public Price_Comp1_RAmt As Double = 0
    Public Price_Comp2 As String = Nothing
    Public Price_Comp2_Amt As Double = 0
    Public Price_Comp2_RAmt As Double = 0
    Public Price_Comp3 As String = Nothing
    Public Price_Comp3_Amt As Double = 0
    Public Price_Comp3_RAmt As Double = 0
    Public Price_Comp4 As String = Nothing
    Public Price_Comp4_Amt As Double = 0
    Public Price_Comp4_RAmt As Double = 0
    Public Price_Comp5 As String = Nothing
    Public Price_Comp5_Amt As Double = 0
    Public Price_Comp5_RAmt As Double = 0
    Public Price_Comp6 As String = Nothing
    Public Price_Comp6_Amt As Double = 0
    Public Price_Comp6_RAmt As Double = 0
    Public Price_Comp7 As String = Nothing
    Public Price_Comp7_Amt As Double = 0
    Public Price_Comp7_RAmt As Double = 0
    Public Price_Comp8 As String = Nothing
    Public Price_Comp8_Amt As Double = 0
    Public Price_Comp8_RAmt As Double = 0
    Public Price_Comp9 As String = Nothing
    Public Price_Comp9_Amt As Double = 0
    Public Price_Comp9_RAmt As Double = 0
    Public Price_Comp10 As String = Nothing
    Public Price_Comp10_Amt As Double = 0
    Public Price_Comp10_RAmt As Double = 0

    Public Tax_Group As String = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Net_Price As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Basic_Price As Double = 0

    Public Scheme_Item As String = Nothing
    Public Promo_Scheme_Item As String = Nothing
    Public Sampling_Item As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsInvoicePirceCompDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsInvoicePirceCompDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Inv_Price_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
                clsCommon.AddColumnsForChange(coll, "Loadout_No", obj.Loadout_No)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Price_Comp1", obj.Price_Comp1)
                clsCommon.AddColumnsForChange(coll, "Price_Comp1_Amt", obj.Price_Comp1_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp1_RAmt", obj.Price_Comp1_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp2", obj.Price_Comp2)
                clsCommon.AddColumnsForChange(coll, "Price_Comp2_Amt", obj.Price_Comp2_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp2_RAmt", obj.Price_Comp2_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp3", obj.Price_Comp3)
                clsCommon.AddColumnsForChange(coll, "Price_Comp3_Amt", obj.Price_Comp3_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp3_RAmt", obj.Price_Comp3_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp4", obj.Price_Comp4)
                clsCommon.AddColumnsForChange(coll, "Price_Comp4_Amt", obj.Price_Comp4_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp4_RAmt", obj.Price_Comp4_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp5", obj.Price_Comp5)
                clsCommon.AddColumnsForChange(coll, "Price_Comp5_Amt", obj.Price_Comp5_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp5_RAmt", obj.Price_Comp5_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp6", obj.Price_Comp6)
                clsCommon.AddColumnsForChange(coll, "Price_Comp6_Amt", obj.Price_Comp6_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp6_RAmt", obj.Price_Comp6_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp7", obj.Price_Comp7)
                clsCommon.AddColumnsForChange(coll, "Price_Comp7_Amt", obj.Price_Comp7_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp7_RAmt", obj.Price_Comp7_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp8", obj.Price_Comp8)
                clsCommon.AddColumnsForChange(coll, "Price_Comp8_Amt", obj.Price_Comp8_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp8_RAmt", obj.Price_Comp8_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp9", obj.Price_Comp9)
                clsCommon.AddColumnsForChange(coll, "Price_Comp9_Amt", obj.Price_Comp9_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp9_RAmt", obj.Price_Comp9_RAmt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp10", obj.Price_Comp10)
                clsCommon.AddColumnsForChange(coll, "Price_Comp10_Amt", obj.Price_Comp10_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_Comp10_RAmt", obj.Price_Comp10_RAmt)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Net_Price", obj.Net_Price)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Basic_Price", obj.Basic_Price)

                clsCommon.AddColumnsForChange(coll, "Scheme_Item", obj.Scheme_Item)
                clsCommon.AddColumnsForChange(coll, "Promo_Scheme_Item", obj.Promo_Scheme_Item)
                clsCommon.AddColumnsForChange(coll, "Sampling_Item", obj.Sampling_Item)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INV_PRICE_COM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
