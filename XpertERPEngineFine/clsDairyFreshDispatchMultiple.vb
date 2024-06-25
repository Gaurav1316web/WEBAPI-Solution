Imports System.Data.SqlClient

Public Class clsDairyFreshDispatchMultiple
#Region "Variables"
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public IsSampling As Integer = 0
    Public TotalCAN As Double = 0
    Public ShippedCAN As Double = 0
    Public CrateQty As Integer = 0
    Public OPKm As Double = 0
    Public CLKm As Double = 0
    Public Screen_Type As String = Nothing
    Public IsSameBillShipParty As Integer = 0
    Public Scheme_Tax_Group As String = Nothing
    Public Electronic_Ref_No As String = Nothing
    Public EWayBillDate As Date?
    Public EWayBillNo As String = Nothing
    Public GSTStatus As Boolean = False
    Public Is_Taxable As Integer = 0
    Public DO_Item_Type As String = Nothing
    Public Shift_Type As String = Nothing
    Public EmptyCharge As Double = 0
    Public FixedCharge As Double = 0
    Public Freight_Type As String = Nothing
    Public Including_Insurance As Integer = 0
    Public Insurance As String = Nothing
    Public VAT_InvoiceNo As String = Nothing
    Public VatInvoice_Type As String = Nothing
    Public Vehicle_Manual_No As String = Nothing
    Public Sub_Location_code As String = String.Empty
    Public Crate As Double = 0
    Public jaali As Double = 0
    Public Box As Double = 0
    Public GatePass_No As String = Nothing
    Public isDispatchfromDelivery As Integer = 0
    Public Against_Delivery_Code As String = Nothing
    Public isCardSale As Integer = 0
    Public Transporter_Name_Manual As String = Nothing
    Public FAT_Per As Double = 0
    Public SNF_Per As Double = 0
    Public Acidity As Double = 0
    Public Temperature As Double = 0
    Public MBRT_Hours As Double = 0
    Public OrgCustCOde As String = Nothing
    Public Is_OwnVehicle As Integer = 0
    Public Is_CustomerChanged As Integer = 0
    Public Gross_Item_Wt As Decimal = Nothing
    Public RoundOffAmount As Double = 0
    Public Advance_Approval_Reqd As Double = 0
    Public Is_Advance_Approved As Double = 0
    Public Total_Item_Weight As Double = 0
    Public Total_Item_WeightMetric As Double = 0
    Public Freight_Charges As Double = 0
    Public Delivery_Code_PS As String = Nothing
    Public Itemwise As Integer
    Public Advance_Percentage As Double = 0
    Public GR_Date As Date?
    Public RoadPermit_Date As Date?
    Public Sale_Invoice_Date As DateTime? = Nothing
    Public Supply_Date As DateTime? = Nothing
    Public Removal_Date As DateTime? = Nothing
    Public ManualVehicle As String = Nothing
    Public Total_Comm_Amt As Double = 0
    Public WayBillDate As Date?
    Public WayBillNo As String = Nothing
    Public SO_Validity As Integer = 0
    Public Commission_Apply As Integer
    Public Dispatch_date As Date?
    Public Dispatch_Terms As String = Nothing
    Public Payment_Terms As String = Nothing
    Public Dispatch_Period As Integer = 0
    Public Vehicle_Capacity As Integer = 0
    Public Road_Permit_No As String = Nothing
    Public Direct_Dispatch As Integer = 0
    Public Is_Delivered As Integer = 0
    Public Podate As DateTime? = Nothing
    Public Form_38_No As String = Nothing
    Public Cust_PO_No As String = Nothing
    Public Price_Group_Code As String = Nothing
    Public Invoice_No As String = Nothing
    Public Invoice_Type As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime? = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing  'Not a table field
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing 'Not a table field
    Public Ship_To_Location As String = Nothing
    Public Ship_To_Party As String = Nothing
    Public Ship_To_Party_Parent As String = Nothing
    Public ShipToLocationName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Amt As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Total_Amt As Double = 0
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As DateTime? = Nothing
    Public Posting_Date As DateTime? = Nothing
    Public Carrier As String = Nothing
    Public VehicleNo As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public AlternateVehicle As String = Nothing
    Public GRNo As String = Nothing
    Public GENo As String = Nothing
    Public GEDate As Date? = Nothing
    Public Add_Charge_Code1 As String = Nothing
    Public Add_Charge_Name1 As String = Nothing
    Public Add_Charge_Amt1 As Double = 0
    Public Add_Charge_Code2 As String = Nothing
    Public Add_Charge_Name2 As String = Nothing
    Public Add_Charge_Amt2 As Double = 0
    Public Add_Charge_Code3 As String = Nothing
    Public Add_Charge_Name3 As String = Nothing
    Public Add_Charge_Amt3 As Double = 0
    Public Add_Charge_Code4 As String = Nothing
    Public Add_Charge_Name4 As String = Nothing
    Public Add_Charge_Amt4 As Double = 0
    Public Add_Charge_Code5 As String = Nothing
    Public Add_Charge_Name5 As String = Nothing
    Public Add_Charge_Amt5 As Double = 0
    Public Add_Charge_Code6 As String = Nothing
    Public Add_Charge_Name6 As String = Nothing
    Public Add_Charge_Amt6 As Double = 0
    Public Add_Charge_Code7 As String = Nothing
    Public Add_Charge_Name7 As String = Nothing
    Public Add_Charge_Amt7 As Double = 0
    Public Add_Charge_Code8 As String = Nothing
    Public Add_Charge_Name8 As String = Nothing
    Public Add_Charge_Amt8 As Double = 0
    Public Add_Charge_Code9 As String = Nothing
    Public Add_Charge_Name9 As String = Nothing
    Public Add_Charge_Amt9 As Double = 0
    Public Add_Charge_Code10 As String = Nothing
    Public Add_Charge_Name10 As String = Nothing
    Public Add_Charge_Amt10 As Double = 0
    Public Total_Add_Charge As Double = 0
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Challan_No As String = Nothing
    Public Challan_Date As DateTime? = Nothing
    Public Inv_No As String = Nothing
    Public Inv_Date As DateTime? = Nothing
    Public Against_Sales_Order As String = Nothing
    Public Is_Internal As Boolean = False
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Trans_Type As String = Nothing

    Public Is_Create_Auto_Invoice As Boolean = False
    Public Sale_Invoice_No As String = Nothing
    Public Is_Create_Auto_Receipt As Boolean = False
    Public Against_POS As String = Nothing

    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Arr As List(Of clsDairyFreshDispatchMultipleDetails) = Nothing
    ' Public ArrChkList As List(Of clsPSShipmentChecklistDetail) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public PROJECT_ID As String = Nothing

    Public Price_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public HeadDisc_Per As Double = 0
    Public HeadDisc_Amt As Double = 0
    Public TotCashDiscAmt As Double = 0
    Public HeadDisc_PerAmt As Double = 0
    Public Mannual_Invoice_No As Integer = 0
    Public InvoiceManualNowithPrefix As String = Nothing
    Public Transport_Id As String = Nothing
    Public Transporter_Name As String
    Public Item_Tax_Type As Integer = 0
    Public Print_Discount_Amt As Double = 0
    Public Nine_NR_No As String = Nothing

    Public CashCustomer As String = Nothing
    Public allowSMSforSalePerson As Boolean = False
    Public Manual_Driver_Name As String = Nothing
    Public Manual_Salesman_Name As String = Nothing
    Public IsReplacement As Integer = 0
    Public Invoice_No_ForReplacement As String = String.Empty
    Public Customer_Complaint_No As String = String.Empty
    Public Freight_Distance As Integer = 0
    Public Distributor_Commission_TotalAmt As Decimal = 0
    Public Transporter_Commission_TotalAmt As Decimal = 0
    Public Security_TotalAmt As Decimal = 0
    Public IsCreditCustomer As Boolean = False
    Public ParentDocNo As String = ""


    Public ArrDemand As List(Of clsDairyFreshDispatchDemand) = Nothing
