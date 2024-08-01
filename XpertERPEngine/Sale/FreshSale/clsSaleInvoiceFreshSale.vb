Imports common
Imports System.Data.SqlClient
Public Class clsSaleInvoiceFreshSale
#Region "Variables"
    Public Sub_Location_code As String = String.Empty
    Public LeakageAmount As Decimal = 0
    Public isCardSale As Integer = 0
    Public Crate As Double = 0
    Public Sale_Invoice_Date As Date?
    Public Dispatch_date As Date?
    Public Screen_Type As String = Nothing
    Public OPKm As Double = 0
    Public CLKm As Double = 0
    Public TotalCAN As Double = 0
    Public ShippedCAN As Double = 0
    Public EWayBillDate As Date?
    Public EWayBillNo As String = Nothing
    Public Electronic_Ref_No As String = Nothing
    Public AlternateVehicle As String = Nothing
    Public ManualVehicle As String = Nothing
    Public podate As DateTime
    Public Form_38_No As String = Nothing
    Public Cust_PO_No As String = Nothing
    Public Price_Group_Code As String = Nothing
    Public PROJECT_ID As String = Nothing
    Public Invoice_Type As String = Nothing
    Public Mannual_Document_Code As Integer
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
    Public Trans_Type As String = Nothing
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
    Public Against_Shipment_No As String = Nothing
    Public Is_Internal As Boolean = False
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Is_Create_Auto_Receipt As Boolean = False
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public RoundOffAmount As Decimal
    Public Is_Taxable As Integer = 0
    Public Arr As List(Of clsSaleInvoiceDetailFreshSale) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public Against_C_Form As Boolean = False
    Public Price_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public HeadDisc_Per As Double = 0
    Public HeadDisc_Amt As Double = 0
    Public HeadDisc_PerAmt As Double = 0
    Public TotCashDiscAmt As Double = 0

    Public InvoiceManualNowithPrefix As String = Nothing
    '''' added column for dispatch 
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
    Public CrateQtyIn As Double = 0
    Public Salesman_CodeIn As String = Nothing
    Public Salesman_NameIn As String = Nothing
    Public VehicleCodeIn As String = Nothing

    ''''  end
    Public Manual_Driver_Name As String = Nothing
    Public Manual_Salesman_Name As String = Nothing

    Public Sampling As Integer = 0
