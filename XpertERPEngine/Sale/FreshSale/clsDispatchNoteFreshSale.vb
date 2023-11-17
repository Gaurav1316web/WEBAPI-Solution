
Imports common
Imports System.Data.SqlClient
Public Class clsDispatchNoteFreshSale
#Region "Variables"
    Public isCardSale As Integer = 0
    Public ShippingThrough As String = String.Empty
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public Sub_Location_code As String = String.Empty
    Public LeakageAmount As Decimal = 0
    Public TotalCAN As Double = 0
    Public ShippedCAN As Double = 0
    Public OPKm As Double = 0
    Public CLKm As Double = 0
    Public Dispatch_date As Date?
    Public Sale_Invoice_Date As Date?
    Public Screen_Type As String = Nothing
    Public DO_Item_Type As String = Nothing
    Public EWayBillDate As Date?
    Public EWayBillNo As String = Nothing
    Public AlternateVehicle As String = Nothing
    Public ManualVehicle As String = Nothing
    Public Trans_Type As String = String.Empty
    Public Shift_Type As String = String.Empty
    Public Payment_Type As String = String.Empty
    Public Payment_Rate As String = String.Empty
    Public Charge_For As String = String.Empty
    Public Payment_Amount As Double = 0
    Public Total_Item_Weight As Double = 0
    Public Podate As DateTime?
    Public Form_38_No As String = Nothing
    Public Cust_PO_No As String = Nothing
    Public Price_Group_Code As String = Nothing
    Public Invoice_No As String = Nothing
    Public Invoice_Type As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime
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

    Public Is_Create_Auto_Invoice As Boolean = False
    Public Sale_Invoice_No As String = Nothing
    Public Is_Create_Auto_Receipt As Boolean = False
    Public Against_POS As String = Nothing

    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Arr As List(Of clsDispatchNoteFreshSaleDetail) = Nothing

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

    '' added column for dispatch 
    Public Booking_No As String = Nothing
    Public Booking_Date As DateTime? = Nothing
    Public DeliveryStatus As String = Nothing
    Public Vehicle_Capacity As Double = 0
    Public Lorry_No As String = Nothing
    Public Road_Permit_No As String = Nothing
    Public Transporter_Name As String = Nothing
    Public Freight As String = Nothing
    Public Freight_Amount As Double = 0
    Public CrateQty As Double = 0
    Public IsSampling As Boolean = False
    Public RoundOffAmount As Decimal = 0
    Public Against_Delivery_Code As String = Nothing
    Public ArrChkList As List(Of clsPSShipmentChecklistDetail) = Nothing
    ''  end

    Public Manual_Driver_Name As String = Nothing
    Public Manual_Salesman_Name As String = Nothing
    Public Is_Taxable As Integer = 0
    Public Distributor_Commission_TotalAmt As Decimal = 0