#End Region

    Public Shared Function GetData(ByVal RouteNo As String, ByVal SupplyDate As DateTime, ByVal ShiftType As String, ByVal LocationCode As String, ByVal ItemType As String) As clsDairyFreshDispatchMultiple
        Return GetData(RouteNo, SupplyDate, ShiftType, LocationCode, ItemType, Nothing)
    End Function
    Public Shared Function GetData(ByVal RouteNo As String, ByVal SupplyDate As DateTime, ByVal ShiftType As String, ByVal LocationCode As String, ByVal ItemType As String, ByVal trans As SqlTransaction) As clsDairyFreshDispatchMultiple
        Dim obj As New clsDairyFreshDispatchMultiple
        Try
            Dim strQry1 As String = "select TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code  from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER
left join TSPL_DISTRIBUTOR_ROUTE on TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code=TSPL_DISTRIBUTOR_ROUTE.Code
                where TSPL_DISTRIBUTOR_ROUTE.Status=1 and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code in(
select top 1 TSPL_DISTRIBUTOR_ROUTE.Code from TSPL_DISTRIBUTOR_ROUTE
left join TSPL_DISTRIBUTOR_ROUTE_CUSTOMER on TSPL_DISTRIBUTOR_ROUTE.Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code
where TSPL_DISTRIBUTOR_ROUTE.Start_Date<='" & clsCommon.GetPrintDate(SupplyDate) & "' and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No='" & RouteNo & "' and 2=(Case when TSPL_DISTRIBUTOR_ROUTE.End_Date is null then 2 else (Case when TSPL_DISTRIBUTOR_ROUTE.End_Date>='" & clsCommon.GetPrintDate(SupplyDate) & "' then 2 else 3 end) end) order by Start_Date desc)
 and TSPL_DISTRIBUTOR_ROUTE.Start_Date<='" & clsCommon.GetPrintDate(SupplyDate) & "' and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No='" & RouteNo & "' and 2=(Case when TSPL_DISTRIBUTOR_ROUTE.End_Date is null then 2 else (Case when TSPL_DISTRIBUTOR_ROUTE.End_Date>='" & clsCommon.GetPrintDate(SupplyDate) & "' then 2 else 3 end) end)"
            obj.Customer_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry1))
            obj.Supply_Date = clsCommon.GetPrintDate(SupplyDate)
            obj.Document_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans))
            obj.Item_Type = ItemType
            obj.Route_No = RouteNo
            Dim Qry As String = "select TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_VEHICLE_MASTER.Number from TSPL_VEHICLE_MASTER
                            left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id
                            where TSPL_ROUTE_MASTER.Route_No='" + RouteNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
                obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("Number"))
            End If
            obj.DO_Item_Type = ItemType
            obj.Shift_Type = ShiftType


            obj.ArrDemand = clsDairyFreshDispatchDemand.GetDispatchDemandData(RouteNo, SupplyDate, ShiftType, LocationCode, ItemType, trans)
            obj.Arr = clsDairyFreshDispatchMultipleDetails.GethDispatchMultipleDetailData(obj.ArrDemand, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


        Return obj
    End Function
    'Private Sub SetTax(ByVal Item_Code As String, ByVal SupplyDate As DateTime, ByVal ItemType As String, ByVal LocationCode As String, ByVal CustomerCode As String, ByVal trans As SqlTransaction)
    '    Dim TaxGroup As String = ""
    '    GSTStatus = clsERPFuncationality.GetGSTStatus(SupplyDate)
    '    If GSTStatus = False OrElse (clsCommon.CompairString(ItemType, "0") <> CompairStringResult.Equal AndAlso GSTStatus) Then
    '        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, trans)) Then
    '            If clsCommon.myLen(LocationCode) > 0 Then
    '                Dim strTaxType As String = clsLocationWiseTax.TaxType(LocationCode, CustomerCode, "S", SupplyDate, trans)
    '                If GSTStatus = True AndAlso clsCommon.CompairString(strTaxType, "L") = CompairStringResult.Equal Then
    '                    TaxGroup = clsItemWiseTaxAuthority.GetTaxGroupItemWise("L", "S", SupplyDate, Item_Code, trans)
    '                Else
    '                    'txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "S", txtDate.Value)
    '                    TaxGroup = clsItemWiseTaxAuthority.GetTaxGroupItemWise("I", "S", SupplyDate, Item_Code, trans)
    '                End If
    '            Else
    '                TaxGroup = clsLocationWiseTax.GetDefaultTaxGroup(LocationCode, CustomerCode, "S", SupplyDate, trans)
    '            End If
    '        Else
    '            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "S", txtDate.Value, trans)
    '            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, trans)
    '        End If
    '    Else
    '        If clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") = CompairStringResult.Equal Then
    '            txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, txtBillToLocation.Value, txtVendorNo.Value, "S", txtDate.Value, trans)
    '            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, trans)
    '        End If
    '    End If
    '    SetTaxDetails(trans)
    'End Sub
