Imports common
Imports System.Data.SqlClient
Public Class clsEXCommercialInvoiceHead

#Region "Variables"
    Public Payment_Terms As String = Nothing
    Public Document_Type As String = Nothing
    Public Incentive_Declaration As String = Nothing

    Public Loading_Port As String = Nothing
    Public Vessel_Flight_No As String = Nothing
    Public CHA_Code As String = Nothing
    Public CHA_Charge_Amount As Decimal = Nothing
    Public CHA_FOB_Amount As Decimal = Nothing
    Public CHA_Frieght_Kg_Amount As Decimal = Nothing
    Public CHA_Basic_Freight_Amount As Decimal = Nothing
    Public Is_Taxable As Integer = 0
    Public Stuffing_Status As String = Nothing
    Public Exporter_Ref_No As String = Nothing
    Public Pre_Carriage_By As String = Nothing
    Public Discharge_Port As String = Nothing
    Public Final_Destination As String = Nothing
    Public Origin_Country As String = Nothing
    Public Final_Destination_Country As String = Nothing

    Public Is_Delivered As Integer = 0
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
    Public Arr As List(Of clsEXCommercialInvoiceDetail) = Nothing

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
    Public MT_Against_PO_Date As DateTime? = Nothing
    Public MT_Against_PO As String = Nothing
    Public Gross_Wt As Double = 0
    Public Final_Gross_Wt As Double = 0
    Public MT_Against_LC As Integer = 0
    Public DocumentAcceptanceNo As String = Nothing
    Public Arr_Notify As List(Of clsEXCommercialInvoiceNotifyDetail) = Nothing
