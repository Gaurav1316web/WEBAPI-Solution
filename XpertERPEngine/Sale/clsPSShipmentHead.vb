
Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls



Public Class clsPSShipmentHead
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
    Public Arr As List(Of clsPSShipmentHeadDetail) = Nothing
    Public ArrChkList As List(Of clsPSShipmentChecklistDetail) = Nothing

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
    Public Security_TotalAmt As Decimal = 0


    Public ArrDemand As List(Of clsPSShipmentDemand) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsPSShipmentHead, ByVal isNewEntry As Boolean, Optional ByVal IsDairyModule As Boolean = False) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans, IsDairyModule)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetInvoiceType(ByVal strCustCode As String, ByVal strLocation As String, ByVal strType As String, ByVal intItemTaxType As Integer, ByVal trans As SqlTransaction)
        Dim dt As DataTable
        Dim CreatVatSeriesOnExciseInvoice As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateVatSeriesForProductExciseinvoice, clsFixedParameterCode.CreateVatSeriesForProductExciseinvoice, trans))

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
        Dim obj As clsPSShipmentHead
        obj = New clsPSShipmentHead()
        If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
            obj.Invoice_Type = "T"
        Else
            obj.Invoice_Type = "R"
            If CreatVatSeriesOnExciseInvoice = 1 Then
                If clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
                    obj.Invoice_Type = "R"
                Else
                    obj.Invoice_Type = "I" 'Interstate series
                End If
            End If
        End If
        Dim strExcise As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + strLocation + "'", trans)) = "T", True, False)
        If strExcise = True AndAlso intItemTaxType = 2 Then
            obj.Invoice_Type = "E"
        End If

        Return obj.Invoice_Type
    End Function
    Public Shared Function AdvanceReceived(ByVal strDONo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dblPendingBookingAdvanceAmt As Double
            dblPendingBookingAdvanceAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (TSPL_BOOKING_MASTER_PRODUCTSALE.Total_Amt * TSPL_BOOKING_MASTER_PRODUCTSALE.Advance_Percentage)/100 -  isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE  " & _
            "left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Against_Sales_Order=TSPL_SD_SALES_ORDER_HEAD.Document_Code  " & _
            "left outer join TSPL_BOOKING_MASTER_PRODUCTSALE on TSPL_SD_SALES_ORDER_HEAD.Against_Booking_No=TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code " & _
            "left outer join TSPL_RECEIPT_HEADER on TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code=TSPL_RECEIPT_HEADER.Booking_Code  " & _
            "where TSPL_BOOKING_MASTER_PRODUCTSALE.Advance_Percentage > 0 and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code='" & strDONo & "' ", trans))
            If dblPendingBookingAdvanceAmt > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
        End Try
        Return True
    End Function
    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String, ByVal InvoiceNo As String, ByVal NavType As NavigatorType) As Boolean
        '' created by Richa Agarwal against ticket No-ERO/09/09/19-001022  on date 09-09-2019
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            '' table list 
            ''1. TSPL_SD_SALE_INVOICE_HEAD
            ''2. TSPL_SD_SALE_INVOICE_DETAIL
            ''3. TSPL_EX_SALE_INVOICE_NOTIFY_PARTY_DETAIL
            ''4.TSPL_CUSTOM_FIELD_VALUES
            ''4. TSPL_INVENTORY_MOVEMENT     ( no need of history)
            ''5. TSPL_Customer_Invoice_Head
            ''6. TSPL_Customer_Invoice_Detail
            ''7. TSPL_JOURNAL_DETAILS
            ''8. TSPL_JOURNAL_MASTER
            ''9. TSPL_BATCH_ITEM ( no need of history)
            '' steps for checking the items stock and batch wise stock

            Dim obj As clsPSShipmentHead = clsPSShipmentHead.GetData(Doc_No, NavType, trans, True)

            If obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If
            Dim strInvTransType As String = String.Empty
            'If clsCommon.CompairString(obj.Document_Type, "MT") = CompairStringResult.Equal Then
            '    strInvTransType = "MT_SALE_IN"
            'Else
            '    strInvTransType = "EX_SALE_IN"
            'End If

            ''richa agarwal 24 Dec,2020
            Dim dtirn As DataTable = clsDBFuncationality.GetDataTable("select Einvoice_type,IRN_No,Is_Taxable,Bill_To_Location from TSPL_SD_SALE_INVOICE_HEAD where document_code='" & InvoiceNo & "'", trans)
            If dtirn IsNot Nothing AndAlso dtirn.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtirn.Rows(0)("Einvoice_type")), "BB") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dtirn.Rows(0)("Is_Taxable")), "1") = CompairStringResult.Equal AndAlso clsERPFuncationality.GetEInvoiceStatus(obj.Document_Date, trans) = True Then
                    If ClsEInvoiceOFAPIs.EInvoice_Cancellation(InvoiceNo, clsCommon.myCstr(dtirn.Rows(0)("IRN_No")), clsCommon.myCstr(dtirn.Rows(0)("Bill_To_Location")), trans) = True Then
                    Else
                        Throw New Exception("Invalid JSON Value")
                    End If
                End If
            End If
            ''----------

            clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)
            '' transfer data into cancel table
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "tspl_sd_shipment_head", "Document_Code", "tspl_sd_shipment_DETAIL", "Document_Code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, InvoiceNo, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", trans)


            '' cancel customer invoice data
            qry = "select Document_No from TSPL_Customer_Invoice_Head  where against_Sale_no='" & InvoiceNo & "'"
            Dim Document_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Document_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Document_No, "TSPL_Customer_Invoice_Head", "Document_No", "TSPL_Customer_Invoice_Detail", "Document_No", trans)
            End If

            '' cancel journal master data invoice
            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where against_Sale_no='" & InvoiceNo & "')"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If

            '' cancel journal master data shipment
            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No='" & Doc_No & "'"
            Voucher_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If

            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, InvoiceNo, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)


            ''delete data from multiple tables

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where against_Sale_no='" & InvoiceNo & "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where against_Sale_no='" & InvoiceNo & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Customer_Invoice_Detail where Document_No in (Select Document_No from TSPL_Customer_Invoice_Head  where against_Sale_no='" & InvoiceNo & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Customer_Invoice_Head where against_Sale_no='" & InvoiceNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code in ('" & InvoiceNo & "','" & Doc_No & "') "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '----- shipment data


            'qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & Doc_No & "' and Trans_Type='FS-SH'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_BATCH_ITEM where  Document_Code='" & Doc_No & "' and Document_Type='FS-SH'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BATCH_ITEM where  Document_Code='" & Doc_No & "' and Document_Type in ('PS-SH', 'FS-SH')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & Doc_No & "' and Trans_Type in ('PS-SH', 'FS-SH')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & Doc_No & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No  ='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" & InvoiceNo & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & InvoiceNo & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_sd_shipment_detail where Document_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_sd_shipment_head where Document_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            '' release objects 
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsPSShipmentHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal IsDairyModule As Boolean = False) As Boolean
        Dim TransType_Str As String = ""
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmSaleDispatchDairy, obj.Bill_To_Location, obj.Document_Date, trans)
            clsSerializeInvenotry.DeleteData("SD-IN", obj.Document_Code, trans)
            If IsDairyModule = False Then
                clsBatchInventory.DeleteData("PS-SH", obj.Document_Code, trans)
            Else
                TransType_Str = obj.Trans_Type
                TransType_Str = TransType_Str & "-SH"
                clsBatchInventory.DeleteData(TransType_Str, obj.Document_Code, trans)
            End If

            If Not isNewEntry Then
                CheckInvoicePostedOnGovtProtal(obj.Document_Code, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_SD_SHIPMENT_HEAD", "Document_Code", "TSPL_SD_SHIPMENT_DETAIL", "Document_Code", trans)
            End If


            Dim qry As String = "delete from TSPL_SD_SHIPMENT_BOOKING_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            obj.GSTStatus = clsERPFuncationality.GetGSTStatus(obj.Document_Date)
            If isNewEntry Then
                Dim strItemCategory As String = String.Empty
                Dim StrCustomerState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(state,'') AS STATE from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(obj.Customer_Code) & "'", trans))
                Dim StrLocationState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(State,'') AS STATE from TSPL_LOCATION_MASTER WHERE LOCATION_CODE='" & clsCommon.myCstr(obj.Bill_To_Location) & "'", trans))
                If clsCommon.CompairString(StrCustomerState, StrLocationState) = CompairStringResult.Equal Then
                    strItemCategory = "L"
                Else
                    strItemCategory = "I"
                End If
                Dim stritemcode As String = String.Empty
                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    For Each obj11 As clsPSShipmentHeadDetail In obj.Arr
                        stritemcode = clsCommon.myCstr(obj11.Item_Code)
                    Next
                End If
                Dim strcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT count(Item_Code) FROM TSPL_LOCATION_WISE_ITEM_MASTER where Location_Code='" & clsCommon.myCstr(obj.Bill_To_Location) & "' and Item_Category='" & strItemCategory & "' and Item_Code='" & stritemcode & "'", trans))
                If strcount > 0 Then
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmShipmentProductSale, clsDocTransactionType.TaxExempted_ProductInvoice, obj.Bill_To_Location)
                    obj.Invoice_Type = "A"
                Else
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmShipmentProductSale, clsDocTransactionType.Other, obj.Bill_To_Location)
                End If
            End If
            '''' for Invoice Type
            If obj.GSTStatus = False Then
                Dim AllowChangeInvoiceType As Boolean = False
                AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters", trans)) = 0, False, True)
                If obj.Is_Create_Auto_Invoice AndAlso clsCommon.CompairString(obj.Invoice_Type, "A") <> CompairStringResult.Equal Then
                    If AllowChangeInvoiceType Then
                        If clsCommon.myLen(obj.Invoice_Type) <= 0 Then
                            Throw New Exception("Please select invoice  Type for creating auto invoice")
                            Return False
                        End If
                    Else
                        If clsCommon.myLen(obj.Ship_To_Location) > 0 Then
                            obj.Invoice_Type = clsPSShipmentHead.GetInvoiceType(obj.Customer_Code, obj.Bill_To_Location, "B", obj.Item_Tax_Type, trans)
                        Else
                            obj.Invoice_Type = clsPSShipmentHead.GetInvoiceType(obj.Customer_Code, obj.Bill_To_Location, "B", obj.Item_Tax_Type, trans)
                        End If
                    End If
                End If
            End If
            ''''' invoice type ends here

            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            ''richa 13/08/2014 against Ticket No BM00000003502
            'If IsDBNull("cust_po_date") Then

            '--------------------------------------------------------
            '-----------------richa 27/06/2014 Ticket No .BM00000002982--------------------------------
            Dim isIncrementCounter As Boolean = True
            If obj.Mannual_Invoice_No > 0 OrElse clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                isIncrementCounter = False
            End If
            Dim Doc_Code As String = Nothing


            If obj.Mannual_Invoice_No > 0 Then
                If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                ElseIf clsCommon.CompairString(obj.Invoice_Type, "R") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                ElseIf clsCommon.CompairString(obj.Invoice_Type, "E") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceExcise, obj.Bill_To_Location, False, isIncrementCounter)
                ElseIf clsCommon.CompairString(obj.Invoice_Type, "S") = CompairStringResult.Equal Then
                    Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceService, obj.Bill_To_Location, False, isIncrementCounter)
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
            clsCommon.AddColumnsForChange(coll, "Freight_Distance", obj.Freight_Distance)
            clsCommon.AddColumnsForChange(coll, "Against_Delivery_Code", obj.Against_Delivery_Code, True)
            clsCommon.AddColumnsForChange(coll, "isCardSale", obj.isCardSale)
            clsCommon.AddColumnsForChange(coll, "Transporter_Name_Manual", obj.Transporter_Name_Manual)
            clsCommon.AddColumnsForChange(coll, "Is_OwnVehicle", obj.Is_OwnVehicle)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "Gross_Item_Wt", obj.Gross_Item_Wt)
            clsCommon.AddColumnsForChange(coll, "Freight_Charges", obj.Freight_Charges)
            clsCommon.AddColumnsForChange(coll, "Total_Item_WeightMetric", obj.Total_Item_WeightMetric)
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
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)

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
            clsCommon.AddColumnsForChange(coll, "Against_Sales_Order", obj.Against_Sales_Order, True)
            clsCommon.AddColumnsForChange(coll, "Against_POS", obj.Against_POS, True)
            clsCommon.AddColumnsForChange(coll, "Delivery_Code_PS", obj.Delivery_Code_PS, True)
            clsCommon.AddColumnsForChange(coll, "GatePass_No", obj.GatePass_No, True)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Manual_No", obj.Vehicle_Manual_No, True)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code, True)
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
            If obj.Supply_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Supply_Date", clsCommon.GetPrintDate(obj.Supply_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Supply_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If
            If obj.Inv_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Inv_Date", clsCommon.GetPrintDate(obj.Inv_Date, "dd/MMM/yyyy"))
            End If
            ''richa ERO/12/06/19-000642
            clsCommon.AddColumnsForChange(coll, "Manual_Driver_Name", obj.Manual_Driver_Name)
            clsCommon.AddColumnsForChange(coll, "Manual_Salesman_Name", obj.Manual_Salesman_Name)

            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Invoice", IIf(obj.Is_Create_Auto_Invoice, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Sale_Invoice_No", obj.Sale_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Receipt", IIf(obj.Is_Create_Auto_Receipt, 1, 0))
            'clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Receipt", 1)
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
            clsCommon.AddColumnsForChange(coll, "Price_Group_Code", obj.Price_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_PO_No", obj.Cust_PO_No)
            clsCommon.AddColumnsForChange(coll, "Form_38_No", obj.Form_38_No)
            '-----------------richa 27/06/2014 Ticket No .BM00000002982--------------------------------
            clsCommon.AddColumnsForChange(coll, "Mannual_Invoice_No", obj.Mannual_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Mannual_Invoice_No_StringType", obj.InvoiceManualNowithPrefix)
            '

            clsCommon.AddColumnsForChange(coll, "SO_Validity", obj.SO_Validity)
            clsCommon.AddColumnsForChange(coll, "Commission_Apply", obj.Commission_Apply)
            clsCommon.AddColumnsForChange(coll, "Dispatch_date", clsCommon.GetPrintDate(obj.Dispatch_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Terms", obj.Dispatch_Terms)
            clsCommon.AddColumnsForChange(coll, "Payment_Terms", obj.Payment_Terms)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Period", obj.Dispatch_Period)
            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "WayBillNo", obj.WayBillNo)
            clsCommon.AddColumnsForChange(coll, "Total_Comm_Amt", obj.Total_Comm_Amt)
            clsCommon.AddColumnsForChange(coll, "Distributor_Commission_TotalAmt", obj.Distributor_Commission_TotalAmt)
            clsCommon.AddColumnsForChange(coll, "Security_TotalAmt", obj.Security_TotalAmt)
            If clsCommon.myLen(obj.Against_Sales_Order) = 0 Then
                obj.Direct_Dispatch = 1
            End If
            clsCommon.AddColumnsForChange(coll, "Direct_Dispatch", obj.Direct_Dispatch)
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
            If clsCommon.myLen(obj.Sale_Invoice_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Sale_Invoice_Date", clsCommon.GetPrintDate(obj.Sale_Invoice_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Sale_Invoice_Date", Nothing, True)
            End If
            If clsCommon.myLen(obj.Removal_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Removal_Date", clsCommon.GetPrintDate(obj.Removal_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Removal_Date", Nothing, True)
            End If
            If obj.Podate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Podate, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "cust_po_date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "IsSampling", obj.IsSampling)
            clsCommon.AddColumnsForChange(coll, "TotalCAN", obj.TotalCAN)
            clsCommon.AddColumnsForChange(coll, "ShippedCAN", obj.ShippedCAN)
            clsCommon.AddColumnsForChange(coll, "CrateQty", obj.CrateQty)
            clsCommon.AddColumnsForChange(coll, "OPKm", obj.OPKm)
            clsCommon.AddColumnsForChange(coll, "CLKm", obj.CLKm)
            clsCommon.AddColumnsForChange(coll, "IsSameBillShipParty", obj.IsSameBillShipParty)
            clsCommon.AddColumnsForChange(coll, "Scheme_Tax_Group", obj.Scheme_Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Itemwise", obj.Itemwise)
            clsCommon.AddColumnsForChange(coll, "Advance_Percentage", obj.Advance_Percentage)
            clsCommon.AddColumnsForChange(coll, "Crate", obj.Crate)
            clsCommon.AddColumnsForChange(coll, "jaali", obj.jaali)
            clsCommon.AddColumnsForChange(coll, "Box", obj.Box)
            clsCommon.AddColumnsForChange(coll, "VAT_InvoiceNo", obj.VAT_InvoiceNo)
            clsCommon.AddColumnsForChange(coll, "Including_Insurance", obj.Including_Insurance)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", obj.Is_Taxable)
            clsCommon.AddColumnsForChange(coll, "Print_Discount_Amt", obj.Print_Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Freight_Type", clsCommon.myCstr(obj.Freight_Type))
            clsCommon.AddColumnsForChange(coll, "FixedCharge", clsCommon.myCdbl(obj.FixedCharge))
            clsCommon.AddColumnsForChange(coll, "EmptyCharge", clsCommon.myCdbl(obj.EmptyCharge))
            clsCommon.AddColumnsForChange(coll, "Nine_NR_No", obj.Nine_NR_No)
            clsCommon.AddColumnsForChange(coll, "Cash_Customer", obj.CashCustomer)
            clsCommon.AddColumnsForChange(coll, "DO_Item_Type", obj.DO_Item_Type)
            clsCommon.AddColumnsForChange(coll, "Shift_type", obj.Shift_type, True)
            clsCommon.AddColumnsForChange(coll, "Is_CustomerChanged", obj.Is_CustomerChanged)

            clsCommon.AddColumnsForChange(coll, "Insurance", obj.Insurance)

            clsCommon.AddColumnsForChange(coll, "ManualVehicle", obj.ManualVehicle)
            clsCommon.AddColumnsForChange(coll, "IsReplacement", obj.IsReplacement)
            clsCommon.AddColumnsForChange(coll, "Invoice_No_ForReplacement", obj.Invoice_No_ForReplacement, True)
            clsCommon.AddColumnsForChange(coll, "Customer_Complaint_No", obj.Customer_Complaint_No, True)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Screen_Type", obj.Screen_Type)
                If IsDairyModule = False Then
                    clsCommon.AddColumnsForChange(coll, "Trans_Type", "PS")
                Else
                    clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SHIPMENT_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsPSShipmentHeadDetail.SaveData(obj.Document_Code, obj.Arr, trans, obj.Document_Date, IsDairyModule, obj.Trans_Type)
            clsPSShipmentDemand.SaveData(obj.Document_Code, obj.ArrDemand, trans)

            clsPSShipmentChecklistDetail.SaveData(obj.Document_Code, obj.ArrChkList, trans)
            clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            '''' to save item weight unit
            qry = "update TSPL_SD_shipment_DETAIL set Weight_UOM= (select Weight_UOM from TSPL_ITEM_MASTER where Item_Code=TSPL_SD_shipment_DETAIL.Item_Code)  where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_SD_SHIPMENT_HEAD", trans)
            ''''  for automatic invoice

            If IsDairyModule = False Then
                Dim objSI As clsPSInvoiceHead = ConvertShipmentToSaleInvoice(obj, trans, obj.Sale_Invoice_No, True, 0, IsDairyModule) 'sanjay
                'If Dairy Sale Off
                If clsCommon.myLen(obj.Sale_Invoice_No) > 0 Then
                    objSI.SaveData(objSI, False, trans, False, False)
                Else
                    objSI.Item_Tax_Type = obj.Item_Tax_Type
                    objSI.SaveData(objSI, True, trans, False, False)
                End If

            Else
                'If Dairy Sale ON
                If Not clsCommon.CompairString(obj.DO_Item_Type, "NT") = CompairStringResult.Equal Then
                    Dim objSI As clsPSInvoiceHead = ConvertShipmentToSaleInvoice(obj, trans, obj.Sale_Invoice_No, True, 1, IsDairyModule) 'sanjay
                    If objSI.Arr.Count > 0 Then
                        If clsCommon.myLen(obj.Sale_Invoice_No) > 0 Then
                            objSI.SaveData(objSI, False, trans, True, False)
                        Else
                            objSI.Item_Tax_Type = obj.Item_Tax_Type
                            objSI.SaveData(objSI, True, trans, True)
                        End If
                    End If
                Else
                    Dim saleInvoiceNo As String = ""
                    Dim CreateSeperateTaxInvForFOCIteminNonTaxdispatch = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateSeperateTaxInvForFOCIteminNonTaxdispatch, clsFixedParameterCode.CreateSeperateTaxInvForFOCIteminNonTaxdispatch, trans))
                    If CreateSeperateTaxInvForFOCIteminNonTaxdispatch = 0 Then
                        Dim objSI As clsPSInvoiceHead = ConvertShipmentToSaleInvoice(obj, trans, obj.Sale_Invoice_No, True, obj.Is_Taxable, IsDairyModule) 'sanjay
                        If objSI.Arr.Count > 0 Then
                            If clsCommon.myLen(obj.Sale_Invoice_No) > 0 Then
                                objSI.SaveData(objSI, False, trans, True, False)
                            Else
                                objSI.Item_Tax_Type = obj.Item_Tax_Type
                                objSI.SaveData(objSI, True, trans, True)
                            End If
                        End If

                    Else
                        saleInvoiceNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_head where Against_Shipment_No ='" & obj.Document_Code & "' and Total_Tax_Amt=0", trans))
                        Dim objSI As clsPSInvoiceHead = ConvertShipmentToSaleInvoice(obj, trans, saleInvoiceNo, False, 0, IsDairyModule) 'sanjay
                        If objSI.Arr.Count > 0 Then
                            obj.Sale_Invoice_No = saleInvoiceNo
                            If clsCommon.myLen(obj.Sale_Invoice_No) > 0 Then
                                objSI.SaveData(objSI, False, trans, True, False)
                            Else
                                objSI.Item_Tax_Type = obj.Item_Tax_Type
                                objSI.SaveData(objSI, True, trans, True, False)
                            End If
                        End If


                        saleInvoiceNo = ""
                        saleInvoiceNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_head where Against_Shipment_No ='" & obj.Document_Code & "' and Total_Tax_Amt > 0", trans))
                        Dim objSI1 As clsPSInvoiceHead = ConvertShipmentToSaleInvoice(obj, trans, saleInvoiceNo, False, 1, IsDairyModule) 'sanjay
                        If objSI1.Arr.Count > 0 Then
                            obj.Sale_Invoice_No = saleInvoiceNo
                            If clsCommon.myLen(obj.Sale_Invoice_No) > 0 Then
                                objSI1.SaveData(objSI1, False, trans, True, True)
                            Else
                                objSI1.Item_Tax_Type = obj.Item_Tax_Type
                                objSI1.SaveData(objSI1, True, trans, True, True)
                            End If
                        End If
                    End If

                End If


            End If
            If obj.Is_CustomerChanged = 1 Then
                UpdateInventoryMovement(obj, trans, True)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateAfterPosting(ByVal obj As clsPSShipmentHead, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.Document_Code) > 0 Then
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "OPKm", obj.OPKm)
                clsCommon.AddColumnsForChange(coll, "CLKm", obj.CLKm)
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SHIPMENT_HEAD.Document_Code='" + obj.Document_Code + "'", trans)

                Dim coll2 As New Hashtable()
                Dim objInvoice As New clsPSInvoiceHead
                objInvoice.Document_Code = clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + obj.Document_Code + "'", trans)
                'objShipment.EWayBillNo = obj.EWayBillNo
                'objShipment.EWayBillDate = obj.EWayBillDate
                clsCommon.AddColumnsForChange(coll2, "GRNo", obj.GRNo)
                If clsCommon.myLen(obj.GR_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll2, "GR_Date", clsCommon.GetPrintDate(obj.GR_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll2, "GR_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll2, "OPKm", obj.OPKm)
                clsCommon.AddColumnsForChange(coll2, "CLKm", obj.CLKm)
                clsCommon.AddColumnsForChange(coll2, "EWayBillNo", obj.EWayBillNo)
                clsCommon.AddColumnsForChange(coll2, "Electronic_Ref_No", obj.Electronic_Ref_No)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll2, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll2, "EWayBillDate", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + objInvoice.Document_Code + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'sanjay
    Public Shared Function UpdateEWayBillDeatil(ByVal obj As clsPSShipmentHead, ByVal TransferNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.Document_Code) > 0 Then
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SHIPMENT_HEAD.document_code='" + obj.Document_Code + "'", trans)


                Dim coll2 As New Hashtable()
                Dim objInvoice As New clsPSInvoiceHead
                objInvoice.Document_Code = clsDBFuncationality.getSingleValue("select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + obj.Document_Code + "'", trans)
                clsCommon.AddColumnsForChange(coll2, "EWayBillNo", obj.EWayBillNo)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll2, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll2, "EWayBillDate", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + objInvoice.Document_Code + "'", trans)

            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'sanjay

    Public Shared Function checkSaveNotification(ByVal obj As clsPSShipmentHead, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Count As Integer = 0
            Dim CreditLimit As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Credit_Limit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_Code + "'", trans))
            Dim qry As String
            Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNShipment, trans)
            For Each dr As DataRow In dt.Rows
                'Criteria, Notification, Validation
                If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Then
                    qry = "Select COUNT(*) from TSPL_SD_SHIPMENT_HEAD" & _
        " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code" & _
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
        " WHERE TSPL_SD_SHIPMENT_HEAD.Status = 1" & _
        " AND TSPL_SD_SHIPMENT_HEAD.Customer_Code='" + obj.Customer_Code + "'" & _
        " AND TSPL_SD_SHIPMENT_HEAD.Due_Date<'" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "'" & _
        " AND ISNULL(TSPL_Customer_Invoice_Head.Balance_Amt,0)<>0"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                        If clsCommon.CompairString(dr("Validation"), "Required Approval") = CompairStringResult.Equal Then
                            'clsCommon.MyMessageBoxShow(clsCommon.myCstr(dt.Rows(0)("Notification")))
                            If common.clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")) + Environment.NewLine + "Do you want to continue?.", "Load Out", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                                Dim frm As New FrmPWD(trans)
                                frm.strCode = clsFixedParameterCode.CreditLimitApproval
                                frm.strType = clsFixedParameterType.CreditLimitApproval
                                frm.ShowDialog()
                                If frm.isPasswordCorrect Then
                                    Count += 1
                                End If
                            Else
                                Return False
                            End If
                        Else
                            Throw New Exception(clsCommon.myCstr(dt.Rows(0)("Notification")))
                        End If
                    End If
                ElseIf clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Then
                    qry = "Select SUM(TSPL_Customer_Invoice_Head.Balance_Amt) from TSPL_SD_SHIPMENT_HEAD" & _
        " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code" & _
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
        " WHERE TSPL_SD_SHIPMENT_HEAD.Status = 1" & _
        " AND TSPL_SD_SHIPMENT_HEAD.Customer_Code='" + obj.Customer_Code + "'" & _
        " AND TSPL_SD_SHIPMENT_HEAD.Document_Date<'" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "'" & _
        " AND ISNULL(TSPL_Customer_Invoice_Head.Balance_Amt,0)<>0"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > CreditLimit Then
                        If clsCommon.CompairString(dr("Validation"), "Required Approval") = CompairStringResult.Equal Then
                            'clsCommon.MyMessageBoxShow(clsCommon.myCstr(dt.Rows(0)("Notification")))
                            If common.clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")) + Environment.NewLine + "Do you want to continue?.", "Load Out", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                                Dim frm As New FrmPWD(trans)
                                frm.strCode = clsFixedParameterCode.CreditLimitApproval
                                frm.strType = clsFixedParameterType.CreditLimitApproval
                                frm.ShowDialog()
                                If frm.isPasswordCorrect Then
                                    Count += 1
                                End If
                            Else
                                Return False
                            End If
                        Else
                            Throw New Exception(clsCommon.myCstr(dt.Rows(0)("Notification")))
                        End If
                    End If
                End If
            Next
            If Count >= 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsPSShipmentHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, Optional ByVal IsDairyModule As Boolean = False) As clsPSShipmentHead
        'Dim TransType_Str As String = ""
        Dim obj As clsPSShipmentHead = Nothing
        Dim qry As String = "SELECT TSPL_SD_SHIPMENT_HEAD.isCardSale,TSPL_SD_SHIPMENT_HEAD.Sub_Location_code,TSPL_SD_SHIPMENT_HEAD.ChangedTCSBaseAmount,TSPL_SD_SHIPMENT_HEAD.ActualTCSBaseAmount, TSPL_SD_SHIPMENT_HEAD.Customer_Complaint_No,TSPL_SD_SHIPMENT_HEAD.Invoice_No_ForReplacement,TSPL_SD_SHIPMENT_HEAD.IsReplacement,TSPL_SD_SHIPMENT_HEAD.Manual_Driver_Name,TSPL_SD_SHIPMENT_HEAD.Manual_Salesman_Name , TSPL_SD_SHIPMENT_HEAD.IsSampling,TSPL_SD_SHIPMENT_HEAD.TotalCAN,TSPL_SD_SHIPMENT_HEAD.ShippedCAN, TSPL_SD_SHIPMENT_HEAD.CrateQty,TSPL_SD_SHIPMENT_HEAD.OPKm,TSPL_SD_SHIPMENT_HEAD.CLKm,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.Is_CustomerChanged, TSPL_SD_SHIPMENT_HEAD.IsSameBillShipParty, TSPL_SD_SHIPMENT_HEAD.Scheme_Tax_Group, TSPL_SD_SHIPMENT_HEAD.Electronic_Ref_No,TSPL_SD_SHIPMENT_HEAD.EWayBillNo,TSPL_SD_SHIPMENT_HEAD.EWayBillDate, TSPL_SD_SHIPMENT_HEAD.Is_Taxable,TSPL_SD_SHIPMENT_HEAD.DO_Item_Type,TSPL_SD_SHIPMENT_HEAD.Shift_Type,TSPL_SD_SHIPMENT_HEAD.Vehicle_Manual_No,isnull(TSPL_SD_SHIPMENT_HEAD.Nine_NR_No,'') as 'Nine_NR_No', TSPL_SD_SHIPMENT_HEAD.Freight_Type,TSPL_SD_SHIPMENT_HEAD.EmptyCharge,TSPL_SD_SHIPMENT_HEAD.FixedCharge,TSPL_SD_SHIPMENT_HEAD.Including_Insurance,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt',TSPL_SD_SHIPMENT_HEAD.VAT_InvoiceNo,TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code,TSPL_SD_SHIPMENT_HEAD.Crate,TSPL_SD_SHIPMENT_HEAD.Jaali,TSPL_SD_SHIPMENT_HEAD.Box ,TSPL_SD_SHIPMENT_HEAD.GatePass_No, ISNULL(TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual,'') AS Transporter_Name_Manual ,TSPL_SD_SHIPMENT_HEAD.Is_OwnVehicle,TSPL_SD_SHIPMENT_HEAD.Gross_Item_Wt,TSPL_SD_SHIPMENT_HEAD.RoundOffAmount,TSPL_SD_SHIPMENT_HEAD.Advance_Approval_Reqd,TSPL_SD_SHIPMENT_HEAD.Is_Advance_Approved,TSPL_SD_SHIPMENT_HEAD.Freight_Charges,TSPL_SD_SHIPMENT_HEAD.Total_Item_Weight,TSPL_SD_SHIPMENT_HEAD.Total_Item_WeightMetric,TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.Transporter_Name,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_SD_SHIPMENT_HEAD.Is_Delivered,TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt,TSPL_SD_SHIPMENT_HEAD.cust_po_date,TSPL_SD_SHIPMENT_HEAD.Cust_PO_No,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code,TSPL_SD_SHIPMENT_HEAD.price_group_code,TSPL_SD_SHIPMENT_HEAD.Invoice_Type,TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per,TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt,TSPL_SD_SHIPMENT_HEAD.TotCashDiscAmt,TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_SD_SHIPMENT_HEAD.Route_Desc,TSPL_SD_SHIPMENT_HEAD.Price_Code,TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SHIPMENT_HEAD.Status,TSPL_SD_SHIPMENT_HEAD.On_Hold,TSPL_SD_SHIPMENT_HEAD.Ref_No,TSPL_SD_SHIPMENT_HEAD.Description,TSPL_SD_SHIPMENT_HEAD.Remarks,TSPL_SD_SHIPMENT_HEAD.Tax_Group,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,isnull(TSPL_SD_SHIPMENT_HEAD.Ship_To_Location,'') as Ship_To_Location,isnull(TSPL_SD_SHIPMENT_HEAD.Ship_To_Party,'') as Ship_To_Party,isnull(TSPL_SD_SHIPMENT_HEAD.Ship_To_Party_Parent,'') as Ship_To_Party_Parent,TSPL_SD_SHIPMENT_HEAD.TAX1,TSPL_SD_SHIPMENT_HEAD.TAX1_Rate,TSPL_SD_SHIPMENT_HEAD.TAX1_Amt,TSPL_SD_SHIPMENT_HEAD.TAX1_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX2,TSPL_SD_SHIPMENT_HEAD.TAX2_Rate,TSPL_SD_SHIPMENT_HEAD.TAX2_Amt,TSPL_SD_SHIPMENT_HEAD.TAX2_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX3,TSPL_SD_SHIPMENT_HEAD.TAX3_Rate,TSPL_SD_SHIPMENT_HEAD.TAX3_Amt,TSPL_SD_SHIPMENT_HEAD.TAX3_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX4,TSPL_SD_SHIPMENT_HEAD.TAX4_Rate,TSPL_SD_SHIPMENT_HEAD.TAX4_Amt,TSPL_SD_SHIPMENT_HEAD.TAX4_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX5,TSPL_SD_SHIPMENT_HEAD.TAX5_Rate,TSPL_SD_SHIPMENT_HEAD.TAX5_Amt,TSPL_SD_SHIPMENT_HEAD.TAX5_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX6,TSPL_SD_SHIPMENT_HEAD.TAX6_Rate,TSPL_SD_SHIPMENT_HEAD.TAX6_Amt,TSPL_SD_SHIPMENT_HEAD.TAX6_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX7,TSPL_SD_SHIPMENT_HEAD.TAX7_Rate,TSPL_SD_SHIPMENT_HEAD.TAX7_Amt,TSPL_SD_SHIPMENT_HEAD.TAX7_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX8,TSPL_SD_SHIPMENT_HEAD.TAX8_Rate,TSPL_SD_SHIPMENT_HEAD.TAX8_Amt,TSPL_SD_SHIPMENT_HEAD.TAX8_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX9,TSPL_SD_SHIPMENT_HEAD.TAX9_Rate,TSPL_SD_SHIPMENT_HEAD.TAX9_Amt,TSPL_SD_SHIPMENT_HEAD.TAX9_Base_Amt,TSPL_SD_SHIPMENT_HEAD.TAX10,TSPL_SD_SHIPMENT_HEAD.TAX10_Rate,TSPL_SD_SHIPMENT_HEAD.TAX10_Amt,TSPL_SD_SHIPMENT_HEAD.TAX10_Base_Amt,TSPL_SD_SHIPMENT_HEAD.Discount_Base,TSPL_SD_SHIPMENT_HEAD.Discount_Amt,TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount,TSPL_SD_SHIPMENT_HEAD.Total_Tax_Amt,TSPL_SD_SHIPMENT_HEAD.Comments,TSPL_SD_SHIPMENT_HEAD.Comp_Code,TSPL_SD_SHIPMENT_HEAD.Terms_Code,TSPL_SD_SHIPMENT_HEAD.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_SD_SHIPMENT_HEAD.Posting_Date,TSPL_SD_SHIPMENT_HEAD.Total_Amt,TSPL_SD_SHIPMENT_HEAD.Carrier,TSPL_SD_SHIPMENT_HEAD.VehicleNo,TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,TSPL_SD_SHIPMENT_HEAD.GRNo,TSPL_SD_SHIPMENT_HEAD.GENo,TSPL_SD_SHIPMENT_HEAD.GEDate, TSPL_SD_SHIPMENT_HEAD.Dept,TSPL_SD_SHIPMENT_HEAD.Dept_Desc,TSPL_SD_SHIPMENT_HEAD.Item_Type,TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ,TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt1,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt2,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt3,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt4,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt5,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt6,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt7,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt8,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code9 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name9,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt9 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Code10 ,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Name10,TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt10,TSPL_SD_SHIPMENT_HEAD.Total_Add_Charge,TSPL_SD_SHIPMENT_HEAD.Tax_Calculation_Type,TSPL_SD_SHIPMENT_HEAD.Challan_No, TSPL_SD_SHIPMENT_HEAD.Challan_Date, TSPL_SD_SHIPMENT_HEAD.Inv_Date,TSPL_SD_SHIPMENT_HEAD.Inv_No,TSPL_SD_SHIPMENT_HEAD.Is_Internal,TSPL_SD_SHIPMENT_HEAD.Is_Create_Auto_Invoice,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Is_Create_Auto_Receipt,TSPL_SD_SHIPMENT_HEAD.Salesman_Code ,TSPL_SD_SHIPMENT_HEAD.Salesman_Name,  "
        qry += " TSPL_SD_SHIPMENT_HEAD.CURRENCY_CODE,TSPL_SD_SHIPMENT_HEAD.CONVRATE,TSPL_SD_SHIPMENT_HEAD.APPLICABLEFROM,TSPL_SD_SHIPMENT_HEAD.PRoject_ID ,TSPL_SD_SHIPMENT_HEAD.Mannual_Invoice_No,TSPL_SD_SHIPMENT_HEAD. Mannual_Invoice_No_StringType,TSPL_SD_SHIPMENT_HEAD.Form_38_No " &
        " ,TSPL_SD_SHIPMENT_HEAD.SO_Validity,TSPL_SD_SHIPMENT_HEAD.Commission_Apply,TSPL_SD_SHIPMENT_HEAD.Total_Comm_Amt,TSPL_SD_SHIPMENT_HEAD.Dispatch_date,TSPL_SD_SHIPMENT_HEAD.WayBillNo,TSPL_SD_SHIPMENT_HEAD.WayBillDate " &
        " ,TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms,TSPL_SD_SHIPMENT_HEAD.Payment_Terms,TSPL_SD_SHIPMENT_HEAD.Dispatch_Period,TSPL_SD_SHIPMENT_HEAD.Vehicle_Capacity " &
        ",TSPL_SD_SHIPMENT_HEAD.Itemwise,TSPL_SD_SHIPMENT_HEAD.Supply_Date,TSPL_SD_SHIPMENT_HEAD.Delivery_Code_PS,TSPL_SD_SHIPMENT_HEAD.Advance_Percentage,TSPL_SD_SHIPMENT_HEAD.GR_Date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date,TSPL_SD_SHIPMENT_HEAD.Cash_Customer,TSPL_SD_SHIPMENT_HEAD.Insurance,TSPL_SD_SHIPMENT_HEAD.ManualVehicle,TSPL_SD_SHIPMENT_HEAD.Freight_Distance,TSPL_SD_SHIPMENT_HEAD.Distributor_Commission_TotalAmt,TSPL_SD_SHIPMENT_HEAD.Security_TotalAmt "

        qry += "  FROM TSPL_SD_SHIPMENT_HEAD "
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location "
        qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Location "
        qry += " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_SD_SHIPMENT_HEAD.Tax_Group "
        qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_SD_SHIPMENT_HEAD.Terms_Code "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code where 2=2"
        Dim whrCls As String = ""
        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = FrmMainTranScreen.CustomerPermission()
        End If
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        'End If

        If IsDairyModule = False Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
                whrCls = "  and TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS' AND TSPL_SD_SHIPMENT_HEAD.Screen_Type='' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ") "
            ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = "  and TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS' AND TSPL_SD_SHIPMENT_HEAD.Screen_Type='' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            ElseIf clsCommon.myLen(strwherecls) > 0 Then
                whrCls = "  and TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS' AND TSPL_SD_SHIPMENT_HEAD.Screen_Type='' AND TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ")"
            Else
                whrCls = "  AND TSPL_SD_SHIPMENT_HEAD.Screen_Type='' "
            End If
        Else
            'TransType_Str = clsDBFuncationality.getSingleValue("SELECT isnull(Trans_Type,'') as Trans_Type FROM TSPL_SD_SHIPMENT_HEAD where Document_Code = '" + strPONo + "'", trans)
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
                whrCls = "  and TSPL_SD_SHIPMENT_HEAD.Trans_Type IN ('FS','PS') AND TSPL_SD_SHIPMENT_HEAD.Screen_Type='DS' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ") "
            ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = "  and TSPL_SD_SHIPMENT_HEAD.Trans_Type IN ('FS','PS') AND TSPL_SD_SHIPMENT_HEAD.Screen_Type='DS' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            ElseIf clsCommon.myLen(strwherecls) > 0 Then
                whrCls = "  and TSPL_SD_SHIPMENT_HEAD.Trans_Type IN ('FS','PS') AND TSPL_SD_SHIPMENT_HEAD.Screen_Type='DS' AND TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ")"
            Else
                whrCls = " and TSPL_SD_SHIPMENT_HEAD.Trans_Type IN ('FS','PS')  AND TSPL_SD_SHIPMENT_HEAD.Screen_Type='DS'"
            End If
        End If
        '-----------------------------------------------------

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SD_SHIPMENT_HEAD WHERE 1=1 " + whrCls + ") "
            Case NavigatorType.Last
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SHIPMENT_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = '" + strPONo + "' "
            Case NavigatorType.Next
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SD_SHIPMENT_HEAD where Document_Code>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SHIPMENT_HEAD where Document_Code<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPSShipmentHead()
            obj.IsSampling = clsCommon.myCdbl(dt.Rows(0)("IsSampling"))
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            obj.TotalCAN = clsCommon.myCdbl(dt.Rows(0)("TotalCAN"))
            obj.ShippedCAN = clsCommon.myCdbl(dt.Rows(0)("ShippedCAN"))
            obj.CrateQty = clsCommon.myCdbl(dt.Rows(0)("CrateQty"))
            obj.isCardSale = clsCommon.myCdbl(dt.Rows(0)("isCardSale"))
            obj.OPKm = clsCommon.myCdbl(dt.Rows(0)("OPKm"))
            obj.CLKm = clsCommon.myCdbl(dt.Rows(0)("CLKm"))
            obj.Is_CustomerChanged = clsCommon.myCdbl(dt.Rows(0)("Is_CustomerChanged"))
            obj.IsSameBillShipParty = clsCommon.myCdbl(dt.Rows(0)("IsSameBillShipParty"))
            obj.Scheme_Tax_Group = clsCommon.myCstr(dt.Rows(0)("Scheme_Tax_Group"))
            obj.Electronic_Ref_No = clsCommon.myCstr(dt.Rows(0)("Electronic_Ref_No"))
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
            obj.DO_Item_Type = clsCommon.myCstr(dt.Rows(0)("DO_Item_Type"))
            obj.Shift_type = clsCommon.myCstr(dt.Rows(0)("Shift_Type"))
            obj.FixedCharge = clsCommon.myCdbl(dt.Rows(0)("FixedCharge"))
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))

            obj.EmptyCharge = clsCommon.myCdbl(dt.Rows(0)("EmptyCharge"))
            obj.Freight_Type = clsCommon.myCstr(dt.Rows(0)("Freight_Type"))
            obj.Including_Insurance = clsCommon.myCdbl(dt.Rows(0)("Including_Insurance"))
            obj.VAT_InvoiceNo = clsCommon.myCstr(dt.Rows(0)("VAT_InvoiceNo"))
            obj.Crate = clsCommon.myCdbl(dt.Rows(0)("Crate"))
            obj.jaali = clsCommon.myCdbl(dt.Rows(0)("jaali"))
            obj.Box = clsCommon.myCdbl(dt.Rows(0)("Box"))
            obj.Against_Delivery_Code = clsCommon.myCstr(dt.Rows(0)("Against_Delivery_Code"))
            obj.GatePass_No = clsCommon.myCstr(dt.Rows(0)("GatePass_No"))
            obj.Vehicle_Manual_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_Manual_No"))
            obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_code"))
            obj.Transporter_Name_Manual = clsCommon.myCstr(dt.Rows(0)("Transporter_Name_Manual"))
            obj.Is_OwnVehicle = clsCommon.myCdbl(dt.Rows(0)("Is_OwnVehicle"))
            obj.Gross_Item_Wt = clsCommon.myCdbl(dt.Rows(0)("Gross_Item_Wt"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.Advance_Approval_Reqd = clsCommon.myCdbl(dt.Rows(0)("Advance_Approval_Reqd"))
            obj.Is_Advance_Approved = clsCommon.myCdbl(dt.Rows(0)("Is_Advance_Approved"))
            obj.Freight_Charges = clsCommon.myCdbl(dt.Rows(0)("Freight_Charges"))
            obj.Total_Item_Weight = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Weight"))
            obj.Total_Item_WeightMetric = clsCommon.myCdbl(dt.Rows(0)("Total_Item_WeightMetric"))
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
            If dt.Rows(0)("Sale_Invoice_Date") IsNot DBNull.Value Then
                obj.Sale_Invoice_Date = clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_Date"))
            End If
            If dt.Rows(0)("Removal_Date") IsNot DBNull.Value Then
                obj.Removal_Date = clsCommon.myCstr(dt.Rows(0)("Removal_Date"))
            End If
            If dt.Rows(0)("Supply_Date") IsNot DBNull.Value Then
                obj.Supply_Date = clsCommon.myCstr(dt.Rows(0)("Supply_Date"))
            End If

            obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Freight_Distance = clsCommon.myCdbl(dt.Rows(0)("Freight_Distance"))
            If dt.Rows(0)("WayBillDate") IsNot DBNull.Value Then
                obj.WayBillDate = clsCommon.myCDate(dt.Rows(0)("WayBillDate"))
            End If
            obj.WayBillNo = clsCommon.myCstr(dt.Rows(0)("WayBillNo"))
            obj.SO_Validity = clsCommon.myCdbl(dt.Rows(0)("SO_Validity"))
            obj.Commission_Apply = clsCommon.myCdbl(dt.Rows(0)("Commission_Apply"))
            obj.Total_Comm_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Comm_Amt"))

            If dt.Rows(0)("Dispatch_date") IsNot DBNull.Value Then
                obj.Dispatch_date = clsCommon.myCDate(dt.Rows(0)("Dispatch_date"))
            End If

            obj.Vehicle_Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Dispatch_Terms = clsCommon.myCstr(dt.Rows(0)("Dispatch_Terms"))
            obj.Payment_Terms = clsCommon.myCstr(dt.Rows(0)("Payment_Terms"))
            obj.Dispatch_Period = clsCommon.myCdbl(dt.Rows(0)("Dispatch_Period"))
            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))

            obj.Is_Delivered = clsCommon.myCdbl(dt.Rows(0)("Is_Delivered"))
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

            obj.CashCustomer = clsCommon.myCstr(dt.Rows(0)("Cash_Customer"))

            If dt.Rows(0)("Due_Date") IsNot DBNull.Value Then
                obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            End If

            '-----------------richa 26/06/2014 Ticket No .BM00000002982----------------------------
            obj.InvoiceManualNowithPrefix = clsCommon.myCstr(dt.Rows(0)("Mannual_Invoice_No_StringType"))
            obj.Mannual_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Mannual_Invoice_No"))
            ''richa ERO/12/06/19-000642
            obj.Manual_Driver_Name = clsCommon.myCstr(dt.Rows(0)("Manual_Driver_Name"))
            obj.Manual_Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Manual_Salesman_Name"))

            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))

            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If

            obj.IsReplacement = clsCommon.myCdbl(dt.Rows(0)("IsReplacement"))
            obj.Invoice_No_ForReplacement = clsCommon.myCstr(dt.Rows(0)("Invoice_No_ForReplacement"))
            obj.Customer_Complaint_No = clsCommon.myCstr(dt.Rows(0)("Customer_Complaint_No"))

            obj.Challan_No = clsCommon.myCdbl(dt.Rows(0)("Challan_No"))
            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("Carrier"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.AlternateVehicle = clsCommon.myCstr(dt.Rows(0)("AlternateVehicle"))
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))
            If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
            End If




            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))

            obj.Against_Sales_Order = clsCommon.myCstr(dt.Rows(0)("Against_Sales_Order"))
            obj.Delivery_Code_PS = clsCommon.myCstr(dt.Rows(0)("Delivery_Code_PS"))


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
            obj.Insurance = clsCommon.myCstr(dt.Rows(0)("Insurance"))
            obj.ManualVehicle = clsCommon.myCstr(dt.Rows(0)("ManualVehicle"))
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
            obj.Distributor_Commission_TotalAmt = clsCommon.myCdbl(dt.Rows(0)("Distributor_Commission_TotalAmt"))
            obj.Security_TotalAmt = clsCommon.myCdbl(dt.Rows(0)("Security_TotalAmt"))
            obj.Invoice_No = clsDBFuncationality.getSingleValue("Select Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" & obj.Document_Code & "' and isnull(TSPL_SD_SALE_INVOICE_HEAD .Invoice_No_For_Supplementary ,'')='' ", trans)

            qry = "SELECT  TSPL_SD_SHIPMENT_DETAIL.Sub_Location_code,TSPL_SD_SHIPMENT_DETAIL.VS_CashSchemeCode,TSPL_SD_SHIPMENT_DETAIL.VS_Cash_Amt,TSPL_SD_SHIPMENT_DETAIL.VS_ltrInCrate,TSPL_SD_SHIPMENT_DETAIL.CAN,TSPL_SD_SHIPMENT_DETAIL.Structure_Code,TSPL_SD_SHIPMENT_DETAIL.ItemwiseTaxCode,TSPL_SD_SHIPMENT_DETAIL.CRATE, TSPL_SD_SHIPMENT_DETAIL.Disc_Scheme_Amount,TSPL_SD_SHIPMENT_DETAIL.Disc_Scheme_Code,TSPL_SD_SHIPMENT_DETAIL.Disc_Scheme_Pers,TSPL_SD_SHIPMENT_DETAIL.Disc_Scheme_Type,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,TSPL_SD_SHIPMENT_DETAIL.GatePass_No,TSPL_SD_SHIPMENT_DETAIL.Alter_UnitQty,TSPL_SD_SHIPMENT_DETAIL.Rate_UnitQty,TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Amount,TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Type,TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Pers,TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Scheme_Item_UOM,TSPL_SD_SHIPMENT_DETAIL.Scheme_Qty,TSPL_SD_SHIPMENT_DETAIL.Scheme_Item_Code,TSPL_SD_SHIPMENT_DETAIL.Scheme_Type,TSPL_SD_SHIPMENT_DETAIL.Total_Item_WeightMetric,TSPL_SD_SHIPMENT_DETAIL.OrgUnit_code,TSPL_SD_SHIPMENT_DETAIL.Is_Mannual_Amt,TSPL_SD_SHIPMENT_DETAIL.Document_Code,TSPL_SD_SHIPMENT_DETAIL.Line_No, " &
            "TSPL_SD_SHIPMENT_DETAIL.Status,TSPL_SD_SHIPMENT_DETAIL.Row_Type,TSPL_SD_SHIPMENT_DETAIL.status,TSPL_SD_SHIPMENT_DETAIL.Item_Code, " &
            "TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SHIPMENT_DETAIL.Qty,TSPL_SD_SHIPMENT_DETAIL.Free_Qty,TSPL_SD_SHIPMENT_DETAIL.Order_Code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Order_Code,TSPL_SD_SHIPMENT_DETAIL.Balance_Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_DETAIL.Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX1,TSPL_SD_SHIPMENT_DETAIL.TAX1_Rate, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX2,TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX3,TSPL_SD_SHIPMENT_DETAIL.TAX3_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX4 , " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX4_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX5,TSPL_SD_SHIPMENT_DETAIL.TAX5_Rate , " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX6,TSPL_SD_SHIPMENT_DETAIL.TAX6_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX7,TSPL_SD_SHIPMENT_DETAIL.TAX7_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX8, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX8_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX9,TSPL_SD_SHIPMENT_DETAIL.TAX9_Rate, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX10,TSPL_SD_SHIPMENT_DETAIL.TAX10_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.Amount,TSPL_SD_SHIPMENT_DETAIL.Disc_Per,TSPL_SD_SHIPMENT_DETAIL.Disc_Amt,TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount, " &
            "TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt,TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX1_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX2_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX3_Base_Amt , " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX4_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX5_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX6_Base_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX7_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX8_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.TAX9_Base_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.TAX10_Base_Amt,TSPL_SD_SHIPMENT_DETAIL.MRP,TSPL_SD_SHIPMENT_DETAIL.Batch_No,TSPL_SD_SHIPMENT_DETAIL.MFG_Date, " &
            "TSPL_SD_SHIPMENT_DETAIL.Expiry_Date,TSPL_SD_SHIPMENT_DETAIL.Specification,TSPL_SD_SHIPMENT_DETAIL.Remarks,TSPL_SD_SHIPMENT_DETAIL.Assessable, " &
            "TSPL_SD_SHIPMENT_DETAIL.AssessableAmt,TSPL_SD_SHIPMENT_DETAIL.Bar_Code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Scheme_Applicable,TSPL_SD_SHIPMENT_DETAIL.Scheme_Code, " &
            "TSPL_SD_SHIPMENT_DETAIL.Scheme_Item,TSPL_SD_SHIPMENT_DETAIL.Item_Tax,TSPL_SD_SHIPMENT_DETAIL.Total_MRP_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.Total_Basic_Amt,TSPL_SD_SHIPMENT_DETAIL.Total_Disc_Amt,TSPL_SD_SHIPMENT_DETAIL.Cust_Discount, " &
            "TSPL_SD_SHIPMENT_DETAIL.Total_Cust_Discount,TSPL_SD_SHIPMENT_DETAIL.ActualRate,TSPL_SD_SHIPMENT_DETAIL.Cust_DiscountQty, " &
            "TSPL_SD_SHIPMENT_DETAIL.Price_code,TSPL_SD_SHIPMENT_DETAIL.Abatement_Per,TSPL_SD_SHIPMENT_DETAIL.Abatement_Amt, " &
            "TSPL_SD_SHIPMENT_DETAIL.FOC_Item,TSPL_SD_SHIPMENT_DETAIL.Item_Weight,TSPL_SD_SHIPMENT_DETAIL.Price_Date, " &
            "TSPL_SD_SHIPMENT_DETAIL.HeadDiscPer,TSPL_SD_SHIPMENT_DETAIL.HeadDiscPerAmt,TSPL_SD_SHIPMENT_DETAIL.Bin_No,TSPL_SD_SHIPMENT_DETAIL.TotalItem_Weight,TSPL_SD_SHIPMENT_DETAIL.Conv_Factor,TSPL_SD_SHIPMENT_DETAIL.Purchase_Cost,TSPL_SD_SHIPMENT_DETAIL.OrgRate,  " &
            "TSPL_SD_SHIPMENT_DETAIL.vendor_code,TSPL_SD_SHIPMENT_DETAIL.vendor_desc,TSPL_SD_SHIPMENT_DETAIL.PrincipleCode,TSPL_SD_SHIPMENT_DETAIL.PrincipleDesc,TSPL_SD_SHIPMENT_DETAIL.Markup_On,TSPL_SD_SHIPMENT_DETAIL.Markup_Percent,TSPL_SD_SHIPMENT_DETAIL.Landing_Cost,TSPL_SD_SHIPMENT_DETAIL.HeadDiscAmt,TSPL_SD_SHIPMENT_DETAIL.CustDiscPer,TSPL_SD_SHIPMENT_DETAIL.CasdDiscScheme_Code " &
            ",TSPL_SD_SHIPMENT_DETAIL.Item_Group,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,TSPL_SD_SHIPMENT_DETAIL.TAX_PAID,TSPL_SD_SHIPMENT_DETAIL.Commission_Rate,TSPL_SD_SHIPMENT_DETAIL.Commission_Party,TSPL_SD_SHIPMENT_DETAIL.Commission_Amt,TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Commission "
            qry += " ,TSPL_SD_SHIPMENT_DETAIL.Alternate_UOM,TSPL_SD_SHIPMENT_DETAIL.RATE_UOM,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No," &
                   " TSPL_BOOKING_MATSER.Created_By as Booking_User_Code,TSPL_USER_MASTER.Distributor_Retailer_Code,SecCust.Customer_Name as Distributor_Retailer_Name,SecCust.Email as Distributor_Retailer_Email,TSPL_Additional_Charges.Description as  AddChargeDesc,TSPL_SD_SHIPMENT_DETAIL.Sampling,TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_PKID,TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Rate,TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_RateWithTax,TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Amt,TSPL_SD_SHIPMENT_DETAIL.Security_Rate,TSPL_SD_SHIPMENT_DETAIL.Security_Amt FROM TSPL_SD_SHIPMENT_DETAIL "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code " & _
                   " left join TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " & _
                   " and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code " & _
                   " and TSPL_SD_SHIPMENT_DETAIL.Line_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No " & _
                   " left join TSPL_BOOKING_MATSER on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No=TSPL_BOOKING_MATSER.Document_No " & _
                   " left join TSPL_USER_MASTER on TSPL_BOOKING_MATSER.Created_By=TSPL_USER_MASTER.User_Code " & _
                   " left join TSPL_SECONDARY_CUSTOMER_MASTER SecCust on SecCust.Cust_Code=TSPL_USER_MASTER.Distributor_Retailer_Code" + _
                   " left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code "
            qry += " where TSPL_SD_SHIPMENT_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_SD_SHIPMENT_DETAIL.Line_No  asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsPSShipmentHeadDetail)
                Dim objTr As clsPSShipmentHeadDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsPSShipmentHeadDetail
                    objTr.Sampling = clsCommon.myCdbl(dr("Sampling"))
                    objTr.CAN = clsCommon.myCdbl(dr("CAN"))
                    objTr.Structure_Code = clsCommon.myCstr(dr("Structure_Code"))
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    objTr.Crate = clsCommon.myCdbl(dr("Crate"))
                    objTr.VS_ltrInCrate = clsCommon.myCdbl(dr("VS_ltrInCrate"))
                    objTr.VS_Cash_Amt = clsCommon.myCdbl(dr("VS_Cash_Amt"))
                    objTr.VS_CashSchemeCode = clsCommon.myCstr(dr("VS_CashSchemeCode"))
                    objTr.Sub_Location_code = clsCommon.myCstr(dr("Sub_Location_code"))
                    objTr.Disc_Scheme_Amount = clsCommon.myCdbl(dt.Rows(0)("Disc_Scheme_Amount"))
                    objTr.Disc_Scheme_Pers = clsCommon.myCdbl(dt.Rows(0)("Disc_Scheme_Pers"))
                    objTr.Disc_Scheme_Code = clsCommon.myCstr(dt.Rows(0)("Disc_Scheme_Code"))
                    objTr.Disc_Scheme_Type = clsCommon.myCstr(dt.Rows(0)("Disc_Scheme_Type"))
                    objTr.Delivery_Code = clsCommon.myCstr(dr("Delivery_Code"))
                    objTr.GatePass_No = clsCommon.myCstr(dr("GatePass_No"))
                    objTr.Alter_UnitQty = clsCommon.myCdbl(dr("Alter_UnitQty"))
                    objTr.Rate_UnitQty = clsCommon.myCdbl(dr("Rate_UnitQty"))
                    objTr.Cash_Scheme_Code = clsCommon.myCstr(dr("Cash_Scheme_Code"))
                    objTr.Cash_Scheme_Type = clsCommon.myCstr(dr("Cash_Scheme_Type"))
                    objTr.Cash_Scheme_Pers = clsCommon.myCdbl(dr("Cash_Scheme_Pers"))
                    objTr.Cash_Scheme_Amount = clsCommon.myCdbl(dr("Cash_Scheme_Amount"))

                    objTr.Scheme_Type = clsCommon.myCstr(dr("Scheme_Type"))
                    objTr.Scheme_Qty = clsCommon.myCdbl(dr("Scheme_Qty"))
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme_Item_UOM"))
                    objTr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme_Item_Code"))

                    objTr.Total_Item_WeightMetric = clsCommon.myCdbl(dr("Total_Item_WeightMetric"))
                    objTr.Alternate_UOM = clsCommon.myCstr(dr("Alternate_UOM"))
                    objTr.RATE_UOM = clsCommon.myCstr(dr("RATE_UOM"))
                    objTr.Commission_Rate = clsCommon.myCdbl(dr("Commission_Rate"))
                    objTr.Commission_Party = clsCommon.myCstr(dr("Commission_Party"))
                    objTr.Item_Group = clsCommon.myCstr(dr("Item_Group"))
                    objTr.TAX_PAID = clsCommon.myCstr(dr("TAX_PAID"))
                    objTr.Commission_Amt = clsCommon.myCdbl(dr("Commission_Amt"))
                    objTr.Amt_Less_Commission = clsCommon.myCdbl(dr("Amt_Less_Commission"))
                    objTr.OrgUnit_code = clsCommon.myCstr(dr("OrgUnit_code"))
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
                    objTr.Order_Code = clsCommon.myCstr(dr("Order_Code"))
                    objTr.Delivery_Code_PS = clsCommon.myCstr(dr("Delivery_Code_PS"))

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

                    '' done by panch Raj for WhollyCow
                    objTr.Booking_User_Code = clsCommon.myCstr(dr("Booking_User_Code"))
                    objTr.Distributor_Retailer_Code = clsCommon.myCstr(dr("Distributor_Retailer_Code"))
                    objTr.Distributor_Retailer_Name = clsCommon.myCstr(dr("Distributor_Retailer_Name"))
                    objTr.Distributor_Retailer_Email = clsCommon.myCstr(dr("Distributor_Retailer_Email"))
                    objTr.Distributor_Commission_PKID = clsCommon.myCstr(dr("Distributor_Commission_PKID"))
                    objTr.Distributor_Commission_Rate = clsCommon.myCdbl(dr("Distributor_Commission_Rate"))
                    objTr.Distributor_Commission_RateWithTax = clsCommon.myCdbl(dr("Distributor_Commission_RateWithTax"))
                    objTr.Distributor_Commission_Amt = clsCommon.myCdbl(dr("Distributor_Commission_Amt"))
                    objTr.Security_Rate = clsCommon.myCdbl(dr("Security_Rate"))
                    objTr.Security_Amt = clsCommon.myCdbl(dr("Security_Amt"))

                    objTr.arrSrItem = clsSerializeInvenotry.GetData("SD-IN", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    'objTr.arrBatchItem = clsBatchInventory.GetData("PS-SH", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData(obj.Trans_Type + "-SH", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
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
    End Function
    Public Shared Function CheckStock(ByVal objShipment As clsPSShipmentHead, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.CompairString(objShipment.Dispatch_Terms, "CIF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objShipment.Dispatch_Terms, "CF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objShipment.Dispatch_Terms, "FE") = CompairStringResult.Equal OrElse clsCommon.CompairString(objShipment.Dispatch_Terms, "O") = CompairStringResult.Equal Then
                If objShipment.Is_OwnVehicle = 0 Then
                    If clsCommon.myCdbl(objShipment.Freight_Charges) = 0 Then
                        Throw New Exception("Pls enter Freight in Route Freight Details in Document No " & objShipment.Document_Code & " .")
                    End If
                End If
            End If
            Dim isAllowStockCheckAtDOLevel As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowStockCheckatDOLevel, clsFixedParameterCode.AllowStockCheckatDOLevel, trans)) = 1
            For Each obj As clsPSShipmentHeadDetail In objShipment.Arr
                If clsCommon.myLen(obj.Unit_code) > 0 Then
                    Dim dblBalQty As Double = 0
                    Dim strCode As String = objShipment.Document_Code
                    If isAllowStockCheckAtDOLevel Then
                        strCode = obj.Delivery_Code_PS
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(objShipment.Bill_To_Location) & "'", trans)), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objShipment.Customer_Code) & "' ", trans)), "Others") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objShipment.Customer_Code) & "' ", trans)), "") = CompairStringResult.Equal Then
                            dblBalQty = clsItemLocationDetails.getBalance(obj.Item_Code, IIf(clsCommon.myLen(clsCommon.myCstr(objShipment.Sub_Location_code)) > 0, objShipment.Sub_Location_code, objShipment.Bill_To_Location), strCode, objShipment.Document_Date, trans, obj.Unit_code, obj.MRP)
                        Else
                            dblBalQty = clsItemLocationDetails.getBalance(obj.Item_Code, IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, objShipment.Bill_To_Location), strCode, objShipment.Document_Date, trans, obj.Unit_code, obj.MRP)
                        End If
                    Else
                        dblBalQty = clsItemLocationDetails.getBalance(obj.Item_Code, IIf(clsCommon.myLen(clsCommon.myCstr(objShipment.Sub_Location_code)) > 0, objShipment.Sub_Location_code, objShipment.Bill_To_Location), strCode, objShipment.Document_Date, trans, obj.Unit_code, obj.MRP)
                    End If
                    Dim dblEnteredQty As Double = obj.Qty
                    If dblEnteredQty > dblBalQty Then ' AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                        Throw New Exception("Document No - " + obj.Document_Code + Environment.NewLine + "Item - " + obj.Item_Code + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, Optional ByVal IsDairyModule As Boolean = False) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans, Nothing, IsDairyModule, "")
            trans.Commit()
        Catch ex As Exception
            Dim strEx As String = ex.Message
            Dim qry As String = "select IRN_No,qr_code,ack_no,ack_date,EWayBillNo, EwayBillDate,EwayBillValidDate,EWayBillRemarks from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + strDocNo + "'"
            Dim dtPortalInfo As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            trans.Rollback()

            Try
                If dtPortalInfo IsNot Nothing AndAlso dtPortalInfo.Rows.Count > 0 Then
                    Dim coll As New Hashtable()
                    If clsCommon.myLen(dtPortalInfo.Rows(0)("IRN_No")) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "IRN_No", clsCommon.myCstr(dtPortalInfo.Rows(0)("IRN_No")))
                        clsCommon.AddColumnsForChange(coll, "qr_code", clsCommon.myCstr(dtPortalInfo.Rows(0)("qr_code")))
                        clsCommon.AddColumnsForChange(coll, "ack_no", dtPortalInfo.Rows(0)("ack_no"))
                        If dtPortalInfo.Rows(0)("ack_date") IsNot DBNull.Value Then
                            clsCommon.AddColumnsForChange(coll, "ack_date", clsCommon.GetPrintDate(clsCommon.myCDate(dtPortalInfo.Rows(0)("ack_date")), "dd/MMM/yyyy hh:mm:ss tt"))
                        End If
                    End If

                    If clsCommon.myLen(dtPortalInfo.Rows(0)("EWayBillNo")) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "EWayBillNo", clsCommon.myCstr(dtPortalInfo.Rows(0)("EWayBillNo")))
                        If dtPortalInfo.Rows(0)("EwayBillDate") IsNot DBNull.Value Then
                            clsCommon.AddColumnsForChange(coll, "EwayBillDate", clsCommon.GetPrintDate(clsCommon.myCDate(dtPortalInfo.Rows(0)("EwayBillDate")), "dd/MMM/yyyy hh:mm:ss tt"))
                        End If
                        If dtPortalInfo.Rows(0)("EwayBillValidDate") IsNot DBNull.Value Then
                            clsCommon.AddColumnsForChange(coll, "EwayBillValidDate", clsCommon.GetPrintDate(clsCommon.myCDate(dtPortalInfo.Rows(0)("EwayBillValidDate")), "dd/MMM/yyyy hh:mm:ss tt"))
                        End If
                        clsCommon.AddColumnsForChange(coll, "EWayBillRemarks", clsCommon.myCstr(dtPortalInfo.Rows(0)("EWayBillRemarks")))
                    End If

                    If coll.Count > 0 Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "Against_Shipment_No='" + strDocNo + "'")
                    End If
                End If
            Catch ex2 As Exception
                strEx += Environment.NewLine + "Portal Info [" + ex2.Message + "]"
            End Try


            Try
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Error_Msg", strEx)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_EXCEPTION", OMInsertOrUpdate.Insert, "")
            Catch ex1 As Exception
            End Try

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing, Optional ByVal IsDairyModule As Boolean = False, Optional ByVal strARInvoiceNoRecreateOnly As String = Nothing, Optional ByVal strDispatchVoucherNo As String = Nothing) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim obj As clsPSShipmentHead = Nothing
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Shipment No not found to Post")
            End If

            If IsDairyModule = False Then
                obj = clsPSShipmentHead.GetData(strDocNo, NavigatorType.Current, trans)
            Else
                obj = clsPSShipmentHead.GetData(strDocNo, NavigatorType.Current, trans, IsDairyModule)
            End If
            Dim StockCheckOnPostForDairyDispatchMultiple As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StockCheckOnPostForDairyDispatchMultiple, clsFixedParameterCode.StockCheckOnPostForDairyDispatchMultiple, trans)) = 1, True, False)

            If StockCheckOnPostForDairyDispatchMultiple = True Then
                CheckStock(obj, trans)
            End If

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            '' Anubhooti 06-Sep-2014 BM00000003735 (Locked Transaction)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Bill_To_Location, obj.Document_Date, trans)
            ''
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmSaleDispatchDairy, obj.Bill_To_Location, obj.Document_Date, trans)
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Shipment No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""

            If Not clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.frmBulkMilkSRN, trans) Then
                If AdvanceReceived(obj.Delivery_Code_PS, trans) = False AndAlso obj.Is_Advance_Approved = 0 AndAlso obj.Advance_Approval_Reqd = 0 Then
                    Dim intExist As Integer = 0
                    If MessageBox.Show("DO you want approval for dispatch w/o advance against booking.", "Dispatch", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        If obj.Advance_Approval_Reqd = 0 Then
                            qry = "Update TSPL_SD_SHIPMENT_HEAD set Advance_Approval_Reqd=1 where Document_Code='" & obj.Document_Code & "' "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " &
                            "values ('Product Dispatch','" & clsUserMgtCode.frmShipmentProductSale & "','" & obj.Document_Code & "','" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy") & "','Advance Receipt',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        End If
                        'trans.Commit()
                        Return False
                        Exit Function
                    Else
                        'trans.Commit()
                        Return False
                        Exit Function
                    End If
                ElseIf AdvanceReceived(obj.Delivery_Code_PS, trans) = False AndAlso obj.Is_Advance_Approved = 0 AndAlso obj.Advance_Approval_Reqd = 1 Then
                    Throw New Exception("Approval is reqd. for this document." + obj.Document_Code + " ")
                End If
            Else
                If AdvanceReceived(obj.Delivery_Code_PS, trans) = False AndAlso obj.Is_Advance_Approved = 0 AndAlso obj.Advance_Approval_Reqd = 0 Then
                    qry = "Update TSPL_SD_SHIPMENT_HEAD set Advance_Approval_Reqd=1 where Document_Code='" & obj.Document_Code & "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SD_SHIPMENT_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                'trans.Commit()
                Return False
            End If

            UpdateInventoryMovement(obj, trans, False, IsDairyModule)
            '===update by preeti gupta Against ticket no[BHA/22/06/18-000081][added Setting Only]
            CreateSMSContent(obj, strVoucherNoForRecreateOnly, trans)

            Dim CalculateProvisionOnGateePass As Integer = 0 ' added by preeti gupta Against ticket no[UDL/10/01/19-000252]
            CalculateProvisionOnGateePass = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateProvisionOnGateePass, clsFixedParameterCode.CalculateProvisionOnGateePass, trans))
            If CalculateProvisionOnGateePass = 0 Then
                If clsCommon.myLen(obj.Transport_Id) > 0 Then
                    If clsCommon.CompairString(obj.Dispatch_Terms, "CIF") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dispatch_Terms, "CF") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dispatch_Terms, "FE") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dispatch_Terms, "O") = CompairStringResult.Equal Then
                        ConvertProvision(obj, trans)
                    End If
                End If
            End If

            'If clsCommon.myLen(obj.Delivery_Code_PS) > 0 Then
            '    Dim objSO As clsPSSalesOrder = ConvertShipmentToSaleOrder(obj)
            '    objSO.SaveData(objSO, True, False, trans)
            '    obj.Against_Sales_Order = objSO.Document_Code
            '    clsPSSalesOrder.PostData("", obj.Against_Sales_Order, trans)
            'End If

            CreateJournalEntry(obj.Document_Code, trans, strDispatchVoucherNo, IsDairyModule)

            qry = "Update TSPL_SD_SHIPMENT_HEAD set  Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''Invoice Should create after post of Shipment.
            If obj.Is_Create_Auto_Invoice Then
                'Dim objSI As clsPSInvoiceHead = ConvertShipmentToSaleInvoice(obj)
                'objSI.SaveData(objSI, True, trans)
                'obj.Sale_Invoice_No = objSI.Document_Code
                If IsDairyModule = False Then
                    obj.Sale_Invoice_No = clsDBFuncationality.getSingleValue("select top 1 TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_DETAIL inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code='" & obj.Document_Code & "' and isnull(TSPL_SD_SALE_INVOICE_HEAD .Invoice_No_For_Supplementary ,'')=''", trans)
                    clsPSInvoiceHead.PostData("", obj.Sale_Invoice_No, trans)
                Else
                    If Not clsCommon.CompairString(obj.DO_Item_Type, "NT") = CompairStringResult.Equal Then
                        obj.Sale_Invoice_No = clsDBFuncationality.getSingleValue("select top 1 TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_DETAIL inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code='" & obj.Document_Code & "' and isnull(TSPL_SD_SALE_INVOICE_HEAD .Invoice_No_For_Supplementary ,'')=''", trans)
                        clsPSInvoiceHead.PostData(FormId, obj.Sale_Invoice_No, trans, IsDairyModule, strVoucherNoForRecreateOnly, strARInvoiceNoRecreateOnly)
                    Else
                        obj.Sale_Invoice_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_head where Against_Shipment_No ='" & obj.Document_Code & "' and Total_Tax_Amt=0 and isnull(TSPL_SD_SALE_INVOICE_HEAD .Invoice_No_For_Supplementary ,'')=''", trans))
                        If clsCommon.myLen(obj.Sale_Invoice_No) > 0 Then
                            clsPSInvoiceHead.PostData(FormId, obj.Sale_Invoice_No, trans, IsDairyModule, strVoucherNoForRecreateOnly, strARInvoiceNoRecreateOnly)
                        End If

                        obj.Sale_Invoice_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_head where Against_Shipment_No ='" & obj.Document_Code & "' and Total_Tax_Amt > 0 and isnull(TSPL_SD_SALE_INVOICE_HEAD .Invoice_No_For_Supplementary ,'')=''", trans))
                        If clsCommon.myLen(obj.Sale_Invoice_No) > 0 Then
                            clsPSInvoiceHead.PostData(FormId, obj.Sale_Invoice_No, trans, IsDairyModule, strVoucherNoForRecreateOnly, strARInvoiceNoRecreateOnly)
                        End If
                    End If
                End If

            End If
            ''================ added parteek 16-01-2017
            Dim qrycheck As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Manual_Customer from tspl_customer_master where cust_code='" + obj.Customer_Code + "'", trans))
            Dim CreateAutoRecieptForManualCustomer As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateAutoRecieptForManualCustomer, clsFixedParameterCode.CreateAutoRecieptForManualCustomer, trans)) = 0, False, True)
            If ((clsCommon.CompairString(qrycheck, "Y") = CompairStringResult.Equal) AndAlso CreateAutoRecieptForManualCustomer = True) Then
                Dim strInvNo = obj.Document_Code
                Dim strBankCode As String = clsFixedParameter.GetData(clsFixedParameterType.AutoRecieptBankCode, clsFixedParameterCode.AutoRecieptBankCode, trans)
                If clsCommon.myLen(strBankCode) <= 0 Then
                    Throw New Exception("Default Bank code not found")
                End If
                Dim strPaymentCode As String = clsFixedParameter.GetData(clsFixedParameterType.AutoRecieptPaymentMode, clsFixedParameterCode.AutoRecieptPaymentMode, trans)
                If clsCommon.CompairString(strPaymentCode, "Cash") = CompairStringResult.Equal Then
                    ReciepEntryOfDispatch(strInvNo, strBankCode, strPaymentCode, trans)
                End If
            End If
            ''===============End
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SD_SHIPMENT_HEAD", "Document_Code", "TSPL_SD_SHIPMENT_DETAIL", "Document_Code", trans)
            Dim FindReasonWhyInvoiceIssueOccursOnErode As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FindReasonWhyInvoiceIssueOccursOnErode, clsFixedParameterCode.FindReasonWhyInvoiceIssueOccursOnErode, trans)) = 0, False, True)
            If FindReasonWhyInvoiceIssueOccursOnErode = True Then
                Dim isSaleInvoicePosted As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Status,0) from TSPL_SD_SALE_INVOICE_head where Document_Code='" + obj.Sale_Invoice_No + "'", trans)) = 0, False, True)
                Dim isShipmentPosted As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Status,0) from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strDocNo + "'", trans)) = 0, False, True)
                If isSaleInvoicePosted = True AndAlso isShipmentPosted = False Then
                    Throw New Exception("Please identify the issue why dispatch not post")
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReciepEntryOfDispatch(ByVal strInvNo As String, ByVal strBankCode As String, ByVal strPaymentCode As String, ByVal trans As SqlTransaction) As String
        Dim objSaleInv As New clsPSShipmentHead()
        Try

            objSaleInv = GetData(strInvNo, NavigatorType.Current, trans)
            Dim dblReceiptAmt As Double = objSaleInv.Total_Amt
            If dblReceiptAmt > 0 Then
                'Dim qry As String = "select SUM(Adjustment_Amount)  from TSPL_Receipt_Adjustment_Header where Doc_No='" + strInvNo + "' and Is_Post='Y'"
                'Dim dblAdjAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                'dblReceiptAmt = dblReceiptAmt - dblAdjAmt

                'Dim originalamt As Decimal = objSaleInv.Document_Total

                '---------------------------------------------------------------------------
                Dim obj As New clsRcptEntryHeader()
                obj.Entry_Desc = "Shipment No - " + strInvNo + " for Cash Memo No - " & objSaleInv.Document_Code & "  "
                obj.Receipt_Date = clsCommon.GetPrintDate(objSaleInv.Posting_Date, "dd/MMM/yyyy")
                obj.Receipt_Post_Date = clsCommon.GetPrintDate(objSaleInv.Posting_Date, "dd/MMM/yyyy")
                obj.Bank_Code = clsCommon.myCstr(strBankCode)

                obj.Receipt_Type = "R"
                obj.Payment_Code = clsCommon.myCstr(strPaymentCode)
                obj.Cheque_No = ""
                obj.Cheque_Date = Nothing
                obj.Cust_Code = clsCommon.myCstr(objSaleInv.Customer_Code)
                obj.Receipt_Amount = dblReceiptAmt
                obj.Balance_Amt = dblReceiptAmt
                obj.UnApply_Amt = dblReceiptAmt
                obj.Apply_By = ""
                obj.Apply_To = ""
                obj.IsSalesmanType = "N"
                obj.Salesman_Code = ""
                obj.Salesman_Name = ""
                obj.SecurityDeposit = "N"

                obj.ArrTr = New List(Of clsReceiptDettail)

                Dim objTr As New clsReceiptDettail()
                objTr.Apply = "Y"
                objTr.Receipt_Type = "I"
                objTr.TagType = "C"
                objTr.Document_No = strInvNo
                If clsCommon.myLen(strInvNo) > 0 Then
                    objTr.Document_Date = clsDBFuncationality.getSingleValue("Select Document_Date from TSPL_SD_Shipment_Head Where Document_Code='" + strInvNo + "'", trans)
                End If
                objTr.Original_Amt = dblReceiptAmt
                objTr.Pending_Balance = dblReceiptAmt
                objTr.Applied_Amount = dblReceiptAmt
                objTr.Adjustment_No = ""
                objTr.Comment = objSaleInv.Remarks
                obj.ArrTr.Add(objTr)

                If obj.SaveData(obj, True, trans) Then
                    clsRcptEntryHeader.funRcptPost(obj.Receipt_No, trans, "MReceivable", objSaleInv.Bill_To_Location, True)
                    clsCommon.MyMessageBoxShow("Receipt No: '" + obj.Receipt_No + "' create aganist dispatch no: '" + objSaleInv.Document_Code + "'")
                End If
                '---------------------------------------------------------------------------
                Return obj.Receipt_No
            Else
                Throw New Exception("Receipt can't be created.Invoice amount is zero.")

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Nothing
    End Function


    Shared Sub CreateSMSContent(ByVal obj As clsPSShipmentHead, ByVal strVoucherNoForRecreateOnly As String, ByVal trans As SqlTransaction)
        obj.allowSMSforSalePerson = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowSMSSendtoSalePerson & "'", trans)) = 0, False, True)

        Dim Form_ID As String = clsUserMgtCode.frmSaleDispatchDairy
        If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", trans)
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
                Dim qry As String = "select distinct tspl_customer_master.Customer_Name,substring (tspl_customer_master.Phone1 ,6,10) as MobileNo,tspl_customer_master.Email from tspl_customer_master"
                qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Customer_Code=tspl_customer_master.Cust_Code"
                qry += " where 2=2 and len(replace( isnull(substring(tspl_customer_master.Phone1,6,10),''),'_',''))>0 and Cust_Code='" + obj.Customer_Code + "' "
                '' work done on setting based client ALPHA 27/03/2018
                If clsCommon.myCBool(obj.allowSMSforSalePerson) = True Then
                    qry += "Union all"
                    qry += " select distinct TSPL_EMPLOYEE_MASTER.Emp_Name, (TSPL_EMPLOYEE_MASTER.PRESENT_MOBILE_NO) as MobileNo,TSPL_EMPLOYEE_MASTER.Email_id from TSPL_EMPLOYEE_MASTER"
                    qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.Emp_Code"
                    qry += " where 2=2 and len(replace( isnull((TSPL_EMPLOYEE_MASTER.PRESENT_MOBILE_NO),''),'_',''))>0 and EMP_CODE='" + obj.Salesman_Code + "' "
                End If

                Dim dtParty As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtParty IsNot Nothing AndAlso dtParty.Rows.Count > 0 Then
                    If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                        Dim objSMSH As New clsSMSHead()
                        objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Code, obj.Customer_Code)
                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Name, obj.Customer_Name)
                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.Document_Code)
                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SalePerson_Code, obj.Salesman_Code)
                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SalePerson_Name, obj.Salesman_Name)
                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.Total_Amt))
                        objSMSH.arrMobilNo = New List(Of String)()
                        If clsCommon.myCBool(obj.allowSMSforSalePerson) = True Then
                            For Each dr In dtParty.Rows
                                objSMSH.arrMobilNo.Add(clsCommon.myCstr(dr("MobileNo")))
                                objSMSH.SaveData(Form_ID, objSMSH, trans)
                                ' objSMSH = Nothing
                            Next
                            objSMSH = Nothing
                        Else
                            objSMSH.arrMobilNo.Add(clsCommon.myCstr(dtParty.Rows(0)("MobileNo")))
                            objSMSH.SaveData(Form_ID, objSMSH, trans)
                            objSMSH = Nothing
                        End If



                    End If

                    If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso clsCommon.myLen(dtParty.Rows(0)("Email")) > 0 Then
                        Dim objSMSH As New clsEMailHead()
                        objSMSH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                        objSMSH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.Document_Code)
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.Total_Amt))
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Cust_Code, obj.Customer_Code)
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Cust_Name, obj.Customer_Name)
                        objSMSH.arrEMail = New List(Of String)()
                        objSMSH.arrEMail.Add(clsCommon.myCstr(dtParty.Rows(0)("Email")))
                        objSMSH.SaveData(Form_ID, objSMSH, trans)
                        objSMSH = Nothing
                    End If
                End If
            End If
        End If
    End Sub

    Private Shared Function ConvertProvision(ByVal objShipment As clsPSShipmentHead, ByVal trans As SqlTransaction) As clsProvisionEntry
        Dim Qry As String = "select TSPL_PROVISION_ENTRY.Doc_No from TSPL_PROVISION_ENTRY left outer join TSPL_SD_SHIPMENT_HEAD on " & _
        "TSPL_PROVISION_ENTRY.Ref_Doc_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No where Prog_Code='SHIPMENT-PS' and " & _
        "convert(date,Doc_Date,103)='" & clsCommon.GetPrintDate(objShipment.Document_Date, "dd/MMM/yyyy") & "' and TSPL_SD_SHIPMENT_HEAD.VehicleNo='" & objShipment.VehicleNo & "' and " & _
        "TSPL_PROVISION_ENTRY.Loc_Code='" & objShipment.Bill_To_Location & " ' and TSPL_PROVISION_ENTRY.Vendor_Code='" & objShipment.Transport_Id & "' "
        '"and  TSPL_PROVISION_ENTRY.Amount < " & objShipment.Freight_Charges & " "
        Dim strProvisionNo = clsDBFuncationality.getSingleValue(Qry, trans)

        Qry = "Select Amount from TSPL_PROVISION_ENTRY where Doc_No='" & strProvisionNo & "'"
        Dim dblProvAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
        If clsCommon.myLen(strProvisionNo) = 0 Then
            Dim obj As New clsProvisionEntry()
            obj = New clsProvisionEntry()
            obj.isNewEntry = True
            obj.Doc_Date = objShipment.Document_Date
            obj.Vendor_Code = objShipment.Transport_Id
            obj.Vendor_Desc = objShipment.Transporter_Name
            obj.Vendor_Type = "Transporter For Product Sale"
            obj.Status = "No"
            obj.Ref_Doc_No = objShipment.Sale_Invoice_No
            obj.Prov_type = "Freight"
            obj.Amount = objShipment.Freight_Charges
            obj.Prog_Code = clsUserMgtCode.frmShipmentProductSale
            obj.Prov_Month = Month(objShipment.Document_Date)
            obj.Prov_Year = Year(objShipment.Document_Date)
            obj.Loc_Code = objShipment.Bill_To_Location
            obj.Loc_Desc = objShipment.BillToLocationName
            obj.Freight_Type = objShipment.Freight_Type
            obj.FixedCharge = objShipment.FixedCharge
            obj.EmptyCharge = objShipment.EmptyCharge
            If clsProvisionEntry.SaveData(obj, trans) Then
                If clsProvisionEntry.PostData(obj.Doc_No, trans, True) Then

                End If
            End If
        Else
            If dblProvAmount < objShipment.Freight_Charges Then
                Qry = "Update TSPL_PROVISION_ENTRY set Ref_Doc_No='" & objShipment.Sale_Invoice_No & "',Amount=" & objShipment.Freight_Charges & " where TSPL_PROVISION_ENTRY.Doc_No='" & strProvisionNo & "' "
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

        End If


        Return Nothing

    End Function
    Public Shared Function UpdateInventoryMovement(ByVal obj As clsPSShipmentHead, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False, Optional ByVal IsDairyModule As Boolean = False, Optional ByVal FromDateForAvg As Date? = Nothing, Optional ByVal ExtraWhrForAvg As String = Nothing) As Boolean
        Dim TransType_Str As String = ""
        Try


            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            If UpdateInventory = True Then
                clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.Document_Code & "'", trans)
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.Document_Code & "'", trans)
            End If
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            For Each objTr As clsPSShipmentHeadDetail In obj.Arr
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
                    End If
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    objInventoryMovemnt.InOut = "O"

                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(obj.Bill_To_Location) & "'", trans)), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(obj.Customer_Code) & "' ", trans)), "Others") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(obj.Customer_Code) & "' ", trans)), "") = CompairStringResult.Equal Then
                            objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, objTr.Location)
                        Else
                            objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(objTr.Sub_Location_code)) > 0, objTr.Sub_Location_code, objTr.Location)
                        End If
                    Else
                        objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, objTr.Location)
                    End If


                    'objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, objTr.Location)
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
                    'Ticket no -MIL/12/06/19-000096 Sanjay case handle for update customer after posting
                    objInventoryMovemnt.Is_Scheme_Item = IIf(objTr.Scheme_Item = "Yes", "Y", IIf(objTr.Scheme_Item = "No", "N", objTr.Scheme_Item))
                    'Ticket no -MIL/12/06/19-000096 Sanjay case handle for update customer after posting
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
            If IsDairyModule = False Then
                clsInventoryMovement.SaveData("PS-SH", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans, Nothing, ExtraWhrForAvg)
            Else
                TransType_Str = obj.Trans_Type
                TransType_Str = TransType_Str & "-SH"
                clsInventoryMovement.SaveData(TransType_Str, obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans, FromDateForAvg, ExtraWhrForAvg)
            End If
            ' done by priti on BHA/09/05/18-000022
            Dim AllowCrateCanPhysicalStock As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCratePhysicalStock, clsFixedParameterCode.AllowCratePhysicalStock, trans))
            Dim dblCrateQty As Double = obj.Crate
            Dim dblCanQty As Double = obj.ShippedCAN
            If AllowCrateCanPhysicalStock = 1 AndAlso IsDairyModule = True Then
                If dblCrateQty > 0 Then
                    Dim strCrateItem = clsDBFuncationality.getSingleValue("select top 1 Item_Code from TSPL_ITEM_MASTER where isnull(CRATE,0)=1", trans)
                    If clsCommon.myLen(strCrateItem) > 0 Then
                        Dim dblRate As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCrateRate, clsFixedParameterCode.ItemCrateRate, trans))
                        Dim strCrateUOM = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strCrateItem & "' and Default_UOM=1", trans)
                        Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strCrateItem, obj.Bill_To_Location, obj.Document_Code, obj.Document_Date, trans, strCrateUOM, 0)

                        If dblCrateQty > dblBalQty Then
                            Throw New Exception("Item - " + strCrateItem + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblCrateQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        Else
                            Dim strItemType As String = clsItemMaster.GetItemType(strCrateItem, trans)
                            Dim strItemTypeToSave As String = ""
                            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                strItemTypeToSave = "RM"
                            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                strItemTypeToSave = "OT"
                            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                strItemTypeToSave = "FT"
                            Else
                                strItemTypeToSave = strItemType
                            End If
                            Dim objInventoryMovemnt As New clsInventoryMovement()
                            Dim ArrInventoryMovementCrate As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                            objInventoryMovemnt.InOut = "O"


                            objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, obj.Bill_To_Location)

                            objInventoryMovemnt.Cust_Code = obj.Customer_Code
                            objInventoryMovemnt.Cust_Name = obj.Customer_Name

                            objInventoryMovemnt.Item_Code = strCrateItem
                            objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strCrateItem, trans)
                            objInventoryMovemnt.Qty = obj.Crate
                            objInventoryMovemnt.UOM = strCrateUOM
                            objInventoryMovemnt.Basic_Cost = dblRate
                            objInventoryMovemnt.MRP = 0
                            objInventoryMovemnt.Add_Cost = 0
                            objInventoryMovemnt.Net_Cost = 0
                            objInventoryMovemnt.ItemType = strItemTypeToSave
                            ArrInventoryMovementCrate.Add(objInventoryMovemnt)

                            clsInventoryMovement.SaveData(TransType_Str, obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCrate, trans)

                            Dim qry = "Update TSPL_SD_SHIPMENT_HEAD set Crate_Item='" & strCrateItem & "',Crate_ItemUnit='" & strCrateUOM & "',Crate_ItemRate='" & dblRate & "' where Document_Code='" & obj.Document_Code & "' "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "Update TSPL_SD_SALE_INVOICE_HEAD set Crate_Item='" & strCrateItem & "',Crate_ItemUnit='" & strCrateUOM & "',Crate_ItemRate='" & dblRate & "' where Document_Code='" & obj.Sale_Invoice_No & "' "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    Else

                        Throw New Exception("Please Create item as Can type Item.")
                    End If
                End If

                If dblCanQty > 0 Then
                    Dim strCanItem = clsDBFuncationality.getSingleValue("select top 1 Item_Code from TSPL_ITEM_MASTER where isnull(Can,0)=1", trans)
                    If clsCommon.myLen(strCanItem) > 0 Then
                        Dim dblRate As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, trans))
                        Dim strCanUOM = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strCanItem & "' and Default_UOM=1", trans)
                        Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strCanItem, obj.Bill_To_Location, obj.Document_Code, obj.Document_Date, trans, strCanUOM, 0)

                        If dblCanQty > dblBalQty Then
                            Throw New Exception("Item - " + strCanItem + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblCanQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        Else
                            Dim strItemType As String = clsItemMaster.GetItemType(strCanItem, trans)
                            Dim strItemTypeToSave As String = ""
                            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                strItemTypeToSave = "RM"
                            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                strItemTypeToSave = "OT"
                            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                strItemTypeToSave = "FT"
                            Else
                                strItemTypeToSave = strItemType
                            End If
                            Dim objInventoryMovemnt As New clsInventoryMovement()
                            Dim ArrInventoryMovementCan As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                            objInventoryMovemnt.InOut = "O"

                            objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, obj.Bill_To_Location)

                            objInventoryMovemnt.Cust_Code = obj.Customer_Code
                            objInventoryMovemnt.Cust_Name = obj.Customer_Name

                            objInventoryMovemnt.Item_Code = strCanItem
                            objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strCanItem, trans)
                            objInventoryMovemnt.Qty = obj.ShippedCAN
                            objInventoryMovemnt.UOM = strCanUOM
                            objInventoryMovemnt.Basic_Cost = dblRate
                            objInventoryMovemnt.MRP = 0
                            objInventoryMovemnt.Add_Cost = 0
                            objInventoryMovemnt.Net_Cost = 0
                            objInventoryMovemnt.ItemType = strItemTypeToSave
                            ArrInventoryMovementCan.Add(objInventoryMovemnt)
                            clsInventoryMovement.SaveData(TransType_Str, obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCan, trans)

                            Dim qry = "Update TSPL_SD_SHIPMENT_HEAD set Can_Item='" & strCanItem & "',Can_ItemUnit='" & strCanUOM & "',Can_ItemRate='" & dblRate & "' where Document_Code='" & obj.Document_Code & "' "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "Update TSPL_SD_SALE_INVOICE_HEAD set Can_Item='" & strCanItem & "',Can_ItemUnit='" & strCanUOM & "',Can_ItemRate='" & dblRate & "' where Document_Code='" & obj.Sale_Invoice_No & "' "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    Else
                        Throw New Exception("Please Create item as Can type Item.")
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing, Optional ByVal IsDairyModule As Boolean = False)
        Try

            Dim RecoControlACC As String = ""
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            RecoControlACC = "I"
        End If
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
            If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                Dim obj As New clsPSShipmentHead
                obj = clsPSShipmentHead.GetData(strCode, NavigatorType.Current, trans)
                Dim ArryLstGLAC As ArrayList = New ArrayList()
                Dim strInventoryControlAc As String = ""
                Dim strShipmentClearingAC As String = ""
                Dim dblTotalCost As Double = 0

                strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                  " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                   " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'", trans)
                strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Bill_To_Location, trans)

                If clsCommon.myLen(strShipmentClearingAC) = 0 Then
                    Throw New Exception("Please set Shipment clearing Account for first item")
                End If
                'done by priti BHA/14/06/18-000053
                Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 and Source_Doc_No='" & obj.Document_Code & "'", trans))

                Dim Acc() As String = {strShipmentClearingAC, dblCogsCost, "", "", "", "", "", "", "H"}
                ArryLstGLAC.Add(Acc)

                Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 and Source_Doc_No='" & obj.Document_Code & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                        " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                        strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Bill_To_Location, trans)

                        If clsCommon.myLen(strInventoryControlAc) = 0 Then
                            Throw New Exception("Please set Inventory Control Account for first item")
                        End If
                        Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost")), "", "", "", "", "", "", RecoControlACC}
                        ArryLstGLAC.Add(Acc1)

                        ''richa agarwal 27 Dec,2018 BHA/27/11/18-000718
                        Dim TransType_Str As String = ""
                        If IsDairyModule = False Then
                            TransType_Str = "PS-SH"
                        Else
                            TransType_Str = obj.Trans_Type & "-SH"
                        End If
                        If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                            clsInventoryMovement.UpdateInvControlAccount(strCode, TransType_Str, clsCommon.myCstr(dr("Item_Code")), "", strInventoryControlAc, "", trans)
                        End If
                        '------------------
                    Next
                End If
                '' BHA/30/10/18-000646 RICHA AGARWAL SEND CUSTOMER CODE AND CUSTOMER NAME INTO JOURNAL ENTRY AND TYPE C instead of O 30 Oct,2018
                If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                    transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, strVoucherNoForRecreateOnly, trans, obj.Document_Date, obj.Remarks, "SD-SH", "Shipment", obj.Document_Code, "", "C", obj.Customer_Code, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
                Else
                    transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, obj.Remarks, "SD-SH", "Shipment", obj.Document_Code, "", "C", obj.Customer_Code, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
                End If
            End If  '' Done By Pankaj Jha For Skipping Cogs GL

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Shared Function ConvertShipmentToSaleInvoice(ByVal objShipment As clsPSShipmentHead, ByVal trans As SqlTransaction, ByVal Sale_Invoice_No As String, ByVal SingleInvoice As Boolean, ByVal Taxable As Integer, ByVal IsDairyModule As Boolean) As clsPSInvoiceHead 'sanjay
        Dim obj As New clsPSInvoiceHead()
        obj = New clsPSInvoiceHead()
        obj.Nine_NR_No = objShipment.Nine_NR_No
        If IsDairyModule = False Then 'sanjay
            obj.Is_Taxable = objShipment.Is_Taxable
        Else
            obj.Is_Taxable = Taxable
            obj.Trans_type = objShipment.Trans_Type
        End If
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
        obj.podate = objShipment.Document_Date
        obj.RoundOffAmount = objShipment.RoundOffAmount
        obj.Total_Comm_Amt = objShipment.Total_Comm_Amt
        obj.Invoice_Type = objShipment.Invoice_Type
        obj.Document_Date = objShipment.Document_Date
        obj.Customer_Code = objShipment.Customer_Code
        obj.Customer_Name = objShipment.Customer_Name
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
        ''richa ERO/18/06/19-000645
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
            obj.Inv_Date = ""
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
        '-----------------richa 27/06/2014 Ticket No .BM00000002982----------
        obj.Mannual_Document_Code = objShipment.Mannual_Invoice_No
        obj.InvoiceManualNowithPrefix = objShipment.InvoiceManualNowithPrefix



        obj.Document_Code = Sale_Invoice_No
        If SingleInvoice = False AndAlso Taxable = 1 Then
            obj.Scheme_Tax_Group = objShipment.Scheme_Tax_Group
            obj.Tax_Group = objShipment.Scheme_Tax_Group
            'obj.RoundOffAmount = 0
        ElseIf SingleInvoice = False And Taxable = 0 Then
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
        If SingleInvoice = False Then
            obj.Discount_Amt = 0
            obj.Amount_Less_Discount = 0
            obj.Total_Amt = 0
            obj.Discount_Base = 0
        End If

        '-------------------------------------------------------------------
        If (objShipment.Arr IsNot Nothing AndAlso objShipment.Arr.Count > 0) Then
            obj.Arr = New List(Of clsPSInvoiceHeadDetail)
            Dim objTr As clsPSInvoiceHeadDetail
            For Each objShipmentDetail As clsPSShipmentHeadDetail In objShipment.Arr
                objTr = New clsPSInvoiceHeadDetail
                Dim IsTaxable As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & objShipmentDetail.Item_Code & "'", trans))
                If (SingleInvoice = True OrElse (SingleInvoice = False AndAlso IIf(Taxable = 0, IsTaxable = 0, IsTaxable = 1))) Then
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

                    objTr.Commission_Rate = objShipmentDetail.Commission_Rate
                    objTr.Commission_Party = objShipmentDetail.Commission_Party
                    objTr.Commission_Amt = objShipmentDetail.Commission_Amt
                    objTr.Amt_Less_Commission = objShipmentDetail.Amt_Less_Commission
                    objTr.Delivery_Code = objShipmentDetail.Delivery_Code
                    If SingleInvoice = False Then
                        If objShipmentDetail.FOC_Item = 1 Then
                            obj.Discount_Amt += objShipmentDetail.Amt_Less_Discount
                        Else
                            obj.Amount_Less_Discount += objShipmentDetail.Amt_Less_Discount
                            obj.Total_Amt += objShipmentDetail.Amt_Less_Discount
                        End If
                        obj.Discount_Base += objShipmentDetail.Amt_Less_Discount
                    End If
                    If SingleInvoice = False AndAlso Taxable = 1 Then

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
                    End If
                    obj.Arr.Add(objTr)
                End If
            Next
        End If
        Return obj
    End Function
    Private Shared Function ConvertShipmentToSaleOrder(ByVal objShipment As clsPSShipmentHead) As clsPSSalesOrder
        Dim obj As New clsPSSalesOrder()
        obj = New clsPSSalesOrder()
        obj.Auto_SaleOrder = 1
        obj.Total_Comm_Amt = objShipment.Total_Comm_Amt
        obj.Cust_PO_No = objShipment.Cust_PO_No
        obj.HeadDisc_Per = objShipment.HeadDisc_Per
        obj.HeadDisc_PerAmt = objShipment.HeadDisc_PerAmt
        obj.HeadDisc_Amt = objShipment.HeadDisc_Amt
        obj.Road_Permit_No = objShipment.Road_Permit_No
        obj.Price_Group_Code = objShipment.Price_Group_Code
        obj.Route_No = objShipment.Route_No
        obj.Route_Desc = objShipment.Route_Desc
        obj.Price_Code = objShipment.Price_Code
        obj.Document_Date = objShipment.Document_Date
        obj.CloseSO = "N"
        obj.Delivery_date = objShipment.Document_Date
        obj.Customer_Code = objShipment.Customer_Code
        obj.Customer_Name = objShipment.Customer_Name
        obj.Ref_No = objShipment.Ref_No
        obj.Total_Tax_Amt = objShipment.Total_Tax_Amt
        obj.Remarks = objShipment.Remarks
        obj.Bill_To_Location = objShipment.Bill_To_Location
        obj.Ship_To_Location = objShipment.Ship_To_Location
        obj.Comments = objShipment.Comments
        obj.On_Hold = objShipment.On_Hold
        obj.Mode_Of_Transport = "By Road"
        obj.Description = objShipment.Description
        obj.Tax_Group = objShipment.Tax_Group
        obj.SalesOrder_Type = ""
        obj.Item_Type = objShipment.Item_Type
        obj.Dept = objShipment.Dept
        obj.Dept_Desc = objShipment.Dept_Desc
        obj.PROJECT_ID = objShipment.PROJECT_ID
        obj.Approvel_Required = 0
        If clsCommon.myLen(objShipment.TAX1) > 0 Then
            obj.TAX1 = objShipment.TAX1
            obj.TAX1_Rate = objShipment.TAX1_Rate
            obj.TAX1_Base_Amt = objShipment.TAX1_Base_Amt
            obj.TAX1_Amt = objShipment.TAX1_Amt
        End If
        If clsCommon.myLen(objShipment.TAX2) > 0 Then
            obj.TAX2 = objShipment.TAX2
            obj.TAX2_Rate = objShipment.TAX2_Rate
            obj.TAX2_Base_Amt = objShipment.TAX2_Base_Amt
            obj.TAX2_Amt = objShipment.TAX2_Amt
        End If
        If clsCommon.myLen(objShipment.TAX3) > 0 Then
            obj.TAX3 = objShipment.TAX3
            obj.TAX3_Rate = objShipment.TAX3_Rate
            obj.TAX3_Base_Amt = objShipment.TAX3_Base_Amt
            obj.TAX3_Amt = objShipment.TAX3_Amt
        End If
        If clsCommon.myLen(objShipment.TAX4) > 0 Then
            obj.TAX4 = objShipment.TAX4
            obj.TAX4_Rate = objShipment.TAX4_Rate
            obj.TAX4_Base_Amt = objShipment.TAX4_Base_Amt
            obj.TAX4_Amt = objShipment.TAX4_Amt
        End If
        If clsCommon.myLen(objShipment.TAX5) > 0 Then
            obj.TAX5 = objShipment.TAX1
            obj.TAX5_Rate = objShipment.TAX5_Rate
            obj.TAX5_Base_Amt = objShipment.TAX5_Base_Amt
            obj.TAX5_Amt = objShipment.TAX5_Amt
        End If
        If clsCommon.myLen(objShipment.TAX6) > 0 Then
            obj.TAX6 = objShipment.TAX6
            obj.TAX6_Rate = objShipment.TAX6_Rate
            obj.TAX6_Base_Amt = objShipment.TAX6_Base_Amt
            obj.TAX6_Amt = objShipment.TAX6_Amt
        End If
        If clsCommon.myLen(objShipment.TAX7) > 0 Then
            obj.TAX7 = objShipment.TAX7
            obj.TAX7_Rate = objShipment.TAX7_Rate
            obj.TAX7_Base_Amt = objShipment.TAX7_Base_Amt
            obj.TAX7_Amt = objShipment.TAX7_Amt
        End If
        If clsCommon.myLen(objShipment.TAX8) > 0 Then
            obj.TAX8 = objShipment.TAX8
            obj.TAX8_Rate = objShipment.TAX8_Rate
            obj.TAX8_Base_Amt = objShipment.TAX8_Base_Amt
            obj.TAX8_Amt = objShipment.TAX8_Amt
        End If
        If clsCommon.myLen(objShipment.TAX9) > 0 Then
            obj.TAX9 = objShipment.TAX9
            obj.TAX9_Rate = objShipment.TAX9_Rate
            obj.TAX9_Base_Amt = objShipment.TAX9_Base_Amt
            obj.TAX9_Amt = objShipment.TAX9_Amt
        End If
        If clsCommon.myLen(objShipment.TAX1) > 0 Then
            obj.TAX10 = objShipment.TAX10
            obj.TAX10_Rate = objShipment.TAX10_Rate
            obj.TAX10_Base_Amt = objShipment.TAX10_Base_Amt
            obj.TAX10_Amt = objShipment.TAX10_Amt
        End If


        obj.Terms_Code = objShipment.Terms_Code
        obj.Due_Date = objShipment.Due_Date
        obj.Discount_Base = objShipment.Discount_Base
        obj.Discount_Amt = objShipment.Discount_Amt
        obj.Amount_Less_Discount = objShipment.Amount_Less_Discount
        obj.Total_Amt = objShipment.Total_Amt
        obj.Abandonment_No = 0
        'obj.Against_Quotation_No = txtReqNo.Value
        obj.Against_DeliveryNo = ""
        obj.SO_Validity = 0
        obj.Commission_Apply = objShipment.Commission_Apply
        obj.Dispatch_date = objShipment.Dispatch_date
        obj.Vehicle_Code = objShipment.Vehicle_Code
        obj.Vehicle_No = objShipment.VehicleNo
        obj.Vehicle_Capacity = objShipment.Vehicle_Capacity
        obj.Payment_Terms = objShipment.Payment_Terms
        obj.Dispatch_Terms = objShipment.Dispatch_Terms
        obj.Dispatch_Period = objShipment.Dispatch_Period
        'If clsCommon.myLen(txtReqNo.Value) = 0 Then
        '    txtDispatchDate.Value = txtDeliveryDate.Value.AddDays(txtDispatchPeriod.Value)
        'End If



        If clsCommon.myLen(obj.Add_Charge_Code1) > 0 Then
            obj.Add_Charge_Code1 = objShipment.Add_Charge_Code1
            obj.Add_Charge_Name1 = objShipment.Add_Charge_Name1
            obj.Add_Charge_Amt1 = objShipment.Add_Charge_Amt1
        End If
        If clsCommon.myLen(obj.Add_Charge_Code2) > 0 Then
            obj.Add_Charge_Code2 = objShipment.Add_Charge_Code2
            obj.Add_Charge_Name2 = objShipment.Add_Charge_Name2
            obj.Add_Charge_Amt2 = objShipment.Add_Charge_Amt2
        End If
        If clsCommon.myLen(obj.Add_Charge_Code3) > 0 Then
            obj.Add_Charge_Code3 = objShipment.Add_Charge_Code3
            obj.Add_Charge_Name3 = objShipment.Add_Charge_Name3
            obj.Add_Charge_Amt3 = objShipment.Add_Charge_Amt3
        End If
        If clsCommon.myLen(obj.Add_Charge_Code4) > 0 Then
            obj.Add_Charge_Code4 = objShipment.Add_Charge_Code4
            obj.Add_Charge_Name4 = objShipment.Add_Charge_Name4
            obj.Add_Charge_Amt4 = objShipment.Add_Charge_Amt4
        End If
        If clsCommon.myLen(obj.Add_Charge_Code5) > 0 Then
            obj.Add_Charge_Code5 = objShipment.Add_Charge_Code5
            obj.Add_Charge_Name1 = objShipment.Add_Charge_Name5
            obj.Add_Charge_Amt5 = objShipment.Add_Charge_Amt5
        End If
        If clsCommon.myLen(obj.Add_Charge_Code6) > 0 Then
            obj.Add_Charge_Code6 = objShipment.Add_Charge_Code6
            obj.Add_Charge_Name6 = objShipment.Add_Charge_Name6
            obj.Add_Charge_Amt6 = objShipment.Add_Charge_Amt6
        End If
        If clsCommon.myLen(obj.Add_Charge_Code7) > 0 Then
            obj.Add_Charge_Code7 = objShipment.Add_Charge_Code7
            obj.Add_Charge_Name7 = objShipment.Add_Charge_Name7
            obj.Add_Charge_Amt7 = objShipment.Add_Charge_Amt7
        End If
        If clsCommon.myLen(obj.Add_Charge_Code8) > 0 Then
            obj.Add_Charge_Code8 = objShipment.Add_Charge_Code8
            obj.Add_Charge_Name8 = objShipment.Add_Charge_Name8
            obj.Add_Charge_Amt8 = objShipment.Add_Charge_Amt8
        End If
        If clsCommon.myLen(obj.Add_Charge_Code9) > 0 Then
            obj.Add_Charge_Code9 = objShipment.Add_Charge_Code9
            obj.Add_Charge_Name9 = objShipment.Add_Charge_Name9
            obj.Add_Charge_Amt9 = objShipment.Add_Charge_Amt9
        End If
        If clsCommon.myLen(obj.Add_Charge_Code10) > 0 Then
            obj.Add_Charge_Code10 = objShipment.Add_Charge_Code10
            obj.Add_Charge_Name10 = objShipment.Add_Charge_Name10
            obj.Add_Charge_Amt10 = objShipment.Add_Charge_Amt10
        End If
        obj.Total_Add_Charge = objShipment.Total_Add_Charge

        obj.Salesman_Code = objShipment.Salesman_Code
        obj.Salesman_Name = objShipment.Salesman_Name

        obj.Arr = New List(Of clsPSSalesOrderDetail)
        For Each objShipmentDetail As clsPSShipmentHeadDetail In objShipment.Arr

            Dim objTr As New clsPSSalesOrderDetail()
            objTr.Line_No = objShipmentDetail.Line_No
            objTr.Row_Type = objShipmentDetail.Row_Type
            objTr.Item_Code = objShipmentDetail.Item_Code
            objTr.Item_Desc = objShipmentDetail.Item_Desc
            objTr.Qty = objShipmentDetail.Qty
            objTr.Balance_Qty = 0
            objTr.Unit_code = objShipmentDetail.Unit_code
            objTr.OrgUnit_code = objShipmentDetail.OrgUnit_code
            'objTr.Quotation_Code = clsCommon.myCstr(grow.Cells(colReqistionNo).Value)
            objTr.Location = objShipmentDetail.Location
            objTr.Ship_Party = obj.Customer_Code
            objTr.Location = objShipmentDetail.Location
            objTr.Item_Cost = objShipmentDetail.Item_Cost
            objTr.Amount = objShipmentDetail.Amount
            objTr.Disc_Per = objShipmentDetail.Disc_Per
            objTr.Disc_Amt = objShipmentDetail.Disc_Amt
            objTr.Amt_Less_Discount = objShipmentDetail.Amt_Less_Discount
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
            objTr.Total_Tax_Amt = objShipmentDetail.Total_Tax_Amt
            objTr.Item_Net_Amt = objShipmentDetail.Item_Net_Amt
            objTr.Specification = objShipmentDetail.Specification
            objTr.Remarks = objShipmentDetail.Remarks
            'objTr.Location = txtBillToLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
            objTr.MRP = objShipmentDetail.MRP
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
            objTr.Price_Date = objShipmentDetail.Price_Date
            objTr.Price_code = objShipmentDetail.Price_code
            objTr.Abatement_Per = objShipmentDetail.Abatement_Per
            objTr.Abatement_Amt = objShipmentDetail.Abatement_Amt
            objTr.FOC_Item = objShipmentDetail.FOC_Item

            objTr.Item_Weight = objShipmentDetail.Item_Weight
            objTr.Conv_Factor = objShipmentDetail.Conv_Factor
            objTr.TotalItem_Weight = objShipmentDetail.TotalItem_Weight
            objTr.Batch_No = objShipmentDetail.Batch_No
            objTr.Bin_No = objShipmentDetail.Bin_No
            objTr.HeadDiscPer = objShipmentDetail.HeadDiscPer
            objTr.HeadDiscPerAmt = objShipmentDetail.HeadDiscPerAmt
            objTr.Expiry_Date = objShipmentDetail.Expiry_Date
            objTr.MFG_Date = objShipmentDetail.MFG_Date
            objTr.Markup_On = objShipmentDetail.Markup_On
            objTr.Markup_Percent = objShipmentDetail.Markup_Percent
            objTr.Landing_Cost = objShipmentDetail.Landing_Cost
            objTr.CustDiscPer = objShipmentDetail.CustDiscPer
            objTr.HeadDiscAmt = objShipmentDetail.HeadDiscAmt
            objTr.CasdDiscScheme_Code = objShipmentDetail.CasdDiscScheme_Code
            objTr.Purchase_Cost = objShipmentDetail.Purchase_Cost
            objTr.OrgRate = objShipmentDetail.OrgRate
            objTr.PrincipleCode = objShipmentDetail.PrincipleCode
            objTr.PrincipleDesc = objShipmentDetail.PrincipleDesc
            objTr.vendor_code = objShipmentDetail.vendor_code
            objTr.vendor_desc = objShipmentDetail.vendor_desc

            objTr.HeadDiscPer = objShipmentDetail.HeadDiscPer
            objTr.HeadDiscPerAmt = objShipmentDetail.HeadDiscPerAmt

            objTr.Commission_Rate = objShipmentDetail.Commission_Rate
            objTr.Commission_Party = objShipmentDetail.Commission_Party
            objTr.Commission_Amt = objShipmentDetail.Commission_Amt
            objTr.Amt_Less_Commission = objShipmentDetail.Amt_Less_Commission
            objTr.Ship_Party = obj.Customer_Code
            objTr.delivery_code = ""
            ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
            ''objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
            If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                obj.Arr.Add(objTr)
            End If
        Next

        Return obj
    End Function

    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsPSShipmentHeadDetail)) As String


        For Each objtr As clsPSShipmentHeadDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal strInvoiceNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(strCode, strInvoiceNo, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal strInvoiceNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Shipment No not found to Delete")
        End If
        Dim obj As clsPSShipmentHead = clsPSShipmentHead.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                '' Anubhooti 06-Sep-2014 BM00000003735 (Locked Transaction)
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Bill_To_Location, obj.Document_Date, trans)
                ''
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmSaleDispatchDairy, obj.Bill_To_Location, obj.Document_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If

                CheckInvoicePostedOnGovtProtal(strCode, trans)

                clsSerializeInvenotry.DeleteData("SD-IN", strCode, trans)
                'clsBatchInventory.DeleteData("PS-SH", strCode, trans)

                ' done by priti BHA/12/06/18-000049
                Dim TransType_Str As String = ""
                TransType_Str = obj.Trans_Type
                TransType_Str = TransType_Str & "-SH"
                clsBatchInventory.DeleteData(TransType_Str, obj.Document_Code, trans)

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SD_SHIPMENT_HEAD", "Document_Code", "TSPL_SD_SHIPMENT_DETAIL", "Document_Code", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strInvoiceNo, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", trans)
                Dim qry As String = "delete from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strInvoiceNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + strInvoiceNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SHIPMENT_BOOKING_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SHIPMENT_CHECKLIST_DETAIL where Shipment_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Private Shared Function CheckInvoicePostedOnGovtProtal(strCode As String, trans As SqlTransaction) As Boolean
        Dim qry As String = "select Document_Code,IRN_No,EWayBillNo from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If clsCommon.myLen(dt.Rows(0)("IRN_No")) > 0 Then
                Throw New Exception("IRN No of Invoice [" + clsCommon.myCstr(dt.Rows(0)("Document_Code")) + "] is Genreated Can't Change in Invoice")
            End If
            If clsCommon.myLen(dt.Rows(0)("EWayBillNo")) > 0 Then
                Throw New Exception("E-Way Bill No of Invoice [" + clsCommon.myCstr(dt.Rows(0)("Document_Code")) + "] is Genreated Can't Change in Invoice")
            End If
        End If
        Return True
    End Function

    Public Shared Function IsValidCustomer(ByVal Arr As List(Of String), ByVal strCustomerCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Customer_Code not in ('" + strCustomerCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "SRN No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Customer Code: " + clsCommon.myCstr(dr("Customer_Code")) + " Customer Name:" + clsCommon.myCstr(dr("Customer_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    ' done by priti TEC/15/03/18-000091
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim TransType_Str As String = ""
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select Status from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            '' richa ERO/10/11/21-001547
            Dim strDairyGAtePassCount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select gpcode from TSPL_SD_SHIPMENT_HEAD where document_code='" & strCode & "' ", trans))
            If clsCommon.myLen(strDairyGAtePassCount) > 0 Then
                Throw New Exception("You cannot cancelled this document because Dairy GAte Pass (" + clsCommon.myCstr(strDairyGAtePassCount) + ") has been created.")
            End If
            Dim strGatepassdoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode from TSPL_SD_SHIPMENT_DETAIL left join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.PK_ID=TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID where TSPL_SD_SHIPMENT_DETAIL.PK_ID in (select top 1 PK_ID from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + strCode + "')", trans))
            If clsCommon.myLen(strGatepassdoc) > 0 Then
                Throw New Exception("Shipment cannot be reverse because its GatePass has been generated.")

            End If
            ''Qry = "select distinct DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_DETAIL where Shipment_Code='" + strCode + "'"
            Qry = " select distinct TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_DETAIL  inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE where TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code='" & strCode & "' and isnull(TSPL_SD_SALE_INVOICE_HEAD .Invoice_No_For_Supplementary ,'')=''"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsPSInvoiceHead.ReverseAndUnpost(clsCommon.myCstr(dr("DOCUMENT_CODE")), trans)
                Next
                ' done by priti TEC/15/03/18-000092
                'Qry = "Current Shipment is used in following Sale invoice -"
                'For Each dr As DataRow In dt.Rows
                '    Qry += Environment.NewLine + clsCommon.myCstr(dr("DOCUMENT_CODE"))
                'Next
                'Throw New Exception(Qry)
            End If

            'sanjay
            TransType_Str = clsDBFuncationality.getSingleValue("SELECT isnull(Trans_Type,'') as Trans_Type FROM TSPL_SD_SHIPMENT_HEAD where Document_Code = '" + strCode + "'", trans)

            TransType_Str = TransType_Str + "-SH"
            '' delete from inventory movement
            'Qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type='PS-SH' and document_code='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & strCode & "' and Trans_Type= 'PS-SH'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" & strCode & "' and Trans_Type= 'PS-SH'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "tspl_batch_item", "document_code", trans)

            Qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type='" + TransType_Str + "' and document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & strCode & "' and Trans_Type= '" + TransType_Str + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" & strCode & "' and Trans_Type= '" + TransType_Str + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            'sanjay

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SD-SH' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
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

            Qry = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set is_reverse=1 where document_code='" + strCode + "' and trans_code='" + clsCommon.myCstr(clsUserMgtCode.frmShipmentProductSale) + "' and is_reverse=0"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SD_SHIPMENT_HEAD", "Document_Code", "TSPL_SD_SHIPMENT_DETAIL", "Document_Code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Public Sub CreateEmailContent(ByVal Booking_No As String, ByVal trans As SqlTransaction)
        Dim Form_ID As String = clsUserMgtCode.frmSaleDispatchDairy
        '' get all distributers
        Dim DistrArr As New ArrayList
        Dim DistrEmail As New ArrayList
        'For Each objTr As clsPSShipmentHeadDetail In obj.Arr
        '    If clsCommon.myLen(objTr.Distributor_Retailer_Code) > 0 Then
        '        If DistrArr.Contains(objTr.Distributor_Retailer_Code) = False AndAlso clsCommon.myLen(objTr.Distributor_Retailer_Code) > 0 AndAlso clsCommon.myLen(objTr.Distributor_Retailer_Email) > 0 Then
        '            DistrArr.Add(objTr.Distributor_Retailer_Code)
        '            DistrEmail.Add(objTr.Distributor_Retailer_Email)
        '        End If
        '    End If
        'Next
        Dim Qry As String = " select TSPL_Dispatch_Distributor.Document_No,TSPL_Dispatch_Distributor.Document_Date,TSPL_Dispatch_Distributor.Distributor_Code," & _
                            " TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name,TSPL_SECONDARY_CUSTOMER_MASTER.Email from TSPL_Dispatch_Distributor " & _
                            " left join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_Dispatch_Distributor.Distributor_Code=TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code where TSPL_Dispatch_Distributor.Booking_No='" & Booking_No & "'"
        Dim dtParty As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
        'For Each dr As DataRow In dtDist.Rows
        '    If clsCommon.myLen(dr.Item("Email")) > 0 Then
        '        DistrArr.Add(clsCommon.myCstr(dr.Item("Distributor_Code")))
        '        DistrEmail.Add(clsCommon.myCstr(dr.Item("Email")))
        '    End If
        'Next
        Qry = GetQuery()
        '' loop for all distributers for entered dispatch
        For Each drParty As DataRow In dtParty.Rows
            Qry += " where TSPL_Dispatch_Distributor.Booking_No ='" & Booking_No & "' and TSPL_Dispatch_Distributor.DistributOr_Code='" & clsCommon.myCstr(drParty.Item("DistributOr_Code")) & "'"
            Qry = "Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2"
            ' UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            'Dim Qry2 As String = "select TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE  as InvoiceNo,Abatement_Amt,TSPL_ITEM_MASTER.Item_Code ,TSPL_ITEM_MASTER.Item_Desc + '( MRP : ' +   convert(varchar,TSPL_SD_SALE_INVOICE_DETAIL.MRP) + '  Abatement : ' + convert(varchar,convert(int,100- TSPL_SD_SALE_INVOICE_DETAIL.Abatement_Per)) + '%)'  as Item_Desc ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate,TSPL_ITEM_MASTER.Cheapter_Heads  ,tax1.Tax_Code_Desc as tax1name,tax2.Tax_Code_Desc as tax2name,tax3.Tax_Code_Desc as tax3name   from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2  left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE       "
            'Qry2 += " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code ='" & obj.Document_Code & "'"
            'Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry2)

            Dim AttachmentPath As String = Application.StartupPath
            Dim count As Integer = 0
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim UOMKG As String = String.Empty

                For i As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("UOM")), clsCommon.myCstr(dt.Rows(0)("UOM"))) = CompairStringResult.Equal Then
                        count = count + 1

                    End If
                Next
            End If
            dt.Columns.Add("UOMKG")
            If dt.Rows.Count = count Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("UOMKG") = "T"
                Next
            Else
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("UOMKG") = "F"
                Next
            End If
            Dim strTrgtFile As String = ("DistributerInvoice_" & Booking_No).ToString.Replace("/", "").Replace("\", "")
            clsERPFuncationalityOLD.exportCrystalToPDF(dt, AttachmentPath & "\Crystal Reports\Kwality Sales Report", "rptDistributerProductSaleInvoice", strTrgtFile, AttachmentPath)

            'If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", trans)
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
                'Qry = "select Customer_Name,substring (Phone1 ,6,10) as MobileNo,Email from tspl_customer_master where Cust_Code='" + obj.Customer_Code + "' "
                'Dim dtParty As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                If dtParty IsNot Nothing AndAlso dtParty.Rows.Count > 0 Then
                    If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso clsCommon.myLen(drParty("Email")) > 0 Then
                        Dim objSMSH As New clsEMailHead()
                        objSMSH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                        objSMSH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, Booking_No)
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(drParty.Item("Document_Date"), "dd/MMM/yyyy"))
                        'objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.Total_Amt))
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Cust_Code, clsCommon.myCstr(drParty.Item("Distributor_Code")))
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(drParty.Item("Customer_Name")))
                        objSMSH.arrEMail = New List(Of String)()
                        objSMSH.arrEMail.Add(clsCommon.myCstr(drParty("Email")))
                        objSMSH.Attachment_1_Path = AttachmentPath
                        objSMSH.SaveData(Form_ID, objSMSH, trans)
                        objSMSH = Nothing

                    End If
                End If
            End If
        Next

        'End If
    End Sub
    Public Function GetQuery() As String
        Dim Qry As String = String.Empty
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "WHOLLY") = CompairStringResult.Equal Then
            Qry = " select TSPL_SD_SALE_INVOICE_HEAD.Remarks, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.jaali,TSPL_SD_SHIPMENT_HEAD.box, TSPL_SD_SALE_INVOICE_head.Invoice_Type AS GP_Invoice_Type, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ," & _
             " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,case when TSPL_SD_SALE_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date," & _
             " TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end" & _
             " + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end " & _
             " + Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " & _
             " + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " & _
             " as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty," & _
             " TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No, TSPL_SD_SHIPMENT_HEAD.Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,(case when len(coalesce(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No,''))>0 then  (TSPL_SD_SALE_INVOICE_HEAD.Document_Code + '/' + coalesce(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No,'')) else TSPL_SD_SALE_INVOICE_HEAD.Document_Code end) as InvoiceNo,TSPL_Dispatch_Distributor.Document_No as Distributer_Dispatch_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate " & _
             " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate," & _
             " TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition," & _
             " TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Loc_Short_Name , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ " & _
             " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone," & _
             " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo," & _
             " TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode," & _
             " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') " & _
             " as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3," & _
             " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, coalesce(p_cust.P_cust_code,TSPL_CUSTOMER_MASTER.Cust_Code) as P_CustCode, coalesce(p_cust .P_cust_name,TSPL_CUSTOMER_MASTER.Customer_Name) as P_Cust_Name," & _
             " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," & _
             " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," & _
             " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then TSPL_CUSTOMER_MASTER  .PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan, " & _
             " TSPL_SD_SALE_INVOICE_DETAIL.item_code  as item_code,((case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then '(TS)' else '' end)+TSPL_ITEM_MASTER.Item_Desc )as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,(case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then  TSPL_SD_SALE_INVOICE_DETAIL.Amt_less_Discount else 0 end) as Scheme_Amount,(case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' AND TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='CRATE' then  TSPL_SD_SALE_INVOICE_DETAIL.Qty else 0 end) as CRATES_QTY," & _
            "(case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' AND TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='CRATE' then  TSPL_SD_SALE_INVOICE_DETAIL.Qty*(ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0 ))else 0 end) as PCS_QTY, " & _
            "(case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' AND TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='PP' then  TSPL_SD_SALE_INVOICE_DETAIL.Qty else 0 end) as SCH_PCS_QTY," & _
           " Item_Cost_Main= ROUND((case when ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)>0 THEN ( CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Unit_code!='PP' THEN  TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost/ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) ELSE TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost END ) else TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost end) ,2),TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty as qty" & _
             "  ,TSPL_SD_SALE_INVOICE_DETAIL.mrp, (TSPL_SD_SALE_INVOICE_DETAIL.amount+coalesce(tspl_item_master.CNF_Commission,0)*TSPL_SD_SALE_INVOICE_DETAIL.Qty) as amount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM" & _
             "  ,(TSPL_SD_SALE_INVOICE_DETAIL.item_cost+coalesce(tspl_item_master.CNF_Commission,0)) as itemcost,TSPL_SD_SALE_INVOICE_DETAIL.Line_No, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt," & _
             "  tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt,   tax3.Tax_Code_Desc as tax3name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt,   tax4.Tax_Code_Desc as tax4name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  ,isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount,coalesce(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No,'') AS Booking_No,(coalesce(tspl_item_master.CNF_Commission,0)*TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Distr_Commision " & _
             "  from TSPL_SD_SALE_INVOICE_HEAD" & _
             "  left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
             "  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" & _
             "  Left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code and TSPL_SD_SHIPMENT_DETAIL.Line_No=TSPL_SD_SALE_INVOICE_DETAIL.Line_No " & _
             "  left join TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " & _
             "  and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code " & _
             "  and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code " & _
             "  left join TSPL_BOOKING_MATSER on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No=TSPL_BOOKING_MATSER.Document_No " & _
             "  left join TSPL_Dispatch_Distributor on TSPL_BOOKING_MATSER.Document_No=TSPL_Dispatch_Distributor.Booking_No and TSPL_Dispatch_Distributor.Document_Type= 'CD_To_Dis' " & _
             "  left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location   =TSPL_LOCATION_MASTER.Location_Code " & _
             "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code " & _
             "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    " & _
             "  LEFT join (select TSPL_CUSTOMER_MASTER.Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan  from TSPL_SECONDARY_CUSTOMER_MASTER as TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
             "  left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE  " & _
             "  ) p_cust on p_cust.P_cust_code=TSPL_BOOKING_MATSER.Distributor_Code " & _
             "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " & _
             " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
             " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1" & _
             " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  " & _
             " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  " & _
             " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  " & _
             " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  " & _
             " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  " & _
             " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 " & _
             " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " & _
             " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " & _
             " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  " & _
             " left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " & _
             " left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM " & _
             " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
             " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
             " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " & _
             " left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " & _
             " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state " & _
             " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" & _
             " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state"
            ''and TSPL_SD_SHIPMENT_DETAIL.Line_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No
        Else


            Qry = " select TSPL_SD_SALE_INVOICE_HEAD.Remarks, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.jaali,TSPL_SD_SHIPMENT_HEAD.box, TSPL_SD_SALE_INVOICE_head.Invoice_Type AS GP_Invoice_Type, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ," & _
             " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,case when TSPL_SD_SALE_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date," & _
             " TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" & _
             " + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end" & _
             " + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end " & _
             " + Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " & _
             " + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " & _
             " as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty," & _
             " TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No, TSPL_SD_SHIPMENT_HEAD.Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,(case when len(coalesce(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No,''))>0 then  (TSPL_SD_SALE_INVOICE_HEAD.Document_Code + '/' + coalesce(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No,'')) else TSPL_SD_SALE_INVOICE_HEAD.Document_Code end) as InvoiceNo,TSPL_Dispatch_Distributor.Document_No as Distributer_Dispatch_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate " & _
             " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate," & _
             " TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition," & _
             " TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Loc_Short_Name , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ " & _
             " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone," & _
             " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo," & _
             " TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode," & _
             " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') " & _
             " as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3," & _
             " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, coalesce(p_cust.P_cust_code,TSPL_CUSTOMER_MASTER.Cust_Code) as P_CustCode, coalesce(p_cust .P_cust_name,TSPL_CUSTOMER_MASTER.Customer_Name) as P_Cust_Name," & _
             " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," & _
             " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," & _
             " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then TSPL_CUSTOMER_MASTER  .PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan, " & _
             " TSPL_SD_SALE_INVOICE_DETAIL.item_code  as item_code,((case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then '(TS)' else '' end)+TSPL_ITEM_MASTER.Item_Desc )as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty as qty" & _
             "  ,TSPL_SD_SALE_INVOICE_DETAIL.mrp, (TSPL_SD_SALE_INVOICE_DETAIL.amount+coalesce(tspl_item_master.CNF_Commission,0)*TSPL_SD_SALE_INVOICE_DETAIL.Qty) as amount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM" & _
             "  ,(TSPL_SD_SALE_INVOICE_DETAIL.item_cost+coalesce(tspl_item_master.CNF_Commission,0)) as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt," & _
             "  tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt,   tax3.Tax_Code_Desc as tax3name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt,   tax4.Tax_Code_Desc as tax4name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name," & _
             "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  ,isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount,coalesce(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No,'') AS Booking_No,(coalesce(tspl_item_master.CNF_Commission,0)*TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Distr_Commision " & _
             "  from TSPL_SD_SALE_INVOICE_HEAD" & _
             "  left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
             "  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" & _
             "  Left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code and TSPL_SD_SHIPMENT_DETAIL.Line_No=TSPL_SD_SALE_INVOICE_DETAIL.Line_No " & _
             "  left join TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " & _
             "  and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code " & _
             "  and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code " & _
             "  left join TSPL_BOOKING_MATSER on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No=TSPL_BOOKING_MATSER.Document_No " & _
             "  left join TSPL_Dispatch_Distributor on TSPL_BOOKING_MATSER.Document_No=TSPL_Dispatch_Distributor.Booking_No and TSPL_Dispatch_Distributor.Document_Type= 'CD_To_Dis' " & _
             "  left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location   =TSPL_LOCATION_MASTER.Location_Code " & _
             "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code " & _
             "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    " & _
             "  LEFT join (select TSPL_CUSTOMER_MASTER.Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan  from TSPL_SECONDARY_CUSTOMER_MASTER as TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
             "  left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE  " & _
             "  ) p_cust on p_cust.P_cust_code=TSPL_BOOKING_MATSER.Distributor_Code " & _
             "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " & _
             " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
             " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1" & _
             " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  " & _
             " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  " & _
             " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  " & _
             " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  " & _
             " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  " & _
             " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 " & _
             " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " & _
             " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " & _
             " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  " & _
             " left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " & _
             " left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM " & _
             " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
             " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
             " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " & _
             " left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " & _
             " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state " & _
             " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" & _
             " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state"
            ''and TSPL_SD_SHIPMENT_DETAIL.Line_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No
        End If
        Return Qry
    End Function

    Public Shared Function GetInvoiceType(ByVal CreateVatSeries As Integer) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Retail"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "Non Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Excise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Tax Exempted"
        dt.Rows.Add(dr)

        If CreateVatSeries = 1 Then
            dr = dt.NewRow()
            dr("Code") = "I"
            dr("Name") = "Interstate"
            dt.Rows.Add(dr)
        End If

        Return dt
    End Function