#End Region


    Public Function SaveData(ByVal obj As clsSaleInvoiceFreshSale, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsSaleInvoiceFreshSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try
            checkSaveNotification(obj.Document_Date, obj.Customer_Code, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", trans)
            End If

            Dim qry As String = "delete from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                'If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location)
                'Else
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SNQuotationOther, obj.Bill_To_Location)
                'End If

                '-----------------richa 26/06/2014 Ticket No .BM00000002982--------------------------------
                Dim isIncrementCounter As Boolean = True
                If obj.Mannual_Document_Code > 0 OrElse clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                    isIncrementCounter = False
                End If

                '---------------------------------------------------------------------------------------

                'Sanjay BHA/21/06/18-000064
                Dim GSTStatus As Boolean = clsERPFuncationality.GetGSTStatus(obj.Document_Date)
                If obj.Screen_Type = "DS" AndAlso GSTStatus = True Then
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonDairyDispatchforFreshAmbient, clsFixedParameterCode.CreateCommonDairyDispatchforFreshAmbient, trans)) = 1 Then ''By Balwinder on 10/12/2019
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonSeriesLocationwiseForAllSale, clsFixedParameterCode.CreateCommonSeriesLocationwiseForAllSale, trans)) = 0 Then
                            Dim strItemCategory As String = String.Empty
                            Dim StrCustomerState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(state,'') AS STATE from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(obj.Customer_Code) & "'", trans))
                            Dim StrLocationState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(State,'') AS STATE from TSPL_LOCATION_MASTER WHERE LOCATION_CODE='" & clsCommon.myCstr(obj.Bill_To_Location) & "'", trans))
                            If clsCommon.CompairString(StrCustomerState, StrLocationState) = CompairStringResult.Equal Then
                                strItemCategory = "L"
                            Else
                                strItemCategory = "I"
                            End If
                            If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(strItemCategory, "L") = CompairStringResult.Equal Then
                                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTLocal, obj.Bill_To_Location, False, isIncrementCounter)
                                Else
                                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                                End If
                            Else
                                If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                                    If clsCommon.CompairString(strItemCategory, "L") = CompairStringResult.Equal Then
                                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTLocal, obj.Bill_To_Location, False, isIncrementCounter)
                                    Else
                                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                                    End If
                                Else
                                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTNonTaxable, obj.Bill_To_Location, False, isIncrementCounter)
                                End If
                            End If
                                ' For Common Sale Series
                        Else
                            Dim intExempted As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" & obj.Tax_Group & "'", trans))

                            If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                                If intExempted = 1 Then
                                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CommonSaleSeries, clsDocTransactionType.GSTBillofSupply, obj.Bill_To_Location, False, isIncrementCounter)
                                Else
                                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CommonSaleSeries, clsDocTransactionType.GSTTaxable, obj.Bill_To_Location, False, isIncrementCounter)
                                End If
                            Else
                                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CommonSaleSeries, clsDocTransactionType.GSTBillofSupply, obj.Bill_To_Location, False, isIncrementCounter)
                            End If
                        End If
                    Else
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonSeriesLocationwiseForAllSale, clsFixedParameterCode.CreateCommonSeriesLocationwiseForAllSale, trans)) = 0 Then
                            obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTNonTaxable, obj.Bill_To_Location, False, isIncrementCounter)
                        End If
                    End If
                Else
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonSeriesLocationwiseForAllSale, clsFixedParameterCode.CreateCommonSeriesLocationwiseForAllSale, trans)) = 0 Then
                        If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                            obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmInvoiceFreshSale, "", obj.Bill_To_Location, False, isIncrementCounter)
                        ElseIf clsCommon.CompairString(obj.Invoice_Type, "R") = CompairStringResult.Equal Then
                            obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmInvoiceFreshSale, "", obj.Bill_To_Location, False, isIncrementCounter)
                        ElseIf clsCommon.CompairString(obj.Invoice_Type, "N") = CompairStringResult.Equal Then
                            obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmInvoiceFreshSale, "", obj.Bill_To_Location, False, isIncrementCounter)
                        ElseIf clsCommon.CompairString(obj.Invoice_Type, "S") = CompairStringResult.Equal Then
                            obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmInvoiceFreshSale, "", obj.Bill_To_Location, False, isIncrementCounter)
                        End If
                    Else
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CommonSaleSeries, clsDocTransactionType.GSTBillofSupply, obj.Bill_To_Location, False, isIncrementCounter)
                    End If
                End If

                'Sanjay BHA/21/06/18-000064


                If obj.Mannual_Document_Code > 0 Then
                    Dim strDocLike As String = ""
                    Dim arr As Array = obj.Document_Code.ToCharArray()
                    For jj As Integer = 0 To arr.Length - clsCommon.myLen(obj.Mannual_Document_Code) - 1
                        strDocLike += clsCommon.myCstr(arr(jj))
                    Next
                    strDocLike += clsCommon.myCstr(obj.Mannual_Document_Code)
                    obj.Document_Code = strDocLike
                    '-----------------richa 26/06/2014 Ticket No .BM00000002982--------------------------------
                ElseIf clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                    obj.Document_Code = obj.InvoiceManualNowithPrefix
                    '----------------------------------------------------------------------------------------
                End If

            End If
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFreshSale, clsUserMgtCode.frmInvoiceFreshSale, obj.Bill_To_Location, obj.Document_Date, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))


            If IsDBNull(obj.podate) = True Then
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.podate, "dd/MMM/yyyy hh:mm tt"))
            End If

            clsCommon.AddColumnsForChange(coll, "Booking_No", obj.Booking_No, True)
            If obj.Booking_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Booking_Date", clsCommon.GetPrintDate(obj.Booking_Date, "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "DeliveryStatus", obj.DeliveryStatus)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
            clsCommon.AddColumnsForChange(coll, "Lorry_No", obj.Lorry_No)
            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "Transporter_Name", obj.Transporter_Name)
            clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
            clsCommon.AddColumnsForChange(coll, "Freight_Amount", obj.Freight_Amount)
            clsCommon.AddColumnsForChange(coll, "CrateQty", obj.CrateQty)
            If clsCommon.CompairString(obj.Screen_Type, "DS") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Crate", obj.Crate)
            End If
            clsCommon.AddColumnsForChange(coll, "LeakageAmount", obj.LeakageAmount)
            ''added by richa on 2 Apr,2019 TEC/13/09/19-001010
            clsCommon.AddColumnsForChange(coll, "OPKm", obj.OPKm)
            clsCommon.AddColumnsForChange(coll, "issampling", obj.Sampling)
            clsCommon.AddColumnsForChange(coll, "CLKm", obj.CLKm)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", obj.Is_Taxable)
            clsCommon.AddColumnsForChange(coll, "isCardSale", obj.isCardSale)
            If obj.Dispatch_date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Dispatch_date", clsCommon.GetPrintDate(obj.Dispatch_date, "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "TotalCAN", obj.TotalCAN)
            clsCommon.AddColumnsForChange(coll, "ShippedCAN", obj.ShippedCAN)
            clsCommon.AddColumnsForChange(coll, "Screen_Type", obj.Screen_Type)
            'sanjay BHA/21/06/18-000064
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "CrateQtyIn", obj.CrateQtyIn)
            clsCommon.AddColumnsForChange(coll, "Salesman_CodeIn", obj.Salesman_CodeIn, True)
            clsCommon.AddColumnsForChange(coll, "Salesman_NameIn", obj.Salesman_NameIn)
            clsCommon.AddColumnsForChange(coll, "VehicleCodeIn", obj.VehicleCodeIn)

            clsCommon.AddColumnsForChange(coll, "Manual_Driver_Name", obj.Manual_Driver_Name)
            clsCommon.AddColumnsForChange(coll, "Manual_Salesman_Name", obj.Manual_Salesman_Name)


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
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code, True)
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
            clsCommon.AddColumnsForChange(coll, "Against_Shipment_No", obj.Against_Shipment_No, True)
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
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Inv_Date", clsCommon.GetPrintDate(obj.Inv_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Receipt", IIf(obj.Is_Create_Auto_Receipt, 1, 0))

            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code, True)
            clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
            clsCommon.AddColumnsForChange(coll, "Against_C_Form", IIf(obj.Against_C_Form, 1, 0))

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion

            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Route_Desc", obj.Route_Desc)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Per", obj.HeadDisc_Per)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Amt", obj.HeadDisc_Amt)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_PerAmt", obj.HeadDisc_PerAmt)
            clsCommon.AddColumnsForChange(coll, "TotCashDiscAmt", obj.TotCashDiscAmt)
            clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Invoice_Type)
            clsCommon.AddColumnsForChange(coll, "Price_Group_Code", obj.Price_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_PO_No", obj.Cust_PO_No)
            clsCommon.AddColumnsForChange(coll, "Form_38_No", obj.Form_38_No)


            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsSaleInvoiceDetailFreshSale.SaveData(obj.Document_Code, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            '''' to save item weight unit
            qry = "update TSPL_SD_SALE_invoice_DETAIL set Weight_UOM= (select Weight_UOM from TSPL_ITEM_MASTER where Item_Code=TSPL_SD_SALE_invoice_DETAIL.Item_Code)  where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '''' 
            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_SD_SALE_INVOICE_HEAD", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function UpdateAfterPosting(ByVal obj As clsSaleInvoiceFreshSale, ByVal ShipmentNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.Document_Code) > 0 Then
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + obj.Document_Code + "'", trans)

                Dim coll2 As New Hashtable()
                Dim objShipment As New clsDispatchNoteFreshSale
                objShipment.Document_Code = ShipmentNo
                'objShipment.EWayBillNo = obj.EWayBillNo
                'objShipment.EWayBillDate = obj.EWayBillDate
                clsCommon.AddColumnsForChange(coll2, "EWayBillNo", obj.EWayBillNo)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll2, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll2, "EWayBillDate", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll2, "Electronic_Ref_No", obj.Electronic_Ref_No)
                clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_SD_SHIPMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SHIPMENT_HEAD.Document_Code='" + objShipment.Document_Code + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function checkSaveNotification(ByVal strDate As String, ByVal strCustCode As String, ByVal trans As SqlTransaction)
        Try
            Dim CreditLimit As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Credit_Limit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + strCustCode + "'", trans))
            Dim qry As String
            Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNSalesOrder, trans)
            For Each dr As DataRow In dt.Rows
                'Criteria, Notification, Validation
                If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Or clsCommon.CompairString(dr("Criteria"), "Credit Days & Amount") = CompairStringResult.Equal Then
                    qry = "Select SUM(Balance_Amt) From(" & _
            " Select TSPL_Customer_Invoice_Head.Document_No, TSPL_Customer_Invoice_Head.Balance_Amt from TSPL_SD_SALE_INVOICE_HEAD" & _
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
            " WHERE ISNULL(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' AND TSPL_Customer_Invoice_Head.Status=1 AND TSPL_Customer_Invoice_Head.Customer_Code='" + strCustCode + "' AND TSPL_SD_SALE_INVOICE_HEAD.Due_Date<'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'" & _
        " ) XXX"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                        If clsCommon.CompairString(dr("Validation"), "Warning") = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                End If
                If clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Or clsCommon.CompairString(dr("Criteria"), "Credit Days & Amount") = CompairStringResult.Equal Then
                    qry = "Select SUM(Balance_Amt) From(" & _
        " Select TSPL_Customer_Invoice_Head.Document_No, Against_Sale_No, TSPL_Customer_Invoice_Head.Balance_Amt, TSPL_Customer_Invoice_Head.Customer_Code" & _
        " from TSPL_Customer_Invoice_Head WHERE ISNULL(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' AND TSPL_Customer_Invoice_Head.Status=1" & _
        " AND TSPL_Customer_Invoice_Head.Customer_Code='" + strCustCode + "'" & _
        " ) XXX"
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
            Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNSaleInvoice, trans)
            For Each dr As DataRow In dt.Rows
                'Criteria, Notification, Validation
                If clsCommon.CompairString(dr("Validation"), "Stop Approval") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Or clsCommon.CompairString(dr("Criteria"), "Credit Days & Amount") = CompairStringResult.Equal Then
                        qry = "Select SUM(Balance_Amt) From(" & _
                " Select TSPL_Customer_Invoice_Head.Document_No, TSPL_Customer_Invoice_Head.Balance_Amt from TSPL_SD_SALE_INVOICE_HEAD" & _
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
                " WHERE ISNULL(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' AND TSPL_Customer_Invoice_Head.Status=1 AND TSPL_Customer_Invoice_Head.Customer_Code='" + strCustCode + "' AND TSPL_SD_SALE_INVOICE_HEAD.Due_Date<'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'" & _
            " ) XXX"
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                    If clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Or clsCommon.CompairString(dr("Criteria"), "Credit Days & Amount") = CompairStringResult.Equal Then
                        qry = "Select SUM(Balance_Amt) From(" & _
            " Select TSPL_Customer_Invoice_Head.Document_No, Against_Sale_No, TSPL_Customer_Invoice_Head.Balance_Amt, TSPL_Customer_Invoice_Head.Customer_Code" & _
            " from TSPL_Customer_Invoice_Head WHERE ISNULL(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' AND TSPL_Customer_Invoice_Head.Status=1" & _
            " AND TSPL_Customer_Invoice_Head.Customer_Code='" + strCustCode + "'" & _
            " ) XXX"
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > CreditLimit Then
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

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal strInvoiceType As String, ByVal NavType As NavigatorType) As clsSaleInvoiceFreshSale
        Return GetData(strDocumentNo, NavType, strInvoiceType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal strInvoiceType As String, ByVal trans As SqlTransaction) As clsSaleInvoiceFreshSale
        Dim obj As clsSaleInvoiceFreshSale = Nothing
        Dim qry As String = "SELECT  TSPL_SD_SALE_INVOICE_HEAD.isCardSale,TSPL_SD_SALE_INVOICE_HEAD.Sub_Location_code,TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable,TSPL_SD_SALE_INVOICE_HEAD.issampling,TSPL_SD_SALE_INVOICE_HEAD.Manual_Driver_Name,TSPL_SD_SALE_INVOICE_HEAD.Manual_Salesman_Name,TSPL_SD_SALE_INVOICE_HEAD.LeakageAmount,TSPL_SD_SALE_INVOICE_HEAD.OPKm,TSPL_SD_SALE_INVOICE_HEAD.CLKm,TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo,TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No,TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate, TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_SD_SALE_INVOICE_HEAD.cust_po_date,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No,TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code,TSPL_SD_SALE_INVOICE_HEAD.price_group_code,TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Per,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt,TSPL_SD_SALE_INVOICE_HEAD.TotCashDiscAmt,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Route_Desc,TSPL_SD_SALE_INVOICE_HEAD.Price_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Status,TSPL_SD_SALE_INVOICE_HEAD.On_Hold,TSPL_SD_SALE_INVOICE_HEAD.Ref_No,TSPL_SD_SALE_INVOICE_HEAD.Description,TSPL_SD_SALE_INVOICE_HEAD.Remarks,TSPL_SD_SALE_INVOICE_HEAD.Tax_Group,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location,TSPL_SD_SALE_INVOICE_HEAD.TAX1,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX2,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX3,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX4,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX5,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX6,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX7,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX8,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX9,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX10,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Base_Amt,TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,TSPL_SD_SALE_INVOICE_HEAD.Total_Tax_Amt,TSPL_SD_SALE_INVOICE_HEAD.Comments,TSPL_SD_SALE_INVOICE_HEAD.Comp_Code,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code,TSPL_SD_SALE_INVOICE_HEAD.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,TSPL_SD_SALE_INVOICE_HEAD.Carrier,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_HEAD.GRNo,TSPL_SD_SALE_INVOICE_HEAD.GENo,TSPL_SD_SALE_INVOICE_HEAD.GEDate, TSPL_SD_SALE_INVOICE_HEAD.Dept,TSPL_SD_SALE_INVOICE_HEAD.Dept_Desc,TSPL_SD_SALE_INVOICE_HEAD.Item_Type,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code9 ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9 ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code10 ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,TSPL_SD_SALE_INVOICE_HEAD.Total_Add_Charge,TSPL_SD_SALE_INVOICE_HEAD.Tax_Calculation_Type,TSPL_SD_SALE_INVOICE_HEAD.Challan_No, TSPL_SD_SALE_INVOICE_HEAD.Challan_Date, TSPL_SD_SALE_INVOICE_HEAD.Inv_Date,TSPL_SD_SALE_INVOICE_HEAD.Inv_No,TSPL_SD_SALE_INVOICE_HEAD.Is_Internal ,TSPL_SD_SALE_INVOICE_HEAD.Is_Create_Auto_Receipt ,TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code ,TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name, "
        qry += " TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE,TSPL_SD_SALE_INVOICE_HEAD.CONVRATE,TSPL_SD_SALE_INVOICE_HEAD.APPLICABLEFROM,Against_C_Form,TSPL_SD_SALE_INVOICE_HEAD.PROJECT_ID, TSPL_SD_SALE_INVOICE_HEAD.Form_38_No "
        qry += " ,TSPL_SD_SALE_INVOICE_HEAD.Booking_No,TSPL_SD_SALE_INVOICE_HEAD.Booking_Date,TSPL_SD_SALE_INVOICE_HEAD.DeliveryStatus,TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Capacity,TSPL_SD_SALE_INVOICE_HEAD.Lorry_No,TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,TSPL_SD_SALE_INVOICE_HEAD.Transporter_Name,TSPL_SD_SALE_INVOICE_HEAD.Freight,TSPL_SD_SALE_INVOICE_HEAD.Freight_Amount,TSPL_SD_SALE_INVOICE_HEAD.CrateQty, isnull(TSPL_SD_SALE_INVOICE_HEAD.Screen_Type,'') as Screen_Type "
        qry += " ,isnull(TSPL_SD_SALE_INVOICE_HEAD.TotalCAN,0) as TotalCAN,isnull(TSPL_SD_SALE_INVOICE_HEAD.ShippedCAN,0) as ShippedCAN,TSPL_SD_SALE_INVOICE_HEAD.Dispatch_date"
        qry += " FROM TSPL_SD_SALE_INVOICE_HEAD"
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location "
        qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location "
        qry += " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_SD_SALE_INVOICE_HEAD.Tax_Group "
        qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_SD_SALE_INVOICE_HEAD.Terms_Code "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='FS' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD WHERE Invoice_Type in (" & strInvoiceType & ")  " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD WHERE Invoice_Type in (" & strInvoiceType & ") " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where Document_Code>'" + strPONo + "' and Invoice_Type in (" & strInvoiceType & ") " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where Document_Code<'" + strPONo + "' and Invoice_Type in (" & strInvoiceType & ") " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSaleInvoiceFreshSale()
            If IsDBNull(dt.Rows(0)("cust_po_date")) = True Then
                obj.podate = Nothing
            Else
                obj.podate = clsCommon.GetPrintDate(dt.Rows(0)("cust_po_date"), "dd/MMM/yyyy hh:mm tt")
            End If
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            obj.LeakageAmount = clsCommon.myCdbl(dt.Rows(0)("LeakageAmount"))
            obj.Sampling = clsCommon.myCdbl(dt.Rows(0)("issampling"))
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            obj.Electronic_Ref_No = clsCommon.myCstr(dt.Rows(0)("Electronic_Ref_No"))
            obj.Booking_No = clsCommon.myCstr(dt.Rows(0)("Booking_No"))
            If dt.Rows(0)("Booking_Date") IsNot DBNull.Value Then
                obj.Booking_Date = clsCommon.myCDate(dt.Rows(0)("Booking_Date"))
            End If

            obj.Manual_Driver_Name = clsCommon.myCstr(dt.Rows(0)("Manual_Driver_Name"))
            obj.Manual_Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Manual_Salesman_Name"))

            obj.DeliveryStatus = clsCommon.myCstr(dt.Rows(0)("DeliveryStatus"))
            obj.Vehicle_Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Lorry_No = clsCommon.myCstr(dt.Rows(0)("Lorry_No"))
            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Freight = clsCommon.myCstr(dt.Rows(0)("Freight"))
            obj.Freight_Amount = clsCommon.myCdbl(dt.Rows(0)("Freight_Amount"))
            obj.CrateQty = clsCommon.myCdbl(dt.Rows(0)("CrateQty"))

            obj.isCardSale = clsCommon.myCdbl(dt.Rows(0)("isCardSale"))
            If dt.Rows(0)("Dispatch_date") IsNot DBNull.Value Then
                obj.Dispatch_date = clsCommon.myCDate(dt.Rows(0)("Dispatch_date"))
            End If
            obj.TotalCAN = clsCommon.myCdbl(dt.Rows(0)("TotalCAN"))
            obj.ShippedCAN = clsCommon.myCdbl(dt.Rows(0)("ShippedCAN"))
            obj.Screen_Type = clsCommon.myCstr(dt.Rows(0)("Screen_Type"))
            obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_code"))
            'sanjay BHA/21/06/18-000064
            obj.OPKm = clsCommon.myCdbl(dt.Rows(0)("OPKm"))
            obj.CLKm = clsCommon.myCdbl(dt.Rows(0)("CLKm"))
            obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
            obj.Form_38_No = clsCommon.myCstr(dt.Rows(0)("Form_38_No"))
            obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
            obj.Price_Group_Code = clsCommon.myCstr(dt.Rows(0)("Price_Group_Code"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
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
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))

            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            obj.Against_C_Form = IIf(clsCommon.myCdbl(dt.Rows(0)("Against_C_Form")) = 1, True, False)

            obj.Challan_No = clsCommon.myCdbl(dt.Rows(0)("Challan_No"))
            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("Carrier"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))
            If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
            End If


            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Invoice_Type = clsCommon.myCstr(dt.Rows(0)("Invoice_Type"))

            obj.Against_Shipment_No = clsCommon.myCstr(dt.Rows(0)("Against_Shipment_No"))


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
            If clsCommon.myLen((dt.Rows(0)("Challan_Date"))) <= 0 Then
                obj.Challan_Date = ""
            Else
                obj.Challan_Date = clsCommon.GetPrintDate((dt.Rows(0)("Challan_Date")), "dd/MMM/yyyy")
            End If

            If clsCommon.myLen((dt.Rows(0)("Inv_Date"))) <= 0 Then
                obj.Inv_Date = ""
            Else
                obj.Inv_Date = clsCommon.GetPrintDate((dt.Rows(0)("Inv_Date")), "dd/MMM/yyyy")
            End If

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Is_Create_Auto_Receipt = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_Receipt")) = 1, True, False)

            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Salesman_Name"))
            obj.HeadDisc_Per = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Per"))
            obj.HeadDisc_Amt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Amt"))
            obj.TotCashDiscAmt = clsCommon.myCdbl(dt.Rows(0)("TotCashDiscAmt"))
            obj.HeadDisc_PerAmt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_PerAmt"))

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 
            qry = "Select AlternateVehicle,ManualVehicle from TSPL_SD_SHIPMENT_HEAD where Document_Code='" & obj.Against_Shipment_No & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.AlternateVehicle = clsCommon.myCstr(dt.Rows(0)("AlternateVehicle"))
                obj.ManualVehicle = clsCommon.myCstr(dt.Rows(0)("ManualVehicle"))
            End If

            qry = "SELECT  TSPL_SD_SALE_INVOICE_DETAIL.Delivery_Code,TSPL_SD_SALE_INVOICE_DETAIL.Crate,TSPL_SD_SALE_INVOICE_DETAIL.Sampling,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Type,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Pers,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Code, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_Code, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Type,TSPL_SD_SALE_INVOICE_DETAIL.Is_Mannual_Amt,TSPL_SD_SALE_INVOICE_DETAIL.Document_Code,TSPL_SD_SALE_INVOICE_DETAIL.Line_No, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Status,TSPL_SD_SALE_INVOICE_DETAIL.Row_Type,TSPL_SD_SALE_INVOICE_DETAIL.status, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.Qty, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Free_Qty,TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code,TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Balance_Qty,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Location, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.TAX1,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX2,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX3,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX4,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX5,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX6,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX7,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX8,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX9,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX10,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Disc_Amt,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Base_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Base_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Base_Amt, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Base_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Base_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Base_Amt, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Base_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Base_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Base_Amt, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.MRP,TSPL_SD_SALE_INVOICE_DETAIL.Batch_No,TSPL_SD_SALE_INVOICE_DETAIL.MFG_Date, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Expiry_Date,TSPL_SD_SALE_INVOICE_DETAIL.Specification,TSPL_SD_SALE_INVOICE_DETAIL.Remarks, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Assessable,TSPL_SD_SALE_INVOICE_DETAIL.AssessableAmt," & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Applicable,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Code, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,TSPL_SD_SALE_INVOICE_DETAIL.Item_Tax,TSPL_SD_SALE_INVOICE_DETAIL.Total_MRP_Amt, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt,TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,TSPL_SD_SALE_INVOICE_DETAIL.Cust_Discount, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Total_Cust_Discount,TSPL_SD_SALE_INVOICE_DETAIL.ActualRate,TSPL_SD_SALE_INVOICE_DETAIL.Cust_DiscountQty, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Price_code,TSPL_SD_SALE_INVOICE_DETAIL.Abatement_Per,TSPL_SD_SALE_INVOICE_DETAIL.Abatement_Amt, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,TSPL_SD_SALE_INVOICE_DETAIL.Item_Weight,TSPL_SD_SALE_INVOICE_DETAIL.Price_Date, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.TotalItem_Weight,TSPL_SD_SALE_INVOICE_DETAIL.Conv_Factor,TSPL_SD_SALE_INVOICE_DETAIL.Purchase_Cost,TSPL_SD_SALE_INVOICE_DETAIL.OrgRate,  " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.HeadDiscPer,TSPL_SD_SALE_INVOICE_DETAIL.HeadDiscPerAmt,TSPL_SD_SALE_INVOICE_DETAIL.Bin_No,TSPL_SD_SALE_INVOICE_DETAIL.vendor_code,TSPL_SD_SALE_INVOICE_DETAIL.vendor_desc,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleCode,TSPL_SD_SALE_INVOICE_DETAIL.PrincipleDesc,TSPL_SD_SALE_INVOICE_DETAIL.Markup_On,TSPL_SD_SALE_INVOICE_DETAIL.Markup_Percent,TSPL_SD_SALE_INVOICE_DETAIL.Landing_Cost,TSPL_SD_SALE_INVOICE_DETAIL.HeadDiscAmt,TSPL_SD_SALE_INVOICE_DETAIL.CustDiscPer,TSPL_SD_SALE_INVOICE_DETAIL.CasdDiscScheme_Code "
            qry += " ,isnull(TSPL_SD_SALE_INVOICE_DETAIL.CAN,0) as can,isnull(TSPL_SD_SALE_INVOICE_DETAIL.ManualCan,0) AS ManualCan "
            qry += " ,ShipmentQty,TSPL_SD_SALE_INVOICE_DETAIL.ItemLeakageAmount FROM TSPL_SD_SALE_INVOICE_DETAIL "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_DETAIL.Location "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code"
            qry += " where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_SD_SALE_INVOICE_DETAIL.Line_No asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsSaleInvoiceDetailFreshSale)
                Dim objTr As clsSaleInvoiceDetailFreshSale
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSaleInvoiceDetailFreshSale
                    objTr.Delivery_Code = clsCommon.myCstr(dr("Delivery_Code"))
                    objTr.Crate = clsCommon.myCdbl(dr("Crate"))

                    'sanjay BHA/21/06/18-000064
                    objTr.CanQty = clsCommon.myCdbl(dr("can"))
                    objTr.ManualCanQty = clsCommon.myCdbl(dr("ManualCan"))
                    'sanjay BHA/21/06/18-000064

                    objTr.Sampling = clsCommon.myCdbl(dr("Sampling"))
                    objTr.Cash_Scheme_Code = clsCommon.myCstr(dr("Cash_Scheme_Code"))
                    objTr.Cash_Scheme_Type = clsCommon.myCstr(dr("Cash_Scheme_Type"))
                    objTr.Cash_Scheme_Pers = clsCommon.myCdbl(dr("Cash_Scheme_Pers"))
                    objTr.Cash_Scheme_Amount = clsCommon.myCdbl(dr("Cash_Scheme_Amount"))

                    objTr.Scheme_Type = clsCommon.myCstr(dr("Scheme_Type"))
                    objTr.Scheme_Qty = clsCommon.myCdbl(dr("Scheme_Qty"))
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme_Item_UOM"))
                    objTr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme_Item_Code"))

                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.ItemLeakageAmount = clsCommon.myCdbl(dr("ItemLeakageAmount"))
                    objTr.ShipmentQty = clsCommon.myCdbl(dr("ShipmentQty"))
                    objTr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))
                    objTr.Shipment_Code = clsCommon.myCstr(dr("Shipment_Code"))

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
                    If IsDBNull(dr("Price_Date")) = True Then
                        objTr.Price_Date = Nothing
                    Else
                        objTr.Price_Date = clsCommon.GetPrintDate(dr("Price_Date"), "dd/MMM/yyyy")
                    End If

                    objTr.Price_code = clsCommon.myCstr(dr("Price_code"))
                    'objTr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
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
                    objTr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                    objTr.Purchase_Cost = clsCommon.myCdbl(dr("Purchase_Cost"))
                    objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    objTr.PrincipleCode = clsCommon.myCstr(dr("PrincipleCode"))
                    objTr.PrincipleDesc = clsCommon.myCstr(dr("PrincipleDesc"))
                    objTr.vendor_code = clsCommon.myCstr(dr("vendor_code"))
                    objTr.vendor_desc = clsCommon.myCstr(dr("vendor_desc"))
                    objTr.HeadDiscPer = clsCommon.myCdbl(dr("HeadDiscPer"))
                    objTr.HeadDiscPerAmt = clsCommon.myCdbl(dr("HeadDiscPerAmt"))
                    obj.Arr.Add(objTr)
                Next
            End If

        End If

        Return obj
    End Function

    Public Shared Function GetOriginalQty(ByVal strMrnNo As String, ByVal strICode As String, ByVal strUOM As String, ByVal dblAssessable As Double, ByVal dblMRP As Double, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "Select TSPL_MRN_DETAIL.MRN_No,(TSPL_MRN_DETAIL.MRN_Qty+ISNULL(TSPL_MRN_DETAIL.Leak_Qty,0) +ISNULL(TSPL_MRN_DETAIL.Burst_Qty,0)+ISNULL(TSPL_MRN_DETAIL.Short_Qty,0)) as MRN_Qty,TSPL_GRN_DETAIL.GRN_No,(TSPL_GRN_DETAIL.GRN_Qty+ISNULL(TSPL_GRN_DETAIL.Leak_Qty,0) +ISNULL(TSPL_GRN_DETAIL.Burst_Qty,0)+ISNULL(TSPL_GRN_DETAIL.Short_Qty,0)) as GRN_Qty, TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty from TSPL_MRN_DETAIL left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code and TSPL_GRN_DETAIL.Unit_code=TSPL_MRN_DETAIL.Unit_code and isnull(TSPL_GRN_DETAIL.Assessable,0)=isnull(TSPL_MRN_DETAIL.Assessable,0) and isnull(TSPL_GRN_DETAIL.Item_Code,0)=isnull(TSPL_MRN_DETAIL.Item_Code ,0) left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No= TSPL_GRN_DETAIL.PO_Id and TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_PURCHASE_ORDER_DETAIL.Unit_code=  TSPL_GRN_DETAIL.Unit_code and isnull(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0)=  isnull(TSPL_GRN_DETAIL.Assessable,0) and isnull(TSPL_PURCHASE_ORDER_DETAIL.MRP,0)=  isnull(TSPL_GRN_DETAIL.MRP,0) where TSPL_MRN_DETAIL.MRN_No='" + strMrnNo + "' and TSPL_MRN_DETAIL.Item_Code='" + strICode + "' and TSPL_MRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_MRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_MRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'"
        Return clsDBFuncationality.GetDataTable(qry, trans)
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
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal strARInvoiceNoForRecreatedOnly As String = Nothing, Optional ByVal strVoucherNoForRecreatedOnly As String = Nothing) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsSaleInvoiceFreshSale = clsSaleInvoiceFreshSale.GetData(strDocNo, NavigatorType.Current, "", trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFreshSale, clsUserMgtCode.frmInvoiceFreshSale, obj.Bill_To_Location, obj.Document_Date, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            checkApprovalNotification(obj.Document_Date, obj.Customer_Code, trans)
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Transaction " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""

            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If

            createARInvoice(obj, trans, strARInvoiceNoForRecreatedOnly, strVoucherNoForRecreatedOnly)
            Dim strARInvNo = clsDBFuncationality.getSingleValue("Select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_No='" + strDocNo + "'", trans)
            Dim ECustomerType = clsERPFuncationality.GetCustomerEInvoiceType(obj.Customer_Code, trans)
            qry = "Update TSPL_SD_SALE_INVOICE_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "',EInvoice_Type='" + ECustomerType + "'"
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If obj.Is_Create_Auto_Receipt Then
                Dim strBankCode As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptDefaultBankForSettlement, clsFixedParameterCode.LOReceiptDefaultBankForSettlement, trans)
                If clsCommon.myLen(strBankCode) <= 0 Then
                    Throw New Exception("Default Bank code not found")
                End If
                Dim strPaymentCode As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptPaymentTypeForSettlement, clsFixedParameterCode.LOReceiptPaymentTypeForSettlement, trans)
                If clsCommon.myLen(strPaymentCode) <= 0 Then
                    Throw New Exception("Default Payemnt code not found")
                End If
                clsReceiptHeader.ReciepEntryWithPostOfInvoice(strARInvNo, strBankCode, strPaymentCode, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function createARInvoice(ByVal obj As clsSaleInvoiceFreshSale, ByVal trans As SqlTransaction, Optional ByVal strARInvNoForRecreatedOnly As String = Nothing, Optional ByVal strVoucherNoForRecreatedOnly As String = Nothing) As Boolean
        ''''''''''''''''''''''''''''''''''For Making AR Invoice
        Dim Auto_Gen_Doc_No As Boolean = True
        Dim objCustInv As New clsCustomerInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed
        If strARInvNoForRecreatedOnly IsNot Nothing AndAlso clsCommon.myLen(strARInvNoForRecreatedOnly) > 0 Then ''23/03/2015
            Auto_Gen_Doc_No = False
            objCustInv.Document_No = strARInvNoForRecreatedOnly
        End If
        objCustInv.LeakageAmount = obj.LeakageAmount
        objCustInv.Document_Date = obj.Document_Date
        objCustInv.Document_Type = "I"
        objCustInv.Trans_Type = "FS"
        objCustInv.Document_Total = obj.Total_Amt
        objCustInv.Customer_Code = obj.Customer_Code
        objCustInv.Customer_Name = obj.Customer_Name
        objCustInv.Posting_Date = obj.Document_Date
        objCustInv.isCardSale = obj.isCardSale
        Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Customer_Code + "'"
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
        ''objCustInv.Order_No
        objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Bill_To_Location, trans)
        objCustInv.On_Hold = 0
        objCustInv.Remarks = obj.Remarks
        objCustInv.Description = obj.Description
        objCustInv.Tax_Group = obj.Tax_Group
        objCustInv.TAX1 = obj.TAX1
        objCustInv.TAX1_Rate = obj.TAX1_Rate
        objCustInv.TAX1_Amt = obj.TAX1_Amt
        objCustInv.TAX2 = obj.TAX2_Amt
        objCustInv.TAX2_Rate = obj.TAX2_Rate
        objCustInv.TAX2_Amt = obj.TAX2_Amt
        objCustInv.TAX3 = obj.TAX3
        objCustInv.TAX3_Rate = obj.TAX3_Rate
        objCustInv.TAX3_Amt = obj.TAX3_Amt
        objCustInv.TAX4 = obj.TAX4
        objCustInv.TAX4_Rate = obj.TAX4_Rate
        objCustInv.TAX4_Amt = obj.TAX4_Amt
        objCustInv.TAX5 = obj.TAX5
        objCustInv.TAX5_Rate = obj.TAX5_Rate
        objCustInv.TAX5_Amt = obj.TAX5_Amt
        objCustInv.TAX6 = obj.TAX6
        objCustInv.TAX6_Rate = obj.TAX6_Rate
        objCustInv.TAX6_Amt = obj.TAX6_Amt
        objCustInv.TAX7 = obj.TAX7
        objCustInv.TAX7_Rate = obj.TAX7_Rate
        objCustInv.TAX7_Amt = obj.TAX7_Amt
        objCustInv.TAX8 = obj.TAX8
        objCustInv.TAX8_Rate = obj.TAX8_Rate
        objCustInv.TAX8_Amt = obj.TAX8_Amt
        objCustInv.TAX9 = obj.TAX9
        objCustInv.TAX9_Rate = obj.TAX9_Rate
        objCustInv.TAX9_Amt = obj.TAX9_Amt
        objCustInv.TAX10 = obj.TAX10
        objCustInv.TAX10_Rate = obj.TAX10_Rate
        objCustInv.TAX10_Amt = obj.TAX10_Amt
        objCustInv.Total_Tax = obj.Total_Tax_Amt
        objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt
        objCustInv.Tax2_BAmount = obj.TAX2_Base_Amt
        objCustInv.Tax3_BAmount = obj.TAX3_Base_Amt
        objCustInv.Tax4_BAmount = obj.TAX4_Base_Amt
        objCustInv.Tax5_BAmount = obj.TAX5_Base_Amt
        objCustInv.Tax6_BAmount = obj.TAX6_Base_Amt
        objCustInv.Tax7_BAmount = obj.TAX7_Base_Amt
        objCustInv.Tax8_BAmount = obj.TAX8_Base_Amt
        objCustInv.Tax9_BAmount = obj.TAX9_Base_Amt
        objCustInv.Tax10_BAmount = obj.TAX10_Base_Amt
        objCustInv.Balance_Amt = obj.Total_Amt
        objCustInv.Terms_Code = clsDBFuncationality.getSingleValue("select Terms_Code from tspl_customer_master where Cust_Code='" & objCustInv.Customer_Code & "'", trans)
        objCustInv.PROJECT_ID = obj.PROJECT_ID

        '' currency details
        objCustInv.CURRENCY_CODE = obj.CURRENCY_CODE
        objCustInv.ConvRate = obj.ConvRate
        objCustInv.ApplicableFrom = obj.ApplicableFrom

        qry = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + objCustInv.Terms_Code + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            objCustInv.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
            objCustInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
        End If

        objCustInv.Discount_Percentage = IIf(obj.Discount_Base > 0, obj.Discount_Amt * 100 / obj.Discount_Base, 0)
        objCustInv.Discount_Base = obj.Discount_Base
        objCustInv.Discount_Amount = obj.Discount_Amt
        ''objCustInv.Amount_Less_Discount = 
        dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
            If clsCommon.myCdbl(obj.Discount_Amt) > 0 Then
                objCustInv.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Receipts_Discount_acct"))
            End If
        End If

        If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
            objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
            objCustInv.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
            objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
            objCustInv.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
            objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
            objCustInv.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
            objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
            objCustInv.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
            objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
            objCustInv.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
            objCustInv.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
            objCustInv.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX6_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
            objCustInv.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
            objCustInv.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX7_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
            objCustInv.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
            objCustInv.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX8_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
            objCustInv.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
            objCustInv.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX9_GLAC, obj.Bill_To_Location, trans)
        End If
        If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
            objCustInv.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
            objCustInv.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX10_GLAC, obj.Bill_To_Location, trans)
        End If

        'objCustInv.RefDocType=
        'objCustInv.RefDocNo
        objCustInv.Add_Charge_Code1 = obj.Add_Charge_Code1
        objCustInv.Add_Charge_Name1 = obj.Add_Charge_Name1
        objCustInv.Add_Charge_Amt1 = obj.Add_Charge_Amt1
        objCustInv.Add_Charge_Code2 = obj.Add_Charge_Code2
        objCustInv.Add_Charge_Name2 = obj.Add_Charge_Name2
        objCustInv.Add_Charge_Amt2 = obj.Add_Charge_Amt2
        objCustInv.Add_Charge_Code3 = obj.Add_Charge_Code3
        objCustInv.Add_Charge_Name3 = obj.Add_Charge_Name3
        objCustInv.Add_Charge_Amt3 = obj.Add_Charge_Amt3
        objCustInv.Add_Charge_Code4 = obj.Add_Charge_Code4
        objCustInv.Add_Charge_Name4 = obj.Add_Charge_Name4
        objCustInv.Add_Charge_Amt4 = obj.Add_Charge_Amt4
        objCustInv.Add_Charge_Code5 = obj.Add_Charge_Code5
        objCustInv.Add_Charge_Name5 = obj.Add_Charge_Name5
        objCustInv.Add_Charge_Amt5 = obj.Add_Charge_Amt5
        objCustInv.Add_Charge_Code6 = obj.Add_Charge_Code6
        objCustInv.Add_Charge_Name6 = obj.Add_Charge_Name6
        objCustInv.Add_Charge_Amt6 = obj.Add_Charge_Amt6
        objCustInv.Add_Charge_Code7 = obj.Add_Charge_Code7
        objCustInv.Add_Charge_Name7 = obj.Add_Charge_Name7
        objCustInv.Add_Charge_Amt7 = obj.Add_Charge_Amt7
        objCustInv.Add_Charge_Code8 = obj.Add_Charge_Code8
        objCustInv.Add_Charge_Name8 = obj.Add_Charge_Name8
        objCustInv.Add_Charge_Amt8 = obj.Add_Charge_Amt8
        objCustInv.Add_Charge_Code9 = obj.Add_Charge_Code9
        objCustInv.Add_Charge_Name9 = obj.Add_Charge_Name9
        objCustInv.Add_Charge_Amt9 = obj.Add_Charge_Amt9
        objCustInv.Add_Charge_Code10 = obj.Add_Charge_Code10
        objCustInv.Add_Charge_Name10 = obj.Add_Charge_Name10
        objCustInv.Add_Charge_Amt10 = obj.Add_Charge_Amt10
        objCustInv.Total_Add_Charge = obj.Total_Add_Charge
        objCustInv.Tax_Calculation_Type = obj.Tax_Calculation_Type
        ''objCustInv.Status
        ''objCustInv.AgainstScrap
        objCustInv.Against_Sale_No = obj.Document_Code
        Dim counter As Integer = 1
        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)
        For Each objTr As clsSaleInvoiceDetailFreshSale In obj.Arr

            If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Scheme_Item, "Y") = CompairStringResult.Equal Then
                Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
                objCustInvTR.SNo = counter
                If clsCommon.CompairString(objTr.Row_Type, "Item") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Scheme_Item, "Y") = CompairStringResult.Equal) Then
                    dt = clsItemMaster.GetSaleAccGLAC(objTr.Item_Code, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set sale account for item" + objTr.Item_Code)
                    End If
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Sales_Account"))
                    objCustInvTR.Reco_Control_Account = "S"
                    objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
                    objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
                Else ''for row type misl.
                    If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
                        Dim objAC As clsAdditionalCharge = clsAdditionalCharge.GetData(objTr.Item_Code, NavigatorType.Current, trans)
                        If objAC Is Nothing Then
                            Throw New Exception("Please set GL Ac from addition charge" + objTr.Item_Code)
                        End If
                        objCustInvTR.GL_Account_Code = objAC.Account_Code
                        objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
                        objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
                    End If
                End If

                objCustInvTR.Amount = objTr.Amt_Less_Discount + objTr.ItemLeakageAmount
                objCustInvTR.Discount_Per = objTr.Disc_Per
                If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Sampling, 0) = CompairStringResult.Equal Then
                    objCustInvTR.Amount_less_Discount = objTr.Amt_Less_Discount + objTr.ItemLeakageAmount
                    objCustInvTR.Discount = objTr.Disc_Amt
                Else
                    objCustInvTR.Amount_less_Discount = objTr.Amt_Less_Discount
                    objCustInvTR.Discount = objTr.Total_Disc_Amt
                End If


                objCustInvTR.TAX1 = objTr.TAX1
                objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
                objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
                objCustInvTR.TAX2 = objTr.TAX2
                objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
                objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
                objCustInvTR.TAX3 = objTr.TAX3
                objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
                objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
                objCustInvTR.TAX4 = objTr.TAX4
                objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
                objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
                objCustInvTR.TAX5 = objTr.TAX5
                objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
                objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
                objCustInvTR.TAX6 = objTr.TAX6
                objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
                objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
                objCustInvTR.TAX7 = objTr.TAX7
                objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
                objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
                objCustInvTR.TAX8 = objTr.TAX8
                objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
                objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
                objCustInvTR.TAX9 = objTr.TAX9
                objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
                objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
                objCustInvTR.TAX10 = objTr.TAX10
                objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
                objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
                objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
                objCustInvTR.Total_Amount = objTr.Item_Net_Amt
                objCustInvTR.Remarks = objTr.Remarks
                objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
                objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
                objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
                objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
                objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
                objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
                objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
                objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
                objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
                'objCustInvTR.Comments=objTr.Comments
                objCustInv.Arr.Add(objCustInvTR)
                counter += 1
            End If



            '''''   for discount  entry

            If clsCommon.CompairString(objTr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Sampling, 1) = CompairStringResult.Equal Then
                Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
                objCustInvTR.SNo = counter
                If clsCommon.CompairString(objTr.Row_Type, "Item") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(objTr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Sampling, 1) = CompairStringResult.Equal) Then
                    dt = clsItemMaster.GetSchemeAccGLAC(objTr.Item_Code, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set scheme account for item" + objTr.Item_Code)
                    End If
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Schemes"))
                    objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
                    objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
                Else ''for row type misl.
                    If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
                        Dim objAC As clsAdditionalCharge = clsAdditionalCharge.GetData(objTr.Item_Code, NavigatorType.Current, trans)
                        If objAC Is Nothing Then
                            Throw New Exception("Please set GL Ac from addition charge" + objTr.Item_Code)
                        End If
                        objCustInvTR.GL_Account_Code = objAC.Account_Code
                        objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
                        objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
                    End If
                End If

                objCustInvTR.Amount = objTr.Amount
                objCustInvTR.Discount_Per = objTr.Disc_Per
                objCustInvTR.Amount_less_Discount = 0
                objCustInvTR.Discount = objTr.Total_Disc_Amt


                objCustInvTR.TAX1 = objTr.TAX1
                objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
                objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
                objCustInvTR.TAX2 = objTr.TAX2
                objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
                objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
                objCustInvTR.TAX3 = objTr.TAX3
                objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
                objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
                objCustInvTR.TAX4 = objTr.TAX4
                objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
                objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
                objCustInvTR.TAX5 = objTr.TAX5
                objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
                objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
                objCustInvTR.TAX6 = objTr.TAX6
                objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
                objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
                objCustInvTR.TAX7 = objTr.TAX7
                objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
                objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
                objCustInvTR.TAX8 = objTr.TAX8
                objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
                objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
                objCustInvTR.TAX9 = objTr.TAX9
                objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
                objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
                objCustInvTR.TAX10 = objTr.TAX10
                objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
                objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
                objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
                objCustInvTR.Total_Amount = objTr.Item_Net_Amt
                objCustInvTR.Remarks = objTr.Remarks
                objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
                objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
                objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
                objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
                objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
                objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
                objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
                objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
                objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
                'objCustInvTR.Comments=objTr.Comments
                objCustInv.Arr.Add(objCustInvTR)
                counter += 1
            End If
            '''''   



            '''''   for Cash discount  entry added by priti on 18/11/2015 for inserting cash discount in scheme account seperately

            'If clsCommon.myCdbl(objTr.Cash_Scheme_Amount) > 0 Then
            '    Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
            '    objCustInvTR.SNo = counter
            '    dt = clsItemMaster.GetSchemeAccGLAC(objTr.Item_Code, trans)
            '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '        Throw New Exception("Please set scheme account for item" + objTr.Item_Code)
            '    End If
            '    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Schemes"))
            '    objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
            '    objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)


            '    objCustInvTR.Amount = objTr.Cash_Scheme_Amount
            '    objCustInvTR.Discount_Per = 0
            '    objCustInvTR.Amount_less_Discount = 0
            '    objCustInvTR.Discount = objTr.Cash_Scheme_Amount


            '    objCustInvTR.TAX1 = objTr.TAX1
            '    objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
            '    objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
            '    objCustInvTR.TAX2 = objTr.TAX2
            '    objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
            '    objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
            '    objCustInvTR.TAX3 = objTr.TAX3
            '    objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
            '    objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
            '    objCustInvTR.TAX4 = objTr.TAX4
            '    objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
            '    objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
            '    objCustInvTR.TAX5 = objTr.TAX5
            '    objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
            '    objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
            '    objCustInvTR.TAX6 = objTr.TAX6
            '    objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
            '    objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
            '    objCustInvTR.TAX7 = objTr.TAX7
            '    objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
            '    objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
            '    objCustInvTR.TAX8 = objTr.TAX8
            '    objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
            '    objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
            '    objCustInvTR.TAX9 = objTr.TAX9
            '    objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
            '    objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
            '    objCustInvTR.TAX10 = objTr.TAX10
            '    objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
            '    objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
            '    objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
            '    objCustInvTR.Total_Amount = objTr.Item_Net_Amt
            '    objCustInvTR.Remarks = objTr.Remarks
            '    objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
            '    objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
            '    objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
            '    objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
            '    objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
            '    objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
            '    objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
            '    objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
            '    objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
            '    objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
            '    'objCustInvTR.Comments=objTr.Comments
            '    objCustInv.Arr.Add(objCustInvTR)
            '    counter += 1
            'End If
            ' '''''   
        Next
        objCustInv.SaveData(objCustInv, Auto_Gen_Doc_No, trans, "FreshSaleInvoice", strVoucherNoForRecreatedOnly)
        clsCustomerInvoiceHead.PostData("FreshSaleInvoice", objCustInv.Document_No, "", trans, strVoucherNoForRecreatedOnly)

        'objCustInv.SaveData(objCustInv, True, trans, "FreshSaleInvoice")
        'clsCustomerInvoiceHead.PostData("FreshSaleInvoice", objCustInv.Document_No, "", trans)
        Return True
    End Function

    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsSaleInvoiceDetailFreshSale)) As String


        For Each objtr As clsSaleInvoiceDetailFreshSale In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal DispatchNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsSaleInvoiceFreshSale = clsSaleInvoiceFreshSale.GetData(strCode, "", NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", trans)

                Dim qry As String = "delete from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" + DispatchNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + DispatchNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function IsValidCustomer(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Customer_Code not in ('" + strVendorCode + "')"
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
    '         "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD .Item_Type as ItemType," & _
    '         "(replace( CONVERT(varchar(11), TSPL_SD_SALE_INVOICE_HEAD.Document_Date,104),'.','/')+' '+CONVERT(varchar(100),TSPL_SD_SALE_INVOICE_HEAD.Document_Date,108) )as MRN_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.GRNo,TSPL_SD_SALE_INVOICE_HEAD.GENo," & _
    '         "(case when LEN(TSPL_SD_SALE_INVOICE_HEAD.GEDate)>0  then REPLACE( CONVERT(varchar(11), TSPL_SD_SALE_INVOICE_HEAD.GEDate,104),'.','/') else '' end) as GEDate,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_HEAD.Remarks ,TSPL_SD_SALE_INVOICE_HEAD.Ref_No,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code," & _
    '         "case when Unit_code='FC' then Qty + ISNULL( Free_Qty,0) end as FCS, " & _
    '         "case when Unit_code='FB' then Qty + ISNULL( Free_Qty,0) end as FBS, " & _
    '         "case when Unit_code='SH' then Qty + ISNULL( Free_Qty,0) end as FSH, " & _
    '         "case when Unit_code='EC' then Qty + ISNULL( Free_Qty,0) end as ECS," & _
    '         "case when Unit_code='EB' then Qty + ISNULL( Free_Qty,0) end as EBS, " & _
    '         "TSPL_SD_SALE_INVOICE_DETAIL.Leak_Qty,TSPL_SD_SALE_INVOICE_DETAIL.Burst_Qty,TSPL_SD_SALE_INVOICE_DETAIL.Short_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code= TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " & _
    '         " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location   where Item_Type ='F'"
    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    qry += " and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "
    '                End If

    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")" 'ADDED BY ABHISHEK AS ON 30 AUG 2012
    '                End If
    '                qry += " )xxx group by Document_Code,Item_Code order by Item_Desc"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    common.clsCommon.MyMessageBoxShow("No Record Found")
    '                Else
    '                    PurchaseOrderViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "rptSRNCustomReport", "SRN Report")

    '                End If
    '            Else ''For RM Other Print out
    '                Dim strquery As String = "SELECT TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Name,(case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else Document_Date end ) as Challan_Date, TSPL_SD_SALE_INVOICE_HEAD.Ref_No  " & _
    '                      "as Challan_No, TSPL_SD_SALE_INVOICE_HEAD.Inv_No, TSPL_SD_SALE_INVOICE_HEAD.Inv_Date, TSPL_SD_SALE_INVOICE_HEAD.GRNo,TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount ,TSPL_SD_SALE_INVOICE_HEAD.GENo,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt, " & _
    '                      "TSPL_SD_SALE_INVOICE_HEAD.GEDate, TSPL_SD_SALE_INVOICE_HEAD.VehicleNo, TSPL_SD_SALE_INVOICE_HEAD.Carrier,TSPL_SD_SALE_INVOICE_HEAD.Remarks,TSPL_SD_SALE_INVOICE_DETAIL.Landed_Cost_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Landed_Cost_Amount , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Row_Type,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount," & _
    '"TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost as basicRate,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as BasicTotal,TSPL_SD_SALE_INVOICE_DETAIL.Unit_Cost_Tax_Rate as UCTR," & _
    '"TSPL_SD_SALE_INVOICE_DETAIL.Unit_Cost_Tax as uctax,TSPL_SD_SALE_INVOICE_DETAIL.Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Rejected_Qty,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,TSPL_SD_SALE_INVOICE_DETAIL.ITEM_COST," & _
    ' "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " & _
    '"tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," & _
    '"isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt," & _
    '"tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," & _
    '"isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt " & _
    '",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," & _
    '"isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt," & _
    '"tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname, " & _
    '"TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_SD_SALE_INVOICE_DETAIL.Qty," & _
    '"case when tax1.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax1_amt else null end as Tax1Recoverable," & _
    '"case when tax2.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt else null end as Tax2Recoverable, " & _
    '"case when tax3.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax3_amt else null end as Tax3Recoverable, " & _
    '"case when tax4.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax4_amt else null end as Tax4Recoverable, " & _
    '"case when tax5.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax5_amt else null end as Tax5Recoverable, " & _
    '"case when tax6.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax6_amt else null end as Tax6Recoverable," & _
    '"case when tax7.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax7_amt else null end as Tax7Recoverable, " & _
    '"case when tax8.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax8_amt else null end as Tax8Recoverable, " & _
    '"case when tax9.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax9_amt else null end as Tax9Recoverable," & _
    '"case when tax10.Tax_Recoverable='Y' then TSPL_SD_SALE_INVOICE_HEAD.tax10_amt else null end as Tax10Recoverable, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,0),103)+'%' as txt1Rate," & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate   ,0),103)+'%' as txt2Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate  ,0),103)+'%' as txt3Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate  ,0),103)+'%' as txt4Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate  ,0),103)+'%' as txt5Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate  ,0),103)+'%' as txt6Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate  ,0),103)+'%' as txt7Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate  ,0),103)+'%' as txt8Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate  ,0),103)+'%' as txt9Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate  ,0),103)+'%' as txt10Rate," & _
    '"TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as Value,(select SUM(rejected_qty) from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code) as Rej_qty, (select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code and TSPL_MRN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code where Document_Code =TSPL_SD_SALE_INVOICE_HEAD.Document_Code)as MrnTotQty, (select SUM(Qty) from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code) as SRNQtyTotal, (select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" & _
    '" ( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
    '" GROUP by TSPL_PI_DETAIL.PI_No)xxx) as PInvNo  ,    " & _
    '       " TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1 as Add1Name, " & _
    '     " TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1 as Add1 , " & _
    '     "     TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2 as Add2Name, " & _
    '     "   TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2 as Add2 , " & _
    '     "    TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3 as Add3Name, " & _
    '     "   TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3 as Add3 , " & _
    '     "    TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4 as Add4Name, " & _
    '     "    TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4 as Add4 , " & _
    '     "     TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5 as Add5Name, " & _
    '      "     TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5 as Add5 , " & _
    '      "     TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6 as Add6Name, " & _
    '      "    TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6 as Add6 , " & _
    '      "    TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7 as Add7Name, " & _
    '      "     TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7 as Add7 , " & _
    '      "       TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8 as Add8Name, " & _
    '      "      TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8 as Add8 , " & _
    '       "      TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9 as Add9Name, " & _
    '       "      TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9 as Add9 , " & _
    '       "      TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10 as Add10Name, " & _
    '       "     TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10 as Add10,TSPL_SD_SALE_INVOICE_HEAD.Against_RGP,TSPL_SD_SALE_INVOICE_DETAIL .Specification   " & _
    ' " FROM  TSPL_SD_SALE_INVOICE_DETAIL INNER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
    ' "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " & _
    ' "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = TSPL_VENDOR_MASTER.Customer_Code " & _
    ' "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1  " & _
    ' "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2 " & _
    ' "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3 " & _
    ' "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4 " & _
    ' "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5 " & _
    ' "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  " & _
    ' "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7  " & _
    ' "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " & _
    ' "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " & _
    ' " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  " & _
    ' "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  " & _
    ' " where TSPL_SD_SALE_INVOICE_HEAD .Item_Type not in('F')"

    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    strquery += " and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "

    '                End If
    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    strquery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    strquery += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    strquery += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")  "

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

            Dim Qry As String = "select Status from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + strCode + "'"
            Dim obj As clsSaleInvoiceFreshSale = clsSaleInvoiceFreshSale.GetData(strCode, NavigatorType.Current, "", trans)

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFreshSale, clsUserMgtCode.frmInvoiceFreshSale, obj.Bill_To_Location, obj.Document_Date, trans)
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select distinct DOCUMENT_CODE from TSPL_SD_SALE_RETURN_DETAIL where Invoice_Code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Sale invoice is used in following Sale return -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("DOCUMENT_CODE"))
                Next
                Throw New Exception(Qry)
            End If

            Dim strARInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No  from TSPL_Customer_Invoice_Head where Against_Sale_No='" + strCode + "'", trans))
            If clsCommon.myLen(strARInvoiceNo) > 0 Then
                Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AR-IN' and Source_Doc_No='" + strARInvoiceNo + "'", trans)
                If clsCommon.myLen(VoucherNo) > 0 Then
                    Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                End If

                Qry = "delete from TSPL_Customer_Invoice_Detail where Document_No ='" + strARInvoiceNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_Customer_Invoice_Head where Document_No ='" + strARInvoiceNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "Update TSPL_SD_SALE_INVOICE_HEAD set Status = 0 where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_Shipment_No from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + strCode + "'", trans))
            Qry = "Update TSPL_SD_SHIPMENT_HEAD set Status = 0 where Document_Code='" + strShipmentNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsSaleInvoiceDetailFreshSale
#Region "Variables"
    Public ItemLeakageAmount As Decimal = 0
    Public Delivery_Code As String = Nothing
    Public Crate As Double = 0
    Public CanQty As Double = 0
    Public ManualCanQty As Double = 0
    Public Document_Code As String = Nothing
    Public VS_CashSchemeCode As String = String.Empty
    Public VS_Cash_Amt As Double = 0
    Public VS_ltrInCrate As Double = 0
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing 'Not a Table Field
    Public Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Free_Qty As Double = 0
    Public Shipment_Code As String = Nothing
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
    Public SRNTax_Group As String = Nothing 'Not a Table Field

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
    Public Bin_No As String = Nothing
    Public PrincipleCode As String = Nothing
    Public PrincipleDesc As String = Nothing
    Public vendor_code As String = Nothing
    Public vendor_desc As String = Nothing
    Public HeadDiscPer As Double = 0
    Public HeadDiscPerAmt As Double = 0
    Public ShipmentQty As Double = 0
    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Decimal = Nothing
    Public Scheme_Item_UOM As String = Nothing
    Public Cash_Scheme_Code As String = Nothing
    Public Cash_Scheme_Type As String = Nothing
    Public Cash_Scheme_Pers As Decimal = Nothing
    Public Cash_Scheme_Amount As Decimal = Nothing
    Public Sampling As Integer = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSaleInvoiceDetailFreshSale), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSaleInvoiceDetailFreshSale In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Delivery_Code", obj.Delivery_Code)
                clsCommon.AddColumnsForChange(coll, "Crate", obj.Crate)
                clsCommon.AddColumnsForChange(coll, "ItemLeakageAmount", obj.ItemLeakageAmount)
                'sanjay BHA/21/06/18-000064
                clsCommon.AddColumnsForChange(coll, "CAN", obj.CanQty)
                clsCommon.AddColumnsForChange(coll, "ManualCan", obj.ManualCanQty)
                'Sanjay BHA/21/06/18-000064

                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Code", obj.Cash_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Type", obj.Cash_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Pers", obj.Cash_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Amount", obj.Cash_Scheme_Amount)

                clsCommon.AddColumnsForChange(coll, "VS_CashSchemeCode", obj.VS_CashSchemeCode)
                clsCommon.AddColumnsForChange(coll, "VS_Cash_Amt", obj.VS_Cash_Amt)
                clsCommon.AddColumnsForChange(coll, "VS_ltrInCrate", obj.VS_ltrInCrate)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", obj.Scheme_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", obj.Scheme_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.Scheme_Qty)
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)

                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "ShipmentQty", obj.ShipmentQty)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommon.AddColumnsForChange(coll, "Free_qty", obj.Free_Qty)

                clsCommon.AddColumnsForChange(coll, "Shipment_Code", obj.Shipment_Code, True)

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
                clsCommon.AddColumnsForChange(coll, "Markup_On", obj.Markup_On)
                clsCommon.AddColumnsForChange(coll, "Markup_Percent", obj.Markup_Percent)
                clsCommon.AddColumnsForChange(coll, "Landing_Cost", obj.Landing_Cost)
                clsCommon.AddColumnsForChange(coll, "HeadDiscAmt", obj.HeadDiscAmt)
                clsCommon.AddColumnsForChange(coll, "CustDiscPer", obj.CustDiscPer)
                clsCommon.AddColumnsForChange(coll, "CasdDiscScheme_Code", obj.CasdDiscScheme_Code)
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

                '    Dim strSql As String = "select top 1 TSPL_ITEM_PRICE_MASTER.Price_Amount1 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 , " & _
                ' "TSPL_ITEM_PRICE_MASTER.Price_Amount3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 , " & _
                ' "TSPL_ITEM_PRICE_MASTER.Price_Amount6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 , " & _
                ' "TSPL_ITEM_PRICE_MASTER.Price_Amount9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 from TSPL_ITEM_PRICE_MASTER " & _
                '"where  TSPL_ITEM_PRICE_MASTER.Price_Code='" & obj.Price_code & "' and  TSPL_ITEM_PRICE_MASTER.Item_Code='" & obj.Item_Code & "' and  " & _
                '"TSPL_ITEM_PRICE_MASTER.Item_Basic_Net=" & obj.MRP & " and TSPL_ITEM_PRICE_MASTER.UOM='" & obj.Unit_code & "' "
                '    Dim dt = New DataTable()
                '    dt = clsDBFuncationality.GetDataTable(strSql, trans)
                '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                'obj.Price_Amount1 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount1"))
                'obj.Price_Amount2 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount2"))
                'obj.Price_Amount3 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount3"))
                'obj.Price_Amount4 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount4"))
                'obj.Price_Amount5 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount5"))
                'obj.Price_Amount6 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount6"))
                'obj.Price_Amount7 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount7"))
                'obj.Price_Amount8 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount8"))
                'obj.Price_Amount9 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount9"))
                'obj.Price_Amount10 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount10"))
                'End If

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
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Abatement_Per", obj.Abatement_Per)
                clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)
                clsCommon.AddColumnsForChange(coll, "FOC_Item", obj.FOC_Item)

                clsCommon.AddColumnsForChange(coll, "Item_Weight", obj.Item_Weight)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "TotalItem_Weight", obj.TotalItem_Weight)
                clsCommon.AddColumnsForChange(coll, "Purchase_Cost", obj.Purchase_Cost)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommon.AddColumnsForChange(coll, "PrincipleCode", obj.PrincipleCode)
                clsCommon.AddColumnsForChange(coll, "PrincipleDesc", obj.PrincipleDesc)
                clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendor_code)
                clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendor_desc)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPer", obj.HeadDiscPer)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPerAmt", obj.HeadDiscPerAmt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceSRNQty(ByVal strInvoiceCode As String, ByVal strDOCode As String, ByVal strICode As String, ByVal strCurrReturnNNo As String, ByVal strUOM As String, ByVal strReturnUOM As String, ByVal dblMRP As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as ICode,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.Qty  / ReturnUnit.Conversion_Factor) * StockingUOM.Conversion_Factor * OrgUnit.Conversion_Factor) as Qty,1 as RI from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " & _
            " left outer join tspl_item_uom_detail as ReturnUnit on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=ReturnUnit.Item_Code and ReturnUnit.UOM_Code='" & strReturnUOM & "' " & _
            " left outer join tspl_item_uom_detail as OrgUnit on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=OrgUnit.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code=OrgUnit.UOM_Code " & _
            " left outer join tspl_item_uom_detail as StockingUOM on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=StockingUOM.Item_Code and StockingUOM.Stocking_Unit='Y' " & _
            " where TSPL_SD_SALE_INVOICE_HEAD.Status=1 and  Scheme_Item='N' and TSPL_SD_SALE_INVOICE_DETAIL.Document_Code ='" + strInvoiceCode + "' and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code='" + strICode + "' and  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='" + strUOM + "' and  Delivery_Code='" & strDOCode & "' " & _
            " union all "
        If strUOM = strReturnUOM Then
            qry += " select TSPL_SD_SALE_RETURN_DETAIL.Item_Code as ICode,case when OrgUnit_code <> Unit_code then convert(decimal(18,2),(TSPL_SD_SALE_RETURN_DETAIL.Qty  / ReturnUnit.Conversion_Factor) * StockingUOM.Conversion_Factor * OrgUnit.Conversion_Factor) else Qty end as Qty,-1 as RI from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " & _
            "left outer join tspl_item_uom_detail as ReturnUnit on TSPL_SD_SALE_RETURN_DETAIL.Item_Code=ReturnUnit.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.OrgUnit_code= ReturnUnit.UOM_Code " & _
            "left outer join tspl_item_uom_detail as OrgUnit on TSPL_SD_SALE_RETURN_DETAIL.Item_Code=OrgUnit.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.Unit_code=OrgUnit.UOM_Code " & _
            "left outer join tspl_item_uom_detail as StockingUOM on TSPL_SD_SALE_RETURN_DETAIL.Item_Code=StockingUOM.Item_Code and StockingUOM.Stocking_Unit='Y' " & _
            " where TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code='" + strInvoiceCode + "'   and " & _
            " TSPL_SD_SALE_RETURN_DETAIL.Item_Code='" + strICode + "'   and  Scheme_Item='N'  and  " & _
            " isnull(TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code,'')='" & strDOCode & "'  and TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE not in ('" + strCurrReturnNNo + "')  " & _
            " )Final "
        Else
            qry += " select TSPL_SD_SALE_RETURN_DETAIL.Item_Code as ICode,case when OrgUnit_code = Unit_code then convert(decimal(18,2),(TSPL_SD_SALE_RETURN_DETAIL.Qty  / ReturnUnit.Conversion_Factor) * StockingUOM.Conversion_Factor * OrgUnit.Conversion_Factor) else Qty end as Qty,-1 as RI from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " & _
            "left outer join tspl_item_uom_detail as ReturnUnit on TSPL_SD_SALE_RETURN_DETAIL.Item_Code=ReturnUnit.Item_Code and  ReturnUnit.UOM_Code='" & strReturnUOM & "' " & _
            "left outer join tspl_item_uom_detail as OrgUnit on TSPL_SD_SALE_RETURN_DETAIL.Item_Code=OrgUnit.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.Unit_code=OrgUnit.UOM_Code " & _
            "left outer join tspl_item_uom_detail as StockingUOM on TSPL_SD_SALE_RETURN_DETAIL.Item_Code=StockingUOM.Item_Code and StockingUOM.Stocking_Unit='Y' " & _
            " where TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code='" + strInvoiceCode + "'   and " & _
            " TSPL_SD_SALE_RETURN_DETAIL.Item_Code='" + strICode + "'   and  Scheme_Item='N'  and  " & _
            " isnull(TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code,'')='" & strDOCode & "'  and TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE not in ('" + strCurrReturnNNo + "')  " & _
            " )Final "
        End If

        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_SD_SALE_INVOICE_DETAIL set Status=1 where Document_Code='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class