End Class
Public Class clsDairyFreshDispatchMultipleDetails
#Region "Variables"
    Public CAN As Double = 0
    Public Structure_Code As String = Nothing
    Public ItemwiseTaxCode As String = ""
    Public VS_CashSchemeCode As String = String.Empty
    Public VS_Cash_Amt As Double = 0
    Public VS_ltrInCrate As Double = 0
    Public Crate As Double = 0
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public GatePass_No As String = Nothing
    Public Delivery_Code As String = ""
    Public Alter_UnitQty As Double = 0
    Public Rate_UnitQty As Double = 0
    Public Is_CustomerChanged As Integer = 0
    Public Customer_Code As String = Nothing
    Public Total_Item_WeightMetric As Double = 0
    Public Alternate_UOM As String = Nothing
    Public RATE_UOM As String = Nothing
    Public Delivery_Code_PS As String = Nothing
    Public Item_Group As String = Nothing
    Public TAX_PAID As String = Nothing
    Public OrgUnit_code As String = ""
    Public Commission_Party As String = Nothing
    Public Commission_Rate As Double = 0
    Public Commission_Amt As Double = 0
    Public Amt_Less_Commission As Double = 0
    Public Sampling As Integer = 0

    Public Document_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing 'Not a Table Field
    Public Bar_Code As String = Nothing
    Public Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Free_Qty As Double = 0
    Public Order_Code As String = Nothing
    Public Unit_code As String = Nothing '
    Public Location As String = Nothing '
    Public LocationName As String = Nothing 'Not a Table Field
    Public Item_Cost As Double = 0
    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Amount As Double = 0
    Public Disc_Per As Double = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public Status As Integer = 0
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0
    Public Is_Mannual_Amt As Integer = Nothing
    'Public Unit_Cogs As Double = 0
    Public SRNTax_Group As String = Nothing 'Not a Table Field

    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing


    Public Scheme_Applicable As String = Nothing
    Public Scheme_Code As String = Nothing
    Public Scheme_Item As String = Nothing
    Public Item_Tax As Double = 0
    Public Total_MRP_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Disc_Amt As Double = 0
    Public Cust_Discount As Double = 0
    Public Total_Cust_Discount As Double = 0
    Public Price_Amount1 As Double = 0
    Public Price_Amount2 As Double = 0
    Public Price_Amount3 As Double = 0
    Public Price_Amount4 As Double = 0
    Public Price_Amount5 As Double = 0
    Public Price_Amount6 As Double = 0
    Public Price_Amount7 As Double = 0
    Public Price_Amount8 As Double = 0
    Public Price_Amount9 As Double = 0
    Public Price_Amount10 As Double = 0
    Public ActualRate As Double = 0
    Public Cust_DiscountQty As Double = 0
    Public Price_code As String = Nothing
    Public Abatement_Per As Double = 0
    Public Abatement_Amt As Double = 0
    Public FOC_Item As Double = 0
    Public Price_Date As String = Nothing

    Public Item_Weight As Double = 0
    Public Conv_Factor As Double = 0
    Public TotalItem_Weight As Double = 0
    Public Markup_On As String = Nothing
    Public Markup_Percent As Double = 0
    Public Landing_Cost As Double = 0
    Public HeadDiscAmt As Double = 0
    Public CustDiscPer As Double = 0
    Public CasdDiscScheme_Code As String = Nothing
    Public Purchase_Cost As Double = 0
    Public OrgRate As Double = 0
    Public PrincipleCode As String = Nothing
    Public PrincipleDesc As String = Nothing
    Public vendor_code As String = Nothing
    Public vendor_desc As String = Nothing
    Public Bin_No As String = Nothing
    Public HeadDiscPer As Double = 0
    Public HeadDiscPerAmt As Double = 0
    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Decimal = Nothing
    Public Scheme_Item_UOM As String = Nothing
    Public Cash_Scheme_Code As String = Nothing
    Public Cash_Scheme_Type As String = Nothing
    Public Cash_Scheme_Pers As Decimal = Nothing
    Public Cash_Scheme_Amount As Decimal = Nothing
    Public OrgCustCOde As String = Nothing
    Public Disc_Scheme_Code As String = Nothing
    Public Disc_Scheme_Type As String = Nothing
    Public Disc_Scheme_Pers As Double = 0
    Public Disc_Scheme_Amount As Double = 0
    Public Sub_Location_code As String = String.Empty
    Public Booking_User_Code As String = ""
    Public Distributor_Retailer_Code As String = ""
    Public Distributor_Retailer_Name As String = ""
    Public Distributor_Retailer_Email As String = ""
    Public Distributor_Commission_PKID As String = ""
    Public Distributor_Commission_Rate As Decimal = 0
    Public Distributor_Commission_RateWithTax As Decimal = 0
    Public Distributor_Commission_Amt As Decimal = 0
    Public Transporter_Commission_Rate As Decimal = 0
    Public Transporter_Commission_Amt As Decimal = 0
    Public Security_Rate As Decimal = 0
    Public Security_Amt As Decimal = 0
    Public Transporter As String = ""

