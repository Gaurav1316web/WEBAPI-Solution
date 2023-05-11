Imports common
Imports System.Data.SqlClient
Public Class clsEXPorformaInvoiceHead

#Region "Variables"
    Public Document_Type As String = Nothing
    Public Payment_Terms As String = Nothing
    Public is_Accepted As String = Nothing
    Public Accepted_Date As Date = Nothing
    Public is_Partshipment As String = Nothing
    Public is_Transshipment As String = Nothing

    Public Stuffing_Status As String = Nothing
    Public Exporter_Ref_No As String = Nothing
    Public Pre_Carriage_By As String = Nothing
    Public Discharge_Port As String = Nothing
    Public Final_Destination As String = Nothing
    Public Origin_Country As String = Nothing
    Public Final_Destination_Country As String = Nothing
    Public Gross_Wt As Double = 0
    Public Final_Gross_Wt As Double = 0
    Public Is_Delivered As Integer = 0
    Public podate As DateTime
    Public Form_38_No As String = Nothing
    Public Cust_PO_No As String = Nothing
    Public Is_Taxable As Integer = 0
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
    Public Airway_Line As String = Nothing
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
    Public Against_Shipment_No As String = Nothing
    Public Is_Internal As Boolean = False
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Is_Create_Auto_Receipt As Boolean = False
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Arr As List(Of clsEXPorformaInvoiceDetail) = Nothing

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

    Public Advance_Percentage As Decimal = Nothing
    Public EX_Term_Code As String = Nothing
    Public Commission_Instruction As String = Nothing
    Public Commission_Payee_Code As String = Nothing
    Public Comm_Amt_Type As String = Nothing
    Public Commission_Amount As Decimal = Nothing
    Public Commission_Paid As String = Nothing
    Public BRANCH_CODE As String = Nothing
    Public BANK_CODE As String = Nothing
    ''richa agarwal 14/04/2015
    Public MT_HS_Classification_No As String = ""
    Public MT_Payment_Terms_Group_Code As String = ""
    Public MT_Payment_Terms_Group_Desc As String = ""
    Public MT_Is_AmountinRs As Integer = 0
    Public MT_LC As Double = 0
    Public MT_CAD As Double = 0
    Public MT_RETAINED As Double = 0
    Public MT_CIF As Double = 0
    Public MT_Balance_Payment As Double = 0
    Public MT_On_Account As Double = 0
    Public MT_Advance As Double = 0
    Public MT_Beneficiary_Code As String = ""
    Public MT_Beneficiary_Name As String = ""
    Public MT_INCOTERMS As String = ""
    Public MT_Against_PO_Date As DateTime? = Nothing
    Public MT_Against_PO As String = Nothing
    ''----------------
    Public Arr_Notify As List(Of clsEXPorformaInvoiceNotifyDetail) = Nothing
    Public Loading_Port As String = Nothing
