'-27/08/2012--Updation By-[Pankaj Kumar]---Applied GL Security While Navigating The Document Finder--Fwd by--Ranjana Mam
'===========BM00000003055,Add Code to Save Auto Debit Note.===================
Imports common
Imports System.Data.SqlClient
Public Class clsMilkTransporterInvoiceMCC
#Region "Variables"
    Public PROJECT_ID As String = Nothing
    Public PI_No As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public PI_Date As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing
    Public Ship_To_Location As String = Nothing
    Public ShipToLocationName As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
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
    Public Total_Tax_Amt As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public PI_Total_Amt As Double = 0
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As String = Nothing
    Public Posting_Date As Date?
    Public Carrier As String = Nothing
    Public GENo As String = Nothing
    Public GEDate As Date? = Nothing
    Public Item_Type As String = Nothing

    Public Against_Requisition As String = Nothing
    Public Against_PO As String = Nothing
    Public Against_GRN As String = Nothing
    Public Against_MRN As String = Nothing
    Public Against_SRN As String = Nothing

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
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public loc_code As String = Nothing
    Public Tot_Empty_Amount As Double = 0
    Public Arr As List(Of clsMilkTransporterInvoiceMCCDetail) = Nothing
    Public objPIRemittance As clsPIRemittance = Nothing
    Public objJVC As clsPJVHead = Nothing
    Public Invdate As Date? = Nothing
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public is_Excise_On_Qty As Boolean = False
    Public AssessableAmt As Decimal = 0

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public Against_C_Form As Boolean = False
    Public IsAbatementPO As Integer = 0
    Public Is_Against_Form As Boolean = False
    Public Against_Form As String = ""
    Public GRNo As String = Nothing
    Public GR_Date As Date?
    Public VehicleNo As String = Nothing
    Public Transporter As String = ""
    Public Transport_Date As Date?
    Public TransporterDesc As String = ""
    Public VehicleDesc As String = ""
    Public LRNo As String = ""
    Public Scheduler_code As String = Nothing
    Public Remit_To As String = Nothing
    Public Expiration_Date As String = Nothing
    Public Expiration_Amount As Double