End Class

Public Class clsPSShipmentHeadDetail
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
    Public Security_Rate As Decimal = 0
    Public Security_Amt As Decimal = 0

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsPSShipmentHeadDetail), ByVal trans As SqlTransaction, ByVal DocDate As DateTime, Optional ByVal IsDairyModule As Boolean = False, Optional ByVal FreshAmbient As String = Nothing) As Boolean

        Dim TransType_Str As String = ""
        If IsDairyModule = True Then
            'TransType_Str = IIf(IsTaxable = 1, "PS", "FS")
            TransType_Str = FreshAmbient & "-SH"
        End If

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsPSShipmentHeadDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "CAN", obj.CAN)
                clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", obj.ItemwiseTaxCode)
                clsCommon.AddColumnsForChange(coll, "Crate", obj.Crate)
                clsCommon.AddColumnsForChange(coll, "GatePass_No", obj.GatePass_No, True)
                clsCommon.AddColumnsForChange(coll, "Alter_UnitQty", obj.Alter_UnitQty)
                clsCommon.AddColumnsForChange(coll, "Rate_UnitQty", obj.Rate_UnitQty)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Code", obj.Cash_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Type", obj.Cash_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Pers", obj.Cash_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Amount", obj.Cash_Scheme_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Amount", obj.Disc_Scheme_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Code", obj.Disc_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Pers", obj.Disc_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Type", obj.Disc_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", obj.Scheme_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", obj.Scheme_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.Scheme_Qty)
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code)

                clsCommon.AddColumnsForChange(coll, "VS_CashSchemeCode", obj.VS_CashSchemeCode)
                clsCommon.AddColumnsForChange(coll, "VS_Cash_Amt", obj.VS_Cash_Amt)
                clsCommon.AddColumnsForChange(coll, "VS_ltrInCrate", obj.VS_ltrInCrate)
                clsCommon.AddColumnsForChange(coll, "Total_Item_WeightMetric", obj.Total_Item_WeightMetric)
                clsCommon.AddColumnsForChange(coll, "Alternate_UOM", obj.Alternate_UOM, True)
                clsCommon.AddColumnsForChange(coll, "RATE_UOM", obj.RATE_UOM, True)
                clsCommon.AddColumnsForChange(coll, "Item_Group", obj.Item_Group)
                clsCommon.AddColumnsForChange(coll, "TAX_PAID", obj.TAX_PAID)
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
                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommon.AddColumnsForChange(coll, "Free_qty", obj.Free_Qty)

                clsCommon.AddColumnsForChange(coll, "Order_Code", obj.Order_Code, True)
                clsCommon.AddColumnsForChange(coll, "Delivery_Code_PS", obj.Delivery_Code_PS, True)
                clsCommon.AddColumnsForChange(coll, "Delivery_Code", obj.Delivery_Code, True)
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
                clsCommon.AddColumnsForChange(coll, "Security_Rate", obj.Security_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Security_Amt", obj.Security_Amt, True)


                'clsCommon.AddColumnsForChange(coll, "Unit_Cogs", clsItemLocationDetails.GetUnitCogs(obj.Item_Code, obj.Location, trans))
                If obj.Is_CustomerChanged = 1 Then

                    Dim qry = "update TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE  set Customer_Code= '" & obj.Customer_Code & "',Is_CustomerChanged=1  where Document_Code='" + obj.Delivery_Code_PS + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "update TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE  set Ship_Party= '" & obj.Customer_Code & "'  where Document_Code='" + obj.Delivery_Code_PS + "' and Ship_Party='" & obj.OrgCustCOde & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    Dim strSaleOrder As String = clsDBFuncationality.getSingleValue("select isnull(Against_Sales_Order,'') from  TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE where Document_Code='" & obj.Delivery_Code_PS & "'", trans)
                    qry = "update TSPL_SD_SALES_ORDER_HEAD  set Customer_Code= '" & obj.Customer_Code & "',Is_CustomerChanged=1  where Document_Code='" + strSaleOrder + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "update TSPL_SD_SALES_ORDER_DETAIL  set Ship_Party= '" & obj.Customer_Code & "' where Document_Code='" + strSaleOrder + "' and Ship_Party='" & obj.OrgCustCOde & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    Dim strBookingNo As String = clsDBFuncationality.getSingleValue("select isnull(Against_Booking_No,'') from  TSPL_SD_SALES_ORDER_HEAD where Document_Code='" & strSaleOrder & "'", trans)
                    qry = "update TSPL_BOOKING_MASTER_PRODUCTSALE  set Customer_Code= '" & obj.Customer_Code & "',Is_CustomerChanged=1  where Document_Code='" + strBookingNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                clsSerializeInvenotry.SaveData("SD-IN", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.arrSrItem, trans)

                ''richa 11 Nov,2020 mrp should be saved into batch table only when checkstockmrpwise setting is ON
                Dim checkstockmrpwise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.checkstockMRPwise, clsFixedParameterCode.checkstockMRPwise, trans)) = 0, False, True)
                If IsDairyModule = False Then
                    If checkstockmrpwise Then
                        clsBatchInventory.SaveData("PS-SH", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.MRP, obj.Unit_code, obj.arrBatchItem, trans)
                    Else
                        clsBatchInventory.SaveData("PS-SH", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)
                    End If
                Else
                    If checkstockmrpwise Then
                        clsBatchInventory.SaveData(TransType_Str, strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.MRP, obj.Unit_code, obj.arrBatchItem, trans)
                    Else
                        clsBatchInventory.SaveData(TransType_Str, strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)
                    End If
                End If
                If IsDairyModule Then
                    If clsCommon.myLen(obj.Delivery_Code) > 0 AndAlso obj.Is_CustomerChanged = 0 Then
                        If clsCommon.CompairString(obj.Scheme_Item, "No") = CompairStringResult.Equal Then
                            Dim dblPendingQty As Double = GetBalanceDOQty(obj.Delivery_Code, strDocNo, obj.Item_Code, obj.Unit_code, trans)
                            Dim dblEnteredQty As Double = clsCommon.myCdbl(obj.Qty)
                            If dblEnteredQty > dblPendingQty Then
                                Throw New Exception("Cannot Save the Entry " + "Because Entered Qty can't be more than Pending Qty " + " Delivery Order No : " + obj.Delivery_Code + "  For Item : " + obj.Item_Code + "  Entered Qty is : " + clsCommon.myCstr(dblEnteredQty) + "  Where Pending Qts is : " + clsCommon.myCstr(dblPendingQty))
                            End If
                        End If
                    End If
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceDOQty(ByVal strDOCode As String, ByVal strDispatchNo As String, ByVal strICode As String, ByVal strUnit As String, ByVal Trans As SqlTransaction) As Double
        Dim qry As String = "select sum (qty) from ( " & _
        "select Qty from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE where Document_No='" & strDOCode & "' and Item_Code='" & strICode & "' and Unit_code='" & strUnit & "' and Scheme_Item='N' " & _
        "union all " & _
        "select -1 * Qty from TSPL_SD_SHIPMENT_DETAIL where Delivery_Code='" & strDOCode & "' and Item_Code='" & strICode & "' and " & _
        "Unit_code='" & strUnit & "' and Scheme_Item='N'  and DOCUMENT_CODE not in ('" & strDispatchNo & "')  " & _
        ")final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
    End Function