#End Region
    Public Shared Function GethDispatchMultipleDetailData(ByVal Arr As List(Of clsDairyFreshDispatchDemand), ByVal trans As SqlTransaction) As List(Of clsDairyFreshDispatchMultipleDetails)

        Dim obj As New List(Of clsDairyFreshDispatchMultipleDetails)


        Try

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                Dim myobj As New List(Of clsMargeDistributorItems)
                Dim myMargeDisitems As New clsMargeDistributorItems

                For Each items As clsDairyFreshDispatchDemand In Arr
                    Dim strKey As String = items.Item_Code + "~" + items.Unit_Code

                    Dim objMDI As New clsMargeDistributorItems
                    objMDI.keys = strKey
                    objMDI.Item_Code = items.Item_Code
                    objMDI.Unit_Code = items.Unit_Code
                    objMDI.Qty = items.Qty
                    myobj.Add(objMDI)

                Next

                Dim GroupbyItems = From item In myobj
                                   Group By item.keys Into Group
                                   Select strKey = keys, TotalQty = Group.Sum(Function(x) x.Qty)
                If GroupbyItems IsNot Nothing AndAlso GroupbyItems.Count > 0 Then
                    For Each item In GroupbyItems
                        Dim objTr As New clsDairyFreshDispatchMultipleDetails
                        objTr.Item_Code = item.strKey.Split("~")(0).ToString()
                        objTr.Unit_code = item.strKey.Split("~")(1).ToString
                        objTr.Qty = item.TotalQty
                        obj.Add(objTr)
                    Next
                End If

            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return obj

    End Function

