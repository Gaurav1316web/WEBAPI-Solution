Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsBookingProformaInvoice
#Region "Variables"

    Public Electronic_Ref_No As String = Nothing
    Public EWayBillDate As Date?
    Public EWayBillNo As String = Nothing
    Public GSTStatus As Boolean = False
    Public Is_Taxable As Integer = 0
    Public DO_Item_Type As String = Nothing
    Public EmptyCharge As Double = 0
    Public FixedCharge As Double = 0
    Public Freight_Type As String = Nothing
    Public Including_Insurance As Integer = 0
    Public Insurance As String = Nothing
    Public VAT_InvoiceNo As String = Nothing
    Public VatInvoice_Type As String = Nothing
    Public Vehicle_Manual_No As String = Nothing
    Public BookingProformaNo As String = Nothing

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
    Public Arr As List(Of clsBookingProformaInvoiceDetail) = Nothing

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
    Public Booking_No As String = Nothing
    Public CashCustomer As String = Nothing
    Public allowSMSforSalePerson As Boolean = False

#End Region

    Public Shared Function SaveData(ByVal obj As clsBookingProformaInvoice, ByVal isNewEntry As Boolean, Optional ByVal IsDairyModule As Boolean = False) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsBookingProformaInvoice, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            clsSerializeInvenotry.DeleteData("PI-IN", obj.Document_Code, trans)
            clsBatchInventory.DeleteData("PI-SH", obj.Document_Code, trans)

            Dim qry As String = "delete from tspl_dairy_proforma_invoice_detail where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            obj.GSTStatus = clsERPFuncationality.GetGSTStatus(obj.Document_Date)

            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmPerformaInvoiceBooking, "", obj.Bill_To_Location)
            End If


            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmPerformaInvoiceDairy, obj.Bill_To_Location, obj.Document_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))


            clsCommon.AddColumnsForChange(coll, "Freight_Charges", obj.Freight_Charges)

            clsCommon.AddColumnsForChange(coll, "Total_Item_Weight", obj.Total_Item_Weight)
            clsCommon.AddColumnsForChange(coll, "Transport_Id", obj.Transport_Id)
            clsCommon.AddColumnsForChange(coll, "Transporter_Name", obj.Transporter_Name)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Internal", IIf(obj.Is_Internal, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Inv_No", obj.Inv_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location, True)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Party", obj.Ship_To_Party, True)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Party_Parent", obj.Ship_To_Party_Parent, True)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)

            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Carrier", obj.Carrier)
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
            clsCommon.AddColumnsForChange(coll, "GENo", obj.GENo)
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)

            clsCommon.AddColumnsForChange(coll, "Vehicle_Manual_No", obj.Vehicle_Manual_No, True)

            If obj.GEDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GEDate", clsCommon.GetPrintDate(obj.GEDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GEDate", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code1", obj.Add_Charge_Code1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name1", obj.Add_Charge_Name1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt1", obj.Add_Charge_Amt1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code2", obj.Add_Charge_Code2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name2", obj.Add_Charge_Name2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt2", obj.Add_Charge_Amt2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code3", obj.Add_Charge_Code3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name3", obj.Add_Charge_Name3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt3", obj.Add_Charge_Amt3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code4", obj.Add_Charge_Code4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name4", obj.Add_Charge_Name4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt4", obj.Add_Charge_Amt4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code5", obj.Add_Charge_Code5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name5", obj.Add_Charge_Name5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt5", obj.Add_Charge_Amt5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code6", obj.Add_Charge_Code6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name6", obj.Add_Charge_Name6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt6", obj.Add_Charge_Amt6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code7", obj.Add_Charge_Code7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name7", obj.Add_Charge_Name7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt7", obj.Add_Charge_Amt7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code8", obj.Add_Charge_Code8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name8", obj.Add_Charge_Name8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt8", obj.Add_Charge_Amt8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code9", obj.Add_Charge_Code9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name9", obj.Add_Charge_Name9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt9", obj.Add_Charge_Amt9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Code10", obj.Add_Charge_Code10)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Name10", obj.Add_Charge_Name10)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt10", obj.Add_Charge_Amt10)
            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge", obj.Total_Add_Charge)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            '=================Added by preeti Gupta Against ticket no[ALF/22/05/18-000067]===============
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)



            If obj.Challan_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            End If
            If obj.Inv_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Inv_Date", clsCommon.GetPrintDate(obj.Inv_Date, "dd/MMM/yyyy"))
            End If


            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code, True)
            clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Route_Desc", obj.Route_Desc)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Per", obj.HeadDisc_Per)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_PerAmt", obj.HeadDisc_PerAmt)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Amt", obj.HeadDisc_Amt)
            clsCommon.AddColumnsForChange(coll, "TotCashDiscAmt", obj.TotCashDiscAmt)


            clsCommon.AddColumnsForChange(coll, "Cust_PO_No", obj.Cust_PO_No)
            clsCommon.AddColumnsForChange(coll, "Form_38_No", obj.Form_38_No)


            clsCommon.AddColumnsForChange(coll, "SO_Validity", obj.SO_Validity)
            clsCommon.AddColumnsForChange(coll, "Commission_Apply", obj.Commission_Apply)

            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Terms", obj.Dispatch_Terms)
            clsCommon.AddColumnsForChange(coll, "PaymentTerm", obj.Payment_Terms)

            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "WayBillNo", obj.WayBillNo)

            If clsCommon.myLen(obj.WayBillNo) > 0 Then
                clsCommon.AddColumnsForChange(coll, "WayBillDate", clsCommon.GetPrintDate(obj.WayBillDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "WayBillDate", Nothing, True)
            End If

            If clsCommon.myLen(obj.GR_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "GR_Date", clsCommon.GetPrintDate(obj.GR_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GR_Date", Nothing, True)
            End If
            If clsCommon.myLen(obj.RoadPermit_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", clsCommon.GetPrintDate(obj.RoadPermit_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", Nothing, True)
            End If

            If obj.Podate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Podate, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "cust_po_date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Itemwise", obj.Itemwise)
            clsCommon.AddColumnsForChange(coll, "Advance_Percentage", obj.Advance_Percentage)


            clsCommon.AddColumnsForChange(coll, "Including_Insurance", obj.Including_Insurance)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", obj.Is_Taxable)
            clsCommon.AddColumnsForChange(coll, "Print_Discount_Amt", obj.Print_Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Freight_Type", clsCommon.myCstr(obj.Freight_Type))
            clsCommon.AddColumnsForChange(coll, "FixedCharge", clsCommon.myCdbl(obj.FixedCharge))
            clsCommon.AddColumnsForChange(coll, "EmptyCharge", clsCommon.myCdbl(obj.EmptyCharge))
            clsCommon.AddColumnsForChange(coll, "Nine_NR_No", obj.Nine_NR_No)
            clsCommon.AddColumnsForChange(coll, "DO_Item_Type", obj.DO_Item_Type)

            clsCommon.AddColumnsForChange(coll, "Booking_No", obj.Booking_No)
            clsCommon.AddColumnsForChange(coll, "BookingProformaNo", obj.BookingProformaNo)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)



            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_dairy_proforma_invoice_head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_dairy_proforma_invoice_head", OMInsertOrUpdate.Update, "tspl_dairy_proforma_invoice_head.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsBookingProformaInvoiceDetail.SaveData(obj.Document_Code, obj.Arr, trans, obj.Document_Date)


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function UpdateAfterPosting(ByVal obj As clsBookingProformaInvoice, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.Document_Code) > 0 Then
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
                clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
                If clsCommon.myLen(obj.GR_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "GR_Date", clsCommon.GetPrintDate(obj.GR_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "GR_Date", Nothing, True)
                End If
                If clsCommon.myLen(obj.RoadPermit_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", clsCommon.GetPrintDate(obj.RoadPermit_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)

                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_dairy_proforma_invoice_head", OMInsertOrUpdate.Update, "tspl_dairy_proforma_invoice_head.Document_Code='" + obj.Document_Code + "'", trans)

            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsBookingProformaInvoice
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, Optional ByVal IsDairyModule As Boolean = False) As clsBookingProformaInvoice

        Dim obj As clsBookingProformaInvoice = Nothing
        Dim qry As String = "SELECT * FROM tspl_dairy_proforma_invoice_head "
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_dairy_proforma_invoice_head.Bill_To_Location "
        qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=tspl_dairy_proforma_invoice_head.Ship_To_Location "
        qry += " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= tspl_dairy_proforma_invoice_head.Tax_Group "
        qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=tspl_dairy_proforma_invoice_head.Terms_Code "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_dairy_proforma_invoice_head.Customer_Code where 2=2"
        Dim whrCls As String = ""

        Select Case NavType
            Case NavigatorType.First
                qry += " and tspl_dairy_proforma_invoice_head.Document_Code = (select MIN(Document_Code) from tspl_dairy_proforma_invoice_head WHERE 1=1 " + whrCls + ") "
            Case NavigatorType.Last
                qry += " and tspl_dairy_proforma_invoice_head.Document_Code = (select Max(Document_Code) from tspl_dairy_proforma_invoice_head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and tspl_dairy_proforma_invoice_head.Document_Code = '" + strPONo + "' "
            Case NavigatorType.Next
                qry += " and tspl_dairy_proforma_invoice_head.Document_Code = (select Min(Document_Code) from tspl_dairy_proforma_invoice_head where Document_Code>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and tspl_dairy_proforma_invoice_head.Document_Code = (select Max(Document_Code) from tspl_dairy_proforma_invoice_head where Document_Code<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBookingProformaInvoice()
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
            obj.DO_Item_Type = clsCommon.myCstr(dt.Rows(0)("DO_Item_Type"))
            obj.FixedCharge = clsCommon.myCdbl(dt.Rows(0)("FixedCharge"))
            obj.EmptyCharge = clsCommon.myCdbl(dt.Rows(0)("EmptyCharge"))
            obj.Freight_Type = clsCommon.myCstr(dt.Rows(0)("Freight_Type"))
            obj.Including_Insurance = clsCommon.myCdbl(dt.Rows(0)("Including_Insurance"))

          
            obj.Vehicle_Manual_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_Manual_No"))
          
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.Advance_Approval_Reqd = clsCommon.myCdbl(dt.Rows(0)("Advance_Approval_Reqd"))
            obj.Is_Advance_Approved = clsCommon.myCdbl(dt.Rows(0)("Is_Advance_Approved"))
            obj.Freight_Charges = clsCommon.myCdbl(dt.Rows(0)("Freight_Charges"))
            obj.Total_Item_Weight = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Weight"))

            obj.Print_Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Print_Discount_Amt"))
            obj.Nine_NR_No = clsCommon.myCstr(dt.Rows(0)("Nine_NR_No"))
            obj.Itemwise = clsCommon.myCdbl(dt.Rows(0)("Itemwise"))
            obj.Advance_Percentage = clsCommon.myCdbl(dt.Rows(0)("Advance_Percentage"))
            If dt.Rows(0)("GR_Date") IsNot DBNull.Value Then
                obj.GR_Date = clsCommon.myCstr(dt.Rows(0)("GR_Date"))
            End If
            If dt.Rows(0)("RoadPermit_Date") IsNot DBNull.Value Then
                obj.RoadPermit_Date = clsCommon.myCstr(dt.Rows(0)("RoadPermit_Date"))
            End If
          

            obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            If dt.Rows(0)("WayBillDate") IsNot DBNull.Value Then
                obj.WayBillDate = clsCommon.myCDate(dt.Rows(0)("WayBillDate"))
            End If
            obj.WayBillNo = clsCommon.myCstr(dt.Rows(0)("WayBillNo"))
   



            obj.Dispatch_Terms = clsCommon.myCstr(dt.Rows(0)("Dispatch_Terms"))
            obj.Payment_Terms = clsCommon.myCstr(dt.Rows(0)("PaymentTerm"))

            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))

            obj.Electronic_Ref_No = clsCommon.myCstr(dt.Rows(0)("Electronic_Ref_No"))

            obj.Is_Delivered = clsCommon.myCdbl(dt.Rows(0)("Is_Delivered"))
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Ship_To_Party = clsCommon.myCstr(dt.Rows(0)("Ship_To_Party"))
            obj.Ship_To_Party_Parent = clsCommon.myCstr(dt.Rows(0)("Ship_To_Party_Parent"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))

            If dt.Rows(0)("Due_Date") IsNot DBNull.Value Then
                obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            End If

            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If

            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
   


            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))

          
            obj.Add_Charge_Code1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code1"))
            obj.Add_Charge_Name1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name1"))
            obj.Add_Charge_Amt1 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt1"))

            obj.Add_Charge_Code2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code2"))
            obj.Add_Charge_Name2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name2"))
            obj.Add_Charge_Amt2 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt2"))

            obj.Add_Charge_Code3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code3"))
            obj.Add_Charge_Name3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name3"))
            obj.Add_Charge_Amt3 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt3"))

            obj.Add_Charge_Code4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code4"))
            obj.Add_Charge_Name4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name4"))
            obj.Add_Charge_Amt4 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt4"))

            obj.Add_Charge_Code5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code5"))
            obj.Add_Charge_Name5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name5"))
            obj.Add_Charge_Amt5 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt5"))

            obj.Add_Charge_Code6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code6"))
            obj.Add_Charge_Name6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name6"))
            obj.Add_Charge_Amt6 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt6"))

            obj.Add_Charge_Code7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code7"))
            obj.Add_Charge_Name7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name7"))
            obj.Add_Charge_Amt7 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt7"))

            obj.Add_Charge_Code8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code8"))
            obj.Add_Charge_Name8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name8"))
            obj.Add_Charge_Amt8 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt8"))

            obj.Add_Charge_Code9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code9"))
            obj.Add_Charge_Name9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name9"))
            obj.Add_Charge_Amt9 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt9"))

            obj.Add_Charge_Code10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code10"))
            obj.Add_Charge_Name10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name10"))
            obj.Add_Charge_Amt10 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt10"))

            obj.Total_Add_Charge = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge"))
            obj.Inv_No = clsCommon.myCstr(dt.Rows(0)("Inv_No"))
            If dt.Rows(0)("Challan_Date") IsNot DBNull.Value Then

                obj.Challan_Date = clsCommon.GetPrintDate((dt.Rows(0)("Challan_Date")), "dd/MMM/yyyy")
            End If

            If dt.Rows(0)("Inv_Date") IsNot DBNull.Value Then
                obj.Inv_Date = clsCommon.GetPrintDate((dt.Rows(0)("Inv_Date")), "dd/MMM/yyyy")
            End If

            obj.Booking_No = clsCommon.myCstr(dt.Rows(0)("Booking_No"))
            obj.BookingProformaNo = clsCommon.myCstr(dt.Rows(0)("BookingProformaNo"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Salesman_Name"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.HeadDisc_Per = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Per"))
            obj.HeadDisc_PerAmt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_PerAmt"))
            obj.HeadDisc_Amt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Amt"))
            obj.TotCashDiscAmt = clsCommon.myCdbl(dt.Rows(0)("TotCashDiscAmt"))

            '  ---------------------------------------------
            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If

            qry = "SELECT tspl_dairy_proforma_invoice_detail.OrgUnit_code,tspl_dairy_proforma_invoice_detail.Is_Mannual_Amt,tspl_dairy_proforma_invoice_detail.Document_Code,tspl_dairy_proforma_invoice_detail.Line_No, tspl_dairy_proforma_invoice_detail.Status,tspl_dairy_proforma_invoice_detail.Row_Type,tspl_dairy_proforma_invoice_detail.status,tspl_dairy_proforma_invoice_detail.Item_Code, TSPL_ITEM_MASTER.Item_Desc,tspl_dairy_proforma_invoice_detail.Qty,tspl_dairy_proforma_invoice_detail.Free_Qty,tspl_dairy_proforma_invoice_detail.Balance_Qty,tspl_dairy_proforma_invoice_detail.Unit_code, tspl_dairy_proforma_invoice_detail.Location,tspl_dairy_proforma_invoice_detail.Item_Cost,tspl_dairy_proforma_invoice_detail.TAX1,tspl_dairy_proforma_invoice_detail.TAX1_Rate, tspl_dairy_proforma_invoice_detail.TAX1_Amt,tspl_dairy_proforma_invoice_detail.TAX2,tspl_dairy_proforma_invoice_detail.TAX2_Rate,tspl_dairy_proforma_invoice_detail.TAX2_Amt, tspl_dairy_proforma_invoice_detail.TAX3,tspl_dairy_proforma_invoice_detail.TAX3_Rate,tspl_dairy_proforma_invoice_detail.TAX3_Amt,tspl_dairy_proforma_invoice_detail.TAX4 , tspl_dairy_proforma_invoice_detail.TAX4_Rate,tspl_dairy_proforma_invoice_detail.TAX4_Amt,tspl_dairy_proforma_invoice_detail.TAX5,tspl_dairy_proforma_invoice_detail.TAX5_Rate , tspl_dairy_proforma_invoice_detail.TAX5_Amt,tspl_dairy_proforma_invoice_detail.TAX6,tspl_dairy_proforma_invoice_detail.TAX6_Rate,tspl_dairy_proforma_invoice_detail.TAX6_Amt, tspl_dairy_proforma_invoice_detail.TAX7,tspl_dairy_proforma_invoice_detail.TAX7_Rate,tspl_dairy_proforma_invoice_detail.TAX7_Amt,tspl_dairy_proforma_invoice_detail.TAX8, tspl_dairy_proforma_invoice_detail.TAX8_Rate,tspl_dairy_proforma_invoice_detail.TAX8_Amt,tspl_dairy_proforma_invoice_detail.TAX9,tspl_dairy_proforma_invoice_detail.TAX9_Rate, tspl_dairy_proforma_invoice_detail.TAX9_Amt,tspl_dairy_proforma_invoice_detail.TAX10,tspl_dairy_proforma_invoice_detail.TAX10_Rate,tspl_dairy_proforma_invoice_detail.TAX10_Amt, tspl_dairy_proforma_invoice_detail.Amount,tspl_dairy_proforma_invoice_detail.Disc_Per,tspl_dairy_proforma_invoice_detail.Disc_Amt,tspl_dairy_proforma_invoice_detail.Amt_Less_Discount, tspl_dairy_proforma_invoice_detail.Total_Tax_Amt,tspl_dairy_proforma_invoice_detail.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName, tspl_dairy_proforma_invoice_detail.TAX1_Base_Amt,tspl_dairy_proforma_invoice_detail.TAX2_Base_Amt,tspl_dairy_proforma_invoice_detail.TAX3_Base_Amt , tspl_dairy_proforma_invoice_detail.TAX4_Base_Amt,tspl_dairy_proforma_invoice_detail.TAX5_Base_Amt,tspl_dairy_proforma_invoice_detail.TAX6_Base_Amt, tspl_dairy_proforma_invoice_detail.TAX7_Base_Amt,tspl_dairy_proforma_invoice_detail.TAX8_Base_Amt,tspl_dairy_proforma_invoice_detail.TAX9_Base_Amt, tspl_dairy_proforma_invoice_detail.TAX10_Base_Amt,tspl_dairy_proforma_invoice_detail.MRP,tspl_dairy_proforma_invoice_detail.Batch_No,tspl_dairy_proforma_invoice_detail.MFG_Date, tspl_dairy_proforma_invoice_detail.Expiry_Date,tspl_dairy_proforma_invoice_detail.Specification,tspl_dairy_proforma_invoice_detail.Remarks,tspl_dairy_proforma_invoice_detail.Assessable, tspl_dairy_proforma_invoice_detail.AssessableAmt,tspl_dairy_proforma_invoice_detail.Bar_Code, tspl_dairy_proforma_invoice_detail.Scheme_Applicable,tspl_dairy_proforma_invoice_detail.Scheme_Code, tspl_dairy_proforma_invoice_detail.Scheme_Item,tspl_dairy_proforma_invoice_detail.Item_Tax,tspl_dairy_proforma_invoice_detail.Total_MRP_Amt, tspl_dairy_proforma_invoice_detail.Total_Basic_Amt,tspl_dairy_proforma_invoice_detail.Total_Disc_Amt,tspl_dairy_proforma_invoice_detail.Cust_Discount, tspl_dairy_proforma_invoice_detail.Total_Cust_Discount,tspl_dairy_proforma_invoice_detail.ActualRate,tspl_dairy_proforma_invoice_detail.Cust_DiscountQty, tspl_dairy_proforma_invoice_detail.Price_code,tspl_dairy_proforma_invoice_detail.Abatement_Per,tspl_dairy_proforma_invoice_detail.Abatement_Amt, tspl_dairy_proforma_invoice_detail.FOC_Item,tspl_dairy_proforma_invoice_detail.Item_Weight,tspl_dairy_proforma_invoice_detail.Price_Date, tspl_dairy_proforma_invoice_detail.HeadDiscPer,tspl_dairy_proforma_invoice_detail.HeadDiscPerAmt,tspl_dairy_proforma_invoice_detail.Bin_No,tspl_dairy_proforma_invoice_detail.TotalItem_Weight,tspl_dairy_proforma_invoice_detail.Conv_Factor,tspl_dairy_proforma_invoice_detail.Purchase_Cost,tspl_dairy_proforma_invoice_detail.OrgRate,  tspl_dairy_proforma_invoice_detail.vendor_code,tspl_dairy_proforma_invoice_detail.vendor_desc"
            qry += " ,tspl_dairy_proforma_invoice_detail.Markup_On,tspl_dairy_proforma_invoice_detail.Markup_Percent,tspl_dairy_proforma_invoice_detail.Landing_Cost,tspl_dairy_proforma_invoice_detail.HeadDiscAmt,tspl_dairy_proforma_invoice_detail.CustDiscPer,tspl_dairy_proforma_invoice_detail.CasdDiscScheme_Code ,tspl_dairy_proforma_invoice_detail.Item_Group,tspl_dairy_proforma_invoice_detail.Commission_Rate,tspl_dairy_proforma_invoice_detail.Commission_Party,tspl_dairy_proforma_invoice_detail.Commission_Amt,tspl_dairy_proforma_invoice_detail.Amt_Less_Commission ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Booking_No, TSPL_BOOKING_MATSER.Created_By as Booking_User_Code,TSPL_USER_MASTER.Distributor_Retailer_Code,SecCust.Customer_Name as Distributor_Retailer_Name,SecCust.Email as Distributor_Retailer_Email,TSPL_Additional_Charges.Description as  AddChargeDesc,tspl_dairy_proforma_invoice_detail.Price_Code,tspl_dairy_proforma_invoice_detail.Price_Date "
            qry += " FROM tspl_dairy_proforma_invoice_detail"
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_dairy_proforma_invoice_detail.Location "
            qry += " left outer join tspl_dairy_proforma_invoice_head on TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Document_Code=tspl_dairy_proforma_invoice_detail.DOCUMENT_CODE "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=tspl_dairy_proforma_invoice_detail.Item_Code  "
            qry += " left join TSPL_BOOKING_MATSER on tspl_dairy_proforma_invoice_head.Booking_No=TSPL_BOOKING_MATSER.Document_No  "
            qry += " left join TSPL_USER_MASTER on TSPL_BOOKING_MATSER.Created_By=TSPL_USER_MASTER.User_Code  "
            qry += " left join TSPL_SECONDARY_CUSTOMER_MASTER SecCust on SecCust.Cust_Code=TSPL_USER_MASTER.Distributor_Retailer_Code "
            qry += " left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code=tspl_dairy_proforma_invoice_detail.Item_Code   "
            qry += " where tspl_dairy_proforma_invoice_detail.Document_Code='" + obj.Document_Code + "' ORDER BY tspl_dairy_proforma_invoice_detail.Line_No  asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsBookingProformaInvoiceDetail)
                Dim objTr As clsBookingProformaInvoiceDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsBookingProformaInvoiceDetail
        
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        objTr.Item_Desc = clsCommon.myCstr(dr("AddChargeDesc"))
                    Else
                        objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    End If

                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Bar_Code = clsCommon.myCstr(dr("Bar_Code"))

                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))


                    objTr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))



                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Location = clsCommon.myCstr(dr("Location"))
                    objTr.LocationName = clsCommon.myCstr(dr("LocationName"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))


                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(dr("Is_Mannual_Amt"))


                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                    End If
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))


                 
                    objTr.Item_Tax = clsCommon.myCdbl(dr("Item_Tax"))
                    objTr.Total_MRP_Amt = clsCommon.myCdbl(dr("Total_MRP_Amt"))
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                    objTr.Total_Disc_Amt = clsCommon.myCdbl(dr("Total_Disc_Amt"))
                    objTr.Cust_Discount = clsCommon.myCdbl(dr("Cust_Discount"))
                    objTr.Total_Cust_Discount = clsCommon.myCdbl(dr("Total_Cust_Discount"))
                    objTr.ActualRate = clsCommon.myCdbl(dr("ActualRate"))
                    objTr.Cust_DiscountQty = clsCommon.myCdbl(dr("Cust_DiscountQty"))
                 
                    objTr.Abatement_Per = clsCommon.myCdbl(dr("Abatement_Per"))
                    objTr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))
                    objTr.FOC_Item = clsCommon.myCdbl(dr("FOC_Item"))
                    objTr.Markup_On = clsCommon.myCstr(dr("Markup_On"))
                    objTr.Markup_Percent = clsCommon.myCdbl(dr("Markup_Percent"))
                    objTr.Landing_Cost = clsCommon.myCdbl(dr("Landing_Cost"))
                    objTr.HeadDiscAmt = clsCommon.myCdbl(dr("HeadDiscAmt"))
                    objTr.CustDiscPer = clsCommon.myCdbl(dr("CustDiscPer"))
                    objTr.CasdDiscScheme_Code = clsCommon.myCstr(dr("CasdDiscScheme_Code"))
                    objTr.Item_Weight = clsCommon.myCdbl(dr("Item_Weight"))
                    objTr.TotalItem_Weight = clsCommon.myCdbl(dr("TotalItem_Weight"))
                    objTr.Purchase_Cost = clsCommon.myCdbl(dr("Purchase_Cost"))
                    objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
                    objTr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                  
                    objTr.vendor_code = clsCommon.myCstr(dr("vendor_code"))
                    objTr.vendor_desc = clsCommon.myCstr(dr("vendor_desc"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    objTr.Price_code = clsCommon.myCstr(dr("Price_Code"))
                    objTr.Price_Date = clsCommon.myCstr(dr("Price_Date"))
                    objTr.HeadDiscPer = clsCommon.myCdbl(dr("HeadDiscPer"))
                    objTr.HeadDiscPerAmt = clsCommon.myCdbl(dr("HeadDiscPerAmt"))
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("PI-IN", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData("PI-SH", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)

                    obj.Arr.Add(objTr)
                Next
            End If


        End If

        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(strCode, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Proforma Invoice No not found to Delete")
        End If
        Dim obj As clsBookingProformaInvoice = clsBookingProformaInvoice.GetData(strCode, NavigatorType.Current, trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmPerformaInvoiceDairy, obj.Bill_To_Location, obj.Document_Date, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsSerializeInvenotry.DeleteData("PI-IN", strCode, trans)
                clsBatchInventory.DeleteData("PI-SH", strCode, trans)
             

                Dim qry As String = "delete from tspl_dairy_proforma_invoice_detail where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Dim qry1 As String = "delete from tspl_dairy_proforma_invoice_head where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry1, trans)

                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, Optional ByVal IsDairyModule As Boolean = False) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim obj As clsBookingProformaInvoice = Nothing
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Shipment No not found to Post")
            End If

            obj = clsBookingProformaInvoice.GetData(strDocNo, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmPerformaInvoiceDairy, obj.Bill_To_Location, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Proforma No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""



            qry = "Update TSPL_DAIRY_PROFORMA_INVOICE_HEAD set  Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsBookingProformaInvoiceDetail
#Region "Variables"
    Public ItemwiseTaxCode As String = ""
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

    Public Booking_User_Code As String = ""
    Public Distributor_Retailer_Code As String = ""
    Public Distributor_Retailer_Name As String = ""
    Public Distributor_Retailer_Email As String = ""
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBookingProformaInvoiceDetail), ByVal trans As SqlTransaction, ByVal DocDate As DateTime) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBookingProformaInvoiceDetail In Arr
                Dim coll As New Hashtable()


                clsCommon.AddColumnsForChange(coll, "Item_Group", obj.Item_Group)

                clsCommon.AddColumnsForChange(coll, "Commission_Rate", obj.Commission_Rate)
                clsCommon.AddColumnsForChange(coll, "Commission_Party", obj.Commission_Party)
                clsCommon.AddColumnsForChange(coll, "Commission_Amt", obj.Commission_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Commission", obj.Amt_Less_Commission)
                clsCommon.AddColumnsForChange(coll, "OrgUnit_code", obj.OrgUnit_code)
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Bar_Code", obj.Bar_Code, True)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommon.AddColumnsForChange(coll, "Free_qty", obj.Free_Qty)




                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)

                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)

                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Is_Mannual_Amt", obj.Is_Mannual_Amt)
                If obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd-MMM-yyyy"))
                End If
                If obj.Expiry_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd-MMM-yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Assessable", obj.Assessable)
                clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)


                clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", IIf(obj.Scheme_Applicable = "Yes", "Y", "N"))
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", IIf(obj.Scheme_Item = "Yes", "Y", "N"))
                clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_MRP_Amt", obj.Total_MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Cust_Discount", obj.Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Total_Cust_Discount", obj.Total_Cust_Discount)

                Dim strSql As String = "select top 1 TSPL_ITEM_PRICE_MASTER.Price_Amount1 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 , " & _
             "TSPL_ITEM_PRICE_MASTER.Price_Amount3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 , " & _
             "TSPL_ITEM_PRICE_MASTER.Price_Amount6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 , " & _
             "TSPL_ITEM_PRICE_MASTER.Price_Amount9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 from TSPL_ITEM_PRICE_MASTER " & _
            "where  TSPL_ITEM_PRICE_MASTER.Price_Code='" & obj.Price_code & "' and  TSPL_ITEM_PRICE_MASTER.Item_Code='" & obj.Item_Code & "' and  " & _
            "TSPL_ITEM_PRICE_MASTER.Item_Basic_Net=" & obj.MRP & " and TSPL_ITEM_PRICE_MASTER.UOM='" & obj.Unit_code & "' "
                Dim dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(strSql, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Price_Amount1 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount1"))
                    obj.Price_Amount2 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount2"))
                    obj.Price_Amount3 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount3"))
                    obj.Price_Amount4 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount4"))
                    obj.Price_Amount5 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount5"))
                    obj.Price_Amount6 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount6"))
                    obj.Price_Amount7 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount7"))
                    obj.Price_Amount8 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount8"))
                    obj.Price_Amount9 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount9"))
                    obj.Price_Amount10 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount10"))
                End If

                clsCommon.AddColumnsForChange(coll, "Price_Amount1", obj.Price_Amount1)
                clsCommon.AddColumnsForChange(coll, "Price_Amount2", obj.Price_Amount2)
                clsCommon.AddColumnsForChange(coll, "Price_Amount3", obj.Price_Amount3)
                clsCommon.AddColumnsForChange(coll, "Price_Amount4", obj.Price_Amount4)
                clsCommon.AddColumnsForChange(coll, "Price_Amount5", obj.Price_Amount5)
                clsCommon.AddColumnsForChange(coll, "Price_Amount6", obj.Price_Amount6)
                clsCommon.AddColumnsForChange(coll, "Price_Amount7", obj.Price_Amount7)
                clsCommon.AddColumnsForChange(coll, "Price_Amount8", obj.Price_Amount8)
                clsCommon.AddColumnsForChange(coll, "Price_Amount9", obj.Price_Amount9)
                clsCommon.AddColumnsForChange(coll, "Price_Amount10", obj.Price_Amount10)
                clsCommon.AddColumnsForChange(coll, "ActualRate", obj.ActualRate)
                clsCommon.AddColumnsForChange(coll, "Cust_DiscountQty", obj.Cust_DiscountQty)
                clsCommon.AddColumnsForChange(coll, "Price_code", obj.Price_code)
                If clsCommon.myLen(obj.Price_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Abatement_Per", obj.Abatement_Per)
                clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)
                clsCommon.AddColumnsForChange(coll, "FOC_Item", obj.FOC_Item)

                clsCommon.AddColumnsForChange(coll, "Item_Weight", obj.Item_Weight)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "TotalItem_Weight", obj.TotalItem_Weight)
                clsCommon.AddColumnsForChange(coll, "Markup_On", obj.Markup_On)
                clsCommon.AddColumnsForChange(coll, "Markup_Percent", obj.Markup_Percent)
                clsCommon.AddColumnsForChange(coll, "Landing_Cost", obj.Landing_Cost)
                clsCommon.AddColumnsForChange(coll, "HeadDiscAmt", obj.HeadDiscAmt)
                clsCommon.AddColumnsForChange(coll, "CustDiscPer", obj.CustDiscPer)
                clsCommon.AddColumnsForChange(coll, "CasdDiscScheme_Code", obj.CasdDiscScheme_Code)
                clsCommon.AddColumnsForChange(coll, "Purchase_Cost", obj.Purchase_Cost)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)


                clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendor_code)
                clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendor_desc)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPer", obj.HeadDiscPer)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPerAmt", obj.HeadDiscPerAmt)

                clsCommonFunctionality.UpdateDataTable(coll, "tspl_dairy_proforma_invoice_detail", OMInsertOrUpdate.Insert, "", trans)
                clsSerializeInvenotry.SaveData("PI-IN", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.arrSrItem, trans)
                clsBatchInventory.SaveData("PI-SH", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)

            Next
        End If
        Return True
    End Function
End Class