#End Region

    Public Shared Function GetProformaDescrptn() As String
        Dim str As String = ""
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 comments from TSPL_EX_COMMERCIAL_INVOICE_HEAD where len(isnull(comments,''))>0 order by document_date desc"))

        If clsCommon.myLen(str) <= 0 Then
            str = "1. Marine insurance exclusively covered by the buyer." + Environment.NewLine + "2. The goods remain the property of seller until paid in full." + Environment.NewLine + "3. Bank contract must be sent within 5 working days from the date " + Environment.NewLine + "   of receipt of this Commercial Invoice." + Environment.NewLine + "4. Immediate Delivery without pallet." + Environment.NewLine + "5. Buyer to advise timely if any additional markings/ information is " + Environment.NewLine + "   required." + Environment.NewLine + "6. Please return a copy of this Commercial Invoice duly signed and " + Environment.NewLine + "   stamped for acceptance." + Environment.NewLine + "7. Country of Origin : INDIA"
        End If

        Return str
    End Function

    Public Shared Function GetProformaIncentiveDeclaration() As String
        Dim str As String = ""
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 incentive_declaration from TSPL_EX_COMMERCIAL_INVOICE_HEAD where len(isnull(incentive_declaration,''))>0 order by document_date desc"))

        Return str
    End Function

    Public Function SaveData(ByVal obj As clsEXCommercialInvoiceHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsEXCommercialInvoiceHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try

            Dim qry As String = "delete from TSPL_EX_COMMERCIAL_INVOICE_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Document_Type, "EX") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXCOMMERSIALINVOICE, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXCOMMERSIALINVOICE, clsDocTransactionType.PORawMaterial, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "S") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXCOMMERSIALINVOICE, clsDocTransactionType.POSemiFinishedGoods, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXCOMMERSIALINVOICE, clsDocTransactionType.SNQuotationOther, obj.Bill_To_Location, False)
                    Else
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EXCOMMERSIALINVOICE, clsDocTransactionType.SNOrderExport, obj.Bill_To_Location, False)
                    End If
                Else
                    If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTCOMMERSIALINVOICE, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTCOMMERSIALINVOICE, clsDocTransactionType.PORawMaterial, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "S") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTCOMMERSIALINVOICE, clsDocTransactionType.POSemiFinishedGoods, obj.Bill_To_Location, False)
                    ElseIf clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTCOMMERSIALINVOICE, clsDocTransactionType.SNQuotationOther, obj.Bill_To_Location, False)
                    Else
                        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MTCOMMERSIALINVOICE, clsDocTransactionType.SNOrderExport, obj.Bill_To_Location, False)
                    End If
                End If
            End If

            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
            clsCommon.AddColumnsForChange(coll, "Incentive_Declaration", obj.Incentive_Declaration)
            If IsDBNull(obj.podate) = True Then
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.podate, "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE, True)
            clsCommon.AddColumnsForChange(coll, "BRANCH_CODE", obj.BRANCH_CODE)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Internal", IIf(obj.Is_Internal, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Inv_No", obj.Inv_No)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", obj.Is_Taxable)
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
            clsCommon.AddColumnsForChange(coll, "Against_PI_No", obj.Against_Shipment_No, True)
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

            clsCommon.AddColumnsForChange(coll, "Commission_Paid", obj.Commission_Paid)
            clsCommon.AddColumnsForChange(coll, "Commission_Amount", obj.Commission_Amount)
            clsCommon.AddColumnsForChange(coll, "Comm_Amt_Type", obj.Comm_Amt_Type)
            clsCommon.AddColumnsForChange(coll, "Commission_Payee_Code", obj.Commission_Payee_Code)
            clsCommon.AddColumnsForChange(coll, "Commission_Instruction", obj.Commission_Instruction)
            clsCommon.AddColumnsForChange(coll, "EX_Term_Code", obj.EX_Term_Code)
            clsCommon.AddColumnsForChange(coll, "Payment_Terms", obj.Payment_Terms)
            clsCommon.AddColumnsForChange(coll, "Advance_Percentage", obj.Advance_Percentage)

            clsCommon.AddColumnsForChange(coll, "Loading_Port", obj.Loading_Port)
            clsCommon.AddColumnsForChange(coll, "Vessel_Flight_No", obj.Vessel_Flight_No)
            clsCommon.AddColumnsForChange(coll, "CHA_Code", obj.CHA_Code, True)
            clsCommon.AddColumnsForChange(coll, "CHA_Charge_Amount", obj.CHA_Charge_Amount)
            clsCommon.AddColumnsForChange(coll, "CHA_FOB_Amount", obj.CHA_FOB_Amount)
            clsCommon.AddColumnsForChange(coll, "CHA_Frieght_Kg_Amount", obj.CHA_Frieght_Kg_Amount)
            clsCommon.AddColumnsForChange(coll, "CHA_Basic_Freight_Amount", obj.CHA_Basic_Freight_Amount)
            ''richa agarwal 21/04/2015
            clsCommon.AddColumnsForChange(coll, "MT_Against_PO", obj.MT_Against_PO, True)
            If obj.MT_Against_PO_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "MT_Against_PO_Date", clsCommon.GetPrintDate(obj.MT_Against_PO_Date, "dd/MMM/yyyy hh:mm tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "MT_Against_PO_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "MT_Against_LC", obj.MT_Against_LC)
            clsCommon.AddColumnsForChange(coll, "DocumentAcceptanceNo", obj.DocumentAcceptanceNo, True)
            clsCommon.AddColumnsForChange(coll, "Gross_Wt", obj.Gross_Wt)
            clsCommon.AddColumnsForChange(coll, "Final_Gross_Wt", obj.Final_Gross_Wt)
            ''-----------------------------
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EX_COMMERCIAL_INVOICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EX_COMMERCIAL_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsEXCommercialInvoiceDetail.SaveData(obj.Document_Code, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            isSaved = isSaved AndAlso clsEXCommercialInvoiceNotifyDetail.SaveData(obj.Document_Code, obj.Arr_Notify, trans)

            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_EX_COMMERCIAL_INVOICE_HEAD", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal strInvoiceType As String, ByVal NavType As NavigatorType, ByVal arrLoc As String, ByVal Export_merchant As String) As clsEXCommercialInvoiceHead
        Try
            Return GetData(strDocumentNo, NavType, strInvoiceType, arrLoc, Export_merchant, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal strInvoiceType As String, ByVal arrLoc As String, ByVal Export_merchant As String, ByVal trans As SqlTransaction) As clsEXCommercialInvoiceHead
        Try
            Dim obj As clsEXCommercialInvoiceHead = Nothing

            Dim qry As String = "SELECT TSPL_EX_COMMERCIAL_INVOICE_HEAD.Is_Taxable,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Incentive_Declaration,TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Commission_Paid,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Commission_Amount,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Comm_Amt_Type,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Commission_Payee_Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Commission_Instruction,TSPL_EX_COMMERCIAL_INVOICE_HEAD.EX_Term_Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Payment_Terms,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Advance_Percentage,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Is_Delivered,TSPL_EX_COMMERCIAL_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.cust_po_date,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Cust_PO_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Vehicle_Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.price_group_code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Invoice_Type,TSPL_EX_COMMERCIAL_INVOICE_HEAD.HeadDisc_Per,TSPL_EX_COMMERCIAL_INVOICE_HEAD.HeadDisc_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TotCashDiscAmt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Route_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Route_Desc,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Price_Code, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status,TSPL_EX_COMMERCIAL_INVOICE_HEAD.On_Hold,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Ref_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Description,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Remarks,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Tax_Group,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Bill_To_Location,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Ship_To_Location,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX1,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX1_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX1_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX1_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX2,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX2_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX2_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX2_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX3,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX3_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX3_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX3_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX4,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX4_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX4_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX4_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX5,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX5_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX5_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX5_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX6,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX6_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX6_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX6_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX7,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX7_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX7_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX7_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX8,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX8_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX8_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX8_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX9,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX9_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX9_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX9_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX10,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX10_Rate,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX10_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.TAX10_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Discount_Base,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Discount_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Amount_Less_Discount,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Total_Tax_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Comments,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Comp_Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Terms_Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Posting_Date,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Total_Amt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Carrier,TSPL_EX_COMMERCIAL_INVOICE_HEAD.VehicleNo,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Import_Export_No as GRNo,TSPL_EX_COMMERCIAL_INVOICE_HEAD.GENo,TSPL_EX_COMMERCIAL_INVOICE_HEAD.GEDate, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Dept,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Dept_Desc,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Item_Type,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Against_PI_No as Against_Shipment_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code1,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name1,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt1,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code2,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name2,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt2,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code3,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name3,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt3,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code4,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name4,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt4,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code5,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name5,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt5,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code6,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name6,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt6,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code7,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name7,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt7,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code8,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name8,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt8,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code9 ,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name9,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt9 ,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Code10 ,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Name10,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Add_Charge_Amt10,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Total_Add_Charge,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Tax_Calculation_Type,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Challan_No, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Challan_Date, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Inv_Date,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Inv_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Is_Internal ,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Is_Create_Auto_Receipt ,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Salesman_Code ,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Salesman_Name, "
            qry += " TSPL_EX_COMMERCIAL_INVOICE_HEAD.CURRENCY_CODE,TSPL_EX_COMMERCIAL_INVOICE_HEAD.CONVRATE,TSPL_EX_COMMERCIAL_INVOICE_HEAD.APPLICABLEFROM,Against_C_Form,TSPL_EX_COMMERCIAL_INVOICE_HEAD.PROJECT_ID, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Form_38_No, "
            qry += "TSPL_EX_COMMERCIAL_INVOICE_HEAD.Exporter_Ref_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Pre_Carriage_By,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Discharge_Port,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Final_Destination,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Origin_Country,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Final_Destination_Country,"
            qry += "TSPL_EX_COMMERCIAL_INVOICE_HEAD.Loading_Port,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Vessel_Flight_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.CHA_Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.CHA_Charge_Amount,TSPL_EX_COMMERCIAL_INVOICE_HEAD.CHA_FOB_Amount,TSPL_EX_COMMERCIAL_INVOICE_HEAD.CHA_Frieght_Kg_Amount,TSPL_EX_COMMERCIAL_INVOICE_HEAD.CHA_Basic_Freight_Amount,TSPL_EX_COMMERCIAL_INVOICE_HEAD.BANK_CODE,TSPL_EX_COMMERCIAL_INVOICE_HEAD.MT_Against_PO_Date,TSPL_EX_COMMERCIAL_INVOICE_HEAD.MT_Against_PO,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Final_Gross_Wt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Gross_Wt,TSPL_EX_COMMERCIAL_INVOICE_HEAD.MT_Against_LC,TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo,TSPL_EX_COMMERCIAL_INVOICE_HEAD.BRANCH_CODE"
            qry += "  FROM TSPL_EX_COMMERCIAL_INVOICE_HEAD"
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EX_COMMERCIAL_INVOICE_HEAD.Bill_To_Location "
            qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_EX_COMMERCIAL_INVOICE_HEAD.Ship_To_Location "
            qry += " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_EX_COMMERCIAL_INVOICE_HEAD.Tax_Group "
            qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_EX_COMMERCIAL_INVOICE_HEAD.Terms_Code "
            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code where 2=2 and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='" + Export_merchant + "' "
            Dim whrCls As String = ""


            Dim strwherecls As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
                strwherecls = FrmMainTranScreen.CustomerPermission()
            End If


            If clsCommon.myLen(arrLoc) > 0 And clsCommon.myLen(strwherecls) > 0 Then
                whrCls = " AND Bill_To_Location in (" + arrLoc + ") and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") "
            ElseIf clsCommon.myLen(arrLoc) > 0 Then
                whrCls = " AND Bill_To_Location in (" + arrLoc + ")"
            ElseIf clsCommon.myLen(strwherecls) > 0 Then
                whrCls = " AND TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code in (" + strwherecls + ")"
            End If
            '-----------------------------------------------------
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_EX_COMMERCIAL_INVOICE_HEAD WHERE Invoice_Type in (" & strInvoiceType & ") and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='" + Export_merchant + "' " + whrCls + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code = (select Max(Document_Code) from TSPL_EX_COMMERCIAL_INVOICE_HEAD WHERE Invoice_Type in (" & strInvoiceType & ") and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='" + Export_merchant + "' " + whrCls + ")"
                Case NavigatorType.Current
                    qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code = '" + strPONo + "'"
                Case NavigatorType.Next
                    qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code = (select Min(Document_Code) from TSPL_EX_COMMERCIAL_INVOICE_HEAD where Document_Code>'" + strPONo + "' and Invoice_Type in (" & strInvoiceType & ") and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='" + Export_merchant + "' " + whrCls + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code = (select Max(Document_Code) from TSPL_EX_COMMERCIAL_INVOICE_HEAD where Document_Code<'" + strPONo + "' and Invoice_Type in (" & strInvoiceType & ") and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='" + Export_merchant + "' " + whrCls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsEXCommercialInvoiceHead()
                If IsDBNull(dt.Rows(0)("cust_po_date")) = True Then
                    obj.podate = Nothing
                Else
                    obj.podate = clsCommon.GetPrintDate(dt.Rows(0)("cust_po_date"), "dd/MMM/yyyy hh:mm tt")
                End If

                obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("document_type"))
                obj.Incentive_Declaration = clsCommon.myCstr(dt.Rows(0)("Incentive_Declaration"))
                obj.Is_Delivered = clsCommon.myCdbl(dt.Rows(0)("Is_Delivered"))
                obj.Form_38_No = clsCommon.myCstr(dt.Rows(0)("Form_38_No"))
                obj.BANK_CODE = clsCommon.myCstr(dt.Rows(0)("BANK_CODE"))
                obj.BRANCH_CODE = clsCommon.myCstr(dt.Rows(0)("BRANCH_CODE"))
                obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
                obj.Price_Group_Code = clsCommon.myCstr(dt.Rows(0)("Price_Group_Code"))
                obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
                obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
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

                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
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
                ''richa agarwal 21/04/2015
                If dt.Rows(0)("MT_Against_PO_Date") Is DBNull.Value Then
                    obj.MT_Against_PO_Date = Nothing
                Else
                    obj.MT_Against_PO_Date = clsCommon.myCDate(dt.Rows(0)("MT_Against_PO_Date"))
                End If
                obj.MT_Against_PO = clsCommon.myCstr(dt.Rows(0)("MT_Against_PO"))
                obj.MT_Against_LC = clsCommon.myCdbl(dt.Rows(0)("MT_Against_LC"))
                obj.DocumentAcceptanceNo = clsCommon.myCstr(dt.Rows(0)("DocumentAcceptanceNo"))
                ''-----------------------------
                obj.Gross_Wt = clsCommon.myCdbl(dt.Rows(0)("Gross_Wt"))
                obj.Final_Gross_Wt = clsCommon.myCdbl(dt.Rows(0)("Final_Gross_Wt"))
                obj.Exporter_Ref_No = clsCommon.myCstr(dt.Rows(0)("Exporter_Ref_No"))
                obj.Pre_Carriage_By = clsCommon.myCstr(dt.Rows(0)("Pre_Carriage_By"))
                obj.Discharge_Port = clsCommon.myCstr(dt.Rows(0)("Discharge_Port"))
                obj.Final_Destination_Country = clsCommon.myCstr(dt.Rows(0)("Final_Destination_Country"))
                obj.Final_Destination = clsCommon.myCstr(dt.Rows(0)("Final_Destination"))
                obj.Origin_Country = clsCommon.myCstr(dt.Rows(0)("Origin_Country"))

                obj.Loading_Port = clsCommon.myCstr(dt.Rows(0)("Loading_Port"))
                obj.Vessel_Flight_No = clsCommon.myCstr(dt.Rows(0)("Vessel_Flight_No"))
                obj.CHA_Code = clsCommon.myCstr(dt.Rows(0)("CHA_Code"))
                obj.CHA_Basic_Freight_Amount = clsCommon.myCdbl(dt.Rows(0)("CHA_Basic_Freight_Amount"))
                obj.CHA_Charge_Amount = clsCommon.myCdbl(dt.Rows(0)("CHA_Charge_Amount"))
                obj.CHA_FOB_Amount = clsCommon.myCdbl(dt.Rows(0)("CHA_FOB_Amount"))
                obj.CHA_Frieght_Kg_Amount = clsCommon.myCdbl(dt.Rows(0)("CHA_Frieght_Kg_Amount"))

                qry = "SELECT TSPL_EX_COMMERCIAL_INVOICE_DETAIL.No_Kind_Package,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Container_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Shipping_Mark,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Is_Mannual_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Status,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Row_Type,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.status, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Free_Qty,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code as Shipment_Code,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Balance_Qty,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Location, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Cost,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX1,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX1_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX1_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX2,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX2_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX2_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX3,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX3_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX3_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX4,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX4_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX4_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX5,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX5_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX5_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX6,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX6_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX6_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX7,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX7_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX7_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX8,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX8_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX8_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX9,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX9_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX9_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX10,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX10_Rate, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX10_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Amount,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Disc_Per, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Disc_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Amt_Less_Discount,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Total_Tax_Amt, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX1_Base_Amt, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX2_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX3_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX4_Base_Amt, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX5_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX6_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX7_Base_Amt, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX8_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX9_Base_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TAX10_Base_Amt, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MRP,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Batch_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MFG_Date, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Expiry_Date,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Specification,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Remarks, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.AssessableAmt," & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Scheme_Applicable,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Scheme_Code, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Scheme_Item,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Tax,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Total_MRP_Amt, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Total_Basic_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Total_Disc_Amt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Cust_Discount, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Total_Cust_Discount,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.ActualRate,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Cust_DiscountQty, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Price_code,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Abatement_Per,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Abatement_Amt, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.FOC_Item,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Weight,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Price_Date, " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.TotalItem_Weight,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Conv_Factor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Purchase_Cost,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.OrgRate,  " & _
                "TSPL_EX_COMMERCIAL_INVOICE_DETAIL.HeadDiscPer,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.HeadDiscPerAmt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Packing_Instruction,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Bin_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.vendor_code,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.vendor_desc,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PrincipleCode,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PrincipleDesc,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Markup_On,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Markup_Percent,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Landing_Cost,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.HeadDiscAmt,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.CustDiscPer,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.CasdDiscScheme_Code ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Weight_UOM ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Gross_Item_Weight "
                qry += " FROM TSPL_EX_COMMERCIAL_INVOICE_DETAIL "
                qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Location "
                qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code"
                qry += " where TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No asc"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Arr = New List(Of clsEXCommercialInvoiceDetail)
                    Dim objTr As clsEXCommercialInvoiceDetail
                    For Each dr As DataRow In dt.Rows
                        objTr = New clsEXCommercialInvoiceDetail
                        objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                        objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                        objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                        objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                        objTr.No_Kind_Package = clsCommon.myCstr(dr("No_Kind_Package"))
                        objTr.Container_No = clsCommon.myCstr(dr("Container_No"))

                        objTr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))
                        objTr.Shipment_Code = clsCommon.myCstr(dr("Shipment_Code"))
                        ''richa agarwal 21/04/2015
                        objTr.MT_Against_PO = clsCommon.myCstr(dr("MT_Against_PO"))
                        ''----------------------------
                        objTr.Shipping_Mark = clsCommon.myCstr(dr("shipping_mark"))
                        objTr.Packing_Instruction = clsCommon.myCstr(dr("Packing_Instruction"))
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
                        objTr.Weight_UOM = clsCommon.myCstr(dr("Weight_UOM"))
                        objTr.Gross_Item_Weight = clsCommon.myCdbl(dr("Gross_Item_Weight"))
                        obj.Arr.Add(objTr)
                    Next
                End If

                qry = "select TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL.*,tspl_customer_master.customer_name,TSPL_NOTIFY_PARTY_SHIP_DETAIL.add1,TSPL_NOTIFY_PARTY_SHIP_DETAIL.add2,TSPL_NOTIFY_PARTY_SHIP_DETAIL.add3,TSPL_NOTIFY_PARTY_SHIP_DETAIL.country_code,TSPL_NOTIFY_PARTY_SHIP_DETAIL.city_code,TSPL_NOTIFY_PARTY_SHIP_DETAIL.state_code from TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL left outer join TSPL_NOTIFY_PARTY_SHIP_DETAIL on TSPL_NOTIFY_PARTY_SHIP_DETAIL.cust_code=TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL.cust_code left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL.cust_code where document_code='" + obj.Document_Code + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                obj.Arr_Notify = New List(Of clsEXCommercialInvoiceNotifyDetail)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsEXCommercialInvoiceNotifyDetail()

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

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, arrLoc, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsEXCommercialInvoiceHead = clsEXCommercialInvoiceHead.GetData(strDocNo, NavigatorType.Current, "", arrLoc, IIf(clsCommon.CompairString(FormId, clsUserMgtCode.frmEXCommercialInvoice) = CompairStringResult.Equal, "EX", "MT"), trans)

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

            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_EX_COMMERCIAL_INVOICE_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                Return False
            End If

            qry = "Update TSPL_EX_COMMERCIAL_INVOICE_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "',modify_date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "'"
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    '==============added by preeti gupta against ticket no[TEC/23/05/18-000254]
    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Document_Code As String, ByVal Document_Type As String) As Boolean

        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As clsEXCommercialInvoiceHead = clsEXCommercialInvoiceHead.GetData(Document_Code, NavigatorType.Current, "", "", Document_Type, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0 Then
                Throw New Exception("Document- " & Document_Code & " not found")
            End If

            '' transfer data into cancel table

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Document_Code, "TSPL_EX_COMMERCIAL_INVOICE_HEAD", "Document_Code", "TSPL_EX_COMMERCIAL_INVOICE_DETAIL", "Document_Code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Document_Code, "TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL", "Document_Code", trans)


            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No='" & Document_Code & "'"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If

            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Document_Code, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)



            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Document_Code & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Document_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_COMMERCIAL_INVOICE_DETAIL where Document_Code='" & Document_Code & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL where Document_Code='" & Document_Code & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_COMMERCIAL_INVOICE_HEAD where Document_Code='" & Document_Code & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Document_Code & "' and Program_Code='" & Form_Id & "'"
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

    Public Shared Function DeleteData(ByVal strCode As String, ByVal FormId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(strCode, FormId, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Invoice No not found to Delete")
            End If

            Dim qry As String = "delete from TSPL_EX_COMMERCIAL_INVOICE_DETAIL where Document_Code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL where Document_Code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EX_COMMERCIAL_INVOICE_HEAD where Document_Code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(FormId, strCode, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function IsValidCustomer(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_EX_COMMERCIAL_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Customer_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "Commercial Invoice No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Customer Code: " + clsCommon.myCstr(dr("Customer_Code")) + " Customer Name:" + clsCommon.myCstr(dr("Customer_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function IsValidDocumentType(ByVal Arr As List(Of String), ByVal strDocType As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code,(case when document_type='MT' then 'Merchant Trade' else 'Export Sale' end) as Customer_Code from TSPL_EX_COMMERCIAL_INVOICE_HEAD where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and document_type not in ('" + strDocType + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "Commercial Invoice No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Document Type: " + clsCommon.myCstr(dr("Customer_Code")) + ""
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select count(*) from tspl_sd_sale_invoice_head where Against_Com_Inv_No='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(Qry, trans)

            If check > 0 Then
                Throw New Exception("Document cannot be amendmented,it is used in Sale Invoice.")
            End If

            Qry = "select Status from TSPL_EX_COMMERCIAL_INVOICE_HEAD where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "Update TSPL_EX_COMMERCIAL_INVOICE_HEAD set Status = 0 where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsEXCommercialInvoiceDetail
#Region "Variables"
    Public Shipping_Mark As String = Nothing
    Public MT_Against_PO As String = Nothing
    Public Packing_Instruction As String = Nothing
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
    Public No_Kind_Package As String = Nothing
    Public Container_No As String = Nothing
    Public Weight_UOM As String = Nothing
    Public Gross_Item_Weight As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsEXCommercialInvoiceDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsEXCommercialInvoiceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)

                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommon.AddColumnsForChange(coll, "Free_qty", obj.Free_Qty)

                clsCommon.AddColumnsForChange(coll, "PI_Code", obj.Shipment_Code, True)
                ''richa agarwal 21/04/2015
                clsCommon.AddColumnsForChange(coll, "MT_Against_PO", obj.MT_Against_PO, True)
                ''------------------------------
                clsCommon.AddColumnsForChange(coll, "Shipping_Mark", obj.Shipping_Mark)
                clsCommon.AddColumnsForChange(coll, "Packing_Instruction", obj.Packing_Instruction)
                clsCommon.AddColumnsForChange(coll, "No_Kind_Package", obj.No_Kind_Package)
                clsCommon.AddColumnsForChange(coll, "Container_No", obj.Container_No)
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
                clsCommon.AddColumnsForChange(coll, "Weight_UOM", obj.Weight_UOM)
                clsCommon.AddColumnsForChange(coll, "Gross_Item_Weight", obj.Gross_Item_Weight)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EX_COMMERCIAL_INVOICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceSRNQty(ByVal strSRNCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty as Qty,1 as RI from TSPL_EX_COMMERCIAL_INVOICE_DETAIL left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code where TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Status=0 and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=1 and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code ='" + strSRNCode + "' and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code='" + strICode + "' and  TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " & _
            " union all " & _
            " select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as ICode,TSPL_SD_SALE_INVOICE_DETAIL.Qty as Qty,-1 as RI from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code where TSPL_SD_SALE_INVOICE_DETAIL.Commercial_Inv_No='" + strSRNCode + "'   and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code='" + strICode + "'  and  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SD_SALE_INVOICE_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SD_SALE_INVOICE_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_SD_SALE_INVOICE_DETAIL.document_code not in ('" + strCurrPINNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    ''richa agarwal 27/04/2015
    Public Shared Function GetBalancePOQtyWithoutLC(ByVal strPOCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            "  select TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,1 as RI from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  ='" + strPOCode + "'  and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + strICode + "' and  TSPL_PURCHASE_ORDER_DETAIL.Unit_code='" + strUOM + "' " & _
            " union all " & _
            "   select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty as Qty,-1 as RI from TSPL_EX_COMMERCIAL_INVOICE_DETAIL left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code where TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO ='" + strPOCode + "'   and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code='" + strICode + "'  and  TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code='" + strUOM + "' and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code not  in ('" + strCurrPINNo + "') " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function GetBalancePOQtyWithLC(ByVal strPOCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double, ByVal strDocumentAcceptance As String) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from( " & _
            " select TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code as ICode,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Qty,1 as RI from TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_MT  on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo =TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted =1 and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ='" + strDocumentAcceptance + "' and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.PurchaseOrder_No ='" + strPOCode + "'  and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code='" + strICode + "' and  TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code='" + strUOM + "' " & _
        " union all " & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty as Qty,-1 as RI from TSPL_EX_COMMERCIAL_INVOICE_DETAIL left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code where TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO ='" + strPOCode + "'   and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code='" + strICode + "'  and  TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code='" + strUOM + "' and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code not in ('" + strCurrPINNo + "')" & _
         " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function GetBalancePIQtyWithoutLC(ByVal strPICode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
         "  select TSPL_EX_PI_DETAIL.Item_Code as ICode,TSPL_EX_PI_DETAIL.Qty as Qty,1 as RI from TSPL_EX_PI_DETAIL left outer join TSPL_EX_PI_HEAD on TSPL_EX_PI_HEAD.Document_Code=TSPL_EX_PI_DETAIL.Document_Code where TSPL_EX_PI_DETAIL.Status=0 and TSPL_EX_PI_HEAD.Status=1 and TSPL_EX_PI_DETAIL.document_code ='" + strPICode + "' and TSPL_EX_PI_DETAIL.Item_Code='" + strICode + "' and  TSPL_EX_PI_DETAIL.Unit_code='" + strUOM + "' " & _
         " union all " & _
         "   select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty as Qty,-1 as RI from TSPL_EX_COMMERCIAL_INVOICE_DETAIL left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code where TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code ='" + strPICode + "'   and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code='" + strICode + "'  and  TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code='" + strUOM + "' and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code not  in ('" + strCurrPINNo + "') " & _
         " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function GetBalancePIQtyWithLC(ByVal strPICode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double, ByVal strDocumentAcceptance As String) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from( " & _
             " select TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code as ICode,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Qty,1 as RI from TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_MT  on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo =TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted =1 and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ='" + strDocumentAcceptance + "' and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.PurchaseInvoice_No ='" + strPICode + "'  and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code='" + strICode + "' and  TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code='" + strUOM + "' " & _
         " union all " & _
             " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty as Qty,-1 as RI from TSPL_EX_COMMERCIAL_INVOICE_DETAIL left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code where TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code ='" + strPICode + "'   and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code='" + strICode + "'  and  TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code='" + strUOM + "'  and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.document_code not in ('" + strCurrPINNo + "')" & _
          " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    ''-------------------------------------

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_EX_COMMERCIAL_INVOICE_DETAIL set Status=1 where Document_Code='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class

Public Class clsEXCommercialInvoiceNotifyDetail
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

    Public Shared Function SaveData(ByVal strDoc As String, ByVal Arr As List(Of clsEXCommercialInvoiceNotifyDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL where document_code='" + strDoc + "'", trans)

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each objtr As clsEXCommercialInvoiceNotifyDetail In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Document_code", strDoc)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.lineno)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", objtr.Cust_code)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EX_COMMERCIAL_INVOICE_NOTIFY_PARTY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class