#End Region

    Public Shared Function GetProformaDescrptn() As String
        Dim str As String = ""
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 comments from TSPL_EX_PI_HEAD where isnull(comments,'')<>'' order by document_date desc"))

        If clsCommon.myLen(str) <= 0 Then
            str = "1. Marine insurance exclusively covered by the buyer." + Environment.NewLine + "2. The goods remain the property of seller until paid in full." + Environment.NewLine + "3. Bank contract must be sent within 5 working days from the date " + Environment.NewLine + "   of receipt of this Proforma Invoice." + Environment.NewLine + "4. Immediate Delivery without pallet." + Environment.NewLine + "5. Buyer to advise timely if any additional markings/ information is " + Environment.NewLine + "   required." + Environment.NewLine + "6. Please return a copy of this Proforma Invoice duly signed and " + Environment.NewLine + "   stamped for acceptance." + Environment.NewLine + "7. Country of Origin : INDIA"
        End If

        Return str
    End Function

    Public Function SaveData(ByVal obj As clsEXPorformaInvoiceHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function SaveData(ByVal obj As clsEXPorformaInvoiceHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try

            Dim qry As String = "delete from TSPL_EX_PI_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_NOTIFY_PARTY_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Document_Type, "EX") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXPORFORMAINVOICE, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "S") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXPORFORMAINVOICE, clsDocTransactionType.POSemiFinishedGoods, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXPORFORMAINVOICE, clsDocTransactionType.PORawMaterial, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXPORFORMAINVOICE, clsDocTransactionType.SNQuotationOther, obj.Bill_To_Location, False)
                    Else
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXPORFORMAINVOICE, clsDocTransactionType.SNOrderExport, obj.Bill_To_Location, False)
                    End If
                Else
                    If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTPORFORMAINVOICE, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "S") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTPORFORMAINVOICE, clsDocTransactionType.POSemiFinishedGoods, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTPORFORMAINVOICE, clsDocTransactionType.PORawMaterial, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTPORFORMAINVOICE, clsDocTransactionType.SNQuotationOther, obj.Bill_To_Location, False)
                    Else
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTPORFORMAINVOICE, clsDocTransactionType.SNOrderExport, obj.Bill_To_Location, False)
                    End If
                End If
            End If
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)

            If IsDBNull(obj.podate) = True Then
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.podate, "dd/MMM/yyyy hh:mm tt"))
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Internal", IIf(obj.Is_Internal, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Airway_Line", obj.Airway_Line)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", obj.Is_Taxable)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Inv_No", obj.Inv_No)
            clsCommon.AddColumnsForChange(coll, "Stuffing_Status", obj.Stuffing_Status)
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
            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Carrier", obj.Carrier)
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Import_Export_No", obj.GRNo)
            clsCommon.AddColumnsForChange(coll, "GENo", obj.GENo)
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Against_SO_No", obj.Against_Shipment_No, True)
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

            If obj.ApplicableFrom.HasValue Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)
            End If
            ''---------------------
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

            clsCommon.AddColumnsForChange(coll, "Exporter_Ref_No", obj.Exporter_Ref_No)
            clsCommon.AddColumnsForChange(coll, "Pre_Carriage_By", obj.Pre_Carriage_By)
            clsCommon.AddColumnsForChange(coll, "Final_Destination", obj.Final_Destination)
            clsCommon.AddColumnsForChange(coll, "Final_Destination_Country", obj.Final_Destination_Country)
            clsCommon.AddColumnsForChange(coll, "Origin_Country", obj.Origin_Country)
            clsCommon.AddColumnsForChange(coll, "Discharge_Port", obj.Discharge_Port)
            clsCommon.AddColumnsForChange(coll, "Loading_Port", obj.Loading_Port)
            clsCommon.AddColumnsForChange(coll, "Gross_Wt", obj.Gross_Wt)
            clsCommon.AddColumnsForChange(coll, "Final_Gross_Wt", obj.Final_Gross_Wt)
            clsCommon.AddColumnsForChange(coll, "Commission_Paid", obj.Commission_Paid)
            clsCommon.AddColumnsForChange(coll, "Commission_Amount", obj.Commission_Amount)
            clsCommon.AddColumnsForChange(coll, "Comm_Amt_Type", obj.Comm_Amt_Type)
            clsCommon.AddColumnsForChange(coll, "Commission_Payee_Code", obj.Commission_Payee_Code)
            clsCommon.AddColumnsForChange(coll, "Commission_Instruction", obj.Commission_Instruction)
            clsCommon.AddColumnsForChange(coll, "EX_Term_Code", obj.EX_Term_Code)
            clsCommon.AddColumnsForChange(coll, "Payment_Terms", obj.Payment_Terms)
            clsCommon.AddColumnsForChange(coll, "Advance_Percentage", obj.Advance_Percentage)
            clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE, True)
            clsCommon.AddColumnsForChange(coll, "BRANCH_CODE", obj.BRANCH_CODE)
            clsCommon.AddColumnsForChange(coll, "is_Accepted", obj.is_Accepted)
            clsCommon.AddColumnsForChange(coll, "Accepted_Date", clsCommon.GetPrintDate(obj.Accepted_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "is_Partshipment", obj.is_Partshipment)
            clsCommon.AddColumnsForChange(coll, "is_Transshipment", obj.is_Transshipment)
            ''richa agarwal 14/04/2015
            clsCommon.AddColumnsForChange(coll, "MT_HS_Classification_No", obj.MT_HS_Classification_No)
            clsCommon.AddColumnsForChange(coll, "MT_Payment_Terms_Group_Code", obj.MT_Payment_Terms_Group_Code, True)
            clsCommon.AddColumnsForChange(coll, "MT_Payment_Terms_Group_Desc", obj.MT_Payment_Terms_Group_Desc, True)
            clsCommon.AddColumnsForChange(coll, "MT_Is_AmountinRs", obj.MT_Is_AmountinRs)
            clsCommon.AddColumnsForChange(coll, "MT_LC", obj.MT_LC)
            clsCommon.AddColumnsForChange(coll, "MT_CAD", obj.MT_CAD)
            clsCommon.AddColumnsForChange(coll, "MT_RETAINED", obj.MT_RETAINED)
            clsCommon.AddColumnsForChange(coll, "MT_CIF", obj.MT_CIF)
            clsCommon.AddColumnsForChange(coll, "MT_Balance_Payment", obj.MT_Balance_Payment)
            clsCommon.AddColumnsForChange(coll, "MT_On_Account", obj.MT_On_Account)
            clsCommon.AddColumnsForChange(coll, "MT_Advance", obj.MT_Advance)
            clsCommon.AddColumnsForChange(coll, "MT_Beneficiary_Code", obj.MT_Beneficiary_Code)
            clsCommon.AddColumnsForChange(coll, "MT_INCOTERMS", obj.MT_INCOTERMS, True)
            clsCommon.AddColumnsForChange(coll, "MT_Beneficiary_Name", obj.MT_Beneficiary_Name)
            clsCommon.AddColumnsForChange(coll, "MT_Against_PO", obj.MT_Against_PO, True)
            If obj.MT_Against_PO_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "MT_Against_PO_Date", clsCommon.GetPrintDate(obj.MT_Against_PO_Date, "dd/MMM/yyyy hh:mm tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "MT_Against_PO_Date", Nothing, True)
            End If
            ''-----------------------
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EX_PI_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EX_PI_HEAD", OMInsertOrUpdate.Update, "TSPL_EX_PI_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsEXPorformaInvoiceDetail.SaveData(obj.Document_Code, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            isSaved = isSaved AndAlso clsEXPorformaInvoiceNotifyDetail.SaveData(obj.Document_Code, obj.Arr_Notify, trans)
            '''' to save item weight unit
            qry = "update TSPL_EX_PI_DETAIL set Weight_UOM= (select Weight_UOM from TSPL_ITEM_MASTER where Item_Code=TSPL_EX_PI_DETAIL.Item_Code)  where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '''' 
            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_EX_PI_HEAD", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal Export_Merchant As String) As Boolean
        '' created by Richa Agarwal against ticket No- TEC/23/05/18-000255 on date 06-06-2018
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            '' table list 
            ''1. TSPL_EX_PI_HEAD
            ''2. TSPL_EX_PI_DETAIL
            ''3. TSPL_EX_NOTIFY_PARTY_DETAIL
            ''4.TSPL_CUSTOM_FIELD_VALUES
           
            '' steps for checking the items stock and batch wise stock

            Dim obj As clsEXPorformaInvoiceHead = clsEXPorformaInvoiceHead.GetData(Doc_No, NavType, "'T','R'", arrLoc, Export_Merchant, trans)

            If obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)
            '' transfer data into cancel table

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_EX_PI_HEAD", "Document_Code", "TSPL_EX_PI_DETAIL", "Document_Code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_EX_NOTIFY_PARTY_DETAIL", "Document_Code", trans)

            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)


            ''delete data from multiple tables

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_NOTIFY_PARTY_DETAIL where Document_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_PI_DETAIL where Document_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_PI_HEAD where Document_Code='" & Doc_No & "' and Document_Type='" & Export_Merchant & "' "
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
    Public Shared Function SaveEx_SO(ByVal objPI As clsEXPorformaInvoiceHead, ByVal arrLoc As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsEXSalesOrder()
        Dim objtr As New clsEXSalesOrderDetail()
        Try
            Dim isSaved As Boolean = True

            obj = New clsEXSalesOrder()
            obj.Arr = New List(Of clsEXSalesOrderDetail)
            obj.Arr_Notify = New List(Of clsEXSalesOrderNotifyParty)

            If objPI IsNot Nothing Then
                obj.Document_Date = objPI.Document_Date
                obj.SalesOrder_Type = objPI.Document_Type
                obj.Delivery_date = objPI.Document_Date
                obj.CloseSO = "N"
                obj.Customer_Code = objPI.Customer_Code
                obj.On_Hold = False
                obj.Ref_No = ""
                obj.Remarks = objPI.Remarks
                obj.Description = objPI.Description
                obj.Bill_To_Location = objPI.Bill_To_Location
                obj.Ship_To_Location = objPI.Ship_To_Location

                obj.Tax_Group = objPI.Tax_Group
                obj.TAX1 = objPI.TAX1
                obj.TAX1_Rate = objPI.TAX1_Rate
                obj.TAX1_Base_Amt = objPI.TAX1_Base_Amt
                obj.TAX1_Amt = objPI.TAX1_Amt
                obj.TAX2 = objPI.TAX2
                obj.TAX2_Rate = objPI.TAX2_Rate
                obj.TAX2_Base_Amt = objPI.TAX2_Base_Amt
                obj.TAX2_Amt = objPI.TAX2_Amt
                obj.TAX3 = objPI.TAX3
                obj.TAX3_Rate = objPI.TAX3_Rate
                obj.TAX3_Base_Amt = objPI.TAX3_Base_Amt
                obj.TAX3_Amt = objPI.TAX3_Amt
                obj.TAX4 = objPI.TAX4
                obj.TAX4_Rate = objPI.TAX4_Rate
                obj.TAX4_Base_Amt = objPI.TAX4_Base_Amt
                obj.TAX4_Amt = objPI.TAX4_Amt
                obj.TAX5 = objPI.TAX5
                obj.TAX5_Rate = objPI.TAX5_Rate
                obj.TAX5_Base_Amt = objPI.TAX5_Base_Amt
                obj.TAX5_Amt = objPI.TAX5_Amt
                obj.TAX6 = objPI.TAX6
                obj.TAX6_Rate = objPI.TAX6_Rate
                obj.TAX6_Base_Amt = objPI.TAX6_Base_Amt
                obj.TAX6_Amt = objPI.TAX6_Amt
                obj.TAX7 = objPI.TAX7
                obj.TAX7_Rate = objPI.TAX7_Rate
                obj.TAX7_Base_Amt = objPI.TAX7_Base_Amt
                obj.TAX7_Amt = objPI.TAX7_Amt
                obj.TAX8 = objPI.TAX8
                obj.TAX8_Rate = objPI.TAX8_Rate
                obj.TAX8_Base_Amt = objPI.TAX8_Base_Amt
                obj.TAX8_Amt = objPI.TAX8_Amt
                obj.TAX9 = objPI.TAX9
                obj.TAX9_Rate = objPI.TAX9_Rate
                obj.TAX9_Base_Amt = objPI.TAX9_Base_Amt
                obj.TAX9_Amt = objPI.TAX9_Amt
                obj.TAX10 = objPI.TAX10
                obj.TAX10_Rate = objPI.TAX10_Rate
                obj.TAX10_Base_Amt = objPI.TAX10_Base_Amt
                obj.TAX10_Amt = objPI.TAX10_Amt
                obj.Total_Tax_Amt = objPI.Total_Tax_Amt
                ''richa BM00000008807
                obj.Auto_SaleOrder = 1
                ''-------------
                obj.Discount_Base = objPI.Discount_Base
                obj.Discount_Amt = objPI.Discount_Amt
                obj.Amount_Less_Discount = objPI.Amount_Less_Discount
                obj.Total_Amt = objPI.Total_Amt
                obj.Mode_Of_Transport = objPI.Pre_Carriage_By
                obj.Comments = objPI.Comments
                obj.Dept = objPI.Dept
                obj.Dept_Desc = objPI.Dept_Desc
                obj.Item_Type = objPI.Item_Type
                obj.Against_Quotation_No = ""
                obj.PROJECT_ID = objPI.PROJECT_ID
                'obj.Approvel_Required = ""

                obj.Add_Charge_Code1 = objPI.Add_Charge_Code1
                obj.Add_Charge_Name1 = objPI.Add_Charge_Name1
                obj.Add_Charge_Amt1 = objPI.Add_Charge_Amt1
                obj.Add_Charge_Code2 = objPI.Add_Charge_Code2
                obj.Add_Charge_Name2 = objPI.Add_Charge_Name2
                obj.Add_Charge_Amt2 = objPI.Add_Charge_Amt2
                obj.Add_Charge_Code3 = objPI.Add_Charge_Code3
                obj.Add_Charge_Name3 = objPI.Add_Charge_Name3
                obj.Add_Charge_Amt3 = objPI.Add_Charge_Amt3
                obj.Add_Charge_Code4 = objPI.Add_Charge_Code4
                obj.Add_Charge_Name4 = objPI.Add_Charge_Name4
                obj.Add_Charge_Amt4 = objPI.Add_Charge_Amt4
                obj.Add_Charge_Code5 = objPI.Add_Charge_Code5
                obj.Add_Charge_Name5 = objPI.Add_Charge_Name5
                obj.Add_Charge_Amt5 = objPI.Add_Charge_Amt5
                obj.Add_Charge_Code6 = objPI.Add_Charge_Code6
                obj.Add_Charge_Name6 = objPI.Add_Charge_Name6
                obj.Add_Charge_Amt6 = objPI.Add_Charge_Amt6
                obj.Add_Charge_Code7 = objPI.Add_Charge_Code7
                obj.Add_Charge_Name7 = objPI.Add_Charge_Name7
                obj.Add_Charge_Amt7 = objPI.Add_Charge_Amt7
                obj.Add_Charge_Code8 = objPI.Add_Charge_Code8
                obj.Add_Charge_Name8 = objPI.Add_Charge_Name8
                obj.Add_Charge_Amt8 = objPI.Add_Charge_Amt8
                obj.Add_Charge_Code9 = objPI.Add_Charge_Code9
                obj.Add_Charge_Name9 = objPI.Add_Charge_Name9
                obj.Add_Charge_Amt9 = objPI.Add_Charge_Amt9
                obj.Add_Charge_Code10 = objPI.Add_Charge_Code10
                obj.Add_Charge_Name10 = objPI.Add_Charge_Name10
                obj.Add_Charge_Amt10 = objPI.Add_Charge_Amt10
                obj.Total_Add_Charge = objPI.Total_Add_Charge

                obj.Salesman_Code = objPI.Salesman_Code
                obj.Salesman_Name = objPI.Salesman_Name
                obj.Due_Date = objPI.Due_Date
                obj.CURRENCY_CODE = objPI.CURRENCY_CODE
                obj.ConvRate = objPI.ConvRate
                obj.ApplicableFrom = objPI.ApplicableFrom
                obj.CloseRemarks = ""
                obj.Price_Code = objPI.Price_Code
                obj.Route_No = objPI.Route_No
                obj.Route_Desc = objPI.Route_Desc
                obj.HeadDisc_Per = objPI.HeadDisc_Per
                obj.HeadDisc_Amt = objPI.HeadDisc_Amt
                obj.HeadDisc_PerAmt = objPI.HeadDisc_PerAmt
                obj.TotCashDiscAmt = objPI.TotCashDiscAmt
                obj.Price_Group_Code = objPI.Price_Group_Code
                obj.Cust_PO_No = objPI.Cust_PO_No
                obj.Terms_Code = objPI.Terms_Code
                obj.Commission_Paid = "No"

                obj.Arr_Notify = New List(Of clsEXSalesOrderNotifyParty)
                obj.Arr = New List(Of clsEXSalesOrderDetail)
                obj.arrCustomFields = New List(Of clsCustomFieldValues)

                If objPI.Arr IsNot Nothing AndAlso objPI.Arr.Count > 0 Then
                    For Each objtrPI As clsEXPorformaInvoiceDetail In objPI.Arr
                        objtr = New clsEXSalesOrderDetail()

                        objtr.Line_No = objtrPI.Line_No
                        objtr.Row_Type = objtrPI.Row_Type
                        objtr.Item_Code = objtrPI.Item_Code
                        objtr.Qty = objtrPI.Qty
                        objtr.Quotation_Code = ""
                        objtr.PurchaseOrder_No = ""
                        objtr.Balance_Qty = Nothing
                        objtr.Unit_code = objtrPI.Unit_code
                        objtr.Location = objtrPI.Location
                        objtr.Item_Cost = objtrPI.Item_Cost
                        objtr.Amount = objtrPI.Amount
                        objtr.Disc_Amt = objtrPI.Disc_Amt
                        objtr.Disc_Per = objtrPI.Disc_Per
                        objtr.Amt_Less_Discount = objtrPI.Amt_Less_Discount

                        objtr.TAX1 = objtrPI.TAX1
                        objtr.TAX1_Amt = objtrPI.TAX1_Amt
                        objtr.TAX1_Rate = objtrPI.TAX1_Rate
                        objtr.TAX1_Base_Amt = objtrPI.TAX1_Base_Amt
                        objtr.TAX2 = objtrPI.TAX2
                        objtr.TAX2_Amt = objtrPI.TAX2_Amt
                        objtr.TAX2_Rate = objtrPI.TAX2_Rate
                        objtr.TAX2_Base_Amt = objtrPI.TAX2_Base_Amt
                        objtr.TAX3 = objtrPI.TAX3
                        objtr.TAX3_Amt = objtrPI.TAX3_Amt
                        objtr.TAX3_Rate = objtrPI.TAX3_Rate
                        objtr.TAX3_Base_Amt = objtrPI.TAX3_Base_Amt
                        objtr.TAX4 = objtrPI.TAX4
                        objtr.TAX4_Amt = objtrPI.TAX4_Amt
                        objtr.TAX4_Rate = objtrPI.TAX4_Rate
                        objtr.TAX4_Base_Amt = objtrPI.TAX4_Base_Amt
                        objtr.TAX5 = objtrPI.TAX5
                        objtr.TAX5_Amt = objtrPI.TAX5_Amt
                        objtr.TAX5_Rate = objtrPI.TAX5_Rate
                        objtr.TAX5_Base_Amt = objtrPI.TAX5_Base_Amt
                        objtr.TAX6 = objtrPI.TAX6
                        objtr.TAX6_Amt = objtrPI.TAX6_Amt
                        objtr.TAX6_Rate = objtrPI.TAX6_Rate
                        objtr.TAX6_Base_Amt = objtrPI.TAX6_Base_Amt
                        objtr.TAX7 = objtrPI.TAX7
                        objtr.TAX7_Amt = objtrPI.TAX7_Amt
                        objtr.TAX7_Rate = objtrPI.TAX7_Rate
                        objtr.TAX7_Base_Amt = objtrPI.TAX7_Base_Amt
                        objtr.TAX8 = objtrPI.TAX8
                        objtr.TAX8_Amt = objtrPI.TAX8_Amt
                        objtr.TAX8_Rate = objtrPI.TAX8_Rate
                        objtr.TAX8_Base_Amt = objtrPI.TAX8_Base_Amt
                        objtr.TAX9 = objtrPI.TAX9
                        objtr.TAX9_Amt = objtrPI.TAX9_Amt
                        objtr.TAX9_Rate = objtrPI.TAX9_Rate
                        objtr.TAX9_Base_Amt = objtrPI.TAX9_Base_Amt
                        objtr.TAX10 = objtrPI.TAX10
                        objtr.TAX10_Amt = objtrPI.TAX10_Amt
                        objtr.TAX10_Rate = objtrPI.TAX10_Rate
                        objtr.TAX10_Base_Amt = objtrPI.TAX10_Base_Amt
                        objtr.Total_Tax_Amt = objtrPI.Total_Tax_Amt
                        objtr.Item_Net_Amt = objtrPI.Item_Net_Amt
                        objtr.Specification = objtrPI.Specification
                        objtr.Remarks = objtrPI.Remarks
                        objtr.MRP = objtrPI.MRP
                        objtr.Price_code = objtrPI.Price_code
                        objtr.Scheme_Applicable = objtrPI.Scheme_Applicable
                        objtr.Scheme_Code = objtrPI.Scheme_Code
                        objtr.Scheme_Item = objtrPI.Scheme_Item
                        objtr.Item_Tax = objtrPI.Item_Tax
                        objtr.Total_MRP_Amt = objtrPI.Total_MRP_Amt
                        objtr.Total_Basic_Amt = objtrPI.Total_Basic_Amt
                        objtr.Total_Disc_Amt = objtrPI.Total_Disc_Amt
                        objtr.Cust_Discount = objtrPI.Cust_Discount
                        objtr.Total_Cust_Discount = objtrPI.Total_Cust_Discount
                        objtr.ActualRate = objtrPI.ActualRate
                        objtr.Cust_DiscountQty = objtrPI.Cust_DiscountQty
                        objtr.CustDiscPer = objtrPI.CustDiscPer
                        objtr.Price_code = objtrPI.Price_code
                        objtr.Price_Date = objtrPI.Price_Date
                        objtr.Abatement_Amt = objtrPI.Abatement_Amt
                        objtr.Abatement_Per = objtrPI.Abatement_Per
                        objtr.FOC_Item = objtrPI.FOC_Item
                        objtr.Batch_No = objtrPI.Batch_No
                        objtr.MFG_Date = objtrPI.MFG_Date
                        objtr.Item_Weight = objtrPI.Item_Weight
                        objtr.Conv_Factor = objtrPI.Conv_Factor
                        objtr.TotalItem_Weight = objtrPI.TotalItem_Weight
                        objtr.Markup_On = objtrPI.Markup_On
                        objtr.Markup_Percent = objtrPI.Markup_Percent
                        objtr.Landing_Cost = objtrPI.Landing_Cost
                        objtr.HeadDiscAmt = objtrPI.HeadDiscAmt
                        objtr.HeadDiscPer = objtrPI.HeadDiscPer
                        objtr.HeadDiscPerAmt = objtrPI.HeadDiscPerAmt
                        objtr.Purchase_Cost = objtrPI.Purchase_Cost
                        objtr.OrgRate = objtrPI.OrgRate
                        objtr.PrincipleCode = objtrPI.PrincipleCode
                        objtr.PrincipleDesc = objtrPI.PrincipleDesc
                        objtr.vendor_code = objtrPI.vendor_code
                        objtr.vendor_desc = objtrPI.vendor_desc
                        objtr.Bin_No = objtrPI.Bin_No

                        obj.Arr.Add(objtr)
                    Next
                End If
            End If

            isSaved = isSaved AndAlso obj.SaveData(obj, True, False, trans)
            isSaved = isSaved AndAlso clsEXSalesOrder.PostData(IIf(clsCommon.CompairString(clsUserMgtCode.frmEXPorformaInvoice, FormId) = CompairStringResult.Equal, clsUserMgtCode.frmEXSalesOrder, clsUserMgtCode.frmSalesOrderMT), obj.Document_Code, arrLoc, trans)

            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Update TSPL_EX_PI_HEAD set Against_SO_No='" + obj.Document_Code + "' where Document_Code='" + objPI.Document_Code + "'", trans)
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Update TSPL_EX_PI_DETAIL set Sales_Order_Code='" + obj.Document_Code + "' where Document_Code='" + objPI.Document_Code + "'", trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal strInvoiceType As String, ByVal NavType As NavigatorType, ByVal arrLoc As String, ByVal Export_merchant As String) As clsEXPorformaInvoiceHead
        Try
            Return GetData(strDocumentNo, NavType, strInvoiceType, arrLoc, Export_merchant, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal strInvoiceType As String, ByVal arrLoc As String, ByVal Export_merchant As String, ByVal trans As SqlTransaction) As clsEXPorformaInvoiceHead
        Dim obj As clsEXPorformaInvoiceHead = Nothing
        Try
            Dim qry As String = "SELECT TSPL_EX_PI_HEAD.Is_Taxable,TSPL_EX_PI_HEAD.document_type,TSPL_EX_PI_HEAD.is_Accepted,TSPL_EX_PI_HEAD.Accepted_Date,TSPL_EX_PI_HEAD.is_Partshipment,TSPL_EX_PI_HEAD.is_Transshipment,TSPL_EX_PI_HEAD.bank_code,TSPL_EX_PI_HEAD.branch_code,TSPL_EX_PI_HEAD.Commission_Paid,TSPL_EX_PI_HEAD.Commission_Amount,TSPL_EX_PI_HEAD.Comm_Amt_Type,TSPL_EX_PI_HEAD.Commission_Payee_Code,TSPL_EX_PI_HEAD.Commission_Instruction,TSPL_EX_PI_HEAD.EX_Term_Code,TSPL_EX_PI_HEAD.Payment_Terms,TSPL_EX_PI_HEAD.Advance_Percentage,TSPL_EX_PI_HEAD.Stuffing_Status,TSPL_EX_PI_HEAD.Is_Delivered,TSPL_EX_PI_HEAD.HeadDisc_PerAmt,TSPL_EX_PI_HEAD.cust_po_date,TSPL_EX_PI_HEAD.Cust_PO_No,TSPL_EX_PI_HEAD.Vehicle_Code,TSPL_EX_PI_HEAD.price_group_code,TSPL_EX_PI_HEAD.Invoice_Type,TSPL_EX_PI_HEAD.HeadDisc_Per,TSPL_EX_PI_HEAD.HeadDisc_Amt,TSPL_EX_PI_HEAD.TotCashDiscAmt,TSPL_EX_PI_HEAD.Route_No,TSPL_EX_PI_HEAD.Route_Desc,TSPL_EX_PI_HEAD.Price_Code, TSPL_EX_PI_HEAD.Document_Code,TSPL_EX_PI_HEAD.Document_Date,TSPL_EX_PI_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_EX_PI_HEAD.Status,TSPL_EX_PI_HEAD.On_Hold,TSPL_EX_PI_HEAD.Ref_No,TSPL_EX_PI_HEAD.Airway_Line,TSPL_EX_PI_HEAD.Description,TSPL_EX_PI_HEAD.Remarks,TSPL_EX_PI_HEAD.Tax_Group,TSPL_EX_PI_HEAD.Bill_To_Location,TSPL_EX_PI_HEAD.Ship_To_Location,TSPL_EX_PI_HEAD.TAX1,TSPL_EX_PI_HEAD.TAX1_Rate,TSPL_EX_PI_HEAD.TAX1_Amt,TSPL_EX_PI_HEAD.TAX1_Base_Amt,TSPL_EX_PI_HEAD.TAX2,TSPL_EX_PI_HEAD.TAX2_Rate,TSPL_EX_PI_HEAD.TAX2_Amt,TSPL_EX_PI_HEAD.TAX2_Base_Amt,TSPL_EX_PI_HEAD.TAX3,TSPL_EX_PI_HEAD.TAX3_Rate,TSPL_EX_PI_HEAD.TAX3_Amt,TSPL_EX_PI_HEAD.TAX3_Base_Amt,TSPL_EX_PI_HEAD.TAX4,TSPL_EX_PI_HEAD.TAX4_Rate,TSPL_EX_PI_HEAD.TAX4_Amt,TSPL_EX_PI_HEAD.TAX4_Base_Amt,TSPL_EX_PI_HEAD.TAX5,TSPL_EX_PI_HEAD.TAX5_Rate,TSPL_EX_PI_HEAD.TAX5_Amt,TSPL_EX_PI_HEAD.TAX5_Base_Amt,TSPL_EX_PI_HEAD.TAX6,TSPL_EX_PI_HEAD.TAX6_Rate,TSPL_EX_PI_HEAD.TAX6_Amt,TSPL_EX_PI_HEAD.TAX6_Base_Amt,TSPL_EX_PI_HEAD.TAX7,TSPL_EX_PI_HEAD.TAX7_Rate,TSPL_EX_PI_HEAD.TAX7_Amt,TSPL_EX_PI_HEAD.TAX7_Base_Amt,TSPL_EX_PI_HEAD.TAX8,TSPL_EX_PI_HEAD.TAX8_Rate,TSPL_EX_PI_HEAD.TAX8_Amt,TSPL_EX_PI_HEAD.TAX8_Base_Amt,TSPL_EX_PI_HEAD.TAX9,TSPL_EX_PI_HEAD.TAX9_Rate,TSPL_EX_PI_HEAD.TAX9_Amt,TSPL_EX_PI_HEAD.TAX9_Base_Amt,TSPL_EX_PI_HEAD.TAX10,TSPL_EX_PI_HEAD.TAX10_Rate,TSPL_EX_PI_HEAD.TAX10_Amt,TSPL_EX_PI_HEAD.TAX10_Base_Amt,TSPL_EX_PI_HEAD.Discount_Base,TSPL_EX_PI_HEAD.Discount_Amt,TSPL_EX_PI_HEAD.Amount_Less_Discount,TSPL_EX_PI_HEAD.Total_Tax_Amt,TSPL_EX_PI_HEAD.Comments,TSPL_EX_PI_HEAD.Comp_Code,TSPL_EX_PI_HEAD.Terms_Code,TSPL_EX_PI_HEAD.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_EX_PI_HEAD.Posting_Date,TSPL_EX_PI_HEAD.Total_Amt,TSPL_EX_PI_HEAD.Carrier,TSPL_EX_PI_HEAD.VehicleNo,TSPL_EX_PI_HEAD.Import_Export_No as GRNo,TSPL_EX_PI_HEAD.GENo,TSPL_EX_PI_HEAD.GEDate, TSPL_EX_PI_HEAD.Dept,TSPL_EX_PI_HEAD.Dept_Desc,TSPL_EX_PI_HEAD.Item_Type,TSPL_EX_PI_HEAD.Against_SO_No as Against_Shipment_No,TSPL_EX_PI_HEAD.Add_Charge_Code1,TSPL_EX_PI_HEAD.Add_Charge_Name1,TSPL_EX_PI_HEAD.Add_Charge_Amt1,TSPL_EX_PI_HEAD.Add_Charge_Code2,TSPL_EX_PI_HEAD.Add_Charge_Name2,TSPL_EX_PI_HEAD.Add_Charge_Amt2,TSPL_EX_PI_HEAD.Add_Charge_Code3,TSPL_EX_PI_HEAD.Add_Charge_Name3,TSPL_EX_PI_HEAD.Add_Charge_Amt3,TSPL_EX_PI_HEAD.Add_Charge_Code4,TSPL_EX_PI_HEAD.Add_Charge_Name4,TSPL_EX_PI_HEAD.Add_Charge_Amt4,TSPL_EX_PI_HEAD.Add_Charge_Code5,TSPL_EX_PI_HEAD.Add_Charge_Name5,TSPL_EX_PI_HEAD.Add_Charge_Amt5,TSPL_EX_PI_HEAD.Add_Charge_Code6,TSPL_EX_PI_HEAD.Add_Charge_Name6,TSPL_EX_PI_HEAD.Add_Charge_Amt6,TSPL_EX_PI_HEAD.Add_Charge_Code7,TSPL_EX_PI_HEAD.Add_Charge_Name7,TSPL_EX_PI_HEAD.Add_Charge_Amt7,TSPL_EX_PI_HEAD.Add_Charge_Code8,TSPL_EX_PI_HEAD.Add_Charge_Name8,TSPL_EX_PI_HEAD.Add_Charge_Amt8,TSPL_EX_PI_HEAD.Add_Charge_Code9 ,TSPL_EX_PI_HEAD.Add_Charge_Name9,TSPL_EX_PI_HEAD.Add_Charge_Amt9 ,TSPL_EX_PI_HEAD.Add_Charge_Code10 ,TSPL_EX_PI_HEAD.Add_Charge_Name10,TSPL_EX_PI_HEAD.Add_Charge_Amt10,TSPL_EX_PI_HEAD.Total_Add_Charge,TSPL_EX_PI_HEAD.Tax_Calculation_Type,TSPL_EX_PI_HEAD.Challan_No, TSPL_EX_PI_HEAD.Challan_Date, TSPL_EX_PI_HEAD.Inv_Date,TSPL_EX_PI_HEAD.Inv_No,TSPL_EX_PI_HEAD.Is_Internal ,TSPL_EX_PI_HEAD.Is_Create_Auto_Receipt ,TSPL_EX_PI_HEAD.Salesman_Code ,TSPL_EX_PI_HEAD.Salesman_Name, " & _
            " TSPL_EX_PI_HEAD.CURRENCY_CODE,TSPL_EX_PI_HEAD.CONVRATE,TSPL_EX_PI_HEAD.APPLICABLEFROM,Against_C_Form,TSPL_EX_PI_HEAD.PROJECT_ID, TSPL_EX_PI_HEAD.Form_38_No, " & _
             "TSPL_EX_PI_HEAD.Exporter_Ref_No,TSPL_EX_PI_HEAD.Pre_Carriage_By,TSPL_EX_PI_HEAD.Discharge_Port,TSPL_EX_PI_HEAD.Final_Destination,TSPL_EX_PI_HEAD.Origin_Country,TSPL_EX_PI_HEAD.Final_Destination_Country," & _
            " TSPL_EX_PI_HEAD.MT_HS_Classification_No, TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code, TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Desc, TSPL_EX_PI_HEAD.MT_Is_AmountinRs," & _
            " TSPL_EX_PI_HEAD.MT_LC, TSPL_EX_PI_HEAD.MT_CAD, TSPL_EX_PI_HEAD.MT_RETAINED, TSPL_EX_PI_HEAD.MT_CIF, TSPL_EX_PI_HEAD.MT_Balance_Payment, TSPL_EX_PI_HEAD.MT_On_Account," & _
            " TSPL_EX_PI_HEAD.MT_Advance,TSPL_EX_PI_HEAD.Final_Gross_Wt,TSPL_EX_PI_HEAD.Gross_Wt, TSPL_EX_PI_HEAD.MT_Beneficiary_Code, TSPL_EX_PI_HEAD.MT_INCOTERMS, TSPL_EX_PI_HEAD.MT_Beneficiary_Name,TSPL_EX_PI_HEAD.MT_Against_PO_Date,TSPL_EX_PI_HEAD.MT_Against_PO ,TSPL_EX_PI_HEAD.Loading_Port" & _
            " FROM TSPL_EX_PI_HEAD" & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EX_PI_HEAD.Bill_To_Location " & _
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_EX_PI_HEAD.Ship_To_Location " & _
            " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_EX_PI_HEAD.Tax_Group " & _
            " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_EX_PI_HEAD.Terms_Code " & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_EX_PI_HEAD.Customer_Code where 2=2 and TSPL_EX_PI_HEAD.document_type='" + Export_merchant + "' "
            Dim whrCls As String = ""


            Dim strwherecls As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
                strwherecls = FrmMainTranScreen.CustomerPermission()
            End If


            If clsCommon.myLen(arrLoc) > 0 And clsCommon.myLen(strwherecls) > 0 Then
                whrCls = " AND Bill_To_Location in (" + arrLoc + ") and TSPL_EX_PI_HEAD.Customer_Code in (" + strwherecls + ") "
            ElseIf clsCommon.myLen(arrLoc) > 0 Then
                whrCls = " AND Bill_To_Location in (" + arrLoc + ")"
            ElseIf clsCommon.myLen(strwherecls) > 0 Then
                whrCls = " AND TSPL_EX_PI_HEAD.Customer_Code in (" + strwherecls + ")"
            End If
            '-----------------------------------------------------
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_EX_PI_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_EX_PI_HEAD WHERE 1=1 and TSPL_EX_PI_HEAD.document_type='" + Export_merchant + "' " + whrCls + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_EX_PI_HEAD.Document_Code = (select Max(Document_Code) from TSPL_EX_PI_HEAD WHERE 1=1 and TSPL_EX_PI_HEAD.document_type='" + Export_merchant + "' " + whrCls + ")"
                Case NavigatorType.Current
                    qry += " and TSPL_EX_PI_HEAD.Document_Code = '" + strPONo + "'"
                Case NavigatorType.Next
                    qry += " and TSPL_EX_PI_HEAD.Document_Code = (select Min(Document_Code) from TSPL_EX_PI_HEAD where Document_Code>'" + strPONo + "' and 1=1 and TSPL_EX_PI_HEAD.document_type='" + Export_merchant + "' " + whrCls + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_EX_PI_HEAD.Document_Code = (select Max(Document_Code) from TSPL_EX_PI_HEAD where Document_Code<'" + strPONo + "' and 1=1 and TSPL_EX_PI_HEAD.document_type='" + Export_merchant + "' " + whrCls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsEXPorformaInvoiceHead()
                If IsDBNull(dt.Rows(0)("cust_po_date")) = True Then
                    obj.podate = Nothing
                Else
                    obj.podate = clsCommon.GetPrintDate(dt.Rows(0)("cust_po_date"), "dd/MMM/yyyy hh:mm tt")
                End If

                obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("document_type"))
                obj.Is_Delivered = clsCommon.myCdbl(dt.Rows(0)("Is_Delivered"))
                obj.Form_38_No = clsCommon.myCstr(dt.Rows(0)("Form_38_No"))
                obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
                obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
                obj.Price_Group_Code = clsCommon.myCstr(dt.Rows(0)("Price_Group_Code"))
                obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
                obj.Stuffing_Status = clsCommon.myCstr(dt.Rows(0)("Stuffing_Status"))
                obj.Gross_Wt = clsCommon.myCdbl(dt.Rows(0)("Gross_Wt"))
                obj.Final_Gross_Wt = clsCommon.myCdbl(dt.Rows(0)("Final_Gross_Wt"))
                obj.is_Transshipment = clsCommon.myCstr(dt.Rows(0)("is_Transshipment"))
                If clsCommon.myLen(obj.is_Transshipment) <= 0 Then
                    obj.is_Transshipment = "N"
                End If
                obj.is_Partshipment = clsCommon.myCstr(dt.Rows(0)("is_Partshipment"))
                If clsCommon.myLen(obj.is_Partshipment) <= 0 Then
                    obj.is_Partshipment = "N"
                End If
                obj.is_Accepted = clsCommon.myCstr(dt.Rows(0)("is_Accepted"))
                If clsCommon.myLen(obj.is_Accepted) <= 0 Then
                    obj.is_Accepted = "N"
                End If
                If dt.Rows(0)("Accepted_Date") Is DBNull.Value Then
                    obj.Accepted_Date = Nothing
                Else
                    obj.Accepted_Date = clsCommon.myCDate(dt.Rows(0)("Accepted_Date"))
                End If


                obj.BANK_CODE = clsCommon.myCstr(dt.Rows(0)("bank_code"))
                obj.BRANCH_CODE = clsCommon.myCstr(dt.Rows(0)("branch_code"))
                obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
                obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
                obj.Is_Internal = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Internal")) = 1, True, False)
                obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
                obj.Airway_Line = clsCommon.myCstr(dt.Rows(0)("Airway_Line"))
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

                obj.Commission_Paid = clsCommon.myCstr(dt.Rows(0)("Commission_Paid"))
                obj.Commission_Amount = clsCommon.myCdbl(dt.Rows(0)("Commission_Amount"))
                obj.Comm_Amt_Type = clsCommon.myCstr(dt.Rows(0)("Comm_Amt_Type"))
                obj.Commission_Payee_Code = clsCommon.myCstr(dt.Rows(0)("Commission_Payee_Code"))
                obj.Commission_Instruction = clsCommon.myCstr(dt.Rows(0)("Commission_Instruction"))
                obj.EX_Term_Code = clsCommon.myCstr(dt.Rows(0)("EX_Term_Code"))
                obj.Payment_Terms = clsCommon.myCstr(dt.Rows(0)("Payment_Terms"))
                obj.Advance_Percentage = clsCommon.myCdbl(dt.Rows(0)("Advance_Percentage"))

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
                    obj.Challan_Date = Nothing
                Else
                    obj.Challan_Date = clsCommon.GetPrintDate((dt.Rows(0)("Challan_Date")), "dd/MMM/yyyy")
                End If

                If clsCommon.myLen((dt.Rows(0)("Inv_Date"))) <= 0 Then
                    obj.Inv_Date = Nothing
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

                ''richa agarwal 14/04/2015
                obj.MT_HS_Classification_No = clsCommon.myCstr(dt.Rows(0)("MT_HS_Classification_No"))
                obj.MT_Payment_Terms_Group_Code = clsCommon.myCstr(dt.Rows(0)("MT_Payment_Terms_Group_Code"))
                obj.MT_Payment_Terms_Group_Desc = clsCommon.myCstr(dt.Rows(0)("MT_Payment_Terms_Group_Desc"))
                obj.MT_Is_AmountinRs = clsCommon.myCdbl(dt.Rows(0)("MT_Is_AmountinRs"))
                obj.MT_LC = clsCommon.myCdbl(dt.Rows(0)("MT_LC"))
                obj.MT_CAD = clsCommon.myCdbl(dt.Rows(0)("MT_CAD"))
                obj.MT_Advance = clsCommon.myCdbl(dt.Rows(0)("MT_Advance"))
                obj.MT_RETAINED = clsCommon.myCdbl(dt.Rows(0)("MT_RETAINED"))
                obj.MT_CIF = clsCommon.myCdbl(dt.Rows(0)("MT_CIF"))
                obj.MT_Balance_Payment = clsCommon.myCdbl(dt.Rows(0)("MT_Balance_Payment"))
                obj.MT_On_Account = clsCommon.myCdbl(dt.Rows(0)("MT_On_Account"))
                obj.MT_Beneficiary_Code = clsCommon.myCstr(dt.Rows(0)("MT_Beneficiary_Code"))
                obj.MT_Beneficiary_Name = clsCommon.myCstr(dt.Rows(0)("MT_Beneficiary_Name"))
                obj.MT_INCOTERMS = clsCommon.myCstr(dt.Rows(0)("MT_INCOTERMS"))
                If dt.Rows(0)("MT_Against_PO_Date") Is DBNull.Value Then
                    obj.MT_Against_PO_Date = Nothing
                Else
                    obj.MT_Against_PO_Date = clsCommon.myCDate(dt.Rows(0)("MT_Against_PO_Date"))
                End If
                obj.MT_Against_PO = clsCommon.myCstr(dt.Rows(0)("MT_Against_PO"))
                ''-----------------------------

                obj.Loading_Port = clsCommon.myCstr(dt.Rows(0)("Loading_Port"))
                obj.Exporter_Ref_No = clsCommon.myCstr(dt.Rows(0)("Exporter_Ref_No"))
                obj.Pre_Carriage_By = clsCommon.myCstr(dt.Rows(0)("Pre_Carriage_By"))
                obj.Discharge_Port = clsCommon.myCstr(dt.Rows(0)("Discharge_Port"))
                obj.Final_Destination_Country = clsCommon.myCstr(dt.Rows(0)("Final_Destination_Country"))
                obj.Final_Destination = clsCommon.myCstr(dt.Rows(0)("Final_Destination"))
                obj.Origin_Country = clsCommon.myCstr(dt.Rows(0)("Origin_Country"))

                qry = "SELECT  TSPL_EX_PI_DETAIL.Packing_Inst,TSPL_EX_PI_DETAIL.Shipping_Mark,TSPL_EX_PI_DETAIL.Is_Mannual_Amt,TSPL_EX_PI_DETAIL.Document_Code,TSPL_EX_PI_DETAIL.Line_No, " & _
                "TSPL_EX_PI_DETAIL.Status,TSPL_EX_PI_DETAIL.Row_Type,TSPL_EX_PI_DETAIL.status, " & _
                "TSPL_EX_PI_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_EX_PI_DETAIL.Qty, " & _
                "TSPL_EX_PI_DETAIL.Free_Qty,TSPL_EX_PI_DETAIL.Sales_Order_Code as Shipment_Code, " & _
                "TSPL_EX_PI_DETAIL.Balance_Qty,TSPL_EX_PI_DETAIL.Unit_code,TSPL_EX_PI_DETAIL.Location, " & _
                "TSPL_EX_PI_DETAIL.Item_Cost,TSPL_EX_PI_DETAIL.TAX1,TSPL_EX_PI_DETAIL.TAX1_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX1_Amt,TSPL_EX_PI_DETAIL.TAX2,TSPL_EX_PI_DETAIL.TAX2_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX2_Amt,TSPL_EX_PI_DETAIL.TAX3,TSPL_EX_PI_DETAIL.TAX3_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX3_Amt,TSPL_EX_PI_DETAIL.TAX4,TSPL_EX_PI_DETAIL.TAX4_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX4_Amt,TSPL_EX_PI_DETAIL.TAX5,TSPL_EX_PI_DETAIL.TAX5_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX5_Amt,TSPL_EX_PI_DETAIL.TAX6,TSPL_EX_PI_DETAIL.TAX6_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX6_Amt,TSPL_EX_PI_DETAIL.TAX7,TSPL_EX_PI_DETAIL.TAX7_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX7_Amt,TSPL_EX_PI_DETAIL.TAX8,TSPL_EX_PI_DETAIL.TAX8_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX8_Amt,TSPL_EX_PI_DETAIL.TAX9,TSPL_EX_PI_DETAIL.TAX9_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX9_Amt,TSPL_EX_PI_DETAIL.TAX10,TSPL_EX_PI_DETAIL.TAX10_Rate, " & _
                "TSPL_EX_PI_DETAIL.TAX10_Amt,TSPL_EX_PI_DETAIL.Amount,TSPL_EX_PI_DETAIL.Disc_Per, " & _
                "TSPL_EX_PI_DETAIL.Disc_Amt,TSPL_EX_PI_DETAIL.Amt_Less_Discount,TSPL_EX_PI_DETAIL.Total_Tax_Amt, " & _
                "TSPL_EX_PI_DETAIL.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_EX_PI_DETAIL.TAX1_Base_Amt, " & _
                "TSPL_EX_PI_DETAIL.TAX2_Base_Amt,TSPL_EX_PI_DETAIL.TAX3_Base_Amt,TSPL_EX_PI_DETAIL.TAX4_Base_Amt, " & _
                "TSPL_EX_PI_DETAIL.TAX5_Base_Amt,TSPL_EX_PI_DETAIL.TAX6_Base_Amt,TSPL_EX_PI_DETAIL.TAX7_Base_Amt, " & _
                "TSPL_EX_PI_DETAIL.TAX8_Base_Amt,TSPL_EX_PI_DETAIL.TAX9_Base_Amt,TSPL_EX_PI_DETAIL.TAX10_Base_Amt, " & _
                "TSPL_EX_PI_DETAIL.MRP,TSPL_EX_PI_DETAIL.Batch_No,TSPL_EX_PI_DETAIL.MFG_Date, " & _
                "TSPL_EX_PI_DETAIL.Expiry_Date,TSPL_EX_PI_DETAIL.Specification,TSPL_EX_PI_DETAIL.Remarks, " & _
                "TSPL_EX_PI_DETAIL.Assessable,TSPL_EX_PI_DETAIL.AssessableAmt," & _
                "TSPL_EX_PI_DETAIL.Scheme_Applicable,TSPL_EX_PI_DETAIL.Scheme_Code, " & _
                "TSPL_EX_PI_DETAIL.Scheme_Item,TSPL_EX_PI_DETAIL.Item_Tax,TSPL_EX_PI_DETAIL.Total_MRP_Amt, " & _
                "TSPL_EX_PI_DETAIL.Total_Basic_Amt,TSPL_EX_PI_DETAIL.Total_Disc_Amt,TSPL_EX_PI_DETAIL.Cust_Discount, " & _
                "TSPL_EX_PI_DETAIL.Total_Cust_Discount,TSPL_EX_PI_DETAIL.ActualRate,TSPL_EX_PI_DETAIL.Cust_DiscountQty, " & _
                "TSPL_EX_PI_DETAIL.Price_code,TSPL_EX_PI_DETAIL.Abatement_Per,TSPL_EX_PI_DETAIL.Abatement_Amt, " & _
                "TSPL_EX_PI_DETAIL.FOC_Item,TSPL_EX_PI_DETAIL.Item_Weight,TSPL_EX_PI_DETAIL.Price_Date, " & _
                "TSPL_EX_PI_DETAIL.TotalItem_Weight,TSPL_EX_PI_DETAIL.Conv_Factor,TSPL_EX_PI_DETAIL.Purchase_Cost,TSPL_EX_PI_DETAIL.OrgRate,  " & _
                "TSPL_EX_PI_DETAIL.HeadDiscPer,TSPL_EX_PI_DETAIL.HeadDiscPerAmt,TSPL_EX_PI_DETAIL.Bin_No,TSPL_EX_PI_DETAIL.vendor_code,TSPL_EX_PI_DETAIL.vendor_desc,TSPL_EX_PI_DETAIL.PrincipleCode,TSPL_EX_PI_DETAIL.PrincipleDesc,TSPL_EX_PI_DETAIL.Markup_On,TSPL_EX_PI_DETAIL.Markup_Percent,TSPL_EX_PI_DETAIL.Landing_Cost,TSPL_EX_PI_DETAIL.HeadDiscAmt,TSPL_EX_PI_DETAIL.CustDiscPer,TSPL_EX_PI_DETAIL.CasdDiscScheme_Code "
                qry += " FROM TSPL_EX_PI_DETAIL "
                qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EX_PI_DETAIL.Location "
                qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_EX_PI_DETAIL.Item_Code"
                qry += " where TSPL_EX_PI_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_EX_PI_DETAIL.Line_No asc"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Arr = New List(Of clsEXPorformaInvoiceDetail)
                    Dim objTr As clsEXPorformaInvoiceDetail
                    For Each dr As DataRow In dt.Rows
                        objTr = New clsEXPorformaInvoiceDetail
                        objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                        objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                        objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                        objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objTr.Qty = clsCommon.myCdbl(dr("Qty"))


                        objTr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))
                        objTr.Shipment_Code = clsCommon.myCstr(dr("Shipment_Code"))
                        objTr.Shipping_Mark = clsCommon.myCstr(dr("shipping_mark"))
                        objTr.Packing_Inst = clsCommon.myCstr(dr("Packing_Inst"))
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
                        If IsDBNull(dt.Rows(0)("Price_Date")) = True Then
                            objTr.Price_Date = Nothing
                        Else
                            objTr.Price_Date = clsCommon.GetPrintDate(dt.Rows(0)("Price_Date"), "dd/MMM/yyyy")
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

                qry = "select TSPL_EX_NOTIFY_PARTY_DETAIL.*,tspl_customer_master.customer_name,TSPL_NOTIFY_PARTY_SHIP_DETAIL.add1,TSPL_NOTIFY_PARTY_SHIP_DETAIL.add2,TSPL_NOTIFY_PARTY_SHIP_DETAIL.add3,TSPL_NOTIFY_PARTY_SHIP_DETAIL.country_code,TSPL_NOTIFY_PARTY_SHIP_DETAIL.city_code,TSPL_NOTIFY_PARTY_SHIP_DETAIL.state_code from TSPL_EX_NOTIFY_PARTY_DETAIL left outer join TSPL_NOTIFY_PARTY_SHIP_DETAIL on TSPL_NOTIFY_PARTY_SHIP_DETAIL.cust_code=TSPL_EX_NOTIFY_PARTY_DETAIL.cust_code left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_EX_NOTIFY_PARTY_DETAIL.cust_code where document_code='" + obj.Document_Code + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                obj.Arr_Notify = New List(Of clsEXPorformaInvoiceNotifyDetail)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsEXPorformaInvoiceNotifyDetail()

                        objtr.add1 = clsCommon.myCstr(dr("add1"))
                        objtr.add2 = clsCommon.myCstr(dr("add2"))
                        objtr.add3 = clsCommon.myCstr(dr("add3"))
                        objtr.city = clsCommon.myCstr(dr("city_code"))
                        objtr.country = clsCommon.myCstr(dr("country_code"))
                        objtr.Cust_code = clsCommon.myCstr(dr("cust_code"))
                        objtr.cust_Name = clsCommon.myCstr(dr("customer_name"))
                        objtr.lineno = clsCommon.myCstr(dr("line_no"))
                        objtr.Location_Code = clsCommon.myCstr(dr("location_code"))
                        objtr.state = clsCommon.myCstr(dr("state_code"))

                        obj.Arr_Notify.Add(objtr)
                    Next
                End If

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetInvoiceBalanceAmt(ByVal strSaleInvoiceNo As String, ByVal trans As SqlTransaction) As Decimal
        Try
            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select TSPL_Customer_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No)+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=Against_Sale_No)) from TSPL_Customer_Invoice_Head WHERE Against_Sale_No='" & strSaleInvoiceNo & "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Try
            isSaved = isSaved AndAlso PostData(FormId, strDocNo, arrLoc, trans)

            '==============auto SO in case when PI made without SO
            Dim obj As clsEXPorformaInvoiceHead = clsEXPorformaInvoiceHead.GetData(strDocNo, NavigatorType.Current, "", arrLoc, IIf(clsCommon.CompairString(FormId, clsUserMgtCode.frmEXPorformaInvoice) = CompairStringResult.Equal, "EX", "MT"), trans)
            If clsCommon.myLen(obj.Against_Shipment_No) <= 0 Then
                SaveEx_SO(obj, arrLoc, FormId, trans)
            End If

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsEXPorformaInvoiceHead = clsEXPorformaInvoiceHead.GetData(strDocNo, NavigatorType.Current, "", arrLoc, IIf(clsCommon.CompairString(FormId, clsUserMgtCode.frmEXPorformaInvoice) = CompairStringResult.Equal, "EX", "MT"), trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Transaction " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""

            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_EX_PI_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                Return False
            End If

            qry = "Update TSPL_EX_PI_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "',Modify_By='" + objCommonVar.CurrentUserCode + "',modify_date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "' "
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal FormID As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Try
            isSaved = isSaved AndAlso DeleteData(strCode, FormID, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal FormID As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Export PI No not found to Delete")
            End If

            Dim qry As String = "delete from TSPL_EX_PI_DETAIL where Document_Code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_NOTIFY_PARTY_DETAIL where Document_Code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_PI_HEAD where Document_Code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(FormID, strCode, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function IsValidCustomer(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_EX_PI_HEAD.Document_Code,TSPL_EX_PI_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_EX_PI_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_EX_PI_HEAD.Customer_Code where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Customer_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PI No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Customer Code: " + clsCommon.myCstr(dr("Customer_Code")) + " Customer Name:" + clsCommon.myCstr(dr("Customer_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function IsValidDocumentType(ByVal Arr As List(Of String), ByVal strDocType As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_EX_PI_HEAD.Document_Code,(case when TSPL_EX_PI_HEAD.document_type='MT' then 'Merchant Trade' else 'Export Sale' end) as Customer_Code from TSPL_EX_PI_HEAD where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and document_type not in ('" + strDocType + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PI No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Document Type: " + clsCommon.myCstr(dr("Customer_Code")) + ""
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
    '         "select TSPL_EX_PI_HEAD.Document_Code,TSPL_EX_PI_HEAD .Item_Type as ItemType," & _
    '         "(replace( CONVERT(varchar(11), TSPL_EX_PI_HEAD.Document_Date,104),'.','/')+' '+CONVERT(varchar(100),TSPL_EX_PI_HEAD.Document_Date,108) )as MRN_Date,TSPL_EX_PI_HEAD.Customer_Name,TSPL_EX_PI_HEAD.GRNo,TSPL_EX_PI_HEAD.GENo," & _
    '         "(case when LEN(TSPL_EX_PI_HEAD.GEDate)>0  then REPLACE( CONVERT(varchar(11), TSPL_EX_PI_HEAD.GEDate,104),'.','/') else '' end) as GEDate,TSPL_EX_PI_HEAD.VehicleNo,TSPL_EX_PI_HEAD.Remarks ,TSPL_EX_PI_HEAD.Ref_No,TSPL_EX_PI_DETAIL.Item_Code,TSPL_EX_PI_DETAIL.Item_Desc,TSPL_EX_PI_DETAIL.Unit_code," & _
    '         "case when Unit_code='FC' then Qty + ISNULL( Free_Qty,0) end as FCS, " & _
    '         "case when Unit_code='FB' then Qty + ISNULL( Free_Qty,0) end as FBS, " & _
    '         "case when Unit_code='SH' then Qty + ISNULL( Free_Qty,0) end as FSH, " & _
    '         "case when Unit_code='EC' then Qty + ISNULL( Free_Qty,0) end as ECS," & _
    '         "case when Unit_code='EB' then Qty + ISNULL( Free_Qty,0) end as EBS, " & _
    '         "TSPL_EX_PI_DETAIL.Leak_Qty,TSPL_EX_PI_DETAIL.Burst_Qty,TSPL_EX_PI_DETAIL.Short_Qty from TSPL_EX_PI_DETAIL left outer join TSPL_EX_PI_HEAD on TSPL_EX_PI_HEAD.Document_Code= TSPL_EX_PI_DETAIL.Document_Code " & _
    '         " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_EX_PI_HEAD.Bill_To_Location   where Item_Type ='F'"
    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    qry += " and Convert(date,TSPL_EX_PI_HEAD.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_EX_PI_HEAD.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "
    '                End If

    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    qry += " and TSPL_EX_PI_HEAD.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    qry += " and TSPL_EX_PI_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")" 'ADDED BY ABHISHEK AS ON 30 AUG 2012
    '                End If
    '                qry += " )xxx group by Document_Code,Item_Code order by Item_Desc"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    common.clsCommon.MyMessageBoxShow("No Record Found")
    '                Else
    '                    PurchaseOrderViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "rptSRNCustomReport", "SRN Report")

    '                End If
    '            Else ''For RM Other Print out
    '                Dim strquery As String = "SELECT TSPL_EX_PI_HEAD.Document_Code, TSPL_EX_PI_HEAD.Document_Date,TSPL_EX_PI_HEAD.Customer_Name,(case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else Document_Date end ) as Challan_Date, TSPL_EX_PI_HEAD.Ref_No  " & _
    '                      "as Challan_No, TSPL_EX_PI_HEAD.Inv_No, TSPL_EX_PI_HEAD.Inv_Date, TSPL_EX_PI_HEAD.GRNo,TSPL_EX_PI_HEAD.Amount_Less_Discount ,TSPL_EX_PI_HEAD.GENo,TSPL_EX_PI_HEAD.Total_Amt, " & _
    '                      "TSPL_EX_PI_HEAD.GEDate, TSPL_EX_PI_HEAD.VehicleNo, TSPL_EX_PI_HEAD.Carrier,TSPL_EX_PI_HEAD.Remarks,TSPL_EX_PI_DETAIL.Landed_Cost_Rate,TSPL_EX_PI_DETAIL.Landed_Cost_Amount , TSPL_EX_PI_DETAIL.Item_Code,TSPL_EX_PI_DETAIL.Row_Type,TSPL_EX_PI_DETAIL.Amt_Less_Discount," & _
    '"TSPL_EX_PI_DETAIL.Item_Cost as basicRate,TSPL_EX_PI_DETAIL.Item_Net_Amt as BasicTotal,TSPL_EX_PI_DETAIL.Unit_Cost_Tax_Rate as UCTR," & _
    '"TSPL_EX_PI_DETAIL.Unit_Cost_Tax as uctax,TSPL_EX_PI_DETAIL.Item_Desc,TSPL_EX_PI_DETAIL.Unit_code,TSPL_EX_PI_DETAIL.Qty,TSPL_EX_PI_DETAIL.Rejected_Qty,TSPL_EX_PI_HEAD.Customer_Code,TSPL_EX_PI_HEAD.Total_Amt,TSPL_EX_PI_DETAIL.ITEM_COST," & _
    ' "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " & _
    '"tax1.Tax_Code_Desc as tax1name,isnull (TSPL_EX_PI_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," & _
    '"isnull (TSPL_EX_PI_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_EX_PI_HEAD.tax3_amt,0) as txt3amt," & _
    '"tax4.Tax_Code_Desc as tax4name,isnull (TSPL_EX_PI_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," & _
    '"isnull (TSPL_EX_PI_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_EX_PI_HEAD.tax6_amt,0) as txt6amt " & _
    '",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_EX_PI_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," & _
    '"isnull (TSPL_EX_PI_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_EX_PI_HEAD.tax9_amt,0) as txt9amt," & _
    '"tax10.Tax_Code_Desc as tax10name,isnull (TSPL_EX_PI_HEAD.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname, " & _
    '"TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_EX_PI_DETAIL.Qty," & _
    '"case when tax1.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax1_amt else null end as Tax1Recoverable," & _
    '"case when tax2.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.TAX2_Amt else null end as Tax2Recoverable, " & _
    '"case when tax3.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax3_amt else null end as Tax3Recoverable, " & _
    '"case when tax4.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax4_amt else null end as Tax4Recoverable, " & _
    '"case when tax5.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax5_amt else null end as Tax5Recoverable, " & _
    '"case when tax6.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax6_amt else null end as Tax6Recoverable," & _
    '"case when tax7.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax7_amt else null end as Tax7Recoverable, " & _
    '"case when tax8.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax8_amt else null end as Tax8Recoverable, " & _
    '"case when tax9.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax9_amt else null end as Tax9Recoverable," & _
    '"case when tax10.Tax_Recoverable='Y' then TSPL_EX_PI_HEAD.tax10_amt else null end as Tax10Recoverable, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX1_Rate ,0),103)+'%' as txt1Rate," & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX2_Rate   ,0),103)+'%' as txt2Rate, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX3_Rate  ,0),103)+'%' as txt3Rate, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX4_Rate  ,0),103)+'%' as txt4Rate, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX5_Rate  ,0),103)+'%' as txt5Rate, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX6_Rate  ,0),103)+'%' as txt6Rate, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX7_Rate  ,0),103)+'%' as txt7Rate, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX8_Rate  ,0),103)+'%' as txt8Rate, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX9_Rate  ,0),103)+'%' as txt9Rate, " & _
    '"convert(varchar,isnull (TSPL_EX_PI_HEAD.TAX10_Rate  ,0),103)+'%' as txt10Rate," & _
    '"TSPL_EX_PI_DETAIL.Amt_Less_Discount as Value,(select SUM(rejected_qty) from TSPL_EX_PI_DETAIL where Document_Code=TSPL_EX_PI_HEAD.Document_Code) as Rej_qty, (select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_EX_PI_DETAIL left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_EX_PI_DETAIL.Shipment_Code and TSPL_MRN_DETAIL.Item_Code=TSPL_EX_PI_DETAIL.Item_Code where Document_Code =TSPL_EX_PI_HEAD.Document_Code)as MrnTotQty, (select SUM(Qty) from TSPL_EX_PI_DETAIL where Document_Code=TSPL_EX_PI_HEAD.Document_Code) as SRNQtyTotal, (select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" & _
    '" ( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= TSPL_EX_PI_HEAD.Document_Code " & _
    '" GROUP by TSPL_PI_DETAIL.PI_No)xxx) as PInvNo  ,    " & _
    '       " TSPL_EX_PI_HEAD.Add_Charge_Name1 as Add1Name, " & _
    '     " TSPL_EX_PI_HEAD.Add_Charge_Amt1 as Add1 , " & _
    '     "     TSPL_EX_PI_HEAD.Add_Charge_Name2 as Add2Name, " & _
    '     "   TSPL_EX_PI_HEAD.Add_Charge_Amt2 as Add2 , " & _
    '     "    TSPL_EX_PI_HEAD.Add_Charge_Name3 as Add3Name, " & _
    '     "   TSPL_EX_PI_HEAD.Add_Charge_Amt3 as Add3 , " & _
    '     "    TSPL_EX_PI_HEAD.Add_Charge_Name4 as Add4Name, " & _
    '     "    TSPL_EX_PI_HEAD.Add_Charge_Amt4 as Add4 , " & _
    '     "     TSPL_EX_PI_HEAD.Add_Charge_Name5 as Add5Name, " & _
    '      "     TSPL_EX_PI_HEAD.Add_Charge_Amt5 as Add5 , " & _
    '      "     TSPL_EX_PI_HEAD.Add_Charge_Name6 as Add6Name, " & _
    '      "    TSPL_EX_PI_HEAD.Add_Charge_Amt6 as Add6 , " & _
    '      "    TSPL_EX_PI_HEAD.Add_Charge_Name7 as Add7Name, " & _
    '      "     TSPL_EX_PI_HEAD.Add_Charge_Amt7 as Add7 , " & _
    '      "       TSPL_EX_PI_HEAD.Add_Charge_Name8 as Add8Name, " & _
    '      "      TSPL_EX_PI_HEAD.Add_Charge_Amt8 as Add8 , " & _
    '       "      TSPL_EX_PI_HEAD.Add_Charge_Name9 as Add9Name, " & _
    '       "      TSPL_EX_PI_HEAD.Add_Charge_Amt9 as Add9 , " & _
    '       "      TSPL_EX_PI_HEAD.Add_Charge_Name10 as Add10Name, " & _
    '       "     TSPL_EX_PI_HEAD.Add_Charge_Amt10 as Add10,TSPL_EX_PI_HEAD.Against_RGP,TSPL_EX_PI_DETAIL .Specification   " & _
    ' " FROM  TSPL_EX_PI_DETAIL INNER JOIN TSPL_EX_PI_HEAD ON TSPL_EX_PI_DETAIL.Document_Code = TSPL_EX_PI_HEAD.Document_Code " & _
    ' "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_EX_PI_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " & _
    ' "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_EX_PI_HEAD.Customer_Code = TSPL_VENDOR_MASTER.Customer_Code " & _
    ' "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_EX_PI_HEAD.tax1  " & _
    ' "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_EX_PI_HEAD.tax2 " & _
    ' "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_EX_PI_HEAD .TAX3 " & _
    ' "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_EX_PI_HEAD .tax4 " & _
    ' "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_EX_PI_HEAD .tax5 " & _
    ' "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_EX_PI_HEAD .TAX6  " & _
    ' "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_EX_PI_HEAD .TAX7  " & _
    ' "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_EX_PI_HEAD .TAX8 " & _
    ' "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_EX_PI_HEAD .TAX9 " & _
    ' " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_EX_PI_HEAD .TAX10  " & _
    ' "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_EX_PI_HEAD.Bill_To_Location  " & _
    ' " where TSPL_EX_PI_HEAD .Item_Type not in('F')"

    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    strquery += " and Convert(date,TSPL_EX_PI_HEAD.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_EX_PI_HEAD.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "

    '                End If
    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    strquery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    strquery += " and TSPL_EX_PI_HEAD.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    strquery += " and TSPL_EX_PI_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")  "

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
        Dim isSaved As Boolean = True
        Try
            isSaved = isSaved AndAlso ReverseAndUnpost(strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select Status from TSPL_EX_PI_HEAD where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select count(*) from tspl_sd_sale_invoice_head where Against_PI_No='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(Qry, trans)

            If check > 0 Then
                Throw New Exception("Document cannot be amendmented,is used in Sale Invoice.")
            End If

            Qry = "select count(*) from TSPL_EX_COMMERCIAL_INVOICE_DETAIL where PI_Code='" + strCode + "'"
            check = clsDBFuncationality.getSingleValue(Qry, trans)

            If check > 0 Then
                Throw New Exception("Document cannot be amendmented,is used in Commercial Invoice.")
            End If

            ''richa agarwal 20/04/2015
            Qry = "select count(*) from TSPL_PURCHASE_ORDER_HEAD where MT_PI_No ='" + strCode + "'"
            check = clsDBFuncationality.getSingleValue(Qry, trans)

            If check > 0 Then
                Throw New Exception("Document cannot be amendmented,is used in Purchase Order.")
            End If
            ''-----------------------------

            Qry = "Update TSPL_EX_PI_HEAD set Status = 0 where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsEXPorformaInvoiceDetail
#Region "Variables"
    Public Packing_Inst As String = Nothing
    Public Shipping_Mark As String = Nothing
    Public Document_Code As String = Nothing
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
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsEXPorformaInvoiceDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsEXPorformaInvoiceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)

                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommon.AddColumnsForChange(coll, "Free_qty", obj.Free_Qty)

                clsCommon.AddColumnsForChange(coll, "Sales_Order_Code", obj.Shipment_Code, True)
                clsCommon.AddColumnsForChange(coll, "Shipping_Mark", obj.Shipping_Mark)
                clsCommon.AddColumnsForChange(coll, "Packing_Inst", obj.Packing_Inst)
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EX_PI_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceSRNQty(ByVal strSRNCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_EX_PI_DETAIL.Item_Code as ICode,TSPL_EX_PI_DETAIL.Qty as Qty,1 as RI from TSPL_EX_PI_DETAIL left outer join TSPL_EX_PI_HEAD on TSPL_EX_PI_HEAD.Document_Code=TSPL_EX_PI_DETAIL.Document_Code where TSPL_EX_PI_DETAIL.Status=0 and TSPL_EX_PI_HEAD.Status=1 and TSPL_EX_PI_DETAIL.document_code ='" + strSRNCode + "' and TSPL_EX_PI_DETAIL.Item_Code='" + strICode + "' and  TSPL_EX_PI_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_EX_PI_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_EX_PI_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " & _
            " union all " & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty as Qty,-1 as RI from TSPL_EX_COMMERCIAL_INVOICE_DETAIL left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code where TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code='" + strSRNCode + "'   and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code='" + strICode + "'  and  TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code not in ('" + strCurrPINNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_EX_PI_DETAIL set Status=1 where Document_Code='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class

Public Class clsEXPorformaInvoiceNotifyDetail
#Region "variables"
    Public Cust_code As String = Nothing
    Public cust_Name As String = Nothing
    Public add1 As String = Nothing
    Public add2 As String = Nothing
    Public add3 As String = Nothing
    Public city As String = Nothing
    Public state As String = Nothing
    Public country As String = Nothing
    Public Location_Code As String = Nothing
    Public lineno As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDoc As String, ByVal Arr As List(Of clsEXPorformaInvoiceNotifyDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_EX_NOTIFY_PARTY_DETAIL where document_code='" + strDoc + "'", trans)

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each objtr As clsEXPorformaInvoiceNotifyDetail In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Document_code", strDoc)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.lineno)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", objtr.Cust_code)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EX_NOTIFY_PARTY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class