End Class

Public Class clsPSShipmentChecklistDetail
#Region "Variables"
    Public Shipment_Code As String = Nothing
    Public Dispatch_Checklist_Code As String = Nothing
    Public CheckListStatus As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsPSShipmentChecklistDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then

            If strDocNo IsNot Nothing AndAlso clsCommon.myLen(strDocNo) > 0 Then
                Dim qry As String = " DELETE FROM TSPL_SD_SHIPMENT_CHECKLIST_DETAIL WHERE SHIPMENT_CODE = '" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If


            For Each obj As clsPSShipmentChecklistDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Shipment_Code", strDocNo, True)
                clsCommon.AddColumnsForChange(coll, "Dispatch_Checklist_Code", obj.Dispatch_Checklist_Code, True)
                clsCommon.AddColumnsForChange(coll, "STATUS", obj.CheckListStatus, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_CHECKLIST_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class

Public Class clsPSShipmentPrint
    Public Shared Function PrintDataBatchWiseInvoice(ByVal StrDocNo As String) As Boolean
        Try
            If clsCommon.myLen(StrDocNo) <= 0 Then
                clsCommon.MyMessageBoxShow("Invoice Not Found")
                Return False
            End If
            Dim qry As String = Nothing
            qry = "  Select * from ( " &
                  "  select '0' as ItemBatchType, case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Customer_Name else TSPL_SHIP_TO_LOCATION.Ship_To_Desc end  as shipName,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Add1 else TSPL_SHIP_TO_LOCATION.add1 end as ship_Add1,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Add2 else  TSPL_SHIP_TO_LOCATION.Add2 end as ship_add2 ,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Add3 else  TSPL_SHIP_TO_LOCATION.Add3 end  as ship_add3  , case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then tspl_customer_master.pin_no else TSPL_SHIP_TO_LOCATION.Pin_Code end as Ship_Pin_Code,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then CUSTOMER_STATE_MASTER.GST_STATE_Code else Ship_State.gst_state_code end as Ship_GST_State_Code,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then Tspl_customer_master.gstno else TSPL_SHIP_TO_LOCATION.gstNo  end as Ship_GSTNo,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.pan else  TSPL_SHIP_TO_LOCATION.PAN end as Ship_PANNo,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then isnull(TSPL_CUSTOMER_MASTER.FSSAI_NO,'') else '' end as Ship_FSSAI_No,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then ''  else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End else TSPL_SHIP_TO_LOCATION.telphone end as SHip_Phn, tspl_customer_master.terms_code,TSPL_TERMS_MASTER.terms_desc,tspl_customer_master.payment_code,tspl_payment_code.payment_desc,tspl_customer_master.pin_no,TSPL_SD_SHIPMENT_detail.amt_less_discount,TSPL_SD_SHIPMENT_HEAD.Total_Amt, TSPL_SD_SALE_INVOICE_HEAD.Description,customer_city_master.city_name as Cust_City,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No ,CUSTOMER_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode, TSPL_SD_SHIPMENT_HEAD.Electronic_Ref_No,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,tspl_location_master.gstno as LocGstNo,TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo,TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code  ,TSPL_SD_SALE_INVOICE_HEAD.document_code as Sale_Invoice_No, Case When ISNULL(TSPL_SD_SHIPMENT_Head.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_Head.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_Head.AlternateVehicle,'')<>'' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SHIPMENT_Head.vehicleNo End as vehicleNo, TSPL_SD_SHIPMENT_Head.Sale_Invoice_Date, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount, TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, TSPL_STATE_MASTER.State_Name as LocationState, case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as LOCPhone,TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO,  TSPL_SD_SHIPMENT_HEAD.Document_Code ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date , TSPL_SD_SHIPMENT_HEAD.Lorry_No,TSPL_ITEM_MASTER.Sku_Seq ,TSPL_SD_SHIPMENT_DETAIL.Item_Code ,TSPL_SD_SHIPMENT_DETAIL.Line_No ,TSPL_ITEM_MASTER.Item_Desc as Particulars, TSPL_SD_SHIPMENT_DETAIL.Crate as QtyCrates ,TSPL_SD_SHIPMENT_DETAIL.Unit_code  , " &
                  "  stuff((select ',' + isnull(TSPL_BATCH_ITEM.Batch_No   ,'')  FROM TSPL_BATCH_ITEM WHERE TSPL_SD_SHIPMENT_DETAIL.Document_Code   =TSPL_BATCH_ITEM.Document_Code AND TSPL_BATCH_ITEM.Parent_Line_No=TSPL_SD_SHIPMENT_DETAIL.Line_No AND TSPL_BATCH_ITEM.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code AND TSPL_BATCH_ITEM.UOM =TSPL_SD_SHIPMENT_DETAIL.Unit_code  for xml path ('')),1,1,'' )as Batch_No, " &
                  "  TSPL_SD_SHIPMENT_DETAIL.Qty  as Batch_Qty, coalesce(TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount,  '' GrandTotalCrates,tspl_company_master.Access_Officer as FSSAI , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , TSPL_COMPANY_MASTER.Tin_No as comp_tinNo , " &
                  "  TSPL_COMPANY_MASTER.Bank_Name, TSPL_COMPANY_MASTER.BankAccountNo, TSPL_COMPANY_MASTER.BankIFSCCode, TSPL_COMPANY_MASTER.BankBranchAddress,TSPL_Company_Master.Logo_Img as Logo_Img2GSTReg_NO,TSPL_Company_Master.GSTReg_NO as CompGSTReg_NO " &
                  "  ,TSPL_SD_SHIPMENT_HEAD. Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER. Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,isnull(TSPL_CUSTOMER_MASTER.pin_Code,'') as Cust_Pin,(select top 1 tspl_employee_master.emp_name from tspl_salesman_detail left join tspl_employee_master on tspl_employee_master.emp_code=tspl_salesman_detail.salesman_code where route_code=tspl_route_master.route_no) as  Sales_Name,isnull(TSPL_CUSTOMER_MASTER.FSSAI_NO,'') as Cust_FSSAI_LIC_NO ,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then ''  else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone ,TSPL_CUSTOMER_MASTER.Fax as cust_fax ,TSPL_CUSTOMER_MASTER.Email as  cust_Email,TSPL_CUSTOMER_MASTER.WebSite as cust_website,TSPL_CUSTOMER_MASTER.pan as Customer_Pan ,TSPL_SD_SHIPMENT_DETAIL.item_cost, TSPL_SD_SHIPMENT_DETAIL.amount , TSPL_SD_SHIPMENT_HEAD.Discount_amt,TSPL_SD_SHIPMENT_HEAD.Total_Tax_Amt,TSPL_SD_SHIPMENT_HEAD.CrateQty,TSPL_SD_SHIPMENT_HEAD.Crate, TSPL_SD_SHIPMENT_HEAD.Is_Taxable  from TSPL_SD_SHIPMENT_DETAIL LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE   LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SHIPMENT_HEAD.Comp_Code   left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SHIPMENT_HEAD .Customer_Code  left join tspl_payment_code on tspl_payment_code.payment_code=tspl_customer_master.payment_code  left join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.terms_code=tspl_customer_master.terms_code  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location  LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code=customer_city_master.City_Code  left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State= CUSTOMER_STATE_MASTER.STATE_CODE   left outer join TSPL_SD_SALE_INVOICE_HEAD  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No    LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  LEFT OUTER JOIN TSPL_VEHICLE_MASTER as Vehicle on Vehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.Vehicle_Code  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left outer join  TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SHIPMENT_HEAD .Ship_To_Location  left outer join TSPL_CITY_MASTER  as Ship_City on Ship_City.City_Code =TSPL_SHIP_TO_LOCATION.City_Code  left join tspl_state_master as Ship_State on Ship_State.state_code=TSPL_SHIP_TO_LOCATION.state  " &
                  "   where 2=2 and TSPL_SD_SHIPMENT_HEAD.Screen_Type ='DS' and TSPL_SD_SALE_INVOICE_HEAD.Document_Code in   ('" + StrDocNo + "')  " &
                  "  union all  " &
                  "  select '1' as ItemBatchType, case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Customer_Name else TSPL_SHIP_TO_LOCATION.Ship_To_Desc end  as shipName,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Add1 else TSPL_SHIP_TO_LOCATION.add1 end as ship_Add1,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Add2 else  TSPL_SHIP_TO_LOCATION.Add2 end as ship_add2 ,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Add3 else  TSPL_SHIP_TO_LOCATION.Add3 end  as ship_add3  , case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then tspl_customer_master.pin_no else TSPL_SHIP_TO_LOCATION.Pin_Code end as Ship_Pin_Code,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then CUSTOMER_STATE_MASTER.GST_STATE_Code else Ship_State.gst_state_code end as Ship_GST_State_Code,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then Tspl_customer_master.gstno else TSPL_SHIP_TO_LOCATION.gstNo  end as Ship_GSTNo,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.pan else  TSPL_SHIP_TO_LOCATION.PAN end as Ship_PANNo,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then isnull(TSPL_CUSTOMER_MASTER.FSSAI_NO,'') else '' end as Ship_FSSAI_No,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then ''  else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End else TSPL_SHIP_TO_LOCATION.telphone end as SHip_Phn, tspl_customer_master.terms_code,TSPL_TERMS_MASTER.terms_desc,tspl_customer_master.payment_code,tspl_payment_code.payment_desc,tspl_customer_master.pin_no,0 as amt_less_discount,TSPL_SD_SHIPMENT_HEAD.Total_Amt, TSPL_SD_SALE_INVOICE_HEAD.Description,customer_city_master.city_name as Cust_City,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No ,CUSTOMER_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode, TSPL_SD_SHIPMENT_HEAD.Electronic_Ref_No,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,tspl_location_master.gstno as LocGstNo,TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo,TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code  ,TSPL_SD_SALE_INVOICE_HEAD.document_code as Sale_Invoice_No, Case When ISNULL(TSPL_SD_SHIPMENT_Head.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_Head.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_Head.AlternateVehicle,'')<>'' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SHIPMENT_Head.vehicleNo End as vehicleNo, TSPL_SD_SHIPMENT_Head.Sale_Invoice_Date, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount, TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, TSPL_STATE_MASTER.State_Name as LocationState, case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as LOCPhone,TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO,  TSPL_SD_SHIPMENT_HEAD.Document_Code ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date , TSPL_SD_SHIPMENT_HEAD.Lorry_No,TSPL_ITEM_MASTER.Sku_Seq ,TSPL_BATCH_ITEM.Item_Code ,TSPL_BATCH_ITEM.Line_No ,TSPL_ITEM_MASTER.Item_Desc as Particulars, TSPL_SD_SHIPMENT_DETAIL.Crate as QtyCrates ,TSPL_BATCH_ITEM.UOM as Unit_code  , " &
                  "  TSPL_BATCH_ITEM.Batch_No as Batch_No, " &
                  "  TSPL_BATCH_ITEM.Qty  as Batch_Qty, 0 as Cash_Scheme_Amount,  '' GrandTotalCrates,tspl_company_master.Access_Officer as FSSAI , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , TSPL_COMPANY_MASTER.Tin_No as comp_tinNo , " &
                  "  TSPL_COMPANY_MASTER.Bank_Name, TSPL_COMPANY_MASTER.BankAccountNo, TSPL_COMPANY_MASTER.BankIFSCCode, TSPL_COMPANY_MASTER.BankBranchAddress,TSPL_Company_Master.Logo_Img as Logo_Img2GSTReg_NO,TSPL_Company_Master.GSTReg_NO as CompGSTReg_NO  " &
                  " ,TSPL_SD_SHIPMENT_HEAD. Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER. Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,isnull(TSPL_CUSTOMER_MASTER.pin_Code,'') as Cust_Pin,(select top 1 tspl_employee_master.emp_name from tspl_salesman_detail left join tspl_employee_master on tspl_employee_master.emp_code=tspl_salesman_detail.salesman_code where route_code=tspl_route_master.route_no) as  Sales_Name,isnull(TSPL_CUSTOMER_MASTER.FSSAI_NO,'') as Cust_FSSAI_LIC_NO ,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then ''  else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone ,TSPL_CUSTOMER_MASTER.Fax as cust_fax ,TSPL_CUSTOMER_MASTER.Email as  cust_Email,TSPL_CUSTOMER_MASTER.WebSite as cust_website,TSPL_CUSTOMER_MASTER.pan as Customer_Pan,TSPL_SD_SHIPMENT_DETAIL.item_cost, TSPL_SD_SHIPMENT_DETAIL.amount , TSPL_SD_SHIPMENT_HEAD.Discount_amt,TSPL_SD_SHIPMENT_HEAD.Total_Tax_Amt,TSPL_SD_SHIPMENT_HEAD.CrateQty,TSPL_SD_SHIPMENT_HEAD.Crate , TSPL_SD_SHIPMENT_HEAD.Is_Taxable  from  TSPL_BATCH_ITEM " &
                  "  Left Outer Join  TSPL_SD_SHIPMENT_DETAIL on  TSPL_SD_SHIPMENT_DETAIL.Document_Code   =TSPL_BATCH_ITEM.Document_Code AND TSPL_BATCH_ITEM.Parent_Line_No=TSPL_SD_SHIPMENT_DETAIL.Line_No AND TSPL_BATCH_ITEM.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code AND TSPL_BATCH_ITEM.UOM =TSPL_SD_SHIPMENT_DETAIL.Unit_code " &
                  "  LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE   " &
                  "  LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_BATCH_ITEM.Item_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SHIPMENT_HEAD.Comp_Code   left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SHIPMENT_HEAD .Customer_Code  left join tspl_payment_code on tspl_payment_code.payment_code=tspl_customer_master.payment_code  left join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.terms_code=tspl_customer_master.terms_code  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location  LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code=customer_city_master.City_Code  left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State= CUSTOMER_STATE_MASTER.STATE_CODE   left outer join TSPL_SD_SALE_INVOICE_HEAD  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No    LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  LEFT OUTER JOIN TSPL_VEHICLE_MASTER as Vehicle on Vehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.Vehicle_Code  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left outer join  TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SHIPMENT_HEAD .Ship_To_Location  left outer join TSPL_CITY_MASTER  as Ship_City on Ship_City.City_Code =TSPL_SHIP_TO_LOCATION.City_Code  left join tspl_state_master as Ship_State on Ship_State.state_code=TSPL_SHIP_TO_LOCATION.state  " &
                  "  where 2=2 and TSPL_SD_SHIPMENT_HEAD.Screen_Type ='DS' and TSPL_SD_SALE_INVOICE_HEAD.Document_Code in   ('" + StrDocNo + "')  " &
                  "  ) XXXX  Order By XXXX.Sale_Invoice_No, XXXX.Item_Code , XXXX.ItemBatchType  "


            Dim qryUomTotal As String = "   select (final.InvoiceNo) as InvoiceNo, final.HSN_Code, sum (Final.Amount- Final.Disc_Amt) as  Amount, max(TAX1_Rate) as TAX1_Rate, sum (Final.TAX1_Amt) as TAX1_Amt ,max( Final.tax1name) as tax1name , max (TAX2_Rate ) as  TAX2_Rate,sum( TAX2_Amt) as TAX2_Amt  , max( tax2name) as tax2name   ,  max (TAX3_Rate ) as  TAX3_Rate,sum( TAX3_Amt) as TAX3_Amt  , max( isnull(tax3name,'')) as tax3name   ,  max (TAX4_Rate ) as  TAX4_Rate,sum( TAX4_Amt) as TAX4_Amt  , max( isnull( tax4name,'')) as tax4name   ,  max (TAX5_Rate ) as  TAX5_Rate,sum( TAX5_Amt) as TAX5_Amt  , max( isnull(tax5name,'')) as tax5name   ,  max (TAX6_Rate ) as  TAX6_Rate,sum( TAX6_Amt) as TAX6_Amt  , max( isnull(tax6name,'')) as tax6name   ,  max (TAX7_Rate ) as  TAX7_Rate,sum( TAX7_Amt) as TAX7_Amt  , max( isnull(tax7name,'')) as tax7name   ,  max (TAX8_Rate ) as  TAX8_Rate,sum( TAX8_Amt) as TAX8_Amt  , max( isnull(tax8name,'')) as tax8name   ,  max (TAX9_Rate ) as  TAX9_Rate,sum( TAX9_Amt) as TAX9_Amt  , max( isnull(tax9name,'')) as tax9name   ,  max (TAX10_Rate ) as  TAX10_Rate,sum( TAX10_Amt) as TAX10_Amt  , max( isnull(tax10name,'')) as tax10name   from (select TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE  as InvoiceNo, TSPL_ITEM_MASTER.HSN_Code ,TSPL_SD_SALE_INVOICE_DETAIL.amount, isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt,   TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate ,   TSPL_SD_SALE_INVOICE_DETAIL.TAX2 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate ,   TSPL_SD_SALE_INVOICE_DETAIL.TAX3 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate,   TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate,   TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate,   TSPL_SD_SALE_INVOICE_DETAIL.TAX6 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate,   TSPL_SD_SALE_INVOICE_DETAIL.TAX7 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate,   TSPL_SD_SALE_INVOICE_DETAIL.TAX8 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate,   TSPL_SD_SALE_INVOICE_DETAIL.TAX9 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate,   TSPL_SD_SALE_INVOICE_DETAIL.TAX10 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate,   tax1.Type as tax1name,tax2.Type as tax2name,tax3.Type as tax3name   ,tax4.Type as tax4name ,tax5.Type as tax5name, tax6.Type as tax6name, tax7.Type as tax7name, tax8.Type as tax8name, tax9.Type as tax9name, tax10.Type as tax10name   from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2  left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE        where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + StrDocNo + "') and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item ='N' ) final group by final.InvoiceNo, final.HSN_Code   "
            Dim dtUOM As DataTable = clsDBFuncationality.GetDataTable(qryUomTotal)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()


                qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & StrDocNo & "')"
                Dim dt10 As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim CustomerCode As String = ""
                If (dt10 IsNot Nothing AndAlso dt10.Rows.Count > 0) Then
                    For Each dr As DataRow In dt10.Rows
                        CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                    Next
                    If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                        CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                    End If
                End If
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptDispacthBatchWiseReportByInvoice", "Invoice", clsCommon.myCDate(dt.Rows(0)("Sale_Invoice_Date")), "rptCompanyAddress.rpt", "rptCustomerOutstanding.rpt", Nothing, "rptHashCodeSummary.rpt", dtUOM)
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No data found to print")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsPSShipmentDemand
    Public DOCUMENT_CODE As String
    Public Booking_TR_Code As String
    Public Qty As Decimal

    Friend Shared Sub SaveData(strDocNo As String, arrDemand As List(Of clsPSShipmentDemand), trans As SqlTransaction)
        If (arrDemand IsNot Nothing AndAlso arrDemand.Count > 0) Then
            For Each obj As clsPSShipmentDemand In arrDemand
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Booking_TR_Code", obj.Booking_TR_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
    End Sub
End Class