#End Region

    Public Function SaveData(ByVal obj As clsDispatchNoteFreshSale, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function GetInvoiceType(ByVal strCustCode As String, ByVal strLocation As String, ByVal strType As String, ByVal trans As SqlTransaction)
        Dim dt As DataTable


        Dim qry As String

        If clsCommon.CompairString(strType, "S") = CompairStringResult.Equal Then

            qry = "select State from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" & strLocation & "'"
        Else

            qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
              "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
              "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
              "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' " & _
              "WHERE TSPL_LOCATION_MASTER.Location_Code = '" + strLocation + "'"
        End If
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strLocState As String = clsCommon.myCstr(dt.Rows(0)("State"))

        qry = "select Price_Code,price_CodeNon,State,Tin_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustCode + "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCustState As String = clsCommon.myCstr(dt.Rows(0)("State"))
        Dim strTinNo As String = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
        'If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
        '    Invoice_Type = "T"
        'Else
        '    Invoice_Type = "R"
        'End If
        Invoice_Type = "N"
        Return Invoice_Type

        dt = Nothing
        strLocState = Nothing
        strCustState = Nothing
        strTinNo = Nothing
    End Function
    Public Function SaveData(ByVal obj As clsDispatchNoteFreshSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fresh Sale", "Fresh Dispatch Multiple", obj.Bill_To_Location, obj.Document_Date, trans)
            clsSerializeInvenotry.DeleteData("SD-IN", obj.Document_Code, trans)
            checkSaveNotification(obj.Document_Date, obj.Customer_Code, trans)

            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_SD_SHIPMENT_HEAD", "Document_Code", "TSPL_SD_SHIPMENT_DETAIL", "Document_Code", trans)
            End If
            Dim qry As String = "delete from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsBatchInventory.DeleteData("FS-SH", obj.Document_Code, trans)
           
            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Screen_Type, "DS") = CompairStringResult.Equal Then  'sanjay 09/july/2018 BHA/21/06/18-000064 Add Dairy Sale Condition
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmShipmentProductSale, clsDocTransactionType.Other, obj.Bill_To_Location)
                Else
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DispatchNoteFreshSale, "", obj.Bill_To_Location)
                End If
                'If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNShipment, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location)
                'Else
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNShipment, clsDocTransactionType.SNQuotationOther, obj.Bill_To_Location)
                'End If
            End If
            ' for Invoice Type
            Dim AllowChangeInvoiceType As Boolean = False
            AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters", trans)) = 0, False, True)
            If obj.Is_Create_Auto_Invoice Then
                If AllowChangeInvoiceType Then
                    obj.Invoice_Type = "N"
                    'If clsCommon.myLen(obj.Invoice_Type) <= 0 Then
                    '    common.clsCommon.MyMessageBoxShow("Please select invoice  Type for creating auto invoice")
                    '    Return False
                    'End If
                Else
                    If clsCommon.myLen(obj.Ship_To_Location) > 0 Then
                        obj.Invoice_Type = GetInvoiceType(obj.Customer_Code, obj.Ship_To_Location, "S", trans)
                    Else
                        obj.Invoice_Type = GetInvoiceType(obj.Customer_Code, obj.Bill_To_Location, "B", trans)
                    End If
                End If
            End If
            ''''' invoice type ends here

            'sanjay 09/july/2018 BHA/21/06/18-000064
            If obj.Screen_Type = "DS" Then
                If clsCommon.CompairString(obj.Trans_Type, "FS") = CompairStringResult.Equal Then
                    obj.Invoice_Type = "R"
                Else
                    obj.Invoice_Type = "T"
                End If

            End If
            'sanjay 09/july/2018

            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))

            If obj.Podate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Podate, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            End If

            '-----------------richa 27/06/2014 Ticket No .BM00000002982--------------------------------
            Dim isIncrementCounter As Boolean = True
            If obj.Mannual_Invoice_No > 0 OrElse clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                isIncrementCounter = False
            End If
            Dim Doc_Code As String = Nothing


            If obj.Mannual_Invoice_No > 0 Then
                If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                ElseIf clsCommon.CompairString(obj.Invoice_Type, "R") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                ElseIf clsCommon.CompairString(obj.Invoice_Type, "S") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceService, obj.Bill_To_Location, False, isIncrementCounter)
                End If

                Dim strDocLike As String = ""
                Dim arr As Array = Doc_Code.ToCharArray()
                For jj As Integer = 0 To arr.Length - clsCommon.myLen(obj.Mannual_Invoice_No) - 1
                    strDocLike += clsCommon.myCstr(arr(jj))
                Next
                strDocLike += clsCommon.myCstr(obj.Mannual_Invoice_No)
                Doc_Code = strDocLike
                obj.Mannual_Invoice_No = Doc_Code

            ElseIf clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                Doc_Code = obj.InvoiceManualNowithPrefix
                obj.InvoiceManualNowithPrefix = Doc_Code
            End If

            '---------------------------------------------------------------------------------------

            clsCommon.AddColumnsForChange(coll, "Payment_Type", obj.Payment_Type)
            clsCommon.AddColumnsForChange(coll, "Payment_Rate", obj.Payment_Rate)
            clsCommon.AddColumnsForChange(coll, "Charge_For", obj.Charge_For)
            clsCommon.AddColumnsForChange(coll, "Payment_Amount", obj.Payment_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Item_Weight", obj.Total_Item_Weight)
            clsCommon.AddColumnsForChange(coll, "DeliveryStatus", obj.DeliveryStatus)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
            clsCommon.AddColumnsForChange(coll, "Lorry_No", obj.Lorry_No)
            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "Transporter_Name", obj.Transporter_Name)
            clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
            clsCommon.AddColumnsForChange(coll, "Freight_Amount", obj.Freight_Amount)
            clsCommon.AddColumnsForChange(coll, "CrateQty", obj.CrateQty)
            clsCommon.AddColumnsForChange(coll, "IsSampling", IIf(obj.IsSampling, 1, 0))
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            If clsCommon.CompairString(obj.Screen_Type, "DS") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Crate", obj.CrateQty)
            End If
            clsCommon.AddColumnsForChange(coll, "isCardSale", obj.isCardSale)
            clsCommon.AddColumnsForChange(coll, "TotalCAN", obj.TotalCAN)
            clsCommon.AddColumnsForChange(coll, "ShippedCAN", obj.ShippedCAN)
            clsCommon.AddColumnsForChange(coll, "SCREEN_TYPE", obj.Screen_Type)
            clsCommon.AddColumnsForChange(coll, "DO_ITEM_TYPE", obj.DO_Item_Type)
            If obj.Dispatch_date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Dispatch_date", clsCommon.GetPrintDate(obj.Dispatch_date, "dd/MMM/yyyy"))
            End If
            If obj.Sale_Invoice_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Sale_Invoice_Date", clsCommon.GetPrintDate(obj.Sale_Invoice_Date, "dd/MMM/yyyy"))
            End If
            'Sanjay BHA/21/06/18-000064
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Internal", IIf(obj.Is_Internal, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Inv_No", obj.Inv_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location)
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
            'clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code, True)
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ShippingThrough", obj.ShippingThrough, True)
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
            'clsCommon.AddColumnsForChange(coll, "Against_Sales_Order", obj.Against_Sales_Order, True)
            clsCommon.AddColumnsForChange(coll, "Against_Delivery_Code", obj.Against_Delivery_Code, True)
            clsCommon.AddColumnsForChange(coll, "Against_POS", obj.Against_POS, True)
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

            If obj.Challan_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            End If
            If obj.Inv_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Inv_Date", clsCommon.GetPrintDate(obj.Inv_Date, "dd/MMM/yyyy"))
            End If


            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Invoice", IIf(obj.Is_Create_Auto_Invoice, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Sale_Invoice_No", obj.Sale_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Receipt", IIf(obj.Is_Create_Auto_Receipt, 1, 0))

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
            clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Invoice_Type)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", obj.Is_Taxable)
            clsCommon.AddColumnsForChange(coll, "Price_Group_Code", obj.Price_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_PO_No", obj.Cust_PO_No)
            clsCommon.AddColumnsForChange(coll, "Form_38_No", obj.Form_38_No)
            ''added by richa 02 Apr,2019
            clsCommon.AddColumnsForChange(coll, "OPKm", obj.OPKm)
            clsCommon.AddColumnsForChange(coll, "CLKm", obj.CLKm)
            '-----------------richa 27/06/2014 Ticket No .BM00000002982--------------------------------
            clsCommon.AddColumnsForChange(coll, "Mannual_Invoice_No", obj.Mannual_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Mannual_Invoice_No_StringType", obj.InvoiceManualNowithPrefix)
            '
            ' done by priti SWA/20/08/18-000045 for swadesh to save leakage amount.
            'Dim dblLeakagePercentage As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.leakagededuction_freshsale, clsFixedParameterCode.leakagededuction_freshsale, trans))
            'Dim CalculateLeakageAmount As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateLeakageAmount, clsFixedParameterCode.CalculateLeakageAmount, trans))
            'Dim dblLeakAmount As Decimal = 0
            'If dblLeakagePercentage > 0 Then
            '    dblLeakAmount = Math.Round((dblLeakagePercentage * obj.Total_Amt) / 100, 2)
            '    obj.LeakageAmount = dblLeakAmount
            '    obj.Total_Amt = obj.Total_Amt - dblLeakAmount
            '    'qry = "Update TSPL_SD_SHIPMENT_HEAD set Total_Amt =" & obj.Total_Amt - dblLeakAmount & ",LeakageAmount=" & dblLeakAmount & "  where Document_Code='" + obj.Document_Code + "'"
            '    'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'End If
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "LeakageAmount", obj.LeakageAmount)

            clsCommon.AddColumnsForChange(coll, "Manual_Driver_Name", obj.Manual_Driver_Name)
            clsCommon.AddColumnsForChange(coll, "Manual_Salesman_Name", obj.Manual_Salesman_Name)
            clsCommon.AddColumnsForChange(coll, "Shift_Type", obj.Shift_Type, True)
            clsCommon.AddColumnsForChange(coll, "Distributor_Commission_TotalAmt", obj.Distributor_Commission_TotalAmt)


            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Booking_No", obj.Booking_No, True)
                If obj.Booking_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Booking_Date", clsCommon.GetPrintDate(obj.Booking_Date, "dd/MMM/yyyy hh:mm tt"))
                End If
                clsCommon.AddColumnsForChange(coll, "AlternateVehicle", obj.AlternateVehicle)
                clsCommon.AddColumnsForChange(coll, "ManualVehicle", obj.ManualVehicle)

                clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SHIPMENT_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsDispatchNoteFreshSaleDetail.SaveData(obj.Document_Code, Arr, trans, obj.Document_Date, obj.Bill_To_Location, obj)
            ''''
            isSaved = isSaved AndAlso clsPSShipmentChecklistDetail.SaveData(obj.Document_Code, obj.ArrChkList, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            '''' to save item weight unit
            qry = "update TSPL_SD_shipment_DETAIL set Weight_UOM= (select Weight_UOM from TSPL_ITEM_MASTER where Item_Code=TSPL_SD_shipment_DETAIL.Item_Code)  where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_SD_SHIPMENT_HEAD", trans)
            '''' 
            Dim CreateFreshInvoiceOnDispatchSave As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateFreshInvoiceOnDispatchSave, clsFixedParameterCode.CreateFreshInvoiceOnDispatchSave, trans))
            If CreateFreshInvoiceOnDispatchSave = 1 Then
                Dim objSI As clsSaleInvoiceFreshSale = ConvertShipmentToSaleInvoice(obj, trans)
                objSI.SaveData(objSI, True, trans)
                obj.Sale_Invoice_No = objSI.Document_Code

                qry = "Update TSPL_SD_SHIPMENT_HEAD set Sale_Invoice_No ='" + obj.Sale_Invoice_No + "' where Document_Code='" + obj.Document_Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Function UpdateDataBatchWise(ByVal obj As clsDispatchNoteFreshSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal strTransType As String) As Boolean

        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fresh Sale", "Fresh Dispatch Multiple", obj.Bill_To_Location, obj.Document_Date, trans)
            clsBatchInventory.DeleteData(strTransType, obj.Document_Code, trans)

            isSaved = isSaved AndAlso clsDispatchNoteFreshSaleDetail.UpdateDataBatchWiseDetail(obj.Document_Code, Arr, trans, obj.Document_Date, obj.Bill_To_Location, obj, strTransType)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function checkSaveNotification(ByVal strDate As String, ByVal strCustCode As String, ByVal trans As SqlTransaction)
        Try
            Dim CreditLimit As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Credit_Limit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + strCustCode + "'", trans))
            Dim qry As String
            Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNSalesOrder, trans)
            For Each dr As DataRow In dt.Rows
                'Criteria, Notification, Validation
                If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Then
                    qry = "Select COUNT(*) from TSPL_SD_SHIPMENT_HEAD WHERE Status<>1 AND Customer_Code='" + strCustCode + "' AND CONVERT(Date, Due_Date,103)<'" + clsCommon.GetPrintDate(strDate, "dd/MMM/yyyy") + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                        If clsCommon.CompairString(dr("Validation"), "Warning") = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                ElseIf clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Then
                    qry = "Select SUM(Total_Amt) from TSPL_SD_SHIPMENT_HEAD WHERE Status<>1 AND Customer_Code='" + strCustCode + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > CreditLimit Then
                        If clsCommon.CompairString(dr("Validation"), "Warning") = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function checkApprovalNotification(ByVal strDate As String, ByVal strCustCode As String, ByVal trans As SqlTransaction)
        Try
            Dim CreditLimit As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Credit_Limit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + strCustCode + "'", trans))
            Dim qry As String
            Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNSalesOrder, trans)
            For Each dr As DataRow In dt.Rows
                'Criteria, Notification, Validation
                If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Then
                    qry = "Select COUNT(*) from TSPL_SD_SHIPMENT_HEAD WHERE Status<>1 AND Customer_Code='" + strCustCode + "' AND CONVERT(Date, Due_Date,103)<'" + clsCommon.GetPrintDate(strDate, "dd/MMM/yyyy") + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                        If clsCommon.CompairString(dr("Validation"), "Stop Approval") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                ElseIf clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Then
                    qry = "Select SUM(Total_Amt) from TSPL_SD_SHIPMENT_HEAD WHERE Status<>1 AND Customer_Code='" + strCustCode + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > CreditLimit Then
                        If clsCommon.CompairString(dr("Validation"), "Stop Approval") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDispatchNoteFreshSale
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDispatchNoteFreshSale
        Dim obj As clsDispatchNoteFreshSale = Nothing
        Dim qry As String = "SELECT TSPL_SD_SHIPMENT_HEAD.isCardSale,TSPL_SD_SHIPMENT_HEAD.ShippingThrough,TSPL_SD_SHIPMENT_HEAD.Sub_Location_code,TSPL_SD_SHIPMENT_HEAD.Is_Taxable,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.Manual_Driver_Name,TSPL_SD_SHIPMENT_HEAD.Manual_Salesman_Name ,TSPL_SD_SHIPMENT_HEAD.OPKm,TSPL_SD_SHIPMENT_HEAD.CLKm,TSPL_SD_SHIPMENT_HEAD.LeakageAmount,TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,TSPL_SD_SHIPMENT_HEAD.ManualVehicle,TSPL_SD_SHIPMENT_HEAD.Payment_Type,TSPL_SD_SHIPMENT_HEAD.Payment_Rate,TSPL_SD_SHIPMENT_HEAD.Charge_For,TSPL_SD_SHIPMENT_HEAD.Payment_Amount,TSPL_SD_SHIPMENT_HEAD.Total_Item_Weight,TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt,TSPL_SD_SHIPMENT_HEAD.cust_po_date,TSPL_SD_SHIPMENT_HEAD.Cust_PO_No,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code,TSPL_SD_SHIPMENT_HEAD.price_group_code,TSPL_SD_SHIPMENT_HEAD.Invoice_Type,TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per,TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt,TSPL_SD_SHIPMENT_HEAD.TotCashDiscAmt,TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_SD_SHIPMENT_HEAD.Route_Desc,TSPL_SD_SHIPMENT_HEAD.Price_Code,TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SHIPMENT_HEAD.Status,TSPL_SD_SHIPMENT_HEAD.On_Hold,TSPL_SD_SHIPMENT_HEAD.Ref_No,TSPL_SD_SHIPMENT_HEAD.Description,TSPL_SD_SHIPMENT_HEAD.Remarks,TSPL_SD_SHIPMENT_HEAD.Tax_Group,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SHIPMENT_HEAD.Ship_To_Location,TSPL_SD_SHIPMENT_HEAD.TAX1,TSPL_SD_SHIPMENT_HEAD.TAX1_Rate,TSPL_SD_SHIPMENT_HEAD.TAX1_Amt,TSPL_SD_SHIPMENT_HEAD.TAX1_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX2,TSPL_SD_SHIPMENT_HEAD.TAX2_Rate,TSPL_SD_SHIPMENT_HEAD.TAX2_Amt,TSPL_SD_SHIPMENT_HEAD.TAX2_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX3,TSPL_SD_SHIPMENT_HEAD.TAX3_Rate,TSPL_SD_SHIPMENT_HEAD.TAX3_Amt,TSPL_SD_SHIPMENT_HEAD.TAX3_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX4,TSPL_SD_SHIPMENT_HEAD.TAX4_Rate,TSPL_SD_SHIPMENT_HEAD.TAX4_Amt,TSPL_SD_SHIPMENT_HEAD.TAX4_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX5,TSPL_SD_SHIPMENT_HEAD.TAX5_Rate,TSPL_SD_SHIPMENT_HEAD.TAX5_Amt,TSPL_SD_SHIPMENT_HEAD.TAX5_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX6,TSPL_SD_SHIPMENT_HEAD.TAX6_Rate,TSPL_SD_SHIPMENT_HEAD.TAX6_Amt,TSPL_SD_SHIPMENT_HEAD.TAX6_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX7,TSPL_SD_SHIPMENT_HEAD.TAX7_Rate,TSPL_SD_SHIPMENT_HEAD.TAX7_Amt,TSPL_SD_SHIPMENT_HEAD.TAX7_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX8,TSPL_SD_SHIPMENT_HEAD.TAX8_Rate,TSPL_SD_SHIPMENT_HEAD.TAX8_Amt,TSPL_SD_SHIPMENT_HEAD.TAX8_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX9,TSPL_SD_SHIPMENT_HEAD.TAX9_Rate,TSPL_SD_SHIPMENT_HEAD.TAX9_Amt,TSPL_SD_SHIPMENT_HEAD.TAX9_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX10,TSPL_SD_SHIPMENT_HEAD.TAX10_Rate,TSPL_SD_SHIPMENT_HEAD.TAX10_Amt,TSPL_SD_SHIPMENT_HEAD.TAX10_Base_Amt,TSPL_SD_SHIPMENT_HEAD.Discount_Base,TSPL_SD_SHIPMENT_HEAD.Discount_Amt,TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount,TSPL_SD_SHIPMENT_HEAD.Total_Tax_Amt,TSPL_SD_SHIPMENT_HEAD.Comments,TSPL_SD_SHIPMENT_HEAD.Comp_Code,TSPL_SD_SHIPMENT_HEAD.Terms_Code,TSPL_SD_SHIPMENT_HEAD.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_SD_SHIPMENT_HEAD.Posting_Date,TSPL_SD_SHIPMENT_HEAD.Total_Amt,TSPL_SD_SHIPMENT_HEAD.Carrier,TSPL_SD_SHIPMENT_HEAD.VehicleNo,TSPL_SD_SHIPMENT_HEAD.GRNo,TSPL_SD_SHIPMENT_HEAD.GENo,TSPL_SD_SHIPMENT_HEAD.GEDate, TSPL_SD_SHIPMENT_HEAD.Dept,TSPL_SD_SHIPMENT_HEAD.Dept_Desc,TSPL_SD_SHIPMENT_HEAD.Item_Type,TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ,TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code9 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name9,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt9 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code10 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name10,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt10,TSPL_SD_SHIPMENT_HEAD.Total_Add_Charge,TSPL_SD_SHIPMENT_HEAD.Tax_Calculation_Type,TSPL_SD_SHIPMENT_HEAD.Challan_No, TSPL_SD_SHIPMENT_HEAD.Challan_Date, TSPL_SD_SHIPMENT_HEAD.Inv_Date,TSPL_SD_SHIPMENT_HEAD.Inv_No,TSPL_SD_SHIPMENT_HEAD.Is_Internal,TSPL_SD_SHIPMENT_HEAD.Is_Create_Auto_Invoice,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Is_Create_Auto_Receipt,TSPL_SD_SHIPMENT_HEAD.Salesman_Code ,TSPL_SD_SHIPMENT_HEAD.Salesman_Name,  "
        qry += " TSPL_SD_SHIPMENT_HEAD.CURRENCY_CODE,TSPL_SD_SHIPMENT_HEAD.CONVRATE,TSPL_SD_SHIPMENT_HEAD.APPLICABLEFROM,TSPL_SD_SHIPMENT_HEAD.PRoject_ID ,TSPL_SD_SHIPMENT_HEAD.Mannual_Invoice_No,TSPL_SD_SHIPMENT_HEAD. Mannual_Invoice_No_StringType,TSPL_SD_SHIPMENT_HEAD.Form_38_No"
        qry += " ,TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code,TSPL_SD_SHIPMENT_HEAD.Booking_No,TSPL_SD_SHIPMENT_HEAD.Booking_Date,TSPL_SD_SHIPMENT_HEAD.DeliveryStatus,TSPL_SD_SHIPMENT_HEAD.Vehicle_Capacity,TSPL_SD_SHIPMENT_HEAD.Lorry_No,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_SD_SHIPMENT_HEAD.Transporter_Name,TSPL_SD_SHIPMENT_HEAD.Freight,TSPL_SD_SHIPMENT_HEAD.Freight_Amount,TSPL_SD_SHIPMENT_HEAD.CrateQty "
        qry += " ,isnull(TSPL_SD_SHIPMENT_HEAD.TotalCAN,0) as TotalCAN,isnull(TSPL_SD_SHIPMENT_HEAD.ShippedCAN,0) as ShippedCAN,ISNULL(TSPL_SD_SHIPMENT_HEAD.Screen_Type,'') AS Screen_Type,ISNULL(TSPL_SD_SHIPMENT_HEAD.DO_Item_Type,'') AS DO_Item_Type,TSPL_SD_SHIPMENT_HEAD.Dispatch_date,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_Date,TSPL_SD_SHIPMENT_HEAD.Shift_Type "
        qry += " FROM TSPL_SD_SHIPMENT_HEAD"
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location "
        qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Location "
        qry += " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_SD_SHIPMENT_HEAD.Tax_Group "
        qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_SD_SHIPMENT_HEAD.Terms_Code "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code where 2=2 "
        Dim whrCls As String = ""
        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = FrmMainTranScreen.CustomerPermission()
        End If
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        'End If

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrCls = " and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = "  and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrCls = " and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' AND TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ")"
        Else
            whrCls = " and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS'"
        End If
        '-----------------------------------------------------

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SD_SHIPMENT_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SHIPMENT_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SD_SHIPMENT_HEAD where Document_Code>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SHIPMENT_HEAD where Document_Code<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDispatchNoteFreshSale()
            obj.LeakageAmount = clsCommon.myCdbl(dt.Rows(0)("LeakageAmount"))
            obj.AlternateVehicle = clsCommon.myCstr(dt.Rows(0)("AlternateVehicle"))
            obj.ManualVehicle = clsCommon.myCstr(dt.Rows(0)("ManualVehicle"))
            obj.Payment_Type = clsCommon.myCstr(dt.Rows(0)("Payment_Type"))
            obj.Payment_Rate = clsCommon.myCstr(dt.Rows(0)("Payment_Rate"))
            obj.ShippingThrough = clsCommon.myCstr(dt.Rows(0)("ShippingThrough"))
            obj.Charge_For = clsCommon.myCstr(dt.Rows(0)("Charge_For"))
            obj.Payment_Amount = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
            obj.Total_Item_Weight = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Weight"))
            obj.Booking_No = clsCommon.myCstr(dt.Rows(0)("Booking_No"))
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
            obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_code"))
            obj.isCardSale = clsCommon.myCdbl(dt.Rows(0)("isCardSale"))
            ''richa agarwal  added on 2 Apr,2019
            obj.OPKm = clsCommon.myCdbl(dt.Rows(0)("OPKm"))
            obj.CLKm = clsCommon.myCdbl(dt.Rows(0)("CLKm"))
            If dt.Rows(0)("Booking_Date") IsNot DBNull.Value Then
                obj.Booking_Date = clsCommon.myCDate(dt.Rows(0)("Booking_Date"))
            End If

            obj.DeliveryStatus = clsCommon.myCstr(dt.Rows(0)("DeliveryStatus"))
            obj.Vehicle_Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Lorry_No = clsCommon.myCstr(dt.Rows(0)("Lorry_No"))
            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Freight = clsCommon.myCstr(dt.Rows(0)("Freight"))
            obj.Freight_Amount = clsCommon.myCdbl(dt.Rows(0)("Freight_Amount"))
            obj.CrateQty = clsCommon.myCdbl(dt.Rows(0)("CrateQty"))
            'sanjay BHA/21/06/18-000064
            If dt.Rows(0)("Dispatch_date") IsNot DBNull.Value Then
                obj.Dispatch_date = clsCommon.myCDate(dt.Rows(0)("Dispatch_date"))
            End If
            If dt.Rows(0)("Sale_Invoice_Date") IsNot DBNull.Value Then
                obj.Sale_Invoice_Date = clsCommon.myCDate(dt.Rows(0)("Sale_Invoice_Date"))
            End If
            obj.TotalCAN = clsCommon.myCdbl(dt.Rows(0)("TotalCAN"))
            obj.ShippedCAN = clsCommon.myCdbl(dt.Rows(0)("ShippedCAN"))
            obj.Screen_Type = clsCommon.myCstr(dt.Rows(0)("Screen_Type"))
            obj.DO_Item_Type = clsCommon.myCstr(dt.Rows(0)("DO_Item_Type"))
            'sanjay BHA/21/06/18-000064
            obj.Against_Delivery_Code = clsCommon.myCstr(dt.Rows(0)("Against_Delivery_Code"))

            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Is_Internal = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Internal")) = 1, True, False)
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
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
            If dt.Rows(0)("Shift_Type") IsNot DBNull.Value Then
                obj.Shift_Type = clsCommon.myCDate(dt.Rows(0)("Shift_Type"))
            End If
            obj.Manual_Driver_Name = clsCommon.myCstr(dt.Rows(0)("Manual_Driver_Name"))
            obj.Manual_Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Manual_Salesman_Name"))

            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))

            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If


            obj.Challan_No = clsCommon.myCdbl(dt.Rows(0)("Challan_No"))
            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("Carrier"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))
            If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
            End If




            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))

            obj.Against_Sales_Order = clsCommon.myCstr(dt.Rows(0)("Against_Sales_Order"))


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

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Is_Create_Auto_Invoice = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_Invoice")) = 1, True, False)
            obj.Sale_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_No"))
            obj.Is_Create_Auto_Receipt = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_Receipt")) = 1, True, False)

            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Salesman_Name"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.HeadDisc_Per = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Per"))
            obj.HeadDisc_PerAmt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_PerAmt"))
            obj.HeadDisc_Amt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Amt"))
            obj.TotCashDiscAmt = clsCommon.myCdbl(dt.Rows(0)("TotCashDiscAmt"))
            obj.Invoice_Type = clsCommon.myCstr(dt.Rows(0)("Invoice_Type"))
            obj.Price_Group_Code = clsCommon.myCstr(dt.Rows(0)("Price_Group_Code"))
            obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
            If dt.Rows(0)("cust_po_date") IsNot DBNull.Value Then
                obj.Podate = clsCommon.myCDate(dt.Rows(0)("cust_po_date"))
            End If
            obj.Form_38_No = clsCommon.myCstr(dt.Rows(0)("Form_38_No"))
            obj.Mannual_Invoice_No = clsCommon.myCdbl(dt.Rows(0)("Mannual_Invoice_No"))
            'richa Ticket No.BM00000002982-------------
            obj.InvoiceManualNowithPrefix = clsCommon.myCstr(dt.Rows(0)("Mannual_Invoice_No_StringType"))
            '  ---------------------------------------------
            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 
            obj.Invoice_No = obj.Sale_Invoice_No

            qry = "SELECT TSPL_SD_SHIPMENT_DETAIL.ItemLeakageAmount, isnull(TSPL_SD_SHIPMENT_DETAIL.CAN,0) as can,isnull(TSPL_SD_SHIPMENT_DETAIL.ManualCan,0) AS ManualCan,TSPL_SD_SHIPMENT_DETAIL.Sampling, TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Amount,TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Type,TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Pers,TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Code, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Scheme_Item_UOM,TSPL_SD_SHIPMENT_DETAIL.Scheme_Qty,TSPL_SD_SHIPMENT_DETAIL.Scheme_Item_Code,TSPL_SD_SHIPMENT_DETAIL.Scheme_Type,TSPL_SD_SHIPMENT_DETAIL.OrgUnit_code,TSPL_SD_SHIPMENT_DETAIL.Is_Mannual_Amt,TSPL_SD_SHIPMENT_DETAIL.Document_Code,TSPL_SD_SHIPMENT_DETAIL.Line_No, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Status,TSPL_SD_SHIPMENT_DETAIL.Row_Type,TSPL_SD_SHIPMENT_DETAIL.status,TSPL_SD_SHIPMENT_DETAIL.Item_Code, " & _
            "TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SHIPMENT_DETAIL.Qty,TSPL_SD_SHIPMENT_DETAIL.Free_Qty,TSPL_SD_SHIPMENT_DETAIL.Order_Code, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Order_Code,TSPL_SD_SHIPMENT_DETAIL.Balance_Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_DETAIL.Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX1,TSPL_SD_SHIPMENT_DETAIL.TAX1_Rate, " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX2,TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt, " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX3,TSPL_SD_SHIPMENT_DETAIL.TAX3_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX4 , " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX4_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX5,TSPL_SD_SHIPMENT_DETAIL.TAX5_Rate , " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX6,TSPL_SD_SHIPMENT_DETAIL.TAX6_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt, " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX7,TSPL_SD_SHIPMENT_DETAIL.TAX7_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX8, " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX8_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX9,TSPL_SD_SHIPMENT_DETAIL.TAX9_Rate, " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX10,TSPL_SD_SHIPMENT_DETAIL.TAX10_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Amount,TSPL_SD_SHIPMENT_DETAIL.Disc_Per,TSPL_SD_SHIPMENT_DETAIL.Disc_Amt,TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt,TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName, " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX1_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX2_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX3_Base_Amt , " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX4_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX5_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX6_Base_Amt, " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX7_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX8_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX9_Base_Amt, " & _
            "TSPL_SD_SHIPMENT_DETAIL.TAX10_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.MRP,TSPL_SD_SHIPMENT_DETAIL.Batch_No,TSPL_SD_SHIPMENT_DETAIL.MFG_Date, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Expiry_Date,TSPL_SD_SHIPMENT_DETAIL.Specification,TSPL_SD_SHIPMENT_DETAIL.Remarks,TSPL_SD_SHIPMENT_DETAIL.Assessable, " & _
            "TSPL_SD_SHIPMENT_DETAIL.AssessableAmt,TSPL_SD_SHIPMENT_DETAIL.Bar_Code, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Scheme_Applicable,TSPL_SD_SHIPMENT_DETAIL.Scheme_Code, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Scheme_Item,TSPL_SD_SHIPMENT_DETAIL.Item_Tax,TSPL_SD_SHIPMENT_DETAIL.Total_MRP_Amt, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Total_Basic_Amt,TSPL_SD_SHIPMENT_DETAIL.Total_Disc_Amt,TSPL_SD_SHIPMENT_DETAIL.Cust_Discount, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Total_Cust_Discount,TSPL_SD_SHIPMENT_DETAIL.ActualRate,TSPL_SD_SHIPMENT_DETAIL.Cust_DiscountQty, " & _
            "TSPL_SD_SHIPMENT_DETAIL.Price_code,TSPL_SD_SHIPMENT_DETAIL.Abatement_Per,TSPL_SD_SHIPMENT_DETAIL.Abatement_Amt, " & _
            "TSPL_SD_SHIPMENT_DETAIL.FOC_Item,TSPL_SD_SHIPMENT_DETAIL.Item_Weight,TSPL_SD_SHIPMENT_DETAIL.Price_Date, " & _
            "TSPL_SD_SHIPMENT_DETAIL.HeadDiscPer,TSPL_SD_SHIPMENT_DETAIL.HeadDiscPerAmt,TSPL_SD_SHIPMENT_DETAIL.Bin_No,TSPL_SD_SHIPMENT_DETAIL.TotalItem_Weight,TSPL_SD_SHIPMENT_DETAIL.Conv_Factor,TSPL_SD_SHIPMENT_DETAIL.Purchase_Cost,TSPL_SD_SHIPMENT_DETAIL.OrgRate,  " & _
            "TSPL_SD_SHIPMENT_DETAIL.vendor_code,TSPL_SD_SHIPMENT_DETAIL.vendor_desc,TSPL_SD_SHIPMENT_DETAIL.PrincipleCode,TSPL_SD_SHIPMENT_DETAIL.PrincipleDesc,TSPL_SD_SHIPMENT_DETAIL.Markup_On,TSPL_SD_SHIPMENT_DETAIL.Markup_Percent,TSPL_SD_SHIPMENT_DETAIL.Landing_Cost,TSPL_SD_SHIPMENT_DETAIL.HeadDiscAmt,TSPL_SD_SHIPMENT_DETAIL.CustDiscPer,TSPL_SD_SHIPMENT_DETAIL.CasdDiscScheme_Code,TSPL_SD_SHIPMENT_DETAIL.Crate "
            qry += " ,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,TSPL_SD_SHIPMENT_DETAIL.DeliverQty FROM TSPL_SD_SHIPMENT_DETAIL "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code"
            qry += " where TSPL_SD_SHIPMENT_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_SD_SHIPMENT_DETAIL.Line_No  asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDispatchNoteFreshSaleDetail)
                Dim objTr As clsDispatchNoteFreshSaleDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDispatchNoteFreshSaleDetail
                    objTr.Sampling = clsCommon.myCdbl(dr("Sampling"))
                    objTr.Cash_Scheme_Code = clsCommon.myCstr(dr("Cash_Scheme_Code"))
                    objTr.Cash_Scheme_Type = clsCommon.myCstr(dr("Cash_Scheme_Type"))
                    objTr.Cash_Scheme_Pers = clsCommon.myCdbl(dr("Cash_Scheme_Pers"))
                    objTr.Cash_Scheme_Amount = clsCommon.myCdbl(dr("Cash_Scheme_Amount"))

                    objTr.Scheme_Type = clsCommon.myCstr(dr("Scheme_Type"))
                    objTr.Scheme_Qty = clsCommon.myCdbl(dr("Scheme_Qty"))
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme_Item_UOM"))
                    objTr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme_Item_Code"))

                    objTr.OrgUnit_code = clsCommon.myCstr(dr("OrgUnit_code"))
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Delivery_Code = clsCommon.myCstr(dr("Delivery_Code"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Bar_Code = clsCommon.myCstr(dr("Bar_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.DeliverQty = clsCommon.myCdbl(dr("DeliverQty"))

                    objTr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))
                    objTr.Order_Code = clsCommon.myCstr(dr("Order_Code"))

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

                    ' ''objTr.Landed_Cost_Rate = clsCommon.myCdbl(dr("Landed_Cost_Rate"))
                    ' ''objTr.Landed_Cost_Amount = clsCommon.myCdbl(dr("Landed_Cost_Amount"))

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
                    'objTr.Unit_Cogs = clsCommon.myCdbl(dr("Unit_Cogs"))

                    objTr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                    objTr.Item_Tax = clsCommon.myCdbl(dr("Item_Tax"))
                    objTr.Total_MRP_Amt = clsCommon.myCdbl(dr("Total_MRP_Amt"))
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                    objTr.Total_Disc_Amt = clsCommon.myCdbl(dr("Total_Disc_Amt"))
                    objTr.Cust_Discount = clsCommon.myCdbl(dr("Cust_Discount"))
                    objTr.Total_Cust_Discount = clsCommon.myCdbl(dr("Total_Cust_Discount"))
                    objTr.ActualRate = clsCommon.myCdbl(dr("ActualRate"))
                    objTr.Cust_DiscountQty = clsCommon.myCdbl(dr("Cust_DiscountQty"))
                    objTr.Price_code = clsCommon.myCstr(dr("Price_code"))
                    If dr("Price_Date") IsNot DBNull.Value Then
                        objTr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    End If

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
                    objTr.PrincipleCode = clsCommon.myCstr(dr("PrincipleCode"))
                    objTr.PrincipleDesc = clsCommon.myCstr(dr("PrincipleDesc"))
                    objTr.vendor_code = clsCommon.myCstr(dr("vendor_code"))
                    objTr.vendor_desc = clsCommon.myCstr(dr("vendor_desc"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    objTr.HeadDiscPer = clsCommon.myCdbl(dr("HeadDiscPer"))
                    objTr.HeadDiscPerAmt = clsCommon.myCdbl(dr("HeadDiscPerAmt"))
                    '' Anubhooti 12-Sep-2014 BM00000003847
                    objTr.Crate = clsCommon.myCdbl(dr("Crate"))
                    'sanjay
                    objTr.CanQty = clsCommon.myCdbl(dt.Rows(0)("CAN"))
                    objTr.ManualCanQty = clsCommon.myCdbl(dt.Rows(0)("ManualCan"))
                    objTr.ItemLeakageAmount = clsCommon.myCdbl(dr("ItemLeakageAmount"))
                    'sanjay
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("SD-IN", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData("FS-SH", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    obj.Arr.Add(objTr)
                Next
            End If
            qry = "select * from TSPL_SD_SHIPMENT_CHECKLIST_DETAIL WHERE SHIPMENT_CODE='" + obj.Document_Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrChkList = New List(Of clsPSShipmentChecklistDetail)
                Dim objTrChk As clsPSShipmentChecklistDetail
                For Each dr As DataRow In dt.Rows
                    objTrChk = New clsPSShipmentChecklistDetail
                    objTrChk.Shipment_Code = clsCommon.myCstr(dr("Shipment_Code"))
                    objTrChk.Dispatch_Checklist_Code = clsCommon.myCstr(dr("Dispatch_Checklist_Code"))
                    obj.ArrChkList.Add(objTrChk)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function GetOriginalQty(ByVal strMrnNo As String, ByVal strICode As String, ByVal strUOM As String, ByVal dblAssessable As Double, ByVal dblMRP As Double, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "Select TSPL_MRN_DETAIL.MRN_No,(TSPL_MRN_DETAIL.MRN_Qty+ISNULL(TSPL_MRN_DETAIL.Leak_Qty,0) +ISNULL(TSPL_MRN_DETAIL.Burst_Qty,0)+ISNULL(TSPL_MRN_DETAIL.Short_Qty,0)) as MRN_Qty,TSPL_GRN_DETAIL.GRN_No,(TSPL_GRN_DETAIL.GRN_Qty+ISNULL(TSPL_GRN_DETAIL.Leak_Qty,0) +ISNULL(TSPL_GRN_DETAIL.Burst_Qty,0)+ISNULL(TSPL_GRN_DETAIL.Short_Qty,0)) as GRN_Qty, TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty from TSPL_MRN_DETAIL left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code and TSPL_GRN_DETAIL.Unit_code=TSPL_MRN_DETAIL.Unit_code and isnull(TSPL_GRN_DETAIL.Assessable,0)=isnull(TSPL_MRN_DETAIL.Assessable,0) and isnull(TSPL_GRN_DETAIL.Item_Code,0)=isnull(TSPL_MRN_DETAIL.Item_Code ,0) left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No= TSPL_GRN_DETAIL.PO_Id and TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_PURCHASE_ORDER_DETAIL.Unit_code=  TSPL_GRN_DETAIL.Unit_code and isnull(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0)=  isnull(TSPL_GRN_DETAIL.Assessable,0) and isnull(TSPL_PURCHASE_ORDER_DETAIL.MRP,0)=  isnull(TSPL_GRN_DETAIL.MRP,0) where TSPL_MRN_DETAIL.MRN_No='" + strMrnNo + "' and TSPL_MRN_DETAIL.Item_Code='" + strICode + "' and TSPL_MRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_MRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_MRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'"
        Return clsDBFuncationality.GetDataTable(qry, trans)
        qry = Nothing
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing) As Boolean

        Try
            ''3GN63ZG
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If
            Dim obj As clsDispatchNoteFreshSale = clsDispatchNoteFreshSale.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            checkApprovalNotification(obj.Document_Date, obj.Customer_Code, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("SRN No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""

            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SD_SHIPMENT_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                'trans.Commit()
                Return False
            End If

           CreateInventoryMovement(obj, trans)

            If obj.Is_Create_Auto_Invoice Then
                Dim CreateFreshInvoiceOnDispatchSave As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateFreshInvoiceOnDispatchSave, clsFixedParameterCode.CreateFreshInvoiceOnDispatchSave, trans))
                If CreateFreshInvoiceOnDispatchSave = 0 Then
                    Dim objSI As clsSaleInvoiceFreshSale = ConvertShipmentToSaleInvoice(obj, trans)
                    objSI.SaveData(objSI, True, trans)
                    obj.Sale_Invoice_No = objSI.Document_Code
                End If
             
                clsSaleInvoiceFreshSale.PostData("", obj.Sale_Invoice_No, trans)
            End If

            CreateJournalEntry(obj.Document_Code, trans, strVoucherNoForRecreateOnly)

            qry = "Update TSPL_SD_SHIPMENT_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "',Sale_Invoice_No ='" + obj.Sale_Invoice_No + "' "
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_VEHICLE_MASTER set InOut='O' where Vehicle_Id='" & obj.Vehicle_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)



            If isSaved AndAlso clsCommon.myCdbl(obj.Payment_Amount) > 0 AndAlso clsCommon.myLen(obj.Payment_Type) > 0 Then
                Dim objProv As clsProvisionEntry = New clsProvisionEntry()
                objProv.isNewEntry = True
                objProv.Doc_Date = obj.Document_Date
                Dim strTransporterCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transport_Id from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & obj.Vehicle_Code & "'", trans))
                Dim strTransporterName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transporter_Name from TSPL_TRANSPORT_MASTER where Transport_Id ='" & strTransporterCode & "'", trans))
                objProv.Vendor_Code = strTransporterCode
                objProv.Vendor_Desc = strTransporterName
                objProv.Vendor_Type = "Secondary Transporter"
                objProv.Prov_type = "Freight"
                objProv.Status = "No"
                objProv.Ref_Doc_No = ""
                objProv.Amount = obj.Payment_Amount
                objProv.Prog_Code = FormId
                objProv.Prov_Month = Month(obj.Document_Date)
                objProv.Prov_Year = Year(obj.Document_Date)
                objProv.Comp_Code = obj.Comp_Code
                objProv.Created_By = objCommonVar.CurrentUserCode
                objProv.Created_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                objProv.Modified_By = objCommonVar.CurrentUserCode
                objProv.Loc_Code = obj.Bill_To_Location
                objProv.Loc_Desc = obj.BillToLocationName

                isSaved = isSaved AndAlso clsProvisionEntry.SaveData(objProv, trans)
                If isSaved Then
                    isSaved = isSaved AndAlso clsProvisionEntry.PostData(objProv.Doc_No, trans, False)
                End If
            End If

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateInventoryMovement(ByVal obj As clsDispatchNoteFreshSale, ByVal trans As SqlTransaction)

        Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)

        'Dim strFirstItemCodeNonItemRowType As String = GetFirstItemCode(obj.Arr)
        Dim strRgpNo As String = Nothing
        Dim intCounter As Integer = 0
        For Each objTr As clsDispatchNoteFreshSaleDetail In obj.Arr
            intCounter = intCounter + 1
            If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If

                Dim objLocationDetails As New clsItemLocationDetails()
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                End If

                objLocationDetails.Item_Code = objTr.Item_Code
                objLocationDetails.Item_Desc = objTr.Item_Desc
                objLocationDetails.Location_Code = objTr.Location
                objLocationDetails.Location_Desc = objTr.LocationName
                objLocationDetails.Item_Qty = -1 * (objTr.Qty + objTr.Free_Qty / ConvFac)
                objLocationDetails.Amount = -1 * objTr.Amount
                objLocationDetails.MRP = objTr.MRP * ConvFac
                If objTr.MFG_Date.HasValue Then
                    objLocationDetails.MFG_Date = objTr.MFG_Date
                End If
                objLocationDetails.Batch_No = objTr.Batch_No
                If objTr.Expiry_Date.HasValue Then
                    objLocationDetails.Expiry_Date = objTr.Expiry_Date
                End If
                objLocationDetails.ItemType = strItemTypeToSave
                ArrLocationDetails.Add(objLocationDetails)

                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, objTr.Location)

                objInventoryMovemnt.Cust_Code = obj.Customer_Code
                objInventoryMovemnt.Cust_Name = obj.Customer_Name
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.Qty + objTr.Free_Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                objInventoryMovemnt.MRP = objTr.MRP
                objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                objInventoryMovemnt.Net_Cost = objTr.Total_Tax_Amt
                ''richa 20 Dec,2018 ERO/20/12/18-000448
                objInventoryMovemnt.Is_Scheme_Item = objTr.Scheme_Item
                ''---------------------
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovement.Add(objInventoryMovemnt)
            End If
        Next
        Dim isSaved As Boolean = True
        isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
        isSaved = isSaved AndAlso clsInventoryMovement.SaveData("FS-SH", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

        Return isSaved
    End Function
    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing)
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)


        If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL

            Dim obj As New clsDispatchNoteFreshSale
            obj = clsDispatchNoteFreshSale.GetData(strCode, NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strInventoryControlAc As String = String.Empty
            Dim strShipmentClearingAC As String = String.Empty
            Dim dblTotalCost As Double = 0

            strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
              " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
               " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'", trans)
            strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Bill_To_Location, trans)

            If clsCommon.myLen(strShipmentClearingAC) = 0 Then
                Throw New Exception("Please set Shipment clearing Account for first item")
            End If

            Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & obj.Document_Code & "'", trans))

            Dim Acc() As String = {strShipmentClearingAC, dblCogsCost, "", "", "", "", "", "", "H"}
            ArryLstGLAC.Add(Acc)
            Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.Document_Code & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                    strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Bill_To_Location, trans)

                    If clsCommon.myLen(strInventoryControlAc) = 0 Then
                        Throw New Exception("Please set Inventory Control Account for first item")
                    End If
                    Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost")), "", "", "", "", "", "", "I"}
                    ArryLstGLAC.Add(Acc1)

                    ''richa agarwal 28 Dec,2018 BHA/27/11/18-000718
                   
                    clsInventoryMovement.UpdateInvControlAccount(strCode, "FS-SH", clsCommon.myCstr(dr("Item_Code")), "", strInventoryControlAc, "", trans)
                    '------------------
                Next
            End If
            '' BHA/30/10/18-000646 RICHA AGARWAL SEND CUSTOMER CODE AND CUSTOMER NAME INTO JOURNAL ENTRY AND TYPE C instead of O 30 Oct,2018
            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, strVoucherNoForRecreateOnly, trans, obj.Document_Date, obj.Remarks, "DS-FS", "Dispatch Fresh Sale", obj.Document_Code, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
            Else
                transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, obj.Remarks, "DS-FS", "Dispatch Fresh Sale", obj.Document_Code, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
            End If


            strInventoryControlAc = Nothing
            strShipmentClearingAC = Nothing
        End If  '' Done By Pankaj Jha For Skipping Cogs GL
    End Sub

    Private Shared Function ConvertShipmentToSaleInvoice(ByVal objShipment As clsDispatchNoteFreshSale, ByVal tran As SqlTransaction) As clsSaleInvoiceFreshSale
        Dim obj As New clsSaleInvoiceFreshSale()
        obj = New clsSaleInvoiceFreshSale()
        obj.podate = objShipment.Document_Date
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonDairyDispatchforFreshAmbient, clsFixedParameterCode.CreateCommonDairyDispatchforFreshAmbient, tran)) = 1 Then ''By Balwinder on 10/12/2019
            obj.Invoice_Type = objShipment.Invoice_Type
            obj.Screen_Type = objShipment.Screen_Type
            obj.Is_Taxable = objShipment.Is_Taxable
        Else
            'Sanjay BHA/21/06/18-000064 add Dairy Sale Condition
            If objShipment.Screen_Type = "DS" Then
                obj.Invoice_Type = "R"
                obj.Screen_Type = "DS"
            Else
                obj.Invoice_Type = "N"
            End If
        End If
        obj.Trans_Type = objShipment.Trans_Type
        ''added by richa agarwal 2 Apr,2019
        obj.OPKm = objShipment.OPKm
        obj.CLKm = objShipment.CLKm
        obj.LeakageAmount = objShipment.LeakageAmount
        obj.Document_Code = objShipment.Document_Code
        obj.Document_Date = objShipment.Document_Date
        obj.Customer_Code = objShipment.Customer_Code
        obj.Customer_Name = objShipment.Customer_Name
        obj.isCardSale = objShipment.isCardSale
        obj.Status = IIf(objShipment.Status = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        obj.On_Hold = IIf(objShipment.On_Hold = 1, True, False)
        obj.Is_Internal = IIf(objShipment.Is_Internal = 1, True, False)
        obj.Ref_No = objShipment.Ref_No
        obj.Description = objShipment.Description
        obj.Remarks = objShipment.Remarks
        obj.Bill_To_Location = objShipment.Bill_To_Location
        obj.Ship_To_Location = objShipment.Ship_To_Location
        obj.Sub_Location_code = objShipment.Sub_Location_code
        obj.Tax_Group = objShipment.Tax_Group
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

        obj.Manual_Driver_Name = objShipment.Manual_Driver_Name
        obj.Manual_Salesman_Name = objShipment.Manual_Salesman_Name


        If objShipment.Posting_Date IsNot Nothing Then
            obj.Posting_Date = objShipment.Posting_Date
        End If

        obj.Salesman_Code = objShipment.Salesman_Code
        obj.Salesman_Name = objShipment.Salesman_Name

        obj.Challan_No = objShipment.Challan_No
        obj.Carrier = objShipment.Carrier
        obj.Vehicle_Code = objShipment.Vehicle_Code
        obj.VehicleNo = objShipment.VehicleNo
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
            obj.Inv_Date = ""
        Else
            obj.Inv_Date = clsCommon.GetPrintDate(objShipment.Inv_Date, "dd/MMM/yyyy")
        End If

        obj.Tax_Calculation_Type = IIf(objShipment.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
        obj.Is_Create_Auto_Receipt = objShipment.Is_Create_Auto_Receipt
        '-----------------richa 27/06/2014 Ticket No .BM00000002982----------
        obj.Mannual_Document_Code = objShipment.Mannual_Invoice_No
        obj.InvoiceManualNowithPrefix = objShipment.InvoiceManualNowithPrefix
        '-------------------------------------------------------------------

        obj.Booking_No = objShipment.Booking_No
        obj.Booking_Date = objShipment.Booking_Date
        obj.Vehicle_Capacity = objShipment.Vehicle_Capacity
        obj.Lorry_No = objShipment.Lorry_No
        obj.Road_Permit_No = objShipment.Road_Permit_No
        obj.Transporter_Name = objShipment.Transporter_Name
        obj.Freight = objShipment.Freight
        obj.Freight_Amount = objShipment.Freight_Amount
        obj.CrateQty = objShipment.CrateQty
        obj.Crate = objShipment.CrateQty
        'Sanjay BHA/21/06/18-000064
        obj.TotalCAN = objShipment.TotalCAN
        obj.ShippedCAN = objShipment.ShippedCAN
        obj.Dispatch_date = objShipment.Dispatch_date
        obj.Sale_Invoice_Date = objShipment.Sale_Invoice_Date
        ''richa TEC/13/09/19-001010
        obj.Sampling = IIf(objShipment.IsSampling = True, 1, 0)
        'Sanjay BHA/21/06/18-000064
        obj.RoundOffAmount = objShipment.RoundOffAmount ''ERO/02/08/19-000981 by balwinder on 02/08/2109
        If (objShipment.Arr IsNot Nothing AndAlso objShipment.Arr.Count > 0) Then
            obj.Arr = New List(Of clsSaleInvoiceDetailFreshSale)
            Dim objTr As clsSaleInvoiceDetailFreshSale
            For Each objShipmentDetail As clsDispatchNoteFreshSaleDetail In objShipment.Arr
                objTr = New clsSaleInvoiceDetailFreshSale
                objTr.Delivery_Code = objShipmentDetail.Delivery_Code
                objTr.Crate = objShipmentDetail.Crate

                'Sanjay BHA/21/06/18-000064
                objTr.CanQty = objShipmentDetail.CanQty
                objTr.ManualCanQty = objShipmentDetail.ManualCanQty
                'Sanjay BHA/21/06/18-000064
                objTr.ItemLeakageAmount = objShipmentDetail.ItemLeakageAmount
                objTr.Sampling = objShipmentDetail.Sampling
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
                objTr.Shipment_Code = objShipment.Document_Code
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
                If objShipmentDetail.MFG_Date IsNot Nothing Then
                    objTr.MFG_Date = objShipmentDetail.MFG_Date
                End If
                If objShipmentDetail.Expiry_Date IsNot Nothing Then
                    objTr.Expiry_Date = objShipmentDetail.Expiry_Date
                End If
                objTr.Specification = objShipmentDetail.Specification
                objTr.Remarks = objShipmentDetail.Remarks

                objTr.Scheme_Applicable = IIf((objShipmentDetail.Scheme_Applicable = "Y" OrElse objShipmentDetail.Scheme_Applicable = "Yes"), "Yes", "No")
                objTr.Scheme_Code = objShipmentDetail.Scheme_Code
                objTr.Scheme_Item = IIf((objShipmentDetail.Scheme_Item = "Y" OrElse objShipmentDetail.Scheme_Item = "Yes"), "Yes", "No")
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

                obj.Arr.Add(objTr)
            Next
        End If
        Return obj
        obj = Nothing
    End Function


    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsDispatchNoteFreshSaleDetail)) As String


        For Each objtr As clsDispatchNoteFreshSaleDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsDispatchNoteFreshSale = clsDispatchNoteFreshSale.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsSerializeInvenotry.DeleteData("SD-IN", strCode, trans)

                Dim qry As String = "delete from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
                qry = Nothing
                obj = Nothing
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    Public Shared Function DeleteData(ByVal obj As clsDispatchNoteFreshSale, ByVal strCode As String, ByVal Trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        'Dim obj As clsDispatchNoteFreshSale = clsDispatchNoteFreshSale.GetData(strCode, NavigatorType.Current)
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If

                Dim strInvoiceNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" & strCode & "'", Trans))

                Dim qry As String = "delete from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strInvoiceNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, Trans)

                qry = "delete from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + strInvoiceNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, Trans)

                qry = "delete from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, Trans)

                qry = "delete from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, Trans)

                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, Trans)
                If (isSaved) Then
                    'Trans.Commit()
                Else
                    'Trans.Rollback()
                End If
                qry = Nothing
                obj = Nothing
            Catch ex As Exception
                'Trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    Public Shared Function IsValidCustomer(ByVal Arr As List(Of String), ByVal strCustomerCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Customer_Code not in ('" + strCustomerCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "SRN No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Customer_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Customer_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    ''To be Uncomment
    '    Public Sub SRNPrintOut(ByVal FromDate As Date?, ByVal ToDate As Date?, ByVal IsDocTypeFinsihGoods As Boolean, ByVal ArrSrnNo As ArrayList, ByVal ArrVendor As ArrayList, ByVal ArrLocation As ArrayList)
    '        Dim qry As String

    '        Try
    '            If IsDocTypeFinsihGoods Then
    '                qry = "select Document_Code,MAX(ItemType )as ItemType,MAX(MRN_Date) as Document_Date,MAX(Customer_Name) as Customer_Name,MAX(GRNo) as GRNo,MAX(GENo) as GENo,MAX(GEDate) as GEDate,Item_Code,MAX(Item_Desc) as Item_Desc,MAX(VehicleNo) as VehicleNo, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Burst_Qty) as Burst,SUM(Short_Qty) as Short,MAX(Remarks) as Remarks,max(Ref_No)as Ref_No from( " & _
    '         "select TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD .Item_Type as ItemType," & _
    '         "(replace( CONVERT(varchar(11), TSPL_SD_SHIPMENT_HEAD.Document_Date,104),'.','/')+' '+CONVERT(varchar(100),TSPL_SD_SHIPMENT_HEAD.Document_Date,108) )as MRN_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Name,TSPL_SD_SHIPMENT_HEAD.GRNo,TSPL_SD_SHIPMENT_HEAD.GENo," & _
    '         "(case when LEN(TSPL_SD_SHIPMENT_HEAD.GEDate)>0  then REPLACE( CONVERT(varchar(11), TSPL_SD_SHIPMENT_HEAD.GEDate,104),'.','/') else '' end) as GEDate,TSPL_SD_SHIPMENT_HEAD.VehicleNo,TSPL_SD_SHIPMENT_HEAD.Remarks ,TSPL_SD_SHIPMENT_HEAD.Ref_No,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Item_Desc,TSPL_SD_SHIPMENT_DETAIL.Unit_code," & _
    '         "case when Unit_code='FC' then Qty + ISNULL( Free_Qty,0) end as FCS, " & _
    '         "case when Unit_code='FB' then Qty + ISNULL( Free_Qty,0) end as FBS, " & _
    '         "case when Unit_code='SH' then Qty + ISNULL( Free_Qty,0) end as FSH, " & _
    '         "case when Unit_code='EC' then Qty + ISNULL( Free_Qty,0) end as ECS," & _
    '         "case when Unit_code='EB' then Qty + ISNULL( Free_Qty,0) end as EBS, " & _
    '         "TSPL_SD_SHIPMENT_DETAIL.Leak_Qty,TSPL_SD_SHIPMENT_DETAIL.Burst_Qty,TSPL_SD_SHIPMENT_DETAIL.Short_Qty from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.Document_Code " & _
    '         " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location   where Item_Type ='F'"
    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    qry += " and Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "
    '                End If

    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    qry += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")" 'ADDED BY ABHISHEK AS ON 30 AUG 2012
    '                End If
    '                qry += " )xxx group by Document_Code,Item_Code order by Item_Desc"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    common.clsCommon.MyMessageBoxShow("No Record Found")
    '                Else
    '                    PurchaseOrderViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "rptSRNCustomReport", "SRN Report")

    '                End If
    '            Else ''For RM Other Print out
    '                Dim strquery As String = "SELECT TSPL_SD_SHIPMENT_HEAD.Document_Code, TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Name,(case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else Document_Date end ) as Challan_Date, TSPL_SD_SHIPMENT_HEAD.Ref_No  " & _
    '                      "as Challan_No, TSPL_SD_SHIPMENT_HEAD.Inv_No, TSPL_SD_SHIPMENT_HEAD.Inv_Date, TSPL_SD_SHIPMENT_HEAD.GRNo,TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount ,TSPL_SD_SHIPMENT_HEAD.GENo,TSPL_SD_SHIPMENT_HEAD.Total_Amt, " & _
    '                      "TSPL_SD_SHIPMENT_HEAD.GEDate, TSPL_SD_SHIPMENT_HEAD.VehicleNo, TSPL_SD_SHIPMENT_HEAD.Carrier,TSPL_SD_SHIPMENT_HEAD.Remarks,TSPL_SD_SHIPMENT_DETAIL.Landed_Cost_Rate,TSPL_SD_SHIPMENT_DETAIL.Landed_Cost_Amount , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Row_Type,TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount," & _
    '"TSPL_SD_SHIPMENT_DETAIL.Item_Cost as basicRate,TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt as BasicTotal,TSPL_SD_SHIPMENT_DETAIL.Unit_Cost_Tax_Rate as UCTR," & _
    '"TSPL_SD_SHIPMENT_DETAIL.Unit_Cost_Tax as uctax,TSPL_SD_SHIPMENT_DETAIL.Item_Desc,TSPL_SD_SHIPMENT_DETAIL.Unit_code,TSPL_SD_SHIPMENT_DETAIL.Qty,TSPL_SD_SHIPMENT_DETAIL.Rejected_Qty,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Total_Amt,TSPL_SD_SHIPMENT_DETAIL.ITEM_COST," & _
    ' "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " & _
    '"tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SHIPMENT_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," & _
    '"isnull (TSPL_SD_SHIPMENT_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SD_SHIPMENT_HEAD.tax3_amt,0) as txt3amt," & _
    '"tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SD_SHIPMENT_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," & _
    '"isnull (TSPL_SD_SHIPMENT_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SD_SHIPMENT_HEAD.tax6_amt,0) as txt6amt " & _
    '",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SD_SHIPMENT_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," & _
    '"isnull (TSPL_SD_SHIPMENT_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SD_SHIPMENT_HEAD.tax9_amt,0) as txt9amt," & _
    '"tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SD_SHIPMENT_HEAD.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname, " & _
    '"TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_SD_SHIPMENT_DETAIL.Qty," & _
    '"case when tax1.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax1_amt else null end as Tax1Recoverable," & _
    '"case when tax2.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.TAX2_Amt else null end as Tax2Recoverable, " & _
    '"case when tax3.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax3_amt else null end as Tax3Recoverable, " & _
    '"case when tax4.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax4_amt else null end as Tax4Recoverable, " & _
    '"case when tax5.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax5_amt else null end as Tax5Recoverable, " & _
    '"case when tax6.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax6_amt else null end as Tax6Recoverable," & _
    '"case when tax7.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax7_amt else null end as Tax7Recoverable, " & _
    '"case when tax8.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax8_amt else null end as Tax8Recoverable, " & _
    '"case when tax9.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax9_amt else null end as Tax9Recoverable," & _
    '"case when tax10.Tax_Recoverable='Y' then TSPL_SD_SHIPMENT_HEAD.tax10_amt else null end as Tax10Recoverable, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX1_Rate ,0),103)+'%' as txt1Rate," & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX2_Rate   ,0),103)+'%' as txt2Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX3_Rate  ,0),103)+'%' as txt3Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX4_Rate  ,0),103)+'%' as txt4Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX5_Rate  ,0),103)+'%' as txt5Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX6_Rate  ,0),103)+'%' as txt6Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX7_Rate  ,0),103)+'%' as txt7Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX8_Rate  ,0),103)+'%' as txt8Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX9_Rate  ,0),103)+'%' as txt9Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SHIPMENT_HEAD.TAX10_Rate  ,0),103)+'%' as txt10Rate," & _
    '"TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount as Value,(select SUM(rejected_qty) from TSPL_SD_SHIPMENT_DETAIL where Document_Code=TSPL_SD_SHIPMENT_HEAD.Document_Code) as Rej_qty, (select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_SD_SHIPMENT_DETAIL.Order_Code and TSPL_MRN_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code where Document_Code =TSPL_SD_SHIPMENT_HEAD.Document_Code)as MrnTotQty, (select SUM(Qty) from TSPL_SD_SHIPMENT_DETAIL where Document_Code=TSPL_SD_SHIPMENT_HEAD.Document_Code) as SRNQtyTotal, (select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" & _
    '" ( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= TSPL_SD_SHIPMENT_HEAD.Document_Code " & _
    '" GROUP by TSPL_PI_DETAIL.PI_No)xxx) as PInvNo  ,    " & _
    '       " TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name1 as Add1Name, " & _
    '     " TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt1 as Add1 , " & _
    '     "     TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name2 as Add2Name, " & _
    '     "   TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt2 as Add2 , " & _
    '     "    TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name3 as Add3Name, " & _
    '     "   TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt3 as Add3 , " & _
    '     "    TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name4 as Add4Name, " & _
    '     "    TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt4 as Add4 , " & _
    '     "     TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name5 as Add5Name, " & _
    '      "     TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt5 as Add5 , " & _
    '      "     TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name6 as Add6Name, " & _
    '      "    TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt6 as Add6 , " & _
    '      "    TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name7 as Add7Name, " & _
    '      "     TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt7 as Add7 , " & _
    '      "       TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name8 as Add8Name, " & _
    '      "      TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt8 as Add8 , " & _
    '       "      TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name9 as Add9Name, " & _
    '       "      TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt9 as Add9 , " & _
    '       "      TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name10 as Add10Name, " & _
    '       "     TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt10 as Add10,TSPL_SD_SHIPMENT_HEAD.Against_RGP,TSPL_SD_SHIPMENT_DETAIL .Specification   " & _
    ' " FROM  TSPL_SD_SHIPMENT_DETAIL INNER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_DETAIL.Document_Code = TSPL_SD_SHIPMENT_HEAD.Document_Code " & _
    ' "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_SD_SHIPMENT_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " & _
    ' "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_SD_SHIPMENT_HEAD.Customer_Code = TSPL_VENDOR_MASTER.Customer_Code " & _
    ' "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SHIPMENT_HEAD.tax1  " & _
    ' "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SHIPMENT_HEAD.tax2 " & _
    ' "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SHIPMENT_HEAD .TAX3 " & _
    ' "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SHIPMENT_HEAD .tax4 " & _
    ' "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SHIPMENT_HEAD .tax5 " & _
    ' "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX6  " & _
    ' "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX7  " & _
    ' "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX8 " & _
    ' "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX9 " & _
    ' " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SHIPMENT_HEAD .TAX10  " & _
    ' "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location  " & _
    ' " where TSPL_SD_SHIPMENT_HEAD .Item_Type not in('F')"

    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    strquery += " and Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "

    '                End If
    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    strquery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    strquery += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    strquery += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")  "

    '                End If
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    common.clsCommon.MyMessageBoxShow("No Record Found")
    '                Else
    '                    PurchaseOrderViewer.funreport(dt, "SRNReportThroughReport", "Store Receipt Report")
    '                End If
    '            End If

    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Sub

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select Status from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select distinct DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_DETAIL where Shipment_Code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Shipment is used in following Sale invoice -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("DOCUMENT_CODE"))
                Next
                Throw New Exception(Qry)
            End If


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SD-SH' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='SD-SH'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
            For Each objtr As DataRow In dt.Rows
                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                Dim objLocationDetails As New clsItemLocationDetails()
                objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                ArrLocationDetails.Add(objLocationDetails)
            Next
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='SD-SH'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_SD_SHIPMENT_HEAD set Status = 0 where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsDispatchNoteFreshSaleDetail
#Region "Variables"
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public Sampling As Integer = 0
    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Decimal = Nothing
    Public VS_CashSchemeCode As String = String.Empty
    Public VS_Cash_Amt As Double = 0
    Public VS_ltrInCrate As Double = 0
    Public Sub_Location_Code As String = String.Empty
    Public Scheme_Item_UOM As String = Nothing
    Public Cash_Scheme_Code As String = Nothing
    Public Cash_Scheme_Type As String = Nothing
    Public Cash_Scheme_Pers As Decimal = Nothing
    Public Cash_Scheme_Amount As Decimal = Nothing
    Public CanQty As Double = 0
    Public ManualCanQty As Double = 0
    Public OrgUnit_code As String = ""

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
    Public Delivery_Code As String = Nothing
    Public DeliverQty As Double = 0
    '' Anubhooti 12-Sep-2014 BM00000003847
    Public Crate As Double = 0
    Public ItemLeakageAmount As Decimal = 0
    Public Distributor_Commission_PKID As String = ""
    Public Distributor_Commission_Rate As Decimal = 0
    Public Distributor_Commission_RateWithTax As Decimal = 0
    Public Distributor_Commission_Amt As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsDispatchNoteFreshSaleDetail), ByVal trans As SqlTransaction, ByVal DocDate As DateTime, ByVal strLocation As String, ByVal objShipment As clsDispatchNoteFreshSale) As Boolean
        Dim CalculateLeakageAmount As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateLeakageAmount, clsFixedParameterCode.CalculateLeakageAmount, trans))
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim dblTotLeagageamt As Decimal = 0
            For Each obj As clsDispatchNoteFreshSaleDetail In Arr
                Dim coll As New Hashtable()
                Dim dblLeakageAmt As Decimal = 0
                If objShipment.LeakageAmount > 0 AndAlso (clsCommon.CompairString(obj.Scheme_Item, "No") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Scheme_Item, "N") = CompairStringResult.Equal) Then
                    ''RICHA AGARWAL 18 jUNE,2019 SWA/06/06/19-000068
                    Dim chkLeakageApplicable As Integer = clsDBFuncationality.getSingleValue("select isnull(is_leakage_Not_Applicable,0) from tspl_item_master where item_code='" & obj.Item_Code & "'", trans)
                    If Not (CalculateLeakageAmount = True AndAlso chkLeakageApplicable = True) Then
                        dblLeakageAmt = (clsCommon.myCdbl(obj.Amt_Less_Discount) * objShipment.LeakageAmount) / (objShipment.Amount_Less_Discount)
                        dblTotLeagageamt += dblLeakageAmt
                        obj.ItemLeakageAmount = dblLeakageAmt
                        obj.Item_Net_Amt = obj.Item_Net_Amt - dblLeakageAmt
                        obj.Amt_Less_Discount = obj.Item_Net_Amt
                    End If
                End If

                'If objShipment.LeakageAmount > 0 And clsCommon.CompairString(obj.Scheme_Item, "No") = CompairStringResult.Equal Then
                '    dblLeakageAmt = Math.Round((clsCommon.myCdbl(obj.Amt_Less_Discount) * objShipment.LeakageAmount) / (objShipment.Total_Amt), 2)
                '    dblTotLeagageamt += dblLeakageAmt
                '    obj.ItemLeakageAmount = dblLeakageAmt
                '    obj.Item_Net_Amt = obj.Item_Net_Amt - dblLeakageAmt
                '    obj.Amt_Less_Discount = obj.Item_Net_Amt
                'End If
                clsCommon.AddColumnsForChange(coll, "ItemLeakageAmount", obj.ItemLeakageAmount)
                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Code", obj.Cash_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Type", obj.Cash_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Pers", obj.Cash_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Amount", obj.Cash_Scheme_Amount)

                clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", obj.Scheme_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", obj.Scheme_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.Scheme_Qty)
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)

                clsCommon.AddColumnsForChange(coll, "VS_CashSchemeCode", obj.VS_CashSchemeCode)
                clsCommon.AddColumnsForChange(coll, "VS_Cash_Amt", obj.VS_Cash_Amt)
                clsCommon.AddColumnsForChange(coll, "VS_ltrInCrate", obj.VS_ltrInCrate)
                clsCommon.AddColumnsForChange(coll, "OrgUnit_code", obj.OrgUnit_code)
                clsCommon.AddColumnsForChange(coll, "DeliverQty", obj.DeliverQty)
                clsCommon.AddColumnsForChange(coll, "Delivery_Code", obj.Delivery_Code, True)
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Bar_Code", obj.Bar_Code, True)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Sub_Location_Code", obj.Sub_Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Free_qty", obj.Free_Qty)

                'clsCommon.AddColumnsForChange(coll, "Order_Code", obj.Order_Code, True)

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

                '   Dim strSql As String = "select top 1 TSPL_ITEM_PRICE_MASTER.Price_Amount1 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 , " & _
                ' "TSPL_ITEM_PRICE_MASTER.Price_Amount3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 , " & _
                ' "TSPL_ITEM_PRICE_MASTER.Price_Amount6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 , " & _
                ' "TSPL_ITEM_PRICE_MASTER.Price_Amount9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 from TSPL_ITEM_PRICE_MASTER " & _
                '"where  TSPL_ITEM_PRICE_MASTER.Price_Code='" & obj.Price_code & "' and  TSPL_ITEM_PRICE_MASTER.Item_Code='" & obj.Item_Code & "' and  " & _
                '"TSPL_ITEM_PRICE_MASTER.Item_Basic_Net=" & obj.MRP & " and TSPL_ITEM_PRICE_MASTER.UOM='" & obj.Unit_code & "' "

                Dim strSql As String = "Select RowNo, XXXE.Price_Amount1 ,XXXE.Price_Amount2 , " & _
                        "XXXE.Price_Amount3 ,XXXE.Price_Amount4 ,XXXE.Price_Amount5 , " & _
                        "XXXE.Price_Amount6 ,XXXE.Price_Amount7 ,XXXE.Price_Amount8 , " & _
                        "XXXE.Price_Amount9 ,XXXE.Price_Amount10 from ( " & _
                        "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
                        "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date, " & _
                        "Item_Basic_Price,Item_Basic_Net,Price_Code,TSPL_ITEM_PRICE_MASTER.Price_Amount1 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 , " & _
                        "TSPL_ITEM_PRICE_MASTER.Price_Amount3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 , " & _
                        "TSPL_ITEM_PRICE_MASTER.Price_Amount6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 , " & _
                        "TSPL_ITEM_PRICE_MASTER.Price_Amount9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
                        "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
                        "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code where  Start_Date<='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' and " & _
                        "(End_Date >= '" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "'  or End_Date is null) and " & _
                        "TSPL_ITEM_PRICE_MASTER.Price_Code='" & obj.Price_code & "' and UOM='" & obj.Unit_code & "' and " & _
                        "TSPL_ITEM_PRICE_MASTER.item_code='" & obj.Item_Code & "' AND Location_Code='" & strLocation & "' ) XXXE WHERE RowNo=1 "
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

                clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", IIf(obj.Scheme_Applicable = "Yes", "Y", "N"))
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", IIf(obj.Scheme_Item = "Yes", "Y", "N"))
                clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_MRP_Amt", obj.Total_MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Cust_Discount", obj.Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Total_Cust_Discount", obj.Total_Cust_Discount)
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
                clsCommon.AddColumnsForChange(coll, "PrincipleCode", obj.PrincipleCode)
                clsCommon.AddColumnsForChange(coll, "PrincipleDesc", obj.PrincipleDesc)
                clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendor_code)
                clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendor_desc)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPer", obj.HeadDiscPer)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPerAmt", obj.HeadDiscPerAmt)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_PKID", obj.Distributor_Commission_PKID, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_Rate", obj.Distributor_Commission_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_RateWithTax", obj.Distributor_Commission_RateWithTax, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_Amt", obj.Distributor_Commission_Amt, True)
                'clsCommon.AddColumnsForChange(coll, "Unit_Cogs", clsItemLocationDetails.GetUnitCogs(obj.Item_Code, obj.Location, trans))
                '' Anubhooti 12-Sep-2014 BM00000003847
                clsCommon.AddColumnsForChange(coll, "Crate", obj.Crate)
                'Sanjay BHA/21/06/18-000064
                clsCommon.AddColumnsForChange(coll, "CAN", obj.CanQty)
                clsCommon.AddColumnsForChange(coll, "ManualCan", obj.ManualCanQty)
                'Sanjay BHA/21/06/18-000064
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                clsSerializeInvenotry.SaveData("SD-IN", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.arrSrItem, trans)
                clsBatchInventory.SaveData("FS-SH", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)

            Next
            'If objShipment.LeakageAmount <> dblTotLeagageamt And objShipment.LeakageAmount > 0 Then
            '    Dim intLineNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select max(Line_No)  from TSPL_SD_SHIPMENT_DETAIL where scheme_item='N' ", trans))
            '    Dim dblDiffLeakage As Decimal = objShipment.LeakageAmount - dblTotLeagageamt
            '    clsDBFuncationality.ExecuteNonQuery("Update TSPL_SD_SHIPMENT_DETAIL set ItemLeakageAmount = ItemLeakageAmount + " & dblDiffLeakage & ",Item_Net_Amt=Item_Net_Amt + " & dblDiffLeakage & " where Line_No=" & intLineNo & " ", trans)
            'End If
            If objShipment.LeakageAmount > 0 Then
                Dim dblItemLeakageAmtForDetail As Double = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(ITEMLEAKAGEAMOUNT) from TSPL_SD_SHIPMENT_DETAIL where document_code='" & strDocNo & "'", trans)), 2)
                Dim dblDiffLeakage As Double = 0.0
                If dblItemLeakageAmtForDetail > objShipment.LeakageAmount Then
                    dblDiffLeakage = Math.Round(dblItemLeakageAmtForDetail - objShipment.LeakageAmount, 2)
                    If dblDiffLeakage <> 0 Then
                        Dim intLineNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select max(Line_No)  from TSPL_SD_SHIPMENT_DETAIL where scheme_item='N' and document_code='" & strDocNo & "' ", trans))
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_SD_SHIPMENT_DETAIL set ItemLeakageAmount = ItemLeakageAmount - " & dblDiffLeakage & ",Item_Net_Amt=Item_Net_Amt + " & dblDiffLeakage & " where Line_No=" & intLineNo & " ", trans)
                    End If
                ElseIf objShipment.LeakageAmount > dblItemLeakageAmtForDetail Then
                    dblDiffLeakage = Math.Round(objShipment.LeakageAmount - dblItemLeakageAmtForDetail, 2)
                    If dblDiffLeakage <> 0 Then
                        Dim intLineNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select max(Line_No)  from TSPL_SD_SHIPMENT_DETAIL where scheme_item='N' and document_code='" & strDocNo & "' ", trans))
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_SD_SHIPMENT_DETAIL set ItemLeakageAmount = ItemLeakageAmount + " & dblDiffLeakage & ",Item_Net_Amt=Item_Net_Amt - " & dblDiffLeakage & " where Line_No=" & intLineNo & " ", trans)
                    End If
                End If
            End If
        End If
        Return True
    End Function
    Public Shared Function UpdateDataBatchWiseDetail(ByVal strDocNo As String, ByVal Arr As List(Of clsDispatchNoteFreshSaleDetail), ByVal trans As SqlTransaction, ByVal DocDate As DateTime, ByVal strLocation As String, ByVal objShipment As clsDispatchNoteFreshSale, ByVal strTransType As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim dblTotLeagageamt As Decimal = 0
            For Each obj As clsDispatchNoteFreshSaleDetail In Arr
                Dim coll As New Hashtable()
                clsBatchInventory.SaveData(strTransType, strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)
            Next

        End If
        Return True
    End Function
    Public Shared Function GetBalanceSRNQty(ByVal strSRNCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,1 as RI from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code where TSPL_SD_SHIPMENT_DETAIL.Status=0 and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_DETAIL.Document_Code ='" + strSRNCode + "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" + strICode + "' and  TSPL_SD_SHIPMENT_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SD_SHIPMENT_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SD_SHIPMENT_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " & _
            " union all " & _
            " select TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.PI_Qty as Qty,-1 as RI from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL.SRN_Id='" + strSRNCode + "'   and TSPL_PI_DETAIL.Item_Code='" + strICode + "'  and  TSPL_PI_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PI_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PI_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_PI_DETAIL.PI_No not in ('" + strCurrPINNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_SD_SHIPMENT_DETAIL set Status=1 where Document_Code='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class
' Ticket No : TEC/03/10/18-000331 By Prabhakar for JE Open on Button Click
Public Class clsOpenJEAgainstInvoice
    Public Shared Function ShowInvoiceJE(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on Current Screen")
                Return False
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_No ='" + strDocNo + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on AR Invoice Screen")
                Return False
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ShowInvoiceJEForReturn(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on Current Screen")
                Return False
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_Return_No ='" + strDocNo + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on AR Invoice Screen")
                Return False
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ShowInvoiceJEForMiscSale(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on Current Screen")
                Return False
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where AgainstScrap ='" + strDocNo + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on AR Invoice Screen")
                Return False
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ShowInvoiceJEForMiscSaleReturn(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on Current Screen")
                Return False
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where AgainstScrapReturn ='" + strDocNo + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on AR Invoice Screen")
                Return False
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ShowInvoiceJEForMccReturn(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on Current Screen")
                Return False
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where Against_MCC_Material_Sale_Return ='" + strDocNo + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on AR Invoice Screen")
                Return False
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ShowJEForPurchaseReturn(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Return No Found on Current Screen")
                Return False
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_Vendor_Invoice_Head.Document_No from TSPL_Vendor_Invoice_Head where Against_PurchaseReturn_No ='" + strDocNo + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Return No Found on AP Screen")
                Return False
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ShowJEForPurchase(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on Current Screen")
                Return False
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_Vendor_Invoice_Head.Document_No from TSPL_Vendor_Invoice_Head where Against_POInvoice_No ='" + strDocNo + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on AP Screen")
                Return False
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function ShowJEAssetDisposal(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on Current Screen")
                Return False
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where Against_asset_Disposal ='" + strDocNo + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Invoice No Found on AR Invoice Screen")
                Return False
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsOpenInventory
    Public Shared Function ShowInventoryDatails(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No document found")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.RptInventoryMovement, strDocNo)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsOpenBankCashBook
    Public Shared Function ShowBankCashBookDatails(ByVal strDocNo As String) As Boolean
        Try
            Dim strCode As String = strDocNo
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No document found")
                Return False
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBankBook, strDocNo)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