#End Region

    Public Function SaveData(ByVal obj As clsMilkTransporterInvoiceMCC, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.mbtnPurchaseInvoice, obj.Bill_To_Location, clsCommon.myCDate(obj.PI_Date), trans)

            Dim qry As String = "delete from TSPL_Mcc_Milk_Transport_Invoice_Detail where Doc_No='" + obj.PI_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'qry = "delete from TSPL_PI_REMITTANCE where Document_No='" + obj.PI_No + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                'If clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal Then
                '    obj.PI_No = clsERPFuncationality.GetNextCode(trans, clsDocType.POInvoice, clsDocTransactionType.PORawMaterial, obj.Bill_To_Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                '    obj.PI_No = clsERPFuncationality.GetNextCode(trans, clsDocType.POInvoice, clsDocTransactionType.POFinishedGoods, obj.Bill_To_Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "P") = CompairStringResult.Equal Then
                '    obj.PI_No = clsERPFuncationality.GetNextCode(trans, clsDocType.POInvoice, clsDocTransactionType.POPromotionalItem, obj.Bill_To_Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                '    obj.PI_No = clsERPFuncationality.GetNextCode(trans, clsDocType.POInvoice, clsDocTransactionType.POOther, obj.Bill_To_Location)
                'If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                '    obj.PI_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PI_Date), clsDocType.POInvoice, clsDocTransactionType.POFinishedGoods, obj.Bill_To_Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal Then
                '    obj.PI_No = clsERPFuncationality.GetNextCode(trans, obj.PI_Date, clsDocType.POInvoice, clsDocTransactionType.PORawMaterial, obj.Bill_To_Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "A") = CompairStringResult.Equal Then
                '    obj.PI_No = clsERPFuncationality.GetNextCode(trans, obj.PI_Date, clsDocType.POInvoice, clsDocTransactionType.POAsset, obj.Bill_To_Location)
                'ElseIf clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                obj.PI_No = clsERPFuncationality.GetNextCode(trans, obj.PI_Date, clsDocType.Mcc_TransporterInvoice, "", obj.Bill_To_Location)
                'Else
                '    Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                'End If
            End If
            If (clsCommon.myLen(obj.PI_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.PI_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", obj.Vendor_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
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
            clsCommon.AddColumnsForChange(coll, "PI_Total_Amt", obj.PI_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)

            clsCommon.AddColumnsForChange(coll, "Against_Requisition", obj.Against_Requisition, True)
            clsCommon.AddColumnsForChange(coll, "Against_PO", obj.Against_PO, True)
            clsCommon.AddColumnsForChange(coll, "Against_GRN", obj.Against_GRN, True)
            clsCommon.AddColumnsForChange(coll, "Against_MRN", obj.Against_MRN, True)
            clsCommon.AddColumnsForChange(coll, "Against_SRN", obj.Against_SRN, True)
            clsCommon.AddColumnsForChange(coll, "Tot_Empty_Amount", obj.Tot_Empty_Amount)
            clsCommon.AddColumnsForChange(coll, "Against_C_Form", IIf(obj.Against_C_Form, 1, 0))
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "LR_No", obj.LRNo, True)

            If obj.Invdate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "InvoiceDate", clsCommon.GetPrintDate(obj.Invdate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "InvoiceDate", Nothing, True)
            End If




            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If


            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Carrier", obj.Carrier)
            clsCommon.AddColumnsForChange(coll, "GENo", obj.GENo)

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
            If obj.GEDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GEDate", clsCommon.GetPrintDate(obj.GEDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GEDate", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "is_Excise_On_Qty", IIf(obj.is_Excise_On_Qty, 1, 0))
            clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion
            clsCommon.AddColumnsForChange(coll, "IsAbatementPO", obj.IsAbatementPO)
            clsCommon.AddColumnsForChange(coll, "Scheduler_code", obj.Scheduler_code)
            clsCommon.AddColumnsForChange(coll, "Remit_To", obj.Remit_To, True)
            clsCommon.AddColumnsForChange(coll, "Expiration_Date", obj.Expiration_Date, True)
            clsCommon.AddColumnsForChange(coll, "Expiration_Amount", obj.Expiration_Amount, True)
            clsCommon.AddColumnsForChange(coll, "Is_Against_Form", IIf(obj.Is_Against_Form = True, "Y", "N"))
            If obj.Is_Against_Form = True Then
                clsCommon.AddColumnsForChange(coll, "Against_Form", obj.Against_Form)
            Else
                clsCommon.AddColumnsForChange(coll, "Against_Form", "")
            End If
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
            If clsCommon.myLen(obj.GRNo) > 0 Then
                clsCommon.AddColumnsForChange(coll, "GR_Date", clsCommon.GetPrintDate(obj.GR_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GR_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "Transporter", obj.Transporter)
            clsCommon.AddColumnsForChange(coll, "TransporterDesc", obj.TransporterDesc)
            clsCommon.AddColumnsForChange(coll, "VehicleDesc", obj.VehicleDesc)
            If clsCommon.myLen(obj.Transporter) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Transport_Date", clsCommon.GetPrintDate(obj.Transport_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Transport_Date", Nothing, True)
            End If
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Doc_No", obj.PI_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Milk_Transport_Invoice_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Milk_Transport_Invoice_HEAD", OMInsertOrUpdate.Update, "TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No='" + obj.PI_No + "'", trans)
            End If


            isSaved = isSaved AndAlso clsMilkTransporterInvoiceMCCDetail.SaveData(obj.PI_No, obj.Bill_To_Location, Arr, trans)
            'isSaved = isSaved AndAlso clsPIRemittance.SaveData(obj.objPIRemittance, obj.PI_No, trans)
            'isSaved = isSaved AndAlso objJVC.SaveData(obj.objJVC, obj.PI_No, obj.Bill_To_Location, isNewEntry, trans)
            'isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.PI_No, obj.arrCustomFields, trans)

            'isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Doc_No", obj.PI_No, "TSPL_Mcc_Milk_Transport_Invoice_HEAD", trans)
            ' isSaved = isSaved AndAlso SaveDebitNoteEntry(obj, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    'By Balwinder on 18/12/2014 no need to make debit note.
    'Public Function SaveDebitNoteEntry(ByVal obj As clsMilkTransporterInvoiceMCC, ByVal trans As SqlTransaction)
    '    Dim rejqty As Double = clsDBFuncationality.getSingleValue("select Rejected_Qty from TSPL_SRN_DETAIL where SRN_No='" & obj.Against_SRN & "'", trans)
    '    If rejqty <= 0 Then
    '        Return True
    '    End If
    '    Dim isSaved As Boolean = True
    '    Dim qry As String = ""
    '    Dim objVendorInvHead As New clsVedorInvoiceHead()
    '    Dim RejLoc As Integer = clsDBFuncationality.getSingleValue("select count(Rejected_Location) from TSPL_LOCATION_MASTER where Location_Code='" & obj.Bill_To_Location & "'", trans)
    '    If RejLoc <= 0 Then
    '        Throw New Exception("Rejected Location Not filled of [" + obj.Bill_To_Location + "]")
    '    End If
    '    Dim Rej_loc As String = clsDBFuncationality.getSingleValue("select Rejected_Location from TSPL_LOCATION_MASTER where Location_Code='" & obj.Bill_To_Location & "'", trans)
    '    objVendorInvHead.Document_No = clsDBFuncationality.getSingleValue("select document_no from tspl_vendor_invoice_head where against_Poinvoice_no='" & obj.PI_No & "' and document_type='D'", trans) 'ToBeGenerated
    '    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.Invdate, "dd/MM/yyyy")
    '    objVendorInvHead.Vendor_Code = obj.Vendor_Code
    '    objVendorInvHead.Vendor_Name = obj.Vendor_Name
    '    objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
    '    objVendorInvHead.Invoice_Type = "AP"
    '    objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.Invdate, "dd/MM/yyyy")
    '    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(Rej_loc, trans)
    '    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
    '    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
    '        Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
    '    End If
    '    objVendorInvHead.Document_Type = "D" ''Purchase Return will make Debit Note of PIInvoice
    '    ''objVendorInvHead.PO_Number = obj.p

    '    '' ''added by priti
    '    ''objVendorInvHead.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
    '    ''objVendorInvHead.RefDocNo = txtRefDocNo.Text
    '    '' '' priti ends here
    '    'objVendorInvHead.Order_No = txtOrderNo.Text
    '    objVendorInvHead.Total_Tax = obj.Total_Tax_Amt '* rejqty

    '    objVendorInvHead.On_Hold = False
    '    objVendorInvHead.Description = "Against Purchase Invoice No " + obj.PI_No
    '    objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
    '    objVendorInvHead.Tax_Group = obj.Tax_Group
    '    If (clsCommon.myLen(obj.TAX1) > 0) Then
    '        objVendorInvHead.TAX1 = obj.TAX1
    '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
    '        objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
    '        objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
    '        objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX2) > 0) Then
    '        objVendorInvHead.TAX2 = obj.TAX2
    '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
    '        objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
    '        objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
    '        objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX3) > 0) Then
    '        objVendorInvHead.TAX3 = obj.TAX3
    '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
    '        objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
    '        objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
    '        objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX4) > 0) Then
    '        objVendorInvHead.TAX4 = obj.TAX4
    '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
    '        objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
    '        objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
    '        objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX5) > 0) Then
    '        objVendorInvHead.TAX5 = obj.TAX5
    '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
    '        objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
    '        objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
    '        objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX6) > 0) Then
    '        objVendorInvHead.TAX6 = obj.TAX6
    '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
    '        objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
    '        objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
    '        objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX7) > 0) Then
    '        objVendorInvHead.TAX7 = obj.TAX7
    '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
    '        objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
    '        objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
    '        objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX8) > 0) Then
    '        objVendorInvHead.TAX8 = obj.TAX8
    '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
    '        objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
    '        objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
    '        objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX9) > 0) Then
    '        objVendorInvHead.TAX9 = obj.TAX9
    '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
    '        objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
    '        objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
    '        objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
    '    End If
    '    If (clsCommon.myLen(obj.TAX10) > 0) Then
    '        objVendorInvHead.TAX10 = obj.TAX10
    '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
    '        objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
    '        objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
    '        objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
    '    End If

    '    objVendorInvHead.Terms_Code = obj.Terms_Code
    '    objVendorInvHead.Terms_Description = obj.TermsName
    '    objVendorInvHead.Due_Date = obj.Due_Date
    '    objVendorInvHead.Discount_Base = obj.Discount_Base
    '    objVendorInvHead.Discount_Amount = obj.Discount_Amt
    '    objVendorInvHead.Amount_Less_Discount = obj.Amount_Less_Discount
    '    objVendorInvHead.Document_Total = obj.PI_Total_Amt
    '    objVendorInvHead.Balance_Amt = obj.PI_Total_Amt
    '    objVendorInvHead.Against_POInvoice_No = obj.PI_No
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
    '        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
    '            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
    '        End If
    '    End If
    '    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
    '        Throw New Exception("Please set the vendor payable Account")
    '    End If

    '    'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

    '    'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
    '    'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
    '    'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

    '    'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
    '    'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
    '    'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

    '    'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
    '    'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
    '    'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

    '    'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
    '    'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
    '    'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

    '    'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
    '    'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
    '    'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

    '    'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
    '    'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
    '    'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

    '    'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
    '    'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
    '    'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

    '    'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
    '    'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
    '    'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

    '    'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
    '    'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
    '    'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

    '    'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
    '    'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
    '    'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10

    '    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

    '    objVendorInvHead.Empty_Amount = 0

    '    Dim ii As Integer = 0
    '    For Each objPIDetail As clsMilkTransporterInvoiceMCCDetail In obj.Arr
    '        ''Fill VendorInvoice details Data
    '        ''qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"
    '        qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"

    '        dt = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '            Throw New Exception("Please set Purchase Account set for item " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ")")
    '        End If
    '        ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
    '        ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
    '        ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
    '        ''End If
    '        ''Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
    '        Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Credit_Debit_Note_Clearing"))

    '        strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
    '        Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))



    '        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
    '            objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
    '            objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Bill_To_Location, trans)
    '        End If

    '        ''If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
    '        ''    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, trans))
    '        ''    objVendorInvHead.Empty_Amount += dblVal * objPIDetail.PR_Qty
    '        ''End If

    '        ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
    '        ''    Throw New Exception("Please set Payable Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
    '        ''End If

    '        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
    '        ii = ii + 1
    '        objVendorInvDetail.Detail_Line_No = ii
    '        objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
    '        objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
    '        objVendorInvDetail.Amount = objPIDetail.Amount

    '        objVendorInvDetail.Discount_Per = objPIDetail.Disc_Per
    '        objVendorInvDetail.Discount = objPIDetail.Disc_Amt
    '        objVendorInvDetail.Amount_less_Discount = objPIDetail.Amt_Less_Discount
    '        objVendorInvDetail.TAX1 = objPIDetail.TAX1
    '        objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
    '        objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt
    '        objVendorInvDetail.TAX2 = objPIDetail.TAX2
    '        objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
    '        objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt
    '        objVendorInvDetail.TAX3 = objPIDetail.TAX3
    '        objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
    '        objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt
    '        objVendorInvDetail.TAX4 = objPIDetail.TAX4
    '        objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
    '        objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt
    '        objVendorInvDetail.TAX5 = objPIDetail.TAX5
    '        objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
    '        objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt
    '        objVendorInvDetail.TAX6 = objPIDetail.TAX6
    '        objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
    '        objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt
    '        objVendorInvDetail.TAX7 = objPIDetail.TAX7
    '        objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
    '        objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt
    '        objVendorInvDetail.TAX8 = objPIDetail.TAX8
    '        objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
    '        objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt
    '        objVendorInvDetail.TAX9 = objPIDetail.TAX9
    '        objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
    '        objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt
    '        objVendorInvDetail.TAX10 = objPIDetail.TAX10
    '        objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
    '        objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt
    '        objVendorInvDetail.Total_Tax = objPIDetail.Total_Tax_Amt
    '        objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt
    '        objVendorInvDetail.Landed_Amount = objPIDetail.Landed_Cost_Amount - objPIDetail.Amt_Less_Discount
    '        objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

    '        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
    '            objVendorInvHead.Arr.Add(objVendorInvDetail)
    '        End If
    '        ''End of Fill Vendor Invoice Detail Data
    '    Next



    '    objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
    '    If objVendorInvHead.Empty_Amount > 0 Then
    '        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
    '            Throw New Exception("Please set Inventory Control Empties")
    '        End If
    '        objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
    '    End If

    '    If obj.objPIRemittance IsNot Nothing Then
    '        objVendorInvHead.RemittanceObject = New clsRemittance()
    '        objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
    '        objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
    '        objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
    '        objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
    '        objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
    '        objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
    '        objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
    '        objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
    '        objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
    '        objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
    '        objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
    '        objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
    '        objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
    '        objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
    '        objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
    '        objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
    '        objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
    '        objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
    '        objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
    '        objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
    '        objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
    '        objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
    '        objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
    '        objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
    '        objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
    '        objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
    '        objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
    '        objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
    '        objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
    '        objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
    '        objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
    '        objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS

    '        objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
    '        objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
    '        objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
    '        objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
    '        objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
    '        objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
    '        objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
    '        objVendorInvHead.Balance_Amt = obj.PI_Total_Amt - obj.objPIRemittance.Actual_Total_TDS
    '        objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code
    '    End If
    '    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
    '        Throw New Exception("No GL Account Found For AP Invoice")
    '    End If
    '    isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
    '    isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

    '    Return isSaved
    'End Function

    Public Shared Function UpdateSecondaryInfo(ByVal obj As clsMilkTransporterInvoiceMCC, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.PI_No) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Is_Against_Form", IIf(obj.Is_Against_Form = True, "Y", "N"))
                If obj.Is_Against_Form = True Then
                    clsCommon.AddColumnsForChange(coll, "Against_Form", obj.Against_Form)
                End If
                clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
                If clsCommon.myLen(obj.GRNo) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "GR_Date", clsCommon.GetPrintDate(obj.GR_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "GR_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
                clsCommon.AddColumnsForChange(coll, "Transporter", obj.Transporter)
                clsCommon.AddColumnsForChange(coll, "TransporterDesc", obj.TransporterDesc)
                clsCommon.AddColumnsForChange(coll, "VehicleDesc", obj.VehicleDesc)
                clsCommon.AddColumnsForChange(coll, "LR_No", obj.LRNo)
                If clsCommon.myLen(obj.Transporter) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Transport_Date", clsCommon.GetPrintDate(obj.Transport_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Transport_Date", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Milk_Transport_Invoice_HEAD", OMInsertOrUpdate.Update, "TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No='" + obj.PI_No + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsMilkTransporterInvoiceMCC
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkTransporterInvoiceMCC
        Dim obj As clsMilkTransporterInvoiceMCC = Nothing
        Dim qry As String = "SELECT TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Vendor_Invoice_No,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_Date,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Vendor_Code, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Vendor_Name,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Status,TSPL_Mcc_Milk_Transport_Invoice_HEAD.On_Hold,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Ref_No,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Description,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Remarks, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Tax_Group,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Bill_To_Location,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Ship_To_Location,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX1,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX1_Rate," & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX1_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX1_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX2,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX2_Rate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX2_Amt, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX2_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX3,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX3_Rate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX3_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX3_Base_Amt, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX4,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX4_Rate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX4_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX4_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX5, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX5_Rate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX5_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX5_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX6,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX6_Rate," & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX6_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX6_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX7,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX7_Rate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX7_Amt, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX7_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX8,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX8_Rate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX8_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX8_Base_Amt, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX9,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX9_Rate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX9_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX9_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX10, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX10_Rate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX10_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TAX10_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Discount_Base, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Discount_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Amount_Less_Discount,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Total_Tax_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Comments, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Comp_Code,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Terms_Code,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Due_Date ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.InvoiceDate, " & _
        " TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName, " & _
        " TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName," & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Posting_Date,TSPL_Mcc_Milk_Transport_Invoice_HEAD.PI_Total_Amt,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Carrier,TSPL_Mcc_Milk_Transport_Invoice_HEAD.VehicleNo, TSPL_Mcc_Milk_Transport_Invoice_HEAD.LR_No, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.GRNo, TSPL_Mcc_Milk_Transport_Invoice_HEAD.GR_Date,TSPL_Mcc_Milk_Transport_Invoice_HEAD.GENo,TSPL_Mcc_Milk_Transport_Invoice_HEAD.GEDate,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Item_Type,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Against_Requisition," & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Against_PO,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Against_GRN,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Against_MRN,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Against_SRN, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code1,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name1 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt1,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code2, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name2,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt2,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code3,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name3," & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt3,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code4,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name4,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt4, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code5 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name5, TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt5,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code6, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name6 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt6 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code7 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name7, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt7 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code8,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name8,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt8 , " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code9 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name9 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt9 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Code10 , " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Name10 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Add_Charge_Amt10 ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Total_Add_Charge,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Tax_Calculation_Type , " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Tot_Empty_Amount,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Dept,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Dept_Desc ,TSPL_Mcc_Milk_Transport_Invoice_HEAD.is_Excise_On_Qty,TSPL_Mcc_Milk_Transport_Invoice_HEAD.AssessableAmt, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.CURRENCY_CODE,TSPL_Mcc_Milk_Transport_Invoice_HEAD.CONVRATE,TSPL_Mcc_Milk_Transport_Invoice_HEAD.APPLICABLEFROM,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Against_C_Form,TSPL_Mcc_Milk_Transport_Invoice_HEAD.PROJECT_ID,TSPL_Mcc_Milk_Transport_Invoice_HEAD.IsAbatementPO, " & _
        " TSPL_Mcc_Milk_Transport_Invoice_HEAD.Is_Against_Form, TSPL_Mcc_Milk_Transport_Invoice_HEAD.Against_Form, TSPL_Mcc_Milk_Transport_Invoice_HEAD.Transporter, TSPL_Mcc_Milk_Transport_Invoice_HEAD.Transport_Date,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TransporterDesc,TSPL_Mcc_Milk_Transport_Invoice_HEAD.VehicleDesc,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Remit_To,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Expiration_Date,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Expiration_Amount,TSPL_Mcc_Milk_Transport_Invoice_HEAD.Scheduler_Code " & _
        " FROM TSPL_Mcc_Milk_Transport_Invoice_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Mcc_Milk_Transport_Invoice_HEAD.Bill_To_Location " & _
        " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_Mcc_Milk_Transport_Invoice_HEAD.Ship_To_Location " & _
        " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_Mcc_Milk_Transport_Invoice_HEAD.Tax_Group " & _
        " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_Mcc_Milk_Transport_Invoice_HEAD.Terms_Code where 2=2"

        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No = (select MIN(Doc_No) from TSPL_Mcc_Milk_Transport_Invoice_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No = (select Max(Doc_No) from TSPL_Mcc_Milk_Transport_Invoice_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No = (select Min(Doc_No) from TSPL_Mcc_Milk_Transport_Invoice_HEAD where Doc_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No = (select Max(Doc_No) from TSPL_Mcc_Milk_Transport_Invoice_HEAD where Doc_No<'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkTransporterInvoiceMCC()
            obj.PI_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
            obj.PI_Date = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
            obj.Vendor_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
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
            obj.PI_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("PI_Total_Amt"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))

            '-----------added by usha----
            obj.loc_code = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.Bill_To_Location + "'", trans)
            '--------------ends 
            If dt.Rows(0)("InvoiceDate") IsNot DBNull.Value Then
                obj.Invdate = clsCommon.myCstr(dt.Rows(0)("InvoiceDate"))
            End If

            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))

            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If

            obj.Against_C_Form = IIf(clsCommon.myCdbl(dt.Rows(0)("Against_C_Form")) = 1, True, False)

            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("Carrier"))
            obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))
            obj.LRNo = clsCommon.myCstr(dt.Rows(0)("LR_No"))

            obj.Against_Requisition = clsCommon.myCstr(dt.Rows(0)("Against_Requisition"))
            obj.Against_PO = clsCommon.myCstr(dt.Rows(0)("Against_PO"))
            obj.Against_GRN = clsCommon.myCstr(dt.Rows(0)("Against_GRN"))
            obj.Against_MRN = clsCommon.myCstr(dt.Rows(0)("Against_MRN"))
            obj.Against_SRN = clsCommon.myCstr(dt.Rows(0)("Against_SRN"))


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
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)

            If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
            End If
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Tot_Empty_Amount = clsCommon.myCdbl(dt.Rows(0)("Tot_Empty_Amount"))
            obj.objPIRemittance = clsPIRemittance.GetData(obj.PI_No, trans)
            obj.is_Excise_On_Qty = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Excise_On_Qty")) = 1, True, False)
            obj.AssessableAmt = clsCommon.myCdbl(dt.Rows(0)("AssessableAmt"))
            obj.Scheduler_code = clsCommon.myCstr(dt.Rows(0)("Scheduler_code"))
            obj.Remit_To = clsCommon.myCstr(dt.Rows(0)("Remit_To"))
            obj.Expiration_Date = clsCommon.myCstr(dt.Rows(0)("Expiration_Date"))
            obj.Expiration_Amount = clsCommon.myCdbl(dt.Rows(0)("Expiration_Amount"))

            obj.Is_Against_Form = IIf(clsCommon.CompairString(dt.Rows(0)("Is_Against_Form").ToString(), "Y") = CompairStringResult.Equal, True, False)
            If obj.Is_Against_Form Then
                obj.Against_Form = clsCommon.myCstr(dt.Rows(0)("Against_Form"))
            End If
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            If clsCommon.myLen(obj.GRNo) > 0 Then
                obj.GR_Date = dt.Rows(0)("GR_Date")
            End If
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.Transporter = clsCommon.myCstr(dt.Rows(0)("Transporter"))
            obj.TransporterDesc = clsCommon.myCstr(dt.Rows(0)("TransporterDesc"))
            obj.VehicleDesc = clsCommon.myCstr(dt.Rows(0)("VehicleDesc"))
            If clsCommon.myLen(obj.Transporter) > 0 Then
                obj.Transport_Date = dt.Rows(0)("Transport_Date")
            End If

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION
            obj.IsAbatementPO = dt.Rows(0)("IsAbatementPO")

            qry = "SELECT TSPL_Mcc_Milk_Transport_Invoice_Detail.Doc_No,TSPL_Mcc_Milk_Transport_Invoice_Detail.Row_Type,TSPL_Mcc_Milk_Transport_Invoice_Detail.Line_No,TSPL_Mcc_Milk_Transport_Invoice_Detail.Status,TSPL_Mcc_Milk_Transport_Invoice_Detail.status,TSPL_Mcc_Milk_Transport_Invoice_Detail.Item_Code,TSPL_Mcc_Milk_Transport_Invoice_Detail.Item_Desc,TSPL_Mcc_Milk_Transport_Invoice_Detail.Qty,TSPL_Mcc_Milk_Transport_Invoice_Detail.Balance_Qty,TSPL_Mcc_Milk_Transport_Invoice_Detail.Free_Qty,TSPL_Mcc_Milk_Transport_Invoice_Detail.Unit_code,TSPL_Mcc_Milk_Transport_Invoice_Detail.Location,TSPL_Mcc_Milk_Transport_Invoice_Detail.Item_Cost,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX1,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX1_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX1_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX2,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX2_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX2_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX3,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX3_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX3_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX4,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX4_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX4_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX5,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX5_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX5_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX6,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX6_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX6_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX7,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX7_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX7_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX8,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX8_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX8_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX9,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX9_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX9_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX10,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX10_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX10_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.Amount,TSPL_Mcc_Milk_Transport_Invoice_Detail.Disc_Per,TSPL_Mcc_Milk_Transport_Invoice_Detail.Disc_Type,TSPL_Mcc_Milk_Transport_Invoice_Detail.Disc_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.Amt_Less_Discount,TSPL_Mcc_Milk_Transport_Invoice_Detail.Total_Tax_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX1_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX2_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX3_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX4_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX5_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX6_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX7_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX8_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX9_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.TAX10_Base_Amt,TSPL_Mcc_Milk_Transport_Invoice_Detail.Bin_No,TSPL_Mcc_Milk_Transport_Invoice_Detail.MRP,TSPL_Mcc_Milk_Transport_Invoice_Detail.Batch_No,TSPL_Mcc_Milk_Transport_Invoice_Detail.MFG_Date,TSPL_Mcc_Milk_Transport_Invoice_Detail.Expiry_Date,TSPL_Mcc_Milk_Transport_Invoice_Detail.Specification,TSPL_Mcc_Milk_Transport_Invoice_Detail.Remarks,TSPL_Mcc_Milk_Transport_Invoice_Detail.Assessable,TSPL_Mcc_Milk_Transport_Invoice_Detail.AssessableAmt,TSPL_Mcc_Milk_Transport_Invoice_Detail.Leak_Qty ,TSPL_Mcc_Milk_Transport_Invoice_Detail.Burst_Qty,TSPL_Mcc_Milk_Transport_Invoice_Detail.Short_Qty,Landed_Cost_Rate,Landed_Cost_Amount,Total_AddtionalCost_PerUnit,Total_NonRecTax_PerUnit,Total_RecTax_PerUnit,Is_Mannual_Amt,Empty_Amount,TSPL_Mcc_Milk_Transport_Invoice_Detail.AbatementRate,TSPL_Mcc_Milk_Transport_Invoice_Detail.AssessableMRP,TSPL_Mcc_Milk_Transport_Invoice_Detail.TotalAssessableMRP,TSPL_Mcc_Milk_Transport_Invoice_Detail.Reject_Qty FROM TSPL_Mcc_Milk_Transport_Invoice_Detail left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Mcc_Milk_Transport_Invoice_Detail.Location where TSPL_Mcc_Milk_Transport_Invoice_Detail.Doc_No='" + obj.PI_No + "' ORDER BY TSPL_Mcc_Milk_Transport_Invoice_Detail.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMilkTransporterInvoiceMCCDetail)
                Dim objTr As clsMilkTransporterInvoiceMCCDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMilkTransporterInvoiceMCCDetail
                    objTr.PI_No = clsCommon.myCstr(dr("Doc_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.PI_Qty = clsCommon.myCdbl(dr("Qty"))
                    'objTr.SRN_Id = clsCommon.myCstr(dr("SRN_Id"))
                    'objTr.OrgSRNQty = clsCommon.myCdbl(dr("OrgSRNQty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    ''''''''starts here--------
                    objTr.Free_qty = clsCommon.myCdbl(dr("Free_Qty"))
                    '''''''''end here------
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Leak_Qty = clsCommon.myCdbl(dr("Leak_Qty"))
                    objTr.Burst_Qty = clsCommon.myCdbl(dr("Burst_Qty"))
                    objTr.Short_Qty = clsCommon.myCdbl(dr("Short_Qty"))
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
                    objTr.Disc_Type = clsCommon.myCdbl(dr("Disc_Type"))
                    objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    objTr.Landed_Cost_Rate = clsCommon.myCdbl(dr("Landed_Cost_Rate"))
                    objTr.Landed_Cost_Amount = clsCommon.myCdbl(dr("Landed_Cost_Amount"))
                    objTr.Total_NonRecTax_PerUnit = clsCommon.myCdbl(dr("Total_NonRecTax_PerUnit"))
                    objTr.Total_RecTax_PerUnit = clsCommon.myCdbl(dr("Total_RecTax_PerUnit"))
                    objTr.Total_AddtionalCost_PerUnit = clsCommon.myCdbl(dr("Total_AddtionalCost_PerUnit"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                    End If
                    objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(dr("Is_Mannual_Amt"))
                    objTr.Empty_Amount = clsCommon.myCdbl(dr("Empty_Amount"))

                    '' for abatement PI
                    objTr.AbatementRate = clsCommon.myCdbl(dr("AbatementRate"))
                    objTr.AssessableMRP = clsCommon.myCdbl(dr("AssessableMRP"))
                    objTr.TotalAssessableMRP = clsCommon.myCdbl(dr("TotalAssessableMRP"))
                    objTr.Reject_Qty = clsCommon.myCdbl(dr("Reject_Qty"))

                    obj.Arr.Add(objTr)
                Next
            End If
            obj.objJVC = clsPJVHead.GetData(obj.PI_No, NavigatorType.Current, trans)
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Invoice No not found to Post")
            End If
            ''Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            Dim obj As clsMilkTransporterInvoiceMCC = clsMilkTransporterInvoiceMCC.GetData(strDocNo, NavigatorType.Current, trans)
            '' Dim strPostDate As String = clsCommon.GetPrintDate(obj.PI_Date, "dd/MM/yyyy")
            If (obj Is Nothing OrElse clsCommon.myLen(obj.PI_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.mbtnPurchaseInvoice, obj.Bill_To_Location, clsCommon.myCDate(obj.PI_Date), trans)

            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Purchase Invoice No " + obj.PI_No + " Is On Hold.Can't Post it")
            End If


            Dim qry As String = ""
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_Mcc_Milk_Transport_Invoice_HEAD", "Doc_No", obj.PI_No, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If
            Dim isCreateDebitNotForReject As Boolean = False
            Dim isCreateDebitNotForShort As Boolean = False
            Dim isCreateDebitNotForDiscountDeduct As Boolean = False
            For Each objTr As clsMilkTransporterInvoiceMCCDetail In obj.Arr
                If clsCommon.myLen(objTr.SRN_Id) > 0 Then
                    Dim qry1 As String = "update TSPL_SRN_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(objTr.PI_Qty) + " where SRN_No='" + objTr.SRN_Id + "' and Item_Code='" + objTr.Item_Code + "' and Unit_code='" + objTr.Unit_code + "' and isnull(MRP,0)='" + clsCommon.myCstr(objTr.MRP) + "' and isnull(Assessable,0)='" + clsCommon.myCstr(objTr.Assessable) + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry1, trans)

                    'Dim coll As New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PI_Cost", objTr.Landed_Cost_Amount)
                    'clsCommon.AddColumnsForChange(coll, "LIFO_Cost", objTr.Landed_Cost_Amount)
                    'clsCommon.AddColumnsForChange(coll, "FIFO_Cost", objTr.Landed_Cost_Amount)
                    'clsCommon.AddColumnsForChange(coll, "Avg_Cost", objTr.Landed_Cost_Amount)
                    'clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT", OMInsertOrUpdate.Update, "Item_Code='" + objTr.Item_Code + "' and Source_Doc_No='" + objTr.SRN_Id + "' and Trans_Type='SRN'", trans)

                    If objTr.Short_Qty > 0 Then
                        isCreateDebitNotForShort = True
                    End If
                    If objTr.Reject_Qty > 0 Then
                        isCreateDebitNotForReject = True
                    End If
                    If objTr.Disc_Type = 1 AndAlso objTr.Disc_Amt > 0 Then
                        isCreateDebitNotForDiscountDeduct = True
                    End If

                End If
            Next



            Dim objVendorInvHead As New clsVedorInvoiceHead()
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.PI_Date, "dd/MM/yyyy")
            objVendorInvHead.Vendor_Code = obj.Vendor_Code
            objVendorInvHead.Vendor_Name = obj.Vendor_Name
            objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = obj.PI_Date
            objVendorInvHead.loc_code = obj.loc_code
            objVendorInvHead.PROJECT_ID = obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p

            '' ''added by priti
            ''objVendorInvHead.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
            ''objVendorInvHead.RefDocNo = txtRefDocNo.Text
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            objVendorInvHead.Total_Tax = obj.Total_Tax_Amt

            objVendorInvHead.On_Hold = False
            Dim srndate As String
            Dim query As String = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + obj.Against_SRN + "' "
            srndate = clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MM/yyyy")

            objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against PO Invoice No " + obj.PI_No + "-" + obj.Against_SRN + "-" + srndate
            objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
            objVendorInvHead.Tax_Group = obj.Tax_Group
            If (clsCommon.myLen(obj.TAX1) > 0) Then
                objVendorInvHead.TAX1 = obj.TAX1
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
                    objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                    objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            End If
            If (clsCommon.myLen(obj.TAX2) > 0) Then
                objVendorInvHead.TAX2 = obj.TAX2
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
                    objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                    objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            End If
            If (clsCommon.myLen(obj.TAX3) > 0) Then
                objVendorInvHead.TAX3 = obj.TAX3
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
                    objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                    objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            End If
            If (clsCommon.myLen(obj.TAX4) > 0) Then
                objVendorInvHead.TAX4 = obj.TAX4
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
                    objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                    objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            End If
            If (clsCommon.myLen(obj.TAX5) > 0) Then
                objVendorInvHead.TAX5 = obj.TAX5
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
                    objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                    objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Bill_To_Location, trans)

                End If
                objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            End If
            If (clsCommon.myLen(obj.TAX6) > 0) Then
                objVendorInvHead.TAX6 = obj.TAX6
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
                    objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                    objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            End If
            If (clsCommon.myLen(obj.TAX7) > 0) Then
                objVendorInvHead.TAX7 = obj.TAX7
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
                    objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                    objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Bill_To_Location, trans)

                End If
                objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            End If
            If (clsCommon.myLen(obj.TAX8) > 0) Then
                objVendorInvHead.TAX8 = obj.TAX8
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
                    objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                    objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            End If
            If (clsCommon.myLen(obj.TAX9) > 0) Then
                objVendorInvHead.TAX9 = obj.TAX9
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
                    objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                    objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            End If
            If (clsCommon.myLen(obj.TAX10) > 0) Then
                objVendorInvHead.TAX10 = obj.TAX10
                If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
                    objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                    objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
                objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            End If

            objVendorInvHead.Terms_Code = obj.Terms_Code
            objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = obj.Due_Date
            objVendorInvHead.Discount_Base = obj.Discount_Base
            objVendorInvHead.Discount_Amount = obj.Discount_Amt
            objVendorInvHead.Amount_Less_Discount = obj.Amount_Less_Discount
            objVendorInvHead.Document_Total = obj.PI_Total_Amt
            objVendorInvHead.Balance_Amt = obj.PI_Total_Amt
            objVendorInvHead.Against_POInvoice_No = obj.PI_No
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Bill_To_Location, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Bill_To_Location, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            Dim strFirstItemCode As String = GetFirstItemCode(obj.Arr)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0
            For Each objPIDetail As clsMilkTransporterInvoiceMCCDetail In obj.Arr
                Dim strICode As String = objPIDetail.Item_Code
                If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    strICode = strFirstItemCode
                End If

                ''Fill VendorInvoice details Data
                qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                End If
                ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
                ''End If
                Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                    objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Bill_To_Location, trans)
                End If

                'If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
                '    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, trans))
                '    objVendorInvHead.Empty_Amount += dblVal * objPIDetail.PI_Qty
                'End If

                ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                ''    Throw New Exception("Please set Payable Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
                ''End If

                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                objVendorInvDetail.Amount = objPIDetail.Amount
                objVendorInvDetail.Discount_Per = objPIDetail.Disc_Per
                objVendorInvDetail.Discount = objPIDetail.Disc_Amt
                objVendorInvDetail.Amount_less_Discount = objPIDetail.Amt_Less_Discount
                objVendorInvDetail.TAX1 = objPIDetail.TAX1
                objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
                objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt
                objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt
                objVendorInvDetail.TAX2 = objPIDetail.TAX2
                objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
                objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt
                objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt
                objVendorInvDetail.TAX3 = objPIDetail.TAX3
                objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
                objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt
                objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt
                objVendorInvDetail.TAX4 = objPIDetail.TAX4
                objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
                objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt
                objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt
                objVendorInvDetail.TAX5 = objPIDetail.TAX5
                objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
                objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt
                objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt
                objVendorInvDetail.TAX6 = objPIDetail.TAX6
                objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
                objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt
                objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt
                objVendorInvDetail.TAX7 = objPIDetail.TAX7
                objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
                objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt
                objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt
                objVendorInvDetail.TAX8 = objPIDetail.TAX8
                objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
                objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt
                objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt
                objVendorInvDetail.TAX9 = objPIDetail.TAX9
                objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
                objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt
                objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt
                objVendorInvDetail.TAX10 = objPIDetail.TAX10
                objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
                objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt
                objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt
                objVendorInvDetail.Total_Tax = objPIDetail.Total_Tax_Amt
                objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt
                objVendorInvDetail.Landed_Amount = objPIDetail.Landed_Cost_Amount - objPIDetail.Amt_Less_Discount
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

                If objPIDetail.Disc_Type = 1 Then
                    objVendorInvHead.Discount_Amount -= objPIDetail.Disc_Amt
                    objVendorInvHead.Amount_Less_Discount += objPIDetail.Disc_Amt
                    objVendorInvHead.Document_Total += objPIDetail.Disc_Amt
                End If


                objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt
                objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt
                objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt
                objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt
                objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt
                objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt
                objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt
                objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt
                objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt
                objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
                ''End of Fill Vendor Invoice Detail Data
            Next

            objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If

            If obj.objPIRemittance IsNot Nothing Then
                objVendorInvHead.RemittanceObject = New clsRemittance()
                objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
                objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
                objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
                objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
                objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
                objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
                objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
                objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
                objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
                objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
                objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
                objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
                objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
                objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
                objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
                objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
                objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
                objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
                objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
                objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
                objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
                objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
                objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
                objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
                objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
                objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
                objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
                objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
                objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
                objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
                objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS

                objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
                objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
                objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
                objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
                objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
                objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
                objVendorInvHead.Balance_Amt = obj.PI_Total_Amt - obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code



            End If
            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            objVendorInvHead.ConvRate = obj.ConvRate
            objVendorInvHead.ApplicableFrom = obj.ApplicableFrom
            ''end multicurrency

            isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.PI_Date)
            '##############################################################################################################################
            ''Balwinder on 12/11/2014 for create debit note
            If isCreateDebitNotForDiscountDeduct Then
                objVendorInvHead = New clsVedorInvoiceHead()
                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.PI_Date, "dd/MM/yyyy")
                objVendorInvHead.Vendor_Code = obj.Vendor_Code
                objVendorInvHead.Vendor_Name = obj.Vendor_Name
                objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = obj.PI_Date
                objVendorInvHead.loc_code = obj.loc_code
                objVendorInvHead.PROJECT_ID = obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
                End If

                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type

                objVendorInvHead.On_Hold = False

                query = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + obj.Against_SRN + "' "
                srndate = clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MM/yyyy")

                objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against PO Invoice No " + obj.PI_No + "-" + obj.Against_SRN + "-" + srndate
                objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                objVendorInvHead.Tax_Group = ""
                'If (clsCommon.myLen(obj.TAX1) > 0) Then
                '    objVendorInvHead.TAX1 = obj.TAX1
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
                '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Bill_To_Location, trans)
                '    End If
                '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                '    objVendorInvHead.Tax1_BAmount = 0 ' obj.TAX1_Base_Amt
                '    objVendorInvHead.TAX1_Amt = 0 'obj.TAX1_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX2) > 0) Then
                '    objVendorInvHead.TAX2 = obj.TAX2
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
                '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Bill_To_Location, trans)
                '    End If
                '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                '    objVendorInvHead.Tax2_BAmount = 0 ' obj.TAX2_Base_Amt
                '    objVendorInvHead.TAX2_Amt = 0 'obj.TAX2_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX3) > 0) Then
                '    objVendorInvHead.TAX3 = obj.TAX3
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
                '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Bill_To_Location, trans)
                '    End If
                '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                '    objVendorInvHead.Tax3_BAmount = 0 ' obj.TAX3_Base_Amt
                '    objVendorInvHead.TAX3_Amt = 0 'obj.TAX3_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX4) > 0) Then
                '    objVendorInvHead.TAX4 = obj.TAX4
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
                '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Bill_To_Location, trans)
                '    End If
                '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                '    objVendorInvHead.Tax4_BAmount = 0 ' obj.TAX4_Base_Amt
                '    objVendorInvHead.TAX4_Amt = 0 'obj.TAX4_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX5) > 0) Then
                '    objVendorInvHead.TAX5 = obj.TAX5
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
                '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Bill_To_Location, trans)

                '    End If
                '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                '    objVendorInvHead.Tax5_BAmount = 0 ' obj.TAX5_Base_Amt
                '    objVendorInvHead.TAX5_Amt = 0 'obj.TAX5_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX6) > 0) Then
                '    objVendorInvHead.TAX6 = obj.TAX6
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
                '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Bill_To_Location, trans)
                '    End If
                '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                '    objVendorInvHead.Tax6_BAmount = 0 ' obj.TAX6_Base_Amt
                '    objVendorInvHead.TAX6_Amt = 0 'obj.TAX6_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX7) > 0) Then
                '    objVendorInvHead.TAX7 = obj.TAX7
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
                '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Bill_To_Location, trans)

                '    End If
                '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                '    objVendorInvHead.Tax7_BAmount = 0 ' obj.TAX7_Base_Amt
                '    objVendorInvHead.TAX7_Amt = 0 'obj.TAX7_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX8) > 0) Then
                '    objVendorInvHead.TAX8 = obj.TAX8
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
                '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Bill_To_Location, trans)
                '    End If
                '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                '    objVendorInvHead.Tax8_BAmount = 0 ' obj.TAX8_Base_Amt
                '    objVendorInvHead.TAX8_Amt = 0 ' obj.TAX8_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX9) > 0) Then
                '    objVendorInvHead.TAX9 = obj.TAX9
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
                '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Bill_To_Location, trans)
                '    End If
                '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                '    objVendorInvHead.Tax9_BAmount = 0 ' obj.TAX9_Base_Amt
                '    objVendorInvHead.TAX9_Amt = 0 'obj.TAX9_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX10) > 0) Then
                '    objVendorInvHead.TAX10 = obj.TAX10
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
                '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Bill_To_Location, trans)
                '    End If
                '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                '    objVendorInvHead.Tax10_BAmount = 0 ' obj.TAX10_Base_Amt
                '    objVendorInvHead.TAX10_Amt = 0 'obj.TAX10_Amt
                'End If

                objVendorInvHead.Terms_Code = obj.Terms_Code
                objVendorInvHead.Terms_Description = obj.TermsName
                objVendorInvHead.Due_Date = obj.Due_Date
                objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
                objVendorInvHead.Discount_Amount = 0 ' obj.Discount_Amt
                objVendorInvHead.Amount_Less_Discount = 0 ' obj.Amount_Less_Discount
                objVendorInvHead.Total_Tax = 0 'obj.Total_Tax_Amt
                objVendorInvHead.Document_Total = 0 ' obj.PI_Total_Amt
                objVendorInvHead.Balance_Amt = 0 'obj.PI_Total_Amt
                objVendorInvHead.Against_POInvoice_No = obj.PI_No
                dt = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Bill_To_Location, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Bill_To_Location, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If


                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                ii = 0
                isFirstTime = True
                strFirstItemCode = GetFirstItemCode(obj.Arr)
                objVendorInvHead.Total_Landed_Amt = 0
                For Each objPIDetail As clsMilkTransporterInvoiceMCCDetail In obj.Arr
                    If objPIDetail.Disc_Type = 0 OrElse objPIDetail.Disc_Amt = 0 Then
                        Continue For
                    End If


                    Dim strICode As String = objPIDetail.Item_Code
                    If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        strICode = strFirstItemCode
                    End If

                    ''Fill VendorInvoice details Data
                    qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set shortage for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                    End If
                    Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                    Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                        objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Bill_To_Location, trans)
                    End If
                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                    ii = ii + 1
                    objVendorInvDetail.Detail_Line_No = ii
                    objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                    objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                    objVendorInvDetail.Amount = objPIDetail.Disc_Amt
                    objVendorInvDetail.Discount_Per = 0
                    objVendorInvDetail.Discount = objPIDetail.Disc_Amt
                    objVendorInvDetail.Amount_less_Discount = objPIDetail.Disc_Amt
                    'objVendorInvDetail.TAX1 = objPIDetail.TAX1
                    'objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
                    'objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt * dclratio
                    'objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt * dclratio
                    'objVendorInvDetail.TAX2 = objPIDetail.TAX2
                    'objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
                    'objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt * dclratio
                    'objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt * dclratio
                    'objVendorInvDetail.TAX3 = objPIDetail.TAX3
                    'objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
                    'objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt * dclratio
                    'objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt * dclratio
                    'objVendorInvDetail.TAX4 = objPIDetail.TAX4
                    'objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
                    'objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt * dclratio
                    'objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt * dclratio
                    'objVendorInvDetail.TAX5 = objPIDetail.TAX5
                    'objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
                    'objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt * dclratio
                    'objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt * dclratio
                    'objVendorInvDetail.TAX6 = objPIDetail.TAX6
                    'objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
                    'objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt * dclratio
                    'objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt * dclratio
                    'objVendorInvDetail.TAX7 = objPIDetail.TAX7
                    'objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
                    'objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt * dclratio
                    'objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt * dclratio
                    'objVendorInvDetail.TAX8 = objPIDetail.TAX8
                    'objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
                    'objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt * dclratio
                    'objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt * dclratio
                    'objVendorInvDetail.TAX9 = objPIDetail.TAX9
                    'objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
                    'objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt * dclratio
                    'objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt * dclratio
                    'objVendorInvDetail.TAX10 = objPIDetail.TAX10
                    'objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
                    'objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt * dclratio
                    'objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt * dclratio
                    objVendorInvDetail.Total_Tax = objPIDetail.Disc_Amt
                    objVendorInvDetail.Total_Amount = objPIDetail.Disc_Amt
                    'objVendorInvDetail.Landed_Amount = 0


                    'objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt * dclratio
                    'objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt * dclratio
                    'objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt * dclratio
                    'objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt * dclratio
                    'objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt * dclratio
                    'objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt * dclratio
                    'objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt * dclratio
                    'objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt * dclratio
                    'objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt * dclratio
                    'objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt * dclratio




                    'objVendorInvHead.Tax1_BAmount += objVendorInvDetail.TAX1_Base_Amt
                    'objVendorInvHead.TAX1_Amt += objVendorInvDetail.TAX1_Amt
                    'objVendorInvHead.Tax2_BAmount += objVendorInvDetail.TAX2_Base_Amt
                    'objVendorInvHead.TAX2_Amt += objVendorInvDetail.TAX2_Amt
                    'objVendorInvHead.Tax3_BAmount += objVendorInvDetail.TAX3_Base_Amt
                    'objVendorInvHead.TAX3_Amt += objVendorInvDetail.TAX3_Amt
                    'objVendorInvHead.Tax4_BAmount += objVendorInvDetail.TAX4_Base_Amt
                    'objVendorInvHead.TAX4_Amt += objVendorInvDetail.TAX4_Amt
                    'objVendorInvHead.Tax5_BAmount += objVendorInvDetail.TAX5_Base_Amt
                    'objVendorInvHead.TAX5_Amt += objVendorInvDetail.TAX5_Amt
                    'objVendorInvHead.Tax6_BAmount += objVendorInvDetail.TAX6_Base_Amt
                    'objVendorInvHead.TAX6_Amt += objVendorInvDetail.TAX6_Amt
                    'objVendorInvHead.Tax7_BAmount += objVendorInvDetail.TAX7_Base_Amt
                    'objVendorInvHead.TAX7_Amt += objVendorInvDetail.TAX7_Amt
                    'objVendorInvHead.Tax8_BAmount += objVendorInvDetail.TAX8_Base_Amt
                    'objVendorInvHead.TAX8_Amt += objVendorInvDetail.TAX8_Amt
                    'objVendorInvHead.Tax9_BAmount += objVendorInvDetail.TAX9_Base_Amt
                    'objVendorInvHead.TAX9_Amt += objVendorInvDetail.TAX9_Amt
                    'objVendorInvHead.Tax10_BAmount += objVendorInvDetail.TAX10_Base_Amt
                    'objVendorInvHead.TAX10_Amt += objVendorInvDetail.TAX10_Amt

                    objVendorInvHead.Discount_Base += objVendorInvDetail.Amount
                    objVendorInvHead.Discount_Amount += objVendorInvDetail.Discount
                    objVendorInvHead.Amount_Less_Discount += objVendorInvDetail.Amount_less_Discount
                    objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                    objVendorInvHead.Total_Tax += (objVendorInvDetail.TAX1_Amt + objVendorInvDetail.TAX1_Amt + objVendorInvDetail.TAX2_Amt + objVendorInvDetail.TAX3_Amt + objVendorInvDetail.TAX4_Amt + objVendorInvDetail.TAX5_Amt + objVendorInvDetail.TAX6_Amt + objVendorInvDetail.TAX7_Amt + objVendorInvDetail.TAX8_Amt + objVendorInvDetail.TAX9_Amt + objVendorInvDetail.TAX10_Amt)
                    objVendorInvHead.Document_Total += objVendorInvDetail.Total_Amount
                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                    End If
                    ''End of Fill Vendor Invoice Detail Data
                Next
                objVendorInvHead.Balance_Amt = objVendorInvHead.Document_Total
                objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If

                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                objVendorInvHead.ConvRate = obj.ConvRate
                objVendorInvHead.ApplicableFrom = obj.ApplicableFrom
                ''end multicurrency

                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.PI_Date)
                ''End of create debit note for shortage
            End If

            If isCreateDebitNotForShort AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateDbitNoteForShortPI, clsFixedParameterCode.CreateDbitNoteForShortPI, trans)) = 1 Then
                ''Create Debit note for Shortage
                objVendorInvHead = New clsVedorInvoiceHead()
                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.PI_Date, "dd/MM/yyyy")
                objVendorInvHead.Vendor_Code = obj.Vendor_Code
                objVendorInvHead.Vendor_Name = obj.Vendor_Name
                objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = obj.PI_Date
                objVendorInvHead.loc_code = obj.loc_code
                objVendorInvHead.PROJECT_ID = obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
                End If

                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type

                objVendorInvHead.On_Hold = False

                query = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + obj.Against_SRN + "' "
                srndate = clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MM/yyyy")

                objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against PO Invoice No " + obj.PI_No + "-" + obj.Against_SRN + "-" + srndate
                objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                objVendorInvHead.Tax_Group = obj.Tax_Group
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    objVendorInvHead.TAX1 = obj.TAX1
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
                        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    objVendorInvHead.Tax1_BAmount = 0 ' obj.TAX1_Base_Amt
                    objVendorInvHead.TAX1_Amt = 0 'obj.TAX1_Amt
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    objVendorInvHead.TAX2 = obj.TAX2
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
                        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    objVendorInvHead.Tax2_BAmount = 0 ' obj.TAX2_Base_Amt
                    objVendorInvHead.TAX2_Amt = 0 'obj.TAX2_Amt
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    objVendorInvHead.TAX3 = obj.TAX3
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
                        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    objVendorInvHead.Tax3_BAmount = 0 ' obj.TAX3_Base_Amt
                    objVendorInvHead.TAX3_Amt = 0 'obj.TAX3_Amt
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    objVendorInvHead.TAX4 = obj.TAX4
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
                        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    objVendorInvHead.Tax4_BAmount = 0 ' obj.TAX4_Base_Amt
                    objVendorInvHead.TAX4_Amt = 0 'obj.TAX4_Amt
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    objVendorInvHead.TAX5 = obj.TAX5
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
                        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Bill_To_Location, trans)

                    End If
                    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    objVendorInvHead.Tax5_BAmount = 0 ' obj.TAX5_Base_Amt
                    objVendorInvHead.TAX5_Amt = 0 'obj.TAX5_Amt
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    objVendorInvHead.TAX6 = obj.TAX6
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
                        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    objVendorInvHead.Tax6_BAmount = 0 ' obj.TAX6_Base_Amt
                    objVendorInvHead.TAX6_Amt = 0 'obj.TAX6_Amt
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    objVendorInvHead.TAX7 = obj.TAX7
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
                        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Bill_To_Location, trans)

                    End If
                    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    objVendorInvHead.Tax7_BAmount = 0 ' obj.TAX7_Base_Amt
                    objVendorInvHead.TAX7_Amt = 0 'obj.TAX7_Amt
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    objVendorInvHead.TAX8 = obj.TAX8
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
                        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    objVendorInvHead.Tax8_BAmount = 0 ' obj.TAX8_Base_Amt
                    objVendorInvHead.TAX8_Amt = 0 ' obj.TAX8_Amt
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    objVendorInvHead.TAX9 = obj.TAX9
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
                        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    objVendorInvHead.Tax9_BAmount = 0 ' obj.TAX9_Base_Amt
                    objVendorInvHead.TAX9_Amt = 0 'obj.TAX9_Amt
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    objVendorInvHead.TAX10 = obj.TAX10
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
                        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                    objVendorInvHead.Tax10_BAmount = 0 ' obj.TAX10_Base_Amt
                    objVendorInvHead.TAX10_Amt = 0 'obj.TAX10_Amt
                End If

                objVendorInvHead.Terms_Code = obj.Terms_Code
                objVendorInvHead.Terms_Description = obj.TermsName
                objVendorInvHead.Due_Date = obj.Due_Date
                objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
                objVendorInvHead.Discount_Amount = 0 ' obj.Discount_Amt
                objVendorInvHead.Amount_Less_Discount = 0 ' obj.Amount_Less_Discount
                objVendorInvHead.Total_Tax = 0 'obj.Total_Tax_Amt
                objVendorInvHead.Document_Total = 0 ' obj.PI_Total_Amt
                objVendorInvHead.Balance_Amt = 0 'obj.PI_Total_Amt
                objVendorInvHead.Against_POInvoice_No = obj.PI_No
                dt = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Bill_To_Location, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Bill_To_Location, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If


                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                ii = 0
                isFirstTime = True
                strFirstItemCode = GetFirstItemCode(obj.Arr)
                objVendorInvHead.Total_Landed_Amt = 0
                For Each objPIDetail As clsMilkTransporterInvoiceMCCDetail In obj.Arr
                    If (objPIDetail.PI_Qty + objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty + objPIDetail.Reject_Qty) = 0 Then
                        Continue For
                    End If
                    Dim dclratio As Decimal = (objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty) / (objPIDetail.PI_Qty + objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty + objPIDetail.Reject_Qty)
                    If dclratio = 0 Then
                        Continue For
                    End If

                    Dim strICode As String = objPIDetail.Item_Code
                    If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        strICode = strFirstItemCode
                    End If

                    ''Fill VendorInvoice details Data
                    qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Other_2  as Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Other_2 where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set shortage for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                    End If
                    Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                    Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                        objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Bill_To_Location, trans)
                    End If
                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                    ii = ii + 1
                    objVendorInvDetail.Detail_Line_No = ii
                    objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                    objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                    objVendorInvDetail.Amount = objPIDetail.Amount * dclratio
                    objVendorInvDetail.Discount_Per = objPIDetail.Disc_Per
                    objVendorInvDetail.Discount = objPIDetail.Disc_Amt * dclratio
                    objVendorInvDetail.Amount_less_Discount = objPIDetail.Amt_Less_Discount * dclratio
                    objVendorInvDetail.TAX1 = objPIDetail.TAX1
                    objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
                    objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt * dclratio
                    objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt * dclratio
                    objVendorInvDetail.TAX2 = objPIDetail.TAX2
                    objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
                    objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt * dclratio
                    objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt * dclratio
                    objVendorInvDetail.TAX3 = objPIDetail.TAX3
                    objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
                    objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt * dclratio
                    objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt * dclratio
                    objVendorInvDetail.TAX4 = objPIDetail.TAX4
                    objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
                    objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt * dclratio
                    objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt * dclratio
                    objVendorInvDetail.TAX5 = objPIDetail.TAX5
                    objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
                    objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt * dclratio
                    objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt * dclratio
                    objVendorInvDetail.TAX6 = objPIDetail.TAX6
                    objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
                    objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt * dclratio
                    objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt * dclratio
                    objVendorInvDetail.TAX7 = objPIDetail.TAX7
                    objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
                    objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt * dclratio
                    objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt * dclratio
                    objVendorInvDetail.TAX8 = objPIDetail.TAX8
                    objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
                    objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt * dclratio
                    objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt * dclratio
                    objVendorInvDetail.TAX9 = objPIDetail.TAX9
                    objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
                    objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt * dclratio
                    objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt * dclratio
                    objVendorInvDetail.TAX10 = objPIDetail.TAX10
                    objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
                    objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt * dclratio
                    objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt * dclratio
                    objVendorInvDetail.Total_Tax = objPIDetail.Total_Tax_Amt * dclratio
                    objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt * dclratio
                    objVendorInvDetail.Landed_Amount = (objPIDetail.Landed_Cost_Amount * dclratio) - (objPIDetail.Amt_Less_Discount * dclratio)


                    objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt * dclratio
                    objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt * dclratio
                    objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt * dclratio
                    objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt * dclratio
                    objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt * dclratio
                    objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt * dclratio
                    objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt * dclratio
                    objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt * dclratio
                    objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt * dclratio
                    objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt * dclratio




                    objVendorInvHead.Tax1_BAmount += objVendorInvDetail.TAX1_Base_Amt
                    objVendorInvHead.TAX1_Amt += objVendorInvDetail.TAX1_Amt
                    objVendorInvHead.Tax2_BAmount += objVendorInvDetail.TAX2_Base_Amt
                    objVendorInvHead.TAX2_Amt += objVendorInvDetail.TAX2_Amt
                    objVendorInvHead.Tax3_BAmount += objVendorInvDetail.TAX3_Base_Amt
                    objVendorInvHead.TAX3_Amt += objVendorInvDetail.TAX3_Amt
                    objVendorInvHead.Tax4_BAmount += objVendorInvDetail.TAX4_Base_Amt
                    objVendorInvHead.TAX4_Amt += objVendorInvDetail.TAX4_Amt
                    objVendorInvHead.Tax5_BAmount += objVendorInvDetail.TAX5_Base_Amt
                    objVendorInvHead.TAX5_Amt += objVendorInvDetail.TAX5_Amt
                    objVendorInvHead.Tax6_BAmount += objVendorInvDetail.TAX6_Base_Amt
                    objVendorInvHead.TAX6_Amt += objVendorInvDetail.TAX6_Amt
                    objVendorInvHead.Tax7_BAmount += objVendorInvDetail.TAX7_Base_Amt
                    objVendorInvHead.TAX7_Amt += objVendorInvDetail.TAX7_Amt
                    objVendorInvHead.Tax8_BAmount += objVendorInvDetail.TAX8_Base_Amt
                    objVendorInvHead.TAX8_Amt += objVendorInvDetail.TAX8_Amt
                    objVendorInvHead.Tax9_BAmount += objVendorInvDetail.TAX9_Base_Amt
                    objVendorInvHead.TAX9_Amt += objVendorInvDetail.TAX9_Amt
                    objVendorInvHead.Tax10_BAmount += objVendorInvDetail.TAX10_Base_Amt
                    objVendorInvHead.TAX10_Amt += objVendorInvDetail.TAX10_Amt

                    objVendorInvHead.Discount_Base += objVendorInvDetail.Amount
                    objVendorInvHead.Discount_Amount += objVendorInvDetail.Discount
                    objVendorInvHead.Amount_Less_Discount += objVendorInvDetail.Amount_less_Discount
                    objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                    objVendorInvHead.Total_Tax += (objVendorInvDetail.TAX1_Amt + objVendorInvDetail.TAX1_Amt + objVendorInvDetail.TAX2_Amt + objVendorInvDetail.TAX3_Amt + objVendorInvDetail.TAX4_Amt + objVendorInvDetail.TAX5_Amt + objVendorInvDetail.TAX6_Amt + objVendorInvDetail.TAX7_Amt + objVendorInvDetail.TAX8_Amt + objVendorInvDetail.TAX9_Amt + objVendorInvDetail.TAX10_Amt)
                    objVendorInvHead.Document_Total += objVendorInvDetail.Total_Amount
                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                    End If
                    ''End of Fill Vendor Invoice Detail Data
                Next
                objVendorInvHead.Balance_Amt = objVendorInvHead.Document_Total
                objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If

                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                objVendorInvHead.ConvRate = obj.ConvRate
                objVendorInvHead.ApplicableFrom = obj.ApplicableFrom
                ''end multicurrency

                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.PI_Date)
                ''End of create debit note for shortage
            End If



            If isCreateDebitNotForReject AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateDbitNoteForRejectPI, clsFixedParameterCode.CreateDbitNoteForRejectPI, trans)) = 1 Then
                ''Create Debit note for Rejected
                objVendorInvHead = New clsVedorInvoiceHead()
                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.PI_Date, "dd/MM/yyyy")
                objVendorInvHead.Vendor_Code = obj.Vendor_Code
                objVendorInvHead.Vendor_Name = obj.Vendor_Name
                objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = obj.PI_Date
                objVendorInvHead.loc_code = obj.loc_code
                objVendorInvHead.PROJECT_ID = obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
                End If

                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type

                objVendorInvHead.On_Hold = False

                query = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + obj.Against_SRN + "' "
                srndate = clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MM/yyyy")

                objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against PO Invoice No " + obj.PI_No + "-" + obj.Against_SRN + "-" + srndate
                objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                objVendorInvHead.Tax_Group = obj.Tax_Group
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    objVendorInvHead.TAX1 = obj.TAX1
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
                        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    objVendorInvHead.Tax1_BAmount = 0 ' obj.TAX1_Base_Amt
                    objVendorInvHead.TAX1_Amt = 0 'obj.TAX1_Amt
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    objVendorInvHead.TAX2 = obj.TAX2
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
                        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    objVendorInvHead.Tax2_BAmount = 0 ' obj.TAX2_Base_Amt
                    objVendorInvHead.TAX2_Amt = 0 'obj.TAX2_Amt
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    objVendorInvHead.TAX3 = obj.TAX3
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
                        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    objVendorInvHead.Tax3_BAmount = 0 ' obj.TAX3_Base_Amt
                    objVendorInvHead.TAX3_Amt = 0 'obj.TAX3_Amt
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    objVendorInvHead.TAX4 = obj.TAX4
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
                        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    objVendorInvHead.Tax4_BAmount = 0 ' obj.TAX4_Base_Amt
                    objVendorInvHead.TAX4_Amt = 0 'obj.TAX4_Amt
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    objVendorInvHead.TAX5 = obj.TAX5
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
                        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Bill_To_Location, trans)

                    End If
                    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    objVendorInvHead.Tax5_BAmount = 0 ' obj.TAX5_Base_Amt
                    objVendorInvHead.TAX5_Amt = 0 'obj.TAX5_Amt
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    objVendorInvHead.TAX6 = obj.TAX6
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
                        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    objVendorInvHead.Tax6_BAmount = 0 ' obj.TAX6_Base_Amt
                    objVendorInvHead.TAX6_Amt = 0 'obj.TAX6_Amt
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    objVendorInvHead.TAX7 = obj.TAX7
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
                        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Bill_To_Location, trans)

                    End If
                    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    objVendorInvHead.Tax7_BAmount = 0 ' obj.TAX7_Base_Amt
                    objVendorInvHead.TAX7_Amt = 0 'obj.TAX7_Amt
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    objVendorInvHead.TAX8 = obj.TAX8
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
                        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    objVendorInvHead.Tax8_BAmount = 0 ' obj.TAX8_Base_Amt
                    objVendorInvHead.TAX8_Amt = 0 ' obj.TAX8_Amt
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    objVendorInvHead.TAX9 = obj.TAX9
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
                        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    objVendorInvHead.Tax9_BAmount = 0 ' obj.TAX9_Base_Amt
                    objVendorInvHead.TAX9_Amt = 0 'obj.TAX9_Amt
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    objVendorInvHead.TAX10 = obj.TAX10
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
                        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                    objVendorInvHead.Tax10_BAmount = 0 ' obj.TAX10_Base_Amt
                    objVendorInvHead.TAX10_Amt = 0 'obj.TAX10_Amt
                End If

                objVendorInvHead.Terms_Code = obj.Terms_Code
                objVendorInvHead.Terms_Description = obj.TermsName
                objVendorInvHead.Due_Date = obj.Due_Date
                objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
                objVendorInvHead.Discount_Amount = 0 ' obj.Discount_Amt
                objVendorInvHead.Amount_Less_Discount = 0 ' obj.Amount_Less_Discount
                objVendorInvHead.Total_Tax = 0 'obj.Total_Tax_Amt
                objVendorInvHead.Document_Total = 0 ' obj.PI_Total_Amt
                objVendorInvHead.Balance_Amt = 0 'obj.PI_Total_Amt
                objVendorInvHead.Against_POInvoice_No = obj.PI_No
                dt = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Bill_To_Location, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Bill_To_Location, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If


                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                ii = 0
                isFirstTime = True
                strFirstItemCode = GetFirstItemCode(obj.Arr)
                objVendorInvHead.Total_Landed_Amt = 0
                For Each objPIDetail As clsMilkTransporterInvoiceMCCDetail In obj.Arr
                    If (objPIDetail.PI_Qty + objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty + objPIDetail.Reject_Qty) = 0 Then
                        Continue For
                    End If

                    Dim dclratio As Decimal = (objPIDetail.Reject_Qty) / (objPIDetail.PI_Qty + objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty + objPIDetail.Reject_Qty)
                    If dclratio = 0 Then
                        Continue For
                    End If

                    Dim strICode As String = objPIDetail.Item_Code
                    If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        strICode = strFirstItemCode
                    End If

                    ''Fill VendorInvoice details Data
                    qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Other_1 as Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Other_1 where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set reject for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                    End If
                    Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                    Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                        objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Bill_To_Location, trans)
                    End If
                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                    ii = ii + 1
                    objVendorInvDetail.Detail_Line_No = ii
                    objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                    objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                    objVendorInvDetail.Amount = objPIDetail.Amount * dclratio
                    objVendorInvDetail.Discount_Per = objPIDetail.Disc_Per
                    objVendorInvDetail.Discount = objPIDetail.Disc_Amt * dclratio
                    objVendorInvDetail.Amount_less_Discount = objPIDetail.Amt_Less_Discount * dclratio
                    objVendorInvDetail.TAX1 = objPIDetail.TAX1
                    objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
                    objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt * dclratio
                    objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt * dclratio
                    objVendorInvDetail.TAX2 = objPIDetail.TAX2
                    objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
                    objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt * dclratio
                    objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt * dclratio
                    objVendorInvDetail.TAX3 = objPIDetail.TAX3
                    objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
                    objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt * dclratio
                    objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt * dclratio
                    objVendorInvDetail.TAX4 = objPIDetail.TAX4
                    objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
                    objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt * dclratio
                    objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt * dclratio
                    objVendorInvDetail.TAX5 = objPIDetail.TAX5
                    objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
                    objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt * dclratio
                    objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt * dclratio
                    objVendorInvDetail.TAX6 = objPIDetail.TAX6
                    objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
                    objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt * dclratio
                    objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt * dclratio
                    objVendorInvDetail.TAX7 = objPIDetail.TAX7
                    objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
                    objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt * dclratio
                    objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt * dclratio
                    objVendorInvDetail.TAX8 = objPIDetail.TAX8
                    objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
                    objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt * dclratio
                    objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt * dclratio
                    objVendorInvDetail.TAX9 = objPIDetail.TAX9
                    objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
                    objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt * dclratio
                    objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt * dclratio
                    objVendorInvDetail.TAX10 = objPIDetail.TAX10
                    objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
                    objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt * dclratio
                    objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt * dclratio
                    objVendorInvDetail.Total_Tax = objPIDetail.Total_Tax_Amt * dclratio
                    objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt * dclratio
                    objVendorInvDetail.Landed_Amount = (objPIDetail.Landed_Cost_Amount * dclratio) - (objPIDetail.Amt_Less_Discount * dclratio)


                    objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt * dclratio
                    objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt * dclratio
                    objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt * dclratio
                    objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt * dclratio
                    objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt * dclratio
                    objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt * dclratio
                    objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt * dclratio
                    objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt * dclratio
                    objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt * dclratio
                    objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt * dclratio




                    objVendorInvHead.Tax1_BAmount += objVendorInvDetail.TAX1_Base_Amt
                    objVendorInvHead.TAX1_Amt += objVendorInvDetail.TAX1_Amt
                    objVendorInvHead.Tax2_BAmount += objVendorInvDetail.TAX2_Base_Amt
                    objVendorInvHead.TAX2_Amt += objVendorInvDetail.TAX2_Amt
                    objVendorInvHead.Tax3_BAmount += objVendorInvDetail.TAX3_Base_Amt
                    objVendorInvHead.TAX3_Amt += objVendorInvDetail.TAX3_Amt
                    objVendorInvHead.Tax4_BAmount += objVendorInvDetail.TAX4_Base_Amt
                    objVendorInvHead.TAX4_Amt += objVendorInvDetail.TAX4_Amt
                    objVendorInvHead.Tax5_BAmount += objVendorInvDetail.TAX5_Base_Amt
                    objVendorInvHead.TAX5_Amt += objVendorInvDetail.TAX5_Amt
                    objVendorInvHead.Tax6_BAmount += objVendorInvDetail.TAX6_Base_Amt
                    objVendorInvHead.TAX6_Amt += objVendorInvDetail.TAX6_Amt
                    objVendorInvHead.Tax7_BAmount += objVendorInvDetail.TAX7_Base_Amt
                    objVendorInvHead.TAX7_Amt += objVendorInvDetail.TAX7_Amt
                    objVendorInvHead.Tax8_BAmount += objVendorInvDetail.TAX8_Base_Amt
                    objVendorInvHead.TAX8_Amt += objVendorInvDetail.TAX8_Amt
                    objVendorInvHead.Tax9_BAmount += objVendorInvDetail.TAX9_Base_Amt
                    objVendorInvHead.TAX9_Amt += objVendorInvDetail.TAX9_Amt
                    objVendorInvHead.Tax10_BAmount += objVendorInvDetail.TAX10_Base_Amt
                    objVendorInvHead.TAX10_Amt += objVendorInvDetail.TAX10_Amt

                    objVendorInvHead.Discount_Base += objVendorInvDetail.Amount
                    objVendorInvHead.Discount_Amount += objVendorInvDetail.Discount
                    objVendorInvHead.Amount_Less_Discount += objVendorInvDetail.Amount_less_Discount
                    objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                    objVendorInvHead.Total_Tax += (objVendorInvDetail.TAX1_Amt + objVendorInvDetail.TAX1_Amt + objVendorInvDetail.TAX2_Amt + objVendorInvDetail.TAX3_Amt + objVendorInvDetail.TAX4_Amt + objVendorInvDetail.TAX5_Amt + objVendorInvDetail.TAX6_Amt + objVendorInvDetail.TAX7_Amt + objVendorInvDetail.TAX8_Amt + objVendorInvDetail.TAX9_Amt + objVendorInvDetail.TAX10_Amt)
                    objVendorInvHead.Document_Total += objVendorInvDetail.Total_Amount
                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                    End If
                    ''End of Fill Vendor Invoice Detail Data
                Next
                objVendorInvHead.Balance_Amt = objVendorInvHead.Document_Total
                objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If

                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                objVendorInvHead.ConvRate = obj.ConvRate
                objVendorInvHead.ApplicableFrom = obj.ApplicableFrom
                ''end multicurrency

                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.PI_Date)
                ''End of create debit note for Rejected
            End If

            ''##############################################################################################################################




            '' Anubhooti 04-Nov-2014 Difference Entry (SRN-PI)
            Dim PIAmount As Double = 0

            '  Dim DiffDesp As String
            ' Dim RMConsuAcc As String

            Dim PayClrAmount As Double = 0
            Dim DiffAmount As Double = 0
            Dim SRNAmount As Double = 0
            'Dim dt1 As DataTable
            Dim counter As Integer = 0
            Dim ArryLst1 As ArrayList = New ArrayList()
            Dim objAdj As New ClsAdjustments()
            objAdj.Arr = New List(Of ClsAdjustmentsDetails)
            For Each objTr As clsMilkTransporterInvoiceMCCDetail In obj.Arr
                counter = counter + 1
                Dim intCount As Integer = obj.Arr.Count
                'SRNAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select (SRN_Qty*Landed_Cost_Rate) AS Amount From TSPL_SRN_DETAIL Where SRN_No ='" & objTr.SRN_Id & "' And Item_Code ='" & objTr.Item_Code & "'", trans))
                PIAmount = objTr.PI_Qty * objTr.Landed_Cost_Rate 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select (PI_Qty*Landed_Cost_Rate) AS Amount From TSPL_Mcc_Milk_Transport_Invoice_Detail Where SRN_Id ='" & objTr.SRN_Id & "' And Item_Code ='" & objTr.Item_Code & "'", trans))

                DiffAmount = SRNAmount - PIAmount


                'Dim SRNNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select SRN_Id from TSPL_Mcc_Milk_Transport_Invoice_Detail Where Doc_No ='" + objTr.SRN_Id + "'", trans))
                'Dim JrnlNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Journal_No  From TSPL_JOURNAL_MASTER Where Source_Doc_No ='" & SRNNo & "'", trans))
                'PayClrAmount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Amount From TSPL_JOURNAL_DETAILS Where Journal_No ='" & JrnlNo & "' AND Account_Code='" & clsCommon.myCstr(objTr.GL_Account_Code) & "'", trans))
                'DiffAmount = clsCommon.myCdbl(PIAmount + PayClrAmount)
                qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.RM_Consumption,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount,TSPL_PURCHASE_ACCOUNTS.Other_1 from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + objTr.Item_Code + "(" + objTr.Item_Code + ")")
                End If

                Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("RM_Consumption"))) <= 0 Then
                    Throw New Exception("Please set RM consumption account in purchase account set for item " + objTr.Item_Code + "(" + objTr.Item_Code + ")")
                End If
                'ArryLst1.Add(strPaybleCleanigCtrlAC)
                'ArryLst1.Add(strRMConsumAC)
                If DiffAmount <> 0 Then
                    Dim strRMConsumAC As String = clsCommon.myCstr(dt.Rows(0)("RM_Consumption"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                    strRMConsumAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strRMConsumAC, obj.Bill_To_Location, trans)
                    Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))
                    Dim strRMConsumACDesp As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strRMConsumAC + "'", trans))


                    Dim AccPayClrCr() As String = {strPaybleCleanigCtrlAC, DiffAmount}
                    Dim AccRMConsumDr() As String = {strRMConsumAC, -1 * DiffAmount}
                    ArryLst1.Add(AccPayClrCr)
                    ArryLst1.Add(AccRMConsumDr)

                    If objTr.Reject_Qty > 0 Then
                        Dim RejAmount As Double = objTr.Reject_Qty * objTr.Landed_Cost_Rate
                        Dim AccPayClrCrRej() As String = {strPaybleCleanigCtrlAC, IIf(DiffAmount > 0, 1, -1) * RejAmount}
                        ArryLst1.Add(AccPayClrCrRej)
                        Dim strRejectAC As String = clsCommon.myCstr(dt.Rows(0)("Other_1"))
                        If clsCommon.myLen(strRejectAC) <= 0 Then
                            Throw New Exception("Please set Reject account in purchase account set for item " + objTr.Item_Code + "(" + objTr.Item_Code + ")")
                        End If
                        strRejectAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strRejectAC, obj.Bill_To_Location, trans)
                        Dim AccRej() As String = {strRejectAC, IIf(DiffAmount > 0, -1, 1) * RejAmount}
                        ArryLst1.Add(AccRej)
                    End If



                    'ElseIf DiffAmount > 0 Then
                    '    Dim AccRMConsumCr() As String = {strRMConsumAC, -1 * DiffAmount}
                    '    Dim AccPayClrDr() As String = {strPaybleCleanigCtrlAC, 1 * DiffAmount}
                    '    ArryLst1.Add(AccRMConsumCr)
                    '    ArryLst1.Add(AccPayClrDr)
                    'End If

                    '' Anubhooti 03-Dec-2014 (Auto-Generated entry to Store Adjustment)

                    ' objAdj = objAdj.GetData(strDocNo, "Store Adjustment", NavigatorType.Current, trans)
                    objAdj.Adjustment_No = ""
                    objAdj.Adjustment_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
                    'objAdj.Reference = txtReference.Text
                    obj.Description = "Auto Adjustment Against PI -" & clsCommon.myCstr(obj.PI_No) & " And SRN -" & clsCommon.myCstr(obj.Against_SRN) & ""
                    objAdj.Unit_Code = "ALL"
                    ''obj.ItemType = "E" Fill at Detail level
                    objAdj.Loc_Code = clsCommon.myCstr(obj.Bill_To_Location)
                    objAdj.Loc_Desc = clsCommon.myCstr(obj.BillToLocationName)
                    If DiffAmount < 0 Then
                        objAdj.Trans_Type = clsCommon.myCstr("In")
                    Else
                        objAdj.Trans_Type = clsCommon.myCstr("Out")
                    End If
                    objAdj.chklocation = "N"
                    objAdj.IsMilkType = 0
                    objAdj.MainLocationCode = ""
                    objAdj.MainLocationDesc = ""
                    ' objAdj.Arr = New List(Of ClsAdjustmentsDetails)()
                    Dim isFirstTimeSA As Boolean = True
                    'For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
                        Dim objAdTr As New ClsAdjustmentsDetails()
                        objAdTr.Adjustment_Line_No = clsCommon.myCstr(clsCommon.myCdbl(counter))
                        objAdTr.Item_Code = clsCommon.myCstr(objTr.Item_Code)
                        If clsCommon.myLen(objAdTr.Item_Code) > 0 Then
                            objAdTr.Item_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') AS Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(objAdTr.Item_Code) & "'", trans))
                        Else
                            objAdTr.Item_Description = ""
                        End If

                        'objAdTr.Bar_Code = ""
                        objAdTr.Adjustment_Type = clsCommon.myCstr("Cost").Substring(0, 1) + IIf(clsCommon.CompairString(objAdj.Trans_Type, "In") = CompairStringResult.Equal, "I", "D")

                        objAdTr.Item_Quantity = clsCommon.myCdbl(0)
                        If DiffAmount < 0 Then
                            objAdTr.Item_Cost = clsCommon.myCdbl(-1 * DiffAmount)
                        Else
                            objAdTr.Item_Cost = DiffAmount
                        End If

                        objAdTr.Unit_Code = clsCommon.myCstr(objTr.Unit_code)
                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PURCHASE_ACCOUNTS.Adjustment_Account ,TSPL_GL_ACCOUNTS.Description  from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'", trans)
                        If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                            Throw New Exception("Plese set the Purchase Account set or its Adjustment Writeoff Account for item " + objAdTr.Item_Code)
                        End If
                        objAdTr.Account_Code = clsCommon.myCstr(dt1.Rows(0)("Adjustment_Account"))
                        objAdTr.Account_Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                        objAdTr.Remarks = ""
                        objAdTr.Comments = ""
                        objAdTr.mrp = clsCommon.myCdbl(0)
                        'objAdTr.MFG_Date = ""
                        objAdTr.Batch_No = ""
                        'objAdTr.Expiry_Date = ""

                        objAdTr.Breakage = clsCommon.myCdbl(0)
                        objAdTr.Breakage_Cost = clsCommon.myCdbl(0)
                        objAdTr.ItemType = ""
                        objAdTr.BreakageType = ""
                        objAdTr.LeakageQty = clsCommon.myCdbl(0)
                        objAdTr.Basic_Price = clsCommon.myCdbl(0)
                        objAdTr.fat_pers = clsCommon.myCdbl(0)
                        objAdTr.fat_kg = clsCommon.myCdbl(0)
                        objAdTr.snf_kg = clsCommon.myCdbl(0)
                        objAdTr.snf_pers = clsCommon.myCdbl(0)

                        objAdTr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objTr.Item_Code, trans)
                        'If isFirstTimeSA Then
                        '    objAdTr.ItemType = "CI"
                        '    isFirstTimeSA = False
                        'End If
                        'objAdTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))

                        'objAdTr.Itemstatus = clsCommon.myCstr(grow.Cells(colICodeStatus).Value)

                        'If clsCommon.myLen(objTr.Itemstatus) <= 0 Then
                        objAdTr.Itemstatus = "NEW"
                        'End If

                        If (clsCommon.myLen(objAdTr.Item_Code) > 0) Then
                            objAdj.Arr.Add(objAdTr)
                        End If
                    End If

                    'Dim isSavedAdj As Boolean = objAdj.SaveData(objAdj, True, "", trans)
                    'ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans, False)
                    'Next

                End If
            Next
            '' Afetr Loop Saved all difference items
            If objAdj IsNot Nothing AndAlso DiffAmount <> 0 Then
                Dim isSavedAdj As Boolean = objAdj.SaveData(objAdj, True, "", trans)
                ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans, False)
            End If

            If ArryLst1 IsNot Nothing AndAlso ArryLst1.Count > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.PI_Date, "Difference Entry Against PI-" & obj.PI_No & "", "PI-CM", "PI Consumption", obj.PI_No, obj.Description, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst1)
            End If

            ''

            qry = "Update TSPL_Mcc_Milk_Transport_Invoice_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.PI_Date, "dd/MM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Doc_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''Throw New Exception("AASFASFASDF")
            'isSaved = isSaved AndAlso obj.SaveDebitNoteEntry(obj, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsMilkTransporterInvoiceMCCDetail)) As String
        For Each objtr As clsMilkTransporterInvoiceMCCDetail In Arr
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
        Dim obj As clsMilkTransporterInvoiceMCC = clsMilkTransporterInvoiceMCC.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PI_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.mbtnPurchaseInvoice, obj.Bill_To_Location, clsCommon.myCDate(obj.PI_Date), trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_Mcc_Milk_Transport_Invoice_Detail where Doc_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = " delete from TSPL_PI_REMITTANCE where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PJV_Detail where PJV_No in (select TSPL_PJV_HEAD.PJV_No from TSPL_PJV_HEAD where TSPL_PJV_HEAD.Invoice_No='" + strCode + "')"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = " delete from TSPL_PJV_HEAD where Invoice_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Mcc_Milk_Transport_Invoice_HEAD where Doc_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

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

    Public Shared Function IsValidVendorForPI(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select Doc_No,Vendor_Code,Vendor_Name from TSPL_Mcc_Milk_Transport_Invoice_HEAD where Doc_No in (" + clsCommon.GetMulcallString(Arr) + ") and Vendor_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PI No:" + clsCommon.myCstr(dr("Doc_No")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Vendor_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Vendor_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function
End Class

Public Class clsMilkTransporterInvoiceMCCDetail
#Region "Variables"
    Public PI_No As String = Nothing
    Public Row_Type As String = Nothing
    Public Line_No As Integer = 0
    Public Status As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public PI_Qty As Double = 0
    Public SRN_Id As String = Nothing
    Public OrgSRNQty As Double = 0 'Not a Table Field
    Public Balance_Qty As Double = 0 '
    Public Free_qty As Double = 0
    Public Unit_code As String = Nothing '
    Public Location As String = Nothing '
    Public LocationName As String = Nothing 'Not a Table Field
    Public Item_Cost As Double = 0 '
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
    Public Disc_Type As Integer = 0
    Public Disc_Per As Double = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public PITax_Group As String = Nothing
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0
    Public Item_GL_Account As String = Nothing
    Public Item_GL_Account_Desc As String = Nothing
    Public Leak_Qty As Double = 0
    Public Burst_Qty As Double = 0
    Public Short_Qty As Double = 0
    Public Total_RecTax_PerUnit As Double = 0
    Public Total_NonRecTax_PerUnit As Double = 0
    Public Total_AddtionalCost_PerUnit As Double = 0
    Public Landed_Cost_Rate As Double = 0
    Public Landed_Cost_Amount As Double = 0
    Public Is_Mannual_Amt As Integer = 0
    Public Empty_Amount As Double = 0

    '' for Abatement PI
    Public AbatementRate As Decimal = 0
    Public AssessableMRP As Decimal = 0
    Public TotalAssessableMRP As Decimal = 0
    Public Bin_No As String = Nothing
    Public Reject_Qty As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strLocation As String, ByVal Arr As List(Of clsMilkTransporterInvoiceMCCDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkTransporterInvoiceMCCDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.PI_Qty)
                'clsCommon.AddColumnsForChange(coll, "SRN_Id", obj.SRN_Id, True)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                '----added by usha----
                clsCommon.AddColumnsForChange(coll, "Free_Qty", obj.Free_qty)
                '--------end here
                clsCommon.AddColumnsForChange(coll, "Reject_Qty", obj.Reject_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Type", obj.Disc_Type)
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
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Leak_Qty", obj.Leak_Qty)
                clsCommon.AddColumnsForChange(coll, "Burst_Qty", obj.Burst_Qty)
                clsCommon.AddColumnsForChange(coll, "Short_Qty", obj.Short_Qty)
                clsCommon.AddColumnsForChange(coll, "Total_RecTax_PerUnit", obj.Total_RecTax_PerUnit)
                clsCommon.AddColumnsForChange(coll, "Total_NonRecTax_PerUnit", obj.Total_NonRecTax_PerUnit)
                clsCommon.AddColumnsForChange(coll, "Total_AddtionalCost_PerUnit", obj.Total_AddtionalCost_PerUnit)
                clsCommon.AddColumnsForChange(coll, "Landed_Cost_Rate", obj.Landed_Cost_Rate)
                clsCommon.AddColumnsForChange(coll, "Landed_Cost_Amount", obj.Landed_Cost_Amount)
                clsCommon.AddColumnsForChange(coll, "Is_Mannual_Amt", obj.Is_Mannual_Amt)

                '' for abatement PO
                clsCommon.AddColumnsForChange(coll, "AbatementRate", obj.AbatementRate)
                clsCommon.AddColumnsForChange(coll, "AssessableMRP", obj.AssessableMRP)
                clsCommon.AddColumnsForChange(coll, "TotalAssessableMRP", obj.TotalAssessableMRP)

                If obj.Row_Type = clsItemRowType.RowTypeItem Then
                    Dim qry As String = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + obj.Item_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + obj.Item_Code + "(" + obj.Item_Desc + ")")
                    End If

                    obj.Item_GL_Account = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                    obj.Item_GL_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Item_GL_Account, strLocation, trans)
                    obj.Item_GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Item_GL_Account + "'", trans))

                    clsCommon.AddColumnsForChange(coll, "Item_GL_Account", obj.Item_GL_Account)
                    clsCommon.AddColumnsForChange(coll, "Item_GL_Account_Desc", obj.Item_GL_Account_Desc)
                End If

                If obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd-MMM-yyyy"))
                End If
                If obj.Expiry_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd-MMM-yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Assessable", obj.Assessable)
                clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)
                clsCommon.AddColumnsForChange(coll, "Empty_Amount", obj.Empty_Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Milk_Transport_Invoice_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function CompletePI(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_Mcc_Milk_Transport_Invoice_Detail set Status=1 where Doc_No='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

    Public Shared Function GetBalancePIQty(ByVal strPICode As String, ByVal strICode As String, ByVal strCurrPRNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double, ByVal isForReject As Boolean) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(   " & _
            " select TSPL_Mcc_Milk_Transport_Invoice_Detail.Item_Code as ICode,"
        If isForReject Then
            qry += " isnull( TSPL_Mcc_Milk_Transport_Invoice_Detail.Reject_Qty,0) as Qty,"
        Else
            qry += " TSPL_Mcc_Milk_Transport_Invoice_Detail.Qty as Qty,"
        End If
        qry += " 1 as RI  from TSPL_Mcc_Milk_Transport_Invoice_Detail " & _
            " left outer join TSPL_Mcc_Milk_Transport_Invoice_HEAD on TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No=TSPL_Mcc_Milk_Transport_Invoice_Detail.Doc_No where TSPL_Mcc_Milk_Transport_Invoice_Detail.Status=0 and TSPL_Mcc_Milk_Transport_Invoice_HEAD.Status=1 and TSPL_Mcc_Milk_Transport_Invoice_Detail.Doc_No ='" + strPICode + "' and TSPL_Mcc_Milk_Transport_Invoice_Detail.Item_Code='" + strICode + "' and  TSPL_Mcc_Milk_Transport_Invoice_Detail.Unit_code='" + strUOM + "' and isnull(TSPL_Mcc_Milk_Transport_Invoice_Detail.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_Mcc_Milk_Transport_Invoice_Detail.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' "
        If isForReject Then
            qry += " and isnull( TSPL_Mcc_Milk_Transport_Invoice_Detail.Reject_Qty,0)>0"
        End If
        qry += " union all  select TSPL_PR_DETAIL.Item_Code as ICode,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI from TSPL_PR_DETAIL left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No where TSPL_PR_DETAIL.PI_Id='" + strPICode + "'   and TSPL_PR_DETAIL.Item_Code='" + strICode + "' and  TSPL_PR_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PR_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PR_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' and TSPL_PR_DETAIL.PR_No not in ('" + strCurrPRNo + "')"
        If isForReject Then
            qry += " and TSPL_PR_HEAD.is_Reject_Item=1 "
        End If
        If isForReject Then
            qry += " Union all select TSPL_Transfer_ORDER_DETAIL.Item_Code,TSPL_Transfer_ORDER_DETAIL.Out_Qty,-1 as RI from TSPL_Transfer_ORDER_DETAIL left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_ORDER_DETAIL.Document_No where LEN( ISNULL( TSPL_TRANSFER_ORDER_HEAD.RMDA_Code,''))>0  and TSPL_Transfer_ORDER_DETAIL.Item_Code='" + strICode + "' and TSPL_Transfer_ORDER_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_Transfer_ORDER_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and TSPL_TRANSFER_ORDER_HEAD.RMDA_Code in (select MAX(TSPL_SRN_HEAD.RMDA_No) from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left outer join TSPL_Mcc_Milk_Transport_Invoice_Detail on TSPL_Mcc_Milk_Transport_Invoice_Detail.SRN_Id=TSPL_SRN_DETAIL.SRN_No where TSPL_Mcc_Milk_Transport_Invoice_Detail.Doc_No='" + strPICode + "')  and TSPL_Transfer_ORDER_DETAIL.Document_No not in ('" + strCurrPRNo + "')"
        End If
        qry += " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    'Public Shared Function GetBalanceRejectedQty(ByVal strRMDACode As String, ByVal strICode As String) As Double
    '    Dim qry As String = "select SUM(qty * RI) as Balance from(   "
    '    qry += " select TSPL_Mcc_Milk_Transport_Invoice_Detail.Item_Code as ICode,isnull( TSPL_Mcc_Milk_Transport_Invoice_Detail.Reject_Qty,0) as Qty, 1 as RI  from TSPL_Mcc_Milk_Transport_Invoice_Detail  left outer join TSPL_Mcc_Milk_Transport_Invoice_HEAD on TSPL_Mcc_Milk_Transport_Invoice_HEAD.Doc_No=TSPL_Mcc_Milk_Transport_Invoice_Detail.Doc_No where TSPL_Mcc_Milk_Transport_Invoice_Detail.Status=0 and TSPL_Mcc_Milk_Transport_Invoice_HEAD.Status=1 and TSPL_Mcc_Milk_Transport_Invoice_Detail.Doc_No ='" + strPICode + "' and TSPL_Mcc_Milk_Transport_Invoice_Detail.Item_Code='" + strICode + "' and  TSPL_Mcc_Milk_Transport_Invoice_Detail.Unit_code='" + strUOM + "' and isnull(TSPL_Mcc_Milk_Transport_Invoice_Detail.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_Mcc_Milk_Transport_Invoice_Detail.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' "
    '    qry += " and isnull( TSPL_Mcc_Milk_Transport_Invoice_Detail.Reject_Qty,0)>0"
    '    qry += " union all  select TSPL_PR_DETAIL.Item_Code as ICode,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI from TSPL_PR_DETAIL left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No where TSPL_PR_DETAIL.PI_Id='" + strPICode + "'   and TSPL_PR_DETAIL.Item_Code='" + strICode + "' and  TSPL_PR_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PR_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PR_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' and TSPL_PR_DETAIL.PR_No not in ('" + strCurrPRNo + "')"
    '    qry += " and TSPL_PR_HEAD.is_Reject_Item=1 "
    '    qry += " )Final "
    '    Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    'End Function
End Class


