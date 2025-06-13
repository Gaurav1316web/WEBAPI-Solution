Imports System.Data.SqlClient

Public Class clsMultipleInvoice

    Public Shared Function GetShipmentDetail(ByVal lstDoc As List(Of String), ByVal trans As SqlTransaction) As clsPSShipmentHead
        Dim obj As New clsPSShipmentHead
        Try
            Dim strQry As String = "select * from TSPL_SD_SHIPMENT_HEAD where Document_Code in(" + clsCommon.GetMulcallString(lstDoc) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows

                    obj.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    obj.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                    obj.Customer_Code = clsCommon.myCstr(dr("Customer_Code"))
                    obj.Status = ERPTransactionStatus.Pending
                    obj.On_Hold = clsCommon.myCBool(dr("On_Hold"))
                    obj.Ref_No = clsCommon.myCstr(dr("Ref_No"))
                    obj.Description = clsCommon.myCstr(dr("Description"))
                    obj.Remarks = clsCommon.myCstr(dr("Remarks"))
                    obj.Tax_Group = clsCommon.myCstr(dr("Tax_Group"))
                    obj.Bill_To_Location = clsCommon.myCstr(dr("Bill_To_Location"))
                    obj.Ship_To_Location = clsCommon.myCstr(dr("Ship_To_Location"))
                    obj.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    obj.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    obj.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    obj.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    obj.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    obj.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    obj.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    obj.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    obj.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    obj.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    obj.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    obj.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    obj.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    obj.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    obj.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    obj.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    obj.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    obj.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    obj.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    obj.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    obj.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    obj.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    obj.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    obj.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    obj.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    obj.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    obj.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    obj.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    obj.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    obj.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    obj.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    obj.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    obj.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    obj.Discount_Base += clsCommon.myCdbl(dr("Discount_Base"))
                    obj.Discount_Amt += clsCommon.myCdbl(dr("Discount_Amt"))
                    obj.Amount_Less_Discount += clsCommon.myCdbl(dr("Amount_Less_Discount"))
                    obj.Total_Tax_Amt += clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    obj.Total_Amt += clsCommon.myCdbl(dr("Total_Amt"))
                    obj.Comments = clsCommon.myCstr(dr("Comments"))
                    obj.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                    obj.Terms_Code = clsCommon.myCstr(dr("Terms_Code"))
                    If dr("Due_Date") IsNot DBNull.Value Then
                        obj.Due_Date = clsCommon.myCstr(dr("Due_Date"))
                    End If

                    obj.Posting_Date = clsCommon.myCDate(dr("Posting_Date"))
                    obj.Carrier = clsCommon.myCstr(dr("Carrier"))
                    obj.VehicleNo = clsCommon.myCstr(dr("VehicleNo"))
                    obj.GRNo = clsCommon.myCstr(dr("GRNo"))
                    obj.GENo = clsCommon.myCstr(dr("GENo"))
                    'obj.GEDate = clsCommon.myCDate(dr("GEDate"))
                    obj.Add_Charge_Code1 = clsCommon.myCstr(dr("Add_Charge_Code1"))
                    obj.Add_Charge_Name1 = clsCommon.myCstr(dr("Add_Charge_Name1"))
                    obj.Add_Charge_Amt1 = clsCommon.myCdbl(dr("Add_Charge_Amt1"))
                    obj.Add_Charge_Code2 = clsCommon.myCstr(dr("Add_Charge_Code2"))
                    obj.Add_Charge_Name2 = clsCommon.myCstr(dr("Add_Charge_Name2"))
                    obj.Add_Charge_Amt2 = clsCommon.myCdbl(dr("Add_Charge_Amt2"))
                    obj.Add_Charge_Code3 = clsCommon.myCstr(dr("Add_Charge_Code3"))
                    obj.Add_Charge_Name3 = clsCommon.myCstr(dr("Add_Charge_Name3"))
                    obj.Add_Charge_Amt3 = clsCommon.myCdbl(dr("Add_Charge_Amt3"))
                    obj.Add_Charge_Code4 = clsCommon.myCstr(dr("Add_Charge_Code4"))
                    obj.Add_Charge_Name4 = clsCommon.myCstr(dr("Add_Charge_Name4"))
                    obj.Add_Charge_Amt4 = clsCommon.myCdbl(dr("Add_Charge_Amt4"))
                    obj.Add_Charge_Code5 = clsCommon.myCstr(dr("Add_Charge_Code5"))
                    obj.Add_Charge_Name5 = clsCommon.myCstr(dr("Add_Charge_Name5"))
                    obj.Add_Charge_Amt5 = clsCommon.myCdbl(dr("Add_Charge_Amt5"))
                    obj.Add_Charge_Code6 = clsCommon.myCstr(dr("Add_Charge_Code6"))
                    obj.Add_Charge_Name6 = clsCommon.myCstr(dr("Add_Charge_Name6"))
                    obj.Add_Charge_Amt6 = clsCommon.myCdbl(dr("Add_Charge_Amt6"))
                    obj.Add_Charge_Code7 = clsCommon.myCstr(dr("Add_Charge_Code7"))
                    obj.Add_Charge_Name7 = clsCommon.myCstr(dr("Add_Charge_Name7"))
                    obj.Add_Charge_Amt7 = clsCommon.myCdbl(dr("Add_Charge_Amt7"))
                    obj.Add_Charge_Code8 = clsCommon.myCstr(dr("Add_Charge_Code8"))
                    obj.Add_Charge_Name8 = clsCommon.myCstr(dr("Add_Charge_Name8"))
                    obj.Add_Charge_Amt8 = clsCommon.myCdbl(dr("Add_Charge_Amt8"))
                    obj.Add_Charge_Code9 = clsCommon.myCstr(dr("Add_Charge_Code9"))
                    obj.Add_Charge_Name9 = clsCommon.myCstr(dr("Add_Charge_Name9"))
                    obj.Add_Charge_Amt9 = clsCommon.myCdbl(dr("Add_Charge_Amt9"))
                    obj.Add_Charge_Code10 = clsCommon.myCstr(dr("Add_Charge_Code10"))
                    obj.Add_Charge_Name10 = clsCommon.myCstr(dr("Add_Charge_Name10"))
                    obj.Add_Charge_Amt10 = clsCommon.myCdbl(dr("Add_Charge_Amt10"))
                    obj.Total_Add_Charge = clsCommon.myCdbl(dr("Total_Add_Charge"))
                    obj.Dept = clsCommon.myCstr(dr("Dept"))
                    obj.Dept_Desc = clsCommon.myCstr(dr("Dept_Desc"))
                    obj.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    obj.Challan_No = clsCommon.myCstr(dr("Challan_No"))
                    obj.Challan_Date = clsCommon.myCDate(dr("Challan_Date"))
                    'obj.Inv_No = clsCommon.myCstr(dr("Inv_No"))
                    'obj.Inv_Date = clsCommon.myCDate(dr("Inv_Date"))
                    obj.Against_Sales_Order = clsCommon.myCstr(dr("Against_Sales_Order"))
                    'obj.Tax_Calculation_Type = clsCommon.myCstr(dr("Tax_Calculation_Type"))
                    'obj.Is_Internal = clsCommon.myCstr(dr("Is_Internal"))
                    obj.Is_Create_Auto_Invoice = clsCommon.myCBool(dr("Is_Create_Auto_Invoice"))
                    'obj.Sale_Invoice_No = clsCommon.myCstr(dr("Sale_Invoice_No"))
                    obj.Is_Create_Auto_Receipt = clsCommon.myCBool(dr("Is_Create_Auto_Receipt"))
                    obj.Against_POS = clsCommon.myCstr(dr("Against_POS"))
                    obj.Salesman_Code = clsCommon.myCstr(dr("Salesman_Code"))
                    obj.Salesman_Name = clsCommon.myCstr(dr("Salesman_Name"))
                    obj.CURRENCY_CODE = clsCommon.myCstr(dr("CURRENCY_CODE"))
                    obj.ConvRate = clsCommon.myCdbl(dr("ConvRate"))
                    'obj.ApplicableFrom = clsCommon.myCstr(dr("ApplicableFrom"))
                    obj.PROJECT_ID = clsCommon.myCstr(dr("PROJECT_ID"))

                    obj.Price_Code = clsCommon.myCstr(dr("Price_code"))
                    obj.Route_No = clsCommon.myCstr(dr("Route_No"))
                    obj.Route_Desc = clsCommon.myCstr(dr("Route_Desc"))
                    obj.HeadDisc_Per = clsCommon.myCdbl(dr("HeadDisc_Per"))
                    obj.HeadDisc_Amt = clsCommon.myCdbl(dr("HeadDisc_Amt"))
                    obj.TotCashDiscAmt = clsCommon.myCdbl(dr("TotCashDiscAmt"))
                    obj.Invoice_Type = clsCommon.myCstr(dr("Invoice_Type"))
                    obj.Price_Group_Code = clsCommon.myCstr(dr("Price_Group_Code"))
                    obj.Vehicle_Code = clsCommon.myCstr(dr("Vehicle_Code"))
                    obj.Mannual_Invoice_No = clsCommon.myCdbl(dr("Mannual_Invoice_No"))
                    obj.Cust_PO_No = clsCommon.myCstr(dr("Cust_PO_No"))
                    obj.Form_38_No = clsCommon.myCstr(dr("Form_38_No"))

                    obj.HeadDisc_PerAmt = clsCommon.myCdbl(dr("HeadDisc_PerAmt"))

                    obj.Vehicle_Capacity = clsCommon.myCdbl(dr("Vehicle_Capacity"))
                    obj.Road_Permit_No = clsCommon.myCstr(dr("Road_Permit_No"))
                    obj.Transporter_Name = clsCommon.myCstr(dr("Transporter_Name"))

                    obj.CrateQty = clsCommon.myCdbl(dr("CrateQty"))
                    obj.Against_Delivery_Code = clsCommon.myCstr(dr("Against_Delivery_Code"))
                    obj.Is_Delivered = clsCommon.myCdbl(dr("Is_Delivered"))
                    obj.SO_Validity = clsCommon.myCstr(dr("SO_Validity"))
                    obj.Commission_Apply = clsCommon.myCdbl(dr("Commission_Apply"))
                    obj.Dispatch_date = clsCommon.myCDate(dr("Dispatch_date"))
                    obj.Dispatch_Terms = clsCommon.myCstr(dr("Dispatch_Terms"))
                    obj.Payment_Terms = clsCommon.myCstr(dr("Payment_Terms"))
                    obj.Dispatch_Period = clsCommon.myCdbl(dr("Dispatch_Period"))
                    obj.Trans_Type = clsCommon.myCstr(dr("Trans_Type"))
                    'obj.WayBillNo = clsCommon.myCstr(dr("WayBillNo"))
                    'obj.WayBillDate = clsCommon.myCstr(dr("WayBillDate"))
                    obj.Direct_Dispatch = clsCommon.myCdbl(dr("Direct_Dispatch"))
                    obj.Total_Comm_Amt = clsCommon.myCdbl(dr("Total_Comm_Amt"))
                    obj.Delivery_Code_PS = clsCommon.myCstr(dr("Delivery_Code_PS"))
                    obj.Itemwise = clsCommon.myCdbl(dr("Itemwise"))
                    obj.Advance_Percentage = clsCommon.myCdbl(dr("Advance_Percentage"))

                    'obj.GR_Date = clsCommon.myCDate(dr("GR_Date"))
                    'obj.RoadPermit_Date = clsCommon.myCDate(dr("RoadPermit_Date"))
                    'obj.Removal_Date = clsCommon.myCstr(dr("Removal_Date"))
                    obj.Transport_Id = clsCommon.myCstr(dr("Transport_Id"))
                    obj.Total_Item_Weight = clsCommon.myCdbl(dr("Total_Item_Weight"))
                    obj.Total_Item_WeightMetric = clsCommon.myCdbl(dr("Total_Item_WeightMetric"))
                    obj.Freight_Charges = clsCommon.myCdbl(dr("Freight_Charges"))
                    obj.Advance_Approval_Reqd = clsCommon.myCdbl(dr("Advance_Approval_Reqd"))
                    obj.Is_Advance_Approved = clsCommon.myCdbl(dr("Is_Advance_Approved"))
                    obj.RoundOffAmount = clsCommon.myCdbl(dr("RoundOffAmount"))

                    obj.AlternateVehicle = clsCommon.myCstr(dr("AlternateVehicle"))
                    obj.ManualVehicle = clsCommon.myCstr(dr("ManualVehicle"))
                    'obj.Gross_Item_Wt = clsCommon.myCstr(dr("Gross_Item_Wt"))
                    'obj.Is_CustomerChanged = clsCommon.myCstr(dr("Is_CustomerChanged"))
                    obj.Is_OwnVehicle = clsCommon.myCdbl(dr("Is_OwnVehicle"))
                    obj.Transporter_Name_Manual = clsCommon.myCstr(dr("Transporter_Name_Manual"))
                    obj.GatePass_No = clsCommon.myCstr(dr("GatePass_No"))
                    obj.Crate = clsCommon.myCdbl(dr("Crate"))
                    obj.jaali = clsCommon.myCdbl(dr("jaali"))
                    obj.Box = clsCommon.myCdbl(dr("Box"))

                    obj.VAT_InvoiceNo = clsCommon.myCstr(dr("VAT_InvoiceNo"))
                    obj.Print_Discount_Amt = clsCommon.myCdbl(dr("Print_Discount_Amt"))
                    obj.Including_Insurance = clsCommon.myCdbl(dr("Including_Insurance"))
                    obj.Freight_Type = clsCommon.myCstr(dr("Freight_Type"))
                    obj.EmptyCharge = clsCommon.myCdbl(dr("EmptyCharge"))
                    obj.FixedCharge = clsCommon.myCdbl(dr("FixedCharge"))
                    obj.Nine_NR_No = clsCommon.myCstr(dr("Nine_NR_No"))
                    'obj.VatInvoice_Type = clsCommon.myCstr(dr("VatInvoice_Type"))
                    obj.Vehicle_Manual_No = clsCommon.myCstr(dr("Vehicle_Manual_No"))

                    obj.DO_Item_Type = clsCommon.myCstr(dr("DO_Item_Type"))
                    'obj.EWayBillNo = clsCommon.myCstr(dr("EWayBillNo"))
                    'obj.EWayBillDate = clsCommon.myCstr(dr("EWayBillDate"))
                    'obj.Electronic_Ref_No = clsCommon.myCstr(dr("Electronic_Ref_No"))
                    obj.Is_Taxable = clsCommon.myCstr(dr("Is_Taxable"))
                    obj.Ship_To_Party = clsCommon.myCstr(dr("Ship_To_Party"))
                    obj.Ship_To_Party_Parent = clsCommon.myCstr(dr("Ship_To_Party_Parent"))
                    obj.Scheme_Tax_Group = clsCommon.myCstr(dr("Scheme_Tax_Group"))
                    'obj.IsSameBillShipParty = clsCommon.myCstr(dr("IsSameBillShipParty"))
                    obj.Screen_Type = clsCommon.myCstr(dr("Screen_Type"))
                    obj.Insurance = clsCommon.myCstr(dr("Insurance"))
                    obj.OPKm += clsCommon.myCdbl(dr("OPKm"))
                    obj.CLKm += clsCommon.myCdbl(dr("CLKm"))

                    obj.TotalCAN += clsCommon.myCdbl(dr("TotalCAN"))
                    obj.ShippedCAN += clsCommon.myCdbl(dr("ShippedCAN"))
                    obj.IsSampling = clsCommon.myCdbl(dr("IsSampling"))

                    obj.Manual_Driver_Name = clsCommon.myCstr(dr("Manual_Driver_Name"))
                    obj.Manual_Salesman_Name = clsCommon.myCstr(dr("Manual_Salesman_Name"))
                    'obj.IsReplacement = clsCommon.myCstr(dr("IsReplacement"))
                    obj.Invoice_No_ForReplacement = clsCommon.myCstr(dr("Invoice_No_ForReplacement"))
                    obj.Customer_Complaint_No = clsCommon.myCstr(dr("Customer_Complaint_No"))
                    obj.ActualTCSBaseAmount = clsCommon.myCdbl(dr("ActualTCSBaseAmount"))
                    'obj.ChakngedTCSBaseAmount = clsCommon.myCstr(dr("ChangedTCSBaseAmount"))
                    obj.Sub_Location_code = clsCommon.myCstr(dr("Sub_Location_code"))
                    obj.Freight_Distance = clsCommon.myCdbl(dr("Freight_Distance"))


                    obj.Distributor_Commission_TotalAmt += clsCommon.myCdbl(dr("Distributor_Commission_TotalAmt"))
                    obj.Shift_Type = clsCommon.myCstr(dr("Shift_Type"))
                    obj.Security_TotalAmt += clsCommon.myCdbl(dr("Security_TotalAmt"))
                    obj.Supply_Date = clsCommon.myCDate(dr("Supply_Date"))
                    obj.Transporter_Commission_TotalAmt += clsCommon.myCdbl(dr("Transporter_Commission_TotalAmt"))
                    obj.FAT_Per = clsCommon.myCdbl(dr("FAT_Per"))
                    obj.SNF_Per = clsCommon.myCdbl(dr("SNF_Per"))
                    obj.Acidity = clsCommon.myCdbl(dr("Acidity"))
                    obj.Temperature = clsCommon.myCdbl(dr("Temperature"))
                    obj.MBRT_Hours = clsCommon.myCdbl(dr("MBRT_Hours"))
                    obj.ParentDocNo = clsCommon.myCstr(dr("ParentDocNo"))
                    obj.ReceiverName = clsCommon.myCstr(dr("ReceiverName"))
                    'obj.Against_Demand_No = clsCommon.myCstr(dr("Against_Demand_No"))
                    obj.Against_Booking_No = clsCommon.myCstr(dr("Against_Booking_No"))
                    obj.BoothSecurity_TotalAmt += clsCommon.myCdbl(dr("BoothSecurity_TotalAmt"))
                    obj.Vehicle_Type = clsCommon.myCstr(dr("Vehicle_Type"))

                    'obj.IsEwaybill = clsCommon.myCstr(dr("IsEwaybill"))


                Next
            End If
            strQry = "Select * From TSPL_SD_SHIPMENT_DETAIL Where Document_Code In(" + clsCommon.GetMulcallString(lstDoc) + ")"
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsPSShipmentHeadDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsPSShipmentHeadDetail
                    objtr.Sampling = clsCommon.myCdbl(dr("Sampling"))
                    objtr.Crate = clsCommon.myCdbl(dr("Crate"))
                    objtr.CAN = clsCommon.myCdbl(dr("CAN"))
                    objtr.Structure_Code = clsCommon.myCstr(dr("Structure_Code"))
                    objtr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    objtr.Alter_UnitQty = clsCommon.myCdbl(dr("Alter_UnitQty"))
                    objtr.Cash_Scheme_Code = clsCommon.myCstr(dr("Cash_Scheme_Code"))
                    objtr.Cash_Scheme_Type = clsCommon.myCstr(dr("Cash_Scheme_Type"))
                    objtr.Cash_Scheme_Pers = clsCommon.myCdbl(dr("Cash_Scheme_Pers"))
                    objtr.Cash_Scheme_Amount = clsCommon.myCdbl(dr("Cash_Scheme_Amount"))
                    objtr.Scheme_Type = clsCommon.myCstr(dr("Scheme_Type"))
                    objtr.Scheme_Qty = clsCommon.myCdbl(dr("Scheme_Qty"))
                    objtr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme_Item_UOM"))
                    objtr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme_Item_Code"))
                    objtr.VS_CashSchemeCode = clsCommon.myCstr(dr("VS_CashSchemeCode"))
                    objtr.VS_Cash_Amt = clsCommon.myCdbl(dr("VS_Cash_Amt"))
                    objtr.VS_ltrInCrate = clsCommon.myCdbl(dr("VS_ltrInCrate"))


                    objtr.RATE_UOM = clsCommon.myCstr(dr("RATE_UOM"))
                    objtr.Alternate_UOM = clsCommon.myCstr(dr("Alternate_UOM"))
                    objtr.PrincipleCode = clsCommon.myCstr(dr("PrincipleCode"))
                    objtr.PrincipleDesc = clsCommon.myCstr(dr("PrincipleDesc"))
                    objtr.vendor_code = clsCommon.myCstr(dr("vendor_code"))
                    objtr.vendor_desc = clsCommon.myCstr(dr("vendor_desc"))
                    objtr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objtr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objtr.Status = Convert.ToInt32(clsCommon.myCstr(dr("Status")))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    'objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objtr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objtr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))
                    ''objtr.Shipment_Code = clscommon.mycstr(dr("Shipment_Code"))
                    objtr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objtr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objtr.Location = clsCommon.myCstr(dr("Location"))
                    'objtr.LocationName = clsCommon.myCstr(dr("LocationName"))
                    objtr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objtr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objtr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objtr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objtr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objtr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objtr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objtr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objtr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objtr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objtr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objtr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objtr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objtr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objtr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objtr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objtr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objtr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objtr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objtr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objtr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objtr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objtr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objtr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objtr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objtr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objtr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objtr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objtr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objtr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objtr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objtr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objtr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objtr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objtr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objtr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objtr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objtr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objtr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objtr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
                    objtr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objtr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))


                    objtr.Is_Mannual_Amt = clsCommon.myCdbl(dr("Is_Mannual_Amt"))

                    objtr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objtr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    objtr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
                    objtr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    'If dr("MFG_Date") IsNot Nothing Then
                    '    objtr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    'End If
                    'If dr("Expiry_Date") IsNot Nothing Then
                    '    objtr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                    'End If
                    objtr.Specification = clsCommon.myCstr(dr("Specification"))
                    objtr.Remarks = clsCommon.myCstr(dr("Remarks"))

                    objtr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                    objtr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objtr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                    objtr.Item_Tax = clsCommon.myCdbl(dr("Item_Tax"))
                    objtr.Total_MRP_Amt = clsCommon.myCdbl(dr("Total_MRP_Amt"))
                    objtr.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                    objtr.Total_Disc_Amt = clsCommon.myCdbl(dr("Total_Disc_Amt"))
                    objtr.Cust_Discount = clsCommon.myCdbl(dr("Cust_Discount"))
                    objtr.Total_Cust_Discount = clsCommon.myCdbl(dr("Total_Cust_Discount"))
                    objtr.ActualRate = clsCommon.myCdbl(dr("ActualRate"))
                    objtr.Cust_DiscountQty = clsCommon.myCdbl(dr("Cust_DiscountQty"))
                    objtr.Price_code = clsCommon.myCstr(dr("Price_code"))
                    objtr.Price_Date = clsCommon.myCstr(dr("Price_Date"))
                    objtr.Abatement_Per = clsCommon.myCdbl(dr("Abatement_Per"))
                    objtr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))
                    objtr.FOC_Item = clsCommon.myCdbl(dr("FOC_Item"))
                    objtr.Markup_On = clsCommon.myCstr(dr("Markup_On"))
                    objtr.Markup_Percent = clsCommon.myCdbl(dr("Markup_Percent"))
                    objtr.Landing_Cost = clsCommon.myCdbl(dr("Landing_Cost"))
                    objtr.HeadDiscAmt = clsCommon.myCdbl(dr("HeadDiscAmt"))
                    objtr.HeadDiscPer = clsCommon.myCdbl(dr("HeadDiscPer"))
                    objtr.HeadDiscPerAmt = clsCommon.myCdbl(dr("HeadDiscPerAmt"))
                    objtr.CustDiscPer = clsCommon.myCstr(dr("CustDiscPer"))
                    objtr.CasdDiscScheme_Code = clsCommon.myCstr(dr("CasdDiscScheme_Code"))

                    objtr.Item_Weight = clsCommon.myCdbl(dr("Item_Weight"))
                    objtr.TotalItem_Weight = clsCommon.myCdbl(dr("TotalItem_Weight"))
                    objtr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                    objtr.Purchase_Cost = clsCommon.myCdbl(dr("Purchase_Cost"))
                    objtr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))

                    objtr.Price_Amount1 = clsCommon.myCdbl(dr("Price_Amount1"))
                    objtr.Price_Amount2 = clsCommon.myCdbl(dr("Price_Amount2"))
                    objtr.Price_Amount3 = clsCommon.myCdbl(dr("Price_Amount3"))
                    objtr.Price_Amount4 = clsCommon.myCdbl(dr("Price_Amount4"))
                    objtr.Price_Amount5 = clsCommon.myCdbl(dr("Price_Amount5"))
                    objtr.Price_Amount6 = clsCommon.myCdbl(dr("Price_Amount6"))
                    objtr.Price_Amount7 = clsCommon.myCdbl(dr("Price_Amount7"))
                    objtr.Price_Amount8 = clsCommon.myCdbl(dr("Price_Amount8"))
                    objtr.Price_Amount9 = clsCommon.myCdbl(dr("Price_Amount9"))
                    objtr.Price_Amount10 = clsCommon.myCdbl(dr("Price_Amount10"))

                    objtr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objtr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objtr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objtr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objtr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objtr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objtr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objtr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objtr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objtr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objtr.Distributor_Commission_PKID = clsCommon.myCstr(dr("Distributor_Commission_PKID"))
                    objtr.Distributor_Commission_Rate = clsCommon.myCdbl(dr("Distributor_Commission_Rate"))
                    objtr.Distributor_Commission_RateWithTax = clsCommon.myCdbl(dr("Distributor_Commission_RateWithTax"))
                    objtr.Distributor_Commission_Amt = clsCommon.myCdbl(dr("Distributor_Commission_Amt"))
                    objtr.Transporter_Commission_Rate = clsCommon.myCdbl(dr("Transporter_Commission_Rate"))
                    objtr.Transporter_Commission_Amt = clsCommon.myCdbl(dr("Transporter_Commission_Amt"))
                    objtr.Security_Rate = clsCommon.myCdbl(dr("Security_Rate"))
                    objtr.Security_Amt = clsCommon.myCdbl(dr("Security_Amt"))


                    objtr.Commission_Rate = clsCommon.myCdbl(dr("Commission_Rate"))
                    objtr.Commission_Party = clsCommon.myCstr(dr("Commission_Party"))
                    objtr.Commission_Amt = clsCommon.myCdbl(dr("Commission_Amt"))
                    objtr.Amt_Less_Commission = clsCommon.myCdbl(dr("Amt_Less_Commission"))
                    'objTr.Delivery_Code = objShipmentDetail.Delivery_Code
                    obj.Arr.Add(objtr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function ConvertShipmentToSaleInvoice(ByVal objShipment As clsPSShipmentHead, ByVal IsDairyModule As Boolean, ByVal trans As SqlTransaction) As clsPSInvoiceHead 'sanjay
        Dim obj As New clsPSInvoiceHead()
        obj = New clsPSInvoiceHead()
        Dim Taxable As Integer = 0
        Try
            obj.Nine_NR_No = objShipment.Nine_NR_No
            If clsCommon.CompairString(objShipment.DO_Item_Type, "NT") = CompairStringResult.Equal Then
                Taxable = 0
                obj.Is_Taxable = 0
            ElseIf clsCommon.CompairString(objShipment.DO_Item_Type, "T") = CompairStringResult.Equal Then
                Taxable = 1
                obj.Is_Taxable = 1
            End If
            obj.Trans_type = objShipment.Trans_Type
            obj.TotalCAN = objShipment.TotalCAN
            obj.IsSampling = objShipment.IsSampling
            obj.ShippedCAN = objShipment.ShippedCAN
            obj.CrateQty = objShipment.CrateQty
            obj.OPKm = objShipment.OPKm
            obj.CLKm = objShipment.CLKm
            obj.Screen_Type = objShipment.Screen_Type
            obj.IsSameBillShipParty = objShipment.IsSameBillShipParty
            obj.Ship_To_Party = objShipment.Ship_To_Party
            obj.Including_Insurance = objShipment.Including_Insurance
            obj.Crate = objShipment.Crate
            obj.jaali = objShipment.jaali
            obj.Box = objShipment.Box
            obj.isCardSale = objShipment.isCardSale
            obj.IsEwaybill = objShipment.IsEwaybill
            obj.podate = objShipment.Document_Date
            obj.RoundOffAmount = objShipment.RoundOffAmount
            obj.Total_Comm_Amt = objShipment.Total_Comm_Amt
            obj.Invoice_Type = objShipment.Invoice_Type
            obj.Document_Date = clsCommon.GETSERVERDATE(trans)
            obj.Customer_Code = objShipment.Customer_Code
            obj.Customer_Name = objShipment.Customer_Name
            obj.Status = ERPTransactionStatus.Pending
            obj.On_Hold = IIf(objShipment.On_Hold = 1, True, False)
            obj.Is_Internal = IIf(objShipment.Is_Internal = 1, True, False)
            obj.Ref_No = objShipment.Ref_No
            obj.Description = objShipment.Description
            obj.Remarks = objShipment.Remarks
            obj.Bill_To_Location = objShipment.Bill_To_Location
            obj.Ship_To_Location = objShipment.Ship_To_Location
            obj.Sub_Location_code = objShipment.Sub_Location_code
            obj.Tax_Group = objShipment.Tax_Group
            obj.ActualTCSBaseAmount = objShipment.ActualTCSBaseAmount
            obj.ChangedTCSBaseAmount = objShipment.ChangedTCSBaseAmount
            obj.TAX1 = objShipment.TAX1
            obj.TAX1_Rate = objShipment.TAX1_Rate
            obj.TAX1_Base_Amt = objShipment.TAX1_Base_Amt
            obj.TAX1_Amt = objShipment.TAX1_Amt
            obj.TAX2 = objShipment.TAX2
            obj.TAX2_Rate = objShipment.TAX2_Rate
            obj.TAX2_Base_Amt = objShipment.TAX2_Base_Amt
            obj.TAX2_Amt = objShipment.TAX2_Amt
            obj.TAX3 = objShipment.TAX3
            obj.TAX3_Base_Amt = objShipment.TAX3_Base_Amt
            obj.TAX3_Rate = objShipment.TAX3_Rate
            obj.TAX3_Amt = objShipment.TAX3_Amt
            obj.TAX4 = objShipment.TAX4
            obj.TAX4_Rate = objShipment.TAX4_Rate
            obj.TAX4_Base_Amt = objShipment.TAX4_Base_Amt
            obj.TAX4_Amt = objShipment.TAX4_Amt
            obj.TAX5 = objShipment.TAX5
            obj.TAX5_Rate = objShipment.TAX5_Rate
            obj.TAX5_Base_Amt = objShipment.TAX5_Base_Amt
            obj.TAX5_Amt = objShipment.TAX5_Amt
            obj.TAX6 = objShipment.TAX6
            obj.TAX6_Rate = objShipment.TAX6_Rate
            obj.TAX6_Base_Amt = objShipment.TAX6_Base_Amt
            obj.TAX6_Amt = objShipment.TAX6_Amt
            obj.TAX7 = objShipment.TAX7
            obj.TAX7_Rate = objShipment.TAX7_Rate
            obj.TAX7_Base_Amt = objShipment.TAX7_Base_Amt
            obj.TAX7_Amt = objShipment.TAX7_Amt
            obj.TAX8 = objShipment.TAX8
            obj.TAX8_Rate = objShipment.TAX8_Rate
            obj.TAX8_Base_Amt = objShipment.TAX8_Base_Amt
            obj.TAX8_Amt = objShipment.TAX8_Amt
            obj.TAX9 = objShipment.TAX9
            obj.TAX9_Rate = objShipment.TAX9_Rate
            obj.TAX9_Base_Amt = objShipment.TAX9_Base_Amt
            obj.TAX9_Amt = objShipment.TAX9_Amt
            obj.TAX10 = objShipment.TAX10
            obj.TAX10_Rate = objShipment.TAX10_Rate
            obj.TAX10_Base_Amt = objShipment.TAX10_Base_Amt
            obj.TAX10_Amt = objShipment.TAX10_Amt
            obj.Total_Tax_Amt = objShipment.Total_Tax_Amt
            obj.Discount_Base = objShipment.Discount_Base
            obj.Discount_Amt = objShipment.Discount_Amt
            obj.Amount_Less_Discount = objShipment.Amount_Less_Discount
            obj.Total_Amt = objShipment.Total_Amt
            obj.Comments = objShipment.Comments
            obj.Comp_Code = objShipment.Comp_Code
            obj.Terms_Code = objShipment.Terms_Code
            obj.Due_Date = objShipment.Due_Date
            obj.BillToLocationName = objShipment.BillToLocationName
            obj.ShipToLocationName = objShipment.ShipToLocationName
            obj.TaxGroupName = objShipment.TaxGroupName
            obj.TermsName = objShipment.TermsName
            obj.PROJECT_ID = objShipment.PROJECT_ID
            obj.Route_No = objShipment.Route_No
            obj.Route_Desc = objShipment.Route_Desc
            obj.Price_Code = objShipment.Price_Code
            obj.HeadDisc_Per = objShipment.HeadDisc_Per
            obj.HeadDisc_Amt = objShipment.HeadDisc_Amt
            obj.HeadDisc_PerAmt = objShipment.HeadDisc_PerAmt
            obj.TotCashDiscAmt = objShipment.TotCashDiscAmt
            obj.Cust_PO_No = objShipment.Cust_PO_No
            obj.podate = objShipment.Podate
            obj.VAT_InvoiceNo = objShipment.VAT_InvoiceNo

            If objShipment.Posting_Date IsNot Nothing Then
                obj.Posting_Date = objShipment.Posting_Date
            End If

            obj.Manual_Driver_Name = objShipment.Manual_Driver_Name
            obj.Manual_Salesman_Name = objShipment.Manual_Salesman_Name

            obj.Salesman_Code = objShipment.Salesman_Code
            obj.Salesman_Name = objShipment.Salesman_Name

            obj.Challan_No = objShipment.Challan_No
            obj.Carrier = objShipment.Carrier
            obj.Vehicle_Code = objShipment.Vehicle_Code
            obj.VehicleNo = objShipment.VehicleNo

            obj.Transport_Code = objShipment.Transport_Id
            obj.Transporter_Name = objShipment.Transporter_Name
            obj.Freight_Distance = objShipment.Freight_Distance

            obj.GRNo = objShipment.GRNo
            obj.GENo = objShipment.GENo
            If objShipment.GEDate IsNot Nothing Then
                obj.GEDate = objShipment.GEDate
            End If




            obj.Dept = objShipment.Dept
            obj.Dept_Desc = objShipment.Dept_Desc
            obj.Item_Type = objShipment.Item_Type

            obj.Against_Shipment_No = objShipment.Document_Code


            obj.Add_Charge_Code1 = objShipment.Add_Charge_Code1
            obj.Add_Charge_Name1 = objShipment.Add_Charge_Name1
            obj.Add_Charge_Amt1 = objShipment.Add_Charge_Amt1

            obj.Add_Charge_Code2 = objShipment.Add_Charge_Code2
            obj.Add_Charge_Name2 = objShipment.Add_Charge_Name2
            obj.Add_Charge_Amt2 = objShipment.Add_Charge_Amt2

            obj.Add_Charge_Code3 = objShipment.Add_Charge_Code3
            obj.Add_Charge_Name3 = objShipment.Add_Charge_Name3
            obj.Add_Charge_Amt3 = objShipment.Add_Charge_Amt3

            obj.Add_Charge_Code4 = objShipment.Add_Charge_Code4
            obj.Add_Charge_Name4 = objShipment.Add_Charge_Name4
            obj.Add_Charge_Amt4 = objShipment.Add_Charge_Amt4

            obj.Add_Charge_Code5 = objShipment.Add_Charge_Code5
            obj.Add_Charge_Name5 = objShipment.Add_Charge_Name5
            obj.Add_Charge_Amt5 = objShipment.Add_Charge_Amt5

            obj.Add_Charge_Code6 = objShipment.Add_Charge_Code6
            obj.Add_Charge_Name6 = objShipment.Add_Charge_Name6
            obj.Add_Charge_Amt6 = objShipment.Add_Charge_Amt6

            obj.Add_Charge_Code7 = objShipment.Add_Charge_Code7
            obj.Add_Charge_Name7 = objShipment.Add_Charge_Name7
            obj.Add_Charge_Amt7 = objShipment.Add_Charge_Amt7

            obj.Add_Charge_Code8 = objShipment.Add_Charge_Code8
            obj.Add_Charge_Name8 = objShipment.Add_Charge_Name8
            obj.Add_Charge_Amt8 = objShipment.Add_Charge_Amt8

            obj.Add_Charge_Code9 = objShipment.Add_Charge_Code9
            obj.Add_Charge_Name9 = objShipment.Add_Charge_Name9
            obj.Add_Charge_Amt9 = objShipment.Add_Charge_Amt9

            obj.Add_Charge_Code10 = objShipment.Add_Charge_Code10
            obj.Add_Charge_Name10 = objShipment.Add_Charge_Name10
            obj.Add_Charge_Amt10 = objShipment.Add_Charge_Amt10

            obj.Total_Add_Charge = objShipment.Total_Add_Charge
            obj.Inv_No = objShipment.Inv_No
            If clsCommon.myLen(objShipment.Challan_Date) <= 0 Then
                obj.Challan_Date = ""
            Else
                obj.Challan_Date = clsCommon.GetPrintDate(objShipment.Challan_Date, "dd/MMM/yyyy")
            End If

            If clsCommon.myLen(objShipment.Inv_Date) <= 0 Then
                obj.Inv_Date = clsCommon.GETSERVERDATE(trans)
            Else
                obj.Inv_Date = clsCommon.GetPrintDate(objShipment.Inv_Date, "dd/MMM/yyyy")
            End If
            obj.SO_Validity = objShipment.SO_Validity
            obj.Commission_Apply = objShipment.Commission_Apply
            obj.Dispatch_date = objShipment.Dispatch_date
            obj.Vehicle_Capacity = objShipment.Vehicle_Capacity
            obj.Dispatch_Terms = objShipment.Dispatch_Terms
            obj.Payment_Terms = objShipment.Payment_Terms
            obj.Dispatch_Period = objShipment.Dispatch_Period
            obj.WayBillNo = objShipment.WayBillNo
            obj.WayBillDate = objShipment.WayBillDate
            obj.Tax_Calculation_Type = IIf(objShipment.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Is_Create_Auto_Receipt = objShipment.Is_Create_Auto_Receipt

            obj.Mannual_Document_Code = objShipment.Mannual_Invoice_No
            obj.InvoiceManualNowithPrefix = objShipment.InvoiceManualNowithPrefix
            If Taxable = 1 Then
                obj.Scheme_Tax_Group = objShipment.Scheme_Tax_Group
                obj.Tax_Group = objShipment.Scheme_Tax_Group
                'obj.RoundOffAmount = 0
            ElseIf Taxable = 0 Then
                obj.Scheme_Tax_Group = objShipment.Scheme_Tax_Group
                obj.Total_Tax_Amt = 0
                obj.TAX1_Rate = 0
                obj.TAX2_Rate = 0
                obj.TAX3_Rate = 0
                obj.TAX4_Rate = 0
                obj.TAX5_Rate = 0
                obj.TAX6_Rate = 0
                obj.TAX7_Rate = 0
                obj.TAX8_Rate = 0
                obj.TAX9_Rate = 0
                obj.TAX10_Rate = 0
                'obj.RoundOffAmount = 0

            End If
            obj.IsMultipleInvoice = 1
            obj.Discount_Amt = 0
            obj.Amount_Less_Discount = 0
            obj.Total_Amt = 0
            obj.Discount_Base = 0
            obj.Distributor_Commission_TotalAmt = 0
            obj.Security_TotalAmt = 0
            obj.Transporter_Commission_TotalAmt = 0



            '-------------------------------------------------------------------
            If (objShipment.Arr IsNot Nothing AndAlso objShipment.Arr.Count > 0) Then
                obj.Arr = New List(Of clsPSInvoiceHeadDetail)
                Dim objTr As clsPSInvoiceHeadDetail
                For Each objShipmentDetail As clsPSShipmentHeadDetail In objShipment.Arr
                    objTr = New clsPSInvoiceHeadDetail
                    'Dim IsTaxable As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & objShipmentDetail.Item_Code & "'"))
                    'If (SingleInvoice = True OrElse (SingleInvoice = False AndAlso IIf(Taxable = 0, IsTaxable = 0, IsTaxable = 1))) Then
                    objTr.Sampling = objShipmentDetail.Sampling
                    objTr.Crate = objShipmentDetail.Crate
                    objTr.CAN = objShipmentDetail.CAN
                    objTr.Structure_Code = objShipmentDetail.Structure_Code
                    objTr.ItemwiseTaxCode = objShipmentDetail.ItemwiseTaxCode
                    objTr.Alter_UnitQty = objShipmentDetail.Alter_UnitQty
                    objTr.Cash_Scheme_Code = objShipmentDetail.Cash_Scheme_Code
                    objTr.Cash_Scheme_Type = objShipmentDetail.Cash_Scheme_Type
                    objTr.Cash_Scheme_Pers = objShipmentDetail.Cash_Scheme_Pers
                    objTr.Cash_Scheme_Amount = objShipmentDetail.Cash_Scheme_Amount
                    objTr.Scheme_Type = objShipmentDetail.Scheme_Type
                    objTr.Scheme_Qty = objShipmentDetail.Scheme_Qty
                    objTr.Scheme_Item_UOM = objShipmentDetail.Scheme_Item_UOM
                    objTr.Scheme_Item_Code = objShipmentDetail.Scheme_Item_Code
                    objTr.VS_CashSchemeCode = objShipmentDetail.VS_CashSchemeCode
                    objTr.VS_Cash_Amt = objShipmentDetail.VS_Cash_Amt
                    objTr.VS_ltrInCrate = objShipmentDetail.VS_ltrInCrate


                    objTr.RATE_UOM = objShipmentDetail.RATE_UOM
                    objTr.Alternate_UOM = objShipmentDetail.Alternate_UOM
                    objTr.PrincipleCode = objShipmentDetail.PrincipleCode
                    objTr.PrincipleDesc = objShipmentDetail.PrincipleDesc
                    objTr.vendor_code = objShipmentDetail.vendor_code
                    objTr.vendor_desc = objShipmentDetail.vendor_desc
                    objTr.Document_Code = objShipmentDetail.Document_Code
                    objTr.Row_Type = objShipmentDetail.Row_Type
                    objTr.Line_No = objShipmentDetail.Line_No
                    objTr.Status = Convert.ToInt32(objShipmentDetail.Status)
                    objTr.Item_Code = objShipmentDetail.Item_Code
                    objTr.Item_Desc = objShipmentDetail.Item_Desc
                    objTr.Qty = objShipmentDetail.Qty
                    objTr.Free_Qty = objShipmentDetail.Free_Qty
                    objTr.Shipment_Code = objShipmentDetail.Document_Code
                    objTr.Balance_Qty = objShipmentDetail.Balance_Qty
                    objTr.Unit_code = objShipmentDetail.Unit_code
                    objTr.Location = objShipmentDetail.Location
                    objTr.LocationName = objShipmentDetail.LocationName
                    objTr.Item_Cost = objShipmentDetail.Item_Cost
                    objTr.TAX1 = objShipmentDetail.TAX1
                    objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
                    objTr.TAX1_Rate = objShipmentDetail.TAX1_Rate
                    objTr.TAX1_Amt = objShipmentDetail.TAX1_Amt
                    objTr.TAX2 = objShipmentDetail.TAX2
                    objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
                    objTr.TAX2_Rate = objShipmentDetail.TAX2_Rate
                    objTr.TAX2_Amt = objShipmentDetail.TAX2_Amt
                    objTr.TAX3 = objShipmentDetail.TAX3
                    objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
                    objTr.TAX3_Rate = objShipmentDetail.TAX3_Rate
                    objTr.TAX3_Amt = objShipmentDetail.TAX3_Amt
                    objTr.TAX4 = objShipmentDetail.TAX4
                    objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
                    objTr.TAX4_Rate = objShipmentDetail.TAX4_Rate
                    objTr.TAX4_Amt = objShipmentDetail.TAX4_Amt
                    objTr.TAX5 = objShipmentDetail.TAX5
                    objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
                    objTr.TAX5_Rate = objShipmentDetail.TAX5_Rate
                    objTr.TAX5_Amt = objShipmentDetail.TAX5_Amt
                    objTr.TAX6 = objShipmentDetail.TAX6
                    objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
                    objTr.TAX6_Rate = objShipmentDetail.TAX6_Rate
                    objTr.TAX6_Amt = objShipmentDetail.TAX6_Amt
                    objTr.TAX7 = objShipmentDetail.TAX7
                    objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
                    objTr.TAX7_Rate = objShipmentDetail.TAX7_Rate
                    objTr.TAX7_Amt = objShipmentDetail.TAX7_Amt
                    objTr.TAX8 = objShipmentDetail.TAX8
                    objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
                    objTr.TAX8_Rate = objShipmentDetail.TAX8_Rate
                    objTr.TAX8_Amt = objShipmentDetail.TAX8_Amt
                    objTr.TAX9 = objShipmentDetail.TAX9
                    objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
                    objTr.TAX9_Rate = objShipmentDetail.TAX9_Rate
                    objTr.TAX9_Amt = objShipmentDetail.TAX9_Amt
                    objTr.TAX10 = objShipmentDetail.TAX10
                    objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt
                    objTr.TAX10_Rate = objShipmentDetail.TAX10_Rate
                    objTr.TAX10_Amt = objShipmentDetail.TAX10_Amt
                    objTr.Amount = objShipmentDetail.Amount
                    objTr.Disc_Per = objShipmentDetail.Disc_Per
                    objTr.Disc_Amt = objShipmentDetail.Disc_Amt
                    objTr.Amt_Less_Discount = objShipmentDetail.Amt_Less_Discount
                    objTr.Total_Tax_Amt = objShipmentDetail.Total_Tax_Amt
                    objTr.Item_Net_Amt = objShipmentDetail.Item_Net_Amt


                    objTr.Is_Mannual_Amt = objShipmentDetail.Is_Mannual_Amt

                    objTr.MRP = objShipmentDetail.MRP
                    objTr.Assessable = objShipmentDetail.Assessable
                    objTr.AssessableAmt = objShipmentDetail.AssessableAmt
                    objTr.Batch_No = objShipmentDetail.Batch_No
                    'If objShipmentDetail.MFG_Date IsNot Nothing Then
                    '    objTr.MFG_Date = objShipmentDetail.MFG_Date
                    'End If
                    'If objShipmentDetail.Expiry_Date IsNot Nothing Then
                    '    objTr.Expiry_Date = objShipmentDetail.Expiry_Date
                    'End If
                    objTr.Specification = objShipmentDetail.Specification
                    objTr.Remarks = objShipmentDetail.Remarks

                    objTr.Scheme_Applicable = objShipmentDetail.Scheme_Applicable
                    objTr.Scheme_Code = objShipmentDetail.Scheme_Code
                    objTr.Scheme_Item = objShipmentDetail.Scheme_Item
                    objTr.Item_Tax = objShipmentDetail.Item_Tax
                    objTr.Total_MRP_Amt = objShipmentDetail.Total_MRP_Amt
                    objTr.Total_Basic_Amt = objShipmentDetail.Total_Basic_Amt
                    objTr.Total_Disc_Amt = objShipmentDetail.Total_Disc_Amt
                    objTr.Cust_Discount = objShipmentDetail.Cust_Discount
                    objTr.Total_Cust_Discount = objShipmentDetail.Total_Cust_Discount
                    objTr.ActualRate = objShipmentDetail.ActualRate
                    objTr.Cust_DiscountQty = objShipmentDetail.Cust_DiscountQty
                    objTr.Price_code = objShipmentDetail.Price_code
                    objTr.Price_Date = objShipmentDetail.Price_Date
                    objTr.Abatement_Per = objShipmentDetail.Abatement_Per
                    objTr.Abatement_Amt = objShipmentDetail.Abatement_Amt
                    objTr.FOC_Item = objShipmentDetail.FOC_Item
                    objTr.Markup_On = objShipmentDetail.Markup_On
                    objTr.Markup_Percent = objShipmentDetail.Markup_Percent
                    objTr.Landing_Cost = objShipmentDetail.Landing_Cost
                    objTr.HeadDiscAmt = objShipmentDetail.HeadDiscAmt
                    objTr.HeadDiscPer = objShipmentDetail.HeadDiscPer
                    objTr.HeadDiscPerAmt = objShipmentDetail.HeadDiscPerAmt
                    objTr.CustDiscPer = objShipmentDetail.CustDiscPer
                    objTr.CasdDiscScheme_Code = objShipmentDetail.CasdDiscScheme_Code

                    objTr.Item_Weight = objShipmentDetail.Item_Weight
                    objTr.TotalItem_Weight = objShipmentDetail.TotalItem_Weight
                    objTr.Conv_Factor = objShipmentDetail.Conv_Factor
                    objTr.Purchase_Cost = objShipmentDetail.Purchase_Cost
                    objTr.OrgRate = objShipmentDetail.OrgRate

                    objTr.Price_Amount1 = objShipmentDetail.Price_Amount1
                    objTr.Price_Amount2 = objShipmentDetail.Price_Amount2
                    objTr.Price_Amount3 = objShipmentDetail.Price_Amount3
                    objTr.Price_Amount4 = objShipmentDetail.Price_Amount4
                    objTr.Price_Amount5 = objShipmentDetail.Price_Amount5
                    objTr.Price_Amount6 = objShipmentDetail.Price_Amount6
                    objTr.Price_Amount7 = objShipmentDetail.Price_Amount7
                    objTr.Price_Amount8 = objShipmentDetail.Price_Amount8
                    objTr.Price_Amount9 = objShipmentDetail.Price_Amount9
                    objTr.Price_Amount10 = objShipmentDetail.Price_Amount10

                    objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
                    objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
                    objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
                    objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
                    objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
                    objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
                    objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
                    objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
                    objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
                    objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt
                    objTr.Distributor_Commission_PKID = objShipmentDetail.Distributor_Commission_PKID
                    objTr.Distributor_Commission_Rate = objShipmentDetail.Distributor_Commission_Rate
                    objTr.Distributor_Commission_RateWithTax = objShipmentDetail.Distributor_Commission_RateWithTax
                    objTr.Distributor_Commission_Amt = objShipmentDetail.Distributor_Commission_Amt
                    objTr.Transporter_Commission_Rate = objShipmentDetail.Transporter_Commission_Rate
                    objTr.Transporter_Commission_Amt = objShipmentDetail.Transporter_Commission_Amt
                    objTr.Security_Rate = objShipmentDetail.Security_Rate
                    objTr.Security_Amt = objShipmentDetail.Security_Amt


                    objTr.Commission_Rate = objShipmentDetail.Commission_Rate
                    objTr.Commission_Party = objShipmentDetail.Commission_Party
                    objTr.Commission_Amt = objShipmentDetail.Commission_Amt
                    objTr.Amt_Less_Commission = objShipmentDetail.Amt_Less_Commission
                    'objTr.Delivery_Code = objShipmentDetail.Delivery_Code

                    'If objShipmentDetail.FOC_Item = 1 Then
                    '    obj.Discount_Amt += objShipmentDetail.Amt_Less_Discount
                    'Else
                    obj.Discount_Base += objShipmentDetail.Amount
                        obj.Discount_Amt += objShipmentDetail.Disc_Amt
                        obj.Amount_Less_Discount += objShipmentDetail.Amt_Less_Discount
                        obj.Total_Tax_Amt += objShipmentDetail.Total_Tax_Amt
                        obj.Total_Amt += objShipmentDetail.Item_Net_Amt
                    'End If
                    'obj.Discount_Base += objShipmentDetail.Amt_Less_Discount



                    obj.TAX1 = objShipmentDetail.TAX1
                    obj.TAX1_Base_Amt += objShipmentDetail.TAX1_Base_Amt
                    obj.TAX1_Rate = objShipmentDetail.TAX1_Rate
                    obj.TAX1_Amt += objShipmentDetail.TAX1_Amt
                    obj.TAX2 = objShipmentDetail.TAX2
                    obj.TAX2_Base_Amt += objShipmentDetail.TAX2_Base_Amt
                    obj.TAX2_Rate = objShipmentDetail.TAX2_Rate
                    obj.TAX2_Amt += objShipmentDetail.TAX2_Amt
                    obj.TAX3 = objShipmentDetail.TAX3
                    obj.TAX3_Base_Amt += objShipmentDetail.TAX3_Base_Amt
                    obj.TAX3_Rate = objShipmentDetail.TAX3_Rate
                    obj.TAX3_Amt += objShipmentDetail.TAX3_Amt
                    obj.TAX4 = objShipmentDetail.TAX4
                    obj.TAX4_Base_Amt += objShipmentDetail.TAX4_Base_Amt
                    obj.TAX4_Rate = objShipmentDetail.TAX4_Rate
                    obj.TAX4_Amt += objShipmentDetail.TAX4_Amt
                    obj.TAX5 = objShipmentDetail.TAX5
                    obj.TAX5_Base_Amt += objShipmentDetail.TAX5_Base_Amt
                    obj.TAX5_Rate = objShipmentDetail.TAX5_Rate
                    obj.TAX5_Amt += objShipmentDetail.TAX5_Amt
                    obj.TAX6 = objShipmentDetail.TAX6
                    obj.TAX6_Base_Amt += objShipmentDetail.TAX6_Base_Amt
                    obj.TAX6_Rate = objShipmentDetail.TAX6_Rate
                    obj.TAX6_Amt += objShipmentDetail.TAX6_Amt
                    obj.TAX7 = objShipmentDetail.TAX7
                    obj.TAX7_Base_Amt += objShipmentDetail.TAX7_Base_Amt
                    obj.TAX7_Rate = objShipmentDetail.TAX7_Rate
                    obj.TAX7_Amt += objShipmentDetail.TAX7_Amt
                    obj.TAX8 = objShipmentDetail.TAX8
                    obj.TAX8_Base_Amt += objShipmentDetail.TAX8_Base_Amt
                    obj.TAX8_Rate = objShipmentDetail.TAX8_Rate
                    obj.TAX8_Amt += objShipmentDetail.TAX8_Amt
                    obj.TAX9 = objShipmentDetail.TAX9
                    obj.TAX9_Base_Amt += objShipmentDetail.TAX9_Base_Amt
                    obj.TAX9_Rate = objShipmentDetail.TAX9_Rate
                    obj.TAX9_Amt += objShipmentDetail.TAX9_Amt
                    obj.TAX10 = objShipmentDetail.TAX10
                    obj.TAX10_Base_Amt += objShipmentDetail.TAX10_Base_Amt
                    obj.TAX10_Rate = objShipmentDetail.TAX10_Rate
                    obj.TAX10_Amt += objShipmentDetail.TAX10_Amt
                    obj.Distributor_Commission_TotalAmt += objShipmentDetail.Distributor_Commission_Amt
                    obj.Security_TotalAmt += objShipmentDetail.Security_Amt
                    obj.Transporter_Commission_TotalAmt += objShipmentDetail.Transporter_Commission_Amt
                    obj.Arr.Add(objTr)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return obj
    End Function

End Class