End Class
Public Class clsDairyFreshDispatchDemand
#Region "Variable"
    Public DOCUMENT_CODE As String
    Public Booth_Code As String
    Public Booking_TR_Code As String
    Public Item_Code As String
    Public Unit_Code As String
    Public Qty As Decimal
    Public Trip_No As Decimal
    Public Commission_Amt As Decimal
    Public Security_Amt As Decimal
#End Region

    Public Shared Function GetDispatchDemandData(ByVal RouteNo As String, ByVal SupplyDate As DateTime, ByVal ShiftType As String, ByVal LocationCode As String, ByVal ItemType As String, ByVal trans As SqlTransaction) As List(Of clsDairyFreshDispatchDemand)
        Dim obj As New List(Of clsDairyFreshDispatchDemand)
        Try
            Dim strQry As String = " select 
TSPL_BOOKING_DETAIL.Against_DemandBooking_TR_Code as TR_CODE,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_BOOKING_DETAIL.Booking_Qty as DemandQty,TSPL_BOOKING_DETAIL.Booking_Qty as Qty,TSPL_BOOKING_DETAIL.Unit_code,TSPL_BOOKING_MATSER.trip_No,0 as Commission_Amt,0 as Security_Amt 
from TSPL_BOOKING_MATSER
left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code 
 left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code 
where TSPL_BOOKING_MATSER.GatePass_Type='" & ShiftType & "'  and Convert(date,TSPL_BOOKING_MATSER.Document_Date,103)='" & clsCommon.GetPrintDate(SupplyDate) & "'
   and TSPL_BOOKING_MATSER.Posted=1
and TSPL_BOOKING_DETAIL.Route_No='" & RouteNo & "' and TSPL_BOOKING_MATSER.Location_Code='" & LocationCode & "' 
  and TSPL_ITEM_MASTER.IsTaxable='" & ItemType & "'  and TSPL_BOOKING_DETAIL.Against_DemandBooking_TR_Code is not null  and not exists(select 1 from TSPL_SD_SHIPMENT_BOOKING_DETAIL where TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code=TSPL_BOOKING_DETAIL.Against_DemandBooking_TR_Code) order by  TSPL_BOOKING_DETAIL.Against_DemandBooking_TR_Code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows
                    Dim objTr As New clsDairyFreshDispatchDemand
                    objTr.Booth_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Booking_TR_Code = clsCommon.myCstr(dr("TR_CODE"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Qty = clsCommon.myCstr(dr("Qty"))
                    objTr.Trip_No = clsCommon.myCstr(dr("trip_No"))
                    objTr.Commission_Amt = clsCommon.myCstr(dr("Commission_Amt"))
                    objTr.Security_Amt = clsCommon.myCstr(dr("Security_Amt"))
                    obj.Add(objTr)
                Next
            Else
                Throw New Exception("Data Not Found! (GetDispatchDemandData)")

            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return obj

    End Function

End Class

Public Class clsMargeDistributorItems
#Region "Variable"
    Public keys As String
    Public Item_Code As String
    Public Unit_Code As String
    Public Qty As String
#End Region
End Class


